using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartFormaPgtoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartFormaPgtoEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartRedeCartao";

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 0;
            list1[0].DisplayMember = "Dinheiro";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 1;
            list1[1].DisplayMember = "Cheque";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = 2;
            list1[2].DisplayMember = "Cartão";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[3].ValueMember = 3;
            list1[3].DisplayMember = "Boleto";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = 0;
            list2[0].DisplayMember = "Outro";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = 1;
            list2[1].DisplayMember = "Crédito";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[2].ValueMember = 2;
            list2[2].DisplayMember = "Débito";

            psComboBox2.DataSource = list2;
            psComboBox2.DisplayMember = "DisplayMember";
            psComboBox2.ValueMember = "ValueMember";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psCheckBox1.Checked = true;
        }

        private void psComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (psComboBox1.Value.ToString() == "2")
            {
                psComboBox2.Chave = true;
                psLookup1.Chave = true;
                psMoedaBox1.Edita = true;
            }
            else
            {
                psComboBox2.Chave = false;
                psComboBox2.Value = "0";
                psLookup1.Chave = false;
                psLookup1.Text = string.Empty;
                psLookup1.LoadLookup();
                psMoedaBox1.Edita = false;
                psMoedaBox1.Text = "0";
            }
        }
    }
}
