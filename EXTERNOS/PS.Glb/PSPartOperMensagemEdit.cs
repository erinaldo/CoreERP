using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartOperMensagemEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartOperMensagemEdit()
        {
            InitializeComponent();

            psLookup19.PSPart = "PSPartFormula";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psCheckBox1.Checked = true;
        }
    }
}
