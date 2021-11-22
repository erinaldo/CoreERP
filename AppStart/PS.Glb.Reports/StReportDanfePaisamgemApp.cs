﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.Reports
{
    public partial class StReportDanfePaisamgemApp : PS.Lib.WinForms.StaticReport.FrmBaseAppStaticReport
    {
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        public StReportDanfePaisamgemApp()
        {
            InitializeComponent();
        }

        public override void Execute()
        {
            base.Execute();
            
            this.Report = new XrDanfePaisagem(this.Parameters);

            PS.Lib.WinForms.Report.ReportDesignTool rp = new PS.Lib.WinForms.Report.ReportDesignTool(Report);
            rp.ShowPreviewDialog();
        }
    }
}
