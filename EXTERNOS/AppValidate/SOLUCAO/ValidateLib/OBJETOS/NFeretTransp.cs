using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeretTransp : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private decimal _vServ;
        private decimal _vBCRet;
        private decimal _pICMSRet;
        private decimal _vICMSRet;
        private string _CFOP;
        private string _cMunFG;

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

        [ParamsAttribute("VSERV")]
        [DataMember]
        public decimal vServ
        {
            get
            {
                return this._vServ;
            }
            set
            {
                this._vServ = value;
            }
        }

        [ParamsAttribute("VBCRET")]
        [DataMember]
        public decimal vBCRet
        {
            get
            {
                return this._vBCRet;
            }
            set
            {
                this._vBCRet = value;
            }
        }

        [ParamsAttribute("PICMSRET")]
        [DataMember]
        public decimal pICMSRet
        {
            get
            {
                return this._pICMSRet;
            }
            set
            {
                this._pICMSRet = value;
            }
        }

        [ParamsAttribute("VICMSRET")]
        [DataMember]
        public decimal vICMSRet
        {
            get
            {
                return this._vICMSRet;
            }
            set
            {
                this._vICMSRet = value;
            }
        }

        [ParamsAttribute("CFOP")]
        [DataMember]
        public string CFOP
        {
            get
            {
                return this._CFOP;
            }
            set
            {
                this._CFOP = value;
            }
        }

        [ParamsAttribute("CMUNFG")]
        [DataMember]
        public string cMunFG
        {
            get
            {
                return this._cMunFG;
            }
            set
            {
                this._cMunFG = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFERETTRANSP WHERE IDOUTBOX = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox))
                {
                    sSql = @"UPDATE ZNFERETTRANSP SET VSERV = ?, VBCRET = ?, PICMSRET = ?, VICMSRET = ?, CFOP = ?, CMUNFG = ? WHERE IDOUTBOX = ?";
                    _conn.ExecQuery(sSql, this.vServ, this.vBCRet, this.pICMSRet, this.vICMSRet, this.CFOP, this.cMunFG, this.IdOutbox);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFERETTRANSP (IDOUTBOX, VSERV, VBCRET, PICMSRET, VICMSRET, CFOP, CMUNFG) 
                                VALUES (?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.vServ, this.vBCRet, this.pICMSRet, this.vICMSRet, this.CFOP, this.cMunFG);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeretTransp.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeretTransp ReadNFeretTransp(params object[] parameters)
        {
            NFeretTransp _nferetTransp = new NFeretTransp();

            try
            {
                string sSql = @"SELECT * FROM ZNFERETTRANSP WHERE IDOUTBOX = ?";
                _nferetTransp.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nferetTransp;
        }
    }
}
