using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib.Data
{
    public interface IProvider
    {
        String ConnectionString { get; set; }
        String Command { get; set; }
        Object[] Parameters { get; set; }

        void Begin();

        void Rollback();

        void Commit();

        System.Data.DataTable QuerySelect();

        int QueryExec();
    }
}
