using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeIde : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _cUF;
        private string _cNF;
        private string _natOp;
        private int _indPag;
        private string _mod;
        private string _serie;
        private string _nNF;
        private DateTime _dhEmi;
        private DateTime _dhSaiEnt;
        private int _tpNF;
        private int _idDest;
        private string _cMunFG;
        private int _tpImp;
        private int _tpEmis;
        private int _cDV;
        private int _tpAmb;
        private int _finNFe;
        private int _indFinal;
        private int _indPres;
        private int _procEmi;
        private string _verProc;
        private DateTime? _dhCont;
        private string _xJust;
     

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

        [ParamsAttribute("CUF")]
        [DataMember]
        public int cUF
        {
            get
            {
                return this._cUF;
            }
            set
            {
                this._cUF = value;
            }
        }

        [ParamsAttribute("CNF")]
        [DataMember]
        public string cNF
        {
            get
            {
                return this._cNF;
            }
            set
            {
                this._cNF = value;
            }
        }

        [ParamsAttribute("NATOP")]
        [DataMember]
        public string natOp
        {
            get
            {
                return this._natOp;
            }
            set
            {
                this._natOp = value;
            }
        }

        [ParamsAttribute("INDPAG")]
        [DataMember]
        public int indPag
        {
            get
            {
                return this._indPag;
            }
            set
            {
                this._indPag = value;
            }
        }

        [ParamsAttribute("MOD")]
        [DataMember]
        public string mod
        {
            get
            {
                return this._mod;
            }
            set
            {
                this._mod = value;
            }
        }

        [ParamsAttribute("SERIE")]
        [DataMember]
        public string serie
        {
            get
            {
                return this._serie;
            }
            set
            {
                this._serie = value;
            }
        }

        [ParamsAttribute("NNF")]
        [DataMember]
        public string nNF
        {
            get
            {
                return this._nNF;
            }
            set
            {
                this._nNF = value;
            }
        }

        [ParamsAttribute("DHEMI")]
        [DataMember]
        public DateTime dhEmi
        {
            get
            {
                return this._dhEmi;
            }
            set
            {
                this._dhEmi = value;
            }
        }

        [ParamsAttribute("DHSAIENT")]
        [DataMember]
        public DateTime dhSaiEnt
        {
            get
            {
                return this._dhSaiEnt;
            }
            set
            {
                this._dhSaiEnt = value;
            }
        }

        [ParamsAttribute("TPNF")]
        [DataMember]
        public int tpNF
        {
            get
            {
                return this._tpNF;
            }
            set
            {
                this._tpNF = value;
            }
        }

        [ParamsAttribute("IDDEST")]
        [DataMember]
        public int idDest
        {
            get
            {
                return this._idDest;
            }
            set
            {
                this._idDest = value;
            }
        }

        [ParamsAttribute("CMUNFG")]
        [DataMember]
        public string cMunFG
        {
            get
            {
                return this._cMunFG;
            }
            set
            {
                this._cMunFG = value;
            }
        }

        [ParamsAttribute("TPIMP")]
        [DataMember]
        public int tpImp
        {
            get
            {
                return this._tpImp;
            }
            set
            {
                this._tpImp = value;
            }
        }

        [ParamsAttribute("TPEMIS")]
        [DataMember]
        public int tpEmis
        {
            get
            {
                return this._tpEmis;
            }
            set
            {
                this._tpEmis = value;
            }
        }

        [ParamsAttribute("CDV")]
        [DataMember]
        public int cDV
        {
            get
            {
                return this._cDV;
            }
            set
            {
                this._cDV = value;
            }
        }

        [ParamsAttribute("TPAMB")]
        [DataMember]
        public int tpAmb
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

        [ParamsAttribute("FINNFE")]
        [DataMember]
        public int finNFe
        {
            get
            {
                return this._finNFe;
            }
            set
            {
                this._finNFe = value;
            }
        }

        [ParamsAttribute("INDFINAL")]
        [DataMember]
        public int indFinal
        {
            get
            {
                return this._indFinal;
            }
            set
            {
                this._indFinal = value;
            }
        }

        [ParamsAttribute("INDPRES")]
        [DataMember]
        public int indPres
        {
            get
            {
                return this._indPres;
            }
            set
            {
                this._indPres = value;
            }
        }

        [ParamsAttribute("PROCEMI")]
        [DataMember]
        public int procEmi
        {
            get
            {
                return this._procEmi;
            }
            set
            {
                this._procEmi = value;
            }
        }

        [ParamsAttribute("VERPROC")]
        [DataMember]
        public string verProc
        {
            get
            {
                return this._verProc;
            }
            set
            {
                this._verProc = value;
            }
        }

        [ParamsAttribute("DHCONT")]
        [DataMember]
        public DateTime? dhCont
        {
            get
            {
                return this._dhCont;
            }
            set
            {
                this._dhCont = value;
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

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEIDE WHERE IDOUTBOX = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox))
                {
                    sSql = @"UPDATE ZNFEIDE SET CUF = ?, CNF = ?, NATOP = ?, INDPAG = ?, MOD = ?, SERIE = ?, NNF = ?, DHEMI = ?, DHSAIENT = ?, TPNF = ?, IDDEST = ?, CMUNFG = ?,
                                TPIMP = ?, TPEMIS = ?, CDV = ?, TPAMB = ?, FINNFE = ?, INDFINAL = ?, INDPRES = ?, PROCEMI = ?, VERPROC = ?, DHCONT = ?, XJUST = ? 
                                WHERE IDOUTBOX = ?";
                    _conn.ExecQuery(sSql, this.cUF, this.cNF, this.natOp, this.indPag, this.mod, this.serie, this.nNF, this.dhEmi, this.dhSaiEnt, this.tpNF, this.idDest, this.cMunFG,
                        this.tpImp, this.tpEmis, this.cDV, this.tpAmb, this.finNFe, this.indFinal, this.indPres, this.procEmi, this.verProc, this.dhCont, this.xJust,
                        this.IdOutbox);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEIDE (IDOUTBOX, CUF, CNF, NATOP, INDPAG, MOD, SERIE, NNF, DHEMI, DHSAIENT, TPNF, IDDEST, CMUNFG, TPIMP, TPEMIS, CDV, TPAMB, FINNFE, 
                                INDFINAL, INDPRES, PROCEMI, VERPROC, DHCONT, XJUST)
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.cUF, this.cNF, this.natOp, this.indPag, this.mod, this.serie, this.nNF, this.dhEmi, this.dhSaiEnt, this.tpNF, 
                        this.idDest, this.cMunFG, this.tpImp, this.tpEmis, this.cDV, this.tpAmb, this.finNFe, this.indFinal, this.indPres, this.procEmi, this.verProc, 
                        this.dhCont, this.xJust);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeIde.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeIde ReadByIDOutbox(params object[] parameters)
        {
            NFeIde _nfeIde = new NFeIde();

            try
            {
                string sSql = @"SELECT * FROM ZNFEIDE WHERE IDOUTBOX = ?";
                _nfeIde.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeIde;
        }
    }
}
