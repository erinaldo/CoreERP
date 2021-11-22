using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.NotaFiscal
{
    public partial class frmInutilizacao : Form
    {
        string Token = string.Empty;
        public int Codoper;
        public int Codfilial;
        private string RetornoInut = string.Empty;
        private string NumeroFormatado = string.Empty;
        Class.NFeAPI NfeAPI = new Class.NFeAPI();

        public frmInutilizacao()
        {
            InitializeComponent();
        }

        private void frmInutilizacao_Load(object sender, EventArgs e)
        {
            tbModelo.Text = "55";
            tbModelo.ReadOnly = true;
            tbFaixaInicial.ReadOnly = true;
            cbSerie.Enabled = false;
        }

        private void lpFilial_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lpFilial.txtcodigo.Text))
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT DISTINCT(CODSERIE) 
                                                                                        FROM VTIPOPERSERIE 
                                                                                        WHERE CODTIPOPER IN	(SELECT CODTIPOPER 
					                                                                                            FROM GTIPOPER 
					                                                                                            WHERE USAOPERACAONFE = 1
					                                                                                            AND CODEMPRESA = ?)
                                                                                         AND CODEMPRESA = ?
                                                                                         AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Empresa, lpFilial.txtcodigo.Text });

                List<string> List = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    List.Add(row["CODSERIE"].ToString());
                }

                cbSerie.Enabled = true;
                cbSerie.DataSource = List;
            }
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            #region Código antigo comentado

            //if (ValidaFilial() == false)
            //{
            //    return;
            //}

            //if (ValidaSerie() == false)
            //{
            //    return;
            //}

            //if (ValidaDataInicial() == false)
            //{
            //    return;
            //}

            //try
            //{
            //    splashScreenManager1.ShowWaitForm();
            //    splashScreenManager1.SetWaitFormCaption("Consultando");

            //    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            //    List<REGISTROS> R = new List<REGISTROS>();

            //    DataTable dtFinal = conn.ExecQuery("SELECT NUMSEQ FROM VSERIE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODSERIE = ?", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text, cbSerie.SelectedValue });

            //    int NumeroIni = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT 
            //                                                                                             MIN(GOPER.NUMERO) NUMERO_INICIAL
            //                                                                                                FROM GOPER, GTIPOPER 
            //                                                                                                WHERE GOPER.CODEMPRESA = GTIPOPER.CODEMPRESA
            //                                                                                                AND GOPER.CODTIPOPER = GTIPOPER.CODTIPOPER 
            //                                                                                                AND	GOPER.CODEMPRESA = ? 
            //                                                                                                AND GOPER.CODFILIAL = ? 
            //                                                                                                AND GTIPOPER.USAOPERACAONFE = 1 
            //                                                                                                AND GOPER.CODSERIE = ?
            //                                                                                                AND GOPER.DATAENTSAI >= ? 
            //                                                                                                AND GOPER.CODSTATUS <> 2", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text, cbSerie.SelectedValue, dteInicial.DateTime.ToString("yyyy-MM-dd") }));

            //    int NumeroFin = 0;

            //    if (!string.IsNullOrEmpty(dtFinal.Rows[0]["NUMSEQ"].ToString()))
            //    {
            //        NumeroFin = Convert.ToInt32(dtFinal.Rows[0]["NUMSEQ"]);
            //    }

            //    int Atual = NumeroIni;

            //    if (Atual < 1)
            //    {
            //        MessageBox.Show("Não existem lançamentos para a data informada!");
            //        splashScreenManager1.CloseWaitForm();
            //        return;
            //    }


            //    DataTable dtComposicao = conn.ExecQuery(@"SELECT X.* FROM (
            //                                                        SELECT 
            //                                                        GOPER.CODEMPRESA,
            //                                                        GOPER.CODFILIAL, 
            //                                                        GOPER.CODSERIE,
            //                                                        GOPER.NUMERO, 
            //                                                        (SELECT GNFESTADUAL.CODSTATUS FROM GNFESTADUAL WHERE GNFESTADUAL.CODEMPRESA = GOPER.CODEMPRESA AND GNFESTADUAL.CODOPER = GOPER.CODOPER) as CODSTATUS, 
            //                                                        (SELECT GTIPOPER.DESCRICAO FROM GTIPOPER WHERE GTIPOPER.CODEMPRESA = GOPER.CODEMPRESA AND GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER) as DESCRICAO_TIPOOPER, 
            //                                                        (SELECT GTIPOPER.USAOPERACAONFE FROM GTIPOPER WHERE GTIPOPER.CODEMPRESA = GOPER.CODEMPRESA AND GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER) as USAOPERACAONFE, 
            //                                                        GOPER.DATAENTSAI,
            //                                                        GOPER.CODTIPOPER, 
            //                                                        (SELECT VCLIFOR.NOMEFANTASIA FROM VCLIFOR WHERE VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR) AS CLIENTE 
            //                                                        from GOPER 
            //                                                        ) AS X 
            //                                                        WHERE 
            //                                                        X.CODEMPRESA = ?
            //                                                        AND X.CODFILIAL = ?
            //                                                        AND X.USAOPERACAONFE = 1
            //                                                        AND X.CODSERIE = ?
            //                                                        AND (X.CODSTATUS IS NULL OR X.CODSTATUS IN ('E','I')) AND X.DATAENTSAI >= ? 
            //                                                        ORDER BY X.CODEMPRESA,X.CODFILIAL,X.NUMERO,X.DATAENTSAI,X.CODTIPOPER", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text, cbSerie.SelectedValue, dteInicial.DateTime.ToString("yyyy-MM-dd") });



            //    verifica se existe a nf
            //    DataTable dtExiste = null;

            //    while (Atual < NumeroFin)
            //    {
            //        dtExiste = conn.ExecQuery(@"SELECT GOPER.NUMERO FROM 
            //                                    GOPER, GTIPOPER
            //                                    WHERE 
            //                                    GOPER.CODEMPRESA = GTIPOPER.CODEMPRESA
            //                                    AND GOPER.CODTIPOPER = GTIPOPER.CODTIPOPER
            //                                    AND GOPER.CODEMPRESA = ?
            //                                    AND GOPER.CODFILIAL = ?
            //                                    AND GTIPOPER.USAOPERACAONFE = 1
            //                                    AND GOPER.CODSERIE = ?
            //                                    AND GOPER.NUMERO = ?", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text, cbSerie.SelectedValue, Atual.ToString().PadLeft(9, '0') });

            //        if (dtExiste.Rows.Count < 1)
            //        {
            //            R.Add(
            //                new REGISTROS(
            //                    AppLib.Context.Empresa.ToString(),
            //                    lpFilial.txtcodigo.Text,
            //                    cbSerie.SelectedValue.ToString(),
            //                    Atual.ToString().PadLeft(9, '0'),
            //                    "",
            //                    "",
            //                    "",
            //                    "",
            //                    "",
            //                    "")
            //                    );

            //        }

            //        Atual++;
            //    }

            //    foreach (DataRow dr in dtComposicao.Rows)
            //    {
            //        R.Add(
            //            new REGISTROS(
            //                dr["CODEMPRESA"].ToString(),
            //                dr["CODFILIAL"].ToString(),
            //                dr["CODSERIE"].ToString(),
            //                dr["NUMERO"].ToString(),
            //                dr["CODSTATUS"].ToString(),
            //                dr["DESCRICAO_TIPOOPER"].ToString(),
            //                dr["USAOPERACAONFE"].ToString(),
            //                dr["DATAENTSAI"].ToString(),
            //                dr["CODTIPOPER"].ToString(),
            //                dr["CLIENTE"].ToString())
            //                );
            //    }

            //    var dados = from p in R.AsEnumerable()
            //                orderby p.NUMERO ascending
            //                select p;

            //    DataTable dtDados = LINQToDataTable(dados);

            //    gridControl1.DataSource = dtDados;
            //    RenomeiaColunas(gridView1);

            //    splashScreenManager1.CloseWaitForm();
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}

            #endregion

            if (ValidaFilial() == false)
            {
                return;
            }

            if (ValidaSerie() == false)
            {
                return;
            }

            if (ValidaDataInicial() == false)
            {
                return;
            }

            try
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("Consultando");

                Token = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TOKEN FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text }).ToString();

                //cria um data table para usuar no lugar da lista
                DataTable meuDataTable = new DataTable();
                meuDataTable.Columns.Add("CODEMPRESA", typeof(string));
                meuDataTable.Columns.Add("NUMERO", typeof(string));
                meuDataTable.Columns.Add("CODOPER", typeof(string));
                meuDataTable.Columns.Add("CODTIPOPER", typeof(string));
                meuDataTable.Columns.Add("DESCRICAO_TIPOOPER", typeof(string));
                meuDataTable.Columns.Add("DATAENTSAI", typeof(string));
                meuDataTable.Columns.Add("NF", typeof(string));
                meuDataTable.Columns.Add("CODSTATUS", typeof(string));
                meuDataTable.Columns.Add("CLIENTE", typeof(string));
                meuDataTable.Columns.Add("CODFILIAL", typeof(string));
                meuDataTable.Columns.Add("CODSERIE", typeof(string));
                meuDataTable.Columns.Add("USAOPERACAONFE", typeof(string));

                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();

                string ano = dteInicial.DateTime.ToString("yyyy-MM-dd").Substring(2, 2);
                string mes = dteInicial.DateTime.ToString("yyyy-MM-dd").Substring(5, 2);

                //lista todas as nf 
                string sqlTodas = @"SELECT X.* FROM(
                                    SELECT 
                                    SUBSTRING(GNFESTADUAL.CHAVEACESSO,26,9) AS NUMERO,
                                    (
                                      SELECT CODFILIAL FROM GFILIAL 
                                      WHERE REPLACE(REPLACE(REPLACE(CGCCPF,'.',''),'/',''),'-','') = SUBSTRING(GNFESTADUAL.CHAVEACESSO,7,14)
                                    ) AS CODFILIAL
                                    FROM  
                                    GNFESTADUAL 
                                    WHERE
                                    GNFESTADUAL.CODEMPRESA = ?
                                    AND SUBSTRING(GNFESTADUAL.CHAVEACESSO,23,3) = ?
                                    AND SUBSTRING(GNFESTADUAL.CHAVEACESSO,3,2) = ?
                                    AND SUBSTRING(GNFESTADUAL.CHAVEACESSO,5,2) = ? 
                                    ) AS X
                                    WHERE X.CODFILIAL = ? ORDER BY X.NUMERO";
                //coempresa, serie, ano,mes, filial

                DataTable dtTodos = conn.ExecQuery(sqlTodas, new object[] { AppLib.Context.Empresa, cbSerie.SelectedValue.ToString().PadLeft(3, '0'), ano, mes, lpFilial.txtcodigo.Text });

                //pega o primeiro valor da sequencia
                string sqlInicio = @"SELECT MAX(X.NUMERO) AS MAXIMO, MIN(X.NUMERO) AS MINIMO FROM(
                                    SELECT 
                                    SUBSTRING(GNFESTADUAL.CHAVEACESSO,26,9) AS NUMERO,
                                    (
                                      SELECT CODFILIAL FROM GFILIAL 
                                      WHERE REPLACE(REPLACE(REPLACE(CGCCPF,'.',''),'/',''),'-','') = SUBSTRING(GNFESTADUAL.CHAVEACESSO,7,14)
                                    ) AS CODFILIAL
                                    FROM  
                                    GNFESTADUAL 
                                    WHERE
                                    GNFESTADUAL.CODEMPRESA =?
                                    AND SUBSTRING(GNFESTADUAL.CHAVEACESSO,23,3) = ?
                                    AND SUBSTRING(GNFESTADUAL.CHAVEACESSO,3,2) = ?
                                    AND SUBSTRING(GNFESTADUAL.CHAVEACESSO,5,2) = ?
                                    ) AS X
                                    WHERE X.CODFILIAL = ?  
                                    GROUP BY X.CODFILIAL";

                DataTable dtMinMax = conn.ExecQuery(sqlInicio, new object[] { AppLib.Context.Empresa, cbSerie.SelectedValue.ToString().PadLeft(3, '0'), ano, mes, lpFilial.txtcodigo.Text });

                string NumeroIni = dtMinMax.Rows[0]["MINIMO"].ToString().TrimStart('0');
                string NumeroFim = dtMinMax.Rows[0]["MAXIMO"].ToString().TrimStart('0');

                //cria um array com todos os numeros da sequencia
                ArrayList tabCompleta = new ArrayList();
                for (int i = Convert.ToInt32(NumeroIni); i <= Convert.ToInt32(NumeroFim); i++)
                {
                    tabCompleta.Add(i);
                }

                //array com os valores da base
                ArrayList tabDaBase = new ArrayList();
                int indice;
                foreach (DataRow item in dtTodos.Rows)
                {
                    indice = Convert.ToInt32(item["NUMERO"].ToString().TrimStart('0'));
                    tabDaBase.Add(indice);
                }

                //compara os dois arrays e retorna a diferença
                var diferenca = tabCompleta.ToArray().Except(tabDaBase.ToArray()).ToArray();

                string sqlPesquisa = "";
                DataTable dtPreenche = null;
                string numeroPesquisa = "";
                string empresaPesquisa = AppLib.Context.Empresa.ToString();

                //pega descrição
                string descricao;
                string CODTIPOPER;

                //pega o cliente
                string nomeCliente;
                string CODCLIFOR;

                //pega o status
                string CODSTATUS;

                //ja existe na nfestadual inut
                DataTable dtExiste = null;

                for (int i = 0; i < diferenca.Length; i++)
                {
                    // Validar
                    numeroPesquisa = diferenca[i].ToString().PadLeft(9, '0');
                    //numeroPesquisa = diferenca[i].ToString().PadLeft(6, '0');

                    //verifica se ja existe na gnfestadualinut
                    dtExiste = null;
                    dtExiste = conn.ExecQuery(@"select IDENTIFICADOR from GNFESTADUALINUT where NUMERONFE = ?", new object[] { numeroPesquisa });
                    if (dtExiste.Rows.Count > 0)
                    {
                        meuDataTable.Rows.Add(
                                    "",
                                     numeroPesquisa,
                                     "",
                                     "",
                                     "",
                                     "",
                                     "",
                                     "",
                                     "",
                                     "",
                                     "",
                                     ""
                                );

                        continue;
                    }
                    else
                    {
                        //pesquisa cada uma das notas para preencher
                        sqlPesquisa = "";
                        sqlPesquisa += "SELECT X.* FROM (";
                        sqlPesquisa += "SELECT";
                        sqlPesquisa += "(";
                        sqlPesquisa += "  SELECT CODOPER FROM GOPER WHERE CODEMPRESA = '" + empresaPesquisa + "' AND NUMERO = '" + numeroPesquisa + "' AND";
                        sqlPesquisa += "      CODTIPOPER IN (";
                        sqlPesquisa += "    SELECT CODTIPOPER FROM GTIPOPER ";
                        sqlPesquisa += "    WHERE CODEMPRESA = '" + empresaPesquisa + "' AND USAOPERACAONFE = 1";
                        sqlPesquisa += "    )";
                        sqlPesquisa += ") AS CODOPER,";
                        sqlPesquisa += "(";
                        sqlPesquisa += "  SELECT DATAENTSAI FROM GOPER WHERE CODEMPRESA = '" + empresaPesquisa + "' AND NUMERO = '" + numeroPesquisa + "' AND";
                        sqlPesquisa += "      CODTIPOPER IN (";
                        sqlPesquisa += "    SELECT CODTIPOPER FROM GTIPOPER ";
                        sqlPesquisa += "    WHERE CODEMPRESA = '" + empresaPesquisa + "' AND USAOPERACAONFE = 1";
                        sqlPesquisa += "    )";
                        sqlPesquisa += ") AS DATAENTSAI,";
                        sqlPesquisa += "(";
                        sqlPesquisa += "CASE ";
                        sqlPesquisa += " WHEN";
                        sqlPesquisa += "   (SELECT count(CODOPER) FROM GNFESTADUAL WHERE CODEMPRESA = '" + empresaPesquisa + "' AND SUBSTRING(CHAVEACESSO,26,9) = '" + numeroPesquisa + "') > 0 THEN 'SIM'";
                        sqlPesquisa += "   ELSE";
                        sqlPesquisa += "   'NÃO'";
                        sqlPesquisa += " END";
                        sqlPesquisa += ")";
                        sqlPesquisa += " AS NF,";
                        sqlPesquisa += "(";
                        sqlPesquisa += " SELECT CODSTATUS FROM GNFESTADUAL WHERE CODEMPRESA = '" + empresaPesquisa + "' AND CHAVEACESSO like '%" + numeroPesquisa + "%'";
                        sqlPesquisa += ") AS STATUS";
                        sqlPesquisa += ") AS X WHERE substring(Convert(varchar(4), DATEPART(year, X.DATAENTSAI)), 3, 2) = ? AND DATEPART(month, X.DATAENTSAI) = ?";

                        dtPreenche = conn.ExecQuery(sqlPesquisa, new object[] { ano, mes.TrimStart('0') });

                        if (dtPreenche.Rows.Count > 0)
                        {
                            //pega  descricao
                            CODTIPOPER = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"select CODTIPOPER from GOPER WHERE CODEMPRESA=? AND CODOPER=?", new object[] { empresaPesquisa, dtPreenche.Rows[0]["CODOPER"].ToString() }).ToString();
                            descricao = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"select DESCRICAO from GTIPOPER where CODTIPOPER = ? and CODEMPRESA = ?", new object[] { CODTIPOPER, empresaPesquisa }).ToString();

                            //pega o cliente
                            CODCLIFOR = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"select CODCLIFOR from GOPER WHERE CODEMPRESA=? AND CODOPER=?", new object[] { empresaPesquisa, dtPreenche.Rows[0]["CODOPER"].ToString() }).ToString();
                            nomeCliente = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"select NOME from VCLIFOR where CODCLIFOR = ? and CODEMPRESA = ?", new object[] { CODCLIFOR, empresaPesquisa }).ToString();

                            //pega o status
                            if (String.IsNullOrEmpty(dtPreenche.Rows[0]["STATUS"].ToString()))
                                CODSTATUS = "";
                            else
                                CODSTATUS = dtPreenche.Rows[0]["STATUS"].ToString();

                            meuDataTable.Rows.Add(
                                                empresaPesquisa,
                                                numeroPesquisa,
                                                dtPreenche.Rows[0]["CODOPER"].ToString(),
                                                CODTIPOPER,
                                                descricao,
                                                dtPreenche.Rows[0]["DATAENTSAI"].ToString(),
                                                dtPreenche.Rows[0]["NF"].ToString(),
                                                CODSTATUS,
                                                nomeCliente,
                                                lpFilial.txtcodigo.Text,
                                                cbSerie.SelectedValue.ToString(),
                                                "1"
                                    );
                        }
                        dtPreenche = null;

                    }
                }

                //preenche CODSTATUS E OU I
                DataTable dtTodosCodOper = null;
                string sqlTodosCodOper = "SELECT CODOPER, CODEMPRESA FROM GOPER WHERE MONTH(DATAENTSAI)=? AND substring(Convert(varchar(4), DATEPART(year, DATAENTSAI)), 3, 2) = ? AND CODEMPRESA = ? order by CODOPER";
                dtTodosCodOper = conn.ExecQuery(sqlTodosCodOper, new object[] { mes.TrimStart('0'), ano, AppLib.Context.Empresa.ToString() });

                string codOperEncontrei;
                string codEmpEncontrei;
                //string saida = "";

                if (dtTodosCodOper.Rows.Count > 0)
                {
                    foreach (DataRow item in dtTodosCodOper.Rows)
                    {
                        codOperEncontrei = item["CODOPER"].ToString();
                        codEmpEncontrei = item["CODEMPRESA"].ToString();

                        //Pesquisa cada codoper
                        string sqlPesquisaUmCodOper = "";
                        sqlPesquisaUmCodOper += "SELECT ";
                        sqlPesquisaUmCodOper += "GNFESTADUAL.CODOPER, ";
                        sqlPesquisaUmCodOper += "(SELECT DATAENTSAI FROM GOPER WHERE CODEMPRESA = '" + codEmpEncontrei + "' AND CODTIPOPER IN(SELECT CODTIPOPER FROM GTIPOPER     WHERE CODEMPRESA = '" + codEmpEncontrei + "' AND USAOPERACAONFE = 1  AND CODOPER = '" + codOperEncontrei + "')) AS DATAENTSAI, ";
                        sqlPesquisaUmCodOper += "(CASE  WHEN(SELECT count(CODOPER) FROM GNFESTADUAL WHERE CODEMPRESA = '" + codEmpEncontrei + "' AND CODOPER = '" + codOperEncontrei + "') > 0 THEN 'SIM'   ELSE   'NÃO' END) AS NF, ";
                        sqlPesquisaUmCodOper += "GNFESTADUAL.CODSTATUS,SUBSTRING(GNFESTADUAL.CHAVEACESSO,26,9) AS NUMERO ";
                        sqlPesquisaUmCodOper += "FROM GNFESTADUAL ";
                        sqlPesquisaUmCodOper += " WHERE GNFESTADUAL.CODEMPRESA = '" + codEmpEncontrei + "' AND GNFESTADUAL.CODOPER = '" + codOperEncontrei + "' AND CODSTATUS IN('E', 'I') ";

                        DataTable dtCodStatus = null;

                        dtCodStatus = conn.ExecQuery(sqlPesquisaUmCodOper, null);
                        string CODTIPOPER_codstatus = "";
                        string descricao_codstatus = "";
                        string CODCLIFOR_codstatus = "";
                        string nomeCliente_codstatus = "";

                        if (dtCodStatus.Rows.Count > 0)
                        {
                            //pega  descricao
                            CODTIPOPER_codstatus = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"select CODTIPOPER from GOPER WHERE CODEMPRESA=? AND CODOPER=?", new object[] { codEmpEncontrei, codOperEncontrei }).ToString();
                            descricao_codstatus = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"select DESCRICAO from GTIPOPER where CODTIPOPER = ? and CODEMPRESA = ?", new object[] { CODTIPOPER_codstatus, empresaPesquisa }).ToString();

                            //pega o cliente
                            CODCLIFOR_codstatus = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"select CODCLIFOR from GOPER WHERE CODEMPRESA=? AND CODOPER=?", new object[] { codEmpEncontrei, codOperEncontrei }).ToString();
                            nomeCliente_codstatus = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"select NOME from VCLIFOR where CODCLIFOR = ? and CODEMPRESA = ?", new object[] { CODTIPOPER_codstatus, codEmpEncontrei }).ToString();

                            meuDataTable.Rows.Add(
                                                codEmpEncontrei,
                                                dtCodStatus.Rows[0]["NUMERO"].ToString(),
                                                codOperEncontrei,
                                                CODTIPOPER_codstatus,
                                                descricao_codstatus,
                                                dtCodStatus.Rows[0]["DATAENTSAI"].ToString(),
                                                dtCodStatus.Rows[0]["NF"].ToString(),
                                                dtCodStatus.Rows[0]["CODSTATUS"].ToString(),
                                                nomeCliente_codstatus,
                                                lpFilial.txtcodigo.Text,
                                                cbSerie.SelectedValue.ToString(),
                                                "1"
                                    );

                        }
                    }
                }

                gridControl1.DataSource = meuDataTable;
                gridView1.BestFitColumns();

                splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            if (ValidaJustificativa() == false)
            {
                return;
            }

            string TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text }).ToString();

            string UF = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODIBGE
                                                                                                    FROM GFILIAL
                                                                                                    INNER JOIN GESTADO ON GFILIAL.CODETD = GESTADO.CODETD
                                                                                                    WHERE GFILIAL.CODEMPRESA = ? AND GFILIAL.CODFILIAL = ?", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text }).ToString();

            string Ano = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT RIGHT(YEAR(GETDATE()),2)", new object[] { }).ToString();
            string CNPJ = NfeAPI.getCGCCPFFormatado(Convert.ToInt32(lpFilial.txtcodigo.Text));
            NumeroFormatado = ValidaMascaraNumero();

            RetornoInut = NfeAPI.InutilizacaoNFe(Token, UF, TpAmb, Ano, CNPJ, cbSerie.SelectedValue.ToString(), NumeroFormatado, NumeroFormatado, tbObservacao.Text);

            dynamic JsonRetornoInut = JsonConvert.DeserializeObject(RetornoInut);
            string StatusInut = JsonRetornoInut.status;

            if (StatusInut == "200")
            {
                string Protocolo = JsonRetornoInut.retornoInutNFe.nProt;
                string Status = "A";
                string Motivo = JsonRetornoInut.retornoInutNFe.xMotivo;
                string XMLInut = JsonRetornoInut.retornoInutNFe.xmlInut;
                XMLInut = XMLInut.Replace("'", "\"");

                InsertGNFESTADUALINUT(Protocolo, XMLInut, Status, Motivo, TpAmb);

                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();

                int Codoper = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT CODOPER 
                                                                                                           FROM GOPER
                                                                                                           WHERE CODEMPRESA = ? AND CODFILIAL = ? AND NUMERO = ?", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text, tbFaixaInicial.Text }));

                DateTime DataOperacao = Convert.ToDateTime(conn.ExecGetField(null, "SELECT DATAEMISSAO FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper }));

                //Exclui Nota Fiscal
                ExcluiNotaFiscal(Codoper);

                //Cancela Operação
                CancelamentoOperacao(conn, Codoper, DataOperacao, Convert.ToInt32(lpFilial.txtcodigo.Text));

                //Excui Operação 
                ExcuiOperacao(conn, Codoper, cbSerie.SelectedValue.ToString(), tbFaixaInicial.Text);

                MessageBox.Show("Operação inutilizada com sucesso.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else if (StatusInut == "-3")
            {
                MessageBox.Show("Inutilização já processada e autorizada anteriormente.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MessageBox.Show("Erro interno ao processar a requisição.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Validações

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidaJustificativa()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (tbObservacao.Text.Length < 15)
            {
                errorProvider1.SetIconAlignment(lbEvento, ErrorIconAlignment.MiddleRight);
                errorProvider1.SetError(lbEvento, "A justificativa deve conter no mínimo 15 caracteres.");
                verifica = false;
            }

            return verifica;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidaFilial()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(lpFilial.txtcodigo.Text))
            {
                errorProvider1.SetIconAlignment(lpFilial.txtcodigo, ErrorIconAlignment.BottomRight);
                errorProvider1.SetError(lpFilial.txtcodigo, "A Filial precisa ser preenchida.");
                verifica = false;
            }

            return verifica;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidaSerie()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(cbSerie.SelectedValue.ToString()))
            {
                errorProvider1.SetError(labelControl1, "O número da Série precisa ser preenchido.");
                verifica = false;
            }

            return verifica;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidaDataInicial()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (dteInicial.EditValue == null)
            {
                errorProvider1.SetError(labelControl5, "A data inicial precisa ser informada.");
                verifica = false;
            }

            return verifica;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string ValidaMascaraNumero()
        {
            string NovoNumero = string.Empty;
            int Mascara = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT GTIPOPER.MASKNUMEROSEQ
                                                                                                         FROM GTIPOPER
                                                                                                         INNER JOIN GOPER ON GTIPOPER.CODEMPRESA = GOPER.CODEMPRESA
                                                                                                         AND GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER
                                                                                                         WHERE GOPER.CODEMPRESA = ? AND GOPER.CODFILIAL = ? AND GOPER.NUMERO = ?", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text, tbFaixaInicial.Text }));
            if (tbFaixaInicial.Text.Length == Mascara)
            {
                NovoNumero = tbFaixaInicial.Text.TrimStart('0');
            }

            return NovoNumero;
        }

        #endregion

        #region Métodos 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Protocolo"></param>
        /// <param name="XML"></param>
        /// <param name="Status"></param>
        /// <param name="Motivo"></param>
        private void InsertGNFESTADUALINUT(string Protocolo, string XML, string Status, string Motivo, string tpAmb)
        {
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppLib.Context.poolConnection.Get("Start").ConnectionString);
            System.Data.SqlClient.SqlCommand command;

            try
            {
                conn.Open();

                command = new System.Data.SqlClient.SqlCommand("INSERT INTO GNFESTADUALINUT (CODEMPRESA, CODFILIAL, SERIE, NUMERONFE, DATA, MODELO, TPAMB, JUSTIFICATIVA, PROTOCOLO, XMLINUT, STATUS, CODUSUARIO, MOTIVO) VALUES (" + AppLib.Context.Empresa + ", " + lpFilial.txtcodigo.Text + ", '" + cbSerie.SelectedValue + "', '" + tbFaixaInicial.Text + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + tbModelo.Text + "', " + tpAmb + ", '" + tbObservacao.Text + "', '" + Protocolo + "', '" + XML + "', '" + Status + "', '" + AppLib.Context.Usuario + "','" + Motivo + "')", conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para verificar a permissão de cancelamento da Operação
        /// </summary>
        /// <returns>Permissão de cancelamento da Operação</returns>
        private bool PermissaoOperacao()
        {
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
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método para exclusão do Saldo do Estoque
        /// </summary>
        /// <param name="conn">Conexão ativa</param>
        /// <param name="Codoper">Código da Operação</param>
        /// <param name="DataOper">Data de Emissão da Operação</param>
        /// <param name="Codfilial">Código da Filial</param>
        private void ExcluiSaldoEstoque(AppLib.Data.Connection conn, int Codoper, DateTime DataOper, int Codfilial)
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
									AND GOPERITEM.CODOPER = ?)", new object[] { DataOper, AppLib.Context.Empresa, Codfilial, AppLib.Context.Empresa, Codoper });

                conn.ExecTransaction(@"DELETE FROM VFICHAESTOQUE
WHERE VFICHAESTOQUE.DATAENTSAI >= ?
AND VFICHAESTOQUE.CODEMPRESA = ?
AND VFICHAESTOQUE.CODFILIAL = ? 
AND VFICHAESTOQUE.CODPRODUTO IN  (SELECT GOPERITEM.CODPRODUTO
									FROM GOPERITEM
									WHERE GOPERITEM.CODEMPRESA = ? 
									AND GOPERITEM.CODOPER = ?)", new object[] { DataOper, AppLib.Context.Empresa, Codfilial, AppLib.Context.Empresa, Codoper });

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
,GOPERITEM.NSEQITEM", new object[] { AppLib.Context.Empresa, Codfilial, AppLib.Context.Empresa, Codoper, DataOper });

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

        /// <summary>
        /// Método responsável pela exclusão do Financeiro 
        /// </summary>
        /// <param name="conn">Conexão ativa</param>
        /// <param name="Codoper">código da Operação</param>
        /// <returns></returns>
        private bool excluiFinanceiro(AppLib.Data.Connection conn, int Codoper)
        {
            try
            {
                conn.ExecTransaction("DELETE FROM FLANCA WHERE CODOPER = ? AND CODEMPRESA =?", new object[] { Codoper, AppLib.Context.Empresa });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="Codoper"></param>
        /// <param name="DataOperacao"></param>
        /// <param name="Codfilial"></param>
        private void CancelamentoOperacao(AppLib.Data.Connection conn, int Codoper, DateTime DataOperacao, int Codfilial)
        {
            if (PermissaoOperacao() == true)
            {
                try
                {
                    ExcluiSaldoEstoque(conn, Codoper, DataOperacao, Codfilial);

                    if (excluiFinanceiro(conn, Codoper) == true)
                    {
                        #region Remove os relacionamentos e calcula a operação de origem novamente

                        DataTable dtRelac = conn.ExecQuery("SELECT GOPERITEMRELAC.CODOPERITEMORIGEM, GOPERITEMRELAC.NSEQITEMORIGEM, GOPERITEM.NSEQITEM, GOPERITEM.QUANTIDADE, GOPERITEM.CODEMPRESA, GOPER.CODTIPOPER FROM GOPERITEMRELAC INNER JOIN GOPERITEM ON GOPERITEMRELAC.CODOPERITEMDESTINO = GOPERITEM.CODOPER AND GOPERITEMRELAC.NSEQITEMDESTINO = GOPERITEM.NSEQITEM  INNER JOIN GOPER ON GOPERITEMRELAC.CODOPERITEMORIGEM = GOPER.CODOPER WHERE GOPERITEMRELAC.CODOPERITEMDESTINO = ? AND GOPER.CODEMPRESA = ?", new object[] { Codoper, AppLib.Context.Empresa });

                        if (dtRelac.Rows.Count > 0)
                        {
                            PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                            psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
                            psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };
                            psPartLocalEstoqueSaldoData.MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.OperacaoCancelamento, AppLib.Context.Empresa, Codoper, 0);
                            conn.ExecTransaction("DELETE FROM VFICHAESTOQUE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codfilial, Codoper });

                            for (int iRelac = 0; iRelac < dtRelac.Rows.Count; iRelac++)
                            {
                                PSPartOperacaoEdit frmGera = new PSPartOperacaoEdit();
                                frmGera.codFilial = Codfilial;

                                string aaaa = conn.ParseCommand("UPDATE GOPERITEM SET QUANTIDADE_SALDO = QUANTIDADE_SALDO + " + dtRelac.Rows[iRelac]["QUANTIDADE"].ToString().Replace(",", ".") + ", QUANTIDADE_FATURADO = QUANTIDADE_FATURADO - " + dtRelac.Rows[iRelac]["QUANTIDADE"].ToString().Replace(",", ".") + " WHERE CODOPER = ? AND CODEMPRESA = ? AND NSEQITEM = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"], dtRelac.Rows[iRelac]["CODEMPRESA"], dtRelac.Rows[iRelac]["NSEQITEMORIGEM"] });

                                conn.ExecTransaction("UPDATE GOPERITEM SET QUANTIDADE_SALDO = QUANTIDADE_SALDO + " + dtRelac.Rows[iRelac]["QUANTIDADE"].ToString().Replace(",", ".") + ", QUANTIDADE_FATURADO = QUANTIDADE_FATURADO - " + dtRelac.Rows[iRelac]["QUANTIDADE"].ToString().Replace(",", ".") + " WHERE CODOPER = ? AND CODEMPRESA = ? AND NSEQITEM = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"], dtRelac.Rows[iRelac]["CODEMPRESA"], dtRelac.Rows[iRelac]["NSEQITEMORIGEM"] });

                                frmGera.calculaOperacao(Convert.ToInt32(dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"]), dtRelac.Rows[iRelac]["CODTIPOPER"].ToString());

                                conn.ExecTransaction("UPDATE GOPER SET CODSTATUS = 0 WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"], AppLib.Context.Empresa });
                                conn.ExecTransaction("UPDATE GOPER SET GOPER.CODSTATUS = CASE WHEN ((SELECT (SUM(GOPERITEM.QUANTIDADE) - SUM(GOPERITEM.QUANTIDADE_SALDO)) FROM GOPERITEM WHERE GOPERITEM.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEM.CODOPER = GOPER.CODOPER) = 0) THEN '0' ELSE '5' END FROM GOPER WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"], AppLib.Context.Empresa });
                                conn.ExecTransaction("DELETE FROM GOPERITEMRELAC WHERE CODOPERITEMORIGEM = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"].ToString() });
                                conn.ExecTransaction("DELETE FROM GOPERRELAC WHERE CODOPER = ? AND CODOPERRELAC = ? AND CODEMPRESA =?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"], Codoper, AppLib.Context.Empresa });
                            }
                        }
                        else
                        {
                            dtRelac = conn.ExecQuery(@"SELECT GOPERRELAC.CODOPERRELAC, GOPERRELAC.CODEMPRESA, GOPER.CODOPER 
                                                                FROM GOPERRELAC
                                                                    INNER JOIN GOPER ON GOPERRELAC.CODOPER = GOPER.CODOPER
                                                                WHERE GOPERRELAC.CODOPERRELAC = ?
                                                                AND GOPERRELAC.CODEMPRESA = ?", new object[] { Codoper, AppLib.Context.Empresa });

                            for (int iRelac = 0; iRelac < dtRelac.Rows.Count; iRelac++)
                            {
                                conn.ExecTransaction("UPDATE GOPER SET GOPER.CODSTATUS = 0 FROM GOPER WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { dtRelac.Rows[iRelac]["CODOPER"], AppLib.Context.Empresa });
                                conn.ExecTransaction("DELETE FROM GOPERITEMRELAC WHERE CODOPERITEMORIGEM = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"].ToString() });
                                conn.ExecTransaction("DELETE FROM GOPERRELAC WHERE CODOPER = ? AND CODOPERRELAC = ? AND CODEMPRESA =?", new object[] { dtRelac.Rows[iRelac]["CODOPER"], dtRelac.Rows[iRelac]["CODOPERRELAC"], AppLib.Context.Empresa });
                            }
                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="Codoper"></param>
        /// <param name="CodSerie"></param>
        /// <param name="Numero"></param>
        private void ExcuiOperacao(AppLib.Data.Connection conn, int Codoper, string CodSerie, string Numero)
        {
            conn.ExecTransaction("DELETE FROM GMOTIVOCANCELAMENTO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper });
            conn.ExecTransaction("DELETE FROM GOPERITEMCOPIAREF WHERE CODOPERDESTINO = ? AND CODEMPRESA = ? ", new object[] { Codoper, AppLib.Context.Empresa });
            conn.ExecTransaction("DELETE FROM GOPERCOPIAREF WHERE CODOPERDESTINO = ? AND CODEMPRESA = ? ", new object[] { Codoper, AppLib.Context.Empresa });
            conn.ExecTransaction("DELETE FROM GOPER WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { Codoper, AppLib.Context.Empresa });

            string Ultimo = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT MAX(NUMSEQ) 
                                                                                                     FROM VSERIE
                                                                                                     WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODSERIE = ? ", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text, CodSerie }).ToString();

            if (Numero == Ultimo)
            {
                if (MessageBox.Show("Deseja voltar a numeração?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int UltimoNumero = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT MAX(NUMSEQ) 
                                                                                                                            FROM VSERIE
                                                                                                                            WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODSERIE = ? ", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text, CodSerie }));
                    UltimoNumero--;
                    string NovoNumero = string.Concat(UltimoNumero.ToString().PadLeft(Ultimo.Length, '0'));

                    try
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE VSERIE 
                                                                                      SET NUMSEQ = ?
                                                                                      WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODSERIE = ? ", new object[] { NovoNumero, AppLib.Context.Empresa, lpFilial.txtcodigo.Text, CodSerie });

                        MessageBox.Show("Numeração atualizada com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void ExcluiNotaFiscal(int _codOper)
        {
            if (_codOper != 0)
            {
                try
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GNFESTADUALHISTORICO WHERE CODEMPRESA  = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codOper });
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GNFESTADUALEVENTO WHERE CODEMPRESA  = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codOper });
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GNFESTADUALCORRECAO WHERE CODEMPRESA  = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codOper });
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GNFESTADUALCANC WHERE CODEMPRESA  = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codOper });
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GNFESTADUAL WHERE CODEMPRESA  = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, _codOper });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void RenomeiaColunas(GridView View)
        {
            View.Columns["CODEMPRESA"].Caption = "Código da Empresa";
            View.Columns["CODFILIAL"].Caption = "Código da Filial";
            View.Columns["CODSERIE"].Caption = "Código da Série";
            View.Columns["NUMERO"].Caption = "Número";
            View.Columns["CODSTATUS"].Caption = "Código do Status";
            View.Columns["DESCRICAO_TIPOOPER"].Caption = "Tipo de Operação";
            View.Columns["USAOPERACAONFE"].Caption = "Usa Operação Nf-e";
            View.Columns["DATAENTSAI"].Caption = "Data de Entrada/Saída";
            View.Columns["CODTIPOPER"].Caption = "Código do Tipo da Operação";
            View.Columns["CLIENTE"].Caption = "Nome Fantasia";

            View.BestFitColumns();
        }

        private void CarregaCampos()
        {

        }

        public DataTable LINQToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();

            System.Reflection.PropertyInfo[] columns = null;

            if (Linqlist == null)
            {
                return dt;
            }

            foreach (T Record in Linqlist)
            {
                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();

                    foreach (System.Reflection.PropertyInfo GeProperty in columns)
                    {
                        Type ColumnType = GeProperty.PropertyType;

                        if ((ColumnType.IsGenericType) && (ColumnType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            ColumnType = ColumnType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GeProperty.Name, ColumnType));
                    }
                }

                DataRow row = dt.NewRow();

                foreach (System.Reflection.PropertyInfo PInfo in columns)
                {
                    row[PInfo.Name] = PInfo.GetValue(Record, null) == null ? DBNull.Value : PInfo.GetValue(Record, null);
                }

                dt.Rows.Add(row);
            }

            return dt;
        }

        private IEnumerable<List<string>> ConverteDatatableEmList(DataTable _dt)
        {
            int Index = _dt.Columns["CODSERIE"].Ordinal;
            List<string> list = new List<string>();

            foreach (DataRow row in _dt.Rows)
            {
                list.Add(row[Index].ToString());
            }
            yield return list;
        }

        #endregion

        public class REGISTROS
        {
            public string CODEMPRESA { get; set; }
            public string CODFILIAL { get; set; }
            public string CODSERIE { get; set; }
            public string NUMERO { get; set; }
            public string CODSTATUS { get; set; }
            public string DESCRICAO_TIPOOPER { get; set; }
            public string USAOPERACAONFE { get; set; }
            public string DATAENTSAI { get; set; }
            public string CODTIPOPER { get; set; }
            public string CLIENTE { get; set; }


            public REGISTROS(string _codempresa, string _codfilial, string _codserie, string _numero, string _codstatus, string _descricao_tipooper, string _usaoperacaonfe, string _dataentsai, string _codtipoper, string _cliente)
            {
                CODEMPRESA = _codempresa;
                CODFILIAL = _codfilial;
                CODSERIE = _codserie;
                NUMERO = _numero;
                CODSTATUS = _codstatus;
                DESCRICAO_TIPOOPER = _descricao_tipooper;
                USAOPERACAONFE = _usaoperacaonfe;
                DATAENTSAI = _dataentsai;
                CODTIPOPER = _codtipoper;
                CLIENTE = _cliente;
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                return;
            }

            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            tbFaixaInicial.Text = row["NUMERO"].ToString();
        }
    }
}
