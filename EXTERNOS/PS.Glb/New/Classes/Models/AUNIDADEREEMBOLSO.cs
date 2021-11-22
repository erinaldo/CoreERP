using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Glb.New.Classes.Models
{
    public class AUNIDADEREEMBOLSO
    {
        public AUNIDADEREEMBOLSO() { }

        public int CodEmpresa { get; set; }
        public int CodFilial { get; set; }
        public int IDReembolso { get; set; }
        public int IDUnidade { get; set; }
        public string CodUsuario { get; set; }
        public int DistanciaKM { get; set; }
        public decimal ValorKM { get; set; }
        public decimal ValorRefeicao { get; set; }
        public decimal ValorPedagio { get; set; }
        public string CodProduto { get; set; }
        public decimal ValorHora { get; set; }
        public string CoordenadorCliente { get; set; }
    }
}
