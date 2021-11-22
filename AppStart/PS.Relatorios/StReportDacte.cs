using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportDacte  : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportDacte()
        {
            this.ReportName = "Dacte";
            this.FormApp = new StReportDacteApp();
            this.Parameters = new string[] {"CODEMPRESA", "CODREPORT"};
        }
    }
}
