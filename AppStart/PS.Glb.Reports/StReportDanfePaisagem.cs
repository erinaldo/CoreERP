using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb.Reports
{
    public class StReportDanfePaisagem : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportDanfePaisagem()
        {
            this.ReportName = "DANFE PAISAGEM";
            this.FormApp = new StReportDanfePaisamgemApp();
            this.Parameters = new string[] {"CODEMPRESA", "CODOPER"};
        }
    }
}
