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
    public partial class frmCadastroExtrato : Form
    {
        public bool edita = false;
        public string IdExtrato = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;
        private bool UsaTransf = false;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroExtrato()
        {
            InitializeComponent();
            CarregaCombo();
            new Class.Utilidades().getDicionario(this, tabControl1, "FEXTRATO");
        }

        public frmCadastroExtrato(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "FCONTA");

            this.edita = true;
            this.lookup = lookup;
            IdExtrato = lookup.ValorCodigoInterno;
            CarregaCombo();
            carregaCampos();
        }

        private void frmCadastroExtrato_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                tbIdExtrato.Enabled = false;
                chkCompensado.Enabled = false;
                dteDataCompensacao.Enabled = false;
                carregaCampos();

                if (ValidaInativacao() == true)
                {
                    InativaCampos();
                }
            }
            else
            {
                tbIdExtrato.Text = setIdExtrato();
                tbIdExtrato.Enabled = false;
                tbCodCheque.Enabled = false;

                dteData.Text = DateTime.Now.ToShortDateString();

                //Transferência
                lpFilialTransf.Visible = false;
                lpContaCaixaTransf.Visible = false;
                lpCentroCustoTransf.Visible = false;
                lpNaturezaOrcamentariaTransf.Visible = false;
            }
        }

        private void CarregaCombo()
        {
            List<PS.Lib.ComboBoxItem> Operacao = new List<PS.Lib.ComboBoxItem>();
            Operacao.Add(new PS.Lib.ComboBoxItem(0, "Entrada"));
            Operacao.Add(new PS.Lib.ComboBoxItem(1, "Saida"));
            Operacao.Add(new PS.Lib.ComboBoxItem(2, "Transferência"));
            Operacao.Add(new PS.Lib.ComboBoxItem(4, "Cheque Saída"));
            Operacao.Add(new PS.Lib.ComboBoxItem(5, "Cheque Entrada"));

            cbTipoOperacao.DataSource = Operacao;
            cbTipoOperacao.DisplayMember = "DisplayMember";
            cbTipoOperacao.ValueMember = "ValueMember";
        }

        private void cbTipoOperacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTipoOperacao.Text == "Transferência")
            {
                lpFilialTransf.Visible = true;
                lpContaCaixaTransf.Visible = true;
                lpCentroCustoTransf.Visible = true;
                lpNaturezaOrcamentariaTransf.Visible = true;

                UsaTransf = true;
            }
            else
            {
                lpFilialTransf.Visible = false;
                lpContaCaixaTransf.Visible = false;
                lpCentroCustoTransf.Visible = false;
                lpNaturezaOrcamentariaTransf.Visible = false;

                UsaTransf = false;
            }
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FEXTRATO WHERE IDEXTRATO = ?", new object[] { IdExtrato });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FEXTRATO WHERE IDEXTRATO = ?", new object[] { IdExtrato });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbIdExtrato.Text = dt.Rows[0]["IDEXTRATO"].ToString();
            lpFilial.txtcodigo.Text = dt.Rows[0]["CODFILIAL"].ToString();
            lpFilial.CarregaDescricao();

            if (Convert.ToInt32(dt.Rows[0]["TIPO"]) == 6)
            {
                cbTipoOperacao.Text = "Transferência";
            }
            else
            {
                cbTipoOperacao.SelectedValue = dt.Rows[0]["TIPO"];
            }
            
            tbNumeroDocumento.Text = dt.Rows[0]["NUMERODOCUMENTO"].ToString();
            lpContaCaixa.txtcodigo.Text = dt.Rows[0]["CODCONTA"].ToString();
            lpContaCaixa.CarregaDescricao();
            tbCodCheque.Text = dt.Rows[0]["CODCHEQUE"].ToString();
            tbValor.Text = dt.Rows[0]["VALOR"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATA"].ToString()))
            {
                dteData.DateTime = Convert.ToDateTime(dt.Rows[0]["DATA"]);
            }

            chkCompensado.Checked = Convert.ToBoolean(dt.Rows[0]["COMPENSADO"]);

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATACOMPENSACAO"].ToString()))
            {
                dteDataCompensacao.DateTime = Convert.ToDateTime(dt.Rows[0]["DATACOMPENSACAO"]);
            } 

            tbHistorico.Text = dt.Rows[0]["HISTORICO"].ToString();
            lpCentroCusto.txtcodigo.Text = dt.Rows[0]["CODCCUSTO"].ToString();
            lpCentroCusto.CarregaDescricao();
            lpNaturezaOrcamentaria.txtcodigo.Text = dt.Rows[0]["CODNATUREZAORCAMENTO"].ToString();
            lpNaturezaOrcamentaria.CarregaDescricao();

            //Transferência
            if (!string.IsNullOrEmpty(dt.Rows[0]["CODFILIALTRF"].ToString()))
            {
                lpFilialTransf.Visible = true;
                lpFilialTransf.txtcodigo.Text = dt.Rows[0]["CODFILIALTRF"].ToString();
                lpFilialTransf.CarregaDescricao();
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["CODCONTATRF"].ToString()))
            {
                lpContaCaixaTransf.Visible = true;
                lpContaCaixaTransf.txtcodigo.Text = dt.Rows[0]["CODCONTATRF"].ToString();
                lpContaCaixaTransf.CarregaDescricao();
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["CODCCUSTOTRANSF"].ToString()))
            {
                lpCentroCustoTransf.Visible = true;
                lpCentroCustoTransf.txtcodigo.Text = dt.Rows[0]["CODCCUSTOTRANSF"].ToString();
                lpCentroCustoTransf.CarregaDescricao();
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["CODNATUREZAORCAMENTOTRANSF"].ToString()))
            {
                lpNaturezaOrcamentariaTransf.Visible = true;
                lpNaturezaOrcamentariaTransf.txtcodigo.Text = dt.Rows[0]["CODNATUREZAORCAMENTOTRANSF"].ToString();
                lpNaturezaOrcamentariaTransf.CarregaDescricao();
            }
        }

        private void InativaCampos()
        {
            tbIdExtrato.Enabled = false;
            lpFilial.Enabled = false;
            cbTipoOperacao.Enabled = false;
            tbNumeroDocumento.Enabled = false;
            lpContaCaixa.Enabled = false;
            tbCodCheque.Enabled = false;
            tbValor.Enabled = false;
            dteData.Enabled = false;
            chkCompensado.Enabled = false;
            dteDataCompensacao.Enabled = false;

            //Transferência
            lpFilialTransf.Enabled = false;
            lpContaCaixaTransf.Enabled = false;
            lpCentroCustoTransf.Enabled = false;
            lpNaturezaOrcamentariaTransf.Enabled = false;
        }

        private bool ValidaInativacao()
        {
            int Idextrato = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT COUNT(IDEXTRATO) FROM FEXTRATOLANCA WHERE CODEMPRESA = ? AND IDEXTRATO = ?", new object[] { AppLib.Context.Empresa, tbIdExtrato.Text }));

            if (chkCompensado.Checked || Idextrato > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidaNumeroDocumento()
        {
            bool verifica = true;

            if (string.IsNullOrEmpty(tbNumeroDocumento.Text))
            {
                errorProvider1.SetError(tbNumeroDocumento, "O Número do Documento precisa ser preenchido.");
                verifica = false;
            }

            return verifica;
        }

        private bool ValidaHistorico()
        {
            bool verifica = true;

            if (string.IsNullOrEmpty(tbHistorico.Text))
            {
                errorProvider1.SetError(tbHistorico, "O histórico do Extrato precisa ser preenchido.");
                verifica = false;
            }

            return verifica;
        }

        private bool ValidaData()
        {
            bool verifica = true;

            if (!(dteDataCompensacao.DateTime >= dteData.DateTime))
            {
                verifica = false;
            }

            return verifica;
        }

        public string setIdExtrato()
        {
            string IdExtrato = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT ISNULL(MAX(IDEXTRATO), 0) + 1 FROM FEXTRATO WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();
            return IdExtrato;
        }

        private void lpContaCaixaTransf_Leave(object sender, EventArgs e)
        {
            if (lpContaCaixaTransf.txtcodigo.Text == lpContaCaixa.txtcodigo.Text)
            {
                MessageBox.Show("A Conta Caixa de destino não pode ser a mesma que a Conta Caixa de origem.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lpContaCaixaTransf.Clear();
                return;
            }
        }

        private void tbValor_Leave(object sender, EventArgs e)
        {
            tbValor.Text = string.Format("{0:n2}", Convert.ToDecimal(tbValor.Text));
        }

        private void chkCompensado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCompensado.Checked)
            {
                dteDataCompensacao.DateTime = Convert.ToDateTime(dteData.Text);
            }
            else
            {
                dteDataCompensacao.Text = string.Empty;
            }
        }

        private void tbValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && !(e.KeyChar == (char)8) && !(e.KeyChar == (char)44))
            {
                e.Handled = true;
            }
        }

        private void tbValor_TextChanged(object sender, EventArgs e)
        {
            Class.Funcoes.Moeda(ref tbValor);
        }

        private bool Salvar()
        {
            if (ValidaNumeroDocumento() == false)
            {
                return false;
            }

            if (ValidaHistorico() == false)
            {
                return false;
            }

            if (ValidaData() == false)
            {
                MessageBox.Show("A data de Compensação não pode ser menor que a data do Extrato.", "Informaçã do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit FEXTRATO = new AppLib.ORM.Jit(conn, "FEXTRATO");
            conn.BeginTransaction();

            try
            {
                FEXTRATO.Set("CODEMPRESA", AppLib.Context.Empresa);
                FEXTRATO.Set("IDEXTRATO", tbIdExtrato.Text);

                if (!string.IsNullOrEmpty(lpFilial.txtcodigo.Text))
                {
                    FEXTRATO.Set("CODFILIAL", lpFilial.txtcodigo.Text);
                }
                else
                {
                    FEXTRATO.Set("CODFILIAL", null);
                }

                if (edita == false)
                {
                    FEXTRATO.Set("TIPO", cbTipoOperacao.SelectedValue);
                }
              
                FEXTRATO.Set("NUMERODOCUMENTO", tbNumeroDocumento.Text);

                if (!string.IsNullOrEmpty(lpContaCaixa.txtcodigo.Text))
                {
                    FEXTRATO.Set("CODCONTA", lpContaCaixa.txtcodigo.Text);
                }
                else
                {
                    FEXTRATO.Set("CODCONTA", null);
                }

                if (!string.IsNullOrEmpty(tbCodCheque.Text))
                {
                   FEXTRATO.Set("CODCHEQUE", tbCodCheque.Text);
                }

                FEXTRATO.Set("VALOR", Convert.ToDecimal(tbValor.Text));

                if (!string.IsNullOrEmpty(dteData.Text))
                {
                    FEXTRATO.Set("DATA", Convert.ToDateTime(dteData.DateTime));
                }
    
                FEXTRATO.Set("COMPENSADO", chkCompensado.Checked == true ? 1 : 0);

                if (!string.IsNullOrEmpty(dteDataCompensacao.Text))
                {
                    FEXTRATO.Set("DATACOMPENSACAO", Convert.ToDateTime(dteDataCompensacao.DateTime));
                }
             
                FEXTRATO.Set("HISTORICO", tbHistorico.Text);

                if (!string.IsNullOrEmpty(lpCentroCusto.txtcodigo.Text))
                {
                    FEXTRATO.Set("CODCCUSTO", lpCentroCusto.txtcodigo.Text);
                }
                else
                {
                    FEXTRATO.Set("CODCCUSTO", null);
                }

                if (!string.IsNullOrEmpty(lpNaturezaOrcamentaria.txtcodigo.Text))
                {
                    FEXTRATO.Set("CODNATUREZAORCAMENTO", lpNaturezaOrcamentaria.txtcodigo.Text);
                }
                else
                {
                    FEXTRATO.Set("CODNATUREZAORCAMENTO", null);
                }

                //Transferência
                if (!string.IsNullOrEmpty(lpFilialTransf.txtcodigo.Text))
                {
                    FEXTRATO.Set("CODFILIALTRF", lpFilialTransf.txtcodigo.Text);
                }
                else
                {
                    FEXTRATO.Set("CODFILIALTRF", null);
                }

                if (!string.IsNullOrEmpty(lpContaCaixaTransf.txtcodigo.Text))
                {
                    FEXTRATO.Set("CODCONTATRF", lpContaCaixaTransf.txtcodigo.Text);
                }
                else
                {
                    FEXTRATO.Set("CODCONTATRF", null);
                }

                if (!string.IsNullOrEmpty(lpCentroCustoTransf.txtcodigo.Text))
                {
                    FEXTRATO.Set("CODCCUSTOTRANSF", lpCentroCustoTransf.txtcodigo.Text);
                }
                else
                {
                    FEXTRATO.Set("CODCCUSTOTRANSF", null);
                }

                if (!string.IsNullOrEmpty(lpNaturezaOrcamentariaTransf.txtcodigo.Text))
                {
                    FEXTRATO.Set("CODNATUREZAORCAMENTOTRANSF", lpNaturezaOrcamentariaTransf.txtcodigo.Text);
                }
                else
                {
                    FEXTRATO.Set("CODNATUREZAORCAMENTOTRANSF", null);
                }

                FEXTRATO.Save();
                conn.Commit();

                string UltimoIdExtrato = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT IDLOG FROM GLOG WHERE CODEMPRESA = ? AND CODTABELA = 'FEXTRATO'", new object[] { AppLib.Context.Empresa }).ToString();

                if (UltimoIdExtrato != tbIdExtrato.Text)
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GLOG SET IDLOG = ? WHERE CODEMPRESA = ? AND CODTABELA = 'FEXTRATO'", new object[] { tbIdExtrato.Text, AppLib.Context.Empresa });
                }         

                if (edita == false)
                {
                    if (UsaTransf == true)
                    {
                        int IdExtratoTransf = SalvarTransferencia();

                        if (IdExtratoTransf > 0)
                        {
                            conn.ExecTransaction("UPDATE FEXTRATO SET IDEXTRATOTRF = ? WHERE CODEMPRESA = ? AND IDEXTRATO = ?", new object[] { IdExtratoTransf, AppLib.Context.Empresa, tbIdExtrato.Text });
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GLOG SET IDLOG = ? WHERE CODEMPRESA = ? AND CODTABELA = 'FEXTRATO'", new object[] { IdExtratoTransf, AppLib.Context.Empresa });
                        }
                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private int SalvarTransferencia()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit FEXTRATO = new AppLib.ORM.Jit(conn, "FEXTRATO");

            int IdExtrato = Convert.ToInt32(setIdExtrato());

            try
            {
                FEXTRATO.Set("CODEMPRESA", AppLib.Context.Empresa);
                FEXTRATO.Set("IDEXTRATO", IdExtrato);

                if (!string.IsNullOrEmpty(lpFilialTransf.txtcodigo.Text))
                {
                    FEXTRATO.Set("CODFILIAL", lpFilialTransf.txtcodigo.Text);
                }
                else
                {
                    FEXTRATO.Set("CODFILIAL", null);
                }

                FEXTRATO.Set("TIPO", 6);
                FEXTRATO.Set("NUMERODOCUMENTO", tbNumeroDocumento.Text);

                if (!string.IsNullOrEmpty(lpContaCaixaTransf.txtcodigo.Text))
                {
                    FEXTRATO.Set("CODCONTA", lpContaCaixaTransf.txtcodigo.Text);
                }
                else
                {
                    FEXTRATO.Set("CODCONTA", null);
                }

                if (!string.IsNullOrEmpty(tbCodCheque.Text))
                {
                    FEXTRATO.Set("CODCHEQUE", tbCodCheque.Text);
                }

                FEXTRATO.Set("VALOR", Convert.ToDecimal(tbValor.Text));
                FEXTRATO.Set("DATA", Convert.ToDateTime(dteData.DateTime));
                FEXTRATO.Set("COMPENSADO", chkCompensado.Checked == true ? 1 : 0);
                FEXTRATO.Set("DATACOMPENSACAO", Convert.ToDateTime(dteDataCompensacao.DateTime));
                FEXTRATO.Set("HISTORICO", tbHistorico.Text);

                if (!string.IsNullOrEmpty(lpCentroCustoTransf.txtcodigo.Text))
                {
                    FEXTRATO.Set("CODCCUSTO", lpCentroCustoTransf.txtcodigo.Text);
                }
                else
                {
                    FEXTRATO.Set("CODCCUSTO", null);
                }

                if (!string.IsNullOrEmpty(lpNaturezaOrcamentariaTransf.txtcodigo.Text))
                {
                    FEXTRATO.Set("CODNATUREZAORCAMENTO", lpNaturezaOrcamentariaTransf.txtcodigo.Text);
                }
                else
                {
                    FEXTRATO.Set("CODNATUREZAORCAMENTO", null);
                }

                FEXTRATO.Save();
                return IdExtrato;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
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
