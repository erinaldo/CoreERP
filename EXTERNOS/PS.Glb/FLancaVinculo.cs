using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class FLancaVinculo
    {
        public int CODLANCA { get; set; }
        public String CODTIPDOC { get; set; }
        public String NUMERO { get; set; }
        public DateTime DATAEMISSAO { get; set; }
        public decimal VLORIGINAL { get; set; }
        public DateTime DATAVENCIMENTO { get; set; }
        public decimal VLLIQUIDO { get; set; }
        public DateTime? DATABAIXA { get; set; }
        public decimal? VLBAIXADO { get; set; }
    }
}
