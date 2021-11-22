using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PS.Glb.Class
{
    public class IBPTAX
    {
        #region Construtor
        public IBPTAX() { }
        #endregion

        #region Propriedades

        public string ARQUIVO { get; set; }
        public string UF { get; set; }
        public int LINHA { get; set; }
        public string CODIGO { get; set; }
        public int EX { get; set; }
        public string TIPO { get; set; }
        public string DESCRICAO { get; set; }
        public string NACIONALFEDERAL { get; set; }
        public decimal IMPORTADOSFEDERAL { get; set; }
        public decimal ESTADUAL { get; set; }
        public decimal MUNICIPAL { get; set; }
        public DateTime VIGENCIAINICIO { get; set; }
        public DateTime VIGENCIAFIM { get; set; }
        public string CHAVE { get; set; }
        public string VERSAO { get; set; }
        public string FONTE { get; set; }

        #endregion

        public IBPTAX getIBPTAXItem(AppLib.Data.Connection conn, int codoper, int codempresa, int nseqitem)
        {
            IBPTAX _ibptax = new IBPTAX();

            try
            {
                DataTable dtIBPTAX = conn.ExecQuery(@"SELECT * FROM VIBPTAX WHERE UF = (SELECT GFILIAL.CODETD FROM GFILIAL INNER JOIN GOPER ON GFILIAL.CODFILIAL = GOPER.CODFILIAL AND GFILIAL.CODEMPRESA = GOPER.CODEMPRESA WHERE GOPER.CODOPER = ?) AND CODIGO = (SELECT CODNCM FROM VPRODUTO INNER JOIN GOPERITEM ON VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO AND VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODOPER = ? AND GOPERITEM.NSEQITEM = ?)", new object[] { codoper, codempresa, codoper, nseqitem });
                if (dtIBPTAX.Rows.Count > 0)
                {
                    _ibptax.ARQUIVO = dtIBPTAX.Rows[0]["ARQUIVO"].ToString();
                    _ibptax.UF = dtIBPTAX.Rows[0]["UF"].ToString();
                    _ibptax.LINHA = Convert.ToInt32(dtIBPTAX.Rows[0]["LINHA"]);
                    _ibptax.CODIGO = dtIBPTAX.Rows[0]["CODIGO"].ToString();
                    _ibptax.EX = Convert.ToInt32(dtIBPTAX.Rows[0]["EX"]);
                    _ibptax.TIPO = dtIBPTAX.Rows[0]["TIPO"].ToString();
                    _ibptax.DESCRICAO = dtIBPTAX.Rows[0]["DESCRICAO"].ToString();
                    _ibptax.NACIONALFEDERAL = dtIBPTAX.Rows[0]["NACIONALFEDERAL"].ToString();
                    _ibptax.IMPORTADOSFEDERAL = Convert.ToDecimal(dtIBPTAX.Rows[0]["IMPORTADOSFEDERAL"]);
                    _ibptax.ESTADUAL = Convert.ToDecimal(dtIBPTAX.Rows[0]["ESTADUAL"]);
                    _ibptax.MUNICIPAL = Convert.ToDecimal(dtIBPTAX.Rows[0]["MUNICIPAL"]);
                    _ibptax.VIGENCIAINICIO = Convert.ToDateTime(dtIBPTAX.Rows[0]["VIGENCIAINICIO"]);
                    _ibptax.VIGENCIAFIM = Convert.ToDateTime(dtIBPTAX.Rows[0]["VIGENCIAFIM"]);
                    _ibptax.CHAVE = dtIBPTAX.Rows[0]["CHAVE"].ToString();
                    _ibptax.VERSAO = dtIBPTAX.Rows[0]["VERSAO"].ToString();
                    _ibptax.FONTE = dtIBPTAX.Rows[0]["FONTE"].ToString();

                }
                return _ibptax;

            }
            catch (Exception)
            {
                _ibptax.CODIGO = string.Empty;
                return _ibptax;
            }
        }
    }
}
