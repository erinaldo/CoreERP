using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartOperSimFatApp : PS.Lib.WinForms.PSPartApp
    {
        public PSPartOperSimFatApp()
        {
            this.AppName = "Simulação Financeira";
            this.FormApp = new PSPartOperSimFatAppFrm();

            this.SecurityID = "PSPartOperSimFatApp";
            this.ModuleID = "PG";
        }
    }
}
