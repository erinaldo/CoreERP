using ITGProducao.Controles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroTransportadora : Form
    {
        public bool edita = false;
        public string Codtransportadora = string.Empty;
        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroTransportadora()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VTRANSPORTADORA");

            #region Combobox

            List<PS.Lib.ComboBoxItem> listFisicoJuridico = new List<PS.Lib.ComboBoxItem>();

            listFisicoJuridico.Add(new PS.Lib.ComboBoxItem());
            listFisicoJuridico[0].ValueMember = 0;
            listFisicoJuridico[0].DisplayMember = "Jurídico";

            listFisicoJuridico.Add(new PS.Lib.ComboBoxItem());
            listFisicoJuridico[1].ValueMember = 1;
            listFisicoJuridico[1].DisplayMember = "Físico";

            cmbFisicoJuridico.DataSource = listFisicoJuridico;
            cmbFisicoJuridico.DisplayMember = "DisplayMember";
            cmbFisicoJuridico.ValueMember = "ValueMember";

            #endregion
        }

        public frmCadastroTransportadora(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VLOCALESTOQUE");

            #region Combobox

            List<PS.Lib.ComboBoxItem> listFisicoJuridico = new List<PS.Lib.ComboBoxItem>();

            listFisicoJuridico.Add(new PS.Lib.ComboBoxItem());
            listFisicoJuridico[0].ValueMember = 0;
            listFisicoJuridico[0].DisplayMember = "Jurídico";

            listFisicoJuridico.Add(new PS.Lib.ComboBoxItem());
            listFisicoJuridico[1].ValueMember = 1;
            listFisicoJuridico[1].DisplayMember = "Físico";

            cmbFisicoJuridico.DataSource = listFisicoJuridico;
            cmbFisicoJuridico.DisplayMember = "DisplayMember";
            cmbFisicoJuridico.ValueMember = "ValueMember";

            #endregion

            this.edita = true;
            this.lookup = lookup;
            Codtransportadora = lookup.ValorCodigoInterno;
            carregaCampos();
        }

        private void frmCadastroTransportadora_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodTransportadora.Enabled = false;
            }
            else
            {
                tbCodTransportadora.Text = setCodTransportadora();
                tbCodTransportadora.Enabled = false;

                chkAtivo.Checked = true;
            }
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VTRANSPORTADORA WHERE CODTRANSPORTADORA = ?", new object[] { Codtransportadora });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VTRANSPORTADORA WHERE CODTRANSPORTADORA = ?", new object[] { Codtransportadora });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodTransportadora.Text = dt.Rows[0]["CODTRANSPORTADORA"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            tbInscricaoEstadual.Text = dt.Rows[0]["INSCRICAOESTADUAL"].ToString();
            tbPis.Text = dt.Rows[0]["PIS"].ToString();
            tbNome.Text = dt.Rows[0]["NOME"].ToString();
            tbNomeFantasia.Text = dt.Rows[0]["NOMEFANTASIA"].ToString();
            cmbFisicoJuridico.SelectedValue = dt.Rows[0]["FISICOJURIDICO"];
            tbCgcCpf.Text = dt.Rows[0]["CGCCPF"].ToString();
            tbTelefone.Text = dt.Rows[0]["TELEFONE"].ToString();
            tbTelCelular.Text = dt.Rows[0]["TELCELULAR"].ToString();
            tbFax.Text = dt.Rows[0]["TELFAX"].ToString();
            tbHomePage.Text = dt.Rows[0]["HOMEPAGE"].ToString();
            tbEmail.Text = dt.Rows[0]["EMAIL"].ToString();
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

        private void cmbFisicoJuridico_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbCgcCpf.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;

            if (cmbFisicoJuridico.SelectedIndex == 0)
            {
                tbCgcCpf.Properties.Mask.EditMask = "00.000.000/0000-00";
            }
            else
            {
                tbCgcCpf.Properties.Mask.EditMask = "000.000.000-00";
            }
        }

        public string setCodTransportadora()
        {
            string Codtransportadora = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT ISNULL(MAX(IDLOG), 0) + 1 FROM GLOG WHERE CODEMPRESA = ? AND CODTABELA = 'VTRANSPORTADORA'", new object[] { AppLib.Context.Empresa }).ToString();
            return Codtransportadora;
        }

        private bool Salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VTRANSPORTADORA = new AppLib.ORM.Jit(conn, "VTRANSPORTADORA");
            conn.BeginTransaction();

            try
            {
                VTRANSPORTADORA.Set("CODEMPRESA", AppLib.Context.Empresa);
                VTRANSPORTADORA.Set("CODTRANSPORTADORA", tbCodTransportadora.Text);
                VTRANSPORTADORA.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                VTRANSPORTADORA.Set("INSCRICAOESTADUAL", tbInscricaoEstadual.Text);
                VTRANSPORTADORA.Set("PIS", tbPis.Text);
                VTRANSPORTADORA.Set("NOME", tbNome.Text);
                VTRANSPORTADORA.Set("NOMEFANTASIA", tbNomeFantasia.Text);
                VTRANSPORTADORA.Set("FISICOJURIDICO", cmbFisicoJuridico.SelectedValue);
                VTRANSPORTADORA.Set("CGCCPF", tbCgcCpf.Text);
                VTRANSPORTADORA.Set("TELEFONE", tbTelefone.Text);
                VTRANSPORTADORA.Set("TELCELULAR", tbTelCelular.Text);
                VTRANSPORTADORA.Set("TELFAX", tbFax.Text);
                VTRANSPORTADORA.Set("HOMEPAGE", tbHomePage.Text);
                VTRANSPORTADORA.Set("EMAIL", tbEmail.Text);
                VTRANSPORTADORA.Set("CONTATO", tbContato.Text);

                if (!string.IsNullOrEmpty(lpTipoRua.txtcodigo.Text))
                {
                    VTRANSPORTADORA.Set("CODTIPORUA", lpTipoRua.txtcodigo.Text);
                }
                else
                {
                    VTRANSPORTADORA.Set("CODTIPORUA", null);
                }

                VTRANSPORTADORA.Set("RUA", tbRua.Text);
                VTRANSPORTADORA.Set("NUMERO", tbNumero.Text);
                VTRANSPORTADORA.Set("COMPLEMENTO", tbComplemento.Text);

                if (!string.IsNullOrEmpty(lpTipoBairro.txtcodigo.Text))
                {
                    VTRANSPORTADORA.Set("CODTIPOBAIRRO", lpTipoBairro.txtcodigo.Text);
                }
                else
                {
                    VTRANSPORTADORA.Set("CODTIPOBAIRRO", null);
                }

                VTRANSPORTADORA.Set("BAIRRO", tbBairro.Text);
                VTRANSPORTADORA.Set("CEP", tbCep.Text);

                if (!string.IsNullOrEmpty(lpPais.txtcodigo.Text))
                {
                    VTRANSPORTADORA.Set("CODPAIS", lpPais.txtcodigo.Text);
                }
                else
                {
                    VTRANSPORTADORA.Set("CODPAIS", null);
                }

                if (!string.IsNullOrEmpty(lpEstado.txtcodigo.Text))
                {
                    VTRANSPORTADORA.Set("CODETD", lpEstado.txtcodigo.Text);
                }
                else
                {
                    VTRANSPORTADORA.Set("CODETD", null);
                }

                if (!string.IsNullOrEmpty(lpCidade.txtcodigo.Text))
                {
                    VTRANSPORTADORA.Set("CODCIDADE", lpCidade.txtcodigo.Text);
                }
                else
                {
                    VTRANSPORTADORA.Set("CODCIDADE", null);
                }
                VTRANSPORTADORA.Save();
                conn.Commit();

                if (edita == false)
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GLOG SET IDLOG = ? WHERE CODEMPRESA = ? AND CODTABELA = 'VTRANSPORTADORA'", new object[] { tbCodTransportadora.Text, AppLib.Context.Empresa });
                }
                              
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
