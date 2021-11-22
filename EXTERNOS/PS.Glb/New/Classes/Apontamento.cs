using DevExpress.XtraEditors;
using PS.Glb.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Glb.New.Classes
{
    public class Apontamento
    {
        #region Variáveis

        private Classes.Utilidades util = new Utilidades();
        private string sql = "";

        public New.Classes.Models.AAPONTAMENTO apontamento;
        public string MensagemFinalEmail = "";

        public MensagemEmail mensagem;

        #endregion

        #region Construtor

        public Apontamento()
        {
            apontamento = new Models.AAPONTAMENTO();

            mensagem = new MensagemEmail();
            mensagem.ParametroEmail = new List<string>();
            mensagem.ApontamentosComSucesso = new List<string>();
            mensagem.ApontamentosComErro = new List<string>();
            mensagem.Motivo = new List<string>();
        }

        #endregion

        #region Métodos

        public DataTable CarregarGrid(string condicao)
        {
            sql = @"SELECT AP.CODEMPRESA,
                           AP.CODFILIAL,
                           IDAPONTAMENTO,
                           IDPROJETO,
                           AP.IDUNIDADE,
                           AU.NOME,
                           CODUSUARIO,
                           DATA,
                           CONVERT(VARCHAR(10), ((TERMINO - INICIO) - ABONO), 108) AS 'TOTALHORAS',
                           VALORAD,
                           MOTVALAD,
                           INLOCO,
                           REEMBOLSO,
                           DATAENVIO,
                           DATARETORNO,
                           CASE WHEN IDSTATUSAPONTAMENTO = 0 THEN '0 - EM DIGITAÇÃO'
                       WHEN IDSTATUSAPONTAMENTO = 1 THEN '1 - CONCLUIDO'
                       WHEN IDSTATUSAPONTAMENTO = 2 THEN '2 - APROVADO'
                       WHEN IDSTATUSAPONTAMENTO = 3 THEN '3 - INTEGRADO' END AS 'IDSTATUSAPONTAMENTO',
                            MOTIVOREPROVACAO,
                            CODOPERDEMANDA,
                            AP.CODEMPRESA,
                            CODOPERREEMBOLSOC,
                               CODOPERREEMBOLSOA,
                            PENALIDADE,
                            DATAPENALIDADE,
                            DATAPROCESSAMENTO,
                            DATADIGITACAO,
                            TIPOFATURAMENTO
                FROM AAPONTAMENTO AP

                INNER JOIN AUNIDADE AU
                ON AU.IDUNIDADE = AP.IDUNIDADE";

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql + " " + condicao, new object[] { });

            return dt;
        }

        /// <summary>
        /// Integra o Apontamento para Faturamento, transformando-o em um movimento de Operação.
        /// </summary>
        /// <param name="idApontamento">ID do Apontamento</param>
        /// <param name="codUsuario">Código do Usuário</param>
        public void IntegrarApontamento(string idApontamento, string codUsuario)
        {
            #region Declaração de variáveis

            string[] tipoFaturamento = new string[25];

            bool reembolso = false;
            bool integraReembolsoAnalista = false;

            string codOperInLoco = "";
            string codOperReembolso = "";
            string codOperDemanda = "";

            string codCondicaoVenda = "";
            string nSeq = "";
            string numeroSequencial = "";
            string codSerie = "";
            string codCliFor = "";
            string codCentroCusto = "";
            string codNatureza = "";
            string nome = "";
            string nomeProduto = "";
            string codProdutoDemanda = "";
            string codUnidadeControle = "";
            string codProdutoCliente = "";
            string codProdutoAnalista = "";
            string codUnidadeOper = "";
            string codOperador = "";

            DateTime? dataEmissao = null;
            DateTime? dataEntradaSaida = null;

            decimal reembolsoCliente = 0;
            decimal reembolsoAnalista = 0;
            decimal valorDemanda = 0;
            decimal valorHora = 0;

            int inLoco = 0;

            #endregion

            try
            {
                // Verifica se o apontamento já foi integrado
                sql = String.Format(@"select CODOPERDEMANDA from AAPONTAMENTO where IDAPONTAMENTO = '{0}'", idApontamento);

                if (!String.IsNullOrWhiteSpace(MetodosSQL.GetField(sql, "CODOPERDEMANDA")))
                {
                    MessageBox.Show("Apontamento ja foi integrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    sql = String.Empty;

                    string sql1 = String.Format(@"SELECT SERIEDEFAULT, NUMSEQ FROM GTIPOPER 

                                            inner join VSERIE
                                            on VSERIE.CODSERIE = GTIPOPER.SERIEDEFAULT

                                            WHERE CODTIPOPER = '2.1.01'");

                    string sql2 = String.Format(@"SELECT 
	AAPONTAMENTO.CODEMPRESA,
	AAPONTAMENTO.CODFILIAL,
	AAPONTAMENTO.IDAPONTAMENTO,
	AAPONTAMENTO.REEMBOLSO,
	AAPONTAMENTO.INLOCO,
	AAPONTAMENTO.DATA,
	AUNIDADEREEMBOLSO.CODPRODUTO AS 'CODPRODUTOANALISTA',
	AUNIDADE.CODPRODUTO AS 'CODPRODUTOCLIENTE',
	AUNIDADE.IDUNIDADE,

	APROJETO.CODPRODUTO AS 'CODPRODUTODEMANDA',
	APROJETO.IDPROJETO,
	APROJETO.CODNATUREZA,
	APROJETO.CODCCUSTO,
	APROJETO.TIPO,
	(SELECT APROJETOTAREFA.TIPOFATURAMENTO FROM APROJETOTAREFA WHERE APROJETOTAREFA.CODEMPRESA = AAPONTAMENTOTAREFA.CODEMPRESA AND APROJETOTAREFA.CODFILIAL = AAPONTAMENTOTAREFA.CODFILIAL AND APROJETOTAREFA.IDTAREFA = AAPONTAMENTOTAREFA.IDTAREFA) TIPOFATURAMENTO,
	UPPER(AAPONTAMENTO.CODUSUARIO) CODUSUARIO,
	VCLIFOR.*,
     
	CASE WHEN DATEPART(DAY, DATA) <= 15 THEN DATEADD(DAY, (16 - DATEPART(DAY, DATA)), DATA) ELSE DATEADD(MONTH, DATEDIFF(MONTH, 0, DATEADD(MONTH, 1, DATA)), 0)  END AS 'DATAENTSAI',
		   (AUNIDADE.DISTANCIAKM* AUNIDADE.VALORKM) + AUNIDADE.VALORPEDAGIO + AUNIDADE.VALORREFEICAO AS 'REEMBOLSOCLIENTE',
	GUSUARIO.CODCLIFOR AS 'FORNECEDOR',
	AUNIDADE.CODCLIFOR AS 'CLIENTEOPER'
        
FROM 
	AAPONTAMENTO

	INNER JOIN AUNIDADE
	ON AUNIDADE.CODEMPRESA = AAPONTAMENTO.CODEMPRESA 
	AND AUNIDADE.CODFILIAL = AAPONTAMENTO.CODFILIAL 
	AND AUNIDADE.IDUNIDADE = AAPONTAMENTO.IDUNIDADE

	INNER JOIN APROJETO
	ON APROJETO.CODEMPRESA = AAPONTAMENTO.CODEMPRESA
	AND APROJETO.CODFILIAL = AAPONTAMENTO.CODFILIAL 
	AND APROJETO.IDPROJETO = AAPONTAMENTO.IDPROJETO 

	INNER JOIN AUNIDADEREEMBOLSO
	ON AUNIDADEREEMBOLSO.CODEMPRESA = AUNIDADE.CODEMPRESA 
	AND AUNIDADEREEMBOLSO.CODFILIAL = AUNIDADE.CODFILIAL 
	AND AUNIDADEREEMBOLSO.IDUNIDADE = AUNIDADE.IDUNIDADE
	AND AUNIDADEREEMBOLSO.CODUSUARIO = AAPONTAMENTO.CODUSUARIO

	INNER JOIN VCLIFOR
	ON VCLIFOR.CODEMPRESA = AUNIDADE.CODEMPRESA 
	AND VCLIFOR.CODCLIFOR = AUNIDADE.CODCLIFOR

	INNER JOIN GUSUARIO
	ON GUSUARIO.CODUSUARIO = AAPONTAMENTO.CODUSUARIO

	INNER JOIN AAPONTAMENTOTAREFA
	ON AAPONTAMENTO.CODEMPRESA = AAPONTAMENTOTAREFA.CODEMPRESA
	AND AAPONTAMENTO.CODFILIAL = AAPONTAMENTOTAREFA.CODFILIAL
	AND AAPONTAMENTO.IDAPONTAMENTO = AAPONTAMENTOTAREFA.IDAPONTAMENTO

	INNER JOIN APROJETOTAREFA
	ON AAPONTAMENTOTAREFA.CODEMPRESA = APROJETOTAREFA.CODEMPRESA
	AND AAPONTAMENTOTAREFA.CODFILIAL = APROJETOTAREFA.CODFILIAL
	AND AAPONTAMENTOTAREFA.IDTAREFA = APROJETOTAREFA.IDTAREFA

WHERE 
	AAPONTAMENTO.CODEMPRESA = {0}
AND AAPONTAMENTO.CODFILIAL = {1}
AND AAPONTAMENTO.IDAPONTAMENTO = {2}", AppLib.Context.Empresa, AppLib.Context.Filial, idApontamento);

                    string sql3 = String.Format(@"select CODCLIFOR from GUSUARIO where CODUSUARIO = '{0}'", codUsuario);

                    string sqlDemanda = String.Format(@"select (DATEDIFF(HOUR, INICIO, TERMINO)-DATEPART(HOUR, ABONO))*VALORHORA as 'DEMANDA' from AAPONTAMENTO

                                                inner join APROJETO
                                                on APROJETO.IDPROJETO = AAPONTAMENTO.IDPROJETO

                                                where IDAPONTAMENTO = '{0}'", idApontamento);

                    string sqlReembolso = String.Format(@"select (DISTANCIAKM*VALORKM)+VALORPEDAGIO+VALORREFEICAO as 'REEMBOLSO' from AUNIDADEREEMBOLSO where CODUSUARIO = '{0}' AND IDUNIDADE = {1}", codUsuario, MetodosSQL.GetField(sql2, "IDUNIDADE"));

                    string sqlProdutoReembolso = String.Format(@"select CODPRODUTO from AUNIDADEREEMBOLSO where IDUNIDADE = '{0}' AND CODUSUARIO = '{1}'", MetodosSQL.GetField(sql2, "IDUNIDADE"), codUsuario);

                    string sqlAprojeto = String.Format(@"select * from APROJETO where IDPROJETO = '{0}'", MetodosSQL.GetField(sql2, "IDPROJETO"));

                    string sqlVproduto = "select * from VPRODUTO where CODPRODUTO = '{0}'";

                    string sqlProdutoDemanda = String.Format("SELECT CODPRODUTO FROM APROJETO WHERE CODEMPRESA = {0} AND CODFILIAL = {1} AND IDUNIDADE = {2} AND IDPROJETO = {3}", AppLib.Context.Empresa, AppLib.Context.Filial, MetodosSQL.GetField(sql2, "IDUNIDADE"), MetodosSQL.GetField(sql2, "IDPROJETO"));

                    string sqlOperador = String.Format("SELECT * FROM VOPERADOR WHERE CODEMPRESA = {0} AND CODUSUARIO = '{1}'", AppLib.Context.Empresa, codUsuario);

                    #region Atribui valor as variáveis

                    inLoco = Convert.ToInt32(MetodosSQL.GetField(sql2, "INLOCO"));
                    reembolso = MetodosSQL.GetField(sql2, "REEMBOLSO") != "0";

                    codCondicaoVenda = MetodosSQL.GetField(sql2, "CODCONDICAOVENDA");
                    nSeq = MetodosSQL.GetField(sql1, "NUMSEQ");
                    numeroSequencial = MetodosSQL.GetField(sql1, "NUMSEQ");
                    codSerie = MetodosSQL.GetField(sql1, "SERIEDEFAULT");
                    codCliFor = MetodosSQL.GetField(sql2, "CLIENTEOPER");
                    codCentroCusto = MetodosSQL.GetField(sql2, "CODCCUSTO");
                    codNatureza = MetodosSQL.GetField(sql2, "CODNATUREZA");
                    nome = MetodosSQL.GetField(sql2, "NOME");
                    codProdutoDemanda = MetodosSQL.GetField(sqlProdutoDemanda, "CODPRODUTO");
                    nomeProduto = MetodosSQL.GetField(String.Format(sqlVproduto, codProdutoDemanda), "NOME");
                    codUnidadeControle = MetodosSQL.GetField(String.Format(sqlVproduto, codProdutoDemanda), "CODUNIDCONTROLE");
                    codOperador = MetodosSQL.GetField(sqlOperador, "CODOPERADOR");

                    integraReembolsoAnalista = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, @"SELECT INTEGRAREEMBOLSOANALISTA FROM VPARAMETROS WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }));

                    DataTable dtFaturamento = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql2);

                    for (int i = 0; i < dtFaturamento.Rows.Count; i++)
                    {
                        tipoFaturamento[i] = dtFaturamento.Rows[i]["TIPOFATURAMENTO"].ToString();
                    }

                    tipoFaturamento = tipoFaturamento.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                    codProdutoCliente = MetodosSQL.GetField(sql2, "CODPRODUTOCLIENTE");

                    if (reembolso)
                    {
                        codProdutoAnalista = MetodosSQL.GetField(sqlProdutoReembolso, "CODPRODUTO");
                    }

                    // Confirmar se o código do produto será da demanda.
                    codUnidadeOper = MetodosSQL.GetField(String.Format(sqlVproduto, codProdutoDemanda), "CODUNIDVENDA");

                    dataEmissao = Convert.ToDateTime(MetodosSQL.GetField(sql2, "DATA"));
                    dataEntradaSaida = Convert.ToDateTime(MetodosSQL.GetField(sql2, "DATAENTSAI"));

                    reembolsoCliente = Convert.ToDecimal(MetodosSQL.GetField(sql2, "REEMBOLSOCLIENTE"));
                    reembolsoAnalista = Convert.ToDecimal(MetodosSQL.GetField(sqlReembolso, "REEMBOLSO"));
                    valorDemanda = Convert.ToDecimal(MetodosSQL.GetField(sqlDemanda, "DEMANDA"));
                    valorHora = Convert.ToDecimal(MetodosSQL.GetField(sqlAprojeto, "VALORHORA"));

                    #endregion

                    // Verifica condição de pagamento
                    if (string.IsNullOrWhiteSpace(codCondicaoVenda))
                    {
                        throw new Exception("Cliente sem condição de pagamento associada.");
                    }

                    // Valida o Operador
                    // Verifica condição de pagamento
                    if (string.IsNullOrWhiteSpace(codOperador))
                    {
                        throw new Exception("Consultor não possue Operador vinculado.");
                    }

                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();

                    conn.BeginTransaction();

                    try
                    {
                        #region GOPER

                        AppLib.ORM.Jit goper = new AppLib.ORM.Jit(conn, "GOPER");

                        if (inLoco == 1 && reembolso)
                        {
                            // Reembolso do Cliente 

                            codOperInLoco = util.getMaxOper("GOPER").ToString();

                            goper.Set("CODOPER", codOperInLoco);
                            goper.Set("SEGUNDONUMERO", idApontamento);
                            goper.Set("VALORBRUTO", reembolsoCliente.ToString().Replace(",", "."));
                            goper.Set("VALORLIQUIDO", reembolsoCliente.ToString().Replace(",", "."));
                            goper.Set("CODEMPRESA", AppLib.Context.Empresa);
                            goper.Set("CODFILIAL", AppLib.Context.Filial);
                            goper.Set("CODTIPOPER", "2.1.01");
                            goper.Set("CODSTATUS", "0");
                            goper.Set("NUMERO", numeroSequencial);

                            sql = String.Format(@"update VSERIE set NUMSEQ = RIGHT('000000'+CAST(ISNULL({0}+1,0) AS VARCHAR),6) where CODSERIE = '{1}'", numeroSequencial, codSerie);
                            MetodosSQL.ExecQuery(sql);

                            numeroSequencial = MetodosSQL.GetField(sql1, "NUMSEQ");

                            goper.Set("CODCLIFOR", codCliFor);
                            goper.Set("CODSERIE", codSerie);
                            goper.Set("CODLOCAL", "001");
                            goper.Set("DATACRIACAO", AppLib.Context.poolConnection.Get("Start").GetDateTime().ToString("yyy-MM-dd"));
                            goper.Set("DATAEMISSAO", dataEmissao);
                            goper.Set("DATAENTSAI", dataEntradaSaida);
                            goper.Set("CODUSUARIOCRIACAO", AppLib.Context.Usuario);
                            goper.Set("CODNATUREZAORCAMENTO", codNatureza);
                            goper.Set("CODCONDICAO", codCondicaoVenda);
                            goper.Set("CODCCUSTO", codCentroCusto);
                            goper.Set("NOMEFANTASIA", nome);
                            goper.Set("CODOPERADOR", codOperador);

                            goper.Insert();

                            #region GOPERITEM

                            AppLib.ORM.Jit goperitem = new AppLib.ORM.Jit(conn, "GOPERITEM");
                            goperitem.Set("CODEMPRESA", AppLib.Context.Empresa);
                            goperitem.Set("CODNATUREZA", "5.949.01");

                            goperitem.Set("CODOPER", codOperInLoco);
                            goperitem.Set("NSEQITEM", 1);
                            goperitem.Set("CODPRODUTO", codProdutoCliente);
                            goperitem.Set("QUANTIDADE", 1);
                            goperitem.Set("QUANTIDADE_SALDO", 1);
                            goperitem.Set("QUANTIDADECONTROLE", 1);
                            goperitem.Set("VLUNITARIO", reembolsoCliente.ToString().Replace(",", "."));
                            goperitem.Set("VLUNITORIGINAL", reembolsoCliente.ToString().Replace(",", "."));
                            goperitem.Set("VLTOTALITEM", reembolsoCliente.ToString().Replace(",", "."));

                            codUnidadeOper = MetodosSQL.GetField(String.Format(sqlVproduto, codProdutoCliente), "CODUNIDVENDA");
                            goperitem.Set("CODUNIDOPER", codUnidadeOper);

                            goperitem.Set("NOMEPRODUTO", nomeProduto);
                            goperitem.Set("CODUNIDCONTROLE", codUnidadeControle);
                            goperitem.Set("FATORCONVERSAO", 1);
                            goperitem.Set("QUANTIDADECONTROLE", 1);

                            goperitem.Insert();

                            #endregion

                            #region GOPERITEMCOMPL

                            AppLib.ORM.Jit goperitemcompl = new AppLib.ORM.Jit(conn, "GOPERITEMCOMPL");
                            goperitemcompl.Set("CODEMPRESA", AppLib.Context.Empresa);
                            goperitemcompl.Set("CODOPER", codOperInLoco);
                            goperitemcompl.Set("NSEQITEM", 1);
                            goperitemcompl.Set("DESCCOMPLPRODUTO", "REEMBOLSO DO CLIENTE");

                            goperitemcompl.Insert();

                            #endregion

                            // Reembolso do Analista

                            if (integraReembolsoAnalista)
                            {
                                codOperReembolso = util.getMaxOper("GOPER").ToString();

                                goper.Set("CODOPER", codOperReembolso);
                                goper.Set("SEGUNDONUMERO", idApontamento);
                                goper.Set("CODTIPOPER", "1.1.02");
                                goper.Set("VALORBRUTO", reembolsoAnalista.ToString().Replace(",", "."));
                                goper.Set("VALORLIQUIDO", reembolsoAnalista.ToString().Replace(",", "."));
                                goper.Set("CODEMPRESA", AppLib.Context.Empresa);
                                goper.Set("CODFILIAL", AppLib.Context.Filial);
                                goper.Set("CODSTATUS", "0");
                                goper.Set("NUMERO", numeroSequencial);

                                sql = String.Format(@"update VSERIE set NUMSEQ = RIGHT('000000'+CAST(ISNULL({0}+1,0) AS VARCHAR),6) where CODSERIE = '{1}'", numeroSequencial, codSerie);
                                MetodosSQL.ExecQuery(sql);

                                numeroSequencial = MetodosSQL.GetField(sql1, "NUMSEQ");

                                goper.Set("CODCLIFOR", codCliFor);
                                goper.Set("CODSERIE", codSerie);
                                goper.Set("CODLOCAL", "001");
                                goper.Set("DATACRIACAO", AppLib.Context.poolConnection.Get("Start").GetDateTime().ToString("yyy-MM-dd"));
                                goper.Set("DATAEMISSAO", dataEmissao);
                                goper.Set("DATAENTSAI", dataEntradaSaida);
                                goper.Set("CODUSUARIOCRIACAO", AppLib.Context.Usuario);
                                goper.Set("CODNATUREZAORCAMENTO", codNatureza);
                                goper.Set("CODCONDICAO", codCondicaoVenda);
                                goper.Set("CODCCUSTO", codCentroCusto);
                                goper.Set("NOMEFANTASIA", nome);
                                goper.Set("CODOPERADOR", codOperador);

                                goper.Insert();

                                #region GOPERITEM

                                AppLib.ORM.Jit goperitemAnalista = new AppLib.ORM.Jit(conn, "GOPERITEM");
                                goperitemAnalista.Set("CODEMPRESA", AppLib.Context.Empresa);
                                goperitemAnalista.Set("CODNATUREZA", "1.949.01");

                                goperitemAnalista.Set("CODOPER", codOperReembolso);
                                goperitemAnalista.Set("NSEQITEM", 1);
                                goperitemAnalista.Set("CODPRODUTO", codProdutoAnalista);
                                goperitemAnalista.Set("QUANTIDADE", 1);
                                goperitemAnalista.Set("QUANTIDADE_SALDO", 1);
                                goperitemAnalista.Set("QUANTIDADECONTROLE", 1);
                                goperitemAnalista.Set("VLUNITARIO", reembolsoAnalista.ToString().Replace(",", "."));
                                goperitemAnalista.Set("VLUNITORIGINAL", reembolsoAnalista.ToString().Replace(",", "."));
                                goperitemAnalista.Set("VLTOTALITEM", reembolsoAnalista.ToString().Replace(",", "."));
                                goperitemAnalista.Set("CODUNIDOPER", codUnidadeOper);
                                goperitemAnalista.Set("NOMEPRODUTO", nomeProduto);
                                goperitemAnalista.Set("CODUNIDCONTROLE", codUnidadeControle);
                                goperitemAnalista.Set("FATORCONVERSAO", 1);
                                goperitemAnalista.Set("QUANTIDADECONTROLE", 1);

                                goperitemAnalista.Insert();

                                #endregion

                                #region GOPERITEMCOMPL

                                AppLib.ORM.Jit goperitemcomplAnalista = new AppLib.ORM.Jit(conn, "GOPERITEMCOMPL");
                                goperitemcomplAnalista.Set("CODEMPRESA", AppLib.Context.Empresa);
                                goperitemcomplAnalista.Set("CODOPER", codOperReembolso);
                                goperitemcomplAnalista.Set("NSEQITEM", 1);
                                goperitemcomplAnalista.Set("DESCCOMPLPRODUTO", "REEMBOLSO DO ANALISTA");

                                goperitemcomplAnalista.Insert();

                                #endregion
                            }
                        }

                        foreach (string fat in tipoFaturamento)
                        {
                            if (fat.Contains("D"))
                            {
                                codOperDemanda = util.getMaxOper("GOPER").ToString();

                                goper.Set("CODOPER", codOperDemanda);
                                goper.Set("SEGUNDONUMERO", idApontamento);
                                goper.Set("CODTIPOPER", "2.1.01");
                                goper.Set("VALORBRUTO", CalcularValorTotalDemanda(idApontamento, valorHora).ToString().Replace(",", "."));
                                goper.Set("VALORLIQUIDO", CalcularValorTotalDemanda(idApontamento, valorHora).ToString().Replace(",", "."));

                                goper.Set("CODEMPRESA", AppLib.Context.Empresa);
                                goper.Set("CODFILIAL", AppLib.Context.Filial);
                                goper.Set("CODSTATUS", "0");
                                goper.Set("NUMERO", numeroSequencial);

                                sql = String.Format(@"update VSERIE set NUMSEQ = RIGHT('000000'+CAST(ISNULL({0}+1,0) AS VARCHAR),6) where CODSERIE = '{1}'", numeroSequencial, codSerie);
                                MetodosSQL.ExecQuery(sql);

                                numeroSequencial = MetodosSQL.GetField(sql1, "NUMSEQ");

                                goper.Set("CODCLIFOR", codCliFor);
                                goper.Set("CODSERIE", codSerie);
                                goper.Set("CODLOCAL", "001");
                                goper.Set("DATACRIACAO", AppLib.Context.poolConnection.Get("Start").GetDateTime().ToString("yyy-MM-dd"));
                                goper.Set("DATAEMISSAO", dataEmissao);
                                goper.Set("DATAENTSAI", dataEntradaSaida);
                                goper.Set("CODUSUARIOCRIACAO", AppLib.Context.Usuario);
                                goper.Set("CODNATUREZAORCAMENTO", codNatureza);
                                goper.Set("CODCONDICAO", codCondicaoVenda);
                                goper.Set("CODCCUSTO", codCentroCusto);
                                goper.Set("NOMEFANTASIA", nome);
                                goper.Set("CODOPERADOR", codOperador);

                                goper.Insert();

                                #region GOPERITEM

                                // Itens de acordo com o número de demandas
                                int count = ListarDemandas("D", Convert.ToInt32(idApontamento));
                                decimal quantidade = 0;

                                sql = String.Format(@"SELECT HORAS, IDTAREFA FROM AAPONTAMENTOTAREFA WHERE IDAPONTAMENTO = '{0}'", MetodosSQL.GetField(sql2, "IDAPONTAMENTO"));
                                DataTable dtDemanda = MetodosSQL.GetDT(sql);

                                DataRow row = null;

                                DateTime? dataApontamento = null;

                                for (int i = 1; i <= count; i++)
                                {
                                    row = dtDemanda.Rows[i - 1];

                                    if (row["HORAS"] != DBNull.Value)
                                    {
                                        dataApontamento = Convert.ToDateTime(row["HORAS"]);

                                        quantidade = BuscarQuantidadeTempoDemanda(Convert.ToDateTime(dataApontamento));
                                    }

                                    AppLib.ORM.Jit goperitemDemanda = new AppLib.ORM.Jit(conn, "GOPERITEM");
                                    goperitemDemanda.Set("CODEMPRESA", AppLib.Context.Empresa);
                                    goperitemDemanda.Set("CODNATUREZA", "5.949.01");

                                    goperitemDemanda.Set("CODOPER", codOperDemanda);
                                    goperitemDemanda.Set("NSEQITEM", i);
                                    goperitemDemanda.Set("CODPRODUTO", codProdutoDemanda);
                                    goperitemDemanda.Set("QUANTIDADE", quantidade);
                                    goperitemDemanda.Set("QUANTIDADE_SALDO", quantidade);
                                    goperitemDemanda.Set("QUANTIDADECONTROLE", quantidade);
                                    goperitemDemanda.Set("VLUNITARIO", valorHora.ToString().Replace(",", "."));
                                    goperitemDemanda.Set("VLUNITORIGINAL", valorHora.ToString().Replace(",", "."));
                                    goperitemDemanda.Set("VLTOTALITEM", (quantidade * valorHora).ToString().Replace(",", "."));

                                    codUnidadeOper = MetodosSQL.GetField(String.Format(sqlVproduto, codProdutoDemanda), "CODUNIDVENDA");
                                    goperitemDemanda.Set("CODUNIDOPER", codUnidadeOper);

                                    goperitemDemanda.Set("NOMEPRODUTO", nomeProduto);
                                    goperitemDemanda.Set("CODUNIDCONTROLE", codUnidadeControle);
                                    goperitemDemanda.Set("FATORCONVERSAO", 1);

                                    goperitemDemanda.Insert();
                                }

                                #endregion

                                #region GOPERITEMCOMPL

                                for (int i = 1; i <= count; i++)
                                {
                                    AppLib.ORM.Jit goperitemcomplDemanda = new AppLib.ORM.Jit(conn, "GOPERITEMCOMPL");
                                    goperitemcomplDemanda.Set("CODEMPRESA", AppLib.Context.Empresa);
                                    goperitemcomplDemanda.Set("CODOPER", codOperDemanda);
                                    goperitemcomplDemanda.Set("NSEQITEM", i);
                                    goperitemcomplDemanda.Set("DESCCOMPLPRODUTO", "DEMANDA DO CLIENTE");

                                    goperitemcomplDemanda.Insert();
                                }

                                break;

                                #endregion
                            }
                        }

                        #endregion

                        #region Update AAPONTAMENTOTAREFA

                        if (string.IsNullOrEmpty(codOperInLoco) && string.IsNullOrEmpty(codOperReembolso) && string.IsNullOrEmpty(codOperDemanda))
                        {
                            sql = String.Format(@"UPDATE AAPONTAMENTO SET CODOPERDEMANDA = 0, CODOPERREEMBOLSOC = 0, CODOPERREEMBOLSOA = 0, IDSTATUSAPONTAMENTO = '3' WHERE IDAPONTAMENTO = '{0}'", MetodosSQL.GetField(sql2, "IDAPONTAMENTO"));
                        }
                        else
                        {
                            sql = String.Format(@"UPDATE AAPONTAMENTO
                                        SET CODOPERDEMANDA = '{0}',
	                                        CODOPERREEMBOLSOC = '{1}',
	                                        CODOPERREEMBOLSOA = '{2}',
                                            IDSTATUSAPONTAMENTO = '3'
                                        WHERE IDAPONTAMENTO = '{3}'",
                                                 codOperDemanda,
                                                 codOperInLoco,
                                                 codOperReembolso,
                                                 MetodosSQL.GetField(sql2, "IDAPONTAMENTO"));
                        }

                        MetodosSQL.ExecQuery(sql);

                        #endregion

                        conn.Commit();
                    }
                    catch (Exception ex)
                    {
                        conn.Rollback();
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Solicita a correção do Apontamento, permitindo assim sua edição e atualizando seu status 0 (Em Digitação).
        /// </summary>
        /// <param name="idApontamento">ID do Apontamento</param>
        public void ReabrirApontamento(string idApontamento)
        {
            try
            {
                sql = String.Format(@"UPDATE AAPONTAMENTO SET IDSTATUSAPONTAMENTO = '0' WHERE IDAPONTAMENTO = '{0}'", idApontamento);

                MetodosSQL.ExecQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Remove todas as operações geradas à partir do Apontamento selecionado.
        /// </summary>
        /// <param name="idApontamento">ID do Apontamento</param>
        public void CancelarIntegracao(string idApontamento)
        {
            var operacoes = ListarOperacoesIntegradas(idApontamento);

            string codoperDemanda = operacoes[0];
            string codOperReembolsoCliente = operacoes[1];
            string codOperReembolsoAnalista = operacoes[2];

            try
            {
                // Exclui as operações de acordo com os respectivos códigos das operações
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM GOPER WHERE CODOPER = ? OR CODOPER = ? OR CODOPER = ?", new object[] { codoperDemanda, codOperReembolsoCliente, codOperReembolsoAnalista });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

            try
            {
                // Atualiza o respectivo Apontamento
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE AAPONTAMENTO SET CODOPERDEMANDA = NULL, CODOPERREEMBOLSOC = NULL, CODOPERREEMBOLSOA = NULL WHERE IDAPONTAMENTO = ?", new object[] { idApontamento });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

            AtualizarStatusApontamento(idApontamento, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idApontamento"></param>
        /// <param name="motivo"></param>
        public void ReprovarApontamento(string idApontamento, string motivo)
        {
            try
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO AAPONTAMENTOREPROVACAO VALUES (?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, idApontamento, AppLib.Context.Usuario, DateTime.Now.ToString("yyy-MM-dd"), motivo });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            AtualizarStatusApontamento(idApontamento, 4);
        }

        /// <summary>
        /// Lista o número de tarefas do tipo Demanda geradas à partir do Apontamento selecionado.
        /// </summary>
        /// <param name="tipoFaturamento">Tipo de Faturamento</param>
        /// <param name="idApontamento">ID do Apontamento</param>
        /// <returns>Quantidade de tarefas do tipo Demanda</returns>
        private int ListarDemandas(string tipoFaturamento, int idApontamento)
        {
            int numeroDemandas = 0;

            DataTable dtNumeroDemandas = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT *
                                                                                                FROM AAPONTAMENTOTAREFA  AP
                                                                                                INNER JOIN APROJETOTAREFA PT
                                                                                                ON PT.CODEMPRESA = AP.CODEMPRESA AND PT.CODFILIAL = AP.CODFILIAL AND PT.IDTAREFA = AP.IDTAREFA
                                                                                                WHERE PT.CODEMPRESA = ? AND PT.CODFILIAL = ? AND PT.TIPOFATURAMENTO = ? AND AP.IDAPONTAMENTO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, tipoFaturamento, idApontamento });

            if (dtNumeroDemandas != null)
            {
                numeroDemandas = dtNumeroDemandas.Rows.Count;
            }

            return numeroDemandas;
        }

        private string[] ListarOperacoesIntegradas(string idApontamento)
        {
            string[] operacoes = new string[] { string.Empty, string.Empty, string.Empty };

            DataTable dtOperacoes = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODOPERDEMANDA, CODOPERREEMBOLSOC, CODOPERREEMBOLSOA 
                                                                                            FROM AAPONTAMENTO 
                                                                                            WHERE IDAPONTAMENTO = ?", new object[] { idApontamento });

            for (int i = 0; i <= 2; i++)
            {
                // Demanda
                if (i == 0)
                {
                    operacoes[i] = dtOperacoes.Rows[0]["CODOPERDEMANDA"].ToString();
                }
                // Reembolso Cliente
                else if (i == 1)
                {
                    operacoes[i] = dtOperacoes.Rows[0]["CODOPERREEMBOLSOC"].ToString();
                }
                // Reembolso Analista
                else if (i == 2)
                {
                    operacoes[i] = dtOperacoes.Rows[0]["CODOPERREEMBOLSOA"].ToString();
                }
            }

            return operacoes;
        }

        private decimal BuscarQuantidadeTempoDemanda(DateTime dataApontamento)
        {
            int horasApontamento = 0;
            int minutosApontamento = 0;

            double minutosConvertidos = 0;
            decimal tempoDemandado = 0;

            if (dataApontamento != null)
            {
                horasApontamento = dataApontamento.Hour;
                minutosApontamento = dataApontamento.Minute;

                switch (minutosApontamento)
                {
                    case 15:
                        minutosConvertidos = 0.25;
                        break;
                    case 30:
                        minutosConvertidos = 0.50;
                        break;
                    case 45:
                        minutosConvertidos = 0.75;
                        break;
                    default:
                        break;
                }

                tempoDemandado = horasApontamento + Convert.ToDecimal(minutosConvertidos);
            }

            return tempoDemandado;
        }

        private decimal CalcularValorTotalDemanda(string idApontamento, decimal valorHora)
        {
            decimal valorTotal = 0;
            decimal quantidade = 0;

            sql = String.Format(@"SELECT HORAS, IDTAREFA FROM AAPONTAMENTOTAREFA WHERE IDAPONTAMENTO = '{0}'", idApontamento);
            DataTable dtDemanda = MetodosSQL.GetDT(sql);

            if (dtDemanda.Rows.Count > 0)
            {
                for (int i = 0; i < dtDemanda.Rows.Count; i++)
                {
                    quantidade += BuscarQuantidadeTempoDemanda(Convert.ToDateTime(dtDemanda.Rows[i]["HORAS"]));
                }

                valorTotal = quantidade * valorHora;
            }

            return valorTotal;
        }

        public void EnviarEmail(bool primeiroEnvio, DataRow row)
        {
            DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery("SELECT EMAILHOST, EMAILPORTA, EMAILUSUARIO, EMAILSENHA, EMAILUSASSL, EMAILAUTENTICA, EMAILREMETENTE FROM VPARAMETROS", new Object[] { });
            if (dt.Rows.Count > 0)
            {
                String ENDERECOSMTP = dt.Rows[0]["EMAILHOST"].ToString();
                int PORTA = int.Parse(dt.Rows[0]["EMAILPORTA"].ToString());
                String USUARIO = dt.Rows[0]["EMAILUSUARIO"].ToString();
                String SENHA = dt.Rows[0]["EMAILSENHA"].ToString();

                int IDAPONTAMENTO = int.Parse(row["AAPONTAMENTO.IDAPONTAMENTO"].ToString());

                String consultaIDSTATUSAPONTAMENTO = "SELECT IDSTATUSAPONTAMENTO FROM AAPONTAMENTO WHERE IDAPONTAMENTO = ?";
                int IDSTATUSAPONTAMENTO = int.Parse(AppLib.Context.poolConnection.Get().ExecGetField(-1, consultaIDSTATUSAPONTAMENTO, new Object[] { IDAPONTAMENTO }).ToString());

                String consultaTIPOFATURAMENTO = @"
SELECT APROJETOTAREFA.TIPOFATURAMENTO
FROM AAPONTAMENTO, AAPONTAMENTOTAREFA, APROJETOTAREFA
WHERE AAPONTAMENTO.IDAPONTAMENTO = AAPONTAMENTOTAREFA.IDAPONTAMENTO
  AND AAPONTAMENTOTAREFA.IDTAREFA = APROJETOTAREFA.IDTAREFA
  AND AAPONTAMENTO.IDAPONTAMENTO = ?";

                String FLAG_TIPOFATURAMENTO = AppLib.Context.poolConnection.Get().ExecGetField(String.Empty, consultaTIPOFATURAMENTO, new Object[] { IDAPONTAMENTO }).ToString();

                if (IDSTATUSAPONTAMENTO > 0)
                {
                    if (ValidarApontamento(IDAPONTAMENTO))
                    {
                        #region EXECUTA CONSULTA SQL

                        String Consulta2 = @"SELECT 
	AAPONTAMENTO.IDAPONTAMENTO,

	(CASE 
		WHEN 
			AAPONTAMENTO.IDUNIDADE = 38 
			AND 
			( SELECT NOME FROM GUSUARIO WHERE GUSUARIO.CODUSUARIO = AAPONTAMENTO.CODUSUARIO) = 'Fabio Coquieri'
	THEN
			( SELECT APELIDO FROM GUSUARIO WHERE GUSUARIO.CODUSUARIO = AAPONTAMENTO.CODUSUARIO)
	ELSE
		( SELECT NOME FROM GUSUARIO WHERE GUSUARIO.CODUSUARIO = AAPONTAMENTO.CODUSUARIO) END) as 'CONSULTOR',

	AAPONTAMENTO.IDUNIDADE,

	( SELECT NOME FROM AUNIDADE WHERE AUNIDADE.CODEMPRESA = AAPONTAMENTO.CODEMPRESA AND AUNIDADE.IDUNIDADE = AAPONTAMENTO.IDUNIDADE ) CLIENTE,
	
	AAPONTAMENTO.DATA,
	AAPONTAMENTO.INICIO,
	AAPONTAMENTO.TERMINO,
	AAPONTAMENTO.ABONO,
	
	(CASE AAPONTAMENTO.TIPOFATURAMENTO WHEN 'D' THEN 'DIREITO' ELSE 'PARCEIRO' END ) MODELOFAT, 
	DATEADD(MI, DATEDIFF(MI, AAPONTAMENTO.INICIO, AAPONTAMENTO.TERMINO) - DATEDIFF(MI, '1900-01-01', ISNULL(AAPONTAMENTO.ABONO,'1900-01-01')), '1900-01-01') TOTAL,
	CASE WHEN ( AAPONTAMENTO.INLOCO = 1 ) THEN 'SIM' ELSE 'NÃO' END INLOCO,
	
	( SELECT CASE WHEN (APROJETOTAREFA.TIPOFATURAMENTO = 'P') THEN 'PROJETO (CONFORME PROPOSTA)' ELSE 'DEMANDA (CONFORME OS)' END FROM APROJETOTAREFA WHERE APROJETOTAREFA.CODEMPRESA = AAPONTAMENTOTAREFA.CODEMPRESA AND IDTAREFA = AAPONTAMENTOTAREFA.IDTAREFA ) TIPOFATURAMENTO,
    AAPONTAMENTO.IDPROJETO,
	( SELECT DESCRICAO FROM APROJETO WHERE APROJETO.CODEMPRESA = AAPONTAMENTO.CODEMPRESA AND APROJETO.IDPROJETO = AAPONTAMENTO.IDPROJETO ) PROJETO,
	( SELECT NOMETAREFA FROM APROJETOTAREFA WHERE APROJETOTAREFA.CODEMPRESA = AAPONTAMENTO.CODEMPRESA AND APROJETOTAREFA.IDTAREFA = AAPONTAMENTOTAREFA.IDTAREFA ) TAREFA,
	
	AAPONTAMENTOTAREFA.OBSERVACAO,

	( SELECT EMAIL FROM GUSUARIO WHERE GUSUARIO.CODUSUARIO = AAPONTAMENTO.CODUSUARIO ) EMAIL_USUARIO, 

	( SELECT GUSUARIO.NOME
	FROM APROJETO, GUSUARIO
	WHERE APROJETO.CODEMPRESA = AAPONTAMENTO.CODEMPRESA
	  AND APROJETO.CODUSUARIOPRESTADOR = GUSUARIO.CODUSUARIO
	  AND APROJETO.IDPROJETO = AAPONTAMENTO.IDPROJETO ) CORDENADOR_PRESTADOR, 

	( SELECT GUSUARIO.EMAIL
	FROM APROJETO, GUSUARIO
	WHERE APROJETO.CODEMPRESA = AAPONTAMENTO.CODEMPRESA
	  AND APROJETO.CODUSUARIOPRESTADOR = GUSUARIO.CODUSUARIO
	  AND APROJETO.IDPROJETO = AAPONTAMENTO.IDPROJETO ) EMAIL_PRESTADOR, 


	( SELECT GUSUARIO.NOME
	FROM APROJETO, GUSUARIO
	WHERE APROJETO.CODEMPRESA = AAPONTAMENTO.CODEMPRESA
	  AND APROJETO.CODUSUARIOCLIENTE = GUSUARIO.CODUSUARIO
	  AND APROJETO.IDPROJETO = AAPONTAMENTO.IDPROJETO ) CORDENADOR_CLIENTE,

	( SELECT GUSUARIO.EMAIL
	FROM APROJETO, GUSUARIO
	WHERE APROJETO.CODEMPRESA = AAPONTAMENTO.CODEMPRESA
	  AND APROJETO.CODUSUARIOCLIENTE = GUSUARIO.CODUSUARIO
	  AND APROJETO.IDPROJETO = AAPONTAMENTO.IDPROJETO ) EMAIL_CLIENTE,

	DATAENVIO,
	DATARETORNO

FROM 
	AAPONTAMENTO
	
	INNER JOIN AAPONTAMENTOTAREFA ON AAPONTAMENTO.CODEMPRESA = AAPONTAMENTOTAREFA.CODEMPRESA AND AAPONTAMENTO.IDAPONTAMENTO = AAPONTAMENTOTAREFA.IDAPONTAMENTO
	INNER JOIN APROJETO ON AAPONTAMENTO.CODEMPRESA = APROJETO.CODEMPRESA AND AAPONTAMENTO.IDPROJETO = APROJETO.IDPROJETO

WHERE 
	AAPONTAMENTO.CODEMPRESA = ?
AND	AAPONTAMENTO.IDAPONTAMENTO = ?";

                        #endregion

                        DataTable dt2 = AppLib.Context.poolConnection.Get().ExecQuery(Consulta2, new Object[] { AppLib.Context.Empresa, IDAPONTAMENTO });

                        AppLib.Util.Email email = new AppLib.Util.Email();
                        email.Host = ENDERECOSMTP;
                        email.Porta = PORTA;
                        email.Usuario = USUARIO;
                        email.Senha = SENHA;

                        #region MENSAGEM ANTES

                        String MENSAGEM_ANTES = @"
<!DOCTYPE html>
<html>
<head>
  <title>Ordem de Serviço - CST SERVICES</title>
  <style type=""text/css"">
  body
  {
    font-family: monospace, serif, arial, times;
    color: black;
    background-color: #FFFFFF;
  }
  table
  {
    border-color:#F9F9F9;
  }
  </style>
</head>
<body>
  <table align=""center"" border=""1"" style=""width:90%"" cellspacing=""0"" cellpadding=""12"">
    <tr>
      <td bgcolor=""#F1F1F1""><font face=""Verdana"" size=""4""><b>ORDEM DE SERVIÇO ([IDAPONTAMENTO])</b></font></td>
    </tr>
    <tr>
      <td>
        <table>
          <tr>
            <td style=""width:100px;""><b>Consultor:</b></td>
            <td>[CONSULTOR]</td>
          </tr>
          <tr>
            <td style=""width:100px;""><b>Unidade:</b></td>
            <td>[CLIENTE]</td>
          </tr>
          <tr>
            <td><b>Data:</b></td>
            <td>[DATA]</td>
          </tr>";

                        if (!FLAG_TIPOFATURAMENTO.Equals("Z"))
                        {
                            MENSAGEM_ANTES += @"
          <tr>
            <td><b>Período:</b></td>
            <td>[INICIO] até [TERMINO] Abono [ABONO] Total = [TOTAL]</td>
          </tr>";
                        }

                        MENSAGEM_ANTES += @"
          <tr>
            <td><b>In loco:</b></td>
            <td>[INLOCO]</td>
          </tr>
          <tr>
            <td><b>[TIPO]:</b></td>
            <td>[PROJETO]</td>
          </tr>
          <tr>
            <td><b>Coordenador:</b></td>
            <td>[COORDENADOR]</td>
          </tr>
          <tr>
            <td><b>Tipo Faturamento:</b></td>
            <td>[TIPOFATURAMENTO]</td>
          </tr>
        </table>
      </td>
    </tr>";

                        #endregion

                        String MENSAGEM_MEIO = "";

                        for (int x = 0; x < dt2.Rows.Count; x++)
                        {
                            #region SETA OS CAMPOS

                            String CONSULTOR = "";
                            CONSULTOR = dt2.Rows[x]["CONSULTOR"].ToString();

                            String CLIENTE = dt2.Rows[x]["CLIENTE"].ToString();
                            DateTime DATA = DateTime.Parse(dt2.Rows[x]["DATA"].ToString());
                            DateTime INICIO = DateTime.Parse(dt2.Rows[x]["INICIO"].ToString());
                            DateTime TERMINO = DateTime.Parse(dt2.Rows[x]["TERMINO"].ToString());

                            DateTime ABONO = new DateTime(1900, 1, 1, 0, 0, 1);
                            if (dt2.Rows[x]["ABONO"] != DBNull.Value)
                            {
                                ABONO = DateTime.Parse(dt2.Rows[x]["ABONO"].ToString());
                            }

                            DateTime TOTAL = DateTime.Parse(dt2.Rows[x]["TOTAL"].ToString());
                            String INLOCO = dt2.Rows[x]["INLOCO"].ToString();
                            String TIPO = "Projeto";
                            String TIPOFATURAMENTO = dt2.Rows[x]["TIPOFATURAMENTO"].ToString();
                            String MODELOFAT = dt2.Rows[x]["MODELOFAT"].ToString();
                            String PROJETO = dt2.Rows[x]["PROJETO"].ToString();

                            String TAREFA = dt2.Rows[x]["TAREFA"].ToString();

                            RichTextBox richTextBoxTemp = new RichTextBox();
                            richTextBoxTemp.Text = dt2.Rows[x]["OBSERVACAO"].ToString();

                            String OBSERVACAO = "";

                            for (int iLinha = 0; iLinha < richTextBoxTemp.Lines.Length; iLinha++)
                            {
                                OBSERVACAO += richTextBoxTemp.Lines[iLinha] += "<BR>";
                            }

                            String EMAIL_USUARIO = dt2.Rows[0]["EMAIL_CLIENTE"].ToString();

                            String CC = dt2.Rows[0]["EMAIL_PRESTADOR"].ToString();

                            String COORDENADOR = dt2.Rows[0]["CORDENADOR_PRESTADOR"].ToString();
                            String CCO = dt2.Rows[0]["EMAIL_USUARIO"].ToString();

                            string emailsAdicionais = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"SELECT EMAIL FROM APROJETOEMAIL WHERE CODCOLIGADA = ? AND CODFILIAL = ? AND IDPROJETO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, dt2.Rows[0]["IDPROJETO"].ToString() }).ToString();

                            #endregion

                            if (x == 0)
                            {
                                email.De = USUARIO;
                                email.DeDisplay = "Workflow";
                                email.Para = EMAIL_USUARIO;

                                if (CC != string.Empty)
                                {
                                    email.CC = CC;
                                }

                                if (!string.IsNullOrEmpty(emailsAdicionais))
                                {
                                    email.CC = email.CC + ", " + emailsAdicionais;
                                }

                                email.CCO = CCO;

                                email.Assunto = "O.S " + AppLib.Util.Format.CompletarZeroEsquerda(5, IDAPONTAMENTO.ToString()) + " de " + DATA.ToShortDateString() + " Projeto - " + PROJETO.ToUpper() + " - " + CLIENTE.ToUpper() + ".";

                                string ComplementoFaturamento = " - " + MODELOFAT + "";

                                MENSAGEM_ANTES = MENSAGEM_ANTES.Replace("[IDAPONTAMENTO]", IDAPONTAMENTO.ToString());
                                MENSAGEM_ANTES = MENSAGEM_ANTES.Replace("[CONSULTOR]", CONSULTOR);
                                MENSAGEM_ANTES = MENSAGEM_ANTES.Replace("[CLIENTE]", CLIENTE);
                                MENSAGEM_ANTES = MENSAGEM_ANTES.Replace("[DATA]", DATA.ToShortDateString());
                                MENSAGEM_ANTES = MENSAGEM_ANTES.Replace("[INICIO]", INICIO.ToShortTimeString());
                                MENSAGEM_ANTES = MENSAGEM_ANTES.Replace("[TERMINO]", TERMINO.ToShortTimeString());
                                MENSAGEM_ANTES = MENSAGEM_ANTES.Replace("[ABONO]", ABONO.ToShortTimeString());
                                MENSAGEM_ANTES = MENSAGEM_ANTES.Replace("[TOTAL]", TOTAL.ToShortTimeString());
                                MENSAGEM_ANTES = MENSAGEM_ANTES.Replace("[INLOCO]", INLOCO);
                                MENSAGEM_ANTES = MENSAGEM_ANTES.Replace("[TIPO]", TIPO);
                                MENSAGEM_ANTES = MENSAGEM_ANTES.Replace("[COORDENADOR]", COORDENADOR);
                                MENSAGEM_ANTES = MENSAGEM_ANTES.Replace("[TIPOFATURAMENTO]", TIPOFATURAMENTO + ComplementoFaturamento);
                                MENSAGEM_ANTES = MENSAGEM_ANTES.Replace("[PROJETO]", PROJETO);
                            }

                            #region MENSAGEM DO E-MAIL (HTML PADRÃO)

                            email.Mensagem = @"
<!DOCTYPE html>
<html>
<head>
  <title>Ordem de Serviço - CST SERVICES</title>
  <style type=""text/css"">
  body
  {
    font-family: monospace, serif, arial, times;
    color: black;
    background-color: #FFFFFF;
  }
  table
  {
    border-color:#F9F9F9;
  }
  </style>
</head>
<body>
  <table align=""center"" border=""1"" style=""width:90%"" cellspacing=""0"" cellpadding=""12"">
    <tr>
      <td bgcolor=""#F1F1F1""><font face=""Verdana"" size=""4""><b>ORDEM DE SERVIÇO ([IDAPONTAMENTO])</b></font></td>
    </tr>
    <tr>
      <td>
        <table>
          <tr>
            <td style=""width:100px;""><b>Consultor:</b></td>
            <td>[CONSULTOR]</td>
          </tr>
          <tr>
            <td style=""width:100px;""><b>Unidade:</b></td>
            <td>[CLIENTE]</td>
          </tr>
          <tr>
            <td><b>Data:</b></td>
            <td>[DATA]</td>
          </tr>";
                            if (!FLAG_TIPOFATURAMENTO.Equals("Z"))
                            {
                                email.Mensagem += @"
          <tr>
            <td><b>Período:</b></td>
            <td>[INICIO] até [TERMINO] (abono: [ABONO])</td>
          </tr>";
                            }

                            email.Mensagem += @"
          <tr>
            <td><b>In loco:</b></td>
            <td>[INLOCO]</td>
          </tr>
          <tr>
            <td><b>[TIPO]:</b></td>
            <td>[PROJETO]</td>
          </tr>
          <tr>
            <td><b>Tipo Faturamento:</b></td>
            <td>[TIPOFATURAMENTO]</td>
          </tr>
        </table>
      </td>
    </tr>
    <tr>
      <td>
        <table>
        <tr>
          <td><b>Tarefa:</b></td>
          <td>[TAREFA]</td>
        </tr>
        <tr>
            <td valign=""top"" style=""width:100px;""><b>Detalhes:</b></td>
            <td>[OBSERVACAO]</td>
          </tr>
      </table>
      </td>
    </tr>
    <tr>
      <td>
        <table>
        <tr>
          <td><b>Tarefa:</b></td>
          <td>[TAREFA]</td>
        </tr>
        <tr>
            <td valign=""top"" style=""width:100px;""><b>Detalhes:</b></td>
            <td>[OBSERVACAO]</td>
          </tr>
      </table>
      </td>
    </tr>
  </table>
  <br>
  <font face=""Verdana"" size=""2"">
  <br>
  <table align=""center"">
    <tr>
      <td><b><center>Atenção: Aprovação automática, caso necessário responda este email com seus comentários para reprova-la.</center></b></td>
    </tr>
    <tr>
      <td>
        <hr>
      </td>
    </tr>
  </table>
  <br>
  <br>
 </font>
  </body>
</html>";

                            #endregion

                            #region MENSAGEM LOOP

                            String MENSAGEM_LOOP = @"
<tr>
<td>
<table>
<tr>
    <td><b>Tarefa:</b></td>
    <td>[TAREFA]</td>
</tr>
<tr>
    <td valign=""top"" style=""width:100px;""><b>Detalhes:</b></td>
    <td>[OBSERVACAO]</td>
    </tr>
</table>
</td>
</tr>
";

                            #endregion

                            MENSAGEM_LOOP = MENSAGEM_LOOP.Replace("[TAREFA]", TAREFA);
                            MENSAGEM_LOOP = MENSAGEM_LOOP.Replace("[OBSERVACAO]", OBSERVACAO);

                            MENSAGEM_MEIO += MENSAGEM_LOOP;
                        }

                        #region MENSAGEM DEPOIS

                        String MENSAGEM_DEPOIS = @"
    </table>
  <br>
  <font face=""Verdana"" size=""2"">
  <br>
  <table align=""center"">
    <tr>
      <td><b><center>Atenção: Aprovação automática, caso necessário responda este email com seus comentários para reprova-la.</center></b></td>
    </tr>
    <tr>
      <td>
        <hr>
      </td>
    </tr>
  </table>
  <br>
  <br>
 </font>
  </body>
</html>";

                        #endregion

                        String IDEMBARALHADO = new Criptografia1().Encoder(IDAPONTAMENTO);

                        email.Mensagem = MENSAGEM_ANTES + MENSAGEM_MEIO + MENSAGEM_DEPOIS;

                        Boolean enviou = false;

                        try
                        {
                            #region VALIDAÇÕES DO E-MAIL

                            if (email.Para.Equals(""))
                            {
                                throw new Exception("O contato selecionado não possui e-mail no seu cadastrado.");
                            }

                            #endregion

                            email.Timeout = int.MaxValue;

                            if (ValidarEmail(email.Para))
                            {
                                enviou = email.Enviar();
                            }
                            else
                            {
                                MessageBox.Show("Email de envio invalido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (enviou)
                            {
                                if (primeiroEnvio)
                                {
                                    String comando1 = "UPDATE AAPONTAMENTO SET DATAENVIO = GETDATE(), IDSTATUSAPONTAMENTO = 1, MOTIVOREPROVACAO = NULL WHERE IDAPONTAMENTO = ?";
                                    int iEnvio = AppLib.Context.poolConnection.Get().ExecTransaction(comando1, new Object[] { IDAPONTAMENTO });

                                    if (iEnvio == 1)
                                    {
                                        MensagemFinalEmail += "Apontamento " + IDAPONTAMENTO + " enviado para " + email.Para + "\r\n";
                                    }
                                    else
                                    {
                                        AppLib.Windows.FormMessageDefault.ShowError("O e-mail foi enviado com sucesso, mas não foi possível atualizar o status e data de envio. Não envie novamente para o cliente! Faça a correção da rotina interna do sistema.");
                                    }
                                }
                                else
                                {
                                    MensagemFinalEmail += "Apontamento " + IDAPONTAMENTO + " enviado para " + email.Para + "\r\n";
                                }
                            }
                            else
                            {
                                AppLib.Windows.FormMessageDefault.ShowError("Erro ao enviar e-mail de Ordem de Serviço.");
                            }
                        }
                        catch (Exception ex)
                        {
                            AppLib.Windows.FormMessageDefault.ShowError(ex.Message);
                        }
                    }
                }
                else
                {
                    AppLib.Windows.FormMessageDefault.ShowError("Ref apontamento " + IDAPONTAMENTO + ", não é possivel enviar e-mail com o status atual do apontamento.");
                }
            }
            else
            {
                AppLib.Windows.FormMessageDefault.ShowError("Erro ao carregar parâmetros de e-mail");
            }
        }
        
        public void ReenviarEmail(int idApontamento, int index, string parametroEmail)
        {
            try
            {
                AppLib.Util.Email email = new AppLib.Util.Email();

                DataTable dtParametrosEmail = new DataTable();
                DataTable dtConfiguracoesEmail = new DataTable();

                string emailHost = "";
                int emailPorta = 0;
                string emailUsuario = "";
                string emailSenha = "";

                string tipoFaturamento = "";

                bool enviou = false;

                // Configura os parâmetros do e-mail
                dtParametrosEmail = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EMAILHOST, EMAILPORTA, EMAILUSUARIO, EMAILSENHA, EMAILUSASSL, EMAILAUTENTICA, EMAILREMETENTE FROM VPARAMETROS", new object[] { });

                emailHost = dtParametrosEmail.Rows[0]["EMAILHOST"].ToString();
                emailPorta = Convert.ToInt32(dtParametrosEmail.Rows[0]["EMAILPORTA"]);
                emailUsuario = dtParametrosEmail.Rows[0]["EMAILUSUARIO"].ToString();
                emailSenha = dtParametrosEmail.Rows[0]["EMAILSENHA"].ToString();

                // Obtém o Tipo de Faturamento
                tipoFaturamento = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"SELECT APROJETOTAREFA.TIPOFATURAMENTO FROM AAPONTAMENTO, AAPONTAMENTOTAREFA, APROJETOTAREFA WHERE AAPONTAMENTO.IDAPONTAMENTO = AAPONTAMENTOTAREFA.IDAPONTAMENTO AND AAPONTAMENTOTAREFA.IDTAREFA = APROJETOTAREFA.IDTAREFA AND AAPONTAMENTO.IDAPONTAMENTO = ?", new object[] { idApontamento }).ToString();

                if (ValidarApontamento(idApontamento))
                {
                    dtConfiguracoesEmail = CarregaParametrosEmail(idApontamento);

                    email.Host = emailHost;
                    email.Porta = emailPorta;
                    email.Usuario = emailUsuario;
                    email.Senha = emailSenha;

                    email.Mensagem = FormatarMensagemEmail(idApontamento, tipoFaturamento, dtConfiguracoesEmail, email, parametroEmail);
                    email.Timeout = int.MaxValue;

                    mensagem.ParametroEmail.Insert(index, parametroEmail);

                    if (ValidarEmail(email.Para))
                    {
                        enviou = email.Enviar();

                        mensagem.ApontamentosComSucesso.Insert(index, idApontamento.ToString());
                    }
                    else
                    {
                        return;
                    }

                    if (!enviou)
                    {
                        XtraMessageBox.Show("Erro ao enviar email para o Apontamento: " + idApontamento, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Erro ao enviar e-mail de Ordem de Serviço.\r\nDetalhes:" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private bool ValidarApontamento(int IDAPONTAMENTO)
        {
            int horasApontamento = 0;
            int minutosApontamento = 0;

            try
            {
                DataTable dtApontamento = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT A.DATA, A.INICIO, A.TERMINO, A.ABONO, T.HORAS
                                                                                                    FROM AAPONTAMENTO A
                                                                                                    INNER JOIN AAPONTAMENTOTAREFA T
                                                                                                    ON T.CODEMPRESA = A.CODEMPRESA AND T.IDAPONTAMENTO = A.IDAPONTAMENTO
                                                                                                    WHERE A.CODEMPRESA = ? AND A.IDAPONTAMENTO = ?", new object[] { AppLib.Context.Empresa, IDAPONTAMENTO });

                if (dtApontamento.Rows.Count > 0)
                {
                    DateTime dataApontamento = Convert.ToDateTime(dtApontamento.Rows[0]["DATA"]);

                    DateTime dataInicio = Convert.ToDateTime(dtApontamento.Rows[0]["INICIO"]);
                    DateTime dataTermino = Convert.ToDateTime(dtApontamento.Rows[0]["TERMINO"]);

                    DateTime? dataCompleta = null;

                    DateTime? tempoApontamento = null;

                    TimeSpan tsDiferencaTempoMinutos;
                    TimeSpan tsDiferencaTempoHoras;

                    for (int i = 0; i < dtApontamento.Rows.Count; i++)
                    {
                        if (dtApontamento.Rows[i]["HORAS"] != DBNull.Value)
                        {
                            tempoApontamento = Convert.ToDateTime(dtApontamento.Rows[i]["HORAS"]);

                            if (tempoApontamento.Value.Minute > 0)
                            {
                                minutosApontamento += tempoApontamento.Value.Minute;
                            }

                            if (tempoApontamento.Value.Hour > 0)
                            {
                                horasApontamento += tempoApontamento.Value.Hour;
                            }
                        }
                    }

                    if (minutosApontamento > 0)
                    {
                        dataCompleta = new DateTime(dataApontamento.Year, dataApontamento.Month, dataApontamento.Day, horasApontamento, minutosApontamento, 0);
                    }
                    else
                    {
                        dataCompleta = new DateTime(dataApontamento.Year, dataApontamento.Month, dataApontamento.Day, horasApontamento, 0, 0);
                    }

                    tsDiferencaTempoMinutos = TimeSpan.FromMinutes(dataCompleta.Value.Minute);

                    tsDiferencaTempoHoras = TimeSpan.FromHours(dataCompleta.Value.Hour);

                    if ((dataCompleta.Value.Minute - tsDiferencaTempoMinutos.Minutes) > 0 || (dataCompleta.Value.Hour - tsDiferencaTempoHoras.Hours) > 0)
                    {
                        XtraMessageBox.Show("ATENÇÃO\r\nApontamento " + IDAPONTAMENTO + " com divergência de horas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (tsDiferencaTempoMinutos.Minutes == 0 && tsDiferencaTempoHoras.Hours == 0)
                    {
                        XtraMessageBox.Show("ATENÇÃO\r\nApontamento " + IDAPONTAMENTO + " com horas zeradas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    XtraMessageBox.Show("ATENÇÃO\r\nApontamento " + IDAPONTAMENTO + " não existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("ATENÇÃO\r\nErro na validação do Apontamento " + IDAPONTAMENTO + ".\r\n" + ex.Message);
                return false;
            }

            return true;
        }

        private bool ValidarEmail(string email)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(email))
                {
                    Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

                    if (rg.IsMatch(email))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private void AtualizarStatusApontamento(string idApontamento, int status)
        {
            try
            {
                sql = String.Format(@"UPDATE AAPONTAMENTO SET IDSTATUSAPONTAMENTO = '" + status + "' WHERE IDAPONTAMENTO = '{0}'", idApontamento);

                MetodosSQL.ExecQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable CarregaParametrosEmail(int idApontamento)
        {
            string query = @"SELECT 
                        	AAPONTAMENTO.IDAPONTAMENTO,
                        
                        	(CASE 
                        		WHEN 
                        			AAPONTAMENTO.IDUNIDADE = 38 
                        			AND 
                        			( SELECT NOME FROM GUSUARIO WHERE GUSUARIO.CODUSUARIO = AAPONTAMENTO.CODUSUARIO) = 'Fabio Coquieri'
                        	THEN
                        			( SELECT APELIDO FROM GUSUARIO WHERE GUSUARIO.CODUSUARIO = AAPONTAMENTO.CODUSUARIO)
                        	ELSE
                        		( SELECT NOME FROM GUSUARIO WHERE GUSUARIO.CODUSUARIO = AAPONTAMENTO.CODUSUARIO) END) as 'CONSULTOR',
                        
                        	AAPONTAMENTO.IDUNIDADE,
                        
                        	( SELECT NOME FROM AUNIDADE WHERE AUNIDADE.CODEMPRESA = AAPONTAMENTO.CODEMPRESA AND AUNIDADE.IDUNIDADE = AAPONTAMENTO.IDUNIDADE ) CLIENTE,
                        	
                        	AAPONTAMENTO.DATA,
                        	AAPONTAMENTO.INICIO,
                        	AAPONTAMENTO.TERMINO,
                        	AAPONTAMENTO.ABONO,
                        	
                        	(CASE AAPONTAMENTO.TIPOFATURAMENTO WHEN 'D' THEN 'DIREITO' ELSE 'PARCEIRO' END ) MODELOFAT, 
                        	DATEADD(MI, DATEDIFF(MI, AAPONTAMENTO.INICIO, AAPONTAMENTO.TERMINO) - DATEDIFF(MI, '1900-01-01', ISNULL(AAPONTAMENTO.ABONO,'1900-01-01')), '1900-01-01') TOTAL,
                        	CASE WHEN ( AAPONTAMENTO.INLOCO = 1 ) THEN 'SIM' ELSE 'NÃO' END INLOCO,
                        	
                        	( SELECT CASE WHEN (APROJETOTAREFA.TIPOFATURAMENTO = 'P') THEN 'PROJETO (CONFORME PROPOSTA)' ELSE 'DEMANDA (CONFORME OS)' END FROM APROJETOTAREFA WHERE APROJETOTAREFA.CODEMPRESA = AAPONTAMENTOTAREFA.CODEMPRESA AND IDTAREFA = AAPONTAMENTOTAREFA.IDTAREFA ) TIPOFATURAMENTO,
                            AAPONTAMENTO.IDPROJETO,
                        	( SELECT DESCRICAO FROM APROJETO WHERE APROJETO.CODEMPRESA = AAPONTAMENTO.CODEMPRESA AND APROJETO.IDPROJETO = AAPONTAMENTO.IDPROJETO ) PROJETO,
                        	( SELECT NOMETAREFA FROM APROJETOTAREFA WHERE APROJETOTAREFA.CODEMPRESA = AAPONTAMENTO.CODEMPRESA AND APROJETOTAREFA.IDTAREFA = AAPONTAMENTOTAREFA.IDTAREFA ) TAREFA,
                        	
                        	AAPONTAMENTOTAREFA.OBSERVACAO,
                        
                        	( SELECT EMAIL FROM GUSUARIO WHERE GUSUARIO.CODUSUARIO = AAPONTAMENTO.CODUSUARIO ) EMAIL_USUARIO, 
                        
                        	( SELECT GUSUARIO.NOME
                        	FROM APROJETO, GUSUARIO
                        	WHERE APROJETO.CODEMPRESA = AAPONTAMENTO.CODEMPRESA
                        	  AND APROJETO.CODUSUARIOPRESTADOR = GUSUARIO.CODUSUARIO
                        	  AND APROJETO.IDPROJETO = AAPONTAMENTO.IDPROJETO ) CORDENADOR_PRESTADOR, 
                        
                        	( SELECT GUSUARIO.EMAIL
                        	FROM APROJETO, GUSUARIO
                        	WHERE APROJETO.CODEMPRESA = AAPONTAMENTO.CODEMPRESA
                        	  AND APROJETO.CODUSUARIOPRESTADOR = GUSUARIO.CODUSUARIO
                        	  AND APROJETO.IDPROJETO = AAPONTAMENTO.IDPROJETO ) EMAIL_PRESTADOR, 
                        
                        
                        	( SELECT GUSUARIO.NOME
                        	FROM APROJETO, GUSUARIO
                        	WHERE APROJETO.CODEMPRESA = AAPONTAMENTO.CODEMPRESA
                        	  AND APROJETO.CODUSUARIOCLIENTE = GUSUARIO.CODUSUARIO
                        	  AND APROJETO.IDPROJETO = AAPONTAMENTO.IDPROJETO ) CORDENADOR_CLIENTE,
                        
                        	( SELECT GUSUARIO.EMAIL
                        	FROM APROJETO, GUSUARIO
                        	WHERE APROJETO.CODEMPRESA = AAPONTAMENTO.CODEMPRESA
                        	  AND APROJETO.CODUSUARIOCLIENTE = GUSUARIO.CODUSUARIO
                        	  AND APROJETO.IDPROJETO = AAPONTAMENTO.IDPROJETO ) EMAIL_CLIENTE,
                        
                        	DATAENVIO,
                        	DATARETORNO

                            FROM 
	                        AAPONTAMENTO
	
	                        INNER JOIN AAPONTAMENTOTAREFA ON AAPONTAMENTO.CODEMPRESA = AAPONTAMENTOTAREFA.CODEMPRESA AND AAPONTAMENTO.IDAPONTAMENTO = AAPONTAMENTOTAREFA.IDAPONTAMENTO
	                        INNER JOIN APROJETO ON AAPONTAMENTO.CODEMPRESA = APROJETO.CODEMPRESA AND AAPONTAMENTO.IDPROJETO = APROJETO.IDPROJETO

                            WHERE 
	                        AAPONTAMENTO.CODEMPRESA = ?
                            AND	AAPONTAMENTO.IDAPONTAMENTO = ?";

            DataTable dtParametrosEmail = AppLib.Context.poolConnection.Get("Start").ExecQuery(query, new object[] { AppLib.Context.Empresa, idApontamento });

            return dtParametrosEmail;
        }

        private string FormatarMensagemEmail(int idApontamento, string tipoFaturamento, DataTable dtConfiguracoesEmail, AppLib.Util.Email email, string parametroEmail)
        {
            string mensagemPrimeiraFormatacao = "";
            string mensagemTarefas = "";
            string mensagemPosValores = "";
            string mensagemFinal = "";

            string consultor = "";
            string cliente = "";
            string inLoco = "";
            string tipo = "";
            string modeloFat = "";
            string projeto = "";
            string tarefa = "";
            string observacao = "";

            string emailCliente = "";
            string cc = "";
            string cco = "";
            string coordenador = "";
            string emailsAdicionais = "";

            DateTime data;
            DateTime inicio;
            DateTime termino;
            DateTime abono;
            DateTime total;

            RichTextBox rtb = new RichTextBox();

            mensagemPrimeiraFormatacao = @"<!DOCTYPE html>
                                            <html>
                                            <head>
                                              <title>Ordem de Serviço - CST SERVICES</title>
                                              <style type=""text/css"">
                                              body
                                              {
                                                font-family: monospace, serif, arial, times;
                                                color: black;
                                                background-color: #FFFFFF;
                                              }
                                              table
                                              {
                                                border-color:#F9F9F9;
                                              }
                                              </style>
                                            </head>
                                            <body>
                                              <table align=""center"" border=""1"" style=""width:90%"" cellspacing=""0"" cellpadding=""12"">
                                                <tr>
                                                  <td bgcolor=""#F1F1F1""><font face=""Verdana"" size=""4""><b>ORDEM DE SERVIÇO ([IDAPONTAMENTO])</b></font></td>
                                                </tr>
                                                <tr>
                                                  <td>
                                                    <table>
                                                      <tr>
                                                        <td style=""width:100px;""><b>Consultor:</b></td>
                                                        <td>[CONSULTOR]</td>
                                                      </tr>
                                                      <tr>
                                                        <td style=""width:100px;""><b>Unidade:</b></td>
                                                        <td>[CLIENTE]</td>
                                                      </tr>
                                                      <tr>
                                                        <td><b>Data:</b></td>
                                                        <td>[DATA]</td>
                                                      </tr>";

            if (tipoFaturamento != "Z")
            {
                mensagemPrimeiraFormatacao += @"
                                              <tr>
                                                <td><b>Período:</b></td>
                                                <td>[INICIO] até [TERMINO] Abono [ABONO] Total = [TOTAL]</td>
                                              </tr>";
            }

            mensagemPrimeiraFormatacao += @"
                                          <tr>
                                            <td><b>In loco:</b></td>
                                            <td>[INLOCO]</td>
                                          </tr>
                                          <tr>
                                            <td><b>[TIPO]:</b></td>
                                            <td>[PROJETO]</td>
                                          </tr>
                                          <tr>
                                            <td><b>Coordenador:</b></td>
                                            <td>[COORDENADOR]</td>
                                          </tr>
                                          <tr>
                                            <td><b>Tipo Faturamento:</b></td>
                                            <td>[TIPOFATURAMENTO]</td>
                                          </tr>
                                        </table>
                                      </td>
                                    </tr>";

            for (int i = 0; i < dtConfiguracoesEmail.Rows.Count; i++)
            {
                consultor = dtConfiguracoesEmail.Rows[i]["CONSULTOR"].ToString();
                cliente = dtConfiguracoesEmail.Rows[i]["CLIENTE"].ToString();

                tipo = "Projeto";
                inLoco = dtConfiguracoesEmail.Rows[i]["INLOCO"].ToString();
                modeloFat = dtConfiguracoesEmail.Rows[i]["MODELOFAT"].ToString();
                projeto = dtConfiguracoesEmail.Rows[i]["PROJETO"].ToString();
                tarefa = dtConfiguracoesEmail.Rows[i]["TAREFA"].ToString();

                data = Convert.ToDateTime(dtConfiguracoesEmail.Rows[i]["DATA"].ToString());
                inicio = Convert.ToDateTime(dtConfiguracoesEmail.Rows[i]["INICIO"].ToString());
                termino = Convert.ToDateTime(dtConfiguracoesEmail.Rows[i]["TERMINO"].ToString());

                if (dtConfiguracoesEmail.Rows[i]["ABONO"] != DBNull.Value)
                {
                    abono = Convert.ToDateTime(dtConfiguracoesEmail.Rows[i]["ABONO"].ToString());
                }
                else
                {
                    abono = new DateTime();
                }

                total = Convert.ToDateTime(dtConfiguracoesEmail.Rows[i]["TOTAL"].ToString());

                rtb.Text = dtConfiguracoesEmail.Rows[i]["OBSERVACAO"].ToString();

                for (int iLinha = 0; iLinha < rtb.Lines.Length; iLinha++)
                {
                    observacao += rtb.Lines[iLinha] += "<BR>";
                }

                coordenador = dtConfiguracoesEmail.Rows[0]["CORDENADOR_PRESTADOR"].ToString();

                emailCliente = dtConfiguracoesEmail.Rows[0]["EMAIL_CLIENTE"].ToString();
                cc = dtConfiguracoesEmail.Rows[0]["EMAIL_PRESTADOR"].ToString();
                cco = dtConfiguracoesEmail.Rows[0]["EMAIL_USUARIO"].ToString();
                emailsAdicionais = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"SELECT EMAIL FROM APROJETOEMAIL WHERE CODCOLIGADA = ? AND CODFILIAL = ? AND IDPROJETO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, dtConfiguracoesEmail.Rows[0]["IDPROJETO"].ToString() }).ToString();

                // Título da O.S
                if (i == 0)
                {
                    email.De = email.Usuario;
                    email.DeDisplay = "Workflow";

                    switch (parametroEmail)
                    {
                        case "Para":
                            email.Para = emailCliente;
                            break;
                        case "Emails Adicionais":

                            if (!string.IsNullOrEmpty(emailsAdicionais))
                            {
                                email.Para = emailsAdicionais;
                            }
                            else
                            {
                                mensagem.ApontamentosComErro.Add(idApontamento.ToString());
                                mensagem.Motivo.Add("Os e-mails adicionais não estão preenchidos.");
                            }

                            break;
                        case "CC":
                            email.Para = cc;
                            break;
                        case "CCo":
                            email.Para = cco;
                            break;
                        default:
                            break;
                    }

                    email.CC = null;
                    email.CCO = null;

                    email.Assunto = "O.S " + AppLib.Util.Format.CompletarZeroEsquerda(5, idApontamento.ToString()) + " de " + data.ToShortDateString() + " Projeto - " + projeto.ToUpper() + " - " + cliente.ToUpper() + ".";

                    string ComplementoFaturamento = " - " + modeloFat + "";

                    mensagemPrimeiraFormatacao = mensagemPrimeiraFormatacao.Replace("[IDAPONTAMENTO]", idApontamento.ToString());
                    mensagemPrimeiraFormatacao = mensagemPrimeiraFormatacao.Replace("[CONSULTOR]", consultor);
                    mensagemPrimeiraFormatacao = mensagemPrimeiraFormatacao.Replace("[CLIENTE]", cliente);
                    mensagemPrimeiraFormatacao = mensagemPrimeiraFormatacao.Replace("[DATA]", data.ToShortDateString());
                    mensagemPrimeiraFormatacao = mensagemPrimeiraFormatacao.Replace("[INICIO]", inicio.ToShortTimeString());
                    mensagemPrimeiraFormatacao = mensagemPrimeiraFormatacao.Replace("[TERMINO]", termino.ToShortTimeString());
                    mensagemPrimeiraFormatacao = mensagemPrimeiraFormatacao.Replace("[ABONO]", abono.ToShortTimeString());
                    mensagemPrimeiraFormatacao = mensagemPrimeiraFormatacao.Replace("[TOTAL]", total.ToShortTimeString());
                    mensagemPrimeiraFormatacao = mensagemPrimeiraFormatacao.Replace("[INLOCO]", inLoco);
                    mensagemPrimeiraFormatacao = mensagemPrimeiraFormatacao.Replace("[TIPO]", tipo);
                    mensagemPrimeiraFormatacao = mensagemPrimeiraFormatacao.Replace("[COORDENADOR]", coordenador);
                    mensagemPrimeiraFormatacao = mensagemPrimeiraFormatacao.Replace("[TIPOFATURAMENTO]", tipoFaturamento + ComplementoFaturamento);
                    mensagemPrimeiraFormatacao = mensagemPrimeiraFormatacao.Replace("[PROJETO]", projeto);
                }

                email.Mensagem = @"<!DOCTYPE html>
                                   <html>
                                   <head>
                                     <title>Ordem de Serviço - CST SERVICES</title>
                                     <style type=""text/css"">
                                     body
                                     {
                                       font-family: monospace, serif, arial, times;
                                       color: black;
                                       background-color: #FFFFFF;
                                     }
                                     table
                                     {
                                       border-color:#F9F9F9;
                                     }
                                     </style>
                                   </head>
                                   <body>
                                     <table align=""center"" border=""1"" style=""width:90%"" cellspacing=""0"" cellpadding=""12"">
                                       <tr>
                                         <td bgcolor=""#F1F1F1""><font face=""Verdana"" size=""4""><b>ORDEM DE SERVIÇO ([IDAPONTAMENTO])</b></font></td>
                                       </tr>
                                       <tr>
                                         <td>
                                           <table>
                                             <tr>
                                               <td style=""width:100px;""><b>Consultor:</b></td>
                                               <td>[CONSULTOR]</td>
                                             </tr>
                                             <tr>
                                               <td style=""width:100px;""><b>Unidade:</b></td>
                                               <td>[CLIENTE]</td>
                                             </tr>
                                             <tr>
                                               <td><b>Data:</b></td>
                                               <td>[DATA]</td>
                                             </tr>";

                if (tipoFaturamento != "Z")
                {
                    email.Mensagem += @"
                                      <tr>
                                        <td><b>Período:</b></td>
                                        <td>[INICIO] até [TERMINO] (abono: [ABONO])</td>
                                      </tr>";
                }

                email.Mensagem += @"
                                  <tr>
                                    <td><b>In loco:</b></td>
                                    <td>[INLOCO]</td>
                                  </tr>
                                  <tr>
                                    <td><b>[TIPO]:</b></td>
                                    <td>[PROJETO]</td>
                                  </tr>
                                  <tr>
                                    <td><b>Tipo Faturamento:</b></td>
                                    <td>[TIPOFATURAMENTO]</td>
                                  </tr>
                                </table>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <table>
                                <tr>
                                  <td><b>Tarefa:</b></td>
                                  <td>[TAREFA]</td>
                                </tr>
                                <tr>
                                    <td valign=""top"" style=""width:100px;""><b>Detalhes:</b></td>
                                    <td>[OBSERVACAO]</td>
                                  </tr>
                              </table>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <table>
                                <tr>
                                  <td><b>Tarefa:</b></td>
                                  <td>[TAREFA]</td>
                                </tr>
                                <tr>
                                    <td valign=""top"" style=""width:100px;""><b>Detalhes:</b></td>
                                    <td>[OBSERVACAO]</td>
                                  </tr>
                              </table>
                              </td>
                            </tr>
                          </table>
                          <br>
                          <font face=""Verdana"" size=""2"">
                          <br>
                          <table align=""center"">
                            <tr>
                              <td><b><center>Atenção: Aprovação automática, caso necessário responda este email com seus comentários para reprova-la.</center></b></td>
                            </tr>
                            <tr>
                              <td>
                                <hr>
                              </td>
                            </tr>
                          </table>
                          <br>
                          <br>
                         </font>
                          </body>
                        </html>";

                mensagemTarefas = @"
                                    <tr>
                                    <td>
                                    <table>
                                    <tr>
                                        <td><b>Tarefa:</b></td>
                                        <td>[TAREFA]</td>
                                    </tr>
                                    <tr>
                                        <td valign=""top"" style=""width:100px;""><b>Detalhes:</b></td>
                                        <td>[OBSERVACAO]</td>
                                        </tr>
                                    </table>
                                    </td>
                                    </tr>
                                    ";

                mensagemTarefas = mensagemTarefas.Replace("[TAREFA]", tarefa);
                mensagemTarefas = mensagemTarefas.Replace("[OBSERVACAO]", observacao);

                mensagemPosValores += mensagemTarefas;
            }

            mensagemFinal = @"
                            </table>
                          <br>
                          <font face=""Verdana"" size=""2"">
                          <br>
                          <table align=""center"">
                            <tr>
                              <td><b><center>Atenção: Aprovação automática, caso necessário responda este email com seus comentários para reprova-la.</center></b></td>
                            </tr>
                            <tr>
                              <td>
                                <hr>
                              </td>
                            </tr>
                          </table>
                          <br>
                          <br>
                         </font>
                          </body>
                        </html>";

            return string.Concat(mensagemPrimeiraFormatacao, mensagemPosValores, mensagemFinal);
        }

        private string FormatarDetalhesEmail()
        {
            string mensagemEmail = "";

            string apontamentosComSucessoFormatados = "";
            string apontamentosComErroFormatados = "";

            // Verifica os apontamentos que foram enviados.
            if (mensagem.ApontamentosComSucesso.Count > 0)
            {
                // Verifica se existe apenas um e-mail enviado.
                if (mensagem.ApontamentosComSucesso.Count > 1)
                {
                    for (int i = 0; i < mensagem.ApontamentosComSucesso.Count; i++)
                    {
                        if (i == (mensagem.ApontamentosComSucesso.Count - 1))
                        {
                            apontamentosComSucessoFormatados += mensagem.ApontamentosComSucesso[i].ToString();
                        }
                        else
                        {
                            apontamentosComSucessoFormatados += mensagem.ApontamentosComSucesso[i].ToString() + ", ";
                        }     
                    }
                }
                else
                {
                    apontamentosComSucessoFormatados = mensagem.ApontamentosComSucesso[0].ToString();
                }
            }

            mensagemEmail = "[Parâmetro: " + mensagem.ParametroEmail[0].ToString() + "]\r\n";
            mensagemEmail += "Apontamentos enviados: " + apontamentosComSucessoFormatados;

            // Verifica os apontamentos que não foram enviados.
            if (mensagem.ApontamentosComErro.Count > 0)
            {
                // Verifica se existe apenas um e-mail com erro.
                if (mensagem.ApontamentosComErro.Count > 1)
                {
                    mensagemEmail += "\r\n";

                    for (int i = 0; i < mensagem.ApontamentosComErro.Count; i++)
                    {
                        mensagemEmail = "[Parâmetro: " + mensagem.ParametroEmail[1].ToString() + "]\r\n";
                        mensagemEmail += "\r\nApontamento com erro: ";

                        if (i == (mensagem.ApontamentosComErro.Count - 1))
                        {
                            apontamentosComErroFormatados = mensagem.ApontamentosComErro[i].ToString();
                            apontamentosComErroFormatados += "\r\nMotivo: " + mensagem.Motivo[i].ToString();
                        }
                        else
                        {
                            apontamentosComErroFormatados = mensagem.ApontamentosComErro[i].ToString();
                            apontamentosComErroFormatados += "\r\nMotivo: " + mensagem.Motivo[i].ToString();
                        }

                        mensagemEmail += apontamentosComErroFormatados;
                    }
                }
                else
                {
                    mensagemEmail += "\r\nApontamento com erro: ";

                    apontamentosComErroFormatados = mensagem.ApontamentosComErro[0].ToString();
                    apontamentosComErroFormatados += "\r\nMotivo: " + mensagem.Motivo[0].ToString();
                }           
            }

            return mensagemEmail;
        }

        public string TratamentoMensagemReenvioEmail()
        {
            string cabecalho = "Reenvio de e-mail(s) realizado com sucesso!\r\n";
            string introducaoDetalhe = "\r\nResumo do processo:\r\n";
            string detalhe = FormatarDetalhesEmail();

            return string.Concat(cabecalho, introducaoDetalhe, detalhe);
        }

        #endregion

        #region Classe

        public class MensagemEmail
        {
            public List<string> ParametroEmail { get; set; }

            public List<string> ApontamentosComSucesso { get; set; }

            public List<string> ApontamentosComErro { get; set; }

            public List<string> Motivo { get; set; }
        }

        #endregion
    }
}
