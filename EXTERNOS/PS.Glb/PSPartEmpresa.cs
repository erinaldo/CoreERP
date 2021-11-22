using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartEmpresa: PS.Lib.WinForms.PSPartGridView
    {
        public PSPartEmpresa()
        {
            this.TableName = "GEMPRESA";
            this.Keys = new string[] { "CODEMPRESA" };
            this.FormEditName = "PSPartEmpresaEdit";
            this.PSPartData = new PSPartEmpresaData();

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODCIDADE", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODCONTROLE", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODCHAVE1", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODCHAVE2", false));

            this.AllowInsert = false;
            this.AllowDelete = false;

            this.SecurityID = "PSPartEmpresa";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODIMAGEM", null, typeof(PS.Lib.WinForms.PSImageBox)));
            objArr.Add(new DataField("EMAIL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("TELEFONE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("INSCRICAOESTADUAL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CGCCPF", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NOMEFANTASIA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
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


            objArr.Add(new DataField("CODCHAVE1", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODCHAVE2", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CCIDADE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODIMAGEM", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODCONTROLE", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
