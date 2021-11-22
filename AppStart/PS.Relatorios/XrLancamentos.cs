using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorios
{
    public partial class XrLancamentos : DevExpress.XtraReports.UI.XtraReport
    {
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private List<PS.Lib.DataField> Parametros { get; set; }
        decimal subTotalNominal = 0, subTotalAcrescimos = 0, subTotalDeducoes = 0, subTotalLiquido = 0, subTotalBaixado = 0, totalNominal = 0, totalAcrescimos = 0, totalDeducoes = 0, totalBaixado = 0;
        private string grupo;

        public XrLancamentos(List<PS.Lib.DataField> Params, string _grupo)
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
FLANCA.CODLANCA,
FLANCA.NUMERO, 
FLANCA.CODTIPDOC, 
CONVERT(DATETIME, CAST(FLANCA.DATAEMISSAO AS DATE)) DATAEMISSAO,
CONVERT(DATETIME, CAST(FLANCA.DATAVENCIMENTO AS DATE))DATAVENCIMENTO,
CONVERT(DATETIME, CAST(FLANCA.DATAPREVBAIXA AS DATE))DATAPREVBAIXA,
CONVERT(DATETIME, CAST(FLANCA.DATABAIXA AS DATE))DATABAIXA,
VCLIFOR.CODCLIFOR,
VCLIFOR.NOMEFANTASIA, 
FLANCA.VLORIGINAL,
(FLANCA.VLMULTA + FLANCA.VLJUROS) ACRESCIMO,
FLANCA.VLDESCONTO,
((FLANCA.VLORIGINAL + (FLANCA.VLMULTA + FLANCA.VLJUROS)) - FLANCA.VLDESCONTO) VALORORIGINAL,
CASE TIPOPAGREC WHEN 1 THEN 'R' ELSE 'P' END TIPOPAGREC,
CASE CODSTATUS WHEN 0 THEN 'A' WHEN 1 THEN 'B' ELSE 'C' END CODSTATUS
FROM 
FLANCA, 
VCLIFOR,
FTIPDOC
WHERE 
FLANCA.CODEMPRESA = VCLIFOR.CODEMPRESA
AND FLANCA.CODCLIFOR = VCLIFOR.CODCLIFOR
AND FLANCA.CODEMPRESA = FTIPDOC.CODEMPRESA
AND FLANCA.CODTIPDOC = FTIPDOC.CODTIPDOC
AND FLANCA.CODEMPRESA = ?
AND FLANCA.CODFILIAL = ?
AND FLANCA.TIPOPAGREC = ?
AND FLANCA.CODSTATUS = ?
AND FLANCA.DATAVENCIMENTO >= ?
AND FLANCA.DATAVENCIMENTO <= ?
AND (FLANCA.NFOUDUP IS NULL OR FLANCA.NFOUDUP <> ?) 
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
            else if (Parametros[5].Valor.Equals("TODOS"))
            {
                Parametros[5].Valor = "2";
                sql = sql.Replace("\r\nAND FLANCA.CODSTATUS = ?", "\r\nAND FLANCA.CODSTATUS <> ?");
            }
            else
            {
                Parametros[5].Valor = string.Empty;
                sql = sql.Replace("\r\nAND FLANCA.CODSTATUS = ?", "");
            }
            //Verfica o tipo de data que se deve realizar a pesquisa.
            if (Parametros[8].Valor.Equals("EMISSÃO"))
            {
                sql = sql.Replace("FLANCA.DATAVENCIMENTO >= ?", "CONVERT(DATE, FLANCA.DATAEMISSAO) >= ?");
                sql = sql.Replace("FLANCA.DATAVENCIMENTO <= ?", "CONVERT(DATE, FLANCA.DATAEMISSAO) <= ?");
            }
            else if(Parametros[8].Valor.Equals("PREV. BAIXA"))
            {
                sql = sql.Replace("FLANCA.DATAVENCIMENTO >= ?", "CONVERT(DATE, FLANCA.DATAPREVBAIXA) >= ?");
                sql = sql.Replace("FLANCA.DATAVENCIMENTO <= ?", "CONVERT(DATE, FLANCA.DATAPREVBAIXA) <= ?");
            }
            else if (Parametros[8].Valor.Equals("BAIXA"))
            {
                sql = sql.Replace("FLANCA.DATAVENCIMENTO <= ?", "CONVERT(DATE, FLANCA.DATABAIXA) <= ?");
                sql = sql.Replace("FLANCA.DATAVENCIMENTO >= ?", "CONVERT(DATE, FLANCA.DATABAIXA) >= ?");
            }

            // Se os dois campos não estiverm vazios
            if (!string.IsNullOrEmpty(Parametros[4].Valor.ToString()) && !string.IsNullOrEmpty(Parametros[5].Valor.ToString()))
            {
                dt = dbs.QuerySelect(sql, new object[] { Parametros[2].Valor, Parametros[3].Valor, Parametros[4].Valor, Parametros[5].Valor, Convert.ToDateTime(Parametros[6].Valor), Convert.ToDateTime(Parametros[7].Valor), 1 });
            }
                //Se o tipo = vazio
            else if (!string.IsNullOrEmpty(Parametros[4].Valor.ToString()) && string.IsNullOrEmpty(Parametros[5].Valor.ToString()))
            {
                dt = dbs.QuerySelect(sql, new object[] { Parametros[2].Valor, Parametros[3].Valor, Parametros[4].Valor, Convert.ToDateTime(Parametros[6].Valor), Convert.ToDateTime(Parametros[7].Valor), 1 });
            }
                //Se o status = vazio
            else if (string.IsNullOrEmpty(Parametros[4].Valor.ToString()) && !string.IsNullOrEmpty(Parametros[5].Valor.ToString()))
            {
                dt = dbs.QuerySelect(sql, new object[] { Parametros[2].Valor, Parametros[3].Valor, Parametros[5].Valor, Convert.ToDateTime(Parametros[6].Valor), Convert.ToDateTime(Parametros[7].Valor), 1 });
            }
                //Se os dois estiverem vazios
            else
            {
                dt = dbs.QuerySelect(sql, new object[] { Parametros[2].Valor, Parametros[3].Valor, Convert.ToDateTime(Parametros[6].Valor), Convert.ToDateTime(Parametros[7].Valor), 1 });
            }
          
            
            DetailReport.DataSource = dt;
            //if (Parametros[5].Valor.Equals("0"))
            //{
            //    lblNumeroDoc.DataBindings.Add("Text", null, "NUMERO");
            //    lblTipoDoc.DataBindings.Add("Text", null, "CODTIPDOC");
            //    lblEmissao.DataBindings.Add("Text", null, "DATAEMISSAO", "{0:dd/MM/yyyy}");
            //    lblVencto.DataBindings.Add("Text", null, "DATAVENCIMENTO", "{0:dd/MM/yyyy}");
            //    lblPrevisao.DataBindings.Add("Text", null, "DATAPREVBAIXA", "{0:dd/MM/yyyy}");
            //    lblNome.DataBindings.Add("Text", null, "NOMEFANTASIA");
            //    lblVlNominal.DataBindings.Add("Text", null, "VLORIGINAL", "{0:n2}");
            //    lblAcrescimos.DataBindings.Add("Text", null, "ACRESCIMO", "{0:n2}");
            //    lblDeducoes.DataBindings.Add("Text", null, "VLDESCONTO", "{0:n2}");
            //    lblVlLiquido.DataBindings.Add("Text", null, "VALORORIGINAL", "{0:n2}");

            //    //Cria Grupo
             
            //}
            //else
            //{
                lblNumeroDoc.DataBindings.Add("Text", null, "NUMERO");
                lblTipoDoc.DataBindings.Add("Text", null, "CODTIPDOC");
                lblEmissao.DataBindings.Add("Text", null, "DATAEMISSAO", "{0:dd/MM/yyyy}");
                lblVencto.DataBindings.Add("Text", null, "DATAVENCIMENTO", "{0:dd/MM/yyyy}");
                lblPrevisao.DataBindings.Add("Text", null, "DATAPREVBAIXA", "{0:dd/MM/yyyy}");
                lblDataBaixa.DataBindings.Add("Text", null, "DATABAIXA", "{0:dd/MM/yyyy}");
                lblCodConta.DataBindings.Add("Text", null, "CODCONTA");
                lblNome.DataBindings.Add("Text", null, "NOMEFANTASIA");
                lblVlNominal.DataBindings.Add("Text", null, "VLORIGINAL", "{0:n2}");
                lblAcrescimos.DataBindings.Add("Text", null, "ACRESCIMO", "{0:n2}");
                lblDeducoes.DataBindings.Add("Text", null, "VLDESCONTO", "{0:n2}");
                lblVlLiquido.DataBindings.Add("Text", null, "VALORORIGINAL", "{0:n2}");
                lblVlBaixado.DataBindings.Add("Text", null, "VLBAIXADO", "{0:n2}");
                lblTipo.DataBindings.Add("Text", null, "TIPOPAGREC");
                lblStatus.DataBindings.Add("Text", null, "CODSTATUS");
                //Cria Grupo
                //xrLabel7.DataBindings.Add("Text", null, "DATABAIXA", "{0:dd/MM/yyyy}");
                //GroupHeader1.GroupFields.Add(new GroupField("DATABAIXA"));
            //}

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
                case "CODTIPDOC":
                    return "TIPO DOCUMENTO";
                case "NOMEFANTASIA":
                    return "CLIENTE";
                default:
                    return string.Empty;
            }
        }
        private void Detail1_AfterPrint(object sender, EventArgs e)
        {
            try
            {
                if (Parametros[5].Valor.Equals("1"))
                {
                    subTotalNominal = subTotalNominal + Convert.ToDecimal(lblVlNominal.Text);
                    subTotalAcrescimos = subTotalAcrescimos + Convert.ToDecimal(lblAcrescimos.Text);
                    subTotalDeducoes = subTotalDeducoes + Convert.ToDecimal(lblDeducoes.Text);
                    subTotalLiquido = subTotalLiquido + Convert.ToDecimal(lblVlLiquido.Text);
                    subTotalBaixado = subTotalBaixado + Convert.ToDecimal(lblVlBaixado.Text);
                }
                else
                {
                    subTotalNominal = subTotalNominal + Convert.ToDecimal(lblVlNominal.Text);
                    subTotalAcrescimos = subTotalAcrescimos + Convert.ToDecimal(lblAcrescimos.Text);
                    subTotalDeducoes = subTotalDeducoes + Convert.ToDecimal(lblDeducoes.Text);
                    subTotalLiquido = subTotalLiquido + Convert.ToDecimal(lblVlLiquido.Text);
                }

            }
            catch (Exception)
            {
                subTotalNominal = 0;
                subTotalAcrescimos = 0;
                subTotalDeducoes = 0;
                subTotalLiquido = 0;
                subTotalBaixado = 0;
            }
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Parametros[5].Valor.Equals("1"))
            {
                xrLabel15.Text = string.Format("{0:n}", (subTotalNominal));
                xrLabel16.Text = string.Format("{0:n}", (subTotalAcrescimos));
                xrLabel17.Text = string.Format("{0:n}", (subTotalDeducoes));
                xrLabel18.Text = string.Format("{0:n}", (subTotalLiquido));
                xrLabel24.Text = string.Format("{0:n}", subTotalBaixado);
                totalNominal = totalNominal + subTotalNominal;
                totalAcrescimos = totalAcrescimos + subTotalAcrescimos;
                totalDeducoes = totalDeducoes + subTotalDeducoes;
                totalBaixado = totalBaixado + subTotalBaixado;
            }
            else
            {
                xrLabel15.Text = string.Format("{0:n}", (subTotalNominal));
                xrLabel16.Text = string.Format("{0:n}", (subTotalAcrescimos));
                xrLabel17.Text = string.Format("{0:n}", (subTotalDeducoes));
                xrLabel18.Text = string.Format("{0:n}", (subTotalLiquido));
                totalNominal = totalNominal + subTotalNominal;
                totalAcrescimos = totalAcrescimos + subTotalAcrescimos;
                totalDeducoes = totalDeducoes + subTotalDeducoes;
            }


            subTotalNominal = 0;
            subTotalAcrescimos = 0;
            subTotalDeducoes = 0;
            subTotalLiquido = 0;
            subTotalBaixado = 0;

        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Parametros[5].Valor.Equals("1"))
            {
                xrLabel22.Text = string.Format("{0:n}", (totalNominal));
                xrLabel21.Text = string.Format("{0:n}", (totalAcrescimos));
                xrLabel20.Text = string.Format("{0:n}", (totalDeducoes));
                xrLabel19.Text = string.Format("{0:n}", (totalNominal + totalAcrescimos - subTotalDeducoes));
                xrLabel25.Text = string.Format("{0:n}", (totalBaixado));
            }
            else
            {
                xrLabel22.Text = string.Format("{0:n}", (totalNominal));
                xrLabel21.Text = string.Format("{0:n}", (totalAcrescimos));
                xrLabel20.Text = string.Format("{0:n}", (totalDeducoes));
                xrLabel19.Text = string.Format("{0:n}", (totalNominal + totalAcrescimos - subTotalDeducoes));
            }
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel39.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GREPORT WHERE CODREPORT = ?", new object[] { Parametros[1].Valor }).ToString();
            xrLabel43.Text = string.Format("{0:dd/MM/yyyy}  a  {1:dd/MM/yyyy}", Convert.ToDateTime(Parametros[6].Valor), Convert.ToDateTime(Parametros[7].Valor));
            xrLabel44.Text = Parametros[4].Valor.ToString();
            xrLabel45.Text = Parametros[2].Valor.ToString() + " / " + Parametros[3].Valor.ToString();
            xrLabel23.Text = Parametros[5].Valor.ToString();
            xrLabel3.Text = string.Format("{0:dd/MM/yyyy}", Parametros[8].Valor);
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Parametros[5].Valor.Equals("BAIXADOS"))
            {
                xrLabel8.Text = "Baixa";
            }
            else
            {
                xrLabel9.Text = "";
            }
        }

    }
}
