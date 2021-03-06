using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartAcessoMenuData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT GACESSOMENU.CODEMPRESA,
GACESSOMENU.CODPERFIL,
GACESSOMENU.CODPSPART,
GPSPART.DESCRICAO,
CONVERT(BIT, GACESSOMENU.ACESSO) ACESSO,
CONVERT(BIT, GACESSOMENU.PERMITEINCLUIR) PERMITEINCLUIR,
CONVERT(BIT, GACESSOMENU.PERMITEALTERAR) PERMITEALTERAR,
CONVERT(BIT, GACESSOMENU.PERMITEEXCLUIR) PERMITEEXCLUIR
 FROM GACESSOMENU INNER JOIN GPSPART ON GACESSOMENU.CODPSPART = GPSPART.CODPSPART  WHERE ";
        }
    }
}
