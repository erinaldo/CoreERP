using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Threading;
using PS.Lib;

namespace PS.Sync
{
    public class SyncServiceProcess
    {
        public void StartSyncServiceProcess(AliasList alias)
        {
            for (int i = 0; i < alias.AliasGroup.Count; i++)
            {
                bool execute = false;
                //if (alias.AliasGroup[i].SyncService != null)
                //{
                    execute = alias.AliasGroup[i].SyncService;

                    //Verifica se o Alias permite execução de processos
                    if (execute)
                    {
                        //Cria uma Thread para o Alias
                        ThreadProcess tp = new ThreadProcess(alias.AliasGroup[i]);
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(tp.Execute));
                        thread.Start();
                    }
                //}
            }
        }
    }
}
