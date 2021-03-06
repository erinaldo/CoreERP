using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFereboque : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private string _placa;
        private string _UF;
        private string _RNTC;

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

        [ParamsAttribute("PLACA")]
        [DataMember]
        public string placa
        {
            get
            {
                return this._placa;
            }
            set
            {
                this._placa = value;
            }
        }

        [ParamsAttribute("UF")]
        [DataMember]
        public string UF
        {
            get
            {
                return this._UF;
            }
            set
            {
                this._UF = value;
            }
        }

        [ParamsAttribute("RNTC")]
        [DataMember]
        public string RNTC
        {
            get
            {
                return this._RNTC;
            }
            set
            {
                this._RNTC = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEREBOQUE WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEREBOQUE SET PLACA = ?, UF = ?, RNTC = ?
                                WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.placa, this.UF, this.RNTC, this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEREBOQUE (IDOUTBOX, NITEM, PLACA, UF, RNTC) 
                                VALUES (?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.placa, this.UF, this.RNTC);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFereboque.Save", err);
                throw new Exception(err);
            }
        }

        public static NFereboque ReadNFereboque(params object[] parameters)
        {
            NFereboque _nfereboque = new NFereboque();

            try
            {
                string sSql = @"SELECT * FROM ZNFEREBOQUE WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfereboque.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfereboque;
        }
    }
}
