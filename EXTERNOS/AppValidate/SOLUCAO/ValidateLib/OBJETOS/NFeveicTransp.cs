using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeveicTransp : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
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
                sSql = @"SELECT IDOUTBOX FROM ZNFEVEICTRANSP WHERE IDOUTBOX = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox))
                {
                    sSql = @"UPDATE ZNFEVEICTRANSP SET PLACA = ?, UF = ?, RNTC = ? WHERE IDOUTBOX = ?";
                    _conn.ExecQuery(sSql, this.placa, this.UF, this.RNTC, this.IdOutbox);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEVEICTRANSP (IDOUTBOX, PLACA, UF, RNTC) 
                                VALUES (?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.placa, this.UF, this.RNTC);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeveicTransp.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeveicTransp ReadNFeveicTransp(params object[] parameters)
        {
            NFeveicTransp _nfeveicTransp = new NFeveicTransp();

            try
            {
                string sSql = @"SELECT * FROM ZNFEVEICTRANSP WHERE IDOUTBOX = ?";
                _nfeveicTransp.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeveicTransp;
        }
    }
}
