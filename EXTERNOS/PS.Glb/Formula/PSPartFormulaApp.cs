using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb.Formula
{
    public class PSPartFormulaApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartFormulaApp()
        {
            this.AppName = "Editar Fórmula";
            this.FormApp = null;
            this.Select = SelectType.OnlyOneRow;

            this.SecurityID = "PSPartFormulaApp";
            this.ModuleID = "PG";
        }

        public override void Execute()
        {
            FrmBaseFormulaEdit f = new FrmBaseFormulaEdit();
            f.psPartApp = this;
            f.ShowDialog();
        }
    }
}
