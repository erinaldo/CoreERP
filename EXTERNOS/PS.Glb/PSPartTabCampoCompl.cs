using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTabCampoCompl : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartTabCampoCompl()
        {
            this.TableName = "GTABCAMPOCOMPL";
            this.Keys = new string[] { "CODENTIDADE", "NOMECAMPO" };
            this.FormEditName = "PSPartTabCampoComplEdit";
            this.PSPartData = new PSPartTabCampoComplData();

            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.AllowDelete = false;

            this.SecurityID = "PSPartTabCampoCompl";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CASASDECIMAIS", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("CODTABELA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("ORDEM", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("TAMANHO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("TIPO", null, typeof(PS.Lib.WinForms.PSComboBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NOMECAMPO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODENTIDADE", null, typeof(PS.Lib.WinForms.PSComboBox)));
            objArr.Add(new DataField("CODENTIDADE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("TIPO", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
