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
    public partial class frmCadastroTributos : Form
    {
        public bool edita = false;
        public string CodTributo = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;
        public frmCadastroTributos()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VTRIBUTO");

            #region
            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();
            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 0;
            list1[0].DisplayMember = "Tributo";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 1;
            list1[1].DisplayMember = "Produto";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = 2;
            list1[2].DisplayMember = "Tipo Operação";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[3].ValueMember = 3;
            list1[3].DisplayMember = "Natureza";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[4].ValueMember = 4;
            list1[4].DisplayMember = "Estado";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[5].ValueMember = 5;
            list1[5].DisplayMember = "Regra";

            cmbAliquota.DataSource = list1;
            cmbAliquota.DisplayMember = "DisplayMember";
            cmbAliquota.ValueMember = "ValueMember";
            #endregion
        }
        public frmCadastroTributos(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VTRIBUTO");
            this.edita = true;
            this.lookup = lookup;
            CodTributo = lookup.ValorCodigoInterno;
            CarregaCampos();
        }
        private void frmCadastroTributos_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                CarregaCampos();
                tbCodTributo.Enabled = false;
            }
        }
        private bool validacoes()
        {
            bool verifica = true;

            errorProvider.Clear();

            if (string.IsNullOrEmpty(tbCodTributo.Text))
            {
                errorProvider.SetError(tbCodTributo, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }
        private void carregaObj(DataTable dt)
        {
            tbCodTributo.Text = dt.Rows[0]["CODTRIBUTO"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            cmbAliquota.SelectedValue = dt.Rows[0]["ALIQUOTAEM"];
            tbAliquota.Text = dt.Rows[0]["ALIQUOTA"].ToString();
            lpTipoTributo.txtcodigo.Text = dt.Rows[0]["CODTIPOTRIBUTO"].ToString();
            lpTipoTributo.CarregaDescricao();
        }
        private void CarregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VTRIBUTO WHERE CODTRIBUTO = ? AND CODEMPRESA = ?", new object[] { CodTributo, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VTRIBUTO WHERE CODTRIBUTO = ? AND CODEMPRESA = ?", new object[] { CodTributo, AppLib.Context.Empresa});
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }
        private bool Salvar()
        {
            if (validacoes() == false)
            {
                return false;
            }
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VTRIBUTO = new AppLib.ORM.Jit(conn, "VTRIBUTO");
            conn.BeginTransaction();

            try
            { 
                VTRIBUTO.Set("CODEMPRESA", AppLib.Context.Empresa);
                VTRIBUTO.Set("CODTRIBUTO", tbCodTributo.Text);
                VTRIBUTO.Set("ATIVO", chkAtivo.Checked == true? 1 : 0);
                VTRIBUTO.Set("DESCRICAO", tbDescricao.Text);
                VTRIBUTO.Set("ALIQUOTAEM", cmbAliquota.SelectedValue);
                VTRIBUTO.Set("ALIQUOTA", Convert.ToDecimal(tbAliquota.Text));
                // VERIFICAR SE É DECIMAL
                if (!string.IsNullOrEmpty(lpTipoTributo.ValorCodigoInterno))
                {
                    VTRIBUTO.Set("CODTIPOTRIBUTO", lpTipoTributo.ValorCodigoInterno);
                }
                else
                {
                    VTRIBUTO.Set("CODTIPOTRIBUTO", null);
                }
                VTRIBUTO.Save();
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
            tbCodTributo.Select();
            tbCodTributo.Text = string.Empty;
            chkAtivo.Checked = false;
            tbDescricao.Text = string.Empty;
            cmbAliquota.SelectedIndex = 0;
            tbAliquota.Text = "0,00";
            lpTipoTributo.Clear();
        }
    }
}
