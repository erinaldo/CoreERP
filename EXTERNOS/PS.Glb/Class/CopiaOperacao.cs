using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace PS.Glb.Class
{
    public class CopiaOperacao
    {
        public static string Codtipoper = string.Empty;

        public CopiaOperacao() { }

        // Valor unitário
        public string Codclifor = string.Empty;
        private string codoper = string.Empty;
        private int codFilial = 0;

        //
        public string CodOperNum = string.Empty;

        public CopiaOperacao(int _codFilial)
        {
            codFilial = _codFilial;
        }

        private int getMaxOper(string tabela)
        {
            return Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT IDLOG FROM GLOG WHERE CODTABELA = ?", new object[] { tabela })) + 1;
        }


        public string CopiarOperacao(string _codoper, int _codFilial)
         {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();
            codoper = getMaxOper("GOPER").ToString();
            codFilial = _codFilial;
            try
            {

                #region GOPER
                AppLib.ORM.Jit goper = new AppLib.ORM.Jit(conn, "GOPER");
                goper.Set("CODEMPRESA", AppLib.Context.Empresa);
                goper.Set("CODOPER", _codoper);
                goper.Select();
                goper.Set("CODOPER", codoper);
                goper.Set("DATACRIACAO", AppLib.Context.poolConnection.Get("Start").GetDateTime());
                goper.Set("DATAEMISSAO", AppLib.Context.poolConnection.Get("Start").GetDateTime());
                goper.Set("DATAENTSAI", null);
                goper.Set("NUMERO", GeraNumeroDocumento(goper.Get("CODTIPOPER").ToString()));

                #region Segundo Número
                bool usaSegundoNumero = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, @"SELECT USASEGUNDONUMERO FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { goper.Get("CODTIPOPER"), AppLib.Context.Empresa }));
                if (usaSegundoNumero == true)
                {
                    DataTable dtSegundoNumero = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT SEGUNDONUMERO, QTDSEGUNDONUMERO, SEQSEGUNDONUMERO FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, codFilial });

                    if (Convert.ToBoolean(dtSegundoNumero.Rows[0]["SEQSEGUNDONUMERO"]) == true)
                    {

                        goper.Set("SEGUNDONUMERO", AppLib.Util.Format.CompletarZeroEsquerda(Convert.ToInt32(dtSegundoNumero.Rows[0]["QTDSEGUNDONUMERO"]), Convert.ToString(Convert.ToInt32(dtSegundoNumero.Rows[0]["SEGUNDONUMERO"]) + 1)));

                    }
                    else
                    {
                        goper.Set("SEGUNDONUMERO", null);
                    }
                }
                #endregion

                goper.Set("CODSTATUS", 0);
                goper.Set("NFE", goper.Get("NFE"));
                goper.Set("CHAVENFE", null);
                goper.Set("CODUSUARIO", AppLib.Context.Usuario);
                goper.Insert().ToString();


                #endregion
                #region GOPERCOMPL
                AppLib.ORM.Jit goperCompl = new AppLib.ORM.Jit(conn, "GOPERCOMPL");
                goperCompl.Set("CODEMPRESA", AppLib.Context.Empresa);
                goperCompl.Set("CODOPER", _codoper);
                goperCompl.Select();
                goperCompl.Set("CODOPER", goper.Get("CODOPER"));
                goperCompl.Save();
                #endregion
                #region GOPERRATEIOCC
                if (goper.Get("CODCCUSTO") != null)
                {
                    AppLib.ORM.Jit goperRateioCC = new AppLib.ORM.Jit(conn, "GOPERRATEIOCC");
                    goperRateioCC.Set("CODEMPRESA", AppLib.Context.Empresa);
                    goperRateioCC.Set("CODOPER", _codoper);
                    goperRateioCC.Set("CODCCUSTO", goper.Get("CODCCUSTO"));
                    goperRateioCC.Select();
                    goperRateioCC.Set("CODOPER", goper.Get("CODOPER"));
                    goperRateioCC.Save();
                }
                #endregion
                #region GOPERRATEIODP
                AppLib.ORM.Jit goperRateioDP = new AppLib.ORM.Jit(conn, "GOPERRATEIODP");
                string codDepto = conn.ExecGetField(string.Empty, "SELECT CODDEPTO FROM GOPERRATEIODP WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { _codoper, AppLib.Context.Empresa }).ToString();
                goperRateioDP.Set("CODEMPRESA", AppLib.Context.Empresa);
                goperRateioDP.Set("CODOPER", _codoper);
                goperRateioDP.Set("CODFILIAL", goper.Get("CODFILIAL"));
                goperRateioDP.Set("CODDEPTO", codDepto);
                goperRateioDP.Select();
                if (goperRateioDP.Count() > 0)
                {
                    goperRateioDP.Set("CODOPER", goper.Get("CODOPER"));
                    goperRateioDP.Save();
                }
                #endregion
                #region GOPERITEM

                AppLib.ORM.Jit goperItem = new AppLib.ORM.Jit(conn, "GOPERITEM");
                DataTable dt = conn.ExecQuery(@"SELECT * FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codoper });
                string vlUnitarioEm = string.Empty;
                bool executaTabPreco = false;

                string TipoValor = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT VLUNITARIOEM FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { goper.Get("CODEMPRESA"), goper.Get("CODTIPOPER") }).ToString();

                if (TipoValor == "TABCLI")
                {
                    if (MessageBox.Show("Deseja executar atualização do preço conforme tabela?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        executaTabPreco = true;
                        //int codTabPreco = Convert.ToInt32(conn.ExecGetField(0, "SELECT CODTABPRECO FROM VCLIFOR WHERE CODEMPRESA = ? AND CODCLIFOR = ?", new object[] { goper.Get("CODEMPRESA"), goper.Get("CODCLIFOR") }));
                        //if (codTabPreco == 6)
                        //{
                        vlUnitarioEm = TipoValor; /*conn.ExecGetField(string.Empty, "SELECT VLUNITARIOEM FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { goper.Get("CODEMPRESA"), goper.Get("CODTIPOPER") }).ToString();*/
                        //}
                    }
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goperItem.Set("CODEMPRESA", AppLib.Context.Empresa);
                    goperItem.Set("CODOPER", _codoper);
                    goperItem.Set("NSEQITEM", dt.Rows[i]["NSEQITEM"]);
                    goperItem.Select();
                    goperItem.Set("CODOPER", goper.Get("CODOPER"));
                    goperItem.Set("QUANTIDADE_SALDO", goperItem.Get("QUANTIDADE"));
                    goperItem.Set("QUANTIDADE_FATURADO", 0);

                    if (executaTabPreco == true)
                    {
                        if (vlUnitarioEm == "TABCLI")
                        {
                            decimal vlUnitarioItem = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT VCLIFORTABPRECOITEM.PRECOUNITARIO FROM VCLIFORTABPRECOITEM INNER JOIN VCLIFORTABPRECO ON VCLIFORTABPRECOITEM.IDTABELA = VCLIFORTABPRECO.IDTABELA AND VCLIFORTABPRECOITEM.CODEMPRESA = VCLIFORTABPRECO.CODEMPRESA
WHERE VCLIFORTABPRECOITEM.CODPRODUTO = ? AND VCLIFORTABPRECOITEM.CODEMPRESA = ? AND VCLIFORTABPRECOITEM.CODFILIAL = ? AND VCLIFORTABPRECO.CODCLIFOR = ? ", new object[] { goperItem.Get("CODPRODUTO"), goperItem.Get("CODEMPRESA"), goper.Get("CODFILIAL"), goper.Get("CODCLIFOR") }));
                            if (vlUnitarioItem != 0)
                            {
                                goperItem.Set("VLUNITARIO", vlUnitarioItem);
                                goperItem.Set("VLUNITORIGINAL", vlUnitarioItem);
                                decimal vlTotalItem = calculaItem(Convert.ToDecimal(goperItem.Get("VLACRESCIMO")), Convert.ToDecimal(goperItem.Get("PRACRESCIMO")), Convert.ToDecimal(goperItem.Get("VLUNITARIO")), goperItem.Get("TIPODESCONTO").ToString(), Convert.ToDecimal(goperItem.Get("VLDESCONTO")), Convert.ToDecimal(goperItem.Get("PRDESCONTO")), Convert.ToDecimal(goperItem.Get("QUANTIDADE")));
                                goperItem.Set("VLTOTALITEM", vlTotalItem);
                            }
                        }
                    }
                    goperItem.Insert();
                    goperItem.Clear();
                }
                #endregion
                #region GOPERITEMDIFAL
                AppLib.ORM.Jit goperItemDifal = new AppLib.ORM.Jit(conn, "GOPERITEMDIFAL");
                dt = new DataTable();
                dt = conn.ExecQuery(@"SELECT * FROM GOPERITEMDIFAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codoper });
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goperItemDifal.Set("CODEMPRESA", AppLib.Context.Empresa);
                    goperItemDifal.Set("CODOPER", _codoper);
                    goperItemDifal.Set("NSEQITEM", dt.Rows[i]["NSEQITEM"]);
                    goperItemDifal.Select();
                    goperItemDifal.Set("CODOPER", goper.Get("CODOPER"));
                    goperItemDifal.Insert();
                    goperItemDifal.Clear();
                }
                #endregion
                #region GOPERITEMCOMPL
                AppLib.ORM.Jit goperItemCompl = new AppLib.ORM.Jit(conn, "GOPERITEMCOMPL");
                dt = new DataTable();
                dt = conn.ExecQuery(@"SELECT * FROM GOPERITEMCOMPL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codoper });
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goperItemCompl.Set("CODEMPRESA", AppLib.Context.Empresa);
                    goperItemCompl.Set("CODOPER", _codoper);
                    goperItemCompl.Set("NSEQITEM", dt.Rows[i]["NSEQITEM"]);
                    goperItemCompl.Select();
                    goperItemCompl.Set("CODOPER", goper.Get("CODOPER"));
                    goperItemCompl.Insert();
                    goperItemCompl.Clear();
                }
                #endregion
                #region GOPERITEMRATEIOCC
                AppLib.ORM.Jit goperItemtRateioCC = new AppLib.ORM.Jit(conn, "GOPERITEMRATEIOCC");
                dt = new DataTable();
                dt = conn.ExecQuery(@"SELECT * FROM GOPERITEMRATEIOCC WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codoper });
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goperItemtRateioCC.Set("CODEMPRESA", AppLib.Context.Empresa);
                    goperItemtRateioCC.Set("CODOPER", _codoper);
                    goperItemtRateioCC.Set("NSEQITEM", dt.Rows[i]["NSEQITEM"]);
                    goperItemtRateioCC.Set("CODCCUSTO", dt.Rows[i]["CODCCUSTO"]);
                    goperItemtRateioCC.Select();
                    goperItemtRateioCC.Set("CODOPER", goper.Get("CODOPER"));
                    goperItemtRateioCC.Insert();
                    goperItemtRateioCC.Clear();
                }
                #endregion
                #region GOPERITEMRATEIODP
                AppLib.ORM.Jit goperItemtRateioDP = new AppLib.ORM.Jit(conn, "GOPERITEMRATEIODP");
                dt = new DataTable();
                dt = conn.ExecQuery(@"SELECT * FROM GOPERITEMRATEIOCC WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codoper });
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goperItemtRateioDP.Set("CODEMPRESA", AppLib.Context.Empresa);
                    goperItemtRateioDP.Set("CODOPER", _codoper);
                    goperItemtRateioDP.Set("NSEQITEM", dt.Rows[i]["NSEQITEM"]);
                    goperItemtRateioDP.Set("CODFILIAL", dt.Rows[i]["CODFILIAL"]);
                    goperItemtRateioDP.Set("CODDEPTO", dt.Rows[i]["CODDEPTO"]);
                    goperItemtRateioDP.Select();
                    goperItemtRateioDP.Set("CODOPER", goper.Get("CODOPER"));
                    goperItemtRateioDP.Insert();
                    goperItemtRateioDP.Clear();
                }
                #endregion
                #region GOPERITEMRECURSO
                AppLib.ORM.Jit goperItemtRecurso = new AppLib.ORM.Jit(conn, "GOPERITEMRECURSO");
                dt = new DataTable();
                dt = conn.ExecQuery(@"SELECT * FROM GOPERITEMRECURSO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codoper });
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goperItemtRecurso.Set("CODEMPRESA", AppLib.Context.Empresa);
                    goperItemtRecurso.Set("CODOPER", _codoper);
                    goperItemtRecurso.Set("NSEQITEM", dt.Rows[i]["NSEQITEM"]);
                    goperItemtRecurso.Set("CODOPERADOR", dt.Rows[i]["CODOPERADOR"]);
                    goperItemtRecurso.Select();
                    goperItemtRecurso.Set("CODOPER", goper.Get("CODOPER"));
                    goperItemtRecurso.Insert();
                    goperItemtRecurso.Clear();
                }
                #endregion
                #region ALTERA A GLOG
                AppLib.ORM.Jit GLOG = new AppLib.ORM.Jit(conn, "GLOG");
                GLOG.Set("CODEMPRESA", AppLib.Context.Empresa);
                GLOG.Set("CODTABELA", "GOPER");
                GLOG.Set("IDLOG", codoper);
                GLOG.Save();
                #endregion
                #region ALTERA VSERIE
                AppLib.ORM.Jit VSERIE = new AppLib.ORM.Jit(conn, "VSERIE");
                VSERIE.Set("CODEMPRESA", AppLib.Context.Empresa);
                VSERIE.Set("CODFILIAL", goper.Get("CODFILIAL"));
                VSERIE.Set("CODSERIE", goper.Get("CODSERIE"));
                VSERIE.Set("NUMSEQ", goper.Get("NUMERO"));
                VSERIE.Save();
                #endregion
                #region ALTERA SEGUNDONUMERO 
                if (usaSegundoNumero == true)
                {
                    AppLib.ORM.Jit FILIAL = new AppLib.ORM.Jit(conn, "GFILIAL");
                    FILIAL.Set("CODEMPRESA", AppLib.Context.Empresa);
                    FILIAL.Set("CODFILIAL", codFilial);
                    FILIAL.Set("SEGUNDONUMERO", goper.Get("SEGUNDONUMERO"));
                    FILIAL.Save();
                }
                #endregion
                conn.Commit();

                #region CALCULA O ESTOQUE

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PS.Glb.PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                    psPartLocalEstoqueSaldoData.MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao, int.Parse(AppLib.Context.Empresa.ToString()), int.Parse(goper.Get("CODOPER").ToString()), Convert.ToInt32(dt.Rows[i]["NSEQITEM"]));

                }

                #endregion

                return codoper;
            }
            catch (Exception)
            {
                conn.Rollback();
                return string.Empty;
            }
        }

        private decimal calculaAcrescimo(decimal vlAcrescimo, decimal prAcrescimo, decimal vlUnitarioOriginal)
        {
            decimal vl = 0, pr = 0;
            vl = vlAcrescimo;
            pr = prAcrescimo;
            //Preenchendo os campos
            if (vl == 0)
            {
                vl = ((pr * vlUnitarioOriginal) / 100);
            }
            else
            {
                pr = ((vl / vlUnitarioOriginal) * 100);
            }
            return vl;

        }

        private decimal calculaDesconto(string tipo, decimal vlDesconto, decimal percDesconto, decimal vlUnitarioOriginal, decimal quantidade)
        {
            decimal vl = 0, pr = 0;
            vl = vlDesconto;
            pr = percDesconto;
            if (tipo == "Unitário")
            {
                //Preenchendo os campos
                if (vl == 0)
                {
                    vl = ((pr * vlUnitarioOriginal) / 100);
                }
                else
                {
                    pr = ((vl / vlUnitarioOriginal) * 100);
                }
                return vl;
            }
            else
            {
                //Preenchendo os campos
                if (vl == 0)
                {
                    vl = (pr * (vlUnitarioOriginal * quantidade) / 100);
                }
                else
                {
                    pr = (vl / (vlUnitarioOriginal * quantidade) * 100);
                }
                return vl;
            }

        }

        private decimal calculaItem(decimal vlAcrescimo, decimal prAcrescimo, decimal vlUnitarioOriginal, string tipoDesconto, decimal vlDesconto, decimal percDesconto, decimal quantidade)
        {
            decimal acrescimo = calculaAcrescimo(vlAcrescimo, prAcrescimo, vlUnitarioOriginal);
            decimal vlUnitario = 0;
            decimal desconto = 0;
            try
            {
                if (tipoDesconto == "U")
                {
                    desconto = calculaDesconto("Unitário", vlDesconto, percDesconto, vlUnitarioOriginal, quantidade);
                    vlUnitario = ((vlUnitarioOriginal + acrescimo) - desconto);
                    if (vlUnitario < 0)
                    {
                        MessageBox.Show("O valor de desconto não pode ser maior que o valor unitário do item.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return 0;
                    }
                }
                else if (tipoDesconto == "T")
                {
                    desconto = calculaDesconto("Total", vlDesconto, percDesconto, vlUnitarioOriginal, quantidade);
                    vlUnitario = (vlUnitarioOriginal + acrescimo);
                    if (vlUnitario < 0)
                    {
                        MessageBox.Show("O valor de desconto não pode ser maior que o valor unitário do item.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return 0;
                    }
                }
                return vlUnitario;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro a realizar o cálculo. Favor verificar.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        #region Gerar Numero
        public string GeraNumeroDocumento(string tipoper)
        {
            string novoNumStr = "";
            PS.Lib.Global gb = new PS.Lib.Global();
            System.Data.DataRow PARAMTIPOPER = gb.RetornaParametrosOperacao(tipoper);

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit goper = new AppLib.ORM.Jit(conn, "GOPER");

            if (PARAMTIPOPER == null)
            {
                throw new Exception("Não foi possível gerar o número da operação pois o tipo da operação não esta parametrizado.");
            }
            else
            {
                if (int.Parse(PARAMTIPOPER["USANUMEROSEQ"].ToString()) == 1)
                {
                    int mask = (PARAMTIPOPER["MASKNUMEROSEQ"] == DBNull.Value) ? 0 : Convert.ToInt32(PARAMTIPOPER["MASKNUMEROSEQ"]);
                    if (mask == 0)
                    {
                        throw new Exception("Não foi possível gerar o número da operação pois a máscara do numero da operação não está parametrizada.");
                    }
                    else
                    {
                        //Busca a série do novo campo da tabela VSERIE
                        //string sql = @"SELECT NUMSEQ FROM VSERIE WHERE CODSERIE = ? ";
                        string sql = @"SELECT VSERIE.NUMSEQ, VSERIE.CODSERIE FROM VSERIE INNER JOIN VTIPOPERSERIE ON VSERIE.CODSERIE = VTIPOPERSERIE.CODSERIE WHERE VSERIE.CODEMPRESA = ? AND VSERIE.CODFILIAL = ? AND VTIPOPERSERIE.CODTIPOPER = ? AND VSERIE.CODSERIE = ?";

                        string codSerie = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODSERIE FROM VTIPOPERSERIE where CODTIPOPER = ? and CODFILIAL = ? and PRINCIPAL =  1 AND CODEMPRESA = ?", new object[] { tipoper, codFilial, AppLib.Context.Empresa }).ToString();
                        int ultimo = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, sql, new object[] { AppLib.Context.Empresa, codFilial, tipoper, codSerie }));

                        //int ultimo = (PARAMTIPOPER["ULTIMONUMERO"] == DBNull.Value) ? 0 : Convert.ToInt32(PARAMTIPOPER["ULTIMONUMERO"]);
                        ultimo++;
                        int tamanho = ultimo.ToString().Length;
                        novoNumStr = string.Concat(ultimo.ToString().PadLeft(mask, '0'));

                        if (novoNumStr.Length > mask)
                        {
                            throw new Exception("Quantidade de caracteres do número da operação é maior que a quantidade permitida.");
                        }
                        return novoNumStr;
                    }
                }
                else
                {
                    if (PARAMTIPOPER["OPERSERIE"].ToString() == "N")
                    {
                        
                        novoNumStr = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT DISTINCT(GOPER.NUMERO) FROM GOPER INNER JOIN GOPERRELAC ON GOPERRELAC.CODEMPRESA = GOPER.CODEMPRESA AND GOPERRELAC.CODOPER = GOPER.CODOPER WHERE GOPER.CODEMPRESA = ? AND GOPERRELAC.CODOPER = ?", new object[] { AppLib.Context.Empresa, CodOperNum}).ToString(); 
                        return novoNumStr;
                    }
                    else
                    {
                        return novoNumStr;
                    }
                }
            }
        }
        #endregion

        private bool verificaStatus(string codOper, AppLib.Data.Connection conn)
        {
            int codStatus = Convert.ToInt32(conn.ExecGetField(0, @"SELECT CODSTATUS FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", AppLib.Context.Empresa, codOper));
            if (codStatus == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private DataRow getParametros(string codTipOper, AppLib.Data.Connection conn)
        {
            DataTable dt = conn.ExecQuery(@"SELECT * FROM GTIPOPER WHERE GTIPOPER.CODEMPRESA = ? AND GTIPOPER.CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper });
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            return null;
        }

        public static string getCodTipoper(int _codoper)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit goper = new AppLib.ORM.Jit(conn, "GOPER");
            goper.Set("CODEMPRESA", AppLib.Context.Empresa);
            goper.Set("CODOPER", _codoper);
            goper.Select();
            Codtipoper = goper.Get("CODTIPOPER").ToString();
            return Codtipoper;
        }

        public string TransferiOperacao(string _codoper, string codTipOper, int _codFilial)
        {
            codFilial = _codFilial;
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            //conn.BeginTransaction();
            codoper = getMaxOper("GOPER").ToString();
            try
            {
                #region GOPER
                AppLib.ORM.Jit goper = new AppLib.ORM.Jit(conn, "GOPER");
                goper.Set("CODEMPRESA", AppLib.Context.Empresa);
                goper.Set("CODOPER", _codoper);
                goper.Select();

                #region Verifica Status
                if (verificaStatus(_codoper, conn) == false)
                {
                    conn.Rollback();
                    return string.Empty;
                }
                #endregion

                goper.Set("CODUSUARIO", AppLib.Context.Usuario);

                #region Busca o parametro
                DataRow param_destino = getParametros(codTipOper, conn);
                if (param_destino == null)
                {
                    conn.Rollback();
                    return string.Empty;
                }
                #endregion

                #region Número
                if (param_destino["USANUMEROSEQ"].ToString() == "1")
                {
                    goper.Set("NUMERO", GeraNumeroDocumento(codTipOper));
                }
                #endregion

                #region Série
                if (param_destino["OPERSERIE"].ToString() == "M")
                {
                    goper.Set("CODSERIE", null);
                }
                else
                {
                    string codSerie = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODSERIE FROM VTIPOPERSERIE WHERE CODTIPOPER = ? AND CODFILIAL = ? AND PRINCIPAL =  1 AND CODEMPRESA = ?", new object[] { codTipOper, codFilial, AppLib.Context.Empresa }).ToString();
                    goper.Set("CODSERIE", codSerie);
                }
                #endregion

                #region Local 1
                if (param_destino["LOCAL1"].ToString() == "M")
                {
                    goper.Set("CODLOCAL", null);
                }
                else
                {
                    try
                    {
                        string codlocal = goper.Get("CODLOCAL").ToString();
                    }
                    catch (Exception)
                    {
                        goper.Set("CODLOCAL", param_destino["LOCAL1DEFAULT"]);
                    }

                }
                #endregion

                #region Local 2
                if (param_destino["LOCAL2"].ToString() == "M")
                {
                    goper.Set("CODLOCALENTREGA", null);
                }
                #endregion

                #region Cliente
                if (param_destino["CODCLIFOR"].ToString() == "M")
                {
                    goper.Set("CODCLIFOR", null);
                }
                else
                {
                    if (goper.Get("CODCLIFOR") == DBNull.Value)
                    {
                        goper.Set("CODCLIFOR", param_destino["CODCLIFORPADRAO"]);
                    }
                }
                #endregion

                // João Pedro - 15/12/2017 - Incluído os campos DATAALTERACAO, USUARIOALTERACAO e CODUSUARIOCRIACAO.
                goper.Set("DATAEMISSAO", conn.GetDateTime());
                goper.Set("DATAALTERACAO", conn.GetDateTime());
                goper.Set("USUARIOALTERACAO", AppLib.Context.Usuario);
                goper.Set("CODUSUARIOCRIACAO", AppLib.Context.Usuario);

                #region Data Entra / Sai
                if (param_destino["USADATAENTSAI"].ToString() == "M")
                {
                    goper.Set("DATAENTSAI", null);
                }
                else
                {
                    goper.Set("DATAENTSAI", conn.GetDateTime());
                }
                #endregion

                #region Data Entrega
                if (param_destino["USADATAENTREGA"].ToString() == "M")
                {
                    goper.Set("DATAENTREGA", null);
                }
                //else
                //{
                //    DateTime dataEntrega;
                //    if (goper.Get("DATAENTREGA") == DBNull.Value)
                //    {
                //        dataEntrega = conn.GetDateTime();
                //    }
                //    else
                //    {
                //        dataEntrega = Convert.ToDateTime(goper.Get("DATAENTREGA"));
                //    }
                //    goper.Set("DATAENTREGA", conn.GetDateTime() + (dataEntrega - Convert.ToDateTime(goper.Get("DATAEMISSAO"))));
                //}
                #endregion

                #region Objeto
                if (param_destino["USACAMPOOBJETO"].ToString() == "M")
                {
                    goper.Set("CODOBJETO", null);
                }
                #endregion

                #region Operador
                if (param_destino["USACAMPOOPERADOR"].ToString() == "M")
                {
                    goper.Set("CODOPERADOR", null);
                }
                else
                {
                    if (goper.Get("CODOPERADOR") == DBNull.Value)
                    {
                        goper.Set("CODOPERADOR", param_destino["CODOPERADORPADRAO"]);
                    }
                }
                #endregion

                #region Condição de Pagamento
                if (param_destino["USACAMPOCONDPGTO"].ToString() == "M")
                {
                    goper.Set("CODCONDICAO", null);
                }
                else
                {
                    if (goper.Get("CODCONDICAO") == null)
                    {
                        goper.Set("CODCONDICAO", param_destino["CODCONDICAOPADRAO"]);
                    }
                }
                #endregion

                #region Forma de Pagamento
                if (param_destino["CODFORMA"].ToString() == "M")
                {
                    goper.Set("CODFORMA", null);
                }
                else
                {
                    if (goper.Get("CODFORMA") == DBNull.Value)
                    {
                        goper.Set("CODFORMA", param_destino["CODFORMAPADRAO"]);
                    }
                }
                #endregion

                #region Conta Caixa
                if (param_destino["USACONTA"].ToString() == "M")
                {
                    goper.Set("CODCONTA", null);
                }
                else
                {
                    if (goper.Get("CODCONTA") == DBNull.Value)
                    {
                        goper.Set("CODCONTA", param_destino["CODCONTAPADRAO"]);
                    }
                }
                #endregion

                #region Representante
                if (param_destino["USACODREPRE"].ToString() == "M")
                {
                    goper.Set("CODREPRE", null);
                }
                else
                {
                    if (goper.Get("CODREPRE") == DBNull.Value)
                    {
                        goper.Set("CODREPRE", param_destino["CODREPREPADRAO"]);
                    }
                }
                #endregion

                #region Centro de Custo
                if (param_destino["USACODCCUSTO"].ToString() == "M")
                {
                    goper.Set("CODCCUSTO", null);
                }
                #endregion

                #region Natureza Orçamentária
                if (param_destino["USACODNATUREZAORCAMENTO"].ToString() == "M")
                {
                    goper.Set("CODNATUREZAORCAMENTO", null);
                }
                #endregion

                #region Observação / histórico
                if (param_destino["USAABAOBSERV"].ToString() != "1")
                {
                    goper.Set("OBSERVACAO", null);
                    goper.Set("HISTORICO", null);
                }
                #endregion

                #region Transporte
                if (param_destino["USAABATRANSP"].ToString() != "1")
                {
                    goper.Set("FRETECIFFOB", 0);
                    goper.Set("CODTRANSPORTADORA", null);
                    goper.Set("QUANTIDADE", 0);
                    goper.Set("PESOLIQUIDO", 0);
                    goper.Set("PESOBRUTO", 0);
                    goper.Set("ESPECIE", null);
                    goper.Set("MARCA", null);
                }
                #endregion

                #region Usa Valor Frete
                if (param_destino["USAVALORFRETE"].ToString() == "M")
                {
                    goper.Set("VALORFRETE", 0);
                    goper.Set("PERCFRETE", 0);
                }
                #endregion

                #region Valor Desconto
                if (param_destino["USAVALORDESCONTO"].ToString() == "M")
                {
                    goper.Set("VALORDESCONTO", 0);
                    goper.Set("PERCDESCONTO", 0);
                }
                #endregion

                #region Valor Despesa
                if (param_destino["USAVALORDESPESA"].ToString() == "M")
                {
                    goper.Set("VALORDESPESA", 0);
                    goper.Set("PERCDESPESA", 0);
                }
                #endregion

                #region Valor Seguro
                if (param_destino["USAVALORSEGURO"].ToString() == "M")
                {
                    goper.Set("VALORSEGURO", 0);
                    goper.Set("PERCSEGURO", 0);
                }
                #endregion

                #region Atualiza Operação Destino
                if (param_destino["CRIAROPFAT"].ToString() == "1")
                {
                    goper.Set("CODSTATUS", 1);
                    goper.Set("DATAFATURAMENTO", conn.GetDateTime());
                    goper.Set("CODUSUARIOFATURAMENTO", AppLib.Context.Usuario);
                }
                #endregion

                #region Mensagem Parametrizada

                List<String> lMensagem = new List<string>();

                if (goper.Get("HISTORICO") != null)
                {
                    lMensagem.Add(goper.Get("HISTORICO").ToString());
                }

                DataTable GOPERMENSAGEM = conn.ExecQuery("SELECT CODMENSAGEM FROM GOPERMENSAGEM WHERE CODEMPRESA = ? AND CODTIPOPER = ?", AppLib.Context.Empresa, codTipOper);

                if (GOPERMENSAGEM.Rows.Count > 0)
                {
                    for (int i = 0; i < GOPERMENSAGEM.Rows.Count; i++)
                    {

                        string sMensagem = conn.ExecGetField(string.Empty, "SELECT MENSAGEM FROM VOPERMENSAGEM WHERE CODEMPRESA = ? AND CODMENSAGEM = ?", AppLib.Context.Empresa, GOPERMENSAGEM.Rows[i]["CODMENSAGEM"].ToString()).ToString();

                        if (sMensagem != string.Empty)
                        {
                            lMensagem.Add(sMensagem);
                        }
                    }
                }
                string historico = string.Empty;
                if (lMensagem.Count > 0)
                {
                    for (int i = 0; i < lMensagem.Count; i++)
                    {
                        historico = historico + " " + lMensagem[i].ToString();
                    }
                }

                goper.Set("HISTORICO", historico);
                #endregion

                goper.Set("NFE", null);
                goper.Set("CHAVENFE", null);

                goper.Set("CODOPER", codoper);
                goper.Set("DATACRIACAO", conn.GetDateTime());
                goper.Set("CODTIPOPER", codTipOper);
                goper.Insert().ToString();

                #endregion

                #region GOPERCOMPL
                AppLib.ORM.Jit goperCompl = new AppLib.ORM.Jit(conn, "GOPERCOMPL");
                goperCompl.Set("CODEMPRESA", AppLib.Context.Empresa);
                goperCompl.Set("CODOPER", _codoper);
                goperCompl.Select();
                goperCompl.Set("CODOPER", goper.Get("CODOPER"));
                goperCompl.Save();
                #endregion

                //#region GOPERMENSAGEM
                //AppLib.ORM.Jit goperMensagem = new AppLib.ORM.Jit(conn, "GOPERMENSAGEM");
                //string codMensagem = conn.ExecGetField(string.Empty, "SELECT CODMENSAGEM FROM GOPERMENSAGEM WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] {AppLib.Context.Empresa, goper.Get("CODTIPOPER") }).ToString();
                //goperMensagem.Set("CODEMPRESA", AppLib.Context.Empresa);
                //goperMensagem.Set("CODMENSAGEM", codMensagem);
                //goperMensagem.Set("CODTIPOPER", goper.Get("CODTIPOPER"));
                //goperMensagem.Select();
                //goperMensagem.Set("CODOPER", goper.Get("CODOPER"));
                //goperMensagem.Save();
                //#endregion

                #region GOPERRATEIOCC
                if (goper.Get("CODCCUSTO") != null)
                {
                    AppLib.ORM.Jit goperRateioCC = new AppLib.ORM.Jit(conn, "GOPERRATEIOCC");
                    goperRateioCC.Set("CODEMPRESA", AppLib.Context.Empresa);
                    goperRateioCC.Set("CODOPER", _codoper);
                    goperRateioCC.Set("CODCCUSTO", goper.Get("CODCCUSTO"));
                    goperRateioCC.Select();
                    goperRateioCC.Set("CODOPER", goper.Get("CODOPER"));
                    goperRateioCC.Save();
                }
                #endregion

                #region GOPERRATEIODP
                AppLib.ORM.Jit goperRateioDP = new AppLib.ORM.Jit(conn, "GOPERRATEIODP");
                string codDepto = conn.ExecGetField(string.Empty, "SELECT CODDEPTO FROM GOPERRATEIODP WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { _codoper, AppLib.Context.Empresa }).ToString();
                goperRateioDP.Set("CODEMPRESA", AppLib.Context.Empresa);
                goperRateioDP.Set("CODOPER", _codoper);
                goperRateioDP.Set("CODFILIAL", goper.Get("CODFILIAL"));
                goperRateioDP.Set("CODDEPTO", codDepto);
                goperRateioDP.Select();
                if (goperRateioDP.Count() > 0)
                {
                    goperRateioDP.Set("CODOPER", goper.Get("CODOPER"));
                    goperRateioDP.Save();
                }
                #endregion

                #region GOPERITEM
                AppLib.ORM.Jit goperItem = new AppLib.ORM.Jit(conn, "GOPERITEM");
                DataTable dt = conn.ExecQuery(@"SELECT * FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codoper });
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goperItem.Set("CODEMPRESA", AppLib.Context.Empresa);
                    goperItem.Set("CODOPER", _codoper);
                    goperItem.Set("NSEQITEM", dt.Rows[i]["NSEQITEM"]);
                    goperItem.Select();
                    goperItem.Set("CODOPER", goper.Get("CODOPER"));
                    goperItem.Set("TIPODESCONTO", dt.Rows[i]["TIPODESCONTO"]);

                    goperItem.Set("QUANTIDADE_SALDO", Convert.ToDecimal(dt.Rows[i]["QUANTIDADE"]));
                    goperItem.Set("QUANTIDADE_FATURADO", 0);

                    #region Valor unitário
                    if (param_destino["USAVLUNITARIO"].ToString() == "M")
                    {
                        goperItem.Set("VLUNITARIO", 0);
                    }
                    #endregion

                    #region Valor Desconto
                    if (param_destino["USAVLDESCONTO"].ToString() == "M")
                    {
                        goperItem.Set("VLDESCONTO", 0);
                    }
                    #endregion

                    #region Perc. Desconto
                    if (param_destino["USAPRDESCONTO"].ToString() == "M")
                    {
                        goperItem.Set("PRDESCONTO", 0);
                    }
                    #endregion

                    #region Valor Total do Item
                    if (param_destino["USAVLTOTALITEM"].ToString() == "M")
                    {
                        goperItem.Set("VLTOTALITEM", 0);
                    }
                    #endregion

                    #region Natureza
                    if (param_destino["USANATUREZA"].ToString() == "M")
                    {
                        goperItem.Set("CODNATUREZA", null);
                    }
                    else
                    {
                        if (goperItem.Get("CODNATUREZA") == DBNull.Value)
                        {
                            string CODNATDENTRO = string.Empty;
                            string CODNATFORA = string.Empty;
                            if (param_destino["CODNATDENTRO"] == DBNull.Value)
                            {
                                CODNATDENTRO = null;
                            }
                            else
                            {
                                CODNATDENTRO = param_destino["CODNATDENTRO"].ToString();
                            }
                            if (param_destino["CODNATFORA"] == DBNull.Value)
                            {
                                CODNATFORA = null;
                            }
                            else
                            {
                                CODNATFORA = param_destino["CODNATFORA"].ToString();
                            }
                            goperItem.Set("CODNATUREZA", DefineNatureza(AppLib.Context.Empresa, Convert.ToInt32(codoper), CODNATDENTRO, CODNATFORA));
                        }
                    }
                    #endregion

                    goperItem.Insert();
                }
                #endregion

                #region GOPERITEMDIFAL
                AppLib.ORM.Jit goperItemDifal = new AppLib.ORM.Jit(conn, "GOPERITEMDIFAL");
                dt = new DataTable();
                dt = conn.ExecQuery(@"SELECT * FROM GOPERITEMDIFAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codoper });
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goperItemDifal.Set("CODEMPRESA", AppLib.Context.Empresa);
                    goperItemDifal.Set("CODOPER", _codoper);
                    goperItemDifal.Set("NSEQITEM", dt.Rows[i]["NSEQITEM"]);
                    goperItemDifal.Select();
                    goperItemDifal.Set("CODOPER", goper.Get("CODOPER"));
                    goperItemDifal.Insert();
                    goperItemDifal.Clear();
                }
                #endregion

                #region GOPERITEMCOMPL
                AppLib.ORM.Jit goperItemCompl = new AppLib.ORM.Jit(conn, "GOPERITEMCOMPL");
                dt = new DataTable();
                dt = conn.ExecQuery(@"SELECT * FROM GOPERITEMCOMPL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codoper });
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goperItemCompl.Set("CODEMPRESA", AppLib.Context.Empresa);
                    goperItemCompl.Set("CODOPER", _codoper);
                    goperItemCompl.Set("NSEQITEM", dt.Rows[i]["NSEQITEM"]);
                    goperItemCompl.Select();
                    goperItemCompl.Set("CODOPER", goper.Get("CODOPER"));
                    goperItemCompl.Insert();
                    goperItemCompl.Clear();
                }
                #endregion

                #region GOPERITEMRATEIOCC
                AppLib.ORM.Jit goperItemtRateioCC = new AppLib.ORM.Jit(conn, "GOPERITEMRATEIOCC");
                dt = new DataTable();
                dt = conn.ExecQuery(@"SELECT * FROM GOPERITEMRATEIOCC WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codoper });
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goperItemtRateioCC.Set("CODEMPRESA", AppLib.Context.Empresa);
                    goperItemtRateioCC.Set("CODOPER", _codoper);
                    goperItemtRateioCC.Set("NSEQITEM", dt.Rows[i]["NSEQITEM"]);
                    goperItemtRateioCC.Set("CODCCUSTO", dt.Rows[i]["CODCCUSTO"]);
                    goperItemtRateioCC.Select();
                    goperItemtRateioCC.Set("CODOPER", goper.Get("CODOPER"));
                    goperItemtRateioCC.Insert();
                    goperItemtRateioCC.Clear();
                }
                #endregion

                #region GOPERITEMRATEIODP
                AppLib.ORM.Jit goperItemtRateioDP = new AppLib.ORM.Jit(conn, "GOPERITEMRATEIODP");
                dt = new DataTable();
                dt = conn.ExecQuery(@"SELECT * FROM GOPERITEMRATEIOCC WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codoper });
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goperItemtRateioDP.Set("CODEMPRESA", AppLib.Context.Empresa);
                    goperItemtRateioDP.Set("CODOPER", _codoper);
                    goperItemtRateioDP.Set("NSEQITEM", dt.Rows[i]["NSEQITEM"]);
                    goperItemtRateioDP.Set("CODFILIAL", dt.Rows[i]["CODFILIAL"]);
                    goperItemtRateioDP.Set("CODDEPTO", dt.Rows[i]["CODDEPTO"]);
                    goperItemtRateioDP.Select();
                    goperItemtRateioDP.Set("CODOPER", goper.Get("CODOPER"));
                    goperItemtRateioDP.Insert();
                    goperItemtRateioDP.Clear();
                }
                #endregion

                #region GOPERITEMRECURSO
                AppLib.ORM.Jit goperItemtRecurso = new AppLib.ORM.Jit(conn, "GOPERITEMRECURSO");
                dt = new DataTable();
                dt = conn.ExecQuery(@"SELECT * FROM GOPERITEMRECURSO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codoper });
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goperItemtRecurso.Set("CODEMPRESA", AppLib.Context.Empresa);
                    goperItemtRecurso.Set("CODOPER", _codoper);
                    goperItemtRecurso.Set("NSEQITEM", dt.Rows[i]["NSEQITEM"]);
                    goperItemtRecurso.Set("CODOPERADOR", dt.Rows[i]["CODOPERADOR"]);
                    goperItemtRecurso.Select();
                    goperItemtRecurso.Set("CODOPER", goper.Get("CODOPER"));
                    goperItemtRecurso.Insert();
                    goperItemtRecurso.Clear();
                }
                #endregion

                #region Atualiza Operação Origem

                conn.ExecTransaction(@"UPDATE GOPER SET CODSTATUS = 1, DATAFATURAMENTO = ?, CODUSUARIOFATURAMENTO = ? WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { conn.GetDateTime(), PS.Lib.Contexto.Session.CodUsuario, AppLib.Context.Empresa, _codoper });
                #endregion

                #region Relacionamento
                conn.ExecTransaction(@"INSERT INTO GOPERRELAC (CODEMPRESA, CODOPER, CODOPERRELAC) VALUES (?,?,?)", new object[] { AppLib.Context.Empresa, _codoper, codoper });

                // João Pedro Luchiari - 15/05/2018 - 16:21
                GoperItemRelac(conn, Convert.ToInt32(_codoper));

                #endregion

                #region Exclui Financeiro
                if (excluiFinanceiro(conn, Convert.ToInt32(_codoper)) == false)
                {
                    conn.Rollback();
                    return string.Empty;
                }
                #endregion

                #region ALTERA A GLOG
                AppLib.ORM.Jit GLOG = new AppLib.ORM.Jit(conn, "GLOG");
                GLOG.Set("CODEMPRESA", AppLib.Context.Empresa);
                GLOG.Set("CODTABELA", "GOPER");
                GLOG.Set("IDLOG", codoper);
                GLOG.Save();
                #endregion

                #region ALTERA VSERIE
                if (param_destino["OPERSERIE"].ToString() != "E")
                {
                    AppLib.ORM.Jit VSERIE = new AppLib.ORM.Jit(conn, "VSERIE");
                    VSERIE.Set("CODEMPRESA", AppLib.Context.Empresa);
                    VSERIE.Set("CODFILIAL", codFilial);
                    VSERIE.Set("CODSERIE", goper.Get("CODSERIE"));
                    VSERIE.Set("NUMSEQ", goper.Get("NUMERO"));
                    VSERIE.Save();
                }
                #endregion

                //conn.Commit();

                #region Estoque

                try
                {
                    if (param_destino["OPERESTOQUE"].ToString() != "N")
                    {
                        PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                        psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
                        psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };
                        psPartLocalEstoqueSaldoData.MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.OperacaoFaturamento, AppLib.Context.Empresa, Convert.ToInt32(goper.Get("CODOPER")), Convert.ToInt32(goperItem.Get("NSEQITEM")));
                    }

                    goperItem.Clear();
                }

                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    return string.Empty;
                }


                #endregion

                return codoper;
            }
            catch (Exception ex)
            {
                //conn.Rollback();
                return string.Empty;
            }
        }

        public bool excluiFinanceiro(AppLib.Data.Connection conn, int codOper)
        {
            try
            {
                conn.ExecTransaction("DELETE FROM FLANCA WHERE CODOPER = ? AND CODEMPRESA =?", new object[] { codOper, AppLib.Context.Empresa });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string DefineNatureza(int CodEmpresa, int CodOper, string CodNatDentro, string CodNatFora)
        {
            string UFEmitente = new PSPartOperacaoData().BuscaUFEmitente(CodEmpresa, CodOper);
            string UFDestinatario = new PSPartOperacaoData().BuscaUFDestinatario(CodEmpresa, CodOper);
            string CodNatureza = string.Empty;

            if (!string.IsNullOrEmpty(UFEmitente))
            {
                if (!string.IsNullOrEmpty(UFDestinatario))
                {
                    if (UFEmitente == UFDestinatario)
                    {
                        CodNatureza = CodNatDentro;
                    }
                    else
                    {
                        CodNatureza = CodNatFora;
                    }
                }
            }

            return CodNatureza;
        }

        private void GoperItemRelac(AppLib.Data.Connection conn, int Codoper)
        {
            DataTable dtItens = conn.ExecQuery(@"SELECT GOPERITEM.*, VPRODUTO.DESCRICAO, GOPER.NUMERO, VPRODUTO.CODIGOAUXILIAR
                                                FROM 
                                                GOPERITEM 
                                                INNER JOIN VPRODUTO ON GOPERITEM.CODPRODUTO  =VPRODUTO.CODPRODUTO AND GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA
                                                INNER JOIN GOPER ON GOPERITEM.CODOPER = GOPER.CODOPER AND GOPERITEM.CODEMPRESA = GOPER.CODEMPRESA WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODOPER = ? AND GOPERITEM.QUANTIDADE_SALDO > 0", new object[] { AppLib.Context.Empresa, Codoper });

            for (int i = 0; i < dtItens.Rows.Count; i++)
            {
                decimal qtd = Convert.ToDecimal(conn.ExecGetField(0, "SELECT QUANTIDADE_FATURADO FROM GOPERITEM WHERE CODOPER = ?  AND CODEMPRESA = ? AND NSEQITEM = ?", new object[] { dtItens.Rows[i]["CODOPER"], AppLib.Context.Empresa, dtItens.Rows[i]["NSEQITEM"] }));
                //Altera a quantidade dos itens de origem
                conn.ExecQuery(@"UPDATE GOPERITEM SET QUANTIDADE_SALDO = ?, QUANTIDADE_FATURADO = ? WHERE CODOPER = ? AND NSEQITEM = ? AND CODEMPRESA = ?", new object[] { dtItens.Rows[i]["QUANTIDADE_SALDO"], Convert.ToDecimal(dtItens.Rows[i]["QUANTIDADE"]) + qtd, dtItens.Rows[i]["CODOPER"], dtItens.Rows[i]["NSEQITEM"], AppLib.Context.Empresa });
                //Altera o Status da operação de origem
                conn.ExecQuery(@"UPDATE GOPER SET CODSTATUS = ? WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { "1", dtItens.Rows[i]["CODOPER"], AppLib.Context.Empresa });

                //Cria o relacionamento item //
                conn.ExecQuery(@"INSERT INTO GOPERITEMRELAC (CODOPERITEMORIGEM, NSEQITEMORIGEM, CODOPERITEMDESTINO, NSEQITEMDESTINO) VALUES (?, ?, ?, ?)", new object[] { dtItens.Rows[i]["CODOPER"], dtItens.Rows[i]["NSEQITEM"], codoper, i + 1 });
            }
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GOPERITEM SET QUANTIDADE_FATURADO = QUANTIDADE, QUANTIDADE_SALDO = 0 WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper });
        }
    }
}
