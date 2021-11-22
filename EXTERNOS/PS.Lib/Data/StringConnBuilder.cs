using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib.Data
{
    public class StringConnBuilder
    {
        public string Build(Alias alias)
        {
            string strConn = string.Empty;

            if (alias.DbProviderType.Equals(DBS.DBProviderType.SQLOleDb))
            {
                string Provider = string.Concat("Provider=", alias.DbProviderType);
                string DataSource = string.Concat(";Data Source=", alias.ServerName);
                string InitialCatalog = string.Concat(";Initial Catalog=", alias.DbName);
                string UserID = string.Concat(";User ID=", alias.UserName);
                string Password = string.Concat(";Password=", alias.Password);

                strConn = string.Concat(Provider, DataSource, InitialCatalog, UserID, Password);
            }

            if (alias.DbProviderType.Equals(DBS.DBProviderType.SqlClient))
            {
                string DataSource = string.Concat("Data Source=", alias.ServerName);
                string InitialCatalog = string.Concat(";Initial Catalog=", alias.DbName);
                string UserID = string.Concat(";User ID=", alias.UserName);
                string Password = string.Concat(";Password=", alias.Password);

                strConn = string.Concat(DataSource, InitialCatalog, UserID, Password);
            }

            if (alias.DbProviderType.Equals(DBS.DBProviderType.OracleClient))
            {

            }

            return strConn;
        }
    }
}
