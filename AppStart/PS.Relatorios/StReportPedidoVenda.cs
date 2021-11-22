using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportPedidoVenda : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportPedidoVenda()
        {
            this.ReportName = "Pedido de Venda";
            this.FormApp = new StReportPedidoVendaApp();
            this.Parameters = new string[] { "CODEMPRESA", "CODOPER" };
        }
    }
}
