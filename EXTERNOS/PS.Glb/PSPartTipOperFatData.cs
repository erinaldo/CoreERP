using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartTipOperFatData: PS.Lib.WinForms.PSPartData
    {
        PS.Lib.Global gb = new PS.Lib.Global();

        public override void ValidateRecord(List<PS.Lib.DataField> objArr)
        {
            base.ValidateRecord(objArr);

            string operA = "";
            string operB = "";

            PS.Lib.DataField dataField = new PS.Lib.DataField();

            dataField = gb.RetornaDataFieldByCampo(objArr, "CODTIPOPER");
            operA = dataField.Valor.ToString();

            dataField = gb.RetornaDataFieldByCampo(objArr, "CODTIPOPERFAT");
            operB = dataField.Valor.ToString();

            if (operA == operB)
            {
                throw new Exception("Tipo de Operação e Tipo de Operação Faturamento devem ser diferentes.");
            }
        }
    }
}
