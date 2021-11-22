using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Lib
{
    public class PSProcessLog
    {
        private StringBuilder LogExecucao;

        public PSProcessLog()
        {
            LogExecucao = new StringBuilder();        
        }

        public void Append(string text)
        {
            string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
            LogExecucao.AppendLine(string.Concat(time, ": ", text));
        }

        public StringBuilder GetLogExecucao()
        {
            if (this.LogExecucao == null)
                this.LogExecucao = new StringBuilder();

            return this.LogExecucao;        
        }
    }
}
