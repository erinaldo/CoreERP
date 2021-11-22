using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartOrdemProducaoAndamentoApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartOrdemProducaoAndamentoApp()
        {
            this.AppName = "Andamento da Ordem de Produção";
            this.FormApp = new PSPartOrdemProducaoAndamentoAppFrm();
            this.Select = PS.Lib.SelectType.OnlyOneRow;
            this.Refresh = true;

            this.SecurityID = "PSPartOrdemProducaoAndamentoApp";
            this.ModuleID = "PR";
        }
    }
}

