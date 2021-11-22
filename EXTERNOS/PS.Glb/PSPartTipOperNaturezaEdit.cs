using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartTipOperNaturezaEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartTipOperNaturezaEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartNatureza";
        }
    }
}
