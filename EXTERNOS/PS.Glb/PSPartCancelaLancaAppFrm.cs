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
    public partial class PSPartCancelaLancaAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        PS.Lib.Valida vl = new PS.Lib.Valida();

        private int ContItensSelecionados = 0;

        public PSPartCancelaLancaAppFrm()
        {
            InitializeComponent();
        }

        private void LimpaFormulario()
        {
            psTextoBox1.Text = string.Empty;
            QuantosItensSelecionados();
        }

        private void VerificarPagarReceber()
        {
            string ContItens = null;

            if (this.psPartApp.Access == PS.Lib.AppAccess.View)
            {
                if (psPartApp.DataGrid != null)
                {
                    for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                    {
                        if (psPartApp.DataGrid.Rows[i].Selected)
                        {
                            if (ContItens == null)
                            {
                                ContItens = this.psPartApp.DataGrid.Rows[i].Cells["TIPOPAGREC"].Value.ToString();
                            }
                            else
                            {
                                if (this.psPartApp.DataGrid.Rows[i].Cells["TIPOPAGREC"].Value.ToString() != ContItens)
                                {
                                    throw new Exception("Cancelamento de lançamento(s) de Contas a Pagar e Contas a Receber devem ser realizada separadamente.");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void QuantosItensSelecionados()
        {
            ContItensSelecionados = 0;

            if (this.psPartApp.Access == PS.Lib.AppAccess.View)
            {
                if (psPartApp.DataGrid != null)
                {
                    for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                    {
                        if (psPartApp.DataGrid.Rows[i].Selected)
                        {
                            ContItensSelecionados = ContItensSelecionados + 1;
                        }
                    }
                }
            }

            if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
            {
                ContItensSelecionados = 1;
            }
        }

        private void PSPartCancelaLancaAppFrm_Load(object sender, EventArgs e)
        {
            LimpaFormulario();
        }

        public override Boolean Execute()
        {
            if (PS.Lib.PSMessageBox.ShowQuestion("Confirma o cancelamento do(s) lançamento(s) selecionado(s) ?") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    VerificarPagarReceber();

                    if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                    {
                        if (psPartApp.DataGrid != null)
                        {
                            for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                            {
                                if (psPartApp.DataGrid.Rows[i].Selected)
                                {
                                    CancelaLancamento(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]));
                                }
                            }
                        }
                    }

                    if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
                    {
                        CancelaLancamento(this.psPartApp.DataField);
                    }

                    // PS.Lib.PSMessageBox.ShowInfo("Operação realizada com sucesso.");
                }
                catch (Exception ex)
                {
                    PS.Lib.PSMessageBox.ShowError(ex.Message);
                    return false;
                }
            }

            return true;
        }

        private void CancelaLancamento(List<DataField> objArr)
        {
            PSPartLancaData psPartLancaData = new PSPartLancaData();
            psPartLancaData._tablename = this.psPartApp.TableName;
            psPartLancaData._keys = this.psPartApp.Keys;

            psPartLancaData.CancelaLancamento(objArr);
        }
    }
}
