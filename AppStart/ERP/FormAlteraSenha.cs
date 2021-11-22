using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ERP
{
    public partial class FormAlteraSenha : Form
    {
        public FormAlteraSenha()
        {
            InitializeComponent();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //Busca a senha no banco 
            string senhaAtual = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT SENHA FROM GUSUARIO WHERE CODUSUARIO = ?", new object[] {AppLib.Context.Usuario}).ToString();
            //Descriptografa a senha
            AppLib.Util.Criptografia crip = new AppLib.Util.Criptografia();
            senhaAtual = crip.Decoder(AppLib.Util.Criptografia.OpcoesEncoder.Rijndael, senhaAtual, AppLib.Context.Usuario);
            //Compara a senha
            if (senhaAtual == txtAtual.Text)
            {
                //Compara nova senha
                if (txtNova.Text == txtConfirmacaoNova.Text)
                {
                    //Altera a senha
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GUSUARIO SET SENHA = ? WHERE CODUSUARIO = ? ", new object[] {crip.Encoder(AppLib.Util.Criptografia.OpcoesEncoder.Rijndael, txtNova.Text, AppLib.Context.Usuario), AppLib.Context.Usuario });
                    MessageBox.Show("Senha alterada com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("As senhas não conferem. Favor digitar novamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("A senha atual não confere. Favor verificar.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
