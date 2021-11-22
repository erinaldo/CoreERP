using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class WebServiceMunParams : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private string _codEstrutura;
        private string _versao;
        private string _uf;
        private string _codMunicipio;
        private string _url;
        private int _tpamb;
        private int _ativo;

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

        [ParamsAttribute("CODMUNICIPIO")]
        [DataMember]
        public string CodMunicipio
        {
            get
            {
                return this._codMunicipio;
            }
            set
            {
                this._codMunicipio = value;
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

        public static WebServiceMunParams ReadActiveService(string CodEstrtura, string UF, string CodMunicipio, int Ambiente)
        {
            try
            {
                string sSql = @"SELECT * FROM ZCONFIGWSMUN WHERE CODESTRUTURA = ? AND UF = ? AND CODMUNICIPIO = ? AND TPAMB = ? AND ATIVO = ?";
                WebServiceMunParams _webServiceMunParams = new WebServiceMunParams();
                _webServiceMunParams.ReadFromCommand(sSql, CodEstrtura, UF, CodMunicipio, Ambiente, 1);
                return _webServiceMunParams;
            }
            catch (Exception ex)
            {
                Log.SalvarLog("WebServiceMunParams.ReadActiveService", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public static WebServiceMunParams ReadService(string CodEstrtura, string Versao, string UF, string CodMunicipio, int Ambiente, bool Ativo)
        {
            try
            {
                string sSql = @"SELECT * FROM ZCONFIGWSMUN WHERE CODESTRUTURA = ? AND VERSAO = ? AND UF = ? AND CODMUNICIPIO = ? AND TPAMB = ? AND ATIVO = ?";
                WebServiceMunParams _webServiceMunParams = new WebServiceMunParams();
                _webServiceMunParams.ReadFromCommand(sSql, CodEstrtura, Versao, UF, CodMunicipio, Ambiente, (Ativo) ? 1 : 0);
                return _webServiceMunParams;
            }
            catch (Exception ex)
            {
                Log.SalvarLog("WebServiceMunParams.ReadService", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public static WebServiceMunParams ReadByKey(params object[] parameters)
        {
            try
            {
                string sSql = @"SELECT * FROM ZCONFIGWSMUN WHERE CODESTRUTURA = ? AND VERSAO = ? AND UF = ? AND CODMUNICIPIO = ?";
                WebServiceMunParams _webServiceMunParams = new WebServiceMunParams();
                _webServiceMunParams.ReadFromCommand(sSql, parameters);
                return _webServiceMunParams;
            }
            catch (Exception ex)
            {
                Log.SalvarLog("WebServiceMunParams.ReadByKey", ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
