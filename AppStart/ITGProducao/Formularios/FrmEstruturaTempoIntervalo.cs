using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITGProducao.Formularios
{
    public partial class FrmEstruturaTempoIntervalo : Form
    {

        private DataTable dtTempoIntervalo;

        public FrmEstruturaTempoIntervalo(ref DataTable dtTempoIntervalo)
        {
            InitializeComponent();

            this.dtTempoIntervalo = dtTempoIntervalo;
            btnNovo.PerformClick();

            txtqtdefinal.Focus();

        }
        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (dtTempoIntervalo.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(dtTempoIntervalo.Rows[dtTempoIntervalo.Rows.Count - 1]["PROTEIROPORINTERVALO.FAIXAFINAL"].ToString()))
                {
                    txtqtdeinicial.Text = "0";
                }
                else
                {
                    txtqtdeinicial.Text = dtTempoIntervalo.Rows[dtTempoIntervalo.Rows.Count - 1]["PROTEIROPORINTERVALO.FAIXAFINAL"].ToString();
                }
            }else
            {
                txtqtdeinicial.Text = "0";
            }
          
            txtqtdefinal.Text = "";
            txttempo.Text = "";
        }

        private bool Salvar()
        {
            bool _salvar = false;

            if (validacao() == true)
            {
                DataRow row = dtTempoIntervalo.NewRow();
                row["PROTEIROPORINTERVALO.FAIXAINICIAL"] = txtqtdeinicial.Text;
                row["PROTEIROPORINTERVALO.FAIXAFINAL"] = txtqtdefinal.Text;
                row["PROTEIROPORINTERVALO.TEMPOOPERACAO"] = txttempo.Text;

                dtTempoIntervalo.Rows.Add(row);

                btnNovo.PerformClick();
            }
            else
            {
                _salvar = false;
            }

            return _salvar;
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {

            }
        }

        private void FrmEstruturaTempoIntervalo_Load(object sender, EventArgs e)
        {
            txtqtdeinicial.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool validacao()
        {
            bool verifica = true;

            errorProvider.Clear();

            if (string.IsNullOrEmpty(txtqtdeinicial.Text))
            {
                errorProvider.SetError(txtqtdeinicial, "Favor preencher a quantidade inicial");
                verifica = false;
            }else
            {
                if ((Convert.ToInt16(txtqtdeinicial.Text) == 0 ) && dtTempoIntervalo.Rows.Count > 0)
                {
                    errorProvider.SetError(txtqtdeinicial, "A quantidade inicial deve ser maior que zero");
                    verifica = false;
                }
            }

            if (string.IsNullOrEmpty(txtqtdefinal.Text))
            {
                errorProvider.SetError(txtqtdefinal, "Favor preencher a quantidade final");
                verifica = false;
            }

            if (!string.IsNullOrEmpty(txtqtdeinicial.Text) && !string.IsNullOrEmpty(txtqtdefinal.Text)){
                if (Convert.ToInt16(txtqtdeinicial.Text) > Convert.ToInt16(txtqtdefinal.Text))
                {
                    errorProvider.SetError(txtqtdefinal, "A quantidade final deve ser maior que a quantidade inicial");
                    verifica = false;
                }
            }

            if (string.IsNullOrEmpty(txttempo.Text))
            {
                errorProvider.SetError(txttempo, "Favor preencher o tempo");
                verifica = false;
            }else
            {
                if (Convert.ToInt16(txttempo.Text) <= 0)
                {
                    errorProvider.SetError(txttempo, "Favor preencher o tempo");
                    verifica = false;
                }
            }

            return verifica;
        }

        private void txtqtdeinicial_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
            }
        }
    }
}
