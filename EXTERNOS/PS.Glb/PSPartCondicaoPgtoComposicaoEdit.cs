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
    public partial class PSPartCondicaoPgtoComposicaoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartCondicaoPgtoComposicaoEdit()
        {
            InitializeComponent();

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = "N";
            list1[0].DisplayMember = "Normal";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = "S";
            list1[1].DisplayMember = "Fora Semana";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = "D";
            list1[2].DisplayMember = "Fora Dezena";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[3].ValueMember = "Q";
            list1[3].DisplayMember = "Fora Dezena";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[4].ValueMember = "M";
            list1[4].DisplayMember = "Fora Mês";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[4].ValueMember = "A";
            list1[4].DisplayMember = "Fora Ano";

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
    }
}
