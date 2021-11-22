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
    public partial class FrmMarca : Form
    {
        public bool edita = false;
        public string codMarca = string.Empty;
        private string mask;
        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;

        public FrmMarca()
        {
            InitializeComponent();
        }

        public FrmMarca(ref NewLookup lookup)
        {
            InitializeComponent();

            this.codMarca = lookup.txtcodigo.Text;
            this.edita = true;

            this.lookup = lookup;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                if (this.lookup == null)
                {
                    MessageBox.Show("Cadastro salvo com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    codMarca = txtCodigo.Text;
                    carregaCampos();
                }
                else
                {
                    switch (lookup.CampoCodigo_Igual_a)
                    {
                        case NewLookup.CampoCodigoIguala.CampoCodigoBD:
                            codMarca = txtCodigo.Text;
                            carregaCampos();

                            lookup.txtcodigo.Text = txtCodigo.Text;
                            lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                            lookup.ValorCodigoInterno = txtCodigo.Text;

                            this.Dispose();
                            break;
                        case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                            if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                            {
                                codMarca = txtCodigo.Text;
                                carregaCampos();

                                lookup.txtcodigo.Text = txtDescricao.Text.ToUpper();
                                lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                                lookup.ValorCodigoInterno = txtDescricao.Text.ToUpper();

                                this.Dispose();
                            }
                            else
                            {
                                codMarca = txtCodigo.Text;
                                carregaCampos();

                                lookup.txtcodigo.Text = txtCodigo.Text;
                                lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                                lookup.ValorCodigoInterno = txtCodigo.Text;

                                this.Dispose();
                            }
                            break;
                    }

                }
            }
        }

        private void FrmMarca_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
            }
            else
            {
                LimpaGrid_Recursos();
            }

            txtCodigo.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool validacao()
        {
            bool verifica = true;

            errorProvider.Clear();

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                errorProvider.SetError(txtCodigo, "Favor preencher o código da marca");
                verifica = false;
            }
            else
            {
                if (this.edita == false)
                {
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PMARCA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODMARCA = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text });
                    if (dt.Rows.Count > 0)
                    {
                        errorProvider.SetError(txtCodigo, "Código Já Cadastrado");
                        verifica = false;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtDescricao.Text))
            {
                errorProvider.SetError(txtDescricao, "Favor preencher a descrição");
                verifica = false;
            }

            return verifica;
        }

        private void carregaObj(DataTable dt)
        {
            txtCodigo.Text = dt.Rows[0]["CODMARCA"].ToString();
            txtDescricao.Text = dt.Rows[0]["DESCMARCA"].ToString();

            txtCodigo.Enabled = false;

            string where = "WHERE PRECURSO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND PRECURSO.CODFILIAL = '" + AppLib.Context.Filial + "' AND PRECURSO.CODMARCA = '" + dt.Rows[0]["CODMARCA"].ToString() + "'";
            carregaRecursos(where);
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PMARCA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODMARCA = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codMarca });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                switch (lookup.CampoCodigo_Igual_a)
                {
                    case NewLookup.CampoCodigoIguala.CampoCodigoBD:
                        dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PMARCA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODMARCA = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codMarca });
                        if (dt.Rows.Count > 0)
                        {
                            carregaObj(dt);
                        }
                        break;

                    case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                        if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PMARCA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND " + lookup.CampoCodigoInterno + " = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookup.ValorCodigoInterno });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        else
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PMARCA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODMARCA = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codMarca });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        break;
                }
            }
        }

        void LimpaGrid_Recursos()
        {
            string tabela = "PRECURSO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                //string sql = new PS.Glb.Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, "").Replace("SELECT", "SELECT TOP 0 ");

                string sql = "SELECT TOP 0 PRECURSO.CODRECURSO AS 'PRECURSO.CODRECURSO', PGRUPORECURSO.TIPORECURSO AS 'PGRUPORECURSO.TIPORECURSO', PRECURSO.DESCRECURSO AS 'PRECURSO.DESCRECURSO' FROM PRECURSO JOIN PGRUPORECURSO ON PRECURSO.CODEMPRESA = PGRUPORECURSO.CODEMPRESA AND PRECURSO.CODFILIAL = PGRUPORECURSO.CODFILIAL AND PRECURSO.CODGRUPORECURSO = PGRUPORECURSO.CODGRUPORECURSO WHERE PRECURSO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PRECURSO.CODFILIAL = " + AppLib.Context.Filial + " ";

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

                gridRecurso.DataSource = null;
                gridView1.Columns.Clear();
                gridRecurso.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "PGRUPORECURSO" });
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
        public void carregaRecursos(string where)
        {
            string tabela = "PRECURSO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();


            try
            {
                string sql = "SELECT PRECURSO.CODRECURSO AS 'PRECURSO.CODRECURSO', PGRUPORECURSO.TIPORECURSO AS 'PGRUPORECURSO.TIPORECURSO', PRECURSO.DESCRECURSO AS 'PRECURSO.DESCRECURSO' FROM PRECURSO JOIN PGRUPORECURSO ON PRECURSO.CODEMPRESA = PGRUPORECURSO.CODEMPRESA AND PRECURSO.CODFILIAL = PGRUPORECURSO.CODFILIAL AND PRECURSO.CODGRUPORECURSO = PGRUPORECURSO.CODGRUPORECURSO " + where;

                //string sql = new PS.Glb.Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);

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

                gridRecurso.DataSource = null;
                gridView1.Columns.Clear();
                gridRecurso.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "PGRUPORECURSO" });
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

        private bool Existe_Marca(string validaano = "")
        {

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PMARCA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODMARCA = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text });

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Salvar()
        {
            bool _salvar = false;

            if (validacao() == true)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PMARCA");
                conn.BeginTransaction();
                try
                {
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODFILIAL", AppLib.Context.Filial);
                    v.Set("CODMARCA", txtCodigo.Text);
                    v.Set("DESCMARCA", txtDescricao.Text);

                    v.Save();
                    conn.Commit();

                    codMarca = txtCodigo.Text;

                    carregaCampos();
                    this.edita = true;

                    _salvar = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Rollback();
                    _salvar = false;
                }
            }
            else
            {
                _salvar = false;
            }

            return _salvar;
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

        private void btnNovo_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            txtCodigo.Text = string.Empty;
            txtDescricao.Text = string.Empty;

            txtCodigo.Enabled = true;

            txtCodigo.Focus();

            LimpaGrid_Recursos();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                 DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM PRECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODMARCA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codMarca });
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Esta Marca esta vinculado a um Recurso e não pode ser excluída.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                        conn.BeginTransaction();
                        try
                        {
                            conn.ExecTransaction("DELETE FROM PMARCA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODMARCA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codMarca });
                            conn.Commit();
                            MessageBox.Show("Cadastro Excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Dispose();
                        }
                        catch (Exception)
                        {
                            conn.Rollback();
                            throw;
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao Excluir Marca, contate o Administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }
    }
}
