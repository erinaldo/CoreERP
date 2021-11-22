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
    public partial class PSPartRegraIPIEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartRegraIPIEdit()
        {
            InitializeComponent();

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

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[5].ValueMember = "M";
            list2[5].DisplayMember = "Tributado 50%";

            psComboBox1.DataSource = list2;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psTextoBox1.Text = "0";
            psTextoBox1.Edita = false;
        }
    }
}
