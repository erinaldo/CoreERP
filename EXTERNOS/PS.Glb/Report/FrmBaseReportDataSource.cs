using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PS.Lib;

namespace PS.Glb.Report
{
    public partial class FrmBaseReportDataSource : DevExpress.XtraEditors.XtraForm
    {
        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public DataSet Dados = new DataSet();

        private List<DataTable> ListaTabela = new List<DataTable>();

        public FrmBaseReportDataSource()
        {
            InitializeComponent();
        }

        private void CarregaParametros()
        {
            textBox1.Text = string.Empty;
            textBox1.Focus();

            tabControl1.TabIndex = 0;

            ListaTabela.Clear();

            Dados.DataSetName = "Dados";

            psBaseVisao1.psPart = new PS.Lib.WinForms.Query.PSPartQuery();
            psBaseVisao1.aplicativo = psBaseVisao1.psPart.PSPartApp;
            psBaseVisao1.CarregaRegistro(null);
        }

        private DataTable BuscaTabela()
        {
            string sSql = @"SELECT TABELA, DESCRICAO FROM GDICIONARIO WHERE COLUNA = '#' AND DESCRICAO LIKE ? ORDER BY TABELA";

            DataTable dt = dbs.QuerySelect(sSql, textBox1.Text);

            return dt;
        }

        private DataTable BuscaCampos(string Tabela)
        {
            string sSql = @"SELECT COLUNA, DESCRICAO FROM GDICIONARIO WHERE COLUNA <> '#' AND TABELA = ? ORDER BY COLUNA";

            DataTable dt = dbs.QuerySelect(sSql, Tabela);

            return dt;
        
        }

        private DataTable BuscaEstrutura(string Tabela)
        {
            string sSql = @"SELECT * FROM " + Tabela ;

            DataTable dt = dbs.QuerySelect(sSql);

            return dt;
        }

        private DataTable BuscaEstrutura(int codEmpresa, string codQuery)
        {
            string sSql = @"SELECT SENTENCA FROM GQUERYCOMPL WHERE CODEMPRESA = ? AND CODQUERY = ?";

            DataTable dt = dbs.QuerySelect(sSql, codEmpresa, codQuery);

            return dt;
        }

        private DataTable BuscaEstruturaQuery(DataTable dt)
        {
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Text = dt.Rows[0][0].ToString();

            DataTable dt1 = dbs.QuerySelect(richTextBox.Text);

            return dt1;
        }

        private void AtualizaListView()
        {
            DataTable dt = new DataTable();
            dt = BuscaCampos(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString());

            listView1.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string[] mItems = new string[] { dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString() };

                ListViewItem lvi = new ListViewItem(mItems);

                listView1.Items.Add(lvi);
            }
        }

        private void AtualizaGrid()
        {
            try
            {
                dataGridView1.DefaultCellStyle.SelectionBackColor = ct.corSelecaoGrid;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ct.corImparGrid;

                dataGridView1.DataSource = BuscaTabela();

                //AlteraNomeColuna();
                AtualizaListView();
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void FrmBaseReportDataSource_Load(object sender, EventArgs e)
        {
            CarregaParametros();
        }

        private void FrmBaseReportDataSource_KeyDown(object sender, KeyEventArgs e)
        {
            Keys flag = e.KeyCode;

            if (flag.Equals(Keys.Enter))
            {
                SelectNextControl(ActiveControl, !e.Shift, true, true, true);
                e.Handled = true;
            }

            if (flag.Equals(Keys.Space))
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    // Tabela
                    marcaDesmarcaToolStripMenuItem_Click(this, null);
                }
                else
                {
                    // Consulta SQL
                    psBaseVisao1.MarcaDesmarca();
                }
            }
        }

        private void marcaDesmarcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                int vSelecionado = dataGridView1.CurrentRow.Index;

                if (Convert.ToBoolean(dataGridView1.Rows[vSelecionado].Cells[0].Value))
                {
                    dataGridView1.Rows[vSelecionado].Cells[0].Value = false;
                }
                else
                {
                    dataGridView1.Rows[vSelecionado].Cells[0].Value = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            marcaDesmarcaToolStripMenuItem_Click(this, null);
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                AtualizaListView();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                // Tabela
                try
                {
                    Dados.Tables.Clear();

                    for (int i = 0; i < ListaTabela.Count; i++)
                    {
                        Dados.Tables.Add(ListaTabela[i].Clone());
                    }
                }
                catch (Exception ex)
                {
                    PSMessageBox.ShowError(ex.Message);
                }
            }
            else
            { 
                // Consulta SQL
                try
                {
                    Dados.Tables.Clear();

                    for (int i = 0; i < ListaTabela.Count; i++)
                    {
                        Dados.Tables.Add(ListaTabela[i].Clone());
                    }
                }
                catch (Exception ex)
                {
                    PSMessageBox.ShowError(ex.Message);
                }            
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    // Tabela
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value))
                        {
                            DataTable dt = BuscaEstrutura(dataGridView1.Rows[i].Cells[1].Value.ToString());
                            dt.TableName = dataGridView1.Rows[i].Cells[1].Value.ToString();

                            ListaTabela.Add(dt);
                        }
                    }
                }
                else
                {
                    // Consulta SQL
                    for (int i = 0; i < psBaseVisao1.dataGridView1.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(psBaseVisao1.dataGridView1.Rows[i].Cells[0].Value))
                        {
                            int codEmpresa = int.Parse(psBaseVisao1.dataGridView1.Rows[i].Cells[1].Value.ToString());
                            string codQuery = psBaseVisao1.dataGridView1.Rows[i].Cells[2].Value.ToString();

                            DataTable dt = BuscaEstrutura(codEmpresa, codQuery);

                            DataTable dt1 = BuscaEstruturaQuery(dt);
                            dt1.TableName = codQuery;

                            ListaTabela.Add(dt1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }
    }
}
