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
    public partial class PSPartCompensarExtratoAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        PS.Lib.Valida vl = new PS.Lib.Valida();

        private int ItensSelecionados;

        public PSPartCompensarExtratoAppFrm()
        {
            InitializeComponent();
        }

        private void QuantosItensSelecionados()
        {
            ItensSelecionados = 0;

            if (this.psPartApp.Access == PS.Lib.AppAccess.View)
            {
                if (psPartApp.DataGrid != null)
                {
                    for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                    {
                        if (psPartApp.DataGrid.Rows[i].Selected)
                        {
                            ItensSelecionados = ItensSelecionados + 1;
                        }
                    }
                }
            }

            if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
            {
                ItensSelecionados = 1;
            }
        }

        public override Boolean Execute()
        {
            this.QuantosItensSelecionados();

            if (PS.Lib.PSMessageBox.ShowQuestion("Confirma a compensação do(s) extrato(s) selecionado(s) ?") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    if (psDateBox1.Text == null)
                    {
                        throw new Exception("Informe a data de compensação.");
                    }

                    Support.FinExtCompPar finExtCompPar = new Support.FinExtCompPar();
                    finExtCompPar.CodEmpresa = PS.Lib.Contexto.Session.Empresa.CodEmpresa;
                    finExtCompPar.IdExtrato = new int[ItensSelecionados];
                    finExtCompPar.DataCompensacao = psDateBox1.Value;

                    if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                    {
                        int cont = 0;

                        if (psPartApp.DataGrid != null)
                        {
                            for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                            {
                                if (psPartApp.DataGrid.Rows[i].Selected)
                                {
                                    finExtCompPar.IdExtrato[cont] = Convert.ToInt32(psPartApp.DataGrid.Rows[i].Cells["IDEXTRATO"].Value);
                                    cont++;
                                }
                            }
                        }
                    }

                    if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
                    {
                        finExtCompPar.IdExtrato = new int[] { Convert.ToInt32(gb.RetornaDataFieldByCampo(this.psPartApp.DataField, "IDEXTRATO").Valor) };
                    }

                    CompensarExtrato(finExtCompPar);

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

        private void CompensarExtrato(Support.FinExtCompPar finExtCompPar)
        {
            PSPartExtratoCaixaData psPartExtratoCaixaData = new PSPartExtratoCaixaData();
            psPartExtratoCaixaData._tablename = this.psPartApp.TableName;
            psPartExtratoCaixaData._keys = this.psPartApp.Keys;

            psPartExtratoCaixaData.CompensaExtrato(finExtCompPar);
        }

        private void PSPartCompensarExtratoAppFrm_Load(object sender, EventArgs e)
        {
            ItensSelecionados = 0;
            psDateBox1.Value = DateTime.Today;
        }
    }
}
