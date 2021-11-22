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
    public partial class PSPartRegraICMSEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartRegraICMSEdit()
        {
            InitializeComponent();
            //Cria lista para combo de seleção MVA
            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = "";
            list1[0].DisplayMember = "";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = "A";
            list1[1].DisplayMember = "MVA Ajustado";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = "I";
            list1[2].DisplayMember = "MVA Importado";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[3].ValueMember = "O";
            list1[3].DisplayMember = "MVA Original";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();
            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = "D";
            list2[0].DisplayMember = "Diferido";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = "I";
            list2[1].DisplayMember = "Isento";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[2].ValueMember = "O";
            list2[2].DisplayMember = "Outros";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[3].ValueMember = "S";
            list2[3].DisplayMember = "Suspenso";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[4].ValueMember = "T";
            list2[4].DisplayMember = "Tributado";

            psComboBox2.DataSource = list2;
            psComboBox2.DisplayMember = "DisplayMember";
            psComboBox2.ValueMember = "ValueMember";

        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psTextoBox1.Text = "0";
            psTextoBox1.Edita = false;
        }

        private void psCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (psCheckBox2.Checked == true)
            {
                psCheckBox3.Enabled = true;
                psMoedaBox3.Enabled = true;
                psMoedaBox4.Enabled = true;
            }
            else
            {
                psCheckBox3.Enabled = false;
                psMoedaBox3.Enabled = false;
                psMoedaBox4.Enabled = false;

                psCheckBox3.Checked = false;
                psMoedaBox3.Text = "0,00";
                psMoedaBox4.Text = "0,00";
            }
        }

        private void PSPartRegraICMSEdit_Load(object sender, EventArgs e)
        {
            psCheckBox2_CheckedChanged(this, e);
            psCheckBox8_CheckedChanged(this, e);
        }

        private void psCheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (psCheckBox8.Checked == true)
            {
                groupBox1.Enabled = true;
                psMoedaBox5.Edita = true;
                psComboBox1.Enabled = true;
            }
            else
            {
                groupBox1.Enabled = false;
                psMoedaBox5.Edita = false;
                psComboBox1.Enabled = false;
                //Limpa os campos
                psCheckBox4.Checked = false;
                psCheckBox5.Checked = false;
                psCheckBox6.Checked = false;
                psCheckBox7.Checked = false;
                psMoedaBox5.Text = "0";
                psComboBox1.Text = "";

            }
        }

    }
}
