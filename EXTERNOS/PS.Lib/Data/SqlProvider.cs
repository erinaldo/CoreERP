using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib.Data
{
    public class SqlProvider : IProvider
    {
        public String ConnectionString { get; set; }
        public String Command { get; set; }
        public Object[] Parameters { get; set; }

        private System.Data.SqlClient.SqlConnection conn;
        private System.Data.SqlClient.SqlCommand cmd;
        private System.Data.SqlClient.SqlDataAdapter da;
        private System.Data.DataSet ds;
        private System.Data.DataTable dt;
        private System.Data.SqlClient.SqlTransaction trs;

        public System.Exception exception;

        public static SqlProvider instance = null;

        public static SqlProvider GetInstance(bool Flag)
        {
            if (Flag)
                instance = null;

            if (instance == null) instance = new SqlProvider();
            return instance;
        }

        public static SqlProvider GetInstance()
        {
            if (instance == null) instance = new SqlProvider();
            return instance;
        }

        public void Begin()
        {
            if (conn == null)
                conn = new System.Data.SqlClient.SqlConnection(ConnectionString);

            conn.Open();
            trs = conn.BeginTransaction();
        }

        public void Rollback()
        {
            trs.Rollback();
            conn.Close();
        }

        public void Commit()
        {
            trs.Commit();
            conn.Close();
        }

        private void SetParams()
        {
            for (int i = 0; i < Parameters.Length; i++)
            {
                string pname = string.Concat("@p", (i + 1).ToString());

                if (Parameters[i] == null)
                {
                    cmd.Parameters.AddWithValue(pname, null);
                    continue;
                }

                if (Parameters[i].GetType() == typeof(string))
                {
                    string objeto = (string)Parameters[i];
                    cmd.Parameters.Add(pname, System.Data.SqlDbType.VarChar).Value = objeto;
                    continue;
                }

                if (Parameters[i].GetType() == typeof(int))
                {
                    int objeto = (int)Parameters[i];
                    cmd.Parameters.Add(pname, System.Data.SqlDbType.Int).Value = objeto;
                    continue;
                }

                if (Parameters[i].GetType() == typeof(DateTime))
                {
                    DateTime objeto = (DateTime)Parameters[i];
                    cmd.Parameters.Add(pname, System.Data.SqlDbType.Date).Value = objeto;
                    continue;
                }
            }
        }

        public System.Data.DataTable QuerySelect()
        {
            if (conn == null)
                conn = new System.Data.SqlClient.SqlConnection(ConnectionString);

            cmd = new System.Data.SqlClient.SqlCommand(Command, conn, trs);
            SetParams();
            da = new System.Data.SqlClient.SqlDataAdapter(cmd);
            ds = new System.Data.DataSet();
            dt = new System.Data.DataTable();
            ds.Tables.Add(dt);
            da.Fill(dt);
            return dt;
        }

        public int QueryExec()
        {
            try
            {
                if (!(conn.State == System.Data.ConnectionState.Open))
                    conn.Open();

                cmd = new System.Data.SqlClient.SqlCommand(Command, conn, trs);
                SetParams();
                return cmd.ExecuteNonQuery();
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                //conn.Close();
            }
        }
    }
}
