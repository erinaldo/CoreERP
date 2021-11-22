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
    public partial class PSPartGerarBoletoAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        PS.Lib.Valida vl = new PS.Lib.Valida();
        bool nfe = false;
        public PSPartGerarBoletoAppFrm()
        {
            InitializeComponent();
        }

        public PSPartGerarBoletoAppFrm(bool _nfe)
        {
            InitializeComponent();
            nfe = _nfe;
        }

        private void PSPartGerarBoletoAppFrm_Load(object sender, EventArgs e)
        {

        }

        public override Boolean Execute()
        {
            int env = 0, nEnv = 0;
            try
            {
                if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                {
                    if (psPartApp.DataGrid != null)
                    {
                        for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
                        {
                            if (nfe == false)
                            {
                                string status = psPartApp.DataGrid.Rows[i].DataGridView["CODTIPDOC", psPartApp.DataGrid.SelectedRows[i].Index].Value.ToString();
                                //Verificar se o mesmo já foi gerado

                                if (Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT * FROM FBOLETO WHERE CODLANCA = ? AND CODFILIAL = ?", new object[] { psPartApp.DataGrid.Rows[i].DataGridView["CODLANCA", psPartApp.DataGrid.SelectedRows[i].Index].Value.ToString(), psPartApp.DataGrid.Rows[i].DataGridView["CODFILIAL", psPartApp.DataGrid.SelectedRows[i].Index].Value.ToString() })).Equals(0))
                                {
                                    if (status.Equals("NFV"))
                                    {
                                        GerarBoleto(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[i]));
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
                            else
                            {
                                string status = psPartApp.DataGrid.Rows[i].DataGridView["CODSTATUS", psPartApp.DataGrid.SelectedRows[i].Index].Value.ToString();
                                //Verificar se o mesmo já foi gerado
                                if (status != "A")
                                {
                                    nEnv = nEnv + 1;
                                }
                                else
                                {
                                    if (Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT CODFORMA FROM FBOLETO INNER JOIN FLANCA ON FBOLETO.CODLANCA = FLANCA.CODLANCA AND FBOLETO.CODEMPRESA = FLANCA.CODEMPRESA AND FBOLETO.CODFILIAL = FLANCA.CODFILIAL  WHERE FLANCA.CODOPER = ? AND FLANCA.CODFILIAL = ?", new object[] { psPartApp.DataGrid.Rows[i].DataGridView["CODOPER", psPartApp.DataGrid.SelectedRows[i].Index].Value.ToString(), psPartApp.DataGrid.Rows[i].DataGridView["CODFILIAL", psPartApp.DataGrid.SelectedRows[i].Index].Value.ToString() })).Equals("4"))
                                    {
                                        GerarBoleto(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[i]));
                                        env = env + 1;
                                    }
                                    else
                                    {
                                        nEnv = nEnv + 1;
                                    }
                                }
                            }
                        }
                    }
                }

                if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
                {
                    GerarBoleto(this.psPartApp.DataField);
                }
                MessageBox.Show("Operação Realizada com Sucesso. \nQtde de Boletos Gerados: " + env + "\nQtde de Boletos não Gerados: " + nEnv + "\n", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
                return false;
            }

            return true;
        }

        public void GerarBoleto(List<DataField> objArr)
        {
            PSPartGerarBoletoData psPartGerarBoletoData = new PSPartGerarBoletoData();
            psPartGerarBoletoData._tablename = this.psPartApp.TableName;
            psPartGerarBoletoData._keys = this.psPartApp.Keys;

            psPartGerarBoletoData.GerarBoleto(objArr);
        }

    }
}
