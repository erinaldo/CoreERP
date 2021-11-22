using ITGProducao.Controles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroProduto : Form
    {
        public bool edita = false;
        public string codProduto = string.Empty;
        private string mask;
        private List<string> tabelasFilhas = new List<string>();

        //Movimentação des Produtos
        string tabela = "VPRODUTO";
        string relacionamento = "INNER JOIN GOPERITEM ON GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO INNER JOIN GOPER ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER INNER JOIN GTIPOPER ON GTIPOPER.CODEMPRESA = GOPER.CODEMPRESA AND GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER LEFT OUTER JOIN VCLIFOR ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR";
        string query = string.Empty;
        //

        //Clientes e Fornecedores de Produtos 
        string tabela2 = "VCLIFOR";
        //string relacionamento2 = "LEFT OUTER JOIN GCIDADE ON GCIDADE.CODETD = VCLIFOR.CODETD AND GCIDADE.CODCIDADE = VCLIFOR.CODCIDADE";
        string query2 = string.Empty;
        //

        //Variaveis para NewLookup
        private NewLookup lookup;

        //Unida de Medida do Produto - João Pedro Luchiari
        public bool consulta = false;

        public string Codclifor = string.Empty;

        public frmCadastroProduto()
        {
            InitializeComponent();
            InicializaCadastroProduto();

            new Class.Utilidades().criaCamposComplementares("VPRODUTOCOMPL", tabCamposComplementares);
        }

        public frmCadastroProduto(ref NewLookup lookup)
        {
            InitializeComponent();

            InicializaCadastroProduto();

            this.codProduto = lookup.ValorCodigoInterno;
            this.edita = true;

            this.lookup = lookup;
        }

        private void InicializaCadastroProduto()
        {
            new Class.Utilidades().getDicionario(this, tabControl1, "VPRODUTO");
            new Class.Utilidades().getDicionario(this, tabControl1, "VPRODUTOCOM");

            psCodFabricante.PSPart = "PSPartFabricante";
            psCodMoeda1.PSPart = "PSPartMoeda";
            psCodUnidVenda.PSPart = "PSPartUnidade";
            psCodMoeda2.PSPart = "PSPartMoeda";
            psCodUnidCompra.PSPart = "PSPartUnidade";
            psCodUnidControle.PSPart = "PSPartUnidade";
            psCodMoeda3.PSPart = "PSPartMoeda";
            psCodMoeda4.PSPart = "PSPartMoeda";
            psCodMoeda5.PSPart = "PSPartMoeda";

            #region Combo

            List<PS.Lib.ComboBoxItem> listProcedencia = new List<PS.Lib.ComboBoxItem>();

            listProcedencia.Add(new PS.Lib.ComboBoxItem());
            listProcedencia[0].ValueMember = 0;
            listProcedencia[0].DisplayMember = "0 - Nacional, exceto as indicadas nos códigos 3, 4, 5 e 8.";

            listProcedencia.Add(new PS.Lib.ComboBoxItem());
            listProcedencia[1].ValueMember = 1;
            listProcedencia[1].DisplayMember = "1 - Estrangeira - Importação direta, exceto a indicada no código 6.";

            listProcedencia.Add(new PS.Lib.ComboBoxItem());
            listProcedencia[2].ValueMember = 2;
            listProcedencia[2].DisplayMember = "2 - Estrangeira - Adquirida no mercado interno, exceto a indicada no código 7.";

            listProcedencia.Add(new PS.Lib.ComboBoxItem());
            listProcedencia[3].ValueMember = 3;
            listProcedencia[3].DisplayMember = "3 - Nacional, mercadoria ou bem com Conteúdo de Importação superior a 40% e inferior ou igual a 70%.";

            listProcedencia.Add(new PS.Lib.ComboBoxItem());
            listProcedencia[4].ValueMember = 4;
            listProcedencia[4].DisplayMember = "4 - Nacional, cuja produção tenha sido feita em conformidade com os processos produtivos básicos de que tratam as legislações citadas nos Ajustes.";

            listProcedencia.Add(new PS.Lib.ComboBoxItem());
            listProcedencia[5].ValueMember = 5;
            listProcedencia[5].DisplayMember = "5 - Nacional, mercadoria ou bem com Conteúdo de Importação inferior ou igual a 40%.";

            listProcedencia.Add(new PS.Lib.ComboBoxItem());
            listProcedencia[6].ValueMember = 6;
            listProcedencia[6].DisplayMember = "6 - Estrangeira - Importação direta, sem similar nacional, constante em lista da CAMEX e gás natural.";

            listProcedencia.Add(new PS.Lib.ComboBoxItem());
            listProcedencia[7].ValueMember = 7;
            listProcedencia[7].DisplayMember = "7 - Estrangeira - Adquirida no mercado interno, sem similar nacional, constante lista CAMEX e gás natural.";

            listProcedencia.Add(new PS.Lib.ComboBoxItem());
            listProcedencia[8].ValueMember = 8;
            listProcedencia[8].DisplayMember = "8 - Nacional, mercadoria ou bem com Conteúdo de Importação superior a 70%'.";


            cmbProcedencia.DataSource = listProcedencia;
            cmbProcedencia.DisplayMember = "DisplayMember";
            cmbProcedencia.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listTipo = new List<PS.Lib.ComboBoxItem>();

            listTipo.Add(new PS.Lib.ComboBoxItem());
            listTipo[0].ValueMember = 4;
            listTipo[0].DisplayMember = "Outros";

            listTipo.Add(new PS.Lib.ComboBoxItem());
            listTipo[1].ValueMember = 0;
            listTipo[1].DisplayMember = "Veiculos Novos";

            listTipo.Add(new PS.Lib.ComboBoxItem());
            listTipo[2].ValueMember = 1;
            listTipo[2].DisplayMember = "Medicamentos";

            listTipo.Add(new PS.Lib.ComboBoxItem());
            listTipo[3].ValueMember = 2;
            listTipo[3].DisplayMember = "Arma de Fogo";

            listTipo.Add(new PS.Lib.ComboBoxItem());
            listTipo[4].ValueMember = 3;
            listTipo[4].DisplayMember = "Combustíveis";

            cmbCodTipo.DataSource = listTipo;
            cmbCodTipo.DisplayMember = "DisplayMember";
            cmbCodTipo.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listEscala = new List<PS.Lib.ComboBoxItem>();

            listEscala.Add(new PS.Lib.ComboBoxItem());
            listEscala[0].ValueMember = "S";
            listEscala[0].DisplayMember = "Produzido em Escala Relevante";

            listEscala.Add(new PS.Lib.ComboBoxItem());
            listEscala[1].ValueMember = "N";
            listEscala[1].DisplayMember = "Produzido em Escala Não Relevante";

            cmbIndEscala.DataSource = listEscala;
            cmbIndEscala.DisplayMember = "DisplayMember";
            cmbIndEscala.ValueMember = "ValueMember";

            #endregion
        }


        private void peImagem_Click(object sender, EventArgs e)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            peImagem.LoadImage();
            if (peImagem.Image != null)
            {
                peImagem.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                lblTamanhoImagem.Text = string.Format("Tamanho da Img: {0}", TamanhoAmigavel(ms.Length));
            }

        }

        private void btnRemoverImagem_Click(object sender, EventArgs e)
        {
            peImagem.EditValue = "";
        }

        private void carregaObj(DataTable dt)
        {
            txtCodProduto.Text = dt.Rows[0]["CODPRODUTO"].ToString();
            cmbAtivo.SelectedIndex = dt.Rows[0]["ATIVO"].ToString() == "1" ? 0 : 1;
            cmbUltimoNivel.SelectedIndex = Convert.ToBoolean(dt.Rows[0]["ULTIMONIVEL"]) == true ? 0 : 1;
            txtCodigoAuxiliar.Text = dt.Rows[0]["CODIGOAUXILIAR"].ToString();
            cmbProdServ.SelectedIndex = dt.Rows[0]["PRODSERV"].ToString() == "1" ? 0 : 1;
            cmbCodClassificacao.Text = string.IsNullOrEmpty(dt.Rows[0]["CLASSIFICACAO"].ToString()) ? string.Empty : dt.Rows[0]["CLASSIFICACAO"].ToString();
            txtNome.Text = dt.Rows[0]["NOME"].ToString();
            txtNomeFantasia.Text = dt.Rows[0]["NOMEFANTASIA"].ToString();
            psCodFabricante.textBox1.Text = dt.Rows[0]["CODFABRICANTE"].ToString();
            psCodFabricante.LoadLookup();
            txtCodPrdFabricante.Text = dt.Rows[0]["CODPRDFABRICANTE"].ToString();
            object bArray = (object)dt.Rows[0]["IMAGEM"];
            peImagem.EditValue = bArray;
            cmbCodTipo.SelectedValue = dt.Rows[0]["CODTIPO"];
            cmbIndEscala.SelectedValue = dt.Rows[0]["INDFABESCALARELEV"];
            // João Pedro Luchiari - Comentado pois o campo NCM foi substituído pelo Lookup
            //txtCodNCM.Text = dt.Rows[0]["CODNCM"].ToString();
            lpNCM.txtcodigo.Text = dt.Rows[0]["CODNCM"].ToString();
            lpNCM.CarregaDescricao();
            txtCodNCMEx.Text = dt.Rows[0]["CODNCMEX"].ToString();
            txtCest.Text = dt.Rows[0]["CEST"].ToString();
            txtNumRegmiNagRi.Text = dt.Rows[0]["NUMREGMINAGRI"].ToString();
            cmbProcedencia.SelectedValue = dt.Rows[0]["PROCEDENCIA"];
            meDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            txtPesoBruto.Text = string.IsNullOrEmpty(dt.Rows[0]["PESOBRUTO"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["PESOBRUTO"]));
            txtpesoLiquido.Text = string.IsNullOrEmpty(dt.Rows[0]["PESOLIQUIDO"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["PESOLIQUIDO"]));
            txtComprimento.Text = string.IsNullOrEmpty(dt.Rows[0]["COMPRIMENTO"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["COMPRIMENTO"]));
            txtlargura.Text = string.IsNullOrEmpty(dt.Rows[0]["LARGURA"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["LARGURA"]));
            txtEspessura.Text = string.IsNullOrEmpty(dt.Rows[0]["ESPESSURA"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["ESPESSURA"]));
            txtDiametro.Text = string.IsNullOrEmpty(dt.Rows[0]["DIAMETRO"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["DIAMETRO"]));
            txtLeadTime.Text = string.IsNullOrEmpty(dt.Rows[0]["LEADTIME"].ToString()) ? "0" : dt.Rows[0]["LEADTIME"].ToString();
            txtLocalEstocagem.Text = dt.Rows[0]["LOCALESTOCAGEM"].ToString();
            txtEstoqeuMinimo.Text = dt.Rows[0]["ESTOQUEMINIMO"].ToString();
            txtEstoqueMaximo.Text = dt.Rows[0]["ESTOQUEMAXIMO"].ToString();
            psCodUnidVenda.textBox1.Text = dt.Rows[0]["CODUNIDVENDA"].ToString();
            psCodUnidVenda.LoadLookup();
            psCodUnidCompra.textBox1.Text = dt.Rows[0]["CODUNIDCOMPRA"].ToString();
            psCodUnidCompra.LoadLookup();
            psCodUnidControle.textBox1.Text = dt.Rows[0]["CODUNIDCONTROLE"].ToString();
            psCodUnidControle.LoadLookup();
            psCodMoeda1.textBox1.Text = dt.Rows[0]["CODMOEDA1"].ToString();
            psCodMoeda2.textBox1.Text = dt.Rows[0]["CODMOEDA2"].ToString();
            psCodMoeda3.textBox1.Text = dt.Rows[0]["CODMOEDA3"].ToString();
            psCodMoeda4.textBox1.Text = dt.Rows[0]["CODMOEDA4"].ToString();
            psCodMoeda5.textBox1.Text = dt.Rows[0]["CODMOEDA5"].ToString();
            txtPreco1.Text = string.IsNullOrEmpty(dt.Rows[0]["PRECO1"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["PRECO1"]));
            txtPreco2.Text = string.IsNullOrEmpty(dt.Rows[0]["PRECO2"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["PRECO2"]));
            txtPreco3.Text = string.IsNullOrEmpty(dt.Rows[0]["PRECO3"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["PRECO3"]));
            txtPreco4.Text = string.IsNullOrEmpty(dt.Rows[0]["PRECO4"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["PRECO4"]));
            txtPreco5.Text = string.IsNullOrEmpty(dt.Rows[0]["PRECO5"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["PRECO5"]));
            dteDataCustoMedio.EditValue = !string.IsNullOrEmpty(dt.Rows[0]["DATACUSTOMEDIO"].ToString()) ? Convert.ToDateTime(dt.Rows[0]["DATACUSTOMEDIO"]).ToShortDateString() : string.Empty;
            dteDataCustoUnitario.EditValue = !string.IsNullOrEmpty(dt.Rows[0]["DATACUSTOUNITARIO"].ToString()) ? Convert.ToDateTime(dt.Rows[0]["DATACUSTOUNITARIO"]).ToShortDateString() : string.Empty;
            txtCustoMedio.Text = string.IsNullOrEmpty(dt.Rows[0]["CUSTOMEDIO"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["CUSTOMEDIO"]));
            txtCustoUnitario.Text = string.IsNullOrEmpty(dt.Rows[0]["CUSTOUNITARIO"].ToString()) ? "0,00" : string.Format("{0:n2}", Convert.ToDecimal(dt.Rows[0]["CUSTOUNITARIO"]));
            tbObservacoes.Text = dt.Rows[0]["OBSERVACOES"].ToString();
            chkUsaEstoqueMinimo.Checked = Convert.ToBoolean(dt.Rows[0]["USAFORMULAESTOQUEMINIMO"]);
            chkUsaLoteProduto.Checked = Convert.ToBoolean(dt.Rows[0]["USALOTEPRODUTO"]);

            if (!string.IsNullOrEmpty(dt.Rows[0]["PDF"].ToString()))
            {
                byte[] arrayimagem = (byte[])dt.Rows[0]["PDF"];
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
                        txtArquivo.Text = caminho;
                    }
                }
            }
        }
        private void carregaCampos()
        {

            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT *, VPRODUTOCLASSIFICACAO.DESCRICAO CLASSIFICACAO FROM VPRODUTO LEFT OUTER JOIN VPRODUTOCLASSIFICACAO ON VPRODUTO.CODCLASSIFICACAO = VPRODUTOCLASSIFICACAO.CODCLASSIFICACAO  WHERE CODEMPRESA = ? AND CODPRODUTO = ? ", new object[] { AppLib.Context.Empresa, codProduto });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {

                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT *, VPRODUTOCLASSIFICACAO.DESCRICAO CLASSIFICACAO FROM VPRODUTO LEFT OUTER JOIN VPRODUTOCLASSIFICACAO ON VPRODUTO.CODCLASSIFICACAO = VPRODUTOCLASSIFICACAO.CODCLASSIFICACAO  WHERE CODEMPRESA = ? AND CODPRODUTO = ?  ", new object[] { AppLib.Context.Empresa, codProduto });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            carregaCamposComplementares();

        }

        public static string TamanhoAmigavel(long bytes)
        {
            if (bytes < 0) throw new ArgumentException("bytes");

            double humano;
            string sufixo;

            if (bytes >= 1152921504606846976L) // Exabyte (1024^6)
            {
                humano = bytes >> 50;
                sufixo = "EB";
            }
            else if (bytes >= 1125899906842624L) // Petabyte (1024^5)
            {
                humano = bytes >> 40;
                sufixo = "PB";
            }
            else if (bytes >= 1099511627776L) // Terabyte (1024^4)
            {
                humano = bytes >> 30;
                sufixo = "TB";
            }
            else if (bytes >= 1073741824) // Gigabyte (1024^3)
            {
                humano = bytes >> 20;
                sufixo = "GB";
            }
            else if (bytes >= 1048576) // Megabyte (1024^2)
            {
                humano = bytes >> 10;
                sufixo = "MB";
            }
            else if (bytes >= 1024) // Kilobyte (1024^1)
            {
                humano = bytes;
                sufixo = "KB";
            }
            else return bytes.ToString("0 B"); // Byte

            humano /= 1024;
            return humano.ToString("0.## ") + sufixo;
        }

        static Image EscalaPercentual(Image imgFoto, int Percentual)
        {
            float nPorcentagem = ((float)Percentual / 100);

            int fonteLargura = imgFoto.Width;     //armazena a largura original da imagem origem
            int fonteAltura = imgFoto.Height;   //armazena a altura original da imagem origem

            int origemX = 0;        //eixo x da imagem origem
            int origemY = 0;        //eixo y da imagem origem

            int destX = 0;          //eixo x da imagem destino
            int destY = 0;          //eixo y da imagem destino
            //Calcula a altura e largura da imagem redimensionada
            int destWidth = (int)(fonteLargura * nPorcentagem);
            int destHeight = (int)(fonteAltura * nPorcentagem);
            //if (destWidth > destHeight)
            //{
            //    destWidth = 200;
            //    destHeight = 100;
            //}
            //else
            //{
            //    destWidth = 100;
            //    destHeight = 200;
            //}
            //Cria um novo objeto bitmap
            Bitmap bmImagem = new Bitmap(destWidth, destHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //Define a resolução do bitmap.
            bmImagem.SetResolution(imgFoto.HorizontalResolution, imgFoto.VerticalResolution);
            //Crima um objeto graphics e defina a qualidade
            Graphics grImagem = Graphics.FromImage(bmImagem);
            grImagem.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            //Desenha a imge usando o método DrawImage() da classe grafica
            grImagem.DrawImage(imgFoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(origemX, origemY, fonteLargura, fonteAltura),
                GraphicsUnit.Pixel);

            grImagem.Dispose();  //libera o objeto grafico
            return bmImagem;
        }

        private void carregaCombo()
        {
            cmbCodClassificacao.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPRODUTOCLASSIFICACAO", new object[] { });
            cmbCodClassificacao.ValueMember = "CODCLASSIFICACAO";
            cmbCodClassificacao.DisplayMember = "DESCRICAO";
        }

        private void carregaParametros()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT PRDUSAPRECO1, PRDUSAPRECO2, PRDUSAPRECO3, PRDUSAPRECO4, PRDUSAPRECO5, MASKPRODSERV FROM VPARAMETROS WHERE CODEMPRESA = ? ", new object[] { AppLib.Context.Empresa });

            if (dt.Rows.Count > 0)
            {
                #region Preco1

                //Edita
                if (dt.Rows[0]["PRDUSAPRECO1"].ToString() == "E")
                {
                    psCodMoeda1.edita(true);
                    txtPreco1.Enabled = true;
                }

                //Não Mostra
                if (dt.Rows[0]["PRDUSAPRECO1"].ToString() == "M")
                {
                    psCodMoeda1.edita(false);
                    txtPreco1.Enabled = false;
                }

                #endregion

                #region Preco2

                //Edita
                if (dt.Rows[0]["PRDUSAPRECO2"].ToString() == "E")
                {
                    psCodMoeda2.edita(true);
                    txtPreco2.Enabled = true;
                }

                //Não Mostra
                if (dt.Rows[0]["PRDUSAPRECO2"].ToString() == "M")
                {
                    psCodMoeda2.edita(false);
                    txtPreco2.Enabled = false;
                }

                #endregion

                #region Preco3

                //Edita
                if (dt.Rows[0]["PRDUSAPRECO3"].ToString() == "E")
                {
                    psCodMoeda3.edita(true);
                    txtPreco3.Enabled = true;
                }

                //Não Mostra
                if (dt.Rows[0]["PRDUSAPRECO3"].ToString() == "M")
                {
                    psCodMoeda3.edita(false);
                    txtPreco3.Enabled = false;
                }

                #endregion

                #region Preco4

                //Edita
                if (dt.Rows[0]["PRDUSAPRECO4"].ToString() == "E")
                {
                    psCodMoeda4.edita(true);
                    txtPreco4.Enabled = true;
                }

                //Não Mostra
                if (dt.Rows[0]["PRDUSAPRECO4"].ToString() == "M")
                {
                    psCodMoeda4.edita(false);
                    txtPreco4.Enabled = false;
                }

                #endregion

                #region Preco5

                //Edita
                if (dt.Rows[0]["PRDUSAPRECO5"].ToString() == "E")
                {
                    psCodMoeda5.edita(true);
                    txtPreco5.Enabled = true;
                }

                //Não Mostra
                if (dt.Rows[0]["PRDUSAPRECO5"].ToString() == "M")
                {
                    psCodMoeda5.edita(false);
                    txtPreco5.Enabled = false;
                }

                #endregion
            }
            mask = (dt.Rows[0]["MASKPRODSERV"] == DBNull.Value) ? string.Empty : dt.Rows[0]["MASKPRODSERV"].ToString();
        }

        #region Unidade de Medida do Produto

        public void CarregaGridUnidadeMedida(string where)
        {
            try
            {
                string sql = string.Empty;

                string Tabela = "VPRODUTOUNIDADE";

                List<string> TabelasFilhas = new List<string>();
                TabelasFilhas.Clear();

                sql = new Class.Utilidades().getVisao(Tabela, string.Empty, TabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridControl4.DataSource = null;
                gridView9.Columns.Clear();
                gridControl4.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { Tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { Tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridView9.Columns.Count; i++)
                {
                    gridView9.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridView9.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridView9.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void atualizaColuna(DataRow dr)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPRODUTOUNIDADE WHERE CODPRODUTO = ? AND CODUNID = ? AND CODUNIDCONVERSAO = ? AND CODEMPRESA = ?", new object[] { codProduto, dr["VPRODUTOUNIDADE.CODUNID"], dr["VPRODUTOUNIDADE.CODUNIDCONVERSAO"], AppLib.Context.Empresa });

            for (int i = 0; i < gridView9.VisibleColumns.Count; i++)
            {
                if (gridView9.Columns[i].ReadOnly == false)
                {
                    try
                    {
                        dr[gridView9.Columns[i].FieldName] = dr[gridView9.Columns[i].FieldName] = dt.Rows[0][gridView9.Columns[i].FieldName.ToString().Remove(0, gridView9.Columns[i].FieldName.ToString().IndexOf(".") + 1)].ToString();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void Atualizar()
        {
            if (this.lookup == null)
            {
                if (gridView9.SelectedRowsCount > 0)
                {
                    PS.Glb.New.Cadastros.frmCadastroUnidadeMedidaProduto UnidProd = new frmCadastroUnidadeMedidaProduto();
                    DataRow row1 = gridView9.GetDataRow(Convert.ToInt32(gridView9.GetSelectedRows().GetValue(0).ToString()));
                    UnidProd.CodProduto = codProduto;
                    UnidProd.CodUnidade = row1["VPRODUTOUNIDADE.CODUNID"].ToString();
                    UnidProd.CodUnidadeConversao = row1["VPRODUTOUNIDADE.CODUNIDCONVERSAO"].ToString();
                    UnidProd.edita = true;
                    UnidProd.ShowDialog();
                    atualizaColuna(row1);
                }
            }
            else
            {
                DataRow row1 = gridView9.GetDataRow(Convert.ToInt32(gridView9.GetSelectedRows().GetValue(0).ToString()));
                lookup.txtconteudo.Text = row1[tabela + ".DESCRICAO"].ToString().ToUpper();
                lookup.txtcodigo.Text = row1[tabela + ".CODUNID"].ToString();
                lookup.ValorCodigoInterno = row1[tabela + ".CODUNID"].ToString();
                this.Dispose();
            }
        }

        private void btnAtualizarUnidadeMedida_Click(object sender, EventArgs e)
        {
            CarregaGridUnidadeMedida("WHERE CODPRODUTO = '" + codProduto + "'");
        }

        private void btnSelecionarColunasUnidadeMedida_Click(object sender, EventArgs e)
        {
            string Tabela = "VPRODUTOUNIDADE";

            frmSelecaoColunas frm = new frmSelecaoColunas(Tabela);
            frm.ShowDialog();
            CarregaGridUnidadeMedida("WHERE CODPRODUTO = '" + codProduto + "'");
        }

        private void btnSalvarLayoutUnidadeMedida_Click(object sender, EventArgs e)
        {
            string Tabela = "VPRODUTOUNIDADE";

            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, Tabela });

            for (int i = 0; i < gridView9.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", Tabela);
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView9.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView9.VisibleColumns[i].Width);
                GVISAOUSUARIO.Set("VISIVEL", 1);
                GVISAOUSUARIO.Save();
            }

            DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ? AND FIXO = 1", new object[] { AppLib.Context.Usuario, Tabela });
            if (dtFixo.Rows.Count > 0)
            {
                for (int i = 0; i < dtFixo.Rows.Count; i++)
                {
                    AppLib.ORM.Jit FIXO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                    FIXO.Set("VISAO", tabela);
                    FIXO.Set("CODUSUARIO", AppLib.Context.Usuario);
                    FIXO.Set("COLUNA", dtFixo.Rows[i]["COLUNA"]);
                    FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
                CarregaGridUnidadeMedida("WHERE CODPRODUTO = '" + codProduto + "'");
            }
        }

        private void btnAgruparUnidadeMedida_Click(object sender, EventArgs e)
        {
            if (gridView9.OptionsView.ShowGroupPanel == true)
            {
                gridView9.OptionsView.ShowGroupPanel = false;
                gridView9.ClearGrouping();
                btnAgruparUnidadeMedida.Text = "Agrupar";
            }
            else
            {
                gridView9.OptionsView.ShowGroupPanel = true;
                btnAgruparUnidadeMedida.Text = "Desagrupar";
            }
        }

        private void btnPesquisarUnidadeMedida_Click(object sender, EventArgs e)
        {
            if (gridView9.OptionsFind.AlwaysVisible == true)
            {
                gridView9.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView9.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnNovoUnidadeMedida_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                PS.Glb.New.Cadastros.frmCadastroUnidadeMedidaProduto UnidProd = new frmCadastroUnidadeMedidaProduto();
                UnidProd.edita = false;
                UnidProd.CodProduto = codProduto;
                UnidProd.ShowDialog();
                CarregaGridUnidadeMedida("WHERE CODPRODUTO = '" + codProduto + "'");
            }
            else
            {
                PS.Glb.New.Cadastros.frmCadastroUnidadeMedidaProduto UnidProd = new frmCadastroUnidadeMedidaProduto(ref this.lookup);
                UnidProd.edita = false;
                UnidProd.CodProduto = codProduto;
                UnidProd.ShowDialog();
                this.Dispose();
            }
        }

        private void btnEditarUnidadeMedida_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (gridView9.SelectedRowsCount > 0)
                {
                    PS.Glb.New.Cadastros.frmCadastroUnidadeMedidaProduto UnidProd = new frmCadastroUnidadeMedidaProduto();
                    DataRow row1 = gridView9.GetDataRow(Convert.ToInt32(gridView9.GetSelectedRows().GetValue(0).ToString()));
                    UnidProd.CodProduto = codProduto;
                    UnidProd.CodUnidade = row1["VPRODUTOUNIDADE.CODUNID"].ToString();
                    UnidProd.CodUnidadeConversao = row1["VPRODUTOUNIDADE.CODUNIDCONVERSAO"].ToString();
                    UnidProd.edita = true;
                    UnidProd.ShowDialog();
                    atualizaColuna(row1);
                }
                else
                {
                    PS.Glb.New.Cadastros.frmCadastroUnidadeMedidaProduto UnidProd = new frmCadastroUnidadeMedidaProduto();
                    UnidProd.edita = false;
                }
            }
            else
            {
                gridControl4_DoubleClick(sender, e);
            }
        }

        private void gridControl4_DoubleClick(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (consulta == true)
                {
                    this.Dispose();
                }
                else
                {
                    Atualizar();
                }
            }
            else
            {
                Atualizar();
            }
        }

        #endregion

        private void fmCadastroProduto_Load(object sender, EventArgs e)
        {
            carregaParametros();
            carregaCombo();

            Codclifor = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT VPRODUTOCLIFOR.CODCLIFOR FROM VPRODUTOCLIFOR WHERE CODEMPRESA = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, codProduto }).ToString();

            if (edita == true)
            {
                cmbAtivo.Select();
                txtCodProduto.Enabled = false;
                carregaCampos();
                //carregaListaIBPTAX();
                // João Pedro Luchiari - Comentado pois o campo NCM foi substituído pelo Lookup
                //carregaGridPadrao("VIBPTAX", gridView1, tabelasFilhas, string.Empty, "WHERE CODIGO = '" + txtCodNCM.Text + "'", gridControl1);
                carregaGridPadrao("VIBPTAX", gridView1, tabelasFilhas, string.Empty, "WHERE CODIGO = '" + lpNCM.txtcodigo.Text + "'", gridControl1);
                carregaGridPadrao("VPRODUTOCODIGO", gridView2, tabelasFilhas, string.Empty, string.Empty, gridCodigoBarras);
                //carregaGridCodigoBarras();
                carregaGridEstoque();

                // Comentado por João Pedro Luchiari pois o carregamento da grid estava errado
                //carregaGridPadrao("VPRODUTOCLIFOR", gridView5, tabelasFilhas, string.Empty, string.Empty, gridCliFor);

                CarregaGridVPRODUTOCLIFOR("VPRODUTOCLIFOR", "WHERE VPRODUTOCLIFOR.CODEMPRESA = '"+ AppLib.Context.Empresa +"' AND VPRODUTOCLIFOR.CODPRODUTO = '"+ codProduto +"'");

                tabelasFilhas.Add("VPRODUTO");
                carregaGridPadrao("VPRODUTOCOM", gridView4, tabelasFilhas, "INNER JOIN VPRODUTO on VPRODUTOCOM.CODPRODCOM = VPRODUTO.CODPRODUTO and VPRODUTOCOM.CODEMPRESA = VPRODUTO.CODEMPRESA", " WHERE VPRODUTOCOM.CODPRODUTO = '" + txtCodProduto.Text + "' AND VPRODUTO.CODEMPRESA = '" + AppLib.Context.Empresa + "'", gridComposto);

                carregaGridPadrao("VPRODUTOTRIBUTO", gridView6, tabelasFilhas, " INNER JOIN VPRODUTO ON VPRODUTOTRIBUTO.CODPRODUTO = VPRODUTO.CODPRODUTO AND VPRODUTOTRIBUTO.CODEMPRESA = VPRODUTO.CODEMPRESA", " WHERE VPRODUTOTRIBUTO.CODPRODUTO = '" + txtCodProduto.Text + "'", gridTributo);
                tabelasFilhas.Clear();

                CarregaGridUnidadeMedida("WHERE CODPRODUTO = '" + codProduto + "'");
            }
            else
            {
                cmbAtivo.SelectedIndex = 0;
                cmbCodClassificacao.SelectedIndex = -1;
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private bool validacoes()
        {
            bool _valida = true;

            if (cmbCodClassificacao.SelectedIndex == -1)
            {
                MessageBox.Show("Favor selecionar a classificação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _valida = false;
                return false;
            }
            if (edita == true)
            {
                if (!string.IsNullOrEmpty(txtCodigoAuxiliar.Text))
                {
                    int retorno = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT COUNT(*) FROM VPRODUTO WHERE CODIGOAUXILIAR = ? AND CODEMPRESA = ? AND CODPRODUTO <> ? ", new object[] { txtCodigoAuxiliar.Text, AppLib.Context.Empresa, txtCodProduto.Text }));
                    if (retorno > 0)
                    {
                        MessageBox.Show(string.Format("Já existe um {0} cadastrado.", AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'VPRODUTO' AND COLUNA = 'VPRODUTO.CODIGOAUXILIAR'", new object[] { })), "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _valida = false;
                        return false;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtCodigoAuxiliar.Text))
                {
                    int retorno = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT COUNT(*) FROM VPRODUTO WHERE CODIGOAUXILIAR = ? AND CODEMPRESA = ?", new object[] { txtCodigoAuxiliar.Text, AppLib.Context.Empresa, txtCodProduto.Text }));
                    if (retorno > 0)
                    {
                        MessageBox.Show(string.Format("Já existe um {0} cadastrado.", AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'VPRODUTO' AND COLUNA = 'VPRODUTO.CODIGOAUXILIAR'", new object[] { })), "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _valida = false;
                        return false;
                    }
                }

            }

            if (cmbAtivo.Text == "Inativo")
            {
                int VerificaPedidos = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT COUNT(GOPERITEM.CODPRODUTO) FROM GOPERITEM INNER JOIN GOPER ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER INNER JOIN GTIPOPER ON GTIPOPER.CODEMPRESA = GOPER.CODEMPRESA AND GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER WHERE GOPER.CODSTATUS IN ('0','5') AND GOPERITEM.QUANTIDADE_SALDO > 0 AND GTIPOPER.ULTIMONIVEL = 0 AND GOPER.CODEMPRESA = ? AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, codProduto }));

                if (VerificaPedidos > 0)
                {
                    MessageBox.Show("Existem operações pendentes para este produto.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _valida = false;
                    return false;
                }
                else
                {
                    int VerificaSaldo = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT SUM(SALDOFINAL) FROM VSALDOESTOQUE WHERE VSALDOESTOQUE.CODEMPRESA = ? AND VSALDOESTOQUE.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, codProduto }));

                    if (VerificaSaldo > 0)
                    {
                        MessageBox.Show("O produto selecionado possui Saldo em Estoque.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _valida = false;
                        return false;
                    }
                }
            }

            PS.Glb.Class.Utilidades util = new PS.Glb.Class.Utilidades();

            _valida = util.validaCamposObrigatorios(this, ref errorProvider);

            return _valida;
        }

        private bool salvar()
        {
            if (validacoes() == false)
            {
                return false;
            }
            try
            {
                DateTime? DATACUSTOMEDIO, DATACUSTOUNITARIO;
                if (!string.IsNullOrEmpty(dteDataCustoMedio.Text))
                {
                    DATACUSTOMEDIO = Convert.ToDateTime(dteDataCustoMedio.EditValue);
                }
                else
                {
                    DATACUSTOMEDIO = null;
                }
                if (!string.IsNullOrEmpty(dteDataCustoUnitario.Text))
                {
                    DATACUSTOUNITARIO = Convert.ToDateTime(dteDataCustoUnitario.EditValue);
                }
                else
                {
                    DATACUSTOUNITARIO = null;
                }

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppLib.Context.poolConnection.Get("Start").ConnectionString);
                conn.Open();
                System.Data.SqlClient.SqlCommand command;
                if (edita == true)
                {
                    command = new System.Data.SqlClient.SqlCommand(@"UPDATE VPRODUTO SET 
CEST = @CEST,
CODFABRICANTE = @CODFABRICANTE,
CODIGOAUXILIAR = @CODIGOAUXILIAR,
CODCLASSIFICACAO = @CODCLASSIFICACAO,
ATIVO = @ATIVO, 
CODMOEDA5  = @CODMOEDA5 ,
CODNCM  = @CODNCM ,
CODNCMEX  = @CODNCMEX ,
CODPRDFABRICANTE  = @CODPRDFABRICANTE ,
CODTIPO  = @CODTIPO ,
INDFABESCALARELEV = @INDFABESCALARELEV,
CODUNIDCOMPRA  = @CODUNIDCOMPRA ,
CODUNIDCONTROLE  = @CODUNIDCONTROLE ,
CODUNIDVENDA  = @CODUNIDVENDA ,
COMPRIMENTO  = @COMPRIMENTO ,
CUSTOMEDIO  = @CUSTOMEDIO ,
CUSTOUNITARIO  = @CUSTOUNITARIO ,
DATACUSTOMEDIO  = @DATACUSTOMEDIO ,
DATACUSTOUNITARIO  = @DATACUSTOUNITARIO ,
DESCRICAO  = @DESCRICAO ,
DIAMETRO  = @DIAMETRO ,
ESPESSURA  = @ESPESSURA ,
ESTOQUEMAXIMO  = @ESTOQUEMAXIMO ,
ESTOQUEMINIMO  = @ESTOQUEMINIMO ,
USAFORMULAESTOQUEMINIMO = @USAFORMULAESTOQUEMINIMO, 
USALOTEPRODUTO = @USALOTEPRODUTO,
IMAGEM  = @IMAGEM ,
LARGURA  = @LARGURA ,
LEADTIME  = @LEADTIME ,
LOCALESTOCAGEM  = @LOCALESTOCAGEM ,
NOME  = @NOME ,
NOMEFANTASIA  = @NOMEFANTASIA ,
NUMREGMINAGRI = @NUMREGMINAGRI,
PDF  = @PDF ,
PESOBRUTO  = @PESOBRUTO ,
PESOLIQUIDO  = @PESOLIQUIDO ,
PRECO1  = @PRECO1 ,
PRECO2  = @PRECO2 ,
PRECO3  = @PRECO3 ,
PRECO4  = @PRECO4 ,
PRECO5  = @PRECO5 ,
PROCEDENCIA  = @PROCEDENCIA ,
PRODSERV  = @PRODSERV ,
ULTIMONIVEL = @ULTIMONIVEL, 
OBSERVACOES = @OBSERVACOES
WHERE CODEMPRESA = @CODEMPRESA AND CODPRODUTO = @CODPRODUTO", conn);

                }
                else
                {
                    command = new System.Data.SqlClient.SqlCommand(@"
INSERT INTO VPRODUTO (
	ATIVO, 
	CEST, 
	CODCLASSIFICACAO, 
	CODEMPRESA, 
	CODFABRICANTE, 
	CODIGOAUXILIAR, 
	CODMOEDA1, 
	CODMOEDA2, 
	CODMOEDA3, 
	CODMOEDA4, 
	CODMOEDA5, 
	CODNCM, 
	CODNCMEX, 
	CODPRDFABRICANTE, 
	CODPRODUTO, 
	CODTIPO, 
    INDFABESCALARELEV,
	CODUNIDCOMPRA, 
	CODUNIDCONTROLE, 
	CODUNIDVENDA, 
	COMPRIMENTO, 
	CUSTOMEDIO, 
	CUSTOUNITARIO, 
	DATACUSTOMEDIO, 
	DATACUSTOUNITARIO, 
	DESCRICAO, 
	DIAMETRO, 
	ESPESSURA, 
	ESTOQUEMAXIMO, 
	ESTOQUEMINIMO, 
	LARGURA, 
	LEADTIME, 
	LOCALESTOCAGEM, 
	NOME, 
	NOMEFANTASIA, 
	NUMREGMINAGRI,
	PESOBRUTO, 
	PESOLIQUIDO, 
	PRECO1, 
	PRECO2, 
	PRECO3, 
	PRECO4, 
	PRECO5, 
	PROCEDENCIA, 
	PRODSERV, 
	ULTIMONIVEL,
    IMAGEM,
    OBSERVACOES, 
    USAFORMULAESTOQUEMINIMO,
    USALOTEPRODUTO
) 
VALUES 
(
    @ATIVO, 
    @CEST, 
    @CODCLASSIFICACAO,
    @CODEMPRESA, 
    @CODFABRICANTE, 
    @CODIGOAUXILIAR, 
    @CODMOEDA1, 
    @CODMOEDA2, 
    @CODMOEDA3, 
    @CODMOEDA4, 
    @CODMOEDA5, 
    @CODNCM, 
    @CODNCMEX, 
    @CODPRDFABRICANTE,
    @CODPRODUTO,
    @CODTIPO,
    @INDFABESCALARELEV,
    @CODUNIDCOMPRA,
    @CODUNIDCONTROLE, 
    @CODUNIDVENDA, 
    @COMPRIMENTO, 
    @CUSTOMEDIO, 
    @CUSTOUNITARIO, 
    @DATACUSTOMEDIO, 
    @DATACUSTOUNITARIO, 
    @DESCRICAO, 
    @DIAMETRO, 
    @ESPESSURA, 
    @ESTOQUEMAXIMO, 
    @ESTOQUEMINIMO, 
    @LARGURA, 
    @LEADTIME, 
    @LOCALESTOCAGEM, 
    @NOME, 
    @NOMEFANTASIA, 
    @NUMREGMINAGRI,
    @PESOBRUTO, 
    @PESOLIQUIDO, 
    @PRECO1, 
    @PRECO2,
    @PRECO3, 
    @PRECO4, 
    @PRECO5, 
    @PROCEDENCIA, 
    @PRODSERV, 
    @ULTIMONIVEL,
    @IMAGEM, 
    @OBSERVACOES, 
    @USAFORMULAESTOQUEMINIMO, 
    @USALOTEPRODUTO
)", conn);

                }
                #region Moeda
                if (string.IsNullOrEmpty(psCodMoeda1.textBox1.Text))
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODMOEDA1", SqlDbType.VarChar)).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODMOEDA1", SqlDbType.VarChar)).Value = psCodMoeda1.textBox1.Text;
                }
                if (string.IsNullOrEmpty(psCodMoeda2.textBox1.Text))
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODMOEDA2", SqlDbType.VarChar)).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODMOEDA2", SqlDbType.VarChar)).Value = psCodMoeda2.textBox1.Text;
                }
                if (string.IsNullOrEmpty(psCodMoeda3.textBox1.Text))
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODMOEDA3", SqlDbType.VarChar)).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODMOEDA3", SqlDbType.VarChar)).Value = psCodMoeda3.textBox1.Text;
                }
                if (string.IsNullOrEmpty(psCodMoeda4.textBox1.Text))
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODMOEDA4", SqlDbType.VarChar)).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODMOEDA4", SqlDbType.VarChar)).Value = psCodMoeda4.textBox1.Text;
                }

                if (string.IsNullOrEmpty(psCodMoeda5.textBox1.Text))
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODMOEDA5", SqlDbType.VarChar)).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODMOEDA5", SqlDbType.VarChar)).Value = psCodMoeda5.textBox1.Text;
                }
                #endregion

                #region Preço
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRECO1", SqlDbType.Decimal)).Value = string.IsNullOrEmpty(txtPreco1.Text) ? Convert.ToDecimal("0,00") : Convert.ToDecimal(txtPreco1.Text);
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRECO2", SqlDbType.Decimal)).Value = string.IsNullOrEmpty(txtPreco2.Text) ? Convert.ToDecimal("0,00") : Convert.ToDecimal(txtPreco2.Text);
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRECO3", SqlDbType.Decimal)).Value = string.IsNullOrEmpty(txtPreco3.Text) ? Convert.ToDecimal("0,00") : Convert.ToDecimal(txtPreco3.Text);
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRECO4", SqlDbType.Decimal)).Value = string.IsNullOrEmpty(txtPreco4.Text) ? Convert.ToDecimal("0,00") : Convert.ToDecimal(txtPreco4.Text);
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRECO5", SqlDbType.Decimal)).Value = string.IsNullOrEmpty(txtPreco5.Text) ? Convert.ToDecimal("0,00") : Convert.ToDecimal(txtPreco5.Text);
                #endregion

                // João Pedro Luchiari - Comentado pois o campo NCM foi substituído pelo Lookup
                //command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODNCM", SqlDbType.VarChar)).Value = txtCodNCM.Text;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODNCM", SqlDbType.VarChar)).Value = lpNCM.txtcodigo.Text;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODNCMEX", SqlDbType.VarChar)).Value = txtCodNCMEx.Text;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODPRDFABRICANTE", SqlDbType.VarChar)).Value = txtCodPrdFabricante.Text;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NOME", SqlDbType.VarChar)).Value = txtNome.Text;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRODSERV", SqlDbType.Int)).Value = cmbProdServ.SelectedIndex == 0 ? 1 : 0;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ATIVO", SqlDbType.Int)).Value = cmbAtivo.SelectedIndex == 0 ? 1 : 0;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODUNIDVENDA", SqlDbType.VarChar)).Value = psCodUnidVenda.textBox1.Text;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODUNIDCOMPRA", SqlDbType.VarChar)).Value = psCodUnidCompra.textBox1.Text;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODUNIDCONTROLE", SqlDbType.VarChar)).Value = psCodUnidControle.textBox1.Text;
                //command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODFABRICANTE", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(psCodFabricante.textBox1.Text) ? DBNull.Value : psCodFabricante.textBox1.Text;
                if (string.IsNullOrEmpty(psCodFabricante.textBox1.Text))
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODFABRICANTE", SqlDbType.VarChar)).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODFABRICANTE", SqlDbType.VarChar)).Value = psCodFabricante.textBox1.Text; ;
                }
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DESCRICAO", SqlDbType.VarChar)).Value = meDescricao.Text;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODTIPO", SqlDbType.Int)).Value = cmbCodTipo.SelectedValue == null ? 0 : cmbCodTipo.SelectedValue;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@INDFABESCALARELEV", SqlDbType.VarChar)).Value = cmbIndEscala.SelectedValue == null ? string.Empty : cmbIndEscala.SelectedValue;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PESOBRUTO", SqlDbType.Decimal)).Value = string.IsNullOrEmpty(txtPesoBruto.Text) ? Convert.ToDecimal("0,00") : Convert.ToDecimal(txtPesoBruto.Text);
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PESOLIQUIDO", SqlDbType.Decimal)).Value = string.IsNullOrEmpty(txtpesoLiquido.Text) ? Convert.ToDecimal("0,00") : Convert.ToDecimal(txtpesoLiquido.Text);
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@COMPRIMENTO", SqlDbType.Decimal)).Value = string.IsNullOrEmpty(txtComprimento.Text) ? Convert.ToDecimal("0,00") : Convert.ToDecimal(txtComprimento.Text);
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LARGURA", SqlDbType.Decimal)).Value = string.IsNullOrEmpty(txtlargura.Text) ? Convert.ToDecimal("0,00") : Convert.ToDecimal(txtlargura.Text);
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ESPESSURA", SqlDbType.Decimal)).Value = string.IsNullOrEmpty(txtEspessura.Text) ? Convert.ToDecimal("0,00") : Convert.ToDecimal(txtEspessura.Text);
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DIAMETRO", SqlDbType.Decimal)).Value = string.IsNullOrEmpty(txtDiametro.Text) ? Convert.ToDecimal("0,00") : Convert.ToDecimal(txtDiametro.Text);
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NOMEFANTASIA", SqlDbType.VarChar)).Value = txtNomeFantasia.Text;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODIGOAUXILIAR", SqlDbType.VarChar)).Value = txtCodigoAuxiliar.Text;

                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CUSTOUNITARIO", SqlDbType.Decimal)).Value = string.IsNullOrEmpty(txtCustoUnitario.Text) ? Convert.ToDecimal("0,00") : Convert.ToDecimal(txtCustoUnitario.Text);
                System.Data.SqlClient.SqlParameter dataCustoUnitario = new System.Data.SqlClient.SqlParameter();
                dataCustoUnitario.IsNullable = true;
                if (DATACUSTOUNITARIO != null)
                {
                    dataCustoUnitario = new System.Data.SqlClient.SqlParameter(@"DATACUSTOUNITARIO", DATACUSTOUNITARIO);
                }
                else
                {
                    dataCustoUnitario = new System.Data.SqlClient.SqlParameter(@"DATACUSTOUNITARIO", DBNull.Value);
                }
                command.Parameters.Add(dataCustoUnitario);


                System.Data.SqlClient.SqlParameter dataCustoMedio = new System.Data.SqlClient.SqlParameter();
                dataCustoMedio.IsNullable = true;
                if (DATACUSTOMEDIO != null)
                {
                    dataCustoMedio = new System.Data.SqlClient.SqlParameter(@"DATACUSTOMEDIO", DATACUSTOMEDIO);
                }
                else
                {
                    dataCustoMedio = new System.Data.SqlClient.SqlParameter(@"DATACUSTOMEDIO", DBNull.Value);
                }
                command.Parameters.Add(dataCustoMedio);

                if (!string.IsNullOrEmpty(txtArquivo.Text))
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PDF", SqlDbType.VarBinary)).Value = System.IO.File.ReadAllBytes(txtArquivo.Text);
                }
                else
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PDF", SqlDbType.VarBinary)).Value = DBNull.Value;
                }

                if (!string.IsNullOrEmpty(cmbCodClassificacao.Text))
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODCLASSIFICACAO", SqlDbType.VarChar)).Value = cmbCodClassificacao.SelectedValue.ToString();
                }
                else
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODCLASSIFICACAO", SqlDbType.VarChar)).Value = DBNull.Value;
                }
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CUSTOMEDIO", SqlDbType.Decimal)).Value = string.IsNullOrEmpty(txtCustoMedio.Text) ? Convert.ToDecimal("0,00") : Convert.ToDecimal(txtCustoMedio.Text);
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PROCEDENCIA", SqlDbType.Int)).Value = cmbProcedencia.SelectedValue == null ? 0 : cmbProcedencia.SelectedValue;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NUMREGMINAGRI", SqlDbType.VarChar)).Value = txtNumRegmiNagRi.Text;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CEST", SqlDbType.VarChar)).Value = txtCest.Text;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ULTIMONIVEL", SqlDbType.Bit)).Value = cmbUltimoNivel.SelectedIndex == 0 ? 1 : 0;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LEADTIME", SqlDbType.Int)).Value = string.IsNullOrEmpty(txtLeadTime.Text) ? Convert.ToInt32("0") : Convert.ToInt32(txtLeadTime.Text.Replace(",", ""));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LOCALESTOCAGEM", SqlDbType.VarChar)).Value = txtLocalEstocagem.Text;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ESTOQUEMINIMO", SqlDbType.Decimal)).Value = string.IsNullOrEmpty(txtEstoqeuMinimo.Text) ? Convert.ToDecimal("0,00") : Convert.ToDecimal(txtEstoqeuMinimo.Text);
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ESTOQUEMAXIMO", SqlDbType.Decimal)).Value = string.IsNullOrEmpty(txtEstoqueMaximo.Text) ? Convert.ToDecimal("0,00") : Convert.ToDecimal(txtEstoqueMaximo.Text);

                if (peImagem.Image != null)
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IMAGEM", SqlDbType.Image)).Value = ConverterFotoParaByteArray();
                }
                else
                {
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IMAGEM", SqlDbType.Image)).Value = DBNull.Value;
                }
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OBSERVACOES", SqlDbType.Text)).Value = tbObservacoes.Text;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@USAFORMULAESTOQUEMINIMO", SqlDbType.Bit)).Value = chkUsaEstoqueMinimo.Checked == true ? 1 : 0;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@USALOTEPRODUTO", SqlDbType.Bit)).Value = chkUsaLoteProduto.Checked == true ? 1 : 0;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODEMPRESA", SqlDbType.Int)).Value = Convert.ToInt32(AppLib.Context.Empresa);
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODPRODUTO", SqlDbType.VarChar)).Value = txtCodProduto.Text;
                command.ExecuteNonQuery();
                SalvaCompl();
                conn.Close();

                edita = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private byte[] ConverterFotoParaByteArray()
        {
            using (var stream = new System.IO.MemoryStream())
            {

                if (peImagem.Image.Width > 300 || peImagem.Image.Width > 300)
                {
                    peImagem.Image = EscalaPercentual(peImagem.Image, 50);
                }


                peImagem.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                lblTamanhoImagem.Text = string.Format("Tamanho da Img: {0}", TamanhoAmigavel(stream.Length));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                byte[] bArray = new byte[stream.Length];
                stream.Read(bArray, 0, System.Convert.ToInt32(stream.Length));
                return bArray;
            }
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                salvar();
            }
            else
            {
                if (salvar() == true)
                {
                    codProduto = txtCodProduto.Text;
                    carregaCampos();

                    lookup.txtcodigo.Text = txtCodProduto.Text;
                    lookup.txtconteudo.Text = txtNome.Text.ToUpper();
                    lookup.ValorCodigoInterno = txtCodProduto.Text;

                    this.Dispose();
                }
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
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (salvar() == true)
            {
                this.Dispose();
                GC.Collect();
            }

        }

        private void txtCodProduto_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodProduto.Text))
            {


                txtCodProduto.Text = proximoNumero(txtCodProduto.Text);
                if (mask.Length == txtCodProduto.Text.Length)
                {
                    cmbUltimoNivel.SelectedIndex = 0;
                }
                else
                {
                    cmbUltimoNivel.SelectedIndex = 1;
                }
            }

            if (string.IsNullOrEmpty(txtCodProduto.Text) || !string.IsNullOrEmpty(txtCodPrdFabricante.Text))
                return;
            else
            {
                txtCodPrdFabricante.Text = txtCodProduto.Text;
            }
        }

        private void carregaListaIBPTAX()
        {
            //  gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODIGO Ncm, UF, NACIONALFEDERAL Federal, IMPORTADOSFEDERAL Importado, ESTADUAL Estadual, MUNICIPAL Municipal, CHAVE Chave FROM VIBPTAX WHERE CODIGO = ?", new object[] { txtCodNCM.Text });
            //gridView2.Columns["Federal"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            string tabela = "VIBPTAX";
            //Verifica se existe registro na tabela GVISAOUSUARIO
            int colunas = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(COLUNA) FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1", new object[] { tabela, AppLib.Context.Usuario }));
            if (colunas == 0)
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });

                DataTable db = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = ?", new object[] { tabela });
                for (int i = 0; i < db.Rows.Count; i++)
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO GVISAOUSUARIO (VISAO, CODUSUARIO, SEQUENCIA, COLUNA, LARGURA, VISIVEL) VALUES (?, ?, ?, ?, ?, ?)", new object[] { tabela, AppLib.Context.Usuario, i, db.Rows[i]["COLUMN_NAME"].ToString(), 100, 1 });
                }
            }
            DataTable dt = new DataTable();
            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
            string sql = string.Empty;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(sql))
                {
                    sql = "SELECT " + dt.Rows[i]["COLUNA"].ToString();
                }
                else
                {
                    sql = sql + ", " + dt.Rows[i]["COLUNA"].ToString();
                }
            }
            // João Pedro Luchiari - Comentado pois o campo NCM foi substituído pelo Lookup
            //sql = sql + " FROM " + tabela + " WHERE CODIGO = '" + txtCodNCM.Text + "'";
            sql = sql + " FROM " + tabela + " WHERE CODIGO = '" + lpNCM.txtcodigo.Text + "'";
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

            DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                DataRow result = dic.Rows.Find(new object[] { gridView1.Columns[i].FieldName.ToString() });
                if (result != null)
                {
                    gridView1.Columns[i].Caption = result["DESCRICAO"].ToString();
                }
            }
        }

        private string proximoNumero(string _codProduto)
        {
            string CodProdutoNovo = string.Empty;

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPARAMETROS WHERE VPARAMETROS.CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });

            if (dt.Rows.Count > 0)
            {
                System.Data.DataRow PARAMVAREJO = dt.Rows[0];

                string mask = (PARAMVAREJO["MASKPRODSERV"] == DBNull.Value) ? string.Empty : PARAMVAREJO["MASKPRODSERV"].ToString();
                if (mask != string.Empty)
                {
                    string codProduto = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODPRODUTO FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, _codProduto }).ToString();

                    if (!string.IsNullOrEmpty(codProduto))
                    {
                        string code = codProduto;
                        if (code.Length < mask.Length)
                        {
                            if (mask.Substring(code.Length, 1) == ".")
                            {
                                System.Text.RegularExpressions.Regex rSplit = new System.Text.RegularExpressions.Regex(";");
                                string[] sMask = rSplit.Split(mask.Replace('.', ';'));
                                int nivelMask = sMask.Length;
                                string[] sCode = rSplit.Split(code.Replace('.', ';'));
                                int nivelCode = sCode.Length;
                                if (nivelCode < nivelMask)
                                {
                                    string CodProdutoProximoMask = string.Concat(codProduto, ".", sMask[nivelCode].Replace('#', '_'));
                                    string CodProdutoUltimo = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT MAX(CODPRODUTO) CODPRODUTO FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO LIKE ?", AppLib.Context.Empresa, CodProdutoProximoMask).ToString();
                                    //aqui
                                    if (!string.IsNullOrEmpty(CodProdutoUltimo))
                                    {
                                        string[] sCodeUlt = rSplit.Split(CodProdutoUltimo.Replace('.', ';'));
                                        if (sCodeUlt.Length > 0)
                                        {
                                            int UltinoNum = Convert.ToInt32(sCodeUlt[nivelCode]);
                                            UltinoNum++;
                                            CodProdutoNovo = string.Concat(code, ".", UltinoNum.ToString().PadLeft(sCodeUlt[nivelCode].Length, '0'));
                                        }
                                        else
                                        {
                                            CodProdutoNovo = codProduto;
                                        }
                                    }
                                    else
                                    {
                                        CodProdutoNovo = codProduto + ".0001";
                                    }
                                }
                                else
                                {
                                    CodProdutoNovo = codProduto;
                                }
                            }
                            else
                            {
                                CodProdutoNovo = codProduto;
                            }
                        }
                        else
                        {
                            CodProdutoNovo = codProduto;
                        }
                    }
                    else
                    {

                        CodProdutoNovo = _codProduto;
                    }
                }

                return CodProdutoNovo;
            }
            return string.Empty;

        }

        private void txtPesoBruto_Validated(object sender, EventArgs e)
        {
            txtPesoBruto.Text = string.Format("{0:n2}", txtPesoBruto.Text);
        }

        private void txtCodNCM_Validating(object sender, CancelEventArgs e)
        {
            carregaListaIBPTAX();
        }

        private void carregaGridCodigoBarras()
        {
            string tabela = "VPRODUTOCODIGO";
            //Verifica se existe registro na tabela GVISAOUSUARIO
            int colunas = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(COLUNA) FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1", new object[] { tabela, AppLib.Context.Usuario }));
            if (colunas == 0)
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });

                DataTable db = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = ?", new object[] { tabela });
                for (int i = 0; i < db.Rows.Count; i++)
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO GVISAOUSUARIO (VISAO, CODUSUARIO, SEQUENCIA, COLUNA, LARGURA, VISIVEL) VALUES (?, ?, ?, ?, ?, ?)", new object[] { tabela, AppLib.Context.Usuario, i, db.Rows[i]["COLUMN_NAME"].ToString(), 100, 1 });
                }
            }
            DataTable dt = new DataTable();
            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
            string sql = string.Empty;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(sql))
                {
                    sql = "SELECT " + dt.Rows[i]["COLUNA"].ToString();
                }
                else
                {
                    sql = sql + ", " + dt.Rows[i]["COLUNA"].ToString();
                }
            }
            sql = sql + " FROM " + tabela + " WHERE CODPRODUTO = '" + txtCodProduto.Text + "' AND CODEMPRESA = " + AppLib.Context.Empresa;
            gridCodigoBarras.DataSource = null;
            gridView2.Columns.Clear();
            gridCodigoBarras.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

            DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
            for (int i = 0; i < gridView2.Columns.Count; i++)
            {
                gridView2.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                DataRow result = dic.Rows.Find(new object[] { gridView2.Columns[i].FieldName.ToString() });
                if (result != null)
                {
                    gridView2.Columns[i].Caption = result["DESCRICAO"].ToString();
                }
            }
        }

        private void btnNovoCodigoBarras_Click(object sender, EventArgs e)
        {
            frmCodigoBarras frm = new frmCodigoBarras(txtCodProduto.Text);
            frm.ShowDialog();
            carregaGridCodigoBarras();
        }

        private void txtLocalEstocagem_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void fmCadastroProduto_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    break;
                case Keys.F10:
                    break;
                case Keys.F11:
                    break;
                case Keys.F12:
                    break;
                case Keys.F13:
                    break;
                case Keys.F14:
                    break;
                case Keys.F15:
                    break;
                case Keys.F16:
                    break;
                case Keys.F17:
                    break;
                case Keys.F18:
                    break;
                case Keys.F19:
                    break;
                case Keys.F2:
                    break;
                case Keys.F20:
                    break;
                case Keys.F21:
                    break;
                case Keys.F22:
                    break;
                case Keys.F23:
                    break;
                case Keys.F24:
                    break;
                case Keys.F3:
                    break;
                case Keys.F4:
                    break;
                case Keys.F5:
                    break;
                case Keys.F6:
                    if (string.IsNullOrEmpty(txtCodProduto.Text))
                    {
                        PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque frm = new PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque();
                        frm.ShowDialog();
                    }
                    else
                    {
                        PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque frm = new PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque(txtCodProduto.Text);
                        frm.getProduto();
                        frm.ShowDialog();

                    }
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    break;
                case Keys.F9:
                    break;
                case Keys.Insert:
                    break;
                default:
                    break;
            }
        }

        private void fmCadastroProduto_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void splitContainer1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void splitContainer2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    break;
                case Keys.F10:
                    break;
                case Keys.F11:
                    break;
                case Keys.F12:
                    break;
                case Keys.F13:
                    break;
                case Keys.F14:
                    break;
                case Keys.F15:
                    break;
                case Keys.F16:
                    break;
                case Keys.F17:
                    break;
                case Keys.F18:
                    break;
                case Keys.F19:
                    break;
                case Keys.F2:
                    break;
                case Keys.F20:
                    break;
                case Keys.F21:
                    break;
                case Keys.F22:
                    break;
                case Keys.F23:
                    break;
                case Keys.F24:
                    break;
                case Keys.F3:
                    break;
                case Keys.F4:
                    break;
                case Keys.F5:
                    break;
                case Keys.F6:
                    if (string.IsNullOrEmpty(txtCodProduto.Text))
                    {
                        PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque frm = new PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque();
                        frm.ShowDialog();
                    }
                    else
                    {
                        PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque frm = new PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque(txtCodProduto.Text);
                        frm.getProduto();
                        frm.ShowDialog();

                    }
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    break;
                case Keys.F9:
                    break;
                case Keys.Insert:
                    //btnNovo_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void carregaGridEstoque()
        {
            try
            {
                gridEstoque.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
               SELECT 
 VSALDOESTOQUE.CODLOCAL
,VLOCALESTOQUE.NOME
,VSALDOESTOQUE.SALDOFINAL
,VSALDOESTOQUE.CODUNIDCONTROLE
,VSALDOESTOQUE.TOTALFINAL
,VSALDOESTOQUE.CUSTOMEDIO
,VSALDOESTOQUE.DATAENTSAI
,VSALDOESTOQUE.CODEMPRESA
,VSALDOESTOQUE.CODFILIAL

FROM VSALDOESTOQUE
INNER JOIN VLOCALESTOQUE ON VLOCALESTOQUE.CODEMPRESA = VSALDOESTOQUE.CODEMPRESA AND VLOCALESTOQUE.CODLOCAL = VSALDOESTOQUE.CODLOCAL

WHERE 
 VSALDOESTOQUE.CODEMPRESA = ?
AND VSALDOESTOQUE.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, txtCodProduto.Text });

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lkpCodProduto_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lkpCodProduto.Text))

            {
                DataTable dtProduto = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
VPRODUTO.CODIGOAUXILIAR,
VPRODUTO.CODPRODUTO, 
VPRODUTO.NOME

FROM 
VPRODUTO 

WHERE
VPRODUTO.CODEMPRESA = ?
AND VPRODUTO.ULTIMONIVEL = 1 
AND VPRODUTO.ATIVO = 1
AND CODIGOAUXILIAR LIKE '%" + lkpCodProduto.Text + "%' ", new object[] { AppLib.Context.Empresa });

                if (dtProduto.Rows.Count > 1)
                {
                    PS.Glb.New.Visao.frmVisaoProduto frm = new PS.Glb.New.Visao.frmVisaoProduto(@"WHERE VPRODUTO.CODIGOAUXILIAR LIKE '%" + lkpCodProduto.Text + "%' AND VPRODUTO.CODEMPRESA = " + AppLib.Context.Empresa, false);
                    frm.consulta = true;
                    frm.ShowDialog();

                    lkpCodProduto.Text = frm.codProduto;
                    txtDescricaoProduto.Text = frm.nome;

                    txtUnidadeComposto.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODUNIDCONTROLE FROM VPRODUTO WHERE CODPRODUTO = ? AND CODEMPRESA = ? ", new object[] { lkpCodProduto.Text, AppLib.Context.Empresa }).ToString();
                }
                else
                {
                    lkpCodProduto.Text = string.Empty;
                    txtDescricaoProduto.Text = string.Empty;
                }
            }
        }

        private void btnLookupProduto_Click(object sender, EventArgs e)
        {

            PS.Glb.New.Visao.frmVisaoProduto frm = new PS.Glb.New.Visao.frmVisaoProduto(@"SELECT 
VPRODUTO.CODPRODUTO AS 'VPRODUTO.CODPRODUTO', 
VPRODUTO.CODIGOAUXILIAR AS 'VPRODUTO.CODIGOAUXILIAR', 
VPRODUTO.NOME AS 'VPRODUTO.NOME', 
VPRODUTO.CODUNIDVENDA AS 'VPRODUTO.CODUNIDVENDA'
FROM 
VPRODUTO 
WHERE VPRODUTO.CODEMPRESA = " + AppLib.Context.Empresa + @"
AND VPRODUTO.ULTIMONIVEL = 1 
AND VPRODUTO.ATIVO = 1", true);
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.codProduto))
            {
                lkpCodProduto.Text = frm.codProduto;
                txtDescricaoProduto.Text = frm.nome;
                txtUnidadeComposto.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODUNIDCONTROLE FROM VPRODUTO WHERE CODPRODUTO = ? AND CODEMPRESA = ? ", new object[] { lkpCodProduto.Text, AppLib.Context.Empresa }).ToString();
            }

        }

        private void btnAddComposto_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lkpCodProduto.Text))
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                conn.BeginTransaction();
                try
                {
                    AppLib.ORM.Jit VPRODUTOCOM = new AppLib.ORM.Jit(conn, "VPRODUTOCOM");
                    VPRODUTOCOM.Set("CODEMPRESA", AppLib.Context.Empresa);
                    VPRODUTOCOM.Set("CODPRODUTO", txtCodProduto.Text);
                    VPRODUTOCOM.Set("CODPRODCOM", lkpCodProduto.Text);
                    VPRODUTOCOM.Set("QUANTIDADE", txtQuantidadeComposto.Text);
                    VPRODUTOCOM.Set("UNIDADE", txtUnidadeComposto.Text);
                    VPRODUTOCOM.Save();
                    conn.Commit();
                    limpaComposto();
                    tabelasFilhas.Add("VPRODUTO");
                    carregaGridPadrao("VPRODUTOCOM", gridView4, tabelasFilhas, "INNER JOIN VPRODUTO on VPRODUTOCOM.CODPRODCOM = VPRODUTO.CODPRODUTO and VPRODUTOCOM.CODEMPRESA = VPRODUTO.CODEMPRESA", " WHERE VPRODUTOCOM.CODPRODUTO = '" + txtCodProduto.Text + "' AND VPRODUTO.CODEMPRESA = '" + AppLib.Context.Empresa + "'", gridComposto);
                }
                catch (Exception ex)
                {
                    conn.Rollback();
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void limpaComposto()
        {
            lkpCodProduto.Text = string.Empty;
            txtDescricaoProduto.Text = string.Empty;
            txtQuantidadeComposto.Text = string.Empty;
            txtUnidadeComposto.Text = string.Empty;
        }

        public void CarregaGridVPRODUTOCLIFOR(string Tabela, string where)
        {
            try
            {
                string sql = string.Empty;

                List<string> Tabcompl = new List<string>();

                Tabcompl.Add("VCLIFOR");

                sql = new Class.Utilidades().getVisao(Tabela, "INNER JOIN VCLIFOR ON VPRODUTOCLIFOR.CODEMPRESA = VCLIFOR.CODEMPRESA AND VPRODUTOCLIFOR.CODCLIFOR = VCLIFOR.CODCLIFOR", Tabcompl, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridCliFor.DataSource = null;
                gridView5.Columns.Clear();
                gridCliFor.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { Tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { Tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridView5.Columns.Count; i++)
                {
                    gridView5.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridView5.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridView5.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //try
            //{
            //    string sql = string.Empty;

            //    sql = @"SELECT VPRODUTOCLIFOR.CODEMPRESA AS 'Cód. Empresa', VPRODUTO.CODPRODUTO AS 'Cód. Produto', VPRODUTOCLIFOR.CODPRDFORNECEDOR AS 'Cód. Identificador', VPRODUTOCLIFOR.CODCLIFOR AS 'Cód. Cliente', VCLIFOR.NOME AS 'Razão Social', VPRODUTOCLIFOR.CODMOEDA AS 'Cód. Moeda', VPRODUTOCLIFOR.ATIVO 'Ativo', VPRODUTOCLIFOR.VALOR AS 'Valor', VPRODUTOCLIFOR.DESCONTO AS 'Desconto', VPRODUTOCLIFOR.LEADTIME AS 'Leadtime', VPRODUTOCLIFOR.OBS AS 'Observação'
            //            FROM VPRODUTOCLIFOR
            //            INNER JOIN VCLIFOR ON VPRODUTOCLIFOR.CODEMPRESA = VCLIFOR.CODEMPRESA AND VPRODUTOCLIFOR.CODCLIFOR = VCLIFOR.CODCLIFOR
            //            INNER JOIN VPRODUTO ON VPRODUTOCLIFOR.CODEMPRESA = VPRODUTO.CODEMPRESA AND VPRODUTOCLIFOR.CODPRODUTO = VPRODUTO.CODPRODUTO
            //            WHERE VPRODUTOCLIFOR.CODEMPRESA = '"+ AppLib.Context.Empresa +"' AND VPRODUTOCLIFOR.CODPRODUTO = '"+ codProduto +"' AND VPRODUTOCLIFOR.CODCLIFOR = '"+ Codclifor +"'";

            //    if (string.IsNullOrEmpty(sql))
            //    {
            //        MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //    gridCliFor.DataSource = null;
            //    gridView5.Columns.Clear();
            //    gridCliFor.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

            //    gridView5.BestFitColumns();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void carregaGridPadrao(string tabela, DevExpress.XtraGrid.Views.Grid.GridView grid, List<string> tabelasFilhas, string relacionamento, string where, DevExpress.XtraGrid.GridControl gridControl)
        {
            try
            {
                string sql = new Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {
                    sql = sql + filtroUsuario;
                }

                gridControl.DataSource = null;
                grid.Columns.Clear();
                gridControl.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });

                for (int i = 0; i < grid.Columns.Count; i++)
                {
                    grid.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { grid.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        grid.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluirComposto_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir o composto?", "Informação do Sistema.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < gridView4.SelectedRowsCount; i++)
                {
                    DataRow row = gridView4.GetDataRow(Convert.ToInt32(gridView4.GetSelectedRows().GetValue(i).ToString()));
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM VPRODUTOCOM WHERE CODEMPRESA = ? AND CODPRODCOM = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, row["VPRODUTOCOM.CODPRODCOM"], row["VPRODUTOCOM.CODPRODUTO"] });
                }
                tabelasFilhas.Add("VPRODUTO");
                carregaGridPadrao("VPRODUTOCOM", gridView4, tabelasFilhas, "INNER JOIN VPRODUTO on VPRODUTOCOM.CODPRODCOM = VPRODUTO.CODPRODUTO and VPRODUTOCOM.CODEMPRESA = VPRODUTO.CODEMPRESA", " WHERE VPRODUTOCOM.CODPRODUTO = '" + txtCodProduto.Text + "' AND VPRODUTO.CODEMPRESA = '" + AppLib.Context.Empresa + "'", gridComposto);
            }
        }

        private void btnNovoCliFor_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodProduto.Text))
            {
                frmCadastroProdutoFornecedor frm = new frmCadastroProdutoFornecedor();
                frm.codProduto = txtCodProduto.Text;
                frm.ShowDialog();
                CarregaGridVPRODUTOCLIFOR("VPRODUTOCLIFOR", "WHERE VPRODUTOCLIFOR.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND VPRODUTOCLIFOR.CODPRODUTO = '" + codProduto + "'");
                //carregaGridPadrao("VPRODUTOCLIFOR", gridView5, tabelasFilhas, string.Empty, string.Empty, gridCliFor);
            }
        }

        private void btnEditarCliFor_Click(object sender, EventArgs e)
        {
            editaCliFor();
        }

        private void atualizaColuna(DataRow dr, DevExpress.XtraGrid.Views.Grid.GridView grid)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPRODUTOCLIFOR WHERE CODPRODUTO = ? AND CODEMPRESA = ? AND CODCLIFOR = ? and CODPRDFORNECEDOR = ?", new object[] { dr["VPRODUTOCLIFOR.CODPRODUTO"], AppLib.Context.Empresa, dr["VPRODUTOCLIFOR.CODCLIFOR"], dr["VPRODUTOCLIFOR.CODPRDFORNECEDOR"] });

            for (int i = 0; i < grid.VisibleColumns.Count; i++)
            {
                try
                {
                    dr[grid.Columns[i].FieldName] = dt.Rows[0][grid.Columns[i].FieldName.ToString().Remove(0, grid.Columns[i].FieldName.ToString().IndexOf(".") + 1)].ToString();
                }
                catch (Exception)
                {

                }

            }
        }

        private void gridCliFor_DoubleClick(object sender, EventArgs e)
        {
            editaCliFor();
        }

        private void editaCliFor()
        {
            if (gridView5.SelectedRowsCount > 0)
            {
                if (gridView5.SelectedRowsCount == 1)
                {
                    frmCadastroProdutoFornecedor frm = new frmCadastroProdutoFornecedor();
                    DataRow row1 = gridView5.GetDataRow(Convert.ToInt32(gridView5.GetSelectedRows().GetValue(0).ToString()));
                    frm.codCliFor = row1["VPRODUTOCLIFOR.CODCLIFOR"].ToString();
                    frm.codPrdFornecedor = row1["VPRODUTOCLIFOR.CODPRDFORNECEDOR"].ToString();
                    frm.codProduto = row1["VPRODUTOCLIFOR.CODPRODUTO"].ToString();
                    frm.edita = true;
                    frm.ShowDialog();
                    atualizaColuna(row1, gridView5);
                }
            }
        }

        private void btnExcluirCliFor_Click(object sender, EventArgs e)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "VPRODUTOCLIFOR");
            conn.BeginTransaction();
            for (int i = 0; i < gridView5.SelectedRowsCount; i++)
            {
                try
                {
                    DataRow row1 = gridView5.GetDataRow(Convert.ToInt32(gridView5.GetSelectedRows().GetValue(i).ToString()));
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODPRODUTO", codProduto);
                    v.Set("CODCLIFOR", row1["VPRODUTOCLIFOR.CODCLIFOR"].ToString());
                    v.Set("CODPRDFORNECEDOR", row1["VPRODUTOCLIFOR.CODPRDFORNECEDOR"].ToString());
                    v.Delete();
                    conn.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Rollback();
                }
            }
            CarregaGridVPRODUTOCLIFOR("VPRODUTOCLIFOR", "WHERE VPRODUTOCLIFOR.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND VPRODUTOCLIFOR.CODPRODUTO = '" + codProduto + "'");
            //carregaGridPadrao("VPRODUTOCLIFOR", gridView5, tabelasFilhas, string.Empty, string.Empty, gridCliFor);
        }

        private void btnAtualizarTributo_Click(object sender, EventArgs e)
        {
            tabelasFilhas.Add("VPRODUTO");
            carregaGridPadrao("VPRODUTOTRIBUTO", gridView6, tabelasFilhas, " INNER JOIN VPRODUTO ON VPRODUTOTRIBUTO.CODPRODUTO = VPRODUTO.CODPRODUTO AND VPRODUTOTRIBUTO.CODEMPRESA = VPRODUTO.CODEMPRESA", " WHERE VPRODUTOTRIBUTO.CODPRODUTO = '" + txtCodProduto.Text + "'", gridTributo);
        }

        private void btnSelecionarColunasTributo_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas("VPRODUTOTRIBUTO");
            frm.ShowDialog();
            tabelasFilhas.Add("VPRODUTO");
            carregaGridPadrao("VPRODUTOTRIBUTO", gridView6, tabelasFilhas, " INNER JOIN VPRODUTO ON VPRODUTOTRIBUTO.CODPRODUTO = VPRODUTO.CODPRODUTO AND VPRODUTOTRIBUTO.CODEMPRESA = VPRODUTO.CODEMPRESA", " WHERE VPRODUTOTRIBUTO.CODPRODUTO = '" + txtCodProduto.Text + "'", gridTributo);
        }

        private void btnSalvarLayoutTributo_Click(object sender, EventArgs e)
        {
            string tabela = "VPRODUTOTRIBUTO";
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });

            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", tabela);
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView1.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView1.VisibleColumns[i].Width);
                GVISAOUSUARIO.Set("VISIVEL", 1);
                GVISAOUSUARIO.Save();
            }

            DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ? AND FIXO = 1", new object[] { AppLib.Context.Usuario, tabela });
            if (dtFixo.Rows.Count > 0)
            {
                for (int i = 0; i < dtFixo.Rows.Count; i++)
                {
                    AppLib.ORM.Jit FIXO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                    FIXO.Set("VISAO", tabela);
                    FIXO.Set("CODUSUARIO", AppLib.Context.Usuario);
                    FIXO.Set("COLUNA", dtFixo.Rows[i]["COLUNA"]);
                    FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
                carregaGridTributo();
            }
        }

        private void carregaGridTributo()
        {
            tabelasFilhas.Add("VPRODUTO");
            carregaGridPadrao("VPRODUTOTRIBUTO", gridView6, tabelasFilhas, " INNER JOIN VPRODUTO ON VPRODUTOTRIBUTO.CODPRODUTO = VPRODUTO.CODPRODUTO AND VPRODUTOTRIBUTO.CODEMPRESA = VPRODUTO.CODEMPRESA", " WHERE VPRODUTOTRIBUTO.CODPRODUTO = '" + txtCodProduto.Text + "'", gridTributo);
        }

        private void btnAgruparTributo_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsView.ShowGroupPanel == true)
            {
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.ClearGrouping();
                btnAgrupar.Text = "Agrupar";
            }
            else
            {
                gridView1.OptionsView.ShowGroupPanel = true;
                btnAgrupar.Text = "Desagrupar";
            }
        }

        private void btnPesquisarTributo_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsFind.AlwaysVisible == true)
            {
                gridView1.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView1.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnNovoTributo_Click(object sender, EventArgs e)
        {
            Anexos.frmCadastroTributoProduto frm = new Anexos.frmCadastroTributoProduto(txtCodProduto.Text);
            frm.edita = false;
            frm.ShowDialog();
            carregaGridTributo();
        }

        private void btnEditarTributo_Click(object sender, EventArgs e)
        {
            if (gridView6.SelectedRowsCount > 0)
            {
                DataRow row1 = gridView6.GetDataRow(Convert.ToInt32(gridView6.GetSelectedRows().GetValue(0).ToString()));
                Anexos.frmCadastroTributoProduto frm = new Anexos.frmCadastroTributoProduto(txtCodProduto.Text);
                frm.edita = true;
                frm.codTributo = row1["VPRODUTOTRIBUTO.CODTRIBUTO"].ToString();
                frm.ShowDialog();
                atualizaColunaGridTributo(row1, gridView6);
            }
        }

        private void atualizaColunaGridTributo(DataRow dr, DevExpress.XtraGrid.Views.Grid.GridView grid)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPRODUTOTRIBUTO WHERE CODPRODUTO = ? AND CODTRIBUTO = ? AND CODEMPRESA = ?", new object[] { dr["VPRODUTOTRIBUTO.CODPRODUTO"], dr["VPRODUTOTRIBUTO.CODTRIBUTO"], AppLib.Context.Empresa });

            for (int i = 0; i < grid.VisibleColumns.Count; i++)
            {
                try
                {
                    dr[grid.Columns[i].FieldName] = dt.Rows[0][grid.Columns[i].FieldName.ToString().Remove(0, grid.Columns[i].FieldName.ToString().IndexOf(".") + 1)].ToString();
                }
                catch (Exception)
                {

                }

            }
        }

        private void gridTributo_DoubleClick(object sender, EventArgs e)
        {
            btnEditarTributo_Click(sender, e);
        }

        private void btnExcluirTributo_Click(object sender, EventArgs e)
        {
            if (gridView6.SelectedRowsCount > 0)
            {
                DataRow row1 = gridView6.GetDataRow(Convert.ToInt32(gridView6.GetSelectedRows().GetValue(0).ToString()));
                AppLib.Context.poolConnection.Get("START").ExecTransaction("DELETE FROM VPRODUTOTRIBUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, row1["VPRODUTOTRIBUTO.CODPRODUTO"], row1["VPRODUTOTRIBUTO.CODTRIBUTO"] });
                carregaGridTributo();

            }
        }

        private void btnAtualizarComposto_Click(object sender, EventArgs e)
        {
            tabelasFilhas.Add("VPRODUTO");
            carregaGridPadrao("VPRODUTOCOM", gridView4, tabelasFilhas, "INNER JOIN VPRODUTO on VPRODUTOCOM.CODPRODCOM = VPRODUTO.CODPRODUTO and VPRODUTOCOM.CODEMPRESA = VPRODUTO.CODEMPRESA", " WHERE VPRODUTOCOM.CODPRODUTO = '" + txtCodProduto.Text + "' AND VPRODUTO.CODEMPRESA = '" + AppLib.Context.Empresa + "'", gridComposto);
        }

        private void btnSelecionarComposto_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas("VPRODUTOCOM");
            frm.ShowDialog();
            tabelasFilhas.Add("VPRODUTO");
            carregaGridPadrao("VPRODUTOCOM", gridView4, tabelasFilhas, "INNER JOIN VPRODUTO on VPRODUTOCOM.CODPRODCOM = VPRODUTO.CODPRODUTO and VPRODUTOCOM.CODEMPRESA = VPRODUTO.CODEMPRESA", " WHERE VPRODUTOCOM.CODPRODUTO = '" + txtCodProduto.Text + "' AND VPRODUTO.CODEMPRESA = '" + AppLib.Context.Empresa + "'", gridComposto);
        }


        private void btnSalvarLayoutComposto_Click(object sender, EventArgs e)
        {
            string tabela = "VPRODUTOCOM";
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });

            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", tabela);
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView1.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView1.VisibleColumns[i].Width);
                GVISAOUSUARIO.Set("VISIVEL", 1);
                GVISAOUSUARIO.Save();
            }

            DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ? AND FIXO = 1", new object[] { AppLib.Context.Usuario, tabela });
            if (dtFixo.Rows.Count > 0)
            {
                for (int i = 0; i < dtFixo.Rows.Count; i++)
                {
                    AppLib.ORM.Jit FIXO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                    FIXO.Set("VISAO", tabela);
                    FIXO.Set("CODUSUARIO", AppLib.Context.Usuario);
                    FIXO.Set("COLUNA", dtFixo.Rows[i]["COLUNA"]);
                    FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
                carregaGridTributo();
            }
        }


        private bool SalvaCompl()
        {
            List<Class.Parametro> listaParametro = new List<Class.Parametro>();

            Class.Parametro parametroEmpresa = new Class.Parametro();
            parametroEmpresa.nomeParametro = "CODEMPRESA";
            parametroEmpresa.valorParametro = "1";
            listaParametro.Add(parametroEmpresa);

            Class.Parametro parametroProduto = new Class.Parametro();
            parametroProduto.nomeParametro = "CODPRODUTO";
            parametroProduto.valorParametro = txtCodProduto.Text;
            listaParametro.Add(parametroProduto);

            if (new Class.Utilidades().salvaCamposComplementares(this, "VPRODUTOCOMPL", tabCamposComplementares, listaParametro, AppLib.Context.poolConnection.Get("Start")) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
            //if (tabCamposComplementares.Controls.Count > 0)
            //{
            //    bool retorno = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT COUNT(CODPRODUTO) FROM VPRODUTOCOMPL WHERE CODEMPRESA = ? AND CODPRODUTO = ? ", new object[] { AppLib.Context.Empresa, txtCodProduto.Text }));
            //    if (retorno == false)
            //    {
            //        AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VPRODUTOCOMPL (CODEMPRESA, CODPRODUTO) VALUES (?, ?)", new object[] { AppLib.Context.Empresa, txtCodProduto.Text });
            //    }

            //    Class.Utilidades util = new Class.Utilidades();
            //    string sql = util.update(this, tabCamposComplementares, "VPRODUTOCOMPL");
            //    if (!string.IsNullOrEmpty(sql))
            //    {
            //        sql = sql.Remove(sql.Length - 2, 1) + " WHERE CODEMPRESA = ? AND CODPRODUTO = ?";
            //        AppLib.Context.poolConnection.Get("Start").ExecTransaction(sql, new object[] { AppLib.Context.Empresa, txtCodProduto.Text });
            //    }
            //    return true;
            //}
            //else
            //{
            //    return true;
            //}
        }

        private void carregaCamposComplementares()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPRODUTOCOMPL WHERE CODPRODUTO = ? AND CODEMPRESA = ?", new object[] { txtCodProduto.Text, AppLib.Context.Empresa });
            DataTable dtColunas = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT NOMECAMPO FROM GTABCAMPOCOMPL WHERE CODENTIDADE = ? AND ATIVO = ? ORDER BY ORDEM", new object[] { "VPRODUTOCOMPL", 1 });
            if (dt.Rows.Count > 0)
            {
                Control controle;
                for (int iColunas = 0; iColunas < dtColunas.Rows.Count; iColunas++)
                {
                    for (int i = 0; i < tabCamposComplementares.Controls.Count; i++)
                    {
                        controle = tabCamposComplementares.Controls[i];
                        if (controle.GetType().Name.Equals("TextEdit") || controle.GetType().Name.Equals("ComboBox") || controle.GetType().Name.Equals("DateEdit") || controle.GetType().Name.Equals("MemoEdit"))
                        {
                            if (controle.Name.Remove(0, 4) == dtColunas.Rows[iColunas]["NOMECAMPO"].ToString())
                            {
                                controle.Text = dt.Rows[0][dtColunas.Rows[iColunas]["NOMECAMPO"].ToString()].ToString();
                            }
                        }
                        else if (controle.GetType().Name.Equals("CheckEdit"))
                        {
                            if (controle.Name.Remove(0, 4) == dtColunas.Rows[iColunas]["NOMECAMPO"].ToString())
                            {
                                if (dt.Rows[0][dtColunas.Rows[iColunas]["NOMECAMPO"].ToString()].ToString() == "1")
                                {
                                    DevExpress.XtraEditors.CheckEdit ck = (DevExpress.XtraEditors.CheckEdit)controle;
                                    ck.Checked = true;
                                }
                                else
                                {
                                    DevExpress.XtraEditors.CheckEdit ck = (DevExpress.XtraEditors.CheckEdit)controle;
                                    ck.Checked = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        #region Movimentações dos Produtos

        private void btnFiltrosMovProd_Click(object sender, EventArgs e)
        {
            PS.Glb.New.Filtro.frmFiltroMovimentacaoProduto prod = new Filtro.frmFiltroMovimentacaoProduto();
            prod.Codproduto = codProduto;
            prod.aberto = true;
            prod.ShowDialog();
            if (this.lookup == null)
            {
                if (!string.IsNullOrEmpty(prod.condicao))
                {
                    query = prod.condicao;
                    CarregaGridMovimentacaoProduto(query);

                    btnAgruparMovProd.Enabled = true;
                    BtnPesquisarMovProd.Enabled = true;
                    btnVisaoMovProd.Enabled = true;
                }
            }
            else
            {
                query = prod.condicao;
                CarregaGridMovimentacaoProduto(query);
            }
        }

        private void CarregaGridMovimentacaoProduto(string where)
        {
            string relacao = "INNER JOIN GOPERITEM ON GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO INNER JOIN GOPER ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER INNER JOIN GTIPOPER ON GTIPOPER.CODEMPRESA = GOPER.CODEMPRESA AND GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER LEFT OUTER JOIN VCLIFOR ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR";
            List<string> tabelasFilhas = new List<string>();
            tabelasFilhas.Add("GOPER");
            tabelasFilhas.Add("GOPERITEM");
            tabelasFilhas.Add("VCLIFOR");

            try
            {
                string sql = new Class.Utilidades().getVisao("MOVIMENTACAO", relacao, tabelasFilhas, where);

                sql = sql.Replace("MOVIMENTACAO", "VPRODUTO");
                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                gridControl2.DataSource = null;
                gridView7.Columns.Clear();
                gridControl2.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                if (gridView7.Columns["GOPER.CODSTATUS"] != null)
                {
                    carregaImagemStatus();
                }

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { "MOVIMENTACAO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { "MOVIMENTACAO", AppLib.Context.Usuario });
                for (int i = 0; i < gridView7.Columns.Count; i++)
                {
                    gridView7.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridView7.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridView7.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregaImagemStatus()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl2.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSTATUS FROM GSTATUS WHERE TABELA = 'GOPER'");
            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
                imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[i]["DESCRICAO"].ToString(), dtImagem.Rows[i]["CODSTATUS"].ToString(), i));
            }
            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView7.Columns["GOPER.CODSTATUS"].ColumnEdit = imageCombo;
        }

        private void btnAgruparMovProd_Click(object sender, EventArgs e)
        {
            if (gridView7.OptionsView.ShowGroupPanel == true)
            {
                gridView7.OptionsView.ShowGroupPanel = false;
                gridView7.ClearGrouping();
                btnAgruparMovProd.Text = "Agrupar";
            }
            else
            {
                gridView7.OptionsView.ShowGroupPanel = true;
                btnAgruparMovProd.Text = "Desagrupar";
            }
        }

        private void BtnPesquisarMovProd_Click(object sender, EventArgs e)
        {
            if (gridView7.OptionsFind.AlwaysVisible == true)
            {
                gridView7.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView7.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnAualizarMovProd_Click(object sender, EventArgs e)
        {
            CarregaGridMovimentacaoProduto(query);
        }

        private void btnSelecionarColunasMovProd_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas("MOVIMENTACAO");
            frm.ShowDialog();
            CarregaGridMovimentacaoProduto(query);
        }

        private void btnSalvarLayoutMovProd_Click(object sender, EventArgs e)
        {
            tabela = "MOVIMENTACAO";
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });

            for (int i = 0; i < gridView7.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", tabela);
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView7.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView7.VisibleColumns[i].Width);
                GVISAOUSUARIO.Set("VISIVEL", 1);
                GVISAOUSUARIO.Save();
            }

            DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ? AND FIXO = 1", new object[] { AppLib.Context.Usuario, tabela });
            if (dtFixo.Rows.Count > 0)
            {
                for (int i = 0; i < dtFixo.Rows.Count; i++)
                {
                    AppLib.ORM.Jit FIXO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                    FIXO.Set("VISAO", tabela);
                    FIXO.Set("CODUSUARIO", AppLib.Context.Usuario);
                    FIXO.Set("COLUNA", dtFixo.Rows[i]["COLUNA"]);
                    FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
                tabela = "VPRODUTO";
            }
        }
        #endregion

        #region Clientes e Fornecdores

        private void btnFilrosProdCliFor_Click(object sender, EventArgs e)
        {
            PS.Glb.New.Filtro.frmFiltroProdCliFor clifor = new Filtro.frmFiltroProdCliFor();
            clifor.Codproduto = codProduto;
            clifor.aberto = true;
            clifor.ShowDialog();
            if (this.lookup == null)
            {
                query2 = clifor.condicao;
                CarregaGridClientesFornecedores(query2);

                btnAgruparProdCliFor.Enabled = true;
                btnPesquisarProdCliFor.Enabled = true;
                btnVisaoProdCliFor.Enabled = true;
            }
            else
            {
                query2 = clifor.condicao;
                CarregaGridClientesFornecedores(query2);
            }
        }

        public void CarregaGridClientesFornecedores(string where)
        {
            string relacionamento2 = "LEFT OUTER JOIN GCIDADE ON GCIDADE.CODETD = VCLIFOR.CODETD AND GCIDADE.CODCIDADE = VCLIFOR.CODCIDADE";
            List<string> tabelasFilhas = new List<string>();
            tabelasFilhas.Add("GCIDADE");

            try
            {
                string sql3 = string.Empty;
                sql3 = new Class.Utilidades().getVisao(tabela2, relacionamento2, tabelasFilhas, query2);

                //sql3.Replace("MOVIMENTACAO", "VCLIFOR");
                if (string.IsNullOrEmpty(sql3))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridControl3.DataSource = null;
                gridView8.Columns.Clear();
                gridControl3.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql3);
                carregaImagemClassificacao();

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela2 });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela2, AppLib.Context.Usuario });
                for (int i = 0; i < gridView8.Columns.Count; i++)
                {
                    gridView8.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result1 = dic.Rows.Find(new object[] { gridView8.Columns[i].FieldName.ToString() });
                    if (result1 != null)
                    {
                        gridView8.Columns[i].Caption = result1["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregaImagemClassificacao()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl3.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();

            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSTATUS FROM GSTATUS WHERE TABELA = ?", new object[] { tabela2 });

            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
                imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[i]["DESCRICAO"].ToString(), Convert.ToInt32(dtImagem.Rows[i]["CODSTATUS"]), i));
            }

            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView8.Columns["VCLIFOR.CODCLASSIFICACAO"].ColumnEdit = imageCombo;

        }

        private void btnAgruparProdCliFor_Click(object sender, EventArgs e)
        {
            if (gridView8.OptionsView.ShowGroupPanel == true)
            {
                gridView8.OptionsView.ShowGroupPanel = false;
                gridView8.ClearGrouping();
                btnAgruparProdCliFor.Text = "Agrupar";
            }
            else
            {
                gridView8.OptionsView.ShowGroupPanel = true;
                btnAgruparProdCliFor.Text = "Desagrupar";
            }
        }

        private void btnPesquisarProdCliFor_Click(object sender, EventArgs e)
        {
            if (gridView8.OptionsFind.AlwaysVisible == true)
            {
                gridView8.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView8.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnAtualizarProdCliFor_Click(object sender, EventArgs e)
        {
            CarregaGridClientesFornecedores(query2);
        }

        private void btnSelecionarColunasProdCliFor_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela2);
            frm.ShowDialog();
            CarregaGridClientesFornecedores(query2);
        }

        private void btnSalvarLayoutProdCliFor_Click(object sender, EventArgs e)
        {
            tabela2 = "MOVIMENTACAO";
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela2 });

            for (int i = 0; i < gridView8.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", tabela2);
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView8.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView8.VisibleColumns[i].Width);
                GVISAOUSUARIO.Set("VISIVEL", 1);
                GVISAOUSUARIO.Save();
            }

            DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ? AND FIXO = 1", new object[] { AppLib.Context.Usuario, tabela2 });
            if (dtFixo.Rows.Count > 0)
            {
                for (int i = 0; i < dtFixo.Rows.Count; i++)
                {
                    AppLib.ORM.Jit FIXO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                    FIXO.Set("VISAO", tabela2);
                    FIXO.Set("CODUSUARIO", AppLib.Context.Usuario);
                    FIXO.Set("COLUNA", dtFixo.Rows[i]["COLUNA"]);
                    FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
                tabela2 = "VCLIFOR";
            }
        }

        #endregion

        private void chkUsaEstoqueMinimo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUsaEstoqueMinimo.Checked == true)
            {
                txtEstoqeuMinimo.Enabled = false;
            }
            else
            {
                txtEstoqeuMinimo.Enabled = true;
            }
        }

        private void toolStrip8_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas("VPRODUTOCLIFOR");
            frm.ShowDialog();
            CarregaGridVPRODUTOCLIFOR("VPRODUTOCLIFOR", "WHERE VPRODUTOCLIFOR.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND VPRODUTOCLIFOR.CODPRODUTO = '" + codProduto + "'");
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            string Tabela = "VPRODUTOCLIFOR";

            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, Tabela });

            for (int i = 0; i < gridView5.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", Tabela);
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView5.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView5.VisibleColumns[i].Width);
                GVISAOUSUARIO.Set("VISIVEL", 1);
                GVISAOUSUARIO.Save();
            }

            DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ? AND FIXO = 1", new object[] { AppLib.Context.Usuario, Tabela });
            if (dtFixo.Rows.Count > 0)
            {
                for (int i = 0; i < dtFixo.Rows.Count; i++)
                {
                    AppLib.ORM.Jit FIXO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                    FIXO.Set("VISAO", tabela);
                    FIXO.Set("CODUSUARIO", AppLib.Context.Usuario);
                    FIXO.Set("COLUNA", dtFixo.Rows[i]["COLUNA"]);
                    FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
                CarregaGridVPRODUTOCLIFOR("VPRODUTOCLIFOR", "WHERE VPRODUTOCLIFOR.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND VPRODUTOCLIFOR.CODPRODUTO = '" + codProduto + "'");
            }
        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            CarregaGridVPRODUTOCLIFOR("VPRODUTOCLIFOR", "WHERE VPRODUTOCLIFOR.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND VPRODUTOCLIFOR.CODPRODUTO = '" + codProduto + "'");
        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {
            string reportPath = string.Empty;
            FolderBrowserDialog open = new FolderBrowserDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                reportPath = open.SelectedPath + "Operações.xlsx";
                DevExpress.XtraPrinting.XlsExportOptions xlsxOptions = new DevExpress.XtraPrinting.XlsExportOptions();
                xlsxOptions.ShowGridLines = true;
                xlsxOptions.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;
                xlsxOptions.ExportHyperlinks = true;
                xlsxOptions.SheetName = "Operações";
                gridView5.ExportToXlsx(reportPath);
                StartProcess(reportPath);
            }
        }

        private void StartProcess(string path)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            try
            {
                process.StartInfo.FileName = path;
                process.Start();
                process.WaitForInputIdle();
            }
            catch { }
        }

        private void regraDeCFOPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lpNCM.txtcodigo.Text))
            {
                PS.Glb.New.Visao.frmVisaoRegraCFOP Regra = new Visao.frmVisaoRegraCFOP("WHERE VREGRAVARCFOP.NCM = '" + lpNCM.txtcodigo.Text + "'");
                Regra.NCM = lpNCM.txtcodigo.Text;
                Regra.ShowDialog();
            }
            else
            {
                MessageBox.Show("Favor informar o NCM no cadastro de produto.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

                //PS.Glb.New.Visao.frmVisaoRegraCFOP Regra = new Visao.frmVisaoRegraCFOP("WHERE NCM < '0'");
                //Regra.NCM = lpNCM.txtcodigo.Text;
                //Regra.ShowDialog();
            }
        }
    }
}
