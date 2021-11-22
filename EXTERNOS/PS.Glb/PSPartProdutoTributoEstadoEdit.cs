using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartProdutoTributoEstadoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartProdutoTributoEstadoEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartTributo";
            psLookup4.PSPart = "PSPartEstado";
        }
    }
}
