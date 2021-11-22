using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartContatoCliForData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODCLIFOR,
CODCONTATO,
NOME,
EMAIL,
TELEFONE,
CPF,
DATANASCIMENTO,
NUMERORG,
OREMISSOR
FROM VCLIFORCONTATO WHERE ";
        }
    }
}
