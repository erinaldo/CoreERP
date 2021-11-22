using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartGerarNFeApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartGerarNFeApp()
        {
            this.AppName = "Gerar NF-e";
            this.FormApp = new PSPartGerarNFeAppFrm();
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartGerarNFeApp";
            this.ModuleID = "PG";
        }
    }
}
