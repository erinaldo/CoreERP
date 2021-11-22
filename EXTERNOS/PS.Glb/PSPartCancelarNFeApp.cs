using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartCancelarNFeApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartCancelarNFeApp()
        {
            this.AppName = "Cancelar NF-e";
            this.FormApp = new PSPartCancelarNFeAppFrm();
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartCancelarNFeApp";
            this.ModuleID = "PG";
        }
    }
}
