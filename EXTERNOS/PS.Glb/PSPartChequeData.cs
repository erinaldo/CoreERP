using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartChequeData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODCHEQUE,
CODBANCO,
CODAGENCIA,
NUMCONTA,
NUMERO,
VALOR,
DATA,
DATABOA,
CONVERT(BIT, VINCULADO) VINCULADO,
CODIMAGEM
FROM FCHEQUE WHERE ";
        }
    }
}
