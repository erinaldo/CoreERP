using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartTipOperMensagemEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartTipOperMensagemEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartOperMensagem";
        }
    }
}
