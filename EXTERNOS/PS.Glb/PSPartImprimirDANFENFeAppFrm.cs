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
    public partial class PSPartImprimirDANFENFeAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Global gb = new PS.Lib.Global();

        public PSPartImprimirDANFENFeAppFrm()
        {
            InitializeComponent();
        }

        private void VerificarAutorizacao()
        {
            if (this.psPartApp.Access == PS.Lib.AppAccess.View)
            {
                if (psPartApp.DataGrid != null)
                {
                    for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                    {
                        if (psPartApp.DataGrid.Rows[i].Selected)
                        {
                            if (this.psPartApp.DataGrid.Rows[i].Cells["CODSTATUS"].Value.ToString() != "A")
                            {
                                throw new Exception("A exportação do XML é permitida apenas para Notas Fiscais autorizadas.");
                            }
                        }
                    }
                }
            }

            if (this.psPartApp.Access == AppAccess.Edit)
            {
                PS.Lib.DataField dataField = gb.RetornaDataFieldByCampo(this.psPartApp.DataField, "CODSTATUS");

                if (dataField.Valor.ToString() != "A")
                {
                    throw new Exception("A exportação do XML é permitida apenas para Notas Fiscais autorizadas.");
                }
            }
        }

        public override Boolean Execute()
        {
            try
            {
                //this.VerificarAutorizacao();

                if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                {
                    if (psPartApp.DataGrid != null)
                    {
                        for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                        {
                            if (psPartApp.DataGrid.Rows[i].Selected)
                            {
                                ImprimirDANFE(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]));
                            }
                        }
                    }
                }
                if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
                {
                    ImprimirDANFE(this.psPartApp.DataField);
                }
                return true;
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
                return false;
            }
        }


        private void ImprimirDANFE(List<DataField> objArr)
        {
            PSPartNFEstadualData psPartNFEstadualData = new PSPartNFEstadualData();
            psPartNFEstadualData._tablename = this.psPartApp.TableName;
            psPartNFEstadualData._keys = this.psPartApp.Keys;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfPROTOCOLO = gb.RetornaDataFieldByCampo(objArr, "PROTOCOLO");
            PS.Lib.DataField dfFORMATOIMPRESSAO = gb.RetornaDataFieldByCampo(objArr, "FORMATOIMPRESSAO");
            List<DataField> Param = new List<DataField>();
            Param.Add(dfCODEMPRESA);
            Param.Add(dfCODOPER);
            Param.Add(dfPROTOCOLO);

            string nomeRelatorio = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CLASSNAME FROM GTIPOPERREPORT 
INNER JOIN GOPER ON GTIPOPERREPORT.CODTIPOPER = GOPER.CODTIPOPER
INNER JOIN GREPORT ON GTIPOPERREPORT.CODREPORT = GREPORT.CODREPORT
WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ? ", new object[] { dfCODOPER.Valor, dfCODEMPRESA.Valor }).ToString();
            if (!string.IsNullOrEmpty(nomeRelatorio))
            {
                switch (nomeRelatorio)
                {
                    case "StReportDanfePaisagem":
                        PS.Lib.WinForms.Report.ReportDesignTool rp = new PS.Lib.WinForms.Report.ReportDesignTool(new Relatorios.XrDanfePaisagem(Param));
                        rp.ShowPreviewDialog();
                        break;
                    case "StReportDanfe":
                        PS.Lib.WinForms.Report.ReportDesignTool rr = new PS.Lib.WinForms.Report.ReportDesignTool(new Relatorios.XrDanfe(Param));
                        rr.ShowPreviewDialog();
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
