using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportFaturamento : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportFaturamento()
        {
            this.ReportName = "Faturamento";
            this.FormApp = new StReportFaturamentoApp();
            this.Parameters = new string[] { "CODEMPRESA", "CODREPORT" };
        }

    }
}
