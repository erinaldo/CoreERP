using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartRegCaixaEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartRegCaixaEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartCaixa";
            psLookup2.PSPart = "PSPartOperador";
            psLookup3.PSPart = "PSPartFilial";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psDateBox2.Text = "";
            psDateBox2.Chave = false;
            psMoedaBox2.Edita = false;

            psMoedaBox1.Edita = true;
            psLookup2.Chave = true;
        }

        public override void CarregaRegistro()
        {
            base.CarregaRegistro();

            psDateBox2.Text = "";
            psDateBox2.Chave = false;
            psMoedaBox2.Edita = false;

            psMoedaBox1.Edita = false;
            psLookup2.Chave = false;
        }
    }
}
