using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeCOFINSQtde : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private string _CST;
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
                sSql = @"SELECT IDOUTBOX FROM ZNFECOFINSQTDE WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFECOFINSQTDE SET CST = ?, QBCPROD = ?, VALIQPROD = ?, VCOFINS = ? WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.CST, this.qBCProd, this.vAliqProd, this.vCOFINS, this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFECOFINSQTDE (IDOUTBOX, NITEM, CST, QBCPROD, VALIQPROD, VCOFINS) VALUES (?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.CST, this.qBCProd, this.vAliqProd, this.vCOFINS);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeCOFINSQtde.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeCOFINSQtde ReadNFeCOFINSQtde(params object[] parameters)
        {
            NFeCOFINSQtde _nfeCOFINSQtde = new NFeCOFINSQtde();

            try
            {
                string sSql = @"SELECT * FROM ZNFECOFINSQTDE WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeCOFINSQtde.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeCOFINSQtde;
        }
    }
}
