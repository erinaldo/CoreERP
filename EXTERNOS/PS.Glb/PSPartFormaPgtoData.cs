using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartFormaPgtoData: PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODFORMA,
NOME,
CONVERT(BIT, ATIVO) ATIVO,
TIPO,
TIPOTRANSACAO,
CODREDECARTAO,
TAXAADM
FROM VFORMAPGTO WHERE ";
        }
    }
}
