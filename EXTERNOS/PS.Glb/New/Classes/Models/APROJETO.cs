using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Glb.New.Classes.Models
{
    public class APROJETO
    {
        public APROJETO() { }

        public int CodEmpresa { get; set; }
        public int CodFilial { get; set; }
        public int IDProjeto { get; set; }

        public int IDUnidade { get; set; }
        public string Descricao { get; set; }
        public string Escopo { get; set; }
        public DateTime DataCriacao { get; set; }
        public string CodUsuarioPrestador { get; set; }
        public string CodUsuarioCliente { get; set; }
        public string CodProduto { get; set; }
        public string CodCCusto { get; set; }
        public string CodNatureza { get; set; }
        public decimal ValorHora { get; set; }
        public int Nivel { get; set; }
        public DateTime DataConclusao { get; set; }
        public string Status { get; set; }
        public string Tipo { get; set; }

        public List<AAPONTAMENTOTAREFA> TarefasProjeto { get; set; }
    }
}
