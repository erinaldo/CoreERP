using ITGProducao.Controles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace PS.Glb
{
    public partial class PSPartOperacaoItemEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private DataRow VPRODUTO;
        private DataRow GTIPOPER;
        private DataRow VCLIFOR;
        private DataRow GFILIAL;
        private DataRow GOPER;

        public bool edita = false;
        public bool editado = false;
        public int codOper = 0;
        public string codTipOper = string.Empty;
        public int nseqitem = 0;
        public string codNatureza = string.Empty;
        public bool consumoFinal = false;
        string codProduto;
        public string codStatusOper = string.Empty;
        public string Codstatus = string.Empty;
        //
        public string codlocal;
        public bool aprovado;
        private bool salvar = false;
        //
        public string ValorDecimal = string.Empty;

        private int GLB_PRODSERV = 0;

        #region Unidade de Medida

        private string UnidadeControle = string.Empty;
        private decimal FatorConversao;
        private decimal QuantidadeControle;
        private decimal ResultadoQuantidade;

        #endregion

        #region Lote

        //Variaveis para NewLookup
        private NewLookup lookup;

        public string Codlote = string.Empty;
        public int FilialLote;
        public string LocalLote = string.Empty;
        public int NseqItemLote;
        public string Codclifor = string.Empty;

        private decimal QtdLote;
        private decimal QtdControle;
        private decimal QuantidadeSugerida;
        private bool UsaAbaLote;
        private bool UsaLoteProduto;
        private DateTime DataEntSai;
        private string TipoOperEstoque = string.Empty;
        private DataTable dtLote = new DataTable();
        DataTable dtVisaoLote;
        DataTable dtVisaoInicial = new DataTable("Visão Inicial");
        DataTable dtVisaoEdicao = new DataTable("Visão Edição Lote");

        #endregion

        public bool ValidaBotao = false;

        #region Antigo

        public PSPartOperacaoItemEdit()
        {
            InitializeComponent();

            lookupproduto.txtcodigo.Validated += new System.EventHandler(lookupproduto_txtcodigo_Validated);
            lookupproduto.txtcodigo.Leave += new System.EventHandler(lookupproduto_txtcodigo_Leave);
            lookupproduto.btnprocurar.Click += new System.EventHandler(lookupproduto_txtcodigo_Leave);
            lookupproduto.txtconteudo.Click += new System.EventHandler(lookupproduto_txtcodigo_Leave);

            new Class.Utilidades().getDicionario(this, tabControl1, "GOPERITEM");
            //new Class.Utilidades().getDicionario(this, tabControl1, "GOPERITEMLOTE"); - Arrumar 

            new Class.Utilidades().criaCamposComplementares("GOPERITEMCOMPL", tabPage3);

            psVlUnitOriginal.Edita = false;

            //psLookupCODPRODUTO.PSPart = "PSPartProduto";

            // João Pedro Luchiari 17/11/2017 - Lookup de Natureza setado com descrição interna.
            psLookupCODNATUREZA.PSPart = "PSPartNatureza";
            psLookupCODNATUREZA.LookupField = "CODNATUREZA;DESCRICAOINTERNA";
            psLookupCODNATUREZA.LookupFieldResult = "CODNATUREZA;DESCRICAOINTERNA";

            //17/01/2018
            //psLookup6.PSPart = "PSPartUnidade";

            PSPartParamVarejoData psPartParamVarejoData = new PSPartParamVarejoData();
            psPartParamVarejoData._tablename = "VPARAMETROS";
            psPartParamVarejoData._keys = new string[] { "CODEMPRESA" };
            DataTable TabPreco = psPartParamVarejoData.RetornaTabelaPreco(PS.Lib.Contexto.Session.Empresa.CodEmpresa);

            List<PS.Lib.ComboBoxItem> listTabPreco = new List<PS.Lib.ComboBoxItem>();
            foreach (DataRow row in TabPreco.Rows)
            {
                listTabPreco.Add(new Lib.ComboBoxItem(row["TABELA"], row["NOME"].ToString()));
            }

            psComboBoxCODTABPRECO.DataSource = listTabPreco;
            psComboBoxCODTABPRECO.DisplayMember = "DisplayMember";
            psComboBoxCODTABPRECO.ValueMember = "ValueMember";

            // LISTA DE APLICAÇÃO DO MATERIAL
            List<PS.Lib.ComboBoxItem> listAPLICACAOMATERIAL = new List<PS.Lib.ComboBoxItem>();

            listAPLICACAOMATERIAL.Add(new PS.Lib.ComboBoxItem());
            listAPLICACAOMATERIAL[0].ValueMember = "V";
            listAPLICACAOMATERIAL[0].DisplayMember = "Venda/Industrialização";

            listAPLICACAOMATERIAL.Add(new PS.Lib.ComboBoxItem());
            listAPLICACAOMATERIAL[1].ValueMember = "R";
            listAPLICACAOMATERIAL[1].DisplayMember = "Revenda";

            listAPLICACAOMATERIAL.Add(new PS.Lib.ComboBoxItem());
            listAPLICACAOMATERIAL[2].ValueMember = "C";
            listAPLICACAOMATERIAL[2].DisplayMember = "Consumo";

            listAPLICACAOMATERIAL.Add(new PS.Lib.ComboBoxItem());
            listAPLICACAOMATERIAL[3].ValueMember = " ";
            listAPLICACAOMATERIAL[3].DisplayMember = " ";

            psComboBoxAPLICACAOMATERIAL.DataSource = listAPLICACAOMATERIAL;
            psComboBoxAPLICACAOMATERIAL.DisplayMember = "DisplayMember";
            psComboBoxAPLICACAOMATERIAL.ValueMember = "ValueMember";


            List<PS.Lib.ComboBoxItem> listaTipoDesconto = new List<PS.Lib.ComboBoxItem>();

            listaTipoDesconto.Add(new PS.Lib.ComboBoxItem());
            listaTipoDesconto[0].ValueMember = " ";
            listaTipoDesconto[0].DisplayMember = " ";

            listaTipoDesconto.Add(new PS.Lib.ComboBoxItem());
            listaTipoDesconto[1].ValueMember = "U";
            listaTipoDesconto[1].DisplayMember = "Unitário";

            listaTipoDesconto.Add(new PS.Lib.ComboBoxItem());
            listaTipoDesconto[2].ValueMember = "T";
            listaTipoDesconto[2].DisplayMember = "Total";

            psComboBox1.DataSource = listaTipoDesconto;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";

            psValorTotalItem.Edita = false;

            #region Importação



            List<PS.Lib.ComboBoxItem> listaTPViaTransp = new List<PS.Lib.ComboBoxItem>();

            listaTPViaTransp.Add(new PS.Lib.ComboBoxItem());
            listaTPViaTransp[0].ValueMember = "4";
            listaTPViaTransp[0].DisplayMember = "Aérea";

            listaTPViaTransp.Add(new PS.Lib.ComboBoxItem());
            listaTPViaTransp[1].ValueMember = "1";
            listaTPViaTransp[1].DisplayMember = "Marítima";

            listaTPViaTransp.Add(new PS.Lib.ComboBoxItem());
            listaTPViaTransp[2].ValueMember = "2";
            listaTPViaTransp[2].DisplayMember = "Fluvial";

            listaTPViaTransp.Add(new PS.Lib.ComboBoxItem());
            listaTPViaTransp[3].ValueMember = "3";
            listaTPViaTransp[3].DisplayMember = "Lacustre";

            listaTPViaTransp.Add(new PS.Lib.ComboBoxItem());
            listaTPViaTransp[4].ValueMember = "5";
            listaTPViaTransp[4].DisplayMember = "Postal";

            listaTPViaTransp.Add(new PS.Lib.ComboBoxItem());
            listaTPViaTransp[5].ValueMember = "6";
            listaTPViaTransp[5].DisplayMember = "Ferroviária";

            listaTPViaTransp.Add(new PS.Lib.ComboBoxItem());
            listaTPViaTransp[6].ValueMember = "7";
            listaTPViaTransp[6].DisplayMember = "Rodoviária";

            listaTPViaTransp.Add(new PS.Lib.ComboBoxItem());
            listaTPViaTransp[7].ValueMember = "8";
            listaTPViaTransp[7].DisplayMember = "Conduto / Rede Transmissão";

            listaTPViaTransp.Add(new PS.Lib.ComboBoxItem());
            listaTPViaTransp[8].ValueMember = "9";
            listaTPViaTransp[8].DisplayMember = "Meios Própios";

            listaTPViaTransp.Add(new PS.Lib.ComboBoxItem());
            listaTPViaTransp[9].ValueMember = "10";
            listaTPViaTransp[9].DisplayMember = "Entrada / Saída ficta";

            cmbTPVIATRANSP.DataSource = listaTPViaTransp;
            cmbTPVIATRANSP.DisplayMember = "DisplayMember";
            cmbTPVIATRANSP.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listaTPIntermedio = new List<PS.Lib.ComboBoxItem>();
            listaTPIntermedio.Add(new PS.Lib.ComboBoxItem());
            listaTPIntermedio[0].ValueMember = "1";
            listaTPIntermedio[0].DisplayMember = "Importação por conta própria";

            listaTPIntermedio.Add(new PS.Lib.ComboBoxItem());
            listaTPIntermedio[1].ValueMember = "2";
            listaTPIntermedio[1].DisplayMember = "Importação por conta e ordem";

            listaTPIntermedio.Add(new PS.Lib.ComboBoxItem());
            listaTPIntermedio[2].ValueMember = "3";
            listaTPIntermedio[2].DisplayMember = "Importação por encomenda";

            cmbTPINTERMEDIO.DataSource = listaTPIntermedio;
            cmbTPINTERMEDIO.DisplayMember = "DisplayMember";
            cmbTPINTERMEDIO.ValueMember = "ValueMember";

            #endregion
        }

        private void lookupproduto_txtcodigo_Leave(object sender, System.EventArgs e)
        {
            getInfoProduto();
            buscaPreco();
            getClassificacao();
            buscaNatureza();

            //João Pedro Luchiari - 16/01/2018
            UnidadeControle = UnidadeControleProduto();
            UnidadeMedida();
            //textBox2.Text = VPRODUTO["DESCRICAO"].ToString();
        }

        private void VerificaLookup()
        {
            GetOperacao(AppLib.Context.Empresa, codOper);

            string CodCliFor = GOPER["CODCLIFOR"].ToString();
            string CodTipoOper = GOPER["CODTIPOPER"].ToString();
            GTIPOPER = gb.RetornaParametrosOperacao(CodTipoOper);

            GetDefaultCliFor(AppLib.Context.Empresa, CodCliFor);

            if (GTIPOPER["VLUNITARIOEM"].ToString() == "TABCLI")
            {
                bool UsaFilial = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT USATABELAPORFILIAL FROM VCLIFORTABPRECO WHERE CODEMPRESA = ? AND CODCLIFOR = ? ", new object[] { AppLib.Context.Empresa, CodCliFor }).ToString());

                if (UsaFilial == true)
                {
                    lookupproduto.Grid_WhereVisao[4].ValorFixo = @"SELECT VPRODUTO.CODPRODUTO 
FROM VPRODUTO 
LEFT OUTER JOIN VCLIFORTABPRECOITEM ON VCLIFORTABPRECOITEM.CODEMPRESA = VPRODUTO.CODEMPRESA AND VCLIFORTABPRECOITEM.CODPRODUTO = VPRODUTO.CODPRODUTO 
INNER JOIN VCLIFORTABPRECO ON VCLIFORTABPRECO.CODEMPRESA = VCLIFORTABPRECOITEM.CODEMPRESA AND VCLIFORTABPRECO.IDTABELA = VCLIFORTABPRECOITEM.IDTABELA 
INNER JOIN VCLIFOR ON VCLIFOR.CODEMPRESA = VCLIFORTABPRECO.CODEMPRESA AND VCLIFOR.CODCLIFOR = VCLIFORTABPRECO.CODCLIFOR
LEFT OUTER JOIN GTIPOPER ON GTIPOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GTIPOPER.CODTIPOPER = '" + CodTipoOper + "' WHERE GTIPOPER.VLUNITARIOEM IN('TABCLI') AND VCLIFORTABPRECOITEM.CODFILIAL = " + GOPER["CODFILIAL"] + " AND VCLIFOR.CODCLIFOR = '" + CodCliFor + "'";
                    lookupproduto.Grid_WhereVisao[4].OutrosFiltros_SelectQuery.Clear();
                }
                else
                {
                    lookupproduto.Grid_WhereVisao[4].ValorFixo = @"SELECT VPRODUTO.CODPRODUTO 
FROM VPRODUTO 
LEFT OUTER JOIN VCLIFORTABPRECOITEM ON VCLIFORTABPRECOITEM.CODEMPRESA = VPRODUTO.CODEMPRESA AND VCLIFORTABPRECOITEM.CODPRODUTO = VPRODUTO.CODPRODUTO 
INNER JOIN VCLIFORTABPRECO ON VCLIFORTABPRECO.CODEMPRESA = VCLIFORTABPRECOITEM.CODEMPRESA AND VCLIFORTABPRECO.IDTABELA = VCLIFORTABPRECOITEM.IDTABELA 
INNER JOIN VCLIFOR ON VCLIFOR.CODEMPRESA = VCLIFORTABPRECO.CODEMPRESA AND VCLIFOR.CODCLIFOR = VCLIFORTABPRECO.CODCLIFOR
LEFT OUTER JOIN GTIPOPER ON GTIPOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GTIPOPER.CODTIPOPER = '" + CodTipoOper + "' WHERE GTIPOPER.VLUNITARIOEM IN('TABCLI') AND VCLIFOR.CODCLIFOR = '" + CodCliFor + "'";
                    lookupproduto.Grid_WhereVisao[4].OutrosFiltros_SelectQuery.Clear();
                }
            }
            else
            {
                lookupproduto.Grid_WhereVisao[4].ValorFixo = @"SELECT VPRODUTO.CODPRODUTO FROM VPRODUTO WHERE CODEMPRESA  = " + AppLib.Context.Empresa + "";
            }

        }

        private void lookupproduto_txtcodigo_Validated(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(lookupproduto.ValorCodigoInterno))
            {
                getInfoProduto();
                //UnidadeControle = UnidadeControleProduto();
                buscaPreco();
                getClassificacao();
                buscaNatureza();
                psPercDesconto.textBox1.Text = "0";
                psValorDesconto.textBox1.Text = "0";
                psPRAcrescimo.textBox1.Text = "0";
                psVLAcrescimo.textBox1.Text = "0";
                txtVLORDESCDI.Text = "0";
                UnidadeMedida();
            }
        }
        public void GetOperacao(int CodEmpresa, int CodOper)
        {
            PSPartOperacaoData psPartOperacaoData = new PSPartOperacaoData();
            psPartOperacaoData._tablename = "GOPER";
            psPartOperacaoData._keys = new string[] { "CODEMPRESA", "CODOPER" };
            GOPER = psPartOperacaoData.ReadRecordEdit(CodEmpresa, CodOper).Rows[0];
        }

        private void GetUnidadeMedida()
        {
            try
            {
                string sSql = @"SELECT CODUNIDOPER FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";

                DataTable VOPERITEM = dbs.QuerySelect(sSql, this._setDefault[0].Valor, this._setDefault[1].Valor, this.psTextoBox1.Text);

                if (VOPERITEM.Rows.Count > 0)
                {
                    lpUnidadeMedida.txtcodigo.Text = VOPERITEM.Rows[0]["CODUNIDOPER"].ToString();
                    lpUnidadeMedida.CarregaDescricao();
                    //17/01/2018
                    //psLookup6.Text = VOPERITEM.Rows[0]["CODUNIDOPER"].ToString();
                    //psLookup6.LoadLookup();
                }
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }
        }

        private void GetDefaultCliFor(int CodEmpresa, string CodCliFor)
        {
            PSPartCliForData psPartCliForData = new PSPartCliForData();
            psPartCliForData._tablename = "VCLIFOR";
            psPartCliForData._keys = new string[] { "CODEMPRESA", "CODCLIFOR" };

            VCLIFOR = psPartCliForData.ReadRecordEdit(CodEmpresa, CodCliFor).Rows[0];
        }

        private void GetDefaultFilial(int CodEmpresa, int CodFilial)
        {
            PSPartFilialData psPartFilialData = new PSPartFilialData();
            psPartFilialData._tablename = "GFILIAL";
            psPartFilialData._keys = new string[] { "CODEMPRESA", "CODFILIAL" };

            GFILIAL = psPartFilialData.ReadRecordEdit(CodEmpresa, CodFilial).Rows[0];
        }

        private void GetProduto()
        {
            try
            {
                PSPartProdutoData psPartProdutoData = new PSPartProdutoData();
                psPartProdutoData._tablename = "VPRODUTO";
                psPartProdutoData._keys = new string[] { "CODEMPRESA", "CODPRODUTO" };
                VPRODUTO = psPartProdutoData.ReadRecordEdit(AppLib.Context.Empresa, lookupproduto.ValorCodigoInterno).Rows[0];
                //VPRODUTO = psPartProdutoData.ReadRecordEdit(AppLib.Context.Empresa, psLookupCODPRODUTO.Text).Rows[0];
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }
        }

        public override void CarregaParametrosTela()
        {
            base.CarregaParametrosTela();

            if (this._setDefault != null && this._setDefault.Count > 0)
            {

                string CodCliFor = GOPER["CODCLIFOR"].ToString();
                string CodTipoOper = GOPER["CODTIPOPER"].ToString();
                GTIPOPER = gb.RetornaParametrosOperacao(CodTipoOper);

                bool usatabADD1 = false;

                #region Natureza

                //Edita
                if (GTIPOPER["USANATUREZA"].ToString() == "E")
                {
                    psLookupCODNATUREZA.Visible = true;
                    psLookupCODNATUREZA.Chave = true;

                    usatabADD1 = true;
                }

                //Não Edita
                if (GTIPOPER["USANATUREZA"].ToString() == "N")
                {
                    psLookupCODNATUREZA.Visible = true;
                    psLookupCODNATUREZA.Chave = false;

                    usatabADD1 = true;
                }

                //Não Mostra
                if (GTIPOPER["USANATUREZA"].ToString() == "M")
                {
                    psLookupCODNATUREZA.Visible = false;
                    psLookupCODNATUREZA.Chave = false;

                    if (!usatabADD1)
                        usatabADD1 = false;
                }

                #region Data Entrega Item


                //Não Mostra
                if (GTIPOPER["USADATAENTREGAITEM"].ToString() == "M")
                {
                    dtEntrega.Enabled = false;
                }

                #endregion

                //Natureza Padrão
                if (_setDefault != null)
                {
                    if (psTextoBox1.Text == "0")
                    {
                        string UFEmitente = new PSPartOperacaoData().BuscaUFEmitente(Convert.ToInt32(_setDefault[0].Valor), Convert.ToInt32(_setDefault[1].Valor));
                        string UFDestinatario = new PSPartOperacaoData().BuscaUFDestinatario(Convert.ToInt32(_setDefault[0].Valor), Convert.ToInt32(_setDefault[1].Valor));

                        if (!string.IsNullOrEmpty(UFEmitente))
                        {
                            if (!string.IsNullOrEmpty(UFDestinatario))
                            {
                                if (UFEmitente == UFDestinatario)
                                {
                                    psLookupCODNATUREZA.Text = GTIPOPER["CODNATDENTRO"].ToString();
                                    if (psLookupCODNATUREZA.Text != string.Empty)
                                        psLookupCODNATUREZA.LoadLookup();
                                }
                                else
                                {
                                    psLookupCODNATUREZA.Text = GTIPOPER["CODNATFORA"].ToString();
                                    if (psLookupCODNATUREZA.Text != string.Empty)
                                        psLookupCODNATUREZA.LoadLookup();
                                }
                            }
                        }
                    }
                }

                #endregion

                #region Tributos

                //Aba Tributos
                if (int.Parse(GTIPOPER["USAABATRIBUTOS"].ToString()) == 0)
                {
                    tabControl1.TabPages.Remove(tabTRI);
                }

                #endregion

                #region Valor Unitario

                //Edita
                if (GTIPOPER["USAVLUNITARIO"].ToString() == "E")
                {
                    psValorUnitario.Visible = true;
                    psValorUnitario.Edita = true;
                }

                //Não Edita
                if (GTIPOPER["USAVLUNITARIO"].ToString() == "N")
                {
                    psValorUnitario.Visible = true;
                    psValorUnitario.Edita = false;
                }

                //Não Mostra
                if (GTIPOPER["USAVLUNITARIO"].ToString() == "M")
                {
                    psValorUnitario.Visible = false;
                    psValorUnitario.Edita = false;
                }
                else
                {
                    psComboBoxCODTABPRECO.Visible = true;

                    if (GTIPOPER["VLUNITARIOEM"].ToString() == "NENHUM")
                        psComboBoxCODTABPRECO.Value = 0;
                    if (GTIPOPER["VLUNITARIOEM"].ToString() == "PRECO1")
                        psComboBoxCODTABPRECO.Value = 1;
                    if (GTIPOPER["VLUNITARIOEM"].ToString() == "PRECO2")
                        psComboBoxCODTABPRECO.Value = 2;
                    if (GTIPOPER["VLUNITARIOEM"].ToString() == "PRECO3")
                        psComboBoxCODTABPRECO.Value = 3;
                    if (GTIPOPER["VLUNITARIOEM"].ToString() == "PRECO4")
                        psComboBoxCODTABPRECO.Value = 4;
                    if (GTIPOPER["VLUNITARIOEM"].ToString() == "PRECO5")
                        psComboBoxCODTABPRECO.Value = 5;
                    if (GTIPOPER["VLUNITARIOEM"].ToString() == "TABCLI")
                        psComboBoxCODTABPRECO.Value = 6;
                }

                #endregion

                #region Tabela de Preço
                //Edita
                if (GTIPOPER["USATABELAPRECO"].ToString() == "E")
                {
                    psComboBoxCODTABPRECO.Visible = true;

                }

                //Não Edita
                if (GTIPOPER["USATABELAPRECO"].ToString() == "N")
                {
                    psComboBoxCODTABPRECO.Visible = true;
                    psComboBoxCODTABPRECO.Enabled = false;
                }

                //Não Mostra
                if (GTIPOPER["USATABELAPRECO"].ToString() == "M")
                {
                    psComboBoxCODTABPRECO.Visible = false;
                }

                if (GTIPOPER["VLUNITARIOEM"].ToString() == "NAOUSA")
                {
                    psComboBoxCODTABPRECO.Visible = false;
                }


                #endregion

                #region Perc. Acrescimo
                //Edita
                if (GTIPOPER["USAPRACRESCIMO"].ToString() == "E")
                {
                    psPRAcrescimo.Visible = true;
                    psPRAcrescimo.Edita = true;
                }

                //Não Edita
                if (GTIPOPER["USAPRACRESCIMO"].ToString() == "N")
                {
                    psPRAcrescimo.Visible = true;
                    psPRAcrescimo.Enabled = false;
                }

                //Não Mostra
                if (GTIPOPER["USAPRACRESCIMO"].ToString() == "M")
                {
                    psPRAcrescimo.Visible = false;
                    psPRAcrescimo.Edita = false;
                }
                #endregion

                #region Valor Acrescimo
                //Edita
                if (GTIPOPER["USAVLACRESCIMO"].ToString() == "E")
                {
                    psVLAcrescimo.Visible = true;
                    psVLAcrescimo.Edita = true;
                }

                //Não Edita
                if (GTIPOPER["USAVLACRESCIMO"].ToString() == "N")
                {
                    psVLAcrescimo.Visible = true;
                    psVLAcrescimo.Enabled = false;
                }

                //Não Mostra
                if (GTIPOPER["USAVLACRESCIMO"].ToString() == "M")
                {
                    psVLAcrescimo.Visible = false;
                    psVLAcrescimo.Edita = false;
                }
                #endregion

                #region Perc. Desconto

                //Edita
                if (GTIPOPER["USAPRDESCONTO"].ToString() == "E")
                {
                    psPercDesconto.Visible = true;
                    psPercDesconto.Edita = true;
                    psComboBox1.Visible = true;
                }

                //Não Edita
                if (GTIPOPER["USAPRDESCONTO"].ToString() == "N")
                {
                    psPercDesconto.Visible = true;
                    psPercDesconto.Edita = false;
                    psComboBox1.Visible = true;
                    psComboBox1.Enabled = false;
                }

                //Não Mostra
                if (GTIPOPER["USAPRDESCONTO"].ToString() == "M")
                {
                    psPercDesconto.Visible = false;
                    psPercDesconto.Edita = false;
                    psComboBox1.Enabled = false;
                    psComboBox1.Visible = false;
                }

                #endregion

                #region Valor Desconto

                //Edita
                if (GTIPOPER["USAVLDESCONTO"].ToString() == "E")
                {
                    psValorDesconto.Visible = true;
                    psValorDesconto.Edita = true;
                }

                //Não Edita
                if (GTIPOPER["USAVLDESCONTO"].ToString() == "N")
                {
                    psValorDesconto.Visible = true;
                    psValorDesconto.Edita = false;
                }

                //Não Mostra
                if (GTIPOPER["USAVLDESCONTO"].ToString() == "M")
                {
                    psValorDesconto.Visible = false;
                    psValorDesconto.Edita = false;
                }

                #endregion

                #region Valor Total Item

                //Edita
                if (GTIPOPER["USAVLTOTALITEM"].ToString() == "E")
                {
                    psValorTotalItem.Visible = true;
                    psValorTotalItem.Edita = true;

                    button2.Visible = true;
                }

                //Não Edita
                if (GTIPOPER["USAVLTOTALITEM"].ToString() == "N")
                {
                    psValorTotalItem.Visible = true;
                    psValorTotalItem.Edita = false;
                    psCheckBox1.Enabled = false;

                    button2.Visible = true;
                }

                //Não Mostra
                if (GTIPOPER["USAVLTOTALITEM"].ToString() == "M")
                {
                    psValorTotalItem.Visible = false;
                    psValorTotalItem.Edita = false;

                    button2.Visible = false;
                }

                #endregion

                #region Produto/Serviço

                GLB_PRODSERV = int.Parse(GTIPOPER["PRODSERV"].ToString());

                #endregion

                #region Seleção da Natureza
                if (GTIPOPER["AUTOSELECAONATUREZA"].ToString() == "1")
                {
                    btnBuscarNatureza.Enabled = true;
                    psComboBoxAPLICACAOMATERIAL.Enabled = true;
                }
                else
                {
                    btnBuscarNatureza.Enabled = false;
                    psComboBoxAPLICACAOMATERIAL.Enabled = false;
                }
                #endregion

                #region Aba Tabelas

                //Aba Tabelas
                if (!usatabADD1)
                {
                    // tabControl1.TabPages.Remove(tabADD1);
                    psLookupCODNATUREZA.Visible = false;
                }

                #endregion

                #region Importação


                #endregion

            }
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psTextoBox1.Text = "0";
            psTextoBox1.Edita = false;

            lpUnidadeMedida.txtcodigo.Text = string.Empty;
            //17/01/2018
            //psLookup6.Text = string.Empty;
            //psLookup6.LoadLookup();

            textBox2.Text = string.Empty;

            this.CarregaParametrosTela();

            dtEntrega.Text = GOPER["DATAENTREGA"].ToString();
        }

        public override void CarregaRegistro()
        {
            if (salvar == false)
            {
                this.CarregaParametrosTela();
            }

            this.GetOperacao(Convert.ToInt32(_setDefault[0].Valor), Convert.ToInt32(_setDefault[1].Valor));
            base.CarregaRegistro();

            this.GetUnidadeMedida();
            //  carregaIBPTAX();
        }

        //private void carregaIBPTAX()
        //{
        //    try
        //    {
        //        string codetd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODETD FROM VCLIFOR WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { GOPER["CODCLIFOR"], AppLib.Context.Empresa }).ToString();
        //        if (!string.IsNullOrEmpty(codetd))
        //        {
        //            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT UF, NACIONALFEDERAL, IMPORTADOSFEDERAL, ESTADUAL, MUNICIPAL, CHAVE FROM VIBPTAX WHERE CODIGO = ? AND UF = ?", new object[] { VPRODUTO["CODNCM"].ToString(), codetd });
        //            if (dt.Rows.Count > 0)
        //            {
        //                psTextoBox2.Text = dt.Rows[0]["UF"].ToString().Replace(",", ".");
        //                psTextoBox3.Text = dt.Rows[0]["NACIONALFEDERAL"].ToString().Replace(",", ".");
        //                psTextoBox4.Text = dt.Rows[0]["IMPORTADOSFEDERAL"].ToString().Replace(",", ".");
        //                psTextoBox5.Text = dt.Rows[0]["ESTADUAL"].ToString().Replace(",", ".");
        //                psTextoBox6.Text = dt.Rows[0]["MUNICIPAL"].ToString().Replace(",", ".");
        //                psTextoBox7.Text = dt.Rows[0]["CHAVE"].ToString().Replace(",", ".");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        PS.Lib.PSMessageBox.ShowError(ex.Message);

        //    }

        //}

        public override void SalvaRegistro()
        {
            button2_Click(this, null);

            salvar = true;

            if (this._psPartData == null)
            {

                this._psPartData = new PSPartOperacaoItemData();
                this._psPartData._tablename = "GOPERITEM";
                this._psPartData._keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM" };
                //_setDefault
            }
            base.SalvaRegistro();

            salvar = false;
        }

        private decimal calculaAcrescimo()
        {
            decimal vl = 0, pr = 0;
            vl = Convert.ToDecimal(psVLAcrescimo.Text);
            pr = Convert.ToDecimal(psPRAcrescimo.Text);
            //Preenchendo os campos
            if (vl == 0)
            {
                vl = ((pr * Convert.ToDecimal(psVlUnitOriginal.Text)) / 100);
                psVLAcrescimo.Text = vl.ToString();
            }
            else
            {
                pr = ((vl / Convert.ToDecimal(psVlUnitOriginal.Text)) * 100);
                psPRAcrescimo.Text = pr.ToString();
            }
            return vl;
        }

        private decimal calculaDesconto(string tipo)
        {
            decimal vl = 0, pr = 0;
            vl = Convert.ToDecimal(psValorDesconto.Text);
            pr = Convert.ToDecimal(psPercDesconto.Text);
            if (tipo == "Unitário")
            {
                //Preenchendo os campos
                if (vl == 0)
                {
                    vl = ((pr * Convert.ToDecimal(psVlUnitOriginal.Text)) / 100);
                    psValorDesconto.Text = vl.ToString();
                }
                else
                {
                    pr = ((vl / Convert.ToDecimal(psVlUnitOriginal.Text)) * 100);
                    psPercDesconto.Text = pr.ToString();
                }
                return vl;
            }
            else
            {
                //Preenchendo os campos
                if (vl == 0)
                {
                    vl = (pr * (Convert.ToDecimal(psVlUnitOriginal.Text) * Convert.ToDecimal(psQuantidade.Text)) / 100);
                    psValorDesconto.Text = vl.ToString();
                }
                else
                {
                    pr = (vl / (Convert.ToDecimal(psVlUnitOriginal.Text) * Convert.ToDecimal(psQuantidade.Text)) * 100);
                    psPercDesconto.Text = pr.ToString();
                }
                return vl;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            psComboBoxCODTABPRECO_SelectedValueChanged(this, e);

            decimal acrescimo = calculaAcrescimo();
            decimal vlUnitario = 0;
            decimal desconto = 0;
            try
            { 
                if (psCheckBox1.checkBox1.Checked == false)
                {
                    if (psComboBox1.Text == "Unitário")
                    {
                        desconto = calculaDesconto("Unitário");
                        vlUnitario = ((Convert.ToDecimal(psVlUnitOriginal.Text) + acrescimo) - desconto);
                        if (vlUnitario >= 0)
                        {
                            psValorUnitario.CasasDecimais = 4; //Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT DECIMALVLUNITARIO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper }));
                            psValorUnitario.Text = vlUnitario.ToString("#.000");
                        }
                        else
                        {
                            MessageBox.Show("O valor de desconto não pode ser maior que o valor unitário do item.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        psValorTotalItem.Text = ((vlUnitario * Convert.ToDecimal(psQuantidade.Text))).ToString();
                    }
                    else if (psComboBox1.Text == "Total")
                    {
                        desconto = calculaDesconto("Total");
                        vlUnitario = (Convert.ToDecimal(psVlUnitOriginal.Text) + acrescimo);
                        if (vlUnitario >= 0)
                        {
                            psValorUnitario.Text = vlUnitario.ToString();
                        }
                        else
                        {
                            MessageBox.Show("O valor de desconto não pode ser maior que o valor unitário do item.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        psValorTotalItem.Text = ((vlUnitario * Convert.ToDecimal(psQuantidade.Text)) - desconto).ToString();
                    }
                    else
                    {
                        psValorTotalItem.Text = (Convert.ToDecimal(psVlUnitOriginal.textBox1.Text) * Convert.ToDecimal(psQuantidade.Text) + acrescimo).ToString();
                    }
                }
                else
                {
                    if (psComboBoxCODTABPRECO.Text == "Nenhum")
                    {
                        psValorUnitario.textBox1.Text = string.Format("{0:n5}", Convert.ToDecimal(psValorTotalItem.textBox1.Text) / Convert.ToDecimal(psQuantidade.textBox1.Text));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro a realizar o cálculo. Favor verificar.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            UnidadeMedida();
        }

        private void psLookup1_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            if (GLB_PRODSERV != 0)
            {
                e.Filtro.Add(new PS.Lib.PSFilter("PRODSERV", GLB_PRODSERV));
                e.Filtro.Add(new PS.Lib.PSFilter("ATIVO", 1));
            }
        }

        private void psLookup2_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            //if (primeiroLookup == false)
            //{
            this.GetDefaultCliFor(Convert.ToInt32(GOPER["CODEMPRESA"]), GOPER["CODCLIFOR"].ToString());
            this.GetDefaultFilial(Convert.ToInt32(GOPER["CODEMPRESA"]), Convert.ToInt32(GOPER["CODFILIAL"]));

            string CODETDCLIFOR = VCLIFOR["CODETD"].ToString();
            string CODETDFILIAL = GFILIAL["CODETD"].ToString();

            if (CODETDCLIFOR == CODETDFILIAL)
            {
                e.Filtro.Add(new Lib.PSFilter("CODNATUREZA", "IN", "(SELECT CODNATUREZA FROM VNATUREZAREGRATRIBUTACAO WHERE CODEMPRESA = '" + AppLib.Context.Empresa + "' AND CODETD = '" + VCLIFOR["CODETD"] + "')"));
            }
            else
            {
                e.Filtro.Add(new Lib.PSFilter("CODNATUREZA", "IN", "(SELECT CODNATUREZA FROM VNATUREZAREGRATRIBUTACAO WHERE CODEMPRESA = '" + AppLib.Context.Empresa + "' AND CODREGIAO IN (SELECT CODREGIAO FROM VREGIAOESTADO WHERE CODEMPRESA = '" + AppLib.Context.Empresa + "' AND CODETD = '" + VCLIFOR["CODETD"] + "'))"));
            }

            //}

            //if (edita == true)
            //{
            //    primeiroLookup = false;
            //}

            // Somente naturezas ativas
            e.Filtro.Add(new PS.Lib.PSFilter("ATIVO", 1));
            // Natureza de entrada para movimento de entrada e saída para movimento de saída
            string CodTipoOper = GOPER["CODTIPOPER"].ToString();
            GTIPOPER = gb.RetornaParametrosOperacao(CodTipoOper);
            String TIPENTSAI = GTIPOPER["TIPENTSAI"].ToString();
            e.Filtro.Add(new PS.Lib.PSFilter("TIPENTSAI", TIPENTSAI));
        }

        private void psLookup6_BeforeLookup(object sender, Lib.LookupEventArgs e)
        {
            //this.GetProduto();
            //e.Filtro.Add(new Lib.PSFilter("CODUNIDBASE", VPRODUTO["CODUNIDCONTROLE"]));
        }

        private void PSPartOperacaoItemEdit_Load(object sender, EventArgs e)
        {
            // CodStatus - João Pedro Luchiari 19/10/2017
            Codstatus = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODSTATUS FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codOper }).ToString();

            string CodTipOper = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codOper }).ToString();

            new Class.Utilidades().criaCamposComplementaresOperacao("GOPERITEMCOMPL", tabPage3, codTipOper);

            bool UsaNfe = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT USANFEIMPORTACAO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper }));

            if (UsaNfe != true)
            {
                tabControl1.TabPages.Remove(tabPage4);
            }

            psValorUnitario.CasasDecimais = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT DECIMALVLUNITARIO FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ? ", new object[] { codTipOper, AppLib.Context.Empresa }));
            psVlUnitOriginal.CasasDecimais = psValorUnitario.CasasDecimais;

            string campo = AppLib.Context.poolConnection.Get("Start").ExecGetField("CODPRODUTO", @"SELECT BUSCAPRODUTOPOR FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codTipOper, AppLib.Context.Empresa }).ToString();
            switch (campo)
            {
                case "0":
                    lookupproduto.CampoCodigo_Igual_a = NewLookup.CampoCodigoIguala.CampoCodigoBD;
                    break;
                case "1":
                    lookupproduto.CampoCodigo_Igual_a = NewLookup.CampoCodigoIguala.CampoCodigoInterno;
                    break;
                default:
                    lookupproduto.CampoCodigo_Igual_a = NewLookup.CampoCodigoIguala.CampoCodigoBD;
                    break;
            }

            lookupproduto.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Clear();
            lookupproduto.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODEMPRESA", ValorColunaChave = AppLib.Context.Empresa.ToString() });
            lookupproduto.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODTIPOPER", ValorColunaChave = codTipOper.ToString() });
            if (aprovado == true)
            {
                btnSalvarAtual.Enabled = false;
                btnOKAtual.Enabled = false;
                toolStripButton2.Enabled = false;
                toolStripButton3.Enabled = false;
            }
            //if (codStatusOper != "Aberto" && !string.IsNullOrEmpty(codStatusOper))

            if (Codstatus != "0" && Codstatus != "5")
            {
                btnSalvarAtual.Enabled = false;
                btnOKAtual.Enabled = false;
                toolStripButton2.Enabled = false;
                toolStripButton3.Enabled = false;
            }
            if (edita == true)
            {
                string StatusNFE = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODSTATUS FROM GNFESTADUAL WHERE CODOPER	= ? AND CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();
                if (!string.IsNullOrEmpty(StatusNFE))
                {
                    if (StatusNFE != "P" && StatusNFE != "E" && StatusNFE != "I")
                    {
                        btnSalvarAtual.Enabled = false;
                        btnOKAtual.Enabled = false;
                        toolStripButton2.Enabled = false;
                        toolStripButton3.Enabled = false;
                    }
                }
            }

            GetOperacao(AppLib.Context.Empresa, codOper);
            GTIPOPER = gb.RetornaParametrosOperacao(codTipOper);
            this._setDefault = new List<Lib.DataField>();

            PS.Lib.DataField DFCODEMPRESA = new Lib.DataField("CODEMPRESA", AppLib.Context.Empresa);
            PS.Lib.DataField DFCODOPER = new Lib.DataField("CODOPER", codOper);

            _setDefault.Add(DFCODEMPRESA);
            _setDefault.Add(DFCODOPER);

            //getDicionario(tabControl1);
            carregaParametros();
            getNseq();
            AtribuiZero();

            if (edita == true)
            {
                VerificaLookup();
                carregaCampos();
                carregaTributos();
                verificaEdicao();
            }
            else
            {
                VerificaLookup();
                tbQuantidadeConversao.Text = "0,00";

                if (!string.IsNullOrEmpty(GOPER["DATAENTREGA"].ToString()))
                {
                    dtEntrega.DateTime = Convert.ToDateTime(GOPER["DATAENTREGA"]);
                }
                if (consumoFinal == true)
                {
                    psComboBoxAPLICACAOMATERIAL.DataSource = null;
                    List<PS.Lib.ComboBoxItem> listAPLICACAOMATERIAL = new List<PS.Lib.ComboBoxItem>();
                    listAPLICACAOMATERIAL.Add(new PS.Lib.ComboBoxItem());
                    listAPLICACAOMATERIAL[0].ValueMember = "C";
                    listAPLICACAOMATERIAL[0].DisplayMember = "Consumo";
                    psComboBoxAPLICACAOMATERIAL.DataSource = listAPLICACAOMATERIAL;
                    psComboBoxAPLICACAOMATERIAL.DisplayMember = "DisplayMember";
                    psComboBoxAPLICACAOMATERIAL.ValueMember = "ValueMember";
                    psComboBoxAPLICACAOMATERIAL.SelectedIndex = 0;
                }
            }

            string EditaCodunidoper = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT EDITACODUNIDOPER FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper }).ToString();

            if (EditaCodunidoper == "E")
            {
                lpUnidadeMedida.Enabled = true;
                //psLookup6.Enabled = true;
            }
            else
            {
                lpUnidadeMedida.Enabled = false;
                //psLookup6.Enabled = false;
            }

            #region Lote 

            //tabControl1.TabPages.Remove(tabPage5);
            groupBox5.Enabled = false;

            // Verifica o parâmetro na tabela GTIPOPER
            UsaAbaLote = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT USALOTEPRODUTO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper }));

            // Verifica o parâmetro na tabela VPRODUTO
            UsaLoteProduto = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT USALOTEPRODUTO FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, codProduto }));

            if (edita == true)
            {
                ValidaProdOper(UsaAbaLote, UsaLoteProduto);
                FilialLote = Convert.ToInt32(GOPER["CODFILIAL"]);
                LocalLote = GOPER["CODLOCAL"].ToString();
                Codclifor = GOPER["CODCLIFOR"].ToString();
                NseqItemLote = Convert.ToInt32(psTextoBox1.Text);

                LimpaDescricaoLote();
                LimpaQuantidadesLote();
                CarregaDescricoesIniciais();
                ValidaTipoEstoque();
                DiferencaInicial();
                AtribuiQuantidadeEntrada();
                HabilitaExclusaoLote();

                if (ValidaQuantidade() == 0)
                {
                    btnNovoLote.Enabled = false;
                    btnIncluir.Enabled = false;
                    btnExcluirRegistro.Enabled = false;
                    btnExcluirLote.Enabled = true;
                    tbQuantidadeEntrada.Text = "0,0000";
                    tbQuantidadeEntrada.ReadOnly = true;
                    lbQtdlote.Text = getQuantidade().ToString();
                    lbQtdDiferenca.Text = "0,0000";
                    lbQtdDiferenca.ForeColor = Color.Black;
                }
            }
            else
            {
                ValidaProdOper(UsaAbaLote, UsaLoteProduto);
                FilialLote = Convert.ToInt32(GOPER["CODFILIAL"]);
                LocalLote = GOPER["CODLOCAL"].ToString();
                Codclifor = GOPER["CODCLIFOR"].ToString();
                NseqItemLote = Convert.ToInt32(psTextoBox1.Text);

                LimpaDescricaoLote();
                LimpaQuantidadesLote();
                CarregaDescricoesIniciais();
                ValidaTipoEstoque();
                DiferencaInicial();
                AtribuiQuantidadeEntrada();
                HabilitaExclusaoLote();
            }

            #endregion

            if (ValidaBotao == true)
            {
                btnSalvarAtual.Enabled = false;
                btnOKAtual.Enabled = false;
            }
        }

        private void verificaEdicao()
        {
            DataTable dtAcesso = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT *  FROM GPERFILTIPOPER INNER JOIN GUSUARIOPERFIL ON GPERFILTIPOPER.CODPERFIL = GUSUARIOPERFIL.CODPERFIL WHERE GPERFILTIPOPER.CODEMPRESA = ? AND GPERFILTIPOPER.CODTIPOPER = ? AND GUSUARIOPERFIL.CODUSUARIO = ?", new object[] { AppLib.Context.Empresa, codTipOper, AppLib.Context.Usuario });
            if (dtAcesso.Rows.Count > 0)
            {
                for (int i = 0; i < dtAcesso.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dtAcesso.Rows[i]["ALTERAR"]) == false)
                    {
                        btnSalvarAtual.Enabled = false;
                        btnOKAtual.Enabled = false;
                    }
                }
            }
        }

        private void buscaNatureza()
        {
            GetDefaultCliFor(AppLib.Context.Empresa, GOPER["CODCLIFOR"].ToString());
            if (!string.IsNullOrEmpty(codProduto))
            {
                String consulta1 = "SELECT AUTOSELECAONATUREZA FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?";
                int AUTOSELECAONATUREZA = int.Parse(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, consulta1, new Object[] { int.Parse(GOPER["CODEMPRESA"].ToString()), GOPER["CODTIPOPER"].ToString() }).ToString());

                if (AUTOSELECAONATUREZA == 1)
                {
                    string query = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODQUERYNATUREZA FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper }).ToString();
                    String consulta2;

                    if (!string.IsNullOrEmpty(query))
                    {
                        consulta2 = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT QUERY FROM GQUERY WHERE CODEMPRESA = ? AND CODQUERY = ?", new object[] { AppLib.Context.Empresa, query }).ToString();
                        // alterar os parametros

                        consulta2 = consulta2.Replace("@CLASSVENDA2", "'" + psComboBoxAPLICACAOMATERIAL.Value.ToString() + "'");

                        // CONTRIBUINTEICMS
                        int NATUREZA_CONTRIBUINTEICMS = 9;

                        if (int.Parse(VCLIFOR["CONTRIBICMS"].ToString()) == 0)
                        {
                            NATUREZA_CONTRIBUINTEICMS = 1;
                        }

                        if (int.Parse(VCLIFOR["CONTRIBICMS"].ToString()) == 1)
                        {
                            NATUREZA_CONTRIBUINTEICMS = 1;
                        }

                        if (int.Parse(VCLIFOR["CONTRIBICMS"].ToString()) == 2)
                        {
                            NATUREZA_CONTRIBUINTEICMS = 0;
                        }

                        // AUTOMOTIVA OU CONSTRUÇÃO CIVIL
                        int Automotiva = Convert.ToInt32(VCLIFOR["AUTOMOTIVA"]);
                        int ConstrucaoCivil = Convert.ToInt32(VCLIFOR["CONSTRUCAOCIVIL"]);

                        consulta2 = consulta2.Replace("@AUTOMOTIVA", "'" + Automotiva + "'");
                        consulta2 = consulta2.Replace("@CONSTRUCAOCIVIL", "'" + ConstrucaoCivil + "'");

                        consulta2 = consulta2.Replace("@CONTRIBICMS", "'" + NATUREZA_CONTRIBUINTEICMS.ToString() + "'");
                        consulta2 = consulta2.Replace("@UFDEST", "'" + VCLIFOR["CODETD"].ToString() + "'");
                        // Modificado por João Pedro em 06/09/2017, porque os parâmetros abaixo não constavam na fórmula
                        consulta2 = consulta2.Replace("@CODEMPRESA", "'" + GOPER["CODEMPRESA"].ToString() + "'");
                        consulta2 = consulta2.Replace("@CODOPER", "'" + GOPER["CODOPER"].ToString() + "'");
                        consulta2 = consulta2.Replace("@CODFILIAL", "'" + GOPER["CODFILIAL"].ToString() + "'");

                        String consultaUFFilial = "SELECT CODETD FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL IN ( SELECT CODFILIAL FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ? )";
                        String UFFILIAL = AppLib.Context.poolConnection.Get("Start").ExecGetField("SP", consultaUFFilial, new Object[] { int.Parse(GOPER["CODEMPRESA"].ToString()), int.Parse(GOPER["CODEMPRESA"].ToString()), int.Parse(GOPER["CODOPER"].ToString()) }).ToString();

                        // ( UF DESTINO )
                        String UFCLIENTE = VCLIFOR["CODETD"].ToString();

                        int NATUREZA_DENTRODOESTADO = 1;

                        if (UFFILIAL.ToUpper().Equals(UFCLIENTE.ToUpper()))
                        {
                            NATUREZA_DENTRODOESTADO = 1;
                        }
                        else
                        {
                            NATUREZA_DENTRODOESTADO = 0;
                        }
                        consulta2 = consulta2.Replace("@DENTRODOESTADO", "'" + NATUREZA_DENTRODOESTADO.ToString() + "'");
                        consulta2 = consulta2.Replace("@CODIGOPRD", "'" + codProduto + "'");

                        DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(consulta2, new object[] { });

                        if (dt.Rows.Count > 0)
                        {
                            psLookupCODNATUREZA.textBox1.Text = dt.Rows[0]["CODNATUREZA"].ToString();
                            psLookupCODNATUREZA.LoadLookup();
                        }

                    }
                    //                    else
                    //                    {
                    //                        consulta2 = @"SELECT
                    //VNAT.CODNATUREZA,
                    //VNAT.CODNATUREZA +' - '+ VNAT.DESCRICAO DESCRICAO
                    //
                    //FROM VPRODUTO, VREGRAVARCFOP,
                    //
                    //  (SELECT VNATUREZA.CODNATUREZA, VNATUREZA.DESCRICAO, VREGRAICMS.ALIQUOTA, VNATUREZA.CLASSVENDA2
                    //		FROM VNATUREZA, VREGRAICMS
                    //		WHERE VNATUREZA.IDREGRAICMS = VREGRAICMS.IDREGRA
                    //		  AND VNATUREZA.ATIVO = 1
                    //		  AND VNATUREZA.ULTIMONIVEL = 1
                    //		  AND SUBSTRING(VNATUREZA.CLASSVENDA2, 1, 1) = ?
                    //		  AND SUBSTRING(VNATUREZA.CODNATUREZA, 1, 5) IN
                    //
                    //						(SELECT DISTINCT SUBSTRING(VNATUREZA.CODNATUREZA, 1, 5)
                    //						FROM VNATUREZA
                    //						WHERE DENTRODOESTADO = ?
                    //						  AND CONTRIBUINTEICMS = ?)
                    //		) VNAT
                    //WHERE CODPRODUTO = ?
                    //  AND VREGRAVARCFOP.NCM = ?
                    //  AND VREGRAVARCFOP.UFDESTINO = ?
                    //  AND VREGRAVARCFOP.ALIQINTERESTADUAL = VNAT.ALIQUOTA";

                    //                    }

                    //                    // UF FILIAL VS UF CLIENTE
                    //                    String consultaUFFilial = "SELECT CODETD FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL IN ( SELECT CODFILIAL FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ? )";
                    //                    String UFFILIAL = AppLib.Context.poolConnection.Get("Start").ExecGetField("SP", consultaUFFilial, new Object[] { int.Parse(GOPER["CODEMPRESA"].ToString()), int.Parse(GOPER["CODEMPRESA"].ToString()), int.Parse(GOPER["CODOPER"].ToString()) }).ToString();

                    //                    // ( UF DESTINO )
                    //                    String UFCLIENTE = VCLIFOR["CODETD"].ToString();

                    //                    int NATUREZA_DENTRODOESTADO = 1;

                    //                    if (UFFILIAL.ToUpper().Equals(UFCLIENTE.ToUpper()))
                    //                    {
                    //                        NATUREZA_DENTRODOESTADO = 1;
                    //                    }
                    //                    else
                    //                    {
                    //                        NATUREZA_DENTRODOESTADO = 0;
                    //                    }

                    //                    // APLICACAO MATERIAL
                    //                    String SUBSTRING_NATUREZA_CLASSVENDA2 = psComboBoxAPLICACAOMATERIAL.comboBox1.Text.Substring(0, 1);

                    //                    // CONTRIBUINTEICMS
                    //                    int NATUREZA_CONTRIBUINTEICMS = 9;

                    //                    if (int.Parse(VCLIFOR["CONTRIBICMS"].ToString()) == 0)
                    //                    {
                    //                        NATUREZA_CONTRIBUINTEICMS = 1;
                    //                    }

                    //                    if (int.Parse(VCLIFOR["CONTRIBICMS"].ToString()) == 1)
                    //                    {
                    //                        NATUREZA_CONTRIBUINTEICMS = 1;
                    //                    }

                    //                    if (int.Parse(VCLIFOR["CONTRIBICMS"].ToString()) == 2)
                    //                    {
                    //                        NATUREZA_CONTRIBUINTEICMS = 0;
                    //                    }

                    //                    // CÓDIGO DO PRODUTO
                    //                    String CODPRODUTO = psLookupCODPRODUTO.textBox1.Text;

                    //                    // NCM DO PRODUTO
                    //                    String consultaCODNCM = "SELECT CODNCM FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?";
                    //                    String NCM = AppLib.Context.poolConnection.Get("Start").ExecGetField(String.Empty, consultaCODNCM, new Object[] { int.Parse(GOPER["CODEMPRESA"].ToString()), CODPRODUTO }).ToString();

                    //                    // BUSCA A NATUREZA
                    //                    string consultaCompleta = AppLib.Context.poolConnection.Get("Start").ParseCommand(consulta2, new Object[] { SUBSTRING_NATUREZA_CLASSVENDA2, NATUREZA_DENTRODOESTADO, NATUREZA_CONTRIBUINTEICMS, CODPRODUTO, NCM, UFCLIENTE });
                    //                    System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaCompleta, new Object[] { });

                    //                    if (dt.Rows.Count == 0)
                    //                    {
                    //                        AppLib.Windows.FormMessageDefault.ShowInfo("Não foi possível localizar a natureza automaticamente.\r\nSelecione a natureza manualmente.");
                    //                    }

                    //                    if (dt.Rows.Count == 1)
                    //                    {
                    //                        String CODNATUREZA = dt.Rows[0][0].ToString();
                    //                        psLookupCODNATUREZA.textBox1.Text = CODNATUREZA;
                    //                        psLookupCODNATUREZA.LoadLookup();
                    //                    }

                    //                    if (dt.Rows.Count > 1)
                    //                    {
                    //                        AppLib.Windows.FormListaPrompt f = new AppLib.Windows.FormListaPrompt();
                    //                        f.PrimeiroItemNulo = true;
                    //                        Object valor = f.Mostrar("Foi encontrado mais de uma natureza.\r\nSelecione a natureza:", dt);

                    //                        if (f.confirmacao == AppLib.Global.Types.Confirmacao.OK)
                    //                        {
                    //                            String NaturezaSelecionada = valor.ToString();
                    //                            psLookupCODNATUREZA.textBox1.Text = NaturezaSelecionada;
                    //                            psLookupCODNATUREZA.LoadLookup();
                    //                        }
                    //                    }
                }
            }
        }

        private void psPercDesconto_Validated(object sender, EventArgs e)
        {
            if (psPercDesconto.textBox1.Text != "0,00")
            {
                psValorDesconto.textBox1.Text = "0,00";
            }
        }

        private void psValorDesconto_Validated(object sender, EventArgs e)
        {
            if (psValorDesconto.textBox1.Text != "0,00")
            {
                psPercDesconto.textBox1.Text = "0,00";
            }
        }

        private void psComboBox1_SelectedValueChanged_1(object sender, EventArgs e)
        {
            psValorDesconto.Text = "0";
            psPercDesconto.Text = "0";
        }

        private void btnBuscarNatureza_Click(object sender, EventArgs e)
        {
            buscaNatureza();
        }

        private void psComboBoxAPLICACAOMATERIAL_Validated(object sender, EventArgs e)
        {
            psLookupCODNATUREZA.textBox1.Clear();
            psLookupCODNATUREZA.textBox2.Clear();
        }

        private void psComboBox1_Validated(object sender, EventArgs e)
        {

        }

        private void psComboBoxCODTABPRECO_Validated(object sender, EventArgs e)
        {

        }

        private void psComboBoxCODTABPRECO_SelectedValueChanged(object sender, EventArgs e)
        {
            if (psComboBoxCODTABPRECO.SelectedIndex == 0)
            {
                psValorUnitario.Edita = true;
            }
            else
            {
                psValorUnitario.Edita = false;
            }
            if (salvar == false)
            {
                if (edita == false)
                {
                    buscaPreco();
                }
            }

        }

        private void psComboBoxCODTABPRECO_Validating(object sender, CancelEventArgs e)
        {

        }

        private void buscaPreco()
        {
            //if (edita == false)
            //{
            string tabPreco = string.Empty;
            if (psComboBoxCODTABPRECO.SelectedIndex == 0)
            {
                return;
            }
            if (!string.IsNullOrEmpty(codProduto))
            {
                switch (psComboBoxCODTABPRECO.Value.ToString())
                {
                    case "1":
                        tabPreco = "PRECO1";
                        break;
                    case "2":
                        tabPreco = "PRECO2";
                        break;
                    case "3":
                        tabPreco = "PRECO3";
                        break;
                    case "4":
                        tabPreco = "PRECO4";
                        break;
                    case "5":
                        tabPreco = "PRECO5";
                        break;
                    case "6":
                        tabPreco = "TABCLI";
                        break;
                    case "7":
                        tabPreco = "CMEDIO";
                        break;
                    default:
                        break;
                }
            }
            if (tabPreco == "TABCLI")
            {
                string codclifor = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODCLIFOR FROM GOPER	WHERE CODOPER = ? AND CODEMPRESA = ?  AND CODFILIAL = ?", new object[] { codOper, AppLib.Context.Empresa, AppLib.Context.Filial }).ToString();
                if (!string.IsNullOrEmpty(codclifor))
                {
                    psValorUnitario.Text = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT PRECOUNITARIO FROM VCLIFORTABPRECO INNER JOIN VCLIFORTABPRECOITEM ON VCLIFORTABPRECO.CODEMPRESA = VCLIFORTABPRECOITEM.CODEMPRESA AND VCLIFORTABPRECO.IDTABELA = VCLIFORTABPRECOITEM.IDTABELA WHERE VCLIFORTABPRECO.CODEMPRESA = ? AND VCLIFORTABPRECOITEM.CODFILIAL = ? AND VCLIFORTABPRECO.CODCLIFOR = ? AND VCLIFORTABPRECOITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codclifor, codProduto })).ToString();
                    psVlUnitOriginal.textBox1.Text = psValorUnitario.textBox1.Text;
                }
            }
            if (tabPreco == "CMEDIO")
            {
                // Busca fórmula
                string Codquery = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT FORMULACUSTOMEDIO FROM VPARAMETROS WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();
                string query = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT QUERY FROM GQUERY WHERE CODEMPRESA = ? AND CODQUERY = ?", new object[] { AppLib.Context.Empresa, Codquery }).ToString();

                query = query.Replace("@CODEMPRESA", AppLib.Context.Empresa.ToString());
                query = query.Replace("@CODFILIAL", FilialLote.ToString());
                query = query.Replace("@CODLOCAL", "'" + codlocal + "'");
                query = query.Replace("@CODPRODUTO", "'" + codProduto + "'");

                decimal Customedio = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, query));

                psValorUnitario.textBox1.Text = Customedio.ToString();
                psVlUnitOriginal.textBox1.Text = psValorUnitario.textBox1.Text;
            }
            else
            {
                PSPartProdutoData psPartProdutoData = new PSPartProdutoData();
                psPartProdutoData._tablename = "VPRODUTO";
                psPartProdutoData._keys = new string[] { "CODEMPRESA", "CODPRODUTO" };
                psValorUnitario.Text = psPartProdutoData.RetornaPrecoProduto(PS.Lib.Contexto.Session.Empresa.CodEmpresa, codProduto, tabPreco).ToString();
                psVlUnitOriginal.textBox1.Text = psValorUnitario.textBox1.Text;
            }
            // }
        }

        private void psValorUnitario_Validated(object sender, EventArgs e)
        {
            psVlUnitOriginal.textBox1.Text = psValorUnitario.textBox1.Text;
        }

        private void psPRAcrescimo_Validated(object sender, EventArgs e)
        {
            if (psPRAcrescimo.textBox1.Text != "0,00")
            {
                psVLAcrescimo.textBox1.Text = "0,00";
            }
        }

        private void psVLAcrescimo_Validated(object sender, EventArgs e)
        {
            if (psVLAcrescimo.textBox1.Text != "0,00")
            {
                psPRAcrescimo.textBox1.Text = "0,00";
            }
        }

        private void psCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (psCheckBox1.checkBox1.Checked == true)
            {
                psComboBoxCODTABPRECO.Text = "Nenhum";
                psComboBoxCODTABPRECO.Enabled = false;
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                psValorUnitario.Edita = false;
                psComboBox1.Text = " ";
                psPercDesconto.Text = "0";
                psValorDesconto.Text = "0";
                psPRAcrescimo.Text = "0";
                psVLAcrescimo.Text = "0";
                psValorTotalItem.Edita = true;
            }
            else
            {
                //Edita
                if (GTIPOPER["USAVLUNITARIO"].ToString() == "E")
                {
                    psValorUnitario.Visible = true;
                    psValorUnitario.Edita = true;
                }

                //Não Edita
                if (GTIPOPER["USAVLUNITARIO"].ToString() == "N")
                {
                    psValorUnitario.Visible = true;
                    psValorUnitario.Edita = false;
                }

                //Não Mostra
                if (GTIPOPER["USAVLUNITARIO"].ToString() == "M")
                {
                    psValorUnitario.Visible = false;
                    psValorUnitario.Edita = false;
                }
                else
                {
                    psComboBoxCODTABPRECO.Enabled = true;

                    if (GTIPOPER["VLUNITARIOEM"].ToString() == "NENHUM")
                        psComboBoxCODTABPRECO.Value = 0;
                    if (GTIPOPER["VLUNITARIOEM"].ToString() == "PRECO1")
                        psComboBoxCODTABPRECO.Value = 1;
                    if (GTIPOPER["VLUNITARIOEM"].ToString() == "PRECO2")
                        psComboBoxCODTABPRECO.Value = 2;
                    if (GTIPOPER["VLUNITARIOEM"].ToString() == "PRECO3")
                        psComboBoxCODTABPRECO.Value = 3;
                    if (GTIPOPER["VLUNITARIOEM"].ToString() == "PRECO4")
                        psComboBoxCODTABPRECO.Value = 4;
                    if (GTIPOPER["VLUNITARIOEM"].ToString() == "PRECO5")
                        psComboBoxCODTABPRECO.Value = 5;
                    if (GTIPOPER["VLUNITARIOEM"].ToString() == "TABCLI")
                        psComboBoxCODTABPRECO.Value = 6;
                    if (GTIPOPER["VLUNITARIOEM"].ToString() == "CMEDIO")
                        psComboBoxCODTABPRECO.Value = 7;
                }

                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                psValorUnitario.Edita = true;
                psValorTotalItem.Edita = false;
            }

        }

        private void psLookupCODNATUREZA_AfterLookup(object sender, Lib.LookupEventArgs e)
        {

        }

        #endregion

        #region NEW

        private void carregaParametros()
        {

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper });
            DataTable dtCliente = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODTABPRECO, CLASSVENDA FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR AND VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa });
            if (dt.Rows.Count > 0)
            {
                #region Natureza

                if (dt.Rows[0]["USANATUREZA"].ToString() == "N")
                {
                    psLookupCODNATUREZA.Chave = false;
                }

                #endregion

                #region Data Entrega Item

                if (dt.Rows[0]["USADATAENTREGAITEM"].ToString() == "N")
                {
                    dtEntrega.Enabled = false;
                }

                #endregion

                #region Natureza Padrão

                if (psTextoBox1.Text == "0")
                {
                    string UFEmitente = new PSPartOperacaoData().BuscaUFEmitente(Convert.ToInt32(_setDefault[0].Valor), Convert.ToInt32(_setDefault[1].Valor));
                    string UFDestinatario = new PSPartOperacaoData().BuscaUFDestinatario(Convert.ToInt32(_setDefault[0].Valor), Convert.ToInt32(_setDefault[1].Valor));

                    if (!string.IsNullOrEmpty(UFEmitente))
                    {
                        if (!string.IsNullOrEmpty(UFDestinatario))
                        {
                            if (UFEmitente == UFDestinatario)
                            {
                                psLookupCODNATUREZA.Text = dt.Rows[0]["CODNATDENTRO"].ToString();
                                if (psLookupCODNATUREZA.Text != string.Empty)
                                {
                                    psLookupCODNATUREZA.LoadLookup();
                                }
                            }
                            else
                            {
                                psLookupCODNATUREZA.Text = dt.Rows[0]["CODNATFORA"].ToString();
                                if (psLookupCODNATUREZA.Text != string.Empty)
                                {
                                    psLookupCODNATUREZA.LoadLookup();
                                }
                            }
                        }
                    }
                }

                #endregion

                #region Valor Unitario

                if (dt.Rows[0]["USAVLUNITARIO"].ToString() == "N")
                {
                    psValorUnitario.Enabled = false;
                }
                if (edita == false)
                {
                    if (Convert.ToInt32(dtCliente.Rows[0]["CODTABPRECO"]) != 0)
                    {
                        psComboBoxCODTABPRECO.Value = Convert.ToInt32(dtCliente.Rows[0]["CODTABPRECO"]);
                    }

                    else
                    {
                        switch (dt.Rows[0]["VLUNITARIOEM"].ToString())
                        {
                            case "NENHUM":
                                psComboBoxCODTABPRECO.Value = 0;
                                break;
                            case "PRECO1":
                                psComboBoxCODTABPRECO.Value = 1;
                                break;
                            case "PRECO2":
                                psComboBoxCODTABPRECO.Value = 2;
                                break;
                            case "PRECO3":
                                psComboBoxCODTABPRECO.Value = 3;
                                break;
                            case "PRECO4":
                                psComboBoxCODTABPRECO.Value = 4;
                                break;
                            case "PRECO5":
                                psComboBoxCODTABPRECO.Value = 5;
                                break;
                            case "TABCLI":
                                psComboBoxCODTABPRECO.Value = 6;
                                break;
                            case "CMEDIO":
                                psComboBoxCODTABPRECO.Value = 7;
                                break;
                            default:
                                break;
                        }
                    }
                }

                #endregion

                #region Tabela de Preço

                if (dt.Rows[0]["USATABELAPRECO"].ToString() == "N")
                {
                    psComboBoxCODTABPRECO.Enabled = false;
                }

                #endregion

                #region Perc. Acrescimo

                if (dt.Rows[0]["USAPRACRESCIMO"].ToString() == "N")
                {
                    psPRAcrescimo.Enabled = false;
                }

                #endregion

                #region Valor Acrescimo

                if (dt.Rows[0]["USAVLACRESCIMO"].ToString() == "N")
                {
                    psVLAcrescimo.Enabled = false;
                }

                #endregion

                #region Perc. Desconto

                if (dt.Rows[0]["USAPRDESCONTO"].ToString() == "N")
                {
                    psPercDesconto.Edita = false;
                    psComboBox1.Enabled = false;
                }

                #endregion

                #region Valor Desconto

                if (dt.Rows[0]["USAVLDESCONTO"].ToString() == "N")
                {
                    psValorDesconto.Edita = false;
                }

                #endregion

                #region Valor Total Item

                if (dt.Rows[0]["USAVLTOTALITEM"].ToString() == "N")
                {
                    psValorTotalItem.Enabled = false;
                    button2.Enabled = true;
                    psCheckBox1.Enabled = false;
                }

                #endregion

                #region Aplicação Material
                if (dtCliente.Rows.Count > 0)
                {
                    psComboBoxAPLICACAOMATERIAL.Value = dtCliente.Rows[0]["CLASSVENDA"].ToString();
                }
                #endregion

                #region Seleção da Natureza

                if (dt.Rows[0]["AUTOSELECAONATUREZA"].ToString() == "1")
                {
                    btnBuscarNatureza.Enabled = true;
                    psComboBoxAPLICACAOMATERIAL.Enabled = true;
                }
                else
                {
                    btnBuscarNatureza.Enabled = false;
                    psComboBoxAPLICACAOMATERIAL.Enabled = false;
                }



                #endregion

                #region Campo Descrição Complementar
                if (dt.Rows[0]["USADESCRICAOCOMPLEMENTAR"].ToString() == "S")
                {
                    memoEdit1.Enabled = true;
                }
                else
                {
                    memoEdit1.Enabled = false;
                }

                #endregion

                #region Tipo de Desconto
                if (dt.Rows[0]["TIPODESCONTOITEM"].ToString() == "U")
                {
                    psComboBox1.SelectedIndex = 1;
                }
                else if (dt.Rows[0]["TIPODESCONTOITEM"].ToString() == "T")
                {
                    psComboBox1.SelectedIndex = 2;
                }
                else
                {
                    psComboBox1.SelectedIndex = 0;
                }
                #endregion

                #region Campos Valores Adicionais

                if (dt.Rows[0]["USAITEMCAMPOVALOR1"].ToString() == "N")
                {
                    txtCampo1.Enabled = false;
                }
                else
                {
                    txtCampo1.Enabled = true;
                }

                if (dt.Rows[0]["USAITEMCAMPOVALOR2"].ToString() == "N")
                {
                    txtCampo2.Enabled = false;
                }
                else
                {
                    txtCampo2.Enabled = true;
                }

                if (dt.Rows[0]["USAITEMCAMPOVALOR3"].ToString() == "N")
                {
                    txtCampo3.Enabled = false;
                }
                else
                {
                    txtCampo3.Enabled = true;
                }

                if (dt.Rows[0]["USAITEMCAMPOVALOR4"].ToString() == "N")
                {
                    txtCampo4.Enabled = false;
                }
                else
                {
                    txtCampo4.Enabled = true;
                }

                DataTable dtDicionario = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT TEXTOITEMCAMPOVALOR1, TEXTOITEMCAMPOVALOR2, TEXTOITEMCAMPOVALOR3, TEXTOITEMCAMPOVALOR4, TEXTOITEMCAMPOVALOR5, TEXTOITEMCAMPOVALOR6 FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, GOPER["CODTIPOPER"] });

                if (dtDicionario.Rows.Count > 0)
                {
                    lblCampo1.Text = dtDicionario.Rows[0]["TEXTOITEMCAMPOVALOR1"].ToString();
                    lblCampo2.Text = dtDicionario.Rows[0]["TEXTOITEMCAMPOVALOR2"].ToString();
                    lblCampo3.Text = dtDicionario.Rows[0]["TEXTOITEMCAMPOVALOR3"].ToString();
                    lblCampo4.Text = dtDicionario.Rows[0]["TEXTOITEMCAMPOVALOR4"].ToString();
                }

                DataTable DtDicionarioCompl = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA, DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPERITEMCOMPL'", new object[] { });
                for (int i = 0; i < DtDicionarioCompl.Rows.Count; i++)
                {
                    if (DESCCOMPLPRODUTO.Text == DtDicionarioCompl.Rows[i]["COLUNA"].ToString())
                    {
                        DESCCOMPLPRODUTO.Text = DtDicionarioCompl.Rows[i]["DESCRICAO"].ToString();
                    }
                }
                #endregion
            }
        }

        private void getNseq()
        {
            int nseq = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT MAX(NSEQITEM) NSEQITEM FROM GOPERITEM WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }));
            psTextoBox1.textBox1.Text = (nseq + 1).ToString();
        }

        private void AtribuiZero()
        {
            psValorTotalItem.textBox1.Text = "0,00";
            psValorUnitario.textBox1.Text = "0,00";
            psQuantidade.textBox1.Text = "0,00";
            psVlUnitOriginal.textBox1.Text = "0,00";
            psVLAcrescimo.textBox1.Text = "0,00";
            psPRAcrescimo.textBox1.Text = "0,00";
            psValorDesconto.textBox1.Text = "0,00";
            psPercDesconto.textBox1.Text = "0,00";
            psMoedaBox1.textBox1.Text = "0,00";
            psMoedaBox2.textBox1.Text = "0,00";
            psMoedaBox3.textBox1.Text = "0,00";
            psMoedaBox4.textBox1.Text = "0,00";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        private bool ValidaDesconto()
        {
            bool valida = true;

            decimal LimiteDesc = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT LIMITEDESC FROM GOPER WHERE CODOPER = ? AND CODEMPRESA = ? ", new object[] { codOper, AppLib.Context.Empresa }));

            if (Convert.ToDecimal(psPercDesconto.textBox1.Text) > LimiteDesc)
            {
                if (MessageBox.Show("O desconto inserido é maior que o disponível para essa Operação, deseja continuar?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    valida = false;
                }
            }

            return valida;
        }

        private bool ValidaFatorConversao()
        {
            int Validacao;
            string BuscaFatorConversao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT FATORCONVERSAO FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codTipOper, AppLib.Context.Empresa }).ToString();

            if (BuscaFatorConversao == "PRODUTO")
            {
                Validacao = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODUNID) FROM VPRODUTOUNIDADE WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODUNID = ? AND CODUNIDCONVERSAO = ? ", new object[] { AppLib.Context.Empresa, lookupproduto.txtcodigo.Text, lpUnidadeMedida.txtcodigo.Text, tbUnidadeControle.Text }));

                if (Validacao == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (BuscaFatorConversao == "PADRAO")
            {
                Validacao = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODUNID) FROM VUNIDCONVERSAO WHERE CODEMPRESA = ? AND CODUNID = ? AND CODUNIDCONVERSAO = ?", new object[] { AppLib.Context.Empresa, lpUnidadeMedida.txtcodigo.Text, tbUnidadeControle.Text }));

                if (Validacao == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool SalvaItens()
        {
            if (ValidaDesconto() == false)
            {
                return false;
            }

            if (tbUnidadeControle.Text != lpUnidadeMedida.txtcodigo.Text)
            {
                if (ValidaFatorConversao() == false)
                {
                    MessageBox.Show("Não existe Fator de Conversão disponível.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (Convert.ToDecimal(psQuantidade.textBox1.Text) <= 0)
            {
                MessageBox.Show("Informe ao menos uma quantidade para poder salvar o item.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //Verifica o parametro 
            string aceitaPreco = AppLib.Context.poolConnection.Get("Start").ExecGetField("1", @"SELECT ACEITAPRECOZERO FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codTipOper, AppLib.Context.Empresa }).ToString();
            if (aceitaPreco == "1")
            {
                if (Convert.ToDecimal(psValorUnitario.textBox1.Text) < 0)
                {
                    MessageBox.Show("O valor unitário não pode ser 0.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (dtEntrega.Enabled == true)
            {
                //Verifica se a data de entrega é maior que a data de emissão da operação.
                DateTime dataEmissao = Convert.ToDateTime(AppLib.Context.poolConnection.Get("Start").ExecGetField(AppLib.Context.poolConnection.Get("Start").GetDateTime(), "SELECT DATAEMISSAO FROM GOPER WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }));
                if (Convert.ToDateTime(dataEmissao.ToShortDateString()) > dtEntrega.DateTime)
                {
                    MessageBox.Show("Data de entrega precisa ser maior que a data de emissão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            #region Validações do Lote

            if (UsaAbaLote == true && UsaLoteProduto == true)
            {
                if (lbQtdDiferenca.Text != "0,0000")
                {
                    MessageBox.Show("Quantidade de entrada divergente com a quantidade do lote", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            #endregion

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();
            try
            {
                AppLib.ORM.Jit GOPERITEM = new AppLib.ORM.Jit(conn, "GOPERITEM");
                GOPERITEM.Set("CODEMPRESA", AppLib.Context.Empresa);
                GOPERITEM.Set("CODOPER", codOper);
                GOPERITEM.Set("NSEQITEM", psTextoBox1.textBox1.Text);
                //GOPERITEM.Set("CODPRODUTO", psLookupCODPRODUTO.textBox1.Text);
                GOPERITEM.Set("CODPRODUTO", lookupproduto.ValorCodigoInterno);
                GOPERITEM.Set("QUANTIDADE", string.IsNullOrEmpty(psQuantidade.textBox1.Text) ? 0 : Convert.ToDecimal(psQuantidade.textBox1.Text));
                GOPERITEM.Set("VLUNITARIO", string.IsNullOrEmpty(psValorUnitario.textBox1.Text) ? 0 : Convert.ToDecimal(psValorUnitario.textBox1.Text));
                GOPERITEM.Set("VLDESCONTO", string.IsNullOrEmpty(psValorDesconto.textBox1.Text) ? 0 : Convert.ToDecimal(psValorDesconto.textBox1.Text));
                GOPERITEM.Set("PRDESCONTO", string.IsNullOrEmpty(psPercDesconto.textBox1.Text) ? 0 : Convert.ToDecimal(psPercDesconto.textBox1.Text));
                GOPERITEM.Set("VLTOTALITEM", string.IsNullOrEmpty(psValorTotalItem.textBox1.Text) ? 0 : Convert.ToDecimal(psValorTotalItem.textBox1.Text));

                // João Pedro 19/10/2017
                if (Codstatus == "0")
                {
                    GOPERITEM.Set("QUANTIDADE_SALDO", GOPERITEM.Get("QUANTIDADE"));
                }
                else if (Codstatus == "5")
                {
                    decimal Quantidade_Faturado = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT QUANTIDADE_FATURADO FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?", new object[] { AppLib.Context.Empresa, codOper, nseqitem }).ToString());
                    decimal Quantidade = Convert.ToDecimal(psQuantidade.textBox1.Text) - Quantidade_Faturado;
                    GOPERITEM.Set("QUANTIDADE_SALDO", Quantidade);
                }
                //GOPERITEM.Set("QUANTIDADE_SALDO", GOPERITEM.Get("QUANTIDADE"));

                if (GTIPOPER["USANATUREZA"].ToString() == "N")
                {
                    GOPERITEM.Set("CODNATUREZA", null);
                }
                else
                {
                    GOPERITEM.Set("CODNATUREZA", psLookupCODNATUREZA.textBox1.Text);
                }

                //17/01/2018
                //GOPERITEM.Set("CODUNIDOPER", psLookup6.textBox1.Text);
                GOPERITEM.Set("CODUNIDOPER", lpUnidadeMedida.txtcodigo.Text);
                GOPERITEM.Set("OBSERVACAO", string.IsNullOrEmpty(psMemoBox1.Text) ? null : psMemoBox1.Text);
                GOPERITEM.Set("INFCOMPL", string.IsNullOrEmpty(psMemoBox2.Text) ? null : psMemoBox2.Text);
                GOPERITEM.Set("CODTABPRECO", psComboBoxCODTABPRECO.Value);
                GOPERITEM.Set("NOMEPRODUTO", lookupproduto.txtconteudo.Text);

                GOPERITEM.Set("APLICACAOMATERIAL", psComboBoxAPLICACAOMATERIAL.Value);
                GOPERITEM.Set("VLACRESCIMO", string.IsNullOrEmpty(psVLAcrescimo.textBox1.Text) ? 0 : Convert.ToDecimal(psVLAcrescimo.textBox1.Text));
                GOPERITEM.Set("PRACRESCIMO", string.IsNullOrEmpty(psPRAcrescimo.textBox1.Text) ? 0 : Convert.ToDecimal(psPRAcrescimo.textBox1.Text));
                GOPERITEM.Set("TIPODESCONTO", psComboBox1.Value);
                GOPERITEM.Set("VLUNITORIGINAL", string.IsNullOrEmpty(psVlUnitOriginal.textBox1.Text) ? 0 : Convert.ToDecimal(psVlUnitOriginal.textBox1.Text));
                GOPERITEM.Set("RATEIODESPESA", string.IsNullOrEmpty(psMoedaBox1.textBox1.Text) ? 0 : Convert.ToDecimal(psMoedaBox1.textBox1.Text));
                GOPERITEM.Set("RATEIODESCONTO", string.IsNullOrEmpty(psMoedaBox2.textBox1.Text) ? 0 : Convert.ToDecimal(psMoedaBox2.textBox1.Text));
                GOPERITEM.Set("RATEIOFRETE", string.IsNullOrEmpty(psMoedaBox3.textBox1.Text) ? 0 : Convert.ToDecimal(psMoedaBox3.textBox1.Text));
                GOPERITEM.Set("RATEIOSEGURO", string.IsNullOrEmpty(psMoedaBox4.textBox1.Text) ? 0 : Convert.ToDecimal(psMoedaBox4.textBox1.Text));

                GOPERITEM.Set("CAMPOVALOR1", string.IsNullOrEmpty(txtCampo1.Text) ? 0 : Convert.ToDecimal(txtCampo1.Text));
                GOPERITEM.Set("CAMPOVALOR2", string.IsNullOrEmpty(txtCampo2.Text) ? 0 : Convert.ToDecimal(txtCampo2.Text));
                GOPERITEM.Set("CAMPOVALOR3", string.IsNullOrEmpty(txtCampo3.Text) ? 0 : Convert.ToDecimal(txtCampo3.Text));
                GOPERITEM.Set("CAMPOVALOR4", string.IsNullOrEmpty(txtCampo4.Text) ? 0 : Convert.ToDecimal(txtCampo4.Text));

                GOPERITEM.Set("XPED", txtXPED.Text);
                GOPERITEM.Set("NITEMPED", string.IsNullOrEmpty(txtNITEMPED.Text) ? 0 : Convert.ToInt32(txtNITEMPED.Text));
                if (!string.IsNullOrEmpty(dtEntrega.Text))
                {
                    GOPERITEM.Set("DATAENTREGA", Convert.ToDateTime(dtEntrega.Text));
                }
                GOPERITEM.Set("TOTALEDITADO", psCheckBox1.Checked == true ? 1 : 0);

                GOPERITEM.Set("CODUNIDCONTROLE", tbUnidadeControle.Text);
                GOPERITEM.Set("QUANTIDADECONTROLE", Convert.ToDecimal(tbQuantidadeConversao.Text));
                GOPERITEM.Set("FATORCONVERSAO", Convert.ToDecimal(tbFatorConversao.Text));

                #region Importação

                GOPERITEM.Set("NUMERODI", txtNUMERODI.Text);
                if (!string.IsNullOrEmpty(dteDATADI.Text))
                {
                    GOPERITEM.Set("DATADI", Convert.ToDateTime(dteDATADI.Text));
                }
                else
                {
                    GOPERITEM.Set("DATADI", null);
                }
                if (!string.IsNullOrEmpty(dteDATADESEMB.Text))
                {
                    GOPERITEM.Set("DATADESEMB", Convert.ToDateTime(dteDATADESEMB.Text));
                }
                else
                {
                    GOPERITEM.Set("DATADESEMB", null);
                }
                GOPERITEM.Set("LOCDESEMB", TXTLOCDESEMB.Text);
                GOPERITEM.Set("UFDESEMB", TXTUFDESEMB.Text);
                GOPERITEM.Set("CODEXPORTADOR", txtCODEXPORTADOR.Text);
                GOPERITEM.Set("NUMADICAO", txtNUMADICAO.Text);
                GOPERITEM.Set("NUMSEQADIC", txtNUMSEQADIC.Text);
                GOPERITEM.Set("CODFABRICANTE", txtCODFABRICANTE.Text);
                if (string.IsNullOrEmpty(txtVLORDESCDI.Text))
                {
                    GOPERITEM.Set("VLORDESCDI", Convert.ToDecimal("0"));
                }
                else
                {
                    GOPERITEM.Set("VLORDESCDI", Convert.ToDecimal(txtVLORDESCDI.Text));
                }
                if (cmbTPVIATRANSP.SelectedValue != null)
                {
                    GOPERITEM.Set("TPVIATRANSP", cmbTPVIATRANSP.SelectedValue.ToString());
                }

                if (string.IsNullOrEmpty(txtVAFRMM.Text))
                {
                    GOPERITEM.Set("VAFRMM", Convert.ToDecimal("0"));
                }
                else
                {
                    GOPERITEM.Set("VAFRMM", Convert.ToDecimal(txtVAFRMM.Text));
                }
                if (cmbTPINTERMEDIO.SelectedValue != null)
                {
                    GOPERITEM.Set("TPINTERMEDIO", cmbTPINTERMEDIO.SelectedValue.ToString());
                }

                GOPERITEM.Set("CNPJ", txtCNPJimportacao.Text);
                GOPERITEM.Set("UFTERCEIRO", txtUFTERCEIRO.Text);
                GOPERITEM.Set("NDRAW", txtNDRAW.Text);

                #endregion

                #region IBPTAX
                DataTable dtIBPTAX = conn.ExecQuery(@"
SELECT 
	UF, 
	NACIONALFEDERAL, 
	IMPORTADOSFEDERAL, 
	ESTADUAL, 
	MUNICIPAL, 
	CHAVE 
FROM 
	VIBPTAX 
WHERE 
	UF = (SELECT GFILIAL.CODETD FROM GFILIAL INNER JOIN GOPER ON GFILIAL.CODFILIAL = GOPER.CODFILIAL AND GFILIAL.CODEMPRESA = GOPER.CODEMPRESA WHERE GOPER.CODOPER = ?) 
	AND CODIGO = (SELECT CODNCM FROM VPRODUTO INNER JOIN GOPERITEM ON VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO AND VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODOPER = ? AND GOPERITEM.NSEQITEM = ?)", new object[] { GOPERITEM.Get("CODOPER"), GOPERITEM.Get("CODEMPRESA"), GOPERITEM.Get("CODOPER"), GOPERITEM.Get("NSEQITEM") });

                if (dtIBPTAX.Rows.Count > 0)
                {
                    GOPERITEM.Set("UFIBPTAX", dtIBPTAX.Rows[0]["UF"]);
                    GOPERITEM.Set("NACIONALFEDERALIBPTAX", dtIBPTAX.Rows[0]["NACIONALFEDERAL"]);
                    GOPERITEM.Set("IMPORTADOSFEDERALIBPTAX", dtIBPTAX.Rows[0]["IMPORTADOSFEDERAL"]);
                    GOPERITEM.Set("ESTADUALIBPTAX", dtIBPTAX.Rows[0]["ESTADUAL"]);
                    GOPERITEM.Set("MUNICIPALIBPTAX", dtIBPTAX.Rows[0]["MUNICIPAL"]);
                    GOPERITEM.Set("CHAVEIBPTAX", dtIBPTAX.Rows[0]["CHAVE"]);
                }

                #endregion

                GOPERITEM.Save();

                if (SalvaItensCompl(conn) == true)
                {
                    conn.Commit();

                    #region Estoque
                    if (edita == true)
                    {
                        int retorno = Convert.ToInt32(conn.ExecGetField(0, "SELECT COUNT(CODEMPRESA) FROM VFICHAESTOQUE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODPRODUTO = ? AND NSEQITEM = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, GOPERITEM.Get("CODPRODUTO"), GOPERITEM.Get("NSEQITEM"), codOper }));
                        if (retorno > 0)
                        {
                            conn.ExecTransaction("DELETE FROM VFICHAESTOQUE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODPRODUTO = ? AND NSEQITEM = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, GOPERITEM.Get("CODPRODUTO"), GOPERITEM.Get("NSEQITEM"), codOper });
                        }
                    }

                    string tipoEstoque = conn.ExecGetField("N", "SELECT OPERESTOQUE FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codTipOper, AppLib.Context.Empresa }).ToString();
                    if (tipoEstoque != "N")
                    {
                        PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                        psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
                        psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };

                        PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque Estoque;
                        Estoque = PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao;

                        DataTable dtLocal = conn.ExecQuery(@"SELECT CODLOCAL, CODLOCALENTREGA FROM GOPER WHERE CODOPER = ? AND CODEMPRESA =? ", new object[] { codOper, AppLib.Context.Empresa });

                        //psPartLocalEstoqueSaldoData.MovimentaEstoque(AppLib.Context.Empresa, AppLib.Context.Filial, dtLocal.Rows[0]["CODLOCAL"].ToString(), dtLocal.Rows[0]["CODLOCALENTREGA"].ToString(), GOPERITEM.Get("CODPRODUTO").ToString(), Convert.ToDecimal(GOPERITEM.Get("QUANTIDADE")), GOPERITEM.Get("CODUNIDOPER").ToString(), PSPartLocalEstoqueSaldoData.Tipo.Diminui);
                        psPartLocalEstoqueSaldoData.MovimentaEstoque2(Estoque, AppLib.Context.Empresa, codOper, Convert.ToInt32(GOPERITEM.Get("NSEQITEM")));
                    }

                    #endregion

                    nseqitem = Convert.ToInt32(psTextoBox1.textBox1.Text);
                    codNatureza = psLookupCODNATUREZA.textBox1.Text;
                    if (!string.IsNullOrEmpty(codNatureza))
                    {
                        if (ValidaItensOperacao(codOper) == true)
                        {
                            geraTributo();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Não foi possível inserir o item.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Rollback();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("VNATUREZA"))
                {
                    MessageBox.Show("Para salvar o item é necessário informar a Natureza de Operação.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Rollback();
                return false;
            }

        }

        private bool ValidaItensOperacao(int Codoper)
        {
            bool GeraTributo = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT GERATRIBUTOS FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper }));

            if (GeraTributo == true)
            {
                DataTable dtItensOperacao = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codOper });

                if (dtItensOperacao.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool SalvaItensCompl(AppLib.Data.Connection conn)
        {
            AppLib.ORM.Jit GOPERITEMCOMPL = new AppLib.ORM.Jit(conn, "GOPERITEMCOMPL");
            try
            {
                GOPERITEMCOMPL.Set("CODEMPRESA", AppLib.Context.Empresa);
                GOPERITEMCOMPL.Set("CODOPER", codOper);
                GOPERITEMCOMPL.Set("NSEQITEM", psTextoBox1.textBox1.Text);
                GOPERITEMCOMPL.Set("DESCCOMPLPRODUTO", memoEdit1.Text);

                GOPERITEMCOMPL.Save();

                // return true;
            }
            catch (Exception)
            {
                return false;
            }
            if (tabPage3.Controls.Count > 0)
            {
                Class.Utilidades util = new Class.Utilidades();
                string sql = util.update(this, tabPage3, "GOPERITEMCOMPL");
                if (!string.IsNullOrEmpty(sql))
                {
                    sql = sql.Remove(sql.Length - 2, 1) + " WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";
                    //sql = sql + " WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";
                    conn.ExecTransaction(sql, new object[] { AppLib.Context.Empresa, codOper, psTextoBox1.textBox1.Text });

                }
                return true;
            }
            else
            {
                return true;
            }
        }

        public void geraTributo()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();

            try
            {

                conn.BeginTransaction();

                #region variavel
                string insert = string.Empty;
                decimal TRB_VALORICMSST = 0;
                decimal TRB_FATORMVA = 0;
                decimal TRB_BCORIGINAL = 0;
                decimal TRB_REDUCAOBASEICMSST = 0;
                decimal pdif = 0;
                decimal TRB_ALIQUOTA = 0;
                decimal TRB_VALOR_ALIQUOTA = 0;
                string TRB_CST = string.Empty;
                int? TRB_MODBC = null;
                decimal TRB_REDBC = 0;
                string TRB_CENQ = string.Empty;
                string sSql = "";
                decimal icms = 0;
                //List<Class.Tributo> ListaTributo = new List<Class.Tributo>();
                decimal TRB_BASE_CALCULO = 0;

                DataTable dtTipOper = conn.ExecQuery("SELECT GERATRIBUTOS, TIPENTSAI FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codTipOper, AppLib.Context.Empresa });

                DataTable dtTipoTributo = conn.ExecQuery(@"SELECT CODTRIBUTO, ALIQUOTA, CODCST FROM GTIPOPERTRIBUTO INNER JOIN GQUERY ON GTIPOPERTRIBUTO.CODQUERY = GQUERY.CODQUERY AND GTIPOPERTRIBUTO.CODEMPRESA = GQUERY.CODEMPRESA WHERE GTIPOPERTRIBUTO.CODEMPRESA = ? AND GTIPOPERTRIBUTO.CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper });
                DataTable VTRIBUTO = conn.ExecQuery(@"SELECT ALIQUOTAEM, ALIQUOTA, CODTIPOTRIBUTO FROM VTRIBUTO WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });
                DataTable VREGRAICMS = conn.ExecQuery(@"SELECT VREGRAICMS.ALIQUOTA, VNATUREZA.CODNATUREZA
FROM VREGRAICMS 
INNER JOIN VNATUREZA ON VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS
WHERE VNATUREZA.CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });

                VREGRAICMS.PrimaryKey = new DataColumn[] { VREGRAICMS.Columns["CODNATUREZA"] };
                VTRIBUTO.PrimaryKey = new DataColumn[] { VTRIBUTO.Columns["CODTIPOTRIBUTO"] };



                if (VTRIBUTO.Rows.Count <= 0)
                {
                    MessageBox.Show("Não foi possível carregar os tributos do Tipo da Operação selecionada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dtTipOper.Rows.Count <= 0)
                {
                    MessageBox.Show("Não foi possível carregar os parâmetros do Tipo da Operação selecionada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (dtTipoTributo.Rows.Count <= 0)
                {
                    MessageBox.Show("Não foi possível carregar os tributos da Operação selecionada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                #endregion

                //Verificar se altera rateio

                conn.ExecTransaction("DELETE FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ? AND EDITADO = 0", new object[] { AppLib.Context.Empresa, codOper, psTextoBox1.textBox1.Text });

                string UFDESTINATARIO = conn.ExecGetField(string.Empty, @"SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();

                if (dtTipOper.Rows[0]["GERATRIBUTOS"].ToString() == "1")
                {
                    //pra cada tributo
                    for (int iTributo = 0; iTributo < dtTipoTributo.Rows.Count; iTributo++)
                    {

                        #region Base de Calculo

                        string query = conn.ExecGetField(string.Empty, "SELECT QUERY FROM GTIPOPERTRIBUTO INNER JOIN GQUERY ON GTIPOPERTRIBUTO.CODEMPRESA = GQUERY.CODEMPRESA AND GTIPOPERTRIBUTO.CODQUERY = GQUERY.CODQUERY WHERE GTIPOPERTRIBUTO.CODTIPOPER  = ? AND GTIPOPERTRIBUTO.CODEMPRESA = ? AND GTIPOPERTRIBUTO.CODTRIBUTO = ?", new object[] { codTipOper, AppLib.Context.Empresa, dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }).ToString();

                        query = query.Replace("@CODEMPRESA", "'" + AppLib.Context.Empresa + "'").Replace("@CODNATUREZA", "'" + psLookupCODNATUREZA.textBox1.Text + "'").Replace("@CODTRIBUTO", "'" + dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString() + "'").Replace("@CODOPER", "'" + codOper + "'").Replace("@NSEQITEM", "'" + psTextoBox1.textBox1.Text + "'").Replace("@UFDESTINO", "'" + UFDESTINATARIO + "'");

                        TRB_BASE_CALCULO = Convert.ToDecimal(conn.ExecGetField(0, query, new object[] { }));
                        TRB_BCORIGINAL = TRB_BASE_CALCULO;
                        #endregion

                        #region Aliquota
                        // Verifica o ipi para buscar ou não a aliquota
                        string tipoTributacaoAliquota = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT VREGRAIPI.TIPOTRIBUTACAO FROM VREGRAIPI INNER JOIN VNATUREZA ON VREGRAIPI.IDREGRA = VNATUREZA.IDREGRAIPI WHERE VNATUREZA.CODNATUREZA = ? AND VNATUREZA.CODEMPRESA = ? ", new object[] { psLookupCODNATUREZA.textBox1.Text, AppLib.Context.Empresa }).ToString();
                        DataRow result = VTRIBUTO.Rows.Find(new object[] { dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString() });

                        //verifica se é ipi e se for entra na regra.
                        if (result["CODTIPOTRIBUTO"].ToString() == "IPI")
                        {
                            if (tipoTributacaoAliquota.Equals("T") || tipoTributacaoAliquota.Equals("M"))
                            {
                                switch (result["ALIQUOTAEM"].ToString())
                                {
                                    case "0":
                                        TRB_ALIQUOTA = Convert.ToDecimal(result["ALIQUOTA"].ToString());
                                        break;
                                    //Produto
                                    case "1":
                                        TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ALIQUOTA FROM VPRODUTOTRIBUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, codProduto, dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }));
                                        break;
                                    //Tipo Operação
                                    case "2":
                                        TRB_ALIQUOTA = Convert.ToDecimal(dtTipoTributo.Rows[iTributo]["ALIQUOTA"].ToString());
                                        break;
                                    //Natureza
                                    case "3":
                                        TRB_ALIQUOTA = 0;

                                        if (string.IsNullOrEmpty(psLookupCODNATUREZA.textBox1.Text))
                                        {
                                            MessageBox.Show("CFOP não localizada no item da operação " + psTextoBox1.textBox1.Text + ".", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }

                                        if (result["CODTIPOTRIBUTO"] == DBNull.Value)
                                        {
                                            MessageBox.Show("Tipo de tributo para o tributo " + dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString() + " não informado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                        else
                                        {
                                            if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                                            {
                                                DataRow ALIQUOTA = VREGRAICMS.Rows.Find(new object[] { psLookupCODNATUREZA.textBox1.Text });
                                                TRB_ALIQUOTA = Convert.ToDecimal(ALIQUOTA["ALIQUOTA"]);
                                                //TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ALIQUOTA FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"] }));
                                            }
                                            else
                                            {

                                                TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ALIQUOTA FROM VNATUREZATRIBUTO WHERE CODEMPRESA = ? AND CODNATUREZA = ? AND CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, psLookupCODNATUREZA.textBox1.Text, dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }));
                                            }
                                        }
                                        break;
                                    //Estado
                                    case "4":

                                        string codetd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();

                                        if (!string.IsNullOrEmpty(codetd))
                                        {
                                            TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ALIQUOTA FROM VPRODUTOTRIBUTOESTADO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODTRIBUTO = ? AND CODESTADO = ?", new object[] { AppLib.Context.Empresa, codProduto, dtTipoTributo.Rows[iTributo]["CODTRIBUTO"], codetd }));
                                        }
                                        else
                                        {
                                            TRB_ALIQUOTA = 0;
                                        }
                                        break;
                                    case "5":
                                        string codetd1 = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();
                                        string ncm = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODNCM FROM VPRODUTO INNER JOIN GOPERITEM ON VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA AND VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, codProduto }).ToString();

                                        TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT VREGRAVARCFOP.ALIQINTERNA FROM VREGRAVARCFOP WHERE NCM = ? AND UFDESTINO = ?", new object[] { ncm, codetd1 }));

                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            switch (result["ALIQUOTAEM"].ToString())
                            {
                                case "0":
                                    TRB_ALIQUOTA = Convert.ToDecimal(result["ALIQUOTA"].ToString());
                                    break;
                                //Produto
                                case "1":
                                    TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ALIQUOTA FROM VPRODUTOTRIBUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, codProduto, dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }));
                                    break;
                                //Tipo Operação
                                case "2":
                                    TRB_ALIQUOTA = Convert.ToDecimal(dtTipoTributo.Rows[iTributo]["ALIQUOTA"].ToString());
                                    break;
                                //Natureza
                                case "3":
                                    TRB_ALIQUOTA = 0;

                                    if (string.IsNullOrEmpty(psLookupCODNATUREZA.textBox1.Text))
                                    {
                                        MessageBox.Show("CFOP não localizada no item da operação " + psTextoBox1.textBox1.Text + ".", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                    if (result["CODTIPOTRIBUTO"] == DBNull.Value)
                                    {
                                        MessageBox.Show("Tipo de tributo para o tributo " + dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString() + " não informado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    else
                                    {
                                        if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                                        {
                                            DataRow ALIQUOTA = VREGRAICMS.Rows.Find(new object[] { psLookupCODNATUREZA.textBox1.Text });
                                            TRB_ALIQUOTA = Convert.ToDecimal(ALIQUOTA["ALIQUOTA"]);
                                            //TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ALIQUOTA FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"] }));
                                        }
                                        else
                                        {

                                            TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ALIQUOTA FROM VNATUREZATRIBUTO WHERE CODEMPRESA = ? AND CODNATUREZA = ? AND CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, psLookupCODNATUREZA.textBox1.Text, dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }));
                                        }
                                    }
                                    break;
                                //Estado
                                case "4":

                                    string codetd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();

                                    if (!string.IsNullOrEmpty(codetd))
                                    {
                                        TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ALIQUOTA FROM VPRODUTOTRIBUTOESTADO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODTRIBUTO = ? AND CODESTADO = ?", new object[] { AppLib.Context.Empresa, codProduto, dtTipoTributo.Rows[iTributo]["CODTRIBUTO"], codetd }));
                                    }
                                    else
                                    {
                                        TRB_ALIQUOTA = 0;
                                    }
                                    break;
                                case "5":
                                    string codetd1 = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();
                                    string ncm = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODNCM FROM VPRODUTO INNER JOIN GOPERITEM ON VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA AND VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, codProduto }).ToString();

                                    TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT VREGRAVARCFOP.ALIQINTERNA FROM VREGRAVARCFOP WHERE NCM = ? AND UFDESTINO = ?", new object[] { ncm, codetd1 }));

                                    break;
                                default:
                                    break;
                            }
                        }


                        #endregion

                        #region Valor Aliquota

                        bool utilizaST = Convert.ToBoolean(conn.ExecGetField(false, "SELECT VREGRAICMS.UTILIZAST FROM VREGRAICMS INNER JOIN VNATUREZA ON VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA WHERE VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, psLookupCODNATUREZA.textBox1.Text }));

                        if (TRB_BASE_CALCULO == 0 || TRB_ALIQUOTA == 0)
                        {
                            TRB_VALOR_ALIQUOTA = 0;
                        }
                        else
                        {
                            TRB_VALOR_ALIQUOTA = ((TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100);
                        }
                        if (result["CODTIPOTRIBUTO"].ToString() == "ICMS-ST")
                        {
                            if (utilizaST == false)
                            {
                                TRB_ALIQUOTA = 0;
                            }
                        }

                        if (dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString() == "ICMS")
                        {
                            icms = TRB_VALOR_ALIQUOTA;
                        }
                        #endregion

                        #region ICMS-ST
                        decimal TRB_VICMSDIF = 0;
                        // Verificação do ICMS - ST


                        if (utilizaST == true)
                        {
                            if (result["ALIQUOTAEM"].ToString() == "5")
                            {
                                DataTable selecaoMVA = conn.ExecQuery(@"SELECT REDUCAOBCST, SELECAOMVAST FROM 
                                                                    VREGRAICMS 
                                                                    INNER JOIN VNATUREZA ON VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA
                                                                    WHERE 
                                                                    VNATUREZA.CODNATUREZA = ?
                                                                    AND VNATUREZA.CODEMPRESA = ?", new object[] { psLookupCODNATUREZA.textBox1.Text, AppLib.Context.Empresa });

                                if (selecaoMVA.Rows.Count > 0)
                                {
                                    TRB_REDUCAOBASEICMSST = Convert.ToDecimal(selecaoMVA.Rows[0]["REDUCAOBCST"]);
                                    string codetd1 = conn.ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();
                                    string ncm = conn.ExecGetField(string.Empty, "SELECT CODNCM FROM VPRODUTO INNER JOIN GOPERITEM ON VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA AND VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, codProduto }).ToString();
                                    DataTable dt = conn.ExecQuery(@"SELECT MVAAJUSTADO, MVAORIGINAL, MVAAJUSTADOMATIMPORT FROM VREGRAVARCFOP WHERE NCM = ? AND UFDESTINO = ?", new object[] { ncm, codetd1 });

                                    switch (selecaoMVA.Rows[0]["SELECAOMVAST"].ToString())
                                    {
                                        case "O":
                                            //TRB_BASE_CALCULO = TRB_BASE_CALCULO + (TRB_BASE_CALCULO * (Convert.ToDecimal(dt.Rows[0]["MVAORIGINAL"]) / 100));
                                            TRB_BASE_CALCULO = TRB_BASE_CALCULO * (1 - (Convert.ToDecimal(selecaoMVA.Rows[0]["REDUCAOBCST"]) / 100));
                                            TRB_VALORICMSST = ((TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100) - icms;
                                            TRB_VALOR_ALIQUOTA = (TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100;
                                            TRB_FATORMVA = Convert.ToDecimal(dt.Rows[0]["MVAORIGINAL"]);
                                            break;
                                        case "A":
                                            //TRB_BASE_CALCULO = TRB_BASE_CALCULO + (TRB_BASE_CALCULO * (Convert.ToDecimal(dt.Rows[0]["MVAAJUSTADO"]) / 100));
                                            TRB_BASE_CALCULO = TRB_BASE_CALCULO * (1 - (Convert.ToDecimal(selecaoMVA.Rows[0]["REDUCAOBCST"]) / 100));
                                            TRB_VALORICMSST = ((TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100) - icms;
                                            TRB_VALOR_ALIQUOTA = (TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100;
                                            TRB_FATORMVA = Convert.ToDecimal(dt.Rows[0]["MVAAJUSTADO"]);
                                            break;
                                        case "I":
                                            //TRB_BASE_CALCULO = TRB_BASE_CALCULO + (TRB_BASE_CALCULO * (Convert.ToDecimal(dt.Rows[0]["MVAAJUSTADOMATIMPORT"]) / 100));
                                            TRB_BASE_CALCULO = TRB_BASE_CALCULO * (1 - (Convert.ToDecimal(selecaoMVA.Rows[0]["REDUCAOBCST"]) / 100));
                                            TRB_VALORICMSST = ((TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100) - icms;
                                            TRB_VALOR_ALIQUOTA = (TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100;
                                            TRB_FATORMVA = Convert.ToDecimal(dt.Rows[0]["MVAAJUSTADOMATIMPORT"]);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }

                        #endregion

                        #region DIF
                        if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                        {

                            string tipoTributacao = conn.ExecGetField(string.Empty, @"SELECT VREGRAICMS.TIPOTRIBUTACAO 
                                                                    FROM VREGRAICMS 
                                                                    INNER JOIN VNATUREZA ON VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA
                                                                    WHERE VNATUREZA.CODNATUREZA = ? AND VNATUREZA.CODEMPRESA = ?", new object[] { psLookupCODNATUREZA.textBox1.Text, AppLib.Context.Empresa }).ToString();
                            if (tipoTributacao == "D")
                            {
                                //pdif = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT PDIF FROM VPRODUTOFISCAL 
                                //                                    WHERE 
                                //                                    CODETD = (SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR AND VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA WHERE CODOPER = ?)
                                //                                    AND CODEMPRESA = ?
                                //                                    AND CODPRODUTO = ?", new object[] { codOper, AppLib.Context.Empresa, codProduto }));
                                pdif = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT VREGRAICMS.VALORDIFERIMENTO
                                                                    FROM VREGRAICMS 
                                                                    INNER JOIN VNATUREZA ON VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA
                                                                    WHERE VNATUREZA.CODNATUREZA = ? AND VNATUREZA.CODEMPRESA = ?", new object[] { psLookupCODNATUREZA.textBox1.Text, AppLib.Context.Empresa }).ToString());


                                TRB_VICMSDIF = ((TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100) * (pdif / 100);
                                TRB_VALOR_ALIQUOTA = TRB_VALOR_ALIQUOTA - TRB_VICMSDIF;
                            }
                        }
                        #endregion

                        #region CST

                        if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                        {
                            TRB_CST = conn.ExecGetField(string.Empty, @"SELECT CODCST FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, psLookupCODNATUREZA.textBox1.Text }).ToString();
                        }
                        else
                        {
                            if (result["CODTIPOTRIBUTO"].ToString() == "IPI")
                            {
                                if (dtTipOper.Rows[0]["TIPENTSAI"].ToString() == "E")
                                {
                                    TRB_CST = conn.ExecGetField(string.Empty, @"SELECT CODCSTENT FROM VREGRAIPI, VNATUREZA WHERE VREGRAIPI.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAIPI.IDREGRA = VNATUREZA.IDREGRAIPI AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, psLookupCODNATUREZA.textBox1.Text }).ToString();
                                }
                                else
                                {
                                    TRB_CST = conn.ExecGetField(string.Empty, @"SELECT CODCSTSAI FROM VREGRAIPI, VNATUREZA WHERE VREGRAIPI.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAIPI.IDREGRA = VNATUREZA.IDREGRAIPI AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, psLookupCODNATUREZA.textBox1.Text }).ToString();
                                }
                            }
                            else
                            {
                                TRB_CST = conn.ExecGetField(string.Empty, @"SELECT CODCST FROM VNATUREZATRIBUTO, VNATUREZA WHERE VNATUREZATRIBUTO.CODEMPRESA = VNATUREZA.CODEMPRESA AND VNATUREZATRIBUTO.CODNATUREZA = VNATUREZA.CODNATUREZA AND VNATUREZATRIBUTO.CODEMPRESA = ? AND VNATUREZATRIBUTO.CODNATUREZA = ? AND VNATUREZATRIBUTO.CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, psLookupCODNATUREZA.textBox1.Text, dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }).ToString();

                                if (string.IsNullOrEmpty(TRB_CST))
                                {
                                    TRB_CST = conn.ExecGetField(0, @"SELECT CODCST FROM VPRODUTOTRIBUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, codProduto, dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }).ToString();
                                }

                                if (string.IsNullOrEmpty(TRB_CST))
                                {
                                    TRB_CST = dtTipoTributo.Rows[iTributo]["CODCST"].ToString();
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(TRB_CST))
                        {
                            MessageBox.Show(string.Concat("CST não encontrada para o Tributo: ", dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString()), "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        #endregion

                        #region Modalidade BC ICMS/ ICMS ST

                        if (result["CODTIPOTRIBUTO"].ToString() == "ICMS" || result["CODTIPOTRIBUTO"].ToString() == "ICMS-ST")
                        {
                            string campo = string.Empty;
                            if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                                campo = "MODALIDADEICMS";
                            if (result["CODTIPOTRIBUTO"].ToString() == "ICMS-ST")
                                campo = "MODALIDADEICMSST";
                            string codetd1 = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();
                            string ncm = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODNCM FROM VPRODUTO INNER JOIN GOPERITEM ON VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA AND VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, codProduto }).ToString();
                            sSql = "SELECT " + campo + @" FROM VREGRAVARCFOP WHERE NCM = ? AND UFDESTINO = ?";
                            TRB_MODBC = Convert.ToInt32(conn.ExecGetField(-1, sSql, new object[] { ncm, codetd1 }));
                            if (TRB_MODBC < 0)
                                TRB_MODBC = null;
                        }
                        #endregion

                        #region Difal

                        int Codfilial = Convert.ToInt32(conn.ExecGetField(0, "SELECT CODFILIAL FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codOper }));
                        string Ufdestino = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();

                        string NCM = conn.ExecGetField(string.Empty, @"SELECT VPRODUTO.CODNCM
                                                                                                            FROM VPRODUTO
                                                                                                            INNER JOIN GOPERITEM ON VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA
                                                                                                            INNER JOIN VREGRAVARCFOP ON VREGRAVARCFOP.CODEMPRESA = VPRODUTO.CODEMPRESA
                                                                                                            AND VREGRAVARCFOP.NCM = VPRODUTO.CODNCM
                                                                                                            AND VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO
                                                                                                            WHERE GOPERITEM.CODEMPRESA = ?
                                                                                                            AND VREGRAVARCFOP.CODFILIAL = ?
                                                                                                            AND GOPERITEM.CODOPER = ?
                                                                                                            AND VREGRAVARCFOP.UFDESTINO = ?
                                                                                                            ", new object[] { AppLib.Context.Empresa, Codfilial, codOper, Ufdestino }).ToString();

                        decimal PFCP = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT VREGRAVARCFOP.PFCP
                                                                                FROM VREGRAVARCFOP
                                                                                INNER JOIN VPRODUTO ON VREGRAVARCFOP.CODEMPRESA = VPRODUTO.CODEMPRESA
                                                                                AND VREGRAVARCFOP.NCM = VPRODUTO.CODNCM
                                                                                WHERE VREGRAVARCFOP.NCM = ?
                                                                                ", new object[] { NCM }));

                        decimal PFCPST = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT VREGRAVARCFOP.PFCPST
                                                                                FROM VREGRAVARCFOP
                                                                                INNER JOIN VPRODUTO ON VREGRAVARCFOP.CODEMPRESA = VPRODUTO.CODEMPRESA
                                                                                AND VREGRAVARCFOP.NCM = VPRODUTO.CODNCM
                                                                                WHERE VREGRAVARCFOP.NCM = ?
                                                                                ", new object[] { NCM }));

                        if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                        {
                            sSql = @"SELECT 
                                                                    VCLIFOR.CODETD UFDEST,
                                                                    GFILIAL.CODETD UFREM, 
                                                                    GESTADO.ALIQUOTAICMSINTERNADEST, 
                                                                    VREGRAICMS.ALIQUOTA ALIQUOTAINTERESTADUAL, 
                                                                    PERCICMSUFDEST, 
                                                                    VREGRAICMS.DIFERENCIALALIQUOTA, 
                                                                    VCLIFOR.CONTRIBICMS, 
                                                                    GOPER.TIPOPERCONSFIN,
                                                                    GOPER.CLIENTERETIRA,
                                                                    VCLIFOR.PRODUTORRURAL
                                                                    FROM GOPER 
                                                                    INNER JOIN VCLIFOR ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR 
                                                                    INNER JOIN GFILIAL ON GOPER.CODEMPRESA = GFILIAL.CODEMPRESA
                                                                    INNER JOIN GESTADO ON VCLIFOR.CODETD = GESTADO.CODETD
                                                                    INNER JOIN GOPERITEM ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER
                                                                    INNER JOIN VNATUREZA ON GOPERITEM.CODEMPRESA = VNATUREZA.CODEMPRESA AND GOPERITEM.CODNATUREZA = VNATUREZA.CODNATUREZA
                                                                    INNER JOIN VREGRAICMS ON VNATUREZA.CODEMPRESA = VREGRAICMS.CODEMPRESA AND VNATUREZA.IDREGRAICMS = VREGRAICMS.IDREGRA
                                                                    WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?";

                            DataTable dtDifal = conn.ExecQuery(sSql, new object[] { codOper, AppLib.Context.Empresa });
                            if (dtDifal.Rows.Count > 0)
                            {
                                AppLib.ORM.Jit reg = new AppLib.ORM.Jit(conn, "GOPERITEMDIFAL");
                                reg.Set("CODEMPRESA", AppLib.Context.Empresa);
                                reg.Set("CODOPER", codOper);
                                reg.Set("NSEQITEM", psTextoBox1.textBox1.Text);
                                if (dtDifal.Rows[0]["DIFERENCIALALIQUOTA"].Equals(true))
                                {
                                    if (dtDifal.Rows[0]["CONTRIBICMS"].ToString() == "2" && dtDifal.Rows[0]["TIPOPERCONSFIN"].Equals(true))
                                    {
                                        if (dtDifal.Rows[0]["PRODUTORRURAL"].Equals(false) || dtDifal.Rows[0]["CLIENTERETIRA"].Equals(true))
                                        {
                                            decimal diferencial = Convert.ToDecimal(dtDifal.Rows[0]["ALIQUOTAICMSINTERNADEST"]) - Convert.ToDecimal(dtDifal.Rows[0]["ALIQUOTAINTERESTADUAL"]);
                                            decimal difal = TRB_BASE_CALCULO * (diferencial / 100);
                                            decimal partilhaDest = Convert.ToDecimal(dtDifal.Rows[0]["PERCICMSUFDEST"]);
                                            partilhaDest = difal * (partilhaDest / 100);
                                            decimal partilhaRem = difal - partilhaDest;
                                            decimal fcp = ((TRB_BASE_CALCULO * PFCP) / 100);
                                            decimal Vbcfcpufdest = ((TRB_BASE_CALCULO * PFCPST) / 100);

                                            reg.Set("VBCFCPSTUFDEST", Vbcfcpufdest);
                                            reg.Set("VBCUFDEST", TRB_BASE_CALCULO);
                                            reg.Set("PFCPUFDEST", PFCP);
                                            reg.Set("PICMSINTER", dtDifal.Rows[0]["ALIQUOTAINTERESTADUAL"]);
                                            reg.Set("PICMSUFDEST", dtDifal.Rows[0]["ALIQUOTAICMSINTERNADEST"]);
                                            reg.Set("PICMSINTERPART", dtDifal.Rows[0]["PERCICMSUFDEST"]);
                                            reg.Set("VFCPUFDEST", fcp);
                                            reg.Set("VICMSUFDEST", partilhaDest);
                                            reg.Set("VICMSUFREMET", partilhaRem);
                                            reg.Save();
                                        }
                                        else
                                        {
                                            reg.Delete();
                                        }
                                    }
                                    else
                                    {
                                        reg.Delete();
                                    }
                                }
                                else
                                {
                                    reg.Delete();
                                }
                            }
                        }
                        // João Pedro Luchiari
                        //if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                        //{
                        //    sSql = @"SELECT 
                        //                                            VCLIFOR.CODETD UFDEST,
                        //                                            GFILIAL.CODETD UFREM, 
                        //                                            GESTADO.ALIQUOTAICMSINTERNADEST, 
                        //                                            VREGRAICMS.ALIQUOTA ALIQUOTAINTERESTADUAL, 
                        //                                            PERCICMSUFDEST, 
                        //                                            VREGRAICMS.PERCFCP, 
                        //                                            VREGRAICMS.DIFERENCIALALIQUOTA, 
                        //                                            VCLIFOR.CONTRIBICMS, 
                        //                                            GOPER.TIPOPERCONSFIN,
                        //                                            GOPER.CLIENTERETIRA,
                        //                                            VCLIFOR.PRODUTORRURAL
                        //                                            FROM GOPER 
                        //                                            INNER JOIN VCLIFOR ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR 
                        //                                            INNER JOIN GFILIAL ON GOPER.CODEMPRESA = GFILIAL.CODEMPRESA
                        //                                            INNER JOIN GESTADO ON VCLIFOR.CODETD = GESTADO.CODETD
                        //                                            INNER JOIN GOPERITEM ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER
                        //                                            INNER JOIN VNATUREZA ON GOPERITEM.CODEMPRESA = VNATUREZA.CODEMPRESA AND GOPERITEM.CODNATUREZA = VNATUREZA.CODNATUREZA
                        //                                            INNER JOIN VREGRAICMS ON VNATUREZA.CODEMPRESA = VREGRAICMS.CODEMPRESA AND VNATUREZA.IDREGRAICMS = VREGRAICMS.IDREGRA
                        //                                            WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?";
                        //    DataTable dtDifal = conn.ExecQuery(sSql, new object[] { codOper, AppLib.Context.Empresa });
                        //    if (dtDifal.Rows.Count > 0)
                        //    {
                        //        AppLib.ORM.Jit reg = new AppLib.ORM.Jit(conn, "GOPERITEMDIFAL");
                        //        reg.Set("CODEMPRESA", AppLib.Context.Empresa);
                        //        reg.Set("CODOPER", codOper);
                        //        reg.Set("NSEQITEM", psTextoBox1.textBox1.Text);
                        //        if (dtDifal.Rows[0]["DIFERENCIALALIQUOTA"].Equals(true))
                        //        {
                        //            if (dtDifal.Rows[0]["CONTRIBICMS"].ToString() == "2" && dtDifal.Rows[0]["TIPOPERCONSFIN"].Equals(true))
                        //            {
                        //                if (dtDifal.Rows[0]["PRODUTORRURAL"].Equals(false) || dtDifal.Rows[0]["CLIENTERETIRA"].Equals(true))
                        //                {
                        //                    decimal diferencial = Convert.ToDecimal(dtDifal.Rows[0]["ALIQUOTAICMSINTERNADEST"]) - Convert.ToDecimal(dtDifal.Rows[0]["ALIQUOTAINTERESTADUAL"]);
                        //                    decimal difal = TRB_BASE_CALCULO * (diferencial / 100);
                        //                    decimal partilhaDest = Convert.ToDecimal(dtDifal.Rows[0]["PERCICMSUFDEST"]);
                        //                    partilhaDest = difal * (partilhaDest / 100);
                        //                    decimal partilhaRem = difal - partilhaDest;
                        //                    decimal fcp = TRB_BASE_CALCULO * (Convert.ToDecimal(dtDifal.Rows[0]["PERCFCP"]) / 100);
                        //                    //Busca pra saber o que executar


                        //                    reg.Set("VBCUFDEST", TRB_BASE_CALCULO);
                        //                    reg.Set("PFCPUFDEST", dtDifal.Rows[0]["PERCFCP"]);
                        //                    reg.Set("PICMSINTER", dtDifal.Rows[0]["ALIQUOTAINTERESTADUAL"]);
                        //                    reg.Set("PICMSUFDEST", dtDifal.Rows[0]["ALIQUOTAICMSINTERNADEST"]);
                        //                    reg.Set("PICMSINTERPART", dtDifal.Rows[0]["PERCICMSUFDEST"]);
                        //                    reg.Set("VFCPUFDEST", fcp);
                        //                    reg.Set("VICMSUFDEST", partilhaDest);
                        //                    reg.Set("VICMSUFREMET", partilhaRem);
                        //                    reg.Save();
                        //                }
                        //                else
                        //                {
                        //                    reg.Delete();
                        //                }
                        //            }
                        //            else
                        //            {
                        //                reg.Delete();
                        //            }
                        //        }
                        //        else
                        //        {
                        //            reg.Delete();
                        //        }
                        //    }
                        //}

                        #endregion

                        #region Redução da BC

                        if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                        {
                            TRB_REDBC = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT REDUCAOBASEICMS FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, psLookupCODNATUREZA.textBox1.Text }));
                        }
                        if (result["CODTIPOTRIBUTO"].ToString() == "ICMS-ST")
                        {
                            TRB_REDBC = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT REDUCAOBCST 
                                                                    FROM 
                                                                    VREGRAICMS 
                                                                    INNER JOIN VNATUREZA ON VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA
                                                                    WHERE	VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, psLookupCODNATUREZA.textBox1.Text }));


                        }

                        #endregion

                        #region Enquadramento IPI

                        if (result["CODTIPOTRIBUTO"].ToString() == "IPI")
                        {
                            TRB_CENQ = conn.ExecGetField(string.Empty, @"SELECT CENQ FROM VREGRAIPI, VNATUREZA WHERE VREGRAIPI.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAIPI.IDREGRA = VNATUREZA.IDREGRAIPI AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, psLookupCODNATUREZA.textBox1.Text }).ToString();
                        }

                        #endregion

                        #region Percentual de crédito de ICMS
                        decimal VCREDICMSSN = 0;
                        decimal pCREDSN = 0;
                        if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                        {
                            if (Convert.ToInt32(conn.ExecGetField(0, @"SELECT CREDITOICMS FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, psLookupCODNATUREZA.textBox1.Text })) == 1)
                            {
                                pCREDSN = Convert.ToDecimal(conn.ExecGetField(0, "SELECT PCREDSN FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial }));
                                VCREDICMSSN = TRB_BASE_CALCULO * (pCREDSN / 100);
                            }
                        }

                        #endregion

                        #region Popula Classe

                        string modalidadeBC = string.Empty;
                        string cenq = string.Empty;
                        if (string.IsNullOrEmpty(TRB_MODBC.ToString()))
                        {
                            modalidadeBC = "''";
                        }
                        else
                        {
                            modalidadeBC = TRB_MODBC.ToString();
                        }

                        if (string.IsNullOrEmpty(TRB_CENQ.ToString()))
                        {
                            cenq = "''";
                        }
                        else
                        {
                            cenq = TRB_CENQ;
                        }

                        insert = insert + @"INSERT INTO GOPERITEMTRIBUTO (CODEMPRESA, CODOPER, NSEQITEM, CODTRIBUTO, ALIQUOTA, VALOR, CODCST, BASECALCULO, MODALIDADEBC, REDUCAOBASEICMS, CENQ, FATORMVA, BCORIGINAL, REDUCAOBASEICMSST, VALORICMSST, PDIF, VICMSDIF, PCREDSN, VCREDICMSSN, VLDESPADUANA, VALORIOF) VALUES (" + AppLib.Context.Empresa + ", " + codOper + "," + Convert.ToInt32(psTextoBox1.textBox1.Text) + ",'" + dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString() + "','" + string.Format("{0:n2}", TRB_ALIQUOTA).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_VALOR_ALIQUOTA).Replace(".", "").Replace(",", ".") + "','" + TRB_CST + "','" + string.Format("{0:n2}", TRB_BASE_CALCULO).Replace(".", "").Replace(",", ".") + "'," + modalidadeBC + ",'" + string.Format("{0:n2}", TRB_REDBC).Replace(".", "").Replace(",", ".") + "'," + cenq + ",'" + string.Format("{0:n2}", TRB_FATORMVA).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_BCORIGINAL).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_REDUCAOBASEICMSST).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_VALORICMSST).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", pdif).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_VICMSDIF).Replace(".", "").Replace(",", ".") + "', '" + string.Format("{0:n2}", pCREDSN).Replace(".", "").Replace(",", ".") + "', '" + string.Format("{0:n2}", VCREDICMSSN).Replace(".", "").Replace(",", ".") + "','" + 0 + "','" + 0 + "');\n\r";

                        TRB_ALIQUOTA = 0;
                        TRB_VALOR_ALIQUOTA = 0;
                        TRB_BASE_CALCULO = 0;
                        TRB_REDBC = 0;
                        TRB_CENQ = string.Empty;
                        TRB_FATORMVA = 0;
                        TRB_BCORIGINAL = 0;
                        TRB_REDUCAOBASEICMSST = 0;
                        TRB_VALORICMSST = 0;
                        pdif = 0;
                        pCREDSN = 0;
                        VCREDICMSSN = 0;
                        TRB_VICMSDIF = 0;
                        TRB_CST = string.Empty;
                        TRB_MODBC = null;
                        #endregion

                    }

                    conn.ExecTransaction(insert, new object[] { });
                    conn.Commit();
                }
            }
            catch (Exception)
            {
                conn.Rollback();
            }
        }

        private void carregaTributos()
        {
            try
            {
                string sql = string.Empty;
                List<string> tabelasFilhas = new List<string>();
                tabelasFilhas.Clear();

                sql = new Class.Utilidades().getVisao("GOPERITEMTRIBUTO", string.Empty, tabelasFilhas, "WHERE CODOPER = " + codOper + " AND CODEMPRESA = " + AppLib.Context.Empresa + " AND NSEQITEM = " + psTextoBox1.textBox1.Text + "");

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { "GOPERITEMTRIBUTO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { "GOPERITEMTRIBUTO", AppLib.Context.Usuario });
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridView1.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridView1.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void carregaCampos()
        {
            GetDefaultCliFor(AppLib.Context.Empresa, GOPER["CODCLIFOR"].ToString());
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT
GOPERITEM.NSEQITEM,
VPRODUTO.CODPRODUTO,
VPRODUTO.CODIGOAUXILIAR,
QUANTIDADE,
VLUNITARIO,
VLDESCONTO,
PRDESCONTO,
VLTOTALITEM,
CODCFOP,
CODNATUREZA,
CODUNIDOPER,
OBSERVACAO,
INFCOMPL,
CODTABPRECO,
QUANTIDADE_FATURADO,
QUANTIDADE_SALDO,
UFIBPTAX,
NACIONALFEDERALIBPTAX,
IMPORTADOSFEDERALIBPTAX,
ESTADUALIBPTAX,
MUNICIPALIBPTAX,
CHAVEIBPTAX,
APLICACAOMATERIAL,
VLACRESCIMO,
PRACRESCIMO,
TIPODESCONTO,
VLUNITORIGINAL,
TOTALEDITADO,
RATEIODESPESA,
RATEIODESCONTO,
RATEIOFRETE,
RATEIOSEGURO,
DATAENTREGA,
TOTALEDITADO,
CAMPOVALOR1,
CAMPOVALOR2,
CAMPOVALOR3,
CAMPOVALOR4,
GOPERITEMCOMPL.DESCCOMPLPRODUTO,
VPRODUTO.NOME AS 'NOMEPRODUTO',
GOPERITEM.XPED,
GOPERITEM.NITEMPED,
GOPERITEM.NUMERODI,
GOPERITEM.DATADI,
GOPERITEM.DATADI,
GOPERITEM.LOCDESEMB,
GOPERITEM.UFDESEMB,
GOPERITEM.DATADESEMB,
GOPERITEM.CODEXPORTADOR,
GOPERITEM.NUMADICAO,
GOPERITEM.NUMSEQADIC,
GOPERITEM.CODFABRICANTE,
GOPERITEM.VLORDESCDI,
GOPERITEM.TPVIATRANSP,
GOPERITEM.VAFRMM,
GOPERITEM.TPINTERMEDIO,
GOPERITEM.CNPJ,
GOPERITEM.UFTERCEIRO,
GOPERITEM.NDRAW, 
GOPERITEM.CODUNIDCONTROLE,
GOPERITEM.QUANTIDADECONTROLE,
GOPERITEM.FATORCONVERSAO
FROM
 GOPERITEM 
  LEFT OUTER JOIN GOPERITEMCOMPL ON GOPERITEM.CODEMPRESA = GOPERITEMCOMPL.CODEMPRESA AND GOPERITEM.NSEQITEM = GOPERITEMCOMPL.NSEQITEM AND GOPERITEM.CODOPER = GOPERITEMCOMPL.CODOPER
  INNER JOIN VPRODUTO ON GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO AND GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA
 WHERE 
 GOPERITEM.CODOPER = ?
 AND GOPERITEM.CODEMPRESA = ?
AND GOPERITEM.NSEQITEM = ?", new object[] { codOper, AppLib.Context.Empresa, nseqitem });

            if (dt.Rows.Count > 0)
            {
                psTextoBox1.textBox1.Text = dt.Rows[0]["NSEQITEM"].ToString();

                string campo = AppLib.Context.poolConnection.Get("Start").ExecGetField("CODPRODUTO", @"SELECT BUSCAPRODUTOPOR FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codTipOper, AppLib.Context.Empresa }).ToString();
                switch (campo)
                {
                    case "0":
                        lookupproduto.txtcodigo.Text = dt.Rows[0]["CODPRODUTO"].ToString();
                        break;
                    case "1":
                        campo = "CODIGOAUXILIAR";
                        lookupproduto.txtcodigo.Text = dt.Rows[0]["CODIGOAUXILIAR"].ToString();
                        break;
                    default:
                        lookupproduto.txtcodigo.Text = dt.Rows[0]["CODPRODUTO"].ToString();
                        break;
                }
                codProduto = dt.Rows[0]["CODPRODUTO"].ToString();
                lookupproduto.CarregaDescricao();

                psQuantidade.textBox1.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["QUANTIDADE"]));
                psValorUnitario.textBox1.Text = Convert.ToDecimal(dt.Rows[0]["VLUNITARIO"]).ToString();
                psValorUnitario.textBox1.Text = string.Format("{0:n" + psValorUnitario.CasasDecimais + "}", Convert.ToDecimal(psValorUnitario.textBox1.Text));
                psValorDesconto.textBox1.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["VLDESCONTO"]));
                psPercDesconto.textBox1.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["PRDESCONTO"]));
                psValorTotalItem.textBox1.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["VLTOTALITEM"]));
                psLookupCODNATUREZA.textBox1.Text = dt.Rows[0]["CODNATUREZA"].ToString();
                psLookupCODNATUREZA.LoadLookup();

                //17/01/2018
                //psLookup6.textBox1.Text = dt.Rows[0]["CODUNIDOPER"].ToString();
                lpUnidadeMedida.txtcodigo.Text = dt.Rows[0]["CODUNIDOPER"].ToString();

                getInfoProduto();
                buscaPreco();
                getClassificacao();

                //buscaNatureza();

                //psLookup6.LoadLookup();
                lpUnidadeMedida.CarregaDescricao();

                psMemoBox1.Text = dt.Rows[0]["OBSERVACAO"].ToString();
                psMemoBox2.Text = dt.Rows[0]["INFCOMPL"].ToString();

                psComboBoxAPLICACAOMATERIAL.Value = dt.Rows[0]["APLICACAOMATERIAL"].ToString();
                psVLAcrescimo.textBox1.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["VLACRESCIMO"]));
                psPRAcrescimo.textBox1.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["PRACRESCIMO"]));

                psVlUnitOriginal.textBox1.Text = Convert.ToDecimal(dt.Rows[0]["VLUNITORIGINAL"]).ToString();
                psVlUnitOriginal.textBox1.Text = string.Format("{0:n" + psVlUnitOriginal.CasasDecimais + "}", Convert.ToDecimal(psVlUnitOriginal.textBox1.Text));
                psMoedaBox1.textBox1.Text = string.IsNullOrEmpty(dt.Rows[0]["RATEIODESPESA"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["RATEIODESPESA"]));
                psMoedaBox2.textBox1.Text = string.IsNullOrEmpty(dt.Rows[0]["RATEIODESCONTO"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["RATEIODESCONTO"]));
                psMoedaBox3.textBox1.Text = string.IsNullOrEmpty(dt.Rows[0]["RATEIOFRETE"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["RATEIOFRETE"]));
                psMoedaBox4.textBox1.Text = string.IsNullOrEmpty(dt.Rows[0]["RATEIOSEGURO"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["RATEIOSEGURO"]));
                dtEntrega.Text = string.IsNullOrEmpty(dt.Rows[0]["DATAENTREGA"].ToString()) ? string.Empty : dt.Rows[0]["DATAENTREGA"].ToString();
                psCheckBox1.Checked = string.IsNullOrEmpty(dt.Rows[0]["TOTALEDITADO"].ToString()) ? false : Convert.ToBoolean(dt.Rows[0]["TOTALEDITADO"]);
                psComboBoxCODTABPRECO.Value = dt.Rows[0]["CODTABPRECO"];
                psComboBox1.Value = dt.Rows[0]["TIPODESCONTO"];

                txtCampo1.Text = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR1"].ToString()) ? "0,00" : string.Format("{0:n2}", dt.Rows[0]["CAMPOVALOR1"]);
                txtCampo2.Text = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR2"].ToString()) ? "0,00" : string.Format("{0:n2}", dt.Rows[0]["CAMPOVALOR2"]);
                txtCampo3.Text = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR3"].ToString()) ? "0,00" : string.Format("{0:n2}", dt.Rows[0]["CAMPOVALOR3"]);
                txtCampo4.Text = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR4"].ToString()) ? "0,00" : string.Format("{0:n2}", dt.Rows[0]["CAMPOVALOR4"]);
                memoEdit1.Text = dt.Rows[0]["DESCCOMPLPRODUTO"].ToString();
                txtXPED.Text = dt.Rows[0]["XPED"].ToString();
                txtNITEMPED.Text = dt.Rows[0]["NITEMPED"].ToString();

                #region Unidade de Medida

                tbUnidadeControle.Text = dt.Rows[0]["CODUNIDCONTROLE"].ToString();
                tbUnidadeControle.Enabled = false;

                if (tbUnidadeControle.Text == lpUnidadeMedida.txtcodigo.Text)
                {
                    tbFatorConversao.Enabled = false;
                }

                tbFatorConversao.Text = Convert.ToDecimal(dt.Rows[0]["FATORCONVERSAO"]).ToString();
                tbQuantidadeConversao.Text = Convert.ToDecimal(dt.Rows[0]["QUANTIDADECONTROLE"]).ToString();
                tbQuantidadeConversao.Enabled = false;

                #endregion

                #region Importação
                dteDATADI.Text = string.IsNullOrEmpty(dt.Rows[0]["DATADI"].ToString()) ? string.Empty : dt.Rows[0]["DATADI"].ToString();
                dteDATADESEMB.Text = string.IsNullOrEmpty(dt.Rows[0]["DATADESEMB"].ToString()) ? string.Empty : dt.Rows[0]["DATADESEMB"].ToString();
                txtNUMERODI.Text = dt.Rows[0]["NUMERODI"].ToString();
                TXTLOCDESEMB.Text = dt.Rows[0]["LOCDESEMB"].ToString();
                TXTUFDESEMB.Text = dt.Rows[0]["UFDESEMB"].ToString();
                txtCODEXPORTADOR.Text = dt.Rows[0]["CODEXPORTADOR"].ToString();
                txtNUMADICAO.Text = dt.Rows[0]["NUMADICAO"].ToString();
                txtNUMSEQADIC.Text = dt.Rows[0]["NUMSEQADIC"].ToString();
                txtCODFABRICANTE.Text = dt.Rows[0]["CODFABRICANTE"].ToString();
                txtVLORDESCDI.Text = dt.Rows[0]["VLORDESCDI"].ToString();
                cmbTPVIATRANSP.SelectedValue = dt.Rows[0]["TPVIATRANSP"].ToString();
                txtVAFRMM.Text = dt.Rows[0]["VAFRMM"].ToString();
                cmbTPINTERMEDIO.SelectedValue = dt.Rows[0]["TPINTERMEDIO"].ToString();
                txtCNPJimportacao.Text = dt.Rows[0]["CNPJ"].ToString();
                txtUFTERCEIRO.Text = dt.Rows[0]["UFTERCEIRO"].ToString();
                txtNDRAW.Text = dt.Rows[0]["NDRAW"].ToString();
                #endregion

                //Campos complementares
                new Class.Utilidades().criaCamposComplementaresOperacao("GOPERITEMCOMPL", tabPage3, codTipOper);
                carregaCamposComplementares();
            }
        }

        private void carregaCamposComplementares()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPERITEMCOMPL WHERE CODOPER = ? AND CODEMPRESA = ? AND NSEQITEM = ?", new object[] { codOper, AppLib.Context.Empresa, psTextoBox1.textBox1.Text });
            DataTable dtColunas = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT GTABCAMPOCOMPL.NOMECAMPO 
                                                                                         FROM 
                                                                                         GTABCAMPOCOMPL 
                                                                                         INNER JOIN GTIPOPERCOMPL ON GTIPOPERCOMPL.CODENTIDADE = GTABCAMPOCOMPL.CODENTIDADE AND GTIPOPERCOMPL.NOMECAMPO = GTABCAMPOCOMPL.NOMECAMPO
                                                                                         WHERE GTABCAMPOCOMPL.CODENTIDADE = ? 
                                                                                         AND GTABCAMPOCOMPL.ATIVO = ? 
                                                                                         AND GTIPOPERCOMPL.CODTIPOPER = ?
                                                                                         ORDER BY GTABCAMPOCOMPL.ORDEM", new object[] { "GOPERITEMCOMPL", 1, codTipOper });
            //DataTable dtColunas = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT NOMECAMPO FROM GTABCAMPOCOMPL WHERE CODENTIDADE = ? AND ATIVO = ? ORDER BY ORDEM", new object[] { "GOPERITEMCOMPL", 1 });
            if (dt.Rows.Count > 0)
            {
                Control controle;
                for (int iColunas = 0; iColunas < dtColunas.Rows.Count; iColunas++)
                {
                    for (int i = 0; i < tabPage3.Controls.Count; i++)
                    {
                        controle = tabPage3.Controls[i];
                        if (controle.GetType().Name.Equals("TextEdit") || controle.GetType().Name.Equals("ComboBox") || controle.GetType().Name.Equals("DateEdit") || controle.GetType().Name.Equals("MemoEdit"))
                        {
                            if (controle.Name.Remove(0, 4) == dtColunas.Rows[iColunas]["NOMECAMPO"].ToString())
                            {
                                controle.Text = dt.Rows[0][dtColunas.Rows[iColunas]["NOMECAMPO"].ToString()].ToString();
                            }
                        }
                        else if (controle.GetType().Name.Equals("CheckEdit"))
                        {
                            if (controle.Name.Remove(0, 4) == dtColunas.Rows[iColunas]["NOMECAMPO"].ToString())
                            {
                                if (dt.Rows[0][dtColunas.Rows[iColunas]["NOMECAMPO"].ToString()].ToString() == "1")
                                {
                                    DevExpress.XtraEditors.CheckEdit ck = (DevExpress.XtraEditors.CheckEdit)controle;
                                    ck.Checked = true;
                                }
                                else
                                {
                                    DevExpress.XtraEditors.CheckEdit ck = (DevExpress.XtraEditors.CheckEdit)controle;
                                    ck.Checked = false;
                                }

                            }
                        }
                    }
                }
            }
        }

        private void txtCampo1_Validating(object sender, CancelEventArgs e)
        {
            txtCampo1.Text = string.Format("{0:n2}", Convert.ToDecimal(txtCampo1.Text));

        }

        private void txtCampo2_Validating(object sender, CancelEventArgs e)
        {
            txtCampo2.Text = string.Format("{0:n2}", Convert.ToDecimal(txtCampo2.Text));
        }

        private void txtCampo3_Validating(object sender, CancelEventArgs e)
        {
            txtCampo3.Text = string.Format("{0:n2}", Convert.ToDecimal(txtCampo3.Text));
        }

        private void txtCampo4_Validating(object sender, CancelEventArgs e)
        {
            txtCampo4.Text = string.Format("{0:n2}", Convert.ToDecimal(txtCampo4.Text));
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
            if (SalvaItens() == true)
            {
                carregaTributos();

                if (groupBox5.Enabled == true)
                {
                    if (lbQtdDiferenca.Text == "0,0000" && gridView4.RowCount > 0)
                    {
                        ExecTransactionLote();
                        CarregaGridLote(false);
                        gridControl4.DataSource = null;
                    }
                }
                edita = true;
            }
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);

            if (SalvaItens() == true)
            {
                if (groupBox5.Enabled == true)
                {
                    if (lbQtdDiferenca.Text == "0,0000" && gridView4.RowCount > 0)
                    {
                        ExecTransactionLote();
                        CarregaGridLote(false);
                    }
                }
                this.Dispose();
                GC.Collect();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                if (row1 != null)
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ? AND CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, codOper, psTextoBox1.textBox1.Text, row1["GOPERITEMTRIBUTO.CODTRIBUTO"] });
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                if (row1 != null)
                {
                    PSPartOperItemTributoEdit frm = new PSPartOperItemTributoEdit(codOper, row1["GOPERITEMTRIBUTO.CODTRIBUTO"].ToString(), Convert.ToInt32(psTextoBox1.textBox1.Text));
                    frm.ShowDialog();
                    carregaTributos();
                }

            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsFind.AlwaysVisible == true)
            {
                gridView1.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView1.OptionsFind.AlwaysVisible = true;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsView.ShowGroupPanel == true)
            {
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.ClearGrouping();
                toolStripButton5.Text = "Agrupar";
            }
            else
            {
                gridView1.OptionsView.ShowGroupPanel = true;
                toolStripButton5.Text = "Desagrupar";
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Glb.New.frmSelecaoColunas frm = new Glb.New.frmSelecaoColunas("GOPERITEMTRIBUTO");
            frm.ShowDialog();
            carregaTributos();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, "GOPERITEMTRIBUTO" });

            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", "GOPERITEMTRIBUTO");
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView1.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView1.VisibleColumns[i].Width);
                GVISAOUSUARIO.Set("VISIVEL", 1);
                GVISAOUSUARIO.Save();
            }

            DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ? AND FIXO = 1", new object[] { AppLib.Context.Usuario, "GOPERITEMTRIBUTO" });
            if (dtFixo.Rows.Count > 0)
            {
                for (int i = 0; i < dtFixo.Rows.Count; i++)
                {
                    AppLib.ORM.Jit FIXO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                    FIXO.Set("VISAO", "GOPERITEMTRIBUTO");
                    FIXO.Set("CODUSUARIO", AppLib.Context.Usuario);
                    FIXO.Set("COLUNA", dtFixo.Rows[i]["COLUNA"]);
                    FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
                carregaTributos();
            }
        }

        private void PSPartOperacaoItemEdit_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    break;
                case Keys.F10:
                    break;
                case Keys.F11:
                    break;
                case Keys.F12:
                    break;
                case Keys.F13:
                    break;
                case Keys.F14:
                    break;
                case Keys.F15:
                    break;
                case Keys.F16:
                    break;
                case Keys.F17:
                    break;
                case Keys.F18:
                    break;
                case Keys.F19:
                    break;
                case Keys.F2:
                    break;
                case Keys.F20:
                    break;
                case Keys.F21:
                    break;
                case Keys.F22:
                    break;
                case Keys.F23:
                    break;
                case Keys.F24:
                    break;
                case Keys.F3:
                    break;
                case Keys.F4:
                    break;
                case Keys.F5:
                    break;
                case Keys.F6:
                    if (string.IsNullOrEmpty(codProduto))
                    {
                        PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque frm = new PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque();
                        frm.ShowDialog();
                    }
                    else
                    {
                        PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque frm = new PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque(codProduto);
                        frm.getProduto();
                        frm.ShowDialog();

                    }
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    break;
                case Keys.F9:
                    break;
                case Keys.Insert:
                    break;
                default:
                    break;
            }
        }

        private void getClassificacao()
        {
            if (!string.IsNullOrEmpty(codProduto))
            {
                string codClassificacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODCLASSIFICACAO FROM GTIPOPERCLASSIFICAOPRODUTO  WHERE CODCLASSIFICACAO = (SELECT CODCLASSIFICACAO FROM VPRODUTO WHERE CODPRODUTO = ? AND CODEMPRESA = ?) AND CODTIPOPER = ?", new object[] { codProduto, AppLib.Context.Empresa, codTipOper }).ToString();
                if (string.IsNullOrEmpty(codClassificacao))
                {
                    MessageBox.Show("Produto não disponível para esse tipo de operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lookupproduto.Clear();
                    //txtCodigoGhpc.Clear();
                    textBox2.Clear();
                    psLookupCODNATUREZA.textBox1.Clear();
                    psLookupCODNATUREZA.textBox2.Clear();

                    //17/01/2018
                    //psLookup6.textBox1.Clear();
                    //psLookup6.textBox2.Clear();

                    lpUnidadeMedida.Clear();
                    return;
                }
            }
        }

        private void getInfoProduto()
        {
            if (salvar == false)
            {
                if (string.IsNullOrEmpty(lookupproduto.ValorCodigoInterno))
                {
                    codProduto = "";
                }
                else
                {
                    codProduto = lookupproduto.ValorCodigoInterno;

                }

                if (!string.IsNullOrEmpty(codProduto))
                {
                    this.GetProduto();
                    //textBox2.Text = VPRODUTO["DESCRICAO"].ToString();
                    //txtCodigoGhpc.Text = VPRODUTO["CODIGOAUXILIAR"].ToString();
                    if (int.Parse(GTIPOPER["UNDMEDPADRAO"].ToString()) == 0)

                        //psLookup6.Text = VPRODUTO["CODUNIDCONTROLE"].ToString();
                        lpUnidadeMedida.txtcodigo.Text = VPRODUTO["CODUNIDCONTROLE"].ToString();

                    if (int.Parse(GTIPOPER["UNDMEDPADRAO"].ToString()) == 1)

                        //psLookup6.Text = VPRODUTO["CODUNIDCOMPRA"].ToString();
                        lpUnidadeMedida.txtcodigo.Text = VPRODUTO["CODUNIDCOMPRA"].ToString();

                    if (int.Parse(GTIPOPER["UNDMEDPADRAO"].ToString()) == 2)

                        //psLookup6.textBox1.Text = VPRODUTO["CODUNIDVENDA"].ToString();
                        lpUnidadeMedida.txtcodigo.Text = VPRODUTO["CODUNIDVENDA"].ToString();

                    textBox2.Text = VPRODUTO["DESCRICAO"].ToString();

                    //psLookup6.LoadLookup();
                    lpUnidadeMedida.CarregaDescricao();

                }
                //  carregaIBPTAX();
            }
            else
            {
                return;
            }
        }

        #region Unidade de Medida

        // João Pedro Luchiari 12/01/2018

        /// <summary>
        /// Método que contém todos os métodos referentes à Unidade de Medida.
        /// </summary>
        private void UnidadeMedida()
        {
            FatorConversaoUnidade(UnidadeControle);

            CalculaQuantidadeControle(FatorConversao);
        }

        /// <summary>
        /// Método para retornar o código da Unidade de Controle do produto.
        /// </summary>
        /// <returns>Retorna o código da Unidade de Controle</returns>
        private string UnidadeControleProduto()
        {
            string CodUnidade = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODUNIDCONTROLE FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, lookupproduto.ValorCodigoInterno }).ToString();
            tbUnidadeControle.Text = CodUnidade;
            tbUnidadeControle.Enabled = false;
            return CodUnidade;
        }

        /// <summary>
        /// Método para retornar o fator de conversão vindo da tabela VUNIDCONVERSAO.
        /// </summary>
        /// <param name="UnidadeConversao">Unidade de Conversão usada para ser o fator de conversão</param>
        /// <returns>Retorna o fator de conversão de acordo com a Unidade de Conversão</returns>
        private decimal FatorConversaoUnidade(string UnidadeConversao)
        {
            string editaFator = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT EDITAFATORCONVERSAO FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codTipOper, AppLib.Context.Empresa }).ToString();

            if (ValidaUnidade() == false)
            {
                tbFatorConversao.Enabled = false;
                tbFatorConversao.Text = 1.ToString();
                string NewConversao = string.Format("{0:n4}", Convert.ToDecimal(tbFatorConversao.Text));
                tbFatorConversao.Text = NewConversao;
                FatorConversao = Convert.ToDecimal(tbFatorConversao.Text);
                return FatorConversao;
            }
            else
            {
                if (editaFator == "E")
                {
                    tbFatorConversao.Enabled = true;
                }
                else
                {
                    tbFatorConversao.Enabled = false;
                }
            }

            string BuscaFator = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT FATORCONVERSAO FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codTipOper, AppLib.Context.Empresa }).ToString();

            if (BuscaFator == "PADRAO")
            {
                decimal Fator = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT FATORCONVERSAO FROM VUNIDCONVERSAO WHERE CODEMPRESA = ? AND CODUNID = ? AND CODUNIDCONVERSAO = ?", new object[] { AppLib.Context.Empresa, lpUnidadeMedida.txtcodigo.Text, tbUnidadeControle.Text }));
                FatorConversao = Fator;

                tbFatorConversao.Text = Fator.ToString();
                return Fator;
            }
            else if (BuscaFator == "PRODUTO")
            {
                decimal Fator = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT FATORCONVERSAO FROM VPRODUTOUNIDADE WHERE CODEMPRESA = ? AND CODUNID = ? AND CODUNIDCONVERSAO = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, lpUnidadeMedida.txtcodigo.Text, tbUnidadeControle.Text, codProduto }));
                FatorConversao = Fator;

                tbFatorConversao.Text = Fator.ToString();
                return Fator;
            }
            else
            {
                MessageBox.Show("Favor verificar o parâmetro de Operação. \nOrigem do fator não encontrada.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        /// <summary>
        /// Método que realiza o cálculo responsável por alimentar o campo tbQuantidadeConversao.
        /// </summary>
        /// <param name="FatordeConversao">Parâmetro para o Fator de Conversão</param>
        private void CalculaQuantidadeControle(decimal FatordeConversao)
        {
            QuantidadeControle = Convert.ToDecimal(psQuantidade.Text);

            ResultadoQuantidade = QuantidadeControle * FatordeConversao;

            tbQuantidadeConversao.Text = ResultadoQuantidade.ToString();
            tbQuantidadeConversao.Text = string.Format("{0:n4}", ResultadoQuantidade);

            tbQuantidadeConversao.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidaUnidade()
        {
            if (tbUnidadeControle.Text == lpUnidadeMedida.txtcodigo.Text)
            {
                tbFatorConversao.Enabled = false;
                return false;
            }
            else
            {
                return true;
            }
        }

        private void lpUnidadeMedida_Leave(object sender, EventArgs e)
        {
            UnidadeMedida();
        }

        private void psLookup6_Leave(object sender, EventArgs e)
        {
            UnidadeMedida();
        }

        private void psQuantidade_Leave(object sender, EventArgs e)
        {
            // Unidade de Medida
            UnidadeMedida();

            // Lote
            if (tabControl1.TabPages.Contains(tabPage5))
            {
                DiferencaInicial();
                AtribuiQuantidadeEntrada();
            }
        }

        private void tbFatorConversao_Leave(object sender, EventArgs e)
        {
            UnidadeMedida();
        }

        #endregion

        private void txtNITEMPED_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        #endregion

        private void psValorUnitario_Load(object sender, EventArgs e)
        {

        }

        private void psLookupCODNATUREZA_Load(object sender, EventArgs e)
        {

        }

        private void gridControl1_DoubleClick_1(object sender, EventArgs e)
        {
            if (Codstatus == "0" || Codstatus == "5")
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    if (row1 != null)
                    {
                        PSPartOperItemTributoEdit frm = new PSPartOperItemTributoEdit(codOper, row1["GOPERITEMTRIBUTO.CODTRIBUTO"].ToString(), Convert.ToInt32(psTextoBox1.textBox1.Text));
                        frm.ShowDialog();
                        carregaTributos();
                    }
                }
            }
        }

        #region Lote

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage5)
            {
                lookupproduto_Leave(sender, e);

                if (ValidaProdOper(UsaAbaLote, UsaLoteProduto) == false)
                {
                    return;
                }

                if (ValidaQuantidadeOperacao(Convert.ToDecimal(tbQuantidadeConversao.Text)) == true)
                {
                    groupBox5.Enabled = true;
                    CarregaGridLote(false);
                }
                else
                {
                    MessageBox.Show("Favor informar a quantidade do item.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl1.SelectedTab = tabPage1;
                    psQuantidade.Select();
                    psQuantidade.Focus();
                    groupBox5.Enabled = false;
                    return;
                }

                if (ValidaNatureza() == true)
                {
                    groupBox5.Enabled = true;
                    CarregaGridLote(false);
                }
                else
                {
                    MessageBox.Show("Favor informar a Natureza de Operação.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl1.SelectedTab = tabPage1;
                    psLookupCODNATUREZA.Select();
                    psLookupCODNATUREZA.Focus();
                    groupBox5.Enabled = false;
                    return;
                }

                if (ValidacaoParaIncluir() == false)
                {
                    btnIncluir.Enabled = false;
                }

                if (gridView5.RowCount == 0)
                {
                    btnExcluirLote.Enabled = false;
                }
            }
        }

        #region Validações 

        /// <summary>
        /// Método que habilita a aba de Lote do Produto 
        /// </summary>
        /// <param name="LoteOperacao">Parâmetro da Operação para uso do lote</param>
        /// <param name="LoteProduto">Parâmetro do Produto para uso do lote</param>
        private bool ValidaProdOper(bool LoteOperacao, bool LoteProduto)
        {
            if (LoteOperacao == true && LoteProduto == true)
            {
                groupBox5.Enabled = true;
                return true;
            }
            else
            {
                groupBox5.Enabled = false;
                return false;
            }
        }

        /// <summary>
        /// Método para validar o Tipo de Estoque e assim carregar a visão 
        /// </summary>
        private void ValidaTipoEstoque()
        {
            TipoOperEstoque = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT OPERESTOQUE FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper }).ToString();

            if (TipoOperEstoque == "D")
            {
                btnNovoLote.Enabled = false;
                CarregaGridLote(false);
            }

            if (TipoOperEstoque == "A")
            {
                CarregaGridLote(false);
            }
        }

        /// <summary>
        /// Método para verificar se a diferença é diferente de 0
        /// </summary>
        /// <param name="DiferencaLote">Diferença</param>
        /// <returns></returns>
        private bool ValidaDiferencaZero(string DiferencaLote)
        {
            if (DiferencaLote != "0,0000")
            {
                MessageBox.Show("Existe diferença na quantidade do Lote.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidacaoParaIncluir()
        {
            if (string.IsNullOrEmpty(lbValorLote.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private decimal ValidaQuantidade()
        {
            decimal Quantidade = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT SUM(QUANTIDADE)
                                                                                                            FROM GOPERITEMLOTE
                                                                                                            WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND NSEQITEM = ? AND CODPRODUTO = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, FilialLote, LocalLote, nseqitem, codProduto, codOper }));
            Quantidade = Quantidade - QtdLote;
            return Quantidade;
        }

        /// <summary>
        /// Método para validar se a quantidade da Operação é maior que zero.
        /// </summary>
        /// <param name="Quantidade">Quantidade do item da Operação</param>
        /// <returns></returns>
        private bool ValidaQuantidadeOperacao(decimal Quantidade)
        {
            if (Quantidade <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Método para validar se a Natureza foi preenchida.
        /// </summary>
        /// <returns></returns>
        private bool ValidaNatureza()
        {
            if (string.IsNullOrEmpty(psLookupCODNATUREZA.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void HabilitaExclusaoLote()
        {
            if (gridView4.RowCount > 0)
            {
                btnExcluirRegistro.Enabled = true;
            }
            else
            {
                btnExcluirRegistro.Enabled = false;
            }
        }

        #endregion

        #region Métodos 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_novoLote"></param>
        public void CarregaGridLote(bool _novoLote)
        {
            try
            {
                //if (gridView5.RowCount > 0)
                //{
                //    gridControl5.DataSource = CriaVisaoInicial(PS.Glb.Class.GoperItemLote._codLote);
                //    gridView5.BestFitColumns();
                //}
                //else
                //{
                //    gridControl5.DataSource = null;
                //    gridView5.Columns.Clear();
                //    gridControl5.DataSource = CriaVisaoInicial(PS.Glb.Class.GoperItemLote._codLote);
                //    gridView5.BestFitColumns();
                //}

                string sql = string.Empty;

                if (TipoOperEstoque == "A")
                {
                    //sql = @"SELECT VPRODUTOLOTE.CODEMPRESA AS 'Código da Empresa', VPRODUTOLOTE.CODFILIAL AS 'Código da Filial', VPRODUTOLOTE.CODLOTE AS 'Código do Lote', VPRODUTOLOTE.NUMERO AS 'Número do Lote', SUM(GOPERITEMLOTE.QUANTIDADE) AS 'Quantidade', GOPERITEMLOTE.NSEQITEM AS 'Sequencial', GOPERITEMLOTE.CODUNIDCONTROLE AS 'Unidade de Controle', VPRODUTOLOTE.DATAFABRICACAO AS 'Data de fabricação', VPRODUTOLOTE.DATAENTRADA AS 'Data de entrada', VPRODUTOLOTE.DATACRIACAO AS 'Data de criação' 
                    //    FROM VPRODUTOLOTE    
                    //    INNER JOIN GOPERITEMLOTE ON GOPERITEMLOTE.CODEMPRESA = VPRODUTOLOTE.CODEMPRESA AND GOPERITEMLOTE.CODFILIAL = VPRODUTOLOTE.CODFILIAL AND VPRODUTOLOTE.CODLOTE = GOPERITEMLOTE.CODLOTE  
                    //    WHERE VPRODUTOLOTE.CODEMPRESA = ? AND VPRODUTOLOTE.CODFILIAL = ? AND VPRODUTOLOTE.CODCLIFOR = ? AND VPRODUTOLOTE.STATUS = 1 
                    //    GROUP BY VPRODUTOLOTE.CODEMPRESA, VPRODUTOLOTE.CODFILIAL, VPRODUTOLOTE.CODLOTE, VPRODUTOLOTE.NUMERO, GOPERITEMLOTE.NSEQITEM, GOPERITEMLOTE.CODUNIDCONTROLE, VPRODUTOLOTE.DATAFABRICACAO, VPRODUTOLOTE.DATAENTRADA, VPRODUTOLOTE.DATACRIACAO";

                    sql = @"SELECT CODEMPRESA AS 'Código da Empresa', 
	                                   CODFILIAL AS 'Código da Filial', 
	                                   CODCLIFOR AS 'Código do Cliente',
	                                   CODLOTE AS 'Código do Lote', 
	                                   NUMERO AS 'Número do Lote', 
	                                   DATAENTRADA 'Data de Entrada', 
	                                   DATALOTE 'Data do Lote', 
	                                   DATAFABRICACAO 'Data de Fabricação',
	                                   DATAVALIDADE 'Data de Validade', 
	                                        (CASE STATUS
		                                    WHEN 1 THEN 'Liberado'
		                                    WHEN 2 THEN 'Bloqueado'
	                                        END) AS 'Status'
                            FROM VPRODUTOLOTE
                            WHERE CODEMPRESA = ?
                            AND CODFILIAL = ?
                            AND CODCLIFOR = ?";

                    dtVisaoLote = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, AppLib.Context.Empresa, FilialLote, Codclifor);
                }
                if (TipoOperEstoque == "D")
                {
                    sql = @"SELECT X.* FROM (
	                        SELECT VPRODUTOLOTE.CODEMPRESA AS 'Código da Empresa', VPRODUTOLOTE.CODFILIAL AS 'Código da Filial', VPRODUTOLOTE.CODLOTE AS 'Código do Lote', VPRODUTOLOTE.NUMERO AS 'Número do Lote', SUM(GOPERITEMLOTE.QUANTIDADE) AS 'Quantidade', GOPERITEMLOTE.NSEQITEM AS 'Sequencial', GOPERITEMLOTE.CODUNIDCONTROLE AS 'Unidade de Controle', VPRODUTOLOTE.DATAFABRICACAO AS 'Data de fabricação', VPRODUTOLOTE.DATAENTRADA AS 'Data de entrada', VPRODUTOLOTE.DATACRIACAO AS 'Data de criação' 
	                        FROM VPRODUTOLOTE    
	                        INNER JOIN GOPERITEMLOTE ON GOPERITEMLOTE.CODEMPRESA = VPRODUTOLOTE.CODEMPRESA AND GOPERITEMLOTE.CODFILIAL = VPRODUTOLOTE.CODFILIAL AND VPRODUTOLOTE.CODLOTE = GOPERITEMLOTE.CODLOTE  
	                        WHERE VPRODUTOLOTE.CODEMPRESA = ? AND VPRODUTOLOTE.CODFILIAL = ? AND GOPERITEMLOTE.CODPRODUTO = ? AND VPRODUTOLOTE.STATUS = 1 
	                        GROUP BY VPRODUTOLOTE.CODEMPRESA, VPRODUTOLOTE.CODFILIAL, VPRODUTOLOTE.CODLOTE, VPRODUTOLOTE.NUMERO, GOPERITEMLOTE.NSEQITEM, GOPERITEMLOTE.CODUNIDCONTROLE , VPRODUTOLOTE.DATAFABRICACAO, VPRODUTOLOTE.DATAENTRADA, VPRODUTOLOTE.DATACRIACAO
                            )X
                            WHERE X.Quantidade > 0
                            ORDER BY X.[Data de criação] ASC";

                    dtVisaoLote = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, AppLib.Context.Empresa, FilialLote, codProduto);
                }

                gridControl5.DataSource = null;
                gridView5.Columns.Clear();
                gridControl5.DataSource = dtVisaoLote;
                gridView5.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Método para limpar as descrições do Lote
        /// </summary>
        private void LimpaDescricaoLote()
        {
            lbValorLote.Text = string.Empty;
            lbValorNumeroLote.Text = string.Empty;
            lbUnidadeControle.Text = string.Empty;
        }

        /// <summary>
        /// Método para limpar os indicadores de quantidade do lote
        /// </summary>
        private void LimpaQuantidadesLote()
        {
            lbQtdControle.Text = string.Empty;
            gridControl4.Text = string.Empty;
            lbQtdDiferenca.Text = string.Empty;
        }

        /// <summary>
        /// Método para carregar as descrições iniciais do Lote.
        /// </summary>
        private void CarregaDescricoesIniciais()
        {
            lbQtdControle.Text = tbQuantidadeConversao.Text;
            lbQtdlote.Text = "0,0000";
            lbQtdDiferenca.Text = "0,0000";

            // Trata o valor dos campos
            if (string.IsNullOrEmpty(lbQtdlote.Text))
            {
                lbQtdlote.Text = "0,0000";
            }

            if (string.IsNullOrEmpty(lbQtdControle.Text))
            {
                lbQtdControle.Text = "0,0000";
            }

            // Atribui valor para as variáveis
            QtdLote = Convert.ToDecimal(lbQtdlote.Text);
            QtdControle = Convert.ToDecimal(lbQtdControle.Text);
        }

        /// <summary>
        /// Método para carregar os campos do lote selecionado
        /// </summary>
        private void CarregaCamposLote()
        {
            DataRow row = gridView5.GetDataRow(Convert.ToInt32(gridView5.GetSelectedRows().GetValue(0).ToString()));

            lbValorLote.Text = row["Código do Lote"].ToString();
            lbValorNumeroLote.Text = row["Número do Lote"].ToString();
            lbUnidadeControle.Text = tbUnidadeControle.Text;
            //lbQuantidadeLote.Text = row["Quantidade"].ToString();
        }


        /// <summary>
        /// Método para carregar os campos do lote selecionado
        /// </summary>
        private void CarregaValoresLote()
        {
            try
            {
                DataRow row = gridView4.GetDataRow(Convert.ToInt32(gridView4.GetSelectedRows().GetValue(0).ToString()));

                lbValorLote.Text = row["Código do Lote"].ToString();
                lbValorNumeroLote.Text = row["Número do Lote"].ToString();
                lbUnidadeControle.Text = tbUnidadeControle.Text;
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Método para atribuir o valor inicial da diferença do lote
        /// </summary>
        private void DiferencaInicial()
        {
            lbQtdDiferenca.Text = (Convert.ToDecimal(lbQtdlote.Text) - Convert.ToDecimal(lbQtdControle.Text)).ToString();

            if (lbQtdDiferenca.Text.Contains("-"))
            {
                lbQtdDiferenca.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// Método que atribui um valor inicial para a Quantidade de Entrada
        /// </summary>
        private void AtribuiQuantidadeEntrada()
        {
            QtdLote = Convert.ToDecimal(tbQuantidadeConversao.Text);

            if (QtdLote == 0)
            {
                tbQuantidadeEntrada.Text = "0,0000";
            }
            else
            {
                tbQuantidadeEntrada.Text = QtdLote.ToString(".0000");
            }
        }

        private void tbQuantidadeLote_Leave(object sender, EventArgs e)
        {
            CalculaQuantidadeLote();
            tbQuantidadeEntrada.Text = QtdLote.ToString(".0000");
        }

        /// <summary>
        /// Método para atribuir o valor do campo Quantidade Lote para a variável QtdLote
        /// </summary>
        private void CalculaQuantidadeLote()
        {
            QtdLote = Convert.ToDecimal(tbQuantidadeEntrada.Text);
        }

        /// <summary>
        /// Método para atribuir valor ao Label de quantidade do Lote 
        /// </summary>
        /// <param name="QuantidadeLote">Quantidade do Lote</param>
        private void AtribuiQuantidadeLote(decimal QuantidadeLote)
        {
            lbQtdlote.Text = (Convert.ToDecimal(lbQtdlote.Text) + QuantidadeLote).ToString("0.0000");
        }

        private void tbQuantidadeConversao_TextChanged(object sender, EventArgs e)
        {
            CalculaQuantidadeControle();
        }

        /// <summary>
        /// Método para atribuir o valor do campo Quantidade de controle para a variável QtdControle
        /// </summary>
        private void CalculaQuantidadeControle()
        {
            QtdControle = Convert.ToDecimal(tbQuantidadeConversao.Text);
            if (QtdControle == 0)
            {
                lbQtdControle.Text = QtdControle.ToString("0.0000");
            }
            else
            {
                lbQtdControle.Text = QtdControle.ToString(".0000");
            }
        }

        /// <summary>
        /// Método para calcular a diferença do lote
        /// </summary>
        private void CalculaDiferenca()
        {
            if (Convert.ToDecimal(lbQtdlote.Text) - QtdControle == 0)
            {
                lbQtdDiferenca.Text = "0,0000";
                tbQuantidadeEntrada.Text = "0,0000";
            }
            else
            {
                lbQtdDiferenca.Text = (Convert.ToDecimal(lbQtdlote.Text) - QtdControle).ToString(".0000");

                QuantidadeSugerida = SugestaoQuantidade(Convert.ToDecimal(lbQtdDiferenca.Text));
                tbQuantidadeEntrada.Text = QuantidadeSugerida.ToString(".0000");
            }

            if (lbQtdDiferenca.Text.Contains("-"))
            {
                lbQtdDiferenca.ForeColor = Color.Red;
            }
            else
            {
                lbQtdDiferenca.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private decimal RecalculaQuantidadeLote(DataRow row)
        {
            QtdLote = Convert.ToDecimal(row["Quantidade"]);

            QtdLote = Convert.ToDecimal(lbQtdlote.Text) - QtdLote;

            return QtdLote;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Diferenca"></param>
        /// <returns></returns>
        private decimal SugestaoQuantidade(decimal Diferenca)
        {
            if (Diferenca.ToString().Contains("-"))
            {
                QuantidadeSugerida = Diferenca * -1;
            }

            return QuantidadeSugerida;
        }

        /// <summary>
        /// Método para validar se o produto usa ou não lote
        /// </summary>
        /// <returns>Usa lote do produto</returns>
        private bool getUsaLoteProduto()
        {
            bool UsaLote = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT USALOTEPRODUTO FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, codProduto }));

            return UsaLote;
        }

        private decimal getQuantidade()
        {
            decimal Quantidade = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT SUM(QUANTIDADE)
                                                                                                            FROM GOPERITEMLOTE
                                                                                                            WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND NSEQITEM = ? AND CODPRODUTO = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, FilialLote, LocalLote, nseqitem, codProduto, codOper }));
            return Quantidade;
        }

        /// <summary>
        /// Método para criar as colunas e os registros da Grid
        /// </summary>
        /// <returns></returns>
        private DataTable CriaColunasLote()
        {
            DataColumn ColEmpresa = new DataColumn("Código da Empresa", typeof(int));
            DataColumn ColFilial = new DataColumn("Código da Filial", typeof(int));
            DataColumn ColCodlote = new DataColumn("Código do Lote", typeof(int));
            DataColumn ColSequencial = new DataColumn("Sequencial", typeof(int));
            DataColumn ColNumerolote = new DataColumn("Número do Lote", typeof(string));
            DataColumn ColQuantidade = new DataColumn("Quantidade", typeof(decimal));

            // Row da visão da Grid
            DataRow row = gridView5.GetDataRow(Convert.ToInt32(gridView5.GetSelectedRows().GetValue(0).ToString()));

            if (dtLote.Columns.Contains("Código da Empresa") || dtLote.Columns.Contains("Código da Filial") || dtLote.Columns.Contains("Código do Lote") || dtLote.Columns.Contains("Sequencial") || dtLote.Columns.Contains("Número do Lote") || dtLote.Columns.Contains("Quantidade"))
            {
                dtLote.Rows.Add(AppLib.Context.Empresa, FilialLote, row["Código do Lote"], NseqItemLote, row["Número do Lote"], Convert.ToDecimal(tbQuantidadeEntrada.Text).ToString(".0000"));
                return dtLote;
            }

            dtLote.Columns.Add(ColEmpresa);
            dtLote.Columns.Add(ColFilial);
            dtLote.Columns.Add(ColCodlote);
            dtLote.Columns.Add(ColSequencial);
            dtLote.Columns.Add(ColNumerolote);
            dtLote.Columns.Add(ColQuantidade);

            if (string.IsNullOrEmpty(tbQuantidadeEntrada.Text))
            {
                MessageBox.Show("Favor informar a quantidade", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            dtLote.Rows.Add(AppLib.Context.Empresa, FilialLote, row["Código do Lote"], NseqItemLote, row["Número do Lote"], Convert.ToDecimal(tbQuantidadeEntrada.Text).ToString(".0000"));
            return dtLote;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_codLote"></param>
        /// <returns></returns>
        private DataTable CriaVisaoInicial(int _codLote)
        {
            DataTable dtVprodutoLote = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPRODUTOLOTE WHERE CODEMPRESA = ? AND CODLOTE = ?", new object[] { AppLib.Context.Empresa, _codLote });

            if (dtVprodutoLote.Rows.Count <= 0)
            {
                return dtVisaoLote;
            }

            DataRow row = dtVprodutoLote.Rows[0];

            DataColumn ColEmpresa = new DataColumn("Código da Empresa", typeof(int));
            DataColumn ColFilial = new DataColumn("Código da Filial", typeof(int));
            DataColumn ColCodlote = new DataColumn("Código do Lote", typeof(int));
            DataColumn ColNumerolote = new DataColumn("Número do Lote", typeof(string));
            DataColumn ColQuantidade = new DataColumn("Quantidade", typeof(decimal));
            DataColumn ColSequencial = new DataColumn("Sequencial", typeof(int));
            DataColumn ColUnidControle = new DataColumn("Unidade de Controle", typeof(string));
            DataColumn ColDataFabricacao = new DataColumn("Data de Fabricação", typeof(DateTime));
            DataColumn ColDataEntrada = new DataColumn("Data de Entrada", typeof(DateTime));
            DataColumn ColDataCriacao = new DataColumn("Data de Criação", typeof(DateTime));

            if (dtVisaoLote.Rows.Count > 0)
            {
                dtVisaoLote.Rows.Add(dtVprodutoLote.Rows[0]["CODEMPRESA"], dtVprodutoLote.Rows[0]["CODFILIAL"], dtVprodutoLote.Rows[0]["CODLOTE"], dtVprodutoLote.Rows[0]["NUMERO"], "0,0000", NseqItemLote, tbUnidadeControle.Text, dtVprodutoLote.Rows[0]["DATAFABRICACAO"], dtVprodutoLote.Rows[0]["DATAENTRADA"], dtVprodutoLote.Rows[0]["DATACRIACAO"]);
                return dtVisaoLote;
            }

            if (dtVisaoInicial.Rows.Count > 0)
            {
                dtVisaoInicial.Rows.Add(dtVprodutoLote.Rows[0]["CODEMPRESA"], dtVprodutoLote.Rows[0]["CODFILIAL"], dtVprodutoLote.Rows[0]["CODLOTE"], dtVprodutoLote.Rows[0]["NUMERO"], "0,0000", NseqItemLote, tbUnidadeControle.Text, dtVprodutoLote.Rows[0]["DATAFABRICACAO"], dtVprodutoLote.Rows[0]["DATAENTRADA"], dtVprodutoLote.Rows[0]["DATACRIACAO"]);
                return dtVisaoInicial;
            }

            dtVisaoInicial.Columns.Add(ColEmpresa);
            dtVisaoInicial.Columns.Add(ColFilial);
            dtVisaoInicial.Columns.Add(ColCodlote);
            dtVisaoInicial.Columns.Add(ColNumerolote);
            dtVisaoInicial.Columns.Add(ColQuantidade);
            dtVisaoInicial.Columns.Add(ColSequencial);
            dtVisaoInicial.Columns.Add(ColUnidControle);
            dtVisaoInicial.Columns.Add(ColDataFabricacao);
            dtVisaoInicial.Columns.Add(ColDataEntrada);
            dtVisaoInicial.Columns.Add(ColDataCriacao);

            dtVisaoInicial.Rows.Add(dtVprodutoLote.Rows[0]["CODEMPRESA"], dtVprodutoLote.Rows[0]["CODFILIAL"], dtVprodutoLote.Rows[0]["CODLOTE"], dtVprodutoLote.Rows[0]["NUMERO"], "0,0000", NseqItemLote, tbUnidadeControle.Text, dtVprodutoLote.Rows[0]["DATAFABRICACAO"], dtVprodutoLote.Rows[0]["DATAENTRADA"], dtVprodutoLote.Rows[0]["DATACRIACAO"]);

            return dtVisaoInicial;
        }

        private void ExecTransactionLote()
        {
            List<int> ListUpdate = new List<int>();
            List<int> ListInsert = new List<int>();

            List<Registros> Records = new List<Registros>();
            List<Insert> Insert = new List<Insert>();
            List<Update> Update = new List<Update>();

            for (int i = 0; i < dtLote.Rows.Count; i++)
            {
                int CodigoOperacao = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT COUNT(CODOPER)
                                                                                                                 FROM GOPERITEMLOTE
                                                                                                                 WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODLOTE = ? AND NSEQITEM = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, FilialLote, LocalLote, dtLote.Rows[i]["Código do Lote"], NseqItemLote, codProduto }));
                if (CodigoOperacao == 0)
                {
                    Insert.Add(new Insert(codOper, Convert.ToInt32(dtLote.Rows[i]["Código do Lote"]), Convert.ToDecimal(dtLote.Rows[i]["Quantidade"])));
                }
                else
                {
                    CodigoOperacao = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(codOper, "SELECT CODOPER FROM GOPERITEMLOTE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODLOTE = ? AND NSEQITEM = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, FilialLote, LocalLote, dtLote.Rows[i]["Código do Lote"], NseqItemLote, codProduto }));
                    Update.Add(new Update(CodigoOperacao, Convert.ToInt32(dtLote.Rows[i]["Código do Lote"]), Convert.ToDecimal(dtLote.Rows[i]["Quantidade"])));
                }

                //int CodigoOperacao = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(codOper, "SELECT CODOPER FROM GOPERITEMLOTE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODLOTE = ? AND NSEQITEM = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, FilialLote, LocalLote, dtLote.Rows[i]["Código do Lote"], nseqitem, codProduto }));
                //int CodigoLote = Convert.ToInt32(dtLote.Rows[i]["Código do Lote"]);
                //decimal Quantidade = Convert.ToDecimal(dtLote.Rows[i]["Quantidade"]);

                //Records.Add(new Registros(CodigoOperacao, CodigoLote, Quantidade));
            }

            for (int i = 0; i < Records.Count; i++)
            {
                // Se o código da Operação for o mesmo que o da operação selecionada, alimenta a lista para realização do Update
                if (Records[i].CODOPER == codOper)
                {
                    Update.Add(new Update(Records[i].CODOPER, Records[i].CODLOTE, Records[i].QUANTIDADE));
                }
                // Se o código da Operação for diferente que o da operação selecionada, alimenta a lista para realização do Insert
                else
                {
                    Insert.Add(new Insert(Records[i].CODOPER, Records[i].CODLOTE, Records[i].QUANTIDADE));
                }
            }

            switch (TipoOperEstoque)
            {
                case "A":

                    // Verfiica se existe registro
                    if (Update.Count > 0)
                    {
                        for (int i = 0; i < Update.Count; i++)
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GOPERITEMLOTE SET QUANTIDADE = (QUANTIDADE + ?) WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODLOTE  = ? AND CODOPER = ?", new object[] { Update[i].QUANTIDADE, AppLib.Context.Empresa, FilialLote, LocalLote, Update[i].CODLOTE, Update[i].CODOPER });
                        }
                    }

                    // Verfiica se existe registro
                    if (Insert.Count > 0)
                    {
                        for (int i = 0; i < Insert.Count; i++)
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO GOPERITEMLOTE(CODEMPRESA, CODFILIAL, CODLOCAL, CODLOTE, CODOPER, NSEQITEM, CODPRODUTO, QUANTIDADE, CODUNIDCONTROLE, QUANTIDADEOPER, CODUNIDOPER, USUARIOCRIACAO, DATACRIACAO, USUARIOALTERACAO, DATAALTERACAO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, FilialLote, LocalLote, Insert[i].CODLOTE, codOper, NseqItemLote, codProduto, Insert[i].QUANTIDADE, tbUnidadeControle.Text, Convert.ToDecimal(tbQuantidadeConversao.Text), lpUnidadeMedida.txtcodigo.Text, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now), null, null });
                        }
                    }

                    break;
                case "D":

                    // Verfiica se existe registro
                    if (ListUpdate.Count > 0)
                    {
                        for (int i = 0; i < ListUpdate.Count; i++)
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GOPERITEMLOTE SET QUANTIDADE = (QUANTIDADE + ?) WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODLOTE  = ? AND CODOPER = ?", new object[] { Convert.ToDecimal(dtLote.Rows[i]["Quantidade"]), AppLib.Context.Empresa, FilialLote, LocalLote, dtLote.Rows[i]["Código do Lote"], ListUpdate[i] });
                        }
                    }

                    // Verfiica se existe registro
                    if (ListInsert.Count > 0)
                    {
                        for (int i = 0; i < ListInsert.Count; i++)
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO GOPERITEMLOTE(CODEMPRESA, CODFILIAL, CODLOCAL, CODLOTE, CODOPER, NSEQITEM, CODPRODUTO, QUANTIDADE, CODUNIDCONTROLE, QUANTIDADEOPER, CODUNIDOPER, USUARIOCRIACAO, DATACRIACAO, USUARIOALTERACAO, DATAALTERACAO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, FilialLote, LocalLote, Convert.ToInt32(dtLote.Rows[i]["Código do Lote"]), ListInsert[i], Convert.ToInt32(dtLote.Rows[i]["Sequencial"]), codProduto, (Convert.ToDecimal(dtLote.Rows[i]["Quantidade"]) * -1), tbUnidadeControle.Text, Convert.ToDecimal(tbQuantidadeConversao.Text), lpUnidadeMedida.txtcodigo.Text, AppLib.Context.Usuario, Convert.ToDateTime(DateTime.Now), null, null });
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        //private void ExecVFICHAESTOQUELOTE()
        //{
        //    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
        //    Class.VFICHAESTOQUELOTE FichaLote = new Class.VFICHAESTOQUELOTE();
        //    List<Class.VFICHAESTOQUELOTE> Lote = new List<Class.VFICHAESTOQUELOTE>();

        //    TipoOperEstoque = conn.ExecGetField(string.Empty, "SELECT OPERESTOQUE FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper }).ToString();
        //    DataEntSai = Convert.ToDateTime(conn.ExecGetField(null, "SELECT DATAENTSAI FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codOper }));

        //    FichaLote.OperEstoque = TipoOperEstoque;
        //    FichaLote.DataEntSai = DataEntSai;
        //    Lote = FichaLote.getValoresItemLote(AppLib.Context.Empresa, FilialLote, LocalLote, codOper, NseqItemLote, codProduto, conn);
        //    FichaLote.setVFICHAESTOQUELOTE(Lote, conn);
        //}

        #endregion

        #endregion

        #region Classes para o Lote

        class Registros
        {
            public int CODOPER { get; set; }
            public int CODLOTE { get; set; }
            public decimal QUANTIDADE { get; set; }

            public Registros(int _codoper, int _codlote, decimal _quantidade)
            {
                CODOPER = _codoper;
                CODLOTE = _codlote;
                QUANTIDADE = _quantidade;
            }
        }
        class Insert
        {
            public int CODOPER { get; set; }
            public int CODLOTE { get; set; }
            public decimal QUANTIDADE { get; set; }

            public Insert(int _codoper, int _codlote, decimal _quantidade)
            {
                CODOPER = _codoper;
                CODLOTE = _codlote;
                QUANTIDADE = _quantidade;
            }
        }

        class Update
        {
            public int CODOPER { get; set; }
            public int CODLOTE { get; set; }
            public decimal QUANTIDADE { get; set; }

            public Update(int _codoper, int _codlote, decimal _quantidade)
            {
                CODOPER = _codoper;
                CODLOTE = _codlote;
                QUANTIDADE = _quantidade;
            }
        }

        #endregion

        private void btnNovoLote_Click_1(object sender, EventArgs e)
        {
            PS.Glb.New.Cadastros.frmCadastroLote frm = new New.Cadastros.frmCadastroLote();
            frm.CodClifor = Codclifor;
            frm.CodFilial = FilialLote;
            frm.ShowDialog();
            CarregaGridLote(true);
        }

        private void btnExcluirLote_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Todos os lotes vinculados a esta Operação serão excluídos, deseja continuar?", "Confirmação.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DataTable dtExclusaoItem = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT *
                                                                                                  FROM GOPERITEMLOTE
                                                                                                  WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codOper });
                    // GOPERITEMLOTE
                    for (int i = 0; i < dtExclusaoItem.Rows.Count; i++)
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GOPERITEMLOTE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODOPER = ? AND CODLOTE = ? AND NSEQITEM = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, dtExclusaoItem.Rows[i]["CODFILIAL"], dtExclusaoItem.Rows[i]["CODLOCAL"], dtExclusaoItem.Rows[i]["CODOPER"], dtExclusaoItem.Rows[i]["CODLOTE"], dtExclusaoItem.Rows[i]["NSEQITEM"], dtExclusaoItem.Rows[i]["CODPRODUTO"] });
                    }

                    // VPRODUTOLOTE
                    for (int i = 0; i < dtExclusaoItem.Rows.Count; i++)
                    {
                        int ValidaItem = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT COUNT(CODLOTE) FROM GOPERITEMLOTE WHERE CODEMPRESA = ? AND CODOPER = ? AND CODLOTE = ?", new object[] { AppLib.Context.Empresa, dtExclusaoItem.Rows[i]["CODOPER"], dtExclusaoItem.Rows[i]["CODLOTE"] }));

                        if (ValidaItem == 0)
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM VPRODUTOLOTE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOTE = ?", new object[] { AppLib.Context.Empresa, dtExclusaoItem.Rows[i]["CODFILIAL"], dtExclusaoItem.Rows[i]["CODLOTE"] });
                        }
                        else
                        {
                            continue;
                        }
                    }

                    MessageBox.Show("Registro(s) excluído(s) com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CarregaDescricoesIniciais();
                    CalculaQuantidadeControle();
                    CalculaDiferenca();
                    btnNovoLote.Enabled = true;

                    if (tbQuantidadeEntrada.ReadOnly == true)
                    {
                        tbQuantidadeEntrada.ReadOnly = false;
                    }

                    CarregaGridLote(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnIncluir_Click_1(object sender, EventArgs e)
        {
            CalculaQuantidadeLote();
            AtribuiQuantidadeLote(QtdLote);

            if (Convert.ToDecimal(lbQtdlote.Text) > Convert.ToDecimal(lbQtdControle.Text))
            {
                MessageBox.Show("Quantidade de entrada do lote não pode ser maior que a quantidade de controle.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lbQtdlote.Text = lbQtdControle.Text;

                return;
            }

            gridControl4.DataSource = CriaColunasLote();
            gridView4.BestFitColumns();

            CalculaDiferenca();

            if (lbQtdDiferenca.Text == "0,0000")
            {
                btnIncluir.Enabled = false;
            }

            btnExcluirLote.Enabled = true;
            btnExcluirRegistro.Enabled = true;
        }

        private void btnExcluirRegistro_Click_1(object sender, EventArgs e)
        {
            if (gridView4.SelectedRowsCount == 0)
            {
                MessageBox.Show("Selecione um item para a exclusão.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow row = gridView4.GetDataRow(Convert.ToInt32(gridView4.GetSelectedRows().GetValue(0).ToString()));

            QtdLote = RecalculaQuantidadeLote(row);

            if (QtdLote == 0)
            {
                lbQtdlote.Text = QtdLote.ToString("0.0000");
            }
            else
            {
                lbQtdlote.Text = QtdLote.ToString(".0000");
            }

            if (Convert.ToDecimal(lbQtdlote.Text) > Convert.ToDecimal(lbQtdControle.Text))
            {
                lbQtdlote.ForeColor = Color.Red;
            }
            else
            {
                lbQtdlote.ForeColor = Color.Black;
            }

            CalculaDiferenca();

            gridView4.DeleteSelectedRows();

            if (gridView4.RowCount == 0)
            {
                LimpaDescricaoLote();

                btnExcluirLote.Enabled = false;
                btnExcluirRegistro.Enabled = false;
            }
        }

        private void lookupproduto_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(tbQuantidadeConversao.Text) <= 0)
            {
                groupBox5.Enabled = false;
            }
            else
            {
                UsaLoteProduto = getUsaLoteProduto();
                if (ValidaProdOper(UsaAbaLote, UsaLoteProduto) == false)
                {
                    return;
                }
                else
                {
                    DiferencaInicial();
                }
            }
        }

        private void gridControl5_DoubleClick(object sender, EventArgs e)
        {
            if (gridView5.SelectedRowsCount == 0)
            {
                return;
            }

            if (tbQuantidadeEntrada.Text != "0,0000")
            {
                btnIncluir.Enabled = true;
                CarregaCamposLote();
                return;
            }

            if (tbQuantidadeEntrada.Text == "0,0000")
            {
                btnIncluir.Enabled = false;
                return;
            }

            if (ValidaQuantidade() == 0)
            {
                btnIncluir.Enabled = false;
                return;
            }
            else
            {
                CarregaCamposLote();
                btnIncluir.Enabled = true;
            }
        }

        private void gridControl4_Click(object sender, EventArgs e)
        {
            CarregaValoresLote();
        }

        private void tbQuantidadeEntrada_Validated(object sender, EventArgs e)
        {
            tbQuantidadeEntrada.Text = string.Format("{0:n4}",Convert.ToDecimal(tbQuantidadeEntrada.Text));
        }
    }
}
