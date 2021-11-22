using System;
using System.Text;
using System.Collections.Generic;

namespace Relatorios
{
    public class parametro
    {
        public string OPERADOR { get; set; }
        public string CAMPO { get; set; }
        public string CONDICAO { get; set; }
        public string VALOR { get; set; }

        #region Tratamento do Query
        //Tratamento da Query
        public string tratamentoQuery(string sql, List<PS.Lib.DataField> Parametros)
        {
            //Tratamento da QUERY
            string condicao = string.Empty;
            for (int i = 0; i < Parametros.Count; i++)
            {
                if (Parametros[i].Field.Equals("OPERADOR"))
                {
                    if (Parametros[i].Valor.Equals("E"))
                    {
                        sql = sql + " AND";
                    }
                    else
                    {
                        sql = sql + " OR";
                    }
                }
                if (Parametros[i].Field.Equals("CAMPO"))
                {
                    sql = sql + " " + Parametros[i].Valor;
                }
                if (Parametros[i].Field.Equals("CONDICAO"))
                {
                    switch (Parametros[i].Valor.ToString())
                    {
                        case "IGUAL A":
                            condicao = " = ";
                            break;
                        case "IGUAL A VÁRIOS":
                            condicao = " IN(";
                            break;
                        case "DIFERENTE DE":
                            condicao = " <>";
                            break;
                        case "DIFERENTE DE VÁRIOS":
                            condicao = " NOT IN(";
                            break;
                        case "MAIOR QUE":
                            condicao = " >";
                            break;
                        case "MENOR QUE":
                            condicao = " <";
                            break;
                        case "MAIOR OU IGUAL":
                            condicao = " >=";
                            break;
                        case "MENOR OU IGUAL":
                            condicao = " <=";
                            break;
                        case "NULO":
                            condicao = " IS NULL";
                            break;
                        case "NÃO NULO":
                            condicao = " IS NOT NULL";
                            break;
                        case "CONTÉM":
                            condicao = " LIKE '%";
                            break;
                        case "NÃO CONTÉM":
                            condicao = " NOT LIKE '%";
                            break;
                        default:
                            break;
                    }
                }

                if (Parametros[i].Field.Equals("VALOR"))
                {
                    switch (condicao)
                    {
                        case " IN(":
                            sql = sql + condicao + Parametros[i].Valor.ToString() + " )";
                            break;
                        case " NOT IN(":
                            sql = sql + condicao + Parametros[i].Valor.ToString() + " )";
                            break;
                        case " IS NULL":
                            sql = sql + condicao;
                            break;
                        case " IS NOT NULL":
                            sql = sql + condicao;
                            break;
                        case " LIKE '%":
                            sql = sql + condicao + Parametros[i].Valor.ToString() + "%'";
                            break;
                        case " NOT LIKE '%":
                            sql = sql + condicao + Parametros[i].Valor.ToString() + "%'";
                            break;
                        default:
                            sql = sql + condicao + "'" + Parametros[i].Valor.ToString() + "'";
                            break;
                    }
                }
                if (Parametros[i].Field.Equals("ORDEM"))
                {
                    if (!string.IsNullOrEmpty(Parametros[i].Valor.ToString()))
                    {
                        sql = sql + " ORDER BY " + Parametros[i].Valor.ToString();
                    }
                }
                if (Parametros[i].Field.Equals("TIPOORDEM"))
                {
                    if (!string.IsNullOrEmpty(Parametros[i].Valor.ToString()))
                    {
                        sql = sql + " " + Parametros[i].Valor.ToString();
                    }
                }
            }
            return sql;
        }

        public string tratamentoQuery(string sql, List<parametro> Parametros)
        {
            //Tratamento da QUERY
            string condicao = string.Empty;
            for (int i = 0; i < Parametros.Count; i++)
            {

                if (Parametros[i].OPERADOR.Equals("E"))
                {
                    sql = sql + " AND";
                }
                else
                {
                    sql = sql + " OR";
                }


                sql = sql + " " + Parametros[i].CAMPO;

                switch (Parametros[i].CONDICAO.ToString())
                {
                    case "IGUAL A":
                        condicao = " = ";
                        break;
                    case "IGUAL A VÁRIOS":
                        condicao = " IN(";
                        break;
                    case "DIFERENTE DE":
                        condicao = " <>";
                        break;
                    case "DIFERENTE DE VÁRIOS":
                        condicao = " NOT IN(";
                        break;
                    case "MAIOR QUE":
                        condicao = " >";
                        break;
                    case "MENOR QUE":
                        condicao = " <";
                        break;
                    case "MAIOR OU IGUAL":
                        condicao = " >=";
                        break;
                    case "MENOR OU IGUAL":
                        condicao = " <=";
                        break;
                    case "NULO":
                        condicao = " IS NULL";
                        break;
                    case "NÃO NULO":
                        condicao = " IS NOT NULL";
                        break;
                    case "CONTÉM":
                        condicao = " LIKE '%";
                        break;
                    case "NÃO CONTÉM":
                        condicao = " NOT LIKE '%";
                        break;
                    default:
                        break;
                }


                switch (condicao)
                {
                    case " IN(":
                        sql = sql + condicao + Parametros[i].VALOR.ToString() + " )";
                        break;
                    case " NOT IN(":
                        sql = sql + condicao + Parametros[i].VALOR.ToString() + " )";
                        break;
                    case " IS NULL":
                        sql = sql + condicao;
                        break;
                    case " IS NOT NULL":
                        sql = sql + condicao;
                        break;
                    case " LIKE '%":
                        sql = sql + condicao + Parametros[i].VALOR.ToString() + "%'";
                        break;
                    case " NOT LIKE '%":
                        sql = sql + condicao + Parametros[i].VALOR.ToString() + "%'";
                        break;
                    default:
                        sql = sql + condicao + "'" + Parametros[i].VALOR.ToString() + "'";
                        break;
                }

            }
            return sql;
        }
        #endregion
    }

}
