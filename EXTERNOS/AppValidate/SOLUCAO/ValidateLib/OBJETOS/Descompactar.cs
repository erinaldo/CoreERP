using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    public class Descompactar
    {
        public Descompactar(String CODORIGEM, int IDCONFIG)
        {
            String[] Arquivos = System.IO.Directory.GetFiles(ValidateLib.Contexto.Session.DiretorioAnexos);

            for (int i = 0; i < Arquivos.Length; i++)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(Arquivos[i]);

                if (!System.IO.Directory.Exists(fi.Directory.FullName + "\\temp"))
                {
                    System.IO.Directory.CreateDirectory(fi.Directory.FullName + "\\temp");
                }

                /*
                Unpacking only: ARJ, CAB, CHM, CPIO, CramFS, DEB, DMG, FAT, HFS, ISO, LZH, LZMA, MBR, MSI, NSIS, NTFS, RAR, RPM, SquashFS, UDF, VHD, WIM, XAR and Z.
                For ZIP and GZIP formats, 7-Zip provides a compression ratio that is 2-10 % better than the ratio provided by PKZip and WinZip
                Strong AES-256 encryption in 7z and ZIP formats 
                */

                if (fi.Extension.ToUpper().Equals(".ZIP") ||
                    fi.Extension.ToUpper().Equals(".RAR") ||
                    fi.Extension.ToUpper().Equals(".7Z") ||
                    fi.Extension.ToUpper().Equals(".TAR") ||
                    fi.Extension.ToUpper().Equals(".GZ") ||
                    fi.Extension.ToUpper().Equals(".BZ2") ||
                    fi.Extension.ToUpper().Equals(".ARJ") ||
                    fi.Extension.ToUpper().Equals(".CAB"))
                {
                    try
                    {
                        SevenZSharp.Decoders.ShellDecoder d = new SevenZSharp.Decoders.ShellDecoder(new SevenZSharp.Engines.ShellEngine());
                        d.DecodeSingleFile(fi.FullName, fi.Directory.FullName + "\\temp");
                    }
                    catch (Exception ex)
                    {
                        Log.SalvarLog("Descompactar Etapa 1", "Arquivo: " + fi.FullName + " Mensagem " + ex.Message);
                    }

                    try
                    {
                        System.IO.File.Delete(fi.FullName);
                    }
                    catch (Exception ex)
                    {
                        Log.SalvarLog("Descompactar Etapa 2", ex.Message);
                    }

                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(fi.Directory.FullName + "\\temp");
                    VarrerPastas(CODORIGEM, IDCONFIG, di.FullName, fi.Directory.FullName);


                    try
                    {
                        System.IO.Directory.Delete(fi.Directory.FullName + "\\temp", true);
                    }
                    catch (Exception ex)
                    {
                        Log.SalvarLog("Descompactar Etapa 3", ex.Message);
                    }

                }

                /* Extrai arquivos EML */

                if (fi.Extension.ToUpper().Equals(".EML") ||
                    fi.Extension.ToUpper().Equals(".MSG"))
                {
                    Eml eml = new Eml();

                    if (eml.Extract3(fi.FullName, fi.Directory.FullName + "\\temp"))
                    {
                        System.IO.File.Delete(fi.FullName);

                        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(fi.Directory.FullName + "\\temp");
                        VarrerPastas(CODORIGEM, IDCONFIG, di.FullName, fi.Directory.FullName);

                        try
                        {
                            System.IO.Directory.Delete(fi.Directory.FullName + "\\temp", true);
                        }
                        catch (Exception) { }
                    }                    
                }
            }
        }

        public void VarrerPastas(String CODORIGEM, int IDCONFIG, String Diretorio, String PastaAnexos)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Diretorio);
            System.IO.DirectoryInfo[] dirs = di.GetDirectories();

            for (int i = 0; i < dirs.Length; i++)
            {
                try
                {
                    VarrerPastas(CODORIGEM, IDCONFIG, dirs[i].FullName, PastaAnexos);
                }
                catch (Exception ex)
                {
                    Log.SalvarLog("VarrerPastas Etapa 1", ex.Message);
                }

                System.IO.FileInfo[] fi = dirs[i].GetFiles();
                MoverArquivos(CODORIGEM, IDCONFIG, fi, PastaAnexos);
            }

            System.IO.FileInfo[] fi2 = di.GetFiles();
            MoverArquivos(CODORIGEM, IDCONFIG, fi2, PastaAnexos);
        }

        public void MoverArquivos(String CODORIGEM, int IDCONFIG, System.IO.FileInfo[] Arquivos, String PastaAnexos)
        {
            for (int i = 0; i < Arquivos.Length; i++)
            {
                try
                {
                    System.IO.File.Move(Arquivos[i].FullName, PastaAnexos + "\\" + Arquivos[i].Name);
                }
                catch (Exception ex)
                {
                    Log.SalvarLog("MoverArquivos Etapa 1", ex.Message);
                }

                new Importar().ImportarArquivo(CODORIGEM, IDCONFIG);
            }
        }

    }
}
