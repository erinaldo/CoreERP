using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportFinanceiro : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportFinanceiro()
        {
            this.ReportName = "Financeiro";
            this.FormApp = new StReportFinanceiroApp();
            this.Parameters = new string[] { "CODEMPRESA", "CODREPORT" };
        }

    }
}
