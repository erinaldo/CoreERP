using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFevol : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private string _qVol;
        private string _esp;
        private string _marca;
        private string _nVol;
        private decimal _pesoL;
        private decimal _pesoB;
        private List<NFelacres> _lacres;

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

        [ParamsAttribute("QVOL")]
        [DataMember]
        public string qVol
        {
            get
            {
                return this._qVol;
            }
            set
            {
                this._qVol = value;
            }
        }

        [ParamsAttribute("ESP")]
        [DataMember]
        public string esp
        {
            get
            {
                return this._esp;
            }
            set
            {
                this._esp = value;
            }
        }

        [ParamsAttribute("MARCA")]
        [DataMember]
        public string marca
        {
            get
            {
                return this._marca;
            }
            set
            {
                this._marca = value;
            }
        }

        [ParamsAttribute("NVOL")]
        [DataMember]
        public string nVol
        {
            get
            {
                return this._nVol;
            }
            set
            {
                this._nVol = value;
            }
        }

        [ParamsAttribute("PESOL")]
        [DataMember]
        public decimal pesoL
        {
            get
            {
                return this._pesoL;
            }
            set
            {
                this._pesoL = value;
            }
        }

        [ParamsAttribute("PESOB")]
        [DataMember]
        public decimal pesoB
        {
            get
            {
                return this._pesoB;
            }
            set
            {
                this._pesoB = value;
            }
        }

        public List<NFelacres> lacres
        {
            get
            {
                return this._lacres;
            }
            set
            {
                this._lacres = value;
            }
        }

        private List<NFelacres> Readlacres(params object[] parameters)
        {
            List<NFelacres> lnfelacres = new List<NFelacres>();
            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();

                string sSql = @"SELECT * FROM ZNFELACRES WHERE IDOUTBOX = ?";
                System.Data.DataTable _dados = _conn.ExecQuery(sSql, parameters);
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    lnfelacres.Add(NFelacres.ReadNFelacres(row["IDOUTBOX"], row["NITEM"]));
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }

            return lnfelacres;
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEVOL WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEVOL SET QVOL = ?, ESP = ?, MARCA = ?, NVOL = ?, PESOL = ?, PESOB = ? WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.qVol, this.esp, this.marca, this.nVol, this.pesoL, this.pesoB, this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEVOL (IDOUTBOX, NITEM, QVOL, ESP, MARCA, NVOL, PESOL, PESOB)
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.qVol, this.esp, this.marca, this.nVol, this.pesoL, this.pesoB);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFevol.Save", err);
                throw new Exception(err);
            }
        }

        public static NFevol ReadNFevol(params object[] parameters)
        {
            NFevol _nfevol = new NFevol();

            try
            {
                string sSql = @"SELECT * FROM ZNFEVOL WHERE IDOUTBOX = ? AND NITEM = ?";
                _nfevol.ReadFromCommand(sSql, parameters);
                _nfevol.lacres = _nfevol.Readlacres(parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfevol;
        }
    }
}
