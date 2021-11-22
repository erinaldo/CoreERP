using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartCancelaLancaApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartCancelaLancaApp()
        {
            this.AppName = "Cancelar Lançamento";
            this.FormApp = new PSPartCancelaLancaAppFrm();
            this.Image = new PS.Lib.ImageProperties(0, this.AppName, Properties.Resources.img_appcanlanca);
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartCancelaLancaApp";
            this.ModuleID = "PG";
        }
    }
}
