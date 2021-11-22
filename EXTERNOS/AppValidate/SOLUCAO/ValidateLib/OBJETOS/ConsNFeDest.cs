using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class ConsNFeDest : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idConsNFe;
        private int _idEmpresa;
        private string _NSU;
        private string _chNFe;
        private string _CNPJ;
        private string _xNome;
        private string _IE;
        private DateTime _dEmi;
        private string _tpNF;
        private object _vNF;
        private string _digVal;
        private DateTime _dhRecbto;
        private string _cSitNFe = string.Empty;
        private string _cSitConf = string.Empty;

        [ParamsAttribute("IDCONSNFE")]
        [DataMember]
        public int IdConsNFe
        {
            get
            {
                return this._idConsNFe;
            }
            set
            {
                this._idConsNFe = value;
            }
        }

        [ParamsAttribute("IDEMPRESA")]
        [DataMember]
        public int IdEmpresa
        {
            get
            {
                return this._idEmpresa;
            }
            set
            {
                this._idEmpresa = value;
            }
        }

        [ParamsAttribute("NSU")]
        [DataMember]
        public string NSU
        {
            get
            {
                return this._NSU;
            }
            set
            {
                this._NSU = value;
            }
        }

        [ParamsAttribute("CHNFE")]
        [DataMember]
        public string chNFe
        {
            get
            {
                return this._chNFe;
            }
            set
            {
                this._chNFe = value;
            }
        }

        [ParamsAttribute("CNPJ")]
        [DataMember]
        public string CNPJ
        {
            get
            {
                return this._CNPJ;
            }
            set
            {
                this._CNPJ = value;
            }
        }

        [ParamsAttribute("xNome")]
        [DataMember]
        public string xNome
        {
            get
            {
                return this._xNome;
            }
            set
            {
                this._xNome = value;
            }
        }

        [ParamsAttribute("IE")]
        [DataMember]
        public string IE
        {
            get
            {
                return this._IE;
            }
            set
            {
                this._IE = value;
            }
        }

        [ParamsAttribute("dEmi")]
        [DataMember]
        public DateTime dEmi
        {
            get
            {
                return this._dEmi;
            }
            set
            {
                this._dEmi = value;
            }
        }

        [ParamsAttribute("tpNF")]
        [DataMember]
        public string tpNF
        {
            get
            {
                return this._tpNF;
            }
            set
            {
                this._tpNF = value;
            }
        }

        [ParamsAttribute("vNF")]
        [DataMember]
        public object vNF
        {
            get
            {
                return this._vNF;
            }
            set
            {
                this._vNF = value;
            }
        }

        [ParamsAttribute("digVal")]
        [DataMember]
        public string digVal
        {
            get
            {
                return this._digVal;
            }
            set
            {
                this._digVal = value;
            }
        }

        [ParamsAttribute("dhRecbto")]
        [DataMember]
        public DateTime dhRecbto
        {
            get
            {
                return this._dhRecbto;
            }
            set
            {
                this._dhRecbto = value;
            }
        }

        [ParamsAttribute("cSitNFe")]
        [DataMember]
        public string cSitNFe
        {
            get
            {
                return this._cSitNFe;
            }
            set
            {
                this._cSitNFe = value;
            }
        }

        [ParamsAttribute("cSitConf")]
        [DataMember]
        public string cSitConf
        {
            get
            {
                return this._cSitConf;
            }
            set
            {
                this._cSitConf = value;
            }
        }

        public void Save()
        {
            if (this.IdConsNFe == 0)
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT CHNFE FROM ZCONSNFE WHERE CHNFE = ?";
                if (!_conn.ExecHasRows(sSql, this.chNFe))
                {
                    sSql = @"INSERT INTO ZCONSNFE (IDEMPRESA, NSU, CHNFE, CNPJ, XNOME, IE, DEMI, TPNF, VNF, DIGVAL, DHRECBTO, CSITNFE, CSITCONF)
                            VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    _conn.ExecTransaction(sSql, new Object[] { this.IdEmpresa, this.NSU, this.chNFe, this.CNPJ, this.xNome, this.IE, this.dEmi, this.tpNF, this.vNF, this.digVal, this.dhRecbto, this.cSitNFe, this.cSitConf });

                    sSql = @"SELECT IDCONSNFE FROM ZCONSNFE WHERE CHNFE = ?";
                    int IdConsNFE = Convert.ToInt32(_conn.ExecGetField(0, sSql, this.chNFe));


                    ServiceParams _serviceParams = ServiceParams.Read();
                    EmpresaParams _empresaParams = EmpresaParams.ReadByIdEmpresa(this.IdEmpresa);
                    UFParams _ufParams = UFParams.ReadByCodigoIBGE(this.chNFe.Substring(0, 2));

                    InboxParams inboxParams = new InboxParams();
                    inboxParams.Chave = this.chNFe;
                    inboxParams.Arquivo = null;
                    inboxParams.CNPJDestinatario = _empresaParams.CNPJ.Replace(".", "").Replace("/", "").Replace("-", "");
                    inboxParams.CNPJEmitente = this.CNPJ;
                    inboxParams.CodEstrutura = "NF-e";
                    inboxParams.CodOrigem = "CON";
                    inboxParams.CodStatus = "HAS";
                    inboxParams.Data = _conn.GetDateTime(); ;
                    inboxParams.DataEmissao = this.dEmi;
                    inboxParams.DataLimite = this.dEmi.AddMinutes(_serviceParams.PrazoCancelamento);
                    inboxParams.Destinatario = _empresaParams.Nome;
                    inboxParams.Emitente = this.xNome;
                    inboxParams.Hash = this.digVal;
                    inboxParams.IdConfigDIR = null;
                    inboxParams.IdConfigFTP = null;
                    inboxParams.IdConfigPOP = null;
                    inboxParams.IdConsNFE = (IdConsNFE == 0) ? null : (int?)IdConsNFE;
                    inboxParams.IdInbox = 0;
                    inboxParams.Log = null;
                    inboxParams.DataUltimoLog = null;
                    inboxParams.MunicipioDestinatario = _empresaParams.Municipio;
                    inboxParams.MunicipioEmitente = null;
                    inboxParams.NumeroDocumento = this.chNFe.Substring(25,9);
                    inboxParams.Texto = null;
                    inboxParams.UFDestinatario = _empresaParams.UF;
                    inboxParams.UFEmitente = _ufParams.UF;
                    inboxParams.Versao = null;

                    if (inboxParams.ExisteByChaveNFE())
                        inboxParams.DeleteByChaveNFE();

                    inboxParams.Save();



                    /*
                    EmpresaParams empresaParams = EmpresaParams.ReadByIdEmpresa(this.IdEmpresa);
                    if (empresaParams.ManDestAut.Equals(1))
                    {
                        ValidateLib.EnvConfRecebto _env = new ValidateLib.EnvConfRecebto();
                        _env.IdEmpresa = this.IdEmpresa;
                        _env.chNFe = this.chNFe;
                        _env.tpEvento = "210200";
                        _env.xJust = null;
                        _env.InserirEventoManifestacaoDestinatario();
                    }
                    */
                }
            }        
        }

        public static ConsNFeDest ReadByChNFe(params object[] parameters)
        {
            string sSql = @"SELECT * FROM ZCONSNFE WHERE CHNFE = ?";
            ConsNFeDest _consNFeDest = new ConsNFeDest();
            _consNFeDest.ReadFromCommand(sSql, parameters);
            return _consNFeDest;
        }

        public static ConsNFeDest ReadByIdConsNFe(params object[] parameters)
        {
            string sSql = @"SELECT * FROM ZCONSNFE WHERE IDCONSNFE = ?";
            ConsNFeDest _consNFeDest = new ConsNFeDest();
            _consNFeDest.ReadFromCommand(sSql, parameters);
            return _consNFeDest;
        }
    }
}
