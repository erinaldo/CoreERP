using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeICMSSN500 : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private int _orig;
        private int _CSOSN;
        private decimal _vBCSTRet;
        private decimal _vICMSSTRet;

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

        [ParamsAttribute("VBCSTRET")]
        [DataMember]
        public decimal vBCSTRet
        {
            get
            {
                return this._vBCSTRet;
            }
            set
            {
                this._vBCSTRet = value;
            }
        }

        [ParamsAttribute("VICMSSTRET")]
        [DataMember]
        public decimal vICMSSTRet
        {
            get
            {
                return this._vICMSSTRet;
            }
            set
            {
                this._vICMSSTRet = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEICMSSN500 WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEICMSSN500 SET ORIG = ?, CSOSN = ?, VBCSTRET = ?, VICMSSTRET = ?
                                WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.orig, this.CSOSN, this.vBCSTRet, this.vICMSSTRet,
                        this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEICMSSN500 (IDOUTBOX, NITEM, ORIG, CSOSN, VBCSTRET, VICMSSTRET)
                                VALUES (?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.orig, this.CSOSN, this.vBCSTRet, this.vICMSSTRet);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeICMSSN500.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeICMSSN500 ReadNFeICMSSN500(params object[] parameters)
        {
            NFeICMSSN500 _nfeICMSSN500 = new NFeICMSSN500();

            try
            {
                string sSql = @"SELECT * FROM ZNFEICMSSN500 WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeICMSSN500.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeICMSSN500;
        }
    }
}