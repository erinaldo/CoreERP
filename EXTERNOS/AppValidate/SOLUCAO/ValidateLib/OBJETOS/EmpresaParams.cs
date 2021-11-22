using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace ValidateLib
{
    [Serializable]
    public class EmpresaParams : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idEmpresa;
        private string _nome;
        private string _cnpj;
        private string _inscricaoMunicipal;
        private string _municipio;
        private string _uf;
        private string _codigoIBGE;
        private string _certificado;
        private string _certificadoPath;
        private string _certificadoSenha;
        private string _certificadoTipo;
        private string _ultNSU;
        private int _indNFe;
        private int _indEmi;
        private int _tpAmb;
        private int _ultLote;
        private int _usaLote;
        private int _manDestAut;
        private DateTime? _dataProxCons;
        private X509Certificate2 _x509Certificate2;

        [DataMember]
        public X509Certificate2 x509Certificate2
        {
            get
            {
                return this._x509Certificate2;
            }
            set
            {
                this._x509Certificate2 = value;
            }
        }

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

        [ParamsAttribute("CODEMPRESA")]//IDEMPRESA
        [DataMember]
        public int IdEmpresa
        {
            get
            {
                return this._idEmpresa;
            }
            set
            {
                this._idEmpresa = value;
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

        [ParamsAttribute("CGCCPF")]//CNPJ
        [DataMember]
        public string CNPJ
        {
            get
            {
                return this._cnpj;
            }
            set
            {
                this._cnpj = value;
            }
        }

        [ParamsAttribute("INSCRICAOMUNICIPAL")]
        [DataMember]
        public string InscricaoMunicipal
        {
            get
            {
                return this._inscricaoMunicipal ;
            }
            set
            {
                this._inscricaoMunicipal = value;
            }
        }

        [ParamsAttribute("CODCIDADE")] //MUNICIPIO
        [DataMember]
        public string Municipio
        {
            get
            {
                return this._municipio;
            }
            set
            {
                this._municipio = value;
            }
        }

        [ParamsAttribute("CODETD")] //UF
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

        //Dirlei
        /*
        [ParamsAttribute("CERTIFICADO")]
        [DataMember]
        public string Certificado
        {
            get
            {
                return this._certificado;
            }
            set
            {
                this._certificado = value;
            }
        }

        [ParamsAttribute("CERTIFICADOPATH")]
        [DataMember]
        public string CertificadoPath
        {
            get
            {
                return this._certificadoPath;
            }
            set
            {
                this._certificadoPath = value;
            }
        }

        [ParamsAttribute("CERTIFICADOSENHA")]
        [DataMember]
        public string CertificadoSenha
        {
            get
            {
                return this._certificadoSenha;
            }
            set
            {
                this._certificadoSenha = value;
            }
        }

        [ParamsAttribute("CERTIFICADOTIPO")]
        [DataMember]
        public string CertificadoTipo
        {
            get
            {
                return this._certificadoTipo;
            }
            set
            {
                this._certificadoTipo = value;
            }
        }

        [ParamsAttribute("ULTNSU")]
        [DataMember]
        public string UltNSU
        {
            get
            {
                return this._ultNSU;
            }
            set
            {
                this._ultNSU = value;
            }
        }

        [ParamsAttribute("INDNFE")]
        [DataMember]
        public int IndNFe
        {
            get
            {
                return this._indNFe;
            }
            set
            {
                this._indNFe = value;
            }
        }

        [ParamsAttribute("INDEMI")]
        [DataMember]
        public int IndEmi
        {
            get
            {
                return this._indEmi;
            }
            set
            {
                this._indEmi = value;
            }
        }
        */

        [ParamsAttribute("TPAMB")]
        [DataMember]
        public int TpAmb
        {
            get
            {
                return this._tpAmb;
            }
            set
            {
                this._tpAmb = value;
            }
        }

        //Dirlei
        /*
         
        [ParamsAttribute("ULTLOTE")]
        [DataMember]
        public int UltLote
        {
            get
            {
                return this._ultLote;
            }
            set
            {
                this._ultLote = value;
            }
        }

        [ParamsAttribute("USALOTE")]
        [DataMember]
        public int UsaLote
        {
            get
            {
                return this._usaLote;
            }
            set
            {
                this._usaLote = value;
            }
        }

        [ParamsAttribute("MANDESTAUT")]
        [DataMember]
        public int ManDestAut
        {
            get
            {
                return this._manDestAut;
            }
            set
            {
                this._manDestAut = value;
            }
        }

        [ParamsAttribute("DATAPROXCONS")]
        [DataMember]
        public DateTime? DataProxCons
        {
            get
            {
                return this._dataProxCons;
            }
            set
            {
                this._dataProxCons = value;
            }
        }
        */

        public void GetNewLote()
        {
            /*
            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                string sSql = @"SELECT ULTLOTE + 1 ULTLOTE FROM ZCONFIGEMP WHERE IDEMPRESA = ?";
                _ultLote = Convert.ToInt32(_conn.ExecGetField(0, sSql, _idEmpresa));
                this.UpdateParams();
            }
            catch (Exception ex)
            {
                Log.SalvarLog("GetNewLote", ex.Message);
                throw new Exception(ex.Message);
            }
            */
            
        }

        private void GetCertificado()
        {
            try
            {
                ValidateLib.Certificado c = new Certificado();
                _x509Certificate2 = c.GetCertificado(_idEmpresa);
            }
            catch (Exception ex)
            {
                Log.SalvarLog("GetCertificado", ex.Message);
                throw new Exception(ex.Message);
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
                Log.SalvarLog("SetCodigoIBGE", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void UpdateParams()
        {
            /*
            
            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                string sSql = @"UPDATE ZCONFIGEMP SET NOME = ?,
CNPJ = ?,
INSCRICAOMUNICIPAL = ?,
MUNICIPIO = ?,
UF = ?,
CERTIFICADO = ?,
CERTIFICADOPATH = ?,
CERTIFICADOSENHA = ?,
CERTIFICADOTIPO = ?,
ULTNSU = ?,
INDNFE = ?,
INDEMI = ?,
TPAMB = ?,
ULTLOTE = ?,
USALOTE = ?,
MANDESTAUT = ?,
DATAPROXCONS = ?
WHERE IDEMPRESA = ?";
                _conn.ExecTransaction(sSql, Nome, CNPJ, InscricaoMunicipal, Municipio, UF, Certificado, CertificadoPath, CertificadoSenha, CertificadoTipo, UltNSU, IndNFe, IndEmi, TpAmb, UltLote, UsaLote, ManDestAut, DataProxCons, IdEmpresa);

            }
            catch (Exception ex)
            {
                Log.SalvarLog("UpdateParams", ex.Message);
                throw new Exception(ex.Message);
            }
           */ 
        }

        public static EmpresaParams ReadByCNPJ(params object[] parameters)
        {
            try
            {
                //Dirlei
               // string sSql = @"SELECT * FROM ZCONFIGEMP WHERE CNPJ = ?";
                string sSql = @"SELECT * FROM GFILIAL WHERE CGCCPF = ?";
                EmpresaParams _empresaParams = new EmpresaParams();
                _empresaParams.ReadFromCommand(sSql, parameters);
                _empresaParams.SetCodigoIBGE();
               // _empresaParams.GetCertificado();
                return _empresaParams;
            }
            catch (Exception ex)
            {
                Log.SalvarLog("ReadByCNPJ", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public static EmpresaParams ReadByIdEmpresa(params object[] parameters)
        {
            try
            {
                //Dirlei
                string sSql = @"SELECT * FROM ZCONFIGEMP WHERE IDEMPRESA = ?";
                //string sSql = @"SELECT * FROM GFILIAL WHERE CODEMPRESA = ?";
                EmpresaParams _empresaParams = new EmpresaParams();
                _empresaParams.ReadFromCommand(sSql, parameters);
                _empresaParams.SetCodigoIBGE();
                //_empresaParams.GetCertificado();
                return _empresaParams;
            }
            catch (Exception ex)
            {
                Log.SalvarLog("ReadByIdEmpresa", ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
