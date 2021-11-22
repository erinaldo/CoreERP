using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Glb.New.Classes.Models
{
    public class AUNIDADE
    {
        public AUNIDADE() { }

        public int CodEmpresa { get; set; }
        public int CodFilial { get; set; }
        public int IDUnidade { get; set; }
        public string CodCliFor { get; set; }
        public string Nome { get; set; }
        public string CodEstado { get; set; }
        public string CodCidade { get; set; }
        public string Observacao { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public int DistanciaKM { get; set; }
        public decimal ValorKM { get; set; }
        public string CodProduto { get; set; }
        public decimal ValorRefeicao { get; set; }
        public decimal ValorPedagio { get; set; }
        public decimal ValorHora { get; set; }
    }
}
