using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class EventoCanNFParams : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idEventoCanNF;
        private DateTime _data;
        private string _cnpjEmitente;
        private string _chnfe;
        private string _nProtAut;
        private string _xJust;
        private string _codStatus;
        private string _log;
        private DateTime? _dataUltimoLog;
        private int _idOutbox;
        private string _xmlEnv;
        private string _nProt;
        private string _xmlProt;

        [ParamsAttribute("IDEVENTOCANNF")]
        [DataMember]
        public int IdEventoCanNF
        {
            get
            {
                return this._idEventoCanNF;
            }
            set
            {
                this._idEventoCanNF = value;
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

        [ParamsAttribute("CHNFE")]
        [DataMember]
        public string chNFe
        {
            get
            {
                return this._chnfe;
            }
            set
            {
                this._chnfe = value;
            }
        }

        [ParamsAttribute("NPROTAUT")]
        [DataMember]
        public string nProtAut
        {
            get
            {
                return this._nProtAut;
            }
            set
            {
                this._nProtAut = value;
            }
        }

        [ParamsAttribute("XJUST")]
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

        [ParamsAttribute("XMLENV")]
        [DataMember]
        public string XmlEnv
        {
            get
            {
                return this._xmlEnv;
            }
            set
            {
                this._xmlEnv = value;
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

        [ParamsAttribute("XMLPROT")]
        [DataMember]
        public string XmlProt
        {
            get
            {
                return this._xmlProt;
            }
            set
            {
                this._xmlProt = value;
            }
        }

        public List<EventoCanNFParams> GetArquivosPendentes(EmpresaParams empresa)
        {
            List<EventoCanNFParams> _list = new List<EventoCanNFParams>();
            string sSql = @"SELECT * FROM ZEVENTOCANNF
WHERE REPLACE(REPLACE(REPLACE(CNPJEMITENTE, '.', ''), '/', ''), '-', '') = REPLACE(REPLACE(REPLACE(?, '.', ''), '/', ''), '-', '') 
    AND CODSTATUS = 'ENV'";

            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                System.Data.DataTable _dados = _conn.ExecQuery(sSql, empresa.CNPJ);
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    _list.Add(EventoCanNFParams.ReadByIDEventoCanNF(row["IDEVENTOCANNF"]));
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.SalvarLog("EventoCanNFParams.GetArquivosPendentes", ex.Message);
            }

            return _list;
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();

                if (this.IdEventoCanNF == 0)
                {
                    sSql = @"INSERT INTO ZEVENTOCANNF (DATA, CNPJEMITENTE, CHNFE, NPROTAUT, XJUST, CODSTATUS, LOG, DATAULTIMOLOG, IDOUTBOX, XMLENV, NPROT, XMLPROT) 
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecTransaction(sSql, this.Data, this.CNPJEmitente, this.chNFe, this.nProtAut, this.xJust, this.CodStatus, this.Log, this.DataUltimoLog, this.IdOutbox, 
                        this.XmlEnv, this.nProt, this.XmlProt);
                }
                else
                {
                    sSql = @"UPDATE ZEVENTOCANNF SET DATA = ?, CNPJEMITENTE = ?, CHNFE = ?, NPROTAUT = ?, XJUST = ?, CODSTATUS = ?, LOG = ?, DATAULTIMOLOG = ?, IDOUTBOX = ?, 
                                XMLENV = ?, NPROT = ?, XMLPROT = ?
                                WHERE IDEVENTOCANNF = ?";
                    _conn.ExecTransaction(sSql, this.Data, this.CNPJEmitente, this.chNFe, this.nProtAut, this.xJust, this.CodStatus, this.Log, this.DataUltimoLog, this.IdOutbox, 
                        this.XmlEnv, this.nProt, this.XmlProt, this.IdEventoCanNF);
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.SalvarLog("EventoCanNFParams.Save", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public static EventoCanNFParams ReadByIDEventoCanNF(params object[] parameters)
        {
            EventoCanNFParams _eventoCanNFParams = new EventoCanNFParams();
            string sSql = @"SELECT * FROM ZEVENTOCANNF WHERE IDEVENTOCANNF = ?";
            _eventoCanNFParams.ReadFromCommand(sSql, parameters);
            return _eventoCanNFParams;
        }

        public static EventoCanNFParams ReadyByIdOutBox(params object[] parameters)
        {
            EventoCanNFParams _eventoCanNFParams = new EventoCanNFParams();
            string sSql = @"SELECT * FROM ZEVENTOCANNF WHERE IDOUTBOX = ?";
            _eventoCanNFParams.ReadFromCommand(sSql, parameters);
            return _eventoCanNFParams;
        }


    }
}
