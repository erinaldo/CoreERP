using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeretTrib : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private decimal _vRetPIS;
        private decimal _vRetCOFINS;
        private decimal _vRetCSLL;
        private decimal _vBCIRRF;
        private decimal _vIRRF;
        private decimal _vBCRetPrev;
        private decimal _vRetPrev;

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

        [ParamsAttribute("VRETPIS")]
        [DataMember]
        public decimal vRetPIS
        {
            get
            {
                return this._vRetPIS;
            }
            set
            {
                this._vRetPIS = value;
            }
        }

        [ParamsAttribute("VRETCOFINS")]
        [DataMember]
        public decimal vRetCOFINS
        {
            get
            {
                return this._vRetCOFINS;
            }
            set
            {
                this._vRetCOFINS = value;
            }
        }

        [ParamsAttribute("VRETCSLL")]
        [DataMember]
        public decimal vRetCSLL
        {
            get
            {
                return this._vRetCSLL;
            }
            set
            {
                this._vRetCSLL = value;
            }
        }

        [ParamsAttribute("VBCIRRF")]
        [DataMember]
        public decimal vBCIRRF
        {
            get
            {
                return this._vBCIRRF;
            }
            set
            {
                this._vBCIRRF = value;
            }
        }

        [ParamsAttribute("VIRRF")]
        [DataMember]
        public decimal vIRRF
        {
            get
            {
                return this._vIRRF;
            }
            set
            {
                this._vIRRF = value;
            }
        }

        [ParamsAttribute("VBCRETPREV")]
        [DataMember]
        public decimal vBCRetPrev
        {
            get
            {
                return this._vBCRetPrev;
            }
            set
            {
                this._vBCRetPrev = value;
            }
        }

        [ParamsAttribute("VRETPREV")]
        [DataMember]
        public decimal vRetPrev
        {
            get
            {
                return this._vRetPrev;
            }
            set
            {
                this._vRetPrev = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFERETTRIB WHERE IDOUTBOX = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox))
                {
                    sSql = @"UPDATE ZNFERETTRIB SET VRETPIS = ?, VRETCOFINS = ?, VRETCSLL = ?, VBCIRRF = ?, VIRRF = ?, VBCRETPREV = ?, VRETPREV = ? WHERE IDOUTBOX = ?";
                    _conn.ExecQuery(sSql, this.vRetPIS, this.vRetCOFINS, this.vRetCSLL, this.vBCIRRF, this.vIRRF, this.vBCRetPrev, this.vRetPrev, this.IdOutbox);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFERETTRIB (IDOUTBOX, VRETPIS, VRETCOFINS, VRETCSLL, VBCIRRF, VIRRF, VBCRETPREV, VRETPREV) 
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.vRetPIS, this.vRetCOFINS, this.vRetCSLL, this.vBCIRRF, this.vIRRF, this.vBCRetPrev, this.vRetPrev);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeretTrib.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeretTrib ReadNFeretTrib(params object[] parameters)
        {
            NFeretTrib _nferetTrib = new NFeretTrib();

            try
            {
                string sSql = @"SELECT * FROM ZNFERETTRIB WHERE IDOUTBOX = ?";
                _nferetTrib.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nferetTrib;
        }
    }
}
