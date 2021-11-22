using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib.OBJETOS_VALIDATESERVICE
{
    public static class FuncoesAuxiliares
    {
        public static string XMLGerado;

        public static string excluirTagsXML(string texto)
        {
            try
            {

                //pega o texto do texto1
                int ini = 0;
                int fim = 0;

                //excluir a tag <enviNFe até </indSinc>
                ini = texto.IndexOf("<enviNFe");
                fim = texto.IndexOf("<NFe xmlns");
                texto = texto.Remove(ini, (fim - ini));

                //retira o fechamento da </enviNFe>
                texto = texto.Replace("</enviNFe>", "");
            }
            catch (Exception)
            { }

            return texto;
        }
    }
}
