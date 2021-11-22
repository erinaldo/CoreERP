using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace PS.Glb
{
    public class PSPartOperacaoData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Glb.Formula.Function function = new PS.Glb.Formula.Function();
        private PS.Lib.Interpretador interpreta = new PS.Lib.Interpretador();
        public bool copia = false, item = false;

        public override string ReadView()
        {
            return @"SELECT GOPER.CODEMPRESA,
CODFILIAL,
CODFILIALENTREGA,
CODFILIAL CCODFILIAL,
GOPER.CODOPER,
GOPER.CODTIPOPER,
(SELECT GNFESTADUAL.CODSTATUS FROM GNFESTADUAL WHERE GNFESTADUAL.CODEMPRESA = GOPER.CODEMPRESA AND GNFESTADUAL.CODOPER = GOPER.CODOPER) STATUSNFE,
NUMERO,
CODCLIFOR,
(SELECT NOMEFANTASIA FROM VCLIFOR WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODCLIFOR = GOPER.CODCLIFOR) CNOMECLIFOR,
CODSERIE,
CODNATUREZA,
CODLOCAL,
CODLOCALENTREGA,
CODOBJETO,
CODOPERADOR,
GOPER.CODSTATUS,
DATACRIACAO,
DATAEMISSAO,
DATAENTSAI,
FRETECIFFOB,
CODTRANSPORTADORA,
CODCONDICAO,
CODFORMA,
VALORBRUTO,
VALORLIQUIDO,
VALORFRETE,
PERCFRETE,
VALORDESCONTO,
PERCDESCONTO,
VALORDESPESA,
PERCDESPESA,
VALORSEGURO,
PERCSEGURO,
CODUSUARIOCRIACAO,
DATAENTREGA,
QUANTIDADE,
PESOLIQUIDO,
PESOBRUTO,
ESPECIE,
MARCA,
CODCONTA,
CODREPRE,
CODCCUSTO,
CODNATUREZAORCAMENTO,
DATAFATURAMENTO,
CODUSUARIOFATURAMENTO,
CAMPOLIVRE1,
CAMPOLIVRE2,
CAMPOLIVRE3,
CAMPOLIVRE4,
CAMPOLIVRE5,
CAMPOLIVRE6,
DATAEXTRA1,
DATAEXTRA2,
DATAEXTRA3,
DATAEXTRA4,
DATAEXTRA5,
DATAEXTRA6,
CAMPOVALOR1,
CAMPOVALOR2,
CAMPOVALOR3,
CAMPOVALOR4,
CAMPOVALOR5,
CAMPOVALOR6,
HISTORICO,
OBSERVACAO,
PLACA,
UFPLACA,
PRCOMISSAO,
NFE,
CHAVENFE,
TIPOPERCONSFIN,
CODVENDEDOR,
ORDEMDECOMPRA,
CODTIPOTRANSPORTE,
CLIENTERETIRA,
LIMITEDESC,
TIPOBLOQUEIODESC,
CODCONTATO
FROM GOPER
WHERE";
        }

        public void InsertRegistroComplementar(List<PS.Lib.DataField> objArr)
        {
            PSPartOperComplData psPartOperComplData = new PSPartOperComplData();
            psPartOperComplData._tablename = "GOPERCOMPL";
            psPartOperComplData._keys = new string[] { "CODEMPRESA", "CODOPER" };

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
            ListObjArr.Add(dfCODEMPRESA);
            ListObjArr.Add(dfCODOPER);

            psPartOperComplData.SaveRecord(ListObjArr);
        }

        public void ExcluiRegistroComplementar(List<PS.Lib.DataField> objArr)
        {
            PSPartOperComplData psPartOperComplData = new PSPartOperComplData();
            psPartOperComplData._tablename = "GOPERCOMPL";
            psPartOperComplData._keys = new string[] { "CODEMPRESA", "CODOPER" };

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
            ListObjArr.Add(dfCODEMPRESA);
            ListObjArr.Add(dfCODOPER);

            psPartOperComplData.DeleteRecordOper(ListObjArr);
        }

        public string BuscaUFEmitente(int CodEmpresa, int CodOper)
        {
            string UF = string.Empty;
            try
            {
                string sSql = @"SELECT CODFILIAL FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";
                int CodFilial = Convert.ToInt32(dbs.QueryValue(0, sSql, CodEmpresa, CodOper));

                sSql = @"SELECT CODETD FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?";
                UF = dbs.QueryValue(string.Empty, sSql, CodEmpresa, CodFilial).ToString();
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }

            return UF;
        }

        public string BuscaUFDestinatario(int CodEmpresa, int CodOper)
        {
            string UF = string.Empty;
            try
            {
                string sSql = @"SELECT CODCLIFOR FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";
                string CodCliFor = dbs.QueryValue(string.Empty, sSql, CodEmpresa, CodOper).ToString();

                sSql = @"SELECT CODETD FROM VCLIFOR WHERE CODEMPRESA = ? AND CODCLIFOR = ?";
                UF = dbs.QueryValue(string.Empty, sSql, CodEmpresa, CodCliFor).ToString();
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }

            return UF;
        }

        public string DefineNatureza(int CodEmpresa, int CodOper, string CodNatDentro, string CodNatFora)
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

        public string GeraNumeroDocumento(string tipoper)
        {
            string novoNumStr = "";
            if (string.IsNullOrEmpty(tipoper))
            {
                throw new Exception("Não foi possível gerar o número da operação pois o tipo da operação não foi informado.");
            }

            System.Data.DataRow PARAMTIPOPER = gb.RetornaParametrosOperacao(tipoper);
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
                        string sql = @"SELECT NUMSEQ FROM VSERIE WHERE CODSERIE = ?";
                        int ultimo = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, sql, new object[] { PARAMTIPOPER["SERIEDEFAULT"] }));

                        //
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
                    return novoNumStr;
                }
            }
        }

        public void CancelaOperacao(List<PS.Lib.DataField> objArr)
        {
            _tablename = "GOPER";
            _keys = new string[] { "CODEMPRESA", "CODOPER" };
            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            DataRow GOPER = this.ReadRecordEdit(dtCODEMPRESA.Valor, dtCODOPER.Valor).Rows[0];
            List<PS.Lib.DataField> objArrOper = gb.RetornaDataFieldByDataRow(GOPER);

            objArrOper[0].Field = "GOPER.CODEMPRESA";
            objArrOper[1].Field = "GOPER.CODOPER";
            objArrOper[2].Field = "GOPER.CODTIPOPER";
            objArrOper[3].Field = "GOPER.NUMERO";
            objArrOper[5].Field = "GOPER.CODFILIAL";
            objArrOper[75].Field = "GOPER.CODFILIALENTREGA";
            objArrOper[8].Field = "GOPER.CODLOCAL";
            objArrOper[9].Field = "GOPER.CODLOCALENTREGA";
            objArrOper[14].Field = "DATAEMISSAO";




            PS.Lib.DataField dfCODTIPOPER = gb.RetornaDataFieldByCampo(objArrOper, "GOPER.CODTIPOPER");
            PS.Lib.DataField dfNUMERO = gb.RetornaDataFieldByCampo(objArrOper, "GOPER.NUMERO");
            PS.Lib.DataField dtCODFILIAL = gb.RetornaDataFieldByCampo(objArrOper, "GOPER.CODFILIAL");
            PS.Lib.DataField dtCODFILIALENTREGA = gb.RetornaDataFieldByCampo(objArrOper, "GOPER.CODFILIALENTREGA");
            PS.Lib.DataField dtCODLOCAL = gb.RetornaDataFieldByCampo(objArrOper, "GOPER.CODLOCAL");
            PS.Lib.DataField dtCODLOCALENTREGA = gb.RetornaDataFieldByCampo(objArrOper, "GOPER.CODLOCALENTREGA");
            PS.Lib.DataField dtDATAEMISSAO = gb.RetornaDataFieldByCampo(objArrOper, "DATAEMISSAO");

            //  string sSql = string.Empty;

            //  #region Validação

            //  if (VerificaSituacao(objArrOper) == 2)
            //  {
            //      throw new Exception("A operação [" + dfNUMERO.Valor + "] já está cancelada.");
            //  }

            //  if (VerificaSituacao(objArrOper) == 3)
            //  {
            //      throw new Exception("A operação [" + dfNUMERO.Valor + "] não pode ser cancelada pois está parcialmente quitada.");
            //  }

            //  if (VerificaSituacao(objArrOper) == 4)
            //  {
            //      throw new Exception("A operação [" + dfNUMERO.Valor + "] não pode ser cancelada pois está quitada.");
            //  }

            //  if (PossuiRelacionamento(Convert.ToInt32(dtCODEMPRESA.Valor), Convert.ToInt32(dtCODOPER.Valor), true))
            //  {
            //      throw new Exception("A operação [" + dfNUMERO.Valor + "] não pode ser cancelada pois possui relacionamento com outra operação.");
            //  }

            //  if (PossuiFinanceiroBaixado(objArr))
            //  {
            //      throw new Exception("A operação [" + dfNUMERO.Valor + "] não pode ser cancelada pois possui lançamentos financeiros vinculados.");
            //  }

            //  if (PossuiNFEstadual(Convert.ToInt32(dtCODEMPRESA.Valor), Convert.ToInt32(dtCODOPER.Valor), true))
            //  {
            //      throw new Exception("A operação [" + dfNUMERO.Valor + "] não pode ser cancelada pois esta vinculada a uma NF-e.");
            //  }

            //  #endregion

            //  #region Carrega Parâmetro do TIpo da Operação

            //  DataRow PARAMNSTIPOPER = gb.RetornaParametrosOperacao(dfCODTIPOPER.Valor.ToString());

            //  if (PARAMNSTIPOPER == null)
            //  {
            //      throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação.");
            //  }

            //  #endregion

            //  #region Estoque antigo

            //  //if (PARAMNSTIPOPER["OPERESTOQUE"].ToString() != "N")
            //  //{
            //  //    sSql = @"SELECT * FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?";
            //  //    DataTable GOPERITEM = dbs.QuerySelect(sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor);
            //  //    if (GOPERITEM.Rows.Count > 0)
            //  //    {
            //  //        PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
            //  //        psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
            //  //        psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };

            //  //        for (int i = 0; i < GOPERITEM.Rows.Count; i++)
            //  //        {
            //  //            PS.Lib.DataField dfQUANTIDADE = gb.RetornaDataFieldByDataRow(GOPERITEM.Rows[0], "QUANTIDADE");
            //  //            PS.Lib.DataField dfCODUNIDOPER = gb.RetornaDataFieldByDataRow(GOPERITEM.Rows[0], "CODUNIDOPER");
            //  //            PS.Lib.DataField dfCODPRODUTO = gb.RetornaDataFieldByDataRow(GOPERITEM.Rows[0], "CODPRODUTO");

            //  //            PSPartLocalEstoqueSaldoData.Tipo Estoque;
            //  //            if (PARAMNSTIPOPER["OPERESTOQUE"].ToString() == "A")
            //  //                Estoque = PSPartLocalEstoqueSaldoData.Tipo.Aumenta;
            //  //            else
            //  //                Estoque = PSPartLocalEstoqueSaldoData.Tipo.Diminui;

            //  //            psPartLocalEstoqueSaldoData.MovimentaEstoque(Convert.ToInt32(dtCODEMPRESA.Valor),
            //  //                Convert.ToInt32(dtCODFILIAL.Valor),
            //  //                (dtCODLOCAL.Valor == null) ? string.Empty : dtCODLOCAL.Valor.ToString(),
            //  //                (dtCODLOCALENTREGA.Valor == null) ? string.Empty : dtCODLOCALENTREGA.Valor.ToString(),
            //  //                (dfCODPRODUTO.Valor == null) ? string.Empty : dfCODPRODUTO.Valor.ToString(),
            //  //                Convert.ToDecimal(dfQUANTIDADE.Valor),
            //  //                (dfCODUNIDOPER.Valor == null) ? string.Empty : dfCODUNIDOPER.Valor.ToString(),
            //  //                Estoque);
            //  //        }
            //  //    }
            //  //}

            //  #endregion

            //  #region ESTOQUE NOVO

            //  PS.Glb.PSPartLocalEstoqueSaldoData estoque = new PSPartLocalEstoqueSaldoData();
            ////  estoque.MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.OperacaoCancelamento, Convert.ToInt32(dtCODEMPRESA.Valor), Convert.ToInt32(dtCODOPER.Valor), 0);

            //  #endregion

            //  #region Financeiro

            //  if (int.Parse(PARAMNSTIPOPER["GERAFINANCEIRO"].ToString()) == 1)
            //  {
            //      sSql = @"SELECT * FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ? AND CODSTATUS <> 2";
            //      System.Data.DataTable dtLancaRelac = dbs.QuerySelect(sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor);
            //      foreach (System.Data.DataRow row in dtLancaRelac.Rows)
            //      {
            //          List<PS.Lib.DataField> temp = gb.RetornaDataFieldByDataRow(row);
            //          PSPartLancaData _psPartLancaData = new PSPartLancaData();
            //          _psPartLancaData._tablename = "FLANCA";
            //          _psPartLancaData._keys = new string[] { "CODEMPRESA", "CODLANCA" };
            //          _psPartLancaData.CancelaLancamentoOper(temp);
            //      }
            //  }

            //  #endregion

            //  #region Possui Antecessor

            //  sSql = @"SELECT CODOPER FROM GOPERRELAC WHERE CODEMPRESA = ? AND CODOPERRELAC = ?";
            //  DataTable OperRelac = dbs.QuerySelect(sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor);
            //  foreach (DataRow row in OperRelac.Rows)
            //  {
            //      sSql = @"SELECT * FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";
            //      List<PS.Lib.DataField> objOperRelac = gb.RetornaDataFieldByDataRow(dbs.QuerySelect(sSql, dtCODEMPRESA.Valor, row["CODOPER"]).Rows[0]);
            //      PS.Lib.DataField dfCODTIPOPERRELAC = gb.RetornaDataFieldByCampo(objOperRelac, "CODTIPOPER");
            //      PS.Lib.DataField dfCODOPERRELAC = gb.RetornaDataFieldByCampo(objOperRelac, "CODOPER");

            //      DataRow PARAMNSTIPOPERRELAC = gb.RetornaParametrosOperacao(dfCODTIPOPERRELAC.Valor.ToString());
            //      if (int.Parse(PARAMNSTIPOPERRELAC["GERAFINANCEIRO"].ToString()) == 1)
            //      {
            //          GeraFinanceiro(objOperRelac);
            //      }

            //      sSql = @"UPDATE GOPER SET CODSTATUS = ? WHERE CODEMPRESA = ? AND CODOPER = ?";
            //      dbs.QueryExec(sSql, 0, dtCODEMPRESA.Valor, dfCODOPERRELAC.Valor);
            //  }

            //  #endregion

            //  #region Atualiza Status

            //  sSql = @"UPDATE GOPER SET CODSTATUS = ? WHERE CODEMPRESA = ? AND CODOPER = ?";

            //  dbs.QueryExec(sSql, 2, dtCODEMPRESA.Valor, dtCODOPER.Valor);

            //  #endregion


            //Verifica a permissão da operação.
            bool permite = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT 
PERMITEALTERAR 
FROM 
GACESSOMENU 
INNER JOIN GUSUARIOPERFIL ON GACESSOMENU.CODPERFIL = GUSUARIOPERFIL.CODPERFIL AND GACESSOMENU.CODEMPRESA = GUSUARIOPERFIL.CODEMPRESA
WHERE 
GACESSOMENU.CODPSPART = ?
AND GUSUARIOPERFIL.CODUSUARIO = ?
AND GACESSOMENU.CODEMPRESA = ?", new object[] { "PSPartOperacao", AppLib.Context.Usuario, AppLib.Context.Empresa }));
            if (permite == true)
            {
                //Verifica a permissão do usuário
                permite = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT 
CANCELAR 
FROM 
GPERFILTIPOPER 
INNER JOIN GUSUARIOPERFIL ON GPERFILTIPOPER.CODPERFIL = GUSUARIOPERFIL.CODPERFIL AND GPERFILTIPOPER.CODEMPRESA = GUSUARIOPERFIL.CODEMPRESA 
WHERE 
GUSUARIOPERFIL.CODUSUARIO = ?
AND GUSUARIOPERFIL.CODEMPRESA = ? 
AND GPERFILTIPOPER.CODTIPOPER = ?", new object[] { AppLib.Context.Usuario, AppLib.Context.Empresa, dfCODTIPOPER.Valor }));
                if (permite == true)
                {

                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                    conn.BeginTransaction();
                    try
                    {
                        conn.ExecTransaction("UPDATE GOPER SET CODSTATUS = 2 WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { dtCODOPER.Valor, AppLib.Context.Empresa });

                        List<int> m_codOper = new List<int>();

                        conn.ExecTransaction("DELETE FROM FLANCA WHERE CODOPER = ? AND CODEMPRESA =?", new object[] { dtCODOPER.Valor, AppLib.Context.Empresa });
                        //Exclui Financeiro

                        string relac = conn.ExecGetField(string.Empty, "SELECT CODOPER FROM GOPERRELAC WHERE CODOPERRELAC = ? AND CODEMPRESA = ?", new object[] { dtCODOPER.Valor, AppLib.Context.Empresa }).ToString();

                        conn.Commit();

                        ExcluiFichaEstoque(conn, Convert.ToInt32(dtCODOPER.Valor), Convert.ToInt32(dtCODFILIAL.Valor), Convert.ToDateTime(dtDATAEMISSAO.Valor));

                        // Bloco de código comentado por ser irrelevante para a execução da rotina - João Pedro Luchiari, 01/03/2018.
                        //PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                        //psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
                        //psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };
                        //psPartLocalEstoqueSaldoData.MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.OperacaoCancelamento, AppLib.Context.Empresa, Convert.ToInt32(dtCODOPER.Valor), 0);

                        conn.BeginTransaction();

                        // Código comentado pois houve mudança na rotina - João Pedro Luchiari, 01/03/2018
                        //conn.ExecTransaction("DELETE FROM VFICHAESTOQUE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, dtCODOPER.Valor });

                        PSPartOperacaoEdit frmGera = new PSPartOperacaoEdit();

                        if (!string.IsNullOrEmpty(relac))
                        {
                            //PSPartOperacaoEdit frmGera = new PSPartOperacaoEdit();

                            DataTable dtItens = conn.ExecQuery("SELECT * FROM GOPERITEMRELAC WHERE CODOPERITEMDESTINO = ?", new object[] { dtCODOPER.Valor });
                            for (int i = 0; i < dtItens.Rows.Count; i++)
                            {
                                decimal qtde = Convert.ToDecimal(conn.ExecGetField(0, "SELECT QUANTIDADE FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?", new object[] {AppLib.Context.Empresa, dtCODOPER.Valor, dtItens.Rows[i]["NSEQITEMDESTINO"] }));

                                conn.ExecTransaction("UPDATE GOPERITEM SET QUANTIDADE_SALDO = QUANTIDADE_SALDO + ?, QUANTIDADE_FATURADO = QUANTIDADE_FATURADO - ? WHERE CODOPER = ? AND CODEMPRESA = ? AND NSEQITEM = ? ", new object[] {qtde, qtde, Convert.ToInt32(dtItens.Rows[i]["CODOPERITEMORIGEM"]), AppLib.Context.Empresa, dtItens.Rows[i]["NSEQITEMORIGEM"] });

                                conn.ExecTransaction("DELETE FROM GOPERITEMRELAC WHERE CODOPERITEMDESTINO = ? AND NSEQITEMDESTINO = ?", new object[] {dtCODOPER.Valor, dtItens.Rows[i]["NSEQITEMDESTINO"] });

                                conn.ExecTransaction("UPDATE GOPER SET CODSTATUS = (SELECT CASE WHEN(SUM(QUANTIDADE_SALDO) = SUM(QUANTIDADE)) THEN '0' ELSE '5' END FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?) WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(dtItens.Rows[i]["CODOPERITEMORIGEM"]), Convert.ToInt32(dtItens.Rows[i]["CODOPERITEMORIGEM"]) , AppLib.Context.Empresa });
                            }

                            //frmGera.geraFinanceiro(conn.ExecGetField(string.Empty, "SELECT CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, relac }).ToString(), Convert.ToInt32(relac));

                            conn.ExecTransaction("UPDATE GOPER SET CODSTATUS = (SELECT CASE WHEN(SUM(QUANTIDADE_SALDO) = SUM(QUANTIDADE)) THEN '0' ELSE '5' END FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?) WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { AppLib.Context.Empresa, relac, relac, AppLib.Context.Empresa });

                           conn.ExecTransaction("DELETE FROM GOPERRELAC WHERE CODOPER = ? AND CODOPERRELAC = ? AND CODEMPRESA =?", new object[] { relac, dtCODOPER.Valor, AppLib.Context.Empresa });
                        }
                        conn.Commit();
                        frmGera.geraFinanceiro(conn.ExecGetField(string.Empty, "SELECT CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, relac }).ToString(), Convert.ToInt32(relac));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conn.Rollback();
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("Usuário sem permissão para alteração.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Opereção sem permissão para alteração.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void ExcluiFichaEstoque(AppLib.Data.Connection conn, int codoper, int codFilial, DateTime DataEmissao)
        {
            DataTable dtRecalSaldo = new DataTable();

            try
            {
                // Executa as transações de Dados em cascata

                conn.ExecTransaction("DELETE FROM VFICHAESTOQUE_ANTERIOR", new object[] { });

                conn.ExecTransaction(@"INSERT INTO VFICHAESTOQUE_ANTERIOR 
SELECT VFICHAESTOQUE.*
FROM VFICHAESTOQUE (NOLOCK)
INNER JOIN VPARAMETROS ON VPARAMETROS.CODEMPRESA = VFICHAESTOQUE.CODEMPRESA
WHERE VFICHAESTOQUE.DATAENTSAI >= ?
AND VFICHAESTOQUE.CODEMPRESA = ?
AND VFICHAESTOQUE.CODFILIAL = ?
AND VFICHAESTOQUE.CODPRODUTO IN (SELECT GOPERITEM.CODPRODUTO
									FROM GOPERITEM
									WHERE GOPERITEM.CODEMPRESA = ? 
									AND GOPERITEM.CODOPER = ?)", new object[] { DataEmissao, AppLib.Context.Empresa, codFilial, AppLib.Context.Empresa, codoper });

                conn.ExecTransaction(@"DELETE FROM VFICHAESTOQUE
WHERE VFICHAESTOQUE.DATAENTSAI >= ?
AND VFICHAESTOQUE.CODEMPRESA = ?
AND VFICHAESTOQUE.CODFILIAL = ? 
AND VFICHAESTOQUE.CODPRODUTO IN  (SELECT GOPERITEM.CODPRODUTO
									FROM GOPERITEM
									WHERE GOPERITEM.CODEMPRESA = ? 
									AND GOPERITEM.CODOPER = ?)", new object[] { DataEmissao, AppLib.Context.Empresa, codFilial, AppLib.Context.Empresa, codoper });

                dtRecalSaldo = conn.ExecQuery(@"SELECT 
 GOPERITEM.CODEMPRESA
,GOPERITEM.CODOPER
,GOPERITEM.NSEQITEM

,GOPER.DATAENTSAI
,GOPERITEM.CODPRODUTO

FROM
GOPERITEM (NOLOCK)
INNER JOIN GOPER (NOLOCK) ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER
INNER JOIN GTIPOPER (NOLOCK) ON GTIPOPER.CODEMPRESA = GOPER.CODEMPRESA AND GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER
INNER JOIN VPARAMETROS ON VPARAMETROS.CODEMPRESA = GOPERITEM.CODEMPRESA

WHERE
GOPERITEM.CODEMPRESA = ?
AND GOPER.CODFILIAL = ? 
AND GTIPOPER.OPERESTOQUE <> 'N'
AND GOPERITEM.CODPRODUTO IN (SELECT GOPERITEM.CODPRODUTO
									FROM GOPERITEM
									WHERE GOPERITEM.CODEMPRESA = ? 
									AND GOPERITEM.CODOPER = ?)
AND GOPER.DATAENTSAI >= ?
AND GOPER.CODSTATUS <> 2

ORDER BY 
 GOPERITEM.CODEMPRESA
,GOPERITEM.CODPRODUTO
,GOPER.DATAENTSAI
,GOPERITEM.CODOPER
,GOPERITEM.NSEQITEM", new object[] { AppLib.Context.Empresa, codFilial, AppLib.Context.Empresa, codoper, DataEmissao });

                for (int i = 0; i < dtRecalSaldo.Rows.Count; i++)
                {
                    PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                    PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque Estoque;
                    Estoque = PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao;

                    psPartLocalEstoqueSaldoData.MovimentaEstoque2(Estoque, AppLib.Context.Empresa, Convert.ToInt32(dtRecalSaldo.Rows[i]["CODOPER"]), Convert.ToInt32(dtRecalSaldo.Rows[i]["NSEQITEM"]));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool inseriHistorico(string codUsuario, int codOper)
        {
            int retorno = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GLIBERACAOOPER (CODEMPRESA, CODOPER, CODUSUARIO, DATACRIACAO) VALUES (?,?,?,?)", new object[] { AppLib.Context.Empresa, codOper, codUsuario, AppLib.Context.poolConnection.Get("Start").GetDateTime() });
            if (retorno > 0)
            {
                return true;
            }
            return false;
        }

        public List<PS.Lib.DataField> Faturar(List<PS.Lib.DataField> objArrOrigem, string FAT_PROXIMA_ETAPA)
        {
            List<PS.Lib.DataField> objArrDDL = new List<Lib.DataField>();

            try
            {

                string sSql = "";

                int FAT_ULTIMONIVEL = 0;

                PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArrOrigem, "CODEMPRESA");
                PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArrOrigem, "CODOPER");
                PS.Lib.DataField dfCODTIPOPER = gb.RetornaDataFieldByCampo(objArrOrigem, "CODTIPOPER");
                PS.Lib.DataField dfNUMERO = gb.RetornaDataFieldByCampo(objArrOrigem, "NUMERO");
                PS.Lib.DataField dfPLACA = gb.RetornaDataFieldByCampo(objArrOrigem, "PLACA");
                PS.Lib.DataField dfUFPLACA = gb.RetornaDataFieldByCampo(objArrOrigem, "UFPLACA");
                PS.Lib.DataField dfPRCOMISSAO = gb.RetornaDataFieldByCampo(objArrOrigem, "PRCOMISSAO");
                PS.Lib.DataField dfCODCLIFOR = gb.RetornaDataFieldByCampo(objArrOrigem, "CODCLIFOR");
                PS.Lib.DataField dfTIPOPERCONSFIN = gb.RetornaDataFieldByCampo(objArrOrigem, "TIPOPERCONSFIN");
                PS.Lib.DataField dfCLIENTERETIRA = gb.RetornaDataFieldByCampo(objArrOrigem, "CLIENTERETIRA");

                AppLib.Util.Log.Escrever("Faturando a operação " + dfCODEMPRESA.Valor.ToString() + ";" + dfCODOPER.Valor.ToString());

                int FRM_CODEMPRESA = 0;
                string FRM_CODFORMULA = string.Empty;
                string FRM_CODFORMULAVALORBRUTO = string.Empty;
                string FRM_CODFORMULAVALORLIQUIDO = string.Empty;

                DataRow PARAMTIPOPERDESTINO;

                //#region Verifica Lançamentos Vencidos
                //if (verificaLancVencidos(FAT_PROXIMA_ETAPA, dfCODCLIFOR.Valor.ToString()).Equals(false))
                //{
                //    //Abrir tela com solicitação de senha;
                //    if (MessageBox.Show("Existe lançamentos em aberto para o cliente selecionado.\nDeseja liberar o pedido?", "Informação do Sistema.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question).Equals(DialogResult.OK))
                //    {
                //        PS.Glb.ERP.Comercial.FormLoginFinanceiro frm = new ERP.Comercial.FormLoginFinanceiro();
                //        frm.ShowDialog();
                //        bool retorno = frm.confirmacao;

                //        if (retorno.Equals(false))
                //        {
                //            throw new Exception("Usuário e senha não confere.");
                //        }
                //        else
                //        {
                //            MessageBox.Show("Cliente liberado com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            if (inseriHistorico(frm.codUsuario, Convert.ToInt32(dfCODOPER.Valor)).Equals(false))
                //            {
                //                throw new Exception("Erro ao cadastrar Histórico.");        
                //            } 
                //        }
                //    }
                //    else
                //    {
                //        throw new Exception("Cliente bloqueado.");
                //    }
                //}
                //#endregion
                #region Verifica Situção

                if (VerificaSituacao(objArrOrigem) != 0)
                {
                    throw new Exception("Operação [ " + dfNUMERO.Valor + " ] não pode ser faturada pois não está em aberto.");
                }

                #endregion

                #region Define Próxima Etapa

                if (FAT_PROXIMA_ETAPA != string.Empty)
                {
                    PARAMTIPOPERDESTINO = gb.RetornaParametrosOperacao(FAT_PROXIMA_ETAPA);

                    if (PARAMTIPOPERDESTINO == null)
                    {
                        throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação [" + FAT_PROXIMA_ETAPA + "].");
                    }
                }
                else
                {
                    //Quando não há próxima etapa definida, verifica se a operação fatura para ela mesma
                    PARAMTIPOPERDESTINO = gb.RetornaParametrosOperacao(dfCODTIPOPER.Valor.ToString());

                    if (PARAMTIPOPERDESTINO == null)
                    {
                        throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação [" + dfCODTIPOPER.Valor + "] selecionada.");
                    }

                    FAT_ULTIMONIVEL = int.Parse(PARAMTIPOPERDESTINO["ULTIMONIVEL"].ToString());

                    if (FAT_ULTIMONIVEL == 0)
                    {
                        throw new Exception("Tipo de operação [" + dfCODTIPOPER.Valor + "] não esta definida como sendo último nível.");
                    }
                }

                #endregion

                #region Fatura Operação

                sSql = @"SELECT * FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";
                DataTable GOPERORIGEM = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor);

                if (GOPERORIGEM.Rows.Count > 0)
                {
                    try
                    {
                        //dbs.Begin();

                        #region Operação

                        #region Atributos

                        PS.Lib.DataField OP_CODEMPRESA = new PS.Lib.DataField("CODEMPRESA", null);
                        PS.Lib.DataField OP_CODOPER = new PS.Lib.DataField("CODOPER", null, typeof(int), PS.Lib.Global.TypeAutoinc.AutoInc);
                        PS.Lib.DataField OP_CODTIPOPER = new PS.Lib.DataField("CODTIPOPER", null);
                        PS.Lib.DataField OP_NUMERO = new PS.Lib.DataField("NUMERO", null);
                        PS.Lib.DataField OP_CODCLIFOR = new PS.Lib.DataField("CODCLIFOR", null);
                        PS.Lib.DataField OP_CODFILIAL = new PS.Lib.DataField("CODFILIAL", null);
                        PS.Lib.DataField OP_CODFILIALENTREGA = new PS.Lib.DataField("CODFILIALENTREGA", null);
                        PS.Lib.DataField OP_CODSERIE = new PS.Lib.DataField("CODSERIE", null);
                        PS.Lib.DataField OP_CODNATUREZA = new PS.Lib.DataField("CODNATUREZA", null);
                        PS.Lib.DataField OP_CODLOCAL = new PS.Lib.DataField("CODLOCAL", null);
                        PS.Lib.DataField OP_CODLOCALENTREGA = new PS.Lib.DataField("CODLOCALENTREGA", null);
                        PS.Lib.DataField OP_CODOBJETO = new PS.Lib.DataField("CODOBJETO", null);
                        PS.Lib.DataField OP_CODOPERADOR = new PS.Lib.DataField("CODOPERADOR", null);
                        PS.Lib.DataField OP_CODSTATUS = new PS.Lib.DataField("CODSTATUS", null);
                        PS.Lib.DataField OP_DATACRIACA = new PS.Lib.DataField("DATACRIACAO", null);
                        PS.Lib.DataField OP_DATAEMISSAO = new PS.Lib.DataField("DATAEMISSAO", null);
                        PS.Lib.DataField OP_DATAENTSAI = new PS.Lib.DataField("DATAENTSAI", null);
                        PS.Lib.DataField OP_FRETECIFFOB = new PS.Lib.DataField("FRETECIFFOB", null);
                        PS.Lib.DataField OP_CODTRANSPORTADORA = new PS.Lib.DataField("CODTRANSPORTADORA", null);
                        PS.Lib.DataField OP_CODCONDICAO = new PS.Lib.DataField("CODCONDICAO", null);
                        PS.Lib.DataField OP_CODFORMA = new PS.Lib.DataField("CODFORMA", null);
                        PS.Lib.DataField OP_VALORBRUTO = new PS.Lib.DataField("VALORBRUTO", 0);
                        PS.Lib.DataField OP_VALORLIQUIDO = new PS.Lib.DataField("VALORLIQUIDO", 0);
                        PS.Lib.DataField OP_VALORFRETE = new PS.Lib.DataField("VALORFRETE", 0);
                        //USUARIOCRIAÇÃO


                        PS.Lib.DataField OP_DATAENTREGA = new PS.Lib.DataField("DATAENTREGA", null);
                        PS.Lib.DataField OP_QUANTIDADE = new PS.Lib.DataField("QUANTIDADE", 0);
                        PS.Lib.DataField OP_PESOLIQUIDO = new PS.Lib.DataField("PESOLIQUIDO", 0);
                        PS.Lib.DataField OP_PESOBRUTO = new PS.Lib.DataField("PESOBRUTO", 0);
                        PS.Lib.DataField OP_ESPECIE = new PS.Lib.DataField("ESPECIE", null);
                        PS.Lib.DataField OP_CODCONTA = new PS.Lib.DataField("CODCONTA", null);
                        PS.Lib.DataField OP_CODREPRE = new PS.Lib.DataField("CODREPRE", null);
                        PS.Lib.DataField OP_PRCOMISSAO = new PS.Lib.DataField("PRCOMISSAO", 0);
                        //DATAFATURAMENTO
                        //CODUSUARIOFATURAMENTO
                        PS.Lib.DataField OP_CAMPOLIVRE1 = new PS.Lib.DataField("CAMPOLIVRE1", null);
                        PS.Lib.DataField OP_CAMPOLIVRE2 = new PS.Lib.DataField("CAMPOLIVRE2", null);
                        PS.Lib.DataField OP_CAMPOLIVRE3 = new PS.Lib.DataField("CAMPOLIVRE3", null);
                        PS.Lib.DataField OP_CAMPOLIVRE4 = new PS.Lib.DataField("CAMPOLIVRE4", null);
                        PS.Lib.DataField OP_CAMPOLIVRE5 = new PS.Lib.DataField("CAMPOLIVRE5", null);
                        PS.Lib.DataField OP_CAMPOLIVRE6 = new PS.Lib.DataField("CAMPOLIVRE6", null);
                        PS.Lib.DataField OP_DATAEXTRA1 = new PS.Lib.DataField("DATAEXTRA1", null);
                        PS.Lib.DataField OP_DATAEXTRA2 = new PS.Lib.DataField("DATAEXTRA2", null);
                        PS.Lib.DataField OP_DATAEXTRA3 = new PS.Lib.DataField("DATAEXTRA3", null);
                        PS.Lib.DataField OP_DATAEXTRA4 = new PS.Lib.DataField("DATAEXTRA4", null);
                        PS.Lib.DataField OP_DATAEXTRA5 = new PS.Lib.DataField("DATAEXTRA5", null);
                        PS.Lib.DataField OP_DATAEXTRA6 = new PS.Lib.DataField("DATAEXTRA6", null);
                        PS.Lib.DataField OP_CAMPOVALOR1 = new PS.Lib.DataField("CAMPOVALOR1", 0);
                        PS.Lib.DataField OP_CAMPOVALOR2 = new PS.Lib.DataField("CAMPOVALOR2", 0);
                        PS.Lib.DataField OP_CAMPOVALOR3 = new PS.Lib.DataField("CAMPOVALOR3", 0);
                        PS.Lib.DataField OP_CAMPOVALOR4 = new PS.Lib.DataField("CAMPOVALOR4", 0);
                        PS.Lib.DataField OP_CAMPOVALOR5 = new PS.Lib.DataField("CAMPOVALOR5", 0);
                        PS.Lib.DataField OP_CAMPOVALOR6 = new PS.Lib.DataField("CAMPOVALOR6", 0);
                        PS.Lib.DataField OP_OBSERVACAO = new PS.Lib.DataField("OBSERVACAO", null);
                        PS.Lib.DataField OP_HISTORICO = new PS.Lib.DataField("HISTORICO", null);
                        PS.Lib.DataField OP_PERCFRETE = new PS.Lib.DataField("PERCFRETE", 0);
                        PS.Lib.DataField OP_VALORDESCONTO = new PS.Lib.DataField("VALORDESCONTO", 0);
                        PS.Lib.DataField OP_PERCDESCONTO = new PS.Lib.DataField("PERCDESCONTO", 0);
                        PS.Lib.DataField OP_VALORDESPESA = new PS.Lib.DataField("VALORDESPESA", 0);
                        PS.Lib.DataField OP_PERCDESPESA = new PS.Lib.DataField("PERCDESPESA", 0);
                        PS.Lib.DataField OP_VALORSEGURO = new PS.Lib.DataField("VALORSEGURO", 0);
                        PS.Lib.DataField OP_PERCSEGURO = new PS.Lib.DataField("PERCSEGURO", 0);
                        PS.Lib.DataField OP_MARCA = new PS.Lib.DataField("MARCA", null);
                        PS.Lib.DataField OP_CODNATUREZAORCAMENTO = new PS.Lib.DataField("CODNATUREZAORCAMENTO", null);
                        PS.Lib.DataField OP_CODCCUSTO = new PS.Lib.DataField("CODCCUSTO", null);
                        PS.Lib.DataField OP_PLACA = new PS.Lib.DataField("PLACA", null);
                        PS.Lib.DataField OP_UFPLACA = new PS.Lib.DataField("UFPLACA", null);

                        PS.Lib.DataField OP_NFE = new PS.Lib.DataField("NFE", null);
                        PS.Lib.DataField OP_CHAVENFE = new PS.Lib.DataField("CHAVENFE", null);
                        PS.Lib.DataField OP_TIPOPERCONSFIN = new PS.Lib.DataField("TIPOPERCONSFIN", 0);
                        PS.Lib.DataField OP_CODVENDEDOR = new PS.Lib.DataField("CODVENDEDOR", 0);
                        PS.Lib.DataField OP_ORDEMDECOMPRA = new PS.Lib.DataField("ORDEMDECOMPRA", null);
                        PS.Lib.DataField OP_CODTIPOTRANSPORTE = new PS.Lib.DataField("CODTIPOTRANSPORTE", null);
                        PS.Lib.DataField OP_CLIENTERETIRA = new PS.Lib.DataField("CLIENTERETIRA", 0);
                        PS.Lib.DataField OP_LIMITEDESC = new PS.Lib.DataField("LIMITEDESC", 0);
                        PS.Lib.DataField OP_TIPOBLOQUEIODESC = new PS.Lib.DataField("TIPOBLOQUEIODESC", null);

                        #endregion

                        #region Sets

                        OP_CODEMPRESA.Valor = dfCODEMPRESA.Valor;
                        OP_PLACA.Valor = dfPLACA.Valor;
                        OP_UFPLACA.Valor = dfUFPLACA.Valor;
                        OP_PRCOMISSAO.Valor = dfPRCOMISSAO.Valor;
                        OP_DATACRIACA.Valor = DateTime.Now;

                        if (dfTIPOPERCONSFIN.Valor.Equals(true))
                        {
                            OP_TIPOPERCONSFIN.Valor = 1;
                        }
                        else
                        {
                            OP_TIPOPERCONSFIN.Valor = 0;
                        }
                        if (dfCLIENTERETIRA.Valor.Equals(true))
                        {
                            OP_CLIENTERETIRA.Valor = 1;
                        }
                        else
                        {
                            OP_CLIENTERETIRA.Valor = 0;
                        }

                        #region Numero

                        if (int.Parse(PARAMTIPOPERDESTINO["USANUMEROSEQ"].ToString()) == 1)
                        {
                            OP_NUMERO.Valor = this.GeraNumeroDocumento(FAT_PROXIMA_ETAPA);
                        }
                        else
                        {
                            OP_NUMERO.Valor = dfNUMERO.Valor;
                        }

                        #endregion

                        #region Série

                        //Edita
                        if (PARAMTIPOPERDESTINO["OPERSERIE"].ToString() == "E")
                        {
                            OP_CODSERIE.Valor = PARAMTIPOPERDESTINO["SERIEDEFAULT"];
                        }

                        //Não Edita
                        if (PARAMTIPOPERDESTINO["OPERSERIE"].ToString() == "N")
                        {
                            OP_CODSERIE.Valor = PARAMTIPOPERDESTINO["SERIEDEFAULT"];
                        }

                        //Não Mostra
                        if (PARAMTIPOPERDESTINO["OPERSERIE"].ToString() == "M")
                        {
                            OP_CODSERIE.Valor = null;
                        }

                        #endregion

                        #region Limite Desconto
                        OP_LIMITEDESC.Valor = GOPERORIGEM.Rows[0]["LIMITEDESC"];
                        #endregion

                        #region NFE
                        OP_NFE.Valor = GOPERORIGEM.Rows[0]["NFE"];
                        #endregion

                        #region Tipo de Desconto
                        OP_TIPOBLOQUEIODESC.Valor = GOPERORIGEM.Rows[0]["TIPOBLOQUEIODESC"];
                        #endregion

                        #region Ordem de Compra
                        OP_ORDEMDECOMPRA.Valor = GOPERORIGEM.Rows[0]["ORDEMDECOMPRA"];
                        #endregion

                        #region Filial

                        OP_CODFILIAL.Valor = GOPERORIGEM.Rows[0]["CODFILIAL"];
                        OP_CODFILIALENTREGA.Valor = GOPERORIGEM.Rows[0]["CODFILIALENTREGA"];

                        #endregion

                        #region Vendedor
                        OP_CODVENDEDOR.Valor = GOPERORIGEM.Rows[0]["CODVENDEDOR"];
                        #endregion

                        #region Local 1

                        //Edita
                        if (PARAMTIPOPERDESTINO["LOCAL1"].ToString() == "E")
                        {
                            if (GOPERORIGEM.Rows[0].ItemArray[GOPERORIGEM.Columns.IndexOf("CODLOCAL")] == DBNull.Value)
                            {
                                OP_CODLOCAL.Valor = PARAMTIPOPERDESTINO["LOCAL1DEFAULT"];
                            }
                            else
                            {
                                OP_CODLOCAL.Valor = GOPERORIGEM.Rows[0]["CODLOCAL"];
                            }
                        }

                        //Não Edita
                        if (PARAMTIPOPERDESTINO["LOCAL1"].ToString() == "N")
                        {
                            if (GOPERORIGEM.Rows[0].ItemArray[GOPERORIGEM.Columns.IndexOf("CODLOCAL")] == DBNull.Value)
                            {
                                OP_CODLOCAL.Valor = PARAMTIPOPERDESTINO["LOCAL1DEFAULT"];
                            }
                            else
                            {
                                OP_CODLOCAL.Valor = GOPERORIGEM.Rows[0]["CODLOCAL"];
                            }
                        }

                        //Não Mostra
                        if (PARAMTIPOPERDESTINO["LOCAL1"].ToString() == "M")
                        {
                            OP_CODLOCAL.Valor = null;
                        }

                        #endregion

                        #region Local 2

                        //Edita
                        if (PARAMTIPOPERDESTINO["LOCAL2"].ToString() == "E")
                        {
                            if (GOPERORIGEM.Rows[0].ItemArray[GOPERORIGEM.Columns.IndexOf("CODLOCALENTREGA")] != DBNull.Value)
                            {
                                OP_CODLOCALENTREGA.Valor = GOPERORIGEM.Rows[0]["CODLOCALENTREGA"];
                            }
                        }

                        //Não Edita
                        if (PARAMTIPOPERDESTINO["LOCAL2"].ToString() == "N")
                        {
                            if (GOPERORIGEM.Rows[0].ItemArray[GOPERORIGEM.Columns.IndexOf("CODLOCALENTREGA")] != DBNull.Value)
                            {
                                OP_CODLOCALENTREGA.Valor = GOPERORIGEM.Rows[0]["CODLOCALENTREGA"];
                            }
                        }

                        //Não Mostra
                        if (PARAMTIPOPERDESTINO["LOCAL2"].ToString() == "M")
                        {
                            OP_CODLOCALENTREGA.Valor = null;
                        }

                        #endregion

                        #region Cliente/Fornecedor

                        //Edita
                        if (PARAMTIPOPERDESTINO["CODCLIFOR"].ToString() == "E")
                        {
                            if (GOPERORIGEM.Rows[0].ItemArray[GOPERORIGEM.Columns.IndexOf("CODCLIFOR")] == DBNull.Value)
                            {
                                OP_CODCLIFOR.Valor = PARAMTIPOPERDESTINO["CODCLIFORPADRAO"];
                            }
                            else
                            {
                                OP_CODCLIFOR.Valor = GOPERORIGEM.Rows[0]["CODCLIFOR"];
                            }
                        }

                        //Não Edita
                        if (PARAMTIPOPERDESTINO["CODCLIFOR"].ToString() == "N")
                        {
                            if (GOPERORIGEM.Rows[0].ItemArray[GOPERORIGEM.Columns.IndexOf("CODCLIFOR")] == DBNull.Value)
                            {
                                OP_CODCLIFOR.Valor = PARAMTIPOPERDESTINO["CODCLIFORPADRAO"];
                            }
                            else
                            {
                                OP_CODCLIFOR.Valor = GOPERORIGEM.Rows[0]["CODCLIFOR"];
                            }
                        }

                        //Não Mostra
                        if (PARAMTIPOPERDESTINO["CODCLIFOR"].ToString() == "M")
                        {
                            OP_CODCLIFOR.Valor = null;
                        }

                        #endregion

                        #region Data Emissão

                        if (PARAMTIPOPERDESTINO["USADATAEMISSAO"].ToString() == "M")
                        {
                            OP_DATAEMISSAO.Valor = null;
                        }
                        else
                        {
                            OP_DATAEMISSAO.Valor = DateTime.Now;
                        }

                        #endregion

                        #region Data Entrada/Saida

                        if (PARAMTIPOPERDESTINO["USADATAENTSAI"].ToString() == "M")
                        {
                            OP_DATAENTSAI.Valor = null;
                        }
                        else
                        {
                            OP_DATAENTSAI.Valor = DateTime.Now;
                        }

                        #endregion

                        #region Data Entrega
                        if (PARAMTIPOPERDESTINO["USADATAENTREGA"].ToString() == "M")
                        {
                            OP_DATAENTREGA.Valor = null;
                        }
                        else
                        {
                            DateTime dataEntrega;
                            if (string.IsNullOrEmpty(GOPERORIGEM.Rows[0]["DATAENTREGA"].ToString()))
                            {
                                dataEntrega = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                            }
                            else
                            {
                                dataEntrega = Convert.ToDateTime(GOPERORIGEM.Rows[0]["DATAENTREGA"]);
                            }
                            //OP_DATAENTREGA.Valor = DateTime.Now + (Convert.ToDateTime(GOPERORIGEM.Rows[0]["DATAENTREGA"]) - Convert.ToDateTime(GOPERORIGEM.Rows[0]["DATAEMISSAO"]));
                            OP_DATAENTREGA.Valor = DateTime.Now + (dataEntrega - Convert.ToDateTime(GOPERORIGEM.Rows[0]["DATAEMISSAO"]));
                        }

                        #endregion

                        #region Objeto

                        //Edita
                        if (PARAMTIPOPERDESTINO["USACAMPOOBJETO"].ToString() == "E")
                        {
                            OP_CODOBJETO.Valor = GOPERORIGEM.Rows[0]["CODOBJETO"];
                        }

                        //Não Edita
                        if (PARAMTIPOPERDESTINO["USACAMPOOBJETO"].ToString() == "N")
                        {
                            OP_CODOBJETO.Valor = GOPERORIGEM.Rows[0]["CODOBJETO"];
                        }

                        //Não Mostra
                        if (PARAMTIPOPERDESTINO["USACAMPOOBJETO"].ToString() == "M")
                        {
                            OP_CODOBJETO.Valor = null;
                        }

                        #endregion

                        #region Operador

                        //Edita
                        if (PARAMTIPOPERDESTINO["USACAMPOOPERADOR"].ToString() == "E")
                        {
                            if (GOPERORIGEM.Rows[0].ItemArray[GOPERORIGEM.Columns.IndexOf("CODOPERADOR")] == DBNull.Value)
                            {
                                OP_CODOPERADOR.Valor = PARAMTIPOPERDESTINO["CODOPERADORPADRAO"];
                            }
                            else
                            {
                                OP_CODOPERADOR.Valor = GOPERORIGEM.Rows[0]["CODOPERADOR"];
                            }
                        }

                        //Não Edita
                        if (PARAMTIPOPERDESTINO["USACAMPOOPERADOR"].ToString() == "N")
                        {
                            if (GOPERORIGEM.Rows[0].ItemArray[GOPERORIGEM.Columns.IndexOf("CODOPERADOR")] == DBNull.Value)
                            {
                                OP_CODOPERADOR.Valor = PARAMTIPOPERDESTINO["CODOPERADORPADRAO"];
                            }
                            else
                            {
                                OP_CODOPERADOR.Valor = GOPERORIGEM.Rows[0]["CODOPERADOR"];
                            }
                        }

                        //Não Mostra
                        if (PARAMTIPOPERDESTINO["USACAMPOOPERADOR"].ToString() == "M")
                        {
                            OP_CODOPERADOR.Valor = null;
                        }

                        #endregion

                        #region Condição de Pagamento

                        //Edita
                        if (PARAMTIPOPERDESTINO["USACAMPOCONDPGTO"].ToString() == "E")
                        {
                            if (GOPERORIGEM.Rows[0].ItemArray[GOPERORIGEM.Columns.IndexOf("CODCONDICAO")] == DBNull.Value)
                            {
                                OP_CODCONDICAO.Valor = PARAMTIPOPERDESTINO["CODCONDICAOPADRAO"];
                            }
                            else
                            {
                                OP_CODCONDICAO.Valor = GOPERORIGEM.Rows[0]["CODCONDICAO"];
                            }
                        }

                        //Não Edita
                        if (PARAMTIPOPERDESTINO["USACAMPOCONDPGTO"].ToString() == "N")
                        {
                            if (GOPERORIGEM.Rows[0].ItemArray[GOPERORIGEM.Columns.IndexOf("CODCONDICAO")] == DBNull.Value)
                            {
                                OP_CODCONDICAO.Valor = PARAMTIPOPERDESTINO["CODCONDICAOPADRAO"];
                            }
                            else
                            {
                                OP_CODCONDICAO.Valor = GOPERORIGEM.Rows[0]["CODCONDICAO"];
                            }
                        }

                        //Não Mostra
                        if (PARAMTIPOPERDESTINO["USACAMPOCONDPGTO"].ToString() == "M")
                        {
                            OP_CODCONDICAO.Valor = null;
                        }

                        #endregion

                        #region Forma de Pagamento

                        //Edita
                        if (PARAMTIPOPERDESTINO["CODFORMA"].ToString() == "E")
                        {
                            if (GOPERORIGEM.Rows[0].ItemArray[GOPERORIGEM.Columns.IndexOf("CODFORMA")] == DBNull.Value)
                            {
                                OP_CODFORMA.Valor = PARAMTIPOPERDESTINO["CODFORMAPADRAO"];
                            }
                            else
                            {
                                OP_CODFORMA.Valor = GOPERORIGEM.Rows[0]["CODFORMA"];
                            }
                        }

                        //Não Edita
                        if (PARAMTIPOPERDESTINO["CODFORMA"].ToString() == "N")
                        {
                            if (GOPERORIGEM.Rows[0].ItemArray[GOPERORIGEM.Columns.IndexOf("CODFORMA")] == DBNull.Value)
                            {
                                OP_CODFORMA.Valor = PARAMTIPOPERDESTINO["CODFORMAPADRAO"];
                            }
                            else
                            {
                                OP_CODFORMA.Valor = GOPERORIGEM.Rows[0]["CODFORMA"];
                            }
                        }

                        //Não Mostra
                        if (PARAMTIPOPERDESTINO["CODFORMA"].ToString() == "M")
                        {
                            OP_CODFORMA.Valor = null;
                        }

                        #endregion

                        #region Conta Caixa

                        //Edita
                        if (PARAMTIPOPERDESTINO["USACONTA"].ToString() == "E")
                        {
                            if (GOPERORIGEM.Rows[0].ItemArray[GOPERORIGEM.Columns.IndexOf("CODCONTA")] == DBNull.Value)
                            {
                                OP_CODCONTA.Valor = PARAMTIPOPERDESTINO["CODCONTAPADRAO"];
                            }
                            else
                            {
                                OP_CODCONTA.Valor = GOPERORIGEM.Rows[0]["CODCONTA"];
                            }
                        }

                        //Não Edita
                        if (PARAMTIPOPERDESTINO["USACONTA"].ToString() == "N")
                        {
                            if (GOPERORIGEM.Rows[0].ItemArray[GOPERORIGEM.Columns.IndexOf("CODCONTA")] == DBNull.Value)
                            {
                                OP_CODCONTA.Valor = PARAMTIPOPERDESTINO["CODCONTAPADRAO"];
                            }
                            else
                            {
                                OP_CODCONTA.Valor = GOPERORIGEM.Rows[0]["CODCONTA"];
                            }
                        }

                        //Não Mostra
                        if (PARAMTIPOPERDESTINO["CODCONTA"].ToString() == "M")
                        {
                            OP_CODCONTA.Valor = null;
                        }

                        #endregion

                        #region Representante

                        //Edita
                        if (PARAMTIPOPERDESTINO["USACODREPRE"].ToString() == "E")
                        {
                            if (GOPERORIGEM.Rows[0].ItemArray[GOPERORIGEM.Columns.IndexOf("CODREPRE")] == DBNull.Value)
                            {
                                OP_CODCONTA.Valor = PARAMTIPOPERDESTINO["CODREPREPADRAO"];
                            }
                            else
                            {
                                OP_CODREPRE.Valor = GOPERORIGEM.Rows[0]["CODREPRE"];
                            }
                        }

                        //Não Edita
                        if (PARAMTIPOPERDESTINO["USACODREPRE"].ToString() == "N")
                        {
                            if (GOPERORIGEM.Rows[0].ItemArray[GOPERORIGEM.Columns.IndexOf("CODREPRE")] == DBNull.Value)
                            {
                                OP_CODCONTA.Valor = PARAMTIPOPERDESTINO["CODREPREPADRAO"];
                            }
                            else
                            {
                                OP_CODREPRE.Valor = GOPERORIGEM.Rows[0]["CODREPRE"];
                            }
                        }

                        //Não Mostra
                        if (PARAMTIPOPERDESTINO["USACODREPRE"].ToString() == "M")
                        {
                            OP_CODREPRE.Valor = null;
                        }

                        #endregion

                        #region Centro de Custo

                        //Edita
                        if (PARAMTIPOPERDESTINO["USACODCCUSTO"].ToString() == "E")
                        {
                            OP_CODCCUSTO.Valor = GOPERORIGEM.Rows[0]["CODCCUSTO"];
                        }

                        //Não Edita
                        if (PARAMTIPOPERDESTINO["USACODCCUSTO"].ToString() == "N")
                        {
                            OP_CODCCUSTO.Valor = GOPERORIGEM.Rows[0]["CODCCUSTO"];
                        }

                        //Não Mostra
                        if (PARAMTIPOPERDESTINO["USACODCCUSTO"].ToString() == "M")
                        {
                            OP_CODCCUSTO.Valor = null;
                        }

                        #endregion

                        #region Natureza Orçamentária

                        //Edita
                        if (PARAMTIPOPERDESTINO["USACODNATUREZAORCAMENTO"].ToString() == "E")
                        {
                            OP_CODNATUREZAORCAMENTO.Valor = GOPERORIGEM.Rows[0]["CODNATUREZAORCAMENTO"];
                        }

                        //Não Edita
                        if (PARAMTIPOPERDESTINO["USACODNATUREZAORCAMENTO"].ToString() == "N")
                        {
                            OP_CODNATUREZAORCAMENTO.Valor = GOPERORIGEM.Rows[0]["CODNATUREZAORCAMENTO"];
                        }

                        //Não Mostra
                        if (PARAMTIPOPERDESTINO["USACODNATUREZAORCAMENTO"].ToString() == "M")
                        {
                            OP_CODNATUREZAORCAMENTO.Valor = null;
                        }

                        #endregion

                        #region Observação/Histórico

                        if (PARAMTIPOPERDESTINO["USAABAOBSERV"].ToString() == "1")
                        {
                            OP_OBSERVACAO.Valor = GOPERORIGEM.Rows[0]["OBSERVACAO"];
                            OP_HISTORICO.Valor = GOPERORIGEM.Rows[0]["HISTORICO"];
                        }
                        else
                        {
                            OP_OBSERVACAO.Valor = null;
                            OP_HISTORICO.Valor = null;
                        }

                        #endregion

                        #region Transporte

                        if (PARAMTIPOPERDESTINO["USAABATRANSP"].ToString() == "1")
                        {
                            OP_FRETECIFFOB.Valor = GOPERORIGEM.Rows[0]["FRETECIFFOB"];
                            OP_CODTRANSPORTADORA.Valor = GOPERORIGEM.Rows[0]["CODTRANSPORTADORA"];
                            OP_QUANTIDADE.Valor = GOPERORIGEM.Rows[0]["QUANTIDADE"];
                            OP_PESOLIQUIDO.Valor = GOPERORIGEM.Rows[0]["PESOLIQUIDO"];
                            OP_PESOBRUTO.Valor = GOPERORIGEM.Rows[0]["PESOBRUTO"];
                            OP_ESPECIE.Valor = GOPERORIGEM.Rows[0]["ESPECIE"];
                            OP_MARCA.Valor = GOPERORIGEM.Rows[0]["MARCA"];
                        }
                        else
                        {
                            OP_FRETECIFFOB.Valor = null;
                            OP_CODTRANSPORTADORA.Valor = null;
                            OP_QUANTIDADE.Valor = 0;
                            OP_PESOLIQUIDO.Valor = 0;
                            OP_PESOBRUTO.Valor = 0;
                            OP_ESPECIE.Valor = null;
                            OP_MARCA.Valor = null;
                        }

                        #endregion

                        #region Campo Livre

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIACAMPOLIVRE1"].ToString()) == 1)
                            OP_CAMPOLIVRE1.Valor = GOPERORIGEM.Rows[0]["CAMPOLIVRE1"];

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIACAMPOLIVRE2"].ToString()) == 1)
                            OP_CAMPOLIVRE2.Valor = GOPERORIGEM.Rows[0]["CAMPOLIVRE2"];

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIACAMPOLIVRE3"].ToString()) == 1)
                            OP_CAMPOLIVRE3.Valor = GOPERORIGEM.Rows[0]["CAMPOLIVRE3"];

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIACAMPOLIVRE4"].ToString()) == 1)
                            OP_CAMPOLIVRE4.Valor = GOPERORIGEM.Rows[0]["CAMPOLIVRE4"];

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIACAMPOLIVRE5"].ToString()) == 1)
                            OP_CAMPOLIVRE5.Valor = GOPERORIGEM.Rows[0]["CAMPOLIVRE5"];

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIACAMPOLIVRE6"].ToString()) == 1)
                            OP_CAMPOLIVRE6.Valor = GOPERORIGEM.Rows[0]["CAMPOLIVRE6"];

                        #endregion

                        #region Campo Data Extra

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIADATAEXTRA1"].ToString()) == 1)
                            OP_DATAEXTRA1.Valor = GOPERORIGEM.Rows[0]["DATAEXTRA1"];

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIADATAEXTRA2"].ToString()) == 1)
                            OP_DATAEXTRA2.Valor = GOPERORIGEM.Rows[0]["DATAEXTRA2"];

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIADATAEXTRA3"].ToString()) == 1)
                            OP_DATAEXTRA3.Valor = GOPERORIGEM.Rows[0]["DATAEXTRA3"];

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIADATAEXTRA4"].ToString()) == 1)
                            OP_DATAEXTRA4.Valor = GOPERORIGEM.Rows[0]["DATAEXTRA4"];

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIADATAEXTRA5"].ToString()) == 1)
                            OP_DATAEXTRA5.Valor = GOPERORIGEM.Rows[0]["DATAEXTRA5"];

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIADATAEXTRA6"].ToString()) == 1)
                            OP_DATAEXTRA6.Valor = GOPERORIGEM.Rows[0]["DATAEXTRA6"];

                        #endregion

                        #region Campo Valor

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIACAMPOVALOR1"].ToString()) == 1)
                            OP_CAMPOVALOR1.Valor = GOPERORIGEM.Rows[0]["CAMPOVALOR1"];

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIACAMPOVALOR2"].ToString()) == 1)
                            OP_CAMPOVALOR2.Valor = GOPERORIGEM.Rows[0]["CAMPOVALOR2"];

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIACAMPOVALOR3"].ToString()) == 1)
                            OP_CAMPOVALOR3.Valor = GOPERORIGEM.Rows[0]["CAMPOVALOR3"];

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIACAMPOVALOR4"].ToString()) == 1)
                            OP_CAMPOVALOR4.Valor = GOPERORIGEM.Rows[0]["CAMPOVALOR4"];

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIACAMPOVALOR5"].ToString()) == 1)
                            OP_CAMPOVALOR5.Valor = GOPERORIGEM.Rows[0]["CAMPOVALOR5"];

                        if (int.Parse(PARAMTIPOPERDESTINO["COPIACAMPOVALOR6"].ToString()) == 1)
                            OP_CAMPOVALOR6.Valor = GOPERORIGEM.Rows[0]["CAMPOVALOR6"];

                        #endregion

                        #region Valores

                        #region Valor Bruto

                        OP_VALORBRUTO.Valor = 0;

                        #endregion

                        #region Valor Liquido

                        OP_VALORLIQUIDO.Valor = 0;

                        #endregion

                        #region Valor Frete

                        //Edita
                        if (PARAMTIPOPERDESTINO["USAVALORFRETE"].ToString() == "E")
                        {
                            OP_VALORFRETE.Valor = GOPERORIGEM.Rows[0]["VALORFRETE"];
                            OP_PERCFRETE.Valor = GOPERORIGEM.Rows[0]["PERCFRETE"];
                        }

                        //Não Edita
                        if (PARAMTIPOPERDESTINO["USAVALORFRETE"].ToString() == "N")
                        {
                            OP_VALORFRETE.Valor = GOPERORIGEM.Rows[0]["VALORFRETE"];
                            OP_PERCFRETE.Valor = GOPERORIGEM.Rows[0]["PERCFRETE"];
                        }

                        //Não Mostra
                        if (PARAMTIPOPERDESTINO["USAVALORFRETE"].ToString() == "M")
                        {
                            OP_VALORFRETE.Valor = 0;
                            OP_PERCFRETE.Valor = 0;
                        }

                        #endregion

                        #region Valor Desconto

                        //Edita
                        if (PARAMTIPOPERDESTINO["USAVALORDESCONTO"].ToString() == "E")
                        {
                            OP_VALORDESCONTO.Valor = GOPERORIGEM.Rows[0]["VALORDESCONTO"];
                            OP_PERCDESCONTO.Valor = GOPERORIGEM.Rows[0]["PERCDESCONTO"];
                        }

                        //Não Edita
                        if (PARAMTIPOPERDESTINO["USAVALORDESCONTO"].ToString() == "N")
                        {
                            OP_VALORDESCONTO.Valor = GOPERORIGEM.Rows[0]["VALORDESCONTO"];
                            OP_PERCDESCONTO.Valor = GOPERORIGEM.Rows[0]["PERCDESCONTO"];
                        }

                        //Não Mostra
                        if (PARAMTIPOPERDESTINO["USAVALORDESCONTO"].ToString() == "M")
                        {
                            OP_VALORDESCONTO.Valor = 0;
                            OP_PERCDESCONTO.Valor = 0;
                        }

                        #endregion

                        #region Valor Despesa

                        //Edita
                        if (PARAMTIPOPERDESTINO["USAVALORDESPESA"].ToString() == "E")
                        {
                            OP_VALORDESPESA.Valor = GOPERORIGEM.Rows[0]["VALORDESPESA"];
                            OP_PERCDESPESA.Valor = GOPERORIGEM.Rows[0]["PERCDESPESA"];
                        }

                        //Não Edita
                        if (PARAMTIPOPERDESTINO["USAVALORDESPESA"].ToString() == "N")
                        {
                            OP_VALORDESPESA.Valor = GOPERORIGEM.Rows[0]["VALORDESPESA"];
                            OP_PERCDESPESA.Valor = GOPERORIGEM.Rows[0]["PERCDESPESA"];
                        }

                        //Não Mostra
                        if (PARAMTIPOPERDESTINO["USAVALORDESPESA"].ToString() == "M")
                        {
                            OP_VALORDESPESA.Valor = 0;
                            OP_PERCDESPESA.Valor = 0;
                        }

                        #endregion

                        #region Valor Seguro

                        //Edita
                        if (PARAMTIPOPERDESTINO["USAVALORSEGURO"].ToString() == "E")
                        {
                            OP_VALORSEGURO.Valor = GOPERORIGEM.Rows[0]["VALORSEGURO"];
                            OP_PERCSEGURO.Valor = GOPERORIGEM.Rows[0]["PERCSEGURO"];
                        }

                        //Não Edita
                        if (PARAMTIPOPERDESTINO["USAVALORSEGURO"].ToString() == "N")
                        {
                            OP_VALORSEGURO.Valor = GOPERORIGEM.Rows[0]["VALORSEGURO"];
                            OP_PERCSEGURO.Valor = GOPERORIGEM.Rows[0]["PERCSEGURO"];
                        }

                        //Não Mostra
                        if (PARAMTIPOPERDESTINO["USAVALORSEGURO"].ToString() == "M")
                        {
                            OP_VALORSEGURO.Valor = 0;
                            OP_PERCSEGURO.Valor = 0;
                        }

                        #endregion

                        #endregion

                        #region TipoTransporte
                        OP_CODTIPOTRANSPORTE.Valor = GOPERORIGEM.Rows[0]["CODTIPOTRANSPORTE"];
                        #endregion

                        #region Ultimo Nivel

                        if (FAT_ULTIMONIVEL == 0)
                        {
                            #region Excluir Previsão

                            if (int.Parse(PARAMTIPOPERDESTINO["GERAFINANCEIRO"].ToString()) == 1)
                            {
                                if (GOPERORIGEM.Rows[0]["CODCONDICAO"] != null)
                                {
                                    PSPartLancaData psPartLancaData = new PSPartLancaData();
                                    psPartLancaData._tablename = "FLANCA";
                                    psPartLancaData._keys = new string[] { "CODEMPRESA", "CODLANCA" };

                                    sSql = "SELECT CODLANCA FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ?";
                                    System.Data.DataTable dtLancaOri = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor);
                                    foreach (DataRow row in dtLancaOri.Rows)
                                    {
                                        sSql = "SELECT * FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?";
                                        DataTable dtLanca = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, row["CODLANCA"]);
                                        foreach (DataRow row1 in dtLanca.Rows)
                                        {
                                            System.Data.DataRow PARAMTIPDOC = gb.RetornaParametrosTipoDocumento(row1["CODTIPDOC"].ToString());
                                            if (PARAMTIPDOC != null)
                                            {
                                                List<PS.Lib.DataField> TipDocParam = gb.RetornaDataFieldByDataRow(PARAMTIPDOC);
                                                PS.Lib.DataField dfCLASSIFICACAO = gb.RetornaDataFieldByCampo(TipDocParam, "CLASSIFICACAO");

                                                if (Convert.ToInt32(dfCLASSIFICACAO.Valor) == 3)
                                                {
                                                    List<PS.Lib.DataField> LancaPrev = gb.RetornaDataFieldByDataRow(row1);
                                                    psPartLancaData.DeleteRecordOper(LancaPrev);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception("Não foi possível carregar os parâmetros da Condição de Pagamento, verifique se a mesma foi informada na operação");
                                }
                            }

                            #endregion

                            OP_CODOPER.Valor = 0;
                            OP_CODTIPOPER.Valor = FAT_PROXIMA_ETAPA;

                            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
                            ListObjArr.Add(OP_CODEMPRESA);
                            ListObjArr.Add(OP_CODOPER);
                            ListObjArr.Add(OP_CODTIPOPER);
                            ListObjArr.Add(OP_NUMERO);
                            ListObjArr.Add(OP_CODCLIFOR);
                            ListObjArr.Add(OP_CODFILIAL);
                            ListObjArr.Add(OP_CODFILIALENTREGA);
                            ListObjArr.Add(OP_CODSERIE);
                            ListObjArr.Add(OP_CODNATUREZA);
                            ListObjArr.Add(OP_CODLOCAL);
                            ListObjArr.Add(OP_CODLOCALENTREGA);
                            ListObjArr.Add(OP_CODOBJETO);
                            ListObjArr.Add(OP_CODOPERADOR);
                            ListObjArr.Add(OP_CODSTATUS);
                            ListObjArr.Add(OP_DATAEMISSAO);
                            ListObjArr.Add(OP_DATAENTSAI);
                            ListObjArr.Add(OP_FRETECIFFOB);
                            ListObjArr.Add(OP_CODTRANSPORTADORA);
                            ListObjArr.Add(OP_CODCONDICAO);
                            ListObjArr.Add(OP_CODFORMA);
                            ListObjArr.Add(OP_VALORBRUTO);
                            ListObjArr.Add(OP_VALORLIQUIDO);
                            ListObjArr.Add(OP_VALORFRETE);
                            ListObjArr.Add(OP_DATAENTREGA);
                            ListObjArr.Add(OP_QUANTIDADE);
                            ListObjArr.Add(OP_PESOLIQUIDO);
                            ListObjArr.Add(OP_PESOBRUTO);
                            ListObjArr.Add(OP_ESPECIE);
                            ListObjArr.Add(OP_CODCONTA);
                            ListObjArr.Add(OP_CODREPRE);
                            ListObjArr.Add(OP_PRCOMISSAO);
                            ListObjArr.Add(OP_CAMPOLIVRE1);
                            ListObjArr.Add(OP_CAMPOLIVRE2);
                            ListObjArr.Add(OP_CAMPOLIVRE3);
                            ListObjArr.Add(OP_CAMPOLIVRE4);
                            ListObjArr.Add(OP_CAMPOLIVRE5);
                            ListObjArr.Add(OP_CAMPOLIVRE6);
                            ListObjArr.Add(OP_DATAEXTRA1);
                            ListObjArr.Add(OP_DATAEXTRA2);
                            ListObjArr.Add(OP_DATAEXTRA3);
                            ListObjArr.Add(OP_DATAEXTRA4);
                            ListObjArr.Add(OP_DATAEXTRA5);
                            ListObjArr.Add(OP_DATAEXTRA6);
                            ListObjArr.Add(OP_CAMPOVALOR1);
                            ListObjArr.Add(OP_CAMPOVALOR2);
                            ListObjArr.Add(OP_CAMPOVALOR3);
                            ListObjArr.Add(OP_CAMPOVALOR4);
                            ListObjArr.Add(OP_CAMPOVALOR5);
                            ListObjArr.Add(OP_CAMPOVALOR6);
                            ListObjArr.Add(OP_OBSERVACAO);
                            ListObjArr.Add(OP_HISTORICO);
                            ListObjArr.Add(OP_PERCFRETE);
                            ListObjArr.Add(OP_VALORDESCONTO);
                            ListObjArr.Add(OP_PERCDESCONTO);
                            ListObjArr.Add(OP_VALORDESPESA);
                            ListObjArr.Add(OP_PERCDESPESA);
                            ListObjArr.Add(OP_VALORSEGURO);
                            ListObjArr.Add(OP_PERCSEGURO);
                            ListObjArr.Add(OP_MARCA);
                            ListObjArr.Add(OP_CODNATUREZAORCAMENTO);
                            ListObjArr.Add(OP_CODCCUSTO);
                            ListObjArr.Add(OP_PLACA);
                            ListObjArr.Add(OP_UFPLACA);
                            ListObjArr.Add(OP_NFE);
                            ListObjArr.Add(OP_CHAVENFE);
                            ListObjArr.Add(OP_TIPOPERCONSFIN);
                            ListObjArr.Add(OP_CODVENDEDOR);
                            ListObjArr.Add(OP_ORDEMDECOMPRA);
                            ListObjArr.Add(OP_CODTIPOTRANSPORTE);
                            ListObjArr.Add(OP_CLIENTERETIRA);
                            ListObjArr.Add(OP_LIMITEDESC);
                            ListObjArr.Add(OP_TIPOBLOQUEIODESC);

                            List<PS.Lib.DataField> RetListObjArr = this.SaveRecord(ListObjArr);

                            OP_CODOPER = gb.RetornaDataFieldByCampo(RetListObjArr, "CODOPER");

                            for (int i = 0; i < ListObjArr.Count; i++)
                            {
                                if (ListObjArr[i].Field == "CODOPER")
                                {
                                    ListObjArr[i].Valor = OP_CODOPER.Valor;
                                }
                            }
                        }
                        else
                        {
                            OP_CODOPER.Valor = dfCODOPER.Valor;
                            OP_CODTIPOPER.Valor = dfCODTIPOPER.Valor;
                        }

                        objArrDDL = gb.RetornaDataFieldByDataRow(dbs.QuerySelect("SELECT * FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", OP_CODEMPRESA.Valor, OP_CODOPER.Valor).Rows[0]);

                        #endregion

                        #endregion

                        #region Item

                        sSql = @"SELECT * FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?";

                        DataTable GOPERITEMORIGEM = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor);

                        if (GOPERITEMORIGEM.Rows.Count > 0)
                        {
                            #region Atributos

                            PS.Lib.DataField OP_ITEMCODEMPRESA = new PS.Lib.DataField("CODEMPRESA", null);
                            PS.Lib.DataField OP_ITEMCODOPER = new PS.Lib.DataField("CODOPER", null);
                            PS.Lib.DataField OP_ITEMNSEQITEM = new PS.Lib.DataField("NSEQITEM", null, typeof(int), PS.Lib.Global.TypeAutoinc.Max);
                            PS.Lib.DataField OP_ITEMCODPRODUTO = new PS.Lib.DataField("CODPRODUTO", null);
                            PS.Lib.DataField OP_ITEMQUANTIDADE = new PS.Lib.DataField("QUANTIDADE", 0);
                            PS.Lib.DataField OP_ITEMVLUNITARIO = new PS.Lib.DataField("VLUNITARIO", 0);
                            PS.Lib.DataField OP_ITEMVLDESCONTO = new PS.Lib.DataField("VLDESCONTO", 0);
                            PS.Lib.DataField OP_ITEMPRDESCONTO = new PS.Lib.DataField("PRDESCONTO", 0);
                            PS.Lib.DataField OP_ITEMVLTOTALITEM = new PS.Lib.DataField("VLTOTALITEM", 0);
                            PS.Lib.DataField OP_ITEMCODNATUREZA = new PS.Lib.DataField("CODNATUREZA", null);
                            PS.Lib.DataField OP_ITEMCAMPOLIVRE1 = new PS.Lib.DataField("CAMPOLIVRE1", null);
                            PS.Lib.DataField OP_ITEMCAMPOLIVRE2 = new PS.Lib.DataField("CAMPOLIVRE2", null);
                            PS.Lib.DataField OP_ITEMCAMPOLIVRE3 = new PS.Lib.DataField("CAMPOLIVRE3", null);
                            PS.Lib.DataField OP_ITEMCAMPOLIVRE4 = new PS.Lib.DataField("CAMPOLIVRE4", null);
                            PS.Lib.DataField OP_ITEMCAMPOLIVRE5 = new PS.Lib.DataField("CAMPOLIVRE5", null);
                            PS.Lib.DataField OP_ITEMCAMPOLIVRE6 = new PS.Lib.DataField("CAMPOLIVRE6", null);
                            PS.Lib.DataField OP_ITEMDATAEXTRA1 = new PS.Lib.DataField("DATAEXTRA1", null);
                            PS.Lib.DataField OP_ITEMDATAEXTRA2 = new PS.Lib.DataField("DATAEXTRA2", null);
                            PS.Lib.DataField OP_ITEMDATAEXTRA3 = new PS.Lib.DataField("DATAEXTRA3", null);
                            PS.Lib.DataField OP_ITEMDATAEXTRA4 = new PS.Lib.DataField("DATAEXTRA4", null);
                            PS.Lib.DataField OP_ITEMDATAEXTRA5 = new PS.Lib.DataField("DATAEXTRA5", null);
                            PS.Lib.DataField OP_ITEMDATAEXTRA6 = new PS.Lib.DataField("DATAEXTRA6", null);
                            PS.Lib.DataField OP_ITEMCAMPOVALOR1 = new PS.Lib.DataField("CAMPOVALOR1", 0);
                            PS.Lib.DataField OP_ITEMCAMPOVALOR2 = new PS.Lib.DataField("CAMPOVALOR2", 0);
                            PS.Lib.DataField OP_ITEMCAMPOVALOR3 = new PS.Lib.DataField("CAMPOVALOR3", 0);
                            PS.Lib.DataField OP_ITEMCAMPOVALOR4 = new PS.Lib.DataField("CAMPOVALOR4", 0);
                            PS.Lib.DataField OP_ITEMCAMPOVALOR5 = new PS.Lib.DataField("CAMPOVALOR5", 0);
                            PS.Lib.DataField OP_ITEMCAMPOVALOR6 = new PS.Lib.DataField("CAMPOVALOR6", 0);
                            PS.Lib.DataField OP_ITEMCODUNIDOPER = new PS.Lib.DataField("CODUNIDOPER", null);
                            PS.Lib.DataField OP_ITEMOBSERVACAO = new PS.Lib.DataField("OBSERVACAO", null);
                            PS.Lib.DataField OP_ITEMINFCOMPL = new PS.Lib.DataField("INFCOMPL", null);
                            PS.Lib.DataField OP_ITEMCODTABPRECO = new PS.Lib.DataField("CODTABPRECO", null);


                            PS.Lib.DataField OP_QUANTIDADE_FATURADO = new PS.Lib.DataField("QUANTIDADE_FATURADO", 0);
                            PS.Lib.DataField OP_QUANTIDADE_SALDO = new PS.Lib.DataField("QUANTIDADE_SALDO", 0);
                            PS.Lib.DataField OP_UFIBPTAX = new PS.Lib.DataField("UFIBPTAX", null);
                            PS.Lib.DataField OP_NACIONALFEDERALIBPTAX = new PS.Lib.DataField("NACIONALFEDERALIBPTAX", null);
                            PS.Lib.DataField OP_IMPORTADOSFEDERALIBPTAX = new PS.Lib.DataField("IMPORTADOSFEDERALIBPTAX", null);
                            PS.Lib.DataField OP_ESTADUALIBPTAX = new PS.Lib.DataField("ESTADUALIBPTAX", null);
                            PS.Lib.DataField OP_MUNICIPALIBPTAX = new PS.Lib.DataField("MUNICIPALIBPTAX", null);
                            PS.Lib.DataField OP_CHAVEIBPTAX = new PS.Lib.DataField("CHAVEIBPTAX", null);
                            PS.Lib.DataField OP_APLICACAOMATERIAL = new PS.Lib.DataField("APLICACAOMATERIAL", null);
                            PS.Lib.DataField OP_VLACRESCIMO = new PS.Lib.DataField("VLACRESCIMO", 0);
                            PS.Lib.DataField OP_PRACRESCIMO = new PS.Lib.DataField("PRACRESCIMO", 0);
                            PS.Lib.DataField OP_TIPODESCONTO = new PS.Lib.DataField("TIPODESCONTO", null);
                            PS.Lib.DataField OP_VLUNITORIGINAL = new PS.Lib.DataField("VLUNITORIGINAL", 0);

                            PS.Lib.DataField OP_TOTALEDITADO = new PS.Lib.DataField("TOTALEDITADO", 0);


                            #endregion

                            PSPartOperacaoItemData psPartOperacaoItemData = new PSPartOperacaoItemData();
                            psPartOperacaoItemData._tablename = "GOPERITEM";
                            psPartOperacaoItemData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM" };

                            PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                            psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
                            psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };

                            for (int i = 0; i < GOPERITEMORIGEM.Rows.Count; i++)
                            {
                                #region Sets

                                OP_ITEMCODEMPRESA.Valor = OP_CODEMPRESA.Valor;
                                OP_ITEMCODOPER.Valor = OP_CODOPER.Valor;

                                OP_ITEMNSEQITEM.Valor = 0;
                                OP_ITEMCODPRODUTO.Valor = GOPERITEMORIGEM.Rows[i]["CODPRODUTO"];
                                OP_ITEMQUANTIDADE.Valor = GOPERITEMORIGEM.Rows[i]["QUANTIDADE"];
                                OP_ITEMCODTABPRECO.Valor = GOPERITEMORIGEM.Rows[i]["CODTABPRECO"];


                                OP_QUANTIDADE_FATURADO.Valor = GOPERITEMORIGEM.Rows[i]["QUANTIDADE_FATURADO"];
                                OP_QUANTIDADE_SALDO.Valor = GOPERITEMORIGEM.Rows[i]["QUANTIDADE_SALDO"];
                                OP_UFIBPTAX.Valor = GOPERITEMORIGEM.Rows[i]["UFIBPTAX"];
                                OP_NACIONALFEDERALIBPTAX.Valor = GOPERITEMORIGEM.Rows[i]["NACIONALFEDERALIBPTAX"];
                                OP_IMPORTADOSFEDERALIBPTAX.Valor = GOPERITEMORIGEM.Rows[i]["IMPORTADOSFEDERALIBPTAX"];
                                OP_ESTADUALIBPTAX.Valor = GOPERITEMORIGEM.Rows[i]["ESTADUALIBPTAX"];
                                OP_MUNICIPALIBPTAX.Valor = GOPERITEMORIGEM.Rows[i]["MUNICIPALIBPTAX"];
                                OP_CHAVEIBPTAX.Valor = GOPERITEMORIGEM.Rows[i]["CHAVEIBPTAX"];
                                OP_APLICACAOMATERIAL.Valor = GOPERITEMORIGEM.Rows[i]["APLICACAOMATERIAL"];
                                OP_VLACRESCIMO.Valor = GOPERITEMORIGEM.Rows[i]["VLACRESCIMO"];
                                OP_PRACRESCIMO.Valor = GOPERITEMORIGEM.Rows[i]["PRACRESCIMO"];
                                OP_TIPODESCONTO.Valor = GOPERITEMORIGEM.Rows[i]["TIPODESCONTO"];
                                OP_VLUNITORIGINAL.Valor = GOPERITEMORIGEM.Rows[i]["VLUNITORIGINAL"];
                                OP_TOTALEDITADO.Valor = GOPERITEMORIGEM.Rows[i]["TOTALEDITADO"];

                                #region Valor Unitário

                                //Edita
                                if (PARAMTIPOPERDESTINO["USAVLUNITARIO"].ToString() == "E")
                                {
                                    if (GOPERITEMORIGEM.Rows[0].ItemArray[GOPERITEMORIGEM.Columns.IndexOf("VLUNITARIO")] == DBNull.Value)
                                    {
                                        OP_ITEMVLUNITARIO.Valor = 0;
                                    }
                                    else
                                    {
                                        OP_ITEMVLUNITARIO.Valor = GOPERITEMORIGEM.Rows[i]["VLUNITARIO"];
                                    }
                                }

                                //Não Edita
                                if (PARAMTIPOPERDESTINO["USAVLUNITARIO"].ToString() == "N")
                                {
                                    if (GOPERITEMORIGEM.Rows[0].ItemArray[GOPERITEMORIGEM.Columns.IndexOf("VLUNITARIO")] == DBNull.Value)
                                    {
                                        OP_ITEMVLUNITARIO.Valor = 0;
                                    }
                                    else
                                    {
                                        OP_ITEMVLUNITARIO.Valor = GOPERITEMORIGEM.Rows[i]["VLUNITARIO"];
                                    }
                                }

                                //Não Mostra
                                if (PARAMTIPOPERDESTINO["USAVLUNITARIO"].ToString() == "M")
                                {
                                    OP_ITEMVLUNITARIO.Valor = 0;
                                }

                                #endregion

                                #region Valor Desconto

                                //Edita
                                if (PARAMTIPOPERDESTINO["USAVLDESCONTO"].ToString() == "E")
                                {
                                    if (GOPERITEMORIGEM.Rows[0].ItemArray[GOPERITEMORIGEM.Columns.IndexOf("VLDESCONTO")] == DBNull.Value)
                                    {
                                        OP_ITEMVLDESCONTO.Valor = 0;
                                    }
                                    else
                                    {
                                        OP_ITEMVLDESCONTO.Valor = GOPERITEMORIGEM.Rows[i]["VLDESCONTO"];
                                    }
                                }

                                //Não Edita
                                if (PARAMTIPOPERDESTINO["USAVLDESCONTO"].ToString() == "N")
                                {
                                    if (GOPERITEMORIGEM.Rows[0].ItemArray[GOPERITEMORIGEM.Columns.IndexOf("VLDESCONTO")] == DBNull.Value)
                                    {
                                        OP_ITEMVLDESCONTO.Valor = 0;
                                    }
                                    else
                                    {
                                        OP_ITEMVLDESCONTO.Valor = GOPERITEMORIGEM.Rows[i]["VLDESCONTO"];
                                    }
                                }

                                //Não Mostra
                                if (PARAMTIPOPERDESTINO["USAVLDESCONTO"].ToString() == "M")
                                {
                                    OP_ITEMVLDESCONTO.Valor = 0;
                                }

                                #endregion

                                #region Perc. Desconto

                                //Edita
                                if (PARAMTIPOPERDESTINO["USAPRDESCONTO"].ToString() == "E")
                                {
                                    if (GOPERITEMORIGEM.Rows[0].ItemArray[GOPERITEMORIGEM.Columns.IndexOf("PRDESCONTO")] == DBNull.Value)
                                    {
                                        OP_ITEMPRDESCONTO.Valor = 0;
                                    }
                                    else
                                    {
                                        OP_ITEMPRDESCONTO.Valor = GOPERITEMORIGEM.Rows[i]["PRDESCONTO"];
                                    }
                                }

                                //Não Edita
                                if (PARAMTIPOPERDESTINO["USAPRDESCONTO"].ToString() == "N")
                                {
                                    if (GOPERITEMORIGEM.Rows[0].ItemArray[GOPERITEMORIGEM.Columns.IndexOf("PRDESCONTO")] == DBNull.Value)
                                    {
                                        OP_ITEMPRDESCONTO.Valor = 0;
                                    }
                                    else
                                    {
                                        OP_ITEMPRDESCONTO.Valor = GOPERITEMORIGEM.Rows[i]["PRDESCONTO"];
                                    }
                                }

                                //Não Mostra
                                if (PARAMTIPOPERDESTINO["USAPRDESCONTO"].ToString() == "M")
                                {
                                    OP_ITEMPRDESCONTO.Valor = 0;
                                }

                                #endregion

                                #region Valor Total Item

                                //Edita
                                if (PARAMTIPOPERDESTINO["USAVLTOTALITEM"].ToString() == "E")
                                {
                                    if (GOPERITEMORIGEM.Rows[0].ItemArray[GOPERITEMORIGEM.Columns.IndexOf("VLTOTALITEM")] == DBNull.Value)
                                    {
                                        OP_ITEMVLTOTALITEM.Valor = 0;
                                    }
                                    else
                                    {
                                        OP_ITEMVLTOTALITEM.Valor = GOPERITEMORIGEM.Rows[i]["VLTOTALITEM"];
                                    }
                                }

                                //Não Edita
                                if (PARAMTIPOPERDESTINO["USAVLTOTALITEM"].ToString() == "N")
                                {
                                    if (GOPERITEMORIGEM.Rows[0].ItemArray[GOPERITEMORIGEM.Columns.IndexOf("VLTOTALITEM")] == DBNull.Value)
                                    {
                                        OP_ITEMVLTOTALITEM.Valor = 0;
                                    }
                                    else
                                    {
                                        OP_ITEMVLTOTALITEM.Valor = GOPERITEMORIGEM.Rows[i]["VLTOTALITEM"];
                                    }
                                }

                                //Não Mostra
                                if (PARAMTIPOPERDESTINO["USAVLTOTALITEM"].ToString() == "M")
                                {
                                    OP_ITEMVLTOTALITEM.Valor = 0;
                                }

                                #endregion

                                OP_ITEMOBSERVACAO.Valor = GOPERITEMORIGEM.Rows[i]["OBSERVACAO"];
                                OP_ITEMINFCOMPL.Valor = GOPERITEMORIGEM.Rows[i]["INFCOMPL"];
                                OP_ITEMCODUNIDOPER.Valor = GOPERITEMORIGEM.Rows[i]["CODUNIDOPER"];

                                #region Natureza

                                //Edita
                                if (PARAMTIPOPERDESTINO["USANATUREZA"].ToString() == "E")
                                {
                                    if (GOPERITEMORIGEM.Rows[0].ItemArray[GOPERITEMORIGEM.Columns.IndexOf("CODNATUREZA")] == DBNull.Value)
                                    {
                                        if (GOPERITEMORIGEM.Rows[0]["CODNATUREZA"] == DBNull.Value)
                                        {
                                            OP_ITEMCODNATUREZA.Valor = this.DefineNatureza(
                                                Convert.ToInt32(OP_CODEMPRESA.Valor),
                                                Convert.ToInt32(OP_CODOPER.Valor),
                                                (PARAMTIPOPERDESTINO["CODNATDENTRO"] == DBNull.Value) ? null : PARAMTIPOPERDESTINO["CODNATDENTRO"].ToString(),
                                                (PARAMTIPOPERDESTINO["CODNATFORA"] == DBNull.Value) ? null : PARAMTIPOPERDESTINO["CODNATFORA"].ToString());
                                        }
                                        else
                                        {
                                            OP_ITEMCODNATUREZA.Valor = GOPERORIGEM.Rows[0]["CODNATUREZA"];
                                        }
                                    }
                                    else
                                    {
                                        OP_ITEMCODNATUREZA.Valor = GOPERITEMORIGEM.Rows[i]["CODNATUREZA"];
                                    }
                                }

                                //Não Edita
                                if (PARAMTIPOPERDESTINO["USANATUREZA"].ToString() == "N")
                                {
                                    if (GOPERITEMORIGEM.Rows[0].ItemArray[GOPERITEMORIGEM.Columns.IndexOf("CODNATUREZA")] == DBNull.Value)
                                    {
                                        if (GOPERITEMORIGEM.Rows[0]["CODNATUREZA"] == DBNull.Value)
                                        {
                                            OP_ITEMCODNATUREZA.Valor = this.DefineNatureza(
                                                Convert.ToInt32(OP_CODEMPRESA.Valor),
                                                Convert.ToInt32(OP_CODOPER.Valor),
                                                (PARAMTIPOPERDESTINO["CODNATDENTRO"] == DBNull.Value) ? null : PARAMTIPOPERDESTINO["CODNATDENTRO"].ToString(),
                                                (PARAMTIPOPERDESTINO["CODNATFORA"] == DBNull.Value) ? null : PARAMTIPOPERDESTINO["CODNATFORA"].ToString());
                                        }
                                        else
                                        {
                                            OP_ITEMCODNATUREZA.Valor = GOPERITEMORIGEM.Rows[0]["CODNATUREZA"];
                                        }
                                    }
                                    else
                                    {
                                        OP_ITEMCODNATUREZA.Valor = GOPERITEMORIGEM.Rows[i]["CODNATUREZA"];
                                    }
                                }

                                //Não Mostra
                                if (PARAMTIPOPERDESTINO["USANATUREZA"].ToString() == "M")
                                {
                                    OP_ITEMCODNATUREZA.Valor = null;
                                }

                                #endregion

                                #region Campo Livre

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMCAMPOLIVRE1"].ToString()) == 1)
                                    OP_ITEMCAMPOLIVRE1.Valor = GOPERITEMORIGEM.Rows[i]["CAMPOLIVRE1"];

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMCAMPOLIVRE2"].ToString()) == 1)
                                    OP_ITEMCAMPOLIVRE2.Valor = GOPERITEMORIGEM.Rows[i]["CAMPOLIVRE2"];

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMCAMPOLIVRE3"].ToString()) == 1)
                                    OP_ITEMCAMPOLIVRE3.Valor = GOPERITEMORIGEM.Rows[i]["CAMPOLIVRE3"];

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMCAMPOLIVRE4"].ToString()) == 1)
                                    OP_ITEMCAMPOLIVRE4.Valor = GOPERITEMORIGEM.Rows[i]["CAMPOLIVRE4"];

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMCAMPOLIVRE5"].ToString()) == 1)
                                    OP_ITEMCAMPOLIVRE5.Valor = GOPERITEMORIGEM.Rows[i]["CAMPOLIVRE5"];

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMCAMPOLIVRE6"].ToString()) == 1)
                                    OP_ITEMCAMPOLIVRE6.Valor = GOPERITEMORIGEM.Rows[i]["CAMPOLIVRE6"];

                                #endregion

                                #region Campo Data Extra

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMDATAEXTRA1"].ToString()) == 1)
                                    OP_ITEMDATAEXTRA1.Valor = GOPERITEMORIGEM.Rows[i]["DATAEXTRA1"];

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMDATAEXTRA2"].ToString()) == 1)
                                    OP_ITEMDATAEXTRA2.Valor = GOPERITEMORIGEM.Rows[i]["DATAEXTRA2"];

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMDATAEXTRA3"].ToString()) == 1)
                                    OP_ITEMDATAEXTRA3.Valor = GOPERITEMORIGEM.Rows[i]["DATAEXTRA3"];

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMDATAEXTRA4"].ToString()) == 1)
                                    OP_ITEMDATAEXTRA4.Valor = GOPERITEMORIGEM.Rows[i]["DATAEXTRA4"];

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMDATAEXTRA5"].ToString()) == 1)
                                    OP_ITEMDATAEXTRA5.Valor = GOPERITEMORIGEM.Rows[i]["DATAEXTRA5"];

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMDATAEXTRA6"].ToString()) == 1)
                                    OP_ITEMDATAEXTRA6.Valor = GOPERITEMORIGEM.Rows[i]["DATAEXTRA6"];

                                #endregion

                                #region Campo Valor

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMCAMPOVALOR1"].ToString()) == 1)
                                    OP_ITEMCAMPOVALOR1.Valor = GOPERITEMORIGEM.Rows[i]["CAMPOVALOR1"];

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMCAMPOVALOR2"].ToString()) == 1)
                                    OP_ITEMCAMPOVALOR2.Valor = GOPERITEMORIGEM.Rows[i]["CAMPOVALOR2"];

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMCAMPOVALOR3"].ToString()) == 1)
                                    OP_ITEMCAMPOVALOR3.Valor = GOPERITEMORIGEM.Rows[i]["CAMPOVALOR3"];

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMCAMPOVALOR4"].ToString()) == 1)
                                    OP_ITEMCAMPOVALOR4.Valor = GOPERITEMORIGEM.Rows[i]["CAMPOVALOR4"];

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMCAMPOVALOR5"].ToString()) == 1)
                                    OP_ITEMCAMPOVALOR5.Valor = GOPERITEMORIGEM.Rows[i]["CAMPOVALOR5"];

                                if (int.Parse(PARAMTIPOPERDESTINO["COPIAITEMCAMPOVALOR6"].ToString()) == 1)
                                    OP_ITEMCAMPOVALOR6.Valor = GOPERITEMORIGEM.Rows[i]["CAMPOVALOR6"];

                                #endregion

                                OP_QUANTIDADE_FATURADO.Valor = GOPERITEMORIGEM.Rows[i]["QUANTIDADE_FATURADO"];
                                OP_QUANTIDADE_SALDO.Valor = GOPERITEMORIGEM.Rows[i]["QUANTIDADE_SALDO"];
                                OP_UFIBPTAX.Valor = GOPERITEMORIGEM.Rows[i]["UFIBPTAX"];
                                OP_NACIONALFEDERALIBPTAX.Valor = GOPERITEMORIGEM.Rows[i]["NACIONALFEDERALIBPTAX"];
                                OP_IMPORTADOSFEDERALIBPTAX.Valor = GOPERITEMORIGEM.Rows[i]["IMPORTADOSFEDERALIBPTAX"];
                                OP_ESTADUALIBPTAX.Valor = GOPERITEMORIGEM.Rows[i]["ESTADUALIBPTAX"];
                                OP_MUNICIPALIBPTAX.Valor = GOPERITEMORIGEM.Rows[i]["MUNICIPALIBPTAX"];
                                OP_CHAVEIBPTAX.Valor = GOPERITEMORIGEM.Rows[i]["CHAVEIBPTAX"];
                                OP_APLICACAOMATERIAL.Valor = GOPERITEMORIGEM.Rows[i]["APLICACAOMATERIAL"];
                                OP_VLACRESCIMO.Valor = GOPERITEMORIGEM.Rows[i]["VLACRESCIMO"];
                                OP_PRACRESCIMO.Valor = GOPERITEMORIGEM.Rows[i]["PRACRESCIMO"];
                                OP_TIPODESCONTO.Valor = GOPERITEMORIGEM.Rows[i]["TIPODESCONTO"];
                                OP_VLUNITORIGINAL.Valor = GOPERITEMORIGEM.Rows[i]["VLUNITORIGINAL"];




                                #endregion

                                #region Item da Operação

                                if (FAT_ULTIMONIVEL == 0)
                                {
                                    List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
                                    ListObjArr.Add(OP_ITEMCODEMPRESA);
                                    ListObjArr.Add(OP_ITEMCODOPER);
                                    ListObjArr.Add(OP_ITEMNSEQITEM);
                                    ListObjArr.Add(OP_ITEMCODPRODUTO);
                                    ListObjArr.Add(OP_ITEMQUANTIDADE);
                                    ListObjArr.Add(OP_ITEMCODTABPRECO);
                                    ListObjArr.Add(OP_ITEMVLUNITARIO);
                                    ListObjArr.Add(OP_ITEMVLDESCONTO);
                                    ListObjArr.Add(OP_ITEMPRDESCONTO);
                                    ListObjArr.Add(OP_ITEMVLTOTALITEM);
                                    ListObjArr.Add(OP_ITEMCODNATUREZA);
                                    ListObjArr.Add(OP_ITEMOBSERVACAO);
                                    ListObjArr.Add(OP_ITEMINFCOMPL);
                                    ListObjArr.Add(OP_ITEMCODUNIDOPER);

                                    ListObjArr.Add(OP_ITEMCAMPOLIVRE1);
                                    ListObjArr.Add(OP_ITEMCAMPOLIVRE2);
                                    ListObjArr.Add(OP_ITEMCAMPOLIVRE3);
                                    ListObjArr.Add(OP_ITEMCAMPOLIVRE4);
                                    ListObjArr.Add(OP_ITEMCAMPOLIVRE5);
                                    ListObjArr.Add(OP_ITEMCAMPOLIVRE6);

                                    ListObjArr.Add(OP_ITEMDATAEXTRA1);
                                    ListObjArr.Add(OP_ITEMDATAEXTRA2);
                                    ListObjArr.Add(OP_ITEMDATAEXTRA3);
                                    ListObjArr.Add(OP_ITEMDATAEXTRA4);
                                    ListObjArr.Add(OP_ITEMDATAEXTRA5);
                                    ListObjArr.Add(OP_ITEMDATAEXTRA6);

                                    ListObjArr.Add(OP_ITEMCAMPOVALOR1);
                                    ListObjArr.Add(OP_ITEMCAMPOVALOR2);
                                    ListObjArr.Add(OP_ITEMCAMPOVALOR3);
                                    ListObjArr.Add(OP_ITEMCAMPOVALOR4);
                                    ListObjArr.Add(OP_ITEMCAMPOVALOR5);
                                    ListObjArr.Add(OP_ITEMCAMPOVALOR6);

                                    ListObjArr.Add(OP_QUANTIDADE_FATURADO);
                                    ListObjArr.Add(OP_QUANTIDADE_SALDO);
                                    ListObjArr.Add(OP_UFIBPTAX);
                                    ListObjArr.Add(OP_NACIONALFEDERALIBPTAX);
                                    ListObjArr.Add(OP_IMPORTADOSFEDERALIBPTAX);
                                    ListObjArr.Add(OP_ESTADUALIBPTAX);
                                    ListObjArr.Add(OP_MUNICIPALIBPTAX);
                                    ListObjArr.Add(OP_CHAVEIBPTAX);
                                    ListObjArr.Add(OP_APLICACAOMATERIAL);
                                    ListObjArr.Add(OP_VLACRESCIMO);
                                    ListObjArr.Add(OP_PRACRESCIMO);
                                    ListObjArr.Add(OP_TIPODESCONTO);
                                    ListObjArr.Add(OP_VLUNITORIGINAL);

                                    //                                    string sql111 = @"INSERT INTO 
                                    //GOPERITEM 
                                    //(CODEMPRESA, CODOPER, NSEQITEM, CODPRODUTO, QUANTIDADE, VLUNITARIO, VLDESCONTO, PRDESCONTO, VLTOTALITEM, CODNATUREZA, CODUNIDOPER, OBSERVACAO, INFCOMPL, CODTABPRECO, QUANTIDADE_FATURADO, QUANTIDADE_SALDO, UFIBPTAX, NACIONALFEDERALIBPTAX, IMPORTADOSFEDERALIBPTAX, ESTADUALIBPTAX, MUNICIPALIBPTAX, CHAVEIBPTAX, APLICACAOMATERIAL, VLACRESCIMO, PRACRESCIMO, TIPODESCONTO, VLUNITORIGINAL, TOTALEDITADO, RATEIODESPESA, RATEIODESCONTO, RATEIOFRETE, RATEIOSEGURO) 
                                    //VALUES 
                                    //(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";


                                    //                                    int kk = AppLib.Context.poolConnection.Get("Start").ExecTransaction(sql111, new object[] { OP_ITEMCODEMPRESA.Valor, OP_ITEMCODOPER.Valor, i, OP_ITEMCODPRODUTO.Valor, OP_ITEMQUANTIDADE.Valor, OP_ITEMVLUNITARIO.Valor, OP_ITEMVLDESCONTO.Valor, OP_ITEMPRDESCONTO.Valor, OP_ITEMVLTOTALITEM.Valor, OP_ITEMCODNATUREZA.Valor, OP_ITEMCODUNIDOPER.Valor, OP_ITEMOBSERVACAO.Valor, OP_ITEMINFCOMPL.Valor, OP_ITEMCODTABPRECO.Valor, 0, 0, OP_UFIBPTAX.Valor, OP_NACIONALFEDERALIBPTAX.Valor, OP_IMPORTADOSFEDERALIBPTAX.Valor, OP_ESTADUALIBPTAX.Valor, OP_MUNICIPALIBPTAX.Valor, OP_CHAVEIBPTAX.Valor, OP_APLICACAOMATERIAL.Valor, 0,0,0,0,0,0,0,0,0 });


                                    psPartOperacaoItemData.SaveRecord(ListObjArr);
                                }

                                #endregion

                                #region Estoque

                                if (PARAMTIPOPERDESTINO["OPERESTOQUE"].ToString() != "N")
                                {
                                    PSPartLocalEstoqueSaldoData.Tipo Estoque;
                                    if (PARAMTIPOPERDESTINO["OPERESTOQUE"].ToString() == "A")
                                        Estoque = PSPartLocalEstoqueSaldoData.Tipo.Aumenta;
                                    else
                                        Estoque = PSPartLocalEstoqueSaldoData.Tipo.Diminui;

                                    psPartLocalEstoqueSaldoData.MovimentaEstoque(Convert.ToInt32(OP_CODEMPRESA.Valor),
                                        Convert.ToInt32(OP_CODFILIAL.Valor),
                                        (OP_CODLOCAL.Valor == null) ? string.Empty : OP_CODLOCAL.Valor.ToString(),
                                        (OP_CODLOCALENTREGA.Valor == null) ? string.Empty : OP_CODLOCALENTREGA.Valor.ToString(),
                                        (OP_ITEMCODPRODUTO.Valor == null) ? string.Empty : OP_ITEMCODPRODUTO.Valor.ToString(),
                                        Convert.ToDecimal(OP_ITEMQUANTIDADE.Valor),
                                        (OP_ITEMCODUNIDOPER.Valor == null) ? string.Empty : OP_ITEMCODUNIDOPER.Valor.ToString(),
                                        Estoque);

                                }

                                #endregion

                                #region Ordem de Produção

                                if (int.Parse(PARAMTIPOPERDESTINO["GERAORDEMPRODUCAO"].ToString()) == 1)
                                {
                                    sSql = @"SELECT CODESTRUTURA FROM PESTRUTURA WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND ATIVO = 1";

                                    DataTable PESTRUTURA = dbs.QuerySelect(sSql, OP_ITEMCODEMPRESA.Valor, OP_ITEMCODPRODUTO.Valor);

                                    if (PESTRUTURA.Rows.Count > 0)
                                    {

                                    }
                                }

                                #endregion
                            }
                        }
                        else
                        {
                            throw new Exception("Operação [" + dfNUMERO.Valor.ToString() + "] não possui itens cadastrados.");
                        }

                        #endregion

                        #region Atualiza Operação Origem

                        sSql = @"UPDATE GOPER SET CODSTATUS = 1, DATAFATURAMENTO = ?, CODUSUARIOFATURAMENTO = ? WHERE CODEMPRESA = ? AND CODOPER = ?";

                        dbs.QueryExec(sSql, DateTime.Now, PS.Lib.Contexto.Session.CodUsuario, dfCODEMPRESA.Valor, dfCODOPER.Valor);

                        #endregion

                        #region Atualiza Operação Destino

                        if (FAT_ULTIMONIVEL == 0)
                        {
                            //Atualiza Operação Destino caso opção Criar como Faturado estiver marcado
                            if (int.Parse(PARAMTIPOPERDESTINO["CRIAROPFAT"].ToString()) == 1)
                            {
                                sSql = @"UPDATE GOPER SET CODSTATUS = 1, DATAFATURAMENTO = ?, CODUSUARIOFATURAMENTO = ? WHERE CODEMPRESA = ? AND CODOPER = ?";

                                dbs.QueryExec(sSql, DateTime.Now, PS.Lib.Contexto.Session.CodUsuario, OP_CODEMPRESA.Valor, OP_CODOPER.Valor);
                            }
                        }

                        #endregion

                        #region Gera Relacionamento

                        if (FAT_ULTIMONIVEL == 0)
                        {
                            sSql = @"INSERT INTO GOPERRELAC (CODEMPRESA, CODOPER, CODOPERRELAC) VALUES (?,?,?)";

                            dbs.QueryExec(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor, OP_CODOPER.Valor);
                        }

                        #endregion

                        #region Mensagem Parametrizada

                        List<String> lMensagem = new List<string>();

                        if (FAT_ULTIMONIVEL == 1)
                        {
                            OP_HISTORICO.Valor = GOPERORIGEM.Rows[0]["HISTORICO"];
                        }
                        else
                        {
                            OP_HISTORICO.Valor = null;
                        }

                        if (OP_HISTORICO.Valor != null)
                        {
                            lMensagem.Add(OP_HISTORICO.Valor.ToString());
                        }

                        sSql = "SELECT CODMENSAGEM FROM GOPERMENSAGEM WHERE CODEMPRESA = ? AND CODTIPOPER = ?";

                        DataTable GOPERMENSAGEM = dbs.QuerySelect(sSql, OP_CODEMPRESA.Valor, OP_CODTIPOPER.Valor);

                        if (GOPERMENSAGEM.Rows.Count > 0)
                        {
                            for (int i = 0; i < GOPERMENSAGEM.Rows.Count; i++)
                            {
                                sSql = "SELECT CODFORMULAMENSAGEM FROM VOPERMENSAGEM WHERE CODEMPRESA = ? AND CODMENSAGEM = ?";

                                string sFormula = dbs.QueryValue(string.Empty, sSql, OP_CODEMPRESA.Valor, GOPERMENSAGEM.Rows[i]["CODMENSAGEM"].ToString()).ToString();

                                if (sFormula != string.Empty)
                                {
                                    PS.Lib.Contexto.Session.key1 = OP_CODEMPRESA.Valor;
                                    PS.Lib.Contexto.Session.key2 = OP_CODOPER.Valor;

                                    FRM_CODEMPRESA = int.Parse(OP_CODEMPRESA.Valor.ToString());
                                    FRM_CODFORMULA = sFormula;

                                    string sMensagem = string.Empty;
                                    try
                                    {
                                        interpreta.comando = function.PreparaFormula(FRM_CODEMPRESA, FRM_CODFORMULA);
                                        sMensagem = interpreta.Executar().ToString();
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception("Erro ao executar a fórmula: " + FRM_CODFORMULA + ".\r\n" + ex.Message);
                                    }

                                    if (sMensagem != string.Empty)
                                    {
                                        lMensagem.Add(sMensagem);
                                    }
                                }
                                else
                                {
                                    sSql = "SELECT MENSAGEM FROM VOPERMENSAGEM WHERE CODEMPRESA = ? AND CODMENSAGEM = ?";

                                    string sMensagem = dbs.QueryValue(string.Empty, sSql, OP_CODEMPRESA.Valor, GOPERMENSAGEM.Rows[i]["CODMENSAGEM"].ToString()).ToString();

                                    if (sMensagem != string.Empty)
                                    {
                                        lMensagem.Add(sMensagem);
                                    }
                                }
                            }
                        }

                        if (lMensagem.Count > 0)
                        {
                            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();

                            for (int i = 0; i < lMensagem.Count; i++)
                            {
                                txt.AppendText(string.Concat(lMensagem[i], Environment.NewLine));
                            }

                            sSql = @"UPDATE GOPER SET HISTORICO = ? WHERE CODEMPRESA = ? AND CODOPER = ?";

                            dbs.QueryExec(sSql, txt.Text, OP_CODEMPRESA.Valor, OP_CODOPER.Valor);

                            txt.Dispose();
                        }

                        #endregion

                        #region Limpa Chave Temporaria

                        PS.Lib.Contexto.Session.key1 = null;
                        PS.Lib.Contexto.Session.key2 = null;
                        PS.Lib.Contexto.Session.key3 = null;
                        PS.Lib.Contexto.Session.key4 = null;
                        PS.Lib.Contexto.Session.key5 = null;

                        #endregion

                        #region realiza o update na série

                        System.Data.DataRow PARAMTIPOPER = gb.RetornaParametrosOperacao(FAT_PROXIMA_ETAPA);
                        if (PARAMTIPOPER == null)
                        {
                            throw new Exception("Não foi possível gerar o número da operação pois o tipo da operação não esta parametrizado.");
                        }
                        string sql = @"UPDATE VSERIE SET NUMSEQ = ? WHERE CODSERIE = ? AND CODEMPRESA = ?";
                        // AppLib.Context.poolConnection.Get("Start").ExecTransaction(sql, new object[] { OP_NUMERO.Valor, PARAMTIPOPER["SERIEDEFAULT"], AppLib.Context.Empresa });
                        dbs.QueryExec(sql, new Object[] { OP_NUMERO.Valor, PARAMTIPOPER["SERIEDEFAULT"], AppLib.Context.Empresa });

                        #endregion

                        #endregion

                        // dbs.Commit();

                    }
                    catch (Exception ex)
                    {
                        dbs.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    throw new Exception("Erro inexperado.");
                }

                #endregion

                return objArrDDL;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public void faturarParcial(Class.GoperItem itens, string proximaEtapa, Class.Goper operacao)
        //{
        //    //Escrevendo o log
        //    AppLib.Util.Log.Escrever("Faturando a operação " + operacao.CODEMPRESA.ToString() + ";" + operacao.CODOPER.ToString());

        //    #region Verifica Situção

        //    if (VerificaSituacao(operacao.CODEMPRESA, operacao.CODOPER))
        //    {
        //        throw new Exception("Operação [ " + dfNUMERO.Valor + " ] não pode ser faturada pois não está em aberto.");
        //    }

        //    #endregion

        //    #region Define Próxima Etapa

        //    if (proximaEtapa != string.Empty)
        //    {
        //        if (string.IsNullOrEmpty(getParametroOperacao(proximaEtapa)))
        //        {
        //            throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação [" + proximaEtapa + "].");
        //        }
        //    }
        //    else
        //    {
        //        //Quando não há próxima etapa definida, verifica se a operação fatura para ela mesma

        //        if (string.IsNullOrEmpty(getParametroOperacao(proximaEtapa)))
        //        {
        //            throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação [" + operacao.CODTIPOPER + "] selecionada.");
        //        }
        //        if (string.IsNullOrEmpty(getParametroOperacao(proximaEtapa)).Equals("0"))
        //        {
        //            throw new Exception("Tipo de operação [" + operacao.CODTIPOPER + "] não esta definida como sendo último nível.");
        //        }
        //    }

        //    #endregion
        //}

        private bool verificaLancVencidos(string codTipOper, string codCliFor)
        {
            Class.VerificaLancamentoVencido _verificaLancamentoVencido = new Class.VerificaLancamentoVencido();
            if (_verificaLancamentoVencido.verificaParametroVencimento(codTipOper, codCliFor))
            {
                return true;
            }
            return false;
        }

        public void VoltaStatusRelacionamento(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            string sSql = @"SELECT CODOPER FROM GOPERRELAC WHERE CODEMPRESA = ? AND CODOPERRELAC = ?";

            DataTable dt = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sSql = @"UPDATE GOPER SET CODSTATUS = 0, DATAFATURAMENTO = NULL, CODUSUARIOFATURAMENTO = NULL WHERE CODEMPRESA = ? AND CODOPER = ?";

                    dbs.QueryExec(sSql, dfCODEMPRESA.Valor, dt.Rows[i]["CODOPER"]);
                }
            }
        }

        public void ExcluiRelacionamento(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            string sSql = @"DELETE FROM GOPERRELAC WHERE CODEMPRESA = ? AND CODOPERRELAC = ?";

            dbs.QueryExec(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor);
        }

        public int VerificaSituacao(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "GOPER.CODEMPRESA");
            PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(objArr, "GOPER.CODOPER");

            sSql = @"SELECT CODSTATUS FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";

            return int.Parse(dbs.QueryValue("0", sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor).ToString());
        }

        public bool ValidaNumeroOperacao(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNUMERO = gb.RetornaDataFieldByCampo(objArr, "NUMERO");
            PS.Lib.DataField dfCODSERIE = gb.RetornaDataFieldByCampo(objArr, "CODSERIE");
            PS.Lib.DataField dfCODCLIFOR = gb.RetornaDataFieldByCampo(objArr, "CODCLIFOR");
            PS.Lib.DataField dfCODTIPOPER = gb.RetornaDataFieldByCampo(objArr, "CODTIPOPER");
            PS.Lib.DataField dfCODFILIAL = gb.RetornaDataFieldByCampo(objArr, "CODFILIAL");

            string sSql = string.Empty;
            string sSerie = string.Empty;
            string sCodCliFor = string.Empty;
            DataRow PARAMTIPOPER = gb.RetornaParametrosOperacao(dfCODTIPOPER.Valor.ToString());
            if (PARAMTIPOPER == null)
            {
                throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação [" + dfCODTIPOPER.Valor + "].");
            }
            else
            {
                if (dfNUMERO.Valor == null)
                {
                    throw new Exception(gb.MensagemDeValidacao(this._tablename, dfNUMERO.Field));
                }
                else
                {
                    sSql = @"SELECT CODOPER FROM GOPER WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPER <> ? AND NUMERO = ? ";

                    if (PARAMTIPOPER["OPERSERIE"].ToString() != "M")
                    {
                        sSerie = " AND CODSERIE = ? ";
                    }

                    if (PARAMTIPOPER["CODCLIFOR"].ToString() != "M")
                    {
                        sCodCliFor = " AND CODCLIFOR = ? ";
                    }

                    if (string.IsNullOrEmpty(sSerie))
                    {
                        if (string.IsNullOrEmpty(sCodCliFor))
                        {
                            return dbs.QueryFind(sSql, dfCODEMPRESA.Valor, dfCODFILIAL.Valor, dfCODOPER.Valor, dfNUMERO.Valor);
                        }
                        else
                        {
                            if (PARAMTIPOPER["TIPENTSAI"].ToString() == "E")
                            {
                                if (dfCODCLIFOR.Valor == null)
                                    throw new Exception("Não foi possível validar o número da operação pois o Cliente/Fornecedor não foi informado.");

                                sSql = string.Concat(sSql, sCodCliFor);
                                return dbs.QueryFind(sSql, dfCODEMPRESA.Valor, dfCODFILIAL.Valor, dfCODOPER.Valor, dfNUMERO.Valor, dfCODCLIFOR.Valor);
                            }
                            else
                            {
                                sSql = string.Concat(sSql);
                                return dbs.QueryFind(sSql, dfCODEMPRESA.Valor, dfCODFILIAL.Valor, dfCODOPER.Valor, dfNUMERO.Valor);
                            }
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(sCodCliFor))
                        {
                            if (dfCODSERIE.Valor == null)
                                throw new Exception("Não foi possível validar o número da operação pois a Série não foi informada.");

                            sSql = string.Concat(sSql, sSerie);
                            return dbs.QueryFind(sSql, dfCODEMPRESA.Valor, dfCODFILIAL.Valor, dfCODOPER.Valor, dfNUMERO.Valor, dfCODSERIE.Valor);
                        }
                        else
                        {
                            if (PARAMTIPOPER["TIPENTSAI"].ToString() == "E")
                            {
                                if (dfCODCLIFOR.Valor == null)
                                    throw new Exception("Não foi possível validar o número da operação pois o Cliente/Fornecedor não foi informado.");

                                if (dfCODSERIE.Valor == null)
                                    throw new Exception("Não foi possível validar o número da operação pois a Série não foi informada.");

                                sSql = string.Concat(sSql, sSerie, sCodCliFor);
                                return dbs.QueryFind(sSql, dfCODEMPRESA.Valor, dfCODFILIAL.Valor, dfCODOPER.Valor, dfNUMERO.Valor, dfCODSERIE.Valor, dfCODCLIFOR.Valor);
                            }
                            else
                            {
                                if (dfCODSERIE.Valor == null)
                                    throw new Exception("Não foi possível validar o número da operação pois a Série não foi informada.");

                                sSql = string.Concat(sSql, sSerie);
                                return dbs.QueryFind(sSql, dfCODEMPRESA.Valor, dfCODFILIAL.Valor, dfCODOPER.Valor, dfNUMERO.Valor, dfCODSERIE.Valor);
                            }
                        }
                    }
                }
            }
        }

        public bool PossuiNFEstadual(int CodEmpresa, int CodOper, bool cancela)
        {
            string sSql = string.Empty;
            string CodTipOper = dbs.QueryValue(string.Empty, "SELECT CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", CodEmpresa, CodOper).ToString();

            if (string.IsNullOrEmpty(CodTipOper))
            {
                throw new Exception("Tipo de Operação não encontrada.");
            }

            DataRow rTipoOperacao = gb.RetornaParametrosOperacao(CodTipOper);
            if (rTipoOperacao["USAOPERACAONFE"] == DBNull.Value)
            {
                return false;
            }

            if (Convert.ToInt32(rTipoOperacao["USAOPERACAONFE"]) == 0)
            {
                return false;
            }
            if (cancela.Equals(true))
            {
                sSql = @"SELECT CODOPER FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ? AND CODSTATUS NOT IN ('P','E', 'C')";
            }
            else
            {
                sSql = @"SELECT CODOPER FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ? AND CODSTATUS NOT IN ('P','E')";
            }

            return dbs.QueryFind(sSql, CodEmpresa, CodOper);
        }

        public bool PossuiRelacionamento(int CodEmpresa, int CodOper, bool Cancelado)
        {
            string sSql = string.Empty;

            sSql = @"SELECT CODOPERRELAC 
FROM GOPERRELAC 
WHERE CODEMPRESA = ? AND CODOPER = ?
AND CODOPERRELAC IN (SELECT CODOPER FROM GOPER WHERE CODEMPRESA = GOPERRELAC.CODEMPRESA AND CODOPER = GOPERRELAC.CODOPERRELAC /**/)";

            if (Cancelado)
                sSql = sSql.Replace("/**/", "AND CODSTATUS <> 2");

            return dbs.QueryFind(sSql, CodEmpresa, CodOper);
        }

        public bool PossuiRelacionamentoPai(int CodEmpresa, int CodOper, bool Cancelado)
        {
            string sSql = string.Empty;

            sSql = @"SELECT CODOPER 
FROM GOPERRELAC 
WHERE CODEMPRESA = ? AND CODOPERRELAC = ?
AND CODOPER IN (SELECT CODOPER FROM GOPER WHERE CODEMPRESA = GOPERRELAC.CODEMPRESA AND CODOPER = GOPERRELAC.CODOPER /**/)";

            if (Cancelado)
                sSql = sSql.Replace("/**/", "AND CODSTATUS <> 2");

            return dbs.QueryFind(sSql, CodEmpresa, CodOper);
        }

        public bool PossuiFinanceiroBaixado(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            sSql = @"SELECT CODLANCA FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ? AND CODSTATUS = 1";

            return dbs.QueryFind(sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor);
        }

        public void ExcluiFinanceiro(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            sSql = @"SELECT CODLANCA FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ?";

            DataTable dtLanca = dbs.QuerySelect(sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor);

            List<PS.Lib.DataField> objArr1 = new List<PS.Lib.DataField>();

            if (dtLanca.Rows.Count > 0)
            {
                PSPartLancaData psPartLancaData = new PSPartLancaData();
                psPartLancaData._tablename = "FLANCA";
                psPartLancaData._keys = new string[] { "CODEMPRESA", "CODLANCA" };

                for (int i = 0; i < dtLanca.Rows.Count; i++)
                {
                    PS.Lib.DataField dtCODLANCA = new PS.Lib.DataField("CODLANCA", dtLanca.Rows[i]["CODLANCA"]);

                    objArr1.Add(dtCODEMPRESA);
                    objArr1.Add(dtCODLANCA);

                    psPartLancaData.DeleteRecordOper(objArr1);

                    objArr1.Clear();
                }
            }
        }

        private void ExcluiTributoItem(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            sSql = @"SELECT NSEQITEM, CODTRIBUTO FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ?";

            DataTable dtTributo = dbs.QuerySelect(sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor);

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();

            if (dtTributo.Rows.Count > 0)
            {
                PSPartOperItemTributoData psPartOperItemTributoData = new PSPartOperItemTributoData();
                psPartOperItemTributoData._tablename = "GOPERITEMTRIBUTO";
                psPartOperItemTributoData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM", "CODTRIBUTO" };

                for (int i = 0; i < dtTributo.Rows.Count; i++)
                {
                    PS.Lib.DataField dtCODTRIBUTO = new PS.Lib.DataField("CODTRIBUTO", dtTributo.Rows[i]["CODTRIBUTO"]);
                    PS.Lib.DataField dtNSEQITEM = new PS.Lib.DataField("NSEQITEM", dtTributo.Rows[i]["NSEQITEM"]);

                    ListObjArr.Add(dtCODEMPRESA);
                    ListObjArr.Add(dtCODOPER);
                    ListObjArr.Add(dtNSEQITEM);
                    ListObjArr.Add(dtCODTRIBUTO);

                    psPartOperItemTributoData.DeleteRecord(ListObjArr);

                    ListObjArr.Clear();
                }
            }
        }

        public void ExcluiItem(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            sSql = @"SELECT NSEQITEM FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?";

            DataTable dtItem = dbs.QuerySelect(sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor);

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();

            if (dtItem.Rows.Count > 0)
            {
                PSPartOperacaoItemData psPartOperacaoItemData = new PSPartOperacaoItemData();
                psPartOperacaoItemData._tablename = "GOPERITEM";
                psPartOperacaoItemData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM" };

                for (int i = 0; i < dtItem.Rows.Count; i++)
                {
                    PS.Lib.DataField dtNSEQITEM = new PS.Lib.DataField("NSEQITEM", dtItem.Rows[i]["NSEQITEM"]);

                    ListObjArr.Add(dtCODEMPRESA);
                    ListObjArr.Add(dtCODOPER);
                    ListObjArr.Add(dtNSEQITEM);

                    psPartOperacaoItemData.DeleteRecordOper(ListObjArr);

                    ListObjArr.Clear();
                }
            }
        }

        public void ExcluiRateioCentroCusto(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            sSql = @"SELECT CODCCUSTO FROM GOPERRATEIOCC WHERE CODEMPRESA = ? AND CODOPER = ?";

            DataTable dtRateio = dbs.QuerySelect(sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor);

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();

            if (dtRateio.Rows.Count > 0)
            {
                PSPartOperRateioCCData psPartOperRateioCCData = new PSPartOperRateioCCData();
                psPartOperRateioCCData._tablename = "GOPERRATEIOCC";
                psPartOperRateioCCData._keys = new string[] { "CODEMPRESA", "CODOPER", "CODCCUSTO" };

                for (int i = 0; i < dtRateio.Rows.Count; i++)
                {
                    PS.Lib.DataField dtCODCCUSTO = new PS.Lib.DataField("CODCCUSTO", dtRateio.Rows[i]["CODCCUSTO"]);

                    ListObjArr.Add(dtCODEMPRESA);
                    ListObjArr.Add(dtCODOPER);
                    ListObjArr.Add(dtCODCCUSTO);

                    psPartOperRateioCCData.DeleteRecordOper(ListObjArr);

                    ListObjArr.Clear();
                }
            }
        }

        public void ExcluiRateioDepartamento(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            sSql = @"SELECT CODFILIAL, CODDEPTO FROM GOPERRATEIODP WHERE CODEMPRESA = ? AND CODOPER = ?";

            DataTable dtRateio = dbs.QuerySelect(sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor);

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();

            if (dtRateio.Rows.Count > 0)
            {
                PSPartOperRateioDPData psPartOperRateioDPData = new PSPartOperRateioDPData();
                psPartOperRateioDPData._tablename = "GOPERRATEIODP";
                psPartOperRateioDPData._keys = new string[] { "CODEMPRESA", "CODOPER", "CODFILIAL", "CODDEPTO" };

                for (int i = 0; i < dtRateio.Rows.Count; i++)
                {
                    PS.Lib.DataField dtCODDEPTO = new PS.Lib.DataField("CODDEPTO", dtRateio.Rows[i]["CODDEPTO"]);
                    PS.Lib.DataField dtCODFILIAL = new PS.Lib.DataField("CODFILIAL", dtRateio.Rows[i]["CODFILIAL"]);

                    ListObjArr.Add(dtCODEMPRESA);
                    ListObjArr.Add(dtCODOPER);
                    ListObjArr.Add(dtCODDEPTO);
                    ListObjArr.Add(dtCODFILIAL);

                    psPartOperRateioDPData.DeleteRecordOper(ListObjArr);

                    ListObjArr.Clear();
                }
            }
        }

        public void ExcluiRegistroNFe(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            PSPartNFEstadualData psPartNFEstadualData = new PSPartNFEstadualData();
            psPartNFEstadualData._tablename = "GNFESTADUAL";
            psPartNFEstadualData._keys = new string[] { "CODEMPRESA", "CODOPER" };

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
            ListObjArr.Add(dtCODEMPRESA);
            ListObjArr.Add(dtCODOPER);

            psPartNFEstadualData.DeleteRecord(ListObjArr);
        }

        public void CalculaOperacao(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            string sSql = @"SELECT * FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";
            List<PS.Lib.DataField> objArrOper = gb.RetornaDataFieldByDataRow(dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor).Rows[0]);

            PS.Lib.DataField dfCODTIPOPER = gb.RetornaDataFieldByCampo(objArrOper, "CODTIPOPER");

            if (dfCODTIPOPER.Valor == null)
            {
                throw new Exception("Tipo de Operação não identificada.");
            }
            else
            {
                DataRow PARAMNSTIPOPER;
                PARAMNSTIPOPER = gb.RetornaParametrosOperacao(dfCODTIPOPER.Valor.ToString());

                if (PARAMNSTIPOPER == null)
                {
                    throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação selecionada.");
                }
                else
                {
                    PS.Lib.Contexto.Session.key1 = dfCODEMPRESA.Valor;
                    PS.Lib.Contexto.Session.key2 = dfCODOPER.Valor;
                    PS.Lib.Contexto.Session.key3 = null;
                    PS.Lib.Contexto.Session.key4 = null;
                    PS.Lib.Contexto.Session.key5 = null;

                    int FRM_CODEMPRESA = 0;
                    string FRM_CODFORMULA = string.Empty;
                    string FRM_CODFORMULAVALORBRUTO = string.Empty;
                    string FRM_CODFORMULAVALORLIQUIDO = string.Empty;
                    decimal OP_VALORBRUTO = 0;
                    decimal OP_VALORLIQUIDO = 0;

                    FRM_CODEMPRESA = int.Parse(PARAMNSTIPOPER["CODEMPRESA"].ToString());
                    FRM_CODFORMULAVALORBRUTO = PARAMNSTIPOPER["CODFORMULAVALORBRUTO"].ToString();
                    FRM_CODFORMULAVALORLIQUIDO = PARAMNSTIPOPER["CODFORMULAVALORLIQUIDO"].ToString();

                    if (FRM_CODFORMULAVALORBRUTO != string.Empty)
                    {
                        try
                        {
                            interpreta.comando = function.PreparaFormula(FRM_CODEMPRESA, FRM_CODFORMULAVALORBRUTO);
                            OP_VALORBRUTO = Convert.ToDecimal(interpreta.Executar().ToString());
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Erro ao executar a fórmula: " + FRM_CODFORMULAVALORBRUTO + ".\r\n" + ex.Message);
                        }
                    }

                    if (FRM_CODFORMULAVALORLIQUIDO != string.Empty)
                    {
                        try
                        {
                            interpreta.comando = function.PreparaFormula(FRM_CODEMPRESA, FRM_CODFORMULAVALORLIQUIDO);
                            OP_VALORLIQUIDO = Convert.ToDecimal(interpreta.Executar().ToString());
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Erro ao executar a fórmula: " + FRM_CODFORMULAVALORLIQUIDO + ".\r\n" + ex.Message);
                        }
                    }

                    sSql = @"UPDATE GOPER SET VALORBRUTO = ?, VALORLIQUIDO = ? WHERE CODEMPRESA = ? AND CODOPER = ?";
                    dbs.QueryExec(sSql, OP_VALORBRUTO, OP_VALORLIQUIDO, dfCODEMPRESA.Valor, dfCODOPER.Valor);

                    this.GeraFinanceiro(objArrOper);

                    PS.Lib.Contexto.Session.key1 = null;
                    PS.Lib.Contexto.Session.key2 = null;
                    PS.Lib.Contexto.Session.key3 = null;
                    PS.Lib.Contexto.Session.key4 = null;
                    PS.Lib.Contexto.Session.key5 = null;
                }
            }
        }

        public void GeraFinanceiro(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfCODTIPOPER = gb.RetornaDataFieldByCampo(objArr, "CODTIPOPER");
            PS.Lib.DataField dfNUMERO = gb.RetornaDataFieldByCampo(objArr, "NUMERO");

            DataRow PARAMTIPOPERDESTINO = gb.RetornaParametrosOperacao(dfCODTIPOPER.Valor.ToString());
            if (PARAMTIPOPERDESTINO == null)
            {
                throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação [" + dfCODTIPOPER.Valor + "].");
            }
            else
            {
                if (Convert.ToInt32(PARAMTIPOPERDESTINO["GERAFINANCEIRO"]) == 1)
                {
                    sSql = @"SELECT * FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";
                    DataTable GOPERORIGEM = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor);

                    if (GOPERORIGEM.Rows[0]["CODCONDICAO"] != null)
                    {
                        sSql = "SELECT * FROM VCONDICAOPGTOCOMPOSICAO WHERE CODEMPRESA = ? AND CODCONDICAO = ?";
                        DataTable COMPOSICAOPAGTO = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, GOPERORIGEM.Rows[0]["CODCONDICAO"]);
                        if (COMPOSICAOPAGTO.Rows.Count > 0)
                        {
                            int[] listCODLANCAOLD;
                            string[] listNUMEROOLD;
                            int QtdLancaOld = 0;
                            int cont = 0;
                            sSql = @"SELECT CODLANCA, NUMERO FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ? AND CODSTATUS <> 2";
                            DataTable FLANCA = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor);
                            if (FLANCA.Rows.Count > 0)
                            {
                                listCODLANCAOLD = new int[FLANCA.Rows.Count];
                                listNUMEROOLD = new string[FLANCA.Rows.Count];
                                QtdLancaOld = FLANCA.Rows.Count;
                                foreach (DataRow rLanca in FLANCA.Rows)
                                {
                                    listCODLANCAOLD[cont] = Convert.ToInt32(rLanca["CODLANCA"]);
                                    listNUMEROOLD[cont] = rLanca["NUMERO"].ToString();
                                    cont++;
                                }
                            }
                            else
                            {
                                listCODLANCAOLD = new int[0];
                                listNUMEROOLD = new string[0];
                                QtdLancaOld = 0;
                            }

                            PSPartLancaData psPartLancaData = new PSPartLancaData();
                            psPartLancaData._tablename = "FLANCA";
                            psPartLancaData._keys = new string[] { "CODEMPRESA", "CODLANCA" };

                            #region Atributos

                            PS.Lib.DataField FIN_CODEMPRESA = new PS.Lib.DataField("CODEMPRESA", null);
                            PS.Lib.DataField FIN_CODLANCA = new PS.Lib.DataField("CODLANCA", null, typeof(int), PS.Lib.Global.TypeAutoinc.AutoInc);
                            PS.Lib.DataField FIN_TIPOPAGREC = new PS.Lib.DataField("TIPOPAGREC", null);
                            PS.Lib.DataField FIN_NUMERO = new PS.Lib.DataField("NUMERO", null);
                            PS.Lib.DataField FIN_CODCLIFOR = new PS.Lib.DataField("CODCLIFOR", null);
                            PS.Lib.DataField FIN_CODFILIAL = new PS.Lib.DataField("CODFILIAL", null);
                            PS.Lib.DataField FIN_DATAEMISSAO = new PS.Lib.DataField("DATAEMISSAO", null);
                            PS.Lib.DataField FIN_DATAVENCIMENTO = new PS.Lib.DataField("DATAVENCIMENTO", null);
                            PS.Lib.DataField FIN_DATAPREVBAIXA = new PS.Lib.DataField("DATAPREVBAIXA", null);
                            PS.Lib.DataField FIN_DATABAIXA = new PS.Lib.DataField("DATABAIXA", null);
                            PS.Lib.DataField FIN_OBSERVACAO = new PS.Lib.DataField("OBSERVACAO", null);
                            PS.Lib.DataField FIN_CODMOEDA = new PS.Lib.DataField("CODMOEDA", null);
                            PS.Lib.DataField FIN_VLORIGINAL = new PS.Lib.DataField("VLORIGINAL", 0);
                            PS.Lib.DataField FIN_PRDESCONTO = new PS.Lib.DataField("PRDESCONTO", 0);
                            PS.Lib.DataField FIN_VLDESCONTO = new PS.Lib.DataField("VLDESCONTO", 0);
                            PS.Lib.DataField FIN_PRMULTA = new PS.Lib.DataField("PRMULTA", 0);
                            PS.Lib.DataField FIN_VLMULTA = new PS.Lib.DataField("VLMULTA", 0);
                            PS.Lib.DataField FIN_PRJUROS = new PS.Lib.DataField("PRJUROS", 0);
                            PS.Lib.DataField FIN_VLJUROS = new PS.Lib.DataField("VLJUROS", 0);
                            PS.Lib.DataField FIN_VLLIQUIDO = new PS.Lib.DataField("VLLIQUIDO", 0);
                            PS.Lib.DataField FIN_CODSTATUS = new PS.Lib.DataField("CODSTATUS", null);
                            PS.Lib.DataField FIN_CODOPER = new PS.Lib.DataField("CODOPER", null);
                            PS.Lib.DataField FIN_CODTIPDOC = new PS.Lib.DataField("CODTIPDOC", null);
                            PS.Lib.DataField FIN_CODCONTA = new PS.Lib.DataField("CODCONTA", null);
                            PS.Lib.DataField FIN_CODFORMA = new PS.Lib.DataField("CODFORMA", null);
                            PS.Lib.DataField FIN_CODCCUSTO = new PS.Lib.DataField("CODCCUSTO", null);
                            PS.Lib.DataField FIN_CODNATUREZAORCAMENTO = new PS.Lib.DataField("CODNATUREZAORCAMENTO", null);
                            PS.Lib.DataField FIN_ORIGEM = new PS.Lib.DataField("ORIGEM", null);
                            PS.Lib.DataField FIN_NFOUDUP = new PS.Lib.DataField("NFOUDUP", "");
                            PS.Lib.DataField FIN_CODPREPRE = new PS.Lib.DataField("CODREPRE", 0);


                            #endregion

                            #region Sets

                            FIN_CODEMPRESA.Valor = GOPERORIGEM.Rows[0]["CODEMPRESA"];
                            FIN_NUMERO.Valor = GOPERORIGEM.Rows[0]["NUMERO"];
                            FIN_CODOPER.Valor = GOPERORIGEM.Rows[0]["CODOPER"];
                            FIN_CODCLIFOR.Valor = GOPERORIGEM.Rows[0]["CODCLIFOR"];
                            FIN_CODFILIAL.Valor = GOPERORIGEM.Rows[0]["CODFILIAL"];
                            FIN_DATAEMISSAO.Valor = GOPERORIGEM.Rows[0]["DATAEMISSAO"];
                            FIN_DATAVENCIMENTO.Valor = FIN_DATAEMISSAO.Valor;
                            FIN_DATAPREVBAIXA.Valor = FIN_DATAVENCIMENTO.Valor;
                            FIN_CODMOEDA.Valor = PARAMTIPOPERDESTINO["CODMOEDA"];
                            FIN_CODTIPDOC.Valor = PARAMTIPOPERDESTINO["CODTIPDOC"];
                            FIN_TIPOPAGREC.Valor = PARAMTIPOPERDESTINO["TIPOPAGREC"];
                            FIN_CODCCUSTO.Valor = GOPERORIGEM.Rows[0]["CODCCUSTO"];
                            FIN_CODNATUREZAORCAMENTO.Valor = GOPERORIGEM.Rows[0]["CODNATUREZAORCAMENTO"];
                            FIN_ORIGEM.Valor = "O";
                            FIN_CODSTATUS.Valor = 0;
                            FIN_CODPREPRE.Valor = GOPERORIGEM.Rows[0]["CODREPRE"];



                            #region Conta Caixa

                            if (GOPERORIGEM.Rows[0]["CODCONTA"] == DBNull.Value)
                                FIN_CODCONTA.Valor = PARAMTIPOPERDESTINO["CODCONTA"];
                            else
                                FIN_CODCONTA.Valor = GOPERORIGEM.Rows[0]["CODCONTA"];

                            #endregion

                            #region Forma de Pagamento

                            if (GOPERORIGEM.Rows[0]["CODFORMA"] == DBNull.Value)
                                FIN_CODFORMA.Valor = PARAMTIPOPERDESTINO["CODFORMAPADRAO"];
                            else
                                FIN_CODFORMA.Valor = GOPERORIGEM.Rows[0]["CODFORMA"];

                            #endregion

                            #region Condição de Pagamento

                            sSql = @"SELECT * FROM VCONDICAOPGTO WHERE CODEMPRESA = ? AND CODCONDICAO = ?";

                            DataTable VCONDICAOPGTO = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, GOPERORIGEM.Rows[0]["CODCONDICAO"]);

                            if (VCONDICAOPGTO.Rows.Count <= 0)
                            {
                                throw new Exception("Não foi possível carregar os parâmetros da Condição de Pagamento, verifique se a mesma foi informada na operação");
                            }

                            decimal FIN_TAXAJUROS = Convert.ToDecimal(VCONDICAOPGTO.Rows[0]["TAXAJUROS"].ToString());

                            #endregion

                            #endregion

                            #region Valida Composição de Pagamento

                            int FIN_NUMPARCELAS = 0;
                            decimal FIN_PERCVALOR = 0;
                            foreach (DataRow rComp in COMPOSICAOPAGTO.Rows)
                            {
                                FIN_PERCVALOR = FIN_PERCVALOR + Convert.ToDecimal(rComp["PERCVALOR"]);
                                FIN_NUMPARCELAS = FIN_NUMPARCELAS + Convert.ToInt32(rComp["NUMPARCELAS"]);
                            }

                            if (FIN_PERCVALOR != 100)
                            {
                                throw new Exception("A soma do percentual do valor da composição da condição de pagamento deve ser 100%.");
                            }

                            if (FIN_NUMPARCELAS < 0)
                            {
                                throw new Exception("O número de parcelas da composição da condição de pagamento deve ser maior que zero.");
                            }

                            #endregion

                            #region Parcelas

                            decimal OP_VALORLIQUIDO = Convert.ToDecimal(GOPERORIGEM.Rows[0]["VALORLIQUIDO"]);

                            decimal vl_funRural = Convert.ToDecimal(dbs.QueryValue(0, @"SELECT VALOR FROM GOPERITEMTRIBUTO WHERE CODOPER = ?  AND CODEMPRESA = ? AND CODTRIBUTO = 'FUNRURAL'", new object[] { FIN_CODOPER.Valor, FIN_CODEMPRESA.Valor }));
                            if (vl_funRural > 0)
                            {
                                OP_VALORLIQUIDO = OP_VALORLIQUIDO - vl_funRural;
                            }


                            if (OP_VALORLIQUIDO > 0)
                            {
                                decimal FIN_VALORORIGINAL = 0;

                                decimal FIN_PERCJUROS = FIN_TAXAJUROS;
                                decimal FIN_VALORJUROS = 0;

                                decimal FIN_PERCDESCONTO = 0;
                                decimal FIN_VALORDESCONTO = 0;

                                decimal FIN_PERCMULTA = 0;
                                decimal FIN_VALORMULTA = 0;

                                decimal FIN_VALORCOMPOSICAOPARCELA = 0;
                                decimal FIN_VALORLIQUIDOPARCELA = 0;

                                string FIN_NUMERODOCUMENTO = FIN_NUMERO.Valor.ToString();
                                int FIN_NSEQPARCELA = 0;
                                int FIN_NUMPARCOMP = 0;
                                int FIN_NUMPRAZO = 0;
                                int FIN_NUMINTERVALO = 0;
                                string FIN_TIPO = "N";

                                FIN_NUMPARCELAS = 0;

                                foreach (DataRow rComposicao in COMPOSICAOPAGTO.Rows)
                                {
                                    FIN_PERCVALOR = Convert.ToDecimal(rComposicao["PERCVALOR"]);
                                    FIN_NUMPARCOMP = Convert.ToInt32(rComposicao["NUMPARCELAS"]);
                                    FIN_NUMPRAZO = Convert.ToInt32(rComposicao["NUMPRAZO"]);
                                    FIN_NUMINTERVALO = Convert.ToInt32(rComposicao["NUMINTERVALO"]);
                                    FIN_TIPO = rComposicao["TIPO"].ToString();

                                    if (FIN_PERCVALOR == 100)
                                        FIN_VALORCOMPOSICAOPARCELA = OP_VALORLIQUIDO;
                                    else
                                        FIN_VALORCOMPOSICAOPARCELA = ((OP_VALORLIQUIDO * FIN_PERCVALOR) / 100);

                                    for (int i = 0; i < FIN_NUMPARCOMP; i++)
                                    {
                                        FIN_NSEQPARCELA = FIN_NSEQPARCELA + 1;

                                        if (QtdLancaOld > 0)
                                        {
                                            FIN_CODLANCA.Valor = listCODLANCAOLD[FIN_NUMPARCELAS];
                                            FIN_NUMERO.Valor = listNUMEROOLD[FIN_NUMPARCELAS];
                                            QtdLancaOld--;
                                        }
                                        else
                                        {
                                            FIN_CODLANCA.Valor = 0;
                                            FIN_NUMERO.Valor = string.Concat(FIN_NUMERODOCUMENTO, "/", FIN_NSEQPARCELA.ToString().PadLeft(2, '0'));
                                        }

                                        #region Valor


                                        FIN_VALORORIGINAL = gb.Arredonda((FIN_VALORCOMPOSICAOPARCELA / FIN_NUMPARCOMP), 2);


                                        if ((FIN_VALORORIGINAL * FIN_NUMPARCOMP) != FIN_VALORCOMPOSICAOPARCELA)
                                        {
                                            if (i == 0)
                                            {
                                                if ((FIN_VALORORIGINAL * FIN_NUMPARCOMP) > FIN_VALORCOMPOSICAOPARCELA)
                                                {
                                                    FIN_VALORORIGINAL = FIN_VALORORIGINAL - ((FIN_VALORORIGINAL * FIN_NUMPARCOMP) - FIN_VALORCOMPOSICAOPARCELA);
                                                }
                                                else
                                                {
                                                    FIN_VALORORIGINAL = FIN_VALORORIGINAL + (FIN_VALORCOMPOSICAOPARCELA - (FIN_VALORORIGINAL * FIN_NUMPARCOMP));
                                                }
                                            }
                                        }

                                        #endregion

                                        #region Prazo

                                        if (i == 0)
                                        {
                                            FIN_DATAVENCIMENTO.Valor = Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).AddDays(FIN_NUMPRAZO);
                                            FIN_DATAPREVBAIXA.Valor = FIN_DATAVENCIMENTO.Valor;
                                        }
                                        else
                                        {
                                            FIN_DATAVENCIMENTO.Valor = Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).AddDays(FIN_NUMINTERVALO);
                                            FIN_DATAPREVBAIXA.Valor = FIN_DATAVENCIMENTO.Valor;
                                        }

                                        //Nomal
                                        if (FIN_TIPO == "N")
                                        {

                                        }

                                        //Fora Semana
                                        if (FIN_TIPO == "S")
                                        {
                                            FIN_DATAVENCIMENTO.Valor = Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).AddDays((7 - (int)Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).DayOfWeek) + 1).AddDays(FIN_NUMPRAZO);
                                        }

                                        //Fora Dezena
                                        if (FIN_TIPO == "D")
                                        {
                                            if (Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Day <= 10)
                                            {
                                                FIN_DATAVENCIMENTO.Valor = new DateTime(Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Year, Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Month, 11).AddDays(FIN_NUMPRAZO);
                                            }
                                            else
                                            {
                                                if (Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Day > 10 && Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Day <= 20)
                                                {
                                                    FIN_DATAVENCIMENTO.Valor = new DateTime(Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Year, Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Month, 21).AddDays(FIN_NUMPRAZO);
                                                }
                                                else
                                                {
                                                    FIN_DATAVENCIMENTO.Valor = new DateTime(Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Year, Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Month, 1).AddMonths(1).AddDays(FIN_NUMPRAZO);
                                                }
                                            }
                                        }

                                        //Fora Quinzena
                                        if (FIN_TIPO == "Q")
                                        {
                                            if (Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Day <= 15)
                                            {
                                                FIN_DATAVENCIMENTO.Valor = new DateTime(Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Year, Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Month, 16).AddDays(FIN_NUMPRAZO);
                                            }
                                            else
                                            {
                                                FIN_DATAVENCIMENTO.Valor = new DateTime(Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Year, Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Month, 1).AddMonths(1).AddDays(FIN_NUMPRAZO);
                                            }
                                        }

                                        //Fora Mês
                                        if (FIN_TIPO == "M")
                                        {
                                            FIN_DATAVENCIMENTO.Valor = new DateTime(Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Year, Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Month, 1).AddMonths(1).AddDays(FIN_NUMPRAZO);
                                        }

                                        //Fora Ano
                                        if (FIN_TIPO == "A")
                                        {
                                            FIN_DATAVENCIMENTO.Valor = new DateTime(Convert.ToDateTime(FIN_DATAVENCIMENTO.Valor).Year, 1, 1).AddYears(1).AddDays(FIN_NUMPRAZO);
                                        }

                                        #endregion

                                        #region Juros

                                        if (FIN_TAXAJUROS > 0)
                                        {
                                            FIN_VALORJUROS = ((FIN_VALORORIGINAL * FIN_TAXAJUROS) / 100);
                                        }

                                        #endregion

                                        #region Valor Liquido

                                        FIN_VALORLIQUIDOPARCELA = (FIN_VALORORIGINAL + FIN_VALORMULTA + FIN_VALORJUROS) - FIN_VALORDESCONTO;

                                        #endregion

                                        FIN_VLORIGINAL.Valor = FIN_VALORORIGINAL;

                                        FIN_PRJUROS.Valor = FIN_PERCJUROS;
                                        FIN_VLJUROS.Valor = FIN_VALORJUROS;

                                        FIN_PRDESCONTO.Valor = FIN_PERCDESCONTO;
                                        FIN_VLDESCONTO.Valor = FIN_VALORDESCONTO;

                                        FIN_PRMULTA.Valor = FIN_PERCMULTA;
                                        FIN_VLMULTA.Valor = FIN_VALORMULTA;

                                        FIN_VLLIQUIDO.Valor = FIN_VALORLIQUIDOPARCELA;

                                        List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
                                        ListObjArr.Add(FIN_CODEMPRESA);
                                        ListObjArr.Add(FIN_CODLANCA);
                                        ListObjArr.Add(FIN_TIPOPAGREC);
                                        ListObjArr.Add(FIN_NUMERO);
                                        ListObjArr.Add(FIN_CODCLIFOR);
                                        ListObjArr.Add(FIN_CODFILIAL);
                                        ListObjArr.Add(FIN_DATAEMISSAO);
                                        ListObjArr.Add(FIN_DATAVENCIMENTO);
                                        ListObjArr.Add(FIN_DATAPREVBAIXA);
                                        ListObjArr.Add(FIN_DATABAIXA);
                                        ListObjArr.Add(FIN_OBSERVACAO);
                                        ListObjArr.Add(FIN_CODMOEDA);
                                        ListObjArr.Add(FIN_VLORIGINAL);
                                        ListObjArr.Add(FIN_PRDESCONTO);
                                        ListObjArr.Add(FIN_VLDESCONTO);
                                        ListObjArr.Add(FIN_PRMULTA);
                                        ListObjArr.Add(FIN_VLMULTA);
                                        ListObjArr.Add(FIN_PRJUROS);
                                        ListObjArr.Add(FIN_VLJUROS);
                                        //Verificar com o DELBONI
                                        //ListObjArr.Add(FIN_VLLIQUIDO);
                                        ListObjArr.Add(FIN_CODSTATUS);
                                        ListObjArr.Add(FIN_CODOPER);
                                        ListObjArr.Add(FIN_CODTIPDOC);
                                        ListObjArr.Add(FIN_CODCONTA);
                                        ListObjArr.Add(FIN_CODFORMA);
                                        ListObjArr.Add(FIN_ORIGEM);
                                        ListObjArr.Add(FIN_CODCCUSTO);
                                        ListObjArr.Add(FIN_CODNATUREZAORCAMENTO);
                                        ListObjArr.Add(FIN_NFOUDUP);
                                        ListObjArr.Add(FIN_CODPREPRE);
                                        List<PS.Lib.DataField> RetListObjArr = psPartLancaData.SaveRecord(ListObjArr);

                                        FIN_NUMPARCELAS++;
                                    }
                                }
                            }

                            #endregion

                            if (listCODLANCAOLD.Length > FIN_NUMPARCELAS)
                            {
                                int index = FIN_NUMPARCELAS;
                                for (int i = index; i < listCODLANCAOLD.Length; i++)
                                {
                                    List<PS.Lib.DataField> objArrExc = new List<Lib.DataField>();
                                    objArrExc.Add(new PS.Lib.DataField("CODEMPRESA", FIN_CODEMPRESA.Valor));
                                    objArrExc.Add(new PS.Lib.DataField("CODLANCA", listCODLANCAOLD[i]));
                                    psPartLancaData.DeleteRecordOper(objArrExc);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Não foi possível carregar os parâmetros da composição da condição de pagamento.");
                        }
                    }
                    else
                    {
                        throw new Exception("Não foi possível carregar os parâmetros da condição de pagamento, verifique se a mesma foi informada na operação.");
                    }
                }
            }
        }

        public void GeraNFe(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dfCODEMPRESA = new Lib.DataField("GOPER.CODEMPRESA", AppLib.Context.Empresa);
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "GOPER.CODOPER");
            PS.Lib.DataField dfCODTIPOPER = gb.RetornaDataFieldByCampo(objArr, "GOPER.CODTIPOPER");
            PS.Lib.DataField dfNUMERO = gb.RetornaDataFieldByCampo(objArr, "GOPER.NUMERO");
            objArr.Add(dfCODEMPRESA);
            DataRow PARAMTIPOPERDESTINO = gb.RetornaParametrosOperacao(dfCODTIPOPER.Valor.ToString());
            if (PARAMTIPOPERDESTINO == null)
            {
                throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação [" + dfCODTIPOPER.Valor + "].");
            }

            if (Convert.ToInt32(PARAMTIPOPERDESTINO["USAOPERACAONFE"]) == 1)
            {
                if (this.VerificaSituacao(objArr) == 2)
                {
                    throw new Exception("Não é possivel gerar a NF-e da operação [" + dfNUMERO.Valor + "] pois a operação está cancelada.");
                }

                sSql = @"SELECT CODOPER FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?";
                if (dbs.QueryFind(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor))
                {
                    throw new Exception("Não é possivel gerar a NF-e da operação [" + dfNUMERO.Valor + "] pois a mesma já possui NF-e gerada.");
                }

                #region Atributos

                PS.Lib.DataField NFE_CODEMPRESA = new PS.Lib.DataField("CODEMPRESA", null);
                PS.Lib.DataField NFE_CODOPER = new PS.Lib.DataField("CODOPER", null);
                PS.Lib.DataField NFE_CODSTATUS = new PS.Lib.DataField("CODSTATUS", null);
                PS.Lib.DataField NFE_CHAVEACESSO = new PS.Lib.DataField("CHAVEACESSO", null);
                PS.Lib.DataField NFE_XMLNFE = new PS.Lib.DataField("XMLNFE", null);
                PS.Lib.DataField NFE_RECIBO = new PS.Lib.DataField("RECIBO", null);
                PS.Lib.DataField NFE_DATARECIBO = new PS.Lib.DataField("DATARECIBO", null);
                PS.Lib.DataField NFE_PROTOCOLO = new PS.Lib.DataField("PROTOCOLO", null);
                PS.Lib.DataField NFE_DATAPROTOCOLO = new PS.Lib.DataField("DATAPROTOCOLO", null);

                #endregion

                #region Sets

                NFE_CODEMPRESA.Valor = dfCODEMPRESA.Valor;
                NFE_CODOPER.Valor = dfCODOPER.Valor;
                NFE_CODSTATUS.Valor = "P";

                List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
                ListObjArr.Add(NFE_CODEMPRESA);
                ListObjArr.Add(NFE_CODOPER);
                ListObjArr.Add(NFE_CODSTATUS);
                ListObjArr.Add(NFE_CHAVEACESSO);
                ListObjArr.Add(NFE_XMLNFE);
                ListObjArr.Add(NFE_RECIBO);
                ListObjArr.Add(NFE_DATARECIBO);
                ListObjArr.Add(NFE_PROTOCOLO);
                ListObjArr.Add(NFE_DATAPROTOCOLO);

                #endregion

                PSPartNFEstadualData psPartNFEstadualData = new PSPartNFEstadualData();
                psPartNFEstadualData._tablename = "GNFESTADUAL";
                psPartNFEstadualData._keys = new string[] { "CODEMPRESA", "CODOPER" };
                psPartNFEstadualData.SaveRecord(ListObjArr);

                //Verificar se existe como colocar a rotina de envio da nf-e aqui.
                PSPartEnviarNFeAppFrm envio = new PSPartEnviarNFeAppFrm();
                envio.EnvNFe(ListObjArr);
            }
            else
            {
                throw new Exception("Tipo da Operação [" + dfCODTIPOPER.Valor + "] não está parametrizada para gerar NF-e");
            }
        }

        //Fábio Campos
        public string GeraNFeRet(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfCODTIPOPER = gb.RetornaDataFieldByCampo(objArr, "CODTIPOPER");
            PS.Lib.DataField dfNUMERO = gb.RetornaDataFieldByCampo(objArr, "NUMERO");

            DataRow PARAMTIPOPERDESTINO = gb.RetornaParametrosOperacao(dfCODTIPOPER.Valor.ToString());
            if (PARAMTIPOPERDESTINO == null)
            {
                throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação [" + dfCODTIPOPER.Valor + "].");
            }

            if (Convert.ToInt32(PARAMTIPOPERDESTINO["USAOPERACAONFE"]) == 1)
            {
                if (this.VerificaSituacao(objArr) == 2)
                {
                    throw new Exception("Não é possivel gerar a NF-e da operação [" + dfNUMERO.Valor + "] pois a operação está cancelada.");
                }

                sSql = @"SELECT CODOPER FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?";
                if (dbs.QueryFind(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor))
                {
                    throw new Exception("Não é possivel gerar a NF-e da operação [" + dfNUMERO.Valor + "] pois a mesma já possui NF-e gerada.");
                }

                #region Atributos

                PS.Lib.DataField NFE_CODEMPRESA = new PS.Lib.DataField("CODEMPRESA", null);
                PS.Lib.DataField NFE_CODOPER = new PS.Lib.DataField("CODOPER", null);
                PS.Lib.DataField NFE_CODSTATUS = new PS.Lib.DataField("CODSTATUS", null);
                PS.Lib.DataField NFE_CHAVEACESSO = new PS.Lib.DataField("CHAVEACESSO", null);
                PS.Lib.DataField NFE_XMLNFE = new PS.Lib.DataField("XMLNFE", null);
                PS.Lib.DataField NFE_RECIBO = new PS.Lib.DataField("RECIBO", null);
                PS.Lib.DataField NFE_DATARECIBO = new PS.Lib.DataField("DATARECIBO", null);
                PS.Lib.DataField NFE_PROTOCOLO = new PS.Lib.DataField("PROTOCOLO", null);
                PS.Lib.DataField NFE_DATAPROTOCOLO = new PS.Lib.DataField("DATAPROTOCOLO", null);

                #endregion

                #region Sets

                NFE_CODEMPRESA.Valor = dfCODEMPRESA.Valor;
                NFE_CODOPER.Valor = dfCODOPER.Valor;
                NFE_CODSTATUS.Valor = "P";

                List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
                ListObjArr.Add(NFE_CODEMPRESA);
                ListObjArr.Add(NFE_CODOPER);
                ListObjArr.Add(NFE_CODSTATUS);
                ListObjArr.Add(NFE_CHAVEACESSO);
                ListObjArr.Add(NFE_XMLNFE);
                ListObjArr.Add(NFE_RECIBO);
                ListObjArr.Add(NFE_DATARECIBO);
                ListObjArr.Add(NFE_PROTOCOLO);
                ListObjArr.Add(NFE_DATAPROTOCOLO);

                #endregion

                PSPartNFEstadualData psPartNFEstadualData = new PSPartNFEstadualData();
                psPartNFEstadualData._tablename = "GNFESTADUAL";
                psPartNFEstadualData._keys = new string[] { "CODEMPRESA", "CODOPER" };
                psPartNFEstadualData.SaveRecord(ListObjArr);

                return NFE_CODOPER.Valor.ToString();

            }
            else
            {
                throw new Exception("Tipo da Operação [" + dfCODTIPOPER.Valor + "] não está parametrizada para gerar NF-e");
            }
        }

        public override void ValidateRecord(List<PS.Lib.DataField> objArr)
        {
            base.ValidateRecord(objArr);

            if (VerificaSituacao(objArr) != 0)
            {
                throw new Exception("Operação não pode ser modificada.");
            }

            string sSql = string.Empty;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dtNUMERO = gb.RetornaDataFieldByCampo(objArr, "NUMERO");
            PS.Lib.DataField dtSERIE = gb.RetornaDataFieldByCampo(objArr, "SERIE");
            PS.Lib.DataField dtCODCLIFOR = gb.RetornaDataFieldByCampo(objArr, "CODCLIFOR");
            PS.Lib.DataField dfCODTIPOPER = gb.RetornaDataFieldByCampo(objArr, "CODTIPOPER");
            PS.Lib.DataField dtCODFILIAL = gb.RetornaDataFieldByCampo(objArr, "CODFILIAL");
            PS.Lib.DataField dtCODFILIALENTREGA = gb.RetornaDataFieldByCampo(objArr, "CODFILIALENTREGA");
            PS.Lib.DataField dfCODCONDICAO = gb.RetornaDataFieldByCampo(objArr, "CODCONDICAO");
            PS.Lib.DataField dtDATAEMISSAO = gb.RetornaDataFieldByCampo(objArr, "DATAEMISSAO");
            PS.Lib.DataField dtDATAENTSAI = gb.RetornaDataFieldByCampo(objArr, "DATAENTSAI");
            PS.Lib.DataField dtDATAENTREGA = gb.RetornaDataFieldByCampo(objArr, "DATAENTREGA");

            if (Convert.ToInt32(dfCODOPER.Valor) > 0)
            {
                if (PossuiNFEstadual(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODOPER.Valor), false))
                {
                    throw new Exception("Operação não pode ser modificada pois esta vinculada a uma NF-e.");
                }
            }

            if (dfCODTIPOPER.Valor == null)
            {
                throw new Exception(gb.MensagemDeValidacao(this._tablename, dfCODTIPOPER.Field));
            }

            if (dtCODFILIAL.Valor == null)
            {
                throw new Exception(gb.MensagemDeValidacao(this._tablename, dtCODFILIAL.Field));
            }

            DataRow PARAMTIPOPER = gb.RetornaParametrosOperacao(dfCODTIPOPER.Valor.ToString());
            if (PARAMTIPOPER == null)
            {
                throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação [" + dfCODTIPOPER.Valor + "].");
            }
            else
            {
                if (PARAMTIPOPER["CODCLIFOR"].ToString() != "M")
                {
                    if (dtCODCLIFOR.Valor == null)
                    {
                        throw new Exception(gb.MensagemDeValidacao(this._tablename, dtCODCLIFOR.Field));
                    }
                }

                if (dtDATAEMISSAO.Valor != null)
                {
                    if (dtDATAENTSAI.Valor != null)
                    {
                        if (Convert.ToDateTime(dtDATAENTSAI.Valor) < Convert.ToDateTime(dtDATAEMISSAO.Valor))
                        {
                            throw new Exception("Data de Entrada/Saida não pode ser menor que Data de Emissão.");
                        }
                    }

                    if (dtDATAENTREGA.Valor != null)
                    {
                        if (Convert.ToDateTime(dtDATAENTREGA.Valor) < Convert.ToDateTime(dtDATAEMISSAO.Valor))
                        {
                            throw new Exception("Data de Entrega não pode ser menor que Data de Emissão.");
                        }
                    }
                }
                else
                {
                    throw new Exception(gb.MensagemDeValidacao(_tablename, dtDATAEMISSAO.Field.ToString()));
                }

                if (dtNUMERO.Valor == null)
                {
                    if (int.Parse(PARAMTIPOPER["USANUMEROSEQ"].ToString()) == 0)
                    {
                        throw new Exception(gb.MensagemDeValidacao(this._tablename, dtNUMERO.Field));
                    }
                }
                else
                {
                    int mask = (PARAMTIPOPER["MASKNUMEROSEQ"] == DBNull.Value) ? 0 : Convert.ToInt32(PARAMTIPOPER["MASKNUMEROSEQ"]);
                    if (mask == 0)
                    {
                        throw new Exception("Máscara do numero da operação não está parametrizada.");
                    }
                    else
                    {
                        if (dtNUMERO.Valor.ToString().Length > mask)
                        {
                            throw new Exception("Quantidade de caracteres do número da operação é maior que a quantidade permitida.");
                        }
                    }
                }

                if (Convert.ToInt32(PARAMTIPOPER["GERAFINANCEIRO"]) == 1)
                {
                    if (dfCODCONDICAO.Valor == null)
                    {
                        throw new Exception(gb.MensagemDeValidacao(_tablename, dfCODCONDICAO.Field.ToString()));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(dfCODCONDICAO.Valor.ToString()))
                        {
                            throw new Exception(gb.MensagemDeValidacao(_tablename, dfCODCONDICAO.Field.ToString()));
                        }
                    }
                }

                #region Formula de Validação

                if (PARAMTIPOPER["CODFORMULAVALIDAOPERACAO"].ToString() != string.Empty)
                {
                    PS.Lib.Contexto.Session.Current = objArr;

                    int FRM_CODEMPRESA = int.Parse(dfCODEMPRESA.Valor.ToString());
                    string FRM_CODFORMULA = PARAMTIPOPER["CODFORMULAVALIDAOPERACAO"].ToString();

                    interpreta.comando = function.PreparaFormula(FRM_CODEMPRESA, FRM_CODFORMULA);
                    bool ERRO = Convert.ToBoolean(interpreta.Executar());

                    PS.Lib.Contexto.Session.key1 = null;
                    PS.Lib.Contexto.Session.key2 = null;
                    PS.Lib.Contexto.Session.key3 = null;
                    PS.Lib.Contexto.Session.key4 = null;
                    PS.Lib.Contexto.Session.key5 = null;

                    PS.Lib.Contexto.Session.Current = null;

                    if (!ERRO)
                    {
                        sSql = @"SELECT DESCRICAO FROM GFORMULA WHERE CODEMPRESA = ? AND CODFORMULA = ?";

                        string FRM_DESCRICAO = dbs.QueryValue("", sSql, FRM_CODEMPRESA, FRM_CODFORMULA).ToString();

                        throw new Exception(string.Concat("Formula de Validação : ", FRM_CODFORMULA, "\n", FRM_DESCRICAO));
                    }
                }

                #endregion
            }
        }

        public override List<PS.Lib.DataField> InsertRecord(List<PS.Lib.DataField> objArr)
        {

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNUMERO = gb.RetornaDataFieldByCampo(objArr, "NUMERO");
            PS.Lib.DataField dfCODSERIE = gb.RetornaDataFieldByCampo(objArr, "CODSERIE");
            PS.Lib.DataField dfCODCLIFOR = gb.RetornaDataFieldByCampo(objArr, "CODCLIFOR");
            PS.Lib.DataField dfCODTIPOPER = gb.RetornaDataFieldByCampo(objArr, "CODTIPOPER");
            PS.Lib.DataField dfCODFILIAL = gb.RetornaDataFieldByCampo(objArr, "CODFILIAL");
            PS.Lib.DataField dfCODFILIALENTREGA = gb.RetornaDataFieldByCampo(objArr, "CODFILIALENTREGA");

            #region Gera Número da Operação

            if (dfNUMERO.Valor == null)
            {
                string novoNumStr = this.GeraNumeroDocumento(dfCODTIPOPER.Valor.ToString());
                if (string.IsNullOrEmpty(novoNumStr))
                {

                    throw new Exception("Erro ao gerar número do documento.");
                }
                else
                {
                    dfNUMERO.Valor = novoNumStr;

                    for (int i = 0; i < objArr.Count; i++)
                    {
                        if (objArr[i].Field == "NUMERO")
                        {
                            objArr[i].Valor = dfNUMERO.Valor;
                        }
                    }
                }
            }
            else
            {
                DataRow PARAMTIPOPER = gb.RetornaParametrosOperacao(dfCODTIPOPER.Valor.ToString());
                if (PARAMTIPOPER == null)
                {
                    throw new Exception("Não foi possível gerar o número do lançamento.");
                }
                else
                {
                    string novoNumStr = string.Empty;
                    int mask = (PARAMTIPOPER["MASKNUMEROSEQ"] == DBNull.Value) ? 0 : Convert.ToInt32(PARAMTIPOPER["MASKNUMEROSEQ"]);
                    if (mask == 0)
                    {
                        throw new Exception("Não foi possível gerar o número da operação pois a máscara do numero da operação não está parametrizada.");
                    }
                    else
                    {
                        int tamanho = dfNUMERO.Valor.ToString().Length;
                        novoNumStr = string.Concat(dfNUMERO.Valor.ToString().PadLeft(mask, '0'));

                        if (novoNumStr.Length > mask)
                        {
                            throw new Exception("Quantidade de caracteres do número da operação é maior que a quantidade permitida.");
                        }
                        else
                        {
                            for (int i = 0; i < objArr.Count; i++)
                            {
                                if (objArr[i].Field == "NUMERO")
                                {
                                    objArr[i].Valor = novoNumStr;
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            if (this.ValidaNumeroOperacao(objArr))
            {
                throw new Exception("Operação [" + dfNUMERO.Valor + "] de série [" + dfCODSERIE.Valor + "] já existe.");
            }

            for (int i = 0; i < objArr.Count; i++)
            {
                if (objArr[i].Field == "DATACRIACAO")
                {
                    objArr[i].Valor = dbs.GetServerDateTimeNow();
                }

                if (objArr[i].Field == "CODUSUARIOCRIACAO")
                {
                    objArr[i].Valor = PS.Lib.Contexto.Session.CodUsuario;
                }
            }
            try
            {
                List<PS.Lib.DataField> objArrDDL = objArr;

                List<PS.Lib.DataField> temp = base.InsertRecord(objArr);

                this.CalculaOperacao(objArr);

                this.InsertRegistroComplementar(objArrDDL);

                verificaDesconto(dfCODTIPOPER.Valor.ToString(), dfCODCLIFOR.Valor.ToString(), Convert.ToInt32(dfCODOPER.Valor));
                //
                //realiza o update na série
                //
                //System.Data.DataRow PARAMTIPOPER = gb.RetornaParametrosOperacao(FAT_PROXIMA_ETAPA);
                string sql = @"UPDATE VSERIE SET NUMSEQ = ? WHERE CODSERIE = ? AND CODEMPRESA = ?";
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(sql, new object[] { dfNUMERO.Valor, dfCODSERIE.Valor, AppLib.Context.Empresa });
                return temp;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void verificaDesconto(string codTipoOper, string codCliFor, int codOper)
        {

            string usaBloq = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT USABLOQUEIODESCONTO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipoOper }).ToString();
            if (!string.IsNullOrEmpty(usaBloq))
            {
                string tipoBloq = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT TIPOBLOQUEIODESC FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codOper }).ToString();
                if (tipoBloq == "T")
                {
                    bloqueioTotal(codTipoOper, codCliFor, codOper);
                }
                else
                {
                    bloqueioItem(codOper);
                }
            }
        }
        private void bloqueioItem(int codOper)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT LIMITEDESC, GOPERITEM.PRDESCONTO FROM GOPER INNER JOIN GOPERITEM ON GOPER.CODOPER = GOPERITEM.CODOPER AND GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToDecimal(dt.Rows[0]["LIMITEDESC"]) < Convert.ToDecimal(dt.Rows[0]["PRDESCONTO"]))
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GOPER SET CODSTATUS = 7 WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa });
                    return;
                }
            }
        }
        private void bloqueioTotal(string codTipoOper, string codCliFor, int codOper)
        {

            decimal compra = 0, venda = 0, valor = 0;
            string tipoEntrada = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT TIPENTSAI FROM GTIPOPER WHERE CODTIPOPER = ?", new object[] { codTipoOper }).ToString();
            if (!string.IsNullOrEmpty(tipoEntrada))
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT DESCMAXCOMPRA, DESCMAXVENDA FROM VCLIFOR WHERE CODCLIFOR = ?", new object[] { codCliFor });
                if (dt.Rows.Count > 0)
                {
                    valor = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"	SELECT 
	((SUM(GOPERITEM.VLDESCONTO) / (CASE WHEN (GOPER.VALORBRUTO = 0) THEN 1 ELSE GOPER.VALORBRUTO END) ) * 100) PERCDESCONTO
	FROM 
	GOPERITEM 
	INNER JOIN GOPER ON GOPERITEM.CODOPER = GOPER.CODOPER AND GOPERITEM.CODEMPRESA = GOPER.CODEMPRESA
	WHERE 
	GOPERITEM.CODOPER = ? 
	GROUP BY 
	VALORBRUTO", new object[] { codOper }));

                    if (tipoEntrada == "E")
                    {
                        compra = Convert.ToDecimal(dt.Rows[0]["DESCMAXCOMPRA"]);
                        if (valor > compra)
                        {
                            // Altera o status da operação para BLOQUEADO - 7
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GOPER SET CODSTATUS = 7 WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa });
                        }
                    }
                    else if (tipoEntrada == "S")
                    {
                        venda = Convert.ToDecimal(dt.Rows[0]["DESCMAXVENDA"]);
                        if (valor > venda)
                        {
                            // Altera o status da operação para BLOQUEADO - 7
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GOPER SET CODSTATUS = 7 WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa });
                        }
                    }
                }
            }
        }

        public override List<PS.Lib.DataField> EditRecord(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNUMERO = gb.RetornaDataFieldByCampo(objArr, "NUMERO");
            PS.Lib.DataField dfCODSERIE = gb.RetornaDataFieldByCampo(objArr, "CODSERIE");
            PS.Lib.DataField dfCODCLIFOR = gb.RetornaDataFieldByCampo(objArr, "CODCLIFOR");
            PS.Lib.DataField dfCODTIPOPER = gb.RetornaDataFieldByCampo(objArr, "CODTIPOPER");
            PS.Lib.DataField dfCODFILIAL = gb.RetornaDataFieldByCampo(objArr, "CODFILIAL");
            PS.Lib.DataField dfCODFILIALENTREGA = gb.RetornaDataFieldByCampo(objArr, "CODFILIALENTREGA");

            if (this.ValidaNumeroOperacao(objArr))
            {
                throw new Exception("Operação [" + dfNUMERO.Valor + "] de série [" + dfCODSERIE.Valor + "] já existe.");
            }

            List<PS.Lib.DataField> temp = base.EditRecord(objArr);

            this.CalculaOperacao(objArr);

            verificaDesconto(dfCODTIPOPER.Valor.ToString(), dfCODCLIFOR.Valor.ToString(), Convert.ToInt32(dfCODOPER.Valor));

            return temp;
        }

        public override void DeleteRecord(List<PS.Lib.DataField> objArr)
        {
            base.ValidateKeyRecord(objArr);

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            List<PS.Lib.DataField> objArr1 = gb.RetornaDataFieldByDataRow(dbs.QuerySelect("SELECT NUMERO FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", dtCODEMPRESA.Valor, dtCODOPER.Valor).Rows[0]);
            PS.Lib.DataField dtNUMERO = gb.RetornaDataFieldByCampo(objArr1, "NUMERO");

            if (this.VerificaSituacao(objArr) != 2)
            {
                throw new Exception("Operação [" + dtNUMERO.Valor + "] não pode ser excluida pois não está cancelada.");
            }

            if (PossuiRelacionamento(Convert.ToInt32(dtCODEMPRESA.Valor), Convert.ToInt32(dtCODOPER.Valor), true))
            {
                throw new Exception("Operação [" + dtNUMERO.Valor + "] não pode ser excluída pois possui relacionamento com outra operação.");
            }

            if (PossuiNFEstadual(Convert.ToInt32(dtCODEMPRESA.Valor), Convert.ToInt32(dtCODOPER.Valor), false))
            {
                throw new Exception("A operação [" + dtNUMERO.Valor + "] não pode ser cancelada pois esta vinculada a uma NF-e.");
            }

            if (this.PossuiFinanceiroBaixado(objArr))
            {
                throw new Exception("Operação [" + dtNUMERO.Valor + "] não pode ser excluída pois possui lançamento(s) baixado(s).");
            }

            this.ExcluiRegistroNFe(objArr);
            this.ExcluiRegistroComplementar(objArr);
            this.ExcluiFinanceiro(objArr);
            this.ExcluiRateioCentroCusto(objArr);
            this.ExcluiRateioDepartamento(objArr);
            this.ExcluiItem(objArr);
            this.VoltaStatusRelacionamento(objArr);
            this.ExcluiRelacionamento(objArr);
            ExcluiMotivo(objArr);
            base.DeleteRecord(objArr);
        }
        public void ExcluiMotivo(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM GMOTIVOCANCELAMENTO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { dfCODEMPRESA.Valor, dfCODOPER.Valor });

        }
    }
}
