using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeICMSTot : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private decimal _vBC;
        private decimal _vICMS;
        private decimal _vICMSDeson;
        private decimal _vBCST;
        private decimal _vST;
        private decimal _vProd;
        private decimal _vFrete;
        private decimal _vSeg;
        private decimal _vDesc;
        private decimal _vII;
        private decimal _vIPI;
        private decimal _vPIS;
        private decimal _vCOFINS;
        private decimal _vOutro;
        private decimal _vNF;
        private decimal _vTotTrib;
        private decimal _vFCPUFDest;
        private decimal _vICMSUFDest;
        private decimal _vICMSUFRemet;

       



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

        [ParamsAttribute("VST")]
        [DataMember]
        public decimal vST
        {
            get
            {
                return this._vST;
            }
            set
            {
                this._vST = value;
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

        [ParamsAttribute("VII")]
        [DataMember]
        public decimal vII
        {
            get
            {
                return this._vII;
            }
            set
            {
                this._vII = value;
            }
        }

        [ParamsAttribute("VIPI")]
        [DataMember]
        public decimal vIPI
        {
            get
            {
                return this._vIPI;
            }
            set
            {
                this._vIPI = value;
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

        [ParamsAttribute("VNF")]
        [DataMember]
        public decimal vNF
        {
            get
            {
                return this._vNF;
            }
            set
            {
                this._vNF = value;
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
        [ParamsAttribute("vFCPUFDest")]
        [DataMember]
        public decimal vFCPUFDest
        {
            get
            {
                return this._vFCPUFDest;
            }
            set
            {
                this._vFCPUFDest = value;
            }
        }
        [ParamsAttribute("vICMSUFDest")]
        [DataMember]
        public decimal vICMSUFDest
        {
            get
            {
                return this._vICMSUFDest;
            }
            set
            {
                this._vICMSUFDest = value;
            }
        }
        [ParamsAttribute("vICMSUFRemet")]
        [DataMember]
        public decimal vICMSUFRemet
        {
            get
            {
                return this._vICMSUFRemet;
            }
            set
            {
                this._vICMSUFRemet = value;
            }
        }


        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEICMSTOT WHERE IDOUTBOX = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox))
                {
                    sSql = @"UPDATE ZNFEICMSTOT SET VBC = ?, VICMS = ?, VICMSDESON = ?, VBCST = ?, VST = ?, VPROD = ?, VFRETE = ?, VSEG = ?, VDESC = ?, VII = ?, VIPI = ?, 
                                VPIS = ?, VCOFINS = ?, VOUTRO = ?, VNF = ?, VTOTTRIB = ?, vFCPUFDest = ?, vICMSUFDest = ?, vICMSUFRemet = ? WHERE IDOUTBOX = ?";
                    _conn.ExecQuery(sSql, this.vBC, this.vICMS, this.vICMSDeson, this.vBCST, this.vST, this.vProd, this.vFrete, this.vSeg, this.vDesc, this.vII, this.vIPI,
                        this.vPIS, this.vCOFINS, this.vOutro, this.vNF, this.vTotTrib, this.vFCPUFDest, this.vICMSUFDest, this.vICMSUFRemet, this.IdOutbox);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEICMSTOT (IDOUTBOX, VBC, VICMS, VICMSDESON, VBCST, VST, VPROD, VFRETE, VSEG, VDESC, VII, VIPI, VPIS, VCOFINS, VOUTRO, VNF, VTOTTRIB, vFCPUFDest, vICMSUFDest, vICMSUFRemet)
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.vBC, this.vICMS, this.vICMSDeson, this.vBCST, this.vST, this.vProd, this.vFrete, this.vSeg, this.vDesc, this.vII, this.vIPI,
                        this.vPIS, this.vCOFINS, this.vOutro, this.vNF, this.vTotTrib, this.vFCPUFDest, this.vICMSUFDest, this.vICMSUFRemet);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeICMSTot.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeICMSTot ReadNFeICMSTot(params object[] parameters)
        {
            NFeICMSTot _nfeICMSTot = new NFeICMSTot();

            try
            {
                string sSql = @"SELECT * FROM ZNFEICMSTOT WHERE IDOUTBOX = ?";
                _nfeICMSTot.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeICMSTot;
        }
    }
}
