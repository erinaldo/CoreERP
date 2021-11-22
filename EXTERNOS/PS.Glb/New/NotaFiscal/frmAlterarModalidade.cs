using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.NotaFiscal
{
    public partial class frmAlterarModalidade : Form
    {
        public frmAlterarModalidade()
        {
            InitializeComponent();
        }

        private void frmAlterarModalidade_Load(object sender, EventArgs e)
        {
            int Modalidade = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT MODALIDADE FROM VPARAMETROS WHERE CODEMPRESA = ? AND IDPARAMETRO = ?", new object[] { AppLib.Context.Empresa, 1 }));

            if (Modalidade == 1)
            {
                cmbModalidade.SelectedIndex = 0;
            }
            else if (Modalidade == 6)
            {
                cmbModalidade.SelectedIndex = 1;
            }
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            string Modalidade = string.Empty;

            if (cmbModalidade.Text == "Normal")
            {
                Modalidade = "1";
            }
            else if (cmbModalidade.Text == "SVC-AN")
            {
                Modalidade = "6";
            }

            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VPARAMETROS SET MODALIDADE = ? WHERE CODEMPRESA = ? AND IDPARAMETRO = ?", new object[] { Modalidade, AppLib.Context.Empresa, 1 });
            this.Dispose();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
