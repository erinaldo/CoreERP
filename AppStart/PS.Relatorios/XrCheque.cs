using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Relatorios
{
    public partial class XrCheque : DevExpress.XtraReports.UI.XtraReport
    {
        private string codCheque;
        private int codEmpresa;
        private string DataCheque;

        public XrCheque(string _codCheque, int _codEmpresa)
        {
            InitializeComponent();
            codCheque = _codCheque;
            codEmpresa = _codEmpresa;
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel26.Text = xrLabel11.Text;
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sql = @"SELECT FCHEQUE.NUMERO,VALOR,  DBO.ZFCNEXTENSO(VALOR) VALOREXTENSO, GCIDADE.NOME CIDADE, FCHEQUE.DATABOA, GBANCO.NOME, FCONTA.CODCONTA, FCONTA.CODAGENCIA, FCONTA.NUMCONTA, FCHEQUE.NUMERO
FROM FCHEQUE 
INNER JOIN GEMPRESA ON FCHEQUE.CODEMPRESA = GEMPRESA.CODEMPRESA 
LEFT OUTER JOIN GCIDADE ON GEMPRESA.CODCIDADE = GCIDADE.CODCIDADE
INNER JOIN FCONTA ON FCHEQUE.CODEMPRESA = FCONTA.CODEMPRESA AND FCHEQUE.CODCONTA = FCONTA.CODCONTA
LEFT OUTER JOIN GBANCO ON FCONTA.CODEMPRESA = GBANCO.CODEMPRESA AND FCONTA.CODBANCO = GBANCO.CODBANCO
WHERE CODCHEQUE = ? AND FCHEQUE.CODEMPRESA = ?";
            DataTable dt = new DataTable();
            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] {codCheque , codEmpresa });
            if (dt.Rows.Count > 0)
            {
                xrLabel4.Text = dt.Rows[0]["NOME"].ToString();
                xrLabel6.Text = dt.Rows[0]["CODAGENCIA"].ToString();
                xrLabel7.Text = dt.Rows[0]["NUMCONTA"].ToString();
                xrLabel10.Text = dt.Rows[0]["NUMERO"].ToString();
                xrLabel11.Text = dt.Rows[0]["VALOR"].ToString();
                xrLabel12.Text = dt.Rows[0]["VALOREXTENSO"].ToString();
                xrLabel13.Text = dt.Rows[0]["CIDADE"].ToString();

                if (string.IsNullOrEmpty(dt.Rows[0]["DATABOA"].ToString()))
                {
                    DataCheque = DateTime.Now.ToLongDateString();
                }
                else
                {
                    DataCheque = Convert.ToDateTime(dt.Rows[0]["DATABOA"]).ToLongDateString();
                }

                xrLabel14.Text = DataCheque;
            }
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sql = @"SELECT FLANCA.CODLANCA, FLANCA.NUMERO, (VCLIFOR.CODCLIFOR + ' - ' + VCLIFOR.NOMEFANTASIA) CLIENTE, FLANCA.DATAVENCIMENTO, FLANCA.VLBAIXADO
FROM FLANCA
INNER JOIN VCLIFOR ON FLANCA.CODEMPRESA = VCLIFOR.CODEMPRESA AND FLANCA.CODCLIFOR = VCLIFOR.CODCLIFOR
INNER JOIN FEXTRATOLANCA ON FLANCA.CODEMPRESA = FEXTRATOLANCA.CODEMPRESA AND FLANCA.CODLANCA = FEXTRATOLANCA.CODLANCA
INNER JOIN FCHEQUE ON FEXTRATOLANCA.CODEMPRESA = FCHEQUE.CODEMPRESA AND FEXTRATOLANCA.CODCHEQUE = FCHEQUE.CODCHEQUE
WHERE FEXTRATOLANCA.CODCHEQUE = ? AND FLANCA.CODEMPRESA = ? AND NFOUDUP <> 1
";
           
//            string sql = @"SELECT CODLANCA, FLANCA.NUMERO, (VCLIFOR.CODCLIFOR + ' - ' +  VCLIFOR.NOMEFANTASIA) CLIENTE, FLANCA.DATAVENCIMENTO, FLANCA.VLBAIXADO
// FROM FLANCA 
// INNER JOIN VCLIFOR ON FLANCA.CODEMPRESA = VCLIFOR.CODEMPRESA AND FLANCA.CODCLIFOR = VCLIFOR.CODCLIFOR WHERE CODCHEQUE = ? AND FLANCA.CODEMPRESA = ? AND NFOUDUP <> 1
//";
            DataTable dt = new DataTable();
            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { codCheque, codEmpresa });
            DetailReport.DataSource = dt;

            xrLabel19.DataBindings.Add("Text", null, "CODLANCA");
            xrLabel18.DataBindings.Add("Text", null, "NUMERO");
            xrLabel15.DataBindings.Add("Text", null, "CLIENTE");
            xrLabel23.DataBindings.Add("Text", null, "DATAVENCIMENTO", "{0:dd/MM/yyyy}");
            xrLabel24.DataBindings.Add("Text", null, "VLBAIXADO", "{0:n2}");

        }

    }
}
    