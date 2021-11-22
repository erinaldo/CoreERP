using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartUsuarioPerfilEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartUsuarioPerfilEdit()
        {
            InitializeComponent();

            psLookup2.PSPart = "PSPartUsuario";
        }

        private void PSPartUsuarioPerfilEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
