using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartProdutoComEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartProdutoComEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartProduto";
        }
    }
}
