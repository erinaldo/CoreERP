using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartFeriado : PS.Lib.WinForms.PSPart
    {
        public PSPartFeriado()
        {
            this.TableName = "GFERIADO";
            this.Keys = new string[] { "CODCALENDARIO", "DATA"};
            this.FormEditName = "PSPartFeriadoEdit";
            this.PSPartData = new PSPartFeriadoData();

            this.SecurityID = "PSPartFeriado";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DATA", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("CODCALENDARIO", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
