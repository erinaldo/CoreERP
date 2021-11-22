using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartObjetoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartObjetoEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartTipoObjeto";
            psLookup3.PSPart = "PSPartModelo";
            psLookup4.PSPart = "PSPartSubModelo";
            psLookup5.PSPart = "PSPartCliFor";

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();
            PS.Lib.ComboBoxItem member = new PS.Lib.ComboBoxItem();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 0;
            list1[0].DisplayMember = "Novo";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 1;
            list1[1].DisplayMember = "Usado";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";
        }
    }
}
