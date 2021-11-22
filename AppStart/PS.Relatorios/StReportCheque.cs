using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportCheque : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportCheque()
        {
            this.ReportName = "Cópia de Cheque";
            this.FormApp = new StReportChequeApp();
            this.Parameters = new string[] { "CODEMPRESA", "CODCHEQUE" };
        }
    }
}
