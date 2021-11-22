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
    public partial class StReportFinanceiroApp : PS.Lib.WinForms.StaticReport.FrmBaseAppStaticReport
    {
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        ListViewItem item = new ListViewItem();
        List<parametro> parametro = new List<parametro>();
        BindingSource bs = new BindingSource();

        public StReportFinanceiroApp()
        {
            InitializeComponent();
        }
        public override void Execute()
        {
            base.Execute();
            //Remove os parâmetros caso existam
            if (this.Parameters.Count > 2)
            {
                this.Parameters.RemoveRange(2, (Parameters.Count - 2));
            }
            //Adicionam os parâmetros obrigatórios
            this.Parameters.Add(new PS.Lib.DataField("EMPRESA", cmbEmpresa.Text));
            this.Parameters.Add(new PS.Lib.DataField("FILIAL", cmbFilial.Text));
            this.Parameters.Add(new PS.Lib.DataField("TIPO", cmbTipo.Text));
            this.Parameters.Add(new PS.Lib.DataField("CODSTATUS", cmbStatus.Text));
            this.Parameters.Add(new PS.Lib.DataField("DATAINICIAL", psDateBoxIni.Text));
            this.Parameters.Add(new PS.Lib.DataField("DATAFINAL", psDateBoxFin.Text));
            this.Parameters.Add(new PS.Lib.DataField("TIPODATA", cmbTipoData.Text));
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
            this.Report = new XrFinanceiro(this.Parameters, getCampo(cmbGrupo.Text));
            PS.Lib.WinForms.Report.ReportDesignTool rp = new PS.Lib.WinForms.Report.ReportDesignTool(Report);
            rp.ShowPreviewDialog();
        }
        private string getCampo(string nomeCampo)
        {
            switch (nomeCampo)
            {
                case "ABERTO":
                    return "0";
                case "BAIXADO":
                    return "1";
                case "CANCELADO":
                    return "2";
                case "TODOS":
                    return "3";
                case "A PAGAR":
                    return "0";
                case "A RECEBER":
                    return "1";
                case "NÚMERO":
                    return "NUMERO";
                case "EMISSÃO":
                    return "DATAEMISSAO";
                case "VENCTO.":
                    return "DATAVENCIMENTO";
                case "DATA BAIXA":
                    return "DATABAIXA";
                case "CCUSTO":
                    return "CCUSTO";
                case "CLIENTE":
                    return "CLIENTE";
                case "NATUREZA":
                    return "NATUREZA";
                case "VL. ORIGINAL":
                    return "VLORIGINAL";
                case "VL. LIQUIDO":
                    return "VLLIQUIDO";
                case "FORMA DE PAGAMENTO":
                    return "NOME";
                case "COD NATUREZA":
                    return "DESCRICAONATUREZA";
                case "CÓDIGO DO TIPO DE DOCUMENTO":
                    return "TIPO DO DOCUMENTO";
                case "´CÓDIGO DO CLIENTE":
                    return "CODIGO DO CLIENTE";
                default:
                    return string.Empty;
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
            //param.CAMPO = getCamposParametro(cmbCampo.Text); - Comentado pois não estava validando o combo de Campo - Jõão Pedro Luchiari - 09/04/2018.
            if (string.IsNullOrEmpty(cmbCampo.Text))
            {
                return;
            }
            param.CAMPO = getCamposParametro(cmbCampo.SelectedItem.ToString());
            param.CONDICAO = cmbCondicao.Text;
            param.VALOR = txtValor.Text;
            parametro.Add(param);
            bs.DataSource = null;
            bs.DataSource = parametro;
            dgvParam.DataSource = bs;
            limparCampos();
        }
        private string getCamposParametro(string campo)
        {
            switch (campo)
            {
                case "NÚMERO":
                    return "FLANCA.NUMERO";
                case "CLIENTE":
                    return "VCLIFOR.NOME";
                case "CENTRO DE CUSTO":
                    return "GCENTROCUSTO.NOME";
                case "NATUREZA":
                    return "VNATUREZAORCAMENTO.DESCRICAO";
                case "VENCTO":
                    return "FLANCA.DATAVENCIMENTO";
                case "COD NATUREZA":
                    return "VNATUREZAORCAMENTO.CODNATUREZA";
                case "CÓDIGO DO TIPO DE DOCUMENTO":
                    return "FLANCA.CODTIPDOC";
                case "CÓDIGO DO CLIENTE":
                    return "VCLIFOR.CODCLIFOR";
                default:
                    return string.Empty;
            }
        }
        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
