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
    public partial class frmCadastroTributoNatureza : Form
    {
        public bool edita = false;
        public string CodTributo = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;
        public string _Codnatureza = string.Empty;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroTributoNatureza()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VNATUREZATRIBUTO");
        }
        public frmCadastroTributoNatureza(ref NewLookup lookup)
        {
            InitializeComponent();
            this.edita = true;
            this.lookup = lookup;
            CodTributo = lookup.ValorCodigoInterno;
        }
        private void frmCadastroTributoNatureza_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                CarregaCampos();
                lpTributo.Enabled = false;
            }
        }
        private void carregaObj(DataTable dt)
        {
            lpTributo.txtcodigo.Text = dt.Rows[0]["CODTRIBUTO"].ToString();
            lpTributo.CarregaDescricao();
            tbAliquota.Text = dt.Rows[0]["ALIQUOTA"].ToString();
            tbCodcst.Text = dt.Rows[0]["CODCST"].ToString();
            chkValorProduto.Checked = Convert.ToBoolean(dt.Rows[0]["VALORPRODUTO"]);
            chkDesconto.Checked = Convert.ToBoolean(dt.Rows[0]["DESCONTO"]);
            chkDespesa.Checked = Convert.ToBoolean(dt.Rows[0]["DESPESA"]);
            chkfrete.Checked = Convert.ToBoolean(dt.Rows[0]["FRETE"]);
            chkIpi.Checked = Convert.ToBoolean(dt.Rows[0]["IPI"]);
            chkSeguro.Checked = Convert.ToBoolean(dt.Rows[0]["SEGURO"]);
        }
        private void CarregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VNATUREZATRIBUTO WHERE CODNATUREZA = ? AND CODEMPRESA = ? AND CODTRIBUTO = ?", new object[] { _Codnatureza, AppLib.Context.Empresa, CodTributo });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VNATUREZATRIBUTO  WHERE CODNATUREZA = ? AND CODEMPRESA = ? AND CODTRIBUTO = ?", new object[] { _Codnatureza, AppLib.Context.Empresa, CodTributo });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }
        private bool Salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VNATUREZATRIBUTO = new AppLib.ORM.Jit(conn, "VNATUREZATRIBUTO");
            conn.BeginTransaction();

            try
            {
                VNATUREZATRIBUTO.Set("CODEMPRESA", AppLib.Context.Empresa);
                VNATUREZATRIBUTO.Set("CODNATUREZA", _Codnatureza);
                if (!string.IsNullOrEmpty(lpTributo.ValorCodigoInterno))
                {
                    VNATUREZATRIBUTO.Set("CODTRIBUTO", lpTributo.ValorCodigoInterno);
                }
                else
                {
                    VNATUREZATRIBUTO.Set("CODTRIBUTO", null);
                }
                VNATUREZATRIBUTO.Set("ALIQUOTA", Convert.ToDecimal(tbAliquota.Text));
                VNATUREZATRIBUTO.Set("CODCST", tbCodcst.Text);
                VNATUREZATRIBUTO.Set("VALORPRODUTO", chkValorProduto.Checked == true ? 1 : 0);
                VNATUREZATRIBUTO.Set("DESCONTO", chkDesconto.Checked == true ? 1 : 0);
                VNATUREZATRIBUTO.Set("DESPESA", chkDespesa.Checked == true ? 1 : 0);
                VNATUREZATRIBUTO.Set("FRETE", chkfrete.Checked == true ? 1 : 0);
                VNATUREZATRIBUTO.Set("IPI", chkIpi.Checked == true ? 1 : 0);
                VNATUREZATRIBUTO.Set("SEGURO", chkSeguro.Checked == true ? 1 : 0);
                VNATUREZATRIBUTO.Save();
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
