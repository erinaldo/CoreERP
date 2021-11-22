using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace PS.Glb.Class
{
    public static class GerarNota4_0
    {
        public static string Gerar(string CODOPER)
        {
            string sbFinal = "";

            //Instancia a Classe que formata
            UtilidadesGerarNF nf_formata = new UtilidadesGerarNF();

            //DATATABLE USADOS EM LOOP
            DataTable dtgOperItemTagProd = null;

            NFeAPI nfe_api = new NFeAPI();

            StringBuilder sb = new StringBuilder();

            string CodProduto;

            //RECUPERA O CODCLIFOR
            string codigoCliFor = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "select CODCLIFOR from GOPER WHERE CODEMPRESA=? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CODOPER }).ToString();

            //RECUPERA FILIAL DA GOPER
            string filial = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODFILIAL FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CODOPER }).ToString();


            //RECUPER CODTIPOPER
            string CODTIPOPER = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "select CODTIPOPER from goper where CODEMPRESA = ? and CODFILIAL=? and CODOPER = ?", new object[] { AppLib.Context.Empresa, filial, CODOPER }).ToString();

            //USA NFE importacao
            bool UsaNfeImportacao = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT USANFEIMPORTACAO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, CODTIPOPER }));

            //USA NFE
            int UsaNfe = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT USAOPERACAONFE FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, CODTIPOPER }));

            //RECUPERA AS TAGS DA IDE(GOPER)
            DataTable dtGOPER = AppLib.Context.poolConnection.Get("Start").ExecQuery("select NUMERO,DATAEMISSAO, DATAENTSAI,TIPOPERCONSFIN,CLIENTERETIRA, NFEINFADIC, HISTORICO from GOPER WHERE CODEMPRESA=? AND CODOPER=?", new object[] { AppLib.Context.Empresa, CODOPER });

            //RECUPERA AS TAGS DA IDE(GFILIAL)
            DataTable dtGFILIAL = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT GFILIAL.* FROM GFILIAL INNER JOIN GOPER ON GFILIAL.CODFILIAL = GOPER.CODFILIAL WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ?", new object[] { AppLib.Context.Empresa, CODOPER });

            //RECUPERA natOp
            DataTable dtnatOp = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT DISTINCT(SUBSTRING(CODNATUREZA,1,5)) AS CODNATUREZA FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CODOPER });
            string natOp = "";
            if (dtnatOp.Rows.Count == 1)
            {
                //se retornar somente 1 ele ja serve como base para pegar a descrição do segundo nível.
                string parteCodNatureza = dtnatOp.Rows[0]["CODNATUREZA"].ToString().Trim();
                //parteCodNatureza = parteCodNatureza.Substring(0, parteCodNatureza.IndexOf('.'));

                natOp = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT DESCRICAO FROM VNATUREZA WHERE CODNATUREZA = ?", new object[] { parteCodNatureza }).ToString().Trim();

            }
            else if (dtnatOp.Rows.Count > 0)
            {
                //se retornar mais que 1 pega a descrição do primeiro nivel
                natOp = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT DESCRICAO FROM VNATUREZA WHERE CODNATUREZA = ?", new object[] { dtnatOp.Rows[0]["CODNATUREZA"].ToString().Trim() }).ToString().Trim();
            }

            //RECUPERA GOPER E GTIPOPER
            DataTable dtgOperGtipoOper = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT TIPOIMPRESSAODANFE, FINEMISSAONFE FROM GTIPOPER, GOPER WHERE GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER AND GOPER.CODOPER = ?", new object[] { CODOPER });

            if (dtgOperGtipoOper.Rows[0]["FINEMISSAONFE"].ToString() == "2")
            {
                // Substitui o valor da tag NATOP
                natOp = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT DESCNATOP FROM GTIPOPER WHERE CODTIPOPER = ?", new object[] { CODTIPOPER }).ToString();
            }

            //TAG IDDEST
            string codCliFor = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODCLIFOR FROM GOPER WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPER=?", new object[] { AppLib.Context.Empresa, filial, CODOPER }).ToString().Trim();
            string codEtd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR WHERE CODCLIFOR = ? AND CODEMPRESA = ?;", new object[] { codCliFor, AppLib.Context.Empresa }).ToString().Trim();
            string idDest = "";
            if (codEtd.Equals("SP"))
                idDest = "1";
            else if (codEtd.Equals("EX"))
                idDest = "3";
            else
                idDest = "2";

            //RECUPERA VCLIFOR
            string sqlVcliFor = @"SELECT 
              VCLIFOR.*, GCIDADE.NOME AS NOMECIDADE, GCIDADE.CODCIDADE AS CODIGO_CIDADE, GESTADO.CODIBGE AS COD_IBGE, GESTADO.CODETD AS COD_ETD, GPAIS.CODBACEN AS COD_BACEN
              FROM VCLIFOR 
              INNER JOIN GCIDADE ON VCLIFOR.CODCIDADE = GCIDADE.CODCIDADE
              INNER JOIN GESTADO ON VCLIFOR.CODETD = GESTADO.CODETD
              INNER JOIN GPAIS ON VCLIFOR.CODPAIS = GPAIS.CODPAIS
              WHERE
              VCLIFOR.CODEMPRESA = ? AND VCLIFOR.CODCLIFOR = ?";
            DataTable dtgVcliFor = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlVcliFor, new object[] { AppLib.Context.Empresa, codigoCliFor });

            //RECUPERA VPRODUTO E GOPERITEM 
            string usaCodAuxNfe = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT USACODAUXNFE FROM GTIPOPER, GOPER WHERE GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER AND GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ?", new object[] { AppLib.Context.Empresa, CODOPER }).ToString().Trim();
            DataTable dtDadosProduto = null;
            DataTable dtDadosProdutoICMSUFDest = null;
            string sqlDadosProduto = "";


            usaCodAuxNfe = (usaCodAuxNfe.ToLower() == "true") ? "1" : "0";
            if (usaCodAuxNfe == "1")
            {

                sqlDadosProduto = @"SELECT
                                    VPRODUTO.CODIGOAUXILIAR as CODIGO,('CODIGOAUXILIAR') AS SAIDA,
                                    GOPERITEM.NSEQITEM, VPRODUTOCODIGO.CODIGOBARRAS
                                    FROM
                                    VPRODUTO
                                    INNER JOIN GOPERITEM ON VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO
                                    LEFT JOIN VPRODUTOCODIGO ON VPRODUTO.CODPRODUTO = VPRODUTOCODIGO.CODPRODUTO
                                    WHERE
                                    VPRODUTO.CODEMPRESA = ?
                                    AND GOPERITEM.CODOPER = ?
                                    ORDER BY GOPERITEM.NSEQITEM";

                dtDadosProduto = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlDadosProduto, new object[] { AppLib.Context.Empresa, CODOPER });
            }
            else
            {
                sqlDadosProduto = @"SELECT
                                    VPRODUTO.CODPRODUTO as CODIGO,('CODPRODUTO') AS SAIDA,
                                    GOPERITEM.NSEQITEM, VPRODUTOCODIGO.CODIGOBARRAS
                                    FROM
                                    VPRODUTO
                                    INNER JOIN GOPERITEM ON VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO
                                    LEFT JOIN VPRODUTOCODIGO ON VPRODUTO.CODPRODUTO = VPRODUTOCODIGO.CODPRODUTO
                                    WHERE
                                    VPRODUTO.CODEMPRESA = ?
                                    AND GOPERITEM.CODOPER = ?
                                    ORDER BY GOPERITEM.NSEQITEM";

                dtDadosProduto = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlDadosProduto, new object[] { AppLib.Context.Empresa, CODOPER });
            }

            string Ufdestino = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { CODOPER, AppLib.Context.Empresa }).ToString();

            //RECUPERA VTRANSPORTADORA E GOPER
            //DataTable dtTransportadora = AppLib.Context.poolConnection.Get("Start").ExecQuery("select VTRANSPORTADORA.* from VTRANSPORTADORA INNER JOIN GOPER ON VTRANSPORTADORA.CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA WHERE GOPER.CODEMPRESA = ? AND GOPER.CODFILIAL = ? AND GOPER.CODOPER = ?", new object[] { AppLib.Context.Empresa, filial, CODOPER });

            //RECUPERA ICMS
            DataTable dtICMS = null;
            string sqldtICMS = "";

            //RECUPERA ICMS-ST
            DataTable dtICMS_ST = null;
            string sqldtICMS_ST = "";

            //string tipoICMS = "";

            int origemProduto;

            #region VARIAVEL TAG IDE
            string cUF = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODIBGE FROM GESTADO, GFILIAL WHERE GESTADO.CODETD = GFILIAL.CODETD AND GFILIAL.CODEMPRESA =? AND GFILIAL.CODFILIAL=?", new object[] { AppLib.Context.Empresa, filial }).ToString().Trim();
            string cNF = new Random().Next(10000000, 100000000).ToString();
            //string cNF = nf_formata.extrairNumDaChave(chaveNfe);
            string serie = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODSERIE FROM GOPER WHERE CODEMPRESA =? AND CODFILIAL =? AND CODOPER =?", new object[] { AppLib.Context.Empresa, filial, CODOPER }).ToString().Trim();
            string nNF = dtGOPER.Rows[0]["NUMERO"].ToString().TrimStart('0');
            string dhEmi = String.Format("{0:yyyy-MM-ddTHH:mm:ss-03:00}", dtGOPER.Rows[0]["DATAEMISSAO"]);//2018-06-04T00:00:00-03:00
            string dhSaiEnt = String.Format("{0:yyyy-MM-ddTHH:mm:ss-03:00}", dtGOPER.Rows[0]["DATAENTSAI"]);
            string cMunFG = dtGFILIAL.Rows[0]["CODCIDADE"].ToString().Trim();
            string tpImp = dtgOperGtipoOper.Rows[0]["TIPOIMPRESSAODANFE"].ToString().Trim();
            string tpEmis = dtGFILIAL.Rows[0]["MODALIDADE"].ToString().Trim();
            //string cDV = nf_formata.extrairUltimoDigitoChave(chaveNfe);
            string tpAmb = dtGFILIAL.Rows[0]["TPAMB"].ToString().Trim();
            string finNFe = dtgOperGtipoOper.Rows[0]["FINEMISSAONFE"].ToString().Trim();
            string indFinal = Convert.ToBoolean(dtGOPER.Rows[0]["TIPOPERCONSFIN"]) == true ? "1" : "0";
            string indPres = dtGOPER.Rows[0]["CLIENTERETIRA"].ToString().Trim();


            string tpNF = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TIPENTSAI FROM GTIPOPER WHERE CODEMPRESA=? AND CODTIPOPER=?", new object[] { AppLib.Context.Empresa, CODTIPOPER }).ToString().Trim();
            if (tpNF.ToUpper() == "E")
                tpNF = "0";
            else
                tpNF = "1";



            serie = nf_formata.ajustaSerie(serie);

            #endregion

            #region VARIAVEL TAG EMIT
            string cnpjFilial = nf_formata.limparCaracteres(dtGFILIAL.Rows[0]["CGCCPF"].ToString().Trim());
            string nomeFilial = dtGFILIAL.Rows[0]["NOME"].ToString().Trim();
            string nomeFantasiaFilial = dtGFILIAL.Rows[0]["NOMEFANTASIA"].ToString().Trim();
            #endregion

            #region VARIAVEL TAG ENDEREMIT
            string xLgr = dtGFILIAL.Rows[0]["RUA"].ToString().Trim();
            string nro = dtGFILIAL.Rows[0]["NUMERO"].ToString();
            string xCpl = dtGFILIAL.Rows[0]["COMPLEMENTO"].ToString();
            string xBairro = dtGFILIAL.Rows[0]["BAIRRO"].ToString();

            string xMun = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GCIDADE WHERE CODCIDADE = ?", new object[] { dtGFILIAL.Rows[0]["CODCIDADE"].ToString() }).ToString();
            string cMun = dtGFILIAL.Rows[0]["CODCIDADE"].ToString();
            string UF = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM GESTADO WHERE CODETD = ?", new object[] { dtGFILIAL.Rows[0]["CODETD"].ToString() }).ToString();
            string CEP = dtGFILIAL.Rows[0]["CEP"].ToString();
            string cPais = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODBACEN  FROM GPAIS WHERE CODPAIS = ?", new object[] { dtGFILIAL.Rows[0]["CODPAIS"].ToString() }).ToString();
            string fone = dtGFILIAL.Rows[0]["TELEFONE"].ToString();
            string IE = dtGFILIAL.Rows[0]["INSCRICAOESTADUAL"].ToString();
            string IM = dtGFILIAL.Rows[0]["INSCRICAOMUNICIPAL"].ToString();
            string CRT = dtGFILIAL.Rows[0]["CODREGIMETRIBUTARIO"].ToString();
            #endregion

            #region VARIAVEL TAG DEST
            int dest_FISICOJURIDICO = Convert.ToInt32(dtgVcliFor.Rows[0]["FISICOJURIDICO"]);
            string dest_CNPJ = dtgVcliFor.Rows[0]["CGCCPF"].ToString().Trim();
            string dest_idEstrangeiro = dtgVcliFor.Rows[0]["IDESTRANGEIRO"].ToString().Trim();
            string dest_xNome = (tpAmb == "2") ? "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL" : dtgVcliFor.Rows[0]["NOME"].ToString().Trim();
            string dest_xLgr = dtgVcliFor.Rows[0]["RUA"].ToString().Trim();
            string dest_nro = dtgVcliFor.Rows[0]["NUMERO"].ToString().Trim();
            string dest_xCpl = dtgVcliFor.Rows[0]["COMPLEMENTO"].ToString().Trim();
            string dest_xBairro = dtgVcliFor.Rows[0]["BAIRRO"].ToString().Trim();
            string dest_cMun = dtgVcliFor.Rows[0]["CODIGO_CIDADE"].ToString().Trim();
            string dest_xMun = dtgVcliFor.Rows[0]["NOMECIDADE"].ToString().Trim();
            string dest_UF = dtgVcliFor.Rows[0]["COD_ETD"].ToString().Trim();
            string dest_CEP = dtgVcliFor.Rows[0]["CEP"].ToString().Trim();
            string dest_cPais = dtgVcliFor.Rows[0]["COD_BACEN"].ToString().Trim();
            string dest_fone = dtgVcliFor.Rows[0]["TELCOMERCIAL"].ToString().Trim();


            string dest_indIEDest = "";
            if (dtgVcliFor.Rows[0]["CONTRIBICMS"].ToString().Trim() == "1")
                dest_indIEDest = "1";
            else if (dtgVcliFor.Rows[0]["CONTRIBICMS"].ToString().Trim() == "2")
                dest_indIEDest = "9";
            else if (dtgVcliFor.Rows[0]["CONTRIBICMS"].ToString().Trim() == "0")
                dest_indIEDest = "1";

            string dest_IE = dtgVcliFor.Rows[0]["INSCRICAOESTADUAL"].ToString().Trim();
            string dest_ISUF = dtgVcliFor.Rows[0]["INSCRICAOSUFRAMA"].ToString().Trim();
            string dest_IM = dtgVcliFor.Rows[0]["INSCRICAOMUNICIPAL"].ToString().Trim();
            string dest_email = dtgVcliFor.Rows[0]["EMAIL"].ToString().Trim();
            #endregion


            #region VARIAVEL DET E PROD
            string textoProdNfe = "";
            string sqlGoperItemTagProd = "";

            string det_xProd = "";
            string det_NCM = "";
            string det_CEST = "";
            string det_CFOP = "";
            string det_uCom = "";
            string det_indTot = "1";
            int usaLote = 0;
            #endregion

            //med
            string det_vTotTrib = "";

            #region VARIAVEL ICMS
            string sqlpFCP = "";
            DataTable dtICMS_preenche_fc = null;
            DataTable dtICMS_preenche_fc_ST = null;
            string retorno_vBCFCP = "";
            string retorno_pFCP = "";
            string retorno_vFCP = "";
            string retorno_vBCFCPST = "";
            string retorno_pFCPST = "";
            string retorno_vFCPST = "";

            //ICMS 00=
            string det_icms00_orig = "";
            string det_icms00_CST = "";
            string det_icms00_modBC = "";
            string det_icms00_vBC = "";
            string det_icms00_pICMS = "";
            string det_icms00_vICMS = "";
            string det_icms00_pFCP = "";
            string det_icms00_vFCP = "";

            //ICMS 10
            string det_icms10_orig = "";
            string det_icms10_CST = "";
            string det_icms10_modBC = "";
            string det_icms10_vBC = "";
            string det_icms10_pICMS = "";
            string det_icms10_vICMS = "";
            string det_icms10_vBCFCP = "";
            string det_icms10_pFCP = "";
            string det_icms10_vFCP = "";
            string det_icms10_modBCST = "";
            string det_icms10_pMVAST = "";
            string det_icms10_pRedBCST = "";
            string det_icms10_vBCST = "";
            string det_icms10_pICMSST = "";
            string det_icms10_vICMSST = "";
            string det_icms10_vBCFCPST = "";
            string det_icms10_pFCPST = "";
            string det_icms10_vFCPST = "";


            //ICMS 20
            string det_icms20_orig = "";
            string det_icms20_CST = "";
            string det_icms20_modBC = "";
            string det_icms20_pRedBC = "";
            string det_icms20_vBC = "";
            string det_icms20_pICMS = "";
            string det_icms20_vICMS = "";
            string det_icms20_vBCFCP = "";
            string det_icms20_pFCP = "";
            string det_icms20_vFCP = "";
            string det_icms20_vICMSDeson = "";
            string det_icms20_motDesICMS = "";

            //ICMS 30
            string det_icms30_orig = "";
            string det_icms30_CST = "";
            string det_icms30_modBCST = "";
            //string det_icms30_pMVAST = "";
            //string det_icms30_pRedBCST = "";
            string det_icms30_vBCST = "";
            string det_icms30_pICMSST = "";
            string det_icms30_vICMSST = "";
            string det_icms30_vBCFCPST = "";
            string det_icms30_pFCPST = "";
            string det_icms30_vFCPST = "";
            string det_icms30_vICMSDeson = "";
            string det_icms30_motDesICMS = "";


            //ICMS 40
            string det_icms40_orig = "";
            string det_icms40_CST = "";
            string det_icms40_vICMSDeson = "";
            string det_icms40_motDesICMS = "";

            //ICMS 51= "";
            string det_icms51_orig = "";
            string det_icms51_CST = "";
            string det_icms51_modBC = "";
            string det_icms51_pRedBCST = "";
            string det_icms51_vBC = "";
            string det_icms51_pICMS = "";
            string det_icms51_vICMSOp = "";
            string det_icms51_pDif = "";
            string det_icms51_vICMSDif = "";
            string det_icms51_vBCFCP = "";
            string det_icms51_vFCP = "";
            string det_icms51_pFCP = "";
            string det_icms51_vICMS = "";
            //string det_icms51_vICMSop = "";

            //ICMS 60
            string det_icms60_orig = "";
            string det_icms60_CST = "";
            string det_icms60_vBCSTRet = "";
            string det_icms60_pST = "";
            string det_icms60_vICMSSTRet = "";
            string det_icms60_vBCFCPSTRet = "";
            string det_icms60_pFCPSTRet = "";
            string det_icms60_vFCPSTRet = "";

            //ICMS 70= "";
            string det_icms70_orig = "";
            string det_icms70_CST = "";
            string det_icms70_modBC = "";
            string det_icms70_pRedBC = "";
            string det_icms70_vBC = "";
            string det_icms70_pICMS = "";
            string det_icms70_vICMS = "";
            string det_icms70_vBCFCP = "";
            string det_icms70_pFCP = "";
            string det_icms70_vFCP = "";
            string det_icms70_mpMVASTodBCST = "";
            string det_icms70_pRedBCST = "";
            string det_icms70_vBCST = "";
            string det_icms70_pICMSST = "";
            string det_icms70_vICMSST = "";
            string det_icms70_vBCFCPST = "";
            string det_icms70_pFCPST = "";
            string det_icms70_vFCPST = "";
            string det_icms70_vICMSDeson = "";
            string det_icms70_motDesICMS = "";
            string det_icms70_modBCST = "";
            string det_icms70_pMVAST = "";

            //ICMS 90
            string det_icms90_orig = "";
            string det_icms90_CST = "";
            string det_icms90_modBC = "";
            string det_icms90_vBC = "";
            string det_icms90_pRedBC = "";
            string det_icms90_pICMS = "";
            string det_icms90_vICMS = "";
            string det_icms90_vBCFCP = "";
            string det_icms90_pFCP = "";
            string det_icms90_vFCP = "";
            string det_icms90_modBCST = "";
            string det_icms90_pMVAST = "";
            string det_icms90_pRedBCST = "";
            string det_icms90_vBCST = "";
            string det_icms90_pICMSST = "";
            string det_icms90_vICMSST = "";
            string det_icms90_vBCFCPST = "";
            string det_icms90_pFCPST = "";
            string det_icms90_vFCPST = "";
            string det_icms90_vICMSDeson = "";
            string det_icms90_motDesICMS = "";


            //ICMSPart
            /*
            string det_icmspart_orig = "";
            string det_icmspart_CST = "";
            string det_icmspart_modBC = "";
            string det_icmspart_vBC = "";
            string det_icmspart_pRedBC = "";
            string det_icmspart_pICMS = "";
            string det_icmspart_vICMS = "";
            string det_icmspart_modBCST = "";
            string det_icmspart_pMVAST = "";
            string det_icmspart_pRedBCST = "";
            string det_icmspart_vBCST = "";
            string det_icmspart_pICMSST = "";
            string det_icmspart_vICMSST = "";
            string det_icmspart_pBCOp = "";
            string det_icmspart_UFST = "";
            */

            // ICMSST
            string det_icmsst_orig = "";
            string det_icmsst_CST = "";
            string det_icmsst_vBCSTRet = "";
            string det_icmsst_vICMSSTRet = "";
            string det_icmsst_vBCSTDest = "";
            string det_icmsst_vICMSSTDest = "";

            //ICMSSN101
            string det_icmssn101_orig = "";
            string det_icmssn101_CSOSN = "";
            string det_icmssn101_pCredSN = "";
            string det_icmssn101_vCredICMSSN = "";

            //ICMSSN102
            string det_icmssn102_orig = "";
            string det_icmssn102_CSOSN = "";

            //ICMSSN201
            string det_icmssn201_orig = "";
            string det_icmssn201_CSOSN = "";
            string det_icmssn201_modBCST = "";
            string det_icmssn201_pMVAST = "";
            string det_icmssn201_pRedBCST = "";
            string det_icmssn201_vBCST = "";
            string det_icmssn201_pICMSST = "";
            string det_icmssn201_vICMSST = "";
            string det_icmssn201_vBCFCPST = "";
            string det_icmssn201_pFCPST = "";
            string det_icmssn201_vFCPST = "";
            string det_icmssn201_pCredSN = "";
            string det_icmssn201_vCredICMSSN = "";

            //ICMSSN202
            string det_icmssn202_orig = "";
            string det_icmssn202_CSOSN = "";
            string det_icmssn202_modBCST = "";
            string det_icmssn202_pMVAST = "";
            string det_icmssn202_pRedBCST = "";
            string det_icmssn202_vBCST = "";
            string det_icmssn202_pICMSST = "";
            string det_icmssn202_vICMSST = "";
            string det_icmssn202_vBCFCPST = "";
            string det_icmssn202_pFCPST = "";
            string det_icmssn202_vFCPST = "";

            //ICMSSN500
            string det_icmssn500_orig = "";
            string det_icmssn500_CSOSN = "";
            string det_icmssn500_vBCSTRet = "";
            string det_icmssn500_pST = "";
            string det_icmssn500_vICMSSTRet = "";
            string det_icmssn500_vBCFCPSTRet = "";
            string det_icmssn500_pFCPSTRet = "";
            string det_icmssn500_vFCPSTRet = "";

            // ICMSSN900
            string det_icmssn900_orig = "";
            string det_icmssn900_CSOSN = "";
            string det_icmssn900_modBC = "";
            string det_icmssn900_vBC = "";
            string det_icmssn900_pRedBC = "";
            string det_icmssn900_pICMS = "";
            string det_icmssn900_vICMS = "";
            string det_icmssn900_modBCST = "";
            string det_icmssn900_pMVAST = "";
            string det_icmssn900_pRedBCST = "";
            string det_icmssn900_vBCST = "";
            string det_icmssn900_pICMSST = "";
            string det_icmssn900_vICMSST = "";
            string det_icmssn900_vBCFCPST = "";
            string det_icmssn900_pFCPST = "";
            string det_icmssn900_vFCPST = "";
            string det_icmssn900_pCredSN = "";
            string det_icmssn900_vCredICMSSN = "";

            //ICMSUFDest
            string det_icmsufdest_vBCUFDest = "";
            string det_icmsufdest_vBCFCPUFDest = "";
            string det_icmsufdest_pFCPUFDest = "";
            string det_icmsufdest_pICMSUFDest = "";
            string det_icmsufdest_pICMSInter = "";
            string det_icmsufdest_pICMSInterPart = "";
            string det_icmsufdest_vFCPUFDest = "";
            string det_icmsufdest_vICMSUFDest = "";
            string det_icmsufdest_vICMSUFRemet = "";
            #endregion

            #region VARIAVEL IPI
            //IPI
            string dest_ipi_clEnq = "";
            string dest_ipi_CNPJProd = "";
            string dest_ipi_infAdProd = "";
            string dest_ipint_VBC = "";
            string dest_ipint_PIPI = "";
            string dest_ipint_VIPI = "";

            //IPINT
            string dest_ipint_CST = "";
            #endregion

            #region     VARIAVEL PIS
            //PIS
            string dest_pis_CST = "";
            string dest_pis_vBC = "";
            string dest_pis_pPIS = "";
            string dest_pis_vPIS = "";

            //PIS ALIQUOTA
            string dest_pisaliq_CST = "";
            string dest_pisaliq_vBC = "";
            string dest_pisaliq_pPIS = "";
            string dest_pisaliq_vPIS = "";

            //PIS QTD
            string dest_pisqtd_CST = "";
            string dest_pisqtd_vPIS = "";

            //PIS NT
            string dest_pisnt_CST = "";

            //PIS OUTR
            string dest_pisoutr_CST = "";
            string dest_pisoutr_vBC = "";
            string dest_pisoutr_pPIS = "";
            string dest_pisoutr_vPIS = "";
            #endregion

            #region VARIAVEL COFINS
            //COFINSAliq
            string dest_cofinsaliq_CST = "";
            string dest_cofinsaliq_vBC = "";
            string dest_cofinsaliq_pCOFINS = "";
            string dest_cofinsaliq_vCOFINS = "";

            //COFINS QTD
            string dest_cofinsqtde_CST = "";
            string dest_cofinsqtde_vCOFINS = "";

            //COFINS NT
            string dest_cofinsnt_CST = "";

            //COFINS OUTR
            string dest_cofinsoutr_CST = "";
            string dest_cofinsoutr_vBC = "";
            string dest_cofinsoutr_pCOFINS = "";
            string dest_cofinsoutr_vCOFINS = "";
            #endregion

            //II
            string dest_ii_vBC = "";
            string dest_ii_vDespAdu = "";
            string dest_ii_vII = "";
            string dest_ii_vIOF = "";

            //TAG total
            string sqlTagTotal = "";
            DataTable dtTotal = null;

            //TAG Transportadora
            string sqlTransportadora = "";
            DataTable dtTransportadora = null;
            string modFrete = "";

            string sqlVeicTransp = "";
            DataTable dtVeicTransp = null;

            string sqlVol = "";
            DataTable dtVol = null;

            //TAG FAT
            string sqlFat = "";
            DataTable dtFat = null;

            //TAG DUP
            DataTable dtDup = null;

            //TAG PAG
            string sqlPag = "";
            DataTable dtPag = null;
            string cnpjcpf_pag = "";

            //TAG refNFe
            string sqlRefNFE = "";
            DataTable dtRefNFE = null;

            //TAG rastro
            DataTable dtRastro = null;
            string sqlRastro = "";
            string rastro_nLote = "";
            string rastro_qLote = "";
            string rastro_dFab = "";
            string rastro_dVal = "";
            string rastro_cAgregacao = "";

            //TAG pag
            string tpIntegra_pag = "";
            string tPag_pag = "";
            string vPag_pag = "";

            //TAG autXML
            DataTable dtAutXML = null;
            string cnpjcpf_autXML = "";

            //TAG INFADIC
            string infAdic_infCpl = dtGOPER.Rows[0]["NFEINFADIC"].ToString().Trim() + " " + dtGOPER.Rows[0]["HISTORICO"].ToString().Trim();
            infAdic_infCpl = nf_formata.trataCaracteres(infAdic_infCpl);

            //TRATAMENTO PARA A TAG 60 DO ICMS-ST

            string sqlCST_exclusivo_cst = "";
            string verificaCstExclusivo = "";

            //TOTALIZADOR DO 
            double totalizadorvFCP = 0;

            #region GERAÇÃO DO XML

            sb.Append("<?xml version='1.0' encoding='UTF-8' ?>\n");
            sb.Append("<NFe xmlns='http://www.portalfiscal.inf.br/nfe'>\n");

            string chaveNfe = nf_formata.GerarChaveAcessoNFe(Convert.ToInt32(cUF), string.Concat(Convert.ToDateTime(dhEmi).Year.ToString().Substring(2, 2), Convert.ToDateTime(dhEmi).Month.ToString().PadLeft(2, '0')), cnpjFilial, "55", serie, nNF, Convert.ToInt32(tpEmis), cNF);
            string cDV = nf_formata.extrairUltimoDigitoChave(chaveNfe);

            sb.Append("	<infNFe versao='4.00' Id='NFe" + chaveNfe + "'>\n");
            //sb.Append("		<versao></versao>\n");

            sb.Append("		<ide>\n");
            sb.Append("			<cUF>" + cUF + "</cUF>\n");
            sb.Append("			<cNF>" + cNF + "</cNF>\n");
            sb.Append("			<natOp>" + natOp + "</natOp>\n");
            sb.Append("			<mod>55</mod>\n");
            sb.Append("			<serie>" + serie + "</serie>\n");
            sb.Append("			<nNF>" + nNF + "</nNF>\n");
            sb.Append("			<dhEmi>" + dhEmi + "</dhEmi>\n");
            sb.Append("			<dhSaiEnt>" + dhSaiEnt + "</dhSaiEnt>\n");
            sb.Append("			<tpNF>" + tpNF + "</tpNF>\n");
            sb.Append("			<idDest>" + idDest + "</idDest>\n");
            sb.Append("			<cMunFG>" + cMunFG + "</cMunFG>\n");
            sb.Append("			<tpImp>" + tpImp + "</tpImp>\n");
            sb.Append("			<tpEmis>" + tpEmis + "</tpEmis>\n");
            sb.Append("			<cDV>" + cDV + "</cDV>\n");
            sb.Append("			<tpAmb>" + tpAmb + "</tpAmb>\n");
            sb.Append("			<finNFe>" + finNFe + "</finNFe>\n");
            sb.Append("			<indFinal>" + indFinal + "</indFinal>\n");
            sb.Append("			<indPres>" + indPres + "</indPres>\n");
            sb.Append("			<procEmi>0</procEmi>\n");
            sb.Append("			<verProc>005.000.000</verProc>\n");
            //sb.Append("			<dhCont></dhCont>\n");

            sb.Append("			<xJust></xJust>\n");


            sb.Append("			<NFref>\n");

            //RECUPERAS AS VARIAS CHAVES DA GOPERCOPIAREF 
            sqlRefNFE = @"SELECT
            GOPERCOPIAREF.CHAVENFE,
            VCLIFOR.CGCCPF,
            GESTADO.CODIBGE,
            GOPER.DATAEMISSAO,
            GOPER.NUMERO,
            GOPER.CODSERIE,
            GTIPOPER.FINEMISSAONFE
            FROM
              GOPERCOPIAREF
              INNER JOIN GOPER ON GOPERCOPIAREF.CODOPERDESTINO = GOPER.CODOPER
              INNER JOIN VCLIFOR ON GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR
              INNER JOIN GESTADO ON GESTADO.CODETD = VCLIFOR.CODETD
              INNER JOIN GTIPOPER ON GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER
                 WHERE
              GOPERCOPIAREF.CODEMPRESA = ? AND GOPERCOPIAREF.CODOPERDESTINO = ?"; // PARA TESTE AppLib.Context.Empresa 1, CODOPER 4142
            dtRefNFE = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlRefNFE, new object[] { AppLib.Context.Empresa, CODOPER });
            foreach (DataRow dr_RefNFE in dtRefNFE.Rows)
            {
                sb.Append("				<refNFe>" + dr_RefNFE["CHAVENFE"].ToString().Trim() + "</refNFe>\n");
                /*
                sb.Append("				<refNF>");
                sb.Append("					<cUF>" + dr_RefNFE["CODIBGE"].ToString().Trim() + "</cUF>\n");
                sb.Append("					<AAMM>" + String.Format("{0:yyMM}", Convert.ToDateTime(dr_RefNFE["DATAEMISSAO"])) + "</AAMM>\n");
                sb.Append("					<CNPJ>" + nf_formata.limparCaracteres(dr_RefNFE["CGCCPF"].ToString()) + "</CNPJ>\n");
                sb.Append("					<mod>" + dr_RefNFE["FINEMISSAONFE"].ToString().PadLeft(2, '0') + "</mod>\n");
                sb.Append("					<serie>" + dr_RefNFE["CODSERIE"].ToString().Trim() + "</serie>\n");
                sb.Append("					<nNF>" + dr_RefNFE["NUMERO"].ToString().TrimStart('0') + "</nNF>\n");
                sb.Append("				</refNF>\n");
                */
            }

            //sb.Append("				<refNFP>\n");
            //sb.Append("					<cUF></cUF>\n");
            //sb.Append("					<AAMM></AAMM>\n");
            //sb.Append("					<CNPJ></CNPJ>\n");
            //sb.Append("					<CPF></CPF>\n");
            //sb.Append("					<IE></IE>\n");
            //sb.Append("					<mod></mod>\n");
            //sb.Append("					<serie></serie>\n");
            //sb.Append("					<nNF></nNF>\n");
            //sb.Append("					<refCTe></refCTe>\n");
            //sb.Append("				</refNFP>\n");

            //sb.Append("				<refECF>\n");
            //sb.Append("					<mod></mod>\n");
            //sb.Append("					<nECF></nECF>\n");
            //sb.Append("					<nCOO></nCOO>\n");
            //sb.Append("				</refECF>\n");

            sb.Append("			</NFref>\n");


            sb.Append("		</ide>\n");



            sb.Append(" <emit>\n");
            sb.Append("			<CNPJ>" + cnpjFilial + "</CNPJ>\n");
            //sb.Append("			<CPF></CPF>\n");
            sb.Append("			<xNome>" + nomeFilial + "</xNome>\n");
            sb.Append("			<xFant>" + nomeFantasiaFilial + "</xFant>\n");
            sb.Append("			<enderEmit>\n");
            sb.Append("				<xLgr>" + xLgr + "</xLgr>\n");
            sb.Append("				<nro>" + nro + "</nro>\n");
            //sb.Append("				<xCpl></xCpl>\n");
            sb.Append("				<xBairro>" + xBairro + "</xBairro>\n");
            sb.Append("				<cMun>" + cMun + "</cMun>\n");
            sb.Append("				<xMun>" + xMun + "</xMun>\n");
            sb.Append("				<UF>" + UF + "</UF>\n");
            sb.Append("				<CEP>" + nf_formata.limparCaracteres(CEP) + "</CEP>\n");
            sb.Append("				<cPais>" + cPais + "</cPais>\n");
            //sb.Append("				<xPais></xPais>\n");
            sb.Append("				<fone>" + nf_formata.limparCaracteres(fone) + "</fone>\n");
            sb.Append("			</enderEmit>\n");
            sb.Append("			<IE>" + nf_formata.limparCaracteres(IE) + "</IE>\n");
            //sb.Append("			<IEST></IEST>\n");
            //sb.Append("			<IM></IM>\n");
            //sb.Append("			<CNAE></CNAE>\n");
            sb.Append("			<CRT>" + CRT + "</CRT>\n");
            sb.Append("	</emit>\n");

            //CAMPO FISICO OU JURIDICO IGUAL AO AUTXML

            sb.Append("		<dest>\n");

            if (dest_cPais == "1058")
            {
                if (dest_FISICOJURIDICO < 1)
                    sb.Append("			<CNPJ>" + nf_formata.limparCaracteres(dest_CNPJ) + "</CNPJ>\n");
                else
                    sb.Append("			<CPF>" + nf_formata.limparCaracteres(dest_CNPJ) + "</CPF>\n");
            }
            else
            {
                string idEstrangeiro = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT IDESTRANGEIRO FROM VCLIFOR WHERE CODCLIFOR = ? AND CODEMPRESA = ?;", new object[] { codCliFor, AppLib.Context.Empresa }).ToString().Trim();
                sb.Append("			<idEstrangeiro>" + idEstrangeiro.ToString() + "</idEstrangeiro>\n");
            }

            sb.Append("			<xNome>" + dest_xNome + "</xNome>\n");
            sb.Append("			<enderDest>\n");
            sb.Append("				<xLgr>" + dest_xLgr + "</xLgr>\n");
            sb.Append("				<nro>" + dest_nro + "</nro>\n");
            sb.Append("				<xCpl>" + dest_xCpl + "</xCpl>\n");
            sb.Append("				<xBairro>" + dest_xBairro + "</xBairro>\n");
            sb.Append("				<cMun>" + dest_cMun + "</cMun>\n");
            sb.Append("				<xMun>" + dest_xMun + "</xMun>\n");
            sb.Append("				<UF>" + dest_UF + "</UF>\n");
            sb.Append("				<CEP>" + nf_formata.limparCaracteres(dest_CEP) + "</CEP>\n");
            sb.Append("				<cPais>" + dest_cPais + "</cPais>\n");
            //sb.Append("				<xPais>Brasil</xPais>\n");
            sb.Append("				<fone>" + nf_formata.limparCaracteres(dest_fone) + "</fone>\n");
            sb.Append("			</enderDest>\n");
            sb.Append("			<indIEDest>" + dest_indIEDest + "</indIEDest>\n");
            sb.Append("			<IE>" + nf_formata.limparCaracteres(dest_IE) + "</IE>\n");
            //sb.Append("			<ISUF></ISUF>\n");
            sb.Append("			<IM>" + dest_IM + "</IM>\n");
            //sb.Append("			<email></email>\n");
            sb.Append("		</dest>\n");



            /*
            sb.Append("		<retirada>\n");
            sb.Append("			<CNPJ></CNPJ>\n");
            sb.Append("			<CPF></CPF>\n");
            sb.Append("			<xLgr></xLgr>\n");
            sb.Append("			<nro></nro>\n");
            sb.Append("			<xCpl></xCpl>\n");
            sb.Append("			<xBairro></xBairro>\n");
            sb.Append("			<cMun></cMun>\n");
            sb.Append("			<xMun></xMun>\n");
            sb.Append("			<UF></UF>\n");
            sb.Append("		</retirada>\n");
            */

            /*
            sb.Append("		<entrega>\n");
            sb.Append("			<CNPJ></CNPJ>\n");
            sb.Append("			<CPF></CPF>\n");
            sb.Append("			<xLgr></xLgr>\n");
            sb.Append("			<nro></nro>\n");
            sb.Append("			<xCpl></xCpl>\n");
            sb.Append("			<xBairro></xBairro>\n");
            sb.Append("			<cMun></cMun>\n");
            sb.Append("			<xMun></xMun>\n");
            sb.Append("			<UF></UF>\n");
            sb.Append("		</entrega>\n");
            */


            dtAutXML = AppLib.Context.poolConnection.Get("Start").ExecQuery("select CNPJCPF from GFILIALAUTXML WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, filial });

            if (dtAutXML.Rows.Count > 0)
            {
                foreach (DataRow dt_AutXML in dtAutXML.Rows)
                {
                    cnpjcpf_autXML = nf_formata.limparCaracteres(dt_AutXML["CNPJCPF"].ToString().Trim());

                    sb.Append("		<autXML>\n");

                    if (cnpjcpf_autXML.Length == 14) // se for CNPJ
                        sb.Append("			<CNPJ>" + cnpjcpf_autXML + "</CNPJ>\n");
                    else
                        sb.Append("			<CPF>" + cnpjcpf_autXML + "</CPF>\n");

                    sb.Append("		</autXML>\n");
                }
            }





            //PREENCHIMENTO DOS PRODUTOS
            #region LAÇO PARA OS PRODUTOS

            foreach (DataRow dr in dtDadosProduto.Rows)
            {

                //RETORNA det_xProd
                textoProdNfe = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT GTIPOPER.TEXTOPRODNFE FROM GTIPOPER INNER JOIN GOPER ON GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER WHERE GOPER.CODEMPRESA=? AND GOPER.CODOPER = ?", new object[] { AppLib.Context.Empresa, CODOPER }).ToString();

                if (textoProdNfe == "NOME")
                {
                    if (dr["SAIDA"].ToString() == "CODIGOAUXILIAR")
                    {
                        CodProduto = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODPRODUTO FROM VPRODUTO WHERE CODEMPRESA = ? AND CODIGOAUXILIAR = ? ", new object[] { AppLib.Context.Empresa, dr["CODIGO"].ToString() }).ToString();

                        det_xProd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT 
                                                                                                            VPRODUTO.NOME
                                                                                                            FROM 
                                                                                                            GOPERITEM,
                                                                                                            VPRODUTO 
                                                                                                            WHERE 
                                                                                                            GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA
                                                                                                            AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO
                                                                                                            AND GOPERITEM.CODEMPRESA = ?
                                                                                                            AND GOPERITEM.CODOPER = ?
                                                                                                            AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, CODOPER, CodProduto }).ToString().Trim();
                    }
                    else
                    {
                        det_xProd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT 
                                                                                                            VPRODUTO.NOME
                                                                                                            FROM 
                                                                                                            GOPERITEM,
                                                                                                            VPRODUTO 
                                                                                                            WHERE 
                                                                                                            GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA
                                                                                                            AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO
                                                                                                            AND GOPERITEM.CODEMPRESA = ?
                                                                                                            AND GOPERITEM.CODOPER = ?
                                                                                                            AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, CODOPER, dr["CODIGO"].ToString() }).ToString().Trim();
                    }
                }
                else if (textoProdNfe == "DESCRICAO")
                {
                    if (dr["SAIDA"].ToString() == "CODIGOAUXILIAR")
                    {
                        CodProduto = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODPRODUTO FROM VPRODUTO WHERE CODEMPRESA = ? AND CODIGOAUXILIAR = ? ", new object[] { AppLib.Context.Empresa, dr["CODIGO"].ToString() }).ToString();

                        det_xProd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT 
                                                                                                            VPRODUTO.DESCRICAO
                                                                                                            FROM 
                                                                                                            GOPERITEM,
                                                                                                            VPRODUTO 
                                                                                                            WHERE 
                                                                                                            GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA
                                                                                                            AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO
                                                                                                            AND GOPERITEM.CODEMPRESA = ?
                                                                                                            AND GOPERITEM.CODOPER = ?
                                                                                                            AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, CODOPER, CodProduto }).ToString().Trim();
                    }
                    else
                    {
                        det_xProd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT 
                                                                                                            VPRODUTO.DESCRICAO
                                                                                                            FROM 
                                                                                                            GOPERITEM,
                                                                                                            VPRODUTO 
                                                                                                            WHERE 
                                                                                                            GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA
                                                                                                            AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO
                                                                                                            AND GOPERITEM.CODEMPRESA = ?
                                                                                                            AND GOPERITEM.CODOPER = ?
                                                                                                            AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, CODOPER, dr["CODIGO"].ToString() }).ToString().Trim();
                    }
                }
                else if (textoProdNfe == "NOMEFANTASIA")
                {
                    if (dr["SAIDA"].ToString() == "CODIGOAUXILIAR")
                    {
                        CodProduto = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODPRODUTO FROM VPRODUTO WHERE CODEMPRESA = ? AND CODIGOAUXILIAR = ? ", new object[] { AppLib.Context.Empresa, dr["CODIGO"].ToString() }).ToString();

                        det_xProd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT 
                                                                                                            VPRODUTO.NOMEFANTASIA 
                                                                                                            FROM 
                                                                                                            GOPERITEM,
                                                                                                            VPRODUTO 
                                                                                                            WHERE 
                                                                                                            GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA
                                                                                                            AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO
                                                                                                            AND GOPERITEM.CODEMPRESA = ?
                                                                                                            AND GOPERITEM.CODOPER = ?
                                                                                                            AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, CODOPER, CodProduto }).ToString().Trim();
                    }
                    else
                    {
                        det_xProd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT 
                                                                                                            VPRODUTO.NOMEFANTASIA 
                                                                                                            FROM 
                                                                                                            GOPERITEM,
                                                                                                            VPRODUTO 
                                                                                                            WHERE 
                                                                                                            GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA
                                                                                                            AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO
                                                                                                            AND GOPERITEM.CODEMPRESA = ?
                                                                                                            AND GOPERITEM.CODOPER = ?
                                                                                                            AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, CODOPER, dr["CODIGO"].ToString() }).ToString().Trim();
                    }
                }

                #region Código do xPRod comentado

                //if (textoProdNfe == "NOME")
                //{
                //    if (dr["SAIDA"].ToString() == "CODIGOAUXILIAR")
                //        det_xProd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM VPRODUTO WHERE CODIGOAUXILIAR = ?", new object[] { dr["CODIGO"].ToString() }).ToString().Trim();
                //    else
                //        det_xProd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM VPRODUTO WHERE CODPRODUTO = ?", new object[] { dr["CODIGO"].ToString() }).ToString().Trim();
                //}
                //else if (textoProdNfe == "DESCRICAO")
                //{
                //    if (dr["SAIDA"].ToString() == "CODIGOAUXILIAR")
                //        det_xProd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT DESCRICAO FROM VPRODUTO WHERE CODIGOAUXILIAR = ?", new object[] { dr["CODIGO"].ToString() }).ToString().Trim();
                //    else
                //        det_xProd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT DESCRICAO FROM VPRODUTO WHERE CODPRODUTO = ?", new object[] { dr["CODIGO"].ToString() }).ToString().Trim();
                //}
                //else if (textoProdNfe == "NOMEFANTASIA")
                //{
                //    if (dr["SAIDA"].ToString() == "CODIGOAUXILIAR")
                //        det_xProd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOMEFANTASIA FROM VPRODUTO WHERE CODIGOAUXILIAR = ?", new object[] { dr["CODIGO"].ToString() }).ToString().Trim();
                //    else
                //        det_xProd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOMEFANTASIA FROM VPRODUTO WHERE CODPRODUTO = ?", new object[] { dr["CODIGO"].ToString() }).ToString().Trim();
                //}

                #endregion

                //RETORNA det_NCM, det_CEST
                if (dr["SAIDA"].ToString() == "CODIGOAUXILIAR")
                {
                    det_NCM = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODNCM FROM VPRODUTO WHERE CODIGOAUXILIAR = ? AND CODEMPRESA = ?", new object[] { dr["CODIGO"].ToString().Trim(), AppLib.Context.Empresa }).ToString().Trim();
                    det_CEST = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CEST FROM VPRODUTO WHERE CODIGOAUXILIAR = ? AND CODEMPRESA = ?", new object[] { dr["CODIGO"].ToString().Trim(), AppLib.Context.Empresa }).ToString().Trim();
                    det_uCom = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODUNIDVENDA FROM VPRODUTO WHERE CODIGOAUXILIAR = ? AND CODEMPRESA = ?", new object[] { dr["CODIGO"].ToString().Trim(), AppLib.Context.Empresa }).ToString().Trim();
                    origemProduto = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT PROCEDENCIA FROM VPRODUTO WHERE CODIGOAUXILIAR = ? AND CODEMPRESA = ?", new object[] { dr["CODIGO"].ToString().Trim(), AppLib.Context.Empresa }));

                    //USA LOTE
                    usaLote = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT USALOTEPRODUTO FROM VPRODUTO WHERE CODIGOAUXILIAR = ? AND CODEMPRESA = ?", new object[] { dr["CODIGO"].ToString().Trim(), AppLib.Context.Empresa }));
                }
                else
                {
                    det_NCM = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODNCM FROM VPRODUTO WHERE CODPRODUTO = ? AND CODEMPRESA = ?", new object[] { dr["CODIGO"].ToString().Trim(), AppLib.Context.Empresa }).ToString().Trim();
                    det_CEST = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CEST FROM VPRODUTO WHERE CODPRODUTO = ? AND CODEMPRESA = ?", new object[] { dr["CODIGO"].ToString().Trim(), AppLib.Context.Empresa }).ToString().Trim();
                    det_uCom = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODUNIDVENDA FROM VPRODUTO WHERE CODPRODUTO = ? AND CODEMPRESA = ?", new object[] { dr["CODIGO"].ToString().Trim(), AppLib.Context.Empresa }).ToString().Trim();
                    origemProduto = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT PROCEDENCIA FROM VPRODUTO WHERE CODPRODUTO = ? AND CODEMPRESA = ?", new object[] { dr["CODIGO"].ToString().Trim(), AppLib.Context.Empresa }));

                    //USA LOTE
                    usaLote = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT USALOTEPRODUTO FROM VPRODUTO WHERE CODPRODUTO = ? AND CODEMPRESA = ?", new object[] { dr["CODIGO"].ToString().Trim(), AppLib.Context.Empresa }));
                }


                sqlGoperItemTagProd = @"SELECT * FROM GOPERITEM WHERE CODOPER = ? AND NSEQITEM = ? AND CODEMPRESA = ?";
                dtgOperItemTagProd = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlGoperItemTagProd, new object[] { CODOPER, dr["NSEQITEM"].ToString().Trim(), AppLib.Context.Empresa });

                sb.Append("		<det nItem=\"" + dr["NSEQITEM"].ToString().Trim() + "\">\n");
                sb.Append("			<prod>\n");
                sb.Append("				<cProd>" + dr["CODIGO"].ToString().Trim() + "</cProd>\n");

                if (dr["CODIGOBARRAS"].ToString().Trim() == "")
                    sb.Append("				<cEAN>SEM GTIN</cEAN>\n");
                else
                    sb.Append("				<cEAN>" + dr["CODIGOBARRAS"].ToString().Trim() + "</cEAN>\n");

                sb.Append("				<xProd>" + det_xProd.Trim() + "</xProd>\n");
                sb.Append("				<NCM>" + nf_formata.limparCaracteres(det_NCM) + "</NCM>\n");
                //sb.Append("				<NVE></NVE>\n");
                sb.Append("				<CEST>" + det_CEST + "</CEST>\n");

                //sb.Append("				<indEscala></indEscala>\n");
                //sb.Append("				<CNPJFab></CNPJFab>\n");
                //sb.Append("				<cBenef></cBenef>\n");
                //sb.Append("				<EXTIPI></EXTIPI>\n");

                det_CFOP = nf_formata.limparCaracteres(dtgOperItemTagProd.Rows[0]["CODNATUREZA"].ToString().Trim());
                det_CFOP = det_CFOP.Substring(0, 4);
                sb.Append("				<CFOP>" + det_CFOP + "</CFOP>\n");
                sb.Append("				<uCom>" + det_uCom + "</uCom>\n");
                sb.Append("				<qCom>" + nf_formata.trocarVirgulaPorPonto(dtgOperItemTagProd.Rows[0]["QUANTIDADE"].ToString().Trim()) + "</qCom>\n");
                sb.Append("				<vUnCom>" + nf_formata.trocarVirgulaPorPonto(dtgOperItemTagProd.Rows[0]["VLUNITARIO"].ToString().Trim()) + "</vUnCom>\n");
                sb.Append("				<vProd>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtgOperItemTagProd.Rows[0]["VLTOTALITEM"].ToString().Trim())) + "</vProd>\n");

                if (dr["CODIGOBARRAS"].ToString().Trim() == "")
                    sb.Append("				<cEANTrib>SEM GTIN</cEANTrib>\n");
                else
                    sb.Append("				<cEANTrib>" + dr["CODIGOBARRAS"].ToString().Trim() + "</cEANTrib>\n");

                sb.Append("				<uTrib>" + dtgOperItemTagProd.Rows[0]["CODUNIDOPER"].ToString().Trim() + "</uTrib>\n");
                sb.Append("				<qTrib>" + nf_formata.trocarVirgulaPorPonto(dtgOperItemTagProd.Rows[0]["QUANTIDADE"].ToString().Trim()) + "</qTrib>\n");
                sb.Append("				<vUnTrib>" + nf_formata.trocarVirgulaPorPonto(dtgOperItemTagProd.Rows[0]["VLUNITARIO"].ToString().Trim()) + "</vUnTrib>\n");

                if (Convert.ToDouble(dtgOperItemTagProd.Rows[0]["RATEIOFRETE"]) > 0)
                    sb.Append("				<vFrete>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtgOperItemTagProd.Rows[0]["RATEIOFRETE"].ToString().Trim())) + "</vFrete>\n");

                if (Convert.ToDouble(dtgOperItemTagProd.Rows[0]["RATEIOSEGURO"]) > 0)
                    sb.Append("				<vSeg>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtgOperItemTagProd.Rows[0]["RATEIOSEGURO"].ToString().Trim())) + "</vSeg>\n");


                if (Convert.ToDouble(dtgOperItemTagProd.Rows[0]["RATEIODESCONTO"]) > 0)
                    sb.Append("				<vDesc>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtgOperItemTagProd.Rows[0]["RATEIODESCONTO"].ToString().Trim())) + "</vDesc>\n");

                if (Convert.ToDouble(dtgOperItemTagProd.Rows[0]["RATEIODESPESA"]) > 0)
                    sb.Append("				<vOutro>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtgOperItemTagProd.Rows[0]["RATEIODESPESA"].ToString().Trim())) + "</vOutro>\n");

                sb.Append("				<indTot>" + det_indTot + "</indTot>\n");


                //VERIFICA SE USARA LOTE
                if (usaLote > 0)
                {

                    //RELACIONADO AO LOTE
                    sqlRastro = @"
                SELECT 
                    VPRODUTOLOTE.NUMERO, 
                    VPRODUTOLOTE.DATAFABRICACAO, 
                    VPRODUTOLOTE.DATAVALIDADE,
                    GOPERITEMLOTE.QUANTIDADE,
                    VPRODUTOLOTE.CODAGREGACAO
                FROM GOPERITEMLOTE 
                INNER JOIN VPRODUTOLOTE ON GOPERITEMLOTE.CODLOTE = VPRODUTOLOTE.CODLOTE 
                AND GOPERITEMLOTE.CODEMPRESA = VPRODUTOLOTE.CODEMPRESA 
                AND GOPERITEMLOTE.CODFILIAL = VPRODUTOLOTE.CODFILIAL
                WHERE
                GOPERITEMLOTE.CODOPER = ? 
                AND GOPERITEMLOTE.CODEMPRESA = ? 
                AND GOPERITEMLOTE.CODFILIAL= ?
                ";

                    dtRastro = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlRastro, new object[] { CODOPER, AppLib.Context.Empresa, filial });

                    if (dtRastro.Rows.Count > 0)
                    {
                        rastro_nLote = dtRastro.Rows[0]["NUMERO"].ToString().Trim();
                        rastro_qLote = dtRastro.Rows[0]["QUANTIDADE"].ToString().Trim();
                        rastro_cAgregacao = dtRastro.Rows[0]["CODAGREGACAO"].ToString().Trim();

                        if (!String.IsNullOrEmpty(dtRastro.Rows[0]["DATAFABRICACAO"].ToString().Trim()))
                            rastro_dFab = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dtRastro.Rows[0]["DATAFABRICACAO"]));

                        if (!String.IsNullOrEmpty(dtRastro.Rows[0]["DATAVALIDADE"].ToString().Trim()))
                            rastro_dVal = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dtRastro.Rows[0]["DATAVALIDADE"]));

                        sb.Append("				<rastro>\n");
                        sb.Append("					<nLote>" + rastro_nLote + "</nLote>\n");
                        sb.Append("					<qLote>" + rastro_qLote + "</qLote>\n");
                        sb.Append("					<dFab>" + rastro_dFab + "</dFab>\n");
                        sb.Append("					<dVal>" + rastro_dVal + "</dVal>\n");
                        sb.Append("					<cAgreg>" + rastro_cAgregacao + "</cAgreg>\n");
                        sb.Append("				</rastro>\n");
                    }
                }

                if (UsaNfeImportacao)
                {
                    if (dtgOperItemTagProd.Rows.Count > 0)
                    {

                        sb.Append("				<DI>\n");
                        sb.Append("					<nDI>" + dtgOperItemTagProd.Rows[0]["NUMERODI"].ToString().Trim() + "</nDI>\n");
                        sb.Append("					<dDI>" + String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dtgOperItemTagProd.Rows[0]["DATADI"])) + "</dDI>\n");
                        sb.Append("					<xLocDesemb>" + dtgOperItemTagProd.Rows[0]["LOCDESEMB"].ToString().Trim() + "</xLocDesemb>\n");
                        sb.Append("					<UFDesemb>" + dtgOperItemTagProd.Rows[0]["UFDESEMB"].ToString().Trim() + "</UFDesemb>\n");
                        sb.Append("					<dDesemb>" + String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dtgOperItemTagProd.Rows[0]["DATADESEMB"])) + "</dDesemb>\n");
                        sb.Append("					<tpViaTransp>" + dtgOperItemTagProd.Rows[0]["TPVIATRANSP"].ToString().Trim() + "</tpViaTransp>\n");
                        sb.Append("					<vAFRMM>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtgOperItemTagProd.Rows[0]["VAFRMM"].ToString())) + "</vAFRMM>\n");
                        sb.Append("					<tpIntermedio>" + dtgOperItemTagProd.Rows[0]["TPINTERMEDIO"].ToString().Trim() + "</tpIntermedio>\n");
                        sb.Append("					<CNPJ>" + nf_formata.limparCaracteres(dtgOperItemTagProd.Rows[0]["CNPJ"].ToString()) + "</CNPJ>\n");
                        sb.Append("					<UFTerceiro>" + dtgOperItemTagProd.Rows[0]["UFTERCEIRO"].ToString().Trim() + "</UFTerceiro>\n");
                        sb.Append("					<cExportador>" + dtgOperItemTagProd.Rows[0]["CODEXPORTADOR"].ToString().Trim() + "</cExportador>\n");
                        sb.Append("					<adi>\n");
                        sb.Append("						<nAdicao>" + dtgOperItemTagProd.Rows[0]["NUMADICAO"].ToString().Trim() + "</nAdicao>\n");
                        sb.Append("						<nSeqAdic>" + dtgOperItemTagProd.Rows[0]["NUMSEQADIC"].ToString().Trim() + "</nSeqAdic>\n");
                        sb.Append("						<cFabricante>" + dtgOperItemTagProd.Rows[0]["CODFABRICANTE"].ToString().Trim() + "</cFabricante>\n");
                        sb.Append("						<vDescDI>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtgOperItemTagProd.Rows[0]["VLORDESCDI"].ToString())) + "</vDescDI>\n");
                        sb.Append("						<nDraw>" + dtgOperItemTagProd.Rows[0]["NDRAW"].ToString().Trim() + "</nDraw>\n");
                        sb.Append("					</adi>\n");
                        sb.Append("				</DI>\n");
                    }
                }


                //sb.Append("				<detExport>\n");
                //sb.Append("					<nDraw></nDraw>\n");
                //sb.Append("					<exportInd>\n");
                //sb.Append("						<nRE></nRE>\n");
                //sb.Append("						<chNFe></chNFe>\n");
                //sb.Append("						<qExport></qExport>\n");
                //sb.Append("					</exportInd>\n");
                //sb.Append("				</detExport>\n");

                //sb.Append("				<xPed></xPed>\n");
                //sb.Append("				<nItemPed></nItemPed>\n");
                //sb.Append("				<nFCI></nFCI>\n");
                //sb.Append("				<veicProd>\n");
                //sb.Append("					<tpOp></tpOp>\n");
                //sb.Append("					<chassi></chassi>\n");
                //sb.Append("					<cCor></cCor>\n");
                //sb.Append("					<xCor></xCor>\n");
                //sb.Append("					<pot></pot>\n");
                //sb.Append("					<cilin></cilin>\n");
                //sb.Append("					<pesoL></pesoL>\n");
                //sb.Append("					<pesoB></pesoB>\n");
                //sb.Append("					<nSerie></nSerie>\n");
                //sb.Append("					<tpComb></tpComb>\n");
                //sb.Append("					<nMotor></nMotor>\n");
                //sb.Append("					<CMT></CMT>\n");
                //sb.Append("					<dist></dist>\n");
                //sb.Append("					<anoMod></anoMod>\n");
                //sb.Append("					<anoFab></anoFab>\n");
                //sb.Append("					<tpPint></tpPint>\n");
                //sb.Append("					<tpVeic></tpVeic>\n");
                //sb.Append("					<espVeic></espVeic>\n");
                //sb.Append("					<VIN></VIN>\n");
                //sb.Append("					<condVeic></condVeic>\n");
                //sb.Append("					<cMod></cMod>\n");
                //sb.Append("					<cCorDENATRAN></cCorDENATRAN>\n");
                //sb.Append("					<lota></lota>\n");
                //sb.Append("					<tpRest></tpRest>\n");
                //sb.Append("				</veicProd>\n");

                //sb.Append("				<med>\n");
                //sb.Append("					<cProdANVISA></cProdANVISA>\n");
                //sb.Append("					<vPMC></vPMC>\n");
                //sb.Append("				</med>\n");

                //sb.Append("				<arma>\n");
                //sb.Append("					<tpArma></tpArma>\n");
                //sb.Append("					<nSerie></nSerie>\n");
                //sb.Append("					<nCano></nCano>\n");
                //sb.Append("					<descr></descr>\n");
                //sb.Append("				</arma>\n");

                //sb.Append("				<comb>\n");
                //sb.Append("					<cProdANP></cProdANP>\n");
                //sb.Append("					<descANP></descANP>\n");
                //sb.Append("					<pGLP></pGLP>\n");
                //sb.Append("					<pGNn></pGNn>\n");
                //sb.Append("					<pGNi></pGNi>\n");
                //sb.Append("					<vPart></vPart>\n");
                //sb.Append("					<CODIF></CODIF>\n");
                //sb.Append("					<qTemp></qTemp>\n");
                //sb.Append("					<UFCons></UFCons>\n");
                //sb.Append("					<CIDE>\n");
                //sb.Append("						<qBCProd></qBCProd>\n");
                //sb.Append("						<vAliqProd></vAliqProd>\n");
                //sb.Append("						<vCIDE></vCIDE>\n");
                //sb.Append("					</CIDE>\n");
                //sb.Append("					<encerrante>\n");
                //sb.Append("						<nBico></nBico>\n");
                //sb.Append("						<nBomba></nBomba>\n");
                //sb.Append("						<nTanque></nTanque>\n");
                //sb.Append("						<vEncIni></vEncIni>\n");
                //sb.Append("						<vEncFin></vEncFin>\n");
                //sb.Append("					</encerrante>\n");
                //sb.Append("				</comb>\n");

                //sb.Append("				<nRECOPI></nRECOPI>\n");


                sb.Append("			</prod>\n");

                sb.Append("			<imposto>\n");
                sb.Append("				<vTotTrib>" + det_vTotTrib + "</vTotTrib>\n");


                sb.Append("				<ICMS>\n");



                #region TRATAMENTO DAS TAGS ICMS
                /****************************************TRATAMENTO DAS TAGS ICMS****************************************************/
                sqldtICMS = @"SELECT *,
                                    (SELECT CODTIPOTRIBUTO FROM VTRIBUTO WHERE CODTRIBUTO = GOPERITEMTRIBUTO.CODTRIBUTO AND CODEMPRESA = GOPERITEMTRIBUTO.CODEMPRESA) CODTIPOTRIBUTO FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?  AND CODTRIBUTO = 'ICMS' ORDER BY CODCST";
                dtICMS = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqldtICMS, new object[] { AppLib.Context.Empresa, CODOPER, dr["NSEQITEM"].ToString().Trim() });

                foreach (DataRow dr_ICMS in dtICMS.Rows)
                {

                    //Preenche a TAG pFCP
                    //sqlpFCP = @"SELECT VBCFCPUFDEST, PFCPUFDEST, VFCPUFDEST, VBCFCPSTUFDEST, PFCPSTUFDEST, VFCPSTUFDEST FROM GOPERITEMDIFAL WHERE CODEMPRESA=? AND CODOPER=? AND NSEQITEM=?";
                    sqlpFCP = @"SELECT *,
                                    (SELECT CODTIPOTRIBUTO FROM VTRIBUTO WHERE CODTRIBUTO = GOPERITEMTRIBUTO.CODTRIBUTO AND CODEMPRESA = GOPERITEMTRIBUTO.CODEMPRESA) CODTIPOTRIBUTO FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?  AND CODTRIBUTO = 'ICMS' ORDER BY CODCST";

                    dtICMS_preenche_fc = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlpFCP, new object[] { AppLib.Context.Empresa, CODOPER, dr["NSEQITEM"].ToString().Trim() });
                    if (dtICMS_preenche_fc.Rows.Count > 0)
                    {

                        //retorno_pFCP = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtICMS_preenche_fc.Rows[0]["PFCPUFDEST"].ToString().Trim()));
                        //nova forma de recuperar o percentual
                        retorno_pFCP = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT isnull(VREGRAVARCFOP.PFCP,0) FROM VREGRAVARCFOP WHERE VREGRAVARCFOP.NCM = ? AND VREGRAVARCFOP.UFDESTINO = ?", new object[] { det_NCM.Trim(), Ufdestino }).ToString().Trim();

                        if (!String.IsNullOrEmpty(retorno_pFCP))
                        {

                            retorno_pFCP = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(retorno_pFCP));
                            /************/
                            retorno_vBCFCP = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtICMS_preenche_fc.Rows[0]["BASECALCULO"].ToString().Trim()));
                            retorno_vFCP = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2((Convert.ToDouble(retorno_pFCP) / 100 * Convert.ToDouble(retorno_vBCFCP)).ToString()));

                        }
                        else
                        {
                            retorno_pFCP = "";
                            retorno_vBCFCP = "";
                            retorno_vFCP = "";
                        }

                    }

                    switch (dr_ICMS["CODCST"].ToString().Trim())
                    {
                        case "00":
                            det_icms00_orig = origemProduto.ToString().Trim();
                            det_icms00_CST = dr_ICMS["CODCST"].ToString().PadLeft(2, '0');
                            det_icms00_modBC = dr_ICMS["MODALIDADEBC"].ToString().Trim();
                            det_icms00_vBC = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["BASECALCULO"].ToString().Trim()));
                            det_icms00_pICMS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["ALIQUOTA"].ToString().Trim()));
                            det_icms00_vICMS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["VALOR"].ToString().Trim()));
                            det_icms00_pFCP = retorno_pFCP;
                            det_icms00_vFCP = retorno_vFCP;

                            //totalizador
                            totalizadorvFCP += String.IsNullOrEmpty(det_icms00_vFCP) ? 0 : Convert.ToDouble(det_icms00_vFCP);

                            break;

                        case "10":
                            det_icms10_orig = origemProduto.ToString().Trim();
                            det_icms10_CST = dr_ICMS["CODCST"].ToString().PadLeft(2, '0');
                            det_icms10_modBC = dr_ICMS["MODALIDADEBC"].ToString().Trim();
                            det_icms10_vBC = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["BASECALCULO"].ToString().Trim()));
                            det_icms10_pICMS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["ALIQUOTA"].ToString().Trim()));
                            det_icms10_vICMS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["VALOR"].ToString().Trim()));
                            /**/
                            det_icms10_vBCFCP = retorno_vBCFCP;
                            /**/
                            det_icms10_pFCP = retorno_pFCP;
                            /**/
                            det_icms10_vFCP = retorno_vFCP;

                            //totalizador
                            totalizadorvFCP += String.IsNullOrEmpty(det_icms10_vFCP) ? 0 : Convert.ToDouble(det_icms10_vFCP);

                            //ajusta para vbcfcp, pfcp e vfcp
                            if (Convert.ToDouble(det_icms10_pFCP) > 0)
                            {
                                det_icms10_vFCP = (Convert.ToDouble(det_icms10_vBCFCP) * Convert.ToDouble(det_icms10_pFCP) / 100).ToString();
                            }
                            else
                            {

                                det_icms10_pFCP = "";
                                det_icms10_vFCP = "";
                                det_icms10_vBCFCP = "";
                            }

                            break;

                        case "20":
                            det_icms20_orig = origemProduto.ToString().Trim();
                            det_icms20_CST = dr_ICMS["CODCST"].ToString().PadLeft(2, '0');
                            det_icms20_modBC = dr_ICMS["MODALIDADEBC"].ToString().Trim();
                            det_icms20_vBC = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["BASECALCULO"].ToString().Trim()));
                            det_icms20_pICMS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["ALIQUOTA"].ToString().Trim()));
                            det_icms20_vICMS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["VALOR"].ToString().Trim()));
                            /**/
                            det_icms20_vBCFCP = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["BASECALCULO"].ToString().Trim()));
                            det_icms20_pRedBC = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["REDUCAOBASEICMS"].ToString().Trim()));                                                                                            /**/
                                                                                                                                                                                                                                                       /**/
                            det_icms20_pFCP = retorno_pFCP;
                            /**/
                            det_icms20_vFCP = retorno_vFCP;
                            det_icms20_vICMSDeson = "";
                            det_icms20_motDesICMS = "";

                            //totalizador
                            totalizadorvFCP += String.IsNullOrEmpty(det_icms20_vFCP) ? 0 : Convert.ToDouble(det_icms20_vFCP);

                            //ajusta para vbcfcp, pfcp e vfcp
                            if (Convert.ToDouble(det_icms20_pFCP) > 0)
                            {
                                det_icms20_vFCP = (Convert.ToDouble(det_icms20_vBCFCP) * Convert.ToDouble(det_icms20_pFCP) / 100).ToString();
                            }
                            else
                            {

                                det_icms20_pFCP = "";
                                det_icms20_vFCP = "";
                                det_icms20_vBCFCP = "";
                            }

                            break;

                        case "30":
                            det_icms30_orig = origemProduto.ToString().Trim();
                            det_icms30_CST = dr_ICMS["CODCST"].ToString().Trim().PadLeft(2, '0');

                            det_icms30_vICMSDeson = "";
                            det_icms30_motDesICMS = "";
                            break;

                        case "40":
                        case "41":
                        case "50":
                            det_icms40_orig = origemProduto.ToString().Trim();
                            det_icms40_CST = dr_ICMS["CODCST"].ToString().PadLeft(2, '0');
                            det_icms40_vICMSDeson = "";
                            det_icms40_motDesICMS = "";
                            break;

                        case "51":
                            det_icms51_orig = origemProduto.ToString().Trim();
                            det_icms51_CST = dr_ICMS["CODCST"].ToString().PadLeft(2, '0');
                            det_icms51_modBC = dr_ICMS["MODALIDADEBC"].ToString().Trim();
                            det_icms51_pRedBCST = "";
                            det_icms51_vBC = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["BASECALCULO"].ToString().Trim()));
                            det_icms51_pICMS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["ALIQUOTA"].ToString().Trim()));
                            det_icms51_vICMS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["VALOR"].ToString().Trim()));
                            //det_icms51_vICMSOp = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2((Convert.ToDouble(dr_ICMS["BCORIGINAL"].ToString()) * (Convert.ToDouble(dr_ICMS["ALIQUOTA"].ToString()) /100)).ToString()));

                            det_icms51_vICMSOp = ((Convert.ToDouble(dr_ICMS["BCORIGINAL"]) * Convert.ToDouble(dr_ICMS["ALIQUOTA"])) / 100).ToString();
                            det_icms51_vICMSOp = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(det_icms51_vICMSOp));

                            det_icms51_vICMSDif = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["VICMSDIF"].ToString().Trim()));
                            det_icms51_pDif = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["PDIF"].ToString().Trim()));
                            /**/
                            det_icms51_vBCFCP = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["BASECALCULO"].ToString().Trim()));
                            /**/
                            det_icms51_pFCP = retorno_pFCP; // dr_ICMS[""].ToString();
                                                            /**/
                            det_icms51_vFCP = retorno_vFCP; // dr_ICMS[""].ToString();


                            //totalizador
                            totalizadorvFCP += String.IsNullOrEmpty(det_icms51_vFCP) ? 0 : Convert.ToDouble(det_icms51_vFCP);


                            //ajusta para vbcfcp, pfcp e vfcp
                            if (Convert.ToDouble(det_icms51_pFCP) > 0)
                            {
                                det_icms51_vFCP = (Convert.ToDouble(det_icms51_vBCFCP) * Convert.ToDouble(det_icms51_pFCP) / 100).ToString();
                            }
                            else
                            {

                                det_icms51_pFCP = "";
                                det_icms51_vFCP = "";
                                det_icms51_vBCFCP = "";
                            }

                            break;

                        case "60":
                            det_icms60_orig = origemProduto.ToString().Trim().Trim();
                            det_icms60_CST = dr_ICMS["CODCST"].ToString().PadLeft(2, '0');
                            det_icms60_vBCSTRet = "";
                            det_icms60_pST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["ALIQUOTA"].ToString().Trim()));
                            det_icms60_vICMSSTRet = "";
                            det_icms60_vBCFCPSTRet = "0";//0
                            det_icms60_pFCPSTRet = "";
                            det_icms60_vFCPSTRet = "";
                            break;

                        case "70":
                            det_icms70_orig = origemProduto.ToString().Trim();
                            det_icms70_CST = dr_ICMS["CODCST"].ToString().PadLeft(2, '0');
                            det_icms70_modBC = dr_ICMS["MODALIDADEBC"].ToString().Trim();
                            det_icms70_vBC = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["BASECALCULO"].ToString().Trim()));
                            det_icms70_pICMS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["ALIQUOTA"].ToString().Trim()));
                            det_icms70_vICMS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["VALOR"].ToString().Trim()));
                            det_icms70_vBCFCP = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["BASECALCULO"].ToString().Trim()));//dr_ICMS[""].ToString();
                                                                                                                                                      /**/
                            det_icms70_pFCP = retorno_pFCP; // dr_ICMS[""].ToString();
                                                            /**/
                            det_icms70_vFCP = retorno_vFCP; // dr_ICMS[""].ToString();

                            //totalizador
                            totalizadorvFCP += String.IsNullOrEmpty(det_icms70_vFCP) ? 0 : Convert.ToDouble(det_icms70_vFCP);

                            //ajusta para vbcfcp, pfcp e vfcp
                            if (Convert.ToDouble(det_icms70_pFCP) > 0)
                            {
                                det_icms70_vFCP = (Convert.ToDouble(det_icms70_vBCFCP) * Convert.ToDouble(det_icms70_pFCP) / 100).ToString();
                            }
                            else
                            {

                                det_icms70_pFCP = "";
                                det_icms70_vFCP = "";
                                det_icms70_vBCFCP = "";
                            }

                            break;

                        case "90":
                            det_icms90_orig = origemProduto.ToString().Trim();
                            det_icms90_CST = dr_ICMS["CODCST"].ToString().PadLeft(2, '0');
                            det_icms90_modBC = dr_ICMS["MODALIDADEBC"].ToString().Trim();
                            det_icms90_vBC = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["BASECALCULO"].ToString().Trim()));
                            det_icms90_pICMS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["ALIQUOTA"].ToString().Trim()));
                            det_icms90_vICMS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["VALOR"].ToString().Trim()));
                            det_icms90_vBCFCP = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["BASECALCULO"].ToString().Trim()));//dr_ICMS[""].ToString();
                            det_icms90_vBCST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["BASECALCULO"].ToString().Trim()));
                            /**/
                            det_icms90_pFCP = retorno_pFCP; // dr_ICMS[""].ToString();
                                                            /**/
                            det_icms90_vFCP = retorno_vFCP; // dr_ICMS[""].ToString();

                            //totalizador
                            totalizadorvFCP += String.IsNullOrEmpty(det_icms90_vFCP) ? 0 : Convert.ToDouble(det_icms90_vFCP);

                            //ajusta para vbcfcp, pfcp e vfcp
                            if (det_icms90_pFCP != "")
                            {
                                if (Convert.ToDouble(det_icms90_pFCP) > 0)
                                {
                                    det_icms90_vFCP = (Convert.ToDouble(det_icms90_vBCFCP) * Convert.ToDouble(det_icms90_pFCP) / 100).ToString();
                                }
                                else
                                {

                                    det_icms90_pFCP = "";
                                    det_icms90_vFCP = "";
                                    det_icms90_vBCFCP = "";
                                }
                            }

                            //corrige para textpak
                            det_icms90_pICMSST = "0.00";
                            det_icms90_vICMSST = "0.00";
                            det_icms90_vBCFCP = "";

                            break;

                        case "0":
                            det_icmsst_orig = origemProduto.ToString().Trim();
                            det_icmsst_CST = dr_ICMS["CODCST"].ToString().PadLeft(2, '0');
                            det_icmsst_vBCSTRet = "0";// dr_ICMS[""].ToString();
                            det_icmsst_vICMSSTRet = "";// dr_ICMS[""].ToString();
                            det_icmsst_vBCSTDest = "0";// dr_ICMS[""].ToString();
                            det_icmsst_vICMSSTDest = "";// dr_ICMS[""].ToString();
                            break;


                        case "101":
                            det_icmssn101_orig = origemProduto.ToString().Trim();
                            det_icmssn101_CSOSN = dr_ICMS["CODCST"].ToString().PadLeft(2, '0');
                            det_icmssn101_pCredSN = nf_formata.trocarVirgulaPorPonto(dr_ICMS["PCREDSN"].ToString().Trim());
                            det_icmssn101_vCredICMSSN = nf_formata.trocarVirgulaPorPonto(dr_ICMS["VCREDICMSSN"].ToString().Trim());
                            break;

                        case "102":
                            det_icmssn102_orig = origemProduto.ToString().Trim();
                            det_icmssn102_CSOSN = dr_ICMS["CODCST"].ToString().PadLeft(2, '0');
                            break;

                        case "201":
                            det_icmssn201_orig = origemProduto.ToString().Trim();
                            det_icmssn201_CSOSN = dr_ICMS["CODCST"].ToString().PadLeft(2, '0');
                            det_icmssn201_modBCST = dr_ICMS["MODALIDADEBC"].ToString().Trim();
                            det_icmssn201_pMVAST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["FATORMVA"].ToString().Trim()));
                            det_icmssn201_pRedBCST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["REDUCAOBASEICMSST"].ToString().Trim()));

                            det_icmssn201_pCredSN = nf_formata.trocarVirgulaPorPonto(dr_ICMS["PCREDSN"].ToString().Trim());
                            det_icmssn201_vCredICMSSN = nf_formata.trocarVirgulaPorPonto(dr_ICMS["VCREDICMSSN"].ToString().Trim());
                            break;

                        case "202":
                            det_icmssn202_orig = origemProduto.ToString();
                            det_icmssn202_CSOSN = dr_ICMS["CODCST"].ToString().PadLeft(2, '0');

                            break;

                        case "500":
                            det_icmssn500_orig = origemProduto.ToString().Trim();
                            det_icmssn500_CSOSN = dr_ICMS["CODCST"].ToString().PadLeft(2, '0');
                            det_icmssn500_vBCSTRet = "0";
                            det_icmssn500_pST = "0.00";
                            det_icmssn500_vICMSSTRet = "0";
                            det_icmssn500_vBCFCPSTRet = "";
                            det_icmssn500_pFCPSTRet = "";
                            det_icmssn500_vFCPSTRet = "";
                            break;

                        case "900":
                            det_icmssn900_orig = origemProduto.ToString().Trim();
                            det_icmssn900_CSOSN = dr_ICMS["CODCST"].ToString().PadLeft(2, '0');
                            det_icmssn900_modBC = dr_ICMS["MODALIDADEBC"].ToString().Trim();
                            det_icmssn900_vBC = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["BASECALCULO"].ToString().Trim()));
                            det_icmssn900_pRedBC = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["REDUCAOBASEICMS"].ToString().Trim()));
                            det_icmssn900_pICMS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["ALIQUOTA"].ToString().Trim()));
                            det_icmssn900_vICMS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS["VALOR"].ToString().Trim()));

                            
                            //Dirlei - 20/08/2018
                            //novo calculo para o grupo 900
                            //para o pCredSN
                            string codNatureza = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "select codnatureza from GOPERITEM where CODOPER = ?", new object[] { CODOPER }).ToString();
                            string idRegraIcms = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "select idregraicms from VNATUREZA where CODNATUREZA = ?", new object[] { codNatureza }).ToString();
                            bool creditoIcms = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "select creditoicms from VREGRAICMS where IDREGRA = ? and codempresa = ?", new object[] { idRegraIcms, AppLib.Context.Empresa}));
                            string saida_pCredSN = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "select PCREDSN from GFILIAL where codempresa = ? and codfilial = ?", new object[] { AppLib.Context.Empresa, filial }).ToString();

                            if (creditoIcms)
                            {
                                det_icmssn900_pCredSN = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_4(saida_pCredSN));
                                /*
                                det_icmssn900_pICMSST = "0.0000";
                                det_icmssn900_vICMSST = "0.00";
                                det_icmssn900_modBCST = det_icmssn900_modBC;
                                det_icmssn900_pMVAST = "0.0000";
                                det_icmssn900_pRedBCST = "0.0000";
                                det_icmssn900_vBCST = "0.00";
                                */
                            }
                            else
                            {
                                det_icmssn900_pCredSN = "0.0000";
                                det_icmssn900_pICMSST = "0.0000";
                                det_icmssn900_vICMSST = "0.00";
                                det_icmssn900_modBCST = det_icmssn900_modBC;
                                det_icmssn900_pMVAST = "0.0000";
                                det_icmssn900_pRedBCST = "0.0000";
                                det_icmssn900_vBCST = "0.00";
                            }


                            det_icmssn900_vCredICMSSN = nf_formata.trocarVirgulaPorPonto(dr_ICMS["VCREDICMSSN"].ToString().Trim());
                            break;

                    }

                }
                #endregion




                #region TRATAMENTO DAS TAGS ICMS-ST
                /****************************************TRATAMENTO DAS TAGS ICMS-ST****************************************************/
                //TODO : Verificar query com o jerry para retirar
                sqldtICMS_ST = @"SELECT *,
                                    (SELECT CODTIPOTRIBUTO FROM VTRIBUTO WHERE CODTRIBUTO = GOPERITEMTRIBUTO.CODTRIBUTO AND CODEMPRESA = GOPERITEMTRIBUTO.CODEMPRESA) CODTIPOTRIBUTO FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?  AND CODTRIBUTO = 'ICMS-ST' ORDER BY CODCST";
                dtICMS_ST = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqldtICMS_ST, new object[] { AppLib.Context.Empresa, CODOPER, dr["NSEQITEM"].ToString().Trim() });

                foreach (DataRow dr_ICMS_ST in dtICMS_ST.Rows)
                {

                    //Preenche a TAG pFCP
                    //sqlpFCP = @"SELECT VBCFCPUFDEST, PFCPUFDEST, VFCPUFDEST, VBCFCPSTUFDEST, PFCPSTUFDEST, VFCPSTUFDEST FROM GOPERITEMDIFAL WHERE CODEMPRESA=? AND CODOPER=? AND NSEQITEM=?";
                    sqlpFCP = @"SELECT *,
                                    (SELECT CODTIPOTRIBUTO FROM VTRIBUTO WHERE CODTRIBUTO = GOPERITEMTRIBUTO.CODTRIBUTO AND CODEMPRESA = GOPERITEMTRIBUTO.CODEMPRESA) CODTIPOTRIBUTO FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?  AND CODTRIBUTO = 'ICMS-ST' ORDER BY CODCST";

                    dtICMS_preenche_fc_ST = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlpFCP, new object[] { AppLib.Context.Empresa, CODOPER, dr["NSEQITEM"].ToString().Trim() });
                    if (dtICMS_preenche_fc_ST.Rows.Count > 0)
                    {

                        retorno_pFCPST = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT isnull(VREGRAVARCFOP.PFCPST,0) as PFCPST FROM VREGRAVARCFOP WHERE VREGRAVARCFOP.NCM = ? AND VREGRAVARCFOP.UFDESTINO = ?", new object[] { det_NCM.Trim(), Ufdestino }).ToString().Trim();
                        retorno_pFCPST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(retorno_pFCPST));

                        retorno_vBCFCPST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtICMS_preenche_fc_ST.Rows[0]["BASECALCULO"].ToString().Trim()));
                        //retorno_vFCPST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2((Convert.ToDouble(retorno_pFCPST) * Convert.ToDouble(retorno_vBCFCPST)).ToString()));
                    }
                    else
                    {
                        retorno_pFCPST = "";
                        retorno_vBCFCPST = "";
                        retorno_vFCPST = "";
                    }

                    //teste exclusivo
                    sqlCST_exclusivo_cst = @"SELECT CODCST FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?  AND CODTRIBUTO = 'ICMS' ORDER BY CODCST";
                    verificaCstExclusivo = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, sqlCST_exclusivo_cst, new object[] { AppLib.Context.Empresa, CODOPER, dr["NSEQITEM"].ToString().Trim() }).ToString().Trim();

                    switch (verificaCstExclusivo.Trim())
                    {

                        case "10":
                            det_icms10_modBCST = dr_ICMS_ST["MODALIDADEBC"].ToString();
                            det_icms10_pMVAST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["FATORMVA"].ToString().Trim()));
                            det_icms10_pRedBCST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["REDUCAOBASEICMSST"].ToString().Trim()));
                            det_icms10_vBCST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["BASECALCULO"].ToString().Trim()));
                            det_icms10_pICMSST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["ALIQUOTA"].ToString().Trim()));
                            det_icms10_vICMSST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["VALORICMSST"].ToString().Trim()));
                            det_icms10_vBCFCPST = retorno_vBCFCPST;
                            det_icms10_pFCPST = retorno_pFCPST;
                            //det_icms10_vFCPST = retorno_vFCPST;

                            //ajusta para vbcfcp, pfcp e vfcp
                            if (Convert.ToDouble(det_icms10_pFCPST) > 0)
                            {
                                det_icms10_vFCPST = (Convert.ToDouble(det_icms10_vBCFCPST) * Convert.ToDouble(det_icms10_pFCPST) / 100).ToString();
                            }
                            else
                            {

                                det_icms10_pFCPST = "";
                                det_icms10_vFCPST = "";
                                det_icms10_vBCFCPST = "";
                            }

                            //totalizador
                            totalizadorvFCP += String.IsNullOrEmpty(det_icms10_vFCPST) ? 0 : Convert.ToDouble(det_icms10_vFCPST);

                            break;


                        case "30":
                            //ST
                            det_icms30_modBCST = dr_ICMS_ST["MODALIDADEBC"].ToString().Trim();
                            det_icms30_vBCST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["BCORIGINAL"].ToString().Trim()));
                            det_icms30_pICMSST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["ALIQUOTA"].ToString().Trim()));
                            det_icms30_vICMSST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["VALOR"].ToString().Trim()));
                            det_icms30_vBCFCPST = retorno_vBCFCPST;
                            det_icms30_pFCPST = retorno_pFCPST;
                            det_icms30_vFCPST = retorno_vFCPST;

                            //totalizador
                            totalizadorvFCP += String.IsNullOrEmpty(det_icms30_vFCPST) ? 0 : Convert.ToDouble(det_icms30_vFCPST);

                            break;

                        case "60":
                            det_icms60_CST = verificaCstExclusivo.ToString().PadLeft(2, '0');
                            det_icms60_vBCSTRet = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["BASECALCULO"].ToString().Trim()));
                            det_icms60_pST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["ALIQUOTA"].ToString().Trim()));
                            det_icms60_vICMSSTRet = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["VALOR"].ToString().Trim()));
                            det_icms60_vBCFCPSTRet = retorno_vBCFCPST;
                            det_icms60_pFCPSTRet = retorno_pFCPST;
                            det_icms60_vFCPSTRet = "";



                            //ajusta para vbcfcp, pfcp e vfcp
                            if (det_icms60_pFCPSTRet != "")
                            {

                                if (Convert.ToDouble(det_icms60_pFCPSTRet) > 0)
                                {
                                    det_icms60_vFCPSTRet = (Convert.ToDouble(det_icms60_vBCFCPSTRet) * Convert.ToDouble(det_icms60_pFCPSTRet) / 100).ToString();
                                }
                                else
                                {

                                    det_icms60_vBCFCPSTRet = "";
                                    det_icms60_pFCPSTRet = "";
                                    det_icms60_vFCPSTRet = "";
                                }
                            }

                            //totalizador
                            totalizadorvFCP += String.IsNullOrEmpty(det_icms60_vFCPSTRet) ? 0 : Convert.ToDouble(det_icms60_vFCPSTRet);


                            break;


                        case "70":
                            //ST
                            det_icms70_modBCST = dr_ICMS_ST["MODALIDADEBC"].ToString().Trim();
                            det_icms70_pMVAST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["FATORMVA"].ToString().Trim()));
                            det_icms70_pRedBCST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["REDUCAOBASEICMSST"].ToString().Trim()));
                            det_icms70_vBCST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["BCORIGINAL"].ToString().Trim()));
                            det_icms70_pICMSST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["ALIQUOTA"].ToString().Trim()));
                            det_icms70_vICMSST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["VALORICMSST"].ToString().Trim()));
                            det_icms70_vBCFCPST = retorno_vBCFCPST;
                            det_icms70_pFCPST = retorno_pFCPST;
                            det_icms70_vFCPST = retorno_vFCPST;


                            //ajusta para vbcfcp, pfcp e vfcp
                            if (Convert.ToDouble(det_icms70_pFCPST) > 0)
                            {
                                det_icms70_vFCPST = (Convert.ToDouble(det_icms70_vBCFCPST) * Convert.ToDouble(det_icms70_pFCPST) / 100).ToString();
                            }
                            else
                            {

                                det_icms70_pFCPST = "";
                                det_icms70_vFCPST = "";
                                det_icms70_vBCFCPST = "";
                            }

                            //totalizador
                            totalizadorvFCP += String.IsNullOrEmpty(det_icms70_vFCPST) ? 0 : Convert.ToDouble(det_icms70_vFCPST);


                            break;

                        case "90":
                            //ST
                            det_icms90_modBCST = dr_ICMS_ST["MODALIDADEBC"].ToString().Trim();
                            det_icms90_pMVAST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["FATORMVA"].ToString().Trim()));
                            det_icms90_pRedBCST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["REDUCAOBASEICMSST"].ToString().Trim()));
                            det_icms90_vBCST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["BCORIGINAL"].ToString().Trim()));
                            det_icms90_pICMSST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["ALIQUOTA"].ToString().Trim()));
                            det_icms90_vICMSST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["VALORICMSST"].ToString().Trim()));
                            det_icms90_vBCFCPST = retorno_vBCFCPST;
                            det_icms90_pFCPST = retorno_pFCPST;
                            det_icms90_vFCPST = retorno_vFCPST;

                            //ajusta para vbcfcp, pfcp e vfcp
                            if (Convert.ToDouble(det_icms90_pFCPST) > 0)
                            {
                                det_icms90_vFCPST = (Convert.ToDouble(det_icms90_vBCFCP) * Convert.ToDouble(det_icms90_pFCPST) / 100).ToString();
                            }
                            else
                            {

                                det_icms90_pFCPST = "";
                                det_icms90_vFCPST = "";
                                det_icms90_vBCFCPST = "";
                            }
                            break;





                            //totalizador
                            totalizadorvFCP += String.IsNullOrEmpty(det_icms90_vFCPST) ? 0 : Convert.ToDouble(det_icms90_vFCPST);


                        case "201":
                            //ST
                            det_icmssn201_vBCST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["BCORIGINAL"].ToString().Trim()));
                            det_icmssn201_pICMSST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["ALIQUOTA"].ToString().Trim()));
                            det_icmssn201_vICMSST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["VALORICMSST"].ToString().Trim()));
                            det_icmssn201_vBCFCPST = retorno_vBCFCPST;
                            det_icmssn201_pFCPST = retorno_pFCPST;
                            det_icmssn201_vFCPST = retorno_vFCPST;

                            if (Convert.ToDouble(det_icmssn201_pFCPST) > 0)
                            {
                                det_icmssn201_vFCPST = (Convert.ToDouble(det_icmssn201_vBCFCPST) * Convert.ToDouble(det_icmssn201_pFCPST) / 100).ToString();
                            }
                            else
                            {

                                det_icmssn201_pFCPST = "";
                                det_icmssn201_vFCPST = "";
                                det_icmssn201_vBCFCPST = "";
                            }

                            //totalizador
                            totalizadorvFCP += String.IsNullOrEmpty(det_icmssn201_vFCPST) ? 0 : Convert.ToDouble(det_icmssn201_vFCPST);

                            break;

                        case "202":
                            //ST
                            det_icmssn202_modBCST = dr_ICMS_ST["MODALIDADEBC"].ToString().Trim();
                            det_icmssn202_pMVAST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["FATORMVA"].ToString().Trim()));
                            det_icmssn202_pRedBCST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["REDUCAOBASEICMSST"].ToString().Trim()));
                            det_icmssn202_vBCST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["BCORIGINAL"].ToString().Trim()));
                            det_icmssn202_pICMSST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["ALIQUOTA"].ToString().Trim()));
                            det_icmssn202_vICMSST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["VALORICMSST"].ToString().Trim()));
                            det_icmssn202_vBCFCPST = retorno_vBCFCPST;
                            det_icmssn202_pFCPST = retorno_pFCPST;
                            det_icmssn202_vFCPST = retorno_vFCPST;

                            if (Convert.ToDouble(det_icmssn202_pFCPST) > 0)
                            {
                                det_icmssn202_vFCPST = (Convert.ToDouble(det_icmssn202_vBCFCPST) * Convert.ToDouble(det_icmssn202_pFCPST) / 100).ToString();
                            }
                            else
                            {

                                det_icmssn202_pFCPST = "";
                                det_icmssn202_vFCPST = "";
                                det_icmssn202_vBCFCPST = "";
                            }

                            //totalizador
                            totalizadorvFCP += String.IsNullOrEmpty(det_icmssn202_vFCPST) ? 0 : Convert.ToDouble(det_icmssn202_vFCPST);


                            break;

                        case "500":
                            //ST
                            det_icmssn500_pST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["ALIQUOTA"].ToString().Trim()));
                            break;

                        case "900":
                            //ST
                            det_icmssn900_modBCST = dr_ICMS_ST["MODALIDADEBC"].ToString().Trim();
                            det_icmssn900_pMVAST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["FATORMVA"].ToString().Trim()));
                            det_icmssn900_pRedBCST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["REDUCAOBASEICMSST"].ToString().Trim()));
                            det_icmssn900_vBCST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["BCORIGINAL"].ToString().Trim()));
                            det_icmssn900_pICMSST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["ALIQUOTA"].ToString().Trim()));
                            det_icmssn900_vICMSST = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_ICMS_ST["VALORICMSST"].ToString().Trim()));
                            det_icmssn900_vBCFCPST = retorno_vBCFCPST;
                            det_icmssn900_pFCPST = retorno_pFCPST;
                            det_icmssn900_vFCPST = retorno_vFCPST;

                            if (Convert.ToDouble(det_icmssn900_pFCPST) > 0)
                            {
                                det_icmssn900_vFCPST = (Convert.ToDouble(det_icmssn900_vBCFCPST) * Convert.ToDouble(det_icmssn900_pFCPST) / 100).ToString();
                            }
                            else
                            {

                                det_icmssn900_pFCPST = "";
                                det_icmssn900_vFCPST = "";
                                det_icmssn900_vBCFCPST = "";
                            }

                            //totalizador
                            totalizadorvFCP += String.IsNullOrEmpty(det_icmssn900_vFCPST) ? 0 : Convert.ToDouble(det_icmssn900_vFCPST);


                            break;

                    }

                }
                #endregion





                sb.Append("					<ICMS00>\n");
                sb.Append("						<orig>" + det_icms00_orig + "</orig>\n");
                sb.Append("						<CST>" + det_icms00_CST + "</CST>\n");
                sb.Append("						<modBC>" + det_icms00_modBC + "</modBC>\n");
                sb.Append("						<vBC>" + det_icms00_vBC + "</vBC>\n");
                sb.Append("						<pICMS>" + det_icms00_pICMS + "</pICMS>\n");
                sb.Append("						<vICMS>" + det_icms00_vICMS + "</vICMS>\n");
                // sb.Append("						<vBCFCP>" + det_icms00_vBC + "</vBCFCP>\n");
                // sb.Append("						<pFCP>"+ det_icms00_pFCP +"</pFCP>\n");
                // sb.Append("						<vFCP>"+ det_icms00_vFCP + "</vFCP>\n");
                sb.Append("					</ICMS00>\n");



                sb.Append("					<ICMS10>\n");
                sb.Append("						<orig>" + det_icms10_orig + "</orig>\n");
                sb.Append("						<CST>" + det_icms10_CST + "</CST>\n");
                sb.Append("						<modBC>" + det_icms10_modBC + "</modBC>\n");
                sb.Append("						<vBC>" + det_icms10_vBC + "</vBC>\n");
                sb.Append("						<pICMS>" + det_icms10_pICMS + "</pICMS>\n");
                sb.Append("						<vICMS>" + det_icms10_vICMS + "</vICMS>\n");
                sb.Append("						<vBCFCP>" + det_icms10_vBCFCP + "</vBCFCP>\n");
                sb.Append("						<pFCP>" + det_icms10_pFCP + "</pFCP>\n");
                sb.Append("						<vFCP>" + det_icms10_vFCP + "</vFCP>\n");
                sb.Append("						<modBCST>" + det_icms10_modBCST + "</modBCST>\n");
                sb.Append("						<pMVAST>" + det_icms10_pMVAST + "</pMVAST>\n");
                sb.Append("						<pRedBCST>" + det_icms10_pRedBCST + "</pRedBCST>\n");
                sb.Append("						<vBCST>" + det_icms10_vBCST + "</vBCST>\n");
                sb.Append("						<pICMSST>" + det_icms10_pICMSST + "</pICMSST>\n");
                sb.Append("						<vICMSST>" + det_icms10_vICMSST + "</vICMSST>\n");
                sb.Append("						<vBCFCPST>" + det_icms10_vBCFCPST + "</vBCFCPST>\n");
                sb.Append("						<pFCPST>" + det_icms10_pFCPST + "</pFCPST>\n");
                sb.Append("						<vFCPST>" + det_icms10_vFCPST + "</vFCPST>\n");
                sb.Append("					</ICMS10>\n");


                sb.Append("					<ICMS20>\n");
                sb.Append("						<orig>" + det_icms20_orig + "</orig>\n");
                sb.Append("						<CST>" + det_icms20_CST + "</CST>\n");
                sb.Append("						<modBC>" + det_icms20_modBC + "</modBC>\n");
                sb.Append("						<pRedBC>" + det_icms20_pRedBC + "</pRedBC>\n");
                sb.Append("						<vBC>" + det_icms20_vBC + "</vBC>\n");
                sb.Append("						<pICMS>" + det_icms20_pICMS + "</pICMS>\n");
                sb.Append("						<vICMS>" + det_icms20_vICMS + "</vICMS>\n");
                sb.Append("						<vBCFCP>" + det_icms20_vBCFCP + "</vBCFCP>\n");
                sb.Append("						<pFCP>" + det_icms20_pFCP + "</pFCP>\n");
                sb.Append("						<vFCP>" + det_icms20_vFCP + "</vFCP>\n");


                sb.Append("						<vICMSDeson>" + det_icms20_vICMSDeson + "</vICMSDeson>\n");
                sb.Append("						<motDesICMS>" + det_icms20_motDesICMS + "</motDesICMS>\n");
                sb.Append("					</ICMS20>\n");


                sb.Append("					<ICMS30>\n");
                sb.Append("						<orig>" + det_icms30_orig + "</orig>\n");
                sb.Append("						<CST>" + det_icms30_CST + "</CST>\n");
                sb.Append("						<modBCST>" + det_icms30_modBCST + "</modBCST>\n");
                //sb.Append("						<pMVAST>" + det_icms30_ + "</pMVAST>\n");
                //sb.Append("						<pRedBCST>" + det_icms30_ + "</pRedBCST>\n");
                sb.Append("						<vBCST>" + det_icms30_vBCST + "</vBCST>\n");
                sb.Append("						<pICMSST>" + det_icms30_pICMSST + "</pICMSST>\n");
                sb.Append("						<vICMSST>" + det_icms30_vICMSST + "</vICMSST>\n");
                sb.Append("						<vBCFCPST>" + det_icms30_vBCFCPST + "</vBCFCPST>\n");
                sb.Append("						<pFCPST>" + det_icms30_pFCPST + "</pFCPST>\n");
                sb.Append("						<vFCPST>" + det_icms30_vFCPST + "</vFCPST>\n");
                sb.Append("						<vICMSDeson>" + det_icms30_vICMSDeson + "</vICMSDeson>\n");
                sb.Append("						<motDesICMS>" + det_icms30_motDesICMS + "</motDesICMS>\n");
                sb.Append("					</ICMS30>\n");


                sb.Append("					<ICMS40>\n");
                sb.Append("						<orig>" + det_icms40_orig + "</orig>\n");
                sb.Append("						<CST>" + det_icms40_CST + "</CST>\n");
                sb.Append("						<vICMSDeson>" + det_icms40_vICMSDeson + "</vICMSDeson>\n");
                sb.Append("						<motDesICMS>" + det_icms40_motDesICMS + "</motDesICMS>\n");
                sb.Append("					</ICMS40>\n");


                sb.Append("					<ICMS51>\n");
                sb.Append("						<orig>" + det_icms51_orig + "</orig>\n");
                sb.Append("						<CST>" + det_icms51_CST + "</CST>\n");
                sb.Append("						<modBC>" + det_icms51_modBC + "</modBC>\n");
                sb.Append("						<pRedBC>" + det_icms51_pRedBCST + "</pRedBC>\n");
                sb.Append("						<vBC>" + det_icms51_vBC + "</vBC>\n");
                sb.Append("						<pICMS>" + det_icms51_pICMS + "</pICMS>\n");
                sb.Append("						<vICMSOp>" + det_icms51_vICMSOp + "</vICMSOp>\n");
                sb.Append("						<pDif>" + det_icms51_pDif + "</pDif>\n");
                sb.Append("						<vICMSDif>" + det_icms51_vICMSDif + "</vICMSDif>\n");
                sb.Append("						<vICMS>" + det_icms51_vICMS + "</vICMS>\n");
                sb.Append("						<vBCFCP>" + det_icms51_vBCFCP + "</vBCFCP>\n");
                sb.Append("						<pFCP>" + det_icms51_pFCP + "</pFCP>\n");
                sb.Append("						<vFCP>" + det_icms51_vFCP + "</vFCP>\n");
                sb.Append("					</ICMS51>\n");

                sb.Append("					<ICMS60>\n");
                sb.Append("						<orig>" + det_icms60_orig + "</orig>\n");
                sb.Append("						<CST>" + det_icms60_CST + "</CST>\n");
                sb.Append("						<vBCSTRet>" + det_icms60_vBCSTRet + "</vBCSTRet>\n");
                sb.Append("						<pST>" + det_icms60_pST + "</pST>\n");
                sb.Append("						<vICMSSTRet>" + det_icms60_vICMSSTRet + "</vICMSSTRet>\n");
                sb.Append("						<vBCFCPSTRet>" + det_icms60_vBCFCPSTRet + "</vBCFCPSTRet>\n");
                sb.Append("						<pFCPSTRet>" + det_icms60_pFCPSTRet + "</pFCPSTRet>\n");
                sb.Append("						<vFCPSTRet>" + det_icms60_vFCPSTRet + "</vFCPSTRet>\n");
                sb.Append("					</ICMS60>\n");


                sb.Append("					<ICMS70>\n");
                sb.Append("						<orig>" + det_icms70_orig + "</orig>\n");
                sb.Append("						<CST>" + det_icms70_CST + "</CST>\n");
                sb.Append("						<modBC>" + det_icms70_modBC + "</modBC>\n");
                sb.Append("						<pRedBC>" + det_icms70_pRedBC + "</pRedBC>\n");
                sb.Append("						<vBC>" + det_icms70_vBC + "</vBC>\n");
                sb.Append("						<pICMS>" + det_icms70_pICMS + "</pICMS>\n");
                sb.Append("						<vICMS>" + det_icms70_vICMS + "</vICMS>\n");
                sb.Append("						<vBCFCP>" + det_icms70_vBCFCP + "</vBCFCP>\n");
                sb.Append("						<pFCP>" + det_icms70_pFCP + "</pFCP>\n");
                sb.Append("						<vFCP>" + det_icms70_vFCP + "</vFCP>\n");
                sb.Append("						<modBCST>" + det_icms70_modBCST + "</modBCST>\n");
                sb.Append("						<pMVAST>" + det_icms70_pMVAST + "</pMVAST>\n");
                sb.Append("						<pRedBCST>" + det_icms70_pRedBC + "</pRedBCST>\n");
                sb.Append("						<vBCST>" + det_icms70_vBCST + "</vBCST>\n");
                sb.Append("						<pICMSST>" + det_icms70_pICMSST + "</pICMSST>\n");
                sb.Append("						<vICMSST>" + det_icms70_vICMSST + "</vICMSST>\n");
                sb.Append("						<vBCFCPST>" + det_icms70_vBCFCPST + "</vBCFCPST>\n");
                sb.Append("						<pFCPST>" + det_icms70_pFCPST + "</pFCPST>\n");
                sb.Append("						<vFCPST>" + det_icms70_vFCPST + "</vFCPST>\n");
                sb.Append("						<vICMSDeson>" + det_icms70_vICMSDeson + "</vICMSDeson>\n");
                sb.Append("						<motDesICMS>" + det_icms70_motDesICMS + "</motDesICMS>\n");
                sb.Append("					</ICMS70>\n");

                sb.Append("					<ICMS90>\n");
                sb.Append("						<orig>" + det_icms90_orig + "</orig>\n");
                sb.Append("						<CST>" + det_icms90_CST + "</CST>\n");
                sb.Append("						<modBC>" + det_icms90_modBC + "</modBC>\n");
                sb.Append("						<vBC>" + det_icms90_vBC + "</vBC>\n");
                sb.Append("						<pRedBC>" + det_icms90_pRedBC + "</pRedBC>\n");
                sb.Append("						<pICMS>" + det_icms90_pICMS + "</pICMS>\n");
                sb.Append("						<vICMS>" + det_icms90_vICMS + "</vICMS>\n");
                sb.Append("						<vBCFCP>" + det_icms90_vBCFCP + "</vBCFCP>\n");
                sb.Append("						<pFCP>" + det_icms90_pFCP + "</pFCP>\n");
                sb.Append("						<vFCP>" + det_icms90_vFCP + "</vFCP>\n");
                sb.Append("						<modBCST>" + det_icms90_modBC + "</modBCST>\n");
                sb.Append("						<pMVAST>" + det_icms90_pMVAST + "</pMVAST>\n");
                sb.Append("						<pRedBCST>" + det_icms90_pRedBCST + "</pRedBCST>\n");
                sb.Append("						<vBCST>" + det_icms90_vBCST + "</vBCST>\n");
                sb.Append("						<pICMSST>" + det_icms90_pICMSST + "</pICMSST>\n");
                sb.Append("						<vICMSST>" + det_icms90_vICMSST + "</vICMSST>\n");
                sb.Append("						<vBCFCPST>" + det_icms90_vBCFCPST + "</vBCFCPST>\n");
                sb.Append("						<pFCPST>" + det_icms90_pFCPST + "</pFCPST>\n");
                sb.Append("						<vFCPST>" + det_icms90_vFCPST + "</vFCPST>\n");
                sb.Append("						<vICMSDeson>" + det_icms90_vICMSDeson + "</vICMSDeson>\n");
                sb.Append("						<motDesICMS>" + det_icms90_motDesICMS + "</motDesICMS>\n");
                sb.Append("					</ICMS90>\n");


                /*
                sb.Append("					<ICMSPart>\n");
                sb.Append("						<orig></orig>\n");
                sb.Append("						<CST></CST>\n");
                sb.Append("						<modBC></modBC>\n");
                sb.Append("						<vBC></vBC>\n");
                sb.Append("						<pRedBC></pRedBC>\n");
                sb.Append("						<pICMS></pICMS>\n");
                sb.Append("						<vICMS></vICMS>\n");
                sb.Append("						<modBCST></modBCST>\n");
                sb.Append("						<pMVAST></pMVAST>\n");
                sb.Append("						<pRedBCST></pRedBCST>\n");
                sb.Append("						<vBCST></vBCST>\n");
                sb.Append("						<pICMSST></pICMSST>\n");
                sb.Append("						<vICMSST></vICMSST>\n");
                sb.Append("						<pBCOp></pBCOp>\n");
                sb.Append("						<UFST></UFST>\n");
                sb.Append("					</ICMSPart>\n");
                */


                sb.Append("					<ICMSST>\n");
                sb.Append("						<orig>" + det_icmsst_orig + "</orig>\n");
                sb.Append("						<CST>" + det_icmsst_CST + "</CST>\n");
                sb.Append("						<vBCSTRet>" + det_icmsst_vBCSTRet + "</vBCSTRet>\n");
                sb.Append("						<vICMSSTRet>" + det_icmsst_vICMSSTRet + "</vICMSSTRet>\n");
                sb.Append("						<vBCSTDest>" + det_icmsst_vBCSTDest + "</vBCSTDest>\n");
                sb.Append("						<vICMSSTDest>" + det_icmsst_vICMSSTDest + "</vICMSSTDest>\n");
                sb.Append("					</ICMSST>\n");


                sb.Append("					<ICMSSN101>\n");
                sb.Append("						<orig>" + det_icmssn101_orig + "</orig>\n");
                sb.Append("						<CSOSN>" + det_icmssn101_CSOSN + "</CSOSN>\n");
                sb.Append("						<pCredSN>" + det_icmssn101_pCredSN + "</pCredSN>\n");
                sb.Append("						<vCredICMSSN>" + det_icmssn101_vCredICMSSN + "</vCredICMSSN>\n");
                sb.Append("					</ICMSSN101>\n");


                sb.Append("					<ICMSSN102>\n");
                sb.Append("						<orig>" + det_icmssn102_orig + "</orig>\n");
                sb.Append("						<CSOSN>" + det_icmssn102_CSOSN + "</CSOSN>\n");
                sb.Append("					</ICMSSN102>\n");


                sb.Append("					<ICMSSN201>\n");
                sb.Append("						<orig>" + det_icmssn201_orig + "</orig>\n");
                sb.Append("						<CSOSN>" + det_icmssn201_CSOSN + "</CSOSN>\n");
                sb.Append("						<modBCST>" + det_icmssn201_modBCST + "</modBCST>\n");
                sb.Append("						<pMVAST>" + det_icmssn201_pMVAST + "</pMVAST>\n");
                sb.Append("						<pRedBCST>" + det_icmssn201_pRedBCST + "</pRedBCST>\n");
                sb.Append("						<vBCST>" + det_icmssn201_vBCST + "</vBCST>\n");
                sb.Append("						<pICMSST>" + det_icmssn201_pICMSST + "</pICMSST>\n");
                sb.Append("						<vICMSST>" + det_icmssn201_vICMSST + "</vICMSST>\n");
                sb.Append("						<vBCFCPST>" + det_icmssn201_vBCFCPST + "</vBCFCPST>\n");
                sb.Append("						<pFCPST>" + det_icmssn201_pFCPST + "</pFCPST>\n");
                sb.Append("						<vFCPST>" + det_icmssn201_vFCPST + "</vFCPST>\n");
                sb.Append("						<pCredSN>" + det_icmssn201_pCredSN + "</pCredSN>\n");
                sb.Append("						<vCredICMSSN>" + det_icmssn201_vCredICMSSN + "</vCredICMSSN>\n");
                sb.Append("					</ICMSSN201>\n");


                sb.Append("					<ICMSSN202>\n");
                sb.Append("						<orig>" + det_icmssn202_orig + "</orig>\n");
                sb.Append("						<CSOSN>" + det_icmssn202_CSOSN + "</CSOSN>\n");
                sb.Append("						<modBCST>" + det_icmssn202_modBCST + "</modBCST>\n");
                sb.Append("						<pMVAST>" + det_icmssn202_pMVAST + "</pMVAST>\n");
                sb.Append("						<pRedBCST>" + det_icmssn202_pRedBCST + "</pRedBCST>\n");
                sb.Append("						<vBCST>" + det_icmssn202_vBCST + "</vBCST>\n");
                sb.Append("						<pICMSST>" + det_icmssn202_pICMSST + "</pICMSST>\n");
                sb.Append("						<vICMSST>" + det_icmssn202_vICMSST + "</vICMSST>\n");
                sb.Append("						<vBCFCPST>" + det_icmssn202_vBCFCPST + "</vBCFCPST>\n");
                sb.Append("						<pFCPST>" + det_icmssn202_pFCPST + "</pFCPST>\n");
                sb.Append("						<vFCPST>" + det_icmssn202_vFCPST + "</vFCPST>\n");
                sb.Append("					</ICMSSN202>\n");

                sb.Append("					<ICMSSN500>\n");
                sb.Append("						<orig>" + det_icmssn500_orig + "</orig>\n");
                sb.Append("						<CSOSN>" + det_icmssn500_CSOSN + "</CSOSN>\n");
                sb.Append("						<vBCSTRet>" + det_icmssn500_vBCSTRet + "</vBCSTRet>\n");
                sb.Append("						<pST>" + det_icmssn500_pST + "</pST>\n");
                sb.Append("						<vICMSSTRet>" + det_icmssn500_vICMSSTRet + "</vICMSSTRet>\n");
                sb.Append("						<vBCFCPSTRet>" + det_icmssn500_vBCFCPSTRet + "</vBCFCPSTRet>\n");
                sb.Append("						<pFCPSTRet>" + det_icmssn500_pFCPSTRet + "</pFCPSTRet>\n");
                sb.Append("						<vFCPSTRet>" + det_icmssn500_vFCPSTRet + "</vFCPSTRet>\n");
                sb.Append("					</ICMSSN500>\n");


                sb.Append("					<ICMSSN900>\n");
                sb.Append("						<orig>" + det_icmssn900_orig + "</orig>\n");
                sb.Append("						<CSOSN>" + det_icmssn900_CSOSN + "</CSOSN>\n");
                sb.Append("						<modBC>" + det_icmssn900_modBC + "</modBC>\n");
                sb.Append("						<vBC>" + det_icmssn900_vBC + "</vBC>\n");
                sb.Append("						<pRedBC>" + det_icmssn900_pRedBC + "</pRedBC>\n");
                sb.Append("						<pICMS>" + det_icmssn900_pICMS + "</pICMS>\n");
                sb.Append("						<vICMS>" + det_icmssn900_vICMS + "</vICMS>\n");
                sb.Append("						<modBCST>" + det_icmssn900_modBCST + "</modBCST>\n");
                sb.Append("						<pMVAST>" + det_icmssn900_pMVAST + "</pMVAST>\n");
                sb.Append("						<pRedBCST>" + det_icmssn900_pRedBCST + "</pRedBCST>\n");
                sb.Append("						<vBCST>" + det_icmssn900_vBCST + "</vBCST>\n");
                sb.Append("						<pICMSST>" + det_icmssn900_pICMSST + "</pICMSST>\n");
                sb.Append("						<vICMSST>" + det_icmssn900_vICMSST + "</vICMSST>\n");
                sb.Append("						<vBCFCPST>" + det_icmssn900_vBCFCPST + "</vBCFCPST>\n");
                sb.Append("						<pFCPST>" + det_icmssn900_pFCPST + "</pFCPST>\n");
                sb.Append("						<vFCPST>" + det_icmssn900_vFCPST + "</vFCPST>\n");
                sb.Append("						<pCredSN>" + det_icmssn900_pCredSN + "</pCredSN>\n");
                sb.Append("						<vCredICMSSN>" + det_icmssn900_vCredICMSSN + "</vCredICMSSN>\n");
                sb.Append("					</ICMSSN900>\n");

                //TAG ICMSUFDest
                dtDadosProdutoICMSUFDest = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT VBCUFDEST,PFCPUFDEST,PICMSUFDEST,PICMSINTER,PICMSINTERPART,VFCPUFDEST,VICMSUFDEST,VICMSUFREMET FROM GOPERITEMDIFAL WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?", new object[] { AppLib.Context.Empresa, CODOPER, dr["NSEQITEM"].ToString() });
                if (dtDadosProdutoICMSUFDest.Rows.Count > 0)
                {

                    det_icmsufdest_vBCUFDest = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtDadosProdutoICMSUFDest.Rows[0]["VBCUFDEST"].ToString().Trim()));
                    det_icmsufdest_vBCFCPUFDest = "";
                    det_icmsufdest_pFCPUFDest = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtDadosProdutoICMSUFDest.Rows[0]["PFCPUFDEST"].ToString().Trim()));
                    det_icmsufdest_pICMSUFDest = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtDadosProdutoICMSUFDest.Rows[0]["PICMSUFDEST"].ToString().Trim()));
                    det_icmsufdest_pICMSInter = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtDadosProdutoICMSUFDest.Rows[0]["PICMSINTER"].ToString().Trim()));
                    det_icmsufdest_pICMSInterPart = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtDadosProdutoICMSUFDest.Rows[0]["PICMSINTERPART"].ToString().Trim()));
                    det_icmsufdest_vFCPUFDest = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtDadosProdutoICMSUFDest.Rows[0]["VFCPUFDEST"].ToString().Trim()));
                    det_icmsufdest_vICMSUFDest = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtDadosProdutoICMSUFDest.Rows[0]["VICMSUFDEST"].ToString().Trim()));
                    det_icmsufdest_vICMSUFRemet = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtDadosProdutoICMSUFDest.Rows[0]["VICMSUFREMET"].ToString().Trim()));

                    /*
                    det_icmsufdest_vBCUFDest = "0";
                    det_icmsufdest_vBCFCPUFDest = "";
                    det_icmsufdest_pFCPUFDest = "0";
                    det_icmsufdest_pICMSUFDest = "0";
                    det_icmsufdest_pICMSInter = "0";
                    det_icmsufdest_pICMSInterPart = "0";
                    det_icmsufdest_vFCPUFDest = "0";
                    det_icmsufdest_vICMSUFDest = "0";
                    det_icmsufdest_vICMSUFRemet = "0";
                    */

                }


                /*
                sb.Append("					<ICMSUFDest>\n");
                sb.Append("						<vBCUFDest>"+ det_icmsufdest_vBCUFDest + "</vBCUFDest>\n");
                sb.Append("						<vBCFCPUFDest>"+ det_icmsufdest_vBCFCPUFDest + "</vBCFCPUFDest>\n");
                sb.Append("						<pFCPUFDest>"+ det_icmsufdest_pFCPUFDest + "</pFCPUFDest>\n");
                sb.Append("						<pICMSUFDest>"+ det_icmsufdest_pICMSUFDest + "</pICMSUFDest>\n");
                sb.Append("						<pICMSInter>"+ det_icmsufdest_pICMSInter + "</pICMSInter>\n");
                sb.Append("						<pICMSInterPart>"+ det_icmsufdest_pICMSInterPart + "</pICMSInterPart>\n");
                sb.Append("						<vFCPUFDest>"+ det_icmsufdest_vFCPUFDest + "</vFCPUFDest>\n");
                sb.Append("						<vICMSUFDest>"+ det_icmsufdest_vICMSUFDest + "</vICMSUFDest>\n");
                sb.Append("						<vICMSUFRemet>"+ det_icmsufdest_vICMSUFRemet + "</vICMSUFRemet>\n");
                sb.Append("					</ICMSUFDest>\n");
               */
                /*
                 sb.Append("					<ICMSUFDest>\n");
                     sb.Append("					<vBCUFDest>22.50</vBCUFDest>\n");
                     sb.Append("					<pFCPUFDest>0.00</pFCPUFDest>\n");
                     sb.Append("					<pICMSUFDest>18.00</pICMSUFDest>\n");
                     sb.Append("					<pICMSInter>12.00</pICMSInter>\n");
                     sb.Append("					<pICMSInterPart>80.00</pICMSInterPart>\n");
                     sb.Append("					<vFCPUFDest>0.00</vFCPUFDest>\n");
                     sb.Append("					<vICMSUFDest>1.08</vICMSUFDest>\n");
                     sb.Append("					<vICMSUFRemet>0.27</vICMSUFRemet>\n");
                 sb.Append("					</ICMSUFDest>\n");
                 */

                sb.Append("				</ICMS>\n");

                sb.Append("					<ICMSUFDest>\n");
                sb.Append("						<vBCUFDest>" + det_icmsufdest_vBCUFDest + "</vBCUFDest>\n");
                sb.Append("						<vBCFCPUFDest>" + det_icmsufdest_vBCFCPUFDest + "</vBCFCPUFDest>\n");
                sb.Append("						<pFCPUFDest>" + det_icmsufdest_pFCPUFDest + "</pFCPUFDest>\n");
                sb.Append("						<pICMSUFDest>" + det_icmsufdest_pICMSUFDest + "</pICMSUFDest>\n");
                sb.Append("						<pICMSInter>" + det_icmsufdest_pICMSInter + "</pICMSInter>\n");
                sb.Append("						<pICMSInterPart>" + det_icmsufdest_pICMSInterPart + "</pICMSInterPart>\n");
                sb.Append("						<vFCPUFDest>" + det_icmsufdest_vFCPUFDest + "</vFCPUFDest>\n");
                sb.Append("						<vICMSUFDest>" + det_icmsufdest_vICMSUFDest + "</vICMSUFDest>\n");
                sb.Append("						<vICMSUFRemet>" + det_icmsufdest_vICMSUFRemet + "</vICMSUFRemet>\n");
                sb.Append("					</ICMSUFDest>\n");



                //AQUI SERÁ CARREGADO O IPI
                string sqldtIPI = @"SELECT *,
                                    (SELECT CODTIPOTRIBUTO FROM VTRIBUTO WHERE CODTRIBUTO = GOPERITEMTRIBUTO.CODTRIBUTO AND CODEMPRESA = GOPERITEMTRIBUTO.CODEMPRESA) CODTIPOTRIBUTO FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ? AND CODTRIBUTO = 'IPI' ORDER BY CODCST";
                DataTable dtIPI = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqldtIPI, new object[] { AppLib.Context.Empresa, CODOPER, dr["NSEQITEM"].ToString().Trim() });

                if (dtIPI.Rows.Count > 0)
                {
                    dest_ipi_clEnq = dtIPI.Rows[0]["CENQ"].ToString().Trim();
                    dest_ipi_CNPJProd = cnpjFilial;
                    dest_ipint_CST = dtIPI.Rows[0]["CODCST"].ToString().Trim();
                    dest_ipint_VBC = dtIPI.Rows[0]["BASECALCULO"].ToString().Trim();
                    dest_ipint_PIPI = dtIPI.Rows[0]["ALIQUOTA"].ToString().Trim();
                    dest_ipint_VIPI = dtIPI.Rows[0]["VALOR"].ToString().Trim();

                }
                sb.Append("				<IPI>\n");
                //sb.Append("					<clEnq>" + dest_ipi_clEnq + "</clEnq>\n");
                //sb.Append("					<CNPJProd>" + dest_ipi_CNPJProd + "</CNPJProd>\n");
                //sb.Append("					<cSelo></cSelo>\n");
                //sb.Append("					<qSelo></qSelo>\n");
                sb.Append("					<cEnq>" + dest_ipi_clEnq + "</cEnq>\n");


                if (dest_ipint_CST == "00" ||
                    dest_ipint_CST == "49" ||
                    dest_ipint_CST == "50" ||
                   dest_ipint_CST == "99")
                {
                    sb.Append("					<IPITrib>\n");
                    sb.Append("						<CST>" + dest_ipint_CST + "</CST>\n");
                    sb.Append("						<vBC>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dest_ipint_VBC)) + "</vBC>\n");
                    sb.Append("						<pIPI>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dest_ipint_PIPI)) + "</pIPI>\n");
                    //sb.Append("						<qUnid>0.0000</qUnid>\n");
                    //sb.Append("						<vUnid>0.0000</vUnid>\n");
                    sb.Append("						<vIPI>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dest_ipint_VIPI)) + "</vIPI>\n");
                    sb.Append("					</IPITrib>\n");
                }

                if (dest_ipint_CST == "01" ||
                    dest_ipint_CST == "02" ||
                    dest_ipint_CST == "03" ||
                    dest_ipint_CST == "04" ||
                    dest_ipint_CST == "51" ||
                    dest_ipint_CST == "52" ||
                    dest_ipint_CST == "53" ||
                    dest_ipint_CST == "54" ||
                    dest_ipint_CST == "55")
                {
                    sb.Append("					<IPINT>\n");
                    sb.Append("						<CST>" + dest_ipint_CST + "</CST>\n");
                    sb.Append("					</IPINT>\n");
                }
                sb.Append("				</IPI>\n");




                //AQUI SERÁ CARREGADO O II
                string sqldtII = @"SELECT *,
                                    (SELECT CODTIPOTRIBUTO FROM VTRIBUTO WHERE CODTRIBUTO = GOPERITEMTRIBUTO.CODTRIBUTO AND CODEMPRESA = GOPERITEMTRIBUTO.CODEMPRESA) CODTIPOTRIBUTO FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ? AND CODTRIBUTO = 'II' ORDER BY CODCST";
                DataTable dtII = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqldtII, new object[] { AppLib.Context.Empresa, CODOPER, dr["NSEQITEM"].ToString().Trim() });

                foreach (DataRow dr_II in dtII.Rows)
                {
                    dest_ii_vBC = nf_formata.trocarVirgulaPorPonto(dr_II["BASECALCULO"].ToString().Trim());
                    dest_ii_vDespAdu = nf_formata.trocarVirgulaPorPonto(dr_II["VLDESPADUANA"].ToString().Trim());
                    dest_ii_vII = nf_formata.trocarVirgulaPorPonto(dr_II["VALOR"].ToString().Trim());
                    dest_ii_vIOF = nf_formata.trocarVirgulaPorPonto(dr_II["VALORIOF"].ToString().Trim());

                }

                //Enviar VII somente se for maior que 1

                sb.Append("				<II>\n");
                sb.Append("					<vBC>" + dest_ii_vBC + "</vBC>\n");
                sb.Append("					<vDespAdu>" + dest_ii_vDespAdu + "</vDespAdu>\n");

                if (!String.IsNullOrEmpty(dest_ii_vII))
                {
                    if (Convert.ToDouble(dest_ii_vII) > 0)
                        sb.Append("					<vII>" + dest_ii_vII + "</vII>\n");
                }

                sb.Append("					<vIOF>" + dest_ii_vIOF + "</vIOF>\n");
                sb.Append("				</II>\n");



                //AQUI SERÁ CARREGADO O PIS
                string sqldtPIS = @"SELECT *,
                                    (SELECT CODTIPOTRIBUTO FROM VTRIBUTO WHERE CODTRIBUTO = GOPERITEMTRIBUTO.CODTRIBUTO AND CODEMPRESA = GOPERITEMTRIBUTO.CODEMPRESA) CODTIPOTRIBUTO FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ? AND CODTRIBUTO = 'PIS' ORDER BY CODCST";
                DataTable dtPIS = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqldtPIS, new object[] { AppLib.Context.Empresa, CODOPER, dr["NSEQITEM"].ToString().Trim() });

                foreach (DataRow dr_PIS in dtPIS.Rows)
                {




                    if (dr_PIS["CODCST"].ToString() == "01" || dr_PIS["CODCST"].ToString() == "02")
                    {

                        dest_pisaliq_CST = dr_PIS["CODCST"].ToString().Trim();
                        dest_pisaliq_vBC = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_PIS["BASECALCULO"].ToString().Trim()));
                        dest_pisaliq_pPIS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_PIS["ALIQUOTA"].ToString().Trim()));
                        dest_pisaliq_vPIS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_PIS["VALOR"].ToString().Trim()));
                    }

                    if (dr_PIS["CODCST"].ToString() == "03")
                    {

                        dest_pisqtd_CST = dr_PIS["CODCST"].ToString().Trim();
                        dest_pisqtd_vPIS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_PIS["VALOR"].ToString().Trim()));
                    }

                    if (dr_PIS["CODCST"].ToString() == "04" ||
                        dr_PIS["CODCST"].ToString() == "05" ||
                        dr_PIS["CODCST"].ToString() == "06" ||
                        dr_PIS["CODCST"].ToString() == "07" ||
                        dr_PIS["CODCST"].ToString() == "08" ||
                        dr_PIS["CODCST"].ToString() == "09")
                    {

                        dest_pisnt_CST = dr_PIS["CODCST"].ToString().Trim();
                    }

                    if (dr_PIS["CODCST"].ToString() == "49" ||
                        dr_PIS["CODCST"].ToString() == "50" ||
                        dr_PIS["CODCST"].ToString() == "51" ||
                        dr_PIS["CODCST"].ToString() == "52" ||
                        dr_PIS["CODCST"].ToString() == "53" ||
                        dr_PIS["CODCST"].ToString() == "54" ||
                        dr_PIS["CODCST"].ToString() == "55" ||
                        dr_PIS["CODCST"].ToString() == "56" ||
                        dr_PIS["CODCST"].ToString() == "60" ||
                        dr_PIS["CODCST"].ToString() == "61" ||
                        dr_PIS["CODCST"].ToString() == "62" ||
                        dr_PIS["CODCST"].ToString() == "63" ||
                        dr_PIS["CODCST"].ToString() == "64" ||
                        dr_PIS["CODCST"].ToString() == "65" ||
                        dr_PIS["CODCST"].ToString() == "66" ||
                        dr_PIS["CODCST"].ToString() == "67" ||
                        dr_PIS["CODCST"].ToString() == "70" ||
                        dr_PIS["CODCST"].ToString() == "71" ||
                        dr_PIS["CODCST"].ToString() == "72" ||
                        dr_PIS["CODCST"].ToString() == "73" ||
                        dr_PIS["CODCST"].ToString() == "74" ||
                        dr_PIS["CODCST"].ToString() == "75" ||
                        dr_PIS["CODCST"].ToString() == "98" ||
                        dr_PIS["CODCST"].ToString() == "99")
                    {

                        dest_pisoutr_CST = dr_PIS["CODCST"].ToString().Trim();
                        dest_pisoutr_vBC = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_PIS["BASECALCULO"].ToString().Trim()));
                        dest_pisoutr_pPIS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_PIS["ALIQUOTA"].ToString().Trim()));
                        dest_pisoutr_vPIS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_PIS["VALOR"].ToString().Trim()));
                    }



                }

                sb.Append("				<PIS>\n");
                sb.Append("					<PISAliq>\n");
                sb.Append("						<CST>" + dest_pisaliq_CST + "</CST>\n");
                sb.Append("						<vBC>" + dest_pisaliq_vBC + "</vBC>\n");
                sb.Append("						<pPIS>" + dest_pisaliq_pPIS + "</pPIS>\n");
                sb.Append("						<vPIS>" + dest_pisaliq_vPIS + "</vPIS>\n");
                sb.Append("					</PISAliq>\n");


                sb.Append("					<PISQtde>\n");
                sb.Append("						<CST>" + dest_pisqtd_CST + "</CST>\n");
                //sb.Append("						<qBCProd>0</qBCProd>\n");
                //sb.Append("						<vAliqProd>"++"</vAliqProd>\n");
                sb.Append("						<vPIS>" + dest_pisqtd_vPIS + "</vPIS>\n");
                sb.Append("					</PISQtde>\n");


                sb.Append("					<PISNT>\n");
                sb.Append("						<CST>" + dest_pisnt_CST + "</CST>\n");
                sb.Append("					</PISNT>\n");



                sb.Append("					<PISOutr>\n");
                sb.Append("						<CST>" + dest_pisoutr_CST + "</CST>\n");
                sb.Append("						<vBC>" + dest_pisoutr_vBC + "</vBC>\n");
                sb.Append("						<pPIS>" + dest_pisoutr_pPIS + "</pPIS>\n");
                //sb.Append("						<qBCProd>0</qBCProd>\n");
                // sb.Append("						<vAliqProd>"++"</vAliqProd>\n");
                sb.Append("						<vPIS>" + dest_pisoutr_vPIS + "</vPIS>\n");
                sb.Append("					</PISOutr>\n");


                sb.Append("				</PIS>\n");




                //AQUI SERÁ CARREGADO O PISST
                /*
                sb.Append("				<PISST>\n");
                sb.Append("					<vBC>"+ dest_pis_vBC + "</vBC>\n");
                sb.Append("					<pPIS>"+ dest_pis_pPIS + "</pPIS>\n");
                sb.Append("					<qBCProd>0.0000</qBCProd>\n");
                // sb.Append("					<vAliqProd></vAliqProd>\n");
                sb.Append("					<vPIS>"+ dest_pis_vPIS + "</vPIS>\n");
                sb.Append("				</PISST>\n");
                */


                //AQUI CARREGA O COFINS
                string sqldtCOFINS = @"SELECT *,
                                    (SELECT CODTIPOTRIBUTO FROM VTRIBUTO WHERE CODTRIBUTO = GOPERITEMTRIBUTO.CODTRIBUTO AND CODEMPRESA = GOPERITEMTRIBUTO.CODEMPRESA) CODTIPOTRIBUTO FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ? AND CODTRIBUTO = 'COFINS' ORDER BY CODCST";
                DataTable dtCOFINS = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqldtCOFINS, new object[] { AppLib.Context.Empresa, CODOPER, dr["NSEQITEM"].ToString().Trim() });

                foreach (DataRow dr_COFINS in dtCOFINS.Rows)
                {



                    if (dr_COFINS["CODCST"].ToString() == "01" || dr_COFINS["CODCST"].ToString() == "02")
                    {

                        dest_cofinsaliq_CST = dr_COFINS["CODCST"].ToString().Trim();
                        dest_cofinsaliq_vBC = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_COFINS["BASECALCULO"].ToString().Trim()));
                        dest_cofinsaliq_pCOFINS = nf_formata.trocarVirgulaPorPonto(dr_COFINS["ALIQUOTA"].ToString().Trim());
                        dest_cofinsaliq_vCOFINS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_COFINS["VALOR"].ToString().Trim()));
                    }

                    if (dr_COFINS["CODCST"].ToString() == "03")
                    {

                        dest_cofinsqtde_CST = dr_COFINS["CODCST"].ToString().Trim();
                        dest_cofinsqtde_vCOFINS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_COFINS["VALOR"].ToString().Trim()));
                    }

                    if (dr_COFINS["CODCST"].ToString() == "04" ||
                        dr_COFINS["CODCST"].ToString() == "05" ||
                        dr_COFINS["CODCST"].ToString() == "06" ||
                        dr_COFINS["CODCST"].ToString() == "07" ||
                        dr_COFINS["CODCST"].ToString() == "08" ||
                        dr_COFINS["CODCST"].ToString() == "09")
                    {

                        dest_cofinsnt_CST = dr_COFINS["CODCST"].ToString();
                    }

                    if (dr_COFINS["CODCST"].ToString() == "49" ||
                        dr_COFINS["CODCST"].ToString() == "50" ||
                        dr_COFINS["CODCST"].ToString() == "51" ||
                        dr_COFINS["CODCST"].ToString() == "52" ||
                        dr_COFINS["CODCST"].ToString() == "53" ||
                        dr_COFINS["CODCST"].ToString() == "54" ||
                        dr_COFINS["CODCST"].ToString() == "55" ||
                        dr_COFINS["CODCST"].ToString() == "56" ||
                        dr_COFINS["CODCST"].ToString() == "60" ||
                        dr_COFINS["CODCST"].ToString() == "61" ||
                        dr_COFINS["CODCST"].ToString() == "62" ||
                        dr_COFINS["CODCST"].ToString() == "63" ||
                        dr_COFINS["CODCST"].ToString() == "64" ||
                        dr_COFINS["CODCST"].ToString() == "65" ||
                        dr_COFINS["CODCST"].ToString() == "66" ||
                        dr_COFINS["CODCST"].ToString() == "67" ||
                        dr_COFINS["CODCST"].ToString() == "70" ||
                        dr_COFINS["CODCST"].ToString() == "71" ||
                        dr_COFINS["CODCST"].ToString() == "72" ||
                        dr_COFINS["CODCST"].ToString() == "73" ||
                        dr_COFINS["CODCST"].ToString() == "74" ||
                        dr_COFINS["CODCST"].ToString() == "75" ||
                        dr_COFINS["CODCST"].ToString() == "98" ||
                        dr_COFINS["CODCST"].ToString() == "99")
                    {

                        dest_cofinsoutr_CST = dr_COFINS["CODCST"].ToString().Trim();
                        dest_cofinsoutr_vBC = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_COFINS["BASECALCULO"].ToString().Trim()));
                        dest_cofinsoutr_pCOFINS = nf_formata.trocarVirgulaPorPonto(dr_COFINS["ALIQUOTA"].ToString().Trim());
                        dest_cofinsoutr_vCOFINS = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dr_COFINS["VALOR"].ToString().Trim()));

                    }




                }

                sb.Append("				<COFINS>\n");
                sb.Append("					<COFINSAliq>\n");
                sb.Append("						<CST>" + dest_cofinsaliq_CST + "</CST>\n");
                sb.Append("						<vBC>" + dest_cofinsaliq_vBC + "</vBC>\n");
                sb.Append("						<pCOFINS>" + dest_cofinsaliq_pCOFINS + "</pCOFINS>\n");
                sb.Append("						<vCOFINS>" + dest_cofinsaliq_vCOFINS + "</vCOFINS>\n");
                sb.Append("					</COFINSAliq>\n");

                /*
                sb.Append("					<COFINSQtde>\n");
                sb.Append("						<CST>" + dest_cofinsqtde_CST + "</CST>\n");
                sb.Append("						<qBCProd>0</qBCProd>\n");
                //sb.Append("						<vAliqProd></vAliqProd>\n");
                sb.Append("						<vCOFINS>" + dest_cofinsqtde_vCOFINS + "</vCOFINS>\n");
                sb.Append("					</COFINSQtde>\n");
                */


                sb.Append("					<COFINSNT>\n");
                sb.Append("						<CST>" + dest_cofinsnt_CST + "</CST>\n");
                sb.Append("					</COFINSNT>\n");



                sb.Append("					<COFINSOutr>\n");
                sb.Append("						<CST>" + dest_cofinsoutr_CST + "</CST>\n");
                sb.Append("						<vBC>" + dest_cofinsoutr_vBC + "</vBC>\n");
                sb.Append("						<pCOFINS>" + dest_cofinsoutr_pCOFINS + "</pCOFINS>\n");
                //sb.Append("						<qBCProd>0</qBCProd>\n");
                //sb.Append("						<vAliqProd></vAliqProd>\n");
                sb.Append("						<vCOFINS>" + dest_cofinsoutr_vCOFINS + "</vCOFINS>\n");
                sb.Append("					</COFINSOutr>\n");


                sb.Append("				</COFINS>\n");

                /*
                sb.Append("				<COFINSST>\n");
                sb.Append("					<vBC>"+ dest_cofinsst_vBC + "</vBC>\n");
                sb.Append("					<pCOFINS>"+ dest_cofinst_pCOFINS + "</pCOFINS>\n");
                sb.Append("					<qBCProd>0.0000</qBCProd>\n");
               // sb.Append("					<vAliqProd></vAliqProd>\n");
                sb.Append("					<vCOFINS>"+ dest_cofinsst_vCOFINS + "</vCOFINS>\n");
                sb.Append("				</COFINSST>\n");
                */


                /*
                sb.Append("				<ISSQN>\n");
                sb.Append("					<vBC></vBC>\n");
                sb.Append("					<vAliq></vAliq>\n");
                sb.Append("					<vISSQN></vISSQN>\n");
                sb.Append("					<cMunFG></cMunFG>\n");
                sb.Append("					<cListServ></cListServ>\n");
                sb.Append("					<vDeducao></vDeducao>\n");
                sb.Append("					<vOutro></vOutro>\n");
                sb.Append("					<vDescIncond></vDescIncond>\n");
                sb.Append("					<vDescCond></vDescCond>\n");
                sb.Append("					<vISSRet></vISSRet>\n");
                sb.Append("					<indISS></indISS>\n");
                sb.Append("					<cServico></cServico>\n");
                sb.Append("					<cMun></cMun>\n");
                sb.Append("					<cPais></cPais>\n");
                sb.Append("					<nProcesso></nProcesso>\n");
                sb.Append("					<indIncentivo></indIncentivo>\n");
                sb.Append("				</ISSQN>\n");
                */

                sb.Append("			</imposto>\n");

                /*
                sb.Append("			<impostoDevol>\n");
                sb.Append("				<pDevol></pDevol>\n");
                sb.Append("				<IPI>\n");
                sb.Append("					<vIPIDevol></vIPIDevol>\n");
                sb.Append("				</IPI>\n");
                sb.Append("			</impostoDevol>\n");
                */

                if (dr["SAIDA"].ToString() == "CODIGOAUXILIAR")
                {
                    CodProduto = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODPRODUTO FROM VPRODUTO WHERE CODEMPRESA = ? AND CODIGOAUXILIAR = ? ", new object[] { AppLib.Context.Empresa, dr["CODIGO"].ToString() }).ToString();

                    dest_ipi_infAdProd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT GOPERITEM.INFCOMPL
                                                                                                            FROM GOPERITEM, VPRODUTO
                                                                                                            WHERE
                                                                                                            GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA
                                                                                                            AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO
                                                                                                            AND GOPERITEM.CODEMPRESA = ?
                                                                                                            AND GOPERITEM.CODPRODUTO = ?
                                                                                                            AND GOPERITEM.CODOPER = ? ", new object[] { AppLib.Context.Empresa, CodProduto, CODOPER }).ToString();
                }
                else
                {
                    dest_ipi_infAdProd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT GOPERITEM.INFCOMPL
                                                                                                            FROM GOPERITEM, VPRODUTO
                                                                                                            WHERE
                                                                                                            GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA
                                                                                                            AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO
                                                                                                            AND GOPERITEM.CODEMPRESA = ?
                                                                                                            AND GOPERITEM.CODPRODUTO = ?
                                                                                                            AND GOPERITEM.CODOPER = ? ", new object[] { AppLib.Context.Empresa, dr["CODIGO"].ToString(), CODOPER }).ToString();
                }

                

                sb.Append("			<infAdProd>" + dest_ipi_infAdProd.Trim() + "</infAdProd>\n");
                //sb.Append("			<nItem></nItem>\n");
                sb.Append("		</det>\n");


                //limpa o dt
                dtgOperItemTagProd = null;
                dtDadosProdutoICMSUFDest = null;

                //LIMPA AS VARIAVEIS CST00

                det_icms00_orig = "";
                det_icms00_CST = "";
                det_icms00_modBC = "";
                det_icms00_vBC = "";
                det_icms00_pICMS = "";
                det_icms00_vICMS = "";
                det_icms00_pFCP = "";
                det_icms00_vFCP = "";


            }
            #endregion

            //TAG DO TOTAL
            sqlTagTotal = @"
SELECT

ISNULL((
SELECT SUM(GOPERITEMTRIBUTO.BASECALCULO)
FROM GOPERITEMTRIBUTO, VTRIBUTO
WHERE GOPERITEMTRIBUTO.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEMTRIBUTO.CODOPER = GOPER.CODOPER
AND GOPERITEMTRIBUTO.CODEMPRESA = VTRIBUTO.CODEMPRESA
AND GOPERITEMTRIBUTO.CODTRIBUTO = VTRIBUTO.CODTRIBUTO
AND VTRIBUTO.CODTIPOTRIBUTO = 'ICMS'
),0) VBC,

ISNULL((
SELECT SUM(GOPERITEMTRIBUTO.VALOR)
FROM GOPERITEMTRIBUTO, VTRIBUTO
WHERE GOPERITEMTRIBUTO.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEMTRIBUTO.CODOPER = GOPER.CODOPER
AND GOPERITEMTRIBUTO.CODEMPRESA = VTRIBUTO.CODEMPRESA
AND GOPERITEMTRIBUTO.CODTRIBUTO = VTRIBUTO.CODTRIBUTO
AND VTRIBUTO.CODTIPOTRIBUTO = 'ICMS'
),0) VICMS,

0 VICMSDESON,


ISNULL((SELECT SUM(VFCPUFDEST) FROM GOPERITEMDIFAL WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODOPER = GOPER.CODOPER),0) VFCP,


ISNULL((SELECT SUM(VFCPUFDEST) FROM GOPERITEMDIFAL WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODOPER = GOPER.CODOPER),0) VFCPUFDEST,



ISNULL((SELECT SUM(VICMSUFDEST) FROM GOPERITEMDIFAL WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODOPER = GOPER.CODOPER),0) VICMSUFDEST,


ISNULL((SELECT SUM(VICMSUFREMET) FROM GOPERITEMDIFAL WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODOPER = GOPER.CODOPER),0) VICMSUFREMET,


ISNULL((
SELECT SUM(GOPERITEMTRIBUTO.BASECALCULO)
FROM GOPERITEMTRIBUTO, VTRIBUTO
WHERE GOPERITEMTRIBUTO.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEMTRIBUTO.CODOPER = GOPER.CODOPER
AND GOPERITEMTRIBUTO.CODEMPRESA = VTRIBUTO.CODEMPRESA
AND GOPERITEMTRIBUTO.CODTRIBUTO = VTRIBUTO.CODTRIBUTO
AND VTRIBUTO.CODTIPOTRIBUTO = 'ICMS-ST'
),0) VBCST,


ISNULL((
SELECT SUM(GOPERITEMTRIBUTO.VALORICMSST)
FROM GOPERITEMTRIBUTO, VTRIBUTO
WHERE GOPERITEMTRIBUTO.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEMTRIBUTO.CODOPER = GOPER.CODOPER
AND GOPERITEMTRIBUTO.CODEMPRESA = VTRIBUTO.CODEMPRESA
AND GOPERITEMTRIBUTO.CODTRIBUTO = VTRIBUTO.CODTRIBUTO
AND VTRIBUTO.CODTIPOTRIBUTO = 'ICMS-ST'
),0) VST,



ISNULL((SELECT SUM(VFCPSTUFDEST) FROM GOPERITEMDIFAL WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODOPER = GOPER.CODOPER),0) VFCPST,


ISNULL((SELECT SUM(VFCPSTRET) FROM GOPERITEMDIFAL WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODOPER = GOPER.CODOPER),0) VFCPSTRET,


ISNULL(VALORBRUTO,0) VPROD,



ISNULL((SELECT SUM(RATEIOFRETE) FROM GOPERITEM WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODOPER = GOPER.CODOPER),0) VFRETE,


ISNULL((SELECT SUM(RATEIOSEGURO) FROM GOPERITEM WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODOPER = GOPER.CODOPER),0) VSEG,


ISNULL((SELECT SUM(RATEIODESCONTO) FROM GOPERITEM WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODOPER = GOPER.CODOPER),0) VDESC,


ISNULL((
SELECT SUM(GOPERITEMTRIBUTO.VALOR)
FROM GOPERITEMTRIBUTO, VTRIBUTO
WHERE GOPERITEMTRIBUTO.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEMTRIBUTO.CODOPER = GOPER.CODOPER
AND GOPERITEMTRIBUTO.CODEMPRESA = VTRIBUTO.CODEMPRESA
AND GOPERITEMTRIBUTO.CODTRIBUTO = VTRIBUTO.CODTRIBUTO
AND VTRIBUTO.CODTIPOTRIBUTO = 'II'
),0) VII,



CASE 
	WHEN
	(SELECT FINEMISSAONFE FROM GTIPOPER WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTIPOPER = GOPER.CODTIPOPER) IN (1,2,3, 4)
THEN
	(ISNULL((
	SELECT SUM(GOPERITEMTRIBUTO.VALOR)
	FROM GOPERITEMTRIBUTO, VTRIBUTO
	WHERE GOPERITEMTRIBUTO.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEMTRIBUTO.CODOPER = GOPER.CODOPER
	AND GOPERITEMTRIBUTO.CODEMPRESA = VTRIBUTO.CODEMPRESA
	AND GOPERITEMTRIBUTO.CODTRIBUTO = VTRIBUTO.CODTRIBUTO
	AND VTRIBUTO.CODTIPOTRIBUTO = 'IPI'
	),0))
ELSE	
	0
END VIPI,



CASE 
	WHEN
	(SELECT FINEMISSAONFE FROM GTIPOPER WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTIPOPER = GOPER.CODTIPOPER) IN (4)
THEN
	--(ISNULL((
	--SELECT SUM(GOPERITEMTRIBUTO.VALOR)
	--FROM GOPERITEMTRIBUTO, VTRIBUTO
	--WHERE GOPERITEMTRIBUTO.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEMTRIBUTO.CODOPER = GOPER.CODOPER
	--AND GOPERITEMTRIBUTO.CODEMPRESA = VTRIBUTO.CODEMPRESA
	--AND GOPERITEMTRIBUTO.CODTRIBUTO = VTRIBUTO.CODTRIBUTO
	--AND VTRIBUTO.CODTIPOTRIBUTO = 'IPI'
	--),0))

0

ELSE	
	0
END VIPIDEVOL,

ISNULL((
SELECT SUM(GOPERITEMTRIBUTO.VALOR)
FROM GOPERITEMTRIBUTO, VTRIBUTO
WHERE GOPERITEMTRIBUTO.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEMTRIBUTO.CODOPER = GOPER.CODOPER
AND GOPERITEMTRIBUTO.CODEMPRESA = VTRIBUTO.CODEMPRESA
AND GOPERITEMTRIBUTO.CODTRIBUTO = VTRIBUTO.CODTRIBUTO
AND VTRIBUTO.CODTIPOTRIBUTO = 'PIS'
),0) VPIS,

ISNULL((
SELECT SUM(GOPERITEMTRIBUTO.VALOR)
FROM GOPERITEMTRIBUTO, VTRIBUTO
WHERE GOPERITEMTRIBUTO.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEMTRIBUTO.CODOPER = GOPER.CODOPER
AND GOPERITEMTRIBUTO.CODEMPRESA = VTRIBUTO.CODEMPRESA
AND GOPERITEMTRIBUTO.CODTRIBUTO = VTRIBUTO.CODTRIBUTO
AND VTRIBUTO.CODTIPOTRIBUTO = 'COFINS'
),0) VCOFINS,


ISNULL((SELECT SUM(RATEIODESPESA) FROM GOPERITEM WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODOPER = GOPER.CODOPER),0) VOUTRO,



ISNULL(VALORLIQUIDO,0) VNF, 

ISNULL((
SELECT SUM(GOPERITEMTRIBUTO.VALOR)
FROM GOPERITEMTRIBUTO, VTRIBUTO
WHERE GOPERITEMTRIBUTO.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEMTRIBUTO.CODOPER = GOPER.CODOPER
AND GOPERITEMTRIBUTO.CODEMPRESA = VTRIBUTO.CODEMPRESA
AND GOPERITEMTRIBUTO.CODTRIBUTO = VTRIBUTO.CODTRIBUTO
AND VTRIBUTO.CODTIPOTRIBUTO = 'ICMS'
),0) VTOTTRIB

FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";


            dtTotal = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlTagTotal, new object[] { AppLib.Context.Empresa, CODOPER });

            sb.Append("		<total>\n");
            sb.Append("			<ICMSTot>\n");
            sb.Append("				<vBC>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VBC"].ToString().Trim())) + "</vBC>\n");
            //sb.Append("				<vBC>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VBC"].ToString().Trim())) + "</vBC>\n");
            sb.Append("				<vICMS>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VICMS"].ToString().Trim())) + "</vICMS>\n");
            sb.Append("				<vICMSDeson>" + nf_formata.trocarVirgulaPorPonto(dtTotal.Rows[0]["VICMSDESON"].ToString().Trim()) + "</vICMSDeson>\n");
            //sb.Append("				<vFCP>"+ nf_formata.trocarVirgulaPorPonto(dtTotal.Rows[0]["VFCP"].ToString().Trim()) + "</vFCP>\n");
            sb.Append("				<vFCP>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(totalizadorvFCP.ToString())) + "</vFCP>\n");
            /* comentada */
            sb.Append("				<vFCPUFDest>" + nf_formata.trocarVirgulaPorPonto(dtTotal.Rows[0]["VFCP"].ToString().Trim()) + "</vFCPUFDest>\n"); // nf_formata.trocarVirgulaPorPonto(dtTotal.Rows[0]["VFCPUFDEST"].ToString())
                                                                                                                                                      /* comentada */
            sb.Append("				<vICMSUFDest>" + nf_formata.trocarVirgulaPorPonto(dtTotal.Rows[0]["VICMSUFDEST"].ToString().Trim()) + "</vICMSUFDest>\n");
            /*comentada */
            sb.Append("				<vICMSUFRemet>" + nf_formata.trocarVirgulaPorPonto(dtTotal.Rows[0]["VICMSUFREMET"].ToString().Trim()) + "</vICMSUFRemet>\n");
            sb.Append("				<vBCST>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VBCST"].ToString().Trim())) + "</vBCST>\n");
            sb.Append("				<vST>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VST"].ToString().Trim())) + "</vST>\n");

            sb.Append("				<vFCPST>" + nf_formata.trocarVirgulaPorPonto(dtTotal.Rows[0]["VFCPST"].ToString().Trim()) + "</vFCPST>\n");
            sb.Append("				<vFCPSTRet>" + nf_formata.trocarVirgulaPorPonto(dtTotal.Rows[0]["VFCPSTRET"].ToString().Trim()) + "</vFCPSTRet>\n");
            sb.Append("				<vProd>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VPROD"].ToString().Trim())) + "</vProd>\n");
            sb.Append("				<vFrete>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VFRETE"].ToString().Trim())) + "</vFrete>\n");
            sb.Append("				<vSeg>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VSEG"].ToString().Trim())) + "</vSeg>\n");
            sb.Append("				<vDesc>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VDESC"].ToString().Trim())) + "</vDesc>\n");

            sb.Append("				<vII>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VII"].ToString().Trim())) + "</vII>\n");
            sb.Append("				<vIPI>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VIPI"].ToString().Trim())) + "</vIPI>\n");
            sb.Append("				<vIPIDevol>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VIPIDEVOL"].ToString().Trim())) + "</vIPIDevol>\n");
            sb.Append("				<vPIS>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VPIS"].ToString().Trim())) + "</vPIS>\n");
            sb.Append("				<vCOFINS>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VCOFINS"].ToString().Trim())) + "</vCOFINS>\n");
            sb.Append("				<vOutro>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VOUTRO"].ToString().Trim())) + "</vOutro>\n");
            sb.Append("				<vNF>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VNF"].ToString().Trim())) + "</vNF>\n");
            sb.Append("				<vTotTrib>0.00</vTotTrib>\n");  //"+ nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtTotal.Rows[0]["VTOTTRIB"].ToString())) + "
            sb.Append("			</ICMSTot>\n");


            /*
            sb.Append("			<ISSQNtot>\n");
            sb.Append("				<vServ></vServ>\n");
            sb.Append("				<vBC></vBC>\n");
            sb.Append("				<vISS></vISS>\n");
            sb.Append("				<vPIS></vPIS>\n");
            sb.Append("				<vCOFINS></vCOFINS>\n");
            sb.Append("				<dCompet></dCompet>\n");
            sb.Append("				<vDeducao></vDeducao>\n");
            sb.Append("				<vOutro></vOutro>\n");
            sb.Append("				<vDescIncond></vDescIncond>\n");
            sb.Append("				<vDescCond></vDescCond>\n");
            sb.Append("				<vISSRet></vISSRet>\n");
            sb.Append("				<cRegTrib></cRegTrib>\n");
            sb.Append("			</ISSQNtot>\n");
            */

            /*
            sb.Append("			<retTrib>\n");
            sb.Append("				<vRetPIS></vRetPIS>\n");
            sb.Append("				<vRetCOFINS></vRetCOFINS>\n");
            sb.Append("				<vRetCSLL></vRetCSLL>\n");
            sb.Append("				<vBCIRRF></vBCIRRF>\n");
            sb.Append("				<vIRRF></vIRRF>\n");
            sb.Append("				<vBCRetPrev></vBCRetPrev>\n");
            sb.Append("				<vRetPrev></vRetPrev>\n");
            sb.Append("			</retTrib>\n");
             */
            sb.Append("		</total>\n");



            //TAG DA TRANSPORTADORA

            #region Query comentada

            //         sqlTransportadora = @"SELECT 
            //         VTRANSPORTADORA.*,
            //         GCIDADE.NOME AS CIDADE,
            //         GOPER.PLACA,
            //         GOPER.UFPLACA,
            //         GOPER.QUANTIDADE,
            //         GOPER.ESPECIE,
            //         GOPER.MARCA,
            //         GOPER.PESOLIQUIDO,
            //         GOPER.PESOBRUTO,
            //GESTADO.CODIBGE
            //         FROM
            //         VTRANSPORTADORA
            //         INNER JOIN GOPER ON VTRANSPORTADORA.CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA
            //         INNER JOIN GCIDADE ON VTRANSPORTADORA.CODCIDADE = GCIDADE.CODCIDADE
            //      INNER JOIN GESTADO ON VTRANSPORTADORA.CODETD = GESTADO.CODETD
            //         WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ?";

            #endregion

            #region Query comentada

            /*
            sqlTransportadora = @"SELECT 
	(SELECT CODEMPRESA FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) CODEMPRESA,
	(SELECT CODTRANSPORTADORA FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) CODTRANSPORTADORA,
	(SELECT NOME FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) NOME,
	(SELECT NOMEFANTASIA FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) NOMEFANTASIA,
	(SELECT CGCCPF FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) CGCCPF,
	(SELECT CEP FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) CEP,
	(SELECT CODTIPORUA FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) CODTIPORUA,
	(SELECT RUA FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) RUA,
	(SELECT NUMERO FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) NUMERO,
	(SELECT COMPLEMENTO FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) COMPLEMENTO,
	(SELECT CODTIPOBAIRRO FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) CODTIPOBAIRRO,
	(SELECT BAIRRO FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) BAIRRO,
	(SELECT CODCIDADE FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) CODCIDADE,
	(SELECT CODETD FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) CODETD,
	(SELECT CODPAIS FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) CODPAIS,
	(SELECT FISICOJURIDICO FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) FISICOJURIDICO,
	(SELECT INSCRICAOESTADUAL FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) INSCRICAOESTADUAL,
	(SELECT ATIVO FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) ATIVO,
	(SELECT TELEFONE FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) TELEFONE,
	(SELECT TELCELULAR FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) TELCELULAR,
	(SELECT TELFAX FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) TELFAX,
	(SELECT CONTATO FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) CONTATO,
	(SELECT HOMEPAGE FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) HOMEPAGE,
	(SELECT EMAIL FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) EMAIL,
	(SELECT PIS FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA) PIS,
	(SELECT NOME FROM GCIDADE WHERE CODCIDADE IN (SELECT CODCIDADE FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA)) CIDADE,
	GOPER.PLACA,
	GOPER.UFPLACA,
	GOPER.QUANTIDADE,
	GOPER.ESPECIE,
	GOPER.MARCA,
	GOPER.PESOLIQUIDO,
	GOPER.PESOBRUTO,
	(SELECT CODIBGE FROM GESTADO WHERE CODETD IN (SELECT CODETD FROM VTRANSPORTADORA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA)) CODIBGE
FROM
	GOPER
WHERE 
	GOPER.CODEMPRESA = ? 
AND GOPER.CODOPER = ?";
*/

            #endregion

            sqlTransportadora = @"SELECT 
                VTRANSPORTADORA.CGCCPF,
                VTRANSPORTADORA.NOME,
                VTRANSPORTADORA.INSCRICAOESTADUAL,
                ISNULL(RUA,'') + ' , ' + ISNULL(VTRANSPORTADORA.NUMERO,'') + ' - ' + ISNULL(COMPLEMENTO,'') + '  ' + ISNULL(BAIRRO,'') + ' - ' + ISNULL(CEP,'') ENDERECO,
                UPPER(GCIDADE.NOME) AS CIDADE,
                GESTADO.CODETD
                FROM
                VTRANSPORTADORA
                INNER JOIN GOPER ON VTRANSPORTADORA.CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA
                INNER JOIN GCIDADE ON VTRANSPORTADORA.CODCIDADE = GCIDADE.CODCIDADE
                INNER JOIN GESTADO ON VTRANSPORTADORA.CODETD = GESTADO.CODETD
                WHERE GOPER.CODEMPRESA =? AND GOPER.CODOPER = ?";

            dtTransportadora = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlTransportadora, new object[] { AppLib.Context.Empresa, CODOPER });
            modFrete = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT FRETECIFFOB FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CODOPER }).ToString();

            sb.Append("		<transp>\n");
            sb.Append("			<modFrete>" + modFrete + "</modFrete>\n");

            if (dtTransportadora.Rows.Count > 0)
            {
                sb.Append("			<transporta>\n");

                if (nf_formata.limparCaracteres(dtTransportadora.Rows[0]["CGCCPF"].ToString()).Length > 11)
                    sb.Append("				<CNPJ>" + nf_formata.limparCaracteres(dtTransportadora.Rows[0]["CGCCPF"].ToString().Trim()) + "</CNPJ>\n");
                else
                    sb.Append("				<CPF>" + nf_formata.limparCaracteres(dtTransportadora.Rows[0]["CGCCPF"].ToString().Trim()) + "</CPF>\n");

                sb.Append("				<xNome>" + dtTransportadora.Rows[0]["NOME"].ToString().Trim() + "</xNome>\n");
                sb.Append("				<IE>" + nf_formata.limparCaracteres(dtTransportadora.Rows[0]["INSCRICAOESTADUAL"].ToString().Trim()) + "</IE>\n");
                sb.Append("				<xEnder>" + dtTransportadora.Rows[0]["ENDERECO"].ToString().Trim() + "</xEnder>\n");
                sb.Append("				<xMun>" + dtTransportadora.Rows[0]["CIDADE"].ToString().Trim() + "</xMun>\n");
                sb.Append("				<UF>" + dtTransportadora.Rows[0]["CODETD"].ToString().Trim() + "</UF>\n");
                sb.Append("			</transporta>\n");
            }
            /*
            sb.Append("			<retTransp>\n");
            sb.Append("				<vServ></vServ>\n");
            sb.Append("				<vBCRet></vBCRet>\n");
            sb.Append("				<pICMSRet></pICMSRet>\n");
            sb.Append("				<vICMSRet></vICMSRet>\n");
            sb.Append("				<CFOP></CFOP>\n");
            sb.Append("				<cMunFG></cMunFG>\n");
            sb.Append("			</retTransp>\n");
            */


            sqlVeicTransp = @"select 
                    GOPER.PLACA,
                    GOPER.UFPLACA
                    from
                    GOPER 
                    WHERE GOPER.CODEMPRESA =? AND GOPER.CODOPER = ?";

            dtVeicTransp = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlVeicTransp, new object[] { AppLib.Context.Empresa, CODOPER });
            if (dtVeicTransp.Rows.Count > 0)
            {
                sb.Append("			<veicTransp>\n");
                sb.Append("				<placa>" + dtVeicTransp.Rows[0]["PLACA"].ToString().Trim() + "</placa>\n");
                sb.Append("				<UF>" + dtVeicTransp.Rows[0]["UFPLACA"].ToString().Trim() + "</UF>\n");
                //sb.Append("				<RNTC></RNTC>\n");
                sb.Append("			</veicTransp>\n");
            }

            /*
            sb.Append("			<reboque>\n");
            sb.Append("				<placa></placa>\n");
            sb.Append("				<UF></UF>\n");
            sb.Append("				<RNTC></RNTC>\n");
            sb.Append("			</reboque>\n");
            */

            //sb.Append("			<vagao></vagao>\n");
            //sb.Append("			<balsa></balsa>\n");

            sqlVol = @"SELECT 
                GOPER.QUANTIDADE,
                GOPER.ESPECIE,
                GOPER.MARCA,
                GOPER.PESOLIQUIDO,
                GOPER.PESOBRUTO
                FROM
                GOPER 
                WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ?";

            dtVol = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlVol, new object[] { AppLib.Context.Empresa, CODOPER });

            if (dtVol.Rows.Count > 0)
            {
                sb.Append("			<vol>\n");
                sb.Append("				<qVol>" + nf_formata.trocarVirgulaPorPonto(dtVol.Rows[0]["QUANTIDADE"].ToString().Trim()) + "</qVol>\n");
                sb.Append("				<esp>" + dtVol.Rows[0]["ESPECIE"].ToString().Trim() + "</esp>\n");
                sb.Append("				<marca>" + dtVol.Rows[0]["MARCA"].ToString().Trim() + "</marca>\n");
                sb.Append("				<nVol></nVol>\n");
                sb.Append("				<pesoL>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_3(dtVol.Rows[0]["PESOLIQUIDO"].ToString().Trim())) + "</pesoL>\n");
                sb.Append("				<pesoB>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_3(dtVol.Rows[0]["PESOBRUTO"].ToString().Trim())) + "</pesoB>\n");

                /*
                sb.Append("				<lacres>\n");
                sb.Append("					<nLacre></nLacre>\n");
                sb.Append("				</lacres>\n");
                */
                sb.Append("			</vol>\n");
            }
            sb.Append("		</transp>\n");


            sb.Append("		<cobr>\n");

            //TAG FAT
            sqlFat = @"SELECT NUMERO,VLORIGINAL,VLDESCONTO, VLLIQUIDO FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ? AND CODFILIAL=?";
            //dtFat = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlFat, new object[] { AppLib.Context.Empresa, CODOPER, filial });
            dtFat = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlFat, new object[] { AppLib.Context.Empresa, CODOPER, filial });

            if (dtFat.Rows.Count > 0)
            {
                //soma as parcelas
                double vloriginal = 0;
                double vlliquido = 0;

                foreach (DataRow item in dtFat.Rows)
                {
                    vloriginal += Convert.ToDouble(item["VLORIGINAL"]);
                    vlliquido += Convert.ToDouble(item["VLLIQUIDO"]);
                }
                /*
                sb.Append("			<fat>\n");
                sb.Append("				<nFat>" + nf_formata.recuperaNFAT(dtFat.Rows[0]["NUMERO"].ToString().Trim()) + "</nFat>\n");
                sb.Append("				<vOrig>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtFat.Rows[0]["VLORIGINAL"].ToString().Trim())) + "</vOrig>\n");
                sb.Append("				<vDesc>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtFat.Rows[0]["VLDESCONTO"].ToString().Trim())) + "</vDesc>\n");
                sb.Append("				<vLiq>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtFat.Rows[0]["VLLIQUIDO"].ToString().Trim())) + "</vLiq>\n");
                sb.Append("			</fat>\n");
                */
                sb.Append("			<fat>\n");
                sb.Append("				<nFat>" + nf_formata.recuperaNFAT(dtFat.Rows[0]["NUMERO"].ToString().Trim()) + "</nFat>\n");
                sb.Append("				<vOrig>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(vloriginal.ToString().Trim())) + "</vOrig>\n");
                sb.Append("				<vDesc>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtFat.Rows[0]["VLDESCONTO"].ToString().Trim())) + "</vDesc>\n");
                sb.Append("				<vLiq>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(vlliquido.ToString().Trim())) + "</vLiq>\n");
                sb.Append("			</fat>\n");

            }



            //TAG DUP
            dtDup = AppLib.Context.poolConnection.Get("Start").ExecQuery("select NUMERO, DATAVENCIMENTO, VLORIGINAL from FLANCA WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CODOPER });
            foreach (DataRow dt_Dup in dtDup.Rows)
            {
                sb.Append("			<dup>\n");
                sb.Append("				<nDup>" + nf_formata.recuperaNDUP(dt_Dup["NUMERO"].ToString().Trim().PadLeft(3, '0')) + "</nDup>\n");
                sb.Append("				<dVenc>" + String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dt_Dup["DATAVENCIMENTO"])) + "</dVenc>\n");
                sb.Append("				<vDup>" + nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dt_Dup["VLORIGINAL"].ToString().Trim())) + "</vDup>\n");
                sb.Append("			</dup>\n");
            }

            sb.Append("		</cobr>\n");

            //VERIFICAR DEPOIS
            sqlPag = @"
SELECT
CASE WHEN 
	(SELECT GERAFINANCEIRO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?) = 1  AND (SELECT FINEMISSAONFE FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?) = 1
THEN
	(SELECT TIPO FROM VFORMAPGTO WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODFORMA = GOPER.CODFORMA)
ELSE
	90
END
	TPAG,

CASE WHEN 
	(SELECT GERAFINANCEIRO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?) = 1
THEN
	(SELECT SUM(VLORIGINAL) FROM FLANCA WHERE CODEMPRESA = GOPER.CODEMPRESA AND CODOPER = GOPER.CODOPER)
ELSE
	0
END
	VPAG
FROM
	GOPER
WHERE 
CODEMPRESA = ? AND CODOPER = ?
";


            dtPag = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlPag, new object[] { AppLib.Context.Empresa, CODTIPOPER, AppLib.Context.Empresa, CODTIPOPER, AppLib.Context.Empresa, CODTIPOPER, AppLib.Context.Empresa, CODOPER });
            cnpjcpf_pag = "";
            tpIntegra_pag = "";

            //nova validação para TPAG
            if (dtPag.Rows[0]["TPAG"].ToString() == "0")
                tPag_pag = "";
            else
                tPag_pag = dtPag.Rows[0]["TPAG"].ToString().PadLeft(2, '0');

            //nova validação para VPAG
            if (dtPag.Rows[0]["VPAG"].ToString() == "0")
                vPag_pag = "";
            else
                vPag_pag = nf_formata.trocarVirgulaPorPonto(nf_formata.casasDecimais_2(dtPag.Rows[0]["VPAG"].ToString().Trim()));

            if (tPag_pag == "90")
                vPag_pag = "0.00";


            if (tPag_pag == "03" || tPag_pag == "04")
            {
                tpIntegra_pag = "2";
                cnpjcpf_pag = ""; //puxar da credenciadora vclifor
            }
            else
            {
                tpIntegra_pag = "";
                cnpjcpf_pag = "";
            }


            sb.Append("		<pag>\n");
            sb.Append("			<detPag>\n");
            sb.Append("				<tPag>" + tPag_pag + "</tPag>\n");
            sb.Append("				<vPag>" + vPag_pag + "</vPag>\n");
            //sb.Append("				<card>\n");
            //sb.Append("					<CNPJ>"+ cnpjcpf_pag + "</CNPJ>\n"); // Caso escolha essa tag, a tpIntegra deve ser preenchida.
            //sb.Append("					<tBand></tBand>\n");
            //sb.Append("					<cAut></cAut>\n");
            //sb.Append("					<tpIntegra>"+tpIntegra_pag+"</tpIntegra>\n");// obrigatorio se escolher a tag CNPJ.
            //sb.Append("				</card>\n");
            sb.Append("			</detPag>\n");
            sb.Append("			<vTroco></vTroco>\n");
            sb.Append("		</pag>\n");



            //Informações complementares da NF
            sb.Append("		<infAdic>\n");
            sb.Append("			<infAdFisco></infAdFisco>\n");
            sb.Append("			<infCpl>" + infAdic_infCpl.Trim() + "</infCpl>\n");
            sb.Append("			<obsCont>\n");
            sb.Append("				<xCampo></xCampo>\n");
            sb.Append("				<xTexto></xTexto>\n");
            sb.Append("			</obsCont>\n");
            sb.Append("			<obsFisco>\n");
            sb.Append("				<xCampo></xCampo>\n");
            sb.Append("				<xTexto></xTexto>\n");
            sb.Append("			</obsFisco>\n");
            sb.Append("			<procRef>\n");
            sb.Append("				<nProc></nProc>\n");
            sb.Append("				<indProc></indProc>\n");
            sb.Append("			</procRef>\n");
            sb.Append("		</infAdic>\n");


            /*
            sb.Append("		<exporta>\n");
            sb.Append("			<UFSaidaPais></UFSaidaPais>\n");
            sb.Append("			<xLocExporta></xLocExporta>\n");
            sb.Append("			<xLocDespacho></xLocDespacho>\n");
            sb.Append("		</exporta>\n");
            */

            /*
            sb.Append("		<compra>\n");
            sb.Append("			<xNEmp></xNEmp>\n");
            sb.Append("			<xPed></xPed>\n");
            sb.Append("			<xCont></xCont>\n");
            sb.Append("		</compra>\n");
            */


            /*
            sb.Append("		<cana>\n");
            sb.Append("			<safra></safra>\n");
            sb.Append("			<ref></ref>\n");
            sb.Append("			<forDia>\n");
            sb.Append("				<dia></dia>\n");
            sb.Append("				<qtde></qtde>\n");
            sb.Append("				<qTotMes></qTotMes>\n");
            sb.Append("				<qTotAnt></qTotAnt>\n");
            sb.Append("				<qTotGer></qTotGer>\n");
            sb.Append("			</forDia>\n");
            sb.Append("			<deduc>\n");
            sb.Append("				<xDed></xDed>\n");
            sb.Append("				<vDed></vDed>\n");
            sb.Append("				<vFor></vFor>\n");
            sb.Append("				<vTotDed></vTotDed>\n");
            sb.Append("				<vLiqFor></vLiqFor>\n");
            sb.Append("			</deduc>\n");
            sb.Append("		</cana>\n");
            */

            sb.Append("	</infNFe>\n");
            sb.Append("</NFe>\n");

            //devolve as aspas
            sb.Replace("'", "\"");

            sbFinal = sb.ToString().Trim();

            sbFinal = nf_formata.limparXMLTagVazia(sb.ToString().Trim());
            //sbFinal = nf_formata.limparXMLTagVazia(sbFinal);
            //sbFinal = nf_formata.limparXMLTagVazia(sbFinal);

            //Força a inserção da tag idEstrangeiro mesmo vazia
            if(UsaNfeImportacao)
             sbFinal = sbFinal.Insert(sbFinal.IndexOf("<dest>")+6, "\n<idEstrangeiro />");

            sbFinal = nf_formata.trataCaracteres(sbFinal);

            int Insert = 0;

            try
            {
                Insert = nf_formata.InsertGnfestadual(Convert.ToInt32(CODOPER), chaveNfe);
            }
            catch (Exception)
            { }

            if (Insert > 0)
            {
                return sbFinal;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Não foi possível gerar o XML. \r\n Para mais informações consulte o suporte técnico.", "Informação do Sistema", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return string.Empty;
            }
        }

        #endregion
    }
}