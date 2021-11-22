using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Relatorios
{
    public partial class StReportPedidoVendaApp :  PS.Lib.WinForms.StaticReport.FrmBaseAppStaticReport
    {
        public StReportPedidoVendaApp()
        {
            InitializeComponent();
        }
         public override void Execute()
        {
            base.Execute();

            this.Report = new XrPedidoVenda(this.Parameters);

            PS.Lib.WinForms.Report.ReportDesignTool rp = new PS.Lib.WinForms.Report.ReportDesignTool(Report);
            rp.ShowPreviewDialog();
        }
    }
}
