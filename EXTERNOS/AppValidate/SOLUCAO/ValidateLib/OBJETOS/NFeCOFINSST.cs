using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeCOFINSST : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private decimal _vBC;
        private decimal _pCOFINS;
        private decimal _qBCProd;
        private decimal _vAliqProd;
        private decimal _vCOFINS;

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

        [ParamsAttribute("PCOFINS")]
        [DataMember]
        public decimal pCOFINS
        {
            get
            {
                return this._pCOFINS;
            }
            set
            {
                this._pCOFINS = value;
            }
        }

        [ParamsAttribute("QBCPROD")]
        [DataMember]
        public decimal qBCProd
        {
            get
            {
                return this._qBCProd;
            }
            set
            {
                this._qBCProd = value;
            }
        }

        [ParamsAttribute("VALIQPROD")]
        [DataMember]
        public decimal vAliqProd
        {
            get
            {
                return this._vAliqProd;
            }
            set
            {
                this._vAliqProd = value;
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

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFECOFINSST WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFECOFINSST SET VBC = ?, PCOFINS = ?, QBCPROD = ?, VALIQPROD = ?, VCOFINS = ? WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.vBC, this.pCOFINS, this.qBCProd, this.vAliqProd, this.vCOFINS, this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFECOFINSST (IDOUTBOX, NITEM, VBC, PCOFINS, QBCPROD, VALIQPROD, VCOFINS) VALUES (?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.vBC, this.pCOFINS, this.qBCProd, this.vAliqProd, this.vCOFINS);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeCOFINSST.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeCOFINSST ReadNFeCOFINSST(params object[] parameters)
        {
            NFeCOFINSST _nfeCOFINSST = new NFeCOFINSST();

            try
            {
                string sSql = @"SELECT * FROM ZNFECOFINSST WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeCOFINSST.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeCOFINSST;
        }
    }
}