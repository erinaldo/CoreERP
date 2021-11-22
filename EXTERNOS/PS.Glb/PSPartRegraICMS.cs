using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartRegraICMS : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartRegraICMS()
        {
            this.TableName = "VREGRAICMS";
            this.Keys = new string[] { "CODEMPRESA", "IDREGRA" };
            this.FormEditName = "PSPartRegraICMSEdit";
            this.PSPartData = new PSPartRegraICMSData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("ALIQUOTA",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));


            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("REDUCAOBASEICMS",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.SecurityID = "PSPartRegraICMS";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("IDREGRA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("CODCST", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("ALIQUOTA", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("REDUCAOBASEICMS", null, typeof(PS.Lib.WinForms.PSMoedaBox)));

            return objArr;
        }
    }
}
