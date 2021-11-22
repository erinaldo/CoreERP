using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PS.Glb
{
    public class PSPartOperacaoItemData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Glb.Formula.Function function = new PS.Glb.Formula.Function();
        private PS.Lib.Interpretador interpreta = new PS.Lib.Interpretador();

        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODOPER,
NSEQITEM,
CODPRODUTO,
(SELECT CODIGOAUXILIAR FROM VPRODUTO WHERE CODEMPRESA = GOPERITEM.CODEMPRESA AND CODPRODUTO = GOPERITEM.CODPRODUTO) CódigoAuxiliar,
(SELECT NOME FROM VPRODUTO WHERE CODEMPRESA = GOPERITEM.CODEMPRESA AND CODPRODUTO = GOPERITEM.CODPRODUTO) CNOME,
QUANTIDADE,
VLUNITARIO,
VLDESCONTO,
PRDESCONTO,
VLTOTALITEM,
CODCFOP,
CODNATUREZA,
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
CODUNIDOPER,
OBSERVACAO,
INFCOMPL,
CODTABPRECO,
APLICACAOMATERIAL,
QUANTIDADE_SALDO,
QUANTIDADE_FATURADO,
UFIBPTAX,
NACIONALFEDERALIBPTAX,
IMPORTADOSFEDERALIBPTAX,
ESTADUALIBPTAX,
MUNICIPALIBPTAX,
CHAVEIBPTAX,
PRACRESCIMO,
VLACRESCIMO,
TIPODESCONTO,
VLUNITORIGINAL,
TOTALEDITADO,
RATEIODESPESA,
RATEIODESCONTO,
RATEIOFRETE,
RATEIOSEGURO,
DATAENTREGA
FROM GOPERITEM WHERE ";
        }

        public Boolean TemParametroDiminuiEstoque(int CODEMPRESA, String CODTIPOPER, String OrigemDestino)
        {
            String consulta1 = @"SELECT OPERESTOQUE FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?";
            String consulta2 = @"SELECT OPERESTOQUE2 OPERESTOQUE FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?";

            String consulta = "";

            if (OrigemDestino.Equals("O"))
            {
                consulta = consulta1;
            }

            if (OrigemDestino.Equals("D"))
            {
                consulta = consulta2;
            }

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(consulta, new Object[] { CODEMPRESA, CODTIPOPER });

            String OPERESTOQUE = "N";
            if (dt.Rows[0]["OPERESTOQUE"] != DBNull.Value)
            {
                OPERESTOQUE = dt.Rows[0]["OPERESTOQUE"].ToString();

                if (OPERESTOQUE.ToUpper().Equals("D"))
                {
                    return true;
                }
            }

            return false;
        }

        public Boolean TemSaldoParaItem(int CODEMPRESA, int CODFILIAL, String CODLOCAL, String CODPRODUTO, decimal QUANTIDADE)
        {
            String consulta = @"
SELECT *
FROM VSALDOESTOQUE
WHERE CODEMPRESA = ?
  AND CODFILIAL = ?
  AND CODLOCAL = ?
  AND CODPRODUTO = ?";

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(consulta, new Object[] { CODEMPRESA, CODFILIAL, CODLOCAL, CODPRODUTO });

            decimal SALDOFINAL = 0;

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["SALDOFINAL"] != DBNull.Value)
                    {
                        SALDOFINAL = Convert.ToDecimal(dt.Rows[0]["SALDOFINAL"]);
                    }
                }
            }

            if ((SALDOFINAL - QUANTIDADE) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CalculaOperacao(List<PS.Lib.DataField> objArr)
        {
            PSPartOperacaoData _psPartOperacaoData = new PSPartOperacaoData();
            _psPartOperacaoData._tablename = "GOPER";
            _psPartOperacaoData._keys = new string[] { "CODEMPRESA", "CODOPER" };
            _psPartOperacaoData.CalculaOperacao(objArr);
        }

        public bool QuantidadeMaiorZero(List<PS.Lib.DataField> objArr)
        {
            bool Flag = true;

            PS.Lib.DataField dfQUANTIDADE = gb.RetornaDataFieldByCampo(objArr, "QUANTIDADE");

            if (dfQUANTIDADE == null)
                Flag = false;

            if (dfQUANTIDADE.Valor == null)
                Flag = false;

            if (dfQUANTIDADE.Valor.ToString() == string.Empty)
                Flag = false;

            try
            {
                if (Convert.ToDecimal(dfQUANTIDADE.Valor.ToString()) <= 0)
                    Flag = false;
            }
            catch
            {
                Flag = false;
            }

            return Flag;
        }

        public int VerificaSituacao(List<PS.Lib.DataField> objArr)
        {
            PSPartOperacaoData psPartOperacaoData = new PSPartOperacaoData();
            psPartOperacaoData._tablename = "GOPER";
            psPartOperacaoData._keys = new string[] { "CODEMPRESA", "CODOPER" };
            return psPartOperacaoData.VerificaSituacao(objArr);
        }

        public bool PossuiNFEstadual(int CodEmpresa, int CodOper)
        {
            PSPartOperacaoData psPartOperacaoData = new PSPartOperacaoData();
            psPartOperacaoData._tablename = "GOPER";
            psPartOperacaoData._keys = new string[] { "CODEMPRESA", "CODOPER" };
            return psPartOperacaoData.PossuiNFEstadual(CodEmpresa, CodOper, false);
        }

        public void GeraTributo(List<PS.Lib.DataField> objArr)
        {
            string sSql = "";

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNSEQITEM = gb.RetornaDataFieldByCampo(objArr, "NSEQITEM");
            PS.Lib.DataField dfCODPRODUTO = gb.RetornaDataFieldByCampo(objArr, "CODPRODUTO");
            PS.Lib.DataField dfCODNATUREZA = gb.RetornaDataFieldByCampo(objArr, "CODNATUREZA");

            sSql = @"SELECT CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";

            PS.Lib.DataField dfCODTIPOPER = new PS.Lib.DataField("CODTIPOPER", dbs.QueryValue(null, sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor).ToString());

            int FRM_CODEMPRESA = 0;
            string FRM_CODFORMULA = string.Empty;

            if (dfCODTIPOPER.Valor == null)
            {
                throw new Exception("Tipo de Operação não identificada.");
            }
            else
            {
                DataRow PARAMNSTIPOPER = gb.RetornaParametrosOperacao(dfCODTIPOPER.Valor.ToString());

                if (PARAMNSTIPOPER == null)
                {
                    throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação selecionada.");
                }
                else
                {
                    if (int.Parse(PARAMNSTIPOPER["GERATRIBUTOS"].ToString()) == 1)
                    {
                        sSql = @"SELECT * FROM GTIPOPERTRIBUTO WHERE CODEMPRESA = ? AND CODTIPOPER = ?";
                        DataTable GTIPOPERTRIBUTO = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODTIPOPER.Valor);
                        if (GTIPOPERTRIBUTO.Rows.Count > 0)
                        {
                            PSPartOperItemTributoData psPartOperItemTributoData = new PSPartOperItemTributoData();
                            psPartOperItemTributoData._tablename = "GOPERITEMTRIBUTO";
                            psPartOperItemTributoData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM", "CODTRIBUTO" };

                            sSql = "DELETE FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ? AND EDITADO = 0";
                            dbs.QueryExec(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor, dfNSEQITEM.Valor);

                            for (int i = 0; i < GTIPOPERTRIBUTO.Rows.Count; i++)
                            {
                                sSql = @"SELECT EDITADO FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ? AND CODTRIBUTO = ?";
                                int Editado = Convert.ToInt32(dbs.QueryValue(0, sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor, dfNSEQITEM.Valor, GTIPOPERTRIBUTO.Rows[i]["CODTRIBUTO"]));

                                bool GerarTributo = true;
                                if (Editado == 1)
                                    GerarTributo = false;

                                if (GerarTributo)
                                {
                                    #region Base de Calculo

                                    PS.Lib.Contexto.Session.key1 = dfCODEMPRESA.Valor;
                                    PS.Lib.Contexto.Session.key2 = dfCODOPER.Valor;
                                    PS.Lib.Contexto.Session.key3 = dfNSEQITEM.Valor;

                                    FRM_CODEMPRESA = int.Parse(GTIPOPERTRIBUTO.Rows[i]["CODEMPRESA"].ToString());
                                    FRM_CODFORMULA = GTIPOPERTRIBUTO.Rows[i]["CODFORMULABASECALCULO"].ToString();

                                    if (string.IsNullOrEmpty(FRM_CODFORMULA))
                                    {
                                        throw new Exception("Fórmula não vinculada para o tributo " + GTIPOPERTRIBUTO.Rows[i]["CODTRIBUTO"]);
                                    }

                                    decimal TRB_BASE_CALCULO = 0;
                                    try
                                    {
                                        interpreta.comando = function.PreparaFormula(FRM_CODEMPRESA, FRM_CODFORMULA);
                                        TRB_BASE_CALCULO = Convert.ToDecimal(interpreta.Executar().ToString());

                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception("Erro ao executar a fórmula: " + FRM_CODFORMULA + ".\r\n" + ex.Message);
                                    }

                                    #endregion

                                    sSql = @"SELECT * FROM VTRIBUTO WHERE CODEMPRESA = ? AND CODTRIBUTO = ?";
                                    DataTable VTRIBUTO = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, GTIPOPERTRIBUTO.Rows[i]["CODTRIBUTO"]);

                                    #region Variavel
                                    decimal TRB_VALORICMSST = 0;
                                    decimal TRB_FATORMVA = 0;
                                    decimal TRB_BCORIGINAL = TRB_BASE_CALCULO;
                                    decimal TRB_REDUCAOBASEICMSST = 0;

                                    decimal TRB_ALIQUOTA = 0;
                                    decimal TRB_VALOR_ALIQUOTA = 0;
                                    string TRB_CST = string.Empty;
                                    int? TRB_MODBC = null;
                                    decimal TRB_REDBC = 0;
                                    string TRB_CENQ = string.Empty;
                                    #endregion

                                    decimal pdif = 0;
                                    if (VTRIBUTO.Rows.Count > 0)
                                    {
                                        #region Aliquota



                                        #region 0 - Tributo

                                        if (int.Parse(VTRIBUTO.Rows[0]["ALIQUOTAEM"].ToString()) == 0)
                                        {
                                            TRB_ALIQUOTA = Convert.ToDecimal(VTRIBUTO.Rows[0]["ALIQUOTA"].ToString());
                                        }

                                        #endregion

                                        #region 1 - Produto

                                        if (int.Parse(VTRIBUTO.Rows[0]["ALIQUOTAEM"].ToString()) == 1)
                                        {
                                            sSql = @"SELECT ALIQUOTA FROM VPRODUTOTRIBUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODTRIBUTO = ?";
                                            TRB_ALIQUOTA = Convert.ToDecimal(dbs.QueryValue(0, sSql, dfCODEMPRESA.Valor, dfCODPRODUTO.Valor, GTIPOPERTRIBUTO.Rows[i]["CODTRIBUTO"]).ToString());
                                        }

                                        #endregion

                                        #region 2 - Tipo Operação

                                        if (int.Parse(VTRIBUTO.Rows[0]["ALIQUOTAEM"].ToString()) == 2)
                                        {
                                            TRB_ALIQUOTA = Convert.ToDecimal(GTIPOPERTRIBUTO.Rows[i]["ALIQUOTA"].ToString());
                                        }

                                        #endregion

                                        #region 3 - Natureza

                                        if (int.Parse(VTRIBUTO.Rows[0]["ALIQUOTAEM"].ToString()) == 3)
                                        {
                                            TRB_ALIQUOTA = 0;

                                            if (dfCODNATUREZA.Valor == null)
                                                throw new Exception("CFOP não localizada no item da operação " + dfNSEQITEM.Valor + ".");

                                            if (VTRIBUTO.Rows[0]["CODTIPOTRIBUTO"] == DBNull.Value)
                                                throw new Exception("Tipo de tributo para o tributo " + GTIPOPERTRIBUTO.Rows[i]["CODTRIBUTO"].ToString() + " não informado.");

                                            if (VTRIBUTO.Rows[0]["CODTIPOTRIBUTO"].ToString() == "ICMS")
                                            {
                                                sSql = @"SELECT ALIQUOTA FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?";
                                                TRB_ALIQUOTA = Convert.ToDecimal(dbs.QueryValue(0, sSql, dfCODEMPRESA.Valor, dfCODNATUREZA.Valor));
                                            }
                                            else
                                            {
                                                sSql = @"SELECT ALIQUOTA FROM VNATUREZATRIBUTO, VNATUREZA WHERE VNATUREZATRIBUTO.CODEMPRESA = VNATUREZA.CODEMPRESA AND VNATUREZATRIBUTO.CODNATUREZA = VNATUREZA.CODNATUREZA AND VNATUREZATRIBUTO.CODEMPRESA = ? AND VNATUREZATRIBUTO.CODNATUREZA = ? AND VNATUREZATRIBUTO.CODTRIBUTO = ?";
                                                TRB_ALIQUOTA = Convert.ToDecimal(dbs.QueryValue(0, sSql, dfCODEMPRESA.Valor, dfCODNATUREZA.Valor, GTIPOPERTRIBUTO.Rows[i]["CODTRIBUTO"].ToString()));
                                            }
                                        }

                                        #endregion

                                        #region 4 - Estado

                                        if (int.Parse(VTRIBUTO.Rows[0]["ALIQUOTAEM"].ToString()) == 4)
                                        {
                                            sSql = @"SELECT CODCLIFOR FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";

                                            string CLI_CODCLIFOR = dbs.QueryValue(null, sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor).ToString();

                                            sSql = @"SELECT CODETD FROM VCLIFOR WHERE CODEMPRESA = ? AND CODCLIFOR = ?";

                                            string CLI_ESTADO = dbs.QueryValue(string.Empty, sSql, dfCODEMPRESA.Valor, CLI_CODCLIFOR).ToString();

                                            if (CLI_ESTADO != string.Empty)
                                            {
                                                sSql = @"SELECT ALIQUOTA FROM VPRODUTOTRIBUTOESTADO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODTRIBUTO = ? AND CODESTADO = ?";

                                                TRB_ALIQUOTA = Convert.ToDecimal(dbs.QueryValue("0", sSql, dfCODEMPRESA.Valor, dfCODPRODUTO.Valor, GTIPOPERTRIBUTO.Rows[i]["CODTRIBUTO"], CLI_ESTADO).ToString());
                                            }
                                            else
                                            {
                                                TRB_ALIQUOTA = 0;
                                            }
                                        }

                                        #endregion

                                        #region 5 - Regra
                                        if (int.Parse(VTRIBUTO.Rows[0]["ALIQUOTAEM"].ToString()) == 5)
                                        {
                                            TRB_ALIQUOTA = Convert.ToDecimal(dbs.QueryValue("0", @"
SELECT VREGRAVARCFOP.ALIQINTERNA
FROM GOPERITEM 
INNER JOIN GOPER ON GOPERITEM.CODOPER = GOPER.CODOPER AND GOPERITEM.CODEMPRESA = GOPER.CODEMPRESA
INNER JOIN VCLIFOR ON GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR AND GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA
INNER JOIN VPRODUTO ON GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO
INNER JOIN VREGRAVARCFOP ON VPRODUTO.CODNCM = VREGRAVARCFOP.NCM AND VPRODUTO.CODEMPRESA = VREGRAVARCFOP.CODEMPRESA
AND VCLIFOR.CODEMPRESA = VREGRAVARCFOP.CODEMPRESA AND VCLIFOR.CODETD = VREGRAVARCFOP.UFDESTINO
WHERE 
VPRODUTO.CODPRODUTO = ?
AND VCLIFOR.CODETD = (SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR AND VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA WHERE GOPER.CODOPER = ?)
AND GOPER.CODOPER = ?
AND GOPERITEM.NSEQITEM = ?
AND GOPER.CODEMPRESA = ?
", new object[] { dfCODPRODUTO.Valor, dfCODOPER.Valor, dfCODOPER.Valor, dfNSEQITEM.Valor, dfCODEMPRESA.Valor }));
                                        }
                                        #endregion

                                        #endregion

                                        #region Valor Aliquota

                                        bool utilizaST = Convert.ToBoolean(dbs.QueryValue(false, "SELECT VREGRAICMS.UTILIZAST FROM VREGRAICMS INNER JOIN VNATUREZA ON VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA WHERE VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dfCODNATUREZA.Valor }));

                                        
                                        if (TRB_BASE_CALCULO == 0 || TRB_ALIQUOTA == 0)
                                        {
                                            TRB_VALOR_ALIQUOTA = 0;
                                        }
                                        else
                                        {
                                            TRB_VALOR_ALIQUOTA = ((TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100);
                                        }

                                        if (utilizaST == false)
                                        {
                                            TRB_VALOR_ALIQUOTA = 0;
                                        }
                                        #endregion

                                        #region ICMS-ST
                                        decimal TRB_VICMSDIF = 0;
                                        // Verificação do ICMS - ST
                                     

                                        if ( utilizaST == true)
                                        {
                                            if (int.Parse(VTRIBUTO.Rows[0]["ALIQUOTAEM"].ToString()) == 5)
                                            {
                                                DataTable selecaoMVA = dbs.QuerySelect(@"SELECT REDUCAOBCST, SELECAOMVAST FROM 
VREGRAICMS 
INNER JOIN VNATUREZA ON VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA
WHERE 
VNATUREZA.CODNATUREZA = ?
AND VNATUREZA.CODEMPRESA = ?", new object[] { dfCODNATUREZA.Valor, dfCODEMPRESA.Valor });

                                                if (selecaoMVA.Rows.Count > 0)
                                                {
                                                    TRB_REDUCAOBASEICMSST = Convert.ToDecimal(selecaoMVA.Rows[0]["REDUCAOBCST"]);

                                                    DataTable dt = dbs.QuerySelect(@"SELECT MVAAJUSTADO, MVAORIGINAL, MVAAJUSTADOMATIMPORT
FROM GOPERITEM 
INNER JOIN GOPER ON GOPERITEM.CODOPER = GOPER.CODOPER AND GOPERITEM.CODEMPRESA = GOPER.CODEMPRESA
INNER JOIN VCLIFOR ON GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR AND GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA
INNER JOIN VPRODUTO ON GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO
INNER JOIN VREGRAVARCFOP ON VPRODUTO.CODNCM = VREGRAVARCFOP.NCM AND VPRODUTO.CODEMPRESA = VREGRAVARCFOP.CODEMPRESA
AND VCLIFOR.CODEMPRESA = VREGRAVARCFOP.CODEMPRESA AND VCLIFOR.CODETD = VREGRAVARCFOP.UFDESTINO
WHERE 
VPRODUTO.CODPRODUTO = ?
AND VCLIFOR.CODETD = (SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR AND VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA WHERE GOPER.CODOPER = ?)
AND GOPER.CODOPER = ?
AND GOPERITEM.NSEQITEM = ?
	AND GOPER.CODEMPRESA = ?", new object[] { dfCODPRODUTO.Valor, dfCODOPER.Valor, dfCODOPER.Valor, dfNSEQITEM.Valor, dfCODEMPRESA.Valor });

                                                    decimal icms = Convert.ToDecimal(dbs.QueryValue(0, "SELECT VALOR FROM GOPERITEMTRIBUTO WHERE CODOPER = ? AND CODTRIBUTO = ? AND CODEMPRESA = ? AND NSEQITEM = ?", new object[] { dfCODOPER.Valor, "ICMS", AppLib.Context.Empresa, dfNSEQITEM.Valor }));
                                                    switch (selecaoMVA.Rows[0]["SELECAOMVAST"].ToString())
                                                    {
                                                        case "O":

                                                            TRB_BASE_CALCULO = TRB_BASE_CALCULO + (TRB_BASE_CALCULO * (Convert.ToDecimal(dt.Rows[0]["MVAORIGINAL"]) / 100));
                                                            TRB_BASE_CALCULO = TRB_BASE_CALCULO * (1 - (Convert.ToDecimal(selecaoMVA.Rows[0]["REDUCAOBCST"]) / 100));
                                                            TRB_VALORICMSST = ((TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100) - icms;
                                                            TRB_VALOR_ALIQUOTA = (TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100;
                                                            TRB_FATORMVA = Convert.ToDecimal(dt.Rows[0]["MVAORIGINAL"]);
                                                            break;
                                                        case "A":
                                                            TRB_BASE_CALCULO = TRB_BASE_CALCULO + (TRB_BASE_CALCULO * (Convert.ToDecimal(dt.Rows[0]["MVAAJUSTADO"]) / 100));
                                                            TRB_BASE_CALCULO = TRB_BASE_CALCULO * (1 - (Convert.ToDecimal(selecaoMVA.Rows[0]["REDUCAOBCST"]) / 100));
                                                            TRB_VALORICMSST = ((TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100) - icms;
                                                            TRB_VALOR_ALIQUOTA = (TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100;
                                                            TRB_FATORMVA = Convert.ToDecimal(dt.Rows[0]["MVAAJUSTADO"]);
                                                            break;
                                                        case "I":
                                                            TRB_BASE_CALCULO = TRB_BASE_CALCULO + (TRB_BASE_CALCULO * (Convert.ToDecimal(dt.Rows[0]["MVAAJUSTADOMATIMPORT"]) / 100));
                                                            TRB_BASE_CALCULO = TRB_BASE_CALCULO * (1 - (Convert.ToDecimal(selecaoMVA.Rows[0]["REDUCAOBCST"]) / 100));
                                                            TRB_VALORICMSST = ((TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100) - icms;
                                                            TRB_VALOR_ALIQUOTA = (TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100;
                                                            TRB_FATORMVA = Convert.ToDecimal(dt.Rows[0]["MVAAJUSTADOMATIMPORT"]);
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                       
                                        #endregion

                                        #region DIF
                                        if (VTRIBUTO.Rows[0]["CODTIPOTRIBUTO"].ToString() == "ICMS")
                                        {
                                            // Buscar o dfPDIF
                                            #region Busca PDIF
                                            
                                                string tipoTributacao = dbs.QueryValue(string.Empty, @"SELECT VREGRAICMS.TIPOTRIBUTACAO 
FROM VREGRAICMS 
INNER JOIN VNATUREZA ON VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA
WHERE VNATUREZA.CODNATUREZA = ? AND VNATUREZA.CODEMPRESA = ?", new object[] { dfCODNATUREZA.Valor, dfCODEMPRESA.Valor }).ToString();
                                                if (tipoTributacao == "D")
                                                {
                                                    pdif  = Convert.ToDecimal(dbs.QueryValue(0, @"SELECT PDIF FROM VPRODUTOFISCAL 
WHERE 
CODETD = (SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR AND VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA WHERE CODOPER = ?)
AND CODEMPRESA = ?
AND CODPRODUTO = ?", new object[] { dfCODOPER.Valor, dfCODEMPRESA.Valor, dfCODPRODUTO.Valor }));
                                                    TRB_VICMSDIF = ((TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100) * (pdif / 100);
                                                    TRB_VALOR_ALIQUOTA = TRB_VALOR_ALIQUOTA - TRB_VICMSDIF;
                                                }

                                            #endregion

                                        }
                                        #endregion

                                        #region CST

                                        if (VTRIBUTO.Rows[0]["CODTIPOTRIBUTO"].ToString() == "ICMS")
                                        {
                                            sSql = @"SELECT CODCST FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?";
                                            TRB_CST = dbs.QueryValue(string.Empty, sSql, dfCODEMPRESA.Valor, dfCODNATUREZA.Valor).ToString();
                                        }
                                        else
                                        {
                                            if (VTRIBUTO.Rows[0]["CODTIPOTRIBUTO"].ToString() == "IPI")
                                            {
                                                if (PARAMNSTIPOPER["TIPENTSAI"].ToString() == "E")
                                                {
                                                    sSql = @"SELECT CODCSTENT FROM VREGRAIPI, VNATUREZA WHERE VREGRAIPI.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAIPI.IDREGRA = VNATUREZA.IDREGRAIPI AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?";
                                                    TRB_CST = dbs.QueryValue(string.Empty, sSql, dfCODEMPRESA.Valor, dfCODNATUREZA.Valor).ToString();
                                                }
                                                else
                                                {
                                                    sSql = @"SELECT CODCSTSAI FROM VREGRAIPI, VNATUREZA WHERE VREGRAIPI.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAIPI.IDREGRA = VNATUREZA.IDREGRAIPI AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?";
                                                    TRB_CST = dbs.QueryValue(string.Empty, sSql, dfCODEMPRESA.Valor, dfCODNATUREZA.Valor).ToString();
                                                }
                                            }
                                            else
                                            {
                                                sSql = @"SELECT CODCST FROM VNATUREZATRIBUTO, VNATUREZA WHERE VNATUREZATRIBUTO.CODEMPRESA = VNATUREZA.CODEMPRESA AND VNATUREZATRIBUTO.CODNATUREZA = VNATUREZA.CODNATUREZA AND VNATUREZATRIBUTO.CODEMPRESA = ? AND VNATUREZATRIBUTO.CODNATUREZA = ? AND VNATUREZATRIBUTO.CODTRIBUTO = ?";
                                                TRB_CST = dbs.QueryValue(string.Empty, sSql, dfCODEMPRESA.Valor, dfCODNATUREZA.Valor, GTIPOPERTRIBUTO.Rows[i]["CODTRIBUTO"].ToString()).ToString();

                                                if (string.IsNullOrEmpty(TRB_CST))
                                                {
                                                    sSql = @"SELECT CODCST FROM VPRODUTOTRIBUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODTRIBUTO = ?";
                                                    TRB_CST = dbs.QueryValue(0, sSql, dfCODEMPRESA.Valor, dfCODPRODUTO.Valor, GTIPOPERTRIBUTO.Rows[i]["CODTRIBUTO"]).ToString();
                                                }

                                                if (string.IsNullOrEmpty(TRB_CST))
                                                {
                                                    TRB_CST = GTIPOPERTRIBUTO.Rows[i]["CODCST"].ToString();
                                                }
                                            }
                                        }

                                        if (string.IsNullOrEmpty(TRB_CST))
                                        {
                                            throw new Exception(string.Concat("CST não encontrada para o Tributo: ", GTIPOPERTRIBUTO.Rows[i]["CODTRIBUTO"].ToString()));
                                        }

                                        #endregion

                                        #region Modalidade BC ICMS/ ICMS ST



                                        if (VTRIBUTO.Rows[0]["CODTIPOTRIBUTO"].ToString() == "ICMS" || VTRIBUTO.Rows[0]["CODTIPOTRIBUTO"].ToString() == "ICMS-ST")
                                        {
                                            string campo = string.Empty;
                                            if (VTRIBUTO.Rows[0]["CODTIPOTRIBUTO"].ToString() == "ICMS")
                                                campo = "MODALIDADEICMS";
                                            if (VTRIBUTO.Rows[0]["CODTIPOTRIBUTO"].ToString() == "ICMS-ST")
                                                campo = "MODALIDADEICMSST";
                                            sSql = "SELECT " + campo + @" FROM 
GOPER
INNER JOIN GOPERITEM ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER
INNER JOIN VCLIFOR ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR
INNER JOIN VPRODUTO ON GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO
INNER JOIN VREGRAVARCFOP ON VPRODUTO.CODEMPRESA = VREGRAVARCFOP.CODEMPRESA AND VPRODUTO.CODNCM = VREGRAVARCFOP.NCM AND VCLIFOR.CODETD = VREGRAVARCFOP.UFDESTINO
WHERE 
GOPER.CODOPER = ?
AND GOPER.CODEMPRESA = ?
AND VPRODUTO.CODPRODUTO = ?";
                                            TRB_MODBC = Convert.ToInt32(dbs.QueryValue(-1, sSql, dfCODOPER.Valor, dfCODEMPRESA.Valor, dfCODPRODUTO.Valor));
                                            if (TRB_MODBC < 0)
                                                TRB_MODBC = null;

                                            #region Difal
                                            sSql = @"SELECT 
VCLIFOR.CODETD UFDEST,
GFILIAL.CODETD UFREM, 
GESTADO.ALIQUOTAICMSINTERNADEST, 
VREGRAICMS.ALIQUOTA ALIQUOTAINTERESTADUAL, 
PERCICMSUFDEST, 
VREGRAICMS.PERCFCP, 
VREGRAICMS.DIFERENCIALALIQUOTA, 
VCLIFOR.CONTRIBICMS, 
GOPER.TIPOPERCONSFIN,
GOPER.CLIENTERETIRA,
VCLIFOR.PRODUTORRURAL
FROM GOPER 
INNER JOIN VCLIFOR ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR 
INNER JOIN GFILIAL ON GOPER.CODEMPRESA = GFILIAL.CODEMPRESA
INNER JOIN GESTADO ON VCLIFOR.CODETD = GESTADO.CODETD
INNER JOIN GOPERITEM ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER
INNER JOIN VNATUREZA ON GOPERITEM.CODEMPRESA = VNATUREZA.CODEMPRESA AND GOPERITEM.CODNATUREZA = VNATUREZA.CODNATUREZA
INNER JOIN VREGRAICMS ON VNATUREZA.CODEMPRESA = VREGRAICMS.CODEMPRESA AND VNATUREZA.IDREGRAICMS = VREGRAICMS.IDREGRA
WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?";
                                            DataTable dtDifal = dbs.QuerySelect(sSql, new object[] { dfCODOPER.Valor, dfCODEMPRESA.Valor });
                                            if (dtDifal.Rows.Count > 0)
                                            {
                                                AppLib.ORM.Jit reg = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GOPERITEMDIFAL");
                                                reg.Set("CODEMPRESA", dfCODEMPRESA.Valor);
                                                reg.Set("CODOPER", dfCODOPER.Valor);
                                                reg.Set("NSEQITEM", dfNSEQITEM.Valor);
                                                if (dtDifal.Rows[0]["DIFERENCIALALIQUOTA"].Equals(true))
                                                {
                                                    if (dtDifal.Rows[0]["CONTRIBICMS"].ToString() == "2" && dtDifal.Rows[0]["TIPOPERCONSFIN"].Equals(true))
                                                    {
                                                        if (dtDifal.Rows[0]["PRODUTORRURAL"].Equals(false) || dtDifal.Rows[0]["CLIENTERETIRA"].Equals(true))
                                                        {
                                                            decimal diferencial = Convert.ToDecimal(dtDifal.Rows[0]["ALIQUOTAICMSINTERNADEST"]) - Convert.ToDecimal(dtDifal.Rows[0]["ALIQUOTAINTERESTADUAL"]);
                                                            decimal difal = TRB_BASE_CALCULO * (diferencial / 100);
                                                            decimal partilhaDest = Convert.ToDecimal(dtDifal.Rows[0]["PERCICMSUFDEST"]);
                                                            partilhaDest = difal * (partilhaDest / 100);
                                                            decimal partilhaRem = difal - partilhaDest;
                                                            decimal fcp = TRB_BASE_CALCULO * (Convert.ToDecimal(dtDifal.Rows[0]["PERCFCP"]) / 100);
                                                            //Busca pra saber o que executar


                                                            reg.Set("VBCUFDEST", TRB_BASE_CALCULO);
                                                            reg.Set("PFCPUFDEST", dtDifal.Rows[0]["PERCFCP"]);
                                                            reg.Set("PICMSINTER", dtDifal.Rows[0]["ALIQUOTAINTERESTADUAL"]);
                                                            reg.Set("PICMSUFDEST", dtDifal.Rows[0]["ALIQUOTAICMSINTERNADEST"]);
                                                            reg.Set("PICMSINTERPART", dtDifal.Rows[0]["PERCICMSUFDEST"]);
                                                            reg.Set("VFCPUFDEST", fcp);
                                                            reg.Set("VICMSUFDEST", partilhaDest);
                                                            reg.Set("VICMSUFREMET", partilhaRem);
                                                            reg.Save();
                                                        }
                                                        else
                                                        {
                                                            reg.Delete();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        reg.Delete();
                                                    }
                                                }
                                                else
                                                {
                                                    reg.Delete();
                                                }

                                            }
                                            #endregion
                                        }
                                        #endregion

                                        #region Redução da BC


                                        if (VTRIBUTO.Rows[0]["CODTIPOTRIBUTO"].ToString() == "ICMS")
                                        {
                                            sSql = @"SELECT REDUCAOBASEICMS FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?";
                                            TRB_REDBC = Convert.ToDecimal(dbs.QueryValue(0, sSql, dfCODEMPRESA.Valor, dfCODNATUREZA.Valor));
                                        }
                                        if (VTRIBUTO.Rows[0]["CODTIPOTRIBUTO"].ToString() == "ICMS-ST")
                                        {
                                            TRB_REDBC = Convert.ToDecimal(dbs.QueryValue(0, @"SELECT REDUCAOBCST 
FROM 
VREGRAICMS 
INNER JOIN VNATUREZA ON VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA
WHERE	VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { dfCODEMPRESA.Valor, dfCODNATUREZA.Valor }));
                                        }

                                        #endregion

                                        #region Enquadramento IPI


                                        if (VTRIBUTO.Rows[0]["CODTIPOTRIBUTO"].ToString() == "IPI")
                                        {
                                            sSql = @"SELECT CENQ FROM VREGRAIPI, VNATUREZA WHERE VREGRAIPI.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAIPI.IDREGRA = VNATUREZA.IDREGRAIPI AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?";
                                            TRB_CENQ = dbs.QueryValue(string.Empty, sSql, dfCODEMPRESA.Valor, dfCODNATUREZA.Valor).ToString();
                                        }

                                        #endregion

                                        #region Gera Tributos

                                        #region Atributos

                                        PS.Lib.DataField TR_CODEMPRESA = new PS.Lib.DataField("CODEMPRESA", null);
                                        PS.Lib.DataField TR_CODOPER = new PS.Lib.DataField("CODOPER", null);
                                        PS.Lib.DataField TR_NSEQITEM = new PS.Lib.DataField("NSEQITEM", null);
                                        PS.Lib.DataField TR_CODTRIBUTO = new PS.Lib.DataField("CODTRIBUTO", null);
                                        PS.Lib.DataField TR_ALIQUOTA = new PS.Lib.DataField("ALIQUOTA", 0);
                                        PS.Lib.DataField TR_VALOR = new PS.Lib.DataField("VALOR", 0);
                                        PS.Lib.DataField TR_CODCST = new PS.Lib.DataField("CODCST", null);
                                        PS.Lib.DataField TR_BASECALCULO = new PS.Lib.DataField("BASECALCULO", 0);
                                        PS.Lib.DataField TR_MODALIDADEBC = new Lib.DataField("MODALIDADEBC", null);
                                        PS.Lib.DataField TR_REDUCAOBASEICMS = new Lib.DataField("REDUCAOBASEICMS", 0);
                                        PS.Lib.DataField TR_CENQ = new Lib.DataField("CENQ", null);
                                        PS.Lib.DataField TR_EDITADO = new Lib.DataField("EDITADO", 0);

                                        PS.Lib.DataField TR_VALORICMSST = new Lib.DataField("VALORICMSST", 0);
                                        PS.Lib.DataField TR_FATORMVA = new Lib.DataField("FATORMVA", 0);
                                        PS.Lib.DataField TR_BCORIGINAL = new Lib.DataField("BCORIGINAL", 0);
                                        PS.Lib.DataField TR_REDUCAOBASEICMSST = new Lib.DataField("REDUCAOBASEICMSST", 0);

                                        PS.Lib.DataField TR_VICMSDIF = new Lib.DataField("VICMSDIF", 0);
                                        PS.Lib.DataField TR_PDIF = new Lib.DataField("PDIF", 0);
                                        #endregion

                                        #region Sets

                                        TR_CODEMPRESA.Valor = dfCODEMPRESA.Valor;
                                        TR_CODOPER.Valor = dfCODOPER.Valor;
                                        TR_NSEQITEM.Valor = dfNSEQITEM.Valor;
                                        TR_CODTRIBUTO.Valor = GTIPOPERTRIBUTO.Rows[i]["CODTRIBUTO"];
                                        TR_ALIQUOTA.Valor = TRB_ALIQUOTA;
                                        TR_VALOR.Valor = TRB_VALOR_ALIQUOTA;
                                        TR_CODCST.Valor = TRB_CST;
                                        TR_BASECALCULO.Valor = TRB_BASE_CALCULO;
                                        TR_MODALIDADEBC.Valor = TRB_MODBC;
                                        TR_REDUCAOBASEICMS.Valor = TRB_REDBC;
                                        TR_CENQ.Valor = TRB_CENQ;

                                        TR_VALORICMSST.Valor = TRB_VALORICMSST;
                                        TR_FATORMVA.Valor = TRB_FATORMVA;
                                        TR_BCORIGINAL.Valor = TRB_BCORIGINAL;
                                        TR_REDUCAOBASEICMSST.Valor = TRB_REDUCAOBASEICMSST;

                                        TR_VICMSDIF.Valor = TRB_VICMSDIF;
                                        TR_PDIF.Valor = pdif;

                                        #endregion

                                        List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
                                        ListObjArr.Add(TR_CODEMPRESA);
                                        ListObjArr.Add(TR_CODOPER);
                                        ListObjArr.Add(TR_NSEQITEM);
                                        ListObjArr.Add(TR_CODTRIBUTO);
                                        ListObjArr.Add(TR_ALIQUOTA);
                                        ListObjArr.Add(TR_VALOR);
                                        ListObjArr.Add(TR_CODCST);
                                        ListObjArr.Add(TR_BASECALCULO);
                                        ListObjArr.Add(TR_MODALIDADEBC);
                                        ListObjArr.Add(TR_EDITADO);
                                        ListObjArr.Add(TR_REDUCAOBASEICMS);
                                        ListObjArr.Add(TR_CENQ);

                                        ListObjArr.Add(TR_VALORICMSST);
                                        ListObjArr.Add(TR_FATORMVA);
                                        ListObjArr.Add(TR_BCORIGINAL);
                                        ListObjArr.Add(TR_REDUCAOBASEICMSST);

                                        ListObjArr.Add(TR_VICMSDIF);
                                        ListObjArr.Add(TR_PDIF);


                                        psPartOperItemTributoData.InsertRecordItemOper(ListObjArr);

                                        #endregion
                                    }
                                }
                            }
                        }
                    }
                }
            }

            PS.Lib.Contexto.Session.key1 = null;
            PS.Lib.Contexto.Session.key2 = null;
            PS.Lib.Contexto.Session.key3 = null;
            PS.Lib.Contexto.Session.key4 = null;
            PS.Lib.Contexto.Session.key5 = null;
        }

        public void MovimentaEstoqueExclusao(List<PS.Lib.DataField> objArr)
        {
            string sSql = "";

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNSEQITEM = gb.RetornaDataFieldByCampo(objArr, "NSEQITEM");

            sSql = @"SELECT CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";

            PS.Lib.DataField dfCODTIPOPER = new PS.Lib.DataField("CODTIPOPER", dbs.QueryValue(null, sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor).ToString());

            if (dfCODTIPOPER.Valor == null)
            {
                throw new Exception("Tipo de Operação não identificada.");
            }
            else
            {
                DataRow PARAMNSTIPOPER = gb.RetornaParametrosOperacao(dfCODTIPOPER.Valor.ToString());

                if (PARAMNSTIPOPER == null)
                {
                    throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação selecionada.");
                }
                else
                {
                    if (PARAMNSTIPOPER["OPERESTOQUE"].ToString() != "N")
                    {
                        PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                        psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
                        psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };

                        sSql = @"SELECT GOPER.CODFILIAL, GOPER.CODLOCAL, GOPER.CODLOCALENTREGA, GOPERITEM.CODPRODUTO, GOPERITEM.QUANTIDADE, GOPERITEM.CODUNIDOPER
                                FROM GOPER, GOPERITEM 
                                WHERE GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER
                                AND GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ? AND GOPERITEM.NSEQITEM = ?";

                        DataTable GOPERITEM = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor, dfNSEQITEM.Valor);

                        if (GOPERITEM.Rows.Count > 0)
                        {
                            PS.Lib.DataField dfCODFILIAL = gb.RetornaDataFieldByDataRow(GOPERITEM.Rows[0], "CODFILIAL");
                            PS.Lib.DataField dfCODLOCAL = gb.RetornaDataFieldByDataRow(GOPERITEM.Rows[0], "CODLOCAL");
                            PS.Lib.DataField dfCODLOCALENTREGA = gb.RetornaDataFieldByDataRow(GOPERITEM.Rows[0], "CODLOCALENTREGA");
                            PS.Lib.DataField dfQUANTIDADE = gb.RetornaDataFieldByDataRow(GOPERITEM.Rows[0], "QUANTIDADE");
                            PS.Lib.DataField dfCODUNIDOPER = gb.RetornaDataFieldByDataRow(GOPERITEM.Rows[0], "CODUNIDOPER");
                            PS.Lib.DataField dfCODPRODUTO = gb.RetornaDataFieldByDataRow(GOPERITEM.Rows[0], "CODPRODUTO");

                            PSPartLocalEstoqueSaldoData.Tipo Estoque;
                            if (PARAMNSTIPOPER["OPERESTOQUE"].ToString() == "A")
                                Estoque = PSPartLocalEstoqueSaldoData.Tipo.Aumenta;
                            else
                                Estoque = PSPartLocalEstoqueSaldoData.Tipo.Diminui;

                            psPartLocalEstoqueSaldoData.MovimentaEstoque(Convert.ToInt32(dfCODEMPRESA.Valor),
                                Convert.ToInt32(dfCODFILIAL.Valor),
                                (dfCODLOCAL.Valor == null) ? string.Empty : dfCODLOCAL.Valor.ToString(),
                                (dfCODLOCALENTREGA.Valor == null) ? string.Empty : dfCODLOCALENTREGA.Valor.ToString(),
                                (dfCODPRODUTO.Valor == null) ? string.Empty : dfCODPRODUTO.Valor.ToString(),
                                Convert.ToDecimal(dfQUANTIDADE.Valor),
                                (dfCODUNIDOPER.Valor == null) ? string.Empty : dfCODUNIDOPER.Valor.ToString(),
                                Estoque);
                        }
                    }
                }
            }

        }

        public void InsertRegistroComplementar(List<PS.Lib.DataField> objArr)
        {
            PSPartOperItemComplData psPartOperItemComplData = new PSPartOperItemComplData();
            psPartOperItemComplData._tablename = "GOPERITEMCOMPL";
            psPartOperItemComplData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM" };

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNSEQITEM = gb.RetornaDataFieldByCampo(objArr, "NSEQITEM");

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
            ListObjArr.Add(dfCODEMPRESA);
            ListObjArr.Add(dfCODOPER);
            ListObjArr.Add(dfNSEQITEM);

            psPartOperItemComplData.SaveRecord(ListObjArr);
        }

        public void ExcluiRegistroComplementar(List<PS.Lib.DataField> objArr)
        {
            PSPartOperItemComplData psPartOperItemComplData = new PSPartOperItemComplData();
            psPartOperItemComplData._tablename = "GOPERITEMCOMPL";
            psPartOperItemComplData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM" };

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNSEQITEM = gb.RetornaDataFieldByCampo(objArr, "NSEQITEM");

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
            ListObjArr.Add(dfCODEMPRESA);
            ListObjArr.Add(dfCODOPER);
            ListObjArr.Add(dfNSEQITEM);

            psPartOperItemComplData.DeleteRecord(ListObjArr);
        }

        public void ExcluiRegistroComplementarOper(List<PS.Lib.DataField> objArr)
        {
            PSPartOperItemComplData psPartOperItemComplData = new PSPartOperItemComplData();
            psPartOperItemComplData._tablename = "GOPERITEMCOMPL";
            psPartOperItemComplData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM" };

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNSEQITEM = gb.RetornaDataFieldByCampo(objArr, "NSEQITEM");

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
            ListObjArr.Add(dfCODEMPRESA);
            ListObjArr.Add(dfCODOPER);
            ListObjArr.Add(dfNSEQITEM);

            psPartOperItemComplData.DeleteRecordOper(ListObjArr);
        }

        public void ExcluiTributo(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNSEQITEM = gb.RetornaDataFieldByCampo(objArr, "NSEQITEM");

            sSql = @"SELECT CODTRIBUTO FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";
            DataTable dtTributo = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor, dfNSEQITEM.Valor);
            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
            if (dtTributo.Rows.Count > 0)
            {
                PSPartOperItemTributoData psPartOperItemTributoData = new PSPartOperItemTributoData();
                psPartOperItemTributoData._tablename = "GOPERITEMTRIBUTO";
                psPartOperItemTributoData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM", "CODTRIBUTO" };

                for (int i = 0; i < dtTributo.Rows.Count; i++)
                {
                    PS.Lib.DataField dfCODTRIBUTO = new PS.Lib.DataField("CODTRIBUTO", dtTributo.Rows[i]["CODTRIBUTO"]);

                    ListObjArr.Add(dfCODEMPRESA);
                    ListObjArr.Add(dfCODOPER);
                    ListObjArr.Add(dfNSEQITEM);
                    ListObjArr.Add(dfCODTRIBUTO);

                    psPartOperItemTributoData.DeleteRecordItemOper(ListObjArr);

                    ListObjArr.Clear();
                }
            }
        }
        private void ExcluiDifal(int codEmpresa, int codOper, int nseqItem)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();
            try
            {
                AppLib.ORM.Jit GOPERITEMDIFAL = new AppLib.ORM.Jit(conn, "GOPERITEMDIFAL");
                GOPERITEMDIFAL.Set("CODEMPRESA", codEmpresa);
                GOPERITEMDIFAL.Set("CODOPER", codOper);
                GOPERITEMDIFAL.Set("NSEQITEM", nseqItem);
                GOPERITEMDIFAL.Delete();
                conn.Commit();
            }
            catch (Exception)
            {
                conn.Rollback();
            }
        }

        public void ExcluiTributoOper(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNSEQITEM = gb.RetornaDataFieldByCampo(objArr, "NSEQITEM");

            sSql = @"SELECT CODTRIBUTO FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";
            DataTable dtTributo = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor, dfNSEQITEM.Valor);
            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
            if (dtTributo.Rows.Count > 0)
            {
                PSPartOperItemTributoData psPartOperItemTributoData = new PSPartOperItemTributoData();
                psPartOperItemTributoData._tablename = "GOPERITEMTRIBUTO";
                psPartOperItemTributoData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM", "CODTRIBUTO" };

                for (int i = 0; i < dtTributo.Rows.Count; i++)
                {
                    PS.Lib.DataField dfCODTRIBUTO = new PS.Lib.DataField("CODTRIBUTO", dtTributo.Rows[i]["CODTRIBUTO"]);

                    ListObjArr.Add(dfCODEMPRESA);
                    ListObjArr.Add(dfCODOPER);
                    ListObjArr.Add(dfNSEQITEM);
                    ListObjArr.Add(dfCODTRIBUTO);

                    psPartOperItemTributoData.DeleteRecordOper(ListObjArr);

                    ListObjArr.Clear();
                }
            }
        }

        public void ExcluiRateioCentroCusto(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNSEQITEM = gb.RetornaDataFieldByCampo(objArr, "NSEQITEM");

            sSql = @"SELECT CODCCUSTO FROM GOPERITEMRATEIOCC WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";

            DataTable dtRateio = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor, dfNSEQITEM.Valor);

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();

            if (dtRateio.Rows.Count > 0)
            {
                PSPartOperItemRateioCCData psPartOperItemRateioCCData = new PSPartOperItemRateioCCData();
                psPartOperItemRateioCCData._tablename = "GOPERITEMRATEIOCC";
                psPartOperItemRateioCCData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM", "CODCCUSTO" };

                for (int i = 0; i < dtRateio.Rows.Count; i++)
                {
                    PS.Lib.DataField dfCODCCUSTO = new PS.Lib.DataField("CODCCUSTO", dtRateio.Rows[i]["CODCCUSTO"]);

                    ListObjArr.Add(dfCODEMPRESA);
                    ListObjArr.Add(dfCODOPER);
                    ListObjArr.Add(dfNSEQITEM);
                    ListObjArr.Add(dfCODCCUSTO);

                    psPartOperItemRateioCCData.DeleteRecord(ListObjArr);

                    ListObjArr.Clear();
                }
            }
        }

        public void ExcluiRateioCentroCustoOper(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNSEQITEM = gb.RetornaDataFieldByCampo(objArr, "NSEQITEM");

            sSql = @"SELECT CODCCUSTO FROM GOPERITEMRATEIOCC WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";

            DataTable dtRateio = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor, dfNSEQITEM.Valor);

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();

            if (dtRateio.Rows.Count > 0)
            {
                PSPartOperItemRateioCCData psPartOperItemRateioCCData = new PSPartOperItemRateioCCData();
                psPartOperItemRateioCCData._tablename = "GOPERITEMRATEIOCC";
                psPartOperItemRateioCCData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM", "CODCCUSTO" };

                for (int i = 0; i < dtRateio.Rows.Count; i++)
                {
                    PS.Lib.DataField dfCODCCUSTO = new PS.Lib.DataField("CODCCUSTO", dtRateio.Rows[i]["CODCCUSTO"]);

                    ListObjArr.Add(dfCODEMPRESA);
                    ListObjArr.Add(dfCODOPER);
                    ListObjArr.Add(dfNSEQITEM);
                    ListObjArr.Add(dfCODCCUSTO);

                    psPartOperItemRateioCCData.DeleteRecordOper(ListObjArr);

                    ListObjArr.Clear();
                }
            }
        }

        public void ExcluiRateioDepartamento(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNSEQITEM = gb.RetornaDataFieldByCampo(objArr, "NSEQITEM");

            sSql = @"SELECT CODFILIAL, CODDEPTO FROM GOPERITEMRATEIODP WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";

            DataTable dtRateio = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor, dfNSEQITEM.Valor);

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();

            if (dtRateio.Rows.Count > 0)
            {
                PSPartOperItemRateioDPData psPartOperItemRateioDPData = new PSPartOperItemRateioDPData();
                psPartOperItemRateioDPData._tablename = "GOPERITEMRATEIODP";
                psPartOperItemRateioDPData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM", "CODFILIAL", "CODDEPTO" };

                for (int i = 0; i < dtRateio.Rows.Count; i++)
                {
                    PS.Lib.DataField dfCODDEPTO = new PS.Lib.DataField("CODDEPTO", dtRateio.Rows[i]["CODDEPTO"]);
                    PS.Lib.DataField dfCODFILIAL = new PS.Lib.DataField("CODFILIAL", dtRateio.Rows[i]["CODFILIAL"]);

                    ListObjArr.Add(dfCODEMPRESA);
                    ListObjArr.Add(dfCODOPER);
                    ListObjArr.Add(dfNSEQITEM);
                    ListObjArr.Add(dfCODDEPTO);
                    ListObjArr.Add(dfCODFILIAL);

                    psPartOperItemRateioDPData.DeleteRecord(ListObjArr);

                    ListObjArr.Clear();
                }
            }
        }

        public void ExcluiRateioDepartamentoOper(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNSEQITEM = gb.RetornaDataFieldByCampo(objArr, "NSEQITEM");

            sSql = @"SELECT CODFILIAL, CODDEPTO FROM GOPERITEMRATEIODP WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";

            DataTable dtRateio = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor, dfNSEQITEM.Valor);

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();

            if (dtRateio.Rows.Count > 0)
            {
                PSPartOperItemRateioDPData psPartOperItemRateioDPData = new PSPartOperItemRateioDPData();
                psPartOperItemRateioDPData._tablename = "GOPERITEMRATEIODP";
                psPartOperItemRateioDPData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM", "CODFILIAL", "CODDEPTO" };

                for (int i = 0; i < dtRateio.Rows.Count; i++)
                {
                    PS.Lib.DataField dfCODDEPTO = new PS.Lib.DataField("CODDEPTO", dtRateio.Rows[i]["CODDEPTO"]);
                    PS.Lib.DataField dfCODFILIAL = new PS.Lib.DataField("CODFILIAL", dtRateio.Rows[i]["CODFILIAL"]);

                    ListObjArr.Add(dfCODEMPRESA);
                    ListObjArr.Add(dfCODOPER);
                    ListObjArr.Add(dfNSEQITEM);
                    ListObjArr.Add(dfCODDEPTO);
                    ListObjArr.Add(dfCODFILIAL);

                    psPartOperItemRateioDPData.DeleteRecordOper(ListObjArr);

                    ListObjArr.Clear();
                }
            }
        }

        public void ExcluiRecurso(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNSEQITEM = gb.RetornaDataFieldByCampo(objArr, "NSEQITEM");

            sSql = @"SELECT CODOPERADOR FROM GOPERITEMRECURSO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";

            DataTable dtRecurso = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor, dfNSEQITEM.Valor);

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();

            if (dtRecurso.Rows.Count > 0)
            {
                PSPartOperacaoItemRecursoData psPartOperacaoItemRecursoData = new PSPartOperacaoItemRecursoData();
                psPartOperacaoItemRecursoData._tablename = "GOPERITEMRECURSO";
                psPartOperacaoItemRecursoData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM", "CODOPERADOR" };

                for (int i = 0; i < dtRecurso.Rows.Count; i++)
                {
                    PS.Lib.DataField dfCODOPERADOR = new PS.Lib.DataField("CODOPERADOR", dtRecurso.Rows[i]["CODOPERADOR"]);

                    ListObjArr.Add(dfCODEMPRESA);
                    ListObjArr.Add(dfCODOPER);
                    ListObjArr.Add(dfNSEQITEM);
                    ListObjArr.Add(dfCODOPERADOR);

                    psPartOperacaoItemRecursoData.DeleteRecord(ListObjArr);

                    ListObjArr.Clear();
                }
            }
        }

        public void ExcluiRecursoOper(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNSEQITEM = gb.RetornaDataFieldByCampo(objArr, "NSEQITEM");

            sSql = @"SELECT CODOPERADOR FROM GOPERITEMRECURSO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";

            DataTable dtRecurso = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor, dfNSEQITEM.Valor);

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();

            if (dtRecurso.Rows.Count > 0)
            {
                PSPartOperacaoItemRecursoData psPartOperacaoItemRecursoData = new PSPartOperacaoItemRecursoData();
                psPartOperacaoItemRecursoData._tablename = "GOPERITEMRECURSO";
                psPartOperacaoItemRecursoData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM", "CODOPERADOR" };

                for (int i = 0; i < dtRecurso.Rows.Count; i++)
                {
                    PS.Lib.DataField dfCODOPERADOR = new PS.Lib.DataField("CODOPERADOR", dtRecurso.Rows[i]["CODOPERADOR"]);

                    ListObjArr.Add(dfCODEMPRESA);
                    ListObjArr.Add(dfCODOPER);
                    ListObjArr.Add(dfNSEQITEM);
                    ListObjArr.Add(dfCODOPERADOR);

                    psPartOperacaoItemRecursoData.DeleteRecordOper(ListObjArr);

                    ListObjArr.Clear();
                }
            }
        }

        public override void ValidateRecord(List<PS.Lib.DataField> objArr)
        {
            base.ValidateRecord(objArr);

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dtCODPRODUTO = gb.RetornaDataFieldByCampo(objArr, "CODPRODUTO");
            PS.Lib.DataField dtQUANTIDADE = gb.RetornaDataFieldByCampo(objArr, "QUANTIDADE");
            PS.Lib.DataField dtCODUNIDOPER = gb.RetornaDataFieldByCampo(objArr, "CODUNIDOPER");

            string sSqlCABECALHO = @"SELECT * FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";
            // System.Data.DataTable dtCABECALHO = AppLib.Context.poolConnection.Get("Start").ExecQuery(sSqlCABECALHO, new Object[] { Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODOPER.Valor) });
            System.Data.DataTable dtCABECALHO = dbs.QuerySelect(sSqlCABECALHO, new Object[] { Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODOPER.Valor) });

            String CODTIPOPER = dtCABECALHO.Rows[0]["CODTIPOPER"].ToString();

            int CODFILIAL = Convert.ToInt32(dtCABECALHO.Rows[0]["CODFILIAL"]);

            int CODFILIALENTREGA = 0;
            if (dtCABECALHO.Rows[0]["CODFILIALENTREGA"] != null)
            {
                CODFILIALENTREGA = string.IsNullOrEmpty(dtCABECALHO.Rows[0]["CODFILIALENTREGA"].ToString()) ? 0 : Convert.ToInt32(dtCABECALHO.Rows[0]["CODFILIALENTREGA"]);
            }

            String CODLOCAL = String.Empty;
            if (dtCABECALHO.Rows[0]["CODLOCAL"] != DBNull.Value)
            {
                CODLOCAL = dtCABECALHO.Rows[0]["CODLOCAL"].ToString();
            }

            String CODLOCALENTREGA = String.Empty;
            if (dtCABECALHO.Rows[0]["CODLOCALENTREGA"] != DBNull.Value)
            {
                CODLOCALENTREGA = dtCABECALHO.Rows[0]["CODLOCALENTREGA"].ToString();
            }



            int retorno = VerificaSituacao(objArr);
            if (retorno != 0 && retorno != 5)
            {
                throw new Exception("Operação não pode ser modificada.");
            }

            if (PossuiNFEstadual(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODOPER.Valor)))
            {
                throw new Exception("Operação não pode ser modificada pois esta vinculada a uma NF-e.");
            }

            if (dtCODPRODUTO.Valor == null)
            {
                throw new Exception(gb.MensagemDeValidacao(_tablename, dtCODPRODUTO.Field));
            }

            if (dtCODUNIDOPER.Valor == null)
            {
                throw new Exception(gb.MensagemDeValidacao(_tablename, dtCODUNIDOPER.Field));
            }

            if (!QuantidadeMaiorZero(objArr))
            {
                throw new Exception("Quantidade informada deve ser maior que zero.");
            }

            #region VALIDA SALDO ESTOQUE

            String CONTROLASALDOESTQUE = AppLib.Context.poolConnection.Get("Start").ExecGetField("B", "SELECT CONTROLASALDOESTQUE FROM VPARAMETROS WHERE CODEMPRESA = ?", new Object[] { AppLib.Context.Empresa }).ToString();

            if (CODLOCAL != String.Empty)
            {
                if (TemParametroDiminuiEstoque(Convert.ToInt32(dfCODEMPRESA.Valor), CODTIPOPER, "O"))
                {
                    if (!TemSaldoParaItem(Convert.ToInt32(dfCODEMPRESA.Valor), CODFILIAL, CODLOCAL, dtCODPRODUTO.Valor.ToString(), Convert.ToDecimal(dtQUANTIDADE.Valor)))
                    {
                        if (CONTROLASALDOESTQUE.ToUpper().Equals("A"))
                        {
                            AppLib.Windows.FormMessageDefault.ShowInfo("Não existe saldo no estoque " + CODLOCAL + " para atender este item.");
                        }

                        if (CONTROLASALDOESTQUE.ToUpper().Equals("B"))
                        {
                            throw new Exception("Este item não será salvo, pois não existe saldo no estoque " + CODLOCAL + ".");
                        }
                    }
                }
            }

            if (CODLOCALENTREGA != String.Empty)
            {
                if (TemParametroDiminuiEstoque(Convert.ToInt32(dfCODEMPRESA.Valor), CODTIPOPER, "D"))
                {
                    if (!TemSaldoParaItem(Convert.ToInt32(dfCODEMPRESA.Valor), CODFILIALENTREGA, CODLOCALENTREGA, dtCODPRODUTO.Valor.ToString(), Convert.ToDecimal(dtQUANTIDADE.Valor)))
                    {
                        if (CONTROLASALDOESTQUE.ToUpper().Equals("A"))
                        {
                            AppLib.Windows.FormMessageDefault.ShowInfo("Não existe saldo no estoque " + CODLOCALENTREGA + " para atender este item.");
                        }

                        if (CONTROLASALDOESTQUE.ToUpper().Equals("B"))
                        {
                            throw new Exception("Este item não será salvo, pios não existe saldo no estoque " + CODLOCALENTREGA + ".");
                        }
                    }
                }
            }

            #endregion

            #region Formula de Validação

            String sSql = @"SELECT CODFORMULAVALIDAOPERITEM FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?";
            System.Data.DataTable PARAMETROS = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, CODTIPOPER);

            if (PARAMETROS.Rows.Count > 0)
            {
                if (PARAMETROS.Rows[0]["CODFORMULAVALIDAOPERITEM"].ToString() != string.Empty)
                {
                    PS.Lib.Contexto.Session.Current = objArr;

                    int FRM_CODEMPRESA = int.Parse(dfCODEMPRESA.Valor.ToString());
                    string FRM_CODFORMULA = PARAMETROS.Rows[0]["CODFORMULAVALIDAOPERITEM"].ToString();

                    bool ERRO = false;
                    interpreta.comando = function.PreparaFormula(FRM_CODEMPRESA, FRM_CODFORMULA);
                    ERRO = Convert.ToBoolean(interpreta.Executar());

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
            }

            #endregion
        }

        public override List<PS.Lib.DataField> InsertRecord(List<PS.Lib.DataField> objArr)
        {
            Boolean existeSaldo = false;

            for (int i = 0; i < objArr.Count; i++)
            {
                if (objArr[i].Field.ToUpper().Equals("QUANTIDADE_SALDO"))
                {
                    existeSaldo = true;
                }
            }

            if (!existeSaldo)
            {
                for (int i = 0; i < objArr.Count; i++)
                {
                    if (objArr[i].Field.ToUpper().Equals("QUANTIDADE"))
                    {
                        Object valor = objArr[i].Valor;
                        objArr.Add(new Lib.DataField("QUANTIDADE_SALDO", valor));
                    }
                }
            }

            List<PS.Lib.DataField> temp = base.InsertRecord(objArr);

            this.InsertRegistroComplementar(objArr);

            this.GeraTributo(objArr);

            //this.CalculaOperacao(objArr);

            int CODEMPRESA = Convert.ToInt32(gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA").Valor);
            int CODOPER = Convert.ToInt32(gb.RetornaDataFieldByCampo(objArr, "CODOPER").Valor);
            int NSEQITEM = Convert.ToInt32(gb.RetornaDataFieldByCampo(objArr, "NSEQITEM").Valor);

            PSPartLocalEstoqueSaldoData saldoEstoque = new PSPartLocalEstoqueSaldoData();
            //saldoEstoque.MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao, CODEMPRESA, CODOPER, NSEQITEM);

            return temp;
        }

        public override List<PS.Lib.DataField> EditRecord(List<PS.Lib.DataField> objArr)
        {
            int CODEMPRESA = Convert.ToInt32(gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA").Valor);
            int CODOPER = Convert.ToInt32(gb.RetornaDataFieldByCampo(objArr, "CODOPER").Valor);
            int NSEQITEM = Convert.ToInt32(gb.RetornaDataFieldByCampo(objArr, "NSEQITEM").Valor);
            PSPartLocalEstoqueSaldoData saldoEstoque = new PSPartLocalEstoqueSaldoData();
            //saldoEstoque.MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemExclusao, CODEMPRESA, CODOPER, NSEQITEM);

            List<PS.Lib.DataField> temp = base.EditRecord(objArr);

            this.GeraTributo(objArr);

           // this.CalculaOperacao(objArr);

            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfCODNATUREZA = gb.RetornaDataFieldByCampo(objArr, "CODNATUREZA");

            //verificaRegraICMS(dfCODNATUREZA.Valor.ToString(), dfCODOPER.Valor.ToString());

            //saldoEstoque.MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao, CODEMPRESA, CODOPER, NSEQITEM);

            return temp;
        }

        #region verifica Difal
        /// <summary>
        /// Verifica se existe itens do difal
        /// </summary>
        /// <param name="codoper">Cod. Oper</param>
        /// <returns>true S / false N</returns>
        private bool verificaDifal(string codoper)
        {
            int retorno = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT COUNT(CODOPER) FROM GOPERITEMDIFAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codoper }));
            if (retorno > 0)
            {
                return true;
            }
            return false;
        }
        #endregion
        private void verificaRegraICMS(string codNatureza, string codOper)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT VREGRAICMS.DESCRICAO, VREGRAICMS.PERCICMSUFDEST, PERCFCP FROM VNATUREZA INNER JOIN VREGRAICMS ON VNATUREZA.CODEMPRESA = VREGRAICMS.CODEMPRESA AND VNATUREZA.IDREGRAICMS = VREGRAICMS.IDREGRA WHERE VNATUREZA.CODNATUREZA = ? AND VNATUREZA.CODEMPRESA = ?", new object[] { codNatureza, AppLib.Context.Empresa });
            if (dt.Rows.Count > 0)
            {
                if (verificaDifal(codOper))
                {
                    //update
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GOPERITEMDIFAL SET PFCPUFDEST = ?, PICMSUFDEST = ?, PICMSINTER = ?, PICMSINTERPART = ?, VFCPUFDEST = ?, VICMSUFDEST = ?, VICMSUFREMET = ? WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?", new object[] { });
                }
                else
                {
                    //insert
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GOPERITEMDIFAL (CODEMPRESA, CODOPER, NSEQITEM, VBCUFDEST, PFCPUFDEST, PICMSUFDEST, PICMSINTER, PICMSINTERPART, VFCPUFDEST, VICMSUFDEST, VICMSUFREMET) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { });
                }
            }
        }

        public override void DeleteRecord(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dtNSEQITEM = gb.RetornaDataFieldByCampo(objArr, "NSEQITEM");

            if (this.VerificaSituacao(objArr) != 0)
            {
                throw new Exception("Operação não pode ser modificada.");
            }

            if (PossuiNFEstadual(Convert.ToInt32(dtCODEMPRESA.Valor), Convert.ToInt32(dtCODOPER.Valor)))
            {
                throw new Exception("Operação não pode ser modificada pois esta vinculada a uma NF-e.");
            }

            int CODEMPRESA = Convert.ToInt32(gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA").Valor);
            int CODOPER = Convert.ToInt32(gb.RetornaDataFieldByCampo(objArr, "CODOPER").Valor);
            int NSEQITEM = Convert.ToInt32(gb.RetornaDataFieldByCampo(objArr, "NSEQITEM").Valor);
            PSPartLocalEstoqueSaldoData saldoEstoque = new PSPartLocalEstoqueSaldoData();
            //saldoEstoque.MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemExclusao, CODEMPRESA, CODOPER, NSEQITEM);

            this.ExcluiRegistroComplementar(objArr);
            this.ExcluiRecurso(objArr);
            this.ExcluiTributo(objArr);
            this.ExcluiRateioCentroCusto(objArr);
            this.ExcluiRateioDepartamento(objArr);
            ExcluiDifal(Convert.ToInt32(dtCODEMPRESA.Valor), Convert.ToInt32(dtCODOPER.Valor), Convert.ToInt32(dtNSEQITEM.Valor));
            base.DeleteRecord(objArr);

            this.CalculaOperacao(objArr);
        }

        public void DeleteRecordOper(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dtNSEQITEM = gb.RetornaDataFieldByCampo(objArr, "NSEQITEM");
            if (VerificaSituacao(objArr) == 2)
            {
                this.ExcluiRegistroComplementarOper(objArr);
                this.ExcluiRecursoOper(objArr);
                this.ExcluiTributoOper(objArr);
                this.ExcluiRateioCentroCustoOper(objArr);
                this.ExcluiRateioDepartamentoOper(objArr);
                ExcluiDifal(Convert.ToInt32(dtCODEMPRESA.Valor), Convert.ToInt32(dtCODOPER.Valor), Convert.ToInt32(dtNSEQITEM.Valor));
                base.DeleteRecord(objArr);
            }
        }

    }
}
