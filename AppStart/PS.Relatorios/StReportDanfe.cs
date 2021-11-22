using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportDanfe : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
       public StReportDanfe()
        {
            this.ReportName = "DANFE RETRATO";
            this.FormApp = new StReportDanfeApp();
            this.Parameters = new string[] {"CODEMPRESA", "CODOPER"};
        }
    }
}
