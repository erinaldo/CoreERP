using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartUsuarioEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartUsuarioEdit()
        {
            InitializeComponent();
            //new Class.Utilidades().aparecer(this, tabControl1);
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psCheckBox1.Checked = true;

            psDateBox2.Text = null;
        }

        private void psCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (psCheckBox2.Checked)
            {
                psDateBox1.Text = null;
                psDateBox1.Enabled = false;
            }
            else
            {
                psDateBox1.Enabled = true;
            }
        }
        

        private void PSPartUsuarioEdit_Load(object sender, EventArgs e)
        {
            
        }
    }
}
