using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeEntrega : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private string _CNPJ;
        private string _CPF;
        private string _xLgr;
        private string _nro;
        private string _xCpl;
        private string _xBairro;
        private string _cMun;
        private string _xMun;
        private string _UF;

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

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEENTREGA WHERE IDOUTBOX = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox))
                {
                    sSql = @"UPDATE ZNFEENTREGA SET CNPJ = ?, CPF = ?, XLGR = ?, NRO = ?, XCPL = ?, XBAIRRO = ?, CMUN = ?, XMUN = ?, UF = ? WHERE IDOUTBOX = ?";
                    _conn.ExecQuery(sSql, this.CNPJ, this.CPF, this.xLgr, this.nro, this.xLgr, this.xBairro, this.cMun, this.xMun, this.UF, this.IdOutbox);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEENTREGA (IDOUTBOX, CNPJ, CPF, XLGR, NRO, XCPL, XBAIRRO, CMUN, XMUN, UF) 
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.CNPJ, this.CPF, this.xLgr, this.nro, this.xLgr, this.xBairro, this.cMun, this.xMun, this.UF);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeEntrega.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeEntrega ReadByIDOutbox(params object[] parameters)
        {
            NFeEntrega _nfeEntrega = new NFeEntrega();

            try
            {
                string sSql = @"SELECT * FROM ZNFEENTREGA WHERE IDOUTBOX = ?";
                _nfeEntrega.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeEntrega;
        }

    }
}