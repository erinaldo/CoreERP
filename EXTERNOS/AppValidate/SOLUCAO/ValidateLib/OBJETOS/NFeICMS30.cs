using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeICMS30 : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private int _orig;
        private string _CST;
        private int _modBCST;
        private decimal _pMVAST;
        private decimal _pRedBCST;
        private decimal _vBCST;
        private decimal _pICMSST;
        private decimal _vICMSST;
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

        [ParamsAttribute("MODBCST")]
        [DataMember]
        public int modBCST
        {
            get
            {
                return this._modBCST;
            }
            set
            {
                this._modBCST = value;
            }
        }

        [ParamsAttribute("PMVAST")]
        [DataMember]
        public decimal pMVAST
        {
            get
            {
                return this._pMVAST;
            }
            set
            {
                this._pMVAST = value;
            }
        }

        [ParamsAttribute("PREDBCST")]
        [DataMember]
        public decimal pRedBCST
        {
            get
            {
                return this._pRedBCST;
            }
            set
            {
                this._pRedBCST = value;
            }
        }

        [ParamsAttribute("VBCST")]
        [DataMember]
        public decimal vBCST
        {
            get
            {
                return this._vBCST;
            }
            set
            {
                this._vBCST = value;
            }
        }

        [ParamsAttribute("PICMSST")]
        [DataMember]
        public decimal pICMSST
        {
            get
            {
                return this._pICMSST;
            }
            set
            {
                this._pICMSST = value;
            }
        }

        [ParamsAttribute("VICMSST")]
        [DataMember]
        public decimal vICMSST
        {
            get
            {
                return this._vICMSST;
            }
            set
            {
                this._vICMSST = value;
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
                sSql = @"SELECT IDOUTBOX FROM ZNFEICMS30 WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEICMS30 SET ORIG = ?, CST = ?, MODBCST = ?, PMVAST = ?, PREDBCST = ?, VBCST = ?, PICMSST = ?, VICMSST = ?, VICMSDESON = ?, MOTDESICMS = ?
                                WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.orig, this.CST, this.modBCST, this.pMVAST, this.pRedBCST, this.vBCST, this.pICMSST, this.vICMSST, this.vICMSDeson, this.motDesICMS,
                        this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEICMS30 (IDOUTBOX, NITEM, ORIG, CST, MODBCST, PMVAST, PREDBCST, VBCST, PICMSST, VICMSST, VICMSDESON, MOTDESICMS)
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.orig, this.CST, this.modBCST, this.pMVAST, this.pRedBCST, this.vBCST, this.pICMSST, this.vICMSST,
                        this.vICMSDeson, this.motDesICMS);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeICMS30.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeICMS30 ReadNFeICMS30(params object[] parameters)
        {
            NFeICMS30 _nfeICMS30 = new NFeICMS30();

            try
            {
                string sSql = @"SELECT * FROM ZNFEICMS30 WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeICMS30.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeICMS30;
        }
    }
}
