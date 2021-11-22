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
    public partial class FrmOrdemMotivoParada : Form
    {
        public bool edita = false;
        public string codMotivo = string.Empty;
        private string mask;
        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;

        public FrmOrdemMotivoParada()
        {
            InitializeComponent();
        }

        public FrmOrdemMotivoParada(ref NewLookup lookup)
        {
            InitializeComponent();

            this.codMotivo = lookup.txtcodigo.Text;
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
                    codMotivo = txtCodigo.Text;
                    carregaCampos();
                }
                else
                {
                    codMotivo = txtCodigo.Text;
                    carregaCampos();

                    lookup.txtcodigo.Text = txtCodigo.Text;
                    lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                    lookup.ValorCodigoInterno = txtCodigo.Text;

                    this.Dispose();
                }
            }
        }

        private void FrmOrdemMotivoParada_Load(object sender, EventArgs e)
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
                errorProvider.SetError(txtCodigo, "Favor preencher o código do motivo");
                verifica = false;
            }
            else
            {
                if (this.edita == false)
                {
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PORDEMMOTIVOPARADA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODMOTIVOPARADA = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text });
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
            txtCodigo.Text = dt.Rows[0]["CODMOTIVOPARADA"].ToString();
            txtDescricao.Text = dt.Rows[0]["DESCMOTIVOPARADA"].ToString();

            txtCodigo.Enabled = false;
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PORDEMMOTIVOPARADA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODMOTIVOPARADA = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codMotivo });
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
                        dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PORDEMMOTIVOPARADA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODMOTIVOPARADA = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codMotivo });
                        if (dt.Rows.Count > 0)
                        {
                            carregaObj(dt);
                        }
                        break;

                    case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                        if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PORDEMMOTIVOPARADA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND " + lookup.CampoCodigoInterno + " = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookup.ValorCodigoInterno });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        else
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PORDEMMOTIVOPARADA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODMOTIVOPARADA = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codMotivo });
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
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEMMOTIVOPARADA");
                conn.BeginTransaction();
                try
                {
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODFILIAL", AppLib.Context.Filial);
                    v.Set("CODMOTIVOPARADA", txtCodigo.Text);
                    v.Set("DESCMOTIVOPARADA", txtDescricao.Text);

                    v.Save();
                    conn.Commit();

                    codMotivo = txtCodigo.Text;

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
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM PORDEMAPONTAMENTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODMOTIVOPARADA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codMotivo });
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Esta motivo esta vinculado a um apontamento e não pode ser excluída.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                        conn.BeginTransaction();
                        try
                        {
                            conn.ExecTransaction("DELETE FROM PORDEMMOTIVOPARADA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODMOTIVOPARADA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codMotivo });
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

                MessageBox.Show("Erro ao excluir motivo de parada, contate o Administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


}
