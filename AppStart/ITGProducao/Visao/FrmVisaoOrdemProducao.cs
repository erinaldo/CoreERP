using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ITGProducao.Class;
using ITGProducao.Controles;
using ITGProducao.Filtros;
using ITGProducao.Formularios;
using PS.Glb.New;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITGProducao.Visao
{
    public partial class FrmVisaoOrdemProducao : Form
    {
        string tabela = "PORDEM";
        string relacionamento = string.Empty;
        string query = string.Empty;
        List<string> tabelasFilhas = new List<string>();

        //Variaveis para usar quando a tela abre para consulta.
        public bool consulta = false;
        public string codCalendario;
        public string nome;

        //Variaveis para NewLookup
        private NewLookup lookup;

        GridView detailView;

        public FrmVisaoOrdemProducao(string _query, Form frmprin)
        {
            InitializeComponent();
            this.MdiParent = (Form)frmprin;
            query = _query;

            carregaGrid(query);
        }

        public FrmVisaoOrdemProducao(string _where)
        {
            InitializeComponent();
            query = _where;
            carregaGrid(query);
        }

        public FrmVisaoOrdemProducao(ref NewLookup lookup)
        {
            InitializeComponent();

            query = lookup.whereVisao;
            carregaGrid(query);

            this.lookup = lookup;
        }

        public void carregaGrid(string where)
        {
            try
            {
                string _parent = " AND PORDEM.CODSEQPAI is null ";
                string _child = " AND PORDEM.CODSEQPAI is not null ";

                string sql = new PS.Glb.Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                DataTable dtParent = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql+ _parent);
                DataTable dtchild = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql + _child);

                DataSet DS = new DataSet();

                dtParent.TableName = "PORDEM_PARENT";
                dtchild.TableName = "PORDEM_CHILD";

                DS.Tables.Add(dtParent);
                DS.Tables.Add(dtchild);

                DataColumn[] parent = new DataColumn[] { DS.Tables[0].Columns["PORDEM.CODEMPRESA"],
                                                         DS.Tables[0].Columns["PORDEM.CODFILIAL"],
                                                         DS.Tables[0].Columns["PORDEM.CODIGOOP"]};

                DataColumn[] child = new DataColumn[] { DS.Tables[1].Columns["PORDEM.CODEMPRESA"],
                                                         DS.Tables[1].Columns["PORDEM.CODFILIAL"],
                                                         DS.Tables[1].Columns["PORDEM.CODIGOOP"]};   

                DS.Relations.Add("Detalhes", parent, child);

                gridControl1.DataSource = DS.Tables[0];

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    if (gridView1.Columns[i].FieldName == "PORDEM.STATUSOP")
                    {
                        gridView1.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
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
            carregaGrid(query);
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            carregaGrid(query);
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
                carregaGrid(query);
            }
        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            FrmFiltroOrdemProducao frm = new FrmFiltroOrdemProducao();
            frm.aberto = true;
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.condicao))
            {
                query = frm.condicao;
                carregaGrid(query);
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DataRow SelectedRow;

            if (detailView == null)
            {
                SelectedRow = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            }
            else
            {
                SelectedRow = detailView.GetDataRow(Convert.ToInt32(detailView.GetSelectedRows().GetValue(0).ToString()));
            }

            string codigoestrutura = SelectedRow[tabela + ".CODESTRUTURA"].ToString();
            string codigorev = SelectedRow[tabela + ".REVESTRUTURA"].ToString();
            string codigoop = SelectedRow[tabela + ".CODIGOOP"].ToString();
            string seqop = SelectedRow[tabela + ".SEQOP"].ToString();

            if (SelectedRow[tabela + ".STATUSOP"].ToString() != "1")
            {
                MessageBox.Show("Esta Ordem de Produção não pode ser excluída", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                    conn.BeginTransaction();
                    try
                    {
                        if (seqop == "001")
                        {
                            conn.ExecTransaction("DELETE FROM PORDEMENTRADA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigoop });
                            conn.ExecTransaction("DELETE FROM PORDEMCONSUMO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial,  codigoop });
                            conn.ExecTransaction("DELETE FROM PORDEMAPTORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial,  codigoop });
                            conn.ExecTransaction("DELETE FROM PORDEMAPONTAMENTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial,  codigoop });
                            conn.ExecTransaction("DELETE FROM PORDEMALOCACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial,  codigoop });
                            conn.ExecTransaction("DELETE FROM PORDEMROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial,  codigoop });
                            conn.ExecTransaction("DELETE FROM PORDEM WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigoop });
                            conn.ExecTransaction("DELETE FROM ZORDEMPEDIDO WHERE CODEMPRESA = ? AND CODFILIALOP = ? AND CODIGOOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigoop });
                        }
                        else
                        {
                            conn.ExecTransaction("DELETE FROM PORDEMENTRADA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigoestrutura, codigorev, codigoop, seqop });
                            conn.ExecTransaction("DELETE FROM PORDEMCONSUMO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigoestrutura, codigorev, codigoop, seqop });
                            conn.ExecTransaction("DELETE FROM PORDEMAPTORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigoestrutura, codigorev, codigoop, seqop });
                            conn.ExecTransaction("DELETE FROM PORDEMAPONTAMENTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigoestrutura, codigorev, codigoop, seqop });
                            conn.ExecTransaction("DELETE FROM PORDEMALOCACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigoestrutura, codigorev, codigoop, seqop });
                            conn.ExecTransaction("DELETE FROM PORDEMROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigoestrutura, codigorev, codigoop, seqop });
                            conn.ExecTransaction("DELETE FROM PORDEM WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigoestrutura, codigorev, codigoop, seqop });
                            conn.ExecTransaction("DELETE FROM ZORDEMPEDIDO WHERE CODEMPRESA = ? AND CODFILIALOP = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigoop, seqop });
                        }
                        conn.Commit();
                        MessageBox.Show("Cadastro Excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        carregaGrid(query);
                    }
                    catch (Exception)
                    {
                        conn.Rollback();
                        throw;
                    }
                }
            }
        }

        private void atualizaColuna(DataRow dr)
        {
            carregaGrid(query);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                FrmOrdemProducao frm = new FrmOrdemProducao();
                frm.edita = false;
                //frm.MdiParent = this.MdiParent;
                frm.ShowDialog();
                carregaGrid(query);
            }
            else
            {
                FrmOrdemProducao frm = new FrmOrdemProducao(ref this.lookup);
                frm.edita = false;
                frm.ShowDialog();
                this.Dispose();
            }
        }

        private void Atualizar(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    FrmOrdemProducao frm = new FrmOrdemProducao();
                    DataRow SelectedRow;

                    if (detailView == null)
                    {
                        SelectedRow = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    }
                    else
                    {
                        SelectedRow = detailView.GetDataRow(Convert.ToInt32(detailView.GetSelectedRows().GetValue(0).ToString()));
                    }

                    frm.codOrdem = SelectedRow[tabela + ".CODIGOOP"].ToString();
                    frm.seqOrdem = SelectedRow[tabela + ".SEQOP"].ToString();
                    frm.codEstrutura = SelectedRow[tabela + ".CODESTRUTURA"].ToString();
                    frm.CodRevEstrutura = SelectedRow[tabela + ".REVESTRUTURA"].ToString();
                    frm.edita = true;
                    frm.ShowDialog();
                    atualizaColuna(SelectedRow);
                }
            }
            else
            {
                DataRow SelectedRow = detailView.GetDataRow(Convert.ToInt32(detailView.GetSelectedRows().GetValue(0).ToString()));
                lookup.txtconteudo.Text = SelectedRow[tabela + ".DESCRICAOOP"].ToString().ToUpper();

                switch (lookup.CampoCodigo_Igual_a)
                {
                    case NewLookup.CampoCodigoIguala.CampoCodigoBD:
                        lookup.txtcodigo.Text = SelectedRow[tabela + ".CODIGOOP"].ToString();
                        lookup.ValorCodigoInterno = SelectedRow[tabela + ".DESCRICAOOP"].ToString();

                        lookup.OutrasChaves.Clear();
                        lookup.OutrasChaves.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODESTRUTURA", ValorColunaChave = SelectedRow[tabela + ".CODESTRUTURA"].ToString() });
                        lookup.OutrasChaves.Add(new Newlookup_OutrasChaves { NomeColunaChave = "REVESTRUTURA", ValorColunaChave = SelectedRow[tabela + ".REVESTRUTURA"].ToString() });
                        lookup.OutrasChaves.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODIGOOP", ValorColunaChave = SelectedRow[tabela + ".CODIGOOP"].ToString() });
                        lookup.OutrasChaves.Add(new Newlookup_OutrasChaves { NomeColunaChave = "SEQOP", ValorColunaChave = SelectedRow[tabela + ".SEQOP"].ToString() });
                        break;

                    case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                        if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                        {
                            lookup.txtcodigo.Text = SelectedRow[tabela + "." + lookup.CampoCodigoInterno].ToString();
                            lookup.ValorCodigoInterno = SelectedRow[tabela + "." + lookup.CampoCodigoInterno].ToString();

                            lookup.OutrasChaves.Clear();
                            lookup.OutrasChaves.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODESTRUTURA", ValorColunaChave = SelectedRow[tabela + ".CODESTRUTURA"].ToString() });
                            lookup.OutrasChaves.Add(new Newlookup_OutrasChaves { NomeColunaChave = "REVESTRUTURA", ValorColunaChave = SelectedRow[tabela + ".REVESTRUTURA"].ToString() });
                            lookup.OutrasChaves.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODIGOOP", ValorColunaChave = SelectedRow[tabela + ".CODIGOOP"].ToString() });
                            lookup.OutrasChaves.Add(new Newlookup_OutrasChaves { NomeColunaChave = "SEQOP", ValorColunaChave = SelectedRow[tabela + ".SEQOP"].ToString() });
                        }
                        else
                        {
                            lookup.txtcodigo.Text = SelectedRow[tabela + ".CODIGOOP"].ToString();
                            lookup.ValorCodigoInterno = SelectedRow[tabela + ".DESCRICAOOP"].ToString();

                            lookup.OutrasChaves.Clear();
                            lookup.OutrasChaves.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODESTRUTURA", ValorColunaChave = SelectedRow[tabela + ".CODESTRUTURA"].ToString() });
                            lookup.OutrasChaves.Add(new Newlookup_OutrasChaves { NomeColunaChave = "REVESTRUTURA", ValorColunaChave = SelectedRow[tabela + ".REVESTRUTURA"].ToString() });
                            lookup.OutrasChaves.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODIGOOP", ValorColunaChave = SelectedRow[tabela + ".CODIGOOP"].ToString() });
                            lookup.OutrasChaves.Add(new Newlookup_OutrasChaves { NomeColunaChave = "SEQOP", ValorColunaChave = SelectedRow[tabela + ".SEQOP"].ToString() });
                        }
                        break;
                }

                this.Dispose();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Atualizar(sender,e);
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            Atualizar(sender,e);
        }

        private void gridView1_MasterRowExpanded(object sender, DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs e)
        {
            GridView dView = gridView1.GetDetailView(e.RowHandle, 0) as GridView;

            dView.Columns["PORDEM.CODEMPRESA"].Visible = false;
            dView.Columns["PORDEM.CODFILIAL"].Visible = false;

            DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA,DESCRICAO FROM GDICIONARIO WHERE TABELA in (?)", new object[] { "PORDEM" });
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
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

            //dView.OptionsView.ShowAutoFilterRow = false;
        }

        private void gridControl1_FocusedViewChanged(object sender, DevExpress.XtraGrid.ViewFocusEventArgs e)
        {
            detailView = gridControl1.FocusedView as GridView;
        }

        private void gerarEntradasDaOPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            FrmOrdemProducaoEntradas frm = new FrmOrdemProducaoEntradas(row1["PORDEM.CODIGOOP"].ToString(), row1["PORDEM.SEQOP"].ToString(), row1["PORDEM.CODESTRUTURA"].ToString(), row1["PORDEM.REVESTRUTURA"].ToString());
            frm.ShowDialog();
        }

        private void planejarOrdemDeProduçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            if (row1["PORDEM.STATUSOP"].ToString() == "1")
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                conn.BeginTransaction();

                try
                {
                    verificaVFICHAESTOQUE(row1["PORDEM.CODESTRUTURA"].ToString(), row1["PORDEM.CODIGOOP"].ToString());
                    carregaGrid(query);
                }
                catch (Exception ex)
                {
                    conn.Rollback();
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Esta ordem de produção não pode ser planejada devido ao status", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void verificaVFICHAESTOQUE(string produto, string codOrdem)
        {
            try
            {
                string Param_LocalEstoqueMP = VerificaParametro("LOCALESTOQUEMP");

                if (!string.IsNullOrEmpty(Param_LocalEstoqueMP))
                {
                    FrmOrdemProducao frmop = new FrmOrdemProducao();
                    DataTable dtRecursivo = frmop.sqlOrdemProducaoEstrutura(codOrdem);

                    List<string> _Listacomponentes = new List<string>();

                    for (int x = 0; x <= (dtRecursivo.Rows.Count - 1); x++)
                    {
                        if (!_Listacomponentes.Contains(dtRecursivo.Rows[x]["CODCOMPONENTE"].ToString()))
                        {
                            _Listacomponentes.Add(dtRecursivo.Rows[x]["CODCOMPONENTE"].ToString());
                        }

                        if (!_Listacomponentes.Contains(dtRecursivo.Rows[x]["CODESTRUTURA"].ToString()))
                        {
                            _Listacomponentes.Add(dtRecursivo.Rows[x]["CODESTRUTURA"].ToString());
                        }
                    }

                    string _componentes = "";

                    for (int x = 0; x <= (_Listacomponentes.Count() - 1); x++)
                    {
                        _componentes = _componentes + (x == 0 ? "'" + _Listacomponentes[x].ToString() + "'" : ",'" + _Listacomponentes[x].ToString() + "'");
                    }

                    DataTable dtFichaEstoque = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
                        with estoque as (
                                           select CODEMPRESA, CODPRODUTO, CODLOCAL, SEQUENCIAL, SALDOFINAL
                                           , RowDesc = ROW_NUMBER() over(partition by CODPRODUTO order by SEQUENCIAL desc)
                                             from VFICHAESTOQUE
                                            where CODEMPRESA = ?
                                              and CODLOCAL = ?
                                              and CODFILIAL = ?" +
                            (string.IsNullOrEmpty(_componentes) ? "" : "AND CODPRODUTO in (" + _componentes + ")") + @"
                                         )
                        select * from estoque where RowDesc = 1"
                    , new object[] { AppLib.Context.Empresa, Param_LocalEstoqueMP, AppLib.Context.Filial });

                    FrmOrdemProducaoNecessidadeMP frm = new FrmOrdemProducaoNecessidadeMP(codOrdem, dtFichaEstoque, _Listacomponentes);
                    frm.ShowDialog();
                }
                else
                {
                    throw new Exception("Parâmetro Inválido: Local Estoque Matéria Prima");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            try
            {
                switch (e.Column.FieldName.ToString())
                {
                    case "PORDEM.STATUSOP":
                        switch (e.Value.ToString())
                        {
                            case "1":
                                e.DisplayText = "1 - Aguardando Liberação";
                                break;

                            case "2":
                                e.DisplayText = "2 - Planejada";
                                break;

                            case "3":
                                e.DisplayText = "3 - Em Produção";
                                break;
                            case "4":
                                e.DisplayText = "4 - Paralisada";
                                break;

                            case "5":
                                e.DisplayText = "5 - Cancelada";
                                break;

                            case "6":
                                e.DisplayText = "6 - Concluída";
                                break;
                        }
                        break;
                }
            }
            catch (Exception)
            {

            }
        }

        #region Relatório Ordem de Produção 

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                getIdReport();
            }
            else
            {
                MessageBox.Show("Nenhum registro selecionado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void getIdReport()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT *
FROM 
ZREPORT 
WHERE 
CODREPORTTIPO = 'ORDEM'", new Object[] { });

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Não existe relatório parametrizado para este tipo de operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dt.Rows.Count == 1)
            {
                int IDREPORT = Convert.ToInt32(dt.Rows[0]["IDREPORT"]);
                this.ImprimirOrdem(IDREPORT);
            }

            if (dt.Rows.Count > 1)
            {
                AppLib.Windows.FormListaPrompt fLista = new AppLib.Windows.FormListaPrompt();
                fLista.PrimeiroItemNulo = false;
                Object result = fLista.Mostrar("Selecione o relatório:", dt);

                if (fLista.confirmacao == AppLib.Global.Types.Confirmacao.OK)
                {
                    int IDREPORT = Convert.ToInt32(result);
                    this.ImprimirOrdem(IDREPORT);
                }
            }
        }

        public void ImprimirOrdem(int IDREPORT)
        {
            String ListCODIGOOP = "";
            string SeqOP = string.Empty;

            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                ListCODIGOOP += row1["PORDEM.CODIGOOP"].ToString();
                ListCODIGOOP += ", ";
                SeqOP = row1["PORDEM.SEQOP"].ToString();
            }

            ListCODIGOOP = ListCODIGOOP.Substring(0, ListCODIGOOP.Length - 2);

            AppLib.Padrao.FormReportVisao f = new AppLib.Padrao.FormReportVisao();
            f.grid1.Conexao = "Start";

            f.Visualizar(IDREPORT, AppLib.Context.Empresa, AppLib.Context.Filial, ListCODIGOOP, SeqOP);
        }

#endregion

    }
}
