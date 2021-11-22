using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb.Report
{
    public class PSPartReportExecApp : PS.Lib.WinForms.PSPartApp
    {
        private PS.Lib.WinForms.StaticReport.PSPartStaticReport _staticReport = new PS.Lib.WinForms.StaticReport.PSPartStaticReport();
        private PS.Lib.Global gb = new Global();

        public PSPartReportExecApp()
        {
            this.AppName = "Visualizar Relatório";
            this.FormApp = null;
            this.Select = SelectType.OnlyOneRow;

            this.SecurityID = "PSPartReportExecApp";
            this.ModuleID = "PG";
        }

        public Boolean PermissaoVisualizar(int CODEMPRESA, String CODREPORT, String CODPERFIL)
        {
            String consulta = "SELECT * FROM GREPORTPERFIL WHERE CODEMPRESA = ? AND CODREPORT = ? AND CODPERFIL = ?";
            return AppLib.Context.poolConnection.Get("Start").ExecHasRows(consulta, new Object[] { CODEMPRESA, CODREPORT, CODPERFIL });
        }

        public override void Execute()
        {
            PS.Lib.WinForms.Report.ReportUtil rpu = new PS.Lib.WinForms.Report.ReportUtil();

            if (this.Access == PS.Lib.AppAccess.View)
            {
                if (DataGrid.Rows != null)
                {
                    for (int i = 0; i < DataGrid.Rows.Count; i++)
                    {
                        if (DataGrid.Rows[i].Selected)
                        {
                            PS.Lib.DataField dfCODEMPRESA = new PS.Lib.DataField();
                            dfCODEMPRESA = gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(DataGrid.Rows[i]), "CODEMPRESA");

                            PS.Lib.DataField dfCODREPORT = new PS.Lib.DataField();
                            dfCODREPORT = gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(DataGrid.Rows[i]), "CODREPORT");

                            if ( this.PermissaoVisualizar(int.Parse(dfCODEMPRESA.Valor.ToString()), dfCODREPORT.Valor.ToString(), AppLib.Context.Perfil) )
                            {
                                _staticReport = rpu.LoadStaticReportList(int.Parse(dfCODEMPRESA.Valor.ToString()), dfCODREPORT.Valor.ToString());
                                _staticReport.DataField = null;
                                _staticReport.DataGrid = this.DataGrid;
                                _staticReport.Access = AppAccess.View;
                                _staticReport.Execute();
                            }
                            else
                            {
                                AppLib.Windows.FormMessageDefault.ShowError("Seu perfil de usuário não possui permissão para visualizar este relatório.");
                            }
                        }
                    }
                }
            }

            if (this.Access == PS.Lib.AppAccess.Edit)
            {
                PS.Lib.DataField dfCODEMPRESA = new PS.Lib.DataField();
                dfCODEMPRESA = gb.RetornaDataFieldByCampo(this.DataField, "CODEMPRESA");

                PS.Lib.DataField dfCODREPORT = new PS.Lib.DataField();
                dfCODREPORT = gb.RetornaDataFieldByCampo(this.DataField, "CODREPORT");

                _staticReport = rpu.LoadStaticReportList(int.Parse(dfCODEMPRESA.Valor.ToString()), dfCODREPORT.Valor.ToString());
                _staticReport.DataField = this.DataField;
                _staticReport.DataGrid = null;
                _staticReport.Access = AppAccess.Edit;
                _staticReport.Execute();
            }
        }
    }
}