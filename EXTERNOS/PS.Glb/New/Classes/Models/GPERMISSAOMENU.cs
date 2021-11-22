using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Glb.New.Classes.Models
{
    public class GPERMISSAOMENU
    {
        public int IDPermissaoMenu { get; set; }

        public string CodMenu { get; set; }

        public string CodPerfil { get; set; }

        public int Acesso { get; set; }

        public int Edicao { get; set; }

        public int Inclusao { get; set; }

        public int Exclusao { get; set; }
    }
}
