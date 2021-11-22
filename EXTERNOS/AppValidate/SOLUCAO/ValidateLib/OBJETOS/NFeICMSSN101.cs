using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeICMSSN101 : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private int _orig;
        private int _CSOSN;
        private decimal _pCredSN;
        private decimal _vCredICMSSN;

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

        [ParamsAttribute("NITEM")]
        [DataMember]
        public int nItem
        {
            get
            {
                return this._nItem;
            }
            set
            {
                this._nItem = value;
            }
        }

        [ParamsAttribute("ORIG")]
        [DataMember]
        public int orig
        {
            get
            {
                return this._orig;
            }
            set
            {
                this._orig = value;
            }
        }

        [ParamsAttribute("CSOSN")]
        [DataMember]
        public int CSOSN
        {
            get
            {
                return this._CSOSN;
            }
            set
            {
                this._CSOSN = value;
            }
        }

        [ParamsAttribute("PCREDSN")]
        [DataMember]
        public decimal pCredSN
        {
            get
            {
                return this._pCredSN;
            }
            set
            {
                this._pCredSN = value;
            }
        }

        [ParamsAttribute("VCREDICMSSN")]
        [DataMember]
        public decimal vCredICMSSN
        {
            get
            {
                return this._vCredICMSSN;
            }
            set
            {
                this._vCredICMSSN = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEICMSSN101 WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEICMSSN101 SET ORIG = ?, CSOSN = ?, PCREDSN = ?, VCREDICMSSN = ? WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.orig, this.CSOSN, this.pCredSN, this.vCredICMSSN, this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEICMSSN101 (IDOUTBOX, NITEM, ORIG, CSOSN, PCREDSN, VCREDICMSSN)
                                VALUES (?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.orig, this.CSOSN, this.pCredSN, this.vCredICMSSN);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeICMSSN101.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeICMSSN101 ReadNFeICMSSN101(params object[] parameters)
        {
            NFeICMSSN101 _nfeICMSSN101 = new NFeICMSSN101();

            try
            {
                string sSql = @"SELECT * FROM ZNFEICMSSN101 WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeICMSSN101.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeICMSSN101;
        }
    }
}