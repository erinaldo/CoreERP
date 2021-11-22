using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTabDinamicaItem : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartTabDinamicaItem()
        {
            this.TableName = "GTABDINAMICAITEM";
            this.Keys = new string[] { "CODEMPRESA", "CODTABELA", "CODREGISTRO" };
            this.FormEditName = "PSPartTabDinamicaItemEdit";
            this.PSPartData = new PSPartTabDinamicaItemData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartTabDinamicaItem";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
