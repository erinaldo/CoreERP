using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartNatureza : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartNatureza()
        {
            this.TableName = "VNATUREZA";
            this.Keys = new string[] { "CODEMPRESA", "CODNATUREZA" };
            this.FormEditName = "PSPartNaturezaEdit";
            this.PSPartData = new PSPartNaturezaData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ULTIMONIVEL", 1));

            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartNaturezaRegraTributacao(), true, "CODEMPRESA", "CODNATUREZA"));
            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartNaturezaTributo(), true, "CODEMPRESA", "CODNATUREZA"));

            this.SecurityID = "PSPartNatureza";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("ULTIMONIVEL", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("DENTRODOESTADO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("CONTRIBUINTEICMS", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODNATUREZA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODMENSAGEM", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            List<ComboBoxItem> Item = new List<ComboBoxItem>();
            Item.Add(new ComboBoxItem("E", "Entrada"));
            Item.Add(new ComboBoxItem("S", "Saída"));
            objArr.Add(new DataField("TIPENTSAI", null, typeof(PS.Lib.WinForms.PSComboBox), Item));

            List<ComboBoxItem> ItemCLASSVENDA2 = new List<ComboBoxItem>();
            ItemCLASSVENDA2.Add(new ComboBoxItem("O", "Outra"));
            ItemCLASSVENDA2.Add(new ComboBoxItem("V", "Venda"));
            ItemCLASSVENDA2.Add(new ComboBoxItem("RS", "Revenda Sem CST"));
            ItemCLASSVENDA2.Add(new ComboBoxItem("RC", "Revenda Com CST"));
            ItemCLASSVENDA2.Add(new ComboBoxItem("CC", "Consumo Contribuinte"));
            ItemCLASSVENDA2.Add(new ComboBoxItem("CN", "Comsumo Não Contribuinte"));
            objArr.Add(new DataField("CLASSVENDA2", null, typeof(PS.Lib.WinForms.PSComboBox), ItemCLASSVENDA2));

            objArr.Add(new DataField("IDREGRAICMS", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("IDREGRAIPI", null, typeof(PS.Lib.WinForms.PSLookup)));

            return objArr;
        }
    }
}
