using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartAprovaDescontoApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartAprovaDescontoApp()
        {
            this.AppName = "Aprova Desconto";
            this.FormApp = new PSPartAprovaDescontoAppFrm();
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartAprovaDescontoApp";
            this.ModuleID = "PG";
        }
    }
}
