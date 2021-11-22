using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PS.Lib
{
    public class DataField
    {
        public string Field { get; set; }
        public object Valor { get; set; }
        public object Tipo { get; set; }

        [DefaultValue(Global.TypeAutoinc.None)]
        public Global.TypeAutoinc Autoinc { get; set; }

        public List<ComboBoxItem> Item { get; set; }

        [DefaultValue(0)]
        public int MaxLength { get; set; }
        [DefaultValue(false)]
        public bool Key { get; set; }
        [DefaultValue(false)]
        public bool Required { get; set; }
        public string PSPart { get; set; }

        public DataField()
        { 
        
        }

        public DataField(string field, object value)
        {
            Field = field;
            Valor = value;
        }

        /// <summary>
        /// Usado quando o campo é PSTextoBox PSMaskedTextBox, PSLookup, PSDateBox, PSCheckBox PSComboBox, PSImageBox e PSMemoBox
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public DataField(string field, object value, object type)
        {
            Field = field;
            Valor = value;
            Tipo = type;
        }

        /// <summary>
        /// Usado quando o campo é PSTextoBox ou PSMaskedTextBox, PSDateBox
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        public DataField(string field, object value, object type, bool key)
        {
            Field = field;
            Valor = value;
            Tipo = type;
            Key = key;
        }

        /// <summary>
        /// Usado quando o campo é PSTextoBox ou PSMaskedTextBox
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="required"></param>
        public DataField(string field, object value, object type, bool key, bool required)
        {
            Field = field;
            Valor = value;
            Tipo = type;
            Key = key;
            Required = required;
        }

        /// <summary>
        /// Usado quando o campo é PSTextoBox ou PSMaskedTextBox
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="required"></param>
        /// <param name="length"></param>
        public DataField(string field, object value, object type, bool key, bool required, int length)
        {
            Field = field;
            Valor = value;
            Tipo = type;
            Key = key;
            Required = required;
            MaxLength = length;
        }

        /// <summary>
        /// Usado quando o campo é PSLookup
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="required"></param>
        /// <param name="length"></param>
        /// <param name="pspart"></param>
        public DataField(string field, object value, object type, bool key, bool required, int length, string pspart)
        {
            Field = field;
            Valor = value;
            Tipo = type;
            Key = key;
            Required = required;
            MaxLength = length;
            PSPart = pspart;
        }

        /// <summary>
        /// Usado quando o campo é PSComboBox
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="item"></param>
        public DataField(string field, object value, object type, List<ComboBoxItem> item)
        {
            Field = field;
            Valor = value;
            Tipo = type;
            Item = item;
        }

        public DataField(string field, object value, object type, Global.TypeAutoinc autoinc)
        {
            Field = field;
            Valor = value;
            Tipo = type;
            Autoinc = autoinc;
        }

        public void ComboBoxItemAdd(ComboBoxItem item)
        {
            if (this.Item == null)
                this.Item = new List<ComboBoxItem>();

            this.Item.Add(item);        
        }

        public void ComboBoxItemAdd(object value, string display)
        {
            if (this.Item == null)
                this.Item = new List<ComboBoxItem>();

            ComboBoxItem it = new ComboBoxItem(value, display);
            this.Item.Add(it);
        }
    }
}
