using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ITGProducao.Controles;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroMoeda : Form
    {
        public bool edita = false;
        public string codMoeda = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;
        public frmCadastroMoeda()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VMOEDA");
        }
        public frmCadastroMoeda(ref NewLookup lookup)
        {
            this.edita = true;
            this.lookup = lookup;
        }

        private void frmCadastroMoeda_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodMoeda.Enabled = false;
            }
        }
        private bool validações()
        {
            if (string.IsNullOrEmpty(tbCodMoeda.Text))
            {
                MessageBox.Show("Favor informar o código da moeda.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(tbNomeMoeda.Text))
            {
                MessageBox.Show("Favor informar o nome.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(tbCodMoeda.Text) && string.IsNullOrEmpty(tbNomeMoeda.Text))
            {
                MessageBox.Show("Favor preencher todos os campos.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private bool Salvar()
        {
            if (validações() == false)
            {
                return false;
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VMOEDA = new AppLib.ORM.Jit(conn, "VMOEDA");
            conn.BeginTransaction();

            try
            {
                VMOEDA.Set("CODEMPRESA", AppLib.Context.Empresa);
                VMOEDA.Set("CODMOEDA", tbCodMoeda.Text);
                VMOEDA.Set("NOME", tbNomeMoeda.Text);
                VMOEDA.Save();
                conn.Commit();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void carregaObj(DataTable dt)
        {
            tbCodMoeda.Text = dt.Rows[0]["CODMOEDA"].ToString();
            tbNomeMoeda.Text = dt.Rows[0]["NOME"].ToString();
        }
        private void carregaCampos()
        {

            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODMOEDA, NOME FROM VMOEDA WHERE CODEMPRESA = ? AND CODMOEDA = ? ", new object[] { AppLib.Context.Empresa, codMoeda });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {

                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODMOEDA, NOME FROM VMOEDA WHERE CODEMPRESA = ? AND CODMOEDA = ?  ", new object[] { AppLib.Context.Empresa, codMoeda });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
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
                    codMoeda = tbCodMoeda.Text;
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
            tbCodMoeda.Select();
            tbCodMoeda.Text = string.Empty;
            tbNomeMoeda.Text = string.Empty;
        }
    }
}
