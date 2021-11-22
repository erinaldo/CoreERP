using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace PS.Glb.Class
{
    public class VFICHAESTOQUELOTE
    {
        #region Propriedades

        public int CODEMPRESA { get; set; }
        public int CODFILIAL { get; set; }
        public string CODLOCAL { get; set; }
        public string CODPRODUTO { get; set; }
        public int CODOPER { get; set; }
        public int NSEQITEM { get; set; }
        public int CODLOTE { get; set; }
        public int SEQUENCIAL { get; set; }
        public DateTime? DATAENTSAI { get; set; }
        public string CODUNIDCONTROLE { get; set; }
        public decimal SALDOANTERIOR { get; set; }
        public decimal QUANTIDADEENTRADA { get; set; }
        public decimal QUANTIDADESAIDA { get; set; }
        public decimal QUANTIDADESINAL { get; set; }
        public decimal SALDOFINAL { get; set; }

        #endregion

        #region Variáveis

        public DateTime DataEntSai;
        public string OperEstoque = string.Empty;
        List<VFICHAESTOQUELOTE> ListFICHALOTE = new List<VFICHAESTOQUELOTE>();

        #endregion

        #region Construtor

        public VFICHAESTOQUELOTE() { }

        #endregion

        #region Métodos 

        /// <summary>
        /// Método para buscar os itens na GOPERITEMLOTE
        /// </summary>
        /// <param name="CodEmpresa">Código da Empresa</param>
        /// <param name="CodFilial">Código da Filial</param>
        /// <param name="CodLocal">Código do Local</param>
        /// <param name="CodOper">Código da Operação</param>
        /// <param name="Nseqitem">Número Sequencial do item</param>
        /// <param name="CodProduto">Código do Produto</param>
        /// <param name="conn">Conexão Ativa</param>
        /// <returns>Lista do itens referentes ao Lote na tabela GOPERITEMLOTE</returns>
        public List<VFICHAESTOQUELOTE> getValoresItemLote(int CodEmpresa, int CodFilial, string CodLocal, int CodOper, int Nseqitem, string CodProduto, AppLib.Data.Connection conn)
        {
            DataTable dt = conn.ExecQuery(@"SELECT GOPERITEMLOTE.CODEMPRESA, GOPERITEMLOTE.CODFILIAL, GOPERITEMLOTE.CODLOCAL, GOPERITEMLOTE.CODLOTE, GOPERITEMLOTE.CODOPER, GOPERITEMLOTE.NSEQITEM, GOPERITEMLOTE.CODPRODUTO, GOPERITEMLOTE.CODUNIDCONTROLE, GOPERITEMLOTE.QUANTIDADE
                                            FROM GOPERITEMLOTE
                                            WHERE GOPERITEMLOTE.CODEMPRESA = ? AND GOPERITEMLOTE.CODFILIAL = ? AND GOPERITEMLOTE.CODLOCAL = ? AND GOPERITEMLOTE.CODOPER = ? AND GOPERITEMLOTE.NSEQITEM = ? AND CODPRODUTO = ?", new object[] { CodEmpresa, CodFilial, CodLocal, CodOper, Nseqitem, CodProduto });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                VFICHAESTOQUELOTE Lote = new VFICHAESTOQUELOTE();
                Lote.CODEMPRESA = Convert.ToInt32(dt.Rows[i]["CODEMPRESA"]);
                Lote.CODFILIAL = Convert.ToInt32(dt.Rows[i]["CODFILIAL"]);
                Lote.CODLOCAL = dt.Rows[i]["CODLOCAL"].ToString();
                Lote.CODLOTE = Convert.ToInt32(dt.Rows[i]["CODLOTE"]);
                Lote.CODOPER = Convert.ToInt32(dt.Rows[i]["CODOPER"]);
                Lote.NSEQITEM = Convert.ToInt32(dt.Rows[i]["NSEQITEM"]);
                Lote.CODPRODUTO = dt.Rows[i]["CODPRODUTO"].ToString();
                Lote.CODUNIDCONTROLE = dt.Rows[i]["CODUNIDCONTROLE"].ToString();

                if (OperEstoque == "A")
                {
                    Lote.QUANTIDADEENTRADA = Convert.ToDecimal(dt.Rows[i]["QUANTIDADE"]);

                }
                if (OperEstoque == "D")
                {
                    Lote.QUANTIDADESAIDA = Convert.ToDecimal(dt.Rows[i]["QUANTIDADE"]);
                }

                Lote.SEQUENCIAL = setSequencial(CodEmpresa, CodFilial, CodLocal, Lote.CODLOTE, CodProduto, conn);
                Lote.DATAENTSAI = DataEntSai;
                Lote.SALDOANTERIOR = setSaldoAnteriror(CodEmpresa, CodFilial, CodLocal, Lote.CODLOTE, CodProduto, conn);

                ListFICHALOTE.Add(Lote);
            }

            return ListFICHALOTE;
        }

        /// <summary>
        /// Método para buscar o Sequencial.
        /// </summary>
        /// <param name="Codempresa">Código da Empresa</param>
        /// <param name="Codfilial">Código da Filial</param>
        /// <param name="Codlocal">Código do Local</param>
        /// <param name="Codlote">Código do Lote</param>
        /// <param name="Codproduto">Código do Produto</param>
        /// <param name="conn">Conexão ativa</param>
        /// <returns>Retorna o campo do Sequencial</returns>
        private int setSequencial(int Codempresa, int Codfilial, string Codlocal, int Codlote, string Codproduto, AppLib.Data.Connection conn)
        {
            int Seq = Convert.ToInt32(conn.ExecGetField(0, "SELECT (ISNULL(MAX(SEQUENCIAL), 0) + 1) FROM VFICHAESTOQUELOTE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODLOTE = ? AND CODPRODUTO = ? ", new object[] { AppLib.Context.Empresa, Codfilial, Codlocal, Codlote, Codproduto }));
            return Seq;
        }

        /// <summary>
        /// Método para buscar o Saldo Anterior.
        /// </summary>
        /// <param name="Codempresa">Código da Empresa</param>
        /// <param name="Codfilial">Código da Filial</param>
        /// <param name="Codlocal">Código do Local</param>
        /// <param name="Codlote">Código do Lote</param>
        /// <param name="Codproduto">Código do Produto</param>
        /// <param name="conn">Conexão ativa</param>
        /// <returns>Retorna o campo do Saldo Anterior</returns>
        private decimal setSaldoAnteriror(int Codempresa, int Codfilial, string Codlocal, int Codlote, string Codproduto, AppLib.Data.Connection conn)
        {
            decimal Saldo = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ISNULL (SALDOFINAL, 0) SALDOFINAL
                                                                        FROM VFICHAESTOQUELOTE
                                                                        WHERE CODEMPRESA = ? 
	                                                                    AND CODFILIAL = ? 
	                                                                    AND CODLOCAL = ?
	                                                                    AND CODLOTE = ?
	                                                                    AND CODPRODUTO = ? 
	                                                                    AND SEQUENCIAL = (SELECT MAX(SEQUENCIAL)
						                                                                    FROM VFICHAESTOQUELOTE
						                                                                    WHERE CODEMPRESA = ? 
							                                                                    AND CODFILIAL = ? 
							                                                                    AND CODLOCAL = ? 
							                                                                    AND CODLOTE = ?
							                                                                    AND CODPRODUTO = ? )", new object[] {  Codempresa, Codfilial, Codlocal, Codlote, Codproduto, Codempresa, Codfilial, Codlocal, Codlote, Codproduto }));
            return Saldo;
        }

        /// <summary>
        /// Método para inserir os registros na tabela VFICHAESTOQUELOTE.
        /// </summary>
        /// <param name="ItensLote">Lista dos itens da GOPERITEMLOTE</param>
        /// <param name="conn">Conexão Ativa</param>
        /// <returns></returns>
        public bool setVFICHAESTOQUELOTE(List<VFICHAESTOQUELOTE> ItensLote, AppLib.Data.Connection conn)
        {
            try
            {
                for (int i = 0; i < ItensLote.Count; i++)
                {
                    int ContadorFicha = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODPRODUTO) FROM VFICHAESTOQUELOTE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODLOTE = ? AND CODPRODUTO = ? AND NSEQITEM = ? ", new object[] { AppLib.Context.Empresa, ItensLote[i].CODFILIAL, ItensLote[i].CODLOCAL, ItensLote[i].CODLOTE, ItensLote[i].CODPRODUTO, ItensLote[i].NSEQITEM }));

                    AppLib.ORM.Jit VFICHAESTOQUELOTE = new AppLib.ORM.Jit(conn, "VFICHAESTOQUELOTE");

                    VFICHAESTOQUELOTE.Set("CODEMPRESA", ItensLote[i].CODEMPRESA);
                    VFICHAESTOQUELOTE.Set("CODFILIAL", ItensLote[i].CODFILIAL);
                    VFICHAESTOQUELOTE.Set("CODLOCAL", ItensLote[i].CODLOCAL);
                    VFICHAESTOQUELOTE.Set("CODPRODUTO", ItensLote[i].CODPRODUTO);
                    VFICHAESTOQUELOTE.Set("CODOPER", ItensLote[i].CODOPER);
                    VFICHAESTOQUELOTE.Set("NSEQITEM", ItensLote[i].NSEQITEM);
                    VFICHAESTOQUELOTE.Set("CODLOTE", ItensLote[i].CODLOTE);
                    VFICHAESTOQUELOTE.Set("SEQUENCIAL", ItensLote[i].SEQUENCIAL);
                    VFICHAESTOQUELOTE.Set("DATAENTSAI", ItensLote[i].DATAENTSAI);
                    VFICHAESTOQUELOTE.Set("CODUNIDCONTROLE", ItensLote[i].CODUNIDCONTROLE);
                    VFICHAESTOQUELOTE.Set("SALDOANTERIOR", ItensLote[i].SALDOANTERIOR);
                    VFICHAESTOQUELOTE.Set("QUANTIDADEENTRADA", ItensLote[i].QUANTIDADEENTRADA);
                    VFICHAESTOQUELOTE.Set("QUANTIDADESAIDA", ItensLote[i].QUANTIDADESAIDA);

                    if (ContadorFicha > 0)
                    {
                        VFICHAESTOQUELOTE.Save();
                    }
                    else
                    {
                        VFICHAESTOQUELOTE.Insert();
                    }                  
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema,", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        #endregion
    }
}
