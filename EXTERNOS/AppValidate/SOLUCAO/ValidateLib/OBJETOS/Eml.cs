using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    public class Eml
    {
        public Boolean Extract(String Arquivo, String Pasta)
        {
            try
            {
                byte[] b = System.IO.File.ReadAllBytes(Arquivo);

                // Download the full message
                OpenPop.Mime.Message message = new OpenPop.Mime.Message(b, false);

                // We know the message contains an attachment with the name "useful.pdf".
                // We want to save this to a file with the same name
                foreach (OpenPop.Mime.MessagePart attachment in message.FindAllAttachments())
                {
                    System.IO.Directory.CreateDirectory(Pasta);

                    // Save the raw bytes to a file
                    System.IO.File.WriteAllBytes(Pasta + "\\" + attachment.FileName, attachment.Body);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Salvar("Erro ao ler o e-mail pelo seguinte motivo (em seguida este arquivo será enviado para lixeira): " + ex);
                new Importar().ExcluirArquivo(Arquivo, true);
                
            }

            return false;
        }

        public Boolean Extract3(String Arquivo, String Pasta)
        {
            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(Arquivo);
                OpenPop.Mime.Message m = OpenPop.Mime.Message.Load(fi);
                List<OpenPop.Mime.MessagePart> anexos = m.FindAllAttachments();

                for (int i = 0; i < anexos.Count; i++)
                {
                    OpenPop.Mime.MessagePart mp = anexos[i];
                    System.IO.Directory.CreateDirectory(Pasta);
                    System.IO.FileInfo fiAnexo = new System.IO.FileInfo(Pasta + "\\" + mp.FileName);
                    mp.Save(fiAnexo);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Salvar("Erro ao ler o e-mail pelo seguinte motivo (em seguida este arquivo será enviado para lixeira): " + ex);
                new Importar().ExcluirArquivo(Arquivo, true);
            }

            return false;
        }
    }
}
