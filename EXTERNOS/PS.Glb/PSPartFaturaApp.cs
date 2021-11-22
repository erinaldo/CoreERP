using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartFaturaApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartFaturaApp()
        {
            this.AppName = "Gerar Fatura";
            this.FormApp = new PSPartFaturaAppFrm();
            this.Image = new PS.Lib.ImageProperties(0, this.AppName, Properties.Resources.img_appfatura);
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartFaturaApp";
            this.ModuleID = "PG";
        }
    }
}
    