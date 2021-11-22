using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class OutBoxParams : ParamsBase 
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private DateTime _data;
        private string _codEstrutura;
        private string _cnpjEmitente;
        private string _codStatus;
        private string _log;
        private DateTime? _dataUltimoLog;

        private NFeDoc _nfeDoc;
        private NFSeDoc _nfseDoc;
        private CTeDoc _cteDoc;

        [ParamsAttribute("IDOUTBOX")]
        [DataMember]
        public int IdOutbox
        {
            get
            {
                return this._idOutbox;
            }
            set
            {
                this._idOutbox = value;
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

        public NFeDoc nfeDoc
        {
            get
            {
                return this._nfeDoc;
            }
            set
            {
                this._nfeDoc = value;
            }
        }

        public NFSeDoc nfseDoc
        {
            get
            {
                return this._nfseDoc;
            }
            set
            {
                this._nfseDoc = value;
            }
        }

        public CTeDoc cteDoc
        {
            get
            {
                return this._cteDoc;
            }
            set
            {
                this._cteDoc = value;
            }
        }

        public List<OutBoxParams> GetArquivosPendentesEnvio(EmpresaParams empresa)
        {
            List<OutBoxParams> _list = new List<OutBoxParams>();
            string sSql = @"SELECT * FROM ZOUTBOX
WHERE REPLACE(REPLACE(REPLACE(CNPJEMITENTE, '.', ''), '/', ''), '-', '') = REPLACE(REPLACE(REPLACE(?, '.', ''), '/', ''), '-', '') 
    AND CODSTATUS = 'ENV'";

            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                System.Data.DataTable _dados = _conn.ExecQuery(sSql, empresa.CNPJ);
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    _list.Add(OutBoxParams.ReadByIDOutbox(row["IDOUTBOX"]));
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.SalvarLog("OutBoxParams.GetArquivosPendentesEnvio", ex.Message);
            }

            return _list;
        }

        public List<OutBoxParams> GetArquivosPendentesConsulta(EmpresaParams empresa)
        {
            List<OutBoxParams> _list = new List<OutBoxParams>();
            string sSql = @"SELECT * FROM ZOUTBOX
WHERE REPLACE(REPLACE(REPLACE(CNPJEMITENTE, '.', ''), '/', ''), '-', '') = REPLACE(REPLACE(REPLACE(?, '.', ''), '/', ''), '-', '') 
    AND CODSTATUS = 'CON'";

            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                System.Data.DataTable _dados = _conn.ExecQuery(sSql, empresa.CNPJ);
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    _list.Add(OutBoxParams.ReadByIDOutbox(row["IDOUTBOX"]));
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.SalvarLog("OutBoxParams.GetArquivosPendentesConsulta", ex.Message);
            }

            return _list;
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();

                if (this.IdOutbox == 0)
                {
                    AutoInc _autoinc = new AutoInc();
                    this.IdOutbox = _autoinc.GetNewAutoInc("IDOUTBOX");

                    sSql = @"INSERT INTO ZOUTBOX (IDOUTBOX, DATA, CODESTRUTURA, CNPJEMITENTE, CODSTATUS, LOG, DATAULTIMOLOG) VALUES (?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecTransaction(sSql, this.IdOutbox, this.Data, this.CodEstrutura, this.CNPJEmitente, this.CodStatus, this.Log, this.DataUltimoLog);
                }
                else
                {
                    sSql = @"UPDATE ZOUTBOX SET DATA = ?, CODESTRUTURA = ?, CNPJEMITENTE = ?, CODSTATUS = ?, LOG = ?, DATAULTIMOLOG = ? WHERE IDOUTBOX = ?";
                    _conn.ExecTransaction(sSql, this.Data, this.CodEstrutura, this.CNPJEmitente, this.CodStatus, this.Log, this.DataUltimoLog, this.IdOutbox);
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.SalvarLog("OutBoxParams.Save", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public static OutBoxParams ReadByIDOutbox(params object[] parameters)
        {
            OutBoxParams _outboxParams = new OutBoxParams();
            string sSql = @"SELECT * FROM ZOUTBOX WHERE IDOUTBOX = ?";
            _outboxParams.ReadFromCommand(sSql, parameters);
            if (_outboxParams.CodEstrutura.Equals("NF-e"))
                _outboxParams.nfeDoc = NFeDoc.ReadByIDOutbox(parameters);
            if (_outboxParams.CodEstrutura.Equals("NFS-e"))
                _outboxParams.nfseDoc = NFSeDoc.ReadByIDOutbox(parameters);
            if (_outboxParams.CodEstrutura.Equals("CT-e"))
                _outboxParams.cteDoc = CTeDoc.ReadByIDOutbox(parameters);

            return _outboxParams;
        }
    }
}
