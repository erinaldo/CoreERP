using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeDoc : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private string _chave;
        private DateTime? _datanrec;
        private string _nrec;
        private DateTime? _datanProt;
        private string _nProt;
        private string _xmlRec;
        private string _xmlProt;
        private string _xmlNFe;
        private string _xmlEnv;

        private NFeIde _nfeIde;
        private NFeEmit _nfeEmit;
        private NFeDest _nfeDest;
        private NFeRetirada _nfeRetirada;
        private NFeEntrega _nfeEntrega;
        private List<NFeDet> _nfeDet;
        private NFeTotal _nfeTotal;
        private NFetransp _nfetransp;
        private NFecobr _nfecobr;
        private NFeinfAdic _nfeinfAdic;
        public OBJETOS.NFeExporta nfeExporta { get; set; }

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

        [ParamsAttribute("DATANREC")]
        [DataMember]
        public DateTime? DatanRec 
        {
            get
            {
                return this._datanrec;
            }
            set
            {
                this._datanrec = value;
            }
        }

        [ParamsAttribute("NREC")]
        [DataMember]
        public string nRec
        {
            get
            {
                return this._nrec;
            }
            set
            {
                this._nrec = value;
            }
        }

        [ParamsAttribute("DATANPROT")]
        [DataMember]
        public DateTime? DatanProt
        {
            get
            {
                return this._datanProt;
            }
            set
            {
                this._datanProt = value;
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

        [ParamsAttribute("XMLREC")]
        [DataMember]
        public string XmlRec
        {
            get
            {
                return this._xmlRec;
            }
            set
            {
                this._xmlRec = value;
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

        [ParamsAttribute("XMLNFE")]
        [DataMember]
        public string XmlNFe
        {
            get
            {
                return this._xmlNFe;
            }
            set
            {
                this._xmlNFe = value;
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

        public NFeIde nfeIde
        {
            get
            {
                return this._nfeIde;
            }
            set
            {
                this._nfeIde = value;
            }
        }

        public NFeEmit nfeEmit
        {
            get
            {
                return this._nfeEmit;
            }
            set
            {
                this._nfeEmit = value;
            }
        }

        public NFeDest nfeDest
        {
            get
            {
                return this._nfeDest;
            }
            set
            {
                this._nfeDest = value;
            }
        }

        public NFeRetirada nfeRetirada
        {
            get
            {
                return this._nfeRetirada;
            }
            set
            {
                this._nfeRetirada = value;
            }
        }

        public NFeEntrega nfeEntrega
        {
            get
            {
                return this._nfeEntrega;
            }
            set
            {
                this._nfeEntrega = value;
            }
        }

        public List<NFeDet> nfeDet
        {
            get
            {
                return this._nfeDet;
            }
            set
            {
                this._nfeDet = value;
            }
        }

        public NFeTotal nfeTotal
        {
            get
            {
                return this._nfeTotal;
            }
            set
            {
                this._nfeTotal = value;
            }
        }

        public NFetransp nfetransp
        {
            get
            {
                return this._nfetransp;
            }
            set
            {
                this._nfetransp = value;
            }
        }

        public NFecobr nfecobr
        {
            get
            {
                return this._nfecobr;
            }
            set
            {
                this._nfecobr = value;
            }
        }

        public NFeinfAdic nfeinfAdic
        {
            get
            {
                return this._nfeinfAdic;
            }
            set
            {
                this._nfeinfAdic = value;
            }
        }

        private List<NFeDet> ReadDet(params object[] parameters)
        {
            List<NFeDet> lnfeDet = new List<NFeDet>();

            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();

                string sSql = @"SELECT * FROM ZNFEDET WHERE IDOUTBOX = ?";
                System.Data.DataTable _dados = _conn.ExecQuery(sSql, parameters);
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    lnfeDet.Add(NFeDet.ReadNFeDet(row["IDOUTBOX"], row["NITEM"]));
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }

            return lnfeDet;
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEDOC WHERE IDOUTBOX = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox))
                {
                    sSql = @"UPDATE ZNFEDOC SET CHAVE = ?, DATANREC = ?, NREC = ?, DATANPROT = ?, NPROT = ?, XMLREC = ?, XMLPROT = ?, XMLNFE = ?, XMLENV = ? WHERE IDOUTBOX = ?";
                    _conn.ExecTransaction(sSql, this.Chave, this.DatanRec, this.nRec, this.DatanProt, this.nProt, this.XmlRec, this.XmlProt, this.XmlNFe, this.XmlEnv, this.IdOutbox);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEDOC (IDOUTBOX, CHAVE, DATANREC, NREC, DATANPROT, NPROT, XMLREC, XMLPROT, XMLNFE, XMLENV) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecTransaction(sSql, this.IdOutbox, this.Chave, this.DatanRec, this.nRec, this.DatanProt, this.nProt, this.XmlRec, this.XmlProt, this.XmlNFe, this.XmlEnv);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeDoc.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeDoc ReadByIDOutbox(params object[] parameters)
        {
            NFeDoc _nfeDoc = new NFeDoc();

            try
            {
                string sSql = @"SELECT * FROM ZNFEDOC WHERE IDOUTBOX = ?";
                _nfeDoc.ReadFromCommand(sSql, parameters);
                _nfeDoc.Chave = _nfeDoc._chave;
                _nfeDoc.nfeIde = NFeIde.ReadByIDOutbox(parameters);
                _nfeDoc.nfeEmit = NFeEmit.ReadByIDOutbox(parameters);
                _nfeDoc.nfeDest = NFeDest.ReadByIDOutbox(parameters);
                _nfeDoc.nfeRetirada = NFeRetirada.ReadByIDOutbox(parameters);
                _nfeDoc.nfeEntrega = NFeEntrega.ReadByIDOutbox(parameters);
                _nfeDoc.nfeDet = _nfeDoc.ReadDet(parameters);
                _nfeDoc.nfeTotal = NFeTotal.ReadNFeTotal(parameters);
                _nfeDoc.nfetransp = NFetransp.ReadNFetransp(parameters);
                _nfeDoc.nfecobr = NFecobr.ReadNFecobr(parameters);
                _nfeDoc.nfeinfAdic = NFeinfAdic.ReadNFeinfAdic(parameters);
                _nfeDoc.nfeExporta = OBJETOS.NFeExporta.ReadByIDOutbox(Convert.ToInt32(_nfeDoc.IdOutbox));

            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(ex.Message);
            }
            return _nfeDoc;
        }
    }
}
