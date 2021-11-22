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
    public partial class FrmUnidade : Form
    {
        public bool edita = false;
        public string codUnidade = string.Empty;
        private string mask;
        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;
        public FrmUnidade()
        {
            InitializeComponent();
        }

        public FrmUnidade(ref NewLookup lookup)
        {
            InitializeComponent();

            this.codUnidade = lookup.txtcodigo.Text;
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
                    codUnidade = txtCodigo.Text;
                    carregaCampos();
                }
                else
                {
                    switch (lookup.CampoCodigo_Igual_a)
                    {
                        case NewLookup.CampoCodigoIguala.CampoCodigoBD:
                            codUnidade = txtCodigo.Text;
                            carregaCampos();

                            lookup.txtcodigo.Text = txtCodigo.Text;
                            lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                            lookup.ValorCodigoInterno = txtCodigo.Text;

                            this.Dispose();
                            break;
                        case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                            if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                            {
                                codUnidade = txtCodigo.Text;
                                carregaCampos();

                                lookup.txtcodigo.Text = txtCodigo.Text.ToUpper();
                                lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                                lookup.ValorCodigoInterno = txtCodigo.Text.ToUpper();

                                this.Dispose();
                            }
                            else
                            {
                                codUnidade = txtCodigo.Text;
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

        private void FrmUnidade_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
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
                errorProvider.SetError(txtCodigo, "Favor preencher o código da unidade");
                verifica = false;
            }
            else
            {
                if (this.edita == false)
                {
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM VUNID WHERE CODEMPRESA = ? AND CODUNID = ? ", new object[] { AppLib.Context.Empresa, txtCodigo.Text });
                    if (dt.Rows.Count > 0)
                    {
                        errorProvider.SetError(txtCodigo, "Código Já Cadastrado");
                        verifica = false;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtDescricao.Text))
            {
                errorProvider.SetError(txtDescricao, "Favor preencher o nome");
                verifica = false;
            }

            if (string.IsNullOrEmpty(txtFatorConversao.Text))
            {
                errorProvider.SetError(txtFatorConversao, "Favor preencher o Fator de Converão");
                verifica = false;
            }

            //if (string.IsNullOrEmpty(lookupunidade.ValorCodigoInterno))
            //{
            //    lookupunidade.mensagemErrorProvider = "Selecione a Unidade Base";
            //    verifica = false;
            //}
            //else
            //{
            //    lookupunidade.mensagemErrorProvider = "";
            //}

            return verifica;
        }

        private void carregaObj(DataTable dt)
        {
            txtCodigo.Text = dt.Rows[0]["CODUNID"].ToString();
            txtDescricao.Text = dt.Rows[0]["NOME"].ToString();
            txtFatorConversao.Text = dt.Rows[0]["FATORCONVERSAO"].ToString();
            lookupunidade.txtcodigo.Text = dt.Rows[0]["CODUNIDBASE"].ToString();
            lookupunidade.CarregaDescricao();

            txtCodigo.Enabled = false;
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM VUNID WHERE CODEMPRESA = ? AND CODUNID = ? ", new object[] { AppLib.Context.Empresa, codUnidade });
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
                        dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM VUNID WHERE CODEMPRESA = ? AND CODUNID = ? ", new object[] { AppLib.Context.Empresa,  codUnidade });
                        if (dt.Rows.Count > 0)
                        {
                            carregaObj(dt);
                        }
                        break;

                    case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                        if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM VUNID WHERE CODEMPRESA = ? AND " + lookup.CampoCodigoInterno + " = ? ", new object[] { AppLib.Context.Empresa,  lookup.ValorCodigoInterno });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        else
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM VUNID WHERE CODEMPRESA = ? AND CODUNID = ? ", new object[] { AppLib.Context.Empresa, codUnidade });
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
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "VUNID");
                conn.BeginTransaction();
                try
                {
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("FATORCONVERSAO", Convert.ToDecimal(txtFatorConversao.Text));
                    v.Set("CODUNID", txtCodigo.Text);
                    v.Set("NOME", txtDescricao.Text);
                    if (string.IsNullOrEmpty(lookupunidade.ValorCodigoInterno))
                    {
                        v.Set("CODUNIDBASE", null);
                    }
                    else
                    {
                        v.Set("CODUNIDBASE", lookupunidade.ValorCodigoInterno.ToString());
                    }

                    v.Save();
                    conn.Commit();

                    codUnidade = txtCodigo.Text;

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
            lookupunidade.Clear();
            txtFatorConversao.Text = string.Empty; 

            txtCodigo.Enabled = true;

            txtCodigo.Focus();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //Implementar
        }
    }
}
