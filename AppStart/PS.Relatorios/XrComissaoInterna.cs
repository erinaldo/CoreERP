using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Relatorios
{
    public partial class XrComissaoInterna : DevExpress.XtraReports.UI.XtraReport
    {
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private List<PS.Lib.DataField> Parametros { get; set; }
        string sql = string.Empty;
        private decimal totalBaixado = 0, totOutros = 0, totDesc = 0;

        public XrComissaoInterna(List<PS.Lib.DataField> Params)
        {
            InitializeComponent();
            this.Parametros = Params;
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            sql = @"SELECT IMAGEM FROM GIMAGEM INNER JOIN GEMPRESA ON GIMAGEM.CODIMAGEM = GEMPRESA.CODIMAGEM WHERE CODEMPRESA = ?";
            byte[] arrayimagem = (byte[])dbs.QueryValue(null, sql, AppLib.Context.Empresa);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(arrayimagem);
            logo.Image = Image.FromStream(ms);
            xrLabel26.Text = string.Format("Período: {0:dd/MM/yyyy} a {1:dd/MM/yyyy}", Convert.ToDateTime(Parametros[4].Valor), Convert.ToDateTime(Parametros[5].Valor));

            // João Pedro 13/11/2017

            sql = @"SELECT GEMPRESA.NOME,
(COALESCE(GTIPORUA.NOME, '') + 
COALESCE(' ' + GEMPRESA.RUA, '') + 
COALESCE( ',' + GEMPRESA.NUMERO, '') + 
COALESCE(' - ' + GEMPRESA.COMPLEMENTO, '') + 
COALESCE(' - ' + GTIPOBAIRRO.NOME, '') + 
COALESCE(' ' +  GEMPRESA.BAIRRO, '') + 
COALESCE(' - ' + GCIDADE.NOME, '') + 
COALESCE( ' - ' + GCIDADE.CODETD, '') + 
COALESCE (' - ' + GEMPRESA.CEP, '') ) AS RUA,
CGCCPF,
INSCRICAOESTADUAL,
TELEFONE,
EMAIL
FROM 
GEMPRESA 
LEFT JOIN GCIDADE ON GEMPRESA.CODCIDADE = GCIDADE.CODCIDADE
INNER JOIN GTIPOBAIRRO ON GEMPRESA.CODTIPOBAIRRO = GTIPOBAIRRO.CODTIPOBAIRRO
INNER JOIN GTIPORUA ON GEMPRESA.CODTIPORUA = GTIPORUA.CODTIPORUA
WHERE
GEMPRESA.CODEMPRESA = ?";
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] {Parametros[0].Valor});

            if (dt.Rows.Count > 0)
            {
                xrLabel2.Text = dt.Rows[0]["RUA"].ToString().ToUpper();
                xrLabel3.Text = "CNPJ: " + dt.Rows[0]["CGCCPF"].ToString().ToUpper();
                xrLabel24.Text = "INSCRIÇÃO ESTADUAL: " + dt.Rows[0]["INSCRICAOESTADUAL"].ToString().ToUpper();
                xrLabel4.Text = "TELEFONE: " + dt.Rows[0]["TELEFONE"].ToString().ToUpper();
                xrLabel38.Text = dt.Rows[0]["EMAIL"].ToString();
            }
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            sql = @"SELECT 
FLANCA.DATAEMISSAO, 
FLANCA.DATABAIXA,
FLANCA.NUMERO, 
ISNULL(GOPER.VALORLIQUIDO,0) VALORLIQUIDO,  
ISNULL((GOPER.VALORFRETE + GOPER.VALORDESPESA + GOPER.VALORSEGURO), 0) VLSEG,
FLANCA.VLDESCONTO, 
ISNULL((FLANCA.VLORIGINAL + (GOPER.VALORFRETE + GOPER.VALORDESPESA + GOPER.VALORSEGURO)), 0) VLORIGINAL, 
FLANCA.VLLIQUIDO, 
(FLANCA.VLJUROS + FLANCA.VLMULTA) VLJUROS, 
FLANCA.VLDESCONTO, 
FLANCA.VLBAIXADO
FROM FLANCA 
LEFT OUTER JOIN GOPER ON FLANCA.CODOPER = GOPER.CODOPER AND FLANCA.CODEMPRESA = GOPER.CODEMPRESA
WHERE convert(date, DATABAIXA) >= ? AND convert(date, DATABAIXA) <= ? AND FLANCA.CODEMPRESA = ? AND FLANCA.CODSTATUS = 1 AND TIPOPAGREC = 1";
            sql = new parametro().tratamentoQuery(sql, Parametros);
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { Convert.ToDateTime(Parametros[4].Valor), Convert.ToDateTime(Parametros[5].Valor), Parametros[0].Valor });
            DetailReport.DataSource = dt;

            xrLabel29.DataBindings.Add("Text", null, "DATAEMISSAO", "{0:dd/MM/yyyy}");
            xrLabel15.DataBindings.Add("Text", null, "DATABAIXA", "{0:dd/MM/yyyy}");
            xrLabel16.DataBindings.Add("Text", null, "NUMERO");
            xrLabel17.DataBindings.Add("Text", null, "VALORLIQUIDO", "{0:n2}");
            xrLabel18.DataBindings.Add("Text", null, "VLSEG", "{0:n2}");
            xrLabel19.DataBindings.Add("Text", null, "VLDESCONTO", "{0:n2}");
            xrLabel20.DataBindings.Add("Text", null, "VLORIGINAL", "{0:n2}");
            xrLabel21.DataBindings.Add("Text", null, "VLLIQUIDO", "{0:n2}");
            xrLabel22.DataBindings.Add("Text", null, "VLJUROS", "{0:n2}");
            xrLabel23.DataBindings.Add("Text", null, "VLDESCONTO", "{0:n2}");
            xrLabel25.DataBindings.Add("Text", null, "VLBAIXADO", "{0:n2}");
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel32.Text = string.Format("{0:n2}", totalBaixado);
            xrLabel33.Text = string.Format("{0:n2}", totOutros);
            xrLabel36.Text = string.Format("{0:n2}", totDesc);
            xrLabel40.Text = string.Format("{0:n2}", (totalBaixado - (totOutros - totDesc)));
        }

        private void Detail1_AfterPrint(object sender, EventArgs e)
        {
            totalBaixado = totalBaixado + Convert.ToDecimal(xrLabel25.Text);
            totOutros = totOutros + Convert.ToDecimal(xrLabel18.Text);
            totDesc = totDesc + Convert.ToDecimal(xrLabel23.Text);
        }
    }
}
