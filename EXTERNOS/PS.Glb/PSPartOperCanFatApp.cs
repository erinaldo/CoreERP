using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartOperCanFatApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartOperCanFatApp()
        {
            this.AppName = "Cancelar Operação";
            this.FormApp = new PSPartOperCanFatAppFrm();
            this.Image = new PS.Lib.ImageProperties(0, this.AppName, Properties.Resources.img_appcanlanca);
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartOperCanFatApp";
            this.ModuleID = "PG";
        }
    }
}
