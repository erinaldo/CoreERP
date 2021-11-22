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
    public partial class frmVisaoFatura : Form
    {
        public Form pai = null;
        public bool consulta = false;
        string tabela = "FLANCA ";
        string query = string.Empty;

        List<string> tabelasFilhas = new List<string>();

        public frmVisaoFatura(string _query, Form frmprin)
        {
            InitializeComponent();
            this.MdiParent = frmprin;
            tabelasFilhas.Clear();
            query = _query;
            CarregaGrid("WHERE FLANCA.CODEMPRESA = 1 AND FLANCA.CODFILIAL = 1 AND FLANCA.CODTIPDOC IN('FATR', 'FATP') ");
        }

        public void CarregaGrid(string where)
        {
            try
            {
                string sql = string.Empty;

                tabelasFilhas.Add("VCLIFOR");
                sql = new Class.Utilidades().getVisao(tabela, " INNER JOIN VCLIFOR ON FLANCA.CODCLIFOR = VCLIFOR.CODCLIFOR AND FLANCA.CODEMPRESA = VCLIFOR.CODEMPRESA", tabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                sql = sql.Replace("FLANCA.CODSTATUS AS 'FLANCA.CODSTATUS'", @"CASE
 WHEN ((CONVERT(DATE,FLANCA.DATAVENCIMENTO) < CONVERT(DATE, GETDATE())) AND FLANCA.CODSTATUS = 0) THEN '3' 
 WHEN ((CONVERT(DATE,FLANCA.DATAVENCIMENTO) = CONVERT(DATE, GETDATE())) AND FLANCA.CODSTATUS = 0)  THEN '4'  
 ELSE FLANCA.CODSTATUS 
 END AS 'FLANCA.CODSTATUS'");

                gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                carregaImagemPagRec();

                if (gridView1.Columns["FLANCA.CODSTATUS"] != null)
                {
                    carregaImagemStatus();
                }

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

        private void carregaImagemStatus()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSTATUS FROM GSTATUS WHERE TABELA = 'FLANCA'");
            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
                imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[i]["DESCRICAO"].ToString(), dtImagem.Rows[i]["CODSTATUS"].ToString(), i));
            }
            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns["FLANCA.CODSTATUS"].ColumnEdit = imageCombo;
        }

        private void carregaImagemPagRec()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSITUACAO FROM GSITUACAO WHERE TABELA = 'FLANCA'");
            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
            }
            imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[0]["DESCRICAO"].ToString(), 1, 0));
            imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[1]["DESCRICAO"].ToString(), 0, 1));

            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns["FLANCA.TIPOPAGREC"].ColumnEdit = imageCombo;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregaGrid("WHERE FLANCA.CODEMPRESA = 1 AND FLANCA.CODFILIAL = 1 AND FLANCA.CODTIPDOC IN('FATR', 'FATP')");
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            CarregaGrid("WHERE FLANCA.CODEMPRESA = 1 AND FLANCA.CODFILIAL = 1 AND FLANCA.CODTIPDOC IN('FATR', 'FATP')");
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
                CarregaGrid("WHERE FLANCA.CODEMPRESA = 1 AND FLANCA.CODFILIAL = 1 AND FLANCA.CODTIPDOC IN('FATR', 'FATP')");
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
    }
}
