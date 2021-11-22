using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartObjeto : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartObjeto()
        {
            this.TableName = "OOBJETO";
            this.Keys = new string[] { "CODEMPRESA", "CODOBJETO" };
            this.FormEditName = "PSPartObjetoEdit";
            this.PSPartData = new PSPartObjetoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartObjetoMedidor(), "CODEMPRESA", "CODOBJETO"));

            this.SecurityID = "PSPartObjeto";
            this.ModuleID = "OF";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODCLIFOR", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("ANOMODELO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("ANOFABRICACAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODSITUACAO", null, typeof(PS.Lib.WinForms.PSComboBox)));
            objArr.Add(new DataField("CODSUBMODELO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODMODELO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODTIPOBJETO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODOBJETO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
