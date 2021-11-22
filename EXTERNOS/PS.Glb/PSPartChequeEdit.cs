using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartChequeEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartChequeEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartBanco";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psTextoBox4.Text = "0";
            psTextoBox4.Edita = false;
            psCheckBox1.Chave = false;
        }

        public override void CarregaRegistro()
        {
            base.CarregaRegistro();

            psCheckBox1.Chave = false;
        }
    }
}
