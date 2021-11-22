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
    public partial class frmCadastroUnidadeConversao : Form
    {
        public bool edita = false;

        public string CodUnidade = string.Empty;
        public string CodUnidadeConversao = string.Empty;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroUnidadeConversao()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VUNIDCONVERSAO");
        }

        public frmCadastroUnidadeConversao(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VUNIDCONVERSAO");
            this.edita = true;
            this.lookup = lookup;
            CodUnidade = lookup.ValorCodigoInterno;
        }

        private void frmCadastroUnidadeConversao_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
            }
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VUNIDCONVERSAO WHERE CODUNID = ? AND CODUNIDCONVERSAO = ? AND CODEMPRESA = ?", new object[] { CodUnidade, CodUnidadeConversao, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VUNIDCONVERSAO WHERE CODUNID = ? AND CODUNIDCONVERSAO = ? AND CODEMPRESA = ?", new object[] { CodUnidade, CodUnidadeConversao, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            lpUnidadeMedida.txtcodigo.Text = dt.Rows[0]["CODUNID"].ToString();
            lpUnidadeMedida.CarregaDescricao();
            lpUnidadeConversao.txtcodigo.Text = dt.Rows[0]["CODUNIDCONVERSAO"].ToString();
            lpUnidadeConversao.CarregaDescricao();
            tbFatorConversao.Text = dt.Rows[0]["FATORCONVERSAO"].ToString();
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
        }

        private bool Salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VUNIDCONVERSAO = new AppLib.ORM.Jit(conn, "VUNIDCONVERSAO");
            conn.BeginTransaction();

            try
            {
                VUNIDCONVERSAO.Set("CODEMPRESA", AppLib.Context.Empresa);
                VUNIDCONVERSAO.Set("CODUNID", lpUnidadeMedida.txtcodigo.Text);
                VUNIDCONVERSAO.Set("CODUNIDCONVERSAO", lpUnidadeConversao.txtcodigo.Text);
                VUNIDCONVERSAO.Set("FATORCONVERSAO", Convert.ToDecimal(tbFatorConversao.Text));
                VUNIDCONVERSAO.Set("DESCRICAO", tbDescricao.Text);
                VUNIDCONVERSAO.Set("USUARIOCRIACAO", AppLib.Context.Usuario);
                VUNIDCONVERSAO.Set("DATACRIACAO", DateTime.Now);

                if (edita == true)
                {
                    VUNIDCONVERSAO.Set("USUARIOALTERACAO", AppLib.Context.Usuario);
                    VUNIDCONVERSAO.Set("DATAALTERACAO", DateTime.Now);
                }

                VUNIDCONVERSAO.Save();
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
                    CodUnidade = lpUnidadeMedida.txtcodigo.Text;
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
