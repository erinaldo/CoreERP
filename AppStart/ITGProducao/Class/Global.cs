using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITGProducao.Class
{
    public class Global
    {
        public enum AlteraReservaOP
        {
            Automatico,
            Estatico
        }

        public static int CodFilial;

        public void EnableTab(TabPage tab, bool enable) 
        {
            ((Control)tab).Enabled = enable;
        }

        public DataTable VerificaParametroDatatable(string Nome)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT PPARAM.CODEMPRESA, PPARAM.CODFILIAL, PPARAM.NOME, PPARAM.VALOR,PPARAM.OBSERVACAO FROM  PPARAM WHERE  PPARAM.CODEMPRESA = ? AND  PPARAM.CODFILIAL = ? AND  PPARAM.NOME = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, Nome });
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }

            return dt;
        }

        public string VerificaParametroString(string Nome)
        {
            string _valor = "";

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT PPARAM.CODEMPRESA, PPARAM.CODFILIAL, PPARAM.NOME, PPARAM.VALOR,PPARAM.OBSERVACAO FROM  PPARAM WHERE  PPARAM.CODEMPRESA = ? AND  PPARAM.CODFILIAL = ? AND  PPARAM.NOME = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, Nome });
            if (dt.Rows.Count > 0)
            {
                _valor = dt.Rows[0]["VALOR"].ToString(); 
            }
            else
            {
                _valor = "";
            }

            return _valor;
        }
    }
}
