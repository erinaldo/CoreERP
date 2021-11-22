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
    public partial class frmFiltroEstoqueMinimo : Form
    {
        // Variaveis
        public Form pai = null;
        public string Codfilial = string.Empty;
        public string Codquery = string.Empty;
        public string Formula = string.Empty;

        public string Mes1 = string.Empty;
        public string Mes2 = string.Empty;
        public string Mes3 = string.Empty;
        public string Mes4 = string.Empty;

        public string Ano1 = string.Empty;
        public string Ano2 = string.Empty;
        public string Ano3 = string.Empty;
        public string Ano4 = string.Empty;

        public frmFiltroEstoqueMinimo()
        {
            InitializeComponent();
        }

        private void dtPeriodo1_EditValueChanged(object sender, EventArgs e)
        {
            var Data1 = dtPeriodo1.DateTime;
            var MesAnterior = Data1.AddMonths(-1);

            Mes1 = Data1.Month.ToString();
            Ano1 = Data1.Year.ToString();

            dtPeriodo2.DateTime = MesAnterior;

            var Data2 = dtPeriodo2.DateTime;
            var MesAnterior2 = Data2.AddMonths(-1);

            Mes2 = Data2.Month.ToString();
            Ano2 = Data2.Year.ToString();

            dtPeriodo3.DateTime = MesAnterior2;

            var Data3 = dtPeriodo3.DateTime;
            var MesAnterior3 = Data3.AddMonths(-1);

            Mes3 = Data3.Month.ToString();
            Ano3 = Data3.Year.ToString();

            dtPeriodo4.DateTime = MesAnterior3;

            var Data4 = dtPeriodo4.DateTime;

            Mes4 = Data4.Month.ToString();
            Ano4 = Data4.Year.ToString();
        }

        private void dtPeriodo2_EditValueChanged(object sender, EventArgs e)
        {
            var Data2 = dtPeriodo2.DateTime;
            Mes2 = Data2.Month.ToString();
            Ano2 = Data2.Year.ToString();
        }

        private void dtPeriodo3_EditValueChanged(object sender, EventArgs e)
        {
            var Data3 = dtPeriodo3.DateTime;
            Mes3 = Data3.Month.ToString();
            Ano3 = Data3.Year.ToString();
        }

        private void dtPeriodo4_EditValueChanged(object sender, EventArgs e)
        {
            var Data4 = dtPeriodo4.DateTime;
            Mes4 = Data4.Month.ToString();
            Ano4 = Data4.Year.ToString();
        }

        private bool Valida()
        {
            if (string.IsNullOrEmpty(lpFilial.txtcodigo.Text))
            {
                MessageBox.Show("Informe a filial.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(dtPeriodo1.Text))
            {
                MessageBox.Show("Informe o período.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (string.IsNullOrEmpty(dtPeriodo2.Text))
            {
                MessageBox.Show("Informe o período.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (string.IsNullOrEmpty(dtPeriodo3.Text))
            {
                MessageBox.Show("Informe o período.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (string.IsNullOrEmpty(dtPeriodo4.Text))
            {
                MessageBox.Show("Informe o período.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Valida() ==false)
            {
                return;
            }
            else
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("Consultando");

                Codfilial = lpFilial.txtcodigo.Text;

                string Query = getQuery();

                AppLib.Context.poolConnection.Get("Start").ExecTransaction(Query);

                PS.Glb.New.Visao.frmVisaoAtualizacaoEstoqueMinimo frm = new Visao.frmVisaoAtualizacaoEstoqueMinimo(Codfilial, pai);
                frm.PrimeiroMes = Mes1;
                frm.PrimeiroAno = Ano1;
                frm.SegundoMes = Mes2;
                frm.SegundoAno = Ano2;
                frm.TerceiroMes = Mes3;
                frm.TerceiroAno = Ano3;
                frm.QuartoMes = Mes4;
                frm.QuartoAno = Ano4;
                frm.Show();

                splashScreenManager1.CloseWaitForm();

                this.Dispose();
            }           
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Método para buscar o código da Query e depois retorná-la.
        /// </summary>
        /// <returns>Retorna a Query desejada</returns>
        private string getQuery()
        {
            Codquery = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODQUERYESTOQUEMINIMO FROM VPARAMETROS WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();

            Formula = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT QUERY FROM GQUERY WHERE CODQUERY = ?", new object[] { Codquery }).ToString();

            Formula = Formula.Replace("@CODEMPRESA", "'" + AppLib.Context.Empresa + "'");
            Formula = Formula.Replace("@CODFILIAL", "'"+ Codfilial + "'");

            Formula = Formula.Replace("@MES1", "'" + Mes1 + "'");
            Formula = Formula.Replace("@MES2", "'" + Mes2 + "'");
            Formula = Formula.Replace("@MES3", "'" + Mes3 + "'");
            Formula = Formula.Replace("@MES4", "'" + Mes4 + "'");

            Formula = Formula.Replace("@ANO1", "'" + Ano1 + "'");
            Formula = Formula.Replace("@ANO2", "'" + Ano2 + "'");
            Formula = Formula.Replace("@ANO3", "'" + Ano3 + "'");
            Formula = Formula.Replace("@ANO4", "'" + Ano4 + "'");

            return Formula;
        }
    }
}
