using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartInutilizarNFeApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartInutilizarNFeApp()
        {
            this.AppName = "Inutilizar NF-e";
            this.FormApp = new PSPartInutilizarNFeAppFrm();
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartInutilizarNFeApp";
            this.ModuleID = "PG";
        }
    }
}
