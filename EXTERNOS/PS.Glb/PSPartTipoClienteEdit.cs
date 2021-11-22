using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartTipoClienteEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartTipoClienteEdit()
        {
            InitializeComponent();
        }
        public override void NovoRegistro()
        {
            base.NovoRegistro();
            psTextoBox1.Edita = false;
        }
    }
}
