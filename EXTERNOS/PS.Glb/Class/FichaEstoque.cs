using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PS.Glb.Class
{
    public class FichaEstoque
    {
        #region Construtor

        public FichaEstoque() { }

        #endregion

        #region Propriedades

        public string CODTIPOPER { get; set; }

        public int CODOPER { get; set; }

        #endregion

        #region Variáveis

        public enum Processo
        {
            Copia,
            Faturamento
        }

        private DataTable dtItens;

        #endregion

        public void ExecutaProcessoEstoque(Processo processo)
        {
            switch (processo)
            {
                case Processo.Copia:

                    if (ValidaOperEstoque(CODTIPOPER) == false)
                    {
                        return;
                    }

                    if (ValidaItensOperacao(CODOPER) == false)
                    {
                        return;
                    }
                    else
                    {
                        dtItens = new DataTable();

                        dtItens = getItensOperacao(CODOPER);

                        try
                        {
                            PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                            PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque Estoque;
                            Estoque = PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao;

                            for (int i = 0; i < dtItens.Rows.Count; i++)
                            {
                                psPartLocalEstoqueSaldoData.MovimentaEstoque2(Estoque, AppLib.Context.Empresa, CODOPER, Convert.ToInt32(dtItens.Rows[i]["NSEQITEM"]));
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }

                    break;
                case Processo.Faturamento:

                    if (ValidaOperEstoque(CODTIPOPER) == false)
                    {
                        return;
                    }

                    if (ValidaItensOperacao(CODOPER) == false)
                    {
                        return;
                    }
                    else
                    {
                        dtItens = new DataTable();

                        dtItens = getItensOperacao(CODOPER);

                        try
                        {
                            PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                            PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque Estoque;
                            Estoque = PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao;

                            for (int i = 0; i < dtItens.Rows.Count; i++)
                            {
                                psPartLocalEstoqueSaldoData.MovimentaEstoque2(Estoque, AppLib.Context.Empresa, CODOPER, Convert.ToInt32(dtItens.Rows[i]["NSEQITEM"]));
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        private bool ValidaOperEstoque(string codTipOper)
        {
            string OperEstoque = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT OPERESTOQUE FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper }).ToString();

            if (OperEstoque == "N")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidaItensOperacao(int codoper)
        {
            int itens = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODPRODUTO) FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codoper }));

            if (itens > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private DataTable getItensOperacao(int codoper)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CODOPER });

            return dt;
        }
    }
}
