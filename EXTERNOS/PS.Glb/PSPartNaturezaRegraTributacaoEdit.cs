using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartNaturezaRegraTributacaoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartNaturezaRegraTributacaoEdit()
        {
            InitializeComponent();

            psLookup4.PSPart = "PSPartEstado";
            psLookup1.PSPart = "PSPartRegiao";

            List<PS.Lib.ComboBoxItem> list8 = new List<PS.Lib.ComboBoxItem>();

            list8.Add(new PS.Lib.ComboBoxItem());
            list8[0].ValueMember = 1;
            list8[0].DisplayMember = "Estado";

            list8.Add(new PS.Lib.ComboBoxItem());
            list8[1].ValueMember = 2;
            list8[1].DisplayMember = "Região";

            list8.Add(new PS.Lib.ComboBoxItem());
            list8[2].ValueMember = 0;
            list8[2].DisplayMember = string.Empty;

            psComboBox8.DataSource = list8;
            psComboBox8.DisplayMember = "DisplayMember";
            psComboBox8.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = "S";
            list1[0].DisplayMember = "Sim";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = "N";
            list1[1].DisplayMember = "Não";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = "D";
            list1[2].DisplayMember = "Depende";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psTextoBox1.Text = "0";
            psTextoBox1.Edita = false;
        }

        private void psComboBox8_SelectedValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(psComboBox8.Text))
            {
                psLookup1.Enabled = false;
                psLookup4.Enabled = false;
                
                psLookup1.textBox1.Text = string.Empty;
                psLookup1.textBox2.Text = string.Empty;

                psLookup4.textBox1.Text = string.Empty;
                psLookup4.textBox2.Text = string.Empty;
            }
            else if (psComboBox8.Text == "Região")
            {
                psLookup1.Enabled = true;
                psLookup4.Enabled = false;
            }
            else
            {
                psLookup4.Enabled = true;
                psLookup1.Enabled = false;
            }
        }
    }
}
