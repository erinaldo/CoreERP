using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PS.Glb
{
    public class PSPartTipOperReportData : PS.Lib.WinForms.PSPartData
    {
        public override string ReadView()
        {
            return @"SELECT CODEMPRESA, 
CODTIPOPER, 
CODREPORT, 
(SELECT DESCRICAO FROM GREPORT WHERE CODEMPRESA = GTIPOPERREPORT.CODEMPRESA AND CODREPORT = GTIPOPERREPORT.CODREPORT) DESCRICAO 
FROM GTIPOPERREPORT WHERE ";
        }
    }
}
