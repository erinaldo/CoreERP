using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartCondicaoPgto : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartCondicaoPgto()
        {
            this.TableName = "VCONDICAOPGTO";
            this.Keys = new string[] { "CODEMPRESA", "CODCONDICAO" };
            this.FormEditName = "PSPartCondicaoPgtoEdit";
            this.PSPartData = new PSPartCondicaoPgtoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("TAXAJUROS",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartCondicaoPgtoComposicao(), true, "CODEMPRESA", "CODCONDICAO"));

            this.SecurityID = "PSPartCondicaoPgto";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("VALORMINIMO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("TAXAJUROS", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("TIPO", null, typeof(PS.Lib.WinForms.PSComboBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODCONDICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
