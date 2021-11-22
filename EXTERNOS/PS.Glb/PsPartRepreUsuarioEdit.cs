using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PsPartRepreUsuarioEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PsPartRepreUsuarioEdit()
        {
            InitializeComponent();

            psLookup2.PSPart = "PSPartUsuario";
        }
    }
}
