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
    public partial class frmCadastroUsuarios : Form
    {
        public bool edita = false;
        public string CodUsuario = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;
        public frmCadastroUsuarios()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GUSUARIO");
        }
        public frmCadastroUsuarios(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GUSUARIO");
            this.edita = true;
            this.lookup = lookup;
            CodUsuario = lookup.ValorCodigoInterno;
            tbSenha.Enabled = false;
            CarregaCampos();
        }
        private void frmCadastroUsuarios_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                CarregaCampos();
                tbCodUsuario.Enabled = false;
                dtUltimoLogin.Enabled = false;

                if (chkNuncaExpira.Checked == true)
                {
                    dtExpiracao.Enabled = false;
                }
            }
            else
            {
                //this.tbSenha.Properties.PasswordChar = '\0';
                dtUltimoLogin.ResetText();
                dtUltimoLogin.Enabled = false;
                chkAtivo.Checked = true;
            }
        }

        private void chkNuncaExpira_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNuncaExpira.Checked == false)
            {
                dtExpiracao.Enabled = true;
            }
            else
            {
                dtExpiracao.Enabled = false;
                dtExpiracao.ResetText();
            }
        }

        private bool validacoes()
        {
            bool verifica = true;

            errorProvider.Clear();

            if (string.IsNullOrEmpty(tbCodUsuario.Text))
            {
                errorProvider.SetError(tbCodUsuario, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }

        private bool ValidaCodUsuario(string _codUsuario)
        {
            bool verifica = true;

            errorProvider.Clear();

            int validaCodUsuario;
            validaCodUsuario = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT COUNT(CODUSUARIO) FROM GUSUARIO WHERE CODUSUARIO = ?", new object[] { _codUsuario }));

            if (validaCodUsuario > 0)
            {
                errorProvider.SetError(tbCodUsuario, "O código informado já existe.");
                verifica = false;
            }
            return verifica;
        }

        private void carregaObj(DataTable dt)
        {
            tbCodUsuario.Text = dt.Rows[0]["CODUSUARIO"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            chkSupervisor.Checked = Convert.ToBoolean(dt.Rows[0]["SUPERVISOR"]);
            tbNome.Text = dt.Rows[0]["NOME"].ToString();
            //tbSenha.Text = dt.Rows[0]["SENHA"].ToString();
            tbSenha.Text = new PS.Lib.Criptografia().Decoder(PS.Lib.Criptografia.OpcoesEncoder.Rijndael, dt.Rows[0]["SENHA"].ToString(), tbCodUsuario.Text).ToString();

            chkNuncaExpira.Checked = Convert.ToBoolean(dt.Rows[0]["NUNCAEXPIRA"]);
            if (!string.IsNullOrEmpty(dt.Rows[0]["ULTIMOLOGIN"].ToString()))
            {
                dtUltimoLogin.DateTime = Convert.ToDateTime(dt.Rows[0]["ULTIMOLOGIN"]);
            }
            if (!string.IsNullOrEmpty(dt.Rows[0]["DTEXPIRACAO"].ToString()))
            {
                dtExpiracao.DateTime = Convert.ToDateTime(dt.Rows[0]["DTEXPIRACAO"]);
            }
            tbEmail.Text = dt.Rows[0]["EMAIL"].ToString();
            clFornecedor.textBoxCODIGO.Text = dt.Rows[0]["CODCLIFOR"].ToString();
            clFornecedor.textBoxDESCRICAO.Text = dt.Rows[0]["NOMECLIFOR"].ToString();

        }
        private void CarregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT GUSUARIO.*, VCLIFOR.NOME as 'NOMECLIFOR' FROM GUSUARIO

                                                                            left join VCLIFOR
                                                                            on VCLIFOR.CODCLIFOR = GUSUARIO.CODCLIFOR

                                                                            WHERE CODUSUARIO = ? ", new object[] { CodUsuario });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GUSUARIO WHERE CODUSUARIO = ?", new object[] { CodUsuario });
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

            if (edita == false)
            {
                if (ValidaCodUsuario(tbCodUsuario.Text) == false)
                {
                    return false;
                }
            }
            
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit GUSUARIO = new AppLib.ORM.Jit(conn, "GUSUARIO");
            conn.BeginTransaction();

            try
            {
                GUSUARIO.Set("CODUSUARIO", tbCodUsuario.Text);
                GUSUARIO.Set("NOME", tbNome.Text);
                GUSUARIO.Set("CODCLIFOR", clFornecedor.textBoxCODIGO.Text);
                GUSUARIO.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                GUSUARIO.Set("SUPERVISOR", chkSupervisor.Checked == true ? 1 : 0);
                GUSUARIO.Set("SENHA", new PS.Lib.Criptografia().Encoder(PS.Lib.Criptografia.OpcoesEncoder.Rijndael, tbSenha.Text, tbCodUsuario.Text));
                GUSUARIO.Set("NUNCAEXPIRA", chkNuncaExpira.Checked == true ? 1 : 0);
                if (!string.IsNullOrEmpty(dtExpiracao.Text))
                {
                    GUSUARIO.Set("DTEXPIRACAO", Convert.ToDateTime(dtExpiracao.Text));
                }
                else
                {
                    GUSUARIO.Set("DTEXPIRACAO", null);
                }
                if (!string.IsNullOrEmpty(dtUltimoLogin.Text))
                {
                    GUSUARIO.Set("ULTIMOLOGIN", Convert.ToDateTime(dtUltimoLogin.Text));
                }
                else
                {
                    GUSUARIO.Set("ULTIMOUSUARIO", null);
                }
                GUSUARIO.Set("EMAIL", tbEmail.Text);
                GUSUARIO.Save();
                conn.Commit();

                edita = true;
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
            tbCodUsuario.Select();
            tbCodUsuario.Text = string.Empty;
            chkAtivo.Checked = false;
            chkSupervisor.Checked = false;
            tbNome.Text = string.Empty;
            tbSenha.Text = string.Empty;
            chkNuncaExpira.Checked = false;
            dtExpiracao.ResetText();
            dtUltimoLogin.ResetText();
            tbEmail.Text = string.Empty;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
