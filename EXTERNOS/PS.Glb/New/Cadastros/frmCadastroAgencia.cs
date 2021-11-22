using ITGProducao.Controles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroAgencia : Form
    {
        public bool edita = false;
        public string CodAgencia = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroAgencia()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GAGENCIA");
        }

        public frmCadastroAgencia(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GAGENCIA");

            this.edita = true;
            this.lookup = lookup;
            CodAgencia = lookup.ValorCodigoInterno;
            carregaCampos();
        }

        private void frmCadastroAgencia_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodAgencia.Enabled = false;
            }
        }

        private bool validacoes()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(tbCodAgencia.Text))
            {
                errorProvider1.SetError(tbCodAgencia, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GAGENCIA WHERE CODAGENCIA = ?", new object[] { CodAgencia });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GAGENCIA WHERE CODAGENCIA = ?", new object[] { CodAgencia });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodAgencia.Text = dt.Rows[0]["CODAGENCIA"].ToString();
            tbDigito.Text = dt.Rows[0]["DVAGENCIA"].ToString();
            lpBanco.txtcodigo.Text = dt.Rows[0]["CODBANCO"].ToString();
            lpBanco.CarregaDescricao();
            tbNome.Text = dt.Rows[0]["NOME"].ToString();
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
            AppLib.ORM.Jit GAGENCIA = new AppLib.ORM.Jit(conn, "GAGENCIA");
            conn.BeginTransaction();

            try
            {
                GAGENCIA.Set("CODEMPRESA", AppLib.Context.Empresa);
                GAGENCIA.Set("CODAGENCIA", tbCodAgencia.Text);
                GAGENCIA.Set("DVAGENCIA", tbDigito.Text);
                GAGENCIA.Set("NOME", tbNome.Text);

                if (!string.IsNullOrEmpty(lpBanco.txtcodigo.Text))
                {
                    GAGENCIA.Set("CODBANCO", lpBanco.txtcodigo.Text);
                }
                else
                {
                    GAGENCIA.Set("CODBANCO", null);
                }

                if (!string.IsNullOrEmpty(lpTipoRua.txtcodigo.Text))
                {
                    GAGENCIA.Set("CODTIPORUA", lpTipoRua.txtcodigo.Text);
                }
                else
                {
                    GAGENCIA.Set("CODTIPORUA", null);
                }

                GAGENCIA.Set("RUA", tbRua.Text);
                GAGENCIA.Set("NUMERO", tbNumero.Text);
                GAGENCIA.Set("COMPLEMENTO", tbComplemento.Text);

                if (!string.IsNullOrEmpty(lpTipoBairro.txtcodigo.Text))
                {
                    GAGENCIA.Set("CODTIPOBAIRRO", lpTipoBairro.txtcodigo.Text);
                }
                else
                {
                    GAGENCIA.Set("CODTIPOBAIRRO", null);
                }

                GAGENCIA.Set("BAIRRO", tbBairro.Text);
                GAGENCIA.Set("CEP", tbCep.Text);

                if (!string.IsNullOrEmpty(lpPais.txtcodigo.Text))
                {
                    GAGENCIA.Set("CODPAIS", lpPais.txtcodigo.Text);
                }
                else
                {
                    GAGENCIA.Set("CODPAIS", null);
                }

                if (!string.IsNullOrEmpty(lpEstado.txtcodigo.Text))
                {
                    GAGENCIA.Set("CODETD", lpEstado.txtcodigo.Text);
                }
                else
                {
                    GAGENCIA.Set("CODETD", null);
                }

                if (!string.IsNullOrEmpty(lpCidade.txtcodigo.Text))
                {
                    GAGENCIA.Set("CODCIDADE", lpCidade.txtcodigo.Text);
                }
                else
                {
                    GAGENCIA.Set("CODCIDADE", null);
                }
                GAGENCIA.Save();
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
