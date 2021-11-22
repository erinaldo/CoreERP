using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidateService.OBJETOS
{
   
        public static class Log
        {
            public static void Escrever(String texto)
            {
                //String arquivoLog = @"C:\TEMP\log.txt";

                //if (!System.IO.File.Exists(arquivoLog))
                //{
                //    System.IO.File.WriteAllText(arquivoLog, "");
                //}

                //System.IO.File.AppendAllText(arquivoLog, DateTime.Now.ToString() + ": " + texto + "\r\n");
            }

            public static void Limpar()
            {
                System.IO.File.Delete(@"C:\TEMP\log.txt");
            }
        }
    }
