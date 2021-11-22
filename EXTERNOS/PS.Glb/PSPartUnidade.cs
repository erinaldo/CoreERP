using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartUnidade : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartUnidade()
        {
            this.TableName = "VUNID";
            this.Keys = new string[] { "CODEMPRESA", "CODUNID" };
            this.FormEditName = "PSPartUnidadeEdit";
            this.PSPartData = new PSPartUnidadeData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA",PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("FATORCONVERSAO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,####0.0000",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.SecurityID = "PSPartUnidade";
            this.ModuleID = "VR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("FATORCONVERSAO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODUNIDBASE", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODUNID", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
