using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartBaixaLancaApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartBaixaLancaApp()
        {
            this.AppName = "Baixa Lançamento";
            this.FormApp = new PSPartBaixaLancaAppFrm();
            this.Image = new PS.Lib.ImageProperties(0, this.AppName, Properties.Resources.img_appbaixalanca);
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartBaixaLancaApp";
            this.ModuleID = "PG";
        }
    }
}
