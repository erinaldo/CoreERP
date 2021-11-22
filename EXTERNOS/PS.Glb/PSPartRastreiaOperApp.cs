using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartRastreiaOperApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartRastreiaOperApp()
        {
            this.AppName = "Operações Relacionadas";
            this.FormApp = new PSPartRastreiaOperAppFrm();
            this.Image = new PS.Lib.ImageProperties(0, this.AppName, Properties.Resources.relac);
            this.Select = PS.Lib.SelectType.OnlyOneRow;
            this.Refresh = true;

            this.SecurityID = "PSPartRastreiaOperApp";
            this.ModuleID = "PG";
        }
    }
}
