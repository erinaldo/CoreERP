using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Glb.New.Processos
{
    public partial class frmReenviarEmail : Form
    {
        #region Variáveis

        public List<int> listIDApontamento = new List<int>();
        private Classes.Apontamento apontamento = new Classes.Apontamento();

        string para = "";
        string emailsAdicionais = "";
        string cc = "";
        string cco = "";

        int idProjeto = 0;

        #endregion

        public frmReenviarEmail()
        {
            InitializeComponent();
        }

        private void frmReenviarEmail_Load(object sender, EventArgs e)
        {
            //if (!ValidaEmailAdicional())
            //{
            //    tsEmailsAdicionais.Enabled = false;
            //}
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();

                for (int i = 0; i < listIDApontamento.Count; i++)
                {
                    // Para
                    if (!string.IsNullOrEmpty(para))
                    {
                        apontamento.ReenviarEmail(listIDApontamento[i], i, para);
                    }

                    // E-mails Adicionais
                    if (!string.IsNullOrEmpty(emailsAdicionais))
                    {
                        apontamento.ReenviarEmail(listIDApontamento[i], i, emailsAdicionais);
                    }

                    // CC
                    if (!string.IsNullOrEmpty(cc))
                    {
                        apontamento.ReenviarEmail(listIDApontamento[i], i, cc);
                    }

                    // CCo
                    if (!string.IsNullOrEmpty(cco))
                    {
                        apontamento.ReenviarEmail(listIDApontamento[i], i, cco);
                    }
                }

                splashScreenManager1.CloseWaitForm();

                XtraMessageBox.Show(apontamento.TratamentoMensagemReenvioEmail(), "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Erro ao enviar email para o Apontamento.\r\nDetalhes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void tsPara_Toggled(object sender, EventArgs e)
        {
            if (tsPara.IsOn)
            {
                para = "Para";
            }
            else
            {
                para = "";
            }
        }

        private void tsEmailsAdicionais_Toggled(object sender, EventArgs e)
        {
            if (tsEmailsAdicionais.IsOn)
            {
                emailsAdicionais = "Emails Adicionais";
            }
            else
            {
                emailsAdicionais = "";
            }
        }

        private void tsCC_Toggled(object sender, EventArgs e)
        {
            if (tsCC.IsOn)
            {
                cc = "CC";
            }
            else
            {
                cc = "";
            }
        }

        private void tsCCo_Toggled(object sender, EventArgs e)
        {
            if (tsCCo.IsOn)
            {
                cco = "CCo";
            }
            else
            {
                cco = "";
            }
        }

        #region Métodos

        private bool ValidaEmailAdicional()
        {
            bool valida = true;

            string emailAdicional = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"SELECT EMAIL FROM APROJETOEMAIL WHERE CODCOLIGADA = ? AND CODFILIAL = ? AND IDPROJETO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, idProjeto }).ToString();

            if (string.IsNullOrEmpty(emailAdicional))
            {
                valida = false;
            }

            return valida;
        }

        #endregion
    }
}
