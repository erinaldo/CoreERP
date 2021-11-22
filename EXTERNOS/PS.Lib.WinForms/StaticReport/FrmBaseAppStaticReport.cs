using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib.WinForms.StaticReport
{
    public partial class FrmBaseAppStaticReport : DevExpress.XtraEditors.XtraForm
    {
        public PSPartStaticReport psPartAppStaticReport { get; set; }
        public string ReportName { get; set; }
        public List<DataField> Parameters { get; set; }
        
        public DevExpress.XtraReports.UI.XtraReport Report;

        public FrmBaseAppStaticReport()
        {
            InitializeComponent();
        }

        private void FrmBaseApp_KeyDown(object sender, KeyEventArgs e)
        {
            Keys flag = e.KeyCode;

            if (flag.Equals(Keys.Enter))
            {
                SelectNextControl(ActiveControl, !e.Shift, true, true, true);
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBaseApp_Load(object sender, EventArgs e)
        {
            this.Text = "Emissão de Relatório";

            TabPage tab = new TabPage();

            tab = tabControl1.SelectedTab;

            tab.Text = this.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Execute();
        }

        public virtual void Execute()
        {
            /*
            if (this.psPartAppStaticReport.Access == PS.Lib.AppAccess.View)
            {
                PS.Lib.PSMessageBox.ShowInfo("Visão");
                return;
            }

            if (this.psPartAppStaticReport.Access == PS.Lib.AppAccess.Edit)
            {
                PS.Lib.PSMessageBox.ShowInfo("Edição");
                return;
            }
            */
        }
    }
}
