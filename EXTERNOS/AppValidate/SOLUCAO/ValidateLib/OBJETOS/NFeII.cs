using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeII : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private decimal _vBC;
        private decimal _vDespAdu;
        private decimal _vII;
        private decimal _vIOF;

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

        [ParamsAttribute("VDESPADU")]
        [DataMember]
        public decimal vDespAdu
        {
            get
            {
                return this._vDespAdu;
            }
            set
            {
                this._vDespAdu = value;
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

        [ParamsAttribute("VIOF")]
        [DataMember]
        public decimal vIOF
        {
            get
            {
                return this._vIOF;
            }
            set
            {
                this._vIOF = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEII WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEII SET VBC = ?, VDESPADU = ?, VII = ?, VIOF = ?
                                WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.vBC, this.vDespAdu, this.vII, this.vIOF, this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEII (IDOUTBOX, NITEM, VBC, VDESPADU, VII, VIOF)
                                VALUES (?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.vBC, this.vDespAdu, this.vII, this.vIOF);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeII.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeII ReadNFeII(params object[] parameters)
        {
            NFeII _nfeII = new NFeII();

            try
            {
                string sSql = @"SELECT * FROM ZNFEII WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeII.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeII;
        }
    }
}
