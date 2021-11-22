using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITGProducao.Controles;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroRepresentante : Form
    {
        public bool edita = false;
        public string CodRepre = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroRepresentante()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VREPRE");
        }

        public frmCadastroRepresentante(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VVENDEDOR");
            this.edita = true;
            this.lookup = lookup;
            CodRepre = lookup.ValorCodigoInterno;
            carregaCampos();
        }

        private void frmCadastroRepresentante_Load(object sender, EventArgs e)
        {
            tbCpfCnpj.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            tbCpfCnpj.Properties.Mask.EditMask = "00.000.000/0000-00";

            if (edita == true)
            {
                carregaCampos();
                tbCodRepresentante.Enabled = false;
            }
        }


        private bool validacoes()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(tbCodRepresentante.Text))
            {
                errorProvider1.SetError(tbCodRepresentante, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VREPRE WHERE CODREPRE = ?", new object[] { CodRepre });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VREPRE WHERE CODREPRE = ?", new object[] { CodRepre });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodRepresentante.Text = dt.Rows[0]["CODREPRE"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            tbCpfCnpj.Text = dt.Rows[0]["CGCCPF"].ToString();
            tbNome.Text = dt.Rows[0]["NOME"].ToString();
            tbNomeFantasia.Text = dt.Rows[0]["NOMEFANTASIA"].ToString();
            tbInscricaoEstadua.Text = dt.Rows[0]["INSCRICAOESTADUAL"].ToString();
            tbComissao.Text = dt.Rows[0]["PRCOMISSAO"].ToString();
            lpTipoRua.txtcodigo.Text = dt.Rows[0]["CODTIPORUA"].ToString();
            lpTipoRua.CarregaDescricao();
            tbRua.Text = dt.Rows[0]["RUA"].ToString();
            tbNumero.Text = dt.Rows[0]["NUMERO"].ToString();
            tbComplemento.Text = dt.Rows[0]["COMPLEMENTO"].ToString();
            lpTipoBairro.txtcodigo.Text = dt.Rows[0]["CODTIPOBAIRRO"].ToString();
            lpTipoBairro.CarregaDescricao();
            tbBairro.Text = dt.Rows[0]["BAIRRO"].ToString();
            tbCep.Text = dt.Rows[0]["CEP"].ToString();
            lpPais.txtcodigo.Text = dt.Rows[0]["CODPAIS"].ToString();
            lpPais.CarregaDescricao();
            lpEstado.txtcodigo.Text = dt.Rows[0]["CODETD"].ToString();
            lpEstado.CarregaDescricao();
            lpCidade.txtcodigo.Text = dt.Rows[0]["CODCIDADE"].ToString();
            lpCidade.CarregaDescricao();
            tbTelefone.Text = dt.Rows[0]["TELEFONE"].ToString();
            tbCelular.Text = dt.Rows[0]["TELCELULAR"].ToString();
            tbFax.Text = dt.Rows[0]["TELFAX"].ToString();
            tbContato.Text = dt.Rows[0]["CONTATO"].ToString();
            tbHomePage.Text = dt.Rows[0]["HOMEPAGE"].ToString();
            tbEmail.Text = dt.Rows[0]["EMAIL"].ToString();
        }

        private void btnSearchCep_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCep.Text))
            {
                PS.Glb.Class.WebCep web = new Class.WebCep(tbCep.Text);
                tbRua.Text = web.Lagradouro;
                lpTipoRua.txtconteudo.Text = web.TipoLagradouro;
                lpTipoRua.txtcodigo.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODTIPORUA FROM GTIPORUA WHERE NOME = ?", new object[] { web.TipoLagradouro }).ToString();
                tbBairro.Text = web.Bairro;
                lpEstado.txtcodigo.Text = web.UF;
                lpEstado.txtconteudo.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GESTADO WHERE CODETD = ?", new object[] { web.UF }).ToString();
                lpCidade.txtconteudo.Text = web.Cidade;
                lpCidade.txtcodigo.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODCIDADE FROM GCIDADE WHERE NOME = ?", new object[] { web.Cidade }).ToString();
                lpPais.txtcodigo.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODPAIS FROM GPAIS WHERE NOME = ?", new object[] { "Brasil" }).ToString();
                lpPais.txtconteudo.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GPAIS WHERE CODPAIS = ?", new object[] { "1" }).ToString();
            }
        }

        private bool Salvar()
        {
            if (validacoes() == false)
            {
                return false;
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VREPRE = new AppLib.ORM.Jit(conn, "VREPRE");
            conn.BeginTransaction();

            try
            {
                VREPRE.Set("CODEMPRESA", AppLib.Context.Empresa);
                VREPRE.Set("CODREPRE", tbCodRepresentante.Text);
                VREPRE.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                VREPRE.Set("NOME", tbNome.Text);
                VREPRE.Set("NOMEFANTASIA", tbNomeFantasia.Text);
                VREPRE.Set("CGCCPF", tbCpfCnpj.Text);
                VREPRE.Set("INSCRICAOESTADUAL", tbInscricaoEstadua.Text);
                VREPRE.Set("PRCOMISSAO", Convert.ToDecimal(tbComissao.Text));

                if (!string.IsNullOrEmpty(lpTipoRua.ValorCodigoInterno))
                {
                    VREPRE.Set("CODTIPORUA", lpTipoRua.ValorCodigoInterno);
                }
                else
                {
                    VREPRE.Set("CODTIPORUA", null);
                }

                VREPRE.Set("RUA", tbRua.Text);
                VREPRE.Set("NUMERO", tbNumero.Text);
                VREPRE.Set("COMPLEMENTO", tbComplemento.Text);

                if (!string.IsNullOrEmpty(lpTipoBairro.ValorCodigoInterno))
                {
                    VREPRE.Set("CODTIPOBAIRRO", lpTipoBairro.ValorCodigoInterno);
                }
                else
                {
                    VREPRE.Set("CODTIPOBAIRRO", null);
                }

                VREPRE.Set("BAIRRO", tbBairro.Text);
                VREPRE.Set("CEP", tbCep.Text);

                if (!string.IsNullOrEmpty(lpPais.txtcodigo.Text))
                {
                    VREPRE.Set("CODPAIS", lpPais.txtcodigo.Text);
                }
                else
                {
                    VREPRE.Set("CODPAIS", null);
                }

                if (!string.IsNullOrEmpty(lpEstado.txtcodigo.Text))
                {
                    VREPRE.Set("CODETD", lpEstado.txtcodigo.Text);
                }
                else
                {
                    VREPRE.Set("CODETD", null);
                }

                if (!string.IsNullOrEmpty(lpCidade.txtcodigo.Text))
                {
                    VREPRE.Set("CODCIDADE", lpCidade.txtcodigo.Text);
                }
                else
                {
                    VREPRE.Set("CODCIDADE", null);
                }

                VREPRE.Set("TELEFONE", tbTelefone.Text);
                VREPRE.Set("TELCELULAR", tbCelular.Text);
                VREPRE.Set("TELFAX", tbFax.Text);
                VREPRE.Set("CONTATO", tbContato.Text);
                VREPRE.Set("HOMEPAGE", tbHomePage.Text);
                VREPRE.Set("EMAIL", tbEmail.Text);

                VREPRE.Save();
                conn.Commit();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                Salvar();
            }
            else
            {
                if (Salvar() == true)
                {
                    carregaCampos();
                    this.Dispose();
                }
            }
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
