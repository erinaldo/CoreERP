using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartFabricante : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartFabricante()
        {
            this.TableName = "VFABRICANTE";
            this.Keys = new string[] { "CODEMPRESA", "CODFABRICANTE" };
            this.FormEditName = "PSPartFabricanteEdit";
            this.PSPartData = new PSPartFabricanteData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODCIDADE", false));

            this.SecurityID = "PSPartFabricante";
            this.ModuleID = "VR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CGCCPF", null, typeof(PS.Lib.WinForms.PSMaskedTextBox)));
            objArr.Add(new DataField("NOMEFANTASIA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODFABRICANTE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODPAIS", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODETD", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCIDADE", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("BAIRRO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPOBAIRRO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("COMPLEMENTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("RUA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPORUA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CEP", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            objArr.Add(new DataField("CCIDADE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CGCCPF", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
