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
    public partial class frmFiltroOperacao : Form
    {
        public string condicao = string.Empty;
        public Form pai = null;
        public string tipOPer = string.Empty;
        public bool aberto = false;
        public string codMenu;
        public int codFilial;


        public frmFiltroOperacao()
        {
            InitializeComponent();
        }

        private void rbStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStatus.Checked == true)
            {
                cmbStatus.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODSTATUS, DESCRICAO FROM GSTATUS WHERE TABELA = ? AND CODEMPRESA = ?", new object[] { "GOPER", AppLib.Context.Empresa });
                cmbStatus.ValueMember = "CODSTATUS";
                cmbStatus.DisplayMember = "DESCRICAO";
                dtData.Visible = false;
                cmbStatus.Visible = true;
                lblValor.Visible = true;
                lblValor.Text = "Valor";
            }
            else
            {
                cmbStatus.Visible = false;
                lblValor.Visible = false;
            }
        }

        private void rbMes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMes.Checked == true)
            {
                cmbStatus.Visible = false; 
                dtData.Visible = true;
                lblValor.Visible = true;
                dtData.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
                dtData.Properties.EditMask = "y";
                lblValor.Text = "Mês";
            }
            else
            {
                dtData.Visible = false;
                lblValor.Visible = false;
            }
      }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (aberto == false)
            {
                getCondicao();
                if (!string.IsNullOrEmpty(condicao))
                {                 
                    Glb.New.Visao.frmVisaoOperacao frmOper = new Glb.New.Visao.frmVisaoOperacao(condicao, pai, tipOPer, codMenu);
                    frmOper.codFilial = codFilial;
                    frmOper.Show();
                    this.Dispose();
                }
            }
            else
            {
                getCondicao();
                if (!string.IsNullOrEmpty(condicao))
                {
                    this.Dispose();
                }
            }
        }

        private void getCondicao()
        {
            try
            {
                if (rbTodos.Checked == true)
                {
                    condicao = "WHERE GOPER.CODTIPOPER = '" + tipOPer + "' AND GOPER.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND GOPER.CODFILIAL = '" + codFilial +"'";
                }
                else if (rbHoje.Checked == true)
                {
                    condicao = "WHERE GOPER.CODTIPOPER = '" + tipOPer + "' AND CONVERT(VARCHAR(10),GOPER.DATAEMISSAO,120) = '" + string.Format("{0:yyyy-MM-dd}", AppLib.Context.poolConnection.Get("Start").GetDateTime()) + "' AND GOPER.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND GOPER.CODFILIAL = '" + codFilial + "'";
                }
                else if (rbEmissao.Checked == true)
                {
                    condicao = "WHERE GOPER.CODTIPOPER = '" + tipOPer + "' AND CONVERT(VARCHAR(10),GOPER.DATAEMISSAO,120) = '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dtData.Text)) + "' AND GOPER.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND GOPER.CODFILIAL = '" + codFilial  + "'";
                }
                else if (rbMes.Checked == true)
                {
                    //condicao = "WHERE GOPER.CODTIPOPER = '" + tipOPer + "' AND CONVERT(VARCHAR(10),GOPER.DATAEMISSAO,120) >= '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dtData.Text)) + "' AND CONVERT(VARCHAR(10),GOPER.DATAEMISSAO,120) < '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dtData.Text).AddMonths(1)) + "' AND GOPER.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND GOPER.CODFILIAL = '" + codFilial + "'";
                    //condicao = "WHERE GOPER.CODTIPOPER = '" + tipOPer + "' AND DATEPART(MM, GOPER.DATAEMISSAO) = '" + string.Format("{0:MM}", Convert.ToDateTime(dtData.Text)) + "' AND GOPER.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND GOPER.CODFILIAL = '" + codFilial + "'";
                    condicao = "WHERE GOPER.CODTIPOPER = '" + tipOPer + "' AND MONTH(GOPER.DATAEMISSAO) = '" +  Convert.ToDateTime(dtData.Text).Month + "' AND YEAR(GOPER.DATAEMISSAO) = '" + Convert.ToDateTime(dtData.Text).Year + "' AND GOPER.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND GOPER.CODFILIAL = '" + codFilial + "'";
                }
                else if (rbStatus.Checked == true)
                {
                    condicao = "WHERE GOPER.CODTIPOPER = '" + tipOPer + "' AND GOPER.CODSTATUS = '" + cmbStatus.SelectedValue + "' AND GOPER.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND GOPER.CODFILIAL = '" + codFilial + "'";
                }
                else if (rbSituacao.Checked == true)
                {
                    condicao = "WHERE GOPER.CODTIPOPER = '" + tipOPer + "' AND GOPER.CODSITUACAO = '" + cmbStatus.SelectedValue + "' AND GOPER.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND GOPER.CODFILIAL = '" + codFilial + "'";
                }
                else if (rbAprovacao.Checked == true)
                {
                    condicao = "WHERE GOPER.CODTIPOPER = '"+ tipOPer+ "' AND GOPER.APROVACAO = '" + cmbStatus.SelectedValue + "' AND GOPER.CODEMPRESA  = '" + AppLib.Context.Empresa + "' AND GOPER.CODFILIAL = '" + codFilial + "'" ;
                }
                else if (rbCliente.Checked == true)
                {
                    New.Visao.frmVisaoCliente frm = new Visao.frmVisaoCliente(string.Empty);
                    frm.consulta = true;
                    frm.ShowDialog();
                    if (!string.IsNullOrEmpty(frm.codCliente))
                    {
                        condicao = condicao = "WHERE GOPER.CODTIPOPER = '" + tipOPer + "' AND GOPER.CODCLIFOR = '" + frm.codCliente + "' AND GOPER.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND GOPER. CODFILIAL = '" + codFilial + "'";
                    }

                }
                else if(rbNumeroOperacao.Checked == true)
                {
                    if (!string.IsNullOrEmpty(txtValor.Text))
                    {
                        condicao = "WHERE GOPER.CODTIPOPER = '" + tipOPer + "' AND GOPER.NUMERO = " + txtValor.Text + " AND GOPER.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND GOPER.CODFILIAL = '" + codFilial + "'";    
                    }
                    else
                    {
                        MessageBox.Show("Favor preencher o número da operação.", "Informação do Sistema.", MessageBoxButtons.OK ,MessageBoxIcon.Warning);
                        return;
                    }
                    
                }
                else if (rbPeriodo.Checked == true)
                {
                    frmPeriodoFiltro frm = new frmPeriodoFiltro();
                    frm.tabela = "GOPER";
                    frm.ShowDialog();
                    if (!string.IsNullOrEmpty(frm.campo))
                    {
                        frm.campo = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT COLUNA FROM GDICIONARIO WHERE TABELA = 'GOPER' AND DESCRICAO = ?", new object[] { frm.campo }).ToString();

                        condicao = "WHERE GOPER.CODTIPOPER = '" + tipOPer + "' AND " + frm.campo + " >= '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(frm.dataInicial)) + "' AND " + frm.campo + " <= '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(frm.datafinal)) + "'";

                        if (frm.parametro.Count > 0)
                        {
                            Relatorios.parametro param = new Relatorios.parametro();
                            for (int i = 0; i < frm.parametro.Count; i++)
                            {
                                frm.parametro[i].VALOR = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(frm.parametro[i].VALOR));
                            }
                            condicao = condicao + " " + param.tratamentoQuery(string.Empty, frm.parametro);
                        }
                    }
                    else
                    {
                        condicao = string.Empty;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível carregar as condições do filtro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmFiltroOperacao_Load(object sender, EventArgs e)
        {
            cmbStatus.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODSTATUS, DESCRICAO FROM GSTATUS WHERE TABELA = ? AND CODEMPRESA = ?", new object[] { "GOPER", AppLib.Context.Empresa });
            cmbStatus.ValueMember = "CODSTATUS";
            cmbStatus.DisplayMember = "DESCRICAO";
        }

        private void rbEmissao_CheckedChanged(object sender, EventArgs e)
         {
            if (rbEmissao.Checked == true)
            {
                cmbStatus.Visible = false;
                dtData.Visible = true;
                lblValor.Visible = true;
                dtData.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.Default;
                dtData.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Default;
                lblValor.Text = "Valor";
            }
            else
            {
                dtData.Visible = false;
                lblValor.Visible = false;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void rbNumeroOperacao_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNumeroOperacao.Checked == true)
            {
                cmbStatus.Visible = false;
                dtData.Visible = false;
                txtValor.Visible = true;
                lblValor.Visible = true;
                lblValor.Text = "Número da Operação";
            }
            else
            {
                txtValor.Visible = false;
                lblValor.Visible = false;
            }
        }

        private void rbSituacao_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSituacao.Checked == true)
            {
                cmbStatus.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODSITUACAO, DESCRICAO FROM GSITUACAO WHERE CODSITUACAO = 7 UNION SELECT CODSITUACAO, DESCRICAO FROM GSITUACAO WHERE CODSITUACAO = 8", new object[] { });
                cmbStatus.ValueMember = "CODSITUACAO";
                cmbStatus.DisplayMember = "DESCRICAO";
                cmbStatus.Visible = true;
                lblValor.Visible = true;
                lblValor.Text = "Valor";
            }
            else
            {
                cmbStatus.Visible = false;
                lblValor.Visible = false;
            }
        }

        private void rbAprovacao_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAprovacao.Checked == true)
            {
                cmbStatus.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT APROVACAO CODIGO, CASE APROVACAO WHEN 1 THEN 'Aprovado' ELSE 'Não aprovado' END DESCRICAO FROM GOPER GROUP BY APROVACAO", new object[] { });
                cmbStatus.ValueMember = "CODIGO";
                cmbStatus.DisplayMember = "DESCRICAO";
                cmbStatus.Visible = true;
                lblValor.Visible = true;
                lblValor.Text = "Valor";
            }
            else
            {
                cmbStatus.Visible = false;
                lblValor.Visible = false;
            }
        }
    }
}
