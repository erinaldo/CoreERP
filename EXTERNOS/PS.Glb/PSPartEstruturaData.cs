using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartEstruturaData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODESTRUTURA,
CODPRODUTO,
CONVERT(BIT, ATIVO) ATIVO,
NOME
FROM PESTRUTURA WHERE ";
        }
    }
}
