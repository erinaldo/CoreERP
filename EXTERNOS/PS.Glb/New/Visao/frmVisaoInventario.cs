using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITGProducao.Controles;
using DevExpress.XtraSplashScreen;

namespace PS.Glb.New.Visao
{
    public partial class frmVisaoInventario : Form
    {
        public Form pai = null;
        public bool consulta = false;
        string tabela = "GINVENTARIO";
        string query = string.Empty;
        public string codMenu = string.Empty;
        private bool permiteEditar = true;

        List<string> tabelasFilhas = new List<string>();

        //Variáveis para usar quando a tela abre para consulta.
        public string Codinventario;

        //Variáveis para NewLookup
        private NewLookup lookup;

        //Variáveis para os processos do Inventário
        private int CODOPER;
        private string CODPRODUTO;
        private int Operacao;

        public frmVisaoInventario(string _query, Form frmprin, string _CodMenu)
        {
            InitializeComponent();
            codMenu = _CodMenu;
            this.MdiParent = frmprin;
            tabelasFilhas.Clear();
            query = _query;
            CarregaGrid(query, false);
            getAcesso(codMenu);
            getAcessoProcesso(codMenu);
        }

        public frmVisaoInventario(string _where, bool _consulta)
        {
            InitializeComponent();
            tabelasFilhas.Clear();
            query = _where;
            consulta = _consulta;
            CarregaGrid(query, _consulta);
        }

        public frmVisaoInventario(ref NewLookup lookup)
        {
            InitializeComponent();
            query = lookup.whereVisao;
            CarregaGrid(query, false);
            this.lookup = lookup;
        }

        private void frmVisaoInventario_Load(object sender, EventArgs e)
        {
            btnFiltros.Enabled = false;
            toolStripDropDownButton2.Enabled = false;
            toolStripDropDownButton4.Enabled = false;
            //int Operacao = GetTipoOperacaoEntrada();
        }

        private void getAcesso(string CodMenu)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO, EXCLUSAO, CONSULTA, INCLUSAO FROM GPERMISSAOMENU WHERE CODMENU = ? AND CODPERFIL = ?", new object[] { "btnUtilitarios_Inventario", AppLib.Context.Perfil });
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

        private void getAcessoProcesso(string CodMenu)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT GPERMISSAOPROCESSO.IDPERMISSAOPROCESSO, GPERMISSAOPROCESSO.CODMENUPROCESSO, GPERMISSAOPROCESSO.ACESSO FROM GMENUPROCESSO INNER JOIN GPERMISSAOPROCESSO ON GPERMISSAOPROCESSO.CODMENUPROCESSO = GMENUPROCESSO.CODMENUPROCESSO WHERE CODPERFIL = ? AND CODMENU = ?", new object[] { AppLib.Context.Perfil, "btnUtilitarios_Inventario" });
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable dtAcesso1 = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODMENUPROCESSO, ACESSO FROM GPERMISSAOPROCESSO WHERE CODUSUARIO = ? AND IDPERMISSAOPROCESSO = ?", new object[] { AppLib.Context.Usuario, dt.Rows[i]["IDPERMISSAOPROCESSO"] });

                    if (dtAcesso1.Rows[0]["CODMENUPROCESSO"].ToString() == "INVENTARIO_INICIAR" && Convert.ToBoolean(dtAcesso1.Rows[0]["ACESSO"]) == false)
                    {
                        iniciarInventárioToolStripMenuItem.Enabled = false;
                    }

                    DataTable dtAcesso2 = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODMENUPROCESSO, ACESSO FROM GPERMISSAOPROCESSO WHERE CODUSUARIO = ? AND IDPERMISSAOPROCESSO = ?", new object[] { AppLib.Context.Usuario, dt.Rows[i]["IDPERMISSAOPROCESSO"] });

                    if (dtAcesso1.Rows[0]["CODMENUPROCESSO"].ToString() == "INVENTARIO_ENCERRARCONTAGEM" && Convert.ToBoolean(dtAcesso1.Rows[0]["ACESSO"]) == false)
                    {
                        contagensToolStripMenuItem.Enabled = false;
                    }

                    DataTable dtAcesso3 = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODMENUPROCESSO, ACESSO FROM GPERMISSAOPROCESSO WHERE CODUSUARIO = ? AND IDPERMISSAOPROCESSO = ?", new object[] { AppLib.Context.Usuario, dt.Rows[i]["IDPERMISSAOPROCESSO"] });

                    if (dtAcesso1.Rows[0]["CODMENUPROCESSO"].ToString() == "INVENTARIO_CANCELAR" && Convert.ToBoolean(dtAcesso1.Rows[0]["ACESSO"]) == false)
                    {
                        cancelamentoDoInventárioToolStripMenuItem.Enabled = false;
                    }

                    DataTable dtAcesso4 = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODMENUPROCESSO, ACESSO FROM GPERMISSAOPROCESSO WHERE CODUSUARIO = ? AND IDPERMISSAOPROCESSO = ?", new object[] { AppLib.Context.Usuario, dt.Rows[i]["IDPERMISSAOPROCESSO"] });

                    if (dtAcesso1.Rows[0]["CODMENUPROCESSO"].ToString() == "INVENTARIO_ENCERRAR" && Convert.ToBoolean(dtAcesso1.Rows[0]["ACESSO"]) == false)
                    {
                        encerrarInventárioToolStripMenuItem.Enabled = false;
                    }
                }
            }
            else
            {
                iniciarInventárioToolStripMenuItem.Enabled = false;
                contagensToolStripMenuItem.Enabled = false;
                cancelamentoDoInventárioToolStripMenuItem.Enabled = false;
                encerrarInventárioToolStripMenuItem.Enabled = false;
            }
        }

        public void CarregaGrid(string where, bool consulta)
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

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregaGrid(query, false);
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            CarregaGrid(query, false);
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
                CarregaGrid(query, false);
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
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GINVENTARIO WHERE CODINVENTARIO = ? AND CODEMPRESA = ?", new object[] { dr["GINVENTARIO.CODINVENTARIO"], AppLib.Context.Empresa });

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
                    PS.Glb.New.Cadastros.frmCadastroInventario inventario = new Cadastros.frmCadastroInventario();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    inventario.CodInventario = row1["GINVENTARIO.CODINVENTARIO"].ToString();
                    inventario.edita = true;
                    inventario.codMenu = codMenu;
                    inventario.ShowDialog();
                    atualizaColuna(row1);
                }
            }
            else
            {
                //Apenas será usado se o Inventário tiver um Lookup
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                lookup.txtconteudo.Text = row1[tabela + ".DESCRICAO"].ToString().ToUpper();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                PS.Glb.New.Cadastros.frmCadastroInventario inventario = new Cadastros.frmCadastroInventario();
                inventario.edita = false;
                inventario.ShowDialog();
                CarregaGrid(query, consulta);
            }
            else
            {
                PS.Glb.New.Cadastros.frmCadastroInventario inventario = new Cadastros.frmCadastroInventario(ref this.lookup);
                inventario.edita = false;
                inventario.ShowDialog();
                this.Dispose();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    PS.Glb.New.Cadastros.frmCadastroInventario inventario = new Cadastros.frmCadastroInventario();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    inventario.CodInventario = row1["GINVENTARIO.CODINVENTARIO"].ToString();
                    inventario.edita = true;
                    inventario.ShowDialog();
                    atualizaColuna(row1);
                }
                else
                {
                    PS.Glb.New.Cadastros.frmCadastroInventario inventario = new Cadastros.frmCadastroInventario();
                    inventario.edita = false;
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
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    Codinventario = row1["GINVENTARIO.CODINVENTARIO"].ToString();
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir os registros selecionados?", "Confirmação.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                        if (new Class.Utilidades().Excluir("CODINVENTARIO", "GINVENTARIO", row["GINVENTARIO.CODINVENTARIO"].ToString()) == true)
                        {
                            if (gridView1.SelectedRowsCount > 0)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Não foi possível excluir registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    MessageBox.Show("Registro excluído com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregaGrid(query, false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #region Validações

        private bool ValidaContagem(DataRow row)
        {
            bool Validacao;

            Validacao = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT ENCERRARCONTAGEM1 FROM GINVENTARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODINVENTARIO = ? AND CODLOCAL = ?", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODINVENTARIO"], row["GINVENTARIO.CODLOCAL"] }));

            if (Validacao == true)
            {
                return Validacao;
            }
            else
            {
                return Validacao;
            }
        }

        /// <summary>
        /// Método de verificação de contagens.
        /// </summary>
        /// <param name="row">Parâmetro utilizado para retornar o registro selecionado da Grid</param>
        /// <returns>Retorna True caso a contagem anterior já tenha sido feita/ Retorna False caso a contagem anterior não tenha sido encerrada</returns>
        private bool ValidaContagem1(DataRow row)
        {
            bool Validacao;

            Validacao = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT ENCERRARCONTAGEM1 FROM GINVENTARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODINVENTARIO = ? AND CODLOCAL = ?", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODINVENTARIO"], row["GINVENTARIO.CODLOCAL"] }));

            if (Validacao == false)
            {
                MessageBox.Show("A contagem anterior precisa ser encerrada.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return Validacao;
            }
            else
            {
                return Validacao;
            }
        }

        /// <summary>
        /// Método de verificação de contagens.
        /// </summary>
        /// <param name="row">Parâmetro utilizado para retornar o registro selecionado da Grid</param>
        /// <returns>Retorna True caso a contagem anterior já tenha sido feita/ Retorna False caso a contagem anterior não tenha sido encerrada</returns>
        private bool ValidaContagem2(DataRow row)
        {
            bool Validacao;

            Validacao = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT ENCERRARCONTAGEM2 FROM GINVENTARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODINVENTARIO = ? AND CODLOCAL = ?", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODINVENTARIO"], row["GINVENTARIO.CODLOCAL"] }));

            if (Validacao == false)
            {
                MessageBox.Show("A contagem anterior precisa ser encerrada.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return Validacao;
            }
            else
            {
                return Validacao;
            }
        }

        #endregion

        #region Processos

        private void iniciarInventárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            if (row1["GINVENTARIO.STATUS"].ToString() != "Aberto")
            {
                MessageBox.Show("Somente Inventários com status Aberto podem ser iniciados.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (MessageBox.Show("Deseja iniciar os Inventários selecionados?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GITEMINVENTARIO SET GITEMINVENTARIO.SALDOINICIAL = VSALDOESTOQUE.SALDOFINAL FROM GITEMINVENTARIO INNER JOIN VSALDOESTOQUE ON VSALDOESTOQUE.CODEMPRESA = GITEMINVENTARIO.CODEMPRESA AND VSALDOESTOQUE.CODPRODUTO = GITEMINVENTARIO.CODPRODUTO WHERE GITEMINVENTARIO.CODEMPRESA = ? AND GITEMINVENTARIO.CODINVENTARIO = ? AND VSALDOESTOQUE.CODFILIAL = ? AND VSALDOESTOQUE.CODLOCAL = ?", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODINVENTARIO"], row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODLOCAL"] });

                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GINVENTARIO SET STATUS = 'Iniciado', DATAINVENTARIO = GETDATE() WHERE CODEMPRESA = ? AND CODLOCAL = ? AND CODFILIAL = ? AND CODINVENTARIO = ?", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODLOCAL"], row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODINVENTARIO"] });
                    }
                    CarregaGrid(query, false);
                }
            }
        }

        private void encerrar1ªContagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            bool Valida = ValidaContagem(row);

            if (Valida == false)
            {
                if (row["GINVENTARIO.STATUS"].ToString() == "Iniciado")
                {
                    if (gridView1.SelectedRowsCount > 0)
                    {
                        if (MessageBox.Show("Deseja encerrar a 1ª Contagem?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int Itens = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODINVENTARIO) FROM GITEMINVENTARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODINVENTARIO = ? AND GITEMINVENTARIO.CONTAGEM1 IS NULL", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODLOCAL"], row["GINVENTARIO.CODINVENTARIO"] }));

                            if (Itens > 0)
                            {
                                if (MessageBox.Show("Existem itens sem digitação da 1ª Contagem, deseja inserir 0 para o valor de contagem?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                {
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GITEMINVENTARIO SET CONTAGEM1 = 0, DIFERENCACONTAGEM1 = (0 - SALDOINICIAL), DIFERENCAFINAL = (0 - SALDOINICIAL), USUARIOCONTAGEM1 = '" + AppLib.Context.Usuario + "', DATA1CONTAGEM = GETDATE() WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODINVENTARIO = ? AND CONTAGEM1 IS NULL", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODLOCAL"], row["GINVENTARIO.CODINVENTARIO"] });

                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GINVENTARIO SET ENCERRARCONTAGEM1 = 1 WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODINVENTARIO = ?", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODLOCAL"], row["GINVENTARIO.CODINVENTARIO"] });

                                    MessageBox.Show("Encerramento executado com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GINVENTARIO SET ENCERRARCONTAGEM1 = 1 WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODINVENTARIO = ?", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODLOCAL"], row["GINVENTARIO.CODINVENTARIO"] });

                                MessageBox.Show("Encerramento executado com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Apenas Inventários com status Iniciado podem ter a 1ª contagem encerrada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("A 1ª contagem deste Inventário ja foi realizada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void encerrar2ªContagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            bool Valida = ValidaContagem1(row);

            if (Valida == true)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    if (MessageBox.Show("Deseja encerrar a 2ª Contagem?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GINVENTARIO SET ENCERRARCONTAGEM2 = 1 WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODINVENTARIO = ?", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODLOCAL"], row["GINVENTARIO.CODINVENTARIO"] });

                        MessageBox.Show("Encerramento executado com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void encerrar3ªContagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            bool Valida = ValidaContagem2(row);

            if (Valida == true)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    if (MessageBox.Show("Deseja encerrar a 3ª Contagem?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GINVENTARIO SET ENCERRARCONTAGEM3 = 1 WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODINVENTARIO = ?", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODLOCAL"], row["GINVENTARIO.CODINVENTARIO"] });

                        MessageBox.Show("Encerramento executado com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void cancelamentoDoInventárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja cancelar os Inventários selecionados?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                    if (row["GINVENTARIO.STATUS"].ToString() == "Aberto")
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GINVENTARIO SET STATUS = 'Cancelado' WHERE CODEMPRESA = ? AND CODLOCAL = ? AND CODFILIAL = ? AND CODINVENTARIO = ?", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODLOCAL"], row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODINVENTARIO"] });
                    }
                    else if (row["GINVENTARIO.STATUS"].ToString() == "Iniciado")
                    {
                        bool Validacao = ValidaContagem(row);

                        if (Validacao == true)
                        {
                            if (MessageBox.Show("Os itens do Inventário '" + row["GINVENTARIO.CODINVENTARIO"] + "' possuem contagens, deseja cancelá-lo mesmo assim?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GINVENTARIO SET STATUS = 'Cancelado' WHERE CODEMPRESA = ? AND CODLOCAL = ? AND CODFILIAL = ? AND CODINVENTARIO = ?", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODLOCAL"], row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODINVENTARIO"] });
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GINVENTARIO SET STATUS = 'Cancelado' WHERE CODEMPRESA = ? AND CODLOCAL = ? AND CODFILIAL = ? AND CODINVENTARIO = ?", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODLOCAL"], row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODINVENTARIO"] });
                        }
                    }
                    else
                    {
                        MessageBox.Show("Inventários com esse status não podem ser cancelados.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                CarregaGrid(query, false);
            }
        }

        private void encerrarInventárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();

            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            if (row["GINVENTARIO.STATUS"].ToString() != "Iniciado")
            {
                MessageBox.Show("Somente Inventários com status Iniciado podem ser encerrados.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Deseja encerrar o Inventário?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool GeraAcerto = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT GERARACERTO FROM GINVENTARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODINVENTARIO = ? AND CODLOCAL = ?", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODINVENTARIO"], row["GINVENTARIO.CODLOCAL"] }));

                if (GeraAcerto == true)
                {
                    Operacao = GetTipoOperacaoEntrada();

                    if (Operacao == 0)
                    {
                        CODOPER = IncluirGOPER(conn, TipoGOPER.Entrada, row);

                        if (CODOPER <= 0)
                        {
                            throw new Exception("Erro ao incluir operação");
                        }
                        else
                        {
                            splashScreenManager1.ShowWaitForm();
                            splashScreenManager1.SetWaitFormCaption("Encerrando Inventário...");

                            IncluirGOPERITEM(CODOPER, conn, row);

                            splashScreenManager1.CloseWaitForm();
                        }
                    }

                    Operacao = GetTipoOperacaoSaida();

                    if (Operacao == 1)
                    {
                        CODOPER = IncluirGOPER(conn, TipoGOPER.Saida, row);

                        if (CODOPER <= 0)
                        {
                            throw new Exception("Erro ao incluir operação");
                        }
                        else
                        {
                            splashScreenManager1.ShowWaitForm();
                            splashScreenManager1.SetWaitFormCaption("Encerrando Inventário...");

                            IncluirGOPERITEM(CODOPER, conn, row);

                            splashScreenManager1.CloseWaitForm();
                        }
                    }
                    //ExecutaProcedureVFICHAESTOQUE(row);

                    MessageBox.Show("Encerramento de Inventário executado com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    EncerrarInventario();

                    MessageBox.Show("Encerramento de Inventário executado com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            CarregaGrid(query, false);
        }

        #endregion

        #region Utilitários para o processo de conclusão de Inventário

        // Tipos de Operação para o Inventário
        enum TipoGOPER
        {
            Entrada,
            Saida
        }

        /// <summary>
        /// Método para incluir registro na tabela GOPER.
        /// </summary>
        /// <param name="conn">Conexão Ativa</param>
        /// <param name="TipoGoper">Tipo de Operação permitida para o Inventário</param>
        /// <param name="row">Parâmetro utilizado para retornar o registro selecionado da Grid</param>
        private int IncluirGOPER(AppLib.Data.Connection conn, TipoGOPER TipoGoper, DataRow row)
        {
            try
            {
                PS.Glb.Class.Goper _goper = new Class.Goper();

                _goper.CODEMPRESA = AppLib.Context.Empresa;
                _goper.CODFILIAL = Convert.ToInt32(row["GINVENTARIO.CODFILIAL"]);
                _goper.CODUSUARIOCRIACAO = AppLib.Context.Usuario;
                _goper.DATACRIACAO = conn.GetDateTime();
                _goper.CODCLIFOR = conn.ExecGetField(string.Empty, "SELECT CODCLIFOR FROM VCLIFOR WHERE CGCCPF IN (SELECT CGCCPF FROM GFILIAL WHERE CODFILIAL = ?)", new object[] { row["GINVENTARIO.CODFILIAL"] }).ToString();
                _goper.CODOPERADOR = conn.ExecGetField(string.Empty, "SELECT CODOPERADOR FROM VOPERADOR WHERE CODUSUARIO = ?", new object[] { AppLib.Context.Usuario }).ToString();

                switch (TipoGoper)
                {
                    case TipoGOPER.Entrada:

                        _goper.CODTIPOPER = GetInventarioEntrada(conn);
                        _goper.CODLOCAL = row["GINVENTARIO.CODLOCAL"].ToString();
                        _goper.CODSTATUS = "8"; // Concluído

                        break;
                    case TipoGOPER.Saida:

                        _goper.CODTIPOPER = GetInventarioSaida(conn);
                        _goper.CODLOCAL = row["GINVENTARIO.CODLOCAL"].ToString();
                        _goper.CODSTATUS = "8"; // Concluído

                        break;
                    default:
                        throw new Exception("Erro ao incluir operação.");
                }

                return _goper.setGoper(_goper, conn);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Método para incluir registro na tabela GOPERITEM.
        /// </summary>
        /// <param name="CODOPER">Código da Operação gerado no método IncluirGOPER</param>
        /// <param name="conn">Conexão Ativa</param>
        /// <param name="row">Parâmetro utilizado para retornar o registro selecionado da Grid</param>
        private void IncluirGOPERITEM(int CODOPER, AppLib.Data.Connection conn, DataRow row)
        {
            try
            {
                DataTable dtItens = ItensInventario(row);

                PS.Glb.Class.GoperItem _goperitem = new Class.GoperItem();

                PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque Estoque;
                Estoque = PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao;

                for (int i = 0; i < dtItens.Rows.Count; i++)
                {
                    _goperitem.CODEMPRESA = AppLib.Context.Empresa;
                    _goperitem.CODPRODUTO = dtItens.Rows[i]["CODPRODUTO"].ToString();
                    _goperitem.DATAENTREGA = null;
                    _goperitem.CODUNIDOPER = dtItens.Rows[i]["CODUNIDCONTROLE"].ToString();
                    _goperitem.APLICACAOMATERIAL = "C"; //CONSUMO
                    _goperitem.QUANTIDADE = Convert.ToDecimal(dtItens.Rows[i]["QUANTIDADE"]);
                    _goperitem.TIPODESCONTO = "U"; // Tipo de Desconto: Valor Unitário

                    //CODPRODUTO é setado de acordo com o item percorrido no Datatable.
                    CODPRODUTO = dtItens.Rows[i]["CODPRODUTO"].ToString();

                    _goperitem.setItem(_goperitem, conn, CODOPER);

                    EncerrarInventario();

                    psPartLocalEstoqueSaldoData.MovimentaEstoque2(Estoque, AppLib.Context.Empresa, CODOPER, _goperitem.NSEQITEM);

                    AtualizaItemInventario(CODPRODUTO);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Método para atualizar o Status e setar a Data de Conclusão da tabela GINVENTARIO.
        /// </summary>
        private void EncerrarInventario()
        {
            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GINVENTARIO SET DATACONCLUSAO = GETDATE(), STATUS = 'Encerrado' WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODINVENTARIO = ? AND CODLOCAL = ?", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODINVENTARIO"], row["GINVENTARIO.CODLOCAL"] });
        }

        /// <summary>
        /// Método para atualizar o CODOPER e o NSEQITEM da tabela GITEMINVENTARIO.
        /// </summary>
        /// <param name="CodProduto">Código do Produto</param>
        private void AtualizaItemInventario(string CodProduto)
        {
            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            int NSEQITEM = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT NSEQITEM FROM GOPERITEM WHERE CODOPER = ? AND CODEMPRESA = ? AND CODPRODUTO = ?", new object[] { CODOPER, AppLib.Context.Empresa, CODPRODUTO }));

            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GITEMINVENTARIO SET CODOPER = '" + CODOPER + "', NSEQITEM = " + NSEQITEM + " WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODINVENTARIO = ? AND CODLOCAL = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODINVENTARIO"], row["GINVENTARIO.CODLOCAL"], CODPRODUTO });
        }

        /// <summary>
        /// Método para verificar se a Operação será de Entrada de acordo com o retorno da Query.
        /// </summary>
        /// <returns>Retorna TipoGOPER como Entrada/ Retorna 2 para não ser uma opção válida.</returns>
        private int GetTipoOperacaoEntrada()
        {
            TipoGOPER Operacao;

            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            int VerificacaoEntrada = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODPRODUTO) FROM GITEMINVENTARIO WHERE CODEMPRESA = ? AND CODLOCAL = ? AND CODFILIAL = ? AND CODINVENTARIO = ? AND DIFERENCAFINAL > 0", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODLOCAL"], row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODINVENTARIO"] }));

            if (VerificacaoEntrada > 0)
            {
                Operacao = TipoGOPER.Entrada;
                return (int)Operacao;
            }
            else
            {
                return 2; // Retorna 2 para não ser nenhuma das opções.
            }
        }

        /// <summary>
        /// Método para verificar se a Operação será de Saída de acordo com o retorno da Query.
        /// </summary>
        /// <returns>Retorna TipoGOPER como Saída/ Retorna 2 para não ser uma opção válida.</returns>
        private int GetTipoOperacaoSaida()
        {
            TipoGOPER Operacao;

            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            int VerificacaoSaida = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODPRODUTO) FROM GITEMINVENTARIO WHERE CODEMPRESA = ? AND CODLOCAL = ? AND CODFILIAL = ? AND CODINVENTARIO = ? AND DIFERENCAFINAL < 0", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODLOCAL"], row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODINVENTARIO"] }));

            if (VerificacaoSaida > 0)
            {
                Operacao = TipoGOPER.Saida;
                return (int)Operacao;
            }
            else
            {
                return 2; // Retorna 2 para não ser nenhuma das opções.
            }
        }

        /// <summary>
        /// Método para retornar o código do tipo de Operação de Entrada.
        /// </summary>
        /// <param name="conn">Conexão Ativa</param>
        /// <returns>Retorna o Código do Tipo de Operação</returns>
        private string GetInventarioEntrada(AppLib.Data.Connection conn)
        {
            string oper = conn.ExecGetField(string.Empty, "SELECT CODTIPOPERINVENTRADA FROM VPARAMETROS WHERE CODEMPRESA =?", new object[] { AppLib.Context.Empresa }).ToString();
            return oper;
        }

        /// <summary>
        /// Método para retornar o código do tipo de Operação de Saída.
        /// </summary>
        /// <param name="conn">Conexão Ativa</param>
        /// <returns>Retorna o Código do Tipo de Operação</returns>
        private string GetInventarioSaida(AppLib.Data.Connection conn)
        {
            string oper = conn.ExecGetField(string.Empty, "SELECT CODTIPOPERINVSAIDA FROM VPARAMETROS WHERE CODEMPRESA =?", new object[] { AppLib.Context.Empresa }).ToString();
            return oper;
        }

        /// <summary>
        ///  Método para retornar um Datatable com os itens para o Insert na GOPERITEM.
        /// </summary>
        /// <param name="row">Parâmetro utilizado para retornar o registro selecionado da Grid</param>
        /// <returns>Retorna um Datatable com os itens do Inventário</returns>
        private DataTable ItensInventario(DataRow row)
        {
            DataTable dt;

            if (Operacao == 0)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
SELECT GITEMINVENTARIO.CODPRODUTO, GITEMINVENTARIO.CODUNIDCONTROLE, GITEMINVENTARIO.CODLOCAL,                                                                                     
CASE 	                                                                                   
WHEN (GITEMINVENTARIO.DIFERENCAFINAL > 0) THEN GITEMINVENTARIO.DIFERENCAFINAL	                                                                                    
WHEN (GITEMINVENTARIO.DIFERENCAFINAL < 0) THEN (GITEMINVENTARIO.DIFERENCAFINAL * -1)                                                                                    
END QUANTIDADE, ISNULL(VSALDOESTOQUE.CUSTOMEDIO, 0) VALORUNITARIO                                                                                   
FROM GITEMINVENTARIO 
LEFT OUTER JOIN VSALDOESTOQUE ON VSALDOESTOQUE.CODEMPRESA = GITEMINVENTARIO.CODEMPRESA AND VSALDOESTOQUE.CODFILIAL = GITEMINVENTARIO.CODFILIAL AND VSALDOESTOQUE.CODLOCAL = GITEMINVENTARIO.CODLOCAL AND VSALDOESTOQUE.CODPRODUTO = GITEMINVENTARIO.CODPRODUTO
WHERE GITEMINVENTARIO.CODEMPRESA = ? AND GITEMINVENTARIO.CODLOCAL = ? AND GITEMINVENTARIO.CODFILIAL = ? AND GITEMINVENTARIO.CODINVENTARIO = ? AND GITEMINVENTARIO.DIFERENCAFINAL > 0", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODLOCAL"], row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODINVENTARIO"] });

                return dt;
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
SELECT GITEMINVENTARIO.CODPRODUTO, GITEMINVENTARIO.CODUNIDCONTROLE, GITEMINVENTARIO.CODLOCAL,                                                                                     
CASE 	                                                                                   
WHEN (GITEMINVENTARIO.DIFERENCAFINAL > 0) THEN GITEMINVENTARIO.DIFERENCAFINAL	                                                                                    
WHEN (GITEMINVENTARIO.DIFERENCAFINAL < 0) THEN (GITEMINVENTARIO.DIFERENCAFINAL * -1)                                                                                    
END QUANTIDADE, ISNULL(VSALDOESTOQUE.CUSTOMEDIO, 0) VALORUNITARIO                                                                                   
FROM GITEMINVENTARIO 
LEFT OUTER JOIN VSALDOESTOQUE ON VSALDOESTOQUE.CODEMPRESA = GITEMINVENTARIO.CODEMPRESA AND VSALDOESTOQUE.CODFILIAL = GITEMINVENTARIO.CODFILIAL AND VSALDOESTOQUE.CODLOCAL = GITEMINVENTARIO.CODLOCAL AND VSALDOESTOQUE.CODPRODUTO = GITEMINVENTARIO.CODPRODUTO
WHERE GITEMINVENTARIO.CODEMPRESA = ? AND GITEMINVENTARIO.CODLOCAL = ? AND GITEMINVENTARIO.CODFILIAL = ? AND GITEMINVENTARIO.CODINVENTARIO = ? AND GITEMINVENTARIO.DIFERENCAFINAL < 0", new object[] { AppLib.Context.Empresa, row["GINVENTARIO.CODLOCAL"], row["GINVENTARIO.CODFILIAL"], row["GINVENTARIO.CODINVENTARIO"] });

                return dt;
            }
        }

        private void ExecutaProcedureVFICHAESTOQUE(DataRow row)
        {
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppLib.Context.poolConnection.Get("Start").ConnectionString);

            try
            {
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("Recalcula_Estoque_Inventario", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 6000;

                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EMPRESA", SqlDbType.Int)).Value = AppLib.Context.Empresa;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FILIAL", SqlDbType.Int)).Value = row["GINVENTARIO.CODFILIAL"];
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LOCALESTOQUE", SqlDbType.VarChar)).Value = row["GINVENTARIO.CODLOCAL"].ToString();
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODINVENTARIO", SqlDbType.VarChar)).Value = row["GINVENTARIO.CODINVENTARIO"].ToString();
                conn.Open();

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        #endregion
    }
}

