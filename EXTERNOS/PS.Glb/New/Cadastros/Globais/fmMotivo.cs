using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Cadastros.Globais
{
    public partial class fmMotivo : Form
    {
        public bool edita;
        public string codMotivo, descricao;
        private string tabela = "GMOTIVOUTILIZACAO";

        string relacionamento = "";
        List<string> tabelasFilhas = new List<string>();

        public fmMotivo()
        {
            InitializeComponent();

            cmbUtilizacao.SelectedIndex = -1;

        }

        private void carregaCampos()
        {
            txtCodMotivo.Text = codMotivo;
            txtDescricao.Text = descricao;
        }

        private void carregaGrid()
        {
            try
            {
                string where = "WHERE GMOTIVOUTILIZACAO.CODMOTIVO = " + txtCodMotivo.Text + "";
                string sql = new Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridControl2.DataSource = null;
                gridView2.Columns.Clear();
                gridControl2.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridView2.Columns.Count; i++)
                {
                    gridView2.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridView2.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridView2.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ////Verifica se existe registro na tabela GVISAOUSUARIO
            //int colunas = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(COLUNA) FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1", new object[] { tabela, AppLib.Context.Usuario }));
            //if (colunas == 0)
            //{
            //    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });
            //    DataTable db = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = ?", new object[] { tabela });
            //    for (int i = 0; i < db.Rows.Count; i++)
            //    {
            //        AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO GVISAOUSUARIO (VISAO, CODUSUARIO, SEQUENCIA, COLUNA, LARGURA, VISIVEL) VALUES (?, ?, ?, ?, ?, ?)", new object[] { tabela, AppLib.Context.Usuario, i, db.Rows[i]["COLUMN_NAME"].ToString(), 100, 1 });
            //    }
            //}
            //DataTable dt = new DataTable();
            //dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
            //string sql = string.Empty;

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (string.IsNullOrEmpty(sql))
            //    {
            //        sql = "SELECT " + dt.Rows[i]["COLUNA"].ToString();
            //    }
            //    else
            //    {
            //        sql = sql + ", " + dt.Rows[i]["COLUNA"].ToString();
            //    }
            //}
            //sql = sql + " FROM " + tabela + " " + "WHERE GMOTIVOUTILIZACAO.CODMOTIVO = ?";

            //gridControl2.DataSource = null;
            //gridView2.Columns.Clear();
            //gridControl2.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { txtCodMotivo.Text });



            //DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
            //for (int i = 0; i < gridView2.Columns.Count; i++)
            //{
            //    gridView2.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
            //    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
            //    DataRow result = dic.Rows.Find(new object[] { gridView2.Columns[i].FieldName.ToString() });
            //    if (result != null)
            //    {
            //        gridView2.Columns[i].Caption = result["DESCRICAO"].ToString();
            //    }
            //}
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            #region Validação
            if (!string.IsNullOrEmpty(txtDescricao.Text))
            {
                if (!string.IsNullOrEmpty(cmbUtilizacao.Text))
                {
                    if (txtDescricao.Text != descricao)
                    {
                        bool retorno = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT COUNT(*) FROM GMOTIVO WHERE DESCRICAO = ?", new object[] { txtDescricao.Text }));
                        if (retorno == true)
                        {
                            MessageBox.Show("Já existe uma descrição cadastrada com o mesmo nome. \nFavor verificar.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Favor preencher a utilização corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Favor preencher a descrição corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            #endregion

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();

            if (edita == true)
            {

                try
                {
                    conn.ExecTransaction("UPDATE GMOTIVO SET DESCRICAO = ? WHERE CODMOTIVO = ?", new object[] { txtDescricao.Text, txtCodMotivo.Text });
                    conn.ExecTransaction("INSERT INTO GMOTIVOUTILIZACAO (CODMOTIVO, UTILIZACAO) VALUES (?, ?)", new object[] { txtCodMotivo.Text, cmbUtilizacao.Text });
                    conn.Commit();
                }
                catch (Exception)
                {
                    conn.Rollback();
                    MessageBox.Show("Não foi possível completar a operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                try
                {
                    txtCodMotivo.Text = conn.ExecGetField("0", "INSERT INTO GMOTIVO (DESCRICAO) VALUES (?); SELECT SCOPE_IDENTITY()", new object[] { txtDescricao.Text }).ToString();
                    conn.ExecTransaction("INSERT INTO GMOTIVOUTILIZACAO (CODMOTIVO, UTILIZACAO) VALUES (?, ?)", new object[] { txtCodMotivo.Text, cmbUtilizacao.Text });
                    conn.Commit();
                }
                catch (Exception)
                {
                    conn.Rollback();
                    MessageBox.Show("Não foi possível completar a operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            edita = true;
            descricao = txtDescricao.Text;
            cmbUtilizacao.SelectedIndex = -1;
            carregaGrid();
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            carregaGrid();
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            carregaGrid();
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            salvarLayout();
        }

        private void salvarLayout()
        {
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });


            for (int i = 0; i < gridView2.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", tabela);
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView2.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView2.VisibleColumns[i].Width);
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
                carregaGrid();
            }

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (gridView2.OptionsView.ShowGroupPanel == true)
            {
                gridView2.OptionsView.ShowGroupPanel = false;
                gridView2.ClearGrouping();
                toolStripButton11.Text = "Agrupar";
            }
            else
            {
                gridView2.OptionsView.ShowGroupPanel = true;
                toolStripButton11.Text = "Desagrupar";
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (gridView2.OptionsFind.AlwaysVisible == true)
            {
                gridView2.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView2.OptionsFind.AlwaysVisible = true;
            }
        }

        private void fmMotivo_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                carregaGrid();
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView2.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView2.GetDataRow(Convert.ToInt32(gridView2.GetSelectedRows().GetValue(i).ToString()));
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GMOTIVOUTILIZACAO WHERE CODMOTIVO = ? AND UTILIZACAO = ?", new object[] { row1["CODMOTIVO"].ToString(), row1["UTILIZACAO"].ToString() });
            }
            carregaGrid();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
