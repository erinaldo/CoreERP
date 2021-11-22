using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb.Reports
{
    public class StReportDactePaisagem : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportDactePaisagem()
        {
            this.ReportName = "Dacte";
            this.FormApp = new StReportDacteApp();
            this.Parameters = new string[] { "CODEMPRESA", "CODREPORT" };
        }
    }
}
