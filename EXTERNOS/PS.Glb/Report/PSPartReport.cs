using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb.Report
{
    public class PSPartReport : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartReport()
        {
            this.TableName = "GREPORT";
            this.Keys = new string[] { "CODEMPRESA", "CODREPORT" };
            this.FormEditName = "PSPartReportEdit";
            this.PSPartData = new PSPartReportData();

            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartAcessoRelatorio(), "CODEMPRESA", "CODREPORT"));

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            // this.PSPartApp.Add(new PSPartReportApp());
            this.PSPartApp.Add(new PSPartReportExecApp());

            this.SecurityID = "PSPartReport";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CLASSNAME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NAMESPACE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("ASSEMBLYNAME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODPSPART", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODREPORT", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
