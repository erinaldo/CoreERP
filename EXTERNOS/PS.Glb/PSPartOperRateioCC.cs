using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartOperRateioCC : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartOperRateioCC()
        {
            this.TableName = "GOPERRATEIOCC";
            this.Keys = new string[] { "CODEMPRESA", "CODOPER", "CODCCUSTO" };
            this.FormEditName = "PSPartOperRateioCCEdit";
            this.PSPartData = new PSPartOperRateioCCData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODOPER", false));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("PERCENTUAL",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VALOR",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.SecurityID = "PSPartOperRateioCC";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}

