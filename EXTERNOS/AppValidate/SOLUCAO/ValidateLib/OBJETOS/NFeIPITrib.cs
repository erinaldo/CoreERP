using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeIPITrib : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private string _CST;
        private decimal _vBC;
        private decimal _pIPI;
        private decimal _qUnid;
        private decimal _vUnid;
        private decimal _vIPI;

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

        [ParamsAttribute("PIPI")]
        [DataMember]
        public decimal pIPI
        {
            get
            {
                return this._pIPI;
            }
            set
            {
                this._pIPI = value;
            }
        }

        [ParamsAttribute("QUNID")]
        [DataMember]
        public decimal qUnid
        {
            get
            {
                return this._qUnid;
            }
            set
            {
                this._qUnid = value;
            }
        }

        [ParamsAttribute("VUNID")]
        [DataMember]
        public decimal vUnid
        {
            get
            {
                return this._vUnid;
            }
            set
            {
                this._vUnid = value;
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

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEIPITRIB WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEIPITRIB SET CST = ?, VBC = ?, PIPI = ?, QUNID = ?, VUNID = ?, VIPI = ? WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.CST, this.vBC, this.pIPI, this.qUnid, this.vUnid, this.vIPI, this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEIPITRIB (IDOUTBOX, NITEM, CST, VBC, PIPI, QUNID, VUNID, VIPI)
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.CST, this.vBC, this.pIPI, this.qUnid, this.vUnid, this.vIPI);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeIPITrib.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeIPITrib ReadNFeIPITrib(params object[] parameters)
        {
            NFeIPITrib _nfeIPITrib = new NFeIPITrib();

            try
            {
                string sSql = @"SELECT * FROM ZNFEIPITRIB WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeIPITrib.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeIPITrib;
        }
    }
}
