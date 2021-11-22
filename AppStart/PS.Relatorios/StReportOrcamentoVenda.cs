using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportOrcamentoVenda : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportOrcamentoVenda()
        {
            this.ReportName = "Orçamento de Venda";
            this.FormApp = new StReportOrcamentoVendaApp();
            this.Parameters = new string[] { "CODEMPRESA", "CODOPER" };
        }
    }
}
