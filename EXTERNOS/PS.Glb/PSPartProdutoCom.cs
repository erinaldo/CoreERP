using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartProdutoCom : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartProdutoCom()
        {
            this.TableName = "VPRODUTOCOM";
            this.Keys = new string[] { "CODEMPRESA", "CODPRODUTO", "CODPRODCOM" };
            this.FormEditName = "PSPartProdutoComEdit";
            this.PSPartData = new PSPartProdutoComData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODPRODUTO", false));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("QUANTIDADE",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.SecurityID = "PSPartProdutoCom";
            this.ModuleID = "VR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
