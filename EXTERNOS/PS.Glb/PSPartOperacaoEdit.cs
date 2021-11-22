using ITGProducao.Controles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartOperacaoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        #region Variaveis

        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        private PS.Glb.Formula.Function function = new PS.Glb.Formula.Function();
        private PS.Lib.Interpretador interpreta = new PS.Lib.Interpretador();

        private PS.Lib.WinForms.PSTextoBox GLB_PSTEXTBOX_SERIE;
        private PS.Lib.WinForms.PSLookup GLB_PSLOOKUP_SERIE;

        private int GLB_SERIELIVRE = 0;
        private int GLB_CLIFORMAMB = 0;

        private decimal OP_VALORBRUTO = 0;
        private decimal OP_VALORLIQUIDO = 0;


        //private bool incluir, excluir, alterar, faturar, incluirFat, consultar;
        private bool editaItem = true;
        //string codoper = string.Empty;

        private DataRow GTIPOPER;

        public int codoper;
        public string codtipoper;
        public bool edita = false;
        private bool item = false;

        public bool faturamento = false;
        private bool composto = false;

        private int MASKNUMEROSEQ = 0;
        private string operCodCliFor;
        private string operTipEntSai;
        private string numero;
        private string codSerie;

        public string codMenu;
        public int codFilial;
        public string CodoperOrigem = string.Empty;
        public bool usaAprovacao;
        private bool aprovado;
        public string Codlocal;

        private decimal vlFrete = 0, vlDesconto = 0, vlDespesa = 0, vlSeguro = 0;

        public bool VerificaCancela = true;

        private string DataEntrega;

        // Variáveis para o lançamento financeiro
        string tabela = "FLANCA";
        string Relacionamento = "INNER JOIN VCLIFOR ON FLANCA.CODCLIFOR = VCLIFOR.CODCLIFOR AND FLANCA.CODEMPRESA = VCLIFOR.CODEMPRESA";
        string query = string.Empty;
        List<string> tabelasFilhas = new List<string>();
        //
        #endregion

        // Variáveis para quando a operação vier do processo de cópia
        public int NumeroOPer;
        public string SerieOper;

        // Variáveis para validação da Ficha de Estoque
        public bool CopiaOperacao = false;
        public bool FaturamentoOperacao = false;

        // Variáveis para validação do fechamento de Estoque
        public DateTime? DataEmissao;
        public bool DesabilitaBotao = false;

        #region Antigo
        public PSPartOperacaoEdit()
        {
            InitializeComponent();
            splitContainer1.SplitterDistance = 30;
            new Class.Utilidades().getDicionario(this, tabControl1, "GOPER");

            //new Class.Utilidades().criaCamposComplementares("GOPERCOMPL", tabCamposComplementares);

            psValorBruto.Caption = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPER' AND COLUNA = 'GOPER.VALORBRUTO'", new object[] { }).ToString();
            psValorLiquido.Caption = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPER' AND COLUNA = 'GOPER.VALORLIQUIDO'", new object[] { }).ToString();


            //psLookup2.PSPart = "PSPartCliFor";
            psLookup3.PSPart = "PSPartFilial";
            psLookup17.PSPart = "PSPartFilial";
            psLookup4.PSPart = "PSPartObjeto";
            psLookup5.PSPart = "PSPartTransportadora";
            psLookup6.PSPart = "PSPartCondicaoPgto";
            psLookup7.PSPart = "PSPartLocalEstoque";
            psLookup9.PSPart = "PSPartLocalEstoque";
            psLookup11.PSPart = "PSPartOperador";
            // João Pedro Luchiari - Comentado pois o Lookup de Forma de Pagamento foi trocado
            //psLookup12.PSPart = "PSPartFormaPgto";
            psLookup8.PSPart = "PSPartConta";
            psLookup15.PSPart = "PSPartRepre";
            psLookup26.PSPart = "PSPartVendedor";
            psLookup10.PSPart = "PSPartOperMensagem";
            psLookup1.PSPart = "PSPartOperMensagem";
            psLookup2.PSPart = "PSPartOperMensagem";
            psLookup13.PSPart = "PSPartCentroCusto";
            psLookup16.PSPart = "PSPartNaturezaOrcamento";
            psLookup14.PSPart = "PSPartTipoTransporte";
            //psLookup18.PSPart = "PSPartContatoCliFor";
            psLookup19.PSPart = "PSPartEstado";
            psMoedaBox2.Edita = false;

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 0;
            list1[0].DisplayMember = "Aberto";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 1;
            list1[1].DisplayMember = "Faturado";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = 2;
            list1[2].DisplayMember = "Cancelado";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[3].ValueMember = 3;
            list1[3].DisplayMember = "Parcialmente Quitado";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[4].ValueMember = 4;
            list1[4].DisplayMember = "Quitado";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[5].ValueMember = 5;
            list1[5].DisplayMember = "Parcialmente Faturado";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[6].ValueMember = 6;
            list1[6].DisplayMember = "Parcialmente Faturado / Finalizado";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[7].ValueMember = 7;
            list1[7].DisplayMember = "Bloqueado";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[8].ValueMember = 8;
            list1[8].DisplayMember = "Concluído";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";

            #region Transporte

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = 0;
            list2[0].DisplayMember = "CIF";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = 1;
            list2[1].DisplayMember = "FOB";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[2].ValueMember = 2;
            list2[2].DisplayMember = "Terceiro";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[3].ValueMember = 3;
            list2[3].DisplayMember = "Próprio Remetente";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[4].ValueMember = 4;
            list2[4].DisplayMember = "Próprio Destinatário";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[5].ValueMember = 9;
            list2[5].DisplayMember = "Sem Frete";

            psComboBox2.DataSource = list2;
            psComboBox2.DisplayMember = "DisplayMember";
            psComboBox2.ValueMember = "ValueMember";

            #endregion

            psCodTipOper.Edita = false;
            psCodUsuarioCriacao.Edita = false;
            psMaskedTextBox1.Chave = false;

            List<PS.Lib.ComboBoxItem> list4 = new List<PS.Lib.ComboBoxItem>();

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[0].ValueMember = "T";
            list4[0].DisplayMember = "Total";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[1].ValueMember = "I";
            list4[1].DisplayMember = "Item";

            psComboBox4.DataSource = list4;
            psComboBox4.DisplayMember = "DisplayMember";
            psComboBox4.ValueMember = "ValueMember";

            //Carrega os campos

            #region Operação Presencial

            List<PS.Lib.ComboBoxItem> List5 = new List<PS.Lib.ComboBoxItem>();
            List5.Add(new PS.Lib.ComboBoxItem());
            List5[0].ValueMember = "0";
            List5[0].DisplayMember = "Não se aplica";

            List5.Add(new PS.Lib.ComboBoxItem());
            List5[1].ValueMember = "1";
            List5[1].DisplayMember = "Operação presencial";

            List5.Add(new PS.Lib.ComboBoxItem());
            List5[2].ValueMember = "2";
            List5[2].DisplayMember = "Operação não presencial, pela Internet";

            List5.Add(new PS.Lib.ComboBoxItem());
            List5[3].ValueMember = "3";
            List5[3].DisplayMember = "Operação não presencial, Teleatendimento";

            List5.Add(new PS.Lib.ComboBoxItem());
            List5[4].ValueMember = "4";
            List5[4].DisplayMember = "NFC-e em operação com entrega a domicílio";

            List5.Add(new PS.Lib.ComboBoxItem());
            List5[5].ValueMember = "5";
            List5[5].DisplayMember = "Operação presencial, fora do estabelecimento";

            List5.Add(new PS.Lib.ComboBoxItem());
            List5[6].ValueMember = "9";
            List5[6].DisplayMember = "Operação não presencial, outros";

            cmbOperacaoPresencial.DataSource = List5;
            cmbOperacaoPresencial.DisplayMember = "DisplayMember";
            cmbOperacaoPresencial.ValueMember = "ValueMember";

            #endregion
        }



        public decimal? CalculaValor(decimal? valorBruto, decimal? percentual)
        {
            return (valorBruto * (percentual / 100));
        }

        public decimal? CalculaPercentual(decimal? valorBruto, decimal? valor)
        {
            return ((valor / valorBruto) * 100);
        }

        private void CriarControleSerieLookup()
        {
            if (this.GLB_PSLOOKUP_SERIE == null)
            {

                this.GLB_PSLOOKUP_SERIE = new PS.Lib.WinForms.PSLookup();
                this.GLB_PSLOOKUP_SERIE.Caption = "Série";
                this.GLB_PSLOOKUP_SERIE.Chave = true;
                this.GLB_PSLOOKUP_SERIE.DataField = "CODSERIE";
                this.GLB_PSLOOKUP_SERIE.KeyField = "CODSERIE";
                this.GLB_PSLOOKUP_SERIE.Location = new System.Drawing.Point(565, 19);
                this.GLB_PSLOOKUP_SERIE.LookupField = "CODSERIE;DESCRICAO";
                this.GLB_PSLOOKUP_SERIE.LookupFieldResult = "CODSERIE;DESCRICAO";
                this.GLB_PSLOOKUP_SERIE.Name = "GLB_PSLOOKUP_SERIE";
                this.GLB_PSLOOKUP_SERIE.PSPart = "PSPartTipOperSerie";
                this.GLB_PSLOOKUP_SERIE.Size = new System.Drawing.Size(126, 38);
                this.GLB_PSLOOKUP_SERIE.TabIndex = 3;
                this.GLB_PSLOOKUP_SERIE.ValorRetorno = null;
                this.GLB_PSLOOKUP_SERIE.BeforeLookup += new PS.Lib.WinForms.PSLookup.BeforeLookupDelegate(this.GLB_PSLOOKUP_SERIE_BeforeLookup);
                this.GLB_PSLOOKUP_SERIE.Caption = gb.NomeDoCampo("GOPER", "CODSERIE");

                this.groupBox5.Controls.Add(this.GLB_PSLOOKUP_SERIE);
            }
        }

        private void CriaControleSerieTextBox()
        {

            if (this.GLB_PSTEXTBOX_SERIE == null)
            {
                this.GLB_PSTEXTBOX_SERIE = new PS.Lib.WinForms.PSTextoBox();

                this.GLB_PSTEXTBOX_SERIE.Caption = "Série";
                this.GLB_PSTEXTBOX_SERIE.Edita = true;
                this.GLB_PSTEXTBOX_SERIE.DataField = "CODSERIE";
                this.GLB_PSTEXTBOX_SERIE.Location = new System.Drawing.Point(565, 19);
                this.GLB_PSTEXTBOX_SERIE.Name = "GLB_PSTEXTBOX_SERIE";
                this.GLB_PSTEXTBOX_SERIE.PasswordChar = '\0';
                this.GLB_PSTEXTBOX_SERIE.Size = new System.Drawing.Size(126, 38);
                this.GLB_PSTEXTBOX_SERIE.TabIndex = 3;
                this.GLB_PSTEXTBOX_SERIE.Caption = gb.NomeDoCampo("GOPER", "CODSERIE");

                this.groupBox5.Controls.Add(this.GLB_PSTEXTBOX_SERIE);
            }
            //string usuarioFat = AppLib.Context.poolConnection

            if (psTextoBox1.Text.Equals(""))
            {
                this.GLB_PSTEXTBOX_SERIE.Enabled = false;
            }
        }

        public override void CarregaParametrosTela()
        {
            base.CarregaParametrosTela();

            bool usatabVAL = false;

            bool usatabADD1 = false;

            bool usatabRATCC = false;
            bool usatabRATDP = false;

            if (this._psPart != null && this._psPart.DefaultFilter.Count > 0)
            {
                #region Tipo de Operação

                PS.Lib.DataField dfCODTIPOPER = gb.RetornaDataFieldByFilter(this._psPart.DefaultFilter, "CODTIPOPER");
                psCodTipOper.Text = (dfCODTIPOPER.Valor == null) ? null : dfCODTIPOPER.Valor.ToString();
                GTIPOPER = gb.RetornaParametrosOperacao((dfCODTIPOPER.Valor == null) ? null : dfCODTIPOPER.Valor.ToString());
                psTextoBox5.Text = Convert.ToString(AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { dfCODTIPOPER.Valor, AppLib.Context.Empresa }));
                #endregion

                #region Número Documento

                if (int.Parse(GTIPOPER["USANUMEROSEQ"].ToString()) == 1)
                {
                    psTextoBox2.Edita = false;
                }
                else
                {
                    psTextoBox2.Edita = true;
                }

                #endregion

                #region Marca
                psTextoBox3.Text = GTIPOPER["DEFAULTMARCA"].ToString();
                psTextoBox4.Text = GTIPOPER["DEFAULTESPECIE"].ToString();
                #endregion

                #region Tipo de Desconto
                if (GTIPOPER["USABLOQUEIODESCONTO"].ToString() == "S")
                {
                    psComboBox4.Value = GTIPOPER["DEFAULTBLOQUEIODESC"].ToString();

                    psComboBox4.Visible = true;
                    psMoedaBox2.Visible = true;
                }
                else
                {
                    psComboBox4.Visible = false;
                    psMoedaBox2.Visible = false;
                }
                #endregion

                #region Série

                //Serie Livre
                GLB_SERIELIVRE = int.Parse(GTIPOPER["SERIELIVRE"].ToString());

                if (GLB_SERIELIVRE == 0)
                {
                    psTextoBox2.Edita = false;

                    CriarControleSerieLookup();

                    //Edita
                    if (GTIPOPER["OPERSERIE"].ToString() == "E")
                    {
                        GLB_PSLOOKUP_SERIE.Visible = true;
                        GLB_PSLOOKUP_SERIE.Chave = true;
                    }

                    //Não Edita
                    if (GTIPOPER["OPERSERIE"].ToString() == "N")
                    {
                        GLB_PSLOOKUP_SERIE.Visible = true;
                        GLB_PSLOOKUP_SERIE.Chave = false;
                    }

                    //Não Mostra
                    if (GTIPOPER["OPERSERIE"].ToString() == "M")
                    {
                        GLB_PSLOOKUP_SERIE.Visible = false;
                        GLB_PSLOOKUP_SERIE.Chave = false;
                    }

                    if (psTextoBox1.Text == "0")
                    {
                        GLB_PSLOOKUP_SERIE.Text = GTIPOPER["SERIEDEFAULT"].ToString();
                        if (GLB_PSLOOKUP_SERIE.Text != string.Empty)
                            GLB_PSLOOKUP_SERIE.LoadLookup();
                    }
                }

                if (GLB_SERIELIVRE == 1)
                {
                    psTextoBox2.Edita = true;

                    CriaControleSerieTextBox();

                    //Edita
                    if (GTIPOPER["OPERSERIE"].ToString() == "E")
                    {
                        GLB_PSTEXTBOX_SERIE.Visible = true;
                        GLB_PSTEXTBOX_SERIE.Edita = true;
                    }

                    //Não Edita
                    if (GTIPOPER["OPERSERIE"].ToString() == "N")
                    {
                        GLB_PSTEXTBOX_SERIE.Visible = true;
                        GLB_PSTEXTBOX_SERIE.Edita = false;
                    }

                    //Não Mostra
                    if (GTIPOPER["OPERSERIE"].ToString() == "M")
                    {
                        GLB_PSTEXTBOX_SERIE.Visible = false;
                        GLB_PSTEXTBOX_SERIE.Edita = false;
                    }
                }

                #endregion

                #region Filial

                // ORIGEM
                PS.Lib.DataField dfCODFILIAL = gb.RetornaDataFieldByFilter(this._psPart.DefaultFilter, "CODFILIAL");
                psLookup3.Text = (dfCODFILIAL.Valor == null) ? null : dfCODFILIAL.Valor.ToString();
                psLookup3.LoadLookup();
                psLookup3.Chave = false;

                // DESTINO
                int CODFILIALENTREGA = int.Parse(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT CODFILIAL2DEFAULT FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new Object[] { AppLib.Context.Empresa, dfCODTIPOPER.Valor.ToString() }).ToString());
                if (CODFILIALENTREGA != 0)
                {
                    psLookup17.Text = CODFILIALENTREGA.ToString();
                    psLookup17.LoadLookup();
                    psLookup17.Chave = false;
                }

                #endregion

                #region Cliente/Fornecedor

                //Edita
                if (GTIPOPER["CODCLIFOR"].ToString() == "E")
                {
                    newLookup1.Enabled = true;
                }
                else
                {
                    newLookup1.Enabled = false;
                }



                GLB_CLIFORMAMB = int.Parse(GTIPOPER["CLIFORAMB"].ToString());
                if (psTextoBox1.Text == "0")
                {
                    newLookup1.txtcodigo.Text = GTIPOPER["CODCLIFORPADRAO"].ToString();

                    if (newLookup1.txtcodigo.Text != string.Empty)
                    {
                        newLookup1.txtconteudo.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NOME FROM VCLIFOR WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { newLookup1.txtcodigo.Text, AppLib.Context.Empresa }).ToString();
                    }
                }


                #endregion

                #region Ordem de Compra
                //Edita
                if (GTIPOPER["USAORDEMDECOMPRA"].ToString() == "E")
                {
                    psTextoBox10.Visible = true;
                }

                //Não Edita
                if (GTIPOPER["USAORDEMDECOMPRA"].ToString() == "N")
                {
                    psTextoBox10.Visible = true;
                    psTextoBox10.Edita = false;
                }

                //Não Mostra
                if (GTIPOPER["USAORDEMDECOMPRA"].ToString() == "M")
                {
                    psTextoBox10.Visible = false;
                }
                #endregion

                #region Objeto

                //Edita
                if (GTIPOPER["USACAMPOOBJETO"].ToString() == "E")
                {
                    psLookup4.Visible = true;
                    psLookup4.Chave = true;

                    usatabADD1 = true;
                }

                //Não Edita
                if (GTIPOPER["USACAMPOOBJETO"].ToString() == "N")
                {
                    psLookup4.Visible = true;
                    psLookup4.Chave = false;

                    usatabADD1 = true;
                }

                //Não Mostra
                if (GTIPOPER["USACAMPOOBJETO"].ToString() == "M")
                {
                    psLookup4.Visible = false;
                    psLookup4.Chave = false;

                    if (!usatabADD1)
                        usatabADD1 = false;
                }

                #endregion

                #region Operador

                //Edita
                if (GTIPOPER["USACAMPOOPERADOR"].ToString() == "E")
                {
                    psLookup11.Visible = true;
                    psLookup11.Chave = true;

                    usatabADD1 = true;
                }

                //Não Edita
                if (GTIPOPER["USACAMPOOPERADOR"].ToString() == "N")
                {
                    psLookup11.Visible = true;
                    psLookup11.Chave = false;

                    usatabADD1 = true;
                }

                //Não Mostra
                if (GTIPOPER["USACAMPOOPERADOR"].ToString() == "M")
                {
                    psLookup11.Visible = false;
                    psLookup11.Chave = false;

                    if (!usatabADD1)
                        usatabADD1 = false;
                }

                if (psTextoBox1.Text == "0")
                {
                    psLookup11.Text = GTIPOPER["CODOPERADORPADRAO"].ToString();
                    if (psLookup11.Text != string.Empty)
                        psLookup11.LoadLookup();
                }

                #endregion

                #region Condição de Pagamento

                //Edita
                if (GTIPOPER["USACAMPOCONDPGTO"].ToString() == "E")
                {
                    psLookup6.Visible = true;
                    psLookup6.Chave = true;

                    usatabADD1 = true;
                }

                //Não Edita
                if (GTIPOPER["USACAMPOCONDPGTO"].ToString() == "N")
                {
                    psLookup6.Visible = true;
                    psLookup6.Chave = false;

                    usatabADD1 = true;
                }

                //Não Mostra
                if (GTIPOPER["USACAMPOCONDPGTO"].ToString() == "M")
                {
                    psLookup6.Visible = false;
                    psLookup6.Chave = false;

                    if (!usatabADD1)
                        usatabADD1 = false;
                }

                if (psTextoBox1.Text == "0")
                {
                    psLookup6.Text = GTIPOPER["CODCONDICAOPADRAO"].ToString();
                    if (psLookup6.Text != string.Empty)
                        psLookup6.LoadLookup();
                }

                #endregion

                #region Forma de Pagamento

                //Edita
                // João Pedro Luchiari - Comentado pois o Lookup de Forma de Pagamento foi trocado
                //if (GTIPOPER["CODFORMA"].ToString() == "E")
                //{
                //    psLookup12.Visible = true;
                //    psLookup12.Chave = true;

                //    usatabADD1 = true;
                //}

                if (GTIPOPER["CODFORMA"].ToString() == "E")
                {
                    lpFormaPagamento.Visible = true;
                    usatabADD1 = true;
                }


                //Não Edita
                // João Pedro Luchiari - Comentado pois o Lookup de Forma de Pagamento foi trocado
                //if (GTIPOPER["CODFORMA"].ToString() == "N")
                //{
                //    psLookup12.Visible = true;
                //    psLookup12.Chave = false;

                //    usatabADD1 = true;
                //}

                if (GTIPOPER["CODFORMA"].ToString() == "N")
                {
                    lpFormaPagamento.Visible = true;
                    usatabADD1 = true;
                }

                //Não Mostra
                // João Pedro Luchiari - Comentado pois o Lookup de Forma de Pagamento foi trocado
                //if (GTIPOPER["CODFORMA"].ToString() == "M")
                //{
                //    psLookup12.Visible = false;
                //    psLookup12.Chave = false;

                //    if (!usatabADD1)
                //        usatabADD1 = false;
                //}

                if (GTIPOPER["CODFORMA"].ToString() == "M")
                {
                    lpFormaPagamento.Visible = false;
                    if (!usatabADD1)
                    {
                        usatabADD1 = false;
                    }
                }

                // João Pedro Luchiari - Comentado pois o Lookup de Forma de Pagamento foi trocado
                //if (psTextoBox1.Text == "0")
                //{
                //    psLookup12.Text = GTIPOPER["CODFORMAPADRAO"].ToString();
                //    if (psLookup12.Text != string.Empty)
                //        psLookup12.LoadLookup();
                //}

                if (psTextoBox1.Text == "0")
                {
                    lpFormaPagamento.txtcodigo.Text = GTIPOPER["CODFORMAPADRAO"].ToString();
                    if (lpFormaPagamento.txtcodigo.Text != string.Empty)
                    {
                        lpFormaPagamento.CarregaDescricao();
                    }
                }

                #endregion

                #region Conta/Caixa

                //Edita
                if (GTIPOPER["USACONTA"].ToString() == "E")
                {
                    psLookup8.Visible = true;
                    psLookup8.Chave = true;

                    usatabADD1 = true;
                }

                //Não Edita
                if (GTIPOPER["USACONTA"].ToString() == "N")
                {
                    psLookup8.Visible = true;
                    psLookup8.Chave = false;

                    usatabADD1 = true;
                }

                //Não Mostra
                if (GTIPOPER["USACONTA"].ToString() == "M")
                {
                    psLookup8.Visible = false;
                    psLookup8.Chave = false;

                    if (!usatabADD1)
                        usatabADD1 = false;
                }

                if (psTextoBox1.Text == "0")
                {
                    psLookup8.Text = GTIPOPER["CODCONTAPADRAO"].ToString();
                    if (psLookup8.Text != string.Empty)
                        psLookup8.LoadLookup();
                }

                #endregion

                #region Representante

                //Edita
                if (GTIPOPER["USACODREPRE"].ToString() == "E")
                {
                    psLookup15.Visible = true;
                    psLookup15.Chave = true;

                    usatabADD1 = true;
                }

                //Não Edita
                if (GTIPOPER["USACODREPRE"].ToString() == "N")
                {
                    psLookup15.Visible = true;
                    psLookup15.Chave = false;

                    usatabADD1 = true;
                }

                //Não Mostra
                if (GTIPOPER["USACODREPRE"].ToString() == "M")
                {
                    psLookup15.Visible = false;
                    psLookup15.Chave = false;

                    if (!usatabADD1)
                        usatabADD1 = false;
                }

                if (psTextoBox1.Text == "0")
                {
                    psLookup15.Text = GTIPOPER["CODREPREPADRAO"].ToString();
                    if (psLookup15.Text != string.Empty)
                        psLookup15.LoadLookup();
                }

                #endregion

                #region Vendedor
                //Edita
                if (GTIPOPER["USACODVENDEDOR"].ToString() == "E")
                {
                    psLookup26.Visible = true;
                    psLookup26.Chave = true;

                    usatabADD1 = true;
                }

                //Não Edita
                if (GTIPOPER["USACODVENDEDOR"].ToString() == "N")
                {
                    psLookup26.Visible = true;
                    psLookup26.Chave = false;

                    usatabADD1 = true;
                }

                //Não Mostra
                if (GTIPOPER["USACODVENDEDOR"].ToString() == "M")
                {
                    psLookup26.Visible = false;
                    psLookup16.Chave = false;

                    if (!usatabADD1)
                        usatabADD1 = false;
                }

                if (psTextoBox1.Text == "0")
                {
                    psLookup26.Text = GTIPOPER["CODVENDEDORPADRAO"].ToString();
                    if (psLookup26.Text != string.Empty)
                        psLookup26.LoadLookup();
                }
                #endregion

                #region Centro de Custo

                //Edita
                if (GTIPOPER["USACODCCUSTO"].ToString() == "E")
                {
                    psLookup13.Visible = true;
                    psLookup13.Chave = true;

                    usatabADD1 = true;
                }

                //Não Edita
                if (GTIPOPER["USACODCCUSTO"].ToString() == "N")
                {
                    psLookup13.Visible = true;
                    psLookup13.Chave = false;

                    usatabADD1 = true;
                }

                //Não Mostra
                if (GTIPOPER["USACODCCUSTO"].ToString() == "M")
                {
                    psLookup13.Visible = false;
                    psLookup13.Chave = false;

                    if (!usatabADD1)
                        usatabADD1 = false;
                }

                #endregion

                #region Natureza Orçamentária

                //Edita
                if (GTIPOPER["USACODNATUREZAORCAMENTO"].ToString() == "E")
                {
                    psLookup16.Visible = true;
                    psLookup16.Chave = true;

                    usatabADD1 = true;
                }

                //Não Edita
                if (GTIPOPER["USACODNATUREZAORCAMENTO"].ToString() == "N")
                {
                    psLookup16.Visible = true;
                    psLookup16.Chave = false;

                    usatabADD1 = true;
                }

                //Não Mostra
                if (GTIPOPER["USACODNATUREZAORCAMENTO"].ToString() == "M")
                {
                    psLookup16.Visible = false;
                    psLookup16.Chave = false;

                    if (!usatabADD1)
                        usatabADD1 = false;
                }

                #endregion

                #region Estoque
                //Edita
                if (GTIPOPER["LOCAL1"].ToString() == "E")
                {
                    psLookup7.Visible = true;
                    psLookup7.Chave = true;
                }
                //Não Edita
                if (GTIPOPER["LOCAL1"].ToString() == "N")
                {
                    psLookup7.Visible = false;
                    psLookup7.Chave = false;
                }

                //Não Mostra
                if (GTIPOPER["LOCAL1"].ToString() == "M")
                {
                    psLookup7.Visible = false;
                    psLookup7.Chave = false;
                }

                #region Filial Entrega
                //Edita
                if (GTIPOPER["OPERESTOQUE2"].ToString() == "E")
                {
                    psLookup17.Visible = true;
                    psLookup17.Chave = true;
                }
                //Não Edita
                if (GTIPOPER["OPERESTOQUE2"].ToString() == "N")
                {
                    psLookup17.Visible = false;
                    psLookup17.Chave = false;
                }

                //Não Mostra
                if (GTIPOPER["OPERESTOQUE2"].ToString() == "M")
                {
                    psLookup17.Visible = false;
                    psLookup17.Chave = false;
                }
                #endregion

                if (psTextoBox1.Text == "0")
                {
                    if (GTIPOPER["CODFILIALDEFUALT"] != DBNull.Value)
                    {
                        if (dfCODFILIAL.Valor.ToString() == GTIPOPER["CODFILIALDEFUALT"].ToString())
                        {
                            psLookup7.Text = GTIPOPER["LOCAL1DEFAULT"].ToString();
                            if (psLookup7.Text != string.Empty)
                                psLookup7.LoadLookup();
                        }
                    }


                    if (GTIPOPER["CODFILIAL2DEFAULT"] != DBNull.Value)
                    {
                        if (CODFILIALENTREGA != 0)
                        {
                            if (CODFILIALENTREGA.ToString().Equals(GTIPOPER["CODFILIAL2DEFAULT"].ToString()))
                            {
                                psLookup9.Text = GTIPOPER["LOCAL2DEFAULT"].ToString();
                                if (psLookup9.Text != string.Empty)
                                    psLookup9.LoadLookup();
                            }
                        }
                    }
                }

                //Edita
                if (GTIPOPER["LOCAL2"].ToString() == "E")
                {
                    psLookup9.Visible = true;
                    psLookup9.Chave = true;
                }

                //Não Edita
                if (GTIPOPER["LOCAL2"].ToString() == "N")
                {
                    psLookup9.Visible = true;
                    psLookup9.Chave = false;
                }

                //Não Mostra
                if (GTIPOPER["LOCAL2"].ToString() == "M")
                {
                    psLookup9.Visible = false;
                    psLookup9.Chave = false;
                }

                #endregion

                #region Transporte

                //Aba Transporte
                if (int.Parse(GTIPOPER["USAABATRANSP"].ToString()) == 0)
                {
                    tabControl1.TabPages.Remove(tabTRANSP);
                }

                #endregion

                #region Valor Bruto

                //Edita
                if (GTIPOPER["USAVALORBRUTO"].ToString() == "E")
                {
                    psValorBruto.Visible = true;
                    psValorBruto.Edita = true;
                }

                //Não Edita
                if (GTIPOPER["USAVALORBRUTO"].ToString() == "N")
                {
                    psValorBruto.Visible = true;
                    psValorBruto.Edita = false;
                }

                //Não Mostra
                if (GTIPOPER["USAVALORBRUTO"].ToString() == "M")
                {
                    psValorBruto.Visible = false;
                    psValorBruto.Edita = false;
                }

                #endregion

                #region Valor Liquido

                //Edita
                if (GTIPOPER["USAVALORLIQUIDO"].ToString() == "E")
                {
                    psValorLiquido.Visible = true;
                    psValorLiquido.Edita = true;
                }

                //Não Edita
                if (GTIPOPER["USAVALORLIQUIDO"].ToString() == "N")
                {
                    psValorLiquido.Visible = true;
                    psValorLiquido.Edita = false;
                }

                //Não Mostra
                if (GTIPOPER["USAVALORLIQUIDO"].ToString() == "M")
                {
                    psValorLiquido.Visible = false;
                    psValorLiquido.Edita = false;
                }

                #endregion

                #region Data Emissão

                //Edita
                if (GTIPOPER["USADATAEMISSAO"].ToString() == "E")
                {
                    dtEmissao.Visible = true;
                }

                //Não Edita
                if (GTIPOPER["USADATAEMISSAO"].ToString() == "N")
                {
                    dtEmissao.Visible = true;
                }

                //Não Mostra
                if (GTIPOPER["USADATAEMISSAO"].ToString() == "M")
                {
                    dtEmissao.Visible = false;
                }

                #endregion

                #region Data Entrega

                //Edita
                if (GTIPOPER["USADATAENTREGA"].ToString() == "E")
                {
                    dtEntrega.Visible = true;
                }

                //Não Edita
                if (GTIPOPER["USADATAENTREGA"].ToString() == "N")
                {
                    dtEntrega.Visible = true;
                }

                //Não Mostra
                if (GTIPOPER["USADATAENTREGA"].ToString() == "M")
                {
                    dtEntrega.Visible = false;
                }

                #endregion

                #region Data Entrada/Saida

                //Edita
                if (GTIPOPER["USADATAENTSAI"].ToString() == "E")
                {
                    dtEntSai.Visible = true;
                    tHoraSai.Visible = true;
                }

                //Não Edita
                if (GTIPOPER["USADATAENTSAI"].ToString() == "N")
                {
                    dtEntSai.Visible = true;
                    tHoraSai.Visible = true;
                }

                //Não Mostra
                if (GTIPOPER["USADATAENTSAI"].ToString() == "M")
                {
                    dtEntSai.Visible = false;
                    tHoraSai.Visible = false;
                }

                #endregion

                #region Rateio Centro de Custo

                if (int.Parse(GTIPOPER["USARATEIOCC"].ToString()) == 0)
                {
                    tabControl2.TabPages.Remove(tabRATCC);
                    usatabRATCC = true;
                }

                #endregion

                #region Rateio Centro de Custo

                if (int.Parse(GTIPOPER["USARATEIODP"].ToString()) == 0)
                {
                    tabControl2.TabPages.Remove(tabRATDP);
                    usatabRATDP = true;
                }

                #endregion

                #region Rateio

                if (usatabRATDP && usatabRATCC)
                {
                    tabControl1.TabPages.Remove(tabRAT);
                }

                #endregion

                #region Bloqueio Desconto

                //Edita
                if (GTIPOPER["USASELECAOBLOQUEIODESCONTO"].ToString() == "E")
                {
                    psComboBox4.Visible = true;
                    psComboBox4.Chave = true;
                }

                //Não Edita
                if (GTIPOPER["USASELECAOBLOQUEIODESCONTO"].ToString() == "N")
                {
                    psComboBox4.Visible = true;
                    psComboBox4.Chave = false;
                }

                //Não Mostra
                if (GTIPOPER["USASELECAOBLOQUEIODESCONTO"].ToString() == "M")
                {
                    psComboBox4.Visible = false;
                    psComboBox4.Chave = false;
                }

                #endregion

                /*
                 *  CAMPOS LIVRES
                 *  DATA EXTRA
                 */

                //ABA VALORES

                #region Frete

                //Edita
                if (GTIPOPER["USAVALORFRETE"].ToString() == "E")
                {
                    psValorFrete.Visible = true;
                    psValorFrete.Edita = true;
                    psPercFrete.Visible = true;
                    psPercFrete.Edita = true;

                    usatabVAL = true;
                }

                //Não Edita
                if (GTIPOPER["USAVALORFRETE"].ToString() == "N")
                {
                    psValorFrete.Visible = true;
                    psValorFrete.Edita = false;
                    psPercFrete.Visible = true;
                    psPercFrete.Edita = false;

                    usatabVAL = true;
                }

                //Não Mostra
                if (GTIPOPER["USAVALORFRETE"].ToString() == "M")
                {
                    psValorFrete.Visible = false;
                    psValorFrete.Edita = false;
                    psPercFrete.Visible = false;
                    psPercFrete.Edita = false;

                    if (!usatabVAL)
                        usatabVAL = false;
                }

                #endregion

                #region Desconto

                //Edita
                if (GTIPOPER["USAVALORDESCONTO"].ToString() == "E")
                {
                    psValorDesconto.Visible = true;
                    psValorDesconto.Edita = true;
                    psPercDesconto.Visible = true;
                    psPercDesconto.Edita = true;

                    usatabVAL = true;
                }

                //Não Edita
                if (GTIPOPER["USAVALORDESCONTO"].ToString() == "N")
                {
                    psValorDesconto.Visible = true;
                    psValorDesconto.Edita = false;
                    psPercDesconto.Visible = true;
                    psPercDesconto.Edita = false;

                    usatabVAL = true;
                }

                //Não Mostra
                if (GTIPOPER["USAVALORDESCONTO"].ToString() == "M")
                {
                    psValorDesconto.Visible = false;
                    psValorDesconto.Edita = false;
                    psPercDesconto.Visible = false;
                    psPercDesconto.Edita = false;

                    if (!usatabVAL)
                        usatabVAL = false;
                }

                #endregion

                #region Despesa

                //Edita
                if (GTIPOPER["USAVALORDESPESA"].ToString() == "E")
                {
                    psValorDespesa.Visible = true;
                    psValorDespesa.Edita = true;
                    psPercDespesa.Visible = true;
                    psPercDespesa.Edita = true;

                    usatabVAL = true;
                }

                //Não Edita
                if (GTIPOPER["USAVALORDESPESA"].ToString() == "N")
                {
                    psValorDespesa.Visible = true;
                    psValorDespesa.Edita = false;
                    psPercDespesa.Visible = true;
                    psPercDespesa.Edita = false;

                    usatabVAL = true;
                }

                //Não Mostra
                if (GTIPOPER["USAVALORDESPESA"].ToString() == "M")
                {
                    psValorDespesa.Visible = false;
                    psValorDespesa.Edita = false;
                    psPercDespesa.Visible = false;
                    psPercDespesa.Edita = false;

                    if (!usatabVAL)
                        usatabVAL = false;
                }

                #endregion

                #region Seguro

                //Edita
                if (GTIPOPER["USAVALORSEGURO"].ToString() == "E")
                {
                    psValorSeguro.Visible = true;
                    psValorSeguro.Edita = true;
                    psPercSeguro.Visible = true;
                    psPercSeguro.Edita = true;

                    usatabVAL = true;
                }

                //Não Edita
                if (GTIPOPER["USAVALORSEGURO"].ToString() == "N")
                {
                    psValorSeguro.Visible = true;
                    psValorSeguro.Edita = false;
                    psPercSeguro.Visible = true;
                    psPercSeguro.Edita = false;

                    usatabVAL = true;
                }

                //Não Mostra
                if (GTIPOPER["USAVALORSEGURO"].ToString() == "M")
                {
                    psValorSeguro.Visible = false;
                    psValorSeguro.Edita = false;
                    psPercSeguro.Visible = false;
                    psPercSeguro.Edita = false;

                    if (!usatabVAL)
                        usatabVAL = false;
                }

                #endregion


                /*
                 * HABILITA/DESABILITA PÁGINAS
                 * OBS: SEMPRE POR ULTIMO
                 */

                #region Aba Tabelas

                //Aba Tabelas
                if (!usatabADD1)
                {
                    tabControl1.TabPages.Remove(tabADD1);
                }

                #endregion

                #region Aba Valores

                //Aba Valores
                if (!usatabVAL)
                {
                    tabControl1.TabPages.Remove(tabVAL);
                }

                #endregion

                #region Aba Observação

                //Aba Observação
                if (int.Parse(GTIPOPER["USAABAOBSERV"].ToString()) == 0)
                {
                    tabControl1.TabPages.Remove(tabOBSERV);
                }

                #endregion
            }
        }

        public override void NovoRegistro()
        {
            try
            {
                base.NovoRegistro();

                psCodUsuarioCriacao.Text = string.Empty;
                psCodTipOper.Edita = false;
                //psLookup3.Chave = false;
                psTextoBox1.Text = "0";
                psTextoBox1.Edita = false;
                psDateBox1.Chave = false;
                psDateBox1.Text = string.Empty;
                dtEmissao.Text = DateTime.Now.ToString();
                psComboBox1.Chave = false;
                //psMoedaBox2.Chave = false;
                //psMoedaBox3.Chave = false;
                CarregaParametrosTela();
                habilitaCampos();
                VerificaAcesso();

            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }
        }

        private void VerificaAcesso()
        {

            if (edita == true)
            {
                if (faturamento == true)
                {
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO FROM GPERMISSAOMENU WHERE CODPERFIL = ? AND CODMENU = ?", new object[] { AppLib.Context.Perfil, codMenu });
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["EDICAO"]) == false)
                        {
                            btnSalvarAtual.Enabled = false;
                            btnOKAtual.Enabled = false;
                            btnNovo.Enabled = false;
                            btnEditar.Enabled = false;
                            editaItem = false;
                            btnExcluir.Enabled = false;
                        }
                    }
                }
                else
                {
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO FROM GPERMISSAOMENU WHERE CODPERFIL = ? AND CODMENU = ?", new object[] { AppLib.Context.Perfil, codMenu });
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["EDICAO"]) == false)
                        {
                            btnSalvarAtual.Enabled = false;
                            btnOKAtual.Enabled = false;
                            btnNovo.Enabled = false;
                            btnEditar.Enabled = false;
                            editaItem = false;
                            btnExcluir.Enabled = false;
                        }
                    }
                }

            }


            //            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT INCLUIR, EXCLUIR, ALTERAR, FATURAR, INCLUIRFAT, CONSULTAR FROM GPERFILTIPOPER 
            //INNER JOIN GPERFIL ON GPERFILTIPOPER.CODPERFIL = GPERFIL.CODPERFIL   
            //INNER JOIN GUSUARIOPERFIL ON GPERFILTIPOPER .CODPERFIL = GUSUARIOPERFIL.CODPERFIL
            //WHERE
            //GUSUARIOPERFIL.CODUSUARIO = ?
            //AND GPERFILTIPOPER.CODTIPOPER = ?
            //AND GUSUARIOPERFIL.CODEMPRESA = ?", new object[] { AppLib.Context.Usuario, psCodTipOper.textBox1.Text, AppLib.Context.Empresa });

            //            if (dt.Rows.Count > 0)
            //            {
            //                incluir = Convert.ToBoolean(dt.Rows[0]["INCLUIR"]);
            //                excluir = Convert.ToBoolean(dt.Rows[0]["EXCLUIR"]);
            //                alterar = Convert.ToBoolean(dt.Rows[0]["ALTERAR"]);
            //                faturar = Convert.ToBoolean(dt.Rows[0]["FATURAR"]);
            //                incluirFat = Convert.ToBoolean(dt.Rows[0]["INCLUIRFAT"]);
            //                consultar = Convert.ToBoolean(dt.Rows[0]["CONSULTAR"]);

            //                if (excluir == false)
            //                {
            //                    PermiteExcluir = false;
            //                }
            //                if (incluir == false)
            //                {
            //                    PermiteInserir = false;
            //                }
            //                if (alterar == false)
            //                {
            //                    PermiteEditar = false;
            //                }
            //            }

        }

        private void habilitaCampos()
        {
            //Verifica se o movimento é do tipo de saída.
            string tipoEntSai = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT TIPENTSAI FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { psCodTipOper.Text, AppLib.Context.Empresa }).ToString();
            if (tipoEntSai == "S")
            {
                chkTipOperConsFin.Visible = true;
                // João Pedro Luchiari
                cmbOperacaoPresencial.Visible = true;
                cmbNfe.Visible = false;
                labelControl5.Visible = false;
                psTextoBox8.Visible = false;
            }
            else
            {
                chkTipOperConsFin.Visible = false;
                // João Pedro Luchiari
                cmbOperacaoPresencial.Visible = false;
                cmbNfe.Visible = true;
                labelControl5.Visible = true;
                psTextoBox8.Visible = true;
            }
            string UsaComposto = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT UTILIZAPRODCOPMOSTO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, psCodTipOper.Text }).ToString();
            if (UsaComposto != "S")
            {
                btnExpandirComposicao.Enabled = false;
            }
        }

        private void carregaGridTributos()
        {
            dataGridView1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT
 GOPERITEMTRIBUTO.CODTRIBUTO AS 'Tríbuto'
,ISNULL((SELECT SUM(BASECALCULO) 
   FROM GOPERITEMTRIBUTO T 
   WHERE T.CODEMPRESA = GOPERITEMTRIBUTO.CODEMPRESA 
   AND T.CODOPER = GOPERITEMTRIBUTO.CODOPER
   AND T.CODTRIBUTO = GOPERITEMTRIBUTO.CODTRIBUTO 
   AND T.BASECALCULO > 0 
   GROUP BY T.CODOPER, T.CODTRIBUTO, T.CODEMPRESA ),0) AS 'Base de Cálculo'
,ISNULL((SELECT SUM(VALOR) 
   FROM GOPERITEMTRIBUTO T 
   WHERE T.CODEMPRESA = GOPERITEMTRIBUTO.CODEMPRESA 
   AND T.CODOPER = GOPERITEMTRIBUTO.CODOPER
   AND T.CODTRIBUTO = GOPERITEMTRIBUTO.CODTRIBUTO 
   AND T.VALOR > 0 
   GROUP BY T.CODOPER, T.CODTRIBUTO, T.CODEMPRESA ),0)  AS 'Valor'
,ISNULL((SELECT SUM(VALORICMSST) 
   FROM GOPERITEMTRIBUTO T 
   WHERE T.CODEMPRESA = GOPERITEMTRIBUTO.CODEMPRESA 
   AND T.CODOPER = GOPERITEMTRIBUTO.CODOPER
   AND T.CODTRIBUTO = GOPERITEMTRIBUTO.CODTRIBUTO 
   AND T.VALORICMSST > 0 
   GROUP BY T.CODOPER, T.CODTRIBUTO, T.CODEMPRESA ),0) AS 'Valor ICMS-ST'

FROM 
GOPERITEMTRIBUTO
WHERE 
GOPERITEMTRIBUTO.CODEMPRESA = ?
AND GOPERITEMTRIBUTO.CODOPER = ?
GROUP BY GOPERITEMTRIBUTO.CODOPER, GOPERITEMTRIBUTO.CODTRIBUTO, GOPERITEMTRIBUTO.CODEMPRESA", new object[] { AppLib.Context.Empresa, codoper });

            dataGridView1.Columns["Valor"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Valor"].DefaultCellStyle.Format = "c";
            dataGridView1.Columns["Valor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns["Base de Cálculo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Base de Cálculo"].DefaultCellStyle.Format = "c";
            dataGridView1.Columns["Base de Cálculo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns["Valor ICMS-ST"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Valor ICMS-ST"].DefaultCellStyle.Format = "c";
            dataGridView1.Columns["Valor ICMS-ST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView2.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
SUM(VBCUFDEST)AS 'Base de Cálculo', 
(SUM(VICMSUFDEST) + SUM(VICMSUFREMET)) Difal, 
SUM(VICMSUFDEST) AS 'ICMS Destino', 
SUM(VICMSUFREMET) AS 'ICMS Origem', 
SUM(VFCPUFDEST) AS 'Valor FCP',  
SUM(VFCPSTUFDEST) AS 'Valor FCP ST'
FROM 
GOPERITEMDIFAL 
WHERE 
CODEMPRESA = ? 
AND CODOPER = ? 
GROUP BY CODOPER, CODEMPRESA", new object[] { AppLib.Context.Empresa, codoper });
            //Alinha o cabeçalho das colunas a direita
            dataGridView2.Columns["Base de Cálculo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["Difal"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["ICMS Destino"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["ICMS Origem"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["Valor FCP"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["Valor FCP ST"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //Alinha a coluna
            dataGridView2.Columns["Base de Cálculo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["Difal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["ICMS Destino"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["ICMS Origem"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["Valor FCP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["Valor FCP ST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //Define o formato das colunas
            dataGridView2.Columns["Base de Cálculo"].DefaultCellStyle.Format = "c";
            dataGridView2.Columns["Difal"].DefaultCellStyle.Format = "c";
            dataGridView2.Columns["ICMS Destino"].DefaultCellStyle.Format = "c";
            dataGridView2.Columns["ICMS Origem"].DefaultCellStyle.Format = "c";
            dataGridView2.Columns["Valor FCP"].DefaultCellStyle.Format = "c";
            dataGridView2.Columns["Valor FCP ST"].DefaultCellStyle.Format = "c";
        }

        //quando clica 2 vezes pra editar
        public override void CarregaRegistro()
        {
            try
            {
                this.CarregaParametrosTela();
                base.CarregaRegistro();
                habilitaCampos();
                if (this._psPart != null && this._psPart.DefaultFilter.Count > 0)
                {
                    PS.Lib.DataField dfCODTIPOPER = gb.RetornaDataFieldByFilter(this._psPart.DefaultFilter, "CODTIPOPER");
                    psCodTipOper.Text = (dfCODTIPOPER.Valor == null) ? null : dfCODTIPOPER.Valor.ToString();
                    psCodTipOper.Edita = false;

                    // ORIGEM
                    PS.Lib.DataField dfCODFILIAL = gb.RetornaDataFieldByFilter(this._psPart.DefaultFilter, "CODFILIAL");
                    psLookup3.Text = (dfCODFILIAL.Valor == null) ? null : dfCODFILIAL.Valor.ToString();
                    psLookup3.LoadLookup();
                    psLookup3.Chave = false;

                    // DESTINO
                    int CODFILIALENTREGA = int.Parse(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT CODFILIAL2DEFAULT FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new Object[] { AppLib.Context.Empresa, dfCODTIPOPER.Valor.ToString() }).ToString());
                    if (CODFILIALENTREGA != 0)
                    {
                        psLookup17.Text = CODFILIALENTREGA.ToString();
                        psLookup17.LoadLookup();
                        psLookup17.Chave = false;
                    }

                    psDateBox1.Chave = false;
                    psComboBox1.Chave = false;

                    carregaGridTributos();

                    if (GTIPOPER != null)
                    {
                        if (int.Parse(GTIPOPER["USANUMEROSEQ"].ToString()) == 0)
                        {
                            PSPartOperacaoData pspartOperacaoData = new PSPartOperacaoData();
                            pspartOperacaoData._tablename = "GOPER";
                            pspartOperacaoData._keys = new string[] { "CODEMPRESA", "CODOPER" };

                            if (pspartOperacaoData.PossuiRelacionamentoPai(PS.Lib.Contexto.Session.Empresa.CodEmpresa, Convert.ToInt32(psTextoBox1.Text), false))
                            {
                                // psTextoBox2.Edita = false;

                                try
                                {
                                    GLB_PSLOOKUP_SERIE.Chave = false;
                                }
                                catch { }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }
            //codoper = psTextoBox1.Text;
        }

        public override void SalvaRegistro()
        {

            psMemoBox1.Text = psMemoBox1.Text.Replace("\r\n", " ");
            psMemoBox2.Text = psMemoBox2.Text.Replace("\r\n", " ");

            base.SalvaRegistro();

            AtualizaValores();

        }

        private void AtualizaValores()
        {
            string sSql = "";

            sSql = @"SELECT VALORBRUTO, VALORLIQUIDO FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";
            DataTable dt = dbs.QuerySelect(sSql, PS.Lib.Contexto.Session.Empresa.CodEmpresa, psTextoBox1.Text);

            if (dt.Rows.Count > 0)
            {
                psValorBruto.Text = dt.Rows[0]["VALORBRUTO"].ToString();
                psValorLiquido.Text = dt.Rows[0]["VALORLIQUIDO"].ToString();
            }
            carregaGridTributos();
        }

        private void GetDefaultCliFor()
        {
            PSPartCliForData psPartCliForData = new PSPartCliForData();
            psPartCliForData._tablename = "VCLIFOR";
            psPartCliForData._keys = new string[] { "CODEMPRESA", "CODCLIFOR" };

            DataTable dados = psPartCliForData.ReadRecordEdit(PS.Lib.Contexto.Session.Empresa.CodEmpresa, newLookup1.txtcodigo.Text);
            if (dados.Rows.Count > 0)
            {
                foreach (DataRow row in dados.Rows)
                {
                    if (Convert.ToInt32(row["FISICOJURIDICO"]) == 0)
                    {
                        psMaskedTextBox1.Mask = "00,000,000/0000-00";
                        psMaskedTextBox1.Text = row["CGCCPF"].ToString();
                    }

                    if (Convert.ToInt32(row["FISICOJURIDICO"]) == 1)
                    {
                        psMaskedTextBox1.Mask = "000,000,000-00";
                        psMaskedTextBox1.Text = row["CGCCPF"].ToString();
                    }
                }

                // JEPETO

                string tipoEntraSai = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT TIPENTSAI FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { psCodTipOper.textBox1.Text, AppLib.Context.Empresa }).ToString();
                psTextoBox9.Visible = true;
                if (tipoEntraSai == "E")
                {
                    psTextoBox9.Text = dados.Rows[0]["DESCMAXCOMPRA"].ToString();
                    psTextoBox9.Caption = "Desconto Máximo de Compra";
                }
                else
                {
                    psTextoBox9.Text = dados.Rows[0]["DESCMAXVENDA"].ToString();
                    psTextoBox9.Caption = "Desconto Máximo de Venda";
                }
                if (psComboBox1.Text == "Aberto")
                {
                    psMoedaBox2.textBox1.Text = psTextoBox9.Text;
                }

                tbEstadoClifor.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR WHERE CODEMPRESA = ? AND CODCLIFOR = ?", new object[] { AppLib.Context.Empresa, newLookup1.txtcodigo.Text }).ToString();

                //FÁBIO CAMPOS

                if (!string.IsNullOrEmpty(dados.Rows[0]["CODREPRE"].ToString()))
                {
                    psLookup15.Text = dados.Rows[0]["CODREPRE"].ToString();
                    psLookup15.Description = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NOMEFANTASIA FROM VREPRE WHERE CODREPRE = ? AND CODEMPRESA = ?", new object[] { dados.Rows[0]["CODREPRE"].ToString(), AppLib.Context.Empresa }).ToString();
                }
                else
                {
                    psLookup15.Text = string.Empty;
                    psLookup15.Description = string.Empty;
                }
                if (!string.IsNullOrEmpty(dados.Rows[0]["CODVENDEDOR"].ToString()))
                {
                    psLookup26.textBox1.Text = dados.Rows[0]["CODVENDEDOR"].ToString();
                    psLookup26.Description = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NOMEFANTASIA FROM VVENDEDOR WHERE CODVENDEDOR = ? AND CODEMPRESA = ?", new object[] { dados.Rows[0]["CODVENDEDOR"].ToString(), AppLib.Context.Empresa }).ToString();
                }
                else
                {
                    psLookup26.Text = string.Empty;
                    psLookup26.Description = string.Empty;

                }
                // João Pedro Luchiari - Comentado pois o Lookup de Forma de Pagamento foi trocado
                //if (!string.IsNullOrEmpty(dados.Rows[0]["CODFORMA"].ToString()))
                //{
                //    psLookup12.textBox1.Text = dados.Rows[0]["CODFORMA"].ToString();
                //    psLookup12.Description = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NOME FROM VFORMAPGTO WHERE CODFORMA = ? AND CODEMPRESA = ?", new object[] { dados.Rows[0]["CODFORMA"].ToString(), AppLib.Context.Empresa }).ToString();
                //}
                //else
                //{
                //    psLookup12.Text = string.Empty;
                //    psLookup12.Description = string.Empty;
                //}

                if (!string.IsNullOrEmpty(dados.Rows[0]["CODFORMA"].ToString()))
                {
                    lpFormaPagamento.txtcodigo.Text = dados.Rows[0]["CODFORMA"].ToString();
                    lpFormaPagamento.CarregaDescricao();
                }
                else
                {
                    lpFormaPagamento.txtcodigo.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(dados.Rows[0]["CODCCUSTO"].ToString()))
                {
                    psLookup13.Text = dados.Rows[0]["CODCCUSTO"].ToString();
                    psLookup13.Description = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NOME FROM GCENTROCUSTO WHERE CODCCUSTO = ? AND CODEMPRESA = ?", new object[] { dados.Rows[0]["CODCCUSTO"].ToString(), AppLib.Context.Empresa }).ToString();
                }
                else
                {
                    psLookup13.Text = string.Empty;
                    psLookup13.Description = string.Empty;
                }
                if (!string.IsNullOrEmpty(dados.Rows[0]["CODNATUREZAORCAMENTO"].ToString()))
                {
                    psLookup16.Text = dados.Rows[0]["CODNATUREZAORCAMENTO"].ToString();
                    psLookup16.Description = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM VNATUREZAORCAMENTO WHERE CODNATUREZA = ? AND CODEMPRESA = ?", new object[] { dados.Rows[0]["CODNATUREZAORCAMENTO"].ToString(), AppLib.Context.Empresa }).ToString();
                }
                else
                {
                    psLookup16.Text = string.Empty;
                    psLookup16.Description = string.Empty;
                }
                if (!string.IsNullOrEmpty(dados.Rows[0]["CODCONTA"].ToString()))
                {
                    psLookup8.Text = dados.Rows[0]["CODCONTA"].ToString();
                    psLookup8.Description = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM FCONTA WHERE CODCONTA = ? AND CODEMPRESA = ?", new object[] { dados.Rows[0]["CODCONTA"].ToString(), AppLib.Context.Empresa }).ToString();
                }
                else
                {
                    psLookup8.Text = string.Empty;
                    psLookup8.Description = string.Empty;
                }
                if (!string.IsNullOrEmpty(dados.Rows[0]["CODTRANSPORTADORA"].ToString()))
                {
                    psLookup5.Text = dados.Rows[0]["CODTRANSPORTADORA"].ToString();
                    psLookup5.Description = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NOMEFANTASIA FROM VTRANSPORTADORA WHERE CODTRANSPORTADORA = ? AND CODEMPRESA = ?", new object[] { dados.Rows[0]["CODTRANSPORTADORA"].ToString(), AppLib.Context.Empresa }).ToString();
                }
                else
                {
                    psLookup5.Text = string.Empty;
                    psLookup5.Description = string.Empty;
                }
                if (!string.IsNullOrEmpty(dados.Rows[0]["FRETECIFFOB"].ToString()))
                {
                    psComboBox2.SelectedIndex = Convert.ToInt32(dados.Rows[0]["FRETECIFFOB"].ToString());
                }
                else
                {
                    psComboBox2.SelectedIndex = 0;
                }
                //
                string tipo = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT TIPENTSAI FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { psCodTipOper.Text, AppLib.Context.Empresa }).ToString();
                if (!string.IsNullOrEmpty(tipo))
                {
                    if (tipo.Equals("S"))
                    {
                        if (!string.IsNullOrEmpty(dados.Rows[0]["CODCONDICAOVENDA"].ToString()))
                        {
                            psLookup6.Text = dados.Rows[0]["CODCONDICAOVENDA"].ToString();
                            psLookup6.Description = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NOME FROM VCONDICAOPGTO where CODCONDICAO = ? AND CODEMPRESA = ?", new object[] { dados.Rows[0]["CODCONDICAOVENDA"].ToString(), AppLib.Context.Empresa }).ToString();
                        }
                        else
                        {
                            psLookup6.Text = string.Empty;
                            psLookup6.Description = string.Empty;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(dados.Rows[0]["CODCONDICAOCOMPRA"].ToString()))
                        {
                            psLookup6.Text = dados.Rows[0]["CODCONDICAOCOMPRA"].ToString();
                            psLookup6.Description = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NOME FROM VCONDICAOPGTO where CODCONDICAO = ? AND CODEMPRESA = ?", new object[] { dados.Rows[0]["CODCONDICAOCOMPRA"].ToString(), AppLib.Context.Empresa }).ToString();
                        }
                        else
                        {
                            psLookup6.Text = string.Empty;
                            psLookup6.Description = string.Empty;
                        }
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabControl1.SelectedIndex == 0)
            //{
            AtualizaValores();
            //}
            if (tabControl1.SelectedIndex == 3)
            {
                carregaGridTributos();
            }
        }

        private void psBaseVisao1_Paint(object sender, PaintEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                AtualizaValores();
            }
        }

        private void psLookup4_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODCLIFOR", newLookup1.txtcodigo.Text));
        }

        private void psLookup7_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODFILIAL", psLookup3.Text));
        }

        private void psLookup9_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODFILIAL", psLookup17.Text));
        }

        private void psLookup8_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODTIPOPER", psCodTipOper.Text));
        }

        private void psLookup10_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODTIPOPER", psCodTipOper.Text));
        }

        private void psLookup3_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
        }

        private void psLookup13_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
        }

        private void psLookup2_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            if (GLB_CLIFORMAMB == 0 || GLB_CLIFORMAMB == 1)
            {
                //e.Filtro.Add(new PS.Lib.PSFilter("CODCLASSIFICACAO", GLB_CLIFORMAMB));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(psTextoBox1.Text) > 0)
                {
                    psLookup10.Text = string.Empty;

                    psLookup10.OperSearchForm();

                    if (psLookup10.Text != string.Empty)
                    {
                        psLookup10.LoadLookup();

                        List<String> lMensagem = new List<string>();

                        if (psMemoBox1.Text != string.Empty)
                        {
                            lMensagem.Add(psMemoBox1.Text);
                        }

                        string sSql = string.Empty;

                        sSql = "SELECT CODFORMULAMENSAGEM FROM VOPERMENSAGEM WHERE CODEMPRESA = ? AND CODMENSAGEM = ?";

                        string sFormula = dbs.QueryValue(string.Empty, sSql, PS.Lib.Contexto.Session.Empresa.CodEmpresa, psLookup10.Text).ToString();

                        if (sFormula != string.Empty)
                        {
                            PS.Lib.Contexto.Session.key1 = PS.Lib.Contexto.Session.Empresa.CodEmpresa;
                            PS.Lib.Contexto.Session.key2 = psTextoBox1.Text;

                            interpreta.comando = function.PreparaFormula(PS.Lib.Contexto.Session.Empresa.CodEmpresa, sFormula);
                            string sMensagem = interpreta.Executar().ToString();

                            if (sMensagem != string.Empty)
                            {
                                lMensagem.Add(sMensagem);
                            }
                        }
                        else
                        {
                            sSql = "SELECT MENSAGEM FROM VOPERMENSAGEM WHERE CODEMPRESA = ? AND CODMENSAGEM = ?";

                            string sMensagem = dbs.QueryValue(string.Empty, sSql, PS.Lib.Contexto.Session.Empresa.CodEmpresa, psLookup10.Text).ToString();

                            if (sMensagem != string.Empty)
                            {
                                lMensagem.Add(sMensagem);
                            }
                        }

                        if (lMensagem.Count > 0)
                        {
                            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();

                            for (int i = 0; i < lMensagem.Count; i++)
                            {
                                txt.AppendText(string.Concat(lMensagem[i], Environment.NewLine));
                            }

                            psMemoBox1.Text = txt.Text;

                            txt.Dispose();
                        }

                        PS.Lib.Contexto.Session.key1 = null;
                        PS.Lib.Contexto.Session.key2 = null;
                    }
                }
                else
                {
                    throw new Exception("Salve o registro antes de executar um aplicativo.");
                }
            }
            catch (Exception ex)
            {

                PS.Lib.Contexto.Session.key1 = null;
                PS.Lib.Contexto.Session.key2 = null;

                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }
        }

        private void GLB_PSLOOKUP_SERIE_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODTIPOPER", psCodTipOper.Text));
        }

        private void psPercFrete_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(psPercFrete.Text) != 0)
            {
                psValorFrete.Text = CalculaValor(Convert.ToDecimal(psValorBruto.Text), Convert.ToDecimal(psPercFrete.Text)).ToString();
            }
        }

        private void psValorFrete_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(psValorFrete.Text) != 0)
            {
                psPercFrete.Text = CalculaPercentual(Convert.ToDecimal(psValorBruto.Text), Convert.ToDecimal(psValorFrete.Text)).ToString();
            }
        }

        private void psPercDesconto_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(psPercDesconto.Text) != 0)
            {
                psValorDesconto.Text = CalculaValor(Convert.ToDecimal(psValorBruto.Text), Convert.ToDecimal(psPercDesconto.Text)).ToString();
            }
        }

        private void psValorDesconto_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(psValorDesconto.Text) != 0)
            {
                psPercDesconto.Text = CalculaPercentual(Convert.ToDecimal(psValorBruto.Text), Convert.ToDecimal(psValorDesconto.Text)).ToString();
            }
        }

        private void psPercDespesa_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(psPercDespesa.Text) != 0)
            {
                psValorDespesa.Text = CalculaValor(Convert.ToDecimal(psValorBruto.Text), Convert.ToDecimal(psPercDespesa.Text)).ToString();
            }
        }

        private void psValorDespesa_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(psValorDespesa.Text) != 0)
            {
                psPercDespesa.Text = CalculaPercentual(Convert.ToDecimal(psValorBruto.Text), Convert.ToDecimal(psValorDespesa.Text)).ToString();
            }
        }

        private void psPercSeguro_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(psPercSeguro.Text) != 0)
            {
                psValorSeguro.Text = CalculaValor(Convert.ToDecimal(psValorBruto.Text), Convert.ToDecimal(psPercSeguro.Text)).ToString();
            }
        }

        private void psValorSeguro_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(psValorSeguro.Text) != 0)
            {
                psPercSeguro.Text = CalculaPercentual(Convert.ToDecimal(psValorBruto.Text), Convert.ToDecimal(psValorSeguro.Text)).ToString();
            }
        }

        private void PSPartOperacaoEdit_Load(object sender, EventArgs e)
        {
            //Remove a aba de acordo com o uso do GERAFINANCEIRO

            string CodTipOper = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codoper }).ToString();
            bool GeraFinancero = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT GERAFINANCEIRO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, CodTipOper }));

            //Remove a aba de acordo com o USABAIXADEPRODUCAO

            bool UsabaixaProducao = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT USABAIXADEPRODUCAO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, CodTipOper }).ToString());

            if (GeraFinancero == false)
            {
                tabControl1.TabPages.Remove(tabPage3);
            }

            else
            {
                // Lançamentos Financeiros
                CarregaLancamentoFinanceiro();
                btnPesquisarLancamento.Enabled = true;
                btnAgruparLancamento.Enabled = true;
                btnVisaoLancamentos.Enabled = true;
                ///
            }

            if (UsabaixaProducao == false)
            {
                tabControl1.TabPages.Remove(tabPage4);
            }
            else
            {
                //Rastreamento de Operação
                CarregaRastreamentoOP();
                //
            }

            // Desativa o botão cCancela

            if (VerificaCancela == false)
            {
                btnCancelarAtual.Enabled = false;
            }

            //bool GeraFinancero = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT GERAFINANCEIRO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] {AppLib.Context.Empresa, codtipoper }));

            //if (GeraFinancero != true)
            //{
            //    tabControl1.TabPages.Remove(tabPage3);
            //}

            splitContainer1.SplitterDistance = 30;

            //Atribui 0 aos campos da tabela valores
            psMoedaBox1.textBox1.Text = "0,00";
            psMoedaBox2.textBox1.Text = "0,00";
            psMoedaBox4.textBox1.Text = "0";
            psMoedaBox5.textBox1.Text = "0,00";
            psMoedaBox6.textBox1.Text = "0,00";

            psPercDesconto.textBox1.Text = "0,00";
            psValorDesconto.textBox1.Text = "0,00";
            psPercDespesa.textBox1.Text = "0,00";
            psValorDespesa.textBox1.Text = "0,00";
            psPercFrete.textBox1.Text = "0,00";
            psValorFrete.textBox1.Text = "0,00";
            psPercSeguro.textBox1.Text = "0,00";
            psValorSeguro.textBox1.Text = "0,00";
            dtEntSai.EditValue = AppLib.Context.poolConnection.Get("Start").GetDateTime();
            tHoraSai.EditValue = AppLib.Context.poolConnection.Get("Start").GetDateTime().TimeOfDay;

            if (edita == true)
            {

                CarregaCampos();
                //Carrega o campo CODSERIE

                psComboBox5.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VSERIE INNER JOIN VTIPOPERSERIE ON VSERIE.CODSERIE = VTIPOPERSERIE.CODSERIE AND VSERIE.CODEMPRESA = VTIPOPERSERIE.CODEMPRESA AND VSERIE.CODFILIAL = VTIPOPERSERIE.CODFILIAL WHERE VTIPOPERSERIE.CODTIPOPER = ? AND VSERIE.CODEMPRESA = ? AND VSERIE.CODFILIAL = ? AND VTIPOPERSERIE.PRINCIPAL = 1", new object[] { psCodTipOper.textBox1.Text, AppLib.Context.Empresa, codFilial });
                psComboBox5.DisplayMember = "DESCRICAO";
                psComboBox5.ValueMember = "CODSERIE";
                carregaParametros();
                if (psComboBox1.Value.ToString() != "0" && psComboBox1.Value.ToString() != "5" && psComboBox1.Value.ToString() != "10")
                {
                    btnNovo.Enabled = false;
                    btnExcluir.Enabled = false;
                    //btnEditar.Enabled = false;
                    btnSalvarAtual.Enabled = false;
                    btnOKAtual.Enabled = false;
                }

                string StatusNFE = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODSTATUS FROM GNFESTADUAL WHERE CODOPER	= ? AND CODEMPRESA = ?", new object[] { psTextoBox1.textBox1.Text, AppLib.Context.Empresa }).ToString();
                if (!string.IsNullOrEmpty(StatusNFE))
                {
                    if (StatusNFE != "I" && StatusNFE != "E")
                    {
                        btnNovo.Enabled = false;
                        btnExcluir.Enabled = false;
                        //btnEditar.Enabled = false;
                        btnSalvarAtual.Enabled = false;
                        btnOKAtual.Enabled = false;
                    }
                }
                //Fábio Campos 22/09/2017 15:17
                if (validaUsaAprovacao(usaAprovacao) == false)
                {
                    MessageBox.Show("Operação aprovada.\nEdição não permitida.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnNovo.Enabled = false;
                    btnExcluir.Enabled = false;
                    //btnEditar.Enabled = false;
                    btnSalvarAtual.Enabled = false;
                    btnOKAtual.Enabled = false;
                    aprovado = true;
                }

                carregaGridChaveRef();
            }
            else
            {
                //Carrega o campo CODSERIE
                psComboBox5.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VSERIE INNER JOIN VTIPOPERSERIE ON VSERIE.CODSERIE = VTIPOPERSERIE.CODSERIE AND VSERIE.CODEMPRESA = VTIPOPERSERIE.CODEMPRESA AND VSERIE.CODFILIAL = VTIPOPERSERIE.CODFILIAL WHERE VTIPOPERSERIE.CODTIPOPER = ? AND VSERIE.CODEMPRESA = ? AND VSERIE.CODFILIAL = ? AND VTIPOPERSERIE.PRINCIPAL = 1", new object[] { codtipoper, AppLib.Context.Empresa, codFilial });
                psComboBox5.DisplayMember = "DESCRICAO";
                psComboBox5.ValueMember = "CODSERIE";
                tHoraSai.Text = DateTime.Now.ToShortTimeString();

                psCodUsuarioCriacao.Text = AppLib.Context.Usuario;
                psTextoBox1.Text = "0";
                cmbNfe.SelectedIndex = 1;
                psDateBox1.Text = DateTime.Now.ToString();
                dtEmissao.Text = DateTime.Now.ToString();
                psCodTipOper.textBox1.Text = codtipoper;
                new Class.Utilidades().criaCamposComplementaresOperacao("GOPERCOMPL", tabCamposComplementares, psCodTipOper.textBox1.Text);
                carregaParametros();

                string _tipOper = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CLASSIFICACAOCLIFOR FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ? ", new object[] { AppLib.Context.Empresa, psCodTipOper.textBox1.Text }).ToString();

                if (_tipOper == "3")
                {
                    newLookup1.Grid_WhereVisao[3].ValorFixo = @"select '0' as 'CLASSIFICACAOCLIFOR' union all 
                                                                select '1' as 'CLASSIFICACAOCLIFOR' union all 
                                                                select '2' as 'CLASSIFICACAOCLIFOR' union all 
                                                                select '3' as 'CLASSIFICACAOCLIFOR' union all 
                                                                select null from GTIPOPER";
                    newLookup1.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Clear();
                    newLookup1.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODEMPRESA", ValorColunaChave = AppLib.Context.Empresa.ToString() });
                    newLookup1.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODTIPOPER", ValorColunaChave = psCodTipOper.textBox1.Text });
                }
                else
                {
                    newLookup1.Grid_WhereVisao[3].ValorFixo = @"select '2' as 'CLASSIFICACAOCLIFOR' union all select CLASSIFICACAOCLIFOR from GTIPOPER ";
                    newLookup1.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Clear();
                    newLookup1.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODEMPRESA", ValorColunaChave = AppLib.Context.Empresa.ToString() });
                    newLookup1.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODTIPOPER", ValorColunaChave = psCodTipOper.textBox1.Text });
                }

                DataTable dtFisco = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT MENSAGEM FROM GOPERMENSAGEMFISCO INNER JOIN VOPERMENSAGEM ON GOPERMENSAGEMFISCO.CODEMPRESA = VOPERMENSAGEM.CODEMPRESA AND GOPERMENSAGEMFISCO.CODMENSAGEM = VOPERMENSAGEM.CODMENSAGEM WHERE GOPERMENSAGEMFISCO.CODTIPOPER = ? AND GOPERMENSAGEMFISCO.CODEMPRESA = ?  ", new object[] { codtipoper, AppLib.Context.Empresa });
                for (int i = 0; i < dtFisco.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        txtMemoFisco.Text = dtFisco.Rows[i]["MENSAGEM"].ToString() + "\r\n";
                    }
                    else
                    {
                        txtMemoFisco.Text += dtFisco.Rows[i]["MENSAGEM"].ToString() + "\r\n";
                    }
                }
            }
            if (psTextoBox1.textBox1.Text == "0" || psTextoBox1.textBox1.Text == string.Empty)
            {
                this.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODTIPOPER + ' - ' + DESCRICAO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, psCodTipOper.textBox1.Text }).ToString();
            }
            else
            {
                this.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT GOPER.NUMERO + ' - (' + GOPER.CODTIPOPER + ' - ' + GTIPOPER.DESCRICAO + ')' FROM GOPER INNER JOIN GTIPOPER ON GOPER.CODTIPOPER = GTIPOPER.CODTIPOPER AND GOPER.CODEMPRESA = GTIPOPER.CODEMPRESA WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ?", new object[] { AppLib.Context.Empresa, psTextoBox1.textBox1.Text }).ToString();
            }

            habilitaCampos();
            VerificaAcesso();
            verificaEdicao();
            //carregaGridChaveRef();
            //validaNumeroOperacao();

            bool UsaAbaCTRC = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT USAABARELACCTRC FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, psCodTipOper.textBox1.Text }));

            if (UsaAbaCTRC == true)
            {
                CarregaGridCTRC("WHERE CODOPER = " + codoper + "");
            }
            else
            {
                tabControl1.TabPages.Remove(tabPage5);
            }

            string ValidaEstoque = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT GOPER.DATAEMISSAO 
                                                                                                            FROM GOPER 
                                                                                                            INNER JOIN GTIPOPER ON GTIPOPER.CODEMPRESA = GOPER.CODEMPRESA AND GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER 
                                                                                                            WHERE (GTIPOPER.OPERESTOQUE <> 'N' OR GTIPOPER.OPERESTOQUE2 <> 'N') 
                                                                                                            AND GOPER.CODEMPRESA=  ? AND GOPER.CODOPER = ?", new object[] { AppLib.Context.Empresa, codoper }).ToString();

            if (!string.IsNullOrEmpty(ValidaEstoque))
            {
                // Validação do fechamento de Estoque
                DateTime DataFechamentoEstoque = Convert.ToDateTime(AppLib.Context.poolConnection.Get("Start").ExecGetField(null, "SELECT DATAFECHAMENTOESTOQUE FROM VPARAMETROS WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }));

                if (DataEmissao == null)
                {
                    DataEmissao = AppLib.Context.poolConnection.Get("Start").GetDateTime();
                }

                if (DataEmissao < DataFechamentoEstoque)
                {
                    btnSalvarAtual.Enabled = false;
                    btnOKAtual.Enabled = false;

                    DesabilitaBotao = true;
                }
            }

            // Jõão Pedro Luchiari - 19/07/2018
            // Validação do fechamento de Estoque
            //DateTime DataFechamentoEstoque = Convert.ToDateTime(AppLib.Context.poolConnection.Get("Start").ExecGetField(null, "SELECT DATAFECHAMENTOESTOQUE FROM VPARAMETROS WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }));

            //if (DataEmissao == null)
            //{
            //    DataEmissao = AppLib.Context.poolConnection.Get("Start").GetDateTime();
            //}

            //if (DataEmissao < DataFechamentoEstoque)
            //{
            //    btnSalvarAtual.Enabled = false;
            //    btnOKAtual.Enabled = false;

            //    DesabilitaBotao = true;
            //}
        }

        //Fábio Campos 22/09/2017 15:17
        private bool validaUsaAprovacao(bool _usaAprov)
        {
            bool USAAPROVA = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT USAAPROVACAO FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { psCodTipOper.textBox1.Text, AppLib.Context.Empresa }));
            if (USAAPROVA == true)
            {
                if (_usaAprov == true)
                {
                    return false;
                }
            }
            return true;
        }

        private void verificaEdicao()
        {
            DataTable dtAcesso = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT *  FROM GPERFILTIPOPER INNER JOIN GUSUARIOPERFIL ON GPERFILTIPOPER.CODPERFIL = GUSUARIOPERFIL.CODPERFIL WHERE GPERFILTIPOPER.CODEMPRESA = ? AND GPERFILTIPOPER.CODTIPOPER = ? AND GUSUARIOPERFIL.CODPERFIL = ?", new object[] { AppLib.Context.Empresa, psCodTipOper.textBox1.Text, AppLib.Context.Perfil });
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
        /// ////////////////////////////////////////////////////
        /// 
        private void button3_Click(object sender, EventArgs e)
        {
            //////////psMemoBox2.Text = string.Empty;
            ////////////Buscar a informação do parametro.
            ////////////Item

            ////////////Parametro
            //////////DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT MENSAGEM FROM VOPERMENSAGEM INNER JOIN GOPERMENSAGEM ON VOPERMENSAGEM.CODMENSAGEM = GOPERMENSAGEM.CODMENSAGEM WHERE GOPERMENSAGEM.CODTIPOPER = ?", new object[] { psCodTipOper.Text });
            //////////for (int i = 0; i < dt.Rows.Count; i++)
            //////////{
            //////////    psMemoBox2.Text = dt.Rows[i]["MENSAGEM"].ToString();
            //////////}
            //psMemoBox2.Text = string.Concat();


            try
            {
                if (int.Parse(psTextoBox1.Text) > 0)
                {
                    if (psMemoBox2.Text != string.Empty)
                    {
                        psLookup1.OperSearchForm();

                        //sSql = "SELECT MENSAGEM FROM VOPERMENSAGEM INNER JOIN GOPERMENSAGEM ON VOPERMENSAGEM.CODMENSAGEM = GOPERMENSAGEM.CODMENSAGEM WHERE GOPERMENSAGEM.CODTIPOPER = ?";
                        string sMensagem = dbs.QueryValue(string.Empty, "SELECT MENSAGEM FROM VOPERMENSAGEM WHERE CODEMPRESA = ? AND CODMENSAGEM = ?", new object[] { AppLib.Context.Empresa, psLookup1.textBox1.Text }).ToString();
                        psMemoBox2.Text = psMemoBox2.Text + " " + sMensagem;
                    }
                    else
                    {
                        psMemoBox2.Text = string.Empty;
                        DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT MENSAGEM FROM VOPERMENSAGEM INNER JOIN GOPERMENSAGEM ON VOPERMENSAGEM.CODMENSAGEM = GOPERMENSAGEM.CODMENSAGEM WHERE GOPERMENSAGEM.CODTIPOPER = ?", new object[] { psCodTipOper.Text });
                        if (dt.Rows.Count == 0)
                        {
                            psLookup1.OperSearchForm();

                            //sSql = "SELECT MENSAGEM FROM VOPERMENSAGEM INNER JOIN GOPERMENSAGEM ON VOPERMENSAGEM.CODMENSAGEM = GOPERMENSAGEM.CODMENSAGEM WHERE GOPERMENSAGEM.CODTIPOPER = ?";
                            string sMensagem = dbs.QueryValue(string.Empty, "SELECT MENSAGEM FROM VOPERMENSAGEM WHERE CODEMPRESA = ? AND CODMENSAGEM = ?", new object[] { AppLib.Context.Empresa, psLookup1.textBox1.Text }).ToString();
                            psMemoBox2.Text = psMemoBox2.Text + " " + sMensagem;
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                psMemoBox2.Text = dt.Rows[i]["MENSAGEM"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Salve o registro antes de executar um aplicativo.");
                }
            }
            catch (Exception ex)
            {

                PS.Lib.Contexto.Session.key1 = null;
                PS.Lib.Contexto.Session.key2 = null;

                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }
        }

        private void psTextoBox7_Validating(object sender, CancelEventArgs e)
        {
            psTextoBox7.Text = psTextoBox7.Text.ToUpper();
        }

        private void psLookup8_AfterLookup(object sender, Lib.LookupEventArgs e)
        {

        }

        private void psLookup16_BeforeLookup(object sender, Lib.LookupEventArgs e)
        {

        }

        private void psLookup2_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(newLookup1.txtcodigo.Text))
            {
                this.GetDefaultCliFor();

            }

        }

        private void psLookup15_Validating(object sender, CancelEventArgs e)
        {

        }

        private void psLookup15_AfterLookup(object sender, Lib.LookupEventArgs e)
        {
            psMoedaBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT PRCOMISSAO FROM VREPRE WHERE CODREPRE  = ?", new object[] { psLookup15.Text }).ToString();
        }

        private void psLookup2_AfterLookup(object sender, Lib.LookupEventArgs e)
        {

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CGCCPF, DESCMAXVENDA, DESCMAXCOMPRA FROM VCLIFOR WHERE CODCLIFOR = ?", new object[] { newLookup1.txtcodigo.Text });
            psMaskedTextBox1.Text = dt.Rows[0]["CGCCPF"].ToString();

            string tipoEntraSai = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT TIPENTSAI FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { psCodTipOper.textBox1.Text, AppLib.Context.Empresa }).ToString();
            psTextoBox9.Visible = true;
            if (tipoEntraSai == "E")
            {
                psTextoBox9.Text = dt.Rows[0]["DESCMAXCOMPRA"].ToString();
                psTextoBox9.Caption = "Desconto Máximo de Compra";
            }
            else
            {
                psTextoBox9.Text = dt.Rows[0]["DESCMAXVENDA"].ToString();
                psTextoBox9.Caption = "Desconto Máximo de Venda";
            }
            if (psComboBox1.Text == "Aberto")
            {
                psMoedaBox2.textBox1.Text = psTextoBox9.Text;
            }

            if (!string.IsNullOrEmpty(psTextoBox1.textBox1.Text))
            {
                if (AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT VCLIFOR.CODETD FROM GOPER INNER JOIN VCLIFOR	ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ? ", new object[] { psTextoBox1.textBox1.Text, AppLib.Context.Empresa }).ToString() == "EX")
                {
                    psLookup19.edita(true);
                    psTextoBox11.Edita = true;
                    psTextoBox12.Edita = true;
                }
                else
                {
                    psLookup19.edita(false);
                    psTextoBox11.Edita = false;
                    psTextoBox12.Edita = false;
                }
            }
        }

        private void psLookup14_AfterLookup(object sender, Lib.LookupEventArgs e)
        {

        }

        //private void psLookup3_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        //{
        //    e.Filtro.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
        //}

        private void psLookup17_BeforeLookup(object sender, Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
        }

        private void psComboBox4_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void psLookup14_BeforeLookup(object sender, Lib.LookupEventArgs e)
        {
            // Somente naturezas ativas
            if (!string.IsNullOrEmpty(psLookup5.textBox1.Text))
            {
                e.Filtro.Add(new PS.Lib.PSFilter("CODTRANSPORTADORA", psLookup5.textBox1.Text));
            }
        }

        private void btnCalculadora_Click(object sender, EventArgs e)
        {
            if (verificaValores(Convert.ToInt32(psTextoBox1.textBox1.Text)) == true)
            {
                return;
            }
            calculaTributo(Convert.ToInt32(psTextoBox1.textBox1.Text), Convert.ToInt32(psLookup3.textBox1.Text));
            carregaGridTributos();
        }

        private void calculaTributo(int codOper, int codFilial)
        {
            if (psValorDesconto.Edita == false)
            {
                decimal vlDesconto = Convert.ToDecimal(dbs.QueryValue(0, "SELECT SUM(VLDESCONTO) FROM GOPERITEM WHERE CODOPER = ? AND TIPODESCONTO = 'T' AND CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }));
                if (vlDesconto > 0)
                {
                    psValorDesconto.textBox1.Text = string.Format("{0:n2}", vlDesconto);
                }
                //return;
            }


            decimal fracao = 0;
            decimal frete = 0;
            decimal desconto = 0;
            decimal despesa = 0;
            decimal seguro = 0;
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();
            try
            {
                decimal totalGeralItens = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT SUM(VLTOTALITEM) FROM GOPERITEM WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }));
                if (totalGeralItens != 0)
                {
                    string sql = "SELECT GOPERITEM.NSEQITEM, VLTOTALITEM, GOPERITEM.CODPRODUTO, GOPERITEM.CODNATUREZA FROM GOPER INNER JOIN GOPERITEM ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER WHERE GOPER.CODOPER = ? AND GOPER.CODFILIAL = ? AND GOPER.CODEMPRESA = ?";
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { codOper, codFilial, AppLib.Context.Empresa });

                    //Cria as variaveis para os valores
                    decimal valorFrete = 0, valorDesconto = 0, valorDespesa = 0, valorSeguro = 0;


                    //Verifica se é faturamento
                    if (faturamento == true)
                    {
                        DataTable dtValoresOrigem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT VALORFRETE, VALORDESCONTO, VALORDESPESA, VALORSEGURO FROM GOPER WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa });
                        if (dtValoresOrigem.Rows.Count > 0)
                        {
                            valorFrete = Convert.ToDecimal(dtValoresOrigem.Rows[0]["VALORFRETE"]);
                            valorDesconto = Convert.ToDecimal(dtValoresOrigem.Rows[0]["VALORDESCONTO"]);
                            valorDespesa = Convert.ToDecimal(dtValoresOrigem.Rows[0]["VALORDESPESA"]);
                            valorSeguro = Convert.ToDecimal(dtValoresOrigem.Rows[0]["VALORSEGURO"]);
                        }
                    }
                    else
                    {
                        valorFrete = Convert.ToDecimal(psValorFrete.textBox1.Text);
                        valorDesconto = Convert.ToDecimal(psValorDesconto.textBox1.Text);
                        valorDespesa = Convert.ToDecimal(psValorDespesa.textBox1.Text);
                        valorSeguro = Convert.ToDecimal(psValorSeguro.textBox1.Text);
                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        fracao = Convert.ToDecimal(dt.Rows[i]["VLTOTALITEM"]) / totalGeralItens;

                        if (valorFrete != 0)
                        {
                            frete = fracao * valorFrete;
                        }
                        if (valorDesconto != 0)
                        {
                            desconto = fracao * valorDesconto;
                        }
                        if (valorDespesa != 0)
                        {
                            despesa = fracao * valorDespesa;
                        }
                        if (valorSeguro != 0)
                        {
                            seguro = fracao * valorSeguro;
                        }
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GOPERITEM SET RATEIODESPESA = ?, RATEIODESCONTO = ?, RATEIOFRETE = ?, RATEIOSEGURO = ? WHERE CODOPER = ? AND NSEQITEM = ? AND CODEMPRESA = ?", new object[] { despesa, desconto, frete, seguro, codOper, dt.Rows[i]["NSEQITEM"], AppLib.Context.Empresa });

                        PS.Lib.DataField CODEMPRESA = new PS.Lib.DataField("CODEMPRESA", null);
                        PS.Lib.DataField CODOPER = new PS.Lib.DataField("CODOPER", null);
                        PS.Lib.DataField NSEQITEM = new PS.Lib.DataField("NSEQITEM", null);
                        PS.Lib.DataField CODPRODUTO = new PS.Lib.DataField("CODPRODUTO", null);
                        PS.Lib.DataField CODNATUREZA = new PS.Lib.DataField("CODNATUREZA", null);

                        CODEMPRESA.Valor = AppLib.Context.Empresa;
                        CODOPER.Valor = codOper;
                        NSEQITEM.Valor = dt.Rows[i]["NSEQITEM"].ToString();
                        CODPRODUTO.Valor = dt.Rows[i]["CODPRODUTO"].ToString();
                        CODNATUREZA.Valor = dt.Rows[i]["CODNATUREZA"].ToString();



                    }
                    conn.Commit();
                }
            }
            catch (Exception)
            {

                conn.Rollback();
            }

        }
        #endregion

        #region NEW

        private bool verificaValores(int codOper)
        {
            decimal valorFrete = 0, valorDesconto = 0, valorDespesa = 0, valorSeguro = 0;
            if (faturamento == true)
            {
                DataTable dtValoresOrigem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT VALORFRETE, VALORDESCONTO, VALORDESPESA, VALORSEGURO FROM GOPER WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa });
                if (dtValoresOrigem.Rows.Count > 0)
                {
                    valorFrete = Convert.ToDecimal(dtValoresOrigem.Rows[0]["VALORFRETE"]);
                    valorDesconto = Convert.ToDecimal(dtValoresOrigem.Rows[0]["VALORDESCONTO"]);
                    valorDespesa = Convert.ToDecimal(dtValoresOrigem.Rows[0]["VALORDESPESA"]);
                    valorSeguro = Convert.ToDecimal(dtValoresOrigem.Rows[0]["VALORSEGURO"]);
                }
            }
            else
            {
                valorFrete = Convert.ToDecimal(psValorFrete.textBox1.Text);
                valorDesconto = Convert.ToDecimal(psValorDesconto.textBox1.Text);
                valorDespesa = Convert.ToDecimal(psValorDespesa.textBox1.Text);
                valorSeguro = Convert.ToDecimal(psValorSeguro.textBox1.Text);
            }

            if (vlFrete == valorFrete)
            {
                if (vlDesconto == valorDesconto)
                {
                    if (vlDespesa == valorDespesa)
                    {
                        if (vlSeguro == valorSeguro)
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

        public void geraTributo(int codOper, string codTipOper, int codFilial)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();

            try
            {

                conn.BeginTransaction();
                #region variavel
                string insert = string.Empty;
                bool Editado = false;
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
                DataTable dtItens = conn.ExecQuery(@"SELECT GOPERITEM.NSEQITEM, VLTOTALITEM, GOPERITEM.CODPRODUTO, GOPERITEM.CODNATUREZA, VPRODUTO.CODNCM, VCLIFOR.CODETD
FROM GOPER 
INNER JOIN GOPERITEM ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER 
INNER JOIN VPRODUTO ON GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO AND GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA
INNER JOIN VCLIFOR ON GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR AND VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA 
WHERE GOPER.CODOPER = ? AND GOPER.CODFILIAL = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, codFilial, AppLib.Context.Empresa });
                DataTable dtTipoTributo = conn.ExecQuery(@"SELECT CODTRIBUTO, ALIQUOTA, CODCST FROM GTIPOPERTRIBUTO WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper });
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

                if (dtItens.Rows.Count <= 0)
                {
                    MessageBox.Show("Não foi possível carregar os itens da Operação selecionada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dtTipoTributo.Rows.Count <= 0)
                {
                    MessageBox.Show("Não foi possível carregar os tributos da Operação selecionada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                #endregion

                //Verificar se altera rateio
                if (verificaValores(codOper) == false)
                {
                    conn.ExecTransaction("DELETE FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codOper });
                }
                else
                {
                    conn.ExecTransaction("DELETE FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND EDITADO = 0", new object[] { AppLib.Context.Empresa, codOper });
                }

                if (dtTipOper.Rows[0]["GERATRIBUTOS"].ToString() == "1")
                {
                    string UFDESTINATARIO = conn.ExecGetField(string.Empty, @"SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();
                    //pra cada item
                    for (int iItens = 0; iItens < dtItens.Rows.Count; iItens++)
                    {
                        //pra cada tributo
                        for (int iTributo = 0; iTributo < dtTipoTributo.Rows.Count; iTributo++)
                        {
                            if (faturamento == false)
                            {
                                Editado = Convert.ToBoolean(conn.ExecGetField(false, @"SELECT EDITADO FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ? AND CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, codOper, dtItens.Rows[iItens]["NSEQITEM"], dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }));
                            }
                            if (Editado == false)
                            {
                                #region Base de Calculo


                                string query = conn.ExecGetField(string.Empty, "SELECT QUERY FROM GTIPOPERTRIBUTO INNER JOIN GQUERY ON GTIPOPERTRIBUTO.CODEMPRESA = GQUERY.CODEMPRESA AND GTIPOPERTRIBUTO.CODQUERY = GQUERY.CODQUERY WHERE GTIPOPERTRIBUTO.CODTIPOPER  = ? AND GTIPOPERTRIBUTO.CODEMPRESA = ? AND GTIPOPERTRIBUTO.CODTRIBUTO = ?", new object[] { codTipOper, AppLib.Context.Empresa, dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }).ToString();

                                query = query.Replace("@CODEMPRESA", "'" + AppLib.Context.Empresa + "'").Replace("@CODNATUREZA", "'" + dtItens.Rows[iItens]["CODNATUREZA"] + "'").Replace("@CODTRIBUTO", "'" + dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString() + "'").Replace("@CODOPER", "'" + codOper + "'").Replace("@NSEQITEM", "'" + dtItens.Rows[iItens]["NSEQITEM"] + "'").Replace("@UFDESTINO", "'" + UFDESTINATARIO + "'");

                                TRB_BASE_CALCULO = Convert.ToDecimal(conn.ExecGetField(0, query, new object[] { }));
                                TRB_BCORIGINAL = TRB_BASE_CALCULO;

                                #endregion

                                #region Aliquota

                                DataRow result = VTRIBUTO.Rows.Find(new object[] { dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString() });
                                switch (result["ALIQUOTAEM"].ToString())
                                {
                                    case "0":
                                        TRB_ALIQUOTA = Convert.ToDecimal(result["ALIQUOTA"].ToString());
                                        break;
                                    //Produto
                                    case "1":
                                        TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ALIQUOTA FROM VPRODUTOTRIBUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODPRODUTO"], dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }));
                                        break;
                                    //Tipo Operação
                                    case "2":
                                        TRB_ALIQUOTA = Convert.ToDecimal(dtTipoTributo.Rows[iTributo]["ALIQUOTA"].ToString());
                                        break;
                                    //Natureza
                                    case "3":
                                        TRB_ALIQUOTA = 0;

                                        if (string.IsNullOrEmpty(dtItens.Rows[iItens]["CODNATUREZA"].ToString()))
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
                                                DataRow ALIQUOTA = VREGRAICMS.Rows.Find(new object[] { dtItens.Rows[iItens]["CODNATUREZA"].ToString() });
                                                TRB_ALIQUOTA = Convert.ToDecimal(ALIQUOTA["ALIQUOTA"]);
                                                //TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ALIQUOTA FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"] }));
                                            }
                                            else
                                            {

                                                TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ALIQUOTA FROM VNATUREZATRIBUTO WHERE CODEMPRESA = ? AND CODNATUREZA = ? AND CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString(), dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }));
                                            }
                                        }
                                        break;
                                    //Estado
                                    case "4":

                                        string codetd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();

                                        if (!string.IsNullOrEmpty(codetd))
                                        {
                                            TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ALIQUOTA FROM VPRODUTOTRIBUTOESTADO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODTRIBUTO = ? AND CODESTADO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODPRODUTO"], dtTipoTributo.Rows[iTributo]["CODTRIBUTO"], codetd }));
                                        }
                                        else
                                        {
                                            TRB_ALIQUOTA = 0;
                                        }
                                        break;
                                    case "5":
                                        string codetd1 = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();
                                        string ncm = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODNCM FROM VPRODUTO INNER JOIN GOPERITEM ON VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA AND VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODPRODUTO"] }).ToString();

                                        TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT VREGRAVARCFOP.ALIQINTERNA FROM VREGRAVARCFOP WHERE NCM = ? AND UFDESTINO = ?", new object[] { ncm, codetd1 }));

                                        break;
                                    default:
                                        break;
                                }


                                #endregion

                                #region Valor Aliquota

                                bool utilizaST = Convert.ToBoolean(conn.ExecGetField(false, "SELECT VREGRAICMS.UTILIZAST FROM VREGRAICMS INNER JOIN VNATUREZA ON VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA WHERE VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"] }));

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
                                        TRB_VALOR_ALIQUOTA = 0;
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
                                                                    AND VNATUREZA.CODEMPRESA = ?", new object[] { dtItens.Rows[iItens]["CODNATUREZA"], AppLib.Context.Empresa });

                                        if (selecaoMVA.Rows.Count > 0)
                                        {
                                            TRB_REDUCAOBASEICMSST = Convert.ToDecimal(selecaoMVA.Rows[0]["REDUCAOBCST"]);
                                            string codetd1 = conn.ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();
                                            string ncm = conn.ExecGetField(string.Empty, "SELECT CODNCM FROM VPRODUTO INNER JOIN GOPERITEM ON VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA AND VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODPRODUTO"] }).ToString();
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
                                                                    WHERE VNATUREZA.CODNATUREZA = ? AND VNATUREZA.CODEMPRESA = ?", new object[] { dtItens.Rows[iItens]["CODNATUREZA"].ToString(), AppLib.Context.Empresa }).ToString();
                                    if (tipoTributacao == "D")
                                    {
                                        pdif = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT PDIF FROM VPRODUTOFISCAL 
                                                                    WHERE 
                                                                    CODETD = (SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR AND VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA WHERE CODOPER = ?)
                                                                    AND CODEMPRESA = ?
                                                                    AND CODPRODUTO = ?", new object[] { codOper, AppLib.Context.Empresa, dtItens.Rows[iItens]["CODPRODUTO"] }));
                                        TRB_VICMSDIF = ((TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100) * (pdif / 100);
                                        TRB_VALOR_ALIQUOTA = TRB_VALOR_ALIQUOTA - TRB_VICMSDIF;
                                    }
                                }
                                #endregion

                                #region CST

                                if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                                {
                                    TRB_CST = conn.ExecGetField(string.Empty, @"SELECT CODCST FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString() }).ToString();
                                }
                                else
                                {
                                    if (result["CODTIPOTRIBUTO"].ToString() == "IPI")
                                    {
                                        if (dtTipOper.Rows[0]["TIPENTSAI"].ToString() == "E")
                                        {
                                            TRB_CST = conn.ExecGetField(string.Empty, @"SELECT CODCSTENT FROM VREGRAIPI, VNATUREZA WHERE VREGRAIPI.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAIPI.IDREGRA = VNATUREZA.IDREGRAIPI AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString() }).ToString();
                                        }
                                        else
                                        {
                                            TRB_CST = conn.ExecGetField(string.Empty, @"SELECT CODCSTSAI FROM VREGRAIPI, VNATUREZA WHERE VREGRAIPI.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAIPI.IDREGRA = VNATUREZA.IDREGRAIPI AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString() }).ToString();
                                        }
                                    }
                                    else
                                    {
                                        TRB_CST = conn.ExecGetField(string.Empty, @"SELECT CODCST FROM VNATUREZATRIBUTO, VNATUREZA WHERE VNATUREZATRIBUTO.CODEMPRESA = VNATUREZA.CODEMPRESA AND VNATUREZATRIBUTO.CODNATUREZA = VNATUREZA.CODNATUREZA AND VNATUREZATRIBUTO.CODEMPRESA = ? AND VNATUREZATRIBUTO.CODNATUREZA = ? AND VNATUREZATRIBUTO.CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString(), dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }).ToString();

                                        if (string.IsNullOrEmpty(TRB_CST))
                                        {
                                            TRB_CST = conn.ExecGetField(0, @"SELECT CODCST FROM VPRODUTOTRIBUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODPRODUTO"], dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }).ToString();
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
                                    string ncm = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODNCM FROM VPRODUTO INNER JOIN GOPERITEM ON VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA AND VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODPRODUTO"] }).ToString();
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
                                                                                        WHERE VREGRAVARCFOP.NCM = ? AND VREGRAVARCFOP.UFDESTINO = ?", new object[] { NCM, Ufdestino }));

                                decimal PFCPST = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT VREGRAVARCFOP.PFCPST
                                                                                        FROM VREGRAVARCFOP
                                                                                        INNER JOIN VPRODUTO ON VREGRAVARCFOP.CODEMPRESA = VPRODUTO.CODEMPRESA
                                                                                        AND VREGRAVARCFOP.NCM = VPRODUTO.CODNCM
                                                                                        WHERE VREGRAVARCFOP.NCM = ? AND VREGRAVARCFOP.UFDESTINO = ?", new object[] { NCM, Ufdestino }));

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
                                        reg.Set("NSEQITEM", dtItens.Rows[iItens]["NSEQITEM"].ToString());
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
                                //                                    VCLIFOR.CODETD UFDEST,
                                //                                    GFILIAL.CODETD UFREM, 
                                //                                    GESTADO.ALIQUOTAICMSINTERNADEST, 
                                //                                    VREGRAICMS.ALIQUOTA ALIQUOTAINTERESTADUAL, 
                                //                                    PERCICMSUFDEST, 
                                //                                    VREGRAICMS.PERCFCP, 
                                //                                    VREGRAICMS.DIFERENCIALALIQUOTA, 
                                //                                    VCLIFOR.CONTRIBICMS, 
                                //                                    GOPER.TIPOPERCONSFIN,
                                //                                    GOPER.CLIENTERETIRA,
                                //                                    VCLIFOR.PRODUTORRURAL
                                //                                    FROM GOPER 
                                //                                    INNER JOIN VCLIFOR ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR 
                                //                                    INNER JOIN GFILIAL ON GOPER.CODEMPRESA = GFILIAL.CODEMPRESA
                                //                                    INNER JOIN GESTADO ON VCLIFOR.CODETD = GESTADO.CODETD
                                //                                    INNER JOIN GOPERITEM ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER
                                //                                    INNER JOIN VNATUREZA ON GOPERITEM.CODEMPRESA = VNATUREZA.CODEMPRESA AND GOPERITEM.CODNATUREZA = VNATUREZA.CODNATUREZA
                                //                                    INNER JOIN VREGRAICMS ON VNATUREZA.CODEMPRESA = VREGRAICMS.CODEMPRESA AND VNATUREZA.IDREGRAICMS = VREGRAICMS.IDREGRA
                                //                                    WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?";
                                //    DataTable dtDifal = conn.ExecQuery(sSql, new object[] { codOper, AppLib.Context.Empresa });
                                //    if (dtDifal.Rows.Count > 0)
                                //    {
                                //        AppLib.ORM.Jit reg = new AppLib.ORM.Jit(conn, "GOPERITEMDIFAL");
                                //        reg.Set("CODEMPRESA", AppLib.Context.Empresa);
                                //        reg.Set("CODOPER", codOper);
                                //        reg.Set("NSEQITEM", dtItens.Rows[iItens]["NSEQITEM"].ToString());
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
                                    TRB_REDBC = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT REDUCAOBASEICMS FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString() }));
                                }
                                if (result["CODTIPOTRIBUTO"].ToString() == "ICMS-ST")
                                {
                                    TRB_REDBC = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT REDUCAOBCST 
                                                                    FROM 
                                                                    VREGRAICMS 
                                                                    INNER JOIN VNATUREZA ON VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA
                                                                    WHERE	VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString() }));
                                }

                                #endregion

                                #region Enquadramento IPI

                                if (result["CODTIPOTRIBUTO"].ToString() == "IPI")
                                {
                                    TRB_CENQ = conn.ExecGetField(string.Empty, @"SELECT CENQ FROM VREGRAIPI, VNATUREZA WHERE VREGRAIPI.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAIPI.IDREGRA = VNATUREZA.IDREGRAIPI AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString() }).ToString();
                                }

                                #endregion

                                #region Percentual de crédito de ICMS

                                decimal VCREDICMSSN = 0;
                                decimal pCREDSN = 0;
                                if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                                {
                                    if (Convert.ToInt32(conn.ExecGetField(0, @"SELECT CREDITOICMS FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString() })) == 1)
                                    {
                                        pCREDSN = Convert.ToDecimal(conn.ExecGetField(0, "SELECT PCREDSN FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, codFilial }));
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

                                //insert = insert + @"INSERT INTO GOPERITEMTRIBUTO (CODEMPRESA, CODOPER, NSEQITEM, CODTRIBUTO, ALIQUOTA, VALOR, CODCST, BASECALCULO, MODALIDADEBC, REDUCAOBASEICMS, CENQ, FATORMVA, BCORIGINAL, REDUCAOBASEICMSST, VALORICMSST, PDIF, VICMSDIF, PCREDSN, VCREDICMSSN) VALUES (" + AppLib.Context.Empresa + ", " + codOper + "," + Convert.ToInt32(dtItens.Rows[iItens]["NSEQITEM"]) + ",'" + dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString() + "','" + string.Format("{0:n2}", TRB_ALIQUOTA).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_VALOR_ALIQUOTA).Replace(".", "").Replace(",", ".") + "','" + TRB_CST + "','" + string.Format("{0:n2}", TRB_BASE_CALCULO).Replace(".", "").Replace(",", ".") + "'," + modalidadeBC + ",'" + string.Format("{0:n2}", TRB_REDBC).Replace(".", "").Replace(",", ".") + "'," + cenq + ",'" + string.Format("{0:n2}", TRB_FATORMVA).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_BCORIGINAL).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_REDUCAOBASEICMSST).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_VALORICMSST).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", pdif).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_VICMSDIF).Replace(".", "").Replace(",", ".") + "', '" + string.Format("{0:n2}", pCREDSN).Replace(".", "").Replace(",", ".") + "', '" + string.Format("{0:n2}", VCREDICMSSN).Replace(".", "").Replace(",", ".") + "');\n\r";
                                insert = insert + @"INSERT INTO GOPERITEMTRIBUTO (CODEMPRESA, CODOPER, NSEQITEM, CODTRIBUTO, ALIQUOTA, VALOR, CODCST, BASECALCULO, MODALIDADEBC, REDUCAOBASEICMS, CENQ, FATORMVA, BCORIGINAL, REDUCAOBASEICMSST, VALORICMSST, PDIF, VICMSDIF, PCREDSN, VCREDICMSSN, VLDESPADUANA, VALORIOF) VALUES (" + AppLib.Context.Empresa + ", " + codOper + "," + Convert.ToInt32(dtItens.Rows[iItens]["NSEQITEM"]) + ",'" + dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString() + "','" + string.Format("{0:n2}", TRB_ALIQUOTA).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_VALOR_ALIQUOTA).Replace(".", "").Replace(",", ".") + "','" + TRB_CST + "','" + string.Format("{0:n2}", TRB_BASE_CALCULO).Replace(".", "").Replace(",", ".") + "'," + modalidadeBC + ",'" + string.Format("{0:n2}", TRB_REDBC).Replace(".", "").Replace(",", ".") + "'," + cenq + ",'" + string.Format("{0:n2}", TRB_FATORMVA).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_BCORIGINAL).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_REDUCAOBASEICMSST).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_VALORICMSST).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", pdif).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_VICMSDIF).Replace(".", "").Replace(",", ".") + "', '" + string.Format("{0:n2}", pCREDSN).Replace(".", "").Replace(",", ".") + "', '" + string.Format("{0:n2}", VCREDICMSSN).Replace(".", "").Replace(",", ".") + "','" + 0 + "','" + 0 + "');\n\r";

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
                        }
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

        private void btnFechar_Click(object sender, EventArgs e)
        {

        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            New.frmSelecaoColunas frm = new New.frmSelecaoColunas("GOPERITEM");
            frm.ShowDialog();
            carregaItens();
        }

        private void salvarLayout()
        {
            ////Altera todos os registro para visivel = false
            //AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, "GOPERITEM" });

            //for (int i = 0; i < gridView1.Columns.Count; i++)
            //{
            //    AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
            //    GVISAOUSUARIO.Set("VISAO", "GOPERITEM");
            //    GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
            //    GVISAOUSUARIO.Set("COLUNA", gridView1.Columns[i].FieldName);
            //    GVISAOUSUARIO.Set("SEQUENCIA", i);
            //    GVISAOUSUARIO.Set("LARGURA", gridView1.Columns[i].Width);
            //    if (gridView1.Columns[i].Visible == true)
            //    {
            //        GVISAOUSUARIO.Set("VISIVEL", 1);
            //    }
            //    else
            //    {
            //        GVISAOUSUARIO.Set("VISIVEL", 0);
            //    }
            //    GVISAOUSUARIO.Save();
            //}
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, "GOPERITEM" });


            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", "GOPERITEM");
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView1.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView1.VisibleColumns[i].Width);
                GVISAOUSUARIO.Set("VISIVEL", 1);
                GVISAOUSUARIO.Save();
            }

            DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ? AND FIXO = 1", new object[] { AppLib.Context.Usuario, "GOPERITEM" });
            if (dtFixo.Rows.Count > 0)
            {
                for (int i = 0; i < dtFixo.Rows.Count; i++)
                {
                    AppLib.ORM.Jit FIXO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                    FIXO.Set("VISAO", "GOPERITEM");
                    FIXO.Set("CODUSUARIO", AppLib.Context.Usuario);
                    FIXO.Set("COLUNA", dtFixo.Rows[i]["COLUNA"]);
                    //FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
                //carregaGrid(query);
            }
        }

        private void btnSalvarLayout_Click(object sender, EventArgs e)
        {
            salvarLayout();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
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

        private void btnAgrupar_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsView.ShowGroupPanel == true)
            {
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.ClearGrouping();
                btnAgrupar.Text = "Agrupar";
            }
            else
            {
                gridView1.OptionsView.ShowGroupPanel = true;
                btnAgrupar.Text = "Desagrupar";
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Salvar() == false)
            {
                MessageBox.Show("Não foi possível concluir a operação.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private bool validaObrigatorios()
        //{
        //    DataTable dtObrig = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM GCAMPOSOBRIGATORIO WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { });
        //    try
        //    {
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Test");
        //        return false;
        //        throw;
        //    }
        //}

        /// <summary>
        /// Método para salvar a operação em objeto
        /// </summary>
        /// <param name="goper">Objeto Goper</param>
        /// <returns>False para erro / True para Ok</returns>
        public bool salvarObjeto(Class.Goper goper)
        {
            try
            {
                #region Validações
                if (psComboBox1.Value.ToString() != "0" && psComboBox1.Value.ToString() != "5")
                {
                    MessageBox.Show("Somente operações em aberto podem ser editadas.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (edita == true)
                {
                    string StatusNFE = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODSTATUS FROM GNFESTADUAL WHERE CODOPER	= ? AND CODEMPRESA = ?", new object[] { psTextoBox1.textBox1.Text, AppLib.Context.Empresa }).ToString();
                    if (!string.IsNullOrEmpty(StatusNFE))
                    {
                        if (StatusNFE != "P" && StatusNFE != "E")
                        {
                            MessageBox.Show("Operação com NF-e vinculada não pode ser alterada.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }

                if (edita == false)
                {
                    int newCodOper = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT IDLOG FROM GLOG WHERE CODTABELA = 'GOPER' AND CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }));
                    if (newCodOper != 0)
                    {
                        psTextoBox1.textBox1.Text = (newCodOper + 1).ToString();
                    }
                }
                if (edita == false)
                {
                    if (validaNumeroOperacao() == true)
                    {
                        MessageBox.Show("Erro ao gravar operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return false;
                    }
                }

                if (dtEntrega.Enabled == true)
                {
                    if (Convert.ToDateTime(dtEmissao.DateTime.ToShortDateString()) > dtEntrega.DateTime)
                    {
                        MessageBox.Show("Data de entrega precisa ser maior que a data de emissão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                if (dtEntSai.Enabled == true)
                {
                    if (string.IsNullOrEmpty(dtEntSai.Text))
                    {
                        MessageBox.Show("A data Entrada/Saída deve ser preenchida.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(psTextoBox2.textBox1.Text))
                {
                    MessageBox.Show("O número do documento deve ser preenchido.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (edita == false)
                {
                    string codSerie = string.Empty;

                    if (psComboBox5.Value != DBNull.Value)
                    {
                        codSerie = txtCodserie.textBox1.Text;
                    }
                    else
                    {
                        codSerie = psComboBox5.Value.ToString();
                    }
                    int val = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(*) FROM GOPER WHERE CODCLIFOR = ? AND CODSERIE = ? AND NUMERO = ? AND CODFILIAL = ? ", new object[] { newLookup1.txtcodigo.Text, codSerie, psTextoBox2.textBox1.Text, codFilial }));
                    if (val > 0)
                    {
                        MessageBox.Show("Já existe uma operação com essas informações.", "Informação do sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                bool geraFinanceiro = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, @"SELECT GERAFINANCEIRO FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codtipoper, AppLib.Context.Empresa }));
                if (geraFinanceiro == true)
                {
                    if (string.IsNullOrEmpty(psLookup6.textBox1.Text))
                    {
                        MessageBox.Show("Para operações que geram lançamento financeiros, favor preencher o campo condição de pagamento.", "Informação do sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                PS.Glb.Class.Utilidades util = new PS.Glb.Class.Utilidades();

                if (util.validaCamposObrigatorios(this, ref errorProvider, null, psCodTipOper.textBox1.Text) == false)
                {
                    return false;
                }

                #endregion
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool Salvar()
        {
            #region Validações
            if ((psComboBox1.Value.ToString() != "0" && psComboBox1.Value.ToString() != "5" && psComboBox1.Value.ToString() != "10"))
            {
                MessageBox.Show("Somente operações em aberto podem ser editadas.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (edita == true)
            {
                string StatusNFE = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODSTATUS FROM GNFESTADUAL WHERE CODOPER	= ? AND CODEMPRESA = ?", new object[] { psTextoBox1.textBox1.Text, AppLib.Context.Empresa }).ToString();
                if (!string.IsNullOrEmpty(StatusNFE))
                {
                    if (StatusNFE != "I" && StatusNFE != "E")
                    {
                        MessageBox.Show("Operação com NF-e vinculada não pode ser alterada.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            if (edita == false)
            {
                int newCodOper = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT IDLOG FROM GLOG WHERE CODTABELA = 'GOPER' AND CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }));
                if (newCodOper != 0)
                {
                    psTextoBox1.textBox1.Text = (newCodOper + 1).ToString();
                }
            }
            if (edita == false)
            {
                if (validaNumeroOperacao() == true)
                {
                    MessageBox.Show("Erro ao gravar operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return false;
                }
            }

            if (dtEntrega.Enabled == true)
            {
                if (string.IsNullOrEmpty(dtEntrega.Text))
                {
                    MessageBox.Show("Favor preencher a data de entrega.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (dtEntrega.Enabled == true)
            {
                if (Convert.ToDateTime(dtEmissao.DateTime.ToShortDateString()) > dtEntrega.DateTime)
                {
                    MessageBox.Show("Data de entrega precisa ser maior que a data de emissão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (dtEntSai.Enabled == true)
            {
                if (string.IsNullOrEmpty(dtEntSai.Text))
                {
                    MessageBox.Show("A data Entrada/Saída deve ser preenchida.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (string.IsNullOrEmpty(psTextoBox2.textBox1.Text))
            {
                MessageBox.Show("O número do documento deve ser preenchido.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (edita == false)
            {
                string codSerie = string.Empty;

                if (psComboBox5.Value != DBNull.Value)
                {
                    codSerie = txtCodserie.textBox1.Text;
                }
                else
                {
                    codSerie = psComboBox5.Value.ToString();
                }
                int val = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(*) FROM GOPER WHERE CODCLIFOR = ? AND CODSERIE = ? AND NUMERO = ? AND CODFILIAL = ? ", new object[] { newLookup1.txtcodigo.Text, codSerie, psTextoBox2.textBox1.Text, codFilial }));
                if (val > 0)
                {
                    MessageBox.Show("Já existe uma operação com essas informações.", "Informação do sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            bool geraFinanceiro = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, @"SELECT GERAFINANCEIRO FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codtipoper, AppLib.Context.Empresa }));

            if (geraFinanceiro == true)
            {
                if (string.IsNullOrEmpty(psLookup6.textBox1.Text))
                {
                    MessageBox.Show("Para operações que geram lançamento financeiros, favor preencher o campo condição de pagamento.", "Informação do sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            PS.Glb.Class.Utilidades util = new PS.Glb.Class.Utilidades();

            if (util.validaCamposObrigatorios(this, ref errorProvider, null, psCodTipOper.textBox1.Text) == false)
            {
                return false;
            }

            #endregion

            if (tHoraSai.Text == "00:00:00")
            {
                tHoraSai.EditValue = AppLib.Context.poolConnection.Get("Start").GetDateTime().ToLongTimeString();
            }

            if (string.IsNullOrEmpty(newLookup1.txtcodigo.Text))
            {
                MessageBox.Show("O Cliente/Fornecedor deve ser preenchido.", "Informação do sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (CopiaOperacao == true)
            {
                if (NumeroOPer == Convert.ToInt32(psTextoBox2.Text) && SerieOper == txtCodserie.Text)
                {
                    MessageBox.Show("O número digitado já existe.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();
            try
            {
                //IDENTIFICAÇÃO
                AppLib.ORM.Jit GOPER = new AppLib.ORM.Jit(conn, "GOPER");
                GOPER.Set("CODEMPRESA", AppLib.Context.Empresa);
                GOPER.Set("CODOPER", psTextoBox1.textBox1.Text);
                GOPER.Set("CODSTATUS", psComboBox1.Value);
                GOPER.Set("NUMERO", psTextoBox2.textBox1.Text);
                GOPER.Set("SEGUNDONUMERO", txtSegundoNumero.textBox1.Text);

                if (psComboBox5.Value != null)
                {
                    GOPER.Set("CODSERIE", psComboBox5.Value);
                }
                else
                {
                    GOPER.Set("CODSERIE", txtCodserie.textBox1.Text);
                }

                GOPER.Set("CODTIPOPER", psCodTipOper.textBox1.Text);
                //GOPER.Set("CODCONTATO", string.IsNullOrEmpty(psLookup18.textBox1.Text) ? null : psLookup18.textBox1.Text);
                GOPER.Set("CODCONTATO", string.IsNullOrEmpty(txtCodContato.Text) ? null : txtCodContato.Text);


                if (edita == false)
                {
                    if (!string.IsNullOrEmpty(psDateBox1.Text))
                    {
                        GOPER.Set("DATACRIACAO", Convert.ToDateTime(psDateBox1.Text));
                    }
                    else
                    {
                        GOPER.Set("DATACRIACAO", null);
                    }

                    GOPER.Set("CODUSUARIOCRIACAO", AppLib.Context.Usuario);
                }
                GOPER.Set("USUARIOALTERACAO", AppLib.Context.Usuario);
                GOPER.Set("DATAALTERACAO", conn.GetDateTime());
                GOPER.Set("TIPOPERCONSFIN", chkTipOperConsFin.Checked == true ? 1 : 0);
                // João Pedro Luchiari
                GOPER.Set("CLIENTERETIRA", cmbOperacaoPresencial.Value);
                GOPER.Set("NFE", (cmbNfe.SelectedIndex == 0) ? 0 : 1);
                GOPER.Set("CHAVENFE", psTextoBox8.textBox1.Text);
                GOPER.Set("CODCLIFOR", newLookup1.txtcodigo.Text);
                GOPER.Set("NOMEFANTASIA", newLookup1.txtconteudo.Text);
                GOPER.Set("LIMITEDESC", string.IsNullOrEmpty(psTextoBox9.textBox1.Text) ? 0 : Convert.ToDecimal(psTextoBox9.textBox1.Text));
                GOPER.Set("ORDEMDECOMPRA", psTextoBox10.textBox1.Text);
                GOPER.Set("CODFILIAL", string.IsNullOrEmpty(psLookup3.textBox1.Text) ? null : psLookup3.textBox1.Text);
                GOPER.Set("CODFILIALENTREGA", string.IsNullOrEmpty(psLookup17.textBox1.Text) ? null : psLookup17.textBox1.Text);
                GOPER.Set("CODLOCAL", string.IsNullOrEmpty(psLookup7.textBox1.Text) ? null : psLookup7.textBox1.Text);
                GOPER.Set("CODLOCALENTREGA", string.IsNullOrEmpty(psLookup9.textBox1.Text) ? null : psLookup9.textBox1.Text);

                if (!string.IsNullOrEmpty(dtEmissao.Text))
                {
                    GOPER.Set("DATAEMISSAO", Convert.ToDateTime(dtEmissao.Text));// + " " + AppLib.Context.poolConnection.Get("Start").GetDateTime().ToLongTimeString()));
                }
                else
                {
                    GOPER.Set("DATAEMISSAO", null);
                }

                if (!string.IsNullOrEmpty(dtEntrega.Text))
                {
                    GOPER.Set("DATAENTREGA", Convert.ToDateTime(dtEntrega.Text));
                }
                else
                {
                    GOPER.Set("DATAENTREGA", null);
                }

                if (!string.IsNullOrEmpty(dtEntSai.Text))
                {
                    GOPER.Set("DATAENTSAI", Convert.ToDateTime(dtEntSai.Text + " " + tHoraSai.Text));
                }
                else
                {
                    GOPER.Set("DATAENTSAI", null);
                }

                GOPER.Set("VALORBRUTO", OP_VALORBRUTO);
                GOPER.Set("VALORLIQUIDO", OP_VALORLIQUIDO);
                //TABELAS
                GOPER.Set("CODCONDICAO", string.IsNullOrEmpty(psLookup6.textBox1.Text) ? null : psLookup6.textBox1.Text);
                // João Pedro Luchiari - Comentado pois o Lookup de Forma de Pagamento foi trocado
                //GOPER.Set("CODFORMA", string.IsNullOrEmpty(psLookup12.textBox1.Text) ? null : psLookup12.textBox1.Text);
                GOPER.Set("CODFORMA", string.IsNullOrEmpty(lpFormaPagamento.txtcodigo.Text) ? null : lpFormaPagamento.txtcodigo.Text);
                GOPER.Set("CODCONTA", string.IsNullOrEmpty(psLookup8.textBox1.Text) ? null : psLookup8.textBox1.Text);
                GOPER.Set("CODOPERADOR", string.IsNullOrEmpty(psLookup11.textBox1.Text) ? null : psLookup11.textBox1.Text);
                GOPER.Set("CODOBJETO", string.IsNullOrEmpty(psLookup4.textBox1.Text) ? null : psLookup4.textBox1.Text);
                GOPER.Set("CODREPRE", string.IsNullOrEmpty(psLookup15.textBox1.Text) ? null : psLookup15.textBox1.Text);
                GOPER.Set("PRCOMISSAO", Convert.ToDecimal(psMoedaBox1.textBox1.Text));
                GOPER.Set("CODVENDEDOR", string.IsNullOrEmpty(psLookup26.textBox1.Text) ? null : psLookup26.textBox1.Text);
                GOPER.Set("CODCCUSTO", string.IsNullOrEmpty(psLookup13.textBox1.Text) ? null : psLookup13.textBox1.Text);
                GOPER.Set("CODNATUREZAORCAMENTO", string.IsNullOrEmpty(psLookup16.textBox1.Text) ? null : psLookup16.textBox1.Text);

                if (psComboBox4.Value != null)
                {
                    GOPER.Set("TIPOBLOQUEIODESC", string.IsNullOrEmpty(psComboBox4.Value.ToString()) ? null : psComboBox4.Value);
                }

                GOPER.Set("LIMITEDESC", (psMoedaBox2.textBox1.Text == "" ? 0 : Convert.ToDecimal(psMoedaBox2.textBox1.Text)));
                GOPER.Set("DESCESPECIAL", chkDescEspecial.Checked == true ? 1 : 0);
                //VALORES
                GOPER.Set("PERCFRETE", string.IsNullOrEmpty(psPercFrete.textBox1.Text) ? 0 : Convert.ToDecimal(psPercFrete.textBox1.Text));
                GOPER.Set("VALORFRETE", string.IsNullOrEmpty(psValorFrete.textBox1.Text) ? 0 : Convert.ToDecimal(psValorFrete.textBox1.Text));
                GOPER.Set("PERCDESCONTO", string.IsNullOrEmpty(psPercDesconto.textBox1.Text) ? 0 : Convert.ToDecimal(psPercDesconto.textBox1.Text));
                GOPER.Set("VALORDESCONTO", string.IsNullOrEmpty(psValorDesconto.textBox1.Text) ? 0 : Convert.ToDecimal(psValorDesconto.textBox1.Text));
                GOPER.Set("PERCDESPESA", string.IsNullOrEmpty(psPercDespesa.textBox1.Text) ? 0 : Convert.ToDecimal(psPercDespesa.textBox1.Text));
                GOPER.Set("VALORDESPESA", string.IsNullOrEmpty(psValorDespesa.textBox1.Text) ? 0 : Convert.ToDecimal(psValorDespesa.textBox1.Text));
                GOPER.Set("PERCSEGURO", string.IsNullOrEmpty(psPercSeguro.textBox1.Text) ? 0 : Convert.ToDecimal(psPercSeguro.textBox1.Text));
                GOPER.Set("VALORSEGURO", string.IsNullOrEmpty(psValorSeguro.textBox1.Text) ? 0 : Convert.ToDecimal(psValorSeguro.textBox1.Text));
                //TRANSPORTE
                GOPER.Set("FRETECIFFOB", psComboBox2.Value);
                GOPER.Set("CODTRANSPORTADORA", string.IsNullOrEmpty(psLookup5.textBox1.Text) ? null : psLookup5.textBox1.Text);
                GOPER.Set("CODTIPOTRANSPORTE", string.IsNullOrEmpty(psLookup14.textBox1.Text) ? null : psLookup14.textBox1.Text);
                GOPER.Set("QUANTIDADE", string.IsNullOrEmpty(psMoedaBox4.textBox1.Text) ? 0 : Convert.ToInt32(psMoedaBox4.Text));
                GOPER.Set("PESOBRUTO", string.IsNullOrEmpty(psMoedaBox5.textBox1.Text) ? 0 : Convert.ToDecimal(psMoedaBox5.Text));
                GOPER.Set("PESOLIQUIDO", string.IsNullOrEmpty(psMoedaBox6.textBox1.Text) ? 0 : Convert.ToDecimal(psMoedaBox6.Text));
                GOPER.Set("ESPECIE", psTextoBox4.Text);
                GOPER.Set("MARCA", psTextoBox3.Text);
                GOPER.Set("PLACA", psTextoBox6.Text);
                GOPER.Set("UFPLACA", psTextoBox7.Text);
                GOPER.Set("UFSAIDAPAIS", string.IsNullOrEmpty(psLookup19.textBox1.Text) ? null : psLookup19.textBox1.Text);
                GOPER.Set("LOCEXPORTA", string.IsNullOrEmpty(psTextoBox11.textBox1.Text) ? null : psTextoBox11.textBox1.Text);
                GOPER.Set("LOCDESPACHO", string.IsNullOrEmpty(psTextoBox12.textBox1.Text) ? null : psTextoBox12.textBox1.Text);
                //HISTORICO
                GOPER.Set("OBSERVACAO", string.IsNullOrEmpty(psMemoBox1.textBox1.Text) ? null : psMemoBox1.textBox1.Text);
                GOPER.Set("HISTORICO", string.IsNullOrEmpty(psMemoBox2.textBox1.Text) ? null : psMemoBox2.textBox1.Text);
                GOPER.Set("NFEINFADIC", string.IsNullOrEmpty(txtMemoFisco.Text) ? null : txtMemoFisco.Text);
                GOPER.Set("MENSAGEMIBPTAX", string.IsNullOrEmpty(txtMensagemIBPTAX.Text) ? null : txtMensagemIBPTAX.Text);
                GOPER.Set("APROVACAO", 0);
                GOPER.Save();

                verificaDesconto(psCodTipOper.textBox1.Text, newLookup1.txtcodigo.Text, Convert.ToInt32(psTextoBox1.textBox1.Text), conn);

                SalvaCompl(conn);

                //AppLib.ORM.Jit GOPERCOMPL = new AppLib.ORM.Jit(conn, "GOPERCOMPL");
                //GOPERCOMPL.Set("CODEMPRESA", AppLib.Context.Empresa);
                //GOPERCOMPL.Set("CODOPER", psTextoBox1.textBox1.Text);
                //GOPERCOMPL.Set("ENTREGA", null);
                //GOPERCOMPL.Set("EMBALAGEM", null);
                //GOPERCOMPL.Set("IMPRESSAO", null);
                //GOPERCOMPL.Set("COR", null);
                //GOPERCOMPL.Set("CERTQUALIDADE", null);

                //GOPERCOMPL.Save();

                if (edita == false)
                {
                    if (!string.IsNullOrEmpty(numero))
                    {
                        conn.ExecTransaction(@"UPDATE VSERIE SET NUMSEQ = ? WHERE CODSERIE = ? AND CODEMPRESA = ? AND CODFILIAL = ?", new object[] { numero, codSerie, AppLib.Context.Empresa, codFilial });
                    }
                    conn.ExecTransaction(@"UPDATE GLOG SET IDLOG = ? WHERE CODTABELA = ? AND CODEMPRESA = ?", new object[] { psTextoBox1.textBox1.Text, "GOPER", AppLib.Context.Empresa });
                    conn.ExecTransaction(@"UPDATE GFILIAL SET SEGUNDONUMERO = ? WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { GOPER.Get("SEGUNDONUMERO"), AppLib.Context.Empresa, codFilial });
                }

                conn.Commit();
            }
            catch (Exception ex)
            {
                conn.Rollback();
                return false;
            }

            try
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("Processando");
                if (edita == true)
                {
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT USAVALORLIQUIDO, USAVALORBRUTO FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { psCodTipOper.textBox1.Text, AppLib.Context.Empresa });
                    if (dt.Rows[0]["USAVALORLIQUIDO"].ToString() != "E" && dt.Rows[0]["USAVALORBRUTO"].ToString() != "E")
                    {
                        calculaTributo(Convert.ToInt32(psTextoBox1.textBox1.Text), Convert.ToInt32(psLookup3.textBox1.Text));
                        if (ValidaItensOperacao(codoper) == true)
                        {
                            geraTributo(Convert.ToInt32(psTextoBox1.textBox1.Text), psCodTipOper.textBox1.Text, Convert.ToInt32(psLookup3.textBox1.Text));
                        }
                        calculaOperacao(Convert.ToInt32(psTextoBox1.textBox1.Text), psCodTipOper.textBox1.Text);
                        this.geraFinanceiro(psCodTipOper.textBox1.Text, Convert.ToInt32(psTextoBox1.textBox1.Text));
                    }
                    if (faturamento == true)
                    {
                        //Busca as informações da origem
                        string codOperOrigem = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODOPER FROM GOPERRELAC WHERE CODOPERRELAC = ? AND CODEMPRESA = ?", new object[] { psTextoBox1.textBox1.Text, AppLib.Context.Empresa }).ToString();
                        if (!string.IsNullOrEmpty(codOperOrigem))
                        {
                            Class.Goper goper = new Class.Goper().getGoper(codOperOrigem, conn);
                            if (!string.IsNullOrEmpty(goper.CODTIPOPER))
                            {
                                calculaTributo(goper.CODOPER, goper.CODFILIAL);
                                if (ValidaItensOperacao(codoper) == true)
                                {
                                    geraTributo(goper.CODOPER, goper.CODTIPOPER, goper.CODFILIAL);
                                }
                                calculaOperacao(goper.CODOPER, goper.CODTIPOPER);
                                this.geraFinanceiro(goper.CODTIPOPER, goper.CODOPER);
                            }
                        }
                    }
                }
                // Bloqueio de Limite de crédito
                string usaLimiteCredito = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT USALIMITECREDITO FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ? ", new object[] { psCodTipOper.textBox1.Text, AppLib.Context.Empresa }).ToString();
                if (!string.IsNullOrEmpty(usaLimiteCredito) && usaLimiteCredito == "0")
                {
                    string codQuery = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODQUERYLIMITECREDITO FROM VPARAMETROS WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();
                    if (!string.IsNullOrEmpty(codQuery))
                    {
                        string query = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT QUERY FROM GQUERY WHERE CODQUERY = ? AND CODEMPRESA = ?", new object[] { codQuery, AppLib.Context.Empresa }).ToString();
                        query = query.Replace("@CODEMPRESA", AppLib.Context.Empresa.ToString()).Replace("@CODCLIFOR", newLookup1.txtcodigo.Text);

                        decimal resultado = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, query, new object[] { }));

                        if (string.IsNullOrEmpty(psValorLiquido.textBox1.Text))
                        {
                            psValorLiquido.textBox1.Text = "0";
                        }

                        if (Convert.ToDecimal(psValorLiquido.textBox1.Text) > resultado)
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GOPER SET CODSITUACAO = 7 WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { psTextoBox1.textBox1.Text, AppLib.Context.Empresa });
                        }
                    }
                }
                carregaGridTributos();
                AtualizaValores();
                splashScreenManager1.CloseWaitForm();
                edita = true;
                faturamento = false;

                // Método de Ficha Estoque
                if (CopiaOperacao == true)
                {
                    PS.Glb.Class.FichaEstoque Ficha = new Class.FichaEstoque();

                    PS.Glb.Class.FichaEstoque.Processo TipoProcesso;
                    TipoProcesso = Class.FichaEstoque.Processo.Copia;

                    Ficha.CODOPER = codoper;
                    Ficha.CODTIPOPER = psCodTipOper.Text;

                    Ficha.ExecutaProcessoEstoque(TipoProcesso);
                }

                if (FaturamentoOperacao == true)
                {
                    PS.Glb.Class.FichaEstoque Ficha = new Class.FichaEstoque();

                    PS.Glb.Class.FichaEstoque.Processo TipoProcesso;
                    TipoProcesso = Class.FichaEstoque.Processo.Faturamento;

                    Ficha.CODOPER = codoper;
                    Ficha.CODTIPOPER = psCodTipOper.Text;

                    Ficha.ExecutaProcessoEstoque(TipoProcesso);
                }

                return true;
            }
            catch (Exception ex)
            {
                splashScreenManager1.CloseWaitForm();
                return false;
            }
        }

        private void carregaItens()
        {
            try
            {
                string tabela = "GOPERITEM";
                string relacionamento = @"INNER JOIN VPRODUTO ON GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO";
                List<string> tabelasFilhas = new List<string>();
                tabelasFilhas.Add("VPRODUTO");
                string where = "WHERE GOPERITEM.CODOPER = " + codoper + " AND GOPERITEM.CODEMPRESA = " + AppLib.Context.Empresa;

                string sql = new Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);
                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
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

        private void CarregaCampos()
        {
            try
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
GOPER.*, 
VCLIFOR.CGCCPF AS CNPJ,
VCLIFOR.NOMEFANTASIA AS CLIENTE,
GFILIAL.NOMEFANTASIA AS FILIAL,
VCLIFORCONTATO.NOME AS CONTATO
FROM 
GOPER
 INNER JOIN VCLIFOR ON GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR AND GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA 
 INNER JOIN GFILIAL ON GOPER.CODFILIAL = GFILIAL.CODFILIAL AND GOPER.CODEMPRESA = GFILIAL.CODEMPRESA
 LEFT OUTER JOIN VCLIFORCONTATO ON GOPER.CODEMPRESA = VCLIFORCONTATO.CODEMPRESA AND GOPER.CODCONTATO = VCLIFORCONTATO.CODCONTATO AND GOPER.CODCLIFOR = VCLIFORCONTATO.CODCLIFOR
WHERE CODOPER = ? AND GOPER.CODEMPRESA = ?
", new object[] { codoper, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    psTextoBox1.textBox1.Text = dt.Rows[0]["CODOPER"].ToString();
                    psComboBox1.SelectedIndex = Convert.ToInt32(dt.Rows[0]["CODSTATUS"].ToString());
                    psTextoBox2.textBox1.Text = dt.Rows[0]["NUMERO"].ToString();
                    txtSegundoNumero.textBox1.Text = dt.Rows[0]["SEGUNDONUMERO"].ToString();
                    psComboBox5.Value = dt.Rows[0]["CODSERIE"].ToString();
                    txtCodserie.textBox1.Text = dt.Rows[0]["CODSERIE"].ToString();
                    psCodTipOper.textBox1.Text = dt.Rows[0]["CODTIPOPER"].ToString();

                    txtCodContato.Text = dt.Rows[0]["CODCONTATO"].ToString();
                    txtDescricaoContato.Text = dt.Rows[0]["CONTATO"].ToString();
                    psDateBox1.Text = dt.Rows[0]["DATACRIACAO"].ToString();
                    psCodUsuarioCriacao.textBox1.Text = dt.Rows[0]["CODUSUARIOCRIACAO"].ToString();
                    chkTipOperConsFin.Checked = Convert.ToBoolean(dt.Rows[0]["TIPOPERCONSFIN"]);
                    cmbOperacaoPresencial.Value = dt.Rows[0]["CLIENTERETIRA"].ToString();
                    chkDescEspecial.Checked = dt.Rows[0]["DESCESPECIAL"].ToString() == "False" ? false : true;
                    if (string.IsNullOrEmpty(dt.Rows[0]["NFE"].ToString()))
                    {
                        cmbNfe.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbNfe.SelectedIndex = Convert.ToInt32(dt.Rows[0]["NFE"]);
                    }

                    psTextoBox8.textBox1.Text = dt.Rows[0]["CHAVENFE"].ToString();

                    newLookup1.txtcodigo.Text = dt.Rows[0]["CODCLIFOR"].ToString();
                    newLookup1.CarregaDescricao();
                    tbEstadoClifor.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR WHERE CODEMPRESA = ? AND CODCLIFOR = ?", new object[] { AppLib.Context.Empresa, newLookup1.txtcodigo.Text }).ToString();

                    //newLookup1.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Clear();
                    //newLookup1.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODEMPRESA", ValorColunaChave = AppLib.Context.Empresa.ToString() });
                    //newLookup1.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODTIPOPER", ValorColunaChave = psCodTipOper.textBox1.Text });

                    string _tipOper = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CLASSIFICACAOCLIFOR FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ? ", new object[] { AppLib.Context.Empresa, psCodTipOper.textBox1.Text }).ToString();

                    if (_tipOper == "3")
                    {
                        newLookup1.Grid_WhereVisao[3].ValorFixo = @"select '0' as 'CLASSIFICACAOCLIFOR' union all 
                                                                select '1' as 'CLASSIFICACAOCLIFOR' union all 
                                                                select '2' as 'CLASSIFICACAOCLIFOR' union all 
                                                                select '3' as 'CLASSIFICACAOCLIFOR' union all 
                                                                select null from GTIPOPER";
                        newLookup1.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Clear();
                        newLookup1.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODEMPRESA", ValorColunaChave = AppLib.Context.Empresa.ToString() });
                        newLookup1.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODTIPOPER", ValorColunaChave = psCodTipOper.textBox1.Text });
                    }
                    else
                    {
                        newLookup1.Grid_WhereVisao[3].ValorFixo = @"select '2' as 'CLASSIFICACAOCLIFOR' union all select CLASSIFICACAOCLIFOR from GTIPOPER ";
                        newLookup1.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Clear();
                        newLookup1.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODEMPRESA", ValorColunaChave = AppLib.Context.Empresa.ToString() });
                        newLookup1.Grid_WhereVisao[3].OutrosFiltros_SelectQuery.Add(new Newlookup_OutrasChaves { NomeColunaChave = "CODTIPOPER", ValorColunaChave = psCodTipOper.textBox1.Text });
                    }

                    //if (!string.IsNullOrEmpty(txtCodCliFor.Text))
                    //{
                    //    txtDescricaoCliente.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NOME FROM VCLIFOR WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { txtCodCliFor.Text, AppLib.Context.Empresa }).ToString();
                    //}
                    psMaskedTextBox1.textBox1.Text = dt.Rows[0]["CNPJ"].ToString();
                    psTextoBox9.textBox1.Text = dt.Rows[0]["LIMITEDESC"].ToString();
                    psTextoBox10.textBox1.Text = dt.Rows[0]["ORDEMDECOMPRA"].ToString();
                    psLookup3.textBox1.Text = dt.Rows[0]["CODFILIAL"].ToString();
                    psLookup3.LoadLookup();
                    psLookup17.textBox1.Text = dt.Rows[0]["CODFILIALENTREGA"].ToString();
                    if (!string.IsNullOrEmpty(psLookup17.textBox1.Text) && psLookup17.textBox1.Text != "0")
                    {
                        psLookup17.LoadLookup();
                    }

                    psLookup7.textBox1.Text = dt.Rows[0]["CODLOCAL"].ToString();
                    psLookup7.LoadLookup();
                    psLookup9.textBox1.Text = dt.Rows[0]["CODLOCALENTREGA"].ToString();
                    if (psLookup17.textBox1.Text != string.Empty)
                    {
                        psLookup9.LoadLookup();
                    }

                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEMISSAO"].ToString()))
                    {
                        dtEmissao.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAEMISSAO"]);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAENTREGA"].ToString()))
                    {
                        dtEntrega.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAENTREGA"]);
                        DataEntrega = dtEntrega.Text;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAENTSAI"].ToString()))
                    {
                        dtEntSai.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAENTSAI"]);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAALTERACAO"].ToString()))
                    {
                        txtDataAlteracao.Text = Convert.ToDateTime(dt.Rows[0]["DATAALTERACAO"]).ToString();
                    }
                    txtUsuarioAlteracao.Text = dt.Rows[0]["USUARIOALTERACAO"].ToString();

                    tHoraSai.EditValue = string.IsNullOrEmpty(dt.Rows[0]["DATAENTSAI"].ToString()) ? string.Empty : Convert.ToDateTime(dt.Rows[0]["DATAENTSAI"]).ToLongTimeString();

                    psValorBruto.textBox1.Text = dt.Rows[0]["VALORBRUTO"].ToString();
                    psValorLiquido.textBox1.Text = dt.Rows[0]["VALORLIQUIDO"].ToString();

                    // Aba Tabelas

                    psLookup6.textBox1.Text = dt.Rows[0]["CODCONDICAO"].ToString();
                    psLookup6.LoadLookup();
                    // João Pedro Luchiari - Comentado pois o Lookup de Forma de Pagamento foi trocado
                    //psLookup12.textBox1.Text = dt.Rows[0]["CODFORMA"].ToString();
                    //psLookup12.LoadLookup();
                    lpFormaPagamento.txtcodigo.Text = dt.Rows[0]["CODFORMA"].ToString();
                    lpFormaPagamento.CarregaDescricao();
                    psLookup8.textBox1.Text = dt.Rows[0]["CODCONTA"].ToString();
                    psLookup8.LoadLookup();
                    psLookup11.textBox1.Text = dt.Rows[0]["CODOPERADOR"].ToString();
                    psLookup11.LoadLookup();
                    psLookup4.textBox1.Text = dt.Rows[0]["CODOBJETO"].ToString();
                    psLookup4.LoadLookup();
                    psLookup15.textBox1.Text = dt.Rows[0]["CODREPRE"].ToString();
                    psLookup15.LoadLookup();
                    psMoedaBox1.textBox1.Text = dt.Rows[0]["PRCOMISSAO"].ToString();
                    psLookup26.textBox1.Text = dt.Rows[0]["CODVENDEDOR"].ToString();
                    psLookup26.LoadLookup();
                    psLookup13.textBox1.Text = dt.Rows[0]["CODCCUSTO"].ToString();
                    psLookup13.LoadLookup();
                    psLookup16.textBox1.Text = dt.Rows[0]["CODNATUREZAORCAMENTO"].ToString();
                    psLookup16.LoadLookup();
                    psComboBox4.Value = dt.Rows[0]["TIPOBLOQUEIODESC"].ToString();
                    psMoedaBox2.textBox1.Text = dt.Rows[0]["LIMITEDESC"].ToString();

                    // Aba Valores

                    psPercFrete.textBox1.Text = dt.Rows[0]["PERCFRETE"].ToString();
                    psPercFrete.textBox1.Text = casasDecimais_2(psPercFrete.textBox1.Text);

                    psValorFrete.textBox1.Text = dt.Rows[0]["VALORFRETE"].ToString();
                    psValorFrete.textBox1.Text = casasDecimais_2(psValorFrete.textBox1.Text);

                    psPercDesconto.textBox1.Text = dt.Rows[0]["PERCDESCONTO"].ToString();
                    psPercDesconto.textBox1.Text = casasDecimais_2(psPercDesconto.textBox1.Text);

                    psValorDesconto.textBox1.Text = dt.Rows[0]["VALORDESCONTO"].ToString();
                    psValorDesconto.textBox1.Text = casasDecimais_2(psValorDesconto.textBox1.Text);

                    psPercDespesa.textBox1.Text = dt.Rows[0]["PERCDESPESA"].ToString();
                    psPercDespesa.textBox1.Text = casasDecimais_2(psPercDespesa.textBox1.Text);

                    psValorDespesa.textBox1.Text = dt.Rows[0]["VALORDESPESA"].ToString();
                    psValorDespesa.textBox1.Text = casasDecimais_2(psValorDespesa.textBox1.Text);

                    psPercSeguro.textBox1.Text = dt.Rows[0]["PERCSEGURO"].ToString();
                    psPercSeguro.textBox1.Text = casasDecimais_2(psPercSeguro.textBox1.Text);

                    psValorSeguro.textBox1.Text = dt.Rows[0]["VALORSEGURO"].ToString();
                    psValorSeguro.textBox1.Text = casasDecimais_2(psValorSeguro.textBox1.Text);

                    vlFrete = Convert.ToDecimal(psValorFrete.textBox1.Text);
                    vlDesconto = Convert.ToDecimal(psValorDesconto.textBox1.Text);
                    vlDespesa = Convert.ToDecimal(psValorDespesa.textBox1.Text);
                    vlSeguro = Convert.ToDecimal(psValorSeguro.textBox1.Text);

                    carregaGridTributos();

                    // Aba Transporte
                    psComboBox2.Value = Convert.ToInt32(dt.Rows[0]["FRETECIFFOB"].ToString());
                    psLookup5.textBox1.Text = dt.Rows[0]["CODTRANSPORTADORA"].ToString();
                    if (!string.IsNullOrEmpty(psLookup5.textBox1.Text))
                    {
                        psLookup5.LoadLookup();
                    }
                    psLookup14.textBox1.Text = dt.Rows[0]["CODTIPOTRANSPORTE"].ToString();
                    if (!string.IsNullOrEmpty(psLookup14.textBox1.Text) && psLookup14.textBox1.Text != "0")
                    {
                        psLookup14.LoadLookup();
                    }


                    psMoedaBox4.Text = dt.Rows[0]["QUANTIDADE"].ToString();
                    psMoedaBox5.Text = dt.Rows[0]["PESOBRUTO"].ToString();
                    psMoedaBox6.Text = dt.Rows[0]["PESOLIQUIDO"].ToString();
                    psTextoBox4.Text = dt.Rows[0]["ESPECIE"].ToString();
                    psTextoBox3.Text = dt.Rows[0]["MARCA"].ToString();
                    psTextoBox6.Text = dt.Rows[0]["PLACA"].ToString();
                    psTextoBox7.Text = dt.Rows[0]["UFPLACA"].ToString();
                    psLookup19.textBox1.Text = dt.Rows[0]["UFSAIDAPAIS"].ToString();
                    psTextoBox11.Text = dt.Rows[0]["LOCEXPORTA"].ToString();
                    psTextoBox12.Text = dt.Rows[0]["LOCDESPACHO"].ToString();

                    // Aba Observação

                    psMemoBox1.textBox1.Text = dt.Rows[0]["OBSERVACAO"].ToString();
                    psMemoBox2.textBox1.Text = dt.Rows[0]["HISTORICO"].ToString();
                    txtMemoFisco.Text = dt.Rows[0]["NFEINFADIC"].ToString();
                    txtMensagemIBPTAX.Text = dt.Rows[0]["MENSAGEMIBPTAX"].ToString();
                    // Aba Itens

                    carregaItens();
                    new Class.Utilidades().criaCamposComplementaresOperacao("GOPERCOMPL", tabCamposComplementares, psCodTipOper.textBox1.Text);
                    carregaCamposComplementares();
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void carregaParametros()
        {
            #region Tipo de Operação

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, psCodTipOper.textBox1.Text });
            psTextoBox5.Text = Convert.ToString(AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { psCodTipOper.textBox1.Text, AppLib.Context.Empresa }));
            if (dt.Rows.Count > 0)
            {
                #region Default
                if (edita == false)
                {

                    #region Marca

                    psTextoBox3.Text = dt.Rows[0]["DEFAULTMARCA"].ToString();
                    psTextoBox4.Text = dt.Rows[0]["DEFAULTESPECIE"].ToString();

                    #endregion

                    #region Tipo de Desconto

                    psComboBox4.Value = dt.Rows[0]["DEFAULTBLOQUEIODESC"].ToString();

                    #endregion

                    #region Filial

                    // ORIGEM
                    psLookup3.Text = codFilial.ToString();
                    psLookup3.LoadLookup();
                    psLookup3.Chave = false;

                    // DESTINO
                    int CODFILIALENTREGA = int.Parse(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT CODFILIAL2DEFAULT FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, psCodTipOper.textBox1.Text }).ToString());
                    if (CODFILIALENTREGA != 0)
                    {
                        psLookup17.Text = CODFILIALENTREGA.ToString();
                        psLookup17.LoadLookup();
                        psLookup17.Chave = false;
                    }

                    #endregion

                    #region Cliente / Fornecedor

                    GLB_CLIFORMAMB = Convert.ToInt16(dt.Rows[0]["CLIFORAMB"]);
                    newLookup1.txtcodigo.Text = dt.Rows[0]["CODCLIFORPADRAO"].ToString();
                    if (!string.IsNullOrEmpty(newLookup1.txtcodigo.Text))
                    {
                        newLookup1.txtconteudo.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NOME FROM VCLIFOR WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { newLookup1.txtcodigo.Text, AppLib.Context.Empresa }).ToString();
                    }

                    #endregion

                    #region Operador

                    psLookup11.Text = dt.Rows[0]["CODOPERADORPADRAO"].ToString();
                    if (!string.IsNullOrEmpty(psLookup11.Text))
                    {
                        psLookup11.LoadLookup();
                    }

                    #endregion

                    #region Condição de Pagamento

                    psLookup6.Text = dt.Rows[0]["CODCONDICAOPADRAO"].ToString();
                    if (!string.IsNullOrEmpty(psLookup6.Text))
                    {
                        psLookup6.LoadLookup();
                    }

                    #endregion

                    #region Forma de Pagamento

                    // João Pedro Luchiari - Comentado pois o Lookup de Forma de Pagamento foi trocado
                    //psLookup12.Text = dt.Rows[0]["CODFORMAPADRAO"].ToString();
                    //if (!string.IsNullOrEmpty(psLookup12.Text))
                    //{
                    //    psLookup12.LoadLookup();
                    //}

                    lpFormaPagamento.txtcodigo.Text = dt.Rows[0]["CODFORMAPADRAO"].ToString();
                    if (!string.IsNullOrEmpty(lpFormaPagamento.txtcodigo.Text))
                    {
                        lpFormaPagamento.CarregaDescricao();
                    }

                    #endregion

                    #region Conta

                    psLookup8.Text = dt.Rows[0]["CODCONTAPADRAO"].ToString();
                    if (!string.IsNullOrEmpty(psLookup8.Text))
                    {
                        psLookup8.LoadLookup();
                    }

                    #endregion

                    #region Representante

                    psLookup15.Text = dt.Rows[0]["CODREPREPADRAO"].ToString();
                    if (!string.IsNullOrEmpty(psLookup15.Text))
                    {
                        psLookup15.LoadLookup();
                    }

                    #endregion

                    #region Vendedor

                    psLookup26.Text = dt.Rows[0]["CODVENDEDORPADRAO"].ToString();
                    if (!string.IsNullOrEmpty(psLookup26.Text))
                    {
                        psLookup26.LoadLookup();
                    }

                    #endregion

                    #region Local 1

                    psLookup7.Text = dt.Rows[0]["LOCAL1DEFAULT"].ToString();
                    if (psLookup7.Text != string.Empty)
                    {
                        psLookup7.LoadLookup();
                    }

                    #endregion

                    #region Local 2

                    psLookup9.Text = dt.Rows[0]["LOCAL2DEFAULT"].ToString();
                    if (psLookup9.Text != string.Empty)
                    {
                        psLookup9.LoadLookup();
                    }

                    #endregion

                }
                #endregion

                #region Número Documento

                if (dt.Rows[0]["USANUMEROSEQ"].ToString() == "1")
                {
                    psTextoBox2.Enabled = false;
                }
                else
                {
                    psTextoBox2.Enabled = true;
                    psTextoBox2.MaxLength = Convert.ToInt32(dt.Rows[0]["MASKNUMEROSEQ"]);
                }

                #endregion

                #region Tipo de Desconto

                if (dt.Rows[0]["USABLOQUEIODESCONTO"].ToString() == "N")
                {
                    psComboBox4.Enabled = false;
                    psMoedaBox2.Enabled = false;
                }

                #endregion

                #region Série

                if (dt.Rows[0]["SERIELIVRE"].ToString() == "1")
                {
                    psTextoBox2.Enabled = true;
                    txtCodserie.Visible = true;
                    psComboBox5.Visible = false;
                }
                else
                {
                    if (dt.Rows[0]["OPERSERIE"].ToString() == "N")
                    {
                        psComboBox5.Enabled = false;
                        psComboBox5.Value = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODSERIE FROM VTIPOPERSERIE WHERE CODEMPRESA = ? AND CODTIPOPER = ? AND CODFILIAL = ? AND PRINCIPAL = 1", new object[] { AppLib.Context.Empresa, psCodTipOper.textBox1.Text, codFilial }).ToString();
                    }
                    psTextoBox2.Enabled = false;
                    txtCodserie.Visible = false;
                }
                #endregion

                #region Cliente/Fornecedor

                operCodCliFor = dt.Rows[0]["CODCLIFOR"].ToString();
                if (dt.Rows[0]["CODCLIFOR"].ToString() == "N")
                {
                    newLookup1.Enabled = false;
                }

                #endregion

                #region Ordem de Compra

                if (dt.Rows[0]["USAORDEMDECOMPRA"].ToString() == "N")
                {
                    psTextoBox10.Visible = false;
                }

                #endregion

                #region Objeto

                if (dt.Rows[0]["USACAMPOOBJETO"].ToString() == "N")
                {
                    psLookup4.Enabled = false;
                    psLookup4.Chave = false;
                }

                #endregion

                #region Operador

                if (dt.Rows[0]["USACAMPOOPERADOR"].ToString() == "N")
                {
                    psLookup11.Enabled = false;
                    psLookup11.Chave = false;

                    psLookup11.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERADOR FROM VOPERADOR WHERE CODUSUARIO = ?", new object[] { AppLib.Context.Usuario }).ToString();
                    psLookup11.LoadLookup();
                }
                else
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0]["CODOPERADORPADRAO"].ToString()))
                    {
                        psLookup11.textBox1.Text = dt.Rows[0]["CODOPERADORPADRAO"].ToString();
                        psLookup11.LoadLookup();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(psLookup11.textBox1.Text))
                        {
                            psLookup11.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERADOR FROM VOPERADOR WHERE CODUSUARIO = ?", new object[] { AppLib.Context.Usuario }).ToString();
                            psLookup11.LoadLookup();

                        }
                    }

                }

                #endregion

                #region Condição de Pagamento

                if (dt.Rows[0]["USACAMPOCONDPGTO"].ToString() == "N")
                {
                    psLookup6.Enabled = false;
                    psLookup6.Chave = false;
                }


                #endregion

                #region Forma de Pagamento

                // João Pedro Luchiari - Comentado pois o Lookup de Forma de Pagamento foi trocado
                //if (dt.Rows[0]["CODFORMA"].ToString() == "N")
                //{
                //    psLookup12.Enabled = false;
                //    psLookup12.Chave = false;
                //}

                if (dt.Rows[0]["CODFORMA"].ToString() == "N")
                {
                    lpFormaPagamento.Enabled = false;
                }

                #endregion

                #region Conta/Caixa

                if (dt.Rows[0]["USACONTA"].ToString() == "N")
                {
                    psLookup8.Enabled = false;
                    psLookup8.Chave = false;
                }

                #endregion

                #region Representante

                if (dt.Rows[0]["USACODREPRE"].ToString() == "N")
                {
                    psLookup15.Enabled = false;
                    psLookup15.Chave = false;
                }

                #endregion

                #region Vendedor

                if (dt.Rows[0]["USACODVENDEDOR"].ToString() == "N")
                {
                    psLookup26.Enabled = false;
                    psLookup26.Chave = false;
                }

                #endregion

                #region Centro de Custo

                if (dt.Rows[0]["USACODCCUSTO"].ToString() == "N")
                {
                    psLookup13.Enabled = false;
                    psLookup13.Chave = false;
                }

                #endregion

                #region Natureza Orçamentária

                if (dt.Rows[0]["USACODNATUREZAORCAMENTO"].ToString() == "N")
                {
                    psLookup16.Enabled = false;
                    psLookup16.Chave = false;
                }

                #endregion

                #region Local 1

                if (dt.Rows[0]["LOCAL1"].ToString() == "N")
                {
                    psLookup7.Enabled = false;
                    psLookup7.Chave = false;
                }

                #endregion

                #region Filial Entrega

                if (dt.Rows[0]["OPERESTOQUE2"].ToString() == "N")
                {
                    psLookup17.Enabled = false;
                    psLookup17.Chave = false;
                }

                #endregion

                #region Local 2

                if (dt.Rows[0]["LOCAL2"].ToString() == "N")
                {
                    psLookup9.Enabled = false;
                    psLookup9.Chave = false;
                }

                #endregion

                #region Transporte

                if (dt.Rows[0]["USAABATRANSP"].ToString() == "0")
                {
                    tabControl1.TabPages.Remove(tabTRANSP);
                }

                #endregion

                #region Valor Bruto

                if (dt.Rows[0]["USAVALORBRUTO"].ToString() == "N")
                {
                    psValorBruto.Enabled = false;
                }


                #endregion

                #region Valor Liquido

                if (dt.Rows[0]["USAVALORLIQUIDO"].ToString() == "N")
                {
                    psValorLiquido.Enabled = false;
                }

                #endregion

                #region Data Emissão

                if (dt.Rows[0]["USADATAEMISSAO"].ToString() == "N")
                {
                    dtEmissao.Enabled = false;
                }

                #endregion

                #region Data Entrega

                if (dt.Rows[0]["USADATAENTREGA"].ToString() == "N")
                {
                    dtEntrega.Enabled = false;
                }

                #endregion

                #region Data Entrada/Saida

                if (dt.Rows[0]["USADATAENTSAI"].ToString() == "N")
                {
                    dtEntSai.Enabled = false;
                    tHoraSai.Enabled = false;
                }

                #endregion

                #region Rateio Centro de Custo

                if (dt.Rows[0]["USARATEIOCC"].ToString() == "0")
                {
                    tabControl2.TabPages.Remove(tabRATCC);
                }

                #endregion

                #region Rateio Departamento

                if (dt.Rows[0]["USARATEIODP"].ToString() == "0")
                {
                    tabControl2.TabPages.Remove(tabRATDP);
                }

                #endregion

                #region Rateio

                if (dt.Rows[0]["USARATEIOCC"].ToString() == "0" && dt.Rows[0]["USARATEIODP"].ToString() == "0")
                {
                    tabControl1.TabPages.Remove(tabRAT);
                }

                #endregion

                #region Bloqueio Desconto

                if (dt.Rows[0]["USASELECAOBLOQUEIODESCONTO"].ToString() == "N")
                {
                    psComboBox4.Enabled = false;
                    psComboBox4.Chave = false;
                }

                #endregion

                #region Frete

                if (dt.Rows[0]["USAVALORFRETE"].ToString() == "N")
                {
                    psValorFrete.Enabled = false;
                    psPercFrete.Enabled = false;
                }

                #endregion

                #region Desconto

                if (dt.Rows[0]["USAVALORDESCONTO"].ToString() == "N")
                {
                    psValorDesconto.Enabled = false;
                    psPercDesconto.Enabled = false;
                }

                #endregion

                #region Despesa

                if (dt.Rows[0]["USAVALORDESPESA"].ToString() == "N")
                {
                    psValorDespesa.Enabled = false;
                    psPercDespesa.Enabled = false;
                }

                #endregion

                #region Seguro

                if (dt.Rows[0]["USAVALORSEGURO"].ToString() == "N")
                {
                    psValorSeguro.Enabled = false;
                    psPercSeguro.Enabled = false;
                }

                #endregion

                #region Aba Observação

                //Aba Observação
                if (dt.Rows[0]["USAABAOBSERV"].ToString() == "0")
                {
                    tabControl1.TabPages.Remove(tabOBSERV);
                }

                #endregion

                #region mask

                MASKNUMEROSEQ = Convert.ToInt16(dt.Rows[0]["MASKNUMEROSEQ"]);

                #endregion

                #region Tipo Entra / Sai

                operTipEntSai = dt.Rows[0]["TIPENTSAI"].ToString();

                #endregion

                #region NFE
                if (dt.Rows[0]["USACAMPONFE"].ToString() == "S")
                {
                    cmbNfe.Enabled = true;
                    psTextoBox8.Enabled = true;
                }
                else
                {
                    cmbNfe.Enabled = false;
                    psTextoBox8.Enabled = false;
                }
                #endregion

                #region Segundo Número

                if (Convert.ToBoolean(dt.Rows[0]["USASEGUNDONUMERO"]) == true)
                {
                    DataTable dtSegundoNumero = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT SEGUNDONUMERO, QTDSEGUNDONUMERO, SEQSEGUNDONUMERO FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, codFilial });

                    if (Convert.ToBoolean(dtSegundoNumero.Rows[0]["SEQSEGUNDONUMERO"]) == true)
                    {
                        txtSegundoNumero.Enabled = false;
                        if (edita == false)
                        {
                            txtSegundoNumero.textBox1.Text = Convert.ToString(Convert.ToInt32(dtSegundoNumero.Rows[0]["SEGUNDONUMERO"]) + 1);
                            txtSegundoNumero.textBox1.Text = AppLib.Util.Format.CompletarZeroEsquerda(Convert.ToInt32(dtSegundoNumero.Rows[0]["QTDSEGUNDONUMERO"]), txtSegundoNumero.textBox1.Text);
                        }

                    }
                    else
                    {
                        if (dtSegundoNumero.Rows[0]["QTDSEGUNDONUMERO"].ToString() == string.Empty)
                        {
                            txtSegundoNumero.textBox1.MaxLength = 15;
                        }
                        else
                        {
                            txtSegundoNumero.textBox1.MaxLength = Convert.ToInt32(dtSegundoNumero.Rows[0]["QTDSEGUNDONUMERO"]);
                        }

                    }
                }
                else
                {
                    txtSegundoNumero.Enabled = false;
                }
                #endregion
            }
            #endregion
        }

        private string getNovoNumero()
        {
            string novoNumStr = string.Empty;
            if (psTextoBox2.Edita == false)
            {
                int ultimo = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT NUMSEQ FROM VSERIE WHERE CODSERIE = ? AND CODFILIAL = ? AND CODEMPRESA = ?", new object[] { psComboBox5.Value, codFilial, AppLib.Context.Empresa }));
                ultimo++;
                int tamanho = ultimo.ToString().Length;
                novoNumStr = string.Concat(ultimo.ToString().PadLeft(MASKNUMEROSEQ, '0'));
                if (novoNumStr.Length > MASKNUMEROSEQ)
                {
                    return string.Empty;
                }

                psTextoBox2.textBox1.Text = novoNumStr;
                return novoNumStr;
            }
            //if (!string.IsNullOrEmpty(psTextoBox2.textBox1.Text))
            //{
            //    novoNumStr = psTextoBox2.textBox1.Text;
            //}
            //else
            //{

            //}           

            return psTextoBox2.textBox1.Text;
        }

        private bool validaNumeroOperacao()
        {

            int Usanumeroorigem = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT USANUMEROORIGEM FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, psCodTipOper.textBox1.Text }));

            if (Usanumeroorigem == 1)
            {
                string NumeroDoc = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NUMERO FROM GOPER WHERE CODOPER IN ( SELECT CODOPER FROM GOPERRELAC WHERE CODEMPRESA = ? AND CODOPERRELAC = ?)", new object[] { AppLib.Context.Empresa, codoper }).ToString();
                psTextoBox2.Text = NumeroDoc;
            }

            //if (edita == false)
            //{
            //    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT USANUMEROSEQ, SERIELIVRE FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, psCodTipOper.textBox1.Text });
            //    int usaNumSeq = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT USANUMEROSEQ FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, psCodTipOper.textBox1.Text }));
            //    //if ((Convert.ToInt32(dt.Rows[0]["USANUMEROSEQ"]) == 0) && (Convert.ToInt32(dt.Rows[0]["SERIELIVRE"]) == 0))
            //    //{
            //        // João Pedro Luchiari 14/11/2017
            //        //string NumeroDoc = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NUMERO FROM GOPER WHERE CODOPER IN ( SELECT CODOPER FROM GOPERRELAC WHERE CODEMPRESA = ? AND CODOPERRELAC = ?)", new object[] { AppLib.Context.Empresa, codoper }).ToString();
            //        //psTextoBox2.Text = NumeroDoc;
            //        return false;
            //    //}
            //    numero = getNovoNumero();
            //}
            int usaNumSeq = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT USANUMEROSEQ FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, psCodTipOper.textBox1.Text }));
            if (usaNumSeq == 0)
            {
                // João Pedro Luchiari 14/11/2017
                return false;
            }
            numero = getNovoNumero();

            if (!string.IsNullOrEmpty(numero))
            {
                string sSql = string.Empty;
                string sSerie = string.Empty;
                string sCodCliFor = string.Empty;

                if (psComboBox5.Value != DBNull.Value)
                {
                    if (psComboBox5.Value != null)
                    {
                        codSerie = psComboBox5.Value.ToString();
                    }
                    else
                    {
                        codSerie = txtCodserie.textBox1.Text;
                    }
                }
                else
                {
                    codSerie = txtCodserie.textBox1.Text;
                }


                if (string.IsNullOrEmpty(codSerie))
                {
                    codSerie = GLB_PSTEXTBOX_SERIE.textBox1.Text;
                }

                sSql = @"SELECT CODOPER FROM GOPER WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPER <> ? AND NUMERO = ? ";

                if (psComboBox5.Text != "M")
                {
                    sSerie = " AND CODSERIE = ? ";
                }

                if (operCodCliFor != "M")
                {
                    sCodCliFor = " AND CODCLIFOR = ? ";
                }

                if (string.IsNullOrEmpty(sSerie))
                {
                    if (string.IsNullOrEmpty(sCodCliFor))
                    {
                        return dbs.QueryFind(sSql, AppLib.Context.Empresa, codFilial, psTextoBox1.textBox1.Text, numero);
                    }
                    else
                    {
                        if (operTipEntSai == "E")
                        {
                            sSql = string.Concat(sSql, sCodCliFor);
                            return dbs.QueryFind(sSql, AppLib.Context.Empresa, codFilial, psTextoBox1.textBox1.Text, numero, newLookup1.txtcodigo.Text);
                        }
                        else
                        {
                            sSql = string.Concat(sSql);
                            return dbs.QueryFind(sSql, AppLib.Context.Empresa, codFilial, psTextoBox1.textBox1.Text, numero);
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(sCodCliFor))
                    {
                        sSql = string.Concat(sSql, sSerie);
                        return dbs.QueryFind(sSql, AppLib.Context.Empresa, codFilial, psTextoBox1.textBox1.Text, numero, codSerie);
                    }
                    else
                    {
                        if (operTipEntSai == "E")
                        {
                            sSql = string.Concat(sSql, sSerie, sCodCliFor);
                            return dbs.QueryFind(sSql, AppLib.Context.Empresa, codFilial, psTextoBox1.textBox1.Text, numero, codSerie, newLookup1.txtcodigo.Text);
                        }
                        else
                        {
                            sSql = string.Concat(sSql, sSerie);
                            return dbs.QueryFind(sSql, AppLib.Context.Empresa, codFilial, psTextoBox1.textBox1.Text, numero, codSerie);
                        }
                    }
                }
            }
            return true;
        }

        private void verificaDesconto(string codTipoOper, string codCliFor, int codOper, AppLib.Data.Connection connection)
        {

            string usaBloq = connection.ExecGetField(string.Empty, @"SELECT USABLOQUEIODESCONTO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipoOper }).ToString();
            if (usaBloq == "S")
            {
                string tipoBloq = connection.ExecGetField(string.Empty, @"SELECT TIPOBLOQUEIODESC FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codOper }).ToString();
                if (tipoBloq == "T")
                {
                    bloqueioTotal(codTipoOper, codCliFor, codOper, connection);
                }
                else
                {
                    bloqueioItem(codOper, connection);
                }
            }
        }

        private void bloqueioItem(int codOper, AppLib.Data.Connection connection)
        {
            DataTable dt = connection.ExecQuery(@"SELECT LIMITEDESC, GOPERITEM.PRDESCONTO FROM GOPER INNER JOIN GOPERITEM ON GOPER.CODOPER = GOPERITEM.CODOPER AND GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToDecimal(dt.Rows[i]["LIMITEDESC"]) < Convert.ToDecimal(dt.Rows[i]["PRDESCONTO"]))
                {
                    connection.ExecTransaction(@"UPDATE GOPER SET CODSTATUS = 7 WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa });
                    return;
                }
            }
        }

        private void bloqueioTotal(string codTipoOper, string codCliFor, int codOper, AppLib.Data.Connection connection)
        {

            decimal compra = 0, venda = 0, valor = 0;
            string tipoEntrada = connection.ExecGetField(string.Empty, @"SELECT TIPENTSAI FROM GTIPOPER WHERE CODTIPOPER = ?", new object[] { codTipoOper }).ToString();
            if (!string.IsNullOrEmpty(tipoEntrada))
            {
                DataTable dt = connection.ExecQuery(@"SELECT DESCMAXCOMPRA, DESCMAXVENDA FROM VCLIFOR WHERE CODCLIFOR = ?", new object[] { codCliFor });
                if (dt.Rows.Count > 0)
                {
                    valor = Convert.ToDecimal(connection.ExecGetField(0, @"	SELECT 
	((SUM(GOPERITEM.VLDESCONTO) / (CASE WHEN (GOPER.VALORBRUTO = 0) THEN 1 ELSE GOPER.VALORBRUTO END) ) * 100) PERCDESCONTO
	FROM 
	GOPERITEM 
	INNER JOIN GOPER ON GOPERITEM.CODOPER = GOPER.CODOPER AND GOPERITEM.CODEMPRESA = GOPER.CODEMPRESA
	WHERE 
	GOPERITEM.CODOPER = ? 
	GROUP BY 
	VALORBRUTO", new object[] { codOper }));

                    if (tipoEntrada == "E")
                    {
                        compra = Convert.ToDecimal(dt.Rows[0]["DESCMAXCOMPRA"]);
                        if (valor > compra)
                        {
                            // Altera o status da operação para BLOQUEADO - 7
                            connection.ExecTransaction(@"UPDATE GOPER SET CODSTATUS = 7 WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa });
                        }
                    }
                    else if (tipoEntrada == "S")
                    {
                        venda = Convert.ToDecimal(dt.Rows[0]["DESCMAXVENDA"]);
                        if (valor > venda)
                        {
                            // Altera o status da operação para BLOQUEADO - 7
                            connection.ExecTransaction(@"UPDATE GOPER SET CODSTATUS = 7 WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa });
                        }
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
                GC.Collect();
            }
            else
            {
                MessageBox.Show("Não foi possível concluir a operação.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            Edita();

        }

        private void Edita()
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                if (editaItem == true)
                {

                    PSPartOperacaoItemEdit frm = new PSPartOperacaoItemEdit();
                    frm.codOper = Convert.ToInt32(psTextoBox1.textBox1.Text);
                    frm.consumoFinal = chkTipOperConsFin.Checked;
                    frm.codTipOper = psCodTipOper.textBox1.Text;
                    frm.edita = true;
                    frm.codStatusOper = psComboBox1.Text;
                    DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    frm.nseqitem = Convert.ToInt32(row["GOPERITEM.NSEQITEM"]);
                    frm.aprovado = aprovado;
                    frm.ValidaBotao = DesabilitaBotao;
                    frm.ShowDialog();
                    carregaItens();
                    calculaOperacao(frm.codOper, frm.codTipOper);
                    //carregaMensagemIbptax();
                    //carregaDispositivoLegal();
                    item = true;
                    if (frm.editado == true)
                    {
                        btnCancelarAtual.Enabled = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Favor selecionar um item para editar.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void carregaDispositivoLegal()
        {
            psMemoBox2.textBox1.Text = string.Empty;

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT DISTINCT VOPERMENSAGEM.DESCRICAO
FROM GOPERITEM 
INNER JOIN VNATUREZA ON GOPERITEM.CODNATUREZA = VNATUREZA.CODNATUREZA AND GOPERITEM.CODEMPRESA = VNATUREZA.CODEMPRESA 
INNER JOIN VOPERMENSAGEM ON VNATUREZA.CODMENSAGEM = VOPERMENSAGEM.CODMENSAGEM 
WHERE GOPERITEM.CODOPER = ? AND GOPERITEM.CODEMPRESA = ?", new object[] { psTextoBox1.textBox1.Text, AppLib.Context.Empresa });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    psMemoBox2.textBox1.Text = dt.Rows[i]["DESCRICAO"].ToString();
                }
                else
                {
                    psMemoBox2.textBox1.Text = psMemoBox2.textBox1.Text + " " + dt.Rows[i]["DESCRICAO"].ToString();
                }
            }
        }

        public void calculaOperacao(int codOper, string codTipOper)
        {
            PS.Lib.Contexto.Session.key1 = AppLib.Context.Empresa;
            PS.Lib.Contexto.Session.key2 = codOper;
            PS.Lib.Contexto.Session.key3 = null;
            PS.Lib.Contexto.Session.key4 = null;
            PS.Lib.Contexto.Session.key5 = null;

            int FRM_CODEMPRESA = 0;
            string FRM_CODFORMULA = string.Empty;
            string FRM_CODFORMULAVALORBRUTO = string.Empty;
            string FRM_CODFORMULAVALORLIQUIDO = string.Empty;
            decimal OP_VALORBRUTO = 0;
            decimal OP_VALORLIQUIDO = 0;

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODFORMULAVALORBRUTO, CODFORMULAVALORLIQUIDO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper });

            if (dt.Rows.Count > 0)
            {
                FRM_CODEMPRESA = AppLib.Context.Empresa;
                FRM_CODFORMULAVALORBRUTO = dt.Rows[0]["CODFORMULAVALORBRUTO"].ToString();
                FRM_CODFORMULAVALORLIQUIDO = dt.Rows[0]["CODFORMULAVALORLIQUIDO"].ToString();
                try
                {
                    interpreta.comando = function.PreparaFormula(FRM_CODEMPRESA, FRM_CODFORMULAVALORBRUTO);
                    OP_VALORBRUTO = Convert.ToDecimal(interpreta.Executar().ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao executar a fórmula: " + FRM_CODFORMULAVALORBRUTO + ".\r\n" + ex.Message);
                }
                try
                {
                    interpreta.comando = function.PreparaFormula(FRM_CODEMPRESA, FRM_CODFORMULAVALORLIQUIDO);
                    OP_VALORLIQUIDO = Convert.ToDecimal(interpreta.Executar().ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao executar a fórmula: " + FRM_CODFORMULAVALORLIQUIDO + ".\r\n" + ex.Message);
                }
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GOPER SET VALORBRUTO = ?, VALORLIQUIDO = ? WHERE CODEMPRESA = ? AND CODOPER = ?", OP_VALORBRUTO, OP_VALORLIQUIDO, AppLib.Context.Empresa, codOper);

            }



            PS.Lib.Contexto.Session.key1 = null;
            PS.Lib.Contexto.Session.key2 = null;
            PS.Lib.Contexto.Session.key3 = null;
            PS.Lib.Contexto.Session.key4 = null;
            PS.Lib.Contexto.Session.key5 = null;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir o item?", "Informação do Sistema.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                    PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                    psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
                    psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };
                    psPartLocalEstoqueSaldoData.MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemExclusao, AppLib.Context.Empresa, Convert.ToInt32(psTextoBox1.textBox1.Text), Convert.ToInt32(row["GOPERITEM.NSEQITEM"]));

                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GOPERITEMCOMPL WHERE CODOPER = ? AND CODEMPRESA = ? AND NSEQITEM = ?", new object[] { psTextoBox1.textBox1.Text, AppLib.Context.Empresa, row["GOPERITEM.NSEQITEM"] });
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GOPERITEMTRIBUTO WHERE CODOPER = ? AND CODEMPRESA = ? AND NSEQITEM = ?", new object[] { psTextoBox1.textBox1.Text, AppLib.Context.Empresa, row["GOPERITEM.NSEQITEM"] });
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GOPERITEMDIFAL WHERE CODOPER = ? AND CODEMPRESA = ? AND NSEQITEM = ?", new object[] { psTextoBox1.textBox1.Text, AppLib.Context.Empresa, row["GOPERITEM.NSEQITEM"] });
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM VFICHAESTOQUE WHERE CODOPER = ? AND CODEMPRESA = ? AND NSEQITEM = ? AND CODPRODUTO = ?", new object[] { psTextoBox1.textBox1.Text, AppLib.Context.Empresa, row["GOPERITEM.NSEQITEM"], row["GOPERITEM.CODPRODUTO"] });
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GOPERITEM WHERE CODOPER = ? AND CODEMPRESA = ? AND NSEQITEM = ?", new object[] { psTextoBox1.textBox1.Text, AppLib.Context.Empresa, row["GOPERITEM.NSEQITEM"] });

                }
                carregaItens();
                calculaOperacao(Convert.ToInt32(psTextoBox1.textBox1.Text), psCodTipOper.textBox1.Text);
                AtualizaValores();
            }
        }

        private void deleteFlanca(string codoper, AppLib.Data.Connection conn)
        {
            DataTable compl = conn.ExecQuery("SELECT CODLANCA FROM FLANCA WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codoper, AppLib.Context.Empresa });
            for (int i = 0; i < compl.Rows.Count; i++)
            {
                conn.ExecTransaction("DELETE FROM FLANCACOMPL WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { compl.Rows[i]["CODLANCA"], AppLib.Context.Empresa });
                conn.ExecTransaction("DELETE FROM FLANCARATEIOCC WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { compl.Rows[i]["CODLANCA"], AppLib.Context.Empresa });
                conn.ExecTransaction("DELETE FROM FLANCARATEIODP WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { compl.Rows[i]["CODLANCA"], AppLib.Context.Empresa });
                conn.ExecTransaction("DELETE FROM FLANCARELAC WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { compl.Rows[i]["CODLANCA"], AppLib.Context.Empresa });
                conn.ExecTransaction("DELETE FROM FRELLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { compl.Rows[i]["CODLANCA"], AppLib.Context.Empresa });
            }

            conn.ExecTransaction("DELETE FROM FLANCA WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codoper, AppLib.Context.Empresa });
        }

        public void geraFinanceiro(string _codTipOper, int _codOper)
        {

            DataRow PARAMTIPOPERDESTINO = null;

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT GERAFINANCEIRO, CODMOEDA, CODTIPDOC, TIPOPAGREC, CODCONTA, CODFORMAPADRAO, BASEVENCIMENTO FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { _codTipOper, AppLib.Context.Empresa });

            if (dt.Rows.Count > 0)
            {
                PARAMTIPOPERDESTINO = dt.Rows[0];
            }

            if (PARAMTIPOPERDESTINO == null)
            {
                throw new Exception("Não foi possível carregar os parâmetros do Tipo da Operação [" + _codTipOper + "].");
            }
            else
            {
                if (Convert.ToInt32(PARAMTIPOPERDESTINO["GERAFINANCEIRO"]) == 1)
                {
                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();

                    DataTable GOPERORIGEM = conn.ExecQuery(@"SELECT GOPER.CODOPER, GOPER.CODCONDICAO, GOPER.CODCLIFOR, GOPER.CODFILIAL, GOPER.DATAEMISSAO, GOPER.DATAENTREGA, GOPER.DATAENTSAI, GOPER.CODCCUSTO, GOPER.CODNATUREZAORCAMENTO, GOPER.CODREPRE, GOPER.OBSERVACAO, GOPER.CODCONTA, GOPER.CODFORMA, GOPER.VALORLIQUIDO, GOPER.NUMERO, VCLIFOR.NOMEFANTASIA, GOPER.CODVENDEDOR FROM GOPER INNER JOIN VCLIFOR ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR WHERE  GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ?
", new object[] { AppLib.Context.Empresa, _codOper });

                    if (!string.IsNullOrEmpty(GOPERORIGEM.Rows[0]["CODCONDICAO"].ToString()))
                    {
                        deleteFlanca(GOPERORIGEM.Rows[0]["CODOPER"].ToString(), conn);

                        DataTable COMPOSICAOPAGTO = conn.ExecQuery("SELECT * FROM VCONDICAOPGTOCOMPOSICAO WHERE CODEMPRESA = ? AND CODCONDICAO = ?", new object[] { AppLib.Context.Empresa, GOPERORIGEM.Rows[0]["CODCONDICAO"] });
                        if (COMPOSICAOPAGTO.Rows.Count > 0)
                        {
                            int[] listCODLANCAOLD;
                            string[] listNUMEROOLD;
                            int QtdLancaOld = 0;
                            int cont = 0;
                            DataTable FLANCA = conn.ExecQuery(@"SELECT CODLANCA, NUMERO FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ? AND CODSTATUS <> 2", new object[] { AppLib.Context.Empresa, _codOper });
                            if (FLANCA.Rows.Count > 0)
                            {
                                listCODLANCAOLD = new int[FLANCA.Rows.Count];
                                listNUMEROOLD = new string[FLANCA.Rows.Count];
                                QtdLancaOld = FLANCA.Rows.Count;
                                foreach (DataRow rLanca in FLANCA.Rows)
                                {
                                    listCODLANCAOLD[cont] = Convert.ToInt32(rLanca["CODLANCA"]);
                                    listNUMEROOLD[cont] = rLanca["NUMERO"].ToString();
                                    cont++;
                                }
                            }
                            else
                            {
                                listCODLANCAOLD = new int[0];
                                listNUMEROOLD = new string[0];
                                QtdLancaOld = 0;
                            }

                            #region Sets



                            conn.BeginTransaction();
                            try
                            {


                                AppLib.ORM.Jit LANCA = new AppLib.ORM.Jit(conn, "FLANCA");
                                LANCA.Set("CODEMPRESA", AppLib.Context.Empresa);

                                LANCA.Set("CODOPER", GOPERORIGEM.Rows[0]["CODOPER"]);
                                LANCA.Set("CODCLIFOR", GOPERORIGEM.Rows[0]["CODCLIFOR"]);
                                LANCA.Set("NOMECLIENTE", GOPERORIGEM.Rows[0]["NOMEFANTASIA"]);
                                LANCA.Set("CODFILIAL", GOPERORIGEM.Rows[0]["CODFILIAL"]);
                                LANCA.Set("DATAEMISSAO", GOPERORIGEM.Rows[0]["DATAEMISSAO"]);


                                LANCA.Set("NSEQLANCA", 1);


                                LANCA.Set("CODMOEDA", PARAMTIPOPERDESTINO["CODMOEDA"]);
                                LANCA.Set("CODTIPDOC", PARAMTIPOPERDESTINO["CODTIPDOC"]);
                                LANCA.Set("TIPOPAGREC", PARAMTIPOPERDESTINO["TIPOPAGREC"]);
                                LANCA.Set("CODCCUSTO", GOPERORIGEM.Rows[0]["CODCCUSTO"]);
                                LANCA.Set("CODNATUREZAORCAMENTO", GOPERORIGEM.Rows[0]["CODNATUREZAORCAMENTO"]);
                                LANCA.Set("CODSTATUS", 0);
                                LANCA.Set("CODREPRE", GOPERORIGEM.Rows[0]["CODREPRE"]);
                                LANCA.Set("OBSERVACAO", GOPERORIGEM.Rows[0]["OBSERVACAO"]);
                                if (GOPERORIGEM.Rows[0]["CODVENDEDOR"] == DBNull.Value)
                                {
                                    LANCA.Set("CODVENDEDOR", null);
                                }
                                else
                                {
                                    LANCA.Set("CODVENDEDOR", GOPERORIGEM.Rows[0]["CODVENDEDOR"]);
                                }

                                LANCA.Set("ORIGEM", "O");
                                LANCA.Set("NFOUDUP", "3");

                                #region Conta Caixa

                                if (GOPERORIGEM.Rows[0]["CODCONTA"] == DBNull.Value)
                                {
                                    LANCA.Set("CODCONTA", PARAMTIPOPERDESTINO["CODCONTA"]);
                                }
                                else
                                {
                                    LANCA.Set("CODCONTA", GOPERORIGEM.Rows[0]["CODCONTA"]);
                                }

                                #endregion

                                #region Forma de Pagamento

                                if (GOPERORIGEM.Rows[0]["CODFORMA"] == DBNull.Value)
                                {
                                    LANCA.Set("CODFORMA", PARAMTIPOPERDESTINO["CODFORMAPADRAO"]);
                                }
                                else
                                {
                                    LANCA.Set("CODFORMA", GOPERORIGEM.Rows[0]["CODFORMA"]);
                                }

                                #endregion

                                #region Condição de Pagamento

                                DataTable VCONDICAOPGTO = conn.ExecQuery(@"SELECT * FROM VCONDICAOPGTO WHERE CODEMPRESA = ? AND CODCONDICAO = ?", new object[] { AppLib.Context.Empresa, GOPERORIGEM.Rows[0]["CODCONDICAO"] });

                                if (VCONDICAOPGTO.Rows.Count <= 0)
                                {
                                    throw new Exception("Não foi possível carregar os parâmetros da Condição de Pagamento, verifique se a mesma foi informada na operação");
                                }

                                decimal FIN_TAXAJUROS = Convert.ToDecimal(VCONDICAOPGTO.Rows[0]["TAXAJUROS"].ToString());

                                #endregion

                                #region Valida Composição de Pagamento

                                int FIN_NUMPARCELAS = 0;
                                decimal FIN_PERCVALOR = 0;

                                foreach (DataRow rComp in COMPOSICAOPAGTO.Rows)
                                {
                                    FIN_PERCVALOR = FIN_PERCVALOR + Convert.ToDecimal(rComp["PERCVALOR"]);
                                    FIN_NUMPARCELAS = FIN_NUMPARCELAS + Convert.ToInt32(rComp["NUMPARCELAS"]);

                                }

                                if (FIN_PERCVALOR != 100)
                                {
                                    throw new Exception("A soma do percentual do valor da composição da condição de pagamento deve ser 100%.");
                                }

                                if (FIN_NUMPARCELAS < 0)
                                {
                                    throw new Exception("O número de parcelas da composição da condição de pagamento deve ser maior que zero.");
                                }

                                #endregion

                                string FORMULA = conn.ExecGetField(string.Empty, "SELECT GQUERY.QUERY FROM GTIPOPER INNER JOIN GQUERY ON GTIPOPER.CODEMPRESA = GQUERY.CODEMPRESA AND GTIPOPER.FORMULAFINANCEIRO = GQUERY.CODQUERY WHERE GTIPOPER.CODTIPOPER  = ? AND GTIPOPER.CODEMPRESA = ? ", new object[] { _codTipOper, AppLib.Context.Empresa }).ToString();

                                FORMULA = FORMULA.Replace("@CODEMPRESA", "'" + AppLib.Context.Empresa + "'").Replace("@CODOPER", "'" + _codOper + "'");

                                // Usar futuramente?
                                //query = query.Replace("@CODEMPRESA", "'" + AppLib.Context.Empresa + "'").Replace("@CODNATUREZA", "'" + dtItens.Rows[iItens]["CODNATUREZA"] + "'").Replace("@CODTRIBUTO", "'" + dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString() + "'").Replace("@CODOPER", "'" + psTextoBox1.textBox1.Text + "'").Replace("@NSEQITEM", "'" + dtItens.Rows[iItens]["NSEQITEM"] + "'").Replace("@UFDESTINO", "'" + UFDESTINATARIO + "'");

                                decimal OP_VALORLIQUIDO = Convert.ToDecimal(conn.ExecGetField(0, FORMULA, new object[] { }));

                                //Descomentar caso dê erro(21/08/2017- Jepeto)

                                //decimal OP_VALORLIQUIDO = Convert.ToDecimal(GOPERORIGEM.Rows[0]["VALORLIQUIDO"]);

                                //decimal vl_funRural = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT SUM(VALOR) VALOR FROM GOPERITEMTRIBUTO WHERE CODOPER = ?  AND CODEMPRESA = ? AND CODTRIBUTO = 'FUNRURAL'", new object[] { GOPERORIGEM.Rows[0]["CODOPER"], AppLib.Context.Empresa }));
                                //if (vl_funRural > 0)
                                //{
                                //    OP_VALORLIQUIDO = OP_VALORLIQUIDO - vl_funRural;
                                //}

                                //decimal OP_VALORLIQUIDO = Convert.ToDecimal(FORMULA);

                                if (OP_VALORLIQUIDO > 0)
                                {
                                    decimal FIN_VALORORIGINAL = 0;

                                    decimal FIN_PERCJUROS = FIN_TAXAJUROS;
                                    decimal FIN_VALORJUROS = 0;

                                    decimal FIN_PERCDESCONTO = 0;
                                    decimal FIN_VALORDESCONTO = 0;

                                    decimal FIN_PERCMULTA = 0;
                                    decimal FIN_VALORMULTA = 0;

                                    decimal FIN_VALORCOMPOSICAOPARCELA = 0;
                                    decimal FIN_VALORLIQUIDOPARCELA = 0;

                                    string FIN_NUMERODOCUMENTO = GOPERORIGEM.Rows[0]["NUMERO"].ToString();
                                    int FIN_NSEQPARCELA = 0;
                                    int FIN_NUMPARCOMP = 0;
                                    int FIN_NUMPRAZO = 0;
                                    int FIN_NUMINTERVALO = 0;


                                    string FIN_NUMERO;
                                    string FIN_TIPO = "N";
                                    DateTime FIN_DATAVENCIMENTO;

                                    switch (dt.Rows[0]["BASEVENCIMENTO"].ToString())
                                    {
                                        case "0":
                                            FIN_DATAVENCIMENTO = Convert.ToDateTime(GOPERORIGEM.Rows[0]["DATAEMISSAO"]);
                                            break;
                                        case "1":
                                            FIN_DATAVENCIMENTO = Convert.ToDateTime(GOPERORIGEM.Rows[0]["DATAENTREGA"]);
                                            break;
                                        default:
                                            FIN_DATAVENCIMENTO = Convert.ToDateTime(GOPERORIGEM.Rows[0]["DATAENTSAI"]);
                                            break;
                                    }

                                    DateTime FIN_DATAPREVBAIXA = FIN_DATAVENCIMENTO;
                                    FIN_NUMPARCELAS = 0;

                                    foreach (DataRow rComposicao in COMPOSICAOPAGTO.Rows)
                                    {

                                        FIN_PERCVALOR = Convert.ToDecimal(rComposicao["PERCVALOR"]);
                                        FIN_NUMPARCOMP = Convert.ToInt32(rComposicao["NUMPARCELAS"]);
                                        FIN_NUMPRAZO = Convert.ToInt32(rComposicao["NUMPRAZO"]);
                                        FIN_NUMINTERVALO = Convert.ToInt32(rComposicao["NUMINTERVALO"]);
                                        FIN_TIPO = rComposicao["TIPO"].ToString();

                                        if (FIN_PERCVALOR == 100)
                                            FIN_VALORCOMPOSICAOPARCELA = OP_VALORLIQUIDO;
                                        else
                                            FIN_VALORCOMPOSICAOPARCELA = ((OP_VALORLIQUIDO * FIN_PERCVALOR) / 100);

                                        for (int i = 0; i < FIN_NUMPARCOMP; i++)
                                        {
                                            FIN_NSEQPARCELA = FIN_NSEQPARCELA + 1;
                                            int FIN_CODLANCA = Convert.ToInt32(conn.ExecGetField(0, "SELECT IDLOG FROM GLOG WHERE CODTABELA = 'FLANCA' AND CODEMPRESA = ?", new object[] { AppLib.Context.Empresa })) + 1;
                                            if (QtdLancaOld > 0)
                                            {
                                                // FIN_CODLANCA = listCODLANCAOLD[FIN_NUMPARCELAS];
                                                FIN_NUMERO = listNUMEROOLD[FIN_NUMPARCELAS];
                                                QtdLancaOld--;
                                            }
                                            else
                                            {
                                                // FIN_CODLANCA = 0;
                                                FIN_NUMERO = string.Concat(FIN_NUMERODOCUMENTO, "/", FIN_NSEQPARCELA.ToString().PadLeft(2, '0'));
                                            }

                                            #region Valor


                                            FIN_VALORORIGINAL = gb.Arredonda((FIN_VALORCOMPOSICAOPARCELA / FIN_NUMPARCOMP), 2);


                                            if ((FIN_VALORORIGINAL * FIN_NUMPARCOMP) != FIN_VALORCOMPOSICAOPARCELA)
                                            {
                                                if (i == 0)
                                                {
                                                    if ((FIN_VALORORIGINAL * FIN_NUMPARCOMP) > FIN_VALORCOMPOSICAOPARCELA)
                                                    {
                                                        FIN_VALORORIGINAL = FIN_VALORORIGINAL - ((FIN_VALORORIGINAL * FIN_NUMPARCOMP) - FIN_VALORCOMPOSICAOPARCELA);
                                                    }
                                                    else
                                                    {
                                                        FIN_VALORORIGINAL = FIN_VALORORIGINAL + (FIN_VALORCOMPOSICAOPARCELA - (FIN_VALORORIGINAL * FIN_NUMPARCOMP));
                                                    }
                                                }
                                            }

                                            #endregion

                                            #region Prazo

                                            if (i == 0)
                                            {
                                                if (COMPOSICAOPAGTO.Rows[0]["NUMPARCELAS"].ToString() == "1")
                                                {
                                                    switch (dt.Rows[0]["BASEVENCIMENTO"].ToString())
                                                    {
                                                        case "0":
                                                            FIN_DATAVENCIMENTO = Convert.ToDateTime(GOPERORIGEM.Rows[0]["DATAEMISSAO"]);
                                                            break;
                                                        case "1":
                                                            FIN_DATAVENCIMENTO = Convert.ToDateTime(GOPERORIGEM.Rows[0]["DATAENTREGA"]);
                                                            break;
                                                        default:
                                                            FIN_DATAVENCIMENTO = Convert.ToDateTime(GOPERORIGEM.Rows[0]["DATAENTSAI"]);
                                                            break;
                                                    }
                                                }
                                                FIN_DATAVENCIMENTO = Convert.ToDateTime(FIN_DATAVENCIMENTO).AddDays(FIN_NUMPRAZO);
                                                FIN_DATAPREVBAIXA = FIN_DATAVENCIMENTO;
                                            }
                                            else
                                            {
                                                FIN_DATAVENCIMENTO = Convert.ToDateTime(FIN_DATAVENCIMENTO).AddDays(FIN_NUMINTERVALO);
                                                FIN_DATAPREVBAIXA = FIN_DATAVENCIMENTO;
                                            }

                                            //Nomal
                                            if (FIN_TIPO == "N")
                                            {

                                            }

                                            //Fora Semana
                                            if (FIN_TIPO == "S")
                                            {
                                                FIN_DATAVENCIMENTO = Convert.ToDateTime(FIN_DATAVENCIMENTO).AddDays((7 - (int)Convert.ToDateTime(FIN_DATAVENCIMENTO).DayOfWeek) + 1).AddDays(FIN_NUMPRAZO);
                                            }

                                            //Fora Dezena
                                            if (FIN_TIPO == "D")
                                            {
                                                if (Convert.ToDateTime(FIN_DATAVENCIMENTO).Day <= 10)
                                                {
                                                    FIN_DATAVENCIMENTO = new DateTime(Convert.ToDateTime(FIN_DATAVENCIMENTO).Year, Convert.ToDateTime(FIN_DATAVENCIMENTO).Month, 11).AddDays(FIN_NUMPRAZO);
                                                }
                                                else
                                                {
                                                    if (Convert.ToDateTime(FIN_DATAVENCIMENTO).Day > 10 && Convert.ToDateTime(FIN_DATAVENCIMENTO).Day <= 20)
                                                    {
                                                        FIN_DATAVENCIMENTO = new DateTime(Convert.ToDateTime(FIN_DATAVENCIMENTO).Year, Convert.ToDateTime(FIN_DATAVENCIMENTO).Month, 21).AddDays(FIN_NUMPRAZO);
                                                    }
                                                    else
                                                    {
                                                        FIN_DATAVENCIMENTO = new DateTime(Convert.ToDateTime(FIN_DATAVENCIMENTO).Year, Convert.ToDateTime(FIN_DATAVENCIMENTO).Month, 1).AddMonths(1).AddDays(FIN_NUMPRAZO);
                                                    }
                                                }
                                            }

                                            //Fora Quinzena
                                            if (FIN_TIPO == "Q")
                                            {
                                                if (Convert.ToDateTime(FIN_DATAVENCIMENTO).Day <= 15)
                                                {
                                                    FIN_DATAVENCIMENTO = new DateTime(Convert.ToDateTime(FIN_DATAVENCIMENTO).Year, Convert.ToDateTime(FIN_DATAVENCIMENTO).Month, 16).AddDays(FIN_NUMPRAZO);
                                                }
                                                else
                                                {
                                                    FIN_DATAVENCIMENTO = new DateTime(Convert.ToDateTime(FIN_DATAVENCIMENTO).Year, Convert.ToDateTime(FIN_DATAVENCIMENTO).Month, 1).AddMonths(1).AddDays(FIN_NUMPRAZO);
                                                }
                                            }

                                            //Fora Mês
                                            if (FIN_TIPO == "M")
                                            {
                                                FIN_DATAVENCIMENTO = new DateTime(Convert.ToDateTime(FIN_DATAVENCIMENTO).Year, Convert.ToDateTime(FIN_DATAVENCIMENTO).Month, 1).AddMonths(1).AddDays(FIN_NUMPRAZO);
                                            }

                                            //Fora Ano
                                            if (FIN_TIPO == "A")
                                            {
                                                FIN_DATAVENCIMENTO = new DateTime(Convert.ToDateTime(FIN_DATAVENCIMENTO).Year, 1, 1).AddYears(1).AddDays(FIN_NUMPRAZO);
                                            }

                                            #endregion

                                            #region Juros

                                            if (FIN_TAXAJUROS > 0)
                                            {
                                                FIN_VALORJUROS = ((FIN_VALORORIGINAL * FIN_TAXAJUROS) / 100);
                                            }

                                            #endregion

                                            #region Valor Liquido

                                            FIN_VALORLIQUIDOPARCELA = (FIN_VALORORIGINAL + FIN_VALORMULTA + FIN_VALORJUROS) - FIN_VALORDESCONTO;

                                            #endregion



                                            LANCA.Set("CODLANCA", FIN_CODLANCA);
                                            LANCA.Set("DATAVENCIMENTO", FIN_DATAPREVBAIXA);
                                            LANCA.Set("DATAPREVBAIXA", FIN_DATAPREVBAIXA);
                                            LANCA.Set("NUMERO", FIN_NUMERO);
                                            LANCA.Set("VLORIGINAL", FIN_VALORORIGINAL);
                                            LANCA.Set("PRDESCONTO", FIN_PERCDESCONTO);
                                            LANCA.Set("VLDESCONTO", FIN_VALORDESCONTO);
                                            LANCA.Set("PRMULTA", FIN_PERCMULTA);
                                            LANCA.Set("VLMULTA", FIN_VALORMULTA);
                                            LANCA.Set("VLLIQUIDO", FIN_VALORLIQUIDOPARCELA);

                                            LANCA.Set("PRJUROS", FIN_PERCJUROS);
                                            LANCA.Set("VLJUROS", FIN_VALORJUROS);


                                            LANCA.Save();

                                            // salvar na glog
                                            conn.ExecTransaction("UPDATE GLOG SET IDLOG = ? WHERE CODTABELA = 'FLANCA' AND CODEMPRESA = ?", new object[] { FIN_CODLANCA, AppLib.Context.Empresa });

                                            FIN_NUMPARCELAS++;
                                        }
                                        //if (listCODLANCAOLD.Length > FIN_NUMPARCELAS)
                                        //{
                                        //    //int index = FIN_NUMPARCELAS;
                                        //    //for (int i = index; i < listCODLANCAOLD.Length; i++)
                                        //    //{
                                        //    //    List<PS.Lib.DataField> objArrExc = new List<Lib.DataField>();
                                        //    //    objArrExc.Add(new PS.Lib.DataField("CODEMPRESA", FIN_CODEMPRESA.Valor));
                                        //    //    objArrExc.Add(new PS.Lib.DataField("CODLANCA", listCODLANCAOLD[i]));
                                        //    //    psPartLancaData.DeleteRecordOper(objArrExc);
                                        //    //}
                                        //}
                                    }
                                }
                                conn.Commit();
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                conn.Rollback();

                            }
                            #endregion
                        }
                        else
                        {
                            MessageBox.Show("Não foi possível carregar os parâmetros da composição da condição de pagamento.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível carregar os parâmetros da condição de pagamento, verifique se a mesma foi informada na operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Edita();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            carregaItens();
        }

        private void gridControl1_DoubleClick_1(object sender, EventArgs e)
        {
            Edita();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void faturaOperacao()
        {

        }

        private void carregaMensagemIbptax()
        {
            decimal federal = 0, municipal = 0, estadual = 0, IBPTAX = 0, Perc = 0;
            string versao = string.Empty;
            string mensagem = string.Empty;

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                DataTable dtIBPTAX = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
SELECT 
	NACIONALFEDERAL, 
	ESTADUAL, 
	MUNICIPAL, 
	VERSAO 
FROM 
	VIBPTAX 
WHERE 
	UF = (SELECT GFILIAL.CODETD FROM GFILIAL INNER JOIN GOPER ON GFILIAL.CODFILIAL = GOPER.CODFILIAL AND GFILIAL.CODEMPRESA = GOPER.CODEMPRESA WHERE GOPER.CODOPER = ?) 
	AND CODIGO = (SELECT CODNCM FROM VPRODUTO INNER JOIN GOPERITEM ON VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO AND VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODOPER = ? AND GOPERITEM.NSEQITEM = ?)", new object[] { codoper, AppLib.Context.Empresa, codoper, row1["GOPERITEM.NSEQITEM"] });
                if (dtIBPTAX.Rows.Count > 0)
                {
                    federal = federal + Convert.ToDecimal(row1["GOPERITEM.VLTOTALITEM"]) * (Convert.ToDecimal(dtIBPTAX.Rows[0]["NACIONALFEDERAL"]) / 100);
                    municipal = municipal + Convert.ToDecimal(row1["GOPERITEM.VLTOTALITEM"]) * (Convert.ToDecimal(dtIBPTAX.Rows[0]["MUNICIPAL"]) / 100);
                    estadual = estadual + Convert.ToDecimal(row1["GOPERITEM.VLTOTALITEM"]) * (Convert.ToDecimal(dtIBPTAX.Rows[0]["ESTADUAL"]) / 100);
                    versao = dtIBPTAX.Rows[0]["VERSAO"].ToString();
                    IBPTAX = federal + municipal + estadual;
                    mensagem = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT VOPERMENSAGEM.MENSAGEM FROM VOPERMENSAGEM INNER JOIN GTIPOPER ON VOPERMENSAGEM.CODMENSAGEM = GTIPOPER.CODMENSAGEMIBPTAX AND VOPERMENSAGEM.CODEMPRESA = GTIPOPER.CODEMPRESA WHERE GTIPOPER.CODTIPOPER = ? AND GTIPOPER.CODEMPRESA = ?", new object[] { psCodTipOper.textBox1.Text, AppLib.Context.Empresa }).ToString();
                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        mensagem = mensagem.Replace("@FEDERAL", string.Format("{0:n2}", federal));
                        mensagem = mensagem.Replace("@ESTADUAL", string.Format("{0:n2}", estadual));
                        mensagem = mensagem.Replace("@MUNICIPAL", string.Format("{0:n2}", municipal));
                        mensagem = mensagem.Replace("@IBPTAX", string.Format("{0:n2}", IBPTAX));
                        mensagem = mensagem.Replace("@PERC", string.Format("{0:n2}", Perc));
                        mensagem = mensagem.Replace("@VERSAO", versao);
                    }
                }
            }
            txtMensagemIBPTAX.Text = mensagem;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(psTextoBox1.textBox1.Text) == 0)
            {
                if (Salvar() == true)
                {
                    PSPartOperacaoItemEdit frm = new PSPartOperacaoItemEdit();
                    frm.codOper = Convert.ToInt32(psTextoBox1.textBox1.Text);
                    frm.consumoFinal = chkTipOperConsFin.Checked;
                    frm.codTipOper = psCodTipOper.textBox1.Text;
                    frm.codlocal = psLookup7.textBox1.Text;
                    frm.ShowDialog();
                    codoper = Convert.ToInt32(psTextoBox1.textBox1.Text);
                    carregaItens();
                    if (composto == true)
                    {
                        btnExpandirComposicao_Click(sender, e);
                    }
                    item = true;
                    calculaOperacao(frm.codOper, frm.codTipOper);
                    AtualizaValores();
                    carregaMensagemIbptax();
                    AtualizaValores();
                    if (frm.editado == true)
                    {
                        btnCancelarAtual.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Não foi possível gravar a operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    psTextoBox1.textBox1.Text = "0";
                    return;
                }
            }
            else
            {
                PSPartOperacaoItemEdit frm = new PSPartOperacaoItemEdit();
                frm.consumoFinal = chkTipOperConsFin.Checked;
                frm.codOper = Convert.ToInt32(psTextoBox1.textBox1.Text);
                frm.codTipOper = psCodTipOper.textBox1.Text;
                frm.codlocal = psLookup7.textBox1.Text;
                frm.ShowDialog();
                carregaItens();
                calculaOperacao(frm.codOper, frm.codTipOper);
                AtualizaValores();
            }
        }

        private void cmbNfe_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbNfe.SelectedIndex == 0)
            {
                psTextoBox8.Enabled = false;
            }
            else
            {
                psTextoBox8.Enabled = true;
            }
        }

        private void psTextoBox6_Validating(object sender, CancelEventArgs e)
        {
            psTextoBox6.textBox1.Text = psTextoBox6.textBox1.Text.ToUpper();
        }

        private void psLookup18_BeforeLookup(object sender, Lib.LookupEventArgs e)
        {
            if (!string.IsNullOrEmpty(newLookup1.txtcodigo.Text))
            {
                e.Filtro.Add(new PS.Lib.PSFilter("CODCLIFOR", newLookup1.txtcodigo.Text));
            }
        }

        private void PSPartOperacaoEdit_KeyDown(object sender, KeyEventArgs e)
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
                    PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque frm = new PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque();
                    frm.ShowDialog();
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    break;
                case Keys.F9:
                    break;
                case Keys.Insert:
                    btnNovo_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            if (Salvar() == false)
            {
                MessageBox.Show("Não foi possível concluir a operação.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
                GC.Collect();
            }
            else
            {
                MessageBox.Show("Não foi possível concluir a operação.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void txtSegundoNumero_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSegundoNumero.textBox1.Text))
            {
                txtSegundoNumero.textBox1.Text = AppLib.Util.Format.CompletarZeroEsquerda(txtSegundoNumero.textBox1.MaxLength, txtSegundoNumero.textBox1.Text);
            }
        }

        private void btnLookupContato_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(newLookup1.txtcodigo.Text))
            {
                PS.Glb.New.Visao.frmVisaoContatoCliente frm = new PS.Glb.New.Visao.frmVisaoContatoCliente(" WHERE CODEMPRESA = " + AppLib.Context.Empresa + " AND CODCLIFOR = " + newLookup1.txtcodigo.Text + " ");
                frm.consulta = true;
                frm.codCliente = newLookup1.txtcodigo.Text;
                frm.ShowDialog();
                if (!string.IsNullOrEmpty(frm.codContato))
                {
                    txtCodContato.Text = frm.codContato;
                    txtDescricaoContato.Text = frm.nome;
                }
            }
        }

        private void txtCodContato_Validated(object sender, EventArgs e)
        {
            getContato();
        }

        public void getContato()
        {
            if (!string.IsNullOrEmpty(txtCodContato.Text) && !string.IsNullOrEmpty(newLookup1.txtcodigo.Text))
            {
                DataTable dtContato = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODCONTATO, NOME FROM VCLIFORCONTATO WHERE CODCONTATO LIKE '%" + txtCodContato.Text + "%' AND CODEMPRESA = ? AND CODCLIFOR = ?", new object[] { AppLib.Context.Empresa, newLookup1.txtcodigo.Text });
                if (dtContato.Rows.Count > 1)
                {
                    PS.Glb.New.Visao.frmVisaoContatoCliente frm = new PS.Glb.New.Visao.frmVisaoContatoCliente(@"WHERE CODPRODUTO LIKE '%" + txtCodContato.Text + "%' AND CODEMPRESA = " + AppLib.Context.Empresa + " AND CODCLIFOR = " + newLookup1.txtcodigo.Text + "");
                    frm.consulta = true;
                    frm.ShowDialog();
                    if (!string.IsNullOrEmpty(frm.codContato))
                    {
                        txtCodContato.Text = frm.codContato;
                        txtDescricaoContato.Text = frm.nome;
                    }
                }
                else if (dtContato.Rows.Count == 1)
                {
                    txtCodContato.Text = dtContato.Rows[0]["CODCONTATO"].ToString();
                    txtDescricaoContato.Text = dtContato.Rows[0]["NOME"].ToString();
                }
                else
                {
                    txtCodContato.Text = string.Empty;
                    txtDescricaoContato.Text = string.Empty;
                }
            }
        }

        private void txtCodCliFor_Validated(object sender, EventArgs e)
        {
            getCliente();
        }

        public void getCliente()
        {

            string filtroUsuario = new Class.Utilidades().getFiltroUsuario("VCLIFOR");
            string sql = @"SELECT CODCLIFOR, NOME FROM VCLIFOR WHERE CODCLIFOR LIKE '%" + newLookup1.txtcodigo.Text + "%' AND CODEMPRESA = " + AppLib.Context.Empresa + "";
            if (!string.IsNullOrEmpty(filtroUsuario))
            {

                sql = sql + filtroUsuario;

            }

            DataTable dtCliente = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa });
            if (dtCliente.Rows.Count > 1)
            {
                string where = @"WHERE CODCLIFOR LIKE '%" + newLookup1.txtcodigo.Text + "%' AND CODEMPRESA = " + AppLib.Context.Empresa + " AND ATIVO = 1 " + filtroUsuario;
                PS.Glb.New.Visao.frmVisaoCliente frm = new PS.Glb.New.Visao.frmVisaoCliente(where);
                frm.consulta = true;
                frm.ShowDialog();
                if (!string.IsNullOrEmpty(frm.codCliente))
                {
                    newLookup1.txtcodigo.Text = frm.codCliente;
                    newLookup1.txtconteudo.Text = frm.nome;
                    GetDefaultCliFor();
                    getDescontoCliente();
                    getConsumidorFinal(frm.codCliente, AppLib.Context.Empresa);
                }
            }
            else if (dtCliente.Rows.Count == 1)
            {
                newLookup1.txtcodigo.Text = dtCliente.Rows[0]["CODCLIFOR"].ToString();
                newLookup1.txtconteudo.Text = dtCliente.Rows[0]["NOME"].ToString();
                GetDefaultCliFor();
                getDescontoCliente();
                getConsumidorFinal(dtCliente.Rows[0]["CODCLIFOR"].ToString(), AppLib.Context.Empresa);
            }
            else
            {
                newLookup1.txtcodigo.Text = string.Empty;
                newLookup1.txtconteudo.Text = string.Empty;
            }
        }

        private void getConsumidorFinal(string codClifor, int codEmpresa)
        {
            int contribIcms = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT CONTRIBICMS FROM VCLIFOR WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { codClifor, codEmpresa }));
            if (contribIcms == 2)
            {
                chkTipOperConsFin.Checked = true;
            }
        }

        private void btnLookupCliente_Click(object sender, EventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoCliente frm = new PS.Glb.New.Visao.frmVisaoCliente(" WHERE CODEMPRESA = " + AppLib.Context.Empresa + " AND ATIVO = 1");
            frm.consulta = true;
            frm.codCliente = newLookup1.txtcodigo.Text;
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.codCliente))
            {
                newLookup1.txtcodigo.Text = frm.codCliente;
                newLookup1.txtconteudo.Text = frm.nome;
                GetDefaultCliFor();
                getDescontoCliente();
                getConsumidorFinal(frm.codCliente, AppLib.Context.Empresa);
            }
        }

        private void getDescontoCliente()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CGCCPF, DESCMAXVENDA, DESCMAXCOMPRA FROM VCLIFOR WHERE CODCLIFOR = ?", new object[] { newLookup1.txtcodigo.Text });
            psMaskedTextBox1.Text = dt.Rows[0]["CGCCPF"].ToString();

            string tipoEntraSai = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT TIPENTSAI FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { psCodTipOper.textBox1.Text, AppLib.Context.Empresa }).ToString();
            psTextoBox9.Visible = true;
            if (tipoEntraSai == "E")
            {
                psTextoBox9.Text = dt.Rows[0]["DESCMAXCOMPRA"].ToString();
                psTextoBox9.Caption = "Desconto Máximo de Compra";
            }
            else
            {
                psTextoBox9.Text = dt.Rows[0]["DESCMAXVENDA"].ToString();
                psTextoBox9.Caption = "Desconto Máximo de Venda";
            }
            if (psComboBox1.Text == "Aberto")
            {
                psMoedaBox2.textBox1.Text = psTextoBox9.Text;
            }
        }

        private void btnAddChaveRef_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtChaveRef.Text))
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GOPERCOPIAREF (CODEMPRESA, CODOPERORIGEM, CODOPERDESTINO, CHAVENFE) VALUES (? ,?, ?, ?)", new object[] { AppLib.Context.Empresa, 0, psTextoBox1.textBox1.Text, txtChaveRef.Text });
                carregaGridChaveRef();
            }
        }

        private void carregaGridChaveRef()
        {
            try
            {
                string tabela = "GOPERCOPIAREF";
                string relacionamento = string.Empty;
                List<string> tabelasFilhas = new List<string>();
                //string where = "WHERE GOPERITEM.CODOPER = " + codoper + " AND GOPERITEM.CODEMPRESA = " + AppLib.Context.Empresa;
                string where = "WHERE GOPERCOPIAREF.CODEMPRESA = " + AppLib.Context.Empresa + " AND GOPERCOPIAREF.CODOPERDESTINO = " + codoper + "";

                string sql = new Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);
                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                gridChaveRef.DataSource = null;
                gridView2.Columns.Clear();
                gridChaveRef.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
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
                if (gridView2.RowCount > 0)
                {
                    //btnAddChaveRef.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnAtualizarChaveRef_Click(object sender, EventArgs e)
        {
            carregaGridChaveRef();
        }

        private void btnSelecionarColunasChaveRef_Click(object sender, EventArgs e)
        {
            New.frmSelecaoColunas frm = new New.frmSelecaoColunas("GOPERCOPIAREF");
            frm.ShowDialog();
            carregaGridChaveRef();
        }

        private void btnSalvarLayouChaveRef_Click(object sender, EventArgs e)
        {
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, "GOPERCOPIAREF" });


            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", "GOPERCOPIAREF");
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView1.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView1.VisibleColumns[i].Width);
                GVISAOUSUARIO.Set("VISIVEL", 1);
                GVISAOUSUARIO.Save();
            }

            DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ? AND FIXO = 1", new object[] { AppLib.Context.Usuario, "GOPERCOPIAREF" });
            if (dtFixo.Rows.Count > 0)
            {
                for (int i = 0; i < dtFixo.Rows.Count; i++)
                {
                    AppLib.ORM.Jit FIXO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                    FIXO.Set("VISAO", "GOPERCOPIAREF");
                    FIXO.Set("CODUSUARIO", AppLib.Context.Usuario);
                    FIXO.Set("COLUNA", dtFixo.Rows[i]["COLUNA"]);
                    FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
            }
        }

        private void btnAgruparChaveRef_Click(object sender, EventArgs e)
        {
            if (gridView2.OptionsView.ShowGroupPanel == true)
            {
                gridView2.OptionsView.ShowGroupPanel = false;
                gridView2.ClearGrouping();
                btnAgrupar.Text = "Agrupar";
            }
            else
            {
                gridView2.OptionsView.ShowGroupPanel = true;
                btnAgrupar.Text = "Desagrupar";
            }
        }

        private void btnPesquisarChaveRef_Click(object sender, EventArgs e)
        {
            if (gridView2.OptionsFind.AlwaysVisible == true)
            {
                gridView2.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView2.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnExcluirChaveRef_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir a chave referenciada?", "Informação do Sistema.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < gridView2.SelectedRowsCount; i++)
                {
                    DataRow row = gridView2.GetDataRow(Convert.ToInt32(gridView2.GetSelectedRows().GetValue(i).ToString()));
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GOPERCOPIAREF WHERE CHAVENFE = ? AND CODEMPRESA = ?", new object[] { row["GOPERCOPIAREF.CHAVENFE"].ToString(), AppLib.Context.Empresa });
                }
                carregaGridChaveRef();
            }
        }

        private void btnExpandirComposicao_Click(object sender, EventArgs e)
        {
            int nseqItem = 1;
            DataTable dtItens = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODPRODUTO, QUANTIDADE, NSEQITEM FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codoper });
            if (dtItens.Rows.Count > 0)
            {
                composto = true;
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT UTILIZAPRODCOPMOSTO, PERMITEEXPANDIRITENS, MANTEMPRODUTOPRINCEXPANSAO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, psCodTipOper.textBox1.Text });
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["MANTEMPRODUTOPRINCEXPANSAO"].ToString() == "S")
                    {

                    }
                    else
                    {
                        for (int i = 0; i < dtItens.Rows.Count; i++)
                        {
                            DataTable dtComposto = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM VPRODUTOCOM WHERE CODEMPRESA = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[i]["CODPRODUTO"] });
                            if (dtComposto.Rows.Count > 0)
                            {
                                AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?", new object[] { AppLib.Context.Empresa, codoper, dtItens.Rows[i]["NSEQITEM"] });
                                for (int iComposto = 0; iComposto < dtComposto.Rows.Count; iComposto++)
                                {
                                    string existeNseq = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NSEQITEM FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?", new object[] { AppLib.Context.Empresa, codoper, nseqItem }).ToString();
                                    if (!string.IsNullOrEmpty(existeNseq))
                                    {
                                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GOPERITEM SET NSEQITEM = ? WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?", new object[] { nseqItem + 1, AppLib.Context.Empresa, codoper, nseqItem });
                                    }
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GOPERITEM (CODEMPRESA, CODOPER, NSEQITEM, CODPRODUTO, QUANTIDADE, VLUNITARIO, VLDESCONTO, PRDESCONTO, VLTOTALITEM) VALUES (?, ?, ?, ?, ?, 0, 0, 0, 0)", new object[] { AppLib.Context.Empresa, codoper, nseqItem, dtComposto.Rows[iComposto]["CODPRODCOM"], Convert.ToDecimal(dtItens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(dtComposto.Rows[iComposto]["QUANTIDADE"]) });
                                    nseqItem = nseqItem + 1;
                                }
                            }
                            else
                            {
                                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GOPERITEM SET NSEQITEM = ? WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?", new object[] { nseqItem, AppLib.Context.Empresa, codoper, dtItens.Rows[i]["NSEQITEM"] });
                                nseqItem = nseqItem + 1;
                            }

                        }
                    }
                    carregaItens();
                }
            }
        }

        private void newLookup1_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(newLookup1.txtcodigo.Text))
            {
                GetDefaultCliFor();
            }
        }

        private void btnMensagemNFEInfAdic_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(psTextoBox1.Text) > 0)
                {
                    psLookup2.Text = string.Empty;
                    if (txtMemoFisco.Text != string.Empty)
                    {
                        psLookup2.OperSearchForm();
                        psLookup2.LoadLookup();

                        string sSql = "SELECT MENSAGEM FROM VOPERMENSAGEM WHERE CODEMPRESA = ? AND CODMENSAGEM = ? ";
                        string sMensagem = dbs.QueryValue(string.Empty, sSql, new object[] { AppLib.Context.Empresa, psLookup2.Text }).ToString();

                        if (sMensagem != string.Empty)
                        {
                            txtMemoFisco.Text = txtMemoFisco.Text + " " + sMensagem;
                        }
                    }
                    else
                    {
                        DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT DISTINCT (convert(varchar(max),VOPERMENSAGEM.MENSAGEM)) MENSAGEM
            FROM GOPERITEM 
            INNER JOIN VNATUREZA ON GOPERITEM.CODNATUREZA = VNATUREZA.CODNATUREZA AND GOPERITEM.CODEMPRESA = VNATUREZA.CODEMPRESA 
            INNER JOIN VOPERMENSAGEM ON VNATUREZA.CODMENSAGEM = VOPERMENSAGEM.CODMENSAGEM 
            INNER JOIN GOPER ON GOPERITEM.CODOPER = GOPER.CODOPER AND GOPERITEM.CODEMPRESA = GOPER.CODEMPRESA
            WHERE GOPERITEM.CODOPER = ?", new object[] { psTextoBox1.Text });
                        if (dt.Rows.Count == 0)
                        {
                            psLookup2.OperSearchForm();
                            psLookup2.LoadLookup();

                            string sSql = "SELECT MENSAGEM FROM VOPERMENSAGEM WHERE CODEMPRESA = ? AND CODMENSAGEM = ? ";
                            string sMensagem = dbs.QueryValue(string.Empty, sSql, new object[] { AppLib.Context.Empresa, psLookup2.Text }).ToString();

                            if (sMensagem != string.Empty)
                            {
                                txtMemoFisco.Text = txtMemoFisco.Text + " " + sMensagem;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                txtMemoFisco.Text = txtMemoFisco.Text + " " + dt.Rows[i]["MENSAGEM"].ToString();
                            }
                        }

                        DataTable dtFisco = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT VOPERMENSAGEM.MENSAGEM FROM GOPERMENSAGEMFISCO INNER JOIN VOPERMENSAGEM ON GOPERMENSAGEMFISCO.CODMENSAGEM = VOPERMENSAGEM.CODMENSAGEM AND GOPERMENSAGEMFISCO.CODEMPRESA = VOPERMENSAGEM.CODEMPRESA WHERE GOPERMENSAGEMFISCO.CODTIPOPER = ? AND GOPERMENSAGEMFISCO.CODEMPRESA = ?", new object[] { psCodTipOper.textBox1.Text, AppLib.Context.Empresa });
                        for (int i = 0; i < dtFisco.Rows.Count; i++)
                        {
                            txtMemoFisco.Text = txtMemoFisco.Text + " " + dtFisco.Rows[i]["MENSAGEM"].ToString();
                        }

                    }
                }
                else
                {
                    throw new Exception("Salve o registro antes de executar um aplicativo.");
                }
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }

        }

        private bool SalvaCompl(AppLib.Data.Connection conn)
        {
            if (tabCamposComplementares.Controls.Count > 0)
            {
                bool retorno = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT COUNT(CODOPER) FROM GOPERCOMPL WHERE CODEMPRESA = ? AND CODOPER = ? ", new object[] { AppLib.Context.Empresa, psTextoBox1.textBox1.Text }));
                if (retorno == false)
                {
                    conn.ExecTransaction("INSERT INTO GOPERCOMPL (CODEMPRESA, CODOPER) VALUES (?, ?)", new object[] { AppLib.Context.Empresa, psTextoBox1.textBox1.Text });
                }
                AppLib.ORM.Jit GOPERCOMPL = new AppLib.ORM.Jit(conn, "GOPERCOMPL");

                Class.Utilidades util = new Class.Utilidades();
                string sql = util.update(this, tabCamposComplementares, "GOPERCOMPL");
                if (!string.IsNullOrEmpty(sql))
                {
                    sql = sql.Remove(sql.Length - 2, 1) + " WHERE CODEMPRESA = ? AND CODOPER = ?";
                    conn.ExecTransaction(sql, new object[] { AppLib.Context.Empresa, psTextoBox1.textBox1.Text });
                }
                return true;
            }
            else
            {
                return true;
            }
        }

        private void carregaCamposComplementares()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPERCOMPL WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { psTextoBox1.textBox1.Text, AppLib.Context.Empresa });
            DataTable dtColunas = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT GTABCAMPOCOMPL.NOMECAMPO 
                                                                                        FROM 
                                                                                         GTABCAMPOCOMPL 
                                                                                         INNER JOIN GTIPOPERCOMPL ON GTIPOPERCOMPL.CODENTIDADE = GTABCAMPOCOMPL.CODENTIDADE AND GTIPOPERCOMPL.NOMECAMPO = GTABCAMPOCOMPL.NOMECAMPO
                                                                                         WHERE GTABCAMPOCOMPL.CODENTIDADE = ? 
                                                                                         AND GTABCAMPOCOMPL.ATIVO = ? 
                                                                                         AND GTIPOPERCOMPL.CODTIPOPER = ?
                                                                                         ORDER BY GTABCAMPOCOMPL.ORDEM", new object[] { "GOPERCOMPL", 1, psCodTipOper.Text });
            //DataTable dtColunas = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT NOMECAMPO FROM GTABCAMPOCOMPL WHERE CODENTIDADE = ? AND ATIVO = ? ORDER BY ORDEM", new object[] { "GOPERCOMPL", 1 });
            if (dt.Rows.Count > 0)
            {
                Control controle;
                for (int iColunas = 0; iColunas < dtColunas.Rows.Count; iColunas++)
                {
                    for (int i = 0; i < tabCamposComplementares.Controls.Count; i++)
                    {
                        controle = tabCamposComplementares.Controls[i];
                        if (controle.GetType().Name.Equals("TextEdit") || controle.GetType().Name.Equals("ComboBox") || controle.GetType().Name.Equals("DateEdit") || controle.GetType().Name.Equals("MemoEdit"))
                        {
                            if (controle.Name.Remove(0, 4) == dtColunas.Rows[iColunas]["NOMECAMPO"].ToString())
                            {
                                // João Pedro Luchiari - Código para carregar os itens do combobox
                                if (controle.GetType().Name.Equals("ComboBox"))
                                {
                                    ComboBox cmb = new ComboBox();
                                    cmb = (ComboBox)controle;

                                    cmb.SelectedText = dt.Rows[0][dtColunas.Rows[iColunas]["NOMECAMPO"].ToString()].ToString();
                                }

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

        #region Lançamentos Financeiros

        private void CarregaGridLancamentoFinanceiro(string where)
        {
            string relacionamento = @" INNER JOIN VCLIFOR ON FLANCA.CODCLIFOR = VCLIFOR.CODCLIFOR AND FLANCA.CODEMPRESA = VCLIFOR.CODEMPRESA";
            List<string> tabelasFilhas = new List<string>();
            tabelasFilhas.Add("VCLIFOR");

            try
            {
                string sql = new Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);
                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridControl2.DataSource = null;
                gridView3.Columns.Clear();

                sql = sql.Replace("FLANCA.CODSTATUS AS 'FLANCA.CODSTATUS'", @"CASE
 WHEN ((CONVERT(DATE,FLANCA.DATAVENCIMENTO) < CONVERT(DATE, GETDATE())) AND FLANCA.CODSTATUS = 0) THEN '3' 
 WHEN ((CONVERT(DATE,FLANCA.DATAVENCIMENTO) = CONVERT(DATE, GETDATE())) AND FLANCA.CODSTATUS = 0)  THEN '4'  
 ELSE FLANCA.CODSTATUS 
 END AS 'FLANCA.CODSTATUS'");

                DataTable dtGrid = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);
                gridControl2.DataSource = dtGrid;

                carregaImagemPagRec();
                if (gridView3.Columns["FLANCA.CODSTATUS"] != null)
                {
                    carregaImagemStatus();
                }

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridView3.Columns.Count; i++)
                {
                    gridView3.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridView3.Columns[i].FieldName.ToString() });

                    if (result != null)
                    {
                        gridView3.Columns[i].Caption = result["DESCRICAO"].ToString();
                        if (gridView3.Columns[i].Caption.Contains("Valor"))
                        {
                            gridView3.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            gridView3.Columns[i].DisplayFormat.FormatString = "n2";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPesquisarLancamento_Click(object sender, EventArgs e)
        {
            if (gridView3.OptionsFind.AlwaysVisible == true)
            {
                gridView3.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView3.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnAgruparLancamento_Click(object sender, EventArgs e)
        {
            if (gridView3.OptionsView.ShowGroupPanel == true)
            {
                gridView3.OptionsView.ShowGroupPanel = false;
                gridView3.ClearGrouping();
                btnAgruparLancamento.Text = "Agrupar";
            }
            else
            {
                gridView3.OptionsView.ShowGroupPanel = true;
                btnAgruparLancamento.Text = "Desagrupar";
            }
        }

        private void btnAtualizarLancamento_Click(object sender, EventArgs e)
        {
            CarregaLancamentoFinanceiro();
        }

        private void btnSelecionarColunasLancamentos_Click(object sender, EventArgs e)
        {
            PS.Glb.New.frmSelecaoColunas frm = new New.frmSelecaoColunas(tabela);
            frm.ShowDialog();
            CarregaLancamentoFinanceiro();
        }

        private void btnSalvarLayoutLancamento_Click(object sender, EventArgs e)
        {
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });

            for (int i = 0; i < gridView3.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", tabela);
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView3.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView3.VisibleColumns[i].Width);
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
            }
        }

        public string casasDecimais_2(string num)
        {
            return String.Format("{0:0.00}", Convert.ToDouble(num));
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

        }

        private void buttonCANCELAR_Click(object sender, EventArgs e)
        {

        }

        private void psTextoBox1_Load(object sender, EventArgs e)
        {

        }

        private void psTextoBox2_Load(object sender, EventArgs e)
        {

        }

        private void psLookup3_Load(object sender, EventArgs e)
        {

        }

        private void psComboBox1_Load(object sender, EventArgs e)
        {

        }

        private void psDateBox1_Load(object sender, EventArgs e)
        {

        }

        private void tabTRANSP_Click(object sender, EventArgs e)
        {

        }

        private void psTextoBox12_Load(object sender, EventArgs e)
        {

        }

        private void psTextoBox11_Load(object sender, EventArgs e)
        {

        }

        private void psLookup19_Load(object sender, EventArgs e)
        {

        }

        private void psLookup14_Load(object sender, EventArgs e)
        {

        }

        private void psTextoBox7_Load(object sender, EventArgs e)
        {

        }

        private void psTextoBox6_Load(object sender, EventArgs e)
        {

        }

        private void psTextoBox3_Load(object sender, EventArgs e)
        {

        }

        private void psTextoBox4_Load(object sender, EventArgs e)
        {

        }

        private void psMoedaBox6_Load(object sender, EventArgs e)
        {

        }

        private void psMoedaBox5_Load(object sender, EventArgs e)
        {

        }

        private void psMoedaBox4_Load(object sender, EventArgs e)
        {

        }

        private void psLookup5_Load(object sender, EventArgs e)
        {

        }

        private void psComboBox2_Load(object sender, EventArgs e)
        {

        }

        private void tabOBSERV_Click(object sender, EventArgs e)
        {

        }

        private void psLookup2_Load(object sender, EventArgs e)
        {

        }

        private void labelControl7_Click(object sender, EventArgs e)
        {

        }

        private void txtMensagemIBPTAX_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl6_Click(object sender, EventArgs e)
        {

        }

        private void txtMemoFisco_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void psLookup1_Load(object sender, EventArgs e)
        {

        }

        private void psLookup10_Load(object sender, EventArgs e)
        {

        }

        private void psMemoBox2_Load(object sender, EventArgs e)
        {

        }

        private void psMemoBox1_Load(object sender, EventArgs e)
        {

        }

        private void psValorBruto_Load(object sender, EventArgs e)
        {

        }

        private void psValorLiquido_Load(object sender, EventArgs e)
        {

        }

        private void tabADD1_Click(object sender, EventArgs e)
        {

        }

        private void chkDescEspecial_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void psMoedaBox2_Load(object sender, EventArgs e)
        {

        }

        private void psComboBox4_Load(object sender, EventArgs e)
        {

        }

        private void psLookup26_Load(object sender, EventArgs e)
        {

        }

        private void psMoedaBox1_Load(object sender, EventArgs e)
        {

        }

        private void psLookup16_Load(object sender, EventArgs e)
        {

        }

        private void psLookup13_Load(object sender, EventArgs e)
        {

        }

        private void psLookup15_Load(object sender, EventArgs e)
        {

        }

        private void psLookup4_Load(object sender, EventArgs e)
        {

        }

        private void psLookup11_Load(object sender, EventArgs e)
        {

        }

        private void psLookup12_Load(object sender, EventArgs e)
        {

        }

        private void psLookup8_Load(object sender, EventArgs e)
        {

        }

        private void psLookup6_Load(object sender, EventArgs e)
        {

        }

        private void psLookup7_Load(object sender, EventArgs e)
        {

        }

        private void psLookup9_Load(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void cmbNfe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void psTextoBox8_Load(object sender, EventArgs e)
        {

        }

        private void newLookup1_Load(object sender, EventArgs e)
        {

        }

        private void labelControl8_Click(object sender, EventArgs e)
        {

        }

        private void txtDescricaoContato_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtCodContato_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtSegundoNumero_Load(object sender, EventArgs e)
        {

        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void chkTipOperConsFin_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkClienteRetira_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void psComboBox5_Load(object sender, EventArgs e)
        {

        }

        private void psTextoBox10_Load(object sender, EventArgs e)
        {

        }

        private void psTextoBox9_Load(object sender, EventArgs e)
        {

        }

        private void psMaskedTextBox1_Load(object sender, EventArgs e)
        {

        }

        private void psTextoBox5_Load(object sender, EventArgs e)
        {

        }

        private void psCodTipOper_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtCodserie_Load(object sender, EventArgs e)
        {

        }

        private void psCodUsuarioCriacao_Load(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void psLookup17_Load(object sender, EventArgs e)
        {

        }

        private void tabITENS_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {

        }

        private void btnAnexo_Click(object sender, EventArgs e)
        {

        }

        private void btnProcessos_Click(object sender, EventArgs e)
        {

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {

        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void dtEntSai_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dtEntrega_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        private void dtEmissao_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void tHoraSai_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void txtDataAlteracao_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl10_Click(object sender, EventArgs e)
        {

        }

        private void txtUsuarioAlteracao_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl9_Click(object sender, EventArgs e)
        {

        }

        private void tabRAT_Click(object sender, EventArgs e)
        {

        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabRATCC_Click(object sender, EventArgs e)
        {

        }

        private void PSPartOperRateioCC_Load(object sender, EventArgs e)
        {

        }

        private void tabRATDP_Click(object sender, EventArgs e)
        {

        }

        private void PSPartOperRateioDP_Load(object sender, EventArgs e)
        {

        }

        private void tabVAL_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gbValorFinanceiro_Enter(object sender, EventArgs e)
        {

        }

        private void psPercSeguro_Load(object sender, EventArgs e)
        {

        }

        private void psPercDespesa_Load(object sender, EventArgs e)
        {

        }

        private void psPercDesconto_Load(object sender, EventArgs e)
        {

        }

        private void psPercFrete_Load(object sender, EventArgs e)
        {

        }

        private void psValorFrete_Load(object sender, EventArgs e)
        {

        }

        private void psValorSeguro_Load(object sender, EventArgs e)
        {

        }

        private void psValorDesconto_Load(object sender, EventArgs e)
        {

        }

        private void psValorDespesa_Load(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void labelControl12_Click(object sender, EventArgs e)
        {

        }

        private void txtChaveRef_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer3_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void toolStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnEditarChaveRef_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator3_Click(object sender, EventArgs e)
        {

        }

        private void btnFiltroChaveRef_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator7_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton2_Click(object sender, EventArgs e)
        {

        }

        private void btnLancamentoFinanceiro_Click(object sender, EventArgs e)
        {

        }

        private void btnNfe_Click(object sender, EventArgs e)
        {

        }

        private void btnEventos_Click(object sender, EventArgs e)
        {

        }

        private void btnNfeDados_Click(object sender, EventArgs e)
        {

        }

        private void btnItens_Click(object sender, EventArgs e)
        {

        }

        private void btnMotivos_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator8_Click(object sender, EventArgs e)
        {

        }

        private void btnFecharAnexo_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimirOperacao_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton3_Click(object sender, EventArgs e)
        {

        }

        private void btnFaturarOperacao_Click(object sender, EventArgs e)
        {

        }

        private void btnCopiaOperacao_Click(object sender, EventArgs e)
        {

        }

        private void btnAjustarValorFinanceiro_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelarOperacao_Click(object sender, EventArgs e)
        {

        }

        private void btnRastreamentoOper_Click(object sender, EventArgs e)
        {

        }

        private void btnConcluirOperacao_Click(object sender, EventArgs e)
        {

        }

        private void nFeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnGerarNfe_Click(object sender, EventArgs e)
        {

        }

        private void btnCartaCorrecao_Click(object sender, EventArgs e)
        {

        }

        private void btnConsultaEventoNfe_Click(object sender, EventArgs e)
        {

        }

        private void liberaçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnAprovaDesconto_Click(object sender, EventArgs e)
        {

        }

        private void btnAprovaLimiteCredito_Click(object sender, EventArgs e)
        {

        }

        private void btnCopiaReferencia_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {

        }

        private void gridChaveRef_Click(object sender, EventArgs e)
        {

        }

        private void tabCamposComplementares_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer4_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void toolStrip4_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnNovoLancamento_Click(object sender, EventArgs e)
        {

        }

        private void btnEditarLancamento_Click(object sender, EventArgs e)
        {

        }

        private void btnExcluirLancamento_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator9_Click(object sender, EventArgs e)
        {

        }

        private void btnFiltrosLancamento_Click(object sender, EventArgs e)
        {

        }

        private void btnVisaoLancamentos_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator10_Click(object sender, EventArgs e)
        {

        }

        private void btnAnexosLancamento_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator11_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {

        }

        private void btnProcessosLancamentos_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem28_Click(object sender, EventArgs e)
        {

        }

        private void btnExportarLancamentos_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem29_Click(object sender, EventArgs e)
        {

        }

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }

        private void gridControl3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer5_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void toolStrip5_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnNovoRastreamentoOP_Click(object sender, EventArgs e)
        {

        }

        private void btnEditarRastreamentoOP_Click(object sender, EventArgs e)
        {

        }

        private void btnExcluirRastreamentoOP_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator12_Click(object sender, EventArgs e)
        {

        }

        private void btnPesquisarRastreamentoOP_Click(object sender, EventArgs e)
        {

        }

        private void btnAgruparRastreamentoOP_Click(object sender, EventArgs e)
        {

        }

        private void btnFiltrosRastreamentoOP_Click(object sender, EventArgs e)
        {

        }

        private void btnVisaoRastreamentoOP_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator13_Click(object sender, EventArgs e)
        {

        }

        private void btnAnexosRastreamentoOP_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem30_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem31_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem32_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem34_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem35_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator14_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem36_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem37_Click(object sender, EventArgs e)
        {

        }

        private void btnProcessosRastreamentoOP_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem38_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem39_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem40_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem41_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem42_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem43_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem44_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem45_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem46_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem47_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem48_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem49_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem50_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem51_Click(object sender, EventArgs e)
        {

        }

        private void btnExportarRastreamentoOP_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem52_Click(object sender, EventArgs e)
        {

        }

        private void CarregaLancamentoFinanceiro()
        {
            CarregaGridLancamentoFinanceiro("WHERE CODOPER = " + codoper + " AND FLANCA.CODEMPRESA = " + AppLib.Context.Empresa);
        }

        private void carregaImagemStatus()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl2.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSTATUS FROM GSTATUS WHERE TABELA = 'FLANCA'");
            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
                imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[i]["DESCRICAO"].ToString(), dtImagem.Rows[i]["CODSTATUS"].ToString(), i));
            }
            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView3.Columns["FLANCA.CODSTATUS"].ColumnEdit = imageCombo;
        }

        private void carregaImagemPagRec()
        {
            //DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl2.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            //DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            //images.AddImage(Image.FromFile(@"Icones\Financeiro\Seta-verde.png"));
            //images.AddImage(Image.FromFile(@"Icones\Financeiro\Seta-vermelha.png"));
            //imageCombo.SmallImages = images;
            //imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Pagar", 0, 1));
            //imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Receber", 1, 0));
            //imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            //gridView3.Columns["FLANCA.TIPOPAGREC"].ColumnEdit = imageCombo;

            // João Pedro Luchiari - 05/01/2018 - Buscar imagem da tabela GSITUACAO.

            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSITUACAO FROM GSITUACAO WHERE TABELA = 'FLANCA'");
            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
            }
            imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[0]["DESCRICAO"].ToString(), 1, 0));
            imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[1]["DESCRICAO"].ToString(), 0, 1));

            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView3.Columns["FLANCA.TIPOPAGREC"].ColumnEdit = imageCombo;
        }

        #endregion

        #region Rastreamento de Operação

        private void CarregaGridRastreamentoOP(string where)
        {
            tabela = "GOPERITEMPRODRELAC";

            string relacionamento = @" INNER JOIN GOPERITEM ON GOPERITEM.CODEMPRESA = GOPERITEMPRODRELAC.CODEMPRESADESTINO AND GOPERITEM.CODOPER = GOPERITEMPRODRELAC.CODOPERDESTINO AND GOPERITEM.NSEQITEM = GOPERITEMPRODRELAC.NSEQITEMDESTINO
                                       INNER JOIN VPRODUTO ON VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA AND VPRODUTO.CODPRODUTO = GOPERITEMPRODRELAC.CODPRODUTODESTINO
                                       INNER JOIN GOPER ON GOPERITEM.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEM.CODOPER = GOPER.CODOPER";
            List<string> tabelasFilhas = new List<string>();
            tabelasFilhas.Add("GOPER");
            tabelasFilhas.Add("GOPERITEM");
            tabelasFilhas.Add("VPRODUTO");

            try
            {
                string sql = new Class.Utilidades().getVisao("MOVIMENTACAO", relacionamento, tabelasFilhas, where);
                sql = sql.Replace("MOVIMENTACAO", "GOPERITEMPRODRELAC");

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridControl3.DataSource = null;
                gridView4.Columns.Clear();

                DataTable dtGrid = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);
                gridControl3.DataSource = dtGrid;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { "MOVIMENTACAO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { "MOVIMENTACAO", AppLib.Context.Usuario });
                for (int i = 0; i < gridView4.Columns.Count; i++)
                {
                    gridView4.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridView4.Columns[i].FieldName.ToString() });

                    if (result != null)
                    {
                        gridView4.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregaRastreamentoOP()
        {
            string CodempresaOrigem = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODEMPRESAORIGEM FROM GOPERITEMPRODRELAC", new object[] { }).ToString();

            CarregaGridRastreamentoOP("WHERE GOPERITEMPRODRELAC.CODOPERORIGEM = '" + codoper + "' AND GOPERITEMPRODRELAC.CODEMPRESAORIGEM = " + AppLib.Context.Empresa);
        }

        private void dtEntrega_Leave(object sender, EventArgs e)
        {
            if (edita == true)
            {
                if (DataEntrega != dtEntrega.Text)
                {
                    if (MessageBox.Show("Deseja alterar a data de entrega dos itens?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GOPERITEM SET DATAENTREGA = ? WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { dtEntrega.DateTime, AppLib.Context.Empresa, codoper });
                    }
                }
                DataEntrega = dtEntrega.Text;
            }
        }

        private void btnAtualizarRastreamentoOP_Click(object sender, EventArgs e)
        {
            CarregaRastreamentoOP();
        }

        private void btnSelecionarColunasRastreamentoOP_Click(object sender, EventArgs e)
        {
            PS.Glb.New.frmSelecaoColunas frm = new New.frmSelecaoColunas("MOVIMENTACAO");
            frm.ShowDialog();
            CarregaRastreamentoOP();
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void gridControl4_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip6_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnNovoCTRC_Click(object sender, EventArgs e)
        {

        }

        private void btnEditarCTRC_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator15_Click(object sender, EventArgs e)
        {

        }

        private void btnFiltrosCTRC_Click(object sender, EventArgs e)
        {

        }

        private void btnVisaoCTRC_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator16_Click(object sender, EventArgs e)
        {

        }

        private void btnAnexosCTRC_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem53_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem54_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem55_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem56_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem57_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem58_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator17_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem59_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem60_Click(object sender, EventArgs e)
        {

        }

        private void btnProcessosCTRC_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem61_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem62_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem63_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem64_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem65_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem66_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem67_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem68_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem69_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem70_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem71_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem72_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem73_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem74_Click(object sender, EventArgs e)
        {

        }

        private void btnExportarCTRC_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem75_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvarLayoutRastreamentoOP_Click(object sender, EventArgs e)
        {
            tabela = "MOVIMENTACAO";
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });

            for (int i = 0; i < gridView4.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", tabela);
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView4.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView4.VisibleColumns[i].Width);
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
                tabela = "GOPERITEMPRODRELAC";
            }
        }

        private bool ValidaItensOperacao(int Codoper)
        {
            bool GeraTributo = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT GERATRIBUTOS FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, psCodTipOper.Text }));

            if (GeraTributo == true)
            {
                DataTable dtItensOperacao = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codoper });

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

        #endregion

        #region CTRC

        private void btnSelecionarNotasFiscais_Click(object sender, EventArgs e)
        {
            PS.Glb.New.Processos.Operacao.frmSelecaoOpCTRCRelac frm = new New.Processos.Operacao.frmSelecaoOpCTRCRelac();
            frm.Codoper = codoper;
            frm.codFilial = codFilial;
            frm.ShowDialog();
            CarregaGridCTRC("WHERE CODOPER = " + codoper + "");
        }

        public void CarregaGridCTRC(string where)
        {
            try
            {
                string sql = string.Empty;

                sql = new Class.Utilidades().getVisao("GOPERRELACCTRC", string.Empty, tabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridControl4.DataSource = null;
                gridView5.Columns.Clear();
                gridControl4.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { "GOPERRELACCTRC" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { "GOPERRELACCTRC", AppLib.Context.Usuario });
                for (int i = 0; i < gridView5.Columns.Count; i++)
                {
                    gridView5.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridView5.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridView5.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluirCTRC_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir os registros selecionados?", "Confirmação.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                        if (new Class.Utilidades().Excluir("CODOPERRELAC", "GOPERRELACCTRC", row["GOPERRELACCTRC.CODOPERRELAC"].ToString()) == true)
                        {
                            if (gridView1.SelectedRowsCount > 0)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Não foi possível excluir registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    MessageBox.Show("Registro excluído com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregaGridCTRC("WHERE CODOPER = " + codoper + "");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPesquisarCTRC_Click(object sender, EventArgs e)
        {
            if (gridView5.OptionsFind.AlwaysVisible == true)
            {
                gridView5.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView5.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnAgruparCTRC_Click(object sender, EventArgs e)
        {
            if (gridView5.OptionsView.ShowGroupPanel == true)
            {
                gridView5.OptionsView.ShowGroupPanel = false;
                gridView5.ClearGrouping();
                btnAgrupar.Text = "Agrupar";
            }
            else
            {
                gridView5.OptionsView.ShowGroupPanel = true;
                btnAgrupar.Text = "Desagrupar";
            }
        }

        private void psTextoBox2_Leave(object sender, EventArgs e)
        {

        }

        private void cmbOperacaoPresencial_Load(object sender, EventArgs e)
        {

        }

        private void lpFormaPagamento_Load(object sender, EventArgs e)
        {

        }

        private void tbEstadoClifor_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl11_Click(object sender, EventArgs e)
        {

        }

        private void btnAtualizarCTRC_Click(object sender, EventArgs e)
        {
            CarregaGridCTRC("WHERE CODOPER = " + codoper + "");
        }

        private void btnSelecionarColunasCTRC_Click(object sender, EventArgs e)
        {
            PS.Glb.New.frmSelecaoColunas frm = new New.frmSelecaoColunas("GOPERRELACCTRC");
            frm.ShowDialog();
            CarregaGridCTRC("WHERE CODOPER = " + codoper + "");
        }

        private void btnSalvarLayoutCTRC_Click(object sender, EventArgs e)
        {
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, "GOPERRELACCTRC" });

            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", tabela);
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView1.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView1.VisibleColumns[i].Width);
                GVISAOUSUARIO.Set("VISIVEL", 1);
                GVISAOUSUARIO.Save();
            }

            DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ? AND FIXO = 1", new object[] { AppLib.Context.Usuario, "GOPERRELACCTRC" });
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
                CarregaGridCTRC("WHERE CODOPER = " + codoper + "");
            }
        }

        #endregion
    }
}