using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartTipOperEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        PS.Lib.Global gb = new PS.Lib.Global();


        string tabela = "GOPERMENSAGEMFISCO";
        string relacionamento = "INNER JOIN VOPERMENSAGEM ON GOPERMENSAGEMFISCO.CODMENSAGEM = VOPERMENSAGEM.CODMENSAGEM AND GOPERMENSAGEMFISCO.CODEMPRESA = VOPERMENSAGEM.CODEMPRESA";
        string query = string.Empty;


        public PSPartTipOperEdit()
        {
            InitializeComponent();

            new Class.Utilidades().getDicionario(this, tabControl1, "GTIPOPER");

            buttonOK.Location = new Point(847, 19);
            buttonSALVAR.Location = new Point(766, 19);
            buttonCANCELAR.Location = new Point(929, 19);

            #region Combos
            psLookup1.PSPart = "PSPartTipDoc";
            psLookup2.PSPart = "PSPartMoeda";
            psLookup8.PSPart = "PSPartTipOperSerie";
            psLookup4.PSPart = "PSPartConta";
            psLookup6.PSPart = "PSPartCliFor";
            psLookup7.PSPart = "PSPartCondicaoPgto";
            psLookup9.PSPart = "PSPartFormaPgto";
            psLookup11.PSPart = "PSPartOperador";
            psLookup10.PSPart = "PSPartConta";
            psLookup12.PSPart = "PSPartNatureza";
            psLookup15.PSPart = "PSPartFormula";
            psLookup16.PSPart = "PSPartFormula";
            psLookup5.PSPart = "PSPartFormula";
            psLookup3.PSPart = "PSPartFormula";
            psLookup13.PSPart = "PSPartFilial";
            psLookup20.PSPart = "PSPartFilial";
            psLookup14.PSPart = "PSPartLocalEstoque";
            psLookup21.PSPart = "PSPartLocalEstoque";
            psLookup17.PSPart = "PSPartNatureza";
            psLookup18.PSPart = "PSPartRepre";
            psLookup19.PSPart = "PSPartVendedor";
            psLookup22.PSPart = "PSPartFormula";
            psLookup23.PSPart = "PSPartOperMensagem";
            psLookup24.PSPart = "PSPartOperMensagem";


            psTextoBox4.Edita = false;

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = "N";
            list1[0].DisplayMember = "Nenhum";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = "A";
            list1[1].DisplayMember = "Aumenta";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = "D";
            list1[2].DisplayMember = "Diminui";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";

            #region AÇÃO ESTOQUE 2

            List<PS.Lib.ComboBoxItem> list83 = new List<PS.Lib.ComboBoxItem>();

            list83.Add(new PS.Lib.ComboBoxItem());
            list83[0].ValueMember = "N";
            list83[0].DisplayMember = "Nenhum";

            list83.Add(new PS.Lib.ComboBoxItem());
            list83[1].ValueMember = "A";
            list83[1].DisplayMember = "Aumenta";

            list83.Add(new PS.Lib.ComboBoxItem());
            list83[2].ValueMember = "D";
            list83[2].DisplayMember = "Diminui";

            psComboBox83.DataSource = list83;
            psComboBox83.DisplayMember = "DisplayMember";
            psComboBox83.ValueMember = "ValueMember";

            #endregion

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = "E";
            list2[0].DisplayMember = "Edita";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = "N";
            list2[1].DisplayMember = "Não Edita";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[2].ValueMember = "M";
            list2[2].DisplayMember = "Não Mostra";

            psComboBox2.DataSource = list2;
            psComboBox2.DisplayMember = "DisplayMember";
            psComboBox2.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list3 = new List<PS.Lib.ComboBoxItem>();

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[0].ValueMember = "E";
            list3[0].DisplayMember = "Edita";

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[1].ValueMember = "N";
            list3[1].DisplayMember = "Não Edita";

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[2].ValueMember = "M";
            list3[2].DisplayMember = "Não Mostra";

            psComboBox3.DataSource = list3;
            psComboBox3.DisplayMember = "DisplayMember";
            psComboBox3.ValueMember = "ValueMember";

            #region EDITA LOCAL ESTQOQUE 2 DESTINO

            List<PS.Lib.ComboBoxItem> listLOCAL2 = new List<PS.Lib.ComboBoxItem>();

            listLOCAL2.Add(new PS.Lib.ComboBoxItem());
            listLOCAL2[0].ValueMember = "E";
            listLOCAL2[0].DisplayMember = "Edita";

            listLOCAL2.Add(new PS.Lib.ComboBoxItem());
            listLOCAL2[1].ValueMember = "N";
            listLOCAL2[1].DisplayMember = "Não Edita";

            listLOCAL2.Add(new PS.Lib.ComboBoxItem());
            listLOCAL2[2].ValueMember = "M";
            listLOCAL2[2].DisplayMember = "Não Mostra";

            psComboBox4.DataSource = listLOCAL2;
            psComboBox4.DisplayMember = "DisplayMember";
            psComboBox4.ValueMember = "ValueMember";

            #endregion


            List<PS.Lib.ComboBoxItem> list4 = new List<PS.Lib.ComboBoxItem>();

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[0].ValueMember = "E";
            list4[0].DisplayMember = "Edita";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[1].ValueMember = "N";
            list4[1].DisplayMember = "Não Edita";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[2].ValueMember = "M";
            list4[2].DisplayMember = "Não Mostra";

            psComboBox4.DataSource = list4;
            psComboBox4.DisplayMember = "DisplayMember";
            psComboBox4.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list5 = new List<PS.Lib.ComboBoxItem>();

            list5.Add(new PS.Lib.ComboBoxItem());
            list5[0].ValueMember = "E";
            list5[0].DisplayMember = "Edita";

            list5.Add(new PS.Lib.ComboBoxItem());
            list5[1].ValueMember = "N";
            list5[1].DisplayMember = "Não Edita";

            list5.Add(new PS.Lib.ComboBoxItem());
            list5[2].ValueMember = "M";
            list5[2].DisplayMember = "Não Mostra";

            psComboBox5.DataSource = list5;
            psComboBox5.DisplayMember = "DisplayMember";
            psComboBox5.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list6 = new List<PS.Lib.ComboBoxItem>();

            list6.Add(new PS.Lib.ComboBoxItem());
            list6[0].ValueMember = "E";
            list6[0].DisplayMember = "Edita";

            list6.Add(new PS.Lib.ComboBoxItem());
            list6[1].ValueMember = "N";
            list6[1].DisplayMember = "Não Edita";

            list6.Add(new PS.Lib.ComboBoxItem());
            list6[2].ValueMember = "M";
            list6[2].DisplayMember = "Não Mostra";

            psComboBox6.DataSource = list6;
            psComboBox6.DisplayMember = "DisplayMember";
            psComboBox6.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list7 = new List<PS.Lib.ComboBoxItem>();

            list7.Add(new PS.Lib.ComboBoxItem());
            list7[0].ValueMember = "E";
            list7[0].DisplayMember = "Edita";

            list7.Add(new PS.Lib.ComboBoxItem());
            list7[1].ValueMember = "N";
            list7[1].DisplayMember = "Não Edita";

            list7.Add(new PS.Lib.ComboBoxItem());
            list7[2].ValueMember = "M";
            list7[2].DisplayMember = "Não Mostra";

            psComboBox7.DataSource = list7;
            psComboBox7.DisplayMember = "DisplayMember";
            psComboBox7.ValueMember = "ValueMember";

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

            List<PS.Lib.ComboBoxItem> list10 = new List<PS.Lib.ComboBoxItem>();

            list10.Add(new PS.Lib.ComboBoxItem());
            list10[0].ValueMember = "E";
            list10[0].DisplayMember = "Edita";

            list10.Add(new PS.Lib.ComboBoxItem());
            list10[1].ValueMember = "N";
            list10[1].DisplayMember = "Não Edita";

            list10.Add(new PS.Lib.ComboBoxItem());
            list10[2].ValueMember = "M";
            list10[2].DisplayMember = "Não Mostra";

            psComboBox10.DataSource = list10;
            psComboBox10.DisplayMember = "DisplayMember";
            psComboBox10.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list11 = new List<PS.Lib.ComboBoxItem>();

            list11.Add(new PS.Lib.ComboBoxItem());
            list11[0].ValueMember = "E";
            list11[0].DisplayMember = "Edita";

            list11.Add(new PS.Lib.ComboBoxItem());
            list11[1].ValueMember = "N";
            list11[1].DisplayMember = "Não Edita";

            list11.Add(new PS.Lib.ComboBoxItem());
            list11[2].ValueMember = "M";
            list11[2].DisplayMember = "Não Mostra";

            psComboBox11.DataSource = list11;
            psComboBox11.DisplayMember = "DisplayMember";
            psComboBox11.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list14 = new List<PS.Lib.ComboBoxItem>();

            list14.Add(new PS.Lib.ComboBoxItem());
            list14[0].ValueMember = "E";
            list14[0].DisplayMember = "Edita";

            list14.Add(new PS.Lib.ComboBoxItem());
            list14[1].ValueMember = "N";
            list14[1].DisplayMember = "Não Edita";

            list14.Add(new PS.Lib.ComboBoxItem());
            list14[2].ValueMember = "M";
            list14[2].DisplayMember = "Não Mostra";

            psComboBox14.DataSource = list14;
            psComboBox14.DisplayMember = "DisplayMember";
            psComboBox14.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list15 = new List<PS.Lib.ComboBoxItem>();

            list15.Add(new PS.Lib.ComboBoxItem());
            list15[0].ValueMember = "E";
            list15[0].DisplayMember = "Edita";

            list15.Add(new PS.Lib.ComboBoxItem());
            list15[1].ValueMember = "N";
            list15[1].DisplayMember = "Não Edita";

            list15.Add(new PS.Lib.ComboBoxItem());
            list15[2].ValueMember = "M";
            list15[2].DisplayMember = "Não Mostra";

            psComboBox15.DataSource = list15;
            psComboBox15.DisplayMember = "DisplayMember";
            psComboBox15.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list16 = new List<PS.Lib.ComboBoxItem>();

            list16.Add(new PS.Lib.ComboBoxItem());
            list16[0].ValueMember = "0";
            list16[0].DisplayMember = "Não";

            list16.Add(new PS.Lib.ComboBoxItem());
            list16[1].ValueMember = "1";
            list16[1].DisplayMember = "Sim";

            psComboBox16.DataSource = list16;
            psComboBox16.DisplayMember = "DisplayMember";
            psComboBox16.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list17 = new List<PS.Lib.ComboBoxItem>();

            list17.Add(new PS.Lib.ComboBoxItem());
            list17[0].ValueMember = "E";
            list17[0].DisplayMember = "Edita";

            list17.Add(new PS.Lib.ComboBoxItem());
            list17[1].ValueMember = "N";
            list17[1].DisplayMember = "Não Edita";

            list17.Add(new PS.Lib.ComboBoxItem());
            list17[2].ValueMember = "M";
            list17[2].DisplayMember = "Não Mostra";

            psComboBox17.DataSource = list17;
            psComboBox17.DisplayMember = "DisplayMember";
            psComboBox17.ValueMember = "ValueMember";


            List<PS.Lib.ComboBoxItem> list18 = new List<PS.Lib.ComboBoxItem>();

            list18.Add(new PS.Lib.ComboBoxItem());
            list18[0].ValueMember = 0;
            list18[0].DisplayMember = "Pagar";

            list18.Add(new PS.Lib.ComboBoxItem());
            list18[1].ValueMember = 1;
            list18[1].DisplayMember = "Receber";

            psComboBox18.DataSource = list18;
            psComboBox18.DisplayMember = "DisplayMember";
            psComboBox18.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list19 = new List<PS.Lib.ComboBoxItem>();

            list19.Add(new PS.Lib.ComboBoxItem());
            list19[0].ValueMember = 0;
            list19[0].DisplayMember = "Cliente";

            list19.Add(new PS.Lib.ComboBoxItem());
            list19[1].ValueMember = 1;
            list19[1].DisplayMember = "Fornecedor";

            list19.Add(new PS.Lib.ComboBoxItem());
            list19[2].ValueMember = 2;
            list19[2].DisplayMember = "Ambos";

            psComboBox19.DataSource = list19;
            psComboBox19.DisplayMember = "DisplayMember";
            psComboBox19.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list20 = new List<PS.Lib.ComboBoxItem>();

            list20.Add(new PS.Lib.ComboBoxItem());
            list20[0].ValueMember = 0;
            list20[0].DisplayMember = "Ambos";

            list20.Add(new PS.Lib.ComboBoxItem());
            list20[1].ValueMember = 1;
            list20[1].DisplayMember = "Produto";

            list20.Add(new PS.Lib.ComboBoxItem());
            list20[2].ValueMember = 2;
            list20[2].DisplayMember = "Serviço";

            psComboBox20.DataSource = list20;
            psComboBox20.DisplayMember = "DisplayMember";
            psComboBox20.ValueMember = "ValueMember";





            List<PS.Lib.ComboBoxItem> list21 = new List<PS.Lib.ComboBoxItem>();

            list21.Add(new PS.Lib.ComboBoxItem());
            list21[0].ValueMember = "E";
            list21[0].DisplayMember = "Edita";

            list21.Add(new PS.Lib.ComboBoxItem());
            list21[1].ValueMember = "N";
            list21[1].DisplayMember = "Não Edita";

            list21.Add(new PS.Lib.ComboBoxItem());
            list21[2].ValueMember = "M";
            list21[2].DisplayMember = "Não Mostra";

            psComboBox21.DataSource = list21;
            psComboBox21.DisplayMember = "DisplayMember";
            psComboBox21.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list22 = new List<PS.Lib.ComboBoxItem>();

            list22.Add(new PS.Lib.ComboBoxItem());
            list22[0].ValueMember = "E";
            list22[0].DisplayMember = "Edita";

            list22.Add(new PS.Lib.ComboBoxItem());
            list22[1].ValueMember = "N";
            list22[1].DisplayMember = "Não Edita";

            list22.Add(new PS.Lib.ComboBoxItem());
            list22[2].ValueMember = "M";
            list22[2].DisplayMember = "Não Mostra";

            psComboBox22.DataSource = list22;
            psComboBox22.DisplayMember = "DisplayMember";
            psComboBox22.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list23 = new List<PS.Lib.ComboBoxItem>();

            list23.Add(new PS.Lib.ComboBoxItem());
            list23[0].ValueMember = "E";
            list23[0].DisplayMember = "Edita";

            list23.Add(new PS.Lib.ComboBoxItem());
            list23[1].ValueMember = "N";
            list23[1].DisplayMember = "Não Edita";

            list23.Add(new PS.Lib.ComboBoxItem());
            list23[2].ValueMember = "M";
            list23[2].DisplayMember = "Não Mostra";

            psComboBox23.DataSource = list23;
            psComboBox23.DisplayMember = "DisplayMember";
            psComboBox23.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list26 = new List<PS.Lib.ComboBoxItem>();

            list26.Add(new PS.Lib.ComboBoxItem());
            list26[0].ValueMember = "E";
            list26[0].DisplayMember = "Edita";

            list26.Add(new PS.Lib.ComboBoxItem());
            list26[1].ValueMember = "N";
            list26[1].DisplayMember = "Não Edita";

            list26.Add(new PS.Lib.ComboBoxItem());
            list26[2].ValueMember = "M";
            list26[2].DisplayMember = "Não Mostra";

            psComboBox26.DataSource = list26;
            psComboBox26.DisplayMember = "DisplayMember";
            psComboBox26.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list27 = new List<PS.Lib.ComboBoxItem>();

            list27.Add(new PS.Lib.ComboBoxItem());
            list27[0].ValueMember = "E";
            list27[0].DisplayMember = "Edita";

            list27.Add(new PS.Lib.ComboBoxItem());
            list27[1].ValueMember = "N";
            list27[1].DisplayMember = "Não Edita";

            list27.Add(new PS.Lib.ComboBoxItem());
            list27[2].ValueMember = "M";
            list27[2].DisplayMember = "Não Mostra";

            psComboBox27.DataSource = list27;
            psComboBox27.DisplayMember = "DisplayMember";
            psComboBox27.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list28 = new List<PS.Lib.ComboBoxItem>();

            list28.Add(new PS.Lib.ComboBoxItem());
            list28[0].ValueMember = "E";
            list28[0].DisplayMember = "Edita";

            list28.Add(new PS.Lib.ComboBoxItem());
            list28[1].ValueMember = "N";
            list28[1].DisplayMember = "Não Edita";

            list28.Add(new PS.Lib.ComboBoxItem());
            list28[2].ValueMember = "M";
            list28[2].DisplayMember = "Não Mostra";

            psComboBox28.DataSource = list28;
            psComboBox28.DisplayMember = "DisplayMember";
            psComboBox28.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list29 = new List<PS.Lib.ComboBoxItem>();

            list29.Add(new PS.Lib.ComboBoxItem());
            list29[0].ValueMember = "E";
            list29[0].DisplayMember = "Edita";

            list29.Add(new PS.Lib.ComboBoxItem());
            list29[1].ValueMember = "N";
            list29[1].DisplayMember = "Não Edita";

            list29.Add(new PS.Lib.ComboBoxItem());
            list29[2].ValueMember = "M";
            list29[2].DisplayMember = "Não Mostra";

            psComboBox29.DataSource = list29;
            psComboBox29.DisplayMember = "DisplayMember";
            psComboBox29.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list30 = new List<PS.Lib.ComboBoxItem>();

            list30.Add(new PS.Lib.ComboBoxItem());
            list30[0].ValueMember = "E";
            list30[0].DisplayMember = "Edita";

            list30.Add(new PS.Lib.ComboBoxItem());
            list30[1].ValueMember = "N";
            list30[1].DisplayMember = "Não Edita";

            list30.Add(new PS.Lib.ComboBoxItem());
            list30[2].ValueMember = "M";
            list30[2].DisplayMember = "Não Mostra";

            psComboBox30.DataSource = list30;
            psComboBox30.DisplayMember = "DisplayMember";
            psComboBox30.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list31 = new List<PS.Lib.ComboBoxItem>();

            list31.Add(new PS.Lib.ComboBoxItem());
            list31[0].ValueMember = "E";
            list31[0].DisplayMember = "Edita";

            list31.Add(new PS.Lib.ComboBoxItem());
            list31[1].ValueMember = "N";
            list31[1].DisplayMember = "Não Edita";

            list31.Add(new PS.Lib.ComboBoxItem());
            list31[2].ValueMember = "M";
            list31[2].DisplayMember = "Não Mostra";

            psComboBox31.DataSource = list31;
            psComboBox31.DisplayMember = "DisplayMember";
            psComboBox31.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list33 = new List<PS.Lib.ComboBoxItem>();

            list33.Add(new PS.Lib.ComboBoxItem());
            list33[0].ValueMember = "E";
            list33[0].DisplayMember = "Edita";

            list33.Add(new PS.Lib.ComboBoxItem());
            list33[1].ValueMember = "N";
            list33[1].DisplayMember = "Não Edita";

            list33.Add(new PS.Lib.ComboBoxItem());
            list33[2].ValueMember = "M";
            list33[2].DisplayMember = "Não Mostra";

            psComboBox33.DataSource = list33;
            psComboBox33.DisplayMember = "DisplayMember";
            psComboBox33.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list34 = new List<PS.Lib.ComboBoxItem>();

            list34.Add(new PS.Lib.ComboBoxItem());
            list34[0].ValueMember = "E";
            list34[0].DisplayMember = "Edita";

            list34.Add(new PS.Lib.ComboBoxItem());
            list34[1].ValueMember = "N";
            list34[1].DisplayMember = "Não Edita";

            list34.Add(new PS.Lib.ComboBoxItem());
            list34[2].ValueMember = "M";
            list34[2].DisplayMember = "Não Mostra";

            psComboBox34.DataSource = list34;
            psComboBox34.DisplayMember = "DisplayMember";
            psComboBox34.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list35 = new List<PS.Lib.ComboBoxItem>();

            list35.Add(new PS.Lib.ComboBoxItem());
            list35[0].ValueMember = "E";
            list35[0].DisplayMember = "Edita";

            list35.Add(new PS.Lib.ComboBoxItem());
            list35[1].ValueMember = "N";
            list35[1].DisplayMember = "Não Edita";

            list35.Add(new PS.Lib.ComboBoxItem());
            list35[2].ValueMember = "M";
            list35[2].DisplayMember = "Não Mostra";

            psComboBox35.DataSource = list35;
            psComboBox35.DisplayMember = "DisplayMember";
            psComboBox35.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list36 = new List<PS.Lib.ComboBoxItem>();

            list36.Add(new PS.Lib.ComboBoxItem());
            list36[0].ValueMember = "E";
            list36[0].DisplayMember = "Edita";

            list36.Add(new PS.Lib.ComboBoxItem());
            list36[1].ValueMember = "N";
            list36[1].DisplayMember = "Não Edita";

            list36.Add(new PS.Lib.ComboBoxItem());
            list36[2].ValueMember = "M";
            list36[2].DisplayMember = "Não Mostra";

            psComboBox36.DataSource = list36;
            psComboBox36.DisplayMember = "DisplayMember";
            psComboBox36.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list37 = new List<PS.Lib.ComboBoxItem>();

            list37.Add(new PS.Lib.ComboBoxItem());
            list37[0].ValueMember = "E";
            list37[0].DisplayMember = "Edita";

            list37.Add(new PS.Lib.ComboBoxItem());
            list37[1].ValueMember = "N";
            list37[1].DisplayMember = "Não Edita";

            list37.Add(new PS.Lib.ComboBoxItem());
            list37[2].ValueMember = "M";
            list37[2].DisplayMember = "Não Mostra";

            psComboBox37.DataSource = list37;
            psComboBox37.DisplayMember = "DisplayMember";
            psComboBox37.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list38 = new List<PS.Lib.ComboBoxItem>();

            list38.Add(new PS.Lib.ComboBoxItem());
            list38[0].ValueMember = "E";
            list38[0].DisplayMember = "Edita";

            list38.Add(new PS.Lib.ComboBoxItem());
            list38[1].ValueMember = "N";
            list38[1].DisplayMember = "Não Edita";

            list38.Add(new PS.Lib.ComboBoxItem());
            list38[2].ValueMember = "M";
            list38[2].DisplayMember = "Não Mostra";

            psComboBox38.DataSource = list38;
            psComboBox38.DisplayMember = "DisplayMember";
            psComboBox38.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list39 = new List<PS.Lib.ComboBoxItem>();

            list39.Add(new PS.Lib.ComboBoxItem());
            list39[0].ValueMember = "E";
            list39[0].DisplayMember = "Edita";

            list39.Add(new PS.Lib.ComboBoxItem());
            list39[1].ValueMember = "N";
            list39[1].DisplayMember = "Não Edita";

            list39.Add(new PS.Lib.ComboBoxItem());
            list39[2].ValueMember = "M";
            list39[2].DisplayMember = "Não Mostra";

            psComboBox39.DataSource = list39;
            psComboBox39.DisplayMember = "DisplayMember";
            psComboBox39.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list40 = new List<PS.Lib.ComboBoxItem>();

            list40.Add(new PS.Lib.ComboBoxItem());
            list40[0].ValueMember = "E";
            list40[0].DisplayMember = "Edita";

            list40.Add(new PS.Lib.ComboBoxItem());
            list40[1].ValueMember = "N";
            list40[1].DisplayMember = "Não Edita";

            list40.Add(new PS.Lib.ComboBoxItem());
            list40[2].ValueMember = "M";
            list40[2].DisplayMember = "Não Mostra";

            psComboBox40.DataSource = list40;
            psComboBox40.DisplayMember = "DisplayMember";
            psComboBox40.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list41 = new List<PS.Lib.ComboBoxItem>();

            list41.Add(new PS.Lib.ComboBoxItem());
            list41[0].ValueMember = "E";
            list41[0].DisplayMember = "Edita";

            list41.Add(new PS.Lib.ComboBoxItem());
            list41[1].ValueMember = "N";
            list41[1].DisplayMember = "Não Edita";

            list41.Add(new PS.Lib.ComboBoxItem());
            list41[2].ValueMember = "M";
            list41[2].DisplayMember = "Não Mostra";

            psComboBox41.DataSource = list41;
            psComboBox41.DisplayMember = "DisplayMember";
            psComboBox41.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list42 = new List<PS.Lib.ComboBoxItem>();

            list42.Add(new PS.Lib.ComboBoxItem());
            list42[0].ValueMember = "E";
            list42[0].DisplayMember = "Edita";

            list42.Add(new PS.Lib.ComboBoxItem());
            list42[1].ValueMember = "N";
            list42[1].DisplayMember = "Não Edita";

            list42.Add(new PS.Lib.ComboBoxItem());
            list42[2].ValueMember = "M";
            list42[2].DisplayMember = "Não Mostra";

            psComboBox42.DataSource = list42;
            psComboBox42.DisplayMember = "DisplayMember";
            psComboBox42.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list43 = new List<PS.Lib.ComboBoxItem>();

            list43.Add(new PS.Lib.ComboBoxItem());
            list43[0].ValueMember = "E";
            list43[0].DisplayMember = "Edita";

            list43.Add(new PS.Lib.ComboBoxItem());
            list43[1].ValueMember = "N";
            list43[1].DisplayMember = "Não Edita";

            list43.Add(new PS.Lib.ComboBoxItem());
            list43[2].ValueMember = "M";
            list43[2].DisplayMember = "Não Mostra";

            psComboBox43.DataSource = list43;
            psComboBox43.DisplayMember = "DisplayMember";
            psComboBox43.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list44 = new List<PS.Lib.ComboBoxItem>();

            list44.Add(new PS.Lib.ComboBoxItem());
            list44[0].ValueMember = "E";
            list44[0].DisplayMember = "Edita";

            list44.Add(new PS.Lib.ComboBoxItem());
            list44[1].ValueMember = "N";
            list44[1].DisplayMember = "Não Edita";

            list44.Add(new PS.Lib.ComboBoxItem());
            list44[2].ValueMember = "M";
            list44[2].DisplayMember = "Não Mostra";

            psComboBox44.DataSource = list44;
            psComboBox44.DisplayMember = "DisplayMember";
            psComboBox44.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list45 = new List<PS.Lib.ComboBoxItem>();

            list45.Add(new PS.Lib.ComboBoxItem());
            list45[0].ValueMember = "E";
            list45[0].DisplayMember = "Edita";

            list45.Add(new PS.Lib.ComboBoxItem());
            list45[1].ValueMember = "N";
            list45[1].DisplayMember = "Não Edita";

            list45.Add(new PS.Lib.ComboBoxItem());
            list45[2].ValueMember = "M";
            list45[2].DisplayMember = "Não Mostra";

            psComboBox45.DataSource = list45;
            psComboBox45.DisplayMember = "DisplayMember";
            psComboBox45.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list46 = new List<PS.Lib.ComboBoxItem>();

            list46.Add(new PS.Lib.ComboBoxItem());
            list46[0].ValueMember = "E";
            list46[0].DisplayMember = "Edita";

            list46.Add(new PS.Lib.ComboBoxItem());
            list46[1].ValueMember = "N";
            list46[1].DisplayMember = "Não Edita";

            list46.Add(new PS.Lib.ComboBoxItem());
            list46[2].ValueMember = "M";
            list46[2].DisplayMember = "Não Mostra";

            psComboBox46.DataSource = list46;
            psComboBox46.DisplayMember = "DisplayMember";
            psComboBox46.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list47 = new List<PS.Lib.ComboBoxItem>();

            list47.Add(new PS.Lib.ComboBoxItem());
            list47[0].ValueMember = "E";
            list47[0].DisplayMember = "Edita";

            list47.Add(new PS.Lib.ComboBoxItem());
            list47[1].ValueMember = "N";
            list47[1].DisplayMember = "Não Edita";

            list47.Add(new PS.Lib.ComboBoxItem());
            list47[2].ValueMember = "M";
            list47[2].DisplayMember = "Não Mostra";

            psComboBox47.DataSource = list47;
            psComboBox47.DisplayMember = "DisplayMember";
            psComboBox47.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list48 = new List<PS.Lib.ComboBoxItem>();

            list48.Add(new PS.Lib.ComboBoxItem());
            list48[0].ValueMember = "E";
            list48[0].DisplayMember = "Edita";

            list48.Add(new PS.Lib.ComboBoxItem());
            list48[1].ValueMember = "N";
            list48[1].DisplayMember = "Não Edita";

            list48.Add(new PS.Lib.ComboBoxItem());
            list48[2].ValueMember = "M";
            list48[2].DisplayMember = "Não Mostra";

            psComboBox48.DataSource = list48;
            psComboBox48.DisplayMember = "DisplayMember";
            psComboBox48.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list49 = new List<PS.Lib.ComboBoxItem>();

            list49.Add(new PS.Lib.ComboBoxItem());
            list49[0].ValueMember = "E";
            list49[0].DisplayMember = "Edita";

            list49.Add(new PS.Lib.ComboBoxItem());
            list49[1].ValueMember = "N";
            list49[1].DisplayMember = "Não Edita";

            list49.Add(new PS.Lib.ComboBoxItem());
            list49[2].ValueMember = "M";
            list49[2].DisplayMember = "Não Mostra";

            psComboBox49.DataSource = list49;
            psComboBox49.DisplayMember = "DisplayMember";
            psComboBox49.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list50 = new List<PS.Lib.ComboBoxItem>();

            list50.Add(new PS.Lib.ComboBoxItem());
            list50[0].ValueMember = "E";
            list50[0].DisplayMember = "Edita";

            list50.Add(new PS.Lib.ComboBoxItem());
            list50[1].ValueMember = "N";
            list50[1].DisplayMember = "Não Edita";

            list50.Add(new PS.Lib.ComboBoxItem());
            list50[2].ValueMember = "M";
            list50[2].DisplayMember = "Não Mostra";

            psComboBox50.DataSource = list50;
            psComboBox50.DisplayMember = "DisplayMember";
            psComboBox50.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list51 = new List<PS.Lib.ComboBoxItem>();

            list51.Add(new PS.Lib.ComboBoxItem());
            list51[0].ValueMember = "E";
            list51[0].DisplayMember = "Edita";

            list51.Add(new PS.Lib.ComboBoxItem());
            list51[1].ValueMember = "N";
            list51[1].DisplayMember = "Não Edita";

            list51.Add(new PS.Lib.ComboBoxItem());
            list51[2].ValueMember = "M";
            list51[2].DisplayMember = "Não Mostra";

            psComboBox51.DataSource = list51;
            psComboBox51.DisplayMember = "DisplayMember";
            psComboBox51.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list52 = new List<PS.Lib.ComboBoxItem>();

            list52.Add(new PS.Lib.ComboBoxItem());
            list52[0].ValueMember = "E";
            list52[0].DisplayMember = "Edita";

            list52.Add(new PS.Lib.ComboBoxItem());
            list52[1].ValueMember = "N";
            list52[1].DisplayMember = "Não Edita";

            list52.Add(new PS.Lib.ComboBoxItem());
            list52[2].ValueMember = "M";
            list52[2].DisplayMember = "Não Mostra";

            psComboBox52.DataSource = list52;
            psComboBox52.DisplayMember = "DisplayMember";
            psComboBox52.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list53 = new List<PS.Lib.ComboBoxItem>();

            list53.Add(new PS.Lib.ComboBoxItem());
            list53[0].ValueMember = "E";
            list53[0].DisplayMember = "Edita";

            list53.Add(new PS.Lib.ComboBoxItem());
            list53[1].ValueMember = "N";
            list53[1].DisplayMember = "Não Edita";

            list53.Add(new PS.Lib.ComboBoxItem());
            list53[2].ValueMember = "M";
            list53[2].DisplayMember = "Não Mostra";

            psComboBox53.DataSource = list53;
            psComboBox53.DisplayMember = "DisplayMember";
            psComboBox53.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list54 = new List<PS.Lib.ComboBoxItem>();

            list54.Add(new PS.Lib.ComboBoxItem());
            list54[0].ValueMember = "E";
            list54[0].DisplayMember = "Edita";

            list54.Add(new PS.Lib.ComboBoxItem());
            list54[1].ValueMember = "N";
            list54[1].DisplayMember = "Não Edita";

            list54.Add(new PS.Lib.ComboBoxItem());
            list54[2].ValueMember = "M";
            list54[2].DisplayMember = "Não Mostra";

            psComboBox54.DataSource = list54;
            psComboBox54.DisplayMember = "DisplayMember";
            psComboBox54.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list55 = new List<PS.Lib.ComboBoxItem>();

            list55.Add(new PS.Lib.ComboBoxItem());
            list55[0].ValueMember = "E";
            list55[0].DisplayMember = "Edita";

            list55.Add(new PS.Lib.ComboBoxItem());
            list55[1].ValueMember = "N";
            list55[1].DisplayMember = "Não Edita";

            list55.Add(new PS.Lib.ComboBoxItem());
            list55[2].ValueMember = "M";
            list55[2].DisplayMember = "Não Mostra";

            psComboBox55.DataSource = list55;
            psComboBox55.DisplayMember = "DisplayMember";
            psComboBox55.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list56 = new List<PS.Lib.ComboBoxItem>();

            list56.Add(new PS.Lib.ComboBoxItem());
            list56[0].ValueMember = "E";
            list56[0].DisplayMember = "Edita";

            list56.Add(new PS.Lib.ComboBoxItem());
            list56[1].ValueMember = "N";
            list56[1].DisplayMember = "Não Edita";

            list56.Add(new PS.Lib.ComboBoxItem());
            list56[2].ValueMember = "M";
            list56[2].DisplayMember = "Não Mostra";

            psComboBox56.DataSource = list56;
            psComboBox56.DisplayMember = "DisplayMember";
            psComboBox56.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list57 = new List<PS.Lib.ComboBoxItem>();

            list57.Add(new PS.Lib.ComboBoxItem());
            list57[0].ValueMember = "E";
            list57[0].DisplayMember = "Edita";

            list57.Add(new PS.Lib.ComboBoxItem());
            list57[1].ValueMember = "N";
            list57[1].DisplayMember = "Não Edita";

            list57.Add(new PS.Lib.ComboBoxItem());
            list57[2].ValueMember = "M";
            list57[2].DisplayMember = "Não Mostra";

            psComboBox57.DataSource = list57;
            psComboBox57.DisplayMember = "DisplayMember";
            psComboBox57.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list58 = new List<PS.Lib.ComboBoxItem>();

            list58.Add(new PS.Lib.ComboBoxItem());
            list58[0].ValueMember = "E";
            list58[0].DisplayMember = "Edita";

            list58.Add(new PS.Lib.ComboBoxItem());
            list58[1].ValueMember = "N";
            list58[1].DisplayMember = "Não Edita";

            list58.Add(new PS.Lib.ComboBoxItem());
            list58[2].ValueMember = "M";
            list58[2].DisplayMember = "Não Mostra";

            psComboBox58.DataSource = list58;
            psComboBox58.DisplayMember = "DisplayMember";
            psComboBox58.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list59 = new List<PS.Lib.ComboBoxItem>();

            list59.Add(new PS.Lib.ComboBoxItem());
            list59[0].ValueMember = "E";
            list59[0].DisplayMember = "Edita";

            list59.Add(new PS.Lib.ComboBoxItem());
            list59[1].ValueMember = "N";
            list59[1].DisplayMember = "Não Edita";

            list59.Add(new PS.Lib.ComboBoxItem());
            list59[2].ValueMember = "M";
            list59[2].DisplayMember = "Não Mostra";

            psComboBox59.DataSource = list59;
            psComboBox59.DisplayMember = "DisplayMember";
            psComboBox59.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list60 = new List<PS.Lib.ComboBoxItem>();

            list60.Add(new PS.Lib.ComboBoxItem());
            list60[0].ValueMember = "E";
            list60[0].DisplayMember = "Edita";

            list60.Add(new PS.Lib.ComboBoxItem());
            list60[1].ValueMember = "N";
            list60[1].DisplayMember = "Não Edita";

            list60.Add(new PS.Lib.ComboBoxItem());
            list60[2].ValueMember = "M";
            list60[2].DisplayMember = "Não Mostra";

            psComboBox60.DataSource = list60;
            psComboBox60.DisplayMember = "DisplayMember";
            psComboBox60.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list61 = new List<PS.Lib.ComboBoxItem>();

            list61.Add(new PS.Lib.ComboBoxItem());
            list61[0].ValueMember = "E";
            list61[0].DisplayMember = "Edita";

            list61.Add(new PS.Lib.ComboBoxItem());
            list61[1].ValueMember = "N";
            list61[1].DisplayMember = "Não Edita";

            list61.Add(new PS.Lib.ComboBoxItem());
            list61[2].ValueMember = "M";
            list61[2].DisplayMember = "Não Mostra";

            psComboBox61.DataSource = list61;
            psComboBox61.DisplayMember = "DisplayMember";
            psComboBox61.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list62 = new List<PS.Lib.ComboBoxItem>();

            list62.Add(new PS.Lib.ComboBoxItem());
            list62[0].ValueMember = "E";
            list62[0].DisplayMember = "Edita";

            list62.Add(new PS.Lib.ComboBoxItem());
            list62[1].ValueMember = "N";
            list62[1].DisplayMember = "Não Edita";

            list62.Add(new PS.Lib.ComboBoxItem());
            list62[2].ValueMember = "M";
            list62[2].DisplayMember = "Não Mostra";

            psComboBox62.DataSource = list62;
            psComboBox62.DisplayMember = "DisplayMember";
            psComboBox62.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list63 = new List<PS.Lib.ComboBoxItem>();

            list63.Add(new PS.Lib.ComboBoxItem());
            list63[0].ValueMember = "E";
            list63[0].DisplayMember = "Edita";

            list63.Add(new PS.Lib.ComboBoxItem());
            list63[1].ValueMember = "N";
            list63[1].DisplayMember = "Não Edita";

            list63.Add(new PS.Lib.ComboBoxItem());
            list63[2].ValueMember = "M";
            list63[2].DisplayMember = "Não Mostra";

            psComboBox63.DataSource = list63;
            psComboBox63.DisplayMember = "DisplayMember";
            psComboBox63.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list64 = new List<PS.Lib.ComboBoxItem>();

            list64.Add(new PS.Lib.ComboBoxItem());
            list64[0].ValueMember = "E";
            list64[0].DisplayMember = "Edita";

            list64.Add(new PS.Lib.ComboBoxItem());
            list64[1].ValueMember = "N";
            list64[1].DisplayMember = "Não Edita";

            list64.Add(new PS.Lib.ComboBoxItem());
            list64[2].ValueMember = "M";
            list64[2].DisplayMember = "Não Mostra";

            psComboBox64.DataSource = list64;
            psComboBox64.DisplayMember = "DisplayMember";
            psComboBox64.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list65 = new List<PS.Lib.ComboBoxItem>();

            list65.Add(new PS.Lib.ComboBoxItem());
            list65[0].ValueMember = "E";
            list65[0].DisplayMember = "Edita";

            list65.Add(new PS.Lib.ComboBoxItem());
            list65[1].ValueMember = "N";
            list65[1].DisplayMember = "Não Edita";

            list65.Add(new PS.Lib.ComboBoxItem());
            list65[2].ValueMember = "M";
            list65[2].DisplayMember = "Não Mostra";

            psComboBox65.DataSource = list65;
            psComboBox65.DisplayMember = "DisplayMember";
            psComboBox65.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list66 = new List<PS.Lib.ComboBoxItem>();

            list66.Add(new PS.Lib.ComboBoxItem());
            list66[0].ValueMember = "E";
            list66[0].DisplayMember = "Edita";

            list66.Add(new PS.Lib.ComboBoxItem());
            list66[1].ValueMember = "N";
            list66[1].DisplayMember = "Não Edita";

            list66.Add(new PS.Lib.ComboBoxItem());
            list66[2].ValueMember = "M";
            list66[2].DisplayMember = "Não Mostra";

            psComboBox66.DataSource = list66;
            psComboBox66.DisplayMember = "DisplayMember";
            psComboBox66.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list67 = new List<PS.Lib.ComboBoxItem>();

            list67.Add(new PS.Lib.ComboBoxItem());
            list67[0].ValueMember = "E";
            list67[0].DisplayMember = "Edita";

            list67.Add(new PS.Lib.ComboBoxItem());
            list67[1].ValueMember = "N";
            list67[1].DisplayMember = "Não Edita";

            list67.Add(new PS.Lib.ComboBoxItem());
            list67[2].ValueMember = "M";
            list67[2].DisplayMember = "Não Mostra";

            psComboBox67.DataSource = list67;
            psComboBox67.DisplayMember = "DisplayMember";
            psComboBox67.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list68 = new List<PS.Lib.ComboBoxItem>();

            list68.Add(new PS.Lib.ComboBoxItem());
            list68[0].ValueMember = "E";
            list68[0].DisplayMember = "Edita";

            list68.Add(new PS.Lib.ComboBoxItem());
            list68[1].ValueMember = "N";
            list68[1].DisplayMember = "Não Edita";

            list68.Add(new PS.Lib.ComboBoxItem());
            list68[2].ValueMember = "M";
            list68[2].DisplayMember = "Não Mostra";

            psComboBox68.DataSource = list68;
            psComboBox68.DisplayMember = "DisplayMember";
            psComboBox68.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list69 = new List<PS.Lib.ComboBoxItem>();

            list69.Add(new PS.Lib.ComboBoxItem());
            list69[0].ValueMember = "NAOUSA";
            list69[0].DisplayMember = "Não Usa";

            list69.Add(new PS.Lib.ComboBoxItem());
            list69[1].ValueMember = "NENHUM";
            list69[1].DisplayMember = "Nenhum";

            list69.Add(new PS.Lib.ComboBoxItem());
            list69[2].ValueMember = "PRECO1";
            list69[2].DisplayMember = "Preço 1";

            list69.Add(new PS.Lib.ComboBoxItem());
            list69[3].ValueMember = "PRECO2";
            list69[3].DisplayMember = "Preço 2";

            list69.Add(new PS.Lib.ComboBoxItem());
            list69[4].ValueMember = "PRECO3";
            list69[4].DisplayMember = "Preço 3";

            list69.Add(new PS.Lib.ComboBoxItem());
            list69[5].ValueMember = "PRECO4";
            list69[5].DisplayMember = "Preço 4";

            list69.Add(new PS.Lib.ComboBoxItem());
            list69[6].ValueMember = "PRECO5";
            list69[6].DisplayMember = "Preço 5";

            list69.Add(new PS.Lib.ComboBoxItem());
            list69[7].ValueMember = "DEFCLI";
            list69[7].DisplayMember = "Default do Cliente";

            list69.Add(new PS.Lib.ComboBoxItem());
            list69[8].ValueMember = "TABCLI";
            list69[8].DisplayMember = "Tabela de Preço por Cliente";

            list69.Add(new PS.Lib.ComboBoxItem());
            list69[9].ValueMember = "CMEDIO";
            list69[9].DisplayMember = "Custo Médio";

            psComboBox69.DataSource = list69;
            psComboBox69.DisplayMember = "DisplayMember";
            psComboBox69.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list70 = new List<PS.Lib.ComboBoxItem>();

            list70.Add(new PS.Lib.ComboBoxItem());
            list70[0].ValueMember = 0;
            list70[0].DisplayMember = "Diversas";

            list70.Add(new PS.Lib.ComboBoxItem());
            list70[1].ValueMember = 1;
            list70[1].DisplayMember = "Estoque";

            list70.Add(new PS.Lib.ComboBoxItem());
            list70[2].ValueMember = 2;
            list70[2].DisplayMember = "Compras";

            list70.Add(new PS.Lib.ComboBoxItem());
            list70[3].ValueMember = 3;
            list70[3].DisplayMember = "Vendas";

            psComboBox70.DataSource = list70;
            psComboBox70.DisplayMember = "DisplayMember";
            psComboBox70.ValueMember = "ValueMember";


            psComboBox76.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GGRUPOREL WHERE ATIVO = ?", new object[] { 1 });
            psComboBox76.DisplayMember = "GRUPO";
            psComboBox76.ValueMember = "IDGRUPOREL";

            List<PS.Lib.ComboBoxItem> list71 = new List<PS.Lib.ComboBoxItem>();

            list71.Add(new PS.Lib.ComboBoxItem());
            list71[0].ValueMember = 0;
            list71[0].DisplayMember = "Unidade de Controle";

            list71.Add(new PS.Lib.ComboBoxItem());
            list71[1].ValueMember = 1;
            list71[1].DisplayMember = "Unidade de Compra";

            list71.Add(new PS.Lib.ComboBoxItem());
            list71[2].ValueMember = 2;
            list71[2].DisplayMember = "Unidade de Venda";

            psComboBox71.DataSource = list71;
            psComboBox71.DisplayMember = "DisplayMember";
            psComboBox71.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list9 = new List<PS.Lib.ComboBoxItem>();

            list9.Add(new PS.Lib.ComboBoxItem());
            list9[0].ValueMember = 0;
            list9[0].DisplayMember = "Sem geração de DANFE";

            list9.Add(new PS.Lib.ComboBoxItem());
            list9[1].ValueMember = 1;
            list9[1].DisplayMember = "DANFE normal, Retrato";

            list9.Add(new PS.Lib.ComboBoxItem());
            list9[2].ValueMember = 2;
            list9[2].DisplayMember = "DANFE normal, Paisagem";

            psComboBox9.DataSource = list9;
            psComboBox9.DisplayMember = "DisplayMember";
            psComboBox9.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list72 = new List<PS.Lib.ComboBoxItem>();

            list72.Add(new PS.Lib.ComboBoxItem());
            list72[0].ValueMember = "55";
            list72[0].DisplayMember = "NF-e";

            list72.Add(new PS.Lib.ComboBoxItem());
            list72[1].ValueMember = "65";
            list72[1].DisplayMember = "NFC-e";

            psComboBox12.DataSource = list72;
            psComboBox12.DisplayMember = "DisplayMember";
            psComboBox12.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list73 = new List<PS.Lib.ComboBoxItem>();

            list73.Add(new PS.Lib.ComboBoxItem());
            list73[0].ValueMember = "NOME";
            list73[0].DisplayMember = "Nome";

            list73.Add(new PS.Lib.ComboBoxItem());
            list73[1].ValueMember = "NOMEFANTASIA";
            list73[1].DisplayMember = "Nome Fantasia";

            list73.Add(new PS.Lib.ComboBoxItem());
            list73[2].ValueMember = "DESCRICAO";
            list73[2].DisplayMember = "Descrição";

            psComboBox13.DataSource = list73;
            psComboBox13.DisplayMember = "DisplayMember";
            psComboBox13.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list74 = new List<PS.Lib.ComboBoxItem>();

            list74.Add(new PS.Lib.ComboBoxItem());
            list74[0].ValueMember = 1;
            list74[0].DisplayMember = "NF-e Normal";

            list74.Add(new PS.Lib.ComboBoxItem());
            list74[1].ValueMember = 2;
            list74[1].DisplayMember = "NF-e Complementar";

            list74.Add(new PS.Lib.ComboBoxItem());
            list74[2].ValueMember = 3;
            list74[2].DisplayMember = "NF-e de Ajuste";

            list74.Add(new PS.Lib.ComboBoxItem());
            list74[3].ValueMember = 4;
            list74[3].DisplayMember = "Devolução/Retorno";

            psComboBox32.DataSource = list74;
            psComboBox32.DisplayMember = "DisplayMember";
            psComboBox32.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list75 = new List<PS.Lib.ComboBoxItem>();

            list75.Add(new PS.Lib.ComboBoxItem());
            list75[0].ValueMember = "E";
            list75[0].DisplayMember = "Edita";

            list75.Add(new PS.Lib.ComboBoxItem());
            list75[1].ValueMember = "N";
            list75[1].DisplayMember = "Não Edita";

            list75.Add(new PS.Lib.ComboBoxItem());
            list75[2].ValueMember = "M";
            list75[2].DisplayMember = "Não Mostra";

            psComboBox72.DataSource = list75;
            psComboBox72.DisplayMember = "DisplayMember";
            psComboBox72.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list76 = new List<PS.Lib.ComboBoxItem>();

            list76.Add(new PS.Lib.ComboBoxItem());
            list76[0].ValueMember = "E";
            list76[0].DisplayMember = "Edita";

            list76.Add(new PS.Lib.ComboBoxItem());
            list76[1].ValueMember = "N";
            list76[1].DisplayMember = "Não Edita";

            list76.Add(new PS.Lib.ComboBoxItem());
            list76[2].ValueMember = "M";
            list76[2].DisplayMember = "Não Mostra";

            psComboBox73.DataSource = list76;
            psComboBox73.DisplayMember = "DisplayMember";
            psComboBox73.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list77 = new List<PS.Lib.ComboBoxItem>();

            list77.Add(new PS.Lib.ComboBoxItem());
            list77[0].ValueMember = "E";
            list77[0].DisplayMember = "Edita";

            list77.Add(new PS.Lib.ComboBoxItem());
            list77[1].ValueMember = "N";
            list77[1].DisplayMember = "Não Edita";

            list77.Add(new PS.Lib.ComboBoxItem());
            list77[2].ValueMember = "M";
            list77[2].DisplayMember = "Não Mostra";

            psComboBox74.DataSource = list77;
            psComboBox74.DisplayMember = "DisplayMember";
            psComboBox74.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list78 = new List<PS.Lib.ComboBoxItem>();

            list78.Add(new PS.Lib.ComboBoxItem());
            list78[0].ValueMember = "E";
            list78[0].DisplayMember = "Edita";

            list78.Add(new PS.Lib.ComboBoxItem());
            list78[1].ValueMember = "N";
            list78[1].DisplayMember = "Não Edita";

            list78.Add(new PS.Lib.ComboBoxItem());
            list78[2].ValueMember = "M";
            list78[2].DisplayMember = "Não Mostra";

            psComboBox75.DataSource = list78;
            psComboBox75.DisplayMember = "DisplayMember";
            psComboBox75.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list79 = new List<PS.Lib.ComboBoxItem>();

            list79.Add(new PS.Lib.ComboBoxItem());
            list79[0].ValueMember = "E";
            list79[0].DisplayMember = "Edita";

            list79.Add(new PS.Lib.ComboBoxItem());
            list79[1].ValueMember = "N";
            list79[1].DisplayMember = "Não Edita";

            list79.Add(new PS.Lib.ComboBoxItem());
            list79[2].ValueMember = "M";
            list79[2].DisplayMember = "Não Mostra";

            psComboBox24.DataSource = list79;
            psComboBox24.DisplayMember = "DisplayMember";
            psComboBox24.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list80 = new List<PS.Lib.ComboBoxItem>();

            list80.Add(new PS.Lib.ComboBoxItem());
            list80[0].ValueMember = "E";
            list80[0].DisplayMember = "Edita";

            list80.Add(new PS.Lib.ComboBoxItem());
            list80[1].ValueMember = "N";
            list80[1].DisplayMember = "Não Edita";

            list80.Add(new PS.Lib.ComboBoxItem());
            list80[2].ValueMember = "M";
            list80[2].DisplayMember = "Não Mostra";

            psComboBox25.DataSource = list80;
            psComboBox25.DisplayMember = "DisplayMember";
            psComboBox25.ValueMember = "ValueMember";


            List<PS.Lib.ComboBoxItem> list82 = new List<PS.Lib.ComboBoxItem>();

            list82.Add(new PS.Lib.ComboBoxItem());
            list82[0].ValueMember = "E";
            list82[0].DisplayMember = "Edita";

            list82.Add(new PS.Lib.ComboBoxItem());
            list82[1].ValueMember = "N";
            list82[1].DisplayMember = "Não Edita";

            list82.Add(new PS.Lib.ComboBoxItem());
            list82[2].ValueMember = "M";
            list82[2].DisplayMember = "Não Mostra";

            psComboBox78.DataSource = list82;
            psComboBox78.DisplayMember = "DisplayMember";
            psComboBox78.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list79_AA = new List<PS.Lib.ComboBoxItem>();

            list79_AA.Add(new PS.Lib.ComboBoxItem());
            list79_AA[0].ValueMember = "E";
            list79_AA[0].DisplayMember = "Edita";

            list79_AA.Add(new PS.Lib.ComboBoxItem());
            list79_AA[1].ValueMember = "N";
            list79_AA[1].DisplayMember = "Não Edita";

            list79_AA.Add(new PS.Lib.ComboBoxItem());
            list79_AA[2].ValueMember = "M";
            list79_AA[2].DisplayMember = "Não Mostra";

            psComboBox79.DataSource = list79_AA;
            psComboBox79.DisplayMember = "DisplayMember";
            psComboBox79.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list84 = new List<PS.Lib.ComboBoxItem>();

            list84.Add(new PS.Lib.ComboBoxItem());
            list84[0].ValueMember = "E";
            list84[0].DisplayMember = "Edita";

            list84.Add(new PS.Lib.ComboBoxItem());
            list84[1].ValueMember = "N";
            list84[1].DisplayMember = "Não Edita";

            list84.Add(new PS.Lib.ComboBoxItem());
            list84[2].ValueMember = "M";
            list84[2].DisplayMember = "Não Mostra";

            psComboBox80.DataSource = list84;
            psComboBox80.DisplayMember = "DisplayMember";
            psComboBox80.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list85 = new List<PS.Lib.ComboBoxItem>();
            list85.Add(new PS.Lib.ComboBoxItem());
            list85[0].ValueMember = "E";
            list85[0].DisplayMember = "Edita";

            list85.Add(new PS.Lib.ComboBoxItem());
            list85[1].ValueMember = "N";
            list85[1].DisplayMember = "Não Edita";

            list85.Add(new PS.Lib.ComboBoxItem());
            list85[2].ValueMember = "M";
            list85[2].DisplayMember = "Não Mostra";

            psComboBox81.DataSource = list85;
            psComboBox81.DisplayMember = "DisplayMember";
            psComboBox81.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list86 = new List<PS.Lib.ComboBoxItem>();

            list86.Add(new PS.Lib.ComboBoxItem());
            list86[0].ValueMember = "E";
            list86[0].DisplayMember = "Edita";

            list86.Add(new PS.Lib.ComboBoxItem());
            list86[1].ValueMember = "N";
            list86[1].DisplayMember = "Não Edita";

            list86.Add(new PS.Lib.ComboBoxItem());
            list86[2].ValueMember = "M";
            list86[2].DisplayMember = "Não Mostra";

            psComboBox82.DataSource = list86;
            psComboBox82.DisplayMember = "DisplayMember";
            psComboBox82.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list87 = new List<PS.Lib.ComboBoxItem>();

            list87.Add(new PS.Lib.ComboBoxItem());
            list87[0].ValueMember = "S";
            list87[0].DisplayMember = "Sim";

            list87.Add(new PS.Lib.ComboBoxItem());
            list87[1].ValueMember = "N";
            list87[1].DisplayMember = "Não";

            psComboBox84.DataSource = list87;
            psComboBox84.DisplayMember = "DisplayMember";
            psComboBox84.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list88 = new List<PS.Lib.ComboBoxItem>();

            list88.Add(new PS.Lib.ComboBoxItem());
            list88[0].ValueMember = "T";
            list88[0].DisplayMember = "Total";

            list88.Add(new PS.Lib.ComboBoxItem());
            list88[1].ValueMember = "I";
            list88[1].DisplayMember = "Item";

            psComboBox85.DataSource = list88;
            psComboBox85.DisplayMember = "DisplayMember";
            psComboBox85.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list89 = new List<PS.Lib.ComboBoxItem>();
            list89.Add(new PS.Lib.ComboBoxItem());
            list89[0].ValueMember = "E";
            list89[0].DisplayMember = "Edita";

            list89.Add(new PS.Lib.ComboBoxItem());
            list89[1].ValueMember = "N";
            list89[1].DisplayMember = "Não Edita";

            list89.Add(new PS.Lib.ComboBoxItem());
            list89[2].ValueMember = "M";
            list86[2].DisplayMember = "Não Mostra";

            psComboBox77.DataSource = list89;
            psComboBox77.DisplayMember = "DisplayMember";
            psComboBox77.ValueMember = "ValueMember";


            List<PS.Lib.ComboBoxItem> list90 = new List<PS.Lib.ComboBoxItem>();
            list90.Add(new PS.Lib.ComboBoxItem());
            list90[0].ValueMember = "E";
            list90[0].DisplayMember = "Edita";

            list90.Add(new PS.Lib.ComboBoxItem());
            list90[1].ValueMember = "N";
            list90[1].DisplayMember = "Não Edita";

            list90.Add(new PS.Lib.ComboBoxItem());
            list90[2].ValueMember = "M";
            list90[2].DisplayMember = "Não Mostra";

            psComboBox86.DataSource = list90;
            psComboBox86.DisplayMember = "DisplayMember";
            psComboBox86.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list91 = new List<PS.Lib.ComboBoxItem>();

            list91.Add(new PS.Lib.ComboBoxItem());
            list91[0].ValueMember = "N";
            list91[0].DisplayMember = "Não";

            list91.Add(new PS.Lib.ComboBoxItem());
            list91[1].ValueMember = "S";
            list91[1].DisplayMember = "Sim";

            psComboBox87.DataSource = list91;
            psComboBox87.DisplayMember = "DisplayMember";
            psComboBox87.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list92 = new List<PS.Lib.ComboBoxItem>();

            list92.Add(new PS.Lib.ComboBoxItem());
            list92[0].ValueMember = "N";
            list92[0].DisplayMember = "Não";

            list92.Add(new PS.Lib.ComboBoxItem());
            list92[1].ValueMember = "S";
            list92[1].DisplayMember = "Sim";

            psComboBox88.DataSource = list92;
            psComboBox88.DisplayMember = "DisplayMember";
            psComboBox88.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list93 = new List<PS.Lib.ComboBoxItem>();

            list93.Add(new PS.Lib.ComboBoxItem());
            list93[0].ValueMember = "U";
            list93[0].DisplayMember = "Item";

            list93.Add(new PS.Lib.ComboBoxItem());
            list93[1].ValueMember = "T";
            list93[1].DisplayMember = "Total";

            psComboBox89.DataSource = list93;
            psComboBox89.DisplayMember = "DisplayMember";
            psComboBox89.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list94 = new List<PS.Lib.ComboBoxItem>();

            list94.Add(new PS.Lib.ComboBoxItem());
            list94[0].ValueMember = "0";
            list94[0].DisplayMember = "Data Emissão";

            list94.Add(new PS.Lib.ComboBoxItem());
            list94[1].ValueMember = "1";
            list94[1].DisplayMember = "Data Entrega";

            list94.Add(new PS.Lib.ComboBoxItem());
            list94[2].ValueMember = "2";
            list94[2].DisplayMember = "Data Saída";

            psComboBaseVencimento.DataSource = list94;
            psComboBaseVencimento.DisplayMember = "DisplayMember";
            psComboBaseVencimento.ValueMember = "ValueMember";

            PSComboCODQUERY.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODQUERY, DESCRICAO FROM GQUERY WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });
            PSComboCODQUERY.ValueMember = "CODQUERY";
            PSComboCODQUERY.DisplayMember = "DESCRICAO";

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

            List<PS.Lib.ComboBoxItem> listAceitaPrecoZero = new List<PS.Lib.ComboBoxItem>();

            listAceitaPrecoZero.Add(new PS.Lib.ComboBoxItem());
            listAceitaPrecoZero[0].ValueMember = "0";
            listAceitaPrecoZero[0].DisplayMember = "Sim";

            listAceitaPrecoZero.Add(new PS.Lib.ComboBoxItem());
            listAceitaPrecoZero[1].ValueMember = "1";
            listAceitaPrecoZero[1].DisplayMember = "Não";


            psComboAceitaPrecoZero.DataSource = listAceitaPrecoZero;
            psComboAceitaPrecoZero.DisplayMember = "DisplayMember";
            psComboAceitaPrecoZero.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listUsaLimiteCredito = new List<PS.Lib.ComboBoxItem>();

            listUsaLimiteCredito.Add(new PS.Lib.ComboBoxItem());
            listUsaLimiteCredito[0].ValueMember = "0";
            listUsaLimiteCredito[0].DisplayMember = "Sim";

            listUsaLimiteCredito.Add(new PS.Lib.ComboBoxItem());
            listUsaLimiteCredito[1].ValueMember = "1";
            listUsaLimiteCredito[1].DisplayMember = "Não";


            psComboUsaLimiteCredito.DataSource = listUsaLimiteCredito;
            psComboUsaLimiteCredito.DisplayMember = "DisplayMember";
            psComboUsaLimiteCredito.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listUtilizaComposto = new List<PS.Lib.ComboBoxItem>();

            listUtilizaComposto.Add(new PS.Lib.ComboBoxItem());
            listUtilizaComposto[0].ValueMember = "S";
            listUtilizaComposto[0].DisplayMember = "Sim";

            listUtilizaComposto.Add(new PS.Lib.ComboBoxItem());
            listUtilizaComposto[1].ValueMember = "N";
            listUtilizaComposto[1].DisplayMember = "Não";

            psComboBox90.DataSource = listUtilizaComposto;
            psComboBox90.DisplayMember = "DisplayMember";
            psComboBox90.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listExpandirItens = new List<PS.Lib.ComboBoxItem>();

            listExpandirItens.Add(new PS.Lib.ComboBoxItem());
            listExpandirItens[0].ValueMember = "S";
            listExpandirItens[0].DisplayMember = "Sim";

            listExpandirItens.Add(new PS.Lib.ComboBoxItem());
            listExpandirItens[1].ValueMember = "N";
            listExpandirItens[1].DisplayMember = "Não";

            psComboBox91.DataSource = listExpandirItens;
            psComboBox91.DisplayMember = "DisplayMember";
            psComboBox91.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listMantemProdutoExp = new List<PS.Lib.ComboBoxItem>();

            listMantemProdutoExp.Add(new PS.Lib.ComboBoxItem());
            listMantemProdutoExp[0].ValueMember = "S";
            listMantemProdutoExp[0].DisplayMember = "Sim";

            listMantemProdutoExp.Add(new PS.Lib.ComboBoxItem());
            listMantemProdutoExp[1].ValueMember = "N";
            listMantemProdutoExp[1].DisplayMember = "Não";

            psComboBox92.DataSource = listMantemProdutoExp;
            psComboBox92.DisplayMember = "DisplayMember";
            psComboBox92.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listClassificacaoCliFor = new List<PS.Lib.ComboBoxItem>();

            listClassificacaoCliFor.Add(new PS.Lib.ComboBoxItem());
            listClassificacaoCliFor[0].ValueMember = 0;
            listClassificacaoCliFor[0].DisplayMember = "Cliente";

            listClassificacaoCliFor.Add(new PS.Lib.ComboBoxItem());
            listClassificacaoCliFor[1].ValueMember = 1;
            listClassificacaoCliFor[1].DisplayMember = "Fornecedor";

            listClassificacaoCliFor.Add(new PS.Lib.ComboBoxItem());
            listClassificacaoCliFor[2].ValueMember = 2;
            listClassificacaoCliFor[2].DisplayMember = "Ambos";

            listClassificacaoCliFor.Add(new PS.Lib.ComboBoxItem());
            listClassificacaoCliFor[3].ValueMember = 3;
            listClassificacaoCliFor[3].DisplayMember = "Todos";

            psComboBox93.DataSource = listClassificacaoCliFor;
            psComboBox93.DisplayMember = "DisplayMember";
            psComboBox93.ValueMember = "ValueMember";


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

            psComboBox94.DataSource = ListaQuantidadeDecimal;
            psComboBox94.DisplayMember = "DisplayMember";
            psComboBox94.ValueMember = "ValueMember";

            #region Unidade de Medida 

            List<PS.Lib.ComboBoxItem> ListComboFatorConversao = new List<PS.Lib.ComboBoxItem>();

            ListComboFatorConversao.Add(new PS.Lib.ComboBoxItem());
            ListComboFatorConversao[0].ValueMember = "PADRAO";
            ListComboFatorConversao[0].DisplayMember = "Padrão";

            ListComboFatorConversao.Add(new PS.Lib.ComboBoxItem());
            ListComboFatorConversao[1].ValueMember = "PRODUTO";
            ListComboFatorConversao[1].DisplayMember = "Produto";

            psComboFatorConversao.DataSource = ListComboFatorConversao;
            psComboFatorConversao.DisplayMember = "DisplayMember";
            psComboFatorConversao.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> ListComboCodUnidOper = new List<PS.Lib.ComboBoxItem>();

            ListComboCodUnidOper.Add(new PS.Lib.ComboBoxItem());
            ListComboCodUnidOper[0].ValueMember = "E";
            ListComboCodUnidOper[0].DisplayMember = "Edita";

            ListComboCodUnidOper.Add(new PS.Lib.ComboBoxItem());
            ListComboCodUnidOper[1].ValueMember = "N";
            ListComboCodUnidOper[1].DisplayMember = "Não Edita";

            psComboEditaCodUnidOper.DataSource = ListComboCodUnidOper;
            psComboEditaCodUnidOper.DisplayMember = "DisplayMember";
            psComboEditaCodUnidOper.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> ListComboEditaFatorConversao = new List<PS.Lib.ComboBoxItem>();

            ListComboEditaFatorConversao.Add(new PS.Lib.ComboBoxItem());
            ListComboEditaFatorConversao[0].ValueMember = "E";
            ListComboEditaFatorConversao[0].DisplayMember = "Edita";

            ListComboEditaFatorConversao.Add(new PS.Lib.ComboBoxItem());
            ListComboEditaFatorConversao[1].ValueMember = "N";
            ListComboEditaFatorConversao[1].DisplayMember = "Não Edita";

            psComboEditaFatorConversao.DataSource = ListComboEditaFatorConversao;
            psComboEditaFatorConversao.DisplayMember = "DisplayMember";
            psComboEditaFatorConversao.ValueMember = "ValueMember";

            #endregion

            #endregion
            #region Query

            cmbQuery.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODQUERY, DESCRICAO FROM GQUERY WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });
            cmbQuery.DisplayMember = "DESCRICAO";
            cmbQuery.ValueMember = "CODQUERY";

            cmbNewQuery.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODQUERY, DESCRICAO FROM GQUERY WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });
            cmbNewQuery.DisplayMember = "DESCRICAO";
            cmbNewQuery.ValueMember = "CODQUERY";

            #endregion
        }

        public void carregaGrid(string where)
        {

            List<string> tabelasFilhas = new List<string>();
            tabelasFilhas.Add("VOPERMENSAGEM");
            //Verifica se existe registro na tabela GVISAOUSUARIO
            int colunas = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(COLUNA) FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1", new object[] { tabela, AppLib.Context.Usuario }));
            if (colunas == 0)
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });
                string tabelas = "'" + tabela + "'";
                for (int i = 0; i < tabelasFilhas.Count; i++)
                {
                    tabelas = tabelas + ", '" + tabelasFilhas[i].ToString() + "'";
                }
                DataTable db = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUMN_NAME, TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME IN ( " + tabelas + "  )", new object[] { });
                for (int i = 0; i < db.Rows.Count; i++)
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO GVISAOUSUARIO (VISAO, CODUSUARIO, SEQUENCIA, COLUNA, LARGURA, VISIVEL) VALUES (?, ?, ?, ?, ?, ?)", new object[] { tabela, AppLib.Context.Usuario, i, (db.Rows[i]["TABLE_NAME"].ToString() + "." + db.Rows[i]["COLUMN_NAME"].ToString()), 100, 1 });
                }
            }
            DataTable dt = new DataTable();
            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
            string sql = string.Empty;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(sql))
                {
                    sql = "SELECT " + dt.Rows[i]["COLUNA"].ToString() + " AS " + "'" + dt.Rows[i]["COLUNA"].ToString() + "'";
                }
                else
                {
                    sql = sql + ", " + dt.Rows[i]["COLUNA"].ToString() + " AS " + "'" + dt.Rows[i]["COLUNA"].ToString() + "'";
                }
            }
            sql = sql + " FROM " + tabela + " " + relacionamento + " " + where;

            gridMensagemFisco.DataSource = null;
            gridView2.Columns.Clear();


            DataTable dtGrid = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

            gridMensagemFisco.DataSource = dtGrid;

            //carregaImagem();
            //carregaImagemStatus();


            DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
            for (int i = 0; i < gridView2.Columns.Count; i++)
            {
                gridView2.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                DataRow result = dic.Rows.Find(new object[] { gridView2.Columns[i].FieldName.ToString() });
                if (result != null)
                {
                    gridView2.Columns[i].Caption = result["DESCRICAO"].ToString();
                }
            }
        }

        private void PSPartTipOperEdit_Load(object sender, EventArgs e)
        {
            query = "WHERE GOPERMENSAGEMFISCO.CODTIPOPER = '" + psTextoBox1.Text + "' AND GOPERMENSAGEMFISCO.CODEMPRESA = " + AppLib.Context.Empresa;
            carregaGridClassificacao();
            carregaGrid(query);
            carregaGridOper();
            carregaGridItens();
        }

        private void carregaGridClassificacao()
        {
            gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
VPRODUTOCLASSIFICACAO.CODCLASSIFICACAO, 
VPRODUTOCLASSIFICACAO.DESCRICAO 
FROM GTIPOPERCLASSIFICAOPRODUTO 
INNER JOIN VPRODUTOCLASSIFICACAO ON GTIPOPERCLASSIFICAOPRODUTO.CODCLASSIFICACAO = VPRODUTOCLASSIFICACAO.CODCLASSIFICACAO
WHERE 
GTIPOPERCLASSIFICAOPRODUTO.CODTIPOPER = ?
AND GTIPOPERCLASSIFICAOPRODUTO.CODEMPRESA = ?", new object[] { psTextoBox1.textBox1.Text, AppLib.Context.Empresa });

            gridView1.Columns[0].Width = 200;
            gridView1.Columns[1].Width = 400;

        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psCheckBox1.Checked = false;

            psComboBox33.SelectedIndex = 2;
            psComboBox34.SelectedIndex = 2;
            psComboBox35.SelectedIndex = 2;
            psComboBox36.SelectedIndex = 2;
            psComboBox37.SelectedIndex = 2;
            psComboBox38.SelectedIndex = 2;
            psComboBox39.SelectedIndex = 2;
            psComboBox40.SelectedIndex = 2;
            psComboBox41.SelectedIndex = 2;
            psComboBox42.SelectedIndex = 2;
            psComboBox43.SelectedIndex = 2;
            psComboBox44.SelectedIndex = 2;
            psComboBox45.SelectedIndex = 2;
            psComboBox46.SelectedIndex = 2;
            psComboBox47.SelectedIndex = 2;
            psComboBox48.SelectedIndex = 2;
            psComboBox49.SelectedIndex = 2;
            psComboBox50.SelectedIndex = 2;

            psComboBox51.SelectedIndex = 2;
            psComboBox52.SelectedIndex = 2;
            psComboBox53.SelectedIndex = 2;
            psComboBox54.SelectedIndex = 2;
            psComboBox55.SelectedIndex = 2;
            psComboBox56.SelectedIndex = 2;
            psComboBox57.SelectedIndex = 2;
            psComboBox58.SelectedIndex = 2;
            psComboBox59.SelectedIndex = 2;
            psComboBox60.SelectedIndex = 2;
            psComboBox61.SelectedIndex = 2;
            psComboBox62.SelectedIndex = 2;
            psComboBox63.SelectedIndex = 2;
            psComboBox64.SelectedIndex = 2;
            psComboBox65.SelectedIndex = 2;
            psComboBox66.SelectedIndex = 2;
            psComboBox67.SelectedIndex = 2;
            psComboBox68.SelectedIndex = 2;
        }

        private void psCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (psCheckBox4.Checked)
            {
                psLookup1.Chave = true;
                psLookup2.Chave = true;
                psLookup4.Chave = true;
            }
            else
            {
                psLookup1.Chave = false;
                psLookup1.Text = string.Empty;
                psLookup2.Chave = false;
                psLookup2.Text = string.Empty;
                psLookup4.Chave = false;
                psLookup4.Text = string.Empty;
            }
        }

        private void psLookup14_BeforeLookup(object sender, Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODFILIAL", psLookup13.Text));
        }

        private void psLookup8_BeforeLookup(object sender, Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODTIPOPER", psTextoBox1.Text));
        }

        private void psTextoBox1_Load(object sender, EventArgs e)
        {

        }

        private void gridData1_SetParametros(object sender, EventArgs e)
        {
            gridData1.Parametros = new Object[] { AppLib.Context.Empresa, psTextoBox1.Text };
        }

        private void gridData1_Novo(object sender, EventArgs e)
        {
            ERP.Comercial.FormGTIPOPERREPORT2Cadastro f = new ERP.Comercial.FormGTIPOPERREPORT2Cadastro();
            f.Chave.Add(new AppLib.ORM.CampoValor("CODEMPRESA", AppLib.Context.Empresa));
            f.Chave.Add(new AppLib.ORM.CampoValor("CODTIPOPER", psTextoBox1.Text));
            f.Novo();
        }

        private void gridData1_Editar(object sender, EventArgs e)
        {
            ERP.Comercial.FormGTIPOPERREPORT2Cadastro f = new ERP.Comercial.FormGTIPOPERREPORT2Cadastro();
            f.Chave.Add(new AppLib.ORM.CampoValor("CODEMPRESA", AppLib.Context.Empresa));
            f.Chave.Add(new AppLib.ORM.CampoValor("CODTIPOPER", psTextoBox1.Text));
            f.Editar(gridData1);
        }

        private void gridData1_Excluir(object sender, EventArgs e)
        {
            ERP.Comercial.FormGTIPOPERREPORT2Cadastro f = new ERP.Comercial.FormGTIPOPERREPORT2Cadastro();
            f.Chave.Add(new AppLib.ORM.CampoValor("CODEMPRESA", AppLib.Context.Empresa));
            f.Chave.Add(new AppLib.ORM.CampoValor("CODTIPOPER", psTextoBox1.Text));
            f.Excluir(gridData1);
        }

        private void psLookup21_BeforeLookup(object sender, Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODFILIAL", psLookup20.Text));
        }

        private void psComboBox84_SelectedValueChanged(object sender, EventArgs e)
        {
            if (psComboBox84.Text == "Sim")
            {
                psComboBox85.Enabled = true;
            }
            else
            {
                psComboBox85.Enabled = false;
                psComboBox85.Text = "";
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            New.Cadastros.frmClassificaoProdutoTipoOperacao frm = new New.Cadastros.frmClassificaoProdutoTipoOperacao(psTextoBox1.textBox1.Text);
            frm.ShowDialog();
            carregaGridClassificacao();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GTIPOPERCLASSIFICAOPRODUTO WHERE CODTIPOPER = ? AND CODEMPRESA = ? AND CODCLASSIFICACAO = ?", new object[] { psTextoBox1.textBox1.Text, AppLib.Context.Empresa, row[0] });

            }
            carregaGridClassificacao();
        }

        private void psComboBox84_Validated(object sender, EventArgs e)
        {

        }

        private void psComboBox84_SelectedValueChanged_1(object sender, EventArgs e)
        {
            if (psComboBox84.SelectedIndex == 1)
            {
                psComboBox85.Enabled = false;
                psComboBox77.Enabled = false;
            }
            else
            {
                psComboBox85.Enabled = true;
                psComboBox77.Enabled = true;
            }
        }

        private void btnIncluirMensagem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(psLookup24.textBox1.Text))
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO GOPERMENSAGEMFISCO (CODEMPRESA, CODTIPOPER, CODMENSAGEM) VALUES (?, ?, ?)", new object[] { AppLib.Context.Empresa, psTextoBox1.textBox1.Text, psLookup24.textBox1.Text });
                carregaGrid(query);
            }
        }

        private void btnExcluirFisco_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView2.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView2.GetDataRow(Convert.ToInt32(gridView2.GetSelectedRows().GetValue(i).ToString()));
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GOPERMENSAGEMFISCO WHERE CODEMPRESA = ? AND CODTIPOPER = ? AND CODMENSAGEM = ?", new object[] { row1["GOPERMENSAGEMFISCO.CODEMPRESA"], row1["GOPERMENSAGEMFISCO.CODTIPOPER"], row1["GOPERMENSAGEMFISCO.CODMENSAGEM"] });
            }
            carregaGrid(query);
        }

        private void btnAtualizarColunaFisco_Click(object sender, EventArgs e)
        {
            carregaGrid(query);
        }

        private void btnSelecionarColunaFisco_Click(object sender, EventArgs e)
        {
            Glb.New.frmSelecaoColunas frm = new Glb.New.frmSelecaoColunas(tabela);
            frm.ShowDialog();
            carregaGrid(query);
        }

        private void btnSalvarLayoutFisco_Click(object sender, EventArgs e)
        {
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });
            for (int i = 0; i < gridView2.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", tabela);
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView2.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView2.VisibleColumns[i].Width);
                GVISAOUSUARIO.Set("VISIVEL", 1);
                GVISAOUSUARIO.Save();
            }

            DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ? AND FIXO = 1", new object[] { AppLib.Context.Usuario, tabela });
            if (dtFixo.Rows.Count > 0)
            {
                for (int i = 0; i < dtFixo.Rows.Count; i++)
                {
                    AppLib.ORM.Jit FIXO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                    FIXO.Set("VISAO", tabela);
                    FIXO.Set("CODUSUARIO", AppLib.Context.Usuario);
                    FIXO.Set("COLUNA", dtFixo.Rows[i]["COLUNA"]);
                    FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
                carregaGrid(query);
            }
        }

        private void psComboBox90_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (psComboBox90.Value.ToString() == "S")
            //{
            //    psComboBox91.Enabled = true;
            //    psComboBox92.Enabled = true;
            //}
            //else
            //{
            //    psComboBox91.Enabled = false;
            //    psComboBox92.Enabled = false;
            //}
        }

        private void carregaGridOper()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT NOMECAMPO, DESCRICAO FROM GTABCAMPOCOMPL WHERE CODENTIDADE = 'GOPERCOMPL'", new object[] { });
            gridComplOper.DataSource = dt;
        }

        private void carregaGridItens()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT NOMECAMPO, DESCRICAO FROM GTABCAMPOCOMPL WHERE CODENTIDADE = 'GOPERITEMCOMPL'", new object[] { });
            gridComplItens.DataSource = dt;
        }

        private void btnSalvarCamposComplementares_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }

        private void psComboBox22_Load(object sender, EventArgs e)
        {

        }

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {

        }

        private void psCheckBox1_Load(object sender, EventArgs e)
        {

        }

        private void psCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (psCheckBox1.checkBox1.Checked == true)
            {
                psCheckBox57.checkBox1.Checked = false;
            }
        }

        private void psCheckBox57_CheckedChanged(object sender, EventArgs e)
        {
            if (psCheckBox57.checkBox1.Checked == true)
            {
                psCheckBox1.checkBox1.Checked = false;
            }
        }

        private void psComboBox86_Load(object sender, EventArgs e)
        {

        }

        private void psComboBox32_SelectedValueChanged(object sender, EventArgs e)
        {
            if (psComboBox32.SelectedIndex == 1)
            {
                psDescNatOP.Visible = true;
            }
            else
            {
                psDescNatOP.Visible = false;
            }
        }
    }
}
