using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartTipOperSerieEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartTipOperSerieEdit()
        {
            InitializeComponent();

            psLookup2.PSPart = "PSPartSerie";
            psLookup13.PSPart = "PSPartFilial";
        }
    }
}
