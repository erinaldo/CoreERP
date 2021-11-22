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
    public partial class frmBaixaLancamento : Form
    {
        List<int> listaCodLanca;
        string tipopagrec;
        int CODLANCA, CODEMPRESA;

        private bool UsaControleCheque = false;
        public List<Class.ControleCheque> ListCheque = new List<Class.ControleCheque>();
        public Class.GeraExtrato GerarExtrato;

        public frmBaixaLancamento(List<int> _listaCodLanca, string _tipoPagRec, string codConta, string dtVencimento)
        {
            InitializeComponent();
            listaCodLanca = _listaCodLanca;
            lpNaturezaOrcamentaria.Enabled = false;
            lpCentroCusto.Enabled = false;

            carregaGrid();
            tipopagrec = _tipoPagRec;

            if (!string.IsNullOrEmpty(codConta))
            {
                lpContaCaixa.txtcodigo.Text = codConta;
                lpContaCaixa.CarregaDescricao();
            }
            if (!string.IsNullOrEmpty(dtVencimento))
            {
                // João Pedro Luchiari - 24/07/2018
                //dtBaixa.Text = dtVencimento;
            }

            #region Controle de Cheque

            btnControleCheque.Enabled = false;
            lbCheque.Visible = false;
            tbValorCheque.Visible = false;

            #endregion
        }

        private void frmBaixaLancamento_Load(object sender, EventArgs e)
        {
            bool erroValidacao = false;

            for (int i = 0; i < listaCodLanca.Count; i++)
            {
                CODEMPRESA = AppLib.Context.Empresa;
                CODLANCA = listaCodLanca[i];

                int CLASSIFICACAO = this.getCLASSIFICACAO(CODEMPRESA, CODLANCA);
                int CODRELLANCA = this.getCODRELRELAC(CODEMPRESA, CODLANCA);

                if ((CLASSIFICACAO == 0) || (CLASSIFICACAO == 1))
                {
                    if (CODRELLANCA != 0)
                    {
                        AppLib.Windows.FormMessageDefault.ShowError("Lançamento vinculado é baixado automaticamente junto com o lançamento principal.\r\nDica: Não selecione lançamentos de adiantamento ou devolução no processo de baixa.");
                        // INTERROMPE O LOOP
                        i = listaCodLanca.Count;
                        erroValidacao = true;
                    }
                }
                //Verifica se o Lançamento não deu origem a uma fatura.
                if (VerificaOrigemFatura(CODLANCA).Equals(true))
                {
                    // INTERROMPE O LOOP
                    i = listaCodLanca.Count;
                    erroValidacao = true;
                    MessageBox.Show("Favor selecionar um lançamento que não seja origem de uma fatura.", "informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (cmbGerarComo.Text == "Um extrato para cada lançamento")
            {
                txtNumeroExtrato.Enabled = false;
                txtNumeroExtrato.Text = string.Empty;
                lpCentroCusto.Enabled = false;
                lpNaturezaOrcamentaria.Enabled = false;
            }

            if (erroValidacao)
            {
                this.Close();
            }
            else
            {
                Boolean AnalisouVinculos = false;

                if (getCLASSIFICACAO(CODEMPRESA, CODLANCA).Equals(2))
                {
                    for (int i = 0; i < listaCodLanca.Count; i++)
                    {
                        System.Data.DataTable dt = new frmVinculoLancamento().GetRegistrosSemVinculo(AppLib.Context.Empresa, listaCodLanca[i]);

                        if (dt.Rows.Count > 0)
                        {
                            if (AppLib.Windows.FormMessageDefault.ShowQuestion("Existem lançamentos á serem vinculads ao lançamento " + listaCodLanca[i] + ".\r\nGostaria de visualizar agora?") == System.Windows.Forms.DialogResult.Yes)
                            {
                                frmVinculoLancamento app = new frmVinculoLancamento(listaCodLanca[i]);

                                app.ShowDialog();
                                AnalisouVinculos = true;
                                carregaGrid();
                            }
                        }
                    }
                }

                if (AnalisouVinculos)
                {
                    // recarrega o formulario de baixa
                    LimpaFormulario();
                }
            }
        }

        private void cmbCheque_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCheque.SelectedText == "SIM")
            {
                cmbCompensar.Enabled = false;
                dtDataCompensacao.Enabled = false;

                cmbGerarComo.SelectedIndex = 2;
                cmbGerarComo.Enabled = false;

                if (cmbGerarComo.SelectedIndex == 2)
                {
                    txtNumeroExtrato.Enabled = false;
                    txtNumeroExtrato.Text = string.Empty;
                }

                // Controle de Cheque
                UsaControleCheque = true;
                btnControleCheque.Enabled = true;
                lbCheque.Visible = true;
                tbValorCheque.Visible = true;
                tbValorCheque.Enabled = false;
                tbValorCheque.Text = "0,00";
                btnExecutar.Enabled = false;
                lpContaCaixa.Enabled = true;
                lpCentroCusto.Enabled = true;
                lpNaturezaOrcamentaria.Enabled = true;
            }
            else
            {
                cmbGerarComo.SelectedIndex = 0;
                cmbGerarComo.Enabled = true;
                lpContaCaixa.Enabled = true;
                cmbCompensar.Enabled = true;
                dtDataCompensacao.Enabled = true;

                // Controle de Cheque
                UsaControleCheque = false;
                btnControleCheque.Enabled = false;
                lbCheque.Visible = false;
                tbValorCheque.Visible = false;
                tbValorCheque.Text = string.Empty;
                btnExecutar.Enabled = true;
            }
        }

        private void cmbGerarComo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGerarComo.SelectedText == "Um extrato para cada lançamento")
            {
                txtNumeroExtrato.Enabled = false;
                txtNumeroExtrato.Text = string.Empty;
                lpCentroCusto.Enabled = false;
                lpNaturezaOrcamentaria.Enabled = false;
            }
            else if (cmbGerarComo.SelectedText == "Um extrato para cada cheque")
            {
                if (cmbCheque.SelectedText == "NÃO" || cmbCheque.Text == "NÃO")
                {
                    MessageBox.Show("Esta opção só pode ser selecionada quando o tipo da baixa for Cheque.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbGerarComo.SelectedIndex = 0;
                    return;
                }
                else
                {
                    txtNumeroExtrato.Enabled = false;
                    txtNumeroExtrato.Text = string.Empty;
                    cmbCompensar.Enabled = false;
                    dtDataCompensacao.Enabled = false;
                    lpContaCaixa.Enabled = false;
                }
            }
            else if (cmbGerarComo.SelectedText == "Um extrato para todos os lançamentos")
            {
                txtNumeroExtrato.Enabled = true;
                cmbCompensar.Enabled = true;
                lpCentroCusto.Enabled = true;
                lpNaturezaOrcamentaria.Enabled = true;
                lpCentroCusto.txtcodigo.Text = string.Empty;
                lpCentroCusto.txtconteudo.Text = string.Empty;
                lpNaturezaOrcamentaria.txtcodigo.Text = string.Empty;
                lpNaturezaOrcamentaria.txtconteudo.Text = string.Empty;
            }
        }

        private void cmbCompensar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompensar.SelectedText == "SIM")
            {
                dtDataCompensacao.Enabled = true;
                dtDataCompensacao.Text = dtBaixa.Text;
            }
            else
            {
                dtDataCompensacao.Enabled = false;
                dtDataCompensacao.Text = string.Empty;
            }
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                if (string.IsNullOrEmpty(txtValorBaixa.Text))
                {
                    txtValorBaixa.Text = "0,00";
                }
                if (txtValorBaixa.Text == "0,00")
                {
                    if (MessageBox.Show("O valor da baixa é igual à 0,00. Deseja continuar?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        row1["Valor à baixar"] = txtValorBaixa.Text;
                    }
                    else
                    {
                        return;
                    }
                }
                row1["Valor à baixar"] = txtValorBaixa.Text;
            }
            calculaValor();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                txtValorLiquido.Text = string.Format("{0:n2}", row1["Valor Líquido"]);
                txtValorBaixa.Text = string.Format("{0:n2}", row1["Valor à baixar"]);
            }
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            #region Rotina sem o Controle de Cheque

            //            try
            //            {
            //                #region validação
            //                if (cmbCheque.SelectedText == "SIM" && string.IsNullOrEmpty(txtNumeroCheque.Text))
            //                {
            //                    MessageBox.Show("Favor preencher o número do cheque", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    return;
            //                }
            //                if (string.IsNullOrEmpty(dtBaixa.Text))
            //                {
            //                    MessageBox.Show("Favor preencher a data de Baixa", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    return;
            //                }
            //                if (string.IsNullOrEmpty(psContaCaixa.textBox1.Text))
            //                {
            //                    MessageBox.Show("Favor preencher a conta caixa", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    return;
            //                }
            //                if (cmbGerarComo.SelectedIndex == 1 && string.IsNullOrEmpty(txtNumeroExtrato.Text))
            //                {
            //                    MessageBox.Show("Favor preencher o número do extrato", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    return;
            //                }
            //                if (string.IsNullOrEmpty(txtHistorico.Text))
            //                {
            //                    MessageBox.Show("Favor preencher o histórico", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    return;
            //                }
            //                if (cmbCompensar.SelectedText == "SIM")
            //                {
            //                    if (Convert.ToDateTime(dtBaixa.Text) != Convert.ToDateTime(dtDataCompensacao.Text))
            //                    {
            //                        if (MessageBox.Show("Data de baixa diferente com a data de compensação.\nDeseja continuar?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //                        {
            //                            return;
            //                        }
            //                    }
            //                }
            //                #endregion

            //                if (MessageBox.Show("Confirma a baixa dos lançamentos?", "Informação do Sistema.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            //                {
            //                    Support.FinLanBaixaPar finLanBaixaPar = new Support.FinLanBaixaPar();
            //                    finLanBaixaPar.CodEmpresa = AppLib.Context.Empresa;
            //                    finLanBaixaPar.DataBaixa = Convert.ToDateTime(dtBaixa.Text);
            //                    finLanBaixaPar.CodConta = psContaCaixa.Text;

            //                    for (int i = 0; i < listaCodLanca.Count; i++)
            //                    {
            //                        if (verificaNFOUDUP(listaCodLanca[i]).Equals("1"))
            //                        {
            //                            MessageBox.Show("Não pode baixar lançamentos faturados.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                            return;
            //                        }
            //                    }

            //                    if (tipopagrec != null)
            //                    {
            //                        if (tipopagrec == "0")
            //                            finLanBaixaPar.PagRec = Support.FinLanPagRecEnum.Pagar;
            //                        if (tipopagrec == "1")
            //                            finLanBaixaPar.PagRec = Support.FinLanPagRecEnum.Receber;
            //                    }
            //                    if (cmbGerarComo.SelectedIndex == 0)
            //                        finLanBaixaPar.GeraExtratoComo = Support.FinLanBaixaGeraExtratoComoEnum.UmExtratoParaCadaLancamento;
            //                    else
            //                        finLanBaixaPar.GeraExtratoComo = Support.FinLanBaixaGeraExtratoComoEnum.UmExtratoParaTodosLancamentos;

            //                    finLanBaixaPar.NumeroExtrato = txtNumeroExtrato.Text;
            //                    finLanBaixaPar.Historico = txtHistorico.Text;
            //                    // PREPARA A LISTA DO QUE TEM QUE SER BAIXADO
            //                    List<int> lista_CODLANCA = new List<int>();
            //                    List<string> lista_NFOUDUP = new List<string>();
            //                    List<decimal> lista_VLBAIXADO = new List<decimal>();

            //                    for (int i = 0; i < gridView1.RowCount; i++)
            //                    {
            //                        DataRow row = gridView1.GetDataRow(i);

            //                        lista_CODLANCA.Add(Convert.ToInt32(row["Código do Lançamento"]));
            //                        lista_VLBAIXADO.Add(Convert.ToDecimal(row["Valor à baixar"]));
            //                        // INCLUI NA LISTA OS LANÇAMENTOS VINCULADOS
            //                        System.Data.DataTable dtBaixarVinculadosJunto = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT FLANCA.CODLANCA, FLANCA.VLLIQUIDO
            //FROM FRELLANCA, FLANCA
            //WHERE FRELLANCA.CODEMPRESA = FLANCA.CODEMPRESA
            //  AND FRELLANCA.CODLANCA = FLANCA.CODLANCA
            //  AND FRELLANCA.CODEMPRESA = ?
            //  AND FRELLANCA.CODLANCA <> ?
            //  AND FRELLANCA.CODRELLANCA = ( SELECT CODRELLANCA FROM FRELLANCA WHERE CODEMPRESA = ? AND CODLANCA = ? )
            //  AND FLANCA.CODSTATUS = 0", new object[] { AppLib.Context.Empresa, row["Código do Lançamento"], AppLib.Context.Empresa, row["Código do Lançamento"] });

            //                        for (int x = 0; x < dtBaixarVinculadosJunto.Rows.Count; x++)
            //                        {
            //                            lista_CODLANCA.Add(int.Parse(dtBaixarVinculadosJunto.Rows[x]["CODLANCA"].ToString()));
            //                            lista_VLBAIXADO.Add(Convert.ToDecimal(dtBaixarVinculadosJunto.Rows[x]["VLLIQUIDO"]));
            //                        }
            //                        // INCLUI NA LISTA OS LANÇAMENTOS FATURADOS
            //                        System.Data.DataTable dtBaixarFaturadosJunto = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
            //SELECT FLANCA.CODLANCA, FLANCA.VLLIQUIDO
            //FROM FLANCA
            //WHERE FLANCA.CODEMPRESA = ?
            //  AND FLANCA.CODFATURA = ?", new object[] { AppLib.Context.Empresa, row["Código do Lançamento"] });

            //                        for (int x = 0; x < dtBaixarFaturadosJunto.Rows.Count; x++)
            //                        {
            //                            lista_CODLANCA.Add(int.Parse(dtBaixarFaturadosJunto.Rows[x]["CODLANCA"].ToString()));
            //                            lista_VLBAIXADO.Add(Convert.ToDecimal(dtBaixarFaturadosJunto.Rows[x]["VLLIQUIDO"]));
            //                        }
            //                    }

            //                    // ALIMENTA O OBJETO COM OS PARÂMETROS DE BAIXA
            //                    finLanBaixaPar.CodLanca = new int[lista_CODLANCA.Count];
            //                    finLanBaixaPar.ValorBaixa = new Decimal[lista_VLBAIXADO.Count];
            //                    finLanBaixaPar.codCCusto = string.IsNullOrEmpty(lpCentroCusto.txtcodigo.Text) ? null : lpCentroCusto.txtcodigo.Text;
            //                    finLanBaixaPar.codNaturezaOrcamento = string.IsNullOrEmpty(lpNaturezaOrcamentaria.txtcodigo.Text) ? null : lpNaturezaOrcamentaria.txtcodigo.Text;
            //                    //finLanBaixaPar.codNaturezaOrcamento = string.IsNullOrEmpty(psLookup16.textBox1.Text) ? null : psLookup16.textBox1.Text;

            //                    for (int i = 0; i < lista_CODLANCA.Count; i++)
            //                    {
            //                        string nf = verificaNFOUDUP(Convert.ToInt32(lista_CODLANCA[i].ToString()));
            //                        lista_NFOUDUP.Add(nf);
            //                    }
            //                    finLanBaixaPar.NFOUDUP = new string[lista_NFOUDUP.Count];
            //                    for (int i = 0; i < lista_CODLANCA.Count; i++)
            //                    {
            //                        finLanBaixaPar.CodLanca[i] = lista_CODLANCA[i];
            //                        finLanBaixaPar.ValorBaixa[i] = lista_VLBAIXADO[i];
            //                        finLanBaixaPar.NFOUDUP[i] = lista_NFOUDUP[i];
            //                    }
            //                    if (cmbCheque.SelectedText == "SIM")
            //                    {
            //                        finLanBaixaPar.CODCHEQUE = this.IncluirCheque();
            //                        finLanBaixaPar.cheque = true;
            //                        //finLanBaixaPar.GeraExtratoComo = Support.FinLanBaixaGeraExtratoComoEnum.UmExtratoParaTodosLancamentos;
            //                    }
            //                    else
            //                    {
            //                        finLanBaixaPar.cheque = false;
            //                    }
            //                    try
            //                    {
            //                        // BAIXAR
            //                        BaixaLancamento(finLanBaixaPar);
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        MessageBox.Show(ex.Message);
            //                        return;
            //                    }

            //                    // COMPENSAR
            //                    if (cmbCompensar.SelectedText == "SIM")
            //                    {
            //                        for (int i = 0; i < lista_CODLANCA.Count; i++)
            //                        {
            //                            string idextrato = AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT IDEXTRATO FROM FLANCA WHERE CODLANCA = ?", new object[] { lista_CODLANCA[i] }).ToString();
            //                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FEXTRATO SET COMPENSADO = ?, DATACOMPENSACAO = ? WHERE CODEMPRESA = ? AND IDEXTRATO = ?", new object[] { 1, Convert.ToDateTime(dtDataCompensacao.Text), AppLib.Context.Empresa, idextrato });
            //                        }
            //                    }

            //                    // Exclui Extrato Devolução

            //                    for (int i = 0; i < lista_CODLANCA.Count; i++)
            //                    {
            //                        AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM FEXTRATO
            //	                                        WHERE
            //		                                    FEXTRATO.CODEMPRESA = ? 
            //	                                        AND FEXTRATO.IDEXTRATO IN (SELECT FLANCA.IDEXTRATO
            //									        FROM
            //									        FLANCA
            //									        INNER JOIN FTIPDOC ON FTIPDOC.CODEMPRESA = FLANCA.CODEMPRESA AND FTIPDOC.CODTIPDOC = FLANCA.CODTIPDOC
            //										    WHERE
            //											FTIPDOC.CLASSIFICACAO = 1
            //										    AND FLANCA.CODEMPRESA = ? 
            //										    AND FLANCA.CODLANCA = ?) ", new object[] { AppLib.Context.Empresa, AppLib.Context.Empresa, lista_CODLANCA[i] });

            //                        AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE 
            //                                                                                        FLANCA
            //                                                                                        SET
            //                                                                                        FLANCA.IDEXTRATO = NULL
            //                                                                                        FROM
            //                                                                                        FLANCA
            //                                                                                        INNER JOIN FTIPDOC ON FTIPDOC.CODEMPRESA = FLANCA.CODEMPRESA AND FTIPDOC.CODTIPDOC = FLANCA.CODTIPDOC
            //                                                                                        WHERE
            //                                                                                            FTIPDOC.CLASSIFICACAO = 1
            //                                                                                        AND FLANCA.CODEMPRESA = ?
            //                                                                                        AND FLANCA.CODLANCA = ?", new object[] { AppLib.Context.Empresa, lista_CODLANCA[i] });
            //                    }

            //                    MessageBox.Show("Baixa realizada com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    this.Dispose();
            //                }
            //            }

            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message, "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            }

            #endregion

            try
            {
                #region validação

                if (UsaControleCheque == true)
                {
                    Class.GeraExtrato gerar = new Class.GeraExtrato(ListCheque);
                    GerarExtrato = gerar;

                    if (string.IsNullOrEmpty(dtBaixa.Text))
                    {
                        MessageBox.Show("Favor preencher a data de Baixa", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    Class.GeraExtrato gerar = new Class.GeraExtrato(null);
                    GerarExtrato = gerar;

                    if (cmbCompensar.SelectedText == "SIM")
                    {
                        if (string.IsNullOrEmpty(dtBaixa.Text))
                        {
                            MessageBox.Show("Favor preencher a data de Baixa", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(dtBaixa.Text))
                    {
                        MessageBox.Show("Favor preencher a data de Baixa", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (string.IsNullOrEmpty(lpContaCaixa.txtcodigo.Text))
                    {
                        MessageBox.Show("Favor preencher a conta caixa", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (cmbGerarComo.SelectedIndex == 1 && string.IsNullOrEmpty(txtNumeroExtrato.Text))
                    {
                        MessageBox.Show("Favor preencher o número do extrato", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (string.IsNullOrEmpty(txtHistorico.Text))
                    {
                        MessageBox.Show("Favor preencher o histórico", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                #endregion

                if (MessageBox.Show("Confirma a baixa dos lançamentos?", "Informação do Sistema.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    Support.FinLanBaixaPar finLanBaixaPar = new Support.FinLanBaixaPar();
                    finLanBaixaPar.CodEmpresa = AppLib.Context.Empresa;

                    if (!string.IsNullOrEmpty(dtBaixa.Text))
                    {
                        finLanBaixaPar.DataBaixa = Convert.ToDateTime(dtBaixa.Text);
                    }

                    finLanBaixaPar.CodConta = lpContaCaixa.txtcodigo.Text;

                    for (int i = 0; i < listaCodLanca.Count; i++)
                    {
                        if (verificaNFOUDUP(listaCodLanca[i]).Equals("1"))
                        {
                            MessageBox.Show("Não pode baixar lançamentos faturados.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    if (tipopagrec != null)
                    {
                        if (tipopagrec == "0")
                            finLanBaixaPar.PagRec = Support.FinLanPagRecEnum.Pagar;
                        if (tipopagrec == "1")
                            finLanBaixaPar.PagRec = Support.FinLanPagRecEnum.Receber;
                    }
                    if (cmbGerarComo.SelectedIndex == 0)
                        finLanBaixaPar.GeraExtratoComo = Support.FinLanBaixaGeraExtratoComoEnum.UmExtratoParaCadaLancamento;
                    else
                        finLanBaixaPar.GeraExtratoComo = Support.FinLanBaixaGeraExtratoComoEnum.UmExtratoParaTodosLancamentos;

                    finLanBaixaPar.NumeroExtrato = txtNumeroExtrato.Text;
                    finLanBaixaPar.Historico = txtHistorico.Text;
                    // PREPARA A LISTA DO QUE TEM QUE SER BAIXADO
                    List<int> lista_CODLANCA = new List<int>();
                    List<string> lista_NFOUDUP = new List<string>();
                    List<decimal> lista_VLBAIXADO = new List<decimal>();

                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        DataRow row = gridView1.GetDataRow(i);

                        lista_CODLANCA.Add(Convert.ToInt32(row["Código do Lançamento"]));
                        lista_VLBAIXADO.Add(Convert.ToDecimal(row["Valor à baixar"]));
                        // INCLUI NA LISTA OS LANÇAMENTOS VINCULADOS
                        DataTable dtBaixarVinculadosJunto = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT FLANCA.CODLANCA, FLANCA.VLLIQUIDO
FROM FRELLANCA, FLANCA
WHERE FRELLANCA.CODEMPRESA = FLANCA.CODEMPRESA
  AND FRELLANCA.CODLANCA = FLANCA.CODLANCA
  AND FRELLANCA.CODEMPRESA = ?
  AND FRELLANCA.CODLANCA <> ?
  AND FRELLANCA.CODRELLANCA = ( SELECT CODRELLANCA FROM FRELLANCA WHERE CODEMPRESA = ? AND CODLANCA = ? )
  AND FLANCA.CODSTATUS = 0", new object[] { AppLib.Context.Empresa, row["Código do Lançamento"], AppLib.Context.Empresa, row["Código do Lançamento"] });

                        for (int x = 0; x < dtBaixarVinculadosJunto.Rows.Count; x++)
                        {
                            lista_CODLANCA.Add(int.Parse(dtBaixarVinculadosJunto.Rows[x]["CODLANCA"].ToString()));
                            lista_VLBAIXADO.Add(Convert.ToDecimal(dtBaixarVinculadosJunto.Rows[x]["VLLIQUIDO"]));
                        }
                        // INCLUI NA LISTA OS LANÇAMENTOS FATURADOS
                        DataTable dtBaixarFaturadosJunto = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
SELECT FLANCA.CODLANCA, FLANCA.VLLIQUIDO
FROM FLANCA
WHERE FLANCA.CODEMPRESA = ?
  AND FLANCA.CODFATURA = ?", new object[] { AppLib.Context.Empresa, row["Código do Lançamento"] });

                        for (int x = 0; x < dtBaixarFaturadosJunto.Rows.Count; x++)
                        {
                            lista_CODLANCA.Add(int.Parse(dtBaixarFaturadosJunto.Rows[x]["CODLANCA"].ToString()));
                            lista_VLBAIXADO.Add(Convert.ToDecimal(dtBaixarFaturadosJunto.Rows[x]["VLLIQUIDO"]));
                        }
                    }

                    finLanBaixaPar.CodLanca = new int[lista_CODLANCA.Count];
                    finLanBaixaPar.ValorBaixa = new Decimal[lista_VLBAIXADO.Count];
                    finLanBaixaPar.codCCusto = string.IsNullOrEmpty(lpCentroCusto.txtcodigo.Text) ? null : lpCentroCusto.txtcodigo.Text;
                    finLanBaixaPar.codNaturezaOrcamento = string.IsNullOrEmpty(lpNaturezaOrcamentaria.txtcodigo.Text) ? null : lpNaturezaOrcamentaria.txtcodigo.Text;

                    for (int i = 0; i < lista_CODLANCA.Count; i++)
                    {
                        string nf = verificaNFOUDUP(Convert.ToInt32(lista_CODLANCA[i].ToString()));
                        lista_NFOUDUP.Add(nf);
                    }
                    finLanBaixaPar.NFOUDUP = new string[lista_NFOUDUP.Count];
                    for (int i = 0; i < lista_CODLANCA.Count; i++)
                    {
                        finLanBaixaPar.CodLanca[i] = lista_CODLANCA[i];
                        finLanBaixaPar.ValorBaixa[i] = lista_VLBAIXADO[i];
                        finLanBaixaPar.NFOUDUP[i] = lista_NFOUDUP[i];
                    }
                    if (cmbCheque.SelectedText == "SIM")
                    {
                        InsertCheque();
                        finLanBaixaPar.cheque = true;
                    }
                    else
                    {
                        finLanBaixaPar.cheque = false;
                    }
                    try
                    {
                        // BAIXAR
                        BaixaLancamento(finLanBaixaPar, GerarExtrato);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    // COMPENSAR
                    if (cmbCompensar.SelectedText == "SIM")
                    {
                        for (int i = 0; i < lista_CODLANCA.Count; i++)
                        {
                            string idextrato = AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT IDEXTRATO FROM FLANCA WHERE CODLANCA = ?", new object[] { lista_CODLANCA[i] }).ToString();
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FEXTRATO SET COMPENSADO = ?, DATACOMPENSACAO = ? WHERE CODEMPRESA = ? AND IDEXTRATO = ?", new object[] { 1, Convert.ToDateTime(dtDataCompensacao.Text), AppLib.Context.Empresa, idextrato });
                        }
                    }

                    // Exclui Extrato Devolução
                    for (int i = 0; i < lista_CODLANCA.Count; i++)
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM FEXTRATO
	                                        WHERE
		                                    FEXTRATO.CODEMPRESA = ? 
	                                        AND FEXTRATO.IDEXTRATO IN (SELECT FLANCA.IDEXTRATO
									        FROM
									        FLANCA
									        INNER JOIN FTIPDOC ON FTIPDOC.CODEMPRESA = FLANCA.CODEMPRESA AND FTIPDOC.CODTIPDOC = FLANCA.CODTIPDOC
										    WHERE
											FTIPDOC.CLASSIFICACAO = 1
										    AND FLANCA.CODEMPRESA = ? 
										    AND FLANCA.CODLANCA = ?) ", new object[] { AppLib.Context.Empresa, AppLib.Context.Empresa, lista_CODLANCA[i] });

                        AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE 
                                                                                        FLANCA
                                                                                        SET
                                                                                        FLANCA.IDEXTRATO = NULL
                                                                                        FROM
                                                                                        FLANCA
                                                                                        INNER JOIN FTIPDOC ON FTIPDOC.CODEMPRESA = FLANCA.CODEMPRESA AND FTIPDOC.CODTIPDOC = FLANCA.CODTIPDOC
                                                                                        WHERE
                                                                                            FTIPDOC.CLASSIFICACAO = 1
                                                                                        AND FLANCA.CODEMPRESA = ?
                                                                                        AND FLANCA.CODLANCA = ?", new object[] { AppLib.Context.Empresa, lista_CODLANCA[i] });
                    }

                    MessageBox.Show("Baixa realizada com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Controle de Cheque

        private void btnControleCheque_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lpContaCaixa.txtcodigo.Text))
            {
                MessageBox.Show("É necessário informar o código da Conta Caixa.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(lpCentroCusto.txtcodigo.Text))
            {
                MessageBox.Show("É necessário informar o Centro de Custo.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(lpNaturezaOrcamentaria.txtcodigo.Text))
            {
                MessageBox.Show("É necessário informar a Natureza.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(dtBaixa.Text))
            {
                MessageBox.Show("Favor preencher a data de Baixa", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PS.Glb.New.Processos.Financeiro.frmControleCheque Cheque = new frmControleCheque();
            Cheque.ValorBaixa = Convert.ToDecimal(txtTotalBaixar.Text);
            Cheque.PagRec = Convert.ToInt32(tipopagrec);
            Cheque.CodConta = lpContaCaixa.txtcodigo.Text;
            Cheque.DataBaixa = dtBaixa.DateTime;
            Cheque.ShowDialog();

            if (Cheque.ExecutaControleCheque == true)
            {
                ListCheque = Cheque.ListCheque;
                tbValorCheque.Text = string.Format("{0:n2}", Convert.ToDecimal(ListCheque.Sum(x => x.VALOR)));

                if (Convert.ToDecimal(tbValorCheque.Text) == Convert.ToDecimal(txtTotalBaixar.Text))
                {
                    btnExecutar.Enabled = true;
                }
            }
            else
            {
                tbValorCheque.Text = "0,00";
            }
        }

        #endregion

        #region Métodos

        #region Controle de Cheque 

        private void InsertCheque()
        {
            AppLib.ORM.Jit FCHEQUE = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "FCHEQUE");

            try
            {
                for (int i = 0; i < ListCheque.Count; i++)
                {
                    FCHEQUE.Set("CODCHEQUE", ListCheque[i].CODCHEQUE);
                    FCHEQUE.Set("CODEMPRESA", AppLib.Context.Empresa);
                    FCHEQUE.Set("CODCONTA", ListCheque[i].CODCONTA);
                    FCHEQUE.Set("NUMERO", ListCheque[i].NUMERO);
                    FCHEQUE.Set("TIPOPAGREC", ListCheque[i].PAGREC);
                    FCHEQUE.Set("VALOR", ListCheque[i].VALOR);
                    FCHEQUE.Set("DATACRIACAO", ListCheque[i].DATACRIACAO);
                    FCHEQUE.Set("USUARIOCRIACAO", ListCheque[i].USUARIOCRIACAO);
                    FCHEQUE.Set("DATAALTERACAO", null);
                    FCHEQUE.Set("USUARIOALTERACAO", null);
                    FCHEQUE.Set("DATABOA", ListCheque[i].DATABOA);
                    FCHEQUE.Set("OBSERVACAO", ListCheque[i].OBSERVACAO);
                    FCHEQUE.Set("CODBANCO", ListCheque[i].CODBANCO);
                    FCHEQUE.Set("CODAGENCIA", ListCheque[i].CODAGENCIA);
                    FCHEQUE.Set("CODCCORRENTE", ListCheque[i].CODCCORRENTE);
                    FCHEQUE.Set("DATAEMISSAO", ListCheque[i].DATAEMISSAO);
                    FCHEQUE.Set("DATACOMPENSACAO", ListCheque[i].DATACOMPENSACAO);
                    FCHEQUE.Set("COMPENSADO", ListCheque[i].COMPENSADO);

                    if (FCHEQUE.Insert() > 0)
                    {
                        if (ListCheque[i].PAGREC == 0)
                        {
                            IncrementaNumero(ListCheque[i].CODCONTA, ListCheque[i].NUMERO);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void IncrementaNumero(string _codConta, int _numero)
        {
            try
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FCONTA SET NUMEROCHEQUE = ? WHERE CODEMPRESA = ? AND CODCONTA = ?", new object[] { _numero, AppLib.Context.Empresa, _codConta });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        private void carregaGrid()
        {
            string sql = string.Empty;
            for (int i = 0; i < listaCodLanca.Count; i++)
            {
                if (i == 0)
                {
                    sql = listaCodLanca[i].ToString();
                }
                sql = sql + ", " + listaCodLanca[i].ToString();
            }

            gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT FLANCA.CODLANCA AS 'Código do Lançamento', FLANCA.NUMERO AS 'Número', VCLIFOR.NOMEFANTASIA AS 'Nome', FLANCA.DATAVENCIMENTO AS 'Data de Vencimento', FLANCA.VLORIGINAL AS 'Valor Original', CONVERT(DECIMAL(15,2), FLANCA.VLLIQUIDO) AS 'Valor Líquido', FLANCA.VLBAIXADO AS 'Valor à baixar', CONVERT(DECIMAL(15,2), FLANCA.VLVINCAD) AS 'Valor Adiantamento', CONVERT(DECIMAL(15,2), FLANCA.VLVINCDV) AS 'Valor Devolução' FROM FLANCA INNER JOIN VCLIFOR ON FLANCA.CODCLIFOR = VCLIFOR.CODCLIFOR AND FLANCA.CODEMPRESA = VCLIFOR.CODEMPRESA WHERE CODLANCA IN (" + sql + ") AND FLANCA.CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });
            gridView1.BestFitColumns();

            txtTotalBaixar.Text = "0";

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DataRow row = gridView1.GetDataRow(i);
                row["Valor à baixar"] = row["Valor Líquido"];
                txtTotalBaixar.Text = string.Format("{0:n2}", Convert.ToDecimal(txtTotalBaixar.Text) + Convert.ToDecimal(row["Valor à baixar"]));
            }
        }

        private string verificaNFOUDUP(int codlanca)
        {
            try
            {
                string retorno = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NFOUDUP FROM FLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { codlanca, AppLib.Context.Empresa }).ToString();
                if (string.IsNullOrEmpty(retorno) || retorno.Equals("0"))
                {
                    return retorno;
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private void calculaValor()
        {
            txtTotalBaixar.Text = "0";
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(i);
                txtTotalBaixar.Text = string.Format("{0:n2}", Convert.ToDecimal(txtTotalBaixar.Text) + Convert.ToDecimal(row1["Valor à baixar"]));
            }
        }

        private void BaixaLancamento(Support.FinLanBaixaPar finLanBaixaPar, Class.GeraExtrato _gerar = null)
        {
            try
            {
                PSPartLancaData psPartLancaData = new PSPartLancaData();
                psPartLancaData._tablename = "FLANCA";
                psPartLancaData._keys = new string[] { "CODEMPRESA", "CODLANCA" };

                psPartLancaData.BaixaLancamento(finLanBaixaPar, GerarExtrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int getCLASSIFICACAO(int CODEMPRESA, int CODLANCA)
        {
            String consultaFLANCA = @"
SELECT 
( SELECT CLASSIFICACAO FROM FTIPDOC WHERE FTIPDOC.CODEMPRESA = FLANCA.CODEMPRESA AND FTIPDOC.CODTIPDOC = FLANCA.CODTIPDOC ) CLASSIFICACAO
FROM FLANCA
WHERE CODEMPRESA = ?
  AND CODLANCA = ?";

            return int.Parse(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, consultaFLANCA, new Object[] { CODEMPRESA, CODLANCA }).ToString());
        }

        public int getCODRELRELAC(int CODEMPRESA, int CODLANCA)
        {
            return new PSPartVinculaLancaAppFrm().CarregaCODRELLANCA(CODEMPRESA, CODLANCA);
        }

        private bool VerificaOrigemFatura(int codLanca)
        {
            try
            {
                if (string.IsNullOrEmpty(AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODFATURA FROM FLANCA WHERE CODLANCA = ?", new object[] { codLanca }).ToString()))
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void LimpaFormulario()
        {
            dtBaixa.Text = DateTime.Now.ToShortDateString();
            txtValorLiquido.Enabled = false;
            txtValorLiquido.Text = "0,00";
            txtValorBaixa.Text = "0,00";
            txtValorBaixa.Enabled = false;
            txtTotalBaixar.Text = "0,00";
            txtHistorico.Text = string.Empty;
            cmbGerarComo.SelectedIndex = 0;
            cmbCompensar.SelectedIndex = -1;
            dtDataCompensacao.Enabled = true;
        }

        #endregion
    }
}
