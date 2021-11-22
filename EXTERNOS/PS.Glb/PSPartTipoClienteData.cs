using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartTipoClienteData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT IDTIPOCLIENTE, DESCRICAO FROM VTIPOCLIENTE WHERE ";
        }
    }
}
