using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmInventarioProdutos : Form
    {
        string tabela = "VPRODUTO";
        string query = string.Empty;
        public string Codinventario = string.Empty;
        List<string> Itens = new List<string>();

        List<string> tabelasFilhas = new List<string>();

        public frmInventarioProdutos(string _query)
        {
            InitializeComponent();
            query = _query;
            carregaGrid(query);
        }

        public void carregaGrid(string where)
        {
            try
            {
                string sql = string.Empty;

                sql = new Class.Utilidades().getVisao(tabela, string.Empty, tabelasFilhas, query);

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

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = (GridView)sender;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = view.CalcHitInfo(e.Location);
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.ColumnButton)
            {
                view.SelectAll();
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
            }
        }

        private bool Salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit GITEMINVENTARIO = new AppLib.ORM.Jit(conn, "GITEMINVENTARIO");
            //conn.BeginTransaction();

            try
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    DataTable inventario = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GINVENTARIO WHERE CODEMPRESA = ? AND CODINVENTARIO = ?", new object[] { AppLib.Context.Empresa, Codinventario });

                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                        #region Variáveis

                        int NSEQITEMINVENTARIO = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(1, "SELECT (MAX(NSEQITEMINVENTARIO) + 1) FROM GITEMINVENTARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODINVENTARIO = ? AND CODLOCAL = ?", new object[] { inventario.Rows[0]["CODEMPRESA"], inventario.Rows[0]["CODFILIAL"], Codinventario, inventario.Rows[0]["CODLOCAL"] }));

                        decimal SALDOINICIAL = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT SALDOFINAL FROM VSALDOESTOQUE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODPRODUTO = ?", new object[] { inventario.Rows[0]["CODEMPRESA"], inventario.Rows[0]["CODFILIAL"], inventario.Rows[0]["CODLOCAL"], row["VPRODUTO.CODPRODUTO"] }));

                        #endregion

                        GITEMINVENTARIO.Set("CODINVENTARIO", Codinventario);
                        GITEMINVENTARIO.Set("CODEMPRESA", AppLib.Context.Empresa);
                        GITEMINVENTARIO.Set("CODFILIAL", inventario.Rows[0]["CODFILIAL"]);                        
                        GITEMINVENTARIO.Set("CODLOCAL", inventario.Rows[0]["CODLOCAL"]);
                        GITEMINVENTARIO.Set("CODPRODUTO", row["VPRODUTO.CODPRODUTO"].ToString());
                        GITEMINVENTARIO.Set("NSEQITEMINVENTARIO", NSEQITEMINVENTARIO);
                        GITEMINVENTARIO.Set("SALDOINICIAL", SALDOINICIAL);
                        GITEMINVENTARIO.Set("CODUNIDCONTROLE", row["VPRODUTO.CODUNIDCONTROLE"].ToString());
                        GITEMINVENTARIO.Set("CODCCUSTO", inventario.Rows[0]["CODCCUSTO"]);
                        GITEMINVENTARIO.Set("USUARIOCRIACAO", AppLib.Context.Usuario);
                        GITEMINVENTARIO.Set("DATACRIACAO", Convert.ToDateTime(DateTime.Now));

                        GITEMINVENTARIO.Save();
                    }
                }
                //conn.Commit();
                return true;
            }
            catch (Exception ex)
            {
                //conn.Rollback();
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("Nenhum item selecionado para a inclusão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (Salvar() == true)
                {
                    this.Dispose();
                }
            }           
        }
    }
}
