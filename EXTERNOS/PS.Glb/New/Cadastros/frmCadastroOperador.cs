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
    public partial class frmCadastroOperador : Form
    {
        public bool edita = false;
        public string CodOperador = string.Empty;
        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;
        public frmCadastroOperador()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VOPERADOR");
        }
        public frmCadastroOperador(ref NewLookup lookup)
        {
            InitializeComponent();
            this.edita = true;
            this.lookup = lookup;
        }
        private void frmCadastroOperador_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodOperador.Enabled = false;
            }
        }
        private bool validações()
        {
            if (string.IsNullOrEmpty(tbCodOperador.Text))
            {
                MessageBox.Show("Favor informar o código do operador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(tbNomeOperador.Text))
            {
                MessageBox.Show("Favor informar o nome.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void carregaObj(DataTable dt)
        {
            tbCodOperador.Text = dt.Rows[0]["CODOPERADOR"].ToString();
            tbNomeOperador.Text = dt.Rows[0]["NOME"].ToString();
            lpUsuario.txtcodigo.Text = dt.Rows[0]["CODUSUARIO"].ToString();
            lpUsuario.CarregaDescricao();
        }
        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VOPERADOR WHERE CODOPERADOR = ? AND CODEMPRESA= ?", new object[] {CodOperador, AppLib.Context.Empresa});
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VOPERADOR WHERE CODOPERADOR = ? AND CODEMPRESA= ?", new object[] {CodOperador, AppLib.Context.Empresa});
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
            AppLib.ORM.Jit VOPERADOR = new AppLib.ORM.Jit(conn, "VOPERADOR");
            conn.BeginTransaction();

            try
            {
                VOPERADOR.Set("CODEMPRESA", AppLib.Context.Empresa);
                VOPERADOR.Set("CODOPERADOR", tbCodOperador.Text);
                VOPERADOR.Set("NOME", tbNomeOperador.Text);
                VOPERADOR.Set("CODUSUARIO",lpUsuario.ValorCodigoInterno);
                if (!string.IsNullOrEmpty(lpUsuario.ValorCodigoInterno))
                {
                    VOPERADOR.Set("CODUSUARIO", lpUsuario.ValorCodigoInterno);
                }
                else
                {
                    VOPERADOR.Set("CODUSUARIO", null);
                }
                VOPERADOR.Save();
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
            tbCodOperador.Select();
            tbCodOperador.Text = string.Empty;
            tbNomeOperador.Text = string.Empty;
            lpUsuario.Clear();
        }
    }
}
