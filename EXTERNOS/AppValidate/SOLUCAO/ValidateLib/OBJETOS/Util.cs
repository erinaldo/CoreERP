using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    public class Util
    {
        public static string MontaXMLNFeProc(string XML, string XMLProt, string versao)
        {
            string result = "";

            XML = XML.Substring(XML.IndexOf("<NFe"), XML.IndexOf("</NFe>") - XML.IndexOf("<NFe") + "</NFe>".Length);
            XMLProt = XMLProt.Substring(XMLProt.IndexOf("<protNFe"), XMLProt.IndexOf("</protNFe>") - XMLProt.IndexOf("<protNFe") + "</protNFe>".Length);
            result = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + "<nfeProc xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + versao  + "\">" + XML + XMLProt + "</nfeProc>";
            return result;
        }

        public static string RetornoXMLNFe(string XML)
        {
            string temp = XML;
            string result = "";

            temp = temp.Substring(temp.IndexOf("<nfeProc"), temp.IndexOf("</nfeProc>") - temp.IndexOf("<nfeProc") + "</nfeProc>".Length);
            result = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + temp;
            return result;
        }

        public static string AdicionarAtributoTag(string XML, string Tag)
        {
            return XML.Replace(string.Concat("<", Tag), string.Concat("<", Tag, " xmlns=\"http://www.portalfiscal.inf.br/nfe\""));
        }

        public static string RemoveAtributoInvalido(string XML)
        {
            return XML.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
        }

        public static string UTF16toUTF8(string XML)
        {
            return XML.Replace("encoding=\"utf-16\"", "encoding=\"UTF-8\"");
        }

        public static string RemoveCaracteresInvalidos(string XML)
        {
            string retorno = XML;
            string[] naoPode = { "\n", "\t", "\r", "> <", ">  <" };
            for (int i = 0; i < naoPode.Length; i++)
            {
                while (retorno.Contains(naoPode[i]))
                {
                    if (naoPode[i] == "> <")
                        retorno = retorno.Replace(naoPode[i], "><");
                    else if (naoPode[i] == ">  <")
                        retorno = retorno.Replace(naoPode[i], "><");
                    else
                        retorno = retorno.Replace(naoPode[i], "");
                }
            }
            return retorno;
        }

        public static string DataSetToString(System.Data.DataSet Table)
        {
            using (var memoryStream = new System.IO.MemoryStream())
            {
                using (System.IO.TextWriter streamWriter = new System.IO.StreamWriter(memoryStream))
                {
                    var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(System.Data.DataSet));
                    xmlSerializer.Serialize(streamWriter, Table);
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }

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

        public DateTime StringToDateTime(string DATAHORA)
        {
            int ano = int.Parse(DATAHORA.Substring(0, 4));
            int mes = int.Parse(DATAHORA.Substring(5, 2));

            int dia = 0;
            if (mes >= 1 && mes <= 12)
            {
                dia = int.Parse(DATAHORA.Substring(8, 2));
            }
            else
            {
                mes = int.Parse(DATAHORA.Substring(8, 2));
                dia = int.Parse(DATAHORA.Substring(5, 2));
            }

            int horas = int.Parse(DATAHORA.Substring(11, 2));
            int minutos = int.Parse(DATAHORA.Substring(14, 2));
            int segundos = int.Parse(DATAHORA.Substring(17, 2));

            DateTime result = new DateTime(ano, mes, dia, horas, minutos, segundos);
            return result;
        }

        public static string FormataCPFCNPJ(string documento)
        {
            if (documento.Length == 14)
            {
                documento = string.Concat(documento.Substring(0, 2), ".", documento.Substring(2, 3), ".", documento.Substring(5, 3), "/", documento.Substring(8, 4), "-", documento.Substring(12, 2));
            }

            if (documento.Length == 11)
            {
                documento = string.Concat(documento.Substring(0, 3), ".", documento.Substring(3, 3), ".", documento.Substring(6, 3), "-", documento.Substring(9, 2));
            }

            return documento;
        }

        public static void ValidaSchema(string CodEstrutura, string Versao, string SchemaPath, string SchemaName, string XML)
        {
            string caminhoXSD = string.Concat(SchemaPath, CodEstrutura, "\\", Versao, "\\", SchemaName, ".xsd");

            try
            {
                System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
                xmlDocument.PreserveWhitespace = false;
                xmlDocument.LoadXml(XML);
                System.Xml.XmlNodeReader xmlNode = new System.Xml.XmlNodeReader(xmlDocument);
                System.Xml.XmlReaderSettings settings = new System.Xml.XmlReaderSettings();
                settings.ValidationType = System.Xml.ValidationType.Schema;
                settings.Schemas.Add(null, caminhoXSD);
                System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(xmlNode, settings);
                while (xmlReader.Read()) { }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
