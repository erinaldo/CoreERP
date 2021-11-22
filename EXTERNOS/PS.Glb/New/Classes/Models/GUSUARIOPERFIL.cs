using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Glb.New.Classes.Models
{
    public class GUSUARIOPERFIL
    {
        public GUSUARIOPERFIL() { }

        public int CodColigada { get; set; }

        public string CodPerfil { get; set; }

        public List<string> Usuarios { get; set; }
    }
}
