using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartTabCampoComplEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartTabCampoComplEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartTabDinamica";

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = "VPRODUTOCOMPL";
            list1[0].DisplayMember = "Produto";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = "VCLIFORCOMPL";
            list1[1].DisplayMember = "Cliente/Fornecedor";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = "GOPERCOMPL";
            list1[2].DisplayMember = "Operação";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[3].ValueMember = "GOPERITEMCOMPL";
            list1[3].DisplayMember = "Item da Operação";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[4].ValueMember = "FLANCACOMPL";
            list1[4].DisplayMember = "Lançamento Financeiro";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[5].ValueMember = "PORDEMCOMPL";
            list1[5].DisplayMember = "Ordem de Produção";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[6].ValueMember = "PORDEMAPONTAMENTOCOMPL";
            list1[6].DisplayMember = "Apontamento de Ordem";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[7].ValueMember = "PROTEIROESTRUTURACOMPL";
            list1[7].DisplayMember = "Estrutura";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[8].ValueMember = "VNATUREZACOMPL";
            list1[8].DisplayMember = "Natureza";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = "PSTextoBox";
            list2[0].DisplayMember = "Texto";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = "PSDateBox";
            list2[1].DisplayMember = "Data";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[2].ValueMember = "PSLookup";
            list2[2].DisplayMember = "Lista Dinâmica";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[3].ValueMember = "PSCheckBox";
            list2[3].DisplayMember = "Seleção";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[4].ValueMember = "PSMoedaBox";
            list2[4].DisplayMember = "Numérico";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[5].ValueMember = "TextEdit";
            list2[5].DisplayMember = "Texto Longo";

            psComboBox2.DataSource = list2;
            psComboBox2.DisplayMember = "DisplayMember";
            psComboBox2.ValueMember = "ValueMember";           
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psComboBox2.Chave = true;
            psMoedaBox1.Edita = true;

            psCheckBox1.Checked = true;

            psComboBox2.SelectedIndex = 0;

            psComboBox2_SelectedValueChanged(this, null);
        }

        public override void CarregaRegistro()
        {
            base.CarregaRegistro();

            psComboBox2.Chave = false;
            psMoedaBox1.Edita = false;
        }

        private void psComboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (psComboBox2.SelectedIndex == 0)
            {
                psMoedaBox1.Edita = true;

                psLookup1.Chave = false;
                psLookup1.Text = string.Empty;
                psLookup1.LoadLookup();

                psMoedaBox3.Edita = false;
                psMoedaBox3.Text = "0";
            }

            if (psComboBox2.SelectedIndex == 1)
            {
                psMoedaBox1.Edita = false;

                psLookup1.Chave = false;
                psLookup1.Text = string.Empty;
                psLookup1.LoadLookup();

                psMoedaBox3.Edita = false;
                psMoedaBox3.Text = "0";
            }

            if (psComboBox2.SelectedIndex == 2)
            {
                psMoedaBox1.Edita = false;

                psLookup1.Chave = true;
                psLookup1.LoadLookup();

                psMoedaBox3.Edita = false;
                psMoedaBox3.Text = "0";
            }

            if (psComboBox2.SelectedIndex == 3)
            {
                psMoedaBox1.Edita = false;

                psLookup1.Chave = false;
                psLookup1.Text = string.Empty;
                psLookup1.LoadLookup();

                psMoedaBox3.Edita = false;
                psMoedaBox3.Text = "0";
            }

            if (psComboBox2.SelectedIndex == 4)
            {
                psMoedaBox1.Edita = false;

                psLookup1.Chave = false;
                psLookup1.Text = string.Empty;
                psLookup1.LoadLookup();

                psMoedaBox3.Edita = true;
            }

            if (psComboBox2.SelectedIndex == 5)
            {
                psTextoBox3.Visible = true;
            }
            else
            {
                psTextoBox3.Visible = false;
            }
        }

        private void PSPartTabCampoComplEdit_Load(object sender, EventArgs e)
        {

        }

        private void psComboBox1_Load(object sender, EventArgs e)
        {

        }
    }
}
