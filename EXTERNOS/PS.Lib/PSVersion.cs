using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class PSVersion
    {
        private Data.DBS dbs = new Data.DBS();
        private Controle control = new Controle();

        private const string CONST_VERSAO_BASE = "005.000.000";

        public PSVersion()
        {
            Contexto.Session.VersaoApp = CONST_VERSAO_BASE;
        }

        public bool VerificaVersaoBase()
        {
            string sSql = "SELECT * FROM GPARAMETROS";
            string vBase = string.Empty;
            string cBase = string.Empty;

            System.Data.DataTable dt = dbs.QuerySelect(sSql);

            if (dt.Rows.Count > 0)
            {
                vBase = dt.Rows[0]["VERSAOBANCO"].ToString();
                cBase = dt.Rows[0]["VERSAOCONTROLE"].ToString();

                if (control.ValidaVersao(CONST_VERSAO_BASE, vBase, cBase))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool VerificaVersaoApp()
        {
            return true;
        }
    }
}
