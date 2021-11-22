using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartConsultaNFEAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        public PSPartConsultaNFEAppFrm()
        {
            InitializeComponent();
        }

        private void PSPartConsultaNFEAppFrm_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://www.nfe.fazenda.gov.br/portal/consulta.aspx?tipoConsulta=completa"); 
        }

        public override Boolean Execute()
        {
            return false;
        }
    }
}
