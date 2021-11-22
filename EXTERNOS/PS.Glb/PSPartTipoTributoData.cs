using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartTipoTributoData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODTIPOTRIBUTO,
DESCRICAO,
CONVERT(BIT, ATIVO) ATIVO,
ABRANGENCIA,
PERIODICIDADE,
DTINIVIGENCIA,
DTFIMVIGENCIA,
TIPOALIQUOTA
FROM VTIPOTRIBUTO WHERE ";
        }
    }
}
