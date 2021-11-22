using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartNaturezaRegraTributacao : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartNaturezaRegraTributacao()
        {
            this.TableName = "VNATUREZAREGRATRIBUTACAO";
            this.Keys = new string[] { "CODEMPRESA", "CODNATUREZA", "NSEQREGRA" };
            this.FormEditName = "PSPartNaturezaRegraTributacaoEdit";
            this.PSPartData = new PSPartNaturezaRegraTributacaoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartNaturezaRegraTributacao";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODNATUREZA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NSEQREGRA", null, typeof(PS.Lib.WinForms.PSMoedaBox)));

            List<ComboBoxItem> Item = new List<ComboBoxItem>();
            Item.Add(new ComboBoxItem("1", "Estado"));
            Item.Add(new ComboBoxItem("2", "Região"));

            objArr.Add(new DataField("TIPOREGRA", null, typeof(PS.Lib.WinForms.PSComboBox), Item));

            objArr.Add(new DataField("CODETD", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODREGIAO", null, typeof(PS.Lib.WinForms.PSLookup)));

            List<ComboBoxItem> Item1 = new List<ComboBoxItem>();
            Item1.Add(new ComboBoxItem("S", "Sim"));
            Item1.Add(new ComboBoxItem("N", "Não"));
            Item1.Add(new ComboBoxItem("D", "Depende"));

            objArr.Add(new DataField("CONTRIBUINTE", null, typeof(PS.Lib.WinForms.PSComboBox), Item1));

            return objArr;
        }
    }
}
