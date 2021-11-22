using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITGProducao.Formularios
{
    public partial class FrmEstruturaDescricaoAuxiliar : Form
    {
        public string descAux = string.Empty;
        public FrmEstruturaDescricaoAuxiliar(ref string descAux)
        {
            InitializeComponent();

            this.descAux = descAux;
            txtDescAux.Text = descAux;
            txtDescAux.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescAux.Text))
            {
                descAux = "";
            }else
            {
                descAux = txtDescAux.Text;
            }
            this.Dispose();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
