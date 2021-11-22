using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraReports.UI;
using PS.Lib;

namespace PS.Glb.Report
{
    public class PSPartReportApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartReportApp()
        {
            this.AppName = "Estrutura do Relatório";
            this.FormApp = null;
            this.Select = SelectType.OnlyOneRow;

            this.SecurityID = "PSPartReportApp";
            this.ModuleID = "PG";
        }

        public override void Execute()
        {
            /*
            XtraReport report = new XtraReport();
            Report.ReportDesignTool rp = new Report.ReportDesignTool(report);
            rp.ShowDesigner();
            */

            FrmBaseReportEdit f = new FrmBaseReportEdit();
            f.ShowDialog();
        }
    }
}
