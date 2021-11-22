using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeinfAdic : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private string _infAdFisco;
        private string _infCpl;
        private List<NFeobsCont> _nfeobsCont;
        private List<NFeobsFisco> _nfeobsFisco;
        
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

        [ParamsAttribute("INFADFISCO")]
        [DataMember]
        public string infAdFisco
        {
            get
            {
                return this._infAdFisco;
            }
            set
            {
                this._infAdFisco = value;
            }
        }

        [ParamsAttribute("INFCPL")]
        [DataMember]
        public string infCpl
        {
            get
            {
                return this._infCpl;
            }
            set
            {
                this._infCpl = value;
            }
        }

        public List<NFeobsCont> nfeobsCont
        {
            get
            {
                return this._nfeobsCont;
            }
            set
            {
                this._nfeobsCont = value;
            }
        }

        public List<NFeobsFisco> nfeobsFisco
        {
            get
            {
                return this._nfeobsFisco;
            }
            set
            {
                this._nfeobsFisco = value;
            }
        }

        private List<NFeobsCont> ReadobsCont(params object[] parameters)
        {
            List<NFeobsCont> lnfeobsCont = new List<NFeobsCont>();
            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();

                string sSql = @"SELECT * FROM ZNFEOBSCONT WHERE IDOUTBOX = ?";
                System.Data.DataTable _dados = _conn.ExecQuery(sSql, parameters);
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    lnfeobsCont.Add(NFeobsCont.ReadNFeobsCont(row["IDOUTBOX"], row["NITEM"]));
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }

            return lnfeobsCont;
        }

        private List<NFeobsFisco> ReadobsFisco(params object[] parameters)
        {
            List<NFeobsFisco> lnfeobsFiscot = new List<NFeobsFisco>();
            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();

                string sSql = @"SELECT * FROM ZNFEOBSFISCO WHERE IDOUTBOX = ?";
                System.Data.DataTable _dados = _conn.ExecQuery(sSql, parameters);
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    lnfeobsFiscot.Add(NFeobsFisco.ReadNFeobsFisco(row["IDOUTBOX"], row["NITEM"]));
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }

            return lnfeobsFiscot;
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEINFADIC WHERE IDOUTBOX = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox))
                {
                    sSql = @"UPDATE ZNFEINFADIC SET INFADFISCO = ?, INFCPL = ? WHERE IDOUTBOX = ?";
                    _conn.ExecQuery(sSql, this.infAdFisco, this.infCpl, this.IdOutbox);
                }
                else
                {
                    _conn.ExecQuery("INSERT INTO ZNFEINFADIC (IDOUTBOX, INFADFISCO, INFCPL) VALUES (?, ?, ?)", new object[]{ this.IdOutbox, this.infAdFisco, this.infCpl});
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeinfAdic.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeinfAdic ReadNFeinfAdic(params object[] parameters)
        {
            NFeinfAdic _nfeinfAdic = new NFeinfAdic();

            try
            {
                string sSql = @"SELECT * FROM ZNFEINFADIC WHERE IDOUTBOX = ?";
                _nfeinfAdic.ReadFromCommand(sSql, parameters);
                _nfeinfAdic.nfeobsCont = _nfeinfAdic.ReadobsCont(parameters);
                _nfeinfAdic.nfeobsFisco = _nfeinfAdic.ReadobsFisco(parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeinfAdic;
        }
    }
}
