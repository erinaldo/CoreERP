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
    public partial class frmCadastroEstado : Form
    {
        public bool edita = false;
        public string CodEstado = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;
        public frmCadastroEstado()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GESTADO");
        }
        public frmCadastroEstado(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GESTADO");
            this.edita = true;
            this.lookup = lookup;
        }
        private void frmCadastroEstado_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodEstado.Enabled = false;
            }
        }
        private bool validações()
        {
            if (string.IsNullOrEmpty(tbCodEstado.Text))
            {
                MessageBox.Show("Favor informar o código do estado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(tbNomeEstado.Text))
            {
                MessageBox.Show("Favor informar o nome.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(tbCodIbge.Text))
            {
                MessageBox.Show("Favor informar o código do IBGE.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(tbAliquota.Text))
            {
                MessageBox.Show("Favor informar a aliquota.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void carregaObj(DataTable dt)
        {
            tbCodEstado.Text = dt.Rows[0]["CODETD"].ToString();
            tbNomeEstado.Text = dt.Rows[0]["NOME"].ToString();
            tbCodIbge.Text = dt.Rows[0]["CODIBGE"].ToString();
            tbAliquota.Text = dt.Rows[0]["ALIQUOTAICMSINTERNADEST"].ToString();
        }
        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GESTADO WHERE CODETD = ?", new object[] { CodEstado});
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
               dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GESTADO WHERE CODETD = ?", new object[] { CodEstado});
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
            AppLib.ORM.Jit GESTADO = new AppLib.ORM.Jit(conn, "GESTADO");
            conn.BeginTransaction();

            try
            {
                GESTADO.Set("CODETD", tbCodEstado.Text);
                GESTADO.Set("NOME", tbNomeEstado.Text);
                GESTADO.Set("CODIBGE", Convert.ToInt32(tbCodIbge.Text));
                GESTADO.Set("ALIQUOTAICMSINTERNADEST", Convert.ToDecimal(tbAliquota.Text));
                GESTADO.Save();
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
                    CodEstado = tbCodEstado.Text;
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
            tbCodEstado.Select();
            tbCodEstado.Text = string.Empty;
            tbNomeEstado.Text = string.Empty;
            tbCodIbge.Text = string.Empty;
            tbAliquota.Text = string.Empty;
        }
    }
}
