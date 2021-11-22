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
    public partial class PSPartOperCanFatAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        PS.Lib.Valida vl = new PS.Lib.Valida();

        public PSPartOperCanFatAppFrm()
        {
            InitializeComponent();
        }

        public override Boolean Execute()
        {
            if (PS.Lib.PSMessageBox.ShowQuestion("Confirma o cancelamento da(s) operação(ões) selecionada(s) ?") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                    {
                        if (psPartApp.DataGrid != null)
                        {
                            for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
                            {
                                CancelaFaturamento(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[i]));
                                InseriMotivo(Convert.ToInt32(psPartApp.DataGrid.SelectedRows[i].Cells["CODOPER"].Value));
                            }
                        }
                    }

                    if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
                    {
                        CancelaFaturamento(this.psPartApp.DataField);
                    }

                    this.psPartApp.Refresh = true;

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
        private void InseriMotivo(int codOper)
        {
            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GMOTIVOCANCELAMENTO (CODOPER, CODEMPRESA, CODUSUARIO, MOTIVO) VALUES (?, ?, ?, ?)", new object[] { codOper, AppLib.Context.Empresa, AppLib.Context.Usuario, txtMotivo.Text });
        }
        private void CancelaFaturamento(List<DataField> objArr)
        {
            PSPartOperacaoData psPartOperacaoData = new PSPartOperacaoData();
            psPartOperacaoData._tablename = this.psPartApp.TableName;
            psPartOperacaoData._keys = this.psPartApp.Keys;

            psPartOperacaoData.CancelaOperacao(objArr);
        }
    }
}
