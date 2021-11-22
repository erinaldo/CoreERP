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
    public partial class PSPartGerarRemessaAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        PS.Lib.Valida vl = new PS.Lib.Valida();
        public PSPartGerarRemessaAppFrm()
        {
            InitializeComponent();
        }
        public override Boolean Execute()
        {
            List<int> codLanca = new List<int>();
            string codConta_temp = string.Empty;
            List<DataField> obj = new List<DataField>();
            try
            {
                if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                {
                    if (psPartApp.DataGrid != null)
                    {
                        // João Pedr Luchiari 

                        for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                        {
                            if (psPartApp.DataGrid.Rows[i].Selected)
                            {
                                obj = gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]);
                                codConta_temp = obj[10].Valor.ToString();

                                if (codConta_temp.Equals(obj[10].Valor.ToString()))
                                {
                                    if (obj[19].Valor.Equals(0) || obj[19].Valor.Equals(5))
                                    {
                                        codLanca.Add(Convert.ToInt32(obj[1].Valor));
                                    }
                                    else
                                    {
                                        throw new Exception("O arquivo de remessa pode ser gerado apenas para boletos com status não remetido.");
                                    }
                                }
                                else
                                {
                                    throw new Exception("O arquivo de remessa pode ser gerado apenas para boletos com status não remetido.");
                                }

                                //for (int ii = 0; ii < psPartApp.DataGrid.Rows.Count; ii++)
                                //{
                                //    if (psPartApp.DataGrid.Rows[ii].Selected)
                                //    {
                                //        obj = gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[ii]);

                                //        if (codConta_temp.Equals(obj[10].Valor.ToString()))
                                //        {
                                //            if (obj[19].Valor.Equals(0) || obj[19].Valor.Equals(5))
                                //            {
                                //                codLanca.Add(Convert.ToInt32(obj[1].Valor));
                                //            }
                                //            else
                                //            {
                                //                throw new Exception("O arquivo de remessa pode ser gerado apenas para boletos com status não remetido.");
                                //            }
                                //        }
                                //        else
                                //        {
                                //            throw new Exception("O arquivo de remessa pode ser gerado apenas para boletos com status não remetido.");
                                //        }
                                //    }
                                //}
                            }
                        }
                        //obj = gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[0]);
                        //codConta_temp = obj[10].Valor.ToString();
                        //for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                        //{
                        //    if (psPartApp.DataGrid.Rows[i].Selected)
                        //    {
                        //        obj = gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]);

                        //        if (codConta_temp.Equals(obj[10].Valor.ToString()))
                        //        {
                        //            if (obj[19].Valor.Equals(0) || obj[19].Valor.Equals(5))
                        //            {
                        //                codLanca.Add(Convert.ToInt32(obj[1].Valor));
                        //            }
                        //            else
                        //            {
                        //                throw new Exception("O arquivo de remessa pode ser gerado apenas para boletos com status não remetido.");
                        //            }
                        //        }
                        //        else
                        //        {
                        //            throw new Exception("O arquivo de remessa pode ser gerado apenas para boletos com status não remetido.");
                        //        }
                        //    }
                        //}
                        Class.GeraBoleto.GerarArquivoRemessa(AppLib.Context.Empresa, codLanca, obj[10].Valor.ToString());
                    }
                }
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
