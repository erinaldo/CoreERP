using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace PS.Glb.Report
{
    public partial class FrmBaseReportEdit : DevExpress.XtraEditors.XtraForm
    {
        public DataSet Dados;

        public FrmBaseReportEdit()
        {
            InitializeComponent();
        }

        private void FrmBaseReportEdit_Load(object sender, EventArgs e)
        {
            // Novo Relatório
            xrDesignPanel1.OpenReport(new XtraReport());
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmBaseReportDataSource f = new FrmBaseReportDataSource();
            f.ShowDialog();
            this.Dados = f.Dados;

            xrDesignPanel1.Report.DataSource = Dados;
        }
    }
}
