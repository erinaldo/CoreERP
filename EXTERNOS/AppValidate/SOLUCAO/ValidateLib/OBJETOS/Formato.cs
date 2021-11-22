using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    public class Formato
    {
        public bool ValidarDocumentoXML(string CodEstrutura, string Versao, string Xml)
        {
            string caminhoXSD = ValidateLib.Contexto.Session.DiretorioSchemas + CodEstrutura +"\\"+ Versao +"\\proc"+ CodEstrutura.Replace("-", "") +"_v"+ Versao +".xsd";

            try
            {
                System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
                xmlDocument.PreserveWhitespace = false;
                xmlDocument.LoadXml(Xml);
                System.Xml.XmlNodeReader xmlNode = new System.Xml.XmlNodeReader(xmlDocument);
                System.Xml.XmlReaderSettings settings = new System.Xml.XmlReaderSettings();
                settings.ValidationType = System.Xml.ValidationType.Schema;
                settings.Schemas.Add(null, caminhoXSD);
                System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(xmlNode, settings);
                while (xmlReader.Read()) { }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
