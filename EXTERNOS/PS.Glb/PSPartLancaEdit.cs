using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartLancaEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        public bool edita = false;
        public bool lancamentopadrao = false;
        public int codlanca;


        public PSPartLancaEdit(bool lancamentopadrao = false)
        {
            //lookupCliente.txtcodigo.Validated += new System.EventHandler(lookupproduto_txtcodigo_Validated);

            InitializeComponent();

            lookupCliente.txtcodigo.Leave += new System.EventHandler(lookupCliente_txtcodigo_Leave);
            lookupCliente.btnprocurar.Click += new System.EventHandler(lookupCliente_txtcodigo_Leave);
            //lookupCliente.txtconteudo.Click += new System.EventHandler(lookupCliente_txtcodigo_Leave);


            new Class.Utilidades().getDicionario(this, tabControl1, "FLANCA");

            //psLookup1.PSPart = "PSPartCliFor";
            psLookup2.PSPart = "PSPartFilial";
            psLookup3.PSPart = "PSPartMoeda";
            psLookup4.PSPart = "PSPartConta";
            psLookup5.PSPart = "PSPartTipDoc";
            psLookup6.PSPart = "PSPartCentroCusto";
            psLookup7.PSPart = "PSPartDepartamento";
            psLookup12.PSPart = "PSPartFormaPgto";
            psLookup16.PSPart = "PSPartNaturezaOrcamento";
            psLookup15.PSPart = "PSPartRepre";
            psLookup26.PSPart = "PSPartVendedor";

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 0;
            list1[0].DisplayMember = "Pagar";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 1;
            list1[1].DisplayMember = "Receber";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = 0;
            list2[0].DisplayMember = "Aberto";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = 1;
            list2[1].DisplayMember = "Baixado";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[2].ValueMember = 2;
            list2[2].DisplayMember = "Cancelado";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[3].ValueMember = 5;
            list2[3].DisplayMember = "Baixado por Parcelamento";

            psComboBox2.DataSource = list2;
            psComboBox2.DisplayMember = "DisplayMember";
            psComboBox2.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list3 = new List<PS.Lib.ComboBoxItem>();

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[0].ValueMember = "O";
            list3[0].DisplayMember = "Operação";

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[1].ValueMember = "F";
            list3[1].DisplayMember = "Financeiro";

            psComboBox3.DataSource = list3;
            psComboBox3.DisplayMember = "DisplayMember";
            psComboBox3.ValueMember = "ValueMember";

            psComboBox3.Chave = false;
            //   psDateBox3.Chave = false;
            psMaskedTextBox1.Chave = false;
            psComboBox2.Enabled = false;

            this.lancamentopadrao = lancamentopadrao;
        }

        private void lookupCliente_txtcodigo_Leave(object sender, System.EventArgs e)
        {
           
        }

        public override void CarregaParametrosTela()
        {
            base.CarregaParametrosTela();

            DataRow PARAMTIPDOC = gb.RetornaParametrosTipoDocumento(psLookup5.Text);
            if (PARAMTIPDOC != null)
            {
                if (int.Parse(PARAMTIPDOC["USANUMEROSEQ"].ToString()) == 1)
                {
                    psTextoBox2.Edita = false;
                }
            }
        }

        public override void NovoRegistro()
        {
            //base.NovoRegistro();

            //psTextoBox1.Text = "0";
            //psTextoBox1.Edita = false;
            //psComboBox1.SelectedIndex = -1;
            //psComboBox1.Chave = true;
            //psComboBox2.Chave = false;
            //// psMoedaBox8.Edita = false;
            //psMoedaBox9.Edita = false;
            //psTextoBox2.Edita = true;
            //psTextoBox3.Edita = true;
            //psLookup5.Chave = true;
            //psLookup2.Chave = true;
            //psLookup1.Chave = true;
            //psDateBox1.Chave = true;
            //psLookup3.Chave = true;
            //psMoedaBox1.Edita = true;
            //psDateBox2.Text = string.Empty;
            //psDateBox3.Text = string.Empty;
            //psDateBox4.Text = string.Empty;
            //psComboBox3.SelectedIndex = 1;

            //System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();
            //if (PARAMVAREJO != null)
            //{
            //    psLookup3.Text = (PARAMVAREJO["CODMOEDAPADRAO"] == DBNull.Value) ? string.Empty : PARAMVAREJO["CODMOEDAPADRAO"].ToString();
            //    psLookup3.LoadLookup();
            //}
        }

        public override void CarregaRegistro()
        {
            //base.CarregaRegistro();

            //psComboBox1.Chave = false;
            //psComboBox2.Chave = false;
            //// psMoedaBox8.Edita = false;
            //psMoedaBox9.Edita = false;
            //psLookup5_AfterLookup(this, null);

            //List<PS.Lib.DataField> objArr = new List<Lib.DataField>();
            //objArr.Add(new Lib.DataField("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            //objArr.Add(new Lib.DataField("CODLANCA", psTextoBox1.Text));
            //PSPartLancaData _psPartLancaData = new PSPartLancaData();
            //if (_psPartLancaData.PossuiOperacaoRelacionada(objArr))
            //{
            //    psLookup5.Chave = false;
            //    psTextoBox2.Edita = false;
            //    psLookup2.Chave = false;
            //    psLookup1.Chave = false;
            //    psDateBox1.Chave = false;
            //    psLookup3.Chave = false;
            //    psMoedaBox1.Edita = false;
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //psMoedaBox3.Text = ((Convert.ToDecimal(psMoedaBox1.Text) * Convert.ToDecimal(psMoedaBox2.Text)) / 100).ToString();
            //    //psMoedaBox5.Text = ((Convert.ToDecimal(psMoedaBox1.Text) * Convert.ToDecimal(psMoedaBox4.Text)) / 100).ToString();
            //    //psMoedaBox7.Text = ((Convert.ToDecimal(psMoedaBox1.Text) * Convert.ToDecimal(psMoedaBox6.Text)) / 100).ToString();
            //    LiquidoLancamento();
            //}
            //catch (Exception ex)
            //{
            //    PS.Lib.PSMessageBox.ShowError(ex.Message);
            //}

            this.CarregarCamposComputados();
        }

        public override void SalvaRegistro()
        {
            button2_Click(this, null);

            base.SalvaRegistro();
        }

        private void psMoedaBox2_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(psMoedaBox2.Text) >= 0)
                {
                    psMoedaBox3.Text = ((Convert.ToDecimal(psMoedaBox1.Text) * Convert.ToDecimal(psMoedaBox2.Text)) / 100).ToString();
                    LiquidoLancamento();
                }
                else
                {
                    throw new Exception("Digite um valor maior ou igual a 0.");
                }
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
                psMoedaBox2.Text = "0,00";
                psMoedaBox3.Text = "0,00";
                LiquidoLancamento();
                psMoedaBox2.Focus();
            }
        }

        private void psMoedaBox4_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(psMoedaBox4.Text) >= 0)
                {
                    psMoedaBox5.Text = ((Convert.ToDecimal(psMoedaBox1.Text) * Convert.ToDecimal(psMoedaBox4.Text)) / 100).ToString();
                    LiquidoLancamento();
                }
                else
                {
                    throw new Exception("Digite um valor maior ou igual a 0.");
                }
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
                psMoedaBox4.Text = "0,00";
                psMoedaBox4.Text = "0,00";
                LiquidoLancamento();
                psMoedaBox4.Focus();
            }
        }

        private void psMoedaBox6_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(psMoedaBox6.Text) >= 0)
                {
                    psMoedaBox7.Text = ((Convert.ToDecimal(psMoedaBox1.Text) * Convert.ToDecimal(psMoedaBox6.Text)) / 100).ToString();
                    LiquidoLancamento();
                }
                else
                {
                    throw new Exception("Digite um valor maior ou igual a 0.");
                }
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
                psMoedaBox6.Text = "0,00";
                psMoedaBox6.Text = "0,00";
                LiquidoLancamento();
                psMoedaBox6.Focus();
            }
        }

        private void LiquidoLancamento()
        {
            // psMoedaBox8.Text = ((Convert.ToDecimal(psMoedaBox1.Text) + Convert.ToDecimal(psMoedaBox5.Text) + Convert.ToDecimal(psMoedaBox7.Text)) - Convert.ToDecimal(psMoedaBox3.Text)).ToString();        
        }

        private void psDateBox2_Leave(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(psDateBox4.Text))
            //{
            //    psDateBox4.Text = psDateBox2.Text;
            //}
        }

        public void ValidaNumeracao()
        {
            //if (psLookup5.Text != string.Empty)
            //{
            //    CarregaParametrosTela();
            //}
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT USANUMEROSEQ, ULTIMONUMERO, MASKNUMEROSEQ FROM FTIPDOC INNER JOIN VPARAMETROS ON FTIPDOC.CODEMPRESA = VPARAMETROS.CODEMPRESA WHERE FTIPDOC.CODEMPRESA = ? AND CODTIPDOC = ?", new object[] { AppLib.Context.Empresa, psLookup5.textBox1.Text });
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["USANUMEROSEQ"].ToString() == "1")
                {
                    psTextoBox2.textBox1.Text = (Convert.ToInt32(dt.Rows[0]["ULTIMONUMERO"]) + 1).ToString();
                    psTextoBox2.textBox1.Text = AppLib.Util.Format.CompletarZeroEsquerda(Convert.ToInt32(dt.Rows[0]["MASKNUMEROSEQ"]), psTextoBox2.textBox1.Text);
                    psTextoBox2.Enabled = false;
                }
                else
                {
                    psTextoBox2.Enabled = true;
                    psTextoBox2.textBox1.Text = string.Empty;
                }
            }

            ValidaLookupCliente();
        }
        private void psLookup5_AfterLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            ValidaNumeracao();
        }

        private void psLookup5_BeforeLookup(object sender, Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("PAGREC", (psComboBox1.Value == null) ? -1 : psComboBox1.Value));           
        }

        private void psLookup7_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODFILIAL", psLookup2.Text));
        }

        private void psLookup1_AfterLookup(object sender, Lib.LookupEventArgs e)
        {
            if (!string.IsNullOrEmpty(psLookup2.Text))
            {
                string sSql = string.Empty;
                sSql = "SELECT FISICOJURIDICO FROM VCLIFOR WHERE CODEMPRESA = ? AND CODCLIFOR = ?";
                string fisjur = dbs.QueryValue(string.Empty, sSql, PS.Lib.Contexto.Session.Empresa.CodEmpresa, lookupCliente.txtcodigo.Text).ToString();
                sSql = "SELECT CGCCPF FROM VCLIFOR WHERE CODEMPRESA = ? AND CODCLIFOR = ?";
                string cgccpf = dbs.QueryValue(string.Empty, sSql, PS.Lib.Contexto.Session.Empresa.CodEmpresa, lookupCliente.txtcodigo.Text).ToString();

                if (fisjur == "0")
                {
                    psMaskedTextBox1.Mask = "00,000,000/0000-00";
                    psMaskedTextBox1.Text = cgccpf;
                }

                if (fisjur == "1")
                {
                    psMaskedTextBox1.Mask = "000,000,000-00";
                    psMaskedTextBox1.Text = cgccpf;
                }
            }
        }

        private void CarregarCamposComputados()
        {
            if (edita == true)
            {
                String consulta = "SELECT VLVINCAD, VLVINCDV, VLLIQUIDO FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?";
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(consulta, new Object[] { AppLib.Context.Empresa, txtCODLANCA.Text });

                String formatacao = "{0:0,0.00}";

                if (dt.Rows.Count > 0)
                {
                    textBoxVLVINCAD.Text = String.Format(formatacao, float.Parse(dt.Rows[0]["VLVINCAD"].ToString()));
                    textBoxVLVINCDV.Text = String.Format(formatacao, float.Parse(dt.Rows[0]["VLVINCDV"].ToString()));
                    textBoxVLLIQUIDO.Text = String.Format(formatacao, float.Parse(dt.Rows[0]["VLLIQUIDO"].ToString()));
                }
            }
        }

        private void PSPartLancaEdit_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                CarregaCampos();

                if (psComboBox3.Value.ToString() == "O")
                {
                    // Aba Identificação
                    txtCODLANCA.Enabled = false;
                    psComboBox1.Enabled = false;
                    psComboBox2.Enabled = false;
                    psComboBox3.Enabled = false;
                    psLookup2.Enabled = false;
                    psLookup5.Enabled = false;
                    psTextoBox2.Enabled = false;
                    psTextoBox4.Enabled = false;
                    lookupCliente.Enabled = false;
                    psMaskedTextBox1.Enabled = false;
                    dtEmissao.Enabled = false;
                    dtBaixa.Enabled = false;
                    // Aba Valores
                    psMoedaBox1.Enabled = false;
                    psLookup3.Enabled = false;
                }
                // tabPage4.Dispose();
                // this.CarregarCamposComputados();

                //if (this.ParticipaDeVinculo(AppLib.Context.Empresa, int.Parse(psTextoBox1.Text)))
                //{
                //    if (this.GetCLASSIFICACAO(AppLib.Context.Empresa, int.Parse(psTextoBox1.Text)) != 2)
                //    {
                //        AppLib.Windows.FormMessageDefault.ShowError("Não é permitido editar lançamento vinculado.");
                //        this.Close();
                //    }
                //}

                if (Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT COUNT(CODLANCA) FROM FBOLETO WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { AppLib.Context.Empresa, codlanca})) > 0)
                {
                    if (MessageBox.Show("Existem boletos vinculados a esse lançamento, deseja prosseguir?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        
                    }
                    else
                    {
                        return;
                    }
                }

            }
            else
            {
                if (this.lancamentopadrao == false)
                {
                    lookupCliente.Edita(false);
                    if (string.IsNullOrEmpty(psLookup5.textBox1.Text))
                    {
                        lookupCliente.mensagemErrorProvider = "Selecione Primeiro o Tipo de Documento";
                    }

                    psMoedaBox1.textBox1.Text = "0,00";
                    psMoedaBox2.textBox1.Text = "0,00";
                    psMoedaBox3.textBox1.Text = "0,00";
                    psMoedaBox4.textBox1.Text = "0,00";
                    psMoedaBox5.textBox1.Text = "0,00";
                    psMoedaBox6.textBox1.Text = "0,00";
                    psMoedaBox7.textBox1.Text = "0,00";
                    psMoedaBox9.textBox1.Text = "0,00";
                    psLookup3.textBox1.Text = "R$";
                    psLookup3.LoadLookup();
                    psComboBox3.Value = "F";
                    dtEmissao.DateTime = AppLib.Context.poolConnection.Get("Start").GetDateTime();
                }
                else
                {
                    dtEmissao.DateTime = AppLib.Context.poolConnection.Get("Start").GetDateTime();
                }
            }


        }

        public int GetCLASSIFICACAO(int CODEMPRESA, int CODLANCA)
        {
            String consulta = @"
SELECT ( SELECT CLASSIFICACAO FROM FTIPDOC WHERE CODEMPRESA = 1 AND CODTIPDOC = FLANCA.CODTIPDOC ) CLASSIFICACAO
FROM FLANCA
WHERE CODEMPRESA = ? AND CODLANCA = ?";

            return int.Parse(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, consulta, new Object[] { CODEMPRESA, CODLANCA }).ToString());
        }

        public Boolean ParticipaDeVinculo(int CODEMPRESA, int CODLANCA)
        {
            String consulta = "SELECT * FROM FRELLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?";
            return AppLib.Context.poolConnection.Get("Start").ExecHasRows(consulta, new Object[] { CODEMPRESA, CODLANCA });
        }

        private void psMoedaBox3_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(psMoedaBox2.Text) >= 0)
                {
                    psMoedaBox2.Text = ((Convert.ToDecimal(psMoedaBox3.Text) / Convert.ToDecimal(psMoedaBox1.Text)) * 100).ToString();
                    LiquidoLancamento();
                }
                else
                {
                    throw new Exception("Digite um valor maior ou igual a 0.");
                }
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
                psMoedaBox2.Text = "0,00";
                psMoedaBox3.Text = "0,00";
                LiquidoLancamento();
                psMoedaBox2.Focus();
            }
        }

        private void psMoedaBox5_Leave(object sender, EventArgs e)
        {

            try
            {
                if (Convert.ToDecimal(psMoedaBox2.Text) >= 0)
                {
                    psMoedaBox4.Text = ((Convert.ToDecimal(psMoedaBox5.Text) / Convert.ToDecimal(psMoedaBox1.Text)) * 100).ToString();
                    LiquidoLancamento();
                }
                else
                {
                    throw new Exception("Digite um valor maior ou igual a 0.");
                }
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
                psMoedaBox2.Text = "0,00";
                psMoedaBox3.Text = "0,00";
                LiquidoLancamento();
                psMoedaBox2.Focus();
            }
        }

        private void psMoedaBox7_Leave(object sender, EventArgs e)
        {

            try
            {
                if (Convert.ToDecimal(psMoedaBox2.Text) >= 0)
                {
                    psMoedaBox6.Text = ((Convert.ToDecimal(psMoedaBox7.Text) / Convert.ToDecimal(psMoedaBox1.Text)) * 100).ToString();
                    LiquidoLancamento();
                }
                else
                {
                    throw new Exception("Digite um valor maior ou igual a 0.");
                }
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
                psMoedaBox2.Text = "0,00";
                psMoedaBox3.Text = "0,00";
                LiquidoLancamento();
                psMoedaBox2.Focus();
            }
        }

        #region New

        public void CarregaCampos()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM FLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { codlanca, AppLib.Context.Empresa });
            if (dt.Rows.Count > 0)
            {
                #region Identificação

                if (this.lancamentopadrao == true)
                {
                    psComboBox2.Value = 0;

                    DataTable dtseqcodlanca = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT USANUMEROSEQ, ULTIMONUMERO, MASKNUMEROSEQ  FROM FTIPDOC INNER JOIN VPARAMETROS ON FTIPDOC.CODEMPRESA = VPARAMETROS.CODEMPRESA WHERE FTIPDOC.CODEMPRESA = ? AND CODTIPDOC = ?", new object[] { AppLib.Context.Empresa, dt.Rows[0]["CODTIPDOC"].ToString() });
                    if (dtseqcodlanca.Rows.Count > 0)
                    {
                        if (dtseqcodlanca.Rows[0]["USANUMEROSEQ"].ToString() == "1")
                        {
                            psTextoBox2.textBox1.Text = (Convert.ToInt32(dtseqcodlanca.Rows[0]["ULTIMONUMERO"]) + 1).ToString();
                            psTextoBox2.textBox1.Text = AppLib.Util.Format.CompletarZeroEsquerda(dtseqcodlanca.Rows[0]["MASKNUMEROSEQ"].ToString().Length, psTextoBox2.textBox1.Text);
                            psTextoBox2.Enabled = false;
                        }
                        else
                        {
                            psTextoBox2.Enabled = true;
                            psTextoBox2.textBox1.Text = string.Empty;
                        }
                    }

                    ValidaLookupCliente();
                }
                else
                {
                    txtCODLANCA.Text = dt.Rows[0]["CODLANCA"].ToString();
                    psComboBox2.Value = Convert.ToInt32(dt.Rows[0]["CODSTATUS"]);                    
                }

                psComboBox1.Value = string.IsNullOrEmpty(dt.Rows[0]["TIPOPAGREC"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["TIPOPAGREC"]);
                psComboBox3.Value = dt.Rows[0]["ORIGEM"].ToString();
                psLookup5.textBox1.Text = dt.Rows[0]["CODTIPDOC"].ToString();
                psLookup5.LoadLookup();

                psTextoBox2.textBox1.Text = dt.Rows[0]["NUMERO"].ToString();

                psLookup2.textBox1.Text = dt.Rows[0]["CODFILIAL"].ToString();
                psLookup2.LoadLookup();

                psTextoBox4.textBox1.Text = dt.Rows[0]["SEGUNDONUMERO"].ToString();

                lookupCliente.Edita(true);
                lookupCliente.mensagemErrorProvider = "";
                
                lookupCliente.txtcodigo.Text = dt.Rows[0]["CODCLIFOR"].ToString();
                lookupCliente.CarregaDescricao();

                psLookup26.textBox1.Text = dt.Rows[0]["CODVENDEDOR"].ToString();
                if (!string.IsNullOrEmpty(psLookup26.textBox1.Text))
                {
                    psLookup26.LoadLookup();
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEMISSAO"].ToString()))
                {
                    dtEmissao.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAEMISSAO"]);
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DATAVENCIMENTO"].ToString()))
                {
                    dtVencimento.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAVENCIMENTO"]);
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DATAPREVBAIXA"].ToString()))
                {
                    dtPrevBaixa.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAPREVBAIXA"]);
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DATABAIXA"].ToString()))
                {
                    dtBaixa.DateTime = Convert.ToDateTime(dt.Rows[0]["DATABAIXA"]);
                }


                psTextoBox3.textBox1.Text = dt.Rows[0]["OBSERVACAO"].ToString();
                #endregion
                #region Valores
                psLookup4.textBox1.Text = dt.Rows[0]["CODCONTA"].ToString();
                psLookup4.LoadLookup();

                psLookup3.textBox1.Text = dt.Rows[0]["CODMOEDA"].ToString();
                psLookup3.LoadLookup();

                psMoedaBox1.textBox1.Text = string.Format("{0:n2}", dt.Rows[0]["VLORIGINAL"]);
                psMoedaBox2.textBox1.Text = string.Format("{0:n2}", dt.Rows[0]["PRDESCONTO"]);
                psMoedaBox3.textBox1.Text = string.Format("{0:n2}", dt.Rows[0]["VLDESCONTO"]);
                psMoedaBox4.textBox1.Text = string.Format("{0:n2}", dt.Rows[0]["PRMULTA"]);
                psMoedaBox5.textBox1.Text = string.Format("{0:n2}", dt.Rows[0]["VLMULTA"]);
                psMoedaBox6.textBox1.Text = string.Format("{0:n2}", dt.Rows[0]["PRJUROS"]);
                psMoedaBox7.textBox1.Text = string.Format("{0:n2}", dt.Rows[0]["VLJUROS"]);
                psMoedaBox9.textBox1.Text = string.Format("{0:n2}", dt.Rows[0]["VLBAIXADO"]);

                textBoxVLVINCAD.Text = string.Format("{0:n2}", dt.Rows[0]["VLVINCAD"]);
                textBoxVLVINCDV.Text = string.Format("{0:n2}", dt.Rows[0]["VLVINCDV"]);
                textBoxVLLIQUIDO.Text = string.Format("{0:n2}", dt.Rows[0]["VLLIQUIDO"]);
                #endregion
                #region Dados Adcionais
                psLookup6.textBox1.Text = dt.Rows[0]["CODCCUSTO"].ToString();
                psLookup6.LoadLookup();

                psLookup16.textBox1.Text = dt.Rows[0]["CODNATUREZAORCAMENTO"].ToString();
                psLookup16.LoadLookup();

                psLookup12.textBox1.Text = dt.Rows[0]["CODFORMA"].ToString();
                psLookup12.LoadLookup();

                psLookup7.textBox1.Text = dt.Rows[0]["CODDEPTO"].ToString();
                psLookup7.LoadLookup();

                psLookup15.textBox1.Text = dt.Rows[0]["CODREPRE"].ToString();
                psLookup15.LoadLookup();
                #endregion
            }

        }

        public void PossuiSaldoFinanceiro(int CodEmpresa, string CodCliFor, int CodLanca, decimal ValorAdicional)
        {
            System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();
            if (PARAMVAREJO == null)
            {
                throw new Exception("Não foi possível carregar os parâmetro globais.");
            }
            else
            {
                if (Convert.ToInt32(PARAMVAREJO["CONTROLALIMITECREDITO"]) == 1)
                {
                    decimal ValorAberto = 0;
                    string sSql = @"SELECT ISNULL(X.VALORABERTO,0) VALORABERTO
FROM
(
SELECT 
SUM(ISNULL(L.VLORIGINAL,0)) - SUM(ISNULL(L.VLBAIXADO,0))  VALORABERTO
FROM   FLANCA L (NOLOCK), FTIPDOC D (NOLOCK), VCLIFOR F
WHERE  L.CODSTATUS IN (0) 
AND L.CODEMPRESA = D.CODEMPRESA 
AND L.CODTIPDOC = D.CODTIPDOC 
AND D.CLASSIFICACAO <> 0
AND D.CLASSIFICACAO <> 1
AND D.CLASSIFICACAO <> 3
AND L.TIPOPAGREC = 1
AND F.CODEMPRESA = L.CODEMPRESA
AND F.CODCLIFOR = L.CODCLIFOR
AND L.CODEMPRESA = ?
AND L.CODCLIFOR = ?
/**/
)X";

                    if (CodLanca > 0)
                    {
                        sSql = sSql.Replace("/**/", "AND L.CODLANCA <> ?");
                        ValorAberto = Convert.ToDecimal(dbs.QueryValue(0, sSql, CodEmpresa, CodCliFor, CodLanca));
                    }
                    else
                    {
                        ValorAberto = Convert.ToDecimal(dbs.QueryValue(0, sSql, CodEmpresa, CodCliFor));
                    }

                    sSql = @"SELECT LIMITECREDITO FROM VCLIFOR WHERE CODEMPRESA = ? AND CODCLIFOR = ?";
                    decimal LimiteCredito = Convert.ToDecimal(dbs.QueryValue(0, sSql, CodEmpresa, CodCliFor));
                    decimal Saldo = LimiteCredito - (ValorAberto + ValorAdicional);
                    if (Saldo < 0)
                    {
                        throw new Exception("Cliente/Fornecedor não tem crédito disponivel.");
                    }
                }
            }
        }

        private bool gravaLancamentoPadrao()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();
            try
            {
                //IDENTIFICAÇÃO
                AppLib.ORM.Jit FLANCA = new AppLib.ORM.Jit(conn, "FLANCA");
                AppLib.ORM.Jit FLANCAPADRAO = new AppLib.ORM.Jit(conn, "FLANCAPADRAO");

                DateTime dtvencimento = Convert.ToDateTime(dtVencimento.Text);
                int codLancaPrincipal = getCodLanca(conn);
                int codLancaParcela = 0;

                for (int i = 1; i <= Convert.ToInt16(txtNPARCELA.Text); i ++)
                {
                    FLANCA.Set("CODEMPRESA", AppLib.Context.Empresa);

                    if (i == 1)
                    {
                        FLANCA.Set("CODLANCA", codLancaPrincipal);
                    }
                    else
                    {
                        codLancaParcela = getCodLanca(conn);
                        FLANCA.Set("CODLANCA", codLancaParcela);
                    }
                        
                    FLANCA.Set("TIPOPAGREC", psComboBox1.Value);

                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT USANUMEROSEQ, ULTIMONUMERO, MASKNUMEROSEQ  FROM FTIPDOC INNER JOIN VPARAMETROS ON FTIPDOC.CODEMPRESA = VPARAMETROS.CODEMPRESA WHERE FTIPDOC.CODEMPRESA = ? AND CODTIPDOC = ?", new object[] { AppLib.Context.Empresa, psLookup5.textBox1.Text });
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["USANUMEROSEQ"].ToString() == "1")
                        {
                            psTextoBox2.textBox1.Text = (Convert.ToInt32(dt.Rows[0]["ULTIMONUMERO"]) + i).ToString();
                            psTextoBox2.textBox1.Text = AppLib.Util.Format.CompletarZeroEsquerda(Convert.ToInt32(dt.Rows[0]["MASKNUMEROSEQ"]), psTextoBox2.textBox1.Text);
                            FLANCA.Set("NUMERO", psTextoBox2.textBox1.Text);
                        }
                        else
                        {
                            FLANCA.Set("NUMERO", psTextoBox2.textBox1.Text + "/" + i.ToString("00"));
                        }
                    }
                    
                    FLANCA.Set("CODCLIFOR", lookupCliente.txtcodigo.Text);
                    FLANCA.Set("NOMECLIENTE", lookupCliente.txtconteudo.Text);
                    FLANCA.Set("CODFILIAL", psLookup2.textBox1.Text);

                    if (!string.IsNullOrEmpty(dtEmissao.Text))
                    {
                        FLANCA.Set("DATAEMISSAO", Convert.ToDateTime(dtEmissao.Text));
                    }
                    if (!string.IsNullOrEmpty(dtVencimento.Text))
                    {
                        if (i == 1)
                        {
                            FLANCA.Set("DATAVENCIMENTO", Convert.ToDateTime(dtvencimento));
                            FLANCA.Set("DATAPREVBAIXA", Convert.ToDateTime(dtPrevBaixa.Text));
                        }
                        else
                        {
                            if (Convert.ToInt16(txtDIAFIXO.Text) > 0)
                            {
                                dtvencimento = dtvencimento.AddMonths(1);
                                dtvencimento = new DateTime(dtvencimento.Year, dtvencimento.Month, Convert.ToInt16(txtDIAFIXO.Text));
                                FLANCA.Set("DATAVENCIMENTO", dtvencimento);
                            }
                            else
                            {
                                dtvencimento = dtvencimento.AddDays((Convert.ToInt16(txtPRAZOVENCTO.Text)));
                                FLANCA.Set("DATAVENCIMENTO", dtvencimento);
                            }
                            //new DateTime(DateTime.Now.Year, 1, 1);
                        }
                    }

                    FLANCA.Set("DATAPREVBAIXA", dtvencimento);

                    if (!string.IsNullOrEmpty(dtBaixa.Text))
                    {
                        FLANCA.Set("DATABAIXA", Convert.ToDateTime(dtBaixa.Text));
                    }

                    FLANCA.Set("OBSERVACAO", psTextoBox3.textBox1.Text);
                    FLANCA.Set("CODMOEDA", psLookup3.textBox1.Text);
                    FLANCA.Set("VLORIGINAL", Convert.ToDecimal(psMoedaBox1.textBox1.Text));
                    FLANCA.Set("PRDESCONTO", Convert.ToDecimal(psMoedaBox2.textBox1.Text));
                    FLANCA.Set("VLDESCONTO", Convert.ToDecimal(psMoedaBox3.textBox1.Text));
                    FLANCA.Set("PRMULTA", Convert.ToDecimal(psMoedaBox4.textBox1.Text));
                    FLANCA.Set("VLMULTA", Convert.ToDecimal(psMoedaBox5.textBox1.Text));
                    FLANCA.Set("PRJUROS", Convert.ToDecimal(psMoedaBox6.textBox1.Text));
                    FLANCA.Set("VLJUROS", Convert.ToDecimal(psMoedaBox7.textBox1.Text));
                    FLANCA.Set("CODSTATUS", psComboBox2.Value);

                    FLANCA.Set("CODCONTA", string.IsNullOrEmpty(psLookup4.textBox1.Text) ? null : psLookup4.textBox1.Text);
                    FLANCA.Set("CODTIPDOC", string.IsNullOrEmpty(psLookup5.textBox1.Text) ? null : psLookup5.textBox1.Text);
                    FLANCA.Set("VLBAIXADO", Convert.ToDecimal(psMoedaBox9.textBox1.Text));
                    FLANCA.Set("SEGUNDONUMERO", psTextoBox4.textBox1.Text);
                    FLANCA.Set("CODCCUSTO", string.IsNullOrEmpty(psLookup6.textBox1.Text) ? null : psLookup6.textBox1.Text);
                    FLANCA.Set("CODDEPTO", string.IsNullOrEmpty(psLookup7.textBox1.Text) ? null : psLookup7.textBox1.Text);
                    FLANCA.Set("CODFORMA", string.IsNullOrEmpty(psLookup12.textBox1.Text) ? null : psLookup12.textBox1.Text);
                    FLANCA.Set("ORIGEM", psComboBox3.Value);
                    FLANCA.Set("DATACRIACAO", conn.GetDateTime());
                    FLANCA.Set("CODUSUARIOCRIACAO", AppLib.Context.Usuario);

                    FLANCA.Set("CODNATUREZAORCAMENTO", string.IsNullOrEmpty(psLookup16.textBox1.Text) ? null : psLookup16.textBox1.Text);
                    FLANCA.Set("VLVINCAD", string.IsNullOrEmpty(textBoxVLVINCAD.Text) ? 0 : Convert.ToDecimal(textBoxVLVINCAD.Text));
                    FLANCA.Set("VLVINCDV", string.IsNullOrEmpty(textBoxVLVINCDV.Text) ? 0 : Convert.ToDecimal(textBoxVLVINCDV.Text));
                    FLANCA.Set("VLLIQUIDO", string.IsNullOrEmpty(textBoxVLLIQUIDO.Text) ? 0 : Convert.ToDecimal(textBoxVLLIQUIDO.Text));
                    FLANCA.Set("NFOUDUP", "3");
                    FLANCA.Set("NSEQLANCA", 1);
                    FLANCA.Set("CODREPRE", string.IsNullOrEmpty(psLookup15.textBox1.Text) ? null : psLookup15.textBox1.Text);
                    FLANCA.Set("CODDEPTO", string.IsNullOrEmpty(psLookup7.textBox1.Text) ? null : psLookup7.textBox1.Text);
                    FLANCA.Set("CODVENDEDOR", string.IsNullOrEmpty(psLookup26.textBox1.Text) ? null : psLookup26.textBox1.Text);
                    if (edita == false)
                    {
                        conn.ExecTransaction("UPDATE GLOG SET IDLOG = ? WHERE CODTABELA = ? AND CODEMPRESA = ?", new object[] { FLANCA.Get("CODLANCA"), "FLANCA", AppLib.Context.Empresa });
                    }

                    FLANCA.Save();

                    if (i > 1)
                    {
                        FLANCAPADRAO.Set("CODEMPRESA", AppLib.Context.Empresa);
                        FLANCAPADRAO.Set("CODLANCA", codLancaPrincipal);
                        FLANCAPADRAO.Set("CODLANCARELAC", codLancaParcela);
                    }
                    else
                    {
                        FLANCAPADRAO.Set("CODEMPRESA", AppLib.Context.Empresa);
                        FLANCAPADRAO.Set("CODLANCA", codLancaPrincipal);
                        FLANCAPADRAO.Set("CODLANCARELAC", codLancaPrincipal);
                    }

                    FLANCAPADRAO.Save();
                }

                conn.ExecTransaction("UPDATE FTIPDOC SET ULTIMONUMERO = ? WHERE CODEMPRESA = ? AND CODTIPDOC = ? ", new object[] { FLANCA.Get("NUMERO"), AppLib.Context.Empresa, FLANCA.Get("CODTIPDOC"), });
                
                codlanca = Convert.ToInt32(FLANCA.Get("CODLANCA").ToString());
                txtCODLANCA.Text = codlanca.ToString();
                conn.Commit();
                CarregaCampos();
                edita = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Rollback();
                edita = false;
                return false;
            }
        }

        private bool salvar()
        {
            bool _valida = true;

            lookupCliente.mensagemErrorProvider = "";

            PS.Glb.Class.Utilidades util = new PS.Glb.Class.Utilidades();

            bool validaCamposObrigatorios = util.validaCamposObrigatorios(this, ref errorProvider);

            if (edita == false)
            {
                if (Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT COUNT (*) FROM FLANCA WHERE CODCLIFOR = ? AND TIPOPAGREC = ? AND NUMERO = ? AND CODFILIAL = ? AND CODTIPDOC = ? AND CODEMPRESA = ?", new object[] { lookupCliente.txtcodigo.Text, psComboBox1.Value, psTextoBox2.textBox1.Text, psLookup2.textBox1.Text, psLookup5.textBox1.Text, AppLib.Context.Empresa })) > 0)
                {
                    throw new Exception("Lançamento já existe.");
                }
            }

            //if (string.IsNullOrEmpty(psTextoBox2.textBox1.Text))
            //{
            //    errorProvider.SetError(psTextoBox2, "Favor preencher o número do lançamento.");
            //    _valida = false;
            //}

            if (psComboBox2.Text != "Aberto")
            {
                errorProvider.SetError(psComboBox2, "Somente lançamento com o status " + "Aberto" + " pode ser editado.");
                _valida = false;
            }

            if (psComboBox1.Value == null)
            {
                errorProvider.SetError(psComboBox1, "Favor preencher o campo tipo de pagamento corretamente.");
                _valida = false;
            }

            if (string.IsNullOrEmpty(psLookup5.textBox1.Text))
            {
                errorProvider.SetError(psLookup5, "Favor preencher o campo tipo de documento corretamente.");
                _valida = false;
            }else
            {
                if (string.IsNullOrEmpty(psTextoBox2.textBox1.Text))
                {
                    System.Data.DataRow PARAMTIPDOC = gb.RetornaParametrosTipoDocumento(psLookup5.textBox1.Text);

                    if (int.Parse(PARAMTIPDOC["USANUMEROSEQ"].ToString()) == 0)
                    {
                        errorProvider.SetError(psTextoBox2, "Favor preencher o campo Número corretamente.");
                        _valida = false;
                    }
                }
                else
                {
                    System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();
                    if (PARAMVAREJO == null)
                    {
                        throw new Exception("Parametros do módulo não foram lozalicados.");
                    }
                    else
                    {
                        int mask = (PARAMVAREJO["MASKNUMEROSEQ"] == DBNull.Value) ? 0 : Convert.ToInt32(PARAMVAREJO["MASKNUMEROSEQ"]);
                        if (mask == 0)
                        {
                            throw new Exception("Máscara do numero do documento não está parametrizada.");
                        }
                        else
                        {
                            if (psTextoBox2.textBox1.Text.Length > mask)
                            {
                                throw new Exception("Quantidade de caracteres do número do documento é maior que a quantidade permitida.");
                            }
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(psLookup2.textBox1.Text))
            {
                errorProvider.SetError(psLookup2, "Favor preencher o campo Cód. Filial corretamente.");
                _valida = false;
            }
            if (string.IsNullOrEmpty(lookupCliente.txtcodigo.Text))
            {
                lookupCliente.mensagemErrorProvider = "Favor preencher o campo Cód. Cliente corretamente.";
                _valida = false;
            }
            if (string.IsNullOrEmpty(dtEmissao.Text))
            {
                errorProvider.SetError(dtEmissao, "Favor preencher o campo Data de Emissão corretamente.");
                _valida = false;
            }
            if (string.IsNullOrEmpty(dtVencimento.Text))
            {
                errorProvider.SetError(dtVencimento, "Favor preencher o campo Data de Vencimento corretamente.");
                _valida = false;
            }

            if (string.IsNullOrEmpty(dtPrevBaixa.Text))
            {
                errorProvider.SetError(dtPrevBaixa, "Favor preencher o campo Data de Previsão corretamente.");
                _valida = false;
            }

            if (!string.IsNullOrEmpty(dtPrevBaixa.Text) && !string.IsNullOrEmpty(dtPrevBaixa.Text))
            { 
                if (Convert.ToDateTime(dtPrevBaixa.Text) < Convert.ToDateTime(dtEmissao.Text))
                {
                    errorProvider.SetError(dtPrevBaixa, "Data de previsão de baixa não pode ser menor que Data de Emissão.");
                    _valida = false;
                }

                if (Convert.ToDateTime(dtVencimento.Text) < Convert.ToDateTime(dtEmissao.Text))
                {
                    errorProvider.SetError(dtVencimento, "Data de Vencimento não pode ser menor que Data de Emissão.");
                    _valida = false;
                }
            }
            
            if (string.IsNullOrEmpty(psLookup3.textBox1.Text))
            {
                errorProvider.SetError(psLookup3, "Favor preencher o campo Cód. Moeda corretamente.");
                _valida = false;
            }

            if (Convert.ToDecimal(psMoedaBox1.textBox1.Text) <= 0)
            {
                errorProvider.SetError(psMoedaBox1, "Valor Original deve ser maior que Zero.");
                _valida = false;
            }
            //if (Convert.ToInt32(psComboBox1.Value) == 1)
            //{
            //    this.PossuiSaldoFinanceiro(AppLib.Context.Empresa, psLookup1.textBox1.Text, Convert.ToInt32(txtCODLANCA.Text), Convert.ToDecimal(textBoxVLLIQUIDO.Text));
            //}

            if (this.lancamentopadrao == true)
            {
                if (Convert.ToInt16(txtNPARCELA.Text) <= 0)
                {
                    errorProvider.SetError(txtNPARCELA, "Favor preencher o número de parcelas.");
                    _valida = false;
                }

                int resultDiaFixo = 0;
                int.TryParse(txtDIAFIXO.Text, out resultDiaFixo);

                if ((Convert.ToInt16(txtPRAZOVENCTO.Text) <= 0) && (Convert.ToInt16(txtDIAFIXO.Text) <= 0))
                {
                    errorProvider.SetError(txtPRAZOVENCTO, "Favor preencher o prazo do vencimento ou o dia fixo.");
                    errorProvider.SetError(txtDIAFIXO, "Favor preencher o prazo do vencimento ou o dia fixo.");
                    _valida = false;
                }
            }

            if (validaCamposObrigatorios == false || _valida == false)
            {
                _valida = false;
                return false;
            }

            if (this.lancamentopadrao == true && edita == false)
            {
                if (gravaLancamentoPadrao() == true)
                {
                    return true;
                }
                return false;
            }
            else
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                conn.BeginTransaction();
                try
                {
                    //IDENTIFICAÇÃO
                    AppLib.ORM.Jit FLANCA = new AppLib.ORM.Jit(conn, "FLANCA");
                    FLANCA.Set("CODEMPRESA", AppLib.Context.Empresa);
                    if (edita == false)
                    {
                        FLANCA.Set("CODLANCA", getCodLanca(conn));
                    }
                    else
                    {
                        FLANCA.Set("CODLANCA", txtCODLANCA.Text);
                    }

                    FLANCA.Set("TIPOPAGREC", psComboBox1.Value);
                    FLANCA.Set("NUMERO", psTextoBox2.textBox1.Text);
                    FLANCA.Set("CODCLIFOR", lookupCliente.txtcodigo.Text);
                    FLANCA.Set("NOMECLIENTE", lookupCliente.txtconteudo.Text);
                    FLANCA.Set("CODFILIAL", psLookup2.textBox1.Text);
                    if (!string.IsNullOrEmpty(dtEmissao.Text))
                    {
                        FLANCA.Set("DATAEMISSAO", Convert.ToDateTime(dtEmissao.Text));
                    }
                    if (!string.IsNullOrEmpty(dtVencimento.Text))
                    {
                        FLANCA.Set("DATAVENCIMENTO", Convert.ToDateTime(dtVencimento.Text));
                    }
                    if (!string.IsNullOrEmpty(dtPrevBaixa.Text))
                    {
                        FLANCA.Set("DATAPREVBAIXA", Convert.ToDateTime(dtPrevBaixa.Text));
                    }
                    if (!string.IsNullOrEmpty(dtBaixa.Text))
                    {
                        FLANCA.Set("DATABAIXA", Convert.ToDateTime(dtBaixa.Text));
                    }

                    FLANCA.Set("OBSERVACAO", psTextoBox3.textBox1.Text);
                    FLANCA.Set("CODMOEDA", psLookup3.textBox1.Text);
                    FLANCA.Set("VLORIGINAL", Convert.ToDecimal(psMoedaBox1.textBox1.Text));
                    FLANCA.Set("PRDESCONTO", Convert.ToDecimal(psMoedaBox2.textBox1.Text));
                    FLANCA.Set("VLDESCONTO", Convert.ToDecimal(psMoedaBox3.textBox1.Text));
                    FLANCA.Set("PRMULTA", Convert.ToDecimal(psMoedaBox4.textBox1.Text));
                    FLANCA.Set("VLMULTA", Convert.ToDecimal(psMoedaBox5.textBox1.Text));
                    FLANCA.Set("PRJUROS", Convert.ToDecimal(psMoedaBox6.textBox1.Text));
                    FLANCA.Set("VLJUROS", Convert.ToDecimal(psMoedaBox7.textBox1.Text));
                    FLANCA.Set("CODSTATUS", psComboBox2.Value);
                    //FLANCA.Set("CODOPER", );
                    FLANCA.Set("CODCONTA", string.IsNullOrEmpty(psLookup4.textBox1.Text) ? null : psLookup4.textBox1.Text);
                    FLANCA.Set("CODTIPDOC", string.IsNullOrEmpty(psLookup5.textBox1.Text) ? null : psLookup5.textBox1.Text);
                    FLANCA.Set("VLBAIXADO", Convert.ToDecimal(psMoedaBox9.textBox1.Text));
                    FLANCA.Set("SEGUNDONUMERO", psTextoBox4.textBox1.Text);
                    FLANCA.Set("CODCCUSTO", string.IsNullOrEmpty(psLookup6.textBox1.Text) ? null : psLookup6.textBox1.Text);
                    FLANCA.Set("CODDEPTO", string.IsNullOrEmpty(psLookup7.textBox1.Text) ? null : psLookup7.textBox1.Text);
                    FLANCA.Set("CODFORMA", string.IsNullOrEmpty(psLookup12.textBox1.Text) ? null : psLookup12.textBox1.Text);
                    //FLANCA.Set("IDEXTRATO", );
                    FLANCA.Set("ORIGEM", psComboBox3.Value);
                    FLANCA.Set("DATACRIACAO", conn.GetDateTime());
                    FLANCA.Set("CODUSUARIOCRIACAO", AppLib.Context.Usuario);
                    //FLANCA.Set("DATACANCELAMENTO", );
                    //FLANCA.Set("CODUSUARIOCANCELAMENTO", );
                    //FLANCA.Set("MOTIVOCANCELAMENTO", );
                    //FLANCA.Set("DATACANCELAMENTOBAIXA", );
                    //FLANCA.Set("CODUSUARIOCANCELAMENTOBAIXA", );
                    //FLANCA.Set("MOTIVOCANCELAMENTOBAIXA", );
                    FLANCA.Set("CODNATUREZAORCAMENTO", string.IsNullOrEmpty(psLookup16.textBox1.Text) ? null : psLookup16.textBox1.Text);
                    FLANCA.Set("VLVINCAD", string.IsNullOrEmpty(textBoxVLVINCAD.Text) ? 0 : Convert.ToDecimal(textBoxVLVINCAD.Text));
                    FLANCA.Set("VLVINCDV", string.IsNullOrEmpty(textBoxVLVINCDV.Text) ? 0 : Convert.ToDecimal(textBoxVLVINCDV.Text));
                    FLANCA.Set("VLLIQUIDO", string.IsNullOrEmpty(textBoxVLLIQUIDO.Text) ? 0 : Convert.ToDecimal(textBoxVLLIQUIDO.Text));
                    //FLANCA.Set("CODFATURA", );
                    FLANCA.Set("NFOUDUP", "3");
                    FLANCA.Set("NSEQLANCA", 1);
                    FLANCA.Set("CODREPRE", string.IsNullOrEmpty(psLookup15.textBox1.Text) ? null : psLookup15.textBox1.Text);
                    FLANCA.Set("CODDEPTO", string.IsNullOrEmpty(psLookup7.textBox1.Text) ? null : psLookup7.textBox1.Text);
                    FLANCA.Set("CODVENDEDOR", string.IsNullOrEmpty(psLookup26.textBox1.Text) ? null : psLookup26.textBox1.Text);

                    if (edita == false)
                    {
                        conn.ExecTransaction("UPDATE GLOG SET IDLOG = ? WHERE CODTABELA = ? AND CODEMPRESA = ?", new object[] { FLANCA.Get("CODLANCA"), "FLANCA", AppLib.Context.Empresa });
                    }
                    FLANCA.Save();
                    conn.ExecTransaction("UPDATE FTIPDOC SET ULTIMONUMERO = ? WHERE CODEMPRESA = ? AND CODTIPDOC = ? ", new object[] { FLANCA.Get("NUMERO"), AppLib.Context.Empresa, FLANCA.Get("CODTIPDOC"), });
                    conn.Commit();
                    txtCODLANCA.Text = FLANCA.Get("CODLANCA").ToString();
                    edita = true;
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    conn.Rollback();
                    edita = false;
                    return false;
                }
            }
            
        }

        private int getCodLanca(AppLib.Data.Connection conn)
        {
            int retorno = Convert.ToInt32(conn.ExecGetField(0, "SELECT IDLOG FROM GLOG WHERE CODTABELA = 'FLANCA' AND CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }));
            if (retorno > 0)
            {
                return retorno + 1;
            }
            else
            {
                return 0;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {


        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {

        }

        private void dtVencimento_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(dtVencimento.Text))
            {
                dtPrevBaixa.DateTime = dtVencimento.DateTime;
            }
        }

        #endregion

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            if (salvar() == true)
            {
                MessageBox.Show("Lançamento salvo com sucesso.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Não foi possível concluir o Lançamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (salvar() == true)
            {
                this.Dispose();
                GC.Collect();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();

        }

        private void psComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            ValidaLookupCliente();

            grplancamentopadrao.Enabled = false;
            grplancamentopadrao.Visible = true;
            chklancamentopadrao.Visible = true;
            chklancamentopadrao.Checked = false;
            lancamentopadrao = false;
        }

        private void ValidaLookupCliente()
        {
            if (!string.IsNullOrEmpty(psLookup5.textBox1.Text))
            {
                lookupCliente.Edita(true);
                lookupCliente.mensagemErrorProvider = "";
                

                string _classificacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CLASSIFICACAO FROM FTIPDOC WHERE FTIPDOC.CODTIPDOC = ? AND FTIPDOC.PAGREC = ?", new object[] { psLookup5.textBox1.Text ,psComboBox1.Value }).ToString();

                if (_classificacao == "1") //DEVOLUCAO
                {
                    if (psComboBox1.Value.ToString() == "0") //PAGAR
                    {
                        lookupCliente.Grid_WhereVisao[3].ValorFixo = "0,2";
                    }
                    else //RECEBER = 1
                    {
                        lookupCliente.Grid_WhereVisao[3].ValorFixo = "1,2";
                    }
                }
                else
                {
                    if (psComboBox1.Value.ToString() == "0") //PAGAR
                    {
                        lookupCliente.Grid_WhereVisao[3].ValorFixo = "1,2";
                    }
                    else //RECEBER = 1
                    {
                        lookupCliente.Grid_WhereVisao[3].ValorFixo = "0,2";
                    }
                }
            }
            else
            {
                lookupCliente.Edita(false);
                if (string.IsNullOrEmpty(psLookup5.textBox1.Text))
                {
                    lookupCliente.mensagemErrorProvider = "Selecione Primeiro o Tipo de Documento";
                }
            }
        }

        private void chklancamentopadrao_CheckedChanged(object sender, EventArgs e)
        {
            if (chklancamentopadrao.Checked == true)
            {
                grplancamentopadrao.Enabled = true;
                this.lancamentopadrao = true;
                this.edita = false;
            }
            else
            {
                grplancamentopadrao.Enabled = false;
                this.lancamentopadrao = false;
                this.edita = false;
            }
        }

        private void txtDIAFIXO_EditValueChanged(object sender, EventArgs e)
        {
            txtPRAZOVENCTO.Text = "0";
        }

        private void txtPRAZOVENCTO_EditValueChanged(object sender, EventArgs e)
        {
            txtDIAFIXO.Text = "0";
        }
    }
}
