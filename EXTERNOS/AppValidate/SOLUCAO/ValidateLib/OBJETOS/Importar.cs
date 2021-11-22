using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    public class Importar
    {
        public void ImportarArquivo(string Origem, int Idconfig)
        {
            try
            {
                String[] arquivos = System.IO.Directory.GetFiles(ValidateLib.Contexto.Session.DiretorioAnexos);
                for (int i = 0; i < arquivos.Length; i++)
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(arquivos[i]);

                    String extensao = fileInfo.Extension.ToUpper();

                    if (extensao.Equals(".XML"))
                    {
                        InboxParams inboxParams = new InboxParams();
                        inboxParams.IdInbox = 0;
                        inboxParams.CodOrigem = Origem;

                        if (Origem.Equals("POP"))
                            inboxParams.IdConfigPOP = Idconfig;

                        if (Origem.Equals("FTP"))
                            inboxParams.IdConfigFTP = Idconfig;

                        if (Origem.Equals("DIR"))
                            inboxParams.IdConfigDIR = Idconfig;

                        inboxParams.Data = DateTime.Now;
                        inboxParams.Arquivo = fileInfo.Name;
                        inboxParams.Texto = System.IO.File.ReadAllText(arquivos[i]);
                        inboxParams.CodStatus = "IMP";
                        inboxParams.DataLimite = null;
                        inboxParams.DataEmissao = null;
                        inboxParams.DataUltimoLog = null;

                        if (!inboxParams.ExisteConsultaNFE())
                            inboxParams.Save();

                        System.IO.File.Delete(arquivos[i]);
                    }
                    else
                    {
                        this.ExcluirArquivo(arquivos[i], false);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SalvarLog("ImportarArquivo", ex.Message);
            }
        }

        public void ExcluirArquivo(string arquivo, bool guardarLog)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(arquivo);
            String extensao = fileInfo.Extension.ToUpper();

            int n = 1;

            while (n != 0)
            {
                String novoCaminho = ValidateLib.Contexto.Session.DiretorioLixeira + n.ToString() + extensao;

                if (System.IO.File.Exists(novoCaminho))
                {
                    n++;
                }
                else
                {
                    n = 0;

                    if (guardarLog)
                    {
                        Log.Salvar("Arquivo: " + arquivo + " movido para lixeira como: " + novoCaminho);
                    }

                    System.IO.File.Move(arquivo, novoCaminho);
                }                        
            }
        }
    }
}
