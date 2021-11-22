using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.NotaFiscal
{
    public partial class frmMonitorSefaz : Form
    {
        Class.NFeAPI NfeAPI = new Class.NFeAPI();
        string Token = string.Empty;

        public frmMonitorSefaz()
        {
            InitializeComponent();
        }

        private void frmMonitorSefaz_Load(object sender, EventArgs e)
        {
            tbModelo.Text = "Nota Fiscal Elettrônica - Modelo 55";
            tbModelo.ReadOnly = true;
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            if (ValidaFilial() == false)
            {
                return;
            }

            Token = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TOKEN FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text }).ToString();

            string Cnpj = NfeAPI.getCGCCPFFormatado(Convert.ToInt32(lpFilial.txtcodigo.Text));
            string TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text }).ToString();
            string Versao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT VERSAO FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text }).ToString();
            string Uf = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT GESTADO.CODIBGE FROM GFILIAL INNER JOIN GESTADO ON GFILIAL.CODETD = GESTADO.CODETD WHERE GFILIAL.CODEMPRESA = ? AND GFILIAL.CODFILIAL = ? ", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text }).ToString();

            string retornoStatusNf = NfeAPI.StatusNFOperacao(Token, Cnpj, TpAmb, Uf, Versao);

            dynamic JsonRetornoConsultaStatusNf = JsonConvert.DeserializeObject(retornoStatusNf);
            string StatusMonitor = JsonRetornoConsultaStatusNf.status;
            string cStat = JsonRetornoConsultaStatusNf.retStatusServico.cStat;
            string Motivo = JsonRetornoConsultaStatusNf.retStatusServico.xMotivo;
            string Mensagem = string.Empty;

            if (StatusMonitor == "200")
            {
                string UfOrigem = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM GESTADO WHERE CODIBGE = ?", new object[] { Uf }).ToString();
                string Ambiente = string.Empty;

                if (TpAmb == "1")
                {
                    Ambiente = "Tipo de Ambiente: 1 - Produção";
                }
                else if (TpAmb == "2")
                {
                    Ambiente = "Tipo de Ambiente: 2 - Homologação";
                }

                Mensagem = "Modelo: 55." + "\r\n" + Ambiente + "." + "\r\n" + "Versão: " + Versao + "." + "\r\n" + "UF Origem: " + UfOrigem + "." + "\r\n" + "Mensagem: " + Motivo.ToUpper() + "." + "\r\n\r\n Consultado em " + DateTime.Now + "." + "";

                if (cStat == "107")
                {
                    MessageBox.Show(Mensagem, "Monitor Sefaz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show(Mensagem, "Monitor Sefaz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Desenvolver a chamada do formulário de Alteração de Modalidade
                }
            }
            else
            {
                MessageBox.Show("Não foi possivel consultar a disponibilidade do SEFAZ, favor tente novamente em alguns segundos.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Validações

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidaFilial()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(lpFilial.txtcodigo.Text))
            {
                errorProvider1.SetIconAlignment(lpFilial.txtcodigo, ErrorIconAlignment.BottomRight);
                errorProvider1.SetError(lpFilial.txtcodigo, "A Filial precisa ser preenchida.");
                verifica = false;
            }

            return verifica;
        }

        #endregion
    }
}
