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
    public partial class frmCadastroCentroCusto : Form
    {
        public bool edita = false;
        public string CodCusto = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;
        public frmCadastroCentroCusto()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GCENTROCUSTO");
        }
        public frmCadastroCentroCusto(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GCENTROCUSTO");

            this.edita = true;
            this.lookup = lookup;
            CodCusto = lookup.ValorCodigoInterno;
            CarregaCampos();
        }
        private void frmCadastroCentroCusto_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                CarregaCampos();
                tbCodCusto.Enabled = false;
            }
        }
        private bool validações()
        {
            if (string.IsNullOrEmpty(tbCodCusto.Text))
            {
                MessageBox.Show("Favor informar o código.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void carregaObj(DataTable dt)
        {
            tbCodCusto.Text = dt.Rows[0]["CODCCUSTO"].ToString();
            tbNomeCusto.Text = dt.Rows[0]["NOME"].ToString();
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            chkPermiteLancamento.Checked = Convert.ToBoolean(dt.Rows[0]["PERMITELANCAMENTO"]);
        }
        private void CarregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GCENTROCUSTO WHERE CODCCUSTO = ? AND CODEMPRESA = ?", new object[] { CodCusto, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GCENTROCUSTO WHERE CODCCUSTO = ? AND CODEMPRESA = ?", new object[] { CodCusto, AppLib.Context.Empresa });
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
            AppLib.ORM.Jit GCENTROCUSTO = new AppLib.ORM.Jit(conn, "GCENTROCUSTO");
            conn.BeginTransaction();

            try
            {
                GCENTROCUSTO.Set("CODEMPRESA", AppLib.Context.Empresa);
                GCENTROCUSTO.Set("CODCCUSTO", tbCodCusto.Text);
                GCENTROCUSTO.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                GCENTROCUSTO.Set("PERMITELANCAMENTO", chkPermiteLancamento.Checked == true ? 1 : 0);
                GCENTROCUSTO.Set("NOME", tbNomeCusto.Text);
                GCENTROCUSTO.Set("DESCRICAO", tbDescricao.Text);
                GCENTROCUSTO.Save();
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
            tbCodCusto.Select();
            tbCodCusto.Text = string.Empty;
            chkAtivo.Checked = false;
            chkPermiteLancamento.Checked = false;
            tbNomeCusto.Text = string.Empty;
            tbDescricao.Text = string.Empty;
        }
    }
}
