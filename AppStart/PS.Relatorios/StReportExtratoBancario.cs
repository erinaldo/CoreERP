using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportExtratoBancario : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportExtratoBancario()
        {
            this.ReportName = "Extrato Bancário";
            this.FormApp = new StReportExtratoBancarioApp();
            this.Parameters = new string[] { "CODEMPRESA", "CODREPORT" };
        }
    }
}
