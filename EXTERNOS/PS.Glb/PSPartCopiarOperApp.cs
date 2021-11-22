using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartCopiarOperApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartCopiarOperApp()
        {
            this.AppName = "Copiar Operação";
            this.FormApp = new PSPartCopiarOperAppFrm();
            //this.Image = new PS.Lib.ImageProperties(0, this.AppName, Properties.Resources.img_appfatura);
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartCopiarOperApp";
            this.ModuleID = "PG";
        }
    }
}
