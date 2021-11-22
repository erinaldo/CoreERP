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
    public partial class frmCadastroCamposObrigatorio : Form
    {
        public bool edita = false;
        public string Id = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;
        public string Tabela = string.Empty;

        public frmCadastroCamposObrigatorio()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GCAMPOSOBRIGATORIO");
        }

        private void CarregaCombo()
        {
            #region Carrega Combobox

            cbTabela.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT T.name AS 'TABELA' FROM sys.all_objects T WHERE T.type = 'U' ORDER BY 1 ", new object[] { });

            cbTabela.DisplayMember = "TABELA";

            cbTabela.ValueMember = "TABELA";

            cbColuna.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT C.name from sys.all_columns C LEFT OUTER JOIN sys.all_objects T ON T.object_id = C.object_id LEFT OUTER JOIN GDICIONARIO D ON D.TABELA = T.name AND D.COLUNA = C.name WHERE T.name = '" + cbTabela.SelectedValue.ToString() + "' ORDER BY 1", new object[] { });

            cbColuna.DisplayMember = "name";

            cbColuna.ValueMember = "name";

            #endregion
        }

        private void frmCadastroCamposObrigatorio_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                CarregaCombo();
                CarregaCampos();
            }
            else
            {
                CarregaCombo();
            }
        }

        private void cbTabela_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbColuna.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT C.name from sys.all_columns C LEFT OUTER JOIN sys.all_objects T ON T.object_id = C.object_id LEFT OUTER JOIN GDICIONARIO D ON D.TABELA = T.name AND D.COLUNA = C.name WHERE T.name = '" + cbTabela.SelectedValue.ToString() + "' ORDER BY 1", new object[] { });
            cbColuna.DisplayMember = "name";
            cbColuna.ValueMember = "name";

            if (cbTabela.Text == "GOPER" || cbTabela.Text == "GTIPOPER")
            {

                cbCodTipOper.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODTIPOPER FROM GTIPOPER WHERE CODEMPRESA = ? ORDER BY 1 ", new object[] { AppLib.Context.Empresa });

                cbCodTipOper.DisplayMember = "CODTIPOPER";

                cbCodTipOper.ValueMember = "CODTIPOPER";

                cbCodTipOper.Enabled = true;
            }
            else
            {
                cbCodTipOper.Enabled = false;
                cbCodTipOper.DataSource = null;
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbIdCampo.Text = dt.Rows[0]["ID"].ToString();
            cbTabela.SelectedValue = dt.Rows[0]["TABELA"];
            cbTabela.DisplayMember = dt.Rows[0]["TABELA"].ToString();
            cbColuna.SelectedValue = dt.Rows[0]["COLUNA"];
            cbCodTipOper.SelectedValue = dt.Rows[0]["CODTIPOPER"];
            tbInfNaoPermitida.Text = dt.Rows[0]["INFNAOPERMITIDA"].ToString();
        }
        private void CarregaCampos()
        {
            DataTable dt;

            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GCAMPOSOBRIGATORIO WHERE ID = ? AND CODEMPRESA = ?", new object[] { Id, AppLib.Context.Empresa });
            if (dt.Rows.Count > 0)
            {
                carregaObj(dt);
            }
        }

        private bool Salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit GCAMPOSOBRIGATORIO = new AppLib.ORM.Jit(conn, "GCAMPOSOBRIGATORIO");
            conn.BeginTransaction();

            try
            {
                GCAMPOSOBRIGATORIO.Set("ID", Id);
                GCAMPOSOBRIGATORIO.Set("CODEMPRESA", AppLib.Context.Empresa);
                GCAMPOSOBRIGATORIO.Set("TABELA", cbTabela.SelectedValue);
                GCAMPOSOBRIGATORIO.Set("COLUNA", cbColuna.SelectedValue);
                GCAMPOSOBRIGATORIO.Set("CODTIPOPER", cbCodTipOper.SelectedValue);
                GCAMPOSOBRIGATORIO.Set("INFNAOPERMITIDA", tbInfNaoPermitida.Text);
                GCAMPOSOBRIGATORIO.Save();
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
            if (Salvar() == true)
            {
                CarregaCampos();
                this.Dispose();
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
