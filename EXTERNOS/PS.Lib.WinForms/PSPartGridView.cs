using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib.WinForms
{
    public class PSPartGridView : PSPart
    {
        private Form _MainForm;
        public FrmBaseVisao f;

        public Form MainForm
        {
            get
            {
                return _MainForm;
            }
            set
            {
                _MainForm = value;
            }
        }

        public PSPartGridView()
        {
            f = new FrmBaseVisao();
            folderType = FolderType.View;
        }

        public override void  Execute()
        {
            try
            {
                base.Execute();

                f._psPart = this;
                f._psData = PSPartData;

                f._psData._keys = Keys;
                f._psData._tablename = TableName;

                f.PermiteIncluir = this.AllowInsert;
                f.PermiteEditar = this.AllowEdit;
                f.PermiteExcluir = this.AllowDelete;

                f.MdiParent = (Form)_MainForm;
                f.Show();
            }
            catch (Exception ex)
            {
                f.Dispose();
                PSMessageBox.ShowError(ex.Message);
            }
        }
    }
}
