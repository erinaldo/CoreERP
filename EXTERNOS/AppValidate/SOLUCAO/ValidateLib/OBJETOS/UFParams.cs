using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class UFParams : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private string _uf;
        private string _nome;
        private string _codigoIBGE;

        [ParamsAttribute("UF")]
        [DataMember]
        public string UF
        {
            get
            {
                return this._uf;
            }
            set
            {
                this._uf = value;
            }
        }

        [ParamsAttribute("NOME")]
        [DataMember]
        public string Nome
        {
            get
            {
                return this._nome;
            }
            set
            {
                this._nome = value;
            }
        }

        [ParamsAttribute("CODIGOIBGE")]
        [DataMember]
        public string CodigoIBGE
        {
            get
            {
                return this._codigoIBGE;
            }
            set
            {
                this._codigoIBGE = value;
            }
        }

        public static UFParams ReadByCodigoIBGE(params object[] parameters)
        {
            try
            {
                string sSql = @"SELECT * FROM ZESTADO WHERE CODIGOIBGE = ?";
                UFParams _ufParams = new UFParams();
                _ufParams.ReadFromCommand(sSql, parameters);
                return _ufParams;
            }
            catch (Exception ex)
            {
                Log.SalvarLog("ReadByCodigoIBGE", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public static UFParams ReadByUF(params object[] parameters)
        {
            try
            {
                string sSql = @"SELECT * FROM ZESTADO WHERE UF = ?";
                UFParams _ufParams = new UFParams();
                _ufParams.ReadFromCommand(sSql, parameters);
                return _ufParams;
            }
            catch (Exception ex)
            {
                Log.SalvarLog("ReadByUF", ex.Message);
                throw new Exception(ex.Message);
            }

        }
    }
}
