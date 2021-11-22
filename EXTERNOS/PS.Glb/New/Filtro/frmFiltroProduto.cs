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
    public partial class frmFiltroProduto : Form
    {
        public string condicao = string.Empty;
        public Form pai = null;
        public string tipOPer = string.Empty;
        public bool aberto = false;
        public string codMenu = string.Empty;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmFiltroProduto()
        {
            InitializeComponent();
        }

        public frmFiltroProduto(ref NewLookup lookup)
        {
            InitializeComponent();

            this.lookup = lookup;
        }


        private void rbStatus_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbMes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (aberto == false)
            {
                getCondicao();
                if (this.lookup == null)
                {
                    Glb.New.Visao.frmVisaoProduto frmOper = new Glb.New.Visao.frmVisaoProduto(condicao, pai, codMenu);
                    frmOper.Show();
                }
                else
                {
                    Glb.New.Visao.frmVisaoProduto frmOper = new Glb.New.Visao.frmVisaoProduto(ref this.lookup);
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
                    condicao = string.Empty;
                }

                else if (rbSituacao.Checked == true)
                {
                    condicao = "WHERE VPRODUTO.ATIVO = '" + cmbStatus.SelectedValue + "'";
                }

                else if (rbTipo.Checked == true)
                {
                    condicao = "WHERE VPRODUTO.PRODSERV = '" + cmbStatus.SelectedValue + "'";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível carregar as condições do filtro.", "Informação do Sistema.", MessageBoxButtons.OK ,MessageBoxIcon.Error);
            }
        }

        private void frmFiltroOperacao_Load(object sender, EventArgs e)
        {


        }

        private void rbTipo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTipo.Checked == true)
            {
                cmbStatus.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT PRODSERV CODIGO, 
	CASE PRODSERV WHEN 1 THEN 'PRODUTO' ELSE 'SERVIÇO' END DESCRICAO FROM VPRODUTO GROUP BY PRODSERV ORDER BY PRODSERV DESC", new object[] { });
                cmbStatus.ValueMember = "CODIGO";
                cmbStatus.DisplayMember = "DESCRICAO";
                cmbStatus.Visible = true;
                lblValor.Visible = true;
            }
//            else if (rbSituacao.Checked == true)
//            {
//                cmbStatus.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT ATIVO CODIGO, 
//	CASE ATIVO WHEN 1 THEN 'ATIVO' ELSE 'INATIVO' END DESCRICAO FROM VPRODUTO GROUP BY ATIVO", new object[] { });
//                cmbStatus.ValueMember = "CODIGO";
//                cmbStatus.DisplayMember = "DESCRICAO";
//                cmbStatus.Visible = true;
//                lblValor.Visible = true;
//            }
            else
            {
                cmbStatus.Visible = false;
                lblValor.Visible = false;
            }
       
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void rbSituacao_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSituacao.Checked == true)
            {
                cmbStatus.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT ATIVO CODIGO, 
	CASE ATIVO WHEN 1 THEN 'ATIVO' ELSE 'INATIVO' END DESCRICAO FROM VPRODUTO GROUP BY ATIVO ORDER BY ATIVO DESC", new object[] { });
                cmbStatus.ValueMember = "CODIGO";
                cmbStatus.DisplayMember = "DESCRICAO";
                cmbStatus.Visible = true;
                lblValor.Visible = true;
            }
            else
            {
                cmbStatus.Visible = false;
                lblValor.Visible = false;
            }
        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
