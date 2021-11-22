using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartCheque : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartCheque()
        {
            this.TableName = "FCHEQUE";
            this.Keys = new string[] { "CODEMPRESA", "CODBANCO", "CODAGENCIA", "NUMCONTA", "NUMERO" };
            this.FormEditName = "PSPartChequeEdit";
            this.PSPartData = new PSPartChequeData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VALOR",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("VINCULADO", 0));

            this.SecurityID = "PSPartCheque";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODCHEQUE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("VINCULADO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("DATABOA", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATA", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("VALOR", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("NUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NUMCONTA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODAGENCIA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODBANCO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODIMAGEM", null, typeof(PS.Lib.WinForms.PSImageBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
