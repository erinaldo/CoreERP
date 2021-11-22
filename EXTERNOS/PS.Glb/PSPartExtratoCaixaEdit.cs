using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartExtratoCaixaEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartExtratoCaixaEdit()
        {
            InitializeComponent();

            psLookup2.PSPart = "PSPartFilial";
            psLookup4.PSPart = "PSPartConta";
            psLookup1.PSPart = "PSPartFilial";
            psLookup3.PSPart = "PSPartConta";
            psLookup6.PSPart = "PSPartCentroCusto";
            psLookup16.PSPart = "PSPartNaturezaOrcamento";
            psLookup7.PSPart = "PSPartCentroCusto";
            psLookup5.PSPart = "PSPartNaturezaOrcamento";

            List<PS.Lib.ComboBoxItem> Item1 = new List<PS.Lib.ComboBoxItem>();
            Item1.Add(new PS.Lib.ComboBoxItem(0, "Entrada"));
            Item1.Add(new PS.Lib.ComboBoxItem(1, "Saida"));
            Item1.Add(new PS.Lib.ComboBoxItem(2, "Transferência"));
            Item1.Add(new PS.Lib.ComboBoxItem(4, "Cheque Saída"));
            Item1.Add(new PS.Lib.ComboBoxItem(5, "Cheque Entrada"));

            psComboBox1.DataSource = Item1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";

            //psCheckBox1.Chave = false;
            //psDateBox2.Chave = false;
            psTextoBox4.Visible = false;
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psTextoBox1.Text = "0";
            psTextoBox1.Edita = false;
            psCheckBox1.Checked = false;
            psDateBox2.Text = null; 

            psCheckBox1_CheckedChanged(this, null);
            psComboBox1.SelectedIndex = 0;
        }

        public override void CarregaRegistro()
        {
            base.CarregaRegistro();

            psCheckBox1.Enabled = false;
            psDateBox2.Enabled = false;
            psCheckBox1_CheckedChanged(this, null);
        }

        private void psComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (psComboBox1.SelectedIndex == 2)
            {
                psLookup1.Visible = true;
                psLookup3.Visible = true;
                psLookup7.Visible = true;
                psLookup5.Visible = true;

            }
            else
            {
                psLookup1.Visible = false;
                psLookup1.Text = string.Empty;
                psLookup3.Visible = false;
                psLookup3.Text = string.Empty;
                psLookup7.Visible = false;
                psLookup7.Text = string.Empty;
                psLookup5.Visible = false;
                psLookup5.Text = string.Empty;
            }
        }

        private void psCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (psCheckBox1.Checked)
            {
                psDateBox2.Chave = true;
            }
            else
            {
                psDateBox2.Chave = false;
                psDateBox2.Text = null;
            }
            */
        }

        private void PSPartExtratoCaixaEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
