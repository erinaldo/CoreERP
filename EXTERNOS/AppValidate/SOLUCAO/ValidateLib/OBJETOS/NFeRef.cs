using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib.OBJETOS
{
    public class NFeRef
    {
        public int IDOUTBOX { get; set; }
        public string CHAVENFEREF { get; set; }

        public NFeRef()
        {
        }

        public bool salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();
            AppLib.ORM.Jit ZNFEREF = new AppLib.ORM.Jit(conn, "ZNFEREF");

            try
            {
                ZNFEREF.Set("IDOUTBOX", IDOUTBOX);
                ZNFEREF.Set("CHAVENFEREF", CHAVENFEREF);
                ZNFEREF.Save();
                conn.Commit();
                return true;
            }
            catch (Exception)
            {
                conn.Rollback();
                return false;
            }
        }

    }
}
