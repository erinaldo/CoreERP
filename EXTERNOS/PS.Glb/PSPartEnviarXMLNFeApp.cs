using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartEnviarXMLNFeApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartEnviarXMLNFeApp()
        {
            this.AppName = "Enviar XML da NF-e por E-mail";
            this.FormApp = new PSPartEnviarXMLNFeAppFrm();
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartEnviarXMLNFeApp";
            this.ModuleID = "PG";
        }
    }
}
