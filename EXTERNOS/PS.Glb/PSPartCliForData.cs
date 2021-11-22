using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartCliForData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Glb.Formula.Function function = new PS.Glb.Formula.Function();
        private PS.Lib.Interpretador interpreta = new PS.Lib.Interpretador();
        private PS.Lib.Valida vl = new PS.Lib.Valida();

        public override string ReadView()
        {
            return @"SELECT 
CODEMPRESA,
CODCLIFOR,
NOME,
NOMEFANTASIA,
CGCCPF,
CODCLASSIFICACAO,
FISICOJURIDICO,
CONVERT(BIT, ATIVO) ATIVO,
CEP,
CODTIPORUA,
RUA,
NUMERO,
COMPLEMENTO,
CODTIPOBAIRRO,
BAIRRO,
CODCIDADE,
(SELECT NOME FROM GCIDADE WHERE CODETD = VCLIFOR.CODETD AND CODCIDADE = VCLIFOR.CODCIDADE) CCIDADE,
CODETD,
CODPAIS,
TELRESIDENCIAL,
TELCOMERCIAL,
TELCELULAR,
TELFAX,
DATANASCIMENTO,
CODESTVIC,
NUMERORG,
OREMISSOR,
EMAIL,
INSCRICAOESTADUAL,
INSCRICAOMUNICIPAL,
INSCRICAOSUFRAMA,
CODNATJUR,
CODREGAPURACAO,
CEPENT,
CODTIPORUAENT,
RUAENT,
NUMEROENT,
COMPLEMENTOENT,
CODTIPOBAIRROENT,
BAIRROENT,
CODCIDADEENT,
(SELECT NOME FROM GCIDADE WHERE CODETD = VCLIFOR.CODETD AND CODCIDADE = VCLIFOR.CODCIDADEENT) CCIDADEENT,
CODETDENT,
CODPAISENT,
CEPPAG,
CODTIPORUAPAG,
RUAPAG,
NUMEROPAG,
COMPLEMENTOPAG,
CODTIPOBAIRROPAG,
BAIRROPAG,
CODCIDADEPAG,
(SELECT NOME FROM GCIDADE WHERE CODETD = VCLIFOR.CODETD AND CODCIDADE = VCLIFOR.CODCIDADEPAG) CCODCIDADEPAG,
CODETDPAG,
CODPAISPAG,
CONTRIBICMS,
NACIONALIDADE,
CODETDEMISSOR,
LIMITECREDITO,
CODREPRE,
CODTRANSPORTADORA,
FRETECIFFOB,
CODCONTA,
CODCCUSTO,
CODCONDICAOCOMPRA,
CODCONDICAOVENDA,
EMAILNFE,
CODTABPRECO,
WEBSITE,
DATACRIACAO,
CODUSUARIOCRIACAO,
CODNATUREZAORCAMENTO,
DESCMAXCOMPRA,
DESCMAXVENDA,
(SELECT IDTIPOCLIENTE FROM VTIPOCLIENTE WHERE VCLIFOR.IDTIPOCLIENTE = VTIPOCLIENTE.IDTIPOCLIENTE) IDTIPOCLIENTE,
CLASSVENDA,
PRODUTORRURAL,
CODVENDEDOR,
CODFORMA,
IDESTRANGEIRO
FROM VCLIFOR
WHERE ";
        }

        public decimal FinanceiroEmAberto(int CodEmpresa, string CodCliFor)
        {
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
)X";

            decimal ValorAberto = Convert.ToDecimal(dbs.QueryValue(0, sSql, CodEmpresa, CodCliFor));
            return ValorAberto;

        }

        public string GeraCodigo()
        {
            System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();

            string novoNumStr = "";

            if (int.Parse(PARAMVAREJO["CLIFORUSANUMEROSEQ"].ToString()) == 1)
            {
                string mask = PARAMVAREJO["CLIFORMASKNUMEROSEQ"].ToString();
                string ultimo = PARAMVAREJO["CLIFORULTIMONUMERO"].ToString();

                int num = 0;
                string str = "";

                for (int i = 0; i < mask.Length; i++)
                {
                    if (mask[i] == '?')
                    {
                        num++;
                    }
                    else
                    {
                        str = string.Concat(str, mask[i]);
                    }
                }

                string ultimoNum = "";
                int novoNum = 0;

                if (ultimo == "")
                {
                    ultimo = ultimoNum.PadLeft(num, '0');
                }
                else
                {
                    ultimo = ultimo.Substring(str.Length, num);
                }

                novoNum = int.Parse(ultimo) + 1;
                novoNumStr = string.Concat(str, novoNum.ToString().PadLeft(num, '0'));

                string sSql = @"UPDATE VPARAMETROS SET CLIFORULTIMONUMERO = ? WHERE CODEMPRESA = ?";

                dbs.QueryExec(sSql, novoNumStr, PS.Lib.Contexto.Session.Empresa.CodEmpresa);
            }

            return novoNumStr;
        }

        public void InsertRegistroComplementar(List<PS.Lib.DataField> objArr)
        {
            PSPartCliForComplData PartCliForComplData = new PSPartCliForComplData();
            PartCliForComplData._tablename = "VCLIFORCOMPL";
            PartCliForComplData._keys = new string[] { "CODEMPRESA", "CODCLIFOR" };

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODCLIFOR = gb.RetornaDataFieldByCampo(objArr, "CODCLIFOR");

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
            ListObjArr.Add(dfCODEMPRESA);
            ListObjArr.Add(dfCODCLIFOR);

            PartCliForComplData.SaveRecord(ListObjArr);

            /*
            string sSql = @"INSERT INTO VCLIFORCOMPL (CODEMPRESA, CODCLIFOR) VALUES(?,?)";

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODCLIFOR = gb.RetornaDataFieldByCampo(objArr, "CODCLIFOR");

            dbs.QueryExec(sSql, dfCODEMPRESA.Valor, dfCODCLIFOR.Valor);
            */
        }

        public void ExcluiRegistroComplementar(List<PS.Lib.DataField> objArr)
        {
            PSPartCliForComplData PartCliForComplData = new PSPartCliForComplData();
            PartCliForComplData._tablename = "VCLIFORCOMPL";
            PartCliForComplData._keys = new string[] { "CODEMPRESA", "CODCLIFOR" };

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODCLIFOR = gb.RetornaDataFieldByCampo(objArr, "CODCLIFOR");

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
            ListObjArr.Add(dfCODEMPRESA);
            ListObjArr.Add(dfCODCLIFOR);

            PartCliForComplData.DeleteRecord(ListObjArr);

            /*
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODCLIFOR = gb.RetornaDataFieldByCampo(objArr, "CODCLIFOR");

            sSql = @"DELETE FROM VCLIFORCOMPL WHERE CODEMPRESA = ? AND CODCLIFOR = ?";

            dbs.QueryExec(sSql, dtCODEMPRESA.Valor, dtCODCLIFOR.Valor);
            */
        }

        public void ExcluiContatoCliFor(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODCLIFOR = gb.RetornaDataFieldByCampo(objArr, "CODCLIFOR");

            sSql = @"SELECT CODCONTATO FROM VCLIFORCONTATO WHERE CODEMPRESA = ? AND CODCLIFOR = ?";

            System.Data.DataTable dt = dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODCLIFOR.Valor);

            if (dt.Rows.Count > 0)
            {
                PSPartContatoCliForData psPartContatoCliForData = new PSPartContatoCliForData();
                psPartContatoCliForData._tablename = "VCLIFORCONTATO";
                psPartContatoCliForData._keys = new string[] { "CODEMPRESA", "CODCLIFOR", "CODCONTATO" };

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PS.Lib.DataField dfCODCONTATO = new PS.Lib.DataField("CODCONTATO", dt.Rows[i]["CODCONTATO"]);

                    List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();
                    ListObjArr.Add(dfCODEMPRESA);
                    ListObjArr.Add(dfCODCLIFOR);
                    ListObjArr.Add(dfCODCONTATO);

                    psPartContatoCliForData.DeleteRecord(ListObjArr);

                }
            }

            /*
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODCLIFOR = gb.RetornaDataFieldByCampo(objArr, "CODCLIFOR");

            sSql = @"DELETE FROM VCLIFORCONTATO WHERE CODEMPRESA = ? AND CODCLIFOR = ?";

            dbs.QueryExec(sSql, dtCODEMPRESA.Valor, dtCODCLIFOR.Valor);
            */
        }

        public override void ValidateRecord(List<PS.Lib.DataField> objArr)
        {

            //base.ValidateRecord(objArr);
            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            if (dtCODEMPRESA.Valor == null)
                dtCODEMPRESA.Valor = PS.Lib.Contexto.Session.Empresa.CodEmpresa;

            PS.Lib.DataField dtCODCLIFOR = gb.RetornaDataFieldByCampo(objArr, "CODCLIFOR");
            PS.Lib.DataField dtCODCLASSIFICACAO = gb.RetornaDataFieldByCampo(objArr, "CODCLASSIFICACAO");
            PS.Lib.DataField dtCGCCPF = gb.RetornaDataFieldByCampo(objArr, "CGCCPF");
            PS.Lib.DataField dtFISJUR = gb.RetornaDataFieldByCampo(objArr, "FISICOJURIDICO");
            PS.Lib.DataField dtCODETD = gb.RetornaDataFieldByCampo(objArr, "CODETD");
            PS.Lib.DataField dtCODPAIS = gb.RetornaDataFieldByCampo(objArr, "CODPAIS");
            PS.Lib.DataField dtIDESTRANGEIRO = gb.RetornaDataFieldByCampo(objArr, "IDESTRANGEIRO");

            System.Data.DataRow PARAMETROS = gb.RetornaParametrosVarejo();

            #region Validação dos campos obrigatórios
            string pais = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GPAIS WHERE CODPAIS = ?", new object[] { dtCODPAIS.Valor }).ToString();

            if ( pais == "Brasil" || pais == "BRASIL" )
            {
                if (dtCGCCPF.Valor.ToString().Equals("   .   .   -"))
                {
                    throw new Exception("Favor Preencher o campo CNPJ/CPF corretamente.");
                }
                if (dtCGCCPF.Valor.ToString().Equals("  .   .   /    -"))
                {
                    throw new Exception("Favor Preencher o campo CNPJ/CPF corretamente.");
                }
                PS.Lib.Valida valida = new Lib.Valida();

                if (dtFISJUR.Valor.ToString().Equals("1"))
                {
                    //Valida o CPF
                    if (valida.validarCPF(dtCGCCPF.Valor.ToString()).Equals(false))
                    {
                        throw new Exception("CPF digitado não é válido.");
                    }
                }
                else
                {
                    //Valida o CNPJ
                    if (valida.validarCNPJ(dtCGCCPF.Valor.ToString()).Equals(false))
                    {
                        throw new Exception("CNPJ digitado não é válido.");
                    }
                }
                //Verifica se existe o CPF/CNPJ
                if (PARAMETROS["CLIFORCONSISTECGCCPF"].ToString() == "1")
                {
                    if (gb.ExisteCGCCPF(dtCGCCPF.Valor.ToString(), dtCODCLIFOR.Valor).Equals(true))
                    {
                        throw new Exception("Atenção. CGC/CPF informado já esta cadastrado");
                    }
                }
                //Verifica se o estado está preenchido 
                if (dtCODETD.Valor == null)
                {
                    throw new Exception("Atenção. Campo Estado é obrigatório.");
                }
            }
            else
            {
                if (dtIDESTRANGEIRO.Valor == null)
                {
                    throw new Exception("Atenção. Campo ID. Estrangeiro é obrigatório.");
                }
            }

            #region Validação do CODETD E CODPAIS
            if (dtCODPAIS.Valor == null)
            {
                throw new Exception("Atenção. Campo Pais é obrigatório.");
            }

            #endregion
            #endregion

            #region Gera Código Cliente/Fornecedor

            if (dtCODCLIFOR.Valor == null)
            {
                dtCODCLIFOR.Valor = GeraCodigo();

                for (int i = 0; i < objArr.Count; i++)
                {
                    if (objArr[i].Field == "CODCLIFOR")
                    {
                        objArr[i].Valor = dtCODCLIFOR.Valor;
                    }
                }
            }
            #endregion

            base.ValidateRecord(objArr);
        }

        public override List<PS.Lib.DataField> InsertRecord(List<PS.Lib.DataField> objArr)
        {
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

            List<PS.Lib.DataField> objArrDDL = objArr;

            List<PS.Lib.DataField> temp = base.InsertRecord(objArr);

            InsertRegistroComplementar(objArrDDL);

            return temp;
        }
        private void insertCampoHistorico()
        {

        }
        public override void DeleteRecord(List<PS.Lib.DataField> objArr)
        {
            base.ValidateKeyRecord(objArr);

            ExcluiRegistroComplementar(objArr);
            ExcluiContatoCliFor(objArr);

            base.DeleteRecord(objArr);
        }
    }
}
