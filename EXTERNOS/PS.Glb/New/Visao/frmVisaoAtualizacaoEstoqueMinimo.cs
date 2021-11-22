using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace PS.Glb.New.Visao
{
    public partial class frmVisaoAtualizacaoEstoqueMinimo : Form
    {
        // Variaveis
        public bool consulta = false;
        string query = string.Empty;
        string Codfilial = string.Empty;
        private DataTable dtQuery;
        public string Codquery = string.Empty;
        public string Formula = string.Empty;

        public string PrimeiroMes = string.Empty;
        public string SegundoMes = string.Empty;
        public string TerceiroMes = string.Empty;
        public string QuartoMes = string.Empty;

        public string PrimeiroAno = string.Empty;
        public string SegundoAno = string.Empty;
        public string TerceiroAno = string.Empty;
        public string QuartoAno = string.Empty;

        // Padrão
        string tabela = "VESTOQUEMINIMO";
        List<string> tabelasFilhas = new List<string>();

        public frmVisaoAtualizacaoEstoqueMinimo(string _codfilial, Form frmprin)
        {
            InitializeComponent();
            Codfilial = _codfilial;
            this.MdiParent = (Form)frmprin;
            //CarregaGridPadrao(query);
            tabelasFilhas.Clear();
        }

        private void frmVisaoAtualizacaoEstoqueMinimo_Load(object sender, EventArgs e)
        {
            CarregaGridPadrao(query);
            btnNovo.Visible = false;
            btnEditar.Visible = false;
            btnExcluir.Visible = false;
            btnFiltros.Visible = false;
            btnAnexos.Visible = false;
        }

        private void CarregaGridPadrao(string where)
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

                        // Bloqueio das linhas na visão 

                        if (gridView1.Columns[i].FieldName.ToString() == "VESTOQUEMINIMO.CODEMPRESA")
                        {
                            gridView1.Columns[i].OptionsColumn.ReadOnly = true;
                        }

                        if (gridView1.Columns[i].FieldName.ToString() == "VESTOQUEMINIMO.CODPRODUTO")
                        {
                            gridView1.Columns[i].OptionsColumn.ReadOnly = true;
                        }

                        if (gridView1.Columns[i].FieldName.ToString() == "VESTOQUEMINIMO.CODIGOAUXILIAR")
                        {
                            gridView1.Columns[i].OptionsColumn.ReadOnly = true;
                        }

                        if (gridView1.Columns[i].FieldName.ToString() == "VESTOQUEMINIMO.DESCRICAO")
                        {
                            gridView1.Columns[i].OptionsColumn.ReadOnly = true;
                        }

                        if (gridView1.Columns[i].FieldName.ToString() == "VESTOQUEMINIMO.CODUNIDADE")
                        {
                            gridView1.Columns[i].OptionsColumn.ReadOnly = true;
                        }

                        if (gridView1.Columns[i].FieldName.ToString() == "VESTOQUEMINIMO.SALDOATUAL")
                        {
                            gridView1.Columns[i].OptionsColumn.ReadOnly = true;
                        }

                        if (gridView1.Columns[i].FieldName.ToString() == "VESTOQUEMINIMO.ESTOQUEMINIMO")
                        {
                            gridView1.Columns[i].OptionsColumn.ReadOnly = true;
                        }

                        if (gridView1.Columns[i].FieldName.ToString() == "VESTOQUEMINIMO.CONSUMO")
                        {
                            gridView1.Columns[i].OptionsColumn.ReadOnly = true;
                        }

                        if (gridView1.Columns[i].FieldName.ToString() == "VESTOQUEMINIMO.PRIMEIROMES")
                        {
                            gridView1.Columns[i].Caption = RenomeiaQuartaColuna();
                            gridView1.Columns[i].OptionsColumn.ReadOnly = true;
                        }
                        if (gridView1.Columns[i].FieldName.ToString() == "VESTOQUEMINIMO.SEGUNDOMES")
                        {
                            gridView1.Columns[i].Caption = RenomeiaTerceiraColuna();
                            gridView1.Columns[i].OptionsColumn.ReadOnly = true;
                        }
                        if (gridView1.Columns[i].FieldName.ToString() == "VESTOQUEMINIMO.TERCEIROMES")
                        {
                            gridView1.Columns[i].Caption = RenomeiaSegundaColuna();
                            gridView1.Columns[i].OptionsColumn.ReadOnly = true;
                        }
                        if (gridView1.Columns[i].FieldName.ToString() == "VESTOQUEMINIMO.QUARTOMES")
                        {
                            gridView1.Columns[i].Caption = RenomeiaPrimeiraColuna();
                            gridView1.Columns[i].OptionsColumn.ReadOnly = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal FormataValor(DataRow row)
        {
            if (gridView1.FocusedValue != row["VESTOQUEMINIMO.ATUALIZAR"])
            {
                decimal Valor;

                Valor = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0,"SELECT ATUALIZAR FROM VESTOQUEMINIMO WHERE CODEMPRESA = ? AND CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, row["VESTOQUEMINIMO.CODPRODUTO"] }));
                return Valor;
                //SELECT ATUALIZAR FROM VESTOQUEMINIMO WHERE CODEMPRESA = ? AND CODIGOAUXILIAR =? AND CODPRODUTO = ?          
            }
            else
            {
                return 0;
            }
        }

        // João Pedro Luchiari 10/01/2018
        //private void VerificaFiltroGrid()
        //{
        //    if (this.gridView1.FilterPanelText != "")
        //    {

        //    }
        //}

        /// <summary>
        /// Método para buscar o código da Query e depois retorná-la.
        /// </summary>
        /// <returns>Retorna a Query desejada</returns>
        private string getQuery()
        {
            Codquery = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODQUERYESTOQUEMINIMO FROM VPARAMETROS WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();

            Formula = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT QUERY FROM GQUERY WHERE CODQUERY = ?", new object[] { Codquery }).ToString();

            Formula = Formula.Replace("@CODEMPRESA", "'" + AppLib.Context.Empresa + "'");

            return Formula;
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

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregaGridPadrao(query);
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            CarregaGridPadrao(query);
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
                CarregaGridPadrao(query);
            }
        }

        /// <summary>
        /// Retorna a primeira coluna de acordo com o primeiro mês e ano
        /// </summary>
        /// <returns></returns>
        private string RenomeiaPrimeiraColuna()
        {
            string PrimeiraColuna = PrimeiroMes + "/" + PrimeiroAno;
            return PrimeiraColuna;
        }

        /// <summary>
        /// Retorna a segunda coluna de acordo com o segundo mês e ano
        /// </summary>
        /// <returns></returns>
        private string RenomeiaSegundaColuna()
        {
            string SegundaColuna = SegundoMes + "/" + SegundoAno;
            return SegundaColuna;
        }

        /// <summary>
        /// Retorna a terceira coluna de acordo com o terceiro mês e ano
        /// </summary>
        /// <returns></returns>
        private string RenomeiaTerceiraColuna()
        {
            string TerceiraColuna = TerceiroMes + "/" + TerceiroAno;
            return TerceiraColuna;
        }

        /// <summary>
        /// Retorna a quarta coluna de acordo com o quarto mês e ano
        /// </summary>
        /// <returns></returns>
        private string RenomeiaQuartaColuna()
        {
            string QuartaColuna = QuartoMes + "/" + QuartoAno;
            return QuartaColuna;
        }

        private void VerificaRegistroSelecionado()
        {
            // Pega a coluna selecionada

            if (gridView1.FocusedColumn == gridView1.Columns["VESTOQUEMINIMO.ATUALIZAR"])
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VESTOQUEMINIMO SET ATUALIZAR = REPLACE('" + gridView1.FocusedValue + "',',','.') , SELECIONAR = 1 WHERE CODEMPRESA = '" + row1["VESTOQUEMINIMO.CODEMPRESA"] + "' AND CODPRODUTO = '" + row1["VESTOQUEMINIMO.CODPRODUTO"] + "'");

                //CarregaGridPadrao(query);
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            VerificaRegistroSelecionado();
        }

        private void selecionarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VESTOQUEMINIMO SET SELECIONAR = 1");
            CarregaGridPadrao(query);
        }

        private void atualizarEstoqueMínimoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja atualizar os produtos selecionados?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE VPRODUTO SET VPRODUTO.ESTOQUEMINIMO = VESTOQUEMINIMO.ATUALIZAR FROM VPRODUTO INNER JOIN VESTOQUEMINIMO ON VESTOQUEMINIMO.CODEMPRESA = VPRODUTO.CODEMPRESA AND VESTOQUEMINIMO.CODPRODUTO = VPRODUTO.CODPRODUTO WHERE VPRODUTO.CODEMPRESA = ? AND VESTOQUEMINIMO.SELECIONAR = 1 ", new object[] { AppLib.Context.Empresa });

                AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO VESTOQUEMINIMOLOG SELECT *, GETDATE(), '" + AppLib.Context.Usuario + "' FROM VESTOQUEMINIMO WHERE VESTOQUEMINIMO.SELECIONAR = 1");

                MessageBox.Show("Processo executado com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string reportPath = string.Empty;
            FolderBrowserDialog open = new FolderBrowserDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                reportPath = open.SelectedPath + "EstoqueMinimo.xlsx";
                DevExpress.XtraPrinting.XlsExportOptions xlsxOptions = new DevExpress.XtraPrinting.XlsExportOptions();
                xlsxOptions.ShowGridLines = true;
                xlsxOptions.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;
                xlsxOptions.ExportHyperlinks = true;
                xlsxOptions.SheetName = "EstoqueMinimo";
                gridView1.ExportToXlsx(reportPath);
                StartProcess(reportPath);
            }
        }

        private void gridView1_ColumnFilterChanged(object sender, EventArgs e)
        {
            //VerificaFiltroGrid();
        }

        private void gridView1_LostFocus(object sender, EventArgs e)
        {
            if (this.gridView1.FilterPanelText != "")
            {

            }
        }
    }
}



