using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib.WinForms
{
    public partial class FrmBaseAppPrint : DevExpress.XtraEditors.XtraForm
    {
        public DataGridView Dados;
        public bool DisplaySelectedRow;

        private StringFormat strFormat; //Used to format the grid rows.
        private ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        private ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        private int iCellHeight = 0; //Used to get/set the datagridview cell height
        private int iTotalWidth = 0; //
        private int iRow = 0;//Used as counter
        private bool bFirstPage = false; //Used to check whether we are printing first page
        private bool bNewPage = false;// Used to check whether we are printing a new page
        private int iHeaderHeight = 0; //Used for the header height

        public FrmBaseAppPrint()
        {
            InitializeComponent();
        }

        private void RemoveInvisibleColumns()
        {
            List<String> RemoveColumns = new List<String>();
            string remove = string.Empty;

            for (int i = 0; i < Dados.Columns.Count; i++)
            {
                if (Dados.Columns[i].Visible == false)
                {
                    remove = Dados.Columns[i].Name;
                    RemoveColumns.Add(remove);
                }
            }

            if (RemoveColumns.Count > 0)
            {
                for (int i = 0; i < RemoveColumns.Count; i++)
                {
                    for (int j = 0; j < Dados.Columns.Count; j++)
                    {
                        if (RemoveColumns[i] == Dados.Columns[j].Name)
                        {
                            Dados.Columns.Remove(RemoveColumns[i]);                        
                        }
                    }
                }
            }            
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

            tab.Text = "Impressão de Relatório";

            this.Text = tab.Text;

            psTextoBox1.Text = "Document1";

            // remove colunas invisiveis
            RemoveInvisibleColumns();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (PSMessageBox.ShowQuestion("Confirma impressão do relatório ?") == DialogResult.Yes)
            {
                try
                {
                    if (checkBox1.Checked)
                    {
                        PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                        printPreviewDialog.Document = printDocument1;

                        printPreviewDialog.PrintPreviewControl.Zoom = 1;
                        (printPreviewDialog as Form).WindowState = FormWindowState.Maximized;
                        printPreviewDialog.ShowDialog();
                    }
                    else
                    {
                        PrintDialog printDialog = new PrintDialog();
                        printDialog.Document = printDocument1;
                        printDialog.UseEXDialog = true;

                        if (DialogResult.OK == printDialog.ShowDialog())
                        {
                            printDocument1.DocumentName = psTextoBox1.Text;
                            printDocument1.Print();
                        }
                    }
                }
                catch (Exception ex)
                {
                    PSMessageBox.ShowError(ex.Message);
                }
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in Dados.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in Dados.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                            (double)iTotalWidth * (double)iTotalWidth *
                            ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                            GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        // Save width and height of headers
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }

                //Loop till all the grid rows not get printed
                while (iRow <= Dados.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = Dados.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allows more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString(psTextoBox1.Text,
                                new Font(Dados.Font, FontStyle.Bold),
                                Brushes.Black, e.MarginBounds.Left,
                                e.MarginBounds.Top - e.Graphics.MeasureString(psTextoBox1.Text,
                                new Font(Dados.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " +
                                DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate,
                                new Font(Dados.Font, FontStyle.Bold), Brushes.Black,
                                e.MarginBounds.Left +
                                (e.MarginBounds.Width - e.Graphics.MeasureString(strDate,
                                new Font(Dados.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Width),
                                e.MarginBounds.Top - e.Graphics.MeasureString(psTextoBox1.Text,
                                new Font(new Font(Dados.Font, FontStyle.Bold),
                                FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in Dados.Columns)
                            {
                                // remove a coluna de seleção da grid de visão
                                if (!DisplaySelectedRow)
                                {
                                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                            new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                            (int)arrColumnWidths[iCount], iHeaderHeight));

                                    e.Graphics.DrawRectangle(Pens.Black,
                                        new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iHeaderHeight));

                                    e.Graphics.DrawString(GridCol.HeaderText,
                                        GridCol.InheritedStyle.Font,
                                        new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                }
                                else
                                {
                                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                        new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iHeaderHeight));

                                    e.Graphics.DrawRectangle(Pens.Black,
                                        new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iHeaderHeight));

                                    e.Graphics.DrawString(GridCol.HeaderText,
                                        GridCol.InheritedStyle.Font,
                                        new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                }

                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }

                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Value != null)
                            {
                                if (!DisplaySelectedRow)
                                {
                                    if (iCount != 0)
                                    {
                                        e.Graphics.DrawString(Cel.Value.ToString(),
                                            Cel.InheritedStyle.Font,
                                            new SolidBrush(Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount],
                                            (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight),
                                            strFormat);
                                    }
                                }
                                else
                                {
                                    e.Graphics.DrawString(Cel.Value.ToString(),
                                        Cel.InheritedStyle.Font,
                                        new SolidBrush(Cel.InheritedStyle.ForeColor),
                                        new RectangleF((int)arrColumnLefts[iCount],
                                        (float)iTopMargin,
                                        (int)arrColumnWidths[iCount], (float)iCellHeight),
                                        strFormat);
                                }

                                //Drawing Cells Borders 
                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iCellHeight));
                            }
                            else
                            {
                                if (iCount != 0)
                                {
                                    //Drawing Cells Borders 
                                    e.Graphics.DrawRectangle(Pens.Black,
                                        new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iCellHeight));
                                }                            
                            }

                            iCount++;
                        }
                    }

                    iRow++;
                    iTopMargin += iCellHeight;
                }
                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                PSMessageBox.ShowError(exc.Message);
            }
        }
    }
}
