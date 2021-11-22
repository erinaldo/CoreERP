using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Glb.New.Classes.Models
{
    public class GPERMISSAOMODULO
    {
        public int IDPermissaoMenu { get; set; }

        public List<string> CodModulo { get; set; }

        public string CodPerfil { get; set; }

        public List<int> Acesso { get; set; }
    }
}
