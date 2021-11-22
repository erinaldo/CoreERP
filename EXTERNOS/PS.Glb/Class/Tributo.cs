using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb.Class
{
    public class Tributo
    {
        public int CODEMPRESA { get; set; }
        public int CODOPER { get; set; }
        public int NSEQITEM { get; set; }
        public string CODTRIBUTO { get; set; }
        public decimal ALIQUOTA { get; set; }
        public decimal VALOR { get; set; }
        public string CODCST { get; set; }
        public string BASECALCULO { get; set; }
        public string MODALIDADEBC { get; set; }
        public decimal REDUCAOBASEICMS { get; set; }
        public string CENQ { get; set; }
        public decimal FATORMVA { get; set; }
        public string BCORIGINAL { get; set; }
        public decimal REDUCAOBASEICMSST { get; set; }
        public decimal VALORICMSST { get; set; }
        public decimal PDIF { get; set; }
        public decimal VICMSDIF { get; set; }

    }
}
