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
    public partial class frmCadastroSerie : Form
    {
        public bool edita = false;
        public string CodSerie = string.Empty;
        public string Codfilial = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroSerie()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VSERIE");
        }

        public frmCadastroSerie(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VSERIE");
            this.edita = true;
            this.lookup = lookup;
            CodSerie = lookup.ValorCodigoInterno;
            carregaCampos();
        }

        private void frmCadastroSerie_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodSerie.Enabled = false;
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodSerie.Text = dt.Rows[0]["CODSERIE"].ToString();
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            tbNumseq.Text = dt.Rows[0]["NUMSEQ"].ToString();
        }

        private bool validacoes()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(tbCodSerie.Text))
            {
                errorProvider1.SetError(tbCodSerie, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VSERIE WHERE CODSERIE = ? AND CODFILIAL = ?", new object[] { CodSerie, Codfilial });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VSERIE WHERE CODSERIE = ? AND CODFILIAL = ?", new object[] { CodSerie, Codfilial });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private bool Salvar()
        {
            if (validacoes() == false)
            {
                return false;
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VSERIE = new AppLib.ORM.Jit(conn, "VSERIE");
            conn.BeginTransaction();

            try
            {
                VSERIE.Set("CODEMPRESA", AppLib.Context.Empresa);
                VSERIE.Set("CODFILIAL", AppLib.Context.Filial);
                VSERIE.Set("CODSERIE", tbCodSerie.Text);
                VSERIE.Set("DESCRICAO", tbDescricao.Text);
                VSERIE.Set("NUMSEQ", tbNumseq.Text);
                VSERIE.Save();
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
                    CodSerie = tbCodSerie.Text;
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