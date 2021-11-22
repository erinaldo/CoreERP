using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Processos.Financeiro
{
    public partial class frmFaturaFinanceiro : Form
    {
        List<DataRow> listaRow = new List<DataRow>();
        private decimal vlOriginal = 0;

        public frmFaturaFinanceiro(List<DataRow> _listaRow)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "FLANCA");

            psLookup13.PSPart = "PSPartCentroCusto";
            psLookup16.PSPart = "PSPartNaturezaOrcamento";
            psLookup5.PSPart = "PSPartTipDoc";

            listaRow = _listaRow;
            if (validaDados() == false)
            {
                this.Dispose();
            }
            else
            {
                carregaDados();
            }
        }

        #region Validação

        private bool validaDados()
        {
            try
            {
                #region Validações
                //Validações
                /*
                 * Fábio Campos 20/04/2016
                 * ************************************************************************************************
                 */
                vlOriginal = 0;
                for (int i = 0; i < listaRow.Count; i++)
                {
                    //DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODCLIFOR, TIPOPAGREC FROM FLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { ListaRow[i]["CODLANCA"].ToString(), AppLib.Context.Empresa });
                    //Verifica se é o mesmo cliente.
                    if (listaRow[0]["FLANCA.CODCLIFOR"].ToString() != listaRow[i]["FLANCA.CODCLIFOR"].ToString())
                    {
                        MessageBox.Show("Permitido apenas para lançamentos do mesmo cliente/fornecedor.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                        return false;
                    }
                    //Verifica se é o mesmo tipo de pagamento.
                    if (listaRow[0]["FLANCA.TIPOPAGREC"].ToString() != listaRow[i]["FLANCA.TIPOPAGREC"].ToString())
                    {
                        MessageBox.Show("Não pode misturar lançamentos de PAGAR/RECEBER ao gerar fatura.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                        return false;
                    }
                    //Verfica se os lançamentos estão em abertos.
                    if (verificaStatusLancamento(listaRow[i]["FLANCA.CODLANCA"].ToString()) == false)
                    {
                        MessageBox.Show("Permitido gerar faturas apenas para lançamentos em aberto.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    //Verifica se o item não é fatura ou faturado.
                    if (verificaItemFaturado(listaRow[i]["FLANCA.CODLANCA"].ToString()) == false)
                    {
                        MessageBox.Show("Não pode gerar fatura de fatura.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    //Verifica se os lançamentos não são de previsão
                    if (verificaClassificacao(listaRow[i]["FLANCA.CODLANCA"].ToString()) == false)
                    {
                        MessageBox.Show("Não é permitido gerar faturas de lançamentos de previsão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    vlOriginal += Convert.ToDecimal(listaRow[i]["FLANCA.VLLIQUIDO"]);
                }
                //**************************************************************************************************
                #endregion
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica o status do NFOUDUP, retorna true para NULL
        /// </summary>
        /// <param name="codlanca">Número do Lançamento</param>
        /// <returns>True ou False</returns>
        private bool verificaItemFaturado(string codlanca)
        {
            try
            {
                string retorno = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODLANCA FROM FLANCA WHERE CODFATURA = ? AND CODEMPRESA = ?", new object[] { codlanca, AppLib.Context.Empresa }).ToString();
                if (string.IsNullOrEmpty(retorno))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Verfica se o lançamento não está baixado, retorna true somente para lançamento em aberto.
        /// </summary>
        /// <param name="codLanca">Número do Lançamento</param>
        /// <returns>True ou False</returns>
        private bool verificaStatusLancamento(string codLanca)
        {
            try
            {
                string codStatus = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODSTATUS FROM FLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { codLanca, AppLib.Context.Empresa }).ToString();
                if (codStatus.Equals("0"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Verifica a classificação - Retorna true para resultado diferente de 3 (PVV)
        /// </summary>
        /// <param name="codLanca">Número do Lançamento</param>
        /// <returns>True ou False</returns>
        private bool verificaClassificacao(string codLanca)
        {
            try
            {
                string codClassificacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT FTIPDOC.CLASSIFICACAO FROM FLANCA INNER JOIN FTIPDOC ON FLANCA.CODEMPRESA = FTIPDOC.CODEMPRESA AND FLANCA.CODTIPDOC = FTIPDOC.CODTIPDOC where FLANCA.CODLANCA = ? AND FLANCA.CODEMPRESA = ?", new object[] { codLanca, AppLib.Context.Empresa }).ToString();
                if (!codClassificacao.Equals("3"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        private void carregaDados()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT DOCDEFAULTFATR, DOCDEFAULTFATP FROM VPARAMETROS WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(listaRow[0]["FLANCA.TIPOPAGREC"]) == 1)
                {
                    psLookup5.textBox1.Text = dt.Rows[0]["DOCDEFAULTFATR"].ToString();
                }
                else
                {
                    psLookup5.textBox1.Text = dt.Rows[0]["DOCDEFAULTFATP"].ToString();
                }
                psLookup5.LoadLookup();
            }
            dtDataVencimento.Text = AppLib.Context.poolConnection.Get("Start").GetDateTime().ToShortDateString();
            txtVlOriginal.Text = string.Format("{0:n2}", vlOriginal);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();
            try
            {
                AppLib.ORM.Jit primeiroRegistro = new AppLib.ORM.Jit(conn, "FLANCA");
                primeiroRegistro.Set("CODEMPRESA", AppLib.Context.Empresa);
                primeiroRegistro.Set("CODLANCA", listaRow[0]["FLANCA.CODLANCA"].ToString());
                primeiroRegistro.Select();

                AppLib.ORM.Jit fatura = new AppLib.ORM.Jit(conn, "FLANCA");
                fatura.Set("CODEMPRESA", AppLib.Context.Empresa);

                String consultaCODLANCA = @"SELECT IDLOG FROM GLOG WHERE CODEMPRESA = ? AND CODTABELA = ?";
                int ULTIMO_CODLANCA = int.Parse(conn.ExecGetField(0, consultaCODLANCA, new Object[] { AppLib.Context.Empresa, "FLANCA" }).ToString());
                int proximo_CODLANCA = (ULTIMO_CODLANCA + 1);
                String comandoCODLANCA = "UPDATE GLOG SET IDLOG = ? WHERE CODEMPRESA = ? AND CODTABELA = ?";
                if (conn.ExecTransaction(comandoCODLANCA, new Object[] { proximo_CODLANCA, AppLib.Context.Empresa, "FLANCA" }) == 1)
                {
                    fatura.Set("CODLANCA", proximo_CODLANCA);
                }
                else
                {
                    throw new Exception("Erro ao atualizar IDLOG na tabela GLOG.");
                }

                fatura.Set("TIPOPAGREC", primeiroRegistro.Get("TIPOPAGREC"));

                String consultaMascara = "SELECT MASKNUMEROSEQ FROM VPARAMETROS WHERE CODEMPRESA = ?";
                int MASKNUMEROSEQ = int.Parse(conn.ExecGetField(0, consultaMascara, new Object[] { AppLib.Context.Empresa }).ToString());
                //String consultaUltimoNumero = "SELECT MAX(NUMERO) FROM FLANCA WHERE CODEMPRESA = ? AND CODTIPDOC = ?";
                //int ultimoNumero = int.Parse(conn.ExecGetField(0, consultaUltimoNumero, new Object[] { AppLib.Context.Empresa, psLookup5.textBox1.Text }).ToString());
                //String proximoNumero = AppLib.Util.Format.CompletarZeroEsquerda(MASKNUMEROSEQ, (ultimoNumero + 1).ToString());

                fatura.Set("NUMERO", txtNumero.Text);
                fatura.Set("CODCLIFOR", primeiroRegistro.Get("CODCLIFOR").ToString());
                fatura.Set("CODFILIAL", primeiroRegistro.Get("CODFILIAL").ToString());
                fatura.Set("DATAEMISSAO", string.Format("{0:yyyy-MM-dd hh:mm:ss}", conn.GetDateTime()));
                fatura.Set("DATAVENCIMENTO", Convert.ToDateTime(dtDataVencimento.Text));
                fatura.Set("DATAPREVBAIXA", Convert.ToDateTime(dtDataVencimento.Text));
                fatura.Set("OBSERVACAO", txtobservacao.Text);
                fatura.Set("CODMOEDA", primeiroRegistro.Get("CODMOEDA").ToString());
                fatura.Set("VLORIGINAL", Convert.ToDecimal(txtVlOriginal.Text));
                fatura.Set("PRDESCONTO", 0);
                fatura.Set("VLDESCONTO", 0);
                fatura.Set("PRMULTA", 0);
                fatura.Set("VLMULTA", 0);
                fatura.Set("PRJUROS", 0);
                fatura.Set("VLJUROS", 0);
                fatura.Set("CODSTATUS", 0);
                fatura.Set("CODTIPDOC", psLookup5.textBox1.Text);
                fatura.Set("ORIGEM", "F");
                fatura.Set("DATACRIACAO", conn.GetDateTime());
                fatura.Set("CODUSUARIOCRIACAO", AppLib.Context.Usuario);
                fatura.Set("NFOUDUP", "0");
                fatura.Set("NSEQLANCA", 1);
                if (string.IsNullOrEmpty(psLookup13.textBox1.Text))
                {
                    fatura.Set("CODCCUSTO", null);
                }
                else
                {
                    fatura.Set("CODCCUSTO", psLookup13.textBox1.Text);
                }
                if (string.IsNullOrEmpty(psLookup16.textBox1.Text))
                {
                    fatura.Set("CODNATUREZAORCAMENTO", null);
                }
                else
                {
                    fatura.Set("CODNATUREZAORCAMENTO", psLookup16.textBox1.Text);
                }
                int retorno = fatura.Save();
                if (retorno == 1)
                {
                    #region ATUALIZAR CODFATURA NOS LANÇAMENTOS
                    for (int i = 0; i < listaRow.Count; i++)
                    {
                        String comandoAtualizaLancamento = "UPDATE FLANCA SET CODFATURA = ?, NFOUDUP = 1 WHERE CODEMPRESA = ? AND CODLANCA = ?";
                        if (conn.ExecTransaction(comandoAtualizaLancamento, new Object[] { proximo_CODLANCA, AppLib.Context.Empresa, listaRow[i]["FLANCA.CODLANCA"] }) != 1)
                        {
                            throw new Exception("Erro ao informar o código da fatura gerado nos lançamentos de origem.");
                        }
                    }
                    #endregion
                    #region Atualiza o ultiom número da ftipdoc
                    conn.ExecTransaction("UPDATE FTIPDOC SET ULTIMONUMERO = ? WHERE CODEMPRESA = ? AND CODTIPDOC = ?", new object[] { txtNumero.Text, AppLib.Context.Empresa, psLookup5.textBox1.Text });
                    #endregion
                }
                else
                {
                    throw new Exception("Erro ao gerar fatura.");
                }

                conn.Commit();
                AppLib.Windows.FormMessageDefault.ShowInfo("Fatura " + proximo_CODLANCA + " gerada com sucesso.");
                this.Dispose();
            }
            catch (Exception ex)
            {
                conn.Rollback();
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void psLookup5_AfterLookup(object sender, Lib.LookupEventArgs e)
        {
             int mask = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT MASKNUMEROSEQ FROM VPARAMETROS WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }));
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT USANUMEROSEQ, ULTIMONUMERO FROM FTIPDOC WHERE CODEMPRESA = ? AND CODTIPDOC = ?", new object[] { AppLib.Context.Empresa, psLookup5.textBox1.Text});
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["USANUMEROSEQ"]) == 0)
                {
                    txtNumero.Enabled = true;
                    txtNumero.Properties.MaxLength = mask;
                    txtNumero.Text = AppLib.Util.Format.CompletarZeroEsquerda(mask, txtNumero.Text);
                    
                }
                else
                {
                    txtNumero.Text = AppLib.Util.Format.CompletarZeroEsquerda(mask, Convert.ToString(Convert.ToInt32(dt.Rows[0]["ULTIMONUMERO"]) + 1));
                }
            }
        }
    }
}
