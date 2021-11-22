using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PS.Glb.Class
{
    public class ControleCheque
    {
        #region Propriedades 

        public int CODEMPRESA { get; set; }
        public int CODCHEQUE { get; set; }
        public string CODCONTA { get; set; }
        public int NUMERO { get; set; }
        public int PAGREC { get; set; }
        public decimal VALOR { get; set; }
        public DateTime? DATACRIACAO { get; set; }
        public string USUARIOCRIACAO { get; set; }
        public DateTime? DATAALTERACAO { get; set; }
        public string USUARIOALTERACAO { get; set; }
        public DateTime? DATABOA { get; set; }
        public string OBSERVACAO { get; set; }
        public string CODBANCO { get; set; }
        public string CODAGENCIA { get; set; }
        public string CODCCORRENTE { get; set; }
        public DateTime? DATAEMISSAO { get; set; }
        public DateTime? DATACOMPENSACAO { get; set; }
        public int COMPENSADO { get; set; }

        #endregion

        #region Variáveis 

        List<ControleCheque> ListControleCheque = new List<ControleCheque>();

        #endregion

        public ControleCheque(int _codempresa, int _codCheque, string _codConta, int _numero, int _pagRec, decimal _valor, DateTime? _dataCriacao, string _usuarioCriacao, DateTime? _dataAlteracao, string _usuarioAlteracao, DateTime? _dataBoa, string _observacao, string _codBanco, string _codAgencia, string _codCorrente, DateTime? _dataEmissao, DateTime? _datCompensacao, int _compensado)
        {
            CODEMPRESA = _codempresa;
            CODCHEQUE = _codCheque;
            CODCONTA = _codConta;
            NUMERO = _numero;
            PAGREC = _pagRec;
            VALOR = _valor;
            DATACRIACAO = _dataCriacao;
            USUARIOCRIACAO = _usuarioCriacao;
            DATAALTERACAO = _dataAlteracao;
            USUARIOALTERACAO = _usuarioAlteracao;
            DATABOA = _dataBoa;
            OBSERVACAO = _observacao;
            CODBANCO = _codBanco;
            CODAGENCIA = _codAgencia;
            CODCCORRENTE = _codCorrente;
            DATAEMISSAO = _dataEmissao;
            DATACOMPENSACAO = _datCompensacao;
            COMPENSADO = _compensado;
        }
    }
}
