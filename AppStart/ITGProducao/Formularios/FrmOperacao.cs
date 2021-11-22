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
    public partial class FrmOperacao : Form
    {
        public bool edita = false;
        public string codOperacao = string.Empty;
        private string mask;
        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;

        public FrmOperacao()
        {
            InitializeComponent();
        }

        public FrmOperacao(ref NewLookup lookup)
        {
            InitializeComponent();

            this.codOperacao = lookup.txtcodigo.Text;
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
            listTipoRecurso[1].ValueMember = "1";
            listTipoRecurso[1].DisplayMember = "1 - Proporcional";

            listTipoRecurso.Add(new PS.Lib.ComboBoxItem());
            listTipoRecurso[2].ValueMember = "2";
            listTipoRecurso[2].DisplayMember = "2 - Fixo (Uma Unidade)";

            cmbTipoQuantidade.DataSource = listTipoRecurso;
            cmbTipoQuantidade.DisplayMember = "DisplayMember";
            cmbTipoQuantidade.ValueMember = "ValueMember";

            cmbTipoQuantidade.SelectedIndex = -1;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                if (this.lookup == null)
                {
                    MessageBox.Show("Cadastro salvo com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    codOperacao = txtCodigo.Text;
                    carregaCampos();
                }
                else
                {
                    switch (lookup.CampoCodigo_Igual_a)
                    {
                        case NewLookup.CampoCodigoIguala.CampoCodigoBD:
                            codOperacao = txtCodigo.Text;
                            carregaCampos();

                            lookup.txtcodigo.Text = txtCodigo.Text;
                            lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                            lookup.ValorCodigoInterno = txtCodigo.Text;

                            this.Dispose();
                            break;
                        case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                            if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                            {
                                codOperacao = txtCodigo.Text;
                                carregaCampos();

                                lookup.txtcodigo.Text = txtCodigo.Text;
                                lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                                lookup.ValorCodigoInterno = txtCodigo.Text;

                                this.Dispose();
                            }
                            else
                            {
                                codOperacao = txtCodigo.Text;
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

        private void FrmOperacao_Load(object sender, EventArgs e)
        {
            CarregaCombos();

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool validacao()
        {
            bool verifica = true;

            errorProvider.Clear();
            lookupservicoexterno.mensagemErrorProvider = "";
            lookupcentrotrabalho.mensagemErrorProvider = "";
            lookupequipamento.mensagemErrorProvider = "";
            lookupferramenta.mensagemErrorProvider = "";
            lookupmaodeobra.mensagemErrorProvider = "";

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                errorProvider.SetError(txtCodigo, "Favor preencher o Código");
                verifica = false;
            }
            else
            {
                if (this.edita == false)
                {
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM POPERACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text });
                    if (dt.Rows.Count > 0)
                    {
                        errorProvider.SetError(txtCodigo, "Código Já Cadastrado");
                        verifica = false;
                    }
                }
            }

            //if (string.IsNullOrEmpty(lookupcentrotrabalho.ValorCodigoInterno))
            //{
            //    lookupcentrotrabalho.mensagemErrorProvider = "Favor preencher o Centro de Trabalho";
            //    verifica = false;
            //}
            //else
            //{
            //    lookupcentrotrabalho.mensagemErrorProvider = "";
            //}

            //if (!string.IsNullOrEmpty(lookupmaodeobra.ValorCodigoInterno) && !string.IsNullOrEmpty(lookupcentrotrabalho.ValorCodigoInterno))
            //{
            //    if (CentroTrabalhoIgual(lookupcentrotrabalho.ValorCodigoInterno.ToString(), "MO", lookupmaodeobra.ValorCodigoInterno.ToString()) == false)
            //    {
            //        lookupmaodeobra.mensagemErrorProvider = "O Centro de Trabalho selecionado é diferente do centro de trabalho para esta mão de obra.";
            //    }
            //}

            //if (!string.IsNullOrEmpty(lookupferramenta.ValorCodigoInterno) && !string.IsNullOrEmpty(lookupcentrotrabalho.ValorCodigoInterno))
            //{
            //    if (CentroTrabalhoIgual(lookupcentrotrabalho.ValorCodigoInterno.ToString(), "FE", lookupferramenta.ValorCodigoInterno.ToString()) == false)
            //    {
            //        lookupferramenta.mensagemErrorProvider = "O Centro de Trabalho selecionado é diferente do centro de trabalho para esta ferramenta.";
            //    }
            //}

            //if (!string.IsNullOrEmpty(lookupequipamento.ValorCodigoInterno) && !string.IsNullOrEmpty(lookupcentrotrabalho.ValorCodigoInterno))
            //{
            //    if (CentroTrabalhoIgual(lookupcentrotrabalho.ValorCodigoInterno.ToString(), "EQ", lookupequipamento.ValorCodigoInterno.ToString()) == false)
            //    {
            //        lookupequipamento.mensagemErrorProvider = "O Centro de Trabalho selecionado é diferente do centro de trabalho para este equipamento.";
            //    }
            //}

            if (string.IsNullOrEmpty(txtDescricao.Text))
            {
                errorProvider.SetError(txtDescricao, "Favor preencher a Descrição");
                verifica = false;
            }

            if (chkExterno.Checked == true)
            {
                if (string.IsNullOrEmpty(lookupservicoexterno.ValorCodigoInterno))
                {
                    lookupservicoexterno.mensagemErrorProvider = "Selecione o Serviço Externo";
                    verifica = false;
                }
                else
                {
                    lookupservicoexterno.mensagemErrorProvider = "";
                }


                if ((cmbTipoQuantidade.SelectedIndex == -1) || (cmbTipoQuantidade.SelectedIndex == 0))
                {
                    errorProvider.SetError(cmbTipoQuantidade, "Selecione o Tipo de Quantidade");
                    verifica = false;
                }
            }

            return verifica;
        }
        private void carregaObj(DataTable dt)
        {
            txtCodigo.Text = dt.Rows[0]["CODOPERACAO"].ToString();
            txtDescricao.Text = dt.Rows[0]["DESCOPERACAO"].ToString();

            lookupequipamento.txtcodigo.Text = dt.Rows[0]["GRUPORECURSOEQ"].ToString();
            lookupequipamento.CarregaDescricao();

            lookupmaodeobra.txtcodigo.Text = dt.Rows[0]["GRUPORECURSOMO"].ToString();
            lookupmaodeobra.CarregaDescricao();

            lookupferramenta.txtcodigo.Text = dt.Rows[0]["GRUPORECURSOFE"].ToString();
            lookupferramenta.CarregaDescricao();

            lookupcentrotrabalho.txtcodigo.Text = dt.Rows[0]["CODCTRABALHO"].ToString();
            lookupcentrotrabalho.CarregaDescricao();
            

            chkExterno.Checked = Convert.ToBoolean(dt.Rows[0]["OPERACAOEXTERNA"]);
            if (chkExterno.Checked == true)
            {
                lookupservicoexterno.Edita(true);
                cmbTipoQuantidade.Enabled = true;
            }
            else
            {
                lookupservicoexterno.Edita(false);
                cmbTipoQuantidade.Enabled = false;
            }

            lookupservicoexterno.txtcodigo.Text = dt.Rows[0]["SERVICOEXTERNO"].ToString();
            lookupservicoexterno.CarregaDescricao();

            cmbTipoQuantidade.SelectedValue = dt.Rows[0]["TIPOQUANTIDADE"].ToString();

            txtCodigo.Enabled = false;
        }
        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM POPERACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codOperacao });
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
                        dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM POPERACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codOperacao });
                        if (dt.Rows.Count > 0)
                        {
                            carregaObj(dt);
                        }
                        break;

                    case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                        if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM POPERACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND " + lookup.CampoCodigoInterno + " = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookup.ValorCodigoInterno });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        else
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM POPERACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codOperacao });
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
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "POPERACAO");
                conn.BeginTransaction();
                try
                {
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODFILIAL", AppLib.Context.Filial);
                    v.Set("CODOPERACAO", txtCodigo.Text);
                    v.Set("DESCOPERACAO", txtDescricao.Text);

                    v.Set("OPERACAOEXTERNA", chkExterno.Checked == true ? 1 : 0);

                    if (string.IsNullOrEmpty(lookupcentrotrabalho.ValorCodigoInterno))
                    {
                        v.Set("CODCTRABALHO", null);
                    }
                    else
                    {
                        v.Set("CODCTRABALHO", lookupcentrotrabalho.ValorCodigoInterno.ToString());
                    }


                    if (string.IsNullOrEmpty(lookupequipamento.ValorCodigoInterno))
                    {
                        v.Set("GRUPORECURSOEQ", null);
                    }
                    else
                    {
                        v.Set("GRUPORECURSOEQ", lookupequipamento.ValorCodigoInterno.ToString());
                    }

                    if (string.IsNullOrEmpty(lookupmaodeobra.ValorCodigoInterno.ToString()))
                    {
                        v.Set("GRUPORECURSOMO", null);
                    }
                    else
                    {
                        v.Set("GRUPORECURSOMO", lookupmaodeobra.ValorCodigoInterno.ToString());
                    }

                    if (string.IsNullOrEmpty(lookupferramenta.ValorCodigoInterno))
                    {
                        v.Set("GRUPORECURSOFE", null);
                    }
                    else
                    {
                        v.Set("GRUPORECURSOFE", lookupferramenta.ValorCodigoInterno.ToString());
                    }


                    if (chkExterno.Checked == true)
                    {
                        v.Set("TIPOQUANTIDADE", cmbTipoQuantidade.SelectedValue);                    
                        v.Set("SERVICOEXTERNO", lookupservicoexterno.ValorCodigoInterno);
                    }
                    else
                    {
                        v.Set("SERVICOEXTERNO", null);
                        v.Set("TIPOQUANTIDADE", null);
                    }

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
                }
            }
            else
            {
                _salvar = false;
            }

            return _salvar;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            txtCodigo.Text = string.Empty;
            txtDescricao.Text = string.Empty;

            lookupequipamento.Clear();
            lookupmaodeobra.Clear();
            lookupferramenta.Clear();
            lookupcentrotrabalho.Clear();

            lookupservicoexterno.Clear();
            cmbTipoQuantidade.SelectedIndex = -1;

            chkExterno.Checked = false;

            lookupservicoexterno.Edita(false);
            cmbTipoQuantidade.Enabled = false;

            txtCodigo.Enabled = true;

            txtCodigo.Focus();
        }

        private bool CentroTrabalhoIgual(string CODCTRABALHO,string TIPORECURSO,string CODGRUPORECURSO)
        {
            bool _verifica = false;

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"select CODCTRABALHO from PGRUPORECURSO where CODEMPRESA = ? AND CODFILIAL = ? AND CODGRUPORECURSO = ? AND TIPORECURSO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, CODGRUPORECURSO, TIPORECURSO });
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["CODCTRABALHO"].ToString() == CODCTRABALHO)
                {
                    _verifica = true;
                }
            }

            return _verifica;
        }

        //private DataTable retornaGrupoRecurso()
        //{
        //    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"select * from PGRUPORECURSO where CODEMPRESA = ? AND CODFILIAL = ? AND CODGRUPORECURSO = ? AND TIPORECURSO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, CODGRUPORECURSO, TIPORECURSO });
        //    if (dt.Rows.Count > 0)
        //    {

        //    }
        //    return dt;
        //}

        private bool validaPreencheCentroTrabalho()
        {
            bool _verifica = false;
            //if (!string.IsNullOrEmpty(lookupcentrotrabalho.ValorCodigoInterno))
            //{
            //    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"select * from PCENTROTRABALHO where CODEMPRESA = ? AND CODFILIAL = ? AND CODGRUPORECURSO = ? AND TIPORECURSO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, CODGRUPORECURSO, TIPORECURSO });
            //    if (dt.Rows.Count > 0)
            //    {

            //    }
            //    return dt;
            //}

            if (!string.IsNullOrEmpty(lookupferramenta.ValorCodigoInterno))
            {

            }
            if (!string.IsNullOrEmpty(lookupmaodeobra.ValorCodigoInterno))
            {

            }
            if (!string.IsNullOrEmpty(lookupequipamento.ValorCodigoInterno))
            {

            }
            //DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"select CODCTRABALHO from PGRUPORECURSO where CODEMPRESA = ? AND CODFILIAL = ? AND CODGRUPORECURSO = ? AND TIPORECURSO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, CODGRUPORECURSO, TIPORECURSO });
            //if (dt.Rows.Count > 0)
            //{
            //    if (dt.Rows[0]["CODCTRABALHO"].ToString() == CODCTRABALHO)
            //    {
            //        _verifica = true;
            //    }
            //}

            return _verifica;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM PROTEIRORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codOperacao });
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Esta Operação esta vinculado a uma Estrutura e não pode ser excluída.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                        conn.BeginTransaction();
                        try
                        {
                            conn.ExecTransaction("DELETE FROM POPERACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codOperacao });
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

                MessageBox.Show("Erro ao Excluir Operação, contate o Administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkExterno_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExterno.Checked == true)
            {
                lookupservicoexterno.Edita(true);
                cmbTipoQuantidade.Enabled = true;
            }
            else
            {
                lookupservicoexterno.Edita(false);
                cmbTipoQuantidade.Enabled = false;
            }
        }
    }
}
