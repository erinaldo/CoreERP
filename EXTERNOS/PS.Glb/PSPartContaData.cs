using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartContaData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODCONTA,
CONVERT(BIT, ATIVO) ATIVO,
DESCRICAO,
DTBASE,
SALDODATABASE,
CODBANCO,
CODAGENCIA,
NUMCONTA, 
RELCONSOLIDADO, 
ORDEMCONSOLIDADO
FROM FCONTA WHERE ";
        }
    }
}
