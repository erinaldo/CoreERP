using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartConvenioEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartConvenioEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartConta";
        }

        private void PSPartConvenioEdit_Load(object sender, EventArgs e)
        {

        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();
            psCheckBox1.Checked = true;
            psCheckBox2.Checked = true;
        }

    }
}
