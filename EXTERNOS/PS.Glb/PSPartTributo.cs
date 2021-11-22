using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTributo : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartTributo()
        {
            this.TableName = "VTRIBUTO";
            this.Keys = new string[] { "CODEMPRESA", "CODTRIBUTO" };
            this.FormEditName = "PSPartTributoEdit";
            this.PSPartData = new PSPartTributoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("ALIQUOTA",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.SecurityID = "PSPartTributo";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("ALIQUOTAEM", null, typeof(PS.Lib.WinForms.PSComboBox)));
            objArr.Add(new DataField("CODTIPOTRIBUTO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("ALIQUOTA", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTRIBUTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            objArr.Add(new DataField("CODCST", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
