using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartTipoTransporteData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODTIPOTRANSPORTE, CODEMPRESA, CODTRANSPORTADORA, DESCRICAO FROM VTIPOTRANSPORTE WHERE ";
        }
    }
}
