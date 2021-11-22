using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportOrcamentoCompra : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportOrcamentoCompra()
        {
            this.ReportName = "Orçamento de Compra";
            this.FormApp = new StReportOrcamentoCompraApp();
            this.Parameters = new string[] { "CODEMPRESA", "CODOPER" };
        }
    }

}
