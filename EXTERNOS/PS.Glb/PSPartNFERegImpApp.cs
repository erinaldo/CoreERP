using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartNFERegImpApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartNFERegImpApp()
        {
            this.AppName = "NFE - Importação de XML";
            this.FormApp = new PSPartNFERegImpAppFrm();
            this.Select = PS.Lib.SelectType.OnlyOneRow;
            this.Refresh = true;

            this.SecurityID = "PSPartNFERegImpApp";
            this.ModuleID = "PG";
        }
    }
}
