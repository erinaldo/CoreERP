using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Glb.New.Classes.Models
{
    public class APROJETOTAREFA
    {
        public APROJETOTAREFA() { }

        public int CodEmpresa { get; set; }
        public int CodFilial { get; set; }
        public int IDProjeto { get; set; }
        public int IDTarefa { get; set; }
        public string Nome { get; set; }
        public string CodUsuario { get; set; }
        public DateTime PrevisaoEntrega { get; set; }
        public int Prioridade { get; set; }
        public int Inloco { get; set; }
        public DateTime DataConclusao { get; set; }
        public string TipoFaturamento { get; set; }
        public int PrevisaoHoras { get; set; }
        public string Descricao { get; set; }
    }
}
