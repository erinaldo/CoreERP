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
    public partial class PSPartCartaCorrecaoNFeAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Global gb = new PS.Lib.Global();

        public PSPartCartaCorrecaoNFeAppFrm()
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
                                throw new Exception("O cancelamento da Nota Fiscal é permitida apenas para Notas Fiscais autorizadas.");
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
                    throw new Exception("O cancelamento da Nota Fiscal é permitida apenas para Notas Fiscais autorizadas.");
                }
            }
        }

        public override Boolean Execute()
        {
            if (PS.Lib.PSMessageBox.ShowQuestion("Confirma o cancelamento do(s) registro(s) selecionado(s) ?") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.VerificarAutorizacao();

                    if (string.IsNullOrEmpty(psMemoBox1.Text))
                    {
                        throw new Exception("Informe o motivo do cancelamento da nota fiscal.");
                    }

                    if (psMemoBox1.Text.Length < 15)
                    {
                        throw new Exception("O motivo do cancelamento deve ter no mínimo 15 caracteres.");
                    }

                    if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                    {
                        if (psPartApp.DataGrid != null)
                        {
                            for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                            {
                                if (psPartApp.DataGrid.Rows[i].Selected)
                                {
                                    CartaCorrecao(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]));
                                }
                            }
                        }
                    }

                    if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
                    {
                        CartaCorrecao(this.psPartApp.DataField);
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

        private void CartaCorrecao(List<DataField> objArr)
        {
            try
            {
                PSPartNFEstadualData psPartNFEstadualData = new PSPartNFEstadualData();
                psPartNFEstadualData._tablename = this.psPartApp.TableName;
                psPartNFEstadualData._keys = this.psPartApp.Keys;

                PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
                PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

                psPartNFEstadualData.CancelarNFe(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODOPER.Valor), psMemoBox1.Text);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
