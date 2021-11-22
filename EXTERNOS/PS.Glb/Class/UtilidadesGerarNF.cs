using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PS.Glb.Class
{
    public class UtilidadesGerarNF
    {
        public string extrairNumDaChave(string num)
        {
            string novo = num.Substring(35, 8);
            return novo;
        }

        public string extrairUltimoDigitoChave(string num)
        {
            string novo = num.Substring(num.Length - 1, 1);
            return novo;
        }

        public string trocarVirgulaPorPonto(string num)
        {
            num = num.Replace(".", "");
            num = num.Replace(",", ".");
            return num;
        }

        public string limparCaracteres(string texto)
        {
            texto = texto.Replace(".", "");
            texto = texto.Replace(",", "");
            texto = texto.Replace("-", "");
            texto = texto.Replace("/", "");
            texto = texto.Replace("(", "");
            texto = texto.Replace(")", "");
            texto = texto.Replace(" ", "");

            return texto;
        }

        public string limparXMLTagVazia(string xml)
        {
            //limpa as tags
            xml = xml.Replace("\t", "");
            string xml_saida = "";
            string ok = "";

            //converte tudo em uma lista
            string[] ls = xml.Split('\n');

            //para os filhos
            foreach (var item in ls)
            {
                if (item.Contains("><") && !item.Contains("<cEAN") && !item.Contains("<cEANTrib"))
                    continue;
                xml_saida += item + Environment.NewLine;
            }

            try
            {
                xml_saida = xml_saida.Replace('&','@');
                var doc = XDocument.Parse(xml_saida);
                doc.Descendants()
                    .Where(e => e.IsEmpty || String.IsNullOrEmpty(e.Value)).Remove();
                return doc.ToString().Replace('@', '&');
            }
           
            catch (Exception ex)
            {
                return "";
            }

            
            //para os pais
            /*
            string[] xml_tag_vazias = xml_saida.Split('\n');
            List<string> tagsExcluir = new List<string>();

            for (int i = 0; i < xml_tag_vazias.Length - 1; i++)
            {
                if (i <= xml_tag_vazias.Length)
                {
                    if (xml_tag_vazias[i].ToString() == xml_tag_vazias[i + 1].ToString().Replace("/", ""))
                    {
                        tagsExcluir.Add(xml_tag_vazias[i].ToString());
                        tagsExcluir.Add(xml_tag_vazias[i + 1].ToString());
                    }
                }
            }

            string[] x = xml_saida.Split('\n');
            for (int i = 0; i < x.Length; i++)
            {

                if (existLista(tagsExcluir, x[i]))
                    continue;
                //ok += x[i] + Environment.NewLine;
                ok += x[i];
            }

            ok = ok.Replace("\n", "");
            return ok;
            */

        }

        private string limparTagAnalisar(string tag)
        {
            tag = tag.Replace("\r", "");
            tag = tag.Replace("\t", "");
            tag = tag.Replace("\n", "");

            return tag;
        }

        private bool existLista(List<string> lista, string tag)
        {
            bool saida = false;
            for (int i = 0; i < lista.Count; i++)
            {
                if (limparTagAnalisar(lista[i]) == limparTagAnalisar(tag))
                    saida = true;
            }

            return saida;
        }

        public string GerarChaveAcessoNFe(int cUF, string AAMM, string CNPJ, string mod, string serie, string nNF, int tpEmis, string cNF)
        {
            if (cUF.ToString().Length != 2)
                throw new Exception("");
            if (AAMM.Length != 4)
                throw new Exception("");
            if (CNPJ.Length != 14)
                throw new Exception("");
            if (mod.Length != 2)
                throw new Exception("");
            if (serie.Length != 3)
                serie = serie.PadLeft(3, '0');
            if (nNF.Length != 9)
                nNF = nNF.PadLeft(9, '0');
            if (tpEmis.ToString().Length != 1)
                throw new Exception("");
            if (cNF.Length != 8)
                throw new Exception("");

            string chave = string.Concat(cUF, AAMM, CNPJ, mod, serie, nNF, tpEmis, cNF);
            string chaveacesso = string.Concat(chave, this.GerarDigitoVerificadorNFe(chave));

            return chaveacesso;
        }

        public string GerarDigitoVerificadorNFe(string chave)
        {
            string sDV = string.Empty;
            int soma = 0; // Vai guardar a Soma
            int mod = -1; // Vai guardar o Resto da divisão
            int dv = -1;  // Vai guardar o DigitoVerificador
            int pesso = 2; // vai guardar o pesso de multiplicacao

            //percorrendo cada caracter da chave da direita para esquerda para fazer os calculos com o pesso
            for (int i = chave.Length - 1; i != -1; i--)
            {
                int ch = Convert.ToInt32(chave[i].ToString());
                soma += ch * pesso;
                //sempre que for 9 voltamos o pesso a 2
                if (pesso < 9)
                    pesso += 1;
                else
                    pesso = 2;
            }

            //Agora que tenho a soma vamos pegar o resto da divisão por 11
            mod = soma % 11;
            //Aqui temos uma regrinha, se o resto da divisão for 0 ou 1 então o dv vai ser 0
            if (mod == 0 || mod == 1)
                dv = 0;
            else
                dv = 11 - mod;

            sDV = dv.ToString();
            return sDV;
        }

        public string ajustaSerie(string num)
        {
            string nova_serie = string.Empty;

            if (num.Length > 1)
            {
                string serie_ultimo_digito = num.Substring(2, 1);
                string serie_primeiro_digito = num.Substring(0, 2).TrimStart('0');
                nova_serie = string.Concat(serie_primeiro_digito, serie_ultimo_digito);
            }
            else
            {
                nova_serie = num;
            }
            return nova_serie;
        }

        public string casasDecimais_2(string num)
        {
            return String.Format("{0:0.00}", Convert.ToDouble(num));
        }


        public string casasDecimais_3(string num)
        {
            return String.Format("{0:0.000}", Convert.ToDouble(num));
        }

        public string casasDecimais_4(string num)
        {
            return String.Format("{0:0.0000}", Convert.ToDouble(num));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_codoper"></param>
        /// <param name="_chave"></param>
        /// <returns></returns>
        public int InsertGnfestadual(int _codoper, string _chave)
        {
            int Retorno = 0;
            Retorno = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GNFESTADUAL(CODEMPRESA, CODOPER, CODSTATUS, CHAVEACESSO, XMLNFE, RECIBO, DATARECIBO, PROTOCOLO, DATAPROTOCOLO, DANFEIMPRESSA, EMAILENVIADO, FORMATOIMPRESSAO, CODTIPOPER, XMLGERADO)
                                                                                    VALUES
                                                                                    (" + AppLib.Context.Empresa + ", " + _codoper + ", 'P', '" + _chave + "', null, null , null, null, null, " + 0 + "," + 0 + ", null, null, null)", new object[] { });

            return Retorno;
        }

        public string recuperaNFAT(string numero)
        {
            //retira os zeros a esquerda
            numero = numero.TrimStart('0');
            int barra = numero.IndexOf('/');
            numero = numero.Substring(0, barra);
            return numero;
        }

        public string recuperaNDUP(string numero)
        {
            int barra = numero.IndexOf('/');
            numero = numero.Substring(barra + 1, numero.Length - barra - 1);
            return numero.PadLeft(3, '0');
        }

        public string RemoveCaracterEspecial(string _xml)
        {
            // Cria três matrizes, uma para acentos, sem acentos e caracteres especiais
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };
            string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°" };
            //Replace em todos os acentos
            for (int i = 0; i < acentos.Length; i++)
            {
                _xml = _xml.Replace(acentos[i], semAcento[i]);
            }
            //Replace em todos os caracteres especiais            
            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                _xml = _xml.Replace(caracteresEspeciais[i], "");
            }
            //Replace nos espaços iniciais
            _xml = _xml.Replace("^\\s+", "");
            _xml = _xml.Replace("\\s+$", "");
            //Replace nos espaço duplos e tabulãções
            _xml = _xml.Replace("\\s+", " ");
            return _xml.Trim();
        }

        public string ReplaceTabelaHTML(string _xml)
        {
            if (_xml.Contains("&"))
            {
                _xml = _xml.Replace("&", "&amp;");
            }
            //if (_xml.Contains("Ø"))
            //{
            //    _xml = _xml.Replace("Ø", "&oslash;");
            //}

            return _xml;
        }

        public string trataCaracteres(string texto)
        {

            //texto = texto.Replace("#", "&#35;");
            //texto = texto.Replace("$", "&#36;");
            //texto = texto.Replace("%", "&#37;");

            texto = texto.Replace("&", "&#38;");

            //texto = texto.Replace("(", "&#40;");
            //texto = texto.Replace(")", "&#41;");
            //texto = texto.Replace("*", "&#42;");
            //texto = texto.Replace("+", "&#43;");
            //texto = texto.Replace(",", "&#44;");
            //texto = texto.Replace("-", "&#45;");
            //texto = texto.Replace(".", "&#46;");
            //texto = texto.Replace(":", "&#58;");
            //texto = texto.Replace(";", "&#59;");
            //texto = texto.Replace("@", "&#64;");
            //texto = texto.Replace("[", "&#91;");
            //texto = texto.Replace("]", "&#93;");
            //texto = texto.Replace("^", "&#94;");
            //texto = texto.Replace("_", "&#95;");
            //texto = texto.Replace("`", "&#96;");
            //texto = texto.Replace("{", "&#123;");
            //texto = texto.Replace("|", "&#124;");
            //texto = texto.Replace("}", "&#125;");
            //texto = texto.Replace("~", "&#126;");
            //texto = texto.Replace("¡", "&#161;");
            //texto = texto.Replace("¢", "&#162;");
            //texto = texto.Replace("£", "&#163;");
            //texto = texto.Replace("¤", "&#164;");
            //texto = texto.Replace("¥", "&#165;");
            //texto = texto.Replace("¦", "&#166;");
            //texto = texto.Replace("§", "&#167;");
            //texto = texto.Replace("¨", "&#168;");
            //texto = texto.Replace("©", "&#169;");
            //texto = texto.Replace("ª", "&#170;");
            //texto = texto.Replace("«", "&#171;");
            //texto = texto.Replace("¬", "&#172;");
            //texto = texto.Replace(" ", "#173; ");
            //texto = texto.Replace("®", "&#174;");
            //texto = texto.Replace("¯", "&#175;");
            //texto = texto.Replace("°", "&#176;");
            //texto = texto.Replace("±", "&#177;");
            //texto = texto.Replace("²", "&#178;");
            //texto = texto.Replace("³", "&#179;");
            //texto = texto.Replace("´", "&#180;");
            //texto = texto.Replace("µ", "&#181;");
            //texto = texto.Replace("¶", "&#182;");
            //texto = texto.Replace("·", "&#183;");
            //texto = texto.Replace("¸", "&#184;");
            //texto = texto.Replace("¹", "&#185;");
            //texto = texto.Replace("º", "&#186;");
            //texto = texto.Replace("»", "&#187;");
            //texto = texto.Replace("¼", "&#188;");
            //texto = texto.Replace("½", "&#189;");
            //texto = texto.Replace("¾", "&#190;");
            //texto = texto.Replace("¿", "&#191;");

            texto = texto.Replace("À", "A");
            texto = texto.Replace("Á", "A");
            texto = texto.Replace("Â", "A");
            texto = texto.Replace("Ã", "A");
            texto = texto.Replace("Ä", "A");
            texto = texto.Replace("Å", "A");

            //texto = texto.Replace("Æ", "&#198;");
            //texto = texto.Replace("Ç", "&#199;");

            texto = texto.Replace("È", "E");
            texto = texto.Replace("É", "E");
            texto = texto.Replace("Ê", "E");
            texto = texto.Replace("Ë", "E");
            texto = texto.Replace("Ì", "I");

            texto = texto.Replace("Í", "I");
            texto = texto.Replace("Í", "I");
            texto = texto.Replace("Î", "I");
            texto = texto.Replace("Ï", "I");

            //texto = texto.Replace("Ð", "&#208;");
            //texto = texto.Replace("Ñ", "&#209;");

            texto = texto.Replace("Ò", "O");
            texto = texto.Replace("Ó", "O");
            texto = texto.Replace("Ô", "O");
            texto = texto.Replace("Õ", "O");
            texto = texto.Replace("Ö", "O");

            //texto = texto.Replace("×", "&#215;");
            //texto = texto.Replace("Ø", "&#216;");

            texto = texto.Replace("Ù", "U");
            texto = texto.Replace("Ú", "U");
            texto = texto.Replace("Û", "U");
            texto = texto.Replace("Ü", "U");

            //texto = texto.Replace("Ý", "&#221;");
            //texto = texto.Replace("Þ", "&#222;");
            //texto = texto.Replace("ß", "&#223;");

            texto = texto.Replace("à", "a");
            texto = texto.Replace("á", "a");
            texto = texto.Replace("â", "a");
            texto = texto.Replace("ã", "a");
            texto = texto.Replace("ä", "a");
            texto = texto.Replace("å", "a");

            //texto = texto.Replace("æ", "&#230;");
            //texto = texto.Replace("ç", "&#231;");

            texto = texto.Replace("è", "e");
            texto = texto.Replace("é", "e");
            texto = texto.Replace("ê", "e");
            texto = texto.Replace("ë", "e");

            texto = texto.Replace("ì", "i");
            texto = texto.Replace("í", "i");
            texto = texto.Replace("í", "i");
            texto = texto.Replace("î", "i");
            texto = texto.Replace("ï", "i");

            //texto = texto.Replace("ð", "&#240;");
            //texto = texto.Replace("ñ", "&#241;");

            texto = texto.Replace("ò", "o");
            texto = texto.Replace("ó", "o");
            texto = texto.Replace("ô", "o");
            texto = texto.Replace("õ", "o");
            texto = texto.Replace("ö", "o");

            //texto = texto.Replace("÷", "&#247;");
            //texto = texto.Replace("ø", "&#248;");

            texto = texto.Replace("ù", "u");
            texto = texto.Replace("ú", "u");
            texto = texto.Replace("û", "u");
            texto = texto.Replace("ü", "u");

            //texto = texto.Replace("ý", "&#253;");
            //texto = texto.Replace("þ", "&#254;");
            //texto = texto.Replace("ÿ", "&#255;");

            texto = texto.Replace("Ā", "A");
            texto = texto.Replace("Ā", "A");

            return texto;
        }
    }
}
