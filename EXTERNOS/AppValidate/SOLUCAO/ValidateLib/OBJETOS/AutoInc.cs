using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    public class AutoInc
    {
        private AppLib.Data.Connection _conn;

        public int GetNewAutoInc(string CodAutoInc)
        {
            try
            {
                string sSql = string.Empty;
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();

                int ValAutoInc = 0;
                sSql = @"SELECT VALAUTOINC FROM ZAUTOINC WHERE CODAUTOINC = ?";
                if (_conn.ExecHasRows(sSql, CodAutoInc))
                {
                    sSql = @"SELECT VALAUTOINC FROM ZAUTOINC WHERE CODAUTOINC = ?";
                    ValAutoInc = Convert.ToInt32(_conn.ExecGetField(0, sSql, CodAutoInc));
                    ValAutoInc++;
                    sSql = @"UPDATE ZAUTOINC SET VALAUTOINC = ? WHERE CODAUTOINC = ?";
                    _conn.ExecQuery(sSql, ValAutoInc, CodAutoInc);
                }
                else
                {
                    sSql = @"INSERT INTO ZAUTOINC (CODAUTOINC, VALAUTOINC) VALUES (?, ?)";
                    _conn.ExecQuery(sSql, CodAutoInc, 1);
                    ValAutoInc = 1;
                }

                return ValAutoInc;

            }
            catch (Exception ex)
            {
                ValidateLib.Log.SalvarLog("OutBoxParams.Save", ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
