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
    public partial class frmCadastroRegraTributacao : Form
    {
        public bool edita = false;
        public string CodEstado = string.Empty;
        public string CodRegiao = string.Empty;
        public string NseqRegra = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;
        public string _Codnatureza = string.Empty;
        public bool verifica = false;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroRegraTributacao()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VNATUREZAREGRATRIBUTACAO");
            CarregaCombo();
            //
            lpEstado.Enabled = false;
            lpRegiao.Enabled = false;
            //
        }
        public frmCadastroRegraTributacao(ref NewLookup lookup)
        {
            InitializeComponent();
            this.edita = true;
            this.lookup = lookup;
            CodEstado = lookup.ValorCodigoInterno;
            CodRegiao = lookup.ValorCodigoInterno;
        }

        private void frmCadastroRegraTributacao_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                CarregaCampos();
            }
        }

        private bool validações()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(lpEstado.ValorCodigoInterno) & string.IsNullOrEmpty(lpRegiao.ValorCodigoInterno))
            {
                errorProvider1.SetError(cbTipoRegra, "Informe pelo menos um tipo de regra.");
                verifica = false;
            }
            else
            {
                verifica = true;
            }
            return verifica;
        }

        public void CarregaCombo()
        {
            List<PS.Lib.ComboBoxItem> list = new List<PS.Lib.ComboBoxItem>();

            list.Add(new PS.Lib.ComboBoxItem());
            list[0].ValueMember = 0;
            list[0].DisplayMember = string.Empty;

            list.Add(new PS.Lib.ComboBoxItem());
            list[1].ValueMember = 1;
            list[1].DisplayMember = "Estado";

            list.Add(new PS.Lib.ComboBoxItem());
            list[2].ValueMember = 2;
            list[2].DisplayMember = "Região";

            cbTipoRegra.DataSource = list;
            cbTipoRegra.DisplayMember = "DisplayMember";
            cbTipoRegra.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = "S";
            list1[0].DisplayMember = "Sim";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = "N";
            list1[1].DisplayMember = "Não";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = "D";
            list1[2].DisplayMember = "Depende";

            cbContribuinte.DataSource = list1;
            cbContribuinte.DisplayMember = "DisplayMember";
            cbContribuinte.ValueMember = "ValueMember";
        }

        private void cbTipoRegra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbTipoRegra.Text))
            {
                lpEstado.Enabled = false;
                lpEstado.txtcodigo.Text = string.Empty;
                lpEstado.txtconteudo.Text = string.Empty;
                lpRegiao.Enabled = false;
                lpRegiao.txtcodigo.Text = string.Empty;
                lpRegiao.txtconteudo.Text = string.Empty;
            }
            else if (cbTipoRegra.Text == "Região")
            {
                lpRegiao.Enabled = true;
                lpEstado.Enabled = false;
                lpEstado.Clear();
            }
            else
            {
                lpRegiao.Enabled = false;
                lpEstado.Enabled = true;
                lpRegiao.Clear();
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbIdentificador.Text = dt.Rows[0]["NSEQREGRA"].ToString();
            cbTipoRegra.SelectedValue = dt.Rows[0]["TIPOREGRA"];
            lpEstado.txtcodigo.Text = dt.Rows[0]["CODETD"].ToString();
            lpEstado.CarregaDescricao();
            lpRegiao.txtcodigo.Text = dt.Rows[0]["CODREGIAO"].ToString();
            lpRegiao.CarregaDescricao();
            cbContribuinte.SelectedValue = dt.Rows[0]["CONTRIBUINTE"];
        }
        private void CarregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VNATUREZAREGRATRIBUTACAO WHERE CODNATUREZA = ? AND CODEMPRESA = ? AND NSEQREGRA = ?", new object[] { _Codnatureza, AppLib.Context.Empresa, NseqRegra });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VNATUREZAREGRATRIBUTACAO WHERE CODNATUREZA = ? AND CODEMPRESA = ? AND NSEQREGRA = ?", new object[] { _Codnatureza, AppLib.Context.Empresa, NseqRegra });
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
            AppLib.ORM.Jit VNATUREZAREGRATRIBUTACAO = new AppLib.ORM.Jit(conn, "VNATUREZAREGRATRIBUTACAO");
            conn.BeginTransaction();

            try
            {
                if (verifica == false)
                {
                    VNATUREZAREGRATRIBUTACAO.Set("CODEMPRESA", AppLib.Context.Empresa);
                    VNATUREZAREGRATRIBUTACAO.Set("CODNATUREZA", _Codnatureza);
                    if (edita == true)
                    {
                        VNATUREZAREGRATRIBUTACAO.Set("NSEQREGRA", Convert.ToInt32(conn.ExecGetField(0, @"SELECT NSEQREGRA FROM VNATUREZAREGRATRIBUTACAO WHERE CODEMPRESA = ? AND NSEQREGRA = ?", new object[] { AppLib.Context.Empresa, NseqRegra })));
                    }
                    else
                    {
                        VNATUREZAREGRATRIBUTACAO.Set("NSEQREGRA", Convert.ToInt32(conn.ExecGetField(0, @"SELECT (MAX(NSEQREGRA) + 1) AS NSEQREGRA FROM VNATUREZAREGRATRIBUTACAO", new object[] { })));
                    }
                    if (!string.IsNullOrEmpty(lpEstado.ValorCodigoInterno))
                    {
                        VNATUREZAREGRATRIBUTACAO.Set("CODETD", lpEstado.ValorCodigoInterno);
                    }
                    else
                    {
                        VNATUREZAREGRATRIBUTACAO.Set("CODETD", null);
                    }
                    if (!string.IsNullOrEmpty(lpRegiao.ValorCodigoInterno))
                    {
                        VNATUREZAREGRATRIBUTACAO.Set("CODREGIAO", lpRegiao.ValorCodigoInterno);
                    }
                    else
                    {
                        VNATUREZAREGRATRIBUTACAO.Set("CODREGIAO", null);
                    }
                    VNATUREZAREGRATRIBUTACAO.Set("TIPOREGRA", cbTipoRegra.SelectedValue);
                    VNATUREZAREGRATRIBUTACAO.Set("CONTRIBUINTE", cbContribuinte.SelectedValue);
                    VNATUREZAREGRATRIBUTACAO.Save();
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
