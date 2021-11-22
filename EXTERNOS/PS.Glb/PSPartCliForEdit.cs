using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PS.Glb;

namespace PS.Glb
{
    public partial class PSPartCliForEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public PSPartCliForEdit()
        {
            InitializeComponent();
            buttonCANCELAR.Location = new Point(551, 19);
            buttonOK.Location = new Point(470, 19);
            buttonSALVAR.Location = new Point(389, 19);

            psLookup1.PSPart = "PSPartTipoRua";
            psLookup2.PSPart = "PSPartTipoBairro";
            psLookup3.PSPart = "PSPartPais";
            psLookup4.PSPart = "PSPartEstado";
            psLookup5.PSPart = "PSPartCidade";
            psLookup6.PSPart = "PSPartEstCiv";

            psLookup7.PSPart = "PSPartTipoRua";
            psLookup8.PSPart = "PSPartTipoBairro";
            psLookup9.PSPart = "PSPartPais";
            psLookup10.PSPart = "PSPartEstado";
            psLookup11.PSPart = "PSPartCidade";

            psLookup12.PSPart = "PSPartTipoRua";
            psLookup13.PSPart = "PSPartTipoBairro";
            psLookup14.PSPart = "PSPartPais";
            psLookup15.PSPart = "PSPartEstado";
            psLookup16.PSPart = "PSPartCidade";
            psLookup17.PSPart = "PSPartEstado";
            psLookup18.PSPart = "PSPartRepre";
            psLookup26.PSPart = "PSPartVendedor";
            psLookup19.PSPart = "PSPartConta";
            psLookup20.PSPart = "PSPartTransportadora";
            psLookup21.PSPart = "PSPartCentroCusto";
            psLookup22.PSPart = "PSPartCondicaoPgto";
            psLookup23.PSPart = "PSPartCondicaoPgto";
            psLookup24.PSPart = "PSPartNaturezaOrcamento";
            psLookup25.PSPart = "PSPartTipoCliente";
            psLookup27.PSPart = "PSPartFormaPgto";

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 0;
            list1[0].DisplayMember = "Cliente";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 1;
            list1[1].DisplayMember = "Fornecedor";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = 2;
            list1[2].DisplayMember = "Ambos";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = 0;
            list2[0].DisplayMember = "Jurídica";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = 1;
            list2[1].DisplayMember = "Física";

            psComboBox2.DataSource = list2;
            psComboBox2.DisplayMember = "DisplayMember";
            psComboBox2.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list3 = new List<PS.Lib.ComboBoxItem>();

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[0].ValueMember = 9;
            list3[0].DisplayMember = "";

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[1].ValueMember = 0;
            list3[1].DisplayMember = "Privada";

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[2].ValueMember = 1;
            list3[2].DisplayMember = "Publica";

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[3].ValueMember = 2;
            list3[3].DisplayMember = "Cooperativa";

            psComboBox3.DataSource = list3;
            psComboBox3.DisplayMember = "DisplayMember";
            psComboBox3.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list4 = new List<PS.Lib.ComboBoxItem>();

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[0].ValueMember = 9;
            list4[0].DisplayMember = "";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[1].ValueMember = 0;
            list4[1].DisplayMember = "ME";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[2].ValueMember = 1;
            list4[2].DisplayMember = "EPP";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[3].ValueMember = 2;
            list4[3].DisplayMember = "Normal";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[4].ValueMember = 3;
            list4[4].DisplayMember = "Outros";

            psComboBox4.DataSource = list4;
            psComboBox4.DisplayMember = "DisplayMember";
            psComboBox4.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list5 = new List<PS.Lib.ComboBoxItem>();
            list5.Add(new PS.Lib.ComboBoxItem());
            list5[0].ValueMember = 9;
            list5[0].DisplayMember = "";

            list5.Add(new PS.Lib.ComboBoxItem());
            list5[1].ValueMember = 0;
            list5[1].DisplayMember = "Contribuinte ICMS";
            //1
            list5.Add(new PS.Lib.ComboBoxItem());
            list5[2].ValueMember = 1;
            list5[2].DisplayMember = "Contribuinte Isento";
            //2
            list5.Add(new PS.Lib.ComboBoxItem());
            list5[3].ValueMember = 2;
            list5[3].DisplayMember = "Não Contribuinte";
            //0--9

            psComboBox5.DataSource = list5;
            psComboBox5.DisplayMember = "DisplayMember";
            psComboBox5.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list6 = new List<PS.Lib.ComboBoxItem>();

            list6.Add(new PS.Lib.ComboBoxItem());
            list6[0].ValueMember = 0;
            list6[0].DisplayMember = "Brasileira";

            list6.Add(new PS.Lib.ComboBoxItem());
            list6[1].ValueMember = 1;
            list6[1].DisplayMember = "Estrangeira";

            psComboBox6.DataSource = list6;
            psComboBox6.DisplayMember = "DisplayMember";
            psComboBox6.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list7 = new List<PS.Lib.ComboBoxItem>();

            list7.Add(new PS.Lib.ComboBoxItem());
            list7[0].ValueMember = 0;
            list7[0].DisplayMember = "CIF";

            list7.Add(new PS.Lib.ComboBoxItem());
            list7[1].ValueMember = 1;
            list7[1].DisplayMember = "FOB";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[2].ValueMember = 2;
            list2[2].DisplayMember = "Terceiro";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[3].ValueMember = 3;
            list2[3].DisplayMember = "Sem Frete";

            psComboBox7.DataSource = list7;
            psComboBox7.DisplayMember = "DisplayMember";
            psComboBox7.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listCLASSVENDA = new List<PS.Lib.ComboBoxItem>();

            listCLASSVENDA.Add(new PS.Lib.ComboBoxItem());
            listCLASSVENDA[0].ValueMember = "V";
            listCLASSVENDA[0].DisplayMember = "Venda";

            listCLASSVENDA.Add(new PS.Lib.ComboBoxItem());
            listCLASSVENDA[1].ValueMember = "R";
            listCLASSVENDA[1].DisplayMember = "Revenda";

            listCLASSVENDA.Add(new PS.Lib.ComboBoxItem());
            listCLASSVENDA[2].ValueMember = "C";
            listCLASSVENDA[2].DisplayMember = "Consumo";

            psComboBoxCLASSVENDA.DataSource = listCLASSVENDA;
            psComboBoxCLASSVENDA.DisplayMember = "DisplayMember";
            psComboBoxCLASSVENDA.ValueMember = "ValueMember";

            lblValorAberto.Text = "0,00";
            lblSaldo.Text = "0,00";

            PSPartParamVarejoData psPartParamVarejoData = new PSPartParamVarejoData();
            psPartParamVarejoData._tablename = "VPARAMETROS";
            psPartParamVarejoData._keys = new string[] { "CODEMPRESA" };
            DataTable TabPreco = psPartParamVarejoData.RetornaTabelaPreco(PS.Lib.Contexto.Session.Empresa.CodEmpresa);

            List<PS.Lib.ComboBoxItem> listTabPreco = new List<PS.Lib.ComboBoxItem>();
            foreach (DataRow row in TabPreco.Rows)
            {
                listTabPreco.Add(new Lib.ComboBoxItem(row["TABELA"], row["NOME"].ToString()));
            }

            psComboBox8.DataSource = listTabPreco;
            psComboBox8.DisplayMember = "DisplayMember";
            psComboBox8.ValueMember = "ValueMember";

            psDataCriacao.Chave = false;
            psCodUsuarioCriacao.Edita = false;
            psDataCriacao.Text = string.Empty;
            psCodUsuarioCriacao.Text = string.Empty;

            

        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            //Aqui vai as definições dos parametros do tipo da operação
          //  DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();

            #region Código Cliente/Fornecedor

            //Usa Código de Cliente/Fornecedor Sequencial
            /*
            if (int.Parse(dt.Rows[0]["CLIFORUSANUMEROSEQ"].ToString()) == 1)
            {
                psTextoBox1.Chave = false;
            }
            */
            
            psTextoBox1.Text = "";
            psTextoBox1.Edita = false;

            #endregion

            psDateBox1.Text = string.Empty;
            psCheckBox1.Checked = true;

            psLookup1.Text = "1";
            psLookup1.LoadLookup();

            psLookup2.Text = "1";
            psLookup2.LoadLookup();

            psLookup7.Text = "1";
            psLookup7.LoadLookup();

            psLookup8.Text = "1";
            psLookup8.LoadLookup();

            psLookup12.Text = "1";
            psLookup12.LoadLookup();

            psLookup13.Text = "1";
            psLookup13.LoadLookup();

            psLookup3.textBox1.Text = "1";
            psLookup3.textBox2.Text = "Brasil";


        }

        private void psComboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (psComboBox2.Value.ToString() == "0")
            {
                psMaskedTextBox1.Mask = "00,000,000/0000-00";
            }

            if (psComboBox2.Value.ToString() == "1")
            {
                psMaskedTextBox1.Mask = "000,000,000-00";
            }
        }

        private void psLookup5_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODETD", psLookup4.Text));
        }

        private void psLookup11_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODETD", psLookup10.Text));
        }

        private void psLookup16_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODETD", psLookup15.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            psTextoBox20.Text = psTextoBox5.Text;
            psLookup7.Text = psLookup1.Text;
            psLookup7.LoadLookup();
            psTextoBox21.Text = psTextoBox6.Text;
            psTextoBox22.Text = psTextoBox7.Text;
            psTextoBox23.Text = psTextoBox8.Text;
            psLookup8.Text = psLookup2.Text;
            psLookup8.LoadLookup();
            psTextoBox24.Text = psTextoBox9.Text;
            psLookup9.Text = psLookup3.Text;
            psLookup9.LoadLookup();
            psLookup10.Text = psLookup4.Text;
            psLookup10.LoadLookup();
            psLookup11.Text = psLookup5.Text;
            psLookup11.LoadLookup();

            psTextoBox25.Text = psTextoBox5.Text;
            psLookup12.Text = psLookup1.Text;
            psLookup12.LoadLookup();
            psTextoBox26.Text = psTextoBox6.Text;
            psTextoBox27.Text = psTextoBox7.Text;
            psTextoBox28.Text = psTextoBox8.Text;
            psLookup13.Text = psLookup2.Text;
            psLookup13.LoadLookup();
            psTextoBox29.Text = psTextoBox9.Text;
            psLookup14.Text = psLookup3.Text;
            psLookup14.LoadLookup();
            psLookup15.Text = psLookup4.Text;
            psLookup15.LoadLookup();
            psLookup16.Text = psLookup5.Text;
            psLookup16.LoadLookup();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(psTextoBox1.Text) > 0)
                {
                    //PSPartCliForData psPartCliForData = (PSPartCliForData)this._psPartData;
                    PSPartCliForData psPartCliForData = new PSPartCliForData();
                   
                    Decimal? ValorAberto = psPartCliForData.FinanceiroEmAberto(PS.Lib.Contexto.Session.Empresa.CodEmpresa, psTextoBox1.Text);
                    Decimal? LimiteCredito = Convert.ToDecimal(psMoedaBox1.Text);
                    lblValorAberto.Text = string.Format("{0:n}", ValorAberto);
                    lblSaldo.Text = string.Format("{0:n}", LimiteCredito - ValorAberto);
                }
                else
                {
                    PS.Lib.PSMessageBox.ShowInfo("Salve o registro antes de prosseguir.");                    
                }
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }
        }

        private void psMaskedTextBox1_Validating(object sender, CancelEventArgs e)
        {
            //if (psMaskedTextBox1.Text.Equals("   .   .   -"))
            //{
            //    MessageBox.Show("Favor Preencher o campo CNPJ/CPF corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //if (psMaskedTextBox1.Text.Equals("  .   .   /    -"))
            //{
            //    MessageBox.Show("Favor Preencher o campo CNPJ/CPF corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //PS.Lib.Valida valida = new Lib.Valida();
            //if (psComboBox2.Text.Equals("Física"))
            //{
            //    Valida o CPF
            //    if (valida.validarCPF(psMaskedTextBox1.Text).Equals(false))
            //    {
            //        MessageBox.Show("CPF digitado não é válido.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}
            //else
            //{
            //    Valida o CNPJ
            //    if (valida.validarCNPJ(psMaskedTextBox1.Text).Equals(false))
            //    {
            //        MessageBox.Show("CNPJ digitado não é válido.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}
            //Verifica se existe o CPF/CNPJ
            //PS.Lib.Global gb = new PS.Lib.Global();
            //if (gb.ExisteCGCCPF(psMaskedTextBox1.Text, psTextoBox1.Text).Equals(true))
            //{
            //     MessageBox.Show("Atenção. CGC/CPF informado já esta cadastrado", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string historico = memoEdit2.Text + "\r\n" + "Usuário: " + AppLib.Context.Usuario.ToString() + "\r\n" + "Data/Hora: " + string.Format("{0:dd/MM/yyyy HH:mm:ss}", AppLib.Context.poolConnection.Get("Start").GetDateTime());
            historico = historico + "\r\n \r\n" + getHistorico(psTextoBox1.Text, Convert.ToInt32(AppLib.Context.Empresa));
            inserirHistórico(psTextoBox1.Text, Convert.ToInt32(AppLib.Context.Empresa), historico);
            
        }

        private void inserirHistórico(string codClifor, int codEmpresa, string historico)
        {
            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE VCLIFOR SET HISTORICO = ? WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { historico, codClifor, codEmpresa });
            memoEdit1.Text = getHistorico(codClifor, codEmpresa);
            memoEdit2.Text = "";
        }

        private string getHistorico(string codClifor, int codEmpresa)
        {
            return AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT HISTORICO FROM VCLIFOR WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { psTextoBox1.Text, AppLib.Context.Empresa }).ToString();
        }

        private void PSPartCliForEdit_Load(object sender, EventArgs e)
        {
            memoEdit1.Text = getHistorico(psTextoBox1.Text, AppLib.Context.Empresa);
        }

        private void btnSintegra_Click(object sender, EventArgs e)
        {
            if (psComboBox2.Text == "Física")
            {
                ERP.Comercial.FormConsultaReceitaCPF frmCPF = new ERP.Comercial.FormConsultaReceitaCPF();
                frmCPF.maskedTextBox1.Text = psMaskedTextBox1.Text;
                frmCPF.maskedTextBox2.Text = psDateBox1.Text;
                frmCPF.ShowDialog();
            }
            else
            {
                ERP.Comercial.FormConsultaReceitaCNPJ frmCNPJ = new ERP.Comercial.FormConsultaReceitaCNPJ();
                frmCNPJ.maskedTextBox1.Text = psMaskedTextBox1.Text;
                frmCNPJ.ShowDialog();
                if (frmCNPJ.copiar == true)
                {
                    //Realiza a cópia dos dados do form
                    psTextoBox2.Text = frmCNPJ.txtRazaoSocial.Text;
                    psTextoBox3.Text = frmCNPJ.txtNomeFantasia.Text;
                    psTextoBox5.Text = frmCNPJ.txtCep.Text.Replace(".", "");
                    psTextoBox6.Text = frmCNPJ.txtLogr.Text;
                    psTextoBox7.Text = frmCNPJ.txtNumero.Text;
                    psTextoBox9.Text = frmCNPJ.txtBairro.Text;
                    psLookup4.textBox1.Text = frmCNPJ.txtUF.Text;
                    psLookup4.textBox2.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NOME FROM GESTADO WHERE CODETD = ?", new object[] { frmCNPJ.txtUF.Text }).ToString();
                    psLookup5.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODCIDADE FROM GCIDADE WHERE NOME = ? AND CODETD = ?", new object[] { frmCNPJ.txtMunicipio.Text.Replace("'", " ").ToString(), frmCNPJ.txtUF.Text }).ToString();
                    psLookup5.textBox2.Text = frmCNPJ.txtMunicipio.Text.Replace("'", " ").ToString();
                    psTextoBox11.Text = frmCNPJ.txtTelefone.Text;
                }
            }
        }

        private void btnSiteSintegra_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(psMaskedTextBox1.Text.Replace(".", "").ToString().Replace("-", "").Replace("/",""));
            ERP.Comercial.FormSiteSintegra frm = new ERP.Comercial.FormSiteSintegra();
            frm.ShowDialog();
        }
        
        private void btnSearchCep_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(psTextoBox5.Text))
            {
                AppLib.Util.Correios.Endereco endereco = new AppLib.Util.Correios.Endereco();
                endereco = AppLib.Util.Correios.BuscaCep.GetEndereco(psTextoBox5.Text, 10000);
                psTextoBox6.Text = endereco.logradouro;
                psTextoBox9.Text = endereco.bairro;
                psLookup4.textBox1.Text = endereco.estado;
                psLookup4.textBox2.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NOME FROM GESTADO WHERE CODETD = ?", new object[] { endereco.estado}).ToString();
                psLookup5.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODCIDADE FROM GCIDADE WHERE NOME = ? AND CODETD = ?", new object[] { endereco.cidade.Replace("&#39;", " ").ToString(), endereco.estado }).ToString();
                psLookup5.textBox2.Text = endereco.cidade.Replace("&#39;", " ").ToString();
            }
            else
            {
                MessageBox.Show("Favor preecher o CEP corretamente", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void psLookup3_Validated(object sender, EventArgs e)
        {
           
        }

        private void psLookup3_AfterLookup(object sender, Lib.LookupEventArgs e)
        {
            if (psLookup3.textBox2.Text != "Brasil" && psLookup3.textBox2.Text != "BRASIL")
            {
                psTextoBox31.Enabled = true;
            }
            else
            {
                psTextoBox31.Enabled = false;
            }
        }
    }
}
