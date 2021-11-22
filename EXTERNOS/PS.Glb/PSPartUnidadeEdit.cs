using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartUnidadeEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartUnidadeEdit()
        {
            InitializeComponent();

            psLookup2.PSPart = "PSPartUnidade";
        }
    }
}
