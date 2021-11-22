using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartPart : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartPart()
        {
            this.TableName = "GPSPART";
            this.Keys = new string[] { "CODPSPART" };
            this.FormEditName = "PSPartPartEdit";
            this.PSPartData = new PSPartPartData();

            this.SecurityID = "PSPartPart";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
