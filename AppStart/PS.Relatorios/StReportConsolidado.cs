using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportConsolidado : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportConsolidado()
        {
            this.ReportName = "Consolidado - Contas";
            this.FormApp = new StReportConsolidadoApp();
            this.Parameters = new string[] { "CODEMPRESA", "CODREPORT" };
        }
    }
}
