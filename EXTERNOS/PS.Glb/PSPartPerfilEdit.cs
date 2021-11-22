using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartPerfilEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartPerfilEdit()
        {
            InitializeComponent();
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psCheckBox1.Checked = true;
        }

        private void PSPartPerfilEdit_Load(object sender, EventArgs e)
        {

        }

        private void PSPartAcessoTipOper_Load(object sender, EventArgs e)
        {

        }
    }
}
