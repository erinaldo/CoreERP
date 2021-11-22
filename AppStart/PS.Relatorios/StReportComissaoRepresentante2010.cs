using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportComissaoRepresentante2010 : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportComissaoRepresentante2010()
        {
            this.ReportName = "Comissão de Representantes - 20.10";
            this.FormApp = new StReportComissaoRepresentante2010App();
            this.Parameters = new string[] { "CODEMPRESA", "CODREPORT" };
        }
    }
}
