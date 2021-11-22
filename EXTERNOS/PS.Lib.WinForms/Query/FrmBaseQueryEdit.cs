using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib.WinForms.Query
{
    public partial class FrmBaseQueryEdit : DevExpress.XtraEditors.XtraForm
    {
        public PSPartApp psPartApp { get; set; } //classe do aplicativo

        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        private int codEmpresa = 0;
        private string codQuery = string.Empty;

        public FrmBaseQueryEdit()
        {
            InitializeComponent();
        }

        private void CarregaParametros()
        {
            if (this.psPartApp.Access == AppAccess.View)
            {
                for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(psPartApp.DataGrid.Rows[i].Cells[0].Value))
                    {
                        for (int j = 0; j < psPartApp.DataGrid.Columns.Count; j++)
                        {
                            if (psPartApp.DataGrid.Columns[j].Name == "CODEMPRESA")
                            {
                                codEmpresa = int.Parse(psPartApp.DataGrid.Rows[i].Cells[j].Value.ToString());                            
                            }

                            if (psPartApp.DataGrid.Columns[j].Name == "CODQUERY")
                            {
                                codQuery = psPartApp.DataGrid.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                    }
                }
            }

            if (this.psPartApp.Access == AppAccess.Edit)
            {
                PS.Lib.DataField dataField = new PS.Lib.DataField();
                dataField = gb.RetornaDataFieldByCampo(this.psPartApp.DataField, "CODEMPRESA");

                codEmpresa = int.Parse(dataField.Valor.ToString());

                dataField = gb.RetornaDataFieldByCampo(this.psPartApp.DataField, "CODQUERY");

                codQuery = dataField.Valor.ToString();
            }

            BuscaSentenca();

            richTextBox1.Focus();
        }

        private void BuscaSentenca()
        {
            string sSql = @"SELECT SENTENCA FROM GQUERYCOMPL WHERE CODEMPRESA = ? AND CODQUERY = ?";

            richTextBox1.Text = dbs.QueryValue(string.Empty, sSql, codEmpresa, codQuery).ToString();
        }

        private void SaveRecord()
        {
            // Operação de INSERT ou UPDATE
            string sSQL = @"SELECT 1 FROM GQUERYCOMPL WHERE CODEMPRESA = ? AND CODQUERY = ?";

            if (dbs.QueryFind(sSQL, codEmpresa, codQuery))
            {
                EditRecord();
            }
            else
            {
                InsertRecord();
            }        
        }

        private void EditRecord()
        {
            string sSQL = @"UPDATE GQUERYCOMPL SET SENTENCA = ? WHERE CODEMPRESA = ? AND CODQUERY = ?";

            dbs.QueryExec(sSQL, richTextBox1.Text, codEmpresa, codQuery);        
        }

        private void InsertRecord()
        {
            string sSQL = @"INSERT INTO GQUERYCOMPL (CODEMPRESA, CODQUERY, SENTENCA) VALUES (?,?,?)";

            dbs.QueryExec(sSQL, codEmpresa, codQuery, richTextBox1.Text);
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBaseQueryEdit_Load(object sender, EventArgs e)
        {
            CarregaParametros();
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveRecord();
                PSMessageBox.ShowInfo("Operação realizada com sucesso");
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);            
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            mnuFileSave_Click(this, null);
        }
    }
}
