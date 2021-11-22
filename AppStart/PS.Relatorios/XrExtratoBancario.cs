using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorios
{
    public partial class XrExtratoBancario : DevExpress.XtraReports.UI.XtraReport
    {
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private List<PS.Lib.DataField> Parametros { get; set; }
        decimal saldoBase = 0;
        decimal TOTALENTRADASGRUPO = 0, TOTALSAIDASGRUPO = 0, TOTALGRUPO = 0, SALDOGRUPO = 0, TOTALENTRADAS = 0, TOTALSAIDAS = 0;
        public XrExtratoBancario(List<PS.Lib.DataField> Params)
        {
            InitializeComponent();
            this.Parametros = Params;
        }
        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sql = @"SELECT 
FEXTRATO.IDEXTRATO,
FEXTRATO.DATACOMPENSACAO, 
FEXTRATO.DATA,
FEXTRATO.NUMERODOCUMENTO, 
UPPER(FEXTRATO.HISTORICO) + COALESCE(' - ' + VCLIFOR.NOMEFANTASIA,'') HISTORICO,
CASE WHEN FEXTRATO.TIPO = 0 OR FEXTRATO.TIPO = 5 OR FEXTRATO.TIPO = 6 THEN FEXTRATO.VALOR END ENTRADAS,
CASE WHEN FEXTRATO.TIPO = 1 OR FEXTRATO.TIPO = 2 OR FEXTRATO.TIPO = 4 THEN FEXTRATO.VALOR END SAIDAS,
FEXTRATO.VALOR, 
FEXTRATO.TIPO
FROM 
FEXTRATO 
INNER JOIN FCONTA ON FEXTRATO.CODEMPRESA = FCONTA.CODEMPRESA AND FEXTRATO.CODCONTA = FCONTA.CODCONTA
LEFT JOIN FLANCA ON FEXTRATO.CODEMPRESA = FLANCA.CODEMPRESA AND FEXTRATO.IDEXTRATO = FLANCA.IDEXTRATO
LEFT JOIN VCLIFOR ON FLANCA.CODCLIFOR = VCLIFOR.CODCLIFOR AND FLANCA.CODEMPRESA = VCLIFOR.CODEMPRESA
WHERE 
FEXTRATO.CODCONTA = ?
AND convert(date,  FEXTRATO.DATACOMPENSACAO) >= ?
AND convert(date, FEXTRATO.DATACOMPENSACAO) <= ?
AND convert(date, FEXTRATO.DATACOMPENSACAO) > convert(date, FCONTA.DTBASE)
AND FEXTRATO.COMPENSADO = 1
AND FEXTRATO.CODEMPRESA = ?
AND FEXTRATO.CODFILIAL = ?
AND FEXTRATO.TIPO NOT IN (4, 5)

UNION

SELECT 
FEXTRATO.IDEXTRATO,
FEXTRATO.DATACOMPENSACAO, 
FEXTRATO.DATA,
FEXTRATO.NUMERODOCUMENTO, 
UPPER(FEXTRATO.HISTORICO) HISTORICO, 
CASE WHEN FEXTRATO.TIPO = 0 OR FEXTRATO.TIPO = 5 OR FEXTRATO.TIPO = 6 THEN FEXTRATO.VALOR END ENTRADAS,
CASE WHEN FEXTRATO.TIPO = 1 OR FEXTRATO.TIPO = 2 OR FEXTRATO.TIPO = 4 THEN FEXTRATO.VALOR END SAIDAS,
FEXTRATO.VALOR, 
FEXTRATO.TIPO
FROM 
FEXTRATO 
INNER JOIN FCONTA ON FEXTRATO.CODEMPRESA = FCONTA.CODEMPRESA AND FEXTRATO.CODCONTA = FCONTA.CODCONTA
WHERE 
FEXTRATO.CODCONTA = ?
AND convert(date,  FEXTRATO.DATACOMPENSACAO) >= ?
AND convert(date, FEXTRATO.DATACOMPENSACAO) <= ?
AND convert(date, FEXTRATO.DATACOMPENSACAO) > convert(date, FCONTA.DTBASE)
AND FEXTRATO.COMPENSADO = 1
AND FEXTRATO.CODEMPRESA = ?
AND FEXTRATO.CODFILIAL = ?
AND FEXTRATO.TIPO IN (4, 5)
ORDER BY FEXTRATO.DATACOMPENSACAO
";
            sql = new parametro().tratamentoQuery(sql, Parametros);
            System.Data.DataTable dt = dbs.QuerySelect(sql, new object[] { Parametros[6].Valor, Convert.ToDateTime(Parametros[4].Valor), Convert.ToDateTime(Parametros[5].Valor), Parametros[2].Valor, Parametros[3].Valor, Parametros[6].Valor, Convert.ToDateTime(Parametros[4].Valor), Convert.ToDateTime(Parametros[5].Valor), Parametros[2].Valor, Parametros[3].Valor });
            string a = AppLib.Context.poolConnection.Get("Start").ParseCommand(sql, new object[] { Parametros[6].Valor, Convert.ToDateTime(Parametros[4].Valor), Convert.ToDateTime(Parametros[5].Valor), Parametros[2].Valor, Parametros[3].Valor, Parametros[6].Valor, Convert.ToDateTime(Parametros[4].Valor), Convert.ToDateTime(Parametros[5].Valor), Parametros[2].Valor, Parametros[3].Valor });
            System.Collections.ArrayList lista = new System.Collections.ArrayList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                extratoBancario extrato = new extratoBancario();
                extrato.IDEXTRATO = Convert.ToInt32(dt.Rows[i]["IDEXTRATO"]);
                extrato.DATACOMPENSACAO = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["DATACOMPENSACAO"]).ToShortDateString());
                extrato.DATA = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["DATA"]).ToShortDateString());
                extrato.NUMERODOCUMENTO = dt.Rows[i]["NUMERODOCUMENTO"].ToString();
                extrato.HISTORICO = dt.Rows[i]["HISTORICO"].ToString();

                if (!string.IsNullOrEmpty(dt.Rows[i]["ENTRADAS"].ToString()))
                {
                    extrato.ENTRADAS = Convert.ToDecimal(dt.Rows[i]["ENTRADAS"]);
                }
                else
                {
                    extrato.ENTRADAS = 0;
                }
                if (!string.IsNullOrEmpty(dt.Rows[i]["SAIDAS"].ToString()))
                {
                    extrato.SAIDAS = Convert.ToDecimal(dt.Rows[i]["SAIDAS"]) * -1;
                }
                else
                {
                    extrato.SAIDAS = 0;
                }

                if (!extrato.ENTRADAS.Equals(0))
                {
                    extrato.SALDO = saldoBase + extrato.ENTRADAS;
                    saldoBase = extrato.SALDO;
                    TOTALENTRADAS = TOTALENTRADAS + extrato.ENTRADAS;
                }
                if (!extrato.SAIDAS.Equals(0))
                {
                    extrato.SALDO = saldoBase + extrato.SAIDAS;
                    saldoBase = extrato.SALDO;
                    TOTALSAIDAS = TOTALSAIDAS + extrato.SAIDAS;
                }
                lista.Add(extrato);
            }
            this.DetailReport.DataSource = lista;
            xrLabel15.DataBindings.Add("Text", null, "IDEXTRATO");
            xrLabel16.DataBindings.Add("Text", null, "DATA", "{0: dd/MM/yyyy}");
            xrLabel17.DataBindings.Add("Text", null, "DATACOMPENSACAO", "{0: dd/MM/yyyy}");
            xrLabel18.DataBindings.Add("Text", null, "NUMERODOCUMENTO");
            xrLabel19.DataBindings.Add("Text", null, "HISTORICO");
            xrLabel20.DataBindings.Add("Text", null, "ENTRADAS", "{0:n2}");
            xrLabel21.DataBindings.Add("Text", null, "SAIDAS", "{0:n2}");
            xrLabel22.DataBindings.Add("Text", null, "SALDO", "{0:n2}");
            //Criando o grupo

            GroupHeader1.GroupFields.Add(new GroupField("DATACOMPENSACAO"));

        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel23.Text = string.Format("{0:n2}", TOTALENTRADAS);
            xrLabel28.Text = string.Format("{0:n2}", TOTALSAIDAS);
            xrLabel29.Text = string.Format("{0:n2}", TOTALENTRADAS + TOTALSAIDAS);
            xrLabel36.Text = string.Format("{0:n2}", Convert.ToDecimal(xrLabel31.Text));
            xrLabel48.Text = xrLabel14.Text;
            //Formatação Condicional
            if (Convert.ToDecimal(xrLabel36.Text) > 0)
            {
                xrLabel36.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                xrLabel36.ForeColor = System.Drawing.Color.Red;
            }
            if (Convert.ToDecimal(xrLabel29.Text) > 0)
            {
                xrLabel29.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                xrLabel29.ForeColor = System.Drawing.Color.Red;
            }
            if (Convert.ToDecimal(xrLabel48.Text) > 0)
            {
                xrLabel48.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                xrLabel48.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel44.Text = Parametros[6].Valor.ToString();
            xrLabel43.Text = string.Format("{0} a {1}", Convert.ToDateTime(Parametros[4].Valor.ToString()).ToShortDateString(), Convert.ToDateTime(Parametros[5].Valor.ToString()).ToShortDateString());
            xrLabel45.Text = Parametros[2].Valor.ToString() + " / " + Parametros[3].Valor.ToString();
            //Busca o Saldo
            string sql = @"SELECT ISNULL(SUM(TOTAL),0) TOTAL
FROM (
	SELECT CODEMPRESA, CODCONTA, DESCRICAO, SALDO, MOVIMENTO, (MOVIMENTO + SALDO) TOTAL
	FROM (
		SELECT
		FCONTA.CODEMPRESA,
		FCONTA.CODCONTA,
		FCONTA.DESCRICAO,
		ISNULL((FCONTA.SALDODATABASE),0) SALDO,
		ISNULL(
				(SELECT SUM(VALOR)
					FROM
					(
					SELECT
					CASE 
					WHEN FEXTRATO.TIPO IN (1, 2, 4)THEN SUM(FEXTRATO.VALOR * -1)
					ELSE SUM(FEXTRATO.VALOR)
					END VALOR
					FROM FEXTRATO
					WHERE FEXTRATO.COMPENSADO = 1
					AND convert(date, FEXTRATO.DATACOMPENSACAO) < ?
					AND FEXTRATO.CODCONTA = ?
                    AND FEXTRATO.CODFILIAL = ?
                    GROUP BY FEXTRATO.TIPO
					)X
		  ),0
				) MOVIMENTO
		FROM FCONTA
		WHERE FCONTA.CODEMPRESA = ?
		  AND FCONTA.CODCONTA = ?
	) XX
	GROUP BY CODEMPRESA, CODCONTA, DESCRICAO, SALDO, MOVIMENTO
) XXX
";
            decimal aa = Convert.ToDecimal(dbs.QueryValue(0, sql, new object[] { Convert.ToDateTime(Parametros[4].Valor), this.Parametros[6].Valor.ToString(), Parametros[3].Valor.ToString(), Parametros[2].Valor.ToString(), this.Parametros[6].Valor.ToString() }));
            xrLabel14.Text = string.Format("{0:n2}", aa);

            //xrLabel14.Text = string.Format("{0:n2}", Convert.ToDecimal(dbs.QueryValue(0, sql, new object[] { Convert.ToDateTime(Parametros[4].Valor), this.Parametros[6].Valor.ToString(), Parametros[3].Valor.ToString(), Parametros[2].Valor.ToString(), this.Parametros[6].Valor.ToString() }).ToString()));
            saldoBase = Convert.ToDecimal(xrLabel14.Text);
            SALDOGRUPO = saldoBase;
            //Formatação Condicional
            if (saldoBase > 0)
            {
                xrLabel14.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                xrLabel14.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            try
            {
                SALDOGRUPO = SALDOGRUPO + TOTALGRUPO;
            }
            catch (Exception)
            {
                SALDOGRUPO = 0;
            }
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel33.Text = string.Format("{0:n}", (TOTALENTRADASGRUPO));
            xrLabel34.Text = string.Format("{0:n}", (TOTALSAIDASGRUPO));
            xrLabel54.Text = string.Format("{0:n}", TOTALGRUPO - SALDOGRUPO);
            SALDOGRUPO = TOTALGRUPO;
            xrLabel31.Text = string.Format("{0:n}", SALDOGRUPO);
            //Zerando a soma
            TOTALENTRADASGRUPO = 0;
            TOTALSAIDASGRUPO = 0;
            TOTALGRUPO = 0;
            //Formatação Condicional
            if (Convert.ToDecimal(xrLabel54.Text) > 0)
            {
                xrLabel54.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                xrLabel54.ForeColor = System.Drawing.Color.Red;
            }
            if (Convert.ToDecimal(xrLabel31.Text) > 0)
            {
                xrLabel31.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                xrLabel31.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void Detail1_AfterPrint(object sender, EventArgs e)
        {
            try
            {
                TOTALENTRADASGRUPO = TOTALENTRADASGRUPO + Convert.ToDecimal(xrLabel20.Text);
                TOTALSAIDASGRUPO = TOTALSAIDASGRUPO + Convert.ToDecimal(xrLabel21.Text);
                TOTALGRUPO = Convert.ToDecimal(xrLabel22.Text);
            }
            catch (Exception)
            {
                TOTALENTRADASGRUPO = 0;
                TOTALSAIDASGRUPO = 0;
                TOTALGRUPO = 0;
            }
        }

    }
}
