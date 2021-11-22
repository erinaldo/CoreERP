using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFSeDoc : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;

        private InfRPS _infRPS;

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

        public InfRPS RPS
        {
            get
            {
                return this._infRPS;
            }
            set
            {
                this._infRPS = value;
            }
        }

        public static NFSeDoc ReadByIDOutbox(params object[] parameters)
        {
            NFSeDoc _nfseDoc = new NFSeDoc();

            try
            {
                string sSql = @"SELECT * FROM ZNFSEDOC WHERE IDOUTBOX = ?";
                _nfseDoc.ReadFromCommand(sSql, parameters);
                _nfseDoc.RPS = InfRPS.ReadByIDOutbox(parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(ex.Message);
            }
            return _nfseDoc;
        }
    }
}
