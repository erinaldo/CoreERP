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

namespace PS.Glb.New.Visao
{
    public partial class frmVisaoTabelaPreco : Form
    {
        string tabela = "VCLIFORTABPRECO";
        string query = string.Empty;
        public bool consulta = false;
        public string codMenu = string.Empty;
        public string IdTabela;
        List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;

        //Variaveis para o processo
        public int processos;
        public decimal Valor_Processo;
        public decimal Result;
        public string CodProduto;
        public string CodFilial;
        public string TipoProcesso;

        DataTable dtOriginal;

        public frmVisaoTabelaPreco(string _query, Form frmprin, string _CodMenu)
        {
            InitializeComponent();
            codMenu = _CodMenu;
            tabelasFilhas.Clear();
            this.MdiParent = frmprin;
            query = _query;
            CarregaGrid(query);
            getAcesso(codMenu);
        }

        public frmVisaoTabelaPreco(string where)
        {
            InitializeComponent();
            string _where = where;
            CarregaGrid(query);
        }

        public frmVisaoTabelaPreco(ref NewLookup lookup)
        {
            InitializeComponent();
            query = lookup.whereVisao;
            CarregaGrid(query);
            this.lookup = lookup;
        }

        private void frmVisaoTabelaPreco_Load(object sender, EventArgs e)
        {
            btnFiltros.Enabled = false;
            toolStripDropDownButton2.Enabled = false;
            toolStripDropDownButton4.Enabled = false;
        }

        private void getAcesso(string CodMenu)
        {
            bool permiteEditar = true;

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO, EXCLUSAO, CONSULTA, INCLUSAO FROM GPERMISSAOMENU WHERE CODMENU = ? AND CODPERFIL = ?", new object[] { "btnUtilitarios_TabelaPreco", AppLib.Context.Perfil });
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dt.Rows[i]["EDICAO"]) == false)
                    {
                        btnEditar.Enabled = false;
                        permiteEditar = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["CONSULTA"]) == true)
                    {
                        btnEditar.Enabled = true;
                        permiteEditar = true;
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["EXCLUSAO"]) == false)
                    {
                        btnExcluir.Enabled = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["INCLUSAO"]) == false)
                    {
                        btnNovo.Enabled = false;
                    }
                }
            }
            else
            {
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                btnNovo.Enabled = false;
            }
        }

        private void CarregaGrid(string where)
        {
            string relacao = "INNER JOIN VCLIFOR ON VCLIFORTABPRECO.CODEMPRESA = VCLIFOR.CODEMPRESA AND VCLIFORTABPRECO.CODCLIFOR = VCLIFOR.CODCLIFOR ORDER BY IDTABELA";
            List<string> tabelasFilhas = new List<string>();
            tabelasFilhas.Add("VCLIFOR");

            try
            {
                string sql = string.Empty;

                sql = new Class.Utilidades().getVisao(tabela, relacao, tabelasFilhas, where);

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

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregaGrid(query);
        }

        private void btnAgrupar_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsView.ShowGroupPanel == true)
            {
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.ClearGrouping();
                btnAgrupar.Text = "Agrupar";
            }
            else
            {
                gridView1.OptionsView.ShowGroupPanel = true;
                btnAgrupar.Text = "Desagrupar";
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
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

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            CarregaGrid(query);
        }

        private void atualizaColuna(DataRow dr)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECO WHERE CODCLIFOR = ? AND CODEMPRESA = ? AND IDTABELA = ?", new object[] { dr["VCLIFORTABPRECO.CODCLIFOR"], AppLib.Context.Empresa, IdTabela });

            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                if (gridView1.Columns[i].ReadOnly == false)
                {
                    try
                    {
                        dr[gridView1.Columns[i].FieldName] = dr[gridView1.Columns[i].FieldName] = dt.Rows[0][gridView1.Columns[i].FieldName.ToString().Remove(0, gridView1.Columns[i].FieldName.ToString().IndexOf(".") + 1)].ToString();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void btnSalvarLayout_Click(object sender, EventArgs e)
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
                    //FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                using (PS.Glb.New.Cadastros.frmCadastroTabelaPreco tabela = new Cadastros.frmCadastroTabelaPreco())
                {
                    tabela.edita = false;
                    tabela.ShowDialog();
                    CarregaGrid(query);
                }
            }
            else
            {
                PS.Glb.New.Cadastros.frmCadastroTabelaPreco tabela = new Cadastros.frmCadastroTabelaPreco(ref this.lookup);
                tabela.edita = false;
                tabela.ShowDialog();
                this.Dispose();
            }
        }

        private void Atualizar()
        {
            if (this.lookup == null)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    PS.Glb.New.Cadastros.frmCadastroTabelaPreco tabela = new Cadastros.frmCadastroTabelaPreco();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    tabela.IdTabela = row1["VCLIFORTABPRECO.IDTABELA"].ToString();
                    tabela.edita = true;
                    tabela.ShowDialog();
                    atualizaColuna(row1);
                }
            }
            else
            {
                ////////////////
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (consulta == true)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    IdTabela = row1["VCLIFORTABPRECO.IDTABELA"].ToString();
                    this.Dispose();
                }
                else
                {
                    Atualizar();
                    CarregaGrid(query);
                }
            }
            else
            {
                Atualizar();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    PS.Glb.New.Cadastros.frmCadastroTabelaPreco tabela = new Cadastros.frmCadastroTabelaPreco();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    tabela.IdTabela = row1["VCLIFORTABPRECO.IDTABELA"].ToString();
                    tabela.Codcliente = row1["VCLIFORTABPRECO.CODCLIFOR"].ToString();
                    IdTabela = tabela.IdTabela;
                    tabela.edita = true;
                    tabela.ShowDialog();
                    atualizaColuna(row1);
                    CarregaGrid(query);
                }
                else
                {
                    PS.Glb.New.Cadastros.frmCadastroSerie serie = new Cadastros.frmCadastroSerie();
                    serie.edita = false;
                }
            }
            else
            {
                gridControl1_DoubleClick(sender, e);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir os registros selecionados?", "Confirmação.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM VCLIFORTABPRECO WHERE IDTABELA = ? AND CODEMPRESA = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa });
                    }
                    MessageBox.Show("Registro excluído com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregaGrid(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        #region Processos

        private void Processos()
        {
            switch (processos)
            {
                case 1:

                    if (string.IsNullOrEmpty(CodProduto))
                    {
                        for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                            DataTable dtitens = new DataTable();

                            if (string.IsNullOrEmpty(CodFilial))
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODFILIAL = ?", new object[] { Valor_Processo, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], dtitens.Rows[ii]["CODFILIAL"] });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Valor_Processo, 0, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                            else
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ? AND CODFILIAL = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa, CodFilial });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODFILIAL = ?", new object[] { Valor_Processo, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], CodFilial });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Valor_Processo, 0, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                            DataTable dtitens = new DataTable();

                            if (string.IsNullOrEmpty(CodFilial))
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ? AND CODPRODUTO = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa, CodProduto });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODPRODUTO = ?", new object[] { Valor_Processo, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], dtitens.Rows[ii]["CODPRODUTO"] });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Valor_Processo, 0, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                            else
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ? AND CODFILIAL = ? AND CODPRODUTO = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa, CodFilial, CodProduto });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODFILIAL = ? AND CODPRODUTO = ?", new object[] { Valor_Processo, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], CodFilial, CodProduto });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Valor_Processo, 0, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                        }
                    }

                    break;
                case 2:

                    if (string.IsNullOrEmpty(CodProduto))
                    {
                        for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                            DataTable dtitens = new DataTable();

                            if (string.IsNullOrEmpty(CodFilial))
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    Result = Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) + Valor_Processo;
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODFILIAL = ?", new object[] { Result, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], dtitens.Rows[ii]["CODFILIAL"] });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Result, 0, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                            else
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ? AND CODFILIAL = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa, CodFilial });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    Result = Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) + Valor_Processo;
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODFILIAL = ?", new object[] { Result, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], CodFilial });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Result, 0, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                            DataTable dtitens = new DataTable();

                            if (string.IsNullOrEmpty(CodFilial))
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ? AND CODPRODUTO = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa, CodProduto });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    Result = Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) + Valor_Processo;
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODPRODUTO = ?", new object[] { Result, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], dtitens.Rows[ii]["CODPRODUTO"] });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Result, 0, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                            else
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ? AND CODFILIAL = ? AND CODPRODUTO = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa, CodFilial, CodProduto });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    Result = Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) + Valor_Processo;
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODFILIAL = ? AND CODPRODUTO = ?", new object[] { Result, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], CodFilial, CodProduto });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Result, 0, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                        }
                    }

                    break;
                case 3:

                    if (string.IsNullOrEmpty(CodProduto))
                    {
                        for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                            DataTable dtitens = new DataTable();

                            if (string.IsNullOrEmpty(CodFilial))
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    Result = Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) + ((Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) * Valor_Processo) / 100);
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODFILIAL = ?", new object[] { Result, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], dtitens.Rows[ii]["CODFILIAL"] });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Result, Valor_Processo, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                            else
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ? AND CODFILIAL = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa, CodFilial });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    Result = Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) + ((Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) * Valor_Processo) / 100);
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODFILIAL = ?", new object[] { Result, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], CodFilial });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Result, Valor_Processo, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                            DataTable dtitens = new DataTable();

                            if (string.IsNullOrEmpty(CodFilial))
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ? AND CODPRODUTO = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa, CodProduto });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    Result = Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) + ((Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) * Valor_Processo) / 100);
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODPRODUTO = ?", new object[] { Result, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], dtitens.Rows[ii]["CODPRODUTO"] });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Result, Valor_Processo, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                            else
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ? AND CODFILIAL = ? AND CODPRODUTO = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa, CodFilial, CodProduto });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    Result = Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) + ((Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) * Valor_Processo) / 100);
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODFILIAL = ? AND CODPRODUTO = ?", new object[] { Result, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], CodFilial, CodProduto });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Result, Valor_Processo, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                        }
                    }

                    break;
                case 4:

                    if (string.IsNullOrEmpty(CodProduto))
                    {
                        for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                            DataTable dtitens = new DataTable();

                            if (string.IsNullOrEmpty(CodFilial))
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    Result = Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) - Valor_Processo;
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODFILIAL = ?", new object[] { Result, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], dtitens.Rows[ii]["CODFILIAL"] });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Result, 0, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                            else
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ? AND CODFILIAL = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa, CodFilial });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    Result = Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) - Valor_Processo;
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODFILIAL = ?", new object[] { Result, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], CodFilial });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Result, 0, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                            DataTable dtitens = new DataTable();

                            if (string.IsNullOrEmpty(CodFilial))
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ? AND CODPRODUTO = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa, CodProduto });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    Result = Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) - Valor_Processo;
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODPRODUTO = ?", new object[] { Result, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], dtitens.Rows[ii]["CODPRODUTO"] });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Result, Valor_Processo, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                            else
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ? AND CODFILIAL = ? AND CODPRODUTO = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa, CodFilial, CodProduto });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    Result = Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) - Valor_Processo;
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODFILIAL = ? AND CODPRODUTO = ?", new object[] { Result, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], CodFilial, CodProduto });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Result, Valor_Processo, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                        }
                    }

                    break;
                case 5:

                    if (string.IsNullOrEmpty(CodProduto))
                    {
                        for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                            DataTable dtitens = new DataTable();

                            if (string.IsNullOrEmpty(CodFilial))
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    Result = Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) - ((Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) * Valor_Processo) / 100);
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODFILIAL = ?", new object[] { Result, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], dtitens.Rows[ii]["CODFILIAL"] });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Result, Valor_Processo, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                            else
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ? AND CODFILIAL = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa, CodFilial });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    Result = Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) - ((Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) * Valor_Processo) / 100);
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODFILIAL = ?", new object[] { Result, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], CodFilial });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Result, Valor_Processo, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                            DataTable dtitens = new DataTable();

                            if (string.IsNullOrEmpty(CodFilial))
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ? AND CODPRODUTO = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa, CodProduto });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    Result = Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) - ((Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) * Valor_Processo) / 100);
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODPRODUTO = ?", new object[] { Result, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], dtitens.Rows[ii]["CODPRODUTO"] });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Result, Valor_Processo, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
                            else
                            {
                                dtitens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ? AND CODFILIAL = ? AND CODPRODUTO = ?", new object[] { row["VCLIFORTABPRECO.IDTABELA"], AppLib.Context.Empresa, CodFilial, CodProduto });
                                for (int ii = 0; ii < dtitens.Rows.Count; ii++)
                                {
                                    Result = Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) - ((Convert.ToDecimal(dtitens.Rows[ii]["PRECOUNITARIO"]) * Valor_Processo) / 100);
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VCLIFORTABPRECOITEM SET PRECOUNITARIO = ? WHERE CODEMPRESA = ? AND IDTABELA = ? AND CODFILIAL = ? AND CODPRODUTO = ?", new object[] { Result, AppLib.Context.Empresa, row["VCLIFORTABPRECO.IDTABELA"], CodFilial, CodProduto });
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VCLIFORTABPRECOLOG (CODEMPRESA, CODFILIAL, CODCLIFOR, CODPRODUTO, CODMOEDA, PRECOUNITARIOORIGINAL, PRECOUNITARIOATUALIZADO, PERCATUALIZACAO, USUARIOALTERACAO, DATAALTERACAO, TIPOPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, dtitens.Rows[ii]["CODFILIAL"], row["VCLIFORTABPRECO.CODCLIFOR"], dtitens.Rows[ii]["CODPRODUTO"], dtitens.Rows[ii]["CODMOEDA"], dtitens.Rows[ii]["PRECOUNITARIO"], Result, Valor_Processo, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now.ToShortDateString()), TipoProcesso });
                                }
                            }
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

            string idtabela = "";
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                idtabela = idtabela + row1["VCLIFORTABPRECO.IDTABELA"].ToString() + (i == (gridView1.SelectedRowsCount - 1) ? "" : ",");
            }

            PS.Glb.New.Cadastros.frmProcessosVisaoTabelaPreco frm = new Cadastros.frmProcessosVisaoTabelaPreco();
            frm.Processos_Selecionado = processos;
            frm.idtabela = idtabela;
            frm.ShowDialog();
            Valor_Processo = frm.Valor;
            CodProduto = frm.Codproduto;
            CodFilial = frm.codFilial;
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

            string idtabela = "";
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                idtabela = idtabela + row1["VCLIFORTABPRECO.IDTABELA"].ToString() + (i == (gridView1.SelectedRowsCount - 1) ? "" : ",");
            }

            PS.Glb.New.Cadastros.frmProcessosVisaoTabelaPreco frm = new Cadastros.frmProcessosVisaoTabelaPreco();
            frm.Processos_Selecionado = processos;
            frm.idtabela = idtabela;
            frm.ShowDialog();
            Valor_Processo = frm.Valor;
            CodProduto = frm.Codproduto;
            CodFilial = frm.codFilial;
            TipoProcesso = "ACRESCENTAR VALOR";

            if (frm.Cancelar == true)
            {
                return;
            }
            else
            {
                Processos();
            }
            CodFilial = string.Empty;
        }

        private void acrescentarPercentualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processos = 3;

            string idtabela = "";
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                idtabela = idtabela + row1["VCLIFORTABPRECO.IDTABELA"].ToString() + (i == (gridView1.SelectedRowsCount - 1) ? "" : ",");
            }

            PS.Glb.New.Cadastros.frmProcessosVisaoTabelaPreco frm = new Cadastros.frmProcessosVisaoTabelaPreco();
            frm.Processos_Selecionado = processos;
            frm.idtabela = idtabela;
            frm.ShowDialog();
            Valor_Processo = frm.Valor;
            CodProduto = frm.Codproduto;
            CodFilial = frm.codFilial;
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

            string idtabela = "";
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                idtabela = idtabela + row1["VCLIFORTABPRECO.IDTABELA"].ToString() + (i == (gridView1.SelectedRowsCount - 1) ? "" : ",");
            }

            PS.Glb.New.Cadastros.frmProcessosVisaoTabelaPreco frm = new Cadastros.frmProcessosVisaoTabelaPreco();
            frm.Processos_Selecionado = processos;
            frm.idtabela = idtabela;
            frm.ShowDialog();
            Valor_Processo = frm.Valor;
            CodProduto = frm.Codproduto;
            CodFilial = frm.codFilial;
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

            string idtabela = "";
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                idtabela = idtabela + row1["VCLIFORTABPRECO.IDTABELA"].ToString() + (i == (gridView1.SelectedRowsCount - 1) ? "" : ",");
            }

            PS.Glb.New.Cadastros.frmProcessosVisaoTabelaPreco frm = new Cadastros.frmProcessosVisaoTabelaPreco();
            frm.Processos_Selecionado = processos;
            frm.idtabela = idtabela;
            frm.ShowDialog();
            Valor_Processo = frm.Valor;
            CodProduto = frm.Codproduto;
            CodFilial = frm.codFilial;
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

        private void copiarTabelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            PS.Glb.New.Cadastros.frmCopiarTabela frm = new Cadastros.frmCopiarTabela(row["VCLIFORTABPRECO.IDTABELA"].ToString(), Convert.ToInt32(row["VCLIFORTABPRECO.CODEMPRESA"].ToString()), row["VCLIFORTABPRECO.CODCLIFOR"].ToString());
            frm.ShowDialog();
            CarregaGrid(query);
        }
    }

    #endregion
}
