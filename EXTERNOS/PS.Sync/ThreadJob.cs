using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Sync
{
    public class ThreadJob
    {
        public int CodEmpresa { get; set; }
        public int IdJob { get; set; }

        private Alias _alias;
        private PSInstance _instance;
        private PS.Lib.Data.DBS dbs;

        public ThreadJob(Alias alias, PSInstance job)
        {
            _alias = alias;
            _instance = job;
        }

        public void Execute()
        {
            dbs = new Lib.Data.DBS();

            bool Flag = true;

            //Verificar se a tarefa possui registro em execução
            string sSql = string.Empty;
            sSql = @"SELECT NSEQ FROM GJOBEXEC WHERE CODEMPRESA = ? AND IDJOB = ? AND STATUS = ?";
            if (!dbs.QueryFind(sSql, this.CodEmpresa, this.IdJob, 0))
            {
                if (_instance.AssemblyName == "" || _instance.NameSpace == "" || _instance.TypeName == "")
                    Flag = false;

                if (Flag)
                {
                    PSProcess job = (PSProcess)_instance.CreateInstance(_instance.AssemblyName, _instance.NameSpace, _instance.TypeName);
                    job.CodEmpresa = this.CodEmpresa;
                    job.IdJob = this.IdJob;
                    job.Execute();
                }
            }
        }
    }
}
