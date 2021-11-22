using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{ 
    public class PSPartAgenciaData: PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODBANCO,
CODAGENCIA,
DVAGENCIA,
NOME,
CEP,
CODTIPORUA,
RUA,
NUMERO,
COMPLEMENTO,
CODTIPOBAIRRO,
BAIRRO,
CODCIDADE,
(SELECT NOME FROM GCIDADE WHERE CODETD = GAGENCIA.CODETD AND CODCIDADE = GAGENCIA.CODCIDADE) CCIDADE,
CODETD,
CODPAIS
FROM GAGENCIA WHERE ";
        }
    }
}
