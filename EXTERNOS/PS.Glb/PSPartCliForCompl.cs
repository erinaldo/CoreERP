using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartCliForCompl : PS.Lib.WinForms.PSPartEditView
    {
        public PSPartCliForCompl()
        {
            this.TableName = "VCLIFORCOMPL";
            this.Keys = new string[] { "CODEMPRESA", "CODCLIFOR" };
            this.PSPartData = new PSPartCliForComplData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartCliForCompl";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
