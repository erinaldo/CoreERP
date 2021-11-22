using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartCancelarFaturaApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartCancelarFaturaApp()
        {
            this.AppName = "Cancelar Fatura";
            this.FormApp = new PSPartCancelarFaturaAppFrm();
            this.Image = new PS.Lib.ImageProperties(0, this.AppName, Properties.Resources.img_appcanlanca);
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartCancelarFaturaApp";
            this.ModuleID = "PG";
        }
    }
}
