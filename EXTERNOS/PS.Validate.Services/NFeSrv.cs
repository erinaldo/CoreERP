using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using PS.Lib;

//namespace ValidateLib
namespace PS.Validate.Services
{
    public class NFeSrv
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private AppLib.Data.Connection _conn;
        public bool usaCodAuxiliar = false;
        public string Codtipoper = string.Empty;


        private void InitValidateServer()
        {
            System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();
            if (PARAMVAREJO == null)
            {
                throw new Exception("Não foi possível carregar os parâmetros do módulo.");
            }
            else
            {
                string ServerName = (PARAMVAREJO["VALIDATESERVERNAME"] == DBNull.Value) ? null : PARAMVAREJO["VALIDATESERVERNAME"].ToString();
                string DataBaseName = (PARAMVAREJO["VALIDATEDATABASENAME"] == DBNull.Value) ? null : PARAMVAREJO["VALIDATEDATABASENAME"].ToString();
                string UserName = (PARAMVAREJO["VALIDATEUSERNAME"] == DBNull.Value) ? null : PARAMVAREJO["VALIDATEUSERNAME"].ToString();
                string Password = (PARAMVAREJO["VALIDATEPASSWORD"] == DBNull.Value) ? null : PARAMVAREJO["VALIDATEPASSWORD"].ToString();

                if (string.IsNullOrEmpty(ServerName) || string.IsNullOrEmpty(DataBaseName) || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                {
                    throw new Exception("A integração do Validate não está parametrizada corretamente, verifique se todos os campos foram preenchidos.");
                }
                else
                {
                    AppLib.Context.poolConnection.Add("Start", AppLib.Global.Types.Database.SqlClient, new AppLib.Data.AssistantConnection().SqlClient(ServerName, DataBaseName, UserName, Password));

                    Boolean testou = AppLib.Context.poolConnection.Get("Start").Test();

                    if (testou)
                    {
                        _conn = AppLib.Context.poolConnection.Get("Start");
                    }
                    else
                    {
                        throw new Exception("Erro de conexão com o banco de dados do APP VALIDATE");
                    }
                }
            }
        }

        public ValidateLib.EmpresaParams GetParamEmpresaValidate(string CNPJ)
        {
            try
            {
                return ValidateLib.EmpresaParams.ReadByCNPJ(CNPJ);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GerarDigitoVerificadorNFe(string chave)
        {
            string sDV = string.Empty;
            int soma = 0; // Vai guardar a Soma
            int mod = -1; // Vai guardar o Resto da divisão
            int dv = -1;  // Vai guardar o DigitoVerificador
            int pesso = 2; // vai guardar o pesso de multiplicacao

            //percorrendo cada caracter da chave da direita para esquerda para fazer os calculos com o pesso
            for (int i = chave.Length - 1; i != -1; i--)
            {
                int ch = Convert.ToInt32(chave[i].ToString());
                soma += ch * pesso;
                //sempre que for 9 voltamos o pesso a 2
                if (pesso < 9)
                    pesso += 1;
                else
                    pesso = 2;
            }

            //Agora que tenho a soma vamos pegar o resto da divisão por 11
            mod = soma % 11;
            //Aqui temos uma regrinha, se o resto da divisão for 0 ou 1 então o dv vai ser 0
            if (mod == 0 || mod == 1)
                dv = 0;
            else
                dv = 11 - mod;

            sDV = dv.ToString();
            return sDV;
        }

        public string GerarChaveAcessoNFe(int cUF, string AAMM, string CNPJ, string mod, string serie, string nNF, int tpEmis, string cNF)
        {
            if (cUF.ToString().Length != 2)
                throw new Exception("");
            if (AAMM.Length != 4)
                throw new Exception("");
            if (CNPJ.Length != 14)
                throw new Exception("");
            if (mod.Length != 2)
                throw new Exception("");
            if (serie.Length != 3)
                serie = serie.PadLeft(3, '0');
            if (nNF.Length != 9)
                nNF = nNF.PadLeft(9, '0');
            if (tpEmis.ToString().Length != 1)
                throw new Exception("");
            if (cNF.Length != 8)
                throw new Exception("");

            string chave = string.Concat(cUF, AAMM, CNPJ, mod, serie, nNF, tpEmis, cNF);
            string chaveacesso = string.Concat(chave, this.GerarDigitoVerificadorNFe(chave));

            return chaveacesso;
        }

        public string GetDescricaoCFOP(int CodEmpresa, int CodOper)
        {
            try
            {
                string CodNatureza = string.Empty;
                string sSql = @"SELECT DISTINCT(CODNATUREZA) FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?";
                DataTable table = dbs.QuerySelect(sSql, CodEmpresa, CodOper);
                if (table.Rows.Count <= 0)
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois não foi informado a CFOP na operação.");
                }

                if (table.Rows.Count > 1)
                {
                    CodNatureza = table.Rows[0]["CODNATUREZA"].ToString().Substring(0, 1);
                }
                else
                {
                    CodNatureza = table.Rows[0]["CODNATUREZA"].ToString();
                }

                sSql = @"SELECT DESCRICAO FROM VNATUREZA WHERE CODEMPRESA = ? AND CODNATUREZA = ?";
                return dbs.QueryValue(string.Empty, sSql, CodEmpresa, CodNatureza).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataRow GetNFEstadualRow(int CodEmpresa, int CodOper)
        {
            try
            {
                string sSql = @"SELECT * FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?";
                DataTable table = dbs.QuerySelect(sSql, CodEmpresa, CodOper);
                if (table.Rows.Count > 1)
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados mais de um registro para a tabela: GOPER.");
                }
                else
                {
                    if (table.Rows.Count <= 0)
                    {
                        throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados registros para a tabela: GOPER.");
                    }
                    else
                    {
                        return table.Rows[0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataRow GetOperacaoRow(int CodEmpresa, int CodOper)
        {
            try
            {
                string sSql = @"SELECT * FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";
                DataTable table = dbs.QuerySelect(sSql, CodEmpresa, CodOper);
                if (table.Rows.Count > 1)
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados mais de um registro para a tabela: GOPER.");
                }
                else
                {
                    if (table.Rows.Count <= 0)
                    {
                        throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados registros para a tabela: GOPER.");
                    }
                    else
                    {
                        return table.Rows[0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataRow GetTotalRow(int CodEmpresa, int CodOper)
        {
            try
            {
                string sSql = @"SELECT

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

ISNULL((
SELECT SUM(GOPERITEMTRIBUTO.VALOR)
FROM GOPERITEMTRIBUTO, VTRIBUTO
WHERE GOPERITEMTRIBUTO.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEMTRIBUTO.CODOPER = GOPER.CODOPER
AND GOPERITEMTRIBUTO.CODEMPRESA = VTRIBUTO.CODEMPRESA
AND GOPERITEMTRIBUTO.CODTRIBUTO = VTRIBUTO.CODTRIBUTO
AND VTRIBUTO.CODTIPOTRIBUTO = 'II'
),0) VII,

ISNULL((
SELECT SUM(GOPERITEMTRIBUTO.VALOR)
FROM GOPERITEMTRIBUTO, VTRIBUTO
WHERE GOPERITEMTRIBUTO.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEMTRIBUTO.CODOPER = GOPER.CODOPER
AND GOPERITEMTRIBUTO.CODEMPRESA = VTRIBUTO.CODEMPRESA
AND GOPERITEMTRIBUTO.CODTRIBUTO = VTRIBUTO.CODTRIBUTO
AND VTRIBUTO.CODTIPOTRIBUTO = 'IPI'
),0) VIPI,

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

ISNULL((
SELECT SUM(GOPERITEMTRIBUTO.VALOR)
FROM GOPERITEMTRIBUTO, VTRIBUTO
WHERE GOPERITEMTRIBUTO.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEMTRIBUTO.CODOPER = GOPER.CODOPER
AND GOPERITEMTRIBUTO.CODEMPRESA = VTRIBUTO.CODEMPRESA
AND GOPERITEMTRIBUTO.CODTRIBUTO = VTRIBUTO.CODTRIBUTO
),0) VTOTTRIB,

ISNULL((
SELECT SUM(GOPERITEM.VLTOTALITEM)
FROM GOPERITEM
WHERE GOPERITEM.CODEMPRESA = GOPER.CODEMPRESA
AND GOPERITEM.CODOPER = GOPER.CODOPER
),0) VPROD

FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?
";
                DataTable table = dbs.QuerySelect(sSql, CodEmpresa, CodOper);
                if (table.Rows.Count > 1)
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados mais de um registro para a tabela: GOPER.");
                }
                else
                {
                    if (table.Rows.Count <= 0)
                    {
                        throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados registros para a tabela: GOPER.");
                    }
                    else
                    {
                        return table.Rows[0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetItemOperacaoTable(int CodEmpresa, int CodOper)
        {
            try
            {
                string sSql = @"SELECT *,

ISNULL((
SELECT SUM(GOPERITEMTRIBUTO.VALOR)
FROM GOPERITEMTRIBUTO, VTRIBUTO
WHERE GOPERITEMTRIBUTO.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPERITEMTRIBUTO.CODOPER = GOPERITEM.CODOPER
AND GOPERITEMTRIBUTO.CODEMPRESA = VTRIBUTO.CODEMPRESA
AND GOPERITEMTRIBUTO.CODTRIBUTO = VTRIBUTO.CODTRIBUTO
),0) VTOTTRIB

FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?";
                DataTable table = dbs.QuerySelect(sSql, CodEmpresa, CodOper);
                if (table.Rows.Count <= 0)
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados registros para a tabela: GOPERITEM.");
                }
                else
                {
                    return table;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetItemTributoOperacaoTable(int CodEmpresa, int CodOper, int NSeqItem)
        {
            try
            {
                string sSql = @"SELECT *,
(SELECT CODTIPOTRIBUTO FROM VTRIBUTO WHERE CODTRIBUTO = GOPERITEMTRIBUTO.CODTRIBUTO AND CODEMPRESA = GOPERITEMTRIBUTO.CODEMPRESA) CODTIPOTRIBUTO
FROM GOPERITEMTRIBUTO
WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";
                DataTable table = dbs.QuerySelect(sSql, CodEmpresa, CodOper, NSeqItem);
                if (table.Rows.Count <= 0)
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados registros para a tabela: GOPERITEMTRIBUTO.");
                }
                else
                {
                    return table;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataRow GetFilialRow(int CodEmpresa, int CodFilial)
        {
            try
            {
                string sSql = @"SELECT *, 
(SELECT CODIBGE FROM GESTADO WHERE CODETD = GFILIAL.CODETD) CODIBGE,
(SELECT NOME FROM GCIDADE WHERE CODETD = GFILIAL.CODETD AND CODCIDADE = GFILIAL.CODCIDADE) CIDADE,
(SELECT CODBACEN FROM GPAIS WHERE CODPAIS = GFILIAL.CODPAIS) CODBACEN,
(SELECT NOME FROM GPAIS WHERE CODPAIS = GFILIAL.CODPAIS) PAIS,
(SELECT NOME FROM GTIPORUA WHERE CODTIPORUA = GFILIAL.CODTIPORUA) TIPORUA
FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?";
                DataTable table = dbs.QuerySelect(sSql, CodEmpresa, CodFilial);
                if (table.Rows.Count > 1)
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados mais de um registro para a tabela: GFILIAL.");
                }
                else
                {
                    if (table.Rows.Count <= 0)
                    {
                        throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados registros para a tabela: GFILIAL.");
                    }
                    else
                    {
                        return table.Rows[0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataRow GetTipoOperacaoRow(int CodEmpresa, string CodTipOper)
        {
            try
            {
                string sSql = @"SELECT * FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?";
                DataTable table = dbs.QuerySelect(sSql, CodEmpresa, CodTipOper);
                if (table.Rows.Count > 1)
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados mais de um registro para a tabela: GTIPOPER.");
                }
                else
                {
                    if (table.Rows.Count <= 0)
                    {
                        throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados registros para a tabela: GTIPOPER.");
                    }
                    else
                    {
                        return table.Rows[0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataRow GetCondicaoPagtoRow(int CodEmpresa, string CodCondicao)
        {
            try
            {
                string sSql = @"SELECT SUM(ISNULL(NUMPARCELAS,0)) NUMPARCELAS FROM VCONDICAOPGTOCOMPOSICAO WHERE CODEMPRESA = ? AND CODCONDICAO = ?";
                DataTable table = dbs.QuerySelect(sSql, CodEmpresa, CodCondicao);
                if (table.Rows.Count > 1)
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados mais de um registro para a tabela: VCONDICAOPGTO.");
                }
                else
                {
                    if (table.Rows.Count <= 0)
                    {
                        throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados registros para a tabela: VCONDICAOPGTO.");
                    }
                    else
                    {
                        return table.Rows[0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataRow GetDestinatarioRow(int CodEmpresa, string CodCliFor)
        {
            try
            {
                string sSql = @"SELECT *,
(SELECT CODIBGE FROM GESTADO WHERE CODETD = VCLIFOR.CODETD) CODIBGE,
(SELECT NOME FROM GCIDADE WHERE CODETD = VCLIFOR.CODETD AND CODCIDADE = VCLIFOR.CODCIDADE) CIDADE,
(SELECT CODBACEN FROM GPAIS WHERE CODPAIS = VCLIFOR.CODPAIS) CODBACEN,
(SELECT NOME FROM GPAIS WHERE CODPAIS = VCLIFOR.CODPAIS) PAIS,
(SELECT NOME FROM GTIPORUA WHERE CODTIPORUA = VCLIFOR.CODTIPORUA) TIPORUA
FROM VCLIFOR WHERE CODEMPRESA = ? AND CODCLIFOR = ?";
                DataTable table = dbs.QuerySelect(sSql, CodEmpresa, CodCliFor);
                if (table.Rows.Count > 1)
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados mais de um registro para a tabela: VCLIFOR.");
                }
                else
                {
                    if (table.Rows.Count <= 0)
                    {
                        throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados registros para a tabela: VCLIFOR.");
                    }
                    else
                    {
                        return table.Rows[0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataRow GetTransportadoraRow(int CodEmpresa, int CodTransportadora)
        {
            try
            {
                string sSql = @"SELECT *,
(SELECT CODIBGE FROM GESTADO WHERE CODETD = VTRANSPORTADORA.CODETD) CODIBGE,
(SELECT NOME FROM GCIDADE WHERE CODETD = VTRANSPORTADORA.CODETD AND CODCIDADE = VTRANSPORTADORA.CODCIDADE) CIDADE,
(SELECT CODBACEN FROM GPAIS WHERE CODPAIS = VTRANSPORTADORA.CODPAIS) CODBACEN,
(SELECT NOME FROM GPAIS WHERE CODPAIS = VTRANSPORTADORA.CODPAIS) PAIS,
(SELECT NOME FROM GTIPORUA WHERE CODTIPORUA = VTRANSPORTADORA.CODTIPORUA) TIPORUA
FROM VTRANSPORTADORA WHERE CODEMPRESA = ? AND CODTRANSPORTADORA = ?";
                DataTable table = dbs.QuerySelect(sSql, CodEmpresa, CodTransportadora);
                if (table.Rows.Count > 1)
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados mais de um registro para a tabela: VTRANSPORTADORA.");
                }
                else
                {
                    if (table.Rows.Count <= 0)
                    {
                        throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados registros para a tabela: VTRANSPORTADORA.");
                    }
                    else
                    {
                        return table.Rows[0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataRow GetProdutoServicoRow(int CodEmpresa, string CodCliFor)
        {
            try
            {
                string sSql = @"SELECT * FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?";
                DataTable table = dbs.QuerySelect(sSql, CodEmpresa, CodCliFor);
                if (table.Rows.Count > 1)
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados mais de um registro para a tabela: VPRODUTO.");
                }
                else
                {
                    if (table.Rows.Count <= 0)
                    {
                        throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados registros para a tabela: VPRODUTO.");
                    }
                    else
                    {
                        return table.Rows[0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetDuplicataTable(int CodEmpresa, int CodOper)
        {
            try
            {
                string sSql = @"SELECT * FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ?";
                DataTable table = dbs.QuerySelect(sSql, CodEmpresa, CodOper);
                if (table.Rows.Count < 0)
                {

                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois foram localizados registros para a tabela: FLANCA.");
                }
                else
                {
                    return table;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<DataField> EnviarNFe(int CodEmpresa, int CodOper)
        {
            try
            {
                // João Pedro Luchiari
                //this.InitValidateServer();

                _conn = AppLib.Context.poolConnection.Get("Start");

                string sSql = string.Empty;

                sSql = @"SELECT IDOUTBOX FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?";
                int IdOutBox = Convert.ToInt32(dbs.QueryValue(0, sSql, CodEmpresa, CodOper));

                DataRow rOperacao = this.GetOperacaoRow(CodEmpresa, CodOper);
                DataRow rTotal = this.GetTotalRow(CodEmpresa, CodOper);

                if (string.IsNullOrEmpty(rOperacao["CODFILIAL"].ToString()))
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois a filial não foi informada.");
                }

                if (Convert.ToInt32(rOperacao["CODFILIAL"]) == 0)
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois a filial não foi informada.");
                }

                DataRow rFilial = this.GetFilialRow(CodEmpresa, Convert.ToInt32(rOperacao["CODFILIAL"]));

                if (string.IsNullOrEmpty(rFilial["CGCCPF"].ToString()))
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois o CNPJ da filial não foi informada.");
                }

                //Código comentado pois não usa mais a tabela ZCONFIGEMP
                //ValidateLib.EmpresaParams empresaParams = this.GetParamEmpresaValidate(rFilial["CGCCPF"].ToString());
                //if (empresaParams.IdEmpresa <= 0)
                //{
                //    throw new Exception("Não foi possivel enviar a nf-e para autorização pois a filial não esta licenciada a emitir nota fiscal eletrônica.");
                //}

                DataRow rTipoOperacao = this.GetTipoOperacaoRow(CodEmpresa, rOperacao["CODTIPOPER"].ToString());

                if (string.IsNullOrEmpty(rTipoOperacao["USAOPERACAONFE"].ToString()))
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois o tipo de operação não esta parametrizado para utilizar nf-e.");
                }

                if (Convert.ToInt32(rTipoOperacao["USAOPERACAONFE"]) == 0)
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois o tipo de operação não esta parametrizado para utilizar nf-e.");
                }

                if (string.IsNullOrEmpty(rTipoOperacao["TIPOIMPRESSAODANFE"].ToString()))
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois o tipo de impressão da DANFE não esta parametrizada.");
                }

                if (string.IsNullOrEmpty(rTipoOperacao["MODDOCFISCAL"].ToString()))
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois a modalidade do documento fiscal não esta parametrizada.");
                }

                if (string.IsNullOrEmpty(rTipoOperacao["FINEMISSAONFE"].ToString()))
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois a finalidade de emissão da nf-e não esta parametrizada.");
                }

                if (string.IsNullOrEmpty(rTipoOperacao["TEXTOPRODNFE"].ToString()))
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois o texto do produto da nf-e não esta parametrizado.");
                }

                DataRow rCondicaoPagto = this.GetCondicaoPagtoRow(CodEmpresa, rOperacao["CODCONDICAO"].ToString());
                DataRow rDestinatario = this.GetDestinatarioRow(CodEmpresa, rOperacao["CODCLIFOR"].ToString());

                //try
                //{
                _conn.BeginTransaction();

                ValidateLib.OutBoxParams outBoxParams = new ValidateLib.OutBoxParams();
                outBoxParams.IdOutbox = IdOutBox;
                outBoxParams.CodEstrutura = "NF-e";
                outBoxParams.CNPJEmitente = Regex.Replace(rFilial["CGCCPF"].ToString(), "[^0-9a-zA-Z]+", "");
                outBoxParams.Data = dbs.GetServerDateTimeToday();
                outBoxParams.CodStatus = "RES";
                outBoxParams.Log = null;
                outBoxParams.DataUltimoLog = null;
                outBoxParams.Save();

                #region Documento

                ValidateLib.NFeDoc nfeDoc = new ValidateLib.NFeDoc();
                nfeDoc.IdOutbox = outBoxParams.IdOutbox;
                outBoxParams.nfeDoc = nfeDoc;

                #region Ide

                ValidateLib.NFeIde nfeIde = new ValidateLib.NFeIde();
                nfeIde.IdOutbox = outBoxParams.IdOutbox;
                nfeIde.cUF = Convert.ToInt32(rFilial["CODIBGE"]);
                nfeIde.cNF = new Random().Next(10000000, 100000000).ToString();
                nfeIde.natOp = PS.Lib.Utils.RemoveCaracterSpecial(this.GetDescricaoCFOP(CodEmpresa, CodOper)).Trim();
                nfeIde.indPag = (string.IsNullOrEmpty(rCondicaoPagto["NUMPARCELAS"].ToString())) ? 0 : (Convert.ToInt32(rCondicaoPagto["NUMPARCELAS"]) == 1) ? 0 : Convert.ToInt32(rCondicaoPagto["NUMPARCELAS"]);
                nfeIde.mod = (string.IsNullOrEmpty(rTipoOperacao["MODDOCFISCAL"].ToString())) ? null : rTipoOperacao["MODDOCFISCAL"].ToString().Trim();
                nfeIde.serie = (string.IsNullOrEmpty(rOperacao["CODSERIE"].ToString())) ? null : Convert.ToInt32(rOperacao["CODSERIE"]).ToString().Trim();
                nfeIde.nNF = Convert.ToInt32(rOperacao["NUMERO"]).ToString();
                nfeIde.dhEmi = Convert.ToDateTime(rOperacao["DATAEMISSAO"]);
                nfeIde.dhSaiEnt = string.IsNullOrEmpty(rOperacao["DATAENTSAI"].ToString()) ? AppLib.Context.poolConnection.Get("Start").GetDateTime() : Convert.ToDateTime(rOperacao["DATAENTSAI"]);
                nfeIde.tpNF = (rTipoOperacao["TIPENTSAI"].ToString() == "E") ? 0 : 1;

                nfeIde.idDest = (rFilial["CODETD"].ToString() == rDestinatario["CODETD"].ToString()) ? 1 : 2;

                if (rDestinatario["CODPAIS"].ToString() != "1")
                {
                    nfeIde.idDest = 3;
                }

                nfeIde.cMunFG = string.IsNullOrEmpty(rFilial["CODCIDADE"].ToString()) ? null : rFilial["CODCIDADE"].ToString();
                nfeIde.tpImp = Convert.ToInt32(rTipoOperacao["TIPOIMPRESSAODANFE"]);
                // Dirlei
                //nfeIde.tpEmis = 1;

                // João Pedro
                //nfeIde.tpEmis = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT MODALIDADE FROM VPARAMETROS WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }));
                nfeIde.tpEmis = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT MODALIDADE FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(rFilial["CODFILIAL"]) }));
                //nfeIde.tpAmb = empresaParams.TpAmb;
                // João Pedro
                //nfeIde.tpAmb = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM VPARAMETROS WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }));
                nfeIde.tpAmb = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(rFilial["CODFILIAL"]) }));
                nfeIde.finNFe = Convert.ToInt32(rTipoOperacao["FINEMISSAONFE"]);
                nfeIde.indFinal = string.IsNullOrEmpty(rOperacao["TIPOPERCONSFIN"].ToString()) ? 0 : Convert.ToInt32(rOperacao["TIPOPERCONSFIN"]);
                nfeIde.indPres = string.IsNullOrEmpty(rOperacao["CLIENTERETIRA"].ToString()) ? 0 : Convert.ToInt32(rOperacao["CLIENTERETIRA"]);
                nfeIde.procEmi = 0;
                nfeIde.verProc = PS.Lib.Contexto.Session.VersaoApp;

                #region Referencia

                DataTable dtNfeRef = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CHAVENFE FROM GOPERCOPIAREF WHERE CODOPERDESTINO = ? AND CODEMPRESA = ?", new object[] { CodOper, AppLib.Context.Empresa });
                if (dtNfeRef.Rows.Count > 0)
                {
                    ValidateLib.OBJETOS.NFeRef nferef = new ValidateLib.OBJETOS.NFeRef();
                    for (int i = 0; i < dtNfeRef.Rows.Count; i++)
                    {
                        nferef.IDOUTBOX = outBoxParams.IdOutbox;
                        nferef.CHAVENFEREF = dtNfeRef.Rows[i]["CHAVENFE"].ToString();
                        nferef.salvar();
                    }
                }

                #endregion



                #endregion

                #region Emit

                ValidateLib.NFeEmit nfeEmit = new ValidateLib.NFeEmit();
                nfeEmit.IdOutbox = outBoxParams.IdOutbox;

                if (rFilial["CGCCPF"].ToString().Length == 18)
                    nfeEmit.CNPJ = Regex.Replace(rFilial["CGCCPF"].ToString(), "[^0-9a-zA-Z]+", "");
                else
                    nfeEmit.CPF = Regex.Replace(rFilial["CGCCPF"].ToString(), "[^0-9a-zA-Z]+", "");

                nfeEmit.xNome = string.IsNullOrEmpty(rFilial["NOME"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rFilial["NOME"].ToString()).Trim();
                nfeEmit.xFant = string.IsNullOrEmpty(rFilial["NOMEFANTASIA"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rFilial["NOMEFANTASIA"].ToString()).Trim();
                nfeEmit.xLgr = string.IsNullOrEmpty(rFilial["RUA"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rFilial["TIPORUA"].ToString() + " " + rFilial["RUA"].ToString()).Trim();
                nfeEmit.nro = string.IsNullOrEmpty(rFilial["NUMERO"].ToString()) ? null : rFilial["NUMERO"].ToString().Trim();
                nfeEmit.xCpl = (string.IsNullOrEmpty(rFilial["COMPLEMENTO"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rFilial["COMPLEMENTO"].ToString()).Trim());
                nfeEmit.xBairro = string.IsNullOrEmpty(rFilial["BAIRRO"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rFilial["BAIRRO"].ToString()).Trim();
                nfeEmit.cMun = string.IsNullOrEmpty(rFilial["CODCIDADE"].ToString()) ? null : rFilial["CODCIDADE"].ToString();
                nfeEmit.xMun = string.IsNullOrEmpty(rFilial["CIDADE"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rFilial["CIDADE"].ToString()).Trim();
                nfeEmit.UF = string.IsNullOrEmpty(rFilial["CODETD"].ToString()) ? null : rFilial["CODETD"].ToString();
                nfeEmit.CEP = string.IsNullOrEmpty(rFilial["CEP"].ToString()) ? null : Regex.Replace(rFilial["CEP"].ToString(), "[^0-9a-zA-Z]+", "").Trim();
                nfeEmit.cPais = Convert.ToInt32(rFilial["CODBACEN"]);
                nfeEmit.xPais = string.IsNullOrEmpty(rFilial["PAIS"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rFilial["PAIS"].ToString()).Trim();
                nfeEmit.fone = string.IsNullOrEmpty(rFilial["TELEFONE"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rFilial["TELEFONE"].ToString()).Trim();
                nfeEmit.IE = string.IsNullOrEmpty(rFilial["INSCRICAOESTADUAL"].ToString()) ? null : Regex.Replace(rFilial["INSCRICAOESTADUAL"].ToString(), "[^0-9a-zA-Z]+", "").Trim();
                nfeEmit.CRT = Convert.ToInt32(rFilial["CODREGIMETRIBUTARIO"]);
                nfeDoc.nfeEmit = nfeEmit;
                
                #endregion

                nfeDoc.Chave = this.GerarChaveAcessoNFe(nfeIde.cUF,
                    string.Concat(nfeIde.dhEmi.Year.ToString().Substring(2, 2), nfeIde.dhEmi.Month.ToString().PadLeft(2, '0')),
                    (nfeEmit.CNPJ == null) ? nfeEmit.CPF : nfeEmit.CNPJ,
                    nfeIde.mod, nfeIde.serie, nfeIde.nNF, nfeIde.tpEmis, nfeIde.cNF);

                nfeIde.cDV = Convert.ToInt32(nfeDoc.Chave.Substring(43, 1));
                nfeDoc.nfeIde = nfeIde;

                if (nfeIde.indPres == 1)
                {
                    #region Retirada
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CGCCPF,  (GTIPORUA.NOME + ' ' + RUA + ', ' + NUMERO) LOGR, BAIRRO, GFILIAL.CODCIDADE, GCIDADE.NOME, GFILIAL.CODETD  FROM GFILIAL 
INNER JOIN GTIPORUA ON GFILIAL.CODTIPORUA = GTIPORUA.CODTIPORUA
INNER JOIN GCIDADE ON GFILIAL.CODCIDADE = GCIDADE.CODCIDADE
WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { rOperacao["CODEMPRESA"], rOperacao["CODFILIAL"] });
                    ValidateLib.NFeRetirada retirada = new ValidateLib.NFeRetirada();
                    retirada.CNPJ = dt.Rows[0]["CGCCPF"].ToString().Replace(".", "").Replace("/", "").Replace("-", "");
                    retirada.xLgr = dt.Rows[0]["LOGR"].ToString();
                    retirada.xBairro = dt.Rows[0]["BAIRRO"].ToString();
                    retirada.cMun = dt.Rows[0]["CODCIDADE"].ToString();
                    retirada.xMun = dt.Rows[0]["NOME"].ToString();
                    retirada.UF = dt.Rows[0]["CODETD"].ToString();

                    nfeDoc.nfeRetirada = retirada;
                    #endregion
                }


                #region Dest

                ValidateLib.NFeDest nfeDest = new ValidateLib.NFeDest();
                nfeDest.IdOutbox = outBoxParams.IdOutbox;

                //Verifica se o campo idEstrangeiro está preenchido, se estiver cnpj e cpf não são preenchidos.
                if (!string.IsNullOrEmpty(rDestinatario["IDESTRANGEIRO"].ToString()))
                {
                    nfeDest.idEstrangeiro = rDestinatario["IDESTRANGEIRO"].ToString();
                }
                else
                {
                    if (rDestinatario["CGCCPF"].ToString().Length == 18)
                        nfeDest.CNPJ = Regex.Replace(rDestinatario["CGCCPF"].ToString(), "[^0-9a-zA-Z]+", "");
                    else
                        nfeDest.CPF = Regex.Replace(rDestinatario["CGCCPF"].ToString(), "[^0-9a-zA-Z]+", "");
                }



                nfeDest.xNome = string.IsNullOrEmpty(rDestinatario["NOME"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rDestinatario["NOME"].ToString()).Trim();
                nfeDest.xLgr = string.IsNullOrEmpty(rDestinatario["RUA"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rDestinatario["TIPORUA"].ToString() + " " + rDestinatario["RUA"].ToString()).Trim();
                nfeDest.nro = string.IsNullOrEmpty(rDestinatario["NUMERO"].ToString()) ? null : rDestinatario["NUMERO"].ToString().Trim();
                nfeDest.xCpl = string.IsNullOrEmpty(rDestinatario["COMPLEMENTO"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rDestinatario["COMPLEMENTO"].ToString()).Trim();
                nfeDest.xBairro = string.IsNullOrEmpty(rDestinatario["BAIRRO"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rDestinatario["BAIRRO"].ToString()).Trim();
                nfeDest.cMun = string.IsNullOrEmpty(rDestinatario["CODCIDADE"].ToString()) ? null : rDestinatario["CODCIDADE"].ToString();
                nfeDest.xMun = string.IsNullOrEmpty(rDestinatario["CIDADE"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rDestinatario["CIDADE"].ToString()).Trim();
                nfeDest.UF = string.IsNullOrEmpty(rDestinatario["CODETD"].ToString()) ? null : rDestinatario["CODETD"].ToString();
                nfeDest.CEP = string.IsNullOrEmpty(rDestinatario["CEP"].ToString()) ? null : Regex.Replace(rDestinatario["CEP"].ToString(), "[^0-9a-zA-Z]+", "").Trim();
                nfeDest.cPais = Convert.ToInt32(rDestinatario["CODBACEN"]);
                nfeDest.xPais = string.IsNullOrEmpty(rDestinatario["PAIS"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rDestinatario["PAIS"].ToString()).Trim();


                if (!string.IsNullOrEmpty(rDestinatario["TELCOMERCIAL"].ToString()))
                {
                    nfeDest.fone = Regex.Replace(rDestinatario["TELCOMERCIAL"].ToString(), "[^0-9a-zA-Z]+", "").Trim();
                }
                else if (!string.IsNullOrEmpty(rDestinatario["TELCELULAR"].ToString()))
                {
                    nfeDest.fone = Regex.Replace(rDestinatario["TELCELULAR"].ToString(), "[^0-9a-zA-Z]+", "").Trim();
                }
                else if (!string.IsNullOrEmpty(rDestinatario["TELRESIDENCIAL"].ToString()))
                {
                    nfeDest.fone = Regex.Replace(rDestinatario["TELRESIDENCIAL"].ToString(), "[^0-9a-zA-Z]+", "").Trim();
                }
                else if (!string.IsNullOrEmpty(rDestinatario["TELFAX"].ToString()))
                {
                    nfeDest.fone = Regex.Replace(rDestinatario["TELFAX"].ToString(), "[^0-9a-zA-Z]+", "").Trim();
                }
                else
                {
                    nfeDest.fone = null;
                }
                //nfeDest.fone = (rDestinatario["TELCOMERCIAL"] == DBNull.Value) ? null : Regex.Replace(rDestinatario["TELCOMERCIAL"].ToString(), "[^0-9a-zA-Z]+", "").Trim();


                nfeDest.indIEDest = Convert.ToInt32(rDestinatario["CONTRIBICMS"]);

                if (nfeDest.indIEDest.Equals(2))
                {
                    nfeDest.indIEDest = 9;
                }
                nfeDest.IE = string.IsNullOrEmpty(rDestinatario["INSCRICAOESTADUAL"].ToString()) ? null : Regex.Replace(rDestinatario["INSCRICAOESTADUAL"].ToString(), "[^0-9a-zA-Z]+", "").Trim();
                nfeDest.ISUF = (string.IsNullOrEmpty(rDestinatario["INSCRICAOSUFRAMA"].ToString()) ? null : Regex.Replace(rDestinatario["INSCRICAOSUFRAMA"].ToString(), "[^0-9a-zA-Z]+", "").Trim());
                nfeDest.IM = string.IsNullOrEmpty(rDestinatario["INSCRICAOMUNICIPAL"].ToString()) ? null : Regex.Replace(rDestinatario["INSCRICAOMUNICIPAL"].ToString(), "[^0-9a-zA-Z]+", "").Trim();
                nfeDest.email = string.IsNullOrEmpty(rDestinatario["EMAIL"].ToString()) ? null : rDestinatario["EMAIL"].ToString().Trim();

                nfeDoc.nfeDest = nfeDest;

                #endregion

                #region Det

                nfeDoc.nfeDet = new List<ValidateLib.NFeDet>();

                DataTable tItem = this.GetItemOperacaoTable(CodEmpresa, CodOper);

                int vQtdItem = tItem.Rows.Count;

                decimal vFrete = Convert.ToDecimal(rOperacao["VALORFRETE"]);
                decimal vSeg = Convert.ToDecimal(rOperacao["VALORSEGURO"]);
                decimal vDesc = Convert.ToDecimal(rOperacao["VALORDESCONTO"]);
                decimal vOutro = Convert.ToDecimal(rOperacao["VALORDESPESA"]);

                int contProd = 0;
                foreach (DataRow rItem in tItem.Rows)
                {
                    ValidateLib.NFeDet nfeDet = new ValidateLib.NFeDet();
                    nfeDet.IdOutbox = outBoxParams.IdOutbox;
                    nfeDet.nItem = Convert.ToInt32(rItem["NSEQITEM"]);

                    #region Rateio de Valores

                    //decimal vFreteRateio = gb.Arredonda((vFrete / vQtdItem), 2);
                    decimal vFreteRateio = gb.Arredonda(Convert.ToDecimal(rItem["RATEIOFRETE"]), 2);
                    /*ecimal vSegRateio = gb.Arredonda((vSeg / vQtdItem), 2);*/
                    decimal vSegRateio = gb.Arredonda(Convert.ToDecimal(rItem["RATEIOSEGURO"]), 2);
                    //decimal vDescRateio = gb.Arredonda((vDesc / vQtdItem), 2);
                    decimal vDescRateio = gb.Arredonda(Convert.ToDecimal(rItem["RATEIODESCONTO"]), 2);
                    //decimal vOutroRateio = gb.Arredonda((vOutro / vQtdItem), 2);
                    decimal vOutroRateio = gb.Arredonda(Convert.ToDecimal(rItem["RATEIODESPESA"]), 2);

                    // Bloco de código comentado pois a rotina estava errada - João Pedro Luchiari, 05/03/2018.

                    //if ((vFreteRateio * vQtdItem) != vFrete)
                    //{
                    //    if (contProd == 0)
                    //    {
                    //        if ((vFreteRateio * vQtdItem) > vFrete)
                    //            vFreteRateio = vFreteRateio - ((vFreteRateio * vQtdItem) - vFrete);
                    //        else
                    //            vFreteRateio = vFreteRateio + (vFrete - (vFreteRateio * vQtdItem));
                    //    }
                    //}

                    //if ((vSegRateio * vQtdItem) != vSeg)
                    //{
                    //    if (contProd == 0)
                    //    {
                    //        if ((vSegRateio * vQtdItem) > vSeg)
                    //            vSegRateio = vSegRateio - ((vSegRateio * vQtdItem) - vSeg);
                    //        else
                    //            vSegRateio = vSegRateio + (vSeg - (vSegRateio * vQtdItem));
                    //    }
                    //}

                    //if ((vDescRateio * vQtdItem) != vDesc)
                    //{
                    //    if (contProd == 0)
                    //    {
                    //        if ((vDescRateio * vQtdItem) > vDesc)
                    //            vDescRateio = vDescRateio - ((vDescRateio * vQtdItem) - vDesc);
                    //        else
                    //            vDescRateio = vDescRateio + (vDesc - (vDescRateio * vQtdItem));
                    //    }
                    //}

                    //if ((vOutroRateio * vQtdItem) != vOutro)
                    //{
                    //    if (contProd == 0)
                    //    {
                    //        if ((vOutroRateio * vQtdItem) > vOutro)
                    //            vOutroRateio = vOutroRateio - ((vOutroRateio * vQtdItem) - vOutro);
                    //        else
                    //            vOutroRateio = vOutroRateio + (vOutro - (vOutroRateio * vQtdItem));
                    //    }
                    //}

                    #endregion

                    #region Prod

                    DataRow rProdutoServico = this.GetProdutoServicoRow(CodEmpresa, rItem["CODPRODUTO"].ToString());

                    ValidateLib.NFeProd nfeProd = new ValidateLib.NFeProd();
                    nfeProd.IdOutbox = outBoxParams.IdOutbox;
                    nfeProd.nItem = nfeDet.nItem;
                    if (usaCodAuxiliar == true)
                    {
                        nfeProd.cProd = string.IsNullOrEmpty(rProdutoServico["CODIGOAUXILIAR"].ToString()) ? null : rProdutoServico["CODIGOAUXILIAR"].ToString().Trim();
                    }
                    else
                    {
                        nfeProd.cProd = string.IsNullOrEmpty(rProdutoServico["CODPRODUTO"].ToString()) ? null : rProdutoServico["CODPRODUTO"].ToString().Trim();
                    }
                    nfeProd.xProd = PS.Lib.Utils.RemoveCaracterSpecial(rProdutoServico[rTipoOperacao["TEXTOPRODNFE"].ToString()].ToString()).Trim();
                    nfeProd.NCM = string.IsNullOrEmpty(rProdutoServico["CODNCM"].ToString()) ? null : rProdutoServico["CODNCM"].ToString().Replace(".", "").Trim();
                    nfeProd.CFOP = Convert.ToInt32(rItem["CODNATUREZA"].ToString().Substring(0, 5).Replace(".", ""));
                    nfeProd.uCom = string.IsNullOrEmpty(rItem["CODUNIDOPER"].ToString()) ? null : rItem["CODUNIDOPER"].ToString().Trim();
                    nfeProd.qCom = Convert.ToDecimal(rItem["QUANTIDADE"]);
                    nfeProd.vUnCom = Convert.ToDecimal(rItem["VLUNITARIO"]);
                    nfeProd.vProd = Convert.ToDecimal(rItem["VLTOTALITEM"]);
                    nfeProd.uTrib = string.IsNullOrEmpty(rItem["CODUNIDOPER"].ToString()) ? null : rItem["CODUNIDOPER"].ToString().Trim();
                    nfeProd.qTrib = Convert.ToDecimal(rItem["QUANTIDADE"]);
                    nfeProd.vUnTrib = Convert.ToDecimal(rItem["VLUNITARIO"]);
                    nfeProd.vFrete = vFreteRateio;
                    nfeProd.vSeg = vSegRateio;
                    nfeProd.vDesc = vDescRateio;
                    nfeProd.vOutro = vOutroRateio;
                    nfeProd.indTot = 1;
                    nfeProd.CEST = rProdutoServico["CEST"].ToString();

                    if (!string.IsNullOrEmpty(rItem["XPED"].ToString()))
                    {
                        nfeProd.xPed = rItem["XPED"].ToString();
                    }
                    if (!string.IsNullOrEmpty(rItem["NITEMPED"].ToString()))
                    {
                        nfeProd.nItemPed = rItem["NITEMPED"].ToString();
                    }

                    string sql2 = @"SELECT CODIGOBARRAS FROM VPRODUTOCODIGO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND ATIVO = 1";
                    string cEan = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, sql2, new object[] { AppLib.Context.Empresa, rItem["CODPRODUTO"].ToString(), 1 }).ToString();
                    nfeProd.cEAN = cEan;
                    nfeProd.cEANTrib = cEan;

                    nfeDet.nfeProd = nfeProd;

                    #region DI
                    //2017-08-10
                    string CodTipOper = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CodOper }).ToString();
                    //
                   bool UsaNfe = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT USANFEIMPORTACAO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, CodTipOper }));
                   
                    try
                    {
                        if (UsaNfe == true)
                        {
                            DataTable dtDI = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT NUMERODI, DATADI, LOCDESEMB, UFDESEMB, DATADESEMB, CODEXPORTADOR, NUMADICAO, NUMSEQADIC, CODFABRICANTE, VLORDESCDI, TPVIATRANSP, VAFRMM, TPINTERMEDIO, CNPJ, UFTERCEIRO, NDRAW FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?", new object[] { AppLib.Context.Empresa, CodOper, Convert.ToInt32(rItem["NSEQITEM"]) });

                            if (dtDI.Rows.Count > 0)
                            {
                                ValidateLib.OBJETOS.DI DI = new ValidateLib.OBJETOS.DI();
                                DI.CEXPORTADOR = dtDI.Rows[0]["CODEXPORTADOR"].ToString();
                                DI.DDESEMB = dtDI.Rows[0]["DATADESEMB"].ToString();
                                DI.DDI = dtDI.Rows[0]["DATADI"].ToString();
                                DI.IDOUTBOX = outBoxParams.IdOutbox;
                                DI.NDI = dtDI.Rows[0]["NUMERODI"].ToString();
                                DI.NITEM = nfeDet.nItem;
                                DI.UFDESEMB = dtDI.Rows[0]["UFDESEMB"].ToString();
                                DI.VDESCDI = dtDI.Rows[0]["VLORDESCDI"].ToString();
                                DI.XLOCDESEMB = dtDI.Rows[0]["LOCDESEMB"].ToString();
                                DI.TPVIATRANSP = dtDI.Rows[0]["TPVIATRANSP"].ToString();
                                DI.VAFRMM = Convert.ToDecimal(dtDI.Rows[0]["VAFRMM"]);
                                DI.TPINTERMEDIO = dtDI.Rows[0]["TPINTERMEDIO"].ToString();
                                DI.CNPJ = dtDI.Rows[0]["CNPJ"].ToString();
                                DI.UFTERCEIRO = dtDI.Rows[0]["UFTERCEIRO"].ToString();
                                ValidateLib.OBJETOS.ADI ADI = new ValidateLib.OBJETOS.ADI();
                                ADI.NADICAO = dtDI.Rows[0]["NUMADICAO"].ToString();
                                ADI.NSEQADIC = dtDI.Rows[0]["NUMSEQADIC"].ToString();
                                ADI.CFABRICANTE = dtDI.Rows[0]["CODFABRICANTE"].ToString();
                                ADI.NDRAW = dtDI.Rows[0]["NDRAW"].ToString();
                                DI.ADI = ADI;
                                DI.save();
                            }
                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }



                    #endregion
                    #endregion

                    #region Impostos

                    ValidateLib.NFeImposto nfeImposto = new ValidateLib.NFeImposto();
                    nfeImposto.IdOutbox = outBoxParams.IdOutbox;
                    nfeImposto.nItem = nfeDet.nItem;





                    //Verificar Fábio Campos
                    //Busca a informação VTOTTRIB de cada ITEM
                    string sql = @"SELECT 
	(Z.VL_FEDERAL + Z.VL_ESTADUAL + Z.VL_FMUNICIPAL) VTOTTRIBITEM
FROM (
SELECT 
	(X.VL_TOTAL * (ALIQ_FEDERAL / 100)) VL_FEDERAL,
	(X.VL_TOTAL * (ALIQ_ESTADUAL / 100)) VL_ESTADUAL,
	(X.VL_TOTAL * (ALIQ_MUNICIPAL / 100)) VL_FMUNICIPAL
FROM(

SELECT
	ISNULL(GOPERITEM.VLTOTALITEM,0) VL_TOTAL,
	(SELECT CODETD FROM VCLIFOR WHERE CODCLIFOR = GOPER.CODCLIFOR AND CODEMPRESA = GOPER.CODEMPRESA) UF,
	(SELECT CODNCM FROM VPRODUTO WHERE CODPRODUTO = GOPERITEM.CODPRODUTO AND CODEMPRESA = GOPERITEM.CODEMPRESA) NCM,
	(SELECT ISNULL(NACIONALFEDERAL,0) FROM VIBPTAX WHERE VIBPTAX.CODIGO = (SELECT CODNCM FROM VPRODUTO WHERE CODPRODUTO = GOPERITEM.CODPRODUTO AND CODEMPRESA = GOPERITEM.CODEMPRESA) AND UF IN (SELECT CODETD FROM VCLIFOR WHERE CODCLIFOR = GOPER.CODCLIFOR AND CODEMPRESA = GOPER.CODEMPRESA) ) ALIQ_FEDERAL,
	(SELECT ISNULL(ESTADUAL,0) FROM VIBPTAX WHERE VIBPTAX.CODIGO = (SELECT CODNCM FROM VPRODUTO WHERE CODPRODUTO = GOPERITEM.CODPRODUTO AND CODEMPRESA = GOPERITEM.CODEMPRESA) AND UF IN (SELECT CODETD FROM VCLIFOR WHERE CODCLIFOR = GOPER.CODCLIFOR AND CODEMPRESA = GOPER.CODEMPRESA) ) ALIQ_ESTADUAL,
	(SELECT ISNULL(MUNICIPAL,0) FROM VIBPTAX WHERE VIBPTAX.CODIGO = (SELECT CODNCM FROM VPRODUTO WHERE CODPRODUTO = GOPERITEM.CODPRODUTO AND CODEMPRESA = GOPERITEM.CODEMPRESA) AND UF IN (SELECT CODETD FROM VCLIFOR WHERE CODCLIFOR = GOPER.CODCLIFOR AND CODEMPRESA = GOPER.CODEMPRESA) ) ALIQ_MUNICIPAL
FROM 
	GOPER,
	GOPERITEM
WHERE 
	GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA
AND GOPER.CODOPER = GOPERITEM.CODOPER
AND	GOPER.CODOPER = ?
AND GOPERITEM.NSEQITEM = ?
) X
) Z";
                    nfeImposto.vTotTrib = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, sql, new object[] { CodOper, Convert.ToInt32(rItem["NSEQITEM"]) }));

                    //////
                    //nfeImposto.vTotTrib = Convert.ToDecimal(rItem["VTOTTRIB"]);

                    DataTable tTributo = this.GetItemTributoOperacaoTable(CodEmpresa, CodOper, Convert.ToInt32(rItem["NSEQITEM"]));

                    foreach (DataRow rTributo in tTributo.Rows)
                    {
                        ValidateLib.NFeICMSSN202 NFeICMSSN202 = new ValidateLib.NFeICMSSN202();
                        ValidateLib.NFeICMS40 nfeICMS40 = new ValidateLib.NFeICMS40();

                        #region ICMS

                        if (rTributo["CODTIPOTRIBUTO"].ToString() == "ICMS")
                        {
                            switch (rTributo["CODCST"].ToString())
                            {
                                case "00":
                                    ValidateLib.NFeICMS00 nfeICMS00 = new ValidateLib.NFeICMS00();
                                    nfeICMS00.IdOutbox = outBoxParams.IdOutbox;
                                    nfeICMS00.nItem = nfeDet.nItem;
                                    nfeICMS00.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    nfeICMS00.CST = rTributo["CODCST"].ToString();
                                    nfeICMS00.modBC = Convert.ToInt32(rTributo["MODALIDADEBC"]);
                                    nfeICMS00.vBC = Convert.ToDecimal(rTributo["BASECALCULO"]);
                                    nfeICMS00.pICMS = Convert.ToDecimal(rTributo["ALIQUOTA"]);
                                    nfeICMS00.vICMS = Convert.ToDecimal(rTributo["VALOR"]);
                                    nfeImposto.nfeICMS00 = nfeICMS00;
                                    break;
                                case "10":
                                    ValidateLib.NFeICMS10 nfeICMS10 = new ValidateLib.NFeICMS10();
                                    nfeICMS10.IdOutbox = outBoxParams.IdOutbox;
                                    nfeICMS10.nItem = nfeDet.nItem;
                                    nfeICMS10.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    nfeICMS10.CST = rTributo["CODCST"].ToString();
                                    nfeICMS10.modBC = Convert.ToInt32(rTributo["MODALIDADEBC"]);
                                    nfeICMS10.vBC = Convert.ToDecimal(rTributo["BASECALCULO"]);
                                    nfeICMS10.pICMS = Convert.ToDecimal(rTributo["ALIQUOTA"]);
                                    nfeICMS10.vICMS = Convert.ToDecimal(rTributo["VALOR"]);
                                    nfeImposto.nfeICMS10 = nfeICMS10;
                                    break;
                                case "20":
                                    ValidateLib.NFeICMS20 nfeICMS20 = new ValidateLib.NFeICMS20();
                                    nfeICMS20.IdOutbox = outBoxParams.IdOutbox;
                                    nfeICMS20.nItem = nfeDet.nItem;
                                    nfeICMS20.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    nfeICMS20.CST = rTributo["CODCST"].ToString();
                                    nfeICMS20.modBC = Convert.ToInt32(rTributo["MODALIDADEBC"]);
                                    nfeICMS20.pRedBC = Convert.ToInt32(rTributo["REDUCAOBASEICMS"]);
                                    nfeICMS20.vBC = Convert.ToDecimal(rTributo["BASECALCULO"]);
                                    nfeICMS20.pICMS = Convert.ToDecimal(rTributo["ALIQUOTA"]);
                                    nfeICMS20.vICMS = Convert.ToDecimal(rTributo["VALOR"]);
                                    nfeImposto.nfeICMS20 = nfeICMS20;
                                    break;
                                case "30":
                                    ValidateLib.NFeICMS30 nfeICMS30 = new ValidateLib.NFeICMS30();
                                    nfeICMS30.IdOutbox = outBoxParams.IdOutbox;
                                    nfeICMS30.nItem = nfeDet.nItem;
                                    nfeICMS30.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    nfeICMS30.CST = rTributo["CODCST"].ToString();
                                    nfeImposto.nfeICMS30 = nfeICMS30;
                                    break;
                                case "40":
                                    nfeICMS40 = new ValidateLib.NFeICMS40();
                                    nfeICMS40.IdOutbox = outBoxParams.IdOutbox;
                                    nfeICMS40.nItem = nfeDet.nItem;
                                    nfeICMS40.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    nfeICMS40.CST = rTributo["CODCST"].ToString();
                                    nfeImposto.nfeICMS40 = nfeICMS40;
                                    break;
                                case "41":
                                    nfeICMS40 = new ValidateLib.NFeICMS40();
                                    nfeICMS40.IdOutbox = outBoxParams.IdOutbox;
                                    nfeICMS40.nItem = nfeDet.nItem;
                                    nfeICMS40.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    nfeICMS40.CST = rTributo["CODCST"].ToString();
                                    nfeImposto.nfeICMS40 = nfeICMS40;
                                    break;
                                case "50":
                                    nfeICMS40 = new ValidateLib.NFeICMS40();
                                    nfeICMS40.IdOutbox = outBoxParams.IdOutbox;
                                    nfeICMS40.nItem = nfeDet.nItem;
                                    nfeICMS40.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    nfeICMS40.CST = rTributo["CODCST"].ToString();
                                    nfeImposto.nfeICMS40 = nfeICMS40;
                                    break;
                                case "51":
                                    ValidateLib.NFeICMS51 nfeICMS51 = new ValidateLib.NFeICMS51();
                                    nfeICMS51.IdOutbox = outBoxParams.IdOutbox;
                                    nfeICMS51.nItem = nfeDet.nItem;
                                    nfeICMS51.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    nfeICMS51.CST = rTributo["CODCST"].ToString();
                                    nfeICMS51.vBC = Convert.ToDecimal(rTributo["BASECALCULO"]);
                                    nfeICMS51.vICMS = Convert.ToDecimal(rTributo["VALOR"]);
                                    nfeICMS51.modBC = Convert.ToInt32(rTributo["MODALIDADEBC"]);
                                    nfeICMS51.pICMS = Convert.ToDecimal(rTributo["ALIQUOTA"]);
                                    nfeICMS51.pRedBC = Convert.ToInt32(rTributo["REDUCAOBASEICMS"]);
                                    nfeICMS51.vICMSOp = (nfeICMS51.vBC * nfeICMS51.pICMS) / 100;
                                    nfeICMS51.pDif = Convert.ToDecimal(rTributo["PDIF"]);
                                    nfeICMS51.vICMSDif = Convert.ToDecimal(rTributo["VICMSDIF"]);

                                    nfeImposto.nfeICMS51 = nfeICMS51;
                                    break;
                                case "60":
                                    ValidateLib.NFeICMS60 nfeICMS60 = new ValidateLib.NFeICMS60();
                                    nfeICMS60.IdOutbox = outBoxParams.IdOutbox;
                                    nfeICMS60.nItem = nfeDet.nItem;
                                    nfeICMS60.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    nfeICMS60.CST = rTributo["CODCST"].ToString();
                                    nfeImposto.nfeICMS60 = nfeICMS60;
                                    break;
                                case "70":
                                    ValidateLib.NFeICMS70 nfeICMS70 = new ValidateLib.NFeICMS70();
                                    nfeICMS70.IdOutbox = outBoxParams.IdOutbox;
                                    nfeICMS70.nItem = nfeDet.nItem;
                                    nfeICMS70.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    nfeICMS70.CST = rTributo["CODCST"].ToString();
                                    nfeImposto.nfeICMS70 = nfeICMS70;
                                    break;
                                case "90":
                                    ValidateLib.NFeICMS90 nfeICMS90 = new ValidateLib.NFeICMS90();
                                    nfeICMS90.IdOutbox = outBoxParams.IdOutbox;
                                    nfeICMS90.nItem = nfeDet.nItem;
                                    nfeICMS90.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    nfeICMS90.CST = rTributo["CODCST"].ToString();
                                    nfeICMS90.modBC = Convert.ToInt32(rTributo["MODALIDADEBC"]);
                                    nfeICMS90.vBC = Convert.ToDecimal(rTributo["BASECALCULO"]);
                                    nfeICMS90.pICMS = Convert.ToDecimal(rTributo["ALIQUOTA"]);
                                    nfeICMS90.vICMS = Convert.ToDecimal(rTributo["VALOR"]);
                                    nfeICMS90.pRedBC = Convert.ToInt32(rTributo["REDUCAOBASEICMS"]);

                                    nfeImposto.nfeICMS90 = nfeICMS90;
                                    break;
                                case "101":
                                    ValidateLib.NFeICMSSN101 NFeICMSSN101 = new ValidateLib.NFeICMSSN101();
                                    NFeICMSSN101.IdOutbox = outBoxParams.IdOutbox;
                                    NFeICMSSN101.nItem = nfeDet.nItem;
                                    NFeICMSSN101.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    NFeICMSSN101.CSOSN = Convert.ToInt32(rTributo["CODCST"].ToString());
                                    NFeICMSSN101.pCredSN = Convert.ToDecimal(rTributo["PCREDSN"]);
                                    NFeICMSSN101.vCredICMSSN = Convert.ToDecimal(rTributo["VCREDICMSSN"]);
                                    nfeImposto.nfeICMSSN101 = NFeICMSSN101;
                                    break;
                                case "102":
                                    ValidateLib.NFeICMSSN102 NFeICMSSN102 = new ValidateLib.NFeICMSSN102();
                                    NFeICMSSN102.IdOutbox = outBoxParams.IdOutbox;
                                    NFeICMSSN102.nItem = nfeDet.nItem;
                                    NFeICMSSN102.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    NFeICMSSN102.CSOSN = Convert.ToInt32(rTributo["CODCST"].ToString());
                                    nfeImposto.nfeICMSSN102 = NFeICMSSN102;
                                    break;
                                case "103":
                                    ValidateLib.NFeICMSSN102 NFeICMSSN103 = new ValidateLib.NFeICMSSN102();
                                    NFeICMSSN103.IdOutbox = outBoxParams.IdOutbox;
                                    NFeICMSSN103.nItem = nfeDet.nItem;
                                    NFeICMSSN103.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    NFeICMSSN103.CSOSN = Convert.ToInt32(rTributo["CODCST"].ToString());
                                    nfeImposto.nfeICMSSN102 = NFeICMSSN103;
                                    break;
                                case "300":
                                    ValidateLib.NFeICMSSN102 NFeICMSSN300 = new ValidateLib.NFeICMSSN102();
                                    NFeICMSSN300.IdOutbox = outBoxParams.IdOutbox;
                                    NFeICMSSN300.nItem = nfeDet.nItem;
                                    NFeICMSSN300.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    NFeICMSSN300.CSOSN = Convert.ToInt32(rTributo["CODCST"].ToString());
                                    nfeImposto.nfeICMSSN102 = NFeICMSSN300;
                                    break;
                                case "400":
                                    ValidateLib.NFeICMSSN102 NFeICMSSN400 = new ValidateLib.NFeICMSSN102();
                                    NFeICMSSN400.IdOutbox = outBoxParams.IdOutbox;
                                    NFeICMSSN400.nItem = nfeDet.nItem;
                                    NFeICMSSN400.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    NFeICMSSN400.CSOSN = Convert.ToInt32(rTributo["CODCST"].ToString());
                                    nfeImposto.nfeICMSSN102 = NFeICMSSN400;
                                    break;

                                case "202":
                                    NFeICMSSN202.IdOutbox = outBoxParams.IdOutbox;
                                    NFeICMSSN202.nItem = nfeDet.nItem;
                                    NFeICMSSN202.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    NFeICMSSN202.CSOSN = Convert.ToInt32(rTributo["CODCST"].ToString());
                                    DataTable dtIMCSST = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPERITEMTRIBUTO WHERE CODTRIBUTO = 'ICMS-ST' AND CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?", new object[] { CodEmpresa, CodOper, Convert.ToInt32(rItem["NSEQITEM"]) });
                                    if (dtIMCSST.Rows.Count > 0)
                                    {
                                        NFeICMSSN202.modBCST = Convert.ToInt32(dtIMCSST.Rows[0]["MODALIDADEBC"]);
                                        NFeICMSSN202.pMVAST = Convert.ToDecimal(dtIMCSST.Rows[0]["FATORMVA"]);
                                        NFeICMSSN202.pRedBCST = Convert.ToDecimal(dtIMCSST.Rows[0]["REDUCAOBASEICMSST"]);
                                        NFeICMSSN202.pICMSST = Convert.ToDecimal(dtIMCSST.Rows[0]["ALIQUOTA"]);
                                        NFeICMSSN202.vICMSST = Convert.ToDecimal(dtIMCSST.Rows[0]["VALORICMSST"]);
                                        NFeICMSSN202.vBCST = Convert.ToDecimal(dtIMCSST.Rows[0]["BASECALCULO"]);
                                    }
                                    nfeImposto.nfeICMSSN202 = NFeICMSSN202;
                                    break;
                                case "500":
                                    ValidateLib.NFeICMSSN500 NFeICMSSN500 = new ValidateLib.NFeICMSSN500();
                                    NFeICMSSN500.IdOutbox = outBoxParams.IdOutbox;
                                    NFeICMSSN500.nItem = nfeDet.nItem;
                                    NFeICMSSN500.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    NFeICMSSN500.CSOSN = Convert.ToInt32(rTributo["CODCST"].ToString());
                                    nfeImposto.nfeICMSSN500 = NFeICMSSN500;
                                    break;
                                case "900":
                                    ValidateLib.NFeICMSSN900 NFeICMSSN900 = new ValidateLib.NFeICMSSN900();
                                    NFeICMSSN900.IdOutbox = outBoxParams.IdOutbox;
                                    NFeICMSSN900.nItem = nfeDet.nItem;
                                    NFeICMSSN900.orig = Convert.ToInt32(rProdutoServico["PROCEDENCIA"]);
                                    NFeICMSSN900.CSOSN = Convert.ToInt32(rTributo["CODCST"].ToString());
                                    NFeICMSSN900.modBC = Convert.ToInt32(rTributo["MODALIDADEBC"]);
                                    NFeICMSSN900.vBC = Convert.ToDecimal(rTributo["BASECALCULO"]);
                                    NFeICMSSN900.pRedBC = Convert.ToInt32(rTributo["REDUCAOBASEICMS"]);

                                    dtIMCSST = new DataTable();
                                    dtIMCSST = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPERITEMTRIBUTO WHERE CODTRIBUTO = 'ICMS-ST' AND CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?", new object[] { CodEmpresa, CodOper, Convert.ToInt32(rItem["NSEQITEM"]) });
                                    if (dtIMCSST.Rows.Count > 0)
                                    {

                                        NFeICMSSN900.modBCST = Convert.ToInt32(dtIMCSST.Rows[0]["MODALIDADEBC"]);
                                        NFeICMSSN900.pMVAST = Convert.ToDecimal(dtIMCSST.Rows[0]["FATORMVA"]);
                                        NFeICMSSN900.pRedBCST = Convert.ToDecimal(dtIMCSST.Rows[0]["REDUCAOBASEICMSST"]);
                                        NFeICMSSN900.vBCST = Convert.ToDecimal(dtIMCSST.Rows[0]["BASECALCULO"]);
                                        NFeICMSSN900.pICMSST = Convert.ToDecimal(dtIMCSST.Rows[0]["ALIQUOTA"]);
                                        NFeICMSSN900.vICMSST = Convert.ToDecimal(dtIMCSST.Rows[0]["VALORICMSST"]);
                                    }

                                    NFeICMSSN900.pICMS = Convert.ToDecimal(rTributo["ALIQUOTA"]);
                                    NFeICMSSN900.vICMS = Convert.ToDecimal(rTributo["VALOR"]);
                                    NFeICMSSN900.pCredSN = Convert.ToDecimal(rTributo["PCREDSN"]);
                                    NFeICMSSN900.vCredICMSSN = Convert.ToDecimal(rTributo["VCREDICMSSN"]);
                                    nfeImposto.nfeICMSSN900 = NFeICMSSN900;
                                    break;

                                default:
                                    break;
                            }
                        }

                        #endregion

                        #region IPI

                        if (rTributo["CODTIPOTRIBUTO"].ToString() == "IPI")
                        {
                            ValidateLib.NFeIPI nfeIPI = new ValidateLib.NFeIPI();

                            nfeIPI.IdOutbox = outBoxParams.IdOutbox;
                            nfeIPI.nItem = nfeDet.nItem;
                            //Esperar o Jerry me passar os parâmetros.

                            nfeIPI.cEnq = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CENQ FROM GOPERITEMTRIBUTO WHERE CODOPER = ? AND CODEMPRESA = ? AND CODTRIBUTO = ?", new object[] { CodOper, CodEmpresa, "IPI" }).ToString();
                            ///////////
                            if (rTributo["CODCST"].ToString() == "00" ||
                                rTributo["CODCST"].ToString() == "49" ||
                                rTributo["CODCST"].ToString() == "50" ||
                                rTributo["CODCST"].ToString() == "99")
                            {
                                ValidateLib.NFeIPITrib nfeIPITrib = new ValidateLib.NFeIPITrib();
                                nfeIPITrib.IdOutbox = outBoxParams.IdOutbox;
                                nfeIPITrib.nItem = nfeDet.nItem;
                                nfeIPITrib.CST = rTributo["CODCST"].ToString();
                                nfeIPITrib.pIPI = Convert.ToDecimal(rTributo["ALIQUOTA"]);
                                nfeIPITrib.vBC = Convert.ToDecimal(rTributo["BASECALCULO"]);
                                nfeIPITrib.vIPI = Convert.ToDecimal(rTributo["VALOR"]);

                                nfeIPI.nfeIPITrib = nfeIPITrib;
                            }

                            if (rTributo["CODCST"].ToString() == "01" ||
                                rTributo["CODCST"].ToString() == "02" ||
                                rTributo["CODCST"].ToString() == "03" ||
                                rTributo["CODCST"].ToString() == "04" ||
                                rTributo["CODCST"].ToString() == "51" ||
                                rTributo["CODCST"].ToString() == "52" ||
                                rTributo["CODCST"].ToString() == "53" ||
                                rTributo["CODCST"].ToString() == "54" ||
                                rTributo["CODCST"].ToString() == "55")
                            {
                                ValidateLib.NFeIPINT nfeIPINT = new ValidateLib.NFeIPINT();
                                nfeIPINT.IdOutbox = outBoxParams.IdOutbox;
                                nfeIPINT.nItem = nfeDet.nItem;
                                nfeIPINT.CST = rTributo["CODCST"].ToString();
                                nfeIPI.nfeIPINT = nfeIPINT;
                            }

                            nfeImposto.nfeIPI = nfeIPI;
                        }

                        #endregion

                        #region II

                        if (rTributo["CODTIPOTRIBUTO"].ToString() == "II")
                        {
                            ValidateLib.NFeII nfeII = new ValidateLib.NFeII();
                            nfeII.IdOutbox = outBoxParams.IdOutbox;
                            nfeII.nItem = nfeDet.nItem;
                            nfeImposto.nfeII = nfeII;
                        }

                        #endregion

                        #region PIS

                        if (rTributo["CODTIPOTRIBUTO"].ToString() == "PIS")
                        {
                            ValidateLib.NFePIS nfePIS = new ValidateLib.NFePIS();
                            if (rTributo["CODCST"].ToString() == "01" ||
                                rTributo["CODCST"].ToString() == "02")
                            {
                                ValidateLib.NFePISAliq nfePISAliq = new ValidateLib.NFePISAliq();
                                nfePISAliq.IdOutbox = outBoxParams.IdOutbox;
                                nfePISAliq.nItem = nfeDet.nItem;
                                nfePISAliq.CST = rTributo["CODCST"].ToString();
                                nfePISAliq.vBC = Convert.ToDecimal(rTributo["BASECALCULO"]);
                                nfePISAliq.pPIS = Convert.ToDecimal(rTributo["ALIQUOTA"]);
                                nfePISAliq.vPIS = Convert.ToDecimal(rTributo["VALOR"]);
                                nfePIS.nfePISAliq = nfePISAliq;
                            }

                            if (rTributo["CODCST"].ToString() == "03")
                            {
                                ValidateLib.NFePISQtde nfePISQtde = new ValidateLib.NFePISQtde();
                                nfePISQtde.IdOutbox = outBoxParams.IdOutbox;
                                nfePISQtde.nItem = nfeDet.nItem;
                                nfePISQtde.CST = rTributo["CODCST"].ToString();
                                nfePIS.nfePISQtde = nfePISQtde;
                            }

                            if (rTributo["CODCST"].ToString() == "04" ||
                                rTributo["CODCST"].ToString() == "05" ||
                                rTributo["CODCST"].ToString() == "06" ||
                                rTributo["CODCST"].ToString() == "07" ||
                                rTributo["CODCST"].ToString() == "08" ||
                                rTributo["CODCST"].ToString() == "09")
                            {
                                ValidateLib.NFePISNT nfePISNT = new ValidateLib.NFePISNT();
                                nfePISNT.IdOutbox = outBoxParams.IdOutbox;
                                nfePISNT.nItem = nfeDet.nItem;
                                nfePISNT.CST = rTributo["CODCST"].ToString();
                                nfePIS.nfePISNT = nfePISNT;
                            }

                            if (rTributo["CODCST"].ToString() == "49" ||
                                rTributo["CODCST"].ToString() == "50" ||
                                rTributo["CODCST"].ToString() == "51" ||
                                rTributo["CODCST"].ToString() == "52" ||
                                rTributo["CODCST"].ToString() == "53" ||
                                rTributo["CODCST"].ToString() == "54" ||
                                rTributo["CODCST"].ToString() == "55" ||
                                rTributo["CODCST"].ToString() == "56" ||
                                rTributo["CODCST"].ToString() == "60" ||
                                rTributo["CODCST"].ToString() == "61" ||
                                rTributo["CODCST"].ToString() == "62" ||
                                rTributo["CODCST"].ToString() == "63" ||
                                rTributo["CODCST"].ToString() == "64" ||
                                rTributo["CODCST"].ToString() == "65" ||
                                rTributo["CODCST"].ToString() == "66" ||
                                rTributo["CODCST"].ToString() == "67" ||
                                rTributo["CODCST"].ToString() == "70" ||
                                rTributo["CODCST"].ToString() == "71" ||
                                rTributo["CODCST"].ToString() == "72" ||
                                rTributo["CODCST"].ToString() == "73" ||
                                rTributo["CODCST"].ToString() == "74" ||
                                rTributo["CODCST"].ToString() == "75" ||
                                rTributo["CODCST"].ToString() == "98" ||
                                rTributo["CODCST"].ToString() == "99")
                            {
                                ValidateLib.NFePISOutr nfePISOutr = new ValidateLib.NFePISOutr();
                                nfePISOutr.IdOutbox = outBoxParams.IdOutbox;
                                nfePISOutr.nItem = nfeDet.nItem;
                                nfePISOutr.CST = rTributo["CODCST"].ToString();
                                nfePISOutr.vBC = Convert.ToDecimal(rTributo["BASECALCULO"]);
                                nfePISOutr.pPIS = Convert.ToDecimal(rTributo["ALIQUOTA"]);
                                nfePISOutr.vPIS = Convert.ToDecimal(rTributo["VALOR"]);
                                nfePIS.nfePISOutr = nfePISOutr;
                            }

                            nfeImposto.nfePIS = nfePIS;
                        }

                        #endregion

                        #region PIS-ST

                        if (rTributo["CODTIPOTRIBUTO"].ToString() == "PIS-ST")
                        {

                        }

                        #endregion

                        #region COFINS

                        if (rTributo["CODTIPOTRIBUTO"].ToString() == "COFINS")
                        {
                            ValidateLib.NFeCOFINS nfeCOFINS = new ValidateLib.NFeCOFINS();
                            if (rTributo["CODCST"].ToString() == "01" ||
                                rTributo["CODCST"].ToString() == "02")
                            {
                                ValidateLib.NFeCOFINSAliq nfeCOFINSAliq = new ValidateLib.NFeCOFINSAliq();
                                nfeCOFINSAliq.IdOutbox = outBoxParams.IdOutbox;
                                nfeCOFINSAliq.nItem = nfeDet.nItem;
                                nfeCOFINSAliq.CST = rTributo["CODCST"].ToString();
                                nfeCOFINSAliq.vBC = Convert.ToDecimal(rTributo["BASECALCULO"]);
                                nfeCOFINSAliq.pCOFINS = Convert.ToDecimal(rTributo["ALIQUOTA"]);
                                nfeCOFINSAliq.vCOFINS = Convert.ToDecimal(rTributo["VALOR"]);
                                nfeCOFINS.nfeCOFINSAliq = nfeCOFINSAliq;
                            }

                            if (rTributo["CODCST"].ToString() == "03")
                            {
                                ValidateLib.NFeCOFINSQtde nfeCOFINSQtde = new ValidateLib.NFeCOFINSQtde();
                                nfeCOFINSQtde.IdOutbox = outBoxParams.IdOutbox;
                                nfeCOFINSQtde.nItem = nfeDet.nItem;
                                nfeCOFINSQtde.CST = rTributo["CODCST"].ToString();
                                nfeCOFINS.nfeCOFINSQtde = nfeCOFINSQtde;
                            }

                            if (rTributo["CODCST"].ToString() == "04" ||
                                rTributo["CODCST"].ToString() == "05" ||
                                rTributo["CODCST"].ToString() == "06" ||
                                rTributo["CODCST"].ToString() == "07" ||
                                rTributo["CODCST"].ToString() == "08" ||
                                rTributo["CODCST"].ToString() == "09")
                            {
                                ValidateLib.NFeCOFINSNT nfeCOFINSNT = new ValidateLib.NFeCOFINSNT();
                                nfeCOFINSNT.IdOutbox = outBoxParams.IdOutbox;
                                nfeCOFINSNT.nItem = nfeDet.nItem;
                                nfeCOFINSNT.CST = rTributo["CODCST"].ToString();
                                nfeCOFINS.nfeCOFINSNT = nfeCOFINSNT;
                            }

                            if (rTributo["CODCST"].ToString() == "49" ||
                                rTributo["CODCST"].ToString() == "50" ||
                                rTributo["CODCST"].ToString() == "51" ||
                                rTributo["CODCST"].ToString() == "52" ||
                                rTributo["CODCST"].ToString() == "53" ||
                                rTributo["CODCST"].ToString() == "54" ||
                                rTributo["CODCST"].ToString() == "55" ||
                                rTributo["CODCST"].ToString() == "56" ||
                                rTributo["CODCST"].ToString() == "60" ||
                                rTributo["CODCST"].ToString() == "61" ||
                                rTributo["CODCST"].ToString() == "62" ||
                                rTributo["CODCST"].ToString() == "63" ||
                                rTributo["CODCST"].ToString() == "64" ||
                                rTributo["CODCST"].ToString() == "65" ||
                                rTributo["CODCST"].ToString() == "66" ||
                                rTributo["CODCST"].ToString() == "67" ||
                                rTributo["CODCST"].ToString() == "70" ||
                                rTributo["CODCST"].ToString() == "71" ||
                                rTributo["CODCST"].ToString() == "72" ||
                                rTributo["CODCST"].ToString() == "73" ||
                                rTributo["CODCST"].ToString() == "74" ||
                                rTributo["CODCST"].ToString() == "75" ||
                                rTributo["CODCST"].ToString() == "98" ||
                                rTributo["CODCST"].ToString() == "99")
                            {
                                ValidateLib.NFeCOFINSOutr nfeCOFINSOutr = new ValidateLib.NFeCOFINSOutr();
                                nfeCOFINSOutr.IdOutbox = outBoxParams.IdOutbox;
                                nfeCOFINSOutr.nItem = nfeDet.nItem;
                                nfeCOFINSOutr.CST = rTributo["CODCST"].ToString();
                                nfeCOFINSOutr.vBC = Convert.ToDecimal(rTributo["BASECALCULO"]);
                                nfeCOFINSOutr.pCOFINS = Convert.ToDecimal(rTributo["ALIQUOTA"]);
                                nfeCOFINSOutr.vCOFINS = Convert.ToDecimal(rTributo["VALOR"]);
                                nfeCOFINS.nfeCOFINSOutr = nfeCOFINSOutr;

                            }

                            nfeImposto.nfeCOFINS = nfeCOFINS;
                        }

                        #endregion

                        #region COFINS-ST

                        if (rTributo["CODTIPOTRIBUTO"].ToString() == "COFINS-ST")
                        {

                        }

                        #endregion

                        #region ISSQN

                        if (rTributo["CODTIPOTRIBUTO"].ToString() == "ISSQN")
                        {

                        }

                        #endregion
                    }



                    //#region Difal

                    //DataTable dtDifal = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPERITEMDIFAL WHERE CODEMPRESA = ? AND CODOPER= ? AND  NSEQITEM = ?", new object[] { CodEmpresa, CodOper, rItem["NSEQITEM"] });

                    //if (dtDifal.Rows.Count > 0)
                    //{
                    //    ValidateLib.OBJETOS.NfeDifal difal = new ValidateLib.OBJETOS.NfeDifal();

                    //    difal.IdOutbox = outBoxParams.IdOutbox;
                    //    difal.PFCPUFDEST = string.IsNullOrEmpty(dtDifal.Rows[0]["PFCPUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dtDifal.Rows[0]["PFCPUFDEST"]);
                    //    difal.PICMSINTER = string.IsNullOrEmpty(dtDifal.Rows[0]["PICMSINTER"].ToString()) ? 0 : Convert.ToDecimal(dtDifal.Rows[0]["PICMSINTER"]);
                    //    difal.PICMSINTERPART = string.IsNullOrEmpty(dtDifal.Rows[0]["PICMSINTERPART"].ToString()) ? 0 : Convert.ToDecimal(dtDifal.Rows[0]["PICMSINTERPART"]);
                    //    difal.PICMSUFDEST = string.IsNullOrEmpty(dtDifal.Rows[0]["PICMSUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dtDifal.Rows[0]["PICMSUFDEST"]);
                    //    difal.VBCUFDEST = string.IsNullOrEmpty(dtDifal.Rows[0]["VBCUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dtDifal.Rows[0]["VBCUFDEST"]);
                    //    difal.VFCPUFDEST = string.IsNullOrEmpty(dtDifal.Rows[0]["VFCPUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dtDifal.Rows[0]["VFCPUFDEST"]);
                    //    difal.VICMSUFDEST = string.IsNullOrEmpty(dtDifal.Rows[0]["VICMSUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dtDifal.Rows[0]["VICMSUFDEST"]);
                    //    difal.VICMSUFREMET = string.IsNullOrEmpty(dtDifal.Rows[0]["VICMSUFREMET"].ToString()) ? 0 : Convert.ToDecimal(dtDifal.Rows[0]["VICMSUFREMET"]);

                    //    difal.save();
                    //}


                    //#endregion
                    nfeDet.nfeImposto = nfeImposto;

                    #endregion

                    nfeDet.infAdProd = string.IsNullOrEmpty(rItem["INFCOMPL"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rItem["INFCOMPL"].ToString()).Trim();

                    nfeDoc.nfeDet.Add(nfeDet);
                    contProd++;
                }
                #region Difal

                DataTable dtDifal = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPERITEMDIFAL WHERE CODEMPRESA = ? AND CODOPER= ?", new object[] { CodEmpresa, CodOper });
                ValidateLib.OBJETOS.NfeDifal difal = new ValidateLib.OBJETOS.NfeDifal();
                difal.IdOutbox = outBoxParams.IdOutbox;
                for (int i = 0; i < dtDifal.Rows.Count; i++)
                {

                    difal.PFCPUFDEST = string.IsNullOrEmpty(dtDifal.Rows[i]["PFCPUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dtDifal.Rows[i]["PFCPUFDEST"]);
                    difal.PICMSINTER = string.IsNullOrEmpty(dtDifal.Rows[i]["PICMSINTER"].ToString()) ? 0 : Convert.ToDecimal(dtDifal.Rows[i]["PICMSINTER"]);
                    difal.PICMSINTERPART = string.IsNullOrEmpty(dtDifal.Rows[i]["PICMSINTERPART"].ToString()) ? 0 : Convert.ToDecimal(dtDifal.Rows[i]["PICMSINTERPART"]);
                    difal.PICMSUFDEST = string.IsNullOrEmpty(dtDifal.Rows[i]["PICMSUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dtDifal.Rows[i]["PICMSUFDEST"]);
                    difal.VBCUFDEST = difal.VBCUFDEST + (string.IsNullOrEmpty(dtDifal.Rows[i]["VBCUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dtDifal.Rows[i]["VBCUFDEST"]));
                    difal.VFCPUFDEST = difal.VFCPUFDEST + (string.IsNullOrEmpty(dtDifal.Rows[i]["VFCPUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dtDifal.Rows[i]["VFCPUFDEST"]));
                    difal.VICMSUFDEST = difal.VICMSUFDEST + (string.IsNullOrEmpty(dtDifal.Rows[i]["VICMSUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dtDifal.Rows[i]["VICMSUFDEST"]));
                    difal.VICMSUFREMET = difal.VICMSUFREMET + (string.IsNullOrEmpty(dtDifal.Rows[i]["VICMSUFREMET"].ToString()) ? 0 : Convert.ToDecimal(dtDifal.Rows[i]["VICMSUFREMET"]));
                }
                if (dtDifal.Rows.Count > 0)
                {
                    difal.save();
                }

                #endregion
                #endregion

                #region Total

                ValidateLib.NFeTotal nfeTotal = new ValidateLib.NFeTotal();

                #region ICMSTot

                ValidateLib.NFeICMSTot nFeICMSTot = new ValidateLib.NFeICMSTot();
                nFeICMSTot.IdOutbox = outBoxParams.IdOutbox;
                nFeICMSTot.vBC = Convert.ToDecimal(rTotal["VBC"]);
                nFeICMSTot.vICMS = Convert.ToDecimal(rTotal["VICMS"]);
                //value = Math.Truncate(100 * value) / 100;
                nFeICMSTot.vICMS = Math.Truncate(100 * nFeICMSTot.vICMS) / 100;
                nFeICMSTot.vICMSDeson = Convert.ToDecimal(rTotal["VICMSDESON"]);
                nFeICMSTot.vBCST = Convert.ToDecimal(rTotal["VBCST"]);
                nFeICMSTot.vST = Convert.ToDecimal(rTotal["VST"]);
                nFeICMSTot.vProd = Convert.ToDecimal(rTotal["VPROD"]);
                nFeICMSTot.vFrete = Convert.ToDecimal(rOperacao["VALORFRETE"]);
                nFeICMSTot.vSeg = Convert.ToDecimal(rOperacao["VALORSEGURO"]);
                nFeICMSTot.vDesc = Convert.ToDecimal(rOperacao["VALORDESCONTO"]);
                nFeICMSTot.vII = Convert.ToDecimal(rTotal["VII"]);
                nFeICMSTot.vIPI = Convert.ToDecimal(rTotal["VIPI"]);
                nFeICMSTot.vPIS = Convert.ToDecimal(rTotal["VPIS"]);
                nFeICMSTot.vCOFINS = Convert.ToDecimal(rTotal["VCOFINS"]);
                nFeICMSTot.vOutro = Convert.ToDecimal(rOperacao["VALORDESPESA"]);
                nFeICMSTot.vNF = Convert.ToDecimal(rOperacao["VALORLIQUIDO"]);



                //Verificar Fábio Campos
                string sql1 = @"
SELECT 
	SUM((Z.VL_FEDERAL + Z.VL_ESTADUAL + Z.VL_FMUNICIPAL)) VTOTTRIB
FROM (
SELECT 
	(X.VL_TOTAL * (ALIQ_FEDERAL / 100)) VL_FEDERAL,
	(X.VL_TOTAL * (ALIQ_ESTADUAL / 100)) VL_ESTADUAL,
	(X.VL_TOTAL * (ALIQ_MUNICIPAL / 100)) VL_FMUNICIPAL
FROM(

SELECT
	ISNULL(GOPERITEM.VLTOTALITEM,0) VL_TOTAL,
	(SELECT CODETD FROM VCLIFOR WHERE CODCLIFOR = GOPER.CODCLIFOR AND CODEMPRESA = GOPER.CODEMPRESA) UF,
	(SELECT CODNCM FROM VPRODUTO WHERE CODPRODUTO = GOPERITEM.CODPRODUTO AND CODEMPRESA = GOPERITEM.CODEMPRESA) NCM,
	(SELECT ISNULL(NACIONALFEDERAL,0) FROM VIBPTAX WHERE VIBPTAX.CODIGO = (SELECT CODNCM FROM VPRODUTO WHERE CODPRODUTO = GOPERITEM.CODPRODUTO AND CODEMPRESA = GOPERITEM.CODEMPRESA) AND UF IN (SELECT CODETD FROM VCLIFOR WHERE CODCLIFOR = GOPER.CODCLIFOR AND CODEMPRESA = GOPER.CODEMPRESA) ) ALIQ_FEDERAL,
	(SELECT ISNULL(ESTADUAL,0) FROM VIBPTAX WHERE VIBPTAX.CODIGO = (SELECT CODNCM FROM VPRODUTO WHERE CODPRODUTO = GOPERITEM.CODPRODUTO AND CODEMPRESA = GOPERITEM.CODEMPRESA) AND UF IN (SELECT CODETD FROM VCLIFOR WHERE CODCLIFOR = GOPER.CODCLIFOR AND CODEMPRESA = GOPER.CODEMPRESA) ) ALIQ_ESTADUAL,
	(SELECT ISNULL(MUNICIPAL,0) FROM VIBPTAX WHERE VIBPTAX.CODIGO = (SELECT CODNCM FROM VPRODUTO WHERE CODPRODUTO = GOPERITEM.CODPRODUTO AND CODEMPRESA = GOPERITEM.CODEMPRESA) AND UF IN (SELECT CODETD FROM VCLIFOR WHERE CODCLIFOR = GOPER.CODCLIFOR AND CODEMPRESA = GOPER.CODEMPRESA) ) ALIQ_MUNICIPAL
FROM 
	GOPER,
	GOPERITEM
WHERE 
	GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA
AND GOPER.CODOPER = GOPERITEM.CODOPER
AND	GOPER.CODOPER = ?
) X
) Z";
                nFeICMSTot.vTotTrib = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, sql1, new object[] { CodOper }));
                //nFeICMSTot.vTotTrib = Convert.ToDecimal(rTotal["VTOTTRIB"]);
                //nFeICMSTot.vTotTrib = Math.Truncate(100 * nFeICMSTot.vTotTrib) / 100;
                DataTable dtTotal = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT SUM(VFCPUFDEST)VFCPUFDEST, SUM(VICMSUFDEST)VICMSUFDEST, SUM(VICMSUFREMET)VICMSUFREMET FROM GOPERITEMDIFAL WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { CodOper, CodEmpresa });
                if (dtTotal.Rows.Count > 0)
                {
                    nFeICMSTot.vFCPUFDest = string.IsNullOrEmpty(dtTotal.Rows[0]["VFCPUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dtTotal.Rows[0]["VFCPUFDEST"]);
                    nFeICMSTot.vICMSUFDest = string.IsNullOrEmpty(dtTotal.Rows[0]["VICMSUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dtTotal.Rows[0]["VICMSUFDEST"]);
                    nFeICMSTot.vICMSUFRemet = string.IsNullOrEmpty(dtTotal.Rows[0]["VICMSUFREMET"].ToString()) ? 0 : Convert.ToDecimal(dtTotal.Rows[0]["VICMSUFREMET"]);
                }

                nfeTotal.nfeICMSTot = nFeICMSTot;

                if (outBoxParams.nfeDoc.nfeDet[0].nfeImposto.nfeICMSSN202 != null)
                {
                    nfeTotal.nfeICMSTot.vICMS = Convert.ToDecimal("0,00");
                    nfeTotal.nfeICMSTot.vBC = Convert.ToDecimal("0,00");
                }
                if (outBoxParams.nfeDoc.nfeDet[0].nfeImposto.nfeICMSSN101 != null)
                {
                    nfeTotal.nfeICMSTot.vICMS = Convert.ToDecimal("0,00");
                    nfeTotal.nfeICMSTot.vBC = Convert.ToDecimal("0,00");
                }

                #endregion

                #region ISSQNtot


                #endregion

                #region retTrib


                #endregion

                nfeDoc.nfeTotal = nfeTotal;

                #endregion

                #region transp

                if (Convert.ToInt32(rOperacao["FRETECIFFOB"]) != 3)
                {
                    ValidateLib.NFetransp nfetransp = new ValidateLib.NFetransp();
                    nfetransp.IdOutbox = outBoxParams.IdOutbox;
                    nfetransp.modFrete = Convert.ToInt32(rOperacao["FRETECIFFOB"]);

                    #region transporta

                    if (rOperacao["CODTRANSPORTADORA"] != DBNull.Value)
                    {
                        DataRow rTransportadora = this.GetTransportadoraRow(CodEmpresa, Convert.ToInt32(rOperacao["CODTRANSPORTADORA"]));

                        ValidateLib.NFetransporta nfetransporta = new ValidateLib.NFetransporta();
                        nfetransporta.IdOutbox = outBoxParams.IdOutbox;

                        if (rTransportadora["CGCCPF"].ToString().Length == 18)
                            nfetransporta.CNPJ = Regex.Replace(rTransportadora["CGCCPF"].ToString(), "[^0-9a-zA-Z]+", "");
                        else
                            nfetransporta.CPF = Regex.Replace(rTransportadora["CGCCPF"].ToString(), "[^0-9a-zA-Z]+", "");

                        nfetransporta.xNome = string.IsNullOrEmpty(rTransportadora["NOME"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rTransportadora["NOME"].ToString()).Trim();
                        //Verificar Fábio Campos
                        nfetransporta.IE = string.IsNullOrEmpty(rTransportadora["INSCRICAOESTADUAL"].ToString()) ? null : rTransportadora["INSCRICAOESTADUAL"].ToString().Trim();
                        if (!string.IsNullOrEmpty(nfetransporta.IE))
                        {
                            nfetransporta.IE = nfetransporta.IE.Replace(".", "");
                        }

                        nfetransporta.xEnder = string.IsNullOrEmpty(rTransportadora["RUA"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rTransportadora["TIPORUA"].ToString() + " " + rTransportadora["RUA"].ToString()).Trim();
                        nfetransporta.xMun = string.IsNullOrEmpty(rTransportadora["CIDADE"].ToString()) ? null : PS.Lib.Utils.RemoveCaracterSpecial(rTransportadora["CIDADE"].ToString()).Trim();
                        nfetransporta.UF = string.IsNullOrEmpty(rTransportadora["CODETD"].ToString()) ? null : rTransportadora["CODETD"].ToString().Trim();

                        nfetransp.nfetransporta = nfetransporta;
                    }


                    ValidateLib.NFeveicTransp nfeVeicTransp = new ValidateLib.NFeveicTransp();
                    nfeVeicTransp.IdOutbox = outBoxParams.IdOutbox;
                    nfeVeicTransp.placa = rOperacao["PLACA"].ToString();
                    nfeVeicTransp.UF = rOperacao["UFPLACA"].ToString();
                    nfetransp.nfeveicTransp = nfeVeicTransp;
                    #endregion

                    #region retTransp

                    #region veicTransp

                    #endregion

                    #region reboque

                    #endregion

                    #endregion

                    #region volume

                    nfetransp.nfevol = new List<ValidateLib.NFevol>();

                    ValidateLib.NFevol nfevol = new ValidateLib.NFevol();
                    nfevol.IdOutbox = outBoxParams.IdOutbox;
                    nfevol.nItem = 1;
                    nfevol.qVol = Convert.ToDecimal(rOperacao["QUANTIDADE"]).ToString();
                    nfevol.esp = string.IsNullOrEmpty(rOperacao["ESPECIE"].ToString()) ? null : rOperacao["ESPECIE"].ToString().Trim();
                    nfevol.marca = string.IsNullOrEmpty(rOperacao["MARCA"].ToString()) ? null : rOperacao["MARCA"].ToString().Trim();
                    //nfevol.nVol = "";
                    nfevol.pesoL = Convert.ToDecimal(rOperacao["PESOLIQUIDO"]);
                    nfevol.pesoB = Convert.ToDecimal(rOperacao["PESOBRUTO"]);

                    nfetransp.nfevol.Add(nfevol);

                    #endregion

                    nfeDoc.nfetransp = nfetransp;
                }

                #endregion

                #region cobr

                sSql = @"SELECT CODTIPOPER FROM GOPER WHERE CODOPER = ? AND CODEMPRESA = ?";
                string CODTIPOOPER = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, sSql, new object[] { CodOper, CodEmpresa }).ToString();
                if (!CODTIPOOPER.Equals("2.06") && !CODTIPOOPER.Equals("2.08") && !CODTIPOOPER.Equals("2.09"))
                {
                    DataTable tDuplicata = this.GetDuplicataTable(CodEmpresa, CodOper);
                    if (tDuplicata.Rows.Count > 0)
                    {
                        ValidateLib.NFecobr nfecobr = new ValidateLib.NFecobr();
                        nfecobr.nfedup = new List<ValidateLib.NFedup>();

                        #region fat

                        #endregion

                        #region dup

                        int contDup = 1;
                        foreach (DataRow rDuplicata in tDuplicata.Rows)
                        {
                            ValidateLib.NFedup nfedup = new ValidateLib.NFedup();
                            nfedup.IdOutbox = outBoxParams.IdOutbox;
                            nfedup.nItem = contDup;
                            nfedup.nDup = rDuplicata["NUMERO"].ToString().Trim();
                            nfedup.dVenc = Convert.ToDateTime(rDuplicata["DATAVENCIMENTO"]).ToString("yyyy-MM-dd");
                            nfedup.vDup = Convert.ToDecimal(rDuplicata["VLORIGINAL"]);
                            nfecobr.nfedup.Add(nfedup);
                            contDup++;
                        }

                        nfeDoc.nfecobr = nfecobr;

                        #endregion
                    }
                }



                #endregion

                #region infAdic
                string mensagem = string.Empty;
                ValidateLib.NFeinfAdic nfeinfAdic = new ValidateLib.NFeinfAdic();
                nfeinfAdic.IdOutbox = outBoxParams.IdOutbox;

                if (rOperacao["HISTORICO"] != DBNull.Value)
                {
                    nfeinfAdic.infCpl = string.IsNullOrEmpty(rOperacao["HISTORICO"].ToString()) ? null : AppLib.Util.Format.RemoveCharSpeciaisSVirgula(rOperacao["HISTORICO"].ToString()).Trim();
                    nfeinfAdic.infCpl = nfeinfAdic.infCpl;
                }
                if (rOperacao["NFEINFADIC"] != DBNull.Value)
                {
                    mensagem = string.IsNullOrEmpty(rOperacao["NFEINFADIC"].ToString()) ? null : AppLib.Util.Format.RemoveCharSpeciaisSVirgula(rOperacao["NFEINFADIC"].ToString()).Trim();
                }
                if (rOperacao["MENSAGEMIBPTAX"] != DBNull.Value)
                {
                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        mensagem = mensagem + " " + AppLib.Util.Format.RemoveCharSpeciaisSVirgula(rOperacao["MENSAGEMIBPTAX"].ToString()).Trim();
                    }
                    else
                    {
                        mensagem = AppLib.Util.Format.RemoveCharSpeciaisSVirgula(rOperacao["MENSAGEMIBPTAX"].ToString()).Trim();
                    }

                }
                nfeinfAdic.infAdFisco = mensagem;
                nfeDoc.nfeinfAdic = nfeinfAdic;

                #endregion


                #region Exporta
                if (nfeIde.idDest == 3)
                {
                    ValidateLib.OBJETOS.NFeExporta nfeExporta = new ValidateLib.OBJETOS.NFeExporta();
                    nfeExporta.IDOUTBOX = outBoxParams.IdOutbox;
                    nfeExporta.UFSAIDAPAIS = rOperacao["UFSAIDAPAIS"].ToString();
                    nfeExporta.LOCEXPORTA = rOperacao["LOCEXPORTA"].ToString();
                    nfeExporta.LOCDESPACHO = string.IsNullOrEmpty(rOperacao["LOCDESPACHO"].ToString()) ? null : rOperacao["LOCDESPACHO"].ToString();
                    nfeDoc.nfeExporta = nfeExporta;
                }

                #endregion

                #endregion

                #region Integração Validate

                if (outBoxParams.nfeDoc != null)
                {
                    outBoxParams.nfeDoc.Save();
                    if (outBoxParams.nfeDoc.nfeIde != null)
                        outBoxParams.nfeDoc.nfeIde.Save();
                    if (outBoxParams.nfeDoc.nfeEmit != null)
                        outBoxParams.nfeDoc.nfeEmit.Save();
                    if (outBoxParams.nfeDoc.nfeDest != null)
                        outBoxParams.nfeDoc.nfeDest.Save();
                    if (outBoxParams.nfeDoc.nfeRetirada != null)
                        outBoxParams.nfeDoc.nfeRetirada.Save();
                    if (outBoxParams.nfeDoc.nfeEntrega != null)
                        outBoxParams.nfeDoc.nfeEntrega.Save();

                    if (outBoxParams.nfeDoc.nfeDet != null)
                    {
                        foreach (ValidateLib.NFeDet nfeDet in outBoxParams.nfeDoc.nfeDet)
                        {
                            nfeDet.Save();
                            nfeDet.nfeProd.CODOPER = CodOper;
                            nfeDet.nfeProd.Save();
                            nfeDet.nfeImposto.Save();

                            if (nfeDet.nfeImposto.nfeICMS00 != null)
                                nfeDet.nfeImposto.nfeICMS00.Save();
                            if (nfeDet.nfeImposto.nfeICMS10 != null)
                                nfeDet.nfeImposto.nfeICMS10.Save();
                            if (nfeDet.nfeImposto.nfeICMS20 != null)
                                nfeDet.nfeImposto.nfeICMS20.Save();
                            if (nfeDet.nfeImposto.nfeICMS30 != null)
                                nfeDet.nfeImposto.nfeICMS30.Save();
                            if (nfeDet.nfeImposto.nfeICMS40 != null)
                                nfeDet.nfeImposto.nfeICMS40.Save();
                            if (nfeDet.nfeImposto.nfeICMS51 != null)
                                nfeDet.nfeImposto.nfeICMS51.Save();
                            if (nfeDet.nfeImposto.nfeICMS60 != null)
                                nfeDet.nfeImposto.nfeICMS60.Save();
                            if (nfeDet.nfeImposto.nfeICMS70 != null)
                                nfeDet.nfeImposto.nfeICMS70.Save();
                            if (nfeDet.nfeImposto.nfeICMS90 != null)
                                nfeDet.nfeImposto.nfeICMS90.Save();
                            if (nfeDet.nfeImposto.nfeICMSPart != null)
                                nfeDet.nfeImposto.nfeICMSPart.Save();
                            if (nfeDet.nfeImposto.nfeICMSSN101 != null)
                                nfeDet.nfeImposto.nfeICMSSN101.Save();
                            if (nfeDet.nfeImposto.nfeICMSSN102 != null)
                                nfeDet.nfeImposto.nfeICMSSN102.Save();
                            if (nfeDet.nfeImposto.nfeICMSSN201 != null)
                                nfeDet.nfeImposto.nfeICMSSN201.Save();
                            if (nfeDet.nfeImposto.nfeICMSSN202 != null)
                                nfeDet.nfeImposto.nfeICMSSN202.Save();
                            if (nfeDet.nfeImposto.nfeICMSSN500 != null)
                                nfeDet.nfeImposto.nfeICMSSN500.Save();
                            if (nfeDet.nfeImposto.nfeICMSSN900 != null)
                                nfeDet.nfeImposto.nfeICMSSN900.Save();
                            if (nfeDet.nfeImposto.nfeICMSST != null)
                                nfeDet.nfeImposto.nfeICMSST.Save();
                            if (nfeDet.nfeImposto.nfeIPI != null)
                            {
                                nfeDet.nfeImposto.nfeIPI.Save();

                                if (nfeDet.nfeImposto.nfeIPI.nfeIPITrib != null)
                                    nfeDet.nfeImposto.nfeIPI.nfeIPITrib.Save();
                                if (nfeDet.nfeImposto.nfeIPI.nfeIPINT != null)
                                    nfeDet.nfeImposto.nfeIPI.nfeIPINT.Save();
                            }
                            if (nfeDet.nfeImposto.nfeII != null)
                                nfeDet.nfeImposto.nfeII.Save();
                            if (nfeDet.nfeImposto.nfePIS != null)
                            {
                                if (nfeDet.nfeImposto.nfePIS.nfePISAliq != null)
                                    nfeDet.nfeImposto.nfePIS.nfePISAliq.Save();
                                if (nfeDet.nfeImposto.nfePIS.nfePISQtde != null)
                                    nfeDet.nfeImposto.nfePIS.nfePISQtde.Save();
                                if (nfeDet.nfeImposto.nfePIS.nfePISNT != null)
                                    nfeDet.nfeImposto.nfePIS.nfePISNT.Save();
                                if (nfeDet.nfeImposto.nfePIS.nfePISOutr != null)
                                    nfeDet.nfeImposto.nfePIS.nfePISOutr.Save();
                                if (nfeDet.nfeImposto.nfePIS.nfePISST != null)
                                    nfeDet.nfeImposto.nfePIS.nfePISST.Save();
                            }
                            if (nfeDet.nfeImposto.nfeCOFINS != null)
                            {
                                if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSAliq != null)
                                    nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSAliq.Save();
                                if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSQtde != null)
                                    nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSQtde.Save();
                                if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSNT != null)
                                    nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSNT.Save();
                                if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr != null)
                                    nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.Save();
                                if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSST != null)
                                    nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSST.Save();
                            }
                            if (nfeDet.nfeImposto.nfeISSQN != null)
                                nfeDet.nfeImposto.nfeISSQN.Save();
                        }
                    }

                    if (outBoxParams.nfeDoc.nfeTotal.nfeICMSTot != null)
                        outBoxParams.nfeDoc.nfeTotal.nfeICMSTot.Save();
                    if (outBoxParams.nfeDoc.nfeTotal.nfeISSQNtot != null)
                        outBoxParams.nfeDoc.nfeTotal.nfeISSQNtot.Save();
                    if (outBoxParams.nfeDoc.nfeTotal.nferetTrib != null)
                        outBoxParams.nfeDoc.nfeTotal.nferetTrib.Save();

                    if (outBoxParams.nfeDoc.nfetransp != null)
                    {
                        outBoxParams.nfeDoc.nfetransp.Save();
                        if (outBoxParams.nfeDoc.nfetransp.nfetransporta != null)
                            outBoxParams.nfeDoc.nfetransp.nfetransporta.Save();
                        if (outBoxParams.nfeDoc.nfetransp.nferetTransp != null)
                            outBoxParams.nfeDoc.nfetransp.nferetTransp.Save();
                        if (outBoxParams.nfeDoc.nfetransp.nfeveicTransp != null)
                            outBoxParams.nfeDoc.nfetransp.nfeveicTransp.Save();

                        if (outBoxParams.nfeDoc.nfetransp.nfereboque != null)
                        {
                            foreach (ValidateLib.NFereboque nfereboque in outBoxParams.nfeDoc.nfetransp.nfereboque)
                            {
                                nfereboque.Save();
                            }
                        }
                        if (outBoxParams.nfeDoc.nfetransp.nfeveicTransp != null)
                        {

                        }
                        if (outBoxParams.nfeDoc.nfetransp.nfevol != null)
                        {
                            foreach (ValidateLib.NFevol nfevol in outBoxParams.nfeDoc.nfetransp.nfevol)
                            {
                                nfevol.Save();
                                if (nfevol.lacres != null)
                                {
                                    foreach (ValidateLib.NFelacres nfelacres in nfevol.lacres)
                                    {
                                        nfelacres.Save();
                                    }
                                }
                            }
                        }
                    }

                    if (outBoxParams.nfeDoc.nfecobr != null)
                    {
                        if (outBoxParams.nfeDoc.nfecobr.nfefat != null)
                            outBoxParams.nfeDoc.nfecobr.nfefat.Save();
                        if (outBoxParams.nfeDoc.nfecobr.nfedup != null)
                        {
                            foreach (ValidateLib.NFedup nfedup in outBoxParams.nfeDoc.nfecobr.nfedup)
                            {
                                nfedup.Save();
                            }
                        }
                    }

                    if (outBoxParams.nfeDoc.nfeinfAdic != null)
                    {
                        outBoxParams.nfeDoc.nfeinfAdic.Save();
                        if (outBoxParams.nfeDoc.nfeinfAdic.nfeobsCont != null)
                        {
                            foreach (ValidateLib.NFeobsCont nfeobsCont in outBoxParams.nfeDoc.nfeinfAdic.nfeobsCont)
                            {
                                nfeobsCont.Save();
                            }
                        }

                        if (outBoxParams.nfeDoc.nfeinfAdic.nfeobsFisco != null)
                        {
                            foreach (ValidateLib.NFeobsFisco nfeobsFisco in outBoxParams.nfeDoc.nfeinfAdic.nfeobsFisco)
                            {
                                nfeobsFisco.Save();
                            }
                        }
                    }
                    if (outBoxParams.nfeDoc.nfeExporta != null)
                    {
                        outBoxParams.nfeDoc.nfeExporta.save();
                    }
                }

                outBoxParams.CodStatus = "ENV";
                outBoxParams.Save();

                #endregion

                _conn.Commit();

                try
                {
                    outBoxParams = ValidateLib.OutBoxParams.ReadByIDOutbox(outBoxParams.IdOutbox);
                    List<ValidateLib.OutBoxParams> list = new List<ValidateLib.OutBoxParams>();
                    list.Add(outBoxParams);

                    ValidateLib.EmpresaParams emp = ValidateLib.EmpresaParams.ReadByIdEmpresa(1);

                    ValidateLib.WebServiceParams _wsParams = ValidateLib.WebServiceParams.ReadActiveService(outBoxParams.CodEstrutura, emp.UF, "NFeAutorizacao", emp.TpAmb);
                    ValidateLib.NFeSrv srv = new ValidateLib.NFeSrv();
                    string xml = srv.NFAutorizacao(list, _wsParams, emp);

                    // Recuperando o XML gerado
                    ValidateLib.OBJETOS_VALIDATESERVICE.FuncoesAuxiliares.XMLGerado = xml;
                }
                catch (Exception ex)
                {
                    outBoxParams.DataUltimoLog = _conn.GetDateTime();
                    outBoxParams.Log = ex.Message;
                    outBoxParams.CodStatus = "ERR";
                    outBoxParams.Save();
                }

                List<PS.Lib.DataField> objArrRet = new List<DataField>();
                objArrRet.Add(new PS.Lib.DataField("CODEMPRESA", CodEmpresa));
                objArrRet.Add(new PS.Lib.DataField("CODOPER", CodOper));
                objArrRet.Add(new PS.Lib.DataField("IDOUTBOX", outBoxParams.IdOutbox));
                objArrRet.Add(new PS.Lib.DataField("CHAVEACESSO", outBoxParams.nfeDoc.Chave));

                return objArrRet;

            }
            //catch (Exception ex)
            //{
            //    _conn.Rollback();
            //    System.Windows.Forms.MessageBox.Show(ex.Message);
            //    throw new Exception(ex.Message);
            //}
            //}

            catch (Exception ex)
            {
                _conn.Rollback();
                System.Windows.Forms.MessageBox.Show(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public List<DataField> CancelarNFe(int CodEmpresa, int CodOper, string Motivo)
        {
            try
            {
                this.InitValidateServer();

                string sSql = string.Empty;

                DataRow rNFEstadual = this.GetNFEstadualRow(CodEmpresa, CodOper);
                DataRow rOperacao = this.GetOperacaoRow(CodEmpresa, CodOper);
                DataRow rFilial = this.GetFilialRow(CodEmpresa, Convert.ToInt32(rOperacao["CODFILIAL"]));

                try
                {
                    ValidateLib.EventoCanNFParams eventoCanNFParams = new ValidateLib.EventoCanNFParams();
                    eventoCanNFParams.Data = dbs.GetServerDateTimeToday();
                    eventoCanNFParams.CNPJEmitente = Regex.Replace(rFilial["CGCCPF"].ToString(), "[^0-9a-zA-Z]+", "");
                    eventoCanNFParams.chNFe = rNFEstadual["CHAVEACESSO"].ToString();
                    eventoCanNFParams.nProtAut = rNFEstadual["PROTOCOLO"].ToString();
                    eventoCanNFParams.xJust = Motivo;
                    eventoCanNFParams.CodStatus = "ENV";
                    eventoCanNFParams.Log = null;
                    eventoCanNFParams.DataUltimoLog = null;
                    eventoCanNFParams.IdOutbox = Convert.ToInt32(rNFEstadual["IDOUTBOX"]);
                    eventoCanNFParams.Save();

                    List<PS.Lib.DataField> objArrRet = new List<DataField>();
                    objArrRet.Add(new PS.Lib.DataField("CODEMPRESA", CodEmpresa));
                    objArrRet.Add(new PS.Lib.DataField("CODOPER", CodOper));

                    return objArrRet;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CartaCorrecao(int CodEmpresa, int CodOper, string Correcao)
        {
            try
            {
                this.InitValidateServer();
                DataRow rNFEstadual = this.GetNFEstadualRow(CodEmpresa, CodOper);
                DataRow rOperacao = this.GetOperacaoRow(CodEmpresa, CodOper);
                DataRow rFilial = this.GetFilialRow(CodEmpresa, Convert.ToInt32(rOperacao["CODFILIAL"]));

                try
                {
                    ValidateLib.OBJETOS.EventoCorrecaoNFParams evento = new ValidateLib.OBJETOS.EventoCorrecaoNFParams();
                    evento.DATA = AppLib.Context.poolConnection.Get().GetDateTime();
                    evento.CNPJEMITENTE = Regex.Replace(rFilial["CGCCPF"].ToString(), "[^0-9a-zA-Z]+", "");
                    evento.CHNFE = rNFEstadual["CHAVEACESSO"].ToString();
                    evento.NPROTAUT = rNFEstadual["PROTOCOLO"].ToString();
                    evento.XJUST = Correcao;
                    evento.CODSTATUS = "ENV";
                    evento.LOG = string.Empty;
                    evento.DATAULTIMOLOG = null;
                    evento.NSEQITEM = Convert.ToInt32(_conn.ExecGetField(0, "SELECT ISNULL(MAX(NSEQITEM), 0) NSEQITEM FROM ZEVENTOCORRECAONF WHERE IDOUTBOX = ?", new object[] { Convert.ToInt32(rNFEstadual["IDOUTBOX"]) })) + 1;
                    evento.IDOUTBOX = Convert.ToInt32(rNFEstadual["IDOUTBOX"]);
                    evento.save();
                    return evento.IDOUTBOX;

                }
                catch (Exception ex)
                {
                    string a = ex.Message;
                    return 0;
                }

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public DataTable GetRetornoCCe(int idOutbox)
        {
            DataTable dt = _conn.ExecQuery("SELECT TOP 1 * FROM ZEVENTOCORRECAONF WHERE IDOUTBOX = ? ORDER BY NSEQITEM DESC", new object[] { idOutbox });
            return dt;
        }

        public List<DataField> ConsultaSituacaoNFe(List<DataField> objArr)
        {
            try
            {
                this.InitValidateServer();

                string sSql = string.Empty;

                PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
                PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
                PS.Lib.DataField dfIDOUTBOX = gb.RetornaDataFieldByCampo(objArr, "IDOUTBOX");
                if (dfIDOUTBOX.Valor == null)
                {
                    dfIDOUTBOX.Valor = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT IDOUTBOX FROM GNFESTADUAL WHERE CODOPER = ? AND CODEMPRESA = ? ", new object[] { dfCODOPER.Valor, dfCODEMPRESA.Valor }).ToString();
                }

                ValidateLib.OutBoxParams _outBoxParams = ValidateLib.OutBoxParams.ReadByIDOutbox(dfIDOUTBOX.Valor);

                List<PS.Lib.DataField> objArrRet = new List<DataField>();
                objArrRet.Add(dfCODEMPRESA);
                objArrRet.Add(dfCODOPER);
                objArrRet.Add(new DataField("CODSTATUS", _outBoxParams.CodStatus));
                objArrRet.Add(new DataField("OBSERVACAO", _outBoxParams.Log));
                objArrRet.Add(new DataField("RECIBO", _outBoxParams.nfeDoc.nRec));
                objArrRet.Add(new DataField("DATARECIBO", _outBoxParams.nfeDoc.DatanRec));
                objArrRet.Add(new DataField("PROTOCOLO", _outBoxParams.nfeDoc.nProt));
                objArrRet.Add(new DataField("DATAPROTOCOLO", _outBoxParams.nfeDoc.DatanProt));
                objArrRet.Add(new DataField("XMLREC", _outBoxParams.nfeDoc.XmlRec));
                objArrRet.Add(new DataField("XMLPROT", _outBoxParams.nfeDoc.XmlProt));
                objArrRet.Add(new DataField("XMLNFE", _outBoxParams.nfeDoc.XmlNFe));




                //Salva o restante das informações na tabela GNFESTADUALEVENTO
                //Busca a informação do banco do validate.
                ValidateLib.EventoCanNFParams _EVE = ValidateLib.EventoCanNFParams.ReadyByIdOutBox(dfIDOUTBOX.Valor);

                if (!string.IsNullOrEmpty(_EVE.chNFe))
                {
                    AppLib.ORM.Jit GNFESTADUALEVENTO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GNFESTADUALCANC");
                    GNFESTADUALEVENTO.Set("IDOUTBOX", _EVE.IdOutbox);
                    GNFESTADUALEVENTO.Set("CODEMPRESA", dfCODEMPRESA.Valor);
                    GNFESTADUALEVENTO.Set("CODOPER", dfCODOPER.Valor);
                    GNFESTADUALEVENTO.Set("CODSTATUS", _EVE.CodStatus);
                    GNFESTADUALEVENTO.Set("MOTIVO", _EVE.xJust);
                    GNFESTADUALEVENTO.Set("PROTOCOLO", _EVE.nProt);
                    GNFESTADUALEVENTO.Set("DATAPROTOCOLO", _EVE.DataUltimoLog);
                    GNFESTADUALEVENTO.Set("XMLENV", _EVE.XmlEnv);
                    GNFESTADUALEVENTO.Set("XMLPROT", _EVE.XmlProt);
                    GNFESTADUALEVENTO.Save();
                }
                return objArrRet;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public string GetDispositivoLegal(int CodEmpresa, int CodOper)
        {
            try
            {
                List<String> lMensagem = new List<string>();
                string Mensagem = string.Empty;

                string sSql = @"SELECT DISTINCT(CODNATUREZA) CODNATUREZA FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?";
                DataTable table = dbs.QuerySelect(sSql, CodEmpresa, CodOper);
                if (table.Rows.Count <= 0)
                {
                    throw new Exception("Não foi possivel enviar a nf-e para autorização pois não foi informado a CFOP na operação.");
                }

                foreach (DataRow row in table.Rows)
                {
                    sSql = @"SELECT CODMENSAGEM FROM VNATUREZA WHERE CODEMPRESA = ? AND CODNATUREZA = ?";
                    string CodMensagem = dbs.QueryValue(string.Empty, sSql, CodEmpresa, row["CODNATUREZA"]).ToString();

                    sSql = "SELECT MENSAGEM FROM VOPERMENSAGEM WHERE CODEMPRESA = ? AND CODMENSAGEM = ?";
                    string sMensagem = dbs.QueryValue(string.Empty, sSql, CodEmpresa, CodMensagem).ToString();
                    if (sMensagem != string.Empty)
                    {
                        lMensagem.Add(sMensagem);
                    }
                }

                if (lMensagem.Count > 0)
                {
                    for (int i = 0; i < lMensagem.Count; i++)
                    {
                        Mensagem = string.Concat(lMensagem[i], " ");
                    }
                }

                return Mensagem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
