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
    public partial class frmVisaoContatoCliente : Form
    {
        string tabela = "VCLIFORCONTATO";
        string relacionamento = string.Empty;
        string query = string.Empty;
        List<string> tabelasFilhas = new List<string>();
        public string codCliente = string.Empty;

        public bool consulta = false;
        public string codContato;
        public string nome;
        


        public frmVisaoContatoCliente(string _codCliente, Form frmprin, string _query)
        {
            InitializeComponent();
            this.MdiParent = (Form)frmprin;
            query = _query;
            codCliente = _codCliente;
            carregaGrid(query);
            splitContainer1.SplitterDistance = 30;
        }

        public frmVisaoContatoCliente(string _where)
        {
            InitializeComponent();
            query = _where;
            carregaGrid(query);
        }

        public void carregaGrid(string where)
        {
            //Verifica se existe registro na tabela GVISAOUSUARIO
            //int colunas = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(COLUNA) FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1", new object[] { tabela, AppLib.Context.Usuario }));
            //if (colunas == 0)
            //{
            //    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });
            //    string tabelas = "'" + tabela + "'";
            //    for (int i = 0; i < tabelasFilhas.Count; i++)
            //    {
            //        tabelas = tabelas + ", '" + tabelasFilhas[i].ToString() + "'";
            //    }
            //    DataTable db = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUMN_NAME, TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME IN ( " + tabelas + "  )", new object[] { });
            //    for (int i = 0; i < db.Rows.Count; i++)
            //    {
            //        AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO GVISAOUSUARIO (VISAO, CODUSUARIO, SEQUENCIA, COLUNA, LARGURA, VISIVEL) VALUES (?, ?, ?, ?, ?, ?)", new object[] { tabela, AppLib.Context.Usuario, i, (db.Rows[i]["TABLE_NAME"].ToString() + "." + db.Rows[i]["COLUMN_NAME"].ToString()), 100, 1 });
            //    }
            //}
            //DataTable dt = new DataTable();
            //dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
            //string sql = string.Empty;

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (string.IsNullOrEmpty(sql))
            //    {
            //        sql = "SELECT " + dt.Rows[i]["COLUNA"].ToString() + " AS " + "'" + dt.Rows[i]["COLUNA"].ToString() + "'";
            //    }
            //    else
            //    {
            //        sql = sql + ", " + dt.Rows[i]["COLUNA"].ToString() + " AS " + "'" + dt.Rows[i]["COLUNA"].ToString() + "'";
            //    }
            //}
            //sql = sql + " FROM " + tabela + " " + relacionamento + " " + where;

            string sql = new Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);

            if (string.IsNullOrEmpty(sql))
            {
                MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();


            DataTable dtGrid = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

            gridControl1.DataSource = dtGrid;

            DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
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
                    //FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
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

        private void btnNovo_Click(object sender, EventArgs e)
        {
                New.Cadastros.frmCadastroContatoCliente frm = new New.Cadastros.frmCadastroContatoCliente(codCliente);
                frm.edita = false;
                frm.ShowDialog();
                carregaGrid(query);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                New.Cadastros.frmCadastroContatoCliente frm = new New.Cadastros.frmCadastroContatoCliente(codCliente, Convert.ToInt32(row1["VCLIFORCONTATO.CODCONTATO"]));
                frm.edita = true;
                frm.ShowDialog();
                carregaGrid(query);
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (consulta == true)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                codContato = row1["VCLIFORCONTATO.CODCONTATO"].ToString();
                nome = row1["VCLIFORCONTATO.NOME"].ToString();
                this.Dispose();
                GC.Collect();
            }
            else
            {
                btnEditar_Click(sender, e);
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

                        if (new Class.Utilidades().Excluir("CODCONTATO", "VCLIFORCONTATO", row["VCLIFORCONTATO.CODCONTATO"].ToString()) == true)
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
                    carregaGrid(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
