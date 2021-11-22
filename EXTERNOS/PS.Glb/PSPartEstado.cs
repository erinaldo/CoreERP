using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartEstado: PS.Lib.WinForms.PSPartGridView
    {
        public PSPartEstado()
        {
            this.TableName = "GESTADO";
            this.Keys = new string[] { "CODETD" };
            this.FormEditName = "PSPartEstadoEdit";
            this.PSPartData = new PSPartEstadoData();

            this.SecurityID = "PSPartEstado";
            this.ModuleID = "PG";

         
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODETD", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODIBGE", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
