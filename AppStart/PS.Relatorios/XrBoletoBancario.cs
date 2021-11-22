using System.Collections.Generic;
using System.Data;
using System;
using System.Drawing;


namespace Relatorios
{
    public partial class XrBoletoBancario : DevExpress.XtraReports.UI.XtraReport
    {
        int CODEMPRESA;
        String CODLANCA;
        public XrBoletoBancario(int _CODEMPRESA, String _CODLANCA)
        {
            InitializeComponent();
            CODEMPRESA = _CODEMPRESA;
            CODLANCA = _CODLANCA;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void Detail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            

        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sql = @"SELECT 
GIMAGEM2.IMAGEM IMGBANCO,
GIMAGEM.IMAGEM,
('Pagável Preferencialmente nas agências do ' + CAST(FCONTA.DESCRICAO AS VARCHAR(50))) DESCRICAO, 
FCONTA.CODCONTA,
GEMPRESA.NOME, 
FBOLETO.DATAEMISSAO,  
FBOLETO.NUMERO,
CASE FBOLETO.ACEITE WHEN 0 THEN
	'Não'
	ELSE
	'Sim'
	END ACEITE,
FBOLETO.DATABOLETO,
FCONVENIO.CARTEIRA,
FBOLETO.DATAVENCIMENTO,
CASE LEN(NOSSONUMERO) 
	WHEN 3 THEN (CAST(FCONVENIO.CARTEIRA AS VARCHAR(10)) + '/' + '000000000' + CAST(NOSSONUMERO AS VARCHAR(15)) + '-' + CAST(FBOLETO.DVNOSSONUMERO AS VARCHAR(10)))
	WHEN 4 THEN (CAST(FCONVENIO.CARTEIRA AS VARCHAR(10)) + '/' + '00000000' + CAST(NOSSONUMERO AS VARCHAR(15)) + '-' + CAST(FBOLETO.DVNOSSONUMERO AS VARCHAR(10)))
	WHEN 5 THEN (CAST(FCONVENIO.CARTEIRA AS VARCHAR(10)) + '/' + '0000000' + CAST(NOSSONUMERO AS VARCHAR(15)) + '-' + CAST(FBOLETO.DVNOSSONUMERO AS VARCHAR(10)))
	WHEN 6 THEN (CAST(FCONVENIO.CARTEIRA AS VARCHAR(10)) + '/' + '000000' + CAST(NOSSONUMERO AS VARCHAR(15)) + '-' + CAST(FBOLETO.DVNOSSONUMERO AS VARCHAR(10)))
	WHEN 7 THEN (CAST(FCONVENIO.CARTEIRA AS VARCHAR(10)) + '/' + '00000' + CAST(NOSSONUMERO AS VARCHAR(15)) + '-' + CAST(FBOLETO.DVNOSSONUMERO AS VARCHAR(10)))		
	WHEN 8 THEN (CAST(FCONVENIO.CARTEIRA AS VARCHAR(10)) + '/' + '0000' + CAST(NOSSONUMERO AS VARCHAR(15)) + '-' + CAST(FBOLETO.DVNOSSONUMERO AS VARCHAR(10)))
	WHEN 9 THEN (CAST(FCONVENIO.CARTEIRA AS VARCHAR(10)) + '/' + '000' + CAST(NOSSONUMERO AS VARCHAR(15)) + '-' + CAST(FBOLETO.DVNOSSONUMERO AS VARCHAR(10)))
	WHEN 10 THEN (CAST(FCONVENIO.CARTEIRA AS VARCHAR(10)) + '/' + '00' + CAST(NOSSONUMERO AS VARCHAR(15)) + '-' + CAST(FBOLETO.DVNOSSONUMERO AS VARCHAR(10)))
	WHEN 11 THEN (CAST(FCONVENIO.CARTEIRA AS VARCHAR(10)) + '/' + '0' + CAST(NOSSONUMERO AS VARCHAR(15)) + '-' + CAST(FBOLETO.DVNOSSONUMERO AS VARCHAR(10)))
	WHEN 12 THEN NOSSONUMERO
	END CARTNUMERO,
(
CAST(FCONTA.CODAGENCIA AS VARCHAR(10)) 
+ '-' + CAST( GAGENCIA.DVAGENCIA AS VARCHAR(10)) 
+ '/' + CAST(FCONTA.NUMCONTA AS VARCHAR(10)) 
+ '-' + CAST(FCCORRENTE.DIGCONTA AS VARCHAR(10)) 
) CODAGENCIA,
FBOLETO.VALOR,
(((FBOLETO.VALOR * 5.9)/100)/30) JUROS,
(((FBOLETO.VALOR + ((((FBOLETO.VALOR * 5.9)/100)/30) ))*2)/100) MULTA,
(
CAST(VCLIFOR.CODCLIFOR AS VARCHAR(10))
+ '-' + CAST(VCLIFOR.NOME AS VARCHAR(255)) 
+ '-' + CAST(VCLIFOR.CGCCPF AS VARCHAR(255)) 
) CLIENTE,
(
COALESCE(GTIPORUA.NOME, '')
 + ' ' + COALESCE(VCLIFOR.RUAPAG, '')  
 + ', ' + COALESCE(VCLIFOR.NUMEROPAG, '') 
 + ' - ' + COALESCE(VCLIFOR.BAIRROPAG, '') 
 + ' - ' + COALESCE(VCLIFOR.COMPLEMENTOPAG, '') 
) ENDERECO,
(
COALESCE(VCLIFOR.CEPPAG, '') 
+ ' - ' + COALESCE(GCIDADE.NOME, '' ) 
+ ' - ' + COALESCE(VCLIFOR.CODETDPAG, '')
) CEP,
FBOLETO.CODIGOBARRAS,
(
  SUBSTRING(FBOLETO.IPTE, 1, 5) 
+ '.' + SUBSTRING(FBOLETO.IPTE, 6, 5) 
+ ' ' + SUBSTRING(FBOLETO.IPTE, 11, 5) 
+ '.' + SUBSTRING(FBOLETO.IPTE, 16, 6) 
+ ' ' + SUBSTRING(FBOLETO.IPTE, 22, 5) 
+ '.' + SUBSTRING(FBOLETO.IPTE, 27, 6) 
+ ' ' + SUBSTRING(FBOLETO.IPTE, 33, 1) 
+ ' ' + SUBSTRING(FBOLETO.IPTE, 34, 14) 
) IPTE,
VCLIFOR.NOME NOMECLIENTE
FROM 
FBOLETO 
INNER JOIN FCONTA ON FBOLETO.CODCONTA = FCONTA.CODCONTA AND FBOLETO.CODEMPRESA = FCONTA.CODEMPRESA
INNER JOIN VCLIFOR ON FBOLETO.CODEMPRESA = VCLIFOR.CODEMPRESA AND FBOLETO.CODCLIFOR = VCLIFOR.CODCLIFOR
INNER JOIN GEMPRESA ON FBOLETO.CODEMPRESA = GEMPRESA.CODEMPRESA
INNER JOIN FCONVENIO ON FBOLETO.CODCONTA = FCONVENIO.CODCONTA AND FBOLETO.CODEMPRESA = FCONVENIO.CODEMPRESA
LEFT JOIN GTIPORUA ON VCLIFOR.CODTIPORUAPAG = GTIPORUA.CODTIPORUA
INNER JOIN GCIDADE ON VCLIFOR.CODCIDADEPAG = GCIDADE.CODCIDADE
LEFT JOIN GIMAGEM ON GEMPRESA.CODIMAGEM = GIMAGEM.CODIMAGEM
INNER JOIN GAGENCIA ON FCONTA.CODAGENCIA = GAGENCIA.CODAGENCIA AND FCONTA.CODEMPRESA = GAGENCIA.CODEMPRESA
INNER JOIN FCCORRENTE ON FCONTA.NUMCONTA = FCCORRENTE.NUMCONTA AND FCONTA.CODEMPRESA = FCCORRENTE.CODEMPRESA AND FCONTA.CODAGENCIA = FCCORRENTE.CODAGENCIA
INNER JOIN GBANCO ON FCONTA.CODBANCO = GBANCO.CODBANCO AND FCONTA.CODEMPRESA = GBANCO.CODEMPRESA 
LEFT JOIN GIMAGEM GIMAGEM2 ON GBANCO.CODIMAGEM = GIMAGEM2.CODIMAGEM 
WHERE FBOLETO.CODLANCA IN " + CODLANCA + " AND FBOLETO.CODEMPRESA = ?";
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { CODEMPRESA });
            this.DetailReport.DataSource = dt;

            //Primeiro
            xrLabel69.DataBindings.Add("Text", null, "CODCONTA");
            xrLabel3.DataBindings.Add("Text", null, "DESCRICAO");
            xrLabel5.DataBindings.Add("Text", null, "NOME");
            xrLabel6.DataBindings.Add("Text", null, "DATAEMISSAO", "{0:dd/MM/yyyy}");
            xrLabel9.DataBindings.Add("Text", null, "NUMERO");
            xrLabel13.DataBindings.Add("Text", null, "ACEITE");
            xrLabel15.DataBindings.Add("Text", null, "DATABOLETO", "{0:dd/MM/yyyy}");
            xrLabel27.DataBindings.Add("Text", null, "CARTEIRA");
            xrLabel29.DataBindings.Add("Text", null, "DATAVENCIMENTO", "{0:dd/MM/yyyy}");
            xrLabel31.DataBindings.Add("Text", null, "CODAGENCIA");
            xrLabel33.DataBindings.Add("Text", null, "CARTNUMERO");
            xrLabel35.DataBindings.Add("Text", null, "VALOR", "{0:n2}");
            xrLabel52.DataBindings.Add("Text", null, "CLIENTE");
            xrLabel147.DataBindings.Add("Text", null, "ENDERECO");
            xrLabel153.DataBindings.Add("Text", null, "CEP");
            xrLabel50.DataBindings.Add("Text", null, "JUROS", "{0:n2}");
            xrLabel54.DataBindings.Add("Text", null, "MULTA", "{0:n2}");
            
            //Segundo
            xrLabel142.DataBindings.Add("Text", null, "CODCONTA");
            xrLabel119.DataBindings.Add("Text", null, "IPTE");
            xrLabel111.DataBindings.Add("Text", null, "DESCRICAO");
            xrLabel74.DataBindings.Add("Text", null, "NOME");
            xrLabel86.DataBindings.Add("Text", null, "DATAEMISSAO", "{0:dd/MM/yyyy}");
            xrLabel103.DataBindings.Add("Text", null, "NUMERO");
            xrLabel88.DataBindings.Add("Text", null, "ACEITE");
            xrLabel87.DataBindings.Add("Text", null, "DATABOLETO", "{0:dd/MM/yyyy}");
            xrLabel92.DataBindings.Add("Text", null, "CARTEIRA");
            xrLabel73.DataBindings.Add("Text", null, "DATAVENCIMENTO", "{0:dd/MM/yyyy}");
            xrLabel72.DataBindings.Add("Text", null, "CODAGENCIA");
            xrLabel71.DataBindings.Add("Text", null, "CARTNUMERO");
            xrLabel70.DataBindings.Add("Text", null, "VALOR", "{0:n2}");
            xrLabel150.DataBindings.Add("Text", null, "CLIENTE");
            xrLabel160.DataBindings.Add("Text", null, "ENDERECO");
            xrLabel163.DataBindings.Add("Text", null, "CEP");
            xrLabel61.DataBindings.Add("Text", null, "JUROS", "{0:n2}");
            xrLabel60.DataBindings.Add("Text", null, "MULTA", "{0:n2}");

            //Comprovante
            xrLabel118.DataBindings.Add("Text", null, "NOME");
            xrLabel127.DataBindings.Add("Text", null, "NOMECLIENTE");
            xrLabel121.DataBindings.Add("Text", null, "CODAGENCIA");
            xrLabel128.DataBindings.Add("Text", null, "DATAVENCIMENTO", "{0:dd/MM/yyyy}");
            xrLabel139.DataBindings.Add("Text", null, "DATAEMISSAO", "{0:dd/MM/yyyy}");
            xrLabel123.DataBindings.Add("Text", null, "CARTNUMERO");
            xrLabel130.DataBindings.Add("Text", null, "VALOR", "{0:n2}");
            xrLabel125.DataBindings.Add("Text", null, "NUMERO");
            xrLabel75.DataBindings.Add("Text", null, "CODCONTA");

            //Código de Barras  e imagens
            xrBarCode1.DataBindings.Add("Text", null, "CODIGOBARRAS");
            xrPictureBox1.DataBindings.Add("Image", null, "IMAGEM");
            xrPictureBox2.DataBindings.Add("Image", null, "IMAGEM");
            xrPictureBox3.DataBindings.Add("Image", null, "IMGBANCO");
            xrPictureBox4.DataBindings.Add("Image", null, "IMGBANCO");
            xrPictureBox5.DataBindings.Add("Image", null, "IMGBANCO");
        }

        private void xrLabel108_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

    }
}
