using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFePISQtde : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private string _CST;
        private decimal _qBCProd;
        private decimal _vAliqProd;
        private decimal _vPIS;

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

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEPISQTDE WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEPISQTDE SET CST = ?, QBCPROD = ?, VALIQPROD = ?, VPIS = ? WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.CST, this.qBCProd, this.vAliqProd, this.vPIS, this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEPISQTDE (IDOUTBOX, NITEM, CST, QBCPROD, VALIQPROD, VPIS) VALUES (?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.CST, this.qBCProd, this.vAliqProd, this.vPIS);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFePISQtde.Save", err);
                throw new Exception(err);
            }
        }

        public static NFePISQtde ReadNFePISQtde(params object[] parameters)
        {
            NFePISQtde _nfePISQtde = new NFePISQtde();

            try
            {
                string sSql = @"SELECT * FROM ZNFEPISQTDE WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfePISQtde.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfePISQtde;
        }
    }
}
