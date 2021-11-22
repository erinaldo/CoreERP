using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeProd : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        public int CODOPER;

        private int _idOutbox;
        private int _nItem;
        private string _cProd;
        private string _cEAN;
        private string _xProd;
        private string _NCM;
        private string _NVE;
        private int? _EXTIPI;
        private int _CFOP;
        private string _uCom;
        private decimal _qCom;
        private decimal _vUnCom;
        private decimal _vProd;
        private string _cEANTrib;
        private string _uTrib;
        private decimal _qTrib;
        private decimal _vUnTrib;
        private decimal _vFrete;
        private decimal _vSeg;
        private decimal _vDesc;
        private decimal _vOutro;
        private int _indTot;
        private string _xPed;
        private string _nItemPed;
        private string _nFCI;
        private string _CEST;

        [ParamsAttribute("CEST")]
        [DataMember]
        public string CEST
        {
            get
            {
                return this._CEST;
            }
            set
            {
                this._CEST = value;
            }
        }

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

        [ParamsAttribute("CPROD")]
        [DataMember]
        public string cProd
        {
            get
            {
                return this._cProd;
            }
            set
            {
                this._cProd = value;
            }
        }

        [ParamsAttribute("CEAN")]
        [DataMember]
        public string cEAN
        {
            get
            {
                return this._cEAN;
            }
            set
            {
                this._cEAN = value;
            }
        }

        [ParamsAttribute("XPROD")]
        [DataMember]
        public string xProd
        {
            get
            {
                return this._xProd;
            }
            set
            {
                this._xProd = value;
            }
        }

        [ParamsAttribute("NCM")]
        [DataMember]
        public string NCM
        {
            get
            {
                return this._NCM;
            }
            set
            {
                this._NCM = value;
            }
        }

        [ParamsAttribute("NVE")]
        [DataMember]
        public string NVE
        {
            get
            {
                return this._NVE;
            }
            set
            {
                this._NVE = value;
            }
        }

        [ParamsAttribute("EXTIPI")]
        [DataMember]
        public int? EXTIPI
        {
            get
            {
                return this._EXTIPI;
            }
            set
            {
                this._EXTIPI = value;
            }
        }

        [ParamsAttribute("CFOP")]
        [DataMember]
        public int CFOP
        {
            get
            {
                return this._CFOP;
            }
            set
            {
                this._CFOP = value;
            }
        }

        [ParamsAttribute("UCOM")]
        [DataMember]
        public string uCom
        {
            get
            {
                return this._uCom;
            }
            set
            {
                this._uCom = value;
            }
        }

        [ParamsAttribute("QCOM")]
        [DataMember]
        public decimal qCom
        {
            get
            {
                return this._qCom;
            }
            set
            {
                this._qCom = value;
            }
        }

        [ParamsAttribute("VUNCOM")]
        [DataMember]
        public decimal vUnCom
        {
            get
            {
                return this._vUnCom;
            }
            set
            {
                this._vUnCom = value;
            }
        }

        [ParamsAttribute("VPROD")]
        [DataMember]
        public decimal vProd
        {
            get
            {
                return this._vProd;
            }
            set
            {
                this._vProd = value;
            }
        }

        [ParamsAttribute("CEANTRIB")]
        [DataMember]
        public string cEANTrib
        {
            get
            {
                return this._cEANTrib;
            }
            set
            {
                this._cEANTrib = value;
            }
        }

        [ParamsAttribute("UTRIB")]
        [DataMember]
        public string uTrib
        {
            get
            {
                return this._uTrib;
            }
            set
            {
                this._uTrib = value;
            }
        }

        [ParamsAttribute("QTRIB")]
        [DataMember]
        public decimal qTrib
        {
            get
            {
                return this._qTrib;
            }
            set
            {
                this._qTrib = value;
            }
        }

        [ParamsAttribute("VUNTRIB")]
        [DataMember]
        public decimal vUnTrib
        {
            get
            {
                return this._vUnTrib;
            }
            set
            {
                this._vUnTrib = value;
            }
        }

        [ParamsAttribute("VFRETE")]
        [DataMember]
        public decimal vFrete
        {
            get
            {
                return this._vFrete;
            }
            set
            {
                this._vFrete = value;
            }
        }

        [ParamsAttribute("VSEG")]
        [DataMember]
        public decimal vSeg
        {
            get
            {
                return this._vSeg;
            }
            set
            {
                this._vSeg = value;
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

        [ParamsAttribute("VOUTRO")]
        [DataMember]
        public decimal vOutro
        {
            get
            {
                return this._vOutro;
            }
            set
            {
                this._vOutro = value;
            }
        }

        [ParamsAttribute("INDTOT")]
        [DataMember]
        public int indTot
        {
            get
            {
                return this._indTot;
            }
            set
            {
                this._indTot = value;
            }
        }

        [ParamsAttribute("XPED")]
        [DataMember]
        public string xPed
        {
            get
            {
                return this._xPed;
            }
            set
            {
                this._xPed = value;
            }
        }

        [ParamsAttribute("NITEMPED")]
        [DataMember]
        public string nItemPed
        {
            get
            {
                return this._nItemPed;
            }
            set
            {
                this._nItemPed = value;
            }
        }

        [ParamsAttribute("NFCI")]
        [DataMember]
        public string nFCI
        {
            get
            {
                return this._nFCI;
            }
            set
            {
                this._nFCI = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEPROD WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
    GOPERITEM.NSEQITEM
    ,GOPERITEM.RATEIODESCONTO
    ,GOPERITEM.RATEIODESPESA
    ,GOPERITEM.RATEIOFRETE
    ,GOPERITEM.RATEIOSEGURO

FROM
GOPERITEM

WHERE
    GOPERITEM.CODEMPRESA = ?
AND GOPERITEM.CODOPER = ?
AND GOPERITEM.NSEQITEM = ?

", new object[] { AppLib.Context.Empresa, CODOPER, nItem });

                    sSql = @"UPDATE ZNFEPROD SET CPROD = ?, CEAN = ?, XPROD = ?, NCM = ?, NVE = ?, EXTIPI = ?, CFOP = ?, UCOM = ?, QCOM = ?, VUNCOM = ?, VPROD = ?, CEANTRIB = ?,
                                UTRIB = ?, QTRIB = ?, VUNTRIB = ?, VFRETE = ?, VSEG = ?, VDESC = ?, VOUTRO = ?, INDTOT = ?, XPED = ?, NITEMPED = ?, NFCI = ?, CEST = ?
                                WHERE IDOUTBOX = ? AND NITEM = ?";

                    _conn.ExecQuery(sSql, new object[]{ this.cProd, this.cEAN, this.xProd, this.NCM, this.NVE, this.EXTIPI, this.CFOP, this.uCom, this.qCom, this.vUnCom, this.vProd,
                        this.cEANTrib, this.uTrib, this.qTrib, this.vUnTrib, this.vFrete, this.vSeg, /*this.vDesc*/  Convert.ToDecimal(dt.Rows[0]["RATEIODESCONTO"]), this.vOutro, this.indTot, this.xPed, this.nItemPed,
                        this.nFCI, this.CEST, this.IdOutbox, this.nItem});



                }
                else
                {
                    System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
    GOPERITEM.NSEQITEM
    ,GOPERITEM.RATEIODESCONTO
    ,GOPERITEM.RATEIODESPESA
    ,GOPERITEM.RATEIOFRETE
    ,GOPERITEM.RATEIOSEGURO

FROM
GOPERITEM

WHERE
    GOPERITEM.CODEMPRESA = ?
AND GOPERITEM.CODOPER = ?
AND GOPERITEM.NSEQITEM = ?

", new object[] { AppLib.Context.Empresa, CODOPER, nItem });

                    sSql = @"INSERT INTO ZNFEPROD (IDOUTBOX, NITEM, CPROD, CEAN, XPROD, NCM, NVE, EXTIPI, CFOP, UCOM, QCOM, VUNCOM, VPROD, CEANTRIB, UTRIB, QTRIB, VUNTRIB, 
                                VFRETE, VSEG, VDESC, VOUTRO, INDTOT, XPED, NITEMPED, NFCI, CEST) 
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, cProd, this.cEAN, this.xProd, this.NCM, this.NVE, this.EXTIPI, this.CFOP, this.uCom, this.qCom,
                        this.vUnCom, this.vProd, this.cEANTrib, this.uTrib, this.qTrib, this.vUnTrib, this.vFrete, this.vSeg,/* this.vDesc*/ Convert.ToDecimal(dt.Rows[0]["RATEIODESCONTO"]), this.vOutro,
                        this.indTot, this.xPed, this.nItemPed, this.nFCI, this.CEST);
                }

            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeProd.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeProd ReadNFeProd(params object[] parameters)
        {
            NFeProd _nfeProd = new NFeProd();

            try
            {
                string sSql = @"SELECT * FROM ZNFEPROD WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeProd.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeProd;
        }
    }
}
