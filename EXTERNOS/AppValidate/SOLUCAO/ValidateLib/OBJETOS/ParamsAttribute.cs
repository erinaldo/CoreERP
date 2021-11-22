using System;
using System.Runtime.Serialization;

namespace ValidateLib
{
    [Serializable]
    public sealed class ParamsAttribute : Attribute
    {
        [DataMember]
        private string _keyColumnName = "";

        [DataMember]
        private string _valueColumnName;

        [DataMember]
        private string _rowId;

        [DataMember]
        public string KeyColumnName
        {
          get
          {
            return this._keyColumnName;
          }
          set
          {
            this._keyColumnName = value;
          }
        }

        [DataMember]
        public string ValueColumnName
        {
          get
          {
            return this._valueColumnName;
          }
          set
          {
            this._valueColumnName = value;
          }
        }

        [DataMember]
        public string rowId
        {
            get
            {
                return this._rowId;
            }
            set
            {
                this._rowId = value;
            }
        }

        public ParamsAttribute(string valueColumnName)
        {
          this._valueColumnName = valueColumnName;
        }

        public ParamsAttribute(string keyColumnName, string rowId, string valueColumnName)
            : this(valueColumnName)
        {
            this._keyColumnName = keyColumnName;
            this._rowId = rowId;
        }
    }
}
