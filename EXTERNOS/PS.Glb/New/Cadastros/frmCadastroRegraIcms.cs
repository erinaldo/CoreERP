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
    public partial class frmCadastroRegraIcms : Form
    {
        public bool edita = false;
        public string IdRegra = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;
        public bool verifica = false;

        //Variaveis para NewLookup
        private NewLookup lookup;
        public frmCadastroRegraIcms()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VREGRAICMS");
            CarregaCombo();
        }
        public frmCadastroRegraIcms(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VREGRAICMS");
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
            list1[0].ValueMember = "";
            list1[0].DisplayMember = "";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = "A";
            list1[1].DisplayMember = "MVA Ajustado";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = "I";
            list1[2].DisplayMember = "MVA Importado";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[3].ValueMember = "O";
            list1[3].DisplayMember = "MVA Original";

            cmbSelecaoMva.DataSource = list1;
            cmbSelecaoMva.DisplayMember = "DisplayMember";
            cmbSelecaoMva.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();
            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = "D";
            list2[0].DisplayMember = "Diferido";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = "I";
            list2[1].DisplayMember = "Isento";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[2].ValueMember = "O";
            list2[2].DisplayMember = "Outros";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[3].ValueMember = "S";
            list2[3].DisplayMember = "Suspenso";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[4].ValueMember = "T";
            list2[4].DisplayMember = "Tributado";

            cmbTributacao.DataSource = list2;
            cmbTributacao.DisplayMember = "DisplayMember";
            cmbTributacao.ValueMember = "ValueMember";
            #endregion
        }
        private void frmCadastroRegraIcms_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                //
                CarregaCampos();
                tbIdRegra.Enabled = false;
                tbPercentualUf.Enabled = false;
                chkConsideraReducao.Enabled = false;
                groupBox2.Enabled = false;
                tbReducaoBaseCalculo.Enabled = false;
                cmbSelecaoMva.Enabled = false;
                //
                if (cmbTributacao.Text != "Diferido")
                {
                    chkTagReduzida.Enabled = false;
                    tbValorDiferimento.Enabled = false;
                }
                else
                {
                    chkTagReduzida.Enabled = true;
                    tbValorDiferimento.Enabled = true;
                }
            }
            else
            {
                if (cmbTributacao.Text == "Diferido")
                {
                    chkTagReduzida.Enabled = true;
                    tbValorDiferimento.Enabled = true;
                }
                tbIdRegra.Enabled = false;
                tbIdRegra.ReadOnly = true;
                tbPercentualUf.Enabled = false;
                chkConsideraReducao.Enabled = false;
                groupBox2.Enabled = false;
                tbReducaoBaseCalculo.Enabled = false;
                cmbSelecaoMva.Enabled = false;
            }
        }
        #region Evento Changed
        private void chkDiferencialAliquota_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDiferencialAliquota.Checked == false)
            {
                chkConsideraReducao.Enabled = false;
                tbPercentualUf.Enabled = false;
            }
            else
            {
                chkConsideraReducao.Enabled = true;
                tbPercentualUf.Enabled = true;
            }
        }
        private void chkUtilizaSt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUtilizaSt.Checked == false)
            {
                cmbSelecaoMva.Enabled = false;
                tbReducaoBaseCalculo.Enabled = false;
                groupBox2.Enabled = false;
            }
            else
            {
                cmbSelecaoMva.Enabled = true;
                tbReducaoBaseCalculo.Enabled = true;
                groupBox2.Enabled = true;
            }
        }
        private void cmbTributacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTributacao.Text != "Diferido")
            {
                chkTagReduzida.Enabled = false;
                tbValorDiferimento.Enabled = false;
            }
            else
            {
                chkTagReduzida.Enabled = true;
                tbValorDiferimento.Enabled = true;
            }
        }
        #endregion
        private void carregaObj(DataTable dt)
        {
            tbIdRegra.Text = dt.Rows[0]["IDREGRA"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            chkCreditoIcms.Checked = Convert.ToBoolean(dt.Rows[0]["CREDITOICMS"]);
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            cmbTributacao.SelectedValue = dt.Rows[0]["TIPOTRIBUTACAO"];
            tbSituacaoTributaria.Text = dt.Rows[0]["CODCST"].ToString();
            tbReducaoBaseIcms.Text = dt.Rows[0]["REDUCAOBASEICMS"].ToString();
            tbAliquota.Text = dt.Rows[0]["ALIQUOTA"].ToString();
            chkTagReduzida.Checked = Convert.ToBoolean(dt.Rows[0]["TAGREDUZIDA"]);
            tbValorDiferimento.Text = dt.Rows[0]["VALORDIFERIMENTO"].ToString();
            chkDiferencialAliquota.Checked = Convert.ToBoolean(dt.Rows[0]["DIFERENCIALALIQUOTA"]);
            chkConsideraReducao.Checked = Convert.ToBoolean(dt.Rows[0]["CONSIDERAREDUCAOCALCULOPROPRIO"]);
            tbPercentualUf.Text = dt.Rows[0]["PERCICMSUFDEST"].ToString();
            chkValorProdutoIcms.Checked = Convert.ToBoolean(dt.Rows[0]["VALORPRODUTOICMS"]);
            chkDescontoIcms.Checked = Convert.ToBoolean(dt.Rows[0]["DESCONTOICMS"]);
            chkDespesaIcms.Checked = Convert.ToBoolean(dt.Rows[0]["DESPESAIMCS"]);
            chkFreteIcms.Checked = Convert.ToBoolean(dt.Rows[0]["FRETEIMCS"]);
            chkIpiIcms.Checked = Convert.ToBoolean(dt.Rows[0]["IPIICMS"]);
            chkSeguroIcms.Checked = Convert.ToBoolean(dt.Rows[0]["SEGUROICMS"]);
            chkUtilizaSt.Checked = Convert.ToBoolean(dt.Rows[0]["UTILIZAST"]);
            tbReducaoBaseCalculo.Text = dt.Rows[0]["REDUCAOBCST"].ToString();
            cmbSelecaoMva.SelectedValue = dt.Rows[0]["SELECAOMVAST"];
            chkValorProdutoSt.Checked = Convert.ToBoolean(dt.Rows[0]["VALORPRODUTOST"]);
            chkDescontoSt.Checked = Convert.ToBoolean(dt.Rows[0]["DESCONTOST"]);
            chkDespesaSt.Checked = Convert.ToBoolean(dt.Rows[0]["DESPESAST"]);
            chkFreteSt.Checked = Convert.ToBoolean(dt.Rows[0]["FRETEST"]);
            chkSeguroSt.Checked = Convert.ToBoolean(dt.Rows[0]["SEGUROST"]);
            chkIpiSt.Checked = Convert.ToBoolean(dt.Rows[0]["IPIST"]);
        }
        private void CarregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VREGRAICMS WHERE IDREGRA = ? AND CODEMPRESA = ?", new object[] { IdRegra, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VREGRAICMS WHERE IDREGRA = ? AND CODEMPRESA = ?", new object[] { IdRegra, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }
        private bool Salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VREGRAICMS = new AppLib.ORM.Jit(conn, "VREGRAICMS");
            conn.BeginTransaction();

            try
            {
                if (verifica == false)
                {
                    VREGRAICMS.Set("CODEMPRESA", AppLib.Context.Empresa);
                    if (edita == true)
                    {
                        VREGRAICMS.Set("IDREGRA", Convert.ToInt32(conn.ExecGetField(0, @"SELECT IDREGRA FROM VREGRAICMS WHERE CODEMPRESA = ? AND IDREGRA = ?", new object[] { AppLib.Context.Empresa, IdRegra })));
                    }
                    else
                    {
                        VREGRAICMS.Set("IDREGRA", Convert.ToInt32(conn.ExecGetField(0, @"SELECT (MAX(IDREGRA) + 1) AS IDREGRA FROM VREGRAICMS", new object[] { })));
                    }
                    VREGRAICMS.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("CREDITOICMS", chkCreditoIcms.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("VREGRAICMS", chkCreditoIcms.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("DESCRICAO", tbDescricao.Text);
                    VREGRAICMS.Set("TIPOTRIBUTACAO", cmbTributacao.SelectedValue);
                    VREGRAICMS.Set("CODCST", tbSituacaoTributaria.Text);
                    VREGRAICMS.Set("REDUCAOBASEICMS", Convert.ToDecimal(tbReducaoBaseIcms.Text));
                    VREGRAICMS.Set("ALIQUOTA", Convert.ToDecimal(tbAliquota.Text));
                    VREGRAICMS.Set("TAGREDUZIDA", chkTagReduzida.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("VALORDIFERIMENTO", Convert.ToDecimal(tbValorDiferimento.Text));
                    VREGRAICMS.Set("DIFERENCIALALIQUOTA", chkDiferencialAliquota.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("CONSIDERAREDUCAOCALCULOPROPRIO", chkConsideraReducao.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("PERCICMSUFDEST", Convert.ToDecimal(tbPercentualUf.Text));
                    VREGRAICMS.Set("VALORPRODUTOICMS", chkValorProdutoIcms.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("DESCONTOICMS", chkDescontoIcms.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("DESPESAIMCS", chkDespesaIcms.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("FRETEIMCS", chkFreteIcms.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("IPIICMS", chkIpiIcms.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("SEGUROICMS", chkSeguroIcms.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("UTILIZAST", chkUtilizaSt.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("REDUCAOBCST", Convert.ToDecimal(tbReducaoBaseCalculo.Text));
                    VREGRAICMS.Set("SELECAOMVAST", cmbSelecaoMva.SelectedValue);
                    VREGRAICMS.Set("VALORPRODUTOST", chkValorProdutoSt.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("DESCONTOST", chkDescontoSt.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("DESPESAST", chkDespesaSt.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("FRETEST", chkFreteSt.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("IPIST", chkIpiSt.Checked == true ? 1 : 0);
                    VREGRAICMS.Set("SEGUROST", chkSeguroSt.Checked == true ? 1 : 0);
                    VREGRAICMS.Save();
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
