using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartTributoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartTributoEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartTipoTributo";

            List<PS.Lib.ComboBoxItem> list5 = new List<PS.Lib.ComboBoxItem>();

            list5.Add(new PS.Lib.ComboBoxItem());
            list5[0].ValueMember = 0;
            list5[0].DisplayMember = "Tributo";

            list5.Add(new PS.Lib.ComboBoxItem());
            list5[1].ValueMember = 1;
            list5[1].DisplayMember = "Produto";

            list5.Add(new PS.Lib.ComboBoxItem());
            list5[2].ValueMember = 2;
            list5[2].DisplayMember = "Tipo Operação";

            list5.Add(new PS.Lib.ComboBoxItem());
            list5[3].ValueMember = 3;
            list5[3].DisplayMember = "Natureza";

            list5.Add(new PS.Lib.ComboBoxItem());
            list5[4].ValueMember = 4;
            list5[4].DisplayMember = "Estado";

            list5.Add(new PS.Lib.ComboBoxItem());
            list5[5].ValueMember = 5;
            list5[5].DisplayMember = "Regra";

            psComboBox5.DataSource = list5;
            psComboBox5.DisplayMember = "DisplayMember";
            psComboBox5.ValueMember = "ValueMember";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psCheckBox1.Checked = true;
        }
    }
}
