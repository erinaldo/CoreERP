using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartCondicaoPgtoComposicao : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartCondicaoPgtoComposicao()
        {
            this.TableName = "VCONDICAOPGTOCOMPOSICAO";
            this.Keys = new string[] { "CODEMPRESA", "CODCONDICAO", "IDCOMPOSICAO" };
            this.FormEditName = "PSPartCondicaoPgtoComposicaoEdit";
            this.PSPartData = new PSPartCondicaoPgtoComposicaoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODCONDICAO", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("PERCVALOR",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.SecurityID = "PSPartCondicaoPgtoComposicao";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            List<ComboBoxItem> Item1 = new List<ComboBoxItem>();
            Item1.Add(new ComboBoxItem("N", "Normal"));
            Item1.Add(new ComboBoxItem("S", "Fora Semana"));
            Item1.Add(new ComboBoxItem("D", "Fora Dezena"));
            Item1.Add(new ComboBoxItem("Q", "Fora Quinzena"));
            Item1.Add(new ComboBoxItem("M", "Fora Mês"));
            Item1.Add(new ComboBoxItem("A", "Fora Ano"));

            objArr.Add(new DataField("TIPO", null, typeof(PS.Lib.WinForms.PSComboBox), Item1));
            objArr.Add(new DataField("PERCVALOR", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("NUMINTERVALO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NUMPRAZO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NUMPARCELAS", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("IDCOMPOSICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODCONDICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
