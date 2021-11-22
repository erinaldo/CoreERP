using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartFabricanteData: PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODFABRICANTE,
NOME,
NOMEFANTASIA,
CGCCPF,
CEP,
CODTIPORUA,
RUA,
NUMERO,
COMPLEMENTO,
CODTIPOBAIRRO,
BAIRRO,
CODCIDADE,
(SELECT NOME FROM GCIDADE WHERE CODETD = VFABRICANTE.CODETD AND CODCIDADE = VFABRICANTE.CODCIDADE) CCIDADE,
CODETD,
CODPAIS
FROM VFABRICANTE WHERE ";
        }
    }
}
