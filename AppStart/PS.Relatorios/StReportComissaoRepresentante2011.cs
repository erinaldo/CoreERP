using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportComissaoRepresentante2011 : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportComissaoRepresentante2011()
        {
            this.ReportName = "Comissão de Representantes - 20.11";
            this.FormApp = new StReportComissaoRepresentante2011App();
            this.Parameters = new string[] { "CODEMPRESA", "CODREPORT" };
        }
    }
}
