using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFetransporta : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private string _CNPJ;
        private string _CPF;
        private string _xNome;
        private string _IE;
        private string _xEnder;
        private string _xMun;
        private string _UF;

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

        [ParamsAttribute("CNPJ")]
        [DataMember]
        public string CNPJ
        {
            get
            {
                return this._CNPJ;
            }
            set
            {
                this._CNPJ = value;
            }
        }

        [ParamsAttribute("CPF")]
        [DataMember]
        public string CPF
        {
            get
            {
                return this._CPF;
            }
            set
            {
                this._CPF = value;
            }
        }

        [ParamsAttribute("XNOME")]
        [DataMember]
        public string xNome
        {
            get
            {
                return this._xNome;
            }
            set
            {
                this._xNome = value;
            }
        }

        [ParamsAttribute("IE")]
        [DataMember]
        public string IE
        {
            get
            {
                return this._IE;
            }
            set
            {
                this._IE = value;
            }
        }

        [ParamsAttribute("XENDER")]
        [DataMember]
        public string xEnder
        {
            get
            {
                return this._xEnder;
            }
            set
            {
                this._xEnder = value;
            }
        }

        [ParamsAttribute("XMUN")]
        [DataMember]
        public string xMun
        {
            get
            {
                return this._xMun;
            }
            set
            {
                this._xMun = value;
            }
        }

        [ParamsAttribute("UF")]
        [DataMember]
        public string UF
        {
            get
            {
                return this._UF;
            }
            set
            {
                this._UF = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFETRANSPORTA WHERE IDOUTBOX = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox))
                {
                    sSql = @"UPDATE ZNFETRANSPORTA SET CNPJ = ?, CPF = ?, XNOME = ?, IE = ?, XENDER = ?, XMUN = ?, UF = ? WHERE IDOUTBOX = ?"; 
                    _conn.ExecQuery(sSql, this.CNPJ, this.CPF, this.xNome, this.IE, this.xEnder, this.xMun, this.UF, this.IdOutbox);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFETRANSPORTA (IDOUTBOX, CNPJ, CPF, XNOME, IE, XENDER, XMUN, UF) 
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.CNPJ, this.CPF, this.xNome, this.IE, this.xEnder, this.xMun, this.UF);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFetransporta.Save", err);
                throw new Exception(err);
            }
        }

        public static NFetransporta ReadNFetransporta(params object[] parameters)
        {
            NFetransporta _nfetransporta = new NFetransporta();

            try
            {
                string sSql = @"SELECT * FROM ZNFETRANSPORTA WHERE IDOUTBOX = ?";
                _nfetransporta.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfetransporta;
        }
    }
}
