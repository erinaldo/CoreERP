using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Filtro
{
    public partial class frmFiltroNotaFiscal : Form
    {
        public string condicao = string.Empty;
        public Form pai = null;
        public bool aberto = false;
        public string codMenu = string.Empty;

        public frmFiltroNotaFiscal()
        {
            InitializeComponent();
        }

        private void getCondicao()
        {
            try
            {
                if (rbTodos.Checked == true)
                {
                    condicao = "WHERE GNFESTADUAL.CODEMPRESA = '" + AppLib.Context.Empresa + "' ";
                }

                else if (rbStatus.Checked == true)
                {
                    condicao = "WHERE GNFESTADUAL.CODSTATUS = '" + cmbStatus.SelectedValue + "'";
                }
                else if (rbHoje.Checked == true)
                {
                    condicao = "WHERE CONVERT(VARCHAR(10),GOPER.DATAEMISSAO,120) = '" + string.Format("{0:yyyy-MM-dd}", AppLib.Context.poolConnection.Get("Start").GetDateTime()) + "'";
                }
                else if (rbNumero.Checked == true)
                {
                    condicao = "WHERE GOPER.NUMERO = '" + cmbStatus.Text + "'";
                }
                else if (rbPeriodo.Checked == true)
                {
                    condicao = "WHERE GOPER.DATAEMISSAO >= '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dteInicial.Text)) + "' AND GOPER.DATAEMISSAO <= '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dteFinal.Text)) + "'";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível carregar as condições do filtro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStatus.Checked == true)
            {
                cmbStatus.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODSTATUS, DESCRICAO FROM GSTATUS WHERE TABELA = ? AND CODEMPRESA = ? AND CODSTATUS NOT IN ('E', 'I')", new object[] { "GNFESTADUAL", AppLib.Context.Empresa });
                cmbStatus.ValueMember = "CODSTATUS";
                cmbStatus.DisplayMember = "DESCRICAO";
                cmbStatus.Visible = true;
                lblValor.Visible = true;
            }
            else
            {
                cmbStatus.Visible = false;
                lblValor.Visible = false;
            }
        }

        private void rbNumero_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNumero.Checked == true)
            {
                cmbStatus.DropDownStyle = ComboBoxStyle.Simple;
                cmbStatus.Text = string.Empty;
                cmbStatus.Visible = true;
                lblValor.Visible = true;
            }
            else
            {
                cmbStatus.DropDownStyle = ComboBoxStyle.DropDown;
                cmbStatus.Visible = false;
                lblValor.Visible = false;
            }
        }

        private void rbPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPeriodo.Checked == true)
            {
                labelControl2.Visible = true;
                labelControl3.Visible = true;
                dteInicial.Visible = true;
                dteFinal.Visible = true;
            }
            else
            {
                labelControl2.Visible = false;
                labelControl3.Visible = false;
                dteInicial.Visible = false;
                dteFinal.Visible = false;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (aberto == false)
            {
                if (rbPeriodo.Checked)
                {
                    if (dteInicial.Text == string.Empty || dteFinal.Text == string.Empty)
                    {
                        MessageBox.Show("Favor informar a data.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                getCondicao();
                PS.Glb.New.Visao.frmVisaoNotaFiscalEstadual Nota = new Visao.frmVisaoNotaFiscalEstadual(condicao, pai, codMenu);
                Nota.Show();
            }
            else
            {
                getCondicao();
            }

            this.Dispose();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
