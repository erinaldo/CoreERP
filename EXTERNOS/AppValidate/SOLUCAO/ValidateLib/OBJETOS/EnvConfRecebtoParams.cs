using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class EnvConfRecebtoParams : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private EmpresaParams _empresaParams;
        private int _idEventoMDNF;
        private int _idEmpresa;
        private string _chNFe;
        private string _tpEvento;
        private string _xJust;
        private DateTime _dataInclusao;
        private string _usuarioInclusao;
        private DateTime _dataExecucao;
        private string _status;
        private string _retorno;
        private string _nProt;
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

        [ParamsAttribute("IDEVENTOMDNF")]
        [DataMember]
        public int idEventoMDNF
        {
            get
            {
                return this._idEventoMDNF;
            }
            set
            {
                this._idEventoMDNF = value;
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

        [ParamsAttribute("TPEVENTO")]
        [DataMember]
        public string tpEvento
        {
            get
            {
                return this._tpEvento;
            }
            set
            {
                this._tpEvento = value;
            }
        }

        [ParamsAttribute("JUSTIFICATIVA")]
        [DataMember]
        public string xJust
        {
            get
            {
                return this._xJust;
            }
            set
            {
                this._xJust = value;
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

        [ParamsAttribute("NPROT")]
        [DataMember]
        public string nProt
        {
            get
            {
                return this._nProt;
            }
            set
            {
                this._nProt = value;
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

        public void RegistraRetornoManifestacaoDestinatario(SucessoErro Status)
        {
            try
            {
                string sStatus = (Status == SucessoErro.Sucesso) ? "S" : "E";
                string sSql = string.Empty;

                sSql = "UPDATE ZEVENTOMDNF SET STATUS = ?, RETORNO = ?, DATAEXECUCAO = ?, NPROT = ?, XMLENVIO = ?, CSTAT = ?, XMLRETORNO = ? WHERE IDEVENTOMDNF = ?";
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                conn.ExecTransaction(sSql, sStatus, this.Retorno, conn.GetDateTime(), this.nProt, this.xmlEnvio, this.cStat, this.xmlRetorno, this.idEventoMDNF);

                if (Status == SucessoErro.Sucesso)
                {
                    if (this.tpEvento.Equals("210200") || this.tpEvento.Equals("210210"))
                    {
                        ValidateLib.DownloadNF _download = new ValidateLib.DownloadNF();
                        _download.IdEmpresa = this.IdEmpresa;
                        _download.chNFe = this.chNFe;
                        _download.InserirEventoDownload();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InserirEventoManifestacaoDestinatario()
        {
            try
            {
                bool Flag = false;
                string sSql = string.Empty;

                ConsNFeDest _ConsNFeDest = ConsNFeDest.ReadByChNFe(this.chNFe);
                if (_ConsNFeDest.chNFe == this.chNFe)
                {
                    sSql = "SELECT CHNFE FROM ZEVENTOMDNF WHERE CHNFE = ?";
                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                    if (conn.ExecHasRows(sSql, this.chNFe))
                    {
                        sSql = "SELECT CHNFE FROM ZEVENTOMDNF WHERE CHNFE = ? AND STATUS = 'P'";
                        if (conn.ExecHasRows(sSql, this.chNFe))
                        {
                            throw new Exception("A chave " + this.chNFe + " já possui evento de manifestação pendente de processamento.");
                        }
                        else
                        {
                            sSql = "SELECT CHNFE FROM ZEVENTOMDNF WHERE CHNFE = ? AND STATUS = 'S' AND TPEVENTO = ?";
                            if (conn.ExecHasRows(sSql, this.chNFe, this.tpEvento))
                            {
                                throw new Exception("A chave " + this.chNFe + " já possui evento de manifestação do tipo " + this.tpEvento + " processada.");
                            }
                            else
                            {
                                Flag = true;
                            }
                        }
                    }
                    else
                    {
                        Flag = true;
                    }

                    if (Flag)
                    {
                        sSql = @"INSERT INTO ZEVENTOMDNF (IDEMPRESA, CHNFE, TPEVENTO, JUSTIFICATIVA, DATAINCLUSAO, USUARIOINCLUSAO, STATUS)
                                VALUES (?, ?, ?, ?, ?, ?, ?)";
                        conn.ExecTransaction(sSql, new Object[] { this.IdEmpresa, this.chNFe, this.tpEvento, this.xJust, conn.GetDateTime(), AppLib.Context.Usuario, "P" });
                    }
                }
                else
                {
                    throw new Exception("A chave " + this.chNFe + " não possui registro de consulta do destinatário.");
                }

            }
            catch (Exception ex)
            {
                ValidateLib.Log.SalvarLog("InserirEventoManifestacaoDestinatario", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public List<EnvConfRecebtoParams> GetEventosPendentes(int IdEmpresa)
        {
            List<EnvConfRecebtoParams> _list = new List<EnvConfRecebtoParams>();
            string sSql = @"SELECT TOP 20 * FROM ZEVENTOMDNF WHERE STATUS = 'P' AND DATAEXECUCAO IS NULL AND IDEMPRESA = ?";

            _conn = AppLib.Context.poolConnection.Get("Start").Clone();
            System.Data.DataTable _dados = _conn.ExecQuery(sSql, IdEmpresa);
            foreach (System.Data.DataRow row in _dados.Rows)
            {
                _list.Add(EnvConfRecebtoParams.ReadByIdEventoMDNF(row["IDEVENTOMDNF"]));
            }

            return _list;
        }

        public static EnvConfRecebtoParams ReadByIdEventoMDNF(params object[] parameters)
        {
            string sSql = @"SELECT * FROM ZEVENTOMDNF WHERE IDEVENTOMDNF = ?";
            EnvConfRecebtoParams _envConfRecebto = new EnvConfRecebtoParams();
            _envConfRecebto.ReadFromCommand(sSql, parameters);
            _envConfRecebto.EmpresaPar = EmpresaParams.ReadByIdEmpresa(_envConfRecebto.IdEmpresa);
            return _envConfRecebto;
        }
    }
}
