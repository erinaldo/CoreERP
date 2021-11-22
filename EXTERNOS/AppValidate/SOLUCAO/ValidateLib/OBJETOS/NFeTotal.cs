using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeTotal : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private NFeICMSTot _nfeICMSTot;
        private NFeISSQNtot _nfeISSQNtot;
        private NFeretTrib _nferetTrib;

        public NFeICMSTot nfeICMSTot
        {
            get
            {
                return this._nfeICMSTot;
            }
            set
            {
                this._nfeICMSTot = value;
            }
        }

        public NFeISSQNtot nfeISSQNtot
        {
            get
            {
                return this._nfeISSQNtot;
            }
            set
            {
                this._nfeISSQNtot = value;
            }
        }
        
        public NFeretTrib nferetTrib
        {
            get
            {
                return this._nferetTrib;
            }
            set
            {
                this._nferetTrib = value;
            }
        }

        public static NFeTotal ReadNFeTotal(params object[] parameters)
        {
            NFeTotal _nfeTotal = new NFeTotal();

            try
            {
                _nfeTotal.nfeICMSTot = NFeICMSTot.ReadNFeICMSTot(parameters);
                _nfeTotal.nfeISSQNtot = NFeISSQNtot.ReadNFeISSQNtot(parameters);
                _nfeTotal.nferetTrib = NFeretTrib.ReadNFeretTrib(parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeTotal;
        }
    }
}
