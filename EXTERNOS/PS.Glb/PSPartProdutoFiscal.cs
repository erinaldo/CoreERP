using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartProdutoFiscal : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartProdutoFiscal()
        {
            this.TableName = "VPRODUTOFISCAL";
            this.Keys = new string[] { "CODEMPRESA", "CODPRODUTO", "CODETD" };
            this.FormEditName = "PSPartProdutoFiscalEdit";
            this.PSPartData = new PSPartProdutoFiscalData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODPRODUTO", false));

            this.SecurityID = "PSPartProdutoFiscal";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
