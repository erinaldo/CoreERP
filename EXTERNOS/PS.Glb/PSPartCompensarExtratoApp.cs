using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartCompensarExtratoApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartCompensarExtratoApp()
        {
            this.AppName = "Compensar Extrato";
            this.FormApp = new PSPartCompensarExtratoAppFrm();
            this.Image = null;
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartCompensarExtratoApp";
            this.ModuleID = "PG";
        }
    }
}
