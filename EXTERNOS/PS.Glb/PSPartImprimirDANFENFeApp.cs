using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartImprimirDANFENFeApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartImprimirDANFENFeApp()
        {
            this.AppName = "Imprimir DANFE";
            this.FormApp = new PSPartImprimirDANFENFeAppFrm();
            this.Select = PS.Lib.SelectType.OnlyOneRow;
            this.Refresh = true;

            this.SecurityID = "PSPartImprimirDANFENFeApp";
            this.ModuleID = "PG";
        }
    }
}
