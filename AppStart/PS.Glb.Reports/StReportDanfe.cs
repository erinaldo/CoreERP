using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb.Reports
{
    public class StReportDanfe : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
       public StReportDanfe()
        {
            this.ReportName = "DANFE";
            this.FormApp = new StReportDanfeApp();
            this.Parameters = new string[] {"CODEMPRESA", "CODOPER"};
        }
    }
}
