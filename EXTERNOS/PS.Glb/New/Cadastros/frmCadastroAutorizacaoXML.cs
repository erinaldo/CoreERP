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
    public partial class frmCadastroAutorizacaoXML : Form
    {
        public bool edita = false;
        public string Codfilial = string.Empty;
        public int Identificador;

        public frmCadastroAutorizacaoXML()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GFILIALAUTXML");

            #region Combobox

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = 0;
            list2[0].DisplayMember = "Jurídica";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = 1;
            list2[1].DisplayMember = "Física";

            cmbFisicoJuridico.DataSource = list2;
            cmbFisicoJuridico.DisplayMember = "DisplayMember";
            cmbFisicoJuridico.ValueMember = "ValueMember";

            #endregion
        }

        private void frmCadastroAutorizacaoXML_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
            }
            lpFilial.txtcodigo.Text = Codfilial;
            lpFilial.CarregaDescricao();
            lpFilial.Enabled = false;
        }

        private void cmbFisicoJuridico_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbCNPJCPF.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            if (cmbFisicoJuridico.SelectedIndex == 0)
            {
                tbCNPJCPF.Properties.Mask.EditMask = "00.000.000/0000-00";
            }

            if (cmbFisicoJuridico.SelectedIndex == 1)
            {
                tbCNPJCPF.Properties.Mask.EditMask = "000.000.000-00";
            }
        }

        private void carregaCampos()
        {
            DataTable dt;

            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GFILIALAUTXML WHERE IDENTIFICADOR = ? AND CODFILIAL = ?", new object[] { Identificador, Codfilial });
            if (dt.Rows.Count > 0)
            {
                carregaObj(dt);
            }
        }

        private void carregaObj(DataTable dt)
        {
            lpFilial.txtcodigo.Text = dt.Rows[0]["CODFILIAL"].ToString();
            lpFilial.CarregaDescricao();
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            tbCNPJCPF.Text = dt.Rows[0]["CNPJCPF"].ToString();
        }

        private bool Salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit GFILIALAUTXML = new AppLib.ORM.Jit(conn, "GFILIALAUTXML");
            conn.BeginTransaction();

            try
            {
                if (edita == false)
                {
                    Identificador = getIdentificador();
                    GFILIALAUTXML.Set("IDENTIFICADOR", Identificador);
                }
                else
                {
                    GFILIALAUTXML.Set("IDENTIFICADOR", Identificador);
                }

                GFILIALAUTXML.Set("CODEMPRESA", AppLib.Context.Empresa);

                if (!string.IsNullOrEmpty(lpFilial.txtcodigo.Text))
                {
                    GFILIALAUTXML.Set("CODFILIAL", Convert.ToInt32(lpFilial.txtcodigo.Text));
                }
                else
                {
                    GFILIALAUTXML.Set("CODFILIAL", null);
                }

                GFILIALAUTXML.Set("CNPJCPF", tbCNPJCPF.Text);
                GFILIALAUTXML.Set("DESCRICAO", tbDescricao.Text);

                GFILIALAUTXML.Save();
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
            Salvar();
            edita = true;
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
            }
            else
            {
                this.Dispose();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Métodos 

        /// <summary>
        /// Método que retorna o Identificador
        /// </summary>
        /// <returns>Identificador</returns>
        public int getIdentificador()
        {
            int Id;

            Id = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT ISNULL(MAX(IDENTIFICADOR), 0) + 1 FROM GFILIALAUTXML WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, Codfilial }));
            return Id;
        }

        #endregion

        #region 

        private bool ValidaId()
        {
            bool verifica = true;

            int Valida = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(IDENTIFICADOR) FROM GFILIALAUTXML WHERE CODFILIAL = ? AND IDENTIFICADOR = ?", new object[] { Codfilial, Identificador }));

            if (Valida >= 1)
            {
                verifica = false;
            }

            return verifica;
        }

        #endregion
    }
}
