using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartRegiao : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartRegiao()
        {
            this.TableName = "VREGIAO";
            this.Keys = new string[] { "CODEMPRESA", "CODREGIAO" };
            this.FormEditName = "PSPartRegiaoEdit";
            this.PSPartData = new PSPartRegiaoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartRegiaoEstado(), true, "CODEMPRESA", "CODREGIAO"));

            this.SecurityID = "PSPartRegiao";
            this.ModuleID = "VR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(System.Int32)));
            objArr.Add(new DataField("CODREGIAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
