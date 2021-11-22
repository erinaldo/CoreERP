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
    public partial class frmCadastroRegraIpi : Form
    {
        public bool edita = false;
        public string IdRegra = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;
        public bool verifica = false;

        //Variaveis para NewLookup
        private NewLookup lookup;
        public frmCadastroRegraIpi()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VREGRAIPI");
            CarregaCombo();
        }
        public frmCadastroRegraIpi(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VREGRAIPI");
            this.edita = true;
            this.lookup = lookup;
            IdRegra = lookup.ValorCodigoInterno;
            CarregaCampos();
            CarregaCombo();
        }
        public void CarregaCombo()
        {
            #region Combobox

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = "D";
            list1[0].DisplayMember = "Diferido";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = "I";
            list1[1].DisplayMember = "Isento";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = "O";
            list1[2].DisplayMember = "Outros";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[3].ValueMember = "S";
            list1[3].DisplayMember = "Suspenso";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[4].ValueMember = "T";
            list1[4].DisplayMember = "Tributado";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[5].ValueMember = "M";
            list1[5].DisplayMember = "Tributado 50%";

            cmbTipoTributacao.DataSource = list1;
            cmbTipoTributacao.DisplayMember = "DisplayMember";
            cmbTipoTributacao.ValueMember = "ValueMember";

            #endregion
        }
        private void frmCadastroRegraIpi_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                CarregaCampos();
                tbIdRegra.Enabled = false;
                tbIdRegra.ReadOnly = true;
            }
            else
            {
                tbIdRegra.Enabled = false;
                tbIdRegra.ReadOnly = true;
            }
        }
        private void carregaObj(DataTable dt)
        {
            tbIdRegra.Text = dt.Rows[0]["IDREGRA"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            cmbTipoTributacao.SelectedValue = dt.Rows[0]["TIPOTRIBUTACAO"];
            tbEntrada.Text = dt.Rows[0]["CODCSTENT"].ToString();
            tbSaida.Text = dt.Rows[0]["CODCSTSAI"].ToString();
            tbEnquadramento.Text = dt.Rows[0]["CENQ"].ToString();
            chkValorProduto.Checked = Convert.ToBoolean(dt.Rows[0]["VALORPRODUTO"]);
            chkDesconto.Checked = Convert.ToBoolean(dt.Rows[0]["DESCONTO"]);
            chkDespesa.Checked = Convert.ToBoolean(dt.Rows[0]["DESPESA"]);
            chkFrete.Checked = Convert.ToBoolean(dt.Rows[0]["FRETE"]);
            chkSeguro.Checked = Convert.ToBoolean(dt.Rows[0]["SEGURO"]);
        }
        private void CarregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VREGRAIPI WHERE IDREGRA = ? AND CODEMPRESA = ?", new object[] { IdRegra, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VREGRAIPI WHERE IDREGRA = ? AND CODEMPRESA = ?", new object[] { IdRegra, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }
        private bool Salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VREGRAIPI = new AppLib.ORM.Jit(conn, "VREGRAIPI");
            conn.BeginTransaction();

            try
            {
                if (verifica == false)
                {

                    VREGRAIPI.Set("CODEMPRESA", AppLib.Context.Empresa);

                    if (edita == true)
                    {
                        VREGRAIPI.Set("IDREGRA", Convert.ToInt32(conn.ExecGetField(0, @"SELECT IDREGRA FROM VREGRAIPI WHERE CODEMPRESA = ? AND IDREGRA = ?", new object[] { AppLib.Context.Empresa, IdRegra })));
                    }
                    else
                    {
                        VREGRAIPI.Set("IDREGRA", Convert.ToInt32(conn.ExecGetField(0, @"SELECT (MAX(IDREGRA) + 1) AS IDREGRA FROM VREGRAIPI", new object[] { })));
                    }
                    VREGRAIPI.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                    VREGRAIPI.Set("DESCRICAO", tbDescricao.Text);
                    VREGRAIPI.Set("TIPOTRIBUTACAO", cmbTipoTributacao.SelectedValue);
                    VREGRAIPI.Set("CODCSTENT", tbEntrada.Text);
                    VREGRAIPI.Set("CODCSTSAI", tbSaida.Text);
                    VREGRAIPI.Set("CENQ", tbEnquadramento.Text);
                    VREGRAIPI.Set("VALORPRODUTO", chkValorProduto.Checked == true ? 1 : 0);
                    VREGRAIPI.Set("DESCONTO", chkDesconto.Checked == true ? 1 : 0);
                    VREGRAIPI.Set("DESPESA", chkDespesa.Checked == true ? 1 : 0);
                    VREGRAIPI.Set("FRETE", chkFrete.Checked == true ? 1 : 0);
                    VREGRAIPI.Set("SEGURO", chkSeguro.Checked == true ? 1 : 0);
                    VREGRAIPI.Save();
                    conn.Commit();
                }
                else
                {
                    this.Dispose();
                }
                verifica = true;
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
    }
}
