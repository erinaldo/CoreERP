using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTipOperNatureza : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartTipOperNatureza()
        {
            this.TableName = "GTIPOPERNAT";
            this.Keys = new string[] { "CODEMPRESA", "CODTIPOPER", "CODNATUREZA" };
            this.FormEditName = "PSPartTipOperNaturezaEdit";
            this.PSPartData = new PSPartTipOperNaturezaData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODTIPOPER", false));


            this.SecurityID = "PSPartTipOperNatureza";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
