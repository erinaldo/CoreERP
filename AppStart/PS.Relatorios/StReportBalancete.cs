using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportBalancete : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportBalancete()
        {
            this.ReportName = "Balancete - Contas";
            this.FormApp = new StReportBalanceteApp();
            this.Parameters = new string[] { "CODEMPRESA", "CODREPORT" };
        }
    }
}
