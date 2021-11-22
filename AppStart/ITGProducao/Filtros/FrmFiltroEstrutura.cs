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
    public partial class FrmFiltroEstrutura : Form
    {
        public string condicao = string.Empty;
        public Form pai = null;
        public bool aberto = false;

        //Variaveis para NewLookup
        private NewLookup lookup;
        public FrmFiltroEstrutura()
        {
            InitializeComponent();
        }

        public FrmFiltroEstrutura(ref NewLookup lookup)
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
                    FrmVisaoEstrutura frmOper = new FrmVisaoEstrutura(condicao, pai);
                    frmOper.Show();
                }
                else
                {
                    FrmVisaoEstrutura frmOper = new FrmVisaoEstrutura(ref this.lookup);
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
                    condicao = "WHERE PROTEIROESTRUTURA.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND PROTEIROESTRUTURA.CODFILIAL = '" + AppLib.Context.Filial + "'"; // AND PROTEIROESTRUTURA.ATIVO = 1
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível carregar as condições do filtro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }
    }
}
