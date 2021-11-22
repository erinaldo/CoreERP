using ITGProducao.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITGProducao.Formularios
{
    public partial class FrmCentroTrabalhoCusto : Form
    {
        public int MesRef = 0;
        public int AnoRef = 0;
        public string CodCTrabalho = string.Empty;
        public bool edita = false;

        public bool Update = false;

        public FrmCentroTrabalhoCusto()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                MessageBox.Show("Cadastro salvo com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
        }

        private bool Existe()
        {
            bool _verifica = false;
            if (Update == false)
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCENTROTRABALHOCUSTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCTRABALHO = ? AND ANOCUSTOHRCTRABALHO = ? AND MESCUSTOHRCTRABALHO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, CodCTrabalho, txtMesAnoReferencia.Text.Split('/')[1], txtMesAnoReferencia.Text.Split('/')[0] });
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Já existe um custo para este Centro de Trabalho, mês e ano de referencia.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _verifica = true;
                }
            }
            else
            {
                _verifica = false;
            }

            return _verifica;
        }

        private bool Salvar()
        {
            bool _salvar = false;

            if (Existe() == false)
            {
                if (validacao() == true)
                {
                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                    AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PCENTROTRABALHOCUSTO");
                    conn.BeginTransaction();
                    try
                    {
                        v.Set("CODEMPRESA", AppLib.Context.Empresa);
                        v.Set("CODFILIAL", AppLib.Context.Filial);
                        v.Set("CODCTRABALHO", CodCTrabalho);

                        v.Set("ANOCUSTOHRCTRABALHO", txtMesAnoReferencia.Text.Split('/')[1]);
                        v.Set("MESCUSTOHRCTRABALHO", txtMesAnoReferencia.Text.Split('/')[0]);

                        v.Set("CUSTOHRCTRABALHO", string.IsNullOrEmpty(txtCustoHora.Text) == true ? "0" : txtCustoHora.Text);

                        v.Set("DATACALCULO", conn.GetDateTime());
                        v.Set("CUSTOGGFCTRABALHO", string.IsNullOrEmpty(txtCustoGGF.Text) == true ? "0" : txtCustoGGF.Text);

                        v.Set("ATIVOPREVISAO", chkAtivoPrevisao.Checked == true ? 1 : 0);

                        v.Save();
                        conn.Commit();
                        _salvar = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conn.Rollback();
                        _salvar = false;
                    }
                }
                else
                {
                    _salvar = false;
                }
            }
            return _salvar;
        }

        public void VerificaParametro_CUSTOFORMA()
        {
            Global gl = new Global();
            string _CUSTOFORMA = gl.VerificaParametroString("CUSTOFORMA");

            if (_CUSTOFORMA == "1") //MANUAL
            {
                gl.EnableTab(tabControl1.TabPages[0], true);
            }
            else if (_CUSTOFORMA == "2") //CALCULADO
            {
                gl.EnableTab(tabControl1.TabPages[0], false);
            }
            else
            {
                MessageBox.Show("Parâmetro não definido, contate o administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
            }
        }

        private bool validacao()
        {
            bool verifica = true;

            errorProvider.Clear();

            if (string.IsNullOrEmpty(txtMesAnoReferencia.Text))
            {
                errorProvider.SetError(txtMesAnoReferencia, "Favor preencher o Mês/Ano de Referência");
                verifica = false;
            }
            else
            {
                if (chkAtivoPrevisao.Checked == true)
                {
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCENTROTRABALHOCUSTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCTRABALHO = ? AND ANOCUSTOHRCTRABALHO = ? AND ATIVOPREVISAO = 1 AND MESCUSTOHRCTRABALHO <> ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, CodCTrabalho ,txtMesAnoReferencia.Text.Split('/')[1], txtMesAnoReferencia.Text.Split('/')[0] });
                    if (dt.Rows.Count > 0)
                    {
                        errorProvider.SetError(txtMesAnoReferencia, "Já existe um Custo Ativo para este Ano de Referência");
                        verifica = false;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtCustoHora.Text))
            {
                errorProvider.SetError(txtCustoHora, "Favor preencher o Custo Hora");
                verifica = false;
            }

            if (string.IsNullOrEmpty(txtCustoGGF.Text))
            {
                errorProvider.SetError(txtCustoGGF, "Favor preencher o Custo GGF");
                verifica = false;
            }            

            return verifica;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            txtCustoHora.Text = "0";
            txtMesAnoReferencia.Text = string.Empty;
            chkAtivoPrevisao.Checked = false;
            txtCustoGGF.Text ="0";

            VerificaParametro_CUSTOFORMA();

            txtMesAnoReferencia.Focus();
        }

        private void chkAtivoPrevisao_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FrmCentroTrabalhoCusto_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCENTROTRABALHOCUSTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCTRABALHO = ? AND ANOCUSTOHRCTRABALHO = ? AND MESCUSTOHRCTRABALHO = ?  ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, CodCTrabalho, AnoRef, MesRef });
                if (dt.Rows.Count > 0)
                {
                    txtCustoGGF.Text = dt.Rows[0]["CUSTOGGFCTRABALHO"].ToString();
                    txtCustoHora.Text = dt.Rows[0]["CUSTOHRCTRABALHO"].ToString();
                    txtMesAnoReferencia.Text = Convert.ToInt16(dt.Rows[0]["MESCUSTOHRCTRABALHO"].ToString()).ToString("00") + "/" + Convert.ToInt16(dt.Rows[0]["ANOCUSTOHRCTRABALHO"].ToString()).ToString("00");
                    chkAtivoPrevisao.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVOPREVISAO"]);
                }
            }

            VerificaParametro_CUSTOFORMA();

            txtMesAnoReferencia.Focus();
        }
    }
}
