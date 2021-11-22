using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartLocalEstoqueData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODFILIAL,
CODLOCAL,
NOME,
DESCRIÇÃO,
CODTIPLOC,
CEP,
CODTIPORUA,
RUA,
NUMERO,
COMPLEMENTO,
CODTIPOBAIRRO,
BAIRRO,
CODCIDADE,
(SELECT NOME FROM GCIDADE WHERE CODETD = VLOCALESTOQUE.CODETD AND CODCIDADE = VLOCALESTOQUE.CODCIDADE) CCIDADE,
CODETD,
CODPAIS,
CONVERT(BIT, ATIVO) ATIVO
FROM VLOCALESTOQUE WHERE ";
        }
    }
}
