using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class DataGridColumnCondition
    {
        public string ColumnNameRefTable { get; set; }
        public string ColumnNameRefGrid { get; set; }
        public object Value { get; set; }

        public DataGridColumnCondition()
        { 
        
        }

        public DataGridColumnCondition(string ColumnRefTable, string ColumnRefGrid )
        {
            this.ColumnNameRefTable = ColumnRefTable;
            this.ColumnNameRefGrid = ColumnRefGrid;
        }
    }
}
