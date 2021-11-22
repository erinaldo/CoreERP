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
    public partial class frmCadastroContaCaixa : Form
    {
        public bool edita = false;
        public string CodConta = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroContaCaixa()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "FCONTA");
        }

        public frmCadastroContaCaixa(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "FCONTA");

            this.edita = true;
            this.lookup = lookup;
            CodConta = lookup.ValorCodigoInterno;
            carregaCampos();
        }

        private void frmCadastroContaCaixa_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodConta.Enabled = false;
                tbNumeroCheque.Enabled = false;
                dteDataBase.Enabled = false;
                tbSaldoData.Enabled = false;
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
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FCONTA WHERE CODCONTA = ?", new object[] { CodConta });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FCONTA WHERE CODCONTA = ?", new object[] { CodConta });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodConta.Text = dt.Rows[0]["CODCONTA"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            lpFilial.txtcodigo.Text = dt.Rows[0]["CODFILIAL"].ToString();
            lpFilial.CarregaDescricao();
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            tbNumeroCheque.Text = dt.Rows[0]["NUMEROCHEQUE"].ToString();
            dteDataBase.DateTime = Convert.ToDateTime(dt.Rows[0]["DTBASE"]);
            tbSaldoData.Text = dt.Rows[0]["SALDODATABASE"].ToString();
            chkRelConsolidado.Checked = Convert.ToBoolean(dt.Rows[0]["RELCONSOLIDADO"]);
            lpBanco.txtcodigo.Text = dt.Rows[0]["CODBANCO"].ToString();
            lpBanco.CarregaDescricao();
            lpAgencia.txtcodigo.Text = dt.Rows[0]["CODAGENCIA"].ToString();
            lpAgencia.CarregaDescricao();
            lpContaCorrente.txtcodigo.Text = dt.Rows[0]["NUMCONTA"].ToString();
            lpContaCorrente.CarregaDescricao();
        }

        private bool Salvar()
        {
            if (validacoes() == false)
            {
                return false;
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit FCONTA = new AppLib.ORM.Jit(conn, "FCONTA");
            conn.BeginTransaction();

            try
            {
                FCONTA.Set("CODEMPRESA", AppLib.Context.Empresa);
                FCONTA.Set("CODCONTA", tbCodConta.Text);
                FCONTA.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);

                if (!string.IsNullOrEmpty(lpFilial.txtcodigo.Text))
                {
                    FCONTA.Set("CODFILIAL", lpFilial.txtcodigo.Text);
                }
                else
                {
                    FCONTA.Set("CODFILIAL", null);
                }

                FCONTA.Set("DESCRICAO", tbDescricao.Text);

                if (!string.IsNullOrEmpty(tbNumeroCheque.Text))
                {
                    FCONTA.Set("NUMEROCHEQUE", Convert.ToInt32(tbNumeroCheque.Text));
                }
           
                FCONTA.Set("DTBASE", Convert.ToDateTime(dteDataBase.DateTime));
                FCONTA.Set("SALDODATABASE", Convert.ToDecimal(tbSaldoData.Text));
                FCONTA.Set("RELCONSOLIDADO", chkRelConsolidado.Checked == true ? 1 : 0);
                FCONTA.Set("ORDEMCONSOLIDADO", 0);

                if (!string.IsNullOrEmpty(lpBanco.txtcodigo.Text))
                {
                    FCONTA.Set("CODBANCO", lpBanco.txtcodigo.Text);
                }
                else
                {
                    FCONTA.Set("CODBANCO", null);
                }

                if (!string.IsNullOrEmpty(lpAgencia.txtcodigo.Text))
                {
                    FCONTA.Set("CODAGENCIA", lpAgencia.txtcodigo.Text);
                }
                else
                {
                    FCONTA.Set("CODAGENCIA", null);
                }

                if (!string.IsNullOrEmpty(lpContaCorrente.txtcodigo.Text))
                {
                    FCONTA.Set("NUMCONTA", lpContaCorrente.txtcodigo.Text);
                }
                else
                {
                    FCONTA.Set("NUMCONTA", null);
                }

                FCONTA.Save();
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
