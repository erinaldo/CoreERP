using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Glb.New.DDFe
{
    public partial class frmManifesto : Form
    {
        Class.DDFeAPI DDFe = new Class.DDFeAPI();

        public frmManifesto()
        {
            InitializeComponent();

            #region Combobox

            List<PS.Lib.ComboBoxItem> listEventos = new List<Lib.ComboBoxItem>();

            listEventos.Add(new PS.Lib.ComboBoxItem(210200, "Confirmação da Operação"));
            listEventos.Add(new PS.Lib.ComboBoxItem(210210, "Ciência da Operação"));
            listEventos.Add(new PS.Lib.ComboBoxItem(210220, "Desconhecimento da Operação"));
            listEventos.Add(new PS.Lib.ComboBoxItem(210240, "Operação não Realizada"));

            cmbEvento.DataSource = listEventos;
            cmbEvento.DisplayMember = "DisplayMember";
            cmbEvento.ValueMember = "ValueMember";

            #endregion
        }

        private void frmManifesto_Load(object sender, EventArgs e)
        {
            tbJustificativa.Enabled = false;
        }

        private void cmbEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEvento.SelectedValue.Equals(210240))
            {
                tbJustificativa.Enabled = true;
            }
            else
            {
                tbJustificativa.Enabled = false;
            }
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            string Token = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TOKEN FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text }).ToString();

            if (string.IsNullOrEmpty(lpFilial.txtcodigo.Text))
            {
                MessageBox.Show("Para a execução do processo é necessário informar a filial.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbEvento.SelectedValue.Equals(210240) && tbJustificativa.Text == string.Empty)
            {
                MessageBox.Show("Para eventos do tipo 'Operação não Realizada' é necessário informar a justificativa.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("Processando...");

                DataTable dtFilial = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GFILIAL WHERE CODFILIAL = ?", new object[] { lpFilial.txtcodigo.Text });

                string CNPJ = dtFilial.Rows[0]["CGCCPF"].ToString().Replace(".", "").Replace("-", "").Replace("/", "");
                string Destinatario = dtFilial.Rows[0]["NOME"].ToString();
                string UfDestinatario = dtFilial.Rows[0]["CODETD"].ToString();

                string RetornoManifesto = DDFe.ManifestacaoDDFe(Token, CNPJ, tbChave.Text, Convert.ToInt32(cmbEvento.SelectedValue), tbJustificativa.Text);

                dynamic JsonRetornoManifesto = JsonConvert.DeserializeObject(RetornoManifesto);

                string StatusManifesto = JsonRetornoManifesto.status;

                if (StatusManifesto == "200")
                {
                    dynamic RetEvento = JsonRetornoManifesto.retEvento;

                    string IdEvento = setIdEvento();
                    string Cstat = RetEvento.cStat;
                    string Retorno = RetEvento.xMotivo;
                    string Protocolo = RetEvento.nProt;
                    DateTime DataEvento = RetEvento.dhRegEvento;
                    string XMLRetorno = RetEvento.xml;

                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GDDFEMANIFESTO (IDEVENTOMDNF, CODEMPRESA, CHNFE, TPEVENTO, JUSTIFICATIVA, USUARIOINCLUSAO, DATAEVENTO, RETORNO, NPROT, CSTAT, XMLRETORNO)
                                                                                 VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { IdEvento, AppLib.Context.Empresa, tbChave.Text, cmbEvento.SelectedValue, tbJustificativa.Text, AppLib.Context.Usuario, DataEvento, Retorno, Protocolo, Cstat, XMLRetorno });
                }
                else
                {
                    // Tratar Retorno
                }
            }
            catch (Exception ex)
            {
                
            }

            splashScreenManager1.CloseWaitForm();
            MessageBox.Show("Processo realizado com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Dispose();
        }

      
        public string setIdEvento()
        {
            string IdManifesto = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT ISNULL(MAX(IDEVENTOMDNF), 0) + 1 FROM GDDFEMANIFESTO WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();
            return IdManifesto;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
