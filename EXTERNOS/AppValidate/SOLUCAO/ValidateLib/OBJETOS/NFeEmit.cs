using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeEmit : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private string _CNPJ;
        private string _CPF;
        private string _xNome;
        private string _xFant;
        private string _xLgr;
        private string _nro;
        private string _xCpl;
        private string _xBairro;
        private string _cMun;
        private string _xMun;
        private string _UF;
        private string _CEP;
        private int _cPais;
        private string _xPais;
        private string _fone;
        private string _IE;
        private string _IEST;
        private string _IM;
        private string _CNAE;
        private int _CRT;

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

        [ParamsAttribute("CPF")]
        [DataMember]
        public string CPF
        {
            get
            {
                return this._CPF;
            }
            set
            {
                this._CPF = value;
            }
        }

        [ParamsAttribute("XNOME")]
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

        [ParamsAttribute("XFANT")]
        [DataMember]
        public string xFant
        {
            get
            {
                return this._xFant;
            }
            set
            {
                this._xFant = value;
            }
        }

        [ParamsAttribute("XLGR")]
        [DataMember]
        public string xLgr
        {
            get
            {
                return this._xLgr;
            }
            set
            {
                this._xLgr = value;
            }
        }

        [ParamsAttribute("NRO")]
        [DataMember]
        public string nro
        {
            get
            {
                return this._nro;
            }
            set
            {
                this._nro = value;
            }
        }

        [ParamsAttribute("XCPL")]
        [DataMember]
        public string xCpl
        {
            get
            {
                return this._xCpl;
            }
            set
            {
                this._xCpl = value;
            }
        }

        [ParamsAttribute("XBAIRRO")]
        [DataMember]
        public string xBairro
        {
            get
            {
                return this._xBairro;
            }
            set
            {
                this._xBairro = value;
            }
        }

        [ParamsAttribute("CMUN")]
        [DataMember]
        public string cMun
        {
            get
            {
                return this._cMun;
            }
            set
            {
                this._cMun = value;
            }
        }

        [ParamsAttribute("XMUN")]
        [DataMember]
        public string xMun
        {
            get
            {
                return this._xMun;
            }
            set
            {
                this._xMun = value;
            }
        }

        [ParamsAttribute("UF")]
        [DataMember]
        public string UF
        {
            get
            {
                return this._UF;
            }
            set
            {
                this._UF = value;
            }
        }

        [ParamsAttribute("CEP")]
        [DataMember]
        public string CEP
        {
            get
            {
                return this._CEP;
            }
            set
            {
                this._CEP = value;
            }
        }

        [ParamsAttribute("CPAIS")]
        [DataMember]
        public int cPais
        {
            get
            {
                return this._cPais;
            }
            set
            {
                this._cPais = value;
            }
        }

        [ParamsAttribute("XPAIS")]
        [DataMember]
        public string xPais
        {
            get
            {
                return this._xPais;
            }
            set
            {
                this._xPais = value;
            }
        }

        [ParamsAttribute("FONE")]
        [DataMember]
        public string fone
        {
            get
            {
                return this._fone;
            }
            set
            {
                this._fone = value;
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

        [ParamsAttribute("IEST")]
        [DataMember]
        public string IEST
        {
            get
            {
                return this._IEST;
            }
            set
            {
                this._IEST = value;
            }
        }

        [ParamsAttribute("IM")]
        [DataMember]
        public string IM
        {
            get
            {
                return this._IM;
            }
            set
            {
                this._IM = value;
            }
        }

        [ParamsAttribute("CNAE")]
        [DataMember]
        public string CNAE
        {
            get
            {
                return this._CNAE;
            }
            set
            {
                this._CNAE = value;
            }
        }

        [ParamsAttribute("CRT")]
        [DataMember]
        public int CRT
        {
            get
            {
                return this._CRT;
            }
            set
            {
                this._CRT = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEEMIT WHERE IDOUTBOX = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox))
                {
                    sSql = @"UPDATE ZNFEEMIT SET CNPJ = ?, CPF = ?, XNOME = ?, XFANT = ?, XLGR = ?, NRO = ?, XCPL = ?, XBAIRRO = ?, CMUN = ?, XMUN = ?, UF = ?, CEP = ?, 
                                CPAIS = ?, XPAIS = ?, FONE = ?, IE = ?, IEST = ?, IM = ?, CNAE = ?, CRT = ? WHERE IDOUTBOX = ?";
                    _conn.ExecQuery(sSql, this.CNPJ, this.CPF, this.xNome, this.xFant, this.xLgr, this.nro, this.xCpl, this.xBairro, this.cMun, this.xMun,
                        this.UF, this.CEP, this.cPais, this.xPais, this.fone, this.IE, this.IEST, this.IM, this.CNAE, this.CRT, this.IdOutbox);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEEMIT (IDOUTBOX, CNPJ, CPF, XNOME, XFANT, XLGR, NRO, XCPL, XBAIRRO, CMUN, XMUN, UF, CEP, CPAIS, XPAIS, FONE, IE, IEST, IM, CNAE, CRT) 
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.CNPJ, this.CPF, this.xNome, this.xFant, this.xLgr, this.nro, this.xCpl, this.xBairro, this.cMun, this.xMun,
                        this.UF, this.CEP, this.cPais, this.xPais, this.fone, this.IE, this.IEST, this.IM, this.CNAE, this.CRT);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeEmit.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeEmit ReadByIDOutbox(params object[] parameters)
        {
            NFeEmit _nfeEmit = new NFeEmit();

            try
            {
                string sSql = @"SELECT * FROM ZNFEEMIT WHERE IDOUTBOX = ?";
                _nfeEmit.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeEmit;
        }
    }
}
