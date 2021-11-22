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
    public partial class PSPartVendedorEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartVendedorEdit()
        {
            InitializeComponent();
            psLookup1.PSPart = "PSPartTipoRua";
            psLookup2.PSPart = "PSPartTipoBairro";
            psLookup3.PSPart = "PSPartPais";
            psLookup4.PSPart = "PSPartEstado";
            psLookup5.PSPart = "PSPartCidade";
            psLookup6.PSPart = "PSPartUsuario";


            List<PS.Lib.ComboBoxItem> list = new List<PS.Lib.ComboBoxItem>();

            list.Add(new PS.Lib.ComboBoxItem());
            list[0].ValueMember = "F";
            list[0].DisplayMember = "Física";

            list.Add(new PS.Lib.ComboBoxItem());
            list[1].ValueMember = "J";
            list[1].DisplayMember = "Jurídica";

            psComboBox1.DataSource = list;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";

            psMaskedTextBox2.Mask = "(00) 0000-0000";
            psMaskedTextBox3.Mask = "(00) 00000-0000";
            psMaskedTextBox4.Mask = "(00) 0000-0000";

        }
        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psCheckBox1.Checked = true;

            psLookup1.Text = "1";
            psLookup1.LoadLookup();

            psLookup2.Text = "1";
            psLookup2.LoadLookup();
        }

        private void psLookup5_BeforeLookup(object sender, Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODETD", psLookup4.Text));
        }

        private void btnSearchCep_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(psTextoBox4.Text))
            {
                //AppLib.Util.Correios.Endereco endereco = new AppLib.Util.Correios.Endereco();
                //endereco = AppLib.Util.Correios.BuscaCep.GetEndereco(psTextoBox4.Text, 10000);
                //psTextoBox6.Text = endereco.logradouro;
                //psTextoBox9.Text = endereco.bairro;
                //psLookup4.textBox1.Text = endereco.estado;
                //psLookup4.textBox2.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NOME FROM GESTADO WHERE CODETD = ?", new object[] { endereco.estado }).ToString();
                //psLookup5.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODCIDADE FROM GCIDADE WHERE NOME = ? AND CODETD = ?", new object[] { endereco.cidade.Replace("&#39;", " ").ToString(), endereco.estado }).ToString();
                //psLookup5.textBox2.Text = endereco.cidade.Replace("&#39;", " ").ToString();
                // João Pedro Luchiari - 29/01/2018
                PS.Glb.Class.WebCep web = new Class.WebCep(psTextoBox4.Text);
                psTextoBox6.Text = web.Lagradouro;
                psLookup1.textBox2.Text = web.TipoLagradouro;
                psLookup1.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODTIPORUA FROM GTIPORUA WHERE NOME = ?", new object[] { web.TipoLagradouro }).ToString();
                psTextoBox9.Text = web.Bairro;
                psLookup4.textBox1.Text = web.UF;
                psLookup4.textBox2.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GESTADO WHERE CODETD = ?", new object[] { web.UF }).ToString();
                psLookup5.textBox2.Text = web.Cidade;
                psLookup5.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODCIDADE FROM GCIDADE WHERE NOME = ?", new object[] { web.Cidade }).ToString();
                psLookup3.textBox1.Text = "1";
                psLookup3.textBox2.Text = "Brasil";
            }
            else
            {
                MessageBox.Show("Favor preecher o CEP corretamente", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void psComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (psComboBox1.Value.ToString() == "J")
            {
                psMaskedTextBox1.Mask = "00,000,000/0000-00";
                psTextoBox17.Caption = "Inscrição Estadual";
            }

            if (psComboBox1.Value.ToString() == "F")
            {
                psMaskedTextBox1.Mask = "000,000,000-00";
                psTextoBox17.Caption = "RG";
            }
        }

        private void PSPartVendedorEdit_Load(object sender, EventArgs e)
        {
            if (psComboBox1.Value == "F")
            {
                psTextoBox17.Caption = "RG";
            }
            else
            {
                psTextoBox17.Caption = "Inscrição Estadual";
            }
        }
    }
}
