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
    public partial class StReportComissaoRepresentante2010App : PS.Lib.WinForms.StaticReport.FrmBaseAppStaticReport
    {
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        ListViewItem item = new ListViewItem();
        List<parametro> parametro = new List<parametro>();
        BindingSource bs = new BindingSource();

        public StReportComissaoRepresentante2010App()
        {
            InitializeComponent();
            psLookup15.PSPart = "PSPartRepre";
        }
        public override void Execute()
        {
            base.Execute();
            //Remove os parâmetros caso existam
            if (this.Parameters.Count > 2)
            {
                this.Parameters.RemoveRange(2, (Parameters.Count - 2));
            }
            //Verfica se os campos estão preenchidos corretamente.
            if (string.IsNullOrEmpty(psLookup15.Text) || string.IsNullOrEmpty(cmbEmpresa.Text) || string.IsNullOrEmpty(cmbFilial.Text) || string.IsNullOrEmpty(psDateBoxIni.Text) || string.IsNullOrEmpty(psDateBoxFin.Text))
            {
                MessageBox.Show("Favor preencher corretamente os parâmetros obrigatórios.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Adicionam os parâmetros obrigatórios
            this.Parameters.Add(new PS.Lib.DataField("EMPRESA", cmbEmpresa.Text));
            this.Parameters.Add(new PS.Lib.DataField("FILIAL", cmbFilial.Text));
            this.Parameters.Add(new PS.Lib.DataField("DATAINICIAL", psDateBoxIni.Text));
            this.Parameters.Add(new PS.Lib.DataField("DATAFINAL", psDateBoxFin.Text));
            this.Parameters.Add(new PS.Lib.DataField("CODREPRE", psLookup15.Text));
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
            this.Report = new XrComissaoRepresentante(this.Parameters);
            PS.Lib.WinForms.Report.ReportDesignTool rp = new PS.Lib.WinForms.Report.ReportDesignTool(Report);
            rp.ShowPreviewDialog();
        }
        private void CarregaCombo()
        {
            //Empresa
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODEMPRESA FROM GEMPRESA", new object[] { });
            cmbEmpresa.DataSource = dt;
            cmbEmpresa.DisplayMember = "CODEMPRESA";
            //Filial
            dt = new DataTable();
            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODFILIAL FROM GFILIAL", new object[] { });
            cmbFilial.DataSource = dt;
            cmbFilial.DisplayMember = "CODFILIAL";
        }
        private string getCampo(string nomeCampo)
        {
            switch (nomeCampo)
            {
                case "DT. EMISSAO":
                    return "GOPER.DATAEMISSAO";
                case "Nº MOVIMENTO":
                    return "GOPER.NUMERO";
                case "CLIENTE":
                    return "VCLIFOR.NOMEFANTASIA";
                case "VL. LIQUIDO":
                    return "GOPER.VALORLIQUIDO";
                case "VL. COMISSAO":
                    return "GOPER.VALORCOMISSAO";
                default:
                    return string.Empty;
            }
        }

        private void StReportComissaoRepresentante2010App_Load(object sender, EventArgs e)
        {
            CarregaCombo();
            //Tratamento dos campos de data.
            psDateBoxIni.Text = DateTime.Now.AddDays(-30).ToShortDateString();
            psDateBoxFin.Text = DateTime.Now.ToShortDateString();
        }
        private void limparCampos()
        {
            cmbOperador.SelectedIndex = -1;
            cmbCampo.SelectedIndex = -1;
            cmbCondicao.SelectedIndex = -1;
            txtValor.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            parametro param = new parametro();
            param.OPERADOR = cmbOperador.Text;
            param.CAMPO = getCampo(cmbCampo.Text);
            param.CONDICAO = cmbCondicao.Text;
            param.VALOR = txtValor.Text;
            parametro.Add(param);
            bs.DataSource = null;
            bs.DataSource = parametro;
            dgvParam.DataSource = bs;
            limparCampos();
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
    }
}
