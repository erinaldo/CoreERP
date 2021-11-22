using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Lib.WinForms.Query
{
    public class PSPartQuery : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartQuery()
        {
            this.TableName = "GQUERY";
            this.Keys = new string[] { "CODEMPRESA", "CODQUERY" };
            this.FormEditName = "PSPartQueryEdit";
            this.PSPartData = new PSPartQueryData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.PSPartApp.Add(new PSPartQueryApp());

            this.SecurityID = "PSPartQuery";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODQUERY", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
