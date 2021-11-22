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
    public partial class frmCadastroLocalEstoque : Form
    {
        public bool edita = false;
        public string CodLocal = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroLocalEstoque()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VLOCALESTOQUE");

            #region Combobox

            List<PS.Lib.ComboBoxItem> listCODTIPLOC = new List<PS.Lib.ComboBoxItem>();

            listCODTIPLOC.Add(new PS.Lib.ComboBoxItem());
            listCODTIPLOC[0].ValueMember = "NN";
            listCODTIPLOC[0].DisplayMember = "Nosso em nosso poder";

            listCODTIPLOC.Add(new PS.Lib.ComboBoxItem());
            listCODTIPLOC[1].ValueMember = "NT";
            listCODTIPLOC[1].DisplayMember = "Nosso em poder de terceiro";

            listCODTIPLOC.Add(new PS.Lib.ComboBoxItem());
            listCODTIPLOC[2].ValueMember = "TN";
            listCODTIPLOC[2].DisplayMember = "De terceiro em nosso poder";

            cmbTipoLoc.DataSource = listCODTIPLOC;
            cmbTipoLoc.DisplayMember = "DisplayMember";
            cmbTipoLoc.ValueMember = "ValueMember";

            #endregion
        }

        public frmCadastroLocalEstoque(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VLOCALESTOQUE");

            #region Combobox

            List<PS.Lib.ComboBoxItem> listCODTIPLOC = new List<PS.Lib.ComboBoxItem>();

            listCODTIPLOC.Add(new PS.Lib.ComboBoxItem());
            listCODTIPLOC[0].ValueMember = "NN";
            listCODTIPLOC[0].DisplayMember = "Nosso em nosso poder";

            listCODTIPLOC.Add(new PS.Lib.ComboBoxItem());
            listCODTIPLOC[1].ValueMember = "NT";
            listCODTIPLOC[1].DisplayMember = "Nosso em poder de terceiro";

            listCODTIPLOC.Add(new PS.Lib.ComboBoxItem());
            listCODTIPLOC[2].ValueMember = "TN";
            listCODTIPLOC[2].DisplayMember = "De terceiro em nosso poder";

            cmbTipoLoc.DataSource = listCODTIPLOC;
            cmbTipoLoc.DisplayMember = "DisplayMember";
            cmbTipoLoc.ValueMember = "ValueMember";

            #endregion

            this.edita = true;
            this.lookup = lookup;
            CodLocal = lookup.ValorCodigoInterno;
            carregaCampos();          
        }

        private void frmCadastroLocalEstoque_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodlocal.Enabled = false;
            }
        }

        private bool validacoes()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(tbCodlocal.Text))
            {
                errorProvider1.SetError(tbCodlocal, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VLOCALESTOQUE WHERE CODLOCAL = ?", new object[] { CodLocal });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VLOCALESTOQUE WHERE CODLOCAL = ?", new object[] { CodLocal });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            lpFilial.txtcodigo.Text = dt.Rows[0]["CODFILIAL"].ToString();
            lpFilial.CarregaDescricao();
            tbCodlocal.Text = dt.Rows[0]["CODLOCAL"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            tbNome.Text = dt.Rows[0]["NOME"].ToString();
            tbDescricao.Text = dt.Rows[0]["DESCRIÇÃO"].ToString();
            cmbTipoLoc.SelectedValue = dt.Rows[0]["CODTIPLOC"];
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
            AppLib.ORM.Jit VLOCALESTOQUE = new AppLib.ORM.Jit(conn, "VLOCALESTOQUE");
            conn.BeginTransaction();

            try
            {
                VLOCALESTOQUE.Set("CODEMPRESA", AppLib.Context.Empresa);

                if (!string.IsNullOrEmpty(lpFilial.ValorCodigoInterno))
                {
                    VLOCALESTOQUE.Set("CODFILIAL", lpFilial.ValorCodigoInterno);
                }
                else
                {
                    VLOCALESTOQUE.Set("CODFILIAL", null);
                }

                VLOCALESTOQUE.Set("CODLOCAL", tbCodlocal.Text);
                VLOCALESTOQUE.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                VLOCALESTOQUE.Set("NOME", tbNome.Text);
                VLOCALESTOQUE.Set("DESCRIÇÃO", tbDescricao.Text);
                VLOCALESTOQUE.Set("CODTIPLOC", cmbTipoLoc.SelectedValue);

                if (!string.IsNullOrEmpty(lpTipoRua.ValorCodigoInterno))
                {
                    VLOCALESTOQUE.Set("CODTIPORUA", lpTipoRua.ValorCodigoInterno);
                }
                else
                {
                    VLOCALESTOQUE.Set("CODTIPORUA", null);
                }

                VLOCALESTOQUE.Set("RUA", tbRua.Text);
                VLOCALESTOQUE.Set("NUMERO", tbNumero.Text);
                VLOCALESTOQUE.Set("COMPLEMENTO", tbComplemento.Text);

                if (!string.IsNullOrEmpty(lpTipoBairro.ValorCodigoInterno))
                {
                    VLOCALESTOQUE.Set("CODTIPOBAIRRO", lpTipoBairro.ValorCodigoInterno);
                }
                else
                {
                    VLOCALESTOQUE.Set("CODTIPOBAIRRO", null);
                }

                VLOCALESTOQUE.Set("BAIRRO", tbBairro.Text);
                VLOCALESTOQUE.Set("CEP", tbCep.Text);

                if (!string.IsNullOrEmpty(lpPais.ValorCodigoInterno))
                {
                    VLOCALESTOQUE.Set("CODPAIS", lpPais.ValorCodigoInterno);
                }
                else
                {
                    VLOCALESTOQUE.Set("CODPAIS", null);
                }

                if (!string.IsNullOrEmpty(lpEstado.ValorCodigoInterno))
                {
                    VLOCALESTOQUE.Set("CODETD", lpEstado.ValorCodigoInterno);
                }
                else
                {
                    VLOCALESTOQUE.Set("CODETD", null);
                }

                if (!string.IsNullOrEmpty(lpCidade.ValorCodigoInterno))
                {
                    VLOCALESTOQUE.Set("CODCIDADE", lpCidade.ValorCodigoInterno);
                }
                else
                {
                    VLOCALESTOQUE.Set("CODCIDADE", null);
                }
                VLOCALESTOQUE.Save();
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

        private void btnNovo_Click(object sender, EventArgs e)
        {
            lpFilial.Select();
            lpFilial.Clear();
            tbCodlocal.Text = string.Empty;
            chkAtivo.Checked = false;
            tbNome.Text = string.Empty;
            tbDescricao.Text = string.Empty;
            cmbTipoLoc.SelectedIndex = 0;
            lpTipoRua.Clear();
            tbRua.Text = string.Empty;
            tbNumero.Text = string.Empty;
            tbComplemento.Text = string.Empty;
            lpTipoBairro.Clear();
            tbBairro.Text = string.Empty;
            tbCep.Text = string.Empty;
            lpPais.Clear();
            lpEstado.Clear();
            lpCidade.Clear();
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
