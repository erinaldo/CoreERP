using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Glb.New.Filtro
{
    public partial class frmFiltroAtendimento : Form
    {
        public DateTime dataInicial;
        public DateTime dataFinal;

        public string unidade = "";
        public string atendente = "";

        public string condicao = "";

        public frmFiltroAtendimento()
        {
            InitializeComponent();
        }

        private void frmFiltroAtendimento_Load(object sender, EventArgs e)
        {

        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTodos.Checked)
            {
                lblDataInicial.Visible = false;
                dteInicial.Visible = false;

                lblDataFinal.Visible = false;
                dteFinal.Visible = false;

                lblValor.Visible = false;
                cmbValorFiltro.Visible = false;
            }
        }

        private void rbPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPeriodo.Checked)
            {
                lblDataInicial.Visible = true;
                dteInicial.Visible = true;

                lblDataFinal.Visible = true;
                dteFinal.Visible = true;
            }
            else
            {
                lblDataInicial.Visible = false;
                dteInicial.Visible = false;

                lblDataFinal.Visible = false;
                dteFinal.Visible = false;
            }
        }

        private void rbHoje_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHoje.Checked)
            {
                lblDataInicial.Visible = false;
                dteInicial.Visible = false;

                lblDataFinal.Visible = false;
                dteFinal.Visible = false;

                lblValor.Visible = false;
                cmbValorFiltro.Visible = false;
            }
        }

        private void rbUnidade_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUnidade.Checked)
            {
                lblValor.Visible = true;
                cmbValorFiltro.Visible = true;

                CarregaUnidade();
            }
            else
            {
                lblValor.Visible = false;
                cmbValorFiltro.Visible = false;
            }
        }

        private void rbContato_CheckedChanged(object sender, EventArgs e)
        {
            if (rbContato.Checked)
            {
                lblValor.Visible = true;
                cmbValorFiltro.Visible = true;

                CarregaContato();
            }
            else
            {
                lblValor.Visible = false;
                cmbValorFiltro.Visible = false;
            }
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            if (rbTodos.Checked)
            {
                condicao = "WHERE CODEMPRESA = "+ AppLib.Context.Empresa +"";
            }
            else if (rbPeriodo.Checked)
            {
                condicao = "WHERE DATAATENDIMENTO BETWEEN '"+ dteInicial.DateTime.ToString("yyy-MM-dd") + "' AND '" + dteFinal.DateTime.ToString("yyy-MM-dd") + "'";
            }
            else if (rbHoje.Checked)
            {
                condicao = "WHERE DAY(DATAATENDIMENTO) = DAY(GETDATE())";
            }
            else if (rbUnidade.Checked)
            {
                condicao = "WHERE CODUNIDADE = "+ cmbValorFiltro.SelectedValue +"";
            }
            else
            {
                condicao = "WHERE CODCONTATO = "+ cmbValorFiltro.SelectedValue + "";
            }

            this.Dispose();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Métodos

        private void CarregaContato()
        {
            try
            {
                cmbValorFiltro.DataSource = null;

                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODCONTATO, NOME
                                                                                        FROM VCLIFORCONTATO
                                                                                        WHERE CODEMPRESA = ? ORDER BY NOME ASC", new object[] { AppLib.Context.Empresa });

                cmbValorFiltro.DataSource = dt;

                cmbValorFiltro.ValueMember = "CODCONTATO";
                cmbValorFiltro.DisplayMember = "NOME";
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CarregaUnidade()
        {
            try
            {
                cmbValorFiltro.DataSource = null;

                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT IDUNIDADE, NOME
                                                                                        FROM AUNIDADE 
                                                                                        WHERE CODEMPRESA = ? ORDER BY NOME ASC", new object[] { AppLib.Context.Empresa });

                cmbValorFiltro.DataSource = dt;

                cmbValorFiltro.ValueMember = "IDUNIDADE";
                cmbValorFiltro.DisplayMember = "NOME";
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
