using ITGProducao.Controles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroContaCorrente : Form
    {
        public bool edita = false;
        public string CodConta = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroContaCorrente()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "FCCORRENTE");
        }

        public frmCadastroContaCorrente(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "FCCORRENTE");

            this.edita = true;
            this.lookup = lookup;
            CodConta = lookup.ValorCodigoInterno;
            carregaCampos();
        }

        private void frmCadastroContaCorrente_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodConta.Enabled = false;
            }
        }

        private bool validacoes()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(tbCodConta.Text))
            {
                errorProvider1.SetError(tbCodConta, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FCCORRENTE WHERE NUMCONTA = ?", new object[] { CodConta });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FCCORRENTE WHERE NUMCONTA = ?", new object[] { CodConta });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodConta.Text = dt.Rows[0]["CODAGENCIA"].ToString();
            tbDigito.Text = dt.Rows[0]["DIGCONTA"].ToString();
            lpBanco.txtcodigo.Text = dt.Rows[0]["CODBANCO"].ToString();
            lpBanco.CarregaDescricao();
            lpAgencia.txtcodigo.Text = dt.Rows[0]["CODAGENCIA"].ToString();
            lpAgencia.CarregaDescricao();
        }

        private bool Salvar()
        {
            if (validacoes() == false)
            {
                return false;
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit FCCORRENTE = new AppLib.ORM.Jit(conn, "FCCORRENTE");
            conn.BeginTransaction();

            try
            {
                FCCORRENTE.Set("CODEMPRESA", AppLib.Context.Empresa);
                FCCORRENTE.Set("NUMCONTA", tbCodConta.Text);
                FCCORRENTE.Set("DIGCONTA", tbDigito.Text);

                if (!string.IsNullOrEmpty(lpBanco.txtcodigo.Text))
                {
                    FCCORRENTE.Set("CODBANCO", lpBanco.txtcodigo.Text);
                }
                else
                {
                    FCCORRENTE.Set("CODBANCO", null);
                }

                if (!string.IsNullOrEmpty(lpAgencia.txtcodigo.Text))
                {
                    FCCORRENTE.Set("CODAGENCIA", lpAgencia.txtcodigo.Text);
                }
                else
                {
                    FCCORRENTE.Set("CODAGENCIA", null);
                }

                FCCORRENTE.Save();
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
    }
}
