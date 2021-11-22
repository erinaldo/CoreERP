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
    public partial class FrmCentroCusto : Form
    {
        public bool edita = false;
        public string codCentroCusto = string.Empty;
        private string mask;
        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;
        public FrmCentroCusto()
        {
            InitializeComponent();
        }

        public FrmCentroCusto(ref NewLookup lookup)
        {
            InitializeComponent();

            this.codCentroCusto = lookup.txtcodigo.Text;
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
                    codCentroCusto = txtCodigo.Text;
                    carregaCampos();
                }
                else
                {
                    switch (lookup.CampoCodigo_Igual_a)
                    {
                        case NewLookup.CampoCodigoIguala.CampoCodigoBD:
                            codCentroCusto = txtCodigo.Text;
                            carregaCampos();

                            lookup.txtcodigo.Text = txtCodigo.Text;
                            lookup.txtconteudo.Text = txtNome.Text.ToUpper();
                            lookup.ValorCodigoInterno = txtCodigo.Text;

                            this.Dispose();
                            break;
                        case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                            if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                            {
                                codCentroCusto = txtCodigo.Text;
                                carregaCampos();

                                lookup.txtcodigo.Text = txtCodigo.Text;
                                lookup.txtconteudo.Text = txtNome.Text.ToUpper();
                                lookup.ValorCodigoInterno = txtCodigo.Text;

                                this.Dispose();
                            }
                            else
                            {
                                codCentroCusto = txtCodigo.Text;
                                carregaCampos();

                                lookup.txtcodigo.Text = txtCodigo.Text;
                                lookup.txtconteudo.Text = txtNome.Text.ToUpper();
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            txtCodigo.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtNome.Text = string.Empty;
            chkAtivo.Checked = false;
            chkPermiteLancamento.Checked = false;

            txtCodigo.Enabled = true;

            txtCodigo.Focus();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //IMPLEMENTAR
        }

        private bool validacao()
        {
            bool verifica = true;

            errorProvider.Clear();

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                errorProvider.SetError(txtCodigo, "Favor preencher o código do Centro de Custo");
                verifica = false;
            }
            else
            {
                if (this.edita == false)
                {
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM GCENTROCUSTO WHERE CODEMPRESA = ? AND CODCCUSTO = ? ", new object[] { AppLib.Context.Empresa,  txtCodigo.Text });
                    if (dt.Rows.Count > 0)
                    {
                        errorProvider.SetError(txtCodigo, "Código Já Cadastrado");
                        verifica = false;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtNome.Text))
            {
                errorProvider.SetError(txtDescricao, "Favor preencher o Nome");
                verifica = false;
            }


            return verifica;
        }

        private void FrmCentroCusto_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
            }
            else
            {
                btnNovo.PerformClick();
            }

            txtCodigo.Focus();
        }

        private void carregaObj(DataTable dt)
        {
            txtCodigo.Text = dt.Rows[0]["CODCCUSTO"].ToString();
            txtNome.Text = dt.Rows[0]["NOME"].ToString();
            txtDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();

            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            chkPermiteLancamento.Checked = Convert.ToBoolean(dt.Rows[0]["PERMITELANCAMENTO"]);

            txtCodigo.Enabled = false;

        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM GCENTROCUSTO WHERE CODEMPRESA = ? AND CODCCUSTO = ?", new object[] { AppLib.Context.Empresa, codCentroCusto });
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
                        dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM GCENTROCUSTO WHERE CODEMPRESA = ? AND CODCCUSTO = ? ", new object[] { AppLib.Context.Empresa,codCentroCusto });
                        if (dt.Rows.Count > 0)
                        {
                            carregaObj(dt);
                        }
                        break;

                    case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                        if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM GCENTROCUSTO WHERE CODEMPRESA = ? AND " + lookup.CampoCodigoInterno + " = ? ", new object[] { AppLib.Context.Empresa, lookup.ValorCodigoInterno });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        else
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM GCENTROCUSTO WHERE CODEMPRESA = ? AND CODCCUSTO = ? ", new object[] { AppLib.Context.Empresa, codCentroCusto });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        break;
                }
            }
        }

        private bool Salvar()
        {
            bool _salvar = false;

            if (validacao() == true)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "GCENTROCUSTO");
                conn.BeginTransaction();
                try
                {
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODCCUSTO", txtCodigo.Text);
                    v.Set("NOME", txtNome.Text);
                    v.Set("DESCRICAO", (string.IsNullOrEmpty(txtDescricao.Text)? null : txtDescricao.Text));
                    v.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                    v.Set("PERMITELANCAMENTO", chkPermiteLancamento.Checked == true ? 1 : 0);

                    v.Save();
                    conn.Commit();

                    codCentroCusto = txtCodigo.Text;

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
    }

}
