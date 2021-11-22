using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
   public class PSPartCancelarRemessaApp : PS.Lib.WinForms.PSPartApp
    {
       public PSPartCancelarRemessaApp()
        {
            this.AppName = "Cancelar Remessa";
            this.FormApp = new PSPartCancelarRemessaAppFrm();
            this.Image = new PS.Lib.ImageProperties(0, this.AppName, Properties.Resources.img_opaberto);
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartCancelarRemessaApp";
            this.ModuleID = "PG";
        }
    }
}
