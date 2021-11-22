using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartCidade : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartCidade()
        {
            this.TableName = "GCIDADE";
            this.Keys = new string[] { "CODETD", "CODCIDADE" };
            this.FormEditName = "PSPartCidadeEdit";
            this.PSPartData = new PSPartCidadeData();

            this.SecurityID = "PSPartCidade";
            this.ModuleID = "PG";

            this.AllowEdit = false;
            this.AllowDelete = false;
            this.AllowInsert = false;
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODCIDADE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODETD", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
