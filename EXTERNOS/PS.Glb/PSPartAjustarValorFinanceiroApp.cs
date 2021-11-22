using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartAjustarValorFinanceiroApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartAjustarValorFinanceiroApp()
        {
            this.AppName = "Ajustar Valor Financeiro";
            this.FormApp = new PSPartAjustarValorFinanceiroAppFrm();
            this.Select = PS.Lib.SelectType.OnlyOneRow;
            this.Refresh = true;

            this.SecurityID = "PSPartAjustarValorFinanceiroApp";
            this.ModuleID = "PG";
        }
    }
}