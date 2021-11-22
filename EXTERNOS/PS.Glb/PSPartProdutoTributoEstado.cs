using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartProdutoTributoEstado : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartProdutoTributoEstado()
        {
            this.TableName = "VPRODUTOTRIBUTOESTADO";
            this.Keys = new string[] { "CODEMPRESA", "CODPRODUTO", "CODTRIBUTO", "CODESTADO" };
            this.FormEditName = "PSPartProdutoTributoEstadoEdit";
            this.PSPartData = new PSPartProdutoTributoEstadoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODPRODUTO", false));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("ALIQUOTA",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.SecurityID = "PSPartProdutoTributoEstado";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
