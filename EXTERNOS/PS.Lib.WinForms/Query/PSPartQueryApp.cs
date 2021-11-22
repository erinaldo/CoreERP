using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib.WinForms.Query
{
    public class PSPartQueryApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartQueryApp()
        {
            this.AppName = "Editar Consulta SQL";
            this.FormApp = null;
            this.Select = SelectType.OnlyOneRow;

            this.SecurityID = "PSPartQueryApp";
            this.ModuleID = "PG";
        }

        public override void Execute()
        {
            FrmBaseQueryEdit f = new FrmBaseQueryEdit();
            f.psPartApp = this;
            f.ShowDialog();
        }
    }
}
