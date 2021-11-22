using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartTipOperMensagemData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODTIPOPER,
CODMENSAGEM,
(SELECT DESCRICAO FROM VOPERMENSAGEM WHERE CODEMPRESA = GOPERMENSAGEM.CODEMPRESA AND CODMENSAGEM = GOPERMENSAGEM.CODMENSAGEM) DESCRICAO
FROM GOPERMENSAGEM WHERE ";
        }
    }
}
