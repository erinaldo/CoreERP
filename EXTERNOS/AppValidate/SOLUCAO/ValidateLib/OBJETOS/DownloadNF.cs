using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class DownloadNF : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private EmpresaParams _empresaParams;
        private int _idDownloadNF;
        private int _idEmpresa;
        private string _chNFe;
        private DateTime _dataInclusao;
        private string _usuarioInclusao;
        private string _xml;
        private DateTime _dataExecucao;
        private string _status;
        private string _retorno;
        private string _xmlEnvio;
        private int _cStat;
        private string _xmlRetorno;

        [DataMember]
        public EmpresaParams EmpresaPar
        {
            get
            {
                return this._empresaParams;
            }
            set
            {
                this._empresaParams = value;
            }
        }

        [ParamsAttribute("IDDOWNLOADNF")]
        [DataMember]
        public int idDownloadNF
        {
            get
            {
                return this._idDownloadNF;
            }
            set
            {
                this._idDownloadNF = value;
            }
        }

        [ParamsAttribute("IDEMPRESA")]
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

        [ParamsAttribute("CHNFE")]
        [DataMember]
        public string chNFe
        {
            get
            {
                return this._chNFe;
            }
            set
            {
                this._chNFe = value;
            }
        }

        [ParamsAttribute("DATAINCLUSAO")]
        [DataMember]
        public DateTime DataInclusao
        {
            get
            {
                return this._dataInclusao;
            }
            set
            {
                this._dataInclusao = value;
            }
        }

        [ParamsAttribute("USUARIOINCLUSAO")]
        [DataMember]
        public string UsuarioInclusao
        {
            get
            {
                return this._usuarioInclusao;
            }
            set
            {
                this._usuarioInclusao = value;
            }
        }

        [ParamsAttribute("XML")]
        [DataMember]
        public string XML
        {
            get
            {
                return this._xml;
            }
            set
            {
                this._xml = value;
            }
        }

        [ParamsAttribute("DATAEXECUCAO")]
        [DataMember]
        public DateTime DataExecucao
        {
            get
            {
                return this._dataExecucao;
            }
            set
            {
                this._dataExecucao = value;
            }
        }

        [ParamsAttribute("STATUS")]
        [DataMember]
        public string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        [ParamsAttribute("RETORNO")]
        [DataMember]
        public string Retorno
        {
            get
            {
                return this._retorno;
            }
            set
            {
                this._retorno = value;
            }
        }

        [ParamsAttribute("XMLENVIO")]
        [DataMember]
        public string xmlEnvio
        {
            get
            {
                return this._xmlEnvio;
            }
            set
            {
                this._xmlEnvio = value;
            }
        }

        [ParamsAttribute("CSTAT")]
        [DataMember]
        public int cStat
        {
            get
            {
                return this._cStat;
            }
            set
            {
                this._cStat = value;
            }
        }

        [ParamsAttribute("XMLRETORNO")]
        [DataMember]
        public string xmlRetorno
        {
            get
            {
                return this._xmlRetorno;
            }
            set
            {
                this._xmlRetorno = value;
            }
        }

        public void RegistraRetornoDownload(SucessoErro Status)
        {
            try
            {
                string sStatus = (Status == SucessoErro.Sucesso) ? "S" : "E";
                string sSql = string.Empty;

                sSql = "UPDATE ZDOWNLOADNF SET STATUS = ?, RETORNO = ?, DATAEXECUCAO = ?, XML = ?, XMLENVIO = ?, CSTAT = ?, XMLRETORNO = ?  WHERE IDDOWNLOADNF = ?";
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                conn.ExecTransaction(sSql, sStatus, this.Retorno, conn.GetDateTime(), this.XML, this.xmlEnvio, this.cStat, this.xmlRetorno, this.idDownloadNF);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.SalvarLog("RegistraRetornoDownload", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void InserirEventoDownload()
        { 
            try
            {
                bool Flag = false;
                string sSql = string.Empty;

                sSql = "SELECT CHNFE FROM ZDOWNLOADNF WHERE CHNFE = ?";
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                if (conn.ExecHasRows(sSql, this.chNFe))
                {
                    sSql = "SELECT CHNFE FROM ZDOWNLOADNF WHERE CHNFE = ? AND STATUS = 'P'";
                    if (conn.ExecHasRows(sSql, this.chNFe))
                    {
                        throw new Exception("A chave " + this.chNFe + " já possui download pendente de processamento.");
                    }
                    else
                    {
                        Flag = true;
                    }
                }
                else
                {
                    Flag = true;
                }

                if (Flag)
                {
                    sSql = @"INSERT INTO ZDOWNLOADNF (IDEMPRESA, CHNFE, DATAINCLUSAO, USUARIOINCLUSAO, STATUS)
                                VALUES (?, ?, ?, ?, ?)";
                    conn.ExecTransaction(sSql, new Object[] { this.IdEmpresa, this.chNFe, conn.GetDateTime(), AppLib.Context.Usuario, "P" });
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.SalvarLog("InserirEventoDownload", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public List<DownloadNF> GetDownloadPendentes(int IdEmpresa)
        {
            List<DownloadNF> _list = new List<DownloadNF>();
            string sSql = @"SELECT * FROM ZDOWNLOADNF WHERE STATUS = 'P' AND DATAEXECUCAO IS NULL AND IDEMPRESA = ?";

            _conn = AppLib.Context.poolConnection.Get("Start").Clone();
            System.Data.DataTable _dados = _conn.ExecQuery(sSql, IdEmpresa);
            foreach (System.Data.DataRow row in _dados.Rows)
            {
                _list.Add(DownloadNF.ReadByIdDownloadNF(row["IDDOWNLOADNF"]));
            }

            return _list;
        }

        public static DownloadNF ReadByIdDownloadNF(params object[] parameters)
        {
            string sSql = @"SELECT * FROM ZDOWNLOADNF WHERE IDDOWNLOADNF = ?";
            DownloadNF _downloadNF = new DownloadNF();
            _downloadNF.ReadFromCommand(sSql, parameters);
            
            _downloadNF.EmpresaPar = EmpresaParams.ReadByIdEmpresa(_downloadNF.IdEmpresa);
            return _downloadNF;
        }
    }
}
