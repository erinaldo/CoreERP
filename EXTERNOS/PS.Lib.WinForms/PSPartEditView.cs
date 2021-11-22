using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib.WinForms
{
    public class PSPartEditView : PSPart
    {
        public FrmBaseEditCompl f;

        public PSPartEditView()
        {
            folderType = FolderType.Edit;
        }

        public override void Execute(List<DataField> ObjArr)
        {
            try
            {
                base.Execute(ObjArr);

                f = new FrmBaseEditCompl();

                f._tabela = TableName;
                f._chaves = Keys;

                f._setDefault = ObjArr;

                f._psData = PSPartData;
                f._psData._keys = Keys;
                f._psData._tablename = TableName;

                f.SecurityID = this.SecurityID;
                f.ModuleID = this.ModuleID;

                f.ShowDialog();
            }
            catch (Exception ex)
            {
                f.Dispose();
                PSMessageBox.ShowError(ex.Message);
            }
        }
    }
}
