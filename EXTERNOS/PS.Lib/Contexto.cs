using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class Contexto
    {

        private static Session _session;
        private static Alias _alias;
        private static List<PSInstance> _psPartList;

        public static Session Session
        {
            get
            {
                return Contexto._session;
            }
        }

        public static Alias Alias
        {
            get
            {
                return Contexto._alias;
            }
        }

        public static List<PSInstance> PSPartList
        {
            get 
            {
                return Contexto._psPartList;            
            }                    
        }

        static Contexto()
        {
            Contexto._session = new Session();
            Contexto._alias = new Alias();
            Contexto._psPartList = new List<PSInstance>();
        }
    }
}
