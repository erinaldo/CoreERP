using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Glb.New.Classes.Models
{
    public class AAPONTAMENTOTAREFA
    {
        public AAPONTAMENTOTAREFA() { }

        public int CodEmpresa { get; set; }
        public int CodFilial { get; set; }
        public int IDApontamento { get; set; }
        public int IDApontamentoTarefa { get; set; }
        public int IDTarefa { get; set; }
        public DateTime Horas { get; set; }
        public int Percentual { get; set; }
        public string Observacao { get; set; }
        public int Minutos { get; set; }
    }
}
