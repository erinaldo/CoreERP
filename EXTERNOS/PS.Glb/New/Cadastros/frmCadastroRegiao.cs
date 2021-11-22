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
    public partial class frmCadastroRegiao : Form
    {
        string tabela = "VREGIAOESTADO";
        string query = string.Empty;
        public bool consulta = false;
        public bool edita = false;
        public string CodRegiao = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;
        public frmCadastroRegiao()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VREGIAO");
            CarregaGrid("WHERE CODREGIAO ='" + CodRegiao + "'", false);
        }
        public frmCadastroRegiao(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VREGIAO");
            CarregaGrid("WHERE CODREGIAO ='" + CodRegiao + "'", false);
            this.edita = true;
            this.lookup = lookup;
            CodRegiao = lookup.ValorCodigoInterno;
            CarregaCampos();
        }
        private void frmCadastroRegiao_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                CarregaCampos();
                CarregaGrid("WHERE CODREGIAO ='" + CodRegiao + "'", false);
                tbCodRegiao.Enabled = false;
                //Região/Estado
                btnEditar1.Enabled = false;
                btnFiltros1.Enabled = false;
                btnExportar1.Enabled = false;
                btnAnexos1.Enabled = false;
                btnProcessos1.Enabled = false;
                //
            }
            else
            {
                gridControl1.Enabled = false;
                toolStrip2.Enabled = false;
            }
        }
        private bool validações()
        {
            bool verifica = true;

            errorProvider.Clear();

            if (string.IsNullOrEmpty(tbCodRegiao.Text))
            {
                errorProvider.SetError(tbCodRegiao, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }
        private void carregaObj(DataTable dt)
        {
            tbCodRegiao.Text = dt.Rows[0]["CODREGIAO"].ToString();
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
        }
        private void CarregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VREGIAO WHERE CODREGIAO = ? AND CODEMPRESA = ?", new object[] { CodRegiao, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VREGIAO WHERE CODREGIAO = ? AND CODEMPRESA = ?", new object[] { CodRegiao, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }
        private bool Salvar()
        {
            if (validações() == false)
            {
                return false;
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VREGIAO = new AppLib.ORM.Jit(conn, "VREGIAO");
            conn.BeginTransaction();

            try
            {
                VREGIAO.Set("CODEMPRESA", AppLib.Context.Empresa);
                VREGIAO.Set("CODREGIAO", tbCodRegiao.Text);
                VREGIAO.Set("DESCRICAO", tbDescricao.Text);
                VREGIAO.Save();
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
                if (validações() == false)
                {
                    gridControl1.Enabled = false;
                }
                else
                {
                    Salvar();
                    gridControl1.Enabled = true;
                    toolStrip2.Enabled = true;
                }
            }
            else
            {
                if (Salvar() == true)
                {
                    CodRegiao = tbCodRegiao.Text;
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

                for (int i = 0; i < tabelasFilhas.Count; i++)
                {
                    dic = new DataTable();
                    dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODREGIAO, CODETD FROM VREGIAOESTADO WHERE TABELA = ? AND CODREGIAO = CODREGIAO", new object[] { tabelasFilhas[i].ToString(), CodRegiao });
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPesquisar1_Click(object sender, EventArgs e)
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
        private void btnAgrupar1_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsView.ShowGroupPanel == true)
            {
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.ClearGrouping();
                btnAgrupar1.Text = "Agrupar";
            }
            else
            {
                gridView1.OptionsView.ShowGroupPanel = true;
                btnAgrupar1.Text = "Desagrupar";
            }
        }      
        private void btnAtualizar1_Click(object sender, EventArgs e)
        {
            CarregaGrid("WHERE CODREGIAO ='" + CodRegiao + "'", false);
        }
        private void btnSelecionarColunas1_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            CarregaGrid("WHERE CODREGIAO ='" + CodRegiao + "'", false);
        }
        private void btnSalvarLayout1_Click(object sender, EventArgs e)
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
                CarregaGrid("WHERE CODREGIAO ='" + CodRegiao + "'", false);
            }
        }
        private void btnNovo_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage1);        
        }
        private void btnNovo1_Click(object sender, EventArgs e)
        {
            using (PS.Glb.New.Cadastros.frmCadastroRegiaoEstado state = new frmCadastroRegiaoEstado())
            {
                state._CodRegiao = tbCodRegiao.Text;
                state.ShowDialog();
                CodRegiao = tbCodRegiao.Text;
                CarregaGrid("WHERE CODREGIAO ='" + CodRegiao + "'", false);
            }
        }
        private void btnExcluir1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir os registros selecionados?", "Confirmação.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                if (gridView1 == null || gridView1.SelectedRowsCount == 0) return;

                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM VREGIAOESTADO WHERE CODREGIAO = ? AND CODEMPRESA = ? AND CODETD = ?", new object[] { row["CODREGIAO"], AppLib.Context.Empresa, row["CODETD"]});
                }
                gridView1.DeleteSelectedRows();
                MessageBox.Show("Registros excluídos com sucesso.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
