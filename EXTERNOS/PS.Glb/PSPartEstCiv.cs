using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartEstCiv : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartEstCiv()
        {
            this.TableName = "GESTCIV";
            this.Keys = new string[] { "GESTCIV" };
            this.FormEditName = "PSPartEstCivEdit";
            this.PSPartData = new PSPartEstCivData();

            this.AllowInsert = false;
            this.AllowEdit = false;
            this.AllowDelete = false;

            this.SecurityID = "PSPartEstCiv";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
