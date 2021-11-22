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
    public partial class frmVisaoProduto : Form
    {
        public bool consulta = false;
        string tabela = "VPRODUTO";
        //string relacionamento = " LEFT OUTER JOIN VSALDOESTOQUE ON VPRODUTO.CODPRODUTO = VSALDOESTOQUE.CODPRODUTO AND VPRODUTO.CODEMPRESA = VSALDOESTOQUE.CODEMPRESA";
        string relacionamento = string.Empty;
        string query = string.Empty;
        public string codMenu = string.Empty;
        private bool permiteEditar = true;

        List<string> tabelasFilhas = new List<string>();


        //Variaveis para usar quando a tela abre para consulta.
        public string codProduto;
        public string nome;
        public string codigoAuxiliar;
        public string codUnidVenda;
        //////

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmVisaoProduto(string _query, Form frmprin, string _Codmenu)
        {
            InitializeComponent();
            this.MdiParent = frmprin;
            codMenu = _Codmenu;
            query = _query;
            getAcesso(codMenu);
            carregaGrid(query, false);
            splitContainer1.SplitterDistance = 30;
        }


        public frmVisaoProduto(string _where, bool _consulta)
        {
            InitializeComponent();
            query = _where;
            consulta = _consulta;
            carregaGrid(query, _consulta);

        }

        public frmVisaoProduto(ref NewLookup lookup)
        {
            InitializeComponent();

            query = lookup.whereVisao;
            carregaGrid(query, false);

            this.lookup = lookup;
        }

        private void getAcesso(string CodMenu)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO, EXCLUSAO, CONSULTA, INCLUSAO FROM GPERMISSAOMENU WHERE CODMENU = ? AND CODPERFIL = ?", new object[] { "btnCadastrosGlobais_Produtos", AppLib.Context.Perfil });
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

        public void carregaGrid(string where, bool consulta)
        {
            try
            {
                string sql = string.Empty;


                sql = new Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);


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

                for (int i = 0; i < tabelasFilhas.Count; i++)
                {
                    dic = new DataTable();
                    dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabelasFilhas[i].ToString() });
                    dt = new DataTable();
                    dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabelasFilhas[i].ToString(), AppLib.Context.Usuario });
                    if (dt.Rows.Count > 0)
                    {
                        for (int ii = 0; ii < gridView1.Columns.Count; ii++)
                        {
                            gridView1.Columns[ii].Width = Convert.ToInt32(dt.Rows[ii]["LARGURA"]);
                            dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                            DataRow result = dic.Rows.Find(new object[] { gridView1.Columns[ii].FieldName.ToString() });
                            if (result != null)
                            {
                                gridView1.Columns[ii].Caption = result["DESCRICAO"].ToString();
                            }
                        }
                    }
                }
                if (gridView1.Columns["VPRODUTO.PRODSERV"] != null)
                {
                    carregaImagem();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregaImagem()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();

            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSTATUS FROM GSTATUS WHERE TABELA = 'VPRODUTO'");

            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
                imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[i]["DESCRICAO"].ToString(), Convert.ToInt32(dtImagem.Rows[i]["CODSTATUS"]), i));
            }
            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns["VPRODUTO.PRODSERV"].ColumnEdit = imageCombo;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            carregaGrid(query, false);
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            carregaGrid(query, false);
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
                carregaGrid(query, false);
            }

        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            Filtro.frmFiltroProduto frm = new Filtro.frmFiltroProduto();
            frm.aberto = true;
            frm.ShowDialog();
            if (this.lookup == null)
            {
                if (!string.IsNullOrEmpty(frm.condicao))
                {
                    query = frm.condicao;
                    carregaGrid(query, false);
                }
            }
            else
            {
                query = frm.condicao;
                carregaGrid(query, false);
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
            if (MessageBox.Show("Deseja excluir os registros selecionados?", "Confirmação.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                        int VerificaGoperItem = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODPRODUTO) FROM GOPERITEM WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, row["VPRODUTO.CODPRODUTO"] }).ToString());

                        if (VerificaGoperItem > 0)
                        {
                            MessageBox.Show("Produto em uso nos itens da operação.", "Aviso:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        else
                        {
                            int VerificaTabPreco = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODPRODUTO) FROM VCLIFORTABPRECOITEM WHERE VCLIFORTABPRECOITEM.CODEMPRESA = ? AND VCLIFORTABPRECOITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, row["VPRODUTO.CODPRODUTO"] }).ToString());

                            if (VerificaTabPreco > 0)
                            {
                                MessageBox.Show("Produto em uso nos itens da Tabela de Preço do Cliente.", "Aviso:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else
                            {
                                AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, row["VPRODUTO.CODPRODUTO"] });
                            }
                        }
                    }
                    MessageBox.Show("Registro excluído com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    carregaGrid(query, consulta);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Atualizar()
        {
            if (this.lookup == null)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    New.Cadastros.frmCadastroProduto frm = new New.Cadastros.frmCadastroProduto();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    frm.codProduto = row1["VPRODUTO.CODPRODUTO"].ToString();
                    frm.edita = true;
                    frm.ShowDialog();
                    atualizaColuna(row1);
                }
            }
            else
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                lookup.txtconteudo.Text = row1[tabela + ".NOME"].ToString().ToUpper();

                switch (lookup.CampoCodigo_Igual_a)
                {

                    //VERIFICAR PARAMETRO DE CODIGO INTERNO OU NAO (CRIAR UM GERAL, POIS O ATUAL É POR TIPO OPERACAO)
                    case NewLookup.CampoCodigoIguala.CampoCodigoBD:
                        lookup.txtcodigo.Text = row1[tabela + ".CODPRODUTO"].ToString();
                        lookup.ValorCodigoInterno = row1[tabela + ".CODPRODUTO"].ToString();
                        break;

                    case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                        if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                        {
                            lookup.txtcodigo.Text = row1[tabela + "." + lookup.CampoCodigoInterno].ToString();
                            lookup.ValorCodigoInterno = row1[tabela + ".CODPRODUTO"].ToString();
                        }
                        else
                        {
                            lookup.txtcodigo.Text = row1[tabela + ".CODPRODUTO"].ToString();
                            lookup.ValorCodigoInterno = row1[tabela + ".CODPRODUTO"].ToString();
                        }
                        break;
                }

                this.Dispose();
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            Atualizar();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {

            if (this.lookup == null)
            {
                if (consulta == true)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    codProduto = row1["VPRODUTO.CODPRODUTO"].ToString();
                    nome = row1["VPRODUTO.NOME"].ToString();
                    codigoAuxiliar = row1["VPRODUTO.CODIGOAUXILIAR"].ToString();
                    codUnidVenda = row1["VPRODUTO.CODUNIDVENDA"].ToString();
                    this.Dispose();
                    GC.Collect();
                }
                else
                {
                    //btnEditar_Click(sender, e);
                    Atualizar();
                }
            }
            else
            {
                Atualizar();
            }
        }

        private void atualizaColuna(DataRow dr)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPRODUTO WHERE CODPRODUTO = ? AND CODEMPRESA = ?", new object[] { dr["VPRODUTO.CODPRODUTO"], AppLib.Context.Empresa });

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

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                New.Cadastros.frmCadastroProduto frm = new New.Cadastros.frmCadastroProduto();
                frm.edita = false;
                frm.ShowDialog();
                carregaGrid(query, consulta);
            }
            else
            {
                New.Cadastros.frmCadastroProduto frm = new New.Cadastros.frmCadastroProduto(ref this.lookup);
                frm.edita = false;
                frm.ShowDialog();
                this.Dispose();
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string reportPath = string.Empty;
            FolderBrowserDialog open = new FolderBrowserDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                reportPath = open.SelectedPath + "Produtos.xlsx";
                DevExpress.XtraPrinting.XlsExportOptions xlsxOptions = new DevExpress.XtraPrinting.XlsExportOptions();
                xlsxOptions.ShowGridLines = true;
                xlsxOptions.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;
                xlsxOptions.ExportHyperlinks = true;
                xlsxOptions.SheetName = "Produtos";
                gridView1.ExportToXlsx(reportPath);
                StartProcess(reportPath);
            }
        }

        private void StartProcess(string path)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            try
            {
                process.StartInfo.FileName = path;
                process.Start();
                process.WaitForInputIdle();
            }
            catch { }
        }

        private void frmVisaoProduto_Load(object sender, EventArgs e)
        {

        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            switch (e.KeyCode)
            {
                case Keys.F1:
                    break;
                case Keys.F10:
                    break;
                case Keys.F11:
                    break;
                case Keys.F12:
                    break;
                case Keys.F13:
                    break;
                case Keys.F14:
                    break;
                case Keys.F15:
                    break;
                case Keys.F16:
                    break;
                case Keys.F17:
                    break;
                case Keys.F18:
                    break;
                case Keys.F19:
                    break;
                case Keys.F2:
                    break;
                case Keys.F20:
                    break;
                case Keys.F21:
                    break;
                case Keys.F22:
                    break;
                case Keys.F23:
                    break;
                case Keys.F24:
                    break;
                case Keys.F3:
                    break;
                case Keys.F4:
                    break;
                case Keys.F5:
                    break;
                case Keys.F6:
                    if (string.IsNullOrEmpty(row1["VPRODUTO.CODPRODUTO"].ToString()))
                    {
                        PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque frm = new PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque();
                        frm.ShowDialog();
                    }
                    else
                    {
                        PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque frm = new PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque(row1["VPRODUTO.CODPRODUTO"].ToString());
                        frm.getProduto();
                        frm.ShowDialog();

                    }
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    break;
                case Keys.F9:
                    break;
                case Keys.Insert:
                    //btnNovo_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        public void carregaGrid(string _tabela, DevExpress.XtraGrid.GridControl grid, DevExpress.XtraGrid.Views.Grid.GridView view, string where, string relacionamento, List<string> tabelasFilhas)
        {
            try
            {
                string sql = new Class.Utilidades().getVisao(_tabela, relacionamento, tabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                grid.DataSource = null;
                view.Columns.Clear();
                grid.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { _tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { _tabela, AppLib.Context.Usuario });
                for (int i = 0; i < view.Columns.Count; i++)
                {
                    view.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { view.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        view.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Anexo Saldo de Estoque

        private void saldoDeEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CarregaSaldoEstoque();
        }

        private void CarregaSaldoEstoque()
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                TabSaldoEstoque.PageVisible = true;
                splitContainer2.Panel2Collapsed = false;
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                //CarregaGridSaldo("WHERE VPRODUTO.CODEMPRESA =" + row1["VPRODUTO.CODEMPRESA"] + "AND VPRODUTO.CODPRODUTO ='" + row1["VPRODUTO.CODPRODUTO"] +"'");
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
SELECT
VSALDOESTOQUE.CODPRODUTO AS	'Cód. Produto',
VPRODUTO.CODIGOAUXILIAR AS 'Código',
VPRODUTO.NOME AS 'Descrição',
VPRODUTO.LOCALESTOCAGEM AS 'Local Estocagem',
VSALDOESTOQUE.SALDOFINAL AS 'Saldo',
VSALDOESTOQUE.CODUNIDCONTROLE AS 'Unidade',
VPRODUTO.ESTOQUEMINIMO AS 'Estoque Mínimo',
VPRODUTO.ESTOQUEMAXIMO AS 'Estoque Máximo',
VSALDOESTOQUE.CODLOCAL AS 'Cód. Local',
VLOCALESTOQUE.DESCRIÇÃO AS 'Local',
VSALDOESTOQUE.TOTALFINAL AS 'Valor Total',
VSALDOESTOQUE.CUSTOMEDIO AS 'Custo Médio',
VSALDOESTOQUE.CODEMPRESA AS 'Cód. Empresa',
VSALDOESTOQUE.CODFILIAL AS 'Cód. Filial'

FROM 
VSALDOESTOQUE
INNER JOIN VLOCALESTOQUE ON VLOCALESTOQUE.CODEMPRESA = VSALDOESTOQUE.CODEMPRESA AND VLOCALESTOQUE.CODFILIAL = VSALDOESTOQUE.CODFILIAL AND VLOCALESTOQUE.CODLOCAL = VSALDOESTOQUE.CODLOCAL
INNER JOIN VPRODUTO ON VPRODUTO.CODEMPRESA = VSALDOESTOQUE.CODEMPRESA AND VPRODUTO.CODPRODUTO = VSALDOESTOQUE.CODPRODUTO

WHERE 
VPRODUTO.CODEMPRESA = ?
AND VPRODUTO.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, row1["VPRODUTO.CODPRODUTO"] });
                gridControl2.DataSource = dt;
                gridView2.BestFitColumns();
            }
        }

        private void CarregaGridSaldo(string where)
        {
            string relacao = @"INNER JOIN VLOCALESTOQUE ON VLOCALESTOQUE.CODEMPRESA = VSALDOESTOQUE.CODEMPRESA AND VLOCALESTOQUE.CODFILIAL = VSALDOESTOQUE.CODFILIAL AND VLOCALESTOQUE.CODLOCAL = VSALDOESTOQUE.CODLOCAL
                               INNER JOIN VPRODUTO ON VPRODUTO.CODEMPRESA = VSALDOESTOQUE.CODEMPRESA AND VPRODUTO.CODPRODUTO = VSALDOESTOQUE.CODPRODUTO";
            List<string> tabelasFilhas = new List<string>();
            tabelasFilhas.Add("VPRODUTO");

            try
            {
                string sql = new Class.Utilidades().getVisao("VSALDOESTOQUE", relacao, tabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                gridControl2.DataSource = null;
                gridView2.Columns.Clear();
                gridControl2.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (splitContainer2.Panel2Collapsed == false)
            {
                if (TabSaldoEstoque.PageVisible == true)
                {
                    CarregaSaldoEstoque();
                }
            }
        }

        private void fecharAnexosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = true;
        }

#endregion

    }
}
