using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PS.Glb
{
    public class PSPartParamVarejoData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public DataTable RetornaTabelaPreco(int CodEmpresa)
        {
            try
            {
                string sSql = @"SELECT TABELA, NOME FROM (
SELECT 
CODEMPRESA, 0 TABELA, 'Nenhum' NOME FROM VPARAMETROS
UNION ALL
SELECT CODEMPRESA, 1 TABELA, PRDTEXTOPRECO1 NOME FROM VPARAMETROS
UNION ALL
SELECT CODEMPRESA, 2 TABELA, PRDTEXTOPRECO2 NOME FROM VPARAMETROS
UNION ALL
SELECT CODEMPRESA, 3 TABELA, PRDTEXTOPRECO3 NOME FROM VPARAMETROS
UNION ALL
SELECT CODEMPRESA, 4 TABELA, PRDTEXTOPRECO4 NOME FROM VPARAMETROS
UNION ALL
SELECT CODEMPRESA, 5 TABELA, PRDTEXTOPRECO5 NOME FROM VPARAMETROS
UNION ALL 
SELECT CODEMPRESA, 6 TABELA, 'Tabela de Preço por Cliente' NOME FROM GEMPRESA
UNION ALL 
SELECT CODEMPRESA, 7 TABELA, 'Custo Médio' NOME FROM VPARAMETROS
) X
WHERE CODEMPRESA = ?
AND NOT(NOME IS NULL)";

                return dbs.QuerySelect(sSql, CodEmpresa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);            
            }
        }
    }
}
