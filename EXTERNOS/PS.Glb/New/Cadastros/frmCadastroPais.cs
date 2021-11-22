using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITGProducao.Controles;
using System.Data.SqlClient;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroPais : Form
    {
        public bool edita = false;
        public string CodPais = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;
        public frmCadastroPais()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GPAIS");
        }
        public frmCadastroPais(ref NewLookup lookup)
        {
            this.edita = true;
            this.lookup = lookup;
        }
        private void frmCadastroPais_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodPais.Enabled = false;
            }
        }
        private bool validações()
        {
            if (string.IsNullOrEmpty(tbCodPais.Text))
            {
                MessageBox.Show("Favor informar o código do país.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(tbNomePais.Text))
            {
                MessageBox.Show("Favor informar o nome.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(tbCodBacen.Text))
            {
                MessageBox.Show("Favor informar o código BACEN.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void carregaObj(DataTable dt)
        {
            tbCodPais.Text = dt.Rows[0]["CODPAIS"].ToString();
            tbNomePais.Text = dt.Rows[0]["NOME"].ToString();
            tbCodBacen.Text = dt.Rows[0]["CODBACEN"].ToString();
            tbNomeReduzido.Text = dt.Rows[0]["CODREDUZIDO"].ToString();
        }
        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GPAIS WHERE CODPAIS = ?", new object[] { CodPais });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GPAIS WHERE CODPAIS = ?", new object[] { CodPais });
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
            AppLib.ORM.Jit GPAIS = new AppLib.ORM.Jit(conn, "GPAIS");
            conn.BeginTransaction();

            try
            {
                GPAIS.Set("CODPAIS", tbCodPais.Text);
                GPAIS.Set("NOME", tbNomePais.Text);
                GPAIS.Set("CODREDUZIDO", tbNomeReduzido.Text);
                GPAIS.Set("CODBACEN", tbCodBacen.Text);
                GPAIS.Save();
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
                    CodPais = tbCodPais.Text;
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
            tbCodPais.Select();
            tbCodPais.Text = string.Empty;
            tbNomePais.Text = string.Empty;
            tbNomeReduzido.Text = string.Empty;
            tbCodBacen.Text = string.Empty;
        }
    }
}
