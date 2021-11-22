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
    public partial class FrmCentroTrabalho : Form
    {
        
        public bool edita = false;
        public string codCentroTrabalho = string.Empty;
        private string mask;
        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;

        public FrmCentroTrabalho()
        {
            InitializeComponent();
        }

        public FrmCentroTrabalho(ref NewLookup lookup)
        {
            InitializeComponent();

            this.codCentroTrabalho = lookup.txtcodigo.Text;
            this.edita = true;

            this.lookup = lookup;

        }

        private void FrmCentroTrabalho_Load(object sender, EventArgs e)
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
            lookupcentrocusto.mensagemErrorProvider = "";
            lookupcustodireto.mensagemErrorProvider = "";
            lookupcustoindireto.mensagemErrorProvider = "";

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                errorProvider.SetError(txtCodigo, "Favor preencher o Código");
                verifica = false;
            }
            else
            {
                if (this.edita == false)
                {
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM PCENTROTRABALHO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCTRABALHO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text });
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

            //if (string.IsNullOrEmpty(lookupcentrocusto.ValorCodigoInterno))
            //{
            //    lookupcentrocusto.mensagemErrorProvider = "Favor preencher o Centro de Custo";
            //    verifica = false;
            //}
            //else
            //{
            //    lookupcentrocusto.mensagemErrorProvider = "";
            //}

            //Global gl = new Global();
            //if (gl.VerificaParametroString("CUSTOFORMA") == "1") //MANUAL
            //{
            //    if (string.IsNullOrEmpty(lookupcustodireto.txtconteudo.Text))
            //    {
            //        lookupcustodireto.mensagemErrorProvider = "Favor preencher o custo direto";
            //        verifica = false;
            //    }
            //    else
            //    {
            //        lookupcustodireto.mensagemErrorProvider = "";
            //    }

            //    if (string.IsNullOrEmpty(lookupcustoindireto.txtconteudo.Text))
            //    {
            //        lookupcustoindireto.mensagemErrorProvider = "Favor preencher o custo indireto";
            //        verifica = false;
            //    }
            //    else
            //    {
            //        lookupcustoindireto.mensagemErrorProvider = "";
            //    }
            //}

            return verifica;
        }

        private void carregaObj(DataTable dt)
        {
            txtCodigo.Text = dt.Rows[0]["CODCTRABALHO"].ToString();
            txtDescricao.Text = dt.Rows[0]["DESCCTRABALHO"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            chkexterno.Checked = Convert.ToBoolean(dt.Rows[0]["EXTERNO"]);

            txtFatorAlocacao.Text = dt.Rows[0]["FATORDIAALOCACAO"].ToString();
            
            lookupcentrocusto.txtcodigo.Text = dt.Rows[0]["CODCCUSTO"].ToString();
            lookupcentrocusto.CarregaDescricao();

            lookupcustodireto.txtcodigo.Text = dt.Rows[0]["CODQUERYCUSTO"].ToString();
            if (!string.IsNullOrEmpty(lookupcustodireto.txtcodigo.Text))
            {
                lookupcustodireto.CarregaDescricao();
            }

            lookupcustoindireto.txtcodigo.Text = dt.Rows[0]["CODQUERYGGF"].ToString();
            if (!string.IsNullOrEmpty(lookupcustoindireto.txtcodigo.Text))
            {
                lookupcustoindireto.CarregaDescricao();
            }

            txtCodigo.Enabled = false;

            Global gl = new Global();
            gl.EnableTab(tabControl2.TabPages["tabCusto"], true);
            gl.EnableTab(tabControl2.TabPages["tabCalendario"], true);

            string where = "WHERE PCENTROTRABALHOCALENDARIO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND PCENTROTRABALHOCALENDARIO.CODFILIAL = '" + AppLib.Context.Filial + "' AND PCENTROTRABALHOCALENDARIO.CODCTRABALHO = '" + dt.Rows[0]["CODCTRABALHO"].ToString() + "'";
            carregaCalendario(where);

            where = "WHERE PCENTROTRABALHOCUSTO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND PCENTROTRABALHOCUSTO.CODFILIAL = '" + AppLib.Context.Filial + "' AND PCENTROTRABALHOCUSTO.CODCTRABALHO = '" + dt.Rows[0]["CODCTRABALHO"].ToString() + "'";
            carregaCusto(where);

            VerificaParametro_CUSTOFORMA();
            if (VerificaParametroAolocacao() == true)
            {
                txtFatorAlocacao.Enabled = true;
            }
            else
            {
                txtFatorAlocacao.Enabled = false;
            }

            this.edita = true;
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCENTROTRABALHO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCTRABALHO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codCentroTrabalho });
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
                        dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCENTROTRABALHO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCTRABALHO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codCentroTrabalho });
                        if (dt.Rows.Count > 0)
                        {
                            carregaObj(dt);
                        }
                        break;

                    case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                        if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCENTROTRABALHO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND " + lookup.CampoCodigoInterno + " = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookup.ValorCodigoInterno });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        else
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCENTROTRABALHO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCTRABALHO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codCentroTrabalho });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        break;
                }
            }
        }

        public void VerificaParametro_CUSTOFORMA()
        {
            Global gl = new Global();
            string _CUSTOFORMA = gl.VerificaParametroString("CUSTOFORMA");

            if (_CUSTOFORMA == "1") //MANUAL
            {
                lookupcustodireto.Edita(true);
                lookupcustoindireto.Edita(true);
                btnNovoCusto.Enabled = true;
            }
            else if (_CUSTOFORMA == "2") //CALCULADO
            {
                lookupcustodireto.Edita(false);
                lookupcustoindireto.Edita(false);
                btnNovoCusto.Enabled = false;
            }
            else
            {
                MessageBox.Show("Parâmetro não definido, contate o administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LimpaGrid_Calendario()
        {
            string tabela = "PCENTROTRABALHOCALENDARIO";
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

                gridCalendario.DataSource = null;
                gridViewCalendario.Columns.Clear();
                gridCalendario.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewCalendario.Columns.Count; i++)
                {
                    gridViewCalendario.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewCalendario.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewCalendario.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LimpaGrid_Custo()
        {
            string tabela = "PCENTROTRABALHOCUSTO";
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

                gridCusto.DataSource = null;
                gridViewCusto.Columns.Clear();
                gridCusto.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewCusto.Columns.Count; i++)
                {
                    gridViewCusto.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewCusto.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewCusto.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void carregaCalendario(string where)
        {
            string tabela = "PCENTROTRABALHOCALENDARIO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();

            try
            {
                string sql = new PS.Glb.Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);

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

                gridCalendario.DataSource = null;
                gridViewCalendario.Columns.Clear();
                gridCalendario.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewCalendario.Columns.Count; i++)
                {
                    gridViewCalendario.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewCalendario.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewCalendario.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void carregaCusto(string where)
        {
            string tabela = "PCENTROTRABALHOCUSTO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();

            try
            {
                string sql = new PS.Glb.Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);

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

                gridCusto.DataSource = null;
                gridViewCusto.Columns.Clear();
                gridCusto.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewCusto.Columns.Count; i++)
                {
                    gridViewCusto.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewCusto.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewCusto.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool VerificaParametroAolocacao()
        {
            //DEFINIR PARAMETRO E MONTAR FUNCAO
            //DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM TABELA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCALEND = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text });
            //if (dt.Rows.Count > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return false;
        }

        private bool Salvar()
        {
            bool _salvar = false;

            if (validacao() == true)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PCENTROTRABALHO");
                conn.BeginTransaction();

                try
                {
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODFILIAL", AppLib.Context.Filial);
                    v.Set("CODCTRABALHO", txtCodigo.Text);
                    v.Set("DESCCTRABALHO", txtDescricao.Text);

                    if (string.IsNullOrEmpty(lookupcentrocusto.ValorCodigoInterno))
                    {
                        v.Set("CODCCUSTO", null);
                    }
                    else
                    {
                        v.Set("CODCCUSTO", lookupcentrocusto.ValorCodigoInterno.ToString());
                    }

                    v.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                    v.Set("EXTERNO", chkexterno.Checked == true ? 1 : 0);
                    
                    //IMPLEMENTAR FATORDIAALOCACAO
                    if (VerificaParametroAolocacao() == true)
                    {
                        v.Set("FATORDIAALOCACAO", Convert.ToDecimal(txtFatorAlocacao.Text)); 
                    }
                    
                    v.Set("FATORDIAALOCACAO", 0);

                    if (string.IsNullOrEmpty(lookupcustodireto.ValorCodigoInterno))
                    {
                        v.Set("CODQUERYCUSTO", null);
                    }
                    else
                    {
                        v.Set("CODQUERYCUSTO", lookupcustodireto.ValorCodigoInterno.ToString());
                    }

                    if (string.IsNullOrEmpty(lookupcustoindireto.ValorCodigoInterno))
                    {
                        v.Set("CODQUERYGGF", null);
                    }
                    else
                    {
                        v.Set("CODQUERYGGF", lookupcustoindireto.ValorCodigoInterno.ToString());
                    }

                    v.Save();
                    conn.Commit();

                    codCentroTrabalho = txtCodigo.Text;

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
                    codCentroTrabalho = txtCodigo.Text;
                    carregaCampos();
                    
                }
                else
                {

                    switch (lookup.CampoCodigo_Igual_a)
                    {
                        case NewLookup.CampoCodigoIguala.CampoCodigoBD:
                            codCentroTrabalho = txtCodigo.Text;
                            carregaCampos();

                            lookup.txtcodigo.Text = txtCodigo.Text;
                            lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                            lookup.ValorCodigoInterno = txtCodigo.Text;

                            this.Dispose();
                            break;
                        case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                            if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                            {
                                codCentroTrabalho = txtCodigo.Text;
                                carregaCampos();

                                lookup.txtcodigo.Text = txtCodigo.Text.ToUpper();
                                lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                                lookup.ValorCodigoInterno = txtCodigo.Text.ToUpper();

                                this.Dispose();
                            }
                            else
                            {
                                codCentroTrabalho = txtCodigo.Text;
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
            txtFatorAlocacao.Text = "0";
            chkAtivo.Checked = false;
            chkexterno.Checked = false;

            lookupcentrocusto.Clear();

            lookupcustodireto.Clear();
            lookupcustoindireto.Clear();

            txtCodigo.Enabled = true;
            txtCodigo.Focus();

            LimpaGrid_Calendario();
            LimpaGrid_Custo();

            Global gl = new Global();
            gl.EnableTab(tabControl2.TabPages["tabCusto"], false);
            gl.EnableTab(tabControl2.TabPages["tabCalendario"], false);

            VerificaParametro_CUSTOFORMA();
            if (VerificaParametroAolocacao()== true)
            {
                txtFatorAlocacao.Enabled = true;
            }
            else
            {
                txtFatorAlocacao.Enabled = false;
            }

        }

        private bool validaExcluir(ref string vinculo, string codigo)
        {
            bool verifica = true;

            try
            {
                DataTable dtRoteiro = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCTRABALHO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigo });
                if (dtRoteiro.Rows.Count > 0)
                {
                    vinculo = vinculo + (string.IsNullOrEmpty(vinculo) ? "Roteiro" : ", Roteiro");
                    verifica = false;
                }

                DataTable dtOperacao = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM POPERACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCTRABALHO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigo });
                if (dtOperacao.Rows.Count > 0)
                {
                    vinculo = vinculo + (string.IsNullOrEmpty(vinculo) ? "Operação" : ", Operação");
                    verifica = false;
                }

                DataTable dtGrupoRecurso = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM PGRUPORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCTRABALHO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigo });
                if (dtGrupoRecurso.Rows.Count > 0)
                {
                    vinculo = vinculo + (string.IsNullOrEmpty(vinculo) ? "Grupo de Recurso" : " e Grupo de Recurso");
                    verifica = false;
                }
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
                if (validaExcluir(ref vinculo, codCentroTrabalho) == true)
                {
                    if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                        conn.BeginTransaction();
                        try
                        {
                            conn.ExecTransaction("DELETE FROM PCENTROTRABALHOCUSTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCTRABALHO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codCentroTrabalho });
                            conn.ExecTransaction("DELETE FROM PCENTROTRABALHOCALENDARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCTRABALHO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codCentroTrabalho });
                            conn.ExecTransaction("DELETE FROM PCENTROTRABALHO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCTRABALHO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codCentroTrabalho });

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
                    MessageBox.Show("Este Centro de Trabalho esta vinculado a um(a) " + vinculo + " e não pode ser excluído.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Excluir Centro de Trabalho, contate o Administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmCentroTrabalhoCalendario frm = new FrmCentroTrabalhoCalendario(" WHERE PCALENDARIO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PCALENDARIO.CODFILIAL = " + AppLib.Context.Filial + " AND PCALENDARIO.ATIVO = 1");
            frm.CodCTrabalho = txtCodigo.Text;
            frm.ShowDialog();
            carregaCampos();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            FrmCentroTrabalhoCusto frm = new FrmCentroTrabalhoCusto();
            frm.CodCTrabalho = txtCodigo.Text;
            frm.ShowDialog();
            carregaCampos();
        }

       private void AtualizaCusto()
        {
            if (gridViewCusto.SelectedRowsCount > 0)
            {
                FrmCentroTrabalhoCusto frm = new FrmCentroTrabalhoCusto();
                DataRow row1 = gridViewCusto.GetDataRow(Convert.ToInt32(gridViewCusto.GetSelectedRows().GetValue(0).ToString()));
                frm.MesRef = Convert.ToInt16(row1["PCENTROTRABALHOCUSTO.MESCUSTOHRCTRABALHO"].ToString());
                frm.AnoRef = Convert.ToInt16(row1["PCENTROTRABALHOCUSTO.ANOCUSTOHRCTRABALHO"].ToString());
                frm.CodCTrabalho = txtCodigo.Text;
                frm.Update = true;
                frm.edita = true;
                frm.ShowDialog();
                carregaCampos();
            }
        }
        private void gridCusto_DoubleClick(object sender, EventArgs e)
        {
            AtualizaCusto();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            AtualizaCusto();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row1 = gridViewCalendario.GetDataRow(Convert.ToInt32(gridViewCalendario.GetSelectedRows().GetValue(0).ToString()));
                string codigo = row1["PCENTROTRABALHOCALENDARIO.CODCALEND"].ToString();

                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                conn.BeginTransaction();
                try
                {
                    conn.ExecTransaction("DELETE FROM PCENTROTRABALHOCALENDARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCTRABALHO = ? and CODCALEND = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codCentroTrabalho ,codigo });

                    conn.Commit();
                    MessageBox.Show("Cadastro Excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    conn.Rollback();
                    throw;
                }

                string where = "WHERE PCENTROTRABALHOCALENDARIO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND PCENTROTRABALHOCALENDARIO.CODFILIAL = '" + AppLib.Context.Filial + "' AND PCENTROTRABALHOCALENDARIO.CODCTRABALHO = '" + codCentroTrabalho + "'";
                carregaCalendario(where);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Excluir Calendário, contate o Administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row1 = gridViewCusto.GetDataRow(Convert.ToInt32(gridViewCusto.GetSelectedRows().GetValue(0).ToString()));
                string ano = row1["PCENTROTRABALHOCUSTO.ANOCUSTOHRCTRABALHO"].ToString();
                string mes = row1["PCENTROTRABALHOCUSTO.MESCUSTOHRCTRABALHO"].ToString();

                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                conn.BeginTransaction();
                try
                {
                    conn.ExecTransaction("DELETE FROM PCENTROTRABALHOCUSTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCTRABALHO = ? AND ANOCUSTOHRCTRABALHO = ? AND MESCUSTOHRCTRABALHO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codCentroTrabalho, ano, mes});

                    conn.Commit();
                    MessageBox.Show("Cadastro Excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    conn.Rollback();
                    throw;
                }

                string where = "WHERE PCENTROTRABALHOCUSTO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND PCENTROTRABALHOCUSTO.CODFILIAL = '" + AppLib.Context.Filial + "' AND PCENTROTRABALHOCUSTO.CODCTRABALHO = '" + codCentroTrabalho  + "'";
                carregaCusto(where);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Excluir Custo, contate o Administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
