using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
   public  class StReportComissaoInterna : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
       public StReportComissaoInterna()
        {
            this.ReportName = "Comissão de Representantes - 20.11";
            this.FormApp = new StReportComissaoInternaApp();
            this.Parameters = new string[] { "CODEMPRESA", "CODREPORT" };
        }
    }
}
