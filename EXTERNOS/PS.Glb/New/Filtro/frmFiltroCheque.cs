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
    public partial class frmFiltroCheque : Form
    {
        public string condicao = string.Empty;
        public Form pai = null;
        public bool aberto = false;
        public string codMenu = string.Empty;

        public frmFiltroCheque()
        {
            InitializeComponent();
        }

        private void getCondicao()
        {
            try
            {
                if (rbCompensado.Checked)
                {
                    condicao = "WHERE FCHEQUE.COMPENSADO = 1";
                }
                else if (rbDataCompensacao.Checked)
                {
                    condicao = "WHERE FCHEQUE.DATACOMPENSACAO = '" + string.Format("{0:yyyy-MM-dd}", dteDataCompensacao.DateTime) + "'";
                }
                else if (rbTodos.Checked)
                {
                    condicao = "WHERE FCHEQUE.CODEMPRESA = '" + AppLib.Context.Empresa + "' ";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível carregar as condições do filtro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbDataCompensacao_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDataCompensacao.Checked)
            {
                lbData.Visible = true;
                dteDataCompensacao.Visible = true;
            }
            else
            {
                lbData.Visible = false;
                dteDataCompensacao.Visible = false;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (aberto == false)
            {
                getCondicao();
                PS.Glb.New.Visao.frmVisaoCheque Cheque = new Visao.frmVisaoCheque(condicao, pai, codMenu);
                Cheque.Show();
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
