using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartFiscalImpData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODIMPRESSORA,
DESCRICAO,
CONVERT(BIT, ATIVO) ATIVO,
MARCA,
MODELO
FROM VFISCALIMP WHERE ";
        }
    }
}
