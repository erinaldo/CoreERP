using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace ERP
{
    public class SalvarXML
    {
        public static void Salvar(object obj, string filename)
        {
            XmlSerializer sr = new XmlSerializer(obj.GetType());
            TextWriter txt = new StreamWriter(filename);
            sr.Serialize(txt, obj);
            txt.Close();
        }
    }
}

