using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Data;
using System.Net.Mail;
using System.Diagnostics;

namespace PS.Glb.Class
{
    public class NFeAPI
    {
        #region Variáveis

        private String urlEnvioNFe;
        private String urlStatusProcessamento;
        private String urlConsulta;
        private String urlDownloadNFe;
        private String urlDownloadNFeInutilizacao;
        private String urlDownloadEvento;
        private String urlInutilizacao;
        private String urlCancelamento;
        private String urlCartaCorrecao;
        
        // Variáveis para o ambiente de homologação da NS
        private String URLEnvioNFE;
        private String URLConsultaNFE;
        private String URLDownloadNFE;
        private String URLCancelamento;
        private String urlConsultaStatusNF;

        #endregion

        #region Propriedades 

        // Desenvolver caso necessário

        #endregion

        #region Construtor

        public NFeAPI()
        {
            //Atribui as urls das operações
            this.urlEnvioNFe = "https://nfe.ns.eti.br/nfe/issue";
            this.urlStatusProcessamento = "https://nfe.ns.eti.br/nfe/issue/status";
            this.urlConsulta = "https://nfe.ns.eti.br/nfe/stats";
            this.urlDownloadNFe = "https://nfe.ns.eti.br/nfe/get";
            this.urlDownloadNFeInutilizacao = "https://nfe.ns.eti.br/nfe/get/inut";
            this.urlDownloadEvento = "https://nfe.ns.eti.br/nfe/get/event";
            this.urlInutilizacao = "https://nfe.ns.eti.br/nfe/inut";
            this.urlCancelamento = "https://nfe.ns.eti.br/nfe/cancel";
            this.urlCartaCorrecao = "https://nfe.ns.eti.br/nfe/cce";

            // Variáveis provisórias pra URL
            this.URLEnvioNFE = "http://nfehml.ns.eti.br/nfe/issue";
            this.URLConsultaNFE = "http://nfehml.ns.eti.br/nfe/issue/status";
            this.URLDownloadNFE = " http://nfehml.ns.eti.br/nfe/get";
            this.URLCancelamento = "http://nfehml.ns.eti.br/nfe/cancel";
            this.urlConsultaStatusNF = "https://nfe.ns.eti.br/util/wssefazstatus";
        }

        #endregion

        #region Métodos referentes à Integração

        /// <summary>
        /// Método responsável pelo envio de conteúdo para a API.
        /// </summary>
        /// <param name="token">Token da empresa, gerado pela NS</param>
        /// <param name="conteudo">Nota do cliente</param>
        /// <param name="url">URL do serviço a ser realizado pela API</param>
        /// <param name="tpConteudo">Tipo de conteúdo</param>
        /// <returns></returns>
        public String EnviaConteudoParaAPI(String token, String conteudo, String url, String tpConteudo = "json")
        {
            String retorno = "";

            //Cria objeto para requisição e atribui as configurações necessárias para API
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers["X-AUTH-TOKEN"] = token;
            if (tpConteudo == "txt") httpWebRequest.ContentType = "text/plain";
            else if (tpConteudo == "xml") httpWebRequest.ContentType = "application/xml";
            else httpWebRequest.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(conteudo);
                streamWriter.Flush();
                streamWriter.Close();
            }

            //Envia requisição
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) retorno = streamReader.ReadToEnd();
            }
            //Se der erro
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;
                    using (var streamReader = new StreamReader(response.GetResponseStream())) retorno = streamReader.ReadToEnd();
                }
            }

            return retorno;
        }

        /// <summary>
        /// Método responsável pela emissão da NF-e.
        /// </summary>
        /// <param name="token">Token da empresa, gerado pela NS</param>
        /// <param name="conteudo">Nota do cliente</param>
        /// <param name="tpConteudo">Tipo de conteúdo</param>
        /// <returns></returns>
        public String EmitirNFe(String token, String conteudo, String tpConteudo)
        {
            return EnviaConteudoParaAPI(token, conteudo, this.urlEnvioNFe, tpConteudo);
        }

        /// <summary>
        /// Método responsável pela consulta do status de processamento da NF-e enviada anteriormente pelo método Emissão de NF-e.
        /// </summary>
        /// <param name="token">Token da empresa, gerado pela NS</param>
        /// <param name="CNPJ">CNPJ do cliente</param>
        /// <param name="nsNRec">Núemro do protocolo de recebimento da API</param>
        /// <returns></returns>
        public String ConsultaStatusProcessamento(String token, String CNPJ, String nsNRec)
        {
            //Monta JSON e envia para API
            String JSON = "{" +
                    "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                    "\"CNPJ\": \"" + CNPJ + "\"," +
                    "\"nsNRec\": \"" + nsNRec + "\"" + "}";

            //GravaLog("[CONSULTA STATUS PROCESSAMENTO]");
            //GravaLog(JSON);
            //GravaLog(EnviaConteudoParaAPI(token, JSON, this.urlStatusProcessamento, "json"));

            return EnviaConteudoParaAPI(token, JSON, this.urlStatusProcessamento, "json");
        }

        /// <summary>
        /// Método responsável pela consulta do status de uma NF-e encontrada no banco da Sefaz.
        /// </summary>
        /// <param name="token">Token da empresa, gerado pela NS</param>
        /// <param name="CNPJ">CNPJ do cliente</param>
        /// <param name="chNFe">Chave de acesso da NF-e</param>
        /// <param name="tpAmb">Tipo do ambiente</param>
        /// <returns></returns>
        public String ConsultaSituacaoNFe(String token, String CNPJ, String chNFe, String tpAmb)
        {
            String retorno = "";

            //Monta JSON e envia para API
            String JSON = "{" +
                    "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                    "\"licencaCnpj\":\"" + CNPJ + "\"," +
                    "\"chNFe\":\"" + chNFe + "\"," +
                    "\"tpAmb\":\"" + tpAmb + "\"" + "}";

            retorno = EnviaConteudoParaAPI(token, JSON, this.urlConsulta, "json");

            return retorno;
        }

        /// <summary>
        /// Método responsável pelo cancelamento da NF-e.
        /// </summary>
        /// <param name="token">Token da empresa, gerado pela NS</param>
        /// <param name="chNFe">Chave de acesso da NF-e</param>
        /// <param name="tpAmb">Tipo do ambiente</param>
        /// <param name="dhEvento">Data e hora da ocorrência do cancelamento</param>
        /// <param name="nProt">Número do protocolo de autorização da NF-e</param>
        /// <param name="xJust">Motivo do cancelamento da NF-e</param>
        /// <returns></returns>
        public String CancelamentoNFe(String token, String chNFe, String tpAmb, String dhEvento, String nProt, String xJust)
        {
            String retorno = "";

            //Monta JSON e envia para API
            String JSON = "{" +
                    "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                    "\"chNFe\":\"" + chNFe + "\"," +
                    "\"tpAmb\":\"" + tpAmb + "\"," +
                    "\"dhEvento\":\"" + dhEvento + "\"," +
                    "\"nProt\":\"" + nProt + "\"," +
                    "\"xJust\":\"" + xJust + "\"" + "}";

            retorno = EnviaConteudoParaAPI(token, JSON, this.urlCancelamento, "json");

            //GravaLog("[CANCELAMENTO]");
            //GravaLog(JSON);
            //GravaLog(retorno);

            return retorno;
        }

        /// <summary>
        /// Método responsável pelo evento de Carta de Correção da NF-e.
        /// </summary>
        /// <param name="token">Token da empresa, gerado pela NS</param>
        /// <param name="chNFe">Chave de acesso da NF-e</param>
        /// <param name="tpAmb">Tipo do ambiente</param>
        /// <param name="dhEvento">Data e hora da ocorrência do cancelamento</param>
        /// <param name="nSeqEvento">Número sequencial do evento</param>
        /// <param name="xCorrecao">Descrição da correção a ser realizada na NF-e</param>
        /// <returns></returns>
        public String CartaCorrecaoNFe(String token, String chNFe, String tpAmb, String dhEvento, String nSeqEvento, String xCorrecao)
        {
            String retorno = "";

            //Monta JSON e envia para API
            String JSON = "{" +
                    "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                    "\"chNFe\":\"" + chNFe + "\"," +
                    "\"tpAmb\":\"" + tpAmb + "\"," +
                    "\"dhEvento\":\"" + dhEvento + "\"," +
                    "\"nSeqEvento\":\"" + nSeqEvento + "\"," +
                    "\"xCorrecao\":\"" + xCorrecao + "\"" + "}";

            retorno = EnviaConteudoParaAPI(token, JSON, this.urlCartaCorrecao, "json");

            //GravaLog("CARTA DE CORREÇÃO");
            //GravaLog(JSON);
            //GravaLog(retorno);

            return retorno;
        }

        /// <summary>
        /// Método responsável pela inutilização da numeração da NF-e.
        /// </summary>
        /// <param name="token">Token da empresa, gerado pela NS</param>
        /// <param name="cUF">Código da UF do emitente</param>
        /// <param name="tpAmb">Tipo do ambiente</param>
        /// <param name="ano">Ano de inutilização da numeração</param>
        /// <param name="CNPJ">CNPJ do emitente</param>
        /// <param name="Serie">Série da NF-e</param>
        /// <param name="nNFIni">Número da NF-e inicial a ser inutilizado</param>
        /// <param name="nNFFin">Número da NF-e final a ser inutilizado</param>
        /// <param name="xJust">Justificativa do pedido de inutilização</param>
        /// <returns></returns>
        public String InutilizacaoNFe(String token, String cUF, String tpAmb, String ano, String CNPJ, String Serie, String nNFIni, String nNFFin, String xJust)
        {
            String retorno = "";

            //Monta JSON e envia para API
            String JSON = "{" +
                    "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                    "\"cUF\":\"" + cUF + "\"," +
                    "\"tpAmb\":\"" + tpAmb + "\"," +
                    "\"ano\":\"" + ano + "\"," +
                    "\"CNPJ\":\"" + CNPJ + "\"," +
                    "\"serie\":\"" + Serie + "\"," +
                    "\"nNFIni\":\"" + nNFIni + "\"," +
                    "\"nNFFin\":\"" + nNFFin + "\"," +
                    "\"xJust\":\"" + xJust + "\"" + "}";

            retorno = EnviaConteudoParaAPI(token, JSON, this.urlInutilizacao, "json");

            //GravaLog("INUTILIZACAO");
            //GravaLog(JSON);
            //GravaLog(retorno);

            return retorno;
        }

        /// <summary>
        /// Método responsável pelo download de uma NF-e enviada e consultada pela API, que foi autorizada pela Sefaz.
        /// </summary>
        /// <param name="token">Token da empresa, gerado pela NS</param>
        /// <param name="chNFe">Chave de acesso da NF-e</param>
        /// <param name="tpDown">Tipo de download</param>
        /// <returns></returns>
        public String DownloadNFe(String token, String chNFe, String tpAmb, String tpDown)
        {
            //Monta JSON e envia para API
            String JSON = "{" +
                    "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                    "\"chNFe\": \"" + chNFe + "\", " +
                     "\"tpAmb\": \"" + tpAmb + "\", " +
                    "\"tpDown\": \"" + tpDown + "\"" + "}";

            return EnviaConteudoParaAPI(token, JSON, this.urlDownloadNFe, "json");
        }

        /// <summary>
        /// Método responsável pelo download da NF-e previamente inutilizada pelo método InutilizacaoNFe.
        /// </summary>
        /// <param name="token">Token da empresa, gerado pela NS</param>
        /// <param name="chNFe">Chave de acesso da NF-e</param>
        /// <param name="tpAmb">Tipo do ambiente</param>
        /// <param name="tpDown">Tipo de download</param>
        /// <returns></returns>
        public String DownloadNFeInutilizacao(String token, String chNFe, String tpAmb, String tpDown)
        {
            //Monta JSON e envia para API
            String JSON = "{" +
                    "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                    "\"chNFe\": \"" + chNFe + "\", " +
                    "\"tpAmb\": \"" + tpAmb + "\", " +
                    "\"tpDown\": \"" + tpDown + "\"" + "}";

            return EnviaConteudoParaAPI(token, JSON, this.urlDownloadNFeInutilizacao, "json");
        }

        /// <summary>
        /// Método responsável pelo download da NF-e que possua os eventos de Cancelamento ou Carta de Correção.
        /// </summary>
        /// <param name="token">Token da empresa, gerado pela NS</param>
        /// <param name="chNFe">Chave de acesso da NF-e</param>
        /// <param name="tpAmb">Tipo do ambiente</param>
        /// <param name="tpDown">Tipo de download</param>
        /// <param name="tpEvento">Tipo do evento</param>
        /// <param name="nSeqEvento">Número sequencial do evento</param>
        /// <returns></returns>
        public String DownloadNFeEvento(String token, String chNFe, String tpAmb, String tpDown, String tpEvento, String nSeqEvento)
        {
            //Monta JSON e envia para API
            String JSON = "{" +
                    "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                    "\"chNFe\": \"" + chNFe + "\", " +
                    "\"tpAmb\": \"" + tpAmb + "\", " +
                    "\"tpDown\": \"" + tpDown + "\", " +
                    "\"tpEvento\": \"" + tpEvento + "\", " +
                    "\"nSeqEvento\": \"" + nSeqEvento + "\"" + "}";

            return EnviaConteudoParaAPI(token, JSON, this.urlDownloadEvento, "json");
        }


        //Dirlei Desenvolvimento Metodo status
        public String StatusNFOperacao(String token, String cnpj, String tpAmb, String uf, String versao)
        {
            //Monta JSON e envia para API
            String JSON = "{" +
                    "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                    "\"CNPJCont\": \"" + cnpj + "\", " +
                    "\"tpAmb\": \"" + tpAmb + "\", " +
                    "\"UF\": \"" + uf + "\", " +
                    "\"versao\": \"" + versao + "\"" + "}";


            return EnviaConteudoParaAPI(token, JSON, this.urlConsultaStatusNF, "json");
        }



        /// <summary>
        /// Método responsável pelo download da NF-e e em seguida salva o documento no caminho informado.
        /// </summary>
        /// <param name="token">Token da empresa, gerado pela NS</param>
        /// <param name="chNFe">Chave de acesso da NF-e</param>
        /// <param name="tpDown">Tipo de download</param>
        /// <param name="caminho">Local onde o documento será salvo</param>
        /// <param name="isShow">Se o PDF do documento deve ser exibido na tela após o download</param>
        /// <returns></returns>
        public String DownloadNFeAndSave(String token, String chNFe, String tpDown, String tpAmb, String caminho = "", Boolean isShow = false)
        {
            String retorno = DownloadNFe(token, chNFe, tpAmb , tpDown);

            if (caminho != "")
            {
                if (!Directory.Exists(caminho)) Directory.CreateDirectory(caminho);
                if (!caminho.EndsWith("\\")) caminho += "\\";
            }

            //Seta caminho para salvar, verifica se status 200 da API e faz o download do que foi solicitado
            String pathName = caminho + chNFe + "-procNfe";
            dynamic JSONRetorno = JsonConvert.DeserializeObject(retorno);
            String status = JSONRetorno.status;

            if (status == "200")
            {
                if (tpDown.ToUpper().Contains("X"))
                {
                    String conteudoSalvar = JSONRetorno.xml;
                    SalvaArquivo(conteudoSalvar, pathName, "X");
                }
                else
                {
                    if (tpDown.ToUpper().Contains("J"))
                    {
                        dynamic conteudoSalvar = JSONRetorno.nfeProc;
                        SalvaArquivo(Convert.ToString(conteudoSalvar), pathName, "J");
                    }
                }

                if (tpDown.ToUpper().Contains("P"))
                {
                    String conteudoSalvar = JSONRetorno.pdf;
                    SalvaArquivo(conteudoSalvar, pathName, "P");
                    if (isShow) System.Diagnostics.Process.Start(@pathName + ".pdf");
                }
            }

            return retorno;
        }

        public String DownloadNFeAndSaveEvento(String token, String chNFe, String tpDown, String tpAmb, String tpEvento, String nSeqEvento, String caminho = "", Boolean isShow = false)
        {
            String retorno = DownloadNFeEvento(token, chNFe, tpAmb, tpDown, tpEvento, nSeqEvento);

            if (caminho != "")
            {
                if (!Directory.Exists(caminho)) Directory.CreateDirectory(caminho);
                if (!caminho.EndsWith("\\")) caminho += "\\";
            }

            //Seta caminho para salvar, verifica se status 200 da API e faz o download do que foi solicitado
            String pathName = caminho + tpEvento + "-" + chNFe + "-procNfe";
            dynamic JSONRetorno = JsonConvert.DeserializeObject(retorno);
            String status = JSONRetorno.status;

            if (status == "200")
            {
                if (tpDown.ToUpper().Contains("X"))
                {
                    String conteudoSalvar = JSONRetorno.xml;
                    SalvaArquivo(conteudoSalvar, pathName, "X");
                }
                else
                {
                    if (tpDown.ToUpper().Contains("J"))
                    {
                        dynamic conteudoSalvar = JSONRetorno.nfeProc;
                        SalvaArquivo(Convert.ToString(conteudoSalvar), pathName, "J");
                    }
                }

                if (tpDown.ToUpper().Contains("P"))
                {
                    String conteudoSalvar = JSONRetorno.pdf;
                    SalvaArquivo(conteudoSalvar, pathName, "P");
                    if (isShow) System.Diagnostics.Process.Start(@pathName + ".pdf");
                }
            }

            return retorno;
        }

        /// <summary>
        /// Método responsável por salvar o documento.
        /// </summary>
        /// <param name="conteudo">Nota do cliente</param>
        /// <param name="pathName">Local onde o documento será salvo</param>
        /// <param name="tpArq">Tipo do arquivo que será salvo</param>
        private void SalvaArquivo(String conteudo, String pathName, String tpArq)
        {
            //Se XML
            if (tpArq == "X") System.IO.File.WriteAllText(@pathName + ".xml", conteudo);
            //Se JSON
            else if (tpArq == "J") System.IO.File.WriteAllText(@pathName + ".json", conteudo);
            //Se PDF
            else
            {
                try
                {
                    byte[] bytes = Convert.FromBase64String(conteudo);
                    if (File.Exists(pathName + ".pdf")) File.Delete(pathName + ".pdf");     
                    System.IO.FileStream stream = new FileStream(@pathName + ".pdf", FileMode.CreateNew);
                    System.IO.BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Close();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        #endregion

        #region Métodos 

        /// <summary>
        /// Método que cria um arquivo .txt para Log.
        /// </summary>
        /// <param name="texto">Texto para ser inserido no Log</param>
        public void GravaLog(String texto)
        {
            using (StreamWriter outputFile = new StreamWriter("logAPI.txt", true))
            {
                String data = DateTime.Now.ToShortDateString();
                String hora = DateTime.Now.ToShortTimeString();
                String computador = Dns.GetHostName();
                outputFile.WriteLine(data + " " + hora + " (" + computador + ")" + texto);
            }
        }

        /// <summary>
        /// Método para retornar o CNPJ/CPF do cliente do contexto.
        /// </summary>
        /// <returns>CNPJ/CPF</returns>
        public string getCGCCPFFormatado(int _codFilial = 1)
        {
            string CGCCPF = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CGCCPF FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, _codFilial }).ToString();

            CGCCPF = CGCCPF.Replace(".", "").Replace("-", "").Replace("/", "");

            return CGCCPF;
        }

        /// <summary>
        /// Método que retorna o Id Histórico
        /// </summary>
        /// <param name="CodOper">Código da Operação</param>
        /// <returns>Id Histórico</returns>
        public int getIdHistorico(int CodOper)
        {
            int IdHistorico;

            IdHistorico = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT ISNULL(MAX(IDHISTORICO), 1) FROM GNFESTADUALHISTORICO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CodOper }));

            if (IdHistorico >= 1)
            {
                IdHistorico++;
            }

            return IdHistorico;
        }

        /// <summary>
        /// Método que realiza o UPDATE na tabela GNFESTADUAL após a emissão da Nf-e.
        /// </summary>
        /// <param name="CodOper">Código da Operação</param>
        /// <param name="Recibo">Recibo (Recibo de integração com a NS)</param>
        /// <param name="DataRecibo">Data do Recibo</param>
        /// <param name="CodTipOper">Código do tipo de Operação</param>
        /// <param name="FormatoImpressao">Formato de impressão da DANFE</param>
        public void UpdateGNFEstadualAposEmissao(int CodOper, string Recibo, DateTime DataRecibo, string CodTipOper, int FormatoImpressao)
        {
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppLib.Context.poolConnection.Get("Start").ConnectionString);
            System.Data.SqlClient.SqlCommand command;
            try
            {
                conn.Open();

                command = new System.Data.SqlClient.SqlCommand("UPDATE GNFESTADUAL SET RECIBO = '" + Recibo + "', DATARECIBO = '" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", DataRecibo) + "', CODTIPOPER = '" + CodTipOper + "', FORMATOIMPRESSAO = '" + FormatoImpressao + "' WHERE CODEMPRESA = " + AppLib.Context.Empresa + " AND CODOPER = " + CodOper + "", conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Método que realiza o UPDATE na tabela GNFESTADUAL após a Consulta da Nf-e.
        /// </summary>
        /// <param name="CodOper">Código da Operação</param>
        /// <param name="XML">XML gerado após a consulta</param>
        /// <param name="Protocolo">Protocolo</param>
        /// <param name="DataProtocolo">Data do Protocolo</param>
        public void UpdateGNFEstadualAposConsulta(int CodOper, string XML, string Protocolo, DateTime DataProtocolo)
        {
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppLib.Context.poolConnection.Get("Start").ConnectionString);
            System.Data.SqlClient.SqlCommand command;
            try
            {
                conn.Open();

                command = new System.Data.SqlClient.SqlCommand("UPDATE GNFESTADUAL SET CODSTATUS = 'A ', XMLNFE = '" + XML + "', PROTOCOLO = '" + Protocolo + "', DATAPROTOCOLO = '" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", DataProtocolo) + "'WHERE CODEMPRESA = " + AppLib.Context.Empresa + " AND CODOPER = " + CodOper + "", conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="XML"></param>
        /// <returns></returns>
        public static string PreparaXML(string XML)
        {
            int indexi = XML.IndexOf('<', 0);
            int indexf = XML.IndexOf('>', 0);

            string substituir = XML.Substring(indexi, indexf + 1);

            if (substituir.Contains("xml version"))
                XML = XML.Replace(substituir, "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            else
                XML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + XML;

            return XML;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codoper"></param>
        /// <param name="Token"></param>
        /// <param name="TpDown"></param>
        /// <param name="Caminho"></param>
        /// <param name="Chave"></param>
        /// <param name="Numero"></param>
        public void EnvioEmail(int Codoper, string Token, string TpDown, string tpAmb, string Caminho, string Chave, string Numero)
        {
            try
            {
                string RetornoDownload = DownloadNFeAndSave(Token, Chave, TpDown, tpAmb, Caminho, false);

                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();

                string EmailFrom = string.Empty;
                string EmailCliente = string.Empty;
                string CodTransportadora = string.Empty;
                string EmailTransportadora = string.Empty;
                MailMessage mail = new MailMessage();

                DataTable dtParametro = conn.ExecQuery("SELECT * FROM VPARAMETROS WHERE CODEMPRESA = ? AND IDPARAMETRO = ?", new object[] { AppLib.Context.Empresa, 1 });

                if (dtParametro.Rows.Count < 0)
                {
                    throw new Exception("Não foi possível carregar os parâmetros do módulo.");
                }
                else
                {
                    //Busca o Tipo de Ambiente para a validação do e-mail

                    int CodFilial = Convert.ToInt32(conn.ExecGetField(0, "SELECT CODFILIAL FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper }));

                    int TpAmb = Convert.ToInt32(conn.ExecGetField(0, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, CodFilial })); 

                    if (TpAmb == 1)
                    {
                        //Busca o e-mail do cliente 
                        EmailCliente = conn.ExecGetField(string.Empty, @"SELECT EMAILNFE
                                                                        FROM VCLIFOR
                                                                        INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA
                                                                        AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR
                                                                        WHERE GOPER.CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper }).ToString();
                        mail.To.Add(EmailCliente);
                    }
                    else
                    {
                        EmailCliente = conn.ExecGetField(string.Empty, "SELECT EMAIL FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial }).ToString();
                        mail.To.Add(EmailCliente);
                    }

                    //Busca o Tipo de Ambiente para a validação do e-mail
                    if (TpAmb == 1)
                    {
                        //Verifica se existe transportadora para a operação
                        CodTransportadora = conn.ExecGetField(string.Empty, "SELECT CODTRANSPORTADORA FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper }).ToString();

                        if (!string.IsNullOrEmpty(CodTransportadora))
                        {
                            EmailTransportadora = conn.ExecGetField(string.Empty, "SELECT EMAIL FROM VTRANSPORTADORA WHERE CODEMPRESA = ? AND CODTRANSPORTADORA = ?", new object[] { AppLib.Context.Empresa, CodTransportadora }).ToString();

                            if (!string.IsNullOrEmpty(EmailTransportadora))
                            {
                                mail.To.Add(EmailTransportadora);
                            }
                        }
                    }

                    //Verifica o E-mail do remetente
                    if (Convert.ToInt32(dtParametro.Rows[0]["EMAILREMETENTE"]) == 0)
                    {
                        EmailFrom = conn.ExecGetField(string.Empty, "SELECT EMAIL FROM GEMPRESA WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();
                    }
                    else if (Convert.ToInt32(dtParametro.Rows[0]["EMAILREMETENTE"]) == 1)
                    {
                        EmailFrom = conn.ExecGetField(string.Empty, "SELECT EMAIL FROM GUSUARIO WHERE CODUSUARIO = ?", new object[] { AppLib.Context.Usuario }).ToString();
                    }

                    if (string.IsNullOrEmpty(EmailFrom))
                    {
                        throw new Exception("E-mail do remetente não informado.");
                    }

                    //Verifica o XML da NF-e
                    string XML = conn.ExecGetField(string.Empty, "SELECT XMLNFE FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper }).ToString();

                    if (string.IsNullOrEmpty(XML))
                    {
                        throw new Exception("O XML da Nota Fiscal não foi encontrado.");
                    }

                    XML = PreparaXML(XML);

                    string TextoEmail = conn.ExecGetField(string.Empty, "SELECT TEXTOEMAILNFE FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, CodFilial}).ToString();

                    string Body = TextoEmail;
                    string sSubject = "NF-e" + Numero;

                    MailAddress from = new MailAddress(EmailFrom, AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOMEFANTASIA FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial }).ToString());
                    mail.From = from;

                    mail.Subject = sSubject;
                    mail.Body = Body;

                    //Anexa os arquivos no e-mail
                    mail.Attachments.Add(new Attachment(Caminho + "\\" + Chave + "-procNfe.pdf", System.Net.Mime.MediaTypeNames.Application.Pdf));
                    mail.Attachments.Add(new Attachment(Caminho + "\\" + Chave + "-procNfe.xml"));

                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Timeout = 3600000;
                    smtp.Host = dtParametro.Rows[0]["EMAILHOST"].ToString();
                    smtp.EnableSsl = Convert.ToInt32(dtParametro.Rows[0]["EMAILUSASSL"]) == 1 ? true : false;
                    NetworkCredential NetworkCred = new NetworkCredential(dtParametro.Rows[0]["EMAILUSUARIO"].ToString(), dtParametro.Rows[0]["EMAILSENHA"].ToString());
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = Convert.ToInt32(dtParametro.Rows[0]["EMAILPORTA"].ToString());

                    try
                    {
                        smtp.Send(mail);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    //smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
