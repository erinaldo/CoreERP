using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Visao.Globais
{
    public partial class frmVisaoSituacao : Form
    {
        string tabela = "GSITUACAO";
        public bool Visualizacao = true;

        public frmVisaoSituacao(Form frmprin)
        {
            InitializeComponent();
            this.MdiParent = (Form)frmprin;
            carregaGrid();
        }

        public void carregaGrid()
        {

            //Verifica se existe registro na tabela GVISAOUSUARIO
            int colunas = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(COLUNA) FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1", new object[] { tabela, AppLib.Context.Usuario }));
            if (colunas == 0)
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });
                DataTable db = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = ?", new object[] { tabela });
                for (int i = 0; i < db.Rows.Count; i++)
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO GVISAOUSUARIO (VISAO, CODUSUARIO, SEQUENCIA, COLUNA, LARGURA, VISIVEL) VALUES (?, ?, ?, ?, ?, ?)", new object[] { tabela, AppLib.Context.Usuario, i, db.Rows[i]["COLUMN_NAME"].ToString(), 100, 1 });
                }
            }
            DataTable dt = new DataTable();
            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
            string sql = string.Empty;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(sql))
                {
                    sql = "SELECT " + dt.Rows[i]["COLUNA"].ToString();
                }
                else
                {
                    sql = sql + ", " + dt.Rows[i]["COLUNA"].ToString();
                }
            }
            sql = sql + " FROM " + tabela;

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

            //if (gridView1.Columns["CODSTATUS"] != null)
            //{
            //    carregaImagem();
            //}


            DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
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

        private void carregaImagem()
        {
            //CODCLASSIFICACAO
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            images.AddImage(Image.FromFile(@"Icones\Financeiro\Seta-verde.png"));
            images.AddImage(Image.FromFile(@"Icones\Financeiro\Seta-vermelha.png"));
            imageCombo.SmallImages = images;
            imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Pagar", 0, 1));
            imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Receber", 1, 0));
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns["TIPOPAGREC"].ColumnEdit = imageCombo;

            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageStatus = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection imagesStatus = new DevExpress.Utils.ImageCollection();
            imagesStatus.AddImage(Image.FromFile(@"Icones\Financeiro\Oper_Aberto.png"));
            imagesStatus.AddImage(Image.FromFile(@"Icones\Financeiro\Oper_Faturado.png"));
            imagesStatus.AddImage(Image.FromFile(@"Icones\Financeiro\Oper_Cancelado.png"));
            imageStatus.SmallImages = imagesStatus;
            imageStatus.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Aberto", 0, 0));
            imageStatus.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Baixado", 1, 1));
            imageStatus.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cancelado", 2, 2));
            imageStatus.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns["CODSTATUS"].ColumnEdit = imageStatus;

            //
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            carregaGrid();
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            carregaGrid();
        }

        private void salvarLayout()
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
                carregaGrid();
            }

        }

        private void btnSalvarLayout_Click(object sender, EventArgs e)
        {
            salvarLayout();
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
            Cadastros.Globais.frmSituacao frm = new Cadastros.Globais.frmSituacao();
            //frm.edita = false;
            frm.ShowDialog();
            carregaGrid();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            //{
            //    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
            //    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GSTATUS SET SITUACAO = 'I' WHERE CODSTATUS = ? AND CODEMPRESA = ? AND TABELA = ?", new object[] { row1["CODSTATUS"].ToString(), row1["CODEMPRESA"].ToString(), row1["TABELA"].ToString() });
            //}
            carregaGrid();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            edita();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (Visualizacao == false)
            {
                return;
            }

            edita();
        }

        private void edita()
        {
            Cadastros.Globais.frmSituacao frm = new Cadastros.Globais.frmSituacao();
            frm.edita = true;
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            frm.codEmpresa = Convert.ToInt32(row1["CODEMPRESA"].ToString());
            frm.CodSituacao = row1["CODSITUACAO"].ToString();
            frm.Tabela = row1["TABELA"].ToString();
            frm.ShowDialog();
            carregaGrid();
        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {

        }
    }
}
