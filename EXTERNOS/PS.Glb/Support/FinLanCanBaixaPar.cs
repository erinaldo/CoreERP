using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb.Support
{
    public class FinLanCanBaixaPar
    {
        public int CodEmpresa { get; set; }
        public int[] CodLanca { get; set; }
        public DateTime DataCancelamento { get; set; }
        public string MotivoCancelamento { get; set; }
        public string UsuarioCancelamento { get; set; }
    }
}
