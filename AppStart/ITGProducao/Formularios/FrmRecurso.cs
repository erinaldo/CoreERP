using ITGProducao.Class;
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
    public partial class FrmRecurso : Form
    {
        public bool edita = false;
        public string codRecurso = string.Empty;
        private string mask;
        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;

        public FrmRecurso()
        {
            InitializeComponent();

            lookupgruporecurso.txtcodigo.Leave += new System.EventHandler(lookupgruporecurso_txtcodigo_Leave);
            lookupgruporecurso.btnprocurar.Click += new System.EventHandler(lookupgruporecurso_txtcodigo_Leave);
            lookupgruporecurso.txtconteudo.Click += new System.EventHandler(lookupgruporecurso_txtcodigo_Leave);
        }

        public FrmRecurso(ref NewLookup lookup)
        {
            InitializeComponent();

            lookupgruporecurso.txtcodigo.Leave += new System.EventHandler(lookupgruporecurso_txtcodigo_Leave);
            lookupgruporecurso.btnprocurar.Click += new System.EventHandler(lookupgruporecurso_txtcodigo_Leave);
            lookupgruporecurso.txtconteudo.Click += new System.EventHandler(lookupgruporecurso_txtcodigo_Leave);

            this.codRecurso = lookup.txtcodigo.Text;
            this.edita = true;

            this.lookup = lookup;
        }

        private void FrmRecurso_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
            }
            else
            {
                btnNovo.PerformClick();
            }
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
                errorProvider.SetError(txtCodigo, "Favor preencher o Código");
                verifica = false;
            }
            else
            {
                if (this.edita == false)
                {
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PRECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODRECURSO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text });
                    if (dt.Rows.Count > 0)
                    {
                        errorProvider.SetError(txtCodigo, "Código Já Cadastrado");
                        verifica = false;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtDescricao.Text))
            {
                errorProvider.SetError(txtDescricao, "Favor preencher a Descrição");
                verifica = false;
            }

            if (string.IsNullOrEmpty(lookupgruporecurso.ValorCodigoInterno))
            {
                lookupgruporecurso.mensagemErrorProvider = "Selecione o Grupo de Recurso";
                verifica = false;
            }
            else
            {
                lookupgruporecurso.mensagemErrorProvider = "";
            }

            return verifica;
        }

        private void lookupgruporecurso_txtcodigo_Leave(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(lookupgruporecurso.ValorCodigoInterno))
            {
                //bool validacao = false;
                //valida_combos(lookupgruporecurso.ValorCodigoInterno.ToString(), ref validacao);
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PGRUPORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODGRUPORECURSO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupgruporecurso.ValorCodigoInterno.ToString() });
                if (dt.Rows.Count > 0){
                    if (dt.Rows[0]["TIPORECURSO"].ToString() == "EQ")
                    {
                        txtModelo.Enabled = true;
                        txtNumSerie.Enabled = true;
                    }
                    else
                    {
                        txtModelo.Enabled = false;
                        txtNumSerie.Enabled = false;

                        txtModelo.Text = "";
                        txtNumSerie.Text = "";
                    }
                }
            }
        }

        //public void valida_combos(string CODGRUPORECURSO,ref bool validacao)
        //{
        //    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PGRUPORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODGRUPORECURSO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, CODGRUPORECURSO });
        //    if (dt.Rows.Count > 0)
        //    {
        //        if (validacao == true)
        //        {
        //            if (Convert.ToBoolean(dt.Rows[0]["CTRABALHOFIXO"]) == true)
        //            {
        //                if (string.IsNullOrEmpty(lookupcentrotrabalho.ValorCodigoInterno))
        //                {
        //                    lookupcentrotrabalho.mensagemErrorProvider = "Selecione o Centro de Trabalho";
        //                    validacao = false;
        //                }
        //                else
        //                {
        //                    lookupcentrotrabalho.mensagemErrorProvider = "";
        //                }
        //            }

        //            if (dt.Rows[0]["TIPORECURSO"].ToString() == "EQ")
        //            {
        //                if (string.IsNullOrEmpty(txtModelo.Text))
        //                {
        //                    errorProvider.SetError(txtModelo, "Favor preencher o Modelo");
        //                    validacao = false;
        //                }

        //                if (string.IsNullOrEmpty(txtNumSerie.Text))
        //                {
        //                    errorProvider.SetError(txtNumSerie, "Favor preencher a Número de Série");
        //                    validacao = false;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            lookupcentrotrabalho.txtcodigo.Text = dt.Rows[0]["CODCTRABALHO"].ToString();
        //            lookupcentrotrabalho.CarregaDescricao();

        //            if (Convert.ToBoolean(dt.Rows[0]["CTRABALHOFIXO"]) == true)
        //            {
        //                lookupcentrotrabalho.Edita(false);
        //            }
        //            else
        //            {
        //                lookupcentrotrabalho.Edita(true);
        //            }

        //            if (dt.Rows[0]["TIPORECURSO"].ToString() == "EQ")
        //            {
        //                txtModelo.Enabled = true;
        //                txtNumSerie.Enabled = true;
        //            }
        //            else
        //            {
        //                txtModelo.Enabled = false;
        //                txtNumSerie.Enabled = false;

        //                txtModelo.Text = "";
        //                txtNumSerie.Text = "";
        //            }
        //        }
        //    }
        //}

        private void carregaObj(DataTable dt)
        {
            txtCodigo.Text = dt.Rows[0]["CODRECURSO"].ToString();
            txtDescricao.Text = dt.Rows[0]["DESCRECURSO"].ToString();

            lookupgruporecurso.txtcodigo.Text = dt.Rows[0]["CODGRUPORECURSO"].ToString(); ;
            lookupgruporecurso.CarregaDescricao();

            //bool validacao = false;
            ////valida_combos(dt.Rows[0]["CODCTRABALHO"].ToString(), ref validacao);
            //lookupcentrotrabalho.txtcodigo.Text = dt.Rows[0]["CODCTRABALHO"].ToString(); ;
            //lookupcentrotrabalho.CarregaDescricao();
            //valida_combos(dt.Rows[0]["CODGRUPORECURSO"].ToString(), ref validacao);

            lookupmarca.txtcodigo.Text = dt.Rows[0]["CODMARCA"].ToString(); ;
            lookupmarca.CarregaDescricao();

            txtModelo.Text = dt.Rows[0]["EQMODELO"].ToString();
            txtNumSerie.Text = dt.Rows[0]["EQNUMSERIE"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);

            txtCodigo.Enabled = false;
            lookupgruporecurso.Edita(false);

            Global gl = new Global();
            gl.EnableTab(tabControl2.TabPages["tabAlocacao"], true);

            //IMPLEMENTAR WHERE
            //string where = "WHERE PGRUPORECURSOCUSTO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND PGRUPORECURSOCUSTO.CODFILIAL = '" + AppLib.Context.Filial + "' AND PGRUPORECURSOCUSTO.CODGRUPORECURSO = '" + txtCodigo.Text + "'";
            //carregaAlocacao(where);
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PRECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODRECURSO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codRecurso });
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
                        dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PRECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODRECURSO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codRecurso });
                        if (dt.Rows.Count > 0)
                        {
                            carregaObj(dt);
                        }
                        break;

                    case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                        if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PRECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND " + lookup.CampoCodigoInterno + " = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookup.ValorCodigoInterno });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        else
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PRECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODRECURSO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codRecurso });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        break;
                }
            }

        }

        void LimpaGrid_Alocacao()
        {
            //string tabela = "PALOCACAO"; //CRIAR TABELA CORRETA
            //string relacionamento = string.Empty;
            //string query = string.Empty;
            //List<string> tabelasFilhas = new List<string>();
            //try
            //{
            //    string sql = new PS.Glb.Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, "").Replace("SELECT", "SELECT TOP 0 ");

            //    if (string.IsNullOrEmpty(sql))
            //    {
            //        MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //    string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
            //    if (!string.IsNullOrEmpty(filtroUsuario))
            //    {

            //        sql = sql + filtroUsuario;
            //    }

            //    gridCusto.DataSource = null;
            //    gridViewCusto.Columns.Clear();
            //    gridCusto.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

            //    DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
            //    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
            //    for (int i = 0; i < gridViewCusto.Columns.Count; i++)
            //    {
            //        gridViewCusto.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
            //        dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
            //        DataRow result = dic.Rows.Find(new object[] { gridViewCusto.Columns[i].FieldName.ToString() });
            //        if (result != null)
            //        {
            //            gridViewCusto.Columns[i].Caption = result["DESCRICAO"].ToString();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        public void carregaCusto(string where)
        {
            //string tabela = "PALOCACAO"; //CRIAR TABELA CORRETA
            //string relacionamento = string.Empty;
            //string query = string.Empty;
            //List<string> tabelasFilhas = new List<string>();

            //try
            //{
            //    string sql = new PS.Glb.Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);

            //    if (string.IsNullOrEmpty(sql))
            //    {
            //        MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //    string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
            //    if (!string.IsNullOrEmpty(filtroUsuario))
            //    {

            //        sql = sql + filtroUsuario;

            //    }

            //    gridCusto.DataSource = null;
            //    gridViewCusto.Columns.Clear();
            //    gridCusto.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

            //    DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
            //    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
            //    for (int i = 0; i < gridViewCusto.Columns.Count; i++)
            //    {
            //        gridViewCusto.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
            //        dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
            //        DataRow result = dic.Rows.Find(new object[] { gridViewCusto.Columns[i].FieldName.ToString() });
            //        if (result != null)
            //        {
            //            gridViewCusto.Columns[i].Caption = result["DESCRICAO"].ToString();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private bool Salvar()
        {
            bool _salvar = false;

            if (validacao() == true)
            {

                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PRECURSO");
                conn.BeginTransaction();

                try
                {
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODFILIAL", AppLib.Context.Filial);
                    v.Set("CODRECURSO", txtCodigo.Text);
                    v.Set("DESCRECURSO", txtDescricao.Text);

                    v.Set("CODGRUPORECURSO", lookupgruporecurso.ValorCodigoInterno.ToString());

                    v.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);

                    //v.Set("CODCTRABALHO", (string.IsNullOrEmpty(lookupcentrotrabalho.ValorCodigoInterno) ? null : lookupcentrotrabalho.ValorCodigoInterno.ToString()));

                    v.Set("CODMARCA", (string.IsNullOrEmpty(lookupmarca.ValorCodigoInterno) ? null : lookupmarca.ValorCodigoInterno.ToString()));

                    v.Set("EQMODELO", txtModelo.Text);
                    v.Set("EQNUMSERIE", txtNumSerie.Text);

                    v.Save();
                    conn.Commit();

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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                if (this.lookup == null)
                {
                    MessageBox.Show("Cadastro salvo com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                codRecurso = txtCodigo.Text;
                carregaCampos();
                }
                else
                {
                    switch (lookup.CampoCodigo_Igual_a)
                    {
                        case NewLookup.CampoCodigoIguala.CampoCodigoBD:
                            codRecurso = txtCodigo.Text;
                            carregaCampos();

                            lookup.txtcodigo.Text = txtCodigo.Text;
                            lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                            lookup.ValorCodigoInterno = txtCodigo.Text;

                            this.Dispose();
                            break;
                        case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                            if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                            {
                                codRecurso = txtCodigo.Text;
                                carregaCampos();

                                lookup.txtcodigo.Text = txtCodigo.Text;
                                lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                                lookup.ValorCodigoInterno = txtCodigo.Text;

                                this.Dispose();
                            }
                            else
                            {
                                codRecurso = txtCodigo.Text;
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

            lookupmarca.Clear();
            lookupgruporecurso.Clear();
            //lookupcentrotrabalho.Clear();

            txtModelo.Text = string.Empty;
            txtNumSerie.Text = string.Empty;

            txtModelo.Enabled = false;
            txtNumSerie.Enabled = false;

            chkAtivo.Checked = false;

            txtCodigo.Enabled = true;
            
            //lookupcentrotrabalho.Edita(true);
            lookupgruporecurso.Edita(true);

            Global gl = new Global();
            gl.EnableTab(tabControl2.TabPages["tabAlocacao"], false);


            LimpaGrid_Alocacao();
        }

        private bool validaExcluir(ref string vinculo, string codigo)
        {
            bool verifica = true;

            try
            {
                //IMPLEMENTAR VALIDACAO COM CADASTRO DE ORDEM DE PRODUCAO
                //DataTable dtRoteiro = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM PORDEMPRODUCAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODRECURSO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigo });
                //if (dtRoteiro.Rows.Count > 0)
                //{
                //    vinculo = vinculo + (string.IsNullOrEmpty(vinculo) ? "Ordem de Produção" : ", Ordem de Produção");
                //    verifica = false;
                //}
            }
            catch (Exception)
            {
                throw;
            }

            return verifica;

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                string vinculo = "";
                if (validaExcluir(ref vinculo, codRecurso) == true)
                {
                    if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                        conn.BeginTransaction();
                        try
                        {
                            conn.ExecTransaction("DELETE FROM PRECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODRECURSO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codRecurso });

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
                else
                {
                    MessageBox.Show("Este Recurso esta vinculado a um(a) " + vinculo + " e não pode ser excluído.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Excluir Recurso, contate o Administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grid_DoubleClick(object sender, EventArgs e)
        {
            //IMPLEMENTAR
        }

        private void btnNovoCusto_Click(object sender, EventArgs e)
        {
            //IMPLEMENTAR
        }
    }
}
