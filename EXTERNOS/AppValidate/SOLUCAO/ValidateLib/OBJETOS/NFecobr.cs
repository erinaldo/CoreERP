using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFecobr : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private NFefat _nfefat;
        private List<NFedup> _nfedup;

        public NFefat nfefat
        {
            get
            {
                return this._nfefat;
            }
            set
            {
                this._nfefat = value;
            }
        }

        public List<NFedup> nfedup
        {
            get
            {
                return this._nfedup;
            }
            set
            {
                this._nfedup = value;
            }
        }

        private List<NFedup> Readdup(params object[] parameters)
        {
            List<NFedup> lnfedup = new List<NFedup>();
            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();

                string sSql = @"SELECT * FROM ZNFEDUP WHERE IDOUTBOX = ?";
                System.Data.DataTable _dados = _conn.ExecQuery(sSql, parameters);
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    lnfedup.Add(NFedup.ReadNFedup(row["IDOUTBOX"], row["NITEM"]));
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }

            return lnfedup;
        }

        public static NFecobr ReadNFecobr(params object[] parameters)
        {
            NFecobr _nfecobr = new NFecobr();

            try
            {
                _nfecobr.nfefat = NFefat.ReadNFefat(parameters);
                _nfecobr.nfedup = _nfecobr.Readdup(parameters);

            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfecobr;
        }
    }
}
