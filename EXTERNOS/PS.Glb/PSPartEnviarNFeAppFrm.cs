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
    public partial class PSPartEnviarNFeAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        PS.Lib.Valida vl = new PS.Lib.Valida();

        public PSPartEnviarNFeAppFrm()
        {
            InitializeComponent();
        }

        public override Boolean Execute()
        {
            int env = 0, nEnv = 0;
            if (PS.Lib.PSMessageBox.ShowQuestion("Confirma o envio dos registro(s) selecionado(s) ?") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                    {
                        if (psPartApp.DataGrid != null)
                        {
                            for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
                            {
                                //Verifica o Status da NFE
                                string status = psPartApp.DataGrid.Rows[i].DataGridView["CODSTATUS", psPartApp.DataGrid.SelectedRows[i].Index].Value.ToString();
                                if (!status.Equals("A") && !status.Equals("C") && !status.Equals("U"))
                                {
                                    EnviarNFe(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[i]));
                                    env = env + 1;
                                }
                                else
                                {
                                    nEnv = nEnv + 1;
                                }
                            }
                        }
                    }

                    if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
                    {
                        EnviarNFe(this.psPartApp.DataField);
                    }
                    MessageBox.Show("Operação Realizada com Sucesso. \nQtde de NFe enviada: " + env + "\nQtde de NFe não enviada: " + nEnv + "\n", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    PS.Lib.PSMessageBox.ShowError(ex.Message);
                    return false;
                }
            }

            return true;
        }

        public void EnviarNFe(List<DataField> objArr)
        {

            PSPartNFEstadualData psPartNFEstadualData = new PSPartNFEstadualData();
            psPartNFEstadualData._tablename = this.psPartApp.TableName;
            psPartNFEstadualData._keys = this.psPartApp.Keys;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfCODSTATUS = gb.RetornaDataFieldByCampo(objArr, "CODSTATUS");

            if (!dfCODSTATUS.Equals("A") && !dfCODSTATUS.Equals("C"))
            {
                psPartNFEstadualData.EnviarNFe(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODOPER.Valor));
            }
        }

        public void EnvNFe(List<DataField> objArr)
        {
            PSPartNFEstadualData nfe = new PSPartNFEstadualData();
            nfe._tablename = "GNFESTADUAL";
            nfe._keys = new string[] { "CODEMPRESA", "CODOPER" };
            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            nfe.EnviarNFe(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODOPER.Valor));
        }

    }
}
