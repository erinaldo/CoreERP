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
    public partial class PSPartAcessoRelatorioEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartAcessoRelatorioEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartPerfil";
        }

        private void PSPartAcessoRelatorioEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
