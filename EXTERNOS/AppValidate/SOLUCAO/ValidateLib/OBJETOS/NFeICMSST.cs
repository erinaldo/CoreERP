using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeICMSST : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private int _orig;
        private int _CST;
        private decimal _vBCSTRet;
        private decimal _vICMSSTRet;
        private decimal _vBCSTDest;
        private decimal _vICMSSTDest;

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

        [ParamsAttribute("CST")]
        [DataMember]
        public int CST
        {
            get
            {
                return this._CST;
            }
            set
            {
                this._CST = value;
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

        [ParamsAttribute("VBCSTDEST")]
        [DataMember]
        public decimal vBCSTDest
        {
            get
            {
                return this._vBCSTDest;
            }
            set
            {
                this._vBCSTDest = value;
            }
        }

        [ParamsAttribute("VICMSSTDEST")]
        [DataMember]
        public decimal vICMSSTDest
        {
            get
            {
                return this._vICMSSTDest;
            }
            set
            {
                this._vICMSSTDest = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEICMSST WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEICMSST SET ORIG = ?, CST = ?, VBCSTRET = ?, VICMSSTRET = ?, VBCSTDEST = ?, VICMSSTDEST = ?
                                WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.orig, this.CST, this.vBCSTRet, this.vICMSSTRet, this.vBCSTDest, this.vICMSSTDest, this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEICMSST (IDOUTBOX, NITEM, ORIG, CST, VBCSTRET, VICMSSTRET, VBCSTDEST, VICMSSTDEST)
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.orig, this.CST, this.vBCSTRet, this.vICMSSTRet, this.vBCSTDest, this.vICMSSTDest);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeICMSST.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeICMSST ReadNFeICMSST(params object[] parameters)
        {
            NFeICMSST _nfeICMSST = new NFeICMSST();

            try
            {
                string sSql = @"SELECT * FROM ZNFEICMSST WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeICMSST.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeICMSST;
        }
    }
}