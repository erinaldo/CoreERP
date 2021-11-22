using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTipOperTributo : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartTipOperTributo()
        {
            this.TableName = "GTIPOPERTRIBUTO";
            this.Keys = new string[] { "CODEMPRESA", "CODTIPOPER", "CODTRIBUTO" };
            this.FormEditName = "PSPartTipOperTributoEdit";
            this.PSPartData = new PSPartTipOperTributoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODTIPOPER", false));

            this.SecurityID = "PSPartTipOperTributo";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
