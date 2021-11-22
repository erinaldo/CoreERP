using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PS.Lib;

namespace PS.Glb.Formula
{
    public partial class FrmBaseFormulaEdit : DevExpress.XtraEditors.XtraForm
    {
        public PS.Lib.WinForms.PSPartApp psPartApp { get; set; } //classe do aplicativo

        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        private int codEmpresa = 0;
        private string codQuery = string.Empty;

        public FrmBaseFormulaEdit()
        {
            InitializeComponent();

            lblChave1.Text = string.Empty;
            lblChave2.Text = string.Empty;
            lblChave3.Text = string.Empty;
            lblChave4.Text = string.Empty;
            lblChave5.Text = string.Empty;
            lblResult.Text = string.Empty;
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

                            if (psPartApp.DataGrid.Columns[j].Name == "CODFORMULA")
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

                dataField = gb.RetornaDataFieldByCampo(this.psPartApp.DataField, "CODFORMULA");

                codQuery = dataField.Valor.ToString();
            }

            BuscaSentenca();

            richTextBox1.Focus();
        }

        private void BuscaSentenca()
        {
            string sSql = @"SELECT TEXTO FROM GFORMULA WHERE CODEMPRESA = ? AND CODFORMULA = ?";

            richTextBox1.Text = dbs.QueryValue(string.Empty, sSql, codEmpresa, codQuery).ToString();
        }

        private int BuscaContexto()
        {
            string sSql = @"SELECT CONTEXTO FROM GFORMULA WHERE CODEMPRESA = ? AND CODFORMULA = ?";

            return int.Parse(dbs.QueryValue("0", sSql, codEmpresa, codQuery).ToString());
        }

        private void SaveRecord()
        {
            // Operação de INSERT ou UPDATE
            string sSQL = @"SELECT 1 FROM GFORMULA WHERE CODEMPRESA = ? AND CODFORMULA = ?";

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
            string sSQL = @"UPDATE GFORMULA SET TEXTO = ? WHERE CODEMPRESA = ? AND CODFORMULA = ?";

            dbs.QueryExec(sSQL, richTextBox1.Text, codEmpresa, codQuery);        
        }

        private void InsertRecord()
        {
            string sSQL = @"INSERT INTO GFORMULA (CODEMPRESA, CODFORMULA, TEXTO) VALUES (?,?,?)";

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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.richTextBox1.Text != string.Empty)
            {
                try
                {
                    FrmBaseFormulaContextApp frm = new FrmBaseFormulaContextApp();
                    frm.ShowDialog();

                    /*
                    if(( PS.Lib.Contexto.Session.key1 == null &&
                         PS.Lib.Contexto.Session.key2 == null &&
                         PS.Lib.Contexto.Session.key3 == null &&
                         PS.Lib.Contexto.Session.key4 == null &&
                         PS.Lib.Contexto.Session.key5 == null    ) || (PS.Lib.Contexto.Session.Current == null))
                        return;
                    */

                    if (PS.Lib.Contexto.Session.key1 == null &&
                     PS.Lib.Contexto.Session.key2 == null &&
                     PS.Lib.Contexto.Session.key3 == null &&
                     PS.Lib.Contexto.Session.key4 == null &&
                     PS.Lib.Contexto.Session.key5 == null)
                        return;

                    if(PS.Lib.Contexto.Session.key1 != null)
                        lblChave1.Text = PS.Lib.Contexto.Session.key1.ToString();

                    if (PS.Lib.Contexto.Session.key2 != null)
                        lblChave2.Text = PS.Lib.Contexto.Session.key2.ToString();

                    if (PS.Lib.Contexto.Session.key3 != null)
                        lblChave3.Text = PS.Lib.Contexto.Session.key3.ToString();

                    if (PS.Lib.Contexto.Session.key4 != null)
                        lblChave4.Text = PS.Lib.Contexto.Session.key4.ToString();

                    if (PS.Lib.Contexto.Session.key5 != null)
                        lblChave5.Text = PS.Lib.Contexto.Session.key5.ToString();

                    Function fc = new Function();

                    PS.Lib.Interpretador interpreta = new Interpretador();
                    interpreta.comando = fc.PreparaFormula(this.richTextBox1.Text);
                    object returno = interpreta.Executar();


                    if (returno == null)
                    {
                        return;
                    }
                    else
                    {
                        lblResult.Text = returno.ToString();                    
                    }
                }
                catch (Exception ex)
                {
                    PSMessageBox.ShowError(ex.Message);                
                }
            }
        }

        private void FrmBaseFormulaEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            PS.Lib.Contexto.Session.key1 = null;
            PS.Lib.Contexto.Session.key2 = null;
            PS.Lib.Contexto.Session.key3 = null;
            PS.Lib.Contexto.Session.key4 = null;
            PS.Lib.Contexto.Session.key5 = null;

            PS.Lib.Contexto.Session.Current = null;
        }

        private void mnuEditCut_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void mnuEditCopy_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void mnuEditPaste_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void tsbCopy_Click(object sender, EventArgs e)
        {
            mnuEditCopy_Click(this, null);
        }

        private void tsbCut_Click(object sender, EventArgs e)
        {
            mnuEditCut_Click(this, null);
        }

        private void tsbPaste_Click(object sender, EventArgs e)
        {
            mnuEditPaste_Click(this, null);
        }

        private void tsbUndo_Click(object sender, EventArgs e)
        {
            mnuEditUndo_Click(this, null);
        }

        private void mnuEditUndo_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void mnuEditRedo_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void tsbRedo_Click(object sender, EventArgs e)
        {
            mnuEditRedo_Click(this, null);
        }
    }
}
