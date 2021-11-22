using ITGProducao.Class;
using ITGProducao.Controles;
using PS.Glb.New;
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
    public partial class FrmGrupoRecurso : Form
    {
        public bool edita = false;
        public string codGrupoRecurso = string.Empty;
        private string mask;
        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;

        public FrmGrupoRecurso()
        {
            InitializeComponent();

            lookupcentrotrabalho.txtcodigo.Leave += new System.EventHandler(lookupcentrotrabalho_txtcodigo_Leave);
            lookupcentrotrabalho.btnprocurar.Click += new System.EventHandler(lookupcentrotrabalho_txtcodigo_Leave);
            lookupcentrotrabalho.txtconteudo.Click += new System.EventHandler(lookupcentrotrabalho_txtcodigo_Leave);
            //splitContainer1.SplitterDistance = 30;
        }

        public FrmGrupoRecurso(ref NewLookup lookup)
        {
            InitializeComponent();

            lookupcentrotrabalho.txtcodigo.EditValueChanged += new System.EventHandler(lookupcentrotrabalho_txtcodigo_Leave);
            lookupcentrotrabalho.btnprocurar.Click += new System.EventHandler(lookupcentrotrabalho_txtcodigo_Leave);
            lookupcentrotrabalho.txtconteudo.Click += new System.EventHandler(lookupcentrotrabalho_txtcodigo_Leave);

            this.codGrupoRecurso = lookup.txtcodigo.Text;
            this.edita = true;

            this.lookup = lookup;
        }

        void CarregaCombos()
        {
            List<PS.Lib.ComboBoxItem> listTipoRecurso = new List<PS.Lib.ComboBoxItem>();

            listTipoRecurso.Add(new PS.Lib.ComboBoxItem());
            listTipoRecurso[0].ValueMember = "0";
            listTipoRecurso[0].DisplayMember = "";

            listTipoRecurso.Add(new PS.Lib.ComboBoxItem());
            listTipoRecurso[1].ValueMember = "EQ";
            listTipoRecurso[1].DisplayMember = "EQ - Equipamento";

            listTipoRecurso.Add(new PS.Lib.ComboBoxItem());
            listTipoRecurso[2].ValueMember = "MO";
            listTipoRecurso[2].DisplayMember = "MO - Mão de Obra";

            listTipoRecurso.Add(new PS.Lib.ComboBoxItem());
            listTipoRecurso[3].ValueMember = "FE";
            listTipoRecurso[3].DisplayMember = "FE - Ferramenta";

            cmbTipoRecurso.DataSource = listTipoRecurso;
            cmbTipoRecurso.DisplayMember = "DisplayMember";
            cmbTipoRecurso.ValueMember = "ValueMember";

            cmbTipoRecurso.SelectedIndex = -1;
        }

        private void FrmGrupoRecurso_Load(object sender, EventArgs e)
        {
            CarregaCombos();

            if (edita == true)
            {
                carregaCampos();
            }
            else
            {
                btnNovo.PerformClick();
                //Global gl = new Global();
                //gl.EnableTab(tabControl2.TabPages["tabCusto"], false);

                //LimpaGrid_Custo();
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
            lookupcentrotrabalho.mensagemErrorProvider = "";
            lookupcustodireto.mensagemErrorProvider = "";
            lookupcustoindireto.mensagemErrorProvider = "";
            lookupcentrocusto.mensagemErrorProvider = "";


            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                errorProvider.SetError(txtCodigo, "Favor preencher o Código");
                verifica = false;
            }
            else
            {
                if (this.edita == false)
                {
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PGRUPORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODGRUPORECURSO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text });
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
            //    lookupcentrocusto.mensagemErrorProvider = "Selecione o Centro de Custo";
            //    verifica = false;
            //}
            //else
            //{
            //    lookupcentrocusto.mensagemErrorProvider = "";
            //}

            if ((cmbTipoRecurso.SelectedIndex == -1) || (cmbTipoRecurso.SelectedIndex == 0))
            {
                errorProvider.SetError(cmbTipoRecurso, "Selecione o Tipo de Recurso");
                verifica = false;
            }
            else
            {
                if (cmbTipoRecurso.SelectedValue == "EQ")
                {
                    chkCentroTrabalhoFixo.Checked = true;
                    chkCentroTrabalhoFixo.Enabled = false;

                    if (string.IsNullOrEmpty(lookupcentrotrabalho.ValorCodigoInterno))
                    {
                        lookupcentrotrabalho.mensagemErrorProvider = "Favor preencher o Centro de Trabalho";
                        verifica = false;
                    }
                    else
                    {
                        lookupcentrotrabalho.mensagemErrorProvider = "";
                    }
                }
                else
                {
                    chkCentroTrabalhoFixo.Enabled = true;
                }
            }

            if(chkCentroTrabalhoFixo.Checked == true)
            {
                
                if (string.IsNullOrEmpty(lookupcentrotrabalho.txtconteudo.Text))
                {
                    lookupcentrotrabalho.mensagemErrorProvider = "Favor preencher o Centro de Trabalho";
                    verifica = false;
                }
                else
                {
                    lookupcentrotrabalho.mensagemErrorProvider = "";
                }
            }

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

        private void lookupcentrotrabalho_txtcodigo_Leave(object sender, System.EventArgs e)
        {

            if (cmbTipoRecurso.SelectedValue == "")
            {
                chkCentroTrabalhoFixo.Enabled = false;
                chkCentroTrabalhoFixo.Checked = false;
            }
            else if (cmbTipoRecurso.SelectedValue == "EQ")
            {
                chkCentroTrabalhoFixo.Checked = true;
                chkCentroTrabalhoFixo.Enabled = false;
            }
            else
            {
                chkCentroTrabalhoFixo.Enabled = true;
            }
        }

   
        private void carregaObj(DataTable dt)
        {
            txtCodigo.Text = dt.Rows[0]["CODGRUPORECURSO"].ToString();
            txtDescricao.Text = dt.Rows[0]["DESCGRUPORECURSO"].ToString();

            cmbTipoRecurso.SelectedValue = dt.Rows[0]["TIPORECURSO"].ToString();

            lookupcentrocusto.txtcodigo.Text = dt.Rows[0]["CODCCUSTO"].ToString();
            lookupcentrocusto.CarregaDescricao();

            lookupcentrotrabalho.txtcodigo.Text = dt.Rows[0]["CODCTRABALHO"].ToString();
            lookupcentrotrabalho.CarregaDescricao();

            chkCentroTrabalhoFixo.Checked = Convert.ToBoolean(dt.Rows[0]["CTRABALHOFIXO"]);

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

            VerificaParametro_CUSTOGEQUIPAMENTO();
            VerificaParametro_CUSTOFORMA();

            txtCodigo.Enabled = false;

            Global gl = new Global();
            gl.EnableTab(tabControl2.TabPages["tabCusto"], true);

            string where = "WHERE PGRUPORECURSOCUSTO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND PGRUPORECURSOCUSTO.CODFILIAL = '" + AppLib.Context.Filial + "' AND PGRUPORECURSOCUSTO.CODGRUPORECURSO = '" + txtCodigo.Text + "'";
            carregaCusto(where);
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PGRUPORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODGRUPORECURSO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codGrupoRecurso });
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
                        dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PGRUPORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODGRUPORECURSO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codGrupoRecurso });
                        if (dt.Rows.Count > 0)
                        {
                            carregaObj(dt);
                        }
                        break;

                    case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                        if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PGRUPORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND " + lookup.CampoCodigoInterno + " = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookup.ValorCodigoInterno });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        else
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PGRUPORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODGRUPORECURSO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codGrupoRecurso });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        break;
                }
            }
        }

        void LimpaGrid_Custo()
        {
            string tabela = "PGRUPORECURSOCUSTO";
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

        public void carregaCusto(string where)
        {
            string tabela = "PGRUPORECURSOCUSTO";
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

        private bool Salvar()
        {
            bool _salvar = false;

            if (validacao() == true)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PGRUPORECURSO");
                conn.BeginTransaction();

                try
                {
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODFILIAL", AppLib.Context.Filial);
                    v.Set("CODGRUPORECURSO", txtCodigo.Text);
                    v.Set("DESCGRUPORECURSO", txtDescricao.Text);
                    v.Set("TIPORECURSO", cmbTipoRecurso.SelectedValue);

                    if (string.IsNullOrEmpty(lookupcentrocusto.ValorCodigoInterno))
                    {
                        v.Set("CODCCUSTO", null);
                    }
                    else
                    {
                        v.Set("CODCCUSTO", lookupcentrocusto.ValorCodigoInterno.ToString());
                    }

                    if (string.IsNullOrEmpty(lookupcentrotrabalho.ValorCodigoInterno))
                    {
                        v.Set("CODCTRABALHO", null);
                    }
                    else
                    {
                        v.Set("CODCTRABALHO", lookupcentrotrabalho.ValorCodigoInterno.ToString());
                    }
                    
                    v.Set("CTRABALHOFIXO", chkCentroTrabalhoFixo.Checked == true ? 1 : 0);


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

                    codGrupoRecurso = txtCodigo.Text;
                    carregaCampos();
                    this.edita = true;

                    v.Save();
                    conn.Commit();
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
                    codGrupoRecurso = txtCodigo.Text;
                    carregaCampos();
                }
                else
                {
                    switch (lookup.CampoCodigo_Igual_a)
                    {
                        case NewLookup.CampoCodigoIguala.CampoCodigoBD:
                            codGrupoRecurso = txtCodigo.Text;
                            carregaCampos();

                            lookup.txtcodigo.Text = txtCodigo.Text;
                            lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                            lookup.ValorCodigoInterno = txtCodigo.Text;

                            this.Dispose();
                            break;
                        case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                            if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                            {
                                codGrupoRecurso = txtCodigo.Text;
                                carregaCampos();

                                lookup.txtcodigo.Text = txtCodigo.Text;
                                lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                                lookup.ValorCodigoInterno = txtCodigo.Text;

                                this.Dispose();
                            }
                            else
                            {
                                codGrupoRecurso = txtCodigo.Text;
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

        private void btnNovo_Click_1(object sender, EventArgs e)
        {
            errorProvider.Clear();

            txtCodigo.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            chkCentroTrabalhoFixo.Checked = false;

            lookupcentrocusto.Clear();
            lookupcentrotrabalho.Clear();

            lookupcustodireto.Clear();
            lookupcustoindireto.Clear();

            cmbTipoRecurso.SelectedIndex = -1;

            txtCodigo.Enabled = true;

            txtCodigo.Focus();

            LimpaGrid_Custo();

            VerificaParametro_CUSTOGEQUIPAMENTO();
            VerificaParametro_CUSTOFORMA();

            Global gl = new Global();
            gl.EnableTab(tabControl2.TabPages["tabCusto"], false);
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

        public void VerificaParametro_CUSTOGEQUIPAMENTO()
        {
            Global gl = new Global();
            string _CUSTOGEQUIPAMENTO = gl.VerificaParametroString("CUSTOGEQUIPAMENTO");

            if (_CUSTOGEQUIPAMENTO == "1") //SIM
            {
                gl.EnableTab(tabControl2.TabPages["tabCusto"], true);
            }
            else if (_CUSTOGEQUIPAMENTO == "2") //NÃO
            {
                gl.EnableTab(tabControl2.TabPages["tabCusto"], false);
            }
            else
            {
                MessageBox.Show("Parâmetro não definido, contate o administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool validaExcluir(ref string vinculo, string codigo)
        {
            bool verifica = true;

            try
            {
                DataTable dtRoteiro = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM POPERACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND GRUPORECURSOEQ = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigo });
                if (dtRoteiro.Rows.Count > 0)
                {
                    vinculo = vinculo + (string.IsNullOrEmpty(vinculo) ? "Operação" : "");
                    verifica = false;
                }

                DataTable dtOperacao = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM POPERACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND GRUPORECURSOMO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigo });
                if (dtOperacao.Rows.Count > 0)
                {
                    vinculo = vinculo + (string.IsNullOrEmpty(vinculo) ? "Operação" : "");
                    verifica = false;
                }

                DataTable dtGrupoRecurso = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM POPERACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND GRUPORECURSOFE = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigo });
                if (dtGrupoRecurso.Rows.Count > 0)
                {
                    vinculo = vinculo + (string.IsNullOrEmpty(vinculo) ? "Operação" : "");
                    verifica = false;
                }

                DataTable dtRecurso = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM PRECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODGRUPORECURSO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigo });
                if (dtRecurso.Rows.Count > 0)
                {
                    vinculo = vinculo + (string.IsNullOrEmpty(vinculo) ? "Recurso" : " e Recurso");
                    verifica = false;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return verifica;

        }
        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            try
            {
                string vinculo = "";
                if (validaExcluir(ref vinculo, codGrupoRecurso) == true)
                {
                    if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                        conn.BeginTransaction();
                        try
                        {
                            conn.ExecTransaction("DELETE FROM PGRUPORECURSOCUSTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODGRUPORECURSO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codGrupoRecurso });
                            conn.ExecTransaction("DELETE FROM PGRUPORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODGRUPORECURSO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codGrupoRecurso });

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
                    MessageBox.Show("Este Grupo de Recurso esta vinculado a um(a) " + vinculo + " e não pode ser excluído.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Excluir Grupo de Recurso, contate o Administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            FrmGrupoRecursoCusto frm = new FrmGrupoRecursoCusto();
            frm.CodGrupoRecurso = txtCodigo.Text;
            frm.ShowDialog();
            carregaCampos();
        }

        private void gridCusto_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewCusto.SelectedRowsCount > 0)
            {
                FrmGrupoRecursoCusto frm = new FrmGrupoRecursoCusto();
                DataRow row1 = gridViewCusto.GetDataRow(Convert.ToInt32(gridViewCusto.GetSelectedRows().GetValue(0).ToString()));
                frm.MesRef = Convert.ToInt16(row1["PGRUPORECURSOCUSTO.MESCUSTOHRRECURSO"].ToString());
                frm.AnoRef = Convert.ToInt16(row1["PGRUPORECURSOCUSTO.ANOCUSTOHRRECURSO"].ToString());
                frm.CodGrupoRecurso = txtCodigo.Text;
                frm.edita = true;
                frm.Update = true;
                frm.ShowDialog();
                carregaCampos();
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (gridViewCusto.SelectedRowsCount > 0)
            {
                FrmGrupoRecursoCusto frm = new FrmGrupoRecursoCusto();
                DataRow row1 = gridViewCusto.GetDataRow(Convert.ToInt32(gridViewCusto.GetSelectedRows().GetValue(0).ToString()));
                frm.MesRef = Convert.ToInt16(row1["PGRUPORECURSOCUSTO.MESCUSTOHRRECURSO"].ToString());
                frm.AnoRef = Convert.ToInt16(row1["PGRUPORECURSOCUSTO.ANOCUSTOHRRECURSO"].ToString());
                frm.CodGrupoRecurso = txtCodigo.Text;
                frm.edita = true;
                frm.Update = true;
                frm.ShowDialog();
                carregaCampos();
            }
        }

        private void cmbTipoRecurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoRecurso.SelectedValue == "EQ")
            {
                chkCentroTrabalhoFixo.Checked = true;
                chkCentroTrabalhoFixo.Enabled = false;
            }
            else
            {
                chkCentroTrabalhoFixo.Enabled = true;
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow row1 = gridViewCusto.GetDataRow(Convert.ToInt32(gridViewCusto.GetSelectedRows().GetValue(0).ToString()));
                    string ano = row1["PGRUPORECURSOCUSTO.ANOCUSTOHRRECURSO"].ToString();
                    string mes = row1["PGRUPORECURSOCUSTO.MESCUSTOHRRECURSO"].ToString();

                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                    conn.BeginTransaction();
                    try
                    {
                        conn.ExecTransaction("DELETE FROM PGRUPORECURSOCUSTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODGRUPORECURSO = ? AND ANOCUSTOHRRECURSO = ? AND MESCUSTOHRRECURSO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codGrupoRecurso, ano, mes });

                        conn.Commit();
                        MessageBox.Show("Cadastro Excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        conn.Rollback();
                        throw;
                    }

                    string where = "WHERE PGRUPORECURSOCUSTO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND PGRUPORECURSOCUSTO.CODFILIAL = '" + AppLib.Context.Filial + "' AND PGRUPORECURSOCUSTO.CODGRUPORECURSO = '" + codGrupoRecurso + "'";
                    carregaCusto(where);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Excluir Custo, contate o Administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
