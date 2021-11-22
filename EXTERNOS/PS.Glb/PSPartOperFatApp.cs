using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartOperFatApp: PS.Lib.WinForms.PSPartApp
    {
        public PSPartOperFatApp()
        {
            this.AppName = "Faturar Operação";
            this.FormApp = new PSPartOperFatAppFrm();
            this.Image = new PS.Lib.ImageProperties(0, this.AppName, Properties.Resources.img_appfatura);
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartOperFatApp";
            this.ModuleID = "PG";
        }
    }
}
