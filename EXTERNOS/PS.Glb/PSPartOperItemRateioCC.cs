using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartOperItemRateioCC : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartOperItemRateioCC()
        {
            this.TableName = "GOPERITEMRATEIOCC";
            this.Keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM", "CODCCUSTO" };
            this.FormEditName = "PSPartOperItemRateioCCEdit";
            this.PSPartData = new PSPartOperItemRateioCCData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODOPER", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("NSEQITEM", false));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("PERCENTUAL",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VALOR",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.SecurityID = "PSPartOperItemRateioCC";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}


