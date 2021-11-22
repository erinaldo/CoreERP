using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PS.Lib;
using System.Net;
using System.Net.Mail;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;


namespace PS.Glb
{
    public class PSPartNFEstadualData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Glb.Formula.Function function = new PS.Glb.Formula.Function();
        private PS.Lib.Interpretador interpreta = new PS.Lib.Interpretador();

        // Variáveis para validação
        public string Email;
        public bool Cliente;

        public bool EnvioAutomatico = true;

        public override string ReadView()
        {


            return @"SELECT * FROM (

SELECT GNFESTADUAL.CODEMPRESA,
GNFESTADUAL.CODSTATUS,
GNFESTADUAL.CODOPER,
GOPER.CODTIPOPER,
GOPER.NUMERO,
GOPER.CODSERIE,
GOPER.DATAEMISSAO,
GOPER.CODFILIAL,
GOPER.CODCLIFOR,
VCLIFOR.NOME,
GNFESTADUAL.CHAVEACESSO,
GNFESTADUAL.RECIBO,
GNFESTADUAL.DATARECIBO,
GNFESTADUAL.DATAPROTOCOLO,
GNFESTADUAL.PROTOCOLO,
GNFESTADUAL.IDOUTBOX,
GNFESTADUAL.XMLREC,
GNFESTADUAL.XMLPROT,
GNFESTADUAL.XMLNFE,
CONVERT(BIT, DANFEIMPRESSA) DANFEIMPRESSA,
CONVERT(BIT, EMAILENVIADO) EMAILENVIADO,
FORMATOIMPRESSAO,
VCLIFOR.NOMEFANTASIA

FROM GNFESTADUAL, GOPER, VCLIFOR
WHERE GNFESTADUAL.CODEMPRESA = GOPER.CODEMPRESA
AND GNFESTADUAL.CODOPER = GOPER.CODOPER
AND GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA
AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR) 

GNFESTADUAL WHERE  ";
        }

        public void RegistraDANFEImpressa(int CodEmpresa, int CodOper)
        {
            string sSql = @"UPDATE GNFESTADUAL SET DANFEIMPRESSA = ? WHERE CODEMPRESA = ? AND CODOPER = ?";
            dbs.QueryExec(sSql, 1, CodEmpresa, CodOper).ToString();
        }

        public void RegistraEmailEnviado(int CodEmpresa, int CodOper)
        {
            string sSql = @"UPDATE GNFESTADUAL SET EMAILENVIADO = ? WHERE CODEMPRESA = ? AND CODOPER = ?";
            dbs.QueryExec(sSql, 1, CodEmpresa, CodOper).ToString();
        }

        public string ExportarXML(int CodEmpresa, int CodOper)
        {
            string sSql = @"SELECT XMLNFE FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?";
            return dbs.QueryValue(string.Empty, sSql, CodEmpresa, CodOper).ToString();
        }

        public System.Data.DataTable GetOperacao(int CodEmpresa, int CodOper)
        {
            System.Data.DataTable dados;
            string sSql = @"SELECT NUMERO, CODSERIE, CODCLIFOR, DATAEMISSAO, CODFILIAL, CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";
            dados = dbs.QuerySelect(sSql, CodEmpresa, CodOper);
            return dados;
        }

        public void RegistraHistorico(int CodEmpresa, int CodOper, string Observacao)
        {
            List<PS.Lib.DataField> objHist = new List<DataField>();

            PS.Lib.DataField HST_CODEMPRESA = new DataField("CODEMPRESA", CodEmpresa);
            PS.Lib.DataField HST_CODOPER = new DataField("CODOPER", CodOper);
            PS.Lib.DataField HST_IDHISTORICO = new DataField("IDHISTORICO", 0, null, Global.TypeAutoinc.Max);
            PS.Lib.DataField HST_DATA = new DataField("DATA", dbs.GetServerDateTimeNow());
            PS.Lib.DataField HST_CODUSUARIO = new DataField("CODUSUARIO", PS.Lib.Contexto.Session.CodUsuario);
            PS.Lib.DataField HST_OBSERVACAO = new DataField("OBSERVACAO", Observacao);

            objHist.Add(HST_CODEMPRESA);
            objHist.Add(HST_CODOPER);
            objHist.Add(HST_IDHISTORICO);
            objHist.Add(HST_DATA);
            objHist.Add(HST_CODUSUARIO);
            objHist.Add(HST_OBSERVACAO);

            PSPartNFEstadualHistoricoData psPartNFEstadualHistoricoData = new PSPartNFEstadualHistoricoData();
            psPartNFEstadualHistoricoData._tablename = "GNFESTADUALHISTORICO";
            psPartNFEstadualHistoricoData._keys = new string[] { "CODEMPRESA", "CODOPER", "IDHISTORICO" };

            psPartNFEstadualHistoricoData.SaveRecord(objHist);
        }

        public void CancelarNFe(int CodEmpresa, int CodOper, string Motivo)
        {
            try
            {
                string sSql = string.Empty;

                PS.Validate.Services.NFeSrv nfeSrv = new Validate.Services.NFeSrv();
                List<DataField> objArrRet = nfeSrv.CancelarNFe(CodEmpresa, CodOper, Motivo);

                List<PS.Lib.DataField> objEvento = new List<DataField>();

                PS.Lib.DataField EVT_CODEMPRESA = new DataField("CODEMPRESA", CodEmpresa);
                PS.Lib.DataField EVT_CODOPER = new DataField("CODOPER", CodOper);
                PS.Lib.DataField EVT_IDEVENTO = new DataField("IDEVENTO", 0, null, Global.TypeAutoinc.Max);
                PS.Lib.DataField EVT_TPEVENTO = new DataField("TPEVENTO", "110111");
                PS.Lib.DataField EVT_JUSTIFICATIVA = new DataField("JUSTIFICATIVA", Motivo);
                PS.Lib.DataField EVT_CODSTATUS = new DataField("CODSTATUS", "P");
                PS.Lib.DataField EVT_DATA = new DataField("DATA", dbs.GetServerDateTimeNow());
                PS.Lib.DataField EVT_CODUSUARIO = new DataField("CODUSUARIO", PS.Lib.Contexto.Session.CodUsuario);

                objEvento.Add(EVT_CODEMPRESA);
                objEvento.Add(EVT_CODOPER);
                objEvento.Add(EVT_IDEVENTO);
                objEvento.Add(EVT_TPEVENTO);
                objEvento.Add(EVT_JUSTIFICATIVA);
                objEvento.Add(EVT_CODSTATUS);
                objEvento.Add(EVT_DATA);
                objEvento.Add(EVT_CODUSUARIO);

                PSPartNFEstadualEventoData psPartNFEstadualEventoData = new PSPartNFEstadualEventoData();
                psPartNFEstadualEventoData._tablename = "GNFESTADUALEVENTO";
                psPartNFEstadualEventoData._keys = new string[] { "CODEMPRESA", "CODOPER", "IDEVENTO" };
                psPartNFEstadualEventoData.SaveRecord(objEvento);
                //ALTERAÇÃO DO STATUS NA TABELA GNFESTADUAL
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET CODSTATUS = ? WHERE CODOPER = ?", new object[] { "P", CodOper });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EnviarXML(int CodEmpresa, int CodOper, bool transp)
        {
            try
            {
                System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();
                if (PARAMVAREJO == null)
                {
                    throw new Exception("Não foi possível carregar os parâmetros do módulo.");
                }
                else
                {                 
                    string sSql = string.Empty;
                    string ChaveAcesso = string.Empty;
                    string Numero = string.Empty;
                    string Serie = string.Empty;
                    string EmailFrom = string.Empty;
                    string emailTransp = string.Empty;
                    string emailCliente = string.Empty;
                    MailMessage mail = new MailMessage();

                    if (EnvioAutomatico == false)
                    {
                        sSql = @"SELECT EMAILNFE FROM VCLIFOR, GOPER WHERE VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR AND GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ?";
                        emailCliente = dbs.QueryValue(string.Empty, sSql, CodEmpresa, CodOper).ToString();
                        mail.To.Add(emailCliente);
                    }

                    // Verifica se o Checkbox de enviar pro remetente do cadastro do cliente esta marcado e se o TextBox do email esta preenchido, se sim, ele busca o remetente no cliente e preenche o restante com o digitado no Textbox.
                    if (Cliente == true && !string.IsNullOrEmpty(Email))
                    {
                        sSql = @"SELECT EMAILNFE FROM VCLIFOR, GOPER WHERE VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR AND GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ?";
                        emailCliente = dbs.QueryValue(string.Empty, sSql, CodEmpresa, CodOper).ToString();
                        mail.To.Add(emailCliente+","+ Email);
                    }

                    // Verifica se o Checkbox de enviar pro remetente do cadastro do cliente esta DESmarcado e se o Textbox do email esta preenchidp, se sim, Ele envia o que estiver neste.
                    if (Cliente == false && !string.IsNullOrEmpty(Email))
                    {
                        mail.To.Add(Email);
                    }

                    // Verifica se o Checkbox de enviar pro remetente do cadastro do cliente esta marcado e se o Textbox do email esta vazio, sem sim, envia pro remetente do cadastro.
                    if (Cliente == true && string.IsNullOrEmpty(Email))
                    {
                        sSql = @"SELECT EMAILNFE FROM VCLIFOR, GOPER WHERE VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR AND GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ?";
                        emailCliente = dbs.QueryValue(string.Empty, sSql, CodEmpresa, CodOper).ToString();
                        mail.To.Add(emailCliente);
                    }

                    // Entrará neste bloco de código nos casos de envio automático.
                    //if (Cliente == false && string.IsNullOrEmpty(Email))
                    //{
                    //    sSql = @"SELECT EMAILNFE FROM VCLIFOR, GOPER WHERE VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR AND GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ?";
                    //    emailCliente = dbs.QueryValue(string.Empty, sSql, CodEmpresa, CodOper).ToString();
                    //    mail.To.Add(emailCliente);
                    //}

                    //Verifica se envia para o transportador
                    if (transp.Equals(true))
                    {
                        sSql = @"SELECT VTRANSPORTADORA.EMAIL FROM VTRANSPORTADORA INNER JOIN GOPER ON VTRANSPORTADORA.CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA AND VTRANSPORTADORA.CODEMPRESA = GOPER.CODEMPRESA WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ?";
                        emailTransp = dbs.QueryValue(string.Empty, sSql, CodEmpresa, CodOper).ToString();
                        if (!string.IsNullOrEmpty(emailTransp))
                        {
                            mail.To.Add(emailTransp);
                        }
                        if (mail.To.Count == 0)
                        {
                            throw new Exception("Não existe destinatário selecionado.");
                        }
                    }
                    //Busca a informação do email da empresa
                    int EmailRem = (PARAMVAREJO["EMAILREMETENTE"] == DBNull.Value) ? 0 : Convert.ToInt32(PARAMVAREJO["EMAILREMETENTE"]);
                    if (EmailRem == 0)
                    {
                        sSql = @"SELECT EMAIL FROM GEMPRESA WHERE CODEMPRESA = ?";
                        EmailFrom = dbs.QueryValue(string.Empty, sSql, CodEmpresa).ToString();
                    }
                    else if (EmailRem == 1)
                    {
                        sSql = @"SELECT EMAIL FROM GUSUARIO WHERE CODUSUARIO = ?";
                        EmailFrom = dbs.QueryValue(string.Empty, sSql, PS.Lib.Contexto.Session.CodUsuario).ToString();
                    }
                    //Verifica se existe um emailFrom
                    if (string.IsNullOrEmpty(EmailFrom))
                    {
                        throw new Exception("E-mail do remetente não informado.");
                    }
                    //Exporta o XML 
                    string sXML = this.ExportarXML(CodEmpresa, CodOper);
                    //Verifica se Exportou
                    if (string.IsNullOrEmpty(sXML))
                    {
                        throw new Exception("O XML da Nota Fiscal não foi encontrado.");
                    }
                    //Atribui em Memory
                    MemoryStream ArqXML = new MemoryStream(Encoding.UTF8.GetBytes(PS.Lib.Utils.PreparaXML(sXML) ?? ""));
                    //MemoryStream ArqPDF = new MemoryStream();
                    //Abre o relatório
                    List<DataField> Param = new List<DataField>();
                    Param.Add(new DataField("CODEMPRESA", CodEmpresa));
                    Param.Add(new DataField("CODOPER", CodOper));
                    //Verifica qual o parametro pra saber qual relatório deve exportar.
                    string nomeRelatorio = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CLASSNAME FROM GTIPOPERREPORT 
INNER JOIN GOPER ON GTIPOPERREPORT.CODTIPOPER = GOPER.CODTIPOPER
INNER JOIN GREPORT ON GTIPOPERREPORT.CODREPORT = GREPORT.CODREPORT
WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ? ", new object[] { CodOper, CodEmpresa }).ToString();
                    sSql = @"SELECT NUMERO FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";
                    Numero = dbs.QueryValue(string.Empty, sSql, CodEmpresa, CodOper).ToString();
                    if (!string.IsNullOrEmpty(nomeRelatorio))
                    {
                        switch (nomeRelatorio)
                        {
                            case "StReportDanfePaisagem":
                                Relatorios.XrDanfePaisagem reportPaisagem = new Relatorios.XrDanfePaisagem(Param);
                                //Exporta para a pasta TEMP
                                reportPaisagem.ExportToPdf(@"C:\Windows\Temp\NFE" + Numero + ".pdf");
                                break;
                            case "StReportDanfe":
                                Relatorios.XrDanfe report = new Relatorios.XrDanfe(Param);
                                //Exporta para a pasta TEMP
                                report.ExportToPdf(@"C:\Windows\Temp\NFE" + Numero + ".pdf");
                                break;
                            default:
                                break;
                        }
                    }

                    string sBody = PARAMVAREJO["TEXTOEMAILNFE"].ToString();
                    string sSubject = "NF-e " + Numero;


                    //EmailFrom, EmailTo
                    MailAddress from = new MailAddress(EmailFrom, AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOMEFANTASIA FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, 1 }).ToString());
                    mail.From = from;

                    mail.Subject = sSubject;
                    mail.Body = sBody;

                    mail.Attachments.Add(new Attachment(@"C:\Windows\Temp\NFE" + Numero + ".pdf", System.Net.Mime.MediaTypeNames.Application.Pdf));
                    mail.Attachments.Add(new Attachment(ArqXML, "NFE" + Numero + ".xml"));

                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Timeout = 3600000;
                    smtp.Host = PARAMVAREJO["EMAILHOST"].ToString();
                    smtp.EnableSsl = Convert.ToInt32(PARAMVAREJO["EMAILUSASSL"]) == 1 ? true : false;
                    //smtp.EnableSsl = false;
                    NetworkCredential NetworkCred = new NetworkCredential(PARAMVAREJO["EMAILUSUARIO"].ToString(), PARAMVAREJO["EMAILSENHA"].ToString());
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = Convert.ToInt32(PARAMVAREJO["EMAILPORTA"].ToString());
                    smtp.Send(mail);
                    this.RegistraEmailEnviado(CodEmpresa, CodOper);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EnviarNFe(int CodEmpresa, int CodOper)
        {
            //try
            //{
            string sSql = string.Empty;

            PS.Validate.Services.NFeSrv nfeSrv = new Validate.Services.NFeSrv();
            List<DataField> objArrRet = new List<DataField>();
            try
            {

                if (Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, @"SELECT USACODAUXNFE FROM GTIPOPER INNER JOIN GOPER ON GTIPOPER.CODEMPRESA = GOPER.CODEMPRESA AND GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER =?", new object[] { CodEmpresa, CodOper })) == true)
                {
                    nfeSrv.usaCodAuxiliar = true;
                }
                objArrRet = nfeSrv.EnviarNFe(CodEmpresa, CodOper);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }

            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArrRet, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArrRet, "CODOPER");
            PS.Lib.DataField dfIDOUTBOX = gb.RetornaDataFieldByCampo(objArrRet, "IDOUTBOX");
            PS.Lib.DataField dfCHAVEACESSO = gb.RetornaDataFieldByCampo(objArrRet, "CHAVEACESSO");

            sSql = "SELECT * FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?";
            objArrRet = gb.RetornaDataFieldByDataRow(dbs.QuerySelect(sSql, CodEmpresa, CodOper).Rows[0]);
            PS.Lib.DataField dfCODSTATUS = gb.RetornaDataFieldByCampo(objArrRet, "CODSTATUS");
            if (dfCODSTATUS.Valor.ToString() == "P" || dfCODSTATUS.Valor.ToString() == "E")
            {
                for (int i = 0; i < objArrRet.Count; i++)
                {
                    if (objArrRet[i].Field == "IDOUTBOX")
                    {
                        objArrRet[i].Valor = dfIDOUTBOX.Valor;
                    }

                    if (objArrRet[i].Field == "CHAVEACESSO")
                    {
                        objArrRet[i].Valor = dfCHAVEACESSO.Valor;
                    }

                    if (objArrRet[i].Field == "CODSTATUS")
                    {
                        objArrRet[i].Valor = "P";
                    }
                }
                try
                {
                    this.SaveRecord(objArrRet);
                }
                catch (Exception)
                {

                    throw new Exception("salvar ------ ");
                }

                try
                {
                    //this.RegistraHistorico(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODOPER.Valor), "NF-e Aguardando Autorização");
                }
                catch (Exception)
                {

                    throw new Exception("historico ------ ");
                }

            }
            else
            {
                throw new Exception("Nota Fiscal não pode ser enviada pois não esta pendente.");
            }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("envia nfe erro");
            //}
        }

        public void ConsultaSituacaoNFe(List<DataField> objArr)
        {
            try
            {
                string sSql = string.Empty;


                for (int i = 0; i < objArr.Count; i++)
                {
                    objArr[i].Field = objArr[i].Field.Replace("GOPER.", "");
                    //   row1.Table.Columns[i].ColumnName = row1.Table.Columns[i].ColumnName.Replace("GOPER.", "");
                }

                PS.Validate.Services.NFeSrv nfeSrv = new Validate.Services.NFeSrv();
                List<DataField> objArrRet = nfeSrv.ConsultaSituacaoNFe(objArr);

                PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArrRet, "CODEMPRESA");
                PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArrRet, "CODOPER");
                PS.Lib.DataField dfCODSTATUS = gb.RetornaDataFieldByCampo(objArrRet, "CODSTATUS");
                PS.Lib.DataField dfDATA = gb.RetornaDataFieldByCampo(objArrRet, "DATA");
                PS.Lib.DataField dfOBSERVACAO = gb.RetornaDataFieldByCampo(objArrRet, "OBSERVACAO");
                PS.Lib.DataField dfRECIBO = gb.RetornaDataFieldByCampo(objArrRet, "RECIBO");
                PS.Lib.DataField dfDATARECIBO = gb.RetornaDataFieldByCampo(objArrRet, "DATARECIBO");
                PS.Lib.DataField dfPROTOCOLO = gb.RetornaDataFieldByCampo(objArrRet, "PROTOCOLO");
                PS.Lib.DataField dfDATAPROTOCOLO = gb.RetornaDataFieldByCampo(objArrRet, "DATAPROTOCOLO");
                PS.Lib.DataField dfXMLREC = gb.RetornaDataFieldByCampo(objArrRet, "XMLREC");
                PS.Lib.DataField dfXMLPROT = gb.RetornaDataFieldByCampo(objArrRet, "XMLPROT");
                PS.Lib.DataField dfXMLNFE = gb.RetornaDataFieldByCampo(objArrRet, "XMLNFE");

                sSql = "SELECT * FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?";
                objArrRet = gb.RetornaDataFieldByDataRow(dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor).Rows[0]);

                if (dfOBSERVACAO.Valor == null)
                {
                    dfOBSERVACAO.Valor = string.Empty;
                }

                this.RegistraHistorico(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODOPER.Valor), dfOBSERVACAO.Valor.ToString());

                #region Seleção do Status

                for (int i = 0; i < objArrRet.Count; i++)
                {
                    switch (objArrRet[i].Field)
                    {
                        case "CODSTATUS":
                            switch (dfCODSTATUS.Valor.ToString())
                            {
                                case "ENV":
                                    objArrRet[i].Valor = "U";
                                    break;
                                case "CON":
                                    objArrRet[i].Valor = "U";
                                    break;
                                case "ERR":
                                    objArrRet[i].Valor = "E";
                                    break;
                                case "VAL":
                                    objArrRet[i].Valor = "A";
                                    break;
                                case "CAN":
                                    objArrRet[i].Valor = "C";
                                    break;
                                case "INU":
                                    objArrRet[i].Valor = "I";
                                    break;
                                case "DEN":
                                    objArrRet[i].Valor = "D";
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "RECIBO":
                            objArrRet[i].Valor = dfRECIBO.Valor;
                            break;
                        case "DATARECIBO":
                            objArrRet[i].Valor = dfDATARECIBO.Valor;
                            break;
                        case "PROTOCOLO":
                            objArrRet[i].Valor = dfPROTOCOLO.Valor;
                            break;
                        case "DATAPROTOCOLO":
                            objArrRet[i].Valor = dfDATAPROTOCOLO.Valor;
                            break;
                        case "XMLREC":
                            objArrRet[i].Valor = dfXMLREC.Valor;
                            break;
                        case "XMLPROT":
                            objArrRet[i].Valor = dfXMLPROT.Valor;
                            break;
                        case "XMLNFE":
                            objArrRet[i].Valor = dfXMLNFE.Valor;
                            break;
                        default:
                            break;
                    }

                    #region Código Antigo Comentado


                    //if (objArrRet[i].Field == "CODSTATUS")
                    //{
                    //    string sStatus = string.Empty;
                    //    if (dfCODSTATUS.Valor.ToString() == "ENV" || dfCODSTATUS.Valor.ToString() == "CON")
                    //        sStatus = "U";
                    //    if (dfCODSTATUS.Valor.ToString() == "ERR")
                    //        sStatus = "E";
                    //    if (dfCODSTATUS.Valor.ToString() == "VAL")
                    //        sStatus = "A";
                    //    if (dfCODSTATUS.Valor.ToString() == "CAN")
                    //        sStatus = "C";
                    //    if (dfCODSTATUS.Valor.ToString() == "INU")
                    //        sStatus = "I";
                    //    if (dfCODSTATUS.Valor.ToString() == "DEN")
                    //        sStatus = "D";
                    //    objArrRet[i].Valor = sStatus;
                    //}
                    //if (objArrRet[i].Field == "RECIBO")
                    //{
                    //    objArrRet[i].Valor = dfRECIBO.Valor;
                    //}
                    //if (objArrRet[i].Field == "DATARECIBO")
                    //{
                    //    objArrRet[i].Valor = dfDATARECIBO.Valor;
                    //}
                    //if (objArrRet[i].Field == "PROTOCOLO")
                    //{
                    //    objArrRet[i].Valor = dfPROTOCOLO.Valor;
                    //}
                    //if (objArrRet[i].Field == "DATAPROTOCOLO")
                    //{
                    //    objArrRet[i].Valor = dfDATAPROTOCOLO.Valor;
                    //}
                    //if (objArrRet[i].Field == "XMLREC")
                    //{
                    //    objArrRet[i].Valor = dfXMLREC.Valor;
                    //}
                    //if (objArrRet[i].Field == "XMLPROT")
                    //{
                    //    objArrRet[i].Valor = dfXMLPROT.Valor;
                    //}
                    //if (objArrRet[i].Field == "XMLNFE")
                    //{
                    //    objArrRet[i].Valor = dfXMLNFE.Valor;
                    //}

                    #endregion

                }

                #endregion

                this.SaveRecord(objArrRet);
                if (dfCODSTATUS.Valor.ToString() == "CAN")
                {
                    PSPartOperacaoData ps = new PSPartOperacaoData();
                    ps.CancelaOperacao(objArrRet);
                }

                string codStatusCan = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODSTATUS FROM GNFESTADUALCANC WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { dfCODOPER.Valor, dfCODEMPRESA.Valor }).ToString();
                if (codStatusCan == "REJ")
                {
                    System.Windows.Forms.MessageBox.Show("Cancelamento rejeitado.", "Informação do Sistema.", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ExcluiRegistroHistorico(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            sSql = @"SELECT IDHISTORICO FROM GNFESTADUALHISTORICO WHERE CODEMPRESA = ? AND CODOPER = ?";

            System.Data.DataTable dtItem = dbs.QuerySelect(sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor);

            List<PS.Lib.DataField> ListObjArr = new List<PS.Lib.DataField>();

            if (dtItem.Rows.Count > 0)
            {
                PSPartNFEstadualHistoricoData psPartNFEstadualHistoricoData = new PSPartNFEstadualHistoricoData();
                psPartNFEstadualHistoricoData._tablename = "GNFESTADUALHISTORICO";
                psPartNFEstadualHistoricoData._keys = new string[] { "CODEMPRESA", "CODOPER", "IDHISTORICO" };

                for (int i = 0; i < dtItem.Rows.Count; i++)
                {
                    PS.Lib.DataField dtNSEQITEM = new PS.Lib.DataField("IDHISTORICO", dtItem.Rows[i]["IDHISTORICO"]);

                    ListObjArr.Add(dtCODEMPRESA);
                    ListObjArr.Add(dtCODOPER);
                    ListObjArr.Add(dtNSEQITEM);

                    psPartNFEstadualHistoricoData.DeleteRecord(ListObjArr);

                    ListObjArr.Clear();
                }
            }
        }

        public override void DeleteRecord(List<DataField> objArr)
        {
            this.ExcluiRegistroHistorico(objArr);

            base.DeleteRecord(objArr);
        }

    }
}
