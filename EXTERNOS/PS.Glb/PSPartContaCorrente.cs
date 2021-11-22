using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartContaCorrente : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartContaCorrente()
        {
            this.TableName = "FCCORRENTE";
            this.Keys = new string[] { "CODEMPRESA", "CODBANCO", "CODAGENCIA", "NUMCONTA" };
            this.FormEditName = "PSPartContaCorrenteEdit";
            this.PSPartData = new PSPartContaCorrenteData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartContaCorrente";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(System.Int32)));
            objArr.Add(new DataField("CODBANCO", null, typeof(System.String)));
            objArr.Add(new DataField("CODAGENCIA", null, typeof(System.String)));
            objArr.Add(new DataField("DIGCONTA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NUMCONTA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
