using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Visao
{
    public partial class frmVisaoNotaFiscalEstadual : Form
    {
        public Form pai = null;
        public bool consulta = false;
        string tabela = "GNFESTADUAL";
        string query = string.Empty;
        public string codMenu = string.Empty;
        private bool permiteEditar = true;

        List<string> tabelasFilhas = new List<string>();

        //Variaveis para usar quando a tela abre para consulta.
        public string CodOper;
        public int IdOutbox;

        //Variaveis para usar nos processos referentes à NF-e
        Class.NFeAPI NfeAPI = new Class.NFeAPI();
        Class.TratarXML x = new Class.TratarXML();

        string Token = string.Empty;

        public frmVisaoNotaFiscalEstadual(string _query, Form frmprin, string _CodMenu)
        {
            InitializeComponent();
            codMenu = _CodMenu;
            this.MdiParent = frmprin;
            tabelasFilhas.Clear();
            query = _query;
            CarregaGrid(query);
            getAcesso(codMenu);
        }

        private void getAcesso(string CodMenu)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO, EXCLUSAO, CONSULTA, INCLUSAO FROM GPERMISSAOMENU WHERE CODMENU = ? AND CODPERFIL = ?", new object[] { "btnOperacoes_NotaFiscalEstadual", AppLib.Context.Perfil });
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
                }
            }
            else
            {
                btnEditar.Enabled = false;
            }
        }

        public void CarregaGrid(string where)
        {
            try
            {
                List<string> TabelasFilhas = new List<string>();

                tabelasFilhas.Add("GOPER");
                tabelasFilhas.Add("VCLIFOR");

                string sql = string.Empty;
                string relac = " INNER JOIN GOPER ON GNFESTADUAL.CODEMPRESA = GOPER.CODEMPRESA AND GOPER.CODOPER = GNFESTADUAL.CODOPER  INNER JOIN VCLIFOR ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR";

                //where = where + " AND GNFESTADUAL.CODSTATUS NOT IN ('E', 'I')";
                sql = new Class.Utilidades().getVisao(tabela, relac, tabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                carregaStatusNFE();

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregaGrid(query);
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            CarregaGrid(query);
        }

        private void btnSalvarLayout_Click(object sender, EventArgs e)
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
                    FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
                CarregaGrid(query);
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

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            Filtro.frmFiltroNotaFiscal Nota = new Filtro.frmFiltroNotaFiscal();
            Nota.aberto = true;
            Nota.ShowDialog();
            if (!string.IsNullOrEmpty(Nota.condicao))
            {
                query = Nota.condicao;
                CarregaGrid(query);
            }
        }

        private void atualizaColuna(DataRow dr)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, dr["GNFESTADUAL.CODOPER"] });

            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                if (gridView1.Columns[i].ReadOnly == false)
                {
                    try
                    {
                        dr[gridView1.Columns[i].FieldName] = dr[gridView1.Columns[i].FieldName] = dt.Rows[0][gridView1.Columns[i].FieldName.ToString().Remove(0, gridView1.Columns[i].FieldName.ToString().IndexOf(".") + 1)].ToString();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void Atualizar()
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                PS.Glb.New.Cadastros.frmCadastroNotaFiscalEstadual Nota = new Cadastros.frmCadastroNotaFiscalEstadual();
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                Nota.CodOper = Convert.ToInt32(row1["GNFESTADUAL.CODOPER"]);
                Nota.edita = true;
                Nota.ShowDialog();
                atualizaColuna(row1);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Atualizar();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            Atualizar();
        }

        private void eventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                TabEventos.PageVisible = true;
                splitContainer1.Panel2Collapsed = false;
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
ORDER BY Empresa, Sequencial", new object[] { AppLib.Context.Empresa, row1["GNFESTADUAL.CODOPER"], AppLib.Context.Empresa, row1["GNFESTADUAL.CODOPER"] });

                gridControl2.DataSource = dt;
            }
        }

        private void inutilizaçãoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = false;
            TabPageInutilizacao.PageVisible = true;
            CarregaGridInutilizacao();
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
                consultaSituaçãoNFeToolStripMenuItem.Visible = false;
                cancelamentoNFeToolStripMenuItem.Visible = false;
                cartaDeCorreçãoToolStripMenuItem.Visible = false;
                consultarEventoToolStripMenuItem.Visible = false;
                consultarSituaçãoToolStripMenuItem.Visible = false;
                monitorSEFAZToolStripMenuItem.Visible = false;
                alterarModalidadeToolStripMenuItem.Visible = false;
                exportarXMLToolStripMenuItem.Visible = false;
                enviarEmailToolStripMenuItem.Visible = false;
                imprirmirDanfeToolStripMenuItem.Visible = false;

                toolStripSeparator3.Visible = false;
                toolStripSeparator4.Visible = false;
                toolStripSeparator5.Visible = false;
                toolStripSeparator6.Visible = false;
            }
            else
            {
                consultaSituaçãoNFeToolStripMenuItem.Visible = true;
                cancelamentoNFeToolStripMenuItem.Visible = true;
                cartaDeCorreçãoToolStripMenuItem.Visible = true;
                consultarEventoToolStripMenuItem.Visible = true;
                consultarSituaçãoToolStripMenuItem.Visible = true;
                monitorSEFAZToolStripMenuItem.Visible = true;
                alterarModalidadeToolStripMenuItem.Visible = true;
                exportarXMLToolStripMenuItem.Visible = true;
                enviarEmailToolStripMenuItem.Visible = true;
                imprirmirDanfeToolStripMenuItem.Visible = true;

                toolStripSeparator3.Visible = true;
                toolStripSeparator4.Visible = true;
                toolStripSeparator5.Visible = true;
                toolStripSeparator6.Visible = true;
            }

            if (splitContainer1.Panel2Collapsed == false)
            {
                if (TabEventos.PageVisible == true)
                {
                    CarregaGridNfe(Convert.ToInt32(row1["GNFESTADUAL.CODOPER"]));
                }
            }
        }

        /// <summary>
        /// Método para carregar a grid de Anexos dos eventos.
        /// </summary>
        /// <param name="Codoper">Código da Operação selecionada</param>
        public void CarregaGridNfe(int Codoper)
        {
            try
            {
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
ORDER BY Empresa, Sequencial", new object[] { AppLib.Context.Empresa, Codoper, AppLib.Context.Empresa, Codoper });

                gridControl2.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fecharAnexosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = true;
            TabEventos.PageVisible = false;
            TabPageInutilizacao.PageVisible = false;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string reportPath = string.Empty;
            FolderBrowserDialog open = new FolderBrowserDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                reportPath = open.SelectedPath + "NFE.xlsx";
                DevExpress.XtraPrinting.XlsExportOptions xlsxOptions = new DevExpress.XtraPrinting.XlsExportOptions();
                xlsxOptions.ShowGridLines = true;
                xlsxOptions.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;
                xlsxOptions.ExportHyperlinks = true;
                xlsxOptions.SheetName = "Nota Fiscal Estadual";
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

        #region Processos

        private void consultaSituaçãoNFeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                if (row["GNFESTADUAL.CODSTATUS"].ToString() == "P")
                {
                    if (gridView1.SelectedRowsCount > 1)
                    {
                        MessageBox.Show("Selecione apenas um registro para o envio.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormCaption("Consultando Situação");

                    try
                    {
                        string CNPJ = NfeAPI.getCGCCPFFormatado(Convert.ToInt32(row["GOPER.CODFILIAL"]));
                        string Recibo = row["GNFESTADUAL.RECIBO"].ToString();
                        string CodtipOper = row["GNFESTADUAL.CODTIPOPER"].ToString();

                        // Aumenta o tempo de consulta em 3000 milissegundos(3 segundos)
                        System.Threading.Thread.Sleep(3000);

                        // Consulta a Nota Fiscal
                        Token = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT GFILIAL.TOKEN
                                                                                                   FROM GOPER
                                                                                                   INNER JOIN GFILIAL ON GOPER.CODEMPRESA = GFILIAL.CODEMPRESA AND GOPER.CODFILIAL = GFILIAL.CODFILIAL
                                                                                                   WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ? AND GOPER.CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"], row["GOPER.CODFILIAL"] }).ToString();

                        string RetornoXML = NfeAPI.ConsultaStatusProcessamento(Token, CNPJ, Recibo);

                        dynamic JsonRetornoConsulta = JsonConvert.DeserializeObject(RetornoXML);
                        string StatusConsulta = JsonRetornoConsulta.status;
                        string MotivoConsulta = JsonRetornoConsulta.xMotivo;

                        // Realiza o INSERT para o log 
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GNFESTADUALHISTORICO(CODEMPRESA, CODOPER, IDHISTORICO, DATA, CODUSUARIO, OBSERVACAO)
                                                                                     VALUES
                                                                                     (?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]), NfeAPI.getIdHistorico(Convert.ToInt32(row["GNFESTADUAL.CODOPER"])), Convert.ToDateTime(DateTime.Now), AppLib.Context.Usuario, MotivoConsulta });

                        if (StatusConsulta == "200")
                        {
                            string XmlDownload = JsonRetornoConsulta.xml;
                            XmlDownload = XmlDownload.Replace("'", "\"");
                            string Protocolo = JsonRetornoConsulta.nProt;
                            DateTime DataProtocolo = JsonRetornoConsulta.dhRecbto;
                            string Chave = JsonRetornoConsulta.chNFe;

                            NfeAPI.UpdateGNFEstadualAposConsulta(Convert.ToInt32(row["GNFESTADUAL.CODOPER"]), XmlDownload, Protocolo, DataProtocolo);
                        }
                        else
                        {
                            MessageBox.Show("Erro ao consultar Nf-e.\r\nPara mais informações consulte o log na Visão de Nf-e.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            splashScreenManager1.CloseWaitForm();
                            CarregaGrid(query);
                            return;
                        }

                        string XMLNfe = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT XMLNFE FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) }).ToString();

                        if (!string.IsNullOrEmpty(XMLNfe))
                        {
                            splashScreenManager1.CloseWaitForm();
                            MessageBox.Show("NF-e consultada com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CarregaGrid(query);
                            return;
                        }
                        else
                        {
                            splashScreenManager1.CloseWaitForm();
                            MessageBox.Show("Erro ao consultar NF-e.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            CarregaGrid(query);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        splashScreenManager1.CloseWaitForm();
                        throw ex;
                    }
                }
                else
                {
                    MessageBox.Show("Somente registros com status Pendente podem ser consultados.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void cancelamentoNFeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                if (PermissaoOperacao() == true)
                {
                    if (PermissaoUsuario(row["GNFESTADUAL.CODTIPOPER"].ToString()) == true)
                    {
                        if (row["GNFESTADUAL.CODSTATUS"].ToString() == "A")
                        {
                            int StatusFinanceiro = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT COUNT(CODOPER) FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ? AND CODSTATUS = 1", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) }));

                            if (StatusFinanceiro == 0)
                            {
                                int BoletoGerado = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT COUNT(CODOPER) FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ? AND CODSTATUS = 0 AND BOLETOGERADO = 'SIM'", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) }));

                                if (BoletoGerado == 0)
                                {
                                    if (gridView1.SelectedRowsCount > 1)
                                    {
                                        MessageBox.Show("Selecione apenas um registro para o cancelamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                    try
                                    {
                                        PS.Glb.New.NotaFiscal.frmProcessoNotaFiscal frm = new NotaFiscal.frmProcessoNotaFiscal("Cancelamento");
                                        frm.Chave = row["GNFESTADUAL.CHAVEACESSO"].ToString();
                                        frm.Protocolo = row["GNFESTADUAL.PROTOCOLO"].ToString();
                                        frm.TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();
                                        frm.Codoper = Convert.ToInt32(row["GNFESTADUAL.CODOPER"]);
                                        frm.ShowDialog();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Não foi possível concluir o cancelamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }

                                    CarregaGrid(query);
                                }
                                else
                                {
                                    MessageBox.Show("O registro selecionado não pode ser cancelado pois possui boleto gerado.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("O registro selecionado não pode ser cancelado pois existe financeiro baixado.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Somente registros com status Autorizado podem ser cancelados.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuário sem permissão para cancelamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Operação sem permissão para cancelamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void cartaDeCorreçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                if (row["GNFESTADUAL.CODSTATUS"].ToString() == "A")
                {
                    if (gridView1.SelectedRowsCount > 1)
                    {
                        MessageBox.Show("Selecione apenas um registro para a correção.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    try
                    {
                        PS.Glb.New.NotaFiscal.frmProcessoNotaFiscal frm = new NotaFiscal.frmProcessoNotaFiscal("Carta de Correção");
                        frm.Chave = row["GNFESTADUAL.CHAVEACESSO"].ToString();
                        frm.TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();

                        int NseqEvento = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODOPER) FROM GNFESTADUALCORRECAO WHERE CODEMPRESA = ? AND CODOPER = ? ", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) }));

                        if (NseqEvento >= 0)
                        {
                            NseqEvento++;
                        }

                        frm.NseqEvento = NseqEvento.ToString();
                        frm.Codoper = Convert.ToInt32(row["GNFESTADUAL.CODOPER"]);
                        frm.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não foi possível concluir a correção.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    CarregaGrid(query);
                }
            }
        }

        private void consultarEventoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                if (row["GNFESTADUAL.CODSTATUS"].ToString() == "U")
                {
                    if (gridView1.SelectedRowsCount > 1)
                    {
                        MessageBox.Show("Selecione apenas um registro para a consulta de evento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormCaption("Consultando Evento");

                    try
                    {
                        string ChaveNfe = row["GNFESTADUAL.CHAVEACESSO"].ToString();
                        string TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();
                        string TpDownload = "X";
                        string TpEvento = "CANC";
                        string NseqEvento = "1";

                        Token = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT GFILIAL.TOKEN
                                                                                                   FROM GOPER
                                                                                                   INNER JOIN GFILIAL ON GOPER.CODEMPRESA = GFILIAL.CODEMPRESA AND GOPER.CODFILIAL = GFILIAL.CODFILIAL
                                                                                                   WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ? AND GOPER.CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"], row["GOPER.CODFILIAL"] }).ToString();

                        string RetornoEvento = NfeAPI.DownloadNFeEvento(Token, ChaveNfe, TpAmb, TpDownload, TpEvento, NseqEvento);

                        dynamic JsonRetornoEvento = JsonConvert.DeserializeObject(RetornoEvento);
                        string StatusEvento = JsonRetornoEvento.status;
                        string MotivoEvento = JsonRetornoEvento.retEvento.xMotivo;
                        DateTime DhRegistroEvento = JsonRetornoEvento.retEvento.dhRegEvento;
                        string Protocolo = JsonRetornoEvento.retEvento.nProt;
                        string XMLEvento = JsonRetornoEvento.xml;
                        string TipoEvento = x.recuperaTagtpEvento(XMLEvento);
                        XMLEvento = XMLEvento.Replace("'", "\"");

                        AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GNFESTADUALHISTORICO(CODEMPRESA, CODOPER, IDHISTORICO, DATA, CODUSUARIO, OBSERVACAO) 
                                                                                     VALUES
                                                                                     (?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]), NfeAPI.getIdHistorico(Convert.ToInt32(row["GNFESTADUAL.CODOPER"])), DateTime.Now, AppLib.Context.Usuario, MotivoEvento });
                        if (StatusEvento == "200")
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET CODSTATUS = 'C' WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) });
                            AtualizaGNFESTADUALEVENTO(Convert.ToInt32(row["GNFESTADUAL.CODOPER"]), TipoEvento, StatusEvento);
                            InsertGNFESTADUALCANC(Convert.ToInt32(row["GNFESTADUAL.CODOPER"]), Protocolo, DhRegistroEvento, XMLEvento);

                            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                            DateTime DataOperacao = Convert.ToDateTime(conn.ExecGetField(null, "SELECT DATAEMISSAO FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) }));
                            int CodFilial = Convert.ToInt32(conn.ExecGetField(0, "SELECT CODFILIAL FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) }));

                            ExcluiSaldoEstoque(conn, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]), DataOperacao, CodFilial);

                            if (excluiFinanceiro(conn, Convert.ToInt32(row["GNFESTADUAL.CODOPER"])) == true)
                            {
                                #region Remove os relacionamentos e calcula a operação de origem novamente

                                DataTable dtRelac = conn.ExecQuery("SELECT GOPERITEMRELAC.CODOPERITEMORIGEM, GOPERITEMRELAC.NSEQITEMORIGEM, GOPERITEM.NSEQITEM, GOPERITEM.QUANTIDADE, GOPERITEM.CODEMPRESA, GOPER.CODTIPOPER FROM GOPERITEMRELAC INNER JOIN GOPERITEM ON GOPERITEMRELAC.CODOPERITEMDESTINO = GOPERITEM.CODOPER AND GOPERITEMRELAC.NSEQITEMDESTINO = GOPERITEM.NSEQITEM  INNER JOIN GOPER ON GOPERITEMRELAC.CODOPERITEMORIGEM = GOPER.CODOPER WHERE GOPERITEMRELAC.CODOPERITEMDESTINO = ? AND GOPER.CODEMPRESA = ?", new object[] { row["GNFESTADUAL.CODOPER"].ToString(), AppLib.Context.Empresa });

                                if (dtRelac.Rows.Count > 0)
                                {
                                    PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                                    psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
                                    psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };
                                    psPartLocalEstoqueSaldoData.MovimentaEstoque2(PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.OperacaoCancelamento, AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]), 0);
                                    conn.ExecTransaction("DELETE FROM VFICHAESTOQUE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CodFilial, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) });

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
                                        conn.ExecTransaction("DELETE FROM GOPERRELAC WHERE CODOPER = ? AND CODOPERRELAC = ? AND CODEMPRESA =?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"], Convert.ToInt32(row["GNFESTADUAL.CODOPER"]), AppLib.Context.Empresa });
                                    }
                                }
                                else
                                {
                                    dtRelac = conn.ExecQuery(@"SELECT GOPERRELAC.CODOPERRELAC, GOPERRELAC.CODEMPRESA, GOPER.CODOPER 
                                                                FROM GOPERRELAC
                                                                    INNER JOIN GOPER ON GOPERRELAC.CODOPER = GOPER.CODOPER
                                                                WHERE GOPERRELAC.CODOPERRELAC = ?
                                                                AND GOPERRELAC.CODEMPRESA = ?", new object[] { Convert.ToInt32(row["GNFESTADUAL.CODOPER"]), AppLib.Context.Empresa });

                                    for (int iRelac = 0; iRelac < dtRelac.Rows.Count; iRelac++)
                                    {
                                        conn.ExecTransaction("UPDATE GOPER SET GOPER.CODSTATUS = 0 FROM GOPER WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { dtRelac.Rows[iRelac]["CODOPER"], AppLib.Context.Empresa });
                                        conn.ExecTransaction("DELETE FROM GOPERITEMRELAC WHERE CODOPERITEMORIGEM = ?", new object[] { dtRelac.Rows[iRelac]["CODOPERITEMORIGEM"].ToString() });
                                        conn.ExecTransaction("DELETE FROM GOPERRELAC WHERE CODOPER = ? AND CODOPERRELAC = ? AND CODEMPRESA =?", new object[] { dtRelac.Rows[iRelac]["CODOPER"], dtRelac.Rows[iRelac]["CODOPERRELAC"], AppLib.Context.Empresa });
                                    }
                                }

                                #endregion

                                #region Altera o status da Operação selecionada 

                                conn.ExecTransaction(@"UPDATE GOPER SET CODSTATUS = (SELECT CODSTATUS FROM GSTATUS WHERE TABELA = 'GOPER' AND DESCRICAO = 'CANCELADO') WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) });

                                #endregion
                            }
                            else
                            {
                                MessageBox.Show("Não foi possível concluir o cancelamento. Erro na exclusão do Financeiro", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET CODSTATUS = 'A' WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) });
                            AtualizaGNFESTADUALEVENTO(Convert.ToInt32(row["GNFESTADUAL.CODOPER"]), "", StatusEvento);
                            MessageBox.Show("Não foi possível consultar o evento de Cancelamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Não foi possível cancelar a Operação. \r\nFavor executar o processo de cancelamento manual.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        splashScreenManager1.CloseWaitForm();
                        CarregaGrid(query);
                        return;
                    }

                    splashScreenManager1.CloseWaitForm();
                    CarregaGrid(query);
                }
                else if (row["GNFESTADUAL.CODSTATUS"].ToString() == "F")
                {
                    if (gridView1.SelectedRowsCount > 1)
                    {
                        MessageBox.Show("Selecione apenas um registro para a consulta de evento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormCaption("Consultando Evento");

                    try
                    {
                        string ChaveNfe = row["GNFESTADUAL.CHAVEACESSO"].ToString();
                        string TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();
                        string TpDownload = "X";
                        string TpEvento = "CCE";

                        int NseqEvento = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODOPER) FROM GNFESTADUALCORRECAO WHERE CODEMPRESA = ? AND CODOPER = ? ", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) }));

                        if (NseqEvento >= 0)
                        {
                            NseqEvento++;
                        }

                        Token = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT GFILIAL.TOKEN
                                                                                                   FROM GOPER
                                                                                                   INNER JOIN GFILIAL ON GOPER.CODEMPRESA = GFILIAL.CODEMPRESA AND GOPER.CODFILIAL = GFILIAL.CODFILIAL
                                                                                                   WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ? AND GOPER.CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"], row["GOPER.CODFILIAL"] }).ToString();

                        string RetornoEvento = NfeAPI.DownloadNFeEvento(Token, ChaveNfe, TpAmb, TpDownload, TpEvento, NseqEvento.ToString());

                        dynamic JsonRetornoEvento = JsonConvert.DeserializeObject(RetornoEvento);
                        string StatusEvento = JsonRetornoEvento.status;
                        string MotivoEvento = JsonRetornoEvento.retEvento.xMotivo;
                        DateTime DhRegistroEvento = JsonRetornoEvento.retEvento.dhRegEvento;
                        string Protocolo = JsonRetornoEvento.retEvento.nProt;
                        string XMLEvento = JsonRetornoEvento.xml;
                        string TipoEvento = x.recuperaTagtpEvento(XMLEvento);
                        XMLEvento = XMLEvento.Replace("'", "\"");
                        string Correcao = x.recuperaTagCorrecao(XMLEvento);

                        AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GNFESTADUALHISTORICO(CODEMPRESA, CODOPER, IDHISTORICO, DATA, CODUSUARIO, OBSERVACAO) 
                                                                                     VALUES
                                                                                     (?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]), NfeAPI.getIdHistorico(Convert.ToInt32(row["GNFESTADUAL.CODOPER"])), DateTime.Now, AppLib.Context.Usuario, MotivoEvento });

                        if (StatusEvento == "200")
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET CODSTATUS = 'A' WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) });
                            InsertGNFESTADUALCORRECAO(Convert.ToInt32(row["GNFESTADUAL.CODOPER"]), NseqEvento, Protocolo, DhRegistroEvento, Correcao, XMLEvento);
                        }
                        else
                        {
                            MessageBox.Show("Não foi possível consultar o evento de Carta de Correção.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    splashScreenManager1.CloseWaitForm();
                    CarregaGrid(query);
                }
                else
                {
                    MessageBox.Show("Não existe evento pendente para a consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void inutilizaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 1)
            {
                MessageBox.Show("Selecione apenas um registro para a inutilização.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PS.Glb.New.NotaFiscal.frmInutilizacao frm = new NotaFiscal.frmInutilizacao();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível concluir a inutilização.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CarregaGrid(query);
        }

        private void consultarSituaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                if (gridView1.SelectedRowsCount > 1)
                {
                    MessageBox.Show("Selecione apenas um registro para a consulta de evento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();

                Token = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT GFILIAL.TOKEN
                                                                                                   FROM GOPER
                                                                                                   INNER JOIN GFILIAL ON GOPER.CODEMPRESA = GFILIAL.CODEMPRESA AND GOPER.CODFILIAL = GFILIAL.CODFILIAL
                                                                                                   WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ? AND GOPER.CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"], row["GOPER.CODFILIAL"] }).ToString();

                string RetornoSituacao = NfeAPI.ConsultaSituacaoNFe(Token, NfeAPI.getCGCCPFFormatado(Convert.ToInt32(row["GOPER.CODFILIAL"])), row["GNFESTADUAL.CHAVEACESSO"].ToString(), TpAmb);

                dynamic JsonRetornoSituacao = JsonConvert.DeserializeObject(RetornoSituacao);
                string StatusSituacao = JsonRetornoSituacao.status;

                if (StatusSituacao == "200")
                {
                    string Situacao = JsonRetornoSituacao.motivo;
                    string xMotivo = JsonRetornoSituacao.retConsSitNFe.xMotivo;
                    string Mensagem = Situacao + "!" + "\r\n" + xMotivo + ".";

                    if (RetornoSituacao.Contains("xEvento"))
                    {
                        string Evento = string.Empty;

                        if (RetornoSituacao.Contains("Cancelamento registrado") && RetornoSituacao.Contains("Carta de Correção registrada"))
                        {
                            Evento = "Evento: Carta de Correção \r\nCarta de Correção registrada." + "\r\nEvento: Cancelamento \r\nCancelamento registrado.";
                        }
                        else
                        {
                            string xEvento = x.recuperaTagxEvento(RetornoSituacao);
                            Evento = "Evento: " + xEvento + ".";
                        }

                        Mensagem = Mensagem + "\r\n" + Evento;
                    }

                    MessageBox.Show(Mensagem, "Situação da NF-e", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Não foi possível consultar a situação do registro selecionado.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void monitorSEFAZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PS.Glb.New.NotaFiscal.frmMonitorSefaz Monitor = new NotaFiscal.frmMonitorSefaz();
            Monitor.ShowDialog();
        }

        private void alterarModalidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PS.Glb.New.NotaFiscal.frmAlterarModalidade Modalidade = new NotaFiscal.frmAlterarModalidade();
            Modalidade.ShowDialog();
        }

        private void exportarXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Enviados = 0, NaoEnviados = 0;

            if (MessageBox.Show("Deseja exportar o(s) registro(s) selecionado(s)?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();

                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                        {
                            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                            if (row1["GNFESTADUAL.CODSTATUS"].ToString() == "A" || row1["GNFESTADUAL.CODSTATUS"].ToString() == "C")
                            {
                                if (ExportarXML(Convert.ToInt32(row1["GNFESTADUAL.CODOPER"]), fbd.SelectedPath) == true)
                                {
                                    Enviados++;
                                }
                                else
                                {
                                    NaoEnviados++;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                MessageBox.Show("Operação Realizada com Sucesso. \nQuantidade de NFe Exportadas: " + Enviados + "\nQuantidade de NFe não Exportada: " + NaoEnviados + "\n", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void enviarEmailToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                PS.Glb.New.NotaFiscal.frmEnvioEmail frm = new PS.Glb.New.NotaFiscal.frmEnvioEmail();

                string sqlFilial = @"SELECT GOPER.CODFILIAL
                                     FROM GNFESTADUAL
                                     INNER JOIN GOPER ON GNFESTADUAL.CODEMPRESA = GOPER.CODEMPRESA
                                     AND GNFESTADUAL.CODOPER = GOPER.CODOPER
                                     WHERE GNFESTADUAL.CODEMPRESA = ? AND GNFESTADUAL.CODOPER = ?";

                frm.Codoper = Convert.ToInt32(row["GNFESTADUAL.CODOPER"]);
                frm.CodEmp = Convert.ToInt32(row["GNFESTADUAL.CODEMPRESA"]);
                frm.chave = row["GNFESTADUAL.CHAVEACESSO"].ToString();
                frm.CodFilial = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, sqlFilial, new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) }));
                frm.CodStatus = row["GNFESTADUAL.CODSTATUS"].ToString();
                frm.ShowDialog();
            }
        }

        private void ImprirmirDanfeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja imprimir a NF-e?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                #region Rotina antiga comentada
                //PS.Lib.Global gb = new Lib.Global();
                //DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                //string nomeRelatorio = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CLASSNAME FROM GTIPOPERREPORT 
                //            INNER JOIN GOPER ON GTIPOPERREPORT.CODTIPOPER = GOPER.CODTIPOPER
                //            INNER JOIN GREPORT ON GTIPOPERREPORT.CODREPORT = GREPORT.CODREPORT
                //            WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ? ", new object[] { Convert.ToInt32(row["GNFESTADUAL.CODOPER"]), AppLib.Context.Empresa }).ToString();
                //if (!string.IsNullOrEmpty(nomeRelatorio))
                //{
                //    PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByDataRow(row, "GNFESTADUAL.CODEMPRESA");
                //    PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByDataRow(row, "GNFESTADUAL.CODOPER");
                //    PS.Lib.DataField dfPROTOCOLO = gb.RetornaDataFieldByDataRow(row, "GNFESTADUAL.PROTOCOLO");
                //    if (dfPROTOCOLO.Valor == null)
                //    {
                //        dfPROTOCOLO.Valor = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT PROTOCOLO FROM GNFESTADUAL WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { dfCODOPER.Valor, dfCODEMPRESA.Valor }).ToString();
                //    }
                //    List<PS.Lib.DataField> Param = new List<PS.Lib.DataField>();
                //    Param.Add(dfCODEMPRESA);
                //    Param.Add(dfCODOPER);
                //    Param.Add(dfPROTOCOLO);

                //    switch (nomeRelatorio)
                //    {
                //        case "StReportDanfePaisagem":
                //            PS.Lib.WinForms.Report.ReportDesignTool rp = new PS.Lib.WinForms.Report.ReportDesignTool(new Relatorios.XrDanfePaisagem(Param));
                //            rp.ShowPreviewDialog();
                //            break;
                //        case "StReportDanfe":
                //            PS.Lib.WinForms.Report.ReportDesignTool rr = new PS.Lib.WinForms.Report.ReportDesignTool(new Relatorios.XrDanfe(Param));
                //            rr.ShowPreviewDialog();
                //            break;
                //        default:
                //            break;
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Nenhum relatório selecionado no parâmetro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                //return;
                #endregion

                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                string TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();
                string Caminho = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT PASTADESTINO FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODFILIAL"] }).ToString();

                if (row["GNFESTADUAL.CODSTATUS"].ToString() == "A")
                {
                    Token = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT GFILIAL.TOKEN
                                                                                                   FROM GOPER
                                                                                                   INNER JOIN GFILIAL ON GOPER.CODEMPRESA = GFILIAL.CODEMPRESA AND GOPER.CODFILIAL = GFILIAL.CODFILIAL
                                                                                                   WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ? AND GOPER.CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"], row["GOPER.CODFILIAL"] }).ToString();

                    string retorno = NfeAPI.DownloadNFeAndSave(Token, row["GNFESTADUAL.CHAVEACESSO"].ToString(), "P", TpAmb, Caminho, true);

                    dynamic JsonRetorno = JsonConvert.DeserializeObject(retorno);
                    string Status = JsonRetorno.status;

                    if (Status == "-999")
                    {
                        PS.Lib.Global gb = new Lib.Global();

                        string nomeRelatorio = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CLASSNAME FROM GTIPOPERREPORT 
                                    INNER JOIN GOPER ON GTIPOPERREPORT.CODTIPOPER = GOPER.CODTIPOPER
                                    INNER JOIN GREPORT ON GTIPOPERREPORT.CODREPORT = GREPORT.CODREPORT
                                    WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ? ", new object[] { Convert.ToInt32(row["GNFESTADUAL.CODOPER"]), AppLib.Context.Empresa }).ToString();
                        if (!string.IsNullOrEmpty(nomeRelatorio))
                        {
                            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByDataRow(row, "GNFESTADUAL.CODEMPRESA");
                            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByDataRow(row, "GNFESTADUAL.CODOPER");
                            PS.Lib.DataField dfPROTOCOLO = gb.RetornaDataFieldByDataRow(row, "GNFESTADUAL.PROTOCOLO");
                            if (dfPROTOCOLO.Valor == null)
                            {
                                dfPROTOCOLO.Valor = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT PROTOCOLO FROM GNFESTADUAL WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { dfCODOPER.Valor, dfCODEMPRESA.Valor }).ToString();
                            }
                            List<PS.Lib.DataField> Param = new List<PS.Lib.DataField>();
                            Param.Add(dfCODEMPRESA);
                            Param.Add(dfCODOPER);
                            Param.Add(dfPROTOCOLO);

                            switch (nomeRelatorio)
                            {
                                case "StReportDanfePaisagem":
                                    PS.Lib.WinForms.Report.ReportDesignTool rp = new PS.Lib.WinForms.Report.ReportDesignTool(new Relatorios.XrDanfePaisagem(Param));
                                    rp.ShowPreviewDialog();
                                    break;
                                case "StReportDanfe":
                                    PS.Lib.WinForms.Report.ReportDesignTool rr = new PS.Lib.WinForms.Report.ReportDesignTool(new Relatorios.XrDanfe(Param));
                                    rr.ShowPreviewDialog();
                                    break;
                                default:
                                    break;
                            }
                            return;
                        }
                    }

                    int ValidaCorrecao = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODOPER) FROM GNFESTADUALCORRECAO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) }));

                    if (ValidaCorrecao > 0)
                    {
                        string NseqEvento = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT MAX(NSEQITEM) FROM GNFESTADUALCORRECAO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) }).ToString();
                        System.Threading.Thread.Sleep(1000);

                        Token = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT GFILIAL.TOKEN
                                                                                                   FROM GOPER
                                                                                                   INNER JOIN GFILIAL ON GOPER.CODEMPRESA = GFILIAL.CODEMPRESA AND GOPER.CODFILIAL = GFILIAL.CODFILIAL
                                                                                                   WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ? AND GOPER.CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"], row["GOPER.CODFILIAL"] }).ToString();

                        string retornoEvento = NfeAPI.DownloadNFeAndSaveEvento(Token, row["GNFESTADUAL.CHAVEACESSO"].ToString(), "P", TpAmb, "CCE", NseqEvento, Caminho, true);
                    }

                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET DANFEIMPRESSA = 1 WHERE CODEMPRESA= ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) });
                }
                else if (row["GNFESTADUAL.CODSTATUS"].ToString() == "C")
                {
                    Token = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT GFILIAL.TOKEN
                                                                                                   FROM GOPER
                                                                                                   INNER JOIN GFILIAL ON GOPER.CODEMPRESA = GFILIAL.CODEMPRESA AND GOPER.CODFILIAL = GFILIAL.CODFILIAL
                                                                                                   WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ? AND GOPER.CODFILIAL = ?", new object[] { AppLib.Context.Empresa, row["GOPER.CODOPER"], row["GOPER.CODFILIAL"] }).ToString();

                    string retorno = NfeAPI.DownloadNFeAndSave(Token, row["GNFESTADUAL.CHAVEACESSO"].ToString(), "P", TpAmb, Caminho, true);
                    System.Threading.Thread.Sleep(1000);

                    int ValidaCorrecao = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODOPER) FROM GNFESTADUALCORRECAO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) }));

                    if (ValidaCorrecao > 0)
                    {
                        string NseqEvento = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NSEQITEM FROM GNFESTADUALCORRECAO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) }).ToString();
                        System.Threading.Thread.Sleep(1000);

                        string retornoEventoCorrecao = NfeAPI.DownloadNFeAndSaveEvento(Token, row["GNFESTADUAL.CHAVEACESSO"].ToString(), "P", TpAmb, "CCE", NseqEvento, Caminho, true);
                    }

                    string retornoEvento = NfeAPI.DownloadNFeAndSaveEvento(Token, row["GNFESTADUAL.CHAVEACESSO"].ToString(), "P", TpAmb, "CANC", "1", Caminho, true);
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET DANFEIMPRESSA = 1 WHERE CODEMPRESA= ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["GNFESTADUAL.CODOPER"]) });
                }
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para exportar o XML.
        /// </summary>
        /// <param name="Codoper">Código da Operação selecioanada</param>
        /// <param name="Caminho">Caminho selecionado pelo usuário</param>
        /// <returns>True, caso o XML seja exportado com êxito/False, caso o XML for nulo</returns>
        private bool ExportarXML(int Codoper, string Caminho)
        {
            string XML = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT XMLNFE FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper }).ToString();
            string ChaveAcesso = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CHAVEACESSO FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper }).ToString();

            if (string.IsNullOrEmpty(XML))
            {
                return false;
            }
            else
            {
                int indexi = XML.IndexOf('<', 0);
                int indexf = XML.IndexOf('>', 0);

                string substituir = XML.Substring(indexi, indexf + 1);

                if (substituir.Contains("xml version"))
                {
                    XML = XML.Replace(substituir, "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                }
                else
                {
                    XML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + XML;
                }

                string Path = Caminho + "\\" + "Nfe" + ChaveAcesso + ".xml";

                System.IO.File.WriteAllText(Path, XML);
                return true;
            }
        }

        /// <summary>
        /// Método para exclusão do Saldo do Estoque
        /// </summary>
        /// <param name="conn">Conexão ativa</param>
        /// <param name="Codoper">Código da Operação</param>
        /// <param name="DataOper">Data de Emissão da Operação</param>
        /// <param name="Codfilial">Código da Filial</param>
        private void ExcluiSaldoEstoque(AppLib.Data.Connection conn, int Codoper, DateTime DataOper, int Codfilial)
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
        /// Método responsável pela exclusão do Financeiro 
        /// </summary>
        /// <param name="conn">Conexão ativa</param>
        /// <param name="Codoper">código da Operação</param>
        /// <returns></returns>
        private bool excluiFinanceiro(AppLib.Data.Connection conn, int Codoper)
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

        /// <summary>
        /// Método para atualizar a tabela GNFESTADUALEVENTO
        /// </summary>
        /// <param name="Codoper">Código da Operação</param>
        /// <param name="TipoEvento">Tipo do Evento</param>
        /// <param name="Justifcativa">Justificativa do Cancelamento</param>
        /// <param name="StatusNS">Status NS</param>
        private void AtualizaGNFESTADUALEVENTO(int Codoper, string TipoEvento, string StatusNS)
        {
            string StatusEvento = string.Empty;

            StatusEvento = (StatusNS != "200") ? "E" : "A";

            int ValidaEvento = (int)AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(IDEVENTO) FROM GNFESTADUALEVENTO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper });

            if (ValidaEvento > 0)
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUALEVENTO SET DATA= ?, TPEVENTO = ?, CODSTATUS = ? WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { DateTime.Now, TipoEvento, StatusEvento, AppLib.Context.Empresa, Codoper });
            }
        }

        /// <summary>
        /// Método para realizar o INSERT na tabela GNFESTADUALCANC
        /// </summary>
        /// <param name="Codoper">Código da Operação</param>
        /// <param name="Protocolo">Protocolo</param>
        /// <param name="DataProtocolo">Data do Protocolo</param>
        /// <param name="XMLEvento">XML do evento consultado</param>
        private void InsertGNFESTADUALCANC(int Codoper, string Protocolo, DateTime DataProtocolo, string XMLEvento)
        {
            string MotivoCancelamento = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT MOTIVO FROM GMOTIVOCANCELAMENTO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper }).ToString();

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppLib.Context.poolConnection.Get("Start").ConnectionString);
            System.Data.SqlClient.SqlCommand command;

            try
            {
                conn.Open();

                command = new System.Data.SqlClient.SqlCommand("INSERT INTO GNFESTADUALCANC (CODEMPRESA, CODOPER, CODSTATUS, MOTIVO, PROTOCOLO, DATAPROTOCOLO, XMLENV, XMLPROT) VALUES (" + AppLib.Context.Empresa + ", " + Codoper + ", 'CAN', '" + MotivoCancelamento + "', '" + Protocolo + "', '" + DataProtocolo.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + XMLEvento + "', '')", conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método que realiza INSERT na tabela GNFESTADUALCORRECAO
        /// </summary>
        /// <param name="Codoper">Código da Operação</param>
        /// <param name="Protocolo">Número do Protocolo</param>
        /// <param name="DataProtocolo">Data do Protocolo</param>
        /// <param name="Correcao">Justificativa da correção</param>
        /// <param name="XMLEvento">XML do evento da correção</param>
        private void InsertGNFESTADUALCORRECAO(int Codoper, int NseqEvento, string Protocolo, DateTime DataProtocolo, string Correcao, string XMLEvento)
        {
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppLib.Context.poolConnection.Get("Start").ConnectionString);
            System.Data.SqlClient.SqlCommand command;

            try
            {
                conn.Open();

                command = new System.Data.SqlClient.SqlCommand("INSERT INTO GNFESTADUALCORRECAO (CODEMPRESA, CODOPER, NSEQITEM ,CODSTATUS, MOTIVO, PROTOCOLO, DATAPROTOCOLO, XMLENV, XMLPROT) VALUES (" + AppLib.Context.Empresa + ", " + Codoper + ", " + NseqEvento + ", 'COR', '" + Correcao + "','" + Protocolo + "','" + DataProtocolo.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + XMLEvento + "', '')", conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Validações 

        /// <summary>
        /// Método para validação dos Status
        /// </summary>
        /// <param name="row">Registro selecionado</param>
        /// <returns>True/False</returns>
        private bool ValidaStatusInutilizacao(DataRow row)
        {
            switch (row["GNFESTADUAL.CODSTATUS"].ToString())
            {
                case "A":
                    return false;
                case "C":
                    return false;
                case "D":
                    return false;
                case "F":
                    return false;
                case "N":
                    return false;
                case "P":
                    return false;
                case "U":
                    return false;
                default:
                case "":
                    return true;
            }
        }

        /// <summary>
        /// Método para verificar a permissão de cancelamento da Operação
        /// </summary>
        /// <returns>Permissão de cancelamento da Operação</returns>
        private bool PermissaoOperacao()
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
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método para verificar a permissão do usuário para cancelamento da Operação
        /// </summary>
        /// <param name="CodtipOper">Código do tipo de Operação</param>
        /// <returns>Permissão de cancelamento para o usuário</returns>
        private bool PermissaoUsuario(string CodtipOper)
        {
            bool permite = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT 
        CANCELAR 
        FROM 
        GPERFILTIPOPER 
        INNER JOIN GUSUARIOPERFIL ON GPERFILTIPOPER.CODPERFIL = GUSUARIOPERFIL.CODPERFIL AND GPERFILTIPOPER.CODEMPRESA = GUSUARIOPERFIL.CODEMPRESA 
        WHERE 
        GUSUARIOPERFIL.CODUSUARIO = ?
        AND GUSUARIOPERFIL.CODEMPRESA = ? 
        AND GPERFILTIPOPER.CODTIPOPER = ?", new object[] { AppLib.Context.Usuario, AppLib.Context.Empresa, CodtipOper }));

            if (permite == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Inutilização

        public void CarregaGridInutilizacao()
        {
            try
            {

                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT IDENTIFICADOR AS 'Identificador', 
                                                                                             CODFILIAL AS 'Código da Filial', 
                                                                                             SERIE AS 'Série',
                                                                                             NUMERONFE AS 'Número NF-e', 
                                                                                             DATA AS 'Data Inutilização', 
                                                                                             TPAMB AS 'Tipo de Ambiente', 
                                                                                             PROTOCOLO AS 'Protocolo', 
                                                                                             MOTIVO AS 'Motivo',
                                                                                             JUSTIFICATIVA AS 'Justificatva',
                                                                                             CODUSUARIO AS 'Usuário'                                                                                  
                                                                                             FROM GNFESTADUALINUT", new object[] { });

                gridControl3.DataSource = dt;
                gridView3.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Método para exportar o XML.
        /// </summary>
        /// <param name="Identificador">Código da Operação selecioanada</param>
        /// <param name="Caminho">Caminho selecionado pelo usuário</param>
        /// <returns>True, caso o XML seja exportado com êxito/False, caso o XML for nulo</returns>
        private bool ExportarXMLInutilizacao(int Identificador, string Caminho)
        {
            string XML = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT XMLINUT FROM GNFESTADUALINUT WHERE CODEMPRESA = ? AND IDENTIFICADOR = ?", new object[] { AppLib.Context.Empresa, Identificador }).ToString();
            string Numero = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NUMERONFE FROM GNFESTADUALINUT WHERE CODEMPRESA = ? AND IDENTIFICADOR = ?", new object[] { AppLib.Context.Empresa, Identificador }).ToString();

            if (string.IsNullOrEmpty(XML))
            {
                return false;
            }
            else
            {
                int indexi = XML.IndexOf('<', 0);
                int indexf = XML.IndexOf('>', 0);

                string substituir = XML.Substring(indexi, indexf + 1);

                if (substituir.Contains("xml version"))
                {
                    XML = XML.Replace(substituir, "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                }
                else
                {
                    XML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + XML;
                }

                string Path = Caminho + "\\" + "Nfe" + Numero + ".xml";

                System.IO.File.WriteAllText(Path, XML);
                return true;
            }
        }

        private void btnPesquisarInut_Click(object sender, EventArgs e)
        {
            if (gridView3.OptionsFind.AlwaysVisible == true)
            {
                gridView3.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView3.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnAgruparInut_Click(object sender, EventArgs e)
        {
            if (gridView3.OptionsView.ShowGroupPanel == true)
            {
                gridView3.OptionsView.ShowGroupPanel = false;
                gridView3.ClearGrouping();
                btnAgruparInut.Text = "Agrupar";
            }
            else
            {
                gridView3.OptionsView.ShowGroupPanel = true;
                btnAgruparInut.Text = "Desagrupar";
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int Enviados = 0, NaoEnviados = 0;

            if (MessageBox.Show("Deseja exportar o(s) registro(s) selecionado(s)?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();

                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        for (int i = 0; i < gridView3.SelectedRowsCount; i++)
                        {
                            DataRow row1 = gridView3.GetDataRow(Convert.ToInt32(gridView3.GetSelectedRows().GetValue(i).ToString()));

                            if (ExportarXMLInutilizacao(Convert.ToInt32(row1["Identificador"]), fbd.SelectedPath) == true)
                            {
                                Enviados++;
                            }
                            else
                            {
                                NaoEnviados++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                MessageBox.Show("Operação Realizada com Sucesso. \nQuantidade de NFe Exportadas: " + Enviados + "\nQuantidade de NFe não Exportada: " + NaoEnviados + "\n", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion
    }
}
