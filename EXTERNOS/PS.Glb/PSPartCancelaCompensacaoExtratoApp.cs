using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartCancelaCompensacaoExtratoApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartCancelaCompensacaoExtratoApp()
        {
            this.AppName = "Cancelar Compensação do Extrato";
            this.FormApp = new PSPartCancelaCompensacaoExtratoAppFrm();
            this.Image = null;
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartCancelaCompensacaoExtratoApp";
            this.ModuleID = "PG";
        }
    }
}
