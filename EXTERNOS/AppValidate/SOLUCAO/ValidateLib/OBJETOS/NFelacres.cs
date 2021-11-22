using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFelacres : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private string _nLacre;

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

        [ParamsAttribute("NLACRE")]
        [DataMember]
        public string nLacre
        {
            get
            {
                return this._nLacre;
            }
            set
            {
                this._nLacre = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFELACRES WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFELACRES SET NLACRE = ? WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.nLacre, this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFELACRES (IDOUTBOX, NITEM, NLACRE)
                                VALUES (?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.nLacre);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFelacres.Save", err);
                throw new Exception(err);
            }
        }

        public static NFelacres ReadNFelacres(params object[] parameters)
        {
            NFelacres _nfelacres = new NFelacres();

            try
            {
                string sSql = @"SELECT * FROM ZNFELACRES WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfelacres.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfelacres;
        }
    }
}
