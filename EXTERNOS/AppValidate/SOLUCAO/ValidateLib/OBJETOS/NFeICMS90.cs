﻿using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeICMS90 : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private int _orig;
        private string _CST;
        private int _modBC;
        private decimal _vBC;
        private decimal _pRedBC;
        private decimal _pICMS;
        private decimal _vICMS;
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
                sSql = @"SELECT IDOUTBOX FROM ZNFEICMS90 WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEICMS90 SET ORIG = ?, CST = ?, MODBC = ?, VBC = ?, PREDBC = ?, PICMS = ?, VICMS = ?, MODBCST = ?, PMVAST = ?, PREDBCST = ?, VBCST = ?, 
                                PICMSST = ?, VICMSST = ?, VICMSDESON = ?, MOTDESICMS = ? WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.orig, this.CST, this.modBC, this.vBC, this.pRedBC, this.pICMS, this.vICMS, this.modBCST, this.pMVAST, this.pRedBCST, this.vBCST,
                        this.pICMSST, this.vICMSST, this.vICMSDeson, this.motDesICMS, this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEICMS90 (IDOUTBOX, NITEM, ORIG, CST, MODBC, VBC, PREDBC, PICMS, VICMS, MODBCST, PMVAST, PREDBCST, VBCST, PICMSST, VICMSST, 
                                VICMSDESON, MOTDESICMS)
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.orig, this.CST, this.modBC, this.vBC, this.pRedBC, this.pICMS, this.vICMS, this.modBCST, this.pMVAST, 
                        this.pRedBCST, this.vBCST, this.pICMSST, this.vICMSST, this.vICMSDeson, this.motDesICMS);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeICMS90.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeICMS90 ReadNFeICMS90(params object[] parameters)
        {
            NFeICMS90 _nfeICMS90 = new NFeICMS90();

            try
            {
                string sSql = @"SELECT * FROM ZNFEICMS90 WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeICMS90.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeICMS90;
        }
    }
}