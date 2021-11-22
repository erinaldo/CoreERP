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
    public partial class PSPartProdutoFiscalEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartProdutoFiscalEdit()
        {
            InitializeComponent();

            psLookup4.PSPart = "PSPartEstado";

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 0;
            list1[0].DisplayMember = "0 - Margem Valor Agregado (%)";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 1;
            list1[1].DisplayMember = "1 - Pauta (Valor)";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = 2;
            list1[2].DisplayMember = "2 - Preço Tabelado Máximo (Valor)";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[3].ValueMember = 3;
            list1[3].DisplayMember = "3 - Valor da Operação";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = 0;
            list2[0].DisplayMember = "0 - Preço Tabelado ou Máximo Sugerido";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = 1;
            list2[1].DisplayMember = "1 - Lista Negativa (Valor)";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[2].ValueMember = 2;
            list2[2].DisplayMember = "2 - Lista Positiva (Valor)";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[3].ValueMember = 3;
            list2[3].DisplayMember = "3 - Lista Neutra (Valor)";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[4].ValueMember = 4;
            list2[4].DisplayMember = "4 - Margem Valor Agregado (%)";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[5].ValueMember = 5;
            list2[5].DisplayMember = "5 - Pauta (Valor)";

            psComboBox2.DataSource = list2;
            psComboBox2.DisplayMember = "DisplayMember";
            psComboBox2.ValueMember = "ValueMember";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psComboBox1.SelectedIndex = 3;
            psComboBox2.SelectedIndex = 4;
        }
    }
}
