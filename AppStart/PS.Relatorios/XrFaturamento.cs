using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorios
{
    public partial class XrFaturamento : DevExpress.XtraReports.UI.XtraReport
    {
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private List<PS.Lib.DataField> Parametros { get; set; }
        decimal vlOriginal = 0, VlLiquido = 0, TotalOriginal = 0, TotalLiquido = 0, VlFrete = 0, TotalFrete = 0;
        private string grupo;

        public XrFaturamento(List<PS.Lib.DataField> Params, string _grupo)
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
    GOPER.CODTIPOPER,
	GTIPOPER.DESCRICAO,
    (GOPER.CODTIPOPER + ' - ' + GTIPOPER.DESCRICAO) OPERACAO, 
	GOPER.NUMERO, 
	CONVERT(DATETIME, CAST(DATAEMISSAO AS DATE)) DATAEMISSAO,
	VCLIFOR.NOME CLIENTE,
    (VNATUREZAORCAMENTO.CODNATUREZA + ' - ' + VNATUREZAORCAMENTO.DESCRICAO) NATUREZA,
	VOPERADOR.NOME OPERADOR,
	VVENDEDOR.NOME VENDEDOR,
	VREPRE.NOMEFANTASIA REPRESENTANTE,
	GCENTROCUSTO.NOME CCUSTO,
	VALORBRUTO VLORIGINAL,
	VALORLIQUIDO VLLIQUIDO,
	VALORFRETE VLFRETE,
    CASE CODSTATUS WHEN 0 THEN 'ABERTO' WHEN 1 THEN 'FATURADO' WHEN 2 THEN 'CANCELADO' WHEN 3 THEN 'PARC. QUITADO' ELSE 'QUITADO' END CODSTATUS,
    GOPER.VALORDESCONTO AS 'VLDESCONTO',
	(GOPER.VALORDESPESA + GOPER.VALORSEGURO + GOPER.VALORFRETE) AS 'VLDESPESAS'
    FROM 
	GOPER
	INNER JOIN GTIPOPER ON GOPER.CODEMPRESA = GTIPOPER.CODEMPRESA AND GOPER.CODTIPOPER = GTIPOPER.CODTIPOPER
	INNER JOIN VCLIFOR ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR
	LEFT JOIN GCENTROCUSTO ON GOPER.CODEMPRESA = GCENTROCUSTO.CODEMPRESA AND GOPER.CODCCUSTO = GCENTROCUSTO.CODCCUSTO
	LEFT JOIN VNATUREZAORCAMENTO ON GOPER.CODEMPRESA = VNATUREZAORCAMENTO.CODEMPRESA AND GOPER.CODNATUREZAORCAMENTO = VNATUREZAORCAMENTO.CODNATUREZA
	LEFT JOIN VOPERADOR ON GOPER.CODEMPRESA = VOPERADOR.CODEMPRESA AND GOPER.CODOPERADOR = VOPERADOR.CODOPERADOR
	LEFT JOIN VREPRE ON GOPER.CODEMPRESA = VREPRE.CODEMPRESA AND GOPER.CODREPRE = VREPRE.CODREPRE
	LEFT JOIN VVENDEDOR ON GOPER.CODVENDEDOR = VVENDEDOR.CODVENDEDOR AND GOPER.CODEMPRESA = VVENDEDOR.CODEMPRESA
    WHERE 
	GOPER.CODSTATUS = ?
    AND GOPER.CODEMPRESA = ?
    AND GOPER.CODFILIAL = ?
    AND convert(date, GOPER.DATAEMISSAO) >= ?
    AND convert(date, GOPER.DATAEMISSAO) <= ?
    AND GTIPOPER.CODTIPOPER IN (?) ";

            // Busca a informação do grupo de faturamento 
            if (!Parametros[7].Valor.ToString().Equals("TODOS"))
            {
                string grupoRel = getGrupoRel(Parametros[7].Valor.ToString());
                if (!string.IsNullOrEmpty(grupoRel))
                {
                    sql = sql.Replace("(?)", "( " + getGrupoRel(Parametros[7].Valor.ToString()) + ")");
                }
                else
                {
                    sql = sql.Replace("AND GTIPOPER.CODTIPOPER IN (?)", "");
                }

            }
            else
            {
                sql = sql.Replace("AND GTIPOPER.CODTIPOPER IN (?)", "");
            }


            sql = new parametro().tratamentoQuery(sql, Parametros);
            System.Data.DataTable dt = new System.Data.DataTable();
            if (Parametros[4].Valor.ToString().Equals("3") || string.IsNullOrEmpty(Parametros[4].Valor.ToString()))
            {
                sql = sql.Replace("\r\n\tGOPER.CODSTATUS = ?\r\n", "\r\n\tGOPER.CODSTATUS <> ?\r\n");

                dt = dbs.QuerySelect(sql, new object[] { "2", Parametros[2].Valor, Parametros[3].Valor, Convert.ToDateTime(Parametros[5].Valor), Convert.ToDateTime(Parametros[6].Valor) });
            }
            else
            {
                dt = dbs.QuerySelect(sql, new object[] { Parametros[4].Valor, Parametros[2].Valor, Parametros[3].Valor, Convert.ToDateTime(Parametros[5].Valor), Convert.ToDateTime(Parametros[6].Valor) });
            }

            DetailReport.DataSource = dt;

            lblNumero.DataBindings.Add("Text", null, "NUMERO");
            xrLabel8.DataBindings.Add("Text", null, "CODTIPOPER");
            lblEmissao.DataBindings.Add("Text", null, "DATAEMISSAO", "{0:dd/MM/yyyy}");
            lblNome.DataBindings.Add("Text", null, "CLIENTE");
            lblCentroCusto.DataBindings.Add("Text", null, "CCUSTO");
            lblNatureza.DataBindings.Add("Text", null, "NATUREZA");
            lblVendedor.DataBindings.Add("Text", null, "VENDEDOR");
            lblRepresentante.DataBindings.Add("Text", null, "REPRESENTANTE");
            lblVLBruto.DataBindings.Add("Text", null, "VLORIGINAL", "{0:n2}");
            lblVLLiquido.DataBindings.Add("Text", null, "VLLIQUIDO", "{0:n2}");
            lblVLFrete.DataBindings.Add("Text", null, "VLFRETE", "{0:n2}");
            xrLabel15.DataBindings.Add("Text", null, "CODSTATUS");
            lbDesconto.DataBindings.Add("Text", null, "VLDESCONTO", "{0:n2}");

            //Cria grupo
            switch (grupo)
            {
                case "CODTIPOPER":
                    xrLabel3.Text = "OPERAÇÃO";
                    grupo = "OPERACAO";
                    xrLabel7.DataBindings.Add("Text", null, grupo);
                    break;
                case "DATAEMISSAO":
                    xrLabel3.Text = "DATA DE EMISSÃO";
                    xrLabel7.DataBindings.Add("Text", null, grupo, "{0:dd/MM/yyyy}");
                    break;
                case "CODVENDEDOR":
                    xrLabel3.Text = "VENDEDOR";
                    xrLabel7.DataBindings.Add("Text", null, grupo);
                    break;
                default:
                    xrLabel3.Text = grupo;
                    xrLabel7.DataBindings.Add("Text", null, grupo);
                    break;
            }


            GroupHeader1.GroupFields.Add(new GroupField(grupo));
        }
        private string getGrupoRel(string GrupoRel)
        {
            string retorno = string.Empty;
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODTIPOPER FROM GTIPOPER INNER JOIN GGRUPOREL ON GTIPOPER.IDGRUPOREL = GGRUPOREL.IDGRUPOREL WHERE GRUPO = ?", new object[] { GrupoRel });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i.Equals(0))
                {
                    retorno = "'" + dt.Rows[i]["CODTIPOPER"].ToString() + "'";
                }
                else
                {
                    retorno = retorno + ", '" + dt.Rows[i]["CODTIPOPER"].ToString() + "'";
                }
            }
            return retorno;
        }
        private void Detail1_AfterPrint(object sender, EventArgs e)
        {
            try
            {
                vlOriginal = vlOriginal + Convert.ToDecimal(lblVLBruto.Text);
                VlLiquido = VlLiquido + Convert.ToDecimal(lblVLLiquido.Text);
                VlFrete = VlFrete + Convert.ToDecimal(lblVLFrete.Text);
            }
            catch (Exception)
            {

                vlOriginal = vlOriginal + 0;
                VlLiquido = VlLiquido + 0;
                VlFrete = VlFrete + 0;
            }

        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            xrLabel18.Text = string.Format("{0:n2}", vlOriginal);
            xrLabel24.Text = string.Format("{0:n2}", VlLiquido);
            xrLabel17.Text = string.Format("{0:n2}", VlFrete);

            TotalOriginal = TotalOriginal + vlOriginal;
            TotalLiquido = TotalLiquido + VlLiquido;
            TotalFrete = TotalFrete + VlFrete;

            vlOriginal = 0;
            VlLiquido = 0;
            VlFrete = 0;
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel19.Text = string.Format("{0:n2}", TotalOriginal);
            xrLabel25.Text = string.Format("{0:n2}", TotalLiquido);
            xrLabel20.Text = string.Format("{0:n2}", TotalFrete);
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel39.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GREPORT WHERE CODREPORT = ?", new object[] { Parametros[1].Valor }).ToString();
            xrLabel43.Text = string.Format("{0:dd/MM/yyyy}  a  {1:dd/MM/yyyy}", Convert.ToDateTime(Parametros[5].Valor), Convert.ToDateTime(Parametros[6].Valor));
            xrLabel45.Text = Parametros[2].Valor.ToString() + " / " + Parametros[3].Valor.ToString();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

    }
}
