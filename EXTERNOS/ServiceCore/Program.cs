using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ServiceCore
{
    class Program
    {

        private static String urlDownloadLoteDocs;

         

        class DDFeLote
        {
            public string XML { get; set; }
            public string CHAVE { get; set; }
            public int NSU { get; set; }
            public string MODELO { get; set; }
            public string PDF { get; set; }

            public DDFeLote(string _xml, string _chave, int _nsu, string _modelo, string _pdf = "")
            {
                XML = _xml;
                CHAVE = _chave;
                NSU = _nsu;
                MODELO = _modelo;
                PDF = _pdf;
            }
        }

        private static string setDescricaoEvento(string _tpEvento)
        {
            string descricao;

            switch (_tpEvento)
            {
                case "110110":
                    return descricao = "Carta de Correção";
                case "110111":
                    return descricao = "Cancelamento";
                case "210200":
                    return descricao = "Confirmação da Operação";
                case "210210":
                    return descricao = "Ciência da Operação";
                case "210220":
                    return descricao = "Desconhecimento da Operação";
                case "210240":
                    return descricao = "Operação não Realizada";
                case "310610":
                    return descricao = "MDF-e Autorizado para CT-e";
                case "310611":
                    return descricao = "MDF-e Cancelado Vinculado a CT-e";
                case "310620":
                    return descricao = "Registro de Passagem";
                case "510620":
                    return descricao = "Registro de Passagem BRID";
                case "610500":
                    return descricao = "Registro Passagem NF-e";
                case "610510":
                    return descricao = "Registro de Passagem de NFe propagado pelo MDFe";
                case "610514":
                    return descricao = "Registro de Passagem de NFe propagado pelo MDFe/CTe";
                case "610501":
                    return descricao = "Registro de Passagem para NF-e Cancelado";
                case "610550":
                    return descricao = "Registro de Passagem NFe RFID";
                case "610552":
                    return descricao = "Registro de Passagem Automatico MDFe";
                case "610554":
                    return descricao = "Registro de Passagem Automatico MDF-e com CT-e";
                case "610600":
                    return descricao = "CT-e Autorizado para NF-e";
                case "610601":
                    return descricao = "Ct-e Cancelado";
                case "610610":
                    return descricao = "MDF-e Autorizado para NF-e";
                case "610611":
                    return descricao = "MDF-e Cancelado";
                case "610614":
                    return descricao = "MDF-e Autorizado com CT-e";
                case "610615":
                    return descricao = "Cancelamento de MDF-e Autorizado com CT-e";
                case "790700":
                    return descricao = "Averbação para Exportação";
                default:
                    return descricao = string.Empty;
            }
        }

        public static String EnviaConteudoParaAPI(String token, String conteudo, String url)
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


        public static String DownloadLote(String token, String CNPJ, int UltNSU, int tpAmb, int modelo, bool apenasPendManif, bool apenasComXML, bool comEventos, bool incluirPDF)
        {
            urlDownloadLoteDocs = "https://ddfe.ns.eti.br/dfe/bunch";

            //Monta JSON e envia para API
            String JSON = "{" +
                    "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                    "\"CNPJInteressado\": \"" + CNPJ + "\", " +
                    "\"ultNSU\": " + UltNSU + ", " +
                     "\"modelo\": \"" + modelo + "\", " +
                    "\"tpAmb\": \"" + tpAmb + "\", " +
                     "\"apenasPendManif\": " + apenasPendManif + ", " +
                      "\"apenasComXml\": " + apenasComXML + ", " +
                     "\"comEventos\": " + comEventos + ", " +
                    "\"incluirPDF\": " + incluirPDF + "" + "}";

            JSON = JSON.Replace("False", "false").Replace("True", "true");

            return EnviaConteudoParaAPI(token, JSON, urlDownloadLoteDocs);
        }

        public class DadosDDFe
        {
            public int nsu = 0;
            public string xml = String.Empty;
            public string chave = String.Empty;
            public string modelo = String.Empty;
            public string Estrutura = String.Empty;
            public string DescricaoEvento = String.Empty;
            public string PDF = String.Empty;
        }

        public class DDFeEvento
        {
            public string XML { get; set; }
            public string CHAVE { get; set; }
            public string TPEVENTO { get; set; }

            public DDFeEvento(string _xml, string _chave)
            {
                XML = _xml;
                CHAVE = _chave;
            }
        }

        static List<DDFeEvento> listEvento = new List<DDFeEvento>();

        private static List<DadosDDFe> FormataListasDDFe(List<DadosDDFe> _listDDFe)
        {
            for (int i = 0; i < _listDDFe.Count; i++)
            {
                if (_listDDFe[i].xml.Contains("resEvento") || _listDDFe[i].xml.Contains("procEventoNFe"))
                {
                    listEvento.Add(new DDFeEvento(_listDDFe[i].xml, _listDDFe[i].chave));
                    _listDDFe.Remove(_listDDFe[i]);
                }
            }

            for (int i = 0; i < _listDDFe.Count; i++)
            {
                if (_listDDFe[i].xml.Contains("resEvento") || _listDDFe[i].xml.Contains("procEventoNFe"))
                {
                    listEvento.Add(new DDFeEvento(_listDDFe[i].xml, _listDDFe[i].chave));
                    _listDDFe.Remove(_listDDFe[i]);
                }
            }

            for (int i = 0; i < _listDDFe.Count; i++)
            {
                if (_listDDFe[i].xml.Contains("resEvento") || _listDDFe[i].xml.Contains("procEventoNFe"))
                {
                    listEvento.Add(new DDFeEvento(_listDDFe[i].xml, _listDDFe[i].chave));
                    _listDDFe.Remove(_listDDFe[i]);
                }
            }

            return _listDDFe;
        }

        public static System.Data.DataSet StringToDataSet(string XML)
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

        public static void SalvaArquivo(String conteudo, String pathName, String chave)
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
                throw new Exception(ex.Message, ex);
            }
        }

        public static void SalvaLog(String msg)
        {
            msg = DateTime.Now + " - " + msg;
            File.AppendAllText(@"C:\temp\log.txt", string.Format("{0}{1}", msg, Environment.NewLine));
        }

        static int NF = 0;
        static int EVNT = 0;
        static int TOTALNF = 0;
        static int TOTALEVNT = 0;
        static int TOTAL = 0;

        public static Boolean BaixaLote(DataTable dtFilial, String retornoLote, String CNPJ, String CODEMPRESA, dynamic JsonRetornoLote)
        {
            string sql = String.Empty;
            try
            {
                string Destinatario = dtFilial.Rows[0]["NOME"].ToString();
                string UfDestinatario = dtFilial.Rows[0]["CODETD"].ToString();

                if (JsonRetornoLote.status == "200")
                {
                    Newtonsoft.Json.Linq.JArray Root = new Newtonsoft.Json.Linq.JArray();

                    Root.Add(JsonRetornoLote.xmls);



                    List<DadosDDFe> listLote = new List<DadosDDFe>();

                    foreach (var Valores in Root.Children())
                    {
                        foreach (JObject obj in Valores.Children<JObject>())
                        {
                            DadosDDFe d = new DadosDDFe();
                            d.nsu = (int)obj.GetValue("nsu");
                            d.xml = obj.GetValue("xml").ToString();
                            d.chave = obj.GetValue("chave").ToString();
                            d.modelo = obj.GetValue("modelo").ToString();

                            if (obj.Properties().Count() > 6)
                            {
                                d.PDF = obj.GetValue("pdf").ToString();
                            }

                            listLote.Add(d);
                        }
                    }

                    listLote = FormataListasDDFe(listLote);
                    DataSet ds = new DataSet();
                    string Estrutura = String.Empty;

                    

                    for (int i = 0; i < listLote.Count; i++)
                    {
                        sql = String.Format("select COUNT(1) as 'CONT' from GDDFE where CNPJDESTINATARIO = '{0}' and NSU = '{1}'", CNPJ, listLote[i].nsu);
                        int count = int.Parse(MetodosSql.GetField(sql, "CONT"));

                        if (count == 0)
                        {
                            ds = StringToDataSet(listLote[i].xml);

                            if (listLote[i].xml.Contains("nfeProc"))
                            {
                                Estrutura = "NF-e";
                            }
                            else if (listLote[i].xml.Contains("cteProc"))
                            {
                                Estrutura = "CT-e";
                            }
                            else
                            {
                                continue;
                            }

                            sql = String.Format(@"INSERT INTO GDDFE(CODEMPRESA, NSU, DATA, XMLRECEBIDO, CODESTRUTURA, VERSAO, MODELO, TPAMB, NUMERODOCUMENTO, CHAVE, DATAEMISSAO, EMITENTE, CNPJEMITENTE, UFEMITENTE, DESTINATARIO, CNPJDESTINATARIO, UFDESTINATARIO)
                                                  VALUES ('{0}', '{1}', CONVERT(DATETIME, '{2}', 103), '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', CONVERT(DATETIME, '{10}', 103), '{11}', '{12}', '{13}', '{14}', '{15}', '{16}')",
                                                  CODEMPRESA,                                                   //0 
                                                  listLote[i].nsu,                                              //1
                                                  DateTime.Now,                                                 //2
                                                  listLote[i].xml.Replace("'","''"),                            //3
                                                  Estrutura,                                                    //4
                                                  ds.Tables["nfeProc"].Rows[0]["versao"].ToString(),            //5
                                                  listLote[i].modelo,                                           //6
                                                  1,                                                            //7
                                                  ds.Tables["ide"].Rows[0]["nNF"].ToString(),                   //8
                                                  listLote[i].chave,                                            //9
                                                  Convert.ToDateTime(ds.Tables["ide"].Rows[0]["dhEmi"]),        //10
                                                  ds.Tables["emit"].Rows[0]["xNome"].ToString(),                //11
                                                  ds.Tables["emit"].Rows[0]["CNPJ"].ToString(),                 //12
                                                  ds.Tables["EnderEmit"].Rows[0]["UF"].ToString(),              //13 
                                                  Destinatario,                                                 //14
                                                  CNPJ,                                                         //15
                                                  UfDestinatario);                                              //16

                            MetodosSql.ExecQuery(sql);
                            NF++;

                            //AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GDDFE (CODEMPRESA, NSU, DATA, XMLRECEBIDO, CODESTRUTURA, VERSAO, MODELO, TPAMB, NUMERODOCUMENTO, CHAVE, DATAEMISSAO, EMITENTE, CNPJEMITENTE, UFEMITENTE, DESTINATARIO, CNPJDESTINATARIO, UFDESTINATARIO)
                            //                                                             VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, listLote[i].NSU, DateTime.Now, listLote[i].XML, Estrutura, ds.Tables["nfeProc"].Rows[0]["versao"].ToString(), listLote[i].MODELO, 1, ds.Tables["ide"].Rows[0]["nNF"].ToString(), listLote[i].CHAVE, Convert.ToDateTime(ds.Tables["ide"].Rows[0]["dhEmi"]), ds.Tables["emit"].Rows[0]["xNome"].ToString(), ds.Tables["emit"].Rows[0]["CNPJ"].ToString(), ds.Tables["EnderEmit"].Rows[0]["UF"].ToString(), Destinatario, CNPJ, UfDestinatario });

                            if (!string.IsNullOrEmpty(listLote[i].PDF))
                            {
                                SalvaArquivo(listLote[i].PDF, dtFilial.Rows[0]["PASTADESTINO"].ToString(), listLote[i].chave);
                            }
                        }
                    }

                    string DescricaoEvento;

                    if (listEvento.Count > 0)
                    {
                        for (int i = 0; i < listEvento.Count; i++)
                        {
                            ds = StringToDataSet(listEvento[i].XML);

                            if (listEvento[i].XML.Contains("procEventoNFe"))
                            {
                                DescricaoEvento = setDescricaoEvento(ds.Tables["infEvento"].Rows[0]["tpEvento"].ToString());

                                sql = String.Format(@"select count(1) as 'CONT' from GDDFEEVENTO where NPROT = '{0}' and CHAVE = '{1}'", ds.Tables["infEvento"].Rows[1]["nProt"].ToString(), listEvento[i].CHAVE);

                                int count = int.Parse(MetodosSql.GetField(sql, "CONT"));

                                if (count == 0)
                                {
                                    sql = String.Format(@"INSERT INTO GDDFEEVENTO(CODEMPRESA, CHAVE, TPEVENTO, DATAEVENTO, NPROT, XML, DESCRICAOEVENTO)
                                                      VALUES('{0}', '{1}', '{2}', CONVERT(DATETIME, '{3}', 103), '{4}', '{5}', '{6}')",
                                                      CODEMPRESA,                                                                   //0
                                                      listEvento[i].CHAVE,                                                          //1
                                                      ds.Tables["infEvento"].Rows[0]["tpEvento"].ToString(),                        //2
                                                      Convert.ToDateTime(ds.Tables["infEvento"].Rows[0]["dhEvento"].ToString()),    //3
                                                      ds.Tables["infEvento"].Rows[1]["nProt"].ToString(),                           //4
                                                      listEvento[i].XML.Replace("'", "''"),                                         //5    
                                                      DescricaoEvento);                                                             //6

                                    MetodosSql.ExecQuery(sql);
                                    EVNT++;
                                }



                                //AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GDDFEEVENTO(CODEMPRESA, CHAVE, TPEVENTO, DATAEVENTO, NPROT, XML, DESCRICAOEVENTO)
                                //                                                             VALUES(?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, listEvento[i].CHAVE, ds.Tables["infEvento"].Rows[0]["tpEvento"].ToString(), Convert.ToDateTime(ds.Tables["infEvento"].Rows[0]["dhEvento"].ToString()), ds.Tables["infEvento"].Rows[1]["nProt"].ToString(), listEvento[i].XML, DescricaoEvento });
                            }
                            else
                            {
                                DescricaoEvento = setDescricaoEvento(ds.Tables["resEvento"].Rows[0]["tpEvento"].ToString());

                                sql = String.Format(@"select count(1) as 'CONT' from GDDFEEVENTO where NPROT = '{0}' and CHAVE = '{1}'", ds.Tables["resEvento"].Rows[0]["nProt"].ToString(), listEvento[i].CHAVE);

                                int count = int.Parse(MetodosSql.GetField(sql, "CONT"));

                                if (count == 0)
                                {
                                    sql = String.Format(@"INSERT INTO GDDFEEVENTO(CODEMPRESA, CHAVE, TPEVENTO, DATAEVENTO, NPROT, XML, DESCRICAOEVENTO)
                                                      VALUES('{0}', '{1}', '{2}', CONVERT(DATETIME, '{3}', 103), '{4}', '{5}', '{6}')",
                                                      CODEMPRESA,                                                                   //0
                                                      listEvento[i].CHAVE,                                                          //1
                                                      ds.Tables["resEvento"].Rows[0]["tpEvento"].ToString(),                        //2
                                                      Convert.ToDateTime(ds.Tables["resEvento"].Rows[0]["dhEvento"].ToString()),    //3
                                                      ds.Tables["resEvento"].Rows[0]["nProt"].ToString(),                           //4
                                                      listEvento[i].XML.Replace("'", "''"),                                         //5
                                                      DescricaoEvento);                                                             //6

                                    MetodosSql.ExecQuery(sql);
                                    EVNT++;
                                }
                                //AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GDDFEEVENTO(CODEMPRESA, CHAVE, TPEVENTO, DATAEVENTO, NPROT, XML, DESCRICAOEVENTO)
                                //                                                             VALUES(?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, listEvento[i].CHAVE, ds.Tables["resEvento"].Rows[0]["tpEvento"].ToString(), Convert.ToDateTime(ds.Tables["resEvento"].Rows[0]["dhEvento"].ToString()), ds.Tables["resEvento"].Rows[0]["nProt"].ToString(), listEvento[i].XML, DescricaoEvento });
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                SalvaLog(ex.Message);
                return false;
            }
        }

        public static void ExecutaProcesso()
        {

        }

        static void Main(string[] args)
        {
            try
            {
                Console.Title = "DDFe";
                Boolean stat = true;
                while(stat)
                {
                    DataTable dtFilial = MetodosSql.GetDT(String.Format("SELECT * FROM GFILIAL"));

                    foreach (DataRow row in dtFilial.Rows)
                    {
                        string CNPJ = row["CGCCPF"].ToString().ToString().Replace(".", "").Replace("-", "").Replace("/", "");

                        int NSU = int.Parse(MetodosSql.GetField(String.Format(@"select top 1 (NSU + 1) as 'NSU' from GDDFE where CNPJDESTINATARIO = '{0}' order by NSU Desc", CNPJ), "NSU"));

                        string ret = ret = row["CGCCPF"].ToString() + " - De " + NSU + " até " + (NSU + 50) + " - ";
                        Console.Write(ret);


                        string Token = MetodosSql.GetField(String.Format("select TOKEN from GFILIAL where CGCCPF = '{0}'", row["CGCCPF"].ToString()), "TOKEN");
                        
                        string retornoLote = DownloadLote(Token, CNPJ, 100000, 1, 55, false, true, true, true);

                        string CODEMPRESA = row["CODEMPRESA"].ToString();

                        dynamic JsonRetornoLote = JsonConvert.DeserializeObject(retornoLote);

                        if (JsonRetornoLote.status == "200")
                        {
                            NF = 0;
                            EVNT = 0;

                            if (BaixaLote(dtFilial, retornoLote, CNPJ, CODEMPRESA, JsonRetornoLote))
                            {
                                Console.WriteLine(String.Format("DDFe: {0}, Eventos: {1} - Concluido", NF, EVNT));
                                SalvaLog(ret + String.Format("DDFe: {0}, Eventos: {1} - Concluido", NF, EVNT));
                            }
                            else
                            {
                                Console.WriteLine(String.Format("DDFe: {0}, Eventos: {1} - Falha", NF, EVNT));
                                SalvaLog(ret + String.Format("DDFe: {0}, Eventos: {1} - Falha", NF, EVNT));
                            }

                            TOTALNF += NF;
                            TOTALEVNT += EVNT;
                            TOTAL = TOTALNF + TOTALEVNT;
                        }
                        else
                        {
                            Console.WriteLine(String.Format("---------------->    DDFe:{0} Eventos:{1} Total:{2}     <----------------", TOTALNF, TOTALEVNT, TOTAL));
                            SalvaLog(String.Format("---------------->    DDFe:{0} Eventos:{1} Total:{2}     <----------------", TOTALNF, TOTALEVNT, TOTAL));
                            Console.WriteLine("################################       " + row["CGCCPF"].ToString() + "       ################################");
                            SalvaLog("################################       " + row["CGCCPF"].ToString() + "       ################################");
                            stat = false;
                        }
                    }
                }

                SalvaLog("################################       Processo finalizado       ################################");

                Thread.Sleep(10000);
            }
            catch (Exception ex)
            {
                SalvaLog(ex.Message);
            }
        }


    }
}
