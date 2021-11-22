using ITGProducao.Controles;
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
    public partial class frmVisaoRegraCFOP : Form
    {
        public Form pai = null;
        public bool consulta = false;
        string tabela = "VREGRAVARCFOP";
        string query = string.Empty;
        public string codMenu = string.Empty;
        private bool permiteEditar = true;

        List<string> tabelasFilhas = new List<string>();

        //Variaveis para usar quando a tela abre para consulta.
        public string NCM;
        public string Descricao;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmVisaoRegraCFOP(string _query)
        {
            InitializeComponent();
            tabelasFilhas.Clear();
            query = _query;
            CarregaGrid(query);
            //getAcesso(codMenu);
        }

        public frmVisaoRegraCFOP(ref NewLookup lookup)
        {
            InitializeComponent();
            query = lookup.whereVisao;
            CarregaGrid(query);
            this.lookup = lookup;
        }

        private void frmVisaoRegraCFOP_Load(object sender, EventArgs e)
        {
            btnExcluir.Enabled = false;
            btnFiltros.Enabled = false;
            toolStripDropDownButton2.Enabled = false;
            toolStripDropDownButton3.Enabled = false;
            toolStripDropDownButton4.Enabled = false;
        }

        public void CarregaGrid(string where)
        {
            try
            {
                List<string> TabelasFilhas = new List<string>();

                tabelasFilhas.Add("VIBPTAX");

                string sql = string.Empty;
                string relac = "INNER JOIN VIBPTAX ON VIBPTAX.CODIGO = VREGRAVARCFOP.NCM";

                sql = new Class.Utilidades().getVisao(tabela, relac, tabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA IN (?, ?)", new object[] { tabela, "VIBPTAX" });
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

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregaGrid(query);
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            CarregaGrid(query);
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
                CarregaGrid(query);
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

        private void atualizaColuna(DataRow dr)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VREGRAVARCFOP WHERE NCM = ? AND CODEMPRESA = ?", new object[] { dr["VREGRAVARCFOP.NCM"], AppLib.Context.Empresa });

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
                    PS.Glb.New.Cadastros.frmCadastroRegraCFOP Regra = new Cadastros.frmCadastroRegraCFOP();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    Regra.Codigo = row1["VREGRAVARCFOP.NCM"].ToString();
                    Regra.CodFilial = Convert.ToInt32(row1["VREGRAVARCFOP.CODFILIAL"]);
                    Regra.UfDestino = row1["VREGRAVARCFOP.UFDESTINO"].ToString();
                    Regra.edita = true;
                    Regra.ShowDialog();
                    atualizaColuna(row1);
                    CarregaGrid(query);
                }
            }
            else
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                lookup.txtconteudo.Text = row1[tabela + ".DESCRICAO"].ToString().ToUpper();
                lookup.txtcodigo.Text = row1[tabela + ".NCM"].ToString();
                lookup.ValorCodigoInterno = row1[tabela + ".NCM"].ToString();
                this.Dispose();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                PS.Glb.New.Cadastros.frmCadastroRegraCFOP Regra = new Cadastros.frmCadastroRegraCFOP();
                Regra.edita = false;
                Regra.Codigo = NCM;
                Regra.ShowDialog();
                CarregaGrid(query);
            }
            else
            {
                PS.Glb.New.Cadastros.frmCadastroLocalEstoque local = new Cadastros.frmCadastroLocalEstoque(ref this.lookup);
                local.edita = false;
                local.ShowDialog();
                this.Dispose();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    PS.Glb.New.Cadastros.frmCadastroRegraCFOP Regra = new Cadastros.frmCadastroRegraCFOP();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    Regra.Codigo = row1["VREGRAVARCFOP.NCM"].ToString();
                    Regra.CodFilial = Convert.ToInt32(row1["VREGRAVARCFOP.CODFILIAL"]);
                    Regra.UfDestino = row1["VREGRAVARCFOP.UFDESTINO"].ToString();
                    Regra.edita = true;
                    Regra.ShowDialog();
                    atualizaColuna(row1);
                    CarregaGrid(query);
                }
                else
                {
                    PS.Glb.New.Cadastros.frmCadastroRegraCFOP Regra = new Cadastros.frmCadastroRegraCFOP();
                    Regra.edita = false;
                }
            }
            else
            {
                gridControl1_DoubleClick(sender, e);
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (consulta == true)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    NCM = row1["VREGRAVARCFOP.NCM"].ToString();
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
    }
}
