using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroNotaFiscalEstadual : Form
    {
        public bool edita = false;
        public int CodOper;

        public frmCadastroNotaFiscalEstadual()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GNFESTADUAL");
        }

        private void frmCadastroNotaFiscalEstadual_Load(object sender, EventArgs e)
        {
            carregaCampos();
            CarregaLogNfe(CodOper);
        }

        private void carregaCampos()
        {
            DataTable dt;
            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CodOper });
            if (dt.Rows.Count > 0)
            {
                carregaObj(dt);
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodOper.Text = dt.Rows[0]["CODOPER"].ToString();
            tbCodStatus.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT DESCRICAO FROM GSTATUS WHERE CODEMPRESA = ? AND TABELA = ? AND CODSTATUS = ?", new object[] { AppLib.Context.Empresa, "GNFESTADUAL", dt.Rows[0]["CODSTATUS"].ToString() }).ToString();
            chkDanfeImpressa.Checked = Convert.ToBoolean(dt.Rows[0]["DANFEIMPRESSA"]);
            chkEmailEnviado.Checked = Convert.ToBoolean(dt.Rows[0]["EMAILENVIADO"]);
            tbCodtipoper.Text = dt.Rows[0]["CODTIPOPER"].ToString();
            tbFormatoImpressao.Text = dt.Rows[0]["FORMATOIMPRESSAO"].ToString();
            tbNumero.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NUMERO FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CodOper }).ToString();
            tbSerie.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODSERIE FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CodOper }).ToString();
            lpFilial.txtcodigo.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODFILIAL FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, dt.Rows[0]["CODOPER"].ToString() }).ToString();
            lpFilial.CarregaDescricao();
            lpCliente.txtcodigo.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT GOPER.CODCLIFOR FROM GOPER INNER JOIN GNFESTADUAL ON GOPER.CODEMPRESA = GNFESTADUAL.CODEMPRESA AND GOPER.CODOPER = GNFESTADUAL.CODOPER INNER JOIN VCLIFOR ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR WHERE GNFESTADUAL.CODOPER = ?", new object[] { CodOper }).ToString();
            lpCliente.CarregaDescricao();
            tbCnpjCpf.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CGCCPF FROM VCLIFOR WHERE CODEMPRESA = ? AND CODCLIFOR = ? ", new object[] { AppLib.Context.Empresa, lpCliente.txtcodigo.Text }).ToString();
            tbChaveAcesso.Text = dt.Rows[0]["CHAVEACESSO"].ToString();

            if (!string.IsNullOrEmpty(getDataEmissao(CodOper).ToString()))
            {
                dteEmissao.DateTime = this.getDataEmissao(CodOper);
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATARECIBO"].ToString()))
            {
                dteRecibo.DateTime = Convert.ToDateTime(dt.Rows[0]["DATARECIBO"]);
            }

            tbRecibo.Text = dt.Rows[0]["RECIBO"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATAPROTOCOLO"].ToString()))
            {
                dteProtocolo.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAPROTOCOLO"]);
            }

            tbProtocolo.Text = dt.Rows[0]["PROTOCOLO"].ToString();

            MemoXmlGerado.Text = dt.Rows[0]["XMLGERADO"].ToString();
            //MemoXmlRec.Text = dt.Rows[0]["XMLREC"].ToString();
            //MemoXmlProt.Text = dt.Rows[0]["XMLPROT"].ToString();
            MemoXmlNfe.Text = dt.Rows[0]["XMLNFE"].ToString();

            if (dt.Rows[0]["CODSTATUS"].ToString() == "C")
            {
                MemoXMLCan.Text = CarregaEventoCancelamento(CodOper).Rows[0]["XMLENV"].ToString();
                MemoCancelamento.Text = CarregaEventoCancelamento(CodOper).Rows[0]["MOTIVO"].ToString();
            }
        }

        /// <summary>
        /// Método para buscar a Data de Emissão da Nota Fiscal.
        /// </summary>
        /// <param name="Codoper">Código da Operação</param>
        /// <param name="Idoutbox">Id da Integração(IDOUTBOX)</param>
        /// <returns>Data de Emissão</returns>
        private DateTime getDataEmissao(int Codoper)
        {
            DateTime DataEmissao = Convert.ToDateTime(AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT GOPER.DATAEMISSAO FROM GOPER INNER JOIN GNFESTADUAL ON GOPER.CODEMPRESA = GNFESTADUAL.CODEMPRESA AND GNFESTADUAL.CODOPER = GOPER.CODOPER WHERE GNFESTADUAL.CODOPER = ?", new object[] { CodOper }).ToString());

            return DataEmissao;
        }

        /// <summary>
        /// Método para buscar O XML de envio e o motivo de cancelamento da Nota Fiscal.
        /// </summary>
        /// <param name="Codoper">Código da Operação</param>
        /// <param name="Idoutbox">Id da Integração(IDOUTBOX)</param>
        /// <returns>XML de Envio(Cancelamento)/Motivo de Cancelamento</returns>
        private DataTable CarregaEventoCancelamento(int Codoper)
        {
            DataTable dtCancelamento = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT XMLENV, MOTIVO FROM GNFESTADUALCANC WHERE CODOPER = ?", new object[] { Codoper });

            if (dtCancelamento.Rows.Count > 0)
            {
                return dtCancelamento;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Método para carregar o Log da Nota Fiscal.
        /// </summary>
        /// <param name="Codoper">Código da Operação</param>
        /// <param name="Idoutbox">Id da Integração(IDOUTBOX)</param>
        private void CarregaLogNfe(int Codoper)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IDHISTORICO AS 'ID Histórico', DATA AS 'Data', OBSERVACAO AS 'Observação' FROM GNFESTADUALHISTORICO INNER JOIN GNFESTADUAL ON GNFESTADUALHISTORICO.CODEMPRESA = GNFESTADUAL.CODEMPRESA AND GNFESTADUALHISTORICO.CODOPER = GNFESTADUAL.CODOPER WHERE GNFESTADUAL.CODEMPRESA = ? AND GNFESTADUAL.CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper });          
            gridControl1.DataSource = dt;
            gridView1.BestFitColumns();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsFind.AlwaysVisible == true)
            {
                gridView1.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView1.OptionsFind.AlwaysVisible = true;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsView.ShowGroupPanel == true)
            {
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.ClearGrouping();
                toolStripButton3.Text = "Agrupar";
            }
            else
            {
                gridView1.OptionsView.ShowGroupPanel = true;
                toolStripButton3.Text = "Desagrupar";
            }
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
