using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb.Support
{
    public class FinLanBaixaBase
    {
        public int CodEmpresa { get; set; }
        public int CodFilial { get; set; }
        public int CodLanca { get; set; }
        public string Numero { get; set; }
        public string CodCliFor { get; set; }
        public string NomeFantasia { get; set; }
        public DateTime DataVencimento { get; set; }
        public Decimal ValorOriginal { get; set; }
        public Decimal ValorLiquido { get; set; }
        public Decimal ValorBaixado { get; set; }
        public decimal ValorVincAD { get; set; }
        public decimal ValorVincDV { get; set; }
    }
}
