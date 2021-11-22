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
    public partial class frmCadastroMensagensPadrao : Form
    {
        public bool edita = false;
        public string CodMensagem = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;
        public frmCadastroMensagensPadrao()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VOPERMENSAGEM");
        }
        public frmCadastroMensagensPadrao(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VOPERMENSAGEM");
            this.edita = true;
            this.lookup = lookup;
            CodMensagem = lookup.ValorCodigoInterno;
            CarregaCampos();
        }
        private void frmCadastroMensagensPadrao_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                CarregaCampos();
                tbCodMensagem.Enabled = false;
            }
        }
        private bool validações()
        {
            bool verifica = true;

            errorProvider.Clear();

            if (string.IsNullOrEmpty(tbCodMensagem.Text))
            {
                errorProvider.SetError(tbCodMensagem, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }
        private void carregaObj(DataTable dt)
        {
            tbCodMensagem.Text = dt.Rows[0]["CODMENSAGEM"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            tbMensagem.Text = dt.Rows[0]["MENSAGEM"].ToString();
        }
        private void CarregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VOPERMENSAGEM WHERE CODMENSAGEM = ? AND CODEMPRESA = ?", new object[] { CodMensagem, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VOPERMENSAGEM WHERE CODMENSAGEM = ? AND CODEMPRESA = ?", new object[] { CodMensagem, AppLib.Context.Empresa });
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
            AppLib.ORM.Jit VOPERMENSAGEM = new AppLib.ORM.Jit(conn, "VOPERMENSAGEM");
            conn.BeginTransaction();

            try
            {
                VOPERMENSAGEM.Set("CODEMPRESA", AppLib.Context.Empresa);
                VOPERMENSAGEM.Set("CODMENSAGEM", tbCodMensagem.Text);                
                VOPERMENSAGEM.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                VOPERMENSAGEM.Set("DESCRICAO", tbDescricao.Text);
                VOPERMENSAGEM.Set("MENSAGEM", tbMensagem.Text);
                VOPERMENSAGEM.Save();
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
        private void btnNovo_Click(object sender, EventArgs e)
        {
            tbCodMensagem.Select();
            tbCodMensagem.Text = string.Empty;
            chkAtivo.Checked = false;
            tbDescricao.Text = string.Empty;
            tbMensagem.Text = string.Empty;
        }
    }
}
