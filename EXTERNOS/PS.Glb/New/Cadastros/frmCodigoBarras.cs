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
    public partial class frmCodigoBarras : Form
    {
        string codProduto = string.Empty;
        public frmCodigoBarras(string _codProduto)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VPRODUTOCODIGO");
            codProduto = _codProduto;
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigoBarras.Text))
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VPRODUTOCODIGO (ATIVO, CODEMPRESA, CODIGOBARRAS, CODPRODUTO) VALUES (?, ?, ?, ?)", new object[] { 1, AppLib.Context.Empresa, txtCodigoBarras.Text, codProduto }); this.Dispose();
                GC.Collect();
            }
            else
            {
                MessageBox.Show("Favor preencher o campo de código de barras corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
