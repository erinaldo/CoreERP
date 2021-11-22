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
    public partial class PSPartTipoTransporteEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartTipoTransporteEdit()
        {
            InitializeComponent();
            psLookup1.PSPart = "PSPartTransportadora";
        }
    }
}
