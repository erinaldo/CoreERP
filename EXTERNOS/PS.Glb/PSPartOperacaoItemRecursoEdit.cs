using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartOperacaoItemRecursoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartOperacaoItemRecursoEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartOperador";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psDateBox1.Text = "";
            psDateBox2.Text = "";
            psDateBox3.Text = "";
            psDateBox4.Text = "";
        }
    }
}
