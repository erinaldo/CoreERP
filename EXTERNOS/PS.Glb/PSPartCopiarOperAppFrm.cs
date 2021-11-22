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
    public partial class PSPartCopiarOperAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        private PS.Lib.Constantes ct = new PS.Lib.Constantes();
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Lib.Valida vl = new PS.Lib.Valida();


        public PSPartCopiarOperAppFrm()
        {
            InitializeComponent();
        }

        private void PSPartCopiarOperAppFrm_Load(object sender, EventArgs e)
        {
        }
        public override Boolean Execute()
        {
            if (PS.Lib.PSMessageBox.ShowQuestion("Confirma a cópia da(s) operação(ões) selecionada(s) ?") == System.Windows.Forms.DialogResult.Yes)
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
                                    Copiar(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]));
                                }
                            }
                        }
                    }

                    if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
                    {
                        Copiar(this.psPartApp.DataField);
                    }

                    //para atualizar a visão depois da execução do aplicativo
                    this.psPartApp.Refresh = true;

                    PS.Lib.PSMessageBox.ShowInfo("Operação realizada com sucesso.");

                }
                catch (Exception ex)
                {
                    PS.Lib.PSMessageBox.ShowError(ex.Message);
                    AppLib.Context.poolConnection.Get().Rollback();
                    return false;
                }
            }

            return true;
        }
        private void Copiar(List<DataField> objArr)
        {
            PSPartOperacaoData psPartOperacaoData = new PSPartOperacaoData();
            psPartOperacaoData._tablename = this.psPartApp.TableName;
            psPartOperacaoData._keys = this.psPartApp.Keys;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODFILIAL = gb.RetornaDataFieldByCampo(objArr, "CODFILIAL");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfCODTIPOOPER = gb.RetornaDataFieldByCampo(objArr, "CODTIPOPER");

            string codTipOper = string.Empty;

            PSPartOperacao _psPartOperacao = new PSPartOperacao();
            _psPartOperacao.DefaultFilter.Add(new PS.Lib.PSFilter("CODFILIAL", dfCODFILIAL.Valor));
            _psPartOperacao.DefaultFilter.Add(new PS.Lib.PSFilter("CODTIPOPER", dfCODTIPOOPER.Valor));
            _psPartOperacao.AllowSave = true;
            _psPartOperacao.AllowInsert = false;
            _psPartOperacao.AllowDelete = false;
            
            //Inseri o item 
            string newCodOper = new Class.CopiaOperacao().CopiarOperacao(dfCODOPER.Valor.ToString(), Convert.ToInt32(dfCODFILIAL.Valor));

            if (!string.IsNullOrEmpty(newCodOper))
            {
                _psPartOperacao.ExecuteWithParams(dfCODEMPRESA.Valor, newCodOper);                
            }
            else
            {
                MessageBox.Show("Não foi possível concluir a rotina de cópia.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
        }
    }
}
