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
    public partial class frmCadastroVendedor : Form
    {
        public bool edita = false;
        public string CodVendedor = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroVendedor()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VVENDEDOR");

            #region Combobox

            List<PS.Lib.ComboBoxItem> listCODTIPLOC = new List<PS.Lib.ComboBoxItem>();

            listCODTIPLOC.Add(new PS.Lib.ComboBoxItem());
            listCODTIPLOC[0].ValueMember = "F";
            listCODTIPLOC[0].DisplayMember = "Física";

            listCODTIPLOC.Add(new PS.Lib.ComboBoxItem());
            listCODTIPLOC[1].ValueMember = "J";
            listCODTIPLOC[1].DisplayMember = "Jurídica";

            cmbTipo.DataSource = listCODTIPLOC;
            cmbTipo.DisplayMember = "DisplayMember";
            cmbTipo.ValueMember = "ValueMember";

            #endregion

        }

        public frmCadastroVendedor(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VVENDEDOR");

            #region Combobox

            List<PS.Lib.ComboBoxItem> listCODTIPLOC = new List<PS.Lib.ComboBoxItem>();

            listCODTIPLOC.Add(new PS.Lib.ComboBoxItem());
            listCODTIPLOC[0].ValueMember = "F";
            listCODTIPLOC[0].DisplayMember = "Física";

            listCODTIPLOC.Add(new PS.Lib.ComboBoxItem());
            listCODTIPLOC[1].ValueMember = "J";
            listCODTIPLOC[1].DisplayMember = "Jurídica";

            cmbTipo.DataSource = listCODTIPLOC;
            cmbTipo.DisplayMember = "DisplayMember";
            cmbTipo.ValueMember = "ValueMember";

            #endregion

            this.edita = true;
            this.lookup = lookup;
            CodVendedor = lookup.ValorCodigoInterno;
            carregaCampos();
        }

        private void frmCadastroVendedor_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodVendedor.Enabled = false;
            }
        }

        private bool validacoes()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(tbCodVendedor.Text))
            {
                errorProvider1.SetError(tbCodVendedor, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbCpfCnpj.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            tbInscricaoEstadua.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;

            if (cmbTipo.SelectedIndex == 0)
            {
                tbCpfCnpj.Properties.Mask.EditMask = "000.000.000-00";
                lbInscricao.Text = "RG";
                tbInscricaoEstadua.Properties.Mask.EditMask = "00.000.000-0"; 
            }
            if (cmbTipo.SelectedIndex == 1)
            {
                lbInscricao.Text = "Inscrição Estadual";
                tbCpfCnpj.Properties.Mask.EditMask = "00.000.000/0000-00";
                tbInscricaoEstadua.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            }
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VVENDEDOR WHERE CODVENDEDOR = ?", new object[] { CodVendedor });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VVENDEDOR WHERE CODVENDEDOR = ?", new object[] { CodVendedor });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodVendedor.Text = dt.Rows[0]["CODVENDEDOR"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            tbCpfCnpj.Text = dt.Rows[0]["CNPJCPF"].ToString();
            tbNome.Text = dt.Rows[0]["NOME"].ToString();
            tbNomeFantasia.Text = dt.Rows[0]["NOMEFANTASIA"].ToString();
            cmbTipo.SelectedValue = dt.Rows[0]["TIPO"];
            tbInscricaoEstadua.Text = dt.Rows[0]["INSCRICAOESTADUAL"].ToString();
            tbComissao.Text = dt.Rows[0]["PRCOMISSAO"].ToString();
            lpUsuario.txtcodigo.Text = dt.Rows[0]["CODUSUARIO"].ToString();
            lpUsuario.CarregaDescricao();
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
            AppLib.ORM.Jit VVENDEDOR = new AppLib.ORM.Jit(conn, "VVENDEDOR");
            conn.BeginTransaction();

            try
            {
                VVENDEDOR.Set("CODEMPRESA", AppLib.Context.Empresa);
                VVENDEDOR.Set("CODVENDEDOR", tbCodVendedor.Text);
                VVENDEDOR.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                VVENDEDOR.Set("NOME", tbNome.Text);
                VVENDEDOR.Set("NOMEFANTASIA", tbNomeFantasia.Text);
                VVENDEDOR.Set("TIPO", cmbTipo.SelectedValue);
                VVENDEDOR.Set("CNPJCPF", tbCpfCnpj.Text);
                VVENDEDOR.Set("INSCRICAOESTADUAL", tbInscricaoEstadua.Text);
                VVENDEDOR.Set("PRCOMISSAO", Convert.ToDecimal(tbComissao.Text));

                if (!string.IsNullOrEmpty(lpUsuario.ValorCodigoInterno))
                {
                    VVENDEDOR.Set("CODUSUARIO", lpUsuario.ValorCodigoInterno);
                }
                else
                {
                    VVENDEDOR.Set("CODUSUARIO", null);
                }

                if (!string.IsNullOrEmpty(lpTipoRua.ValorCodigoInterno))
                {
                    VVENDEDOR.Set("CODTIPORUA", lpTipoRua.txtcodigo.Text);
                }
                else
                {
                    VVENDEDOR.Set("CODTIPORUA", null);
                }

                VVENDEDOR.Set("RUA", tbRua.Text);
                VVENDEDOR.Set("NUMERO", tbNumero.Text);
                VVENDEDOR.Set("COMPLEMENTO", tbComplemento.Text);

                if (!string.IsNullOrEmpty(lpTipoBairro.txtcodigo.Text))
                {
                    VVENDEDOR.Set("CODTIPOBAIRRO", lpTipoBairro.ValorCodigoInterno);
                }
                else
                {
                    VVENDEDOR.Set("CODTIPOBAIRRO", null);
                }

                VVENDEDOR.Set("BAIRRO", tbBairro.Text);
                VVENDEDOR.Set("CEP", tbCep.Text);

                if (!string.IsNullOrEmpty(lpPais.txtcodigo.Text))
                {
                    VVENDEDOR.Set("CODPAIS", lpPais.txtcodigo.Text);
                }
                else
                {
                    VVENDEDOR.Set("CODPAIS", null);
                }

                if (!string.IsNullOrEmpty(lpEstado.txtcodigo.Text))
                {
                    VVENDEDOR.Set("CODETD", lpEstado.txtcodigo.Text);
                }
                else
                {
                    VVENDEDOR.Set("CODETD", null);
                }

                if (!string.IsNullOrEmpty(lpCidade.txtcodigo.Text))
                {
                    VVENDEDOR.Set("CODCIDADE", lpCidade.txtcodigo.Text);
                }
                else
                {
                    VVENDEDOR.Set("CODCIDADE", null);
                }

                VVENDEDOR.Set("TELEFONE", tbTelefone.Text);
                VVENDEDOR.Set("TELCELULAR", tbCelular.Text);
                VVENDEDOR.Set("TELFAX", tbFax.Text);
                VVENDEDOR.Set("CONTATO", tbContato.Text);
                VVENDEDOR.Set("HOMEPAGE", tbHomePage.Text);
                VVENDEDOR.Set("EMAIL", tbEmail.Text);

                VVENDEDOR.Save();
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
