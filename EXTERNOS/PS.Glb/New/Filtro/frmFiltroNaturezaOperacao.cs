using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITGProducao.Controles;

namespace PS.Glb.New.Filtro
{
    public partial class frmFiltroNaturezaOperacao : Form
    {
        public string condicao = string.Empty;
        public Form pai = null;
        public bool aberto = false;
        public string codMenu = string.Empty;
        private bool permiteEditar = true;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmFiltroNaturezaOperacao()
        {
            InitializeComponent();
        }
        public frmFiltroNaturezaOperacao(ref NewLookup lookup)
        {
            InitializeComponent();
            this.lookup = lookup;
        }
        private void getCondicao()
        {
            try
            {
                if (rbTodos.Checked == true)
                {
                    condicao = "WHERE VNATUREZA.CODEMPRESA = '" + AppLib.Context.Empresa + "' ";
                }

                else if (rbSituacao.Checked == true)
                {
                    condicao = "WHERE VNATUREZA.ATIVO = '" + cmbStatus.SelectedValue + "'";
                }

                else if (rbTipo.Checked == true)
                {
                    condicao = "WHERE VNATUREZA.TIPENTSAI = '" + cmbStatus.SelectedValue + "'";
                }

                else if (rbClassificacao.Checked == true)
                {
                    condicao = "WHERE VNATUREZA.DENTRODOESTADO = '" + cmbStatus.SelectedValue + "'";
                }
                else if (rbUltimoNivel.Checked == true)
                {
                    condicao = "WHERE VNATUREZA.ULTIMONIVEL = '" + cmbStatus.SelectedValue + "'";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível carregar as condições do filtro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbTipo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTipo.Checked == true)
            {
                cmbStatus.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT TIPENTSAI CODIGO, CASE TIPENTSAI WHEN 'E' THEN 'ENTRADA' ELSE 'SAÍDA' END DESCRICAO FROM VNATUREZA GROUP BY TIPENTSAI", new object[] { });
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

        private void rbSituacao_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSituacao.Checked == true)
            {
                cmbStatus.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT ATIVO CODIGO, CASE ATIVO WHEN 1 THEN  'ATIVO' ELSE 'INATIVO' END DESCRICAO FROM VNATUREZA GROUP BY ATIVO ORDER BY ATIVO DESC", new object[] { });
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
                cmbStatus.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT DENTRODOESTADO CODIGO, CASE DENTRODOESTADO WHEN 1 THEN 'DENTRO DO ESTADO' ELSE 'FORA DO ESTADO' END DESCRICAO FROM VNATUREZA GROUP BY DENTRODOESTADO ORDER BY DENTRODOESTADO DESC", new object[] { });
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

        private void rbUltimoNivel_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUltimoNivel.Checked == true)
            {
                cmbStatus.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT ULTIMONIVEL CODIGO, CASE ULTIMONIVEL WHEN 1 THEN 'SIM' ELSE 'NÃO' END DESCRICAO FROM VNATUREZA GROUP BY ULTIMONIVEL ORDER BY ULTIMONIVEL DESC", new object[] { });
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (aberto == false)
            {
                getCondicao();
                if (this.lookup == null)
                {
                    Glb.New.Visao.frmVisaoNatureza natureza = new Glb.New.Visao.frmVisaoNatureza(condicao, pai, codMenu);
                    natureza.Show();
                }
                else
                {
                    Glb.New.Visao.frmVisaoNatureza natureza = new Glb.New.Visao.frmVisaoNatureza(ref this.lookup);
                    natureza.WindowState = FormWindowState.Normal;
                    natureza.StartPosition = FormStartPosition.CenterScreen;
                    natureza.Show();
                }

            }
            else
            {
                getCondicao();
            }

            this.Dispose();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}

