using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartFilial : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartFilial()
        {
            this.TableName = "GFILIAL";
            this.Keys = new string[] { "CODEMPRESA", "CODFILIAL" };
            this.FormEditName = "PSPartFilialEdit";
            this.PSPartData = new PSPartFilialData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODCIDADE", false));

            this.SecurityID = "PSPartFilial";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            List<ComboBoxItem> Item = new List<ComboBoxItem>();
            Item.Add(new ComboBoxItem(1, "Simples Nacional"));
            Item.Add(new ComboBoxItem(3, "Regime Normal"));

            objArr.Add(new DataField("CODREGIMETRIBUTARIO", null, typeof(PS.Lib.WinForms.PSComboBox), Item));

            objArr.Add(new DataField("WEBSITE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("EMAIL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("TELEFONE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("INSCRICAOMUNICIPAL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("INSCRICAOESTADUAL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CGCCPF", null, typeof(PS.Lib.WinForms.PSMaskedTextBox)));
            objArr.Add(new DataField("NOMEFANTASIA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODFILIAL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
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

            objArr.Add(new DataField("CGCCPF", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODCIDADE", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
