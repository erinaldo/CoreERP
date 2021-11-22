using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartGerarBoletoApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartGerarBoletoApp()
        {
            this.AppName = "Gerar Boleto";
            this.FormApp = new PSPartGerarBoletoAppFrm();
            this.Image = new PS.Lib.ImageProperties(0, this.AppName, Properties.Resources.img_opaberto);
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartGerarBoletoApp";
            this.ModuleID = "PG";
        }
        public PSPartGerarBoletoApp(bool nfe)
        {
            this.AppName = "Gerar Boleto";
            this.FormApp = new PSPartGerarBoletoAppFrm(nfe);
            this.Image = new PS.Lib.ImageProperties(0, this.AppName, Properties.Resources.img_opaberto);
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartGerarBoletoApp";
            this.ModuleID = "PG";
        }
    }
}
