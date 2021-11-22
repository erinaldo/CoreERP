using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PS.Lib;

namespace PS.Glb
{
    public partial class PSPartFaturaAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        PS.Lib.Valida vl = new PS.Lib.Valida();

        private List<int> CODLANCA = new List<int>();
        private decimal VLORIGINAL { get; set; }

        public PSPartFaturaAppFrm()
        {
            InitializeComponent();
            psLookup13.PSPart = "PSPartCentroCusto";
            psLookup16.PSPart = "PSPartNaturezaOrcamento";
        }

        private void PSPartFaturaAppFrm_Load(object sender, EventArgs e)
        {

            #region Validações
            //Validações
            /*
             * Fábio Campos 20/04/2016
             * ************************************************************************************************
             */
            string CODCLIFOR = gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[0]), "CODCLIFOR").Valor.ToString();
            string TIPOPAGREC = gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[0]), "TIPOPAGREC").Valor.ToString();
            string TIPOPAGREC_VERIFICA = string.Empty;
            string CODCLIFOR_VERIFICA = string.Empty;
            //verifica se o cliente é o mesmo para poder gerar a fatura.
            for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
            {
                CODCLIFOR_VERIFICA = gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[i]), "CODCLIFOR").Valor.ToString();
                TIPOPAGREC_VERIFICA = gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[i]), "TIPOPAGREC").Valor.ToString();
                if (!CODCLIFOR.Equals(CODCLIFOR_VERIFICA))
                {
                    MessageBox.Show("Permitido apenas para lançamentos do mesmo cliente/fornecedor.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }
                //Verifica se os lançamentos estão misturados.
                if (!TIPOPAGREC.Equals(TIPOPAGREC_VERIFICA))
                {
                    MessageBox.Show("Não pode misturar lançamentos de PAGAR/RECEBER ao gerar fatura.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }
            }

            //Altera o CODTIPODOC
            if (TIPOPAGREC.Equals("0"))
            {
                textBoxCODTIPDOC.Text = "FATP";
            }
            else
            {
                textBoxCODTIPDOC.Text = "FATR";
            }
            //**************************************************************************************************
            #endregion

            textBoxNOME.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField("", "SELECT NOME FROM FTIPDOC WHERE CODEMPRESA = 1 AND CODTIPDOC = '" + textBoxCODTIPDOC.Text + "'").ToString();
            dateTimePickerDATAVENCIMENTO.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            CODLANCA.Clear();
            VLORIGINAL = 0;

            if (psPartApp.DataGrid != null)
            {
                for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
                {
                    CODLANCA.Add(int.Parse(gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[i]), "CODLANCA").Valor.ToString()));
                    VLORIGINAL += Convert.ToDecimal(gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[i]), "VLLIQUIDO").Valor);
                    //Verfica se os lançamentos estão em abertos.
                    if (verificaStatusLancamento(CODLANCA[i].ToString()).Equals(false))
                    {
                        MessageBox.Show("Permitido gerar faturas apenas para lançamentos em aberto.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                    }
                    //Verifica se o item não é fatura ou faturado.
                    if (verificaItemFaturado(CODLANCA[i].ToString()).Equals(false))
                    {
                        MessageBox.Show("Não pode gerar fatura de fatura.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                    }
                    //Verifica se os lançamentos não são de previsão
                    if (verificaClassificacao(CODLANCA[i].ToString()).Equals(false))
                    {
                        MessageBox.Show("Não é permitido gerar faturas de lançamentos de previsão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                    }
                }
            }
            String formatacao = "{0:0,0.00}";
            textBoxVLORIGINAL.Text = String.Format(formatacao, VLORIGINAL);
            textBoxOBSERVACAO.Text = "";
            psLookup13.textBox2.Text = "";
            psLookup13.textBox1.Text = "";
            psLookup16.textBox2.Text = "";
            psLookup16.textBox1.Text = "";
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

        public override Boolean Execute()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();

            try
            {
                #region GERAR FATURA

                AppLib.ORM.Jit primeiroRegistro = new AppLib.ORM.Jit(conn, "FLANCA");
                primeiroRegistro.Set("CODEMPRESA", AppLib.Context.Empresa);
                primeiroRegistro.Set("CODLANCA", CODLANCA[0]);
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
                String consultaUltimoNumero = "SELECT MAX(NUMERO) FROM FLANCA WHERE CODEMPRESA = ? AND CODTIPDOC = ?";
                int ultimoNumero = int.Parse(conn.ExecGetField(0, consultaUltimoNumero, new Object[] { AppLib.Context.Empresa, textBoxCODTIPDOC.Text }).ToString());
                String proximoNumero = AppLib.Util.Format.CompletarZeroEsquerda(MASKNUMEROSEQ, (ultimoNumero + 1).ToString());

                fatura.Set("NUMERO", proximoNumero);
                fatura.Set("CODCLIFOR", primeiroRegistro.Get("CODCLIFOR").ToString());
                fatura.Set("CODFILIAL", primeiroRegistro.Get("CODFILIAL").ToString());
                fatura.Set("DATAEMISSAO", string.Format("{0:yyyy-MM-dd hh:mm:ss}", conn.GetDateTime()));
                fatura.Set("DATAVENCIMENTO", dateTimePickerDATAVENCIMENTO.Value);
                fatura.Set("DATAPREVBAIXA", dateTimePickerDATAVENCIMENTO.Value);
                fatura.Set("OBSERVACAO", textBoxOBSERVACAO.Text);
                fatura.Set("CODMOEDA", primeiroRegistro.Get("CODMOEDA").ToString());
                fatura.Set("VLORIGINAL", VLORIGINAL);
                fatura.Set("PRDESCONTO", 0);
                fatura.Set("VLDESCONTO", 0);
                fatura.Set("PRMULTA", 0);
                fatura.Set("VLMULTA", 0);
                fatura.Set("PRJUROS", 0);
                fatura.Set("VLJUROS", 0);
                fatura.Set("CODSTATUS", 0);
                fatura.Set("CODTIPDOC", textBoxCODTIPDOC.Text);
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
                if (fatura.Insert() == 1)
                {
                    #region ATUALIZAR CODFATURA NOS LANÇAMENTOS
                    for (int i = 0; i < CODLANCA.Count; i++)
                    {
                        String comandoAtualizaLancamento = "UPDATE FLANCA SET CODFATURA = ?, NFOUDUP = 1 WHERE CODEMPRESA = ? AND CODLANCA = ?";
                        if (conn.ExecTransaction(comandoAtualizaLancamento, new Object[] { proximo_CODLANCA, AppLib.Context.Empresa, CODLANCA[i] }) != 1)
                        {
                            throw new Exception("Erro ao informar o código da fatura gerado nos lançamentos de origem.");
                        }
                    }

                    #endregion
                }
                else
                {
                    throw new Exception("Erro ao gerar fatura.");
                }

                #endregion

                conn.Commit();
                AppLib.Windows.FormMessageDefault.ShowInfo("Fatura " + proximo_CODLANCA + " gerada com sucesso.");
                this.Close();
            }
            catch (Exception ex)
            {
                conn.Rollback();
                AppLib.Windows.FormMessageDefault.ShowError(ex.Message);
                return false;
            }

            return true;
        }

    }
}
