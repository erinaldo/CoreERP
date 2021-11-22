using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartProduto : PS.Lib.WinForms.PSPartGridView
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public PSPartProduto()
        {
            this.TableName = "VPRODUTO";
            this.Keys = new string[] { "CODEMPRESA", "CODPRODUTO" };
            this.FormEditName = "PSPartProdutoEdit";
            this.PSPartData = new PSPartProdutoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));
            
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("PESOBRUTO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("PESOLIQUIDO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("COMPRIMENTO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("LARGURA",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("ESPESSURA",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("DIAMETRO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("PRECO1",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("PRECO2",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("PRECO3",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("PRECO4",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("PRECO5",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));
            
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CPRODSERV",
                                                                                "",
                                                                                PS.Lib.DataGridColumnType.Image,
                                                                                1,
                                                                                "PRODSERV",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter,
                                                                                new PS.Lib.ImageProperties[] { new PS.Lib.ImageProperties(1, "Produto", Properties.Resources.img_produto),
                                                                                                               new PS.Lib.ImageProperties(2, "Serviço", Properties.Resources.img_service)} ));
            
            
            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartCodigoBarras(), "CODEMPRESA", "CODPRODUTO"));

            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartProdutoCompl(), "CODEMPRESA", "CODPRODUTO"));
            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartProdutoTributo(), "CODEMPRESA", "CODPRODUTO"));
            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartProdutoTributoEstado(), "CODEMPRESA", "CODPRODUTO"));
            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartProdutoCom(), "CODEMPRESA", "CODPRODUTO"));
            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartLocalEstoqueSaldo(), "CODEMPRESA", "CODPRODUTO"));
            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartProdutoFiscal(), "CODEMPRESA", "CODPRODUTO"));
            


            this.SecurityID = "PSPartProduto";
            this.ModuleID = "VR";
        }

        public override void CustomDataGridView(System.Windows.Forms.DataGridView DataGrid)
        {
            base.CustomDataGridView(DataGrid);

            DataRow VPARAMETROS = gb.RetornaParametrosVarejo();

            if (VPARAMETROS == null)
            {
                throw new Exception("Não foi possível carregar os parâmetros do Varejo.");
            }

            //Fazer a verificação se a grid esta vazia ou não para não apresentar erro.
            if (DataGrid.Rows.Count <= 0)
                return;

            #region Preco1

            //Edita
            if (VPARAMETROS["PRDUSAPRECO1"].ToString() == "E")
            {
                DataGrid.Columns["PRECO1"].Visible = true;
                DataGrid.Columns["CODMOEDA1"].Visible = true;
            }

            //Não Mostra
            if (VPARAMETROS["PRDUSAPRECO1"].ToString() == "M")
            {
                DataGrid.Columns["PRECO1"].Visible = false;
                DataGrid.Columns["CODMOEDA1"].Visible = false;
            }

            #endregion

            #region Texto dos Preços/Moedas

            DataGrid.Columns["PRECO1"].HeaderText = VPARAMETROS["PRDTEXTOPRECO1"].ToString();
            DataGrid.Columns["PRECO2"].HeaderText = VPARAMETROS["PRDTEXTOPRECO2"].ToString();
            DataGrid.Columns["PRECO3"].HeaderText = VPARAMETROS["PRDTEXTOPRECO3"].ToString();
            DataGrid.Columns["PRECO4"].HeaderText = VPARAMETROS["PRDTEXTOPRECO4"].ToString();
            DataGrid.Columns["PRECO5"].HeaderText = VPARAMETROS["PRDTEXTOPRECO5"].ToString();

            //DataGrid.Columns["CODMOEDA1"].HeaderText = string.Concat(DataGrid.Columns["CODMOEDA1"].HeaderText, " ", VPARAMETROS["PRDTEXTOPRECO1"].ToString());
            //DataGrid.Columns["CODMOEDA2"].HeaderText = string.Concat(DataGrid.Columns["CODMOEDA2"].HeaderText, " ", VPARAMETROS["PRDTEXTOPRECO2"].ToString());
            //DataGrid.Columns["CODMOEDA3"].HeaderText = string.Concat(DataGrid.Columns["CODMOEDA3"].HeaderText, " ", VPARAMETROS["PRDTEXTOPRECO3"].ToString());
            //DataGrid.Columns["CODMOEDA4"].HeaderText = string.Concat(DataGrid.Columns["CODMOEDA4"].HeaderText, " ", VPARAMETROS["PRDTEXTOPRECO4"].ToString());
            //DataGrid.Columns["CODMOEDA5"].HeaderText = string.Concat(DataGrid.Columns["CODMOEDA5"].HeaderText, " ", VPARAMETROS["PRDTEXTOPRECO5"].ToString());

            #endregion

            #region Preco2

            //Edita
            if (VPARAMETROS["PRDUSAPRECO2"].ToString() == "E")
            {
                DataGrid.Columns["PRECO2"].Visible = true;
                DataGrid.Columns["CODMOEDA2"].Visible = true;
            }

            //Não Mostra
            if (VPARAMETROS["PRDUSAPRECO2"].ToString() == "M")
            {
                DataGrid.Columns["PRECO2"].Visible = false;
                DataGrid.Columns["CODMOEDA2"].Visible = false;
            }

            #endregion

            #region Preco3

            //Edita
            if (VPARAMETROS["PRDUSAPRECO3"].ToString() == "E")
            {
                DataGrid.Columns["PRECO3"].Visible = true;
                DataGrid.Columns["CODMOEDA3"].Visible = true;
            }

            //Não Mostra
            if (VPARAMETROS["PRDUSAPRECO3"].ToString() == "M")
            {
                DataGrid.Columns["PRECO3"].Visible = false;
                DataGrid.Columns["CODMOEDA3"].Visible = false;
            }

            #endregion

            #region Preco4

            //Edita
            if (VPARAMETROS["PRDUSAPRECO4"].ToString() == "E")
            {
                DataGrid.Columns["PRECO4"].Visible = true;
                DataGrid.Columns["CODMOEDA4"].Visible = true;
            }

            //Não Mostra
            if (VPARAMETROS["PRDUSAPRECO4"].ToString() == "M")
            {
                DataGrid.Columns["PRECO4"].Visible = false;
                DataGrid.Columns["CODMOEDA4"].Visible = false;
            }

            #endregion

            #region Preco5

            //Edita
            if (VPARAMETROS["PRDUSAPRECO5"].ToString() == "E")
            {
                DataGrid.Columns["PRECO5"].Visible = true;
                DataGrid.Columns["CODMOEDA5"].Visible = true;
            }

            //Não Mostra
            if (VPARAMETROS["PRDUSAPRECO5"].ToString() == "M")
            {
                DataGrid.Columns["PRECO5"].Visible = false;
                DataGrid.Columns["CODMOEDA5"].Visible = false;
            }

            #endregion
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("NUMREGMINAGRI", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NOMEFANTASIA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODIGOAUXILIAR", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODPRDFABRICANTE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODIMAGEM", null, typeof(PS.Lib.WinForms.PSImageBox)));
            objArr.Add(new DataField("CODFABRICANTE", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));

            List<ComboBoxItem> Item1 = new List<ComboBoxItem>();
            Item1.Add(new ComboBoxItem(1, "Produto"));
            Item1.Add(new ComboBoxItem(2, "Serviço"));

            objArr.Add(new DataField("PRODSERV", null, typeof(PS.Lib.WinForms.PSComboBox), Item1));

            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODPRODUTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSMemoBox)));

            List<ComboBoxItem> Item3 = new List<ComboBoxItem>();
            Item3.Add(new ComboBoxItem(0, "Nacional"));
            Item3.Add(new ComboBoxItem(1, "Estrangeira"));

            objArr.Add(new DataField("PROCEDENCIA", null, typeof(PS.Lib.WinForms.PSComboBox), Item3));

            List<ComboBoxItem> Item2 = new List<ComboBoxItem>();
            Item2.Add(new ComboBoxItem(1, "Veiculos Novos"));
            Item2.Add(new ComboBoxItem(2, "Medicamentos"));
            Item2.Add(new ComboBoxItem(3, "Arma de Fogo"));
            Item2.Add(new ComboBoxItem(4, "Combustíveis"));
            Item2.Add(new ComboBoxItem(0, "Outros"));

            objArr.Add(new DataField("CODTIPO", null, typeof(PS.Lib.WinForms.PSComboBox), Item2));
            objArr.Add(new DataField("CODNCMEX", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODNCM", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("COMPRIMENTO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("DIAMETRO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("ESPESSURA", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("LARGURA", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("PESOLIQUIDO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("PESOBRUTO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODUNIDCONTROLE", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODUNIDCOMPRA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODUNIDVENDA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("PRECO5", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODMOEDA5", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("PRECO4", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODMOEDA4", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("PRECO3", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODMOEDA3", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("PRECO2", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODMOEDA2", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("PRECO1", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODMOEDA1", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CUSTOMEDIO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("DATACUSTOMEDIO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("CUSTOUNITARIO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("DATACUSTOUNITARIO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            objArr.Add(new DataField("CEST", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODIMAGEM", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
