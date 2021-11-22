using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib.WinForms
{
    public partial class FrmBaseAppExport : DevExpress.XtraEditors.XtraForm
    {
        public ExportFileType FileType;
        public DataGridView Dados;

        public FrmBaseAppExport()
        {
            InitializeComponent();
        }

        private void FrmBaseApp_KeyDown(object sender, KeyEventArgs e)
        {
            Keys flag = e.KeyCode;

            if (flag.Equals(Keys.Enter))
            {
                SelectNextControl(ActiveControl, !e.Shift, true, true, true);
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBaseApp_Load(object sender, EventArgs e)
        {
            TabPage tab = new TabPage();

            tab = tabControl1.SelectedTab;

            tab.Text = string.Concat("Exportar para : ",FileType.ToString());

            this.Text = tab.Text;

            comboBox1.SelectedIndex = 0;

            checkBox1.CheckState = CheckState.Checked;

            if (FileType == ExportFileType.CSV)
            {
                comboBox1.Enabled = true;
                checkBox1.Enabled = true;
            }

            if (FileType == ExportFileType.Excel)
            {
                comboBox1.Enabled = false;
                checkBox1.Enabled = true;
            }

            if (FileType == ExportFileType.PDF)
            {
                comboBox1.Enabled = false;
                checkBox1.Enabled = true;                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (PSMessageBox.ShowQuestion("Confirma exportação dos dados ?") == DialogResult.Yes)
            {
                try
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                    if (FileType == ExportFileType.CSV)
                    {
                        saveFileDialog1.Filter = "CSV (*.csv)|*.csv";
                    }

                    if (FileType == ExportFileType.Excel)
                    {
                        saveFileDialog1.Filter = "XLS (Pasta de Trabalho do Excel)(*.xls)|*.xls";
                    }

                    if (FileType == ExportFileType.PDF)
                    {
                        saveFileDialog1.Filter = "PDF (*.pdf)|*.pdf";
                    }

                    // define delimitador
                    string delimitador = "";

                    if (comboBox1.Text == "Tab")
                    {
                        delimitador = "\t";
                    }
                    else
                    {
                        delimitador = comboBox1.Text;
                    }

                    saveFileDialog1.FilterIndex = 2;
                    saveFileDialog1.RestoreDirectory = true;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        this.Cursor = Cursors.WaitCursor;

                        progressBar1.Maximum = Dados.Rows.Count;

                        if (FileType == ExportFileType.CSV)
                        {
                            System.IO.StreamWriter Sw = new System.IO.StreamWriter(saveFileDialog1.FileName);

                            // cabeçalho
                            if (checkBox1.Checked)
                            {
                                for (int j = 1; j < Dados.Columns.Count; j++)
                                {
                                    Sw.Write(Dados.Columns[j].HeaderText);
                                    Sw.Write(delimitador);
                                }
                                Sw.Write(Sw.NewLine);
                            }

                            // conteudo
                            for (int i = 0; i < Dados.Rows.Count; i++)
                            {
                                for (int j = 1; j < Dados.Columns.Count; j++)
                                {
                                    Sw.Write(Dados.Rows[i].Cells[j].Value.ToString());
                                    Sw.Write(delimitador);
                                }
                                Sw.Write(Sw.NewLine);

                                progressBar1.Increment(1);
                            }
                            Sw.Close();
                        }

                        if (FileType == ExportFileType.Excel)
                        {
                            Microsoft.Office.Interop.Excel.Application wapp;
                            Microsoft.Office.Interop.Excel.Worksheet wsheet;
                            Microsoft.Office.Interop.Excel.Workbook wbook;

                            wapp = new Microsoft.Office.Interop.Excel.Application();

                            wapp.Visible = false;
                            wbook = wapp.Workbooks.Add(true);
                            wsheet = (Microsoft.Office.Interop.Excel.Worksheet)wbook.ActiveSheet;

                            // cabeçalho
                            if (checkBox1.Checked)
                            {
                                for (int i = 1; i < Dados.Columns.Count; i++)
                                {
                                    wsheet.Cells[1, i + 1] = Dados.Columns[i].HeaderText;
                                }
                            }

                            // conteudo
                            for (int i = 1; i < Dados.Rows.Count; i++)
                            {
                                DataGridViewRow row = Dados.Rows[i];
                                for (int j = 0; j < row.Cells.Count; j++)
                                {
                                    DataGridViewCell cell = row.Cells[j];
                                    try
                                    {
                                        wsheet.Cells[i + 2, j + 1] = (cell.Value == null) ? "" : cell.Value.ToString();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }

                                progressBar1.Increment(1);
                            }

                            wbook.Save();
                        }

                        if (FileType == ExportFileType.PDF)
                        {

                        }

                        this.Cursor = Cursors.Default;                    
                    }

                    PSMessageBox.ShowInfo("Exportação realizada com sucesso");
                }
                catch (Exception ex)
                {
                    PSMessageBox.ShowError(ex.Message);
                }
            }
        }
    }
}
