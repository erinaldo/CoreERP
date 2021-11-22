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
    public partial class PSPartConsultarAutorizacaoNFeAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        PS.Lib.Valida vl = new PS.Lib.Valida();

        public PSPartConsultarAutorizacaoNFeAppFrm()
        {
            InitializeComponent();
        }

        public override Boolean Execute()
        {
            if (PS.Lib.PSMessageBox.ShowQuestion("Confirma a consulta da situação dos registro(s) selecionado(s) ?") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                    {
                        if (psPartApp.DataGrid != null)
                        {
                            for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                            {
                                if (psPartApp.DataGrid.Rows[i].Selected)
                                {
                                    ConsultaSituacaoNFe(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]));
                                }
                            }
                        }
                    }

                    if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
                    {
                        ConsultaSituacaoNFe(this.psPartApp.DataField);
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

        private void ConsultaSituacaoNFe(List<DataField> objArr)
        {
            PSPartNFEstadualData psPartNFEstadualData = new PSPartNFEstadualData();
            psPartNFEstadualData._tablename = this.psPartApp.TableName;
            psPartNFEstadualData._keys = this.psPartApp.Keys;

            psPartNFEstadualData.ConsultaSituacaoNFe(objArr);
        }
    }
}
