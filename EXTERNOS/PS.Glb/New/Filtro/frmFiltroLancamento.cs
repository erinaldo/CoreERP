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
    public partial class frmFiltroLancamento : Form
    {
        public string condicao = string.Empty;
        public Form pai = null;
        public bool aberto = false;

        public frmFiltroLancamento()
        {
            InitializeComponent();
        }

        private void rbStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStatus.Checked == true)
            {
                dtData.Visible = false;
                cmbStatus.Visible = true;
                lblValor.Visible = true;
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
                dtData.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
                lblValor.Text = "Mês";
            }
            else
            {
                dtData.Visible = false;
                lblValor.Visible = false;
                lblValor.Text = "Valor";
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (aberto == false)
            {
                getCondicao();
                if (!string.IsNullOrEmpty(condicao))
                {
                    if (condicao == "Todos")
                    {
                        if (rbReceber.Checked == true)
                        {
                            condicao = " WHERE TIPOPAGREC = 1";
                        }
                        else if (rbPagar.Checked == true)
                        {
                            condicao = " WHERE TIPOPAGREC = 0";
                        }
                        else
                        {
                            condicao = string.Empty;
                        }
                    }
                    else
                    {
                        if (rbReceber.Checked == true)
                        {
                            condicao = condicao + " AND TIPOPAGREC = 1";
                        }
                        else if (rbPagar.Checked == true)
                        {
                            condicao = condicao + " AND TIPOPAGREC = 0";
                        }
                    }
                    Glb.New.Visao.frmVisaoLancamento frmOper = new Glb.New.Visao.frmVisaoLancamento(condicao, pai);
                    frmOper.Show();
                    this.Dispose();
                }

            }
            else
            {
                getCondicao();
                if (!string.IsNullOrEmpty(condicao))
                {
                    if (condicao == "Todos")
                    {
                        if (rbReceber.Checked == true)
                        {
                            condicao = " WHERE TIPOPAGREC = 1";
                        }
                        else if (rbPagar.Checked == true)
                        {
                            condicao = " WHERE TIPOPAGREC = 0";
                        }
                        else
                        {
                            condicao = string.Empty;
                        }
                    }
                    else
                    {
                        if (rbReceber.Checked == true)
                        {
                            condicao = condicao + " AND TIPOPAGREC = 1";
                        }
                        else if (rbPagar.Checked == true)
                        {
                            condicao = condicao + " AND TIPOPAGREC = 0";
                        }
                    }
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
                    condicao = "Todos";
                }
                else if (rbHoje.Checked == true)
                {
                    condicao = "WHERE CONVERT(VARCHAR(10),FLANCA.DATAVENCIMENTO,120) = '" + string.Format("{0:yyyy-MM-dd}", AppLib.Context.poolConnection.Get("Start").GetDateTime()) + "'";
                }
                else if (rbEmissao.Checked == true)
                {
                    condicao = "WHERE CONVERT(VARCHAR(10),FLANCA.DATAEMISSAO,120) = '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dtData.Text)) + "'";
                }
                else if (rbMes.Checked == true)
                {
                    condicao = "WHERE CONVERT(VARCHAR(10),FLANCA.DATAEMISSAO,120) >= '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dtData.Text)) + "' AND CONVERT(VARCHAR(10),FLANCA.DATAEMISSAO,120) < '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dtData.Text).AddMonths(1)) + "'";
                }
                else if (rbStatus.Checked == true)
                {
                    switch (cmbStatus.SelectedValue.ToString())
                    {
                        case "3":
                            condicao = "WHERE FLANCA.DATAVENCIMENTO < CONVERT(VARCHAR(10),GETDATE(),120) AND FLANCA.CODSTATUS = 0";
                            break;
                        case "4":
                            condicao = "WHERE FLANCA.DATAVENCIMENTO = CONVERT(VARCHAR(10),GETDATE(),120) AND FLANCA.CODSTATUS = 0";
                            break;
                        default:
                            condicao = "WHERE FLANCA.CODSTATUS = '" + cmbStatus.SelectedValue + "'";
                            break;
                    }
                }
                else if (rbPeriodo.Checked == true)
                {
                    frmPeriodoFiltro frm = new frmPeriodoFiltro();
                    frm.tabela = "FLANCA";
                    frm.ShowDialog();
                    if (!string.IsNullOrEmpty(frm.campo))
                    {

                        frm.campo = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT COLUNA FROM GDICIONARIO WHERE TABELA = 'FLANCA' AND DESCRICAO = ?", new object[] { frm.campo }).ToString();
                        condicao = "WHERE CONVERT(VARCHAR(10), " + frm.campo + ",120) >= '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(frm.dataInicial)) + "' AND CONVERT(VARCHAR(10), " + frm.campo + ",120) <= '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(frm.datafinal)) + "'";

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmFiltroOperacao_Load(object sender, EventArgs e)
        {
            cmbStatus.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODSTATUS, DESCRICAO FROM GSTATUS WHERE TABELA = ? AND CODEMPRESA = ?", new object[] { "FLANCA", AppLib.Context.Empresa });
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
    }
}
