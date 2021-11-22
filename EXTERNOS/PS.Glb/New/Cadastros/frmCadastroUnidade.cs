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
    public partial class frmCadastroUnidade : Form
    {
        public frmCadastroUnidade()
        {
            InitializeComponent();
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {

        }

        private bool salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();
            try
            {
                AppLib.ORM.Jit VUNID = new AppLib.ORM.Jit(conn, "VUNID");
                VUNID.Set("CODEMPRESA", AppLib.Context.Empresa);
                VUNID.Set("CODUNID", txtCodUnid.Text);
                VUNID.Set("NOME", txtNome.Text);
                VUNID.Set("CODUNIDBASE", psLookup2.textBox1.Text);
                VUNID.Set("FATORCONVERSAO", !string.IsNullOrEmpty(txtFatorConversao.Text) ? Convert.ToDecimal(txtFatorConversao.Text) : 0);
                conn.Commit();
                return true;
            }
            catch (Exception ex)
            {
                conn.Rollback();
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
