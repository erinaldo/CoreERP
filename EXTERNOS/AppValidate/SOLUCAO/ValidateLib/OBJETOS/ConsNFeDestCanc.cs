using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class ConsNFeDestCanc : ParamsBase
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
                string sSql = @"INSERT INTO ZCONSNFECAN (IDEMPRESA, NSU, CHNFE, CNPJ, XNOME, IE, DEMI, TPNF, VNF, DIGVAL, DHRECBTO, CSITNFE, CSITCONF)
                                                            VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                _conn.ExecTransaction(sSql, new Object[] { this.IdEmpresa, this.NSU, this.chNFe, this.CNPJ, this.xNome, this.IE, this.dEmi, this.tpNF, this.vNF, this.digVal, this.dhRecbto, this.cSitNFe, this.cSitConf });
            }
        }

        public static ConsNFeDestCanc ReadByIdConsNFe(params object[] parameters)
        {
            string sSql = @"SELECT * FROM ZCONSNFECAN WHERE IDCONSNFE = ?";
            ConsNFeDestCanc _consNFeDestCanc = new ConsNFeDestCanc();
            _consNFeDestCanc.ReadFromCommand(sSql, parameters);
            return _consNFeDestCanc;
        }
    }
}
