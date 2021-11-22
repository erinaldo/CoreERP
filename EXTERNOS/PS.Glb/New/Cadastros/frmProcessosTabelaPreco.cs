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
    public partial class frmProcessosTabelaPreco : Form
    {
        public int Processos_Selecionado;
        public decimal Valor;
        public string mascara;
        public bool Cancelar;
        public string ValorDecimal;

        public frmProcessosTabelaPreco()
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

            mascara = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT  VPARAMETROS.DECIMALTABPRECO FROM VPARAMETROS WHERE VPARAMETROS.CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();
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

            frmCadastroTabelaPreco frm = new frmCadastroTabelaPreco();

            Valor = Convert.ToDecimal(tbValorProcesso.Text);
            frm.Valor_Processo = Valor;
            this.Dispose();
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            Cancelar = true;
            this.Dispose();
        }
    }
}
