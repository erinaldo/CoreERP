using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
namespace Relatorios
{
    public partial class XrConsolidadoGHPC : DevExpress.XtraReports.UI.XtraReport
    {
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private List<PS.Lib.DataField> Parametros { get; set; }
        private decimal banco1, banco2, banco3, banco4, total;

        public XrConsolidadoGHPC(List<PS.Lib.DataField> Params)
        {
            InitializeComponent();
            this.Parametros = Params;
        }


        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            string sql = @"SELECT
(
  SELECT
  ISNULL((FCONTA.SALDODATABASE),0) +
  ISNULL(
    (SELECT SUM(VALOR)
     FROM
     (
     SELECT
     CASE 
     WHEN FEXTRATO.TIPO IN (1, 2, 4)THEN (CONVERT(DECIMAL (15,4),SUM(FEXTRATO.VALOR * -1)))
     ELSE (CONVERT(DECIMAL (15,4),SUM(FEXTRATO.VALOR)))
     END VALOR
     FROM FEXTRATO
     WHERE FEXTRATO.COMPENSADO = 1
     AND convert(date, FEXTRATO.DATACOMPENSACAO) < ?
     AND FEXTRATO.CODCONTA = ?
     GROUP BY FEXTRATO.TIPO
     )X
    ),0
    ) MOVIMENTO
  FROM FCONTA
  WHERE FCONTA.CODEMPRESA = ?
    AND FCONTA.CODCONTA = ?
) CONTA1,

(
  SELECT
  ISNULL((FCONTA.SALDODATABASE),0) +
  ISNULL(
    (SELECT SUM(VALOR)
     FROM
     (
     SELECT
     CASE 
     WHEN FEXTRATO.TIPO IN (1, 2, 4)THEN (CONVERT(DECIMAL (15,4),SUM(FEXTRATO.VALOR * -1)))
     ELSE (CONVERT(DECIMAL (15,4),SUM(FEXTRATO.VALOR)))
     END VALOR
     FROM FEXTRATO
     WHERE FEXTRATO.COMPENSADO = 1
     AND convert(date, FEXTRATO.DATACOMPENSACAO) < ?
     AND FEXTRATO.CODCONTA = ?
     GROUP BY FEXTRATO.TIPO
     )X
    ),0
    ) MOVIMENTO
  FROM FCONTA
  WHERE FCONTA.CODEMPRESA = ?
    AND FCONTA.CODCONTA = ?
) CONTA2,

(
  SELECT
  ISNULL((FCONTA.SALDODATABASE),0) +
  ISNULL(
    (SELECT SUM(VALOR)
     FROM
     (
     SELECT
     CASE 
     WHEN FEXTRATO.TIPO IN (1, 2, 4)THEN (CONVERT(DECIMAL (15,4),SUM(FEXTRATO.VALOR * -1)))
     ELSE (CONVERT(DECIMAL (15,4),SUM(FEXTRATO.VALOR)))
     END VALOR
     FROM FEXTRATO
     WHERE FEXTRATO.COMPENSADO = 1
     AND convert(date, FEXTRATO.DATACOMPENSACAO) < ?
     AND FEXTRATO.CODCONTA = ?
     GROUP BY FEXTRATO.TIPO
     )X
    ),0
    ) MOVIMENTO
  FROM FCONTA
  WHERE FCONTA.CODEMPRESA = ?
    AND FCONTA.CODCONTA = ?
) CONTA3,

(
  SELECT
  ISNULL((FCONTA.SALDODATABASE),0) +
  ISNULL(
    (SELECT SUM(VALOR)
     FROM
     (
     SELECT
     CASE 
     WHEN FEXTRATO.TIPO IN (1, 2, 4)THEN (CONVERT(DECIMAL (15,4),SUM(FEXTRATO.VALOR * -1)))
     ELSE (CONVERT(DECIMAL (15,4),SUM(FEXTRATO.VALOR)))
     END VALOR
     FROM FEXTRATO
     WHERE FEXTRATO.COMPENSADO = 1
     AND convert(date, FEXTRATO.DATACOMPENSACAO) < ?
     AND FEXTRATO.CODCONTA = ?
     GROUP BY FEXTRATO.TIPO
     )X
    ),0
    ) MOVIMENTO
  FROM FCONTA
  WHERE FCONTA.CODEMPRESA = ?
    AND FCONTA.CODCONTA = ?
) CONTA4,

(
SELECT SUM(X.MOVIMENTO) FROM (
  SELECT
  ISNULL((FCONTA.SALDODATABASE),0) +
  ISNULL(
    (SELECT SUM(VALOR)
     FROM
     (
     SELECT
     CASE 
     WHEN FEXTRATO.TIPO IN (1, 2, 4)THEN (CONVERT(DECIMAL (15,4),SUM(FEXTRATO.VALOR * -1)))
     ELSE (CONVERT(DECIMAL (15,4),SUM(FEXTRATO.VALOR)))
     END VALOR
     FROM FEXTRATO
     WHERE FEXTRATO.COMPENSADO = 1
     AND convert(date, FEXTRATO.DATACOMPENSACAO) < ?
     AND FEXTRATO.CODCONTA = FCONTA.CODCONTA
     GROUP BY FEXTRATO.TIPO
     )X
    ),0
    )   MOVIMENTO
  FROM FCONTA
  WHERE FCONTA.CODEMPRESA = ?
    AND FCONTA.CODCONTA IN (?,?,?,?)
    )X
    ) TOTAL
 
FROM GEMPRESA
WHERE CODEMPRESA =?";

            DateTime data = Convert.ToDateTime(Parametros[4].Valor);
            string empresa;
            empresa = Parametros[0].Valor.ToString();
            xrLabel52.Text = string.Format("{0:dd/MM/yyyy}  a  {1:dd/MM/yyyy}", data, Parametros[5].Valor);
            xrLabel54.Text = empresa + " / " + Parametros[3].Valor.ToString();
            System.Data.DataTable dt = dbs.QuerySelect(sql, new object[] { data, "001-01", empresa, "001-01", data, "237-01", empresa, "237-01", data, "341-01", empresa, "341-01", data, "999-01", empresa, "999-01", data, empresa, "001-01", "237-01", "341-01", "999-01", empresa });
            xrLabel10.Text = string.Format("{0:n2}", dt.Rows[0]["CONTA1"]);
            xrLabel11.Text = string.Format("{0:n2}", dt.Rows[0]["CONTA2"]);
            xrLabel12.Text = string.Format("{0:n2}", dt.Rows[0]["CONTA3"]);
            xrLabel13.Text = string.Format("{0:n2}", dt.Rows[0]["CONTA4"]);
            if (string.IsNullOrEmpty(dt.Rows[0]["TOTAL"].ToString()))
            {
                xrLabel16.Text = "0,00";
            }
            else
            {
                xrLabel16.Text = string.Format("{0:n2}", dt.Rows[0]["TOTAL"]);
            }

        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sql = @"SELECT CODNATUREZA, DESCRICAO, CONTA1, CONTA2, CONTA3, CONTA4, TOTAL
FROM
(
	SELECT CODNATUREZA, DESCRICAO, CODEMPRESA, 
	ISNULL((
	SELECT SUM(VALOR) 
	FROM FEXTRATO
	WHERE FEXTRATO.CODEMPRESA = VNATUREZAORCAMENTO.CODEMPRESA
	AND FEXTRATO.CODNATUREZAORCAMENTO = VNATUREZAORCAMENTO.CODNATUREZA
	AND CODCONTA = '001-01'
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (0, 5)
	),0) CONTA1,
	
	ISNULL((
	SELECT SUM(VALOR) 
	FROM FEXTRATO
	WHERE FEXTRATO.CODEMPRESA = VNATUREZAORCAMENTO.CODEMPRESA
	AND FEXTRATO.CODNATUREZAORCAMENTO = VNATUREZAORCAMENTO.CODNATUREZA
	AND CODCONTA = '237-01'
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (0, 5)
	),0)  CONTA2,

	ISNULL((
	SELECT SUM(VALOR) 
	FROM FEXTRATO
	WHERE FEXTRATO.CODEMPRESA = VNATUREZAORCAMENTO.CODEMPRESA
	AND FEXTRATO.CODNATUREZAORCAMENTO = VNATUREZAORCAMENTO.CODNATUREZA
	AND CODCONTA = '341-01'
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (0, 5)
	),0)  CONTA3,	
	
	ISNULL((
	SELECT SUM(VALOR) 
	FROM FEXTRATO
	WHERE FEXTRATO.CODEMPRESA = VNATUREZAORCAMENTO.CODEMPRESA
	AND FEXTRATO.CODNATUREZAORCAMENTO = VNATUREZAORCAMENTO.CODNATUREZA
	AND CODCONTA = '999-01'
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (0, 5)
	
	),0)  CONTA4,
	
	ISNULL((
	SELECT SUM(VALOR) 
	FROM FEXTRATO
	WHERE FEXTRATO.CODEMPRESA = VNATUREZAORCAMENTO.CODEMPRESA
	AND FEXTRATO.CODNATUREZAORCAMENTO = VNATUREZAORCAMENTO.CODNATUREZA
	AND CODCONTA IN( '001-01','237-01', '341-01', '999-01' )
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (0, 5)
	),0)  TOTAL
FROM VNATUREZAORCAMENTO
)X
WHERE 
(
CONTA1 > 0 OR
CONTA2 > 0 OR
CONTA3 > 0 OR
CONTA4 > 0 OR
TOTAL > 0
AND CODEMPRESA = ?
) 
";
            DateTime dataIni = Convert.ToDateTime(Parametros[4].Valor);
            DateTime dataFin = Convert.ToDateTime(Parametros[5].Valor);

            string empresa, filial;
            empresa = Parametros[2].Valor.ToString();
            filial = Parametros[3].Valor.ToString();

            sql = new parametro().tratamentoQuery(sql, Parametros);

            System.Data.DataTable dt = dbs.QuerySelect(sql, new object[] { dataIni, dataFin, filial, dataIni, dataFin, filial, dataIni, dataFin, filial, dataIni, dataFin, filial, dataIni, dataFin, filial, empresa });
            DetailReport.DataSource = dt;
            xrLabel23.DataBindings.Add("Text", null, "CODNATUREZA");
            xrLabel25.DataBindings.Add("Text", null, "DESCRICAO");
            xrLabel21.DataBindings.Add("Text", null, "CONTA1", "{0:n2}");
            xrLabel20.DataBindings.Add("Text", null, "CONTA2", "{0:n2}");
            xrLabel19.DataBindings.Add("Text", null, "CONTA3", "{0:n2}");
            xrLabel18.DataBindings.Add("Text", null, "CONTA4", "{0:n2}");
            xrLabel22.DataBindings.Add("Text", null, "TOTAL", "{0:n2}");
        }

        private void Detail1_AfterPrint(object sender, EventArgs e)
        {
            try
            {
                banco1 = banco1 + Convert.ToDecimal(xrLabel21.Text);
                banco2 = banco2 + Convert.ToDecimal(xrLabel20.Text);
                banco3 = banco3 + Convert.ToDecimal(xrLabel19.Text);
                banco4 = banco4 + Convert.ToDecimal(xrLabel18.Text);
                total = total + Convert.ToDecimal(xrLabel22.Text);

            }
            catch (Exception)
            {
                banco1 = 0;
                banco2 = 0;
                banco3 = 0;
                banco4 = 0;
                total = 0;
            }
        }

        private void ReportFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
            string sql = @"SELECT * FROM (
	SELECT ISNULL(SUM(VALOR), 0) '001-01'
	FROM FEXTRATO
	WHERE 
	 CODCONTA = '001-01'
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (0, 5)
	and FEXTRATO.CODNATUREZAORCAMENTO is null
	) CONTA1, 
	(
	SELECT ISNULL(SUM(VALOR), 0) '237-01'
	FROM FEXTRATO
	WHERE 
	 CODCONTA = '237-01'
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (0, 5)
	and FEXTRATO.CODNATUREZAORCAMENTO is null
	) CONTA2,
	(
	SELECT ISNULL(SUM(VALOR), 0) '341-01'
	FROM FEXTRATO
	WHERE 
	 CODCONTA = '341-01'
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (0, 5)
	and FEXTRATO.CODNATUREZAORCAMENTO is null
	) CONTA3,
	(
	SELECT ISNULL(SUM(VALOR), 0) '999-01'
	FROM FEXTRATO
	WHERE 
	 CODCONTA = '999-01'
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (0, 5)
	and FEXTRATO.CODNATUREZAORCAMENTO is null
	) CONTA4,
    (
	SELECT ISNULL(SUM(VALOR), 0) 'TOTAL'
	FROM FEXTRATO
	WHERE 
	convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (0, 5)
	and FEXTRATO.CODNATUREZAORCAMENTO is null
	) TOTAL";
            DateTime dataIni = Convert.ToDateTime(Parametros[4].Valor);
            DateTime dataFin = Convert.ToDateTime(Parametros[5].Valor);

            string empresa, filial;
            empresa = Parametros[2].Valor.ToString();
            filial = Parametros[3].Valor.ToString();
            System.Data.DataTable dt = dbs.QuerySelect(sql, new object[] { dataIni, dataFin, filial, dataIni, dataFin, filial, dataIni, dataFin, filial, dataIni, dataFin, filial, dataIni, dataFin, filial });
            DetailReport1.DataSource = dt;
            xrLabel3.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["001-01"].ToString()));
            xrLabel4.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["237-01"].ToString()));
            xrLabel5.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["341-01"].ToString()));
            xrLabel24.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["999-01"].ToString()));
            xrLabel2.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["TOTAL"].ToString()));

            xrLabel27.Text = string.Format("{0:n2}", banco1 + Convert.ToDecimal(xrLabel3.Text));
            xrLabel28.Text = string.Format("{0:n2}", banco2 + Convert.ToDecimal(xrLabel4.Text));
            xrLabel29.Text = string.Format("{0:n2}", banco3 + Convert.ToDecimal(xrLabel5.Text));
            xrLabel30.Text = string.Format("{0:n2}", banco4 + Convert.ToDecimal(xrLabel24.Text));
            xrLabel31.Text = string.Format("{0:n2}", total + Convert.ToDecimal(xrLabel2.Text));

            banco1 = 0;
            banco2 = 0;
            banco3 = 0;
            banco4 = 0;
            total = 0;
        }

        private void DetailReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sql = @"SELECT CODNATUREZA, DESCRICAO, CONTA1, CONTA2, CONTA3, CONTA4, TOTAL
FROM
(
	SELECT CODNATUREZA, DESCRICAO, CODEMPRESA, 
	ISNULL((
	SELECT SUM(VALOR) 
	FROM FEXTRATO
	WHERE FEXTRATO.CODEMPRESA = VNATUREZAORCAMENTO.CODEMPRESA
	AND FEXTRATO.CODNATUREZAORCAMENTO = VNATUREZAORCAMENTO.CODNATUREZA
	AND CODCONTA = '001-01'
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (1, 2, 4)
	),0) CONTA1,
	
	ISNULL((
	SELECT SUM(VALOR) 
	FROM FEXTRATO
	WHERE FEXTRATO.CODEMPRESA = VNATUREZAORCAMENTO.CODEMPRESA
	AND FEXTRATO.CODNATUREZAORCAMENTO = VNATUREZAORCAMENTO.CODNATUREZA
	AND CODCONTA = '237-01'
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (1, 2, 4)
	),0)  CONTA2,

	ISNULL((
	SELECT SUM(VALOR) 
	FROM FEXTRATO
	WHERE FEXTRATO.CODEMPRESA = VNATUREZAORCAMENTO.CODEMPRESA
	AND FEXTRATO.CODNATUREZAORCAMENTO = VNATUREZAORCAMENTO.CODNATUREZA
	AND CODCONTA = '341-01'
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (1, 2, 4)
	),0)  CONTA3,	
	
	ISNULL((
	SELECT SUM(VALOR) 
	FROM FEXTRATO
	WHERE FEXTRATO.CODEMPRESA = VNATUREZAORCAMENTO.CODEMPRESA
	AND FEXTRATO.CODNATUREZAORCAMENTO = VNATUREZAORCAMENTO.CODNATUREZA
	AND CODCONTA = '999-01'
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (1, 2, 4)
	
	),0)  CONTA4,
	
	ISNULL((
	SELECT SUM(VALOR) 
	FROM FEXTRATO
	WHERE FEXTRATO.CODEMPRESA = VNATUREZAORCAMENTO.CODEMPRESA
	AND FEXTRATO.CODNATUREZAORCAMENTO = VNATUREZAORCAMENTO.CODNATUREZA
	AND CODCONTA IN( '001-01','237-01', '341-01', '999-01' )
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (1, 2, 4)
	
	),0)  TOTAL
	
FROM VNATUREZAORCAMENTO
)X
WHERE 
(
CONTA1 > 0 OR
CONTA2 > 0 OR
CONTA3 > 0 OR
CONTA4 > 0 OR
TOTAL > 0
AND CODEMPRESA = ?
) 
";
            DateTime dataIni = Convert.ToDateTime(Parametros[4].Valor);
            DateTime dataFin = Convert.ToDateTime(Parametros[5].Valor);

            string empresa, filial;
            empresa = Parametros[2].Valor.ToString();
            filial = Parametros[3].Valor.ToString();

            sql = new parametro().tratamentoQuery(sql, Parametros);

            System.Data.DataTable dt = dbs.QuerySelect(sql, new object[] { dataIni, dataFin, filial, dataIni, dataFin, filial, dataIni, dataFin, filial, dataIni, dataFin, filial, dataIni, dataFin, filial, empresa });
            DetailReport1.DataSource = dt;
            xrLabel41.DataBindings.Add("Text", null, "CODNATUREZA");
            xrLabel40.DataBindings.Add("Text", null, "DESCRICAO");
            xrLabel34.DataBindings.Add("Text", null, "CONTA1", "{0:n2}");
            xrLabel35.DataBindings.Add("Text", null, "CONTA2", "{0:n2}");
            xrLabel36.DataBindings.Add("Text", null, "CONTA3", "{0:n2}");
            xrLabel39.DataBindings.Add("Text", null, "CONTA4", "{0:n2}");
            xrLabel33.DataBindings.Add("Text", null, "TOTAL", "{0:n2}");

        }

        private void Detail2_AfterPrint(object sender, EventArgs e)
        {
           

            try
            {
                banco1 = banco1 + Convert.ToDecimal(xrLabel34.Text);
                banco2 = banco2 + Convert.ToDecimal(xrLabel35.Text);
                banco3 = banco3 + Convert.ToDecimal(xrLabel36.Text);
                banco4 = banco4 + Convert.ToDecimal(xrLabel39.Text);
                total = total + Convert.ToDecimal(xrLabel33.Text);

            }
            catch (Exception)
            {
                banco1 = 0;
                banco2 = 0;
                banco3 = 0;
                banco4 = 0;
                total = 0;
            }
        }

        private void ReportFooter2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sql = @"SELECT * FROM (
	SELECT ISNULL(SUM(VALOR), 0) '001-01'
	FROM FEXTRATO
	WHERE 
	CODCONTA = '001-01'
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (1, 2, 4)
	AND FEXTRATO.CODNATUREZAORCAMENTO is null
	) CONTA1, 
	(
	SELECT ISNULL(SUM(VALOR), 0) '237-01'
	FROM FEXTRATO
	WHERE 
	 CODCONTA = '237-01'
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (1, 2, 4)
	AND FEXTRATO.CODNATUREZAORCAMENTO is null
	) CONTA2,
	(
	SELECT ISNULL(SUM(VALOR), 0) '341-01'
	FROM FEXTRATO
	WHERE 
	CODCONTA = '341-01'
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
	AND TIPO IN (1, 2, 4)
	AND FEXTRATO.CODNATUREZAORCAMENTO is null
	) CONTA3,
	(
	SELECT ISNULL(SUM(VALOR), 0) '999-01'
	FROM FEXTRATO
	WHERE 
	CODCONTA = '999-01'
	AND convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
    AND TIPO IN (1, 2, 4)
	AND FEXTRATO.CODNATUREZAORCAMENTO is null
	) CONTA4,
    (
	SELECT ISNULL(SUM(VALOR), 0) 'TOTAL'
	FROM FEXTRATO
	WHERE 
	convert(date, DATACOMPENSACAO) >= ?
	AND convert(date, DATACOMPENSACAO) <= ?
    AND CODFILIAL = ?
    AND TIPO IN (1, 2, 4)
	AND FEXTRATO.CODNATUREZAORCAMENTO is null
	) TOTAL";
            DateTime dataIni = Convert.ToDateTime(Parametros[4].Valor);
            DateTime dataFin = Convert.ToDateTime(Parametros[5].Valor);

            string empresa, filial;
            empresa = Parametros[2].Valor.ToString();
            filial = Parametros[3].Valor.ToString();

            System.Data.DataTable dt = dbs.QuerySelect(sql, new object[] { dataIni, dataFin, filial, dataIni, dataFin, filial, dataIni, dataFin, filial, dataIni, dataFin, filial, dataIni, dataFin, filial });
            DetailReport1.DataSource = dt;
            xrLabel61.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["001-01"].ToString()));
            xrLabel60.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["237-01"].ToString()));
            xrLabel50.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["341-01"].ToString()));
            xrLabel48.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["999-01"].ToString()));
            xrLabel65.Text = string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["TOTAL"].ToString()));


            xrLabel46.Text = string.Format("{0:n2}", banco1 + Convert.ToDecimal(xrLabel61.Text));
            xrLabel45.Text = string.Format("{0:n2}", banco2 + Convert.ToDecimal(xrLabel60.Text));
            xrLabel44.Text = string.Format("{0:n2}", banco3 + Convert.ToDecimal(xrLabel50.Text));
            xrLabel43.Text = string.Format("{0:n2}", banco4 + Convert.ToDecimal(xrLabel48.Text));
            xrLabel42.Text = string.Format("{0:n2}", total + Convert.ToDecimal(xrLabel65.Text));


        }

        private void Detail2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel57.Text = xrLabel31.Text;
            xrLabel56.Text = xrLabel42.Text;
            xrLabel55.Text = string.Format("{0:n2}", Convert.ToDecimal(xrLabel31.Text) - Convert.ToDecimal(xrLabel56.Text));
            xrLabel58.Text = string.Format("{0:n2}", (Convert.ToDecimal(xrLabel16.Text) + Convert.ToDecimal(xrLabel31.Text)) - Convert.ToDecimal(xrLabel42.Text));
        }
    

        private void Detail3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void SubBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }
    }
}
