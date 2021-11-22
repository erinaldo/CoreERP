using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Glb.New.Classes.Models
{
    public class AAPONTAMENTO
    {
        public AAPONTAMENTO() { }

        public int CodEmpresa { get; set; }
        public int CodFilial { get; set; }
        public int IDApontamento { get; set; }
        public string CodUsuario { get; set; }
        public DateTime Data { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public DateTime Abono { get; set; }
        public int InLoco { get; set; }
        public int Reembolso { get; set; }
        public int IDProjeto { get; set; }
        public DateTime DataEnvio { get; set; }
        public DateTime DataRetorno { get; set; }
        public int IDStatusApontamento { get; set; }
        public string MotivoAprovacao { get; set; }
        public int CodOperDemanda { get; set; }
        public int CodOperReembolsoCliente { get; set; }
        public int CodOperReembolsoAnalista { get; set; }
        public int IDUnidade { get; set; }
        public decimal ValorAdicional { get; set; }
        public int MotivoValorAdicional { get; set; }
        public int Penalidade { get; set; }
        public DateTime DataPenalidade { get; set; }
        public DateTime DataProcessamento { get; set; }
        public DateTime DataDigitacao { get; set; }
        public string TipoFaturamento { get; set; }

        public List<AAPONTAMENTOTAREFA> TarefasApontamento { get; set; }
    }
}
