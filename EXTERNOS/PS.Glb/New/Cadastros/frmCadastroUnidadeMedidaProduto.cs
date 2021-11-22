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
    public partial class frmCadastroUnidadeMedidaProduto : Form
    {
        public bool edita = false;

        public string CodProduto = string.Empty;
        public string CodUnidade = string.Empty;
        public string CodUnidadeConversao = string.Empty;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroUnidadeMedidaProduto()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VPRODUTOUNIDADE");
        }

        public frmCadastroUnidadeMedidaProduto(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VPRODUTOUNIDADE");
            this.edita = true;
            this.lookup = lookup;
            CodUnidade = lookup.ValorCodigoInterno;
        }

        private void frmCadastroUnidadeMedidaProduto_Load(object sender, EventArgs e)
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
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPRODUTOUNIDADE WHERE CODPRODUTO = ? AND CODUNID = ? AND CODUNIDCONVERSAO = ? AND CODEMPRESA = ?", new object[] { CodProduto, CodUnidade, CodUnidadeConversao, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPRODUTOUNIDADE WHERE CODPRODUTO = ? AND CODUNID = ? AND CODUNIDCONVERSAO = ? AND CODEMPRESA = ?", new object[] { CodProduto, CodUnidade, CodUnidadeConversao, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            lpUnidadeMedidaProduto.txtcodigo.Text = dt.Rows[0]["CODUNID"].ToString();
            lpUnidadeMedidaProduto.CarregaDescricao();
            lpUnidadeConversaoProduto.txtcodigo.Text = dt.Rows[0]["CODUNIDCONVERSAO"].ToString();
            lpUnidadeConversaoProduto.CarregaDescricao();
            tbFatorConversaoProduto.Text = dt.Rows[0]["FATORCONVERSAO"].ToString();
            tbDescricaoProduto.Text = dt.Rows[0]["DESCRICAO"].ToString();
        }

        private bool Salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VPRODUTOUNIDADE = new AppLib.ORM.Jit(conn, "VPRODUTOUNIDADE");
            conn.BeginTransaction();

            try
            {
                VPRODUTOUNIDADE.Set("CODEMPRESA", AppLib.Context.Empresa);
                VPRODUTOUNIDADE.Set("CODPRODUTO", CodProduto);
                VPRODUTOUNIDADE.Set("CODUNID", lpUnidadeMedidaProduto.txtcodigo.Text);
                VPRODUTOUNIDADE.Set("CODUNIDCONVERSAO", lpUnidadeConversaoProduto.txtcodigo.Text);
                VPRODUTOUNIDADE.Set("FATORCONVERSAO", Convert.ToDecimal(tbFatorConversaoProduto.Text));
                VPRODUTOUNIDADE.Set("DESCRICAO", tbDescricaoProduto.Text);
                VPRODUTOUNIDADE.Set("USUARIOCRIACAO", AppLib.Context.Usuario);
                VPRODUTOUNIDADE.Set("DATACRIACAO", DateTime.Now);

                if (edita == true)
                {
                    VPRODUTOUNIDADE.Set("USUARIOALTERACAO", AppLib.Context.Usuario);
                    VPRODUTOUNIDADE.Set("DATAALTERACAO", DateTime.Now);
                }

                VPRODUTOUNIDADE.Save();
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
