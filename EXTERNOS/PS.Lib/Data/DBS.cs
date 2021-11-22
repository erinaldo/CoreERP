using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib.Data
{
    public class DBS
    {
        public DBProviderType ProviderType { get; set; }
        public DBType DbType { get; set; }
        public String ConnectionString { get; set; }

        private IProvider InfProvider;

        public DBS(bool flag)
        {
            if (flag)
            {
                ProviderType = Contexto.Alias.DbProviderType;
                DbType = Contexto.Alias.DbType;
                ConnectionString = Contexto.Alias.ConnectionString;
                InfProvider = null;

                //Nova Instancia
                InfProvider = GetProvider(flag);
            }
            else
            {
                ProviderType = Contexto.Alias.DbProviderType;
                DbType = Contexto.Alias.DbType;
                ConnectionString = Contexto.Alias.ConnectionString;
                InfProvider = GetProvider();            
            }  
        }

        public DBS()
        {
            ProviderType = Contexto.Alias.DbProviderType;
            DbType = Contexto.Alias.DbType;
            ConnectionString = Contexto.Alias.ConnectionString;
            InfProvider = GetProvider();
        }

        public IProvider GetProvider(bool Flag)
        {
            if (InfProvider == null)
            {
                if (ProviderType.Equals(DBProviderType.SQLOleDb))
                {
                    InfProvider = OleProvider.GetInstance(Flag);
                }

                if (ProviderType.Equals(DBProviderType.SqlClient))
                {
                    InfProvider = SqlProvider.GetInstance(Flag);
                }
            }
            return InfProvider;
        }

        public IProvider GetProvider()
        {
            if (InfProvider == null)
            {
                if (ProviderType.Equals(DBProviderType.SQLOleDb))
                {
                    InfProvider = OleProvider.GetInstance();
                }

                if (ProviderType.Equals(DBProviderType.SqlClient))
                {
                    InfProvider = SqlProvider.GetInstance();
                }
            }
            return InfProvider;
        }

        public enum DBProviderType
        {
            SQLOleDb,
            SqlClient,
            OracleClient
        }

        public enum DBType
        {
            SqlServer,
            Oracle
        }

        public void Begin()
        {
            InfProvider.ConnectionString = ConnectionString;
            InfProvider.Begin();
        }

        public void Rollback()
        {
            InfProvider.Rollback();
        }

        public void Commit()
        {
            InfProvider.Commit();
        }

        public object QueryValue(object nullvalue, string command, params object[] parameters)
        {
            object obj = null;

            System.Data.DataTable result = new System.Data.DataTable();
                
            result  = QuerySelect(command, parameters);

            if (result.Rows.Count > 0)
                if(result.Rows[0][0] == DBNull.Value)
                    return obj = nullvalue;
                else
                    return obj = result.Rows[0][0];
            else
                return obj = nullvalue;                
        }

        public DateTime GetServerDateTimeToday()
        {
            string sSql = @"SELECT CONVERT(DATETIME, CONVERT(VARCHAR, GETDATE(), 103), 103) SERVERDATE";
            DateTime ServerDateToday = Convert.ToDateTime(this.QueryValue(DateTime.Today, sSql));
            return ServerDateToday;
        }

        public DateTime GetServerDateTimeNow()
        {
            string sSql = @"SELECT GETDATE() SERVERDATE";
            DateTime ServerDateTimeNow = Convert.ToDateTime(this.QueryValue(DateTime.Now, sSql));
            return ServerDateTimeNow;
        }

        public bool QueryFind(String command, params object[] parameters)
        {
            System.Data.DataTable result = new System.Data.DataTable(); 

            result = QuerySelect(command, parameters);

            if (result.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public System.Data.DataTable QuerySelect(string command, params object[] parameters)
        {
            if (!command.Equals(string.Empty))
            {
                InfProvider.ConnectionString = ConnectionString;
                InfProvider.Command = command;
                InfProvider.Parameters = parameters;
                return InfProvider.QuerySelect();
            }

            return null;
        }

        public int QueryExec(string command, params object[] parameters)
        {
            InfProvider.Command = command;
            InfProvider.Parameters = parameters;
            return InfProvider.QueryExec();
        }
    }
}
