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
    public partial class frmCadastroTerceiros : Form
    {
        public bool edita = false;
        public string CodTerceiros = string.Empty;
        public string Codigo = string.Empty;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroTerceiros()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "FTERCEIROS");

            #region Combobox 

            List<PS.Lib.ComboBoxItem> list = new List<PS.Lib.ComboBoxItem>();

            list.Add(new PS.Lib.ComboBoxItem());
            list[0].ValueMember = 0;
            list[0].DisplayMember = "Jurídica";

            list.Add(new PS.Lib.ComboBoxItem());
            list[1].ValueMember = 1;
            list[1].DisplayMember = "Física";

            cmbFisicoJuridico.DataSource = list;
            cmbFisicoJuridico.DisplayMember = "DisplayMember";
            cmbFisicoJuridico.ValueMember = "ValueMember";

            #endregion
        }

        public frmCadastroTerceiros(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "FTERCEIROS");

            #region Combobox

            List<PS.Lib.ComboBoxItem> list = new List<PS.Lib.ComboBoxItem>();

            list.Add(new PS.Lib.ComboBoxItem());
            list[0].ValueMember = 0;
            list[0].DisplayMember = "Jurídica";

            list.Add(new PS.Lib.ComboBoxItem());
            list[1].ValueMember = 1;
            list[1].DisplayMember = "Física";

            cmbFisicoJuridico.DataSource = list;
            cmbFisicoJuridico.DisplayMember = "DisplayMember";
            cmbFisicoJuridico.ValueMember = "ValueMember";

            #endregion

            this.edita = true;
            this.lookup = lookup;
            CodTerceiros = lookup.ValorCodigoInterno;
            carregaCampos();
        }

        private void frmCadastroTerceiros_Load(object sender, EventArgs e)
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
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FTERCEIROS WHERE CODEMPRESA = ? AND CODTERCEIROS = ?", new object[] { AppLib.Context.Empresa, CodTerceiros });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FTERCEIROS WHERE CODEMPRESA = ? AND CODTERCEIROS = ?", new object[] { AppLib.Context.Empresa, CodTerceiros });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodterceiro.Text = dt.Rows[0]["CODTERCEIROS"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            cmbFisicoJuridico.SelectedValue = dt.Rows[0]["FISICOJURIDICO"];
            tbNome.Text = dt.Rows[0]["NOME"].ToString();
            tbCgcCpf.Text = dt.Rows[0]["CNPJCPF"].ToString();
            tbInscricaoEstadual.Text = dt.Rows[0]["INSCRICAOESTADUAL"].ToString();
            tbNumeroRg.Text = dt.Rows[0]["NUMERORG"].ToString();
            tbOREmissor.Text = dt.Rows[0]["OREMISSOR"].ToString();
            lpEstadoEmissor.txtcodigo.Text = dt.Rows[0]["CODETDEMISSOR"].ToString();
            lpEstadoEmissor.CarregaDescricao();

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATACRIACAO"].ToString()))
            {
                dtDataCriacao.DateTime = Convert.ToDateTime(dt.Rows[0]["DATACRIACAO"]);
            }

            tbUsuarioCriacao.Text = dt.Rows[0]["USUARIOCRIACAO"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATAALTERACAO"].ToString()))
            {
                dtDataAlteracao.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAALTERACAO"]);
            }

            tbUsuarioAlteracao.Text = dt.Rows[0]["USUARIOALTERACAO"].ToString();
            tbCep.Text = dt.Rows[0]["CEP"].ToString();
            lpTipoRua.txtcodigo.Text = dt.Rows[0]["CODTIPORUA"].ToString();
            lpTipoRua.CarregaDescricao();
            tbRua.Text = dt.Rows[0]["RUA"].ToString();
            tbNumeroEnd.Text = dt.Rows[0]["NUMEROEND"].ToString();
            tbComplemento.Text = dt.Rows[0]["COMPLEMENTO"].ToString();
            lpTipoBairro.txtcodigo.Text = dt.Rows[0]["CODTIPOBAIRRO"].ToString();
            lpTipoBairro.CarregaDescricao();
            tbBairro.Text = dt.Rows[0]["BAIRRO"].ToString();
            lpEstado.txtcodigo.Text = dt.Rows[0]["CODETD"].ToString();
            lpEstado.CarregaDescricao();
            lpCidade.txtcodigo.Text = dt.Rows[0]["CODCIDADE"].ToString();
            lpCidade.CarregaDescricao();
            tbTelefone.Text = dt.Rows[0]["TELEFONE"].ToString();
            tbCelular.Text = dt.Rows[0]["CELULAR"].ToString();
            tbEmail.Text = dt.Rows[0]["EMAIL"].ToString();
            tbObervacao.Text = dt.Rows[0]["OBSERVACAO"].ToString();
        }

        private void cmbFisicoJuridico_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbCgcCpf.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            if (cmbFisicoJuridico.SelectedIndex == 0)
            {
                tbCgcCpf.Properties.Mask.EditMask = "00.000.000/0000-00";
            }

            if (cmbFisicoJuridico.SelectedIndex == 1)
            {
                tbCgcCpf.Properties.Mask.EditMask = "000.000.000-00";
            }
        }

        private string getCodigo()
        {
            string Codigo = string.Empty;

            Codigo = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT ((REPLICATE(0, (3 - LEN(MAX(CONVERT(INT,ISNULL(FTERCEIROS.CODTERCEIROS,0))))))) +  CONVERT(VARCHAR(9),(MAX(CONVERT(INT,ISNULL(FTERCEIROS.CODTERCEIROS,0)))+1))) FROM FTERCEIROS WHERE FTERCEIROS.CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();
            if (string.IsNullOrEmpty(tbCodterceiro.Text))
            {
                if (string.IsNullOrEmpty(Codigo))
                {
                    Codigo = "001";
                }
            }
            else
            {
                Codigo = tbCodterceiro.Text;
            }
            return Codigo;
        }

        private bool Salvar()
        {
            //if (validacoes() == false)
            //{
            //    return false;
            //}

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit FTERCEIROS = new AppLib.ORM.Jit(conn, "FTERCEIROS");
            conn.BeginTransaction();

            try
            {
                FTERCEIROS.Set("CODEMPRESA", AppLib.Context.Empresa);
                FTERCEIROS.Set("CODTERCEIROS", Codigo = getCodigo());
                FTERCEIROS.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                FTERCEIROS.Set("FISICOJURIDICO", cmbFisicoJuridico.SelectedValue);
                FTERCEIROS.Set("NOME", tbNome.Text);
                FTERCEIROS.Set("CNPJCPF", tbCgcCpf.Text);
                FTERCEIROS.Set("INSCRICAOESTADUAL", tbInscricaoEstadual.Text);
                FTERCEIROS.Set("NUMERORG", tbNumeroRg.Text);
                FTERCEIROS.Set("OREMISSOR", tbOREmissor.Text);

                if (!string.IsNullOrEmpty(lpEstadoEmissor.ValorCodigoInterno))
                {
                    FTERCEIROS.Set("CODETDEMISSOR", lpEstadoEmissor.ValorCodigoInterno);
                }
                else
                {
                    FTERCEIROS.Set("CODETDEMISSOR", null);
                }

                FTERCEIROS.Set("DATACRIACAO", Convert.ToDateTime(DateTime.Now));
                FTERCEIROS.Set("USUARIOCRIACAO", AppLib.Context.Usuario);

                if (edita == true)
                {
                    FTERCEIROS.Set("DATAALTERACAO", Convert.ToDateTime(DateTime.Now));
                    FTERCEIROS.Set("USUARIOALTERACAO", AppLib.Context.Usuario);
                }

                FTERCEIROS.Set("CEP", tbCep.Text);

                if (!string.IsNullOrEmpty(lpTipoRua.ValorCodigoInterno))
                {
                    FTERCEIROS.Set("CODTIPORUA", lpTipoRua.ValorCodigoInterno);
                }
                else
                {
                    FTERCEIROS.Set("CODTIPORUA", null);
                }

                FTERCEIROS.Set("RUA", tbRua.Text);
                FTERCEIROS.Set("NUMEROEND", tbNumeroEnd.Text);
                FTERCEIROS.Set("COMPLEMENTO", tbComplemento.Text);

                if (!string.IsNullOrEmpty(lpTipoBairro.ValorCodigoInterno))
                {
                    FTERCEIROS.Set("CODTIPOBAIRRO", lpTipoBairro.ValorCodigoInterno);
                }
                else
                {
                    FTERCEIROS.Set("CODTIPOBAIRRO", null);
                }

                FTERCEIROS.Set("BAIRRO", tbBairro.Text);

                if (!string.IsNullOrEmpty(lpEstado.ValorCodigoInterno))
                {
                    FTERCEIROS.Set("CODETD", lpEstado.ValorCodigoInterno);
                }
                else
                {
                    FTERCEIROS.Set("CODETD", null);
                }

                if (!string.IsNullOrEmpty(lpCidade.ValorCodigoInterno))
                {
                    FTERCEIROS.Set("CODCIDADE", lpCidade.ValorCodigoInterno);
                }
                else
                {
                    FTERCEIROS.Set("CODCIDADE", null);
                }

                FTERCEIROS.Set("TELEFONE", tbTelefone.Text);
                FTERCEIROS.Set("CELULAR", tbCelular.Text);
                FTERCEIROS.Set("EMAIL", tbEmail.Text);
                FTERCEIROS.Set("OBSERVACAO", tbObervacao.Text);

                FTERCEIROS.Save();
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
