using ITGProducao.Class;
using PS.Glb;
using PS.Glb.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITGProducao.Formularios
{
    public partial class FrmOrdemProducaoEntradas : Form
    {
        DataTable dtSubproduto;

        public string codOrdem = string.Empty;
        public string seqOrdem = string.Empty;
        public string codEstrutura = string.Empty;
        public string CodRevEstrutura = string.Empty;

        private enum TipoGOPER
        {
            EntradaRefugo,
            EntradaProducao,
            Devolucao
        }

        public FrmOrdemProducaoEntradas(string codOrdem, string seqOrdem, string codEstrutura, string CodRevEstrutura)
        {
            InitializeComponent();
            this.codOrdem = codOrdem;
            this.seqOrdem = seqOrdem;
            this.codEstrutura = codEstrutura;
            this.CodRevEstrutura = CodRevEstrutura;

            CarregaGridSubProduto();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnGerarEntradas_Click(object sender, EventArgs e)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEMENTRADA");
            conn.BeginTransaction();

            try
            {
                int CODOPER_PRODUTOACABADO = 0;
                int NSEQITEM_PRODUTOACABADO = 1;

                int CODOPER_REFUGO = 0;
                int NSEQITEM_REFUGO = 1;

                int CODOPER_DEVOLUÇÃO = 0;
                int NSEQITEM_DEVOLUÇÃO = 1;

                int CODOPER_INTERMEDIARIO = 0;
                int NSEQITEM_INTERMEDIARIO = 1;

                if (MessageBox.Show("Deseja realmente gerar as entradas de todos os produtos?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i <= dtSubproduto.Rows.Count - 1; i++)
                    {
                        if (string.IsNullOrEmpty(dtSubproduto.Rows[i]["PORDEMENTRADA.CODOPER"].ToString()))
                        {
                            bool salvar = false;
                            switch (dtSubproduto.Rows[i]["PORDEMENTRADA.TIPOENTRADA"].ToString())
                            {
                                case "Produto Acabado":
                                    salvar = SalvarEntrada(ref CODOPER_PRODUTOACABADO, NSEQITEM_PRODUTOACABADO, conn, TipoGOPER.EntradaProducao, dtSubproduto.Rows[i]);
                                    NSEQITEM_PRODUTOACABADO = NSEQITEM_PRODUTOACABADO + 1;
                                    break;
                                case "Produto Intermediário":
                                    salvar = SalvarEntrada(ref CODOPER_INTERMEDIARIO, NSEQITEM_INTERMEDIARIO, conn, TipoGOPER.EntradaProducao, dtSubproduto.Rows[i]);
                                    NSEQITEM_INTERMEDIARIO = NSEQITEM_INTERMEDIARIO + 1;
                                    break;
                                case "Refugo":
                                    salvar = SalvarEntrada(ref CODOPER_REFUGO, NSEQITEM_REFUGO, conn, TipoGOPER.EntradaRefugo, dtSubproduto.Rows[i]);
                                    NSEQITEM_REFUGO = NSEQITEM_REFUGO + 1;
                                    break;
                                case "Devolução":
                                    salvar = SalvarEntrada(ref CODOPER_DEVOLUÇÃO, NSEQITEM_DEVOLUÇÃO, conn, TipoGOPER.Devolucao, dtSubproduto.Rows[i]);
                                    NSEQITEM_DEVOLUÇÃO = NSEQITEM_DEVOLUÇÃO + 1;
                                    break;
                                default:
                                    throw new Exception("Erro ao gerar entradas.");
                            }
                            if (salvar == false)
                            {
                                throw new Exception("Erro ao gerar entradas.");
                            }
                        }
                    }
                    conn.Commit();

                    CarregaGridSubProduto();
                    MessageBox.Show("Entradas geradas com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                conn.Rollback();
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool SalvarEntrada(ref int CODOPER, int NSEQITEM, AppLib.Data.Connection conn , TipoGOPER tipo, DataRow registro)
        {
            try
            {

                PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque Estoque;
                Estoque = PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao;

                if (CODOPER == 0)
                {
                    CODOPER = IncluirGOPER(conn, tipo);
                }

                if (CODOPER <= 0)
                {
                    throw new Exception("Erro ao incluir operação");
                }

                if (IncluirGOPERITEM(CODOPER, conn, registro) == true)
                {
                    psPartLocalEstoqueSaldoData.MovimentaEstoque2(Estoque, AppLib.Context.Empresa, CODOPER, NSEQITEM);

                    conn.ExecTransaction("UPDATE PORDEMENTRADA SET CODOPER = ?, NSEQITEM = ? WHERE CODEMPRESA = ? AND CODFILIAL = ? AND ID = ?", new object[] { CODOPER,NSEQITEM, AppLib.Context.Empresa, AppLib.Context.Filial, registro["PORDEMENTRADA.ID"].ToString() });
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para Incluir registro na tabela GOPER e GOPERITEM
        /// </summary>
        /// <param name="conn">Conexão Ativa</param>
        /// <param name="tipogoper">tipo da operação permitida na OP</param>
        /// <returns>CODOPER</returns>
        private int IncluirGOPER(AppLib.Data.Connection conn, TipoGOPER tipogoper)
        {
            try
            {
                Goper _goper = new Goper();

                _goper.CODEMPRESA = AppLib.Context.Empresa;
                _goper.CODFILIAL = AppLib.Context.Filial;
                _goper.CODUSUARIOCRIACAO = AppLib.Context.Usuario;
                _goper.DATACRIACAO = conn.GetDateTime();
                _goper.CODCLIFOR = conn.ExecGetField("", "select CODCLIFOR from VCLIFOR where  CGCCPF in (select CGCCPF from gfilial where codfilial = ?)", new object[] { AppLib.Context.Filial }).ToString();
                _goper.CODOPERADOR = conn.ExecGetField("", "select CODOPERADOR from VOPERADOR where CODUSUARIO = ?", new object[] { AppLib.Context.Usuario }).ToString();

                switch (tipogoper)
                {
                    case TipoGOPER.EntradaRefugo:
                        _goper.CODTIPOPER = VerificaParametro("TIPOPERENTREFUGO");
                        //_goper.CODLOCAL = VerificaParametro("LOCALESTOQUEMP");
                        //_goper.CODLOCALENTREGA = VerificaParametro("LOCALESTOQUERESERVA");
                        break;
                    case TipoGOPER.EntradaProducao:
                        _goper.CODTIPOPER = VerificaParametro("TIPOPERENTPRODUCAO");
                        //_goper.CODLOCAL = VerificaParametro("LOCALESTOQUEMP");
                        break;
                    case TipoGOPER.Devolucao:
                        _goper.CODTIPOPER = VerificaParametro("TIPOPERDEVOLUCAOMP");
                        break;
                    default:
                        throw new Exception("Erro ao incluir operação.");
                }

                return _goper.setGoper(_goper, conn);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao incluir operação.");
            }
        }

        private bool IncluirGOPERITEM(int CODOPER, AppLib.Data.Connection conn, DataRow row)
        {
            //// João Pedro Luchiari - 24/11/2017 
            //AppLib.Data.Connection con = AppLib.Context.poolConnection.Get("Start").Clone();
            //conn.BeginTransaction();
            //AppLib.ORM.Jit GOPERITEM = new AppLib.ORM.Jit(con, "GOPERITEM");
            ////
            GoperItem _goperitem = new GoperItem();

            _goperitem.CODEMPRESA = AppLib.Context.Empresa;
            _goperitem.CODPRODUTO = row["PORDEMENTRADA.CODPRODUTO"].ToString();
            //_goperitem.DATAENTREGA = Convert.ToDateTime(row["PORDEM.DATAINIPLANOP"].ToString()); //select DATAINIPLANOP from pordem where CODEMPRESA = 1 and CODFILIAL = 2 and CODESTRUTURA = '01.01.2093' and REVESTRUTURA = 1 and CODIGOOP = '000001/17' and SEQOP = '001'
            _goperitem.CODUNIDOPER = row["PORDEMENTRADA.UNDPRODUTO"].ToString();
            _goperitem.QUANTIDADE = Convert.ToDecimal(row["PORDEMENTRADA.QTDENTRADA"].ToString());
            _goperitem.APLICACAOMATERIAL = "C"; //CONSUMO

            return _goperitem.setItem(_goperitem, conn, CODOPER);

            #region INSERT na tabela VFICHAESTOQUE

            //string codTipOper = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CODOPER }).ToString();

            //string tipoEstoque = conn.ExecGetField("N", "SELECT OPERESTOQUE FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codTipOper, AppLib.Context.Empresa }).ToString();
            //if (tipoEstoque != "N")
            //{
            //    PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
            //    psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
            //    psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };

            //    PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque Estoque;
            //    Estoque = PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao;

            //    DataTable dtLocal = conn.ExecQuery(@"SELECT CODLOCAL, CODLOCALENTREGA FROM GOPER WHERE CODOPER = ? AND CODEMPRESA =? ", new object[] { CODOPER, AppLib.Context.Empresa });


            //    //psPartLocalEstoqueSaldoData.MovimentaEstoque(AppLib.Context.Empresa, AppLib.Context.Filial, dtLocal.Rows[0]["CODLOCAL"].ToString(), dtLocal.Rows[0]["CODLOCALENTREGA"].ToString(), GOPERITEM.Get("CODPRODUTO").ToString(), Convert.ToDecimal(GOPERITEM.Get("QUANTIDADE")), GOPERITEM.Get("CODUNIDOPER").ToString(), PSPartLocalEstoqueSaldoData.Tipo.Diminui);
            //    psPartLocalEstoqueSaldoData.MovimentaEstoque2(Estoque, AppLib.Context.Empresa, CODOPER, Convert.ToInt32(GOPERITEM.Get("NSEQITEM")));
            //}
            #endregion
        }

        public string VerificaParametro(string parametro)
        {
            try
            {
                Global gl = new Global();
                string _PARAM = gl.VerificaParametroString(parametro);

                return _PARAM;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void CarregaGridSubProduto(bool LimpaGrid = false)
        {
            string tabela = "PORDEMENTRADA";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();

            try
            {
                string campos = @"  PORDEMENTRADA.ID as 'PORDEMENTRADA.ID'
                                    ,PORDEMAPONTAMENTO.DATAAPO as 'PORDEMAPONTAMENTO.DATAAPO'
                                    ,PORDEMENTRADA.SEQAPO as 'PORDEMENTRADA.SEQAPO'
                                    ,PORDEMENTRADA.CODPRODUTO AS 'PORDEMENTRADA.CODPRODUTO'
                                    ,VPRODUTO.DESCRICAO AS 'VPRODUTO.DESCRICAO'
	                                ,PORDEMENTRADA.UNDPRODUTO AS 'PORDEMENTRADA.UNDPRODUTO'
	                                ,PORDEMENTRADA.QTDENTRADA AS 'PORDEMENTRADA.QTDENTRADA'
	                                ,(CASE PORDEMENTRADA.TIPOENTRADA WHEN 1 THEN 'Produto Acabado'
                                                                     WHEN 2 THEN 'Produto Intermediário'
                                                                     WHEN 3 THEN 'Refugo'
                                                                     WHEN 4 THEN 'Devolução'
                                     END ) AS 'PORDEMENTRADA.TIPOENTRADA'
	                                ,PORDEMENTRADA.CODOPER AS 'PORDEMENTRADA.CODOPER'
	                                ,PORDEMENTRADA.NSEQITEM AS 'PORDEMENTRADA.NSEQITEM'
	                                ,PORDEMENTRADA.DATAMOVIMENTO AS 'PORDEMENTRADA.DATAMOVIMENTO'";

                string sql = @"SELECT " + (LimpaGrid == false ? campos : " TOP 0 " + campos) + @"
                                         FROM PORDEMENTRADA JOIN VPRODUTO ON PORDEMENTRADA.CODEMPRESA = VPRODUTO.CODEMPRESA 
                                                                         AND PORDEMENTRADA.CODPRODUTO = VPRODUTO.CODPRODUTO 
                                                            JOIN PORDEMAPONTAMENTO ON PORDEMAPONTAMENTO.CODEMPRESA = PORDEMENTRADA.CODEMPRESA
                			                                              AND PORDEMAPONTAMENTO.CODFILIAL = PORDEMENTRADA.CODFILIAL
                				                                          AND PORDEMAPONTAMENTO.CODESTRUTURA = PORDEMENTRADA.CODESTRUTURA
                				                                          AND PORDEMAPONTAMENTO.REVESTRUTURA = PORDEMENTRADA .REVESTRUTURA
                				                                          AND PORDEMAPONTAMENTO.CODIGOOP = PORDEMENTRADA.CODIGOOP
                				                                          AND PORDEMAPONTAMENTO.SEQOP = PORDEMENTRADA.SEQOP
                				                                          AND PORDEMAPONTAMENTO.CODOPERACAO = PORDEMENTRADA.CODOPERACAO 
                				                                          AND PORDEMAPONTAMENTO.SEQOPERACAO = PORDEMENTRADA.SEQOPERACAO
                				                                          AND PORDEMAPONTAMENTO.SEQAPO = PORDEMENTRADA.SEQAPO
                                        WHERE PORDEMENTRADA.CODEMPRESA = ?
                                          AND PORDEMENTRADA.CODFILIAL = ? "
                            + (LimpaGrid == false ? @" AND PORDEMENTRADA.CODESTRUTURA = ?
                                          AND PORDEMENTRADA.REVESTRUTURA = ?
                                          AND PORDEMENTRADA.CODIGOOP = ?
                                          AND PORDEMENTRADA.SEQOP = ?" : "");


                //AND PORDEMENTRADA.SEQOPERACAO = ?
                //AND PORDEMENTRADA.CODOPERACAO = ?
                //AND PORDEMENTRADA.SEQAPO = ?

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {
                    sql = sql + filtroUsuario;
                }
                gridSubproduto.DataSource = null;
                gridViewSubproduto.Columns.Clear();
                if (LimpaGrid == false)
                {
                    dtSubproduto = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem });
                }
                else
                {
                    dtSubproduto = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial });
                }

                gridSubproduto.DataSource = dtSubproduto;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "VPRODUTO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewSubproduto.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewSubproduto.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        if (gridViewSubproduto.Columns[i].FieldName == "PORDEMENTRADA.ID" || gridViewSubproduto.Columns[i].FieldName == "PORDEMAPONTAMENTO.DATAAPO")
                        {
                            gridViewSubproduto.Columns[i].Visible = false;
                        }
                        gridViewSubproduto.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewSubproduto.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
