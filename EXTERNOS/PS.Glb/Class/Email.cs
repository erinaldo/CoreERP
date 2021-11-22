using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PS.Glb.Class
{
    public class Email
    {

        public string Host { get; set; }
        public bool EnableSsl { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public int Porta { get; set; }

        public Email(string _Host, bool _EnableSsl, string _Usuario, string _Senha, bool _UseDefaultCredentials, int _Porta)
        {
            Host = _Host;
            EnableSsl = _EnableSsl;
            Usuario = _Usuario;
            Senha = _Senha;
            UseDefaultCredentials = _UseDefaultCredentials;
            Porta = _Porta;
        }



        public int envio(string de, string para, string assunto, string mensagem, string[] caminho_anexo, string[] emails_adicionais)
        {
            int retorno = 0;

            try
            {
                /*
                string De = "workflow@itinit.com.br";
                string Para = para;
                //string Copia1 = cc1;
                //string Copia2 = cc2;
                string Assunto = titulo;
                string Host = "smtp.itinit.com.br";
                int Porta = 587;
                string Usuario = De;
                //string Senha = @"it@101@2017%12";
                string Senha = "aiti!2017@naite";
                int Timeout = 3600000;
                string Mensagem = texto;
                */

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(Host, Porta);
                client.Timeout = 3600000;
                client.UseDefaultCredentials = false;

                System.Net.NetworkCredential netcred = new System.Net.NetworkCredential(Usuario, Senha);
                client.Credentials = netcred;

                client.EnableSsl = false;

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(de, para, assunto, mensagem);

                //com copia1
                /*
                if (!string.IsNullOrEmpty(cc1))
                    message.CC.Add(cc1);

                //com copia2
                if (!string.IsNullOrEmpty(cc2))
                    message.CC.Add(cc2);
                    */

                message.IsBodyHtml = true;


                if (emails_adicionais != null)
                {
                    foreach (var item in emails_adicionais)
                    {
                        if (string.IsNullOrEmpty(item))
                        {
                            continue;
                        }
                        message.CC.Add(item.Trim());
                    }
                }

                if (caminho_anexo != null)
                {
                    //System.Collections.ArrayList lista = new System.Collections.ArrayList();

                    foreach (var item in caminho_anexo)
                    {
                        if (string.IsNullOrEmpty(item))
                        {
                            continue;
                        }
                        message.Attachments.Add(new System.Net.Mail.Attachment(item));
                    }
                }
                client.Send(message);

                retorno = 1;
            }
            catch (Exception ex)
            {
                retorno = 0;
            }

            return retorno;
        }



    }


}

