using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.ERP.Comercial
{
    public partial class FormFaturaParcial : Form
    {
        public Class.Goper operacao = null;
        List<Class.GoperItem> listItens = null;
        List<Class.GoperItem> ListItensFaturar = new List<Class.GoperItem>();
        public string codTipOperDestino = string.Empty;
        public bool cancelar = false;
        private DataRow parametroDestino;
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        public int codFilial;

        public FormFaturaParcial(Class.Goper _operacao, List<Class.GoperItem> itens)
        {
            InitializeComponent();
            operacao = _operacao;
            listItens = itens;
            dataGridView1.DataSource = listItens;
            removeColunas();
            RenomearColunas();
            txtCliente.Text = getNomeCliente();
            DesabilitaColunas();
        }

        private void RenomearColunas()
        {
            dataGridView1.Columns["CODOPER"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = ? AND COLUNA = ?", new object[] { "GOPER", "GOPER.CODOPER" }).ToString();
            dataGridView1.Columns["NUMERO"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = ? AND COLUNA = ?", new object[] { "GOPER", "GOPER.NUMERO" }).ToString();
            dataGridView1.Columns["NSEQITEM"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = ? AND COLUNA = ?", new object[] { "GOPERITEM", "GOPERITEM.NSEQITEM" }).ToString();
            dataGridView1.Columns["CODPRODUTO"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = ? AND COLUNA = ?", new object[] { "GOPERITEM", "GOPERITEM.CODPRODUTO" }).ToString();
            dataGridView1.Columns["CODIGOAUXILIAR"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = ? AND COLUNA = ?", new object[] { "VPRODUTO", "VPRODUTO.CODIGOAUXILIAR" }).ToString();
            dataGridView1.Columns["DESCRICAOPRODUTO"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = ? AND COLUNA = ?", new object[] { "VPRODUTO", "VPRODUTO.DESCRICAO" }).ToString();
            dataGridView1.Columns["QTD_ORIGINAL"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = ? AND COLUNA = ?", new object[] { "GOPERITEM", "GOPERITEM.QUANTIDADE" }).ToString();
            dataGridView1.Columns["QTD_SALDO"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = ? AND COLUNA = ?", new object[] { "GOPERITEM", "GOPERITEM.QUANTIDADE_SALDO" }).ToString();
            dataGridView1.Columns["QTD_FATURAR"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = ? AND COLUNA = ?", new object[] { "GOPERITEM", "GOPERITEM.QTD_FATURAR" }).ToString();
            dataGridView1.Columns["CODUNIDOPER"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = ? AND COLUNA = ?", new object[] { "GOPERITEM", "GOPERITEM.CODUNIDOPER" }).ToString();
        }

        private void DesabilitaColunas()
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (dataGridView1.Columns[i].HeaderText != "Qtd. Faturar")
                {
                    if (dataGridView1.Columns[i].HeaderText == "Selecionar")
                    {
                        continue;
                    }
                    dataGridView1.Columns[i].ReadOnly = true;
                }
            }
        }

        private string getNomeCliente()
        {
            return dbs.QueryValue(string.Empty, "SELECT NOMEFANTASIA FROM VCLIFOR WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { operacao.CODCLIFOR, operacao.CODEMPRESA }).ToString();
        }

        private void removeColunas()
        {
            dataGridView1.Columns["CAMPOLIVRE1"].Visible = false;
            dataGridView1.Columns["CAMPOLIVRE2"].Visible = false;
            dataGridView1.Columns["CAMPOLIVRE3"].Visible = false;
            dataGridView1.Columns["CAMPOLIVRE4"].Visible = false;
            dataGridView1.Columns["CAMPOLIVRE5"].Visible = false;
            dataGridView1.Columns["CAMPOLIVRE6"].Visible = false;

            dataGridView1.Columns["DATAEXTRA1"].Visible = false;
            dataGridView1.Columns["DATAEXTRA2"].Visible = false;
            dataGridView1.Columns["DATAEXTRA3"].Visible = false;
            dataGridView1.Columns["DATAEXTRA4"].Visible = false;
            dataGridView1.Columns["DATAEXTRA5"].Visible = false;
            dataGridView1.Columns["DATAEXTRA6"].Visible = false;

            dataGridView1.Columns["CAMPOVALOR1"].Visible = false;
            dataGridView1.Columns["CAMPOVALOR2"].Visible = false;
            dataGridView1.Columns["CAMPOVALOR3"].Visible = false;
            dataGridView1.Columns["CAMPOVALOR4"].Visible = false;
            dataGridView1.Columns["CAMPOVALOR5"].Visible = false;
            dataGridView1.Columns["CAMPOVALOR6"].Visible = false;

            dataGridView1.Columns["VLUNITARIO"].Visible = false;
            dataGridView1.Columns["PRDESCONTO"].Visible = false;
            dataGridView1.Columns["VLTOTALITEM"].Visible = false;
            dataGridView1.Columns["VLDESCONTO"].Visible = false;
            dataGridView1.Columns["CODCFOP"].Visible = false;
            dataGridView1.Columns["CODNATUREZA"].Visible = false;
            dataGridView1.Columns["OBSERVACAO"].Visible = false;
            dataGridView1.Columns["INFCOMPL"].Visible = false;
            dataGridView1.Columns["CODTABPRECO"].Visible = false;
            dataGridView1.Columns["NSEQDESTINO"].Visible = false;

            dataGridView1.Columns["CODEMPRESA"].Visible = false;
            dataGridView1.Columns["QUANTIDADE"].Visible = false;
            dataGridView1.Columns["QUANTIDADE_FATURADO"].Visible = false;
            dataGridView1.Columns["QUANTIDADE_SALDO"].Visible = false;
            dataGridView1.Columns["NACIONALFEDERALIBPTAX"].Visible = false;
            dataGridView1.Columns["IMPORTADOSFEDERALIBPTAX"].Visible = false;
            dataGridView1.Columns["ESTADUALIBPTAX"].Visible = false;
            dataGridView1.Columns["MUNICIPALIBPTAX"].Visible = false;
            dataGridView1.Columns["CHAVEIBPTAX"].Visible = false;
            dataGridView1.Columns["APLICACAOMATERIAL"].Visible = false;
            dataGridView1.Columns["VLACRESCIMO"].Visible = false;
            dataGridView1.Columns["PRACRESCIMO"].Visible = false;
            dataGridView1.Columns["TIPODESCONTO"].Visible = false;
            dataGridView1.Columns["VLUNITORIGINAL"].Visible = false;
            dataGridView1.Columns["TOTALEDITADO"].Visible = false;
            dataGridView1.Columns["RATEIODESPESA"].Visible = false;
            dataGridView1.Columns["RATEIODESCONTO"].Visible = false;
            dataGridView1.Columns["RATEIOFRETE"].Visible = false;
            dataGridView1.Columns["RATEIOSEGURO"].Visible = false;
            dataGridView1.Columns["DATAENTREGA"].Visible = false;
            dataGridView1.Columns["NOMEPRODUTO"].Visible = false;
            dataGridView1.Columns["UFIBPTAX"].Visible = false;
            dataGridView1.Columns["XPED"].Visible = false;
            dataGridView1.Columns["NITEMPED"].Visible = false;
            dataGridView1.Columns["NUMERODI"].Visible = false;
            dataGridView1.Columns["DATADI"].Visible = false;
            dataGridView1.Columns["LOCDESEMB"].Visible = false;
            dataGridView1.Columns["UFDESEMB"].Visible = false;
            dataGridView1.Columns["DATADESEMB"].Visible = false;
            dataGridView1.Columns["CODEXPORTADOR"].Visible = false;
            dataGridView1.Columns["NUMADICAO"].Visible = false;
            dataGridView1.Columns["NUMSEQADIC"].Visible = false;
            dataGridView1.Columns["CODFABRICANTE"].Visible = false;
            dataGridView1.Columns["VLORDESCDI"].Visible = false;
            dataGridView1.Columns["TPVIATRANSP"].Visible = false;
            dataGridView1.Columns["VAFRMM"].Visible = false;
            dataGridView1.Columns["TPINTERMEDIO"].Visible = false;
            dataGridView1.Columns["CNPJ"].Visible = false;
            dataGridView1.Columns["UFTERCEIRO"].Visible = false;
            dataGridView1.Columns["NDRAW"].Visible = false;
            dataGridView1.Columns["IBPTAX"].Visible = false;
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
            else
            {
                DataTable dt = conn.ExecQuery(@"SELECT * FROM GTIPOPER WHERE GTIPOPER.CODEMPRESA = ? AND GTIPOPER.CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, operacao.CODTIPOPER });
                if (dt.Rows.Count > 0)
                {
                    parametroDestino = dt.Rows[0];
                }
            }
        }

        private void btnFaturar_Click(object sender, EventArgs e)
        {
            // Variável que define que o botão Cancelar do fomrulário de Operações será inativo.
            bool Cancela = false;
            ListItensFaturar = new List<Class.GoperItem>();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                try
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.Equals(true))
                    {
                        decimal ori = Convert.ToDecimal(listItens[i].QTD_ORIGINAL);
                        decimal saldo = Convert.ToDecimal(listItens[i].QTD_SALDO);
                        decimal faturado = Convert.ToDecimal(listItens[i].QTD_FATURAR);
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
                        listItens[i].QTD_SALDO = saldo - faturado;
                        ListItensFaturar.Add(listItens[i]);
                    }
                }
                catch (Exception ex)
                {

                }
            }

            if (ListItensFaturar.Count > 0)
            {
                int retorno = faturarParcial();
                if (retorno != 0)
                {
                    PSPartOperacaoEdit frm = new PSPartOperacaoEdit();
                    frm.codFilial = codFilial;
                    frm.codoper = Convert.ToInt32(retorno);

                    frm.btnFechar.Enabled = false;
                    frm.edita = true;
                    frm.faturamento = true;
                    frm.VerificaCancela = Cancela;
                    frm.FaturamentoOperacao = true;
                    frm.ShowDialog();
                    this.Dispose();
                }
                //else
                //{
                //    MessageBox.Show("Não foi possível abrir a tela de operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}

            }
            else
            {
                MessageBox.Show("Favor selecionar pelo menos um item para realizar o faturamento parcial.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cancelar = true;
            }
        }

        private int faturarParcial()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();
            try
            {
                getParametroOperacao(conn);
                Class.CopiaOperacao copia = new Class.CopiaOperacao(codFilial);

                copia.excluiFinanceiro(conn, Convert.ToInt32(operacao.CODOPER));
                alteraValoresParametroOperacao();


                AppLib.ORM.Jit goper = new AppLib.ORM.Jit(conn, "GOPER");

                goper.Set("CODEMPRESA", operacao.CODEMPRESA);
                goper.Set("CODOPER", operacao.CODOPER);
                goper.Select();
                goper.Set("CODOPER", Convert.ToInt32(conn.ExecGetField(0, @"SELECT IDLOG FROM GLOG WHERE CODTABELA = ?", new object[] { "GOPER" })) + 1);
                copia.CodOperNum = operacao.CODOPER.ToString();
                goper.Set("NUMERO", copia.GeraNumeroDocumento(codTipOperDestino));
                goper.Set("CODTIPOPER", codTipOperDestino);
                goper.Set("CODSERIE", operacao.CODSERIE);
                goper.Set("CODLOCAL", operacao.CODLOCAL);
                goper.Set("CODLOCALENTREGA", operacao.CODLOCALENTREGA);
                goper.Set("CODCLIFOR", operacao.CODCLIFOR);
                goper.Set("DATAEMISSAO", Convert.ToDateTime(operacao.DATAEMISSAO));
                goper.Set("DATAENTSAI", Convert.ToDateTime(operacao.DATAENTSAI));
                goper.Set("CODOBJETO", operacao.CODOBJETO);
                goper.Set("CODOPERADOR", operacao.CODOPERADOR);
                goper.Set("CODCONDICAO", string.IsNullOrEmpty(operacao.CODCONDICAO) ? null : operacao.CODCONDICAO);
                goper.Set("CODFORMA", operacao.CODFORMA);
                goper.Set("CODCONTA", operacao.CODCONTA);
                goper.Set("CODREPRE", string.IsNullOrEmpty(operacao.CODREPRE) ? null : operacao.CODREPRE);
                goper.Set("CODSTATUS", 0);
                goper.Set("DATACRIACAO", conn.GetDateTime());
                goper.Set("DATAALTERACAO", conn.GetDateTime());
                goper.Set("USUARIOALTERACAO", AppLib.Context.Usuario);
                goper.Set("CODUSUARIOCRIACAO", AppLib.Context.Usuario);
                goper.Set("NFE", null);
                goper.Set("CHAVENFE", null);
                goper.Set("NOMEFANTASIA", operacao.NOMEFANTASIA);
                if (goper.Get("CODFORMA").ToString() == string.Empty)
                {
                    goper.Set("CODFORMA", null);
                }
                goper.Insert();

                #region GOPERITEM
                alteraValoresParametroItens();
                for (int i = 0; i < ListItensFaturar.Count; i++)
                {
                    AppLib.ORM.Jit goperItem = new AppLib.ORM.Jit(conn, "GOPERITEM");
                    goperItem.Set("CODEMPRESA", operacao.CODEMPRESA);
                    goperItem.Set("CODOPER", goper.Get("CODOPER"));
                    goperItem.Set("NSEQITEM", i + 1);
                    goperItem.Set("CODPRODUTO", ListItensFaturar[i].CODPRODUTO);
                    goperItem.Set("QUANTIDADE", ListItensFaturar[i].QTD_FATURAR);
                    goperItem.Set("CODTABPRECO", ListItensFaturar[i].CODTABPRECO);
                    goperItem.Set("VLUNITARIO", ListItensFaturar[i].VLUNITARIO);
                    goperItem.Set("VLUNITORIGINAL", ListItensFaturar[i].VLUNITORIGINAL);

                    goperItem.Set("VLACRESCIMO", Convert.ToDecimal(ListItensFaturar[i].VLACRESCIMO));//calculaAcrescimo( Convert.ToDecimal(ListItensFaturar[i].VLACRESCIMO), Convert.ToDecimal(ListItensFaturar[i].PRACRESCIMO), Convert.ToDecimal(ListItensFaturar[i].VLUNITORIGINAL)));

                    goperItem.Set("VLDESCONTO", Convert.ToDecimal(ListItensFaturar[i].VLDESCONTO));//calculaDesconto(ListItensFaturar[i].TIPODESCONTO, Convert.ToDecimal(ListItensFaturar[i].VLDESCONTO), Convert.ToDecimal(ListItensFaturar[i].PRDESCONTO),  Convert.ToDecimal(ListItensFaturar[i].VLUNITORIGINAL), Convert.ToDecimal(ListItensFaturar[i].QTD_FATURAR)));

                    goperItem.Set("PRDESCONTO", ListItensFaturar[i].PRDESCONTO);

                    goperItem.Set("VLTOTALITEM", CalculaTotalItem(ListItensFaturar[i].TIPODESCONTO, Convert.ToDecimal(ListItensFaturar[i].VLUNITORIGINAL), Convert.ToDecimal(ListItensFaturar[i].QTD_FATURAR), Convert.ToDecimal(goperItem.Get("VLACRESCIMO")), Convert.ToDecimal(goperItem.Get("VLDESCONTO"))));

                    goperItem.Set("CODNATUREZA", ListItensFaturar[i].CODNATUREZA);
                    goperItem.Set("OBSERVACAO", ListItensFaturar[i].OBSERVACAO);
                    goperItem.Set("INFCOMPL", ListItensFaturar[i].INFCOMPL);
                    goperItem.Set("CODUNIDOPER", ListItensFaturar[i].CODUNIDOPER);
                    goperItem.Set("CAMPOLIVRE1", ListItensFaturar[i].CAMPOLIVRE1);
                    goperItem.Set("CAMPOLIVRE2", ListItensFaturar[i].CAMPOLIVRE2);
                    goperItem.Set("CAMPOLIVRE3", ListItensFaturar[i].CAMPOLIVRE3);
                    goperItem.Set("CAMPOLIVRE4", ListItensFaturar[i].CAMPOLIVRE4);
                    goperItem.Set("CAMPOLIVRE5", ListItensFaturar[i].CAMPOLIVRE5);
                    goperItem.Set("CAMPOLIVRE6", ListItensFaturar[i].CAMPOLIVRE6);
                    goperItem.Set("DATAEXTRA1", ListItensFaturar[i].DATAEXTRA1);
                    goperItem.Set("DATAEXTRA2", ListItensFaturar[i].DATAEXTRA2);
                    goperItem.Set("DATAEXTRA3", ListItensFaturar[i].DATAEXTRA3);
                    goperItem.Set("DATAEXTRA4", ListItensFaturar[i].DATAEXTRA4);
                    goperItem.Set("DATAEXTRA5", ListItensFaturar[i].DATAEXTRA5);
                    goperItem.Set("DATAEXTRA6", ListItensFaturar[i].DATAEXTRA6);
                    goperItem.Set("CAMPOVALOR1", Convert.ToDecimal(ListItensFaturar[i].CAMPOVALOR1));
                    goperItem.Set("CAMPOVALOR2", Convert.ToDecimal(ListItensFaturar[i].CAMPOVALOR2));
                    goperItem.Set("CAMPOVALOR3", Convert.ToDecimal(ListItensFaturar[i].CAMPOVALOR3));
                    goperItem.Set("CAMPOVALOR4", Convert.ToDecimal(ListItensFaturar[i].CAMPOVALOR4));
                    goperItem.Set("CAMPOVALOR5", Convert.ToDecimal(ListItensFaturar[i].CAMPOVALOR5));
                    goperItem.Set("CAMPOVALOR6", Convert.ToDecimal(ListItensFaturar[i].CAMPOVALOR6));
                    goperItem.Set("QUANTIDADE", Convert.ToDecimal(ListItensFaturar[i].QTD_FATURAR));
                    goperItem.Set("QUANTIDADE_SALDO", Convert.ToDecimal(ListItensFaturar[i].QTD_FATURAR));
                    goperItem.Set("QUANTIDADE_FATURADO", 0);
                    goperItem.Set("APLICACAOMATERIAL", ListItensFaturar[i].APLICACAOMATERIAL);
                    goperItem.Set("TIPODESCONTO", ListItensFaturar[i].TIPODESCONTO);
                    goperItem.Set("CODUNIDCONTROLE", ListItensFaturar[i].CODUNIDCONTROLE);
                    goperItem.Set("FATORCONVERSAO", ListItensFaturar[i].FATORCONVERSAO);

                    decimal QuantidadeControle = ListItensFaturar[i].QTD_FATURAR * ListItensFaturar[i].FATORCONVERSAO;

                    goperItem.Set("QUANTIDADECONTROLE", QuantidadeControle);

                    if (ListItensFaturar[i].DATAENTREGA != null)
                    {
                        goperItem.Set("DATAENTREGA", ListItensFaturar[i].DATAENTREGA);
                    }
                    goperItem.Insert();
                }

                #endregion

                string codSerie = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODSERIE FROM VTIPOPERSERIE where CODTIPOPER = ? and CODFILIAL = ? and PRINCIPAL =  1 AND CODEMPRESA = ?", new object[] { codTipOperDestino, codFilial, AppLib.Context.Empresa }).ToString();

                conn.ExecTransaction(@"UPDATE VSERIE SET NUMSEQ = ? WHERE CODSERIE = ? AND CODEMPRESA = ? AND CODFILIAL = ?", new object[] { goper.Get("NUMERO"), codSerie, operacao.CODEMPRESA, codFilial });
                conn.ExecTransaction(@"UPDATE GLOG SET IDLOG = ? WHERE CODTABELA = ? AND CODEMPRESA = ?", new object[] { goper.Get("CODOPER"), "GOPER", operacao.CODEMPRESA });

                //Cria o relacionamento
                conn.ExecQuery(@"INSERT INTO GOPERRELAC (CODEMPRESA, CODOPER, CODOPERRELAC) VALUES (?,?,?)", new object[] { operacao.CODEMPRESA, operacao.CODOPER, goper.Get("CODOPER") });


                AlteraValoresStatusItens(conn, Convert.ToInt32(goper.Get("CODOPER")));

                verificaStatusOrigem(conn);
                conn.Commit();

                new PSPartOperacaoEdit().geraFinanceiro(operacao.CODTIPOPER, Convert.ToInt32(operacao.CODOPER));

                return Convert.ToInt32(goper.Get("CODOPER"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cancelar = true;

                conn.Rollback();
                return 0;
            }
        }

        private decimal calculaAcrescimo(decimal vlAcrescimo, decimal prAcrescimo, decimal vlUnitarioOriginal)
        {
            decimal vl = 0, pr = 0;
            vl = vlAcrescimo;
            pr = prAcrescimo;
            //Preenchendo os campos
            if (vl == 0 && pr != 0)
            {
                return ((pr * vlUnitarioOriginal) / 100);
            }
            else
            {
                if (vl != 0)
                {
                    return ((vl / vlUnitarioOriginal) * 100);
                }
                return 0;
            }
        }

        private decimal calculaDesconto(string tipo, decimal vlDesconto, decimal prDesconto, decimal vlUnitarioOriginal, decimal quantidade)
        {
            decimal vl = 0, pr = 0;
            vl = vlDesconto;
            pr = prDesconto;

            //Preenchendo os campos
            if (tipo == "U")
            {
                //Preenchendo os campos
                if (vl == 0)
                {
                    return ((pr * vlUnitarioOriginal) / 100);

                }
                else
                {
                    return ((vl / vlUnitarioOriginal) * 100);
                }
            }
            else
            {

                if (vl == 0)
                {
                    return (pr * vlUnitarioOriginal * quantidade) / 100;
                }
                else
                {
                    return (vl / (vlUnitarioOriginal * quantidade) * 100);
                }

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

        private void AlteraValoresStatusItens(AppLib.Data.Connection conn, int codoper)
        {
            for (int i = 0; i < ListItensFaturar.Count; i++)
            {
                decimal qtd = Convert.ToDecimal(conn.ExecGetField(0, "SELECT QUANTIDADE_FATURADO FROM GOPERITEM WHERE CODOPER = ?  AND CODEMPRESA = ? AND NSEQITEM = ?", new object[] { ListItensFaturar[i].CODOPER, operacao.CODEMPRESA, ListItensFaturar[i].NSEQITEM }));
                //Altera a quantidade dos itens de origem
                conn.ExecQuery(@"UPDATE GOPERITEM SET QUANTIDADE_SALDO = ?, QUANTIDADE_FATURADO = ? WHERE CODOPER = ? AND NSEQITEM = ? AND CODEMPRESA = ?", new object[] { ListItensFaturar[i].QTD_SALDO, ListItensFaturar[i].QTD_FATURAR + qtd, ListItensFaturar[i].CODOPER, ListItensFaturar[i].NSEQITEM, operacao.CODEMPRESA });
                //Altera o Status da operação de origem
                conn.ExecQuery(@"UPDATE GOPER SET CODSTATUS = ? WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { "5", ListItensFaturar[i].CODOPER, operacao.CODEMPRESA });

                //Cria o relacionamento item //
                conn.ExecQuery(@"INSERT INTO GOPERITEMRELAC (CODOPERITEMORIGEM, NSEQITEMORIGEM, CODOPERITEMDESTINO, NSEQITEMDESTINO) VALUES (?, ?, ?, ?)", new object[] { ListItensFaturar[i].CODOPER, ListItensFaturar[i].NSEQITEM, codoper, i + 1 });
            }
        }

        private int InseriOperacao(AppLib.Data.Connection conn)
        {
            try
            {
                #region Atributos
                AppLib.ORM.Jit goper = new AppLib.ORM.Jit(conn, "GOPER");
                goper.Set("CODEMPRESA", AppLib.Context.Empresa);
                goper.Set("CODOPER", getMaxOper("GOPER"));
                goper.Set("CODTIPOPER", codTipOperDestino);
                goper.Set("NUMERO", operacao.NUMERO);
                goper.Set("CODCLIFOR", operacao.CODCLIFOR);
                goper.Set("CODFILIAL", operacao.CODFILIAL);
                goper.Set("CODSERIE", operacao.CODSERIE);
                goper.Set("CODLOCAL", operacao.CODLOCAL);
                goper.Set("CODLOCALENTREGA", operacao.CODLOCALENTREGA);
                goper.Set("CODOBJETO", operacao.CODOBJETO);
                goper.Set("CODOPERADOR", operacao.CODOPERADOR);
                goper.Set("CODSTATUS", operacao.CODSTATUS);
                //goper.Set("DATAEMISSAO", Convert.ToDateTime(operacao.DATAEMISSAO));
                //goper.Set("DATAENTSAI", Convert.ToDateTime(operacao.DATAENTSAI));
                goper.Set("FRETECIFFOB", operacao.FRETECIFFOB);
                goper.Set("CODTRANSPORTADORA", operacao.CODTRANSPORTADORA);
                goper.Set("OBSERVACAO", operacao.OBSERVACAO);
                goper.Set("HISTORICO", operacao.HISTORICO);
                goper.Set("CODCONDICAO", operacao.CODCONDICAO);
                goper.Set("CODFORMA", operacao.CODFORMA);
                goper.Set("VALORBRUTO", operacao.VALORBRUTO);
                goper.Set("VALORLIQUIDO", operacao.VALORLIQUIDO);
                goper.Set("QUANTIDADE", operacao.QUANTIDADE);
                goper.Set("PESOLIQUIDO", operacao.PESOLIQUIDO);
                goper.Set("PESOBRUTO", operacao.PESOBRUTO);
                goper.Set("ESPECIE", operacao.ESPECIE);
                goper.Set("MARCA", operacao.MARCA);
                goper.Set("CODCONTA", operacao.CODCONTA);
                goper.Set("CODREPRE", operacao.CODREPRE);
                goper.Set("CODCCUSTO", operacao.CODCCUSTO);
                goper.Set("CODNATUREZAORCAMENTO", operacao.CODNATUREZAORCAMENTO);
                goper.Set("PLACA", operacao.PLACA);
                goper.Set("UFPLACA", operacao.UFPLACA);
                // goper.Set("DATACRIACAO", Convert.ToDateTime(operacao.DATACRIACAO));
                // goper.Set("DATACRIACAO", Convert.ToDateTime(operacao.DATAFATURAMENTO));
                goper.Set("VALORFRETE", operacao.VALORFRETE);
                goper.Set("PERCFRETE", operacao.PERCFRETE);
                goper.Set("VALORDESCONTO", operacao.VALORDESCONTO);
                goper.Set("PERCDESCONTO", operacao.PERCDESCONTO);
                goper.Set("VALORDESPESA", operacao.VALORDESPESA);
                goper.Set("PERCDESPESA", operacao.PERCDESPESA);
                goper.Set("VALORSEGURO", operacao.VALORSEGURO);
                goper.Set("PERCSEGURO", operacao.PERCSEGURO);
                goper.Set("PRCOMISSAO", operacao.PRCOMISSAO);
                goper.Insert();

                #endregion

                return Convert.ToInt32(goper.Get("CODOPER"));
            }
            catch (Exception)
            {
                throw new Exception("Erro ao inserir a operação.");
            }

        }

        private int getMaxOper(string tabela)
        {
            return Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT IDLOG FROM GLOG WHERE CODTABELA = ?", new object[] { tabela })) + 1;
        }

        private void ExcluiPrevisao()
        {
            if (parametroDestino["GERAFINANCEIRO"].ToString().Equals("1"))
            {
                if (!string.IsNullOrEmpty(operacao.CODCONDICAO))
                {
                    PSPartLancaData psPartLancaData = new PSPartLancaData();
                    psPartLancaData._tablename = "FLANCA";
                    psPartLancaData._keys = new string[] { "CODEMPRESA", "CODLANCA" };

                    System.Data.DataTable dtLancaOri = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODLANCA FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ?", operacao.CODEMPRESA, operacao.CODOPER);
                    foreach (DataRow row in dtLancaOri.Rows)
                    {
                        DataTable dtLanca = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", operacao.CODEMPRESA, row["CODLANCA"]);
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
        }

        private void InseriItens()
        {
            try
            {


                for (int i = 0; i < ListItensFaturar.Count; i++)
                {
                    PS.Lib.DataField OP_ITEMCODEMPRESA = new PS.Lib.DataField("CODEMPRESA", operacao.CODEMPRESA);
                    PS.Lib.DataField OP_ITEMCODOPER = new PS.Lib.DataField("CODOPER", operacao.CODOPER);
                    PS.Lib.DataField OP_ITEMNSEQITEM = new PS.Lib.DataField("NSEQITEM", null, typeof(int), PS.Lib.Global.TypeAutoinc.Max);
                    PS.Lib.DataField OP_ITEMCODPRODUTO = new PS.Lib.DataField("CODPRODUTO", ListItensFaturar[i].CODPRODUTO);
                    PS.Lib.DataField OP_ITEMQUANTIDADE = new PS.Lib.DataField("QUANTIDADE", ListItensFaturar[i].QTD_FATURAR);
                    PS.Lib.DataField OP_ITEMCODTABPRECO = new PS.Lib.DataField("CODTABPRECO", ListItensFaturar[i].CODTABPRECO);
                    PS.Lib.DataField OP_ITEMVLUNITARIO = new PS.Lib.DataField("VLUNITARIO", ListItensFaturar[i].VLUNITARIO);
                    PS.Lib.DataField OP_ITEMVLDESCONTO = new PS.Lib.DataField("VLDESCONTO", ListItensFaturar[i].VLDESCONTO);
                    PS.Lib.DataField OP_ITEMPRDESCONTO = new PS.Lib.DataField("PRDESCONTO", ListItensFaturar[i].PRDESCONTO);
                    PS.Lib.DataField OP_ITEMVLTOTALITEM = new PS.Lib.DataField("VLTOTALITEM", ListItensFaturar[i].VLTOTALITEM);
                    PS.Lib.DataField OP_ITEMCODNATUREZA = new PS.Lib.DataField("CODNATUREZA", ListItensFaturar[i].CODNATUREZA);
                    PS.Lib.DataField OP_ITEMOBSERVACAO = new PS.Lib.DataField("OBSERVACAO", ListItensFaturar[i].OBSERVACAO);
                    PS.Lib.DataField OP_ITEMINFCOMPL = new PS.Lib.DataField("INFCOMPL", ListItensFaturar[i].INFCOMPL);
                    PS.Lib.DataField OP_ITEMCODUNIDOPER = new PS.Lib.DataField("CODUNIDOPER", ListItensFaturar[i].CODUNIDOPER);
                    PS.Lib.DataField OP_ITEMCAMPOLIVRE1 = new PS.Lib.DataField("CAMPOLIVRE1", ListItensFaturar[i].CAMPOLIVRE1);
                    PS.Lib.DataField OP_ITEMCAMPOLIVRE2 = new PS.Lib.DataField("CAMPOLIVRE2", ListItensFaturar[i].CAMPOLIVRE2);
                    PS.Lib.DataField OP_ITEMCAMPOLIVRE3 = new PS.Lib.DataField("CAMPOLIVRE3", ListItensFaturar[i].CAMPOLIVRE3);
                    PS.Lib.DataField OP_ITEMCAMPOLIVRE4 = new PS.Lib.DataField("CAMPOLIVRE4", ListItensFaturar[i].CAMPOLIVRE4);
                    PS.Lib.DataField OP_ITEMCAMPOLIVRE5 = new PS.Lib.DataField("CAMPOLIVRE5", ListItensFaturar[i].CAMPOLIVRE5);
                    PS.Lib.DataField OP_ITEMCAMPOLIVRE6 = new PS.Lib.DataField("CAMPOLIVRE6", ListItensFaturar[i].CAMPOLIVRE6);
                    PS.Lib.DataField OP_ITEMDATAEXTRA1 = new PS.Lib.DataField("DATAEXTRA1", ListItensFaturar[i].DATAEXTRA1);
                    PS.Lib.DataField OP_ITEMDATAEXTRA2 = new PS.Lib.DataField("DATAEXTRA2", ListItensFaturar[i].DATAEXTRA2);
                    PS.Lib.DataField OP_ITEMDATAEXTRA3 = new PS.Lib.DataField("DATAEXTRA3", ListItensFaturar[i].DATAEXTRA3);
                    PS.Lib.DataField OP_ITEMDATAEXTRA4 = new PS.Lib.DataField("DATAEXTRA4", ListItensFaturar[i].DATAEXTRA4);
                    PS.Lib.DataField OP_ITEMDATAEXTRA5 = new PS.Lib.DataField("DATAEXTRA5", ListItensFaturar[i].DATAEXTRA5);
                    PS.Lib.DataField OP_ITEMDATAEXTRA6 = new PS.Lib.DataField("DATAEXTRA6", ListItensFaturar[i].DATAEXTRA6);
                    PS.Lib.DataField OP_ITEMCAMPOVALOR1 = new PS.Lib.DataField("CAMPOVALOR1", ListItensFaturar[i].CAMPOVALOR1);
                    PS.Lib.DataField OP_ITEMCAMPOVALOR2 = new PS.Lib.DataField("CAMPOVALOR2", ListItensFaturar[i].CAMPOVALOR2);
                    PS.Lib.DataField OP_ITEMCAMPOVALOR3 = new PS.Lib.DataField("CAMPOVALOR3", ListItensFaturar[i].CAMPOVALOR3);
                    PS.Lib.DataField OP_ITEMCAMPOVALOR4 = new PS.Lib.DataField("CAMPOVALOR4", ListItensFaturar[i].CAMPOVALOR4);
                    PS.Lib.DataField OP_ITEMCAMPOVALOR5 = new PS.Lib.DataField("CAMPOVALOR5", ListItensFaturar[i].CAMPOVALOR5);
                    PS.Lib.DataField OP_ITEMCAMPOVALOR6 = new PS.Lib.DataField("CAMPOVALOR6", ListItensFaturar[i].CAMPOVALOR6);
                    PS.Lib.DataField OP_ITEMQUANTIDADESALDO = new PS.Lib.DataField("QUANTIDADE_SALDO", Convert.ToDecimal(ListItensFaturar[i].QTD_FATURAR));
                    PS.Lib.DataField OP_ITEMQUANTIDADEFATURADO = new PS.Lib.DataField("QUANTIDADE_FATURADO", Convert.ToDecimal(0));
                    OP_ITEMNSEQITEM.Valor = 0;
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

                    ListObjArr.Add(OP_ITEMQUANTIDADESALDO);
                    ListObjArr.Add(OP_ITEMQUANTIDADEFATURADO);

                    PSPartOperacaoItemData psPartOperacaoItemData = new PSPartOperacaoItemData();
                    psPartOperacaoItemData._tablename = "GOPERITEM";
                    psPartOperacaoItemData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM" };


                    psPartOperacaoItemData.SaveRecord(ListObjArr);

                    ListItensFaturar[i].NSEQDESTINO = i + 1;

                    // Estoque antigo
                    //if (parametroDestino["OPERESTOQUE"].ToString() != "N")
                    //{
                    //    PSPartLocalEstoqueSaldoData.Tipo Estoque;
                    //    if (parametroDestino["OPERESTOQUE"].ToString() == "A")
                    //    {
                    //        Estoque = PSPartLocalEstoqueSaldoData.Tipo.Aumenta;
                    //    }
                    //    else
                    //    {
                    //        Estoque = PSPartLocalEstoqueSaldoData.Tipo.Diminui;
                    //    }
                    //    PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                    //    psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
                    //    psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };

                    //    psPartLocalEstoqueSaldoData.MovimentaEstoque(Convert.ToInt32(operacao.CODEMPRESA), Convert.ToInt32(operacao.CODFILIAL), string.IsNullOrEmpty(operacao.CODLOCAL) ? string.Empty : operacao.CODLOCAL, string.IsNullOrEmpty(operacao.CODLOCALENTREGA) ? string.Empty : operacao.CODLOCALENTREGA, (OP_ITEMCODPRODUTO.Valor == null) ? string.Empty : OP_ITEMCODPRODUTO.Valor.ToString(), Convert.ToDecimal(OP_ITEMQUANTIDADE.Valor), (OP_ITEMCODUNIDOPER.Valor == null) ? string.Empty : OP_ITEMCODUNIDOPER.Valor.ToString(), Estoque);
                    //}

                    //// NOVO CONTROLE DE ESTOQUE
                    //int CODEMPRESA = Convert.ToInt32(operacao.CODEMPRESA);
                    //int CODOPER = Convert.ToInt32(operacao.CODOPER);
                    //int NSEQITEM = Convert.ToInt32(gb.RetornaDataFieldByCampo(objArr, "NSEQITEM").Valor);
                    //PSPartLocalEstoqueSaldoData saldoEstoque = new PSPartLocalEstoqueSaldoData();
                    //saldoEstoque.MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.OperacaoFaturamento, CODEMPRESA, CODOPER, NSEQITEM);

                }
            }
            catch (Exception)
            {

                throw new Exception("Operação [" + operacao.NUMERO + "] não possui itens cadastrados.");
            }


        }

        private void alteraValoresParametroOperacao()
        {
            //Verifica se o parametro de destino está vazio;
            //Se estiver retorna mensagem
            if (parametroDestino.Equals(null))
            {
                MessageBox.Show("Não foi possível carregar os parâmetros do Tipo da Operação [" + codTipOperDestino + "]", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Verifica se o ultimo nível é igual a 0.
            //if (parametroDestino["ULTIMONIVEL"].ToString().Equals("0"))
            //{
            //    MessageBox.Show("Tipo de operação [" + operacao.CODTIPOPER + "] não esta definida como sendo último nível.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //Verfica se usa o número sequencial
            if (parametroDestino["USANUMEROSEQ"].ToString().Equals("1"))
            {
                operacao.NUMERO = new Class.CopiaOperacao().GeraNumeroDocumento(codTipOperDestino);
            }
            //Verifica a série
            if (parametroDestino["OPERSERIE"].ToString().Equals("M"))
            {
                operacao.CODSERIE = null;
            }
            else
            {
                operacao.CODSERIE = parametroDestino["SERIEDEFAULT"].ToString();
            }
            //Verifica o local
            if (parametroDestino["LOCAL1"].ToString().Equals("M"))
            {
                operacao.CODLOCAL = null;
            }
            else
            {
                if (string.IsNullOrEmpty(operacao.CODLOCAL))
                {
                    operacao.CODLOCAL = parametroDestino["LOCAL1DEFAULT"].ToString();
                }
            }
            //Verifica Local 2
            if (parametroDestino["LOCAL2"].ToString().Equals("M"))
            {
                operacao.CODLOCALENTREGA = null;
            }
            else
            {
                if (parametroDestino["LOCAL2DEFAULT"] != DBNull.Value)
                {
                    operacao.CODLOCALENTREGA = parametroDestino["LOCAL2DEFAULT"].ToString();
                }
            }
            //Verifica o Cliente
            if (parametroDestino["CODCLIFOR"].ToString().Equals("M"))
            {
                operacao.CODCLIFOR = null;
            }
            else
            {
                if (string.IsNullOrEmpty(operacao.CODCLIFOR))
                {
                    operacao.CODCLIFOR = parametroDestino["CODCLIFORPADRAO"].ToString();
                }
            }
            //Verifica a Data de emissão
            if (parametroDestino["USADATAEMISSAO"].ToString() == "M")
            {
                operacao.DATAEMISSAO = null;
            }
            else
            {
                operacao.DATAEMISSAO = AppLib.Context.poolConnection.Get("Start").GetDateTime();
            }
            //Verifica a Data Entrada / Saída
            if (parametroDestino["USADATAENTSAI"].ToString() == "M")
            {
                operacao.DATAENTSAI = null;
            }
            else
            {
                operacao.DATAENTSAI = AppLib.Context.poolConnection.Get("Start").GetDateTime();
            }
            //Verifica a data de entrega
            //if (parametroDestino["USADATAENTREGA"].ToString() == "M")
            //{
            //    operacao.DATAENTREGA = null;
            //}
            //else
            //{
            //    DateTime dataentrega = Convert.ToDateTime(operacao.DATAENTREGA);
            //    DateTime dataemissao = Convert.ToDateTime(operacao.DATAEMISSAO);
            //    TimeSpan dd = dataentrega - dataemissao;


            //    operacao.DATAENTREGA = string.Format("{0:dd/MM/yyyy hh:mm:ss", DateTime.Now + (Convert.ToDateTime(operacao.DATAENTREGA) - Convert.ToDateTime(operacao.DATAEMISSAO)));
            //}
            //Verifica Objeto,
            if (parametroDestino["USACAMPOOBJETO"].ToString().Equals("M"))
            {
                operacao.CODOBJETO = null;
            }
            //Verifica o Operador
            if (parametroDestino["USACAMPOOPERADOR"].ToString().Equals("M"))
            {
                operacao.CODOPERADOR = null;
            }
            else
            {
                if (string.IsNullOrEmpty(operacao.CODOPERADOR))
                {

                    if (string.IsNullOrEmpty(parametroDestino["CODOPERADORPADRAO"].ToString()))
                    {
                        operacao.CODOPERADOR = null;
                    }
                    else
                    {
                        operacao.CODOPERADOR = parametroDestino["CODOPERADORPADRAO"].ToString();
                    }
                }
            }
            //Verifica Condição Pagamento
            if (parametroDestino["USACAMPOCONDPGTO"].ToString().Equals("M"))
            {
                operacao.CODCONDICAO = null;
            }
            else
            {
                if (string.IsNullOrEmpty(operacao.CODCONDICAO))
                {
                    operacao.CODCONDICAO = parametroDestino["CODCONDICAOPADRAO"].ToString();
                }
            }
            //Verifica a forma de pagto.
            if (parametroDestino["CODFORMA"].ToString().Equals("M"))
            {
                operacao.CODFORMA = null;
            }
            else
            {
                if (string.IsNullOrEmpty(operacao.CODFORMA))
                {
                    operacao.CODFORMA = parametroDestino["CODFORMAPADRAO"].ToString();
                }
            }
            //Verifica Conta Caixa
            if (parametroDestino["CODCONTA"].ToString().Equals("M"))
            {
                operacao.CODCONTA = null;
            }
            else
            {
                if (string.IsNullOrEmpty(operacao.CODCONTA))
                {
                    if (string.IsNullOrEmpty(parametroDestino["CODCONTAPADRAO"].ToString()))
                    {
                        operacao.CODCONTA = null;
                    }
                    else
                    {
                        operacao.CODCONTA = parametroDestino["CODCONTAPADRAO"].ToString();
                    }

                }
            }
            //Verifica Representante
            if (parametroDestino["USACODREPRE"].ToString().Equals("M"))
            {
                operacao.CODREPRE = null;
            }
            else
            {
                if (string.IsNullOrEmpty(operacao.CODREPRE))
                {
                    operacao.CODREPRE = parametroDestino["CODREPREPADRAO"].ToString();
                }
            }
            //Verifica Centro de Custo
            if (parametroDestino["USACODCCUSTO"].ToString().Equals("M"))
            {
                operacao.CODCCUSTO = null;
            }
            //Verificar Natureza Orcamento
            if (parametroDestino["USACODNATUREZAORCAMENTO"].ToString().Equals("M"))
            {
                operacao.CODNATUREZAORCAMENTO = null;
            }
            //Verifica Observação e Historico
            if (!parametroDestino["USAABAOBSERV"].ToString().Equals("1"))
            {
                operacao.OBSERVACAO = null;
                operacao.HISTORICO = null;
            }
            //Verifica Transporte
            if (!parametroDestino["USAABATRANSP"].ToString().Equals("1"))
            {
                operacao.FRETECIFFOB = 0;
                operacao.CODTRANSPORTADORA = null;
                operacao.QUANTIDADE = 0;
                operacao.PESOLIQUIDO = 0;
                operacao.PESOBRUTO = 0;
                operacao.ESPECIE = null;
                operacao.MARCA = null;
            }
            //Verifica Frete
            if (parametroDestino["USAVALORFRETE"].ToString().Equals("M"))
            {
                operacao.VALORFRETE = 0;
                operacao.PERCFRETE = 0;
            }
            //Verifica Valor Desconto
            if (parametroDestino["USAVALORDESCONTO"].ToString().Equals("M"))
            {
                operacao.VALORDESCONTO = 0;
                operacao.PERCDESCONTO = 0;
            }
            //Verifica Valor Despesa
            if (parametroDestino["USAVALORDESPESA"].ToString().Equals("M"))
            {
                operacao.VALORDESPESA = 0;
                operacao.PERCDESPESA = 0;
            }
            //Verifica Valor Seguro
            if (parametroDestino["USAVALORSEGURO"].ToString().Equals("M"))
            {
                operacao.VALORSEGURO = 0;
                operacao.PERCSEGURO = 0;
            }

            //Alteração dos Valores
            operacao.VALORBRUTO = 0;
            operacao.VALORLIQUIDO = 0;
        }

        private void alteraValoresParametroItens()
        {
            for (int i = 0; i < ListItensFaturar.Count; i++)
            {
                //Verifica valor unitário
                if (parametroDestino["USAVLUNITARIO"].ToString().Equals("M"))
                {
                    ListItensFaturar[i].VLUNITARIO = 0;
                }
                //Verifica Valor Desconto
                if (parametroDestino["USAVLDESCONTO"].ToString().Equals("M"))
                {
                    ListItensFaturar[i].VLDESCONTO = 0;
                }
                //Verifica o Valor PR Desconto
                if (parametroDestino["USAPRDESCONTO"].ToString().Equals("M"))
                {
                    ListItensFaturar[i].PRDESCONTO = 0;
                }
                //Verifica o Valor Total
                if (parametroDestino["USAVLTOTALITEM"].ToString().Equals("M"))
                {
                    ListItensFaturar[i].VLTOTALITEM = 0;
                }
                //Verifica a Natureza
                if (parametroDestino["USANATUREZA"].ToString().Equals("M"))
                {
                    ListItensFaturar[i].CODNATUREZA = null;
                }
                else
                {
                    if (string.IsNullOrEmpty(ListItensFaturar[i].CODNATUREZA))
                    {
                        ListItensFaturar[i].CODNATUREZA = new PSPartOperacaoData().DefineNatureza(Convert.ToInt32(operacao.CODEMPRESA), Convert.ToInt32(operacao.CODOPER), string.IsNullOrEmpty(parametroDestino["CODNATDENTRO"].ToString()) ? null : parametroDestino["CODNATDENTRO"].ToString(), (string.IsNullOrEmpty(parametroDestino["CODNATFORA"].ToString()) ? null : parametroDestino["CODNATFORA"].ToString()));
                    }
                }
            }
        }

        private void verificaStatusOrigem(AppLib.Data.Connection conn)
        {
            bool status = true;
            for (int i = 0; i < ListItensFaturar.Count; i++)
            {
                DataTable dt = conn.ExecQuery("SELECT QUANTIDADE_SALDO FROM GOPERITEM WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { ListItensFaturar[i].CODOPER, operacao.CODEMPRESA });
                for (int iDt = 0; iDt < dt.Rows.Count; iDt++)
                {
                    if (!dt.Rows[iDt]["QUANTIDADE_SALDO"].ToString().Equals("0,0000") && !dt.Rows[iDt]["QUANTIDADE_SALDO"].ToString().Equals("0"))
                    {
                        status = false;
                    }
                }
                if (status.Equals(true))
                {
                    conn.ExecTransaction("UPDATE GOPER SET CODSTATUS = ? WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { "1", ListItensFaturar[i].CODOPER, operacao.CODEMPRESA });
                }
                status = true;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            cancelar = true;
            this.Dispose();
        }

        private void btnSelecionarTudo_Click(object sender, EventArgs e)
        {
            if (btnSelecionarTudo.Text == "Selecionar todos")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = true;
                }

                btnSelecionarTudo.Text = "Desselecionar todos";
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = false;
                }

                btnSelecionarTudo.Text = "Selecionar todos";
            }
        }
    }
}
