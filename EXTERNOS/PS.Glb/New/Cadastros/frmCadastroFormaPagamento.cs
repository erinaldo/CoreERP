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
    public partial class frmCadastroFormaPagamento : Form
    {
        public bool edita = false;
        public string CodForma = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroFormaPagamento()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VFORMAPGTO");

            #region Combobox

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 1;
            list1[0].DisplayMember = "Dinheiro";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 2;
            list1[1].DisplayMember = "Cheque";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = 3;
            list1[2].DisplayMember = "Cartão de Crédito";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[3].ValueMember = 4;
            list1[3].DisplayMember = "Cartão de Débito";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[4].ValueMember = 5;
            list1[4].DisplayMember = "Crédito Loja";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[5].ValueMember = 10;
            list1[5].DisplayMember = "Vale Alimentação";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[6].ValueMember = 11;
            list1[6].DisplayMember = "Vale Refeição";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[7].ValueMember = 12;
            list1[7].DisplayMember = "Vale Presente";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[8].ValueMember = 13;
            list1[8].DisplayMember = "Vale Combustível";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[9].ValueMember = 15;
            list1[9].DisplayMember = "Boleto Bancário";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[10].ValueMember = 90;
            list1[10].DisplayMember = "Sem Pagamento";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[11].ValueMember = 99;
            list1[11].DisplayMember = "Outros";

            cmbTipo.DataSource = list1;
            cmbTipo.DisplayMember = "DisplayMember";
            cmbTipo.ValueMember = "ValueMember";

            #endregion
        }

        public frmCadastroFormaPagamento(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VFORMAPGTO");
            this.edita = true;
            this.lookup = lookup;
            CodForma = lookup.ValorCodigoInterno;
            carregaCampos();

            #region Combobox

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 1;
            list1[0].DisplayMember = "Dinheiro";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 2;
            list1[1].DisplayMember = "Cheque";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = 3;
            list1[2].DisplayMember = "Cartão de Crédito";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[3].ValueMember = 4;
            list1[3].DisplayMember = "Cartão de Débito";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[4].ValueMember = 5;
            list1[4].DisplayMember = "Crédito Loja";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[5].ValueMember = 10;
            list1[5].DisplayMember = "Vale Alimentação";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[6].ValueMember = 11;
            list1[6].DisplayMember = "Vale Refeição";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[7].ValueMember = 12;
            list1[7].DisplayMember = "Vale Presente";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[8].ValueMember = 13;
            list1[8].DisplayMember = "Vale Combustível";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[9].ValueMember = 15;
            list1[9].DisplayMember = "Boleto Bancário";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[10].ValueMember = 90;
            list1[10].DisplayMember = "Sem Pagamento";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[11].ValueMember = 99;
            list1[11].DisplayMember = "Outros";

            cmbTipo.DataSource = list1;
            cmbTipo.DisplayMember = "DisplayMember";
            cmbTipo.ValueMember = "ValueMember";

            #endregion
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.Text == "Cartão de Crédito")
            {
                lpCartao.Enabled = true;
            }
            else if(cmbTipo.Text == "Cartão de Débito")
            {
                lpCartao.Enabled = true;
            }
            else
            {
                lpCartao.Enabled = false;
            }
        }

        private void frmCadastroFormaPagamento_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodForma.Enabled = false;
            }
            else
            {
                cmbTipo.SelectedValue = 99;
                chkAtivo.Checked = true;
            }
        }

        private bool validacoes()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(tbCodForma.Text))
            {
                errorProvider1.SetError(tbCodForma, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VFORMAPGTO WHERE CODFORMA = ?", new object[] { CodForma });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VFORMAPGTO WHERE CODFORMA = ?", new object[] { CodForma });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodForma.Text = dt.Rows[0]["CODFORMA"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            tbNome.Text = dt.Rows[0]["NOME"].ToString();
            cmbTipo.SelectedValue = dt.Rows[0]["TIPO"];
            lpCartao.txtcodigo.Text = dt.Rows[0]["CODREDECARTAO"].ToString();
            lpCartao.CarregaDescricao();
        }

        private bool Salvar()
        {
            if (validacoes() == false)
            {
                return false;
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VFORMAPGTO = new AppLib.ORM.Jit(conn, "VFORMAPGTO");
            conn.BeginTransaction();

            try
            {
                VFORMAPGTO.Set("CODEMPRESA", AppLib.Context.Empresa);
                VFORMAPGTO.Set("CODFORMA", tbCodForma.Text);
                VFORMAPGTO.Set("NOME", tbNome.Text);
                VFORMAPGTO.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                VFORMAPGTO.Set("TIPO", cmbTipo.SelectedValue);

                if (!string.IsNullOrEmpty(lpCartao.ValorCodigoInterno))
                {
                    VFORMAPGTO.Set("CODREDECARTAO", lpCartao.ValorCodigoInterno);
                }
                else
                {
                    VFORMAPGTO.Set("CODREDECARTAO", null);
                }

                VFORMAPGTO.Save();
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
            tbCodForma.Select();
            tbCodForma.Text = string.Empty;
            chkAtivo.Checked = false;
            tbNome.Text = string.Empty;
            cmbTipo.SelectedIndex = 0;
        }
    }
}
