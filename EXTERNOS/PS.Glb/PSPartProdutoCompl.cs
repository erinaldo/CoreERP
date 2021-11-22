using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartProdutoCompl: PS.Lib.WinForms.PSPartEditView
    {
        public PSPartProdutoCompl()
        {
            this.TableName = "VPRODUTOCOMPL";
            this.Keys = new string[] { "CODEMPRESA", "CODPRODUTO" };
            this.PSPartData = new PSPartProdutoComplData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartProdutoCompl";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
