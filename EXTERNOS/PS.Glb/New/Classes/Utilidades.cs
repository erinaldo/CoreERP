using PS.Glb.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Glb.New.Classes
{
    public class Utilidades
    {
        public Utilidades() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabela"></param>
        /// <returns></returns>
        public int getMaxOper(string tabela)
        {
            try
            {
                int id = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT IDLOG FROM GLOG WHERE CODTABELA = ?", new object[] { tabela })) + 1;
                string sql = String.Format(@"UPDATE GLOG SET IDLOG = {0}  WHERE CODTABELA = '{1}'", id, tabela);
                MetodosSQL.ExecQuery(sql);

                return id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
