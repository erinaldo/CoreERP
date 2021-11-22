using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Processos.Operacao
{
    public partial class frmConsultaSaldoEstoque : Form
    {

        public frmConsultaSaldoEstoque()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VFICHAESTOQUE");
        }

        public frmConsultaSaldoEstoque(string _codProduto)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VFICHAESTOQUE");

            if (!string.IsNullOrEmpty(_codProduto))
            {
                txtCodProduto.Text = _codProduto;
            }
        }

        private void psLookupCODPRODUTO_AfterLookup(object sender, Lib.LookupEventArgs e)
        {
            getSaldo();
        }

        private void getSaldo()
        {
            DataTable dtSaldo = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT

 ISNULL(SUM(VSALDOESTOQUE.SALDOFINAL), 0) SALDOFINAL
, ISNULL(SUM(VSALDOESTOQUE.TOTALFINAL), 0) TOTALFINAL
FROM 
VSALDOESTOQUE
WHERE
    VSALDOESTOQUE.CODEMPRESA = ?
AND VSALDOESTOQUE.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, txtCodProduto.Text });

            if (dtSaldo.Rows.Count > 0)
            {
                txtSaldoAtual.Text = string.Format("{0:n2}", dtSaldo.Rows[0]["SALDOFINAL"]);
                txtSaldoFinanceiro.Text = string.Format("{0:n2}", dtSaldo.Rows[0]["TOTALFINAL"]);
            }
        }

        private void getVisaoSaldo()
        {
            try
            {
                gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
 X.Empresa
,X.Filial
,X.Local
,X.Saldo AS 'Saldo Atual'
,X.UN AS 'Unidade'
,X.Pedidos AS 'Pedidos Pendentes'
,(X.Saldo - X.Pedidos) AS 'Saldo Disponível'

FROM(
SELECT
 VSALDOESTOQUE.CODEMPRESA as 'Empresa'  
,VSALDOESTOQUE.CODFILIAL as 'Filial'  
,VSALDOESTOQUE.CODLOCAL as 'Local'  
,VSALDOESTOQUE.SALDOFINAL as 'Saldo'  
,VSALDOESTOQUE.CODUNIDCONTROLE as 'UN'  

,ISNULL((SELECT 
ISNULL(SUM(
	CASE
		WHEN GOPER.CODSTATUS = 0 THEN ISNULL(GOPERITEM.QUANTIDADE,0)
		WHEN GOPER.CODSTATUS = 5 THEN ISNULL(GOPERITEM.QUANTIDADE_SALDO,0)
		WHEN GOPER.CODSTATUS = 7 THEN ISNULL(GOPERITEM.QUANTIDADE,0)
		ELSE 0
	END),0) 

		FROM 
		VPRODUTO
		INNER JOIN GOPERITEM ON GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO
		INNER JOIN GOPER ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER

			WHERE 
				GOPER.CODEMPRESA = VSALDOESTOQUE.CODEMPRESA
			AND GOPER.CODTIPOPER = '2.1.02'
			AND GOPER.CODSTATUS IN (0,5,7)
			AND GOPERITEM.CODPRODUTO = VSALDOESTOQUE.CODPRODUTO),0) AS 'Pedidos'



FROM   
VSALDOESTOQUE  
  
WHERE 
VSALDOESTOQUE.CODEMPRESA = ?  
AND VSALDOESTOQUE.CODPRODUTO = ?

)X", new object[] {AppLib.Context.Empresa, txtCodProduto.Text });
//                gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT

// VSALDOESTOQUE.CODFILIAL as 'Filial'
//,VSALDOESTOQUE.CODLOCAL as 'Local'
//,VSALDOESTOQUE.SALDOFINAL as 'Saldo'
//,VSALDOESTOQUE.CODUNIDCONTROLE as 'UN'
//--,VSALDOESTOQUE.TOTALFINAL as 'Valor'
//--,VSALDOESTOQUE.CUSTOMEDIO as 'Custo Médio'

//FROM 
//VSALDOESTOQUE

//WHERE
//    VSALDOESTOQUE.CODEMPRESA = ?
//AND VSALDOESTOQUE.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, txtCodProduto.Text });

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLookupProduto_Click(object sender, EventArgs e)
        {
            Visao.frmVisaoProduto frm = new Visao.frmVisaoProduto(" WHERE VPRODUTO.CODEMPRESA = " + AppLib.Context.Empresa + "", false);
            frm.consulta = true;
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.codProduto))
            {
                txtCodProduto.Text = frm.codProduto;
                txtDescricaoProduto.Text = frm.nome;
                getProduto();
            }
        }

        private void txtCodProduto_Validated(object sender, EventArgs e)
        {
            getProduto();
        }

        public void getProduto()
        {
            if (!string.IsNullOrEmpty(txtCodProduto.Text))
            {
                DataTable dtProduto = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODPRODUTO, NOME, CODIGOAUXILIAR, CODUNIDVENDA, PRECO3  FROM VPRODUTO WHERE CODPRODUTO LIKE '%" + txtCodProduto.Text + "%' AND CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });
                if (dtProduto.Rows.Count > 1)
                {
                    Visao.frmVisaoProduto frm = new Visao.frmVisaoProduto(@"WHERE CODPRODUTO LIKE '%" + txtCodProduto.Text + "%' AND CODEMPRESA = " + AppLib.Context.Empresa + "", false);
                    frm.consulta = true;
                    frm.ShowDialog();
                    if (!string.IsNullOrEmpty(frm.codProduto))
                    {
                        txtCodProduto.Text = frm.codProduto;
                        txtDescricaoProduto.Text = frm.nome;
                        txtCodUnidVenda.Text = frm.codUnidVenda;
                        txtCodigoAuxiliar.Text = frm.codigoAuxiliar;
                    }
                }
                else if (dtProduto.Rows.Count == 1)
                {
                    txtCodProduto.Text = dtProduto.Rows[0]["CODPRODUTO"].ToString();
                    txtDescricaoProduto.Text = dtProduto.Rows[0]["NOME"].ToString();
                    txtCodigoAuxiliar.Text = dtProduto.Rows[0]["CODIGOAUXILIAR"].ToString();
                    txtCodUnidVenda.Text = dtProduto.Rows[0]["CODUNIDVENDA"].ToString();
                    txtValorVenda.Text = string.Format("{0:n2}", Convert.ToDecimal(dtProduto.Rows[0]["PRECO3"]));
                }

                if (!string.IsNullOrEmpty(txtDescricaoProduto.Text))
                {
                    getSaldo();
                    getVisaoSaldo();
                }
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtCodigoAuxiliar_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigoAuxiliar.Text) && string.IsNullOrEmpty(txtCodProduto.Text))
            {
                 DataTable dtProduto = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODPRODUTO, NOME, CODIGOAUXILIAR, CODUNIDVENDA  FROM VPRODUTO WHERE CODIGOAUXILIAR LIKE '%" + txtCodigoAuxiliar.Text + "%' AND CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });
                if (dtProduto.Rows.Count > 1)
                {
                    Visao.frmVisaoProduto frm = new Visao.frmVisaoProduto(@"WHERE CODIGOAUXILIAR LIKE '%" + txtCodigoAuxiliar.Text + "%' AND CODEMPRESA = " + AppLib.Context.Empresa + "", false);
                    frm.consulta = true;
                    frm.ShowDialog();
                    if (!string.IsNullOrEmpty(frm.codProduto))
                    {
                        txtCodProduto.Text = frm.codProduto;
                        txtDescricaoProduto.Text = frm.nome;
                        txtCodUnidVenda.Text = frm.codUnidVenda;
                        txtCodigoAuxiliar.Text = frm.codigoAuxiliar;
                    }
                }
                else if (dtProduto.Rows.Count == 1)
                {
                    txtCodProduto.Text = dtProduto.Rows[0]["CODPRODUTO"].ToString();
                    txtDescricaoProduto.Text = dtProduto.Rows[0]["NOME"].ToString();
                    txtCodigoAuxiliar.Text = dtProduto.Rows[0]["CODIGOAUXILIAR"].ToString();
                    txtCodUnidVenda.Text = dtProduto.Rows[0]["CODUNIDVENDA"].ToString();
                }

                if (!string.IsNullOrEmpty(txtDescricaoProduto.Text))
                {
                    getSaldo();
                    getVisaoSaldo();
                }
            }
        }
    }
}
