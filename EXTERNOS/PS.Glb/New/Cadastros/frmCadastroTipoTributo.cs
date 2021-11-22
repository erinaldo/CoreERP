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
    public partial class frmCadastroTipoTributo : Form
    {
        public bool edita = false;
        public string CodTipoTributo = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;
        public frmCadastroTipoTributo()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VTIPOTRIBUTO");
            CarregaCombo();
        }
        public frmCadastroTipoTributo(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VTIPOTRIBUTO");
            this.edita = true;
            this.lookup = lookup;
            CodTipoTributo = lookup.ValorCodigoInterno;
            CarregaCombo();
            CarregaCampos();          
        }
        public void CarregaCombo()
        {
            #region Combobox

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = "M";
            list1[0].DisplayMember = "Municipal";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = "E";
            list1[1].DisplayMember = "Estadual";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = "F";
            list1[2].DisplayMember = "Federal";

            cmbAbrangencia.DataSource = list1;
            cmbAbrangencia.DisplayMember = "DisplayMember";
            cmbAbrangencia.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = "M";
            list2[0].DisplayMember = "Mensal";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = "S";
            list2[1].DisplayMember = "Semanal";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[2].ValueMember = "Q";
            list2[2].DisplayMember = "Quinzenal";

            cmbPeriodicidade.DataSource = list2;
            cmbPeriodicidade.DisplayMember = "DisplayMember";
            cmbPeriodicidade.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list3 = new List<PS.Lib.ComboBoxItem>();

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[0].ValueMember = 0;
            list3[0].DisplayMember = "Fixa";

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[1].ValueMember = 1;
            list3[1].DisplayMember = "Variável";

            cmbAliquota.DataSource = list3;
            cmbAliquota.DisplayMember = "DisplayMember";
            cmbAliquota.ValueMember = "ValueMember";

            #endregion
        }
        private void frmCadastroTipoTributo_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                CarregaCampos();
                tbCodTipoTributo.Enabled = false;
            }
        }
        private bool validações()
        {
            bool verifica = true;

            errorProvider.Clear();

            if (string.IsNullOrEmpty(tbCodTipoTributo.Text))
            {
                errorProvider.SetError(tbCodTipoTributo, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }
        private void carregaObj(DataTable dt)
        {
            tbCodTipoTributo.Text = dt.Rows[0]["CODTIPOTRIBUTO"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);           
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            cmbAliquota.SelectedValue = dt.Rows[0]["TIPOALIQUOTA"];
            cmbAbrangencia.SelectedValue = dt.Rows[0]["ABRANGENCIA"];
            cmbPeriodicidade.SelectedValue = dt.Rows[0]["PERIODICIDADE"];
            if (!string.IsNullOrEmpty(dt.Rows[0]["DTINIVIGENCIA"].ToString()))
            {
               dtInicio.DateTime = Convert.ToDateTime(dt.Rows[0]["DTINIVIGENCIA"]);
            }
            if (!string.IsNullOrEmpty(dt.Rows[0]["DTFIMVIGENCIA"].ToString()))
            {
                dtFim.DateTime = Convert.ToDateTime(dt.Rows[0]["DTFIMVIGENCIA"]);
            }
        }
        private void CarregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VTIPOTRIBUTO WHERE CODTIPOTRIBUTO = ? AND CODEMPRESA = ?", new object[] { CodTipoTributo, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VTIPOTRIBUTO WHERE CODTIPOTRIBUTO = ? AND CODEMPRESA = ?", new object[] { CodTipoTributo, AppLib.Context.Empresa });
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
            AppLib.ORM.Jit VTIPOTRIBUTO = new AppLib.ORM.Jit(conn, "VTIPOTRIBUTO");
            conn.BeginTransaction();

            try
            {
                VTIPOTRIBUTO.Set("CODEMPRESA", AppLib.Context.Empresa);
                VTIPOTRIBUTO.Set("CODTIPOTRIBUTO", tbCodTipoTributo.Text);
                VTIPOTRIBUTO.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                VTIPOTRIBUTO.Set("DESCRICAO", tbDescricao.Text);
                VTIPOTRIBUTO.Set("TIPOALIQUOTA", cmbAliquota.SelectedValue);
                VTIPOTRIBUTO.Set("ABRANGENCIA", cmbAbrangencia.SelectedValue);
                VTIPOTRIBUTO.Set("PERIODICIDADE", cmbPeriodicidade.SelectedValue);
                if (!string.IsNullOrEmpty(dtInicio.Text))
                {
                    VTIPOTRIBUTO.Set("DTINIVIGENCIA", Convert.ToDateTime(dtInicio.Text));
                }
                else
                {
                    VTIPOTRIBUTO.Set("DTINIVIGENCIA", null);
                }
                if (!string.IsNullOrEmpty(dtFim.Text))
                {
                    VTIPOTRIBUTO.Set("DTFIMVIGENCIA", Convert.ToDateTime(dtFim.Text));
                }
                else
                {
                    VTIPOTRIBUTO.Set("DTFIMVIGENCIA", null);
                }
                VTIPOTRIBUTO.Save();
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
    }
}
