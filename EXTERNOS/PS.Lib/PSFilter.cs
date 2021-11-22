using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class PSFilter
    {
        public string Field { get; set; }
        public string Oper { get; set; }
        public object Value { get; set; }

        public PSFilter(string field, object value)
        {
            Field = field;
            Oper = "=";
            Value = value;
        }

        public PSFilter(string field, string oper, object value)
        {
            Field = field;
            Oper = oper;
            Value = value;        
        }
    }
}
