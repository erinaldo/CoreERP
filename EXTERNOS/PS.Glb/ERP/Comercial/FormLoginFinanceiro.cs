using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.ERP.Comercial
{
    public partial class FormLoginFinanceiro : Form
    {
        public bool confirmacao = false;
        public string codUsuario = string.Empty;
        
        public FormLoginFinanceiro()
        {
            InitializeComponent();
        }
        private bool verificaSupervisor(string codUsuario)
        {
            bool supervisor = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT SUPERVISOR FROM GUSUARIO WHERE CODUSUARIO = ?", new object[] {codUsuario}));
            return supervisor;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (verificaSupervisor(txtUsuario.Text))
            {
                confirmacao = verificaSenha(txtUsuario.Text, txtSenha.Text);
                codUsuario = txtUsuario.Text;    
            }
            else
            {
                confirmacao = false;
            }
            this.Close();
        }
        private bool verificaSenha(string usuario, string senha)
        {
            string codUsuario = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NOME FROM GUSUARIO WHERE CODUSUARIO = ? AND SENHA = ?", new object[] {usuario, new PS.Lib.Criptografia().Encoder(PS.Lib.Criptografia.OpcoesEncoder.Rijndael, senha, usuario)}).ToString();
            if (!string.IsNullOrEmpty(codUsuario))
            {
                return true;
            }
            return false;
        }
       
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }
    }
}
