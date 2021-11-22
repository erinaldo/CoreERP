using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartProdutoTributoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartProdutoTributoEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartTributo";
        }
    }
}
