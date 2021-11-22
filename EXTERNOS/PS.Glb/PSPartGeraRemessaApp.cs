using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartGeraRemessaApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartGeraRemessaApp()
        {
            this.AppName = "Gerar Remessa";
            this.FormApp = new PSPartGerarRemessaAppFrm();
            this.Image = new PS.Lib.ImageProperties(0, this.AppName, Properties.Resources.img_opcancelado);
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartGeraRemessaApp";
            this.ModuleID = "PG";
        }
    }
}
