using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Processos.Financeiro
{
    public partial class frmCompensarExtrato : Form
    {
        public int IdExtrato;
        public List<int> ListExtrato = new List<int>();

        public frmCompensarExtrato()
        {
            InitializeComponent();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dteDataCompensacao.DateTime.ToString()))
            {
                try
                {
                    for (int i = 0; i < ListExtrato.Count; i++)
                    {
                        if (AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FEXTRATO SET COMPENSADO = 1, DATACOMPENSACAO = ? WHERE CODEMPRESA = ? AND IDEXTRATO = ?", new object[] { Convert.ToDateTime(dteDataCompensacao.DateTime.ToShortDateString()), AppLib.Context.Empresa, ListExtrato[i] }) > 0)
                        {
                            int Codcheque = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT CODCHEQUE FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?", new object[] { AppLib.Context.Empresa, ListExtrato[i] }));

                            if (Codcheque > 0)
                            {
                                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FCHEQUE SET COMPENSADO = 1, DATACOMPENSACAO = ? WHERE CODEMPRESA = ? AND CODCHEQUE = ?", new object[] { Convert.ToDateTime(dteDataCompensacao.DateTime.ToShortDateString()), AppLib.Context.Empresa, Codcheque });
                            }
                        }
                    }

                    MessageBox.Show("Compensação de extrato realizada com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível realizar a compensação de extrato.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
