using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb.Support
{
    public class FinLanBaixaPar
    {
        public int CodEmpresa { get; set; }
        public DateTime DataBaixa { get; set; }
        public string CodConta { get; set; }
        public FinLanPagRecEnum PagRec { get; set; }
        public FinLanBaixaGeraExtratoComoEnum GeraExtratoComo { get; set; }
        public string NumeroExtrato { get; set; }
        public string Historico { get; set; }
        public int[] CodLanca { get; set; }
        public decimal[] ValorBaixa { get; set; }
        public string codCCusto { get; set; }
        public string codNaturezaOrcamento { get; set; }
        public int CODCHEQUE { get; set; }
        public string[] NFOUDUP { get; set; }
        public bool cheque { get; set; }
    }
}
