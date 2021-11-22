using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib
{
    public class CustomDataColumn
    {
        public string DataName { get; set; } // nome da coluna
        public string Text { get; set; } // texto da coluna
        public int Order { get; set; } // ordem da coluna na grid

        public string TableName { get; set; } // quando lookup informar a tabela
        public string FieldName { get; set; } // quando lookup informar o campo de retorno

        public DataGridColumnType ColumnType { get; set; } // tipo da coluna a ser adicionada
        public string FormatString { get; set; } // formatação da coluna
        public string ReplaceFor { get; set; } // coluna que sera substituida
        public DataGridViewContentAlignment ContentAlignment { get; set; } // alinhamento

        public ImageProperties[] Image { get; set; }
        public DataGridColumnCondition[] Condition { get; set; }

        [DefaultValue(true)]
        public bool Visible { get; set; }

        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public CustomDataColumn(string ColumnDataName, bool Visible)
        {
            this.DataName = ColumnDataName;
            this.Visible = Visible;
            this.ColumnType = DataGridColumnType.TextBox;
        }

        public CustomDataColumn(string ColumnDataName, string ColumnText, int ColumnOrder, string TableName, string FieldName, string Replace, DataGridViewContentAlignment Alignment, DataGridColumnCondition[] ColumnCondition)
        {
            this.DataName = ColumnDataName;
            this.Text = ColumnText;
            this.ColumnType = DataGridColumnType.Lookup;
            this.Order = ColumnOrder;
            this.TableName = TableName;
            this.FieldName = FieldName;
            this.ReplaceFor = Replace;
            this.ContentAlignment = Alignment;
            this.Condition = ColumnCondition;
            this.Visible = true;
        }

        public CustomDataColumn(string ColumnDataName, string ColumnText, int ColumnOrder, string TableName, string FieldName, DataGridViewContentAlignment Alignment, DataGridColumnCondition[] ColumnCondition)
        {
            this.DataName = ColumnDataName;
            this.Text = ColumnText;
            this.ColumnType = DataGridColumnType.Lookup;
            this.Order = ColumnOrder;
            this.TableName = TableName;
            this.FieldName = FieldName;
            this.ContentAlignment = Alignment;
            this.Condition = ColumnCondition;
            this.Visible = true;
        }

        public CustomDataColumn(string ColumnDataName, string ColumnText, DataGridColumnType Type, string Format, string Replace, DataGridViewContentAlignment Alignment)
        {
            this.DataName = ColumnDataName;
            this.Text = ColumnText;
            this.ColumnType = Type;
            this.FormatString = Format;
            this.ReplaceFor = Replace;
            this.ContentAlignment = Alignment;
            this.Visible = true;
        }

        public CustomDataColumn(string ColumnDataName, string ColumnText, DataGridColumnType Type, int ColumnOrder, string Format, string Replace, DataGridViewContentAlignment Alignment)
        {
            this.DataName = ColumnDataName;
            this.Text = ColumnText;
            this.ColumnType = Type;
            this.Order = ColumnOrder;
            this.FormatString = Format;
            this.ReplaceFor = Replace;
            this.ContentAlignment = Alignment;
            this.Visible = true;
        }

        public CustomDataColumn(string ColumnDataName, string ColumnText, DataGridColumnType Type, int ColumnOrder, string Replace, DataGridViewContentAlignment Alignment, ImageProperties[] ColumnImage)
        {
            this.DataName = ColumnDataName;
            this.Text = ColumnText;
            this.ColumnType = Type;
            this.Order = ColumnOrder;
            this.ReplaceFor = Replace;
            this.ContentAlignment = Alignment;
            this.Image = ColumnImage;
            this.Visible = true;
        }

        public CustomDataColumn(string ColumnDataName, string ColumnText, DataGridColumnType Type, string Format, DataGridViewContentAlignment Alignment)
        {
            this.DataName = ColumnDataName;
            this.Text = ColumnText;
            this.ColumnType = Type;
            this.FormatString = Format;
            this.ContentAlignment = Alignment;
            this.Visible = true;
        }

        public CustomDataColumn(string ColumnDataName, DataGridColumnType Type, string Format, DataGridViewContentAlignment Alignment)
        {
            this.DataName = ColumnDataName;
            this.ColumnType = Type;
            this.FormatString = Format;
            this.ContentAlignment = Alignment;
            this.Visible = true;
        }

        public string LoadLookup()
        {
            string sSql = string.Concat("SELECT ", this.FieldName ," FROM ", this.TableName, " WHERE ");
            object[] parArr = new object[this.Condition.Length];

            for (int i = 0; i < Condition.Length; i++)
            {
                if (i == 0)
                {
                    sSql = string.Concat(sSql, Condition[i].ColumnNameRefTable, " =  ? ");
                }
                else
                {
                    sSql = string.Concat(sSql, " AND ", Condition[i].ColumnNameRefTable, " = ? ");
                }

                parArr[i] = Condition[i].Value;
            }
            
            System.Data.DataTable dt = dbs.QuerySelect(sSql, parArr);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][this.FieldName].ToString();
            }
            else
            {
                return string.Empty;            
            }
        }
    }
}
