using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartBanco : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartBanco()
        {
            this.TableName = "GBANCO";
            this.Keys = new string[] { "CODEMPRESA", "CODBANCO" };
            this.FormEditName = "PSPartBancoEdit";
            this.PSPartData = new PSPartBancoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartAgencia(), "CODEMPRESA", "CODBANCO"));

            this.SecurityID = "PSPartBanco";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODIMAGEM", null, typeof(PS.Lib.WinForms.PSImageBox)));
            objArr.Add(new DataField("CODFEBRABAN", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODBANCO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
