using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeIPI : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private string _clEnq;
        private string _CNPJProd;
        private string _cSelo;
        private string _qSelo;
        private string _cEnq;
        private NFeIPITrib _nfeIPITrib;
        private NFeIPINT _nfeIPINT;

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

        [ParamsAttribute("CLENQ")]
        [DataMember]
        public string clEnq
        {
            get
            {
                return this._clEnq;
            }
            set
            {
                this._clEnq = value;
            }
        }

        [ParamsAttribute("CNPJPROD")]
        [DataMember]
        public string CNPJProd
        {
            get
            {
                return this._CNPJProd;
            }
            set
            {
                this._CNPJProd = value;
            }
        }

        [ParamsAttribute("CSELO")]
        [DataMember]
        public string cSelo
        {
            get
            {
                return this._cSelo;
            }
            set
            {
                this._cSelo = value;
            }
        }

        [ParamsAttribute("QSELO")]
        [DataMember]
        public string qSelo
        {
            get
            {
                return this._qSelo;
            }
            set
            {
                this._qSelo = value;
            }
        }

        [ParamsAttribute("CENQ")]
        [DataMember]
        public string cEnq
        {
            get
            {
                return this._cEnq;
            }
            set
            {
                this._cEnq = value;
            }
        }

        public NFeIPITrib nfeIPITrib
        {
            get
            {
                return this._nfeIPITrib;
            }
            set
            {
                this._nfeIPITrib = value;
            }
        }

        public NFeIPINT nfeIPINT
        {
            get
            {
                return this._nfeIPINT;
            }
            set
            {
                this._nfeIPINT = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEIPI WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEIPI SET CLENQ = ?, CNPJPROD = ?, CSELO = ?, QSELO = ?, CENQ = ? WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.clEnq, this.CNPJProd, this.cSelo, this.qSelo, this.cEnq, this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEIPI (IDOUTBOX, NITEM, CLENQ, CNPJPROD, CSELO, QSELO, CENQ)
                                VALUES (?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.clEnq, this.CNPJProd, this.cSelo, this.qSelo, this.cEnq);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeIPI.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeIPI ReadNFeIPI(params object[] parameters)
        {
            NFeIPI _nfeIPI = new NFeIPI();

            try
            {
                string sSql = @"SELECT * FROM ZNFEIPI WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeIPI.ReadFromCommand(sSql, parameters);
                _nfeIPI.nfeIPITrib = NFeIPITrib.ReadNFeIPITrib(parameters);
                _nfeIPI.nfeIPINT = NFeIPINT.ReadNFeIPINT(parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeIPI;
        }
    }
}
