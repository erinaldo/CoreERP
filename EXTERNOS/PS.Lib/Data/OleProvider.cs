using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib.Data
{
    public class OleProvider : IProvider
    {
        public String ConnectionString { get; set; }
        public String Command { get; set; }
        public Object[] Parameters { get; set; }

        private System.Data.OleDb.OleDbConnection conn;
        //private System.Data.OleDb.OleDbCommand cmd;
        //private System.Data.OleDb.OleDbDataAdapter da;
        //private System.Data.DataSet ds;
        //private System.Data.DataTable dt;
        private System.Data.OleDb.OleDbTransaction trs;

        public System.Exception exception;

        public static OleProvider instance = null;

        public static OleProvider GetInstance(bool Flag)
        {
            if (Flag)
                instance = null;

            if (instance == null)
                instance = new OleProvider();

            return instance;
        }

        public static OleProvider GetInstance()
        {
            if (instance == null)
                instance = new OleProvider();

            return instance;
        }

        public Boolean Open()
        {
            try
            {
                if ((trs == null) && (conn.State != System.Data.ConnectionState.Open))
                {
                    conn.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Boolean Close()
        {
            try
            {
                if ((trs == null) && (conn.State != System.Data.ConnectionState.Closed))
                {
                    conn.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void Begin()
        {
            if (conn == null)
                conn = new System.Data.OleDb.OleDbConnection(ConnectionString);

            this.Open();

            trs = conn.BeginTransaction();
        }

        public void Rollback()
        {
            trs.Rollback();
            trs = null;
            this.Close();
        }

        public void Commit()
        {
            trs.Commit();
            trs = null;
            this.Close();
        }

        private void SetParams(System.Data.OleDb.OleDbCommand cmd)
        {
            for (int i = 0; i < Parameters.Length; i++)
            {
                cmd.Parameters.AddWithValue("", Parameters[i]);

                /*
                if (parametros[i] == null)
                {
                    cmd.Parameters.AddWithValue("", null);
                    continue;
                }

                if (parametros[i].GetType() == typeof(string))
                {
                    string objeto = (string)parametros[i];
                    cmd.Parameters.Add("", System.Data.OleDb.OleDbType.VarChar).Value = objeto;
                    continue;
                }

                if (parametros[i].GetType() == typeof(int))
                {
                    int objeto = (int)parametros[i];
                    cmd.Parameters.Add("", System.Data.OleDb.OleDbType.Integer).Value = objeto;
                    continue;
                }

                if (parametros[i].GetType() == typeof(DateTime))
                {
                    DateTime objeto = (DateTime)parametros[i];
                    cmd.Parameters.Add("", System.Data.OleDb.OleDbType.Date).Value = objeto;
                    continue;
                }
                */
            }
        }

        public System.Data.DataTable QuerySelect()
        {
            System.Data.OleDb.OleDbCommand cmd;
            // System.Data.DataSet ds;
            System.Data.DataTable dt;

            try
            {
                if (conn == null)
                    conn = new System.Data.OleDb.OleDbConnection(ConnectionString);

                this.Open();

                dt = new System.Data.DataTable("Table1");

                cmd = new System.Data.OleDb.OleDbCommand(Command, conn, trs);
                SetParams(cmd);
                cmd.CommandTimeout = 30;

                // OLD
                // System.Data.OleDb.OleDbDataReader rd = cmd.ExecuteReader();
                // dt.Load(rd);

                // NEW
                using (System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                this.Close();
                return dt;
            }
               catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int QueryExec()
        {
            System.Data.OleDb.OleDbCommand cmd;

            try
            {
                if (conn == null)
                    conn = new System.Data.OleDb.OleDbConnection(ConnectionString);

                this.Open();

                cmd = new System.Data.OleDb.OleDbCommand(Command, conn, trs);
                SetParams(cmd);
                cmd.CommandTimeout = 30;
                cmd.CommandType = System.Data.CommandType.Text;
                int result = cmd.ExecuteNonQuery();
                this.Close();
                return result;
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public System.Data.DataTable GetSchemaTable()
        {
            System.Data.OleDb.OleDbCommand cmd;
            System.Data.OleDb.OleDbDataAdapter da;
            System.Data.DataSet ds;
            System.Data.DataTable dt;

            try
            {
                if (conn == null)
                    conn = new System.Data.OleDb.OleDbConnection(ConnectionString);

                this.Open();

                cmd = new System.Data.OleDb.OleDbCommand(Command, conn, trs);
                cmd.CommandTimeout = 30;
                SetParams(cmd);

                // OLD
                //da = new System.Data.OleDb.OleDbDataAdapter(cmd);
                //ds = new System.Data.DataSet();
                //dt = new System.Data.DataTable();
                //System.Data.OleDb.OleDbDataReader rd = cmd.ExecuteReader();
                //dt = rd.GetSchemaTable();
                //conn.Close();

                System.Data.OleDb.OleDbDataReader rd = cmd.ExecuteReader();
                dt = rd.GetSchemaTable();
                this.Close();

                return dt;
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
