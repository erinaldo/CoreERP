using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class LookupEventArgs: EventArgs
    {
        public List<PSFilter> Filtro = new List<PSFilter>();
    }
}
