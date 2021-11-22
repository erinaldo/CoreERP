using ITGProducao.Controles;
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
    public partial class frmFiltroProjeto : Form
    {
        public string condicao = string.Empty;
        public Form pai = null;
        public bool aberto = false;
        public string codMenu = string.Empty;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmFiltroProjeto()
        {
            InitializeComponent();
        }

        public frmFiltroProjeto(ref NewLookup lookup)
        {
            InitializeComponent();

            this.lookup = lookup;
        }
        

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //if (aberto == false)
            //{
            //    getCondicao();
            //    if (this.lookup == null)
            //    {
            //        Glb.New.Visao.frmVisaoCliente frmOper = new Glb.New.Visao.frmVisaoCliente(condicao, pai, codMenu);
            //        frmOper.Show();
            //    }
            //    else
            //    {
            //        Glb.New.Visao.frmVisaoCliente frmOper = new Glb.New.Visao.frmVisaoCliente(ref this.lookup);
            //        frmOper.WindowState = FormWindowState.Normal;
            //        frmOper.StartPosition = FormStartPosition.CenterScreen;
            //        frmOper.Show();
            //    }

            //}
            //else
            //{
            //    getCondicao();
            //}


            getCondicao();
            this.Dispose();
        }

        private void getCondicao()
        {
            try
            {
                if (rbTodos.Checked == true)
                {
                    condicao = "WHERE APROJETO.CODEMPRESA = '" + AppLib.Context.Empresa + "' ";
                }

                else if (rbStatus.Checked == true)
                {
                    condicao = "WHERE APROJETO.STATUS = '" + cbStatus.Text + "'";
                }

                else if (rbUnidade.Checked == true)
                {
                    condicao = "WHERE AUNIDADE.NOME like '%" + txtValor.Text + "%'";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível carregar as condições do filtro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmFiltroOperacao_Load(object sender, EventArgs e)
        {


        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            condicao = null;
            GC.Collect();
        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTodos.Checked == true)
            {
                lblValor.Visible = false;
                txtValor.Visible = false;
                cbStatus.Visible = false;
            }
            else
            {
                lblValor.Visible = true;
            }
        }

        private void rbStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStatus.Checked == true)
            {   
                txtValor.Visible = false;
                cbStatus.Visible = true;
            }
        }

        private void rbUnidade_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUnidade.Checked == true)
            {
                txtValor.Visible = true;
                cbStatus.Visible = false;
            }
        }
    }
}
