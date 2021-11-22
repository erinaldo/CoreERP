using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using SevenZipLib;

namespace PS.Lib
{
    public class Zip
    {
        private FileInfo[] Files { get; set; }

        public Zip(FileInfo[] files)
        {
            this.Files = files;    
        }

        public void Descompactar(string Path)
        {
            try
            {
                foreach (FileInfo fileToDecompress in this.Files)
                {
                    SevenZipArchive zip = new SevenZipArchive(fileToDecompress.FullName);
                    zip.ExtractAll(string.Concat(Path,"\\"), ExtractOptions.OverwriteExistingFiles);
                    zip.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
