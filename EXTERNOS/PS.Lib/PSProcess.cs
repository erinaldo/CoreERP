using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class PSProcess
    {
        public int CodEmpresa { get; set; }
        public int IdJob { get; set; }
        
        private int IdExecucao { get; set; }

        public PS.Lib.Data.DBS dbs;
        public PS.Lib.Global gb;

        public PSProcessLog LogExecucao;

        public PSProcess()
        {
            Initialize();        
        }

        private void Initialize()
        { 
            dbs = new PS.Lib.Data.DBS();
            gb = new PS.Lib.Global();

            LogExecucao = new PSProcessLog();
        }

        public virtual void Execute()
        {

        }

        public void RegistraInicioExecucao()
        {
            try
            {
                string sSql = string.Empty;
                sSql = @"INSERT INTO GJOBEXEC (CODEMPRESA, IDJOB, NSEQ, CODUSUARIOEXECUCAO, DATAHORAEXECINICIAL, DATAHORAEXECFINAL, STATUS)
                        VALUES(?,?,?,?,?,?,?)";

                PS.Lib.DataField CODEMPRESA = new PS.Lib.DataField("CODEMPRESA", this.CodEmpresa);
                PS.Lib.DataField IDJOB = new PS.Lib.DataField("IDJOB", this.IdJob);
                PS.Lib.DataField NSEQ = new PS.Lib.DataField("NSEQ", 0);

                List<PS.Lib.DataField> objArr = new List<DataField>();
                objArr.Add(CODEMPRESA);
                objArr.Add(IDJOB);
                objArr.Add(NSEQ);

                IdExecucao = gb.GetIdNovoRegistro(this.CodEmpresa, "GJOBEXEC", objArr, new string[] { "CODEMPRESA", "IDJOB", "NSEQ" });

                dbs.QueryExec(sSql, this.CodEmpresa, this.IdJob, IdExecucao, null, DateTime.Now, null, 0);
            }
            catch
            { 
            }
        }

        public void RegistraFimExecucao()
        {
            try
            {
                string sLog = this.LogExecucao.GetLogExecucao().ToString();

                string sSql = string.Empty;
                sSql = @"UPDATE GJOBEXEC SET DATAHORAEXECFINAL = ?, STATUS = ?, LOGEXECUCAO = ?
                        WHERE CODEMPRESA = ? AND IDJOB = ? AND NSEQ = ?";

                dbs.QueryExec(sSql, DateTime.Now, 1, sLog, this.CodEmpresa, this.IdJob, IdExecucao);
            }
            catch
            { 
            }
        }
    }
}
