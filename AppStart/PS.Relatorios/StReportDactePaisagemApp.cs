using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Relatorios
{
    public partial class StReportDactePaisagemApp  : PS.Lib.WinForms.StaticReport.FrmBaseAppStaticReport
    {
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public StReportDactePaisagemApp()
        {
            InitializeComponent();
        }
        public override void Execute()
        {
            base.Execute();
            if (this.Parameters.Count > 2)
            {
                this.Parameters.RemoveRange(2, 1);

            }
            List<PS.Lib.DataField> caminho = this.Parameters;
            caminho.Add(new PS.Lib.DataField("CAMINHO", psTxtCaminho.Text));



            this.Report = new XrDacte(this.Parameters);

            PS.Lib.WinForms.Report.ReportDesignTool rp = new PS.Lib.WinForms.Report.ReportDesignTool(Report);
            rp.ShowPreviewDialog();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = @"C:\Temp\Dacte";
            open.Title = "Selecione o XML";
            open.DefaultExt = "xml";
            open.Filter = "XML files (*.xml) |*.xml";
            open.Multiselect = false;
            if (open.ShowDialog() == DialogResult.OK)
            {
                psTxtCaminho.Text = open.FileName;
            }
        }
    }
}
