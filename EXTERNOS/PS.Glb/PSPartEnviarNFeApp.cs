using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartEnviarNFeApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartEnviarNFeApp()
        {
            this.AppName = "Enviar NF-e";
            this.FormApp = new PSPartEnviarNFeAppFrm();
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartEnviarNFeApp";
            this.ModuleID = "PG";
        }
    }
}
