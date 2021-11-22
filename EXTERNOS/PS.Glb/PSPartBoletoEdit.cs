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
    public partial class PSPartBoletoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartBoletoEdit()
        {
            InitializeComponent();
            psLookup1.PSPart = "PSPartCliFor";
            psLookup2.PSPart = "PSPartConta";
            psLookup3.PSPart = "PSPartTipDoc";
            psLookup4.PSPart = "PSPartConvenio";
            psLookup5.PSPart = "PSPartBoletoStatus";
            List<PS.Lib.ComboBoxItem> list = new List<PS.Lib.ComboBoxItem>();

            list.Add(new PS.Lib.ComboBoxItem());
            list[0].ValueMember = 0;
            list[0].DisplayMember = "Não";

            list.Add(new PS.Lib.ComboBoxItem());
            list[1].ValueMember = 1;
            list[1].DisplayMember = "Sim";

            psComboBox1.DataSource = list;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";
        }
    }
}
