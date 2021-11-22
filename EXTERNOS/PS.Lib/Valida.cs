using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class Valida
    {
        // Método de validação do CPF
        public Boolean validarCPF(String cpf)
        {
            if (cpf.Equals("000.000.000-00")) { return false; }
            if (cpf.Equals("111.111.111-11")) { return false; }
            if (cpf.Equals("222.222.222-22")) { return false; }
            if (cpf.Equals("333.333.333-33")) { return false; }
            if (cpf.Equals("444.444.444-44")) { return false; }
            if (cpf.Equals("555.555.555-55")) { return false; }
            if (cpf.Equals("666.666.666-66")) { return false; }
            if (cpf.Equals("777.777.777-77")) { return false; }
            if (cpf.Equals("888.888.888-88")) { return false; }
            if (cpf.Equals("999.999.999-99")) { return false; }

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11) return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++) soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2) resto = 0;
            else resto = 11 - resto;
            digito = resto.ToString();

            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++) soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2) resto = 0;
            else resto = 11 - resto;
            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        // Método de validação do CNPJ
        public Boolean validarCNPJ(string cnpj)
        {
            if (cnpj.Equals("00.000.000/0000-00")) { return false; }
            if (cnpj.Equals("11.111.111/1111-11")) { return false; }
            if (cnpj.Equals("22.222.222/2222-22")) { return false; }
            if (cnpj.Equals("33.333.333/3333-33")) { return false; }
            if (cnpj.Equals("44.444.444/4444-44")) { return false; }
            if (cnpj.Equals("55.555.555/5555-55")) { return false; }
            if (cnpj.Equals("66.666.666/6666-66")) { return false; }
            if (cnpj.Equals("77.777.777/7777-77")) { return false; }
            if (cnpj.Equals("88.888.888/8888-88")) { return false; }
            if (cnpj.Equals("99.999.999/9999-99")) { return false; }

            string CNPJ = cnpj.Replace(".", ""); 
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", ""); 
            int[] digitos, soma, resultado; 
            int nrDig; string ftmt; 
            bool[] CNPJOk; 
            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2]; 
            soma[0] = 0; 
            soma[1] = 0; 
            resultado = new int[2];
            resultado[0] = 0; 
            resultado[1] = 0; 
            CNPJOk = new bool[2]; 
            CNPJOk[0] = false;
            CNPJOk[1] = false; 
            try 
            { 
                for (nrDig = 0; nrDig < 14; nrDig++)
                { 
                    digitos[nrDig] = int.Parse( CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11) soma[0] += (digitos[nrDig] * int.Parse(ftmt.Substring( nrDig + 1, 1)));
                    if (nrDig <= 12) soma[1] += (digitos[nrDig] * int.Parse(ftmt.Substring( nrDig, 1))); 
                } 
                for (nrDig = 0; nrDig < 2; nrDig++) 
                {
                    resultado[nrDig] = (soma[nrDig] % 11); 
                    if ((resultado[nrDig] == 0) || ( resultado[nrDig] == 1)) CNPJOk[nrDig] = ( digitos[12 + nrDig] == 0); 
                    else CNPJOk[nrDig] = ( digitos[12 + nrDig] == ( 11 - resultado[nrDig])); 
                } 
                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            } 
        }

        // Método de validação de e-mail
        public Boolean validarEmail(String email)
        {
            //// Corrigir o antes de enviar para este método
            //email = email.Replace(" ", "");

            String caracterarroba = "@";
            String ponto = ".";

            int cont = 0;
            int arroba = 0;

            // Ter no mínimo 7 caracteres
            if (email.Length < 7)
            {
                return false;
            }

            // Uma arroba no meio da String
            for (int i = 0; i < (email.Length); i++)
            {
                if (caracterarroba.Equals(email[i].ToString()))
                {
                    cont++;
                    if (arroba == 0)
                    {
                        arroba = i;
                    }
                }
            }
            if (cont != 1)
            {
                return false;
            }
            else
            {
                cont = 0;
            }

            // No mínimo um ponto depois da arroba
            for (int i = arroba; i < (email.Length); i++)
            {
                if (ponto.Equals(email[i].ToString()))
                {
                    cont++;
                }
            }
            if (cont == 0)
            {
                return false;
            }

            // O ultimo caracter não pode ser um ponto
            if (ponto.Equals(email[email.Length - 1].ToString()))
            {
                return false;
            }

            // Se passar pelos testes retornar verdadeiro
            return true;
        }

        public Boolean validarData(String data)
        {
            if (data.Length == 10)
            {
                try
                {
                    System.DateTime.Parse(data);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public Boolean validarHora(String horaMinuto)
        {
            if (horaMinuto.Length == 5)
            {
                int hora = int.Parse(horaMinuto.Substring(0, 2));
                int minuto = int.Parse(horaMinuto.Substring(3, 2));

                if (hora > 23)
                {
                    return false;
                }

                if (minuto > 59)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public Boolean validarDataHora(String dataHora)
        {
            if (dataHora.Length == 16)
            {
                if (validarData(dataHora.Substring(0, 10)) && validarHora(dataHora.Substring(11, 5)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
