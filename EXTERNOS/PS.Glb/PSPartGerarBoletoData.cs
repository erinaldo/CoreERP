using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PS.Glb
{
    public class PSPartGerarBoletoData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public void GerarBoleto(List<PS.Lib.DataField> objArr)
        {
            try
            {
                PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "FLANCA.CODEMPRESA");
                PS.Lib.DataField dfCODLANCA = gb.RetornaDataFieldByCampo(objArr, "FLANCA.CODLANCA");
                PS.Lib.DataField dfCODFILIAL = gb.RetornaDataFieldByCampo(objArr, "FLANCA.CODFILIAL");
                PS.Lib.DataField dfNUMERO = gb.RetornaDataFieldByCampo(objArr, "FLANCA.NUMERO");
                PS.Lib.DataField dfCODCLIFOR = gb.RetornaDataFieldByCampo(objArr, "FLANCA.CODCLIFOR");
                PS.Lib.DataField dfDATAEMISSAO = gb.RetornaDataFieldByCampo(objArr, "FLANCA.DATAEMISSAO");
                PS.Lib.DataField dfDATAVENCIMENTO = gb.RetornaDataFieldByCampo(objArr, "FLANCA.DATAVENCIMENTO");
                PS.Lib.DataField dfCODMOEDA = gb.RetornaDataFieldByCampo(objArr, "FLANCA.CODMOEDA");
                PS.Lib.DataField dfVALOR = gb.RetornaDataFieldByCampo(objArr, "FLANCA.VLLIQUIDO");
                PS.Lib.DataField dfCODCONTA = gb.RetornaDataFieldByCampo(objArr, "FLANCA.CODCONTA");
                PS.Lib.DataField dfCODTIPDOC = gb.RetornaDataFieldByCampo(objArr, "FLANCA.CODTIPDOC");
                PS.Lib.DataField dfTIPOPAGREC = gb.RetornaDataFieldByCampo(objArr, "FLANCA.TIPOPAGREC");

                if (dfTIPOPAGREC.Valor.ToString().Equals("0"))
                    throw new Exception("Lançamento: " + dfCODLANCA.Valor + ".\r\nNão é do tipo á receber.");

                String consulta = "SELECT * FROM FCONVENIO WHERE CODEMPRESA = ? AND CODCONTA = ?";
                System.Data.DataTable dtConvenio = this.dbs.QuerySelect(consulta, new Object[] { PS.Lib.Contexto.Session.Empresa.CodEmpresa, dfCODCONTA.Valor });

                if (dtConvenio.Rows == null)
                    throw new Exception("Lançamento: " + dfCODLANCA.Valor + ".\r\nConvênio não encontrado para a conta " + dfCODCONTA.Valor);

                if (dtConvenio.Rows.Count == 0)
                    throw new Exception("Lançamento: " + dfCODLANCA.Valor + ".\r\nParâmetro de convênio não encontrado para a conta " + dfCODCONTA.Valor);

                if (dtConvenio.Rows.Count > 1)
                    throw new Exception("Lançamento: " + dfCODLANCA.Valor + ".\r\nFoi encontrado mais de um parâmetro de convênio para a conta " + dfCODCONTA.Valor);

                String CODCONVENIO = dtConvenio.Rows[0]["CODCONVENIO"].ToString();

                try
                {
                    dbs.Begin();

                    string sSql = @"INSERT INTO FBOLETO (CODEMPRESA, CODLANCA, CODFILIAL, NUMERO, CODCLIFOR, DATAEMISSAO, DATAVENCIMENTO, DATABOLETO, CODMOEDA, VALOR, CODCONTA, CODTIPDOC, CODCONVENIO, ACEITE, IDBOLETOSTATUS) VALUES (?, ?, ?, ?, ?, ?, ?, GETDATE(), ?, ?, ?, ?, ?, 0, 0)";
                    int result = dbs.QueryExec(sSql, dfCODEMPRESA.Valor, dfCODLANCA.Valor, dfCODFILIAL.Valor, dfNUMERO.Valor, dfCODCLIFOR.Valor, dfDATAEMISSAO.Valor, dfDATAVENCIMENTO.Valor, dfCODMOEDA.Valor, dfVALOR.Valor, dfCODCONTA.Valor, dfCODTIPDOC.Valor, CODCONVENIO);

                    dbs.Commit();
                    //

                    //Chamada para Geração
                    GerarNossoNumero(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODLANCA.Valor));
                    GerarDigitoNossoNumero(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODLANCA.Valor));
                    GerarCodigoBarras(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODLANCA.Valor));
                    GerarIPTE(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODLANCA.Valor));
                    alteraStatusFlancaBoleto(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODLANCA.Valor));
                }
                catch (Exception ex)
                {
                    dbs.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region Gerar Código Boleto

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

        public static int Modulo11Base9_033(String CODIGOBARRAS)
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

            int result;

            if (Resto == 0)
            {
                return result = 0;
            }

            if (Resto == 1)
            {
                return result = 0;
            }

            if (Resto == 10)
            {
                return result = 1;
            }

            return Convert.ToInt32(Result);
        }

        public static int Modulo11Base9_033_B(String CODIGOBARRAS)
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

            int result;

            if (Resto == 0)
            {
                return result = 1;
            }

            if (Resto == 1)
            {
                return result = 1;
            }

            if (Resto == 10)
            {
                return result = 1;
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
                            //depois += AppLib.Util.Format.CompletarZeroEsquerda(1, DVCONTA); // Mudança 04/09/2018 
                            depois += "0";

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
                        else if (CODBANCO.Equals("033"))
                        {
                            String antes = "033";
                            antes += "9";
                            int FatorVencimento = DATAVENCIMENTO.Subtract(new DateTime(1997, 10, 7)).Days;
                            String depois = AppLib.Util.Format.CompletarZeroEsquerda(4, FatorVencimento.ToString());
                            depois += AppLib.Util.Format.CompletarZeroEsquerda(10, AppLib.Util.Format.Decimal(2, VALOR));
                            depois += "9";
                            depois += AppLib.Util.Format.CompletarZeroEsquerda(7, dtFCONVENIO.Rows[0]["CODIGOCEDENTE"].ToString() + dtFCONVENIO.Rows[0]["DIGITOCEDENTE"].ToString());
                            int dvNossoNumero = Modulo11Base9_033(NOSSONUMERO);

                            depois += AppLib.Util.Format.CompletarZeroEsquerda(13, NOSSONUMERO + dvNossoNumero);
                            depois += "0101";
                            String digito = Modulo11Base9_033_B(antes + depois).ToString();
                            String CODIGOBARRAS = antes + digito + depois;

                            if (CODIGOBARRAS.Length == 44)
                            {
                                String comandoCODIGOBARRAS = "UPDATE FBOLETO SET CODIGOBARRAS = ?, DVNOSSONUMERO = ? WHERE CODEMPRESA = ? AND CODLANCA = ?";
                                int tempCODIGOBARRAS = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comandoCODIGOBARRAS, new Object[] { CODIGOBARRAS, dvNossoNumero, CODEMPRESA, CODLANCA });
                            }
                            else
                            {
                                throw new Exception("Erro na composição do código de barras.");
                            }
                        }
                        else if (CODBANCO.Equals("341"))
                        {
                            String antes = "341";
                            antes += "9";
                            int FatorVencimento = DATAVENCIMENTO.Subtract(new DateTime(1997, 10, 7)).Days;
                            String depois = AppLib.Util.Format.CompletarZeroEsquerda(4, FatorVencimento.ToString());
                            depois += AppLib.Util.Format.CompletarZeroEsquerda(10, AppLib.Util.Format.Decimal(2, VALOR));
                            depois += "9";
                            depois += AppLib.Util.Format.CompletarZeroEsquerda(7, dtFCONVENIO.Rows[0]["CODIGOCEDENTE"].ToString() + dtFCONVENIO.Rows[0]["DIGITOCEDENTE"].ToString());
                            int dvNossoNumero = Modulo11Base9(NOSSONUMERO);

                            depois += AppLib.Util.Format.CompletarZeroEsquerda(13, NOSSONUMERO + dvNossoNumero);
                            depois += "0101";
                            String digito = Modulo11Base9(antes + depois).ToString();
                            String CODIGOBARRAS = antes + digito + depois;

                            if (CODIGOBARRAS.Length == 44)
                            {
                                String comandoCODIGOBARRAS = "UPDATE FBOLETO SET CODIGOBARRAS = ?, DVNOSSONUMERO = ? WHERE CODEMPRESA = ? AND CODLANCA = ?";
                                int tempCODIGOBARRAS = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comandoCODIGOBARRAS, new Object[] { CODIGOBARRAS, dvNossoNumero, CODEMPRESA, CODLANCA });
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
            else if (CODBANCO.Equals("033"))
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
            else if (CODBANCO.Equals("341"))
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
        #endregion

        public void GerarBoleto(Class.FLANCA flanca, AppLib.Data.Connection conn)
        {
            try
            {
                if (flanca.TIPOPAGREC.Equals("0"))
                {
                    throw new Exception("Lançamento: " + flanca.CODLANCA + ".\r\nNão é do tipo á receber.");
                }
                string CODCONVENIO = conn.ExecGetField(string.Empty, "SELECT CODCONVENIO FROM FCONVENIO WHERE CODEMPRESA = ? AND CODCONTA = ?", new object[] { flanca.CODEMPRESA, flanca.CODCONTA }).ToString();

                if (string.IsNullOrEmpty(CODCONVENIO))
                {
                    throw new Exception("Lançamento: " + flanca.CODLANCA + ".\r\nConvênio não encontrado para a conta " + flanca.CODCONTA);
                }

                try
                {
                    Class.FBOLETO FBOLETO = new Class.FBOLETO();
                    FBOLETO.CODEMPRESA = flanca.CODEMPRESA;
                    FBOLETO.CODLANCA = flanca.CODLANCA;
                    FBOLETO.CODFILIAL = flanca.CODFILIAL;
                    FBOLETO.NUMERO = flanca.NUMERO;
                    FBOLETO.CODCLIFOR = flanca.CODCLIFOR;
                    FBOLETO.DATAEMISSAO = flanca.DATAEMISSAO;
                    FBOLETO.DATAVENCIMENTO = flanca.DATAVENCIMENTO;
                    FBOLETO.DATABOLETO = conn.GetDateTime();
                    FBOLETO.CODMOEDA = flanca.CODMOEDA;
                    FBOLETO.VALOR = flanca.VLLIQUIDO;
                    FBOLETO.CODCONTA = flanca.CODCONTA;
                    FBOLETO.CODTIPDOC = flanca.CODTIPDOC;
                    FBOLETO.CODCONVENIO = CODCONVENIO;
                    FBOLETO.ACEITE = 0;
                    FBOLETO.IDBOLETOSTATUS = 0;
                    FBOLETO.DATAREMESSA = null;

                    if (FBOLETO.persistFBOLETO(conn, FBOLETO) == false)
                    {
                        throw new Exception("Não foi possível inserir o boleto.");
                    }

                    //Chamada para Geração
                    GerarNossoNumero(flanca.CODEMPRESA, flanca.CODLANCA);
                    GerarDigitoNossoNumero(flanca.CODEMPRESA, flanca.CODLANCA);
                    GerarCodigoBarras(flanca.CODEMPRESA, flanca.CODLANCA);
                    GerarIPTE(flanca.CODEMPRESA, flanca.CODLANCA);
                    alteraStatusFlancaBoleto(flanca.CODEMPRESA, flanca.CODLANCA);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void alteraStatusFlancaBoleto(int codEmpresa, int codlanca)
        {
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FLANCA SET BOLETOGERADO = ? WHERE CODEMPRESA = ? AND CODLANCA =?", new object[] { "SIM", codEmpresa, codlanca });
        }
    }
}
