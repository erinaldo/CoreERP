using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartTransportadoraEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartTransportadoraEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartTipoRua";
            psLookup2.PSPart = "PSPartTipoBairro";
            psLookup3.PSPart = "PSPartPais";
            psLookup4.PSPart = "PSPartEstado";
            psLookup5.PSPart = "PSPartCidade";

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = 0;
            list2[0].DisplayMember = "Jurídica";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = 1;
            list2[1].DisplayMember = "Física";

            psComboBox2.DataSource = list2;
            psComboBox2.DisplayMember = "DisplayMember";
            psComboBox2.ValueMember = "ValueMember";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psCheckBox1.Checked = true;

            psTextoBox1.Text = "0";
            psTextoBox1.Edita = false;

            psLookup1.Text = "1";
            psLookup1.LoadLookup();

            psLookup2.Text = "1";
            psLookup2.LoadLookup();
        }

        private void psLookup5_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODETD", psLookup4.Text));
        }

        private void psComboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (psComboBox2.Value.ToString() == "0")
            {
                psMaskedTextBox1.Mask = "00,000,000/0000-00";
              
            }

            if (psComboBox2.Value.ToString() == "1")
            {
                psMaskedTextBox1.Mask = "000,000,000-00";
              
            }
        }

        private void PSPartTransportadoraEdit_Load(object sender, EventArgs e)
        {
         

        }

        private void psTextoBox15_Load(object sender, EventArgs e)
        {

        }

        private void psMaskedTextBox1_Validating(object sender, CancelEventArgs e)
        {

        }

        private void psComboBox2_Load(object sender, EventArgs e)
        {

        }

        private void buttonCANCELAR_Click(object sender, EventArgs e)
        {

        }

        private void psTextoBox1_Load(object sender, EventArgs e)
        {

        }

        private void psLookup1_Load(object sender, EventArgs e)
        {

        }

        private void btnSearchCep_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(psTextoBox5.Text))
            {
                PS.Glb.Class.WebCep web = new Class.WebCep(psTextoBox5.Text);
                psTextoBox6.Text = web.Lagradouro;
                psLookup1.textBox2.Text = web.TipoLagradouro;
                psLookup1.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODTIPORUA FROM GTIPORUA WHERE NOME = ?", new object[] { web.TipoLagradouro }).ToString();
                psTextoBox9.Text = web.Bairro;
                psLookup4.textBox1.Text = web.UF;
                psLookup4.textBox2.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GESTADO WHERE CODETD = ?", new object[] { web.UF }).ToString();
                psLookup5.textBox2.Text = web.Cidade;
                psLookup5.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODCIDADE FROM GCIDADE WHERE NOME = ?", new object[] { web.Cidade }).ToString();
                psLookup3.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODPAIS FROM GPAIS WHERE NOME = ?", new object[] { "Brasil" }).ToString();
                psLookup3.textBox2.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GPAIS WHERE CODPAIS = ?", new object[] { "1" }).ToString();
            }
        }

        private void psPis_Load(object sender, EventArgs e)
        {

        }

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {

        }
    }
}
