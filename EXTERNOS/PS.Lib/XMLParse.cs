using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class XMLParse
    {
        public String Write(Object Objeto)
        {
            System.IO.StringWriter Escritor = new System.IO.StringWriter();
            System.Xml.Serialization.XmlSerializer Serializador = new System.Xml.Serialization.XmlSerializer(Objeto.GetType());
            Serializador.Serialize(Escritor, Objeto);
            return Escritor.ToString();
        }

        public Object Read(String XML, Object ToObject)
        {
            System.IO.StringReader Leitor = new System.IO.StringReader(XML);
            System.Xml.Serialization.XmlSerializer Serializador = new System.Xml.Serialization.XmlSerializer(ToObject.GetType());
            return Serializador.Deserialize(Leitor);
        }

        public Object Read(String XML, Object[] ToObjectArry)
        {
            System.IO.StringReader Leitor = new System.IO.StringReader(XML);
            System.Xml.Serialization.XmlSerializer Serializador = new System.Xml.Serialization.XmlSerializer(ToObjectArry.GetType());
            return Serializador.Deserialize(Leitor);
        }
    }
}
