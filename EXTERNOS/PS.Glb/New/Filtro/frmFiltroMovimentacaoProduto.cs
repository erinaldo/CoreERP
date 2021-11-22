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
    public partial class frmFiltroMovimentacaoProduto : Form
    {
        public string condicao = string.Empty;
        public DateTime dataInicial, datafinal;
        public string query = string.Empty;
        public bool aberto = false;
        public string Codproduto = string.Empty;

        public frmFiltroMovimentacaoProduto()
        {
            InitializeComponent();
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

        private bool ValidaData()
        {
            if (!string.IsNullOrEmpty(dteInicial.Text) && string.IsNullOrEmpty(dteFinal.Text))
            {
                MessageBox.Show("Os campos de data devem ser informados corretamente.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (string.IsNullOrEmpty(dteInicial.Text) && !string.IsNullOrEmpty(dteFinal.Text))
            {
                MessageBox.Show("Os campos de data devem ser informados corretamente.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void getCondicao()
        {
            try
            {
                if (rbTodos.Checked == true)
                {
                    condicao = " WHERE GOPER.CODSTATUS <> 2 AND VPRODUTO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND VPRODUTO.CODPRODUTO = '"+ Codproduto + "' ORDER BY  GOPER.CODTIPOPER ,GOPER.CODOPER ,GOPERITEM.NSEQITEM";
                }
                else if (rbPeriodo.Checked == true)
                {
                    if (string.IsNullOrEmpty(dteInicial.Text))
                    {

                    }
                    condicao = " WHERE GOPER.CODSTATUS <> 2 AND VPRODUTO.CODEMPRESA = '"+AppLib.Context.Empresa+"' AND VPRODUTO.CODPRODUTO = '"+ Codproduto +"' AND GOPER.DATAEMISSAO >= '"+ string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dteInicial.Text)) + "' AND GOPER.DATAEMISSAO <= '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dteFinal.Text)) + "' ORDER BY GOPER.CODTIPOPER ,GOPER.CODOPER ,GOPERITEM.NSEQITEM";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível carregar as condições do filtro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (aberto == false)
            {
                if (ValidaData() == false)
                {
                    return;
                }

                getCondicao();
                if (!string.IsNullOrEmpty(condicao))
                {
                    this.Dispose();
                }
            }
            else
            {
                if (ValidaData() == false)
                {
                    return;
                }

                getCondicao();
                if (!string.IsNullOrEmpty(condicao))
                {
                    this.Dispose();
                }
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
