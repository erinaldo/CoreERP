using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeISSQN : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private decimal _vBC;
        private decimal _vAliq;
        private decimal _vISSQN;
        private string _cMunFG;
        private string _cListServ;
        private decimal _vDeducao;
        private decimal _vOutro;
        private decimal _vDescIncond;
        private decimal _vDescCond;
        private decimal _vISSRet;
        private int _indISS;
        private string _cServico;
        private string _cMun;
        private string _cPais;
        private string _nProcesso;
        private int _indIncentivo;

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

        [ParamsAttribute("NITEM")]
        [DataMember]
        public int nItem
        {
            get
            {
                return this._nItem;
            }
            set
            {
                this._nItem = value;
            }
        }

        [ParamsAttribute("VBC")]
        [DataMember]
        public decimal vBC
        {
            get
            {
                return this._vBC;
            }
            set
            {
                this._vBC = value;
            }
        }

        [ParamsAttribute("VALIQ")]
        [DataMember]
        public decimal vAliq
        {
            get
            {
                return this._vAliq;
            }
            set
            {
                this._vAliq = value;
            }
        }

        [ParamsAttribute("VISSQN")]
        [DataMember]
        public decimal vISSQN
        {
            get
            {
                return this._vISSQN;
            }
            set
            {
                this._vISSQN = value;
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

        [ParamsAttribute("CLISTSERV")]
        [DataMember]
        public string cListServ
        {
            get
            {
                return this._cListServ;
            }
            set
            {
                this._cListServ = value;
            }
        }

        [ParamsAttribute("VDEDUCAO")]
        [DataMember]
        public decimal vDeducao
        {
            get
            {
                return this._vDeducao;
            }
            set
            {
                this._vDeducao = value;
            }
        }

        [ParamsAttribute("VOUTRO")]
        [DataMember]
        public decimal vOutro
        {
            get
            {
                return this._vOutro;
            }
            set
            {
                this._vOutro = value;
            }
        }

        [ParamsAttribute("VDESCINCOND")]
        [DataMember]
        public decimal vDescIncond
        {
            get
            {
                return this._vDescIncond;
            }
            set
            {
                this._vDescIncond = value;
            }
        }

        [ParamsAttribute("VDESCCOND")]
        [DataMember]
        public decimal vDescCond
        {
            get
            {
                return this._vDescCond;
            }
            set
            {
                this._vDescCond = value;
            }
        }

        [ParamsAttribute("VISSRET")]
        [DataMember]
        public decimal vISSRet
        {
            get
            {
                return this._vISSRet;
            }
            set
            {
                this._vISSRet = value;
            }
        }

        [ParamsAttribute("INDISS")]
        [DataMember]
        public int indISS
        {
            get
            {
                return this._indISS;
            }
            set
            {
                this._indISS = value;
            }
        }

        [ParamsAttribute("CSERVICO")]
        [DataMember]
        public string cServico
        {
            get
            {
                return this._cServico;
            }
            set
            {
                this._cServico = value;
            }
        }

        [ParamsAttribute("CMUN")]
        [DataMember]
        public string cMun
        {
            get
            {
                return this._cMun;
            }
            set
            {
                this._cMun = value;
            }
        }

        [ParamsAttribute("CPAIS")]
        [DataMember]
        public string cPais
        {
            get
            {
                return this._cPais;
            }
            set
            {
                this._cPais = value;
            }
        }

        [ParamsAttribute("NPROCESSO")]
        [DataMember]
        public string nProcesso
        {
            get
            {
                return this._nProcesso;
            }
            set
            {
                this._nProcesso = value;
            }
        }

        [ParamsAttribute("INDINCENTIVO")]
        [DataMember]
        public int indIncentivo
        {
            get
            {
                return this._indIncentivo;
            }
            set
            {
                this._indIncentivo = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEISSQN WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEISSQN SET VBC = ?, VALIQ = ?, VISSQN = ?, CMUNFG = ?, CLISTSERV = ?, VDEDUCAO = ?, VOUTRO = ?, VDESCINCOND = ?, VDESCCOND = ?, 
                                VISSRET = ?, INDISS = ?, CSERVICO = ?, CMUN = ?, CPAIS = ?, NPROCESSO = ?,INDINCENTIVO = ?
                                WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.vBC, this.vAliq, this.vISSQN, this.cMunFG, this.cListServ, this.vDeducao, this.vOutro, this.vDescIncond, this.vDescCond,
                        this.vISSRet, this.indISS, this.cServico, this.cMun, this.cPais, this.nProcesso, this.indIncentivo,
                        this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEISSQN (IDOUTBOX, NITEM, VBC, VALIQ, VISSQN, CMUNFG, CLISTSERV, VDEDUCAO, VOUTRO, VDESCINCOND, VDESCCOND, VISSRET, INDISS, CSERVICO,
                                CMUN,CPAIS,NPROCESSO,INDINCENTIVO)
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.vBC, this.vAliq, this.vISSQN, this.cMunFG, this.cListServ, this.vDeducao, this.vOutro, 
                        this.vDescIncond, this.vDescCond, this.vISSRet, this.indISS, this.cServico, this.cMun, this.cPais, this.nProcesso, this.indIncentivo);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeISSQN.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeISSQN ReadNFeISSQN(params object[] parameters)
        {
            NFeISSQN _nfeISSQN = new NFeISSQN();

            try
            {
                string sSql = @"SELECT * FROM ZNFEISSQN WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfeISSQN.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeISSQN;
        }
    }
}