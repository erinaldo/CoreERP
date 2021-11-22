using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class EmailParams : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idConfigPOP;
        private string _nome;
        private string _host;
        private int _porta;
        private string _login;
        private string _senha;
        private int _ssl;
        private int _ativo;

        [ParamsAttribute("IDCONFIGPOP")]
        [DataMember]
        public int IdConfigPOP
        {
            get
            {
                return this._idConfigPOP;
            }
            set
            {
                this._idConfigPOP = value;
            }
        }

        [ParamsAttribute("NOME")]
        [DataMember]
        public string Nome
        {
            get
            {
                return this._nome;
            }
            set
            {
                this._nome = value;
            }
        }

        [ParamsAttribute("HOST")]
        [DataMember]
        public string Host
        {
            get
            {
                return this._host;
            }
            set
            {
                this._host = value;
            }
        }

        [ParamsAttribute("PORTA")]
        [DataMember]
        public int Porta
        {
            get
            {
                return this._porta;
            }
            set
            {
                this._porta = value;
            }
        }

        [ParamsAttribute("LOGIN")]
        [DataMember]
        public string Login
        {
            get
            {
                return this._login;
            }
            set
            {
                this._login = value;
            }
        }

        [ParamsAttribute("SENHA")]
        [DataMember]
        public string Senha
        {
            get
            {
                return this._senha;
            }
            set
            {
                this._senha = value;
            }
        }

        [ParamsAttribute("SSL")]
        [DataMember]
        public int SSL
        {
            get
            {
                return this._ssl;
            }
            set
            {
                this._ssl = value;
            }
        }

        [ParamsAttribute("ATIVO")]
        [DataMember]
        public int Ativo
        {
            get
            {
                return this._ativo;
            }
            set
            {
                this._ativo = value;
            }
        }

        public static EmailParams ReadByIdConfigPOP(params object[] parameters)
        {
            string sSql = @"SELECT * FROM ZCONFIGPOP WHERE IDCONFIGPOP = ?";
            EmailParams _emailParams = new EmailParams();
            _emailParams.ReadFromCommand(sSql, parameters);
            return _emailParams;
        }

        public void Execute()
        {
            this.Receber3(this.IdConfigPOP, this.Host, this.Porta, (this.SSL == 1), this.Login, this.Senha);
        }

        private void Receber(int IDCONFIG, string hostname, int port, bool useSsl, string username, string password)
        {
            //string hostname = "pop3.bol.com.br";
            //int port = 995;
            //bool useSsl = true;
            //string username = "loginSemArroba";
            //string password = "senhaEmail";

            // The client disconnects from the server when being disposed
            using (OpenPop.Pop3.Pop3Client client = new OpenPop.Pop3.Pop3Client())
            {
                // Connect to the server
                client.Connect(hostname, port, useSsl);

                // Authenticate ourselves towards the server
                client.Authenticate(username, password);

                // Get the number of messages in the inbox
                int messageCount = client.GetMessageCount();

                // Messages are numbered in the interval: [1, messageCount]
                // Ergo: message numbers are 1-based.
                // Most servers give the latest message the highest number
                for (int i = messageCount; i > 0; i--)
                {
                    OpenPop.Mime.Header.MessageHeader headers;

                    String emitente = "";
                    String subject = "";
                    String data = "";

                    try
                    {
                        // We want to check the headers of the message before we download the full message
                        headers = client.GetMessageHeaders(i);

                        OpenPop.Mime.Header.RfcMailAddress from = headers.From;
                        emitente = headers.From.Address;
                        subject = headers.Subject;
                        data = headers.Date;

                        // Only want to download message if:
                        if (from.HasValidMailAddress)
                        {
                            // Download the full message
                            OpenPop.Mime.Message message = client.GetMessage(i);

                            // We know the message contains an attachment with the name "useful.pdf".
                            // We want to save this to a file with the same name
                            foreach (OpenPop.Mime.MessagePart attachment in message.FindAllAttachments())
                            {
                                String arquivoAnexo = attachment.FileName;

                                if (arquivoAnexo.Equals("(no name)"))
                                {
                                    arquivoAnexo = "anexo.eml";
                                }

                                // Save the raw bytes to a file
                                System.IO.File.WriteAllBytes("Anexos\\" + arquivoAnexo, attachment.Body);

                                new Descompactar("POP", IDCONFIG);
                                new Importar().ImportarArquivo("POP", IDCONFIG);
                            }

                            // Remove mensagem
                            client.DeleteMessage(i);
                        }
                    }
                    catch (Exception ex)
                    {
                        String temp = "Erro ao obter e-mail IDCONFIG="+ IDCONFIG +" DE="+ emitente +" ASSUNTO="+ subject +" DATA="+ data +" - Excessão: ";
                        Log.Salvar(temp + ex);
                    }
                }
            }
        }

        private void Receber2(int IDCONFIG, string hostname, int port, bool useSsl, string username, string password)
        {
            //string hostname = "pop3.bol.com.br";
            //int port = 995;
            //bool useSsl = true;
            //string username = "loginSemArroba";
            //string password = "senhaEmail";

            // The client disconnects from the server when being disposed
            using (OpenPop.Pop3.Pop3Client client = new OpenPop.Pop3.Pop3Client())
            {
                // Connect to the server
                client.Connect(hostname, port, useSsl);

                // Authenticate ourselves towards the server
                client.Authenticate(username, password);

                // Get the number of messages in the inbox
                int messageCount = client.GetMessageCount();

                // Messages are numbered in the interval: [1, messageCount]
                // Ergo: message numbers are 1-based.
                // Most servers give the latest message the highest number
                for (int i = messageCount; i > 0; i--)
                {
                    try 
	                {
                        System.IO.FileInfo fi = new System.IO.FileInfo("Anexos\\" + "mensagem.eml");
                        client.GetMessage(i).Save(fi);

                        new Descompactar("POP", IDCONFIG);
                        new Importar().ImportarArquivo("POP", IDCONFIG);

                        // Remove mensagem
                        client.DeleteMessage(i);
	                }
                    catch (Exception ex)
                    {
                        Log.Salvar("Erro ao obter e-mail IDCONFIG=" + IDCONFIG + " - Excessão: " + ex);
                    }
                }
            }
        }

        private void Receber3(int IDCONFIG, string hostname, int port, bool useSsl, string username, string password)
        {
            //string hostname = "pop3.bol.com.br";
            //int port = 995;
            //bool useSsl = true;
            //string username = "loginSemArroba";
            //string password = "senhaEmail";

            // The client disconnects from the server when being disposed
            using (OpenPop.Pop3.Pop3Client client = new OpenPop.Pop3.Pop3Client())
            {
                // Connect to the server
                client.Connect(hostname, port, useSsl);

                // Authenticate ourselves towards the server
                client.Authenticate(username, password);

                // Get the number of messages in the inbox
                int messageCount = client.GetMessageCount();

                // Messages are numbered in the interval: [1, messageCount]
                // Ergo: message numbers are 1-based.
                // Most servers give the latest message the highest number
                for (int i = messageCount; i > 0; i--)
                {
                    try
                    {
                        byte[] b = client.GetMessageAsBytes(i);
                        System.IO.File.WriteAllBytes(ValidateLib.Contexto.Session.DiretorioAnexos + "mensagem.eml", b);

                        new Descompactar("POP", IDCONFIG);
                        new Importar().ImportarArquivo("POP", IDCONFIG);

                        // Remove mensagem
                        client.DeleteMessage(i);
                    }
                    catch (Exception ex)
                    {
                        Log.Salvar("Erro ao obter e-mail IDCONFIG=" + IDCONFIG + " - Excessão: " + ex);
                    }
                }
            }
        }
    }
}
