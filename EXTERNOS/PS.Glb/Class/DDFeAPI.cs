using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PS.Glb.Class
{
    public class DDFeAPI
    {
        #region Variáveis

        private String urlDownloadUnico;
        private String urlDownloadLoteDocs;
        private String urlManifestacaoDDFe;
        private String urlDesacordoOperacao;

        #endregion

        #region Construtor

        public DDFeAPI()
        {
            this.urlDownloadUnico = "https://ddfe.ns.eti.br/dfe/unique";
            this.urlDownloadLoteDocs = "https://ddfe.ns.eti.br/dfe/bunch";
            this.urlManifestacaoDDFe = "https://ddfe.ns.eti.br/events/manif";
            this.urlDesacordoOperacao = "https://ddfe.ns.eti.br/events/cte/disagree";
        }

        #endregion

        #region Métodos referentes à Integração

        /// <summary>
        /// Método responsável pelo envio de conteúdo para a API.
        /// </summary>
        /// <param name="token">Token da empresa, gerado pela NS</param>
        /// <param name="conteudo">Nota do cliente</param>
        /// <param name="url">URL do serviço a ser realizado pela API</param>
        /// <returns></returns>
        public String EnviaConteudoParaAPI(String token, String conteudo, String url)
        {
            String retorno = "";

            //Cria objeto para requisição e atribui as configurações necessárias para API
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers["X-AUTH-TOKEN"] = token;
            httpWebRequest.ContentType = "application/json";

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
        ///  Método responsável por fazer o download automático do Xml.
        /// </summary>
        /// <param name="token">Método responsável por fazer o download em lote dos Xmls's.</param>
        /// <param name="CNPJ">CNPJ</param>
        /// <param name="apenasComXML">Apenas documentos com Xmls's disponíveis</param>
        /// <param name="comEventos">Incluir eventos vinculados ao documento</param>
        /// <param name="incluirPDF">Incluir PDF</param>
        /// <returns></returns>
        public String DownloadAutomatico(String token, String CNPJ, int NSU, bool apenasComXML = false, bool comEventos = false, bool incluirPDF = false)
        {
            //Monta JSON e envia para API
            String JSON = "{" +
                    "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                    "\"CNPJInteressado\": \"" + CNPJ + "\", " +
                     "\"nsu\": \"" + NSU + "\", " +
                     "\"apenasComXml\": \"" + apenasComXML + "\", " +
                     "\"comEventos\": \"" + comEventos + "\", " +
                    "\"incluirPDF\": \"" + incluirPDF + "\"" + "}";

            return EnviaConteudoParaAPI(token, JSON, this.urlDownloadUnico);
        }

        /// <summary>
        /// Método responsável por fazer o download do Xml.
        /// </summary>
        /// <param name="token">Método responsável por fazer o download em lote dos Xmls's.</param>
        /// <param name="CNPJ">CNPJ</param>
        /// <param name="NSU">Número Sequencial Único</param>
        /// <param name="modelo">Modelo</param>
        /// <param name="chave">Chave de acesso</param>
        /// <param name="tpAmb">Tipo de Ambiente</param>
        /// <param name="apenasComXML">Apenas documentos com Xmls's disponíveis</param>
        /// <param name="comEventos">Incluir eventos vinculados ao documento</param>
        /// <param name="incluirPDF">Incluir PDF</param>
        /// <returns></returns>
        public String DownloadUnico(String token, String CNPJ, String chave, int? NSU, int tpAmb, int modelo, bool apenasComXML, bool comEventos, bool incluirPDF)
        {
            //Monta JSON e envia para API
            String JSON = "{" +
                    "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                    "\"CNPJInteressado\": \"" + CNPJ + "\", " +
                    "\"nsu\": \"" + NSU + "\", " +
                     "\"modelo\": \"" + modelo + "\", " +
                    "\"chave\": \"" + chave + "\", " +
                    "\"tpAmb\": \"" + tpAmb + "\", " +
                     "\"apenasComXml\": " + apenasComXML + ", " +
                     "\"comEventos\": " + comEventos + ", " +
                    "\"incluirPDF\": " + incluirPDF + "" + "}";

            JSON = JSON.Replace("False", "false").Replace("True", "true");

            return EnviaConteudoParaAPI(token, JSON, this.urlDownloadUnico);
        }

        /// <summary>
        /// Método responsável por fazer o download em lote dos Xmls's.
        /// </summary>
        /// <param name="token">Token da empresa, gerado pela NS</param>
        /// <param name="CNPJ">CNPJ</param>
        /// <param name="UltNSU">Último NSU</param>
        /// <param name="modelo">Modelo</param>
        /// <param name="tpAmb">Tipo de Ambiente</param>
        /// <param name="apenasPendManif">Apenas documentos pendentes</param>
        /// <param name="apenasComXML">Apenas documentos com Xmls's disponíveis</param>
        /// <param name="comEventos">Incluir eventos vinculados ao documento</param>
        /// <param name="incluirPDF">Incluir PDF</param>
        /// <returns></returns>
        public String DownloadLote(String token, String CNPJ, int UltNSU, int modelo, bool apenasComXML, bool comEventos, int tpAmb, bool incluirPDF)
        {
            //Monta JSON e envia para API
            String JSON = "{" +
                    "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                    "\"CNPJInteressado\": \"" + CNPJ + "\", " +
                    "\"ultNSU\": " + UltNSU + ", " +
                     "\"modelo\": \"" + modelo + "\", " +
                      "\"apenasComXml\": " + apenasComXML + ", " +
                     "\"comEventos\": " + comEventos + ", " +
                    "\"tpAmb\": \"" + tpAmb + "\", " +
                    "\"incluirPDF\": " + incluirPDF + "" + "}";

            JSON = JSON.Replace("False", "false").Replace("True", "true");

            return EnviaConteudoParaAPI(token, JSON, this.urlDownloadLoteDocs);
        }


        /// <summary>
        /// Método responsável pela manifestação da DDF-e.
        /// </summary>
        /// <param name="token">Token da empresa, gerado pela NS</param>
        /// <param name="CNPJ">CNPJ</param>
        /// <param name="NSU"></param>
        /// <param name="chave">Chave de acesso</param>
        /// <param name="tpEvento">Tipo do evento</param>
        /// <param name="xJust"></param>
        /// <returns></returns>
        public String ManifestacaoDDFe(String token, String CNPJ, String chave, int tpEvento, String xJust)
        {
            //Monta JSON e envia para API
            String JSON = "{" +
                   "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                   "\"CNPJInteressado\": \"" + CNPJ + "\", " +
                   "\"chave\": \"" + chave + "\", " +
                   "\"manifestacao\": {" +
                   "\"tpEvento\": " + tpEvento + "" + "}" +
                          "}";

            return EnviaConteudoParaAPI(token, JSON, this.urlManifestacaoDDFe);
        }

        /// <summary>
        /// Método para informar ao fisco que o documento CT-e que o relaciona está em desacordo com a prestação do serviço.
        /// </summary>
        /// <param name="token">Token da empresa, gerado pela NS</param>
        /// <param name="CNPJ">CNPJ</param>
        /// <param name="tpAmb">Tipo de Ambiente</param>
        /// <param name="dhEvento">Data e hora do Evento</param>
        /// <param name="xObs">Observações do tomador</param>
        /// <param name="indDesacordoOper">Indicador de prestação do serviço em desacordo</param>
        /// <returns></returns>
        private String DesacordoOperacaoCTe(String token, String CNPJ, String chCTe, int tpAmb, DateTime dhEvento, String xObs, int indDesacordoOper)
        {
            //Monta JSON e envia para API
            String JSON = "{" +
                   "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                   "\"CNPJInteressado\": \"" + CNPJ + "\", " +
                   "infEvento: {" +
                   "\"chCTe\": \"" + chCTe + "\", " +
                   "\"tpAmb\": \"" + tpAmb + "\", " +
                    "\"dhEvento\": \"" + dhEvento + "\", " +
                    "\"xObs\": \"" + xObs + "\", " +
                   "\"indDesacordoOper\": \"" + indDesacordoOper + "\"" + "}" +
                          "}";

            return EnviaConteudoParaAPI(token, JSON, this.urlDesacordoOperacao);
        }

        /// <summary>
        /// Método responsável por salvar o documento.
        /// </summary>
        /// <param name="conteudo">Nota do cliente</param>
        /// <param name="pathName">Local onde o documento será salvo</param>
        /// <param name="tpArq">Tipo do arquivo que será salvo</param>
        /// <param name="chave"></param>
        public void SalvaArquivo(String conteudo, String pathName, String chave)
        {
            try
            {
                if (pathName != "")
                {
                    if (!Directory.Exists(pathName)) Directory.CreateDirectory(pathName);
                    if (!pathName.EndsWith("\\")) pathName += "\\";
                }

                string caminho = pathName + chave + "-procDDfe";

                System.IO.File.WriteAllText(@caminho + ".pdf", conteudo);

                byte[] bytes = Convert.FromBase64String(conteudo);
                if (File.Exists(caminho + ".pdf")) File.Delete(caminho + ".pdf");
                System.IO.FileStream stream = new FileStream(@caminho + ".pdf", FileMode.CreateNew);
                System.IO.BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="XML"></param>
        /// <returns></returns>
        public System.Data.DataSet StringToDataSet(string XML)
        {
            try
            {
                System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                xdoc.LoadXml(XML);
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(xdoc.DocumentElement.OuterXml)));

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    #endregion
}

