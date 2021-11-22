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
    public partial class PSPartExportarXMLNFeAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Global gb = new PS.Lib.Global();

        public PSPartExportarXMLNFeAppFrm()
        {
            InitializeComponent();
        }

        private void VerificarAutorizacao()
        {
            if (this.psPartApp.Access == PS.Lib.AppAccess.View)
            {
                if (psPartApp.DataGrid != null)
                {
                    for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
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
            int env = 0, nEnv = 0;
            if (PS.Lib.PSMessageBox.ShowQuestion("Confirma a exportação do XML do(s) registro(s) selecionado(s) ?") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    if (psPartApp.DataGrid != null)
                    {
                        System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
                            {
                                string status = this.psPartApp.DataGrid.Rows[i].Cells["CODSTATUS"].Value.ToString();
                                if (status.Equals("A") || status.Equals("C"))
                                {

                                    if (ExportarXML(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[i]), fbd.SelectedPath))
                                    {
                                        env = env + 1;    
                                    }
                                    else
                                    {
                                        nEnv = nEnv + 1;
                                    }
                                }
                                else
                                {
                                    nEnv = nEnv + 1;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    PS.Lib.PSMessageBox.ShowError(ex.Message);
                    return false;
                }
                MessageBox.Show("Operação Realizada com Sucesso. \nQtde de NFe Exportadas: " + env + "\nQtde de NFe não Exportada: " + nEnv + "\n", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return true;
        }

        private bool ExportarXML(List<DataField> objArr, string Path)
        {
            PSPartNFEstadualData psPartNFEstadualData = new PSPartNFEstadualData();
            psPartNFEstadualData._tablename = this.psPartApp.TableName;
            psPartNFEstadualData._keys = this.psPartApp.Keys;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfCHAVEACESSO = gb.RetornaDataFieldByCampo(objArr, "CHAVEACESSO");

            string sXML = psPartNFEstadualData.ExportarXML(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODOPER.Valor));

            if (string.IsNullOrEmpty(sXML))
            {
                return false;
            }
            else
            {
                int indexi = sXML.IndexOf('<', 0);
                int indexf = sXML.IndexOf('>', 0);

                string substituir = sXML.Substring(indexi, indexf + 1);

                if (substituir.Contains("xml version"))
                    sXML = sXML.Replace(substituir, "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                else
                    sXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + sXML;


                string sPath = Path + "\\" + "NFe" + dfCHAVEACESSO.Valor.ToString() + ".XML";
                System.IO.File.WriteAllText(sPath, sXML);
                return true;
            }
        }
    }
}
