using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartCodigoBarrasData : PS.Lib.WinForms.PSPartData
    {
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODPRODUTO,
CODIGOBARRAS,
CONVERT(BIT, ATIVO) ATIVO
FROM VPRODUTOCODIGO WHERE ";
        }

        public override void ValidateRecord(List<PS.Lib.DataField> objArr)
        {
            base.ValidateRecord(objArr);

            PS.Lib.DataField dataField = new PS.Lib.DataField();
            dataField = gb.RetornaDataFieldByCampo(objArr, "ATIVO");

            if (int.Parse(dataField.Valor.ToString()) == 1)
            {
                dataField = gb.RetornaDataFieldByCampo(objArr, "CODPRODUTO");

                if (dbs.QueryFind("SELECT CODIGOBARRAS FROM VPRODUTOCODIGO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND ATIVO = 1", PS.Lib.Contexto.Session.Empresa.CodEmpresa, dataField.Valor))
                {
                    throw new Exception("Produto ja possui um Código de Barras ativo.");
                }
            }
        }        
    }
}
