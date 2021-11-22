using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class Alias
    {
        public string Name { get; set; }
        public Data.DBS.DBType DbType { get; set; }
        public Data.DBS.DBProviderType DbProviderType { get; set; }
        public string ServerName { get; set; }
        public string DbName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool SyncService { get; set; }
        public string ConnectionString { get; set; }

        public Alias()
        { 
        
        }

        public bool GetAlias()
        {
            return false;            
        }
    }
}
