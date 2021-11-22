using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New
{
    public partial class frmSelecaoColunas : Form
    {
        private string tabela;
        DataTable dt = new DataTable();
        public frmSelecaoColunas(string _tabela)
        {
            InitializeComponent();
            tabela = _tabela;
            carregaGrid();
        }
        private void carregaGrid()
        {
            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
GVISAOUSUARIO.COLUNA, 
GDICIONARIO.DESCRICAO, 
GVISAOUSUARIO.VISIVEL,
GVISAOUSUARIO.LARGURA, 
GVISAOUSUARIO.FIXO 
FROM 
GVISAOUSUARIO 
LEFT OUTER JOIN GDICIONARIO ON GVISAOUSUARIO.VISAO = GDICIONARIO.TABELA AND GVISAOUSUARIO.COLUNA = GDICIONARIO.COLUNA 
WHERE 
VISAO = ?
AND CODUSUARIO = ?
ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
            gridControl1.DataSource = dt;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("Processando");

            DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ? AND FIXO = 1", new object[] { AppLib.Context.Usuario, tabela });
            dtFixo.PrimaryKey = new DataColumn[] { dtFixo.Columns["COLUNA"] };

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");

                GVISAOUSUARIO.Set("VISAO", tabela);
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView1.GetDataRow(i)["COLUNA"]);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("VISIVEL", gridView1.GetDataRow(i)["VISIVEL"].Equals(false) ? 0 : 1);
                DataRow result = dtFixo.Rows.Find(new object[] { gridView1.GetDataRow(i)["COLUNA"] });
                if (result != null)
                {
                    GVISAOUSUARIO.Set("VISIVEL", 1);
                }

                GVISAOUSUARIO.Set("LARGURA", gridView1.GetDataRow(i)["LARGURA"]);

                GVISAOUSUARIO.Update();
            }
            splashScreenManager1.CloseWaitForm();

            this.Dispose();
            GC.Collect();

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            //coluna, descrição, visivel
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            DataRow row2 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()) - 1);
            object COLUNA1 = row1["COLUNA"];
            object DESCRICAO1 = row1["DESCRICAO"];
            object VISIVEL1 = row1["VISIVEL"];
            object LARGURA1 = row1["LARGURA"];

            object COLUNA2 = row2["COLUNA"];
            object DESCRICAO2 = row2["DESCRICAO"];
            object VISIVEL2 = row2["VISIVEL"];
            object LARGURA2 = row2["LARGURA"];

            row1["COLUNA"] = COLUNA2;
            row1["DESCRICAO"] = DESCRICAO2;
            row1["VISIVEL"] = VISIVEL2;
            row1["LARGURA"] = LARGURA2;

            row2["COLUNA"] = COLUNA1;
            row2["DESCRICAO"] = DESCRICAO1;
            row2["VISIVEL"] = VISIVEL1;
            row2["LARGURA"] = LARGURA1;

            gridView1.FocusedRowHandle = Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()) - 1;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            //coluna, descrição, visivel
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            DataRow row2 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()) + 1);
            object COLUNA1 = row1["COLUNA"];
            object DESCRICAO1 = row1["DESCRICAO"];
            object VISIVEL1 = row1["VISIVEL"];
            object LARGURA1 = row1["LARGURA"];

            object COLUNA2 = row2["COLUNA"];
            object DESCRICAO2 = row2["DESCRICAO"];
            object VISIVEL2 = row2["VISIVEL"];
            object LARGURA2 = row2["LARGURA"];

            row1["COLUNA"] = COLUNA2;
            row1["DESCRICAO"] = DESCRICAO2;
            row1["VISIVEL"] = VISIVEL2;
            row1["LARGURA"] = LARGURA2;

            row2["COLUNA"] = COLUNA1;
            row2["DESCRICAO"] = DESCRICAO1;
            row2["VISIVEL"] = VISIVEL1;
            row2["LARGURA"] = LARGURA1;

            gridView1.FocusedRowHandle = Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()) + 1;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            //coluna, descrição, visivel
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            DataRow row2 = gridView1.GetDataRow(0);
            object COLUNA1 = row1["COLUNA"];
            object DESCRICAO1 = row1["DESCRICAO"];
            object VISIVEL1 = row1["VISIVEL"];
            object LARGURA1 = row1["LARGURA"];

            object COLUNA2 = row2["COLUNA"];
            object DESCRICAO2 = row2["DESCRICAO"];
            object VISIVEL2 = row2["VISIVEL"];
            object LARGURA2 = row2["LARGURA"];

            row1["COLUNA"] = COLUNA2;
            row1["DESCRICAO"] = DESCRICAO2;
            row1["VISIVEL"] = VISIVEL2;
            row1["LARGURA"] = LARGURA2;

            row2["COLUNA"] = COLUNA1;
            row2["DESCRICAO"] = DESCRICAO1;
            row2["VISIVEL"] = VISIVEL1;
            row2["LARGURA"] = LARGURA1;

            gridView1.FocusedRowHandle = 0;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            //coluna, descrição, visivel
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            DataRow row2 = gridView1.GetDataRow(gridView1.RowCount - 1);
            object COLUNA1 = row1["COLUNA"];
            object DESCRICAO1 = row1["DESCRICAO"];
            object VISIVEL1 = row1["VISIVEL"];
            object LARGURA1 = row1["LARGURA"];

            object COLUNA2 = row2["COLUNA"];
            object DESCRICAO2 = row2["DESCRICAO"];
            object VISIVEL2 = row2["VISIVEL"];
            object LARGURA2 = row2["LARGURA"];

            row1["COLUNA"] = COLUNA2;
            row1["DESCRICAO"] = DESCRICAO2;
            row1["VISIVEL"] = VISIVEL2;
            row1["LARGURA"] = LARGURA2;

            row2["COLUNA"] = COLUNA1;
            row2["DESCRICAO"] = DESCRICAO1;
            row2["VISIVEL"] = VISIVEL1;
            row2["LARGURA"] = LARGURA1;

            gridView1.FocusedRowHandle = gridView1.RowCount - 1;
        }

        private void btnUncheckedAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                gridView1.GetDataRow(i)["VISIVEL"] = false;
            }
        }

        private void btnChechedAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                gridView1.GetDataRow(i)["VISIVEL"] = true;
            }
        }

        private void frmSelecaoColunas_Load(object sender, EventArgs e)
        {

        }
    }
}
