using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb.Class
{
    public class Criptografia1
    {
        private decimal Multiplicador;
        private List<String> Letras;

        public Criptografia1()
        {
            Multiplicador = 263;

            Letras = new List<String>();
            Letras.Add("R");
            Letras.Add("M");
            Letras.Add("S");
            Letras.Add("E");
            Letras.Add("T");
            Letras.Add("N");
            Letras.Add("I");
            Letras.Add("U");
            Letras.Add("Q");
            Letras.Add("O");
        }

        public String Encoder(int x)
        {
            try
            {
                decimal temp = x * Multiplicador;
                String s = temp.ToString();

                String result = "";

                for (int i = 0; i < s.Length; i++)
                {
                    int numero = int.Parse(s[i].ToString());
                    result += Letras[numero];
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }


        }

        public int Decoder(String s)
        {
            try
            {
                String temp = "";

                for (int i = 0; i < s.Length; i++)
                {
                    int posicao = Letras.IndexOf(s[i].ToString());
                    temp += posicao;
                }

                decimal result = decimal.Parse(temp);
                result = result / Multiplicador;
                return int.Parse(result.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }


        }
    }
}
