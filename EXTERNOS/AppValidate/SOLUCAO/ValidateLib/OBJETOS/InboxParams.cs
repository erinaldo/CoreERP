using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class InboxParams : ParamsBase 
    {
        private AppLib.Data.Connection _conn;

        private int _idInbox;
        private string _codOrigem;
        private int? _idConfigPOP;
        private int? _idConfigFTP;
        private int? _idConfigDIR;
        private int? _idConsNFE;
        private DateTime _data;
        private string _arquivo;
        private string _texto;
        private string _codEstrutura;
        private string _versao;
        private string _chave;
        private string _hash;
        private DateTime? _dataEmissao;
        private DateTime? _dataLimite;
        private string _emitente;
        private string _cnpjEmitente;
        private string _municipioEmitente;
        private string _ufEmitente;
        private string _destinatario;
        private string _cnpjDestinatario;
        private string _municipioDestinatario;
        private string _ufDestinatario;
        private string _codStatus;
        private string _numeroDocumento;
        private string _log;
        private DateTime? _dataUltimoLog;

        [ParamsAttribute("IDINBOX")]
        [DataMember]
        public int IdInbox
        {
            get
            {
                return this._idInbox;
            }
            set
            {
                this._idInbox = value;
            }
        }

        [ParamsAttribute("CODORIGEM")]
        [DataMember]
        public string CodOrigem
        {
            get
            {
                return this._codOrigem;
            }
            set
            {
                this._codOrigem = value;
            }
        }

        [ParamsAttribute("IDCONFIGPOP")]
        [DataMember]
        public int? IdConfigPOP
        {
            get
            {
                return this._idConfigPOP;
            }
            set
            {
                this._idConfigPOP = value;
            }
        }

        [ParamsAttribute("IDCONFIGFTP")]
        [DataMember]
        public int? IdConfigFTP
        {
            get
            {
                return this._idConfigFTP;
            }
            set
            {
                this._idConfigFTP = value;
            }
        }

        [ParamsAttribute("IDCONFIGDIR")]
        [DataMember]
        public int? IdConfigDIR
        {
            get
            {
                return this._idConfigDIR;
            }
            set
            {
                this._idConfigDIR = value;
            }
        }

        [ParamsAttribute("IDCONSNFE")]
        [DataMember]
        public int? IdConsNFE
        {
            get
            {
                return this._idConsNFE;
            }
            set
            {
                this._idConsNFE = value;
            }
        }

        [ParamsAttribute("DATA")]
        [DataMember]
        public DateTime Data
        {
            get
            {
                return this._data;
            }
            set
            {
                this._data = value;
            }
        }

        [ParamsAttribute("ARQUIVO")]
        [DataMember]
        public string Arquivo
        {
            get
            {
                return this._arquivo;
            }
            set
            {
                this._arquivo = value;
            }
        }

        [ParamsAttribute("TEXTO")]
        [DataMember]
        public string Texto
        {
            get
            {
                return this._texto;
            }
            set
            {
                this._texto = value;
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

        [ParamsAttribute("CHAVE")]
        [DataMember]
        public string Chave
        {
            get
            {
                return this._chave;
            }
            set
            {
                this._chave = value;
            }
        }

        [ParamsAttribute("HASH")]
        [DataMember]
        public string Hash
        {
            get
            {
                return this._hash;
            }
            set
            {
                this._hash = value;
            }
        }

        [ParamsAttribute("DATAEMISSAO")]
        [DataMember]
        public DateTime? DataEmissao
        {
            get
            {
                return this._dataEmissao;
            }
            set
            {
                this._dataEmissao = value;
            }
        }

        [ParamsAttribute("DATALIMITE")]
        [DataMember]
        public DateTime? DataLimite
        {
            get
            {
                return this._dataLimite;
            }
            set
            {
                this._dataLimite = value;
            }
        }

        [ParamsAttribute("EMITENTE")]
        [DataMember]
        public string Emitente
        {
            get
            {
                return this._emitente;
            }
            set
            {
                this._emitente = value;
            }
        }

        [ParamsAttribute("CNPJEMITENTE")]
        [DataMember]
        public string CNPJEmitente
        {
            get
            {
                return this._cnpjEmitente;
            }
            set
            {
                this._cnpjEmitente = value;
            }
        }

        [ParamsAttribute("MUNICIPIOEMITENTE")]
        [DataMember]
        public string MunicipioEmitente
        {
            get
            {
                return this._municipioEmitente;
            }
            set
            {
                this._municipioEmitente = value;
            }
        }

        [ParamsAttribute("UFEMITENTE")]
        [DataMember]
        public string UFEmitente
        {
            get
            {
                return this._ufEmitente;
            }
            set
            {
                this._ufEmitente = value;
            }
        }

        [ParamsAttribute("DESTINATARIO")]
        [DataMember]
        public string Destinatario
        {
            get
            {
                return this._destinatario;
            }
            set
            {
                this._destinatario = value;
            }
        }

        [ParamsAttribute("CNPJDESTINATARIO")]
        [DataMember]
        public string CNPJDestinatario
        {
            get
            {
                return this._cnpjDestinatario;
            }
            set
            {
                this._cnpjDestinatario = value;
            }
        }

        [ParamsAttribute("MUNICIPIODESTINATARIO")]
        [DataMember]
        public string MunicipioDestinatario
        {
            get
            {
                return this._municipioDestinatario;
            }
            set
            {
                this._municipioDestinatario = value;
            }
        }

        [ParamsAttribute("UFDESTINATARIO")]
        [DataMember]
        public string UFDestinatario
        {
            get
            {
                return this._ufDestinatario;
            }
            set
            {
                this._ufDestinatario = value;
            }
        }

        [ParamsAttribute("CODSTATUS")]
        [DataMember]
        public string CodStatus
        {
            get
            {
                return this._codStatus;
            }
            set
            {
                this._codStatus = value;
            }
        }

        [ParamsAttribute("NUMERODOCUMENTO")]
        [DataMember]
        public string NumeroDocumento
        {
            get
            {
                return this._numeroDocumento;
            }
            set
            {
                this._numeroDocumento = value;
            }
        }

        [ParamsAttribute("LOG")]
        [DataMember]
        public string Log
        {
            get
            {
                return this._log;
            }
            set
            {
                this._log = value;
            }
        }

        [ParamsAttribute("DATAULTIMOLOG")]
        [DataMember]
        public DateTime? DataUltimoLog
        {
            get
            {
                return this._dataUltimoLog;
            }
            set
            {
                this._dataUltimoLog = value;
            }
        }

        public string PreparaXML()
        {
            try
            {
                int indexi = this.Texto.IndexOf('<', 0);
                int indexf = this.Texto.IndexOf('>', 0);

                string substituir = this.Texto.Substring(indexi, indexf + 1);

                if (substituir.Contains("xml version"))
                    return this.Texto.Replace(substituir, "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                else
                    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + this.Texto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Classifica()
        {
            bool Flag = true;
            string sSql = string.Empty;

            _conn = AppLib.Context.poolConnection.Get("Start").Clone();

            try
            {
                System.Data.DataSet ds = new Util().StringToDataSet(this.PreparaXML());

                this.CodEstrutura = "XML";
                this.CodStatus = "DES";
                this.Log = null;
                this.DataUltimoLog = null;

                if (ds != null)
                {
                    this.Log = ds.Tables[0].TableName;
                    this.DataUltimoLog = _conn.GetDateTime();

                    #region SE FOR CT-e

                    if (ds.Tables.Contains("cteProc") && this.CodEstrutura.Equals("XML"))
                    {
                        this.Log = null;
                        this.DataUltimoLog = null;


                        this.CodEstrutura = "CT-e";
                        this.CodStatus = "CLA";

                        this.Chave = ds.Tables["infProt"].Rows[0]["chCTe"].ToString().Trim();
                        this.Versao = ds.Tables["infCte"].Rows[0]["versao"].ToString().Trim();
                        this.DataEmissao = new Util().StringToDateTime(ds.Tables["infProt"].Rows[0]["dhRecbto"].ToString().Trim());
                        this.Emitente = ds.Tables["emit"].Rows[0]["xNome"].ToString().Trim();
                        this.CNPJEmitente = ds.Tables["emit"].Rows[0]["CNPJ"].ToString().Trim();
                        this.MunicipioEmitente = ds.Tables["enderEmit"].Rows[0]["xMun"].ToString().Trim();
                        this.UFEmitente = ds.Tables["enderEmit"].Rows[0]["UF"].ToString().Trim();
                        this.NumeroDocumento = ds.Tables["ide"].Rows[0]["nCT"].ToString().Trim();

                        #region TRATAMENTO TOMA03

                        try
                        {
                            this.Destinatario = ds.Tables["dest"].Rows[0]["xNome"].ToString().Trim();
                            this.CNPJDestinatario = "";

                            try
                            {
                                this.CNPJDestinatario = ds.Tables["dest"].Rows[0]["CNPJ"].ToString().Trim();
                            }
                            catch { }

                            try
                            {
                                this.CNPJDestinatario = ds.Tables["dest"].Rows[0]["CPF"].ToString().Trim();
                            }
                            catch { }

                            this.MunicipioDestinatario = ds.Tables["enderDest"].Rows[0]["xMun"].ToString().Trim();
                            this.UFDestinatario = ds.Tables["enderDest"].Rows[0]["UF"].ToString().Trim();

                            //Dirlei
                            //sSql = "SELECT 1 FROM ZCONFIGEMP WHERE REPLACE(REPLACE(REPLACE(CNPJ, '.', ''), '/', ''), '-', '') = ?";
                            sSql = "SELECT 1 FROM GFILIAL WHERE REPLACE(REPLACE(REPLACE(CGCCPF, '.', ''), '/', ''), '-', '') = ?";
                            Flag = _conn.ExecHasRows(sSql, this.CNPJDestinatario);
                            
                            /*
                            if (!Flag)
                            {
                                this.Destinatario = null;
                                this.CNPJDestinatario = null;
                                this.MunicipioDestinatario = null;
                                this.UFDestinatario = null;
                            }
                            */
                        }
                        catch { }

                        #endregion

                        #region TRATAMENTO TOMA04

                        if (!Flag)
                        {

                            try
                            {
                                this.Destinatario = ds.Tables["toma4"].Rows[0]["xNome"].ToString().Trim();
                                this.CNPJDestinatario = "";

                                try
                                {
                                    this.CNPJDestinatario = ds.Tables["toma4"].Rows[0]["CNPJ"].ToString().Trim();
                                }
                                catch { }

                                try
                                {
                                    this.CNPJDestinatario = ds.Tables["toma4"].Rows[0]["CPF"].ToString().Trim();
                                }
                                catch { }

                                this.MunicipioDestinatario = ds.Tables["enderToma"].Rows[0]["xMun"].ToString().Trim();
                                this.UFDestinatario = ds.Tables["enderToma"].Rows[0]["UF"].ToString().Trim();

                                //Dirlei
                                //sSql = "SELECT 1 FROM ZCONFIGEMP WHERE REPLACE(REPLACE(REPLACE(CNPJ, '.', ''), '/', ''), '-', '') = ?";
                                sSql = "SELECT 1 FROM GFILIAL WHERE REPLACE(REPLACE(REPLACE(CGCCPF, '.', ''), '/', ''), '-', '') = ?";
                                Flag = _conn.ExecHasRows(sSql, this.CNPJDestinatario);

                                /*
                                if (!Flag)
                                {
                                    this.Destinatario = null;
                                    this.CNPJDestinatario = null;
                                    this.MunicipioDestinatario = null;
                                    this.UFDestinatario = null;
                                }
                                */
                            }
                            catch { }

                        }

                        #endregion

                        #region REM

                        if (!Flag)
                        {

                            try
                            {
                                this.Destinatario = ds.Tables["rem"].Rows[0]["xNome"].ToString().Trim();
                                this.CNPJDestinatario = ds.Tables["rem"].Rows[0]["CNPJ"].ToString().Trim();
                                this.MunicipioDestinatario = ds.Tables["enderReme"].Rows[0]["xMun"].ToString().Trim();
                                this.UFDestinatario = ds.Tables["enderReme"].Rows[0]["UF"].ToString().Trim();

                                //Dirlei
                               // sSql = "SELECT 1 FROM ZCONFIGEMP WHERE REPLACE(REPLACE(REPLACE(CNPJ, '.', ''), '/', ''), '-', '') = ?";
                               sSql = "SELECT 1 FROM GFILIAL WHERE REPLACE(REPLACE(REPLACE(CGCCPF, '.', ''), '/', ''), '-', '') = ?";
                                Flag = _conn.ExecHasRows(sSql, new Object[] { this.CNPJDestinatario });

                                /*
                                if (!Flag)
                                {
                                    this.Destinatario = null;
                                    this.CNPJDestinatario = null;
                                    this.MunicipioDestinatario = null;
                                    this.UFDestinatario = null;
                                }
                                */
                            }
                            catch { }

                        }

                        #endregion

                        ServiceParams _serviceParams = ServiceParams.Read();
                        this.DataLimite = ((DateTime)this.DataEmissao).AddMinutes(_serviceParams.PrazoCancelamento);

                        //Dirlei
                       // sSql = "SELECT 1 FROM ZCONFIGEMP WHERE REPLACE(REPLACE(REPLACE(CNPJ, '.', ''), '/', ''), '-', '') = ?";
                        sSql = "SELECT 1 FROM GFILIAL WHERE REPLACE(REPLACE(REPLACE(CGCCPF, '.', ''), '/', ''), '-', '') = ?";
                        Flag = _conn.ExecHasRows(sSql, this.CNPJDestinatario);
                        if (!Flag)
                        {
                            throw new Exception("Destinatário não possui licença de uso.");
                        }
                    }

                    #endregion

                    #region SE FOR NF-e

                    if (ds.Tables.Contains("nfeProc") && this.CodEstrutura.Equals("XML"))
                    {
                        this.Log = null;
                        this.DataUltimoLog = null;

                        this.CodEstrutura = "NF-e";
                        this.CodStatus = "CLA";

                        this.Chave = ds.Tables["infProt"].Rows[0]["chNFe"].ToString().Trim();
                        this.Versao = ds.Tables["infNFe"].Rows[0]["versao"].ToString().Trim();

                        this.DataEmissao = new Util().StringToDateTime(ds.Tables["infProt"].Rows[0]["dhRecbto"].ToString().Trim());
                        this.Emitente = ds.Tables["emit"].Rows[0]["xNome"].ToString().Trim();

                        if(ds.Tables["emit"].Columns.Contains("CNPJ"))
                            this.CNPJEmitente = ds.Tables["emit"].Rows[0]["CNPJ"].ToString().Trim();
                        if (ds.Tables["emit"].Columns.Contains("CPF"))
                            this.CNPJEmitente = ds.Tables["emit"].Rows[0]["CPF"].ToString().Trim();

                        this.MunicipioEmitente = ds.Tables["enderEmit"].Rows[0]["xMun"].ToString().Trim();
                        this.UFEmitente = ds.Tables["enderEmit"].Rows[0]["UF"].ToString().Trim();
                        this.NumeroDocumento = ds.Tables["ide"].Rows[0]["nNF"].ToString().Trim();

                        this.Destinatario = ds.Tables["dest"].Rows[0]["xNome"].ToString().Trim();

                        if (ds.Tables["dest"].Columns.Contains("CNPJ"))
                            this.CNPJDestinatario = ds.Tables["dest"].Rows[0]["CNPJ"].ToString().Trim();
                        if (ds.Tables["dest"].Columns.Contains("CPF"))
                            this.CNPJDestinatario = ds.Tables["dest"].Rows[0]["CPF"].ToString().Trim();

                        this.MunicipioDestinatario = ds.Tables["enderDest"].Rows[0]["xMun"].ToString().Trim();
                        this.UFDestinatario = ds.Tables["enderDest"].Rows[0]["UF"].ToString().Trim();

                        ServiceParams _serviceParams = ServiceParams.Read();
                        this.DataLimite = ((DateTime)this.DataEmissao).AddMinutes(_serviceParams.PrazoCancelamento);

                        //Dirlei
                        //sSql = "SELECT 1 FROM ZCONFIGEMP WHERE REPLACE(REPLACE(REPLACE(CNPJ, '.', ''), '/', ''), '-', '') = ?";
                        sSql = "SELECT 1 FROM GFILIAL WHERE REPLACE(REPLACE(REPLACE(CGCCPF, '.', ''), '/', ''), '-', '') = ?";
                        Flag = _conn.ExecHasRows(sSql, this.CNPJDestinatario);
                        if (!Flag)
                        {
                            throw new Exception("Destinatário não possui licença de uso.");
                        }
                    }

                    #endregion

                    #region SE FOR NFS-e

                    if (ds.Tables.Contains("tcIdentificacaoNfse") && this.CodEstrutura.Equals("XML"))
                    {
                        this.Log = null;
                        this.DataUltimoLog = null;

                        this.CodEstrutura = "NFS-e";
                        this.CodStatus = "CLA";

                        //Dirlei
                        //sSql = "SELECT 1 FROM ZCONFIGEMP WHERE REPLACE(REPLACE(REPLACE(CNPJ, '.', ''), '/', ''), '-', '') = ?";
                        sSql = "SELECT 1 FROM GFILIAL WHERE REPLACE(REPLACE(REPLACE(CGCCPF, '.', ''), '/', ''), '-', '') = ?";
                        Flag = _conn.ExecHasRows(sSql, this.CNPJDestinatario);
                        if (!Flag)
                        {
                            throw new Exception("Destinatário não possui licença de uso.");
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                this.CodStatus = "DES";
                this.Log = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                this.DataUltimoLog = _conn.GetDateTime();
            }
        }

        public void ValidaEstrutura(EmpresaParams empresa)
        {
            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                if (new Formato().ValidarDocumentoXML(this.CodEstrutura, this.Versao, this.PreparaXML()))
                {
                    this.CodStatus = "XOK";
                }
                else
                {
                    this.CodStatus = "XIN";
                }
            }
            catch (Exception ex)
            {
                this.CodStatus = "XIN";
                this.Log = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                this.DataUltimoLog = _conn.GetDateTime();
            }
        }

        public void CalcularHash(EmpresaParams empresa)
        {
            this.CodStatus = "HAS";
            this.Hash = new Assinatura().RecalcularHash(this.CodEstrutura, this.PreparaXML(), this.Chave, empresa.IdEmpresa);
        }

        public bool ExisteConsultaNFE()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();

                sSql = @"SELECT CHAVE FROM ZINBOX WHERE CHAVE = ? AND NOT(IDCONSNFE IS NULL)";

                return _conn.ExecHasRows(sSql, this.Chave);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.SalvarLog("ExisteConsultaNFE", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public bool ExisteByChaveNFE()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();

                sSql = @"SELECT CHAVE FROM ZINBOX WHERE CHAVE = ?";

                return _conn.ExecHasRows(sSql, this.Chave);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.SalvarLog("ExisteByChaveNFE", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void DeleteByChaveNFE()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"DELETE FROM ZINBOX WHERE CHAVE = ?";
                _conn.ExecTransaction(sSql, this.Chave);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.SalvarLog("DeleteByChaveNFE", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();

                if (this.IdInbox == 0)
                {
                    sSql = @"INSERT INTO ZINBOX (CODORIGEM, IDCONFIGPOP, IDCONFIGFTP, IDCONFIGDIR, DATA, ARQUIVO, TEXTO, CODESTRUTURA, VERSAO, CHAVE, HASH, DATAEMISSAO,
                            DATALIMITE, EMITENTE, CNPJEMITENTE, MUNICIPIOEMITENTE, UFEMITENTE, DESTINATARIO, CNPJDESTINATARIO, MUNICIPIODESTINATARIO, UFDESTINATARIO, CODSTATUS,
                            NUMERODOCUMENTO, IDCONSNFE, LOG, DATAULTIMOLOG)
                            VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    _conn.ExecTransaction(sSql, this.CodOrigem, this.IdConfigPOP, this.IdConfigFTP, this.IdConfigDIR, this.Data, this.Arquivo, this.Texto, this.CodEstrutura,
                        this.Versao, this.Chave, this.Hash, this.DataEmissao, this.DataLimite, this.Emitente, this.CNPJEmitente, this.MunicipioEmitente, this.UFEmitente,
                        this.Destinatario, this.CNPJDestinatario, this.MunicipioDestinatario, this.UFDestinatario, this.CodStatus, this.NumeroDocumento,
                        this.IdConsNFE, this.Log, this.DataUltimoLog);
                }
                else
                {
                    sSql = @"UPDATE ZINBOX SET CODORIGEM = ?, IDCONFIGPOP = ?, IDCONFIGFTP = ?, IDCONFIGDIR = ?, DATA = ?, ARQUIVO = ?, TEXTO = ?, CODESTRUTURA = ?, VERSAO = ?, 
                            CHAVE = ?, HASH = ?, DATAEMISSAO = ?, DATALIMITE = ?, EMITENTE = ?, CNPJEMITENTE = ?, MUNICIPIOEMITENTE = ?, UFEMITENTE = ?, 
                            DESTINATARIO = ?, CNPJDESTINATARIO = ?, MUNICIPIODESTINATARIO = ?, UFDESTINATARIO = ?, CODSTATUS = ?, NUMERODOCUMENTO = ?, IDCONSNFE = ?,
                            LOG = ?, DATAULTIMOLOG = ?
                            WHERE IDINBOX = ?";

                    _conn.ExecTransaction(sSql, this.CodOrigem, this.IdConfigPOP, this.IdConfigFTP, this.IdConfigDIR, this.Data, this.Arquivo, this.Texto, this.CodEstrutura,
                        this.Versao, this.Chave, this.Hash, this.DataEmissao, this.DataLimite, this.Emitente, this.CNPJEmitente, this.MunicipioEmitente, this.UFEmitente,
                        this.Destinatario, this.CNPJDestinatario, this.MunicipioDestinatario, this.UFDestinatario, this.CodStatus, this.NumeroDocumento, this.IdConsNFE, 
                        this.Log, this.DataUltimoLog, this.IdInbox);
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.SalvarLog("Save", ex.Message);
                throw new Exception(ex.Message);            
            }
        }

        public List<InboxParams> GetArquivosPendentes()
        {
            List<InboxParams> _list = new List<InboxParams>();
            string sSql = @"SELECT * FROM ZINBOX WHERE (CODSTATUS IS NULL OR CODSTATUS = 'IMP') ORDER BY IDINBOX";

            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                System.Data.DataTable _dados = _conn.ExecQuery(sSql);
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    _list.Add(InboxParams.ReadByIDInbox(row["IDINBOX"]));
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(ex.Message);
            }

            return _list;
        }

        public List<InboxParams> GetArquivosClassificados(EmpresaParams empresa)
        {
            List<InboxParams> _list = new List<InboxParams>();
            string sSql = @"SELECT *
FROM ZINBOX
WHERE REPLACE(REPLACE(REPLACE(CNPJDESTINATARIO, '.', ''), '/', ''), '-', '') = REPLACE(REPLACE(REPLACE(?, '.', ''), '/', ''), '-', '')
  AND CODSTATUS = 'CLA'";

            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                System.Data.DataTable _dados = _conn.ExecQuery(sSql, empresa.CNPJ);
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    _list.Add(InboxParams.ReadByIDInbox(row["IDINBOX"]));
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(ex.Message);
            }

            return _list;
        }

        public List<InboxParams> GetArquivosValidos(EmpresaParams empresa)
        {
            List<InboxParams> _list = new List<InboxParams>();
            string sSql = @"SELECT *
FROM ZINBOX
WHERE REPLACE(REPLACE(REPLACE(CNPJDESTINATARIO, '.', ''), '/', ''), '-', '') = REPLACE(REPLACE(REPLACE(?, '.', ''), '/', ''), '-', '')
  AND CODSTATUS = 'XOK'";

            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                System.Data.DataTable _dados = _conn.ExecQuery(sSql, empresa.CNPJ);
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    _list.Add(InboxParams.ReadByIDInbox(row["IDINBOX"]));
                }
                
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(ex.Message);
            }

            return _list;
        }

        public List<InboxParams> GetArquivosPendentesSefaz(EmpresaParams empresa)
        {
            List<InboxParams> _list = new List<InboxParams>();
            string sSql = @"SELECT *
FROM ZINBOX
WHERE REPLACE(REPLACE(REPLACE(CNPJDESTINATARIO, '.', ''), '/', ''), '-', '') = REPLACE(REPLACE(REPLACE(?, '.', ''), '/', ''), '-', '')
  AND ((CODSTATUS = 'HAS') OR (CODSTATUS = 'PRE' AND DATALIMITE <= ?))";

            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                System.Data.DataTable _dados = _conn.ExecQuery(sSql, empresa.CNPJ, _conn.GetDateTime());
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    _list.Add(InboxParams.ReadByIDInbox(row["IDINBOX"]));
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(ex.Message);
            }

            return _list;
        }

        public static InboxParams ReadByIDInbox(params object[] parameters)
        {
            InboxParams _inboxParams = new InboxParams();

            try
            {
                string sSql = @"SELECT * FROM ZINBOX WHERE IDINBOX = ?";                
                _inboxParams.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _inboxParams;
        }
    }
}
