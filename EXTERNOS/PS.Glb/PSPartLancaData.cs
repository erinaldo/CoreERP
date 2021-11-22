using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PS.Glb
{
    public class PSPartLancaData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public override string ReadView()
        {
            return @"SELECT TIPOPAGREC, CODEMPRESA,
CODFILIAL,
CODLANCA,
CODSTATUS,
NUMERO,
CODTIPDOC,
CODCLIFOR,
(SELECT NOMEFANTASIA FROM VCLIFOR WHERE CODEMPRESA = FLANCA.CODEMPRESA AND CODCLIFOR = FLANCA.CODCLIFOR) CNOME,
DATAEMISSAO,
DATAVENCIMENTO,
DATAPREVBAIXA,
DATABAIXA,
OBSERVACAO,
CODMOEDA,
VLORIGINAL,
PRDESCONTO,
VLDESCONTO,
PRMULTA,
VLMULTA,
PRJUROS,
VLJUROS,
VLLIQUIDO,
VLBAIXADO,
CODOPER,
CODCONTA,
SEGUNDONUMERO,
CODCCUSTO,
CODNATUREZAORCAMENTO,
CODDEPTO,
CODFORMA,
IDEXTRATO,
ORIGEM,
DATACRIACAO,
CODUSUARIOCRIACAO,
DATACANCELAMENTO,
CODUSUARIOCANCELAMENTO,
MOTIVOCANCELAMENTO,
DATACANCELAMENTOBAIXA,
CODUSUARIOCANCELAMENTOBAIXA,
MOTIVOCANCELAMENTOBAIXA,
NFOUDUP,
CODCHEQUE,
CODFATURA,
(SELECT NOMEFANTASIA FROM VREPRE WHERE CODEMPRESA = FLANCA.CODEMPRESA AND CODREPRE = FLANCA.CODREPRE) CODREPRE
FROM FLANCA WHERE ";
        }

        public void InsertRegistroComplementar(List<PS.Lib.DataField> objArr)
        {
            PSPartLancaComplData psPartLancaComplData = new PSPartLancaComplData();
            psPartLancaComplData._tablename = "FLANCACOMPL";
            psPartLancaComplData._keys = new string[] { "CODEMPRESA", "CODLANCA" };

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
            ListObjArr.Add(dfCODEMPRESA);
            ListObjArr.Add(dfCODLANCA);

            psPartLancaComplData.SaveRecord(ListObjArr);
        }

        public void ExcluiRegistroComplementar(List<PS.Lib.DataField> objArr)
        {
            PSPartLancaComplData psPartLancaComplData = new PSPartLancaComplData();
            psPartLancaComplData._tablename = "FLANCACOMPL";
            psPartLancaComplData._keys = new string[] { "CODEMPRESA", "CODLANCA" };

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
            ListObjArr.Add(dfCODEMPRESA);
            ListObjArr.Add(dfCODLANCA);

            psPartLancaComplData.DeleteRecord(ListObjArr);
        }

        public void ExcluiRegistroRateioCC(List<PS.Lib.DataField> objArr)
        {
            PSPartLancaRateioCCData psPartLancaRateioCCData = new PSPartLancaRateioCCData();
            psPartLancaRateioCCData._tablename = "FLANCARATEIOCC";
            psPartLancaRateioCCData._keys = new string[] { "CODEMPRESA", "CODLANCA" };

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
            ListObjArr.Add(dfCODEMPRESA);
            ListObjArr.Add(dfCODLANCA);

            psPartLancaRateioCCData.DeleteRecord(ListObjArr);
        }

        public void ExcluiRegistroRateioDP(List<PS.Lib.DataField> objArr)
        {
            PSPartLancaRateioDPData psPartLancaRateioDPData = new PSPartLancaRateioDPData();
            psPartLancaRateioDPData._tablename = "FLANCARATEIODP";
            psPartLancaRateioDPData._keys = new string[] { "CODEMPRESA", "CODLANCA" };

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
            ListObjArr.Add(dfCODEMPRESA);
            ListObjArr.Add(dfCODLANCA);

            psPartLancaRateioDPData.DeleteRecord(ListObjArr);
        }

        public bool ExcluiRelacionamento(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");

            sSql = @"DELETE FROM FLANCARELAC WHERE CODEMPRESA = ? AND CODLANCARELAC = ?";

            return dbs.QueryFind(sSql, dtCODEMPRESA.Valor, dtCODLANCA.Valor);
        }

        public bool PossuiRelacionamento(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");

            sSql = @"SELECT CODLANCARELAC FROM FLANCARELAC WHERE CODEMPRESA = ? AND CODLANCA = ?";

            return dbs.QueryFind(sSql, dtCODEMPRESA.Valor, dtCODLANCA.Valor);
        }

        public bool PossuiOperacaoRelacionada(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");

            sSql = @"SELECT CODOPER FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ? AND ORIGEM = 'O'";
            object retorno = dbs.QueryValue(null, sSql, dtCODEMPRESA.Valor, dtCODLANCA.Valor);

            if (retorno == null)
                return false;
            else
                if (retorno.ToString() == string.Empty)
                return false;
            else
                return true;
        }

        public bool PossuiAntecessorRelacionamento(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");

            sSql = @"SELECT CODLANCA FROM FLANCARELAC WHERE CODEMPRESA = ? AND CODLANCARELAC = ?";

            return dbs.QueryFind(sSql, dtCODEMPRESA.Valor, dtCODLANCA.Valor);
        }

        public void CancelaLancamentoNaBaixa(List<PS.Lib.DataField> objArr)
        {
            try
            {
                PS.Lib.DataField dfNUMERO = gb.RetornaDataFieldByCampo(objArr, "NUMERO");

                if (VerificaSituacao(objArr) == 2)
                {
                    throw new Exception("Não foi possível realizar o cancelamento do lançamento [ " + dfNUMERO.Valor.ToString() + " ] pois o mesmo já está cancelado.");
                }

                if (VerificaSituacao(objArr) == 1)
                {
                    throw new Exception("Não foi possível realizar o cancelamento do lançamento [ " + dfNUMERO.Valor.ToString() + " ] pois o mesmo está baixado.");
                }

                if (PossuiOperacaoRelacionada(objArr))
                {
                    throw new Exception("Não foi possível realizar o cancelamento do lançamento [ " + dfNUMERO.Valor.ToString() + " ] pois o mesmo está relacionado a uma operação.");
                }

                this.CancelaLancamentoOper(objArr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CancelaLancamento(List<PS.Lib.DataField> objArr)
        {
            try
            {
                PS.Lib.DataField dfNUMERO = gb.RetornaDataFieldByCampo(objArr, "NUMERO");

                if (VerificaSituacao(objArr) == 2)
                {
                    throw new Exception("Não foi possível realizar o cancelamento do lançamento [ " + dfNUMERO.Valor.ToString() + " ] pois o mesmo já está cancelado.");
                }

                if (VerificaSituacao(objArr) == 1)
                {
                    throw new Exception("Não foi possível realizar o cancelamento do lançamento [ " + dfNUMERO.Valor.ToString() + " ] pois o mesmo está baixado.");
                }

                if (PossuiOperacaoRelacionada(objArr))
                {
                    throw new Exception("Não foi possível realizar o cancelamento do lançamento [ " + dfNUMERO.Valor.ToString() + " ] pois o mesmo está relacionado a uma operação.");
                }

                if (PossuiAntecessorRelacionamento(objArr))
                {
                    throw new Exception("Não foi possível realizar o cancelamento do lançamento [ " + dfNUMERO.Valor.ToString() + " ] pois o mesmo está relacionado a um outro lançamento.");
                }

                this.CancelaLancamentoOper(objArr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CancelaLancamentoOper(List<PS.Lib.DataField> objArr)
        {
            try
            {
                PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
                PS.Lib.DataField dfCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");
                PS.Lib.DataField dfNUMERO = gb.RetornaDataFieldByCampo(objArr, "NUMERO");

                if (VerificaSituacao(objArr) != 0)
                {
                    throw new Exception("Não foi possível realizar o cancelamento do lançamento [ " + dfNUMERO.Valor.ToString() + " ] pois o mesmo não está aberto.");
                }

                if (PossuiRelacionamento(objArr))
                {
                    throw new Exception("Não foi possível realizar o cancelamento do lançamento [ " + dfNUMERO.Valor.ToString() + " ] pois o mesmo está relacionado a um outro lançamento.");
                }

                try
                {
                    dbs.Begin();

                    string sSql = @"UPDATE FLANCA SET CODSTATUS = ? WHERE CODEMPRESA = ? AND CODLANCA = ?";
                    dbs.QueryExec(sSql, 2, dfCODEMPRESA.Valor, dfCODLANCA.Valor);

                    dbs.Commit();
                }
                catch (Exception ex)
                {
                    dbs.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CancelaBaixaLancamento(Support.FinLanCanBaixaPar finLanCanBaixaPar)
        {
            try
            {
                string _nfoudup = string.Empty;

                if (finLanCanBaixaPar == null)
                {
                    throw new Exception("Atenção. Nenhum lançamento foi informado para o cancelamento da baixa.");
                }

                if (finLanCanBaixaPar.CodLanca == null)
                {
                    throw new Exception("Atenção. Nenhum lançamento foi informado para o cancelamento da baixa.");
                }

                foreach (int CodLanca in finLanCanBaixaPar.CodLanca)
                {
                    string sSql = string.Empty;
                    sSql = "SELECT * FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?";
                    System.Data.DataTable dtLanca = dbs.QuerySelect(sSql, finLanCanBaixaPar.CodEmpresa, CodLanca);
                    List<PS.Lib.DataField> objArr = gb.RetornaDataFieldByDataRow(dtLanca.Rows[0]);

                    PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
                    PS.Lib.DataField dfCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");
                    PS.Lib.DataField dfNUMERO = gb.RetornaDataFieldByCampo(objArr, "NUMERO");
                    PS.Lib.DataField dfIDEXTRATO = gb.RetornaDataFieldByCampo(objArr, "IDEXTRATO");
                    PS.Lib.DataField dfNFOUDUP = gb.RetornaDataFieldByCampo(objArr, "NFOUDUP");

                    if (VerificaSituacao(objArr) != 1)
                    {
                        throw new Exception("Não foi possível realizar o cancelamento da baixa do lançamento [ " + dfNUMERO.Valor.ToString() + " ] pois o mesmo não está baixado.");
                    }

                    sSql = "SELECT CODLANCARELAC FROM FLANCARELAC WHERE CODEMPRESA = ? AND CODLANCA = ?";
                    System.Data.DataTable dtLancaRelac = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODLANCA.Valor);

                    // Controle de Cheque
                    if (dfIDEXTRATO.Valor != null)
                    {
                        DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT *
                                                                                              FROM FEXTRATOLANCA
                                                                                              INNER JOIN FCHEQUE ON FEXTRATOLANCA.CODCHEQUE = FCHEQUE.CODCHEQUE
                                                                                              INNER JOIN FEXTRATO ON FEXTRATO.CODCHEQUE = FEXTRATOLANCA.CODCHEQUE
                                                                                              INNER JOIN FLANCA ON FEXTRATOLANCA.CODLANCA = FLANCA.CODLANCA
                                                                                              WHERE FEXTRATOLANCA.CODEMPRESA = ? AND FEXTRATOLANCA.IDEXTRATO = ?", new object[] { AppLib.Context.Empresa, dfIDEXTRATO.Valor });
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows.Count >= 1)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FLANCA SET DATABAIXA = ? WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { null, AppLib.Context.Empresa, dt.Rows[i]["CODLANCA"] });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FLANCA SET VLBAIXADO = ? WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { null, AppLib.Context.Empresa, dt.Rows[i]["CODLANCA"] });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FLANCA SET IDEXTRATO = ? WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { null, AppLib.Context.Empresa, dt.Rows[i]["CODLANCA"] });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FLANCA SET CODSTATUS = ? WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { 0, AppLib.Context.Empresa, dt.Rows[i]["CODLANCA"] });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FEXTRATO SET DATACOMPENSACAO = ? WHERE CODEMPRESA = ? AND IDEXTRATO = ?", new object[] { null, AppLib.Context.Empresa, dt.Rows[i]["IDEXTRATO"] });

                                    int Cheque = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT CODCHEQUE FROM FEXTRATOLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { AppLib.Context.Empresa, dt.Rows[i]["CODLANCA"] }));

                                    if (Cheque > 0)
                                    {
                                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM FEXTRATOLANCA WHERE CODEMPRESA = ? AND IDEXTRATO = ? AND CODLANCA = ?", new object[] { AppLib.Context.Empresa, dt.Rows[i]["IDEXTRATO"], dt.Rows[i]["CODLANCA"] });
                                    }
                                }

                                return;
                            }
                        }

                        sSql = @"UPDATE FLANCA SET IDEXTRATO = ? WHERE CODEMPRESA = ? AND CODLANCA = ?";
                        dbs.QueryExec(sSql, null, dfCODEMPRESA.Valor, dfCODLANCA.Valor);

                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM FEXTRATOLANCA WHERE CODEMPRESA = ? AND IDEXTRATO = ?", new object[] { dfCODEMPRESA.Valor, dfIDEXTRATO.Valor });

                        sSql = @"SELECT * FROM FLANCA WHERE CODEMPRESA = ? AND IDEXTRATO = ?";
                        DataTable dtLancaExt = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfIDEXTRATO.Valor);
                        foreach (System.Data.DataRow row in dtLancaExt.Rows)
                        {
                            bool Flag = true;
                            foreach (int CodLanca1 in finLanCanBaixaPar.CodLanca)
                            {
                                if (CodLanca1 == Convert.ToInt32(row["CODLANCA"]))
                                {
                                    if (Flag)
                                        Flag = false;
                                }
                            }

                            if (Flag)
                            {
                                sSql = @"SELECT * FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?";
                                List<PS.Lib.DataField> temp1 = gb.RetornaDataFieldByDataRow(dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, row["CODLANCA"]).Rows[0]);

                                Support.FinLanCanBaixaPar finLanCanBaixaPar1 = new Support.FinLanCanBaixaPar();
                                finLanCanBaixaPar1.CodEmpresa = PS.Lib.Contexto.Session.Empresa.CodEmpresa;
                                finLanCanBaixaPar1.CodLanca = new int[] { Convert.ToInt32(row["CODLANCA"]) };
                                finLanCanBaixaPar1.DataCancelamento = finLanCanBaixaPar.DataCancelamento;
                                finLanCanBaixaPar1.MotivoCancelamento = finLanCanBaixaPar.MotivoCancelamento;
                                finLanCanBaixaPar1.UsuarioCancelamento = finLanCanBaixaPar.UsuarioCancelamento;
                                this.CancelaBaixaLancamento(finLanCanBaixaPar1);
                            }
                        }
                    }

                    if (dfNFOUDUP.Valor.ToString().Equals("1"))
                    {
                        sSql = @"SELECT CODLANCA FROM FLANCA WHERE CODFATURA = ? AND CODEMPRESA = ?";
                        DataTable dtNFOUDUP = dbs.QuerySelect(sSql, dfCODLANCA.Valor, dfCODEMPRESA.Valor);
                        for (int i = 0; i < dtNFOUDUP.Rows.Count; i++)
                        {
                            Support.FinLanCanBaixaPar finLanCanBaixaPar1 = new Support.FinLanCanBaixaPar();
                            finLanCanBaixaPar1.CodEmpresa = PS.Lib.Contexto.Session.Empresa.CodEmpresa;
                            finLanCanBaixaPar1.CodLanca = new int[] { Convert.ToInt32(dtNFOUDUP.Rows[i]["CODLANCA"]) };
                            finLanCanBaixaPar1.DataCancelamento = finLanCanBaixaPar.DataCancelamento;
                            finLanCanBaixaPar1.MotivoCancelamento = finLanCanBaixaPar.MotivoCancelamento;
                            finLanCanBaixaPar1.UsuarioCancelamento = finLanCanBaixaPar.UsuarioCancelamento;
                            this.CancelaBaixaLancamento(finLanCanBaixaPar1);
                        }
                    }

                    try
                    {
                        dbs.Begin();

                        sSql = @"UPDATE FLANCA 
                                    SET CODSTATUS = ?, DATABAIXA = ?, VLBAIXADO = ?, DATACANCELAMENTOBAIXA = ?, CODUSUARIOCANCELAMENTOBAIXA = ?, MOTIVOCANCELAMENTOBAIXA = ? 
                                WHERE CODEMPRESA = ? AND CODLANCA = ?";
                        dbs.QueryExec(sSql, 0, null, 0, finLanCanBaixaPar.DataCancelamento, finLanCanBaixaPar.UsuarioCancelamento, finLanCanBaixaPar.MotivoCancelamento,
                            dfCODEMPRESA.Valor, dfCODLANCA.Valor);

                        //string relac = dbs.QueryValue(string.Empty, "select CODLANCARELAC from FLANCARELAC WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { dfCODLANCA.Valor, dfCODEMPRESA.Valor }).ToString();
                        int codRelac = Convert.ToInt32(dbs.QueryValue(0, @"SELECT CODRELLANCA FROM FRELLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { dfCODLANCA.Valor, dfCODEMPRESA.Valor }));
                        if (!codRelac.Equals(0))
                        {
                            DataTable dtRelLanca = dbs.QuerySelect(@"SELECT CODLANCA FROM FRELLANCA WHERE CODLANCA <> ? AND CODRELLANCA = ? AND CODEMPRESA = ?", new object[] { dfCODLANCA.Valor, codRelac, dfCODEMPRESA.Valor });
                            for (int i = 0; i < dtRelLanca.Rows.Count; i++)
                            {
                                dbs.QueryExec(@"UPDATE FLANCA SET CODSTATUS = ?, DATABAIXA = ?, VLBAIXADO = ?, DATACANCELAMENTOBAIXA = ?, CODUSUARIOCANCELAMENTOBAIXA = ?, MOTIVOCANCELAMENTOBAIXA = ? FROM FLANCA INNER JOIN FTIPDOC ON FTIPDOC.CODEMPRESA = FLANCA.CODEMPRESA AND FTIPDOC.CODTIPDOC = FLANCA.CODTIPDOC WHERE FLANCA.CODEMPRESA = ? AND FLANCA.CODLANCA = ? AND FTIPDOC.CLASSIFICACAO <> 0", 0, null, null, finLanCanBaixaPar.DataCancelamento, finLanCanBaixaPar.UsuarioCancelamento, finLanCanBaixaPar.MotivoCancelamento, dfCODEMPRESA.Valor, dtRelLanca.Rows[i]["CODLANCA"]);
                            }
                            dbs.QueryExec(@"DELETE FROM FRELLANCA WHERE CODRELLANCA = ? AND CODEMPRESA = ?", new object[] { codRelac, dfCODEMPRESA.Valor });
                        }

                        sSql = @"SELECT CODOPER FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?";
                        int CodOper = Convert.ToInt32(dbs.QueryValue(0, sSql, dfCODEMPRESA.Valor, dfCODLANCA.Valor));
                        if (CodOper > 0)
                        {
                            sSql = @"SELECT COUNT(CODLANCA) FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ? AND CODSTATUS = 0";
                            int aberto = Convert.ToInt32(dbs.QueryValue(0, sSql, dfCODEMPRESA.Valor, CodOper));
                            sSql = @"SELECT COUNT(CODLANCA) FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ? AND CODSTATUS = 1";
                            int baixado = Convert.ToInt32(dbs.QueryValue(0, sSql, dfCODEMPRESA.Valor, CodOper));

                            if (aberto == 0)
                            {
                                if (baixado > 0)
                                {
                                    sSql = @"UPDATE GOPER SET CODSTATUS = 4 WHERE CODEMPRESA = ? AND CODOPER = ?";
                                    dbs.QueryExec(sSql, dfCODEMPRESA.Valor, CodOper);
                                }
                            }
                            else
                            {
                                if (baixado > 0)
                                {
                                    sSql = @"UPDATE GOPER SET CODSTATUS = 3 WHERE CODEMPRESA = ? AND CODOPER = ?";
                                    dbs.QueryExec(sSql, dfCODEMPRESA.Valor, CodOper);
                                }
                                else
                                {
                                    sSql = @"SELECT CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";
                                    string CodTipOper = dbs.QueryValue(string.Empty, sSql, dfCODEMPRESA.Valor, CodOper).ToString();
                                    if (string.IsNullOrEmpty(CodTipOper))
                                    {
                                        throw new Exception("Não foi possível realizar o cancelamento da baixa do lançamento [ " + dfNUMERO.Valor.ToString() + " ] pois o movimento relacionado ao lançamento não possui tipo de operação informada.");
                                    }

                                    DataRow PARAMTIPOPERDESTINO = gb.RetornaParametrosOperacao(CodTipOper);

                                    if (PARAMTIPOPERDESTINO == null)
                                    {
                                        throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação [" + CodTipOper + "].");
                                    }

                                    sSql = @"UPDATE GOPER SET CODSTATUS = ? WHERE CODEMPRESA = ? AND CODOPER = ?";
                                    if (Convert.ToInt32(PARAMTIPOPERDESTINO["CRIAROPFAT"]) == 1)
                                    {
                                        dbs.QueryExec(sSql, 1, dfCODEMPRESA.Valor, CodOper);
                                    }
                                    else
                                    {
                                        dbs.QueryExec(sSql, 0, dfCODEMPRESA.Valor, CodOper);
                                    }
                                }
                            }
                        }
                        dbs.Commit();

                        if (dfNFOUDUP.Valor.ToString().Equals("0"))
                        {

                            DataTable dtNFOUDUP = dbs.QuerySelect(@"SELECT CODLANCA FROM FLANCA WHERE CODFATURA = ? AND CODEMPRESA = ?", dfCODLANCA.Valor, dfCODEMPRESA.Valor);
                            for (int i = 0; i < dtNFOUDUP.Rows.Count; i++)
                            {
                                Support.FinLanCanBaixaPar finLanCanBaixaPar1 = new Support.FinLanCanBaixaPar();
                                finLanCanBaixaPar1.CodEmpresa = PS.Lib.Contexto.Session.Empresa.CodEmpresa;
                                finLanCanBaixaPar1.CodLanca = new int[] { Convert.ToInt32(dtNFOUDUP.Rows[i]["CODLANCA"]) };
                                finLanCanBaixaPar1.DataCancelamento = finLanCanBaixaPar.DataCancelamento;
                                finLanCanBaixaPar1.MotivoCancelamento = finLanCanBaixaPar.MotivoCancelamento;
                                finLanCanBaixaPar1.UsuarioCancelamento = finLanCanBaixaPar.UsuarioCancelamento;
                                _nfoudup = "0";
                                this.CancelaBaixaLancamento(finLanCanBaixaPar1);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dbs.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void BaixaLancamento(Support.FinLanBaixaPar finLanBaixaPar, Class.GeraExtrato _gerar = null)
        {
            try
            {
                if (finLanBaixaPar != null)
                {
                    for (int i = 0; i < finLanBaixaPar.CodLanca.Length; i++)
                    {
                        string sSql = @"SELECT * FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?";
                        List<PS.Lib.DataField> objArr = gb.RetornaDataFieldByDataRow(dbs.QuerySelect(sSql, finLanBaixaPar.CodEmpresa, finLanBaixaPar.CodLanca[i]).Rows[0]);

                        PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
                        PS.Lib.DataField dfCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");
                        PS.Lib.DataField dfCODFILIAL = gb.RetornaDataFieldByCampo(objArr, "CODFILIAL");
                        PS.Lib.DataField dfNUMERO = gb.RetornaDataFieldByCampo(objArr, "NUMERO");
                        PS.Lib.DataField dfCODTIPDOC = gb.RetornaDataFieldByCampo(objArr, "CODTIPDOC");
                        PS.Lib.DataField dfVLLIQUIDO = gb.RetornaDataFieldByCampo(objArr, "VLLIQUIDO");
                        PS.Lib.DataField dfCODCCUSTO = gb.RetornaDataFieldByCampo(objArr, "CODCCUSTO");
                        PS.Lib.DataField dfCODNATUREZAORCAMENTO = gb.RetornaDataFieldByCampo(objArr, "CODNATUREZAORCAMENTO");

                        if (VerificaSituacao(objArr) != 0)
                        {
                            throw new Exception("Não foi possível realizar a baixa do lançamento [ " + dfNUMERO.Valor + " ] pois o lançamento não está em aberto.");
                        }

                        System.Data.DataRow PARAMTIPDOC = gb.RetornaParametrosTipoDocumento(dfCODTIPDOC.Valor.ToString());
                        if (PARAMTIPDOC != null)
                        {
                            List<PS.Lib.DataField> TipDocParam = gb.RetornaDataFieldByDataRow(PARAMTIPDOC);
                            PS.Lib.DataField dfCLASSIFICACAO = gb.RetornaDataFieldByCampo(TipDocParam, "CLASSIFICACAO");

                            if (Convert.ToInt32(dfCLASSIFICACAO.Valor) == 3)
                            {
                                throw new Exception("Não foi possível realizar a baixa do lançamento [ " + dfNUMERO.Valor + " ] pois o tipo do documento é de previsão.");
                            }

                            if (Convert.ToDecimal(dfVLLIQUIDO.Valor) > finLanBaixaPar.ValorBaixa[i])
                            {
                                try
                                {
                                    decimal ValorParcela = Convert.ToDecimal(dfVLLIQUIDO.Valor) - finLanBaixaPar.ValorBaixa[i];

                                    dbs.Begin();

                                    // João Pedro Luchiari - 17/07/2018
                                    //sSql = @"UPDATE FLANCA SET CODCONTA = ?, CODSTATUS = ? , DATABAIXA = ? , VLBAIXADO = ?, CODCHEQUE = ? WHERE CODEMPRESA = ? AND CODLANCA = ?";
                                    //dbs.QueryExec(sSql, finLanBaixaPar.CodConta, 1, finLanBaixaPar.DataBaixa, finLanBaixaPar.ValorBaixa[i], finLanBaixaPar.CODCHEQUE, dfCODEMPRESA.Valor, dfCODLANCA.Valor);

                                    sSql = @"UPDATE FLANCA SET CODCONTA = ?, CODSTATUS = ? , DATABAIXA = ? , VLBAIXADO = ? WHERE CODEMPRESA = ? AND CODLANCA = ?";
                                    dbs.QueryExec(sSql, finLanBaixaPar.CodConta, 1, finLanBaixaPar.DataBaixa, finLanBaixaPar.ValorBaixa[i], dfCODEMPRESA.Valor, dfCODLANCA.Valor);

                                    List<PS.Lib.DataField> NewObjArr = new List<Lib.DataField>();

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
                                    PS.Lib.DataField FIN_CODMOEDA = new PS.Lib.DataField("CODMOEDA", null);
                                    PS.Lib.DataField FIN_VLORIGINAL = new PS.Lib.DataField("VLORIGINAL", 0);
                                    // PS.Lib.DataField FIN_VLLIQUIDO = new PS.Lib.DataField("VLLIQUIDO", 0);
                                    PS.Lib.DataField FIN_CODSTATUS = new PS.Lib.DataField("CODSTATUS", null);
                                    PS.Lib.DataField FIN_CODOPER = new PS.Lib.DataField("CODOPER", null);
                                    PS.Lib.DataField FIN_CODTIPDOC = new PS.Lib.DataField("CODTIPDOC", null);
                                    PS.Lib.DataField FIN_CODCONTA = new PS.Lib.DataField("CODCONTA", null);
                                    PS.Lib.DataField FIN_CODFORMA = new PS.Lib.DataField("CODFORMA", null);
                                    PS.Lib.DataField FIN_CODCCUSTO = new PS.Lib.DataField("CODCCUSTO", null);
                                    PS.Lib.DataField FIN_CODNATUREZAORCAMENTO = new PS.Lib.DataField("CODNATUREZAORCAMENTO", null);
                                    PS.Lib.DataField FIN_ORIGEM = new PS.Lib.DataField("ORIGEM", null);
                                    PS.Lib.DataField FIN_NSEQLANCA = new Lib.DataField("NSEQLANCA", null);
                                    PS.Lib.DataField FIN_NFOUDUP = new Lib.DataField("NFOUDUP", null);

                                    #endregion

                                    #region Sets

                                    FIN_CODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
                                    FIN_CODLANCA.Valor = 0;
                                    FIN_TIPOPAGREC = gb.RetornaDataFieldByCampo(objArr, "TIPOPAGREC");
                                    FIN_NUMERO.Valor = gb.RetornaDataFieldByCampo(objArr, "NUMERO");//this.GeraNumeroDocumentoBaixaParcial(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODLANCA.Valor));
                                    FIN_NSEQLANCA.Valor = Convert.ToInt32(dbs.QueryValue(0, @"SELECT MAX(NSEQLANCA) + 1 FROM FLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { dfCODLANCA.Valor, AppLib.Context.Empresa }));

                                    FIN_CODCLIFOR = gb.RetornaDataFieldByCampo(objArr, "CODCLIFOR");
                                    FIN_CODFILIAL = gb.RetornaDataFieldByCampo(objArr, "CODFILIAL");
                                    FIN_DATAEMISSAO = gb.RetornaDataFieldByCampo(objArr, "DATAEMISSAO");
                                    FIN_DATAVENCIMENTO = gb.RetornaDataFieldByCampo(objArr, "DATAVENCIMENTO");
                                    FIN_DATAPREVBAIXA = gb.RetornaDataFieldByCampo(objArr, "DATAPREVBAIXA"); ;
                                    FIN_CODMOEDA = gb.RetornaDataFieldByCampo(objArr, "CODMOEDA");
                                    FIN_VLORIGINAL.Valor = ValorParcela;
                                    // FIN_VLLIQUIDO.Valor = ValorParcela;
                                    FIN_CODSTATUS.Valor = 0;
                                    FIN_CODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
                                    FIN_CODTIPDOC = gb.RetornaDataFieldByCampo(objArr, "CODTIPDOC");
                                    FIN_CODCONTA = gb.RetornaDataFieldByCampo(objArr, "CODCONTA");
                                    FIN_CODFORMA = gb.RetornaDataFieldByCampo(objArr, "CODFORMA");
                                    FIN_CODCCUSTO = gb.RetornaDataFieldByCampo(objArr, "CODCCUSTO");
                                    FIN_CODNATUREZAORCAMENTO = gb.RetornaDataFieldByCampo(objArr, "CODNATUREZAORCAMENTO");
                                    FIN_ORIGEM.Valor = "F";
                                    // Verifica Tipo de Documento pra setar o NFOUDUP
                                    if (FIN_CODTIPDOC.Valor.ToString() == "FATP" || FIN_CODTIPDOC.Valor.ToString() == "FATR")
                                    {
                                        FIN_NFOUDUP.Valor = 0;
                                    }
                                    else
                                    {
                                        FIN_NFOUDUP.Valor = 3;
                                    }

                                    NewObjArr.Add(FIN_CODEMPRESA);
                                    NewObjArr.Add(FIN_CODLANCA);
                                    NewObjArr.Add(FIN_TIPOPAGREC);
                                    NewObjArr.Add(dfNUMERO);
                                    NewObjArr.Add(FIN_NSEQLANCA);
                                    NewObjArr.Add(FIN_CODCLIFOR);
                                    NewObjArr.Add(FIN_CODFILIAL);
                                    NewObjArr.Add(FIN_DATAEMISSAO);
                                    NewObjArr.Add(FIN_DATAVENCIMENTO);
                                    NewObjArr.Add(FIN_DATAPREVBAIXA);
                                    NewObjArr.Add(FIN_CODMOEDA);
                                    NewObjArr.Add(FIN_VLORIGINAL);
                                    // NewObjArr.Add(FIN_VLLIQUIDO);
                                    NewObjArr.Add(FIN_CODSTATUS);
                                    NewObjArr.Add(FIN_CODOPER);
                                    NewObjArr.Add(FIN_CODTIPDOC);
                                    NewObjArr.Add(FIN_CODCONTA);
                                    NewObjArr.Add(FIN_CODFORMA);
                                    NewObjArr.Add(FIN_CODCCUSTO);
                                    NewObjArr.Add(FIN_CODNATUREZAORCAMENTO);
                                    NewObjArr.Add(FIN_ORIGEM);
                                    NewObjArr.Add(FIN_NFOUDUP);

                                    #endregion

                                    List<PS.Lib.DataField> RetListObjArr = this.InsertRecord(NewObjArr);

                                    FIN_CODLANCA = gb.RetornaDataFieldByCampo(RetListObjArr, "CODLANCA");
                                    this.IncluiRelacionamento(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODLANCA.Valor), Convert.ToInt32(FIN_CODLANCA.Valor));
                                    dbs.Commit();
                                    if (finLanBaixaPar.GeraExtratoComo == Support.FinLanBaixaGeraExtratoComoEnum.UmExtratoParaCadaLancamento)
                                    {
                                        Support.FinLanExtratoPar finLanExtratoPar = new Support.FinLanExtratoPar();
                                        finLanExtratoPar.CodEmpresa = finLanBaixaPar.CodEmpresa;
                                        finLanExtratoPar.Data = finLanBaixaPar.DataBaixa;
                                        finLanExtratoPar.CodConta = finLanBaixaPar.CodConta;
                                        finLanExtratoPar.GeraExtratoComo = finLanBaixaPar.GeraExtratoComo;

                                        if (finLanBaixaPar.PagRec == Support.FinLanPagRecEnum.Pagar)
                                            finLanExtratoPar.Tipo = Support.FinLanBaixaExtratoTipoEnum.Saida;
                                        else
                                            finLanExtratoPar.Tipo = Support.FinLanBaixaExtratoTipoEnum.Entrada;

                                        finLanExtratoPar.NumeroExtrato = dfNUMERO.Valor.ToString();
                                        finLanExtratoPar.Historico = finLanBaixaPar.Historico;
                                        finLanExtratoPar.CodLanca = new int[1] { finLanBaixaPar.CodLanca[i] };
                                        finLanExtratoPar.Valor = new decimal[1] { finLanBaixaPar.ValorBaixa[i] };
                                        finLanExtratoPar.NFOUDUP = new string[1] { finLanBaixaPar.NFOUDUP[i] };
                                        finLanExtratoPar.cheque = finLanBaixaPar.cheque;

                                        if (finLanBaixaPar.CODCHEQUE != 0)
                                        {
                                            finLanExtratoPar.CODCHEQUE = finLanBaixaPar.CODCHEQUE;
                                        }

                                        this.GeraExtrato(finLanExtratoPar);

                                        // Um extrato para cada Lançamento
                                        List<string> List = new List<string>();

                                        for (int x = 0; x < finLanBaixaPar.CodLanca.Length; x++)
                                        {
                                            List.Add(finLanBaixaPar.CodLanca[x].ToString());
                                        }

                                        _gerar.getLancamento(List);

                                        if (_gerar.GerarExtratoLanca(false) > 0)
                                        {
                                            return;
                                        }
                                    }

                                    sSql = @"SELECT CODOPER FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?";
                                    int CodOper = Convert.ToInt32(dbs.QueryValue(0, sSql, dfCODEMPRESA.Valor, dfCODLANCA.Valor));
                                    if (CodOper > 0)
                                    {
                                        sSql = @"SELECT COUNT(CODLANCA) FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ? AND CODSTATUS = 0";
                                        int cont = Convert.ToInt32(dbs.QueryValue(0, sSql, dfCODEMPRESA.Valor, CodOper));
                                        if (cont == 0)
                                        {
                                            sSql = @"UPDATE GOPER SET CODSTATUS = 4 WHERE CODEMPRESA = ? AND CODOPER = ?";
                                            dbs.QueryExec(sSql, dfCODEMPRESA.Valor, CodOper);
                                        }
                                        else
                                        {
                                            sSql = @"UPDATE GOPER SET CODSTATUS = 3 WHERE CODEMPRESA = ? AND CODOPER = ?";
                                            dbs.QueryExec(sSql, dfCODEMPRESA.Valor, CodOper);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    dbs.Rollback();
                                    throw new Exception(ex.Message);
                                }
                            }
                            else
                            {
                                try
                                {
                                    dbs.Begin();

                                    // João Pedro Luchiari - 17/07/2018
                                    //sSql = @"UPDATE FLANCA SET CODCONTA = ?, CODSTATUS = ? , DATABAIXA = ? , VLBAIXADO = ?, CODCHEQUE = ? WHERE CODEMPRESA = ? AND CODLANCA = ?";
                                    //dbs.QueryExec(sSql, finLanBaixaPar.CodConta, 1, finLanBaixaPar.DataBaixa, finLanBaixaPar.ValorBaixa[i], finLanBaixaPar.CODCHEQUE, dfCODEMPRESA.Valor, dfCODLANCA.Valor);

                                    sSql = @"UPDATE FLANCA SET CODCONTA = ?, CODSTATUS = ? , DATABAIXA = ? , VLBAIXADO = ? WHERE CODEMPRESA = ? AND CODLANCA = ?";
                                    dbs.QueryExec(sSql, finLanBaixaPar.CodConta, 1, finLanBaixaPar.DataBaixa, finLanBaixaPar.ValorBaixa[i], dfCODEMPRESA.Valor, dfCODLANCA.Valor);

                                    if (finLanBaixaPar.GeraExtratoComo == Support.FinLanBaixaGeraExtratoComoEnum.UmExtratoParaCadaLancamento)
                                    {
                                        Support.FinLanExtratoPar finLanExtratoPar = new Support.FinLanExtratoPar();
                                        finLanExtratoPar.CodEmpresa = finLanBaixaPar.CodEmpresa;
                                        finLanExtratoPar.Data = finLanBaixaPar.DataBaixa;
                                        finLanExtratoPar.CodConta = finLanBaixaPar.CodConta;
                                        finLanExtratoPar.GeraExtratoComo = finLanBaixaPar.GeraExtratoComo;

                                        if (finLanBaixaPar.PagRec == Support.FinLanPagRecEnum.Pagar)
                                            finLanExtratoPar.Tipo = Support.FinLanBaixaExtratoTipoEnum.Saida;
                                        else
                                            finLanExtratoPar.Tipo = Support.FinLanBaixaExtratoTipoEnum.Entrada;

                                        finLanExtratoPar.NumeroExtrato = dfNUMERO.Valor.ToString();
                                        finLanExtratoPar.Historico = finLanBaixaPar.Historico;
                                        finLanExtratoPar.CodLanca = new int[1] { finLanBaixaPar.CodLanca[i] };
                                        finLanExtratoPar.Valor = new decimal[1] { finLanBaixaPar.ValorBaixa[i] };
                                        finLanExtratoPar.NFOUDUP = new string[1] { finLanBaixaPar.NFOUDUP[i] };

                                        if (string.IsNullOrEmpty(dfCODCCUSTO.Valor.ToString()))
                                        {
                                            finLanExtratoPar.codCCusto = null;
                                        }
                                        else
                                        {
                                            finLanExtratoPar.codCCusto = dfCODCCUSTO.Valor.ToString();
                                        }
                                        if (string.IsNullOrEmpty(dfCODNATUREZAORCAMENTO.Valor.ToString()))
                                        {
                                            finLanExtratoPar.codNaturezaOrcamento = null;
                                        }
                                        else
                                        {
                                            finLanExtratoPar.codNaturezaOrcamento = dfCODNATUREZAORCAMENTO.Valor.ToString();
                                        }

                                        if (finLanBaixaPar.CODCHEQUE != 0)
                                        {
                                            finLanExtratoPar.CODCHEQUE = finLanBaixaPar.CODCHEQUE;
                                        }

                                        if (!finLanExtratoPar.Valor[0].Equals(0))
                                        {
                                            this.GeraExtrato(finLanExtratoPar);

                                            List<string> ListLanca = new List<string>();
                                            for (int x = 0; x < finLanExtratoPar.CodLanca.Length; x++)
                                            {
                                                ListLanca.Add(finLanExtratoPar.CodLanca[x].ToString());
                                            }

                                            _gerar.getLancamento(ListLanca);

                                            if (_gerar.GerarExtratoLanca(false) > 0)
                                            {

                                            }
                                        }
                                    }

                                    sSql = @"SELECT CODOPER FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?";
                                    int CodOper = Convert.ToInt32(dbs.QueryValue(0, sSql, dfCODEMPRESA.Valor, dfCODLANCA.Valor));
                                    if (CodOper > 0)
                                    {
                                        sSql = @"SELECT COUNT(CODLANCA) FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ? AND CODSTATUS = 0";
                                        int cont = Convert.ToInt32(dbs.QueryValue(0, sSql, dfCODEMPRESA.Valor, CodOper));
                                        if (cont == 0)
                                        {
                                            sSql = @"UPDATE GOPER SET CODSTATUS = 4 WHERE CODEMPRESA = ? AND CODOPER = ?";
                                            dbs.QueryExec(sSql, dfCODEMPRESA.Valor, CodOper);
                                        }
                                        else
                                        {
                                            sSql = @"UPDATE GOPER SET CODSTATUS = 3 WHERE CODEMPRESA = ? AND CODOPER = ?";
                                            dbs.QueryExec(sSql, dfCODEMPRESA.Valor, CodOper);
                                        }
                                    }
                                    try
                                    {
                                        dbs.Commit();
                                    }
                                    catch (Exception)
                                    {

                                    }

                                }
                                catch (Exception ex)
                                {
                                    dbs.Rollback();
                                    throw new Exception(ex.Message);
                                }
                            }
                        }
                    }

                    if (finLanBaixaPar.GeraExtratoComo == Support.FinLanBaixaGeraExtratoComoEnum.UmExtratoParaTodosLancamentos)
                    {
                        bool fatura = false;
                        //Verifica se existe uma fatura no meio
                        Support.FinLanExtratoPar finLanExtratoPar = new Support.FinLanExtratoPar();

                        //João Pedro 30-10-2017
                        List<string> Codlist = new List<string>();
                        string Result = string.Empty;

                        //Funçaõ para concatenar valores da lista
                        for (int i = 0; i < finLanBaixaPar.CodLanca.Length; i++)
                        {
                            Codlist.Add(finLanBaixaPar.CodLanca[i].ToString());
                            Result = string.Join(",", Codlist.ToArray());
                        }

                        // João Pedro Luchiari 17/07/2018 - Método para ter acesso aos Códigos do Lançamento.
                        if (finLanBaixaPar.cheque == true)
                        {
                            _gerar.getCodCusto(finLanBaixaPar.codCCusto);
                            _gerar.getCodNatureza(finLanBaixaPar.codNaturezaOrcamento);
                            _gerar.getLancamento(Codlist);
                        }
                        else
                        {
                            _gerar.getLancamento(Codlist);
                        }

                        DataTable dtCodLanca = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT FLANCA.CODLANCA, FLANCA.CODTIPDOC, FLANCA.NFOUDUP, FLANCA.VLBAIXADO FROM FLANCA WHERE FLANCA.NFOUDUP <> 1 AND FLANCA.CODEMPRESA = ? AND FLANCA.CODLANCA IN(" + Result + ")", new object[] { AppLib.Context.Empresa });
                        Codlist.Clear();
                        finLanBaixaPar.CodLanca = new int[dtCodLanca.Rows.Count];
                        finLanBaixaPar.ValorBaixa = new decimal[dtCodLanca.Rows.Count];

                        for (int i = 0; i < dtCodLanca.Rows.Count; i++)
                        {
                            finLanBaixaPar.CodLanca[i] = Convert.ToInt32(dtCodLanca.Rows[i]["CODLANCA"].ToString());
                            finLanBaixaPar.ValorBaixa[i] = Convert.ToDecimal(dtCodLanca.Rows[i]["VLBAIXADO"]);
                        }

                        finLanExtratoPar.CodLanca = finLanBaixaPar.CodLanca;
                        finLanExtratoPar.Valor = finLanBaixaPar.ValorBaixa;

                        finLanExtratoPar.CodEmpresa = finLanBaixaPar.CodEmpresa;
                        finLanExtratoPar.Data = finLanBaixaPar.DataBaixa;
                        finLanExtratoPar.CodConta = finLanBaixaPar.CodConta;
                        finLanExtratoPar.GeraExtratoComo = finLanBaixaPar.GeraExtratoComo;

                        if (finLanBaixaPar.PagRec == Support.FinLanPagRecEnum.Pagar)
                            finLanExtratoPar.Tipo = Support.FinLanBaixaExtratoTipoEnum.Saida;
                        else
                            finLanExtratoPar.Tipo = Support.FinLanBaixaExtratoTipoEnum.Entrada;

                        finLanExtratoPar.NumeroExtrato = finLanBaixaPar.NumeroExtrato;
                        finLanExtratoPar.Historico = finLanBaixaPar.Historico;


                        finLanExtratoPar.codCCusto = finLanBaixaPar.codCCusto;
                        finLanExtratoPar.codNaturezaOrcamento = finLanBaixaPar.codNaturezaOrcamento;
                        finLanExtratoPar.cheque = finLanBaixaPar.cheque;

                        if (finLanExtratoPar.cheque == true)
                        {
                            if (_gerar.GerarExtrato() > 0)
                            {
                                if (_gerar.GerarExtratoLanca(true) > 0)
                                {
                                    return;
                                }
                            }
                            else
                            {
                                // Mensagem
                            }
                        }

                        this.GeraExtrato(finLanExtratoPar);

                        if (_gerar.GerarExtratoLanca(false) > 0)
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void IncluiRelacionamento(int CodEmpresa, int CodLancamento, int CodLancamentoRelac)
        {
            string sSql = string.Empty;

            sSql = @"SELECT CODLANCARELAC FROM FLANCARELAC WHERE CODEMPRESA = ? AND CODLANCA = ?";

            int CodRelac = int.Parse(dbs.QueryValue("0", sSql, CodEmpresa, CodLancamento).ToString());

            if (CodRelac == 0)
            {
                sSql = @"INSERT INTO FLANCARELAC (CODEMPRESA, CODLANCA, CODLANCARELAC) VALUES(?,?,?)";

                dbs.QueryExec(sSql, CodEmpresa, CodLancamento, CodLancamentoRelac);
            }
        }

        public int VerificaSituacao(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;
            try
            {
                PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
                PS.Lib.DataField dtCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");

                sSql = @"SELECT CODSTATUS FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?";

                return int.Parse(dbs.QueryValue("0", sSql, dtCODEMPRESA.Valor, dtCODLANCA.Valor).ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public string GeraNumeroDocumentoBaixaParcial(int CodEmpresa, int CodLanca)
        {
            List<PS.Lib.DataField> objArr = gb.RetornaDataFieldByDataRow(dbs.QuerySelect("SELECT NUMERO FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", CodEmpresa, CodLanca).Rows[0]);
            PS.Lib.DataField dfNUMERO = gb.RetornaDataFieldByCampo(objArr, "NUMERO");

            System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();
            if (PARAMVAREJO == null)
            {
                throw new Exception("Não foi possível gerar o número do lançamento.");
            }
            else
            {
                int mask = (PARAMVAREJO["MASKNUMEROSEQ"] == DBNull.Value) ? 0 : Convert.ToInt32(PARAMVAREJO["MASKNUMEROSEQ"]);
                if (mask == 0)
                {
                    throw new Exception("Não foi possível gerar o número do lançamento pois a máscara do numero do lançamento não está parametrizada.");
                }
                else
                {
                    int qtdzeros = 0;
                    string newnum = dfNUMERO.Valor.ToString();
                    for (int i = 0; i < newnum.Length; i++)
                    {
                        if (newnum.Substring(i, 1) == "0")
                            qtdzeros++;
                        else
                            break;
                    }

                    if (qtdzeros > 0)
                        newnum = newnum.Substring(qtdzeros, newnum.Length - qtdzeros);

                    string novoNumStr = string.Empty;
                    bool flag = true;
                    int cont = 0;
                    while (flag)
                    {
                        cont++;
                        novoNumStr = (string.Concat(newnum, "-", cont).PadLeft(mask, '0'));

                        if (novoNumStr.Length > mask)
                        {
                            throw new Exception("Quantidade de caracteres do número do documento é maior que a quantidade permitida.");
                        }
                        else
                        {
                            string sSql = "SELECT CODLANCA FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA <> ? AND NUMERO = ?";
                            flag = dbs.QueryFind(sSql, CodEmpresa, CodLanca, novoNumStr);
                        }
                    }

                    return novoNumStr;
                }
            }
        }

        public string GeraNumeroDocumento(string tipdoc)
        {
            string novoNumStr = "";
            if (string.IsNullOrEmpty(tipdoc))
            {
                throw new Exception("Não foi possível gerar o número do documento pois o tipo do documento não foi informado.");
            }

            System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();
            if (PARAMVAREJO == null)
            {
                throw new Exception("Não foi possível gerar o número do lançamento.");
            }
            else
            {
                System.Data.DataRow PARAMTIPDOC = gb.RetornaParametrosTipoDocumento(tipdoc);
                if (PARAMTIPDOC != null)
                {
                    if (int.Parse(PARAMTIPDOC["USANUMEROSEQ"].ToString()) == 1)
                    {
                        int mask = (PARAMVAREJO["MASKNUMEROSEQ"] == DBNull.Value) ? 0 : Convert.ToInt32(PARAMVAREJO["MASKNUMEROSEQ"]);
                        if (mask == 0)
                        {
                            throw new Exception("Não foi possível gerar o número do lançamento pois a máscara do numero do lançamento não está parametrizada.");
                        }
                        else
                        {
                            int ultimo = (PARAMTIPDOC["ULTIMONUMERO"] == DBNull.Value) ? 0 : Convert.ToInt32(PARAMTIPDOC["ULTIMONUMERO"]);
                            ultimo++;
                            int tamanho = ultimo.ToString().Length;
                            novoNumStr = string.Concat(ultimo.ToString().PadLeft(mask, '0'));

                            if (novoNumStr.Length > mask)
                            {
                                throw new Exception("Quantidade de caracteres do número do documento é maior que a quantidade permitida.");
                            }
                            else
                            {
                                string sSql = @"UPDATE FTIPDOC SET ULTIMONUMERO = ? WHERE CODEMPRESA = ? AND CODTIPDOC = ?";
                                dbs.QueryExec(sSql, novoNumStr, PS.Lib.Contexto.Session.Empresa.CodEmpresa, tipdoc);
                                return novoNumStr;
                            }
                        }
                    }
                    else
                    {
                        return novoNumStr;
                    }
                }
                else
                {
                    throw new Exception("Não foi possível gerar o número do lançamento pois o tipo de documento não esta parametrizado.");
                }
            }
        }

        public void PossuiSaldoFinanceiro(int CodEmpresa, string CodCliFor, int CodLanca, decimal ValorAdicional)
        {
            System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();
            if (PARAMVAREJO == null)
            {
                throw new Exception("Não foi possível carregar os parâmetro globais.");
            }
            else
            {
                if (Convert.ToInt32(PARAMVAREJO["CONTROLALIMITECREDITO"]) == 1)
                {
                    decimal ValorAberto = 0;
                    string sSql = @"SELECT ISNULL(X.VALORABERTO,0) VALORABERTO
FROM
(
SELECT 
SUM(ISNULL(L.VLORIGINAL,0)) - SUM(ISNULL(L.VLBAIXADO,0))  VALORABERTO
FROM   FLANCA L (NOLOCK), FTIPDOC D (NOLOCK), VCLIFOR F
WHERE  L.CODSTATUS IN (0) 
AND L.CODEMPRESA = D.CODEMPRESA 
AND L.CODTIPDOC = D.CODTIPDOC 
AND D.CLASSIFICACAO <> 0
AND D.CLASSIFICACAO <> 1
AND D.CLASSIFICACAO <> 3
AND L.TIPOPAGREC = 1
AND F.CODEMPRESA = L.CODEMPRESA
AND F.CODCLIFOR = L.CODCLIFOR
AND L.CODEMPRESA = ?
AND L.CODCLIFOR = ?
/**/
)X";

                    if (CodLanca > 0)
                    {
                        sSql = sSql.Replace("/**/", "AND L.CODLANCA <> ?");
                        ValorAberto = Convert.ToDecimal(dbs.QueryValue(0, sSql, CodEmpresa, CodCliFor, CodLanca));
                    }
                    else
                    {
                        ValorAberto = Convert.ToDecimal(dbs.QueryValue(0, sSql, CodEmpresa, CodCliFor));
                    }

                    sSql = @"SELECT LIMITECREDITO FROM VCLIFOR WHERE CODEMPRESA = ? AND CODCLIFOR = ?";
                    decimal LimiteCredito = Convert.ToDecimal(dbs.QueryValue(0, sSql, CodEmpresa, CodCliFor));
                    decimal Saldo = LimiteCredito - (ValorAberto + ValorAdicional);
                    if (Saldo < 0)
                    {
                        throw new Exception("Cliente/Fornecedor não tem crédito disponivel.");
                    }
                }
            }
        }

        public void GeraExtrato(Support.FinLanExtratoPar finLanExtratoPar)
        {
            int CODFILIAL = 0;

            string sSql = string.Empty;
            try
            {
                if (finLanExtratoPar != null)
                {
                    if (finLanExtratoPar.CodLanca.Length <= 0)
                    {
                        throw new Exception("Não foi possível gerar o extrato pois não foi informado a baixa de origem.");
                    }

                    if (finLanExtratoPar.Valor.Length <= 0)
                    {
                        throw new Exception("Não foi possível gerar o extrato pois não foi informado o valor.");
                    }
                    else
                    {
                        foreach (decimal ValorBaixa in finLanExtratoPar.Valor)
                        {
                            if (ValorBaixa <= 0)
                            {
                                throw new Exception("Não foi possível gerar o extrato pois o valor da baixa deve ser maior que 0 (zero).");
                            }
                        }
                    }

                    if (finLanExtratoPar.GeraExtratoComo == Support.FinLanBaixaGeraExtratoComoEnum.UmExtratoParaTodosLancamentos)
                    {
                        if (string.IsNullOrEmpty(finLanExtratoPar.NumeroExtrato))
                        {
                            throw new Exception("Não foi possível gerar o extrato pois não foi informado o número do extrato.");
                        }
                    }

                    if (string.IsNullOrEmpty(finLanExtratoPar.CodConta))
                    {
                        throw new Exception("Não foi possível gerar o extrato pois não foi informada a conta/caixa.");
                    }

                    #region Atributos

                    PS.Lib.DataField EX_CODEMPRESA = new PS.Lib.DataField("CODEMPRESA", null);
                    PS.Lib.DataField EX_IDEXTRATO = new PS.Lib.DataField("IDEXTRATO", null, typeof(int), PS.Lib.Global.TypeAutoinc.AutoInc);
                    PS.Lib.DataField EX_CODFILIAL = new PS.Lib.DataField("CODFILIAL", null);
                    PS.Lib.DataField EX_CODCONTA = new PS.Lib.DataField("CODCONTA", null);
                    PS.Lib.DataField EX_NUMERODOCUMENTO = new PS.Lib.DataField("NUMERODOCUMENTO", null);
                    PS.Lib.DataField EX_DATA = new PS.Lib.DataField("DATA", null);
                    PS.Lib.DataField EX_VALOR = new PS.Lib.DataField("VALOR", null);
                    PS.Lib.DataField EX_HISTORICO = new PS.Lib.DataField("HISTORICO", null);
                    PS.Lib.DataField EX_TIPO = new PS.Lib.DataField("TIPO", null);
                    PS.Lib.DataField EX_CODCCUSTO = new PS.Lib.DataField("CODCCUSTO", null);
                    PS.Lib.DataField EX_CODNATUREZAORCAMENTO = new PS.Lib.DataField("CODNATUREZAORCAMENTO", null);
                    PS.Lib.DataField EX_CHEQUE = new PS.Lib.DataField("CHEQUE", null);

                    #endregion

                    if (finLanExtratoPar.GeraExtratoComo == Support.FinLanBaixaGeraExtratoComoEnum.UmExtratoParaCadaLancamento)
                    {
                        for (int i = 0; i < finLanExtratoPar.CodLanca.Length; i++)
                        {
                            #region Sets

                            EX_CODEMPRESA.Valor = finLanExtratoPar.CodEmpresa;
                            EX_IDEXTRATO.Valor = 0;
                            EX_CODFILIAL.Valor = dbs.QueryValue(0, "SELECT CODFILIAL FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", finLanExtratoPar.CodEmpresa, finLanExtratoPar.CodLanca[i]);
                            CODFILIAL = int.Parse(EX_CODFILIAL.Valor.ToString());
                            EX_CODCONTA.Valor = finLanExtratoPar.CodConta;
                            EX_NUMERODOCUMENTO.Valor = finLanExtratoPar.NumeroExtrato;
                            EX_DATA.Valor = finLanExtratoPar.Data;
                            EX_HISTORICO.Valor = finLanExtratoPar.Historico;
                            EX_VALOR.Valor = finLanExtratoPar.Valor[i];
                            EX_CODCCUSTO.Valor = finLanExtratoPar.codCCusto;
                            EX_CODNATUREZAORCAMENTO.Valor = finLanExtratoPar.codNaturezaOrcamento;


                            if (finLanExtratoPar.Tipo == Support.FinLanBaixaExtratoTipoEnum.Entrada)
                                EX_TIPO.Valor = 0;

                            if (finLanExtratoPar.Tipo == Support.FinLanBaixaExtratoTipoEnum.Saida)
                                EX_TIPO.Valor = 1;

                            #endregion

                            List<PS.Lib.DataField> objArrExTrf = new List<Lib.DataField>();
                            objArrExTrf.Add(EX_CODEMPRESA);
                            objArrExTrf.Add(EX_IDEXTRATO);
                            objArrExTrf.Add(EX_CODFILIAL);
                            objArrExTrf.Add(EX_CODCONTA);
                            objArrExTrf.Add(EX_NUMERODOCUMENTO);
                            objArrExTrf.Add(EX_DATA);
                            objArrExTrf.Add(EX_HISTORICO);
                            objArrExTrf.Add(EX_VALOR);
                            objArrExTrf.Add(EX_TIPO);
                            objArrExTrf.Add(EX_CODCCUSTO);
                            objArrExTrf.Add(EX_CODNATUREZAORCAMENTO);

                            PSPartExtratoCaixaData psPartExtratoCaixaData = new PSPartExtratoCaixaData();
                            psPartExtratoCaixaData._tablename = "FEXTRATO";
                            psPartExtratoCaixaData._keys = new string[] { "CODEMPRESA", "IDEXTRATO" };

                            if (finLanExtratoPar.CODCHEQUE == null)
                            {
                                if (GetCodFatura(Convert.ToInt32(finLanExtratoPar.CodLanca[i]), finLanExtratoPar.CodEmpresa).Equals(true))
                                {
                                    objArrExTrf = psPartExtratoCaixaData.SaveRecord(objArrExTrf);

                                    PS.Lib.DataField dfIDEXTRATO = gb.RetornaDataFieldByCampo(objArrExTrf, "IDEXTRATO");
                                    sSql = @"UPDATE FLANCA SET IDEXTRATO = ? WHERE CODEMPRESA = ? AND CODLANCA = ?";
                                    dbs.QueryExec(sSql, dfIDEXTRATO.Valor, finLanExtratoPar.CodEmpresa, finLanExtratoPar.CodLanca[i]);
                                }
                            }

                        }
                    }
                    else
                    {
                        #region Sets

                        EX_CODEMPRESA.Valor = finLanExtratoPar.CodEmpresa;
                        EX_IDEXTRATO.Valor = 0;
                        EX_CODFILIAL.Valor = dbs.QueryValue(0, "SELECT CODFILIAL FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", finLanExtratoPar.CodEmpresa, finLanExtratoPar.CodLanca[0]);
                        CODFILIAL = int.Parse(EX_CODFILIAL.Valor.ToString());
                        EX_CODCONTA.Valor = finLanExtratoPar.CodConta;
                        EX_NUMERODOCUMENTO.Valor = finLanExtratoPar.NumeroExtrato;
                        EX_DATA.Valor = finLanExtratoPar.Data;
                        EX_HISTORICO.Valor = finLanExtratoPar.Historico;
                        EX_CODCCUSTO.Valor = finLanExtratoPar.codCCusto;
                        EX_CODNATUREZAORCAMENTO.Valor = finLanExtratoPar.codNaturezaOrcamento;
                        EX_VALOR.Valor = 0;

                        foreach (decimal ValorBaixa in finLanExtratoPar.Valor)
                        {
                            EX_VALOR.Valor = Convert.ToDecimal(EX_VALOR.Valor) + ValorBaixa;
                        }

                        if (finLanExtratoPar.Tipo == Support.FinLanBaixaExtratoTipoEnum.Entrada)
                        {
                            if (finLanExtratoPar.cheque)
                            {
                                EX_TIPO.Valor = 5;
                            }
                            else
                            {
                                EX_TIPO.Valor = 0;
                            }

                        }


                        if (finLanExtratoPar.Tipo == Support.FinLanBaixaExtratoTipoEnum.Saida)
                        {
                            if (finLanExtratoPar.cheque)
                            {
                                EX_TIPO.Valor = 4;
                            }
                            else
                            {
                                EX_TIPO.Valor = 1;
                            }
                        }
                        #endregion

                        List<PS.Lib.DataField> objArrExTrf = new List<Lib.DataField>();
                        objArrExTrf.Add(EX_CODEMPRESA);
                        objArrExTrf.Add(EX_IDEXTRATO);
                        objArrExTrf.Add(EX_CODFILIAL);
                        objArrExTrf.Add(EX_CODCONTA);
                        objArrExTrf.Add(EX_NUMERODOCUMENTO);
                        objArrExTrf.Add(EX_DATA);
                        objArrExTrf.Add(EX_HISTORICO);
                        objArrExTrf.Add(EX_VALOR);
                        objArrExTrf.Add(EX_TIPO);
                        objArrExTrf.Add(EX_CODCCUSTO);
                        objArrExTrf.Add(EX_CODNATUREZAORCAMENTO);

                        PSPartExtratoCaixaData psPartExtratoCaixaData = new PSPartExtratoCaixaData();
                        psPartExtratoCaixaData._tablename = "FEXTRATO";
                        psPartExtratoCaixaData._keys = new string[] { "CODEMPRESA", "IDEXTRATO" };

                        if (GetCodFatura(Convert.ToInt32(finLanExtratoPar.CodLanca[0]), finLanExtratoPar.CodEmpresa).Equals(true))
                        {
                            if (finLanExtratoPar.CODCHEQUE == null)
                            {
                                objArrExTrf = psPartExtratoCaixaData.InsertRecord(objArrExTrf);
                                PS.Lib.DataField dfIDEXTRATO = gb.RetornaDataFieldByCampo(objArrExTrf, "IDEXTRATO");
                                for (int ii = 0; ii < finLanExtratoPar.CodLanca.Length; ii++)
                                {
                                    sSql = @"UPDATE FLANCA SET IDEXTRATO = ? WHERE CODEMPRESA = ? AND CODLANCA = ?";
                                    dbs.QueryExec(sSql, dfIDEXTRATO.Valor, finLanExtratoPar.CodEmpresa, finLanExtratoPar.CodLanca[ii]);
                                }
                            }
                        }
                    }

                    finLanExtratoPar.CodFilial = CODFILIAL;
                    GerarExtratoCheque(finLanExtratoPar);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private bool GetCodFatura(int codLanca, int codEmpresa)
        {
            try
            {

                if (string.IsNullOrEmpty(dbs.QueryValue(string.Empty, "SELECT CODFATURA FROM FLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { codLanca, codEmpresa }).ToString()))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean ChequeJaExisteNoExtrato(int CODEMPRESA, int CODCHEQUE)
        {
            String NUMERODOCUMENTO = getFCHEQUE_NUMERODOCUMENTO(CODEMPRESA, CODCHEQUE);

            String consulta = "SELECT 1 FROM FEXTRATO WHERE CODEMPRESA = ? AND NUMERODOCUMENTO = ? AND TIPO IN (4, 5)";
            return dbs.QueryFind(consulta, CODEMPRESA, NUMERODOCUMENTO);
        }

        public String getFCHEQUE_NUMERODOCUMENTO(int CODEMPRESA, int CODCHEQUE)
        {
            return dbs.QueryValue(null, "SELECT NUMERO FROM FCHEQUE WHERE CODEMPRESA = ? AND CODCHEQUE = ?", CODEMPRESA, CODCHEQUE).ToString();
        }

        public void GerarExtratoCheque(Support.FinLanExtratoPar finLanExtratoPar)
        {
            if (finLanExtratoPar.CODCHEQUE != null)
            {
                if (this.ChequeJaExisteNoExtrato(finLanExtratoPar.CodEmpresa, (int)finLanExtratoPar.CODCHEQUE))
                {
                    // não pode de incluir novamente
                    dbs.Commit();
                }
                else
                {
                    System.Data.DataTable dtFCHEQUE = dbs.QuerySelect("SELECT * FROM FCHEQUE WHERE CODEMPRESA = ? AND CODCHEQUE = ?", new Object[] { finLanExtratoPar.CodEmpresa, finLanExtratoPar.CODCHEQUE });

                    AppLib.ORM.Jit FEXTRATO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "FEXTRATO");
                    FEXTRATO.Set("CODEMPRESA", finLanExtratoPar.CodEmpresa);
                    FEXTRATO.Set("IDEXTRATO", this.ProximoIDLOG(finLanExtratoPar.CodEmpresa, "FEXTRATO"));
                    FEXTRATO.Set("CODFILIAL", finLanExtratoPar.CodFilial);
                    FEXTRATO.Set("CODCONTA", finLanExtratoPar.CodConta);

                    String NUMERODOCUMENTO = getFCHEQUE_NUMERODOCUMENTO(finLanExtratoPar.CodEmpresa, (int)finLanExtratoPar.CODCHEQUE);
                    FEXTRATO.Set("NUMERODOCUMENTO", NUMERODOCUMENTO);

                    FEXTRATO.Set("DATA", finLanExtratoPar.Data);
                    FEXTRATO.Set("VALOR", Convert.ToDecimal(dtFCHEQUE.Rows[0]["VALOR"]));
                    FEXTRATO.Set("HISTORICO", finLanExtratoPar.Historico);
                    FEXTRATO.Set("COMPENSADO", 0);

                    if (finLanExtratoPar.Tipo == Support.FinLanBaixaExtratoTipoEnum.Saida)
                    {
                        FEXTRATO.Set("TIPO", 4);
                    }

                    if (finLanExtratoPar.Tipo == Support.FinLanBaixaExtratoTipoEnum.Entrada)
                    {
                        FEXTRATO.Set("TIPO", 5);
                    }

                    FEXTRATO.Set("CODCCUSTO", finLanExtratoPar.codCCusto);
                    FEXTRATO.Set("CODNATUREZAORCAMENTO", finLanExtratoPar.codNaturezaOrcamento);
                    dbs.Commit();
                    if (FEXTRATO.Insert() > 0)
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FLANCA SET IDEXTRATO = ? WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { FEXTRATO.CampoValor[1].Valor, finLanExtratoPar.CodEmpresa, finLanExtratoPar.CodLanca[0].ToString() });
                    }
                    else
                    {
                        throw new Exception("Erro ao inserir registro de extrato do cheque.\r\nDetalhe técnico: método GerarExtratoCheque(obj).");
                    }
                }
            }
            else
            {
                try
                {
                    dbs.Commit();
                }
                catch (Exception)
                {

                }
            }

        }

        public int ProximoIDLOG(int CODEMPRESA, String CODTABELA)
        {
            String Consulta = @"
SELECT IDLOG
FROM GLOG
WHERE CODEMPRESA = ?
  AND CODTABELA = ?";

            System.Data.DataTable dt = dbs.QuerySelect(Consulta, new Object[] { CODEMPRESA, CODTABELA });

            if (dt.Rows.Count == 0)
            {
                String Comando = @"INSERT INTO GLOG (CODEMPRESA, CODTABELA, IDLOG) VALUES (?, ?, ?)";
                dbs.QueryExec(Comando, new Object[] { CODEMPRESA, CODTABELA, 1 });
                return 1;
            }
            else
            {
                int IDLOG = int.Parse(dt.Rows[0][0].ToString());
                IDLOG++;

                String Comando = @"UPDATE GLOG SET IDLOG = ? WHERE CODEMPRESA = ? AND CODTABELA = ?";
                dbs.QueryExec(Comando, new Object[] { IDLOG, CODEMPRESA, CODTABELA });
                return IDLOG;
            }
        }

        public override void ValidateRecord(List<PS.Lib.DataField> objArr)
        {
            base.ValidateRecord(objArr);

            if (VerificaSituacao(objArr) != 0)
            {
                throw new Exception("Lançamento não pode ser modificado.");
            }

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");
            PS.Lib.DataField dtCODTIPDOC = gb.RetornaDataFieldByCampo(objArr, "CODTIPDOC");
            PS.Lib.DataField dtTIPOPAGREC = gb.RetornaDataFieldByCampo(objArr, "TIPOPAGREC");
            PS.Lib.DataField dtNUMERO = gb.RetornaDataFieldByCampo(objArr, "NUMERO");
            PS.Lib.DataField dtCODFILIAL = gb.RetornaDataFieldByCampo(objArr, "CODFILIAL");
            PS.Lib.DataField dtCODCLIFOR = gb.RetornaDataFieldByCampo(objArr, "CODCLIFOR");
            PS.Lib.DataField dtDATAEMISSAO = gb.RetornaDataFieldByCampo(objArr, "DATAEMISSAO");
            PS.Lib.DataField dtVLLIQUIDO = gb.RetornaDataFieldByCampo(objArr, "VLORIGINAL");
            PS.Lib.DataField dtDATAVENCIMENTO = gb.RetornaDataFieldByCampo(objArr, "DATAVENCIMENTO");
            PS.Lib.DataField dtDATAPREVBAIXA = gb.RetornaDataFieldByCampo(objArr, "DATAPREVBAIXA");
            PS.Lib.DataField dtCODMOEDA = gb.RetornaDataFieldByCampo(objArr, "CODMOEDA");
            PS.Lib.DataField dtVLORIGINAL = gb.RetornaDataFieldByCampo(objArr, "VLORIGINAL");

            if (dtTIPOPAGREC.Valor == null)
            {
                throw new Exception(gb.MensagemDeValidacao(this._tablename, dtTIPOPAGREC.Field));
            }

            if (dtCODTIPDOC.Valor == null)
            {
                throw new Exception(gb.MensagemDeValidacao(this._tablename, dtCODTIPDOC.Field));
            }
            if (dtNUMERO.Valor == null)
            {
                System.Data.DataRow PARAMTIPDOC = gb.RetornaParametrosTipoDocumento(dtCODTIPDOC.Valor.ToString());

                if (int.Parse(PARAMTIPDOC["USANUMEROSEQ"].ToString()) == 0)
                {
                    throw new Exception(gb.MensagemDeValidacao(this._tablename, dtNUMERO.Field));
                }
            }
            else
            {
                System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();
                if (PARAMVAREJO == null)
                {
                    throw new Exception("Parametros do módulo não foram lozalicados.");
                }
                else
                {
                    int mask = (PARAMVAREJO["MASKNUMEROSEQ"] == DBNull.Value) ? 0 : Convert.ToInt32(PARAMVAREJO["MASKNUMEROSEQ"]);
                    if (mask == 0)
                    {
                        throw new Exception("Máscara do numero do documento não está parametrizada.");
                    }
                    else
                    {
                        if (dtNUMERO.Valor.ToString().Length > mask)
                        {
                            throw new Exception("Quantidade de caracteres do número do documento é maior que a quantidade permitida.");
                        }
                    }
                }
            }
            if (dtCODFILIAL.Valor == null)
            {
                throw new Exception(gb.MensagemDeValidacao(this._tablename, dtCODFILIAL.Field));
            }
            if (dtCODCLIFOR.Valor == null)
            {
                throw new Exception(gb.MensagemDeValidacao(this._tablename, dtCODCLIFOR.Field));
            }
            if (dtDATAEMISSAO.Valor == null)
            {
                throw new Exception(gb.MensagemDeValidacao(this._tablename, dtDATAEMISSAO.Field));
            }
            if (dtDATAVENCIMENTO.Valor == null)
            {
                throw new Exception(gb.MensagemDeValidacao(this._tablename, dtDATAVENCIMENTO.Field));
            }
            if ((DateTime)dtDATAPREVBAIXA.Valor < (DateTime)dtDATAEMISSAO.Valor)
            {
                throw new Exception("Data de previsão de baixa não pode ser menor que Data de Emissão.");
            }
            if ((DateTime)dtDATAVENCIMENTO.Valor < (DateTime)dtDATAEMISSAO.Valor)
            {
                throw new Exception("Data de Vencimento não pode ser menor que Data de Emissão.");
            }
            if (dtCODMOEDA.Valor == null)
            {
                throw new Exception(gb.MensagemDeValidacao(this._tablename, dtCODMOEDA.Field));
            }
            if (Convert.ToDecimal(dtVLORIGINAL.Valor) <= 0)
            {
                throw new Exception("Valor Original deve ser maior que Zero.");
            }
            if (Convert.ToInt32(dtTIPOPAGREC.Valor) == 1)
            {
                this.PossuiSaldoFinanceiro(Convert.ToInt32(dtCODEMPRESA.Valor), dtCODCLIFOR.Valor.ToString(), Convert.ToInt32(dtCODLANCA.Valor), Convert.ToDecimal(dtVLLIQUIDO.Valor));
            }
            if (Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT COUNT (*) FROM FLANCA WHERE CODCLIFOR = ? AND TIPOPAGREC = ? AND NUMERO = ? AND CODFILIAL = ? AND CODTIPDOC = ? AND CODEMPRESA = ?", new object[] { dtCODCLIFOR.Valor, dtTIPOPAGREC.Valor, dtNUMERO.Valor, dtCODFILIAL.Valor, dtCODTIPDOC.Valor, dtCODEMPRESA.Valor })) > 0)
            {
                throw new Exception("Lançamento já existe.");
            }
        }

        public override List<PS.Lib.DataField> InsertRecord(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dtNUMERO = gb.RetornaDataFieldByCampo(objArr, "NUMERO");
            PS.Lib.DataField dtTIPDOC = gb.RetornaDataFieldByCampo(objArr, "CODTIPDOC");

            if (dtNUMERO.Valor == null)
            {
                string novoNumStr = GeraNumeroDocumento(dtTIPDOC.Valor.ToString());
                if (string.IsNullOrEmpty(novoNumStr))
                {
                    throw new Exception("Erro ao gerar número do documento financeiro.");
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
            else
            {
                System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();
                if (PARAMVAREJO == null)
                {
                    throw new Exception("Não foi possível gerar o número do lançamento.");
                }
                //else
                //{
                //    string novoNumStr = string.Empty;
                //    int mask = (PARAMVAREJO["MASKNUMEROSEQ"] == DBNull.Value) ? 0 : Convert.ToInt32(PARAMVAREJO["MASKNUMEROSEQ"]);
                //    if (mask == 0)
                //    {
                //        throw new Exception("Não foi possível gerar o número do lançamento pois a máscara do numero do lançamento não está parametrizada.");
                //    }
                //    else
                //    {
                //        int tamanho = dtNUMERO.Valor.ToString().Length;
                //        novoNumStr = string.Concat(dtNUMERO.Valor.ToString().PadLeft(mask, '0'));

                //        if (novoNumStr.Length > mask)
                //        {
                //            throw new Exception("Quantidade de caracteres do número do documento é maior que a quantidade permitida.");
                //        }
                //        else
                //        {
                //            for (int i = 0; i < objArr.Count; i++)
                //            {
                //                if (objArr[i].Field == "NUMERO")
                //                {
                //                    objArr[i].Valor = novoNumStr;
                //                }
                //            }
                //        }
                //    }
                //}
            }

            PS.Lib.DataField dtCODUSUARIOCRIACAO = new Lib.DataField("CODUSUARIOCRIACAO", PS.Lib.Contexto.Session.CodUsuario);
            PS.Lib.DataField dtDATACRIACAO = new Lib.DataField("DATACRIACAO", dbs.GetServerDateTimeNow());
            objArr.Add(dtCODUSUARIOCRIACAO);
            objArr.Add(dtDATACRIACAO);

            List<PS.Lib.DataField> objArrDDL = objArr;

            List<PS.Lib.DataField> temp = base.InsertRecord(objArr);

            InsertRegistroComplementar(objArrDDL);

            return temp;
        }

        public override List<PS.Lib.DataField> EditRecord(List<PS.Lib.DataField> objArr)
        {
            List<PS.Lib.DataField> temp = base.EditRecord(objArr);
            return temp;
        }

        public override void DeleteRecord(List<PS.Lib.DataField> objArr)
        {
            base.ValidateKeyRecord(objArr);

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");
            List<PS.Lib.DataField> objArr1 = gb.RetornaDataFieldByDataRow(dbs.QuerySelect("SELECT NUMERO FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", dtCODEMPRESA.Valor, dtCODLANCA.Valor).Rows[0]);
            PS.Lib.DataField dtNUMERO = gb.RetornaDataFieldByCampo(objArr1, "NUMERO");

            if (VerificaSituacao(objArr) != 2)
            {
                throw new Exception("Lançamento [" + dtNUMERO.Valor + "] não pode ser excluído pois não está cancelado.");
            }

            if (PossuiOperacaoRelacionada(objArr))
            {
                throw new Exception("Lançamento [" + dtNUMERO.Valor + "] não pode ser excluído pois esta relacionado a uma operação.");
            }

            DeleteRecordOper(objArr);
        }

        public void DeleteRecordOper(List<PS.Lib.DataField> objArr)
        {
            base.ValidateKeyRecord(objArr);

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");
            List<PS.Lib.DataField> objArr1 = gb.RetornaDataFieldByDataRow(dbs.QuerySelect("SELECT NUMERO FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", dtCODEMPRESA.Valor, dtCODLANCA.Valor).Rows[0]);
            PS.Lib.DataField dtNUMERO = gb.RetornaDataFieldByCampo(objArr1, "NUMERO");

            if (VerificaSituacao(objArr) == 1)
            {
                throw new Exception("Lançamento [" + dtNUMERO.Valor + "] não pode ser excluído pois está baixado.");
            }

            if (PossuiRelacionamento(objArr))
            {
                throw new Exception("Lançamento [" + dtNUMERO.Valor + "] não pode ser excluído pois possui relacionamento com outro lançamento.");
            }
            else
            {
                ExcluiRelacionamento(objArr);
            }

            ExcluiRegistroComplementar(objArr);
            ExcluiRegistroRateioCC(objArr);
            ExcluiRegistroRateioDP(objArr);

            base.DeleteRecord(objArr);
        }

    }
}
