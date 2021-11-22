using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class ConsNFeDestCCe : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idConsNFe;
        private int _idEmpresa;
        private string _NSU;
        private string _chNFe;
        private DateTime _dhEvento;
        private string _tpEvento;
        private string _nSeqEvento;
        private string _descEvento;
        private string _xCorrecao;
        private string _tpNF;
        private DateTime _dhRecbto;

        [ParamsAttribute("IDCONSNFE")]
        [DataMember]
        public int IdConsNFe
        {
            get
            {
                return this._idConsNFe;
            }
            set
            {
                this._idConsNFe = value;
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

        [ParamsAttribute("NSU")]
        [DataMember]
        public string NSU
        {
            get
            {
                return this._NSU;
            }
            set
            {
                this._NSU = value;
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

        [ParamsAttribute("DHEVENTO")]
        [DataMember]
        public DateTime dhEvento
        {
            get
            {
                return this._dhEvento;
            }
            set
            {
                this._dhEvento = value;
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

        [ParamsAttribute("NSEQEVENTO")]
        [DataMember]
        public string nSeqEvento
        {
            get
            {
                return this._nSeqEvento;
            }
            set
            {
                this._nSeqEvento = value;
            }
        }


        [ParamsAttribute("DESCEVENTO")]
        [DataMember]
        public string descEvento
        {
            get
            {
                return this._descEvento;
            }
            set
            {
                this._descEvento = value;
            }
        }

        [ParamsAttribute("XCORRECAO")]
        [DataMember]
        public string xCorrecao
        {
            get
            {
                return this._xCorrecao;
            }
            set
            {
                this._xCorrecao = value;
            }
        }

        [ParamsAttribute("tpNF")]
        [DataMember]
        public string tpNF
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
 

        [ParamsAttribute("dhRecbto")]
        [DataMember]
        public DateTime dhRecbto
        {
            get
            {
                return this._dhRecbto;
            }
            set
            {
                this._dhRecbto = value;
            }
        }

        public void Save()
        {
            if (this.IdConsNFe == 0)
            {
                string sSql = @"INSERT INTO ZCONSNFECC (IDEMPRESA, NSU, CHNFE, DHEVENTO, TPEVENTO, NSEQEVENTO, DESCEVENTO, XCORRECAO, TPNF, DHRECBTO)
                                                            VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                _conn.ExecTransaction(sSql, new Object[] { this.IdEmpresa, this.NSU, this.chNFe, this.dhEvento, this.tpEvento, this.nSeqEvento, this.descEvento, this.xCorrecao, this.tpNF, this.dhRecbto });
            }        
        }

        public static ConsNFeDestCCe ReadByIdConsNFe(params object[] parameters)
        {
            string sSql = @"SELECT * FROM ZCONSNFECC WHERE IDCONSNFE = ?";
            ConsNFeDestCCe _consNFeDestCCe = new ConsNFeDestCCe();
            _consNFeDestCCe.ReadFromCommand(sSql, parameters);
            return _consNFeDestCCe;
        }
    }
}