using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Data;
using System.Timers;
using PS.Lib;

namespace PS.Sync
{
    public class ThreadProcess
    {
        private Alias _alias { get; set; }
        private PS.Lib.Data.DBS dbs;
        private System.Timers.Timer timer = new System.Timers.Timer();

        public ThreadProcess(Alias alias)
        {
            _alias = alias;

            //Autenticação por Alias
            PS.Lib.Login login = new PS.Lib.Login();
            login.Autenticar(_alias);

            dbs = new Lib.Data.DBS();
        }

        public void Execute()
        {
            //Intervalo de 1 min.
            /*
            timer.Interval = 60000;
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(EventoTimer);
            */

            EventoTimer();
        }

        //private void EventoTimer(object sender, ElapsedEventArgs e)
        private void EventoTimer()
        {
            //timer.Enabled = false;

            bool bExecute = false;

            int iEXECSEG = 0;
            int iEXECTER = 0;
            int iEXECQUA = 0;
            int iEXECQUI = 0;
            int iEXECSEX = 0;
            int iEXECSAB = 0;
            int iEXECDOM = 0;

            string sEXECHOR= string.Empty;

            DateTime dDataHoraAtual = DateTime.Now;
            DayOfWeek eDayOfWeek = dDataHoraAtual.DayOfWeek;

            string sMINUTO = dDataHoraAtual.Minute.ToString();
            string sHORA = dDataHoraAtual.Hour.ToString();

            if (sMINUTO.Length == 1)
                sMINUTO = string.Concat("0", sMINUTO);
            if (sHORA.Length == 1)
                sHORA = string.Concat("0", sHORA);

            string sHORMINATUAL = string.Concat(sHORA, sMINUTO);

            string sSql = @"SELECT IDJOB, NOME, CODEMPRESA, ASSEMBLYNAME, NAMESPACE, CLASSNAME, EXECSEG, EXECTER, EXECQUA, EXECQUI, EXECSEX, EXECSAB, EXECDOM, EXECHOR
                            FROM GJOB WHERE ATIVO = ?";

            DataTable dtGJOB = dbs.QuerySelect(sSql, 1);
            if (dtGJOB.Rows.Count > 0)
            {
                for (int i = 0; i < dtGJOB.Rows.Count; i++)
                {
                    iEXECSEG = (dtGJOB.Rows[i]["EXECSEG"].ToString() != "") ? int.Parse(dtGJOB.Rows[i]["EXECSEG"].ToString()) : 0;
                    iEXECTER = (dtGJOB.Rows[i]["EXECTER"].ToString() != "") ? int.Parse(dtGJOB.Rows[i]["EXECTER"].ToString()) : 0;
                    iEXECQUA = (dtGJOB.Rows[i]["EXECQUA"].ToString() != "") ? int.Parse(dtGJOB.Rows[i]["EXECQUA"].ToString()) : 0;
                    iEXECQUI = (dtGJOB.Rows[i]["EXECQUI"].ToString() != "") ? int.Parse(dtGJOB.Rows[i]["EXECQUI"].ToString()) : 0;
                    iEXECSEX = (dtGJOB.Rows[i]["EXECSEX"].ToString() != "") ? int.Parse(dtGJOB.Rows[i]["EXECSEX"].ToString()) : 0;
                    iEXECSAB = (dtGJOB.Rows[i]["EXECSAB"].ToString() != "") ? int.Parse(dtGJOB.Rows[i]["EXECSAB"].ToString()) : 0;
                    iEXECDOM = (dtGJOB.Rows[i]["EXECDOM"].ToString() != "") ? int.Parse(dtGJOB.Rows[i]["EXECDOM"].ToString()) : 0;

                    sEXECHOR = dtGJOB.Rows[i]["EXECHOR"].ToString();

                    if ((eDayOfWeek == DayOfWeek.Monday) && (iEXECSEG == 1))
                        bExecute = true;

                    if ((eDayOfWeek == DayOfWeek.Tuesday) && (iEXECTER == 1))
                        bExecute = true;

                    if ((eDayOfWeek == DayOfWeek.Wednesday) && (iEXECQUA == 1))
                        bExecute = true;

                    if ((eDayOfWeek == DayOfWeek.Thursday) && (iEXECQUI == 1))
                        bExecute = true;

                    if ((eDayOfWeek == DayOfWeek.Friday) && (iEXECSEX == 1))
                        bExecute = true;

                    if ((eDayOfWeek == DayOfWeek.Saturday) && (iEXECSAB == 1))
                        bExecute = true;

                    if ((eDayOfWeek == DayOfWeek.Sunday) && (iEXECDOM == 1))
                        bExecute = true;

                    if (bExecute)
                    {
                        /* Comentar para debugar
                        if (sEXECHOR == sHORMINATUAL)
                            bExecute = true;
                        else
                            bExecute = false;
                        */

                        // Descomentar para debugar
                        bExecute = true;

                        if (bExecute)
                        {
                            PSInstance job = new PSInstance();
                            job.AssemblyName = dtGJOB.Rows[i]["ASSEMBLYNAME"].ToString();
                            job.NameSpace = dtGJOB.Rows[i]["NAMESPACE"].ToString();
                            job.TypeName = dtGJOB.Rows[i]["CLASSNAME"].ToString();

                            //Cria uma Thread para o Alias executar cada serviço ativo
                            ThreadJob tJob = new ThreadJob(_alias, job);

                            tJob.CodEmpresa = (dtGJOB.Rows[i]["CODEMPRESA"].ToString() != "") ? int.Parse(dtGJOB.Rows[i]["CODEMPRESA"].ToString()) : 0;
                            tJob.IdJob = (dtGJOB.Rows[i]["IDJOB"].ToString() != "") ? int.Parse(dtGJOB.Rows[i]["IDJOB"].ToString()) : 0;

                            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(tJob.Execute));
                            thread.Start();
                        }
                    }
                }
            }

            //timer.Enabled = true;
        }
    }
}
