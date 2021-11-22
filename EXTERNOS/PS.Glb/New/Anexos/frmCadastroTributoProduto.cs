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
    public partial class frmCadastroTributoProduto : Form
    {
        string codProduto;
        public string codTributo = string.Empty;
        public bool edita = false;

        public frmCadastroTributoProduto(string _codProduto)
        {
            InitializeComponent();
            codProduto = _codProduto;
            new Class.Utilidades().getDicionario(this, tabControl1, "VPRODUTOTRIBUTO");
            psLookup1.PSPart = "PSPartTributo";
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "VPRODUTOTRIBUTO");
            conn.BeginTransaction();
            try
            {
                v.Set("CODEMPRESA", AppLib.Context.Empresa);
                v.Set("CODPRODUTO", codProduto);
                v.Set("CODTRIBUTO", psLookup1.textBox1.Text);
                v.Set("ALIQUOTA", Convert.ToDecimal(txtAliquota.Text));
                v.Set("CODCST", txtCodCst.Text);
                v.Save();
                conn.Commit();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Rollback();
                return false;
            }
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            salvar();
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (salvar() == true)
            {
                this.Dispose();
            }
        }

        private void carregaCampos()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM VPRODUTOTRIBUTO WHERE CODPRODUTO = ? AND CODTRIBUTO = ? AND CODEMPRESA = ?", new object[] { codProduto, codTributo, AppLib.Context.Empresa });
            
            if (dt.Rows.Count > 0)
            {
                psLookup1.textBox1.Text = dt.Rows[0]["CODTRIBUTO"].ToString();
                psLookup1.LoadLookup();
                txtAliquota.Text = dt.Rows[0]["ALIQUOTA"].ToString();
                txtCodCst.Text = dt.Rows[0]["CODCST"].ToString();
            }
        }

        private void frmCadastroTributoProduto_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
            }
        }
    }
}
