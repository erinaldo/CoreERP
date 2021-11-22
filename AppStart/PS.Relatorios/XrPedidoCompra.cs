using System.Collections.Generic;
using System.Data;
using System;
using System.Drawing;

namespace Relatorios
{
    public partial class XrPedidoCompra : DevExpress.XtraReports.UI.XtraReport
    {

        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private List<PS.Lib.DataField> Parametros { get; set; }


        public XrPedidoCompra(List<PS.Lib.DataField> Params)
        {
            InitializeComponent();
            this.Parametros = Params;
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sSql = @"SELECT IMAGEM FROM GIMAGEM WHERE CODIMAGEM = (SELECT CODIMAGEM FROM GEMPRESA WHERE CODEMPRESA = ?)";
            byte[] arrayimagem = (byte[])dbs.QueryValue(null, sSql, this.Parametros[0].Valor);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(arrayimagem);
            logo.Image = Image.FromStream(ms);
            sSql = @"SELECT GFILIAL.NOME,
(COALESCE(GTIPORUA.NOME, '') + 
COALESCE(' ' + GFILIAL.RUA, '') + 
COALESCE( ',' + GFILIAL.NUMERO, '') + 
COALESCE(' - ' + GFILIAL.COMPLEMENTO, '') + 
COALESCE(' - ' + GTIPOBAIRRO.NOME, '') + 
COALESCE(' ' +  GFILIAL.BAIRRO, '') + 
COALESCE(' - ' + GCIDADE.NOME, '') + 
COALESCE( ' - ' + GCIDADE.CODETD, '') + 
COALESCE (' - ' + GFILIAL.CEP, '') ) AS RUA,
CGCCPF,
INSCRICAOESTADUAL,
TELEFONE,
EMAIL,
WEBSITE
FROM GFILIAL INNER JOIN GOPER ON GFILIAL.CODEMPRESA = GOPER.CODEMPRESA AND GFILIAL.CODFILIAL = GOPER.CODFILIAL
LEFT JOIN GCIDADE ON GFILIAL.CODCIDADE = GCIDADE.CODCIDADE
INNER JOIN GTIPOBAIRRO ON GFILIAL.CODTIPOBAIRRO = GTIPOBAIRRO.CODTIPOBAIRRO
INNER JOIN GTIPORUA ON GFILIAL.CODTIPORUA = GTIPORUA.CODTIPORUA
WHERE
GFILIAL.CODEMPRESA = ?
AND GOPER.CODOPER = ?";
            DataTable dt = dbs.QuerySelect(sSql, new Object[] { this.Parametros[0].Valor, this.Parametros[1].Valor });
            if (dt.Rows.Count > 0)
            {
                xrLabel13.Text = dt.Rows[0]["NOME"].ToString().ToUpper();
                xrLabel12.Text = dt.Rows[0]["RUA"].ToString().ToUpper();
                xrLabel15.Text = "CNPJ: " + dt.Rows[0]["CGCCPF"].ToString().ToUpper();
                xrLabel16.Text = "INSCRIÇÃO ESTADUAL: " + dt.Rows[0]["INSCRICAOESTADUAL"].ToString().ToUpper();
                xrLabel14.Text = "TELEFONE: " + dt.Rows[0]["TELEFONE"].ToString().ToUpper();
                xrLabel17.Text = dt.Rows[0]["WEBSITE"].ToString();
                xrLabel18.Text = dt.Rows[0]["EMAIL"].ToString();
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sql = @"SELECT  
GOPER.NUMERO, 
VCLIFOR.NOME, 
GOPER.DATAEMISSAO, 
VCLIFOR.NOMEFANTASIA, 
(COALESCE(GTIPORUA.NOME, '') + 
COALESCE(' ' + VCLIFOR.RUA, '') + 
COALESCE( ',' + VCLIFOR.NUMERO, '') + 
COALESCE(' - ' + VCLIFOR.COMPLEMENTO, '') + 
COALESCE(' - ' + GTIPOBAIRRO.NOME, '') + 
COALESCE(' ' +  VCLIFOR.BAIRRO, '') + 
COALESCE(' - ' + GCIDADE.NOME, '') + 
COALESCE( ' - ' + GCIDADE.CODETD, '') + 
COALESCE (' - ' + VCLIFOR.CEP, '') ) AS RUA, 
VCLIFOR.TELCOMERCIAL, 
VCLIFOR.TELCELULAR, 
VCLIFOR.INSCRICAOESTADUAL, 
VCLIFOR.CGCCPF, 
VREPRE.CODREPRE, 
VREPRE.NOMEFANTASIA REPRESENTANTE, 
VOPERADOR.CODOPERADOR, 
VOPERADOR.NOME OPERADOR,
VCLIFOR.EMAIL
FROM
GOPER LEFT OUTER JOIN VREPRE ON GOPER.CODEMPRESA = VREPRE.CODEMPRESA AND GOPER.CODREPRE = VREPRE.CODREPRE 
LEFT JOIN VOPERADOR ON GOPER.CODEMPRESA = VOPERADOR.CODEMPRESA AND GOPER.CODOPERADOR = VOPERADOR.CODOPERADOR
INNER JOIN VCLIFOR ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR
LEFT OUTER JOIN GCIDADE ON VCLIFOR.CODCIDADE = GCIDADE.CODCIDADE
LEFT OUTER JOIN GTIPORUA ON VCLIFOR.CODTIPORUA = GTIPORUA.CODTIPORUA
LEFT OUTER JOIN GTIPOBAIRRO ON VCLIFOR.CODTIPOBAIRRO = GTIPOBAIRRO.CODTIPOBAIRRO
WHERE 
GOPER.CODEMPRESA = ?
AND GOPER.CODOPER = ?
";
            DataTable dt = dbs.QuerySelect(sql, new Object[] { this.Parametros[0].Valor, this.Parametros[1].Valor });
            if (dt.Rows.Count > 0)
            {
                xrLabel2.Text = string.Format("Número do Pedido : {0}", dt.Rows[0]["NUMERO"].ToString());
                xrLabel34.Text = dt.Rows[0]["NOME"].ToString();
                xrLabel9.Text = Convert.ToDateTime(dt.Rows[0]["DATAEMISSAO"].ToString()).ToShortDateString();
                xrLabel7.Text = dt.Rows[0]["NOMEFANTASIA"].ToString();
                xrLabel11.Text = dt.Rows[0]["RUA"].ToString().ToUpper();
                xrLabel96.Text = dt.Rows[0]["EMAIL"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["TELCOMERCIAL"].ToString()))
                {
                    xrLabel19.Text = dt.Rows[0]["TELCOMERCIAL"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[0]["TELCELULAR"].ToString()))
                    {
                        xrLabel19.Text = xrLabel19.Text + " -- Celular: " + dt.Rows[0]["TELCELULAR"].ToString();
                    }
                }
                else if (!string.IsNullOrEmpty(dt.Rows[0]["TELCELULAR"].ToString()))
                {
                    xrLabel19.Text = dt.Rows[0]["TELCELULAR"].ToString();
                }
                xrLabel22.Text = dt.Rows[0]["INSCRICAOESTADUAL"].ToString();
                xrLabel24.Text = dt.Rows[0]["CGCCPF"].ToString();
                xrLabel26.Text = dt.Rows[0]["REPRESENTANTE"].ToString();
                xrLabel28.Text = dt.Rows[0]["OPERADOR"].ToString();
            }
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sql = @"SELECT 

	VPRODUTO.CODPRODUTO, 
	(VPRODUTO.NOME +  COALESCE(' - ' + VPRODUTOCODICO.CODIGOBARRAS, '')) DESCRICAO, 
	GOPERITEM.QUANTIDADE, 
	GOPERITEM.VLUNITARIO, 
	GOPERITEM.VLTOTALITEM, 
	GOPERITEM.CODUNIDOPER,
	(
		SELECT GOPERITEMTRIBUTO.ALIQUOTA 
		FROM GOPERITEMTRIBUTO, VTRIBUTO 
		WHERE GOPERITEM.CODOPER = GOPERITEMTRIBUTO.CODOPER 
		AND GOPERITEM.NSEQITEM = GOPERITEMTRIBUTO.NSEQITEM
		AND GOPERITEMTRIBUTO.CODEMPRESA = VTRIBUTO.CODEMPRESA
		AND GOPERITEMTRIBUTO.CODTRIBUTO = VTRIBUTO.CODTRIBUTO
		AND VTRIBUTO.CODTIPOTRIBUTO = 'IPI'
	) ALIQUOTA_IPI,
	(
		SELECT GOPERITEMTRIBUTO.ALIQUOTA 
		FROM GOPERITEMTRIBUTO, VTRIBUTO 
		WHERE GOPERITEM.CODOPER = GOPERITEMTRIBUTO.CODOPER 
		AND GOPERITEM.NSEQITEM = GOPERITEMTRIBUTO.NSEQITEM
		AND GOPERITEMTRIBUTO.CODEMPRESA = VTRIBUTO.CODEMPRESA
		AND GOPERITEMTRIBUTO.CODTRIBUTO = VTRIBUTO.CODTRIBUTO
		AND VTRIBUTO.CODTIPOTRIBUTO = 'ICMS'
	) ALIQUOTA_ICMS
FROM 
	GOPER INNER JOIN GOPERITEM ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER 
	INNER JOIN VPRODUTO ON GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO 
	LEFT JOIN VPRODUTOCODICO ON VPRODUTO.CODPRODUTO = VPRODUTOCODICO.CODPRODUTO AND VPRODUTO.CODEMPRESA = VPRODUTOCODICO.CODEMPRESA
WHERE 
	GOPER.CODEMPRESA = ?
	AND GOPER.CODOPER = ?";
            DataTable dt = dbs.QuerySelect(sql, this.Parametros[0].Valor, this.Parametros[1].Valor);
            if (dt.Rows.Count > 0)
            {
                DetailReport.DataSource = dt;
                xrLabel51.DataBindings.Add("Text", null, "CODPRODUTO");
                xrLabel52.DataBindings.Add("Text", null, "DESCRICAO");
                xrLabel53.DataBindings.Add("Text", null, "QUANTIDADE", "{0:n2}");
                xrLabel54.DataBindings.Add("Text", null, "VLUNITARIO", "{0:n2}");
                //xrLabel55.DataBindings.Add("Text", null, "PRDESCONTO", "{0:n2}");
                xrLabel56.DataBindings.Add("Text", null, "VLTOTALITEM", "{0:n2}");
                xrLabel40.DataBindings.Add("Text", null, "CODUNIDOPER");
                xrLabel91.DataBindings.Add("Text", null, "ALIQUOTA_ICMS", "{0:n2}");
                xrLabel93.DataBindings.Add("Text", null, "ALIQUOTA_IPI", "{0:n2}");
            }
            sql = @"SELECT 
	GOPER.VALORBRUTO,
	GOPER.VALORDESCONTO,
	GOPER.VALORDESPESA,
	GOPER.VALORSEGURO,
	GOPER.OBSERVACAO, 
	GOPER.VALORFRETE,
	(
		SELECT SUM(VALOR) IPI 
		FROM 
		GOPERITEMTRIBUTO 
		WHERE 
		CODTRIBUTO = 'IPI'
		AND CODEMPRESA = ?
		AND CODOPER = ?
	) IPI
FROM
	 GOPER 
WHERE 
	GOPER.CODEMPRESA = ?
	AND GOPER.CODOPER = ?

 ";
            dt = dbs.QuerySelect(sql, this.Parametros[0].Valor, this.Parametros[1].Valor, this.Parametros[0].Valor, this.Parametros[1].Valor);
            if (dt.Rows.Count > 0)
            {
                xrLabel60.Text = string.Format("{0:n2}", dt.Rows[0]["VALORBRUTO"]);
                xrLabel102.Text = string.Format("{0:n2}", dt.Rows[0]["VALORFRETE"]);
                xrLabel99.Text = string.Format("{0:n2}", dt.Rows[0]["VALORDESCONTO"]);
                xrLabel105.Text = string.Format("{0:n2}", dt.Rows[0]["VALORDESPESA"]);
                xrLabel107.Text = string.Format("{0:n2}", dt.Rows[0]["VALORSEGURO"]);
                xrLabel95.Text = string.Format("{0:n2}", dt.Rows[0]["IPI"]);
                xrLabel100.Text = string.Format("{0:n2}", Convert.ToDecimal(xrLabel60.Text) + Convert.ToDecimal(xrLabel102.Text) + Convert.ToDecimal(xrLabel105.Text) + Convert.ToDecimal(xrLabel107.Text) + Convert.ToDecimal(xrLabel95.Text) - Convert.ToDecimal(xrLabel99.Text));
                xrLabel66.Text = dt.Rows[0]["OBSERVACAO"].ToString();
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sql = @"SELECT 
(COALESCE(GTIPORUA.NOME, '') 
+ COALESCE(' ' + VCLIFOR.RUAENT, '')
+ COALESCE(', ' + VCLIFOR.NUMEROENT,'') 
+ COALESCE(' - ' + VCLIFOR.COMPLEMENTOENT, '') 
+ COALESCE(' - ' + GTIPOBAIRRO.NOME, '') 
+ COALESCE(' ' + VCLIFOR.BAIRROENT,'') 
+ COALESCE(' - ' + GCIDADE.NOME, '') 
+ COALESCE(' - ' + GCIDADE.CODETD, '') 
+ COALESCE(' - ' + VCLIFOR.CEPENT, '')) AS RUA,
VCONDICAOPGTO.CODCONDICAO, 
VCONDICAOPGTO.NOME, 
VTRANSPORTADORA.CODTRANSPORTADORA, 
VTRANSPORTADORA.NOMEFANTASIA, 
VCLIFOR.NOME RAZAO, 
GOPER.FRETECIFFOB, 
GOPER.DATAENTREGA,
GOPER.CODOPER
FROM GOPER
INNER JOIN VCLIFOR ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR
LEFT OUTER JOIN GCIDADE ON VCLIFOR.CODCIDADEENT = GCIDADE.CODCIDADE
LEFT OUTER JOIN GTIPORUA ON VCLIFOR.CODTIPORUA = GTIPORUA.CODTIPORUA
LEFT OUTER JOIN GTIPOBAIRRO ON VCLIFOR.CODTIPOBAIRRO = GTIPOBAIRRO.CODTIPOBAIRRO
INNER JOIN VCONDICAOPGTO ON GOPER.CODEMPRESA = VCONDICAOPGTO.CODEMPRESA AND GOPER.CODCONDICAO = VCONDICAOPGTO.CODCONDICAO
LEFT OUTER JOIN VTRANSPORTADORA ON GOPER.CODEMPRESA = VTRANSPORTADORA.CODEMPRESA AND GOPER.CODTRANSPORTADORA = VTRANSPORTADORA.CODTRANSPORTADORA
WHERE 
GOPER.CODEMPRESA = ?
AND GOPER.CODOPER = ?";
            DataTable dt = dbs.QuerySelect(sql, this.Parametros[0].Valor, this.Parametros[1].Valor);
            if (dt.Rows.Count > 0)
            {
                // xrLabel36.Text = dt.Rows[0]["RUA"].ToString().ToUpper();
                xrLabel38.Text = dt.Rows[0]["NOME"].ToString();
                xrLabel42.Text = dt.Rows[0]["NOMEFANTASIA"].ToString();
                xrLabel69.Text = dt.Rows[0]["RAZAO"].ToString();
                xrLabel57.Text = Convert.ToDateTime(dt.Rows[0]["DATAENTREGA"]).ToShortDateString();
                if (dt.Rows[0]["FRETECIFFOB"].ToString().Equals("0"))
                {
                    xrLabel89.Text = "CIF";
                }
                else
                {
                    xrLabel89.Text = "FOB";
                }
            }
        }
    }
}
