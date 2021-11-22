using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PS.Glb.New.Visao
{
    public partial class frmVisaoOperacao : Form
    {
        string tabela = "GOPER";
        string query = string.Empty;
        string codTipOper = string.Empty;
        string codMenu = string.Empty;
        private bool permiteEditar = true;
        public int codFilial;
        public bool DocumentoBaixaodo = false;

        // Variáveis para o envio da NF-e
        public string xml = string.Empty;

        // Variáveis para a validação do Envio e Consulta da Nf-e
        private bool StatusEnvioNfe = false;
        private bool StatusConsultaNfe = false;

        private bool selecionou = false;

        private decimal ValorTotal = 0;

        int contador = 0;
        public frmVisaoOperacao(string _query, Form frmprin, string _codTipOper, string _codMenu)
        {
            InitializeComponent();
            codMenu = _codMenu;
            this.MdiParent = (Form)frmprin;
            codTipOper = _codTipOper;
            query = _query;
            carregaGrid(query);
            DataTable dtBuscaGtipOper = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODTIPOPER + ' - ' + DESCRICAO DESCRICAO, USABAIXADEPRODUCAO, GERAFINANCEIRO, USAAPROVACAO  FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper });
            this.Text = dtBuscaGtipOper.Rows[0]["DESCRICAO"].ToString();
            //Verifica se mostra ou não os menus
            if (Convert.ToBoolean(dtBuscaGtipOper.Rows[0]["USABAIXADEPRODUCAO"]) == false)
            {
                btnConcluirOrdemProducao.Visible = false;
            }
            if (Convert.ToBoolean(dtBuscaGtipOper.Rows[0]["GERAFINANCEIRO"]) == false)
            {
                gerarBoletoToolStripMenuItem.Visible = false;
            }
            if (Convert.ToBoolean(dtBuscaGtipOper.Rows[0]["USAAPROVACAO"]) == false)
            {
                btnAprovaOperacao.Visible = false;
                btnReprovaOperacao.Visible = false;
            }
            verificaParametro();
            getAcesso(codMenu);
            verificaAcesso();
        }

        private void getAcesso(string CodMenu)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO, EXCLUSAO, CONSULTA, INCLUSAO FROM GPERMISSAOMENU WHERE CODMENU = ? AND CODPERFIL = ?", new object[] { CodMenu, AppLib.Context.Perfil });
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dt.Rows[i]["EDICAO"]) == false)
                    {
                        btnEditar.Enabled = false;
                        permiteEditar = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["CONSULTA"]) == true)
                    {
                        btnEditar.Enabled = true;
                        permiteEditar = true;
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["EXCLUSAO"]) == false)
                    {
                        btnExcluir.Enabled = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["INCLUSAO"]) == false)
                    {
                        btnNovo.Enabled = false;
                    }
                }
            }
            else
            {
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                btnNovo.Enabled = false;
            }
        }

        private void verificaAcesso()
        {
            DataTable dtAcesso = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT *  FROM GPERFILTIPOPER INNER JOIN GUSUARIOPERFIL ON GPERFILTIPOPER.CODPERFIL = GUSUARIOPERFIL.CODPERFIL WHERE GPERFILTIPOPER.CODEMPRESA = ? AND GPERFILTIPOPER.CODTIPOPER = ? AND GUSUARIOPERFIL.CODUSUARIO = ?", new object[] { AppLib.Context.Empresa, codTipOper, AppLib.Context.Usuario });
            if (dtAcesso.Rows.Count > 0)
            {
                for (int i = 0; i < dtAcesso.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dtAcesso.Rows[i]["FATURAR"]) == false)
                    {
                        btnFaturarOperacao.Enabled = false;
                    }
                    if (Convert.ToBoolean(dtAcesso.Rows[i]["CANCELAR"]) == false)
                    {
                        btnCancelarOperacao.Enabled = false;
                    }
                    if (Convert.ToBoolean(dtAcesso.Rows[i]["CONCLUIR"]) == false)
                    {
                        btnConcluirOperacao.Enabled = false;
                    }
                    if (Convert.ToBoolean(dtAcesso.Rows[i]["APROVADESCONTO"]) == false)
                    {
                        btnAprovaDesconto.Enabled = false;
                    }
                    if (Convert.ToBoolean(dtAcesso.Rows[i]["APROVALIMITECREDITO"]) == false)
                    {
                        btnAprovaLimiteCredito.Enabled = false;
                    }
                    if (Convert.ToBoolean(dtAcesso.Rows[i]["APROVAR"]) == false)
                    {
                        btnAprovaOperacao.Enabled = false;
                    }
                    if (Convert.ToBoolean(dtAcesso.Rows[i]["REPROVAR"]) == false)
                    {
                        btnReprovaOperacao.Enabled = false;
                    }
                    if (Convert.ToBoolean(dtAcesso.Rows[i]["GERARBOLETO"]) == false)
                    {
                        btnGerarBoleto.Enabled = false;
                    }

                    if (Convert.ToBoolean(dtAcesso.Rows[i]["INCLUIR"]) == false)
                    {
                        btnNovo.Enabled = false;
                        btnCopiaOperacao.Enabled = false;
                    }
                }
            }
        }

        private void verificaParametro()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codTipOper, AppLib.Context.Empresa });
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                if (row["GERAFINANCEIRO"].ToString() == "0")
                {
                    btnAjustarValorFinanceiro.Enabled = false;
                }
                if (row["USAOPERACAONFE"].ToString() == "0")
                {
                    btnGerarNfe.Enabled = false;
                    btnCartaCorrecao.Enabled = false;
                }
                if (row["ULTIMONIVEL"].ToString() == "1")
                {
                    btnFaturarOperacao.Enabled = false;
                }
                if (Convert.ToBoolean(row["UTILIZACOPIAREFERENCIA"]) == false)
                {
                    btnCopiaReferencia.Enabled = false;
                }
            }
        }

        #region Visão

        public void carregaGrid(string where)
        {

            string relacionamento = @"INNER JOIN VCLIFOR ON GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR AND GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA
LEFT OUTER JOIN GCIDADE ON VCLIFOR.CODCIDADE = GCIDADE.CODCIDADE
LEFT OUTER JOIN GNFESTADUAL ON GOPER.CODOPER = GNFESTADUAL.CODOPER AND GOPER.CODEMPRESA = GNFESTADUAL.CODEMPRESA 
LEFT OUTER JOIN VVENDEDOR ON GOPER.CODEMPRESA = VVENDEDOR.CODEMPRESA AND GOPER.CODVENDEDOR = VVENDEDOR.CODVENDEDOR
LEFT OUTER JOIN VREPRE ON GOPER.CODEMPRESA = VREPRE.CODEMPRESA AND GOPER.CODREPRE = VREPRE.CODREPRE
LEFT OUTER JOIN VOPERADOR ON GOPER.CODEMPRESA = VOPERADOR.CODEMPRESA AND GOPER.CODOPERADOR = VOPERADOR.CODOPERADOR";
            List<string> tabelasFilhas = new List<string>();
            tabelasFilhas.Add("VCLIFOR");
            tabelasFilhas.Add("GNFESTADUAL");
            tabelasFilhas.Add("VVENDEDOR");
            tabelasFilhas.Add("VREPRE");
            tabelasFilhas.Add("VOPERADOR");
            tabelasFilhas.Add("GCIDADE");
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

                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);
                if (gridView1.Columns["GOPER.CODSTATUS"] != null)
                {
                    carregaImagemStatus();
                }
                if (gridView1.Columns["GOPER.CODSITUACAO"] != null)
                {
                    carregaImagemSituacao();
                }
                if (AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT USAOPERACAONFE FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codTipOper, AppLib.Context.Empresa }).ToString() == "1")
                {
                    if (gridView1.Columns.Contains(gridView1.Columns["GNFESTADUAL.CODSTATUS"]))
                    {
                        carregaStatusNFE();
                    }
                }
                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
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

                // Valor total
                gridView1.Columns["GOPER.VALORLIQUIDO"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                gridView1.Columns["GOPER.VALORLIQUIDO"].SummaryItem.Tag = "VALORTOTAL";

                // Contador de registros selecionados 
                gridView1.Columns["GOPER.CODSTATUS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                gridView1.Columns["GOPER.VALORLIQUIDO"].SummaryItem.Tag = "STATUS";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregaImagemStatus()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
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
            gridView1.Columns["GOPER.CODSTATUS"].ColumnEdit = imageCombo;
        }

        private void carregaStatusNFE()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSTATUS FROM GSTATUS WHERE TABELA = 'GNFESTADUAL'");
            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
                imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[i]["DESCRICAO"].ToString(), dtImagem.Rows[i]["CODSTATUS"].ToString(), i));
            }
            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns["GNFESTADUAL.CODSTATUS"].ColumnEdit = imageCombo;
        }

        private void carregaImagemSituacao()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSITUACAO FROM GSITUACAO WHERE TABELA = 'GOPER'");
            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
                imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[i]["DESCRICAO"].ToString(), dtImagem.Rows[i]["CODSITUACAO"].ToString(), i));
            }
            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns["GOPER.CODSITUACAO"].ColumnEdit = imageCombo;
        }

        #endregion

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            carregaGrid(query);
        }

        private void btnSalvarLayout_Click(object sender, EventArgs e)
        {
            salvarLayout();
        }

        private void salvarLayout()
        {
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
                    //FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
                carregaGrid(query);
            }

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
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

        private void btnAgrupar_Click(object sender, EventArgs e)
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

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            Glb.New.Filtro.frmFiltroOperacao frm = new Filtro.frmFiltroOperacao();
            frm.aberto = true;
            frm.tipOPer = codTipOper;
            frm.codFilial = codFilial;
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.condicao))
            {
                query = frm.condicao;
                carregaGrid(query);
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            editar();
        }

        private void editar()
        {
            if (permiteEditar == true)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    PSPartOperacaoEdit frm = new PSPartOperacaoEdit();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    if (row1 != null)
                    {
                        frm.codoper = Convert.ToInt32(row1["GOPER.CODOPER"]);
                        frm.CodoperOrigem = row1["GOPER.CODOPER"].ToString();
                        frm.edita = true;
                        frm.codMenu = codMenu;
                        frm.codFilial = codFilial;
                        frm.usaAprovacao = Convert.ToBoolean(row1["GOPER.APROVACAO"]);
                        frm.DataEmissao = Convert.ToDateTime(row1["GOPER.DATAEMISSAO"]);
                        frm.ShowDialog();
                        atualizaColuna(row1);
                    }
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            editar();
        }

        private void atualizaColuna(DataRow dr)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPER WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { dr["GOPER.CODOPER"], AppLib.Context.Empresa });

            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                try
                {
                    if (dr[gridView1.Columns[i].FieldName].ToString() == dr["VCLIFOR.NOMEFANTASIA"].ToString())
                    {
                        dr[gridView1.Columns[i].FieldName] = dr[gridView1.Columns[i].FieldName];
                    }
                    else
                    {
                        dr[gridView1.Columns[i].FieldName] = dt.Rows[0][gridView1.Columns[i].FieldName.ToString().Remove(0, gridView1.Columns[i].FieldName.ToString().IndexOf(".") + 1)].ToString();
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            PSPartOperacaoEdit frm = new PSPartOperacaoEdit();
            frm.codFilial = codFilial;
            frm.codtipoper = codTipOper;
            frm.edita = false;
            frm.ShowDialog();
            carregaGrid(query);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
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
                gridView1.ExportToXlsx(reportPath);
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

        private void btnFaturarOperacao_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                //DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                //New.Processos.frmTransferirOperacao frm = new New.Processos.frmTransferirOperacao(row1["CODOPER"].ToString());
                List<string> m_codOper = new List<string>();

                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                    if (row1["GOPER.CODSITUACAO"].ToString() != "7")
                    {
                        if (row1["GOPER.CODSTATUS"].ToString() == "0" || row1["GOPER.CODSTATUS"].ToString() == "5" || row1["GOPER.CODSTATUS"].ToString() == "10")
                        {
                            #region Validações
                            //Validações
                            /*
                             * Fábio Campos 20/04/2016
                             * ************************************************************************************************
                             */
                            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
SELECT VCLIFOR.CODCLIFOR, GTIPOPER.TIPOPAGREC FROM GOPER 
INNER JOIN GTIPOPER ON GOPER.CODEMPRESA = GTIPOPER.CODEMPRESA AND GOPER.CODTIPOPER = GTIPOPER.CODTIPOPER
INNER JOIN VCLIFOR ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR
WHERE 
GOPER.CODOPER = ?
AND GOPER.CODEMPRESA = ?", new object[] { row1["GOPER.CODOPER"].ToString(), AppLib.Context.Empresa });
                            if (dt.Rows.Count > 0)
                            {
                                string CODCLIFOR = dt.Rows[0]["CODCLIFOR"].ToString();
                                string TIPOPAGREC = dt.Rows[0]["TIPOPAGREC"].ToString();
                                string TIPOPAGREC_VERIFICA = string.Empty;
                                string CODCLIFOR_VERIFICA = string.Empty;
                                bool USAAPROVACAO = false;
                                //verifica se o cliente é o mesmo para poder gerar a fatura.
                                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                                DataTable dtGrid = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
SELECT VCLIFOR.CODCLIFOR, GTIPOPER.TIPOPAGREC, GTIPOPER.USAAPROVACAO FROM GOPER 
INNER JOIN GTIPOPER ON GOPER.CODEMPRESA = GTIPOPER.CODEMPRESA AND GOPER.CODTIPOPER = GTIPOPER.CODTIPOPER
INNER JOIN VCLIFOR ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR
WHERE 
GOPER.CODOPER = ?
AND GOPER.CODEMPRESA = ?", new object[] { row["GOPER.CODOPER"].ToString(), AppLib.Context.Empresa });

                                CODCLIFOR_VERIFICA = dtGrid.Rows[0]["CODCLIFOR"].ToString();
                                TIPOPAGREC_VERIFICA = dtGrid.Rows[0]["TIPOPAGREC"].ToString();
                                USAAPROVACAO = Convert.ToBoolean(dtGrid.Rows[0]["USAAPROVACAO"]);
                                if (!CODCLIFOR.Equals(CODCLIFOR_VERIFICA))
                                {
                                    MessageBox.Show("Permitido apenas para lançamentos do mesmo cliente/fornecedor.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                //Verifica se os lançamentos estão misturados.
                                if (!TIPOPAGREC.Equals(TIPOPAGREC_VERIFICA))
                                {
                                    MessageBox.Show("Não pode misturar lançamentos de PAGAR/RECEBER ao transferir.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                //Fábio Campos 22/09/2017 15:02
                                //Verficação da GTIPOPER se utiliza aprovação
                                if (USAAPROVACAO == true)
                                {
                                    if (Convert.ToBoolean(row["GOPER.APROVACAO"]) == false)
                                    {
                                        MessageBox.Show("Transferência não pode ser realizada.\nOperação não aprovada", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                            }
                            //**************************************************************************************************
                            #endregion

                            m_codOper.Add(row1["GOPER.CODOPER"].ToString());
                        }
                        else
                        {
                            //Status
                            MessageBox.Show("Não é permitido transferir operação bloqueada.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        //Situacao
                        MessageBox.Show("Não é permitido transferir operação com limite de crédito.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (m_codOper.Count > 0)
                {
                    New.Processos.frmTransferirOperacao frm = new New.Processos.frmTransferirOperacao(m_codOper, codFilial);
                    frm.codMenu = codMenu;
                    frm.ShowDialog();
                }

                carregaGrid(query);
            }
            else
            {
                MessageBox.Show("Favor selecionar o registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnCopiaOperacao_Click(object sender, EventArgs e)
        {
            // Variável que define que o botão Cancelar do fomrulário de Operações será inativo.
            bool Cancela = false;

            if (gridView1.SelectedRowsCount > 0)
            {
                if (MessageBox.Show("Deseja copiar a operação?", "Informação do Sistema.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    try
                    {
                        if (gridView1.SelectedRowsCount > 1)
                        {
                            MessageBox.Show("Favor selecionar apenas 1(um) registro.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                        {
                            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                            if (row1 != null)
                            {
                                splashScreenManager1.ShowWaitForm();
                                splashScreenManager1.SetWaitFormCaption("Copiando Operação...");
                                string newCodOper = new Class.CopiaOperacao().CopiarOperacao(row1["GOPER.CODOPER"].ToString(), codFilial);

                                splashScreenManager1.CloseWaitForm();

                                if (!string.IsNullOrEmpty(newCodOper))
                                {
                                    if (gridView1.SelectedRowsCount > 0)
                                    {
                                        PSPartOperacaoEdit frm = new PSPartOperacaoEdit();
                                        frm.codFilial = codFilial;
                                        frm.codoper = Convert.ToInt32(newCodOper);
                                        frm.edita = true;
                                        frm.faturamento = true;
                                        frm.codMenu = codMenu;
                                        frm.btnFechar.Enabled = false;
                                        frm.VerificaCancela = Cancela;
                                        frm.CopiaOperacao = true;
                                        frm.NumeroOPer = Convert.ToInt32(row1["GOPER.NUMERO"]);
                                        frm.SerieOper = row1["GOPER.CODSERIE"].ToString();
                                        //string CodTipOper = Class.CopiaOperacao.getCodTipoper(Convert.ToInt32(newCodOper));
                                        //frm.geraTributo(Convert.ToInt32(newCodOper), CodTipOper, codFilial);
                                        frm.ShowDialog();
                                        carregaGrid(query);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Não foi possível concluir a cópia da operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Favor selecionar o registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAjustarValorFinanceiro_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                PSPartAjustarValorFinanceiroAppFrm frm = new PSPartAjustarValorFinanceiroAppFrm();
                frm.psPartApp = new Lib.WinForms.PSPartApp();

                frm.psPartApp.AppName = "Ajustar Valor Financeiro";
                frm.psPartApp.FormApp = new PSPartAjustarValorFinanceiroAppFrm();
                frm.psPartApp.Select = PS.Lib.SelectType.OnlyOneRow;
                frm.psPartApp.Refresh = true;
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                frm.codOper = row1["GOPER.CODOPER"].ToString();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Favor selecionar o registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            carregaGrid(query);
        }

        private void btnLancamentoFinanceiro_Click(object sender, EventArgs e)
        {
            carregaLancamentoFinanceiro();
        }

        public void carregaGrid(string _tabela, DevExpress.XtraGrid.GridControl grid, DevExpress.XtraGrid.Views.Grid.GridView view, string where, string relacionamento, List<string> tabelasFilhas)
        {
            try
            {
                List<string> TabelasFilhas = new List<string>();
                TabelasFilhas.Add("GOPER");

                string sql = new Class.Utilidades().getVisao(_tabela, relacionamento, TabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                grid.DataSource = null;
                view.Columns.Clear();
                grid.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { _tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { _tabela, AppLib.Context.Usuario });
                for (int i = 0; i < view.Columns.Count; i++)
                {
                    view.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { view.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        view.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFecharAnexo_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = true;

            if (tabEventosNfe.PageVisible == true)
            {
                tabEventosNfe.PageVisible = false;
            }
            if (tabItens.PageVisible == true)
            {
                tabItens.PageVisible = false;
            }
            if (tabLancamento.PageVisible == true)
            {
                tabLancamento.PageVisible = false;
            }
            if (tabMotivoConclusao.PageVisible == true)
            {
                tabMotivoConclusao.PageVisible = false;
            }
            if (tabNFe.PageVisible == true)
            {
                tabNFe.PageVisible = false;
            }
            if (tabPageHistorico.PageVisible == true)
            {
                tabPageHistorico.PageVisible = false;
            }
        }

        private void btnItens_Click(object sender, EventArgs e)
        {
            carregaItens();
        }

        private void carregaLancamentoFinanceiro()
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                string relacionamento = " INNER JOIN VCLIFOR ON FLANCA.CODCLIFOR = VCLIFOR.CODCLIFOR AND FLANCA.CODEMPRESA = VCLIFOR.CODEMPRESA";
                List<string> tabelasFilhas = new List<string>();

                tabLancamento.PageVisible = true;
                splitContainer2.Panel2Collapsed = false;
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                carregaGrid("FLANCA", gridControl2, gridView2, "WHERE CODOPER = " + Convert.ToInt32(row1["GOPER.CODOPER"]) + " AND FLANCA.CODEMPRESA = " + AppLib.Context.Empresa, relacionamento, tabelasFilhas);
            }
        }

        private void carregaNFE()
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                string relacionamento = string.Empty;
                List<string> tabelasFilhas = new List<string>();

                tabNFe.PageVisible = true;
                splitContainer2.Panel2Collapsed = false;
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                //  carregaGrid("GNFESTADUAL", gridControl3, gridView3, "WHERE CODOPER = " + Convert.ToInt32(row1["GOPER.CODOPER"]) + " AND CODEMPRESA = " + AppLib.Context.Empresa);
                carregaGrid("GNFESTADUAL", gridControl3, gridView3, "WHERE GNFESTADUAL.CODOPER = " + Convert.ToInt32(row1["GOPER.CODOPER"]) + " AND GNFESTADUAL.CODEMPRESA = " + AppLib.Context.Empresa, relacionamento, tabelasFilhas);
            }
        }

        private void carregaItens()
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                string relacionamento = @"INNER JOIN VPRODUTO ON GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA AND GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO";
                List<string> tabelasFilhas = new List<string>();
                tabelasFilhas.Add("VPRODUTO");

                tabItens.PageVisible = true;
                splitContainer2.Panel2Collapsed = false;
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                carregaGrid("GOPERITEM", gridControl4, gridView4, "WHERE GOPERITEM.CODOPER = " + Convert.ToInt32(row1["GOPER.CODOPER"]) + " AND GOPERITEM.CODEMPRESA = " + AppLib.Context.Empresa, relacionamento, tabelasFilhas);
            }
        }

        private void históricoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CarregaHistorico();
        }

        private void CarregaHistorico()
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                tabPageHistorico.PageVisible = true;
                splitContainer2.Panel2Collapsed = false;

                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                DataTable dtHistorico = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODEMPRESA AS 'Código da Empresa', CODOPER AS 'Código da Operação', IDHISTORICO AS 'Id Histórico', DATA AS 'Data', CODUSUARIO AS 'Usuário', OBSERVACAO AS 'Observação' FROM GNFESTADUALHISTORICO WHERE CODOPER = ?", new object[] { Convert.ToInt32(row1["GOPER.CODOPER"]) });

                gridControl8.DataSource = dtHistorico;
                gridView8.BestFitColumns();
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                return;
            }

            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            if (row1["GNFESTADUAL.CODSTATUS"].ToString() == "E" || row1["GNFESTADUAL.CODSTATUS"].ToString() == "I")
            {
                históricoToolStripMenuItem.Visible = true;

                if (splitContainer2.Panel2Collapsed == false)
                {
                    if (tabPageHistorico.PageVisible == true)
                    {
                        CarregaHistorico();
                    }
                }
            }
            else
            {
                if (splitContainer2.Panel2Collapsed == false)
                {
                    if (tabLancamento.PageVisible == true)
                    {
                        carregaLancamentoFinanceiro();
                    }
                    if (tabItens.PageVisible == true)
                    {
                        carregaItens();
                    }
                    if (tabNFe.PageVisible == true)
                    {
                        carregaNFE();
                    }
                    if (tabEventosNfe.PageVisible == true)
                    {
                        btnEventos_Click(sender, e);
                    }
                    if (tabPageHistorico.PageVisible == true)
                    {
                        splitContainer2.Panel2Collapsed = true;
                        tabPageHistorico.PageVisible = false;
                        históricoToolStripMenuItem.Visible = false;
                        return;
                    }
                }
                históricoToolStripMenuItem.Visible = false;

                if (tabPageHistorico.PageVisible == true)
                {
                    splitContainer2.Panel2Collapsed = true;
                    tabPageHistorico.PageVisible = false;
                }
            }
        }

        private void btnImprimirOperacao_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                getIdReport();
            }
            else
            {
                MessageBox.Show("Nenhum registro selecionado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void getIdReport()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
SELECT 
GTIPOPERREPORT2.IDREPORT,
ZREPORT.NOME AS 'NOME'

FROM 
GTIPOPERREPORT2
INNER JOIN ZREPORT ON ZREPORT.IDREPORT = GTIPOPERREPORT2.IDREPORT
INNER JOIN ZREPORTPERFIL ON ZREPORTPERFIL.IDREPORT = ZREPORT.IDREPORT
INNER JOIN ZPERFIL ON ZPERFIL.CODPERFIL = ZREPORTPERFIL.CODPERFIL
INNER JOIN ZUSUARIO ON ZUSUARIO.CODPERFIL = ZPERFIL.CODPERFIL

WHERE 
	GTIPOPERREPORT2.CODEMPRESA = ?
AND GTIPOPERREPORT2.CODTIPOPER = ?
AND ZUSUARIO.USUARIO = ?
AND ZREPORT.ATIVO = 1
", new Object[] { AppLib.Context.Empresa, codTipOper, AppLib.Context.Usuario });

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Não existe relatório parametrizado para este tipo de operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dt.Rows.Count == 1)
            {
                int IDREPORT = Convert.ToInt32(dt.Rows[0]["IDREPORT"]);
                this.ImprimirOperacao(IDREPORT);
            }

            if (dt.Rows.Count > 1)
            {
                AppLib.Windows.FormListaPrompt fLista = new AppLib.Windows.FormListaPrompt();
                fLista.PrimeiroItemNulo = false;
                Object result = fLista.Mostrar("Selecione o relatório:", dt);

                if (fLista.confirmacao == AppLib.Global.Types.Confirmacao.OK)
                {
                    int IDREPORT = Convert.ToInt32(result);
                    this.ImprimirOperacao(IDREPORT);
                }
            }
        }

        public void ImprimirOperacao(int IDREPORT)
        {
            String ListCODOPER = "";

            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                ListCODOPER += Convert.ToInt32(row1["GOPER.CODOPER"]);
                ListCODOPER += ", ";
            }

            ListCODOPER = ListCODOPER.Substring(0, ListCODOPER.Length - 2);

            AppLib.Padrao.FormReportVisao f = new AppLib.Padrao.FormReportVisao();
            f.grid1.Conexao = "Start";
            f.Visualizar(IDREPORT, ListCODOPER);
        }

        private void btnCancelarOperacao_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                //Verifica a permissão da operação.
                bool permite = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT 
            PERMITEALTERAR 
            FROM 
            GACESSOMENU 
            INNER JOIN GUSUARIOPERFIL ON GACESSOMENU.CODPERFIL = GUSUARIOPERFIL.CODPERFIL AND GACESSOMENU.CODEMPRESA = GUSUARIOPERFIL.CODEMPRESA
            WHERE 
            GACESSOMENU.CODPSPART = ?
            AND GUSUARIOPERFIL.CODUSUARIO = ?
            AND GACESSOMENU.CODEMPRESA = ?", new object[] { "PSPartOperacao", AppLib.Context.Usuario, AppLib.Context.Empresa }));
                if (permite == true)
                {
                    //Verifica a permissão do usuário
                    permite = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT 
            CANCELAR 
            FROM 
            GPERFILTIPOPER 
            INNER JOIN GUSUARIOPERFIL ON GPERFILTIPOPER.CODPERFIL = GUSUARIOPERFIL.CODPERFIL AND GPERFILTIPOPER.CODEMPRESA = GUSUARIOPERFIL.CODEMPRESA 
            WHERE 
            GUSUARIOPERFIL.CODUSUARIO = ?
            AND GUSUARIOPERFIL.CODEMPRESA = ? 
            AND GPERFILTIPOPER.CODTIPOPER = ?", new object[] { AppLib.Context.Usuario, AppLib.Context.Empresa, codTipOper }));
                    if (permite == true)
                    {
                        if (MessageBox.Show("Deseja executar o cancelamento?", "Informação do Sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                        {
                            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                            conn.BeginTransaction();
                            try
                            {
                                List<int> m_codOper = new List<int>();
                                List<DateTime> DataOperacao = new List<DateTime>();

                                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                                {
                                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                                    if (!string.IsNullOrEmpty(row1["GNFESTADUAL.CODSTATUS"].ToString()))
                                    {
                                        MessageBox.Show("Não é possível fazer o Cancelamento pois existe Nota Fiscal gerada.\r\nUtilize o processo de cancelamento da NF-e.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                    if (row1["GOPER.CODSTATUS"].ToString() == "0")
                                    {
                                        m_codOper.Add(Convert.ToInt32(row1["GOPER.CODOPER"].ToString()));
                                        DataOperacao.Add(Convert.ToDateTime(row1["GOPER.DATAEMISSAO"]));
                                    }
                                }

                                if (m_codOper.Count > 0)
                                {
                                    PS.Glb.New.Processos.frmCancelaOperacao frm = new New.Processos.frmCancelaOperacao(m_codOper, conn);
                                    frm.ShowDialog();
                                    conn.Commit();
                                    ExcluiSaldoEstoqueManual(conn, m_codOper, DataOperacao);

                                    if (frm.statusExecucao == false)
                                    {
                                        MessageBox.Show("Não foi possível concluir o cancelamento.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        //conn.Rollback();
                                        return;
                                    }

                                    //Exclui Financeiro                                  
                                    if (excluiFinanceiroManual(conn, m_codOper) == false)
                                    {
                                        MessageBox.Show("Não foi possível concluir o cancelamento. Erro na exclusão do Financeiro", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        conn.Rollback();
                                        return;
                                    }
                                }

                                #region Remove os relacionamentos e calcula a operação de origem novamente.

                                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                                {
                                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                                    DataTable dtRelac = conn.ExecQuery("SELECT GOPERITEMRELAC.CODOPERITEMORIGEM, GOPERITEMRELAC.NSEQITEMORIGEM, GOPERITEM.NSEQITEM, GOPERITEM.QUANTIDADE, GOPERITEM.CODEMPRESA, GOPER.CODTIPOPER FROM GOPERITEMRELAC INNER JOIN GOPERITEM ON GOPERITEMRELAC.CODOPERITEMDESTINO = GOPERITEM.CODOPER AND GOPERITEMRELAC.NSEQITEMDESTINO = GOPERITEM.NSEQITEM  INNER JOIN GOPER ON GOPERITEMRELAC.CODOPERITEMORIGEM = GOPER.CODOPER WHERE CODOPERITEMDESTINO = ?", new object[] { row1["GOPER.CODOPER"].ToString(), AppLib.Context.Empresa });

                                    // João Pedro Luchiari 20/10/2017
                                    if (dtRelac.Rows.Count > 0)
                                    {
                                        PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                                        psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
                                        psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };
                                        psPartLocalEstoqueSaldoData.MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.OperacaoCancelamento, AppLib.Context.Empresa, Convert.ToInt32(row1["GOPER.CODOPER"]), 0);
                                        conn.ExecTransaction("DELETE FROM VFICHAESTOQUE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codFilial, row1["GOPER.CODOPER"].ToString() });

                                        for (int iRelac = 0; iRelac < dtRelac.Rows.Count; iRelac++)
                                        {
                                            PSPartOperacaoEdit frmGera = new PSPartOperacaoEdit();
                                            frmGera.codFilial = codFilial;

                                            string aaaa = conn.ParseCommand("UPDATE GOPERITEM SET QUANTIDADE_SALDO = QUANTIDADE_SALDO + " + dtRelac.Rows[iRelac]["QUANTIDADE"].ToString().Replace(",", ".") + ", QUANTIDADE_FATURADO = QUANTIDADE_FATURADO - " + dtRelac.Rows[iRelac]["QUANTIDADE"].ToString().Replace(",", ".") + " WHERE CODOPER = ? AND CODEMPRESA = ? AND NSEQITEM = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"], dtRelac.Rows[iRelac]["CODEMPRESA"], dtRelac.Rows[iRelac]["NSEQITEMORIGEM"] });

                                            conn.ExecTransaction("UPDATE GOPERITEM SET QUANTIDADE_SALDO = QUANTIDADE_SALDO + " + dtRelac.Rows[iRelac]["QUANTIDADE"].ToString().Replace(",", ".") + ", QUANTIDADE_FATURADO = QUANTIDADE_FATURADO - " + dtRelac.Rows[iRelac]["QUANTIDADE"].ToString().Replace(",", ".") + " WHERE CODOPER = ? AND CODEMPRESA = ? AND NSEQITEM = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"], dtRelac.Rows[iRelac]["CODEMPRESA"], dtRelac.Rows[iRelac]["NSEQITEMORIGEM"] });

                                            frmGera.calculaOperacao(Convert.ToInt32(dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"]), dtRelac.Rows[iRelac]["CODTIPOPER"].ToString());

                                            //conn.ExecTransaction("UPDATE GOPER SET CODSTATUS = 0 WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"], AppLib.Context.Empresa });
                                            conn.ExecTransaction("UPDATE GOPER SET GOPER.CODSTATUS = CASE WHEN ((SELECT (SUM(GOPERITEM.QUANTIDADE) - SUM(GOPERITEM.QUANTIDADE_SALDO)) FROM GOPERITEM WHERE GOPERITEM.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEM.CODOPER = GOPER.CODOPER) = 0) THEN '0' ELSE '5' END FROM GOPER WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"], AppLib.Context.Empresa });
                                            conn.ExecTransaction("DELETE FROM GOPERITEMRELAC WHERE CODOPERITEMDESTINO = ?", new object[] { row1["GOPER.CODOPER"].ToString() });
                                            conn.ExecTransaction("DELETE FROM GOPERRELAC WHERE CODOPER = ? AND CODOPERRELAC = ? AND CODEMPRESA =?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"], row1["GOPER.CODOPER"].ToString(), AppLib.Context.Empresa });
                                        }
                                    }
                                    else
                                    {
                                        dtRelac = conn.ExecQuery(@"SELECT GOPERRELAC.CODOPERRELAC, GOPERRELAC.CODEMPRESA, GOPER.CODOPER 
                                                                                FROM GOPERRELAC
                                                                                    INNER JOIN GOPER ON GOPERRELAC.CODOPER = GOPER.CODOPER
                                                                                WHERE GOPERRELAC.CODOPERRELAC = ?
                                                                                AND GOPERRELAC.CODEMPRESA = ?", new object[] { row1["GOPER.CODOPER"].ToString(), AppLib.Context.Empresa });

                                        for (int iRelac = 0; iRelac < dtRelac.Rows.Count; iRelac++)
                                        {
                                            conn.ExecTransaction("UPDATE GOPER SET GOPER.CODSTATUS = 0 FROM GOPER WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { dtRelac.Rows[iRelac]["CODOPER"], AppLib.Context.Empresa });
                                            conn.ExecTransaction("DELETE FROM GOPERITEMRELAC WHERE CODOPERITEMDESTINO = ?", new object[] { row1["GOPER.CODOPER"].ToString() });
                                            conn.ExecTransaction("DELETE FROM GOPERRELAC WHERE CODOPER = ? AND CODOPERRELAC = ? AND CODEMPRESA =?", new object[] { dtRelac.Rows[iRelac]["CODOPER"], dtRelac.Rows[iRelac]["CODOPERRELAC"], AppLib.Context.Empresa });
                                        }
                                    }
                                }
                                #endregion

                                carregaGrid(query);
                                if (tabLancamento.PageVisible == true)
                                {
                                    carregaLancamentoFinanceiro();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Não foi possível concluir o cancelamento.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                conn.Rollback();
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Usuário sem permissão para alteração.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Operação sem permissão para alteração.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Favor selecionar o registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //Verifica a permissão da operação.
            bool permite = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT 
PERMITEEXCLUIR 
FROM 
GACESSOMENU 
INNER JOIN GUSUARIOPERFIL ON GACESSOMENU.CODPERFIL = GUSUARIOPERFIL.CODPERFIL AND GACESSOMENU.CODEMPRESA = GUSUARIOPERFIL.CODEMPRESA
WHERE 
GACESSOMENU.CODPSPART = ?
AND GUSUARIOPERFIL.CODUSUARIO = ?
AND GACESSOMENU.CODEMPRESA = ?", new object[] { "PSPartOperacao", AppLib.Context.Usuario, AppLib.Context.Empresa }));
            if (permite == true)
            {
                //Verifica a permissão do usuário
                permite = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT 
EXCLUIR 
FROM 
GPERFILTIPOPER 
INNER JOIN GUSUARIOPERFIL ON GPERFILTIPOPER.CODPERFIL = GUSUARIOPERFIL.CODPERFIL AND GPERFILTIPOPER.CODEMPRESA = GUSUARIOPERFIL.CODEMPRESA 
WHERE 
GUSUARIOPERFIL.CODUSUARIO = ?
AND GUSUARIOPERFIL.CODEMPRESA = ? 
AND GPERFILTIPOPER.CODTIPOPER = ?", new object[] { AppLib.Context.Usuario, AppLib.Context.Empresa, codTipOper }));
                if (permite == true)
                {
                    if (MessageBox.Show("Deseja executar a exclusão?", "Informação do Sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                    {
                        AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();

                        try
                        {
                            PSPartOperacaoEdit frm = new PSPartOperacaoEdit();
                            frm.codFilial = codFilial;
                            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                            {

                                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                                if (row1["GOPER.CODSTATUS"].ToString() == "0" || row1["GOPER.CODSTATUS"].ToString() == "2")
                                {
                                    DataTable ITENS = conn.ExecQuery("SELECT QUANTIDADE, NSEQITEM FROM GOPERITEM WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { row1["GOPER.CODOPER"].ToString(), AppLib.Context.Empresa });
                                    DataTable dtRelac = conn.ExecQuery("SELECT NSEQITEMORIGEM, CODOPERITEMORIGEM FROM GOPERITEMRELAC WHERE CODOPERITEMDESTINO = ?", new object[] { row1["GOPER.CODOPER"].ToString() });

                                    PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                                    psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
                                    psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };
                                    psPartLocalEstoqueSaldoData.MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.OperacaoExclusao, AppLib.Context.Empresa, Convert.ToInt32(row1["GOPER.CODOPER"]), 0);

                                    conn.BeginTransaction();
                                    if (dtRelac.Rows.Count > 0)
                                    {
                                        for (int iitens = 0; iitens < ITENS.Rows.Count; iitens++)
                                        {
                                            conn.ExecTransaction("UPDATE GOPERITEM SET QUANTIDADE_FATURADO = QUANTIDADE_FATURADO - ?, QUANTIDADE_SALDO = QUANTIDADE_SALDO + ? WHERE CODOPER = ? AND NSEQITEM = ? AND CODEMPRESA = ?", new object[] { ITENS.Rows[iitens]["QUANTIDADE"], ITENS.Rows[iitens]["QUANTIDADE"], dtRelac.Rows[iitens]["CODOPERITEMORIGEM"], dtRelac.Rows[iitens]["NSEQITEMORIGEM"], AppLib.Context.Empresa });
                                        }
                                    }
                                    conn.ExecTransaction("DELETE FROM GOPERITEMRELAC WHERE CODOPERITEMDESTINO = ?", new object[] { row1["GOPER.CODOPER"].ToString() });
                                    conn.ExecTransaction("DELETE FROM GMOTIVOCANCELAMENTO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, row1["GOPER.CODOPER"].ToString() });
                                    conn.ExecTransaction("DELETE FROM GOPERITEMCOPIAREF WHERE CODOPERDESTINO = ? AND CODEMPRESA = ? ", new object[] { row1["GOPER.CODOPER"], AppLib.Context.Empresa });
                                    conn.ExecTransaction("DELETE FROM GOPERCOPIAREF WHERE CODOPERDESTINO = ? AND CODEMPRESA = ? ", new object[] { row1["GOPER.CODOPER"], AppLib.Context.Empresa });
                                    conn.ExecTransaction("DELETE FROM GOPER WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { row1["GOPER.CODOPER"].ToString(), AppLib.Context.Empresa });
                                    string relac = conn.ExecGetField(string.Empty, "SELECT CODOPER FROM GOPERRELAC WHERE CODOPERRELAC = ? AND CODEMPRESA = ?", new object[] { row1["GOPER.CODOPER"].ToString(), AppLib.Context.Empresa }).ToString();
                                    conn.Commit();
                                    MessageBox.Show("Operação excluída com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    if (!string.IsNullOrEmpty(relac))
                                    {
                                        conn.BeginTransaction();

                                        conn.ExecTransaction("UPDATE GOPER SET CODSTATUS = 0 WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { relac, AppLib.Context.Empresa });
                                        conn.ExecTransaction("DELETE FROM GOPERRELAC WHERE CODOPER = ? AND CODOPERRELAC = ? AND CODEMPRESA =?", new object[] { relac, row1["GOPER.CODOPER"].ToString(), AppLib.Context.Empresa });
                                        conn.Commit();

                                        frm.geraFinanceiro(row1["GOPER.CODTIPOPER"].ToString(), Convert.ToInt32(relac));
                                        MessageBox.Show("Operação excluída com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }

                                    #region Verificação para a numeração

                                    string Ultimo = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT MAX(NUMSEQ) 
                                                                                                                            FROM VSERIE
                                                                                                                            WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODSERIE = ? ", new object[] { AppLib.Context.Empresa, row1["GOPER.CODFILIAL"], row1["GOPER.CODSERIE"] }).ToString();

                                    // Se a nota selecionada tiver a mesma numeração do último número 
                                    if (Ultimo == row1["GOPER.NUMERO"].ToString())
                                    {
                                        if (MessageBox.Show("Deseja voltar a numeração?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            int UltimoNumero = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT MAX(NUMSEQ) 
                                                                                                                            FROM VSERIE
                                                                                                                            WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODSERIE = ? ", new object[] { AppLib.Context.Empresa, row1["GOPER.CODFILIAL"], row1["GOPER.CODSERIE"] }));

                                            UltimoNumero--;

                                            string NovoNumero = string.Concat(UltimoNumero.ToString().PadLeft(Ultimo.Length, '0'));

                                            try
                                            {
                                                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE VSERIE 
                                                                                                            SET NUMSEQ = ?
                                                                                                            WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODSERIE = ? ", new object[] { NovoNumero, AppLib.Context.Empresa, row1["GOPER.CODFILIAL"], row1["GOPER.CODSERIE"] });

                                                MessageBox.Show("Numeração atualizada com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            catch (Exception ex)
                                            {
                                                throw ex;
                                            }
                                        }
                                    }

                                    #endregion

                                }
                                else
                                {
                                    MessageBox.Show("Somente operações com Status em (Aberto e Cancelado) podem ser excluídas.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            carregaGrid(query);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Não foi possível concluir a exclusão.\r\nDetalhes: " + ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            conn.Rollback();
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Usuário sem permissão para alteração.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Operação sem permissão para alteração.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnRastreamentoOper_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                New.Processos.Operacao.frmRastreamentoOperacao frm = new New.Processos.Operacao.frmRastreamentoOperacao(Convert.ToInt32(row1["GOPER.CODOPER"]), codTipOper, row1["GOPER.NUMERO"].ToString());
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Favor selecionar o registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnConcluirOperacao_Click(object sender, EventArgs e)
        {
            List<int> codOper = new List<int>();
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                codOper.Add(Convert.ToInt32(row["GOPER.CODOPER"].ToString()));
            }
            if (codOper.Count > 0)
            {
                New.Processos.Operacao.frmConcluirOperacao frm = new Processos.Operacao.frmConcluirOperacao(codOper);
                frm.ShowDialog();
                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    atualizaColuna(row);
                }
            }
            else
            {
                MessageBox.Show("Favor selecionar pelo menos um registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMotivos_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                string relacionamento = string.Empty;
                List<string> tabelasFilhas = new List<string>();

                tabMotivoConclusao.PageVisible = true;
                splitContainer2.Panel2Collapsed = false;
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                //carregaGrid("GOPERMOTIVOCONCLUSAO", gridControl5, gridView5, "WHERE CODOPER = " + Convert.ToInt32(row1["GOPER.CODOPER"]) + " AND CODEMPRESA = " + AppLib.Context.Empresa);
                carregaGrid("GOPERMOTIVOCONCLUSAO", gridControl5, gridView5, "WHERE GOPERMOTIVOCONCLUSAO.CODOPER = " + Convert.ToInt32(row1["GOPER.CODOPER"]) + " AND GOPERMOTIVOCONCLUSAO.CODEMPRESA = " + AppLib.Context.Empresa, relacionamento, tabelasFilhas);
            }
        }

        private void btnGerarNfe_Click(object sender, EventArgs e)
        {
            #region Rotina comentada  

            //            bool Envio = false;

            //            if (gridView1.SelectedRowsCount > 0)
            //            {
            //                if (MessageBox.Show("Deseja enviar a NF-e?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //                {
            //                    splashScreenManager1.ShowWaitForm();
            //                    splashScreenManager1.SetWaitFormCaption("Enviando NF-e");
            //                    try
            //                    {
            //                        PSPartOperacaoData psPartOperacaoData = new PSPartOperacaoData();
            //                        psPartOperacaoData._tablename = "GOPER";
            //                        psPartOperacaoData._keys = new string[] { "CODEMPRESA", "CODOPER" };
            //                        PS.Lib.Global gb = new Lib.Global();
            //                        bool retornoNFE = false;
            //                        string CodStatus = string.Empty;

            //                        if (gridView1.SelectedRowsCount == 1)
            //                        {

            //                            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            //                            if (row1 != null)
            //                            {
            //                                psPartOperacaoData.GeraNFe(gb.RetornaDataFieldByDataRow(row1));
            //                            }
            //                            //MessageBox.Show("NF-e enviando com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //                            xml = ValidateLib.OBJETOS_VALIDATESERVICE.FuncoesAuxiliares.XMLGerado;

            //                            UpdateGNFEstadual(xml, Convert.ToInt32(row1["GOPER.CODOPER"]));

            //                            RotinaEnvioXML(xml);

            //                            // Descomentar caso dê erro

            //                            //splashScreenManager1.SetWaitFormCaption("Consultando Situação");

            //                            //string xml = ValidateLib.OBJETOS_VALIDATESERVICE.FuncoesAuxiliares.XMLGerado;

            //                            //// Consulta Situação
            //                            //while (retornoNFE == false)
            //                            //{
            //                            //    PSPartNFEstadualData nfestadual = new PSPartNFEstadualData();
            //                            //    nfestadual._tablename = "GNFESTADUAL";
            //                            //    nfestadual._keys = new string[] { "CODEMPRESA", "CODOPER" };

            //                            //    //for (int i = 0; i < row1.Table.Columns.Count; i++)
            //                            //    //{
            //                            //    //    row1.Table.Columns[i].ColumnName = row1.Table.Columns[i].ColumnName.Replace("GOPER.", "");
            //                            //    //}

            //                            //    nfestadual.ConsultaSituacaoNFe(gb.RetornaDataFieldByDataRow(row1));

            //                            //    // Consulta GnfEstadual
            //                            //    CodStatus = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODSTATUS FROM GNFESTADUAL WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { row1["GOPER.CODOPER"], row1["GOPER.CODEMPRESA"] }).ToString();
            //                            //    if (!string.IsNullOrEmpty(CodStatus))
            //                            //    {
            //                            //        if (CodStatus != "U")
            //                            //        {
            //                            //            retornoNFE = true;
            //                            //        }
            //                            //    }
            //                            //}

            //                            // 
            //                            splashScreenManager1.CloseWaitForm();

            //                            return;

            //                            // Validar a rotina abaixo com o Jerry

            //                            if (CodStatus == "A")
            //                            {
            //                                // E-mail
            //                                if (MessageBox.Show("Deseja enviar e-mail?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            //                                {
            //                                    splashScreenManager1.ShowWaitForm();
            //                                    splashScreenManager1.SetWaitFormCaption("Enviando E-mail");
            //                                    PSPartNFEstadualData psPartNFEstadualData = new PSPartNFEstadualData();
            //                                    psPartNFEstadualData._tablename = "GNFESTADUAL";
            //                                    psPartNFEstadualData._keys = new string[] { "CODEMPRESA", "CODOPER" };
            //                                    psPartNFEstadualData.EnvioAutomatico = Envio;
            //                                    psPartNFEstadualData.EnviarXML(Convert.ToInt32(row1["GOPER.CODEMPRESA"]), Convert.ToInt32(row1["GOPER.CODOPER"]), true);
            //                                    splashScreenManager1.CloseWaitForm();
            //                                }

            //                                // Imprimir
            //                                if (MessageBox.Show("Deseja imprimir NF-e?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            //                                {
            //                                    string nomeRelatorio = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CLASSNAME FROM GTIPOPERREPORT 
            //INNER JOIN GOPER ON GTIPOPERREPORT.CODTIPOPER = GOPER.CODTIPOPER
            //INNER JOIN GREPORT ON GTIPOPERREPORT.CODREPORT = GREPORT.CODREPORT
            //WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ? ", new object[] { Convert.ToInt32(row1["GOPER.CODOPER"]), Convert.ToInt32(row1["GOPER.CODEMPRESA"]) }).ToString();
            //                                    if (!string.IsNullOrEmpty(nomeRelatorio))
            //                                    {
            //                                        PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByDataRow(row1, "GOPER.CODEMPRESA");
            //                                        PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByDataRow(row1, "GOPER.CODOPER");
            //                                        PS.Lib.DataField dfPROTOCOLO = gb.RetornaDataFieldByDataRow(row1, "GOPER.PROTOCOLO");
            //                                        if (dfPROTOCOLO.Valor == null)
            //                                        {
            //                                            dfPROTOCOLO.Valor = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT PROTOCOLO FROM GNFESTADUAL WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { dfCODOPER.Valor, dfCODEMPRESA.Valor }).ToString();
            //                                        }
            //                                        List<PS.Lib.DataField> Param = new List<PS.Lib.DataField>();
            //                                        Param.Add(dfCODEMPRESA);
            //                                        Param.Add(dfCODOPER);
            //                                        Param.Add(dfPROTOCOLO);

            //                                        switch (nomeRelatorio)
            //                                        {
            //                                            case "StReportDanfePaisagem":
            //                                                PS.Lib.WinForms.Report.ReportDesignTool rp = new PS.Lib.WinForms.Report.ReportDesignTool(new Relatorios.XrDanfePaisagem(Param));
            //                                                rp.ShowPreviewDialog();
            //                                                break;
            //                                            case "StReportDanfe":
            //                                                PS.Lib.WinForms.Report.ReportDesignTool rr = new PS.Lib.WinForms.Report.ReportDesignTool(new Relatorios.XrDanfe(Param));
            //                                                rr.ShowPreviewDialog();
            //                                                break;
            //                                            default:
            //                                                break;
            //                                        }

            //                                        PSPartNFEstadualData psPartNFEstadualData = new PSPartNFEstadualData();
            //                                        psPartNFEstadualData._tablename = "GNFESTADUAL";
            //                                        psPartNFEstadualData._keys = new string[] { "CODEMPRESA", "CODOPER" };
            //                                        psPartNFEstadualData.RegistraDANFEImpressa(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODOPER.Valor));
            //                                    }
            //                                    else
            //                                    {
            //                                        MessageBox.Show("Nenhum relatório selecionado no parâmetro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                                    }
            //                                }
            //                                atualizaColuna(row1);
            //                                string codStatusNfe = AppLib.Context.poolConnection.Get("Start").ExecGetField("0", "SELECT CODSTATUS FROM GNFESTADUAL WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { row1["GOPER.CODOPER"].ToString(), AppLib.Context.Empresa }).ToString();

            //                                row1["GNFESTADUAL.CODSTATUS"] = codStatusNfe;
            //                                carregaStatusNFE();
            //                            }
            //                            else
            //                            {
            //                                MessageBox.Show("Nf-e não autorizada.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //                            }
            //                        }
            //                        else
            //                        {
            //                            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            //                            {
            //                                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
            //                                if (row1 != null)
            //                                {
            //                                    psPartOperacaoData.GeraNFe(gb.RetornaDataFieldByDataRow(row1));
            //                                }
            //                            }
            //                            MessageBox.Show("NF-e enviando com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                            splashScreenManager1.CloseWaitForm();
            //                        }
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        if (splashScreenManager1.IsSplashFormVisible == true)
            //                        {
            //                            splashScreenManager1.CloseWaitForm();
            //                        }

            //                        MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                MessageBox.Show("Favor selecionar o registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            }

            #endregion

            #region Nova rotina comentada

            //AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            //DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            //PS.Lib.Global gb = new Lib.Global();

            //if (gridView1.SelectedRowsCount > 0)
            //{
            //    if (gridView1.SelectedRowsCount > 1)
            //    {
            //        MessageBox.Show("Selecione apenas um registro para o envio.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }

            //    if (!PS.Glb.Class.VerificaConexao.temInternet(Convert.ToInt32(row["GOPER.CODFILIAL"])))
            //    {
            //        MessageBox.Show("Não é possível enviar a Nota Fiscal pois está sem conexão com a internet.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }

            //    #region Rotina de envio de NF-e por usuário Supervisor

            //    if (row["GNFESTADUAL.CODSTATUS"].ToString() == "E" || row["GNFESTADUAL.CODSTATUS"].ToString() == "I")
            //    {
            //        bool Supervisor = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT SUPERVISOR FROM GUSUARIO WHERE CODUSUARIO = ?", new object[] { AppLib.Context.Usuario }));

            //        if (Supervisor == true)
            //        {
            //            if (MessageBox.Show("Deseja manter o XML gerado?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //            {
            //                splashScreenManager1.ShowWaitForm();
            //                splashScreenManager1.SetWaitFormCaption("Enviando NF-e");

            //                string xml = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT XMLGERADO FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"] }).ToString();

            //                splashScreenManager1.SetWaitFormCaption("Consultando Situação");

            //                EnvioAutomaticoNFe(xml, Convert.ToInt32(row["GOPER.CODOPER"]), row["GOPER.CODTIPOPER"].ToString());

            //                if (StatusEnvioNfe == false)
            //                {
            //                    splashScreenManager1.CloseWaitForm();
            //                    carregaGrid(query);
            //                    return;
            //                }

            //                if (StatusEnvioNfe == false)
            //                {
            //                    MessageBox.Show("Erro ao enviar Nf-e.\r\nPara mais informações consulte o log na Visão de Nf-e.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    splashScreenManager1.CloseWaitForm();
            //                    carregaGrid(query);
            //                    return;
            //                }
            //                if (StatusConsultaNfe == false)
            //                {
            //                    MessageBox.Show("Nota Fiscal não autorizada. \r\nPara mais informações consulte o histórico na Visão de Operação.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    splashScreenManager1.CloseWaitForm();
            //                    carregaGrid(query);
            //                    return;
            //                }

            //                string XMLNfe = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT XMLNFE FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"] }).ToString();

            //                if (!string.IsNullOrEmpty(XMLNfe))
            //                {
            //                    splashScreenManager1.CloseWaitForm();
            //                    MessageBox.Show("NF-e autorizada com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);                  

            //                    //E - mail
            //                    if (MessageBox.Show("Deseja enviar e-mail?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //                    {
            //                        try
            //                        {
            //                            splashScreenManager1.ShowWaitForm();
            //                            splashScreenManager1.SetWaitFormCaption("Enviando E-mail");

            //                            Class.NFeAPI NfeAPI = new Class.NFeAPI();

            //                            string TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();
            //                            string ChaveEmail = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CHAVEACESSO FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ? ", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GOPER.CODOPER"]) }).ToString();
            //                            string Caminho = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT PASTADESTINO FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();

            //                            NfeAPI.EnvioEmail(Convert.ToInt32(row["GOPER.CODOPER"]), Token, "XP", TpAmb ,Caminho, ChaveEmail, row["GOPER.NUMERO"].ToString());

            //                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET EMAILENVIADO = 1 WHERE CODEMPRESA= ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GOPER.CODOPER"]) });
            //                            DocumentoBaixaodo = true;
            //                            splashScreenManager1.CloseWaitForm();
            //                        }
            //                        catch (Exception ex)
            //                        {
            //                            MessageBox.Show("Não foi possível enviar o e-mail.\r\n Utilize a Visão de NF-e para reenviá-lo.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                        }
            //                    }

            //                    // Impressão
            //                    if (MessageBox.Show("Deseja imprimir NF-e?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //                    {
            //                        if (DocumentoBaixaodo == true)
            //                        {
            //                            Process.Start(@"");

            //                        }

            //                        try
            //                        {
            //                            Class.NFeAPI NfeAPI = new Class.NFeAPI();

            //                            string TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();
            //                            string ChaveImpressao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CHAVEACESSO FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ? ", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GOPER.CODOPER"]) }).ToString();
            //                            string Caminho = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT PASTADESTINO FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();

            //                            string retorno = NfeAPI.DownloadNFeAndSave(Token, ChaveImpressao, "P", TpAmb, Caminho, true);
            //                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET DANFEIMPRESSA = 1 WHERE CODEMPRESA= ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GOPER.CODOPER"]) });
            //                        }
            //                        catch (Exception ex)
            //                        {
            //                            MessageBox.Show("Não foi possível imprimir a Danfe.\r\n Utilize a Visão de NF-e para a impressão.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                        }
            //                    }

            //                    if (splashScreenManager1.IsSplashFormVisible == true)
            //                    {
            //                        splashScreenManager1.CloseWaitForm();
            //                    }

            //                    carregaGrid(query);
            //                    return;
            //                }
            //            }
            //        }
            //    }

            //    #endregion

            //    if (Convert.ToInt32(conn.ExecGetField(1, "SELECT COUNT(CODOPER) FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"] })) == 0 || row["GNFESTADUAL.CODSTATUS"].ToString() == "I" || row["GNFESTADUAL.CODSTATUS"].ToString() == "E")
            //    {
            //        AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GNFESTADUALHISTORICO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GOPER.CODOPER"]) });
            //        AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GOPER.CODOPER"]) });

            //        splashScreenManager1.ShowWaitForm();
            //        splashScreenManager1.SetWaitFormCaption("Enviando NF-e");

            //        try
            //        {
            //            #region Bloco de código que gera o XML 

            //            string Versao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT VERSAO FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();

            //            if (Versao == "3.10")
            //            {
            //                PSPartOperacaoData psPartOperacaoData = new PSPartOperacaoData();
            //                psPartOperacaoData._tablename = "GOPER";
            //                psPartOperacaoData._keys = new string[] { "CODEMPRESA", "CODOPER" };
            //                psPartOperacaoData.GeraNFe(gb.RetornaDataFieldByDataRow(row));
            //                xml = ValidateLib.OBJETOS_VALIDATESERVICE.FuncoesAuxiliares.XMLGerado;
            //            }
            //            else if (Versao == "4.00")
            //            {
            //                xml = PS.Glb.Class.GerarNota4_0.Gerar(row["GOPER.CODOPER"].ToString());
            //            }

            //            if (string.IsNullOrEmpty(xml))
            //            {
            //                MessageBox.Show("Erro ao gerar XML.\r\n Favor entrar em contato com o suporte técnico.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                return;
            //            }

            //            #endregion

            //            Class.NFeAPI NfeAPI = new Class.NFeAPI();

            //            splashScreenManager1.SetWaitFormCaption("Consultando Situação");

            //            UpdateXMLGerado(xml, Convert.ToInt32(row["GOPER.CODOPER"]));
            //            EnvioAutomaticoNFe(xml, Convert.ToInt32(row["GOPER.CODOPER"]), row["GOPER.CODTIPOPER"].ToString());

            //            if (StatusEnvioNfe == false)
            //            {
            //                splashScreenManager1.CloseWaitForm();
            //                carregaGrid(query);
            //                return;
            //            }

            //            if (StatusEnvioNfe == false)
            //            {
            //                MessageBox.Show("Erro ao enviar Nf-e.\r\nPara mais informações consulte o log na Visão de Nf-e.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                splashScreenManager1.CloseWaitForm();
            //                carregaGrid(query);
            //                return;
            //            }
            //            if (StatusConsultaNfe == false)
            //            {
            //                MessageBox.Show("Nota Fiscal não autorizada. \r\nPara mais informações consulte o histórico na Visão de Operação.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                splashScreenManager1.CloseWaitForm();
            //                carregaGrid(query);
            //                return;
            //            }

            //            string XMLNfe = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT XMLNFE FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"] }).ToString();

            //            if (!string.IsNullOrEmpty(XMLNfe))
            //            {
            //                splashScreenManager1.CloseWaitForm();
            //                MessageBox.Show("NF-e autorizada com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //                //E - mail
            //                if (MessageBox.Show("Deseja enviar e-mail?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //                {
            //                    try
            //                    {
            //                        splashScreenManager1.ShowWaitForm();
            //                        splashScreenManager1.SetWaitFormCaption("Enviando E-mail");

            //                        string TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();
            //                        string ChaveEmail = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CHAVEACESSO FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ? ", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GOPER.CODOPER"]) }).ToString();
            //                        string Caminho = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT PASTADESTINO FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();

            //                        NfeAPI.EnvioEmail(Convert.ToInt32(row["GOPER.CODOPER"]), Token, "XP", TpAmb ,Caminho, ChaveEmail, row["GOPER.NUMERO"].ToString());
            //                        DocumentoBaixaodo = true;

            //                        #region Rotina antiga

            //                        //PSPartNFEstadualData psPartNFEstadualData = new PSPartNFEstadualData();
            //                        //psPartNFEstadualData._tablename = "GNFESTADUAL";
            //                        //psPartNFEstadualData._keys = new string[] { "CODEMPRESA", "CODOPER" };
            //                        //psPartNFEstadualData.EnvioAutomatico = false;
            //                        //psPartNFEstadualData.EnviarXML(AppLib.Context.Empresa, Convert.ToInt32(row["GOPER.CODOPER"]), true);

            //                        #endregion

            //                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET EMAILENVIADO = 1 WHERE CODEMPRESA= ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GOPER.CODOPER"]) });
            //                        splashScreenManager1.CloseWaitForm();
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        splashScreenManager1.CloseWaitForm();
            //                        MessageBox.Show("Não foi possível enviar o e-mail.\r\n Utilize a Visão de NF-e para reenviá-lo.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                    }
            //                }

            //                // Impressão
            //                if (MessageBox.Show("Deseja imprimir NF-e?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //                {
            //                    if (DocumentoBaixaodo == true)
            //                    {
            //                        DirectoryInfo DirInfo = new DirectoryInfo("C:\\Temp");
            //                        string Danfe = BuscarArquivo(DirInfo);
            //                        System.Diagnostics.Process.Start(@"C:\\Temp\\" + Danfe);
            //                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET DANFEIMPRESSA = 1 WHERE CODEMPRESA= ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GOPER.CODOPER"]) });
            //                        carregaGrid(query);
            //                        return;
            //                    }

            //                    try
            //                    {
            //                        #region Rotina antiga
            //                        //string nomeRelatorio = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CLASSNAME FROM GTIPOPERREPORT 
            //                        //                                                                                                INNER JOIN GOPER ON GTIPOPERREPORT.CODTIPOPER = GOPER.CODTIPOPER
            //                        //                                                                                                INNER JOIN GREPORT ON GTIPOPERREPORT.CODREPORT = GREPORT.CODREPORT
            //                        //                                                                                                WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ? ", new object[] { Convert.ToInt32(row["GOPER.CODOPER"]), AppLib.Context.Empresa }).ToString();
            //                        //if (!string.IsNullOrEmpty(nomeRelatorio))
            //                        //{
            //                        //    PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByDataRow(row, "GOPER.CODEMPRESA");
            //                        //    PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByDataRow(row, "GOPER.CODOPER");
            //                        //    PS.Lib.DataField dfPROTOCOLO = gb.RetornaDataFieldByDataRow(row, "GOPER.PROTOCOLO");
            //                        //    if (dfPROTOCOLO.Valor == null)
            //                        //    {
            //                        //        dfPROTOCOLO.Valor = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT PROTOCOLO FROM GNFESTADUAL WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { dfCODOPER.Valor, dfCODEMPRESA.Valor }).ToString();
            //                        //    }
            //                        //    List<PS.Lib.DataField> Param = new List<PS.Lib.DataField>();
            //                        //    Param.Add(dfCODEMPRESA);
            //                        //    Param.Add(dfCODOPER);
            //                        //    Param.Add(dfPROTOCOLO);

            //                        //    switch (nomeRelatorio)
            //                        //    {
            //                        //        case "StReportDanfePaisagem":
            //                        //            PS.Lib.WinForms.Report.ReportDesignTool rp = new PS.Lib.WinForms.Report.ReportDesignTool(new Relatorios.XrDanfePaisagem(Param));
            //                        //            rp.ShowPreviewDialog();
            //                        //            break;
            //                        //        case "StReportDanfe":
            //                        //            PS.Lib.WinForms.Report.ReportDesignTool rr = new PS.Lib.WinForms.Report.ReportDesignTool(new Relatorios.XrDanfe(Param));
            //                        //            rr.ShowPreviewDialog();
            //                        //            break;
            //                        //        default:
            //                        //            break;
            //                        //    }

            //                        //    PSPartNFEstadualData psPartNFEstadualData = new PSPartNFEstadualData();
            //                        //    psPartNFEstadualData._tablename = "GNFESTADUAL";
            //                        //    psPartNFEstadualData._keys = new string[] { "CODEMPRESA", "CODOPER" };
            //                        //    psPartNFEstadualData.RegistraDANFEImpressa(Convert.ToInt32(dfCODEMPRESA.Valor), Convert.ToInt32(dfCODOPER.Valor));
            //                        //}
            //                        //else
            //                        //{
            //                        //    MessageBox.Show("Nenhum relatório selecionado no parâmetro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                        //}
            //                        #endregion

            //                        string TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();
            //                        string ChaveImpressao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CHAVEACESSO FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ? ", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GOPER.CODOPER"]) }).ToString();
            //                        string Caminho = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT PASTADESTINO FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();

            //                        string retorno = NfeAPI.DownloadNFeAndSave(Token, ChaveImpressao, "P", TpAmb , Caminho, true);
            //                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET DANFEIMPRESSA = 1 WHERE CODEMPRESA= ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GOPER.CODOPER"]) });
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        splashScreenManager1.CloseWaitForm();
            //                        MessageBox.Show("Não foi possível imprimir a Danfe.\r\n Utilize a Visão de NF-e para a impressão.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                    }
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            splashScreenManager1.CloseWaitForm();
            //            MessageBox.Show("Erro ao gerar NF-e.\r\n Favor entrar em contato com o suporte técnico.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            carregaGrid(query);
            //            return;
            //        }
            //        carregaGrid(query);
            //    }
            //    else
            //    {
            //        MessageBox.Show("O registro selecionado possui Nota Fiscal gerada, favor consultar na visão de NF-e.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Selecione um registro para o envio.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            #endregion

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            PS.Lib.Global gb = new Lib.Global();

            DataRow row = null;

            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("Consultando...");

            DataTable dtResultadoNF = new DataTable();
            dtResultadoNF.Columns.Add("CODOPER", typeof(string));
            dtResultadoNF.Columns.Add("CODSTATUS", typeof(string));
            dtResultadoNF.Columns.Add("ENVIANS", typeof(string));
            dtResultadoNF.Columns.Add("AUTORIZOU", typeof(string));
            dtResultadoNF.Columns.Add("FRASE", typeof(string));
            dtResultadoNF.Columns.Add("CODFILIAL", typeof(string));
            dtResultadoNF.Columns.Add("NUMERO", typeof(string));
            dtResultadoNF.Columns.Add("ENVIOUNF", typeof(string));
            dtResultadoNF.Columns.Add("CONSULTOUNF", typeof(string));

            foreach (int item in gridView1.GetSelectedRows())
            {
                row = gridView1.GetDataRow(item);

                #region Rotina de envio de NF-e por usuário Supervisor

                if (row["GNFESTADUAL.CODSTATUS"].ToString() == "E" || row["GNFESTADUAL.CODSTATUS"].ToString() == "I")
                {
                    bool Supervisor = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT SUPERVISOR FROM GUSUARIO WHERE CODUSUARIO = ?", new object[] { AppLib.Context.Usuario }));

                    if (Supervisor == true)
                    {
                        if (MessageBox.Show("Deseja manter o XML gerado?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //Enviando NF-e;
                            string xml = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT XMLGERADO FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"] }).ToString();

                            //Consultando Situação
                            EnvioAutomaticoNFe(xml, Convert.ToInt32(row["GOPER.CODOPER"]), row["GOPER.CODTIPOPER"].ToString());

                            if (StatusEnvioNfe == false)
                            {
                                //Erro ao enviar Nf-e.\r\nPara mais informações consulte o log na Visão de Nf-e
                                dtResultadoNF.Rows.Add(row["GOPER.CODOPER"].ToString(), row["GNFESTADUAL.CODSTATUS"].ToString(), "n", "n", "Erro ao enviar Nf-e.\r\nPara mais informações consulte o log na Visão de Nf-e.", row["GOPER.CODFILIAL"].ToString(), row["GOPER.NUMERO"].ToString(), "n", "n");
                            }
                            if (StatusConsultaNfe == false)
                            {
                                //Nota Fiscal não autorizada. \r\nPara mais informações consulte o histórico na Visão de Operação.
                                dtResultadoNF.Rows.Add(row["GOPER.CODOPER"].ToString(), row["GNFESTADUAL.CODSTATUS"].ToString(), "s", "n", "Nota Fiscal não autorizada. \r\nPara mais informações consulte o histórico na Visão de Operação.", row["GOPER.CODFILIAL"].ToString(), row["GOPER.NUMERO"].ToString(), "n", "n");
                            }

                            string XMLNfe = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT XMLNFE FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"] }).ToString();

                            if (!string.IsNullOrEmpty(XMLNfe))
                            {
                                //NF-e autorizada com sucesso!
                                dtResultadoNF.Rows.Add(row["GOPER.CODOPER"].ToString(), row["GNFESTADUAL.CODSTATUS"].ToString(), "s", "s", "NF-e autorizada com sucesso!", row["GOPER.CODFILIAL"].ToString(), row["GOPER.NUMERO"].ToString(), "s", "s");
                            }

                            if (splashScreenManager1.IsSplashFormVisible == true)
                            {
                                splashScreenManager1.CloseWaitForm();
                            }

                            int nao_autorizada = 0;
                            int autorizada = 0;
                            int enviada = 0;
                            int nao_enviada = 0;

                            foreach (DataRow it in dtResultadoNF.Rows)
                            {
                                if (it["AUTORIZOU"].Equals("s"))
                                    autorizada++;
                                else
                                    nao_autorizada++;

                                if (it["ENVIANS"].Equals("s"))
                                    enviada++;
                                else
                                    nao_enviada++;
                            }

                            //exibe mensagem do resultado da operação
                            string saida_resultado = "RESULTADO DA OPERAÇÃO REALIZADA:";
                            saida_resultado += "\n\n";
                            saida_resultado += "NF-e enviada(s): " + enviada;
                            saida_resultado += "\n";
                            saida_resultado += "NF-e autorizada(s): " + autorizada;
                            saida_resultado += "\n";
                            saida_resultado += "NF-e não enviada(s): " + nao_enviada;
                            saida_resultado += "\n";
                            saida_resultado += "NF-e não autorizada(s): " + nao_autorizada;


                            MessageBox.Show(saida_resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            return;
                        }
                    }
                }

                #endregion

                if (Convert.ToInt32(conn.ExecGetField(1, "SELECT COUNT(CODOPER) FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"] })) == 0 || row["GNFESTADUAL.CODSTATUS"].ToString() == "I" || row["GNFESTADUAL.CODSTATUS"].ToString() == "E")
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GNFESTADUALHISTORICO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GOPER.CODOPER"]) });
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GOPER.CODOPER"]) });

                    try
                    {
                        #region Bloco de código que gera o XML 

                        string Versao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT VERSAO FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();

                        if (Versao == "3.10")
                        {
                            PSPartOperacaoData psPartOperacaoData = new PSPartOperacaoData();
                            psPartOperacaoData._tablename = "GOPER";
                            psPartOperacaoData._keys = new string[] { "CODEMPRESA", "CODOPER" };
                            psPartOperacaoData.GeraNFe(gb.RetornaDataFieldByDataRow(row));
                            xml = ValidateLib.OBJETOS_VALIDATESERVICE.FuncoesAuxiliares.XMLGerado;
                        }
                        else if (Versao == "4.00")
                        {
                            xml = PS.Glb.Class.GerarNota4_0.Gerar(row["GOPER.CODOPER"].ToString());
                        }

                        if (string.IsNullOrEmpty(xml))
                        {
                            //"Erro ao gerar XML.\r\n Favor entrar em contato com o suporte técnico
                            dtResultadoNF.Rows.Add(row["GOPER.CODOPER"].ToString(), row["GNFESTADUAL.CODSTATUS"].ToString(), "n", "n", "Erro ao gerar XML.\r\n Favor entrar em contato com o suporte técnico.", row["GOPER.CODFILIAL"].ToString(), row["GOPER.NUMERO"].ToString(), "n", "n");
                        }

                        #endregion

                        //Consultando Situação
                        UpdateXMLGerado(xml, Convert.ToInt32(row["GOPER.CODOPER"]));
                        EnvioAutomaticoNFe(xml, Convert.ToInt32(row["GOPER.CODOPER"]), row["GOPER.CODTIPOPER"].ToString());

                        if (StatusEnvioNfe == false)
                        {
                            //"Erro ao enviar Nf-e.\r\nPara mais informações consulte o log na Visão de Nf-e
                            dtResultadoNF.Rows.Add(row["GOPER.CODOPER"].ToString(), row["GNFESTADUAL.CODSTATUS"].ToString(), "n", "n", "Erro ao enviar Nf-e.\r\nPara mais informações consulte o log na Visão de Nf-e.", row["GOPER.CODFILIAL"].ToString(), row["GOPER.NUMERO"].ToString(), "n", "n");
                        }
                        if (StatusConsultaNfe == false)
                        {
                            //Nota Fiscal não autorizada. \r\nPara mais informações consulte o histórico na Visão de Operação
                            dtResultadoNF.Rows.Add(row["GOPER.CODOPER"].ToString(), row["GNFESTADUAL.CODSTATUS"].ToString(), "s", "n", "Nota Fiscal não autorizada. \r\nPara mais informações consulte o histórico na Visão de Operação.", row["GOPER.CODFILIAL"].ToString(), row["GOPER.NUMERO"].ToString(), "n", "n");
                        }

                        string XMLNfe = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT XMLNFE FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"] }).ToString();

                        if (!string.IsNullOrEmpty(XMLNfe))
                        {
                            //NF-e autorizada com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information
                            dtResultadoNF.Rows.Add(row["GOPER.CODOPER"].ToString(), row["GNFESTADUAL.CODSTATUS"].ToString(), "s", "s", "NF-e autorizada com sucesso!", row["GOPER.CODFILIAL"].ToString(), row["GOPER.NUMERO"].ToString(), "s", "s");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao gerar NF-e.\r\n Favor entrar em contato com o suporte técnico.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        splashScreenManager1.CloseWaitForm();
                        //carregaGrid(query);
                        return;
                    }
                }
            }//final do for 

            if (splashScreenManager1.IsSplashFormVisible == true)
            {
                splashScreenManager1.CloseWaitForm();
            }

            int nf_nao_autorizada = 0;
            int nf_autorizada = 0;
            int nf_enviada = 0;
            int nf_nao_enviada = 0;

            foreach (DataRow item in dtResultadoNF.Rows)
            {
                if (item["AUTORIZOU"].Equals("s"))
                    nf_autorizada++;
                else
                    nf_nao_autorizada++;

                if (item["ENVIANS"].Equals("s"))
                    nf_enviada++;
                else
                    nf_nao_enviada++;
            }

            //exibe mensagem do resultado da operação
            string saida_resultado_operacao = "RESULTADO DA OPERAÇÃO REALIZADA:";
            saida_resultado_operacao += "\n\n";
            saida_resultado_operacao += "NF-e enviada(s): " + nf_enviada;
            saida_resultado_operacao += "\n";
            saida_resultado_operacao += "NF-e autorizada(s): " + nf_autorizada;
            saida_resultado_operacao += "\n";
            saida_resultado_operacao += "NF-e não enviada(s): " + nf_nao_enviada;
            saida_resultado_operacao += "\n";
            saida_resultado_operacao += "NF-e não autorizada(s): " + nf_nao_autorizada;


            MessageBox.Show(saida_resultado_operacao, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //verifica se tem nf autorizada para enviar
            if (nf_autorizada < 1)
            {
                return;
            }

            Class.NFeAPI NfeAPI = new Class.NFeAPI();
            string TpAmb = "";
            string ChaveEmail = "";
            string Caminho = "";
            string CodOper = "";
            string CodFilial = "";
            string Numero = "";
            string ChaveImpressao = "";

            //ENVIAR POR E-MAIL
            bool enviarEmail = false;
            if (MessageBox.Show("Deseja enviar e-mail?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                enviarEmail = true;

            // IMPRESSÃO
            if (MessageBox.Show("Deseja imprimir NF-e?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string retorno = "";
                string Danfe = "";
                string arquivo = "";

                try
                {
                    foreach (DataRow item in dtResultadoNF.Rows)
                    {
                        CodOper = item["CODOPER"].ToString();
                        CodFilial = item["CODFILIAL"].ToString();
                        Numero = item["NUMERO"].ToString();
                        ChaveImpressao = "";

                        TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, CodFilial }).ToString();
                        ChaveImpressao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CHAVEACESSO FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ? ", new object[] { AppLib.Context.Empresa, Convert.ToInt32(CodOper) }).ToString();
                        Caminho = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT PASTADESTINO FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, CodFilial }).ToString();

                        retorno = NfeAPI.DownloadNFeAndSave(Class.Constantes.TOKEN, ChaveImpressao, "P", TpAmb, Caminho, true);
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET DANFEIMPRESSA = 1 WHERE CODEMPRESA= ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(CodOper) });

                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET DANFEIMPRESSA = 1 WHERE CODEMPRESA= ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(CodOper) });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível imprimir a Danfe.\r\n Utilize a Visão de NF-e para a impressão.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (enviarEmail)
            {
                try
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormCaption("Enviando E-mail");

                    foreach (DataRow item in dtResultadoNF.Rows)
                    {
                        CodOper = item["CODOPER"].ToString();
                        CodFilial = item["CODFILIAL"].ToString();
                        Numero = item["NUMERO"].ToString();

                        TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, CodFilial }).ToString();
                        ChaveEmail = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CHAVEACESSO FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ? ", new object[] { AppLib.Context.Empresa, Convert.ToInt32(CodOper) }).ToString();
                        Caminho = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT PASTADESTINO FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, CodFilial }).ToString();

                        NfeAPI.EnvioEmail(Convert.ToInt32(CodOper), Class.Constantes.TOKEN, "XP", TpAmb, Caminho, ChaveEmail, Numero);
                        //DocumentoBaixaodo = true;

                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET EMAILENVIADO = 1 WHERE CODEMPRESA= ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(CodOper) });
                    }

                    splashScreenManager1.CloseWaitForm();
                    carregaGrid(query);
                }
                catch (Exception ex)
                {
                    splashScreenManager1.CloseWaitForm();
                    MessageBox.Show("Não foi possível enviar o e-mail.\r\n Utilize a Visão de NF-e para reenviá-lo.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string BuscarArquivo(DirectoryInfo DirInf)
        {
            FileInfo[] Files = DirInf.GetFiles();

            var ArquivoOrdenado = Files.OrderBy(x => x.CreationTime);
            var Arquivo = ArquivoOrdenado.Last().ToString();

            return Arquivo;
        }

        private void btnCartaCorrecao_Click(object sender, EventArgs e)
        {
            //if (gridView1.SelectedRowsCount > 0)
            //{
            //    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            //    Processos.Operacao.frmCartaCorrecao frm = new Processos.Operacao.frmCartaCorrecao();
            //    frm.CodOPer = Convert.ToInt32(row1["GOPER.CODOPER"]);
            //    frm.ShowDialog();
            //}
        }

        private void btnNfeDados_Click(object sender, EventArgs e)
        {
            carregaNFE();
        }

        private void btnEventos_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {

                tabEventosNfe.PageVisible = true;
                splitContainer2.Panel2Collapsed = false;
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"(
SELECT 
'Carta Correção' AS 'Evento', 
GNFESTADUALCORRECAO.CODEMPRESA AS 'Empresa', 
GNFESTADUALCORRECAO.CODOPER AS 'Operação',
GOPER.NUMERO AS 'Número NF-e',
GNFESTADUALCORRECAO.DATAPROTOCOLO AS 'Data Evento', 
GNFESTADUALCORRECAO.NSEQITEM AS 'Sequencial', 
GNFESTADUALCORRECAO.MOTIVO AS 'Motivo', 
GNFESTADUALCORRECAO.PROTOCOLO AS 'Protocolo', 
GNFESTADUALCORRECAO.XMLENV AS 'XML Envio', 
GNFESTADUALCORRECAO.XMLPROT AS 'XML Protocolo'

FROM GNFESTADUALCORRECAO
INNER JOIN GOPER ON GOPER.CODEMPRESA = GNFESTADUALCORRECAO.CODEMPRESA AND GOPER.CODOPER = GNFESTADUALCORRECAO.CODOPER

WHERE GNFESTADUALCORRECAO.CODEMPRESA = ?
  AND GNFESTADUALCORRECAO.CODOPER = ?

UNION ALL

SELECT 
'Cancelamento' AS 'Evento', 
GNFESTADUALCANC.CODEMPRESA AS 'Empresa', 
GNFESTADUALCANC.CODOPER AS 'Operação',
GOPER.NUMERO AS 'Número NF-e',
GNFESTADUALCANC.DATAPROTOCOLO AS 'Data Evento', 
'0' AS 'Sequencial', 
GNFESTADUALCANC.MOTIVO AS 'Motivo', 
GNFESTADUALCANC.PROTOCOLO AS 'Protocolo', 
GNFESTADUALCANC.XMLENV AS 'XML Envio', 
GNFESTADUALCANC.XMLPROT AS 'XML Protocolo'

FROM GNFESTADUALCANC
INNER JOIN GOPER ON GOPER.CODEMPRESA = GNFESTADUALCANC.CODEMPRESA AND GOPER.CODOPER = GNFESTADUALCANC.CODOPER

WHERE GNFESTADUALCANC.CODEMPRESA = ?
  AND GNFESTADUALCANC.CODOPER = ?
)
ORDER BY Empresa, Sequencial", new object[] { AppLib.Context.Empresa, row1["GOPER.CODOPER"], AppLib.Context.Empresa, row1["GOPER.CODOPER"] });
                gridControl7.DataSource = dt;

                //carregaGrid("GOPERITEM", gridControl5, gridView4, "WHERE GOPERITEM.CODOPER = " + Convert.ToInt32(row1["GOPER.CODOPER"]) + " AND GOPERITEM.CODEMPRESA = " + AppLib.Context.Empresa, relacionamento, tabelasFilhas);
            }
        }

        private void btnConsultaEventoNfe_Click(object sender, EventArgs e)
        {
            //    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            //    PS.Validate.Services.ConsultaEventos con = new Validate.Services.ConsultaEventos();

            //    int idOutBox = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT IDOUTBOX FROM GNFESTADUAL WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { row1["GOPER.CODOPER"], AppLib.Context.Empresa }));

            //    List<PS.Validate.Services.ConsultaEventos> list = con.ConsultaEventosNfe(idOutBox);
            //    foreach (PS.Validate.Services.ConsultaEventos item in list)
            //    {
            //        switch (item.Evento)
            //        {
            //            case "CCE":

            //                if (Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(*) FROM GNFESTADUALCORRECAO WHERE CODEMPRESA = ? AND CODOPER = ? AND IDOUTBOX = ? AND NSEQITEM = ?", new object[] { AppLib.Context.Empresa, row1["GOPER.CODOPER"], item.idOutBox, item.nseqItem })) == 0)
            //                {
            //                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GNFESTADUALCORRECAO (CODEMPRESA, CODOPER, CODSTATUS, DATAPROTOCOLO, IDOUTBOX, MOTIVO, NSEQITEM, PROTOCOLO, XMLENV, XMLPROT) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, row1["GOPER.CODOPER"], item.codStatus, item.dataProtocolo, item.idOutBox, item.Motivo, item.nseqItem, item.protocolo, item.xmlEnv, item.xmlProt });
            //                }
            //                else
            //                {
            //                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GNFESTADUALCORRECAO SET PROTOCOLO = ?, DATAPROTOCOLO = ?, XMLENV = ?, XMLPROT = ? WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?", new object[] { item.protocolo, item.dataProtocolo, item.xmlEnv, item.xmlProt, AppLib.Context.Empresa, row1["GOPER.CODOPER"], item.nseqItem });
            //                }
            //                break;
            //            case "CAN":
            //                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GNFESTADUALCANC (CODEMPRESA, CODOPER, CODSTATUS, DATAPROTOCOLO, IDOUTBOX, MOTIVO, PROTOCOLO, XMLENV, XMLPROT) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, row1["GOPER.CODOPER"], item.codStatus, item.dataProtocolo, item.idOutBox, item.Motivo, item.protocolo, item.xmlEnv, item.xmlProt });
            //                break;
            //            case "NFE":
            //                if (Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(*) FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, row1["GOPER.CODOPER"] })) == 0)
            //                {
            //                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GNFESTADUAL (CHAVEACESSO, CODEMPRESA, CODOPER, CODSTATUS, CODTIPOPER, DANFEIMPRESSA, DATAPROTOCOLO, DATARECIBO, EMAILENVIADO, IDOUTBOX, PROTOCOLO, RECIBO, XMLNFE, XMLPROT, XMLREC) VALUES (?, ?, ?, ?, ?, 0, ?, ?, 0, ?, ?, ?, ?, ?, ?)", new object[] { item.Chave, AppLib.Context.Empresa, row1["GOPER.CODOPER"], item.codStatus, row1["GOPER.CODTIPOPER"], item.dataProtocolo, item.dataRecibo, item.idOutBox, item.protocolo, item.recibo, item.xmlEnv, item.xmlProt, item.xmlRec });
            //                }
            //                else
            //                {
            //                    switch (item.codStatus)
            //                    {
            //                        case "VAL":
            //                            item.codStatus = "A";
            //                            break;
            //                        case "CON":
            //                            item.codStatus = "U";
            //                            break;
            //                        case "CAN":
            //                            item.codStatus = "C";
            //                            break;
            //                        case "ERR":
            //                            item.codStatus = "E";
            //                            break;
            //                        default:
            //                            break;
            //                    }

            //                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GNFESTADUAL SET CHAVEACESSO = ?, CODSTATUS = ?, DATAPROTOCOLO = ?, DATARECIBO = ?, PROTOCOLO = ?, RECIBO = ?, XMLNFE = ?, XMLPROT = ?, XMLREC = ? WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { item.Chave, item.codStatus, item.dataProtocolo, item.dataRecibo, item.protocolo, item.recibo, item.xmlEnv, item.xmlProt, item.xmlRec, AppLib.Context.Empresa, row1["GOPER.CODOPER"] });

            //                }
            //                break;
            //            default:
            //                break;
            //        }
            //        carregaNFE();
            //}
        }

        private void btnAprovaDesconto_Click(object sender, EventArgs e)
        {
            // VERFICAR PERFIL
            New.Processos.Operacao.frmAprovaDesconto frm = new Processos.Operacao.frmAprovaDesconto();
            frm.ListaCodOper = new List<string>();
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                if (row1 != null)
                {
                    frm.ListaCodOper.Add(row1["GOPER.CODOPER"].ToString());
                }
            }
            frm.ShowDialog();
            carregaGrid(query);
        }

        private void btnAprovaLimiteCredito_Click(object sender, EventArgs e)
        {
            Processos.Operacao.frmAprovaLimiteCredito frm = new Processos.Operacao.frmAprovaLimiteCredito();
            frm.ListaCodOper = new List<string>();
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                if (row1 != null)
                {
                    if (row1["GOPER.CODSITUACAO"].ToString() == "7")
                    {
                        frm.ListaCodOper.Add(row1["GOPER.CODOPER"].ToString());
                    }
                }
            }
            frm.ShowDialog();
            carregaGrid(query);
        }

        private void frmVisaoOperacao_Load(object sender, EventArgs e)
        {
            gridControl1.Select();

            bool GeraBoleto = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT GTIPOPER.GERABOLETO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper }).ToString());

            if (GeraBoleto == false)
            {
                gerarBoletoToolStripMenuItem.Visible = false;
            }

            int Usanumeroorigem = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT USANUMEROORIGEM FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper }));

            if (Usanumeroorigem == 1)
            {
                btnNovo.Enabled = false;
            }
        }

        private void btnCopiaReferencia_Click(object sender, EventArgs e)
        {
            Processos.Operacao.frmSelecaoOpCopiaReferencia frm = new Processos.Operacao.frmSelecaoOpCopiaReferencia();
            frm.codFilial = codFilial;
            frm.codTipOper = codTipOper;
            frm.ShowDialog();
            carregaGrid(query);
        }

        private void btnImprimirDanfe_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja imprimir NF-e?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                    Class.NFeAPI NfeAPI = new Class.NFeAPI();

                    string Token = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT GFILIAL.TOKEN
                                                                                                   FROM GOPER
                                                                                                   INNER JOIN GFILIAL ON GOPER.CODEMPRESA = GFILIAL.CODEMPRESA AND GOPER.CODFILIAL = GFILIAL.CODFILIAL
                                                                                                   WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ? AND GOPER.CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"], row["GOPER.CODFILIAL"] }).ToString();

                    string TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();
                    string ChaveImpressao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CHAVEACESSO FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ? ", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GOPER.CODOPER"]) }).ToString();
                    string Caminho = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT PASTADESTINO FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();

                    string retorno = NfeAPI.DownloadNFeAndSave(Token, ChaveImpressao, "P", TpAmb, Caminho, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível imprimir a Danfe.\r\n Utilize a Visão de NF-e para a impressão.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnConcluirOrdemProducao_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 1)
            {
                MessageBox.Show("Favor selecionar apenas um registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            if (row1["GOPER.CODSTATUS"].ToString() != "0")
            {
                MessageBox.Show("Somente Operações com status Aberto podem ser concluídas", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> CodigosProduto = new List<string>();

            DataTable dtComposicao = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
GOPERITEM.CODPRODUTO, 
COUNT(CODPRODCOM) AS 'COMPOSICAO'
FROM GOPERITEM
LEFT OUTER JOIN VPRODUTOCOM ON VPRODUTOCOM.CODEMPRESA = GOPERITEM.CODEMPRESA AND VPRODUTOCOM.CODPRODUTO = GOPERITEM.CODPRODUTO
WHERE GOPERITEM.CODEMPRESA = ?
AND GOPERITEM.CODOPER = ?
GROUP BY GOPERITEM.CODPRODUTO", new object[] { AppLib.Context.Empresa, row1["GOPER.CODOPER"] });

            if (dtComposicao.Rows.Count <= 0)
            {
                for (int i = 0; i < dtComposicao.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtComposicao.Rows[i]["COMPOSICAO"]) == 0)
                    {
                        CodigosProduto.Add(dtComposicao.Rows[i]["CODPRODUTO"].ToString());
                    }
                }

                if (CodigosProduto.Count == 1)
                {
                    MessageBox.Show("Existem produtos na Ordem sem composição.\nProdutos sem composição: " + CodigosProduto[0] + "", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    string Codigos = string.Empty;

                    for (int i = 0; i < CodigosProduto.Count; i++)
                    {
                        if (i == 0)
                        {
                            Codigos = CodigosProduto[i] + ",";
                        }
                        else
                        {
                            if (i == (CodigosProduto.Count - 1))
                            {
                                Codigos = Codigos + CodigosProduto[i];
                            }
                            else
                            {
                                Codigos = Codigos + CodigosProduto[i] + ",";
                            }
                        }
                    }
                    MessageBox.Show("Existem produtos na Ordem sem composição.\nProdutos sem composição: " + Codigos + "", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();
            List<Class.GOPERITEMPRODRELAC> listRelacionamento = new List<Class.GOPERITEMPRODRELAC>();
            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            Class.gTipOper gtipOPer = new Class.gTipOper().getgTipOper(row["GOPER.CODTIPOPER"].ToString());
            Class.Goper goperOrigem = new Class.Goper();
            goperOrigem = new Class.Goper().getGoper(row["GOPER.CODOPER"].ToString(), conn);

            if (!string.IsNullOrEmpty(gtipOPer.CODTIPOPERENTRADA))
            {
                #region Cópia da Operação

                PS.Glb.Class.Goper goperEntrada = new PS.Glb.Class.Goper();
                Class.gTipOper gtipOPerEntrada = new Class.gTipOper().getgTipOper(gtipOPer.CODTIPOPERENTRADA);
                goperEntrada = new Class.Goper().getGoper(row["GOPER.CODOPER"].ToString(), conn);
                goperEntrada.CODTIPOPER = gtipOPerEntrada.CODTIPOPER;
                goperEntrada.CODLOCAL = gtipOPerEntrada.LOCAL1DEFAULT;
                goperEntrada.CODFILIAL = AppLib.Context.Filial;
                goperEntrada.DATAENTREGA = AppLib.Context.poolConnection.Get("Start").GetDateTime();
                goperEntrada.CODCLIFOR = goperOrigem.CODCLIFOR;
                goperEntrada.CODOPERADOR = goperOrigem.CODOPERADOR;
                goperEntrada.CODSTATUS = "0";
                goperEntrada.CODEMPRESA = AppLib.Context.Empresa;
                goperEntrada.CODCCUSTO = goperOrigem.CODCCUSTO;
                goperEntrada.CODCONTATO = goperOrigem.CODCONTATO;
                goperEntrada.CODNATUREZAORCAMENTO = goperOrigem.CODNATUREZAORCAMENTO;
                goperEntrada.CODSERIE = gtipOPerEntrada.VTIPOPERSERIE.CODSERIE;
                goperEntrada.CODUSUARIOCRIACAO = AppLib.Context.Usuario;
                goperEntrada.CODUSUARIOFATURAMENTO = AppLib.Context.Usuario;
                goperEntrada.CODVENDEDOR = goperOrigem.CODVENDEDOR;
                goperEntrada.CODREPRE = goperOrigem.CODREPRE;
                goperEntrada.DATAALTERACAO = AppLib.Context.poolConnection.Get("Start").GetDateTime();
                goperEntrada.DATACRIACAO = AppLib.Context.poolConnection.Get("Start").GetDateTime();
                goperEntrada.DATAEMISSAO = AppLib.Context.poolConnection.Get("Start").GetDateTime();
                goperEntrada.DATAFATURAMENTO = AppLib.Context.poolConnection.Get("Start").GetDateTime();
                goperEntrada.HISTORICO = goperOrigem.HISTORICO;
                goperEntrada.OBSERVACAO = goperOrigem.OBSERVACAO;
                goperEntrada.ORDEMDECOMPRA = goperOrigem.ORDEMDECOMPRA;

                try
                {
                    // Cópia da entrada
                    if (goperEntrada.setGoper(goperEntrada, conn) != 0)
                    {
                        //Relacionamento Entrada
                        for (int i = 0; i < goperEntrada.item.Count; i++)
                        {
                            Class.GOPERITEMPRODRELAC relacEntrada = new Class.GOPERITEMPRODRELAC();
                            relacEntrada.CODEMPRESAORIGEM = goperOrigem.CODEMPRESA;
                            relacEntrada.CODPRODUTOORIGEM = goperOrigem.item[i].CODPRODUTO;
                            relacEntrada.CODOPERORIGEM = goperOrigem.CODOPER;
                            relacEntrada.NSEQITEMORIGEM = goperOrigem.item[i].NSEQITEM;
                            relacEntrada.CODEMPRESADESTINO = goperEntrada.CODEMPRESA;
                            relacEntrada.CODPRODUTODESTINO = goperEntrada.item[i].CODPRODUTO;
                            relacEntrada.CODOPERDESTINO = goperEntrada.CODOPER;
                            relacEntrada.NSEQITEMDESTINO = goperEntrada.item[i].NSEQITEM;
                            listRelacionamento.Add(relacEntrada);


                        }
                        for (int i = 0; i < listRelacionamento.Count; i++)
                        {
                            if (new Class.GOPERITEMPRODRELAC().setRelacionamento(conn, listRelacionamento[i]) == false)
                            {
                                conn.Rollback();
                                return;
                            }
                        }
                        listRelacionamento.Clear();
                        //
                        #region Baixa
                        Class.Goper goperBaixa = new Class.Goper().getGoper(goperEntrada.CODOPER.ToString(), conn);
                        Class.gTipOper gtipOperBaixa = new Class.gTipOper().getgTipOper(gtipOPer.CODTIPOPERBAIXA);
                        goperBaixa.CODTIPOPER = gtipOperBaixa.CODTIPOPER;
                        goperBaixa.CODLOCAL = gtipOperBaixa.LOCAL1DEFAULT;
                        goperBaixa.CODFILIAL = AppLib.Context.Filial;
                        goperBaixa.DATAENTREGA = AppLib.Context.poolConnection.Get("Start").GetDateTime();
                        goperBaixa.CODCLIFOR = goperOrigem.CODCLIFOR;
                        goperBaixa.CODOPERADOR = goperOrigem.CODOPERADOR;
                        goperBaixa.CODSTATUS = "0";
                        goperBaixa.CODEMPRESA = AppLib.Context.Empresa;
                        goperBaixa.CODCCUSTO = goperOrigem.CODCCUSTO;
                        goperBaixa.CODCONTATO = goperOrigem.CODCONTATO;
                        goperBaixa.CODNATUREZAORCAMENTO = goperOrigem.CODNATUREZAORCAMENTO;
                        goperBaixa.CODSERIE = gtipOperBaixa.VTIPOPERSERIE.CODSERIE;
                        goperBaixa.CODUSUARIOCRIACAO = AppLib.Context.Usuario;
                        goperBaixa.CODUSUARIOFATURAMENTO = AppLib.Context.Usuario;
                        goperBaixa.CODVENDEDOR = goperOrigem.CODVENDEDOR;
                        goperBaixa.CODREPRE = goperOrigem.CODREPRE;
                        goperBaixa.DATAALTERACAO = AppLib.Context.poolConnection.Get("Start").GetDateTime();
                        goperBaixa.DATACRIACAO = AppLib.Context.poolConnection.Get("Start").GetDateTime();
                        goperBaixa.DATAEMISSAO = AppLib.Context.poolConnection.Get("Start").GetDateTime();
                        goperBaixa.DATAFATURAMENTO = AppLib.Context.poolConnection.Get("Start").GetDateTime();
                        goperBaixa.HISTORICO = goperOrigem.HISTORICO;
                        goperBaixa.OBSERVACAO = goperOrigem.OBSERVACAO;
                        goperBaixa.ORDEMDECOMPRA = goperOrigem.ORDEMDECOMPRA;

                        List<Class.GoperItem> goperItemBaixa = new List<Class.GoperItem>();
                        int contador = 1;

                        for (int i = 0; i < goperEntrada.item.Count; i++)
                        {
                            DataTable dtItensComposto = conn.ExecQuery("SELECT CODPRODCOM, QUANTIDADE FROM VPRODUTOCOM WHERE CODPRODUTO = ? AND CODEMPRESA = ?", new object[] { goperEntrada.item[i].CODPRODUTO, goperEntrada.CODEMPRESA });
                            for (int iComposto = 0; iComposto < dtItensComposto.Rows.Count; iComposto++)
                            {
                                Class.GoperItem itemBaixa = new Class.GoperItem();
                                itemBaixa.CODPRODUTO = dtItensComposto.Rows[iComposto]["CODPRODCOM"].ToString();

                                itemBaixa.NSEQITEM = contador;
                                contador++;
                                itemBaixa.CODEMPRESA = goperEntrada.CODEMPRESA;
                                itemBaixa.APLICACAOMATERIAL = "C";
                                //Busca o Código da unidade
                                switch (gtipOperBaixa.UNDMEDPADRAO)
                                {
                                    case 0:
                                        itemBaixa.CODUNIDOPER = conn.ExecGetField(string.Empty, "SELECT CODUNIDCONTROLE FROM VPRODUTO WHERE CODPRODUTO = ? AND CODEMPRESA = ? ", new object[] { itemBaixa.CODPRODUTO, goperBaixa.CODEMPRESA }).ToString();
                                        break;
                                    case 1:
                                        itemBaixa.CODUNIDOPER = conn.ExecGetField(string.Empty, "SELECT CODUNIDCOMPRA FROM VPRODUTO WHERE CODPRODUTO = ? AND CODEMPRESA = ? ", new object[] { itemBaixa.CODPRODUTO, goperBaixa.CODEMPRESA }).ToString();
                                        break;
                                    case 2:
                                        itemBaixa.CODUNIDOPER = conn.ExecGetField(string.Empty, "SELECT CODUNIDVENDA FROM VPRODUTO WHERE CODPRODUTO = ? AND CODEMPRESA = ? ", new object[] { itemBaixa.CODPRODUTO, goperBaixa.CODEMPRESA }).ToString();
                                        break;
                                    default:
                                        break;
                                }
                                itemBaixa.QUANTIDADE = goperBaixa.item[i].QUANTIDADE * Convert.ToDecimal(dtItensComposto.Rows[iComposto]["QUANTIDADE"]);
                                itemBaixa.CODUNIDCONTROLE = conn.ExecGetField(string.Empty, "SELECT CODUNIDCONTROLE FROM VPRODUTO WHERE CODPRODUTO = ? AND CODEMPRESA = ? ", new object[] { itemBaixa.CODPRODUTO, goperBaixa.CODEMPRESA }).ToString();
                                goperItemBaixa.Add(itemBaixa);
                                //Preenchimento relacionamento Baixa
                                Class.GOPERITEMPRODRELAC relacBaixa = new Class.GOPERITEMPRODRELAC();
                                relacBaixa.CODEMPRESAORIGEM = goperOrigem.CODEMPRESA;
                                relacBaixa.CODPRODUTOORIGEM = goperOrigem.item[i].CODPRODUTO;
                                relacBaixa.CODOPERORIGEM = goperOrigem.CODOPER;
                                relacBaixa.NSEQITEMORIGEM = goperOrigem.item[i].NSEQITEM;
                                relacBaixa.CODEMPRESADESTINO = goperBaixa.CODEMPRESA;
                                relacBaixa.CODPRODUTODESTINO = itemBaixa.CODPRODUTO;
                                relacBaixa.CODOPERDESTINO = goperBaixa.CODOPER;
                                relacBaixa.NSEQITEMDESTINO = itemBaixa.NSEQITEM;
                                listRelacionamento.Add(relacBaixa);
                            }
                        }
                        goperBaixa.item = goperItemBaixa;
                        if (goperBaixa.setGoper(goperBaixa, conn) == 0)
                        {
                            conn.Rollback();
                            return;
                        }
                        #endregion

                        //Relacionamento Baixa
                        for (int i = 0; i < listRelacionamento.Count; i++)
                        {
                            listRelacionamento[i].CODOPERDESTINO = goperBaixa.CODOPER;
                            if (new Class.GOPERITEMPRODRELAC().setRelacionamento(conn, listRelacionamento[i]) == false)
                            {
                                conn.Rollback();
                                return;
                            }
                        }

                        //Altera o status da origem para concluído
                        conn.ExecTransaction("UPDATE GOPER SET CODSTATUS = '8' WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { goperOrigem.CODOPER, AppLib.Context.Empresa });
                        conn.Commit();

                        //Estoque Entrada
                        if (gtipOPerEntrada.OPERESTOQUE == "A")
                        {
                            PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                            psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
                            psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };
                            PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque Estoque;
                            Estoque = PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao;
                            for (int i = 0; i < goperEntrada.item.Count; i++)
                            {
                                psPartLocalEstoqueSaldoData.MovimentaEstoque2(Estoque, goperEntrada.CODEMPRESA, goperEntrada.CODOPER, goperEntrada.item[i].NSEQITEM);
                            }
                        }
                        //Estoque Baixa
                        if (gtipOperBaixa.OPERESTOQUE == "D")
                        {
                            PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                            psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
                            psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };
                            PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque Estoque;
                            Estoque = PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao;
                            for (int i = 0; i < goperBaixa.item.Count; i++)
                            {
                                psPartLocalEstoqueSaldoData.MovimentaEstoque2(Estoque, goperBaixa.CODEMPRESA, goperBaixa.CODOPER, goperBaixa.item[i].NSEQITEM);
                            }
                        }
                        ////

                        MessageBox.Show("Ordem finalizada com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        carregaGrid(query);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    conn.Rollback();
                }
                #endregion
            }
        }

        private void frmVisaoOperacao_KeyDown(object sender, KeyEventArgs e)
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
                    PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque frm = new PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque();
                    frm.ShowDialog();
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    break;
                case Keys.F9:
                    break;
                case Keys.Insert:
                    btnNovo_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
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
                    PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque frm = new PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque();
                    frm.ShowDialog();
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    break;
                case Keys.F9:
                    break;
                case Keys.Insert:
                    btnNovo_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void btnGerarBoleto_Click(object sender, EventArgs e)
        {
            int env = 0, nEnv = 0;
            PS.Lib.Global gb = new PS.Lib.Global();
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                int TipoPagamento = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT VFORMAPGTO.TIPO FROM GOPER INNER JOIN VFORMAPGTO ON VFORMAPGTO.CODEMPRESA = GOPER.CODEMPRESA AND VFORMAPGTO.CODFORMA = GOPER.CODFORMA WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ? ", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"] }).ToString());

                if (TipoPagamento != 15)
                {
                    MessageBox.Show("Forma de Pagamento diferente de boleto.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    List<Class.FLANCA> listaFLANCA = new Class.FLANCA().getFlancaOper(Convert.ToInt32(row["GOPER.CODOPER"]), AppLib.Context.poolConnection.Get("Start"));
                    for (int iLista = 0; iLista < listaFLANCA.Count; iLista++)
                    {
                        if (string.IsNullOrEmpty(listaFLANCA[iLista].CODSTATUS))
                        {
                            MessageBox.Show("Não existe financeiro para essa operação.\nProcesso cancelado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        //Verificar se o mesmo já foi gerado
                        if (Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(*) FROM FBOLETO INNER JOIN FLANCA ON FBOLETO.CODLANCA = FLANCA.CODLANCA AND FBOLETO.CODEMPRESA = FLANCA.CODEMPRESA AND FBOLETO.CODFILIAL = FLANCA.CODFILIAL WHERE FLANCA.CODOPER = ? AND FLANCA.CODFILIAL = ? AND FLANCA.CODEMPRESA = ? AND FLANCA.NUMERO = ?", new object[] { row["GOPER.CODOPER"], AppLib.Context.Filial, AppLib.Context.Empresa, listaFLANCA[iLista].NUMERO })).Equals(0))
                        {
                            if (listaFLANCA[iLista].CODTIPDOC.Equals("NFV"))
                            {
                                //new frmVisaoLancamento().GerarBoleto(gb.RetornaDataFieldByDataRow(row));
                                PSPartGerarBoletoData psPartGerarBoletoData = new PSPartGerarBoletoData();
                                //psPartGerarBoletoData._tablename = "FBOLETO";
                                //psPartGerarBoletoData._keys = new string[] { "CODEMPRESA", "CODLANCA" };
                                psPartGerarBoletoData.GerarBoleto(listaFLANCA[iLista], AppLib.Context.poolConnection.Get("Start"));
                                env = env + 1;
                            }
                            else
                            {
                                nEnv = nEnv + 1;
                            }
                        }
                        else
                        {
                            nEnv = nEnv + 1;
                        }
                    }
                    MessageBox.Show("Operação Realizada com Sucesso. \nQtde de Boletos Gerados: " + env + "\nQtde de Boletos não Gerados: " + nEnv + "\n", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnAprovaOperacao_Click(object sender, EventArgs e)
        {
            Processos.Operacao.frmAprovaOperacao frm = new Processos.Operacao.frmAprovaOperacao();
            frm.ListaCodOper = new List<string>();
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {

                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                if (row1 != null)
                {
                    if (row1["GOPER.CODSTATUS"].ToString() == "0")
                    {
                        frm.ListaCodOper.Add(row1["GOPER.CODOPER"].ToString());
                    }
                }
            }
            frm.ShowDialog();
            carregaGrid(query);
        }

        private void btnReprovaOperacao_Click(object sender, EventArgs e)
        {
            Processos.Operacao.frmReprovaOperacao frm = new Processos.Operacao.frmReprovaOperacao();
            frm.ListaCodOper = new List<string>();
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                if (row1 != null)
                {
                    if (row1["GOPER.CODSTATUS"].ToString() == "0")
                    {
                        frm.ListaCodOper.Add(row1["GOPER.CODOPER"].ToString());
                    }
                }
            }
            frm.ShowDialog();
            carregaGrid(query);
        }

        #region Rotina de envio da NF-e

        /// <summary>
        /// Método que executa o envio automático da NF-e.
        /// </summary>
        /// <param name="XML">XML para o envio da nota</param>
        private void EnvioAutomaticoNFe(string XML, int Codoper, string CodtipOper)
        {
            Class.NFeAPI NfeAPI = new Class.NFeAPI();
            Class.TratarXML x = new Class.TratarXML();

            int Codfilial = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT CODFILIAL FROM GOPER WHERE CODOPER = ?", new object[] { Codoper }));
            string Token = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT GFILIAL.TOKEN
                                                                                                   FROM GOPER
                                                                                                   INNER JOIN GFILIAL ON GOPER.CODEMPRESA = GFILIAL.CODEMPRESA AND GOPER.CODFILIAL = GFILIAL.CODFILIAL
                                                                                                   WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ? AND GOPER.CODFILIAL = ?", new object[] { AppLib.Context.Empresa, Codoper, codFilial }).ToString();

            try
            {
                string RetornoEnvio = NfeAPI.EmitirNFe(Token, XML, "xml");

                // Tratamento para recuperar o valores retornados pelo método EmitirNFe.
                dynamic JsonEnvio = JsonConvert.DeserializeObject(RetornoEnvio);
                string Status = JsonEnvio.status;
                string cStatEnvio = string.Empty;
                string xMotivo = string.Empty;

                if (Status != "200" && Status == "-5")
                {
                    cStatEnvio = JsonEnvio.erro.cStat;
                    xMotivo = JsonEnvio.erro.xMotivo;
                }

                string Motivo = JsonEnvio.motivo;
                string Rec = JsonEnvio.nsNRec;
                string EnvioErro = string.Empty;

                int FormatoImpressao = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT TIPOIMPRESSAODANFE FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ? ", new object[] { AppLib.Context.Empresa, CodtipOper }));

                // Validação do SCHEMA
                if (Status == "-2")
                {
                    string Json = Convert.ToString(JsonEnvio);

                    int IniErros = Json.IndexOf("[");
                    int FimErros = Json.LastIndexOf("]");

                    string Erros = Json.Substring(IniErros, (FimErros - IniErros)).Replace("[", "");

                    for (int i = 0; i < Erros.Length; i++)
                    {
                        Erros = Erros.Trim();
                    }

                    if (Erros.Contains("\r\n"))
                    {
                        Erros = Erros.Replace("\r\n", "");
                    }

                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GNFESTADUALHISTORICO(CODEMPRESA, CODOPER, IDHISTORICO, DATA, CODUSUARIO, OBSERVACAO) 
                                                                             VALUES
                                                                             (?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, Codoper, getIdHistorico(Codoper), DateTime.Now, AppLib.Context.Usuario, Motivo + "-" + Erros });

                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET CODSTATUS = 'I' WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper });

                    MessageBox.Show("Erro ao validar o Schema do XML. Para mais informações consulte o histórico na Visão de Operação.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    StatusEnvioNfe = false;
                    return;
                }

                if (string.IsNullOrEmpty(xMotivo))
                {
                    xMotivo = "";
                }
                else
                {
                    EnvioErro = ". Erro " + cStatEnvio + ": " + xMotivo;
                    Motivo += EnvioErro;
                }

                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GNFESTADUALHISTORICO(CODEMPRESA, CODOPER, IDHISTORICO, DATA, CODUSUARIO, OBSERVACAO) 
                                                                             VALUES
                                                                             (?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, Codoper, getIdHistorico(Codoper), DateTime.Now, AppLib.Context.Usuario, Motivo });
                // Enviou para a NS
                if (Status == "200")
                {
                    StatusEnvioNfe = true;
                    NfeAPI.UpdateGNFEstadualAposEmissao(Codoper, Rec, DateTime.Now, CodtipOper, FormatoImpressao);

                    string CNPJ = getCGCCPFFormatado();

                    // Aumenta o tempo de consulta em 3000 milissegundos(3 segundos)
                    System.Threading.Thread.Sleep(3000);

                    // Consulta a Nota Fiscal
                    string RetornoXML = NfeAPI.ConsultaStatusProcessamento(Token, CNPJ, Rec);

                    dynamic JsonRetornoConsulta = JsonConvert.DeserializeObject(RetornoXML);
                    string StatusConsulta = JsonRetornoConsulta.status;
                    string cStat = JsonRetornoConsulta.cStat;
                    string MotivoConsulta = JsonRetornoConsulta.xMotivo;

                    // Realiza o INSERT para o log de Consulta.
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GNFESTADUALHISTORICO(CODEMPRESA, CODOPER, IDHISTORICO, DATA, CODUSUARIO, OBSERVACAO)
                                                                                 VALUES
                                                                                 (?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, Codoper, getIdHistorico(Codoper), Convert.ToDateTime(DateTime.Now), AppLib.Context.Usuario, "cStat:" + cStat + " - " + MotivoConsulta });

                    if (StatusConsulta == "200" && cStat == "100")
                    {
                        //Executa a rotina e atualza o status para "A"
                        StatusConsultaNfe = true;
                        string XmlDownload = JsonRetornoConsulta.xml;
                        XmlDownload = XmlDownload.Replace("'", "\"");
                        string Protocolo = JsonRetornoConsulta.nProt;
                        DateTime DataProtocolo = JsonRetornoConsulta.dhRecbto;
                        string Chave = JsonRetornoConsulta.chNFe;

                        NfeAPI.UpdateGNFEstadualAposConsulta(Codoper, XmlDownload, Protocolo, DataProtocolo);
                    }
                    else if (StatusConsulta == "200" && cStat == "110")
                    {
                        //Atualiza o status para "D", executa a rotina de cancelamento da Operação e retorna
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET CODSTATUS = 'D' WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper });

                        AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                        DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                        DateTime DataOperacao = Convert.ToDateTime(row["GOPER.DATAEMISSAO"]);
                        int CodFilial = Convert.ToInt32(row["GOPER.CODFILIAL"]);

                        ExcluiSaldoEstoqueNF(conn, Codoper, DataOperacao, CodFilial);

                        if (excluiFinanceiroNF(conn, Codoper) == true)
                        {
                            #region Remove os relacionamentos e calcula a operação de origem novamente

                            DataTable dtRelac = conn.ExecQuery("SELECT GOPERITEMRELAC.CODOPERITEMORIGEM, GOPERITEMRELAC.NSEQITEMORIGEM, GOPERITEM.NSEQITEM, GOPERITEM.QUANTIDADE, GOPERITEM.CODEMPRESA, GOPER.CODTIPOPER FROM GOPERITEMRELAC INNER JOIN GOPERITEM ON GOPERITEMRELAC.CODOPERITEMDESTINO = GOPERITEM.CODOPER AND GOPERITEMRELAC.NSEQITEMDESTINO = GOPERITEM.NSEQITEM  INNER JOIN GOPER ON GOPERITEMRELAC.CODOPERITEMORIGEM = GOPER.CODOPER WHERE GOPERITEMRELAC.CODOPERITEMDESTINO = ? AND GOPER.CODEMPRESA = ?", new object[] { Codoper, AppLib.Context.Empresa });

                            if (dtRelac.Rows.Count > 0)
                            {
                                PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                                psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
                                psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };
                                psPartLocalEstoqueSaldoData.MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.OperacaoCancelamento, AppLib.Context.Empresa, Codoper, 0);
                                conn.ExecTransaction("DELETE FROM VFICHAESTOQUE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CodFilial, Codoper });

                                for (int iRelac = 0; iRelac < dtRelac.Rows.Count; iRelac++)
                                {
                                    PSPartOperacaoEdit frmGera = new PSPartOperacaoEdit();
                                    frmGera.codFilial = CodFilial;

                                    string aaaa = conn.ParseCommand("UPDATE GOPERITEM SET QUANTIDADE_SALDO = QUANTIDADE_SALDO + " + dtRelac.Rows[iRelac]["QUANTIDADE"].ToString().Replace(",", ".") + ", QUANTIDADE_FATURADO = QUANTIDADE_FATURADO - " + dtRelac.Rows[iRelac]["QUANTIDADE"].ToString().Replace(",", ".") + " WHERE CODOPER = ? AND CODEMPRESA = ? AND NSEQITEM = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"], dtRelac.Rows[iRelac]["CODEMPRESA"], dtRelac.Rows[iRelac]["NSEQITEMORIGEM"] });

                                    conn.ExecTransaction("UPDATE GOPERITEM SET QUANTIDADE_SALDO = QUANTIDADE_SALDO + " + dtRelac.Rows[iRelac]["QUANTIDADE"].ToString().Replace(",", ".") + ", QUANTIDADE_FATURADO = QUANTIDADE_FATURADO - " + dtRelac.Rows[iRelac]["QUANTIDADE"].ToString().Replace(",", ".") + " WHERE CODOPER = ? AND CODEMPRESA = ? AND NSEQITEM = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"], dtRelac.Rows[iRelac]["CODEMPRESA"], dtRelac.Rows[iRelac]["NSEQITEMORIGEM"] });

                                    frmGera.calculaOperacao(Convert.ToInt32(dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"]), dtRelac.Rows[iRelac]["CODTIPOPER"].ToString());

                                    conn.ExecTransaction("UPDATE GOPER SET CODSTATUS = 0 WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"], AppLib.Context.Empresa });
                                    conn.ExecTransaction("UPDATE GOPER SET GOPER.CODSTATUS = CASE WHEN ((SELECT (SUM(GOPERITEM.QUANTIDADE) - SUM(GOPERITEM.QUANTIDADE_SALDO)) FROM GOPERITEM WHERE GOPERITEM.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEM.CODOPER = GOPER.CODOPER) = 0) THEN '0' ELSE '5' END FROM GOPER WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"], AppLib.Context.Empresa });
                                    conn.ExecTransaction("DELETE FROM GOPERITEMRELAC WHERE CODOPERITEMORIGEM = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"].ToString() });
                                    conn.ExecTransaction("DELETE FROM GOPERRELAC WHERE CODOPER = ? AND CODOPERRELAC = ? AND CODEMPRESA =?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"], Codoper, AppLib.Context.Empresa });
                                }
                            }
                            else
                            {
                                dtRelac = conn.ExecQuery(@"SELECT GOPERRELAC.CODOPERRELAC, GOPERRELAC.CODEMPRESA, GOPER.CODOPER 
                                                                FROM GOPERRELAC
                                                                    INNER JOIN GOPER ON GOPERRELAC.CODOPER = GOPER.CODOPER
                                                                WHERE GOPERRELAC.CODOPERRELAC = ?
                                                                AND GOPERRELAC.CODEMPRESA = ?", new object[] { Codoper, AppLib.Context.Empresa });

                                for (int iRelac = 0; iRelac < dtRelac.Rows.Count; iRelac++)
                                {
                                    conn.ExecTransaction("UPDATE GOPER SET GOPER.CODSTATUS = 0 FROM GOPER WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { dtRelac.Rows[iRelac]["CODOPER"], AppLib.Context.Empresa });
                                    conn.ExecTransaction("DELETE FROM GOPERITEMRELAC WHERE CODOPERITEMORIGEM = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"].ToString() });
                                    conn.ExecTransaction("DELETE FROM GOPERRELAC WHERE CODOPER = ? AND CODOPERRELAC = ? AND CODEMPRESA =?", new object[] { dtRelac.Rows[iRelac]["CODOPER"], dtRelac.Rows[iRelac]["CODOPERRELAC"], AppLib.Context.Empresa });
                                }
                            }

                            #endregion

                            #region Altera o status da Operação selecionada e o Status da Nota Fiscal como Denegada

                            conn.ExecTransaction(@"UPDATE GOPER SET CODSTATUS = (SELECT CODSTATUS FROM GSTATUS WHERE TABELA = 'GOPER' AND DESCRICAO = 'CANCELADO') WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper });

                            #endregion
                        }
                        else
                        {
                            MessageBox.Show("Não foi possível concluir o cancelamento. Erro na exclusão do Financeiro", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        MessageBox.Show("Uso Denegado: Irregularidade fiscal do emitente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (StatusConsulta == "200" && cStat == "105")
                    {
                        //Mantém o status "P" e retorna
                        StatusConsultaNfe = false;
                        return;
                    }
                    else
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET CODSTATUS = 'E' WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper });
                        StatusConsultaNfe = false;
                        return;
                    }
                }
                else
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET CODSTATUS = 'I' WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper });
                    StatusEnvioNfe = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para atualizar o XMLGerado na tabela GNFESTADUAL.
        /// </summary>
        /// <param name="XMLGerado">XML gerado para o envio da NF-e</param>
        /// <param name="Codoper">Código da Operação</param>
        private void UpdateXMLGerado(string XMLGerado, int Codoper)
        {
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppLib.Context.poolConnection.Get("Start").ConnectionString);
            System.Data.SqlClient.SqlCommand command;
            try
            {
                conn.Open();

                command = new System.Data.SqlClient.SqlCommand("UPDATE GNFESTADUAL SET XMLGERADO = '" + XMLGerado + "' WHERE CODEMPRESA = " + AppLib.Context.Empresa + " AND CODOPER = " + Codoper + "", conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Método para retornar o CNPJ/CPF do cliente do contexto.
        /// </summary>
        /// <returns>CNPJ/CPF</returns>
        private string getCGCCPFFormatado()
        {
            string CGCCPF = string.Empty;

            CGCCPF = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CGCCPF FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, codFilial }).ToString();
            //CGCCPF = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CGCCPF FROM GEMPRESA WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();

            CGCCPF = CGCCPF.Replace(".", "").Replace("-", "").Replace("/", "");

            return CGCCPF;
        }

        /// <summary>
        /// Método que retorna o Id Histórico
        /// </summary>
        /// <param name="CodOper">Código da Operação</param>
        /// <returns>Id Histórico</returns>
        private int getIdHistorico(int CodOper)
        {
            int IdHistorico;

            IdHistorico = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT ISNULL(MAX(IDHISTORICO), 0) FROM GNFESTADUALHISTORICO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CodOper }));

            if (IdHistorico >= 0)
            {
                IdHistorico++;
            }
            return IdHistorico;
        }

        /// <summary>
        /// Método para exclusão do Saldo do Estoque
        /// </summary>
        /// <param name="conn">Conexão ativa</param>
        /// <param name="Codoper">Código da Operação</param>
        /// <param name="DataOper">Data de Emissão da Operação</param>
        /// <param name="Codfilial">Código da Filial</param>
        private void ExcluiSaldoEstoqueNF(AppLib.Data.Connection conn, int Codoper, DateTime DataOper, int Codfilial)
        {
            DataTable dtRecalSaldo = new DataTable();

            try
            {
                // Executa as transações de Dados em cascata
                conn.ExecTransaction("DELETE FROM VFICHAESTOQUE_ANTERIOR", new object[] { });

                conn.ExecTransaction(@"INSERT INTO VFICHAESTOQUE_ANTERIOR 
SELECT VFICHAESTOQUE.*
FROM VFICHAESTOQUE (NOLOCK)
INNER JOIN VPARAMETROS ON VPARAMETROS.CODEMPRESA = VFICHAESTOQUE.CODEMPRESA
WHERE VFICHAESTOQUE.DATAENTSAI >= ?
AND VFICHAESTOQUE.CODEMPRESA = ?
AND VFICHAESTOQUE.CODFILIAL = ?
AND VFICHAESTOQUE.CODPRODUTO IN (SELECT GOPERITEM.CODPRODUTO
									FROM GOPERITEM
									WHERE GOPERITEM.CODEMPRESA = ? 
									AND GOPERITEM.CODOPER = ?)", new object[] { DataOper, AppLib.Context.Empresa, Codfilial, AppLib.Context.Empresa, Codoper });

                conn.ExecTransaction(@"DELETE FROM VFICHAESTOQUE
WHERE VFICHAESTOQUE.DATAENTSAI >= ?
AND VFICHAESTOQUE.CODEMPRESA = ?
AND VFICHAESTOQUE.CODFILIAL = ? 
AND VFICHAESTOQUE.CODPRODUTO IN  (SELECT GOPERITEM.CODPRODUTO
									FROM GOPERITEM
									WHERE GOPERITEM.CODEMPRESA = ? 
									AND GOPERITEM.CODOPER = ?)", new object[] { DataOper, AppLib.Context.Empresa, Codfilial, AppLib.Context.Empresa, Codoper });

                dtRecalSaldo = conn.ExecQuery(@"SELECT 
 GOPERITEM.CODEMPRESA
,GOPERITEM.CODOPER
,GOPERITEM.NSEQITEM

,GOPER.DATAENTSAI
,GOPERITEM.CODPRODUTO

FROM
GOPERITEM (NOLOCK)
INNER JOIN GOPER (NOLOCK) ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER
INNER JOIN GTIPOPER (NOLOCK) ON GTIPOPER.CODEMPRESA = GOPER.CODEMPRESA AND GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER
INNER JOIN VPARAMETROS ON VPARAMETROS.CODEMPRESA = GOPERITEM.CODEMPRESA

WHERE
GOPERITEM.CODEMPRESA = ?
AND GOPER.CODFILIAL = ? 
AND GTIPOPER.OPERESTOQUE <> 'N'
AND GOPERITEM.CODPRODUTO IN (SELECT GOPERITEM.CODPRODUTO
									FROM GOPERITEM
									WHERE GOPERITEM.CODEMPRESA = ? 
									AND GOPERITEM.CODOPER = ?)
AND GOPER.DATAENTSAI >= ?
AND GOPER.CODSTATUS <> 2

ORDER BY 
 GOPERITEM.CODEMPRESA
,GOPERITEM.CODPRODUTO
,GOPER.DATAENTSAI
,GOPERITEM.CODOPER
,GOPERITEM.NSEQITEM", new object[] { AppLib.Context.Empresa, Codfilial, AppLib.Context.Empresa, Codoper, DataOper });

                for (int i = 0; i < dtRecalSaldo.Rows.Count; i++)
                {
                    PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                    PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque Estoque;
                    Estoque = PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao;

                    psPartLocalEstoqueSaldoData.MovimentaEstoque2(Estoque, AppLib.Context.Empresa, Convert.ToInt32(dtRecalSaldo.Rows[i]["CODOPER"]), Convert.ToInt32(dtRecalSaldo.Rows[i]["NSEQITEM"]));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="Codoper"></param>
        /// <returns></returns>
        private bool excluiFinanceiroNF(AppLib.Data.Connection conn, int Codoper)
        {
            try
            {
                conn.ExecTransaction("DELETE FROM FLANCA WHERE CODOPER = ? AND CODEMPRESA =?", new object[] { Codoper, AppLib.Context.Empresa });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Métodos para execução dos processos manuais 

        /// <summary>
        /// Método para exclusão do Saldo do Estoque
        /// </summary>
        /// <param name="conn">Conexão ativa</param>
        /// <param name="Codoper">Código da Operação</param>
        /// <param name="DataOper">Data de Emissão da Operação</param>
        /// <param name="Codfilial">Código da Filial</param>
        private void ExcluiSaldoEstoqueManual(AppLib.Data.Connection conn, List<int> codoper, List<DateTime> DataOper)
        {
            DataTable dtRecalSaldo = new DataTable();

            try
            {
                // Executa as transações de Dados em cascata

                for (int i = 0; i < codoper.Count; i++)
                {
                    conn.ExecTransaction("DELETE FROM VFICHAESTOQUE_ANTERIOR", new object[] { });

                    conn.ExecTransaction(@"INSERT INTO VFICHAESTOQUE_ANTERIOR 
SELECT VFICHAESTOQUE.*
FROM VFICHAESTOQUE (NOLOCK)
INNER JOIN VPARAMETROS ON VPARAMETROS.CODEMPRESA = VFICHAESTOQUE.CODEMPRESA
WHERE VFICHAESTOQUE.DATAENTSAI >= ?
AND VFICHAESTOQUE.CODEMPRESA = ?
AND VFICHAESTOQUE.CODFILIAL = ?
AND VFICHAESTOQUE.CODPRODUTO IN (SELECT GOPERITEM.CODPRODUTO
									FROM GOPERITEM
									WHERE GOPERITEM.CODEMPRESA = ? 
									AND GOPERITEM.CODOPER = ?)", new object[] { DataOper[i], AppLib.Context.Empresa, codFilial, AppLib.Context.Empresa, codoper[i] });

                    conn.ExecTransaction(@"DELETE FROM VFICHAESTOQUE
WHERE VFICHAESTOQUE.DATAENTSAI >= ?
AND VFICHAESTOQUE.CODEMPRESA = ?
AND VFICHAESTOQUE.CODFILIAL = ? 
AND VFICHAESTOQUE.CODPRODUTO IN  (SELECT GOPERITEM.CODPRODUTO
									FROM GOPERITEM
									WHERE GOPERITEM.CODEMPRESA = ? 
									AND GOPERITEM.CODOPER = ?)", new object[] { DataOper[i], AppLib.Context.Empresa, codFilial, AppLib.Context.Empresa, codoper[i] });

                    dtRecalSaldo = conn.ExecQuery(@"SELECT 
 GOPERITEM.CODEMPRESA
,GOPERITEM.CODOPER
,GOPERITEM.NSEQITEM

,GOPER.DATAENTSAI
,GOPERITEM.CODPRODUTO

FROM
GOPERITEM (NOLOCK)
INNER JOIN GOPER (NOLOCK) ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER
INNER JOIN GTIPOPER (NOLOCK) ON GTIPOPER.CODEMPRESA = GOPER.CODEMPRESA AND GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER
INNER JOIN VPARAMETROS ON VPARAMETROS.CODEMPRESA = GOPERITEM.CODEMPRESA

WHERE
GOPERITEM.CODEMPRESA = ?
AND GOPER.CODFILIAL = ? 
AND GTIPOPER.OPERESTOQUE <> 'N'
AND GOPERITEM.CODPRODUTO IN (SELECT GOPERITEM.CODPRODUTO
									FROM GOPERITEM
									WHERE GOPERITEM.CODEMPRESA = ? 
									AND GOPERITEM.CODOPER = ?)
AND GOPER.DATAENTSAI >= ?
AND GOPER.CODSTATUS <> 2

ORDER BY 
 GOPERITEM.CODEMPRESA
,GOPERITEM.CODPRODUTO
,GOPER.DATAENTSAI
,GOPERITEM.CODOPER
,GOPERITEM.NSEQITEM", new object[] { AppLib.Context.Empresa, codFilial, AppLib.Context.Empresa, codoper[i], DataOper[i] });
                }

                for (int i = 0; i < dtRecalSaldo.Rows.Count; i++)
                {
                    PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                    PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque Estoque;
                    Estoque = PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao;

                    psPartLocalEstoqueSaldoData.MovimentaEstoque2(Estoque, AppLib.Context.Empresa, Convert.ToInt32(dtRecalSaldo.Rows[i]["CODOPER"]), Convert.ToInt32(dtRecalSaldo.Rows[i]["NSEQITEM"]));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="m_codOper"></param>
        /// <returns></returns>
        private bool excluiFinanceiroManual(AppLib.Data.Connection conn, List<int> m_codOper)
        {
            try
            {
                for (int i = 0; i < m_codOper.Count; i++)
                {
                    conn.ExecTransaction("DELETE FROM FLANCA WHERE CODOPER = ? AND CODEMPRESA =?", new object[] { m_codOper[i], AppLib.Context.Empresa });
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        private void gridView1_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                selecionou = true;

                gridView1.UpdateSummary();
            }

            if (gridView1.SelectedRowsCount == 1)
            {
                contador = 1;
                lbResultRegistros.Text = contador.ToString();
                tbTotalResult.Text = new Class.Utilidades().CalculaTotal(gridView1, "GOPER").ToString();
                contador = 0;
            }
            else
            {
                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    contador = gridView1.SelectedRowsCount;

                    lbResultRegistros.Text = contador.ToString();
                    tbTotalResult.Text = new Class.Utilidades().CalculaTotal(gridView1, "GOPER").ToString();
                }
            }
        }

        private void gridView1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                GridView view = sender as GridView;

                if (e.IsTotalSummary && (e.Item as GridSummaryItem).FieldName == "GOPER.CODSTATUS" || e.IsTotalSummary && (e.Item as GridSummaryItem).FieldName == "GOPER.VALORLIQUIDO") 
                {
                    GridSummaryItem item = e.Item as GridSummaryItem;

                    if (item.FieldName == "GOPER.CODSTATUS")
                    {
                        switch (e.SummaryProcess)
                        {
                            case DevExpress.Data.CustomSummaryProcess.Start:
                                break;
                            case DevExpress.Data.CustomSummaryProcess.Calculate:
                                break;
                            case DevExpress.Data.CustomSummaryProcess.Finalize:
                                e.TotalValue = gridView1.SelectedRowsCount;
                                break;
                            default:
                                break;
                        }
                    }
                    if (item.FieldName == "GOPER.VALORLIQUIDO")
                    {
                        switch (e.SummaryProcess)
                        {
                            case DevExpress.Data.CustomSummaryProcess.Start:
                                break;
                            case DevExpress.Data.CustomSummaryProcess.Calculate:
                                break;
                            case DevExpress.Data.CustomSummaryProcess.Finalize:
                                e.TotalValue = string.Format("Total: {0}", new Class.Utilidades().CalculaTotal(gridView1, "GOPER").ToString("n2"));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
