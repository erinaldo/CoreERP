using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartParamVarejoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartParamVarejoEdit()
        {
            InitializeComponent();

            psLookup3.PSPart = "PSPartMoeda";
            psLookup5.PSPart = "PSPartTipDoc";
            psLookup1.PSPart = "PSPartTipDoc";

            psTextoBox4.Edita = false;
            psTextoBox17.Edita = false;

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = "E";
            list1[0].DisplayMember = "Edita";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = "M";
            list1[1].DisplayMember = "Não Mostra";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = "E";
            list2[0].DisplayMember = "Edita";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = "M";
            list2[1].DisplayMember = "Não Mostra";

            psComboBox2.DataSource = list2;
            psComboBox2.DisplayMember = "DisplayMember";
            psComboBox2.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list3 = new List<PS.Lib.ComboBoxItem>();

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[0].ValueMember = "E";
            list3[0].DisplayMember = "Edita";

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[1].ValueMember = "M";
            list3[1].DisplayMember = "Não Mostra";

            psComboBox3.DataSource = list3;
            psComboBox3.DisplayMember = "DisplayMember";
            psComboBox3.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list4 = new List<PS.Lib.ComboBoxItem>();

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[0].ValueMember = "E";
            list4[0].DisplayMember = "Edita";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[1].ValueMember = "M";
            list4[1].DisplayMember = "Não Mostra";

            psComboBox4.DataSource = list4;
            psComboBox4.DisplayMember = "DisplayMember";
            psComboBox4.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list5 = new List<PS.Lib.ComboBoxItem>();

            list5.Add(new PS.Lib.ComboBoxItem());
            list5[0].ValueMember = "E";
            list5[0].DisplayMember = "Edita";

            list5.Add(new PS.Lib.ComboBoxItem());
            list5[1].ValueMember = "M";
            list5[1].DisplayMember = "Não Mostra";

            psComboBox5.DataSource = list5;
            psComboBox5.DisplayMember = "DisplayMember";
            psComboBox5.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list6 = new List<PS.Lib.ComboBoxItem>();

            list6.Add(new PS.Lib.ComboBoxItem());
            list6[0].ValueMember = 0;
            list6[0].DisplayMember = "Não";

            list6.Add(new PS.Lib.ComboBoxItem());
            list6[1].ValueMember = 1;
            list6[1].DisplayMember = "Sim";

            psComboBox6.DataSource = list6;
            psComboBox6.DisplayMember = "DisplayMember";
            psComboBox6.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list7 = new List<PS.Lib.ComboBoxItem>();
            list7.Add(new PS.Lib.ComboBoxItem(0, "Não"));
            list7.Add(new PS.Lib.ComboBoxItem(1, "Sim"));
            psComboBox7.DataSource = list7;
            psComboBox7.DisplayMember = "DisplayMember";
            psComboBox7.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list8 = new List<PS.Lib.ComboBoxItem>();
            list8.Add(new PS.Lib.ComboBoxItem(0, "Não"));
            list8.Add(new PS.Lib.ComboBoxItem(1, "Sim"));
            psComboBox8.DataSource = list8;
            psComboBox8.DisplayMember = "DisplayMember";
            psComboBox8.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list9 = new List<PS.Lib.ComboBoxItem>();
            list9.Add(new PS.Lib.ComboBoxItem(0, "E-mail da Empresa corrente"));
            list9.Add(new PS.Lib.ComboBoxItem(1, "E-mail do Usuário corrente"));
            psComboBox9.DataSource = list9;
            psComboBox9.DisplayMember = "DisplayMember";
            psComboBox9.ValueMember = "ValueMember";

            // Quantidade Decimal Tabela de Preço

            List<PS.Lib.ComboBoxItem> ListaQuantidadeDecimal = new List<PS.Lib.ComboBoxItem>();

            ListaQuantidadeDecimal.Add(new PS.Lib.ComboBoxItem());
            ListaQuantidadeDecimal[0].ValueMember = 2;
            ListaQuantidadeDecimal[0].DisplayMember = "2";

            ListaQuantidadeDecimal.Add(new PS.Lib.ComboBoxItem());
            ListaQuantidadeDecimal[1].ValueMember = 3;
            ListaQuantidadeDecimal[1].DisplayMember = "3";

            ListaQuantidadeDecimal.Add(new PS.Lib.ComboBoxItem());
            ListaQuantidadeDecimal[2].ValueMember = 4;
            ListaQuantidadeDecimal[2].DisplayMember = "4";

            ListaQuantidadeDecimal.Add(new PS.Lib.ComboBoxItem());
            ListaQuantidadeDecimal[3].ValueMember = 5;
            ListaQuantidadeDecimal[3].DisplayMember = "5";

            ListaQuantidadeDecimal.Add(new PS.Lib.ComboBoxItem());
            ListaQuantidadeDecimal[4].ValueMember = 6;
            ListaQuantidadeDecimal[4].DisplayMember = "6";

            psComboboxTabelaPreco.DataSource = ListaQuantidadeDecimal;
            psComboboxTabelaPreco.DisplayMember = "DisplayMember";
            psComboboxTabelaPreco.ValueMember = "ValueMember";

            PSComboCODQUERY.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODQUERY, DESCRICAO FROM GQUERY WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });
            PSComboCODQUERY.ValueMember = "CODQUERY";
            PSComboCODQUERY.DisplayMember = "DESCRICAO";

            psComboBoxEstoqueMinimo.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODQUERY, DESCRICAO FROM GQUERY WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });
            psComboBoxEstoqueMinimo.ValueMember = "CODQUERY";
            psComboBoxEstoqueMinimo.DisplayMember = "DESCRICAO";

            psComboBoxCustoMedio.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODQUERY, DESCRICAO FROM GQUERY WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });
            psComboBoxCustoMedio.ValueMember = "CODQUERY";
            psComboBoxCustoMedio.DisplayMember = "DESCRICAO";

            List<PS.Lib.ComboBoxItem> listBuscaProdutoPor = new List<PS.Lib.ComboBoxItem>();

            listBuscaProdutoPor.Add(new PS.Lib.ComboBoxItem());
            listBuscaProdutoPor[0].ValueMember = "0";
            listBuscaProdutoPor[0].DisplayMember = "Cód. Produto";

            listBuscaProdutoPor.Add(new PS.Lib.ComboBoxItem());
            listBuscaProdutoPor[1].ValueMember = "1";
            listBuscaProdutoPor[1].DisplayMember = "Cód. Auxiliar";


            psComboBuscaProdutoPor.DataSource = listBuscaProdutoPor;
            psComboBuscaProdutoPor.DisplayMember = "DisplayMember";
            psComboBuscaProdutoPor.ValueMember = "ValueMember";

            #region CONTROLA SALDO ESTQUE

            List<PS.Lib.ComboBoxItem> listCONTROLASALDOESTQUE = new List<PS.Lib.ComboBoxItem>();

            listCONTROLASALDOESTQUE.Add(new PS.Lib.ComboBoxItem());
            listCONTROLASALDOESTQUE[0].ValueMember = "B";
            listCONTROLASALDOESTQUE[0].DisplayMember = "Bloqueia";

            listCONTROLASALDOESTQUE.Add(new PS.Lib.ComboBoxItem());
            listCONTROLASALDOESTQUE[1].ValueMember = "A";
            listCONTROLASALDOESTQUE[1].DisplayMember = "Avisa";

            psComboBoxCONTROLASALDOESTQUE.DataSource = listCONTROLASALDOESTQUE;
            psComboBoxCONTROLASALDOESTQUE.DisplayMember = "DisplayMember";
            psComboBoxCONTROLASALDOESTQUE.ValueMember = "ValueMember";

            #endregion

        }

        public override void CarregaRegistro()
        {
            base.CarregaRegistro();

            psCheckBox1_CheckedChanged(this, null);
            psCheckBox3_CheckedChanged(this, null);

            if (!string.IsNullOrEmpty(psTextoBox4.Text))
            {
                psTextoBox3.Edita = false;
            }
            else
            {
                psTextoBox3.Edita = true;
            }
        }

        private void psCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (psCheckBox1.Checked)
            {
                psTextoBox3.Edita = true;
            }
            else
            {
                psTextoBox3.Edita = false;
                psTextoBox3.Text = string.Empty;
            }
        }

        private void psCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (psCheckBox3.Checked)
            {
                psTextoBox16.Edita = true;
            }
            else
            {
                psTextoBox16.Edita = false;
                psTextoBox17.Text = string.Empty;
            }
        }

        private void PSPartParamVarejoEdit_Load(object sender, EventArgs e)
        {

        }

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {

        }
    }
}
