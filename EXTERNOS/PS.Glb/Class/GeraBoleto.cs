using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb.Class
{
    public class GeraBoleto
    {
        public static void GerarNossoNumero(int CODEMPRESA, int CODLANCA)
        {
            System.Data.DataTable dtLanca = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FBOLETO WHERE CODEMPRESA = ? AND CODLANCA = ?", new Object[] { CODEMPRESA, CODLANCA });
            String CODCONVENIO = dtLanca.Rows[0]["CODCONVENIO"].ToString();

            String consultaConvenio = @"SELECT * FROM FCONVENIO WHERE CODEMPRESA = ? AND CODCONVENIO = ?";
            System.Data.DataTable dtConvenio = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaConvenio, new Object[] { CODEMPRESA, CODCONVENIO });

            if (dtConvenio.Rows.Count > 0)
            {
                int IDCONVENIOREGRA = int.Parse(dtConvenio.Rows[0]["IDCONVENIOREGRA"].ToString());

                if (IDCONVENIOREGRA == 1)
                {
                    // Ref. do Lançamento
                    String comando = "UPDATE FBOLETO SET NOSSONUMERO = ? WHERE CODEMPRESA = ? AND CODLANCA = ?";
                    int temp = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comando, new Object[] { CODLANCA, CODEMPRESA, CODLANCA });
                }

                if (IDCONVENIOREGRA == 2)
                {
                    // Sequêncial Convênio
                    int? proximosequencial = Convert.ToInt32(dtConvenio.Rows[0]["PROXIMOSEQUENCIAL"]);

                    if (proximosequencial == null)
                    {
                        proximosequencial = 1;
                    }

                    String comando = "UPDATE FBOLETO SET NOSSONUMERO = ? WHERE CODEMPRESA = ? AND CODLANCA = ?";
                    int temp = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comando, new Object[] { proximosequencial, CODEMPRESA, CODLANCA });

                    proximosequencial++;

                    String comandoSequencial = "UPDATE FCONVENIO SET PROXIMOSEQUENCIAL = ? WHERE CODEMPRESA = ? AND CODCONVENIO = ?";
                    int tempSequencial = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comandoSequencial, new Object[] { proximosequencial, CODEMPRESA, CODCONVENIO });
                }

                if (IDCONVENIOREGRA == 3)
                {
                    // Gerado pelo Banco -> Deixa nulo
                }
            }
        }
        public static void GerarDigitoNossoNumero(int CODEMPRESA, int CODLANCA)
        {
            String consultaFBOLETO = "SELECT * FROM FBOLETO WHERE CODEMPRESA = ? AND CODLANCA = ?";
            System.Data.DataTable dtFBOLETO = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaFBOLETO, new Object[] { CODEMPRESA, CODLANCA });

            if (dtFBOLETO.Rows[0]["NOSSONUMERO"] == DBNull.Value)
            {
                // não existe nosso número para calcular o dígito
            }
            else
            {
                String CODCONVENIO = dtFBOLETO.Rows[0]["CODCONVENIO"].ToString();
                String NOSSONUMERO = dtFBOLETO.Rows[0]["NOSSONUMERO"].ToString();
                String CODCONTA = dtFBOLETO.Rows[0]["CODCONTA"].ToString();

                String consultaFCONVENIO = "SELECT * FROM FCONVENIO WHERE CODEMPRESA = ? AND CODCONVENIO = ?";
                System.Data.DataTable dtFCONVENIO = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaFCONVENIO, new Object[] { CODEMPRESA, CODCONVENIO });
                String CARTEIRA = dtFCONVENIO.Rows[0]["CARTEIRA"].ToString();

                String consultaFCONTA = "SELECT CODBANCO FROM FCONTA WHERE CODEMPRESA = ? AND CODCONTA = ?";
                System.Data.DataTable dtFCONTA = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaFCONTA, new Object[] { CODEMPRESA, CODCONTA });
                String CODBANCO = dtFCONTA.Rows[0]["CODBANCO"].ToString();

                if (CODBANCO.Equals("237"))
                {
                    String DVNOSSONUMERO = Modulo11Base7(AppLib.Util.Format.CompletarZeroEsquerda(3, CARTEIRA) + AppLib.Util.Format.CompletarZeroEsquerda(11, NOSSONUMERO));
                    String comando = "UPDATE FBOLETO SET DVNOSSONUMERO = ? WHERE CODEMPRESA = ? AND CODLANCA = ?";
                    int temp = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comando, new Object[] { DVNOSSONUMERO, CODEMPRESA, CODLANCA });
                }
            }
        }
        public static int Modulo10(String texto)
        {
            /* Variáveis
             * -------------
             * d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            int d, s = 0, p = 2, r;

            for (int i = texto.Length; i > 0; i--)
            {
                r = (Convert.ToInt32(Microsoft.VisualBasic.Strings.Mid(texto, i, 1)) * p);

                if (r > 9)
                    r = (r / 10) + (r % 10);

                s += r;

                if (p == 2)
                    p = 1;
                else
                    p = p + 1;
            }

            d = ((10 - (s % 10)) % 10);

            return d;
        }
        public static int Modulo11Base9(String CODIGOBARRAS)
        {
            int Modulo = 11;
            int Base = 9;

            int BaseLoop = 2;
            int Soma = 0;

            for (int i = (CODIGOBARRAS.Length - 1); i != -1; i--)
            {
                int Atual = int.Parse(CODIGOBARRAS[i].ToString());
                Soma += (Atual * BaseLoop);
                BaseLoop++;

                if (BaseLoop > Base)
                {
                    BaseLoop = 2;
                }
            }

            Decimal Resto = (Convert.ToDecimal(Soma) % Convert.ToDecimal(Modulo));
            Decimal Result = Convert.ToDecimal(Modulo) - Resto;

            if (Result == 0)
            {
                return 1;
            }

            if (Result > 9)
            {
                return 1;
            }

            return Convert.ToInt32(Result);
        }
        public static String Modulo11Base7(String TEXTO)
        {
            int Modulo = 11;
            int Base = 7;

            int BaseLoop = 2;
            int Soma = 0;

            for (int i = (TEXTO.Length - 1); i != -1; i--)
            {
                int Atual = int.Parse(TEXTO[i].ToString());
                Soma += (Atual * BaseLoop);
                BaseLoop++;

                if (BaseLoop > Base)
                {
                    BaseLoop = 2;
                }
            }

            Decimal Resto = (Convert.ToDecimal(Soma) % Convert.ToDecimal(Modulo));

            if (Resto == 0)
            {
                return "0";
            }

            if (Resto == 1)
            {
                return "P";
            }

            Decimal Result = Convert.ToDecimal(Modulo) - Resto;
            return Convert.ToInt32(Result) + "";
        }
        public static void GerarCodigoBarras(int CODEMPRESA, int CODLANCA)
        {
            try
            {
                String consultaFBOLETO = "SELECT * FROM FBOLETO WHERE CODEMPRESA = ? AND CODLANCA = ?";
                System.Data.DataTable dtFBOLETO = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaFBOLETO, new Object[] { CODEMPRESA, CODLANCA });

                if (dtFBOLETO.Rows.Count > 0)
                {
                    String CODCONVENIO = dtFBOLETO.Rows[0]["CODCONVENIO"].ToString();
                    String CODCONTA = dtFBOLETO.Rows[0]["CODCONTA"].ToString();
                    DateTime DATAVENCIMENTO = Convert.ToDateTime(dtFBOLETO.Rows[0]["DATAVENCIMENTO"]);
                    Decimal VALOR = Convert.ToDecimal(dtFBOLETO.Rows[0]["VALOR"]);
                    String NOSSONUMERO = dtFBOLETO.Rows[0]["NOSSONUMERO"].ToString();

                    String consultaFCONVENIO = "SELECT * FROM FCONVENIO WHERE CODEMPRESA = ? AND CODCONTA = ?";
                    System.Data.DataTable dtFCONVENIO = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaFCONVENIO, new Object[] { CODEMPRESA, CODCONTA });

                    String CARTEIRA;

                    if (dtFCONVENIO.Rows[0]["CARTEIRA"] != DBNull.Value)
                    {
                        CARTEIRA = dtFCONVENIO.Rows[0]["CARTEIRA"].ToString();
                    }
                    else
                    {
                        throw new Exception("Informe o código da carteira no cadastro do convênio " + CODCONVENIO);
                    }

                    String consultaFCONTA = "SELECT * FROM FCONTA WHERE CODEMPRESA = ? AND CODCONTA = ?";
                    System.Data.DataTable dtFCONTA = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaFCONTA, new Object[] { CODEMPRESA, CODCONTA });

                    if (dtFCONTA.Rows.Count > 0)
                    {
                        String CODBANCO = dtFCONTA.Rows[0]["CODBANCO"].ToString();
                        String CODAGENCIA = dtFCONTA.Rows[0]["CODAGENCIA"].ToString();
                        String DVAGENCIA = "0";
                        String NUMCONTA = dtFCONTA.Rows[0]["NUMCONTA"].ToString();

                        String consultaFCCORRENTE = "SELECT * FROM FCCORRENTE WHERE CODEMPRESA = ? AND CODBANCO = ? AND CODAGENCIA = ?";
                        System.Data.DataTable dtFCCORRENTE = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaFCCORRENTE, new Object[] { CODEMPRESA, CODBANCO, CODAGENCIA });

                        String DVCONTA;

                        if (dtFCCORRENTE.Rows.Count > 0)
                        {
                            DVCONTA = dtFCCORRENTE.Rows[0]["DIGCONTA"].ToString();
                        }
                        else
                        {
                            throw new Exception("Conta corrente não encontrada no cadastro da conta/caixa " + CODCONTA);
                        }

                        // Bradesco
                        if (CODBANCO.Equals("237"))
                        {
                            String antes = CODBANCO;
                            antes += "9";

                            int FatorVencimento = DATAVENCIMENTO.Subtract(new DateTime(1997, 10, 7)).Days;
                            if (DATAVENCIMENTO >= new DateTime(2025, 2, 22))
                            {
                                FatorVencimento -= 9000;
                            }

                            String depois = AppLib.Util.Format.CompletarZeroEsquerda(4, FatorVencimento.ToString());
                            depois += AppLib.Util.Format.CompletarZeroEsquerda(10, AppLib.Util.Format.Decimal(2, VALOR));

                            depois += AppLib.Util.Format.CompletarZeroEsquerda(4, CODAGENCIA);
                            depois += AppLib.Util.Format.CompletarZeroEsquerda(2, CARTEIRA);
                            depois += AppLib.Util.Format.CompletarZeroEsquerda(11, NOSSONUMERO);
                            depois += AppLib.Util.Format.CompletarZeroEsquerda(7, NUMCONTA);
                            depois += AppLib.Util.Format.CompletarZeroEsquerda(1, DVCONTA);

                            String digito = Modulo11Base9(antes + depois).ToString();

                            String CODIGOBARRAS = antes + digito + depois;

                            if (CODIGOBARRAS.Length == 44)
                            {
                                String comandoCODIGOBARRAS = "UPDATE FBOLETO SET CODIGOBARRAS = ? WHERE CODEMPRESA = ? AND CODLANCA = ?";
                                int tempCODIGOBARRAS = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comandoCODIGOBARRAS, new Object[] { CODIGOBARRAS, CODEMPRESA, CODLANCA });
                            }
                            else
                            {
                                throw new Exception("Erro na composição do código de barras.");
                            }
                        }
                        // Santander
                        if (CODBANCO.Equals("033"))
                        {
                            String antes = CODBANCO;
                            antes += "9";

                            int FatorVencimento = DATAVENCIMENTO.Subtract(new DateTime(1997, 10, 7)).Days;
                            if (DATAVENCIMENTO >= new DateTime(2025, 2, 22))
                            {
                                FatorVencimento -= 9000;
                            }

                            String depois = AppLib.Util.Format.CompletarZeroEsquerda(4, FatorVencimento.ToString());
                            depois += AppLib.Util.Format.CompletarZeroEsquerda(10, AppLib.Util.Format.Decimal(2, VALOR));

                            depois += AppLib.Util.Format.CompletarZeroEsquerda(4, CODAGENCIA);
                            depois += AppLib.Util.Format.CompletarZeroEsquerda(2, CARTEIRA);
                            depois += AppLib.Util.Format.CompletarZeroEsquerda(11, NOSSONUMERO);
                            depois += AppLib.Util.Format.CompletarZeroEsquerda(7, NUMCONTA);
                            depois += AppLib.Util.Format.CompletarZeroEsquerda(1, DVCONTA);

                            String digito = Modulo11Base9(antes + depois).ToString();

                            String CODIGOBARRAS = antes + digito + depois;

                            if (CODIGOBARRAS.Length == 44)
                            {
                                String comandoCODIGOBARRAS = "UPDATE FBOLETO SET CODIGOBARRAS = ? WHERE CODEMPRESA = ? AND CODLANCA = ?";
                                int tempCODIGOBARRAS = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comandoCODIGOBARRAS, new Object[] { CODIGOBARRAS, CODEMPRESA, CODLANCA });
                            }
                            else
                            {
                                throw new Exception("Erro na composição do código de barras.");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public static void GerarIPTE(int CODEMPRESA, int CODLANCA)
        {
            String consulta = "SELECT * FROM FBOLETO WHERE CODEMPRESA = ? AND CODLANCA = ?";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(consulta, new Object[] { CODEMPRESA, CODLANCA });
            String CODIGOBARRAS = dt.Rows[0]["CODIGOBARRAS"].ToString();

            String CODBANCO = CODIGOBARRAS.Substring(0, 3);

            // Bradesco
            if (CODBANCO.Equals("237"))
            {
                String B1A = CODIGOBARRAS.Substring(0, 4);
                String B1B = CODIGOBARRAS.Substring(19, 5);
                int D1 = Modulo10(B1A + B1B);

                String B2 = CODIGOBARRAS.Substring(24, 10);
                int D2 = Modulo10(B2);

                String B3 = CODIGOBARRAS.Substring(34, 10);
                int D3 = Modulo10(B3);

                String B4 = CODIGOBARRAS.Substring(4, 15);

                String IPTE = B1A + B1B + D1 + B2 + D2 + B3 + D3 + B4;

                String comando = "UPDATE FBOLETO SET IPTE = ? WHERE CODEMPRESA = ? AND CODLANCA = ?";
                int temp = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comando, new Object[] { IPTE, CODEMPRESA, CODLANCA });
            }
        }
        public static void GerarArquivoRemessa(int CODEMPRESA, List<int> CODLANCA, String CODCONTA)
        {
            String consultaFCONTA = "SELECT * FROM FCONTA WHERE CODEMPRESA = ? AND CODCONTA = ?";
            System.Data.DataTable dtFCONTA = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaFCONTA, new Object[] { CODEMPRESA, CODCONTA });
            String CODAGENCIA = dtFCONTA.Rows[0]["CODAGENCIA"].ToString();

            String CODBANCO = dtFCONTA.Rows[0]["CODBANCO"].ToString();

            String consultaFCCORRENTE = "SELECT * FROM FCCORRENTE WHERE CODEMPRESA = ? AND CODBANCO = ? AND CODAGENCIA = ?";
            System.Data.DataTable dtFCCORRENTE = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaFCCORRENTE, new Object[] { CODEMPRESA, CODBANCO, CODAGENCIA });
            String NUMCONTA = dtFCCORRENTE.Rows[0]["NUMCONTA"].ToString();
            String DIGCONTA = dtFCCORRENTE.Rows[0]["DIGCONTA"].ToString();

            #region BRADESCO
            if (CODBANCO.Equals("237"))
            {
                #region ANTES

                System.Data.DataTable dtGEMPRESA = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GEMPRESA WHERE CODEMPRESA = ?", new Object[] { CODEMPRESA });
                String RAZAOSOCIAL = dtGEMPRESA.Rows[0]["NOME"].ToString();

                String CODCONVENIO = AppLib.Context.poolConnection.Get("Start").ExecGetField(String.Empty, "SELECT CODCONVENIO FROM FBOLETO WHERE CODEMPRESA = ? AND CODLANCA = ?", new Object[] { CODEMPRESA, CODLANCA[0] }).ToString();

                System.Data.DataTable dtFCONVENIO = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FCONVENIO WHERE CODEMPRESA = ? AND CODCONVENIO = ?", new Object[] { CODEMPRESA, CODCONVENIO });
                String CODIGOCEDENTE = dtFCONVENIO.Rows[0]["CODIGOCEDENTE"].ToString();
                String DIGITOCEDENTE = dtFCONVENIO.Rows[0]["DIGITOCEDENTE"].ToString();
                String CARTEIRA = dtFCONVENIO.Rows[0]["CARTEIRA"].ToString();

                int NSEQREMESSA = 1;
                if (dtFCONVENIO.Rows[0]["NSEQREMESSA"] != DBNull.Value)
                {
                    NSEQREMESSA = int.Parse(dtFCONVENIO.Rows[0]["NSEQREMESSA"].ToString());
                }

                int contador = 1;

                #endregion

                #region CABEÇALHO

                String cabecalho = "01REMESSA01COBRANCA       ";
                cabecalho += AppLib.Util.Format.CompletarZeroEsquerda(20, CODIGOCEDENTE + DIGITOCEDENTE);
                cabecalho += AppLib.Util.Format.CompletarEspacoDireita(30, AppLib.Util.Format.Esquerda(30, RAZAOSOCIAL));
                cabecalho += "237BANCO BRADESCO ";
                cabecalho += AppLib.Util.Format.DataAA(DateTime.Now);
                cabecalho += "        MX";
                cabecalho += AppLib.Util.Format.CompletarZeroEsquerda(7, NSEQREMESSA.ToString());
                cabecalho += AppLib.Util.Format.CompletarEspacoDireita(277, "");

                cabecalho += AppLib.Util.Format.CompletarZeroEsquerda(6, contador.ToString());
                contador++;

                cabecalho += "\r\n";

                #endregion

                #region DETALHE

                String detalhe = "";

                for (int i = 0; i < CODLANCA.Count; i++)
                {
                    String consultaFBOLETO = "SELECT * FROM FBOLETO WHERE CODEMPRESA = ? AND CODLANCA = ?";
                    System.Data.DataTable dtFBOLETO = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaFBOLETO, new Object[] { CODEMPRESA, CODLANCA[i] });
                    String NOSSONUMERO = dtFBOLETO.Rows[0]["NOSSONUMERO"].ToString();
                    String DVNOSSONUMERO = dtFBOLETO.Rows[0]["DVNOSSONUMERO"].ToString();
                    String NUMERO = dtFBOLETO.Rows[0]["NUMERO"].ToString();
                    DateTime DATAEMISSAO = Convert.ToDateTime(dtFBOLETO.Rows[0]["DATAEMISSAO"]);
                    DateTime DATAVENCIMENTO = Convert.ToDateTime(dtFBOLETO.Rows[0]["DATAVENCIMENTO"]);
                    Decimal VALOR = Convert.ToDecimal(dtFBOLETO.Rows[0]["VALOR"]);
                    String CODCLIFOR = dtFBOLETO.Rows[0]["CODCLIFOR"].ToString();

                    String consultaVCLIFOR = "SELECT * FROM VCLIFOR WHERE CODEMPRESA = ? AND CODCLIFOR = ?";
                    System.Data.DataTable dtVCLIFOR = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaVCLIFOR, new Object[] { CODEMPRESA, CODCLIFOR });
                    int FISICOJURIDICO = int.Parse(dtVCLIFOR.Rows[0]["FISICOJURIDICO"].ToString());
                    String CGCCPF = dtVCLIFOR.Rows[0]["CGCCPF"].ToString();
                    String NOME = dtVCLIFOR.Rows[0]["NOME"].ToString();
                    String RUAPAG = dtVCLIFOR.Rows[0]["RUAPAG"].ToString();
                    String NUMEROPAG = dtVCLIFOR.Rows[0]["NUMEROPAG"].ToString();
                    String CODCIDADEPAG = dtVCLIFOR.Rows[0]["CODCIDADEPAG"].ToString();
                    String CODETDPAG = dtVCLIFOR.Rows[0]["CODETDPAG"].ToString();
                    String CEPPAG = dtVCLIFOR.Rows[0]["CEPPAG"].ToString();

                    String consultaGCIDADE = "SELECT NOME FROM GCIDADE WHERE CODETD = ? AND CODCIDADE = ?";

                    //System.Data.DataTable dtGCIDADE = AppLib.Context.poolConnection.Get("Start").ExecGetField(null, consultaGCIDADE, new Object[] { CODETDPAG, CODCIDADEPAG });
                    String CIDADE = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, consultaGCIDADE, new Object[] { CODETDPAG, CODCIDADEPAG }).ToString();

                    detalhe += "10000000000000000000";
                    detalhe += "0";
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(3, CARTEIRA);
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(5, CODAGENCIA);
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(7, NUMCONTA);
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(1, DIGCONTA);
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(3, CODEMPRESA.ToString());
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(22, CODLANCA[i].ToString());
                    detalhe += "000";
                    detalhe += "2";
                    detalhe += "0200";
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(11, NOSSONUMERO);
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(1, AppLib.Util.Format.Esquerda(1, DVNOSSONUMERO));
                    detalhe += "0000000000";
                    detalhe += "2";
                    detalhe += "S";
                    detalhe += "          ";
                    detalhe += " ";
                    detalhe += "0";
                    detalhe += "  ";
                    detalhe += "01";
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(10, AppLib.Util.Format.Direita(10, NUMERO));
                    detalhe += AppLib.Util.Format.DataAA(DATAVENCIMENTO);
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(13, AppLib.Util.Format.Decimal(2, VALOR));
                    detalhe += "000";
                    detalhe += "00000";
                    detalhe += "01";
                    detalhe += "N";
                    detalhe += AppLib.Util.Format.DataAA(DATAEMISSAO);
                    detalhe += "06";
                    detalhe += "05";
                    if (string.IsNullOrEmpty(dtFCONVENIO.Rows[0]["PERCENTUALJUROSDIA"].ToString()))
                    {
                        detalhe += "0000000000000";
                    }
                    else
                    {
                        detalhe += AppLib.Util.Format.CompletarZeroEsquerda(13, (string.Format("{0:n2}", VALOR * Convert.ToDecimal(dtFCONVENIO.Rows[0]["PERCENTUALJUROSDIA"]) / 100)).ToString().Replace(",", "").Replace(".", ""));
                    }
                    //detalhe += "0000000000000";
                    detalhe += "000000";
                    detalhe += "0000000000000";
                    detalhe += "0000000000000";
                    detalhe += "0000000000000";

                    if (FISICOJURIDICO == 1)
                    {
                        detalhe += "01";
                    }

                    if (FISICOJURIDICO == 0)
                    {
                        detalhe += "02";
                    }

                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(14, AppLib.Util.Format.RemoveCharSpeciais(CGCCPF));
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(40, AppLib.Util.Format.Esquerda(40, NOME));
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(18, AppLib.Util.Format.Esquerda(18, RUAPAG));
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(6, AppLib.Util.Format.Esquerda(6, NUMEROPAG));
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(14, AppLib.Util.Format.Esquerda(14, CIDADE));
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(2, CODETDPAG);
                    detalhe += "            ";
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(8, AppLib.Util.Format.RemoveCharSpeciais(CEPPAG).Replace(" ", ""));
                    detalhe += "SUJEITO A PROTESTO APOS VENCIMENTO                          ";

                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(6, contador.ToString());
                    contador++;

                    detalhe += "\r\n";

                    #region SETA A REMESSA

                    String comandoCODREMESSA = "UPDATE FBOLETO SET CODREMESSA = ?, DATAREMESSA = GETDATE(), IDBOLETOSTATUS = 1 WHERE CODEMPRESA = ? AND CODLANCA = ?";
                    int tempCODREMESSA = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comandoCODREMESSA, new Object[] { NSEQREMESSA, CODEMPRESA, CODLANCA[i] });

                    #endregion

                }

                #endregion

                #region RODAPÉ

                String rodape = "9";
                rodape += "                                                                                                                                                                                                                                                                                                                                                                                                         ";
                rodape += AppLib.Util.Format.CompletarZeroEsquerda(6, contador.ToString());

                #endregion

                #region DEPOIS

                NSEQREMESSA++;
                String comandoNSEQREMESSA = "UPDATE FCONVENIO SET NSEQREMESSA = ? WHERE CODEMPRESA = ? AND CODCONVENIO = ?";
                int tempNSEQREMESSA = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comandoNSEQREMESSA, new Object[] { NSEQREMESSA, CODEMPRESA, CODCONVENIO });

                String conteudo = cabecalho + detalhe + rodape;

                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.Filter = "*.txt|Arquivo CNAB";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.File.WriteAllText(sfd.FileName, conteudo, Encoding.Default);
                }

                #endregion

            }
            #endregion

            #region SANTANDER
            else if (CODBANCO.Equals("033"))
            {
                decimal valorTotal = 0;
                int contadorItem = 0;

                #region ANTES

                System.Data.DataTable dtGEMPRESA = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GEMPRESA WHERE CODEMPRESA = ?", new Object[] { CODEMPRESA });
                String RAZAOSOCIAL = dtGEMPRESA.Rows[0]["NOME"].ToString();

                String CODCONVENIO = AppLib.Context.poolConnection.Get("Start").ExecGetField(String.Empty, "SELECT CODCONVENIO FROM FBOLETO WHERE CODEMPRESA = ? AND CODLANCA = ?", new Object[] { CODEMPRESA, CODLANCA[0] }).ToString();

                System.Data.DataTable dtFCONVENIO = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FCONVENIO WHERE CODEMPRESA = ? AND CODCONVENIO = ?", new Object[] { CODEMPRESA, CODCONVENIO });
                String CODIGOCEDENTE = dtFCONVENIO.Rows[0]["CODIGOCEDENTE"].ToString();
                String DIGITOCEDENTE = dtFCONVENIO.Rows[0]["DIGITOCEDENTE"].ToString();
                String CARTEIRA = dtFCONVENIO.Rows[0]["CARTEIRA"].ToString();

                int NSEQREMESSA = 1;
                if (dtFCONVENIO.Rows[0]["NSEQREMESSA"] != DBNull.Value)
                {
                    NSEQREMESSA = int.Parse(dtFCONVENIO.Rows[0]["NSEQREMESSA"].ToString());
                }

                int contador = 1;

                #endregion

                #region CABEÇALHO

                String cabecalho = "01REMESSA01COBRANCA       ";
                cabecalho += AppLib.Util.Format.CompletarEspacoDireita(20, dtFCONVENIO.Rows[0]["IDENTIFICADORREMESSA"].ToString());
                cabecalho += AppLib.Util.Format.CompletarEspacoDireita(30, AppLib.Util.Format.Esquerda(30, RAZAOSOCIAL));
                cabecalho += "033SANTANDER      ";
                cabecalho += AppLib.Util.Format.DataAA(DateTime.Now);
                cabecalho += "0000000000000000";
                cabecalho += "                                                                                                                                                                                                                                                                                   ";
                cabecalho += AppLib.Util.Format.CompletarZeroEsquerda(9, contador.ToString());
                //cabecalho += AppLib.Util.Format.CompletarZeroEsquerda(20, CODIGOCEDENTE + DIGITOCEDENTE);



                //cabecalho += "          ";
                //cabecalho += AppLib.Util.Format.CompletarZeroEsquerda(7, NSEQREMESSA.ToString());
                //cabecalho += AppLib.Util.Format.CompletarEspacoDireita(277, "");


                contador++;

                cabecalho += "\r\n";

                #endregion

                #region DETALHE

                String detalhe = "";

                for (int i = 0; i < CODLANCA.Count; i++)
                {
                    String consultaFBOLETO = "SELECT * FROM FBOLETO WHERE CODEMPRESA = ? AND CODLANCA = ?";
                    System.Data.DataTable dtFBOLETO = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaFBOLETO, new Object[] { CODEMPRESA, CODLANCA[i] });
                    String NOSSONUMERO = dtFBOLETO.Rows[0]["NOSSONUMERO"].ToString();
                    String DVNOSSONUMERO = dtFBOLETO.Rows[0]["DVNOSSONUMERO"].ToString();
                    String NUMERO = dtFBOLETO.Rows[0]["NUMERO"].ToString();
                    DateTime DATAEMISSAO = Convert.ToDateTime(dtFBOLETO.Rows[0]["DATAEMISSAO"]);
                    DateTime DATAVENCIMENTO = Convert.ToDateTime(dtFBOLETO.Rows[0]["DATAVENCIMENTO"]);
                    Decimal VALOR = Convert.ToDecimal(dtFBOLETO.Rows[0]["VALOR"]);
                    valorTotal = valorTotal + VALOR;
                    String CODCLIFOR = dtFBOLETO.Rows[0]["CODCLIFOR"].ToString();

                    String consultaVCLIFOR = "SELECT * FROM VCLIFOR WHERE CODEMPRESA = ? AND CODCLIFOR = ?";
                    System.Data.DataTable dtVCLIFOR = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaVCLIFOR, new Object[] { CODEMPRESA, CODCLIFOR });
                    int FISICOJURIDICO = int.Parse(dtVCLIFOR.Rows[0]["FISICOJURIDICO"].ToString());
                    String CGCCPF = dtVCLIFOR.Rows[0]["CGCCPF"].ToString();
                    String NOME = dtVCLIFOR.Rows[0]["NOME"].ToString();
                    String RUAPAG = dtVCLIFOR.Rows[0]["RUAPAG"].ToString();
                    String NUMEROPAG = dtVCLIFOR.Rows[0]["NUMEROPAG"].ToString();
                    String CODCIDADEPAG = dtVCLIFOR.Rows[0]["CODCIDADEPAG"].ToString();
                    String CODETDPAG = dtVCLIFOR.Rows[0]["CODETDPAG"].ToString();
                    String CEPPAG = dtVCLIFOR.Rows[0]["CEPPAG"].ToString();

                    String consultaGCIDADE = "SELECT NOME FROM GCIDADE WHERE CODETD = ? AND CODCIDADE = ?";

                    //System.Data.DataTable dtGCIDADE = AppLib.Context.poolConnection.Get("Start").ExecGetField(null, consultaGCIDADE, new Object[] { CODETDPAG, CODCIDADEPAG });
                    String CIDADE = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, consultaGCIDADE, new Object[] { CODETDPAG, CODCIDADEPAG }).ToString();

                    detalhe += "1";   //Posição 001-001
                    if (dtVCLIFOR.Rows[0]["FISICOJURIDICO"].ToString() == "0")   //Posição 002-003
                    {
                        detalhe += "02";
                    }
                    else
                    {
                        detalhe += "01";
                    }


                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(14, AppLib.Util.Format.RemoveCharSpeciais(AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CGCCPF FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[]{AppLib.Context.Empresa, dtFBOLETO.Rows[0]["CODFILIAL"].ToString()}).ToString()));   //Posição 004-017


                    detalhe += dtFCONVENIO.Rows[0]["IDENTIFICADORREMESSA"].ToString();   //Posição 018-037
                    //detalhe += AppLib.Util.Format.CompletarZeroEsquerda(4, CODAGENCIA);
                    //detalhe += AppLib.Util.Format.CompletarZeroEsquerda(8, CODIGOCEDENTE + DIGITOCEDENTE);
                    //detalhe += AppLib.Util.Format.CompletarZeroEsquerda(8, dtFCONVENIO.Rows[0]["CODIGOCEDENTE"].ToString().Replace(" ", ""));//NUMCONTA.Replace(" ", ""));
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(2, CODEMPRESA.ToString());   //Posição 038-062
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(23, CODLANCA[i].ToString());   //Posição 038-062
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(7, NOSSONUMERO);   //Posição 063-069
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(1, DVNOSSONUMERO);   //Posição 070-070
                    detalhe += "000000";   //Posição 071-076
                    detalhe += " ";   //Posição 077-077

                    //  detalhe += "0";

                    if (!string.IsNullOrEmpty(dtFCONVENIO.Rows[0]["INFORMACAOMULTA"].ToString()))   //Posição 078-078
                    {
                        if (Convert.ToBoolean(dtFCONVENIO.Rows[0]["INFORMACAOMULTA"]) == false)
                        {
                            detalhe += "0";
                        }
                        else
                        {
                            detalhe += "4";
                        }
                    }
                    else
                    {
                        detalhe += "0";
                    }
                    //detalhe += Convert.ToBoolean(dtFCONVENIO.Rows[0]["INFORMACAOMULTA"].ToString()) == false ? "0" : "4";
                    detalhe += string.IsNullOrEmpty(dtFCONVENIO.Rows[0]["PERCENTUALMULTA"].ToString()) ? "0000" : AppLib.Util.Format.CompletarZeroEsquerda(4, dtFCONVENIO.Rows[0]["PERCENTUALMULTA"].ToString().Replace(".", "").Replace(",", ""));    //Posição 079-082
                    detalhe += "00";   //Posição 083-084
                    detalhe += "0000000000000";   //Posição 085-097
                    detalhe += "    ";   //Posição 098-101

                    if (string.IsNullOrEmpty(dtFCONVENIO.Rows[0]["DIASMULTA"].ToString()) || Convert.ToDecimal(dtFCONVENIO.Rows[0]["DIASMULTA"]) == 0)    //Posição 102-107
                    {
                        detalhe += "000000";
                    }
                    else
                    {
                        DateTime dataMulta = DATAVENCIMENTO.AddDays(Convert.ToInt32(dtFCONVENIO.Rows[0]["DIASMULTA"]));
                        detalhe += AppLib.Util.Format.DataAA(dataMulta);
                    }

                    detalhe += CARTEIRA;   //Posição 108-108
                    detalhe += "01";   //Posição 109-110
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(10, AppLib.Util.Format.Direita(10, NUMERO));   //Posição 111-120
                    detalhe += AppLib.Util.Format.DataAA(DATAVENCIMENTO);   //Posição 121-126
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(13, AppLib.Util.Format.Decimal(2, VALOR));   //Posição 127-139
                    detalhe += "033";   //Posição 140-142
                    detalhe += "00000";   //Posição 143-147
                    detalhe += "01";   //Posição 148-149
                    detalhe += "N";  //Posição 150-150
                    detalhe += AppLib.Util.Format.DataAA(DATAEMISSAO);  //Posição 151-156
                    detalhe += "00";  //Posição 157-158
                    detalhe += dtFCONVENIO.Rows[0]["INSTRUCAOPROTESTO"];  //Posição 159-160

                    if (string.IsNullOrEmpty(dtFCONVENIO.Rows[0]["PERCENTUALJUROSDIA"].ToString()))   //Posição 161-173
                    {
                        detalhe += "0000000000000";
                    }
                    else
                    {
                        detalhe +=  AppLib.Util.Format.CompletarZeroEsquerda(13, string.Format("{0:n2}", (VALOR * Convert.ToDecimal(dtFCONVENIO.Rows[0]["PERCENTUALJUROSDIA"]) /100)).ToString().Replace(",","").Replace(".",""));
                    }
                    detalhe += AppLib.Util.Format.DataAA(DATAVENCIMENTO);   //Posição 174-179
                    detalhe += "000000000000000000000000000000000000000";   //Posição 180-218
                    if (FISICOJURIDICO == 1)   //Posição 219-220
                    {
                        detalhe += "01";
                    }

                    if (FISICOJURIDICO == 0)   //Posição 219-220
                    {
                        detalhe += "02";
                    }  
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(14, AppLib.Util.Format.RemoveCharSpeciais(CGCCPF));  //Posição 221-234
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(40, AppLib.Util.Format.Esquerda(40, NOME));  //Posição 235-274
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(35, AppLib.Util.Format.Esquerda(35, RUAPAG));  //Posição 275-314
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(5, AppLib.Util.Format.Esquerda(5, NUMEROPAG));  //Posição 275-314
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(12, AppLib.Util.Format.Esquerda(12, dtVCLIFOR.Rows[0]["BAIRROPAG"].ToString()));  //Posição 315-326
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(8, AppLib.Util.Format.RemoveCharSpeciais(CEPPAG).Replace(" ", ""));  //Posição 327-334
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(15, AppLib.Util.Format.Esquerda(15, CIDADE));  //Posição 335-349
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(2, CODETDPAG);  //Posição 350-351
                    detalhe += "                               ";   //Posição 352-381
                    detalhe += dtFCONVENIO.Rows[0]["IDENTIFICADORCOMPLEMENTO"].ToString();  //Posição 383-383
                    detalhe += dtFCONVENIO.Rows[0]["COMPLEMENTO"].ToString();   //Posição 384-385
                    detalhe += "      ";   //Posição 386-391
                    detalhe += dtFCONVENIO.Rows[0]["DIASPROTESTO"];   //Posição 392-393
                    detalhe += " ";   //Posição 394-394
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(6, contador.ToString());   //Posição 395-400



                    contador++;

                    detalhe += "\r\n";

                    #region SETA A REMESSA

                    String comandoCODREMESSA = "UPDATE FBOLETO SET CODREMESSA = ?, DATAREMESSA = GETDATE(), IDBOLETOSTATUS = 1 WHERE CODEMPRESA = ? AND CODLANCA = ?";
                    int tempCODREMESSA = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comandoCODREMESSA, new Object[] { NSEQREMESSA, CODEMPRESA, CODLANCA[i] });

                    #endregion

                }

                #endregion

                #region RODAPÉ

                String rodape = "9";
                //rodape += AppLib.Util.Format.CompletarZeroEsquerda(6, (CODLANCA.Count + 2).ToString());
                rodape += AppLib.Util.Format.CompletarZeroEsquerda(6, contador.ToString());
                rodape += AppLib.Util.Format.CompletarZeroEsquerda(13, valorTotal.ToString().Replace(",", "").Replace(".", ""));
                rodape += AppLib.Util.Format.CompletarZeroEsquerda(374, "0");
                rodape += AppLib.Util.Format.CompletarZeroEsquerda(6, contador.ToString());

                #endregion

                #region DEPOIS

                NSEQREMESSA++;
                String comandoNSEQREMESSA = "UPDATE FCONVENIO SET NSEQREMESSA = ? WHERE CODEMPRESA = ? AND CODCONVENIO = ?";
                int tempNSEQREMESSA = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comandoNSEQREMESSA, new Object[] { NSEQREMESSA, CODEMPRESA, CODCONVENIO });

                String conteudo = cabecalho + detalhe + rodape;

                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.Filter = "*.txt|Arquivo CNAB";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.File.WriteAllText(sfd.FileName, conteudo, Encoding.Default);
                }

                #endregion
            }
            #endregion

            #region ITAU
            else if (CODBANCO.Equals("341"))
            {
                #region ANTES

                System.Data.DataTable dtGEMPRESA = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GEMPRESA WHERE CODEMPRESA = ?", new Object[] { CODEMPRESA });
                String RAZAOSOCIAL = dtGEMPRESA.Rows[0]["NOME"].ToString();
                String CGCEMPRESA = AppLib.Util.Format.RemoveCharSpeciais(dtGEMPRESA.Rows[0]["CGCCPF"].ToString());

                String CODCONVENIO = AppLib.Context.poolConnection.Get("Start").ExecGetField(String.Empty, "SELECT CODCONVENIO FROM FBOLETO WHERE CODEMPRESA = ? AND CODLANCA = ?", new Object[] { CODEMPRESA, CODLANCA[0] }).ToString();

                System.Data.DataTable dtFCONVENIO = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FCONVENIO WHERE CODEMPRESA = ? AND CODCONVENIO = ?", new Object[] { CODEMPRESA, CODCONVENIO });
                String CODIGOCEDENTE = dtFCONVENIO.Rows[0]["CODIGOCEDENTE"].ToString();
                String DIGITOCEDENTE = dtFCONVENIO.Rows[0]["DIGITOCEDENTE"].ToString();
                String CARTEIRA = dtFCONVENIO.Rows[0]["CARTEIRA"].ToString();

                int NSEQREMESSA = 1;
                if (dtFCONVENIO.Rows[0]["NSEQREMESSA"] != DBNull.Value)
                {
                    NSEQREMESSA = int.Parse(dtFCONVENIO.Rows[0]["NSEQREMESSA"].ToString());
                }

                int contador = 1;

                #endregion

                #region CABEÇALHO

                String cabecalho = "01REMESSA01COBRANCA       ";
                cabecalho += AppLib.Util.Format.CompletarZeroEsquerda(4, CODAGENCIA);
                cabecalho += "00";
                cabecalho += AppLib.Util.Format.CompletarZeroEsquerda(5, CODIGOCEDENTE);
                cabecalho += AppLib.Util.Format.CompletarZeroEsquerda(1, DIGITOCEDENTE);
                cabecalho += "        ";
                cabecalho += AppLib.Util.Format.CompletarEspacoDireita(30, AppLib.Util.Format.Esquerda(30, RAZAOSOCIAL));
                cabecalho += CODBANCO;
                cabecalho += AppLib.Util.Format.Direita(15, "BANCO ITAU SA  ");
                cabecalho += AppLib.Util.Format.DataAA(DateTime.Now);
                cabecalho += AppLib.Util.Format.CompletarEspacoDireita(294, "");
                cabecalho += AppLib.Util.Format.CompletarZeroEsquerda(6, contador.ToString());
                contador++;

                cabecalho += "\r\n";

                #endregion

                #region DETALHE

                String detalhe = "";

                for (int i = 0; i < CODLANCA.Count; i++)
                {
                    String consultaFBOLETO = "SELECT * FROM FBOLETO WHERE CODEMPRESA = ? AND CODLANCA = ?";
                    System.Data.DataTable dtFBOLETO = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaFBOLETO, new Object[] { CODEMPRESA, CODLANCA[i] });
                    String NOSSONUMERO = dtFBOLETO.Rows[0]["NOSSONUMERO"].ToString();
                    String DVNOSSONUMERO = dtFBOLETO.Rows[0]["DVNOSSONUMERO"].ToString();
                    String NUMERO = dtFBOLETO.Rows[0]["NUMERO"].ToString();
                    DateTime DATAEMISSAO = Convert.ToDateTime(dtFBOLETO.Rows[0]["DATAEMISSAO"]);
                    DateTime DATAVENCIMENTO = Convert.ToDateTime(dtFBOLETO.Rows[0]["DATAVENCIMENTO"]);
                    Decimal VALOR = Convert.ToDecimal(dtFBOLETO.Rows[0]["VALOR"]);
                    //Decimal VLJUROS = Convert.ToDecimal(dtFBOLETO.Rows[0]["VLJUROS"]);
                    String CODCLIFOR = dtFBOLETO.Rows[0]["CODCLIFOR"].ToString();
                    int ACEITE = int.Parse(dtFBOLETO.Rows[0]["ACEITE"].ToString());

                    String consultaVCLIFOR = "SELECT *, (SUBSTRING(COALESCE(RUAPAG,''),1,(40 - (LEN (COALESCE(', ' + NUMEROPAG,'')) + LEN(CASE WHEN COMPLEMENTOPAG = '' THEN '' WHEN COMPLEMENTOPAG IS NULL THEN '' ELSE '-'+ SUBSTRING(COMPLEMENTOPAG,1,10) END)))) + COALESCE(',' + NUMEROPAG,'') + CASE WHEN COMPLEMENTOPAG = '' THEN '' WHEN COMPLEMENTOPAG IS NULL THEN '' ELSE '-'+ SUBSTRING(COMPLEMENTOPAG,1,10) END) AS ENDERECOPAG FROM VCLIFOR WHERE CODEMPRESA = ? AND CODCLIFOR = ?";
                    System.Data.DataTable dtVCLIFOR = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaVCLIFOR, new Object[] { CODEMPRESA, CODCLIFOR });
                    int FISICOJURIDICO = int.Parse(dtVCLIFOR.Rows[0]["FISICOJURIDICO"].ToString());
                    String CGCCPF = AppLib.Util.Format.RemoveCharSpeciais(dtVCLIFOR.Rows[0]["CGCCPF"].ToString());
                    String NOME = dtVCLIFOR.Rows[0]["NOME"].ToString();
                    String RUAPAG = dtVCLIFOR.Rows[0]["RUAPAG"].ToString();
                    String COMPLEMENTOPAG = dtVCLIFOR.Rows[0]["COMPLEMENTOPAG"].ToString();
                    String NUMEROPAG = dtVCLIFOR.Rows[0]["NUMEROPAG"].ToString();
                    String BAIRROPAG = dtVCLIFOR.Rows[0]["BAIRROPAG"].ToString();
                    String CEPPAG = AppLib.Util.Format.RemoveCharSpeciais(dtVCLIFOR.Rows[0]["CEPPAG"].ToString());
                    String CODCIDADEPAG = dtVCLIFOR.Rows[0]["CODCIDADEPAG"].ToString();
                    String CODETDPAG = dtVCLIFOR.Rows[0]["CODETDPAG"].ToString();
                    String ENDERECOPAG = dtVCLIFOR.Rows[0]["ENDERECOPAG"].ToString();

                    String consultaGCIDADE = "SELECT NOME FROM GCIDADE WHERE CODETD = ? AND CODCIDADE = ?";

                    //System.Data.DataTable dtGCIDADE = AppLib.Context.poolConnection.Get("Start").ExecGetField(null, consultaGCIDADE, new Object[] { CODETDPAG, CODCIDADEPAG });
                    String CIDADE = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, consultaGCIDADE, new Object[] { CODETDPAG, CODCIDADEPAG }).ToString();

                    detalhe += "1";

                    if (CGCEMPRESA.Length == 14)
                    {
                        detalhe += "01";
                    }
                    else
                    {
                        detalhe += "02";
                    }

                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(14, CGCEMPRESA);
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(4, CODAGENCIA);
                    detalhe += "00";
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(5, CODIGOCEDENTE);
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(1, DIGITOCEDENTE);
                    detalhe += "    ";
                    detalhe += "0000";
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(3, CODEMPRESA.ToString());
                    detalhe += "0";
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(21, CODLANCA[i].ToString());
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(8, NOSSONUMERO);
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(13, "0");
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(3, CARTEIRA);
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(21, "");
                    detalhe += "I";
                    detalhe += "01";
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(10, AppLib.Util.Format.Direita(10,NUMERO));
                    detalhe += AppLib.Util.Format.DataAA(DATAVENCIMENTO);
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(13, AppLib.Util.Format.Decimal(2, VALOR));
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(3, CODBANCO);
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(5, "0");
                    detalhe += "01";

                    if (ACEITE == 1)
                    {
                        detalhe += "A";
                    }
                    else
                    {
                        detalhe += "N";
                    }

                    detalhe += AppLib.Util.Format.DataAA(DATAEMISSAO);
                    detalhe += "00"; // instrução 1
                    detalhe += "00"; // instrução 2
                    //detalhe += AppLib.Util.Format.CompletarZeroEsquerda(13, AppLib.Util.Format.Decimal(2, VLJUROS));
                    detalhe += "0000000000000";
                    detalhe += "000000"; // data limite desconto
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(13, "0"); // valor desconto
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(13, "0"); // valor conta
                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(13, "0"); // valor mora

                    if (CGCCPF.Length == 14)
                    {
                        detalhe += "01";
                    }
                    else
                    {
                        detalhe += "02";
                    }

                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(14, CGCCPF);
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(40, AppLib.Util.Format.Esquerda(40, NOME));
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(40, AppLib.Util.Format.Esquerda(40, ENDERECOPAG));
                    //detalhe += AppLib.Util.Format.CompletarEspacoDireita(23, AppLib.Util.Format.Esquerda(23, RUAPAG));
                    //detalhe += AppLib.Util.Format.CompletarEspacoDireita(12, AppLib.Util.Format.Esquerda(12, COMPLEMENTOPAG));
                    //detalhe += AppLib.Util.Format.CompletarEspacoDireita(5, NUMEROPAG);
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(12, AppLib.Util.Format.Esquerda(12,BAIRROPAG));
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(8, AppLib.Util.Format.Esquerda(8,CEPPAG));
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(15, AppLib.Util.Format.Esquerda(15, CIDADE));
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(2, CODETDPAG);
                    detalhe += AppLib.Util.Format.CompletarEspacoDireita(34, "");
                    detalhe += "000000"; // data de mora
                    detalhe += "05";
                    detalhe += " ";

                    detalhe += AppLib.Util.Format.CompletarZeroEsquerda(6, contador.ToString());
                    contador++;

                    detalhe += "\r\n";

                    #region SETA A REMESSA

                    String comandoCODREMESSA = "UPDATE FBOLETO SET CODREMESSA = ?, DATAREMESSA = GETDATE(), IDBOLETOSTATUS = 1 WHERE CODEMPRESA = ? AND CODLANCA = ?";
                    int tempCODREMESSA = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comandoCODREMESSA, new Object[] { NSEQREMESSA, CODEMPRESA, CODLANCA[i] });

                    #endregion

                }

                #endregion

                #region RODAPÉ

                String rodape = "9";
                rodape += AppLib.Util.Format.CompletarEspacoDireita(393, "");
                rodape += AppLib.Util.Format.CompletarZeroEsquerda(6, contador.ToString());

                #endregion

                #region DEPOIS

                NSEQREMESSA++;
                String comandoNSEQREMESSA = "UPDATE FCONVENIO SET NSEQREMESSA = ? WHERE CODEMPRESA = ? AND CODCONVENIO = ?";
                int tempNSEQREMESSA = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comandoNSEQREMESSA, new Object[] { NSEQREMESSA, CODEMPRESA, CODCONVENIO });

                String conteudo = cabecalho + detalhe + rodape;

                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.Filter = "*.txt|Arquivo CNAB";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.File.WriteAllText(sfd.FileName, conteudo, Encoding.Default);
                }

                #endregion
            }

            #endregion
        }
    }
}
