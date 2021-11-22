using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITGProducao.Controles;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroInventario : Form
    {
        public bool edita = false;
        public string CodInventario = string.Empty;
        public string Codfilial = string.Empty;
        public string Codlocal = string.Empty;
        private DataTable dtValidaInventario;

        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;
        private bool permiteEditar = true;

        string query = string.Empty;
        string tabela = "GITEMINVENTARIO";

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroInventario()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GINVENTARIO");

            #region Combobox

            List<PS.Lib.ComboBoxItem> TIPOINVENTARIO = new List<PS.Lib.ComboBoxItem>();

            TIPOINVENTARIO.Add(new PS.Lib.ComboBoxItem());
            TIPOINVENTARIO[0].ValueMember = "Produto";
            TIPOINVENTARIO[0].DisplayMember = "Produto";

            cmbTipoInventario.DataSource = TIPOINVENTARIO;
            cmbTipoInventario.DisplayMember = "DisplayMember";
            cmbTipoInventario.ValueMember = "ValueMember";

            #endregion

            this.codMenu = "btnUtilitarios_InventarioItem";
            getAcesso(codMenu);
        }

        public frmCadastroInventario(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GINVENTARIO");

            #region Combobox

            List<PS.Lib.ComboBoxItem> TIPOINVENTARIO = new List<PS.Lib.ComboBoxItem>();

            TIPOINVENTARIO.Add(new PS.Lib.ComboBoxItem());
            TIPOINVENTARIO[0].ValueMember = "Produto";
            TIPOINVENTARIO[0].DisplayMember = "Produto";

            cmbTipoInventario.DataSource = TIPOINVENTARIO;
            cmbTipoInventario.DisplayMember = "DisplayMember";
            cmbTipoInventario.ValueMember = "ValueMember";

            #endregion

            this.edita = true;
            this.lookup = lookup;
            CodInventario = lookup.ValorCodigoInterno;
            carregaCampos();
        }

        private void frmCadastroInventario_Load(object sender, EventArgs e)
        {
            // Ja começa com o tipo como produto.
            cmbTipoInventario.SelectedIndex = 0;

            if (edita == true)
            {
                carregaCampos();
                tbCodInventario.Enabled = false;
                cmbTipoInventario.Enabled = false;
                carregaGrid("WHERE CODINVENTARIO ='" + CodInventario + "'");
                ValidaItens();
                ValidaStatus();
            }
            else
            {
                LimpaGrid_Itens();
            }
        }

        private void getAcesso(string Codmenu)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO, EXCLUSAO, CONSULTA, INCLUSAO FROM GPERMISSAOMENU WHERE CODMENU = ? AND CODUSUARIO = ?", new object[] { "btnUtilitarios_InventarioItem", AppLib.Context.Usuario });
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dt.Rows[i]["EDICAO"]) == false)
                    {
                        btnEditarItem.Enabled = false;

                        btnCancelarAtual.Text = "Fechar";
                        btnSalvarAtual.Enabled = false;
                        btnOKAtual.Enabled = false;

                        lpFilial.Enabled = false;
                        lpLocalEstoque.Enabled = false;
                        lpCentroCusto.Enabled = false;
                        tbObservacao.Enabled = false;

                        permiteEditar = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["CONSULTA"]) == false)
                    {
                        btnEditarItem.Enabled = true;

                        btnCancelarAtual.Text = "Fechar";
                        btnSalvarAtual.Enabled = false;
                        btnOKAtual.Enabled = false;

                        lpFilial.Enabled = false;
                        lpLocalEstoque.Enabled = false;
                        lpCentroCusto.Enabled = false;
                        tbObservacao.Enabled = false;

                        permiteEditar = true;
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["EXCLUSAO"]) == false)
                    {
                        btnExcluirItem.Enabled = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["INCLUSAO"]) == false)
                    {
                        btnNovoItem.Enabled = false;

                        btnCancelarAtual.Text = "Fechar";
                        btnSalvarAtual.Enabled = false;
                        btnOKAtual.Enabled = false;

                        lpFilial.Enabled = false;
                        lpLocalEstoque.Enabled = false;
                        lpCentroCusto.Enabled = false;
                        tbObservacao.Enabled = false;
                    }
                }
            }
            else
            {
                btnEditarItem.Enabled = false;
                btnExcluirItem.Enabled = false;
                btnNovoItem.Enabled = false;
            }
        }

        #region Grid

        private void LimpaGrid_Itens()
        {
            try
            {
                string Relacionamento = "INNER JOIN VPRODUTO ON GITEMINVENTARIO.CODEMPRESA = VPRODUTO.CODEMPRESA AND GITEMINVENTARIO.CODPRODUTO = VPRODUTO.CODPRODUTO";
                List<string> Tabelasfilhas = new List<string>();
                Tabelasfilhas.Add("VPRODUTO");

                string sql = string.Empty;

                sql = new Class.Utilidades().getVisao(tabela, Relacionamento, Tabelasfilhas, "").Replace("SELECT", "SELECT TOP 0 ");

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

        public void carregaGrid(string where)
        {
            try
            {
                string Relacionamento = "INNER JOIN VPRODUTO ON GITEMINVENTARIO.CODEMPRESA = VPRODUTO.CODEMPRESA AND GITEMINVENTARIO.CODPRODUTO = VPRODUTO.CODPRODUTO";
                List<string> Tabelasfilhas = new List<string>();
                Tabelasfilhas.Add("VPRODUTO");

                string sql = string.Empty;

                sql = new Class.Utilidades().getVisao(tabela, Relacionamento, Tabelasfilhas, where);

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

        #endregion

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GINVENTARIO WHERE CODINVENTARIO = ?", new object[] { CodInventario });
                if (dt.Rows.Count > 0)
                {
                    dtValidaInventario = dt;
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GINVENTARIO WHERE CODINVENTARIO = ?", new object[] { CodInventario });
                if (dt.Rows.Count > 0)
                {
                    dtValidaInventario = dt;
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodInventario.Text = dt.Rows[0]["CODINVENTARIO"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATAINVENTARIO"].ToString()))
            {
                dtInventario.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAINVENTARIO"]);
            }

            tbStatus.Text = dt.Rows[0]["STATUS"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATACONCLUSAO"].ToString()))
            {
                dtConclusao.DateTime = Convert.ToDateTime(dt.Rows[0]["DATACONCLUSAO"]);
            }

            lpFilial.txtcodigo.Text = dt.Rows[0]["CODFILIAL"].ToString();
            lpFilial.CarregaDescricao();
            // Carrega a variável no edita
            Codfilial = lpFilial.txtcodigo.Text;
            //
            lpLocalEstoque.txtcodigo.Text = dt.Rows[0]["CODLOCAL"].ToString();
            lpLocalEstoque.CarregaDescricao();
            // Carrega a variável no edita
            Codlocal = lpLocalEstoque.txtcodigo.Text;
            //
            lpCentroCusto.txtcodigo.Text = dt.Rows[0]["CODCCUSTO"].ToString();
            lpCentroCusto.CarregaDescricao();
            chkBloqueiaMov.Checked = Convert.ToBoolean(dt.Rows[0]["BLOQUEIAMOV"]);
            cmbTipoInventario.SelectedValue = dt.Rows[0]["TIPOINVENTARIO"];
            chkGeraAcerto.Checked = Convert.ToBoolean(dt.Rows[0]["GERARACERTO"]);
            tbObservacao.Text = dt.Rows[0]["OBSERVACAO"].ToString();
            tbUsuarioCriacao.Text = dt.Rows[0]["USUARIOCRIACAO"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATACRIACAO"].ToString()))
            {
                dtCriacao.DateTime = Convert.ToDateTime(dt.Rows[0]["DATACRIACAO"]);
            }

            tbUsuarioAlteracao.Text = dt.Rows[0]["USUARIOALTERACAO"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATAALTERACAO"].ToString()))
            {
                dtAlteracao.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAALTERACAO"]);
            }
        }

        private string getCodigoInventario()
        {
            string Codigo = string.Empty;

            Codigo = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT ((REPLICATE(0, (6 - LEN(MAX(CONVERT(INT,ISNULL(GINVENTARIO.CODINVENTARIO,0))))))) +  CONVERT(VARCHAR(9),(MAX(CONVERT(INT,ISNULL(GINVENTARIO.CODINVENTARIO,0)))+1))) FROM GINVENTARIO WHERE GINVENTARIO.CODEMPRESA = ? AND GINVENTARIO.CODFILIAL = ?", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text }).ToString();

            if (string.IsNullOrEmpty(Codigo))
            {
                Codigo = "000001";
            }
            return Codigo;
        }

        #region Validações

        private void ValidaItens()
        {
            int Itens = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODPRODUTO) FROM GITEMINVENTARIO WHERE CODEMPRESA = ? AND CODLOCAL = ? AND CODFILIAL = ? AND CODINVENTARIO = ?", new object[] { AppLib.Context.Empresa, Codlocal, Codfilial, CodInventario }));

            if (Itens > 0)
            {
                lpFilial.Enabled = false;
                lpLocalEstoque.Enabled = false;
            }
        }

        private void ValidaStatus()
        {
            string Status = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT STATUS FROM GINVENTARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODINVENTARIO = ? AND CODLOCAL = ?", new object[] { AppLib.Context.Empresa, Codfilial, CodInventario, Codlocal }).ToString();

            if (Status == "Encerrado")
            {
                chkGeraAcerto.Enabled = false;
                chkBloqueiaMov.Enabled = false;
                cmbTipoInventario.Enabled = false;
                lpFilial.Enabled = false;
                lpLocalEstoque.Enabled = false;
                lpCentroCusto.Enabled = false;
                tbObservacao.Enabled = false;

                btnNovoItem.Enabled = false;
                btnExcluirItem.Enabled = false;
            }
            if (Status == "Iniciado")
            {
                chkGeraAcerto.Enabled = false;
                chkBloqueiaMov.Enabled = false;
                cmbTipoInventario.Enabled = false;
                lpFilial.Enabled = false;
                lpLocalEstoque.Enabled = false;
                lpCentroCusto.Enabled = false;
            }
            if (Status == "Cancelado")
            {
                chkGeraAcerto.Enabled = false;
                chkBloqueiaMov.Enabled = false;
                cmbTipoInventario.Enabled = false;
                lpFilial.Enabled = false;
                lpLocalEstoque.Enabled = false;
                lpCentroCusto.Enabled = false;
                tbObservacao.Enabled = false;

                btnNovoItem.Enabled = false;
                btnEditarItem.Enabled = false;
                btnExcluirItem.Enabled = false;
            }
        }

        #endregion

        private bool Salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit GINVENTARIO = new AppLib.ORM.Jit(conn, "GINVENTARIO");
            conn.BeginTransaction();

            try
            {
                GINVENTARIO.Set("CODINVENTARIO", tbCodInventario.Text);
                GINVENTARIO.Set("CODEMPRESA", AppLib.Context.Empresa);

                #region Validação da Data de Inventário

                if (edita == false)
                {
                    GINVENTARIO.Set("DATAINVENTARIO", null);
                }

                if (tbStatus.Text == "Iniciado" || tbStatus.Text == "Encerrado" || tbStatus.Text == "Cancelado")
                {
                    GINVENTARIO.Set("DATAINVENTARIO", Convert.ToDateTime(dtValidaInventario.Rows[0]["DATAINVENTARIO"]));                                  
                }
                else
                {
                    GINVENTARIO.Set("DATAINVENTARIO", null);
                }

                #endregion

                if (edita == false)
                {
                    GINVENTARIO.Set("STATUS", "Aberto");
                }

                GINVENTARIO.Set("DATACONCLUSAO", null);

                if (!string.IsNullOrEmpty(lpFilial.ValorCodigoInterno))
                {
                    GINVENTARIO.Set("CODFILIAL", Convert.ToInt32(lpFilial.ValorCodigoInterno));
                }
                else
                {
                    GINVENTARIO.Set("CODFILIAL", null);
                }

                if (!string.IsNullOrEmpty(lpLocalEstoque.ValorCodigoInterno))
                {
                    GINVENTARIO.Set("CODLOCAL", lpLocalEstoque.ValorCodigoInterno);
                }
                else
                {
                    GINVENTARIO.Set("CODLOCAL", null);
                }

                if (!string.IsNullOrEmpty(lpCentroCusto.ValorCodigoInterno))
                {
                    GINVENTARIO.Set("CODCCUSTO", lpCentroCusto.ValorCodigoInterno);
                }
                else
                {
                    GINVENTARIO.Set("CODCCUSTO", null);
                }

                GINVENTARIO.Set("TIPOINVENTARIO", cmbTipoInventario.SelectedValue);
                GINVENTARIO.Set("GERARACERTO", chkGeraAcerto.Checked == true ? 1 : 0);
                GINVENTARIO.Set("BLOQUEIAMOV", chkBloqueiaMov.Checked == true ? 1 : 0);
                GINVENTARIO.Set("OBSERVACAO", tbObservacao.Text);
                GINVENTARIO.Set("USUARIOCRIACAO", AppLib.Context.Usuario);
                GINVENTARIO.Set("DATACRIACAO", Convert.ToDateTime(DateTime.Now));

                if (edita == true)
                {
                    GINVENTARIO.Set("USUARIOALTERACAO", AppLib.Context.Usuario);
                    GINVENTARIO.Set("DATAALTERACAO", Convert.ToDateTime(DateTime.Now));
                }
                else
                {
                    GINVENTARIO.Set("USUARIOALTERACAO", null);
                    GINVENTARIO.Set("DATAALTERACAO", null);
                }

                GINVENTARIO.Save();
                conn.Commit();
                edita = true;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            if (edita == false)
            {
                if (this.lookup == null)
                {
                    tbCodInventario.Text = getCodigoInventario();
                    Salvar();
                    carregaCampos();
                }
                else
                {
                    if (Salvar() == true)
                    {
                        carregaCampos();
                        this.Dispose();
                    }
                }
            }
            else
            {
                if (this.lookup == null)
                {
                    Salvar();
                }
                else
                {
                    if (Salvar() == true)
                    {
                        carregaCampos();
                        this.Dispose();
                    }
                }
            }
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (edita == false)
            {
                tbCodInventario.Text = getCodigoInventario();

                if (Salvar() == true)
                {
                    this.Dispose();
                }
            }
            else
            {
                if (Salvar() == true)
                {
                    this.Dispose();
                }
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Itens do Inventário

        private void btnNovoItem_Click(object sender, EventArgs e)
        {
            if (edita == false)
            {
                tbCodInventario.Text = getCodigoInventario();

                if (Salvar() == true)
                {
                    Codfilial = lpFilial.txtcodigo.Text;
                    Codlocal = lpLocalEstoque.txtcodigo.Text;
                    CodInventario = tbCodInventario.Text;
                    PS.Glb.New.Filtro.frmFiltroInventarioItens filtro = new Filtro.frmFiltroInventarioItens();
                    filtro.CODINVENTARIO = CodInventario;
                    filtro.CODFILIAL = Codfilial;
                    filtro.CODLOCAL = Codlocal;
                    filtro.ShowDialog();
                    carregaGrid("WHERE CODINVENTARIO ='" + CodInventario + "'");
                    ValidaItens();
                }
            }
            else
            {
                if (Salvar() == true)
                {
                    Codfilial = lpFilial.txtcodigo.Text;
                    Codlocal = lpLocalEstoque.txtcodigo.Text;
                    CodInventario = tbCodInventario.Text;
                    PS.Glb.New.Filtro.frmFiltroInventarioItens filtro = new Filtro.frmFiltroInventarioItens();
                    filtro.CODINVENTARIO = CodInventario;
                    filtro.CODFILIAL = Codfilial;
                    filtro.CODLOCAL = Codlocal;
                    filtro.ShowDialog();
                    carregaGrid("WHERE CODINVENTARIO ='" + CodInventario + "'");
                    ValidaItens();
                }
            }
        }

        private void btnEditarItem_Click(object sender, EventArgs e)
        {
            PS.Glb.New.Cadastros.frmCadastroItensInventario itens = new frmCadastroItensInventario();
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            itens.Codproduto = row1["GITEMINVENTARIO.CODPRODUTO"].ToString();
            itens.CodInventario = row1["GITEMINVENTARIO.CODINVENTARIO"].ToString();
            itens.CodFilial = row1["GITEMINVENTARIO.CODFILIAL"].ToString();
            itens.edita = true;
            itens.ShowDialog();
        }

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir os registros selecionados?", "Confirmação.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GITEMINVENTARIO WHERE CODINVENTARIO = ? AND CODPRODUTO = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODEMPRESA = ?", new object[] { row["GITEMINVENTARIO.CODINVENTARIO"], row["GITEMINVENTARIO.CODPRODUTO"], row["GITEMINVENTARIO.CODFILIAL"], row["GITEMINVENTARIO.CODLOCAL"], AppLib.Context.Empresa });
                    }
                    MessageBox.Show("Registro excluído com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    carregaGrid("WHERE CODINVENTARIO ='" + CodInventario + "'");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPesquisarItem_Click(object sender, EventArgs e)
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

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            if (btnFiltro.Text == "Itens com diferença")
            {
                carregaGrid("WHERE CODINVENTARIO ='" + CodInventario + "' AND DIFERENCAFINAL <> 0 ");
                btnFiltro.Text = "Remover filtro";
            }
            else
            {
                carregaGrid("WHERE CODINVENTARIO ='" + CodInventario + "'");
                btnFiltro.Text = "Itens com diferença";
            }
        }

        private void gridControl1_DoubleClick_1(object sender, EventArgs e)
        {
            PS.Glb.New.Cadastros.frmCadastroItensInventario itens = new frmCadastroItensInventario();
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            itens.Codproduto = row1["GITEMINVENTARIO.CODPRODUTO"].ToString();
            itens.CodInventario = row1["GITEMINVENTARIO.CODINVENTARIO"].ToString();
            itens.CodFilial = row1["GITEMINVENTARIO.CODFILIAL"].ToString();
            itens.edita = true;
            itens.ShowDialog();
        }

        private void btnAtualizarItens_Click(object sender, EventArgs e)
        {
            carregaGrid("WHERE CODINVENTARIO ='" + CodInventario + "'");
        }

        private void btnSelecionarColunasItens_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            carregaGrid("WHERE CODINVENTARIO ='" + CodInventario + "'");
        }

        private void btnSalvarLayoutItens_Click(object sender, EventArgs e)
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

        #endregion
    }
}
