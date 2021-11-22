using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportLancamento : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportLancamento()
        {
            this.ReportName = "Consolidado - Contas";
            this.FormApp = new StReportLancamentoApp();
            this.Parameters = new string[] { "CODEMPRESA", "CODREPORT" };
        }
    }
}
