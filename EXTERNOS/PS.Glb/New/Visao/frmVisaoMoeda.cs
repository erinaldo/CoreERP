﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITGProducao.Controles;

namespace PS.Glb.New.Visao
{
    public partial class frmVisaoMoeda : Form
    {
        public Form pai = null;
        public bool consulta = false;
        string tabela = "VMOEDA";
        string query = string.Empty;
        public string codMenu = string.Empty;
        private bool permiteEditar = true;

        List<string> tabelasFilhas = new List<string>();

        //Variaveis para usar quando a tela abre para consulta.
        public string codMoeda;
        public string Nome;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmVisaoMoeda(string _query, Form frmprin, string _CodMenu)
        {
            InitializeComponent();
            codMenu = _CodMenu;
            this.MdiParent = frmprin;
            tabelasFilhas.Clear();
            query = _query;
            carregaGrid(query, false);
            getAcesso(codMenu);
        }
        public frmVisaoMoeda(string _where, bool _consulta)
        {
            InitializeComponent();
            tabelasFilhas.Clear();
            query = _where;
            consulta = _consulta;
            carregaGrid(query, _consulta);
        }
        public frmVisaoMoeda(ref NewLookup lookup)
        {
            InitializeComponent();
            query = lookup.whereVisao;
            carregaGrid(query, false);
            this.lookup = lookup;
        }
        private void frmVisaoMoeda_Load(object sender, EventArgs e)
        {
            btnFiltros.Enabled = false;
            btnAnexos.Enabled = false;
            btnProcessos.Enabled = false;
            btnExportar.Enabled = false;
        }
        private void getAcesso(string CodMenu)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO, EXCLUSAO, CONSULTA, INCLUSAO FROM GPERMISSAOMENU WHERE CODMENU = ? AND CODPERFIL = ?", new object[] { "btnCadastrosGlobais_MoedaseIndices", AppLib.Context.Perfil });
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
        public void carregaGrid(string where, bool consulta)
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
                    dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VMOEDA WHERE TABELA = ?", new object[] { tabelasFilhas[i].ToString() });
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
        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            carregaGrid(query, false);
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            carregaGrid(query, false);
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
                carregaGrid(query, false);
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
        private void atualizaColuna(DataRow dr)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VMOEDA WHERE CODMOEDA = ? AND CODEMPRESA = ?", new object[] { dr["VMOEDA.CODMOEDA"], AppLib.Context.Empresa });

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
                    New.Cadastros.frmCadastroMoeda moeda = new Cadastros.frmCadastroMoeda();
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    moeda.codMoeda = row1["VMOEDA.CODMOEDA"].ToString();
                    moeda.edita = true;
                    moeda.codMenu = codMenu;
                    moeda.ShowDialog();
                    atualizaColuna(row1);
                }
            }
            else
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                lookup.txtconteudo.Text = row1[tabela + ".NOME"].ToString().ToUpper();
                lookup.txtcodigo.Text = row1[tabela + ".CODMOEDA"].ToString();
                lookup.ValorCodigoInterno = row1[tabela + ".CODMOEDA"].ToString();
                this.Dispose();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                New.Cadastros.frmCadastroMoeda moeda = new Cadastros.frmCadastroMoeda();
                moeda.edita = false;
                moeda.ShowDialog();
                carregaGrid(query, consulta);
            }
            else
            {
                New.Cadastros.frmCadastroMoeda moeda = new Cadastros.frmCadastroMoeda(ref this.lookup);
                moeda.edita = false;
                moeda.ShowDialog();
                this.Dispose();
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            Atualizar();
        }

        private void gridControl1_DoubleClick_1(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (consulta == true)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    codMoeda = row1["VMOEDA.CODMOEDA"].ToString();
                    Nome = row1["VMOEDA.NOME"].ToString();
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
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir os registros selecionados?", "Confirmação.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                        if (new Class.Utilidades().Excluir("CODMOEDA", "VMOEDA", row["VMOEDA.CODMOEDA"].ToString()) == true)
                        {
                            if (gridView1.SelectedRowsCount > 0)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Não foi possível excluir registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    MessageBox.Show("Registro excluído com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    carregaGrid(query, false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
