using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartLocalEstoqueSaldo : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartLocalEstoqueSaldo()
        {
            this.TableName = "VLOCALESTOQUESALDO";
            this.Keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };
            this.FormEditName = "PSPartLocalEstoqueSaldoEdit";
            this.PSPartData = new PSPartLocalEstoqueSaldoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODPRODUTO", false));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("SALDO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.AllowDelete = false;
            this.AllowEdit = false;
            this.AllowInsert = false;

            this.SecurityID = "PSPartLocalEstoqueSaldo";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
