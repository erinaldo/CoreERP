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
    public partial class frmCadastroUnidadeMedida : Form
    {
        public bool edita = false;
        public bool consulta = false;
        public string codMenu;
        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;

        string tabela = "VUNIDCONVERSAO";
        string query = string.Empty;

        //Variáveis para usar quando a tela abre para consulta.
        public string CodUnidade = string.Empty;
        public string CodUnidadeConversao = string.Empty;

        public frmCadastroUnidadeMedida()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VUNID");
        }

        public frmCadastroUnidadeMedida(ref NewLookup lookup)
        {
            InitializeComponent();
            CarregaGridConversaoUnidade("WHERE CODUNID = '" + CodUnidade + "'");
            this.edita = true;
            this.lookup = lookup;
        }

        private void frmCadastroUnidadeMedida_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                tbCodUnidade.Enabled = false;
                CarregaGridConversaoUnidade("WHERE CODUNID = '" + CodUnidade + "'");
                CarregaCampos();
            }
            else
            {
                CarregaGridConversaoUnidade("WHERE CODUNID = '" + CodUnidade + "'");
            }
        }

        public void CarregaGridConversaoUnidade(string where)
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

        private bool validacoes()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(tbCodUnidade.Text))
            {
                errorProvider1.SetError(tbCodUnidade, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }

        private void carregaObj(DataTable dt)
        {
            tbCodUnidade.Text = dt.Rows[0]["CODUNID"].ToString();
            tbNome.Text = dt.Rows[0]["NOME"].ToString();
        }

        private void CarregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VUNID WHERE CODUNID = ? AND CODEMPRESA = ?", new object[] { CodUnidade, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VUNID WHERE CODUNID = ? AND CODEMPRESA = ?", new object[] { CodUnidade, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private bool Salvar()
        {
            if (validacoes() == false)
            {
                return false;
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VUNID = new AppLib.ORM.Jit(conn, "VUNID");
            conn.BeginTransaction();

            try
            {
                VUNID.Set("CODEMPRESA", AppLib.Context.Empresa);
                VUNID.Set("CODUNID", tbCodUnidade.Text);
                VUNID.Set("NOME", tbNome.Text);
                VUNID.Set("CODUNIDBASE", tbCodUnidade.Text);
                VUNID.Set("FATORCONVERSAO", 1);

                VUNID.Save();
                conn.Commit();

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
            if (this.lookup == null)
            {
                Salvar();
            }
            else
            {
                if (Salvar() == true)
                {
                    CodUnidade = tbCodUnidade.Text;
                    CarregaCampos();
                    this.Dispose();
                }
            }
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void atualizaColuna(DataRow dr)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VUNIDCONVERSAO WHERE CODUNID = ? AND CODUNIDCONVERSAO = ? AND CODEMPRESA = ?", new object[] { dr["VUNIDCONVERSAO.CODUNID"], dr["VUNIDCONVERSAO.CODUNIDCONVERSAO"], AppLib.Context.Empresa });

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
                    PS.Glb.New.Cadastros.frmCadastroUnidadeConversao Unidade = new Cadastros.frmCadastroUnidadeConversao();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    Unidade.CodUnidade = row1["VUNIDCONVERSAO.CODUNID"].ToString();
                    Unidade.CodUnidadeConversao = row1["VUNIDCONVERSAO.CODUNIDCONVERSAO"].ToString();
                    Unidade.edita = true;
                    Unidade.ShowDialog();
                    atualizaColuna(row1);
                }
            }
            else
            {
                //Apenas será usado se o Inventário tiver um Lookup
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                lookup.txtconteudo.Text = row1[tabela + ".DESCRICAO"].ToString().ToUpper();
                lookup.txtcodigo.Text = row1[tabela + ".CODUNID"].ToString();
                lookup.ValorCodigoInterno = row1[tabela + ".CODUNID"].ToString();
                this.Dispose();
            }
        }

        private void btnNovoFatorConversao_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                PS.Glb.New.Cadastros.frmCadastroUnidadeConversao Unidade = new Cadastros.frmCadastroUnidadeConversao();
                Unidade.edita = false;
                Unidade.ShowDialog();
                CarregaGridConversaoUnidade("WHERE CODUNID = '" + CodUnidade + "'");
            }
            else
            {
                PS.Glb.New.Cadastros.frmCadastroUnidadeConversao Unidade = new Cadastros.frmCadastroUnidadeConversao(ref this.lookup);
                Unidade.edita = false;
                Unidade.ShowDialog();
                this.Dispose();
            }
        }

        private void btnEditarFatorConversao_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    PS.Glb.New.Cadastros.frmCadastroUnidadeConversao Unidade = new Cadastros.frmCadastroUnidadeConversao();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    Unidade.CodUnidade = row1["VUNIDCONVERSAO.CODUNID"].ToString();
                    Unidade.CodUnidadeConversao = row1["VUNIDCONVERSAO.CODUNIDCONVERSAO"].ToString();
                    Unidade.edita = true;
                    Unidade.ShowDialog();
                    atualizaColuna(row1);
                }
                else
                {
                    PS.Glb.New.Cadastros.frmCadastroUnidadeConversao Unidade = new Cadastros.frmCadastroUnidadeConversao();
                    Unidade.edita = false;
                }
            }
            else
            {
                gridControl1_DoubleClick_1(sender, e);
            }
        }

        private void gridControl1_DoubleClick_1(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (consulta == true)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    CodUnidade = row1["VUNIDCONVERSAO.CODUNID"].ToString();
                    CodUnidadeConversao = row1["VUNIDCONVERSAO.CODUNIDCONVERSAO"].ToString();
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

        private void btnPesquisarFatorConversao_Click(object sender, EventArgs e)
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

        private void btnAgruparFatorConversao_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsView.ShowGroupPanel == true)
            {
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.ClearGrouping();
                btnAgruparFatorConversao.Text = "Agrupar";
            }
            else
            {
                gridView1.OptionsView.ShowGroupPanel = true;
                btnAgruparFatorConversao.Text = "Desagrupar";
            }
        }

        private void btnAtualizarFatorConversao_Click(object sender, EventArgs e)
        {
            CarregaGridConversaoUnidade("WHERE CODUNID = '" + CodUnidade + "'");
        }

        private void btnSelecionarColunasFatorConversao_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            CarregaGridConversaoUnidade("WHERE CODUNID = '" + CodUnidade + "'");
        }

        private void btnSalvarLayoutFatorConversao_Click(object sender, EventArgs e)
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
                CarregaGridConversaoUnidade("WHERE CODUNID = '" + CodUnidade + "'");
            }
        }
    }
}
