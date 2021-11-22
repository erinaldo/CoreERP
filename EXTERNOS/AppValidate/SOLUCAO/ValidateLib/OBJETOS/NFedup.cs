using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFedup : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private string _nDup;
        private string _dVenc;
        private decimal _vDup;

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

        [ParamsAttribute("NDUP")]
        [DataMember]
        public string nDup
        {
            get
            {
                return this._nDup;
            }
            set
            {
                this._nDup = value;
            }
        }

        [ParamsAttribute("DVENC")]
        [DataMember]
        public string dVenc
        {
            get
            {
                return this._dVenc;
            }
            set
            {
                this._dVenc = value;
            }
        }

        [ParamsAttribute("VDUP")]
        [DataMember]
        public decimal vDup
        {
            get
            {
                return this._vDup;
            }
            set
            {
                this._vDup = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEDUP WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEDUP SET NDUP = ?, DVENC = ?, VDUP = ? WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.nDup, this.dVenc, this.vDup, this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEDUP (IDOUTBOX, NITEM, NDUP, DVENC, VDUP) VALUES (?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.nDup, this.dVenc, this.vDup);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFedup.Save", err);
                throw new Exception(err);
            }
        }

        public static NFedup ReadNFedup(params object[] parameters)
        {
            NFedup _nfedup = new NFedup();

            try
            {
                string sSql = @"SELECT * FROM ZNFEDUP WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfedup.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfedup;
        }
    }
}
