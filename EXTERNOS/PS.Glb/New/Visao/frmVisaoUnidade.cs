using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ITGProducao.Controles;
using PS.Glb.Class;

namespace PS.Glb.New.Visao
{
    public partial class frmVisaoUnidade : Form
    {
        public string Condicao { get; set; }
        string tabela = "AUNIDADE";

        private NewLookup lookup;

        public frmVisaoUnidade()
        {
            InitializeComponent();
        }

        public frmVisaoUnidade(ref NewLookup lookup)
        {
            InitializeComponent();
            Condicao = lookup.whereVisao;
            AtualizaGrid();
            this.lookup = lookup;
        }

        private void AtualizaGrid()
        {
            try
            {
                string sql = String.Format(@"select * from AUNIDADE {0}", Condicao);
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
                catch
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AbrirFiltro()
        {
            PS.Glb.New.Filtro.frmFiltroUnidade frm = new Filtro.frmFiltroUnidade();
            frm.ShowDialog();

            if (String.IsNullOrWhiteSpace(frm.condicao))
            {
                Condicao = frm.condicao;
                AtualizaGrid();
            }
        }

        private void frmVisaoUnidade_Load(object sender, EventArgs e)
        {
            AtualizaGrid();
            getAcesso("btnUnidade");
        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            AbrirFiltro();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                PS.Glb.New.Cadastros.frmCadastroUnidadeCliente frm = new Cadastros.frmCadastroUnidadeCliente(null, null);
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
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                PS.Glb.New.Cadastros.frmCadastroUnidadeCliente frm = new Cadastros.frmCadastroUnidadeCliente(row1["IDUNIDADE"].ToString(), row1["CODCLIFOR"].ToString());
                frm.ShowDialog();
                AtualizaGrid();
            }

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show("Deseja exluir o registro selecionado?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                    New.Classes.Unidade unidade = new Classes.Unidade();

                    if (unidade.Excluir(Convert.ToInt32(row1["IDUNIDADE"])))
                    {
                        XtraMessageBox.Show("Registro excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        AtualizaGrid();
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            btnEditar_Click(sender, e);
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            AtualizaGrid();
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

        private void getAcesso(string CodMenu)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO, EXCLUSAO, CONSULTA, INCLUSAO FROM GPERMISSAOMENU WHERE CODMENU = ? AND CODPERFIL = ?", new object[] { CodMenu, AppLib.Context.Perfil });
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dt.Rows[i]["EDICAO"]) == false)
                    {
                        btnEditar.Enabled = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["CONSULTA"]) == true)
                    {
                        btnEditar.Enabled = true;
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["EXCLUSAO"]) == false)
                    {
                        btnExcluir.Enabled = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["INCLUSAO"]) == false)
                    {
                        btnNovo.Enabled = false;
                    }
                }
            }
            else
            {
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                btnNovo.Enabled = false;
            }
        }

        private void btnSalvarLayout_Click(object sender, EventArgs e)
        {
            SalvarLayout();
        }
    }
}
