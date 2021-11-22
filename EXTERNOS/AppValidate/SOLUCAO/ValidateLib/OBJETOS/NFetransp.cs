using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFetransp : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _modFrete;
        private NFetransporta _nfetransporta;
        private NFeretTransp _nferetTransp;
        private NFeveicTransp _nfeveicTransp;
        private List<NFereboque> _nfereboque;
        private string _vagao;
        private string _balsa;
        private List<NFevol> _nfevol;

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

        [ParamsAttribute("MODFRETE")]
        [DataMember]
        public int modFrete
        {
            get
            {
                return this._modFrete;
            }
            set
            {
                this._modFrete = value;
            }
        }

        [ParamsAttribute("VAGAO")]
        [DataMember]
        public string vagao
        {
            get
            {
                return this._vagao;
            }
            set
            {
                this._vagao = value;
            }
        }

        [ParamsAttribute("BALSA")]
        [DataMember]
        public string balsa
        {
            get
            {
                return this._balsa;
            }
            set
            {
                this._balsa = value;
            }
        }

        public NFetransporta nfetransporta
        {
            get
            {
                return this._nfetransporta;
            }
            set
            {
                this._nfetransporta = value;
            }
        }

        public NFeretTransp nferetTransp
        {
            get
            {
                return this._nferetTransp;
            }
            set
            {
                this._nferetTransp = value;
            }
        }

        public NFeveicTransp nfeveicTransp
        {
            get
            {
                return this._nfeveicTransp;
            }
            set
            {
                this._nfeveicTransp = value;
            }
        }

        public List<NFereboque> nfereboque
        {
            get
            {
                return this._nfereboque;
            }
            set
            {
                this._nfereboque = value;
            }
        }

        public List<NFevol> nfevol
        {
            get
            {
                return this._nfevol;
            }
            set
            {
                this._nfevol = value;
            }
        }

        private List<NFereboque> Readreboque(params object[] parameters)
        {
            List<NFereboque> lnfereboque = new List<NFereboque>();
            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();

                string sSql = @"SELECT * FROM ZNFEREBOQUE WHERE IDOUTBOX = ?";
                System.Data.DataTable _dados = _conn.ExecQuery(sSql, parameters);
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    lnfereboque.Add(NFereboque.ReadNFereboque(row["IDOUTBOX"], row["NITEM"]));
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }

            return lnfereboque;
        }

        private List<NFevol> Readvol(params object[] parameters)
        {
            List<NFevol> lnfereboque = new List<NFevol>();
            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();

                string sSql = @"SELECT * FROM ZNFEVOL WHERE IDOUTBOX = ?";
                System.Data.DataTable _dados = _conn.ExecQuery(sSql, parameters);
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    lnfereboque.Add(NFevol.ReadNFevol(row["IDOUTBOX"], row["NITEM"]));
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }

            return lnfereboque;
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFETRANSP WHERE IDOUTBOX = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox))
                {
                    sSql = @"UPDATE ZNFETRANSP SET MODFRETE = ?, VAGAO = ?, BALSA = ? WHERE IDOUTBOX = ?";
                    _conn.ExecQuery(sSql, this.modFrete, this.vagao, this.balsa, this.IdOutbox);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFETRANSP (IDOUTBOX, MODFRETE, VAGAO, BALSA) 
                                VALUES (?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.modFrete, this.vagao, this.balsa);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFetransp.Save", err);
                throw new Exception(err);
            }
        }

        public static NFetransp ReadNFetransp(params object[] parameters)
        {
            NFetransp _nfetransp = new NFetransp();

            try
            {
                string sSql = @"SELECT * FROM ZNFETRANSP WHERE IDOUTBOX = ?";
                _nfetransp.ReadFromCommand(sSql, parameters);
                _nfetransp.nfetransporta = NFetransporta.ReadNFetransporta(parameters);
                _nfetransp.nferetTransp = NFeretTransp.ReadNFeretTransp(parameters);
                _nfetransp.nfeveicTransp = NFeveicTransp.ReadNFeveicTransp(parameters);
                _nfetransp.nfereboque = _nfetransp.Readreboque(parameters);
                _nfetransp.nfevol = _nfetransp.Readvol(parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfetransp;
        }
    }
}
