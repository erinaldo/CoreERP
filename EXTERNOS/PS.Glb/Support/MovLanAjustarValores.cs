using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb.Support
{
    public class MovLanAjustarValores
    {
        public int CodEmpresa { get; set; }
        public int CodOper { get; set; }
        public int CodLanca { get; set; }
        public string Numero { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorOriginal { get; set; }
        public decimal ValorLiquido { get; set; }
        public decimal ValorBaixado { get; set; }
    }
}
