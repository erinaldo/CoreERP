using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartFilialEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartFilialEdit()
        {
            InitializeComponent();

            buttonSALVAR.Location = new Point(377, 19);
            buttonOK.Location = new Point(468, 19);
            buttonCANCELAR.Location = new Point(558, 19);

            psLookup1.PSPart = "PSPartTipoRua";
            psLookup2.PSPart = "PSPartTipoBairro";
            psLookup3.PSPart = "PSPartPais";
            psLookup4.PSPart = "PSPartEstado";
            psLookup5.PSPart = "PSPartCidade";

            psMaskedTextBox1.Mask = "00,000,000/0000-00";

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 1;
            list1[0].DisplayMember = "Simples Nacional";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 3;
            list1[1].DisplayMember = "Regime Normal";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psLookup1.Text = "1";
            psLookup1.LoadLookup();

            psLookup2.Text = "1";
            psLookup2.LoadLookup();
        }

        private void psLookup5_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODETD", psLookup4.Text));
        }

        private void psCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (psCheckBox2.checkBox1.Checked == true)
            {
                psTextoBox4.Enabled = true;
                psTextoBox11.Enabled = true;
                if (string.IsNullOrEmpty(psTextoBox4.textBox1.Text))
                {
                    psTextoBox4.textBox1.Text = "0";
                }
                else
                {
                    psTextoBox4.Text = AppLib.Util.Format.CompletarZeroEsquerda(Convert.ToInt32(psTextoBox11.textBox1.Text), "0");
                }
            }
            else
            {
                psTextoBox4.Enabled = false;
                psTextoBox11.Enabled = false;
            }
        }

        private void psTextoBox11_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(psTextoBox11.textBox1.Text))
            {
                psTextoBox4.textBox1.MaxLength = Convert.ToInt32(psTextoBox11.textBox1.Text);
            }
        }

        private void psTextoBox4_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(psTextoBox4.textBox1.Text))
            {
                psTextoBox4.textBox1.Text = AppLib.Util.Format.CompletarZeroEsquerda(Convert.ToInt32(psTextoBox11.textBox1.Text), psTextoBox4.textBox1.Text);
            }
        }

        private void psCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
         
        }

        private void PSPartFilialEdit_Load(object sender, EventArgs e)
        {
            psTextoBox4.Enabled = false;
            psTextoBox11.Enabled = false;

            if (psCheckBox2.Checked == true)
            {
                psCheckBox2.Enabled = false;
                psTextoBox4.Enabled = false;
                psTextoBox11.Enabled = false;
            }
        }

        private void psTextoBox4_Load(object sender, EventArgs e)
        { 

        }
    }
}
