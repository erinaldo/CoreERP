using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartImprimirCopiaChequeApp: PS.Lib.WinForms.PSPartApp
    {
        public PSPartImprimirCopiaChequeApp()
        {
            this.AppName = "Imprimir Cópia de Cheque";
            this.FormApp = new PSPartImprimirCopiaChequeAppFrm();
            this.Select = PS.Lib.SelectType.OnlyOneRow;
            this.Refresh = true;

            this.SecurityID = "PSPartImprimirCopiaChequeApp";
            this.ModuleID = "PG";
        }
    }
}
