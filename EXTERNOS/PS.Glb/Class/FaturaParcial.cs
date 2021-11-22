using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PS.Glb.Class
{
    public class FaturaParcial
    {
        public FaturaParcial() { }
        /// <summary>
        /// Verifica a sistuação da operação
        /// </summary>
        /// <param name="codEmpresa">Código da Empresa</param>
        /// <param name="listaCodOper">Código da Operação</param>
        /// <returns>True - OK / False - Falso</returns>
        public bool VerificaSituacao(int codEmpresa, string codOper)
        {
            try
            {
                string retorno = string.Empty;
                retorno = AppLib.Context.poolConnection.Get("Start").ExecGetField("1", @"SELECT CODSTATUS FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codOper }).ToString();
                if (string.IsNullOrEmpty(retorno))
                {
                    return false;
                }
                else if (!retorno.Equals("0") && !retorno.Equals("5"))
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
       
    }
}
