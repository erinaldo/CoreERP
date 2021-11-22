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
    public partial class frmFiltroProdCliFor : Form
    {
        public string condicao = string.Empty;
        public string query = string.Empty;
        public bool aberto = false;
        public string Codproduto = string.Empty;

        public frmFiltroProdCliFor()
        {
            InitializeComponent();
        }

        private void rbClassificacao_CheckedChanged(object sender, EventArgs e)
        {
            if (rbClassificacao.Checked == true)
            {
                cmbStatus.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODCLASSIFICACAO CODIGO, CASE CODCLASSIFICACAO WHEN 0 THEN 'CLIENTE' WHEN 1 THEN 'FORNECEDOR' ELSE 'AMBOS' END DESCRICAO FROM VCLIFOR GROUP BY CODCLASSIFICACAO ORDER BY CODCLASSIFICACAO ", new object[] { });
                cmbStatus.ValueMember = "CODIGO";
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

        private void getCondicao()
        {
            try
            {
                if (rbTodos.Checked == true)
                {
                    condicao = " WHERE VCLIFOR.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND VCLIFOR.CODCLIFOR IN (SELECT GOPER.CODCLIFOR FROM VPRODUTO INNER JOIN GOPERITEM ON GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO INNER JOIN GOPER ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER INNER JOIN GTIPOPER ON GTIPOPER.CODEMPRESA = GOPER.CODEMPRESA AND GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER WHERE GOPER.CODSTATUS <> 2 AND VPRODUTO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND VPRODUTO.CODPRODUTO = '" + Codproduto + "')";
                }
                else if (rbClassificacao.Checked == true)
                {
                    condicao = " WHERE VCLIFOR.CODEMPRESA = '" + AppLib.Context.Empresa +"' AND VCLIFOR.CODCLASSIFICACAO = '"+ cmbStatus.SelectedValue+ "' AND CODCLIFOR IN (SELECT GOPER.CODCLIFOR FROM VPRODUTO INNER JOIN GOPERITEM ON GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO INNER JOIN GOPER ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER INNER JOIN GTIPOPER ON GTIPOPER.CODEMPRESA = GOPER.CODEMPRESA AND GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER WHERE GOPER.CODSTATUS <> 2 AND VPRODUTO.CODEMPRESA = '" +AppLib.Context.Empresa+ "' AND VPRODUTO.CODPRODUTO = '" +Codproduto+ "')";
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
                getCondicao();
                if (!string.IsNullOrEmpty(condicao))
                {
                    this.Dispose();
                }
            }
            else
            {
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
