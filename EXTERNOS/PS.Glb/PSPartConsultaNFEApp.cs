using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartConsultaNFEApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartConsultaNFEApp()
        {
            this.AppName = "Consulta NF-e";
            this.FormApp = new PSPartConsultaNFEAppFrm();
            this.Image = new PS.Lib.ImageProperties(0, this.AppName, Properties.Resources.img_appconnfe);

            this.SecurityID = "PSPartConsultaNFEApp";
            this.ModuleID = "PG";
        }
    }
}
