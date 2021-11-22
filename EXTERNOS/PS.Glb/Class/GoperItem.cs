using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PS.Glb;

namespace PS.Glb.Class
{
    public class GoperItem
    {
        #region Construtor

        public GoperItem() { }

        #endregion

        #region Propriedades

        public int CODEMPRESA { get; set; }
        public int CODOPER { get; set; }
        public string CODTIPOPER { get; set; }
        public int NSEQITEM { get; set; }
        public string CODPRODUTO { get; set; }
        public decimal QUANTIDADE { get; set; }
        public decimal VLUNITARIO { get; set; }
        public decimal VLDESCONTO { get; set; }
        public decimal PRDESCONTO { get; set; }
        public decimal VLTOTALITEM { get; set; }
        public string CODCFOP { get; set; }
        public string CODNATUREZA { get; set; }
        public string CAMPOLIVRE1 { get; set; }
        public string CAMPOLIVRE2 { get; set; }
        public string CAMPOLIVRE3 { get; set; }
        public string CAMPOLIVRE4 { get; set; }
        public string CAMPOLIVRE5 { get; set; }
        public string CAMPOLIVRE6 { get; set; }
        public DateTime? DATAEXTRA1 { get; set; }
        public DateTime? DATAEXTRA2 { get; set; }
        public DateTime? DATAEXTRA3 { get; set; }
        public DateTime? DATAEXTRA4 { get; set; }
        public DateTime? DATAEXTRA5 { get; set; }
        public DateTime? DATAEXTRA6 { get; set; }
        public decimal CAMPOVALOR1 { get; set; }
        public decimal CAMPOVALOR2 { get; set; }
        public decimal CAMPOVALOR3 { get; set; }
        public decimal CAMPOVALOR4 { get; set; }
        public decimal CAMPOVALOR5 { get; set; }
        public decimal CAMPOVALOR6 { get; set; }
        public string CODUNIDOPER { get; set; }
        public string OBSERVACAO { get; set; }
        public string INFCOMPL { get; set; }
        public string CODTABPRECO { get; set; }
        public decimal QUANTIDADE_FATURADO { get; set; }
        public decimal QUANTIDADE_SALDO { get; set; }
        public decimal NACIONALFEDERALIBPTAX { get; set; }
        public string UFIBPTAX { get; set; }
        public decimal IMPORTADOSFEDERALIBPTAX { get; set; }
        public decimal ESTADUALIBPTAX { get; set; }
        public decimal MUNICIPALIBPTAX { get; set; }
        public string CHAVEIBPTAX { get; set; }
        public string APLICACAOMATERIAL { get; set; }
        public decimal VLACRESCIMO { get; set; }
        public decimal PRACRESCIMO { get; set; }
        public string TIPODESCONTO { get; set; }
        public decimal VLUNITORIGINAL { get; set; }
        public int TOTALEDITADO { get; set; }
        public decimal RATEIODESPESA { get; set; }
        public decimal RATEIODESCONTO { get; set; }
        public decimal RATEIOFRETE { get; set; }
        public decimal RATEIOSEGURO { get; set; }
        public DateTime? DATAENTREGA { get; set; }
        public string NOMEPRODUTO { get; set; }
        public string XPED { get; set; }
        public int NITEMPED { get; set; }
        public string NUMERODI { get; set; }
        public DateTime? DATADI { get; set; }
        public string LOCDESEMB { get; set; }
        public string UFDESEMB { get; set; }
        public DateTime? DATADESEMB { get; set; }
        public string CODEXPORTADOR { get; set; }
        public string NUMADICAO { get; set; }
        public string NUMSEQADIC { get; set; }
        public string CODFABRICANTE { get; set; }
        public decimal VLORDESCDI { get; set; }
        public string TPVIATRANSP { get; set; }
        public decimal VAFRMM { get; set; }
        public string TPINTERMEDIO { get; set; }
        public string CNPJ { get; set; }
        public string UFTERCEIRO { get; set; }
        public string NDRAW { get; set; }
        public decimal QTD_ORIGINAL { get; set; }
        public decimal QTD_SALDO { get; set; }
        public decimal QTD_FATURAR { get; set; }
        public int NSEQDESTINO { get; set; }
        public IBPTAX IBPTAX { get; set; }

        public string NUMERO { get; set; }
        public string CODIGOAUXILIAR { get; set; }
        public string DESCRICAOPRODUTO { get; set; }

        public string CODUNIDCONTROLE { get; set; }
        public decimal QUANTIDADECONTROLE { get; set; }
        public decimal FATORCONVERSAO { get; set; }

        #endregion

        #region Variáveis 

        private Goper Goper = new Goper();

        #endregion

        /// <summary>
        /// Método para buscar os itens da operação para o Faturamento
        /// </summary>
        /// <param name="CodOper">Código da Operação</param>
        /// <returns>Lista dos Itens referentes a operação</returns>
        public List<Class.GoperItem> getGoperItemFaturamento(List<string> CodOper, AppLib.Data.Connection conn)
        {
            List<Class.GoperItem> listaItens = new List<Class.GoperItem>();
            for (int iCodOper = 0; iCodOper < CodOper.Count; iCodOper++)
            {
                DataTable  dt = conn.ExecQuery(@"SELECT GOPERITEM.*, VPRODUTO.DESCRICAO, GOPER.NUMERO, VPRODUTO.CODIGOAUXILIAR
FROM 
GOPERITEM 
INNER JOIN VPRODUTO ON GOPERITEM.CODPRODUTO  =VPRODUTO.CODPRODUTO AND GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA
INNER JOIN GOPER ON GOPERITEM.CODOPER = GOPER.CODOPER AND GOPERITEM.CODEMPRESA = GOPER.CODEMPRESA WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODOPER = ? AND GOPERITEM.QUANTIDADE_SALDO > 0", new object[] { AppLib.Context.Empresa, CodOper[iCodOper] });
                for (int iItens = 0; iItens < dt.Rows.Count; iItens++)
                {
                    Class.GoperItem itens = new Class.GoperItem();
                    itens.CODOPER = Convert.ToInt32(dt.Rows[iItens]["CODOPER"]);
                    itens.CODEMPRESA = Convert.ToInt32(dt.Rows[iItens]["CODEMPRESA"]);
                    itens.NUMERO = dt.Rows[iItens]["NUMERO"].ToString();
                    itens.CODIGOAUXILIAR = dt.Rows[iItens]["CODIGOAUXILIAR"].ToString();
                    itens.CAMPOLIVRE1 = dt.Rows[iItens]["CAMPOLIVRE1"].ToString();
                    itens.CAMPOLIVRE2 = dt.Rows[iItens]["CAMPOLIVRE2"].ToString();
                    itens.CAMPOLIVRE3 = dt.Rows[iItens]["CAMPOLIVRE3"].ToString();
                    itens.CAMPOLIVRE4 = dt.Rows[iItens]["CAMPOLIVRE4"].ToString();
                    itens.CAMPOLIVRE5 = dt.Rows[iItens]["CAMPOLIVRE5"].ToString();
                    itens.CAMPOLIVRE6 = dt.Rows[iItens]["CAMPOLIVRE6"].ToString();
                    itens.CAMPOVALOR1 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR1"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR1"]);
                    itens.CAMPOVALOR2 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR2"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR2"]);
                    itens.CAMPOVALOR3 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR3"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR3"]);
                    itens.CAMPOVALOR4 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR4"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR4"]);
                    itens.CAMPOVALOR5 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR5"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR5"]);
                    itens.CAMPOVALOR6 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR6"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR6"]);
                    itens.DESCRICAOPRODUTO = dt.Rows[iItens]["DESCRICAO"].ToString();
                    itens.CODCFOP = dt.Rows[iItens]["CODCFOP"].ToString();
                    itens.CODNATUREZA = dt.Rows[iItens]["CODNATUREZA"].ToString();
                    itens.CODPRODUTO = dt.Rows[iItens]["CODPRODUTO"].ToString();
                    itens.CODTABPRECO = dt.Rows[iItens]["CODTABPRECO"].ToString();
                    itens.CODUNIDOPER = dt.Rows[iItens]["CODUNIDOPER"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA1"].ToString()))
                    {
                        itens.DATAEXTRA1 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA1"]);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA2"].ToString()))
                    {
                        itens.DATAEXTRA2 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA2"]);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA3"].ToString()))
                    {
                        itens.DATAEXTRA3 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA3"]);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA4"].ToString()))
                    {
                        itens.DATAEXTRA4 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA4"]);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA5"].ToString()))
                    {
                        itens.DATAEXTRA5 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA5"]);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA6"].ToString()))
                    {
                        itens.DATAEXTRA6 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA6"]);
                    }
                    itens.INFCOMPL = dt.Rows[iItens]["INFCOMPL"].ToString();
                    itens.NSEQITEM = Convert.ToInt32(dt.Rows[iItens]["NSEQITEM"]);
                    itens.OBSERVACAO = dt.Rows[iItens]["OBSERVACAO"].ToString();
                    itens.PRDESCONTO = Convert.ToDecimal(dt.Rows[iItens]["PRDESCONTO"]);
                    itens.QTD_ORIGINAL = Convert.ToDecimal(dt.Rows[iItens]["QUANTIDADE"]);
                    itens.VLDESCONTO = Convert.ToDecimal(dt.Rows[iItens]["VLDESCONTO"]);
                    itens.VLTOTALITEM = Convert.ToDecimal(dt.Rows[iItens]["VLTOTALITEM"]);
                    itens.VLUNITARIO = Convert.ToDecimal(dt.Rows[iItens]["VLUNITARIO"]);
                    itens.QTD_FATURAR = Convert.ToDecimal(dt.Rows[iItens]["QUANTIDADE_SALDO"]);
                    itens.QTD_SALDO = Convert.ToDecimal(dt.Rows[iItens]["QUANTIDADE_SALDO"]);
                    itens.VLUNITORIGINAL = Convert.ToDecimal(dt.Rows[iItens]["VLUNITORIGINAL"]);
                    itens.PRACRESCIMO = Convert.ToDecimal(dt.Rows[iItens]["PRACRESCIMO"]);
                    itens.VLACRESCIMO = Convert.ToDecimal(dt.Rows[iItens]["VLACRESCIMO"]);
                    itens.CODUNIDOPER = dt.Rows[iItens]["CODUNIDOPER"].ToString();
                    if (dt.Rows[iItens]["DATAENTREGA"].ToString() != string.Empty)
                    {
                        itens.DATAENTREGA = Convert.ToDateTime(dt.Rows[iItens]["DATAENTREGA"]);
                    }
                    else
                    {
                        itens.DATAENTREGA = null;
                    }

                    itens.APLICACAOMATERIAL = dt.Rows[iItens]["APLICACAOMATERIAL"].ToString();
                    itens.TIPODESCONTO = dt.Rows[iItens]["TIPODESCONTO"].ToString();

                    if (dt.Rows[iItens]["TOTALEDITADO"] == DBNull.Value)
                    {
                        itens.TOTALEDITADO = 0;
                    }
                    else
                    {
                        itens.TOTALEDITADO = Convert.ToInt32(dt.Rows[iItens]["TOTALEDITADO"]);
                    }

                    itens.CODUNIDCONTROLE = dt.Rows[iItens]["CODUNIDCONTROLE"].ToString();
                    itens.FATORCONVERSAO = Convert.ToDecimal(dt.Rows[iItens]["FATORCONVERSAO"]);
                    itens.QUANTIDADECONTROLE = Convert.ToDecimal(dt.Rows[iItens]["QUANTIDADECONTROLE"]);

                    listaItens.Add(itens);
                }
            }
            return listaItens;
        }

        /// <summary>
        /// Método para buscar os itens da operação
        /// </summary>
        /// <param name="CodOper">Código da Operação</param>
        /// <returns>Lista dos Itens referentes a operação</returns>
        public List<Class.GoperItem> getGoperItem(int CodOper, AppLib.Data.Connection conn)
        {
            List<Class.GoperItem> listaItens = new List<Class.GoperItem>();
            DataTable dt = conn.ExecQuery("SELECT * FROM GOPERITEM WHERE CODOPER = ? AND CODEMPRESA = ? ", new object[] { CodOper, AppLib.Context.Empresa });
            for (int iItens = 0; iItens < dt.Rows.Count; iItens++)
            {
                Class.GoperItem itens = new Class.GoperItem();
                itens.CODOPER = Convert.ToInt32(dt.Rows[iItens]["CODOPER"]);
                itens.CODEMPRESA = Convert.ToInt32(dt.Rows[iItens]["CODEMPRESA"]);
                //itens.NUMERO = dt.Rows[iItens]["NUMERO"].ToString();
                //itens.CODIGOAUXILIAR = dt.Rows[iItens]["CODIGOAUXILIAR"].ToString();
                itens.CAMPOLIVRE1 = dt.Rows[iItens]["CAMPOLIVRE1"].ToString();
                itens.CAMPOLIVRE2 = dt.Rows[iItens]["CAMPOLIVRE2"].ToString();
                itens.CAMPOLIVRE3 = dt.Rows[iItens]["CAMPOLIVRE3"].ToString();
                itens.CAMPOLIVRE4 = dt.Rows[iItens]["CAMPOLIVRE4"].ToString();
                itens.CAMPOLIVRE5 = dt.Rows[iItens]["CAMPOLIVRE5"].ToString();
                itens.CAMPOLIVRE6 = dt.Rows[iItens]["CAMPOLIVRE6"].ToString();
                itens.CAMPOVALOR1 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR1"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR1"]);
                itens.CAMPOVALOR2 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR2"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR2"]);
                itens.CAMPOVALOR3 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR3"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR3"]);
                itens.CAMPOVALOR4 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR4"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR4"]);
                itens.CAMPOVALOR5 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR5"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR5"]);
                itens.CAMPOVALOR6 = string.IsNullOrEmpty(dt.Rows[0]["CAMPOVALOR6"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["CAMPOVALOR6"]);
                //itens.DESCRICAOPRODUTO = dt.Rows[iItens]["DESCRICAO"].ToString();
                itens.CODCFOP = dt.Rows[iItens]["CODCFOP"].ToString();
                itens.CODNATUREZA = dt.Rows[iItens]["CODNATUREZA"].ToString();
                itens.CODPRODUTO = dt.Rows[iItens]["CODPRODUTO"].ToString();
                itens.CODTABPRECO = dt.Rows[iItens]["CODTABPRECO"].ToString();
                itens.CODUNIDOPER = dt.Rows[iItens]["CODUNIDOPER"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA1"].ToString()))
                {
                    itens.DATAEXTRA1 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA1"]);
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA2"].ToString()))
                {
                    itens.DATAEXTRA2 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA2"]);
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA3"].ToString()))
                {
                    itens.DATAEXTRA3 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA3"]);
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA4"].ToString()))
                {
                    itens.DATAEXTRA4 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA4"]);
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA5"].ToString()))
                {
                    itens.DATAEXTRA5 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA5"]);
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEXTRA6"].ToString()))
                {
                    itens.DATAEXTRA6 = Convert.ToDateTime(dt.Rows[0]["DATAEXTRA6"]);
                }
                itens.INFCOMPL = dt.Rows[iItens]["INFCOMPL"].ToString();
                itens.NSEQITEM = Convert.ToInt32(dt.Rows[iItens]["NSEQITEM"]);
                itens.OBSERVACAO = dt.Rows[iItens]["OBSERVACAO"].ToString();
                itens.PRDESCONTO = Convert.ToDecimal(dt.Rows[iItens]["PRDESCONTO"]);
                itens.QTD_ORIGINAL = Convert.ToDecimal(dt.Rows[iItens]["QUANTIDADE"]);
                itens.VLDESCONTO = Convert.ToDecimal(dt.Rows[iItens]["VLDESCONTO"]);
                itens.VLTOTALITEM = Convert.ToDecimal(dt.Rows[iItens]["VLTOTALITEM"]);
                itens.VLUNITARIO = Convert.ToDecimal(dt.Rows[iItens]["VLUNITARIO"]);
                itens.QTD_FATURAR = Convert.ToDecimal(dt.Rows[iItens]["QUANTIDADE_SALDO"]);
                itens.QTD_SALDO = Convert.ToDecimal(dt.Rows[iItens]["QUANTIDADE_SALDO"]);
                itens.VLUNITORIGINAL = Convert.ToDecimal(dt.Rows[iItens]["VLUNITORIGINAL"]);
                itens.PRACRESCIMO = Convert.ToDecimal(dt.Rows[iItens]["PRACRESCIMO"]);
                itens.VLACRESCIMO = Convert.ToDecimal(dt.Rows[iItens]["VLACRESCIMO"]);
                itens.QUANTIDADE = Convert.ToDecimal(dt.Rows[iItens]["QUANTIDADE"]);
                itens.CODUNIDOPER = dt.Rows[iItens]["CODUNIDOPER"].ToString();
                if (dt.Rows[iItens]["DATAENTREGA"].ToString() != string.Empty)
                {
                    itens.DATAENTREGA = Convert.ToDateTime(dt.Rows[iItens]["DATAENTREGA"]);
                }
                else
                {
                    itens.DATAENTREGA = null;
                }

                itens.APLICACAOMATERIAL = dt.Rows[iItens]["APLICACAOMATERIAL"].ToString();
                itens.TIPODESCONTO = dt.Rows[iItens]["TIPODESCONTO"].ToString();
                itens.TOTALEDITADO = Convert.ToInt32(dt.Rows[iItens]["TOTALEDITADO"]);

                itens.CODUNIDCONTROLE = dt.Rows[iItens]["CODUNIDCONTROLE"].ToString();
                itens.FATORCONVERSAO = Convert.ToDecimal(dt.Rows[iItens]["FATORCONVERSAO"]);
                itens.QUANTIDADECONTROLE = Convert.ToDecimal(dt.Rows[iItens]["QUANTIDADECONTROLE"]);

                listaItens.Add(itens);
            }
            return listaItens;
        }


        /// <summary>
        /// Método para inserir os itens
        /// </summary>
        /// <param name="item">Objeto Item</param>
        /// <param name="conn">Conexão</param>
        /// <returns></returns>
        public bool setItem(GoperItem item, AppLib.Data.Connection conn, int codOper)
        {
            AppLib.ORM.Jit GOPERITEM = new AppLib.ORM.Jit(conn, "GOPERITEM");
            item.CODOPER = codOper;

            // João Pedro Luchiari - 23/11/2017 - Setado o campo NSEQITEM como 1.
            //item.NSEQITEM = 1;

            try
            {

                GOPERITEM.Set("CODEMPRESA", item.CODEMPRESA);
                GOPERITEM.Set("CODOPER", item.CODOPER);

                item.NSEQITEM = setNseqItem(conn, codOper);

                GOPERITEM.Set("NSEQITEM", item.NSEQITEM);

                GOPERITEM.Set("NSEQITEM", item.NSEQITEM);
                GOPERITEM.Set("CODPRODUTO", item.CODPRODUTO);
                GOPERITEM.Set("QUANTIDADE", item.QUANTIDADE);
                GOPERITEM.Set("VLUNITARIO", item.VLUNITARIO);
                GOPERITEM.Set("VLDESCONTO", item.VLDESCONTO);
                GOPERITEM.Set("PRDESCONTO", item.PRDESCONTO);
                GOPERITEM.Set("VLTOTALITEM", item.VLTOTALITEM);
                GOPERITEM.Set("QUANTIDADE_SALDO", item.QUANTIDADE_SALDO);
                GOPERITEM.Set("CODNATUREZA", item.CODNATUREZA == string.Empty ? null : item.CODNATUREZA);
                GOPERITEM.Set("CODUNIDOPER", item.CODUNIDOPER);
                GOPERITEM.Set("OBSERVACAO", item.OBSERVACAO);
                GOPERITEM.Set("INFCOMPL", item.INFCOMPL);
                GOPERITEM.Set("CODTABPRECO", item.CODTABPRECO);
                GOPERITEM.Set("NOMEPRODUTO", item.NOMEPRODUTO);
                GOPERITEM.Set("APLICACAOMATERIAL", item.APLICACAOMATERIAL);
                GOPERITEM.Set("VLACRESCIMO", item.VLACRESCIMO);
                GOPERITEM.Set("PRACRESCIMO", item.PRACRESCIMO);
                GOPERITEM.Set("TIPODESCONTO", item.TIPODESCONTO);
                GOPERITEM.Set("VLUNITORIGINAL", item.VLUNITORIGINAL);
                GOPERITEM.Set("RATEIODESPESA", item.RATEIODESPESA);
                GOPERITEM.Set("RATEIODESCONTO", item.RATEIODESCONTO);
                GOPERITEM.Set("RATEIOFRETE", item.RATEIOFRETE);
                GOPERITEM.Set("RATEIOSEGURO", item.RATEIOSEGURO);
                GOPERITEM.Set("CAMPOVALOR1", item.CAMPOVALOR1);
                GOPERITEM.Set("CAMPOVALOR2", item.CAMPOVALOR2);
                GOPERITEM.Set("CAMPOVALOR3", item.CAMPOVALOR3);
                GOPERITEM.Set("CAMPOVALOR4", item.CAMPOVALOR4);
                GOPERITEM.Set("XPED", item.XPED);
                GOPERITEM.Set("NITEMPED", item.NITEMPED);
                GOPERITEM.Set("DATAENTREGA", item.DATAENTREGA);
                GOPERITEM.Set("TOTALEDITADO", item.TOTALEDITADO);

                #region Importação

                GOPERITEM.Set("NUMERODI", item.NUMERODI);
                GOPERITEM.Set("DATADI", item.DATADI);
                GOPERITEM.Set("DATADESEMB", item.DATADESEMB);
                GOPERITEM.Set("LOCDESEMB", item.LOCDESEMB);
                GOPERITEM.Set("UFDESEMB", item.UFDESEMB);
                GOPERITEM.Set("CODEXPORTADOR", item.CODEXPORTADOR);
                GOPERITEM.Set("NUMADICAO", item.NUMADICAO);
                GOPERITEM.Set("NUMSEQADIC", item.NUMSEQADIC);
                GOPERITEM.Set("CODFABRICANTE", item.CODFABRICANTE);
                GOPERITEM.Set("VLORDESCDI", item.VLORDESCDI);
                GOPERITEM.Set("TPVIATRANSP", item.TPVIATRANSP);
                GOPERITEM.Set("VAFRMM", item.VAFRMM);
                GOPERITEM.Set("TPINTERMEDIO", item.TPINTERMEDIO);
                GOPERITEM.Set("CNPJ", item.CNPJ);
                GOPERITEM.Set("UFTERCEIRO", item.UFTERCEIRO);
                GOPERITEM.Set("NDRAW", item.NDRAW);

                #endregion

                #region IBPTAX

                item.IBPTAX = new IBPTAX().getIBPTAXItem(conn, item.CODOPER, item.CODEMPRESA, item.NSEQITEM);

                GOPERITEM.Set("UFIBPTAX", item.IBPTAX.UF);
                GOPERITEM.Set("NACIONALFEDERALIBPTAX", Convert.ToDecimal(item.IBPTAX.NACIONALFEDERAL));
                GOPERITEM.Set("IMPORTADOSFEDERALIBPTAX", item.IBPTAX.IMPORTADOSFEDERAL);
                GOPERITEM.Set("ESTADUALIBPTAX", item.IBPTAX.ESTADUAL);
                GOPERITEM.Set("MUNICIPALIBPTAX", item.IBPTAX.MUNICIPAL);
                GOPERITEM.Set("CHAVEIBPTAX", item.IBPTAX.CHAVE);

                #region Unidade de Medida

                if (item.CODUNIDCONTROLE == null)
                {
                    GOPERITEM.Set("CODUNIDCONTROLE", item.CODUNIDOPER);
                    CODUNIDCONTROLE = CODUNIDOPER;
                }
                else
                {
                    GOPERITEM.Set("CODUNIDCONTROLE", item.CODUNIDCONTROLE);
                }

                if (item.FATORCONVERSAO == 0)
                {
                    CODTIPOPER = conn.ExecGetField(string.Empty, "SELECT CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, item.CODOPER }).ToString();

                    string BuscaFator = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT FATORCONVERSAO FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { CODTIPOPER , AppLib.Context.Empresa }).ToString();

                    if (BuscaFator == "PADRAO")
                    {
                        decimal Fator = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT FATORCONVERSAO FROM VUNIDCONVERSAO WHERE CODEMPRESA = ? AND CODUNID = ? AND CODUNIDCONVERSAO = ?", new object[] { AppLib.Context.Empresa, item.CODUNIDOPER, item.CODUNIDCONTROLE }));
                        item.FATORCONVERSAO = Fator;
                        GOPERITEM.Set("FATORCONVERSAO", item.FATORCONVERSAO);
                    }
                    else if (BuscaFator == "PRODUTO")
                    {
                        decimal Fator = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT FATORCONVERSAO FROM VPRODUTOUNIDADE WHERE CODEMPRESA = ? AND CODUNID = ? AND CODUNIDCONVERSAO = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, item.CODUNIDOPER, item.CODUNIDCONTROLE, item.CODPRODUTO }));
                        item.FATORCONVERSAO = Fator;
                        GOPERITEM.Set("FATORCONVERSAO", item.FATORCONVERSAO);
                    }
                }
                else
                {
                    GOPERITEM.Set("FATORCONVERSAO", item.FATORCONVERSAO);
                }

                decimal ResultQtd = item.QUANTIDADE * item.FATORCONVERSAO;
                item.QUANTIDADECONTROLE = ResultQtd;
                GOPERITEM.Set("QUANTIDADECONTROLE", item.QUANTIDADECONTROLE);

                #endregion

                #endregion

                GOPERITEM.Insert();

                //AppLib.Data.Connection conexao = AppLib.Context.poolConnection.Get("Start").Clone();
                //AppLib.ORM.Jit GOPER = new AppLib.ORM.Jit(conexao, "GOPER");

                //string codTipOper = conn.ExecGetField(string.Empty, "SELECT CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CODOPER }).ToString();

                //string tipoEstoque = conn.ExecGetField("N", "SELECT OPERESTOQUE FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codTipOper, AppLib.Context.Empresa }).ToString();
                //if (tipoEstoque != "N")
                //{
                //    PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                //    psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
                //    psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };

                //    PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque Estoque;
                //    Estoque = PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao;

                //    DataTable dtLocal = conn.ExecQuery(@"SELECT CODLOCAL, CODLOCALENTREGA FROM GOPER WHERE CODOPER = ? AND CODEMPRESA =? ", new object[] { CODOPER, AppLib.Context.Empresa });

                //    //psPartLocalEstoqueSaldoData.MovimentaEstoque(AppLib.Context.Empresa, AppLib.Context.Filial, dtLocal.Rows[0]["CODLOCAL"].ToString(), dtLocal.Rows[0]["CODLOCALENTREGA"].ToString(), GOPERITEM.Get("CODPRODUTO").ToString(), Convert.ToDecimal(GOPERITEM.Get("QUANTIDADE")), GOPERITEM.Get("CODUNIDOPER").ToString(), PSPartLocalEstoqueSaldoData.Tipo.Diminui);
                //    psPartLocalEstoqueSaldoData.MovimentaEstoque2(Estoque, AppLib.Context.Empresa, CODOPER, NSEQITEM);

                //}

                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="Itens"></param>
        /// <returns></returns>
        private int setNseqItem(AppLib.Data.Connection conn, int _Codoper)
        {
            int Nseqitem = Convert.ToInt32(conn.ExecGetField(0, "SELECT ISNULL(MAX(NSEQITEM), 0) + 1 FROM GOPERITEM WHERE CODOPER = ?", new object[] { _Codoper }));
            return Nseqitem;
        }
    }
}
