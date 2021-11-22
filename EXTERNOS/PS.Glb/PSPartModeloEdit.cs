using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartModeloEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartModeloEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartFabricante";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psTextoBox1.Text = "0";
            psTextoBox1.Edita = false;
        }
    }
}
