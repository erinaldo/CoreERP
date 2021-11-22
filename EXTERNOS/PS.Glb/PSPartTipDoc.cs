using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTipDoc : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartTipDoc()
        {
            this.TableName = "FTIPDOC";
            this.Keys = new string[] { "CODEMPRESA", "CODTIPDOC"};
            this.FormEditName = "PSPartTipDocEdit";
            this.PSPartData = new PSPartTipDocData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.SecurityID = "PSPartTipDoc";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("ULTIMONUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("USANUMEROSEQ", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPDOC", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            List<ComboBoxItem> Item1 = new List<ComboBoxItem>();
            Item1.Add(new ComboBoxItem(0, "Pagar"));
            Item1.Add(new ComboBoxItem(1, "Receber"));
            Item1.Add(new ComboBoxItem(2, "Ambos"));

            objArr.Add(new DataField("PAGREC", null, typeof(PS.Lib.WinForms.PSComboBox), Item1));

            List<ComboBoxItem> Item2 = new List<ComboBoxItem>();
            Item2.Add(new ComboBoxItem(0, "Adiantamento"));
            Item2.Add(new ComboBoxItem(1, "Devolução"));
            Item2.Add(new ComboBoxItem(2, "Sem Classificação"));
            Item2.Add(new ComboBoxItem(3, "Previsão"));

            objArr.Add(new DataField("CLASSIFICACAO", null, typeof(PS.Lib.WinForms.PSComboBox), Item2));

            return objArr;
        }
    }
}
