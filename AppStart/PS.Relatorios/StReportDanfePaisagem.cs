using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportDanfePaisagem : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportDanfePaisagem()
        {
            this.ReportName = "DANFE PAISAGEM";
            this.FormApp = new StReportDanfePaisagemApp();
            this.Parameters = new string[] {"CODEMPRESA", "CODOPER"};
        }
    }
}
