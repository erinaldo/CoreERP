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
    public partial class FrmFiltroUnidade : Form
    {
        public string condicao = string.Empty;
        public Form pai = null;
        public bool aberto = false;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public FrmFiltroUnidade()
        {
            InitializeComponent();
        }

        public FrmFiltroUnidade(ref NewLookup lookup)
        {
            InitializeComponent();

            this.lookup = lookup;
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (aberto == false)
            {
                getCondicao();
                if (this.lookup == null)
                {
                    FrmVisaoUnidade frmOper = new FrmVisaoUnidade(condicao, pai);
                    frmOper.Show();
                }
                else
                {
                    FrmVisaoUnidade frmOper = new FrmVisaoUnidade(ref this.lookup);
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
                    condicao = "WHERE VUNID.CODEMPRESA = '" + AppLib.Context.Empresa;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível carregar as condições do filtro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }
    }
}
