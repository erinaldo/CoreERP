using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PS.Lib.WinForms
{
    public class PSPartData
    {
        private Global gb = new Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public string _tablename { get; set; }
        public string[] _keys { get; set; }

        public String Consulta { get; set; }
        public Object[] Parametros { get; set; }

        public PSPartData()
        { 
        
        }

        private object ParameterValue(List<DataField> objArr, string campo)
        {
            for (int i = 0; i < objArr.Count; i++)
            {
                if (objArr[i].Field == campo)
                {
                    return objArr[i].Valor;
                }
            }

            return null;        
        }

        private int AddParameter(object[] Param)
        {
            for (int i = 0; i < Param.Length; i++)
            {
                if (Param[i] == null)
                {
                    return i;                
                }
            }

            return 0;
        }

        private bool IsKey(string campo)
        {
            for (int i = 0; i < this._keys.Length; i++)
            {
                if (this._keys[i] == campo)
                    return true;
            }

            return false;
        }

        private List<DataField> SetContextKey(List<DataField> objArr)
        {
            bool Flag = false;

            for (int i = 0; i < _keys.Length; i++)
            {
                if (_keys[i] == "CODEMPRESA")
                {
                    Flag = true;
                }
            }

            if (Flag)
            {
                Flag = false;

                for (int i = 0; i < objArr.Count; i++)
                {
                    if (objArr[i].Field == "CODEMPRESA")
                    {
                        Flag = true;

                        if (objArr[i].Valor != null)
                        {
                            return objArr;
                        }
                        else
                        {
                            objArr[i].Field = "CODEMPRESA";
                            objArr[i].Valor = Contexto.Session.Empresa.CodEmpresa;
                            objArr[i].Tipo = Contexto.Session.Empresa.CodEmpresa.GetType();

                            return objArr;
                        }
                    }
                }

                if (!Flag)
                {
                    DataField objDt = new DataField();
                    objDt.Field = "CODEMPRESA";
                    objDt.Valor = Contexto.Session.Empresa.CodEmpresa;
                    objDt.Tipo = Contexto.Session.Empresa.CodEmpresa.GetType();

                    objArr.Add(objDt);

                    return objArr;                                    
                }
            }

            return objArr;
        }

        public List<Filter> GetFilter(string tabela, string[] chave, string codUsuario, int codColigada, List<DataField> campos)
        {
            DataTable dt = new DataTable();
            List<Filter> filtros = new List<Filter>();

            dt = GetGlobalFilter(tabela, codColigada);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Filter ft = new Filter();
                ft.codEmpresa = int.Parse(dt.Rows[i]["CODEMPRESA"].ToString());
                ft.id = int.Parse(dt.Rows[i]["ID"].ToString());
                ft.descricao = dt.Rows[i]["DESCRICAO"].ToString().Trim();
                ft.codUsuario = null;
                ft.tabela = dt.Rows[i]["TABELA"].ToString().Trim();
                ft.selecionado = 0;
                ft.padrao = 0;
                ft.BuscaCondicao(campos);

                if (!ft.condicao.Equals(""))
                    filtros.Add(ft);
            }

            dt = GetUserFilter(tabela, codUsuario, codColigada);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Filter ft = new Filter();
                ft.codEmpresa = int.Parse(dt.Rows[i]["CODEMPRESA"].ToString());
                ft.id = int.Parse(dt.Rows[i]["ID"].ToString());
                ft.descricao = dt.Rows[i]["DESCRICAO"].ToString().Trim();
                ft.codUsuario = dt.Rows[i]["CODUSUARIO"].ToString().Trim();
                ft.tabela = dt.Rows[i]["TABELA"].ToString().Trim();
                ft.selecionado = 0;
                ft.BuscaCondicao(campos);

                if (!ft.condicao.Equals(""))
                    filtros.Add(ft);
            }

            //filtros.Add(BuscaFiltroPadraoVisao(tabela, chave, codColigada));

            return filtros;
        }

        public List<Filter> GetFilter(string tabela, string[] chave, string codUsuario, int codColigada)
        {
            DataTable dt = new DataTable();
            List<Filter> filtros = new List<Filter>();

            dt = GetGlobalFilter(tabela, codColigada);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Filter ft = new Filter();
                ft.codEmpresa = int.Parse(dt.Rows[i]["CODEMPRESA"].ToString());
                ft.id = int.Parse(dt.Rows[i]["ID"].ToString());
                ft.descricao = dt.Rows[i]["DESCRICAO"].ToString().Trim();
                ft.codUsuario = null;
                ft.tabela = dt.Rows[i]["TABELA"].ToString().Trim();
                ft.selecionado = 0;
                ft.padrao = 0;
                ft.BuscaCondicao();

                if (!ft.condicao.Equals(""))
                    filtros.Add(ft);
            }

            dt = GetUserFilter(tabela, codUsuario, codColigada);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Filter ft = new Filter();
                ft.codEmpresa = int.Parse(dt.Rows[i]["CODEMPRESA"].ToString());
                ft.id = int.Parse(dt.Rows[i]["ID"].ToString());
                ft.descricao = dt.Rows[i]["DESCRICAO"].ToString().Trim();
                ft.codUsuario = dt.Rows[i]["CODUSUARIO"].ToString().Trim();
                ft.tabela = dt.Rows[i]["TABELA"].ToString().Trim();
                ft.selecionado = 0;
                ft.BuscaCondicao();

                if (!ft.condicao.Equals(""))
                    filtros.Add(ft);
            }

            //filtros.Add(BuscaFiltroPadraoVisao(tabela, chave, codColigada));

            return filtros;
        }

        private DataTable GetGlobalFilter(string tabela, int codColigada)
        {
            string sSql = "SELECT * FROM GFILTRO WHERE TABELA = ? AND CODUSUARIO IS NULL AND CODEMPRESA = ?";

            DataTable dt = new DataTable();

            dt = dbs.QuerySelect(sSql, tabela, codColigada);

            return dt;
        }

        private DataTable GetUserFilter(string tabela, string codUsuario, int codColigada)
        {
            string sSql = "SELECT * FROM GFILTRO WHERE TABELA = ? AND CODUSUARIO = ? AND CODEMPRESA = ?";

            DataTable dt = new DataTable();

            dt = dbs.QuerySelect(sSql, tabela, codUsuario, codColigada);

            return dt;
        }

        private string SetDefaultFiltro(List<PSFilter> _filtroPadrao)
        {
            string filtro = "";

            if (_filtroPadrao != null)
            {
                for (int i = 0; i < _filtroPadrao.Count; i++)
                {
                    if(i == 0)
                        filtro = string.Concat(_filtroPadrao[i].Field, " = ?");
                    else
                        filtro = string.Concat(filtro, " AND ", _filtroPadrao[i].Field, " = ?");
                }
            }

            return filtro;
        }

        public String ReplaceCondicaoValueData(String CondicaoValue)
        {
            Boolean condicaoE = false;
            Boolean condicaoOU = false;

            if (CondicaoValue.Contains(" AND "))
            {
                condicaoE = true;
                CondicaoValue = CondicaoValue.Replace(" AND ", "");
            }

            if (CondicaoValue.Contains(" OR "))
            {
                condicaoOU = true;
                CondicaoValue = CondicaoValue.Replace(" OR ", "");
            }

            int indice = CondicaoValue.IndexOf(" ");
            String campo = CondicaoValue.Substring(0, indice);
            String result = CondicaoValue.Replace(campo, "CONVERT(DATETIME, CONVERT(DATE, "+ campo +"))");

            if (condicaoE)
            {
                result = " AND " + result;
            }

            if (condicaoOU)
            {
                result = " OR " + result;
            }

            return result;
        }

        public DataTable ExecuteQuery(String consulta, Object[] parametros)
        {
            try
            {
                return dbs.QuerySelect(consulta, parametros);
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError("Erro ao executar a query: " + ex.Message);
                return null;
            }
        }

        public DataTable ExecuteFilter(List<Filter> filtrosOriginal, string tabela, List<String> campos, String[] chaves, List<PSFilter> psFilter)
        {
            List<Filter> filtrosTemp = new List<Filter>();

            for (int i = 0; i < filtrosOriginal.Count; i++)
            {
                if (filtrosOriginal[i] != null)
                {
                    filtrosTemp.Add(filtrosOriginal[i].Copiar());
                }
            }
            
            this._tablename = tabela;
            this._keys = chaves;

            string sSql = string.Empty;            
            DataTable dt = new DataTable();

            for (int i = 0; i < filtrosTemp.Count; i++)
            {
                if (filtrosTemp[i].selecionado == 1)
                {
                    sSql = string.Concat(this.ReadView(), " ", filtrosTemp[i].condicao, (psFilter.Count > 0)? " AND " : "", SetDefaultFiltro(psFilter));
                }
            }

            if (sSql.Equals(string.Empty))
                return dt;
            else
            {
                int qtdcond = 0;

                // quantidade de condições do filtro selecionado
                for (int i = 0; i < filtrosTemp.Count; i++)
                {
                    if (filtrosTemp[i].selecionado == 1)
                    {
                        for (int j = 0; j < filtrosTemp[i].listaCondicao.Count; j++)
                        {
                            if (filtrosTemp[i].listaCondicao[j].operador != "IS NULL" && filtrosTemp[i].listaCondicao[j].operador != "IS NOT NULL" /*&&
                                filtros[i].listaCondicao[j].operador != "LIKE" && filtros[i].listaCondicao[j].operador != "NOT LIKE"*/)
                            {
                                qtdcond = filtrosTemp[i].listaCondicao.Count;
                            }
                        }
                    }
                }

                // quantidade de condições do filtro default
                if (psFilter != null)
                {
                    qtdcond = qtdcond + psFilter.Count;
                }

                object[] paramFiltro = new object[qtdcond];

                int cont = 0;

                for (int i = 0; i < filtrosTemp.Count; i++)
                {
                    if (filtrosTemp[i].selecionado == 1)
                    {
                        for (int j = 0; j < filtrosTemp[i].listaCondicao.Count; j++)
                        {
                            if (filtrosTemp[i].listaCondicao[j].operador != "IS NULL" && filtrosTemp[i].listaCondicao[j].operador != "IS NOT NULL" /*&&
                                filtros[i].listaCondicao[j].operador != "LIKE" && filtros[i].listaCondicao[j].operador != "NOT LIKE"*/)
                            {
                                String valorTemp = filtrosTemp[i].listaCondicao[j].valor.ToString();

                                if (valorTemp.ToUpper().Equals("$HOJE"))
                                {
                                    filtrosTemp[i].listaCondicao[j].valor = String.Format("{0:yyyy-MM-dd}", this.dbs.GetServerDateTimeToday());
                                }

                                if (valorTemp.Length == 10)
                                {
                                    if (valorTemp.Substring(2, 1).Equals("/"))
                                    {
                                        if (valorTemp.Substring(5, 1).Equals("/"))
                                        {
                                            sSql = sSql.Replace(filtrosTemp[i].listaCondicao[j].condicaoValue, ReplaceCondicaoValueData(filtrosTemp[i].listaCondicao[j].condicaoValue));
                                            filtrosTemp[i].listaCondicao[j].valor = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(filtrosTemp[i].listaCondicao[j].valor));
                                        }
                                    }
                                }

                                //if (valorTemp.ToUpper().Equals("$SEMANA"))
                                //{

                                //}

                                //if (valorTemp.ToUpper().Equals("$MES"))
                                //{

                                //}

                                //if (valorTemp.ToUpper().Equals("$ANO"))
                                //{

                                //}

                                if (valorTemp.ToUpper().Equals("$USUARIO"))
                                {
                                    filtrosTemp[i].listaCondicao[j].valor = PS.Lib.Contexto.Session.CodUsuario;
                                }

                                if (valorTemp.Length >= 2)
                                {

                                    if ((filtrosTemp[i].listaCondicao[j].operador.Equals("LIKE")) || (filtrosTemp[i].listaCondicao[j].operador.Equals("NOT LIKE")))
                                    {
                                        if (valorTemp[0].Equals('['))
                                        {
                                            if (valorTemp[valorTemp.Length - 1].Equals(']'))
                                            {
                                                FormParametro f = new FormParametro();
                                                f.Set(filtrosTemp[i].listaCondicao[j].campoText, valorTemp.Replace("[", "").Replace("]", ""), String.Empty);
                                                f.ShowDialog();

                                                if (f.Result.Length == 10)
                                                {
                                                    if (f.Result.Substring(2, 1).Equals("/"))
                                                    {
                                                        if (f.Result.Substring(5, 1).Equals("/"))
                                                        {
                                                            f.Result = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(f.Result));
                                                            sSql = sSql.Replace(filtrosTemp[i].listaCondicao[j].condicaoValue, ReplaceCondicaoValueData(filtrosTemp[i].listaCondicao[j].condicaoValue));
                                                        }
                                                    }
                                                }

                                                sSql = sSql.Replace("'" + filtrosTemp[i].listaCondicao[j].valor.ToString() + "'", "?");
                                                filtrosTemp[i].listaCondicao[j].valor = f.Result;
                                            }
                                        }
                                        else
                                        {
                                            sSql = sSql.Replace("'" + filtrosTemp[i].listaCondicao[j].valor.ToString() + "'", "?");
                                        }
                                    }
                                    else
                                    {
                                        if (valorTemp[0].Equals('['))
                                        {
                                            if (valorTemp[valorTemp.Length - 1].Equals(']'))
                                            {
                                                FormParametro f = new FormParametro();
                                                f.Set(filtrosTemp[i].listaCondicao[j].campoText, valorTemp.Replace("[", "").Replace("]", ""), String.Empty);
                                                f.ShowDialog();

                                                if (f.Result.Length == 10)
                                                {
                                                    if (f.Result.Substring(2, 1).Equals("/"))
                                                    {
                                                        if (f.Result.Substring(5, 1).Equals("/"))
                                                        {
                                                            f.Result = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(f.Result));
                                                            sSql = sSql.Replace(filtrosTemp[i].listaCondicao[j].condicaoValue, ReplaceCondicaoValueData(filtrosTemp[i].listaCondicao[j].condicaoValue));
                                                        }
                                                    }
                                                }
                                                
                                                sSql = sSql.Replace("'" + filtrosTemp[i].listaCondicao[j].valor.ToString() + "'", "?");
                                                filtrosTemp[i].listaCondicao[j].valor = f.Result;
                                            }
                                        }
                                    }
                                }

                                paramFiltro[cont] = filtrosTemp[i].listaCondicao[j].valor;
                                cont++;
                            }
                        }
                    }
                }

                if (psFilter != null)
                {
                    for (int j = 0; j < psFilter.Count; j++)
                    {
                        paramFiltro[cont] = psFilter[j].Value;
                        cont++;
                    }
                }

                try
                {
                    Consulta = sSql;
                    Parametros = paramFiltro;
                    return dt = dbs.QuerySelect(sSql, paramFiltro);
                }
                catch (Exception ex)
                {
                    PSMessageBox.ShowError("Erro ao atualizar a grid: " + ex.Message);
                    return null;
                }

            }
        }

        public DataTable ExecuteFilter(List<Filter> filtros, string tabela, String[] chaves, List<PSFilter> psFilter)
        {
            this._tablename = tabela;
            this._keys = chaves;

            string sSql = string.Empty;
            DataTable dt = new DataTable();

            for (int i = 0; i < filtros.Count; i++)
            {
                if (filtros[i].selecionado == 1)
                {
                    sSql = string.Concat(this.ReadView(), " ", filtros[i].condicao, (psFilter.Count > 0) ? " AND " : "", SetDefaultFiltro(psFilter));
                }
            }

            if (sSql.Equals(""))
                return dt;
            else
                return dt = dbs.QuerySelect(sSql);
        }

        public DataTable ExecuteFilter(Filter filtro, string tabela, String[] chaves, List<PSFilter> psFilter)
        {
            this._tablename = tabela;
            this._keys = chaves;

            string sSql = string.Empty;
            DataTable dt = new DataTable();

            if (filtro != null && filtro.condicao != null)
            {
                sSql = string.Concat(this.ReadView(), filtro.condicao, (psFilter.Count > 0) ? " AND " : "", SetDefaultFiltro(psFilter));
            }
            else
            {
                sSql = string.Concat(this.ReadView(), SetDefaultFiltro(psFilter)).Replace("WHERE AND", "WHERE");
            }

            if (sSql.Equals(""))
                return dt;
            else
            {
                int qtdcond = 0;

                // quantidade de condições do filtro selecionado
                if (filtro != null && filtro.condicao != null)
                {
                    for (int j = 0; j < filtro.listaCondicao.Count; j++)
                    {
                        if (filtro.listaCondicao[j].operador != "IS NULL" && filtro.listaCondicao[j].operador != "IS NOT NULL" &&
                            filtro.listaCondicao[j].operador != "LIKE" && filtro.listaCondicao[j].operador != "NOT LIKE")
                        {
                            qtdcond = filtro.listaCondicao.Count;
                        }
                    }
                }

                // quantidade de condições do filtro default
                if (psFilter != null)
                {
                    qtdcond = qtdcond + psFilter.Count;
                }

                object[] paramFiltro = new object[qtdcond];

                int cont = 0;

                if (filtro != null && filtro.condicao != null)
                {
                    for (int j = 0; j < filtro.listaCondicao.Count; j++)
                    {
                        if (filtro.listaCondicao[j].operador != "IS NULL" && filtro.listaCondicao[j].operador != "IS NOT NULL" &&
                            filtro.listaCondicao[j].operador != "LIKE" && filtro.listaCondicao[j].operador != "NOT LIKE")
                        {
                            paramFiltro[cont] = filtro.listaCondicao[j].valor;
                            cont++;
                        }
                    }
                }

                if (psFilter != null)
                {
                    for (int j = 0; j < psFilter.Count; j++)
                    {
                        paramFiltro[cont] = psFilter[j].Value;
                        cont++;
                    }
                }

                return dt = dbs.QuerySelect(sSql, paramFiltro);
            }
        }

        public DataTable ExecuteFilterMasterDetail(string tabela, String[] chaves, List<DataField> filter)
        {
            this._tablename = tabela;
            this._keys = chaves;

            string sSql = string.Empty;
            DataTable dt = new DataTable();

            if (filter != null)
            {
                object[] parArr = new object[filter.Count];

                sSql = this.ReadView();

                for (int i = 0; i < filter.Count; i++)
                {
                    if (i == 0)
                    {
                        sSql = string.Concat(sSql, filter[i].Field, " =  ? ");
                    }
                    else
                    {
                        sSql = string.Concat(sSql, " AND ", filter[i].Field, " = ? ");
                    }

                    if (filter[i].Valor == null)
                        return dt;
                    else
                        parArr[i] = filter[i].Valor;

                }

                if (sSql.Equals(""))
                    return dt;
                else
                    return dt = dbs.QuerySelect(sSql, parArr);
            }
            else
            {
                if (sSql.Equals(""))
                    return dt;
                else
                    return dt = dbs.QuerySelect(sSql);
            }

        }

        public virtual void FillLookup(ref DataTable dt, DataField filter, List<PSFilter> padrao)
        {
            string sSql = this.ReadViewLookup();
            List<object> parArrobj = new List<object>();
            object[] parArr = null;

            if (padrao.Count > 0)
            {
                for (int i = 0; i < padrao.Count; i++)
                {
                    if (i == 0)
                    {
                        if (padrao[i].Oper.Trim() == "IN")
                        {
                            sSql = string.Concat(sSql, padrao[i].Field, " ", padrao[i].Oper, " ", padrao[i].Value);
                        }
                        else
                        {
                            sSql = string.Concat(sSql, padrao[i].Field, " ", padrao[i].Oper, " ", " ? ");
                            parArrobj.Add(padrao[i].Value);
                        }
                    }
                    else
                    {
                        if (padrao[i].Oper.Trim() == "IN")
                        {
                            sSql = string.Concat(sSql, " AND ", padrao[i].Field, " ", padrao[i].Oper, " ", padrao[i].Value);
                        }
                        else
                        {
                            sSql = string.Concat(sSql, " AND ", padrao[i].Field, " ", padrao[i].Oper, " ", " ? ");
                            parArrobj.Add(padrao[i].Value);
                        }
                    }
                }

                if (filter.Valor == null)
                    parArr = new object[parArrobj.Count];
                else
                    parArr = new object[parArrobj.Count + 1];

                for (int i = 0; i < parArrobj.Count; i++)
                {
                    parArr[i] = parArrobj[i];
                }

                if (filter.Valor != null)
                {
                    if (filter.Valor.ToString().Contains("%"))
                        sSql = string.Concat(sSql, " AND ", filter.Field, " LIKE ?");
                    else
                        sSql = string.Concat(sSql, " AND ", filter.Field, " = ?");

                    parArr[parArr.Length - 1] = filter.Valor;
                    dt = dbs.QuerySelect(sSql, parArr);
                }
                else
                {
                    dt = dbs.QuerySelect(sSql, parArr);
                }
            }
            else
            {
                parArr = new object[1];

                if (filter.Valor == null)
                {
                    sSql = sSql.Replace("WHERE", "");
                    dt = dbs.QuerySelect(sSql);
                }
                else
                {
                    parArr[0] = filter.Valor;

                    if (filter.Valor.ToString().Contains("%"))
                    {
                        sSql = string.Concat(sSql, filter.Field, " LIKE ?");
                        dt = dbs.QuerySelect(sSql, parArr);
                    }
                    else
                    {
                        sSql = string.Concat(sSql, filter.Field, " = ?");
                        dt = dbs.QuerySelect(sSql, parArr);
                    }
                }
            }
        }

        public virtual string ReadViewLookup()
        {
            return string.Concat("SELECT * FROM ", _tablename, " WHERE ");
        }

        public virtual string ReadView()
        {
            return string.Concat("SELECT * FROM ", _tablename, " WHERE ");
        }

        public DataTable ReadRecordEdit(params object[] parameters)
        {
            string sSQL = string.Concat("SELECT 1 FROM ", _tablename, " WITH(NOLOCK) WHERE ");
            object[] parArr = new object[_keys.Length];
            parArr = parameters;

            for (int i = 0; i < _keys.Length; i++)
            {
                if (i == 0)
                {
                    sSQL = string.Concat(sSQL, _keys[i], " =  ? ");
                }
                else
                {
                    sSQL = string.Concat(sSQL, " AND ", _keys[i], " = ? ");
                }
            }

            if (dbs.QueryFind(sSQL, parArr))
            {
                sSQL = string.Concat("SELECT * FROM ", _tablename, " WITH(NOLOCK) WHERE ");

                for (int i = 0; i < _keys.Length; i++)
                {
                    if (i == 0)
                    {
                        sSQL = string.Concat(sSQL, _keys[i], " =  ? ");
                    }
                    else
                    {
                        sSQL = string.Concat(sSQL, " AND ", _keys[i], " = ? ");
                    }
                }

                DataTable dt = dbs.QuerySelect(sSQL, parArr);

                return dt;
            }

            return null;
        }

        public List<DataField> ReadRecordEdit(DataRow objArr)
        {
            string sSQL = string.Concat("SELECT 1 FROM ", _tablename, " WITH(NOLOCK) WHERE ");
            object[] parArr = new object[_keys.Length];

            for (int i = 0; i < _keys.Length; i++)
            {
                if (i == 0)
                {
                    sSQL = string.Concat(sSQL, _keys[i], " =  ? ");
                }
                else
                {
                    sSQL = string.Concat(sSQL, " AND ", _keys[i], " = ? ");
                }

                for (int j = 0; j < objArr.Table.Columns.Count; j++)
                {
                    if (objArr.Table.Columns[j].ColumnName == _keys[i])
                    {
                        parArr[i] = objArr.ItemArray[j];
                    }
                }
            }

            if (dbs.QueryFind(sSQL, parArr))
            {
                sSQL = string.Concat("SELECT * FROM ", _tablename, " WITH(NOLOCK) WHERE ");

                for (int i = 0; i < _keys.Length; i++)
                {
                    if (i == 0)
                    {
                        sSQL = string.Concat(sSQL, _keys[i], " =  ? ");
                    }
                    else
                    {
                        sSQL = string.Concat(sSQL, " AND ", _keys[i], " = ? ");
                    }

                    for (int j = 0; j < objArr.Table.Columns.Count; j++)
                    {
                        if (objArr.Table.Columns[j].ColumnName == _keys[i])
                        {
                            parArr[i] = objArr.ItemArray[j];
                        }
                    }
                }

                DataTable dt = dbs.QuerySelect(sSQL, parArr);

                if(dt.Rows.Count > 0)
                {
                    List<DataField> objArr1 = new List<DataField>();

                    for(int i =0; i< dt.Rows.Count; i++)
                    {
                        for(int j = 0; j < dt.Columns.Count; j++)
                        {
                            DataField objDf = new DataField();

                            objDf.Field = dt.Columns[j].ColumnName;
                            objDf.Valor = dt.Rows[i][j];

                            objArr1.Add(objDf);
                        }
                    }

                    return objArr1;
                }
            }

            return null;
        }

        public virtual void ValidateRecord(List<DataField> objArr)
        {
            objArr = SetContextKey(objArr);

            // Valida Preenchimento dos Campos Chaves
            for (int i = 0; i < _keys.Length; i++)
            {
                for (int j = 0; j < objArr.Count; j++)
                {
                    if (objArr[j].Field.ToString() == _keys[i])
                    {
                        if (objArr[j].Valor == null)
                        {
                            throw new Exception(gb.MensagemDeValidacao(_tablename, objArr[j].Field.ToString()));
                        }

                        if (objArr[j].Valor.GetType() == typeof(string))
                        {
                            string objeto = (string)objArr[j].Valor;
                            if (objeto.Equals(string.Empty))
                            {
                                throw new Exception(gb.MensagemDeValidacao(_tablename, objArr[j].Field.ToString()));
                            }
                        }

                        if (objArr[j].Valor.GetType() == typeof(int))
                        {
                            string objeto = objArr[j].Valor.ToString();
                            if (objeto.Equals(string.Empty))
                            {
                                throw new Exception(gb.MensagemDeValidacao(_tablename, objArr[j].Field.ToString()));
                            }
                        }

                        if (objArr[j].Valor.GetType() == typeof(decimal))
                        {
                            string objeto = (string)objArr[j].Valor;
                            if (objeto.Equals(string.Empty))
                            {
                                throw new Exception(gb.MensagemDeValidacao(_tablename, objArr[j].Field.ToString()));
                            }
                        }

                        /*
                        if (objArr[j].valor.GetType() == typeof(DateTime))
                        {
                            string objeto = (string)objArr[j].valor;
                            if (objeto.Equals(string.Empty))
                            {
                                throw new Exception(gb.MensagemDeValidacao(_tabela, objArr[j].campo.ToString()));
                            }
                        }
                        */
                    }
                }
            }
        }

        public void ValidateKeyRecord(List<DataField> objArr)
        {
            objArr = SetContextKey(objArr);

            // Valida Preenchimento dos Campos Chaves
            for (int i = 0; i < _keys.Length; i++)
            {
                for (int j = 0; j < objArr.Count; j++)
                {
                    if (objArr[j].Field.ToString() == _keys[i])
                    {
                        if (objArr[j].Valor == null)
                        {
                            throw new Exception(gb.MensagemDeValidacao(_tablename, objArr[j].Field.ToString()));
                        }

                        if (objArr[j].Valor.GetType() == typeof(string))
                        {
                            string objeto = (string)objArr[j].Valor;
                            if (objeto.Equals(string.Empty))
                            {
                                throw new Exception(gb.MensagemDeValidacao(_tablename, objArr[j].Field.ToString()));
                            }
                        }

                        if (objArr[j].Valor.GetType() == typeof(int))
                        {
                            string objeto = objArr[j].Valor.ToString();
                            if (objeto.Equals(string.Empty))
                            {
                                throw new Exception(gb.MensagemDeValidacao(_tablename, objArr[j].Field.ToString()));
                            }
                        }

                        if (objArr[j].Valor.GetType() == typeof(decimal))
                        {
                            string objeto = (string)objArr[j].Valor;
                            if (objeto.Equals(string.Empty))
                            {
                                throw new Exception(gb.MensagemDeValidacao(_tablename, objArr[j].Field.ToString()));
                            }
                        }

                        /*
                        if (objArr[j].valor.GetType() == typeof(DateTime))
                        {
                            string objeto = (string)objArr[j].valor;
                            if (objeto.Equals(string.Empty))
                            {
                                throw new Exception(gb.MensagemDeValidacao(_tabela, objArr[j].campo.ToString()));
                            }
                        }
                        */
                    }
                }
            }
        }

        public bool ExistRecord(List<DataField> objArr)
        {
            objArr = SetContextKey(objArr);

            // Operação de INSERT ou UPDATE
            string sSQL = string.Concat("SELECT 1 FROM ", _tablename, " WITH(NOLOCK) WHERE ");
            object[] parArr = new object[_keys.Length];

            for (int i = 0; i < _keys.Length; i++)
            {
                if (i == 0)
                {
                    sSQL = string.Concat(sSQL, _keys[i], " =  ? ");
                }
                else
                {
                    sSQL = string.Concat(sSQL, " AND ", _keys[i], " = ? ");
                }

                for (int j = 0; j < objArr.Count; j++)
                {
                    if (objArr[j].Field.ToString() == _keys[i])
                    {
                        parArr[i] = objArr[j].Valor;
                    }
                }
            }

            for (int i = 0; i < parArr.Length; i++)
            {
                if (parArr[i] == null )
                {
                    return false;
                }            
            }

            if (dbs.QueryFind(sSQL, parArr))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual List<DataField> SaveRecord(List<DataField> objArr)
        {
            try
            {
                //Faz a validação do registro
                this.ValidateRecord(objArr);

                //objArr = SetContextKey(objArr);

                // Operação de INSERT ou UPDATE
                string sSQL = string.Concat("SELECT 1 FROM ", _tablename, " WITH(NOLOCK) WHERE ");
                object[] parArr = new object[_keys.Length];

                for (int i = 0; i < _keys.Length; i++)
                {
                    if (i == 0)
                    {
                        sSQL = string.Concat(sSQL, _keys[i], " =  ? ");
                    }
                    else
                    {
                        sSQL = string.Concat(sSQL, " AND ", _keys[i], " = ? ");
                    }

                    for (int j = 0; j < objArr.Count; j++)
                    {
                        if (objArr[j].Field.ToString() == _keys[i])
                        {
                            parArr[i] = objArr[j].Valor;
                        }
                    }
                }

                if (dbs.QueryFind(sSQL, parArr))
                {
                    return EditRecord(objArr);
                }
                else
                {
                    return InsertRecord(objArr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //List<DataField> vazia = new List<DataField>();
                //return vazia;
            } 
        }

        public virtual List<DataField> InsertRecord(List<DataField> objArr)
        {
            //GenerateDataFieldArchive(objArr);

            string sSQL = string.Concat("INSERT INTO ", _tablename, " (");
            object[] parArr = new object[objArr.Count];

            for (int i = 0; i < objArr.Count; i++)
            {
                if (i == objArr.Count - 1)
                {
                    if (i == 0)
                    {
                        sSQL = string.Concat(sSQL, objArr[i].Field, ")");
                    }
                    else
                    {
                        sSQL = string.Concat(sSQL, objArr[i].Field, ")");
                    }
                }
                else
                {
                    sSQL = string.Concat(sSQL, objArr[i].Field, ",");
                }
            }

            sSQL = string.Concat(sSQL, " VALUES(");

            for (int i = 0; i < objArr.Count; i++)
            {
                if (i == objArr.Count - 1)
                {
                    if (i == 0)
                    {
                        sSQL = string.Concat(sSQL, "?)");
                    }
                    else
                    {
                        sSQL = string.Concat(sSQL, "?)");
                    }
                }
                else
                {
                    sSQL = string.Concat(sSQL, "?,");
                }

                if (objArr[i].Autoinc == Global.TypeAutoinc.AutoInc || objArr[i].Autoinc == Global.TypeAutoinc.Max)
                {
                    if (int.Parse(objArr[i].Valor.ToString()) == 0)
                    {
                        if (objArr[i].Autoinc == Global.TypeAutoinc.AutoInc)
                        {
                            parArr[i] = gb.GetIdNovoRegistro(Contexto.Session.Empresa.CodEmpresa, _tablename);
                            objArr[i].Valor = parArr[i];
                        }

                        if (objArr[i].Autoinc == Global.TypeAutoinc.Max)
                        {
                            parArr[i] = gb.GetIdNovoRegistro(Contexto.Session.Empresa.CodEmpresa, _tablename, objArr, _keys);
                            objArr[i].Valor = parArr[i];
                        }                    
                    }
                    else
                    {
                        parArr[i] = objArr[i].Valor;
                    }
                }
                else
                {
                    parArr[i] = objArr[i].Valor;
                }
            }

            try
            {
                dbs.QueryExec(sSQL, parArr);
               string a  = AppLib.Context.poolConnection.Get("Start").ParseCommand(sSQL, parArr);

                List<DataField> dtf = new List<DataField>();

                for (int i = 0; i < objArr.Count; i++)
                {
                    if (IsKey(objArr[i].Field))
                    {
                        dtf.Add(objArr[i]);
                    }
                }

                return dtf;
            }
            catch(Exception ex)
            {
                throw ex;
            }  
        }

        public virtual List<DataField> EditRecord(List<DataField> objArr)
        {
            //GenerateDataFieldArchive(objArr);

            string sSQL = string.Concat("UPDATE ", _tablename, " SET ");
            object[] parArr = new object[objArr.Count];
            bool Flag = true;
            int index = 0;

            for (int i = 0; i < objArr.Count; i++)
            {
                if (!IsKey(objArr[i].Field))
                {
                    if (i == objArr.Count - 1)
                    {
                        if (i == 0)
                        {
                            sSQL = string.Concat(sSQL, objArr[i].Field, " = ? ");
                            //parArr[AddParameter(parArr)] = ParameterValue(objArr, objArr[i].campo);
                            parArr[index] = ParameterValue(objArr, objArr[i].Field);
                            index++;
                        }
                        else
                        {
                            sSQL = string.Concat(sSQL, objArr[i].Field, " = ? ");
                            //parArr[AddParameter(parArr)] = ParameterValue(objArr, objArr[i].campo);
                            parArr[index] = ParameterValue(objArr, objArr[i].Field);
                            index++;
                        }
                    }
                    else
                    {
                        sSQL = string.Concat(sSQL, objArr[i].Field, " = ?,");
                        //parArr[AddParameter(parArr)] = ParameterValue(objArr, objArr[i].campo);
                        parArr[index] = ParameterValue(objArr, objArr[i].Field);
                        index++;
                    }
                }
            }

            char ochar = sSQL[sSQL.Length - 1];

            if (ochar.Equals(','))
            {
                sSQL = sSQL.Substring(0, sSQL.Length - 1);            
            }

            sSQL = string.Concat(sSQL, " WHERE ");

            for (int i = 0; i < objArr.Count; i++)
            {
                if (IsKey(objArr[i].Field))
                {
                    if (Flag)
                    {
                        sSQL = string.Concat(sSQL, objArr[i].Field, " =  ? ");
                        //parArr[AddParameter(parArr)] = ParameterValue(objArr, objArr[i].campo);
                        parArr[index] = ParameterValue(objArr, objArr[i].Field);
                        index++;
                        Flag = false;
                    }
                    else
                    {
                        sSQL = string.Concat(sSQL, " AND ", objArr[i].Field, " = ? ");
                        //parArr[AddParameter(parArr)] = ParameterValue(objArr, objArr[i].campo);
                        parArr[index] = ParameterValue(objArr, objArr[i].Field);
                        index++;
                    }
                }
            }

            try
            {
                int cont = 0;
                for (int i = 0; i < objArr.Count; i++)
                {
                    if (!IsKey(objArr[i].Field))
                    {
                        cont++;
                    }
                }

                if (cont > 0)
                {
                    dbs.QueryExec(sSQL, parArr);
                }

                List<DataField> dtf = new List<DataField>();

                for (int i = 0; i < objArr.Count; i++)
                {
                    if (IsKey(objArr[i].Field))
                    {
                        dtf.Add(objArr[i]);
                    }
                }

                return dtf;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public virtual void DeleteRecord(List<DataField> objArr)
        {
            this.ValidateKeyRecord(objArr);

            objArr = SetContextKey(objArr);

            string sSQL = string.Concat("DELETE FROM ", _tablename, " WHERE ");
            object[] parArr = new object[_keys.Length];

            for (int i = 0; i < _keys.Length; i++)
            {
                if (i == 0)
                {
                    sSQL = string.Concat(sSQL, _keys[i], " =  ? ");
                }
                else
                {
                    sSQL = string.Concat(sSQL, " AND ", _keys[i], " = ? ");
                }

                for (int j = 0; j < objArr.Count; j++)
                {
                    if (objArr[j].Field.ToString() == _keys[i])
                    {
                        parArr[i] = objArr[j].Valor;
                    }
                }
            }

            dbs.QueryExec(sSQL, parArr);
        }

        private void DeleteCascade(List<DataField> objArr)
        {
            //ROTINA DE EXCLUSÃO DE ANEXO

            string strChave = string.Empty;

            for (int i = 0; i < objArr.Count; i++)
            {
                if (i == (objArr.Count - 1))
                {
                    strChave = string.Concat(strChave, objArr[i].Valor);
                }
                else
                {
                    strChave = string.Concat(strChave, objArr[i].Valor, ";");
                }
            }

            /*
            if (strChave != null || strChave != string.Empty)
            {
                string sSql = string.Empty;

                sSql = "DELETE FROM GANEXO WHERE CODPSPART = ? AND CODANEXO = ?";

                dbs.QueryExec(sSql, pspart, strChave);

                sSql = "DELETE FROM GIMAGEM WHERE CODIMAGEM = ?";

                dbs.QueryExec(sSql, idImg);
            }
             * */
        }
    }
}
