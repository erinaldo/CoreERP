using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartAcessoMenu : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartAcessoMenu()
        {
            this.TableName = "GACESSOMENU";
            this.Keys = new string[] { "CODEMPRESA", "CODPERFIL", "CODPSPART" };
            this.FormEditName = "PSPartAcessoMenuEdit";
            this.PSPartData = new PSPartAcessoMenuData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODPERFIL", false));

            this.SecurityID = "PSPartAcessoMenu";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(System.Int32)));
            objArr.Add(new DataField("CODPERFIL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("PERMITEEXCLUIR", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("PERMITEALTERAR", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("PERMITEINCLUIR", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("ACESSO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("CODPSPART", null, typeof(PS.Lib.WinForms.PSLookup)));

            return objArr;
        }
    }
}
