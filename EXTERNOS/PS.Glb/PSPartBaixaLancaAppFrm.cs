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
    public partial class PSPartBaixaLancaAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        PS.Lib.Valida vl = new PS.Lib.Valida();

        private List<Support.FinLanBaixaBase> finLanBaixaBase;
        private int ItensSelecionados = 0;
        private int? PagRec = null;

        //private int IDTIPOCHEQUE = 0;

        public PSPartBaixaLancaAppFrm()
        {
            InitializeComponent();

            psLookup13.PSPart = "PSPartCentroCusto";
            psLookup16.PSPart = "PSPartNaturezaOrcamento";

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 0;
            list1[0].DisplayMember = "Um extrato para cada lançamento";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 1;
            list1[1].DisplayMember = "Um extrato para todos os lançamentos";

            psExtratoComo.DataSource = list1;
            psExtratoComo.DisplayMember = "DisplayMember";
            psExtratoComo.ValueMember = "ValueMember";

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

        private void PSPartBaixaLancaAppFrm_Load(object sender, EventArgs e)
        {
            LimpaFormulario();



            #region VALIDAÇÕES

            int CODEMPRESA = 0;
            int CODLANCA = 0;

            Boolean erroValidacao = false;

            // OBTÉM OS CAMPOS CHAVE
            if (psPartApp.DataGrid != null)
            {
                for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
                {
                    CODEMPRESA = int.Parse(gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[i]), "CODEMPRESA").Valor.ToString());
                    CODLANCA = int.Parse(gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[i]), "CODLANCA").Valor.ToString());

                    int CLASSIFICACAO = this.getCLASSIFICACAO(CODEMPRESA, CODLANCA);
                    int CODRELLANCA = this.getCODRELRELAC(CODEMPRESA, CODLANCA);

                    if ((CLASSIFICACAO == 0) || (CLASSIFICACAO == 1))
                    {
                        if (CODRELLANCA != 0)
                        {   
                            AppLib.Windows.FormMessageDefault.ShowError("Lançamento vinculado é baixado automaticamente junto com o lançamento principal.\r\nDica: Não selecione lançamentos de adiantamento ou devolução no processo de baixa.");
                            // INTERROMPE O LOOP
                            i = psPartApp.DataGrid.Rows.Count;
                            erroValidacao = true;
                        }
                    }
                    //Verifica se o Lançamento não deu origem a uma fatura.
                    if (VerificaOrigemFatura(CODLANCA).Equals(true))
                    {
                        // INTERROMPE O LOOP
                        i = psPartApp.DataGrid.Rows.Count;
                        erroValidacao = true;
                        MessageBox.Show("Favor selecionar um lançamento que não seja origem de uma fatura.", "informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            #endregion



            if (erroValidacao)
            {
                this.Close();
            }
            else
            {
                Boolean AnalisouVinculos = false;

                if (getCLASSIFICACAO(CODEMPRESA, CODLANCA).Equals(2))
                {
                    for (int i = 0; i < finLanBaixaBase.Count; i++)
                    {
                        System.Data.DataTable dt = new PSPartVinculaLancaAppFrm().GetRegistrosSemVinculo(finLanBaixaBase[i].CodEmpresa, finLanBaixaBase[i].CodLanca);

                        if (dt.Rows.Count > 0)
                        {
                            if (AppLib.Windows.FormMessageDefault.ShowQuestion("Existem lançamentos á serem vinculads ao lançamento " + finLanBaixaBase[i].Numero + ".\r\nGostaria de visualizar agora?") == System.Windows.Forms.DialogResult.Yes)
                            {
                                PSPartVinculaLancaAppFrm app = new PSPartVinculaLancaAppFrm();
                                app.psPartApp = new PSPartVinculaLancaApp();
                                app.psPartApp.DataGrid = this.psPartApp.DataGrid;
                                app.ShowDialog();
                                AnalisouVinculos = true;
                                
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


            // Tratamento para o flag do cheque
            checkBoxCOMCHEQUE.Checked = false;
            labelNUMEROCHEQUE.Visible = false;
            textBoxNUMEROCHEQUE.Visible = false;

        }

        private void LimpaFormulario()
        {
            psDataBaixa.Text = DateTime.Now.ToShortDateString();
            psValorLiquido.Edita = false;
            psValorLiquido.Text = "0,00";
            psValorBaixa.Text = "0,00";
            psTotalBaixar.Edita = false;
            psTotalBaixar.Text = "0,00";
            psHistorico.Text = string.Empty;
            psExtratoComo.SelectedIndex = 0;
            checkBox1.Checked = false;
            finLanBaixaBase = new List<Support.FinLanBaixaBase>();

            this.QuantosItensSelecionados();
            this.CarregaContaCaixa();
            this.CarregaListaLancamento();
        }

        private void CarregaContaCaixa()
        {
            if (ItensSelecionados == 1)
            {
                if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                {
                    if (psPartApp.DataGrid != null)
                    {
                        for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                        {
                            if (psPartApp.DataGrid.Rows[i].Selected)
                            {
                                if (gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]), "CODCONTA").Valor != null)
                                {
                                    psContaCaixa.Text = gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]), "CODCONTA").Valor.ToString();
                                }
                            }
                        }
                    }
                }

                if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
                {
                    if (gb.RetornaDataFieldByCampo(this.psPartApp.DataField, "CODCONTA").Valor != null)
                    {
                        psContaCaixa.Text = gb.RetornaDataFieldByCampo(this.psPartApp.DataField, "CODCONTA").Valor.ToString();
                    }
                }

                psContaCaixa.LoadLookup();
            }
            else
            {
                psContaCaixa.Text = string.Empty;
                psContaCaixa.LoadLookup();
            }
        }

        private void CarregaListaLancamento()
        {
            VerificarFilial();

            object[] codLanca = new object[ItensSelecionados];

            int cont = 0;
            if (this.psPartApp.Access == PS.Lib.AppAccess.View)
            {
                if (psPartApp.DataGrid != null)
                {
                    for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                    {
                        if (psPartApp.DataGrid.Rows[i].Selected)
                        {
                            codLanca[cont] = gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]), "CODLANCA").Valor;
                            cont++;
                        }
                    }
                }
            }

            if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
            {
                codLanca[cont] = gb.RetornaDataFieldByCampo(this.psPartApp.DataField, "CODLANCA").Valor;
            }

            if (codLanca.Length > 0)
            {
                string sSql = @"SELECT FLANCA.CODEMPRESA, FLANCA.CODFILIAL, FLANCA.CODLANCA, FLANCA.NUMERO, FLANCA.CODCLIFOR, VCLIFOR.NOMEFANTASIA, FLANCA.DATAVENCIMENTO, FLANCA.VLORIGINAL, FLANCA.VLLIQUIDO, 0 VALORBAIXADO
FROM FLANCA, VCLIFOR
WHERE FLANCA.CODEMPRESA = VCLIFOR.CODEMPRESA
AND FLANCA.CODCLIFOR = VCLIFOR.CODCLIFOR
AND FLANCA.CODEMPRESA = ?
AND FLANCA.CODLANCA IN ( :CODLANCA )";

                string str = string.Empty;

                for (int i = 0; i < codLanca.Length; i++)
                {
                    if (i == (codLanca.Length - 1))
                        str = str + codLanca[i];
                    else
                        str = str + codLanca[i] + ",";
                }

                sSql = sSql.Replace(":CODLANCA", str);

                DataTable Lancamentos = dbs.QuerySelect(sSql, PS.Lib.Contexto.Session.Empresa.CodEmpresa);
                foreach (DataRow row in Lancamentos.Rows)
                {
                    Support.FinLanBaixaBase lanca = new Support.FinLanBaixaBase();
                    lanca.CodEmpresa = Convert.ToInt32(row["CODEMPRESA"]);
                    lanca.CodFilial = Convert.ToInt32(row["CODFILIAL"]);
                    lanca.CodLanca = Convert.ToInt32(row["CODLANCA"]);
                    lanca.Numero = row["NUMERO"].ToString();
                    lanca.CodCliFor = row["CODCLIFOR"].ToString();
                    lanca.NomeFantasia = row["NOMEFANTASIA"].ToString();
                    lanca.DataVencimento = Convert.ToDateTime(row["DATAVENCIMENTO"]);
                    lanca.ValorOriginal = Convert.ToDecimal(row["VLORIGINAL"]);
                    lanca.ValorLiquido = Convert.ToDecimal(row["VLLIQUIDO"]);
                    lanca.ValorBaixado = lanca.ValorLiquido; //Convert.ToDecimal(row["VALORBAIXADO"]);

                    finLanBaixaBase.Add(lanca);
                }
            }

            AtualizaGrid();
        }

        private void QuantosItensSelecionados()
        {
            ItensSelecionados = 0;

            if (this.psPartApp.Access == PS.Lib.AppAccess.View)
            {
                if (psPartApp.DataGrid != null)
                {
                    for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                    {
                        if (psPartApp.DataGrid.Rows[i].Selected)
                        {
                            ItensSelecionados = ItensSelecionados + 1;
                        }
                    }
                }
            }

            if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
            {
                ItensSelecionados = 1;
            }
        }

        private void AtualizaGrid()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = finLanBaixaBase;
            if (dataGridView1.Columns.Count > 1)
            {
                dataGridView1.Columns["CODEMPRESA"].Visible = false;
                dataGridView1.Columns["CODFILIAL"].Visible = false;
                dataGridView1.Columns["CODLANCA"].HeaderText = "Lançamento";
                dataGridView1.Columns["NUMERO"].HeaderText = "Numero";
                dataGridView1.Columns["CODCLIFOR"].HeaderText = "Cliente";
                dataGridView1.Columns["NOMEFANTASIA"].HeaderText = "Nome Fantasia";
                dataGridView1.Columns["DATAVENCIMENTO"].HeaderText = "Data de Vencimento";

                dataGridView1.Columns["VALORORIGINAL"].HeaderText = "Valor Original";
                dataGridView1.Columns["VALORORIGINAL"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["VALORORIGINAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dataGridView1.Columns["VALORLIQUIDO"].HeaderText = "Valor Liquido";
                dataGridView1.Columns["VALORLIQUIDO"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["VALORLIQUIDO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dataGridView1.Columns["VALORBAIXADO"].HeaderText = "Valor da Baixa";
                dataGridView1.Columns["VALORBAIXADO"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["VALORBAIXADO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            dataGridView1_Click(this, null);
            AtualizaTotalBaixar();
        }

        private void AtualizaTotalBaixar()
        {
            decimal TotalBaixar = 0;
            foreach (Support.FinLanBaixaBase lanBase in finLanBaixaBase)
            {
                TotalBaixar = TotalBaixar + lanBase.ValorBaixado;
            }

            psTotalBaixar.Text = TotalBaixar.ToString();
        }

        private void psExtratoComo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (psExtratoComo.Value.ToString() == "0")
            {
                psNumeroExtrato.Edita = false;
                psNumeroExtrato.Text = string.Empty;
                psLookup13.Visible = false;
                psLookup16.Visible = false;
            }

            if (psExtratoComo.Value.ToString() == "1")
            {
                psNumeroExtrato.Edita = true;
                psNumeroExtrato.Text = string.Empty;
                psLookup13.Visible = true;
                psLookup16.Visible = true;
                psLookup13.textBox1.Text = string.Empty;
                psLookup13.textBox2.Text = string.Empty;
                psLookup16.textBox1.Text = string.Empty;
                psLookup16.textBox2.Text = string.Empty;
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int index = 0;
                if (dataGridView1.CurrentRow == null)
                    index = 0;
                else
                    index = dataGridView1.CurrentRow.Index;

                psValorLiquido.Text = dataGridView1.Rows[index].Cells["VALORLIQUIDO"].Value.ToString();
                psValorBaixa.Text = dataGridView1.Rows[index].Cells["VALORBAIXADO"].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (Convert.ToDecimal(psValorBaixa.Text) > 0)
                {
                    int index = 0;
                    if (dataGridView1.CurrentRow == null)
                        index = 0;
                    else
                        index = dataGridView1.CurrentRow.Index;

                    int LancaSelecionado = Convert.ToInt32(dataGridView1.Rows[index].Cells["CODLANCA"].Value);
                    foreach (Support.FinLanBaixaBase lanBase in finLanBaixaBase)
                    {
                        if (lanBase.CodLanca == LancaSelecionado)
                        {
                            lanBase.ValorBaixado = Convert.ToDecimal(psValorBaixa.Text);
                        }
                    }
                }
            }

            AtualizaGrid();
        }

        private void VerificarFilial()
        {
            bool Flag = false;
            int? Filial = null;

            if (this.psPartApp.Access == PS.Lib.AppAccess.View)
            {
                if (psPartApp.DataGrid != null)
                {
                    for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                    {
                        if (psPartApp.DataGrid.Rows[i].Selected)
                        {
                            if (Filial == null)
                            {
                                Filial = Convert.ToInt32(this.psPartApp.DataGrid.Rows[i].Cells["CODFILIAL"].Value);
                            }
                            else
                            {
                                if (Convert.ToInt32(this.psPartApp.DataGrid.Rows[i].Cells["CODFILIAL"].Value) != Filial)
                                {
                                    Flag = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (Flag)
            {
                psExtratoComo.Chave = false;
                psExtratoComo.SelectedIndex = 0;
            }
            else
            {
                psExtratoComo.Chave = true;
                psExtratoComo.SelectedIndex = 0;
            }
        }

        private void VerificarPagarReceber()
        {
            PagRec = null;

            if (this.psPartApp.Access == PS.Lib.AppAccess.View)
            {
                if (psPartApp.DataGrid != null)
                {
                    for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                    {
                        if (psPartApp.DataGrid.Rows[i].Selected)
                        {
                            if (PagRec == null)
                            {
                                PagRec = Convert.ToInt32(this.psPartApp.DataGrid.Rows[i].Cells["TIPOPAGREC"].Value);
                            }
                            else
                            {
                                if (Convert.ToInt32(this.psPartApp.DataGrid.Rows[i].Cells["TIPOPAGREC"].Value) != PagRec)
                                {
                                    throw new Exception("Baixa de Contas a Pagar e Contas a Receber devem ser realizada separadamente.");
                                }
                            }
                        }
                    }
                }
            }
        }

        private double RetornaValorBaixa(PS.Lib.AppAccess Acesso)
        {
            List<DataField> parametros = new List<DataField>();

            double valor = 0;

            if (Acesso == AppAccess.Edit)
            {
                PS.Lib.DataField dataField = new PS.Lib.DataField();
                dataField = gb.RetornaDataFieldByCampo(this.psPartApp.DataField, "VLLIQUIDO");

                valor = Convert.ToDouble(dataField.Valor);
            }

            if (Acesso == AppAccess.View)
            {
                if (psPartApp.DataGrid != null)
                {
                    for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                    {
                        if (psPartApp.DataGrid.Rows[i].Selected)
                        {
                            valor = valor + Convert.ToDouble(this.psPartApp.DataGrid.Rows[i].Cells["VLLIQUIDO"].Value);
                        }
                    }
                }
            }

            return valor;
        }

        public System.Data.DataTable GetLancamentosVinculadosParaBaixar(int CODEMPRESA, int CODLANCA)
        {
            String consulta = @"SELECT FLANCA.CODLANCA, FLANCA.VLLIQUIDO
FROM FRELLANCA, FLANCA
WHERE FRELLANCA.CODEMPRESA = FLANCA.CODEMPRESA
  AND FRELLANCA.CODLANCA = FLANCA.CODLANCA
  AND FRELLANCA.CODEMPRESA = ?
  AND FRELLANCA.CODLANCA <> ?
  AND FRELLANCA.CODRELLANCA = ( SELECT CODRELLANCA FROM FRELLANCA WHERE CODEMPRESA = ? AND CODLANCA = ? )
  AND FLANCA.CODSTATUS = 0";

            return AppLib.Context.poolConnection.Get("Start").ExecQuery(consulta, new Object[] { CODEMPRESA, CODLANCA, CODEMPRESA, CODLANCA });
        }

        public System.Data.DataTable GetLancamentosFaturadosParaBaixar(int CODEMPRESA, int CODLANCA)
        {
            String consulta = @"
SELECT FLANCA.CODLANCA, FLANCA.VLLIQUIDO
FROM FLANCA
WHERE FLANCA.CODEMPRESA = ?
  AND FLANCA.CODFATURA = ?";

            return AppLib.Context.poolConnection.Get("Start").ExecQuery(consulta, new Object[] { CODEMPRESA, CODLANCA });
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

        public override Boolean Execute()
        {
            Support.FinLanBaixaPar finLanBaixaPar = new Support.FinLanBaixaPar();
            if (PS.Lib.PSMessageBox.ShowQuestion("Confirma a baixa do(s) lançamento(s) ?") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    if (checkBoxCOMCHEQUE.Checked)
                    {
                        if (textBoxNUMEROCHEQUE.Text.Equals(""))
                        {
                            throw new Exception("Informe o número do cheque.");
                        }
                    }

                    if (psDataBaixa.Text == null)
                    {
                        throw new Exception("Informe a data da baixa.");
                    }

                    if (string.IsNullOrEmpty(psContaCaixa.Text))
                    {
                        throw new Exception("informe a conta/caixa.");
                    }

                    if (psExtratoComo.SelectedIndex == 1 && string.IsNullOrEmpty(psNumeroExtrato.Text))
                    {
                        throw new Exception("informe o número do extrato.");
                    }

                    if (string.IsNullOrEmpty(psHistorico.Text))
                    {
                        throw new Exception("Informe o histórico do extrato.");
                    }


                    VerificarPagarReceber();


                    finLanBaixaPar.CodEmpresa = PS.Lib.Contexto.Session.Empresa.CodEmpresa;
                    finLanBaixaPar.DataBaixa = Convert.ToDateTime(psDataBaixa.Text);
                    finLanBaixaPar.CodConta = psContaCaixa.Text;

                    for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
                    {
                        if (verificaNFOUDUP(Convert.ToInt32(psPartApp.DataGrid.SelectedRows[i].Cells["CODLANCA"].Value)).Equals("1"))
                        {
                            throw new Exception("Não pode baixar lançamentos faturados.");
                        }
                    }


                    if (PagRec != null)
                    {
                        if (PagRec == 0)
                            finLanBaixaPar.PagRec = Support.FinLanPagRecEnum.Pagar;
                        if (PagRec == 1)
                            finLanBaixaPar.PagRec = Support.FinLanPagRecEnum.Receber;
                    }

                    if (psExtratoComo.SelectedIndex == 0)
                        finLanBaixaPar.GeraExtratoComo = Support.FinLanBaixaGeraExtratoComoEnum.UmExtratoParaCadaLancamento;
                    else
                        finLanBaixaPar.GeraExtratoComo = Support.FinLanBaixaGeraExtratoComoEnum.UmExtratoParaTodosLancamentos;

                    finLanBaixaPar.NumeroExtrato = psNumeroExtrato.Text;
                    finLanBaixaPar.Historico = psHistorico.Text;

                    // PREPARA A LISTA DO QUE TEM QUE SER BAIXADO
                    List<int> lista_CODLANCA = new List<int>();
                    List<string> lista_NFOUDUP = new List<string>();
                    List<decimal> lista_VLBAIXADO = new List<decimal>();

                    for (int i = 0; i < finLanBaixaBase.Count; i++)
                    {
                        lista_CODLANCA.Add(finLanBaixaBase[i].CodLanca);
                        lista_VLBAIXADO.Add(finLanBaixaBase[i].ValorBaixado);
                        // INCLUI NA LISTA OS LANÇAMENTOS VINCULADOS
                        System.Data.DataTable dtBaixarVinculadosJunto = this.GetLancamentosVinculadosParaBaixar(finLanBaixaPar.CodEmpresa, finLanBaixaBase[i].CodLanca);

                        for (int x = 0; x < dtBaixarVinculadosJunto.Rows.Count; x++)
                        {
                            lista_CODLANCA.Add(int.Parse(dtBaixarVinculadosJunto.Rows[x]["CODLANCA"].ToString()));
                            lista_VLBAIXADO.Add(Convert.ToDecimal(dtBaixarVinculadosJunto.Rows[x]["VLLIQUIDO"]));
                        }
                        // INCLUI NA LISTA OS LANÇAMENTOS FATURADOS
                        System.Data.DataTable dtBaixarFaturadosJunto = this.GetLancamentosFaturadosParaBaixar(finLanBaixaPar.CodEmpresa, finLanBaixaBase[i].CodLanca);

                        for (int x = 0; x < dtBaixarFaturadosJunto.Rows.Count; x++)
                        {
                            lista_CODLANCA.Add(int.Parse(dtBaixarFaturadosJunto.Rows[x]["CODLANCA"].ToString()));
                            lista_VLBAIXADO.Add(Convert.ToDecimal(dtBaixarFaturadosJunto.Rows[x]["VLLIQUIDO"]));
                        }
                    }

                    // ALIMENTA O OBJETO COM OS PARÂMETROS DE BAIXA
                    finLanBaixaPar.CodLanca = new int[lista_CODLANCA.Count];
                    finLanBaixaPar.ValorBaixa = new Decimal[lista_VLBAIXADO.Count];
                    finLanBaixaPar.codCCusto = string.IsNullOrEmpty(psLookup13.textBox1.Text) ? null : psLookup13.textBox1.Text;
                    finLanBaixaPar.codNaturezaOrcamento = string.IsNullOrEmpty(psLookup16.textBox1.Text) ? null : psLookup16.textBox1.Text;



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
                    if (checkBoxCOMCHEQUE.Checked)
                    {
                        finLanBaixaPar.CODCHEQUE = this.IncluirCheque();
                        finLanBaixaPar.cheque = true;
                    }
                    else
                    {
                        finLanBaixaPar.cheque = false;
                    }
                    // BAIXAR
                    BaixaLancamento(finLanBaixaPar);
                    // COMPENSAR
                    if (checkBox1.Checked.Equals(true))
                    {
                        for (int i = 0; i < lista_CODLANCA.Count; i++)
                        {
                            string idextrato = AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT IDEXTRATO FROM FLANCA WHERE CODLANCA = ?", new object[] { lista_CODLANCA[i] }).ToString();
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FEXTRATO SET COMPENSADO = ?, DATACOMPENSACAO = ? WHERE CODEMPRESA = ? AND IDEXTRATO = ?", new object[] { 1, psDateBox1.Value, AppLib.Context.Empresa, idextrato });
                        }
                    }
                }
                catch (Exception ex)
                {
                    PS.Lib.PSMessageBox.ShowError(ex.Message);
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM FCHEQUE WHERE CODCHEQUE = ?", new object[] { finLanBaixaPar.CODCHEQUE });
                    return false;
                }
            }

            return true;
        }

        private void BaixaLancamento(Support.FinLanBaixaPar finLanBaixaPar)
        {
            PSPartLancaData psPartLancaData = new PSPartLancaData();
            psPartLancaData._tablename = this.psPartApp.TableName;
            psPartLancaData._keys = this.psPartApp.Keys;

            psPartLancaData.BaixaLancamento(finLanBaixaPar);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked.Equals(true))
            {
                try
                {
                    if (Convert.ToDecimal(psValorBaixa.Text) > 0)
                    {
                        psDateBox1.Enabled = true;
                        psDateBox1.Text = psDataBaixa.Text;
                    }
                    else
                    {
                        psDateBox1.Enabled = false;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void psValorBaixa_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(psValorBaixa.Text) > 0)
                {
                    checkBox1.Enabled = true;

                }
                else
                {
                    checkBox1.Enabled = false;
                    checkBox1.Checked = false;
                }
            }
            catch (FormatException)
            {

            }
        }

        private void checkBoxCOMCHEQUE_CheckedChanged(object sender, EventArgs e)
        {
            VerificarPagarReceber();
            if (checkBoxCOMCHEQUE.Checked.Equals(true))
            {
                psExtratoComo.SelectedIndex = 1;
                psExtratoComo.Enabled = false;

            }
            else
            {
                psExtratoComo.SelectedIndex = 0;
                psExtratoComo.Enabled = true;
            }

            if (PagRec != null)
            {
                if ((int)PagRec == 0)
                {
                    textBoxNUMEROCHEQUE.ReadOnly = true;
                    psNumeroExtrato.Text = textBoxNUMEROCHEQUE.Text;
                }

                if ((int)PagRec == 1)
                {
                    textBoxNUMEROCHEQUE.Text = "";
                    textBoxNUMEROCHEQUE.ReadOnly = false;
                }
            }

            labelNUMEROCHEQUE.Visible = checkBoxCOMCHEQUE.Checked;
            textBoxNUMEROCHEQUE.Visible = checkBoxCOMCHEQUE.Checked;
            psLookup13.textBox1.Text = string.Empty;
            psLookup13.textBox2.Text = string.Empty;
            psLookup16.textBox1.Text = string.Empty;
            psLookup16.textBox2.Text = string.Empty;

            //if (checkBoxCOMCHEQUE.Checked)
            //{
            //    AppLib.Windows.FormListaPrompt f = new AppLib.Windows.FormListaPrompt();
            //    f.PrimeiroItemNulo = true;
            //    f.DescricaoPrimeiroItem = "Selecione uma opção";
            //    System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FTIPOCHEQUE", new Object[] { });
            //    Object result = f.Mostrar("Tipo de cheque", dt);

            //    if (f.confirmacao == AppLib.Global.Types.Confirmacao.OK)
            //    {
            //        IDTIPOCHEQUE = int.Parse(result.ToString());
            //    }
            //    else
            //    {
            //        checkBoxCOMCHEQUE.Checked = false;
            //        IDTIPOCHEQUE = 0;
            //    }
            //}
        }

        private void psContaCaixa_AfterLookup(object sender, LookupEventArgs e)
        {
            // POR DEFAULT A CONTA CAIXA VEM SEMPRE DESMARCADA PARA FORÇAR SOLICITAR O TIPO DE CHEQUE
            checkBoxCOMCHEQUE.Checked = false;
            labelNUMEROCHEQUE.Visible = false;
            textBoxNUMEROCHEQUE.Visible = false;

            try
            {
                String CODCONTA = psContaCaixa.textBox1.Text;
                string consulta = "SELECT NUMEROCHEQUE FROM FCONTA WHERE CODEMPRESA = ? AND CODCONTA = ?";
                //string consulta = "SELECT MAX(NUMERO) FROM FCHEQUE WHERE CODCONTA = ?";
                int numero = int.Parse(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, consulta, new Object[] { AppLib.Context.Empresa, CODCONTA }).ToString());
                numero = numero + 1;
                int MASKNUMEROSEQ = int.Parse(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT MASKNUMEROSEQ FROM VPARAMETROS WHERE CODEMPRESA = ?", new Object[] { AppLib.Context.Empresa }).ToString());

                textBoxNUMEROCHEQUE.Text = AppLib.Util.Format.CompletarZeroEsquerda(MASKNUMEROSEQ, numero.ToString());
            }
            catch (Exception ex)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Erro ao obter o número do cheque.\r\nDetalhe técnico: " + ex.Message);
            }
        }

        public void IncrementaNumeroCheque(int CODEMPRESA, String CODCONTA)
        {
            String comando = "UPDATE FCONTA SET NUMEROCHEQUE = ( NUMEROCHEQUE + 1 ) WHERE CODEMPRESA = ? AND CODCONTA = ?";
            int temp = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comando, new Object[] { CODEMPRESA, CODCONTA });
        }

        public int IncluirCheque()
        {
            AppLib.ORM.Jit FCHEQUE = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "FCHEQUE");
            FCHEQUE.Set("CODEMPRESA", AppLib.Context.Empresa);
            FCHEQUE.Set("CODCONTA", psContaCaixa.Text);
            FCHEQUE.Set("NUMERO", textBoxNUMEROCHEQUE.Text);
            //FCHEQUE.Set("IDTIPOCHEQUE", null);//IDTIPOCHEQUE);
            FCHEQUE.Set("TIPOPAGREC", (int)this.PagRec);

            Decimal VALOR = Convert.ToDecimal(psValorLiquido.Text);
            FCHEQUE.Set("VALOR", Convert.ToDecimal(psTotalBaixar.Text));
            FCHEQUE.Set("DATACRIACAO", AppLib.Context.poolConnection.Get("Start").GetDateTime());
            FCHEQUE.Set("USUARIOCRIACAO", AppLib.Context.Usuario);
            FCHEQUE.Set("DATAALTERACAO", FCHEQUE.Get("DATACRIACAO"));
            FCHEQUE.Set("USUARIOALTERACAO", AppLib.Context.Usuario);

            DateTime DATABOA = DateTime.ParseExact(psDataBaixa.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            FCHEQUE.Set("DATABOA", DATABOA);
            FCHEQUE.Set("OBSERVACAO", psHistorico.Text);
            if (FCHEQUE.Insert() > 0)
            {
                try
                {
                    int CODCHEQUE = (int)AppLib.Context.poolConnection.Get("Start").GetIncrement();
                    this.IncrementaNumeroCheque(AppLib.Context.Empresa, psContaCaixa.Text);

                    return CODCHEQUE;
                }
                catch
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        private void textBoxNUMEROCHEQUE_Validating(object sender, CancelEventArgs e)
        {
            String consultaMascara = "SELECT MASKNUMEROSEQ FROM VPARAMETROS WHERE CODEMPRESA = ?";
            int MASKNUMEROSEQ = int.Parse(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, consultaMascara, new Object[] { AppLib.Context.Empresa }).ToString());

            textBoxNUMEROCHEQUE.Text = AppLib.Util.Format.CompletarZeroEsquerda(MASKNUMEROSEQ, textBoxNUMEROCHEQUE.Text);


            psNumeroExtrato.Text = textBoxNUMEROCHEQUE.Text;

        }


    }
}
