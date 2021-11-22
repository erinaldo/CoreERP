using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartConta : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartConta()
        {
            this.TableName = "FCONTA";
            this.Keys = new string[] { "CODEMPRESA", "CODCONTA"};
            this.FormEditName = "PSPartContaEdit";
            this.PSPartData = new PSPartContaData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("SALDODATABASE",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.SecurityID = "PSPartConta";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("SALDODATABASE", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("DTBASE", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("CODCONTA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NUMCONTA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODAGENCIA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODBANCO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
