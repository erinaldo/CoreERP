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
    public partial class frmCadastroProdutoFornecedor : Form
    {
        public bool edita = false;
        public string codProduto = string.Empty;
        public string codCliFor = string.Empty;
        public string codPrdFornecedor = string.Empty;


        public frmCadastroProdutoFornecedor()
        {
            InitializeComponent();
            psCodMoeda1.PSPart = "PSPartMoeda";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool validacao()
        {
            if (string.IsNullOrEmpty(txtCodCliFor.Text))
            {
                MessageBox.Show("Favor selecionar o fornecedor.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool Salvar()
        {
            if (validacao() == true)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "VPRODUTOCLIFOR");
                conn.BeginTransaction();
                try
                {
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODPRODUTO", codProduto);
                    v.Set("CODCLIFOR", txtCodCliFor.Text);
                    v.Set("CODPRDFORNECEDOR", txtCodPrdFornecedor.Text);
                    v.Set("CODMOEDA", psCodMoeda1.textBox1.Text);
                    v.Set("ATIVO", cmbAtivo.SelectedText == "SIM" ? 1 : 0);
                    v.Set("VALOR", !string.IsNullOrEmpty(txtValor.Text) ? Convert.ToDecimal(txtValor.Text) : 0);
                    v.Set("DESCONTO", !string.IsNullOrEmpty(txtDesconto.Text) ? Convert.ToDecimal(txtDesconto.Text) : 0);
                    v.Set("LEADTIME", !string.IsNullOrEmpty(txtLeadtime.Text) ? Convert.ToInt32(txtLeadtime.Text) : 0);
                    v.Set("OBS", meObs.Text);
                    v.Save();
                    conn.Commit();
                    return true;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Rollback();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void frmCadastroProdutoFornecedor_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
            }

            cmbAtivo.SelectedIndex = 0;
        }

        private void carregaCampos()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM VPRODUTOCLIFOR WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODCLIFOR = ? AND CODPRDFORNECEDOR = ?", new object[] { AppLib.Context.Empresa, codProduto, codCliFor, codPrdFornecedor });

            if (dt.Rows.Count > 0 )
            {
                txtCodCliFor.Text = dt.Rows[0]["CODCLIFOR"].ToString();
                txtDescricaoCliente.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM VCLIFOR WHERE CODCLIFOR = ?", new object[] { codCliFor }).ToString();
                txtCodPrdFornecedor.Text = dt.Rows[0]["CODPRDFORNECEDOR"].ToString();
                cmbAtivo.Text = dt.Rows[0]["ATIVO"].ToString() == "1" ? "SIM" : "NÃO";
                psCodMoeda1.textBox1.Text = dt.Rows[0]["CODMOEDA"].ToString();
                psCodMoeda1.LoadLookup();
                txtValor.Text = string.Format("{0:n4}", Convert.ToDecimal(dt.Rows[0]["VALOR"]));
                txtDesconto.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["DESCONTO"]));
                txtLeadtime.Text = dt.Rows[0]["LEADTIME"].ToString();
                meObs.Text = dt.Rows[0]["OBS"].ToString();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
            }
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)44)
            {
                e.Handled = true;
            }
        }

        private void txtValor_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtValor.Text))
            {
                txtValor.Text = string.Format("{0:n4}", Convert.ToDecimal(txtValor.Text));
            }
        }

        private void txtDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)44)
            {
                e.Handled = true;
            }
        }

        private void txtDesconto_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDesconto.Text))
            {
                txtDesconto.Text = string.Format("{0:n2}", Convert.ToDecimal(txtDesconto.Text));
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtCodPrdFornecedor.Text = string.Empty;
            cmbAtivo.SelectedText = "SIM";
            txtCodCliFor.Text = string.Empty;
            txtDescricaoCliente.Text = string.Empty;
            psCodMoeda1.textBox1.Text = string.Empty;
            psCodMoeda1.textBox2.Text = string.Empty;
            txtValor.Text = string.Empty;
            txtDesconto.Text = string.Empty;
            txtLeadtime.Text = string.Empty;
            meObs.Text = string.Empty;
        }

        private void txtCodCliFor_Validated(object sender, EventArgs e)
        {
            string filtroUsuario = new Class.Utilidades().getFiltroUsuario("VCLIFOR");
            string sql = @"SELECT CODCLIFOR, NOME FROM VCLIFOR WHERE CODCLIFOR LIKE '%" + txtCodCliFor.Text + "%' AND CODEMPRESA = " + AppLib.Context.Empresa + "";
            if (!string.IsNullOrEmpty(filtroUsuario))
            {

                sql = sql + filtroUsuario;

            }

            DataTable dtCliente = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa });
            if (dtCliente.Rows.Count > 1)
            {
                string where = @"WHERE CODCLIFOR LIKE '%" + txtCodCliFor.Text + "%' AND CODEMPRESA = " + AppLib.Context.Empresa + " " + filtroUsuario;
                PS.Glb.New.Visao.frmVisaoCliente frm = new PS.Glb.New.Visao.frmVisaoCliente(where);
                frm.consulta = true;
                frm.ShowDialog();
                if (!string.IsNullOrEmpty(frm.codCliente))
                {
                    txtCodCliFor.Text = frm.codCliente;
                    txtDescricaoCliente.Text = frm.nome;
                }
            }
            else if (dtCliente.Rows.Count == 1)
            {
                txtCodCliFor.Text = dtCliente.Rows[0]["CODCLIFOR"].ToString();
                txtDescricaoCliente.Text = dtCliente.Rows[0]["NOME"].ToString();
            }
            else
            {
                txtCodCliFor.Text = string.Empty;
                txtDescricaoCliente.Text = string.Empty;
            }
        }

        private void btnLookupCliente_Click(object sender, EventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoCliente frm = new PS.Glb.New.Visao.frmVisaoCliente(" WHERE CODEMPRESA = " + AppLib.Context.Empresa + "");
            frm.consulta = true;
            frm.codCliente = txtCodCliFor.Text;
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.codCliente))
            {
                txtCodCliFor.Text = frm.codCliente;
                txtDescricaoCliente.Text = frm.nome;
            }
        }
    }
}
