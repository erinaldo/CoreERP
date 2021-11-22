using ITGProducao.Controles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Cadastros.Globais
{
    public partial class frmCadastroQuery : Form
    {
        public bool edita = false;
        public string codQuery = string.Empty;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroQuery()
        {
            InitializeComponent();
        }


        public frmCadastroQuery(ref NewLookup lookup)
        {
            InitializeComponent();

            this.codQuery = lookup.txtcodigo.Text;
            this.edita = true;

            this.lookup = lookup;
        }
        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void carregaObj(DataTable dt)
        {
            txtCodQuery.Text = dt.Rows[0]["CODQUERY"].ToString();
            txtDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            meQuery.Text = dt.Rows[0]["QUERY"].ToString();
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GQUERY WHERE CODEMPRESA = ? AND CODQUERY = ?", new object[] { AppLib.Context.Empresa, codQuery });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GQUERY WHERE CODEMPRESA = ? AND CODQUERY = ?", new object[] { AppLib.Context.Empresa, codQuery });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
             
        }

        private void frmCadastroQuery_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
            }
        }

        private bool salvar()
        {
            try
            {
                if (string.IsNullOrEmpty(txtCodQuery.Text) || string.IsNullOrEmpty(txtDescricao.Text))
                {
                    MessageBox.Show("Favor preecher os campos corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (edita == true)
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GQUERY SET DESCRICAO = ?, QUERY = ? WHERE CODQUERY = ? AND CODEMPRESA = ?", new object[] { txtDescricao.Text, meQuery.Text, txtCodQuery.Text, AppLib.Context.Empresa });
                }
                else
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO GQUERY (DESCRICAO, QUERY, CODQUERY, CODEMPRESA) VALUES (?, ?, ?, ?)", new object[] { txtDescricao.Text, meQuery.Text, txtCodQuery.Text, AppLib.Context.Empresa });
                    edita = true;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtCodQuery.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            meQuery.Text = string.Empty;
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                salvar();
            }
            else
            {
                if (salvar() == true)
                {
                    codQuery = txtCodQuery.Text;
                    carregaCampos();

                    lookup.txtcodigo.Text = txtCodQuery.Text;
                    lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                    lookup.ValorCodigoInterno = txtCodQuery.Text;

                    this.Dispose();
                }
            }
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (salvar() == true)
                {
                    this.Dispose();
                    GC.Collect();
                }
            }
            else
            {
                if (salvar() == true)
                {
                    codQuery = txtCodQuery.Text;
                    carregaCampos();

                    lookup.txtcodigo.Text = txtCodQuery.Text;
                    lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                    lookup.ValorCodigoInterno = txtCodQuery.Text;

                    this.Dispose();
                }
            }
        }
    }
}
