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
    public partial class frmCadastroFabricante : Form
    {
        public bool edita = false;
        public string CodFabricante = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroFabricante()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VFABRICANTE");
        }

        public frmCadastroFabricante(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VFABRICANTE");
            this.edita = true;
            this.lookup = lookup;
            CodFabricante = lookup.ValorCodigoInterno;
            carregaCampos();
        }

        private void frmCadastroFabricante_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodFabricante.Enabled = false;
            }
        }

        private bool validacoes()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(tbCodFabricante.Text))
            {
                errorProvider1.SetError(tbCodFabricante, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VFABRICANTE WHERE CODFABRICANTE = ?", new object[] { CodFabricante });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VFABRICANTE WHERE CODFABRICANTE = ?", new object[] { CodFabricante });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodFabricante.Text = dt.Rows[0]["CODFABRICANTE"].ToString();
            tbNome.Text = dt.Rows[0]["NOME"].ToString();
            tbNomeFantasia.Text = dt.Rows[0]["NOMEFANTASIA"].ToString();
            tbCGCCPF.Text = dt.Rows[0]["CGCCPF"].ToString();
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

        private bool Salvar()
        {
            if (validacoes() == false)
            {
                return false;
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VFABRICANTE = new AppLib.ORM.Jit(conn, "VFABRICANTE");
            conn.BeginTransaction();

            try
            {
                VFABRICANTE.Set("CODEMPRESA", AppLib.Context.Empresa);

                VFABRICANTE.Set("CODFABRICANTE", tbCodFabricante.Text);
                VFABRICANTE.Set("NOME", tbNome.Text);
                VFABRICANTE.Set("NOMEFANTASIA", tbNomeFantasia.Text);
                VFABRICANTE.Set("CGCCPF", tbCGCCPF.Text);

                if (!string.IsNullOrEmpty(lpTipoRua.ValorCodigoInterno))
                {
                    VFABRICANTE.Set("CODTIPORUA", lpTipoRua.ValorCodigoInterno);
                }
                else
                {
                    VFABRICANTE.Set("CODTIPORUA", null);
                }

                VFABRICANTE.Set("RUA", tbRua.Text);
                VFABRICANTE.Set("NUMERO", tbNumero.Text);
                VFABRICANTE.Set("COMPLEMENTO", tbComplemento.Text);

                if (!string.IsNullOrEmpty(lpTipoBairro.ValorCodigoInterno))
                {
                    VFABRICANTE.Set("CODTIPOBAIRRO", lpTipoBairro.ValorCodigoInterno);
                }
                else
                {
                    VFABRICANTE.Set("CODTIPOBAIRRO", null);
                }

                VFABRICANTE.Set("BAIRRO", tbBairro.Text);
                VFABRICANTE.Set("CEP", tbCep.Text);

                if (!string.IsNullOrEmpty(lpPais.ValorCodigoInterno))
                {
                    VFABRICANTE.Set("CODPAIS", lpPais.ValorCodigoInterno);
                }
                else
                {
                    VFABRICANTE.Set("CODPAIS", null);
                }

                if (!string.IsNullOrEmpty(lpEstado.ValorCodigoInterno))
                {
                    VFABRICANTE.Set("CODETD", lpEstado.ValorCodigoInterno);
                }
                else
                {
                    VFABRICANTE.Set("CODETD", null);
                }

                if (!string.IsNullOrEmpty(lpCidade.ValorCodigoInterno))
                {
                    VFABRICANTE.Set("CODCIDADE", lpCidade.ValorCodigoInterno);
                }
                else
                {
                    VFABRICANTE.Set("CODCIDADE", null);
                }
                VFABRICANTE.Save();
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
    }
}
