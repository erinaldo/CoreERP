using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTipoRua : PS.Lib.WinForms.PSPart
    {
        public PSPartTipoRua()
        {
            this.TableName = "GTIPORUA";
            this.Keys = new string[] { "CODTIPORUA" };
            this.FormEditName = "PSPartTipoRuaEdit";
            this.PSPartData = new PSPartTipoRuaData();

            this.AllowInsert = false;
            this.AllowDelete = false;
            this.AllowEdit = false;

            this.SecurityID = "PSPartTipoRua";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
