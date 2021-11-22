using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Relatorios
{
    public partial class XrComissaoRepresentante2011 : DevExpress.XtraReports.UI.XtraReport
    {
        private string sql = string.Empty;
        private decimal vlLiquido = 0, vlComissao = 0;

        //Variáveis PSLib
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private List<PS.Lib.DataField> Parametros { get; set; }


        public XrComissaoRepresentante2011(List<PS.Lib.DataField> Params)
        {
            InitializeComponent();
            this.Parametros = Params;
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            sql = @"SELECT DATAEMISSAO, CODSERIE, GOPER.NUMERO, VCLIFOR.NOMEFANTASIA CLIENTE, VALORLIQUIDO, ((GOPER.VALORLIQUIDO * GOPER.PRCOMISSAO)/100) VLCOMISSAO
FROM 
GOPER 
INNER JOIN VCLIFOR ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR
INNER JOIN VREPRE ON GOPER.CODEMPRESA = VREPRE.CODEMPRESA AND GOPER.CODREPRE = VREPRE.CODREPRE
WHERE GOPER.CODEMPRESA = ? AND convert(date, GOPER.DATAEMISSAO) >= ? AND convert(date, GOPER.DATAEMISSAO) <= ? AND GOPER.CODSERIE = '003' AND GOPER.CODREPRE = ?";
            //Tratamento da query para a inserção dos parametros opcionais.
            sql = new parametro().tratamentoQuery(sql, Parametros);
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { Parametros[0].Valor, Convert.ToDateTime(Parametros[4].Valor), Convert.ToDateTime(Parametros[5].Valor), Parametros[6].Valor });
            DetailReport.DataSource = dt;
            xrLabel11.DataBindings.Add("Text", null, "DATAEMISSAO", "{0:dd/MM/yyyy}");
            xrLabel12.DataBindings.Add("Text", null, "CODSERIE");
            xrLabel13.DataBindings.Add("Text", null, "NUMERO");
            xrLabel14.DataBindings.Add("Text", null, "CLIENTE");
            xrLabel15.DataBindings.Add("Text", null, "VALORLIQUIDO", "{0:n2}");
            xrLabel16.DataBindings.Add("Text", null, "VLCOMISSAO", "{0:n2}");
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            sql = @"SELECT IMAGEM FROM GIMAGEM INNER JOIN GEMPRESA ON GIMAGEM.CODIMAGEM = GEMPRESA.CODIMAGEM WHERE CODEMPRESA = ?";
            byte[] arrayimagem = (byte[])dbs.QueryValue(null, sql, AppLib.Context.Empresa);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(arrayimagem);
            logo.Image = Image.FromStream(ms);
            xrLabel21.Text = string.Format("Período: {0:dd/MM/yyyy} a {1:dd/MM/yyyy}", Convert.ToDateTime(Parametros[4].Valor), Convert.ToDateTime(Parametros[5].Valor));

            DataTable dtHeader = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT GFILIAL.NOMEFANTASIA, GFILIAL.RUA, GFILIAL.NUMERO, GFILIAL.COMPLEMENTO, GFILIAL.BAIRRO, GCIDADE.NOME, GFILIAL.CEP, GFILIAL.CODETD, GFILIAL.CGCCPF, GFILIAL.INSCRICAOESTADUAL, GFILIAL.TELEFONE, GFILIAL.EMAIL, GFILIAL.WEBSITE
FROM GFILIAL 
INNER JOIN GCIDADE ON GFILIAL.CODCIDADE = GCIDADE.CODCIDADE
where GFILIAL.CODEMPRESA = ? and GFILIAL.CODFILIAL = ?", new object[] { Parametros[2].Valor, Parametros[3].Valor });

            if (dtHeader.Rows.Count > 0)
            {
                xrLabel2.Text = string.Format("{0}, {1} - {2} - Cep: {3} - {4} - {5} - {6}", dtHeader.Rows[0]["RUA"].ToString(), dtHeader.Rows[0]["NUMERO"].ToString(), dtHeader.Rows[0]["COMPLEMENTO"].ToString(), dtHeader.Rows[0]["CEP"].ToString(), dtHeader.Rows[0]["BAIRRO"].ToString(), dtHeader.Rows[0]["NOME"].ToString(), dtHeader.Rows[0]["CODETD"].ToString());
            }
            xrLabel38.Text = dtHeader.Rows[0]["EMAIL"].ToString();
            xrLabel3.Text = dtHeader.Rows[0]["CGCCPF"].ToString();
            xrLabel24.Text = dtHeader.Rows[0]["INSCRICAOESTADUAL"].ToString();
            xrLabel4.Text = dtHeader.Rows[0]["TELEFONE"].ToString();
            xrLabel37.Text = dtHeader.Rows[0]["WEBSITE"].ToString();


            //Busca as informações do representante
            sql = @"SELECT NOMEFANTASIA FROM VREPRE WHERE CODREPRE = ? AND CODEMPRESA = ?";
            xrLabel22.Text = string.Format("Representante: {0} - {1}", Parametros[6].Valor, AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, sql, new object[] { Parametros[6].Valor, Parametros[2].Valor }));
        }

        private void Detail1_AfterPrint(object sender, EventArgs e)
        {
            try
            {
                vlLiquido = vlLiquido + Convert.ToDecimal(xrLabel15.Text);
                vlComissao = vlComissao + Convert.ToDecimal(xrLabel16.Text);
            }
            catch (Exception)
            {
                
            }
           
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel17.Text = string.Format("{0:n2}", vlLiquido);
            xrLabel18.Text = string.Format("{0:n2}", vlComissao);
        }

    }
}
