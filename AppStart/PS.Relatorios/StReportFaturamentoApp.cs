using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Relatorios
{
    public partial class StReportFaturamentoApp : PS.Lib.WinForms.StaticReport.FrmBaseAppStaticReport
    {
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        ListViewItem item = new ListViewItem();
        List<parametro> parametro = new List<parametro>();
        BindingSource bs = new BindingSource();

        public StReportFaturamentoApp()
        {
            InitializeComponent();
        }
        private bool verificaCampos()
        {
            if (cmbGrupoRel.SelectedIndex.Equals(-1) || cmbEmpresa.SelectedIndex.Equals(-1) || cmbFilial.SelectedIndex.Equals(-1))
            {
                return false;
            }
            return true;
        }
        public override void Execute()
        {
            base.Execute();
            //Remove os parâmetros caso existam
            if (this.Parameters.Count > 2)
            {
                this.Parameters.RemoveRange(2, (Parameters.Count - 2));
            }
            //Verifica se os campos foram preenchidos corretamente.
            if (verificaCampos().Equals(false))
            {
                MessageBox.Show("Favor preencher os campos corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Adicionam os parâmetros obrigatórios
            this.Parameters.Add(new PS.Lib.DataField("EMPRESA", cmbEmpresa.Text));
            this.Parameters.Add(new PS.Lib.DataField("FILIAL", cmbFilial.Text));
            this.Parameters.Add(new PS.Lib.DataField("CODSTATUS", getCampo(cmbMovimento.Text)));
            this.Parameters.Add(new PS.Lib.DataField("DATAINICIAL", psDateBoxIni.Text));
            this.Parameters.Add(new PS.Lib.DataField("DATAFINAL", psDateBoxFin.Text));
            this.Parameters.Add(new PS.Lib.DataField("IDGRUPOREL", cmbGrupoRel.Text));
            //Adicionam os parâmetros opicionais
            for (int i = 0; i < dgvParam.Rows.Count - 1; i++)
            {
                List<PS.Lib.DataField> param = this.Parameters;
                param.Add(new PS.Lib.DataField("OPERADOR", dgvParam.Rows[i].Cells[0].Value));
                param.Add(new PS.Lib.DataField("CAMPO", dgvParam.Rows[i].Cells[1].Value));
                param.Add(new PS.Lib.DataField("CONDICAO", dgvParam.Rows[i].Cells[2].Value));
                param.Add(new PS.Lib.DataField("VALOR", dgvParam.Rows[i].Cells[3].Value));
            }
            //Adiciona a Ordem
            this.Parameters.Add(new PS.Lib.DataField("ORDEM", getCampo(cmbOrdem.Text)));
            this.Parameters.Add(new PS.Lib.DataField("TIPOORDEM", (cmbTipoOrdem.Text.Equals("DECRESCENTE") ? "DESC" : "").ToString()));

            //Chamada para o relatório.
            this.Report = new XrFaturamento(this.Parameters, getCampo(cmbGrupo.Text));
            PS.Lib.WinForms.Report.ReportDesignTool rp = new PS.Lib.WinForms.Report.ReportDesignTool(Report);
            rp.ShowPreviewDialog();
        }
        private string getCampo(string nomeCampo)
        {
            switch (nomeCampo)
            {
                case "ABERTO":
                    return "0";
                case "FATURADO":
                    return "1";
                case "CANCELADO":
                    return "2";
                case "TODOS":
                    return "3";
                case "NÚMERO":
                    return "NUMERO";
                case "EMISSÃO":
                    return "DATAEMISSAO";
                case "CCUSTO":
                    return "CCUSTO";
                case "CLIENTE":
                    return "CLIENTE";
                case "NATUREZA":
                    return "NATUREZA";
                case "OPERAÇÃO":
                    return "CODTIPOPER";
                case "VENDEDOR":
                    return "VENDEDOR";
                default:
                    return nomeCampo;
            }
        }

        private void StReportFinanceiroApp_Load(object sender, EventArgs e)
        {
            CarregaCombo();
            //Tratamento dos campos de data.
            psDateBoxIni.Text = DateTime.Now.AddDays(-30).ToShortDateString();
            psDateBoxFin.Text = DateTime.Now.ToShortDateString();
        }
        private void CarregaCombo()
        {
            //Empresa
            string sql = @"SELECT CODEMPRESA FROM GEMPRESA";
            DataTable dt = dbs.QuerySelect(sql, new object[] { });
            cmbEmpresa.DataSource = dt;
            cmbEmpresa.DisplayMember = "CODEMPRESA";
            //Filial
            sql = @"SELECT CODFILIAL FROM GFILIAL";
            dt = new DataTable();
            dt = dbs.QuerySelect(sql, new object[] { });
            cmbFilial.DataSource = dt;
            cmbFilial.DisplayMember = "CODFILIAL";
            //Grupo Relatório
            DataTable dt1 = new DataTable();
            dt1 = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IDGRUPOREL, GRUPO FROM GGRUPOREL WHERE ATIVO = ?", new object[] { 1 });
            dt1.Rows.Add(new object[] { dt1.Rows.Count + 1, "TODOS" });
            cmbGrupoRel.DataSource = dt1;//AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IDGRUPOREL, GRUPO FROM GGRUPOREL WHERE ATIVO = ?", new object[] { 1 });
            cmbGrupoRel.DisplayMember = "GRUPO";
            cmbGrupoRel.ValueMember = "IDGRUPOREL";
        }
        private void limparCampos()
        {
            cmbOperador.SelectedIndex = -1;
            cmbCampo.SelectedIndex = -1;
            cmbCondicao.SelectedIndex = -1;
            txtValor.Clear();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvParam.Rows)
            {
                if (dgvr.Selected == true)
                {
                    dgvParam.Rows.Remove(dgvr);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            parametro param = new parametro();
            param.OPERADOR = cmbOperador.Text;
            param.CAMPO = getCamposParamentro(cmbCampo.Text);
            param.CONDICAO = cmbCondicao.Text;
            param.VALOR = txtValor.Text;
            parametro.Add(param);
            bs.DataSource = null;
            bs.DataSource = parametro;
            dgvParam.DataSource = bs;
            limparCampos();
        }
        private string getCamposParamentro(string campo)
        {
            switch (campo)
            {
                case "NÚMERO":
                    return "GOPER.NUMERODOC";
                case "CLIENTE":
                    return "VCLIFOR.NOME";
                case "CCUSTO":
                    return "GCENTROCUSTO.NOME";
                case "CODTIPOPER":
                    return "GOPER.CODTIPOPER";
                default :
                    return campo;
            }
        }
        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbGrupoRel_Validating(object sender, CancelEventArgs e)
        {
            if (cmbGrupoRel.Text.Equals("TODOS"))
            {
                cmbCampo.Items.Add("CODTIPOPER");
            }
            else
            {
                cmbCampo.Items.Remove("CODTIPOPER");
            }
        }
    }
}
