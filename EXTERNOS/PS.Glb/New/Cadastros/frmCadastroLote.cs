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
    public partial class frmCadastroLote : Form
    {
        #region Variáveis

        public string CodLote = string.Empty;
        public int CodFilial;
        public string CodClifor = string.Empty;
        public string Numero = string.Empty;
        public bool edita = false;

        #endregion

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroLote()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VPRODUTOLOTE");
        }

        public frmCadastroLote(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VPRODUTOLOTE");
            this.edita = true;
            this.lookup = lookup;
            CodLote = lookup.ValorCodigoInterno;
            carregaCampos();
        }

        private void frmCadastroLote_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodLote.Enabled = false;
                dteDataEntrada.Properties.TodayDate = DateTime.Now;
                dteValidade.Properties.TodayDate = DateTime.Now;
                dteFab.Properties.TodayDate = DateTime.Now;

                if (!string.IsNullOrEmpty(CodClifor))
                {
                    lpCliente.txtcodigo.Text = CodClifor;
                    lpCliente.CarregaDescricao();
                }

                lpFilial.txtcodigo.Text = CodFilial.ToString();
                lpFilial.CarregaDescricao();
            }
            else
            {
                tbCodLote.Text = setCodigoLote();
                dteDataEntrada.Properties.TodayDate = DateTime.Now;
                dteValidade.Properties.TodayDate = DateTime.Now;
                dteFab.Properties.TodayDate = DateTime.Now;

                lpCliente.txtcodigo.Text = CodClifor;
                lpCliente.CarregaDescricao();

                lpFilial.txtcodigo.Text = CodFilial.ToString();
                lpFilial.CarregaDescricao();

                cbImageStatus.SelectedIndex = 0;
            }

            PS.Glb.Class.GoperItemLote.getCodlote(Convert.ToInt32(tbCodLote.Text));
        }

        private bool ValidaNumeroLote()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(tbNumero.Text))
            {
                errorProvider1.SetError(tbNumero, "Favor informar o número do Lote.");
                verifica = false;
            }
            return verifica;
        }

        private bool NumeroLote(string NumeroLote)
        {
            bool verifica = true;

            errorProvider1.Clear();

            bool ExisteLote = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT COUNT(NUMERO) FROM VPRODUTOLOTE WHERE CODEMPRESA = ? AND NUMERO = ? ", new object[] { AppLib.Context.Empresa, NumeroLote }));

            if (ExisteLote == true)
            {
                MessageBox.Show("O número do lote já está sendo usado.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                verifica = false;
            }

            return verifica;
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPRODUTOLOTE WHERE CODEMPRESA = ? AND CODLOTE = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, CodLote, CodFilial });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPRODUTOLOTE WHERE CODEMPRESA = ? AND CODLOTE = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, CodLote, CodFilial });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodLote.Text = dt.Rows[0]["CODLOTE"].ToString();
            tbNumero.Text = dt.Rows[0]["NUMERO"].ToString();
            cbImageStatus.EditValue = dt.Rows[0]["STATUS"];
            if (cbImageStatus.EditValue.ToString() == "1")
            {
                cbImageStatus.SelectedIndex = 0;
            }
            else
            {
                cbImageStatus.SelectedIndex = 1;
            }
            lpCliente.txtcodigo.Text = dt.Rows[0]["CODCLIFOR"].ToString();
            lpCliente.CarregaDescricao();
            lpFilial.txtcodigo.Text = dt.Rows[0]["CODFILIAL"].ToString();
            lpFilial.CarregaDescricao();
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["DATAENTRADA"].ToString()))
            {
                dteDataEntrada.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAENTRADA"]);
            }
            if (!string.IsNullOrEmpty(dt.Rows[0]["DATAVALIDADE"].ToString()))
            {
                dteValidade.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAVALIDADE"]);
            }
            if (!string.IsNullOrEmpty(dt.Rows[0]["DATAFABRICACAO"].ToString()))
            {
                dteFab.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAFABRICACAO"]);
            }
            tbUsuarioCriacao.Text = dt.Rows[0]["USUARIOCRIACAO"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATACRIACAO"].ToString()))
            {
                dteCriacao.DateTime = Convert.ToDateTime(dt.Rows[0]["DATACRIACAO"]);
            }

            tbUsuarioAlteracao.Text = dt.Rows[0]["USUARIOALTERACAO"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATAALTERACAO"].ToString()))
            {
                dteAlteracao.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAALTERACAO"]);
            }
        }

        private bool Salvar()
        {
            if (ValidaNumeroLote() == false)
            {
                return false;
            }

            if (edita == false)
            {
                if (NumeroLote(tbNumero.Text) == false)
                {
                    return false;
                }
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VPRODUTOLOTE = new AppLib.ORM.Jit(conn, "VPRODUTOLOTE");
            conn.BeginTransaction();

            try
            {
                VPRODUTOLOTE.Set("CODEMPRESA", AppLib.Context.Empresa);

                if (!string.IsNullOrEmpty(lpFilial.txtcodigo.Text))
                {
                    VPRODUTOLOTE.Set("CODFILIAL", Convert.ToInt32(lpFilial.txtcodigo.Text));
                }
                else
                {
                    VPRODUTOLOTE.Set("CODFILIAL", null);
                }

                VPRODUTOLOTE.Set("CODLOTE", tbCodLote.Text);

                if (!string.IsNullOrEmpty(lpCliente.txtcodigo.Text))
                {
                    VPRODUTOLOTE.Set("CODCLIFOR", lpCliente.txtcodigo.Text);
                }
                else
                {
                    VPRODUTOLOTE.Set("CODCLIFOR", null);
                }

                VPRODUTOLOTE.Set("NUMERO", tbNumero.Text);
                VPRODUTOLOTE.Set("STATUS", cbImageStatus.EditValue);
                VPRODUTOLOTE.Set("DESCRICAO", tbDescricao.Text);

                if (!string.IsNullOrEmpty(dteDataEntrada.Text))
                {
                    VPRODUTOLOTE.Set("DATAENTRADA", Convert.ToDateTime(dteDataEntrada.Text));
                }
                else
                {
                    VPRODUTOLOTE.Set("DATAENTRADA", null);
                }

                if (!string.IsNullOrEmpty(dteValidade.Text))
                {
                    VPRODUTOLOTE.Set("DATAVALIDADE", Convert.ToDateTime(dteValidade.Text));
                }
                else
                {
                    VPRODUTOLOTE.Set("DATAVALIDADE", null);
                }

                if (!string.IsNullOrEmpty(dteFab.Text))
                {
                    VPRODUTOLOTE.Set("DATAFABRICACAO", Convert.ToDateTime(dteFab.Text));
                }
                else
                {
                    VPRODUTOLOTE.Set("DATAFABRICACAO", null);
                }

                VPRODUTOLOTE.Set("USUARIOCRIACAO", AppLib.Context.Usuario);
                VPRODUTOLOTE.Set("DATACRIACAO", Convert.ToDateTime(DateTime.Now));

                if (edita == true)
                {
                    VPRODUTOLOTE.Set("USUARIOALTERACAO", AppLib.Context.Usuario);
                    VPRODUTOLOTE.Set("DATAALTERACAO", Convert.ToDateTime(DateTime.Now));
                }

                VPRODUTOLOTE.Save();

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
                edita = true;
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

                this.Close();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Métodos 

        /// <summary>
        /// Método que retorna o código sequencial do Lote.
        /// </summary>
        /// <returns>Código do Lote</returns>
        public string setCodigoLote()
        {
            string CodigoLote = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT ISNULL(MAX(CODLOTE), 0) + 1 FROM VPRODUTOLOTE WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();
            return CodigoLote;
        }

        #endregion
    }
}
