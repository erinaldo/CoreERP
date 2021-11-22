using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace PS.Lib
{
    public class Utils
    {
        public string RetornaDataHoraMinSec()
        {
            DateTime data = DateTime.Now;

            string mil = data.Millisecond.ToString();
            string sec = data.Second.ToString();
            string min = data.Minute.ToString();
            string hor = data.Hour.ToString();
            string dia = data.Day.ToString();
            string mes = data.Month.ToString();
            string ano = data.Year.ToString();

            if (mil.Length == 1)
                mil = string.Concat("00", mil);
            else if (mil.Length == 2)
                mil = string.Concat("0", mil);

            if (sec.Length == 1)
                sec = string.Concat("0", sec);

            if (min.Length == 1)
                min = string.Concat("0", min);

            if (hor.Length == 1)
                hor = string.Concat("0", hor);

            if (dia.Length == 1)
                dia = string.Concat("0", dia);

            if (mes.Length == 1)
                mes = string.Concat("0", mes);

            return string.Concat(ano, mes, dia, hor, min, sec, mil);        
        }

        public static bool MascaraCaracterValido(char code, string mask, int index)
        {
            var Digitos = "0123456789";
            if (mask[index] == '.')
            {
                if (code == '.')
                    return true;
                else
                    return false;
            }
            else
            {
                if (mask[index] == '#')
                {
                    if (Digitos.Contains(code.ToString()))
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;                            
                }
            }
        }

        public static string MascaraNivelAnterior(string code)
        {
            code = code.Replace('.',';');
            Regex rSplit = new Regex(";");
            string[] sFields = rSplit.Split(code);
            string retorno = string.Empty;
            if (sFields.Length == 1)
            {
                return retorno;
            }
            else
            {
                if (sFields.Length > 1)
                {
                    for(int i = 0; i < sFields.Length; i++)
                    {
                        if(i != (sFields.Length -1))
                        {
                            if (i == (sFields.Length - 2))
                                retorno = string.Concat(retorno, sFields[i]);
                            else
                                retorno = string.Concat(retorno, sFields[i], ".");
                        }
                    }
                }
            }

            return retorno;
        }

        public static string RemoveCaracterSpecial(string str)
        {
            // Cria três matrizes, uma para acentos, sem acentos e caracteres especiais
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };
            string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°" };
            //Replace em todos os acentos
            for (int i = 0; i < acentos.Length; i++)
            {
                str = str.Replace(acentos[i], semAcento[i]);
            }
            //Replace em todos os caracteres especiais            
            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                str = str.Replace(caracteresEspeciais[i], "");
            }
            //Replace nos espaços iniciais
            str = str.Replace("^\\s+", "");
            str = str.Replace("\\s+$", "");
            //Replace nos espaço duplos e tabulãções
            str = str.Replace("\\s+", " ");
            return str.Trim();
        }

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
    }
}
