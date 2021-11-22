using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartNFERegCabEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartNFERegCabEdit()
        {
            InitializeComponent();

            psLookup14.PSPart = "PSPartUsuario";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psTextoBox1.Text = "0";
            psTextoBox1.Edita = false;
            psDateBox1.Chave = false;
            psLookup14.Chave = false;
            psLookup14.Text = PS.Lib.Contexto.Session.CodUsuario;
            psLookup14.LoadLookup();
        }

        public override void CarregaRegistro()
        {
            base.CarregaRegistro();

            psDateBox1.Chave = false;
            psLookup14.Chave = false;
        }
    }
}
