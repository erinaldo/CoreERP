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
    public partial class PSPartNaturezaOrcamentoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartNaturezaOrcamentoEdit()
        {
            InitializeComponent();

            List<PS.Lib.ComboBoxItem> Item1 = new List<PS.Lib.ComboBoxItem>();
            Item1.Add(new PS.Lib.ComboBoxItem(0, "Entrada"));
            Item1.Add(new PS.Lib.ComboBoxItem(1, "Saida"));

            psComboBox1.DataSource = Item1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psCheckBox1.Checked = true;
        }
    }
}
