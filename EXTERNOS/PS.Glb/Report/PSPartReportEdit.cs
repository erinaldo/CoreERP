using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.Report
{
    public partial class PSPartReportEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartReportEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartPart";
        }

        private void PSPartReportEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
