using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartCaixaEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartCaixaEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartFilial";
            psLookup2.PSPart = "PSPartFiscalImp";
            psLookup3.PSPart = "PSPartTipOper";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psCheckBox4.Checked = false;
            psCheckBox4_CheckedChanged(this, null);
        }

        private void psCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (psCheckBox4.Checked)
            {
                psCheckBox1.Chave = true;
                psCheckBox2.Chave = true;
                psCheckBox3.Chave = true;
            }
            else
            {
                psCheckBox1.Chave = false;
                psCheckBox2.Chave = false;
                psCheckBox3.Chave = false;

                psCheckBox1.Checked = false;
                psCheckBox2.Checked = false;
                psCheckBox3.Checked = false;
            }
        }
    }
}
