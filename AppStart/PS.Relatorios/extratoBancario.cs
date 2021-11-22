using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class extratoBancario
    {
        public int IDEXTRATO { get; set; }
        public DateTime DATACOMPENSACAO { get; set; }
        public DateTime DATA { get; set; }
        public string NUMERODOCUMENTO { get; set; }
        public string HISTORICO { get; set; }
        public decimal ENTRADAS { get; set; }
        public decimal SAIDAS { get; set; }
        public decimal SALDO { get; set; }

    }
}
