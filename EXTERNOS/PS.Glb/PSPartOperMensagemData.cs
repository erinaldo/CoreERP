using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartOperMensagemData: PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODMENSAGEM,
MENSAGEM,
CONVERT(BIT, ATIVO) ATIVO,
DESCRICAO,
CODFORMULAMENSAGEM
FROM VOPERMENSAGEM WHERE ";
        }
    }
}
