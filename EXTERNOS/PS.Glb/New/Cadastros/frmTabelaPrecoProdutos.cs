using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Cadastros
{
    public partial class frmTabelaPrecoProdutos : Form
    {
        string tabela = "VPRODUTO";
        string relacionamento = " LEFT OUTER JOIN VSALDOESTOQUE ON VPRODUTO.CODPRODUTO = VSALDOESTOQUE.CODPRODUTO AND VPRODUTO.CODEMPRESA = VSALDOESTOQUE.CODEMPRESA";
        string query = string.Empty;
        public string Codproduto = string.Empty;
        public string CodCliente = string.Empty;
        public string CodFilial = string.Empty;
        public string idTabela = string.Empty;
        public bool UsaFilial;
        List<string> tabelasFilhas = new List<string>();

        public frmTabelaPrecoProdutos()
        {
            InitializeComponent();
            tabelasFilhas.Add("VSALDOESTOQUE");
            carregaGrid(query);
        }

        public void carregaGrid(string where)
        {
            try
            {
                string sql = string.Empty;


                sql = new Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, "WHERE VPRODUTO.ULTIMONIVEL = 1 AND VPRODUTO.ATIVO = 1");


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
                    FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
            }
        }

        private bool Salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VCLIFORTABPRECOITEM = new AppLib.ORM.Jit(conn, "VCLIFORTABPRECOITEM");
            conn.BeginTransaction();

            try
            {
                using (PS.Glb.New.Cadastros.frmCadastroTabelaPreco tabela = new frmCadastroTabelaPreco())
                {

                    if (gridView1.SelectedRowsCount > 0)
                    {
                        for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                            VCLIFORTABPRECOITEM.Set("IDTABELA", idTabela);

                            VCLIFORTABPRECOITEM.Set("CODEMPRESA", AppLib.Context.Empresa);

                            if (UsaFilial == true)
                            {
                                VCLIFORTABPRECOITEM.Set("CODFILIAL", CodFilial);
                            }
                            else
                            {
                                CodFilial = AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT TOP 1 CODFILIAL FROM GFILIAL WHERE CODEMPRESA = ? ", new object[] { AppLib.Context.Empresa }).ToString();
                                VCLIFORTABPRECOITEM.Set("CODFILIAL", CodFilial);
                            }

                            VCLIFORTABPRECOITEM.Set("CODPRODUTO", row["VPRODUTO.CODPRODUTO"]);
                            VCLIFORTABPRECOITEM.Set("CODMOEDA", "R$");
                            VCLIFORTABPRECOITEM.Set("PRECOUNITARIO", 0);
                            VCLIFORTABPRECOITEM.Set("USUARIOCRIACAO", AppLib.Context.Usuario);
                            VCLIFORTABPRECOITEM.Set("DATACRIACAO", Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                            VCLIFORTABPRECOITEM.Save();
                        }
                    }
                    conn.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                conn.Rollback();
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
            }
        }
    }
}
