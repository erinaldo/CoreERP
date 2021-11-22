using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Threading;
using PS.Lib;

namespace PS.Sync
{
    partial class SyncService : ServiceBase
    {
        private Arquivo arq = new Arquivo();
        private XMLParse xml = new XMLParse();
        private AliasList alias = new AliasList();
        private SyncServiceProcess syncServiceProcess = new SyncServiceProcess();
        public static System.Timers.Timer objTimer = new System.Timers.Timer();

        public SyncService()
        {
            InitializeComponent();
        }

        public static void Main(params string[] parameters)
        {
            if (parameters.Length > 0 && parameters[0].ToLower() == "-console")
                new SyncService().RunConsole(parameters);
            else
                ServiceBase.Run(new SyncService());
        }

        private void RunConsole(string[] args)
        {
            Console.WriteLine("Starting service in console mode...");
            Console.WriteLine("Service running in console mode... Press any key to stop");
            OnStart(args);
            Console.ReadKey();
            OnStop();
        }

        private void GetAlias()
        {
            string AppPath = string.Concat(Convert.ToString(AppDomain.CurrentDomain.BaseDirectory),"Alias.xml");

            alias.AliasGroup.Clear();
            alias = (AliasList)xml.Read(arq.Ler(AppPath), new AliasList());
        }

        private void EventoDoTimer(object source, ElapsedEventArgs e)
        {
            objTimer.Enabled = false;

            GetAlias();
            syncServiceProcess.StartSyncServiceProcess(alias);

            objTimer.Enabled = true;
        }
        

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            try
            {
                objTimer.Elapsed += new ElapsedEventHandler(EventoDoTimer);
                objTimer.Interval = 60000;
                objTimer.Start();
            }
            catch
            {
                //TO DO
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            try
            {
                objTimer.Stop();
            }
            catch
            {
                //TO DO               
            }
        }
    }
}
