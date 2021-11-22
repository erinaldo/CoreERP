using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Glb.New.Visao
{
    public partial class frmVisaoDDFe : Form
    {
        public Form pai = null;
        public bool consulta = false;
        string tabela = "GDDFE";
        string query = string.Empty;
        public string codMenu = string.Empty;

        Class.DDFeAPI DDFe = new Class.DDFeAPI();

        public frmVisaoDDFe(string _query, Form frmprin, string _CodMenu)
        {
            InitializeComponent();
            codMenu = _CodMenu;
            this.MdiParent = frmprin;
            query = _query;
            CarregaGrid(query);
            getAcesso(codMenu);
        }

        private void frmVisaoDDFe_Load(object sender, EventArgs e)
        {
            toolStripDropDownButton4.Enabled = false;
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
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["CONSULTA"]) == true)
                    {
                        btnEditar.Enabled = true;
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

                string sql = string.Empty;

                sql = new Class.Utilidades().getVisao(tabela, string.Empty, TabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

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

        private void eventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabEventos.PageVisible = true;
            splitContainer1.Panel2Collapsed = false;

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT IDEVENTO AS 'ID Evento', CHAVE AS 'Chave', TPEVENTO AS 'Tipo do Evento', DESCRICAOEVENTO AS 'Descrição', NPROT AS 'Protocólo', XML AS 'XML', DATAEVENTO AS 'Data do Evento' FROM GDDFEEVENTO WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });

            gridControl2.DataSource = dt;
            gridView2.BestFitColumns();
        }

        private void fecharAnexosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabEventos.PageVisible = false;
            splitContainer1.Panel2Collapsed = true;
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
            Filtro.frmFiltroDDFe DDFe = new Filtro.frmFiltroDDFe();
            DDFe.aberto = true;
            DDFe.ShowDialog();
            if (!string.IsNullOrEmpty(DDFe.condicao))
            {
                query = DDFe.condicao;
                CarregaGrid(query);
            }
        }

        private void atualizaColuna(DataRow dr)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDDFE WHERE CODEMPRESA = ? AND NSU = ?", new object[] { AppLib.Context.Empresa, dr["GDDFE.NSU"] });

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
                PS.Glb.New.Cadastros.frmCadastroDDFe DDFe = new Cadastros.frmCadastroDDFe();
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                DDFe.NSU = Convert.ToInt32(row1["GDDFE.NSU"]);
                DDFe.edita = true;
                DDFe.ShowDialog();
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

        private void downloadManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PS.Glb.New.DDFe.frmDownloadManual frm = new New.DDFe.frmDownloadManual();
            frm.ShowDialog();

            CarregaGrid(query);
        }

        private void downloadEmLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PS.Glb.New.DDFe.frmDownloadLote frm = new New.DDFe.frmDownloadLote();
            frm.ShowDialog();

            CarregaGrid(query);
        }

        private void manifestarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PS.Glb.New.DDFe.frmManifesto frm = new New.DDFe.frmManifesto();
            frm.ShowDialog();

            CarregaGrid(query);
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

                            if (ExportarXML(Convert.ToInt32(row1["GDDFE.NSU"]), fbd.SelectedPath) == true)
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

        private void imprirmirDanfeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region Código comentado 

            //PS.Lib.Global gb = new Lib.Global();
            //DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            //string nomeRelatorio = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CLASSNAME FROM GTIPOPERREPORT 
            //                        INNER JOIN GOPER ON GTIPOPERREPORT.CODTIPOPER = GOPER.CODTIPOPER
            //                        INNER JOIN GREPORT ON GTIPOPERREPORT.CODREPORT = GREPORT.CODREPORT
            //                        WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ? ", new object[] { Convert.ToInt32(row["GNFESTADUAL.CODOPER"]), AppLib.Context.Empresa }).ToString();
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
            //    return;
            //}

            #endregion

            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            string Caminho = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT PASTADESTINO FROM GFILIAL WHERE CODFILIAL = ?", new object[] { AppLib.Context.Filial }).ToString();

            try
            {
                System.Diagnostics.Process.Start(Caminho + "\\" + row["GDDFE.CHAVE"].ToString() + "-procDDfe.pdf");
            }
            catch 
            {
                MessageBox.Show("O arquivo não foi encontrado para impressão, favor consultar a pasta " + Caminho + ".", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        #region Métodos

        /// <summary>
        /// Método para exportar o XML.
        /// </summary>
        /// <param name="Codoper">Código da Operação selecioanada</param>
        /// <param name="Caminho">Caminho selecionado pelo usuário</param>
        /// <returns>True, caso o XML seja exportado com êxito/False, caso o XML for nulo</returns>
        private bool ExportarXML(int NSU, string Caminho)
        {
            string XML = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT XMLRECEBIDO FROM GDDFE WHERE CODEMPRESA = ? AND NSU = ?", new object[] { AppLib.Context.Empresa, NSU }).ToString();
            string Chave = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CHAVE FROM GDDFE WHERE CODEMPRESA = ? AND NSU = ?", new object[] { AppLib.Context.Empresa, NSU }).ToString();

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

                string Path = Caminho + "\\" + "Nfe" + Chave + ".xml";

                System.IO.File.WriteAllText(Path, XML);
                return true;
            }
        }

        #endregion

        private void incluirOperaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PSSelTipoOperacao psSelTipoOperacao = new PSSelTipoOperacao();
            psSelTipoOperacao.Tipo = 2;

            psSelTipoOperacao.ShowDialog();
        }
    }
}
