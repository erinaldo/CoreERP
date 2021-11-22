using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartEmpresaData: PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Lib.Global gb = new PS.Lib.Global();

        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
NOMEFANTASIA,
NOME,
CGCCPF,
INSCRICAOESTADUAL,
TELEFONE,
EMAIL,
CODTIPORUA,
RUA,
NUMERO,
COMPLEMENTO,
CODTIPOBAIRRO,
BAIRRO,
CODCIDADE,
(SELECT NOME FROM GCIDADE WHERE CODETD = GEMPRESA.CODETD AND CODCIDADE = GEMPRESA.CODCIDADE) CCIDADE,
CEP,
CODETD,
CODPAIS,
CODCONTROLE,
CODIMAGEM,
CODCHAVE1,
CODCHAVE2
FROM GEMPRESA WHERE ";
        }
    }
}
