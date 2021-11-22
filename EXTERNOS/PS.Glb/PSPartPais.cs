using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartPais : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartPais()
        {
            this.TableName = "GPAIS";
            this.Keys = new string[] { "CODPAIS" };
            this.FormEditName = "PSPartPaisEdit";
            this.PSPartData = new PSPartPaisData();

            this.SecurityID = "PSPartPais";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODREDUZIDO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODPAIS", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            objArr.Add(new DataField("CODBACEN", null, typeof(PS.Lib.WinForms.PSTextoBox)));


            return objArr;
        }
    }
}
