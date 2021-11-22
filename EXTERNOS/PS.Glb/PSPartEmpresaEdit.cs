using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartEmpresaEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartEmpresaEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartTipoRua";
            psLookup2.PSPart = "PSPartTipoBairro";
            psLookup3.PSPart = "PSPartPais";
            psLookup4.PSPart = "PSPartEstado";
            psLookup5.PSPart = "PSPartCidade";

            psTextoBox1.Edita = false;
            psTextoBox2.Edita = false;
            psTextoBox3.Edita = false;
            psTextoBox4.Edita = false;
            psTextoBox5.Edita = false;
        }

        private void psLookup5_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODETD", psLookup4.Text));
        }
    }
}