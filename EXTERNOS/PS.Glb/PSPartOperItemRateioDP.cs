using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartOperItemRateioDP : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartOperItemRateioDP()
        {
            this.TableName = "GOPERITEMRATEIODP";
            this.Keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM", "CODFILIAL", "CODDEPTO" };
            this.FormEditName = "PSPartOperItemRateioDPEdit";
            this.PSPartData = new PSPartOperItemRateioDPData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODOPER", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODFILIAL", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("NSEQITEM", false));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("PERCENTUAL",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VALOR",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.SecurityID = "PSPartOperRateioDP";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
