using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartImprimirCopiaChequeAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        public PSPartImprimirCopiaChequeAppFrm()
        {
            InitializeComponent();
        }
        PS.Lib.Global gb = new PS.Lib.Global();
        public override Boolean Execute()
        {
            for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
            {
                imprimirCopiaCheque(psPartApp.DataGrid.SelectedRows[i].Cells["CODCHEQUE"].Value.ToString(), Convert.ToInt32(psPartApp.DataGrid.SelectedRows[i].Cells["CODEMPRESA"].Value.ToString()));
            }
            return true;
        }
        private void imprimirCopiaCheque(string codCheque, int codEmpresa)
        {
            PS.Lib.WinForms.Report.ReportDesignTool rp = new PS.Lib.WinForms.Report.ReportDesignTool(new Relatorios.XrCheque(codCheque, codEmpresa));
            rp.ShowPreviewDialog();
        }
    }
}
