using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartOperItemCompl : PS.Lib.WinForms.PSPartEditView
    {
        public PSPartOperItemCompl()
        {
            this.TableName = "GOPERITEMCOMPL";
            this.Keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM" };
            this.PSPartData = new PSPartOperItemComplData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartOperItemCompl";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
