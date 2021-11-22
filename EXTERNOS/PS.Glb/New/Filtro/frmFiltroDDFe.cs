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
    public partial class frmFiltroDDFe : Form
    {
        public string condicao = string.Empty;
        public Form pai = null;
        public bool aberto = false;
        public string codMenu = string.Empty;

        List<string> listEstrutura = new List<string>();

        public frmFiltroDDFe()
        {
            InitializeComponent();
        }

        private void getCondicao()
        {
            try
            {
                if (rbTodos.Checked == true)
                {
                    condicao = "WHERE GDDFE.CODEMPRESA = '" + AppLib.Context.Empresa + "' ";
                }

                else if (rbChave.Checked == true)
                {
                    condicao = "WHERE GDDFE.CHAVE = '" + cmbStatus.Text + "'";
                }
                else if (rbNumeroDocumento.Checked == true)
                {
                    condicao = "WHERE GDDFE.NUMERODOCUMENTO = '" + cmbStatus.Text + "'";
                }
                else if (rbCNPJEmitente.Checked == true)
                {
                    condicao = "WHERE GDDFE.CNPJEMITENTE = '" + cmbStatus.Text + "'";
                }
                else if (rbDataEmissao.Checked == true)
                {
                    condicao = "WHERE GDDFE.DATAEMISSAO = '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dteInicial.Text)) + "'";
                }
                else if (rbEstrutura.Checked == true)
                {
                    condicao = "WHERE GDDFE.CODESTRUTURA = '" + cmbStatus.Text + "'";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível carregar as condições do filtro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbChave_CheckedChanged(object sender, EventArgs e)
        {
            if (rbChave.Checked)
            {
                cmbStatus.Visible = true;
                lblValor.Visible = true;
                cmbStatus.DropDownStyle = ComboBoxStyle.Simple;
            }
            else
            {
                cmbStatus.Visible = false;
                lblValor.Visible = false;
                cmbStatus.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

        private void rbNumeroDocumento_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNumeroDocumento.Checked)
            {
                cmbStatus.Visible = true;
                lblValor.Visible = true;
                cmbStatus.DropDownStyle = ComboBoxStyle.Simple;
            }
            else
            {
                cmbStatus.Visible = false;
                lblValor.Visible = false;
                cmbStatus.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

        private void rbCNPJEmitente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCNPJEmitente.Checked)
            {
                cmbStatus.Visible = true;
                lblValor.Visible = true;
                cmbStatus.DropDownStyle = ComboBoxStyle.Simple;
            }
            else
            {
                cmbStatus.Visible = false;
                lblValor.Visible = false;
                cmbStatus.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

        private void rbEstrutura_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEstrutura.Checked)
            {
                listEstrutura.Add("NF-e");
                listEstrutura.Add("CT-e");

                cmbStatus.DataSource = listEstrutura;
                cmbStatus.Visible = true;
                lblValor.Visible = true;
            }
            else
            {
                cmbStatus.Visible = false;
                lblValor.Visible = false;
            }
        }

        private void rbDataEmissao_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDataEmissao.Checked)
            {
                labelControl2.Visible = true;
                dteInicial.Visible = true;

                labelControl2.Location = lblValor.Location;
                dteInicial.Location = cmbStatus.Location;
            }
            else
            {
                labelControl2.Visible = false;
                dteInicial.Visible = false;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (aberto == false)
            {
                if (rbDataEmissao.Checked)
                {
                    if (dteInicial.Text == string.Empty)
                    {
                        MessageBox.Show("Favor informar a data.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                getCondicao();
                PS.Glb.New.Visao.frmVisaoDDFe Nota = new Visao.frmVisaoDDFe(condicao, pai, codMenu);
                Nota.Show();
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

        private void frmFiltroDDFe_Load(object sender, EventArgs e)
        {

        }
    }
}
