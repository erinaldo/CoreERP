using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartNaturezaOrcamento : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartNaturezaOrcamento()
        {
            this.TableName = "VNATUREZAORCAMENTO";
            this.Keys = new string[] { "CODEMPRESA", "CODNATUREZA" };
            this.FormEditName = "PSPartNaturezaOrcamentoEdit";
            this.PSPartData = new PSPartNaturezaOrcamentoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("PERMITELANCAMENTO", 1));
            

            this.SecurityID = "PSPartNaturezaOrcamento";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(System.Int32)));
            objArr.Add(new DataField("CODNATUREZA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("PERMITELANCAMENTO", null, typeof(PS.Lib.WinForms.PSCheckBox)));

            List<ComboBoxItem> Item = new List<ComboBoxItem>();
            Item.Add(new ComboBoxItem(0, "Entrada"));
            Item.Add(new ComboBoxItem(1, "Saida"));

            objArr.Add(new DataField("NATUREZA", null, typeof(PS.Lib.WinForms.PSComboBox), Item));

            return objArr;
        }
    }
}
