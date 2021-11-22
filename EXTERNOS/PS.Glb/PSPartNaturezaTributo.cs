using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartNaturezaTributo : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartNaturezaTributo()
        {
            this.TableName = "VNATUREZATRIBUTO";
            this.Keys = new string[] { "CODEMPRESA", "CODNATUREZA", "CODTRIBUTO" };
            this.FormEditName = "PSPartNaturezaTributoEdit";
            this.PSPartData = new PSPartNaturezaTributoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODNATUREZA", false));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("ALIQUOTA",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.SecurityID = "PSPartNaturezaTributo";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
