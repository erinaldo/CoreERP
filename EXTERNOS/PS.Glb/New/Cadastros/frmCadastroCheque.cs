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
    public partial class frmCadastroCheque : Form
    {
        public bool edita = false;
        public string Codcheque = string.Empty;
        public bool verifica = false;

        public DateTime Databaixa;
        public decimal Valor;
        public decimal ValorBaixa;
        private bool ControleCheque;
        public int PagRec;
        public string CodConta = string.Empty;
        PS.Glb.New.Processos.Financeiro.frmControleCheque frm;

        private DateTime? Databoa;
        private DateTime? DataCompensacao;
        private DateTime? DataEmissao;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroCheque(bool _controleCheque, PS.Glb.New.Processos.Financeiro.frmControleCheque _frm)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "FCHEQUE");
            ControleCheque = _controleCheque;
            frm = _frm;
        }

        public frmCadastroCheque(ref NewLookup lookup)
        {
            InitializeComponent();
            this.edita = true;
            this.lookup = lookup;
        }

        private void frmCadastroCheque_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                InativaCampos();
                carregaCampos();
                btnOKAtual.Enabled = false;
                btnCancelarAtual.Text = "Fechar";
            }

            if (PagRec == 0)
            {
                tbPagRec.Text = "Pagar";
                tbCodcheque.Enabled = false;
                tbNumero.Enabled = false;
            }
            else
            {
                tbPagRec.Text = "Receber";
                tbCodcheque.Enabled = true;
            }

            if (ControleCheque == true)
            {
                tbCodcheque.Enabled = false;
                tbCodcheque.Text = getCodCheque().ToString();
                tbPagRec.Enabled = false;
                tbValor.Text = Valor.ToString();
                lpContaCaixa.txtcodigo.Text = CodConta;
                lpContaCaixa.CarregaDescricao();
                lpContaCaixa.Enabled = false;

                if (PagRec == 0)
                {
                    int CodNumeroCheque;
                    CodNumeroCheque = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT NUMEROCHEQUE FROM FCONTA WHERE CODEMPRESA = ? AND CODCONTA = ?", new object[] { AppLib.Context.Empresa, lpContaCaixa.txtcodigo.Text }));

                    if (CodNumeroCheque > 0)
                    {
                        if (frm.ListCheque.Count > 0)
                        {
                            var result = frm.ListCheque.Max(x => x.NUMERO);
                            CodNumeroCheque = result + 1;
                        }
                        else
                        {
                            CodNumeroCheque++;
                        }

                        tbNumero.Text = CodNumeroCheque.ToString();
                    }
                }
            }
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FCHEQUE WHERE CODCHEQUE = ?", new object[] { Codcheque });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FCHEQUE WHERE CODCHEQUE = ?", new object[] { Codcheque });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodcheque.Text = dt.Rows[0]["CODCHEQUE"].ToString();
            int PagRec = Convert.ToInt32(dt.Rows[0]["TIPOPAGREC"]);

            if (PagRec == 0)
            {
                tbPagRec.Text = "Pagar";
            }
            else
            {
                tbPagRec.Text = "Receber";
            }

            tbValor.Text = dt.Rows[0]["VALOR"].ToString();
            tbNumero.Text = dt.Rows[0]["NUMERO"].ToString();
            tbValor.Text = dt.Rows[0]["VALOR"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATABOA"].ToString()))
            {
                dteDataboa.DateTime = Convert.ToDateTime(dt.Rows[0]["DATABOA"]);
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["COMPENSADO"].ToString()))
            {
                chkCompensado.Checked = Convert.ToBoolean(dt.Rows[0]["COMPENSADO"]);
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATACOMPENSACAO"].ToString()))
            {
                dteCompensacao.DateTime = Convert.ToDateTime(dt.Rows[0]["DATACOMPENSACAO"]);
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEMISSAO"].ToString()))
            {
                dteDataEmissao.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAEMISSAO"]);
            }

            lpContaCaixa.txtcodigo.Text = dt.Rows[0]["CODCONTA"].ToString();
            tbNumero.Text = dt.Rows[0]["NUMERO"].ToString();
            tbCodBanco.Text = dt.Rows[0]["CODBANCO"].ToString();
            tbCodAgencia.Text = dt.Rows[0]["CODAGENCIA"].ToString();
            tbCodContaCorrente.Text = dt.Rows[0]["CODCCORRENTE"].ToString();
            tbObservacao.Text = dt.Rows[0]["OBSERVACAO"].ToString();
        }

        private void InativaCampos()
        {
            tbCodcheque.Enabled = false;
            tbPagRec.Enabled = false;
            tbValor.Enabled = false;
            dteDataboa.Enabled = false;
            chkCompensado.Enabled = false;
            dteCompensacao.Enabled = false;
            dteDataEmissao.Enabled = false;
            lpContaCaixa.Enabled = false;
            tbNumero.Enabled = false;
            tbCodBanco.Enabled = false;
            tbCodAgencia.Enabled = false;
            tbCodContaCorrente.Enabled = false;
            tbObservacao.Enabled = false;
        }

        private void tbValor_Leave(object sender, EventArgs e)
        {
            if (ControleCheque == true)
            {
                Valor = Convert.ToDecimal(tbValor.Text);
            }
        }

        private void dteDataboa_Leave(object sender, EventArgs e)
        {
            if (ControleCheque == true)
            {
                if (!string.IsNullOrEmpty(dteDataboa.Text))
                {
                    chkCompensado.Enabled = false;
                    chkCompensado.Checked = false;
                }
                else
                {
                    chkCompensado.Enabled = true;
                }
            }
        }

        private void chkCompensado_CheckedChanged(object sender, EventArgs e)
        {
            if (ControleCheque == true)
            {
                if (chkCompensado.Checked)
                {
                    dteCompensacao.DateTime = Databaixa;
                }
                else
                {
                    dteCompensacao.ResetText();
                }
            }
        }

        private void lpContaCaixa_Leave(object sender, EventArgs e)
        {
            if (ControleCheque == true)
            {
                if (PagRec == 0)
                {
                    int CodNumeroCheque;
                    CodNumeroCheque = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT NUMEROCHEQUE FROM FCONTA WHERE CODEMPRESA = ? AND CODCONTA = ?", new object[] { AppLib.Context.Empresa, lpContaCaixa.txtcodigo.Text }));

                    if (CodNumeroCheque > 0)
                    {
                        if (frm.ListCheque.Count > 0)
                        {
                            var result = frm.ListCheque.Max(x => x.NUMERO);
                            CodNumeroCheque = result + 1;
                        }
                        else
                        {
                            CodNumeroCheque++;
                        }

                        tbNumero.Text = CodNumeroCheque.ToString();
                    }
                }
            }
        }

        private bool ValidaContaCaixa()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(lpContaCaixa.txtcodigo.Text))
            {
                errorProvider1.SetError(lpContaCaixa.btnprocurar, "O código da Conta deve ser informado.");
                verifica = false;
            }
            return verifica;
        }

        private bool ValidaValor()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(tbValor.Text) || tbValor.Text == "0,00")
            {
                errorProvider1.SetError(labelControl5, "O valor do cheque deve ser informado.");
                verifica = false;
            }
            return verifica;
        }

        private bool ValidaNumeroCheque()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(tbNumero.Text))
            {
                errorProvider1.SetError(labelControl3, "O número do cheque deve ser informado.");
                verifica = false;
            }
            return verifica;
        }

        private bool ValidaObservacao()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(tbObservacao.Text))
            {
                errorProvider1.SetError(labelControl7, "É necessário informar a descrição do cheque.");
                verifica = false;
            }
            return verifica;
        }

        private bool ValidaDataCompensacao()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(dteCompensacao.Text))
            {
                errorProvider1.SetError(labelControl10, "É necessário informar a data de compensação.");
                verifica = false;
            }
            return verifica;
        }

        private bool ValidaClassificacao()
        {
            if (PagRec == 0)
            {
                if (Convert.ToDecimal(tbValor.Text) < ValorBaixa)
                {
                    MessageBox.Show("Para lançamentos com classificação 'Pagar' é permitido somente um cheque.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private int getCodCheque()
        {
            int codCheque;
            codCheque = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT ISNULL(MAX(CODCHEQUE), 0) + 1 FROM FCHEQUE WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }));

            if (frm.ListCheque.Count > 0)
            {
                var result = frm.ListCheque.Max(x => x.CODCHEQUE);
                codCheque = result + 1;
            }

            return codCheque;
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            // Atribui valor para os campos referentes às datas
            DataCompensacao = dteCompensacao.Text == string.Empty ? new Nullable<DateTime>() : Convert.ToDateTime(dteCompensacao.DateTime.ToShortDateString());
            Databoa = dteDataboa.Text == string.Empty ? new Nullable<DateTime>() : Convert.ToDateTime(dteDataboa.DateTime.ToShortDateString());
            DataEmissao = dteDataEmissao.Text == string.Empty ? new Nullable<DateTime>() : Convert.ToDateTime(dteDataEmissao.DateTime.ToShortDateString());

            if (ControleCheque == true)
            {
                if (ValidaContaCaixa() == false)
                {
                    return;
                }

                if (ValidaNumeroCheque() == false)
                {
                    return;
                }

                if (ValidaValor() == false)
                {
                    return;
                }

                if (ValidaDataCompensacao() == false)
                {
                    return;
                }

                if (ValidaObservacao() == false)
                {
                    return;
                }

                if (ValidaClassificacao() == false)
                {
                    tbValor.Text = ValorBaixa.ToString();
                    return;
                }

                frm.AdicionarLista(AppLib.Context.Empresa, Convert.ToInt32(tbCodcheque.Text), lpContaCaixa.txtcodigo.Text, Convert.ToInt32(tbNumero.Text), tbPagRec.Text == "Receber" ? 1 : 0, Valor, DateTime.Now, AppLib.Context.Usuario, null, null, Databoa, tbObservacao.Text, tbCodBanco.Text, tbCodAgencia.Text, tbCodContaCorrente.Text, DataEmissao , DataCompensacao, chkCompensado.Checked == true ? 1 : 0);
                this.Dispose();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
