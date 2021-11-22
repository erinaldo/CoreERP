using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartLocalEstoque : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartLocalEstoque()
        {
            this.TableName = "VLOCALESTOQUE";
            this.Keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL" };
            this.FormEditName = "PSPartLocalEstoqueEdit";
            this.PSPartData = new PSPartLocalEstoqueData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODCIDADE", false));

            this.SecurityID = "PSPartLocalEstoque";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("DESCRIÇÃO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODLOCAL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODFILIAL", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODETD", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCIDADE", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODPAIS", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("BAIRRO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPOBAIRRO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("COMPLEMENTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("RUA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPORUA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CEP", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            List<ComboBoxItem> ItemCODTIPLOC = new List<ComboBoxItem>();
            ItemCODTIPLOC.Add(new ComboBoxItem("NN", "Nosso em nosso poder"));
            ItemCODTIPLOC.Add(new ComboBoxItem("NT", "Nosso em poder de terceiro"));
            ItemCODTIPLOC.Add(new ComboBoxItem("TN", "De terceiro em nosso poder"));
            objArr.Add(new DataField("CODTIPLOC", null, typeof(PS.Lib.WinForms.PSComboBox), ItemCODTIPLOC));

            return objArr;
        }
    }
}
