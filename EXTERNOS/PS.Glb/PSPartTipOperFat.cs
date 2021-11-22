using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTipOperFat : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartTipOperFat()
        {
            this.TableName = "GTIPOPERFAT";
            this.Keys = new string[] { "CODEMPRESA", "CODTIPOPER", "CODTIPOPERFAT" };
            this.FormEditName = "PSPartTipOperFatEdit";
            this.PSPartData = new PSPartTipOperFatData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartTipOperFat";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
