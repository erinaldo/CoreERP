using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartNaturezaEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartNaturezaEdit()
        {
            InitializeComponent();

          

            psLookup1.PSPart = "PSPartOperMensagem";
            psLookup2.PSPart = "PSPartRegraICMS";
            psLookup3.PSPart = "PSPartRegraIPI";

            List<PS.Lib.ComboBoxItem> list8 = new List<PS.Lib.ComboBoxItem>();

            list8.Add(new PS.Lib.ComboBoxItem());
            list8[0].ValueMember = "E";
            list8[0].DisplayMember = "Entrada";

            list8.Add(new PS.Lib.ComboBoxItem());
            list8[1].ValueMember = "S";
            list8[1].DisplayMember = "Saída";

            psComboBox8.DataSource = list8;
            psComboBox8.DisplayMember = "DisplayMember";
            psComboBox8.ValueMember = "ValueMember";

            #region CLASSIFICAÇÃO DE VENDAS (2)

            List<PS.Lib.ComboBoxItem> listCLASSVENDA2 = new List<PS.Lib.ComboBoxItem>();

            listCLASSVENDA2.Add(new PS.Lib.ComboBoxItem());
            listCLASSVENDA2[0].ValueMember = "O";
            listCLASSVENDA2[0].DisplayMember = "Outra";

            listCLASSVENDA2.Add(new PS.Lib.ComboBoxItem());
            listCLASSVENDA2[1].ValueMember = "V";
            listCLASSVENDA2[1].DisplayMember = "Venda";

            listCLASSVENDA2.Add(new PS.Lib.ComboBoxItem());
            listCLASSVENDA2[2].ValueMember = "RS";
            listCLASSVENDA2[2].DisplayMember = "Revenda Sem CST";

            listCLASSVENDA2.Add(new PS.Lib.ComboBoxItem());
            listCLASSVENDA2[3].ValueMember = "RC";
            listCLASSVENDA2[3].DisplayMember = "Revenda Com CST";

            listCLASSVENDA2.Add(new PS.Lib.ComboBoxItem());
            listCLASSVENDA2[4].ValueMember = "CC";
            listCLASSVENDA2[4].DisplayMember = "Consumo Contribuinte";

            listCLASSVENDA2.Add(new PS.Lib.ComboBoxItem());
            listCLASSVENDA2[5].ValueMember = "CN";
            listCLASSVENDA2[5].DisplayMember = "Consumo Não Contribuinte";

            psComboBoxCLASSVENDA2.DataSource = listCLASSVENDA2;
            psComboBoxCLASSVENDA2.DisplayMember = "DisplayMember";
            psComboBoxCLASSVENDA2.ValueMember = "ValueMember";

            #endregion


        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psCheckBox1.Checked = true;
        }

        public override void CarregaRegistro()
        {
            base.CarregaRegistro();

            if (psComboBoxCLASSVENDA2.Text == "Consumo Contribuinte" || psComboBoxCLASSVENDA2.Text == "Consumo Não Contribuinte")
            {
                psCheckBoxCONTRIBUINTEICMS.Enabled = false;
            }
            if (psTextoBox1.textBox1.Text.Substring(0) == "6" || psTextoBox1.textBox1.Text.Substring(0) == "5")
            {
                psCheckBoxDENTRODOESTADO.Enabled = false;
            }

        }

        private void PSPartNaturezaEdit_Load(object sender, EventArgs e)
        {

        }

        private void psComboBoxCLASSVENDA2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (psComboBoxCLASSVENDA2.Text ==  "Consumo Contribuinte")
            {
                psCheckBoxCONTRIBUINTEICMS.checkBox1.Checked = true;
                psCheckBoxCONTRIBUINTEICMS.Enabled = false;
            }
            else if (psComboBoxCLASSVENDA2.Text == "Consumo Não Contribuinte")
            {
                psCheckBoxCONTRIBUINTEICMS.checkBox1.Checked = false;
                psCheckBoxCONTRIBUINTEICMS.Enabled = false;
            }

           
        }

        private void psTextoBox1_Validating(object sender, CancelEventArgs e)
        {
            if (psTextoBox1.textBox1.Text.Substring(0,1) == "6")
            {
                psCheckBoxDENTRODOESTADO.checkBox1.Checked = true;
                psCheckBoxDENTRODOESTADO.Enabled = false;
            }
            else if (psTextoBox1.textBox1.Text.Substring(0) == "5")
            {
                psCheckBoxDENTRODOESTADO.checkBox1.Checked = false;
                psCheckBoxDENTRODOESTADO.Enabled = false;
            }
        }
    }
}
