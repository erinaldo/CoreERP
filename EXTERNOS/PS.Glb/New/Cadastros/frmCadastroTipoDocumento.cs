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
    public partial class frmCadastroTipoDocumento : Form
    {
        public bool edita = false;
        public string CodTipDoc = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroTipoDocumento()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "FTIPDOC");

            #region Combobox

            List<PS.Lib.ComboBoxItem> list1 = new List<Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem(0, "Pagar"));
            list1.Add(new PS.Lib.ComboBoxItem(1, "Receber"));

            cmbPagRec.DataSource = list1;
            cmbPagRec.DisplayMember = "DisplayMember";
            cmbPagRec.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list2 = new List<Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem(0, "Adiantamento"));
            list2.Add(new PS.Lib.ComboBoxItem(1, "Devolução"));
            list2.Add(new PS.Lib.ComboBoxItem(2, "Sem Classificação"));
            list2.Add(new PS.Lib.ComboBoxItem(3, "Previsão"));

            cmbClassificacao.DataSource = list2;
            cmbClassificacao.DisplayMember = "DisplayMember";
            cmbClassificacao.ValueMember = "ValueMember";

            #endregion
        }
        public frmCadastroTipoDocumento(ref NewLookup lookup)
        {
            this.edita = true;
            this.lookup = lookup;
        }
        private void frmCadastroTipoDocumento_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                CarregaCampos();
                tbCodTipDoc.Enabled = false;
                string mascara = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT MAX(ULTIMONUMERO) FROM FTIPDOC WHERE CODEMPRESA = ? AND CODTIPDOC = ?", new object[] { AppLib.Context.Empresa, CodTipDoc }).ToString();
                tbUltimoNumero.Text = mascara;
            }
            else
            {
                string mascara = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT REPLICATE(0, VPARAMETROS.MASKNUMEROSEQ) FROM VPARAMETROS WHERE VPARAMETROS.CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();
                tbUltimoNumero.Text = mascara;
                chkAtivo.Checked = true;
                cmbClassificacao.SelectedValue = 2;
            }
        }
        private bool validações()
        {
            bool verifica = true;

            errorProvider.Clear();

            if (string.IsNullOrEmpty(tbCodTipDoc.Text))
            {
                errorProvider.SetError(tbCodTipDoc, "Favor informar o código.");
                verifica = false;
            }

            return verifica;
        }
        private void carregaObj(DataTable dt)
        {
            tbCodTipDoc.Text = dt.Rows[0]["CODTIPDOC"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            tbNomeDoc.Text = dt.Rows[0]["NOME"].ToString();
            chkUsaNumero.Checked = Convert.ToBoolean(dt.Rows[0]["USANUMEROSEQ"]);
            tbUltimoNumero.Text = dt.Rows[0]["ULTIMONUMERO"].ToString();
            cmbPagRec.SelectedValue = dt.Rows[0]["PAGREC"];
            cmbClassificacao.SelectedValue = dt.Rows[0]["CLASSIFICACAO"];
            chkGeraBoleto.Checked = Convert.ToBoolean(dt.Rows[0]["GERABOLETO"]);
        }
        private void CarregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FTIPDOC WHERE CODTIPDOC = ? AND CODEMPRESA = ?", new object[] { CodTipDoc, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FTIPDOC WHERE CODTIPDOC = ? AND CODEMPRESA = ?", new object[] { CodTipDoc, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        } 
        private bool Salvar()
        {
            if (validações() == false)
            {
                return false;
            }
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit FTIPDOC = new AppLib.ORM.Jit(conn, "FTIPDOC");
            conn.BeginTransaction();

            try
            {
                FTIPDOC.Set("CODEMPRESA", AppLib.Context.Empresa);
                FTIPDOC.Set("CODTIPDOC", tbCodTipDoc.Text);
                FTIPDOC.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                FTIPDOC.Set("NOME", tbNomeDoc.Text);
                FTIPDOC.Set("USANUMEROSEQ", chkUsaNumero.Checked == true ? 1 : 0);
                FTIPDOC.Set("ULTIMONUMERO", tbUltimoNumero.Text);
                FTIPDOC.Set("PAGREC", cmbPagRec.SelectedValue);
                FTIPDOC.Set("CLASSIFICACAO", cmbClassificacao.SelectedValue);
                FTIPDOC.Set("GERABOLETO", chkGeraBoleto.Checked == true ? 1 : 0);
                FTIPDOC.Save();
                conn.Commit();

                return true;
            }
            catch (Exception ex)
            {
                conn.Rollback();
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
                    CarregaCampos();
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
            tbCodTipDoc.Select();
            tbCodTipDoc.Text = string.Empty;
            chkAtivo.Checked = false;
            tbNomeDoc.Text = string.Empty;
            chkUsaNumero.Checked = false;
            tbUltimoNumero.Text = string.Empty;
            cmbPagRec.SelectedIndex = 0;
            cmbClassificacao.SelectedIndex = 0;
            chkGeraBoleto.Checked = false;
        }

        private void chkUsaNumero_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUsaNumero.Checked == true)
            {
                tbUltimoNumero.Enabled = false;
            }
            else
            {
                tbUltimoNumero.Enabled = true;
            }
        }
    }
}
