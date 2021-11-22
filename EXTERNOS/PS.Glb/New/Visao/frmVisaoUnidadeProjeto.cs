using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS.Glb.Class;

namespace PS.Glb.New.Visao
{
    public partial class frmVisaoUnidadeProjeto : Form
    {
        string Condicao = String.Empty;
        string sql = String.Empty;
        string tabela = "AUNIDADEPROJETO";
        public DataRowCollection drc { get; set; }
        public int CODEMPRESA { get; set; }
        public String CODFILIAL { get; set; }
        public String CODUNIDADE { get; set; }

        public frmVisaoUnidadeProjeto(String _Condicao)
        {
            InitializeComponent();
        }

        private void SalvarLayout()
        {
            try
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
                    AtualizaGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizaGrid()
        {
            sql = String.Format(@"select * from APROJETO
                                    where CODEMPRESA = '{0}'
                                    and CODFILIAL = '{1}'
                                    and IDUNIDADE = '{2}'", CODEMPRESA, CODFILIAL, CODUNIDADE);
            gridControl1.DataSource = MetodosSQL.GetDT(sql);

            try
            {
                sql = String.Format(@"SELECT COUNT(1) as 'TOTAL' FROM GVISAOUSUARIO WHERE VISAO = '{0}' AND CODUSUARIO = '{1}' AND VISIVEL = 1", tabela, AppLib.Context.Usuario);
                if (MetodosSQL.GetField(sql, "TOTAL") != "0")
                {
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
                else
                {
                    SalvarLayout();
                }
            }
            catch (Exception)
            {

            }
        }

        private void frmVisaoUnidadeProjeto_Load(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {

        }



        public System.Data.DataRowCollection GetDataRows(Boolean ValidarSelecao)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                System.Data.DataTable dt = new DataTable();
                int[] handles = gridView1.GetSelectedRows();

                for (int i = 0; i < handles.Length; i++)
                {
                    if (i == 0)
                    {
                        for (int col = 0; col < gridView1.GetDataRow(handles[i]).Table.Columns.Count; col++)
                        {
                            dt.Columns.Add(gridView1.GetDataRow(handles[i]).Table.Columns[col].ColumnName);
                        }
                    }

                    if (handles[i] >= 0)
                    {
                        dt.Rows.Add(gridView1.GetDataRow(handles[i]).ItemArray);
                    }
                }

                return dt.Rows;
            }
            else
            {
                if (ValidarSelecao)
                {
                    MessageBox.Show("Selecione o(s) registro(s).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return null;
            }
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            drc = GetDataRows(true);
            this.Close();
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvarLayout_Click(object sender, EventArgs e)
        {
            SalvarLayout();
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            AtualizaGrid();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                PS.Glb.New.Cadastros.frmCadastroProjeto frm = new Cadastros.frmCadastroProjeto();
                frm.IDPROJETO = null;
                frm.CODEMPRESA = AppLib.Context.Empresa.ToString();
                frm.ShowDialog();
                AtualizaGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                //CodUsuario = row1["GUSUARIO.CODUSUARIO"].ToString();
                //Nome = row1["GUSUARIO.CODUSUARIO"].ToString();
                PS.Glb.New.Cadastros.frmCadastroProjeto frm = new Cadastros.frmCadastroProjeto();
                frm.IDPROJETO = row1["IDPROJETO"].ToString();
                frm.CODEMPRESA = row1["CODEMPRESA"].ToString();
                frm.ShowDialog();
                AtualizaGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Deseja exluir o registro selecionado?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    sql = String.Format(@"delete from APROJETO where IDPROJETO = '{0}'", row1["IDPROJETO"].ToString());
                    MetodosSQL.ExecQuery(sql);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            drc = GetDataRows(true);
            this.Close();
        }
    }
}
