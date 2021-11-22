using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartConsultarAutorizacaoNFeApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartConsultarAutorizacaoNFeApp()
        {
            this.AppName = "Consultar Situação NF-e";
            this.FormApp = new PSPartConsultarAutorizacaoNFeAppFrm();
            this.Select = PS.Lib.SelectType.MultiRows;
            this.Refresh = true;

            this.SecurityID = "PSPartConsultarAutorizacaoNFeApp";
            this.ModuleID = "PG";
        }
    }
}
