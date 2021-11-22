using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeCOFINS : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private NFeCOFINSAliq _nfeCOFINSAliq;
        private NFeCOFINSQtde _nfeCOFINSQtde;
        private NFeCOFINSNT _nfeCOFINSNT;
        private NFeCOFINSOutr _nfeCOFINSOutr;
        private NFeCOFINSST _nfeCOFINSST; 

        public NFeCOFINSAliq nfeCOFINSAliq
        {
            get
            {
                return this._nfeCOFINSAliq;
            }
            set
            {
                this._nfeCOFINSAliq = value;
            }
        }

        public NFeCOFINSQtde nfeCOFINSQtde
        {
            get
            {
                return this._nfeCOFINSQtde;
            }
            set
            {
                this._nfeCOFINSQtde = value;
            }
        }

        public NFeCOFINSNT nfeCOFINSNT
        {
            get
            {
                return this._nfeCOFINSNT;
            }
            set
            {
                this._nfeCOFINSNT = value;
            }
        }

        public NFeCOFINSOutr nfeCOFINSOutr
        {
            get
            {
                return this._nfeCOFINSOutr;
            }
            set
            {
                this._nfeCOFINSOutr = value;
            }
        }

        public NFeCOFINSST nfeCOFINSST
        {
            get
            {
                return this._nfeCOFINSST;
            }
            set
            {
                this._nfeCOFINSST = value;
            }
        }

        public static NFeCOFINS ReadNFeCOFINS(params object[] parameters)
        {
            NFeCOFINS _nfeCOFINS = new NFeCOFINS();

            try
            {
                _nfeCOFINS.nfeCOFINSAliq = NFeCOFINSAliq.ReadNFeCOFINSAliq(parameters);
                _nfeCOFINS.nfeCOFINSQtde = NFeCOFINSQtde.ReadNFeCOFINSQtde(parameters);
                _nfeCOFINS.nfeCOFINSNT = NFeCOFINSNT.ReadNFeCOFINSNT(parameters);
                _nfeCOFINS.nfeCOFINSOutr = NFeCOFINSOutr.ReadNFeCOFINSOutr(parameters);
                _nfeCOFINS.nfeCOFINSST = NFeCOFINSST.ReadNFeCOFINSST(parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeCOFINS;
        }
    }
}