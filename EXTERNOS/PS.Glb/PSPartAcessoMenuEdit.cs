using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartAcessoMenuEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private int GLB_TIPO_PSPART;

        public PSPartAcessoMenuEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartPart";

            GLB_TIPO_PSPART = 99;
        }

        private void psCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (psCheckBox1.Checked == false)
            {
                psCheckBox2.Checked = false;
                psCheckBox2.Chave = false;
                psCheckBox3.Checked = false;
                psCheckBox3.Chave = false;
                psCheckBox4.Checked = false;
                psCheckBox4.Chave = false;
            }
            else
            {
                if (GLB_TIPO_PSPART == 1)
                    return;

                psCheckBox2.Chave = true;
                psCheckBox3.Chave = true;
                psCheckBox4.Chave = true;            
            }
        }

        private void Parametros()
        {
            string sSql = "SELECT TIPO FROM GPSPART WHERE CODPSPART = ?";

            GLB_TIPO_PSPART = int.Parse(dbs.QueryValue("99", sSql, psLookup1.Text).ToString());

            if (GLB_TIPO_PSPART != 99)
            {
                if (GLB_TIPO_PSPART == 0)
                {
                    psCheckBox2.Chave = true;
                    psCheckBox3.Chave = true;
                    psCheckBox4.Chave = true;
                }

                if (GLB_TIPO_PSPART == 1)
                {
                    psCheckBox2.Checked = false;
                    psCheckBox2.Chave = false;
                    psCheckBox3.Checked = false;
                    psCheckBox3.Chave = false;
                    psCheckBox4.Checked = false;
                    psCheckBox4.Chave = false;                
                }
            }
        }

        private void psLookup1_AfterLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            if (psLookup1.Text != string.Empty)
            {
                Parametros();
            }
        }

        private void PSPartAcessoMenuEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
