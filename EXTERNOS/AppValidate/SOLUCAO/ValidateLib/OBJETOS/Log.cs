using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    public class Log
    {
        public static void Salvar(string Mensagem)
        {
            String arquivo = "log.txt";

            if (!(System.IO.File.Exists(arquivo)))
            {
                System.IO.File.WriteAllText(arquivo, "");
            }

            String Separador = "------------------------------------------------------------------------------------------------------------------------";
            String DataHora = DateTime.Now.ToString();
            String Texto = Separador + "\r\n" + DataHora + " -> " + Mensagem + "\r\n";
            String TextoBD = DataHora + " -> " + Mensagem + "\r\n";

            try
            {
                System.IO.File.AppendAllText(arquivo, Texto);
                SalvarLog(Mensagem);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Erro ao gerar log: " + ex.Message);
            }
        }

        public static void Salvar(int idinbox, Exception exception)
        {
            if (idinbox == 0)
            {
                string err = (exception.InnerException != null ? exception.InnerException.Message : exception.Message);
                Salvar(err);
            }
            else
            {
                string err = (exception.InnerException != null ? exception.InnerException.Message : exception.Message);
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                string sSql = "UPDATE ZINBOX SET LOG = ?, DATAULTIMOLOG = ? WHERE IDINBOX = ?";
                int temp = conn.ExecTransaction(sSql, new Object[] { err, DateTime.Now, idinbox });
            }
        }

        public static void Salvar(Exception exception)
        {
            Salvar(exception.Message + "\r\n" + exception.StackTrace);
        }

        public static void SalvarLog(string Mensagem)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            string sSql = "INSERT INTO ZLOG (ROTINA, DATA, MENSAGEM) VALUES(?, ?, ?)";
            int temp = conn.ExecTransaction(sSql, new Object[] { null, conn.GetDateTime(), Mensagem});
        }

        public static void SalvarLog(string Rotina, string Mensagem)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            string sSql = "INSERT INTO ZLOG (ROTINA, DATA, MENSAGEM) VALUES(?, ?, ?)";
            int temp = conn.ExecTransaction(sSql, new Object[] { Rotina, conn.GetDateTime(), Mensagem });
        }

        public static void Excluir()
        {
            System.IO.File.Delete("log.txt");
            Salvar("Excluido o log");
        }
    }
}
