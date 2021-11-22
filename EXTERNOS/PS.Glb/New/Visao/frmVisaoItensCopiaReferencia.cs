using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Visao
{
    public partial class frmVisaoItensCopiaReferencia : Form
    {

        List<Class.GoperItem> ListItensFaturar = new List<Class.GoperItem>();
        List<Class.GoperItem> listItens = null;
        public string codTipOperDestino = string.Empty;
        private DataRow parametroDestino;
        string codoper = string.Empty;
        public int codFilial = 0;

        public frmVisaoItensCopiaReferencia(List<Class.GoperItem> _listaItens)
        {
            InitializeComponent();
            listItens = _listaItens;

        }

        private void carregaItens()
        {
            try
            {
                for (int i = 0; i < listItens.Count; i++)
                {
                    if (i == 0)
                    {
                        codoper = listItens[i].CODOPER.ToString();
                    }
                    else
                    {
                        codoper = codoper + " , " + listItens[i].CODOPER;
                    }
                }

                string sql = @"SELECT x.* FROM (
SELECT
	GOPERITEM.CODEMPRESA  'Código da Empresa',
	GOPERITEM.CODOPER  'Operação',
	(SELECT NUMERO FROM GOPER WHERE CODEMPRESA = GOPERITEM.CODEMPRESA AND CODOPER = GOPERITEM.CODOPER ) 'Numero',
	GOPERITEM.NSEQITEM 'Seq.',
	GOPERITEM.CODPRODUTO 'Produto',
	(SELECT CODIGOAUXILIAR FROM VPRODUTO WHERE CODEMPRESA = GOPERITEM.CODEMPRESA AND CODPRODUTO = GOPERITEM.CODPRODUTO ) 'Codigo Auxiliar',
	(SELECT NOME FROM VPRODUTO WHERE CODEMPRESA = GOPERITEM.CODEMPRESA AND CODPRODUTO = GOPERITEM.CODPRODUTO ) 'Nome do Produto',
	GOPERITEM.QUANTIDADE 'Quantidade',
	GOPERITEM.CODUNIDOPER 'Unidade de Medida',
	ISNULL((SELECT SUM(ISNULL(GOPERITEMCOPIAREF.QUANTIDADE,0)) FROM GOPERITEMCOPIAREF WHERE CODEMPRESA = GOPERITEM.CODEMPRESA AND CODOPERDESTINO = GOPERITEM.CODOPER AND NSEQITEMDESTINO = GOPERITEM.NSEQITEM),0) 'Qtd. Referenciada',
	(GOPERITEM.QUANTIDADE - ISNULL((SELECT SUM(ISNULL(GOPERITEMCOPIAREF.QUANTIDADE,0)) FROM GOPERITEMCOPIAREF WHERE CODEMPRESA = GOPERITEM.CODEMPRESA AND CODOPERDESTINO = GOPERITEM.CODOPER AND NSEQITEMDESTINO = GOPERITEM.NSEQITEM),0) ) 'Qtd. Saldo',
	GOPERITEM.QUANTIDADE 'Qtd. Copia'
FROM
	GOPERITEM
WHERE
GOPERITEM.CODEMPRESA = " + AppLib.Context.Empresa + @"
AND GOPERITEM.CODOPER IN ( " + codoper + @" ) 
GROUP BY
	GOPERITEM.CODEMPRESA,
	GOPERITEM.CODOPER,
	GOPERITEM.NSEQITEM,
	GOPERITEM.CODPRODUTO,
	GOPERITEM.QUANTIDADE,
	GOPERITEM.CODUNIDOPER
) x
ORDER BY
	X.[CÓDIGO DA EMPRESA],
	X.[OPERAÇÃO],
	X.[SEQ.]";

                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmVisaoItensCopiaReferencia_Load(object sender, EventArgs e)
        {
            carregaItens();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            //
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                try
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                    decimal ori = Convert.ToDecimal(row1["Quantidade"]);
                    decimal saldo = Convert.ToDecimal(row1["Qtd. Saldo"]);
                    decimal faturado = Convert.ToDecimal(row1["Qtd. Copia"]);

                    if (faturado <= 0)
                    {
                        MessageBox.Show("Favor informar a quantidade a ser faturada.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    decimal cal = saldo - faturado;
                    if (cal < 0)
                    {
                        MessageBox.Show("A Quantidade Faturada não pode ser menor que a Quantidade do Saldo.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    row1["Quantidade"] = saldo - faturado;

                    ListItensFaturar.Add(listItens[Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString())]);
                    ListItensFaturar[i].QTD_ORIGINAL = faturado;
                    ListItensFaturar[i].QTD_FATURAR = faturado;
                    ListItensFaturar[i].QTD_SALDO = faturado;
                }
                catch (Exception)
                {

                }
            }

            if (ListItensFaturar.Count > 0)
            {

                int retorno = faturarParcial(Convert.ToInt32(ListItensFaturar[0].CODOPER));
                //inserir as chaves 
                DataTable dtChave = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CHAVENFE FROM GOPER WHERE CODOPER IN (" + codoper + ") AND CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });
                if (dtChave.Rows.Count > 0)
                {
                    for (int i = 0; i < dtChave.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(dtChave.Rows[i]["CHAVENFE"].ToString()))
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GOPERCOPIAREF (CODEMPRESA, CODOPERORIGEM, CODOPERDESTINO, CHAVENFE) VALUES (?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, ListItensFaturar[0].CODOPER, retorno, dtChave.Rows[i]["CHAVENFE"].ToString() });
                        }
                        else
                        {
                            string chave = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CHAVEACESSO FROM GNFESTADUAL WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { ListItensFaturar[0].CODOPER, AppLib.Context.Empresa }).ToString();

                            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GOPERCOPIAREF (CODEMPRESA, CODOPERORIGEM, CODOPERDESTINO, CHAVENFE) VALUES (?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, ListItensFaturar[0].CODOPER, retorno, chave });
                        }
                    
                    }
                }
                else
                {
                    
                }
                ListItensFaturar.Clear();

                if (retorno != 0)
                {
                    PSPartOperacaoEdit frm = new PSPartOperacaoEdit();
                    frm.codoper = Convert.ToInt32(retorno);
                    frm.btnFechar.Enabled = false;
                    frm.edita = true;
                    frm.faturamento = true;
                    frm.codFilial = codFilial;
                    frm.ShowDialog();
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Não foi possível abrir a tela de operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("Favor selecionar pelo menos um item para realizar o faturamento parcial.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //cancelar = true;
            }

        }
        // 
        private int faturarParcial(int codoper)
        {
            
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();
            try
            {
                getParametroOperacao(conn);
                // alteraValoresParametroOperacao();

                AppLib.ORM.Jit goper = new AppLib.ORM.Jit(conn, "GOPER");

                goper.Set("CODEMPRESA", AppLib.Context.Empresa);
                goper.Set("CODOPER", codoper);
                goper.Select();
                goper.Set("CODOPER", Convert.ToInt32(conn.ExecGetField(0, @"SELECT IDLOG FROM GLOG WHERE CODTABELA = ?", new object[] { "GOPER" })) + 1);
                goper.Set("NUMERO", new Class.CopiaOperacao(codFilial).GeraNumeroDocumento(codTipOperDestino));
                goper.Set("CODTIPOPER", codTipOperDestino);
                goper.Set("CODSERIE", conn.ExecGetField(string.Empty, @"SELECT CODSERIE FROM VTIPOPERSERIE where CODTIPOPER = ? and CODFILIAL = ? and PRINCIPAL =  1 AND CODEMPRESA = ?", new object[] { codTipOperDestino, codFilial, AppLib.Context.Empresa}).ToString());
                goper.Set("CODSTATUS", 0);
                goper.Set("DATACRIACAO", conn.GetDateTime());
                goper.Set("DATAEMISSAO", conn.GetDateTime());
                goper.Set("DATAENTSAI", null);
                goper.Set("CODUSUARIOCRIACAO", AppLib.Context.Usuario);
                goper.Set("NFE", null);
                goper.Set("CHAVENFE", null);
                if (goper.Get("CODFORMA") != DBNull.Value)
                {
                    goper.Set("CODFORMA", null);
                }
                goper.Insert();

                #region GOPERITEM
                //  alteraValoresParametroItens();
                for (int i = 0; i < ListItensFaturar.Count; i++)
                {

                    AppLib.ORM.Jit goperItem = new AppLib.ORM.Jit(conn, "GOPERITEM");
                    goperItem.Set("CODEMPRESA", AppLib.Context.Empresa);
                    goperItem.Set("CODOPER", goper.Get("CODOPER"));
                    goperItem.Set("NSEQITEM", i + 1);
                    goperItem.Set("CODPRODUTO", ListItensFaturar[i].CODPRODUTO);
                    goperItem.Set("QUANTIDADE", ListItensFaturar[i].QTD_FATURAR);
                    //goperItem.Set("CODTABPRECO", ListItensFaturar[i].CODTABPRECO);
                    goperItem.Set("VLUNITARIO", ListItensFaturar[i].VLUNITARIO);
                    goperItem.Set("VLUNITORIGINAL", ListItensFaturar[i].VLUNITORIGINAL);
                    goperItem.Set("CODUNIDOPER", ListItensFaturar[i].CODUNIDOPER);
                    goperItem.Set("VLACRESCIMO", Convert.ToDecimal(ListItensFaturar[i].VLACRESCIMO));//calculaAcrescimo( Convert.ToDecimal(ListItensFaturar[i].VLACRESCIMO), Convert.ToDecimal(ListItensFaturar[i].PRACRESCIMO), Convert.ToDecimal(ListItensFaturar[i].VLUNITORIGINAL)));

                    goperItem.Set("VLDESCONTO", Convert.ToDecimal(ListItensFaturar[i].VLDESCONTO));//calculaDesconto(ListItensFaturar[i].TIPODESCONTO, Convert.ToDecimal(ListItensFaturar[i].VLDESCONTO), Convert.ToDecimal(ListItensFaturar[i].PRDESCONTO),  Convert.ToDecimal(ListItensFaturar[i].VLUNITORIGINAL), Convert.ToDecimal(ListItensFaturar[i].QTD_FATURAR)));

                    goperItem.Set("PRDESCONTO", ListItensFaturar[i].PRDESCONTO);

                    goperItem.Set("VLTOTALITEM", CalculaTotalItem(ListItensFaturar[i].TIPODESCONTO, Convert.ToDecimal(ListItensFaturar[i].VLUNITORIGINAL), Convert.ToDecimal(ListItensFaturar[i].QTD_FATURAR), Convert.ToDecimal(goperItem.Get("VLACRESCIMO")), Convert.ToDecimal(goperItem.Get("VLDESCONTO"))));

                    goperItem.Set("CODNATUREZA", null);//ListItensFaturar[i].CODNATUREZA);
                    //goperItem.Set("OBSERVACAO", ListItensFaturar[i].OBSERVACAO);
                    //goperItem.Set("INFCOMPL", ListItensFaturar[i].INFCOMPL);
                    //goperItem.Set("CODUNIDOPER", ListItensFaturar[i].CODUNIDOPER);
                    //goperItem.Set("CAMPOLIVRE1", ListItensFaturar[i].CAMPOLIVRE1);
                    //goperItem.Set("CAMPOLIVRE2", ListItensFaturar[i].CAMPOLIVRE2);
                    //goperItem.Set("CAMPOLIVRE3", ListItensFaturar[i].CAMPOLIVRE3);
                    //goperItem.Set("CAMPOLIVRE4", ListItensFaturar[i].CAMPOLIVRE4);
                    //goperItem.Set("CAMPOLIVRE5", ListItensFaturar[i].CAMPOLIVRE5);
                    //goperItem.Set("CAMPOLIVRE6", ListItensFaturar[i].CAMPOLIVRE6);
                    //goperItem.Set("DATAEXTRA1", ListItensFaturar[i].DATAEXTRA1);
                    //goperItem.Set("DATAEXTRA2", ListItensFaturar[i].DATAEXTRA2);
                    //goperItem.Set("DATAEXTRA3", ListItensFaturar[i].DATAEXTRA3);
                    //goperItem.Set("DATAEXTRA4", ListItensFaturar[i].DATAEXTRA4);
                    //goperItem.Set("DATAEXTRA5", ListItensFaturar[i].DATAEXTRA5);
                    //goperItem.Set("DATAEXTRA6", ListItensFaturar[i].DATAEXTRA6);
                    //goperItem.Set("CAMPOVALOR1", Convert.ToDecimal(ListItensFaturar[i].CAMPOVALOR1));
                    //goperItem.Set("CAMPOVALOR2", Convert.ToDecimal(ListItensFaturar[i].CAMPOVALOR2));
                    //goperItem.Set("CAMPOVALOR3", Convert.ToDecimal(ListItensFaturar[i].CAMPOVALOR3));
                    //goperItem.Set("CAMPOVALOR4", Convert.ToDecimal(ListItensFaturar[i].CAMPOVALOR4));
                    //goperItem.Set("CAMPOVALOR5", Convert.ToDecimal(ListItensFaturar[i].CAMPOVALOR5));
                    //goperItem.Set("CAMPOVALOR6", Convert.ToDecimal(ListItensFaturar[i].CAMPOVALOR6));
                    //goperItem.Set("QUANTIDADE", Convert.ToDecimal(ListItensFaturar[i].QTD_FATURAR));
                    //goperItem.Set("QUANTIDADE_SALDO", Convert.ToDecimal(ListItensFaturar[i].QTD_FATURAR));
                    //goperItem.Set("QUANTIDADE_FATURADO", 0);
                    goperItem.Set("APLICACAOMATERIAL", ListItensFaturar[i].APLICACAOMATERIAL);
                    goperItem.Set("TIPODESCONTO", ListItensFaturar[i].TIPODESCONTO);
                    goperItem.Set("CODUNIDCONTROLE", ListItensFaturar[i].CODUNIDCONTROLE);
                    goperItem.Set("QUANTIDADECONTROLE", ListItensFaturar[i].QUANTIDADECONTROLE);
                    goperItem.Set("FATORCONVERSAO", ListItensFaturar[i].FATORCONVERSAO);

                    if (ListItensFaturar[i].DATAENTREGA != null)
                    {
                        goperItem.Set("DATAENTREGA", ListItensFaturar[i].DATAENTREGA);
                    }
                    goperItem.Insert();

                    conn.ExecQuery(@"INSERT INTO GOPERITEMCOPIAREF (CODEMPRESA, CODOPERDESTINO, NSEQITEMDESTINO, QUANTIDADE, CODOPERORIGEM, NSEQITEMORIGEM) VALUES (?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, goper.Get("CODOPER"), goperItem.Get("NSEQITEM"), ListItensFaturar[i].QTD_FATURAR, codoper, ListItensFaturar[i].NSEQITEM });

                }

                #endregion
                conn.ExecTransaction(@"UPDATE VSERIE SET NUMSEQ = ? WHERE CODSERIE = ? AND CODEMPRESA = ? AND CODFILIAL = ?", new object[] { goper.Get("NUMERO"), goper.Get("CODSERIE"), AppLib.Context.Empresa, codFilial });
                conn.ExecTransaction(@"UPDATE GLOG SET IDLOG = ? WHERE CODTABELA = ? AND CODEMPRESA = ?", new object[] { goper.Get("CODOPER"), "GOPER", AppLib.Context.Empresa });

                //Cria o relacionamento
                conn.ExecQuery(@"INSERT INTO GOPERRELAC (CODEMPRESA, CODOPER, CODOPERRELAC) VALUES (?,?,?)", new object[] { AppLib.Context.Empresa, codoper, goper.Get("CODOPER") });

                //


                AlteraValoresStatusItens(conn, Convert.ToInt32(goper.Get("CODOPER")));

                verificaStatusOrigem(conn);

                conn.Commit();

                //  new PSPartOperacaoEdit().geraFinanceiro(operacao.CODTIPOPER, Convert.ToInt32(operacao.CODOPER));

                return Convert.ToInt32(goper.Get("CODOPER"));
            }
            catch (Exception)
            {
                //cancelar = true;

                conn.Rollback();
                return 0;
            }

        }

        private void verificaStatusOrigem(AppLib.Data.Connection conn)
        {
            bool status = true;
            for (int i = 0; i < ListItensFaturar.Count; i++)
            {
                DataTable dt = conn.ExecQuery("SELECT QUANTIDADE_SALDO FROM GOPERITEM WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { ListItensFaturar[i].CODOPER, AppLib.Context.Empresa });
                for (int iDt = 0; iDt < dt.Rows.Count; iDt++)
                {
                    if (!dt.Rows[iDt]["QUANTIDADE_SALDO"].ToString().Equals("0,0000") && !dt.Rows[iDt]["QUANTIDADE_SALDO"].ToString().Equals("0"))
                    {
                        status = false;
                    }
                }
                if (status.Equals(true))
                {
                    conn.ExecTransaction("UPDATE GOPER SET CODSTATUS = ? WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { "1", ListItensFaturar[i].CODOPER, AppLib.Context.Empresa });
                }
            }
        }

        private void AlteraValoresStatusItens(AppLib.Data.Connection conn, int codoper)
        {
            for (int i = 0; i < ListItensFaturar.Count; i++)
            {
                decimal qtd = Convert.ToDecimal(conn.ExecGetField(0, "SELECT QUANTIDADE_FATURADO FROM GOPERITEM WHERE CODOPER = ?  AND CODEMPRESA = ? AND NSEQITEM = ?", new object[] { ListItensFaturar[i].CODOPER, AppLib.Context.Empresa, ListItensFaturar[i].NSEQITEM }));
                //Altera a quantidade dos itens de origem
                conn.ExecQuery(@"UPDATE GOPERITEM SET QUANTIDADE_SALDO = ?, QUANTIDADE_FATURADO = ? WHERE CODOPER = ? AND NSEQITEM = ? AND CODEMPRESA = ?", new object[] { ListItensFaturar[i].QTD_SALDO, ListItensFaturar[i].QTD_FATURAR + qtd, ListItensFaturar[i].CODOPER, ListItensFaturar[i].NSEQITEM, AppLib.Context.Empresa });
                //Altera o Status da operação de origem
                conn.ExecQuery(@"UPDATE GOPER SET CODSTATUS = ? WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { "0", ListItensFaturar[i].CODOPER, AppLib.Context.Empresa });

                //Cria o relacionamento item //
                conn.ExecQuery(@"INSERT INTO GOPERITEMRELAC (CODOPERITEMORIGEM, NSEQITEMORIGEM, CODOPERITEMDESTINO, NSEQITEMDESTINO) VALUES (?, ?, ?, ?)", new object[] { ListItensFaturar[i].CODOPER, ListItensFaturar[i].NSEQITEM, codoper, ListItensFaturar[i].NSEQDESTINO });


            }
        }

        private decimal CalculaTotalItem(string _tipoDesconto, decimal _VLUNITORIGINAL, decimal _quantidade, decimal vlAcrescimo, decimal vlDesconto)
        {
            decimal acrescimo = vlAcrescimo;
            decimal vlUnitario = 0;
            decimal desconto = vlDesconto;
            try
            {
                if (_tipoDesconto == "T")
                {
                    vlUnitario = (Convert.ToDecimal(_VLUNITORIGINAL) + acrescimo);

                    return (vlUnitario * _quantidade) - desconto;
                }
                else
                {

                    return (((_VLUNITORIGINAL - desconto) + acrescimo)) * _quantidade;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro a realizar o cálculo. Favor verificar.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }


        private void getParametroOperacao(AppLib.Data.Connection conn)
        {
            //Verifica se o CodTipOper está vazio, se não esiver, busca o parametro do destino, se não, verifica se pode ser faturado pra ele mesmo.
            if (!string.IsNullOrEmpty(codTipOperDestino))
            {
                DataTable dt = conn.ExecQuery(@"SELECT * FROM GTIPOPER WHERE GTIPOPER.CODEMPRESA = ? AND GTIPOPER.CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOperDestino });
                if (dt.Rows.Count > 0)
                {
                    parametroDestino = dt.Rows[0];
                }
            }
        }

        //private void alteraValoresParametroOperacao()
        //{
        //    //Verifica se o parametro de destino está vazio;
        //    //Se estiver retorna mensagem
        //    if (parametroDestino.Equals(null))
        //    {
        //        MessageBox.Show("Não foi possível carregar os parâmetros do Tipo da Operação [" + codTipOperDestino + "]", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    //Verfica se usa o número sequencial
        //    if (parametroDestino["USANUMEROSEQ"].ToString().Equals("1"))
        //    {
        //        operacao.NUMERO = new Class.CopiaOperacao().GeraNumeroDocumento(codTipOperDestino);
        //    }
        //    //Verifica a série
        //    if (parametroDestino["OPERSERIE"].ToString().Equals("M"))
        //    {
        //        operacao.CODSERIE = null;
        //    }
        //    else
        //    {
        //        operacao.CODSERIE = parametroDestino["SERIEDEFAULT"].ToString();
        //    }
        //    //Verifica o local
        //    if (parametroDestino["LOCAL1"].ToString().Equals("M"))
        //    {
        //        operacao.CODLOCAL = null;
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(operacao.CODLOCAL))
        //        {
        //            operacao.CODLOCAL = parametroDestino["LOCAL1DEFAULT"].ToString();
        //        }
        //    }
        //    //Verifica Local 2
        //    if (parametroDestino["LOCAL2"].ToString().Equals("M"))
        //    {
        //        operacao.CODLOCALENTREGA = null;
        //    }
        //    else
        //    {
        //        if (parametroDestino["LOCAL2DEFAULT"] != DBNull.Value)
        //        {
        //            operacao.CODLOCALENTREGA = parametroDestino["LOCAL2DEFAULT"].ToString();
        //        }
        //    }
        //    //Verifica o Cliente
        //    if (parametroDestino["CODCLIFOR"].ToString().Equals("M"))
        //    {
        //        operacao.CODCLIFOR = null;
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(operacao.CODCLIFOR))
        //        {
        //            operacao.CODCLIFOR = parametroDestino["CODCLIFORPADRAO"].ToString();
        //        }
        //    }
        //    //Verifica a Data de emissão
        //    if (parametroDestino["USADATAEMISSAO"].ToString() == "M")
        //    {
        //        operacao.DATAEMISSAO = null;
        //    }
        //    else
        //    {
        //        operacao.DATAEMISSAO = string.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now);
        //    }
        //    //Verifica a Data Entrada / Saída
        //    if (parametroDestino["USADATAENTSAI"].ToString() == "M")
        //    {
        //        operacao.DATAENTSAI = null;
        //    }
        //    else
        //    {
        //        operacao.DATAENTSAI = string.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now);
        //    }

        //    //Verifica Objeto,
        //    if (parametroDestino["USACAMPOOBJETO"].ToString().Equals("M"))
        //    {
        //        operacao.CODOBJETO = null;
        //    }
        //    //Verifica o Operador
        //    if (parametroDestino["USACAMPOOPERADOR"].ToString().Equals("M"))
        //    {
        //        operacao.CODOPERADOR = null;
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(operacao.CODOPERADOR))
        //        {

        //            if (string.IsNullOrEmpty(parametroDestino["CODOPERADORPADRAO"].ToString()))
        //            {
        //                operacao.CODOPERADOR = null;
        //            }
        //            else
        //            {
        //                operacao.CODOPERADOR = parametroDestino["CODOPERADORPADRAO"].ToString();
        //            }
        //        }
        //    }
        //    //Verifica Condição Pagamento
        //    if (parametroDestino["USACAMPOCONDPGTO"].ToString().Equals("M"))
        //    {
        //        operacao.CODCONDICAO = null;
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(operacao.CODCONDICAO))
        //        {
        //            operacao.CODCONDICAO = parametroDestino["CODCONDICAOPADRAO"].ToString();
        //        }
        //    }
        //    //Verifica a forma de pagto.
        //    if (parametroDestino["CODFORMA"].ToString().Equals("M"))
        //    {
        //        operacao.CODFORMA = null;
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(operacao.CODFORMA))
        //        {
        //            operacao.CODFORMA = parametroDestino["CODFORMAPADRAO"].ToString();
        //        }
        //    }
        //    //Verifica Conta Caixa
        //    if (parametroDestino["CODCONTA"].ToString().Equals("M"))
        //    {
        //        operacao.CODCONTA = null;
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(operacao.CODCONTA))
        //        {
        //            if (string.IsNullOrEmpty(parametroDestino["CODCONTAPADRAO"].ToString()))
        //            {
        //                operacao.CODCONTA = null;
        //            }
        //            else
        //            {
        //                operacao.CODCONTA = parametroDestino["CODCONTAPADRAO"].ToString();
        //            }

        //        }
        //    }
        //    //Verifica Representante
        //    if (parametroDestino["USACODREPRE"].ToString().Equals("M"))
        //    {
        //        operacao.CODREPRE = null;
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(operacao.CODREPRE))
        //        {
        //            operacao.CODREPRE = parametroDestino["CODREPREPADRAO"].ToString();
        //        }
        //    }
        //    //Verifica Centro de Custo
        //    if (parametroDestino["USACODCCUSTO"].ToString().Equals("M"))
        //    {
        //        operacao.CODCCUSTO = null;
        //    }
        //    //Verificar Natureza Orcamento
        //    if (parametroDestino["USACODNATUREZAORCAMENTO"].ToString().Equals("M"))
        //    {
        //        operacao.CODNATUREZAORCAMENTO = null;
        //    }
        //    //Verifica Observação e Historico
        //    if (!parametroDestino["USAABAOBSERV"].ToString().Equals("1"))
        //    {
        //        operacao.OBSERVACAO = null;
        //        operacao.HISTORICO = null;
        //    }
        //    //Verifica Transporte
        //    if (!parametroDestino["USAABATRANSP"].ToString().Equals("1"))
        //    {
        //        operacao.FRETECIFFOB = null;
        //        operacao.CODTRANSPORTADORA = null;
        //        operacao.QUANTIDADE_oper = 0;
        //        operacao.PESOLIQUIDO = 0;
        //        operacao.PESOBRUTO = 0;
        //        operacao.ESPECIE = null;
        //        operacao.MARCA = null;
        //    }
        //    //Verifica Frete
        //    if (parametroDestino["USAVALORFRETE"].ToString().Equals("M"))
        //    {
        //        operacao.VALORFRETE = 0;
        //        operacao.PERCFRETE = 0;
        //    }
        //    //Verifica Valor Desconto
        //    if (parametroDestino["USAVALORDESCONTO"].ToString().Equals("M"))
        //    {
        //        operacao.VALORDESCONTO = 0;
        //        operacao.PRDESCONTO_oper = 0;
        //    }
        //    //Verifica Valor Despesa
        //    if (parametroDestino["USAVALORDESPESA"].ToString().Equals("M"))
        //    {
        //        operacao.VALORDESPESA = 0;
        //        operacao.PERCDESPESA = 0;
        //    }
        //    //Verifica Valor Seguro
        //    if (parametroDestino["USAVALORSEGURO"].ToString().Equals("M"))
        //    {
        //        operacao.VALORSEGURO = 0;
        //        operacao.PERCSEGURO = 0;
        //    }

        //    //Alteração dos Valores
        //    operacao.VALORBRUTO = 0;
        //    operacao.VALORLIQUIDO = 0;
        //}

        //private void alteraValoresParametroItens()
        //{
        //    for (int i = 0; i < ListItensFaturar.Count; i++)
        //    {
        //        //Verifica valor unitário
        //        if (parametroDestino["USAVLUNITARIO"].ToString().Equals("M"))
        //        {
        //            ListItensFaturar[i].VLUNITARIO = 0;
        //        }
        //        //Verifica Valor Desconto
        //        if (parametroDestino["USAVLDESCONTO"].ToString().Equals("M"))
        //        {
        //            ListItensFaturar[i].VLDESCONTO = 0;
        //        }
        //        //Verifica o Valor PR Desconto
        //        if (parametroDestino["USAPRDESCONTO"].ToString().Equals("M"))
        //        {
        //            ListItensFaturar[i].PRDESCONTO = 0;
        //        }
        //        //Verifica o Valor Total
        //        if (parametroDestino["USAVLTOTALITEM"].ToString().Equals("M"))
        //        {
        //            ListItensFaturar[i].VLTOTALITEM = 0;
        //        }
        //        //Verifica a Natureza
        //        if (parametroDestino["USANATUREZA"].ToString().Equals("M"))
        //        {
        //            ListItensFaturar[i].CODNATUREZA = null;
        //        }
        //        else
        //        {
        //            if (string.IsNullOrEmpty(ListItensFaturar[i].CODNATUREZA))
        //            {
        //                ListItensFaturar[i].CODNATUREZA = new PSPartOperacaoData().DefineNatureza(Convert.ToInt32(operacao.CODEMPRESA), Convert.ToInt32(operacao.CODOPER), string.IsNullOrEmpty(parametroDestino["CODNATDENTRO"].ToString()) ? null : parametroDestino["CODNATDENTRO"].ToString(), (string.IsNullOrEmpty(parametroDestino["CODNATFORA"].ToString()) ? null : parametroDestino["CODNATFORA"].ToString()));
        //            }
        //        }


        //    }
        //}


    }
}
