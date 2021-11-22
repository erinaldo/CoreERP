using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTipOperSerie : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartTipOperSerie()
        {
            this.TableName = "VTIPOPERSERIE";
            this.Keys = new string[] { "CODEMPRESA", "CODTIPOPER", "CODSERIE", "CODFILIAL" };
            this.FormEditName = "PSPartTipOperSerieEdit";
            this.PSPartData = new PSPartTipOperSerieData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODTIPOPER",false));

            this.SecurityID = "PSPartTipOperSerie";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
