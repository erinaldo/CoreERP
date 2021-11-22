using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartTipOperReportEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartTipOperReportEdit()
        {
            InitializeComponent();

            psLookup2.PSPart = "PSPartReport";
        }
    }
}
