using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartRegraIPIData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
IDREGRA,
DESCRICAO,
CONVERT(BIT, ATIVO) ATIVO,
CODCSTENT,
CODCSTSAI,
CENQ,
TIPOTRIBUTACAO
FROM VREGRAIPI WHERE ";
        }
    }
}
