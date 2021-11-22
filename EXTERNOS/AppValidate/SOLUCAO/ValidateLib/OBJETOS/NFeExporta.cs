using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib.OBJETOS
{
    public class NFeExporta : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        public int IDOUTBOX { get; set; }
        public string UFSAIDAPAIS { get; set; }
        public string LOCEXPORTA { get; set; }
        public string LOCDESPACHO { get; set; }

        public void save()
        {
            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit ZNFEEXPORTA = new AppLib.ORM.Jit(_conn, "ZNFEEXPORTA");
                ZNFEEXPORTA.Set("IDOUTBOX", this.IDOUTBOX);
                ZNFEEXPORTA.Set("UFSAIDAPAIS", this.UFSAIDAPAIS);
                ZNFEEXPORTA.Set("LOCEXPORTA", this.LOCEXPORTA);
                ZNFEEXPORTA.Set("LOCDESPACHO", this.LOCDESPACHO);
                ZNFEEXPORTA.Save();
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NFeExporta.Save", err);
                throw new Exception(err);
            }
        }

        public static NFeExporta ReadByIDOutbox(int idOutbox)
        {
            NFeExporta _nfeExporta = new NFeExporta();

            try
            {
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(@"SELECT * FROM ZNFEEXPORTA WHERE IDOUTBOX = ?", new object[] { idOutbox });
                if (dt.Rows.Count > 0)
                {
                    _nfeExporta.IDOUTBOX = idOutbox;
                    _nfeExporta.UFSAIDAPAIS = dt.Rows[0]["UFSAIDAPAIS"].ToString();
                    _nfeExporta.LOCDESPACHO = dt.Rows[0]["LOCDESPACHO"].ToString();
                    _nfeExporta.LOCEXPORTA = dt.Rows[0]["LOCEXPORTA"].ToString();
                }

                return _nfeExporta;
            }
            catch (Exception ex)
            {
                ValidateLib.Log.Salvar(idOutbox, ex);
            }
            return _nfeExporta;
        }

    }
}
