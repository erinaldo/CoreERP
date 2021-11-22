using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeISSQNtot : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private decimal _vServ;
        private decimal _vBC;
        private decimal _vISS;
        private decimal _vPIS;
        private decimal _vCOFINS;
        private string _dCompet;
        private decimal _vDeducao;
        private decimal _vOutro;
        private decimal _vDescIncond;
        private decimal _vDescCond;
        private decimal _vISSRet;
        private int _cRegTrib;

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

        [ParamsAttribute("VSERV")]
        [DataMember]
        public decimal vServ
        {
            get
            {
                return this._vServ;
            }
            set
            {
                this._vServ = value;
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

        [ParamsAttribute("VISS")]
        [DataMember]
        public decimal vISS
        {
            get
            {
                return this._vISS;
            }
            set
            {
                this._vISS = value;
            }
        }

        [ParamsAttribute("VPIS")]
        [DataMember]
        public decimal vPIS
        {
            get
            {
                return this._vPIS;
            }
            set
            {
                this._vPIS = value;
            }
        }

        [ParamsAttribute("VCOFINS")]
        [DataMember]
        public decimal vCOFINS
        {
            get
            {
                return this._vCOFINS;
            }
            set
            {
                this._vCOFINS = value;
            }
        }

        [ParamsAttribute("DCOMPET")]
        [DataMember]
        public string dCompet
        {
            get
            {
                return this._dCompet;
            }
            set
            {
                this._dCompet = value;
            }
        }

        [ParamsAttribute("VDEDUCAO")]
        [DataMember]
        public decimal vDeducao
        {
            get
            {
                return this._vDeducao;
            }
            set
            {
                this._vDeducao = value;
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

        [ParamsAttribute("VDESCINCOND")]
        [DataMember]
        public decimal vDescIncond
        {
            get
            {
                return this._vDescIncond;
            }
            set
            {
                this._vDescIncond = value;
            }
        }

        [ParamsAttribute("VDESCCOND")]
        [DataMember]
        public decimal vDescCond
        {
            get
            {
                return this._vDescCond;
            }
            set
            {
                this._vDescCond = value;
            }
        }

        [ParamsAttribute("VISSRET")]
        [DataMember]
        public decimal vISSRet
        {
            get
            {
                return this._vISSRet;
            }
            set
            {
                this._vISSRet = value;
            }
        }

        [ParamsAttribute("CREGTRIB")]
        [DataMember]
        public int cRegTrib
        {
            get
            {
                return this._cRegTrib;
            }
            set
            {
                this._cRegTrib = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEISSQNTOT WHERE IDOUTBOX = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox))
                {
                    sSql = @"UPDATE ZNFEISSQNTOT SET VSERV = ?, VBC = ?, VISS = ?, VPIS = ?, VCOFINS = ?, DCOMPET = ?, VDEDUCAO = ?, VOUTRO = ?, VDESCINCOND = ?, VDESCCOND = ?,
                                VISSRET = ?, CREGTRIB = ? WHERE IDOUTBOX = ?";
                    _conn.ExecQuery(sSql, this.vServ, this.vBC, this.vISS, this.vPIS, this.vCOFINS, this.dCompet, this.vDeducao, this.vOutro, this.vDescIncond, this.vDescCond,
                        this.vISSRet, this.cRegTrib, this.IdOutbox);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEISSQNTOT (IDOUTBOX, VSERV, VBC, VISS, VPIS, VCOFINS, DCOMPET, VDEDUCAO, VOUTRO, VDESCINCOND, VDESCCOND, VISSRET, CREGTRIB)
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.vServ, this.vBC, this.vISS, this.vPIS, this.vCOFINS, this.dCompet, this.vDeducao, this.vOutro, this.vDescIncond, this.vDescCond,
                        this.vISSRet, this.cRegTrib);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeISSQNtot.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeISSQNtot ReadNFeISSQNtot(params object[] parameters)
        {
            NFeISSQNtot _nfeISSQNtot = new NFeISSQNtot();

            try
            {
                string sSql = @"SELECT * FROM ZNFEISSQNTOT WHERE IDOUTBOX = ?";
                _nfeISSQNtot.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeISSQNtot;
        }
    }
}
