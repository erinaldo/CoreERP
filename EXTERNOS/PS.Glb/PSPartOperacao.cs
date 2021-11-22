using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartOperacao : PS.Lib.WinForms.PSPartGridView
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public PSPartOperacao()
        {
            this.TableName = "GOPER";
            this.Keys = new string[] { "CODEMPRESA", "CODOPER" };

            this.FormEditName = "PSPartOperacaoEdit";
            this.PSPartData = new PSPartOperacaoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            #region Custom Column

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("HISTORICO", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("OBSERVACAO", false));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VALORBRUTO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VALORLIQUIDO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VALORFRETE",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CCODSTATUS",
                                                                                "",
                                                                                PS.Lib.DataGridColumnType.Image,
                                                                                1,
                                                                                "CODSTATUS",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter,
                                                                                new PS.Lib.ImageProperties[] { new PS.Lib.ImageProperties(0, "Aberto", Properties.Resources.oper_aberto),
                                                                                                               new PS.Lib.ImageProperties(1, "Faturado", Properties.Resources.oper_faturado),
                                                                                                               new PS.Lib.ImageProperties(2, "Cancelado", Properties.Resources.oper_cancelado),
                                                                                                               new PS.Lib.ImageProperties(3, "Parcialmente Quitado", Properties.Resources.oper_parcquitado),
                                                                                                               new PS.Lib.ImageProperties(4, "Quitado", Properties.Resources.oper_quitado),
                                                                                                               new PS.Lib.ImageProperties(5, "Parcialmente Faturado", Properties.Resources.parcialmentefaturado),
                                                                                                               new PS.Lib.ImageProperties(6, "Parcialmente Faturado / Finalizado", Properties.Resources.cancelado),
                                                                                                               new PS.Lib.ImageProperties(7, "Bloqueado", Properties.Resources.bloqueado)
                                                                                }));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CSTATUSNFE",
                                                                   "",
                                                                   PS.Lib.DataGridColumnType.Image,
                                                                   1,
                                                                   "STATUSNFE",
                                                                   System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter,
                                                                   new PS.Lib.ImageProperties[] { 
                                                                        new PS.Lib.ImageProperties("P", "Pendente", Properties.Resources.nf_warning),
                                                                        new PS.Lib.ImageProperties("U", "Aguardando Processamento", Properties.Resources.nf_wait),
                                                                        new PS.Lib.ImageProperties("E", "Inconsistente", Properties.Resources.nf_erro),
                                                                        new PS.Lib.ImageProperties("A", "Autorizada", Properties.Resources.nf_ok),
                                                                        new PS.Lib.ImageProperties("C", "Cancelada", Properties.Resources.nf_cancel),
                                                                        new PS.Lib.ImageProperties("I", "Inutilizado", Properties.Resources.nf_inutilizado),
                                                                        new PS.Lib.ImageProperties("D", "Denegado", Properties.Resources.nf_denegado),
                                                                                }));

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

            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartOperacaoItem(), true, "CODEMPRESA", "CODOPER"));
            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartOperRateioCC(), "CODEMPRESA", "CODOPER"));
            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartOperRateioDP(), "CODEMPRESA", "CODOPER"));

            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartOperCompl(), "CODEMPRESA", "CODOPER"));
            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartLanca(), "CODEMPRESA", "CODOPER"));
            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartNFEstadual(), "CODEMPRESA", "CODOPER"));

            this.PSPartApp.Add(new PS.Glb.PSPartOperFatApp());
            this.PSPartApp.Add(new PS.Glb.PSPartOperCanFatApp());
            this.PSPartApp.Add(new PS.Glb.PSPartRastreiaOperApp());
            this.PSPartApp.Add(new PS.Glb.PSPartGerarNFeApp());
            
            this.PSPartApp.Add(new PS.Glb.PSPartAjustarValorFinanceiroApp());
            
            
            this.PSPartApp.Add(new PS.Glb.PSPartCopiarOperApp());
            this.PSPartApp.Add(new PS.Glb.PSPartConcluirOperacaoApp());
            this.PSPartApp.Add(new PS.Glb.PSPartAprovaDescontoApp());

            this.SecurityID = "PSPartOperacao";
            this.ModuleID = "PG";

            //this.f.GetProcessos().Add("Concluir Operação", null, ConcluirOperacao);

            this.f.GetAnexos().Add(new System.Windows.Forms.ToolStripSeparator());
            this.f.GetAnexos().Add("Devolução", null, Devolucao);
            this.f.GetAnexos().Add("Imprimir Operação", null, ImprimirOperacao);
        }
       
        public override void CustomDataGridView(System.Windows.Forms.DataGridView DataGrid)
        {
            base.CustomDataGridView(DataGrid);

            DataField dfCODTIPOPER = gb.RetornaDataFieldByFilter(this.DefaultFilter, "CODTIPOPER");
            DataRow PARAMNSTIPOPER = gb.RetornaParametrosOperacao((dfCODTIPOPER.Valor == null) ? null : dfCODTIPOPER.Valor.ToString());

            if (PARAMNSTIPOPER == null)
            {
                throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação selecionada.");
            }

            //Fazer a verificação se a grid esta vazia ou não para não apresentar erro.
            if (DataGrid.Rows.Count <= 0)
                return;

            #region Cliente/Fornecedor

            //Edita
            if (PARAMNSTIPOPER["CODCLIFOR"].ToString() == "E")
            {
                DataGrid.Columns["CODCLIFOR"].Visible = true;
                DataGrid.Columns["CNOMECLIFOR"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["CODCLIFOR"].ToString() == "N")
            {
                DataGrid.Columns["CODCLIFOR"].Visible = true;
                DataGrid.Columns["CNOMECLIFOR"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["CODCLIFOR"].ToString() == "M")
            {
                DataGrid.Columns["CODCLIFOR"].Visible = false;
                DataGrid.Columns["CNOMECLIFOR"].Visible = false;
            }
            DataGrid.Columns["PRCOMISSAO"].Visible = false;
            #endregion

            #region Série

            //Edita
            if (PARAMNSTIPOPER["OPERSERIE"].ToString() == "E")
            {
                DataGrid.Columns["CODSERIE"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["OPERSERIE"].ToString() == "N")
            {
                DataGrid.Columns["CODSERIE"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["OPERSERIE"].ToString() == "M")
            {
                DataGrid.Columns["CODSERIE"].Visible = false;
            }

            #endregion

            #region Natureza

            //Padrão Não Modificar
            DataGrid.Columns["CODNATUREZA"].Visible = false;

            /*

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
            
            */

            #endregion

            #region Local 1

            //Edita
            if (PARAMNSTIPOPER["LOCAL1"].ToString() == "E")
            {
                DataGrid.Columns["CODLOCAL"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["LOCAL1"].ToString() == "N")
            {
                DataGrid.Columns["CODLOCAL"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["LOCAL1"].ToString() == "M")
            {
                DataGrid.Columns["CODLOCAL"].Visible = false;
            }

            #endregion

            #region Local 2

            //Edita
            if (PARAMNSTIPOPER["LOCAL2"].ToString() == "E")
            {
                DataGrid.Columns["CODLOCALENTREGA"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["LOCAL2"].ToString() == "N")
            {
                DataGrid.Columns["CODLOCALENTREGA"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["LOCAL2"].ToString() == "M")
            {
                DataGrid.Columns["CODLOCALENTREGA"].Visible = false;
            }

            #endregion

            #region Objeto

            //Edita
            if (PARAMNSTIPOPER["USACAMPOOBJETO"].ToString() == "E")
            {
                DataGrid.Columns["CODOBJETO"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACAMPOOBJETO"].ToString() == "N")
            {
                DataGrid.Columns["CODOBJETO"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACAMPOOBJETO"].ToString() == "M")
            {
                DataGrid.Columns["CODOBJETO"].Visible = false;
            }

            #endregion

            #region Operador

            //Edita
            if (PARAMNSTIPOPER["USACAMPOOPERADOR"].ToString() == "E")
            {
                DataGrid.Columns["CODOPERADOR"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACAMPOOPERADOR"].ToString() == "N")
            {
                DataGrid.Columns["CODOPERADOR"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACAMPOOPERADOR"].ToString() == "M")
            {
                DataGrid.Columns["CODOPERADOR"].Visible = false;
            }

            #endregion

            #region Data Emissão

            //Edita
            if (PARAMNSTIPOPER["USADATAEMISSAO"].ToString() == "E")
            {
                DataGrid.Columns["DATAEMISSAO"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USADATAEMISSAO"].ToString() == "N")
            {
                DataGrid.Columns["DATAEMISSAO"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USADATAEMISSAO"].ToString() == "M")
            {
                DataGrid.Columns["DATAEMISSAO"].Visible = false;
            }

            #endregion

            #region Data Entrada/Saida

            //Edita
            if (PARAMNSTIPOPER["USADATAENTSAI"].ToString() == "E")
            {
                DataGrid.Columns["DATAENTSAI"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USADATAENTSAI"].ToString() == "N")
            {
                DataGrid.Columns["DATAENTSAI"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USADATAENTSAI"].ToString() == "M")
            {
                DataGrid.Columns["DATAENTSAI"].Visible = false;
            }

            #endregion

            #region Data Entrega

            //Edita
            if (PARAMNSTIPOPER["USADATAENTREGA"].ToString() == "E")
            {
                DataGrid.Columns["DATAENTREGA"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USADATAENTREGA"].ToString() == "N")
            {
                DataGrid.Columns["DATAENTREGA"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USADATAENTREGA"].ToString() == "M")
            {
                DataGrid.Columns["DATAENTREGA"].Visible = false;
            }

            #endregion

            #region Condição de Pagamento

            //Edita
            if (PARAMNSTIPOPER["USACAMPOCONDPGTO"].ToString() == "E")
            {
                DataGrid.Columns["CODCONDICAO"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACAMPOCONDPGTO"].ToString() == "N")
            {
                DataGrid.Columns["CODCONDICAO"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACAMPOCONDPGTO"].ToString() == "M")
            {
                DataGrid.Columns["CODCONDICAO"].Visible = false;
            }

            #endregion

            #region Forma de Pagamento

            //Edita
            if (PARAMNSTIPOPER["CODFORMA"].ToString() == "E")
            {
                DataGrid.Columns["CODFORMA"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["CODFORMA"].ToString() == "N")
            {
                DataGrid.Columns["CODFORMA"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["CODFORMA"].ToString() == "M")
            {
                DataGrid.Columns["CODFORMA"].Visible = false;
            }

            #endregion

            #region Valor Bruto

            //Edita
            if (PARAMNSTIPOPER["USAVALORBRUTO"].ToString() == "E")
            {
                DataGrid.Columns["VALORBRUTO"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USAVALORBRUTO"].ToString() == "N")
            {
                DataGrid.Columns["VALORBRUTO"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USAVALORBRUTO"].ToString() == "M")
            {
                DataGrid.Columns["VALORBRUTO"].Visible = false;
            }

            #endregion

            #region Valor Liquido

            //Edita
            if (PARAMNSTIPOPER["USAVALORLIQUIDO"].ToString() == "E")
            {
                DataGrid.Columns["VALORLIQUIDO"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USAVALORLIQUIDO"].ToString() == "N")
            {
                DataGrid.Columns["VALORLIQUIDO"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USAVALORLIQUIDO"].ToString() == "M")
            {
                DataGrid.Columns["VALORLIQUIDO"].Visible = false;
            }

            #endregion

            #region Usa Aba Transporte

            //Mostra
            if (PARAMNSTIPOPER["USAABATRANSP"].ToString() == "1")
            {
                DataGrid.Columns["FRETECIFFOB"].Visible = true;
                DataGrid.Columns["CODTRANSPORTADORA"].Visible = true;
                DataGrid.Columns["QUANTIDADE"].Visible = true;
                DataGrid.Columns["PESOLIQUIDO"].Visible = true;
                DataGrid.Columns["PESOBRUTO"].Visible = true;
                DataGrid.Columns["ESPECIE"].Visible = true;
                DataGrid.Columns["MARCA"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USAABATRANSP"].ToString() == "0")
            {
                DataGrid.Columns["FRETECIFFOB"].Visible = false;
                DataGrid.Columns["CODTRANSPORTADORA"].Visible = false;
                DataGrid.Columns["QUANTIDADE"].Visible = false;
                DataGrid.Columns["PESOLIQUIDO"].Visible = false;
                DataGrid.Columns["PESOBRUTO"].Visible = false;
                DataGrid.Columns["ESPECIE"].Visible = false;
                DataGrid.Columns["MARCA"].Visible = false;
            }

            #endregion

            #region Conta Caixa

            //Edita
            if (PARAMNSTIPOPER["USACONTA"].ToString() == "E")
            {
                DataGrid.Columns["CODCONTA"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACONTA"].ToString() == "N")
            {
                DataGrid.Columns["CODCONTA"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACONTA"].ToString() == "M")
            {
                DataGrid.Columns["CODCONTA"].Visible = false;
            }

            #endregion

            #region Representante

            //Edita
            if (PARAMNSTIPOPER["USACODREPRE"].ToString() == "E")
            {
                DataGrid.Columns["CODREPRE"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACODREPRE"].ToString() == "N")
            {
                DataGrid.Columns["CODREPRE"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACODREPRE"].ToString() == "M")
            {
                DataGrid.Columns["CODREPRE"].Visible = false;
            }

            #endregion

            #region Centro de Custo

            //Edita
            if (PARAMNSTIPOPER["USACODCCUSTO"].ToString() == "E")
            {
                DataGrid.Columns["CODCCUSTO"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACODCCUSTO"].ToString() == "N")
            {
                DataGrid.Columns["CODCCUSTO"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACODCCUSTO"].ToString() == "M")
            {
                DataGrid.Columns["CODCCUSTO"].Visible = false;
            }

            #endregion

            #region Natureza Orçamentária

            //Edita
            if (PARAMNSTIPOPER["USACODNATUREZAORCAMENTO"].ToString() == "E")
            {
                DataGrid.Columns["CODNATUREZAORCAMENTO"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACODNATUREZAORCAMENTO"].ToString() == "N")
            {
                DataGrid.Columns["CODNATUREZAORCAMENTO"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACODNATUREZAORCAMENTO"].ToString() == "M")
            {
                DataGrid.Columns["CODNATUREZAORCAMENTO"].Visible = false;
            }

            #endregion

            /*
             *  CAMPOS LIVRES
             *  DATA EXTRA
             *  CAMPOS VALOR
             */

            #region Texto dos Campos

            DataGrid.Columns["CAMPOLIVRE1"].HeaderText = PARAMNSTIPOPER["TEXTOCAMPOLIVRE1"].ToString();
            DataGrid.Columns["CAMPOLIVRE2"].HeaderText = PARAMNSTIPOPER["TEXTOCAMPOLIVRE2"].ToString();
            DataGrid.Columns["CAMPOLIVRE3"].HeaderText = PARAMNSTIPOPER["TEXTOCAMPOLIVRE3"].ToString();
            DataGrid.Columns["CAMPOLIVRE4"].HeaderText = PARAMNSTIPOPER["TEXTOCAMPOLIVRE4"].ToString();
            DataGrid.Columns["CAMPOLIVRE5"].HeaderText = PARAMNSTIPOPER["TEXTOCAMPOLIVRE5"].ToString();
            DataGrid.Columns["CAMPOLIVRE6"].HeaderText = PARAMNSTIPOPER["TEXTOCAMPOLIVRE6"].ToString();

            DataGrid.Columns["DATAEXTRA1"].HeaderText = PARAMNSTIPOPER["TEXTODATAEXTRA1"].ToString();
            DataGrid.Columns["DATAEXTRA2"].HeaderText = PARAMNSTIPOPER["TEXTODATAEXTRA2"].ToString();
            DataGrid.Columns["DATAEXTRA3"].HeaderText = PARAMNSTIPOPER["TEXTODATAEXTRA3"].ToString();
            DataGrid.Columns["DATAEXTRA4"].HeaderText = PARAMNSTIPOPER["TEXTODATAEXTRA4"].ToString();
            DataGrid.Columns["DATAEXTRA5"].HeaderText = PARAMNSTIPOPER["TEXTODATAEXTRA5"].ToString();
            DataGrid.Columns["DATAEXTRA6"].HeaderText = PARAMNSTIPOPER["TEXTODATAEXTRA6"].ToString();

            DataGrid.Columns["CAMPOVALOR1"].HeaderText = PARAMNSTIPOPER["TEXTOCAMPOVALOR1"].ToString();
            DataGrid.Columns["CAMPOVALOR2"].HeaderText = PARAMNSTIPOPER["TEXTOCAMPOVALOR2"].ToString();
            DataGrid.Columns["CAMPOVALOR3"].HeaderText = PARAMNSTIPOPER["TEXTOCAMPOVALOR3"].ToString();
            DataGrid.Columns["CAMPOVALOR4"].HeaderText = PARAMNSTIPOPER["TEXTOCAMPOVALOR4"].ToString();
            DataGrid.Columns["CAMPOVALOR5"].HeaderText = PARAMNSTIPOPER["TEXTOCAMPOVALOR5"].ToString();
            DataGrid.Columns["CAMPOVALOR6"].HeaderText = PARAMNSTIPOPER["TEXTOCAMPOVALOR6"].ToString();

            #endregion

            #region Campo Livre 1

            //Edita
            if (PARAMNSTIPOPER["USACAMPOLIVRE1"].ToString() == "E")
            {
                DataGrid.Columns["CAMPOLIVRE1"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACAMPOLIVRE1"].ToString() == "N")
            {
                DataGrid.Columns["CAMPOLIVRE1"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACAMPOLIVRE1"].ToString() == "M")
            {
                DataGrid.Columns["CAMPOLIVRE1"].Visible = false;
            }

            #endregion

            #region Campo Livre 2

            //Edita
            if (PARAMNSTIPOPER["USACAMPOLIVRE2"].ToString() == "E")
            {
                DataGrid.Columns["CAMPOLIVRE2"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACAMPOLIVRE2"].ToString() == "N")
            {
                DataGrid.Columns["CAMPOLIVRE2"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACAMPOLIVRE2"].ToString() == "M")
            {
                DataGrid.Columns["CAMPOLIVRE2"].Visible = false;
            }

            #endregion

            #region Campo Livre 3

            //Edita
            if (PARAMNSTIPOPER["USACAMPOLIVRE3"].ToString() == "E")
            {
                DataGrid.Columns["CAMPOLIVRE3"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACAMPOLIVRE3"].ToString() == "N")
            {
                DataGrid.Columns["CAMPOLIVRE3"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACAMPOLIVRE3"].ToString() == "M")
            {
                DataGrid.Columns["CAMPOLIVRE3"].Visible = false;
            }

            #endregion

            #region Campo Livre 4

            //Edita
            if (PARAMNSTIPOPER["USACAMPOLIVRE4"].ToString() == "E")
            {
                DataGrid.Columns["CAMPOLIVRE4"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACAMPOLIVRE4"].ToString() == "N")
            {
                DataGrid.Columns["CAMPOLIVRE4"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACAMPOLIVRE4"].ToString() == "M")
            {
                DataGrid.Columns["CAMPOLIVRE4"].Visible = false;
            }

            #endregion

            #region Campo Livre 5

            //Edita
            if (PARAMNSTIPOPER["USACAMPOLIVRE5"].ToString() == "E")
            {
                DataGrid.Columns["CAMPOLIVRE5"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACAMPOLIVRE5"].ToString() == "N")
            {
                DataGrid.Columns["CAMPOLIVRE5"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACAMPOLIVRE5"].ToString() == "M")
            {
                DataGrid.Columns["CAMPOLIVRE5"].Visible = false;
            }

            #endregion

            #region Campo Livre 6

            //Edita
            if (PARAMNSTIPOPER["USACAMPOLIVRE6"].ToString() == "E")
            {
                DataGrid.Columns["CAMPOLIVRE6"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACAMPOLIVRE6"].ToString() == "N")
            {
                DataGrid.Columns["CAMPOLIVRE6"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACAMPOLIVRE6"].ToString() == "M")
            {
                DataGrid.Columns["CAMPOLIVRE6"].Visible = false;
            }

            #endregion

            #region Data Extra 1

            //Edita
            if (PARAMNSTIPOPER["USADATAEXTRA1"].ToString() == "E")
            {
                DataGrid.Columns["DATAEXTRA1"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USADATAEXTRA1"].ToString() == "N")
            {
                DataGrid.Columns["DATAEXTRA1"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USADATAEXTRA1"].ToString() == "M")
            {
                DataGrid.Columns["DATAEXTRA1"].Visible = false;
            }

            #endregion

            #region Data Extra 2

            //Edita
            if (PARAMNSTIPOPER["USADATAEXTRA2"].ToString() == "E")
            {
                DataGrid.Columns["DATAEXTRA2"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USADATAEXTRA2"].ToString() == "N")
            {
                DataGrid.Columns["DATAEXTRA2"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USADATAEXTRA2"].ToString() == "M")
            {
                DataGrid.Columns["DATAEXTRA2"].Visible = false;
            }

            #endregion

            #region Data Extra 3

            //Edita
            if (PARAMNSTIPOPER["USADATAEXTRA3"].ToString() == "E")
            {
                DataGrid.Columns["DATAEXTRA3"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USADATAEXTRA3"].ToString() == "N")
            {
                DataGrid.Columns["DATAEXTRA3"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USADATAEXTRA3"].ToString() == "M")
            {
                DataGrid.Columns["DATAEXTRA3"].Visible = false;
            }

            #endregion

            #region Data Extra 4

            //Edita
            if (PARAMNSTIPOPER["USADATAEXTRA4"].ToString() == "E")
            {
                DataGrid.Columns["DATAEXTRA4"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USADATAEXTRA4"].ToString() == "N")
            {
                DataGrid.Columns["DATAEXTRA4"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USADATAEXTRA4"].ToString() == "M")
            {
                DataGrid.Columns["DATAEXTRA4"].Visible = false;
            }

            #endregion

            #region Data Extra 5

            //Edita
            if (PARAMNSTIPOPER["USADATAEXTRA5"].ToString() == "E")
            {
                DataGrid.Columns["DATAEXTRA5"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USADATAEXTRA5"].ToString() == "N")
            {
                DataGrid.Columns["DATAEXTRA5"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USADATAEXTRA5"].ToString() == "M")
            {
                DataGrid.Columns["DATAEXTRA5"].Visible = false;
            }

            #endregion

            #region Data Extra 6

            //Edita
            if (PARAMNSTIPOPER["USADATAEXTRA6"].ToString() == "E")
            {
                DataGrid.Columns["DATAEXTRA6"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USADATAEXTRA6"].ToString() == "N")
            {
                DataGrid.Columns["DATAEXTRA6"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USADATAEXTRA6"].ToString() == "M")
            {
                DataGrid.Columns["DATAEXTRA6"].Visible = false;
            }

            #endregion

            //ABA VALORES

            #region Frete

            //Edita
            if (PARAMNSTIPOPER["USAVALORFRETE"].ToString() == "E")
            {
                DataGrid.Columns["VALORFRETE"].Visible = true;
                DataGrid.Columns["PERCFRETE"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USAVALORFRETE"].ToString() == "N")
            {
                DataGrid.Columns["VALORFRETE"].Visible = true;
                DataGrid.Columns["PERCFRETE"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USAVALORFRETE"].ToString() == "M")
            {
                DataGrid.Columns["VALORFRETE"].Visible = false;
                DataGrid.Columns["PERCFRETE"].Visible = false;
            }

            #endregion

            #region Desconto

            //Edita
            if (PARAMNSTIPOPER["USAVALORDESCONTO"].ToString() == "E")
            {
                DataGrid.Columns["VALORDESCONTO"].Visible = true;
                DataGrid.Columns["PERCDESCONTO"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USAVALORDESCONTO"].ToString() == "N")
            {
                DataGrid.Columns["VALORDESCONTO"].Visible = true;
                DataGrid.Columns["PERCDESCONTO"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USAVALORDESCONTO"].ToString() == "M")
            {
                DataGrid.Columns["VALORDESCONTO"].Visible = false;
                DataGrid.Columns["PERCDESCONTO"].Visible = false;
            }

            #endregion

            #region Despesa

            //Edita
            if (PARAMNSTIPOPER["USAVALORDESPESA"].ToString() == "E")
            {
                DataGrid.Columns["VALORDESCONTO"].Visible = true;
                DataGrid.Columns["PERCDESCONTO"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USAVALORDESPESA"].ToString() == "N")
            {
                DataGrid.Columns["VALORDESCONTO"].Visible = true;
                DataGrid.Columns["PERCDESCONTO"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USAVALORDESPESA"].ToString() == "M")
            {
                DataGrid.Columns["VALORDESCONTO"].Visible = false;
                DataGrid.Columns["PERCDESCONTO"].Visible = false;
            }

            #endregion

            #region Seguro

            //Edita
            if (PARAMNSTIPOPER["USAVALORSEGURO"].ToString() == "E")
            {
                DataGrid.Columns["VALORSEGURO"].Visible = true;
                DataGrid.Columns["PERCSEGURO"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USAVALORSEGURO"].ToString() == "N")
            {
                DataGrid.Columns["VALORSEGURO"].Visible = true;
                DataGrid.Columns["PERCSEGURO"].Visible = true;

            }

            //Não Mostra
            if (PARAMNSTIPOPER["USAVALORSEGURO"].ToString() == "M")
            {
                DataGrid.Columns["VALORSEGURO"].Visible = false;
                DataGrid.Columns["PERCSEGURO"].Visible = false;
            }

            #endregion

            #region Campo Valor 1

            //Edita
            if (PARAMNSTIPOPER["USACAMPOVALOR1"].ToString() == "E")
            {
                DataGrid.Columns["CAMPOVALOR1"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACAMPOVALOR1"].ToString() == "N")
            {
                DataGrid.Columns["CAMPOVALOR1"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACAMPOVALOR1"].ToString() == "M")
            {
                DataGrid.Columns["CAMPOVALOR1"].Visible = false;
            }

            #endregion

            #region Campo Valor 2

            //Edita
            if (PARAMNSTIPOPER["USACAMPOVALOR2"].ToString() == "E")
            {
                DataGrid.Columns["CAMPOVALOR2"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACAMPOVALOR2"].ToString() == "N")
            {
                DataGrid.Columns["CAMPOVALOR2"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACAMPOVALOR2"].ToString() == "M")
            {
                DataGrid.Columns["CAMPOVALOR2"].Visible = false;
            }

            #endregion

            #region Campo Valor 3

            //Edita
            if (PARAMNSTIPOPER["USACAMPOVALOR3"].ToString() == "E")
            {
                DataGrid.Columns["CAMPOVALOR3"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACAMPOVALOR3"].ToString() == "N")
            {
                DataGrid.Columns["CAMPOVALOR3"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACAMPOVALOR3"].ToString() == "M")
            {
                DataGrid.Columns["CAMPOVALOR3"].Visible = false;
            }

            #endregion

            #region Campo Valor 4

            //Edita
            if (PARAMNSTIPOPER["USACAMPOVALOR4"].ToString() == "E")
            {
                DataGrid.Columns["CAMPOVALOR4"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACAMPOVALOR4"].ToString() == "N")
            {
                DataGrid.Columns["CAMPOVALOR4"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACAMPOVALOR4"].ToString() == "M")
            {
                DataGrid.Columns["CAMPOVALOR4"].Visible = false;
            }

            #endregion

            #region Campo Valor 5

            //Edita
            if (PARAMNSTIPOPER["USACAMPOVALOR5"].ToString() == "E")
            {
                DataGrid.Columns["CAMPOVALOR5"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACAMPOVALOR5"].ToString() == "N")
            {
                DataGrid.Columns["CAMPOVALOR5"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACAMPOVALOR5"].ToString() == "M")
            {
                DataGrid.Columns["CAMPOVALOR5"].Visible = false;
            }

            #endregion

            #region Campo Valor 6

            //Edita
            if (PARAMNSTIPOPER["USACAMPOVALOR6"].ToString() == "E")
            {
                DataGrid.Columns["CAMPOVALOR6"].Visible = true;
            }

            //Não Edita
            if (PARAMNSTIPOPER["USACAMPOVALOR6"].ToString() == "N")
            {
                DataGrid.Columns["CAMPOVALOR6"].Visible = true;
            }

            //Não Mostra
            if (PARAMNSTIPOPER["USACAMPOVALOR6"].ToString() == "M")
            {
                DataGrid.Columns["CAMPOVALOR6"].Visible = false;
            }

            #endregion
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("VALORFRETE", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("PERCFRETE", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("VALORDESCONTO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("PERCDESCONTO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("VALORDESPESA", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("PERCDESPESA", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("VALORSEGURO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("PERCSEGURO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));

            objArr.Add(new DataField("VALORBRUTO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("VALORLIQUIDO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("DATAEMISSAO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATAENTSAI", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATAENTREGA", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("CODFILIAL", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODFILIALENTREGA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODLOCAL", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCLIFOR", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODLOCALENTREGA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODTIPOPER", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODOPER", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODUSUARIOCRIACAO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("NUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            List<ComboBoxItem> Item = new List<ComboBoxItem>();
            Item.Add(new ComboBoxItem(0, "Aberto"));
            Item.Add(new ComboBoxItem(1, "Faturado"));
            Item.Add(new ComboBoxItem(2, "Cancelado"));
            Item.Add(new ComboBoxItem(3, "Parcialmente Quitado"));
            Item.Add(new ComboBoxItem(4, "Quitado"));
            Item.Add(new ComboBoxItem(5, "Parcialmente Faturado"));
            Item.Add(new ComboBoxItem(6, "Parcialmente Faturado / Finalizado"));
            Item.Add(new ComboBoxItem(7, "Bloqueado"));

            objArr.Add(new DataField("CODSTATUS", null, typeof(PS.Lib.WinForms.PSComboBox), Item));

            List<ComboBoxItem> ItemNfe = new List<ComboBoxItem>();
            ItemNfe.Add(new ComboBoxItem("P", "Pendente"));
            ItemNfe.Add(new ComboBoxItem("U", "Aguardando Processamento"));
            ItemNfe.Add(new ComboBoxItem("E", "Inconsistente"));
            ItemNfe.Add(new ComboBoxItem("A", "Autorizada"));
            ItemNfe.Add(new ComboBoxItem("C", "Cancelada"));
            ItemNfe.Add(new ComboBoxItem("I", "Inutilizado"));
            ItemNfe.Add(new ComboBoxItem("D", "Denegado"));

            objArr.Add(new DataField("CODSTATUSNFE", null, typeof(PS.Lib.WinForms.PSComboBox), ItemNfe));





            objArr.Add(new DataField("DATACRIACAO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("CODSERIE", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODREPRE", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODOBJETO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODOPERADOR", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODFORMA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCONTA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCONDICAO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CAMPOVALOR6", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CAMPOVALOR5", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CAMPOVALOR4", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CAMPOVALOR3", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CAMPOVALOR2", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CAMPOVALOR1", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("DATAEXTRA6", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATAEXTRA5", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATAEXTRA4", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATAEXTRA3", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATAEXTRA2", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATAEXTRA1", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("CAMPOLIVRE6", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CAMPOLIVRE5", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CAMPOLIVRE4", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CAMPOLIVRE3", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CAMPOLIVRE2", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CAMPOLIVRE1", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("ESPECIE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("PESOLIQUIDO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("PESOBRUTO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("QUANTIDADE", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODTRANSPORTADORA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCCUSTO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODNATUREZAORCAMENTO", null, typeof(PS.Lib.WinForms.PSLookup)));

            List<ComboBoxItem> Item1 = new List<ComboBoxItem>();
            Item1.Add(new ComboBoxItem(0, "CIF"));
            Item1.Add(new ComboBoxItem(1, "FOB"));
            Item1.Add(new ComboBoxItem(2, "Terceiro"));
            Item1.Add(new ComboBoxItem(9, "Sem Frete"));

            objArr.Add(new DataField("FRETECIFFOB", null, typeof(PS.Lib.WinForms.PSComboBox), Item1));
            objArr.Add(new DataField("HISTORICO", null, typeof(PS.Lib.WinForms.PSMemoBox)));
            objArr.Add(new DataField("OBSERVACAO", null, typeof(PS.Lib.WinForms.PSMemoBox)));
            objArr.Add(new DataField("MARCA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            objArr.Add(new DataField("CHAVENFE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DATAFATURAMENTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CCODFILIAL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODFILIALENTREGA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODNATUREZA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NFE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CHAVENFE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CNOMEFILIAL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CNOMECLIFOR", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("PRCOMISSAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("PLACA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("UFPLACA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODUSUARIOFATURAMENTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));



            return objArr;
        }

        public void Devolucao(object sender, EventArgs e)
        {
            ERP.Comercial.FormProcessoDevolucao f = new ERP.Comercial.FormProcessoDevolucao();
            f.ShowDialog();
        }

        public void ImprimirOperacao(object sender, EventArgs e)
        {
            // this.f.GetAnexos().Add("Imprimir Operação", null, ImprimirOperacao);

            DataField dfCODTIPOPER = gb.RetornaDataFieldByFilter(this.DefaultFilter, "CODTIPOPER");
            String CODTIPOPER = dfCODTIPOPER.Valor.ToString();

            String consultaZREPORT = @"
SELECT IDREPORT,
( SELECT NOME FROM ZREPORT WHERE IDREPORT = GTIPOPERREPORT2.IDREPORT ) NOME
FROM GTIPOPERREPORT2
WHERE CODEMPRESA = ?
  AND CODTIPOPER = ?";

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaZREPORT, new Object[] { AppLib.Context.Empresa, CODTIPOPER });

            if (dt.Rows.Count == 0)
            {
                AppLib.Windows.FormMessageDefault.ShowInfo("Não existe relatório parametrizado para este tipo operação.");
            }

            if (dt.Rows.Count == 1)
            {
                int IDREPORT = Convert.ToInt32(dt.Rows[0]["IDREPORT"]);
                this.ImprimirOperacao(IDREPORT);
            }

            if (dt.Rows.Count > 1)
            {
                AppLib.Windows.FormListaPrompt fLista = new AppLib.Windows.FormListaPrompt();
                fLista.PrimeiroItemNulo = false;
                Object result = fLista.Mostrar("Selecione o relatório:", dt);

                if (fLista.confirmacao == AppLib.Global.Types.Confirmacao.OK)
                {
                    int IDREPORT = Convert.ToInt32(result);
                    this.ImprimirOperacao(IDREPORT);
                }
            }
        }

        public void ImprimirOperacao(int IDREPORT)
        {
            if (this.f.dataGridView1.SelectedRows.Count > 0)
            {
                String ListCODOPER = "";

                for (int i = 0; i < this.f.dataGridView1.SelectedRows.Count; i++)
                {
                    ListCODOPER += int.Parse(this.f.dataGridView1.SelectedRows[i].Cells["CODOPER"].Value.ToString());
                    ListCODOPER += ", ";
                }

                ListCODOPER = ListCODOPER.Substring(0, ListCODOPER.Length - 2);

                AppLib.Padrao.FormReportVisao f = new AppLib.Padrao.FormReportVisao();
                f.grid1.Conexao = "Start";
                f.Visualizar(IDREPORT, ListCODOPER);
            }
            else
            {
                AppLib.Windows.FormMessageDefault.ShowInfo("Selecione o(s) registro(s).");
            }
        }
       
        
    }
}
