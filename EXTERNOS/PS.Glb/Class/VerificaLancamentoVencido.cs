using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PS.Glb.Class
{
    public class VerificaLancamentoVencido
    {
        public VerificaLancamentoVencido()
        {

        }
        private string sql = string.Empty;
        DataTable dt = new DataTable();

        public bool verificaParametroVencimento(string codTipOper, string codCliFor)
        {
            sql = @"SELECT QTDDIASVENCIDOS, VERIFICAVENCIMENTO FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?";
            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { codTipOper, AppLib.Context.Empresa });
            if (dt.Rows.Count > 0)
            {
                return verificaLancamentoVencido(codCliFor, codTipOper, Convert.ToInt32(dt.Rows[0]["QTDDIASVENCIDOS"]));
            }
            return false;
        }
        private bool verificaLancamentoVencido(string codCliFor, string codTipOper, int qtdDias)
        {
            try
            {
                sql = @"SELECT CODLANCA, DATAVENCIMENTO FROM FLANCA WHERE CODCLIFOR = ? AND DATAVENCIMENTO < ? AND DATABAIXA IS NULL AND CODEMPRESA = ?";
                dt = new DataTable();
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { codCliFor, AppLib.Context.poolConnection.Get("Start").GetDateTime(), AppLib.Context.Empresa });
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int a = DateTime.Compare(AppLib.Context.poolConnection.Get("Start").GetDateTime(), Convert.ToDateTime(dt.Rows[i]["DATAVENCIMENTO"]).AddDays(3));
                    if (a > 0 )
                    {
                        return false;
                    }
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
