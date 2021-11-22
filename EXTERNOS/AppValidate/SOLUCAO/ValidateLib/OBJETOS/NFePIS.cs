using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFePIS : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private NFePISAliq _nfePISAliq;
        private NFePISQtde _nfePISQtde;
        private NFePISNT _nfePISNT;
        private NFePISOutr _nfePISOutr;
        private NFePISST _nfePISST;

        public NFePISAliq nfePISAliq
        {
            get
            {
                return this._nfePISAliq;
            }
            set
            {
                this._nfePISAliq = value;
            }
        }

        public NFePISQtde nfePISQtde
        {
            get
            {
                return this._nfePISQtde;
            }
            set
            {
                this._nfePISQtde = value;
            }
        }

        public NFePISNT nfePISNT
        {
            get
            {
                return this._nfePISNT;
            }
            set
            {
                this._nfePISNT = value;
            }
        }

        public NFePISOutr nfePISOutr
        {
            get
            {
                return this._nfePISOutr;
            }
            set
            {
                this._nfePISOutr = value;
            }
        }

        public NFePISST nfePISST
        {
            get
            {
                return this._nfePISST;
            }
            set
            {
                this._nfePISST = value;
            }
        }

        public static NFePIS ReadNFePIS(params object[] parameters)
        {
            NFePIS _nfePIS = new NFePIS();

            try
            {
                _nfePIS.nfePISAliq = NFePISAliq.ReadNFePISAliq(parameters);
                _nfePIS.nfePISQtde = NFePISQtde.ReadNFePISQtde(parameters);
                _nfePIS.nfePISNT = NFePISNT.ReadNFePISNT(parameters);
                _nfePIS.nfePISOutr = NFePISOutr.ReadNFePISOutr(parameters);
                _nfePIS.nfePISST = NFePISST.ReadNFePISST(parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfePIS;
        }
    }
}
