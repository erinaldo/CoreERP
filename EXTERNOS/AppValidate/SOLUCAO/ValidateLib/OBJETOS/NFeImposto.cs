using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeImposto : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private decimal _vTotTrib;
        private NFeICMS00 _nfeICMS00;
        private NFeICMS10 _nfeICMS10;
        private NFeICMS20 _nfeICMS20;
        private NFeICMS30 _nfeICMS30;
        private NFeICMS40 _nfeICMS40;
        private NFeICMS51 _nfeICMS51;
        private NFeICMS60 _nfeICMS60;
        private NFeICMS70 _nfeICMS70;
        private NFeICMS90 _nfeICMS90;
        private NFeICMSPart _nfeICMSPart;
        private NFeICMSSN101 _nfeICMSSN101;
        private NFeICMSSN102 _nfeICMSSN102;
        private NFeICMSSN201 _nfeICMSSN201;
        private NFeICMSSN202 _nfeICMSSN202;
        private NFeICMSSN500 _nfeICMSSN500;
        private NFeICMSSN900 _nfeICMSSN900;
        private NFeICMSST _nfeICMSST;
        private NFeIPI _nfeIPI;
        private NFeII _nfeII;
        private NFePIS _nfePIS;
        private NFeCOFINS _nfeCOFINS;
        private NFeISSQN _nfeISSQN;

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

        [ParamsAttribute("VTOTTRIB")]
        [DataMember]
        public decimal vTotTrib
        {
            get
            {
                return this._vTotTrib;
            }
            set
            {
                this._vTotTrib = value;
            }
        }

        public NFeICMS00 nfeICMS00
        {
            get
            {
                return this._nfeICMS00;
            }
            set
            {
                this._nfeICMS00 = value;
            }
        }

        public NFeICMS10 nfeICMS10
        {
            get
            {
                return this._nfeICMS10;
            }
            set
            {
                this._nfeICMS10 = value;
            }
        }

        public NFeICMS20 nfeICMS20
        {
            get
            {
                return this._nfeICMS20;
            }
            set
            {
                this._nfeICMS20 = value;
            }
        }

        public NFeICMS30 nfeICMS30
        {
            get
            {
                return this._nfeICMS30;
            }
            set
            {
                this._nfeICMS30 = value;
            }
        }

        public NFeICMS40 nfeICMS40
        {
            get
            {
                return this._nfeICMS40;
            }
            set
            {
                this._nfeICMS40 = value;
            }
        }

        public NFeICMS51 nfeICMS51
        {
            get
            {
                return this._nfeICMS51;
            }
            set
            {
                this._nfeICMS51 = value;
            }
        }

        public NFeICMS60 nfeICMS60
        {
            get
            {
                return this._nfeICMS60;
            }
            set
            {
                this._nfeICMS60 = value;
            }
        }

        public NFeICMS70 nfeICMS70
        {
            get
            {
                return this._nfeICMS70;
            }
            set
            {
                this._nfeICMS70 = value;
            }
        }

        public NFeICMS90 nfeICMS90
        {
            get
            {
                return this._nfeICMS90;
            }
            set
            {
                this._nfeICMS90 = value;
            }
        }

        public NFeICMSPart nfeICMSPart
        {
            get
            {
                return this._nfeICMSPart;
            }
            set
            {
                this._nfeICMSPart = value;
            }
        }

        public NFeICMSSN101 nfeICMSSN101
        {
            get
            {
                return this._nfeICMSSN101;
            }
            set
            {
                this._nfeICMSSN101 = value;
            }
        }

        public NFeICMSSN102 nfeICMSSN102
        {
            get
            {
                return this._nfeICMSSN102;
            }
            set
            {
                this._nfeICMSSN102 = value;
            }
        }

        public NFeICMSSN201 nfeICMSSN201
        {
            get
            {
                return this._nfeICMSSN201;
            }
            set
            {
                this._nfeICMSSN201 = value;
            }
        }

        public NFeICMSSN202 nfeICMSSN202
        {
            get
            {
                return this._nfeICMSSN202;
            }
            set
            {
                this._nfeICMSSN202 = value;
            }
        }

        public NFeICMSSN500 nfeICMSSN500
        {
            get
            {
                return this._nfeICMSSN500;
            }
            set
            {
                this._nfeICMSSN500 = value;
            }
        }

        public NFeICMSSN900 nfeICMSSN900
        {
            get
            {
                return this._nfeICMSSN900;
            }
            set
            {
                this._nfeICMSSN900 = value;
            }
        }

        public NFeICMSST nfeICMSST
        {
            get
            {
                return this._nfeICMSST;
            }
            set
            {
                this._nfeICMSST = value;
            }
        }

        public NFeIPI nfeIPI
        {
            get
            {
                return this._nfeIPI;
            }
            set
            {
                this._nfeIPI = value;
            }
        }

        public NFeII nfeII
        {
            get
            {
                return this._nfeII;
            }
            set
            {
                this._nfeII = value;
            }
        }

        public NFePIS nfePIS
        {
            get
            {
                return this._nfePIS;
            }
            set
            {
                this._nfePIS = value;
            }
        }

        public NFeCOFINS nfeCOFINS
        {
            get
            {
                return this._nfeCOFINS;
            }
            set
            {
                this._nfeCOFINS = value;
            }
        }

        public NFeISSQN nfeISSQN
        {
            get
            {
                return this._nfeISSQN;
            }
            set
            {
                this._nfeISSQN = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEIMPOSTO WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEIMPOSTO SET VTOTTRIB = ? WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.vTotTrib, this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEIMPOSTO (IDOUTBOX, NITEM, VTOTTRIB)
                                VALUES (?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.vTotTrib);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeImposto.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeImposto ReadNFeImposto(params object[] parameters)
        {
            NFeImposto _nfeImposto = new NFeImposto();

            try
            {
                string sSql = @"SELECT * FROM ZNFEIMPOSTO WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeImposto.ReadFromCommand(sSql, parameters);
                _nfeImposto.nfeICMS00 = NFeICMS00.ReadNFeICMS00(parameters);
                _nfeImposto.nfeICMS10 = NFeICMS10.ReadNFeICMS10(parameters);
                _nfeImposto.nfeICMS20 = NFeICMS20.ReadNFeICMS20(parameters);
                _nfeImposto.nfeICMS30 = NFeICMS30.ReadNFeICMS30(parameters);
                _nfeImposto.nfeICMS40 = NFeICMS40.ReadNFeICMS40(parameters);
                _nfeImposto.nfeICMS51 = NFeICMS51.ReadNFeICMS51(parameters);
                _nfeImposto.nfeICMS60 = NFeICMS60.ReadNFeICMS60(parameters);
                _nfeImposto.nfeICMS70 = NFeICMS70.ReadNFeICMS70(parameters);
                _nfeImposto.nfeICMS90 = NFeICMS90.ReadNFeICMS90(parameters);
                _nfeImposto.nfeICMSPart = NFeICMSPart.ReadNFeICMSPart(parameters);
                _nfeImposto.nfeICMSSN101 = NFeICMSSN101.ReadNFeICMSSN101(parameters);
                _nfeImposto.nfeICMSSN102 = NFeICMSSN102.ReadNFeICMSSN102(parameters);
                _nfeImposto.nfeICMSSN201 = NFeICMSSN201.ReadNFeICMSSN201(parameters);
                _nfeImposto.nfeICMSSN202 = NFeICMSSN202.ReadNFeICMSSN202(parameters);
                _nfeImposto.nfeICMSSN500 = NFeICMSSN500.ReadNFeICMSSN500(parameters);
                _nfeImposto.nfeICMSSN900 = NFeICMSSN900.ReadNFeICMSSN900(parameters);
                _nfeImposto.nfeICMSST = NFeICMSST.ReadNFeICMSST(parameters);
                _nfeImposto.nfeIPI = NFeIPI.ReadNFeIPI(parameters);
                _nfeImposto.nfeII = NFeII.ReadNFeII(parameters);
                _nfeImposto.nfePIS = NFePIS.ReadNFePIS(parameters);
                _nfeImposto.nfeCOFINS = NFeCOFINS.ReadNFeCOFINS(parameters);
                _nfeImposto.nfeISSQN = NFeISSQN.ReadNFeISSQN(parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeImposto;
        }
    }
}
