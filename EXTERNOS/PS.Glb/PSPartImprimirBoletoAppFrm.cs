using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PS.Lib;

namespace PS.Glb
{
    public partial class PSPartImprimirBoletoAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        List<DataField> obj = new List<DataField>();
        public PSPartImprimirBoletoAppFrm()
        {
            InitializeComponent();
        }
        public override Boolean Execute()
        {
            try
            {
                if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                {
                    if (psPartApp.DataGrid != null)
                    {
                        string CODLANCA = "( ";
                        for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                        {
                            if (psPartApp.DataGrid.Rows[i].Selected)
                            {

                                obj = gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]);
                                CODLANCA += obj[1].Valor.ToString();
                                CODLANCA += ", ";

                                
                            }
                           
                        }
                        if (CODLANCA.Length > 2)
                        {
                            CODLANCA = CODLANCA.Substring(0, CODLANCA.Length - 2);
                            CODLANCA += ")";
                        }
                        //
                        Relatorios.XrBoletoBancario rel = new Relatorios.XrBoletoBancario(AppLib.Context.Empresa, CODLANCA);
                        new DevExpress.XtraReports.UI.ReportPrintTool(rel).ShowPreviewDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
                return false;
            }

            return true;
        }
    }
}
