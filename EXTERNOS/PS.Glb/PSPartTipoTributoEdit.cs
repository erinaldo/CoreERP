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
    public partial class PSPartTipoTributoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartTipoTributoEdit()
        {
            InitializeComponent();

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = "M";
            list1[0].DisplayMember = "Municipal";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = "E";
            list1[1].DisplayMember = "Estadual";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = "F";
            list1[2].DisplayMember = "Federal";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = "M";
            list2[0].DisplayMember = "Mensal";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = "S";
            list2[1].DisplayMember = "Semanal";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[2].ValueMember = "Q";
            list2[2].DisplayMember = "Quinzenal";

            psComboBox2.DataSource = list2;
            psComboBox2.DisplayMember = "DisplayMember";
            psComboBox2.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list3 = new List<PS.Lib.ComboBoxItem>();

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[0].ValueMember = 0;
            list3[0].DisplayMember = "Fixa";

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[1].ValueMember = 1;
            list3[1].DisplayMember = "Variável";

            psComboBox3.DataSource = list3;
            psComboBox3.DisplayMember = "DisplayMember";
            psComboBox3.ValueMember = "ValueMember";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psCheckBox1.Checked = true;
            psDateBox1.Text = string.Empty;
            psDateBox2.Text = string.Empty;
        }
    }
}
