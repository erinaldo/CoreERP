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
    public partial class frmCadastroRegiaoEstado : Form
    {
        public bool edita = false;
        public string CodEstado = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;
        public string _CodRegiao = string.Empty;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroRegiaoEstado()
        {
            InitializeComponent();
        }
        public frmCadastroRegiaoEstado(ref NewLookup lookup)
        {
            InitializeComponent();
            this.edita = true;
            this.lookup = lookup;
            CodEstado = lookup.ValorCodigoInterno;
        }
        private void frmCadastroRegiaoEstado_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                CarregaCampos();
            }
        }
        private void carregaObj(DataTable dt)
        {
            lpEstado.txtcodigo.Text = dt.Rows[0]["CODETD"].ToString();
            lpEstado.CarregaDescricao();
        }
        private void CarregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VREGIAOESTADO WHERE CODREGIAO = ? AND CODEMPRESA = ?", new object[] { _CodRegiao, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VREGIAOESTADO WHERE CODREGIAO = ? AND CODEMPRESA = ?", new object[] { _CodRegiao, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }
        private bool Salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VREGIAOESTADO = new AppLib.ORM.Jit(conn, "VREGIAOESTADO");
            conn.BeginTransaction();

            //PS.Glb.New.Cadastros.frmCadastroRegiao regiao = new frmCadastroRegiao();
            //regiao.CodRegiao = _CodRegiao;

            try
            {
                VREGIAOESTADO.Set("CODEMPRESA", AppLib.Context.Empresa);
                VREGIAOESTADO.Set("CODREGIAO", _CodRegiao);
                if (!string.IsNullOrEmpty(lpEstado.ValorCodigoInterno))
                {
                    VREGIAOESTADO.Set("CODETD", lpEstado.ValorCodigoInterno);
                }
                else
                {
                    VREGIAOESTADO.Set("CODETD", null);
                }
                VREGIAOESTADO.Save();
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
