using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITGProducao.Controles;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroNatureza : Form
    {
        string tabela = "VNATUREZAREGRATRIBUTACAO";
        string tabela2 = "VNATUREZATRIBUTO";
        string query = string.Empty;
        public bool consulta = false;
        public bool edita = false;
        public string CodNatureza = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroNatureza()
        {
            InitializeComponent();

            new Class.Utilidades().getDicionario(this, tabControl1, "VNATUREZA");
            new Class.Utilidades().criaCamposComplementares("VNATUREZACOMPL", tabCamposComplementares);

            CarregaGrid("WHERE CODNATUREZA = '" + CodNatureza + "'", false);
            CarregaOutraGrid("WHERE CODNATUREZA = '" + CodNatureza + "'", false);
            CarregaCombo();
        }
        public frmCadastroNatureza(ref NewLookup lookup)
        {
            InitializeComponent();

            new Class.Utilidades().getDicionario(this, tabControl1, "VNATUREZA");
            new Class.Utilidades().criaCamposComplementares("VNATUREZACOMPL", tabCamposComplementares);

            CarregaGrid("WHERE CODNATUREZA = '" + CodNatureza + "'", false);
            CarregaOutraGrid("WHERE CODNATUREZA = '" + CodNatureza + "'", false);
            this.edita = true;
            this.lookup = lookup;
        }
        private void frmCadastroNatureza_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                CarregaCampos();
                CarregaGrid("WHERE CODNATUREZA = '" + CodNatureza + "'", false);
                CarregaOutraGrid("WHERE CODNATUREZA = '" + CodNatureza + "'", false);
                tbCodNatureza.Enabled = false;

                // Regra de Tributação
                btnExcluir_Tributacao.Enabled = false;
                btnFiltros_Tributacao.Enabled = false;
                btnAnexos_Tributacao.Enabled = false;
                btnProcessos_Tributacao.Enabled = false;
                btnExportar_Tributacao.Enabled = false;
                //

                // Natureza Tributo
                btnExcluir_Natureza.Enabled = false;
                btnFiiltros_Natureza.Enabled = false;
                btnAnexos_Natureza.Enabled = false;
                btnProcessos_Natureza.Enabled = false; ;
                btnExportar_Natureza.Enabled = false;
                //
            }
            else
            {
                chkAtivo.Checked = true;
                gridControl1.Enabled = false;
                gridControl2.Enabled = false;
                toolStrip2.Enabled = false;
                toolStrip3.Enabled = false;
            }
        }
        private bool validacoes()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(tbCodNatureza.Text))
            {
                errorProvider1.SetError(tbCodNatureza, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }

        private void CarregaCombo()
        {
            #region Combobox
            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem("E", "Entrada"));
            list1.Add(new PS.Lib.ComboBoxItem("S", "Saida"));

            cbTipEntSai.DataSource = list1;
            cbTipEntSai.DisplayMember = "DisplayMember";
            cbTipEntSai.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = "O";
            list2[0].DisplayMember = "Outra";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = "V";
            list2[1].DisplayMember = "Venda/Industrialização";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[2].ValueMember = "RS";
            list2[2].DisplayMember = "Revenda Sem CST";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[3].ValueMember = "RC";
            list2[3].DisplayMember = "Revenda Com CST";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[4].ValueMember = "CC";
            list2[4].DisplayMember = "Consumo Contribuinte";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[5].ValueMember = "CN";
            list2[5].DisplayMember = "Consumo Não Contribuinte";

            cbClassVenda.DataSource = list2;
            cbClassVenda.DisplayMember = "DisplayMember";
            cbClassVenda.ValueMember = "ValueMember";

            #endregion
        }

        private void carregaObj(DataTable dt)
        {
            tbCodNatureza.Text = dt.Rows[0]["CODNATUREZA"].ToString();
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
            chkUltimoNivel.Checked = Convert.ToBoolean(dt.Rows[0]["ULTIMONIVEL"]);
            tbDescricaoInterna.Text = dt.Rows[0]["DESCRICAOINTERNA"].ToString();
            cbTipEntSai.SelectedValue = dt.Rows[0]["TIPENTSAI"];
            chkDentrodoEstado.Checked = Convert.ToBoolean(dt.Rows[0]["DENTRODOESTADO"]);
            lpMensagem.txtcodigo.Text = dt.Rows[0]["CODMENSAGEM"].ToString();
            lpMensagem.CarregaDescricao();
            lpIcms.txtcodigo.Text = dt.Rows[0]["IDREGRAICMS"].ToString();
            lpIcms.CarregaDescricao();
            lpIpi.txtcodigo.Text = dt.Rows[0]["IDREGRAIPI"].ToString();
            lpIpi.CarregaDescricao();
            cbClassVenda.SelectedValue = dt.Rows[0]["CLASSVENDA2"];
            chkContrinuinteIcms.Checked = Convert.ToBoolean(dt.Rows[0]["CONTRIBUINTEICMS"]);
            chkProdImportado.Checked = Convert.ToBoolean(dt.Rows[0]["PRODIMPORTADO"]);
            chkUtilizaSt.Checked = Convert.ToBoolean(dt.Rows[0]["UTILIZAST"]);
            chkDifalIcmsSt.Checked = Convert.ToBoolean(dt.Rows[0]["DIFALICMSST"]);
            chkDifalIcmsStEspecial.Checked = Convert.ToBoolean(dt.Rows[0]["DIFALICMSSTESPECIAL"]);
            chkusaDecretoConvenio.Checked = Convert.ToBoolean(dt.Rows[0]["USADECRETOCONVENIO"]);
            chkConstrucaoCivil.Checked = Convert.ToBoolean(dt.Rows[0]["CONSTRUCAOCIVIL"]);
            chkAutomotiva.Checked = Convert.ToBoolean(dt.Rows[0]["AUTOMOTIVA"]);
        }

        private void CarregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VNATUREZA WHERE CODNATUREZA = ?", new object[] { CodNatureza });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VNATUREZA WHERE CODNATUREZA = ?", new object[] { CodNatureza });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }

            carregaCamposComplementares();
        }

        private void carregaCamposComplementares()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VNATUREZACOMPL WHERE CODEMPRESA = ? AND CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, CodNatureza });
            DataTable dtColunas = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT NOMECAMPO FROM GTABCAMPOCOMPL WHERE CODENTIDADE = ? AND ATIVO = ? ORDER BY ORDEM", new object[] { "VNATUREZACOMPL", 1 });
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

        #region Regra de Tributação

        private void btnNovo_Tributacao_Click(object sender, EventArgs e)
        {
            using (PS.Glb.New.Cadastros.frmCadastroRegraTributacao regra = new frmCadastroRegraTributacao())
            {
                regra._Codnatureza = tbCodNatureza.Text;
                regra.ShowDialog();
                CodNatureza = tbCodNatureza.Text;
                CarregaGrid("WHERE CODNATUREZA = '" + CodNatureza + "'", false);
            }
        }

        private void btnEditar_Tributacao_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    PS.Glb.New.Cadastros.frmCadastroRegraTributacao regra = new Cadastros.frmCadastroRegraTributacao();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    regra._Codnatureza = row1["VNATUREZAREGRATRIBUTACAO.CODNATUREZA"].ToString();
                    regra.NseqRegra = row1["VNATUREZAREGRATRIBUTACAO.NSEQREGRA"].ToString();
                    regra.edita = true;
                    regra.ShowDialog();
                    atualizaColuna(row1);
                }
                else
                {
                    PS.Glb.New.Cadastros.frmCadastroRegraTributacao regra = new Cadastros.frmCadastroRegraTributacao();
                    regra.edita = false;
                }
            }
            else
            {
                gridControl1_DoubleClick(sender, e);
            }
        }

        private void btnPesquisar_Tributacao_Click(object sender, EventArgs e)
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

        private void btnAgrupar_Tributacao_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsView.ShowGroupPanel == true)
            {
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.ClearGrouping();
                btnAgrupar_Tributacao.Text = "Agrupar";
            }
            else
            {
                gridView1.OptionsView.ShowGroupPanel = true;
                btnAgrupar_Tributacao.Text = "Desagrupar";
            }
        }

        public void CarregaGrid(string where, bool consulta)
        {
            try
            {
                string sql = string.Empty;

                sql = new Class.Utilidades().getVisao(tabela, string.Empty, tabelasFilhas, where);

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

                for (int i = 0; i < tabelasFilhas.Count; i++)
                {
                    dic = new DataTable();
                    dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VNATUREZAREGRATRIBUTACAO WHERE TABELA = ? AND CODNATUREZA =  ?", new object[] { tabelasFilhas[i].ToString(), CodNatureza });
                    dt = new DataTable();
                    dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabelasFilhas[i].ToString(), AppLib.Context.Usuario });
                    if (dt.Rows.Count > 0)
                    {
                        for (int ii = 0; ii < gridView1.Columns.Count; ii++)
                        {
                            gridView1.Columns[ii].Width = Convert.ToInt32(dt.Rows[ii]["LARGURA"]);
                            dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                            DataRow result = dic.Rows.Find(new object[] { gridView1.Columns[ii].FieldName.ToString() });
                            if (result != null)
                            {
                                gridView1.Columns[ii].Caption = result["DESCRICAO"].ToString();
                            }
                        }
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
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VNATUREZAREGRATRIBUTACAO WHERE CODNATUREZA = ? AND CODEMPRESA = ? AND NSEQREGRA = ?", new object[] { dr["VNATUREZAREGRATRIBUTACAO.CODNATUREZA"], AppLib.Context.Empresa, dr["VNATUREZAREGRATRIBUTACAO.NSEQREGRA"] });

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
            if (this.lookup == null)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    PS.Glb.New.Cadastros.frmCadastroRegraTributacao regra = new Cadastros.frmCadastroRegraTributacao();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    regra._Codnatureza = row1["VNATUREZAREGRATRIBUTACAO.CODNATUREZA"].ToString();
                    regra.NseqRegra = row1["VNATUREZAREGRATRIBUTACAO.NSEQREGRA"].ToString();
                    regra.edita = true;
                    regra.ShowDialog();
                    atualizaColuna(row1);
                }
            }
            else
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                lookup.txtconteudo.Text = row1[tabela + ".DESCRICAO"].ToString().ToUpper();
                lookup.txtcodigo.Text = row1[tabela + ".CODNATUREZA"].ToString();
                lookup.ValorCodigoInterno = row1[tabela + ".CODNATUREZA"].ToString();
                this.Dispose();
            }
        }

        private void Atualizar_Tributacao_Click(object sender, EventArgs e)
        {
            CarregaGrid("WHERE CODNATUREZA ='" + CodNatureza + "'", false);
        }

        private void btnSelecionarColunas_Tributacao_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            CarregaGrid("WHERE CODNATUREZA ='" + CodNatureza + "'", false);
        }

        private void btnSalavrLayout_Tributacao_Click(object sender, EventArgs e)
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
                CarregaGrid("WHERE CODNATUREZA ='" + CodNatureza + "'", false);
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (consulta == true)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    CodNatureza = row1["VNATUREZAREGRATRIBUTACAO.CODNATUREZA"].ToString();
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

        #region Tributos da Natureza

        private void btnNovo_Natureza_Click(object sender, EventArgs e)
        {
            using (PS.Glb.New.Cadastros.frmCadastroTributoNatureza natureza = new frmCadastroTributoNatureza())
            {
                natureza._Codnatureza = tbCodNatureza.Text;
                natureza.ShowDialog();
                CodNatureza = tbCodNatureza.Text;
                CarregaOutraGrid("WHERE CODNATUREZA = '" + CodNatureza + "'", false);
            }
        }

       

        private void btnEditar_Natureza_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (gridView2.SelectedRowsCount > 0)
                {
                    PS.Glb.New.Cadastros.frmCadastroTributoNatureza natureza = new Cadastros.frmCadastroTributoNatureza();
                    DataRow row1 = gridView2.GetDataRow(Convert.ToInt32(gridView2.GetSelectedRows().GetValue(0).ToString()));
                    natureza._Codnatureza = row1["VNATUREZATRIBUTO.CODNATUREZA"].ToString();
                    natureza.CodTributo = row1["VNATUREZATRIBUTO.CODTRIBUTO"].ToString();
                    natureza.edita = true;
                    natureza.ShowDialog();
                    atualizaColuna_Tributo(row1);
                }
                else
                {
                    PS.Glb.New.Cadastros.frmCadastroTributoNatureza natureza = new Cadastros.frmCadastroTributoNatureza();
                    natureza.edita = false;
                }
            }
            else
            {
                gridControl2_DoubleClick(sender, e);
            }
        }

        public void CarregaOutraGrid(string where, bool consulta)
        {
            try
            {
                string sql = string.Empty;

                sql = new Class.Utilidades().getVisao(tabela2, string.Empty, tabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridControl2.DataSource = null;
                gridView2.Columns.Clear();
                gridControl2.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela2 });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela2, AppLib.Context.Usuario });
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

                for (int i = 0; i < tabelasFilhas.Count; i++)
                {
                    dic = new DataTable();
                    dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VNATUREZATRIBUTO WHERE TABELA = ? AND CODNATUREZA = ?", new object[] { tabelasFilhas[i].ToString(), CodNatureza });
                    dt = new DataTable();
                    dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabelasFilhas[i].ToString(), AppLib.Context.Usuario });
                    if (dt.Rows.Count > 0)
                    {
                        for (int ii = 0; ii < gridView2.Columns.Count; ii++)
                        {
                            gridView2.Columns[ii].Width = Convert.ToInt32(dt.Rows[ii]["LARGURA"]);
                            dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                            DataRow result = dic.Rows.Find(new object[] { gridView2.Columns[ii].FieldName.ToString() });
                            if (result != null)
                            {
                                gridView2.Columns[ii].Caption = result["DESCRICAO"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgrupar_Natureza_Click(object sender, EventArgs e)
        {
            if (gridView2.OptionsView.ShowGroupPanel == true)
            {
                gridView2.OptionsView.ShowGroupPanel = false;
                gridView2.ClearGrouping();
                btnAgrupar_Natureza.Text = "Agrupar";
            }
            else
            {
                gridView2.OptionsView.ShowGroupPanel = true;
                btnAgrupar_Natureza.Text = "Desagrupar";
            }
        }

        private void btnPesquisar_Natureza_Click(object sender, EventArgs e)
        {
            if (gridView2.OptionsFind.AlwaysVisible == true)
            {
                gridView2.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView2.OptionsFind.AlwaysVisible = true;
            }
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (consulta == true)
                {
                    DataRow row1 = gridView2.GetDataRow(Convert.ToInt32(gridView2.GetSelectedRows().GetValue(0).ToString()));
                    CodNatureza = row1["VNATUREZATRIBUTO.CODNATUREZA"].ToString();
                    this.Dispose();
                }
                else
                {
                    Atualizar_Tributo();
                }
            }
            else
            {
                Atualizar_Tributo();
            }
        }

        private void Atualizar_Tributo()
        {
            if (this.lookup == null)
            {
                if (gridView2.SelectedRowsCount > 0)
                {
                    PS.Glb.New.Cadastros.frmCadastroTributoNatureza natureza = new Cadastros.frmCadastroTributoNatureza();
                    DataRow row1 = gridView2.GetDataRow(Convert.ToInt32(gridView2.GetSelectedRows().GetValue(0).ToString()));
                    natureza._Codnatureza = row1["VNATUREZATRIBUTO.CODNATUREZA"].ToString();
                    natureza.CodTributo = row1["VNATUREZATRIBUTO.CODTRIBUTO"].ToString();
                    natureza.edita = true;
                    natureza.ShowDialog();
                    atualizaColuna_Tributo(row1);
                }
            }
            else
            {
                DataRow row1 = gridView2.GetDataRow(Convert.ToInt32(gridView2.GetSelectedRows().GetValue(0).ToString()));
                lookup.txtconteudo.Text = row1[tabela2 + ".DESCRICAO"].ToString().ToUpper();
                lookup.txtcodigo.Text = row1[tabela2 + ".CODNATUREZA"].ToString();
                lookup.ValorCodigoInterno = row1[tabela2 + ".CODNATUREZA"].ToString();
                this.Dispose();
            }
        }

        private void atualizaColuna_Tributo(DataRow dr)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VNATUREZATRIBUTO WHERE CODNATUREZA = ? AND CODEMPRESA = ? AND CODTRIBUTO = ?", new object[] { dr["VNATUREZATRIBUTO.CODNATUREZA"], AppLib.Context.Empresa, dr["VNATUREZATRIBUTO.CODTRIBUTO"] });

            for (int i = 0; i < gridView2.VisibleColumns.Count; i++)
            {
                if (gridView2.Columns[i].ReadOnly == false)
                {
                    try
                    {
                        dr[gridView2.Columns[i].FieldName] = dr[gridView2.Columns[i].FieldName] = dt.Rows[0][gridView2.Columns[i].FieldName.ToString().Remove(0, gridView2.Columns[i].FieldName.ToString().IndexOf(".") + 1)].ToString();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        #endregion

        private bool SalvaCompl()
        {
            List<PS.Glb.Class.Parametro> param = new List<PS.Glb.Class.Parametro>();

            PS.Glb.Class.Parametro parametro = new PS.Glb.Class.Parametro();

            parametro.nomeParametro = "CODEMPRESA";
            parametro.valorParametro = AppLib.Context.Empresa.ToString();

            param.Add(parametro);

            parametro = new PS.Glb.Class.Parametro();

            parametro.nomeParametro = "CODNATUREZA";
            parametro.valorParametro = tbCodNatureza.Text;

            param.Add(parametro);

            PS.Glb.Class.Utilidades util = new PS.Glb.Class.Utilidades();

            if (tabCamposComplementares.Controls.Count > 0)
            {
                util.salvaCamposComplementares(this, "VNATUREZACOMPL", tabCamposComplementares, param, AppLib.Context.poolConnection.Get("Start"));
                return true;
            }
            else
            {
                return true;
            }
        }

        private bool Salvar()
        {
            if (validacoes() == false)
            {
                return false;
            }
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VNATUREZA = new AppLib.ORM.Jit(conn, "VNATUREZA");
            conn.BeginTransaction();

            try
            {
                VNATUREZA.Set("CODEMPRESA", AppLib.Context.Empresa);
                VNATUREZA.Set("CODNATUREZA", tbCodNatureza.Text);
                VNATUREZA.Set("DESCRICAO", tbDescricao.Text);
                VNATUREZA.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                VNATUREZA.Set("ULTIMONIVEL", chkUltimoNivel.Checked == true ? 1 : 0);
                VNATUREZA.Set("DESCRICAOINTERNA", tbDescricaoInterna.Text);
                VNATUREZA.Set("TIPENTSAI", cbTipEntSai.SelectedValue);
                VNATUREZA.Set("DENTRODOESTADO", chkDentrodoEstado.Checked == true ? 1 : 0);
                if (!string.IsNullOrEmpty(lpMensagem.ValorCodigoInterno))
                {
                    VNATUREZA.Set("CODMENSAGEM", lpMensagem.ValorCodigoInterno);
                }
                else
                {
                    VNATUREZA.Set("CODMENSAGEM", null);
                }
                if (!string.IsNullOrEmpty(lpIcms.ValorCodigoInterno))
                {
                    VNATUREZA.Set("IDREGRAICMS", lpIcms.ValorCodigoInterno);
                }
                else
                {
                    VNATUREZA.Set("IDREGRAICMS", null);
                }
                if (!string.IsNullOrEmpty(lpIpi.ValorCodigoInterno))
                {
                    VNATUREZA.Set("IDREGRAIPI", lpIpi.ValorCodigoInterno);
                }
                else
                {
                    VNATUREZA.Set("IDREGRAIPI", null);
                }
                VNATUREZA.Set("CLASSVENDA2", cbClassVenda.SelectedValue);
                VNATUREZA.Set("CONTRIBUINTEICMS", chkContrinuinteIcms.Checked == true ? 1 : 0);
                VNATUREZA.Set("PRODIMPORTADO", chkProdImportado.Checked == true ? 1 : 0);
                VNATUREZA.Set("UTILIZAST", chkUtilizaSt.Checked == true ? 1 : 0);
                VNATUREZA.Set("DIFALICMSST", chkDifalIcmsSt.Checked == true ? 1 : 0);
                VNATUREZA.Set("DIFALICMSSTESPECIAL", chkDifalIcmsStEspecial.Checked == true ? 1 : 0);
                VNATUREZA.Set("USADECRETOCONVENIO", chkusaDecretoConvenio.Checked == true ? 1 : 0);
                VNATUREZA.Set("CONSTRUCAOCIVIL", chkConstrucaoCivil.Checked == true ? 1 : 0);
                VNATUREZA.Set("AUTOMOTIVA", chkAutomotiva.Checked == true ? 1 : 0);

                VNATUREZA.Save();
                SalvaCompl();
                conn.Commit();

                return true;
            }
            catch (Exception ex)
            {
                conn.Rollback();
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (validacoes() == false)
                {
                    gridControl1.Enabled = false;
                    gridControl2.Enabled = false;
                }
                else
                {
                    Salvar();
                    gridControl1.Enabled = true;
                    gridControl2.Enabled = true;
                    toolStrip2.Enabled = true;
                    toolStrip3.Enabled = true;
                }
            }
            else
            {
                if (Salvar() == true)
                {
                    CodNatureza = tbCodNatureza.Text;
                    CarregaCampos();
                    this.Dispose();
                }
            }
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
