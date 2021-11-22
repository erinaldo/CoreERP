using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.Reports
{
    public partial class StReportDanfeApp : PS.Lib.WinForms.StaticReport.FrmBaseAppStaticReport
    {
        public StReportDanfeApp()
        {
            InitializeComponent();
        }

        public override void Execute()
        {
            base.Execute();
            
            //this.Report = new XrDanfe(this.Parameters);

            //PS.Lib.WinForms.Report.ReportDesignTool rp = new PS.Lib.WinForms.Report.ReportDesignTool(Report);
            //rp.ShowPreviewDialog();
        }
    }
}
