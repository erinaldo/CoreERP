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
    public partial class frmVisaoLote : Form
    {
        public Form pai = null;
        public bool consulta = false;
        string tabela = "VPRODUTOLOTE";
        string query = string.Empty;
        public string codMenu = string.Empty;
        private bool permiteEditar = true;

        List<string> tabelasFilhas = new List<string>();

        //Variaveis para usar quando a tela abre para consulta.
        public string CodLote;
        public string Nome;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmVisaoLote(string _query, Form frmprin, string _CodMenu)
        {
            InitializeComponent();
            codMenu = _CodMenu;
            this.MdiParent = frmprin;
            tabelasFilhas.Clear();
            query = _query;
            CarregaGridLoteEx(query);
            //CarregaGridLote(query);
            //CarregaGrid(query);
            getAcesso(codMenu);
        }

        public frmVisaoLote(ref NewLookup lookup)
        {
            InitializeComponent();
            query = lookup.whereVisao;
            //CarregaGrid(query);
            //CarregaGridLote(query);
            CarregaGridLoteEx(query);
            this.lookup = lookup;
        }

        private void frmVisaoLote_Load(object sender, EventArgs e)
        {
            btnNovo.Enabled = false;
            btnFiltros.Enabled = false;
            btnProcessos.Enabled = false;
            btnExportar.Enabled = false;
        }

        private void getAcesso(string CodMenu)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO, EXCLUSAO, CONSULTA, INCLUSAO FROM GPERMISSAOMENU WHERE CODMENU = ? AND CODPERFIL = ?", new object[] { "btnUtilitarios_Lote", AppLib.Context.Perfil });
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

        public void CarregaGrid(string where)
        {
            try
            {
                string sql = string.Empty;

                sql = new Class.Utilidades().getVisao(tabela, string.Empty, tabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

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

        public void CarregaGridLote(string where)
        {
            try
            {
                string sql = string.Empty;

                tabelasFilhas.Add("VSALDOESTOQUELOTE");

                string Relacionamento = "LEFT OUTER JOIN VSALDOESTOQUELOTE ON VSALDOESTOQUELOTE.CODEMPRESA = VPRODUTOLOTE.CODEMPRESA AND VSALDOESTOQUELOTE.CODFILIAL = VPRODUTOLOTE.CODFILIAL AND VSALDOESTOQUELOTE.CODLOCAL = VPRODUTOLOTE.CODLOCAL AND VSALDOESTOQUELOTE.CODLOTE = VPRODUTOLOTE.CODLOTE AND VSALDOESTOQUELOTE.CODPRODUTO = VPRODUTOLOTE.CODPRODUTO";

                sql = new Class.Utilidades().getVisao(tabela, Relacionamento, tabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

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

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            //CarregaGrid(query);
            //CarregaGridLote(query);
            CarregaGridLoteEx(query);
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            //CarregaGrid(query);
            //CarregaGridLote(query);
            CarregaGridLoteEx(query);
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
                    FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
                //CarregaGrid(query);
                //CarregaGridLote(query);
                CarregaGridLoteEx(query);
            }
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

        private void atualizaColuna(DataRow dr)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPRODUTOLOTE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODLOTE = ? AND CODPRODUTO = ? AND NUMERO = ?", new object[] { AppLib.Context.Empresa, dr["VPRODUTOLOTE.CODFILIAL"], dr["VPRODUTOLOTE.CODLOCAL"], dr["VPRODUTOLOTE.CODLOTE"], dr["VPRODUTOLOTE.CODPRODUTO"], dr["VPRODUTOLOTE.NUMERO"] });

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

        private void Atualizar()
        {
            if (this.lookup == null)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    PS.Glb.New.Cadastros.frmCadastroLote lote = new Cadastros.frmCadastroLote();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    lote.CodLote = row1["VPRODUTOLOTE.CODLOTE"].ToString();
                    lote.CodFilial = Convert.ToInt32(row1["VPRODUTOLOTE.CODFILIAL"]);
                    lote.Numero = row1["VPRODUTOLOTE.NUMERO"].ToString();
                    lote.edita = true;
                    lote.ShowDialog();
                    atualizaColuna(row1);
                }
            }
            else
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                lookup.txtconteudo.Text = row1[tabela + ".DESCRICAO"].ToString().ToUpper();
                lookup.txtcodigo.Text = row1[tabela + ".CODLOTE"].ToString();
                lookup.ValorCodigoInterno = row1[tabela + ".CODLOTE"].ToString();
                this.Dispose();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                PS.Glb.New.Cadastros.frmCadastroLote lote = new Cadastros.frmCadastroLote();
                lote.edita = false;
                lote.ShowDialog();
                //CarregaGrid(query);
                //CarregaGridLote(query);
                CarregaGridLoteEx(query);
            }
            else
            {
                PS.Glb.New.Cadastros.frmCadastroLote lote = new Cadastros.frmCadastroLote(ref this.lookup);
                lote.edita = false;
                lote.ShowDialog();
                this.Dispose();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    PS.Glb.New.Cadastros.frmCadastroLote lote = new Cadastros.frmCadastroLote();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    lote.CodLote = row1["VPRODUTOLOTE.CODLOTE"].ToString();
                    lote.CodFilial = Convert.ToInt32(row1["VPRODUTOLOTE.CODFILIAL"]);
                    lote.Numero = row1["VPRODUTOLOTE.NUMERO"].ToString();
                    lote.edita = true;
                    lote.ShowDialog();
                    atualizaColuna(row1);
                }
                else
                {
                    PS.Glb.New.Cadastros.frmCadastroLote lote = new Cadastros.frmCadastroLote();
                    lote.edita = false;
                }
            }
            else
            {
                gridControl1_DoubleClick(sender, e);
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (consulta == true)
                {
                    // Deixar comentado por hora
                    //DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    //CodLote = row1["VPRODUTOLOTE.CODLOTE"].ToString();
                    //this.Dispose();
                }
                else
                {
                    Atualizar();
                }
            }
            else
            {
                Atualizar();
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

                        if (new Class.Utilidades().Excluir("CODLOTE", "VPRODUTOLOTE", row["VPRODUTOLOTE.CODLOTE"].ToString()) == true)
                        {
                            if (gridView1.SelectedRowsCount > 0)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Exclusão não permitida, existem produtos associados a este(s) lote(s).", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    MessageBox.Show("Registro excluído com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //CarregaGridLote(query);
                    CarregaGridLoteEx(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CarregaGridLoteEx(string where)
        {
            try
            {
                // Tabela pai
                string sql = string.Empty;
                tabelasFilhas.Add("VSALDOESTOQUELOTE");
                string Relacionamento = "LEFT OUTER JOIN VSALDOESTOQUELOTE ON VSALDOESTOQUELOTE.CODEMPRESA = VPRODUTOLOTE.CODEMPRESA AND VSALDOESTOQUELOTE.CODFILIAL = VPRODUTOLOTE.CODFILIAL AND VSALDOESTOQUELOTE.CODLOTE = VPRODUTOLOTE.CODLOTE";
                sql = new Class.Utilidades().getVisao(tabela, Relacionamento, tabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tabela filha
                string sql2 = string.Empty;
                List<string> tabelasFilhas2 = new List<string>();
                tabelasFilhas2.Clear();
                string tabela2 = "GOPERITEMLOTE";
                sql2 = new Class.Utilidades().getVisao(tabela2, string.Empty, tabelasFilhas2, where);

                if (string.IsNullOrEmpty(sql2))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                //Tabelas para popular o Dataset
                DataTable dtpai = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { });
                DataTable dtfilha = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql2, new object[] { });

                DataSet DS = new DataSet();

                dtpai.TableName = "VPRODUTOLOTE";
                dtfilha.TableName = "GOPERITEMLOTE";

                DS.Tables.Add(dtpai);
                DS.Tables.Add(dtfilha);

                //Relações entre as tabelas
                DataColumn[] _pai = new DataColumn[] {DS.Tables[0].Columns["VPRODUTOLOTE.CODEMPRESA"],
                                                              DS.Tables[0].Columns["VPRODUTOLOTE.CODLOTE"]};

                DataColumn[] _filha = new DataColumn[] {DS.Tables[1].Columns["GOPERITEMLOTE.CODEMPRESA"],
                                                              DS.Tables[1].Columns["GOPERITEMLOTE.CODLOTE"]};
                //Criação da relação
                DS.Relations.Add("Lote", _pai, _filha);

                //Populando o Dataset
                gridControl1.DataSource = DS.Tables[0];

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

        private void gridView1_MasterRowExpanded(object sender, DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs e)
        {
            GridView dView = gridView1.GetDetailView(e.RowHandle, 0) as GridView;

            dView.Columns["GOPERITEMLOTE.CODEMPRESA"].Visible = false;
            dView.Columns["GOPERITEMLOTE.CODLOTE"].Visible = false;

            DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA,DESCRICAO FROM GDICIONARIO WHERE TABELA in (?)", new object[] { "GOPERITEMLOTE" });
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { "GOPERITEMLOTE", AppLib.Context.Usuario });
            for (int i = 0; i < dView.Columns.Count; i++)
            {
                dView.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                DataRow result = dic.Rows.Find(new object[] { dView.Columns[i].FieldName.ToString() });
                if (result != null)
                {
                    dView.Columns[i].Caption = result["DESCRICAO"].ToString();
                }
            }
            dView.BestFitColumns();
        }

        public void CarregaVisaoEstoque(string _codproduto)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                splitContainer1.Panel2Collapsed = false;
                TabEstoque.PageVisible = true;
                DataTable dtEstoque = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
 X.Empresa
,X.Filial
,X.Local
,X.Saldo AS 'Saldo Atual'
,X.UN AS 'Unidade'
,X.Pedidos AS 'Pedidos Pendentes'
,(X.Saldo - X.Pedidos) AS 'Saldo Disponível'

FROM(
SELECT
 VSALDOESTOQUE.CODEMPRESA as 'Empresa'  
,VSALDOESTOQUE.CODFILIAL as 'Filial'  
,VSALDOESTOQUE.CODLOCAL as 'Local'  
,VSALDOESTOQUE.SALDOFINAL as 'Saldo'  
,VSALDOESTOQUE.CODUNIDCONTROLE as 'UN'  

,ISNULL((SELECT 
ISNULL(SUM(
	CASE
		WHEN GOPER.CODSTATUS = 0 THEN ISNULL(GOPERITEM.QUANTIDADE,0)
		WHEN GOPER.CODSTATUS = 5 THEN ISNULL(GOPERITEM.QUANTIDADE_SALDO,0)
		WHEN GOPER.CODSTATUS = 7 THEN ISNULL(GOPERITEM.QUANTIDADE,0)
		ELSE 0
	END),0) 

		FROM 
		VPRODUTO
		INNER JOIN GOPERITEM ON GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO
		INNER JOIN GOPER ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER

			WHERE 
				GOPER.CODEMPRESA = VSALDOESTOQUE.CODEMPRESA
			AND GOPER.CODTIPOPER = '2.1.02'
			AND GOPER.CODSTATUS IN (0,5,7)
			AND GOPERITEM.CODPRODUTO = VSALDOESTOQUE.CODPRODUTO),0) AS 'Pedidos'



FROM   
VSALDOESTOQUE  
  
WHERE 
VSALDOESTOQUE.CODEMPRESA = ?  
AND VSALDOESTOQUE.CODPRODUTO = ?

)X", new object[] { AppLib.Context.Empresa, _codproduto });

                gridControl2.DataSource = dtEstoque;
                gridView2.BestFitColumns();
            }
        }

        private void estoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            CarregaVisaoEstoque(getProduto(row1));
        }

        private void fecharAnexosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = true;
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                return;
            }

            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            if (splitContainer1.Panel2Collapsed == false)
            {
                if (TabEstoque.PageVisible == true)
                {
                    CarregaVisaoEstoque(getProduto(row1));
                }
            }
        }

        private string getProduto(DataRow row)
        {
            string Produto = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT GOPERITEMLOTE.CODPRODUTO
                                                                                FROM GOPERITEMLOTE
                                                                                INNER JOIN VPRODUTOLOTE ON GOPERITEMLOTE.CODEMPRESA = VPRODUTOLOTE.CODEMPRESA
                                                                                AND GOPERITEMLOTE.CODLOTE = VPRODUTOLOTE.CODLOTE
                                                                                WHERE VPRODUTOLOTE.CODLOTE = ?", new object[] { row["VPRODUTOLOTE.CODLOTE"] }).ToString();

            return Produto;
        }
    }
}
