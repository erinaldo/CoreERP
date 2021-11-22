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
    public partial class frmProcessosVisaoTabelaPreco : Form
    {
        public int Processos_Selecionado;
        public decimal Valor;
        public string Codproduto;
        public string idtabela;
        public string codFilial;
        public bool Cancelar;
        public string mascara;

        public frmProcessosVisaoTabelaPreco()
        {
            InitializeComponent();
        }

        private void frmProcessosTabelaPreco_Load(object sender, EventArgs e)
        {
            tbValorProcesso.Focus();

            if (Processos_Selecionado == 3 || Processos_Selecionado == 5)
            {
                lpPorcentagem.Visible = true;
                labelControl1.Text = "Percentual:";
            }
            VerificaLookup();
            mascara = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT  VPARAMETROS.DECIMALTABPRECO FROM VPARAMETROS WHERE VPARAMETROS.CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();

        }


        private void VerificaLookup()
        {
            if (!string.IsNullOrEmpty(this.idtabela))
            {
                lpProduto.Grid_WhereVisao[3].ValorFixo = @"SELECT DISTINCT(CODPRODUTO) FROM VCLIFORTABPRECOITEM WHERE IDTABELA IN (" + this.idtabela + ") AND CODEMPRESA = " + AppLib.Context.Empresa.ToString() + (string.IsNullOrEmpty(lpFilial.txtcodigo.Text) ? "" : " AND CODFILIAL = " + lpFilial.txtcodigo.Text);
                lpProduto.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Clear();
            }
        }

        private void chkProdutoEspecifico_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProdutoEspecifico.Checked == true)
            {
                lpProduto.Enabled = true;
                lpFilial.Enabled = true;
            }
            else
            {
                lpProduto.Enabled = false;
                lpFilial.Enabled = false;
            }
        }

        private void tbValorProcesso_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbValorProcesso.Text))
            {
                return;
            }
            Valor = Convert.ToDecimal(tbValorProcesso.Text);
            tbValorProcesso.Text = string.Format("{0:n" + mascara + "}", Valor);
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbValorProcesso.Text))
            {
                MessageBox.Show("Informe o valor.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Valor = Convert.ToDecimal(tbValorProcesso.Text);

            PS.Glb.New.Visao.frmVisaoTabelaPreco frm = new Visao.frmVisaoTabelaPreco(string.Empty);

            Codproduto = lpProduto.txtcodigo.Text;
            codFilial = lpFilial.txtcodigo.Text;

            frm.Valor_Processo = Valor;
            frm.CodProduto = Codproduto;
            frm.CodFilial = codFilial;
            this.Dispose();
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            Cancelar = true;
            this.Dispose();
        }

        private void lpFilial_Leave(object sender, EventArgs e)
        {
            VerificaLookup();
        }
    }
}
