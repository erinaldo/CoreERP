using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartOperItemRateioCCEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartOperItemRateioCCEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartCentroCusto";

            psMoedaBox1.Edita = false;
        }
    }
}
