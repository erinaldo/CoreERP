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
    public partial class PSPartNFEstadualHistoricoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartNFEstadualHistoricoEdit()
        {
            InitializeComponent();

            psLookup14.PSPart = "PSPartUsuario";
        }

        public override void CarregaRegistro()
        {
            base.CarregaRegistro();

            psDateBox1.Chave = false;
            psLookup14.Chave = false;
            psMemoBox1.ReadOnly = true;
        }
    }
}
