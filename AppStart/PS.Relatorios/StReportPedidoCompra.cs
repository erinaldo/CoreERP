using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportPedidoCompra : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportPedidoCompra()
        {
            this.ReportName = "Pedido de Compra";
            this.FormApp = new StReportPedidoCompraApp();
            this.Parameters = new string[] { "CODEMPRESA", "CODOPER" };
        }
    }
}
