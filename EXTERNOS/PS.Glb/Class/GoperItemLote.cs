using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb.Class
{
    public static class GoperItemLote
    {
        public static int CODFILIAL { get; set; }
        public static string CODLOCAL { get; set; }
        public static int CODLOTE { get; set; }
        public static int CODOPER { get; set; }
        public static int NSEQITEM { get; set; }
        public static string CODPRODUTO { get; set; }
        public static decimal QUANTIDADE { get; set; }
        public static string CODUNIDCONTROLE { get; set; }
        public static decimal QUANTIDADEOPER { get; set; }
        public static string CODUNIDOPER { get; set; }
        public static string USUARIOCRIACAO { get; set; }
        public static DateTime DATACRIACAO { get; set; }
        public static string USUARIOALTERACAO { get; set; }
        public static DateTime? DATAALTERACAO { get; set; }

        public static int _codLote;

        public static int getCodlote(int _codlote)
        {
            _codLote = _codlote;
            return _codLote;
        }
    }
}
