using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeDet : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private string _infAdProd;
        private NFeProd _nfeProd;
        private NFeImposto _nfeImposto;

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

        [ParamsAttribute("INFADPROD")]
        [DataMember]
        public string infAdProd
        {
            get
            {
                return this._infAdProd;
            }
            set
            {
                this._infAdProd = value;
            }
        }

        public NFeProd nfeProd
        {
            get
            {
                return this._nfeProd;
            }
            set
            {
                this._nfeProd = value;
            }
        }

        public NFeImposto nfeImposto
        {
            get
            {
                return this._nfeImposto;
            }
            set
            {
                this._nfeImposto = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEDET WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEDET SET INFADPROD = ? WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.infAdProd, this.IdOutbox, this.nItem );
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEDET (IDOUTBOX, NITEM, INFADPROD) VALUES (?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.infAdProd);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeDet.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeDet ReadNFeDet(params object[] parameters)
        {
            NFeDet _nfeDet = new NFeDet();

            try
            {
                string sSql = @"SELECT * FROM ZNFEDET WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeDet.ReadFromCommand(sSql, parameters);
                _nfeDet.nfeProd = NFeProd.ReadNFeProd(parameters);
                _nfeDet.nfeImposto = NFeImposto.ReadNFeImposto(parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeDet;
        }
    }
}
