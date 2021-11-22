using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartProdutoTributo : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartProdutoTributo()
        {
            this.TableName = "VPRODUTOTRIBUTO";
            this.Keys = new string[] { "CODEMPRESA", "CODPRODUTO", "CODTRIBUTO" };
            this.FormEditName = "PSPartProdutoTributoEdit";
            this.PSPartData = new PSPartProdutoTributoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODPRODUTO", false));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("ALIQUOTA",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.SecurityID = "PSPartProdutoTributo";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
