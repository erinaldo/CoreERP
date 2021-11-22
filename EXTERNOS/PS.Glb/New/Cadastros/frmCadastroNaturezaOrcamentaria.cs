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
    public partial class frmCadastroNaturezaOrcamentaria : Form
    {
        public bool edita = false;
        public string CodNatureza = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;
        public frmCadastroNaturezaOrcamentaria()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VNATUREZAORCAMENTO");

            #region Combobox

            List<PS.Lib.ComboBoxItem> list1 = new List<Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem(0, "Entrada"));
            list1.Add(new PS.Lib.ComboBoxItem(1, "Saida"));

            cmbNatureza.DataSource = list1;
            cmbNatureza.DisplayMember = "DisplayMember";
            cmbNatureza.ValueMember = "ValueMember";

            #endregion
        }
        public frmCadastroNaturezaOrcamentaria(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VNATUREZAORCAMENTO");

            #region Combobox

            List<PS.Lib.ComboBoxItem> list1 = new List<Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem(0, "Entrada"));
            list1.Add(new PS.Lib.ComboBoxItem(1, "Saida"));

            cmbNatureza.DataSource = list1;
            cmbNatureza.DisplayMember = "DisplayMember";
            cmbNatureza.ValueMember = "ValueMember";

            #endregion

            this.edita = true;
            this.lookup = lookup;
            CodNatureza = lookup.ValorCodigoInterno;
            CarregaCampos();
        }
        private void frmCadastroNaturezaOrcamentaria_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                CarregaCampos();
                tbCodNatureza.Enabled = false;
            }
        }
        private bool validações()
        {
            bool verifica = true;

            errorProvider.Clear();

            if (string.IsNullOrEmpty(tbCodNatureza.Text))
            {
                errorProvider.SetError(tbCodNatureza, "Favor informar o código.");
                //MessageBox.Show("Favor informar o código.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                verifica = false;
            }

            return verifica;
        }
        private void carregaObj(DataTable dt)
        {
            tbCodNatureza.Text = dt.Rows[0]["CODNATUREZA"].ToString();
            tbDescricaoNatureza.Text = dt.Rows[0]["DESCRICAO"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            chkPermiteLancamento.Checked = Convert.ToBoolean(dt.Rows[0]["PERMITELANCAMENTO"]);
            chkControleFiscal.Checked = Convert.ToBoolean(dt.Rows[0]["CONTROLEFISCAL"]);
            cmbNatureza.SelectedValue = dt.Rows[0]["NATUREZA"];
        }
        private void CarregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VNATUREZAORCAMENTO WHERE CODNATUREZA = ? AND CODEMPRESA = ?", new object[] { CodNatureza, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VNATUREZAORCAMENTO WHERE CODNATUREZA = ? AND CODEMPRESA = ?", new object[] { CodNatureza, AppLib.Context.Empresa });
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
            AppLib.ORM.Jit VNATUREZAORCAMENTO = new AppLib.ORM.Jit(conn, "VNATUREZAORCAMENTO");
            conn.BeginTransaction();

            try
            {
                VNATUREZAORCAMENTO.Set("CODEMPRESA", AppLib.Context.Empresa);
                VNATUREZAORCAMENTO.Set("CODNATUREZA", tbCodNatureza.Text);
                VNATUREZAORCAMENTO.Set("DESCRICAO", tbDescricaoNatureza.Text);
                VNATUREZAORCAMENTO.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                VNATUREZAORCAMENTO.Set("PERMITELANCAMENTO", chkPermiteLancamento.Checked == true ? 1 : 0);
                VNATUREZAORCAMENTO.Set("CONTROLEFISCAL",chkControleFiscal.Checked == true ? 1 : 0);
                VNATUREZAORCAMENTO.Set("NATUREZA", cmbNatureza.SelectedValue);
                VNATUREZAORCAMENTO.Save();
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
            tbCodNatureza.Select();
            tbCodNatureza.Text = string.Empty;
            chkAtivo.Checked = false;
            chkPermiteLancamento.Checked = false;
            tbDescricaoNatureza.Text = string.Empty;
            cmbNatureza.SelectedIndex = 0;
            chkControleFiscal.Checked = false;
        }
    }
}
