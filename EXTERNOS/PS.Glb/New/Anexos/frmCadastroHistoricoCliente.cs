using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Anexos
{
    public partial class frmCadastroHistoricoCliente : Form
    {
        string codclifor;
        public frmCadastroHistoricoCliente(string _codClifor)
        {
            InitializeComponent();

            new Class.Utilidades().getDicionario(this, tabControl1, "VCLIFOR");
            codclifor = _codClifor;
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            savar();
        }

        private bool savar()
        {
            try
            {
                string historico = meAddHistorico.Text + "\r\n" + "Usuário: " + AppLib.Context.Usuario.ToString() + "\r\n" + "Data/Hora: " + string.Format("{0:dd/MM/yyyy HH:mm:ss}", AppLib.Context.poolConnection.Get("Start").GetDateTime());
                historico = historico + "\r\n \r\n" + getHistorico(codclifor, Convert.ToInt32(AppLib.Context.Empresa));
                inserirHistórico(codclifor, Convert.ToInt32(AppLib.Context.Empresa), historico);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
          
        }
        private string getHistorico(string codClifor, int codEmpresa)
        {
            return AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT HISTORICO FROM VCLIFOR WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { codclifor, AppLib.Context.Empresa }).ToString();
        }

        private void inserirHistórico(string codClifor, int codEmpresa, string historico)
        {
            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE VCLIFOR SET HISTORICO = ? WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { historico, codClifor, codEmpresa });
            meHistorico.Text = getHistorico(codClifor, codEmpresa);
            meAddHistorico.Text = "";
        }

        private void frmCadastroHistoricoCliente_Load(object sender, EventArgs e)
        {
            meHistorico.Text =   AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT HISTORICO FROM VCLIFOR WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { codclifor, AppLib.Context.Empresa }).ToString();
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (savar() == true)
            {
                this.Dispose();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
