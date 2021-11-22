using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorios
{
    public partial class XrFinanceiro : DevExpress.XtraReports.UI.XtraReport
    {
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private List<PS.Lib.DataField> Parametros { get; set; }
        decimal vlOriginal = 0, VlLiquido = 0, TotalOriginal = 0, TotalLiquido = 0;
        private string grupo;

        public XrFinanceiro(List<PS.Lib.DataField> Params, string _grupo)
        {
            InitializeComponent();
            this.Parametros = Params;
            grupo = _grupo;
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string condicao = string.Empty;
            string sql = string.Empty;
            sql = @"SELECT 
	FLANCA.NUMERO, 
	CONVERT(DATETIME, CAST(FLANCA.DATAEMISSAO AS DATE)) DATAEMISSAO,
	VCLIFOR.NOME CLIENTE,
	GCENTROCUSTO.NOME CCUSTO,
	(VNATUREZAORCAMENTO.CODNATUREZA + '-' + VNATUREZAORCAMENTO.DESCRICAO) DESCRICAONATUREZA,
	VNATUREZAORCAMENTO.CODNATUREZA,
    (VNATUREZAORCAMENTO.DESCRICAO + ' - ' + VNATUREZAORCAMENTO.CODNATUREZA ) NATUREZA,
	CONVERT(DATETIME, CAST(FLANCA.DATAVENCIMENTO AS DATE)) DATAVENCIMENTO,
	FLANCA.DATABAIXA,
	FLANCA.VLORIGINAL,
	FLANCA.VLLIQUIDO,
	FLANCA.CODFORMA,
	VFORMAPGTO.NOME,
    CASE TIPOPAGREC WHEN 1 THEN 'R' ELSE 'P' END TIPOPAGREC,
	CASE CODSTATUS WHEN 0 THEN 'A' WHEN 1 THEN 'B' ELSE 'C' END CODSTATUS,
    FLANCA.CODTIPDOC, 
	CONVERT(DECIMAL(15,2),FLANCA.VLDESCONTO) AS 'VLDESCONTO',
	CONVERT(DECIMAL(15,2),(FLANCA.VLJUROS + FLANCA.VLMULTA)) AS 'VLACRESCIMOS'
FROM 
	FLANCA 
	INNER JOIN VCLIFOR ON FLANCA.CODEMPRESA = VCLIFOR.CODEMPRESA AND FLANCA.CODCLIFOR = VCLIFOR.CODCLIFOR
	INNER JOIN GCENTROCUSTO ON FLANCA.CODEMPRESA = GCENTROCUSTO.CODEMPRESA AND FLANCA.CODCCUSTO = GCENTROCUSTO.CODCCUSTO
	INNER JOIN VNATUREZAORCAMENTO ON FLANCA.CODEMPRESA = VNATUREZAORCAMENTO.CODEMPRESA AND FLANCA.CODNATUREZAORCAMENTO = VNATUREZAORCAMENTO.CODNATUREZA
	INNER JOIN FTIPDOC ON FLANCA.CODEMPRESA = FTIPDOC.CODEMPRESA AND FLANCA.CODTIPDOC = FTIPDOC.CODTIPDOC
	LEFT JOIN VFORMAPGTO ON FLANCA.CODEMPRESA = VFORMAPGTO.CODEMPRESA AND FLANCA.CODFORMA = VFORMAPGTO.CODFORMA
WHERE 
	FLANCA.CODSTATUS = ?
AND FLANCA.CODEMPRESA = ?
AND FLANCA.CODFILIAL = ?
AND FLANCA.TIPOPAGREC = ?
AND dataInicial >= ?
AND dataFinal <= ?
AND (FLANCA.NFOUDUP <> '0' OR FLANCA.NFOUDUP IS NULL)
AND FTIPDOC.CLASSIFICACAO <> 3
";

            sql = new parametro().tratamentoQuery(sql, Parametros);
            System.Data.DataTable dt = new System.Data.DataTable();
            //Verifica o Tipo
            if (Parametros[4].Valor.Equals("RECEBER"))
            {
                Parametros[4].Valor = "1";
            }
            else if (Parametros[4].Valor.Equals("PAGAR"))
            {
                Parametros[4].Valor = "0";
            }
            else
            {
                Parametros[4].Valor = string.Empty;
                sql = sql.Replace("\r\nAND FLANCA.TIPOPAGREC = ?", "");
               
            }
            //Verifica o Status
            if (Parametros[5].Valor.Equals("ABERTO"))
            {
                Parametros[5].Valor = "0";
            }
            else if (Parametros[5].Valor.Equals("BAIXADO"))
            {
                Parametros[5].Valor = "1";
            }
            else if (Parametros[5].Valor.Equals("CANCELADO"))
            {
                Parametros[5].Valor = "2";
            }
            else if (Parametros[5].Valor.Equals("TODOS"))
            {
                Parametros[5].Valor = "2";
                sql = sql.Replace("\r\n\tFLANCA.CODSTATUS = ?\r\nAND", "\r\n\tFLANCA.CODSTATUS <> ?\r\nAND");
            }
            else
            {
                Parametros[5].Valor = string.Empty;
                sql = sql.Replace("\r\n\tFLANCA.CODSTATUS = ?\r\nAND", "");
            }


            //Verfica o tipo de data que se deve realizar a pesquisa.
            if (Parametros[8].Valor.Equals("EMISSÃO"))
            {
                sql = sql.Replace("dataInicial >= ?", "CONVERT(DATE, FLANCA.DATAEMISSAO) >= ?");
                sql = sql.Replace("dataFinal <= ?", "CONVERT(DATE, FLANCA.DATAEMISSAO) <= ?");
            }
            else if (Parametros[8].Valor.Equals("PREV. BAIXA"))
            {
                sql = sql.Replace("dataInicial >= ?", "CONVERT(DATE, FLANCA.DATAPREVBAIXA) >= ?");
                sql = sql.Replace("dataFinal <= ?", "CONVERT(DATE, FLANCA.DATAPREVBAIXA) <= ?");
            }
            else if (Parametros[8].Valor.Equals("BAIXA"))
            {
                sql = sql.Replace("dataInicial >= ?", "CONVERT(DATE, FLANCA.DATABAIXA) >= ?");
                sql = sql.Replace("dataFinal <= ?", "CONVERT(DATE, FLANCA.DATABAIXA) <= ?");
            }
            else
            {
                sql = sql.Replace("dataInicial >= ?", "CONVERT(DATE, FLANCA.DATAVENCIMENTO) >= ?");
                sql = sql.Replace("dataFinal <= ?", "CONVERT(DATE, FLANCA.DATAVENCIMENTO) <= ?");
            }

            // Se os dois campos não estiverm vazios
            if (!string.IsNullOrEmpty(Parametros[4].Valor.ToString()) && !string.IsNullOrEmpty(Parametros[5].Valor.ToString()))
            {
                dt = dbs.QuerySelect(sql, new object[] { Parametros[5].Valor, Parametros[2].Valor, Parametros[3].Valor, Parametros[4].Valor, Convert.ToDateTime(Parametros[6].Valor), Convert.ToDateTime(Parametros[7].Valor)});
            }
            //Se o status = vazio
            else if (!string.IsNullOrEmpty(Parametros[4].Valor.ToString()) && string.IsNullOrEmpty(Parametros[5].Valor.ToString()))
            {
                dt = dbs.QuerySelect(sql, new object[] { Parametros[2].Valor, Parametros[3].Valor, Parametros[4].Valor, Convert.ToDateTime(Parametros[6].Valor), Convert.ToDateTime(Parametros[7].Valor)});
            }
            //Se o tipo = vazio
            else if (string.IsNullOrEmpty(Parametros[4].Valor.ToString()) && !string.IsNullOrEmpty(Parametros[5].Valor.ToString()))
            {
               dt = dbs.QuerySelect(sql, new object[] {Parametros[5].Valor, Parametros[2].Valor, Parametros[3].Valor,  Convert.ToDateTime(Parametros[6].Valor), Convert.ToDateTime(Parametros[7].Valor)});
            }
            //Se os dois estiverem vazios
            else
            {
                dt = dbs.QuerySelect(sql, new object[] { Parametros[2].Valor, Parametros[3].Valor, Convert.ToDateTime(Parametros[6].Valor), Convert.ToDateTime(Parametros[7].Valor)});
            }

            DetailReport.DataSource = dt;

            lblNumeroDoc.DataBindings.Add("Text", null, "NUMERO");
            lblEmissao.DataBindings.Add("Text", null, "DATAEMISSAO", "{0:dd/MM/yyyy}");
            lblNome.DataBindings.Add("Text", null, "CLIENTE");
            lblCentroCusto.DataBindings.Add("Text", null, "CCUSTO");
            lblNatureza.DataBindings.Add("Text", null, "DESCRICAONATUREZA");
            lbAcrescimo.DataBindings.Add("Text", null, "VLACRESCIMOS", "{0:n2}");
            lbDesconto.DataBindings.Add("Text", null, "VLDESCONTO", "{0:n2}");
            lblVencto.DataBindings.Add("Text", null, "DATAVENCIMENTO", "{0:dd/MM/yyyy}");
            lblDataBaixa.DataBindings.Add("Text", null, "DATABAIXA", "{0:dd/MM/yyyy}");
            lblVLOriginal.DataBindings.Add("Text", null, "VLORIGINAL", "{0:n2}");
            lblVLLiquido.DataBindings.Add("Text", null, "VLLIQUIDO", "{0:n2}");
            lblStatus.DataBindings.Add("Text", null, "CODSTATUS");
            lblTipo.DataBindings.Add("Text", null, "TIPOPAGREC");
            lblCodTipDoc.DataBindings.Add("Text", null, "CODTIPDOC");
            //Cria grupo

            lblLabelGrupo.Text = getNomeGrupo(grupo);

            if (grupo.Equals("DATAPREVBAIXA") || grupo.Equals("DATAEMISSAO") || grupo.Equals("DATAVENCIMENTO") || grupo.Equals("DATABAIXA"))
            {
                lblDescricaoGrupo.DataBindings.Add("Text", null, grupo, "{0:dd/MM/yyyy}");
            }
            else
            {
                lblDescricaoGrupo.DataBindings.Add("Text", null, grupo);
            }
            GroupHeader1.GroupFields.Add(new GroupField(grupo));
        }
        private string getNomeGrupo(string grupo)
        {
            switch (grupo)
            {
                case "DATAPREVBAIXA":
                    return "DATA PREV. BAIXA";
                case "DATAEMISSAO":
                    return "DATA EMISSÃO";
                case "DATAVENCIMENTO":
                    return "DATA VENCIMENTO";
                case "DATABAIXA":
                    return "DATA BAIXA";
                case "CCUSTO":
                    return "CENTRO DE CUSTO";
                case "NOMEFANTASIA":
                    return "CLIENTE";
                case "NATUREZA":
                    return "NATUREZA";
                case "CODNATUREZA":
                    return "COD NATUREZA";
                case "NOME":
                    return "FORMA DE PAGAMENTO";
                case "DESCRICAONATUREZA":
                    return "NATUREZA";
                default:
                    return string.Empty;
            }
        }
        private void Detail1_AfterPrint(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblVLOriginal.Text))
            {
                lblVLOriginal.Text = "0";
            }
            if (string.IsNullOrEmpty(lblVLLiquido.Text))
            {
                lblVLLiquido.Text = "0";
            }
            vlOriginal = vlOriginal + Convert.ToDecimal(lblVLOriginal.Text);
            VlLiquido = VlLiquido + Convert.ToDecimal(lblVLLiquido.Text);
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            xrLabel18.Text = string.Format("{0:n2}", vlOriginal);
            xrLabel24.Text = string.Format("{0:n2}", VlLiquido);

            TotalOriginal = TotalOriginal + vlOriginal;
            TotalLiquido = TotalLiquido + VlLiquido;

            vlOriginal = 0;
            VlLiquido = 0;

        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel19.Text = string.Format("{0:n2}", TotalOriginal);
            xrLabel25.Text = string.Format("{0:n2}", TotalLiquido);
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel39.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GREPORT WHERE CODREPORT = ?", new object[] { Parametros[1].Valor }).ToString();

            xrLabel43.Text = string.Format("{0:dd/MM/yyyy}  a  {1:dd/MM/yyyy}", Convert.ToDateTime(Parametros[6].Valor), Convert.ToDateTime(Parametros[7].Valor));
            xrLabel8.Text = string.Format("{0:dd/MM/yyyy}", Parametros[8].Valor);
            xrLabel45.Text = Parametros[2].Valor.ToString() + " / " + Parametros[3].Valor.ToString();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

    }
}
