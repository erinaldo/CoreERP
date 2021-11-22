using System.Collections.Generic;
using System.Data;
using System;
using System.Drawing;

namespace Relatorios
{
    public partial class XrProgramacaoSemanal : DevExpress.XtraReports.UI.XtraReport
    {

        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private List<PS.Lib.DataField> Parametros { get; set; }

        public XrProgramacaoSemanal(List<PS.Lib.DataField> Params)
        {
            InitializeComponent();
            this.Parametros = Params;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel2.Text = "Pedidos de: " + Parametros[4].Valor.ToString() + " a " + Parametros[5].Valor.ToString();
            //xrLabel4.Text = System.Globalization.CalendarWeekRulex
            System.Globalization.DateTimeFormatInfo dfi = System.Globalization.DateTimeFormatInfo.CurrentInfo;
            DateTime date1 = new DateTime(2011, 1, 1);
            System.Globalization.Calendar cal = dfi.Calendar;
            xrLabel4.Text = cal.GetWeekOfYear(Convert.ToDateTime(Parametros[4].Valor), dfi.CalendarWeekRule, dfi.FirstDayOfWeek).ToString();
            if (Parametros.Count.Equals(5))
            {
                xrLabel1.Text = "PROGRAMAÇÃO SEMANAL MOTORISTA";
            }
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sql;
             DataTable dt;
            if (!string.IsNullOrEmpty(Parametros[6].Valor.ToString()))
            {
                sql = @"SELECT GOPER.DATAENTREGA, GOPER.NUMERO, VCLIFOR.NOMEFANTASIA AS CLIENTE, VPRODUTO.CODPRODUTO, GOPERITEM.QUANTIDADE QUANTIDADE,   GOPERITEM.CODUNIDOPER, GOPER.OBSERVACAO, VTRANSPORTADORA.NOME 
FROM
GOPER, VCLIFOR, VPRODUTO, GOPERITEM, VTRANSPORTADORA
WHERE
GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA
AND GOPER.CODOPER = GOPERITEM.CODOPER
AND GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA
AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR
AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO
AND GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA
AND GOPER.CODEMPRESA = VTRANSPORTADORA.CODEMPRESA
AND GOPER.CODTRANSPORTADORA = VTRANSPORTADORA.CODTRANSPORTADORA
AND GOPER.CODSTATUS = 0
AND convert(date, GOPER.DATAENTREGA) >= ?
AND convert(date, GOPER.DATAENTREGA) <= ?
AND GOPER.CODEMPRESA = ?
AND VTRANSPORTADORA.CODTRANSPORTADORA = ?
AND GOPER.CODFILIAL = ? 
AND GOPER.CODTIPOPER = '2.02'";
                sql = new parametro().tratamentoQuery(sql, Parametros);
                dt = dbs.QuerySelect(sql, new Object[] { Convert.ToDateTime(this.Parametros[4].Valor), Convert.ToDateTime(this.Parametros[5].Valor), this.Parametros[2].Valor, this.Parametros[6].Valor, Parametros[3].Valor });

            }
            else
            {
//                //sql = @"SELECT GOPER.DATAENTREGA, GOPER.NUMERO, VCLIFOR.NOMEFANTASIA AS CLIENTE, VPRODUTO.CODPRODUTO, GOPERITEM.QUANTIDADE QUANTIDADE,   GOPERITEM.CODUNIDOPER, GOPER.OBSERVACAO, VTRANSPORTADORA.NOME 
//FROM
//GOPER, VCLIFOR, VPRODUTO, GOPERITEM, VTRANSPORTADORA
//WHERE
//GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA
//AND GOPER.CODOPER = GOPERITEM.CODOPER
//AND GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA
//AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR
//AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO
//AND GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA
//AND GOPER.CODEMPRESA = VTRANSPORTADORA.CODEMPRESA
//AND GOPER.CODTRANSPORTADORA = VTRANSPORTADORA.CODTRANSPORTADORA
//AND GOPER.CODSTATUS = 0
//AND convert(date, GOPER.DATAENTREGA) >= ?
//AND convert(date, GOPER.DATAENTREGA) <= ?
//AND GOPER.CODEMPRESA = ?
//AND GOPER.CODFILIAL = ?
//AND GOPER.CODTIPOPER = '2.02'";
                sql = @"SELECT GOPER.DATAENTREGA, GOPER.NUMERO, VCLIFOR.NOMEFANTASIA AS CLIENTE, VPRODUTO.CODPRODUTO, GOPERITEM.QUANTIDADE QUANTIDADE,   GOPERITEM.CODUNIDOPER, GOPER.OBSERVACAO, VTRANSPORTADORA.NOME 
FROM
GOPER
INNER JOIN VCLIFOR ON GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR AND GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA
INNER JOIN GOPERITEM ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER
INNER JOIN VPRODUTO ON GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO
LEFT JOIN VTRANSPORTADORA ON GOPER.CODTRANSPORTADORA = VTRANSPORTADORA.CODTRANSPORTADORA AND GOPER.CODEMPRESA = VTRANSPORTADORA.CODEMPRESA
WHERE
GOPER.CODSTATUS = 0
AND CONVERT(DATE, GOPER.DATAENTREGA) >= ?
AND CONVERT(DATE, GOPER.DATAENTREGA) <= ?
AND GOPER.CODEMPRESA = ?
AND GOPER.CODFILIAL = ?
AND GOPER.CODTIPOPER = '2.02'";
                sql = new parametro().tratamentoQuery(sql, Parametros);
                dt = dbs.QuerySelect(sql, new Object[] { Convert.ToDateTime(this.Parametros[4].Valor), Convert.ToDateTime(this.Parametros[5].Valor), this.Parametros[2].Valor, Parametros[3].Valor });
            }
           
           
            System.Collections.ArrayList lista = new System.Collections.ArrayList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item item = new item();
                item.dataEntrega = Convert.ToDateTime(dt.Rows[i]["DATAENTREGA"]);
                item.numero = dt.Rows[i]["NUMERO"].ToString();
                item.cliente = dt.Rows[i]["CLIENTE"].ToString();
                item.codProduto = dt.Rows[i]["CODPRODUTO"].ToString();
                item.qtd = Convert.ToDecimal(dt.Rows[i]["QUANTIDADE"]).ToString(); 
                if (dt.Rows[i]["CODPRODUTO"].ToString().IndexOf("6", 0, 4).Equals(3))
                {
                    item.total_qtd = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i]["QUANTIDADE"].ToString()) / 20).ToString() + " M³";
                }
                item.observacao = dt.Rows[i]["OBSERVACAO"].ToString();
                item.transportadora = dt.Rows[i]["NOME"].ToString();
                item.codUnidOper = dt.Rows[i]["CODUNIDOPER"].ToString();
                lista.Add(item);
            }
            this.DetailReport.DataSource = lista;
            xrLabel12.DataBindings.Add("Text", null, "dataEntrega", "{0: dd/MM/yyyy}");
            xrLabel13.DataBindings.Add("Text", null, "numero");
            xrLabel14.DataBindings.Add("Text", null, "cliente");
            xrLabel15.DataBindings.Add("Text", null, "codProduto");
            xrLabel16.DataBindings.Add("Text", null, "qtd");
            xrLabel17.DataBindings.Add("Text", null, "total_qtd");
            xrLabel18.DataBindings.Add("Text", null, "observacao");
            xrLabel19.DataBindings.Add("Text", null, "transportadora");
            xrLabel20.DataBindings.Add("Text", null, "codUnidOper");
        }

     
    }
}

