using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartLancaRateioDPEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartLancaRateioDPEdit()
        {
            InitializeComponent();

            psLookup2.PSPart = "PSPartFilial";
            psLookup7.PSPart = "PSPartDepartamento";
            psMoedaBox1.Edita = false;
            psLookup2.Chave = false;
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

        }
    }
}
