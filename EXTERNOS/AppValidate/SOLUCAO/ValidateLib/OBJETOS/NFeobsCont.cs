﻿using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class NFeobsCont : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idOutbox;
        private int _nItem;
        private string _xCampo;
        private string _xTexto;

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

        [ParamsAttribute("XCAMPO")]
        [DataMember]
        public string xCampo
        {
            get
            {
                return this._xCampo;
            }
            set
            {
                this._xCampo = value;
            }
        }

        [ParamsAttribute("XTEXTO")]
        [DataMember]
        public string xTexto
        {
            get
            {
                return this._xTexto;
            }
            set
            {
                this._xTexto = value;
            }
        }

        public void Save()
        {
            try
            {
                string sSql = string.Empty;

                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                sSql = @"SELECT IDOUTBOX FROM ZNFEOBSCONT WHERE IDOUTBOX = ? AND NITEM = ?";

                if (_conn.ExecHasRows(sSql, this.IdOutbox, this.nItem))
                {
                    sSql = @"UPDATE ZNFEOBSCONT SET XCAMPO = ?, XTEXTO = ? WHERE IDOUTBOX = ? AND NITEM = ?";
                    _conn.ExecQuery(sSql, this.xCampo, this.xTexto, this.IdOutbox, this.nItem);
                }
                else
                {
                    sSql = @"INSERT INTO ZNFEOBSCONT (IDOUTBOX, NITEM, XCAMPO, XTEXTO)
                                VALUES (?, ?, ?, ?)";
                    _conn.ExecQuery(sSql, this.IdOutbox, this.nItem, this.xCampo, this.xTexto);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeobsCont.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeobsCont ReadNFeobsCont(params object[] parameters)
        {
            NFeobsCont _nfeobsCont = new NFeobsCont();

            try
            {
                string sSql = @"SELECT * FROM ZNFEOBSCONT WHERE IDOUTBOX = ?";
                _nfeobsCont.ReadFromCommand(sSql, parameters);
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(Convert.ToInt32(parameters[0]), ex);
            }
            return _nfeobsCont;
        }
    }
}
