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
    public partial class PSPartRegiaoEstadoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartRegiaoEstadoEdit()
        {
            InitializeComponent();

            psLookup4.PSPart = "PSPartEstado";
        }
    }
}
