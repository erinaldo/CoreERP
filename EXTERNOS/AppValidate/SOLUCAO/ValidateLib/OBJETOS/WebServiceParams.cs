using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class WebServiceParams : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private string _codEstrutura;
        private string _versao;
        private string _uf;
        private string _servico;
        private string _url;
        private int _tpamb;
        private int _ativo;
        private string _urlc;
        private string _schema;
        private string _codigoIBGE;

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

        [ParamsAttribute("CODESTRUTURA")]
        [DataMember]
        public string CodEstrutura
        {
            get
            {
                return this._codEstrutura;
            }
            set
            {
                this._codEstrutura = value;
            }
        }

        [ParamsAttribute("VERSAO")]
        [DataMember]
        public string Versao
        {
            get
            {
                return this._versao;
            }
            set
            {
                this._versao = value;
            }
        }

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

        [ParamsAttribute("SERVICO")]
        [DataMember]
        public string Servico
        {
            get
            {
                return this._servico;
            }
            set
            {
                this._servico = value;
            }
        }

        [ParamsAttribute("URL")]
        [DataMember]
        public string URL
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
            }
        }

        [ParamsAttribute("TPAMB")]
        [DataMember]
        public int TPAMB
        {
            get
            {
                return this._tpamb;
            }
            set
            {
                this._tpamb = value;
            }
        }

        [ParamsAttribute("ATIVO")]
        [DataMember]
        public int Ativo
        {
            get
            {
                return this._ativo;
            }
            set
            {
                this._ativo = value;
            }
        }

        [ParamsAttribute("URLC")]
        [DataMember]
        public string URLC
        {
            get
            {
                return this._urlc;
            }
            set
            {
                this._urlc = value;
            }
        }

        [ParamsAttribute("SCHEMA")]
        [DataMember]
        public string Schema
        {
            get
            {
                return this._schema;
            }
            set
            {
                this._schema = value;
            }
        }

        private void SetCodigoIBGE()
        {
            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                string sSql = @"SELECT CODIGOIBGE FROM ZESTADO WHERE UF = ?";
                _codigoIBGE = _conn.ExecGetField("0", sSql, _uf).ToString();
            }
            catch (Exception ex)
            {
                Log.SalvarLog("WebServiceParams.SetCodigoIBGE", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public static WebServiceParams ReadActiveService(string CodEstrtura, string UF, string Servico, int Ambiente)
        {
            try
            {
                string sSql = @"SELECT * FROM ZCONFIGWS WHERE CODESTRUTURA = ? AND UF = ? AND SERVICO = ? AND TPAMB = ? AND ATIVO = ?";
                WebServiceParams _webServiceParams = new WebServiceParams();
                _webServiceParams.ReadFromCommand(sSql, CodEstrtura, UF, Servico, Ambiente, 1);
                _webServiceParams.SetCodigoIBGE();
                return _webServiceParams;
            }
            catch (Exception ex)
            {
                Log.SalvarLog("WebServiceParams.ReadActiveService", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public static WebServiceParams ReadService(string CodEstrtura, string Versao, string UF, string Servico, int Ambiente, bool Ativo)
        {
            try
            {
                string sSql = @"SELECT * FROM ZCONFIGWS WHERE CODESTRUTURA = ? AND VERSAO = ? AND UF = ? AND SERVICO = ? AND TPAMB = ? AND ATIVO = ?";
                WebServiceParams _webServiceParams = new WebServiceParams();
                _webServiceParams.ReadFromCommand(sSql, CodEstrtura, Versao, UF, Servico, Ambiente, (Ativo)? 1 : 0);
                _webServiceParams.SetCodigoIBGE();
                return _webServiceParams;
            }
            catch (Exception ex)
            {
                Log.SalvarLog("WebServiceParams.ReadService", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public static WebServiceParams ReadByKey(params object[] parameters)
        {
            try
            {
                string sSql = @"SELECT * FROM ZCONFIGWS WHERE CODESTRUTURA = ? AND VERSAO = ? AND UF = ? AND SERVICO = ?";
                WebServiceParams _webServiceParams = new WebServiceParams();
                _webServiceParams.ReadFromCommand(sSql, parameters);
                _webServiceParams.SetCodigoIBGE();
                return _webServiceParams;
            }
            catch (Exception ex)
            {
                Log.SalvarLog("WebServiceParams.ReadByKey", ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
