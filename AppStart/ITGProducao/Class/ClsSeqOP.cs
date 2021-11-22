using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITGProducao.Class
{
    public class ClsSeqOP
    {
        public string CodEstrutura { get; set; }
        public string CodEstruturaPai { get; set; }
        public string SeqOpe { get; set; }
        public int SeqOp { get; set; }
        public int SeqOpPai { get; set; }

        public ClsSeqOP(string CodEstrutura, string CodEstruturaPai, int SeqOp, int SeqOpPai, string SeqOpe)
        {
            this.CodEstrutura = CodEstrutura;
            this.CodEstruturaPai = CodEstruturaPai;
            this.SeqOp = SeqOp;
            this.SeqOpPai = SeqOpPai;
            this.SeqOpe = SeqOpe;
        }
    }
}
