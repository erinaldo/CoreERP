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
    public partial class frmFiltroCliente : Form
    {
        public string condicao = string.Empty;
        public Form pai = null;
        public bool aberto = false;
        public string codMenu = string.Empty;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmFiltroCliente()
        {
            InitializeComponent();
        }

        public frmFiltroCliente(ref NewLookup lookup)
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
                    Glb.New.Visao.frmVisaoCliente frmOper = new Glb.New.Visao.frmVisaoCliente(condicao, pai, codMenu);
                    frmOper.Show();
                }
                else
                {
                    Glb.New.Visao.frmVisaoCliente frmOper = new Glb.New.Visao.frmVisaoCliente(ref this.lookup);
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
                    condicao = "WHERE VCLIFOR.CODEMPRESA = '" + AppLib.Context.Empresa + "' ";
                }

                else if (rbSituacao.Checked == true)
                {
                    condicao = "WHERE VCLIFOR.ATIVO = '" + cmbStatus.SelectedValue + "'";
                }

                else if (rbTipo.Checked == true)
                {
                    condicao = "WHERE VCLIFOR.FISICOJURIDICO = '" + cmbStatus.SelectedValue + "'";
                }

                else if (rbClassificacao.Checked == true)
                {
                    condicao = "WHERE VCLIFOR.CODCLASSIFICACAO = '" + cmbStatus.SelectedValue + "'";
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



        private void rbTipo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTipo.Checked == true)
            {
                cmbStatus.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT FISICOJURIDICO CODIGO, 
	CASE FISICOJURIDICO WHEN 1 THEN 'PESSOA FISICA' ELSE 'PESSOA JURIDICA' END DESCRICAO FROM VCLIFOR GROUP BY FISICOJURIDICO", new object[] { });
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
	CASE ATIVO WHEN 1 THEN 'ATIVO' ELSE 'INATIVO' END DESCRICAO FROM VCLIFOR GROUP BY ATIVO ORDER BY ATIVO DESC", new object[] { });
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

        private void rbClassificacao_CheckedChanged(object sender, EventArgs e)
        {
            if (rbClassificacao.Checked == true)
            {
                cmbStatus.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODCLASSIFICACAO CODIGO, 
	CASE CODCLASSIFICACAO WHEN 0 THEN 'CLIENTE' WHEN 1 THEN 'FORNECEDOR' ELSE 'AMBOS' END DESCRICAO FROM VCLIFOR GROUP BY CODCLASSIFICACAO ORDER BY CODCLASSIFICACAO ", new object[] { });
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
    }
}
