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
    public partial class frmFiltroExtrato : Form
    {
        public string condicao = string.Empty;
        public Form pai = null;
        public bool aberto = false;
        public string codMenu = string.Empty;

        public frmFiltroExtrato()
        {
            InitializeComponent();
        }

        private void getCondicao()
        {
            try
            {
                if (rbCompensado.Checked)
                {
                    condicao = "WHERE FEXTRATO.COMPENSADO = 1";
                }
                else if (rbNaoCompensado.Checked)
                {
                    condicao = "WHERE FEXTRATO.COMPENSADO = 0";
                }
                else if (rbDataCompensacao.Checked)
                {
                    condicao = "WHERE FEXTRATO.DATACOMPENSACAO = '" + string.Format("{0:yyyy-MM-dd}", dteInicial.DateTime) + "'";
                }
                else if (rbPeriodoCompensacao.Checked)
                {
                    condicao = "WHERE FEXTRATO.DATACOMPENSACAO >= '" + string.Format("{0:yyyy-MM-dd}", dteInicial.DateTime) + "' AND FEXTRATO.DATACOMPENSACAO <= '" + string.Format("{0:yyyy-MM-dd}", dteFinal.DateTime) + "'";
                }
                else if (rbData.Checked)
                {
                    condicao = "WHERE FEXTRATO.DATA = '" + string.Format("{0:yyyy-MM-dd}", dteInicial.DateTime) + "'";
                }
                else if (rbContaCaixa.Checked)
                {
                    condicao = "WHERE FEXTRATO.CODCONTA = '" + lpContaCaixa.txtcodigo.Text + "'";
                }
                else if (rbExtrato.Checked)
                {
                    condicao = "WHERE FEXTRATO.IDEXTRATO = '" + tbExtrato.Text + "'";
                }
                else if (rbTodos.Checked)
                {
                    condicao = "WHERE FEXTRATO.CODEMPRESA = '" + AppLib.Context.Empresa + "' ";
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível carregar as condições do filtro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbCompensado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCompensado.Checked)
            {
                ValidaGrupoFiltros();
            }
        }

        private void rbNaoCompensado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNaoCompensado.Checked)
            {
                ValidaGrupoFiltros();
            }
        }

        private void rbDataCompensacao_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDataCompensacao.Checked)
            {
                ValidaGrupoFiltros();
                lbData.Visible = true;
                lbData.Text = "Data Compensação";
                lbData.Location = new Point(76, 40);
                dteInicial.Visible = true;
                dteInicial.Location = new Point(76, 59);
            }
            else
            {
                lbData.Visible = false;
                lbData.Text = "Data Inicial";
                lbData.Location = new Point(6, 112);
                dteInicial.Visible = false;
                dteInicial.Location = new Point(6, 128);
            }
        }

        private void rbPeriodoCompensacao_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPeriodoCompensacao.Checked)
            {
                ValidaGrupoFiltros();
                lbData.Visible = true;
                lbData.Text = "Data Inicial";
                lbData.Location = new Point(6, 21);
                dteInicial.Visible = true;
                dteInicial.Location = new Point(6, 40);

                lbDataFinal.Visible = true;
                lbDataFinal.Location = new Point(149, 21);
                dteFinal.Visible = true;
                dteFinal.Location = new Point(149, 40);
            }
            else
            {
                lbData.Visible = false;
                lbData.Text = "Data Inicial";
                lbData.Location = new Point(6, 112);
                dteInicial.Visible = false;
                dteInicial.Location = new Point(6, 128);

                lbDataFinal.Visible = false;
                lbDataFinal.Location = new Point(149, 109);
                dteFinal.Visible = false;
                dteFinal.Location = new Point(149, 128);
            }
        }

        private void rbData_CheckedChanged(object sender, EventArgs e)
        {
            if (rbData.Checked)
            {
                ValidaGrupoCompensacao();
                lbData.Visible = true;
                lbData.Text = "Data";
                lbData.Location = new Point(76, 40);
                dteInicial.Visible = true;
                dteInicial.Location = new Point(76, 59);
            }
            else
            {
                lbData.Visible = false;
                lbData.Text = "Data Inicial";
                lbData.Location = new Point(6, 112);
                dteInicial.Visible = false;
                dteInicial.Location = new Point(6, 128);
            }
        }

        private void rbContaCaixa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbContaCaixa.Checked)
            {
                ValidaGrupoCompensacao();
                lpContaCaixa.Visible = true;
            }
            else
            {
                lpContaCaixa.Visible = false;
            }
        }

        private void rbExtrato_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExtrato.Checked)
            {
                ValidaGrupoCompensacao();
                lblValor.Visible = true;
                lblValor.Text = "Número do Extrato";
                lblValor.Location = new Point(76, 40);
                tbExtrato.Visible = true;
                tbExtrato.Location = new Point(76, 59);
            }
            else
            {
                lblValor.Visible = false;
                lblValor.Text = "Valor";
                lblValor.Location = new Point(6, 66);
                tbExtrato.Visible = false;
                tbExtrato.Location = new Point(149, 86);
            }
        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTodos.Checked)
            {
                ValidaGrupoCompensacao();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (aberto == false)
            {
                if (rbData.Checked)
                {
                    if (dteInicial.Text == string.Empty)
                    {
                        MessageBox.Show("Favor informar a data.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (rbDataCompensacao.Checked)
                {
                    if (dteInicial.Text == string.Empty)
                    {
                        MessageBox.Show("Favor informar a data.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (rbPeriodoCompensacao.Checked)
                {
                    if (dteInicial.Text == string.Empty || dteFinal.Text == string.Empty)
                    {
                        MessageBox.Show("Favor informar a data.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                getCondicao();
                PS.Glb.New.Visao.frmVisaoExtrato Extrato = new Visao.frmVisaoExtrato(condicao, pai, codMenu);
                Extrato.Show();
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

        private void ValidaGrupoFiltros()
        {
            if (rbData.Checked)
            {
                rbData.Checked = false;
            }
            if (rbContaCaixa.Checked)
            {
                rbContaCaixa.Checked = false;
            }
            if (rbExtrato.Checked)
            {
                rbExtrato.Checked = false;
            }
            if (rbTodos.Checked)
            {
                rbTodos.Checked = false;
            }
        }

        private void ValidaGrupoCompensacao()
        {
            if (rbCompensado.Checked)
            {
                rbCompensado.Checked = false;
            }
            if (rbNaoCompensado.Checked)
            {
                rbNaoCompensado.Checked = false;
            }
            if (rbDataCompensacao.Checked)
            {
                rbDataCompensacao.Checked = false;
            }
            if (rbPeriodoCompensacao.Checked)
            {
                rbPeriodoCompensacao.Checked = false;
            }
        }
    }
}
