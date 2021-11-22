using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PS.Lib;

namespace PS.Glb
{
    public partial class PSPartEnviarXMLNFeAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Global gb = new PS.Lib.Global();

        public PSPartEnviarXMLNFeAppFrm()
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
            if (PS.Lib.PSMessageBox.ShowQuestion("Confirma o envio do XML por e-mail do(s) registro(s) selecionado(s) ?") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.VerificarAutorizacao();

                    if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                    {
                        if (psPartApp.DataGrid != null)
                        {
                            for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                            {
                                if (psPartApp.DataGrid.Rows[i].Selected)
                                {
                                    EnviarXML(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]));
                                }
                            }
                        }
                    }

                    if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
                    {
                        EnviarXML(this.psPartApp.DataField);
                    }

                    PS.Lib.PSMessageBox.ShowInfo("Operação realizada com sucesso.");
                }
                catch (Exception ex)
                {
                    PS.Lib.PSMessageBox.ShowError(ex.Message);
                    return false;
                }
            }

            return true;
        }

        private void EnviarXML(List<DataField> objArr)
        {
            try
            {
                PSPartNFEstadualData psPartNFEstadualData = new PSPartNFEstadualData();
                psPartNFEstadualData._tablename = this.psPartApp.TableName;
                psPartNFEstadualData._keys = this.psPartApp.Keys;

                PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
                PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

                // Atribuição dos valores para as variáveis
                psPartNFEstadualData.Email = EMAIL;
                psPartNFEstadualData.Cliente = CLIENTE;
                
                if (MessageBox.Show("Deseja enviar XML para o transportador?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                {
                    psPartNFEstadualData.EnviarXML(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODOPER.Valor), true);
                }
                else
                {
                    psPartNFEstadualData.EnviarXML(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODOPER.Valor), false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
