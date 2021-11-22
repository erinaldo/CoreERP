using ITGProducao.Controles;
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
    public partial class frmVisaoCliente : Form
    {
        string tabela = "VCLIFOR";
        string relacionamento = "LEFT OUTER JOIN GCIDADE ON VCLIFOR.CODCIDADE = GCIDADE.CODCIDADE";
        string query = string.Empty;
        List<string> tabelasFilhas = new List<string>();
        public string codMenu = string.Empty;
        private bool permiteEditar = true;
        public string lp_Codclifor = string.Empty;

        //Variaveis para usar quando a tela abre para consulta.
        public bool consulta = false;
        public string codCliente;
        public string nome;
        //////

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmVisaoCliente(string _query, Form frmprin, string _Codmenu)
        {
            InitializeComponent();
            this.MdiParent = (Form)frmprin;
            codMenu = _Codmenu;
            query = _query;
            carregaGrid(query);
            getAcesso(codMenu);
            splitContainer1.SplitterDistance = 30;
        }

        public frmVisaoCliente(string _where)
        {
            InitializeComponent();
            query = _where;
            carregaGrid(query);
        }
        public frmVisaoCliente(ref NewLookup lookup)
        {
            InitializeComponent();
            query = lookup.whereVisao;
            carregaGrid(query);
            this.lookup = lookup;
        }

        private void getAcesso(string CodMenu)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO, EXCLUSAO, CONSULTA, INCLUSAO FROM GPERMISSAOMENU WHERE CODMENU = ? AND CODPERFIL = ?", new object[] { "btnCadastrosGlobais_ClientesFornecedores", AppLib.Context.Perfil });
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

        public void carregaGrid(string where)
        {
            try
            {
                tabelasFilhas.Add("GCIDADE");
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
                //if (gridView1.Columns["VCLIFOR.FISICOJURIDICO"] != null)
                //{
                //    carregaImagem();
                //}
                carregaImagemClassificacao();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregaImagemClassificacao()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();

            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSTATUS FROM GSTATUS WHERE TABELA = ?", new object[] { tabela });

            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
                imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[i]["DESCRICAO"].ToString(), Convert.ToInt32(dtImagem.Rows[i]["CODSTATUS"]), i));
            }

            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns["VCLIFOR.CODCLASSIFICACAO"].ColumnEdit = imageCombo;

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            carregaGrid(query);
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            carregaGrid(query);
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
                    //FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
                carregaGrid(query);
            }

        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            Filtro.frmFiltroCliente frm = new Filtro.frmFiltroCliente();
            frm.aberto = true;
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.condicao))
            {
                query = frm.condicao;
                carregaGrid(query);
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                if (Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, @"SELECT COUNT(CODCLIFOR) FROM GOPER WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { row1["VCLIFOR.CODCLIFOR"].ToString(), AppLib.Context.Empresa })) == true)
                {

                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (gridView1.SelectedRowsCount > 1)
                {
                    MessageBox.Show("Selecione apenas um registro para edição.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (gridView1.SelectedRowsCount == 0)
                {
                    return;
                }
                else
                {
                    New.Cadastros.frmCadastroCliente frm = new New.Cadastros.frmCadastroCliente();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    frm.codCliFor = row1["VCLIFOR.CODCLIFOR"].ToString();
                    frm.edita = true;
                    frm.ShowDialog();
                    atualizaColuna(row1);
                }
            }
            else
            {
                gridControl1_DoubleClick(sender, e);
            }
        }


        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (consulta == true)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    codCliente = row1["VCLIFOR.CODCLIFOR"].ToString();
                    nome = row1["VCLIFOR.NOME"].ToString();
                    this.Dispose();
                    GC.Collect();
                }
                else
                {
                    btnEditar_Click(sender, e);
                }
            }
            else
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                lookup.txtconteudo.Text = row1[tabela + ".NOMEFANTASIA"].ToString().ToUpper();

                switch (lookup.CampoCodigo_Igual_a)
                {
                    case NewLookup.CampoCodigoIguala.CampoCodigoBD:
                        lookup.txtcodigo.Text = row1[tabela + ".CODCLIFOR"].ToString();
                        lookup.ValorCodigoInterno = row1[tabela + ".NOMEFANTASIA"].ToString();
                        break;

                    case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                        if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                        {
                            lookup.txtcodigo.Text = row1[tabela + "." + lookup.CampoCodigoInterno].ToString();
                            lookup.ValorCodigoInterno = row1[tabela + "." + lookup.CampoCodigoInterno].ToString();
                        }
                        else
                        {
                            lookup.txtcodigo.Text = row1[tabela + ".CODCLIFOR"].ToString();
                            lookup.ValorCodigoInterno = row1[tabela + ".NOMEFANTASIA"].ToString();
                        }
                        break;
                }

                this.Dispose();
            }
        }

        private void atualizaColuna(DataRow dr)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFOR WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { dr["VCLIFOR.CODCLIFOR"], AppLib.Context.Empresa });

            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                if (gridView1.Columns[i].ReadOnly == false)
                {
                    try
                    {
                        dr[gridView1.Columns[i].FieldName] = dt.Rows[0][gridView1.Columns[i].FieldName.ToString().Remove(0, gridView1.Columns[i].FieldName.ToString().IndexOf(".") + 1)].ToString();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                New.Cadastros.frmCadastroCliente frm = new New.Cadastros.frmCadastroCliente();
                frm.edita = false;
                frm.ShowDialog();
                carregaGrid(query);
            }
            else
            {
                New.Cadastros.frmCadastroCliente frm = new New.Cadastros.frmCadastroCliente(ref this.lookup);
                frm.edita = false;               
                frm.ShowDialog();

                this.Dispose();
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string reportPath = string.Empty;
            FolderBrowserDialog open = new FolderBrowserDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                reportPath = open.SelectedPath + "Clientes.xlsx";
                DevExpress.XtraPrinting.XlsExportOptions xlsxOptions = new DevExpress.XtraPrinting.XlsExportOptions();
                xlsxOptions.ShowGridLines = true;
                xlsxOptions.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;
                xlsxOptions.ExportHyperlinks = true;
                xlsxOptions.SheetName = "Clientes";
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

        private void btnHistoricoCliente_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 1)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                Anexos.frmCadastroHistoricoCliente frm = new Anexos.frmCadastroHistoricoCliente(row1["VCLIFOR.CODCLIFOR"].ToString());
                frm.ShowDialog();
                atualizaColuna(row1);
            }
            else
            {
                MessageBox.Show("Favor selecionar apenas um registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void toolStripDropDownButton4_Click(object sender, EventArgs e)
        {

        }
    }
}
