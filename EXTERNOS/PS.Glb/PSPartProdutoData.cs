using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace PS.Glb
{
    public class PSPartProdutoData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Glb.Formula.Function function = new PS.Glb.Formula.Function();
        private PS.Lib.Interpretador interpreta = new PS.Lib.Interpretador();

        public override string ReadView()
        {
            return @"SELECT 
CODEMPRESA,
CODPRODUTO,
CODIGOAUXILIAR,
ULTIMONIVEL,
NOME,
PRODSERV,
CONVERT(BIT, ATIVO) ATIVO,
CODMOEDA1,
PRECO1,
CODMOEDA2,
PRECO2,
CODUNIDVENDA,
CODUNIDCOMPRA,
CODUNIDCONTROLE,
CODFABRICANTE,
CODIMAGEM,
DESCRICAO,
CODNCM,
CODNCMEX,
CODTIPO,
PESOBRUTO,
PESOLIQUIDO,
COMPRIMENTO,
LARGURA,
ESPESSURA,
DIAMETRO,
NOMEFANTASIA,
CODPRDFABRICANTE,
PRECO3,
PRECO4,
PRECO5,
CODMOEDA3,
CODMOEDA4,
CODMOEDA5,
CUSTOUNITARIO,
DATACUSTOUNITARIO,
CUSTOMEDIO,
DATACUSTOMEDIO,
PROCEDENCIA,
NUMREGMINAGRI,
CEST,
LEADTIME,
LOCALESTOCAGEM,
ESTOQUEMINIMO,
ESTOQUEMAXIMO,
CODCLASSIFICACAO
FROM VPRODUTO WHERE ";
        }

        public void InsertRegistroComplementar(List<PS.Lib.DataField> objArr)
        {
            PSPartProdutoComplData psPartProdutoComplData = new PSPartProdutoComplData();
            psPartProdutoComplData._tablename = "VPRODUTOCOMPL";
            psPartProdutoComplData._keys = new string[] { "CODEMPRESA", "CODPRODUTO" };

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODPRODUTO = gb.RetornaDataFieldByCampo(objArr, "CODPRODUTO");

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
            ListObjArr.Add(dfCODEMPRESA);
            ListObjArr.Add(dfCODPRODUTO);

            psPartProdutoComplData.SaveRecord(ListObjArr);

            /*
            string sSql = @"INSERT INTO VPRODUTOCOMPL (CODEMPRESA, CODPRODUTO) VALUES(?,?)";

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODPRODUTO = gb.RetornaDataFieldByCampo(objArr, "CODPRODUTO");

            dbs.QueryExec(sSql, dfCODEMPRESA.Valor, dfCODPRODUTO.Valor);
            */
        }

        public void ExcluiRegistroComplementar(List<PS.Lib.DataField> objArr)
        {
            PSPartProdutoComplData psPartProdutoComplData = new PSPartProdutoComplData();
            psPartProdutoComplData._tablename = "VPRODUTOCOMPL";
            psPartProdutoComplData._keys = new string[] { "CODEMPRESA", "CODPRODUTO" };

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODPRODUTO = gb.RetornaDataFieldByCampo(objArr, "CODPRODUTO");

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
            ListObjArr.Add(dfCODEMPRESA);
            ListObjArr.Add(dfCODPRODUTO);

            psPartProdutoComplData.DeleteRecord(ListObjArr);

            /*
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODPRODUTO = gb.RetornaDataFieldByCampo(objArr, "CODPRODUTO");

            sSql = @"DELETE FROM VPRODUTOCOMPL WHERE CODEMPRESA = ? AND CODPRODUTO = ?";

            dbs.QueryExec(sSql, dtCODEMPRESA.Valor, dtCODPRODUTO.Valor);
            */
        }

        public void ConsisteUnidadeControle(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODPRODUTO = gb.RetornaDataFieldByCampo(objArr, "CODPRODUTO");
            PS.Lib.DataField dfCODUNIDCONTROLE = gb.RetornaDataFieldByCampo(objArr, "CODUNIDCONTROLE");

            if (dfCODUNIDCONTROLE == null)
                throw new Exception("Unidade de Controle não foi informada.");

            if (dfCODUNIDCONTROLE.Valor == null)
                throw new Exception("Unidade de Controle não foi informada.");

            sSql = @"SELECT CODUNIDCONTROLE FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?";

            string CODUNIDCONTROLE_ANTEIOR = dbs.QueryValue("", sSql, dfCODEMPRESA.Valor, dfCODPRODUTO.Valor).ToString();

            if (CODUNIDCONTROLE_ANTEIOR != dfCODUNIDCONTROLE.Valor.ToString())
            {
                sSql = @"SELECT SALDO FROM VLOCALESTOQUESALDO WHERE CODEMPRESA = ? AND CODPRODUTO = ?";

                decimal saldo = Convert.ToDecimal(dbs.QueryValue(0, sSql, dfCODEMPRESA.Valor, dfCODPRODUTO.Valor).ToString());

                if (saldo > 0)
                    throw new Exception("Unidade de Controle não pode ser alterada pois o produto possui saldo em estoque.");
            }
        }

        public void ExcluiProdutoComposto(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODPRODUTO = gb.RetornaDataFieldByCampo(objArr, "CODPRODUTO");

            string sSql = @"DELETE FROM VPRODUTOCOM WHERE CODEMPRESA = ? AND CODPRODUTO = ?";

            dbs.QueryExec(sSql, dfCODEMPRESA.Valor, dtCODPRODUTO.Valor);
        }

        public void ExcluiTributoProduto(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODPRODUTO = gb.RetornaDataFieldByCampo(objArr, "CODPRODUTO");

            string sSql = @"DELETE FROM VPRODUTOTRIBUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?";

            dbs.QueryExec(sSql, dfCODEMPRESA.Valor, dtCODPRODUTO.Valor);
        }

        public void ExcluiTributoProdutoEstado(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODPRODUTO = gb.RetornaDataFieldByCampo(objArr, "CODPRODUTO");

            string sSql = @"DELETE FROM VPRODUTOTRIBUTOESTADO WHERE CODEMPRESA = ? AND CODPRODUTO = ?";

            dbs.QueryExec(sSql, dfCODEMPRESA.Valor, dtCODPRODUTO.Valor);
        }

        public void ExcluiRegistroLocalEstoque(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODPRODUTO = gb.RetornaDataFieldByCampo(objArr, "CODPRODUTO");

            string sSql = @"DELETE FROM VLOCALESTOQUESALDO WHERE CODEMPRESA = ? AND CODPRODUTO = ?";

            dbs.QueryExec(sSql, dfCODEMPRESA.Valor, dtCODPRODUTO.Valor);
        }

        public decimal RetornaPrecoProduto(int CodEmpresa, string CodProduto, string TabPreco)
        {
            try
            {
                if(string.IsNullOrEmpty(TabPreco) || TabPreco == "NAOUSA" || TabPreco == "NENHUM")
                {
                    return 0;                                    
                }
                else
                {
                    string sSql = string.Concat("SELECT ", TabPreco ," FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?");
                    return Convert.ToDecimal(dbs.QueryValue(0, sSql, CodEmpresa, CodProduto));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string RetornaProximoCodProduto(int CodEmpresa, string CodProduto)
        {
            string CodProdutoNovo = string.Empty;
            System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();
            if (PARAMVAREJO == null)
            {
                throw new Exception("Não foi possível carregar os parâmetros do módulo.");
            }
            else
            {
                string mask = (PARAMVAREJO["MASKPRODSERV"] == DBNull.Value) ? string.Empty : PARAMVAREJO["MASKPRODSERV"].ToString();
                if (mask != string.Empty)
                {
                    string sSql = "SELECT CODPRODUTO FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?";
                    if (this.dbs.QueryFind(sSql, CodEmpresa, CodProduto))
                    {
                        string code = CodProduto;
                        if (code.Length < mask.Length)
                        {
                            if (mask.Substring(code.Length, 1) == ".")
                            {
                                Regex rSplit = new Regex(";");
                                string[] sMask = rSplit.Split(mask.Replace('.', ';'));
                                int nivelMask = sMask.Length;
                                string[] sCode = rSplit.Split(code.Replace('.', ';'));
                                int nivelCode = sCode.Length;
                                if (nivelCode < nivelMask)
                                {
                                    string CodProdutoProximoMask = string.Concat(CodProduto, ".", sMask[nivelCode].Replace('#', '_'));
                                    sSql = "SELECT MAX(CODPRODUTO) CODPRODUTO FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO LIKE ?";
                                    string CodProdutoUltimo = dbs.QueryValue(string.Empty, sSql, CodEmpresa, CodProdutoProximoMask).ToString();
                                    //aqui
                                    if (!string.IsNullOrEmpty(CodProdutoUltimo))
                                    {
                                        string[] sCodeUlt = rSplit.Split(CodProdutoUltimo.Replace('.', ';'));
                                        if (sCodeUlt.Length > 0)
                                        {
                                            int UltinoNum = Convert.ToInt32(sCodeUlt[nivelCode]);
                                            UltinoNum++;
                                            CodProdutoNovo = string.Concat(code, ".", UltinoNum.ToString().PadLeft(sCodeUlt[nivelCode].Length, '0'));
                                        }
                                        else
                                        {
                                            CodProdutoNovo = CodProduto;
                                        }
                                    }
                                    else
                                    {
                                        CodProdutoNovo = CodProduto + ".0001";
                                    }
                                }
                                else
                                {
                                    CodProdutoNovo = CodProduto;
                                }
                            }
                            else
                            {
                                CodProdutoNovo = CodProduto;
                            }
                        }
                        else
                        {
                            CodProdutoNovo = CodProduto;
                        }
                    }
                    else
                    {
                        CodProdutoNovo = CodProduto;
                    }
                }
            }

            return CodProdutoNovo;
        }

        public override void ValidateRecord(List<PS.Lib.DataField> objArr)
        {
            base.ValidateRecord(objArr);

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODPRODUTO = gb.RetornaDataFieldByCampo(objArr, "CODPRODUTO");

            #region Valida Máscara

            System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();
            if (PARAMVAREJO == null)
            {
                throw new Exception("Não foi possível carregar os parâmetros do módulo.");
            }
            else
            {
                string mask = (PARAMVAREJO["MASKPRODSERV"] == DBNull.Value) ? string.Empty : PARAMVAREJO["MASKPRODSERV"].ToString();
                if(mask != string.Empty)
                {
                    string CodProduto = dtCODPRODUTO.Valor.ToString();
                    if (CodProduto.Length <= mask.Length)
                    {
                        for (int i = 0; i < CodProduto.Length; i++)
                        {
                            if (PS.Lib.Utils.MascaraCaracterValido(CodProduto[i], mask, i))
                            {
                                if (i == (CodProduto.Length - 1) && CodProduto[i] == '.')
                                {
                                    throw new Exception("O código do produto não pode terminar com o caracter separador da máscara parametrizada.");
                                }
                            }
                            else
                            {
                                throw new Exception("O código do produto não está consistente com a máscara parametrizada.");
                            }
                        }

                        if (CodProduto.Length < mask.Length)
                        {
                            if (mask.Substring(CodProduto.Length, 1) != ".")
                            {
                                throw new Exception("O código do produto não está consistente com a máscara parametrizada.");
                            }                        
                        }

                        string CodProdutoPai = PS.Lib.Utils.MascaraNivelAnterior(CodProduto);
                        if(!string.IsNullOrEmpty(CodProdutoPai))
                        {
                            string sSql = @"SELECT CODPRODUTO FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?";
                            if (!dbs.QueryFind(sSql, dtCODEMPRESA.Valor, CodProdutoPai))
                            {
                                throw new Exception("O código do produto informado não possui nível anterior.");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("O código do produto não pode ser maior que a máscara parametrizada.");
                    }
                }
            }

            #endregion

            ConsisteUnidadeControle(objArr);

            #region Formula de Validação
            /*
            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");

            string sSql = @"SELECT CODFORMULAVALIDAPRODUTO FROM VPARAMETROS WHERE CODEMPRESA = ?";

            System.Data.DataTable PARAMETROS = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor);

            if (PARAMETROS.Rows.Count > 0)
            {
                if (PARAMETROS.Rows[0]["CODFORMULAVALIDAPRODUTO"].ToString() != string.Empty)
                {
                    PS.Lib.Contexto.Session.Current = objArr;

                    int FRM_CODEMPRESA = int.Parse(dfCODEMPRESA.Valor.ToString());
                    string FRM_CODFORMULA = PARAMETROS.Rows[0]["CODFORMULAVALIDAPRODUTO"].ToString();

                    interpreta.comando = function.PreparaFormula(FRM_CODEMPRESA, FRM_CODFORMULA);
                    bool ERRO = Convert.ToBoolean(interpreta.Executar());

                    PS.Lib.Contexto.Session.Current = null;

                    if (!ERRO)
                    {
                        sSql = @"SELECT DESCRICAO FROM GFORMULA WHERE CODEMPRESA = ? AND CODFORMULA = ?";

                        string FRM_DESCRICAO = dbs.QueryValue("", sSql, FRM_CODEMPRESA, FRM_CODFORMULA).ToString();

                        throw new Exception(string.Concat("Formula de Validação : ", FRM_CODFORMULA, "\n", FRM_DESCRICAO));
                    }
                }
            }
            */
            #endregion
        }

        public override List<PS.Lib.DataField> InsertRecord(List<PS.Lib.DataField> objArr)
        {
            List<PS.Lib.DataField> objArrDDL = objArr;

            List<PS.Lib.DataField> temp = base.InsertRecord(objArr);

            //inseri o vproduto fiscal
            //BUSCAR 

            InsertRegistroComplementar(objArrDDL);
            

            return temp;
        }
        private void insertProdutoFiscal(string codNCM, string codProduto)
        {
            //VERIFICAR SE O CODNCM NÃO TEM NO OBJETO AO INVÉS DE BUSCA NA TABELA
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT NACIONALFEDERAL, IMPORTADOSFEDERAL, ESTADUAL, MUNICIPAL, CHAVE, UF FROM VIBPTAX WHERE CODIGO = ?", new object[] { codNCM });
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO VPRODUTOFISCAL (CODEMPRESA, CODPRODUTO, CODETD, ALIQUOTAINTICMS, PERCREDUCAOICMS, PERCMARGEMST, MODDETBCICMS, MODDETBCICMSST, CHAVEIBPT, ALIQUOTAIBPTFEDERAL, ALIQUOTAIBPTESTADUAL, ALIQUOTAIBPTMUNICIPAL) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)", new object[] { AppLib.Context.Empresa, codProduto, dt.Rows[i]["UF"], "0", "0", "0", "3", "4", dt.Rows[i]["CHAVE"], dt.Rows[i]["NACIONALFEDERAL"], dt.Rows[i]["ESTADUAL"], dt.Rows[i]["MUNICIPAL"] });
                
            }
        }
        public override void DeleteRecord(List<PS.Lib.DataField> objArr)
        {
            this.ValidateKeyRecord(objArr);

            ExcluiRegistroComplementar(objArr);
            ExcluiProdutoComposto(objArr);
            ExcluiTributoProduto(objArr);
            ExcluiTributoProdutoEstado(objArr);
            ExcluiRegistroLocalEstoque(objArr);

            base.DeleteRecord(objArr);
        }
    }
}
