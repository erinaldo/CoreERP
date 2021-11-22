using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartCodigoBarras : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartCodigoBarras()
        {
            this.TableName = "VPRODUTOCODIGO";
            this.Keys = new string[] { "CODEMPRESA", "CODPRODUTO", "CODIGOBARRAS" };
            this.FormEditName = "PSPartCodigoBarrasEdit";
            this.PSPartData = new PSPartCodigoBarrasData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODPRODUTO", false));

            this.SecurityID = "PSPartCodigoBarras";
            this.ModuleID = "VR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(System.Int32)));
            objArr.Add(new DataField("CODPRODUTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("CODIGOBARRAS", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
