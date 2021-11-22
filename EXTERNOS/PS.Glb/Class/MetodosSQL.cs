using System;
using System.Data;
using System.Data.SqlClient;
using AppLib;

namespace PS.Glb.Class
{
    class MetodosSQL
    {
        public static string CS { get; set; } = AppLib.Context.poolConnection.Get("Start").ConnectionString;

        public static SqlConnection GetConnection()
        {
            SqlConnection conection = new SqlConnection();

            conection.ConnectionString = CS;

            try
            {
                conection.Open();
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
            }
            finally
            {
                conection.Close();
            }

            return conection;

        }

        public static void ExecQuery(string sql)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd;

            con.ConnectionString = CS;
            cmd = new SqlCommand(sql, con);

            int i = 0;

            try
            {
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
            }
            finally
            {
                con.Close();
            }            
        }

        public static Object ExecScalar(string sql)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd;

            con.ConnectionString = CS;
            cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                object i = cmd.ExecuteScalar();
                return i;
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public static string GetField(String sql, String field)
        {
            string retorno = string.Empty;

            SqlConnection con = new SqlConnection();
            SqlCommand cmd;

            con.ConnectionString = CS;

            try
            {
                cmd = new SqlCommand(sql, con);
                SqlDataReader dr;
                con.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    retorno = Convert.ToString(String.Format("{0}", dr[field]));
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
            }
            finally
            {
                con.Close();
            }

            return retorno;
        }

        public static DataTable GetDT(String sql)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd;

            con.ConnectionString = CS;

            try
            {
                cmd = new SqlCommand(sql, con);
                con.Open();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
            }
            finally
            {
                con.Close();
            }
        }

        public static Boolean VerificaPermissao(String _processo)
        {
            try
            {
                string sql = String.Format(@"select count(1) as 'CONT' from ZUSUARIO ZU

                                             inner join ZPROCESSOPERFIL ZPP
                                             on ZPP.CODPERFIL = ZU.CODPERFIL

                                             where ZPP.CODPROCESSO = '{0}'
                                             and ZU.USUARIO = '{1}'", _processo, AppLib.Context.Usuario);

                int i = int.Parse(MetodosSQL.GetField(sql, "CONT"));

                if(i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }
    }
}
