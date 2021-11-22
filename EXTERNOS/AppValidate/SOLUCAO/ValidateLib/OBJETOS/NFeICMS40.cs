using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeICMS40 : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private int _orig;
        private string _CST;
        private decimal _vICMSDeson;
        private int _motDesICMS;

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
        public string CST
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

        [ParamsAttribute("VICMSDESON")]
        [DataMember]
        public decimal vICMSDeson
        {
            get
            {
                return this._vICMSDeson;
            }
            set
            {
                this._vICMSDeson = value;
            }
        }

        [ParamsAttribute("MOTDESICMS")]
        [DataMember]
        public int motDesICMS
        {
            get
            {
                return this._motDesICMS;
            }
            set
            {
                this._motDesICMS = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEICMS40 WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEICMS40 SET ORIG = ?, CST = ?, VICMSDESON = ?, MOTDESICMS = ? WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.orig, this.CST, this.vICMSDeson, this.motDesICMS, this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEICMS40 (IDOUTBOX, NITEM, ORIG, CST, VICMSDESON, MOTDESICMS)
                                VALUES (?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.orig, this.CST, this.vICMSDeson, this.motDesICMS);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeICMS40.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeICMS40 ReadNFeICMS40(params object[] parameters)
        {
            NFeICMS40 _nfeICMS40 = new NFeICMS40();

            try
            {
                string sSql = @"SELECT * FROM ZNFEICMS40 WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeICMS40.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeICMS40;
        }
    }
}
