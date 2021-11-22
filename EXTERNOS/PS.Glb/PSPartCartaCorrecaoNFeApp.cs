using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartCartaCorrecaoNFeApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartCartaCorrecaoNFeApp()
        {
            this.AppName = "Carta de Correção";
            this.FormApp = new PSPartCartaCorrecaoNFeAppFrm();
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartCartaCorrecaoNFeApp";
            this.ModuleID = "PG";
        }
    }
}
