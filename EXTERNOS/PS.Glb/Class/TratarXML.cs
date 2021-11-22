using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Xml;

namespace PS.Glb.Class
{
    //public class TratarXML
    //{
    //    public string recuperaTagRECIBO(string texto)
    //    {
    //        try
    //        {
    //            int ini = 0;
    //            int fim = 0;

    //            ini = texto.IndexOf("<retEnviNFe");
    //            fim = texto.LastIndexOf("</retEnviNFe>");
    //            texto = texto.Substring(ini, (fim - ini) + 13);
    //        }
    //        catch (Exception ex)
    //        { }

    //        return texto;
    //    }

    //    public string recuperaTagNUM_RECIBO(string texto)
    //    {
    //        try
    //        {
    //            string valor = "";
    //            XmlDocument doc = new XmlDocument();
    //            doc.LoadXml(texto);
    //            var infEvento = doc.GetElementsByTagName("retConsReciNFe");

    //            foreach (XmlElement nodo in infEvento)
    //            {
    //                valor = nodo.GetElementsByTagName("nRec")[0].InnerText.Trim();
    //            }
    //            return valor;
    //        }
    //        catch (Exception ex)
    //        { }

    //        return texto;
    //    }

    //    public string recuperaTagPROTOCOLO(string texto)
    //    {
    //        try
    //        {
    //            int ini = 0;
    //            int fim = 0;

    //            ini = texto.IndexOf("<nProt>");
    //            fim = texto.LastIndexOf("</nProt>");
    //            texto = texto.Substring(ini + 7, (fim - ini) - 7);
    //        }
    //        catch (Exception ex)
    //        { }

    //        return texto;
    //    }

    //    public string recuperaTagSTATUS(string texto)
    //    {
    //        string valor = "";

    //        JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //        string json = texto.Replace("\"", "'");

    //        dynamic resultado = serializer.DeserializeObject(json);
    //        foreach (KeyValuePair<string, object> entry in resultado)
    //        {
    //            var key = entry.Key;
    //            var value = entry.Value;

    //            if (key.Equals("status"))
    //            {
    //                valor = String.Format("{0}", value);
    //                break;
    //            }
    //        }

    //        return valor;
    //    }

    //    public string recuperaTagMOTIVO(string texto)
    //    {
    //        string valor = "";

    //        JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //        string json = texto.Replace("\"", "'");

    //        dynamic resultado = serializer.DeserializeObject(json);
    //        foreach (KeyValuePair<string, object> entry in resultado)
    //        {
    //            var key = entry.Key;
    //            var value = entry.Value;

    //            if (key.Equals("motivo"))
    //            {
    //                valor = String.Format("{0}", value);
    //                break;
    //            }
    //        }

    //        return valor;
    //    }

    //    public string recuperaTagNSNREC(string texto)
    //    {
    //        string valor = "";

    //        JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

    //        string json = texto.Replace("\"", "'");

    //        dynamic resultado = serializer.DeserializeObject(json);
    //        foreach (KeyValuePair<string, object> entry in resultado)
    //        {
    //            var key = entry.Key;
    //            var value = entry.Value;

    //            if (key.Equals("nsNRec"))
    //            {
    //                valor = String.Format("{0}", value);
    //                break;
    //            }
    //        }

    //        return valor;
    //    }

    //    public string recuperaTagChNFe(string texto)
    //    {
    //        string valor = "";

    //        JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //        string json = texto.Replace("\"", "'");

    //        dynamic resultado = serializer.DeserializeObject(json);
    //        foreach (KeyValuePair<string, object> entry in resultado)
    //        {
    //            var key = entry.Key;
    //            var value = entry.Value;

    //            if (key.Equals("chNFe"))
    //            {
    //                valor = String.Format("{0}", value);
    //                break;
    //            }
    //        }


    //        return valor;
    //    }

    //    public string recuperaTagxMotivo(string texto)
    //    {
    //        string valor = "";

    //        JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //        string json = texto.Replace("\"", "'");

    //        dynamic resultado = serializer.DeserializeObject(json);
    //        foreach (KeyValuePair<string, object> entry in resultado)
    //        {
    //            var key = entry.Key;
    //            var value = entry.Value;

    //            if (key.Equals("xMotivo"))
    //            {
    //                valor = String.Format("{0}", value);
    //                break;
    //            }
    //        }

    //        return valor;
    //    }

    //    public string recuperaTagXML(string texto)
    //    {
    //        string valor = "";

    //        JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //        string json = texto.Replace("\"", "'");

    //        dynamic resultado = serializer.DeserializeObject(json);
    //        foreach (KeyValuePair<string, object> entry in resultado)
    //        {
    //            var key = entry.Key;
    //            var value = entry.Value;

    //            if (key.Equals("xml"))
    //            {
    //                valor = String.Format("{0}", value);
    //                break;
    //            }
    //        }

    //        return valor;
    //    }

    //    public string recuperaTagdhRecbto(string texto)
    //    {
    //        try
    //        {
    //            int ini = 0;
    //            int fim = 0;

    //            ini = texto.IndexOf("<dhRecbto>");
    //            fim = texto.IndexOf("</dhRecbto>");
    //            texto = texto.Substring(ini + 10, (fim - ini)-10);
    //        }
    //        catch (Exception ex)
    //        { }

    //        return texto;
    //    }
    //}
    public class TratarXML
    {


        #region METODOS UTILIZADOS PARA LER RETORNOS DO XML

        public string recuperaTagRECIBO(string texto)
        {
            try
            {
                int ini = 0;
                int fim = 0;

                ini = texto.IndexOf("<retEnviNFe");
                fim = texto.LastIndexOf("</retEnviNFe>");
                texto = texto.Substring(ini, (fim - ini) + 13);
            }
            catch (Exception ex)
            { }

            return texto;
        }

        public string recuperaTagNUM_RECIBO(string texto)
        {
            try
            {
                string valor = "";
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(texto);
                var infEvento = doc.GetElementsByTagName("retConsReciNFe");

                foreach (XmlElement nodo in infEvento)
                {
                    valor = nodo.GetElementsByTagName("nRec")[0].InnerText.Trim();
                }
                return valor;
            }
            catch (Exception ex)
            { }

            return texto;
        }

        public string recuperaTagPROTOCOLO(string texto)
        {
            try
            {
                int ini = 0;
                int fim = 0;

                ini = texto.IndexOf("<nProt>");
                fim = texto.LastIndexOf("</nProt>");
                texto = texto.Substring(ini + 7, (fim - ini) - 7);
            }
            catch (Exception ex)
            { }

            return texto;
        }

        public string recuperaTagSTATUS(string texto)
        {
            string valor = "";

            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string json = texto.Replace("\"", "'");

            dynamic resultado = serializer.DeserializeObject(json);
            foreach (KeyValuePair<string, object> entry in resultado)
            {
                var key = entry.Key;
                var value = entry.Value;

                if (key.Equals("status"))
                {
                    valor = String.Format("{0}", value);
                    break;
                }
            }

            return valor;
        }

        public string recuperaTagcStat(string texto)
        {
            string valor = "";

            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string json = texto.Replace("\"", "'");

            dynamic resultado = serializer.DeserializeObject(json);
            foreach (KeyValuePair<string, object> entry in resultado)
            {
                var key = entry.Key;
                var value = entry.Value;

                if (key.Equals("cStat"))
                {
                    valor = String.Format("{0}", value);
                    break;
                }
            }

            return valor;
        }

        public string recuperaTagMOTIVO(string texto)
        {
            string valor = "";

            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string json = texto.Replace("\"", "'");

            dynamic resultado = serializer.DeserializeObject(json);
            foreach (KeyValuePair<string, object> entry in resultado)
            {
                var key = entry.Key;
                var value = entry.Value;

                if (key.Equals("motivo"))
                {
                    valor = String.Format("{0}", value);
                    break;
                }
            }

            return valor;
        }

        public string recuperaTagNSNREC(string texto)
        {
            string valor = "";

            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            string json = texto.Replace("\"", "'");

            dynamic resultado = serializer.DeserializeObject(json);
            foreach (KeyValuePair<string, object> entry in resultado)
            {
                var key = entry.Key;
                var value = entry.Value;

                if (key.Equals("nsNRec"))
                {
                    valor = String.Format("{0}", value);
                    break;
                }
            }

            return valor;
        }

        public string recuperaTagChNFe(string texto)
        {
            string valor = "";

            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string json = texto.Replace("\"", "'");

            dynamic resultado = serializer.DeserializeObject(json);
            foreach (KeyValuePair<string, object> entry in resultado)
            {
                var key = entry.Key;
                var value = entry.Value;

                if (key.Equals("chNFe"))
                {
                    valor = String.Format("{0}", value);
                    break;
                }
            }


            return valor;
        }

        public string recuperaTagxMotivo(string texto)
        {
            string valor = "";

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = texto.Replace("\"", "'");

            dynamic resultado = serializer.DeserializeObject(json);

            foreach (KeyValuePair<string, object> entry in resultado)
            {
                var key = entry.Key;
                var value = entry.Value;

                if (key.Equals("xMotivo"))
                {
                    valor = String.Format("{0}", value);
                    break;
                }

            }


            return valor;
        }

        public string recuperaTagXML(string texto)
        {
            string valor = "";

            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string json = texto.Replace("\"", "'");

            dynamic resultado = serializer.DeserializeObject(json);
            foreach (KeyValuePair<string, object> entry in resultado)
            {
                var key = entry.Key;
                var value = entry.Value;

                if (key.Equals("xml"))
                {
                    valor = String.Format("{0}", value);
                    break;
                }
            }

            return valor;
        }

        public string recuperaTagdhRecbto(string texto)
        {
            try
            {
                int ini = 0;
                int fim = 0;

                ini = texto.IndexOf("<dhRecbto>");
                fim = texto.IndexOf("</dhRecbto>");
                texto = texto.Substring(ini + 10, (fim - ini) - 10);
            }
            catch (Exception ex)
            { }

            return texto;
        }

        public string recuperaTagxEvento(string texto)
        {
            try
            {
                int ini = 0;
                string novo;
                int fim = 0;

                ini = texto.IndexOf("\"xEvento\":");
                ini += 11;
                novo = texto.Substring(ini, texto.Length - ini);

                fim = novo.IndexOf("\",");
                texto = novo.Substring(0, fim);
            }
            catch (Exception ex)
            { }

            return texto;
        }

        #endregion

        #region CANCELAMENTO

        public string recuperaTagxMotivoCancelamento(string texto)
        {
            string valor = "";

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = texto.Replace("\"", "'");

            dynamic resultado = serializer.DeserializeObject(json);

            var subtags = resultado["retEvento"];

            valor = (string)subtags["xMotivo"];

            return valor;
        }

        public string recuperaTagPROTOCOLOCANCELAMENTO(string texto)
        {
            string valor = "";

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = texto.Replace("\"", "'");

            dynamic resultado = serializer.DeserializeObject(json);

            var subtags = resultado["retEvento"];

            valor = (string)subtags["nProt"];

            return valor;
        }

        public string recuperaTagdhRegEventoCancelamento(string texto)
        {
            string valor = "";

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = texto.Replace("\"", "'");

            dynamic resultado = serializer.DeserializeObject(json);

            var subtags = resultado["retEvento"];

            valor = (string)subtags["dhRegEvento"];

            return valor;
        }

        public string recuperaTagtpEvento(string texto)
        {
            try
            {
                int ini = 0;
                int fim = 0;

                ini = texto.IndexOf("<tpEvento>");
                fim = texto.IndexOf("</tpEvento>");
                texto = texto.Substring(ini + 10, (fim - ini) - 10);
            }
            catch (Exception ex)
            { }

            return texto;
        }

        public string recuperaTagCorrecao(string texto)
        {
            try
            {
                int ini = 0;
                int fim = 0;

                ini = texto.IndexOf("<xCorrecao>");
                fim = texto.IndexOf("</xCorrecao>");
                texto = texto.Substring(ini + 11, (fim - ini) - 11);
            }
            catch (Exception ex)
            { }

            return texto;
        }

        #endregion

        public string recuperaTagxMotivoConsultaSituacao(string texto)
        {
            string valor = "";

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = texto.Replace("\"", "'");

            dynamic resultado = serializer.DeserializeObject(json);

            var subtags = resultado["retConsSitNFe"];

            valor = (string)subtags["xMotivo"];

            return valor;
        }

        #region METODOS NECESSÁRIOS PARA PARA MANIPULAR AS NOVAS NF

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="taginicial"></param>
        /// <param name="tagfinal"></param>
        /// <returns>Devolve o xml sem as tags especificadas</returns>
        public string excluirTagEspecifica(string xml, string taginicial, string tagfinal)
        {

            string retirar = "";

            try
            {
                int ini = 0;
                int fim = 0;


                ini = xml.IndexOf(taginicial);
                fim = xml.LastIndexOf(tagfinal);
                retirar = xml.Substring(ini, (fim - ini) + tagfinal.Length);

                xml = xml.Replace(retirar, "");
            }
            catch (Exception ex)
            { }

            return xml;

        }

        private static string criaListaDeTags(string tagInicio, string tagFim, string[] valores)
        {
            string listaDeValores = "";
            var colecao = from s in valores
                          select s;
            foreach (var item in colecao)
            {
                listaDeValores += String.Format("{0}{1}{2}{3}", tagInicio, item, tagFim, Environment.NewLine);
            }
            return listaDeValores;
        }

        private string novaTag(string xml, string tagPai, string tagInicio, string valor, string tagFim)
        {
            //determina onde vai a tag
            int ini = xml.IndexOf(tagPai);

            if (ini >= 0) //somente insere a tag se existir a tagPai
            {
                //nova tag
                if (xml.IndexOf(tagInicio) < 0) //somente inseri se for tag unica
                {
                    string novatag = String.Format("{0}{1}{2}{3}", Environment.NewLine, tagInicio, valor, tagFim);
                    xml = xml.Insert(ini + tagPai.Length, novatag);
                }
            }

            return xml;
        }

        public string moveTag(string xml, string tagAlvo, string tagInicio, string tagFim)
        {


            //determina onde vai a tag
            int ini = xml.IndexOf(tagAlvo);

            //pega o bloco
            int iniBloco = xml.IndexOf(tagInicio);
            int fimBloco = xml.LastIndexOf(tagFim);
            string blocoDeTags = xml.Substring(iniBloco, (fimBloco - iniBloco) + tagFim.Length);

            //retira
            xml = xml.Replace(blocoDeTags, "");

            //inserir
            xml = xml.Insert((ini - blocoDeTags.Length) + tagAlvo.Length, blocoDeTags);

            return xml;
        }

        public string RetornaTagLista(string retorno_da_sefaz)
        {
            string frase = retorno_da_sefaz.Replace("[", "#").Replace("]", "#");
            string saida = "";
            string conteudo = "";

            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"#.*#");
            Match mat = rg.Match(frase);

            if (mat.Success)
                saida = mat.Value;

            if (saida.Length > 0)
            {
                conteudo = saida.Replace("#", "").Replace("\"", "'");

            }

            return conteudo;
        }

        public string formataXML4_00(string texto)
        {
            //troca aspas duplas por aspas simples
            string xml = texto.Replace("\"", "'");


            /*###################################### EXEMPLO DE USO NÃO EXCLUIR ########################################
 
            --------------------------------
            COMO EXCLUIR UMA TAG
            --------------------------------
 
            xml = excluirTagEspecifica(xml, "<NomeDaTag>", "</NomeDaTag>");  
           
            -------------------------------
            COMO CRIAR UMA TAG SIMPLES
            -------------------------------
 
            xml = NovaTag(xml, "<TagAlvo>", "<NomeDaTag>", "Valor da Tag", "</NomeDaTag>");
            ou
            xml = NovaTag(xml, "</TagAlvo>", "<NomeDaTag>", "Valor da Tag", "</NomeDaTag>");
            ou
            xml = NovaTag(xml, "<TagAlvo>", "<NomeDaTag>", "<TagFilha>valor</TagFilha>", "</NomeDaTag>");

            Observação: A nova tag será criada imediatamente depois da tagAlvo informada nova parametro.


            -----------------------------------------------------------------------------------------
            COMO CRIAR UMA TAG PAI COM UMA LISTA DE TAGS(VALORES DE UM BANCO DE DADOS OU MANUAL)
            -----------------------------------------------------------------------------------------

            1) Criar a lista de valores Array de String
            string[] valores = { "funcionario 1", "funcionario 2", "funcionario 3" };

            2) Informe o nome da Tag que conterá esses valores
            string listaDeValores = criaListaDeTags("<Func>", "</Func>", valores);

            3) Crie Informe a tagAlvo, a Tag Pai, a lista criada e o fechamento da tag Pai
            xml = novaTag(xml, "</transp>", "<Itinit>", listaDeValores, "</Itinit>"); 

            Obs: Valores recuperados de um banco de dados devem ser transformados em um array.
        
            /*###############################################################################*/


            //ATENÇÃO 
            //Utilize a variavel xml como no exemplo abaixo porque sempre será retornado o xml alterado
            //Edite os itens abaxo para adequar o xml


            #region TAGS EXCLUIDAS

            //TODAS AS TAGS EXCLUIDAS
            xml = excluirTagEspecifica(xml, "<indPag>", "</indPag>");

            #endregion


            #region NOVAS TAGS

            //NOVAS TAGS


            //<indEscala>
            xml = novaTag(xml, "</CEST>", "<indEscala>", "0", "</indEscala>");

            //<CNPJFab>
            xml = novaTag(xml, "</indEscala>", "<CNPJFab>", "0", "</CNPJFab>");

            //<cBenef>
            xml = novaTag(xml, "</CNPJFab>", "<cBenef>", "0", "</cBenef>");

            //<rastro>
            xml = novaTag(xml, "</indTot>", "<rastro>", "", "</rastro>");

            //<nLote>
            xml = novaTag(xml, "<rastro>", "<nLote>", "0", "</nLote>");

            //<qLote>
            xml = novaTag(xml, "</nLote>", "<qLote>", "0", "</qLote>");

            //<dFab>
            xml = novaTag(xml, "</qLote>", "<dFab>", "0", "</dFab>");

            //<dVal>
            xml = novaTag(xml, "</dFab>", "<dVal>", "0", "</dVal>");

            //<cAgreg>
            xml = novaTag(xml, "</dVal>", "<cAgreg>", "0", "</cAgreg>");


            //xml = novaTag(xml, "</serie>", "<mod1>", "0", "</mod1>");

            #endregion

            //devolve o xml com aspas duplas, NÃO ALTERAR AS ULTIMAS DUAS LINHAS!
            xml = xml.Replace("'", "\"");
            return xml;
        }

        #endregion

    }
}
