using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartContaEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartContaEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartBanco";
            psLookup2.PSPart = "PSPartAgencia";
            psLookup3.PSPart = "PSPartContaCorrente";
        }
        bool novo = true;
        

        public override void NovoRegistro()
        {
            base.NovoRegistro();
            psCheckBox1.Checked = true;
        }

        public override void CarregaRegistro()
        {
            novo = false;
            base.CarregaRegistro();
            if (string.IsNullOrEmpty(psDateBox1.Text))
            {
                psDateBox1.Chave = true;
                psMoedaBox1.Edita = true;
            }
            else
            {
                psDateBox1.Chave = false;
                psMoedaBox1.Edita = false;
            }
        }

        private void psLookup2_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            if (novo.Equals(true))
            {
                e.Filtro.Add(new PS.Lib.PSFilter("CODBANCO", psLookup1.Text));    
            }
            
        }

        private void psLookup3_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            if (novo.Equals(true))
            {
                e.Filtro.Add(new PS.Lib.PSFilter("CODBANCO", psLookup1.Text));
                e.Filtro.Add(new PS.Lib.PSFilter("CODAGENCIA", psLookup2.Text));     
            }
           
        }

        private void psLookup1_AfterLookup(object sender, Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODBANCO", psLookup1.Text));
            novo = true;
        }

        private void psLookup2_AfterLookup(object sender, Lib.LookupEventArgs e)
        {
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

        }

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {

        }
    }
}
