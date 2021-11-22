using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartOperacaoItem : PS.Lib.WinForms.PSPartGridView
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public PSPartOperacaoItem()
        {
            this.TableName = "GOPERITEM";
            this.Keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM" };
            this.FormEditName = "PSPartOperacaoItemEdit";
            this.PSPartData = new PSPartOperacaoItemData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            #region Custom Column

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODOPER", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODCFOP", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("OBSERVACAO", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("INFCOMPL", false));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("QUANTIDADE",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VLUNITARIO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VLDESCONTO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("PRDESCONTO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VLTOTALITEM",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("QUANTIDADE_FATURADO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("QUANTIDADE_SALDO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            #endregion

            #region Custom Column Campo Valor

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CAMPOVALOR1",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CAMPOVALOR2",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CAMPOVALOR3",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CAMPOVALOR4",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CAMPOVALOR5",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CAMPOVALOR6",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            #endregion

            this.Folder.Add(new PS.Lib.WinForms.Folder(new PS.Glb.PSPartOperItemCompl(), "CODEMPRESA", "CODOPER", "NSEQITEM"));

            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartOperItemTributo(), "CODEMPRESA", "CODOPER", "NSEQITEM"));
            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartOperItemRateioCC(), "CODEMPRESA", "CODOPER", "NSEQITEM"));
            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartOperItemRateioDP(), "CODEMPRESA", "CODOPER", "NSEQITEM"));

            this.SecurityID = "PSPartOperacaoItem";
            this.ModuleID = "PG";
        }
      
        public override void CustomDataGridView(System.Windows.Forms.DataGridView DataGrid)
        {
            base.CustomDataGridView(DataGrid);
            DataRow PARAMNSTIPOPER;
            DataTable dt = new DataTable();
            if (DataGrid.Rows.Count > 0)
            {
                PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(DataGrid.Rows[0]), "CODEMPRESA");
                PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(DataGrid.Rows[0]), "CODOPER");

                string CodTipoOper = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", dfCODEMPRESA.Valor, dfCODOPER.Valor).ToString();

                //string CodTipoOper = dbs.QueryValue(string.Empty, @"SELECT CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", dfCODEMPRESA.Valor, dfCODOPER.Valor).ToString();
                PARAMNSTIPOPER = gb.RetornaParametrosOperacao(CodTipoOper);

                if (PARAMNSTIPOPER == null)
                {
                    //throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação selecionada.");
                    return;
                }

                //Fazer a verificação se a grid esta vazia ou não para não apresentar erro.
                if (DataGrid.Rows.Count <= 0)
                    return;

                #region Valor Unitário

                //Edita
                if (PARAMNSTIPOPER["USAVLUNITARIO"].ToString() == "E")
                {
                    DataGrid.Columns["VLUNITARIO"].Visible = true;
                }
                DataGrid.Columns["QUANTIDADE_SALDO"].Visible = true;
                DataGrid.Columns["QUANTIDADE_FATURADO"].Visible = true;
                //Não Edita
                if (PARAMNSTIPOPER["USAVLUNITARIO"].ToString() == "N")
                {
                    DataGrid.Columns["VLUNITARIO"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAVLUNITARIO"].ToString() == "M")
                {
                    DataGrid.Columns["VLUNITARIO"].Visible = false;
                }

                #endregion

                #region Percentual Desconto

                //Edita
                if (PARAMNSTIPOPER["USAPRDESCONTO"].ToString() == "E")
                {
                    DataGrid.Columns["PRDESCONTO"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAPRDESCONTO"].ToString() == "N")
                {
                    DataGrid.Columns["PRDESCONTO"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAPRDESCONTO"].ToString() == "M")
                {
                    DataGrid.Columns["PRDESCONTO"].Visible = false;
                }

                #endregion

                #region Valor Desconto

                //Edita
                if (PARAMNSTIPOPER["USAVLDESCONTO"].ToString() == "E")
                {
                    DataGrid.Columns["VLDESCONTO"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAVLDESCONTO"].ToString() == "N")
                {
                    DataGrid.Columns["VLDESCONTO"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAVLDESCONTO"].ToString() == "M")
                {
                    DataGrid.Columns["VLDESCONTO"].Visible = false;
                }

                #endregion

                #region Valor Total Item

                //Edita
                if (PARAMNSTIPOPER["USAVLTOTALITEM"].ToString() == "E")
                {
                    DataGrid.Columns["VLTOTALITEM"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAVLTOTALITEM"].ToString() == "N")
                {
                    DataGrid.Columns["VLTOTALITEM"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAVLTOTALITEM"].ToString() == "M")
                {
                    DataGrid.Columns["VLTOTALITEM"].Visible = false;
                }

                #endregion

                #region Natureza

                //Edita
                if (PARAMNSTIPOPER["USANATUREZA"].ToString() == "E")
                {
                    DataGrid.Columns["CODNATUREZA"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USANATUREZA"].ToString() == "N")
                {
                    DataGrid.Columns["CODNATUREZA"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USANATUREZA"].ToString() == "M")
                {
                    DataGrid.Columns["CODNATUREZA"].Visible = false;
                }

                #endregion

                /*
             *  CAMPOS LIVRES
             *  DATA EXTRA
             *  CAMPOS VALOR
             */

                #region Texto dos Campos

                DataGrid.Columns["CAMPOLIVRE1"].HeaderText = PARAMNSTIPOPER["TEXTOITEMCAMPOLIVRE1"].ToString();
                DataGrid.Columns["CAMPOLIVRE2"].HeaderText = PARAMNSTIPOPER["TEXTOITEMCAMPOLIVRE2"].ToString();
                DataGrid.Columns["CAMPOLIVRE3"].HeaderText = PARAMNSTIPOPER["TEXTOITEMCAMPOLIVRE3"].ToString();
                DataGrid.Columns["CAMPOLIVRE4"].HeaderText = PARAMNSTIPOPER["TEXTOITEMCAMPOLIVRE4"].ToString();
                DataGrid.Columns["CAMPOLIVRE5"].HeaderText = PARAMNSTIPOPER["TEXTOITEMCAMPOLIVRE5"].ToString();
                DataGrid.Columns["CAMPOLIVRE6"].HeaderText = PARAMNSTIPOPER["TEXTOITEMCAMPOLIVRE6"].ToString();

                DataGrid.Columns["DATAEXTRA1"].HeaderText = PARAMNSTIPOPER["TEXTOITEMDATAEXTRA1"].ToString();
                DataGrid.Columns["DATAEXTRA2"].HeaderText = PARAMNSTIPOPER["TEXTOITEMDATAEXTRA2"].ToString();
                DataGrid.Columns["DATAEXTRA3"].HeaderText = PARAMNSTIPOPER["TEXTOITEMDATAEXTRA3"].ToString();
                DataGrid.Columns["DATAEXTRA4"].HeaderText = PARAMNSTIPOPER["TEXTOITEMDATAEXTRA4"].ToString();
                DataGrid.Columns["DATAEXTRA5"].HeaderText = PARAMNSTIPOPER["TEXTOITEMDATAEXTRA5"].ToString();
                DataGrid.Columns["DATAEXTRA6"].HeaderText = PARAMNSTIPOPER["TEXTOITEMDATAEXTRA6"].ToString();

                DataGrid.Columns["CAMPOVALOR1"].HeaderText = PARAMNSTIPOPER["TEXTOITEMCAMPOVALOR1"].ToString();
                DataGrid.Columns["CAMPOVALOR2"].HeaderText = PARAMNSTIPOPER["TEXTOITEMCAMPOVALOR2"].ToString();
                DataGrid.Columns["CAMPOVALOR3"].HeaderText = PARAMNSTIPOPER["TEXTOITEMCAMPOVALOR3"].ToString();
                DataGrid.Columns["CAMPOVALOR4"].HeaderText = PARAMNSTIPOPER["TEXTOITEMCAMPOVALOR4"].ToString();
                DataGrid.Columns["CAMPOVALOR5"].HeaderText = PARAMNSTIPOPER["TEXTOITEMCAMPOVALOR5"].ToString();
                DataGrid.Columns["CAMPOVALOR6"].HeaderText = PARAMNSTIPOPER["TEXTOITEMCAMPOVALOR6"].ToString();

                #endregion

                #region Campo Livre 1

                //Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE1"].ToString() == "E")
                {
                    DataGrid.Columns["CAMPOLIVRE1"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE1"].ToString() == "N")
                {
                    DataGrid.Columns["CAMPOLIVRE1"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE1"].ToString() == "M")
                {
                    DataGrid.Columns["CAMPOLIVRE1"].Visible = false;
                }

                #endregion

                #region Campo Livre 2

                //Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE2"].ToString() == "E")
                {
                    DataGrid.Columns["CAMPOLIVRE2"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE2"].ToString() == "N")
                {
                    DataGrid.Columns["CAMPOLIVRE2"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE2"].ToString() == "M")
                {
                    DataGrid.Columns["CAMPOLIVRE2"].Visible = false;
                }

                #endregion

                #region Campo Livre 3

                //Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE3"].ToString() == "E")
                {
                    DataGrid.Columns["CAMPOLIVRE3"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE3"].ToString() == "N")
                {
                    DataGrid.Columns["CAMPOLIVRE3"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE3"].ToString() == "M")
                {
                    DataGrid.Columns["CAMPOLIVRE3"].Visible = false;
                }

                #endregion

                #region Campo Livre 4

                //Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE4"].ToString() == "E")
                {
                    DataGrid.Columns["CAMPOLIVRE4"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE4"].ToString() == "N")
                {
                    DataGrid.Columns["CAMPOLIVRE4"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE4"].ToString() == "M")
                {
                    DataGrid.Columns["CAMPOLIVRE4"].Visible = false;
                }

                #endregion

                #region Campo Livre 5

                //Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE5"].ToString() == "E")
                {
                    DataGrid.Columns["CAMPOLIVRE5"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE5"].ToString() == "N")
                {
                    DataGrid.Columns["CAMPOLIVRE5"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE5"].ToString() == "M")
                {
                    DataGrid.Columns["CAMPOLIVRE5"].Visible = false;
                }

                #endregion

                #region Campo Livre 6

                //Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE6"].ToString() == "E")
                {
                    DataGrid.Columns["CAMPOLIVRE6"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE6"].ToString() == "N")
                {
                    DataGrid.Columns["CAMPOLIVRE6"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMCAMPOLIVRE6"].ToString() == "M")
                {
                    DataGrid.Columns["CAMPOLIVRE6"].Visible = false;
                }

                #endregion

                #region Data Extra 1

                //Edita
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA1"].ToString() == "E")
                {
                    DataGrid.Columns["DATAEXTRA1"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA1"].ToString() == "N")
                {
                    DataGrid.Columns["DATAEXTRA1"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA1"].ToString() == "M")
                {
                    DataGrid.Columns["DATAEXTRA1"].Visible = false;
                }

                #endregion

                #region Data Extra 2

                //Edita
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA2"].ToString() == "E")
                {
                    DataGrid.Columns["DATAEXTRA2"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA2"].ToString() == "N")
                {
                    DataGrid.Columns["DATAEXTRA2"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA2"].ToString() == "M")
                {
                    DataGrid.Columns["DATAEXTRA2"].Visible = false;
                }

                #endregion

                #region Data Extra 3

                //Edita
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA3"].ToString() == "E")
                {
                    DataGrid.Columns["DATAEXTRA3"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA3"].ToString() == "N")
                {
                    DataGrid.Columns["DATAEXTRA3"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA3"].ToString() == "M")
                {
                    DataGrid.Columns["DATAEXTRA3"].Visible = false;
                }

                #endregion

                #region Data Extra 4

                //Edita
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA4"].ToString() == "E")
                {
                    DataGrid.Columns["DATAEXTRA4"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA4"].ToString() == "N")
                {
                    DataGrid.Columns["DATAEXTRA4"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA4"].ToString() == "M")
                {
                    DataGrid.Columns["DATAEXTRA4"].Visible = false;
                }

                #endregion

                #region Data Extra 5

                //Edita
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA5"].ToString() == "E")
                {
                    DataGrid.Columns["DATAEXTRA5"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA5"].ToString() == "N")
                {
                    DataGrid.Columns["DATAEXTRA5"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA5"].ToString() == "M")
                {
                    DataGrid.Columns["DATAEXTRA5"].Visible = false;
                }

                #endregion

                #region Data Extra 6

                //Edita
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA6"].ToString() == "E")
                {
                    DataGrid.Columns["DATAEXTRA6"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA6"].ToString() == "N")
                {
                    DataGrid.Columns["DATAEXTRA6"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMDATAEXTRA6"].ToString() == "M")
                {
                    DataGrid.Columns["DATAEXTRA6"].Visible = false;
                }

                #endregion

                #region Campo Valor 1

                //Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR1"].ToString() == "E")
                {
                    DataGrid.Columns["CAMPOVALOR1"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR1"].ToString() == "N")
                {
                    DataGrid.Columns["CAMPOVALOR1"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR1"].ToString() == "M")
                {
                    DataGrid.Columns["CAMPOVALOR1"].Visible = false;
                }

                #endregion

                #region Campo Valor 2

                //Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR2"].ToString() == "E")
                {
                    DataGrid.Columns["CAMPOVALOR2"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR2"].ToString() == "N")
                {
                    DataGrid.Columns["CAMPOVALOR2"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR2"].ToString() == "M")
                {
                    DataGrid.Columns["CAMPOVALOR2"].Visible = false;
                }

                #endregion

                #region Campo Valor 3

                //Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR3"].ToString() == "E")
                {
                    DataGrid.Columns["CAMPOVALOR3"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR3"].ToString() == "N")
                {
                    DataGrid.Columns["CAMPOVALOR3"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR3"].ToString() == "M")
                {
                    DataGrid.Columns["CAMPOVALOR3"].Visible = false;
                }

                #endregion

                #region Campo Valor 4

                //Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR4"].ToString() == "E")
                {
                    DataGrid.Columns["CAMPOVALOR4"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR4"].ToString() == "N")
                {
                    DataGrid.Columns["CAMPOVALOR4"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR4"].ToString() == "M")
                {
                    DataGrid.Columns["CAMPOVALOR4"].Visible = false;
                }

                #endregion

                #region Campo Valor 5

                //Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR5"].ToString() == "E")
                {
                    DataGrid.Columns["CAMPOVALOR5"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR5"].ToString() == "N")
                {
                    DataGrid.Columns["CAMPOVALOR5"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR5"].ToString() == "M")
                {
                    DataGrid.Columns["CAMPOVALOR5"].Visible = false;
                }

                #endregion

                #region Campo Valor 6

                //Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR6"].ToString() == "E")
                {
                    DataGrid.Columns["CAMPOVALOR6"].Visible = true;
                }

                //Não Edita
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR6"].ToString() == "N")
                {
                    DataGrid.Columns["CAMPOVALOR6"].Visible = true;
                }

                //Não Mostra
                if (PARAMNSTIPOPER["USAITEMCAMPOVALOR6"].ToString() == "M")
                {
                    DataGrid.Columns["CAMPOVALOR6"].Visible = false;
                }

                #endregion
            }
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            List<ComboBoxItem> ItemCLASSVENDA2 = new List<ComboBoxItem>();
            ItemCLASSVENDA2.Add(new ComboBoxItem("V", "Venda"));
            ItemCLASSVENDA2.Add(new ComboBoxItem("R", "Revenda"));
            ItemCLASSVENDA2.Add(new ComboBoxItem("C", "Comsumo"));
            objArr.Add(new DataField("APLICACAOMATERIAL", null, typeof(PS.Lib.WinForms.PSComboBox), ItemCLASSVENDA2));

            return objArr;
        }
    }
}
