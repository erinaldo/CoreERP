using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartTipOperFatEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartTipOperFatEdit()
        {
            InitializeComponent();

            psLookup2.PSPart = "PSPartTipOper";
        }
    }
}
