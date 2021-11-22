using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTipoObjeto : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartTipoObjeto()
        {
            this.TableName = "OTIPOBJETO";
            this.Keys = new string[] { "CODEMPRESA", "CODTIPOBJETO" };
            this.FormEditName = "PSPartTipoObjetoEdit";
            this.PSPartData = new PSPartTipoObjetoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartTipoObjeto";
            this.ModuleID = "OF";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPOBJETO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
