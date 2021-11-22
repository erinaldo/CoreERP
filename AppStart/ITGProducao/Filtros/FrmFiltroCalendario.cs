using ITGProducao.Controles;
using ITGProducao.Visao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITGProducao.Filtros
{
    public partial class FrmFiltroCalendario : Form
    {
        public string condicao = string.Empty;
        public Form pai = null;
        public bool aberto = false;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public FrmFiltroCalendario()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (aberto == false)
            {
                getCondicao();
                if (this.lookup == null)
                {
                    FrmVisaoCalendario frmOper = new FrmVisaoCalendario(condicao, pai);
                //frmOper.MdiParent = this.MdiParent;
                frmOper.Show();
                }
                else
                {
                    FrmVisaoCalendario frmOper = new FrmVisaoCalendario(ref this.lookup);
                    frmOper.WindowState = FormWindowState.Normal;
                    frmOper.StartPosition = FormStartPosition.CenterScreen;
                    frmOper.Show();
                }
            }
            else
            {
                getCondicao();
            }

            this.Dispose();
        }

        private void getCondicao()
        {
            try
            {
                if (rbTodos.Checked == true)
                {
                    condicao = "WHERE PCALENDARIO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND PCALENDARIO.CODFILIAL = '" + AppLib.Context.Filial + "'";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível carregar as condições do filtro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }
    }
}
