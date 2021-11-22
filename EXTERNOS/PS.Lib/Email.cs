using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LumiSoft.Net;
using LumiSoft.Net.POP3.Client;
using LumiSoft.Net.IMAP.Client;
using LumiSoft.Net.IMAP;
using LumiSoft.Net.Mail;
using LumiSoft.Net.MIME;

namespace PS.Lib
{
    public class Email
    {
        public string Servidor { get; set; }
        public int Porta { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public bool SSL { get; set; }
        public bool APOP { get; set; }

        private POP3_Client pop;

        public Email()
        { 
        
        }

        public Email(string servidor, int porta, string usuario, string senha, bool ssl, bool apop)
        {
            this.Servidor = servidor;
            this.Porta = porta;
            this.Usuario = usuario;
            this.Senha = senha;
            this.SSL = ssl;
            this.APOP = apop;

            pop = new POP3_Client();
        }

        public bool Desconectar()
        {
            bool flag = false;
            try
            {
                if (pop.IsConnected)
                    pop.Disconnect();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return flag;        
        }

        public bool Conectar()
        {
            bool flag = false;
            try
            {
                pop.Connect(this.Servidor, this.Porta, this.SSL);
                flag = pop.IsConnected;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return flag;        
        }

        public bool Autenticar()
        {
            bool flag = false;
            try
            {
                if (pop.IsConnected)
                {
                    pop.Authenticate(this.Usuario, this.Senha, this.APOP);
                    flag = pop.IsAuthenticated;
                }
                else
                    flag = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return flag;        
        }

        public bool TestarConexao()
        {
            bool flag = false;
            try
            {
                if (this.Conectar())
                    return this.Autenticar();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message); 
            }
            finally
            {
                pop.Disconnect();
                pop.Dispose();
            }

            return flag;
        }

        /// <summary>
        /// Método exclusivo do EFIS
        /// </summary>
        /// <param name="dirEmail">Diretório Email</param>
        /// <param name="dirAnexo">Diretório Anexo</param>
        /// <param name="dirPool">Diretório Pool</param>
        /// <returns></returns>
        public bool BaixarEmail(string dirEmail, string dirAnexo, string dirPool)
        {
            bool flag = false;
            try
            {
                if (pop.IsConnected)
                {
                    if (pop.IsAuthenticated)
                    {
                        foreach (POP3_ClientMessage message in pop.Messages)
                        {
                            Mail_Message anexo = Mail_Message.ParseFromByte(message.MessageToByte());
                            BaixarAnexo(anexo.Attachments, dirEmail, dirAnexo, dirPool);
                            message.MarkForDeletion();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                pop.Disconnect();
                pop.Dispose();
            }

            return flag;       
        }

        /// <summary>
        /// Método exclusivo do EFIS
        /// </summary>
        /// <param name="anexo">Anexo</param>
        /// <param name="dirEmail">Diretório Email</param>
        /// <param name="dirAnexo">Diretório Anexo</param>
        /// <param name="dirPool">Diretório Pool</param>
        /// <returns></returns>
        private bool BaixarAnexo(MIME_Entity[] anexo, string dirEmail, string dirAnexo, string dirPool)
        {
            PS.Lib.Utils util = new PS.Lib.Utils();

            bool flag = false;
            try
            {
                if (pop.IsConnected)
                    if (pop.IsAuthenticated)
                    {
                        foreach (MIME_Entity entity in anexo)
                        {
                            //string Folder = DateTime.Now.ToString("yyyyMMddHHmmssms");

                            if (entity.ContentDisposition != null && entity.ContentDisposition.Param_FileName != null)
                            {
                                entity.ContentDisposition.Param_FileName = entity.ContentDisposition.Param_FileName.Replace("/", "_");
                                entity.ContentDisposition.Param_FileName = entity.ContentDisposition.Param_FileName.Replace(@"\", "_");
                                entity.ContentDisposition.Param_FileName = entity.ContentDisposition.Param_FileName.Replace(":", "_");

                                if (entity.Body.GetType() == typeof(MIME_b_SinglepartBase) || entity.Body.GetType() == typeof(MIME_b_Text) || entity.Body.GetType() == typeof(MIME_b_Application))
                                {
                                    File.WriteAllBytes(string.Concat(dirEmail, "\\",entity.ContentDisposition.Param_FileName), ((MIME_b_SinglepartBase)entity.Body).Data);
                                }

                                if (entity.Body.GetType() == typeof(MIME_b_MessageRfc822))
                                {
                                    BaixarAnexo(((MIME_b_MessageRfc822)entity.Body).Message.Attachments, dirEmail, dirAnexo, dirPool);
                                }

                                if (entity.Body.GetType() == typeof(MIME_b_Unknown))
                                {
                                    File.WriteAllBytes(string.Concat(dirEmail, "\\", ((MIME_b_Unknown)entity.Body).Entity.ContentDisposition.Param_FileName), ((MIME_b_SinglepartBase)entity.Body).Data);
                                }
                            }
                            else
                            {
                                if (entity.ContentType.Param_Name != null)
                                {
                                    entity.ContentType.Param_Name = entity.ContentType.Param_Name.Replace("/", "_");
                                    entity.ContentType.Param_Name = entity.ContentType.Param_Name.Replace(@"\", "_");
                                    entity.ContentType.Param_Name = entity.ContentType.Param_Name.Replace(":", "_");

                                    if (entity.Body.GetType() == typeof(MIME_b_SinglepartBase) || entity.Body.GetType() == typeof(MIME_b_Text) || entity.Body.GetType() == typeof(MIME_b_Application))
                                    {
                                        File.WriteAllBytes(string.Concat(dirEmail, "\\", entity.ContentType.Param_Name), ((MIME_b_SinglepartBase)entity.Body).Data);
                                    }

                                    if (entity.Body.GetType() == typeof(MIME_b_MessageRfc822))
                                    {
                                        BaixarAnexo(((MIME_b_MessageRfc822)entity.Body).Message.Attachments, dirEmail, dirAnexo, dirPool);
                                    }
                                }
                            }

                            AnalisaArquivos(dirEmail, dirAnexo, dirPool);
                        }
                    }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return flag;
        }

        /// <summary>
        /// Método exclusivo do EFIS 
        /// </summary>
        /// <param name="sDir">Diretório</param>
        private void Descompactar(string sDir)
        {
            try
            {
                DirectoryInfo directorySelected = new DirectoryInfo(sDir);

                string[] ext = new string[] { "*.zip", "*.rar", "*.7z", "*.gzip", "*.tar", "*.arj" };

                foreach (string extensao in ext)
                {
                    PS.Lib.Zip zip = new PS.Lib.Zip(directorySelected.GetFiles(extensao));
                    zip.Descompactar(sDir);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Método exclusivo do EFIS
        /// </summary>
        /// <param name="dirEmail">Diretório Email</param>
        /// <param name="dirAnexo">Diretório Anexo</param>
        private void AnalisaArquivos(string dirEmail, string dirAnexo, string dirPool)
        {
            try
            {
                Descompactar(dirEmail);
                BuscaArquivo(dirEmail, dirAnexo, dirPool);
                ApagaDiretorio(dirEmail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Método exclusivo do EFIS
        /// </summary>
        /// <param name="sDir">Diretório</param>
        private void ApagaDiretorio(string sDir)
        {
            try
            {
                foreach (string dir in Directory.GetDirectories(sDir))
                {
                    System.IO.Directory.Delete(dir, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }                    
        }

        /// <summary>
        /// Método exclusivo do EFIS
        /// </summary>
        /// <param name="sDirBusca">Diretório Email</param>
        /// <param name="sDirXML">Diretório XML</param>
        /// <param name="sDirPool">Diretório Pool</param>
        private void BuscaArquivo(string sDirBusca, string sDirXML, string sDirPool)
        {
            foreach (string file in Directory.GetFiles(sDirBusca))
            {
                FileInfo f = new FileInfo(file);
                if (f.Extension.ToLower() == ".xml")
                {
                    //mover para a pasta destinada ao xml
                    System.IO.File.Move(f.FullName, string.Concat(sDirXML, "\\", f.Name));
                }
                else
                {
                    if (File.Exists(string.Concat(sDirPool, "\\", f.Name)))
                        File.Delete(string.Concat(sDirPool, "\\", f.Name));

                    System.IO.File.Move(f.FullName, string.Concat(sDirPool, "\\", f.Name));
                }
            }
            BuscaDiretorio(sDirBusca, sDirXML, sDirPool);
        }

        /// <summary>
        /// Método exclusivo do EFIS
        /// </summary>
        /// <param name="sDirBusca">Diretório Busca</param>
        /// <param name="sDirXML">Diretório XML</param>
        /// <param name="sDirPool">Diretório Pool</param>
        private void BuscaDiretorio(string sDirBusca, string sDirXML, string sDirPool)
        {
            try
            {
                foreach (string dir in Directory.GetDirectories(sDirBusca))
                {
                    Descompactar(dir);
                    BuscaArquivo(dir, sDirXML, sDirPool);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
