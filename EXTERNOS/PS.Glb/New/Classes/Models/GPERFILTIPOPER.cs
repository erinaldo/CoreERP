using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Glb.New.Classes.Models
{
    public class GPERFILTIPOPER
    {
        public GPERFILTIPOPER() { }

        public int CodColigada { get; set; }

        public string CodPerfil { get; set; }

        public List<string> CodTipOper { get; set; }

        public List<int> Incluir { get; set; }

        public List<int> Excluir { get; set; }

        public List<int> Alterar { get; set; }

        public List<int> Faturar { get; set; }

        public List<int> IncluirFat { get; set; }

        public List<int> Consultar { get; set; }

        public List<int> Cancelar { get; set; }

        public List<int> Concluir { get; set; }

        public List<int> Aprovar { get; set; }

        public List<int> AprovaFinanceiro { get; set; }

        public List<int> AprovaDesconto { get; set; }

        public List<int> AprovaLimiteCredito { get; set; }

        public List<int> Reprovar { get; set; }

        public List<int> GerarBoleto { get; set; }
    }
}
