using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartAcessoTipOperEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartAcessoTipOperEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartTipOper";
        }
    }
}
