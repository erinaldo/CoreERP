using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartCondicaoPgtoData: PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

//        public override string ReadView()
//        {
//            return @"SELECT *
//FROM VCONDICAOPGTO WHERE ";
//        }

        public decimal ValidaPercentualComposicao(int CodEmpresa, string CodCondicao)
        {
            try
            {
                string sSql = string.Empty;
                sSql = "SELECT SUM(PERCVALOR) PERCVALOR FROM VCONDICAOPGTOCOMPOSICAO WHERE CODEMPRESA = ? AND CODCONDICAO = ?";
                return Convert.ToDecimal(dbs.QueryValue(0, sSql, CodEmpresa, CodCondicao));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }        
        }
    }
}
