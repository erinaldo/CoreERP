using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorios
{
    public partial class XrBalancete : DevExpress.XtraReports.UI.XtraReport
    {
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private List<PS.Lib.DataField> Parametros { get; set; }
        private decimal subtotal = 0;

        public XrBalancete(List<PS.Lib.DataField> Params)
        {
            InitializeComponent();
            this.Parametros = Params;
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sql = @"SELECT 
FEXTRATO.DATACOMPENSACAO, 
VCLIFOR.NOMEFANTASIA, 
FEXTRATO.CODCONTA, 
FEXTRATO.NUMERODOCUMENTO, 
FEXTRATO.HISTORICO, 
FEXTRATO.VALOR  
FROM 
FEXTRATO 
LEFT JOIN FLANCA ON FEXTRATO.IDEXTRATO = FLANCA.IDEXTRATO AND FEXTRATO.CODEMPRESA = FLANCA.CODEMPRESA 
LEFT JOIN VCLIFOR ON FLANCA.CODEMPRESA = VCLIFOR.CODEMPRESA AND FLANCA.CODCLIFOR = VCLIFOR.CODCLIFOR
WHERE 
convert(date, FEXTRATO.DATACOMPENSACAO) >= ?
AND convert(date, FEXTRATO.DATACOMPENSACAO) <= ?
AND FEXTRATO.CODEMPRESA = ?
AND FEXTRATO.CODCONTA = ?
AND FEXTRATO.TIPO = 0
AND FEXTRATO.CODFILIAL = ?";
            sql = new parametro().tratamentoQuery(sql, Parametros);

            System.Data.DataTable dt = dbs.QuerySelect(sql, new object[] { Convert.ToDateTime(Parametros[4].Valor), Convert.ToDateTime(Parametros[5].Valor), Parametros[2].Valor, Parametros[6].Valor, Parametros[3].Valor });
            DetailReport.DataSource = dt;
            xrLabel23.DataBindings.Add("Text", null, "DATACOMPENSACAO", "{0:dd/MM/yyyy}");
            xrLabel7.DataBindings.Add("Text", null, "NOMEFANTASIA");
            xrLabel21.DataBindings.Add("Text", null, "NUMERODOCUMENTO");
            xrLabel20.DataBindings.Add("Text", null, "HISTORICO");
            xrLabel22.DataBindings.Add("Text", null, "VALOR", "{0:n2}");
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel52.Text = string.Format("{0:dd/MM/yyyy}  a  {1:dd/MM/yyyy}", Parametros[4].Valor, Parametros[5].Valor);
            xrLabel6.Text = Parametros[6].Valor.ToString();
            xrLabel54.Text = Parametros[0].Valor.ToString() + " / " + Parametros[3].Valor.ToString();
        }

        private void Detail1_AfterPrint(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xrLabel22.Text))
            {
                xrLabel22.Text = "0,00";
            }
            subtotal = subtotal + Convert.ToDecimal(xrLabel22.Text);
        }

        private void ReportFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel30.Text = string.Format("{0:n2}", subtotal);
            subtotal = 0;
        }

        private void DetailReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sql = @"SELECT 
FEXTRATO.DATACOMPENSACAO, 
VCLIFOR.NOMEFANTASIA, 
FEXTRATO.CODCONTA, 
FEXTRATO.NUMERODOCUMENTO, 
FEXTRATO.HISTORICO, 
FEXTRATO.VALOR  
FROM 
FEXTRATO 
LEFT JOIN FLANCA ON FEXTRATO.IDEXTRATO = FLANCA.IDEXTRATO AND FEXTRATO.CODEMPRESA = FLANCA.CODEMPRESA 
LEFT JOIN VCLIFOR ON FLANCA.CODEMPRESA = VCLIFOR.CODEMPRESA AND FLANCA.CODCLIFOR = VCLIFOR.CODCLIFOR
WHERE 
convert(date, FEXTRATO.DATACOMPENSACAO) >= ?
AND convert(date, FEXTRATO.DATACOMPENSACAO) <= ?
AND FEXTRATO.CODEMPRESA = ?
AND FEXTRATO.CODCONTA = ?
AND FEXTRATO.TIPO IN (1,2)
AND FEXTRATO.CODFILIAL = ?";
            sql = new parametro().tratamentoQuery(sql, Parametros);
            System.Data.DataTable dt = dbs.QuerySelect(sql, new object[] { Convert.ToDateTime(Parametros[4].Valor), Convert.ToDateTime(Parametros[5].Valor), Parametros[2].Valor, Parametros[6].Valor, Parametros[3].Valor });
            DetailReport1.DataSource = dt;
            xrLabel19.DataBindings.Add("Text", null, "DATACOMPENSACAO", "{0:dd/MM/yyyy}");
            xrLabel25.DataBindings.Add("Text", null, "NOMEFANTASIA");
            xrLabel27.DataBindings.Add("Text", null, "NUMERODOCUMENTO");
            xrLabel26.DataBindings.Add("Text", null, "HISTORICO");
            xrLabel29.DataBindings.Add("Text", null, "VALOR", "{0:n2}");
        }
        private void Detail2_AfterPrint(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xrLabel29.Text))
            {
                xrLabel29.Text = "0,00";
            }
            subtotal = subtotal + Convert.ToDecimal(xrLabel29.Text);
        }
        private void ReportFooter2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel31.Text = string.Format("{0:n2}", subtotal);
            subtotal = 0;
        }
        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel57.Text = xrLabel30.Text;
            xrLabel56.Text = xrLabel31.Text;
            xrLabel55.Text = string.Format("{0:n2}", Convert.ToDecimal(xrLabel30.Text) - Convert.ToDecimal(xrLabel31.Text));
        }
        #region Tratamento do Query
        //Tratamento da Query
        private string tratamentoQuery(string sql)
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
            }
            return sql;
        }
        #endregion
    }
}
