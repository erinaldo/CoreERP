using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relatorios
{
    public class StReportProgramacaoSemanal : PS.Lib.WinForms.StaticReport.PSPartStaticReport
    {
        public StReportProgramacaoSemanal()
        {
            this.ReportName = "Programação Semanal";
            this.FormApp = new StReportProgramacaoSemanalApp();
            this.Parameters = new string[] { "CODEMPRESA", "CODREPORT" };
        }
    }
}
