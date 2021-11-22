using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PS.Glb
{
    public partial class PSPartProdutoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private string mask;
        private DataRow VPARAMETROS;

        public PSPartProdutoEdit()
        {
            InitializeComponent();

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 1;
            list1[0].DisplayMember = "Produto";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 2;
            list1[1].DisplayMember = "Serviço";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = 1;
            list2[0].DisplayMember = "Veiculos Novos";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = 2;
            list2[1].DisplayMember = "Medicamentos";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[2].ValueMember = 3;
            list2[2].DisplayMember = "Arma de Fogo";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[3].ValueMember = 4;
            list2[3].DisplayMember = "Combustíveis";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[4].ValueMember = 0;
            list2[4].DisplayMember = "Outros";

            psComboBox2.DataSource = list2;
            psComboBox2.DisplayMember = "DisplayMember";
            psComboBox2.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list4 = new List<PS.Lib.ComboBoxItem>();

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[0].ValueMember = 0;
            list4[0].DisplayMember = "0 - Nacional, exceto as indicadas nos códigos 3, 4, 5 e 8.";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[1].ValueMember = 1;
            list4[1].DisplayMember = "1 - Estrangeira - Importação direta, exceto a indicada no código 6.";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[2].ValueMember = 2;
            list4[2].DisplayMember = "2 - Estrangeira - Adquirida no mercado interno, exceto a indicada no código 7.";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[3].ValueMember = 3;
            list4[3].DisplayMember = "3 - Nacional, mercadoria ou bem com Conteúdo de Importação superior a 40% e inferior ou igual a 70%.";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[4].ValueMember = 4;
            list4[4].DisplayMember = "4 - Nacional, cuja produção tenha sido feita em conformidade com os processos produtivos básicos de que tratam as legislações citadas nos Ajustes.";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[5].ValueMember = 5;
            list4[5].DisplayMember = "5 - Nacional, mercadoria ou bem com Conteúdo de Importação inferior ou igual a 40%.";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[6].ValueMember = 6;
            list4[6].DisplayMember = "6 - Estrangeira - Importação direta, sem similar nacional, constante em lista da CAMEX e gás natural.";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[7].ValueMember = 7;
            list4[7].DisplayMember = "7 - Estrangeira - Adquirida no mercado interno, sem similar nacional, constante lista CAMEX e gás natural.";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[8].ValueMember = 8;
            list4[8].DisplayMember = "8 - Nacional, mercadoria ou bem com Conteúdo de Importação superior a 70%'.";

            psComboBox4.DataSource = list4;
            psComboBox4.DisplayMember = "DisplayMember";
            psComboBox4.ValueMember = "ValueMember";


            psComboBox3.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPRODUTOCLASSIFICACAO", new object[] { });
            psComboBox3.ValueMember = "CODCLASSIFICACAO";
            psComboBox3.DisplayMember = "DESCRICAO";




            psLookup1.PSPart = "PSPartMoeda";
            psLookup2.PSPart = "PSPartUnidade";
            psLookup3.PSPart = "PSPartFabricante";
            psLookup4.PSPart = "PSPartMoeda";
            psLookup5.PSPart = "PSPartUnidade";
            psLookup6.PSPart = "PSPartUnidade";
            psLookup7.PSPart = "PSPartMoeda";
            psLookup8.PSPart = "PSPartMoeda";
            psLookup9.PSPart = "PSPartMoeda";

            psDateBox1.Chave = false;
            psDateBox2.Chave = false;
            psMoedaBox14.Edita = false;
            psMoedaBox15.Edita = false;

            CarregaParametrosTela();
        }

        private void CarregaLabelCamposAdicionais()
        {
            if (VPARAMETROS != null)
            {
                //psLookup1.Caption = string.Concat(psLookup1.Caption," ",VPARAMETROS["PRDTEXTOPRECO1"].ToString());
                //psLookup4.Caption = string.Concat(psLookup4.Caption," ",VPARAMETROS["PRDTEXTOPRECO2"].ToString());
                //psLookup7.Caption = string.Concat(psLookup7.Caption," ",VPARAMETROS["PRDTEXTOPRECO3"].ToString());
                //psLookup8.Caption = string.Concat(psLookup8.Caption," ",VPARAMETROS["PRDTEXTOPRECO4"].ToString());
                //psLookup9.Caption = string.Concat(psLookup9.Caption," ",VPARAMETROS["PRDTEXTOPRECO5"].ToString());

                psMoedaBox1.Caption = VPARAMETROS["PRDTEXTOPRECO1"].ToString();
                psMoedaBox2.Caption = VPARAMETROS["PRDTEXTOPRECO2"].ToString();
                psMoedaBox11.Caption = VPARAMETROS["PRDTEXTOPRECO3"].ToString();
                psMoedaBox12.Caption = VPARAMETROS["PRDTEXTOPRECO4"].ToString();
                psMoedaBox13.Caption = VPARAMETROS["PRDTEXTOPRECO5"].ToString();
            }
        }

        public override void CarregaParametrosTela()
        {
            base.CarregaParametrosTela();

            VPARAMETROS = gb.RetornaParametrosVarejo();

            if (VPARAMETROS != null)
            {
                #region Preco1

                //Edita
                if (VPARAMETROS["PRDUSAPRECO1"].ToString() == "E")
                {
                    psLookup1.Visible = true;
                    psLookup1.Chave = true;

                    psMoedaBox1.Visible = true;
                    psMoedaBox1.Edita = true;
                }

                //Não Mostra
                if (VPARAMETROS["PRDUSAPRECO1"].ToString() == "M")
                {
                    psLookup1.Visible = false;
                    psLookup1.Chave = false;

                    psMoedaBox1.Visible = false;
                    psMoedaBox1.Edita = false;
                }

                #endregion

                #region Preco2

                //Edita
                if (VPARAMETROS["PRDUSAPRECO2"].ToString() == "E")
                {
                    psLookup4.Visible = true;
                    psLookup4.Chave = true;

                    psMoedaBox2.Visible = true;
                    psMoedaBox2.Edita = true;
                }

                //Não Mostra
                if (VPARAMETROS["PRDUSAPRECO2"].ToString() == "M")
                {
                    psLookup4.Visible = false;
                    psLookup4.Chave = false;

                    psMoedaBox2.Visible = false;
                    psMoedaBox2.Edita = false;
                }

                #endregion

                #region Preco3

                //Edita
                if (VPARAMETROS["PRDUSAPRECO3"].ToString() == "E")
                {
                    psLookup7.Visible = true;
                    psLookup7.Chave = true;

                    psMoedaBox11.Visible = true;
                    psMoedaBox11.Edita = true;
                }

                //Não Mostra
                if (VPARAMETROS["PRDUSAPRECO3"].ToString() == "M")
                {
                    psLookup7.Visible = false;
                    psLookup7.Chave = false;

                    psMoedaBox11.Visible = false;
                    psMoedaBox11.Edita = false;
                }

                #endregion

                #region Preco4

                //Edita
                if (VPARAMETROS["PRDUSAPRECO4"].ToString() == "E")
                {
                    psLookup8.Visible = true;
                    psLookup8.Chave = true;

                    psMoedaBox12.Visible = true;
                    psMoedaBox12.Edita = true;
                }

                //Não Mostra
                if (VPARAMETROS["PRDUSAPRECO4"].ToString() == "M")
                {
                    psLookup8.Visible = false;
                    psLookup8.Chave = false;

                    psMoedaBox12.Visible = false;
                    psMoedaBox12.Edita = false;
                }

                #endregion

                #region Preco5

                //Edita
                if (VPARAMETROS["PRDUSAPRECO5"].ToString() == "E")
                {
                    psLookup9.Visible = true;
                    psLookup9.Chave = true;

                    psMoedaBox13.Visible = true;
                    psMoedaBox13.Edita = true;
                }

                //Não Mostra
                if (VPARAMETROS["PRDUSAPRECO5"].ToString() == "M")
                {
                    psLookup9.Visible = false;
                    psLookup9.Chave = false;

                    psMoedaBox13.Visible = false;
                    psMoedaBox13.Edita = false;
                }

                #endregion
            }
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psComboBox2.SelectedIndex = 4;
            psComboBox4.SelectedIndex = 0;

            psCheckBox1.Checked = true;

            psDateBox1.Text = null;
            psDateBox2.Text = null;

            CarregaLabelCamposAdicionais();

            psLookup1.Text = "R$";
            psLookup1.Description = "REAL";
            psLookup4.Text = "R$";
            psLookup4.Description = "REAL";
            psLookup7.Text = "R$";
            psLookup7.Description = "REAL";
            psLookup8.Text = "R$";
            psLookup8.Description = "REAL";
            psLookup9.Text = "R$";
            psLookup9.Description = "REAL";
        }

        public override void CarregaRegistro()
        {
            base.CarregaRegistro();

            CarregaLabelCamposAdicionais();
            carregaListaIBPTAX();

            byte[] arrayimagem = (byte[])AppLib.Context.poolConnection.Get("Start").ExecGetField(null, "SELECT PDF FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, psTextoBox1.textBox1.Text });
            if (arrayimagem != null)
            {
                if (arrayimagem.Length > 0)
                {
                    string caminho;
                    caminho = System.IO.Path.GetTempFileName();
                    System.IO.File.Move(caminho, System.IO.Path.ChangeExtension(caminho, ".pdf"));
                    caminho = System.IO.Path.ChangeExtension(caminho, ".pdf");
                    System.IO.File.WriteAllBytes(caminho, arrayimagem);
                    pdfViewer1.LoadDocument(caminho);
                }  
            }
           
        }

        private void psTextoBox1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.psTextoBox1.Text))
            {
                PSPartProdutoData psPartProdutoData = new PSPartProdutoData();
                psPartProdutoData._tablename = "VPRODUTO";
                psPartProdutoData._keys = new string[] { "CODEMPRESA", "CODPRODUTO" };

                this.psTextoBox1.Text = psPartProdutoData.RetornaProximoCodProduto(PS.Lib.Contexto.Session.Empresa.CodEmpresa, this.psTextoBox1.Text);
                if (mask.Length == psTextoBox1.Text.Length)
                {
                    psCheckBox2.Checked = true;
                }
                else
                {
                    psCheckBox2.Checked = false;
                }
            }

            if (string.IsNullOrEmpty(this.psTextoBox1.Text) || !string.IsNullOrEmpty(this.psTextoBox8.Text))
                return;
            else
            {
                this.psTextoBox8.Text = this.psTextoBox1.Text;
            }
        }

        private void PSPartProdutoEdit_Load(object sender, EventArgs e)
        {
            System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();
            mask = (PARAMVAREJO["MASKPRODSERV"] == DBNull.Value) ? string.Empty : PARAMVAREJO["MASKPRODSERV"].ToString();
        }

        private void psMaskedTextBox1_Validating(object sender, CancelEventArgs e)
        {
            carregaListaIBPTAX();
        }

        private void carregaListaIBPTAX()
        {
            dataGridView1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODIGO Ncm, UF, NACIONALFEDERAL Federal, IMPORTADOSFEDERAL Importado, ESTADUAL Estadual, MUNICIPAL Municipal, CHAVE Chave FROM VIBPTAX WHERE CODIGO = ?", new object[] { psMaskedTextBox1.Text });
            dataGridView1.Columns["Federal"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void psTextoBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void psMoedaBox3_Enter(object sender, EventArgs e)
        {

        }

        public override void SalvaRegistro()
        {
            try
            {
                base.SalvaRegistro();
                if (!string.IsNullOrEmpty(txtArquivo.Text))
                {
                    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppLib.Context.poolConnection.Get("Start").ConnectionString);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("UPDATE VPRODUTO SET PDF = @pdf WHERE CODPRODUTO = @codProduto AND CODEMPRESA = @codEmpresa", conn);
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@codProduto", SqlDbType.VarChar)).Value = psTextoBox1.textBox1.Text;
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@codEmpresa", SqlDbType.Int)).Value = AppLib.Context.Empresa;
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@pdf", SqlDbType.VarBinary)).Value = System.IO.File.ReadAllBytes(txtArquivo.Text);
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);   
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Pdf Files|*.pdf";
            open.ShowDialog();
            if (!string.IsNullOrEmpty(open.FileName))
            {
                pdfViewer1.LoadDocument(open.FileName);
                txtArquivo.Text = open.FileName;
            }
        }

        private void btnExcluirPDF_Click(object sender, EventArgs e)
        {
            txtArquivo.Text = string.Empty;
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VPRODUTO SET PDF = ? WHERE CODEMPRESA = ? AND CODPRODUTO = ?", new object[] {null, AppLib.Context.Empresa, psTextoBox1.textBox1.Text });

        }
    }
}
