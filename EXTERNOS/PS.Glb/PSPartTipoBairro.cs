using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTipoBairro : PS.Lib.WinForms.PSPart
    {
        public PSPartTipoBairro()
        {
            this.TableName = "GTIPOBAIRRO";
            this.Keys = new string[] { "CODTIPOBAIRRO" };
            this.FormEditName = "PSPartTipoBairroEdit";
            this.PSPartData = new PSPartTipoBairroData();

            this.AllowInsert = false;
            this.AllowDelete = false;
            this.AllowEdit = false;

            this.SecurityID = "PSPartTipoBairro";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
