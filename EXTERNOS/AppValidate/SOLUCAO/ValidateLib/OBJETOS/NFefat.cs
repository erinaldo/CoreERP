using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFefat : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private string _nFat;
        private decimal _vOrig;
        private decimal _vDesc;
        private decimal _vLiq;

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

        [ParamsAttribute("NFAT")]
        [DataMember]
        public string nFat
        {
            get
            {
                return this._nFat;
            }
            set
            {
                this._nFat = value;
            }
        }

        [ParamsAttribute("VORIG")]
        [DataMember]
        public decimal vOrig
        {
            get
            {
                return this._vOrig;
            }
            set
            {
                this._vOrig = value;
            }
        }

        [ParamsAttribute("VDESC")]
        [DataMember]
        public decimal vDesc
        {
            get
            {
                return this._vDesc;
            }
            set
            {
                this._vDesc = value;
            }
        }

        [ParamsAttribute("VLIQ")]
        [DataMember]
        public decimal vLiq
        {
            get
            {
                return this._vLiq;
            }
            set
            {
                this._vLiq = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEFAT WHERE IDOUTBOX = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox))
                {
                    sSql = @"UPDATE ZNFEFAT SET NFAT = ?, VORIG = ?, VDESC = ?, VLIQ = ? WHERE IDOUTBOX = ?";
                    _conn.ExecQuery(sSql, this.nFat, this.vOrig, this.vDesc, this.vLiq, this.IdOutbox);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEFAT (IDOUTBOX, NFAT, VORIG, VDESC, VLIQ) 
                                VALUES (?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nFat, this.vOrig, this.vDesc, this.vLiq);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFefat.Save", err);
                throw new Exception(err);
            }
        }

        public static NFefat ReadNFefat(params object[] parameters)
        {
            NFefat _nfefat = new NFefat();

            try
            {
                string sSql = @"SELECT * FROM ZNFEFAT WHERE IDOUTBOX = ?";
                _nfefat.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfefat;
        }
    }
}
