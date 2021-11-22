using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class Session
    {
        public Empresa Empresa { get; set; }
        public string CodUsuario { get; set; }
        public string CodModulo { get; set; }
        public string[] CodPerfil { get; set; }
        public string VersaoApp { get; set; }

        public string Caixa { get; set; }

        public object key1 { get; set; }
        public object key2 { get; set; }
        public object key3 { get; set; }
        public object key4 { get; set; }
        public object key5 { get; set; }

        public List<PS.Lib.DataField> Current { get; set; }
    }
}
