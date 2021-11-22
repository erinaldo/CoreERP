using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Globalization;

namespace ValidateLib
{
    public class ParamsBase
    {
        private AppLib.Data.Connection _conn;
        private DataTable _table;
        private DataRow _row;

        private string[] _defaultFields;
        private object[] _defaultValues;

        public string[] DefaultFields
        {
            get
            {
                return this._defaultFields;
            }
            set
            {
                this._defaultFields = value;
            }
        }

        public object[] DefaultValues
        {
            get
            {
                return this._defaultValues;
            }
            set
            {
                this._defaultValues = value;
            }
        }

        private void Read()
        {
            try
            {
                foreach (MemberInfo memberInfo in this.GetType().FindMembers(MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (MemberFilter)null, (object)""))
                {
                    object[] customAttributes = memberInfo.GetCustomAttributes(typeof(ParamsAttribute), false);
                    if (customAttributes.Length != 0)
                    {
                        ParamsAttribute paramsAttribute = (ParamsAttribute)customAttributes[0];
                        PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
                        object paramValue = this.GetParamValue(paramsAttribute.KeyColumnName, paramsAttribute.rowId, paramsAttribute.ValueColumnName);

                        try
                        {
                            this.DoLoadPropertyValue(propertyInfo, paramValue);
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException(string.Format("A propriedade {0} da classe {1} não pode ser carregada da base de dados. \r\nSolicite ao administrador checar os parâmetros do sistema.", (object)propertyInfo.Name, (object)this.GetType().Name), ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public void ReadFromDataRow(DataRow row)
        {
            try
            {
                this._row = row;
                this.Read();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public void ReadFromDataTable(DataTable table)
        {
            try
            {
                this._table = table;
                this.Read();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public void ReadFromCommand(string command)
        {
            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                System.Data.DataTable _dados = _conn.ExecQuery(command);
                this.ReadFromDataTable(_dados);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public void ReadFromCommand(string command, params object[] parameters)
        {
            try
            {
                _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                System.Data.DataTable _dados = _conn.ExecQuery(command, parameters);
                this.ReadFromDataTable(_dados);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        private object GetParamValue(string keyColumnName, string rowId, string valueColumnName)
        {
            DataRow row = (DataRow)null;
            this.GetRow(keyColumnName, rowId, ref row);
            if (row != null)
                return row[valueColumnName];
            else
                return (object)DBNull.Value;
        }

        private void GetRow(string keyColumnName, string rowId, ref DataRow row)
        {
            try
            {
                DataRow[] dataRowArray;

                if (_table != null)
                {
                    if (keyColumnName != "")
                    {
                        string str = "'";
                        int result;
                        if (int.TryParse(rowId, out result))
                            str = "";
                        dataRowArray = this._table.Select(keyColumnName + "=" + str + rowId + str);
                    }
                    else
                        dataRowArray = this._table.Select();
                }
                else
                {
                    if (this._row != null)
                        dataRowArray = new DataRow[] { this._row };
                    else
                        dataRowArray = new DataRow[] { };
                }

                switch (dataRowArray.Length)
                {
                    case 0:
                        if (this._defaultFields == null || this._defaultFields.Length <= 0)
                            break;

                        if (this._table != null)
                        {
                            row = this._table.NewRow();
                            if (keyColumnName != "")
                                row[keyColumnName] = (object)rowId;
                            this.OnNewRow(row);
                            this._table.Rows.Add(row);
                        }
                        else
                        {
                            row = null;
                        }
                        break;
                    case 1:
                        row = dataRowArray[0];
                        break;
                    default:
                        throw new ApplicationException("Erro: Apenas um único registro é esperado");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        private void OnNewRow(DataRow row)
        {
            try
            {
                int num = 0;
                foreach (string index in this._defaultFields)
                {
                    row[index] = this._defaultValues[num++];
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        private void DoLoadPropertyValue(PropertyInfo propertyInfo, object value)
        {
            try
            {
                System.Type conversionType = propertyInfo.PropertyType;

                if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericArguments().Length > 0)
                    conversionType = propertyInfo.PropertyType.GetGenericArguments()[0];

                if (value == DBNull.Value)
                    return;

                if (value.GetType() == typeof(string) && conversionType == typeof(bool))
                {
                    bool flag = this.SetValueForBool(propertyInfo.Name, value.ToString());
                    propertyInfo.SetValue((object)this, (object)(bool)(flag ? true : false), (object[])null);
                }
                else
                {
                    if (value.GetType() == typeof(string) && conversionType == typeof(DateTime))
                    {
                        DateTime dateTime = this.SetValueForDateTime(propertyInfo.Name, value.ToString());
                        propertyInfo.SetValue((object)this, (object)dateTime, (object[])null);
                    }
                    else
                    {
                        if (value.GetType() == typeof(string) && conversionType == typeof(Decimal))
                        {
                            Decimal num = this.SetValueForDecimal(propertyInfo.Name, value.ToString());
                            propertyInfo.SetValue((object)this, (object)num, (object[])null);
                        }
                        else
                        {
                            propertyInfo.SetValue((object)this, Convert.ChangeType(value, conversionType), (object[])null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        protected virtual bool SetValueForBool(string propertyName, string value)
        {
            if (!(value == "1") && !(value == "T"))
                return value == "S";
            else
                return true;
        }

        protected virtual DateTime SetValueForDateTime(string propertyName, string value)
        {
            string shortDatePattern = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            DateTime result;
            if (DateTime.TryParseExact(value, string.Format("{0} {1}", (object)shortDatePattern, (object)"HH:mm:ss"), (IFormatProvider)Thread.CurrentThread.CurrentCulture.DateTimeFormat, DateTimeStyles.None, out result) || DateTime.TryParseExact(value, shortDatePattern, (IFormatProvider)Thread.CurrentThread.CurrentCulture.DateTimeFormat, DateTimeStyles.None, out result))
                return result;
            else
                throw new ApplicationException(string.Format("A propriedade {0} da classe {0} não pode ser carregada.Formato de data 'dd/MM/yyyy' inválido para o conteúdo", (object)propertyName, (object)this.GetType().Name));
        }

        protected virtual Decimal SetValueForDecimal(string propertyName, string value)
        {
            return Decimal.Parse(value, (IFormatProvider)Thread.CurrentThread.CurrentCulture.NumberFormat);
        }
    }
}
