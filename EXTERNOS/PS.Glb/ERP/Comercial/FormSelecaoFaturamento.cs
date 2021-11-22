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
    public partial class FormSelecaoFaturamento : Form
    {
        public bool cancelado = false;

        public FormSelecaoFaturamento()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cancelado = true;
            this.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            cancelado = false;
            this.Close();
        }
    }
}
