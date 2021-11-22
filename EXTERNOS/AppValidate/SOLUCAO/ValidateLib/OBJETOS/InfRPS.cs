using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class InfRPS : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private string _numero;
        private string _serie;
        private int _tipo;
        private DateTime _dataEmissao;
        private int _naturezaOperacao;
        private int? _regimeEspecialTributacao;
        private int _optanteSimplesNacional;
        private int _incentivadorCultural;
        private int _status;

        private string _rpsSubNumero;
        private string _rpsSubSerie;
        private int _rpsSubTipo;

        private decimal _valorServicos;
        private decimal _valorDeducoes;
        private decimal _valorPIS;
        private decimal _valorCOFINS;
        private decimal _valorINSS;
        private decimal _valorIR;
        private decimal _valorCSLL;
        private int _issRetido;
        private decimal _valorISS;
        private decimal _outrasRetencoes;
        private decimal _baseCalculo;
        private decimal _aliquota;
        private decimal _valorLiquidoNFSe;
        private decimal _valorISSRetido;
        private decimal _descontoCondicionado;
        private decimal _descontoIncondicionado;

        private string _itemListaServico;
        private string _codigoCNAE;
        private string _codigoTributacaoMunicipio;
        private string _discriminacao;
        private string _codigoMunicipio;

        private string _cnpj;
        private string _inscricaoMunicipal;

        private string _tomCPFCNPJ;
        private string _tomInscricaoMunicipal;
        private string _tomRazaoSocial;
        private string _tomEndereco;
        private string _tomNumero;
        private string _tomComplemento;
        private string _tomBairro;
        private string _tomCodigoMunicipio;
        private string _tomUF;
        private string _tomCEP;
        private string _tomTelefone;
        private string _tomEmail;

        private string _intRazaoSocial;
        private string _intCPFCNPJ;
        private string _intInscricaoMunicipal;

        private string _cvCodigoObra;
        private string _cvART;

        [ParamsAttribute("IDOUTBOX")]
        [DataMember]
        public int IdOutbox
        {
            get
            {
                return this._idOutbox;
            }
            set
            {
                this._idOutbox = value;
            }
        }

        [ParamsAttribute("NUMERO")]
        [DataMember]
        public string Numero
        {
            get
            {
                return this._numero;
            }
            set
            {
                this._numero = value;
            }
        }

        [ParamsAttribute("SERIE")]
        [DataMember]
        public string Serie
        {
            get
            {
                return this._serie;
            }
            set
            {
                this._serie = value;
            }
        }

        [ParamsAttribute("TIPO")]
        [DataMember]
        public int Tipo
        {
            get
            {
                return this._tipo;
            }
            set
            {
                this._tipo = value;
            }
        }

        [ParamsAttribute("DATAEMISSAO")]
        [DataMember]
        public DateTime DataEmissao
        {
            get
            {
                return this._dataEmissao;
            }
            set
            {
                this._dataEmissao = value;
            }
        }

        [ParamsAttribute("NATUREZAOPERACAO")]
        [DataMember]
        public int NaturezaOperacao
        {
            get
            {
                return this._naturezaOperacao;
            }
            set
            {
                this._naturezaOperacao = value;
            }
        }

        [ParamsAttribute("REGIMEESPECIALTRIBUTACAO")]
        [DataMember]
        public int? RegimeEspecialTributacao
        {
            get
            {
                return this._regimeEspecialTributacao;
            }
            set
            {
                this._regimeEspecialTributacao = value;
            }
        }

        [ParamsAttribute("OPTANTESIMPLESNACIONAL")]
        [DataMember]
        public int OptanteSimplesNacional
        {
            get
            {
                return this._optanteSimplesNacional;
            }
            set
            {
                this._optanteSimplesNacional = value;
            }
        }

        [ParamsAttribute("INCENTIVADORCULTURAL")]
        [DataMember]
        public int IncentivadorCultural
        {
            get
            {
                return this._incentivadorCultural;
            }
            set
            {
                this._incentivadorCultural = value;
            }
        }

        [ParamsAttribute("STATUS")]
        [DataMember]
        public int Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        [ParamsAttribute("RPSSUBNUMERO")]
        [DataMember]
        public string RPSSubNumero
        {
            get
            {
                return this._rpsSubNumero;
            }
            set
            {
                this._rpsSubNumero = value;
            }
        }

        [ParamsAttribute("RPSSUBSERIE")]
        [DataMember]
        public string RPSSubSerie
        {
            get
            {
                return this._rpsSubSerie;
            }
            set
            {
                this._rpsSubSerie = value;
            }
        }

        [ParamsAttribute("RPSSUBTIPO")]
        [DataMember]
        public int RPSSubTipo
        {
            get
            {
                return this._rpsSubTipo;
            }
            set
            {
                this._rpsSubTipo = value;
            }
        }

        [ParamsAttribute("VALORSERVICOS")]
        [DataMember]
        public decimal ValorServicos
        {
            get
            {
                return this._valorServicos;
            }
            set
            {
                this._valorServicos = value;
            }
        }

        [ParamsAttribute("VALORDEDUCOES")]
        [DataMember]
        public decimal ValorDeducoes
        {
            get
            {
                return this._valorDeducoes;
            }
            set
            {
                this._valorDeducoes = value;
            }
        }

        [ParamsAttribute("VALORPIS")]
        [DataMember]
        public decimal ValorPIS
        {
            get
            {
                return this._valorPIS;
            }
            set
            {
                this._valorPIS = value;
            }
        }

        [ParamsAttribute("VALORCOFINS")]
        [DataMember]
        public decimal ValorCOFINS
        {
            get
            {
                return this._valorCOFINS;
            }
            set
            {
                this._valorCOFINS = value;
            }
        }

        [ParamsAttribute("VALORINSS")]
        [DataMember]
        public decimal ValorINSS
        {
            get
            {
                return this._valorINSS;
            }
            set
            {
                this._valorINSS = value;
            }
        }

        [ParamsAttribute("VALORIR")]
        [DataMember]
        public decimal ValorIR
        {
            get
            {
                return this._valorIR;
            }
            set
            {
                this._valorIR = value;
            }
        }

        [ParamsAttribute("VALORCSLL")]
        [DataMember]
        public decimal ValorCSLL
        {
            get
            {
                return this._valorCSLL;
            }
            set
            {
                this._valorCSLL = value;
            }
        }

        [ParamsAttribute("ISSRETIDO")]
        [DataMember]
        public int ISSRetido
        {
            get
            {
                return this._issRetido;
            }
            set
            {
                this._issRetido = value;
            }
        }

        [ParamsAttribute("VALORISS")]
        [DataMember]
        public decimal ValorISS
        {
            get
            {
                return this._valorISS;
            }
            set
            {
                this._valorISS = value;
            }
        }

        [ParamsAttribute("OUTRASRETENCOES")]
        [DataMember]
        public decimal OutrasRetencoes
        {
            get
            {
                return this._outrasRetencoes;
            }
            set
            {
                this._outrasRetencoes = value;
            }
        }

        [ParamsAttribute("BASECALCULO")]
        [DataMember]
        public decimal BaseCalculo
        {
            get
            {
                return this._baseCalculo;
            }
            set
            {
                this._baseCalculo = value;
            }
        }

        [ParamsAttribute("ALIQUOTA")]
        [DataMember]
        public decimal Aliquota
        {
            get
            {
                return this._aliquota;
            }
            set
            {
                this._aliquota = value;
            }
        }

        [ParamsAttribute("VALORLIQUIDONFSE")]
        [DataMember]
        public decimal ValorLiquidoNFSe
        {
            get
            {
                return this._valorLiquidoNFSe;
            }
            set
            {
                this._valorLiquidoNFSe = value;
            }
        }

        [ParamsAttribute("VALORISSRETIDO")]
        [DataMember]
        public decimal ValorISSRetido
        {
            get
            {
                return this._valorISSRetido;
            }
            set
            {
                this._valorISSRetido = value;
            }
        }

        [ParamsAttribute("DESCONTOCONDICIONADO")]
        [DataMember]
        public decimal DescontoCondicionado
        {
            get
            {
                return this._descontoCondicionado;
            }
            set
            {
                this._descontoCondicionado = value;
            }
        }

        [ParamsAttribute("DESCONTOINCONDICIONADO")]
        [DataMember]
        public decimal DescontoIncondicionado
        {
            get
            {
                return this._descontoIncondicionado;
            }
            set
            {
                this._descontoIncondicionado = value;
            }
        }

        [ParamsAttribute("ITEMLISTASERVICO")]
        [DataMember]
        public string ItemListaServico
        {
            get
            {
                return this._itemListaServico;
            }
            set
            {
                this._itemListaServico = value;
            }
        }

        [ParamsAttribute("CODIGOCNAE")]
        [DataMember]
        public string CodigoCNAE
        {
            get
            {
                return this._codigoCNAE;
            }
            set
            {
                this._codigoCNAE = value;
            }
        }

        [ParamsAttribute("CODIGOTRIBUTACAOMUNICIPIO")]
        [DataMember]
        public string CodigoTributacaoMunicipio
        {
            get
            {
                return this._codigoTributacaoMunicipio;
            }
            set
            {
                this._codigoTributacaoMunicipio = value;
            }
        }

        [ParamsAttribute("DISCRIMINACAO")]
        [DataMember]
        public string Discriminacao
        {
            get
            {
                return this._discriminacao;
            }
            set
            {
                this._discriminacao = value;
            }
        }

        [ParamsAttribute("CODIGOMUNICIPIO")]
        [DataMember]
        public string CodigoMunicipio
        {
            get
            {
                return this._codigoMunicipio;
            }
            set
            {
                this._codigoMunicipio = value;
            }
        }

        [ParamsAttribute("CNPJ")]
        [DataMember]
        public string CNPJ
        {
            get
            {
                return this._cnpj;
            }
            set
            {
                this._cnpj = value;
            }
        }

        [ParamsAttribute("INSCRICAOMUNICIPAL")]
        [DataMember]
        public string InscricaoMunicipal
        {
            get
            {
                return this._inscricaoMunicipal;
            }
            set
            {
                this._inscricaoMunicipal = value;
            }
        }

        [ParamsAttribute("TOMCPFCNPJ")]
        [DataMember]
        public string TomCPFCNPJ
        {
            get
            {
                return this._tomCPFCNPJ;
            }
            set
            {
                this._tomCPFCNPJ = value;
            }
        }

        [ParamsAttribute("TOMINSCRICAOMUNICIPAL")]
        [DataMember]
        public string TomInscricaoMunicipal
        {
            get
            {
                return this._tomInscricaoMunicipal;
            }
            set
            {
                this._tomInscricaoMunicipal = value;
            }
        }

        [ParamsAttribute("TOMRAZAOSOCIAL")]
        [DataMember]
        public string TomRazaoSocial
        {
            get
            {
                return this._tomRazaoSocial;
            }
            set
            {
                this._tomRazaoSocial = value;
            }
        }

        [ParamsAttribute("TOMENDERECO")]
        [DataMember]
        public string TomEndereco
        {
            get
            {
                return this._tomEndereco;
            }
            set
            {
                this._tomEndereco = value;
            }
        }

        [ParamsAttribute("TOMNUMERO")]
        [DataMember]
        public string TomNumero
        {
            get
            {
                return this._tomNumero;
            }
            set
            {
                this._tomNumero = value;
            }
        }

        [ParamsAttribute("TOMCOMPLEMENTO")]
        [DataMember]
        public string TomComplemento
        {
            get
            {
                return this._tomComplemento;
            }
            set
            {
                this._tomComplemento = value;
            }
        }

        [ParamsAttribute("TOMBAIRRO")]
        [DataMember]
        public string TomBairro
        {
            get
            {
                return this._tomBairro;
            }
            set
            {
                this._tomBairro = value;
            }
        }

        [ParamsAttribute("TOMCODIGOMUNICIPIO")]
        [DataMember]
        public string TomCodigoMunicipio
        {
            get
            {
                return this._tomCodigoMunicipio;
            }
            set
            {
                this._tomCodigoMunicipio = value;
            }
        }

        [ParamsAttribute("TOMUF")]
        [DataMember]
        public string TomUF
        {
            get
            {
                return this._tomUF;
            }
            set
            {
                this._tomUF = value;
            }
        }

        [ParamsAttribute("TOMCEP")]
        [DataMember]
        public string TomCEP
        {
            get
            {
                return this._tomCEP;
            }
            set
            {
                this._tomCEP = value;
            }
        }

        [ParamsAttribute("TOMTELEFONE")]
        [DataMember]
        public string TomTelefone
        {
            get
            {
                return this._tomTelefone;
            }
            set
            {
                this._tomTelefone = value;
            }
        }

        [ParamsAttribute("TOMEMAIL")]
        [DataMember]
        public string TomEmail
        {
            get
            {
                return this._tomEmail;
            }
            set
            {
                this._tomEmail = value;
            }
        }

        [ParamsAttribute("INTRAZAOSOCIAL")]
        [DataMember]
        public string IntRazaoSocial
        {
            get
            {
                return this._intRazaoSocial;
            }
            set
            {
                this._intRazaoSocial = value;
            }
        }

        [ParamsAttribute("INTCPFCNPJ")]
        [DataMember]
        public string IntCPFCNPJ
        {
            get
            {
                return this._intCPFCNPJ;
            }
            set
            {
                this._intCPFCNPJ = value;
            }
        }

        [ParamsAttribute("INTINSCRICAOMUNICIPAL")]
        [DataMember]
        public string IntInscricaoMunicipal
        {
            get
            {
                return this._intInscricaoMunicipal;
            }
            set
            {
                this._intInscricaoMunicipal = value;
            }
        }

        [ParamsAttribute("CVCODIGOOBRA")]
        [DataMember]
        public string CVCodigoObra
        {
            get
            {
                return this._cvCodigoObra;
            }
            set
            {
                this._cvCodigoObra = value;
            }
        }

        [ParamsAttribute("CVART")]
        [DataMember]
        public string CVART
        {
            get
            {
                return this._cvART;
            }
            set
            {
                this._cvART = value;
            }
        }

        public static InfRPS ReadByIDOutbox(params object[] parameters)
        {
            InfRPS _infRPS = new InfRPS();

            try
            {
                string sSql = @"SELECT * FROM ZINFRPS WHERE IDOUTBOX = ?";
                _infRPS.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(ex.Message);
            }
            return _infRPS;
        }
    }
}
