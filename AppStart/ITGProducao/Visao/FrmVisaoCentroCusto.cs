using ITGProducao.Controles;
using ITGProducao.Filtros;
using ITGProducao.Formularios;
using PS.Glb.New;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITGProducao.Visao
{
    public partial class FrmVisaoCentroCusto : Form
    {
        string tabela = "GCENTROCUSTO";
        string relacionamento = string.Empty;
        string query = string.Empty;
        List<string> tabelasFilhas = new List<string>();

        //Variaveis para usar quando a tela abre para consulta.
        public bool consulta = false;
        public string codCentroCusto;
        public string nome;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public FrmVisaoCentroCusto(string _query, Form frmprin)
        {
            InitializeComponent();
            this.MdiParent = (Form)frmprin;
            query = _query;

            carregaGrid(query);
            //splitContainer1.SplitterDistance = 30;
        }

        public FrmVisaoCentroCusto(string _where)
        {
            InitializeComponent();
            query = _where;
            carregaGrid(query);
        }

        public FrmVisaoCentroCusto(ref NewLookup lookup)
        {
            InitializeComponent();

            query = lookup.whereVisao;
            carregaGrid(query);

            this.lookup = lookup;
        }

        public void carregaGrid(string where)
        {
            try
            {
                //string sql = new PS.Glb.Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);
                string sql = "SELECT GCENTROCUSTO.CODEMPRESA AS 'CODEMPRESA', GCENTROCUSTO.CODCCUSTO AS 'CODCCUSTO', GCENTROCUSTO.NOME AS 'NOME', GCENTROCUSTO.DESCRICAO AS 'DESCRICAO', GCENTROCUSTO.ATIVO AS 'ATIVO', GCENTROCUSTO.PERMITELANCAMENTO AS 'PERMITELANCAMENTO' FROM GCENTROCUSTO   WHERE  CODEMPRESA  =  1  AND ATIVO  =  '1'   AND PERMITELANCAMENTO  =  '1'";
                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;

                }

                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                FrmCentroCusto frm = new FrmCentroCusto();
                frm.edita = false;
                //frm.MdiParent = this.MdiParent;
                frm.ShowDialog();
                carregaGrid(query);
            }
            else
            {
                FrmCentroCusto frm = new FrmCentroCusto(ref this.lookup);
                frm.edita = false;
                frm.ShowDialog();
                this.Dispose();
            }
        }

        private void Atualizar()
        {
            if (this.lookup == null)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    FrmCentroCusto frm = new FrmCentroCusto();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    frm.codCentroCusto = row1[tabela + ".CODCCUSTO"].ToString();
                    frm.edita = true;
                    frm.ShowDialog();
                    atualizaColuna(row1);
                }
            }
            else
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                lookup.txtconteudo.Text = row1["NOME"].ToString().ToUpper();

                switch (lookup.CampoCodigo_Igual_a)
                {
                    case NewLookup.CampoCodigoIguala.CampoCodigoBD:
                        lookup.txtcodigo.Text = row1["CODCCUSTO"].ToString();
                        lookup.ValorCodigoInterno = row1["CODCCUSTO"].ToString();
                        break;

                    case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                        if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                        {
                            lookup.txtcodigo.Text = row1[tabela + "." + lookup.CampoCodigoInterno].ToString();
                            lookup.ValorCodigoInterno = row1[tabela + "." + lookup.CampoCodigoInterno].ToString();
                        }
                        else
                        {
                            lookup.txtcodigo.Text = row1["CODCCUSTO"].ToString();
                            lookup.ValorCodigoInterno = row1["CODCCUSTO"].ToString();
                        }
                        break;
                }

                this.Dispose();
            }
        }

        private void atualizaColuna(DataRow dr)
        {
            carregaGrid(query);
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            Atualizar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Atualizar();
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
                carregaGrid(query);
            }
        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            FrmFiltroCentroCusto frm = new FrmFiltroCentroCusto();
            frm.aberto = true;
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.condicao))
            {
                query = frm.condicao;
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //Implementar
        }
    }
}
