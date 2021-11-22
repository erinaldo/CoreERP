using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartNaturezaData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODNATUREZA,
DESCRICAO, 
CONVERT(BIT, ATIVO) ATIVO, 
CODMENSAGEM, 
TIPENTSAI, 
DENTRODOESTADO,
CLASSVENDA2,
IDREGRAICMS, 
IDREGRAIPI,
CONTRIBUINTEICMS,
CONVERT(BIT, ULTIMONIVEL) ULTIMONIVEL,
DESCRICAOINTERNA
FROM VNATUREZA WHERE ";
        }
    }
}
