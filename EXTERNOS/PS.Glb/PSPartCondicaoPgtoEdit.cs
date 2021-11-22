using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartCondicaoPgtoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartCondicaoPgtoEdit()
        {
            InitializeComponent();

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();
            PS.Lib.ComboBoxItem member = new PS.Lib.ComboBoxItem();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 0;
            list1[0].DisplayMember = "Compra";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 1;
            list1[1].DisplayMember = "Venda";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = 2;
            list1[2].DisplayMember = "Ambos";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";

            textBox2.Enabled = false;
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psCheckBox1.Checked = true;
            textBox2.Text = "0,00";
            pictureBox1.Image = Properties.Resources.erro;
        }

        public override void CarregaRegistro()
        {
            base.CarregaRegistro();

            this.ValidaPercentualComposicao();
        }

        public void ValidaPercentualComposicao()
        {
            PSPartCondicaoPgtoData psPartCondicaoPgtoData = new PSPartCondicaoPgtoData();
            psPartCondicaoPgtoData._tablename = "VCONDICAOPGTO";
            psPartCondicaoPgtoData._keys = new string[] { "CODEMPRESA", "CODCONDICAO" };
            Decimal PercValor = psPartCondicaoPgtoData.ValidaPercentualComposicao(PS.Lib.Contexto.Session.Empresa.CodEmpresa, psTextoBox1.Text);
            if (PercValor != 100)
                pictureBox1.Image = Properties.Resources.erro;
            else
                pictureBox1.Image = Properties.Resources.ok;

            textBox2.Text = PercValor.ToString("###,###,##0.00");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidaPercentualComposicao();
        }
    }
}
