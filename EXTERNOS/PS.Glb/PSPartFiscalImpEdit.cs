using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartFiscalImpEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartFiscalImpEdit()
        {
            InitializeComponent();
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psCheckBox1.Checked = true;
        }
    }
}
