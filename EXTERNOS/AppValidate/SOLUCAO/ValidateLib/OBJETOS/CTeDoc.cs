using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class CTeDoc : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        public static CTeDoc ReadByIDOutbox(params object[] parameters)
        {
            CTeDoc _cteDoc = new CTeDoc();

            try
            {

            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _cteDoc;
        }
    }
}
