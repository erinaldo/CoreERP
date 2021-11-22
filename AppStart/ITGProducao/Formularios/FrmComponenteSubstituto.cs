using ITGProducao.Controles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITGProducao.Formularios
{
    public partial class FrmComponenteSubstituto : Form
    {
        public bool edita = false;
        public string codProduto = string.Empty;
        private string mask;
        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;

        public FrmComponenteSubstituto(ref NewLookup lookup)
        {
            InitializeComponent();

            this.codProduto = lookup.txtcodigo.Text;
            this.edita = true;

            this.lookup = lookup;
        }

        public FrmComponenteSubstituto()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                if (this.lookup == null)
                {
                    MessageBox.Show("Cadastro salvo com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    codProduto = lookupproduto.ValorCodigoInterno;
                    carregaCampos();
                }
                else
                {
                    codProduto = lookupproduto.ValorCodigoInterno;
                    carregaCampos();

                    lookup.txtcodigo.Text = lookupproduto.txtcodigo.Text;
                    lookup.txtconteudo.Text = lookupproduto.txtconteudo.Text.ToUpper();
                    lookup.ValorCodigoInterno = lookupproduto.txtcodigo.Text;

                    this.Dispose();
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (Salvar() == true)
                {
                    this.Dispose();
                }
            }
            else
            {
                btnSalvar.PerformClick();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FrmComponenteSubstituto_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
            }
            else
            {
                LimpaGrid();
            }

            lookupproduto.Focus();
        }

        private bool validacao()
        {
            bool verifica = true;

            errorProvider.Clear();
            lookupproduto.mensagemErrorProvider = "";
            newLookupprodutosubstituto.mensagemErrorProvider = "";

            if (string.IsNullOrEmpty(newLookupprodutosubstituto.ValorCodigoInterno))
            {
                newLookupprodutosubstituto.mensagemErrorProvider = "Favor selecionar o produto substituto";
                verifica = false;
            }
            else
            {
                if (this.edita == false)
                {
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCOMPONENTESUB WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODPRODUTO = ? AND CODPRODUTOSUB = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupproduto.ValorCodigoInterno,newLookupprodutosubstituto.ValorCodigoInterno});
                    if (dt.Rows.Count > 0)
                    {
                        newLookupprodutosubstituto.mensagemErrorProvider = "Produto Já Cadastrado";
                        verifica = false;
                    }
                }
            }

            return verifica;
        }

        void CarregaGrid(string codproduto)
        {
            string tabela = "PCOMPONENTESUB";
            string relacionamento = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = new PS.Glb.Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, " WHERE CODPRODUTO = '" + codproduto + "'");

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

                gridComponentes.DataSource = null;
                gridView1.Columns.Clear();
                gridComponentes.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?)", new object[] { tabela });
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

        void LimpaGrid()
        {
            string tabela = "PCOMPONENTESUB";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = new PS.Glb.Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, "").Replace("SELECT", "SELECT TOP 0 ");

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

                gridComponentes.DataSource = null;
                gridView1.Columns.Clear();
                gridComponentes.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?)", new object[] { tabela });
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

        private void carregaObj(DataTable dt)
        {
            lookupproduto.txtcodigo.Text = dt.Rows[0]["CODPRODUTO"].ToString();
            lookupproduto.CarregaDescricao();
            newLookupprodutosubstituto.txtcodigo.Text = dt.Rows[0]["CODPRODUTOSUB"].ToString();
            newLookupprodutosubstituto.CarregaDescricao();
            txtPrioridade.Text = dt.Rows[0]["PRIORIDADE"].ToString();
            txtFator.Text = dt.Rows[0]["FATOR"].ToString();

            lookupproduto.Edita(false);
        }

        private void carregaCampos()
        {
            if (!string.IsNullOrEmpty(codProduto))
            {
                lookupproduto.txtcodigo.Text = codProduto;
                lookupproduto.CarregaDescricao();

                lookupproduto.Edita(false);

                CarregaGrid(codProduto);
            }
        }

        private bool Salvar()
        {
            bool _salvar = false;
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PCOMPONENTESUB");
                conn.BeginTransaction();

                if (validacao() == true)
                {
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODFILIAL", AppLib.Context.Filial);

                    v.Set("CODPRODUTO", lookupproduto.ValorCodigoInterno);
                    v.Set("CODPRODUTOSUB", newLookupprodutosubstituto.ValorCodigoInterno);

                    v.Set("PRIORIDADE", Convert.ToInt16(txtPrioridade.Text));
                    v.Set("FATOR", Convert.ToDecimal(txtFator.Text.Replace(".", ",")));

                    v.Save();
                    conn.Commit();

                    codProduto = lookupproduto.ValorCodigoInterno;

                    carregaCampos();

                    this.edita = true;

                    _salvar = true;
                }
                else
                {
                    _salvar = false;
                }

                return _salvar;
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();
            try
            {
                conn.ExecTransaction("DELETE FROM PCOMPONENTESUB WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODMARCA = ? AND CODPRODUTO = ? AND CODPRODUTOSUB = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codProduto, newLookupprodutosubstituto.ValorCodigoInterno });
                conn.Commit();
                MessageBox.Show("Cadastro excluído com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            catch (Exception ex)
            {
                conn.Rollback();
                MessageBox.Show("Erro ao excluir componente substituto, contate o Administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            newLookupprodutosubstituto.mensagemErrorProvider = "";
            txtFator.Text = "";
            txtPrioridade.Text = "";

            LimpaGrid();
        }

        private void gridComponentes_DoubleClick(object sender, EventArgs e)
        {
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCOMPONENTESUB WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODPRODUTO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, row1["PCOMPONENTESUB.CODPRODUTO".ToString()] });
            if (dt.Rows.Count > 0)
            {
                carregaObj(dt);
            }
        }
    }
}
