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
    public partial class PSPartCancelarRemessaAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        public int CODLANCA;

        public PSPartCancelarRemessaAppFrm()
        {
            InitializeComponent();
        }
        public override Boolean Execute()
        {
            base.Execute();
            List<DataField> obj = new List<DataField>();
            try
            {
                if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                {
                    if (psPartApp.DataGrid != null)
                    {

                        if (AppLib.Windows.FormMessageDefault.ShowQuestion("Confirma o cancelamento da remessa somente do(s) boleto(s) selecionado(s)?") == System.Windows.Forms.DialogResult.Yes)
                        {
                            for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
                            {
                                obj = gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[i]);
                                List<int> lista = new List<int>();

                                int CODEMPRESA = 0;
                                String CODCONVENIO = String.Empty;

                                if (obj[0].Valor != DBNull.Value)
                                {
                                    CODEMPRESA = Convert.ToInt32(obj[0].Valor);
                                    CODLANCA = Convert.ToInt32(obj[1].Valor);
                                    int CODREMESSA = Convert.ToInt32(obj[17].Valor);
                                    CODCONVENIO = obj[12].Valor.ToString();
                                    int IDBOLETOSTATUS = Convert.ToInt32(obj[19].Valor);

                                    if (IDBOLETOSTATUS == 0)
                                    {
                                        AppLib.Windows.FormMessageDefault.ShowInfo("Referente lançamento " + CODLANCA + ", não é possível cancelar remessa não remetida.");
                                    }

                                    if ((IDBOLETOSTATUS == 1) || (IDBOLETOSTATUS == 3))
                                    {
                                        if (!lista.Contains(CODREMESSA))
                                        {
                                            lista.Add(CODREMESSA);
                                        }
                                    }

                                    if (IDBOLETOSTATUS == 2)
                                    {
                                        AppLib.Windows.FormMessageDefault.ShowInfo("Referente lançamento " + CODLANCA + ", não é possível cancelar remessa registrada.");
                                    }

                                    if (IDBOLETOSTATUS == 4)
                                    {
                                        AppLib.Windows.FormMessageDefault.ShowInfo("Referente lançamento " + CODLANCA + ", não é possível cancelar remessa baixada.");
                                    }

                                    if (IDBOLETOSTATUS == 5)
                                    {
                                        AppLib.Windows.FormMessageDefault.ShowInfo("Referente lançamento " + CODLANCA + ", não é possível cancelar remessa já cancelada.");
                                    }
                                }

                                for (int ii = 0; ii < lista.Count; ii++)
                                {
                                    String comando = @"UPDATE FBOLETO SET CODREMESSA = NULL, DATAREMESSA = NULL, IDBOLETOSTATUS = 5 WHERE CODEMPRESA = ? AND CODCONVENIO = ? AND CODREMESSA = ? AND CODLANCA = ?";

                                    int temp = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comando, new Object[] { CODEMPRESA, CODCONVENIO, lista[ii], CODLANCA });

                                    if (temp >= 1)
                                    {
                                        // ok
                                    }
                                    else
                                    {
                                        AppLib.Windows.FormMessageDefault.ShowError("Erro ao cancelar a remessa " + lista[ii]);
                                    }
                                }
                            }
                        }
                        //
                        else
                        {
                            for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                            {
                                if (psPartApp.DataGrid.Rows[i].Selected)
                                {
                                    obj = gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]);
                                    if (AppLib.Windows.FormMessageDefault.ShowQuestion("ATENÇÃO: TODOS OS BOLETOS DA MESMA REMESSA SERÃO CANCELADOS.") == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        List<int> lista = new List<int>();

                                        int CODEMPRESA = 0;
                                        String CODCONVENIO = String.Empty;

                                        if (obj[0].Valor != DBNull.Value)
                                        {
                                            CODEMPRESA = Convert.ToInt32(obj[0].Valor);
                                            int CODLANCA = Convert.ToInt32(obj[1].Valor);
                                            int CODREMESSA = Convert.ToInt32(obj[17].Valor);
                                            CODCONVENIO = obj[12].Valor.ToString();
                                            int IDBOLETOSTATUS = Convert.ToInt32(obj[19].Valor);

                                            if (IDBOLETOSTATUS == 0)
                                            {
                                                AppLib.Windows.FormMessageDefault.ShowInfo("Referente lançamento " + CODLANCA + ", não é possível cancelar remessa não remetida.");
                                            }

                                            if ((IDBOLETOSTATUS == 1) || (IDBOLETOSTATUS == 3))
                                            {
                                                if (!lista.Contains(CODREMESSA))
                                                {
                                                    lista.Add(CODREMESSA);
                                                }
                                            }

                                            if (IDBOLETOSTATUS == 2)
                                            {
                                                AppLib.Windows.FormMessageDefault.ShowInfo("Referente lançamento " + CODLANCA + ", não é possível cancelar remessa registrada.");
                                            }

                                            if (IDBOLETOSTATUS == 4)
                                            {
                                                AppLib.Windows.FormMessageDefault.ShowInfo("Referente lançamento " + CODLANCA + ", não é possível cancelar remessa baixada.");
                                            }

                                            if (IDBOLETOSTATUS == 5)
                                            {
                                                AppLib.Windows.FormMessageDefault.ShowInfo("Referente lançamento " + CODLANCA + ", não é possível cancelar remessa já cancelada.");
                                            }
                                        }

                                        for (int ii = 0; ii < lista.Count; ii++)
                                        {
                                            String comando = @"
UPDATE FBOLETO
SET CODREMESSA = NULL, DATAREMESSA = NULL, IDBOLETOSTATUS = 5
WHERE CODEMPRESA = ?
  AND CODCONVENIO = ?
  AND CODREMESSA = ?";

                                            int temp = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comando, new Object[] { CODEMPRESA, CODCONVENIO, lista[ii] });

                                            if (temp >= 1)
                                            {
                                                // ok
                                            }
                                            else
                                            {
                                                AppLib.Windows.FormMessageDefault.ShowError("Erro ao cancelar a remessa " + lista[ii]);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //                else
                //                {
                //                    for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                //                    {
                //                        if (psPartApp.DataGrid.Rows[i].Selected)
                //                        {
                //                            obj = gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]);
                //                            if (AppLib.Windows.FormMessageDefault.ShowQuestion("Confirma o cancelamento deste e dos demais boletos da mesma remessa?\r\nATENÇÃO: TODOS OS BOLETOS DA MESMA REMESSA SERÃO CANCELADOS.") == System.Windows.Forms.DialogResult.Yes)
                //                            {
                //                                List<int> lista = new List<int>();

                //                                int CODEMPRESA = 0;
                //                                String CODCONVENIO = String.Empty;

                //                                if (obj[0].Valor != DBNull.Value)
                //                                {
                //                                    CODEMPRESA = Convert.ToInt32(obj[0].Valor);
                //                                    int CODLANCA = Convert.ToInt32(obj[1].Valor);
                //                                    int CODREMESSA = Convert.ToInt32(obj[17].Valor);
                //                                    CODCONVENIO = obj[12].Valor.ToString();
                //                                    int IDBOLETOSTATUS = Convert.ToInt32(obj[19].Valor);

                //                                    if (IDBOLETOSTATUS == 0)
                //                                    {
                //                                        AppLib.Windows.FormMessageDefault.ShowInfo("Referente lançamento " + CODLANCA + ", não é possível cancelar remessa não remetida.");
                //                                    }

                //                                    if ((IDBOLETOSTATUS == 1) || (IDBOLETOSTATUS == 3))
                //                                    {
                //                                        if (!lista.Contains(CODREMESSA))
                //                                        {
                //                                            lista.Add(CODREMESSA);
                //                                        }
                //                                    }

                //                                    if (IDBOLETOSTATUS == 2)
                //                                    {
                //                                        AppLib.Windows.FormMessageDefault.ShowInfo("Referente lançamento " + CODLANCA + ", não é possível cancelar remessa registrada.");
                //                                    }

                //                                    if (IDBOLETOSTATUS == 4)
                //                                    {
                //                                        AppLib.Windows.FormMessageDefault.ShowInfo("Referente lançamento " + CODLANCA + ", não é possível cancelar remessa baixada.");
                //                                    }

                //                                    if (IDBOLETOSTATUS == 5)
                //                                    {
                //                                        AppLib.Windows.FormMessageDefault.ShowInfo("Referente lançamento " + CODLANCA + ", não é possível cancelar remessa já cancelada.");
                //                                    }
                //                                }

                //                                for (int ii = 0; ii < lista.Count; ii++)
                //                                {
                //                                    String comando = @"
                //UPDATE FBOLETO
                //SET CODREMESSA = NULL, DATAREMESSA = NULL, IDBOLETOSTATUS = 5
                //WHERE CODEMPRESA = ?
                //  AND CODCONVENIO = ?
                //  AND CODREMESSA = ?";

                //                                    int temp = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comando, new Object[] { CODEMPRESA, CODCONVENIO, lista[ii] });

                //                                    if (temp >= 1)
                //                                    {
                //                                        // ok
                //                                    }
                //                                    else
                //                                    {
                //                                        AppLib.Windows.FormMessageDefault.ShowError("Erro ao cancelar a remessa " + lista[ii]);
                //                                    }
                //                                }
                //                            }
                //                        }
                //                    }
                //                }
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
                return false;
            }

            return true;
        }
    }
}
