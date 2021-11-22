using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTabDinamica : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartTabDinamica()
        {
            this.TableName = "GTABDINAMICA";
            this.Keys = new string[] { "CODEMPRESA", "CODTABELA" };
            this.FormEditName = "PSPartTabDinamicaEdit";
            this.PSPartData = new PSPartTabDinamicaData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.Folder.Add(new PS.Lib.WinForms.Folder(new PS.Glb.PSPartTabDinamicaItem(), true, "CODEMPRESA", "CODTABELA"));

            this.SecurityID = "PSPartTabDinamica";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTABELA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
