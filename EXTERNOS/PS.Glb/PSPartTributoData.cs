using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartTributoData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODTRIBUTO,
DESCRICAO,
CONVERT(BIT, ATIVO) ATIVO,
ALIQUOTA,
ALIQUOTAEM,
CODTIPOTRIBUTO
FROM VTRIBUTO WHERE ";
        }
    }
}
