using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartAcessoRelatorioData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"
SELECT GREPORTPERFIL.*,
( SELECT NOME FROM GPERFIL WHERE CODPERFIL = GREPORTPERFIL.CODPERFIL ) PERFIL
FROM GREPORTPERFIL
WHERE ";
        }
    }
}
