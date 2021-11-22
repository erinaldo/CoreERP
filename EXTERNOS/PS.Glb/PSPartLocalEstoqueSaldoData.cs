using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartLocalEstoqueSaldoData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Glb.Formula.Function function = new PS.Glb.Formula.Function();
        private PS.Lib.Interpretador interpreta = new PS.Lib.Interpretador();

        public enum Tipo
        {
            Aumenta,
            Diminui
        }

        public void MovimentaEstoque(int CODEMPRESA, int CODFILIAL, string CODLOCAL, string CODLOCALENTREGA, string CODPRODUTO, decimal QUANTIDADE, string CODUNID, Tipo TIPO)
        {
            decimal EST_FATORCONVERSAO;
            string EST_CODUNIDCONTROLE;
            string sSql;

            if (string.IsNullOrEmpty(CODLOCAL))
                throw new Exception("Local de Estoque não encontrado.");

            if (string.IsNullOrEmpty(CODUNID))
                throw new Exception("Unidade de Medida não encontrada.");

            sSql = @"SELECT CODUNIDCONTROLE FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?";
            EST_CODUNIDCONTROLE = dbs.QueryValue(string.Empty, sSql, CODEMPRESA, CODPRODUTO).ToString();

            if (string.IsNullOrEmpty(EST_CODUNIDCONTROLE))
                throw new Exception("Produto não possui unidade de controle cadastrada.");

            if (EST_CODUNIDCONTROLE != CODUNID)
            {
                sSql = @"SELECT FATORCONVERSAO FROM VUNID WHERE CODEMPRESA = ? AND CODUNID = ? AND CODUNIDBASE = ?";

                EST_FATORCONVERSAO = Convert.ToDecimal(dbs.QueryValue(0, sSql, CODEMPRESA, CODUNID, EST_CODUNIDCONTROLE).ToString());

                if (EST_FATORCONVERSAO == 0)
                    throw new Exception("Fator de Conversão não definido para a unidade de controle.");

                QUANTIDADE = QUANTIDADE * EST_FATORCONVERSAO;
            }

            if (string.IsNullOrEmpty(CODLOCALENTREGA))
            {
                //Se não informado o Local de Entrega

                sSql = @"SELECT 1 FROM VLOCALESTOQUESALDO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODPRODUTO = ?";

                if (!dbs.QueryFind(sSql, CODEMPRESA, CODFILIAL, CODLOCAL, CODPRODUTO))
                {
                    sSql = "INSERT INTO VLOCALESTOQUESALDO(CODEMPRESA, CODFILIAL, CODLOCAL, CODPRODUTO, SALDO) VALUES(?,?,?,?,?)";

                    dbs.QueryExec(sSql, CODEMPRESA, CODFILIAL, CODLOCAL, CODPRODUTO, 0);
                }

                if (TIPO == Tipo.Aumenta)
                {
                    sSql = @"UPDATE VLOCALESTOQUESALDO SET SALDO = SALDO + ? WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODPRODUTO = ? ";

                    dbs.QueryExec(sSql, QUANTIDADE, CODEMPRESA, CODFILIAL, CODLOCAL, CODPRODUTO);
                }

                if (TIPO == Tipo.Diminui)
                {
                    sSql = @"UPDATE VLOCALESTOQUESALDO SET SALDO = SALDO - ? WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODPRODUTO = ? ";

                    dbs.QueryExec(sSql, QUANTIDADE, CODEMPRESA, CODFILIAL, CODLOCAL, CODPRODUTO);
                }
            }
            else
            {
                //Se informado o Local de Entrega
                sSql = @"SELECT 1 FROM VLOCALESTOQUESALDO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODPRODUTO = ?";

                //Cria o registro na tabela de saldo para o Local
                if (!dbs.QueryFind(sSql, CODEMPRESA, CODFILIAL, CODLOCAL, CODPRODUTO))
                {
                    sSql = "INSERT INTO VLOCALESTOQUESALDO(CODEMPRESA, CODFILIAL, CODLOCAL, CODPRODUTO, SALDO) VALUES(?,?,?,?,?)";

                    dbs.QueryExec(sSql, CODEMPRESA, CODFILIAL, CODLOCAL, CODPRODUTO, 0);
                }

                sSql = @"SELECT 1 FROM VLOCALESTOQUESALDO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODPRODUTO = ?";

                //Cria o registro na tabela de saldo para o Local de Entrega
                if (!dbs.QueryFind(sSql, CODEMPRESA, CODFILIAL, CODLOCALENTREGA, CODPRODUTO))
                {
                    sSql = "INSERT INTO VLOCALESTOQUESALDO(CODEMPRESA, CODFILIAL, CODLOCAL, CODPRODUTO, SALDO) VALUES(?,?,?,?,?)";

                    dbs.QueryExec(sSql, CODEMPRESA, CODFILIAL, CODLOCALENTREGA, CODPRODUTO, 0);
                }

                //Diminui do Local
                sSql = @"UPDATE VLOCALESTOQUESALDO SET SALDO = SALDO - ? WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODPRODUTO = ? ";

                dbs.QueryExec(sSql, QUANTIDADE, CODEMPRESA, CODFILIAL, CODLOCAL, CODPRODUTO);

                //Aumenta do Local de Entrega
                sSql = @"UPDATE VLOCALESTOQUESALDO SET SALDO = SALDO + ? WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODPRODUTO = ? ";

                dbs.QueryExec(sSql, QUANTIDADE, CODEMPRESA, CODFILIAL, CODLOCALENTREGA, CODPRODUTO);
            }
        }

        /// <summary>
        /// Realiza as 2 movimentações de estoques origem / destino
        /// </summary>
        /// <param name="AcaoMovimentouEstoque">Ação que gerou a movimentação do estoque</param>
        /// <param name="CODEMPRESA">Código da Empresa</param>
        /// <param name="CODOPER">Código da Operação</param>
        /// <param name="NSEQITEM">Número sequencial do item (Informar zero quando for a operação como um todo)</param>
        public void MovimentaEstoque2(AcaoMovimentouEstoque AcaoMovimentouEstoque, int CODEMPRESA, int CODOPER, int NSEQITEM)
        {
            this.MovimentaEstoque2(AcaoMovimentouEstoque, OrigemDestino.Origem, CODEMPRESA, CODOPER, NSEQITEM);
            this.MovimentaEstoque2(AcaoMovimentouEstoque, OrigemDestino.Destino, CODEMPRESA, CODOPER, NSEQITEM);
        }

        private void MovimentaEstoque2(AcaoMovimentouEstoque AcaoMovimentouEstoque, OrigemDestino OrigemDestino, int CODEMPRESA, int CODOPER, int NSEQITEM)
        {
            #region CARREGA OS CAMPOS PADRÕES

            System.Data.DataTable dtGOPER = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new Object[] { CODEMPRESA, CODOPER });

            int CODFILIAL = Convert.ToInt32(dtGOPER.Rows[0]["CODFILIAL"]);

            int? CODFILIALENTREGA = null;
            if (dtGOPER.Rows[0]["CODFILIALENTREGA"] != DBNull.Value)
                CODFILIALENTREGA = Convert.ToInt32(dtGOPER.Rows[0]["CODFILIALENTREGA"]);

            if (OrigemDestino == PSPartLocalEstoqueSaldoData.OrigemDestino.Destino)
                if (CODFILIALENTREGA == null)
                    return;

            String CODLOCAL = dtGOPER.Rows[0]["CODLOCAL"].ToString();

            String CODLOCALENTREGA = String.Empty;
            if (dtGOPER.Rows[0]["CODLOCALENTREGA"] != DBNull.Value)
                CODLOCALENTREGA = dtGOPER.Rows[0]["CODLOCALENTREGA"].ToString();

            DateTime DATAENTSAI = Convert.ToDateTime(dtGOPER.Rows[0]["DATAEMISSAO"]);

            String CODTIPOPER = dtGOPER.Rows[0]["CODTIPOPER"].ToString();
            System.Data.DataTable dtGTIPOPER = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new Object[] { CODEMPRESA, CODTIPOPER });

            String OPERESTOQUE = "N";
            if (OrigemDestino == PSPartLocalEstoqueSaldoData.OrigemDestino.Origem)
                OPERESTOQUE = dtGTIPOPER.Rows[0]["OPERESTOQUE"].ToString();
            else
                OPERESTOQUE = dtGTIPOPER.Rows[0]["OPERESTOQUE2"].ToString();

            if (OPERESTOQUE.ToUpper().Equals("N"))
                return;

            //String FRMCUSTOUNITARIO = String.Empty;
            //if (dtGTIPOPER.Rows[0]["FRMCUSTOUNITARIO"] == DBNull.Value)
            //    throw new Exception("Não existe fórmula de custo unitário parametrizada no tipo de operação " + CODTIPOPER);
            //else
            //    FRMCUSTOUNITARIO = dtGTIPOPER.Rows[0]["FRMCUSTOUNITARIO"].ToString();

            // João Pedro Luchiari - 19/01/2018
            String FORMULAVLUNITARIO = String.Empty;
            if (dtGTIPOPER.Rows[0]["FORMULAVlUNITARIO"] == DBNull.Value)
                throw new Exception("Não existe fórmula de custo unitário parametrizada no tipo de operação " + CODTIPOPER);
            else
                FORMULAVLUNITARIO = dtGTIPOPER.Rows[0]["FORMULAVLUNITARIO"].ToString();

            #endregion

            if (AcaoMovimentouEstoque == PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao)
            {
                #region CARREGA OS CAMPOS

                System.Data.DataTable dtGOPERITEM = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?", new object[] { CODEMPRESA, CODOPER, NSEQITEM });
                String CODPRODUTO = dtGOPERITEM.Rows[0]["CODPRODUTO"].ToString();
                String CODUNIDOPER = dtGOPERITEM.Rows[0]["CODUNIDOPER"].ToString();

                System.Data.DataTable dtVPRODUTO = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?", new Object[] { CODEMPRESA, CODPRODUTO });
                String CODUNIDCONTROLE = dtVPRODUTO.Rows[0]["CODUNIDCONTROLE"].ToString();

                // João Pedro Luchiari - 19/01/2018 - Alteração do cálculo de unidade de conversão.
                //Decimal FATORCONVERSAO = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(1, "SELECT FATORCONVERSAO FROM VUNID WHERE CODEMPRESA = ? AND CODUNID = ? AND CODUNIDBASE = ?", new Object[] { CODEMPRESA, CODUNIDOPER, CODUNIDCONTROLE }));

                Decimal FATORCONVERSAO = Convert.ToDecimal(dtGOPERITEM.Rows[0]["FATORCONVERSAO"]);

                System.Data.DataTable dtVSALDOESTOQUE = null;

                if (OrigemDestino == PSPartLocalEstoqueSaldoData.OrigemDestino.Origem)
                    dtVSALDOESTOQUE = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT  * FROM VFICHAESTOQUE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODPRODUTO = ? ORDER BY SEQUENCIAL DESC", new Object[] { CODEMPRESA, CODFILIAL, CODLOCAL, CODPRODUTO });
                else
                    dtVSALDOESTOQUE = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VFICHAESTOQUE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODPRODUTO = ? ORDER BY SEQUENCIAL DESC", new Object[] { CODEMPRESA, CODFILIALENTREGA, CODLOCALENTREGA, CODPRODUTO });

               // int ULTIMO_SEQUENCIAL = 0;
                int PROXIMO_SEQUENCIAL = 1;
                decimal SALDOANTERIOR = 0;
                decimal TOTALANTERIOR = 0;
                decimal CUSTOMEDIOANTERIOR = 0;

                System.Data.DataTable dtRegistro = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VFICHAESTOQUE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODPRODUTO = ? AND CODOPER = ? AND NSEQITEM = ? ORDER BY SEQUENCIAL DESC", new object[] { CODEMPRESA, CODFILIAL, CODLOCAL, CODPRODUTO, CODOPER, NSEQITEM });
                //Atual
                if (dtRegistro.Rows.Count > 0)
                {
                    PROXIMO_SEQUENCIAL = Convert.ToInt32(dtRegistro.Rows[0]["SEQUENCIAL"]);
                    SALDOANTERIOR = Convert.ToDecimal(dtRegistro.Rows[0]["SALDOANTERIOR"]);
                    TOTALANTERIOR = Convert.ToDecimal(dtRegistro.Rows[0]["TOTALANTERIOR"]);
                    CUSTOMEDIOANTERIOR = Convert.ToDecimal(dtRegistro.Rows[0]["CUSTOMEDIOANTERIOR"]);
                }
                //Anterior
                else
                {
                    if (dtVSALDOESTOQUE.Rows.Count > 0)
                    {
                        PROXIMO_SEQUENCIAL = Convert.ToInt32(dtVSALDOESTOQUE.Rows[0]["SEQUENCIAL"]) + 1;
                        SALDOANTERIOR = Convert.ToDecimal(dtVSALDOESTOQUE.Rows[0]["SALDOFINAL"]);
                        TOTALANTERIOR = Convert.ToDecimal(dtVSALDOESTOQUE.Rows[0]["TOTALFINAL"]);
                        CUSTOMEDIOANTERIOR = Convert.ToDecimal(dtVSALDOESTOQUE.Rows[0]["CUSTOMEDIO"]);
                    }
                }

                //if (dtVSALDOESTOQUE.Rows.Count > 0)
                //    ULTIMO_SEQUENCIAL = Convert.ToInt32(dtVSALDOESTOQUE.Rows[0]["SEQUENCIAL"]);

                //int PROXIMO_SEQUENCIAL = (ULTIMO_SEQUENCIAL + 1);

                //if (dtVSALDOESTOQUE.Rows.Count > 0)
                //    SALDOANTERIOR = Convert.ToDecimal(dtVSALDOESTOQUE.Rows[0]["SALDOFINAL"]);

                //if (dtVSALDOESTOQUE.Rows.Count > 0)
                //    TOTALANTERIOR = Convert.ToDecimal(dtVSALDOESTOQUE.Rows[0]["TOTALFINAL"]);

                //if (dtVSALDOESTOQUE.Rows.Count > 0)
                //    CUSTOMEDIOANTERIOR = Convert.ToDecimal(dtVSALDOESTOQUE.Rows[0]["CUSTOMEDIO"]);

                EntradaSaida EntradaSaida;
                if (OPERESTOQUE.ToUpper().Equals("A"))
                    EntradaSaida = PSPartLocalEstoqueSaldoData.EntradaSaida.Entrada;
                else
                    EntradaSaida = PSPartLocalEstoqueSaldoData.EntradaSaida.Saida;

                AppLib.ORM.Jit VFICHAESTOQUE = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "VFICHAESTOQUE");

                #endregion

                #region REGRA

                VFICHAESTOQUE.Set("CODEMPRESA", CODEMPRESA);

                if (OrigemDestino == PSPartLocalEstoqueSaldoData.OrigemDestino.Origem)
                    VFICHAESTOQUE.Set("CODFILIAL", CODFILIAL);
                else
                    VFICHAESTOQUE.Set("CODFILIAL", CODFILIALENTREGA);

                if (OrigemDestino == PSPartLocalEstoqueSaldoData.OrigemDestino.Origem)
                    VFICHAESTOQUE.Set("CODLOCAL", CODLOCAL);
                else
                    VFICHAESTOQUE.Set("CODLOCAL", CODLOCALENTREGA);
                                
                VFICHAESTOQUE.Set("CODPRODUTO", CODPRODUTO);
                VFICHAESTOQUE.Set("SEQUENCIAL", PROXIMO_SEQUENCIAL);
                VFICHAESTOQUE.Set("CODOPER", CODOPER);
                VFICHAESTOQUE.Set("DATAENTSAI", DATAENTSAI);
                VFICHAESTOQUE.Set("NSEQITEM", NSEQITEM);
                VFICHAESTOQUE.Set("CODUNIDCONTROLE", CODUNIDCONTROLE);
                VFICHAESTOQUE.Set("SALDOANTERIOR", SALDOANTERIOR);
                VFICHAESTOQUE.Set("TOTALANTERIOR", TOTALANTERIOR);

                decimal QUANTIDADEENTRADA = 0;
                if (EntradaSaida == PSPartLocalEstoqueSaldoData.EntradaSaida.Entrada)
                {
                    QUANTIDADEENTRADA = Convert.ToDecimal(dtGOPERITEM.Rows[0]["QUANTIDADE"]);
                    QUANTIDADEENTRADA = (QUANTIDADEENTRADA * FATORCONVERSAO);
                }

                decimal TOTALENTRADA = 0;
                if (EntradaSaida == PSPartLocalEstoqueSaldoData.EntradaSaida.Entrada)
                {
                    TOTALENTRADA = Convert.ToDecimal(dtGOPERITEM.Rows[0]["VLTOTALITEM"]);
                }

                decimal QUANTIDADESAIDA = 0;
                if (EntradaSaida == PSPartLocalEstoqueSaldoData.EntradaSaida.Saida)
                {
                    QUANTIDADESAIDA = Convert.ToDecimal(dtGOPERITEM.Rows[0]["QUANTIDADE"]);
                    QUANTIDADESAIDA = (QUANTIDADESAIDA * FATORCONVERSAO);
                }

                decimal TOTALSAIDA = 0;
                if (EntradaSaida == PSPartLocalEstoqueSaldoData.EntradaSaida.Saida)
                {
                    TOTALSAIDA = Convert.ToDecimal(dtGOPERITEM.Rows[0]["VLTOTALITEM"]);
                }

                VFICHAESTOQUE.Set("QUANTIDADEENTRADA", QUANTIDADEENTRADA);
                VFICHAESTOQUE.Set("TOTALENTRADA", TOTALENTRADA);
                VFICHAESTOQUE.Set("QUANTIDADESAIDA", QUANTIDADESAIDA);
                VFICHAESTOQUE.Set("TOTALSAIDA", TOTALSAIDA);
                VFICHAESTOQUE.Set("CUSTOMEDIOANTERIOR", CUSTOMEDIOANTERIOR);

                decimal ResultPrecoUnit = Convert.ToDecimal(dtGOPERITEM.Rows[0]["VLUNITARIO"]) / FATORCONVERSAO;

                VFICHAESTOQUE.Set("PRECOUNITARIO", ResultPrecoUnit);
                decimal CUSTOUNITARIO = 0;
                if (EntradaSaida == PSPartLocalEstoqueSaldoData.EntradaSaida.Entrada)
                {
                    try
                    {
                        //interpreta.comando = function.PreparaFormula(AppLib.Context.Empresa, FRMCUSTOUNITARIO);

                        //PS.Lib.Contexto.Session.key1 = CODEMPRESA;
                        //PS.Lib.Contexto.Session.key2 = CODOPER;
                        //PS.Lib.Contexto.Session.key3 = NSEQITEM;
                        //CUSTOUNITARIO = Convert.ToDecimal(interpreta.Executar().ToString());
                        //CUSTOUNITARIO = CUSTOUNITARIO / FATORCONVERSAO; 

                        string Codquery = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT FORMULAVLUNITARIO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER= ?", new object[] { AppLib.Context.Empresa, CODTIPOPER }).ToString();
                        string Formula = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT QUERY FROM GQUERY WHERE CODQUERY = ?", new object[] { Codquery }).ToString();

                        Formula = Formula.Replace("@CODEMPRESA", "'" + AppLib.Context.Empresa + "'");
                        Formula = Formula.Replace("@CODOPER", "'" + CODOPER + "'");
                        Formula = Formula.Replace("@NSEQITEM", "'" + NSEQITEM + "'");

                        string FormulaFormatada = Formula;

                        decimal NewCustoUnitario = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0,FormulaFormatada));

                        PS.Lib.Contexto.Session.key1 = CODEMPRESA;
                        PS.Lib.Contexto.Session.key2 = CODOPER;
                        PS.Lib.Contexto.Session.key3 = NSEQITEM;
                        CUSTOUNITARIO = NewCustoUnitario / FATORCONVERSAO;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao executar a fórmula (" + FORMULAVLUNITARIO+ ") de custo unitário.\r\nDetalhe técnico: " + ex.Message);
                    }
                }
                else
                {
                    CUSTOUNITARIO = Convert.ToDecimal(VFICHAESTOQUE.Get("CUSTOMEDIOANTERIOR"));
                }
                
                VFICHAESTOQUE.Set("CUSTOUNITARIO", CUSTOUNITARIO);

                decimal CUSTOMEDIO = 0;
                if (EntradaSaida == PSPartLocalEstoqueSaldoData.EntradaSaida.Entrada)
                {
                    if (SALDOANTERIOR == 0)
                    {
                        CUSTOMEDIO = CUSTOUNITARIO;
                    }
                    else
                    {
                        if (SALDOANTERIOR + QUANTIDADEENTRADA != 0)
                        {
                            CUSTOMEDIO = (((TOTALANTERIOR + TOTALENTRADA) - TOTALSAIDA) / ((SALDOANTERIOR + QUANTIDADEENTRADA) - QUANTIDADESAIDA));    
                        }
                        else
                        {
                            CUSTOMEDIO = CUSTOUNITARIO; 
                        }
                        
                    }
                }
                else
                {
                    CUSTOMEDIO = Convert.ToDecimal(VFICHAESTOQUE.Get("CUSTOMEDIOANTERIOR"));
                }

                VFICHAESTOQUE.Set("CUSTOMEDIO", CUSTOMEDIO);

                if (VFICHAESTOQUE.Save() == 1)
                {
                    // ok
                }
                else
                {
                    throw new Exception("Erro ao inserir registro de ficha de estoque da operação " + CODOPER + " item " + NSEQITEM + ".");
                }

                #endregion
            }

            if (AcaoMovimentouEstoque == PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemExclusao)
            {
                #region CARREGA OS CAMPOS

                System.Data.DataTable dtGOPERITEM = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?", new Object[] { CODEMPRESA, CODOPER, NSEQITEM });
                String CODPRODUTO = dtGOPERITEM.Rows[0]["CODPRODUTO"].ToString();

                #endregion

                #region REGRA

                String consultaBackup = @"INSERT VFICHAESTOQUE_LOG (CODEMPRESA, CODFILIAL, CODLOCAL, CODPRODUTO, SEQUENCIAL, CODOPER, DATAENTSAI, NSEQITEM, CODUNIDCONTROLE, SALDOANTERIOR, TOTALANTERIOR, QUANTIDADEENTRADA, TOTALENTRADA, QUANTIDADESAIDA, TOTALSAIDA, QUANTIDADECOMSINAL, SALDOFINAL, TOTALFINAL, CUSTOMEDIOANTERIOR, CUSTOMEDIO, PRECOUNITARIO)
SELECT CODEMPRESA, CODFILIAL, CODLOCAL, CODPRODUTO, SEQUENCIAL, CODOPER, DATAENTSAI, NSEQITEM, CODUNIDCONTROLE, SALDOANTERIOR, TOTALANTERIOR, QUANTIDADEENTRADA, TOTALENTRADA, QUANTIDADESAIDA, TOTALSAIDA, QUANTIDADECOMSINAL, SALDOFINAL, TOTALFINAL, CUSTOMEDIOANTERIOR, CUSTOMEDIO, PRECOUNITARIO
FROM VFICHAESTOQUE
WHERE CODEMPRESA = ?
  AND CODOPER = ?
  AND NSEQITEM = ?";

                int tempBackup = AppLib.Context.poolConnection.Get("Start").ExecTransaction(consultaBackup, new Object[] { CODEMPRESA, CODOPER, NSEQITEM });
                string aa = AppLib.Context.poolConnection.Get("Start").ParseCommand(consultaBackup, new Object[] { CODEMPRESA, CODOPER, NSEQITEM });
                String comandoDelete = "DELETE VFICHAESTOQUE WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";

                if (AppLib.Context.poolConnection.Get("Start").ExecTransaction(comandoDelete, new Object[] { CODEMPRESA, CODOPER, NSEQITEM }) >= 0)
                {
                    // ok
                }
                else
                {
                    throw new Exception("Erro ao excluir ficha de estoque do item.");
                }
                
                #endregion
            }

            if (AcaoMovimentouEstoque == PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.OperacaoFaturamento)
            {
                #region LOOP

                System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?", new Object[] { CODEMPRESA, CODOPER });

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NSEQITEM = int.Parse(dt.Rows[i]["NSEQITEM"].ToString());
                    MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao, CODEMPRESA, CODOPER, NSEQITEM);
                }

                #endregion
            }

            if (AcaoMovimentouEstoque == PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.OperacaoCancelamento)
            {
                #region LOOP

                System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?", new Object[] { CODEMPRESA, CODOPER });

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NSEQITEM = int.Parse(dt.Rows[i]["NSEQITEM"].ToString());
                    MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemExclusao, CODEMPRESA, CODOPER, NSEQITEM);
                }
                
                #endregion
            }

            if (AcaoMovimentouEstoque == PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.OperacaoExclusao)
            {
                #region LOOP

                System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?", new Object[] { CODEMPRESA, CODOPER });

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NSEQITEM = int.Parse(dt.Rows[i]["NSEQITEM"].ToString());
                    MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemExclusao, CODEMPRESA, CODOPER, NSEQITEM);
                }

                #endregion
            }
        }

        public enum AcaoMovimentouEstoque { ItemInclusao, ItemExclusao, OperacaoFaturamento, OperacaoCancelamento, OperacaoExclusao }

        public enum OrigemDestino { Origem, Destino }

        public enum EntradaSaida { Entrada, Saida }
        
    }
}
