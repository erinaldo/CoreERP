using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTipOperReport : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartTipOperReport()
        {
            this.TableName = "GTIPOPERREPORT";
            this.Keys = new string[] { "CODEMPRESA", "CODTIPOPER", "CODREPORT" };
            this.FormEditName = "PSPartTipOperReportEdit";
            this.PSPartData = new PSPartTipOperReportData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODTIPOPER",false));

            this.SecurityID = "PSPartTipOperReport";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
