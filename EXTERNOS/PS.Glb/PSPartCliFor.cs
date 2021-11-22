using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartCliFor : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartCliFor()
        {
            this.TableName = "VCLIFOR";
            this.Keys = new string[] { "CODEMPRESA", "CODCLIFOR" };
            this.FormEditName = "PSPartCliForEdit";
            this.PSPartData = new PSPartCliForData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODCIDADE", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODCIDADEENT", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODCIDADEPAG", false));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("LIMITECREDITO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));
            
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CCODCLASSIFICACAO",
                                                                                "",
                                                                                PS.Lib.DataGridColumnType.Image,
                                                                                1,
                                                                                "CODCLASSIFICACAO",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter,
                                                                                new PS.Lib.ImageProperties[] { new PS.Lib.ImageProperties(0, "Cliente", Properties.Resources.img_cliente),
                                                                                                                    new PS.Lib.ImageProperties(1, "Fornecedor", Properties.Resources.img_fornecedor),
                                                                                                                    new PS.Lib.ImageProperties(2, "Ambos", Properties.Resources.img_clifor)}));
           
            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartCliForCompl(), "CODEMPRESA", "CODCLIFOR"));
            ////////////////////////CONTATO
            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartContatoCliFor(), "CODEMPRESA", "CODCLIFOR"));
            

            this.SecurityID = "PSPartCliFor";
            this.ModuleID = "VR";
             
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            PSPartParamVarejoData psPartParamVarejoData = new PSPartParamVarejoData();
            psPartParamVarejoData._tablename = "VPARAMETROS";
            psPartParamVarejoData._keys = new string[] { "CODEMPRESA" };
            System.Data.DataTable TabPreco = psPartParamVarejoData.RetornaTabelaPreco(PS.Lib.Contexto.Session.Empresa.CodEmpresa);
            List<PS.Lib.ComboBoxItem> listTabPreco = new List<PS.Lib.ComboBoxItem>();
            foreach (System.Data.DataRow row in TabPreco.Rows)
            {
                listTabPreco.Add(new Lib.ComboBoxItem(row["TABELA"], row["NOME"].ToString()));
            }

            objArr.Add(new DataField("CODTABPRECO", null, typeof(PS.Lib.WinForms.PSComboBox), listTabPreco));

            objArr.Add(new DataField("WEBSITE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("EMAILNFE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CGCCPF", null, typeof(PS.Lib.WinForms.PSMaskedTextBox)));

            List<ComboBoxItem> Item = new List<ComboBoxItem>();
            Item.Add(new ComboBoxItem(0, "Jurídica"));
            Item.Add(new ComboBoxItem(1, "Física"));

            objArr.Add(new DataField("FISICOJURIDICO", null, typeof(PS.Lib.WinForms.PSComboBox), Item));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));

            List<ComboBoxItem> Item1 = new List<ComboBoxItem>();
            Item1.Add(new ComboBoxItem(0, "Cliente"));
            Item1.Add(new ComboBoxItem(1, "Fornecedor"));
            Item1.Add(new ComboBoxItem(2, "Ambos"));

            objArr.Add(new DataField("CODCLASSIFICACAO", null, typeof(PS.Lib.WinForms.PSComboBox), Item1));
            objArr.Add(new DataField("NOMEFANTASIA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODCLIFOR", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CEP", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPORUA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("RUA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPOBAIRRO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("NUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODPAIS", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("COMPLEMENTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODETD", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("BAIRRO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODCIDADE", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODETDENT", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCIDADEENT", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODPAISENT", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("BAIRROENT", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPOBAIRROENT", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("COMPLEMENTOENT", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NUMEROENT", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("RUAENT", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPORUAENT", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CEPENT", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODETDPAG", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCIDADEPAG", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODPAISPAG", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("BAIRROPAG", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPOBAIRROPAG", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("COMPLEMENTOPAG", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NUMEROPAG", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("RUAPAG", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPORUAPAG", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CEPPAG", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("EMAIL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODESTVIC", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("OREMISSOR", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NUMERORG", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DATANASCIMENTO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("TELFAX", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("TELCELULAR", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("TELCOMERCIAL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("TELRESIDENCIAL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("INSCRICAOSUFRAMA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("INSCRICAOMUNICIPAL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("INSCRICAOESTADUAL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
           

            List<ComboBoxItem> Item2 = new List<ComboBoxItem>();
            Item2.Add(new ComboBoxItem(0, "ME"));
            Item2.Add(new ComboBoxItem(1, "EPP"));
            Item2.Add(new ComboBoxItem(2, "Normal"));
            Item2.Add(new ComboBoxItem(3, "Outros"));

            objArr.Add(new DataField("CODREGAPURACAO", null, typeof(PS.Lib.WinForms.PSComboBox), Item2));

            List<ComboBoxItem> Item3 = new List<ComboBoxItem>();
            Item3.Add(new ComboBoxItem(0, "Privada"));
            Item3.Add(new ComboBoxItem(1, "Publica"));
            Item3.Add(new ComboBoxItem(2, "Cooperativa"));

            objArr.Add(new DataField("CODNATJUR", null, typeof(PS.Lib.WinForms.PSComboBox), Item3));

            List<ComboBoxItem> Item4 = new List<ComboBoxItem>();
            Item4.Add(new ComboBoxItem(0, "Contribuinte ICMS")); //1
            Item4.Add(new ComboBoxItem(1, "Contribuinte Isento")); //2
            Item4.Add(new ComboBoxItem(2, "Não Contribuinte")); //9

            objArr.Add(new DataField("CONTRIBICMS", null, typeof(PS.Lib.WinForms.PSComboBox), Item4));

            List<ComboBoxItem> Item5 = new List<ComboBoxItem>();
            Item5.Add(new ComboBoxItem(0, "Brasileira"));
            Item5.Add(new ComboBoxItem(1, "Estrangeira"));

            List<ComboBoxItem> ItemCLASSVENDA = new List<ComboBoxItem>();
            ItemCLASSVENDA.Add(new ComboBoxItem("V", "Venda"));
            ItemCLASSVENDA.Add(new ComboBoxItem("R", "Revenda"));
            ItemCLASSVENDA.Add(new ComboBoxItem("C", "Consumo"));

            objArr.Add(new DataField("NACIONALIDADE", null, typeof(PS.Lib.WinForms.PSComboBox), Item5));
            objArr.Add(new DataField("CODETDEMISSOR", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("LIMITECREDITO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODREPRE", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODTRANSPORTADORA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCONTA", null, typeof(PS.Lib.WinForms.PSLookup)));

            List<ComboBoxItem> Item6 = new List<ComboBoxItem>();
            Item6.Add(new ComboBoxItem(0, "CIF"));
            Item6.Add(new ComboBoxItem(1, "FOB"));
            Item1.Add(new ComboBoxItem(2, "Terceiro"));
            Item1.Add(new ComboBoxItem(3, "Sem Frete"));

            objArr.Add(new DataField("FRETECIFFOB", null, typeof(PS.Lib.WinForms.PSComboBox), Item5));
            objArr.Add(new DataField("CLASSVENDA", null, typeof(PS.Lib.WinForms.PSComboBox), ItemCLASSVENDA));
            objArr.Add(new DataField("CODCCUSTO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCONDICAOCOMPRA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCONDICAOVENDA", null, typeof(PS.Lib.WinForms.PSLookup)));

            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            objArr.Add(new DataField("CCIDADE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CGCCPF", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODNATUREZAORCAMENTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DATACRIACAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODUSUARIOCRIACAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
