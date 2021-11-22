using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartCancelaBaixaApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartCancelaBaixaApp()
        {
            this.AppName = "Cancelar Baixa";
            this.FormApp = new PSPartCancelaBaixaAppFrm();
            this.Image = new PS.Lib.ImageProperties(0, this.AppName, Properties.Resources.img_canbaixalanca);
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartCancelaBaixaApp";
            this.ModuleID = "PG";
        }
    }
}
