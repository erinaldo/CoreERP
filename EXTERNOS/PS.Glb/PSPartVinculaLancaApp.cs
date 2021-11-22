using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartVinculaLancaApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartVinculaLancaApp()
        {
            this.AppName = "Vincula Lançamento";
            this.FormApp = new PSPartVinculaLancaAppFrm();
            this.Image = new PS.Lib.ImageProperties(0, this.AppName, Properties.Resources.img_appbaixalanca);
            this.Select = PS.Lib.SelectType.OnlyOneRow;
            this.Refresh = true;

            this.SecurityID = "PSPartVinculaLancaApp";
            this.ModuleID = "PG";
        }
    }
}
