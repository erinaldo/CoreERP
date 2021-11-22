using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartProdutoComData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODPRODUTO,
CODPRODCOM,
(SELECT NOME FROM VPRODUTO WHERE CODEMPRESA = VPRODUTOCOM.CODEMPRESA AND CODPRODUTO = VPRODUTOCOM.CODPRODCOM) CNOME,
QUANTIDADE
FROM VPRODUTOCOM WHERE ";
        }
    }
}
