using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartTipDocData: PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODTIPDOC,
NOME,
CONVERT(BIT, ATIVO) ATIVO,
USANUMEROSEQ,
ULTIMONUMERO,
PAGREC,
CLASSIFICACAO
FROM FTIPDOC WHERE ";
        }
    }
}
