using ITGProducao.Controles;
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
    public partial class frmCadastroRegraCFOP : Form
    {
        public bool edita = false;
        public string Codigo = string.Empty;
        public int CodFilial;
        public string UfDestino = string.Empty;
        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroRegraCFOP()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VREGRAVARCFOP");

            #region Combobox

            List<PS.Lib.ComboBoxItem> listIcms = new List<Lib.ComboBoxItem>();

            listIcms.Add(new PS.Lib.ComboBoxItem(0, "Margem Valor Agregado (%)"));
            listIcms.Add(new PS.Lib.ComboBoxItem(1, "Pauta (valor)"));
            listIcms.Add(new PS.Lib.ComboBoxItem(2, "Preço Tabelado Máximo (valor)"));
            listIcms.Add(new PS.Lib.ComboBoxItem(3, "Valor da Operação"));

            cbModalidadeIcms.DataSource = listIcms;
            cbModalidadeIcms.DisplayMember = "DisplayMember";
            cbModalidadeIcms.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listIcmsST = new List<Lib.ComboBoxItem>();

            listIcmsST.Add(new PS.Lib.ComboBoxItem(0, "Preço tabelado ou máximo sugerido"));
            listIcmsST.Add(new PS.Lib.ComboBoxItem(1, "Lista Negativa (valor)"));
            listIcmsST.Add(new PS.Lib.ComboBoxItem(2, "Lista Positiva (valor)"));
            listIcmsST.Add(new PS.Lib.ComboBoxItem(3, "Lista Neutra (valor)"));
            listIcmsST.Add(new PS.Lib.ComboBoxItem(4, "Margem Valor Agregado (%)"));
            listIcmsST.Add(new PS.Lib.ComboBoxItem(5, "Pauta (valor)"));

            cbModalidadeIcmsSt.DataSource = listIcmsST;
            cbModalidadeIcmsSt.DisplayMember = "DisplayMember";
            cbModalidadeIcmsSt.ValueMember = "ValueMember";

            #endregion
        }

        public frmCadastroRegraCFOP(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VREGRAVARCFOP");

            #region Combobox

            List<PS.Lib.ComboBoxItem> listIcms = new List<Lib.ComboBoxItem>();

            listIcms.Add(new PS.Lib.ComboBoxItem(0, "Margem Valor Agregado (%)"));
            listIcms.Add(new PS.Lib.ComboBoxItem(1, "Pauta (valor)"));
            listIcms.Add(new PS.Lib.ComboBoxItem(2, "Preço Tabelado Máximo (valor)"));
            listIcms.Add(new PS.Lib.ComboBoxItem(3, "Valor da Operação"));

            cbModalidadeIcms.DataSource = listIcms;
            cbModalidadeIcms.DisplayMember = "DisplayMember";
            cbModalidadeIcms.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listIcmsST = new List<Lib.ComboBoxItem>();

            listIcmsST.Add(new PS.Lib.ComboBoxItem(0, "Preço tabelado ou máximo sugerido"));
            listIcmsST.Add(new PS.Lib.ComboBoxItem(1, "Lista Negativa (valor)"));
            listIcmsST.Add(new PS.Lib.ComboBoxItem(2, "Lista Positiva (valor)"));
            listIcmsST.Add(new PS.Lib.ComboBoxItem(3, "Lista Neutra (valor)"));
            listIcmsST.Add(new PS.Lib.ComboBoxItem(4, "Margem Valor Agregado (%)"));
            listIcmsST.Add(new PS.Lib.ComboBoxItem(5, "Pauta (valor)"));

            cbModalidadeIcmsSt.DataSource = listIcmsST;
            cbModalidadeIcmsSt.DisplayMember = "DisplayMember";
            cbModalidadeIcmsSt.ValueMember = "ValueMember";

            #endregion

            this.edita = true;
            this.lookup = lookup;
            Codigo = lookup.ValorCodigoInterno;
            carregaCampos();
        }

        private void frmCadastroRegraCFOP_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                lpNCM.Grid_WhereVisao.RemoveAt(1);
                carregaCampos();
            }
            else
            {
                CarregaCondicoesLookup();
            }
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VREGRAVARCFOP WHERE NCM = ? AND CODEMPRESA = ? AND CODFILIAL = ? AND UFDESTINO = ?", new object[] { Codigo, AppLib.Context.Empresa, CodFilial, UfDestino });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VREGRAVARCFOP WHERE NCM = ? AND CODEMPRESA = ? AND CODFILIAL = ? AND UFDESTINO = ?", new object[] { Codigo, AppLib.Context.Empresa, CodFilial, UfDestino });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            lpFilial.txtcodigo.Text = dt.Rows[0]["CODFILIAL"].ToString();
            lpFilial.CarregaDescricao();
            lpEstado.txtcodigo.Text = dt.Rows[0]["UFDESTINO"].ToString();
            lpEstado.CarregaDescricao();
            lpNCM.txtcodigo.Text = dt.Rows[0]["NCM"].ToString();
            lpNCM.CarregaDescricao();
            tbAliquotaInterna.Text = dt.Rows[0]["ALIQINTERNA"].ToString();
            tbAliquotaInterestadual.Text = dt.Rows[0]["ALIQINTERESTADUAL"].ToString();
            tbAliquotaInternaImportacao.Text = dt.Rows[0]["ALIQINTERMATIMPORT"].ToString();
            tbMvaOriginal.Text = dt.Rows[0]["MVAORIGINAL"].ToString();
            tbMvaAjustado.Text = dt.Rows[0]["MVAAJUSTADO"].ToString();
            tbMvaAjustadoMaterialImportado.Text = dt.Rows[0]["MVAAJUSTADOMATIMPORT"].ToString();
            cbModalidadeIcms.SelectedValue = Convert.ToInt32(dt.Rows[0]["MODALIDADEICMS"]);
            cbModalidadeIcmsSt.SelectedValue = Convert.ToInt32(dt.Rows[0]["MODALIDADEICMSST"]);
            chkUsaIcmsSt.Checked = Convert.ToBoolean(dt.Rows[0]["USAICMSST"]);
            chkUsaDifalIcmsSt.Checked = Convert.ToBoolean(dt.Rows[0]["DIFALICMSST"]);
            tbCodigoFiscal.Text = dt.Rows[0]["CODFISCAL"].ToString();
            tbPfcp.Text = dt.Rows[0]["PFCP"].ToString();
            tbPfcpST.Text = dt.Rows[0]["PFCPST"].ToString();
        }

        public void CarregaCondicoesLookup()
        {
            lpNCM.Grid_WhereVisao[1].ValorFixo = @" SELECT CODIGO FROM VIBPTAX WHERE CODIGO = '" + Codigo + "'";
            lpNCM.Grid_WhereVisao[1].OutrosFiltros_SelectQuery.Clear();
        }

        private bool Salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VREGRAVARCFOP = new AppLib.ORM.Jit(conn, "VREGRAVARCFOP");
            conn.BeginTransaction();

            try
            {
                VREGRAVARCFOP.Set("CODEMPRESA", AppLib.Context.Empresa);

                if (!string.IsNullOrEmpty(lpFilial.txtcodigo.Text))
                {
                    VREGRAVARCFOP.Set("CODFILIAL", Convert.ToInt32(lpFilial.txtcodigo.Text));
                }
                else
                {
                    VREGRAVARCFOP.Set("CODFILIAL", null);
                }

                if (!string.IsNullOrEmpty(lpEstado.txtcodigo.Text))
                {
                    VREGRAVARCFOP.Set("UFDESTINO", lpEstado.txtcodigo.Text);
                }
                else
                {
                    VREGRAVARCFOP.Set("UFDESTINO", null);
                }

                if (!string.IsNullOrEmpty(lpNCM.txtcodigo.Text))
                {
                    VREGRAVARCFOP.Set("NCM", lpNCM.txtcodigo.Text);
                }
                else
                {
                    VREGRAVARCFOP.Set("NCM", null);
                }

                VREGRAVARCFOP.Set("ALIQINTERNA", Convert.ToDecimal(tbAliquotaInterna.Text));
                VREGRAVARCFOP.Set("ALIQINTERESTADUAL", Convert.ToDecimal(tbAliquotaInterestadual.Text));
                VREGRAVARCFOP.Set("ALIQINTERMATIMPORT", Convert.ToDecimal(tbAliquotaInternaImportacao.Text));

                VREGRAVARCFOP.Set("MVAORIGINAL", Convert.ToDecimal(tbMvaOriginal.Text));
                VREGRAVARCFOP.Set("MVAAJUSTADO", Convert.ToDecimal(tbMvaAjustado.Text));
                VREGRAVARCFOP.Set("MVAAJUSTADOMATIMPORT", Convert.ToDecimal(tbMvaAjustadoMaterialImportado.Text));

                VREGRAVARCFOP.Set("MODALIDADEICMS", cbModalidadeIcms.SelectedValue);
                VREGRAVARCFOP.Set("MODALIDADEICMSST", cbModalidadeIcmsSt.SelectedValue);

                VREGRAVARCFOP.Set("DIFALICMSST", chkUsaIcmsSt.Checked == true ? 1 : 0);
                VREGRAVARCFOP.Set("USAICMSST", chkUsaDifalIcmsSt.Checked == true ? 1 : 0);

                VREGRAVARCFOP.Set("DIFALICMSSTESPECIAL", 0);
                VREGRAVARCFOP.Set("USADECRETOCONVENIO", null);
                VREGRAVARCFOP.Set("CODFISCAL", tbCodigoFiscal.Text);
                VREGRAVARCFOP.Set("PFCP", Convert.ToDecimal(tbPfcp.Text));
                VREGRAVARCFOP.Set("PFCPST", Convert.ToDecimal(tbPfcpST.Text));

                VREGRAVARCFOP.Save();
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
