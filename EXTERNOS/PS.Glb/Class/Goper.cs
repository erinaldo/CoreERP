using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PS.Glb.Class
{
    public class Goper
    {
        #region Propriedades

        public int CODEMPRESA { get; set; }
        public int CODOPER { get; set; }
        public string CODTIPOPER { get; set; }
        public string NUMERO { get; set; }
        public string CODCLIFOR { get; set; }
        public int CODFILIAL { get; set; }
        public string CODSERIE { get; set; }
        public string CODNATUREZA { get; set; }
        public string CODLOCAL { get; set; }
        public string CODLOCALENTREGA { get; set; }
        public string CODOBJETO { get; set; }
        public string CODOPERADOR { get; set; }
        public string CODSTATUS { get; set; }
        public DateTime? DATACRIACAO { get; set; }
        public DateTime? DATAEMISSAO { get; set; }
        public DateTime? DATAENTSAI { get; set; }
        public int FRETECIFFOB { get; set; }
        public string CODTRANSPORTADORA { get; set; }
        public string CODCONDICAO { get; set; }
        public string CODFORMA { get; set; }
        public decimal VALORBRUTO { get; set; }
        public decimal VALORLIQUIDO { get; set; }
        public decimal VALORFRETE { get; set; }
        public string CODUSUARIOCRIACAO { get; set; }
        public DateTime? DATAENTREGA { get; set; }
        public decimal QUANTIDADE { get; set; }
        public decimal PESOLIQUIDO { get; set; }
        public decimal PESOBRUTO { get; set; }
        public string ESPECIE { get; set; }
        public string CODCONTA { get; set; }
        public string CODREPRE { get; set; }
        public decimal PRCOMISSAO { get; set; }
        public DateTime? DATAFATURAMENTO { get; set; }
        public string CODUSUARIOFATURAMENTO { get; set; }
        public string CAMPOLIVRE1 { get; set; }
        public string CAMPOLIVRE2 { get; set; }
        public string CAMPOLIVRE3 { get; set; }
        public string CAMPOLIVRE4 { get; set; }
        public string CAMPOLIVRE5 { get; set; }
        public string CAMPOLIVRE6 { get; set; }
        public DateTime? DATAEXTRA1 { get; set; }
        public DateTime? DATAEXTRA2 { get; set; }
        public DateTime? DATAEXTRA3 { get; set; }
        public DateTime? DATAEXTRA4 { get; set; }
        public DateTime? DATAEXTRA5 { get; set; }
        public DateTime? DATAEXTRA6 { get; set; }
        public decimal CAMPOVALOR1 { get; set; }
        public decimal CAMPOVALOR2 { get; set; }
        public decimal CAMPOVALOR3 { get; set; }
        public decimal CAMPOVALOR4 { get; set; }
        public decimal CAMPOVALOR5 { get; set; }
        public decimal CAMPOVALOR6 { get; set; }
        public string OBSERVACAO { get; set; }
        public string HISTORICO { get; set; }
        public decimal PERCFRETE { get; set; }
        public decimal VALORDESCONTO { get; set; }
        public decimal PERCDESCONTO { get; set; }
        public decimal VALORDESPESA { get; set; }
        public decimal PERCDESPESA { get; set; }
        public decimal VALORSEGURO { get; set; }
        public decimal PERCSEGURO { get; set; }
        public string MARCA { get; set; }
        public string CODNATUREZAORCAMENTO { get; set; }
        public string CODCCUSTO { get; set; }
        public string PLACA { get; set; }
        public string UFPLACA { get; set; }
        public string NFE { get; set; }
        public string CHAVENFE { get; set; }
        public bool TIPOPERCONSFIN { get; set; }
        public string CODVENDEDOR { get; set; }
        public string ORDEMDECOMPRA { get; set; }
        public int? CODTIPOTRANSPORTE { get; set; }
        public bool CLIENTERETIRA { get; set; }
        public decimal LIMITEDESC { get; set; }
        public string TIPOBLOQUEIODESC { get; set; }
        public int? CODFILIALENTREGA { get; set; }
        public string NOMEFANTASIA { get; set; }
        public int CODCONTATO { get; set; }
        public string CODSITUACAO { get; set; }
        public string UFSAIDAPAIS { get; set; }
        public string LOCEXPORTA { get; set; }
        public string LOCDESPACHO { get; set; }
        public string NFEINFADIC { get; set; }
        public string MENSAGEMIBPTAX { get; set; }
        public string SEGUNDONUMERO { get; set; }
        public string USUARIOALTERACAO { get; set; }
        public DateTime? DATAALTERACAO { get; set; }
        public bool DESCESPECIAL { get; set; }
        public int APROVACAO { get; set; }

        public List<GoperItem> item { get; set; }

        #endregion

        #region Variaveis

        private gTipOper _gTipoper = new gTipOper();

        #endregion

        #region Construtor

        public Goper() { }

        #endregion


        /// <summary>
        /// Método para buscar a operação
        /// </summary>
        /// <param name="codOper">Código da Operação</param>
        /// <returns>Classe populada Goper</returns>
        public Class.Goper getGoper(string codOper, AppLib.Data.Connection conn)
        {
            Class.Goper operacao = new Class.Goper();
            try
            {

                DataTable dt = conn.ExecQuery(@"SELECT * FROM GOPER WHERE CODOPER = ? AND CODEMPRESA = ? ", new object[] { codOper, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {

                    operacao.CAMPOLIVRE1 = dt.Rows[0]["CAMPOLIVRE1"].ToString();
                    operacao.CAMPOLIVRE2 = dt.Rows[0]["CAMPOLIVRE2"].ToString();
                    operacao.CAMPOLIVRE3 = dt.Rows[0]["CAMPOLIVRE3"].ToString();
                    operacao.CAMPOLIVRE4 = dt.Rows[0]["CAMPOLIVRE4"].ToString();
                    operacao.CAMPOLIVRE5 = dt.Rows[0]["CAMPOLIVRE5"].ToString();
                    operacao.CAMPOLIVRE6 = dt.Rows[0]["CAMPOLIVRE6"].ToString();
                    operacao.CAMPOVALOR1 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR1"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR1"]);
                    operacao.CAMPOVALOR2 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR2"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR2"]);
                    operacao.CAMPOVALOR3 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR3"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR3"]);
                    operacao.CAMPOVALOR4 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR4"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR4"]);
                    operacao.CAMPOVALOR5 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR5"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR5"]);
                    operacao.CAMPOVALOR6 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR6"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR6"]);
                    operacao.CHAVENFE = dt.Rows[0]["CHAVENFE"].ToString();
                    operacao.CODCCUSTO = dt.Rows[0]["CODCCUSTO"].ToString();
                    operacao.CODCLIFOR = dt.Rows[0]["CODCLIFOR"].ToString();
                    operacao.CODCONDICAO = dt.Rows[0]["CODCONDICAO"].ToString();
                    operacao.CODCONTA = dt.Rows[0]["CODCONTA"].ToString();
                    operacao.CODEMPRESA = Convert.ToInt32(dt.Rows[0]["CODEMPRESA"].ToString());
                    operacao.CODFILIAL = Convert.ToInt32(dt.Rows[0]["CODFILIAL"].ToString());
                    operacao.CODFORMA = dt.Rows[0]["CODFORMA"].ToString();
                    operacao.CODLOCAL = dt.Rows[0]["CODLOCAL"].ToString();
                    operacao.CODLOCALENTREGA = dt.Rows[0]["CODLOCALENTREGA"].ToString();
                    operacao.CODNATUREZA = dt.Rows[0]["CODNATUREZA"].ToString();
                    operacao.CODNATUREZAORCAMENTO = dt.Rows[0]["CODNATUREZAORCAMENTO"].ToString();
                    operacao.CODOBJETO = dt.Rows[0]["CODOBJETO"].ToString();
                    operacao.CODOPER = Convert.ToInt32(dt.Rows[0]["CODOPER"]);
                    operacao.CODOPERADOR = dt.Rows[0]["CODOPERADOR"].ToString();
                    operacao.CODREPRE = dt.Rows[0]["CODREPRE"].ToString();
                    operacao.CODSERIE = dt.Rows[0]["CODSERIE"].ToString();
                    operacao.CODSTATUS = "0";
                    operacao.CODTIPOPER = dt.Rows[0]["CODTIPOPER"].ToString();
                    operacao.CODTRANSPORTADORA = dt.Rows[0]["CODTRANSPORTADORA"].ToString();
                    operacao.CODUSUARIOCRIACAO = dt.Rows[0]["CODUSUARIOCRIACAO"].ToString();
                    operacao.CODUSUARIOFATURAMENTO = dt.Rows[0]["CODUSUARIOFATURAMENTO"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATACRIACAO"].ToString()))
                    {
                        operacao.DATACRIACAO = Convert.ToDateTime(dt.Rows[0]["DATACRIACAO"]);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEMISSAO"].ToString()))
                    {
                        operacao.DATAEMISSAO = Convert.ToDateTime(dt.Rows[0]["DATAEMISSAO"]);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAENTREGA"].ToString()))
                    {
                        operacao.DATAENTREGA = Convert.ToDateTime(dt.Rows[0]["DATAENTREGA"]);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAENTSAI"].ToString()))
                    {
                        operacao.DATAENTSAI = Convert.ToDateTime(dt.Rows[0]["DATAENTSAI"]);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA1"].ToString()))
                    {
                        operacao.DATAEXTRA1 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA1"]);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA2"].ToString()))
                    {
                        operacao.DATAEXTRA2 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA2"]);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA3"].ToString()))
                    {
                        operacao.DATAEXTRA3 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA3"]);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA4"].ToString()))
                    {
                        operacao.DATAEXTRA4 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA4"]);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA5"].ToString()))
                    {
                        operacao.DATAEXTRA5 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA5"]);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA6"].ToString()))
                    {
                        operacao.DATAEXTRA6 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA6"]);
                    }
                    operacao.DATAFATURAMENTO = AppLib.Context.poolConnection.Get("Start").GetDateTime();
                    operacao.ESPECIE = dt.Rows[0]["ESPECIE"].ToString();
                    operacao.FRETECIFFOB = string.IsNullOrEmpty(dt.Rows[0]["FRETECIFFOB"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["FRETECIFFOB"]);
                    operacao.HISTORICO = dt.Rows[0]["HISTORICO"].ToString();
                    operacao.MARCA = dt.Rows[0]["MARCA"].ToString();
                    operacao.NFE = dt.Rows[0]["NFE"].ToString();
                    operacao.NUMERO = dt.Rows[0]["NUMERO"].ToString();
                    operacao.OBSERVACAO = dt.Rows[0]["OBSERVACAO"].ToString();
                    operacao.PERCDESCONTO = Convert.ToDecimal(dt.Rows[0]["PERCDESCONTO"]);
                    operacao.PERCDESPESA = Convert.ToDecimal(dt.Rows[0]["PERCDESPESA"]);
                    operacao.PERCFRETE = Convert.ToDecimal(dt.Rows[0]["PERCFRETE"]);
                    operacao.PERCSEGURO = Convert.ToDecimal(dt.Rows[0]["PERCSEGURO"]);
                    operacao.PESOBRUTO = Convert.ToDecimal(dt.Rows[0]["PESOBRUTO"]);
                    operacao.PESOLIQUIDO = Convert.ToDecimal(dt.Rows[0]["PESOLIQUIDO"]);
                    operacao.PLACA = dt.Rows[0]["PLACA"].ToString();
                    operacao.PRCOMISSAO = Convert.ToDecimal(dt.Rows[0]["PRCOMISSAO"]);
                    operacao.TIPOPERCONSFIN = Convert.ToBoolean(dt.Rows[0]["TIPOPERCONSFIN"]);
                    operacao.UFPLACA = dt.Rows[0]["UFPLACA"].ToString();
                    operacao.VALORBRUTO = Convert.ToDecimal(dt.Rows[0]["VALORBRUTO"]);
                    operacao.VALORDESCONTO = Convert.ToDecimal(dt.Rows[0]["VALORDESCONTO"]);
                    operacao.VALORDESPESA = Convert.ToDecimal(dt.Rows[0]["VALORDESPESA"]);
                    operacao.VALORFRETE = Convert.ToDecimal(dt.Rows[0]["VALORFRETE"]);
                    operacao.VALORLIQUIDO = Convert.ToDecimal(dt.Rows[0]["VALORLIQUIDO"]);
                    operacao.VALORSEGURO = Convert.ToDecimal(dt.Rows[0]["VALORSEGURO"]);
                    operacao.NOMEFANTASIA = dt.Rows[0]["NOMEFANTASIA"].ToString();
                    operacao.CODCONTATO = string.IsNullOrEmpty(dt.Rows[0]["CODCONTATO"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["CODCONTATO"]);
                    operacao.CODSITUACAO = dt.Rows[0]["CODSITUACAO"].ToString();
                    operacao.UFSAIDAPAIS = dt.Rows[0]["UFSAIDAPAIS"].ToString();
                    operacao.LOCEXPORTA = dt.Rows[0]["LOCEXPORTA"].ToString();
                    operacao.LOCDESPACHO = dt.Rows[0]["LOCDESPACHO"].ToString();
                    operacao.NFEINFADIC = dt.Rows[0]["NFEINFADIC"].ToString();
                    operacao.MENSAGEMIBPTAX = dt.Rows[0]["MENSAGEMIBPTAX"].ToString();
                    operacao.SEGUNDONUMERO = dt.Rows[0]["SEGUNDONUMERO"].ToString();
                    operacao.USUARIOALTERACAO = dt.Rows[0]["USUARIOALTERACAO"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAALTERACAO"].ToString()))
                    {
                        operacao.DATAALTERACAO = Convert.ToDateTime(dt.Rows[0]["DATAALTERACAO"]);
                    }
                    operacao.DESCESPECIAL = dt.Rows[0]["USUARIOALTERACAO"].ToString() == "0" ? false : true;
                    operacao.item = new GoperItem().getGoperItem(operacao.CODOPER, conn);
                }
                return operacao;
            }
            catch (Exception)
            {
                return operacao;
            }
        }

        /// <summary>
        /// Validação do Cabeçalho
        /// </summary>
        /// <returns>True / False</returns>
        private bool validacaoCabecalho(AppLib.Data.Connection conn)
        {
            try
            {
                //Código da Operação
                if (CODOPER == 0)
                {
                    MessageBox.Show("Erro ao tentar inserir operação.\nCódigo da operação não pode ser nulo.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //Data de Entrega
                if (_gTipoper.USADATAENTREGA == "E")
                {
                    if (DATAENTREGA == null)
                    {
                        MessageBox.Show("A data e entrega não pode ser nula.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                //Cliente
                if (string.IsNullOrEmpty(CODCLIFOR))
                {
                    MessageBox.Show("O cliente não pode ser nulo.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //Operador
                if (string.IsNullOrEmpty(CODOPERADOR))
                {
                    MessageBox.Show("O operador não pode ser nulo.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //Número da operação
                if (validaNumeroOperacao(conn) == true)
                {
                    MessageBox.Show("O número da operação não pode ser validado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Busca o último código da operação.
        /// </summary>
        private void getUltimoNumeroOperacao(AppLib.Data.Connection conn)
        {
            CODOPER = Convert.ToInt32(conn.ExecGetField(0, "SELECT IDLOG FROM GLOG WHERE CODTABELA = 'GOPER' AND CODEMPRESA = ?", new object[] { AppLib.Context.Empresa })) + 1;
        }

        /// <summary>
        /// Busca o novo número da operação
        /// </summary>
        private void getNovoNumero(AppLib.Data.Connection conn)
        {
            if (_gTipoper.USANUMEROSEQ == 1)
            {
                if (string.IsNullOrEmpty(_gTipoper.VTIPOPERSERIE.CODSERIE))
                {
                    string CodSerie = getCodSerie(conn);
                    int _numero = Convert.ToInt32(conn.ExecGetField(0, @"SELECT NUMSEQ FROM VSERIE WHERE CODSERIE = ? AND CODFILIAL = ? AND CODEMPRESA = ?", new object[] { CodSerie, CODFILIAL, AppLib.Context.Empresa })) + 1;
                    NUMERO = string.Concat(_numero.ToString().PadLeft(_gTipoper.MASKNUMEROSEQ, '0'));
                }
                else
                {
                    int _numero = Convert.ToInt32(conn.ExecGetField(0, @"SELECT NUMSEQ FROM VSERIE WHERE CODSERIE = ? AND CODFILIAL = ? AND CODEMPRESA = ?", new object[] { _gTipoper.VTIPOPERSERIE.CODSERIE, CODFILIAL, AppLib.Context.Empresa })) + 1;
                    NUMERO = string.Concat(_numero.ToString().PadLeft(_gTipoper.MASKNUMEROSEQ, '0'));
                }              
            }
        }

        /// <summary>
        /// Validação dos campos obrigatórios para gerar operação
        /// </summary>
        /// <returns>True / False</returns>
        private bool validaNumeroOperacao(AppLib.Data.Connection conn)
        {
            //Verifica se utiliza número sequencial
            if (_gTipoper.USANUMEROSEQ == 1)
            {
                //Busca o novo número
                getNovoNumero(conn);
                //Verifica se o número não está vazio
                if (!string.IsNullOrEmpty(NUMERO))
                {
                    string sSerie = string.Empty;
                    string sCodCliFor = string.Empty;
                    int CountCodOper = 0;
                    CODSERIE = _gTipoper.VTIPOPERSERIE.CODSERIE;
                    if (CODSERIE != "M")
                    {
                        sSerie = " AND CODSERIE = ? ";
                    }
                    if (_gTipoper.CODCLIFOR != "M")
                    {
                        sCodCliFor = " AND CODCLIFOR = ? ";
                    }
                    string sSql = @"SELECT COUNT(CODOPER) FROM GOPER WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPER <> ? AND NUMERO = ? ";
                    if (string.IsNullOrEmpty(sSerie))
                    {
                        if (string.IsNullOrEmpty(sCodCliFor))
                        {
                            CountCodOper = Convert.ToInt32(conn.ExecGetField(0, sSql, CODEMPRESA, CODFILIAL, CODOPER, NUMERO));
                            return CountCodOper == 0 ? false : true;
                        }
                        else
                        {
                            if (_gTipoper.TIPENTSAI == "E")
                            {
                                sSql = string.Concat(sSql, sCodCliFor);
                                CountCodOper = Convert.ToInt32(conn.ExecGetField(0, sSql, CODEMPRESA, CODFILIAL, CODOPER, NUMERO, CODCLIFOR));
                                return CountCodOper == 0 ? false : true;
                            }
                            else
                            {
                                CountCodOper = Convert.ToInt32(conn.ExecGetField(0, sSql, CODEMPRESA, CODFILIAL, CODOPER, NUMERO));
                                return CountCodOper == 0 ? false : true;
                            }
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(sCodCliFor))
                        {
                            sSql = string.Concat(sSql, sSerie);
                            CountCodOper = Convert.ToInt32(conn.ExecGetField(0, sSql, CODEMPRESA, CODFILIAL, CODOPER, NUMERO, CODSERIE));
                            return CountCodOper == 0 ? false : true;
                        }
                        else
                        {
                            if (_gTipoper.TIPENTSAI == "E")
                            {
                                sSql = string.Concat(sSql, sSerie);
                                CountCodOper = Convert.ToInt32(conn.ExecGetField(0, sSql, CODEMPRESA, CODFILIAL, CODOPER, NUMERO, CODCLIFOR));
                                return CountCodOper == 0 ? false : true;
                            }
                            else
                            {
                                sSql = string.Concat(sSql, sSerie);
                                CountCodOper = Convert.ToInt32(conn.ExecGetField(0, sSql, CODEMPRESA, CODFILIAL, CODOPER, NUMERO, CODSERIE));
                                return CountCodOper == 0 ? false : true;
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Busca o Código da Série
        /// </summary>
        /// <param name="conn">Conexão Ativa</param>
        /// <returns>Retorna o Código da Série com base na Query.</returns>
        private string getCodSerie(AppLib.Data.Connection conn)
        {
            string Serie = conn.ExecGetField(string.Empty, "SELECT CODSERIE FROM VTIPOPERSERIE WHERE CODTIPOPER = ? AND CODEMPRESA = ? AND CODFILIAL = ? AND PRINCIPAL = 1", new object[] { CODTIPOPER, CODEMPRESA, CODFILIAL}).ToString();
            return Serie;
        }

        /// <summary>
        /// Inseri o operação
        /// </summary>
        /// <param name="gOper">Classe populada GOPER</param>
        /// <returns>True / False</returns>
        public int setGoper(Goper gOper, AppLib.Data.Connection conn)
        {
            try
            {
                //Busca a tabela GTIPOPER
                _gTipoper = new gTipOper().getgTipOper(gOper.CODTIPOPER);
                //Busca o ultimo número da operação
                getUltimoNumeroOperacao(conn);

                // João Pedro Luchiari - 23/11/2017 - Setado o campo CODSTATUS como 0.
                //gOper.CODSTATUS = "0";

                if (!string.IsNullOrEmpty(gOper.CODOPER.ToString())) //if (!string.IsNullOrEmpty(_gTipoper.CODTIPOPER))
                {
                    //Valida o número da operação
                    if (validacaoCabecalho(conn) == true)
                    {
                        try
                        {
                            #region Operação

                            //IDENTIFICAÇÃO
                            AppLib.ORM.Jit GOPER = new AppLib.ORM.Jit(conn, "GOPER");
                            GOPER.Set("CODEMPRESA", CODEMPRESA);
                            GOPER.Set("CODOPER", CODOPER);
                            if (string.IsNullOrEmpty(CODSTATUS))
                            {
                                CODSTATUS = "0";
                            }
                            GOPER.Set("CODSTATUS", CODSTATUS);
                            GOPER.Set("NUMERO", NUMERO);
                            GOPER.Set("SEGUNDONUMERO", SEGUNDONUMERO);
                            if (string.IsNullOrEmpty(CODSERIE))
                            {
                                CODSERIE = getCodSerie(conn);
                            }
                            GOPER.Set("CODSERIE", CODSERIE);
                            GOPER.Set("CODTIPOPER", CODTIPOPER);
                            if (CODCONTATO == 0)
                            {
                                GOPER.Set("CODCONTATO", null);
                            }
                            else
                            {
                                GOPER.Set("CODCONTATO", CODCONTATO);
                            }
                            GOPER.Set("DATACRIACAO", conn.GetDateTime());
                            GOPER.Set("CODUSUARIOCRIACAO", CODUSUARIOCRIACAO);
                            GOPER.Set("USUARIOALTERACAO", USUARIOALTERACAO);
                            GOPER.Set("DATAALTERACAO", conn.GetDateTime());
                            GOPER.Set("TIPOPERCONSFIN", TIPOPERCONSFIN == false ? 0 : 1);
                            GOPER.Set("CLIENTERETIRA", CLIENTERETIRA == false ? 0 : 1);
                            GOPER.Set("NFE", NFE);
                            GOPER.Set("CHAVENFE", CHAVENFE);
                            GOPER.Set("CODCLIFOR", CODCLIFOR);
                            GOPER.Set("NOMEFANTASIA", NOMEFANTASIA);
                            GOPER.Set("LIMITEDESC", LIMITEDESC);
                            GOPER.Set("ORDEMDECOMPRA", ORDEMDECOMPRA);
                            GOPER.Set("CODFILIAL", CODFILIAL);
                            GOPER.Set("CODFILIALENTREGA", CODFILIALENTREGA == 0 ? null : CODFILIALENTREGA);
                            GOPER.Set("CODLOCAL", CODLOCAL);
                            GOPER.Set("CODLOCALENTREGA", string.IsNullOrEmpty(CODLOCALENTREGA) ? null : CODLOCALENTREGA);
                            GOPER.Set("DATAEMISSAO", conn.GetDateTime());
                            GOPER.Set("DATAENTREGA", DATAENTREGA);
                            GOPER.Set("DATAENTSAI", conn.GetDateTime());
                            GOPER.Set("VALORBRUTO", VALORBRUTO);
                            GOPER.Set("VALORLIQUIDO", VALORLIQUIDO);
                            GOPER.Set("APROVACAO", APROVACAO);
                            //TABELAS
                            GOPER.Set("CODCONDICAO", string.IsNullOrEmpty(CODCONDICAO) ? null : CODCONDICAO);
                            GOPER.Set("CODFORMA", string.IsNullOrEmpty(CODFORMA) ? null : CODFORMA);
                            GOPER.Set("CODCONTA", string.IsNullOrEmpty(CODCONTA) ? null : CODCONTA);
                            GOPER.Set("CODOPERADOR", string.IsNullOrEmpty(CODOPERADOR) ? null : CODOPERADOR);
                            GOPER.Set("CODOBJETO", string.IsNullOrEmpty(CODOBJETO) ? null : CODOBJETO);
                            GOPER.Set("CODREPRE", string.IsNullOrEmpty(CODREPRE) ? null : CODREPRE);
                            GOPER.Set("PRCOMISSAO", PRCOMISSAO);
                            GOPER.Set("CODVENDEDOR", string.IsNullOrEmpty(CODVENDEDOR) ? null : CODVENDEDOR);
                            GOPER.Set("CODCCUSTO", string.IsNullOrEmpty(CODCCUSTO) ? null : CODCCUSTO);
                            GOPER.Set("CODNATUREZAORCAMENTO", string.IsNullOrEmpty(CODNATUREZAORCAMENTO) ? null : CODNATUREZAORCAMENTO);
                            GOPER.Set("TIPOBLOQUEIODESC", TIPOBLOQUEIODESC);
                            GOPER.Set("LIMITEDESC", LIMITEDESC);
                            GOPER.Set("DESCESPECIAL", Convert.ToInt32(DESCESPECIAL));
                            //VALORES
                            GOPER.Set("PERCFRETE", PERCFRETE);
                            GOPER.Set("VALORFRETE", VALORFRETE);
                            GOPER.Set("PERCDESCONTO", PERCDESCONTO);
                            GOPER.Set("VALORDESCONTO", VALORDESCONTO);
                            GOPER.Set("PERCDESPESA", PERCDESPESA);
                            GOPER.Set("VALORDESPESA", VALORDESPESA);
                            GOPER.Set("PERCSEGURO", PERCSEGURO);
                            GOPER.Set("VALORSEGURO", VALORSEGURO);
                            //TRANSPORTE
                            GOPER.Set("FRETECIFFOB", FRETECIFFOB);
                            GOPER.Set("CODTRANSPORTADORA", string.IsNullOrEmpty(CODTRANSPORTADORA) ? null : CODTRANSPORTADORA);
                            GOPER.Set("CODTIPOTRANSPORTE", CODTIPOTRANSPORTE == 0 ? null : CODTIPOTRANSPORTE);
                            GOPER.Set("QUANTIDADE", QUANTIDADE);
                            GOPER.Set("PESOBRUTO", PESOBRUTO);
                            GOPER.Set("PESOLIQUIDO", PESOLIQUIDO);
                            GOPER.Set("ESPECIE", ESPECIE);
                            GOPER.Set("MARCA", MARCA);
                            GOPER.Set("PLACA", PLACA);
                            GOPER.Set("UFPLACA", UFPLACA);
                            GOPER.Set("UFSAIDAPAIS", string.IsNullOrEmpty(UFSAIDAPAIS) ? null : UFSAIDAPAIS);
                            GOPER.Set("LOCEXPORTA", LOCEXPORTA);
                            GOPER.Set("LOCDESPACHO", LOCDESPACHO);
                            //HISTORICO
                            GOPER.Set("OBSERVACAO", OBSERVACAO);
                            GOPER.Set("HISTORICO", HISTORICO);
                            GOPER.Set("NFEINFADIC", NFEINFADIC);
                            GOPER.Set("MENSAGEMIBPTAX", MENSAGEMIBPTAX);
                            GOPER.Save();

                            #endregion

                            #region Itens
                            if (item != null)
                            {
                                for (int i = 0; i < item.Count; i++)
                                {
                                    if (new GoperItem().setItem(item[i], conn, CODOPER) == false)
                                    {
                                        MessageBox.Show("Não foi possível inserir o item.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return 0;
                                    }
                                }
                            }

                            #endregion

                            #region Altera o código da operação da tabela GLOG

                            AppLib.ORM.Jit GLOG = new AppLib.ORM.Jit(conn, "GLOG");
                            GLOG.Set("CODEMPRESA", CODEMPRESA);
                            GLOG.Set("CODTABELA", "GOPER");
                            GLOG.Set("IDLOG", CODOPER);
                            GLOG.Save();

                            #endregion

                            #region Altera o número da operação da tabela VSERIE

                            AppLib.ORM.Jit VSERIE = new AppLib.ORM.Jit(conn, "VSERIE");
                            VSERIE.Set("CODEMPRESA", CODEMPRESA);
                            VSERIE.Set("CODSERIE", CODSERIE);
                            VSERIE.Set("CODFILIAL", CODFILIAL);
                            VSERIE.Set("NUMSEQ", NUMERO);
                            VSERIE.Save();

                            #endregion

                        }
                        catch (Exception ex)
                        {
                            return 0;
                        }
                        return CODOPER;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
