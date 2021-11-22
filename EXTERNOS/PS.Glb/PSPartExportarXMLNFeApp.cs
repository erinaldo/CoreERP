using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartExportarXMLNFeApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartExportarXMLNFeApp()
        {
            this.AppName = "Exportar XML";
            this.FormApp = new PSPartExportarXMLNFeAppFrm();
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartExportarXMLNFeApp";
            this.ModuleID = "PG";
        }
    }
}
