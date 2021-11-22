using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    public class Contexto
    {
        private static Session _session;

        public static Session Session
        {
            get
            {
                return Contexto._session;
            }
        }

        static Contexto()
        {
            Contexto._session = new Session();
        }
    }
}
