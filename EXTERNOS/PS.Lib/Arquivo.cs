using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class Arquivo
    {
        public Boolean Existe(String NomeArquivo)
        {
            return System.IO.File.Exists(NomeArquivo);
        }

        public void Escrever(String NomeArquivo, String Texto)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(NomeArquivo);
            sw.Write(Texto + "\r\n");
            sw.Close();
        }

        public bool CriarCopia(String pathOrigem, String pathDestino)
        {
            try
            {
                System.IO.File.Copy(pathOrigem, pathDestino, true);
                return true;
            }
            catch
            {
                return false;            
            }                    
        }

        public List<String> LerArray(String NomeArquivo)
        {
            List<String> st = new List<String>();

            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(NomeArquivo);

                String linha = "";
                int cont = 0;
                while (linha != null)
                {
                    linha = sr.ReadLine();
                    if (linha != null)
                    {
                        st.Add(linha);
                        cont++;
                    }
                }
                sr.Close();
                return st;
            }
            catch (Exception ex)
            {
                st.Add(ex.Message);
                return st;
            }
        }

        public String Ler(String NomeArquivo)
        {
            if (!Existe(NomeArquivo))
            {
                Escrever(NomeArquivo, "");
            }

            System.IO.StreamReader sr = new System.IO.StreamReader(NomeArquivo);

            String linha = "";
            String texto = "";
            while (linha != null)
            {
                linha = sr.ReadLine();
                if (linha != null) texto += linha + "\r\n";
            }
            sr.Close();
            return texto;
        }

        public String LerLinha(String NomeArquivo, int NumeroLinha)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(NomeArquivo);
            int i = 0;
            String result = "";
            String linha = "";
            while (linha != null)
            {
                linha = sr.ReadLine();
                if (linha != null) i++;
                if (i == NumeroLinha) result = linha;
            }
            sr.Close();
            return result;
        }

        public int TotalLinhas(String NomeArquivo)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(NomeArquivo);
            int i = 0;
            String linha = "";
            while (linha != null)
            {
                linha = sr.ReadLine();
                if (linha != null) i++;
            }
            sr.Close();
            return i;
        }

        public void Acrescentar(String NomeArquivo, String Texto)
        {
            String temp = this.Ler(NomeArquivo);
            this.Escrever(NomeArquivo, temp + Texto);
        }

    }
}
