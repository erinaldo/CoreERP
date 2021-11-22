using ITGProducao.Controles;
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
    public partial class frmVisaoExtrato : Form
    {
        public Form pai = null;
        public bool consulta = false;
        string tabela = "FEXTRATO";
        string query = string.Empty;
        public string codMenu = string.Empty;
        private bool permiteEditar = true;

        List<string> tabelasFilhas = new List<string>();

        //Variaveis para usar quando a tela abre para consulta.
        public string IdExtrato;
        public string Historico;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmVisaoExtrato(string _query, Form frmprin, string _CodMenu)
        {
            InitializeComponent();
            codMenu = _CodMenu;
            this.MdiParent = frmprin;
            tabelasFilhas.Clear();
            query = _query;
            CarregaGrid(query);
            getAcesso(codMenu);
        }

        public frmVisaoExtrato(ref NewLookup lookup)
        {
            InitializeComponent();
            query = lookup.whereVisao;
            CarregaGrid(query);
            this.lookup = lookup;
        }

        private void getAcesso(string CodMenu)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO, EXCLUSAO, CONSULTA, INCLUSAO FROM GPERMISSAOMENU WHERE CODMENU = ? AND CODPERFIL = ?", new object[] { "btnMovimentacoesBancarias_Bancos", AppLib.Context.Perfil });
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

                carregaStatusExtrato();

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

        private void carregaStatusExtrato()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSITUACAO FROM GSITUACAO WHERE TABELA = 'FEXTRATO'");
            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
            }
            imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[0]["DESCRICAO"].ToString(), 0, 0));
            imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[1]["DESCRICAO"].ToString(), 1, 1));
            imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[2]["DESCRICAO"].ToString(), 2, 2));
            imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[3]["DESCRICAO"].ToString(), 4, 3));
            imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[4]["DESCRICAO"].ToString(), 5, 4));
            imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[5]["DESCRICAO"].ToString(), 6, 5));

            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns["FEXTRATO.TIPO"].ColumnEdit = imageCombo;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregaGrid(query);
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            CarregaGrid(query);
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
                CarregaGrid(query);
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

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            Filtro.frmFiltroExtrato Extrato = new Filtro.frmFiltroExtrato();
            Extrato.aberto = true;
            Extrato.ShowDialog();
            if (!string.IsNullOrEmpty(Extrato.condicao))
            {
                query = Extrato.condicao;
                CarregaGrid(query);
            }
        }

        private void atualizaColuna(DataRow dr)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FEXTRATO WHERE IDEXTRATO = ? AND CODEMPRESA = ?", new object[] { dr["FEXTRATO.IDEXTRATO"], AppLib.Context.Empresa });

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
                    PS.Glb.New.Cadastros.frmCadastroExtrato Extrato = new Cadastros.frmCadastroExtrato();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    Extrato.IdExtrato = row1["FEXTRATO.IDEXTRATO"].ToString();
                    Extrato.edita = true;
                    Extrato.codMenu = codMenu;
                    Extrato.ShowDialog();
                    atualizaColuna(row1);
                }
            }
            else
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                lookup.txtconteudo.Text = row1[tabela + ".HISTORICO"].ToString().ToUpper();
                lookup.txtcodigo.Text = row1[tabela + ".IDEXTRATO"].ToString();
                lookup.ValorCodigoInterno = row1[tabela + ".IDEXTRATO"].ToString();
                this.Dispose();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                PS.Glb.New.Cadastros.frmCadastroExtrato Extrato = new Cadastros.frmCadastroExtrato();
                Extrato.edita = false;
                Extrato.ShowDialog();
                CarregaGrid(query);
            }
            else
            {
                PS.Glb.New.Cadastros.frmCadastroExtrato Extrato = new Cadastros.frmCadastroExtrato(ref this.lookup);
                Extrato.edita = false;
                Extrato.ShowDialog();
                this.Dispose();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    PS.Glb.New.Cadastros.frmCadastroExtrato Extrato = new Cadastros.frmCadastroExtrato();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    Extrato.IdExtrato = row1["FEXTRATO.IDEXTRATO"].ToString();
                    Extrato.edita = true;
                    Extrato.ShowDialog();
                    atualizaColuna(row1);
                }
                else
                {
                    PS.Glb.New.Cadastros.frmCadastroExtrato Extrato = new Cadastros.frmCadastroExtrato();
                    Extrato.edita = false;
                }
            }
            else
            {
                gridControl1_DoubleClick(sender, e);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir o(s) registro(s) selecionados(?)", "Confirmação.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    List<int> ListCheque = new List<int>();
                    List<int> ListIdExtrato = new List<int>();

                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                        ListIdExtrato.Add(Convert.ToInt32(row["FEXTRATO.IDEXTRATO"]));

                        int codCheque = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT CODCHEQUE FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?", new object[] { AppLib.Context.Empresa, row["FEXTRATO.IDEXTRATO"] }));

                        if (codCheque > 0)
                        {
                            ListCheque.Add(codCheque);
                        }
                    }

                    if (ListCheque.Count > 0)
                    {
                        if (MessageBox.Show("Existem cheques relacionados a este(s) extrato(s), deseja excluí-lo(s)?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                            {
                                if (Excluir(ListCheque, ListIdExtrato) == true)
                                {
                                    MessageBox.Show("Registro(s) excluído(s) com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CarregaGrid(query);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ListIdExtrato.Count; i++)
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?", new object[] { AppLib.Context.Empresa, ListIdExtrato[i] });
                        }
                    }

                    #region Rotina antiga

                    //DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                    //if (new Class.Utilidades().Excluir("IDEXTRATO", "FEXTRATO", row["FEXTRATO.IDEXTRATO"].ToString()) == true)
                    //{
                    //    if (gridView1.SelectedRowsCount > 0)
                    //    {
                    //        continue;
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Não foi possível excluir pois existe(m) lançamento(s) vinculado(s).", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}

                    #endregion

                    MessageBox.Show("Registro(s) excluído(s) com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregaGrid(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool Excluir(List<int> _listCheque, List<int> _listExtrato)
        {
            try
            {
                for (int i = 0; i < _listCheque.Count; i++)
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM FCHEQUE WHERE CODEMPRESA = ? AND CODCHEQUE = ?", new object[] { AppLib.Context.Empresa, _listCheque[i] });
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?", new object[] { AppLib.Context.Empresa, _listExtrato[i] });
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void CarregaGridLancamentos()
        {
            TabLancamentos.PageVisible = true;
            splitContainer1.Panel2Collapsed = false;
            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
                                                                                  FEXTRATOLANCA.CODEMPRESA AS 'Código da Empresa',
                                                                                  FEXTRATOLANCA.CODFILIAL AS 'Código da Filial', 
                                                                                  FEXTRATOLANCA.CODLANCA AS 'Código do Lançamento', 
                                                                                  FEXTRATO.CODCONTA AS 'Código da Conta', 
                                                                                  FLANCA.CODCLIFOR AS 'Cliente/Fornecedor',
                                                                                  FLANCA.NOMECLIENTE AS 'Nome do Cliente',
                                                                                  FLANCA.CODTIPDOC AS 'Tipo de Documento',
                                                                                  FLANCA.NUMERO AS 'Numero Documento',
                                                                                  FLANCA.NSEQLANCA AS 'Sequencial Lançamento',
                                                                                  FLANCA.DATAEMISSAO AS 'Data de Emissão Lançamento',
                                                                                  FLANCA.DATAVENCIMENTO AS 'Data de Vencimento',
                                                                                  FLANCA.DATABAIXA AS 'Data de Baixa',
                                                                                  FLANCA.CODMOEDA AS 'Moeda',
                                                                                  FLANCA.VLORIGINAL AS 'Valor Original',
                                                                                  FLANCA.VLBAIXADO AS 'Valor Baixado',
                                                                                  FLANCA.CODOPER AS 'Operação',
                                                                                  FLANCA.OBSERVACAO AS 'Observação'
                                                                                  FROM FEXTRATOLANCA
                                                                                  INNER JOIN FLANCA ON FEXTRATOLANCA.CODLANCA = FLANCA.CODLANCA AND FEXTRATOLANCA.CODEMPRESA = FLANCA.CODEMPRESA AND FEXTRATOLANCA.CODFILIAL = FLANCA.CODFILIAL
                                                                                  LEFT JOIN FEXTRATO ON FEXTRATO.IDEXTRATO = FEXTRATOLANCA.IDEXTRATO AND FEXTRATO.CODEMPRESA = FEXTRATOLANCA.CODEMPRESA AND FEXTRATO.CODFILIAL = FEXTRATOLANCA.CODFILIAL
                                                                                  WHERE FEXTRATOLANCA.CODEMPRESA = ? AND FEXTRATOLANCA.CODFILIAL = ? AND FEXTRATOLANCA.IDEXTRATO = ?", new object[] { AppLib.Context.Empresa, row["FEXTRATO.CODFILIAL"], row["FEXTRATO.IDEXTRATO"] });

            gridControl2.DataSource = dt;
            gridView2.BestFitColumns();
        }

        private void lançamentosFinanceirosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                CarregaGridLancamentos();
            }
        }

        private void fecharAnexosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabLancamentos.PageVisible = false;
            splitContainer1.Panel2Collapsed = true;
        }

        private void compensarExtratpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                PS.Glb.New.Processos.Financeiro.frmCompensarExtrato frm = new Processos.Financeiro.frmCompensarExtrato();

                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                    frm.ListExtrato.Add(Convert.ToInt32(row["FEXTRATO.IDEXTRATO"]));
                }

                frm.ShowDialog();
                CarregaGrid(query);
            }
        }

        private void cancelarCompensaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja cancelar a compensação do(s) registro(s) selecionado(s)?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                        if (AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FEXTRATO SET COMPENSADO = 0, DATACOMPENSACAO = NULL WHERE CODEMPRESA = ? AND IDEXTRATO = ?", new object[] { AppLib.Context.Empresa, row["FEXTRATO.IDEXTRATO"].ToString() }) > 0)
                        {
                            int Codcheque = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT CODCHEQUE FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?", new object[] { AppLib.Context.Empresa, row["FEXTRATO.IDEXTRATO"].ToString() }));

                            if (Codcheque > 0)
                            {
                                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FCHEQUE SET COMPENSADO = 0, DATACOMPENSACAO = NULL WHERE CODEMPRESA = ? AND CODCHEQUE = ?", new object[] { AppLib.Context.Empresa, Codcheque });
                            }
                        }
                    }
                    MessageBox.Show("Cancelamento realizado com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível concluir o cancelamento de extrato.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CarregaGrid(query);
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                return;
            }

            if (splitContainer1.Panel2Collapsed == false)
            {
                if (TabLancamentos.PageVisible == true)
                {
                    CarregaGridLancamentos();
                }
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (consulta == true)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    IdExtrato = row1["FEXTRATO.IDEXTRATO"].ToString();
                    this.Dispose();
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
    }
}
