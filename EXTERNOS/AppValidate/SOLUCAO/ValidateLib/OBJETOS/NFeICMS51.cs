using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeICMS51 : ParamsBase
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
        private decimal _vICMSOp;
        private decimal _pDif;
        private decimal _vICMSDif;
        private decimal _vICMS;

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

        [ParamsAttribute("VICMSOP")]
        [DataMember]
        public decimal vICMSOp
        {
            get
            {
                return this._vICMSOp;
            }
            set
            {
                this._vICMSOp = value;
            }
        }

        [ParamsAttribute("PDIF")]
        [DataMember]
        public decimal pDif
        {
            get
            {
                return this._pDif;
            }
            set
            {
                this._pDif = value;
            }
        }

        [ParamsAttribute("VICMSDIF")]
        [DataMember]
        public decimal vICMSDif
        {
            get
            {
                return this._vICMSDif;
            }
            set
            {
                this._vICMSDif = value;
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

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEICMS51 WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEICMS51 SET ORIG = ?, CST = ?, MODBC = ?, PREDBC = ?, VBC = ?, PICMS = ?, VICMSOP = ?, PDIF = ?, VICMSDIF = ?, VICMS = ?
                                WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.orig, this.CST, this.modBC, this.pRedBC, this.vBC, this.pICMS, this.vICMSOp, this.pDif, this.vICMSDif, this.vICMS,
                        this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEICMS51 (IDOUTBOX, NITEM, ORIG, CST, MODBC, PREDBC, VBC, PICMS, VICMSOP, PDIF, VICMSDIF, VICMS)
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.orig, this.CST, this.modBC, this.pRedBC, this.vBC, this.pICMS, this.vICMSOp, this.pDif, 
                        this.vICMSDif, this.vICMS);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeICMS51.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeICMS51 ReadNFeICMS51(params object[] parameters)
        {
            NFeICMS51 _nfeICMS51 = new NFeICMS51();

            try
            {
                string sSql = @"SELECT * FROM ZNFEICMS51 WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeICMS51.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeICMS51;
        }
    }
}