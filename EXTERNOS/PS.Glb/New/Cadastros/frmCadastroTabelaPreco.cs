using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITGProducao.Controles;
using DevExpress.XtraGrid.Views.Grid;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroTabelaPreco : Form
    {
        public bool edita = false;
        public string Codcliente = string.Empty;
        public string Codproduto = string.Empty;
        public string Codfilial = string.Empty;
        public string IdTabela;
        public bool UsaTabela = false;
        public decimal Valor_Processo;

        //Variaveis para NewLookup
        private NewLookup lookup;

        //Variaveis para Carregar a Grid
        string query = string.Empty;
        string Relacionamento = string.Empty;
        string tabela = "VCLIFORTABPRECOITEM";
        DataTable dtOriginal;

        //Variaveis para o processo
        public int processos;
        public decimal Valor;
        public decimal Result;
        public string TipoProcesso;

        private List<string> tabelasFilhas = new List<string>();

        public frmCadastroTabelaPreco()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VCLIFORTABPRECO");
        }

        public frmCadastroTabelaPreco(ref NewLookup lookup)
        {
            InitializeComponent();
            this.edita = true;
            this.lookup = lookup;
        }

        private void frmCadastroTabelaPreco_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                CarregaCampos();
                bool UsaFilial = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT USATABELAPORFILIAL FROM VCLIFORTABPRECO WHERE CODEMPRESA = ? AND IDTABELA = ? ", new object[] { AppLib.Context.Empresa, IdTabela }).ToString());

                if (UsaFilial == true)
                {
                    lpFilial.Enabled = true;
                }
                else
                {
                    lpFilial.Enabled = false;
                }
                CarregaGrid("WHERE IDTABELA ='" + IdTabela + "'");
                lpCliente.Enabled = false;
                btnProcessosTabelaPreco.Enabled = true;
            }
            else
            {
                LimpaGrid_Itens();
                lpFilial.Enabled = false;
            }

            VerificaAcesso();
        }

        private void VerificaAcesso()
        {
            if (edita == true)
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO FROM GPERMISSAOMENU WHERE CODUSUARIO = ? AND CODMENU = ?", new object[] { AppLib.Context.Usuario, "btnUtilitarios_TabelaPreco" });
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0]["EDICAO"]) == false)
                    {
                        btnSalvarAtual.Enabled = false;
                        btnOKAtual.Enabled = false;
                        btnNovo.Enabled = false;
                        btnEditar.Enabled = false;
                        btnExcluir.Enabled = false;
                    }
                }
                else
                {
                    DataTable dtPermissao = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO FROM GPERMISSAOMENU WHERE CODUSUARIO = ? AND CODMENU = ?", new object[] { AppLib.Context.Usuario, "btnUtilitarios_TabelaPreco" });
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["EDICAO"]) == false)
                        {
                            btnSalvarAtual.Enabled = false;
                            btnOKAtual.Enabled = false;
                            btnNovo.Enabled = false;
                            btnEditar.Enabled = false;
                            btnExcluir.Enabled = false;
                        }
                    }
                }
            }
        }

        private bool validar()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(lpCliente.txtcodigo.Text))
            {
                errorProvider1.SetError(lpCliente, "O código do cliente deve ser informado.");
                verifica = false;
            }
            return verifica;
        }

        // Valida se o Cliente/Fornecedor ja foi usado anteriormente

        private void lpCliente_Leave_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lpCliente.ValorCodigoInterno))
            {
                DataTable dtTabPreco = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECO ORDER BY IDTABELA ASC", new object[] { });

                for (int i = 0; i < dtTabPreco.Rows.Count; i++)
                {
                    string Codclifor = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODCLIFOR FROM VCLIFORTABPRECO WHERE IDTABELA = ?", new object[] { dtTabPreco.Rows[i]["IDTABELA"] }).ToString();

                    if (Codclifor == lpCliente.txtcodigo.Text)
                    {
                        MessageBox.Show("Tabela de preço já cadastrada para o cliente selecionado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        lpCliente.Clear();
                        break;
                    }
                }

            }
        }

        #region Grid

        private void LimpaGrid_Itens()
        {
            try
            {
                string sql = string.Empty;

                sql = new Class.Utilidades().getVisao(tabela, string.Empty, tabelasFilhas, "").Replace("SELECT", "SELECT TOP 0 ");

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dtOriginal = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                gridControl1.DataSource = dtOriginal;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridView1.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridView1.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregaGrid(string where)
        {
            Relacionamento = "INNER JOIN VPRODUTO ON VPRODUTO.CODEMPRESA = VCLIFORTABPRECOITEM.CODEMPRESA AND VPRODUTO.CODPRODUTO = VCLIFORTABPRECOITEM.CODPRODUTO";
            List<string> Tabelasfilhas = new List<string>();
            Tabelasfilhas.Add("VPRODUTO");

            try
            {
                string sql = string.Empty;

                sql = new Class.Utilidades().getVisao(tabela, Relacionamento, Tabelasfilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dtOriginal = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                DataColumn colValorAnterior = new DataColumn("PRECOUNITARIO_ANTERIOR", typeof(decimal));
                dtOriginal.Columns.Add(colValorAnterior);

                gridControl1.DataSource = dtOriginal;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    if (gridView1.Columns[i].FieldName.ToString() == "PRECOUNITARIO_ANTERIOR")
                    {
                        gridView1.Columns[i].Visible = false;
                        continue;
                    }

                    gridView1.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridView1.Columns[i].FieldName.ToString() });

                    if (result != null)
                    {
                        gridView1.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = (GridView)sender;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = view.CalcHitInfo(e.Location);
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.ColumnButton)
            {
                view.SelectAll();
            }
        }

        #endregion

        private void carregaObj(DataTable dt)
        {
            //Busca os valores na tabela VCLIFORTABPRECO

            ////dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODCLIFOR, USATABELAPORFILIAL FROM VCLIFORTABPRECO WHERE CODEMPRESA = ? AND IDTABELA = ?", new object[] { AppLib.Context.Empresa, IdTabela });

            ////lpCliente.txtcodigo.Text = dt.Rows[0]["CODCLIFOR"].ToString();
            ////lpCliente.CarregaDescricao();
            ////Codcliente = lpCliente.txtcodigo.Text;

            ////chkUsaTabelaFilial.Checked = Convert.ToBoolean(dt.Rows[0]["USATABELAPORFILIAL"]);

            //Busca os valores na tabela VCLIFORTABPRECOITEM

            Codfilial = AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT CODFILIAL FROM VCLIFORTABPRECOITEM WHERE CODEMPRESA = ? AND IDTABELA = ?", new object[] { AppLib.Context.Empresa, IdTabela }).ToString();
            if (Codfilial == "0")
            {
                LimpaGrid_Itens();
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE CODEMPRESA = ? AND CODFILIAL = ? AND IDTABELA = ?", new object[] { AppLib.Context.Empresa, Codfilial, IdTabela });

                lpFilial.txtcodigo.Text = dt.Rows[0]["CODFILIAL"].ToString();
                lpFilial.CarregaDescricao();
                Codfilial = lpFilial.ValorCodigoInterno;
            }           
        }

        private void CarregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODCLIFOR, USATABELAPORFILIAL FROM VCLIFORTABPRECO WHERE CODEMPRESA = ? AND IDTABELA = ?", new object[] { AppLib.Context.Empresa, IdTabela });

                lpCliente.txtcodigo.Text = dt.Rows[0]["CODCLIFOR"].ToString();
                lpCliente.CarregaDescricao();
                Codcliente = lpCliente.txtcodigo.Text;

                chkUsaTabelaFilial.Checked = Convert.ToBoolean(dt.Rows[0]["USATABELAPORFILIAL"]);

                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE CODEMPRESA = ? AND CODFILIAL = ? AND IDTABELA = ?", new object[] { AppLib.Context.Empresa, Codfilial, IdTabela });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void chkUsaTabelaFilial_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUsaTabelaFilial.Checked == true)
            {
                lpFilial.Enabled = true;
                UsaTabela = true;
            }
            else
            {
                lpFilial.Enabled = false;
                UsaTabela = false;
            }
        }

        private void btnBuscarProdutos_Click(object sender, EventArgs e)
        {
            if (validar() == false)
            {
                return;
            }

            SalvarTabelaPreco();

            string IdTabelaPreco = AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT MAX(IDTABELA) FROM VCLIFORTABPRECO WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();

            using (PS.Glb.New.Cadastros.frmTabelaPrecoProdutos produtos = new frmTabelaPrecoProdutos())
            {
                produtos.CodCliente = lpCliente.txtcodigo.Text;
                produtos.CodFilial = lpFilial.txtcodigo.Text;
                produtos.UsaFilial = UsaTabela;

                if (edita == true)
                {
                    produtos.idTabela = IdTabela;
                }
                else
                {
                    produtos.idTabela = IdTabelaPreco;
                }
                produtos.ShowDialog();

                IdTabela = produtos.idTabela;
                btnProcessosTabelaPreco.Enabled = true;
                CarregaGrid("WHERE IDTABELA ='" + IdTabela + "'");
            }
        }

        #region Tabela de Preço

        private void btnPesquisarTabelaPreco_Click_1(object sender, EventArgs e)
        {
            if (gridView1.OptionsFind.AlwaysVisible == true)
            {
                gridView1.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView1.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnAgruparTabelaPreco_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsView.ShowGroupPanel == true)
            {
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.ClearGrouping();
                btnAgruparTabelaPreco.Text = "Agrupar";
            }
            else
            {
                gridView1.OptionsView.ShowGroupPanel = true;
                btnAgruparTabelaPreco.Text = "Desagrupar";
            }
        }

        private void btnAtualizarTabelaPreco_Click_1(object sender, EventArgs e)
        {
            CarregaGrid("WHERE IDTABELA ='" + IdTabela + "'");
        }

        private void btnSelecionarColunasTabelaPreco_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            CarregaGrid("WHERE IDTABELA ='" + IdTabela + "'");
        }

        private void btnSalvarLayoutTabelaPreco_Click_1(object sender, EventArgs e)
        {
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });

            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", tabela);
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView1.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView1.VisibleColumns[i].Width);
                GVISAOUSUARIO.Set("VISIVEL", 1);
                GVISAOUSUARIO.Save();
            }

            DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ? AND FIXO = 1", new object[] { AppLib.Context.Usuario, tabela });
            if (dtFixo.Rows.Count > 0)
            {
                for (int i = 0; i < dtFixo.Rows.Count; i++)
                {
                    AppLib.ORM.Jit FIXO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                    FIXO.Set("VISAO", tabela);
                    FIXO.Set("CODUSUARIO", AppLib.Context.Usuario);
                    FIXO.Set("COLUNA", dtFixo.Rows[i]["COLUNA"]);
                    FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
            }
        }

        private void btnExcluirTabelaPreco_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir os registros selecionados?", "Confirmação.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODPRODUTO = ? AND CODFILIAL = ? AND PRECOUNITARIO = ?", new object[] { row["VCLIFORTABPRECOITEM.IDTABELA"], row["VCLIFORTABPRECOITEM.CODPRODUTO"], row["VCLIFORTABPRECOITEM.CODFILIAL"], row["VCLIFORTABPRECOITEM.PRECOUNITARIO"] });
                    }
                    MessageBox.Show("Registro excluído com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregaGrid("WHERE IDTABELA ='" + IdTabela + "'");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Métodos para salvar

        private bool SalvarNovoPrecoUnitario()
        {
            try
            {
                if (dtOriginal.Rows.Count > 0)
                {
                    for (int i = 0; i < dtOriginal.Rows.Count; i++)
                    {
                        // Atualiza o preço do valor unitário na VCLIFORTABPRECOITEM

                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODPRODUTO = ? AND IDTABELA = ?", new object[] { Convert.ToDecimal(dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"]), AppLib.Context.Empresa, dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.CODFILIAL"], dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.CODPRODUTO"], dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.IDTABELA"] });
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool SalvarEdicao()
        {
            try
            {
                DataTable dtobj = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE CODEMPRESA = ? AND IDTABELA = ?", new object[] { AppLib.Context.Empresa, IdTabela });

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    for (int j = 0; j < dtobj.Rows.Count; j++)
                    {
                        DataRow row = gridView1.GetDataRow(i);

                        if (row["VCLIFORTABPRECOITEM.CODPRODUTO"].ToString() == dtobj.Rows[j]["CODPRODUTO"].ToString())
                        {
                            if (Convert.ToInt32(row["VCLIFORTABPRECOITEM.CODFILIAL"]) == Convert.ToInt32(dtobj.Rows[j]["CODFILIAL"]))
                            {
                                if (Convert.ToDecimal(row["VCLIFORTABPRECOITEM.PRECOUNITARIO"]) != Convert.ToDecimal(dtobj.Rows[j]["PRECOUNITARIO"]))
                                {
                                    if (Convert.ToDecimal(row["VCLIFORTABPRECOITEM.PRECOUNITARIO"]) > Convert.ToDecimal(dtobj.Rows[j]["PRECOUNITARIO"]))
                                    {
                                        TipoProcesso = "ACRESCENTAR VALOR";
                                    }
                                    else
                                    {
                                        TipoProcesso = "REDUZIR VALOR";
                                    }

                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODPRODUTO = ? AND CODFILIAL = ?", new object[] { row["VCLIFORTABPRECOITEM.PRECOUNITARIO"], AppLib.Context.Empresa, IdTabela, row["VCLIFORTABPRECOITEM.CODPRODUTO"], row["VCLIFORTABPRECOITEM.CODFILIAL"] });

                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, row["VCLIFORTABPRECOITEM.CODFILIAL"], Codcliente, row["VCLIFORTABPRECOITEM.CODPRODUTO"], row["VCLIFORTABPRECOITEM.CODMOEDA"], dtobj.Rows[j]["PRECOUNITARIO"], row["VCLIFORTABPRECOITEM.PRECOUNITARIO"], 0, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            //AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            //AppLib.ORM.Jit VCLIFORTABPRECOLOG = new AppLib.ORM.Jit(conn, "VCLIFORTABPRECOLOG");
            //conn.BeginTransaction();

            //DataTable dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ?", new object[] { IdTabela });

            //try
            //{
            //    if (dtOriginal.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dtOriginal.Rows.Count; i++)
            //        {
            //            // Inserindo no VCLIFORTABPRECOLOG

            //            VCLIFORTABPRECOLOG.Set("CODEMPRESA", AppLib.Context.Empresa);
            //            VCLIFORTABPRECOLOG.Set("CODFILIAL", dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.CODFILIAL"]);
            //            VCLIFORTABPRECOLOG.Set("CODCLIFOR", lpCliente.txtcodigo.Text);
            //            VCLIFORTABPRECOLOG.Set("CODPRODUTO", dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.CODPRODUTO"]);
            //            VCLIFORTABPRECOLOG.Set("CODMOEDA", dtitens.Rows[i]["CODMOEDA"]);

            //            if (DBNull.Value.Equals(dtOriginal.Rows[i]["PRECOUNITARIO_ANTERIOR"]))
            //            {
            //                VCLIFORTABPRECOLOG.Set("PRECOUNITARIOORIGINAL", dtitens.Rows[i]["PRECOUNITARIO"]);
            //            }
            //            else
            //            {
            //                VCLIFORTABPRECOLOG.Set("PRECOUNITARIOORIGINAL", Convert.ToDecimal(dtitens.Rows[i]["PRECOUNITARIO"]));
            //            }

            //            if (DBNull.Value.Equals(dtOriginal.Rows[i]["PRECOUNITARIO_ANTERIOR"]))
            //            {
            //                VCLIFORTABPRECOLOG.Set("PRECOUNITARIOATUALIZADO", Convert.ToDecimal(dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"]));
            //            }
            //            else
            //            {
            //                VCLIFORTABPRECOLOG.Set("PRECOUNITARIOATUALIZADO", Convert.ToDecimal(dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"]));
            //            }

            //            if (processos == 3 || processos == 5)
            //            {
            //                VCLIFORTABPRECOLOG.Set("PERCATUALIZACAO", Valor_Processo);
            //            }
            //            else
            //            {
            //                VCLIFORTABPRECOLOG.Set("PERCATUALIZACAO", 0);
            //            }

            //            VCLIFORTABPRECOLOG.Set("USUARIOALTERACAO", AppLib.Context.Usuario);
            //            VCLIFORTABPRECOLOG.Set("DATAALTERACAO", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
            //            VCLIFORTABPRECOLOG.Set("TIPOPROCESSO", TipoProcesso);

            //            VCLIFORTABPRECOLOG.Insert();
            //        }
            //        // Atualiza o preço do valor unitário na VCLIFORTABPRECOITEM

            //        SalvarNovoPrecoUnitario();
            //    }
            //    conn.Commit();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    conn.Rollback();
            //    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
        }

        private bool SalvarTabelaPreco()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VCLIFORTABPRECO = new AppLib.ORM.Jit(conn, "VCLIFORTABPRECO");
            conn.BeginTransaction();
            try
            {
                // Inserindo no VCLIFORTABPRECO

                VCLIFORTABPRECO.Set("CODEMPRESA", AppLib.Context.Empresa);

                if (!string.IsNullOrEmpty(lpCliente.ValorCodigoInterno))
                {
                    VCLIFORTABPRECO.Set("CODCLIFOR", lpCliente.txtcodigo.Text);
                }
                else
                {
                    VCLIFORTABPRECO.Set("CODCLIFOR", null);
                }

                VCLIFORTABPRECO.Set("USATABELAPORFILIAL", chkUsaTabelaFilial.Checked == true ? 1 : 0);
                VCLIFORTABPRECO.Set("USUARIOCRIACAO", AppLib.Context.Usuario);
                VCLIFORTABPRECO.Set("DATACRIACAO", Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                VCLIFORTABPRECO.Save();

                conn.Commit();
                return true;
            }
            catch (Exception ex)
            {
                conn.Rollback();
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool AtualizaTabelaPreco()
        {
            if (edita == true)
            {
                //AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE VCLIFORTABPRECO SET CODCLIFOR = ?, USATABELAPORFILIAL = ?, USUARIOCRIACAO = ?, DATACRIACAO = ? 
                //                                                             WHERE CODEMPRESA = ? AND IDTABELA = ?", new object[] { lpCliente.txtcodigo.Text, chkUsaTabelaFilial.Checked == true ? 1 : 0, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), AppLib.Context.Empresa, IdTabela });

                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit VCLIFORTABPRECO = new AppLib.ORM.Jit(conn, "VCLIFORTABPRECO");
                conn.BeginTransaction();

                try
                {
                    VCLIFORTABPRECO.Set("CODEMPRESA", AppLib.Context.Empresa);
                    VCLIFORTABPRECO.Set("CODCLIFOR", lpCliente.txtcodigo.Text);
                    VCLIFORTABPRECO.Set("USATABELAPORFILIAL", chkUsaTabelaFilial.Checked == true ? 1 : 0);
                    VCLIFORTABPRECO.Set("USUARIOCRIACAO", AppLib.Context.Usuario);
                    VCLIFORTABPRECO.Set("DATACRIACAO", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                    VCLIFORTABPRECO.Save();
                    conn.Commit();
                }
                catch (Exception ex)
                {
                    conn.Rollback();
                    throw ex;
                }

                //if (chkUsaTabelaFilial.Checked == true)
                //{
                //    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET CODFILIAL = ? WHERE CODEMPRESA = ? AND IDTABELA = ?", new object[] { lpFilial.txtcodigo.Text, AppLib.Context.Empresa, IdTabela });
                //}
                //else
                //{
                //    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET CODFILIAL = ? WHERE CODEMPRESA = ? AND IDTABELA = ?", new object[] { 1, AppLib.Context.Empresa, IdTabela });
                //}

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Processos

        private void Processos()
        {
            switch (processos)
            {
                case 1:

                    if (gridView1.SelectedRowsCount >= 1)
                    {
                        for (int j = 0; j < gridView1.SelectedRowsCount; j++)
                        {
                            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(j).ToString()));

                            dtOriginal.Rows[j]["PRECOUNITARIO_ANTERIOR"] = dtOriginal.Rows[j]["VCLIFORTABPRECOITEM.PRECOUNITARIO"];

                            row["VCLIFORTABPRECOITEM.PRECOUNITARIO"] = Valor_Processo;
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("ATENÇÃO: Todos os registros serão alterados, deseja continuar?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            for (int i = 0; i < dtOriginal.Rows.Count; i++)
                            {
                                dtOriginal.Rows[i]["PRECOUNITARIO_ANTERIOR"] = dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"];

                                dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"] = Valor_Processo;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }

                    break;
                case 2:

                    if (gridView1.SelectedRowsCount >= 1)
                    {
                        for (int j = 0; j < gridView1.SelectedRowsCount; j++)
                        {
                            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(j).ToString()));

                            dtOriginal.Rows[j]["PRECOUNITARIO_ANTERIOR"] = dtOriginal.Rows[j]["VCLIFORTABPRECOITEM.PRECOUNITARIO"];

                            row["VCLIFORTABPRECOITEM.PRECOUNITARIO"] = Valor_Processo + Convert.ToDecimal(row["VCLIFORTABPRECOITEM.PRECOUNITARIO"]);
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("ATENÇÃO: Todos os registros serão alterados, deseja continuar?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            for (int i = 0; i < dtOriginal.Rows.Count; i++)
                            {
                                dtOriginal.Rows[i]["PRECOUNITARIO_ANTERIOR"] = dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"];

                                dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"] = Valor_Processo + Convert.ToDecimal(dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"]);
                            }
                        }
                        else
                        {
                            return;
                        }
                    }

                    break;
                case 3:

                    if (gridView1.SelectedRowsCount >= 1)
                    {
                        for (int j = 0; j < gridView1.SelectedRowsCount; j++)
                        {
                            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(j).ToString()));

                            Result = ((Convert.ToDecimal(row["VCLIFORTABPRECOITEM.PRECOUNITARIO"]) * Valor_Processo) / 100);

                            dtOriginal.Rows[j]["PRECOUNITARIO_ANTERIOR"] = dtOriginal.Rows[j]["VCLIFORTABPRECOITEM.PRECOUNITARIO"];

                            row["VCLIFORTABPRECOITEM.PRECOUNITARIO"] = Convert.ToDecimal(row["VCLIFORTABPRECOITEM.PRECOUNITARIO"]) + Result;
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("ATENÇÃO: Todos os registros serão alterados, deseja continuar?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            for (int i = 0; i < dtOriginal.Rows.Count; i++)
                            {
                                Result = ((Convert.ToDecimal(dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"]) * Valor_Processo) / 100);

                                dtOriginal.Rows[i]["PRECOUNITARIO_ANTERIOR"] = dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"];

                                dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"] = Convert.ToDecimal(dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"]) + Result;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }

                    break;
                case 4:

                    if (gridView1.SelectedRowsCount >= 1)
                    {
                        for (int j = 0; j < gridView1.SelectedRowsCount; j++)
                        {
                            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(j).ToString()));

                            dtOriginal.Rows[j]["PRECOUNITARIO_ANTERIOR"] = dtOriginal.Rows[j]["VCLIFORTABPRECOITEM.PRECOUNITARIO"];

                            row["VCLIFORTABPRECOITEM.PRECOUNITARIO"] = Convert.ToDecimal(row["VCLIFORTABPRECOITEM.PRECOUNITARIO"]) - Valor_Processo;
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("ATENÇÃO: Todos os registros serão alterados, deseja continuar?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            for (int i = 0; i < dtOriginal.Rows.Count; i++)
                            {
                                dtOriginal.Rows[i]["PRECOUNITARIO_ANTERIOR"] = dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"];

                                dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"] = Convert.ToDecimal(dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"]) - Valor_Processo;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }

                    break;
                case 5:

                    if (gridView1.SelectedRowsCount >= 1)
                    {
                        for (int j = 0; j < gridView1.SelectedRowsCount; j++)
                        {
                            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(j).ToString()));

                            Result = ((Convert.ToDecimal(row["VCLIFORTABPRECOITEM.PRECOUNITARIO"]) * Valor_Processo) / 100);

                            dtOriginal.Rows[j]["PRECOUNITARIO_ANTERIOR"] = dtOriginal.Rows[j]["VCLIFORTABPRECOITEM.PRECOUNITARIO"];

                            row["VCLIFORTABPRECOITEM.PRECOUNITARIO"] = Convert.ToDecimal(row["VCLIFORTABPRECOITEM.PRECOUNITARIO"]) - Result;
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("ATENÇÃO: Todos os registros serão alterados, deseja continuar?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            for (int i = 0; i < dtOriginal.Rows.Count; i++)
                            {
                                Result = ((Convert.ToDecimal(dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"]) * Valor_Processo) / 100);

                                dtOriginal.Rows[i]["PRECOUNITARIO_ANTERIOR"] = dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"];

                                dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"] = Convert.ToDecimal(dtOriginal.Rows[i]["VCLIFORTABPRECOITEM.PRECOUNITARIO"]) - Result;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        private void inserirValorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processos = 1;

            frmProcessosTabelaPreco frm = new frmProcessosTabelaPreco();
            frm.Processos_Selecionado = processos;
            frm.ShowDialog();
            Valor_Processo = frm.Valor;
            TipoProcesso = "INSERIR VALOR";

            if (frm.Cancelar == true)
            {
                return;
            }
            else
            {
                Processos();
            }
        }

        private void acrescentarValorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processos = 2;

            frmProcessosTabelaPreco frm = new frmProcessosTabelaPreco();
            frm.Processos_Selecionado = processos;
            frm.ShowDialog();
            Valor_Processo = frm.Valor;
            TipoProcesso = "ACRESCENTAR VALOR";

            if (frm.Cancelar == true)
            {
                return;
            }
            else
            {
                Processos();
            }
        }

        private void acrescentarPercentualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processos = 3;

            frmProcessosTabelaPreco frm = new frmProcessosTabelaPreco();
            frm.Processos_Selecionado = processos;
            frm.ShowDialog();
            Valor_Processo = frm.Valor;
            TipoProcesso = "ACRESCENTAR PERCENTUAL";

            if (frm.Cancelar == true)
            {
                return;
            }
            else
            {
                Processos();
            }
        }

        private void reduzirValorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processos = 4;

            frmProcessosTabelaPreco frm = new frmProcessosTabelaPreco();
            frm.Processos_Selecionado = processos;
            frm.ShowDialog();
            Valor_Processo = frm.Valor;
            TipoProcesso = "REDUZIR VALOR";

            if (frm.Cancelar == true)
            {
                return;
            }
            else
            {
                Processos();
            }
        }

        private void reduzirPercentualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processos = 5;

            frmProcessosTabelaPreco frm = new frmProcessosTabelaPreco();
            frm.Processos_Selecionado = processos;
            frm.ShowDialog();
            Valor_Processo = frm.Valor;
            TipoProcesso = "REDUZIR PERCENTUAL";

            if (frm.Cancelar == true)
            {
                return;
            }
            else
            {
                Processos();
            }
        }

        #endregion

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            if (validar() == false)
            {
                return;
            }
            else
            {
                SalvarEdicao();
                if (lpFilial.txtcodigo.Text != Codfilial)
                {
                    AtualizaTabelaPreco();
                }                             
                CarregaGrid("WHERE IDTABELA ='" + IdTabela + "'");
            }
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (edita == true)
            {
                if (SalvarEdicao() == true && AtualizaTabelaPreco() == true)
                {
                    this.Dispose();
                }
            }
            if (SalvarEdicao() == true)
            {
                this.Dispose();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
