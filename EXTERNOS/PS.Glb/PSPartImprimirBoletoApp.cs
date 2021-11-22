using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartImprimirBoletoApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartImprimirBoletoApp()
        {
            this.AppName = "Imprimir Boleto";
            this.FormApp = new PSPartImprimirBoletoAppFrm();
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartImprimirBoletoApp";
            this.ModuleID = "PG";
        }
    }
}
