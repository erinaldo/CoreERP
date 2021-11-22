using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeICMS20 : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private int _orig;
        private string _CST;
        private int _modBC;
        private decimal _pRedBC;
        private decimal _vBC;
        private decimal _pICMS;
        private decimal _vICMS;
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

        [ParamsAttribute("MODBC")]
        [DataMember]
        public int modBC
        {
            get
            {
                return this._modBC;
            }
            set
            {
                this._modBC = value;
            }
        }

        [ParamsAttribute("PREDBC")]
        [DataMember]
        public decimal pRedBC
        {
            get
            {
                return this._pRedBC;
            }
            set
            {
                this._pRedBC = value;
            }
        }

        [ParamsAttribute("VBC")]
        [DataMember]
        public decimal vBC
        {
            get
            {
                return this._vBC;
            }
            set
            {
                this._vBC = value;
            }
        }

        [ParamsAttribute("PICMS")]
        [DataMember]
        public decimal pICMS
        {
            get
            {
                return this._pICMS;
            }
            set
            {
                this._pICMS = value;
            }
        }

        [ParamsAttribute("VICMS")]
        [DataMember]
        public decimal vICMS
        {
            get
            {
                return this._vICMS;
            }
            set
            {
                this._vICMS = value;
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
                sSql = @"SELECT IDOUTBOX FROM ZNFEICMS20 WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEICMS20 SET ORIG = ?, CST = ?, MODBC = ?, PREDBC = ?, VBC = ?, PICMS = ?, VICMS = ?, VICMSDESON = ?, MOTDESICMS = ?
                                WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.orig, this.CST, this.modBC, this.pRedBC, this.vBC, this.pICMS, this.vICMS, this.vICMSDeson, this.motDesICMS,
                        this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEICMS20 (IDOUTBOX, NITEM, ORIG, CST, MODBC, PREDBC, VBC, PICMS, VICMS, VICMSDESON, MOTDESICMS) 
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.orig, this.CST, this.modBC, this.pRedBC, this.vBC, this.pICMS, this.vICMS, this.vICMSDeson, 
                        this.motDesICMS);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeICMS20.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeICMS20 ReadNFeICMS20(params object[] parameters)
        {
            NFeICMS20 _nfeICMS20 = new NFeICMS20();

            try
            {
                string sSql = @"SELECT * FROM ZNFEICMS20 WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeICMS20.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeICMS20;
        }
    }
}
