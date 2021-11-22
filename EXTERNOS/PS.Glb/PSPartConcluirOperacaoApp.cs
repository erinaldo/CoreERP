using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartConcluirOperacaoApp : PS.Lib.WinForms.PSPartApp
    {
       
        public PSPartConcluirOperacaoApp()
        {
            this.AppName = "Concluir Operação";
            this.FormApp = new PSPartConcluirOperacaoAppFrm();
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartConcluirOperacaoApp";
            this.ModuleID = "PG";
        }
    }
}
